using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NewWorkTracking.Converters
{
    /// <summary>
    /// Converter output to representation of access level values 
    /// </summary>
    class AccsessConverter : IValueConverter
    {
        /// <summary>
        /// The method takes a number and returns a text value corresponding to this number 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                switch (value)
                {
                    case 0:
                        value = "Просмотр";
                        break;
                    case 1:
                        value = "Управление";
                        break;
                    case 2:
                        value = "Администратор";
                        break;
                }

                return value;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
