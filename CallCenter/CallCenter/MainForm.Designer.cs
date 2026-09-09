using System.Drawing;
using System.Windows.Forms;

namespace CallCenterApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblCallerName;
        private TextBox txtCallerName;
        private Button btnMakeCall;
        private Button btnViewLog;
        private Button btnSpeak;
        private Button btnHold;
        private Button btnHangUp;
        private Button btnGroupCalls;

        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnPrevious;
        private Button btnNext;

        private ListBox lstCalls;
        private Timer timerTick;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblCallerName = new System.Windows.Forms.Label();
            this.txtCallerName = new System.Windows.Forms.TextBox();
            this.btnMakeCall = new System.Windows.Forms.Button();
            this.btnViewLog = new System.Windows.Forms.Button();
            this.btnSpeak = new System.Windows.Forms.Button();
            this.btnHold = new System.Windows.Forms.Button();
            this.btnHangUp = new System.Windows.Forms.Button();
            this.btnGroupCalls = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lstCalls = new System.Windows.Forms.ListBox();
            this.timerTick = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblCallerName
            // 
            this.lblCallerName.AutoSize = true;
            this.lblCallerName.Location = new System.Drawing.Point(20, 20);
            this.lblCallerName.Name = "lblCallerName";
            this.lblCallerName.Size = new System.Drawing.Size(97, 20);
            this.lblCallerName.TabIndex = 0;
            this.lblCallerName.Text = "Caller name:";
            // 
            // txtCallerName
            // 
            this.txtCallerName.Location = new System.Drawing.Point(110, 17);
            this.txtCallerName.Name = "txtCallerName";
            this.txtCallerName.Size = new System.Drawing.Size(150, 26);
            this.txtCallerName.TabIndex = 1;
            this.txtCallerName.TextChanged += new System.EventHandler(this.txtCallerName_TextChanged);
            // 
            // btnMakeCall
            // 
            this.btnMakeCall.Location = new System.Drawing.Point(280, 15);
            this.btnMakeCall.Name = "btnMakeCall";
            this.btnMakeCall.Size = new System.Drawing.Size(100, 23);
            this.btnMakeCall.TabIndex = 2;
            this.btnMakeCall.Text = "Make Call";
            this.btnMakeCall.Click += new System.EventHandler(this.BtnMakeCall_Click);
            // 
            // btnViewLog
            // 
            this.btnViewLog.Location = new System.Drawing.Point(390, 15);
            this.btnViewLog.Name = "btnViewLog";
            this.btnViewLog.Size = new System.Drawing.Size(100, 23);
            this.btnViewLog.TabIndex = 3;
            this.btnViewLog.Text = "View Log";
            this.btnViewLog.Click += new System.EventHandler(this.BtnViewLog_Click);
            // 
            // btnSpeak
            // 
            this.btnSpeak.Location = new System.Drawing.Point(20, 55);
            this.btnSpeak.Name = "btnSpeak";
            this.btnSpeak.Size = new System.Drawing.Size(100, 23);
            this.btnSpeak.TabIndex = 4;
            this.btnSpeak.Text = "Speak";
            this.btnSpeak.Click += new System.EventHandler(this.BtnSpeak_Click);
            // 
            // btnHold
            // 
            this.btnHold.Location = new System.Drawing.Point(130, 55);
            this.btnHold.Name = "btnHold";
            this.btnHold.Size = new System.Drawing.Size(100, 23);
            this.btnHold.TabIndex = 5;
            this.btnHold.Text = "Hold";
            this.btnHold.Click += new System.EventHandler(this.BtnHold_Click);
            // 
            // btnHangUp
            // 
            this.btnHangUp.Location = new System.Drawing.Point(240, 55);
            this.btnHangUp.Name = "btnHangUp";
            this.btnHangUp.Size = new System.Drawing.Size(100, 23);
            this.btnHangUp.TabIndex = 6;
            this.btnHangUp.Text = "Hang Up";
            this.btnHangUp.Click += new System.EventHandler(this.BtnHangUp_Click);
            // 
            // btnGroupCalls
            // 
            this.btnGroupCalls.Location = new System.Drawing.Point(350, 55);
            this.btnGroupCalls.Name = "btnGroupCalls";
            this.btnGroupCalls.Size = new System.Drawing.Size(120, 23);
            this.btnGroupCalls.TabIndex = 7;
            this.btnGroupCalls.Text = "Group Calls";
            this.btnGroupCalls.Click += new System.EventHandler(this.BtnGroupCalls_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(20, 95);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(105, 20);
            this.lblSearch.TabIndex = 8;
            this.lblSearch.Text = "Search caller:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(110, 92);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(150, 26);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(280, 90);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 23);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(370, 90);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(70, 23);
            this.btnPrevious.TabIndex = 11;
            this.btnPrevious.Text = "< Prev";
            this.btnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(445, 90);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(70, 23);
            this.btnNext.TabIndex = 12;
            this.btnNext.Text = "Next >";
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // lstCalls
            // 
            this.lstCalls.ItemHeight = 20;
            this.lstCalls.Location = new System.Drawing.Point(20, 130);
            this.lstCalls.Name = "lstCalls";
            this.lstCalls.Size = new System.Drawing.Size(560, 204);
            this.lstCalls.TabIndex = 13;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(686, 471);
            this.Controls.Add(this.lblCallerName);
            this.Controls.Add(this.txtCallerName);
            this.Controls.Add(this.btnMakeCall);
            this.Controls.Add(this.btnViewLog);
            this.Controls.Add(this.btnSpeak);
            this.Controls.Add(this.btnHold);
            this.Controls.Add(this.btnHangUp);
            this.Controls.Add(this.btnGroupCalls);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lstCalls);
            this.Name = "MainForm";
            this.Text = "Call Center Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
