﻿namespace TwoSafe.View
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateFolderDialog));
            this.labelMessage = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonCreateDefault = new System.Windows.Forms.Button();
            this.buttonCreateCustom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelMessage
            // 
            resources.ApplyResources(this.labelMessage, "labelMessage");
            this.labelMessage.Name = "labelMessage";
            // 
            // buttonExit
            // 
            resources.ApplyResources(this.buttonExit, "buttonExit");
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.UseVisualStyleBackColor = true;
            // 
            // buttonCreateDefault
            // 
            resources.ApplyResources(this.buttonCreateDefault, "buttonCreateDefault");
            this.buttonCreateDefault.Name = "buttonCreateDefault";
            this.buttonCreateDefault.UseVisualStyleBackColor = true;
            // 
            // buttonCreateCustom
            // 
            resources.ApplyResources(this.buttonCreateCustom, "buttonCreateCustom");
            this.buttonCreateCustom.Name = "buttonCreateCustom";
            this.buttonCreateCustom.UseVisualStyleBackColor = true;
            // 
            // CreateFolderDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCreateCustom);
            this.Controls.Add(this.buttonCreateDefault);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelMessage);
            this.Name = "CreateFolderDialog";
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