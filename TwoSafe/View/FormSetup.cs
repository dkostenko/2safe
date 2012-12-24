using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Resources;
using System.IO;

namespace TwoSafe.View
{
    public partial class FormSetup : Form
    {
        private string captchaId;
        private string activePanel;
        ResourceManager language;
        Thread threadCaptcha;
        Thread logIn;
        Thread signUp;

        public FormSetup()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            language = new ResourceManager(typeof(TwoSafe.View.WinFormStrings));
            activePanel = "enter";
            captchaId = "";
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
            InitializeComponent();
        }

        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ChangeLanguage(Properties.Settings.Default.Language);
        }

        /// <summary>
        /// Метод изменяющий надписи на контролах во время исполнения
        /// </summary>
        /// <param name="lang"> Язык </param>
        private void ChangeLanguage(string lang)
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FormSetup));
            CultureInfo cultInformation = new CultureInfo(lang);
            // все контролы формы 
            var c = GetAllControls(this);

            // сначала применяем ресурс к самой форме
            resources.ApplyResources(this, "$this", cultInformation);
            foreach (Control control in c)
            {
                // затем применяем ресурс ко всем контролам
                resources.ApplyResources(control, control.Name, cultInformation);
            }
        }

        /// <summary>
        /// Рекурсивный метод для получения всех контролов какого-нибуль контейнера
        /// </summary>
        /// <param name="container"> Контейнер из которого нужно получить все контролы </param>
        /// <returns> Список всех контролов контейнера </returns>
        private IEnumerable<Control> GetAllControls(Control container)
        {
            List<Control> controlList = new List<Control>();
            foreach (Control c in container.Controls)
            {
                controlList.AddRange(GetAllControls(c));
                controlList.Add(c);
            }
            return controlList;
        }

        private void FormSetup_Load(object sender, EventArgs e)
        {
            pictureBoxLogo.BackgroundImage = Properties.Resources._2safeLogo;
            buttonPrevious.Visible = false;

            loginCaptchaVisible(false);

            // делаем невидимыми все панели кроме стартовой
            panelLogin.Visible = false;
            panelCreateAccount.Visible = false;
            panelFullSetup.Visible = false;
            panelExit.Visible = false;
        }

        /// <summary>
        /// Нажатие на кнопку "Вперед"
        /// </summary>
        private void buttonNext_Click(object sender, EventArgs e)
        {
            // сначала определяем какая панель была активной
            if (activePanel == "login") // делаем login
            {
                logIn = new Thread(() => LogIn(textBoxAccountLogin.Text, textBoxPasswordLogin.Text, captchaId, textBoxCaptchaLogin.Text));
                logIn.Start();
                return;
            }

            if (activePanel == "signup") // делаем signup
            {
                signUp = new Thread(() => SignUp(textBoxAccountSignup.Text, textBoxPasswordSignup.Text, textBoxEmailSignup.Text, textBoxCaptchaSignup.Text, captchaId));
                signUp.Start();
                return;
            }

            if (activePanel == "enter") // переход на панель login/signup
            {
                if (radioButtonHasAccount.Checked)
                {
                    activePanel = "login";

                    buttonPrevious.Visible = true;
                    
                    panelEnter.Visible = false;
                    panelLogin.Visible = true;

                    ValidateLoginTextBoxes();
                }
                else
                {
                    threadCaptcha = new Thread(getCaptchaAndId);
                    threadCaptcha.Start();
                    
                    activePanel = "signup";
                    
                    buttonPrevious.Visible = true;
                    
                    panelEnter.Visible = false;
                    panelCreateAccount.Visible = true;
                    
                    ValidateSignupTextBoxes();
                }
                return;
            }

            if (activePanel == "fullSetup")
            {
                if (radioButtonTypical.Checked) // typical folder setup
                {
                    // смотрим - есть ли дефолт папка
                    if (Directory.Exists(@"C:\Users\" + Environment.UserName + @"\2safe"))
                    {
                        // если есть - выкидываем диалог, что папка будет синхронизирована с серваком
                        FolderWarning fw = new FolderWarning();
                        fw.ShowDialog();
                        switch (fw.DialogResult)
                        {
                            case DialogResult.Cancel: // если юзер не согласен, то выкидываем ему выбор папки.
                                FolderBrowserDialog fbd = new FolderBrowserDialog();
                                fbd.ShowDialog();
                                if (fbd.SelectedPath != "") // если пользователь что то выбрал
                                {
                                    Properties.Settings.Default.UserFolderPath = fbd.SelectedPath;
                                    Properties.Settings.Default.Save();

                                    panelFullSetup.Visible = false;
                                    panelExit.Visible = true;
                                    activePanel = "exit";
                                    buttonNext.Text = language.GetString("buttonText001");
                                }
                                break;
                            case DialogResult.OK: // если юзер согласен - все ОК
                                Properties.Settings.Default.UserFolderPath = @"C:\Users\" + Environment.UserName + @"\2safe";
                                Properties.Settings.Default.Save();
                                
                                panelFullSetup.Visible = false;
                                panelExit.Visible = true;
                                activePanel = "exit";
                                buttonNext.Text = language.GetString("buttonText001");
                                break;
                        }
                    }
                    else
                    {
                        //если нету - то просто создаем и идем на панель exit
                        Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\2safe");
                        panelFullSetup.Visible = false;
                        panelExit.Visible = true;
                        activePanel = "exit";
                        buttonNext.Text = language.GetString("buttonText001");
                    }
                }
                else // advanced setup
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.ShowDialog();
                    if (fbd.SelectedPath != "") // если пользователь что то выбрал
                    {
                        Properties.Settings.Default.UserFolderPath = fbd.SelectedPath;
                        Properties.Settings.Default.Save();

                        panelFullSetup.Visible = false;
                        panelExit.Visible = true;
                        activePanel = "exit";
                        buttonNext.Text = language.GetString("buttonText001");
                    }
                }
                return;
            }
            if (activePanel == "exit")
            {
                this.Close();
                return;
            }
        }


        private void LogIn(string account, string password, string idOfCaptcha, string captcha)
        {
            buttonNext.Enabled = false;
            buttonPrevious.Enabled = false;
            labelErrorMessageLogin.Text = language.GetString("message002");

            Dictionary<string, dynamic> response = Controller.ApiTwoSafe.auth(account, password, idOfCaptcha, captcha);

            if (response == null)
            {
                labelErrorMessageLogin.Text = language.GetString("error085");
                buttonPrevious.Enabled = true;
                buttonNext.Enabled = true;
            }
            else
            {
                if (response.ContainsKey("error_code"))
                {
                    // если проблемы с капчей - вываливаем ее
                    if (response["error_code"] == "053")
                    {
                        loginCaptchaVisible(true);
                        threadCaptcha = new Thread(getCaptchaAndId);
                        threadCaptcha.Start();

                    }
                    else
                    {
                        threadCaptcha = new Thread(getCaptchaAndId);
                        threadCaptcha.Start();
                    }

                    labelErrorMessageLogin.Text = language.GetString("error" + response["error_code"]);
                    buttonPrevious.Enabled = true;
                    buttonNext.Enabled = true;
                }
                else
                {
                    // full setup
                    loginCaptchaVisible(false);
                    labelErrorMessageLogin.Text = language.GetString("message001");
                    Properties.Settings.Default.Token = response["response"]["token"];
                    Properties.Settings.Default.Account = account;
                    Properties.Settings.Default.Save();

                    activePanel = "fullSetup";
                    panelFullSetup.Visible = true;
                    panelLogin.Visible = false;

                    buttonPrevious.Enabled = true;
                    buttonNext.Enabled = true;
                    buttonPrevious.Visible = false;
                }
            }
            logIn = null;
        }

        private void SignUp(string account, string password, string email, string captcha, string idOfCaptcha)
        {
            buttonNext.Enabled = false;
            buttonPrevious.Enabled = false;
            
            // валидация паролей
            if (textBoxPasswordSignup.Text != textBoxRepeatPasswordSignup.Text)
            {
                labelErrorMessageSignup.Text = language.GetString("message004");
                buttonNext.Enabled = true;
                buttonPrevious.Enabled = true;
                signUp = null;
                return;
            }

            labelErrorMessageSignup.Text = language.GetString("message002");

            Dictionary<string, dynamic> response = Controller.ApiTwoSafe.addLogin(account, password, email, captcha, idOfCaptcha);
            
            if (response == null) // невозможно соединиться с сервером
            {
                labelErrorMessageSignup.Text = language.GetString("error085");
            }
            else
            {
                if (response.ContainsKey("error_code")) // сервер вернул ошибку
                {
                    labelErrorMessageSignup.Text = language.GetString("error" + response["error_code"]);
                    threadCaptcha = new Thread(getCaptchaAndId);
                    threadCaptcha.Start();
                }
                else // ошибки нет - все норм, нужно логиниться данным аккаунтом и далее смотреть настройки
                {
                    panelCreateAccount.Visible = false;
                    panelLogin.Visible = true;
                    textBoxAccountLogin.Text = account;
                    activePanel = "login";
                    labelErrorMessageLogin.Text = language.GetString("message003");
                    labelErrorMessageSignup.Text = "";
                    buttonPrevious.Enabled = true;
                }
            }

            buttonNext.Enabled = true;
            buttonPrevious.Enabled = true;
            signUp = null; 
        }

        /// <summary>
        /// Получение капчи и ее ID
        /// Запускать в отдельном треде!
        /// </summary>
        private void getCaptchaAndId()
        {
            Object[] captcha = Controller.ApiTwoSafe.getCaptcha();
            pictureBoxCaptchaSignup.BackgroundImage = (Bitmap)captcha[0];
            pictureBoxCaptchaLogin.BackgroundImage = (Bitmap)captcha[0];
            captchaId = (string)captcha[1];
        }

        /// <summary>
        /// Делает видимым/невидимым группу captcha на панели Login
        /// </summary>
        /// <param name="visible"></param>
        private void loginCaptchaVisible(bool visible)
        {
            if (visible)
            {
                pictureBoxCaptchaLogin.BackgroundImage = null;
                pictureBoxCaptchaSignup.BackgroundImage = null;
                textBoxCaptchaLogin.Clear();

                pictureBoxCaptchaLogin.Visible = true;
                labelCaptchaLogin.Visible = true;
                textBoxCaptchaLogin.Visible = true;
                buttonRefreshCaptchaLogin.Visible = true;
            }
            else
            {
                pictureBoxCaptchaLogin.BackgroundImage = null;
                pictureBoxCaptchaSignup.BackgroundImage = null;
                textBoxCaptchaLogin.Clear();

                pictureBoxCaptchaLogin.Visible = false;
                labelCaptchaLogin.Visible = false;
                textBoxCaptchaLogin.Visible = false;
                buttonRefreshCaptchaLogin.Visible = false;
            }
        }

        /// <summary>
        /// Нажатие на кнопку "Назад"
        /// </summary>
        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            
            if (activePanel == "login")
            {
                activePanel = "enter";
                buttonNext.Enabled = true;
                buttonPrevious.Visible = false;
                
                panelLogin.Visible = false;
                panelEnter.Visible = true;
            }
            
            if (activePanel == "signup")
            {
                activePanel = "enter";
                buttonNext.Enabled = true;
                buttonPrevious.Visible = false;

                panelEnter.Visible = true;
                panelCreateAccount.Visible = false;
            }
        }

        /// <summary>
        /// Валидация содержимого текстбоксов панели логин
        /// </summary>
        private void ValidateLoginTextBoxes()
        {
            if (pictureBoxCaptchaLogin.Visible == false)
            {
                if (textBoxAccountLogin.Text != "" && textBoxPasswordLogin.Text != "" && logIn == null)
                {
                    buttonNext.Enabled = true;
                }
                else
                {
                    buttonNext.Enabled = false;
                }
            }
            else
            {
                if (textBoxAccountLogin.Text != "" && 
                    textBoxPasswordLogin.Text != ""&&
                    textBoxCaptchaLogin.Text != "")
                {
                    buttonNext.Enabled = true;
                }
                else
                {
                    buttonNext.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Валидация содержимого текстбоксов панели Signup
        /// </summary>
        private void ValidateSignupTextBoxes()
        {
            if (textBoxAccountSignup.Text != "" && 
                textBoxPasswordSignup.Text != "" &&
                textBoxRepeatPasswordSignup.Text != "" &&
                textBoxEmailSignup.Text != "" &&
                textBoxCaptchaSignup.Text!= "" &&
                signUp == null)
            {
                buttonNext.Enabled = true;
            }
            else
            {
                buttonNext.Enabled = false;
            }
        }

        /// <summary>
        /// Запуск валидации содержимого текстбоксов панели логин при изменении этого содержимого
        /// </summary>
        private void textBoxesLogin_TextChanged(object sender, EventArgs e)
        {
            ValidateLoginTextBoxes();
        }

        /// <summary>
        /// Запуск валидации содержимого текстбоксов панели логин при изменении этого содержимого
        /// </summary>
        private void textBoxesSignup_TextChanged(object sender, EventArgs e)
        {
            ValidateSignupTextBoxes();
        }

        /// <summary>
        /// Нажатие на кнопку обновления капчи на панели login
        /// </summary>
        private void buttonRefreshCaptcha_Click(object sender, EventArgs e)
        {
            threadCaptcha = new Thread(getCaptchaAndId);
            threadCaptcha.Start();
        }

        /// <summary>
        /// При закрытии мы предупреждаем, что закроется все приложение
        /// </summary>
        private void FormSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show(language.GetString("message006"), language.GetString("message007"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    //e.Cancel = false;
            //    Environment.Exit(0);
            //}
        }
    }
}
