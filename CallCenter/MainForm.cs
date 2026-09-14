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
        private ICallService service;
        private SingleCall currentCall;
        private CallLogIterator navigator;
        private bool refreshing;
        private bool storageAvailable = true;

        // Keep the constructor free of runtime work so Visual Studio can open the Designer.
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode) return;
            service = new CallProxy(callLog);
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
            currentCall = service.ReturnCall(lstCalls.SelectedItem as SingleCall, txtCallerName.Text,
                (UserRole)cmbRole.SelectedItem, (PhoneDevice)cmbDevice.SelectedItem);
            RebuildLog();
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
        private void BtnMakeCall_Click(object sender, EventArgs e) => Act(() => {
            currentCall = service.MakeCall(txtCallerName.Text, txtReceiver.Text, (UserRole)cmbRole.SelectedItem, (PhoneDevice)cmbDevice.SelectedItem);
            RebuildLog();
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
        private void SelectCurrent()
        {
            refreshing = true;
            lstCalls.SelectedItem = navigator.CurrentItem();
            refreshing = false;
            RefreshLog();
        }
        private void RebuildLog()
        {
            refreshing = true;
            lstCalls.Items.Clear();
            navigator = callLog.CreateIterator(txtSearch.Text);
            for (navigator.First(); !navigator.IsDone(); navigator.Next()) lstCalls.Items.Add(navigator.CurrentItem());
            navigator.First();
            refreshing = false;
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
            if (item is CallGroup group)
                foreach (var child in group.Children) node.Nodes.Add(BuildNode(child));
            else if (item is SingleCall leaf) node.Text = leaf.Call.Caller + " → " + leaf.Call.Receiver;
            return node;
        }
        private void TimerTick_Tick(object sender, EventArgs e) => RefreshLog();
        private void RefreshLog()
        {
            var call = currentCall?.Call;
            lblStatus.Text = call == null ? "Ready. Enter participants and make a call." :
                $"Active session: {call.Caller} → {call.Receiver} | {call.State.Name} | Talk duration: {call.Duration:hh\\:mm\\:ss} | {call.Device}\n{call.Speak()}";
            bool active = call != null && !call.IsEnded;
            btnMakeCall.Enabled = !active;
            btnHold.Enabled = active && call.State is OnCallState;
            btnResume.Enabled = active && call.State is OnHoldState;
            btnHangUp.Enabled = active;
            btnSpeak.Enabled = active;
            btnReturnCall.Enabled = !active && lstCalls.SelectedItem is SingleCall;
            refreshing = true;
            for (int i = 0; i < lstCalls.Items.Count; i++) lstCalls.Items[i] = lstCalls.Items[i];
            refreshing = false;
            if (lstCalls.Items.Count == 0) lblStatus.Text += "\nNo calls match the current search.";
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
        private void lstCalls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (refreshing || navigator == null) return;
            navigator.First();
            for (int i = 0; i < lstCalls.SelectedIndex; i++) navigator.Next();
        }
    }
}

