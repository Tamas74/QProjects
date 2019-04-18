using System;
using System.Collections.Generic;
using System.Text;

namespace gloRxHub
{
    public class ClsCopay:IDisposable

    {
        #region "Public and Private variable"

         private ClsCopayHeader objCopayHeader = new ClsCopayHeader();
        List<ClsCopay_DS_Details> objCopayDSDetails = new List<ClsCopay_DS_Details>();
        List<ClsCopay_SL_Details> objCopaySLDetails = new List<ClsCopay_SL_Details>();

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

            public ClsCopayHeader CopayHeader
           {
               get{return objCopayHeader;}
               set{objCopayHeader=value;}
           
           }
            public List<ClsCopay_DS_Details> CopayDSDetails

           {
               get{return objCopayDSDetails;}
               set{objCopayDSDetails=value;}
           
           }
        public List<ClsCopay_SL_Details> CopaySLDetails
        {
            get { return objCopaySLDetails; }
            set { objCopaySLDetails = value; }
        }
          #endregion "Property Procedures"
    }
    public class ClsCopayHeader:IDisposable
       {
             #region "Public and Private variable"

         private string sRecordType="";
         private string sVersionNo="";
         private string sSenderID="";
         private string sSenderPwd="";
         private string sReceiverID="";
         private string sSourceName="";
         private string sTransmissionCtrlNo="";
         private string sTransmissionDate="";
         private string sTransmissionTime="";
         private string sTransmissionFile="";
         private string sTransmissionAction="";
         private string sExtractDate="";
         private string sFileType="";
         private string sCoPayRecordType="";
         private string sCoPayListID="";
         private string sListType="";
         private string sListAction="";
         private string sListEffectiveDate="";
               
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

      #region "Property procedures"
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

           public string CoPayRecordType
        {
            get { return sCoPayRecordType; }
            set { sCoPayRecordType = value; }
        }
        public string CoPayListID
        {
            get { return sCoPayListID; }
            set { sCoPayListID = value; }
        }

           public string ListType
        {
            get { return sListType; }
            set { sListType = value; }
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
           
             #endregion "Property procedures"
       }
    public class ClsCopay_DS_Details:IDisposable
       {
       
          #region "Public and Private variable"

         private string sRecordType="";
         private string sChangeIdentifier="";
         private string sCoPayID="";
         private string sServiceID1="";
         private string sServiceID2="";
         private string sDrugReferenceNo="";
         private string sDrugReferenceQualifier="";
         private string sRxNormCode="";
         private string sRxNormQualifier="";
         private string sPharmacyType="";
         private string sFlatCoPayAmount="";
         private string sPercentCoPayRate="";
         private string sFirstCoPayTerm="";
         private string sMinimumCoPay="";
         private string sMaximumCoPay="";
         private string sDaysSupplyPerCoPay="";
         private string sCoPayTier="";
         private string sMaximumCoPayTier="";
         private string sExtractDate="";
         private string sCoPayListID="";
         private string sField1="";
         private string sField2="";

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
               get{return sRecordType;}
               set{sRecordType=value;}
           
           }
            public string ChangeIdentifier

           {
               get{return sChangeIdentifier;}
               set{sChangeIdentifier=value;}
           
           }

            public string CoPayID
           {
               get{return CoPayID;}
               set{sCoPayID=value;}
           
           }

            public string ServiceID1
           {
               get{return sServiceID1;}
               set{sServiceID1=value;}
           
           }

            public string ServiceID2
           {
               get{return sServiceID2;}
               set{sServiceID2=value;}
           
           }

            public string DrugReferenceNo
           {
               get{return sDrugReferenceNo;}
               set{sDrugReferenceNo=value;}
           
           }

            public string DrugReferenceQualifier
           {
               get{return sDrugReferenceQualifier;}
               set{sDrugReferenceQualifier=value;}
           
           }

            public string RxNormCode
           {
               get{return sRxNormCode;}
               set{sRxNormCode=value;}
           
           }

            public string RxNormQualifier
           {
               get{return sRxNormQualifier;}
               set{sRxNormQualifier=value;}
           
           }

            public string PharmacyType
           {
               get{return sPharmacyType;}
               set{sPharmacyType=value;}
           
           }

            public string FlatCoPayAmount
           {
               get{return sFlatCoPayAmount;}
               set{sFlatCoPayAmount=value;}
           
           }

