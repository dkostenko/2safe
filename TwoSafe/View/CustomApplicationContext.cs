using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace TwoSafe.View
{   
    public class CustomApplicationContext : ApplicationContext
    {
        /// <summary>
        /// Список компонентов подлежащих уничтожению при закрытии приложения
        /// </summary>
        private System.ComponentModel.IContainer components;
        
        /// <summary>
        /// Иконка приложения в трее
        /// </summary>
        private NotifyIcon notifyIcon;
        
        /// <summary>
        /// Текст всплывающей подсказки по умолчанию
        /// </summary>
        private static readonly string DefaultTooltip = "2safe";

        /// <summary>
        /// Контекстное меню иконки
        /// </summary>
        private ContextMenu contextMenu;

        /// <summary>
        /// Управляющий элемент "Выход" контекстного меню иконки 
        /// </summary>
        private MenuItem menuItemExit;
        
        /// <summary>
        /// Управляющий элемент "Настройки" контекстного меню иконки
        /// </summary>
        private MenuItem menuItemProperties;

        /// <summary>
        /// Форма с настройками
        /// </summary>
        View.FormPreferences prefsForm;
        
        /// <summary>
        /// Форма логина
        /// </summary>
        View.FormLogin loginForm;

        /// <summary>
        /// Конструктор CustomApplicationContext без параметров
        /// </summary>
        public CustomApplicationContext()
        {            
            InitializeContext();

            // TODO: добавить синхронизацию и что то еще


            // Проверки
            runUserChecks();
            

        }

        /// <summary>
        /// Запуск проверок условий необходимых для запуска программы
        /// наличие интернета не является одной из них
        /// при невыполнении условий программа должна предоставлять адекватное решение
        /// </summary>
        private void runUserChecks()
        {

            // Если программа запускается первый раз - в настройках пусто
            // смотрим - есть ли папка по дефолту 
            if (Properties.Settings.Default.UserFolderPath == "")
            {
                // если до этого существовала дефолтовая папка, то просо используем ее 
                if (Directory.Exists(@"C:\Users\" + Environment.UserName + @"\2safe"))
                {
                    Properties.Settings.Default.UserFolderPath = @"C:\Users\" + Environment.UserName + @"\2safe";
                    Properties.Settings.Default.Save();
                }
                // если дефолтовая папка не существовала, надо предложить пользователю 3 опции
                // 1. закрыть программу
                // 2. создать дефолтную папку в дефолтном месте
                // 3. создать папку со своим именем с которой будет все синхронизироваться
                else
                {
                    CreateFolderDialog folderDialog = new CreateFolderDialog();
                    folderDialog.ShowDialog();
                    switch (folderDialog.DialogResult)
                    {
                        case DialogResult.Abort:
                            // close the application
                            ExitThread();
                            break;
                        case DialogResult.OK:
                            // create default folder
                            Directory.CreateDirectory(@"C:\Users\" + Environment.UserName + @"\2safe");
                            break;
                        case DialogResult.Yes:
                            // show dialog for choosing a custom destination of the folder

                            break;

                    }
                }
            }
            
            /*
            if (Model.User.userFolderExists()) // Существует папка 2safe юзера
            {
                if (!Model.User.isAuthorized()) // Если с токеном все плохо - выпадает окно авторизации
                {
                    loginForm = new FormLogin();
                    loginForm.ShowDialog();
                    runUserChecks();
                }
            }
            else // Нет папки
            {
                MessageBox.Show("Папка " + Properties.Settings.Default.UserFolderPath + " не существует");
                runUserChecks();
            }*/
        }

        /// <summary>
        /// Инициализатор иконки и контекстного меню
        /// </summary>
        private void InitializeContext()
        {
            // Определение компонентов подлежащих уничтожению при закрытии приложения
            components = new System.ComponentModel.Container();

            // Инициализация контекстного меню и управляющих элементов
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItemExit = new MenuItem();
            this.menuItemProperties = new MenuItem();
            
            // Инициализация полей управляющего элемента "Настройки". Подписка на обработчик нажатия мыши 
            this.menuItemProperties.Index = 0;
            this.menuItemProperties.Click += new System.EventHandler(this.propertiesItem_Click);

            // Инициализация полей управляющего элемента "Выход". Подписка на обработчик нажатия мыши
            this.menuItemExit.Index = 1;
            this.menuItemExit.Click += new System.EventHandler(this.exitItem_Click);

            // Выставление язвка в меню иконки
            SetMenuItemsLanguage(Properties.Settings.Default.Language);
            
            // Добавление управляющих элементов в контекстное меню
            this.contextMenu.MenuItems.AddRange(
                    new MenuItem[] { this.menuItemProperties, 
                                     new MenuItem("-"),   // Этот элемент отображается как разделительная черта
                                     this.menuItemExit });

            // Инициализация иконки
            notifyIcon = new NotifyIcon(components)
            {
                ContextMenu = contextMenu,
                Icon = TwoSafe.Properties.Resources.green_icon,
                Text = DefaultTooltip,
                Visible = true
            };
            
            // Подписка на обработчик события двойного щелчка мыши на иконке приложения
            notifyIcon.DoubleClick += this.notifyIcon_DoubleClick;
            // Подписка на обработчик события изменения настроек
            Properties.Settings.Default.PropertyChanged += Default_PropertyChanged;
        }



        /// <summary>
        /// Метод, изменяющий язык управляющих элементов контекстного меню иконки
        /// </summary>
        /// <param name="language"> Язык в формате "ru-RU" или "en" </param>
        private void SetMenuItemsLanguage(string language)
        {
            switch (language)
            {
                case "en":
                    menuItemExit.Text = "Exit";
                    menuItemProperties.Text = "Preferences";
                    break;
                case "ru-RU":
                    menuItemExit.Text = "Выход";
                    menuItemProperties.Text = "Настройки";
                    break;
            }
        }
        
        // Обработчик изменения настроек
        private void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetMenuItemsLanguage(Properties.Settings.Default.Language);
            Model.User.token = Properties.Settings.Default.Token;
            Model.User.userFolderPath = Properties.Settings.Default.UserFolderPath;
        }

        /// <summary>
        /// Обработчик события щелчка мыши на управляющем элементе "Свойства". Открывает форму с настройками
        /// </summary>
        private void propertiesItem_Click(object sender, System.EventArgs e)
        {
            prefsForm = new FormPreferences();
            prefsForm.Show();
        }

        /// <summary>
        /// Обработчик события двойного щелчка мыши на иконке приложения. Открывает папку 2safe процессом explorer.exe
        /// </summary>
        private void notifyIcon_DoubleClick(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @"\");
        }

        /// <summary>
        /// Обработчик события щелчка мыши на управляющем элементе "Выход"
        /// </summary>
        private void exitItem_Click(object sender, EventArgs e)
        {
            ExitThread();
        }

        /// <summary>
        /// When the application context is disposed, dispose things like the notify icon.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) { components.Dispose(); }
        }

        /// <summary>
        /// Закрытие главного потока
        /// </summary>
        protected override void ExitThreadCore()
        {
            // Если показываются какие-то формы, то они закрываются
            if (prefsForm != null) { prefsForm.Close(); }
            if (loginForm != null) { loginForm.Close(); }

            notifyIcon.Visible = false; // это удалит иконку из трея
            base.ExitThreadCore();
            //Application.Exit();
        }
    }
}
