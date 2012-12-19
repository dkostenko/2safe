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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreferences));
            this.tabControlPreferences = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.groupBoxGeneral = new System.Windows.Forms.GroupBox();
            this.checkBoxLanSync = new System.Windows.Forms.CheckBox();
            this.checkBoxStart = new System.Windows.Forms.CheckBox();
            this.checkBoxNotifications = new System.Windows.Forms.CheckBox();
            this.tabPageAccount = new System.Windows.Forms.TabPage();
            this.buttonLogOut = new System.Windows.Forms.Button();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.labelLoggedInAs = new System.Windows.Forms.Label();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.groupBoxLocation = new System.Windows.Forms.GroupBox();
            this.buttonMoveLocation = new System.Windows.Forms.Button();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.groupBoxLanguage = new System.Windows.Forms.GroupBox();
            this.comboBoxLanguages = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.tabControlPreferences.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.groupBoxGeneral.SuspendLayout();
            this.tabPageAccount.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            this.groupBoxLocation.SuspendLayout();
            this.groupBoxLanguage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlPreferences
            // 
            this.tabControlPreferences.Controls.Add(this.tabPageGeneral);
            this.tabControlPreferences.Controls.Add(this.tabPageAccount);
            this.tabControlPreferences.Controls.Add(this.tabPageAdvanced);
            resources.ApplyResources(this.tabControlPreferences, "tabControlPreferences");
            this.tabControlPreferences.Name = "tabControlPreferences";
            this.tabControlPreferences.SelectedIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageGeneral.Controls.Add(this.groupBoxGeneral);
            resources.ApplyResources(this.tabPageGeneral, "tabPageGeneral");
            this.tabPageGeneral.Name = "tabPageGeneral";
            // 
            // groupBoxGeneral
            // 
            this.groupBoxGeneral.Controls.Add(this.checkBoxLanSync);
            this.groupBoxGeneral.Controls.Add(this.checkBoxStart);
            this.groupBoxGeneral.Controls.Add(this.checkBoxNotifications);
            resources.ApplyResources(this.groupBoxGeneral, "groupBoxGeneral");
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.TabStop = false;
            // 
            // checkBoxLanSync
            // 
            resources.ApplyResources(this.checkBoxLanSync, "checkBoxLanSync");
            this.checkBoxLanSync.Name = "checkBoxLanSync";
            this.checkBoxLanSync.UseVisualStyleBackColor = true;
            this.checkBoxLanSync.CheckedChanged += new System.EventHandler(this.checkBoxLanSync_CheckedChanged);
            // 
            // checkBoxStart
            // 
            resources.ApplyResources(this.checkBoxStart, "checkBoxStart");
            this.checkBoxStart.Name = "checkBoxStart";
            this.checkBoxStart.UseVisualStyleBackColor = true;
            this.checkBoxStart.CheckedChanged += new System.EventHandler(this.checkBoxStart_CheckedChanged);
            // 
            // checkBoxNotifications
            // 
            resources.ApplyResources(this.checkBoxNotifications, "checkBoxNotifications");
            this.checkBoxNotifications.Name = "checkBoxNotifications";
            this.checkBoxNotifications.UseVisualStyleBackColor = true;
            this.checkBoxNotifications.CheckedChanged += new System.EventHandler(this.checkBoxNotifications_CheckedChanged);
            // 
            // tabPageAccount
            // 
            this.tabPageAccount.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageAccount.Controls.Add(this.buttonLogOut);
            this.tabPageAccount.Controls.Add(this.textBoxUserName);
            this.tabPageAccount.Controls.Add(this.labelLoggedInAs);
            resources.ApplyResources(this.tabPageAccount, "tabPageAccount");
            this.tabPageAccount.Name = "tabPageAccount";
            // 
            // buttonLogOut
            // 
            resources.ApplyResources(this.buttonLogOut, "buttonLogOut");
            this.buttonLogOut.Name = "buttonLogOut";
            this.buttonLogOut.UseVisualStyleBackColor = true;
            this.buttonLogOut.Click += new System.EventHandler(this.buttonLogOut_Click);
            // 
            // textBoxUserName
            // 
            resources.ApplyResources(this.textBoxUserName, "textBoxUserName");
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.ReadOnly = true;
            // 
            // labelLoggedInAs
            // 
            resources.ApplyResources(this.labelLoggedInAs, "labelLoggedInAs");
            this.labelLoggedInAs.Name = "labelLoggedInAs";
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageAdvanced.Controls.Add(this.groupBoxLocation);
            this.tabPageAdvanced.Controls.Add(this.groupBoxLanguage);
            resources.ApplyResources(this.tabPageAdvanced, "tabPageAdvanced");
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            // 
            // groupBoxLocation
            // 
            this.groupBoxLocation.Controls.Add(this.buttonMoveLocation);
            this.groupBoxLocation.Controls.Add(this.textBoxLocation);
            resources.ApplyResources(this.groupBoxLocation, "groupBoxLocation");
            this.groupBoxLocation.Name = "groupBoxLocation";
            this.groupBoxLocation.TabStop = false;
            // 
            // buttonMoveLocation
            // 
            resources.ApplyResources(this.buttonMoveLocation, "buttonMoveLocation");
            this.buttonMoveLocation.Name = "buttonMoveLocation";
            this.buttonMoveLocation.UseVisualStyleBackColor = true;
            this.buttonMoveLocation.Click += new System.EventHandler(this.buttonMoveLocation_Click);
            // 
            // textBoxLocation
            // 
            resources.ApplyResources(this.textBoxLocation, "textBoxLocation");
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.ReadOnly = true;
            // 
            // groupBoxLanguage
            // 
            this.groupBoxLanguage.Controls.Add(this.comboBoxLanguages);
            resources.ApplyResources(this.groupBoxLanguage, "groupBoxLanguage");
            this.groupBoxLanguage.Name = "groupBoxLanguage";
            this.groupBoxLanguage.TabStop = false;
            // 
            // comboBoxLanguages
            // 
            this.comboBoxLanguages.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxLanguages, "comboBoxLanguages");
            this.comboBoxLanguages.Name = "comboBoxLanguages";
            this.comboBoxLanguages.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguages_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonApply
            // 
            resources.ApplyResources(this.buttonApply, "buttonApply");
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // FormPreferences
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControlPreferences);
            this.Name = "FormPreferences";
            this.Load += new System.EventHandler(this.FormPreferences_Load);
            this.tabControlPreferences.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.groupBoxGeneral.ResumeLayout(false);
            this.groupBoxGeneral.PerformLayout();
            this.tabPageAccount.ResumeLayout(false);
            this.tabPageAccount.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.groupBoxLocation.ResumeLayout(false);
            this.groupBoxLocation.PerformLayout();
            this.groupBoxLanguage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlPreferences;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageAccount;
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
        private System.Windows.Forms.GroupBox groupBoxLocation;
        private System.Windows.Forms.Button buttonMoveLocation;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.Button buttonLogOut;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label labelLoggedInAs;
    }
}