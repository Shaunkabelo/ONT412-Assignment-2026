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
            
        }

        private void BtnSpeak_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnHold_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnHangUp_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnGroupCalls_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
           
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnViewLog_Click(object sender, EventArgs e) => RefreshLog();

        private void TimerTick_Tick(object sender, EventArgs e)
        {
            currentCall?.GetCall().Tick();
            RefreshLog();
        }

        private void RefreshLog()
        {
            
        }

        private void txtCallerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void lstCalls_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }

        private void lblCallerName_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
