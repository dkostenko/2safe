using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using System.Resources;

namespace TwoSafe.View
{
    public partial class FormPreferences : Form
    {
        private string[] cookie;
        private NotifyIcon _notifyIcon = new NotifyIcon();
        private ContextMenu contextMenu1;
        // решил их сделать отдельными, чтоыб было понятно, что за что отвечает :)
        private MenuItem open2safefolder = new MenuItem();
        private MenuItem launchwebsite = new MenuItem();
        private MenuItem recentlychangedfiles = new MenuItem();
        private MenuItem amountofdata = new MenuItem();//сколько занято места от общего количества
        private MenuItem state = new MenuItem();
        private MenuItem resumesyncing = new MenuItem();
        private MenuItem preferences = new MenuItem();
        private MenuItem helpcenter = new MenuItem();
        private MenuItem exit = new MenuItem();

        public FormPreferences()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US"); // эта строка ициниализирует язык ru-RU
            this.cookie = Model.Cookie.Read();
            
            //trayIcon.Visible = true; // показываем иконку в трее


            username.Enabled = false;
            password.Enabled = false;
            proxypassword.Checked = false;
            Proxytype.Items.Add("HTTP");
            Proxytype.Items.Add("SOCKS4");
            Proxytype.Items.Add("SOCKS5");
            language.Items.Add("English");
            language.Items.Add("Russian");

            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            // Initialize contextMenu1
            this.contextMenu1.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { this.open2safefolder,this.launchwebsite,this.recentlychangedfiles,
                        this.amountofdata, this.state,this.resumesyncing,this.preferences,this.helpcenter,this.exit});



            // Initialize
            this.open2safefolder.Index = 0;
            this.open2safefolder.Text = "Open 2Safe Folder";
            this.open2safefolder.Click += new System.EventHandler(this.open2safefolder_Click);
            this.launchwebsite.Index = 1;
            this.launchwebsite.Text = "Launch 2safe Website";
            this.launchwebsite.Click += new System.EventHandler(this.launchwebsite_Click);
            this.recentlychangedfiles.Index = 2;
            this.recentlychangedfiles.Text = "Recently Changed Files";
            this.recentlychangedfiles.Click += new System.EventHandler(this.recentlychangedfiles_Click);
            this.amountofdata.Index = 3;
            this.amountofdata.Text = "Capacity";
            this.amountofdata.Click += new System.EventHandler(this.amountofdata_Click);
            this.state.Index = 4;
            this.state.Text = "State"; // for example: All files up to date
            this.state.Click += new System.EventHandler(this.state_Click);
            this.resumesyncing.Index = 5;
            this.resumesyncing.Text = "Resume syncing";
            this.resumesyncing.Click += new System.EventHandler(this.resumesyncing_Click);
            this.preferences.Index = 6;
            this.preferences.Text = "Preferences";
            this.preferences.Click += new System.EventHandler(this.preferences_Click);
            this.helpcenter.Index = 7;
            this.helpcenter.Text = "Help center";
            this.helpcenter.Click += new System.EventHandler(this.helpcenter_Click);
            this.exit.Index = 8;
            this.exit.Text = "Exit";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            //задаем всплывающий текст-подсказку (появляется при наведении указателя на иконку в трее)
            _notifyIcon.Text = "It is our application";

