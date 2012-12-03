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
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(60, 22);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(46, 13);
            this.labelPassword.TabIndex = 0;
            this.labelPassword.Text = "account";
            // 
            // tbAccount
            // 
            this.tbAccount.Location = new System.Drawing.Point(113, 22);
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.Size = new System.Drawing.Size(100, 20);
            this.tbAccount.TabIndex = 1;
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.Location = new System.Drawing.Point(54, 75);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(52, 13);
            this.labelPass.TabIndex = 2;
            this.labelPass.Text = "password";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(113, 72);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(100, 20);
            this.tbPassword.TabIndex = 3;
            // 
            // tbEmail
            // 
            this.tbEmail.Location = new System.Drawing.Point(113, 119);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(100, 20);
            this.tbEmail.TabIndex = 4;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(70, 125);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(31, 13);
            this.labelEmail.TabIndex = 5;
            this.labelEmail.Text = "email";
            // 
            // buttonRegistration
            // 
            this.buttonRegistration.Location = new System.Drawing.Point(126, 288);
            this.buttonRegistration.Name = "buttonRegistration";
            this.buttonRegistration.Size = new System.Drawing.Size(75, 23);
            this.buttonRegistration.TabIndex = 6;
            this.buttonRegistration.Text = "registrate";
            this.buttonRegistration.UseVisualStyleBackColor = true;
            this.buttonRegistration.Click += new System.EventHandler(this.buttonRegistration_Click);
            // 
            // pbCaptcha
            // 
            this.pbCaptcha.Location = new System.Drawing.Point(57, 169);
            this.pbCaptcha.Name = "pbCaptcha";
            this.pbCaptcha.Size = new System.Drawing.Size(169, 50);
            this.pbCaptcha.TabIndex = 7;
            this.pbCaptcha.TabStop = false;
            // 
            // labelCaptcha
            // 
            this.labelCaptcha.AutoSize = true;
            this.labelCaptcha.Location = new System.Drawing.Point(65, 246);
            this.labelCaptcha.Name = "labelCaptcha";
            this.labelCaptcha.Size = new System.Drawing.Size(46, 13);
            this.labelCaptcha.TabIndex = 8;
            this.labelCaptcha.Text = "captcha";
            // 
            // tbCaptcha
            // 
            this.tbCaptcha.Location = new System.Drawing.Point(117, 243);
            this.tbCaptcha.Name = "tbCaptcha";
            this.tbCaptcha.Size = new System.Drawing.Size(100, 20);
            this.tbCaptcha.TabIndex = 9;
            // 
            // FormRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 333);
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
            this.Text = "FormRegistration";
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