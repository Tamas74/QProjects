using System.Windows.Data;
using System.Windows;
using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Collections;

namespace gloUIControlLibrary.Classes
{

    #region "PharmacyType"
    class PharmacyTypeConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string returned = string.Empty;
            if (value != null)
            {
                string sValue = value.ToString().ToLower();

                switch (sValue)
                {
                    case "a":
                        returned = "Any";
                        break;
                    case "r":
                        returned = "Retail";
                        break;
                    case "m":
                        returned = "Mail";
                        break;
                    case "s":
                        returned = "Specialty";
                        break;
                    case "l":
                        returned = "Long Term Care";
                        break;

                }
            }
            return returned;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string returned = string.Empty;


            if (value is string)
            {
                string sValue = (string)value;

                switch (sValue)
                {
                    case "Any":
                        returned = "a";
                        break;
                    case "Retail":
                        returned = "r";
                        break;
                    case "Mail":
                        returned = "m";
                        break;
                    case "Specialty":
                        returned = "s";
                        break;
                    case "Long Term Care":
                        returned = "l";
                        break;
                }
            }

            return returned;
        }
    }

    class PharmacyTypeVisibilityConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            Visibility returned = Visibility.Visible;
            if (value != null)
            {
                string sValue = value.ToString().ToLower();

                switch (sValue)
                {
                    case "a":
                    case "r":
                    case "m":
                    case "s":
                    case "l":
                        returned = Visibility.Visible;
                        break;
                    case "":
                        returned = Visibility.Collapsed;
                        break;

                }
            }
            return returned;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;

            if (value is Visibility)
            {
                Visibility vValue = (Visibility)value;

                if (vValue == Visibility.Visible)
                { sReturned = "Shown"; }
                else
                { sReturned = "Not Shown"; }

            }

            return sReturned;
        }
    }

    #endregion

    #region "Out Of Pocket"

    class OutOfPocketRangeEndConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string returned = string.Empty;
            if (value != null)
            {
                string sValue = value.ToString().ToLower();

                switch (sValue)
                {
                    case "":
                        returned = "no upper range";
                        break;
                    default:
                        returned = sValue;
                        break;
                }
            }
            return returned;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string returned = string.Empty;


            if (value is string)
            {
                string sValue = (string)value;

                switch (sValue)
                {
                    case "no upper range":
                        returned = "";
                        break;
                }
            }

            return returned;
        }
    }

    class OutOfPocketRangeStartConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string returned = string.Empty;
            if (value != null)
            {
                string sValue = value.ToString().ToLower();

                switch (sValue)
                {
                    case "":
                        returned = "No lower range";
                        break;
                    default:
                        returned = sValue;
                        break;
                }
            }
            return returned;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string returned = string.Empty;


            if (value is string)
            {
                string sValue = (string)value;

                switch (sValue)
                {
                    case "No lower range":
                        returned = "";
                        break;
                }
            }

            return returned;
        }
    }

    class FormularyStatusConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;
            if (value != null)
            {
                string sValue = value.ToString().ToLower();
                switch (sValue)
                {
                    case "U":
                    case "-1":
                        sReturned = "Unknown";
                        break;
                    case "0":
                        sReturned = "Non Reimbursable";
                        break;
                    case "1":
                        sReturned = "Non Formulary";
                        break;
                    case "2":
                        sReturned = "On Formulary/Non Preferred";
                        break;
                    default:
                        sReturned = "Preferred Level - " + sValue;
                        break;
                }
            }
            return sReturned;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;


            if (value is string)
            {
                string sValue = (string)value;

                switch (sValue)
                {
                    case "U":
                    case "Unknown":
                        sReturned = "-1";
                        break;
                    case "Non Reimbursable":
                        sReturned = "0";
                        break;
                    case "Non Formulary":
                        sReturned = "1";
                        break;
                    case "On Formulary/Non Preferred":
                        sReturned = "2";
                        break;

                }
            }

            return sReturned;
        }
    }

    

    #endregion

    

    class MultiVisibilityInverseConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility returned = Visibility.Visible;
            if (values != null)
            {
                if (values.Any(p => p is string && Convert.ToString(p).Trim().Length > 0))
                { returned = Visibility.Collapsed; }
            }
            return returned;

        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        { return null; }
    }

    class MultiVisibilityConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility returned = Visibility.Collapsed;

            if (values != null)
            {
                if (values.Any(p => p is string && Convert.ToString(p).Trim().Length > 0))
                { returned = Visibility.Visible; }
            }
            
            return returned;

        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        { return null; }
    }

    class SingleVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility returned = Visibility.Collapsed;

            if (value != null && value is string && Convert.ToString(value).Trim().Length > 0)
            { returned = Visibility.Visible; }

            return returned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    class NullVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility returned = Visibility.Collapsed;

            if (value !=null)
            { returned = Visibility.Visible; }

            return returned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
   
    class BolleanVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility returned = Visibility.Collapsed;
            if (value != null)
            {
                if (value is Boolean && Convert.ToBoolean(value) == true)
                { returned = Visibility.Visible; }
            }
            return returned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

