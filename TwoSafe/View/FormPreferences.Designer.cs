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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreferences));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.table = new System.Windows.Forms.TabControl();
            this.general = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lansync = new System.Windows.Forms.CheckBox();
            this.startup = new System.Windows.Forms.CheckBox();
            this.desktopnotification = new System.Windows.Forms.CheckBox();
            this.account = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.more = new System.Windows.Forms.Button();
            this.bandwidth = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.uptextlimit = new System.Windows.Forms.TextBox();
            this.UploadLimit = new System.Windows.Forms.RadioButton();
            this.Limitauto = new System.Windows.Forms.RadioButton();
            this.uploaddontlimit = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.downtextlimit = new System.Windows.Forms.TextBox();
            this.DownloadLimit = new System.Windows.Forms.RadioButton();
            this.downloaddontlimit = new System.Windows.Forms.RadioButton();
            this.proxies = new System.Windows.Forms.TabPage();
            this.password = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.proxypassword = new System.Windows.Forms.CheckBox();
            this.port = new System.Windows.Forms.TextBox();
            this.server = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Proxytype = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.manual = new System.Windows.Forms.RadioButton();
            this.autodetect = new System.Windows.Forms.RadioButton();
            this.noproxy = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.advanced = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.language = new System.Windows.Forms.ComboBox();
            this.location = new System.Windows.Forms.TextBox();
            this.move = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.table.SuspendLayout();
            this.general.SuspendLayout();
            this.panel1.SuspendLayout();
            this.account.SuspendLayout();
            this.bandwidth.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.proxies.SuspendLayout();
            this.panel4.SuspendLayout();
            this.advanced.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            resources.ApplyResources(this.trayIcon, "trayIcon");
            this.trayIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // table
            // 
            this.table.Controls.Add(this.general);
            this.table.Controls.Add(this.account);
            this.table.Controls.Add(this.bandwidth);
            this.table.Controls.Add(this.proxies);
            this.table.Controls.Add(this.advanced);
            resources.ApplyResources(this.table, "table");
            this.table.Name = "table";
            this.table.SelectedIndex = 0;
            // 
            // general
            // 
            this.general.Controls.Add(this.panel1);
            resources.ApplyResources(this.general, "general");
            this.general.Name = "general";
            this.general.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lansync);
            this.panel1.Controls.Add(this.startup);
            this.panel1.Controls.Add(this.desktopnotification);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // lansync
            // 
            resources.ApplyResources(this.lansync, "lansync");
            this.lansync.Name = "lansync";
            this.lansync.UseVisualStyleBackColor = true;
            // 
            // startup
            // 
            resources.ApplyResources(this.startup, "startup");
            this.startup.Name = "startup";
            this.startup.UseVisualStyleBackColor = true;
            // 
            // desktopnotification
            // 
            resources.ApplyResources(this.desktopnotification, "desktopnotification");
            this.desktopnotification.Name = "desktopnotification";
            this.desktopnotification.UseVisualStyleBackColor = true;
            // 
            // account
            // 
            this.account.Controls.Add(this.label1);
            this.account.Controls.Add(this.more);
            resources.ApplyResources(this.account, "account");
            this.account.Name = "account";
            this.account.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // more
            // 
            resources.ApplyResources(this.more, "more");
            this.more.Name = "more";
            this.more.UseVisualStyleBackColor = true;
            this.more.Click += new System.EventHandler(this.more_Click);
            // 
            // bandwidth
            // 
            this.bandwidth.Controls.Add(this.label3);
            this.bandwidth.Controls.Add(this.label2);
            this.bandwidth.Controls.Add(this.panel3);
            this.bandwidth.Controls.Add(this.panel2);
            resources.ApplyResources(this.bandwidth, "bandwidth");
            this.bandwidth.Name = "bandwidth";
            this.bandwidth.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.uptextlimit);
            this.panel3.Controls.Add(this.UploadLimit);
            this.panel3.Controls.Add(this.Limitauto);
            this.panel3.Controls.Add(this.uploaddontlimit);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // uptextlimit
            // 
            resources.ApplyResources(this.uptextlimit, "uptextlimit");
            this.uptextlimit.Name = "uptextlimit";
            // 
            // UploadLimit
            // 
            resources.ApplyResources(this.UploadLimit, "UploadLimit");
            this.UploadLimit.Name = "UploadLimit";
            this.UploadLimit.TabStop = true;
            this.UploadLimit.UseVisualStyleBackColor = true;
            // 
            // Limitauto
            // 
            resources.ApplyResources(this.Limitauto, "Limitauto");
            this.Limitauto.Name = "Limitauto";
            this.Limitauto.TabStop = true;
            this.Limitauto.UseVisualStyleBackColor = true;
            // 
            // uploaddontlimit
            // 
            resources.ApplyResources(this.uploaddontlimit, "uploaddontlimit");
            this.uploaddontlimit.Name = "uploaddontlimit";
            this.uploaddontlimit.TabStop = true;
            this.uploaddontlimit.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.downtextlimit);
            this.panel2.Controls.Add(this.DownloadLimit);
            this.panel2.Controls.Add(this.downloaddontlimit);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // downtextlimit
            // 
            resources.ApplyResources(this.downtextlimit, "downtextlimit");
            this.downtextlimit.Name = "downtextlimit";
            // 
            // DownloadLimit
            // 
            resources.ApplyResources(this.DownloadLimit, "DownloadLimit");
            this.DownloadLimit.Name = "DownloadLimit";
            this.DownloadLimit.TabStop = true;
            this.DownloadLimit.UseVisualStyleBackColor = true;
            // 
            // downloaddontlimit
            // 
            resources.ApplyResources(this.downloaddontlimit, "downloaddontlimit");
            this.downloaddontlimit.Name = "downloaddontlimit";
            this.downloaddontlimit.TabStop = true;
            this.downloaddontlimit.UseVisualStyleBackColor = true;
            // 
            // proxies
            // 
            this.proxies.Controls.Add(this.password);
            this.proxies.Controls.Add(this.username);
            this.proxies.Controls.Add(this.label8);
            this.proxies.Controls.Add(this.label7);
            this.proxies.Controls.Add(this.proxypassword);
            this.proxies.Controls.Add(this.port);
            this.proxies.Controls.Add(this.server);
            this.proxies.Controls.Add(this.label6);
            this.proxies.Controls.Add(this.Proxytype);
            this.proxies.Controls.Add(this.label5);
            this.proxies.Controls.Add(this.panel4);
            resources.ApplyResources(this.proxies, "proxies");
            this.proxies.Name = "proxies";
            this.proxies.UseVisualStyleBackColor = true;
            // 
            // password
            // 
            resources.ApplyResources(this.password, "password");
            this.password.Name = "password";
            // 
            // username
            // 
            resources.ApplyResources(this.username, "username");
            this.username.Name = "username";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // proxypassword
            // 
            resources.ApplyResources(this.proxypassword, "proxypassword");
            this.proxypassword.Name = "proxypassword";
            this.proxypassword.UseVisualStyleBackColor = true;
            this.proxypassword.CheckedChanged += new System.EventHandler(this.proxypassword_CheckedChanged);
            // 
            // port
            // 
            resources.ApplyResources(this.port, "port");
            this.port.Name = "port";
            // 
            // server
            // 
            resources.ApplyResources(this.server, "server");
            this.server.Name = "server";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // Proxytype
            // 
            this.Proxytype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Proxytype.FormattingEnabled = true;
            resources.ApplyResources(this.Proxytype, "Proxytype");
            this.Proxytype.Name = "Proxytype";
            // 
            // label5
            // 
            this.label5.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.manual);
            this.panel4.Controls.Add(this.autodetect);
            this.panel4.Controls.Add(this.noproxy);
            this.panel4.Controls.Add(this.label4);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // manual
            // 
            resources.ApplyResources(this.manual, "manual");
            this.manual.Name = "manual";
            this.manual.TabStop = true;
            this.manual.UseVisualStyleBackColor = true;
            // 
            // autodetect
            // 
            resources.ApplyResources(this.autodetect, "autodetect");
            this.autodetect.Name = "autodetect";
            this.autodetect.TabStop = true;
            this.autodetect.UseVisualStyleBackColor = true;
            // 
            // noproxy
            // 
            resources.ApplyResources(this.noproxy, "noproxy");
            this.noproxy.Name = "noproxy";
            this.noproxy.TabStop = true;
            this.noproxy.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // advanced
            // 
            this.advanced.Controls.Add(this.label10);
            this.advanced.Controls.Add(this.panel5);
            this.advanced.Controls.Add(this.location);
            this.advanced.Controls.Add(this.move);
            this.advanced.Controls.Add(this.label9);
            resources.ApplyResources(this.advanced, "advanced");
            this.advanced.Name = "advanced";
            this.advanced.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.language);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // language
            // 
            this.language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.language.FormattingEnabled = true;
            resources.ApplyResources(this.language, "language");
            this.language.Name = "language";
            // 
            // location
            // 
            resources.ApplyResources(this.location, "location");
            this.location.Name = "location";
            // 
            // move
            // 
            resources.ApplyResources(this.move, "move");
            this.move.Name = "move";
            this.move.UseVisualStyleBackColor = true;
            this.move.Click += new System.EventHandler(this.move_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // FormPreferences
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.table);
            this.Name = "FormPreferences";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPreferences_FormClosing);
            this.Shown += new System.EventHandler(this.FormPreferences_Shown);
            this.table.ResumeLayout(false);
            this.general.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.account.ResumeLayout(false);
            this.account.PerformLayout();
            this.bandwidth.ResumeLayout(false);
            this.bandwidth.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.proxies.ResumeLayout(false);
            this.proxies.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.advanced.ResumeLayout(false);
            this.advanced.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.TabControl table;
        private System.Windows.Forms.TabPage general;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox lansync;
        private System.Windows.Forms.CheckBox startup;
        private System.Windows.Forms.CheckBox desktopnotification;
        private System.Windows.Forms.TabPage account;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button more;
        private System.Windows.Forms.TabPage bandwidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox uptextlimit;
        private System.Windows.Forms.RadioButton UploadLimit;
        private System.Windows.Forms.RadioButton Limitauto;
        private System.Windows.Forms.RadioButton uploaddontlimit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox downtextlimit;
        private System.Windows.Forms.RadioButton DownloadLimit;
        private System.Windows.Forms.RadioButton downloaddontlimit;
        private System.Windows.Forms.TabPage proxies;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox proxypassword;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox server;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox Proxytype;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton manual;
        private System.Windows.Forms.RadioButton autodetect;
        private System.Windows.Forms.RadioButton noproxy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage advanced;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox language;
        private System.Windows.Forms.TextBox location;
        private System.Windows.Forms.Button move;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}