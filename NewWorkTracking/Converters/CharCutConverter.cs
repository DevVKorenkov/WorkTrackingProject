using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NewWorkTracking.Converters
{
    /// <summary>
    /// Converter to remove invalid characters 
    /// </summary>
    class CharCutConverter : IValueConverter
    {
        private int _minStringLenght = 10;
        private int _maxStringLenght = 12;

        char[] symbols = new char[] { ' ', ',', '.', '?', '/', '\\', '-', '=', '_', '+', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '<', '>', '~', ';', ':' };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
                value = value.ToString().Trim(symbols);

            return value;
        }

        /// <summary>
        /// The method takes a string and removes unnecessary characters from it 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tempSymbols = @"^[a-zA-Zа-яА-Я0-9]+$";
            //var tempLenght = @"^(.{10}|.{12})$";

            if (!string.IsNullOrWhiteSpace(value.ToString()) && !Regex.IsMatch(value.ToString(), tempSymbols))
            {
                Message.Show("Ошибка заполнения", "Инвентарный номер должен содержать только буквы и цифры. Специальные символы будут удалены. Проверте правильность инвентарного номера.", MessageBoxButton.OK);

                foreach (var t in symbols)
                {
                    if (value.ToString().Contains(t))
                    {
                        if (value != null)
                            value = value.ToString().Remove(value.ToString().IndexOf(t), 1);
                    }
                }
            }

            if (/*!Regex.IsMatch(value.ToString(), tempLenght)*/ value.ToString().Length < _minStringLenght || value.ToString().Length > _maxStringLenght)
            {
                Message.Show("Ошибка заполнения", "Инвентарный номер должен содержать от 10 до 12 символов. Введите инвентарный номер снова.", MessageBoxButton.OK);

                value = null;

                //if (value != null && value.ToString().Length > 12)
                //    //value = value.ToString().Remove(_maxStringLenght);
                //    value = null;
            }

            return value;
        }
    }
}
