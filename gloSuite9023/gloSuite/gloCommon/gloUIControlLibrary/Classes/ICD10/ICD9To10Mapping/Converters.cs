using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace gloUIControlLibrary.Classes.ICD10.ICD9To10Mapping
{
    public class ScenarioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string sReturned = string.Empty;

            if (value != null && value is Int32)
            {
                if ((Int32)value == 0)
                {
                    sReturned = "ICD 9 code maps to";
                }
                else
                { sReturned = "Scenario " + (Int32)value; }
            }

            return sReturned;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WithConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            string sReturned = string.Empty;

            if (value != null && value is bool)
            {
                bool argValue = (bool)value;

                if (!argValue)
                { sReturned = "With"; }
                else
                { sReturned = "ICD10 Code"; }
            }

            return sReturned;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ICD10ORConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            Visibility vReturned = Visibility.Collapsed;

            if (value != null && value is bool)
            {
                bool argValue = (bool)value;

                if (!argValue)
                { vReturned = Visibility.Visible; }
            }

            return vReturned;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