#region "Coverage Converters"
    class MinimumAgeConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;
            if (value != null)
            {
                string sValue = value.ToString().ToLower();
                switch (sValue)
                {
                    case "U":
                    case "-1":
                        sReturned = "Unknown";
                        break;
                    case "0":
                        sReturned = "Non Reimbursable";
                        break;
                    case "1":
                        sReturned = "Non Formulary";
                        break;
                    case "2":
                        sReturned = "On Formulary/Non Preferred";
                        break;
                    default:
                        sReturned = "Preferred Level - " + sValue;
                        break;
                }
            }
            return sReturned;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;


            if (value is string)
            {
                string sValue = (string)value;

                switch (sValue)
                {
                    case "U":
                    case "Unknown":
                        sReturned = "-1";
                        break;
                    case "Non Reimbursable":
                        sReturned = "0";
                        break;
                    case "Non Formulary":
                        sReturned = "1";
                        break;
                    case "On Formulary/Non Preferred":
                        sReturned = "2";
                        break;

                }
            }

            return sReturned;
        }
    }

    class PercentCoPayRateConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {                       
            double dReturned = 0;

            if (value != null && value is string)
            {
                double dValue = 0;                
                if (double.TryParse(Convert.ToString(value), out dValue)) { dReturned = dValue * 100; }
            }

            return dReturned;
            
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    class MinMaxCoPayConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;
            if (value != null)
            {
                string sValue = value.ToString().ToLower();
                switch (sValue)
                {
                    case "":
                        sReturned = "0";
                        break;
                    default:
                        sReturned = sValue;
                        break;
                }
            }
            return sReturned;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;


            if (value is string)
            {
                string sValue = (string)value;

                switch (sValue)
                {
                    case "U":
                    case "Unknown":
                        sReturned = "-1";
                        break;
                    case "Non Reimbursable":
                        sReturned = "0";
                        break;
                    case "Non Formulary":
                        sReturned = "1";
                        break;
                    case "On Formulary/Non Preferred":
                        sReturned = "2";
                        break;

                }
            }

            return sReturned;
        }
    }

    class CopayListTypeConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;

            if (value != null)
            {
                string sValue = value.ToString().ToLower();
                switch (sValue.ToUpper())
                {
                    case "DS":
                        sReturned = "Drug Specific Copay ";
                        break;
                    case "SL":
                        sReturned = "Summary Level Copay ";
                        break;
                    default:
                        sReturned = sValue;
                        break;
                }
            }          
            return sReturned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    class DaysConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;
            if (value != null)
            {
                string sValue = value.ToString().ToLower();

                switch (sValue.ToUpper())
                {
                    case "D":
                        sReturned = "Days";
                        break;
                    case "Y":
                        sReturned = "Years";
                        break;
                    default:
                        sReturned = sValue;
                        break;
                }
            }            
            return sReturned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    class RecordTypeVisibilityConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility returned = Visibility.Collapsed;

            if (values != null)
            {
                if (values.All(p => p is string && Convert.ToString(p).Trim().Length > 0))
                {
                    if (values.Any(p => Convert.ToString(p).ToUpper().Trim() == "AL") && values.Any(p => Convert.ToString(p) == "AgeLimitWrapPanel"))
                    { returned = Visibility.Visible; }
                    else if (values.Any(p => Convert.ToString(p).ToUpper().Trim() == "GL") && values.Any(p => Convert.ToString(p) == "GenderWrapPanel"))
                    { returned = Visibility.Visible; }
                    else if (values.Any(p => Convert.ToString(p).ToUpper().Trim() == "DE") && values.Any(p => Convert.ToString(p) == "DrugExclusionWrapPanel"))
                    { returned = Visibility.Visible; }
                    else if (values.Any(p => Convert.ToString(p).ToUpper().Trim() == "PA") && values.Any(p => Convert.ToString(p) == "PriorAuthorizationWrapPanel"))
                    { returned = Visibility.Visible; }

                    else if (values.Any(p => Convert.ToString(p).ToUpper().Trim() == "TM") && values.Any(p => Convert.ToString(p) == "LongMessageWrapPanel"))
                    { returned = Visibility.Visible; }
                    else if (values.Any(p => Convert.ToString(p).ToUpper().Trim() == "QL") && values.Any(p => Convert.ToString(p) == "QuantityLimitWrapPanel"))
                    { returned = Visibility.Visible; }
                    else if (values.Any(p => Convert.ToString(p).ToUpper().Trim() == "ST") && values.Any(p => Convert.ToString(p) == "StepTherapyWrapPanel"))
                    { returned = Visibility.Visible; }
                }
                else if (values.All(p => p is string) && values.Any(p => Convert.ToString(p).ToUpper().Trim() == "TM") && values.Any(p => Convert.ToString(p) == "ShortMessageWrapPanel"))
                {
                    //Specialized case for Short Text Message
                    if (!values.All(p => Convert.ToString(p).Any())) { returned = Visibility.Visible; }
                }
            }
                                    
            return returned;

        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        { return null; }
    }

    class GenderConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;
            if (value != null && value is string)
            {
                string sValue = value.ToString().ToLower();

                switch (sValue.ToUpper())
                {
                    case "1":
                        sReturned = "Male";
                        break;
                    case "2":
                        sReturned = "Female";
                        break;
                    default:
                        sReturned = sValue;
                        break;

                }                
            }
            return sReturned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    class QuanLimitMaxAmtConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;
            if (value != null)
            {
                string sValue = value.ToString().ToLower();

                switch (sValue.ToUpper())
                {
                    case "DL":
                        sReturned = "Dollar Amount";
                        break;
                    case "DS":
                        sReturned = "Days Supply";
                        break;
                    case "FL":
                        sReturned = "Fills";
                        break;
                    case "QY":
                        sReturned = "Quantity";
                        break;
                    default:
                        sReturned = sValue;
                        break;

                }
            }

            
            return sReturned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    class QuanLimitMaxTimePeriodConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;
            if (value != null)
            {
                string sValue = value.ToString().ToLower();

                switch (sValue.ToUpper())
                {
                    case "CM":
                        sReturned = "Calender Month";
                        break;
                    case "CQ":
                        sReturned = "Calender Quarter";
                        break;
                    case "CY":
                        sReturned = "Calender Year";
                        break;
                    case "DY":
                        sReturned = "Days";
                        break;
                    case "LT":
                        sReturned = "Lifetime";
                        break;
                    case "PD":
                        sReturned = "Dispensing";
                        break;
                    case "SP":
                        sReturned = "Specific Date Range";
                        break;
                    default:
                        sReturned = sValue;
                        break;

                }
            }         
            return sReturned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    class ResourceTypeConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;
            if (value != null)
            {
                string sValue = value.ToString().ToLower();

                switch (sValue.ToLower())
                {
                    case "agelimits":
                        sReturned = "Age Limits";
                        break;
                    case "coapy":
                        sReturned = "Copay";
                        break;
                    case "coverageexclusion":
                        sReturned = "Product Coverage Exclusion";
                        break;
                    case "formulary":
                        sReturned = "Formulary";
                        break;
                    case "generalinfo":
                        sReturned = "General Info";
                        break;
                    case "genderlimits":
                        sReturned = "Gender Limits";
                        break;
                    case "priorauthorization":
                        sReturned = "Prior Authorization";
                        break;
                    case "quantitylimits":
                        sReturned = "Quantity Limits";
                        break;
                    case "steptherapy":
                        sReturned = "Step Therapy";
                        break;
                    default:
                        sReturned = sValue;
                        break;

                }
            }
           
            return sReturned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    class CopayTermConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sReturned = string.Empty;
            if (value != null)
            {
                string sValue = value.ToString().ToLower();

                switch (sValue.ToUpper())
                {
                    case "F":
                        sReturned = "Flat Copay";
                        break;
                    case "P":
                        sReturned = "Percent Copay";
                        break;                    
                    default:
                        sReturned = sValue;
                        break;
                }
            }

            return sReturned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

#endregion

    class AlternativesVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility vReturned = Visibility.Collapsed;

            if (value != null && value is DataTable)
            {
                if (((DataTable)value).Rows.Count > 0)
                { vReturned = Visibility.Visible; }
            }

         
            return vReturned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    class EnumerableContainsAnyVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility vReturned = Visibility.Collapsed;
            
            if (value != null && value is IEnumerable)
            {
                IEnumerable enumerator = (IEnumerable)value;
                
                foreach (var item in enumerator)
                {
                    vReturned = Visibility.Visible;
                    break;
                }
            }


            return vReturned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