            public string PercentCoPayRate
           {
               get{return sPercentCoPayRate;}
               set{sPercentCoPayRate=value;}
           
           }
                      public string  FirstCoPayTerm
           {
               get{return  sFirstCoPayTerm;}
               set{ sFirstCoPayTerm=value;}
           
           }
           public string MinimumCoPay
           {
               get{return sMinimumCoPay;}
               set{sMinimumCoPay=value;}
           
           }
           public string MaximumCoPay
           {
               get{return sMaximumCoPay;}
               set{sMaximumCoPay=value;}
           
           }
           public string DaysSupplyPerCoPay
           {
               get{return sDaysSupplyPerCoPay;}
               set{sDaysSupplyPerCoPay=value;}
           
           }
           public string CoPayTier
           {
               get{return sCoPayTier;}
               set{sCoPayTier=value;}
           
           }
             public string MaximumCoPayTier
           {
               get{return sMaximumCoPayTier;}
               set{sMaximumCoPayTier=value;}
           
           }
           
           public string ExtractDate
           {
               get{return sExtractDate;}
               set{sExtractDate=value;}
           
           }
           public string CoPayListID
           {
               get{return sCoPayListID;}
               set{sCoPayListID=value;}
           
           }
           public string Field1
           {
               get{return sField1;}
               set{sField1=value;}
           
           }
           public string Field2
           {
               get{return sField2;}
               set{sField2=value;}
           
           }       



       #endregion "Property Procedures"
               
       
       }
    public class ClsCopay_SL_Details : IDisposable
    {
      
        #region "Public and Private variable"
            private string sRecordType="";
           private string sChangeIdentifier="";
           private string sCoPayID="";
           private string sFormularyStatus="";
           private string sProductType="";
           private string sPharmacyType="";
           private string sOutOfPocketRangeStart="";
           private string sOutOfPocketRangeEnd="";
           private string sFlatCoPayAmount="";
           private string sPercentCoPayRate="";
           private string sFirstCoPayTerm="";
           private string sMinimumCoPay="";
           private string sMaximumCoPay="";
           private string sDaysSupplyPerCoPay="";
           private string sCoPayTier="";
           private string sMaximumCoPayTier="";
           private string sExtractDate="";
           private string sCoPayListID="";
           private string sField1="";
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
        public string ChangeIdentifier
        {
            get { return sChangeIdentifier; }
            set { sChangeIdentifier = value; }

        }
        public string CoPayID
        {
            get { return CoPayID; }
            set { sCoPayID = value; }

        }
        public string FormularyStatus
        {
            get { return sFormularyStatus; }
            set { sFormularyStatus = value; }

        }
        public string ProductType
        {
            get { return sProductType; }
            set { sProductType = value; }

        }
        public string PharmacyType
        {
            get { return sPharmacyType; }
            set { sPharmacyType = value; }

        }
        public string OutOfPocketRangeStart
        {
            get { return sOutOfPocketRangeStart; }
            set { sOutOfPocketRangeStart = value; }

        }
        public string OutOfPocketRangeEnd
        {
            get { return sOutOfPocketRangeEnd; }
            set { sOutOfPocketRangeEnd = value; }

        }
        public string FlatCoPayAmount
        {
            get { return sFlatCoPayAmount; }
            set { sFlatCoPayAmount = value; }

        }
        public string PercentCoPayRate
        {
            get { return sPercentCoPayRate; }
            set { sPercentCoPayRate = value; }

        }
        public string FirstCoPayTerm
        {
            get { return sFirstCoPayTerm; }
            set { sFirstCoPayTerm = value; }

        }
        public string MinimumCoPay
        {
            get { return sMinimumCoPay; }
            set { sMinimumCoPay = value; }

        }
        public string MaximumCoPay
        {
            get { return sMaximumCoPay; }
            set { sMaximumCoPay = value; }

        }
        public string DaysSupplyPerCoPay
        {
            get { return sDaysSupplyPerCoPay; }
            set { sDaysSupplyPerCoPay = value; }

        }
        public string CoPayTier
        {
            get { return sCoPayTier; }
            set { sCoPayTier = value; }

        }
        public string MaximumCoPayTier
        {
            get { return sMaximumCoPayTier; }
            set { sMaximumCoPayTier = value; }

        }
        public string ExtractDate
        {
            get { return sExtractDate; }
            set { sExtractDate = value; }

        }
        public string CoPayListID
        {
            get { return sCoPayListID; }
            set { sCoPayListID = value; }

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
