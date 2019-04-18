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

namespace gloUIControlLibrary.Classes.ClaimStatus
{

    public enum MessageType
    {
        Error = 1,
        Message = 2,
        StatusCategory = 3,
        Status = 4,
        None = 0
    }

    public class ClaimStatusInfo
    {

        private string payerId = "";
        private string payerName = "";
        private string claimNumber = "";
        private string claimStatus = "";
        private MessageType messageTypeInfo = MessageType.None;



        public string PayerId
        { get { return payerId; } }

        public string PayerName
        { get { return payerName; } }

        public string ClaimNumber
        { get { return claimNumber; } }

        public string ClaimStatus
        { get { return claimStatus; } }
        
        public MessageType MessageTypeInfo
        { get { return messageTypeInfo; } }

        public ClaimStatusInfo(string PayerId, string PayerName, string ClaimNumber, string Status, MessageType Messagetypeinfo)
        {
            this.payerId = PayerId;
            this.payerName = PayerName;
            this.claimNumber = ClaimNumber;
            this.claimStatus = Status;
            this.messageTypeInfo = Messagetypeinfo;
        }

    }

    public class ClaimStatusInfoObservable : IDisposable
    {
        private ObservableCollection<ClaimStatusInfo> _ClaimStatusInfoList;
        public ObservableCollection<ClaimStatusInfo> ClaimStatusInfoList
        {
            get { return _ClaimStatusInfoList; }
            set { _ClaimStatusInfoList = value; }
        }

        public ClaimStatusInfoObservable()
        {
            this._ClaimStatusInfoList = new ObservableCollection<ClaimStatusInfo>();
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
                    if (this._ClaimStatusInfoList != null)
                    {
                        this._ClaimStatusInfoList.Clear();
                        this._ClaimStatusInfoList = null;
                    }
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

    public class StatusInfoVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string returned = string.Empty;

            if (value != null && Convert.ToInt32(value) == Convert.ToInt32(MessageType.Error))
            { returned = "ICO/RuleError.ico"; }
            else if (value != null && Convert.ToInt32(value) == Convert.ToInt32(MessageType.Message))
            { returned = "ICO/RuleInformation.ico"; }
            else if (value != null && Convert.ToInt32(value) == Convert.ToInt32(MessageType.StatusCategory))
            { returned = "ICO/RuleWarning.ico"; }
            else if (value != null && Convert.ToInt32(value) == Convert.ToInt32(MessageType.Status))
            { returned = "ICO/RuleWarning.ico"; }

            return returned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public class StatusTextVisibilityConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string returned = string.Empty;

            if (value != null && Convert.ToInt32(value) == Convert.ToInt32(MessageType.Error))
            { returned = "Error"; }
            else if (value != null && Convert.ToInt32(value) == Convert.ToInt32(MessageType.Message))
            { returned = "Failed Rule Evaluation"; }
            else if (value != null && Convert.ToInt32(value) == Convert.ToInt32(MessageType.StatusCategory))
            { returned = "Information"; }
            else if (value != null && Convert.ToInt32(value) == Convert.ToInt32(MessageType.Status))
            { returned = "Warning"; }

            return returned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
