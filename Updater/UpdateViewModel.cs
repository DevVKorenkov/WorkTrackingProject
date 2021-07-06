using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Updater
{
    class UpdateViewModel : Change
    {
        #region Поля

        /// <summary>
        /// Поле сообщения окна обновления
        /// </summary>
        private string update;
        /// <summary>
        /// Поле запуска прогресс-бара
        /// </summary>
        private bool indeterminate;
        /// <summary>
        /// Поле значения прогресс-бара
        /// </summary>
        private int pbcount;

        private int maxValue;

        #endregion

        #region Свойства

        /// <summary>
        /// Свойство сообщений окна обнвления
        /// </summary>
        public string Update
        {
            get { return update; }
            set { update = value; OnPropertyChanged(nameof(Update)); }
        }
        /// <summary>
        /// Свойство работы прогресс-бара
        /// </summary>
        public bool Indeterminate
        {
            get { return indeterminate; }
            set { indeterminate = value; OnPropertyChanged(nameof(Indeterminate)); }
        }
        /// <summary>
        /// Свойство заполнения прогресс-бара
        /// </summary>
        public int Pbcount
        {
            get { return pbcount; }
            set { pbcount = value; OnPropertyChanged(nameof(Pbcount)); }
        }        
        /// <summary>
        /// Свойство значения максимального числа прогресс-бара
        /// </summary>
        public int MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; OnPropertyChanged(nameof(MaxValue)); }
        }

        #endregion

        #region Конструкторы

        public UpdateViewModel()
        {
            Indeterminate = true;

            UpdateModel.MesEvent += message;

            UpdateModel.StopProgress += FillingProgBar;

            //UpdateModel.ProgEvent += ChangeProgBarValue;

            StartUpdate();          
        }

        #endregion

        #region Методы

        /// <summary>
        /// Метод запуска обновления PassYourWork
        /// </summary>
        private async void StartUpdate()
        {
            // Инициализация экземпляра класса класса обновления
            UpdateModel updateModel = new UpdateModel();

            var path = $@"{new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName}\Work_Tracking.exe";

            // Действие при удачном обновлении
            if (await Task.Run(() => updateModel.UpdateProgramm()))
            {
                // Запуск основной программы
                Process.Start(path);

                // Закрытие основного приложения
                Process.GetCurrentProcess().Kill();
            }
            // Действие при неудачном обновлнеии
            else
            {
                Process.Start(path);

                // Закрытие основного приложения
                Process.GetCurrentProcess().Kill();
            }
        }

        /// <summary>
        /// Обработчик события окна обновления
        /// </summary>
        /// <param name="msg"></param>
        void message(string msg)
        {
            Update = msg;
        }

        /// <summary>
        /// Метод заполнения прогресс-бара
        /// </summary>
        /// <param name="x"></param>
        void FillingProgBar(string x)
        {
            Indeterminate = false;

            for (Pbcount = 5; Pbcount > 0; Pbcount--)
            {
                Update = $"{x}. Программа будет перезапущена через {Pbcount}";
                Thread.Sleep(1000);
            }
        }

        #endregion
    }
}
