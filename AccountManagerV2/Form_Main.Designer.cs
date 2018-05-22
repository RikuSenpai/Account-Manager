namespace AccountManagerV2
{
    partial class Form_Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.ListView_Accounts = new System.Windows.Forms.ListView();
            this.NameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PasswordHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StatusHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CooldownStatusHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Panel_AccountManagement = new System.Windows.Forms.Panel();
            this.Button_ClearData = new System.Windows.Forms.Button();
            this.ComboBox_Status = new System.Windows.Forms.ComboBox();
            this.Button_RemoveAccount = new System.Windows.Forms.Button();
            this.Label_DayTime = new System.Windows.Forms.Label();
            this.TrackBar_DayTime = new System.Windows.Forms.TrackBar();
            this.MonthCalendar_CooldownExpireDate = new System.Windows.Forms.MonthCalendar();
            this.Label_AccountPassword = new System.Windows.Forms.Label();
            this.Button_AddAccount = new System.Windows.Forms.Button();
            this.Label_AccountName = new System.Windows.Forms.Label();
            this.TextBox_AccountPassword = new System.Windows.Forms.TextBox();
            this.TextBox_AccountName = new System.Windows.Forms.TextBox();
            this.Panel_AccountManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_DayTime)).BeginInit();
            this.SuspendLayout();
            // 
            // ListView_Accounts
            // 
            this.ListView_Accounts.BackColor = System.Drawing.SystemColors.Window;
            this.ListView_Accounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameHeader,
            this.PasswordHeader,
            this.StatusHeader,
            this.CooldownStatusHeader});
            this.ListView_Accounts.Location = new System.Drawing.Point(12, 12);
            this.ListView_Accounts.MultiSelect = false;
            this.ListView_Accounts.Name = "ListView_Accounts";
            this.ListView_Accounts.Size = new System.Drawing.Size(446, 390);
            this.ListView_Accounts.TabIndex = 0;
            this.ListView_Accounts.UseCompatibleStateImageBehavior = false;
            this.ListView_Accounts.SelectedIndexChanged += new System.EventHandler(this.ListView_Accounts_SelectedIndexChanged);
            // 
            // NameHeader
            // 
            this.NameHeader.Text = "NAME";
            // 
            // PasswordHeader
            // 
            this.PasswordHeader.Text = "PASSWORD";
            this.PasswordHeader.Width = 120;
            // 
            // StatusHeader
            // 
            this.StatusHeader.Text = "STATUS";
            this.StatusHeader.Width = 90;
            // 
            // CooldownStatusHeader
            // 
            this.CooldownStatusHeader.Text = "COOLDOWN EXPIRES";
            this.CooldownStatusHeader.Width = 170;
            // 
            // Panel_AccountManagement
            // 
            this.Panel_AccountManagement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_AccountManagement.Controls.Add(this.Button_ClearData);
            this.Panel_AccountManagement.Controls.Add(this.ComboBox_Status);
            this.Panel_AccountManagement.Controls.Add(this.Button_RemoveAccount);
            this.Panel_AccountManagement.Controls.Add(this.Label_DayTime);
            this.Panel_AccountManagement.Controls.Add(this.TrackBar_DayTime);
            this.Panel_AccountManagement.Controls.Add(this.MonthCalendar_CooldownExpireDate);
            this.Panel_AccountManagement.Controls.Add(this.Label_AccountPassword);
            this.Panel_AccountManagement.Controls.Add(this.Button_AddAccount);
            this.Panel_AccountManagement.Controls.Add(this.Label_AccountName);
            this.Panel_AccountManagement.Controls.Add(this.TextBox_AccountPassword);
            this.Panel_AccountManagement.Controls.Add(this.TextBox_AccountName);
            this.Panel_AccountManagement.Location = new System.Drawing.Point(464, 12);
            this.Panel_AccountManagement.Name = "Panel_AccountManagement";
            this.Panel_AccountManagement.Size = new System.Drawing.Size(199, 390);
            this.Panel_AccountManagement.TabIndex = 1;
            // 
            // Button_ClearData
            // 
            this.Button_ClearData.Location = new System.Drawing.Point(9, 355);
            this.Button_ClearData.Name = "Button_ClearData";
            this.Button_ClearData.Size = new System.Drawing.Size(178, 23);
            this.Button_ClearData.TabIndex = 12;
            this.Button_ClearData.Text = "Clear Data";
            this.Button_ClearData.UseVisualStyleBackColor = true;
            this.Button_ClearData.Click += new System.EventHandler(this.Button_ClearData_Click);
            // 
            // ComboBox_Status
            // 
            this.ComboBox_Status.FormattingEnabled = true;
            this.ComboBox_Status.Items.AddRange(new object[] {
            "Ready",
            "Cooldown",
            "Banned",
            "Unknown"});
            this.ComboBox_Status.Location = new System.Drawing.Point(12, 270);
            this.ComboBox_Status.Name = "ComboBox_Status";
            this.ComboBox_Status.Size = new System.Drawing.Size(175, 21);
            this.ComboBox_Status.TabIndex = 11;
            this.ComboBox_Status.Text = "Status";
            // 
            // Button_RemoveAccount
            // 
            this.Button_RemoveAccount.Location = new System.Drawing.Point(9, 326);
            this.Button_RemoveAccount.Name = "Button_RemoveAccount";
            this.Button_RemoveAccount.Size = new System.Drawing.Size(178, 23);
            this.Button_RemoveAccount.TabIndex = 10;
            this.Button_RemoveAccount.Text = "Remove Account";
            this.Button_RemoveAccount.UseVisualStyleBackColor = true;
            this.Button_RemoveAccount.Click += new System.EventHandler(this.Button_RemoveAccount_Click);
            // 
            // Label_DayTime
            // 
            this.Label_DayTime.AutoSize = true;
            this.Label_DayTime.Location = new System.Drawing.Point(9, 246);
            this.Label_DayTime.Name = "Label_DayTime";
            this.Label_DayTime.Size = new System.Drawing.Size(167, 13);
            this.Label_DayTime.TabIndex = 9;
            this.Label_DayTime.Text = "Expire Date: 01.01.1998 00:00:00";
            // 
            // TrackBar_DayTime
            // 
            this.TrackBar_DayTime.Location = new System.Drawing.Point(9, 216);
            this.TrackBar_DayTime.Maximum = 23;
            this.TrackBar_DayTime.Name = "TrackBar_DayTime";
            this.TrackBar_DayTime.Size = new System.Drawing.Size(178, 45);
            this.TrackBar_DayTime.TabIndex = 8;
            this.TrackBar_DayTime.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrackBar_DayTime.Scroll += new System.EventHandler(this.TrackBar_DayTime_Scroll);
            // 
            // MonthCalendar_CooldownExpireDate
            // 
            this.MonthCalendar_CooldownExpireDate.Location = new System.Drawing.Point(9, 58);
            this.MonthCalendar_CooldownExpireDate.MaxSelectionCount = 1;
            this.MonthCalendar_CooldownExpireDate.Name = "MonthCalendar_CooldownExpireDate";
            this.MonthCalendar_CooldownExpireDate.ShowToday = false;
            this.MonthCalendar_CooldownExpireDate.TabIndex = 7;
            this.MonthCalendar_CooldownExpireDate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.MonthCalendar_CooldownExpireDate_DateChanged);
            // 
            // Label_AccountPassword
            // 
            this.Label_AccountPassword.AutoSize = true;
            this.Label_AccountPassword.Location = new System.Drawing.Point(9, 36);
            this.Label_AccountPassword.Name = "Label_AccountPassword";
            this.Label_AccountPassword.Size = new System.Drawing.Size(56, 13);
            this.Label_AccountPassword.TabIndex = 6;
            this.Label_AccountPassword.Text = "Password:";
            // 
            // Button_AddAccount
            // 
            this.Button_AddAccount.Location = new System.Drawing.Point(9, 297);
            this.Button_AddAccount.Name = "Button_AddAccount";
            this.Button_AddAccount.Size = new System.Drawing.Size(178, 23);
            this.Button_AddAccount.TabIndex = 2;
            this.Button_AddAccount.Text = "Add/Update Account";
            this.Button_AddAccount.UseVisualStyleBackColor = true;
            this.Button_AddAccount.Click += new System.EventHandler(this.Button_AddAccount_Click);
            // 
            // Label_AccountName
            // 
            this.Label_AccountName.AutoSize = true;
            this.Label_AccountName.Location = new System.Drawing.Point(9, 10);
            this.Label_AccountName.Name = "Label_AccountName";
            this.Label_AccountName.Size = new System.Drawing.Size(38, 13);
            this.Label_AccountName.TabIndex = 4;
            this.Label_AccountName.Text = "Name:";
            // 
            // TextBox_AccountPassword
            // 
            this.TextBox_AccountPassword.Location = new System.Drawing.Point(67, 33);
            this.TextBox_AccountPassword.Name = "TextBox_AccountPassword";
            this.TextBox_AccountPassword.Size = new System.Drawing.Size(120, 20);
            this.TextBox_AccountPassword.TabIndex = 5;
            // 
            // TextBox_AccountName
            // 
            this.TextBox_AccountName.Location = new System.Drawing.Point(67, 7);
            this.TextBox_AccountName.Name = "TextBox_AccountName";
            this.TextBox_AccountName.Size = new System.Drawing.Size(120, 20);
            this.TextBox_AccountName.TabIndex = 4;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 416);
            this.Controls.Add(this.Panel_AccountManagement);
            this.Controls.Add(this.ListView_Accounts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_Main";
            this.Text = "AccountManagerV2";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.Panel_AccountManagement.ResumeLayout(false);
            this.Panel_AccountManagement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar_DayTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ListView_Accounts;
        private System.Windows.Forms.ColumnHeader NameHeader;
        private System.Windows.Forms.ColumnHeader PasswordHeader;
        private System.Windows.Forms.ColumnHeader StatusHeader;
        private System.Windows.Forms.ColumnHeader CooldownStatusHeader;
        private System.Windows.Forms.Panel Panel_AccountManagement;
        private System.Windows.Forms.Label Label_AccountPassword;
        private System.Windows.Forms.Button Button_AddAccount;
        private System.Windows.Forms.Label Label_AccountName;
        private System.Windows.Forms.TextBox TextBox_AccountPassword;
        private System.Windows.Forms.TextBox TextBox_AccountName;
        private System.Windows.Forms.MonthCalendar MonthCalendar_CooldownExpireDate;
        private System.Windows.Forms.Label Label_DayTime;
        private System.Windows.Forms.TrackBar TrackBar_DayTime;
        private System.Windows.Forms.ComboBox ComboBox_Status;
        private System.Windows.Forms.Button Button_RemoveAccount;
        private System.Windows.Forms.Button Button_ClearData;
    }
}

