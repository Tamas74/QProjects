using System;
using System.Collections.Generic;
using System.Text;

namespace gloRxHub
{
    public class ClsAlternate: IDisposable
    {
      
        #region "Public and Private variable"
        private ClsAlternateHeader objAlternateHeader = new ClsAlternateHeader();
        List<ClsAlternateDetails> objAlternateDetails = new List<ClsAlternateDetails>();

        
        private bool disposedValue = false;
        #endregion "Public and Private variable"

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }
                // TODO: free managed resources when explicitly called 
            }

            // TODO: free shared unmanaged resources 
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern. 
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above. 
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    
        #region "Property Procedures"
        public ClsAlternateHeader AlternateHeader
        {
            get { return objAlternateHeader; }
            set { objAlternateHeader = value; }
        
        }

       public List<ClsAlternateDetails> AlternateDetails
        {
            get { return objAlternateDetails; }
            set { objAlternateDetails = value; }
        
        }
       #endregion "Property Procedures"

         

   
    }
    public class ClsAlternateHeader : IDisposable
    {

        #region "Public and Private variable"

        private string sRecordType = "";
        private string sVersionNo = "";
        private string sSenderID = "";
        private string sSenderPwd = "";
        private string sReceiverID = "";
        private string sSourceName = "";
        private string sTransmissionCtrlNo = "";
        private string sTransmissionDate = "";
        private string sTransmissionTime = "";
        private string sTransmissionFile = "";
        private string sTransmissionAction = "";
        private string sExtractDate = "";
        private string sFileType = "";
        private string sAlternativeRecordType="";
        private string sAlternativeID = "";
        private string sListAction = "";
        private string sListEffectiveDate = "";

        private bool disposedValue = false;
        #endregion "Public and Private variable"

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }
                // TODO: free managed resources when explicitly called 
            }

            // TODO: free shared unmanaged resources 
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern. 
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above. 
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region "Property Procedures"
        public string RecordType
        {
            get { return sRecordType; }
            set { sRecordType = value; }
        }
        public string VersionNo
        {
            get { return sVersionNo; }
            set { sVersionNo = value; }
        }
        public string SenderID
        {
            get { return sSenderID; }
            set { sSenderID = value; }
        }
        public string SenderPwd
        {
            get { return sSenderPwd; }
            set { sSenderPwd = value; }
        }
        public string ReceiverID
        {
            get { return sReceiverID; }
            set { sReceiverID = value; }
        }
        public string SourceName
        {
            get { return sSourceName; }
            set { sSourceName = value; }
        }
        public string TransmissionCtrlNo
        {
            get { return sTransmissionCtrlNo; }
            set { sTransmissionCtrlNo = value; }
        }
        public string TransmissionDate
        {
            get { return sTransmissionDate; }
            set { sTransmissionDate = value; }
        }
        public string TransmissionTime
        {
            get { return sTransmissionTime; }
            set { sTransmissionTime = value; }
        }
        public string TransmissionFile
        {
            get { return sTransmissionFile; }
            set { sTransmissionFile = value; }
        }
        public string TransmissionAction
        {
            get { return sTransmissionAction; }
            set { sTransmissionAction = value; }
        }
        public string ExtractDate
        {
            get { return sExtractDate; }
            set { sExtractDate = value; }
        }
        public string FileType
        {
            get { return sFileType; }
            set { sFileType = value; }
        }
        public string AlternativeRecordType
        {
            get { return sAlternativeRecordType; }
            set { sAlternativeRecordType = value; }
        }
        public string AlternativeID
        {
            get { return sAlternativeID; }
            set { sAlternativeID = value; }
        }
        public string ListAction
        {
            get { return sListAction; }
            set { sListAction = value; }
        }
        public string ListEffectiveDate
        {
            get { return sListEffectiveDate; }
            set { sListEffectiveDate = value; }
        }

        #endregion "Property Procedures"
        
    }
    public class ClsAlternateDetails : IDisposable
    {

        #region "Public and Private variable"

       private string sRecordType = "";
      private string sChangeIDentifier="";
      private string sServiceID1="";
      private string sServiceID2 = "";
      private string sDrugRefNo="";
      private string sDrugRefQualifierSource = "";
      private string sRxNormCodeSource="";
      private string sRxNormQulifierSource = "";
      private string sServiceIDAlternative="";
      private string sServiceID3 = "";
      private string sDrugRefNoAlternative="";
      private string sDrugRefQualifierAlternative = "";
      private string sRxNormCodeAlternative="";
      private string sRxNormQualifierAlternative = "";
      private string sPreferenceLevel="";
      private string sExtractDate = "";
      private string sAlternativeID="";
      private string  sField1="";
      private string sField2 = "";


        private bool disposedValue = false;
        #endregion "Public and Private variable"

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }
                // TODO: free managed resources when explicitly called 
            }

            // TODO: free shared unmanaged resources 
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern. 
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above. 
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region "Property Procedures"
        public string RecordType
        {
            get { return sRecordType; }
            set { sRecordType = value; }
        }
        public string ChangeIDentifier
        {
            get { return sChangeIDentifier; }
            set { sChangeIDentifier = value; }
        }
        public string ServiceID1
        {
            get { return sServiceID1; }
            set { sServiceID1 = value; }
        }
        public string ServiceID2
        {
            get { return sServiceID2; }
            set { sServiceID2 = value; }
        }
        public string DrugRefNo
        {
            get { return sDrugRefNo; }
            set { sDrugRefNo = value; }
        }
        public string DrugRefQualifierSource
        {
            get { return sDrugRefQualifierSource; }
            set { sDrugRefQualifierSource = value; }
        }
        public string RxNormCodeSource
        {
            get { return sRxNormCodeSource; }
            set { sRxNormCodeSource = value; }
        }
        public string RxNormQulifierSource
        {
            get { return sRxNormQulifierSource; }
            set { sRxNormQulifierSource = value; }
        }
        public string ServiceIDAlternative
        {
            get { return sServiceIDAlternative; }
            set { sServiceIDAlternative = value; }
        }
        public string ServiceID3
        {
            get { return sServiceID3; }
            set { sServiceID3 = value; }
        }
        public string DrugRefNoAlternative
        {
            get { return sDrugRefNoAlternative; }
            set { sDrugRefNoAlternative = value; }
        }
        public string DrugRefQualifierAlternative
        {
            get { return sDrugRefQualifierAlternative; }
            set { sDrugRefQualifierAlternative = value; }
        }
        public string RxNormCodeAlternative
        {
            get { return sRxNormCodeAlternative; }
            set { sRxNormCodeAlternative = value; }
        }
        public string RxNormQualifierAlternative
        {
            get { return sRxNormQualifierAlternative; }
            set { sRxNormQualifierAlternative = value; }
        }
        public string PreferenceLevel
        {
            get { return sPreferenceLevel; }
            set { sPreferenceLevel = value; }
        }
        public string ExtractDate
        {
            get { return sExtractDate; }
            set { sExtractDate = value; }

        }
        public string AlternativeID
        {
            get { return sAlternativeID; }
            set { sAlternativeID = value; }
        }
        public string Field1
        {
            get { return sField1; }
            set { sField1 = value; }
        }
        public string Field2
        {
            get { return sField2; }
            set { sField2 = value; }
        }
       
        #endregion "Property Procedures"




    }
}
