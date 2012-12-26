namespace TwoSafe.View
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.tbAccount = new System.Windows.Forms.TextBox();
            this.labelAccount = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.labelPassowrd = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.labelErrorMessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelErrorMessageLogin = new System.Windows.Forms.Label();
            this.buttonRefreshCaptchaLogin = new System.Windows.Forms.Button();
            this.labelCaptchaLogin = new System.Windows.Forms.Label();
            this.textBoxCaptchaLogin = new System.Windows.Forms.TextBox();
            this.pictureBoxCaptchaLogin = new System.Windows.Forms.PictureBox();
            this.labelLogIn = new System.Windows.Forms.Label();
            this.loginBW = new System.ComponentModel.BackgroundWorker();
            this.getCaptchaBW = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptchaLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // tbAccount
            // 
            this.tbAccount.BackColor = System.Drawing.Color.White;
            this.tbAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.tbAccount, "tbAccount");
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.ReadOnly = true;
            this.tbAccount.TextChanged += new System.EventHandler(this.textBoxes_TextChanged);
            // 
            // labelAccount
            // 
            resources.ApplyResources(this.labelAccount, "labelAccount");
            this.labelAccount.Name = "labelAccount";
            // 
            // tbPassword
            // 
            this.tbPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.tbPassword, "tbPassword");
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.TextChanged += new System.EventHandler(this.textBoxes_TextChanged);
            // 
            // labelPassowrd
            // 
            resources.ApplyResources(this.labelPassowrd, "labelPassowrd");
            this.labelPassowrd.Name = "labelPassowrd";
            // 
            // buttonLogin
            // 
            resources.ApplyResources(this.buttonLogin, "buttonLogin");
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // labelErrorMessage
            // 
            resources.ApplyResources(this.labelErrorMessage, "labelErrorMessage");
            this.labelErrorMessage.Name = "labelErrorMessage";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.labelErrorMessageLogin);
            this.panel1.Controls.Add(this.buttonRefreshCaptchaLogin);
            this.panel1.Controls.Add(this.labelCaptchaLogin);
            this.panel1.Controls.Add(this.textBoxCaptchaLogin);
            this.panel1.Controls.Add(this.pictureBoxCaptchaLogin);
            this.panel1.Controls.Add(this.labelLogIn);
            this.panel1.Controls.Add(this.tbAccount);
            this.panel1.Controls.Add(this.labelErrorMessage);
            this.panel1.Controls.Add(this.labelAccount);
            this.panel1.Controls.Add(this.tbPassword);
            this.panel1.Controls.Add(this.labelPassowrd);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // labelErrorMessageLogin
            // 
            resources.ApplyResources(this.labelErrorMessageLogin, "labelErrorMessageLogin");
            this.labelErrorMessageLogin.Name = "labelErrorMessageLogin";
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
            this.textBoxCaptchaLogin.TextChanged += new System.EventHandler(this.textBoxes_TextChanged);
            // 
            // pictureBoxCaptchaLogin
            // 
            resources.ApplyResources(this.pictureBoxCaptchaLogin, "pictureBoxCaptchaLogin");
            this.pictureBoxCaptchaLogin.Name = "pictureBoxCaptchaLogin";
            this.pictureBoxCaptchaLogin.TabStop = false;
            // 
            // labelLogIn
            // 
            resources.ApplyResources(this.labelLogIn, "labelLogIn");
            this.labelLogIn.Name = "labelLogIn";
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
            // FormLogin
            // 
            this.AcceptButton = this.buttonLogin;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptchaLogin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbAccount;
        private System.Windows.Forms.Label labelAccount;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label labelPassowrd;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label labelErrorMessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelLogIn;
        private System.Windows.Forms.Button buttonRefreshCaptchaLogin;
        private System.Windows.Forms.Label labelCaptchaLogin;
        private System.Windows.Forms.TextBox textBoxCaptchaLogin;
        private System.Windows.Forms.PictureBox pictureBoxCaptchaLogin;
        private System.Windows.Forms.Label labelErrorMessageLogin;
        private System.ComponentModel.BackgroundWorker loginBW;
        private System.ComponentModel.BackgroundWorker getCaptchaBW;
    }
}

