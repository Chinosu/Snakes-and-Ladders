using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SnakesAndLadders
{
    public class WinnerConverter : IMultiValueConverter
    {
        /// <summary>
        ///  Adds winner to a string
        /// </summary>
        /// <remarks>
        ///  E.g. if we have "Bill" -> "Winner: Bill"
        /// </remarks>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is string Player
                && targetType == typeof(string))
            {
                return $"Winner: {Player}";
            }
            throw new InvalidCastException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
