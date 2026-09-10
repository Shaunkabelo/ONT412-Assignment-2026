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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCallerName
            // 
            this.lblCallerName.AutoSize = true;
            this.lblCallerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblCallerName.Location = new System.Drawing.Point(16, 20);
            this.lblCallerName.Name = "lblCallerName";
            this.lblCallerName.Size = new System.Drawing.Size(166, 22);
            this.lblCallerName.TabIndex = 0;
            this.lblCallerName.Text = "ACCESS NUMBER";
            this.lblCallerName.Click += new System.EventHandler(this.lblCallerName_Click);
            // 
            // txtCallerName
            // 
            this.txtCallerName.Location = new System.Drawing.Point(177, 16);
            this.txtCallerName.Multiline = true;
            this.txtCallerName.Name = "txtCallerName";
            this.txtCallerName.Size = new System.Drawing.Size(163, 32);
            this.txtCallerName.TabIndex = 1;
            this.txtCallerName.TextChanged += new System.EventHandler(this.txtCallerName_TextChanged);
            // 
            // btnMakeCall
            // 
            this.btnMakeCall.Location = new System.Drawing.Point(12, 396);
            this.btnMakeCall.Name = "btnMakeCall";
            this.btnMakeCall.Size = new System.Drawing.Size(136, 36);
            this.btnMakeCall.TabIndex = 2;
            this.btnMakeCall.Text = "Make Call";
            this.btnMakeCall.Click += new System.EventHandler(this.BtnMakeCall_Click);
            // 
            // btnViewLog
            // 
            this.btnViewLog.Location = new System.Drawing.Point(430, 53);
            this.btnViewLog.Name = "btnViewLog";
            this.btnViewLog.Size = new System.Drawing.Size(136, 41);
            this.btnViewLog.TabIndex = 3;
            this.btnViewLog.Text = "View Log";
            this.btnViewLog.Click += new System.EventHandler(this.BtnViewLog_Click);
            // 
            // btnSpeak
            // 
            this.btnSpeak.Location = new System.Drawing.Point(466, 396);
            this.btnSpeak.Name = "btnSpeak";
            this.btnSpeak.Size = new System.Drawing.Size(136, 36);
            this.btnSpeak.TabIndex = 4;
            this.btnSpeak.Text = "Speak";
            this.btnSpeak.Click += new System.EventHandler(this.BtnSpeak_Click);
            // 
            // btnHold
            // 
            this.btnHold.Location = new System.Drawing.Point(324, 396);
            this.btnHold.Name = "btnHold";
            this.btnHold.Size = new System.Drawing.Size(136, 36);
            this.btnHold.TabIndex = 5;
            this.btnHold.Text = "Hold";
            this.btnHold.Click += new System.EventHandler(this.BtnHold_Click);
            // 
            // btnHangUp
            // 
            this.btnHangUp.Location = new System.Drawing.Point(165, 396);
            this.btnHangUp.Name = "btnHangUp";
            this.btnHangUp.Size = new System.Drawing.Size(136, 36);
            this.btnHangUp.TabIndex = 6;
            this.btnHangUp.Text = "Hang Up";
            this.btnHangUp.Click += new System.EventHandler(this.BtnHangUp_Click);
            // 
            // btnGroupCalls
            // 
            this.btnGroupCalls.Location = new System.Drawing.Point(623, 401);
            this.btnGroupCalls.Name = "btnGroupCalls";
            this.btnGroupCalls.Size = new System.Drawing.Size(120, 31);
            this.btnGroupCalls.TabIndex = 7;
            this.btnGroupCalls.Text = "Add Call";
            this.btnGroupCalls.Click += new System.EventHandler(this.BtnGroupCalls_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSearch.Location = new System.Drawing.Point(292, 106);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(132, 25);
            this.lblSearch.TabIndex = 8;
            this.lblSearch.Text = "Search caller:";
            this.lblSearch.Click += new System.EventHandler(this.lblSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(430, 98);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(232, 38);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(668, 98);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 38);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(592, 55);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(70, 38);
            this.btnPrevious.TabIndex = 11;
            this.btnPrevious.Text = "< Prev";
            this.btnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(668, 53);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(70, 38);
            this.btnNext.TabIndex = 12;
            this.btnNext.Text = "Next >";
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // lstCalls
            // 
            this.lstCalls.ItemHeight = 20;
            this.lstCalls.Location = new System.Drawing.Point(12, 146);
            this.lstCalls.Name = "lstCalls";
            this.lstCalls.Size = new System.Drawing.Size(731, 244);
            this.lstCalls.TabIndex = 13;
            this.lstCalls.SelectedIndexChanged += new System.EventHandler(this.lstCalls_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(120, 54);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(207, 39);
            this.textBox1.TabIndex = 14;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(15, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "Receiver :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1007, 471);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
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

        private TextBox textBox1;
        private Label label1;
    }
}
