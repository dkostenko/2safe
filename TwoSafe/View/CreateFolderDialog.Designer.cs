namespace TwoSafe.View
{
    partial class CreateFolderDialog
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
            this.labelMessage = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonCreateDefault = new System.Windows.Forms.Button();
            this.buttonCreateCustom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(39, 38);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(295, 13);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.Text = "A 2safe folder is required on your computer to run 2safe client";
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(79, 74);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(209, 43);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "Close application";
            this.buttonExit.UseVisualStyleBackColor = true;
            // 
            // buttonCreateDefault
            // 
            this.buttonCreateDefault.Location = new System.Drawing.Point(79, 123);
            this.buttonCreateDefault.Name = "buttonCreateDefault";
            this.buttonCreateDefault.Size = new System.Drawing.Size(209, 43);
            this.buttonCreateDefault.TabIndex = 2;
            this.buttonCreateDefault.Text = "Create default folder";
            this.buttonCreateDefault.UseVisualStyleBackColor = true;
            // 
            // buttonCreateCustom
            // 
            this.buttonCreateCustom.Location = new System.Drawing.Point(79, 172);
            this.buttonCreateCustom.Name = "buttonCreateCustom";
            this.buttonCreateCustom.Size = new System.Drawing.Size(209, 43);
            this.buttonCreateCustom.TabIndex = 3;
            this.buttonCreateCustom.Text = "Create custom/ show existing folder";
            this.buttonCreateCustom.UseVisualStyleBackColor = true;
            // 
            // CreateFolderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 255);
            this.Controls.Add(this.buttonCreateCustom);
            this.Controls.Add(this.buttonCreateDefault);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelMessage);
            this.Name = "CreateFolderDialog";
            this.Text = "CreateFolderDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonCreateDefault;
        private System.Windows.Forms.Button buttonCreateCustom;
    }
}