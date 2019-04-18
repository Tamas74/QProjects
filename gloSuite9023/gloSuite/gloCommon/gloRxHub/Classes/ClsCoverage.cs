using System;
using System.Collections.Generic;
using System.Text;

namespace gloRxHub
{
    public class ClsCoverage:IDisposable

    {
        #region "Public and Private variable"
        private ClsCoverageHeader objCoverageHeader = new ClsCoverageHeader();
        List<ClsCoverage_AL_Details> objCoverageALDetails = new List<ClsCoverage_AL_Details>();
        List<ClsCoverage_DE_Details> ObjCoverageDEDetails = new List<ClsCoverage_DE_Details>();
        List<ClsCoverage_GL_Details> ObjCoverageGLDetails = new List<ClsCoverage_GL_Details>();
        List<ClsCoverage_QL_Details> ObjCoverageQLDetails = new List<ClsCoverage_QL_Details>();
        List<ClsCoverage_RD_Details> ObjCoverageRDDetails = new List<ClsCoverage_RD_Details>();
        List<ClsCoverage_RS_Details> ObjCoverageRSDetails = new List<ClsCoverage_RS_Details>();
        List<ClsCoverage_SM_Details> ObjCoverageSMDetails = new List<ClsCoverage_SM_Details>();
        List<ClsCoverage_TM_Details> ObjCoverageTMDetails = new List<ClsCoverage_TM_Details>();
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
        public ClsCoverageHeader CoverageHeader
        {
            get { return objCoverageHeader; }
            set { objCoverageHeader = value; }
        }
        public List<ClsCoverage_AL_Details> CoverageALDetails
        {
            get { return objCoverageALDetails; }
            set { objCoverageALDetails = value; }
        }
        public List<ClsCoverage_DE_Details> CoverageDEDetails
        {
            get { return ObjCoverageDEDetails; }
            set { ObjCoverageDEDetails = value; }
        }
        public List<ClsCoverage_GL_Details> CoverageGLDetails
        {
            get { return ObjCoverageGLDetails; }
            set { ObjCoverageGLDetails = value; }
        }
        public List<ClsCoverage_QL_Details> CoverageQLDetails
        {
            get { return ObjCoverageQLDetails; }
            set { ObjCoverageQLDetails = value; }
        }        
        public List<ClsCoverage_RD_Details> CoverageRDDetails
        {
            get { return ObjCoverageRDDetails; }
            set { ObjCoverageRDDetails = value; }
        }
        public List<ClsCoverage_RS_Details> CoverageRSDetails
        {
            get { return ObjCoverageRSDetails; }
            set { ObjCoverageRSDetails = value; }
        }
        public List<ClsCoverage_SM_Details> CoverageSMDetails
        {
            get { return ObjCoverageSMDetails; }
            set { ObjCoverageSMDetails = value; }
        }
        public List<ClsCoverage_TM_Details> CoverageTMDetails
        {
            get { return ObjCoverageTMDetails; }
            set { ObjCoverageTMDetails = value; }
        }

