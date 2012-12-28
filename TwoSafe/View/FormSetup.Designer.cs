namespace TwoSafe.View
{
    partial class FormSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetup));
            this.radioButtonHasAccount = new System.Windows.Forms.RadioButton();
            this.radioButtonDoesNotHaveAccount = new System.Windows.Forms.RadioButton();
            this.panelEnter = new System.Windows.Forms.Panel();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.buttonRefreshCaptchaLogin = new System.Windows.Forms.Button();
            this.labelCaptchaLogin = new System.Windows.Forms.Label();
            this.textBoxCaptchaLogin = new System.Windows.Forms.TextBox();
            this.pictureBoxCaptchaLogin = new System.Windows.Forms.PictureBox();
            this.labelErrorMessageLogin = new System.Windows.Forms.Label();
            this.labelPasswordLogin = new System.Windows.Forms.Label();
            this.labelAccountLogin = new System.Windows.Forms.Label();
            this.textBoxPasswordLogin = new System.Windows.Forms.TextBox();
            this.textBoxAccountLogin = new System.Windows.Forms.TextBox();
            this.labelLogin = new System.Windows.Forms.Label();
            this.panelCreateAccount = new System.Windows.Forms.Panel();
            this.textBoxRepeatPasswordSignup = new System.Windows.Forms.TextBox();
            this.labelRepeatPasswordSignup = new System.Windows.Forms.Label();
            this.buttonRefreshCaptchaSignup = new System.Windows.Forms.Button();
            this.labelErrorMessageSignup = new System.Windows.Forms.Label();
            this.labelCaptchaSignup = new System.Windows.Forms.Label();
            this.textBoxCaptchaSignup = new System.Windows.Forms.TextBox();
            this.labelSignUp = new System.Windows.Forms.Label();
            this.labelEmailSignup = new System.Windows.Forms.Label();
            this.pictureBoxCaptchaSignup = new System.Windows.Forms.PictureBox();
            this.textBoxEmailSignup = new System.Windows.Forms.TextBox();
            this.textBoxPasswordSignup = new System.Windows.Forms.TextBox();
            this.labelPasswordSignup = new System.Windows.Forms.Label();
            this.textBoxAccountSignup = new System.Windows.Forms.TextBox();
            this.labelAccountSignup = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.panelFullSetup = new System.Windows.Forms.Panel();
            this.radioButtonAdvanced = new System.Windows.Forms.RadioButton();
            this.radioButtonTypical = new System.Windows.Forms.RadioButton();
            this.labelFulSetup = new System.Windows.Forms.Label();
            this.panelExit = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loginBW = new System.ComponentModel.BackgroundWorker();
            this.getCaptchaBW = new System.ComponentModel.BackgroundWorker();
            this.signUpBW = new System.ComponentModel.BackgroundWorker();
            this.panelEnter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptchaLogin)).BeginInit();
            this.panelCreateAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptchaSignup)).BeginInit();
            this.panelFullSetup.SuspendLayout();
            this.panelExit.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButtonHasAccount
            // 
            resources.ApplyResources(this.radioButtonHasAccount, "radioButtonHasAccount");
            this.radioButtonHasAccount.Name = "radioButtonHasAccount";
            this.radioButtonHasAccount.TabStop = true;
            this.radioButtonHasAccount.UseVisualStyleBackColor = true;
            // 
            // radioButtonDoesNotHaveAccount
            // 
            resources.ApplyResources(this.radioButtonDoesNotHaveAccount, "radioButtonDoesNotHaveAccount");
            this.radioButtonDoesNotHaveAccount.Name = "radioButtonDoesNotHaveAccount";
            this.radioButtonDoesNotHaveAccount.TabStop = true;
            this.radioButtonDoesNotHaveAccount.UseVisualStyleBackColor = true;
            // 
            // panelEnter
            // 
            this.panelEnter.BackColor = System.Drawing.Color.White;
            this.panelEnter.Controls.Add(this.pictureBoxLogo);
            this.panelEnter.Controls.Add(this.radioButtonDoesNotHaveAccount);
            this.panelEnter.Controls.Add(this.radioButtonHasAccount);
            resources.ApplyResources(this.panelEnter, "panelEnter");
            this.panelEnter.Name = "panelEnter";
            // 
            // pictureBoxLogo
            // 
            resources.ApplyResources(this.pictureBoxLogo, "pictureBoxLogo");
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.TabStop = false;
            // 
            // panelLogin
            // 
            this.panelLogin.BackColor = System.Drawing.Color.White;
            this.panelLogin.Controls.Add(this.buttonRefreshCaptchaLogin);
            this.panelLogin.Controls.Add(this.labelCaptchaLogin);
            this.panelLogin.Controls.Add(this.textBoxCaptchaLogin);
            this.panelLogin.Controls.Add(this.pictureBoxCaptchaLogin);
            this.panelLogin.Controls.Add(this.labelErrorMessageLogin);
            this.panelLogin.Controls.Add(this.labelPasswordLogin);
            this.panelLogin.Controls.Add(this.labelAccountLogin);
            this.panelLogin.Controls.Add(this.textBoxPasswordLogin);
            this.panelLogin.Controls.Add(this.textBoxAccountLogin);
            this.panelLogin.Controls.Add(this.labelLogin);
            resources.ApplyResources(this.panelLogin, "panelLogin");
            this.panelLogin.Name = "panelLogin";
            // 
            // buttonRefreshCaptchaLogin
            // 
            this.buttonRefreshCaptchaLogin.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonRefreshCaptchaLogin, "buttonRefreshCaptchaLogin");
            this.buttonRefreshCaptchaLogin.ForeColor = System.Drawing.Color.Transparent;
            this.buttonRefreshCaptchaLogin.Name = "buttonRefreshCaptchaLogin";
            this.buttonRefreshCaptchaLogin.TabStop = false;
            this.buttonRefreshCaptchaLogin.UseVisualStyleBackColor = false;
            this.buttonRefreshCaptchaLogin.Click += new System.EventHandler(this.buttonRefreshCaptcha_Click);
            // 
            // labelCaptchaLogin
            // 
            resources.ApplyResources(this.labelCaptchaLogin, "labelCaptchaLogin");
            this.labelCaptchaLogin.Name = "labelCaptchaLogin";
            // 
            // textBoxCaptchaLogin
            // 
            this.textBoxCaptchaLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxCaptchaLogin, "textBoxCaptchaLogin");
            this.textBoxCaptchaLogin.Name = "textBoxCaptchaLogin";
            this.textBoxCaptchaLogin.TextChanged += new System.EventHandler(this.textBoxesLogin_TextChanged);
            // 
            // pictureBoxCaptchaLogin
            // 
            resources.ApplyResources(this.pictureBoxCaptchaLogin, "pictureBoxCaptchaLogin");
            this.pictureBoxCaptchaLogin.Name = "pictureBoxCaptchaLogin";
            this.pictureBoxCaptchaLogin.TabStop = false;
            // 
            // labelErrorMessageLogin
            // 
            resources.ApplyResources(this.labelErrorMessageLogin, "labelErrorMessageLogin");
            this.labelErrorMessageLogin.Name = "labelErrorMessageLogin";
            // 
            // labelPasswordLogin
            // 
            resources.ApplyResources(this.labelPasswordLogin, "labelPasswordLogin");
            this.labelPasswordLogin.Name = "labelPasswordLogin";
            // 
            // labelAccountLogin
            // 
            resources.ApplyResources(this.labelAccountLogin, "labelAccountLogin");
            this.labelAccountLogin.Name = "labelAccountLogin";
            // 
            // textBoxPasswordLogin
            // 
            this.textBoxPasswordLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxPasswordLogin, "textBoxPasswordLogin");
            this.textBoxPasswordLogin.Name = "textBoxPasswordLogin";
            this.textBoxPasswordLogin.TextChanged += new System.EventHandler(this.textBoxesLogin_TextChanged);
            // 
            // textBoxAccountLogin
            // 
            this.textBoxAccountLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxAccountLogin, "textBoxAccountLogin");
            this.textBoxAccountLogin.Name = "textBoxAccountLogin";
            this.textBoxAccountLogin.TextChanged += new System.EventHandler(this.textBoxesLogin_TextChanged);
            // 
            // labelLogin
            // 
            resources.ApplyResources(this.labelLogin, "labelLogin");
            this.labelLogin.Name = "labelLogin";
            // 
            // panelCreateAccount
            // 
            this.panelCreateAccount.BackColor = System.Drawing.Color.White;
            this.panelCreateAccount.Controls.Add(this.textBoxRepeatPasswordSignup);
            this.panelCreateAccount.Controls.Add(this.labelRepeatPasswordSignup);
            this.panelCreateAccount.Controls.Add(this.buttonRefreshCaptchaSignup);
            this.panelCreateAccount.Controls.Add(this.labelErrorMessageSignup);
            this.panelCreateAccount.Controls.Add(this.labelCaptchaSignup);
            this.panelCreateAccount.Controls.Add(this.textBoxCaptchaSignup);
            this.panelCreateAccount.Controls.Add(this.labelSignUp);
            this.panelCreateAccount.Controls.Add(this.labelEmailSignup);
            this.panelCreateAccount.Controls.Add(this.pictureBoxCaptchaSignup);
            this.panelCreateAccount.Controls.Add(this.textBoxEmailSignup);
            this.panelCreateAccount.Controls.Add(this.textBoxPasswordSignup);
            this.panelCreateAccount.Controls.Add(this.labelPasswordSignup);
            this.panelCreateAccount.Controls.Add(this.textBoxAccountSignup);
            this.panelCreateAccount.Controls.Add(this.labelAccountSignup);
            resources.ApplyResources(this.panelCreateAccount, "panelCreateAccount");
            this.panelCreateAccount.Name = "panelCreateAccount";
            // 
            // textBoxRepeatPasswordSignup
            // 
            this.textBoxRepeatPasswordSignup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxRepeatPasswordSignup, "textBoxRepeatPasswordSignup");
            this.textBoxRepeatPasswordSignup.Name = "textBoxRepeatPasswordSignup";
            // 
            // labelRepeatPasswordSignup
            // 
            resources.ApplyResources(this.labelRepeatPasswordSignup, "labelRepeatPasswordSignup");
            this.labelRepeatPasswordSignup.Name = "labelRepeatPasswordSignup";
            // 
            // buttonRefreshCaptchaSignup
            // 
            this.buttonRefreshCaptchaSignup.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonRefreshCaptchaSignup, "buttonRefreshCaptchaSignup");
            this.buttonRefreshCaptchaSignup.ForeColor = System.Drawing.Color.Transparent;
            this.buttonRefreshCaptchaSignup.Name = "buttonRefreshCaptchaSignup";
            this.buttonRefreshCaptchaSignup.TabStop = false;
            this.buttonRefreshCaptchaSignup.UseVisualStyleBackColor = false;
            this.buttonRefreshCaptchaSignup.Click += new System.EventHandler(this.buttonRefreshCaptcha_Click);
            // 
            // labelErrorMessageSignup
            // 
            resources.ApplyResources(this.labelErrorMessageSignup, "labelErrorMessageSignup");
            this.labelErrorMessageSignup.Name = "labelErrorMessageSignup";
            // 
            // labelCaptchaSignup
            // 
            resources.ApplyResources(this.labelCaptchaSignup, "labelCaptchaSignup");
            this.labelCaptchaSignup.Name = "labelCaptchaSignup";
            // 
            // textBoxCaptchaSignup
            // 
            this.textBoxCaptchaSignup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxCaptchaSignup, "textBoxCaptchaSignup");
            this.textBoxCaptchaSignup.Name = "textBoxCaptchaSignup";
            this.textBoxCaptchaSignup.TextChanged += new System.EventHandler(this.textBoxesSignup_TextChanged);
            // 
            // labelSignUp
            // 
            resources.ApplyResources(this.labelSignUp, "labelSignUp");
            this.labelSignUp.Name = "labelSignUp";
            // 
            // labelEmailSignup
            // 
            resources.ApplyResources(this.labelEmailSignup, "labelEmailSignup");
            this.labelEmailSignup.Name = "labelEmailSignup";
            // 
            // pictureBoxCaptchaSignup
            // 
            resources.ApplyResources(this.pictureBoxCaptchaSignup, "pictureBoxCaptchaSignup");
            this.pictureBoxCaptchaSignup.Name = "pictureBoxCaptchaSignup";
            this.pictureBoxCaptchaSignup.TabStop = false;
            // 
            // textBoxEmailSignup
            // 
            this.textBoxEmailSignup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxEmailSignup, "textBoxEmailSignup");
            this.textBoxEmailSignup.Name = "textBoxEmailSignup";
            this.textBoxEmailSignup.TextChanged += new System.EventHandler(this.textBoxesSignup_TextChanged);
            // 
            // textBoxPasswordSignup
            // 
            this.textBoxPasswordSignup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxPasswordSignup, "textBoxPasswordSignup");
            this.textBoxPasswordSignup.Name = "textBoxPasswordSignup";
            this.textBoxPasswordSignup.TextChanged += new System.EventHandler(this.textBoxesSignup_TextChanged);
            // 
            // labelPasswordSignup
            // 
            resources.ApplyResources(this.labelPasswordSignup, "labelPasswordSignup");
            this.labelPasswordSignup.Name = "labelPasswordSignup";
            // 
            // textBoxAccountSignup
            // 
            this.textBoxAccountSignup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBoxAccountSignup, "textBoxAccountSignup");
            this.textBoxAccountSignup.Name = "textBoxAccountSignup";
            this.textBoxAccountSignup.TextChanged += new System.EventHandler(this.textBoxesSignup_TextChanged);
            // 
            // labelAccountSignup
            // 
            resources.ApplyResources(this.labelAccountSignup, "labelAccountSignup");
            this.labelAccountSignup.Name = "labelAccountSignup";
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrevious
            // 
            resources.ApplyResources(this.buttonPrevious, "buttonPrevious");
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // panelFullSetup
            // 
            this.panelFullSetup.BackColor = System.Drawing.Color.White;
            this.panelFullSetup.Controls.Add(this.radioButtonAdvanced);
            this.panelFullSetup.Controls.Add(this.radioButtonTypical);
            this.panelFullSetup.Controls.Add(this.labelFulSetup);
            resources.ApplyResources(this.panelFullSetup, "panelFullSetup");
            this.panelFullSetup.Name = "panelFullSetup";
            // 
            // radioButtonAdvanced
            // 
            resources.ApplyResources(this.radioButtonAdvanced, "radioButtonAdvanced");
            this.radioButtonAdvanced.Name = "radioButtonAdvanced";
            this.radioButtonAdvanced.TabStop = true;
            this.radioButtonAdvanced.UseVisualStyleBackColor = true;
            // 
            // radioButtonTypical
            // 
            resources.ApplyResources(this.radioButtonTypical, "radioButtonTypical");
            this.radioButtonTypical.Checked = true;
            this.radioButtonTypical.Name = "radioButtonTypical";
            this.radioButtonTypical.TabStop = true;
            this.radioButtonTypical.UseVisualStyleBackColor = true;
            // 
            // labelFulSetup
            // 
            resources.ApplyResources(this.labelFulSetup, "labelFulSetup");
            this.labelFulSetup.Name = "labelFulSetup";
            // 
            // panelExit
            // 
            this.panelExit.BackColor = System.Drawing.Color.White;
            this.panelExit.Controls.Add(this.label2);
            this.panelExit.Controls.Add(this.label1);
            resources.ApplyResources(this.panelExit, "panelExit");
            this.panelExit.Name = "panelExit";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // loginBW
            // 
            this.loginBW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.loginBW_DoWork);
            this.loginBW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.loginBW_RunWorkerCompleted);
            // 
            // getCaptchaBW
            // 
            this.getCaptchaBW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.getCaptchaBW_DoWork);
            this.getCaptchaBW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.getCaptchaBW_RunWorkerCompleted);
            // 
            // signUpBW
            // 
            this.signUpBW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.signUpBW_DoWork);
            this.signUpBW.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.signUpBW_RunWorkerCompleted);
            // 
            // FormSetup
            // 
            this.AcceptButton = this.buttonNext;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Controls.Add(this.panelExit);
            this.Controls.Add(this.panelFullSetup);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.panelCreateAccount);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.panelEnter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetup";
            this.Activated += new System.EventHandler(this.FormSetup_Activated);
            this.Deactivate += new System.EventHandler(this.FormSetup_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSetup_FormClosing);
            this.Load += new System.EventHandler(this.FormSetup_Load);
            this.panelEnter.ResumeLayout(false);
            this.panelEnter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptchaLogin)).EndInit();
            this.panelCreateAccount.ResumeLayout(false);
            this.panelCreateAccount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptchaSignup)).EndInit();
            this.panelFullSetup.ResumeLayout(false);
            this.panelFullSetup.PerformLayout();
            this.panelExit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonHasAccount;
        private System.Windows.Forms.RadioButton radioButtonDoesNotHaveAccount;
        private System.Windows.Forms.Panel panelEnter;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPasswordLogin;
        private System.Windows.Forms.Label labelAccountLogin;
        private System.Windows.Forms.TextBox textBoxPasswordLogin;
        private System.Windows.Forms.TextBox textBoxAccountLogin;
        private System.Windows.Forms.Label labelErrorMessageLogin;
        private System.Windows.Forms.Panel panelCreateAccount;
        private System.Windows.Forms.Label labelEmailSignup;
        private System.Windows.Forms.TextBox textBoxEmailSignup;
        private System.Windows.Forms.TextBox textBoxPasswordSignup;
        private System.Windows.Forms.Label labelPasswordSignup;
        private System.Windows.Forms.TextBox textBoxAccountSignup;
        private System.Windows.Forms.Label labelAccountSignup;
        private System.Windows.Forms.Label labelSignUp;
        private System.Windows.Forms.TextBox textBoxCaptchaSignup;
        private System.Windows.Forms.Label labelCaptchaSignup;
        private System.Windows.Forms.PictureBox pictureBoxCaptchaSignup;
        private System.Windows.Forms.PictureBox pictureBoxCaptchaLogin;
        private System.Windows.Forms.Label labelCaptchaLogin;
        private System.Windows.Forms.TextBox textBoxCaptchaLogin;
        private System.Windows.Forms.Label labelErrorMessageSignup;
        private System.Windows.Forms.Button buttonRefreshCaptchaLogin;
        private System.Windows.Forms.Button buttonRefreshCaptchaSignup;
        private System.Windows.Forms.TextBox textBoxRepeatPasswordSignup;
        private System.Windows.Forms.Label labelRepeatPasswordSignup;
        private System.Windows.Forms.Panel panelFullSetup;
        private System.Windows.Forms.RadioButton radioButtonAdvanced;
        private System.Windows.Forms.RadioButton radioButtonTypical;
        private System.Windows.Forms.Label labelFulSetup;
        private System.Windows.Forms.Panel panelExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker loginBW;
        private System.ComponentModel.BackgroundWorker getCaptchaBW;
        private System.ComponentModel.BackgroundWorker signUpBW;
    }
}