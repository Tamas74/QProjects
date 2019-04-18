using System;
using System.Windows.Data;

namespace gloUIControlLibrary.Classes.ICD10
{
    class clsXMLSpaceConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = (string)value;
            return sReturned.Replace("\r\n", "").Trim();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //class CodeVisibilityConverter : IValueConverter
    //{
    //    object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        string returned = string.Empty;

    //        if (value != null && Convert.ToString(value) == "1")
    //        { returned = "Resources\\OnFormulary.pnl"; }
    //        else
    //        { returned = "Resources\\ICD 10.ico"; }

    //        return returned;
    //    }

    //    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        return null;
    //    }
    //}
}
