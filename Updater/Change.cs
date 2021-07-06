using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    class Change : INotifyPropertyChanged
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
