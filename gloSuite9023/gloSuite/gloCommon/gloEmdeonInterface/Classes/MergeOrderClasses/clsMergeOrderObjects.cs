using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace gloEmdeonInterface.Classes.MergeOrderClasses
{

    #region "Business Objects"

    #region "Order"
    public class clsGloOrder : Dictionary<Int32, clsGloTest>, IDisposable
    {

        #region "Properties"
        public UInt64 OrderID { get; set; }
        public UInt64 OrderNoID { get; set; }
        public bool IsAcknowledged { get; set; }        
        public UInt64 VisitID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ExternalCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProviderName { get; set; }
        public string  OrderStatus { get; set; }

        [DefaultValue(false)]
        public bool IsMerged { get; set; }
        #endregion

        #region "Constructors"

        #region "Supplementary Constructors"

        #region "By Object"
        public clsGloOrder(clsGloOrder Order) 
            : 
            this
            (
            Order.OrderID, 
            Order.OrderNoID, 
            Order.IsAcknowledged, 
            Order.VisitID, 
            Order.ExternalCode, 
            Order.TransactionDate, 
            Order.OrderDate, 
            Order.ProviderName, 
            Order.OrderStatus, 
            Order.IsMerged, 
            Order.Values
            ) { }

        public clsGloOrder(DataRow DataRow)
            : 
            this
            (
            Convert.ToUInt64(DataRow["labom_OrderID"]),
            Convert.ToUInt64(DataRow["labom_OrderNoID"]),
            Convert.ToBoolean(DataRow["bIsClosed"]),
            Convert.ToUInt64(DataRow["labom_VisitID"]),
            Convert.ToString(DataRow["labom_ExternalCode"]),
            Convert.ToDateTime(DataRow["labom_TransactionDate"]),            
            Convert.ToDateTime(DataRow["labom_OrderDate"]),
            Convert.ToString(DataRow["ProviderName"]),
            Convert.ToString(DataRow["OrderStatus"]),
            false, 
            null
            ) { }
        #endregion

        #region "Null"
        public clsGloOrder()
            : 
            this
            (
            0, 
            0, 
            false, 
            0, 
            string.Empty, 
            System.DateTime.Today, 
            System.DateTime.Today, 
            string.Empty, 
            string.Empty, 
            false, 
            null
            ) { }
        #endregion

        //public clsGloOrder(UInt64 OrderID, UInt64 OrderNoID)
        //    : this(OrderID, OrderNoID, false, 0, string.Empty,System.DateTime.Today, System.DateTime.Today, string.Empty, string.Empty, false, null) { }
        #endregion

        #region "Primary Constructor"
        public clsGloOrder
            (
            UInt64 OrderID,
            UInt64 OrderNoID,
            bool IsAcknowledged,
            UInt64 VisitID,
            string ExternalCode,
            DateTime TransactionDate,
            DateTime OrderDate,
            string ProviderName,
            string OrderStatus,
            bool IsMerged,
            Dictionary<Int32, clsGloTest>.ValueCollection xGloTestValues
            )
            : 
            base()
        {
            try
            {
                if (xGloTestValues != null && xGloTestValues.Any())
                {
                    foreach (clsGloTest xGloTest in xGloTestValues.AsEnumerable())
                    { if (!this.ContainsKey(xGloTest.Key)) { this.Add(xGloTest.Key, clsGloTest.CloneTest(xGloTest)); } }
                }

                this.OrderID = OrderID;
                this.OrderNoID = OrderNoID;
                this.IsAcknowledged = IsAcknowledged;
                this.IsMerged = IsMerged;
                this.ProviderName = ProviderName;
                this.VisitID = VisitID;
                this.OrderStatus = OrderStatus;
                this.TransactionDate = TransactionDate;
                this.OrderDate = OrderDate;
                this.ExternalCode = ExternalCode;
            }
            catch (Exception ex) { throw ex; }                                   
        }
        #endregion

        #endregion

        #region "Functions"
        public static clsGloOrder Clone(clsGloOrder OrderToClone)
        {
            return new clsGloOrder(OrderToClone);

            //return new clsGloOrder(OrderToClone.OrderID, OrderToClone.OrderNoID, OrderToClone.VisitID, OrderToClone.ExternalCode, OrderToClone.TransactionDate, OrderToClone.OrderDate, OrderToClone.IsMerged, OrderToClone.Values);
        }

        public int GetMaxTestNo() { return base.Values.Max(Test => Test.LineNo); }
        #endregion

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
                    if (this.Any())
                    {
                        foreach (clsGloTest xGloTest in base.Values) { xGloTest.Dispose(); }
                        base.Clear();
                    }

                     OrderID = 0;
                     OrderNoID = 0;
                     IsAcknowledged = false;
                     VisitID = 0;                     
                     ExternalCode = null;                     
                     ProviderName = null;
                     OrderStatus = null;
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
    #endregion

    #region "Test"
    public class clsGloTest : Dictionary<Int32, clsGloResult>, IDisposable
    {
        #region "Attributes"

        UInt64 nTestID = 0;
        UInt64 nTargetOrderID = 0;
        bool bIsShifting = false;

        bool bHasAttachmentsForRemoval = false;
        bool bHasAttachmentForInsertion = false;

        private Dictionary<UInt64, clsGloResultAttachment> _DictionaryDocs;
        //private List<xGloDiagnosis> _listDiagnosis;

        #endregion

        #region "Properties"
        public UInt64 OrderID { get; set; }
        public Int32 LineNo { get; set; }
        public string TestName { get; set; }

        public string TestStatus { get; set; }
        public string SpecimenSource { get; set; }
        public string LOINCCode { get; set; }

        public bool HasAttachmentsForRemoval
        {
            get { return bHasAttachmentsForRemoval; }
            set { bHasAttachmentsForRemoval = value; }
        }
        public bool HasAttachmentsForInsertion
        {
            get { return bHasAttachmentForInsertion; }
            set { bHasAttachmentForInsertion = value; }
        }
        
        [DefaultValue("")]
        public string Instruction { get; set; }

        [DefaultValue("")]
        public string Comment { get; set; }

        [DefaultValue(false)]
        public bool HasTemplate { get; set; }

        public Dictionary<UInt64, clsGloResultAttachment> DictionaryDocuments
        {
            get { return this._DictionaryDocs; }
            set { this._DictionaryDocs = value; }
        }
               
        public Int32 Key
        {
            get
            {
                string sTestNameHashCode = Convert.ToString(this.TestName.Trim(Convert.ToChar(" ")).ToLower().GetHashCode());
                string sTestIDHashCode = Convert.ToString(TestID.GetHashCode());

                return (sTestNameHashCode + sTestIDHashCode).GetHashCode();
            }
        }

        public UInt64 TestID
        {
            get { return nTestID; }
            set { nTestID = value; }
        }

        public bool IsShifting
        {
            get { return bIsShifting; }
            set { bIsShifting = value; }
        }

        public UInt64 TargetOrderID
        {
            get { return nTargetOrderID; }
            set { nTargetOrderID = value; }
        }

        #endregion

        #region "Constructors"

        #region "Supplementary Constructors"
        
        #region "By DataRow"
        public clsGloTest(DataRow row)
            : this(
            Convert.ToString(row["labotd_TestName"]),
            Convert.ToUInt64(row["labotd_OrderID"]),
            Convert.ToUInt64(row["labotd_TestID"]),
            Convert.ToInt32(row["labotd_LineNo"]),
            Convert.ToString(row["labotd_Instruction"]),
            Convert.ToBoolean(row["Template"]),
            Convert.ToString(row["labotd_TestStatus"]),
            Convert.ToString(row["labotd_SpecimenSource"]),
            Convert.ToString(row["labotd_LOINCCode"])) { }

        #endregion

        #region "By Arguments"
        public clsGloTest
            (
            string TestName, 
            UInt64 OrderID, 
            UInt64 TestID, 
            Int32 LineNo, 
            string Instruction, 
            bool HasTemplate, 
            string TestStatus, 
            string SpecimenSource, 
            string LOINCCode
            )
            : 
            this
            (
            TestName, 
            OrderID, 
            TestID, 
            LineNo, 
            Instruction, 
            HasTemplate, 
            TestStatus, 
            SpecimenSource, 
            LOINCCode, 
            0, 
            false, 
            null, 
            null
            ) { }
        #endregion

        #region "By Object"
        public clsGloTest(ref clsGloTest xGloTest)
            : this(
            xGloTest.TestName,
            xGloTest.OrderID,
            xGloTest.TestID,
            xGloTest.LineNo,
            xGloTest.Instruction,
            xGloTest.HasTemplate,
            xGloTest.TestStatus,
            xGloTest.SpecimenSource,
            xGloTest.LOINCCode,
            xGloTest.TargetOrderID,
            xGloTest.IsShifting,
            xGloTest.Values,
            xGloTest.DictionaryDocuments.Values) { }
        #endregion

        #endregion

        #region "Primary Constructor"
        public clsGloTest
            (
            string TestName,
            UInt64 OrderID,
            UInt64 TestID,
            Int32 LineNo,
            string Instruction,
            bool HasTemplate,
            string TestStatus,
            string SpecimenSource,
            string LOINCCode,
            UInt64 TargetOrderID,
            bool IsShifting,
            Dictionary<Int32, clsGloResult>.ValueCollection xGloResultValues,
            Dictionary<UInt64, clsGloResultAttachment>.ValueCollection xGloAttachmentValues
            )
            : base()
        {
            try
            {
                this.DictionaryDocuments = new Dictionary<UInt64, clsGloResultAttachment>();
                //this.ListDiagnosis = new List<xGloDiagnosis>();

                this.TestName = TestName;
                this.OrderID = OrderID;

                this.TestID = TestID;
                this.LineNo = LineNo;
                
                this.Instruction = Instruction;
                this.HasTemplate = HasTemplate;
                
                this.TestStatus = TestStatus;
                this.SpecimenSource = SpecimenSource;
                this.LOINCCode = LOINCCode;

                this.TargetOrderID = TargetOrderID;
                this.IsShifting = IsShifting;

                if (xGloAttachmentValues != null && xGloAttachmentValues.Any())
                {
                    foreach (clsGloResultAttachment _xGloAttachment in xGloAttachmentValues)
                    {
                        if (!this.DictionaryDocuments.ContainsKey(_xGloAttachment.AttachmentID))
                        { DictionaryDocuments.Add(_xGloAttachment.AttachmentID, _xGloAttachment); }
                    }
                }

                if (xGloResultValues != null && xGloResultValues.Any())
                {
                    foreach (clsGloResult _xGloResult in xGloResultValues)
                    {
                        if (!base.ContainsKey(_xGloResult.GetHashCode()))
                        { base.Add(_xGloResult.GetHashCode(), clsGloResult.CloneResult(_xGloResult)); }
                    }
                }

             

               
            }
            catch (Exception ex) { throw ex; }            
        }
        #endregion

        #endregion

        #region "Functions"
        public static clsGloTest CloneTest(clsGloTest _xGloTest)
        { return new clsGloTest(ref _xGloTest); }

        public UInt16 GetMaxResultNo()
        {
            try { return base.Values.Max(Result => Result.TargetResultNumber); }
            catch (Exception ex) { throw ex; }            
        }

        public IEnumerable<clsGloResultAttachment> GetRemovalAttachments()
        {
            try { return this.DictionaryDocuments.Values.Where(p => p.IsDeleting); }
            catch (Exception ex) { throw ex; }                        
        }

        public IEnumerable<clsGloResultAttachment> GetShiftingAttachments()
        {
            try { return this.DictionaryDocuments.Values.Where(p => p.IsShifting); }
            catch (Exception ex) { throw ex; }            
        }

        public IEnumerable<clsGloResult> GetUpdatingResults()
        {
            try { return base.Values.Where(p => p.IsUpdating); }
            catch (Exception ex) {throw ex;}
        }

        #endregion

        #region "IDisposable"
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                base.Clear();
                this.OrderID = 0;
                this.LineNo = 0;
                this.TestName = null;

                if (this.DictionaryDocuments != null)
                {
                    if (this.DictionaryDocuments.Any())
                    {
                        foreach (clsGloResultAttachment xAttachment in this.DictionaryDocuments.Values)
                        { xAttachment.Dispose(); }
                    }
                    DictionaryDocuments = null;
                }
            }

            // Free any unmanaged objects here. 
            //
            disposed = true;
        }
        #endregion


    }
    #endregion

    #region "Result"
    public class clsGloResult : Dictionary<Int32, clsGloResultDetail>, IDisposable
    {

        #region "Attributes"

        UInt64 nOrderID = 0;       
        bool bIsUpdating = false;
        string sResultName = string.Empty;
        UInt16 nTargetResultNumber = 0;

        #endregion

        #region "Properties"
        public UInt64 TestID { get; set; }
        public UInt16 ResultNumber { get; set; }

        public UInt64 OrderID { get { return this.nOrderID; } set { this.nOrderID = value; } }        
        public bool IsUpdating { get { return this.bIsUpdating; } set { this.bIsUpdating = value; } }

        public UInt16 TargetResultNumber { get { return this.nTargetResultNumber; } set { this.nTargetResultNumber = value; } }
        public string TargetResultName { get { return this.sResultName; } set { this.sResultName = value; } }

        public string ResultName { get { return "Result-" + this.ResultNumber; } }

        public UInt64 TargetOrderID { get; set; }
        #endregion

        #region "Constructors"

        #region "Supplementary Constructors"
        public clsGloResult(clsGloResult xGloResult)
            : 
            this
            (
            xGloResult.OrderID,
            xGloResult.TestID,
            xGloResult.ResultNumber,
            xGloResult.IsUpdating, xGloResult.Values
            ) { }

        public clsGloResult(DataRow row)
            : 
            this
            (
            Convert.ToUInt64(row["labotr_OrderID"]),
            Convert.ToUInt64(row["labotr_TestID"]),
            Convert.ToUInt16(row["labotr_TestResultNumber"]), 
            false, 
            null
            ) { }
        #endregion

        #region "Primary Constructor"
        public clsGloResult
            (
            UInt64 OrderID,
            UInt64 TestID,
            UInt16 ResultNumber,
            Boolean IsUpdating,
            Dictionary<Int32, clsGloResultDetail>.ValueCollection dResultDetails
            )
            : base()
        {

            try
            {
                this.OrderID = OrderID;
                this.TestID = TestID;
                this.ResultNumber = ResultNumber;
                this.IsUpdating = IsUpdating;
                this.TargetResultNumber = ResultNumber;

                if (dResultDetails != null && dResultDetails.Any())
                {
                    foreach (clsGloResultDetail _xGloResultDetail in dResultDetails)
                    {
                        if (!base.ContainsKey(_xGloResultDetail.GetHashCode()))
                        { base.Add(_xGloResultDetail.GetHashCode(), clsGloResultDetail.CloneResultDetail(_xGloResultDetail)); }
                    }
                }
            }
            catch (Exception ex) { throw ex; }

            
        }
        #endregion

        #endregion

        #region "Functions"

        public override int GetHashCode()
        { return (Convert.ToString(OrderID) + Convert.ToString(TestID) + Convert.ToString(ResultNumber)).GetHashCode(); }
        
        public static clsGloResult CloneResult(clsGloResult xGloResult)
        {
            try
            {
                clsGloResult _xResult = new clsGloResult(xGloResult.OrderID, xGloResult.TestID, xGloResult.ResultNumber, xGloResult.IsUpdating, xGloResult.Values);

                _xResult.TargetOrderID = xGloResult.TargetOrderID;
                _xResult.TargetResultNumber = xGloResult.TargetResultNumber;
                _xResult.TargetResultName = xGloResult.TargetResultName;
                return _xResult;
            }
            catch (Exception ex) { throw ex; }

           
        }

        public IEnumerable<clsGloResultDetail> GetAllResults()
        {
            try { return base.Values.AsEnumerable(); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

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
                    base.Clear();
                    this.nOrderID = 0;
                    //this.nTargetOrderID = 0;
                    this.bIsUpdating = false;
                    this.sResultName = string.Empty;
                    this.nTargetResultNumber = 0;
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
    #endregion

    #region "Result Doc"
    public class clsGloResultAttachment : IDisposable
    {

        #region "Attributes"
        public UInt64 OrderID { get; set; }
        public UInt64 TestID { get; set; }
        public UInt64 AttachmentID { get; set; }
        public UInt64 AttachmentNumber { get; set; }
        public bool IsDeleting { get; set; }
        public bool IsShifting { get; set; }
        #endregion

        #region "Constructors"

        #region "Supplementary Constructors"

        public clsGloResultAttachment(DataRow dataRow)
            : 
            this
            (
            Convert.ToUInt64(dataRow["labotrda_OrderID"]),
            Convert.ToUInt64(dataRow["labotrda_TestID"]),
            Convert.ToUInt64(dataRow["labotrda_AttachmentID"]),
            Convert.ToUInt16(dataRow["labotrda_AttachmentNo"])
            ) { }

        public clsGloResultAttachment(clsGloResultAttachment XGloResultDoc)
            : 
            this
            (
            XGloResultDoc.OrderID,
            XGloResultDoc.TestID,
            XGloResultDoc.AttachmentID,
            XGloResultDoc.AttachmentNumber
            ) { }

        #endregion

        #region "Primary Constructor"
        public clsGloResultAttachment(UInt64 nOrderID, UInt64 nTestID, UInt64 nAttachmentID, UInt64 nAttachmentNumber)
        {
            try
            {
                this.OrderID = nOrderID;
                this.TestID = nTestID;
                this.AttachmentID = nAttachmentID;
                this.AttachmentNumber = nAttachmentNumber;
            }
            catch (Exception ex) { throw ex; }

            
        }
        #endregion

        #endregion

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
                    this.OrderID = 0;
                    this.TestID = 0;
                    this.AttachmentID = 0;
                    this.AttachmentNumber = 0;
                    this.IsDeleting = false;
                    this.IsShifting = false;
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
    #endregion

    #region "Result Detail"
    public class clsGloResultDetail : IDisposable
    {
        #region "Properties"
      
        public UInt64 labom_OrderID { get; set; }      
        public UInt64 labotd_TestID { get; set; }        
        public UInt16 labotr_TestResultNumber { get; set; }
        public UInt16 ResultLineNo { get; set; }

        public UInt64 TargetOrderID { get; set; }        
        public UInt16 TargetResultNumber { get; set; }
        #endregion

        #region "Extras"

        public UInt64 labotr_TestID { get { return this.labotd_TestID; } }

        public Int64 labom_PatientID { get; set; }
        public string labotd_TestName { get; set; }
        public Int64 labotrd_ResultNameID { get; set; }
        public string labotrd_ResultName { get; set; }

        public Int64 labom_VisitID { get; set; }
        public Int64 labotd_LineNo { get; set; }
        public string labom_TransactionDate { get; set; }
        public string labotd_TestStatus { get; set; }

        public string labotd_SpecimenSource { get; set; }
        public string labotd_SpecimenConditionDisp { get; set; }
        public string labotd_TestType { get; set; }
        public string labotd_LOINCCode { get; set; }

        public string labotr_TestResultName { get; set; }
        public Int32 labotr_IsFinished { get; set; }
        public string labotr_TestName { get; set; }
        public string labotr_TestResultDateTime { get; set; }

        public string labotrd_TestSpecimenCollectionDateTime { get; set; }
        public string labotrd_ResultValue { get; set; }
        public string labotrd_ResultUnit { get; set; }
        public string labotrd_ResultRange { get; set; }

        public string labotrd_ResultType { get; set; }
        public string labotrd_AbnormalFlag { get; set; }
        public Int32 labotrd_IsFinished { get; set; }
        public string labotrd_LOINCID { get; set; }

        public string labotrd_TestName { get; set; }
        public string labotrd_LabFacilityName { get; set; }
        public string labotrd_LabFacilityStreetAddress { get; set; }
        public string labotrd_LabFacilityCity { get; set; }

        public string labotrd_LabFacilityState { get; set; }
        public string labotrd_LabFacilityZipCode { get; set; }
        public string labotrd_ResultComment { get; set; }
        public string labotrd_specificResultRefRange { get; set; }

        public Int32 labotrd_TestResultNumber { get; set; }
        public string labotrd_ResultDateTime { get; set; }
        public Int32 labotrd_ResultLineNo { get; set; }
        public Int64 labotd_DMSID { get; set; }
        public string sCPTCode { get; set; }



        #endregion

        #region "Secondary Constructor"
        public clsGloResultDetail(clsGloResultDetail ResultDetail)
            : this(
                ResultDetail.labom_OrderID,
                ResultDetail.labom_PatientID,
                ResultDetail.labotd_TestID,
                ResultDetail.labotd_TestName,
                ResultDetail.labotr_TestResultNumber,
                ResultDetail.ResultLineNo,
                ResultDetail.labotrd_ResultNameID,
                ResultDetail.labotrd_ResultName,
                ResultDetail.labom_VisitID,
                ResultDetail.labotd_LineNo,
                ResultDetail.labom_TransactionDate,
                ResultDetail.labotd_TestStatus,
                ResultDetail.labotd_SpecimenSource,
                ResultDetail.labotd_SpecimenConditionDisp,
                ResultDetail.labotd_TestType,
                ResultDetail.labotd_LOINCCode,
                ResultDetail.labotr_TestResultName,
                ResultDetail.labotr_IsFinished,
                ResultDetail.labotr_TestName,
                ResultDetail.labotr_TestResultDateTime,
                ResultDetail.labotrd_TestSpecimenCollectionDateTime,
                ResultDetail.labotrd_ResultValue,
                ResultDetail.labotrd_ResultUnit,
                ResultDetail.labotrd_ResultRange,
                ResultDetail.labotrd_ResultType,
                ResultDetail.labotrd_AbnormalFlag,
                ResultDetail.labotrd_IsFinished,
                ResultDetail.labotrd_LOINCID,
                ResultDetail.labotrd_TestName,
                ResultDetail.labotrd_LabFacilityName,
                ResultDetail.labotrd_LabFacilityStreetAddress,
                ResultDetail.labotrd_LabFacilityCity,
                ResultDetail.labotrd_LabFacilityState,
                ResultDetail.labotrd_LabFacilityZipCode,
                ResultDetail.labotrd_ResultComment,
                ResultDetail.labotrd_specificResultRefRange,
                ResultDetail.labotrd_TestResultNumber,
                ResultDetail.labotrd_ResultDateTime,
                ResultDetail.labotrd_ResultLineNo,
                ResultDetail.labotd_DMSID,
                ResultDetail.sCPTCode) { }
        #endregion
                
        #region "Data Cons"

        
        public clsGloResultDetail(DataRow Row)
            : this(Convert.ToUInt64(Row["labom_OrderID"]),
        Convert.ToInt64(Row["labom_PatientID"]),
        Convert.ToUInt64(Row["labotd_TestID"]),
        Convert.ToString(Row["labotd_TestName"]),

        Convert.ToUInt16(Row["labotr_TestResultNumber"]),
        Convert.ToUInt16(Row["labotrd_ResultLineNo"]),
        Convert.ToInt64(Row["labotrd_ResultNameID"]),
        Convert.ToString(Row["labotrd_ResultName"]),

        Convert.ToInt64(Row["labom_VisitID"]),
        Convert.ToInt64(Row["labotd_LineNo"]),
        Convert.ToString(Row["labom_TransactionDate"]),
        Convert.ToString(Row["labotd_TestStatus"]),

        Convert.ToString(Row["labotd_SpecimenSource"]),
        Convert.ToString(Row["labotd_SpecimenConditionDisp"]),
        Convert.ToString(Row["labotd_TestType"]),
        Convert.ToString(Row["labotd_LOINCCode"]),

        Convert.ToString(Row["labotr_TestResultName"]),
        Convert.ToInt32(Row["labotr_IsFinished"]),
        Convert.ToString(Row["labotr_TestName"]),
        Convert.ToString(Row["labotr_TestResultDateTime"]),

        Convert.ToString(Row["labotrd_TestSpecimenCollectionDateTime"]),
        Convert.ToString(Row["labotrd_ResultValue"]),
        Convert.ToString(Row["labotrd_ResultUnit"]),
        Convert.ToString(Row["labotrd_ResultRange"]),

        Convert.ToString(Row["labotrd_ResultType"]),
        Convert.ToString(Row["labotrd_AbnormalFlag"]),
        Convert.ToInt32(Row["labotr_IsFinished"]),
        Convert.ToString(Row["labotrd_LOINCID"]),

        Convert.ToString(Row["labotrd_TestName"]),
        Convert.ToString(Row["labotrd_LabFacilityName"]),
        Convert.ToString(Row["labotrd_LabFacilityStreetAddress"]),
        Convert.ToString(Row["labotrd_LabFacilityCity"]),

        Convert.ToString(Row["labotrd_LabFacilityState"]),
        Convert.ToString(Row["labotrd_LabFacilityZipCode"]),
        Convert.ToString(Row["labotrd_ResultComment"]),
        Convert.ToString(Row["labotrd_specificResultRefRange"]),

        Convert.ToInt32(Row["labotrd_TestResultNumber"]),
        Convert.ToString(Row["labotrd_ResultDateTime"]),
        Convert.ToInt32(Row["labotrd_ResultLineNo"]),
        Convert.ToInt64(Row["labotd_DMSID"]),
        Convert.ToString(Row["sCPTCode"])) { }



        #endregion

        #region "Primary Constructor"

        public clsGloResultDetail(
                UInt64 OrderID,
                Int64 labom_PatientID,
                UInt64 TestID,
                string TestName,

                UInt16 TestResultNumber,
                UInt16 ResultLineNo,
                Int64 ResultNameID,
                string ResultName,

                Int64 labom_VisitID,
                Int64 labotd_LineNo,
                string labom_TransactionDate,
                string labotd_TestStatus,

                string labotd_SpecimenSource,
                string labotd_SpecimenConditionDisp,
                string labotd_TestType,
                string labotd_LOINCCode,

                string labotr_TestResultName,
                Int32 labotr_IsFinished,
                string labotr_TestName,
                string labotr_TestResultDateTime,

                string TestSpecimenCollectionDateTimeUTC,
                string ResultValue,
                string ResultUnit,
                string ResultRange,

                string ResultType,
                string AbnormalFlag,
                Int32 IsFinished,
                string LOINCID,

                string labotrd_TestName,
                string LabFacilityName,
                string LabFacilityStreetAddress,
                string LabFacilityCity,

                string LabFacilityState,
                string LabFacilityZipCode,
                string ResultComment,
                string specificResultRefRange,

                Int32 labotrd_TestResultNumber,
                string labotrd_ResultDateTime,
                Int32 labotrd_ResultLineNo,
                Int64 labotd_DMSID,
                string sCPTCode
         )
        {

            try
            {
                this.labom_OrderID = OrderID;
                this.labom_PatientID = labom_PatientID;
                this.labotd_TestID = TestID;
                this.labotd_TestName = TestName;
                this.labotr_TestResultNumber = TestResultNumber;
                this.ResultLineNo = ResultLineNo;
                this.labotrd_ResultNameID = ResultNameID;
                this.labotrd_ResultName = ResultName;
                this.labom_VisitID = labom_VisitID;
                this.labotd_LineNo = labotd_LineNo;
                this.labom_TransactionDate = labom_TransactionDate;
                this.labotd_TestStatus = labotd_TestStatus;
                this.labotd_SpecimenSource = labotd_SpecimenSource;
                this.labotd_SpecimenConditionDisp = labotd_SpecimenConditionDisp;
                this.labotd_TestType = labotd_TestType;
                this.labotd_LOINCCode = labotd_LOINCCode;
                this.labotr_TestResultName = labotr_TestResultName;
                this.labotr_IsFinished = labotr_IsFinished;
                this.labotr_TestName = labotr_TestName;
                this.labotr_TestResultDateTime = labotr_TestResultDateTime;
                this.labotrd_TestSpecimenCollectionDateTime = TestSpecimenCollectionDateTimeUTC;
                this.labotrd_ResultValue = ResultValue;
                this.labotrd_ResultUnit = ResultUnit;
                this.labotrd_ResultRange = ResultRange;
                this.labotrd_ResultType = ResultType;
                this.labotrd_AbnormalFlag = AbnormalFlag;
                this.labotrd_IsFinished = IsFinished;
                this.labotrd_LOINCID = LOINCID;
                this.labotrd_TestName = labotrd_TestName;
                this.labotrd_LabFacilityName = LabFacilityName;
                this.labotrd_LabFacilityStreetAddress = LabFacilityStreetAddress;
                this.labotrd_LabFacilityCity = LabFacilityCity;
                this.labotrd_LabFacilityState = LabFacilityState;
                this.labotrd_LabFacilityZipCode = LabFacilityZipCode;
                this.labotrd_ResultComment = ResultComment;
                this.labotrd_specificResultRefRange = specificResultRefRange;
                this.labotrd_TestResultNumber = labotrd_TestResultNumber;
                this.labotrd_ResultDateTime = labotrd_ResultDateTime;
                this.labotrd_ResultLineNo = labotrd_ResultLineNo;
                this.labotd_DMSID = labotd_DMSID;
                this.sCPTCode = sCPTCode;
            }
            catch (Exception ex) { throw ex; }

           
        }

        //public clsGloResultDetail(UInt64 OrderID, UInt64 TestID, UInt16 ResultNumber, UInt16 ResultLineNo)
        //{
        //    this.OrderID = OrderID;
        //    this.TestID = TestID;
        //    this.ResultNumber = ResultNumber;
        //    this.ResultLineNo = ResultLineNo;
        //}
        #endregion

        #region "Functions"
       
        public override int GetHashCode()
        {
            try { return (labom_OrderID.ToString() + labotd_TestID.ToString() + labotr_TestResultNumber.ToString() + ResultLineNo.ToString()).GetHashCode(); }
            catch (Exception ex) { throw ex; }
        }

        public static clsGloResultDetail CloneResultDetail(clsGloResultDetail ResultDetail)
        {
            return new clsGloResultDetail(ResultDetail);
        }
        #endregion

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

    #endregion

    #endregion

    #region "LINQ"
    public class clsMergeOrderLINQ
    {
                
        #region "Attachment Queries"

        #region "Matched and Unmatched Attachments"

        
        public static IEnumerable<clsGloResultAttachment> GetMatchedAttachments(clsGloOrder SourceOrder, clsGloOrder TargetOrder, clsGloTest xMatchedTest)
        {
            try
            { return SourceOrder[xMatchedTest.Key].DictionaryDocuments.Values.Intersect(TargetOrder[xMatchedTest.Key].DictionaryDocuments.Values, new clsGloAttachmentComparer()); }
            catch (Exception ex) { throw ex; }


        }

        public static IEnumerable<clsGloResultAttachment> GetUnmatchedAttachments(clsGloOrder SourceOrder, clsGloOrder TargetOrder, clsGloTest _xGloTest)
        {
            try
            { return SourceOrder[_xGloTest.Key].DictionaryDocuments.Values.Except(TargetOrder[_xGloTest.Key].DictionaryDocuments.Values, new clsGloResultDocumentComparer()); }
            catch (Exception ex) { throw ex; }




        }

        #endregion

        #region "Shifting and Deleting Attachments"

        public static IQueryable<clsGloResultAttachment> GetRemovalAttachments(clsGloOrder Order)
        {
            try
            {

                return clsMergeOrderLINQ.GetTestsWithRemovalAttachments(Order).AsParallel().SelectMany(argAttachment => argAttachment.GetRemovalAttachments()).AsQueryable();

                //return clsMergeOrderLINQ.GetTestsWithRemovalAttachments(Order).ToList().SelectMany(argAttachment => argAttachment.GetRemovalAttachments()).AsQueryable();
            }
            catch (Exception ex) { throw ex; }
            //return Order.Values.Where(argTest => argTest.HasAttachmentsForRemoval).ToList().SelectMany(argAttachment => argAttachment.GetRemovalAttachments()).AsQueryable();
        }

        public static IQueryable<clsGloResultAttachment> GetShiftingAttachments(clsGloOrder Order)
        {
            try
            {
                return clsMergeOrderLINQ.GetTestsWithShiftingAttachments(Order).AsParallel().SelectMany(argAttachment => argAttachment.GetShiftingAttachments()).AsQueryable();
            }
            catch (Exception ex) { throw ex; }

            //return Order.Values.Where(argTest => argTest.HasAttachmentsForInsertion).ToList().SelectMany(argAttachment => argAttachment.GetShiftingAttachments()).AsQueryable();
        }
      
        #endregion

        #endregion

        #region "Result Detail Queries"

        public static IEnumerable<clsGloResultDetail> GetAllResultDetails(clsGloOrder xOrder)
        {
            try { return xOrder.Values.Where(xTest => xTest.Values.Any()).SelectMany(yTest => yTest.Values.SelectMany(xResult => xResult.GetAllResults())); }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region "Attachment Test Queries"

        public static IEnumerable<clsGloTest> GetMatchedTests(clsGloOrder SourceOrder, clsGloOrder TargetOrder)
        {
            try
            { return SourceOrder.Values.Intersect(TargetOrder.Values, new clsGloTestComparer()).OrderBy(p => p.LineNo); }
            catch (Exception ex) { throw ex; }
        }

        public static IQueryable<clsGloTest> GetTestsWithRemovalAttachments(clsGloOrder Order)
        {
            try { return Order.Values.Where(argTest => argTest.HasAttachmentsForRemoval).AsQueryable(); }
            catch (Exception ex) { throw ex; }            
        }

        public static IQueryable<clsGloTest> GetTestsWithShiftingAttachments(clsGloOrder Order)
        {
            try { return Order.Values.Where(argTest => argTest.HasAttachmentsForInsertion).AsQueryable(); }
            catch (Exception ex) { throw ex; }
            
        }

        #endregion

        #region "Tests and Results related Queries"
        public static IQueryable<clsGloTest> GetNonShiftingTests(clsGloOrder Order)
        {
            try { return Order.Values.Where(p => !p.IsShifting).AsQueryable(); }
            catch (Exception ex) { throw ex; }
        }

        public static IQueryable<clsGloResult> GetUpdatingResults(clsGloOrder Order)
        {
            try { return clsMergeOrderLINQ.GetNonShiftingTests(Order).AsParallel().SelectMany(p => p.GetUpdatingResults()).AsQueryable(); }
            catch (Exception ex) { throw ex; }
        }
        #endregion
                
    }
    #endregion

    #region "Comparer Classes"

    public class clsGloTestComparer : EqualityComparer<clsGloTest>
    {

        public override bool Equals(clsGloTest x, clsGloTest y)
        {
            try
            { if (x.Key == y.Key) { return true; } else { return false; } }
            catch (Exception ex) { throw ex; }            
        }

        public override int GetHashCode(clsGloTest z)
        {
            try
            { return z.Key.GetHashCode(); }
            catch (Exception ex) { throw ex; }            
        }

    }

    public class clsGloResultDocumentComparer : EqualityComparer<clsGloResultAttachment>
    {

        public override bool Equals(clsGloResultAttachment x, clsGloResultAttachment y)
        {
            try
            {
                if (x.TestID == y.TestID && x.AttachmentID == y.AttachmentID)
                { return true; }
                else { return false; }
            }
            catch (Exception ex) { throw ex; }

           
        }

        public override int GetHashCode(clsGloResultAttachment z)
        {
            try
            { return z.AttachmentID.GetHashCode(); }
            catch (Exception ex) { throw ex; }

        }

    }

    public class clsGloAttachmentComparer : EqualityComparer<clsGloResultAttachment>
    {

        public override bool Equals(clsGloResultAttachment x, clsGloResultAttachment y)
        {
            try
            {
                if (x.AttachmentID == y.AttachmentID) { return true; }
                else { return false; }
            }
            catch (Exception ex) { throw ex; }

           
        }

        public override int GetHashCode(clsGloResultAttachment z)
        {
            try
            { return z.AttachmentID.GetHashCode(); }
            catch (Exception ex) { throw ex; }                        
        }

    }
    #endregion

    #region "Database Layer"
    public class clsMergeOrderDBLayer : IDisposable
    {

        #region "Constructor, Destructor and Properties"

        #region "Constructor"
        public clsMergeOrderDBLayer(UInt64 SourceOrderID, UInt64 TargetOrderID, string ConnectionString)
        { LoadOrders(SourceOrderID, TargetOrderID, ConnectionString); }
        #endregion

        #region "Destructor"
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
                    this.SourceOrderID = 0;
                    this.TargetOrderID = 0;

                    if (this.OrdersAndResults != null)
                    {
                        OrdersAndResults.Clear();
                        OrdersAndResults.Dispose();
                        OrdersAndResults = null;
                    }

                    if (this.dtTest != null)
                    {
                        dtTest.Clear();
                        dtTest.Dispose();
                        dtTest = null;
                    }

                    if (this.dtResult != null)
                    {
                        dtResult.Clear();
                        dtResult.Dispose();
                        dtResult = null;
                    }

                    if (this.dtResultDoc != null)
                    {
                        dtResultDoc.Clear();
                        dtResultDoc.Dispose();
                        dtResultDoc = null;
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
        #endregion

        #region "Properties"
        #region "Integers and Strings"
        [DefaultValue(0)]
        public UInt64 SourceOrderID { get; set; }

        [DefaultValue(0)]
        public UInt64 TargetOrderID { get; set; }

        [DefaultValue("")]
        public string ConnectionString { get; set; }
        #endregion

        #region "DataTables"
        [DefaultValue(null)]
        public DataSet OrdersAndResults { get; set; }

        [DefaultValue(null)]
        public DataTable Orders
        {
            get
            {
                if (this.OrdersAndResults != null && this.OrdersAndResults.Tables.Count >= 0)
                { return this.OrdersAndResults.Tables[0]; }
                else
                { return null; }
            }
        }

        [DefaultValue(null)]
        public DataTable Tests
        {
            get
            {
                if (this.OrdersAndResults != null && this.OrdersAndResults.Tables.Count >= 1)
                { return this.OrdersAndResults.Tables[1]; }
                else
                { return null; }
            }
        }

        [DefaultValue(null)]
        public DataTable Results
        {
            get
            {
                if (this.OrdersAndResults != null && this.OrdersAndResults.Tables.Count >= 2)
                { return this.OrdersAndResults.Tables[2]; }
                else
                { return null; }
            }
        }

        [DefaultValue(null)]
        public DataTable ResultDetail
        {
            get
            {
                if (this.OrdersAndResults != null && this.OrdersAndResults.Tables.Count >= 5)
                { return this.OrdersAndResults.Tables[5]; }
                else
                { return null; }
            }
        }

        [DefaultValue(null)]
        public DataTable Attachments
        {
            get
            {
                if (this.OrdersAndResults != null && this.OrdersAndResults.Tables.Count >= 3)
                { return this.OrdersAndResults.Tables[3]; }
                else
                { return null; }
            }
        }

        [DefaultValue(null)]
        public DataTable RenamingTests { get; set; }
        #endregion
        #endregion
        
        #endregion
        
        #region "Load Orders"
        public void LoadOrders(UInt64 SourceOrderID, UInt64 TargetOrderID, string ConnectionString)
        {
            try
            {
                this.SourceOrderID = SourceOrderID;
                this.TargetOrderID = TargetOrderID;
                this.ConnectionString = ConnectionString;

                if (this.OrdersAndResults != null) 
                { 
                    this.OrdersAndResults.Clear();
                    this.OrdersAndResults.Tables.Clear();
                }
                else { this.OrdersAndResults = new DataSet(); }

                SqlCommand selectCommand = new SqlCommand("MergeOrders_GetSourceAndTargetOrder", new SqlConnection(this.ConnectionString));
                selectCommand.CommandType = CommandType.StoredProcedure;

                selectCommand.Parameters.Add(Get_SQL_Parameter("@SourceOrderID", SqlDbType.BigInt, SourceOrderID));
                selectCommand.Parameters.Add(Get_SQL_Parameter("@TargetOrderID", SqlDbType.BigInt, TargetOrderID));


                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(selectCommand)) 
                { 
                    sqlAdapter.Fill(this.OrdersAndResults);

                    selectCommand.Parameters.Clear();
                    selectCommand.Dispose();
                    selectCommand = null;
                }

                
            }
            catch (Exception ex) { throw ex; }            
        }
        #endregion

        #region "Attributes"

        DataTable dtTest = null;
        DataTable dtResult = null;       
        DataTable dtResultDoc = null;

        #endregion

        #region "Get DataRows"

        private DataRow GetResultsDataRow(clsGloResult xGloResult)
        {

            try
            {
                DataRow ResultRow = dtResult.NewRow();
                ResultRow["nOrderID"] = xGloResult.OrderID;
                ResultRow["nTestID"] = xGloResult.TestID;
                ResultRow["nTestResultNumber"] = xGloResult.ResultNumber;
                ResultRow["nTargetOrderID"] = xGloResult.TargetOrderID;                
                ResultRow["nTargetTestResultNumber"] = xGloResult.TargetResultNumber;
                ResultRow["sTargetResultName"] = xGloResult.TargetResultName;

                return ResultRow;
            }
            catch (Exception Ex) { throw Ex; }
        }

        private DataRow GetResultDoc(clsGloResultAttachment xGloDoc)
        {
            try
            {
                DataRow ResultRow = dtResultDoc.NewRow();
                ResultRow["nOrderID"] = xGloDoc.OrderID;
                ResultRow["nTestID"] = xGloDoc.TestID;
                ResultRow["nAttachmentID"] = xGloDoc.AttachmentID;
                ResultRow["nAttachmentNumber"] = xGloDoc.AttachmentNumber;
                ResultRow["bIsDeleting"] = xGloDoc.IsDeleting;
                ResultRow["bIsShifting"] = xGloDoc.IsShifting;

                return ResultRow;
            }
            catch (Exception Ex) { throw Ex; }
        }

        private DataRow GetTestDataRow(clsGloTest xGloTest)
        {
            try
            {
                DataRow ResultRow = dtTest.NewRow();
                ResultRow["nOrderID"] = xGloTest.OrderID;
                ResultRow["nTestID"] = xGloTest.TestID;
                ResultRow["nTargetOrderID"] = xGloTest.TargetOrderID;
                return ResultRow;
            }
            catch (Exception Ex) { throw Ex; }
        }

        #endregion

        #region "Insert Test into DataTable"
                
        public void InsertTestIntoDataTable(clsGloOrder xGloOrder)
        {
            try
            {                
                // For all Tests that are common to Source and Target
                // we need to just shift the Results and not the whole
                // Tests.

                foreach (clsGloResult _xGloResult in clsMergeOrderLINQ.GetUpdatingResults(xGloOrder))
                { dtResult.Rows.Add(GetResultsDataRow(_xGloResult)); }
                             
                // For all Tests that are different
                foreach (clsGloTest _xGloTest in xGloOrder.Values.Where(p => p.IsShifting).AsParallel())
                { dtTest.Rows.Add(GetTestDataRow(_xGloTest)); }

            }
            catch (Exception Ex) { throw Ex; }
        }

        #endregion

        #region "Build Table Valued Parameters"
        public void Build_TVPs()
        {

            try
            {
                if (dtResult == null)
                {
                    dtResult = new DataTable("Results");

                    var _with2 = dtResult.Columns;
                    _with2.Add(new DataColumn("nOrderID", System.Type.GetType("System.Int64")));
                    _with2.Add(new DataColumn("nTestID", System.Type.GetType("System.Int64")));
                    _with2.Add(new DataColumn("nTestResultNumber", System.Type.GetType("System.Int64")));
                    _with2.Add(new DataColumn("nTargetOrderID", System.Type.GetType("System.Int64")));
                    _with2.Add(new DataColumn("nTargetTestResultNumber", System.Type.GetType("System.Int64")));
                    _with2.Add(new DataColumn("sTargetResultName", System.Type.GetType("System.String")));
                }
                else { dtResult.Clear(); }
                
                if (dtTest == null)
                {

                    dtTest = new DataTable("Test");

                    var _with1 = dtTest.Columns;
                    _with1.Add(new DataColumn("nOrderID", System.Type.GetType("System.Int64")));
                    _with1.Add(new DataColumn("nTestID", System.Type.GetType("System.Int64")));
                    _with1.Add(new DataColumn("nTargetOrderID", System.Type.GetType("System.Int64")));
                }
                else { dtTest.Clear(); }

                if (dtResultDoc == null)
                {
                    dtResultDoc = new DataTable("ResultDocuments");

                    var _with3 = dtResultDoc.Columns;
                    _with3.Add(new DataColumn("nOrderID", System.Type.GetType("System.Int64")));
                    _with3.Add(new DataColumn("nTestID", System.Type.GetType("System.Int64")));
                    _with3.Add(new DataColumn("nAttachmentID", System.Type.GetType("System.Int64")));
                    _with3.Add(new DataColumn("nAttachmentNumber", System.Type.GetType("System.Int32")));
                    _with3.Add(new DataColumn("bIsDeleting", System.Type.GetType("System.Boolean")));
                    _with3.Add(new DataColumn("bIsShifting", System.Type.GetType("System.Boolean")));
                }
                else { dtResultDoc.Clear(); }
                                            
            }
            catch (Exception Ex) { throw Ex; }
        }
        #endregion

        #region "Execute Update query"

        public void ExecuteUpdate(UInt64 SourceOrderID,UInt64 TargetOrderID)
        {
            SqlTransaction sqlTransaction = null;            
            SqlParameter[] parameters = null;
            SqlCommand dbCommand = new SqlCommand();
           
            try
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Connection = new SqlConnection(this.ConnectionString);

                parameters = GetUpdateParameters(SourceOrderID, TargetOrderID);

                if (this.RenamingTests != null && this.RenamingTests.AsEnumerable().Any())
                {
                    dbCommand.CommandText = "MergeOrders_RenameTests";
                    dbCommand.Parameters.Add(Get_SQL_Parameter("@TargetOrderID", SqlDbType.BigInt, TargetOrderID));
                    dbCommand.Parameters.Add(Get_SQL_Parameter("@TVP_Rename_Tests", SqlDbType.Structured, this.RenamingTests));

                    dbCommand.Connection.Open();

                    sqlTransaction = dbCommand.Connection.BeginTransaction();

                    dbCommand.Transaction = sqlTransaction;
                    dbCommand.ExecuteNonQuery();
                    dbCommand.Parameters.Clear();
                }

                dbCommand.CommandText = "MergeOrders_UpdateTables";
                dbCommand.Parameters.AddRange(parameters);

                if (this.RenamingTests == null)
                {
                    dbCommand.Connection.Open();
                    sqlTransaction = dbCommand.Connection.BeginTransaction();
                    dbCommand.Transaction = sqlTransaction;
                }

                dbCommand.ExecuteNonQuery();

                sqlTransaction.Commit();
                dbCommand.Connection.Close();

                //DataSet datasetTest = new DataSet();
                //SqlDataAdapter da = new SqlDataAdapter(UpdateCommand);
                //da.Fill(datasetTest);

            }
            catch
            {
                try { if (sqlTransaction != null) { sqlTransaction.Rollback(); } }
                catch (Exception exception) { throw exception; }

                throw new Exception("Error occured while merging Order in database. Merge operation aborted.");
            }
            finally
            {
                dbCommand.Connection.Close();
                dbCommand.Connection.Dispose();
                dbCommand.Connection = null;
                if (dbCommand != null)
                {
                    dbCommand.Parameters.Clear();
                    dbCommand.Dispose();
                    dbCommand = null;
                }
                sqlTransaction.Dispose();
                sqlTransaction = null;
                parameters = null;
            }
        }
        #endregion

        #region "Get Parameters"

        private SqlParameter Get_SQL_Parameter(string ParameterName, SqlDbType DBType, object ParameterValue)
        {
            try
            {
                SqlParameter sqlParameter = new SqlParameter();

                sqlParameter.SqlDbType = DBType;
                sqlParameter.Direction = ParameterDirection.Input;
                sqlParameter.ParameterName = ParameterName;
                sqlParameter.Value = ParameterValue;

                return sqlParameter;
            }
            catch (Exception Ex) { throw Ex; }
        }

        private SqlParameter[] GetUpdateParameters(UInt64 SourceOrderID, UInt64 TargetOrderID)
        {
            try
            {
                return new SqlParameter[] { Get_SQL_Parameter("@SourceOrderID", SqlDbType.BigInt, SourceOrderID),
                                                    Get_SQL_Parameter("@TargetOrderID", SqlDbType.BigInt, TargetOrderID),
                                                    Get_SQL_Parameter("@TVP_Test", SqlDbType.Structured, this.dtTest),
                                                    Get_SQL_Parameter("@TVP_Results", SqlDbType.Structured, this.dtResult),
                                                    Get_SQL_Parameter("@TVP_ResultDocs", SqlDbType.Structured, this.dtResultDoc)
                                                };
            }
            catch (Exception Ex) { throw Ex; }

        }

        #endregion

        #region "Insert Attachment data"
        public void InsertAttachmentIntoDataTable(clsGloOrder SourceOrder, clsGloOrder TargetOrder)
        {
            try
            {                
                foreach (clsGloResultAttachment _xGloAttachment in clsMergeOrderLINQ.GetRemovalAttachments(SourceOrder))
                { dtResultDoc.Rows.Add(GetResultDoc(_xGloAttachment)); }

                foreach (clsGloResultAttachment _xGloAttachment in clsMergeOrderLINQ.GetShiftingAttachments(TargetOrder))
                { dtResultDoc.Rows.Add(GetResultDoc(_xGloAttachment)); }                                                
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
   
        
    }
    #endregion

    #region "Exception Objects"

    public class TemplateException : Exception
    {public TemplateException(string Message) : base(Message) { } }

    public class ReplacementTestException : Exception
    { public ReplacementTestException(string Message) : base(Message) { } }

    #endregion

    #region "Replace Tests TVP Builder"
    public class clsReplaceTests_TVPBuilder : IDisposable
    {
        #region "Properties"

        [DefaultValue(null)]
        public DataTable RenamingTests { get; set; }

        #endregion

        #region "Constructor"
        public clsReplaceTests_TVPBuilder() { BuildDatatable(); }
        #endregion

        #region "Build Datatable"

        private void BuildDatatable()
        {
            try
            {
                if (RenamingTests == null)
                {
                    RenamingTests = new DataTable("RenameTests");

                    var _1 = RenamingTests.Columns;
                    _1.Add(new DataColumn("nOrderID", System.Type.GetType("System.Int64")));
                    _1.Add(new DataColumn("nTestID", System.Type.GetType("System.Int64")));
                    _1.Add(new DataColumn("nTargetTestID", System.Type.GetType("System.Int64")));
                    _1.Add(new DataColumn("sTargetTestName", System.Type.GetType("System.String")));
                    _1.Add(new DataColumn("sTargetLOINCCode", System.Type.GetType("System.String")));
                    _1 = null;
                }
                else { RenamingTests.Clear(); }

                
            }
            catch (Exception ex) { throw ex; }

           
        }

        #endregion

        #region "Add rows"
        public void AddRow(clsGloTest SourceTest, clsGloTest TargetTest)
        {
            try
            {
                DataRow ResultRow = RenamingTests.NewRow();
                ResultRow["nOrderID"] = TargetTest.OrderID;
                ResultRow["nTestID"] = TargetTest.TestID;
                ResultRow["nTargetTestID"] = SourceTest.TestID;
                ResultRow["sTargetTestName"] = SourceTest.TestName;
                ResultRow["sTargetLOINCCode"] = SourceTest.LOINCCode;
                RenamingTests.Rows.Add(ResultRow);
                ResultRow = null;
            }
            catch (Exception Ex) { throw Ex; }
        }
        #endregion

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
                    if (this.RenamingTests != null)
                    {
                        this.RenamingTests.Clear();
                        this.RenamingTests.Dispose();
                        this.RenamingTests = null;
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
    #endregion

    #region "Replacing Test Object"

    public class clsReplacingObject
    {
        #region "Attributes"
        public UInt64 SourceTestID { get; set; }
        public UInt64 TargetTestID { get; set; }
        #endregion

        #region "Constructors"
        public clsReplacingObject(DataRow Row)
            :
            this(
                Convert.ToUInt64(Row["Electronic"]),
                Convert.ToUInt64(Row["Manual"])
                ) { }



        public clsReplacingObject(UInt64 SourceTestID, UInt64 TargetTestID)
        {
            this.SourceTestID = SourceTestID;
            this.TargetTestID = TargetTestID;
        }
        #endregion

        #region "Functions"
        public int Key { get { return (this.SourceTestID.GetHashCode() + this.TargetTestID.GetHashCode()).GetHashCode(); } }
        #endregion                                
    }

    #endregion
}



