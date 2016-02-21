using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace wpfPass
{
    [ValueConversion(typeof(string), typeof(string))]

    public class PasswordConvertClass : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string pass = PasswordProtect.DecryptStringAES(value.ToString(), MainWindow.Secret);
            return pass;
        }
        public object ConvertBack(object value, Type targetType, object parameter, 
                System.Globalization.CultureInfo culture)
        {
            string pass = PasswordProtect.EncryptStringAES(value.ToString(), MainWindow.Secret);
            return pass;
        }
    }
}

