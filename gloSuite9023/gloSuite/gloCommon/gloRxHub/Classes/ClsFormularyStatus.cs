using System;
using System.Collections.Generic;
using System.Text;

namespace gloRxHub
{
   public class ClsFormularyStatus :IDisposable
   {

       #region "Prvate and public variables"
       private ClsFormularyStatusHeader objFormularyStatusHeader = new ClsFormularyStatusHeader();
       List<ClsFormularyStatusDetails> objFormularyStatusDetails = new List<ClsFormularyStatusDetails>();
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

       public ClsFormularyStatusHeader FormularyStatusHeader
       {
           get { return objFormularyStatusHeader; }
           set { objFormularyStatusHeader = value; }
       }


       public List<ClsFormularyStatusDetails> FormularyStatusDetails
       {
           get { return objFormularyStatusDetails; }

           set { objFormularyStatusDetails = value; }
       }
       #endregion "Property Procedures"
    }

    public class ClsFormularyStatusHeader : IDisposable
    {
        #region "Public and Private Variables"

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
        private string sFormularyRecordType = "";
        private string sFormularyID = "";
        private string sFormularyName = "";
        private string sNLPrescriptionBrandFStatus = "";
        private string sNLPrescriptionGenericFStatus = "";
        private string sNLBrandCounterFStatus = "";
        private string sNLGenericCounterFStatus = "";
        private string sNLSuppliesFStatus = "";
        private string sRealativeCostLimit = "";
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

        #region "Property procedures for RxH_FormularyStatus_Header Table"
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
        public string FormularyRecordType
        {
            get { return sFormularyRecordType; }
            set { sFormularyRecordType = value; }
        }

        public string FormularyID
        {
            get { return sFormularyID; }
            set { sFormularyID = value; }
        }

        public string FormularyName
        {
            get { return sFormularyName; }
            set { sFormularyName = value; }
        }


        public string NLPrescriptionBrandFStatus
        {
            get { return sNLPrescriptionBrandFStatus; }
            set { sNLPrescriptionBrandFStatus = value; }
        }


        public string NLPrescriptionGenericFStatus
        {
            get { return sNLPrescriptionGenericFStatus; }
            set { sNLPrescriptionGenericFStatus = value; }
        }


        public string NLBrandCounterFStatus
        {
            get { return sNLBrandCounterFStatus; }
            set { sNLBrandCounterFStatus = value; }
        }


        public string NLGenericCounterFStatus
        {
            get { return sNLGenericCounterFStatus; }
            set { sNLGenericCounterFStatus = value; }
        }

        public string NLSuppliesFStatus
        {
            get { return sNLSuppliesFStatus; }
            set { sNLSuppliesFStatus = value; }
        }
        public string RealativeCostLimit
        {
            get { return sRealativeCostLimit; }
            set { sRealativeCostLimit = value; }
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

        #endregion "Property procedures for RxH_FormularyStatus_Header Table"
    }

    public class ClsFormularyStatusDetails : IDisposable
    {

        #region "Public and private variables"

        private string sRecordType = "";
        private string sChangeIdentifier = "";
        private string sServiceID1 = "";
        //private string sServiceID2 = "";
        private string sDrugRefNo = "";
        private string sDrugRefQualifier = "";
        private string sRxNormCode = "";
        private string sRxNormQualifier = "";
        private string sFormularyStatus = "";
        private string sRelativeCost = "";
        private string sExtractDate = "";
        private string sFormularyID = "";
        private string sField1 = "";
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


        #region "Property procedures for RxH_FormularyStatus_DTL tables"

        public string RecordType
        {
            get { return sRecordType; }
            set { sRecordType = value; }

        }
        public string ChangeIdentifier
        {
             get{ return sChangeIdentifier; }
            set { sChangeIdentifier = value; }

        }

        public string ServiceID1
        {
            get { return sServiceID1; }
            set { sServiceID1 = value; }

        }

        public string ServiceID2
        {
            get { return sServiceID1; }
            set { sServiceID1 = value; }

        }

        public string DrugRefNo
        {
            get { return sDrugRefNo; }
            set { sDrugRefNo = value; }

        }

        public string DrugRefQualifier
        {
            get { return sDrugRefQualifier; }
            set { sDrugRefQualifier = value; }

        }

        public string RxNormCode
        {
            get { return sRxNormCode; }
            set { sRxNormCode = value; }

        }

        public string RxNormQualifier
        {
            get { return sRxNormQualifier; }
            set { sRxNormQualifier = value; }

        }

        public string FormularyStatus
        {
            get { return sFormularyStatus; }
            set { sFormularyStatus = value; }

        }

        public string RelativeCost
        {
            get { return sRelativeCost; }
            set { sRelativeCost = value; }

        }

        public string ExtractDate
        {
            get { return sExtractDate; }
            set { sExtractDate = value; }

        }
        public string FormularyID
        {
            get { return sFormularyID; }
            set { sFormularyID = value; }

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


        #endregion "Property procedures"
    }
}
