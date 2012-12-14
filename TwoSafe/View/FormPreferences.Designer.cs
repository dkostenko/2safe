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
            resources.ApplyResources(this.tabControlPreferences, "tabControlPreferences");
            this.tabControlPreferences.Name = "tabControlPreferences";
            this.tabControlPreferences.SelectedIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.groupBoxGeneral);
            resources.ApplyResources(this.tabPageGeneral, "tabPageGeneral");
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
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
            // 
            // checkBoxStart
            // 
            resources.ApplyResources(this.checkBoxStart, "checkBoxStart");
            this.checkBoxStart.Name = "checkBoxStart";
            this.checkBoxStart.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotifications
            // 
            resources.ApplyResources(this.checkBoxNotifications, "checkBoxNotifications");
            this.checkBoxNotifications.Name = "checkBoxNotifications";
            this.checkBoxNotifications.UseVisualStyleBackColor = true;
            // 
            // tabPageAccount
            // 
            resources.ApplyResources(this.tabPageAccount, "tabPageAccount");
            this.tabPageAccount.Name = "tabPageAccount";
            this.tabPageAccount.UseVisualStyleBackColor = true;
            // 
            // tabPageBandwidth
            // 
            resources.ApplyResources(this.tabPageBandwidth, "tabPageBandwidth");
            this.tabPageBandwidth.Name = "tabPageBandwidth";
            this.tabPageBandwidth.UseVisualStyleBackColor = true;
            // 
            // tabPageProxies
            // 
            resources.ApplyResources(this.tabPageProxies, "tabPageProxies");
            this.tabPageProxies.Name = "tabPageProxies";
            this.tabPageProxies.UseVisualStyleBackColor = true;
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.groupBoxLanguage);
            resources.ApplyResources(this.tabPageAdvanced, "tabPageAdvanced");
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
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