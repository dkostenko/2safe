﻿using System;
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

namespace TwoSafe.View
{
    public partial class FormPreferences : Form
    {
        public FormPreferences()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
        }

        private void FormPreferences_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Language == "en") { comboBoxLanguages.Text = "English"; }
            else                                              { comboBoxLanguages.Text = "Russian"; }

            comboBoxLanguages.Items.Add("English");
            comboBoxLanguages.Items.Add("Russian");

            textBoxLocation.Text = Properties.Settings.Default.UserFolderPath;
            if (Model.User.isAuthorized())
            {
                textBoxUserName.Text = Controller.ApiTwoSafe.getPersonalData(Properties.Settings.Default.Token)["response"]["personal"]["email"];
                buttonLogin.Enabled = false;
                buttonLogOut.Enabled = true;
            }
            else
            {
                textBoxUserName.Text = "";
                buttonLogOut.Enabled = false;
                buttonLogin.Enabled = true;
            }
            checkBoxLanSync.Checked = Properties.Settings.Default.LanSync;
            checkBoxNotifications.Checked = Properties.Settings.Default.DesktopNotifications;
            checkBoxStart.Checked = Properties.Settings.Default.StartOnSystemStartup;
        }

        /// <summary>
        /// При выборе языка из комбобокса перерисовываем контролы 
        /// и пишем в настройки язык, но настройки не сохраняем
        /// </summary>
        private void comboBoxLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)comboBoxLanguages.SelectedItem == "Russian")
            {
                Properties.Settings.Default.Language = "ru-RU";
                ChangeLanguage("ru-RU");
                this.Refresh();  
            }
            if ((string)comboBoxLanguages.SelectedItem == "English")
            {
                Properties.Settings.Default.Language = "en";
                ChangeLanguage("en");
                this.Refresh();
            }
        }

        /// <summary>
        /// Обработчик нажатия "OK"
        /// Сохраняет текущие настройки и закрывает форму
        /// </summary>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Close();
        }
        /// <summary>
        /// Обработчик нажатия "Применить"
        /// Сохраняет текущие настройки
        /// </summary>
        private void buttonApply_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Обработчик нажатия "Отмена"
        /// Перегружает настройки на те которые были до открытия формы или до нажатия "Применить" или "ОК"
        /// Закрывает форму
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            this.Close();
        }
        
        /// <summary>
        /// Метод изменяющий надписи на контролах во время исполнения
        /// </summary>
        /// <param name="lang"> Язык </param>
        private void ChangeLanguage(string lang)
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FormPreferences));
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
        /// Обработчик нажатия на кнопку "Поменять расположение папки"
        /// </summary>
        private void buttonMoveLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != "")
            {
                Properties.Settings.Default.UserFolderPath = fbd.SelectedPath;
                textBoxLocation.Text = fbd.SelectedPath;
            }
        }
        
        /// <summary>
        /// Изменение настроек при изменении галки
        /// </summary>
        private void checkBoxNotifications_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DesktopNotifications = checkBoxNotifications.Checked;
        }

        /// <summary>
        /// Изменение настроек при изменении галки
        /// </summary>
        private void checkBoxStart_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.StartOnSystemStartup = checkBoxStart.Checked;
        }

        /// <summary>
        /// Изменение настроек при изменении галки
        /// </summary>
        private void checkBoxLanSync_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.LanSync = checkBoxLanSync.Checked;
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Token = "";
            Properties.Settings.Default.Save();
            textBoxUserName.Clear();
            buttonLogOut.Enabled = false;
            buttonLogin.Enabled = true;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            FormLogin loginForm = new FormLogin();
            loginForm.Show();
        }


        private void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Model.User.isAuthorized())
            {
                textBoxUserName.Text = Controller.ApiTwoSafe.getPersonalData(Properties.Settings.Default.Token)["response"]["personal"]["email"];
                buttonLogin.Enabled = false;
                buttonLogOut.Enabled = true;
            }
            else
            {
                textBoxUserName.Text = "";
                buttonLogOut.Enabled = false;
                buttonLogin.Enabled = true;
            }
        }

    }
}
