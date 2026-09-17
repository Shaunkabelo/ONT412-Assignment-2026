using CallCenterApp.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CallCenterApp
{
    public partial class MainForm : Form
    {
        private readonly CallLog callLog = new CallLog();
        private readonly CallLogStore store = new CallLogStore(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CallCenterAssignment2", "calls.json"));
        private SubjectBase service;
        private SingleCall currentCall;
        private IteratorBase navigator;
        private bool refreshing;
        private bool storageAvailable = true;


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode) return;
            service = new CallProxy(new RealCallService(callLog));
            cmbRole.DataSource = Enum.GetValues(typeof(UserRole));
            cmbDevice.DataSource = Enum.GetValues(typeof(PhoneDevice));
            try { store.Load(callLog); }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is System.Text.Json.JsonException || ex is ArgumentException)
            {
                storageAvailable = false;
                MessageBox.Show("Could not load history. Saving is disabled to protect the existing file.\n" + ex.Message, "History error");
            }
            RebuildLog();
            timerTick.Start();
        }

        private void BtnResume_Click(object sender, EventArgs e) => Act(() => Active().Resume());

        private void BtnReturnCall_Click(object sender, EventArgs e) => Act(() =>
        {
            currentCall = service.ReturnCall(SelectedCall, txtCallerName.Text,
                (UserRole)cmbRole.SelectedItem, (PhoneDevice)cmbDevice.SelectedItem);
            RebuildLog(currentCall);
        });
        private Call Active()
        {
            if (currentCall == null || currentCall.Call.IsEnded) throw new InvalidOperationException("Make a call first.");
            return currentCall.Call;
        }
        private void Act(Action action)
        {
            try { action(); RefreshLog(); }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException || ex is UnauthorizedAccessException || ex is IOException)
            { MessageBox.Show(ex.Message, "Call center", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
        private void BtnMakeCall_Click(object sender, EventArgs e) => Act(() =>
        {
            currentCall = service.MakeCall(txtCallerName.Text, txtReceiver.Text, (UserRole)cmbRole.SelectedItem, (PhoneDevice)cmbDevice.SelectedItem);
            RebuildLog(currentCall);
        });
        private void BtnSpeak_Click(object sender, EventArgs e) => Act(() => MessageBox.Show(Active().Speak(), "Speak"));
        private void BtnHold_Click(object sender, EventArgs e) => Act(() => Active().Hold());
        private void BtnHangUp_Click(object sender, EventArgs e) => Act(() => { Active().HangUp(); SaveHistory(); RebuildLog(); });
        private void SaveHistory()
        {
            if (!storageAvailable) throw new IOException("Saving is disabled because history could not be loaded. Calls remain in memory for this session.");
            store.Save(callLog);
        }
        private void BtnGroupCalls_Click(object sender, EventArgs e) => RefreshGroups();
        private void BtnSearch_Click(object sender, EventArgs e) => RebuildLog();
        private void BtnNext_Click(object sender, EventArgs e) { navigator.Next(); SelectCurrent(); }
        private void BtnPrevious_Click(object sender, EventArgs e) { navigator.Previous(); SelectCurrent(); }
        private void BtnViewLog_Click(object sender, EventArgs e) { txtSearch.Clear(); RebuildLog(); }
        private SingleCall SelectedCall => dgvCalls.SelectedRows.Count == 0
            ? null : dgvCalls.SelectedRows[0].Tag as SingleCall;

        private void SelectCurrent()
        {
            refreshing = true;
            try
            {
                dgvCalls.ClearSelection();
                dgvCalls.CurrentCell = null;
                var selected = navigator.CurrentItem();
                foreach (DataGridViewRow row in dgvCalls.Rows)
                {
                    if (ReferenceEquals(row.Tag, selected))
                    {
                        dgvCalls.CurrentCell = row.Cells[0];
                        row.Selected = true;
                        dgvCalls.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
            finally { refreshing = false; }
            RefreshLog();
        }
        private void RebuildLog(SingleCall preferred = null)
        {
            var selected = preferred ?? SelectedCall;
            refreshing = true;
            try
            {
                dgvCalls.Rows.Clear();
                navigator = callLog.CreateIterator(txtSearch.Text);

                for (var item = navigator.First(); !navigator.IsDone(); item = navigator.Next())
                {
                    var call = item.Call;
                    int index = dgvCalls.Rows.Add(call.StartedAt, call.Caller, call.Receiver,
                        call.Role, call.Device, call.StateName, call.Duration.ToString(@"hh\:mm\:ss"),
                        call.Id.ToString().Substring(0, 8));
                    dgvCalls.Rows[index].Tag = item;
                }
                navigator.First();
                if (selected != null)
                {
                    while (!navigator.IsDone() && !ReferenceEquals(navigator.CurrentItem(), selected)) navigator.Next();
                    if (navigator.IsDone()) navigator.First();
                }
            }
            finally { refreshing = false; }
            SelectCurrent();
            RefreshGroups();
        }
        private void RefreshGroups()
        {
            treeGroups.Nodes.Clear();
            treeGroups.Nodes.Add(BuildNode(callLog.Root));
            treeGroups.ExpandAll();
        }
        private TreeNode BuildNode(CallLogComponent item)
        {
            var node = new TreeNode(item.Name + (item is CallGroup ? " (" + item.GetCalls().Count() + ")" : ""));
            if (item is CallGroup)
                foreach (var child in item) node.Nodes.Add(BuildNode(child));
            else if (item is SingleCall leaf) node.Text = leaf.Call.Caller + " → " + leaf.Call.Receiver;
            return node;
        }
        private void TimerTick_Tick(object sender, EventArgs e) => RefreshLog();
        private void RefreshLog()
        {
            var call = currentCall?.Call;
            lblStatus.Text = call == null ? "Ready. Enter participants and make a call." :
                $"Active session: {call.Caller} → {call.Receiver} | {call.StateName} | Talk duration: {call.Duration:hh\\:mm\\:ss} | {call.Device}\n{call.Speak()}";
            bool active = call != null && !call.IsEnded;
            btnMakeCall.Enabled = !active;
            btnHold.Enabled = active && call.CanHold;
            btnResume.Enabled = active && call.CanResume;
            btnHangUp.Enabled = active;
            btnSpeak.Enabled = active;
            btnReturnCall.Enabled = !active && SelectedCall != null && SelectedCall.Call.IsEnded;

            // Only refresh changing cells. Never rebuild or reorder rows on a timer tick.
            foreach (DataGridViewRow row in dgvCalls.Rows)
            {
                var logged = ((SingleCall)row.Tag).Call;
                var duration = logged.Duration.ToString(@"hh\:mm\:ss");
                if (!Equals(row.Cells["colState"].Value, logged.StateName))
                    row.Cells["colState"].Value = logged.StateName;
                if (!Equals(row.Cells["colDuration"].Value, duration))
                    row.Cells["colDuration"].Value = duration;
            }
            if (dgvCalls.Rows.Count == 0) lblStatus.Text += "\nNo calls match the current search.";
        }
        private void ClosingDashboard(object sender, FormClosingEventArgs e)
        {
            if (currentCall != null && !currentCall.Call.IsEnded) currentCall.Call.HangUp();
            if (!storageAvailable) return;
            try { SaveHistory(); }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                e.Cancel = MessageBox.Show("History could not be saved: " + ex.Message + "\nExit without saving?", "Save error",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes;
            }
        }
        private void DgvCalls_SelectionChanged(object sender, EventArgs e)
        {
            if (refreshing || navigator == null) return;
            var selected = SelectedCall;
            navigator.First();
            while (!navigator.IsDone() && !ReferenceEquals(navigator.CurrentItem(), selected)) navigator.Next();
            RefreshLog();
        }

        private void lblLogHeading_Click(object sender, EventArgs e)
        {

        }
    }
}
