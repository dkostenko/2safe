namespace TwoSafe.View
{
    partial class CreateFolderFromSettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateFolderFromSettingsDialog));
            this.labelMessage = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonCreateFolder = new System.Windows.Forms.Button();
            this.labelFolder = new System.Windows.Forms.Label();
            this.labelWarning = new System.Windows.Forms.Label();
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
            // buttonCreateFolder
            // 
            resources.ApplyResources(this.buttonCreateFolder, "buttonCreateFolder");
            this.buttonCreateFolder.Name = "buttonCreateFolder";
            this.buttonCreateFolder.UseVisualStyleBackColor = true;
            // 
            // labelFolder
            // 
            resources.ApplyResources(this.labelFolder, "labelFolder");
            this.labelFolder.Name = "labelFolder";
            // 
            // labelWarning
            // 
            resources.ApplyResources(this.labelWarning, "labelWarning");
            this.labelWarning.Name = "labelWarning";
            // 
            // CreateFolderFromSettingsDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.labelFolder);
            this.Controls.Add(this.buttonCreateFolder);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelMessage);
            this.Name = "CreateFolderFromSettingsDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonCreateFolder;
        private System.Windows.Forms.Label labelFolder;
        private System.Windows.Forms.Label labelWarning;
    }
}