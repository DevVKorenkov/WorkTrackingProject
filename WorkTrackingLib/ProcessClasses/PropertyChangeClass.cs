using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkTrackingLib
{

    /// <summary>
    /// Класс наследует интерфейс изменения свойств и следит за изменениями свойст
    /// </summary>
    public class PropertyChangeClass : INotifyPropertyChanged
    {
        // событие изменения свойств
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Обработчик события изменения свойств
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
