namespace TwoSafe.View
{
    partial class FormPreferences
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlPreferences = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.groupBoxGeneral = new System.Windows.Forms.GroupBox();
            this.checkBoxLanSync = new System.Windows.Forms.CheckBox();
            this.checkBoxStart = new System.Windows.Forms.CheckBox();
            this.checkBoxNotifications = new System.Windows.Forms.CheckBox();
            this.tabPageAccount = new System.Windows.Forms.TabPage();
            this.tabPageBandwidth = new System.Windows.Forms.TabPage();
            this.tabPageProxies = new System.Windows.Forms.TabPage();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.groupBoxLanguage = new System.Windows.Forms.GroupBox();
            this.comboBoxLanguages = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.tabControlPreferences.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.groupBoxGeneral.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            this.groupBoxLanguage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlPreferences
            // 
            this.tabControlPreferences.Controls.Add(this.tabPageGeneral);
            this.tabControlPreferences.Controls.Add(this.tabPageAccount);
            this.tabControlPreferences.Controls.Add(this.tabPageBandwidth);
            this.tabControlPreferences.Controls.Add(this.tabPageProxies);
            this.tabControlPreferences.Controls.Add(this.tabPageAdvanced);
            this.tabControlPreferences.Location = new System.Drawing.Point(12, 12);
            this.tabControlPreferences.Name = "tabControlPreferences";
            this.tabControlPreferences.SelectedIndex = 0;
            this.tabControlPreferences.Size = new System.Drawing.Size(358, 330);
            this.tabControlPreferences.TabIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.groupBoxGeneral);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(350, 304);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBoxGeneral
            // 
            this.groupBoxGeneral.Controls.Add(this.checkBoxLanSync);
            this.groupBoxGeneral.Controls.Add(this.checkBoxStart);
            this.groupBoxGeneral.Controls.Add(this.checkBoxNotifications);
            this.groupBoxGeneral.Location = new System.Drawing.Point(21, 25);
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.Size = new System.Drawing.Size(302, 141);
            this.groupBoxGeneral.TabIndex = 0;
            this.groupBoxGeneral.TabStop = false;
            // 
            // checkBoxLanSync
            // 
            this.checkBoxLanSync.AutoSize = true;
            this.checkBoxLanSync.Location = new System.Drawing.Point(32, 75);
            this.checkBoxLanSync.Name = "checkBoxLanSync";
            this.checkBoxLanSync.Size = new System.Drawing.Size(110, 17);
            this.checkBoxLanSync.TabIndex = 2;
            this.checkBoxLanSync.Text = "Enable LAN Sync";
            this.checkBoxLanSync.UseVisualStyleBackColor = true;
            // 
            // checkBoxStart
            // 
            this.checkBoxStart.AutoSize = true;
            this.checkBoxStart.Location = new System.Drawing.Point(32, 52);
            this.checkBoxStart.Name = "checkBoxStart";
            this.checkBoxStart.Size = new System.Drawing.Size(162, 17);
            this.checkBoxStart.TabIndex = 1;
            this.checkBoxStart.Text = "Start 2safe on system startup";
            this.checkBoxStart.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotifications
            // 
            this.checkBoxNotifications.AutoSize = true;
            this.checkBoxNotifications.Location = new System.Drawing.Point(32, 29);
            this.checkBoxNotifications.Name = "checkBoxNotifications";
            this.checkBoxNotifications.Size = new System.Drawing.Size(153, 17);
            this.checkBoxNotifications.TabIndex = 0;
            this.checkBoxNotifications.Text = "Show desktop notifications";
            this.checkBoxNotifications.UseVisualStyleBackColor = true;
            // 
            // tabPageAccount
            // 
            this.tabPageAccount.Location = new System.Drawing.Point(4, 22);
            this.tabPageAccount.Name = "tabPageAccount";
            this.tabPageAccount.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAccount.Size = new System.Drawing.Size(350, 304);
            this.tabPageAccount.TabIndex = 1;
            this.tabPageAccount.Text = "Account";
            this.tabPageAccount.UseVisualStyleBackColor = true;
            // 
            // tabPageBandwidth
            // 
            this.tabPageBandwidth.Location = new System.Drawing.Point(4, 22);
            this.tabPageBandwidth.Name = "tabPageBandwidth";
            this.tabPageBandwidth.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBandwidth.Size = new System.Drawing.Size(350, 304);
            this.tabPageBandwidth.TabIndex = 2;
            this.tabPageBandwidth.Text = "Bandwidth";
            this.tabPageBandwidth.UseVisualStyleBackColor = true;
            // 
            // tabPageProxies
            // 
            this.tabPageProxies.Location = new System.Drawing.Point(4, 22);
            this.tabPageProxies.Name = "tabPageProxies";
            this.tabPageProxies.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProxies.Size = new System.Drawing.Size(350, 304);
            this.tabPageProxies.TabIndex = 3;
            this.tabPageProxies.Text = "Proxies";
            this.tabPageProxies.UseVisualStyleBackColor = true;
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.groupBoxLanguage);
            this.tabPageAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdvanced.Size = new System.Drawing.Size(350, 304);
            this.tabPageAdvanced.TabIndex = 4;
            this.tabPageAdvanced.Text = "Advanced";
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // groupBoxLanguage
            // 
            this.groupBoxLanguage.Controls.Add(this.comboBoxLanguages);
            this.groupBoxLanguage.Location = new System.Drawing.Point(15, 199);
            this.groupBoxLanguage.Name = "groupBoxLanguage";
            this.groupBoxLanguage.Size = new System.Drawing.Size(320, 89);
            this.groupBoxLanguage.TabIndex = 0;
            this.groupBoxLanguage.TabStop = false;
            this.groupBoxLanguage.Text = "Language";
            // 
            // comboBoxLanguages
            // 
            this.comboBoxLanguages.FormattingEnabled = true;
            this.comboBoxLanguages.Location = new System.Drawing.Point(34, 38);
            this.comboBoxLanguages.Name = "comboBoxLanguages";
            this.comboBoxLanguages.Size = new System.Drawing.Size(251, 21);
            this.comboBoxLanguages.TabIndex = 0;
            this.comboBoxLanguages.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguages_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(129, 348);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(210, 348);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(291, 348);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 2;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            // 
            // FormPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 387);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControlPreferences);
            this.Name = "FormPreferences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "2safe Preferences";
            this.Load += new System.EventHandler(this.FormPreferences_Load);
            this.tabControlPreferences.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.groupBoxGeneral.ResumeLayout(false);
            this.groupBoxGeneral.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.groupBoxLanguage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlPreferences;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageAccount;
        private System.Windows.Forms.TabPage tabPageBandwidth;
        private System.Windows.Forms.TabPage tabPageProxies;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.GroupBox groupBoxGeneral;
        private System.Windows.Forms.CheckBox checkBoxLanSync;
        private System.Windows.Forms.CheckBox checkBoxStart;
        private System.Windows.Forms.CheckBox checkBoxNotifications;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.GroupBox groupBoxLanguage;
        private System.Windows.Forms.ComboBox comboBoxLanguages;
    }
}