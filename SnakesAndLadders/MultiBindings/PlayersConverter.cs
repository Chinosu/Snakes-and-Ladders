using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SnakesAndLadders
{
    public class PlayersConverter : IMultiValueConverter
    {
        /// <summary>
        ///  Converts a string[] of players to a string of "... vs ..."
        /// </summary>
        /// <remarks>
        ///  E.g. if we have a string[] of "Bill", "Joe", "Jake" -> "Bill vs Joe vs Jake"
        /// </remarks>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is string[] Players
                && targetType == typeof(string))
            {
                var result = Players[0];
                for (int i = 1; i < Players.Length; i++)
                {
                    result += $" vs {Players[i]}";
                }
                return result;
            }
            throw new InvalidCastException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
