namespace TwoSafe.View
{
    partial class FolderWarning
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderWarning));
            this.labelWarning = new System.Windows.Forms.Label();
            this.buttonMerge = new System.Windows.Forms.Button();
            this.buttonChooseAnother = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelWarning
            // 
            resources.ApplyResources(this.labelWarning, "labelWarning");
            this.labelWarning.Name = "labelWarning";
            // 
            // buttonMerge
            // 
            resources.ApplyResources(this.buttonMerge, "buttonMerge");
            this.buttonMerge.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonMerge.Name = "buttonMerge";
            this.buttonMerge.UseVisualStyleBackColor = true;
            // 
            // buttonChooseAnother
            // 
            resources.ApplyResources(this.buttonChooseAnother, "buttonChooseAnother");
            this.buttonChooseAnother.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonChooseAnother.Name = "buttonChooseAnother";
            this.buttonChooseAnother.UseVisualStyleBackColor = true;
            // 
            // FolderWarning
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.buttonChooseAnother);
            this.Controls.Add(this.buttonMerge);
            this.Controls.Add(this.labelWarning);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FolderWarning";
            this.Load += new System.EventHandler(this.FolderWarning_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.Button buttonMerge;
        private System.Windows.Forms.Button buttonChooseAnother;
    }
}