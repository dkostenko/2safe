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
    class CustomApplicationContext : ApplicationContext
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
        /// Путь к файлу иконки
        /// </summary>
        private static readonly string IconFileName = "green_icon.ico";
        
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
        View.FormPreferences prefs;

        /// <summary>
        /// Конструктор CustomApplicationContext без параметров
        /// </summary>
        public CustomApplicationContext()
        {
            InitializeContext();
            // TODO: добавить синхронизацию и что то еще
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
            this.menuItemProperties.Text = "Настройки";
            this.menuItemProperties.Click += new System.EventHandler(this.propertiesItem_Click);


            // Инициализация полей управляющего элемента "Выход". Подписка на обработчик нажатия мыши
            this.menuItemExit.Index = 1;
            this.menuItemExit.Text = "Выход";
            this.menuItemExit.Click += new System.EventHandler(this.exitItem_Click);

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
        }

        /// <summary>
        /// Обработчик события щелчка мыши на управляющем элементе "Свойства". Открывает форму с настройками
        /// </summary>
        private void propertiesItem_Click(object sender, System.EventArgs e)
        {
            prefs = new FormPreferences();
            prefs.Show();
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
            if (prefs != null) { prefs.Close(); }
            
            notifyIcon.Visible = false; // это удалит иконку из трея
            base.ExitThreadCore();
        }
    }
}
