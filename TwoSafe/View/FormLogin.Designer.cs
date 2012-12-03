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
            this.tbAccount = new System.Windows.Forms.TextBox();
            this.labelAccount = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.labelPassowrd = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbAccount
            // 
            this.tbAccount.Location = new System.Drawing.Point(97, 44);
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.Size = new System.Drawing.Size(100, 20);
            this.tbAccount.TabIndex = 0;
            // 
            // labelAccount
            // 
            this.labelAccount.AutoSize = true;
            this.labelAccount.Location = new System.Drawing.Point(44, 47);
            this.labelAccount.Name = "labelAccount";
            this.labelAccount.Size = new System.Drawing.Size(47, 13);
            this.labelAccount.TabIndex = 1;
            this.labelAccount.Text = "Account";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(97, 70);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(100, 20);
            this.tbPassword.TabIndex = 2;
            // 
            // labelPassowrd
            // 
            this.labelPassowrd.AutoSize = true;
            this.labelPassowrd.Location = new System.Drawing.Point(38, 73);
            this.labelPassowrd.Name = "labelPassowrd";
            this.labelPassowrd.Size = new System.Drawing.Size(53, 13);
            this.labelPassowrd.TabIndex = 3;
            this.labelPassowrd.Text = "Password";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(97, 97);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "Log In";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.labelPassowrd);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.labelAccount);
            this.Controls.Add(this.tbAccount);
            this.Name = "FormLogin";
            this.Text = "Авторизация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbAccount;
        private System.Windows.Forms.Label labelAccount;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label labelPassowrd;
        private System.Windows.Forms.Button buttonLogin;
    }
}

