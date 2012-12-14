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
        }

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

        private void buttonApply_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }
        
        
        /// <summary>
        /// Метод изменяющий контролы во время исполнения
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


    }
}
