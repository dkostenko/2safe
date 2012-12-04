namespace TwoSafe.View
{
    partial class FormRegistration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegistration));
            this.labelPassword = new System.Windows.Forms.Label();
            this.tbAccount = new System.Windows.Forms.TextBox();
            this.labelPass = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.buttonRegistration = new System.Windows.Forms.Button();
            this.pbCaptcha = new System.Windows.Forms.PictureBox();
            this.labelCaptcha = new System.Windows.Forms.Label();
            this.tbCaptcha = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptcha)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPassword
            // 
            resources.ApplyResources(this.labelPassword, "labelPassword");
            this.labelPassword.Name = "labelPassword";
            // 
            // tbAccount
            // 
            resources.ApplyResources(this.tbAccount, "tbAccount");
            this.tbAccount.Name = "tbAccount";
            // 
            // labelPass
            // 
            resources.ApplyResources(this.labelPass, "labelPass");
            this.labelPass.Name = "labelPass";
            // 
            // tbPassword
            // 
            resources.ApplyResources(this.tbPassword, "tbPassword");
            this.tbPassword.Name = "tbPassword";
            // 
            // tbEmail
            // 
            resources.ApplyResources(this.tbEmail, "tbEmail");
            this.tbEmail.Name = "tbEmail";
            // 
            // labelEmail
            // 
            resources.ApplyResources(this.labelEmail, "labelEmail");
            this.labelEmail.Name = "labelEmail";
            // 
            // buttonRegistration
            // 
            resources.ApplyResources(this.buttonRegistration, "buttonRegistration");
            this.buttonRegistration.Name = "buttonRegistration";
            this.buttonRegistration.UseVisualStyleBackColor = true;
            this.buttonRegistration.Click += new System.EventHandler(this.buttonRegistration_Click);
            // 
            // pbCaptcha
            // 
            resources.ApplyResources(this.pbCaptcha, "pbCaptcha");
            this.pbCaptcha.Name = "pbCaptcha";
            this.pbCaptcha.TabStop = false;
            // 
            // labelCaptcha
            // 
            resources.ApplyResources(this.labelCaptcha, "labelCaptcha");
            this.labelCaptcha.Name = "labelCaptcha";
            // 
            // tbCaptcha
            // 
            resources.ApplyResources(this.tbCaptcha, "tbCaptcha");
            this.tbCaptcha.Name = "tbCaptcha";
            // 
            // FormRegistration
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbCaptcha);
            this.Controls.Add(this.labelCaptcha);
            this.Controls.Add(this.pbCaptcha);
            this.Controls.Add(this.buttonRegistration);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.tbEmail);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.labelPass);
            this.Controls.Add(this.tbAccount);
            this.Controls.Add(this.labelPassword);
            this.Name = "FormRegistration";
            this.Shown += new System.EventHandler(this.FormRegistration_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptcha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox tbAccount;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Button buttonRegistration;
        private System.Windows.Forms.PictureBox pbCaptcha;
        private System.Windows.Forms.Label labelCaptcha;
        private System.Windows.Forms.TextBox tbCaptcha;
    }
}