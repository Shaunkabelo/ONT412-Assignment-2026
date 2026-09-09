using CallCenterApp.Models;
using System;
using System.Windows.Forms;

namespace CallCenterApp
{
    public partial class MainForm : Form
    {
        private readonly CallLog callLog = new CallLog();
        private SingleCall currentCall;
        private CallLogIterator navigator; // drives the Next/Previous buttons

        public MainForm()
        {
            InitializeComponent(); // calls the Designer code
            timerTick.Tick += TimerTick_Tick;
        }

        private void BtnMakeCall_Click(object sender, EventArgs e)
        {
            string name = string.IsNullOrWhiteSpace(txtCallerName.Text)
                ? "Student Caller"
                : txtCallerName.Text.Trim();

            var call = new Call(name);
            var proxy = new CallProxy(call);
            var accessedCall = proxy.AccessCall();

            currentCall = new SingleCall(accessedCall);
            callLog.AddCall(currentCall);
            timerTick.Start();
            RefreshLog();
        }

        private void BtnSpeak_Click(object sender, EventArgs e)
        {
            currentCall?.GetCall().Speak();
            RefreshLog();
        }

        private void BtnHold_Click(object sender, EventArgs e)
        {
            currentCall?.GetCall().Hold();
            RefreshLog();
        }

        private void BtnHangUp_Click(object sender, EventArgs e)
        {
            currentCall?.GetCall().HangUp();
            timerTick.Stop();
            RefreshLog();
        }

        private void BtnGroupCalls_Click(object sender, EventArgs e)
        {
            // Composite pattern in action: bundle every single call currently
            // in the log into one named group entry the manager can call back.
            var group = new CallGroup("Manager Callback Batch");
            var it = callLog.GetIterator();
            bool any = false;

            while (it.HasNext())
            {
                var component = it.Next();
                if (component is SingleCall)
                {
                    group.Add(component);
                    any = true;
                }
            }

            if (any)
            {
                callLog.AddCall(group);
                RefreshLog();
            }
            else
            {
                MessageBox.Show("No calls available to group yet.");
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var found = callLog.FindByCallerName(txtSearch.Text.Trim());
            if (found != null)
            {
                lstCalls.SelectedIndex = lstCalls.Items.IndexOf(found.Info());
            }
            else
            {
                MessageBox.Show("No matching call found in the log.");
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (navigator == null)
            {
                navigator = callLog.GetIterator();
            }

            if (navigator.HasNext())
            {
                var component = navigator.Next();
                lstCalls.SelectedIndex = lstCalls.Items.IndexOf(component.Info());
            }
            else
            {
                MessageBox.Show("Reached the end of the call log.");
            }
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (navigator != null && navigator.HasPrevious())
            {
                var component = navigator.Previous();
                lstCalls.SelectedIndex = lstCalls.Items.IndexOf(component.Info());
            }
            else
            {
                MessageBox.Show("Already at the start of the call log.");
            }
        }

        private void BtnViewLog_Click(object sender, EventArgs e) => RefreshLog();

        private void TimerTick_Tick(object sender, EventArgs e)
        {
            currentCall?.GetCall().Tick();
            RefreshLog();
        }

        private void RefreshLog()
        {
            lstCalls.Items.Clear();
            var it = callLog.GetIterator();
            while (it.HasNext())
            {
                lstCalls.Items.Add(it.Next().Info());
            }
            navigator = null; // start Next/Previous over whenever the log changes
        }

        private void txtCallerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
