using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections;
using System.Xml.Linq;

namespace gloUIControlLibrary.Classes.SmartDX
{
    public class clsSmartDXDisplay:IDisposable 
    {
        private string _DisplayName;
        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }

        private string _Type;
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private Int64 _Id;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private Int64 _ICDId;
        public Int64 ICDId
        {
            get { return _ICDId; }
            set { _ICDId = value; }
        }

        private Int16 _SortId;
        public Int16 SortId
        {
            get { return _SortId; }
            set { _SortId = value; }
        }

        #region "IDisposable Support"
        // To detect redundant calls
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            this.disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        //Protected Overrides Sub Finalize()
        //    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        //    Dispose(False)
        //    MyBase.Finalize()
        //End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class clsSmartDX : IDisposable
    {
        private ObservableCollection<clsSmartDXDisplay> _TreatmentList;
        public ObservableCollection<clsSmartDXDisplay> TreatmentList
        {
            get { return _TreatmentList; }
            set { _TreatmentList = value; }
        }
        #region "IDisposable Support"
        // To detect redundant calls
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                // TODO: set large fields to null.
            }
            this.disposedValue = true;
        }

        // TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        //Protected Overrides Sub Finalize()
        //    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        //    Dispose(False)
        //    MyBase.Finalize()
        //End Sub

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }


    public class CodeVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string returned = string.Empty;

            if (value != null && Convert.ToString(value) == "CPT")
            { returned = "ICO/CPT.ico"; }
            else if (value != null && Convert.ToString(value) == "Drugs")
            { returned = "ICO/Drugs.ico"; }
            else if (value != null && Convert.ToString(value) == "Flowsheet")
            { returned = "ICO/Flowsheet.ico"; }
            else if (value != null && Convert.ToString(value) == "Order Templates")
            { returned = "ICO/OrderTemplates.ico"; }
            else if (value != null && Convert.ToString(value) == "Orders and Results")
            { returned = "ICO/OrderandResults.ico"; }
            else if (value != null && Convert.ToString(value) == "Patient Education")
            { returned = "ICO/PatientEducation.ico"; }
            else if (value != null && Convert.ToString(value) == "Referral Letter")
            { returned = "ICO/RefferalLetter.ico"; }
            else if (value != null && Convert.ToString(value) == "Tags")
            { returned = "ICO/Tags.ico"; }
            else if (value != null && Convert.ToString(value) == "ICD-9")
            { returned = "ICO/ICD09.ico"; }
            else if (value != null && Convert.ToString(value) == "ICD-10")
            { returned = "ICO/ICD10GalleryGreen.ico"; }
            return returned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }


}
