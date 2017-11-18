using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Mirage.Converters
{
    /// <summary>
    /// Represents a converter class that converts a boolean value to an opacity
    /// </summary>
    public class BooleanToOpacityConverter : IValueConverter
    {
        /// <summary>
        /// Convert a boolean value to opacity
        /// </summary>
        /// <param name="value">Boolean value</param>
        /// <param name="targetType">Target type</param>
        /// <param name="parameter">Parameter to converter</param>
        /// <param name="language">Language for converter</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double opacity = 0;
            if (value is bool && value.Equals(true))
            {
                opacity = 1.0;
            }
            return opacity;
        }
        /// <summary>
        /// Convert an opacity value to a boolean value
        /// </summary>
        /// <param name="value">Opacity value</param>
        /// <param name="targetType">Target Type</param>
        /// <param name="parameter">Parameter to the converter</param>
        /// <param name="language">Language of the converter</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
