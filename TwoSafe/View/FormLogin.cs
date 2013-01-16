using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Globalization;
using System.Threading;
using System.Resources;

namespace TwoSafe.View
{
    public partial class FormLogin : Form
    {
        ResourceManager language;
        Dictionary<string, dynamic> logInResponse;
        string account, password, captchaId, captcha;
       
        /// <summary>
        /// Ответ сервера на запрос капчи
        /// </summary>
        Object[] captchaResponse;

        public FormLogin()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            language = new ResourceManager(typeof(TwoSafe.View.WinFormStrings));
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            buttonLogin.Enabled = false;
            loginCaptchaVisible(false);
            tbAccount.Text = Properties.Settings.Default.Account;
            tbPassword.Focus();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
                buttonLogin.Enabled = false;
                this.labelErrorMessageLogin.Text = language.GetString("message002");

                account = tbAccount.Text;
                password = tbPassword.Text;
                captcha = textBoxCaptchaLogin.Text;

                loginBW.RunWorkerAsync();
        }

        private void loginBW_DoWork(object sender, DoWorkEventArgs e)
        {
            logInResponse = Controller.ApiTwoSafe.auth(account, password, captchaId, captcha);
        }

        private void loginBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (logInResponse == null)
            {
                labelErrorMessageLogin.Text = language.GetString("error085");
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
                }
                else
                {
                    loginCaptchaVisible(false);
                    labelErrorMessageLogin.Text = language.GetString("message001");
                    Properties.Settings.Default.Token = logInResponse["response"]["token"];
                    Properties.Settings.Default.Save();
                }
            }
            ValidateLoginTextBoxes();
        }
        
        /// <summary>
        /// Делает видимой/невидимой группу captcha на панели Login
        /// </summary>
        /// <param name="visible">TRUE - если надо сделать группу видимой, FALSE - если надо сделать группу невидимой</param>
        private void loginCaptchaVisible(bool visible)
        {
            pictureBoxCaptchaLogin.BackgroundImage = null;
            textBoxCaptchaLogin.Clear();

            pictureBoxCaptchaLogin.Visible = visible;
            labelCaptchaLogin.Visible = visible;
            textBoxCaptchaLogin.Visible = visible;
            buttonRefreshCaptchaLogin.Visible = visible;
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
                pictureBoxCaptchaLogin.BackgroundImage = (Bitmap)captchaResponse[0];
                captchaId = (string)captchaResponse[1];
                textBoxCaptchaLogin.Clear();
            }
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
        /// Валидация содержимого текстбоксов панели логин
        /// </summary>
        private void ValidateLoginTextBoxes()
        {
            if (pictureBoxCaptchaLogin.Visible == false)
            {
                if (tbAccount.Text != "" && tbPassword.Text != "" && !loginBW.IsBusy)
                {
                    buttonLogin.Enabled = true;
                }
                else
                {
                    buttonLogin.Enabled = false;
                }
            }
            else
            {
                if (tbAccount.Text != "" &&
                    tbPassword.Text != "" &&
                    textBoxCaptchaLogin.Text != "")
                {
                    buttonLogin.Enabled = true;
                }
                else
                {
                    buttonLogin.Enabled = false;
                }
            }
        }

        private void textBoxes_TextChanged(object sender, EventArgs e)
        {
            ValidateLoginTextBoxes();
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FormLogin));
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

        private void FormLogin_Activated(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
        }

        private void FormLogin_Deactivate(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        }

        private void textBoxesLogin_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar < 'А') && !(e.KeyChar > 'ё') && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
                labelErrorMessageLogin.Text = language.GetString("message009");
            }
            else
            {
                labelErrorMessageLogin.Text = "";
            }

        }

    }
}
