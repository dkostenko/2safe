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
    /// <summary>
    /// Форма первоначальной настройки
    /// </summary>
    public partial class FormSetup : Form
    {
        /// <summary>
        /// Менеджер языковых ресурсов
        /// </summary>
        ResourceManager language;
        
        /// <summary>
        /// Переменные для дублирования значений из текстбоксов формы
        /// Используются в параллельном треде, из которого нельзя получить доступ к контролам из основного треда
        /// </summary>
        private string captchaId, activePanel, account, password, captcha, email;
        
        /// <summary>
        /// Ответ сервера на запрос капчи
        /// </summary>
        Object[] captchaResponse;
        
        /// <summary>
        /// Ответ с сервера на запрос авторизации
        /// </summary>
        Dictionary<string, dynamic> logInResponse;
        
        /// <summary>
        /// Ответ с сервера на запрос создания аккаунта
        /// </summary>
        Dictionary<string, dynamic> signUpResponse;

        /// <summary>
        /// Контсруктор по умолчанию
        /// </summary>
        public FormSetup()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            language = new ResourceManager(typeof(TwoSafe.View.WinFormStrings));
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
            activePanel = "enter";
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события изменения настроек
        /// Выставляет язык контролов формы Setup
        /// </summary>
        private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            language = new ResourceManager(typeof(TwoSafe.View.WinFormStrings));
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

        /// <summary>
        /// Обработчик события загрузки формы
        /// </summary>
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
                //делаем в основном треде неактивными контролы, и ставим сообщение Connecting...
                this.buttonNext.Enabled = false;
                this.buttonPrevious.Enabled = false;
                this.labelErrorMessageLogin.Text = language.GetString("message002");
                // транслируем значения из текстбоксов в местные поля
                account = textBoxAccountLogin.Text;
                password = textBoxPasswordLogin.Text;
                captcha = textBoxCaptchaLogin.Text;
                // запускаем loginBW
                loginBW.RunWorkerAsync();
                return;
            }

            if (activePanel == "signup") // делаем signup
            {
                //делаем в основном треде неактивными контролы, и ставим сообщение Connecting...
                this.buttonNext.Enabled = false;
                this.buttonPrevious.Enabled = false;
                this.labelErrorMessageSignup.Text = language.GetString("message002");
                // валидация паролей
                if (textBoxPasswordSignup.Text != textBoxRepeatPasswordSignup.Text)
                {
                    labelErrorMessageSignup.Text = language.GetString("message004");
                    buttonNext.Enabled = true;
                    buttonPrevious.Enabled = true;
                    return;
                }
                // транслируем значения из текстбоксов в местные поля
                account = textBoxAccountSignup.Text;
                password = textBoxPasswordSignup.Text;
                captcha = textBoxCaptchaSignup.Text;
                email = textBoxEmailSignup.Text;
                // запускаем signUpBW
                signUpBW.RunWorkerAsync();
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
                    if (!getCaptchaBW.IsBusy)
                    {
                        getCaptchaBW.RunWorkerAsync();    
                    }
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

        /// <summary>
        /// Отправка запроса на логин в параллельном треде
        /// </summary>
        private void loginBW_DoWork(object sender, DoWorkEventArgs e)
        {
            logInResponse = Controller.ApiTwoSafe.auth(account, password, captchaId, captcha);
        }

        /// <summary>
        /// Метод выполняющийся в главном треде по окончанию работы loginBW 
        /// </summary>
        private void loginBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (logInResponse == null)
            {
                labelErrorMessageLogin.Text = language.GetString("error085");
                ValidateLoginTextBoxes();
            }
            else
            {
                if (logInResponse.ContainsKey("error_code"))
                {
                    // если проблемы с капчей - вываливаем ее
                    if (logInResponse["error_code"] == "053")
                    {
                        loginCaptchaVisible(true);
                    }
                    // Получаем пока новую параллельно
                    if (!getCaptchaBW.IsBusy)
                    {
                        getCaptchaBW.RunWorkerAsync();
                    }
                    labelErrorMessageLogin.Text = language.GetString("error" + logInResponse["error_code"]);
                    ValidateLoginTextBoxes();
                }
                else
                {
                    // full setup
                    loginCaptchaVisible(false);
                    labelErrorMessageLogin.Text = language.GetString("message001");
                    Properties.Settings.Default.Token = logInResponse["response"]["token"];
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
        }
        
        /// <summary>
        /// Отправка запроса на создание аккаунта в параллельном треде
        /// </summary>
        private void signUpBW_DoWork(object sender, DoWorkEventArgs e)
        {
            signUpResponse = Controller.ApiTwoSafe.addLogin(account, password, email, captcha, captchaId);
        }

        /// <summary>
        /// Метод выполняющийся в главном треде по окончанию работы signUpBW 
        /// </summary>
        private void signUpBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (signUpResponse == null) // невозможно соединиться с сервером
            {
                labelErrorMessageSignup.Text = language.GetString("error085");
            }
            else
            {
                if (signUpResponse.ContainsKey("error_code")) // сервер вернул ошибку
                {
                    labelErrorMessageSignup.Text = language.GetString("error" + signUpResponse["error_code"]);
                    if (!getCaptchaBW.IsBusy)
                    {
                        getCaptchaBW.RunWorkerAsync();    
                    }
                }
                else // ошибки нет - все норм, нужно логиниться данным аккаунтом
                {
                    panelCreateAccount.Visible = false;
                    panelLogin.Visible = true;
                    activePanel = "login";
                    textBoxAccountLogin.Text = account;
                    labelErrorMessageLogin.Text = language.GetString("message003");
                    labelErrorMessageSignup.Text = "";
                    buttonPrevious.Enabled = true;
                }
            }
            ValidateSignupTextBoxes();
        }

        /// <summary>
        /// Отправка запроса на капчу в параллельном треде
        /// </summary>
        private void getCaptchaBW_DoWork(object sender, DoWorkEventArgs e)
        {
            captchaResponse = Controller.ApiTwoSafe.getCaptcha();
        }

        /// <summary>
        /// Метод выполняющийся в главном треде по окончанию работы getCaptchaBW 
        /// </summary>
        private void getCaptchaBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (captchaResponse.Length != 0)
            {
                pictureBoxCaptchaSignup.BackgroundImage = (Bitmap)captchaResponse[0];
                pictureBoxCaptchaLogin.BackgroundImage = (Bitmap)captchaResponse[0];
                captchaId = (string)captchaResponse[1];
                textBoxCaptchaLogin.Clear();
                textBoxCaptchaSignup.Clear();
            }
        }

        /// <summary>
        /// Делает видимой/невидимой группу captcha на панели Login
        /// </summary>
        /// <param name="visible">TRUE - если надо сделать группу видимой, FALSE - если надо сделать группу невидимой</param>
        private void loginCaptchaVisible(bool visible)
        {
            pictureBoxCaptchaLogin.BackgroundImage = null;
            pictureBoxCaptchaSignup.BackgroundImage = null;
            textBoxCaptchaLogin.Clear();
            
            pictureBoxCaptchaLogin.Visible = visible;
            labelCaptchaLogin.Visible = visible;
            textBoxCaptchaLogin.Visible = visible;
            buttonRefreshCaptchaLogin.Visible = visible;
        }

        /// <summary>
        /// Нажатие на кнопку "Назад"
        /// </summary>
        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (activePanel == "login")
            {
                panelLogin.Visible = false;
            }

            if (activePanel == "signup")
            {
                panelCreateAccount.Visible = false;
            }
            activePanel = "enter";
            buttonNext.Enabled = true;
            buttonPrevious.Visible = false;
            panelEnter.Visible = true;
        }

        /// <summary>
        /// Валидация содержимого текстбоксов панели логин
        /// </summary>
        private void ValidateLoginTextBoxes()
        {
            if (pictureBoxCaptchaLogin.Visible == false)
            {
                if (textBoxAccountLogin.Text != "" && textBoxPasswordLogin.Text != "" && !loginBW.IsBusy )
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
                !signUpBW.IsBusy)
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
        /// Нажатие на кнопку обновления капчи
        /// </summary>
        private void buttonRefreshCaptcha_Click(object sender, EventArgs e)
        {
            if (!getCaptchaBW.IsBusy)
            {
                getCaptchaBW.RunWorkerAsync();    
            }
        }

        /// <summary>
        /// При закрытии формы - закрываем приложение
        /// </summary>
        private void FormSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Добавить корректный выход
        }

        private void FormSetup_Activated(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
        }

        private void FormSetup_Deactivate(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }
    }
}