        #endregion "Property Procedures"
    }
    public class ClsCoverageHeader : IDisposable
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
        private string sCoverageRecordType = "";
        private string sCoverageListID = "";
        private string sType = "";
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
        public string CoverageRecordType
        {
            get { return sCoverageRecordType; }
            set { sCoverageRecordType = value; }
        }
        public string CoverageListID
        {
            get { return sCoverageListID; }
            set { sCoverageListID = value; }
        }
        public string Type
        {
            get { return sType; }
            set { sType = value; }
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
    public class ClsCoverage_AL_Details : IDisposable
    {
        #region "Public and Private variable"
        private string sRecordType="";
        private string sChangeIdentifier="";
        private string sCoverageID="";
        private string sServiceID1="";
        private string sServiceID2="";
        private string sDrugReferenceNo="";
        private string sDrugReferenceQualifier="";
        private string sRxNormCode="";
        private string sRxNormQualifier="";
        private string sMinimumAge="";
        private string sMinimumAgeQualifier="";
        private string sMaximumAge="";
        private string sMaximumAgeQualifier="";
        private string sExtractDate="";
        private string sCoverageListID="";
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
            get { return sRecordType; }
            set { sRecordType = value; }
        }
        public string ChangeIdentifier
        {
            get { return sChangeIdentifier; }
            set { sChangeIdentifier = value; }
        }
        public string CoverageID
        {
            get { return sCoverageID; }
            set { sCoverageID = value; }
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
        public string DrugReferenceNo
        {
            get { return sDrugReferenceNo; }
            set { sDrugReferenceNo = value; }
        }
        public string DrugReferenceQualifier
        {
            get { return sDrugReferenceQualifier; }
            set { sDrugReferenceQualifier = value; }
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
        public string MinimumAge
        {
            get { return sMinimumAge; }
            set { sMinimumAge = value; }
        }
        public string MinimumAgeQualifier
        {
            get { return sMinimumAgeQualifier; }
            set { sMinimumAgeQualifier = value; }
        }
        public string MaximumAge
        {
            get { return sMaximumAge; }
            set { sMaximumAge = value; }
        }
        public string MaximumAgeQualifier
        {
            get { return sMaximumAgeQualifier; }
            set { sMaximumAgeQualifier = value; }
        }
        public string ExtractDate
        {
            get { return sExtractDate; }
            set { sExtractDate = value; }

        }
        public string CoverageListID
        {
            get { return sCoverageListID; }
            set { sCoverageListID = value; }

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
    public class ClsCoverage_DE_Details : IDisposable
    {
        #region "Public and Private variable"
                private string sRecordType="";
                private string sChangeIdentifier="";
                private string sCoverageID="";
                private string sServiceID1="";
                private string sServiceID2="";
                private string sDrugReferenceNo="";
                private string sDrugReferenceQualifier="";
                private string sRxNormCode="";
                private string sRxNormQulifier="";
                private string sExtractDate="";
                private string sCoverageListID="";
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
        public string CoverageID
        {
            get { return sCoverageID; }
            set { sCoverageID = value; }
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
        public string DrugReferenceNo
        {
            get { return sDrugReferenceNo; }
            set { sDrugReferenceNo = value; }
        }
        public string DrugReferenceQualifier
        {
            get { return sDrugReferenceQualifier; }
            set { sDrugReferenceQualifier = value; }
        }
        public string RxNormCode
        {
            get { return sRxNormCode; }
            set { sRxNormCode = value; }
        }
        public string RxNormQualifier
        {
            get { return sRxNormQulifier; }
            set { sRxNormQulifier = value; }
        }
        public string ExtractDate
        {
            get { return sExtractDate; }
            set { sExtractDate = value; }

        }
        public string CoverageListID
        {
            get { return sCoverageListID; }
            set { sCoverageListID = value; }

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
    public class ClsCoverage_GL_Details : IDisposable
    {
        #region "Public and Private variable"
         private string sRecordType="";
        private string sChangeIdentifier="";
        private string sCoverageID="";
        private string sServiceID1="";
        private string sServiceID2="";
        private string sDrugReferenceNo="";
        private string sDrugReferenceQualifier="";
        private string sRxNormCode="";
        private string sRxNormQualifier="";
        private string sGender="";
        private string sExtractDate="";
        private string sCoverageListID="";
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
        public string CoverageID
        {
            get { return sCoverageID; }
            set { sCoverageID = value; }
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
        public string DrugReferenceNo
        {
            get { return sDrugReferenceNo; }
            set { sDrugReferenceNo = value; }
        }
        public string DrugReferenceQualifier
        {
            get { return sDrugReferenceQualifier; }
            set { sDrugReferenceQualifier = value; }
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
        public string Gender
        {
            get { return sGender; }
            set { sGender = value; }
        }
        public string ExtractDate
        {
            get { return sExtractDate; }
            set { sExtractDate = value; }

        }
        public string CoverageListID
        {
            get { return sCoverageListID; }
            set { sCoverageListID = value; }

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
    public class ClsCoverage_QL_Details : IDisposable
    {
        #region "Public and Private variable"
        private string sRecordType="";
        private string sChangeIdentifier="";
        private string sCoverageID="";
        private string sServiceID1="";
        private string sServiceID2="";
        private string sDrugReferenceNo="";
        private string sDrugReferenceQualifier="";
        private string sRxNormCode="";
        private string sRxNormQualifier="";
        private string sMaximumAmount="";
        private string sMaximumAmountQualifier="";
        private string sMaximumAmountTime="";
        private string sMaximumAmountStartDate="";
        private string sMaximumAmountEndDate="";
        private string sMaximumAmountUnits="";
        private string sExtractDate="";
        private string sCoverageListID="";
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
        public string CoverageID
        {
            get { return sCoverageID; }
            set { sCoverageID = value; }
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
        public string DrugReferenceNo
        {
            get { return sDrugReferenceNo; }
            set { sDrugReferenceNo = value; }
        }
        public string DrugReferenceQualifier
        {
            get { return sDrugReferenceQualifier; }
            set { sDrugReferenceQualifier = value; }
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
        public string MaximumAmount
        {
            get { return sMaximumAmount; }
            set { sMaximumAmount = value; }
        }
        public string MaximumAmountQualifier
        {
            get { return sMaximumAmountQualifier; }
            set { sMaximumAmountQualifier = value; }

        }
        public string MaximumAmountTime
        {
            get { return sMaximumAmountTime; }
            set { sMaximumAmountTime = value; }
        }
        public string MaximumAmountStartDate
        {
            get { return sMaximumAmountStartDate; }
            set { sMaximumAmountStartDate = value; }
        }
        public string MaximumAmountEndDate
        {
            get { return sMaximumAmountEndDate; }
            set { sMaximumAmountEndDate = value; }
        }
        public string MaximumAmountUnits
        {
            get { return sMaximumAmountUnits; }
            set { sMaximumAmountUnits = value; }
        }
        public string ExtractDate
        {
            get { return sExtractDate; }
            set { sExtractDate = value; }

        }
        public string CoverageListID
        {
            get { return sCoverageListID; }
            set { sCoverageListID = value; }

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
    public class ClsCoverage_RD_Details : IDisposable
    {
        #region "Public and Private variable"
    private string sRecordType="";
        private string sChangeIdentifier="";
        private string sCoverageID="";
        private string sServiceID1="";
        private string sServiceID2="";
        private string sDrugReferenceNo="";
        private string sDrugReferenceQualifier="";
        private string sRxNormCode="";
        private string sRxNormQualifier="";
        private string sResourceLinkType="";
        private string sURL="";
        private string sExtractDate="";
        private string sCoverageListID="";
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
        public string CoverageID
        {
            get { return sCoverageID; }
            set { sCoverageID = value; }
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
        public string DrugReferenceNo
        {
            get { return sDrugReferenceNo; }
            set { sDrugReferenceNo = value; }
        }
        public string DrugReferenceQualifier
        {
            get { return sDrugReferenceQualifier; }
            set { sDrugReferenceQualifier = value; }
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
        public string ResourceLinkType
        {
            get { return sResourceLinkType; }
            set { sResourceLinkType = value; }
        }
        public string URL
        {
            get { return sURL; }
            set { sURL = value; }

        }
        public string ExtractDate
        {
            get { return sExtractDate; }
            set { sExtractDate = value; }

        }
        public string CoverageListID
        {
            get { return sCoverageListID; }
            set { sCoverageListID = value; }

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
    public class ClsCoverage_RS_Details : IDisposable
    {
        #region "Public and Private variable"
           private string sRecordType="";
            private string sChangeIdentifier="";
            private string sCoverageID="";
            private string sResourceLinkType="";
            private string sURL="";
            private string sExtractDate="";
            private string sCoverageListID="";
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
        public string CoverageID
        {
            get { return sCoverageID; }
            set { sCoverageID = value; }
        }
        public string ResourceLinkType
        {
            get { return sResourceLinkType; }
            set { sResourceLinkType = value; }
        }
        public string URL
        {
            get { return sURL; }
            set { sURL = value; }

        }
        public string ExtractDate
        {
            get { return sExtractDate; }
            set { sExtractDate = value; }

        }
        public string CoverageListID
        {
            get { return sCoverageListID; }
            set { sCoverageListID = value; }

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
    public class ClsCoverage_SM_Details : IDisposable
    {
        #region "Public and Private variable"
        private string sRecordType="";
        private string sChangeIdentifier="";
        private string sCoverageID="";
        private string sServiceID1="";
        private string sServiceID2="";
        private string sDrugReferenceNo1="";
        private string sDrugReferenceQualifier1="";
        private string sRxNormCode1="";
        private string sRxNormQulifier1="";
        private string sServiceID3="";
        private string sServiceID4="";
        private string sDrugReferenceNo2="";
        private string sDrugReferenceQualifier2="";
        private string sRxNormCode2="";
        private string sRxNormQulifier2="";
        private string sDrugQualifier="";
        private string sClassID="";
        private string sSubClassID="";
        private string sNoOfDrug="";
        private string sStepOrder="";
        private string sDiagnosisCode="";
        private string sDiagnosisCodeQualifier="";
        private string sExtractDate="";
        private string sCoverageListID="";
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
        public string CoverageID
        {
            get { return sCoverageID; }
            set { sCoverageID = value; }
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
        public string DrugReferenceNo1
        {
            get { return sDrugReferenceNo1; }
            set { sDrugReferenceNo1 = value; }
        }
        public string DrugReferenceQualifier1
        {
            get { return sDrugReferenceQualifier1; }
            set { sDrugReferenceQualifier1 = value; }
        }
        public string RxNormCode1
        {
            get { return sRxNormCode1; }
            set { sRxNormCode1 = value; }
        }
        public string RxNormQualifier1
        {
            get { return sRxNormQulifier1; }
            set { sRxNormQulifier1 = value; }
        }
        public string ServiceID3
        {
            get { return sServiceID3; }
            set { sServiceID3 = value; }
        }
        public string ServiceID4
        {
            get { return sServiceID4; }
            set { sServiceID4 = value; }
        }
        public string DrugReferenceNo2
        {
            get { return sDrugReferenceNo2; }
            set { sDrugReferenceNo2 = value; }
        }
        public string DrugReferenceQualifier2
        {
            get { return sDrugReferenceQualifier2; }
            set { sDrugReferenceQualifier2 = value; }
        }
        public string RxNormCode2
        {
            get { return sRxNormCode2; }
            set { sRxNormCode2 = value; }
        }
        public string RxNormQualifier2
        {
            get { return sRxNormQulifier2; }
            set { sRxNormQulifier2 = value; }
        }
        public string DrugQualifier
        {
            get { return sDrugQualifier; }
            set { sDrugQualifier = value; }

        }
        public string ClassID
        {
            get { return sClassID; }
            set { sClassID = value; }

        }
        public string SubClassID
        {
            get { return sSubClassID; }
            set { sSubClassID = value; }

        }
        public string NoOfDrug
        {
            get { return sNoOfDrug; }
            set { sNoOfDrug = value; }

        }
        public string StepOrder
        {
            get { return sStepOrder; }
            set { sStepOrder = value; }

        }
        public string DiagnosisCode
        {
            get { return sDiagnosisCode; }
            set { sDiagnosisCode = value; }

        }
        public string DiagnosisCodeQualifier
        {
            get { return sDiagnosisCodeQualifier; }
            set { sDiagnosisCodeQualifier = value; }

        } 
        public string ExtractDate
        {
            get { return sExtractDate; }
            set { sExtractDate = value; }

        }
        public string CoverageListID
        {
            get { return sCoverageListID; }
            set { sCoverageListID = value; }

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
    public class ClsCoverage_TM_Details : IDisposable
    {
        #region "Public and Private variable"
            private string sRecordType="";
        private string sChangeIdentifier="";
        private string sCoverageID="";
        private string sServiceID1="";
        private string sServiceID2="";
        private string sDrugReferenceNo="";
        private string sDrugReferenceQualifier="";
        private string sRxNormCode="";
        private string sRxNormQulifier="";
        private string sMessageShort="";
        private string sMessageLong="";
        private string sExtractDate="";
        private string sCoverageListID = "";
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
        public string CoverageID
        {
            get { return sCoverageID; }
            set { sCoverageID = value; }
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
        public string DrugReferenceNo
        {
            get { return sDrugReferenceNo; }
            set { sDrugReferenceNo = value; }
        }
        public string DrugReferenceQualifier
        {
            get { return sDrugReferenceQualifier; }
            set { sDrugReferenceQualifier = value; }
        }
        public string RxNormCode
        {
            get { return sRxNormCode; }
            set { sRxNormCode = value; }
        }
        public string RxNormQualifier
        {
            get { return sRxNormQulifier; }
            set { sRxNormQulifier = value; }
        }
        public string MessageShort
        {
            get { return sMessageShort; }
            set { sMessageShort = value; }
        }
        public string MessageLong
        {
            get { return sMessageLong; }
            set { sMessageLong = value; }

        }
        public string ExtractDate
        {
            get { return sExtractDate; }
            set { sExtractDate = value; }

        }
        public string CoverageListID
        {
            get { return sCoverageListID; }
            set { sCoverageListID = value; }

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