            //устанавливаем значок, отображаемый в трее:
            //либо один из стандартных:
            _notifyIcon.Icon = SystemIcons.Shield;
            //либо свой из файла:
            //_notifyIcon.Icon = new Icon("favicon.ico");
            //подписываемся на событие клика мышкой по значку в трее
            _notifyIcon.MouseClick += new MouseEventHandler(_notifyIcon_MouseClick);
            _notifyIcon.Visible = true;
            _notifyIcon.ContextMenu = this.contextMenu1;
            this.Resize += new EventHandler(FormForTray_Resize);
        }

        private void recentlychangedfiles_Click(object Sender, EventArgs e)
        {

        }
        private void amountofdata_Click(object Sender, EventArgs e)
        {

        }
        private void state_Click(object Sender, EventArgs e)
        {

        }
        private void resumesyncing_Click(object Sender, EventArgs e)
        {

        }
        private void preferences_Click(object Sender, EventArgs e)
        {

        }
        private void helpcenter_Click(object Sender, EventArgs e)
        {

        }
        private void launchwebsite_Click(object Sender, EventArgs e)
        {

        }
        private void more_Click(object sender, EventArgs e)
        {
            // нажимая на эту кпопку выводить данные системные,там типо время последнего логина, сам логин, может пассворд, какие-нибудь ещё хрени
        }
        private void exit_Click(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            this.Close();
        }
        private void open2safefolder_Click(object Sender, EventArgs e)
        {

        }
        private void menuItem2_Click(object Sender, EventArgs e)
        {
            MessageBox.Show("Hello, It's a nice party isn't it?");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            View.FormRegistration formRegistration = new FormRegistration();
            formRegistration.Show();
            //this.Hide();
        }

        private void proxypassword_CheckedChanged(object sender, EventArgs e)
        {
            // чисто событие для прокси пасворда и пароля, доступны они или нет
            if (proxypassword.Checked == true)
            {
                username.Enabled = true;
                password.Enabled = true;
            }
            else
            {
                username.Enabled = false;
                password.Enabled = false;
            }
        }

        private void move_Click(object sender, EventArgs e)
        {
            //выбор папки, где будем хранить файлы
            folderBrowserDialog1.ShowDialog();
            location.Text = folderBrowserDialog1.SelectedPath;
        }
        ///
        /// здесь хранится состояние окна до сворачивания (максимизированное или нормальное)
        ///
        private FormWindowState _OldFormState;

        ///
        /// обрабатываем событие клика мышью по значку в трее
        ///
        void _notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            //проверяем, какой кнопкой было произведено нажатие
            if (e.Button == MouseButtons.Left)//если левой кнопкой мыши
            {
                //проверяем текущее состояние окна
                if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)//если оно развернуто
                {
                    //сохраняем текущее состояние
                    _OldFormState = WindowState;
                    //сворачиваем окно
                    WindowState = FormWindowState.Minimized;
                    //скрываться в трей оно будет по событию Resize (изменение размера), которое сгенерировалось после минимизации строчкой выше
                }
                else//в противном случае
                {
                    //и показываем на нанели задач
                    Show();
                    //разворачиваем (возвращаем старое состояние "до сворачивания")
                    WindowState = _OldFormState;
                }
            }
        }

        ///
        /// обрабатываем событие изменения размера
        ///
        void FormForTray_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)//если окно "свернуто"
            {
                //то скрываем его
                Hide();
            }
        }

        private void FormPreferences_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //this.Hide();
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
        }

        private void FormPreferences_Shown(object sender, EventArgs e)
        {
            bool showLoginForm = false;

            if (this.cookie == null) //если куков не существует, то просим пользователя авторизоваться заного
            {
                showLoginForm = true;
            }
            else
            {
                cookie = Model.Cookie.Read();

                Dictionary<string, dynamic> response = Controller.ApiTwoSafe.getPersonalData(cookie[0]);

                if (response.ContainsKey("error_code"))
                {
                    MessageBox.Show(response["error_msg"]);
                }
                else
                {
                    MessageBox.Show(response["response"]["success"]);
                }
            }

            if(showLoginForm)
            {
                cookie = new string[5];
                FormLogin formLogin = new FormLogin(cookie, this);
                this.Hide();
                formLogin.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //NameValueCollection postData = new NameValueCollection();
            //postData.Add("token", "4233308f69da003a7d19cbda751a32f4");
            //postData.Add("dir_id", "1074797033539"); 

            //Dictionary<string, dynamic> respond = Controller.ApiTwoSafe.putFile(postData, "C:/Users/dmitry/Desktop/text__.txt");

            //Controller.ApiTwoSafe.getFile("1931301033537", "4233308f69da003a7d19cbda751a32f4", null, "C:/Users/dmitry/Desktop/asd.jpg");

            //Controller.ApiTwoSafe.removeFile("1931301033537", "4233308f69da003a7d19cbda751a32f4", false);
        }
    }
}
