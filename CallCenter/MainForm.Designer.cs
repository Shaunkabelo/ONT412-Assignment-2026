namespace CallCenterApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support. Change the layout using Visual Studio's Designer.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTitle = new System.Windows.Forms.Label();
            lblCallerName = new System.Windows.Forms.Label();
            txtCallerName = new System.Windows.Forms.TextBox();
            lblReceiver = new System.Windows.Forms.Label();
            txtReceiver = new System.Windows.Forms.TextBox();
            lblRole = new System.Windows.Forms.Label();
            cmbRole = new System.Windows.Forms.ComboBox();
            lblDevice = new System.Windows.Forms.Label();
            cmbDevice = new System.Windows.Forms.ComboBox();
            lblSearch = new System.Windows.Forms.Label();
            txtSearch = new System.Windows.Forms.TextBox();
            btnSearch = new System.Windows.Forms.Button();
            btnViewLog = new System.Windows.Forms.Button();
            btnPrevious = new System.Windows.Forms.Button();
            btnNext = new System.Windows.Forms.Button();
            btnGroupCalls = new System.Windows.Forms.Button();
            lblLogHeading = new System.Windows.Forms.Label();
            lblGroupsHeading = new System.Windows.Forms.Label();
            lstCalls = new System.Windows.Forms.ListBox();
            treeGroups = new System.Windows.Forms.TreeView();
            btnMakeCall = new System.Windows.Forms.Button();
            btnSpeak = new System.Windows.Forms.Button();
            btnHold = new System.Windows.Forms.Button();
            btnResume = new System.Windows.Forms.Button();
            btnHangUp = new System.Windows.Forms.Button();
            btnReturnCall = new System.Windows.Forms.Button();
            lblStatus = new System.Windows.Forms.Label();
            lblSimulation = new System.Windows.Forms.Label();
            timerTick = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            lblTitle.Location = new System.Drawing.Point(13, 9);
            lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(1429, 58);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Student Call Center";
            // 
            // lblCallerName
            // 
            lblCallerName.Location = new System.Drawing.Point(18, 65);
            lblCallerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblCallerName.Name = "lblCallerName";
            lblCallerName.Size = new System.Drawing.Size(221, 42);
            lblCallerName.TabIndex = 1;
            lblCallerName.Text = "Caller name / number";
            // 
            // txtCallerName
            // 
            txtCallerName.Location = new System.Drawing.Point(246, 58);
            txtCallerName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtCallerName.Name = "txtCallerName";
            txtCallerName.Size = new System.Drawing.Size(427, 31);
            txtCallerName.TabIndex = 2;
            // 
            // lblReceiver
            // 
            lblReceiver.Location = new System.Drawing.Point(718, 65);
            lblReceiver.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblReceiver.Name = "lblReceiver";
            lblReceiver.Size = new System.Drawing.Size(229, 42);
            lblReceiver.TabIndex = 3;
            lblReceiver.Text = "Receiver name / number";
            // 
            // txtReceiver
            // 
            txtReceiver.Location = new System.Drawing.Point(960, 58);
            txtReceiver.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtReceiver.Name = "txtReceiver";
            txtReceiver.Size = new System.Drawing.Size(427, 31);
            txtReceiver.TabIndex = 4;
            // 
            // lblRole
            // 
            lblRole.Location = new System.Drawing.Point(18, 138);
            lblRole.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblRole.Name = "lblRole";
            lblRole.Size = new System.Drawing.Size(221, 42);
            lblRole.TabIndex = 5;
            lblRole.Text = "Caller role";
            // 
            // cmbRole
            // 
            cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new System.Drawing.Point(246, 132);
            cmbRole.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new System.Drawing.Size(427, 33);
            cmbRole.TabIndex = 6;
            // 
            // lblDevice
            // 
            lblDevice.Location = new System.Drawing.Point(718, 138);
            lblDevice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblDevice.Name = "lblDevice";
            lblDevice.Size = new System.Drawing.Size(221, 42);
            lblDevice.TabIndex = 7;
            lblDevice.Text = "Device";
            // 
            // cmbDevice
            // 
            cmbDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbDevice.FormattingEnabled = true;
            cmbDevice.Location = new System.Drawing.Point(960, 132);
            cmbDevice.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbDevice.Name = "cmbDevice";
            cmbDevice.Size = new System.Drawing.Size(427, 33);
            cmbDevice.TabIndex = 8;
            // 
            // lblSearch
            // 
            lblSearch.Location = new System.Drawing.Point(18, 225);
            lblSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new System.Drawing.Size(214, 42);
            lblSearch.TabIndex = 9;
            lblSearch.Text = "Search name / ID / role";
            // 
            // txtSearch
            // 
            txtSearch.Location = new System.Drawing.Point(246, 218);
            txtSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(384, 31);
            txtSearch.TabIndex = 10;
            // 
            // btnSearch
            // 
            btnSearch.Location = new System.Drawing.Point(638, 213);
            btnSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(126, 57);
            btnSearch.TabIndex = 11;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += BtnSearch_Click;
            // 
            // btnViewLog
            // 
            btnViewLog.Location = new System.Drawing.Point(778, 213);
            btnViewLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnViewLog.Name = "btnViewLog";
            btnViewLog.Size = new System.Drawing.Size(143, 57);
            btnViewLog.TabIndex = 12;
            btnViewLog.Text = "View all calls";
            btnViewLog.UseVisualStyleBackColor = true;
            btnViewLog.Click += BtnViewLog_Click;
            // 
            // btnPrevious
            // 
            btnPrevious.Location = new System.Drawing.Point(935, 213);
            btnPrevious.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new System.Drawing.Size(129, 57);
            btnPrevious.TabIndex = 13;
            btnPrevious.Text = "< Previous";
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += BtnPrevious_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new System.Drawing.Point(1078, 213);
            btnNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnNext.Name = "btnNext";
            btnNext.Size = new System.Drawing.Size(129, 57);
            btnNext.TabIndex = 14;
            btnNext.Text = "Next >";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += BtnNext_Click;
            // 
            // btnGroupCalls
            // 
            btnGroupCalls.Location = new System.Drawing.Point(1275, 213);
            btnGroupCalls.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnGroupCalls.Name = "btnGroupCalls";
            btnGroupCalls.Size = new System.Drawing.Size(357, 57);
            btnGroupCalls.TabIndex = 15;
            btnGroupCalls.Text = "Refresh groups";
            btnGroupCalls.UseVisualStyleBackColor = true;
            btnGroupCalls.Click += BtnGroupCalls_Click;
            // 
            // lblLogHeading
            // 
            lblLogHeading.Location = new System.Drawing.Point(18, 287);
            lblLogHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblLogHeading.Name = "lblLogHeading";
            lblLogHeading.Size = new System.Drawing.Size(1200, 40);
            lblLogHeading.TabIndex = 16;
            lblLogHeading.Text = "Virtual call log - select a completed call to return it";
            // 
            // lblGroupsHeading
            // 
            lblGroupsHeading.Location = new System.Drawing.Point(1275, 287);
            lblGroupsHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblGroupsHeading.Name = "lblGroupsHeading";
            lblGroupsHeading.Size = new System.Drawing.Size(357, 40);
            lblGroupsHeading.TabIndex = 17;
            lblGroupsHeading.Text = "Calls grouped by caller role";
            // 
            // lstCalls
            // 
            lstCalls.FormattingEnabled = true;
            lstCalls.HorizontalScrollbar = true;
            lstCalls.IntegralHeight = false;
            lstCalls.Location = new System.Drawing.Point(13, 332);
            lstCalls.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            lstCalls.Name = "lstCalls";
            lstCalls.Size = new System.Drawing.Size(1198, 431);
            lstCalls.TabIndex = 18;
            lstCalls.SelectedIndexChanged += lstCalls_SelectedIndexChanged;
            // 
            // treeGroups
            // 
            treeGroups.Location = new System.Drawing.Point(1270, 332);
            treeGroups.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            treeGroups.Name = "treeGroups";
            treeGroups.Size = new System.Drawing.Size(355, 431);
            treeGroups.TabIndex = 19;
            // 
            // btnMakeCall
            // 
            btnMakeCall.Location = new System.Drawing.Point(13, 773);
            btnMakeCall.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnMakeCall.Name = "btnMakeCall";
            btnMakeCall.Size = new System.Drawing.Size(186, 67);
            btnMakeCall.TabIndex = 20;
            btnMakeCall.Text = "Make call";
            btnMakeCall.UseVisualStyleBackColor = true;
            btnMakeCall.Click += BtnMakeCall_Click;
            // 
            // btnSpeak
            // 
            btnSpeak.Location = new System.Drawing.Point(215, 773);
            btnSpeak.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnSpeak.Name = "btnSpeak";
            btnSpeak.Size = new System.Drawing.Size(136, 67);
            btnSpeak.TabIndex = 21;
            btnSpeak.Text = "Speak";
            btnSpeak.UseVisualStyleBackColor = true;
            btnSpeak.Click += BtnSpeak_Click;
            // 
            // btnHold
            // 
            btnHold.Location = new System.Drawing.Point(368, 773);
            btnHold.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnHold.Name = "btnHold";
            btnHold.Size = new System.Drawing.Size(136, 67);
            btnHold.TabIndex = 22;
            btnHold.Text = "Hold";
            btnHold.UseVisualStyleBackColor = true;
            btnHold.Click += BtnHold_Click;
            // 
            // btnResume
            // 
            btnResume.Location = new System.Drawing.Point(521, 773);
            btnResume.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnResume.Name = "btnResume";
            btnResume.Size = new System.Drawing.Size(136, 67);
            btnResume.TabIndex = 23;
            btnResume.Text = "Resume";
            btnResume.UseVisualStyleBackColor = true;
            btnResume.Click += BtnResume_Click;
            // 
            // btnHangUp
            // 
            btnHangUp.Location = new System.Drawing.Point(674, 773);
            btnHangUp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnHangUp.Name = "btnHangUp";
            btnHangUp.Size = new System.Drawing.Size(157, 67);
            btnHangUp.TabIndex = 24;
            btnHangUp.Text = "Hang up";
            btnHangUp.UseVisualStyleBackColor = true;
            btnHangUp.Click += BtnHangUp_Click;
            // 
            // btnReturnCall
            // 
            btnReturnCall.Location = new System.Drawing.Point(848, 773);
            btnReturnCall.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnReturnCall.Name = "btnReturnCall";
            btnReturnCall.Size = new System.Drawing.Size(364, 67);
            btnReturnCall.TabIndex = 25;
            btnReturnCall.Text = "Manager: return selected";
            btnReturnCall.UseVisualStyleBackColor = true;
            btnReturnCall.Click += BtnReturnCall_Click;
            // 
            // lblStatus
            // 
            lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lblStatus.Location = new System.Drawing.Point(13, 860);
            lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new System.Windows.Forms.Padding(11, 13, 11, 13);
            lblStatus.Size = new System.Drawing.Size(1199, 90);
            lblStatus.TabIndex = 26;
            lblStatus.Text = "Ready. Enter participants and make a call.";
            // 
            // lblSimulation
            // 
            lblSimulation.ForeColor = System.Drawing.Color.DimGray;
            lblSimulation.Location = new System.Drawing.Point(29, 1083);
            lblSimulation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblSimulation.Name = "lblSimulation";
            lblSimulation.Size = new System.Drawing.Size(1614, 50);
            lblSimulation.TabIndex = 27;
            lblSimulation.Text = "Simulation: talk time pauses on hold. Smartphone and telephone choices do not connect to a real phone network.";
            // 
            // timerTick
            // 
            timerTick.Interval = 250;
            timerTick.Tick += TimerTick_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1677, 1050);
            Controls.Add(lblTitle);
            Controls.Add(lblCallerName);
            Controls.Add(txtCallerName);
            Controls.Add(lblReceiver);
            Controls.Add(txtReceiver);
            Controls.Add(lblRole);
            Controls.Add(cmbRole);
            Controls.Add(lblDevice);
            Controls.Add(cmbDevice);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(btnViewLog);
            Controls.Add(btnPrevious);
            Controls.Add(btnNext);
            Controls.Add(btnGroupCalls);
            Controls.Add(lblLogHeading);
            Controls.Add(lblGroupsHeading);
            Controls.Add(lstCalls);
            Controls.Add(treeGroups);
            Controls.Add(btnMakeCall);
            Controls.Add(btnSpeak);
            Controls.Add(btnHold);
            Controls.Add(btnResume);
            Controls.Add(btnHangUp);
            Controls.Add(btnReturnCall);
            Controls.Add(lblStatus);
            Controls.Add(lblSimulation);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MinimumSize = new System.Drawing.Size(1691, 1078);
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Assignment 2 - Student Call Center";
            FormClosing += ClosingDashboard;
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCallerName;
        private System.Windows.Forms.TextBox txtCallerName;
        private System.Windows.Forms.Label lblReceiver;
        private System.Windows.Forms.TextBox txtReceiver;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.ComboBox cmbDevice;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnViewLog;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnGroupCalls;
        private System.Windows.Forms.Label lblLogHeading;
        private System.Windows.Forms.Label lblGroupsHeading;
        private System.Windows.Forms.ListBox lstCalls;
        private System.Windows.Forms.TreeView treeGroups;
        private System.Windows.Forms.Button btnMakeCall;
        private System.Windows.Forms.Button btnSpeak;
        private System.Windows.Forms.Button btnHold;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnHangUp;
        private System.Windows.Forms.Button btnReturnCall;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSimulation;
        private System.Windows.Forms.Timer timerTick;
    }
}

