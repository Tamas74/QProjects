using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace gloAccountsV2
{
    public class PaymentCollection
    {
        public class Credit
        {
            #region "Private Variables"

            private Int64 nCreditID = 0;
            private string sPaymentNo = "";
            private PaymentModeV2 nPaymentMode = PaymentModeV2.None;
            private DateTime dtCloseDate = DateTime.Now;
            private string sReceiptNo = "";
            private DateTime dtReceiptDate = DateTime.Now;
            private string sCreditCardType = "";
            private string sAuthorizationNo = "";
            private decimal dReceiptAmount = 0;
            private PayerTypeV2 nPayerType = PayerTypeV2.None;
            private Int64 nPayerID = 0;
            private string sPayerName = "";
            private Int64 nPaymentTrayID = 0;
            private string sPaymentTrayDesc = "";
            private string sPaymentNote = "";
            private Int64 nUserID = 0;
            private string sUserName = "";
            private bool bIsPaymentVoid = false;
            private Int64 nVoidType = 0;
            private DateTime dtPaymentVoidCloseDate = DateTime.Now;
            private DateTime dtPaymentVoidDateTime = DateTime.Now;
            private Int64 nVoidRefID = 0;
            private bool bIsERAPost = false;
            private Int64 nBPRID = 0;
            private DateTime dtCreatedDateTime = DateTime.Now;
            private DateTime dtModifiedDateTime = DateTime.Now;
            private Int64 nPAccountID = 0;
            private Int64 nGuarantorID = 0;
            private Int64 nSequnceNo = 0;
            private Int64 nAccountPatientID = 0;
            private PaymentEntryTypeV2 nEntryType = PaymentEntryTypeV2.None;
            #endregion "Private Variables"

            #region "Constructor & Distructor"

            public Credit()
            {
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        //_PaymentInsuranceClaims.Dispose();
                        //_EOBInsurancePaymentLineDetails.Dispose();
                        //_EOBInsurancePaymentReserveLineDetail.Dispose();
                        //_Notes.Dispose();
                    }
                }
                disposed = true;
            }

            ~Credit()
            {
                Dispose(false);
            }

            #endregion

            #region "Declaring Properties .."

            public Int64 PaymentID
            {
                set
                {
                    this.nCreditID = value;
                }
                get
                {
                    return this.nCreditID;
                }
            }
            public string PaymentNo
            {
                set
                {
                    this.sPaymentNo = value;
                }
                get
                {
                    return this.sPaymentNo;
                }
            }
            public string PaymentNumberPefix = "";
            public PaymentModeV2 PaymentMode
            {
                set
                {
                    this.nPaymentMode = value;
                }
                get
                {
                    return this.nPaymentMode;
                }
            }
            public DateTime CloseDate
            {
                set
                {
                    this.dtCloseDate = value;
                }
                get
                {
                    return dtCloseDate;
                }
            }
            public string ReceiptNo
            {
                set
                {
                    this.sReceiptNo = value;
                }
                get
                {
                    return sReceiptNo;
                }
            }
            public DateTime ReceiptDate
            {
                set
                {
                    this.dtReceiptDate = value;
                }
                get
                {
                    return this.dtReceiptDate;
                }
            }
            public string CreditCardType
            {
                set
                {
                    this.sCreditCardType = value;
                }
                get
                {
                    return this.sCreditCardType;
                }

            }
            public string AuthorizationNo
            {
                set
                {
                    this.sAuthorizationNo = value;
                }
                get
                {
                    return this.sAuthorizationNo;
                }
            }
            public decimal ReceiptAmount
            {
                set
                {
                    this.dReceiptAmount = value;
                }
                get
                {
                    return this.dReceiptAmount;
                }
            }
            public PayerTypeV2 PayerType
            {
                set
                {
                    this.nPayerType = value;
                }
                get
                {
                    return this.nPayerType;
                }
            }
            public Int64 PayerID
            {
                set
                {
                    this.nPayerID = value;
                }
                get
                {
                    return this.nPayerID;
                }
            }
            public string PayerName
            {
                set
                {
                    this.sPayerName = value;
                }
                get
                {
                    return this.sPayerName;
                }
            }
            public Int64 PaymentTrayID
            {
                set
                {
                    this.nPaymentTrayID = value;
                }
                get
                {
                    return this.nPaymentTrayID;
                }
            }
            public string PaymentTrayDesc
            {
                set
                {
                    sPaymentTrayDesc = value;
                }
                get
                {
                    return this.sPaymentTrayDesc;
                }
            }
            public string PaymentNote
            {
                set
                {
                    sPaymentNote = value;
                }
                get
                {
                    return this.sPaymentNote;
                }
            }
            public Int64 UserID
            {
                set
                {
                    this.nUserID = value;
                }
                get
                {
                    return this.nUserID;
                }

            }
            public string UserName
            {
                set
                {
                    sUserName = value;
                }
                get
                {
                    return this.sUserName;
                }
            }
            public bool IsPaymentVoid
            {
                set
                {
                    bIsPaymentVoid = value;
                }
                get
                {
                    return this.bIsPaymentVoid;
                }
            }
            public Int64 VoidType
            {
                set
                {
                    this.nVoidType = value;
                }
                get
                {
                    return this.nVoidType;
                }
            }
            public DateTime PaymentVoidCloseDate
            {
                set
                {
                    this.dtPaymentVoidCloseDate = value;
                }
                get
                {
                    return this.dtPaymentVoidCloseDate;
                }
            }
            public DateTime PaymentVoidDateTime
            {
                set
                {
                    this.dtPaymentVoidDateTime = value;
                }
                get
                {
                    return dtPaymentVoidDateTime;
                }
            }
            public Int64 VoidRefID
            {
                set
                {
                    this.nVoidRefID = value;
                }
                get
                {
                    return nVoidRefID;
                }
            }
            public bool IsERAPost
            {
                set
                {
                    this.bIsERAPost = value;
                }
                get
                {
                    return this.bIsERAPost;
                }
            }
            public Int64 BPRID
            {
                set
                {
                    this.nBPRID = value;
                }
                get
                {
                    return this.nBPRID;
                }
            }
            public DateTime CreatedDateTime
            {
                set
                {
                    this.dtCreatedDateTime = value;
                }
                get
                {
                    return this.dtCreatedDateTime;
                }
            }
            public DateTime ModifiedDateTime
            {
                set
                {
                    this.dtModifiedDateTime = value;
                }
                get
                {
                    return this.dtModifiedDateTime;
                }
            }
            public PaymentEntryTypeV2 Entrytype
            {   set { this.nEntryType = value; }
                get { return this.nEntryType; }
            }
            public Int64 AccountID
            {
                set
                {
                    this.nPAccountID = value;
                }
                get
                {
                    return this.nPAccountID;
                }
            }
            public Int64 AccGuarantorID
            {
                set
                {
                    this.nGuarantorID = value;
                }
                get
                {
                    return this.nGuarantorID;
                }
            }
            public Int64 SequnceNo
            {
                set
                {
                    this.nSequnceNo = value;
                }
                get
                {
                    return this.nSequnceNo;
                }
            }
            public Int64 AccSelectedPatient
            {
                set
                {
                    this.nAccountPatientID = value;
                }
                get
                {
                    return this.nAccountPatientID;
                }
            }

            public EOBCollection<Debits> EOBDebits = new EOBCollection<Debits>();
            public EOBCollection<CreditsDTL> EOBCreditDTL = new EOBCollection<CreditsDTL>();
            public EOBCollection<Reserves> EOBReserves = new EOBCollection<Reserves>();
            public EOBCollection<EOB> PaymentEOB = new EOBCollection<EOB>();
            public EOBCollection<EOB_EXT> PaymentEOB_EXT = new EOBCollection<EOB_EXT>();
            public EOBCollection<CreditsEXT> PaymentCredit_EXT = new EOBCollection<CreditsEXT>();
            public EOBCollection<Credit> PaymentCredit = new EOBCollection<Credit>();
            public EOBCollection<PaymentInsuranceLineReasonCode> PaymentInsuranceLineReasonCode = new EOBCollection<PaymentInsuranceLineReasonCode>();
            public EOBCollection<PaymentInsuranceLineNote> PaymentInsuranceLineNote = new EOBCollection<PaymentInsuranceLineNote>();

            #endregion

        }

        public class EOBCollection<T>
        {

            #region "Variable Declarations"
            protected ArrayList _innerlist;
            #endregion "Variable Declarations"

            #region "Constructor & Destructor"

            public EOBCollection()
            {
                _innerlist = new ArrayList();
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }


            ~EOBCollection()
            {
                Dispose(false);
            }
            #endregion

            #region "Property Procedures"
            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(T item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(T item)
            //Remark - Work Remining for comparision
            {
                bool result = false;
                return result;
            }

            public bool RemoveAt(int index)
            {
                bool result = false;
                _innerlist.RemoveAt(index);
                result = true;
                return result;
            }

            public void Clear()
            {
                _innerlist.Clear();
            }

            public T this[int index]
            {
                get
                { return (T)_innerlist[index]; }
            }

            public bool Contains(T item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(T item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(T[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }
            public EOBCollection<CreditsDTL> CopyCreditDTL()
            {
                EOBCollection<CreditsDTL> objEOBCollection = new EOBCollection<CreditsDTL>();
                CreditsDTL objCreditsDTL ;
                for (int i = 0; i <= _innerlist.Count-1; i++)
                {
                    objCreditsDTL = new CreditsDTL();
                    objCreditsDTL.Credits_ID = ((CreditsDTL)(_innerlist[i])).Credits_ID;
                    objCreditsDTL.Amount = ((CreditsDTL)(_innerlist[i])).Amount;
                    objCreditsDTL.DBReserveAmount = ((CreditsDTL)(_innerlist[i])).DBReserveAmount;
                    objCreditsDTL.CloseDate = ((CreditsDTL)(_innerlist[i])).CloseDate;
                    objCreditsDTL.CreatedDateTime = ((CreditsDTL)(_innerlist[i])).CreatedDateTime;
                    objCreditsDTL.CreditsDTL_ID = ((CreditsDTL)(_innerlist[i])).CreditsDTL_ID;
                    objCreditsDTL.CreditsRef_ID = ((CreditsDTL)(_innerlist[i])).CreditsRef_ID;
                    objCreditsDTL.EntryDesc = ((CreditsDTL)(_innerlist[i])).EntryDesc;
                    objCreditsDTL.EntryType = ((CreditsDTL)(_innerlist[i])).EntryType;
                    objCreditsDTL.IsPaymentVoid = ((CreditsDTL)(_innerlist[i])).IsPaymentVoid;
                    objCreditsDTL.ModifiedDateTime = ((CreditsDTL)(_innerlist[i])).ModifiedDateTime;
                    objCreditsDTL.ReserveID = ((CreditsDTL)(_innerlist[i])).ReserveID;
                    objCreditsDTL.InsuranceID = ((CreditsDTL)(_innerlist[i])).InsuranceID;
                    objEOBCollection.Add(objCreditsDTL);
                }

                return objEOBCollection;

            }
            #endregion
        }

        public class PaymentInsuranceLineReasonCode
        {
            #region "Constructor & Distructor"

            public PaymentInsuranceLineReasonCode()
            {
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~PaymentInsuranceLineReasonCode()
            {
                Dispose(false);
            }

            #endregion

            #region "Variables Declarations"

            private Int64 _nID = 0;
            private Int64 _nClaimNo = 0;
            private Int64 _nEOBPaymentID = 0;
            private Int64 _nEOBID = 0;
            private Int64 _nEOBPaymentDetailID = 0;

            private Int64 _nBillingTransactionID = 0;
            private Int64 _nBillingTransactionDetailID = 0;

            private Int64 _nTrackBillingTransactionID = 0;
            private Int64 _nTrackBillingTransactionDetailID = 0;
            private string _sSubClaimNo = "";


            private string _sReasonCode = "";
            private string _sReasonDescription = "";
            private decimal _dReasonAmount = 0;
            private bool _isNullReasonAmount = true;
            private Int64 _nClinicID = 0;

            private bool _HasData = false;
            private Int64 _nCloseDate = 0;
            private EOBCommentTypeV2 _EOBCommentType = EOBCommentTypeV2.None;

            private Int32 _ReasonCodeSubType = 0;

            #endregion"Variables Declarations"

            #region " Property Procedures "

            public Int64 ID
            { get { return _nID; } set { _nID = value; } }
            public Int64 ClaimNo
            { get { return _nClaimNo; } set { _nClaimNo = value; } }
            public Int64 EOBPaymentID
            { get { return _nEOBPaymentID; } set { _nEOBPaymentID = value; } }
            public Int64 EOBID
            { get { return _nEOBID; } set { _nEOBID = value; } }
            public Int64 EOBPaymentDetailID
            { get { return _nEOBPaymentDetailID; } set { _nEOBPaymentDetailID = value; } }

            public Int64 BillingTransactionID
            { get { return _nBillingTransactionID; } set { _nBillingTransactionID = value; } }
            public Int64 BillingTransactionDetailID
            { get { return _nBillingTransactionDetailID; } set { _nBillingTransactionDetailID = value; } }

            public Int64 TrackBillingTransactionID
            { get { return _nTrackBillingTransactionID; } set { _nTrackBillingTransactionID = value; } }
            public Int64 TrackBillingTransactionDetailID
            { get { return _nTrackBillingTransactionDetailID; } set { _nTrackBillingTransactionDetailID = value; } }
            public string SubClaimNo
            { get { return _sSubClaimNo; } set { _sSubClaimNo = value; } }

            public string ReasonCode
            { get { return _sReasonCode; } set { _sReasonCode = value; } }
            public string ReasonDescription
            { get { return _sReasonDescription; } set { _sReasonDescription = value; } }
            public decimal ReasonAmount
            { get { return _dReasonAmount; } set { _dReasonAmount = value; } }
            public bool IsNullReasonAmount
            { get { return _isNullReasonAmount; } set { _isNullReasonAmount = value; } }
            public Int64 ClinicID
            { get { return _nClinicID; } set { _nClinicID = value; } }
            public bool HasData
            { get { return _HasData; } set { _HasData = value; } }
            public Int64 CloseDate
            { get { return _nCloseDate; } set { _nCloseDate = value; } }
            public EOBCommentTypeV2 CommentType
            { get { return _EOBCommentType; } set { _EOBCommentType = value; } }
            public Int32 ReasonCodeSubType
            { get { return _ReasonCodeSubType; } set { _ReasonCodeSubType = value; } }

            #endregion

        }

        public class PaymentInsuranceLineNote
        {
            #region "Constructor & Distructor"

            public PaymentInsuranceLineNote()
            {
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~PaymentInsuranceLineNote()
            {
                Dispose(false);
            }

            #endregion

            #region "Variables Declarations"

            private Int64 _nID = 0;
            private Int64 _nEOBPaymentID = 0;
            private Int64 _nEOBID = 0;
            private Int64 _nEOBPaymentDetailID = 0;

            private Int64 _nClaimNo = 0;
            private Int64 _nBillingTransactionID = 0;
            private Int64 _nBillingTransactionDetailID = 0;

            private string _sSubClaimNo = "";
            private Int64 _nTrackBillingTransactionID = 0;
            private Int64 _nTrackBillingTransactionDetailID = 0;
            private int _nTrackBillingTransactionLineNo = 0;

            private string _sReasonCode = "";
            private string _sReasonDescription = "";
            private decimal _dReasonAmount = 0;
            private bool _bIncludeOnPrint = false;

            private Int64 _nClinicID = 0;

            private PaymentTypeV2 _nPaymentNoteType = PaymentTypeV2.None;
            private bool _HasData = false;
            private Int64 _nCloseDate = 0;
            private Int64 _nUserId = 0;

            #endregion"Variables Declarations"

            #region " Property Procedures "

            public Int64 ID
            { get { return _nID; } set { _nID = value; } }
            public Int64 ClaimNo
            { get { return _nClaimNo; } set { _nClaimNo = value; } }
            public Int64 EOBPaymentID
            { get { return _nEOBPaymentID; } set { _nEOBPaymentID = value; } }
            public Int64 EOBID
            { get { return _nEOBID; } set { _nEOBID = value; } }
            public Int64 EOBPaymentDetailID
            { get { return _nEOBPaymentDetailID; } set { _nEOBPaymentDetailID = value; } }
            public Int64 BillingTransactionID
            { get { return _nBillingTransactionID; } set { _nBillingTransactionID = value; } }
            public Int64 BillingTransactionDetailID
            { get { return _nBillingTransactionDetailID; } set { _nBillingTransactionDetailID = value; } }
            public string Code
            { get { return _sReasonCode; } set { _sReasonCode = value; } }
            public string Description
            { get { return _sReasonDescription; } set { _sReasonDescription = value; } }
            public decimal Amount
            { get { return _dReasonAmount; } set { _dReasonAmount = value; } }
            public Int64 ClinicID
            { get { return _nClinicID; } set { _nClinicID = value; } }
            public PaymentTypeV2 PaymentNoteType
            { get { return _nPaymentNoteType; } set { _nPaymentNoteType = value; } }
            
            public bool HasData
            { get { return _HasData; } set { _HasData = value; } }
            public bool IncludeOnPrint
            { get { return _bIncludeOnPrint; } set { _bIncludeOnPrint = value; } }

            public string SubClaimNo
            { get { return _sSubClaimNo; } set { _sSubClaimNo = value; } }
            public Int64 TrackBillingTransactionID
            { get { return _nTrackBillingTransactionID; } set { _nTrackBillingTransactionID = value; } }
            public Int64 TrackBillingTransactionDetailID
            { get { return _nTrackBillingTransactionDetailID; } set { _nTrackBillingTransactionDetailID = value; } }
            public int TrackBillingTransactionLineNo
            { get { return _nTrackBillingTransactionLineNo; } set { _nTrackBillingTransactionLineNo = value; } }
            public Int64 CloseDate
            { get { return _nCloseDate; } set { _nCloseDate = value; } }
            public Int64 UserId
            { get { return _nUserId; } set { _nUserId = value; } }


            #endregion

        }

        public class Debits
        {
            #region "Constructor & Distructor"

            public Debits()
            {
                //_LineNotes = new PaymentInsuranceLineNotes();
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~Debits()
            {
                Dispose(false);
            }

            #endregion

            #region "Variable Declarations"

            private Int64 _nDebitID = 0;
            private Int64 _nCreditID = 0;
            private Int64 _nCredit_RefID = 0;
            private Int64 _nEOBID = 0;
            private Int64 _nEOBDetailID = 0;
            private PaymentEntryTypeV2 nEntryType = PaymentEntryTypeV2.None;
            private string sEntryDesc = "";
            private Int64 nPatientID = 0;
            private Int64 nAccountID = 0;
            private Int64 nAccGuarantorID = 0;
            private Int64 nMSTTransactionID = 0;
            private Int64 nMSTTransactionDetailID = 0;
            private Int64 nTrackTransactionID = 0;
            private Int64 nTrackTransactionDetailID = 0;
            private string sCPTCode = "";
            private string sCPTDesc = "";
            private decimal dPaymentAmount = 0;
            private decimal dWriteoffAmount = 0;
            private decimal dWithholdAmount = 0;
            private decimal dOtherAdjustmentAmount = 0;
            private Int64 nPaymentTrayID = 0;
            private string sPaymentTrayDesc = "";
            private string sUserName = string.Empty;
            private Int64 nUserID = 0;
            private DateTime dtCloseDate = DateTime.Now;
            private DateTime dtCreatedDateTime = DateTime.Now;
            private DateTime dtModifiedDateTime = DateTime.Now;
            private bool bIsPaymentVoid = false;
            private Int64 nVoidType = 0;
            private DateTime dtPaymentVoidCloseDate = DateTime.Now;
            private DateTime dtPaymentVoidDateTime = DateTime.Now;
            private bool bIsERAPost = false;
            private string sVersion = "";
            private string sMachineName = "";
            private string sSiteID = "";
            private Int64 nClinicID = 0;
            private Int64 nPatientInsuranceID = 0;
            private Int64 nContactID = 0;
            private Int64 nInsCompanyID = 0;
            private Int64 nVoidRefID = 0;
            private Int64 nAccSelectedPatient = 0;
            private bool isNullAmount = false;
            private bool _IsNullWriteOff = true;
        //    private bool _IsNullNonCovered = true;
            private bool _IsNullInsurance = true;
         //   private bool _IsNullCopay = true;
         //   private bool _IsNullDeductible = true;
         //   private bool _IsNullCoInsurance = true;
            private bool _IsNullWithhold = true;
            private bool _IsNullAllowed = true;
            private bool _IsNullOtherAdj = true;
            #endregion "Variable Declarations"

            #region "Property Procedures"
            public Int64 nDebitID
            {
                set
                {
                    this._nDebitID = value;
                }
                get
                {
                    return _nDebitID;
                }
            }
            public Int64 nCreditID
            {
                set
                {
                    this._nCreditID = value;
                }
                get
                {
                    return _nCreditID;
                }
            }
            public Int64 nCredit_RefID
            {
                set
                {
                    this._nCredit_RefID = value;
                }
                get
                {
                    return _nCredit_RefID;
                }
            }
            public Int64 nEOBID
            {
                set
                {
                    this._nEOBID = value;
                }
                get
                {
                    return _nEOBID;
                }
            }
            public Int64 nEOBDetailID
            {
                set
                {
                    this._nEOBDetailID = value;
                }
                get
                {
                    return _nEOBDetailID;
                }
            }
            public PaymentEntryTypeV2 EntryType
            {
                set
                {
                    this.nEntryType = value;
                }
                get
                {
                    return nEntryType;
                }
            }
            public string EntryDesc
            {
                set
                {
                    this.sEntryDesc = value;
                }
                get
                {
                    return sEntryDesc;
                }
            }
            public Int64 PatientID
            {
                set
                {
                    this.nPatientID = value;
                }
                get
                {
                    return nPatientID;
                }
            }
            public Int64 AccountID
            {
                set
                {
                    this.nAccountID = value;
                }
                get
                {
                    return nAccountID;
                }
            }
            public Int64 AccGuarantorID
            {
                set
                {
                    this.nAccGuarantorID = value;
                }
                get
                {
                    return nAccGuarantorID;
                }
            }

            public Int64 UserID
            {
                set
                {
                    this.nUserID = value;
                }
                get
                {
                    return nUserID;
                }
            }

            public string UserName
            {
                set
                {
                    this.sUserName = value;
                }
                get
                {
                    return sUserName;
                }
            }

            public Int64 MSTTransactionID
            {
                set
                {
                    this.nMSTTransactionID = value;
                }
                get
                {
                    return nMSTTransactionID;
                }
            }
            public Int64 MSTTransactionDetailID
            {
                set
                {
                    this.nMSTTransactionDetailID = value;
                }
                get
                {
                    return nMSTTransactionDetailID;
                }
            }
            public Int64 TrackTransactionID
            {
                set
                {
                    this.nTrackTransactionID = value;
                }
                get
                {
                    return nTrackTransactionID;
                }
            }
            public Int64 TrackTransactionDetailID
            {
                set
                {
                    this.nTrackTransactionDetailID = value;
                }
                get
                {
                    return nTrackTransactionDetailID;
                }
            }
            public string CPTCode
            {
                set
                {
                    this.sCPTCode = value;
                }
                get
                {
                    return sCPTCode;
                }
            }
            public string CPTDesc
            {
                set
                {
                    this.sCPTDesc = value;
                }
                get
                {
                    return sCPTDesc;
                }
            }
            public decimal PaymentAmount
            {
                set
                {
                    this.dPaymentAmount = value;
                }
                get
                {
                    return dPaymentAmount;
                }
            }
            public decimal WriteoffAmount
            {
                set
                {
                    this.dWriteoffAmount = value;
                }
                get
                {
                    return dWriteoffAmount;
                }
            }
            public decimal WithholdAmount
            {
                set
                {
                    this.dWithholdAmount = value;
                }
                get
                {
                    return dWithholdAmount;
                }
            }
            public decimal OtherAdjustmentAmount
            {
                set
                {
                    this.dOtherAdjustmentAmount = value;
                }
                get
                {
                    return dOtherAdjustmentAmount;
                }
            }
            public Int64 PaymentTrayID
            {
                set
                {
                    this.nPaymentTrayID = value;
                }
                get
                {
                    return nPaymentTrayID;
                }
            }
            public string PaymentTrayDesc
            {
                set
                {
                    this.sPaymentTrayDesc = value;
                }
                get
                {
                    return sPaymentTrayDesc;
                }
            }

            public DateTime CloseDate
            {
                set
                {
                    this.dtCloseDate = value;
                }
                get
                {
                    return dtCloseDate;
                }
            }
            public DateTime CreatedDateTime
            {
                set
                {
                    this.dtCreatedDateTime = value;
                }
                get
                {
                    return dtCreatedDateTime;
                }
            }
            public DateTime ModifiedDateTime
            {
                set
                {
                    this.dtModifiedDateTime = value;
                }
                get
                {
                    return dtModifiedDateTime;
                }
            }
            public bool IsPaymentVoid
            {
                set
                {
                    this.bIsPaymentVoid = value;
                }
                get
                {
                    return bIsPaymentVoid;
                }
            }
            public Int64 VoidType
            {
                set
                {
                    this.nVoidType = value;
                }
                get
                {
                    return nVoidType;
                }
            }
            public DateTime PaymentVoidCloseDate
            {
                set
                {
                    this.dtCloseDate = value;
                }
                get
                {
                    return dtCloseDate;
                }
            }
            public DateTime PaymentVoidDateTime
            {
                set
                {
                    this.dtPaymentVoidDateTime = value;
                }
                get
                {
                    return dtPaymentVoidDateTime;
                }
            }
            public bool IsERAPost
            {
                set
                {
                    this.bIsERAPost = value;
                }
                get
                {
                    return bIsERAPost;
                }
            }
            public string Version
            {
                set
                {
                    this.sVersion = value;
                }
                get
                {
                    return sVersion;
                }
            }
            public string MachineName
            {
                set
                {
                    this.sMachineName = value;
                }
                get
                {
                    return sMachineName;
                }
            }
            public string SiteID
            {
                set
                {
                    this.sSiteID = value;
                }
                get
                {
                    return sSiteID;
                }
            }
            public Int64 ClinicID
            {
                set
                {
                    this.nClinicID = value;
                }
                get
                {
                    return nClinicID;
                }
            }

            public Int64 PatientInsuranceID
            {
                set
                {
                    this.nPatientInsuranceID = value;
                }
                get
                {
                    return nPatientInsuranceID;
                }
            }
            public Int64 ContactID
            {
                set
                {
                    this.nContactID = value;
                }
                get
                {
                    return nContactID;
                }
            }
            public Int64 InsCompanyID
            {
                set
                {
                    this.nInsCompanyID = value;
                }
                get
                {
                    return nInsCompanyID;
                }
            }
            public Int64 VoidRefID
            {
                set
                {
                    this.nVoidRefID = value;
                }
                get
                {
                    return nVoidRefID;
                }
            }
            public Int64 AccSelectedPatient
            {
                set
                {
                    this.nAccSelectedPatient = value;
                }
                get
                {
                    return nAccSelectedPatient;
                }
            }
            public bool IsNullAmount
            {
                set
                {
                    this.isNullAmount = value;
                }
                get
                {
                    return isNullAmount;
                }
            }

            public bool IsNullAllowed
            { get { return _IsNullAllowed; } set { _IsNullAllowed = value; } }
            public bool IsNullWriteOff
            { get { return _IsNullWriteOff; } set { _IsNullWriteOff = value; } }
            public bool IsNullInsurance
            { get { return _IsNullInsurance; } set { _IsNullInsurance = value; } }
            public bool IsNullWithhold
            { get { return _IsNullWithhold; } set { _IsNullWithhold = value; } }
            public bool IsNullOtherAdj
            { get { return _IsNullOtherAdj; } set { _IsNullOtherAdj = value; } }

            #endregion "Property Procedures"

        }

        public class Reserves
        {
            #region "Constructor & Distructor"

            public Reserves()
            {
                //_LineNotes = new PaymentInsuranceLineNotes();
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~Reserves()
            {
                Dispose(false);
            }

            #endregion "Constructor & Distructor"

            #region "Variable Declarations"

            private Int64 nReserveID = 0;
            private Int64 _nCreditID = 0;
            private Int64 nCredits_RefID = 0;
            private decimal dReserveAmount = 0;
            private ReserveEntryTypeV2 nReserveType = ReserveEntryTypeV2.None;
            private string sReserveNote = string.Empty;
            private string sMachineName = string.Empty;
            private Int64 nAccountID = 0;
            private Int64 nGuarantorID = 0;
            private Int64 nUserID = 0;
            private string sUserName = "";
            private DateTime dtCloseDate = DateTime.Now;
            private DateTime dtPaymentVoidCloseDate = DateTime.Now;
            private bool bIsPaymentVoid = false;
            private VoidTypeV2 nVoidType = 0;
     //       private Int64 nPaymentVoidCloseDate = 0;
            private DateTime dtPaymentVoidDateTime = DateTime.Now;
            private DateTime dtCreatedDateTime = DateTime.Now;
            private DateTime dtModifiedDateTime = DateTime.Now;
            private Int64 nPatientID = 0;
            private Int64 nProviderID = 0;
            private Int64 nInsCompanyID = 0;
            private string sInsCompanyName = "";
            private Int64 nAccSelectedPatient = 0;
            private Int64 nMSTTransactionID = 0;
            private Int64 nTransactionID = 0;
            private string sPatientName = "";
            private string sProviderName = "";
            
            private string sClaimNo= "";

            private DateTime _CloseDateOffset = DateTime.Now;
            private Int64 _PaymentTryID = 0;
            private string _PaymentTryDesc = "";

            #endregion "Variable Declarations"

            #region "Property Procedures"
            public Int64 ReserveID
            {
                set
                {
                    this.nReserveID = value;
                }
                get
                {
                    return nReserveID;
                }
            }
            public Int64 nCreditID
            {
                set
                {
                    this._nCreditID = value;
                }
                get
                {
                    return _nCreditID;
                }
            }
            public Int64 Credits_RefID
            {
                set
                {
                    this.nCredits_RefID = value;
                }
                get
                {
                    return nCredits_RefID;
                }
            }
            public decimal ReserveAmount
            {
                set
                {
                    this.dReserveAmount = value;
                }
                get
                {
                    return dReserveAmount;
                }
            }
            public ReserveEntryTypeV2 ReserveType
            {
                set
                {
                    this.nReserveType = value;
                }
                get
                {
                    return nReserveType;
                }
            }
            public string ReserveNote
            {
                set
                {
                    this.sReserveNote = value;
                }
                get
                {
                    return sReserveNote;
                }
            }
            public Int64 AccountID
            {
                set
                {
                    this.nAccountID = value;
                }
                get
                {
                    return nAccountID;
                }
            }
            public Int64 GuarantorID
            {
                set
                {
                    this.nGuarantorID = value;
                }
                get
                {
                    return nGuarantorID;
                }
            }
            public Int64 UserID
            {
                set
                {
                    this.nUserID = value;
                }
                get
                {
                    return nUserID;
                }
            }
            public string UserName
            {
                set
                {
                    this.sUserName = value;
                }
                get
                {
                    return sUserName;
                }
            }
            public DateTime CloseDate
            {
                set
                {
                    this.dtCloseDate = value;
                }
                get
                {
                    return dtCloseDate;
                }
            }
            public bool IsPaymentVoid
            {
                set
                {
                    this.bIsPaymentVoid = value;
                }
                get
                {
                    return bIsPaymentVoid;
                }
            }
            public VoidTypeV2 VoidType
            {
                set
                {
                    this.nVoidType = value;
                }
                get
                {
                    return nVoidType;
                }
            }
            public DateTime PaymentVoidCloseDate
            {
                set
                {
                    this.dtPaymentVoidCloseDate = value;
                }
                get
                {
                    return dtPaymentVoidCloseDate;
                }
            }
            public DateTime PaymentVoidDateTime
            {
                set
                {
                    this.dtPaymentVoidDateTime = value;
                }
                get
                {
                    return dtPaymentVoidDateTime;
                }
            }
            public DateTime CreatedDateTime
            {
                set
                {
                    this.dtCreatedDateTime = value;
                }
                get
                {
                    return dtCreatedDateTime;
                }
            }
            public DateTime ModifiedDateTime
            {
                set
                {
                    this.dtModifiedDateTime = value;
                }
                get
                {
                    return dtModifiedDateTime;
                }
            }
            public string MachineName
            {
                set
                {
                    this.sMachineName = value;
                }
                get
                {
                    return sMachineName;
                }
            }
            public Int64 PatientID
            {
                set
                {
                    this.nPatientID = value;
                }
                get
                {
                    return nPatientID;
                }
            }
            public Int64 ProviderID
            { set { this.nProviderID = value; }
                get { return nProviderID; }
            }
            public Int64 InsCompanyID
            {
                set
                {
                    this.nInsCompanyID = value;
                }
                get
                {
                    return nInsCompanyID;
                }
            }
            public string InsCompanyName
            {
                set
                {
                    this.sInsCompanyName = value;
                }
                get
                {
                    return sInsCompanyName;
                }
            }
            public Int64 AccSelectedPatient
            {
                set
                {
                    this.nAccSelectedPatient = value;
                }
                get
                {
                    return nAccSelectedPatient;
                }
            }

            public Int64 MSTTransactionID
            { get { return nMSTTransactionID;} set {this.nMSTTransactionID= value;} }

            public Int64 TransactionID
            { get { return nTransactionID;} set { this.nTransactionID= value;} }

            public String PatientName
            { get {return sPatientName;} set{this.sPatientName = value;}}

            public String ProviderName
            { get { return sProviderName; } set { this.sProviderName = value; } }

            public String ClaimNo
            { get { return sClaimNo; } set { sClaimNo = value; } }

            public Int64 BusinessCenterID { get; set; }

            public Int64 ClaimsAccountID { get; set; }

            public DateTime  CloseDateOffset
            { get { return _CloseDateOffset; } set { this._CloseDateOffset = value; } }
            public Int64 PaymentTryID
            { get { return _PaymentTryID; } set { this._PaymentTryID = value; } }
            public String PaymentTryDesc
            { get { return _PaymentTryDesc; } set { this._PaymentTryDesc = value; } }
            #endregion "Property Procedures"
        }

        public class EOB
        {
            #region "Constructor & Distructor"

            public EOB()
            {
                //_LineNotes = new PaymentInsuranceLineNotes();
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~EOB()
            {
                Dispose(false);
            }

            #endregion "Constructor & Distructor"

            #region "Variable Declaration"

            private Int64 _nCreditID = 0;
            private Int64 nEOBID = 0;
            private Int64 nEOBDetailID = 0;
            private string sEntryDesc = string.Empty;
            private Int64 nPatientID = 0;
            private Int64 nAccountID = 0;
            private Int64 nAccountPatientID = 0;
            private Int64 nAccGuarantorID = 0;
            private Int64 nBillingTransactionID = 0;
            private Int64 nBillingTransactionDetailID = 0;
            private Int64 nTrackTransactionID = 0;
            private Int64 nTrackTransactionDetailID = 0;
            private string sCPTCode = string.Empty;
            private decimal dTotalChargeAmount = 0;
            private decimal dAllowedAmount = 0;
            private decimal dPaymentAmount = 0;
            private decimal dWriteoffAmount = 0;
            private decimal dCopayAmount = 0;
            private decimal dDeductibleAmount = 0;
            private decimal dWithholdAmount = 0;
            private decimal dCoinsurance = 0;
            private string dNextAction = string.Empty;
            private string dNextParty = string.Empty;
            private Int64 nNextPartyID = 0;
            private DateTime dtCloseDate = DateTime.Now;
            private Int64 nPaymentTrayID = 0;
            private string sPaymentTrayDesc = "";
            private bool bIsPaymentVoid = false;
            private VoidTypeV2 nVoidType = VoidTypeV2.None;
            private DateTime dtPaymentVoidCloseDate = DateTime.Now;
            private DateTime dtPaymentVoidDateTime = DateTime.Now;
            private Int64 nVoidRefID = 0;
            private Int64 nPatientInsuranceID = 0;
            private Int64 nContactID = 0;
            private Int64 nInsCompanyID = 0;
            private bool bIsERAPost;
            private PaymentEntryTypeV2 nEntryType = PaymentEntryTypeV2.None;
            private Int64 nSequenceNo = 0;
            private bool _IsNullCharges = true;
            private bool _IsNullUnit = true;
            private bool _IsNullTotalCharges = true;
            private bool _IsNullAllowed = true;
            private bool _IsNullWriteOff = true;
            private bool _IsNullNonCovered = true;
            private bool _IsNullInsurance = true;
            private bool _IsNullCopay = true;
            private bool _IsNullDeductible = true;
            private bool _IsNullCoInsurance = true;
            private bool _IsNullWithhold = true;
            #endregion "Variable Declaration"

            #region "Property Procedures"
            public Int64 nCreditID
            {
                set
                {
                    this._nCreditID = value;
                }
                get
                {
                    return _nCreditID;
                }
            }

            public Int64 EOBID
            {
                set
                {
                    this.nEOBID = value;
                }
                get
                {
                    return nEOBID;
                }
            }
            public Int64 EOBDetailID
            {
                set
                {
                    this.nEOBDetailID = value;
                }
                get
                {
                    return nEOBDetailID;
                }
            }
            public PaymentEntryTypeV2 EntryType
            {
                set
                {
                    this.nEntryType = value;
                }
                get
                {
                    return nEntryType;
                }
            }
            public string EntryDesc
            {
                set
                {
                    this.sEntryDesc = value;
                }
                get
                {
                    return sEntryDesc;
                }
            }
            public Int64 PatientID
            {
                set
                {
                    this.nPatientID = value;
                }
                get
                {
                    return nPatientID;
                }
            }
            public Int64 AccountPatientID
            {
                set
                {
                    this.nAccountPatientID = value;
                }
                get
                {
                    return nAccountPatientID;
                }
            }
            public Int64 AccountID
            {
                set
                {
                    this.nAccountID = value;
                }
                get
                {
                    return nAccountID;
                }
            }
            public Int64 AccGuarantorID
            {
                set
                {
                    this.nAccGuarantorID = value;
                }
                get
                {
                    return nAccGuarantorID;
                }
            }
            public Int64 MSTTransactionID
            {
                set
                {
                    this.nBillingTransactionID = value;
                }
                get
                {
                    return nBillingTransactionID;
                }
            }
            public Int64 MSTTransactionDetailID
            {
                set
                {
                    this.nBillingTransactionDetailID = value;
                }
                get
                {
                    return nBillingTransactionDetailID;
                }
            }
            public Int64 TrackTransactionID
            {
                set
                {
                    this.nTrackTransactionID = value;
                }
                get
                {
                    return nTrackTransactionID;
                }
            }
            public Int64 TrackTransactionDetailID
            {
                set
                {
                    this.nTrackTransactionDetailID = value;
                }
                get
                {
                    return nTrackTransactionDetailID;
                }
            }
            public string CPTCode
            {
                set
                {
                    this.sCPTCode = value;
                }
                get
                {
                    return sCPTCode;
                }
            }

            public decimal TotalChargeAmount
            {
                set
                {
                    this.dTotalChargeAmount = value;
                }
                get
                {
                    return dTotalChargeAmount;
                }
            }
            public decimal AllowedAmount
            {
                set
                {
                    this.dAllowedAmount = value;
                }
                get
                {
                    return dAllowedAmount;
                }
            }
            public decimal PaymentAmount
            {
                set
                {
                    this.dPaymentAmount = value;
                }
                get
                {
                    return dPaymentAmount;
                }
            }
            public decimal WriteoffAmount
            {
                set
                {
                    this.dWriteoffAmount = value;
                }
                get
                {
                    return dWriteoffAmount;
                }
            }
            public decimal CopayAmount
            {
                set
                {
                    this.dCopayAmount = value;
                }
                get
                {
                    return dCopayAmount;
                }
            }
            public decimal DeductibleAmount
            {
                set
                {
                    this.dDeductibleAmount = value;
                }
                get
                {
                    return dDeductibleAmount;
                }
            }
            public decimal WithholdAmount
            {
                set
                {
                    this.dWithholdAmount = value;
                }
                get
                {
                    return dWithholdAmount;
                }
            }
            public decimal Coinsurance
            {
                set
                {
                    this.dCoinsurance = value;
                }
                get
                {
                    return dCoinsurance;
                }
            }
            public string NextAction
            {
                set
                {
                    this.dNextAction = value;
                }
                get
                {
                    return dNextAction;
                }
            }
            public string NextParty
            {
                set
                {
                    this.dNextParty = value;
                }
                get
                {
                    return dNextParty;
                }
            }
            public Int64 NextPartyID
            {
                set
                {
                    this.nNextPartyID = value;
                }
                get
                {
                    return nNextPartyID;
                }
            }
            public DateTime CloseDate
            {
                set
                {
                    this.dtCloseDate = value;
                }
                get
                {
                    return dtCloseDate;
                }
            }
            public Int64 PaymentTrayID
            {
                set
                {
                    this.nPaymentTrayID = value;
                }
                get
                {
                    return nPaymentTrayID;
                }
            }
            public string PaymentTrayDesc
            {
                set
                {
                    this.sPaymentTrayDesc = value;
                }
                get
                {
                    return sPaymentTrayDesc;
                }
            }

            public bool IsPaymentVoid
            {
                set
                {
                    this.bIsPaymentVoid = value;
                }
                get
                {
                    return bIsPaymentVoid;
                }
            }
            public VoidTypeV2 VoidType
            {
                set
                {
                    this.nVoidType = value;
                }
                get
                {
                    return nVoidType;
                }
            }
            public DateTime PaymentVoidCloseDate
            {
                set
                {
                    this.dtPaymentVoidCloseDate = value;
                }
                get
                {
                    return dtPaymentVoidCloseDate;
                }
            }
            public DateTime PaymentVoidDateTime
            {
                set
                {
                    this.dtPaymentVoidDateTime = value;
                }
                get
                {
                    return dtPaymentVoidDateTime;
                }
            }
            public Int64 VoidRefID
            {
                set
                {
                    this.nVoidRefID = value;
                }
                get
                {
                    return nVoidRefID;
                }
            }
            public Int64 PatientInsuranceID
            {
                set
                {
                    this.nPatientInsuranceID = value;
                }
                get
                {
                    return nPatientInsuranceID;
                }
            }
            public Int64 ContactID
            {
                set
                {
                    this.nContactID = value;
                }
                get
                {
                    return nContactID;
                }
            }
            public Int64 InsCompanyID
            {
                set
                {
                    this.nInsCompanyID = value;
                }
                get
                {
                    return nInsCompanyID;
                }
            }
            public bool IsERAPost
            {
                set
                {
                    this.bIsERAPost = value;
                }
                get
                {
                    return bIsERAPost;
                }
            }

            public Int64 SequenceNo
            {
                set
                {
                    this.nSequenceNo = value;
                }
                get
                {
                    return nSequenceNo;
                }
            }

            public bool IsNullCharges
            { get { return _IsNullCharges; } set { _IsNullCharges = value; } }
            public bool IsNullUnit
            { get { return _IsNullUnit; } set { _IsNullUnit = value; } }
            public bool IsNullTotalCharges
            { get { return _IsNullTotalCharges; } set { _IsNullTotalCharges = value; } }
            public bool IsNullAllowed
            { get { return _IsNullAllowed; } set { _IsNullAllowed = value; } }
            public bool IsNullWriteOff
            { get { return _IsNullWriteOff; } set { _IsNullWriteOff = value; } }
            public bool IsNullNonCovered
            { get { return _IsNullNonCovered; } set { _IsNullNonCovered = value; } }
            public bool IsNullInsurance
            { get { return _IsNullInsurance; } set { _IsNullInsurance = value; } }
            public bool IsNullCopay
            { get { return _IsNullCopay; } set { _IsNullCopay = value; } }
            public bool IsNullDeductible
            { get { return _IsNullDeductible; } set { _IsNullDeductible = value; } }
            public bool IsNullCoInsurance
            { get { return _IsNullCoInsurance; } set { _IsNullCoInsurance = value; } }
            public bool IsNullWithhold
            { get { return _IsNullWithhold; } set { _IsNullWithhold = value; } }

            #endregion "Property Procedures"
        }

        public class EOB_EXT
        {
            #region "Constructor & Distructor"

            public EOB_EXT()
            {
                //_LineNotes = new PaymentInsuranceLineNotes();
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~EOB_EXT()
            {
                Dispose(false);
            }

            #endregion "Constructor & Distructor"

            #region "Variable Declaration"

            private Int64 _nCreditID = 0;
            private Int64 nEOBID = 0;
            private Int64 nEOBDetailID = 0;
            private Int64 nEXTID = 0;
            private Int64 nNextPartyID = 0;
            private string sNextAction = string.Empty;
            private string sNextParty = string.Empty;
            private string sUserName = string.Empty;
            private string sMachineName = string.Empty;
            private string sSiteID = string.Empty;
            private string sVersion = string.Empty;
            private Int64 nUserID = 0;
            private Int64 nSVCID = 0;
            private Int64 nClinicID = 0;
            private Int64 nCLPId = 0;
            private DateTime dtCreatedDateTime = DateTime.Now;
            private DateTime dtModifiedDateTime = DateTime.Now;
            private bool bIsERAPost = false;

            #endregion "Variable Declaration"

            #region "Property Procedures"

            public Int64 nCreditID
            {
                set
                {
                    this._nCreditID = value;
                }
                get
                {
                    return _nCreditID;
                }
            }
            public Int64 EOBID
            {
                set
                {
                    this.nEOBID = value;
                }
                get
                {
                    return nEOBID;
                }
            }
            public Int64 EOBDetailID
            {
                set
                {
                    this.nEOBDetailID = value;
                }
                get
                {
                    return nEOBDetailID;
                }
            }
            public string NextAction
            {
                set
                {
                    this.sNextAction = value;
                }
                get
                {
                    return sNextAction;
                }
            }
            public string NextParty
            {
                set
                {
                    this.sNextParty = value;
                }
                get
                {
                    return sNextParty;
                }
            }
            public Int64 NextPartyID
            {
                set
                {
                    this.nNextPartyID = value;
                }
                get
                {
                    return nNextPartyID;
                }
            }
            public DateTime CreatedDateTime
            {
                set
                {
                    this.dtCreatedDateTime = value;
                }
                get
                {
                    return dtCreatedDateTime;
                }
            }
            public DateTime ModifiedDateTime
            {
                set
                {
                    this.dtModifiedDateTime = value;
                }
                get
                {
                    return dtModifiedDateTime;
                }
            }
            public bool IsERAPost
            {
                set
                {
                    this.bIsERAPost = value;
                }
                get
                {
                    return bIsERAPost;
                }
            }
            public Int64 EXTID
            {
                set
                {
                    this.nEXTID = value;
                }
                get
                {
                    return nEXTID;
                }
            }
            public string UserName
            {
                set
                {
                    this.sUserName = value;
                }
                get
                {
                    return sUserName;
                }
            }
            public string MachineName
            {
                set
                {
                    this.sMachineName = value;
                }
                get
                {
                    return sMachineName;
                }
            }
            public string SiteID
            {
                set
                {
                    this.sSiteID = value;
                }
                get
                {
                    return sSiteID;
                }
            }
            public string Version
            {
                set
                {
                    this.sVersion = value;
                }
                get
                {
                    return sVersion;
                }
            }
            public Int64 UserID
            {
                set
                {
                    this.nUserID = value;
                }
                get
                {
                    return nUserID;
                }
            }
            public Int64 SVCID
            {
                set
                {
                    this.nSVCID = value;
                }
                get
                {
                    return nSVCID;
                }
            }
            public Int64 ClinicID
            {
                set
                {
                    this.nClinicID = value;
                }
                get
                {
                    return nClinicID;
                }
            }
            public Int64 CLPId
            {
                set
                {
                    this.nCLPId = value;
                }
                get
                {
                    return nCLPId;
                }
            }

            #endregion "Property Procedures"
        }

        public class CreditsDTL
        {
            #region "Constructor & Distructor"

            public CreditsDTL()
            {
                //_LineNotes = new PaymentInsuranceLineNotes();
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~CreditsDTL()
            {
                Dispose(false);
            }

            #endregion "Constructor & Distructor"

            #region "Variable Declaration"

            private bool bIsPaymentVoid = false;
            private Int64 nCreditsDTL_ID = 0;
            private Int64 nCredits_ID = 0;
            private Int64 nCreditsRef_ID = 0;
            private decimal dAmount = 0;
            private decimal dbReserveAmount = 0;
            private PaymentEntryTypeV2 nEntryType = PaymentEntryTypeV2.None;
            private string sEntryDesc = "";
            private DateTime dtCloseDate;
            private DateTime dtCreatedDateTime;
            private DateTime dtModifiedDateTime;
            private Int64 nReserveID = 0;
            private Int64 nInsuranceID = 0;

            #endregion "Variable Declaration"

            #region "Property Procedures"

            public Int64 CreditsDTL_ID
            {
                set
                {
                    this.nCreditsDTL_ID = value;
                }
                get
                {
                    return nCreditsDTL_ID;
                }
            }
            public Int64 ReserveID
            {
                set
                {
                    this.nReserveID = value;
                }
                get
                {
                    return nReserveID;
                }
            }
            public Int64 Credits_ID
            {
                set
                {
                    this.nCredits_ID = value;
                }
                get
                {
                    return nCredits_ID;
                }
            }
            public Int64 CreditsRef_ID
            {
                set
                {
                    this.nCreditsRef_ID = value;
                }
                get
                {
                    return nCreditsRef_ID;
                }
            }
            public decimal Amount
            {
                set
                {
                    this.dAmount = value;
                }
                get
                {
                    return dAmount;
                }
            }
            public PaymentEntryTypeV2 EntryType
            {
                set
                {
                    this.nEntryType = value;
                }
                get
                {
                    return nEntryType;
                }
            }
            public string EntryDesc
            {
                set
                {
                    this.sEntryDesc = value;
                }
                get
                {
                    return sEntryDesc;
                }
            }
            public DateTime CloseDate
            {
                set
                {
                    this.dtCloseDate = value;
                }
                get
                {
                    return dtCloseDate;
                }
            }
            public DateTime CreatedDateTime
            {
                set
                {
                    this.dtCreatedDateTime = value;
                }
                get
                {
                    return dtCreatedDateTime;
                }
            }
            public DateTime ModifiedDateTime
            {
                set
                {
                    this.dtModifiedDateTime = value;
                }
                get
                {
                    return dtModifiedDateTime;
                }
            }
            public bool IsPaymentVoid
            {
                set
                {
                    this.bIsPaymentVoid = value;
                }
                get
                {
                    return bIsPaymentVoid;
                }
            }
            public Int64 InsuranceID
            { get { return nInsuranceID; } set { nInsuranceID = value; } }
            public decimal DBReserveAmount
            {
                set
                {
                    this.dbReserveAmount = value;
                }
                get
                {
                    return dbReserveAmount;
                }
            }
            
            #endregion "Property Procedures"



        }

        public class CreditsEXT
        {
            #region "Constructor & Distructor"

            public CreditsEXT()
            {
                //_LineNotes = new PaymentInsuranceLineNotes();
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~CreditsEXT()
            {
                Dispose(false);
            }

            #endregion "Constructor & Distructor"

            #region "Variable Declaration"

            private Int64 nID = 0;
            private Int64 nCredits_ID = 0;
            private string sCreditCardType = "";
            private DateTime dtPaymentVoidDateTime;
            private DateTime dtCreatedDateTime;
            private DateTime dtModifiedDateTime;
            private string sVersion = "";
            private string sMachineName = "";
            private string sSiteID = "";
            private string sAuthorizationNo = "";
            private Int64 nClinicID = 0;
            private Int64 nBPRID = 0;
            private bool bIsERAPost = false;
            private bool bIsFinished = false;

            #endregion "Variable Declaration"

            #region "Property Procedures"

            public bool IsERAPost
            {
                set
                {
                    this.bIsERAPost = value;
                }
                get
                {
                    return bIsERAPost;
                }
            }
            public bool IsFinished
            {
                set
                {
                    this.bIsFinished = value;
                }
                get
                {
                    return bIsFinished;
                }
            }
            public Int64 Credits_ID
            {
                set
                {
                    this.nCredits_ID = value;
                }
                get
                {
                    return nCredits_ID;
                }
            }
            public Int64 Credits_EXTID
            {
                set
                {
                    this.nID = value;
                }
                get
                {
                    return nID;
                }
            }
            public Int64 BPRID
            {
                set
                {
                    this.nBPRID = value;
                }
                get
                {
                    return nBPRID;
                }
            }
            public string CreditCardType
            {
                set
                {
                    this.sCreditCardType = value;
                }
                get
                {
                    return sCreditCardType;
                }
            }
            public string AuthorizationNo
            {
                set
                {
                    this.sAuthorizationNo = value;
                }
                get
                {
                    return sAuthorizationNo;
                }
            }
            public DateTime PaymentVoidDateTime
            {
                set
                {
                    this.dtPaymentVoidDateTime = value;
                }
                get
                {
                    return dtPaymentVoidDateTime;
                }
            }
            public DateTime CreatedDateTime
            {
                set
                {
                    this.dtCreatedDateTime = value;
                }
                get
                {
                    return dtCreatedDateTime;
                }
            }
            public DateTime ModifiedDateTime
            {
                set
                {
                    this.dtModifiedDateTime = value;
                }
                get
                {
                    return dtModifiedDateTime;
                }
            }
            public string Version
            {
                set
                {
                    this.sVersion = value;
                }
                get
                {
                    return sVersion;
                }
            }
            public string MachineName
            {
                set
                {
                    this.sMachineName = value;
                }
                get
                {
                    return sMachineName;
                }
            }
            public string SiteID
            {
                set
                {
                    this.sSiteID = value;
                }
                get
                {
                    return sSiteID;
                }
            }
            public Int64 ClinicID
            {
                set
                {
                    this.nClinicID = value;
                }
                get
                {
                    return nClinicID;
                }
            }

            #endregion "Property Procedures"
        }

        public class PaymentInsuranceClaim
        {
            #region "Private Variables"

            private Int64 _ClaimNo = 0;
            private string _DisplayClaimNo = "";
            private string _ClaimNoPrefix = "";
            private Int64 _BillingTransactionID = 0;
            private Int64 _BillingTransactionDate = 0;
            private Int64 _PatientID = 0;
            private string _PatientName = "";
            private PaymentInsuranceLines _CliamLines = null;
            private Int64 _ClinicId = 0;

            private string _SubClaimNo = "";
            private Int64 _TrackBillingTrnID = 0;
            private Int16 _FacilityType = 0;


            #endregion

            #region "Constructor & Distructor"


            public PaymentInsuranceClaim()
            {
                _CliamLines = new PaymentInsuranceLines();
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        // _CliamLines.Dispose();
                    }
                }
                disposed = true;
            }

            ~PaymentInsuranceClaim()
            {
                Dispose(false);
            }

            #endregion

            #region " Property Procedures "

            public Int64 ClaimNo
            {
                get { return _ClaimNo; }
                set { _ClaimNo = value; }
            }
            public string DisplayClaimNo
            {
                get { return _DisplayClaimNo; }
                set { _DisplayClaimNo = value; }
            }
            public string ClaimNoPrefix
            {
                get { return _ClaimNoPrefix; }
                set { _ClaimNoPrefix = value; }
            }
            public Int64 BillingTransactionID
            {
                get { return _BillingTransactionID; }
                set { _BillingTransactionID = value; }
            }
            public Int64 BillingTransactionDate
            {
                get { return _BillingTransactionDate; }
                set { _BillingTransactionDate = value; }
            }
            public Int64 PatientID
            {
                get { return _PatientID; }
                set { _PatientID = value; }
            }
            public string PatientName
            {
                get { return _PatientName; }
                set { _PatientName = value; }
            }
            public Int64 ClinicID
            {
                get { return _ClinicId; }
                set { _ClinicId = value; }
            }
            public PaymentInsuranceLines CliamLines
            {
                get { return _CliamLines; }
                set { _CliamLines = value; }
            }
            public string SubClaimNo
            { get { return _SubClaimNo; } set { _SubClaimNo = value; } }

            public Int64 TrackBillingTrnID
            { get { return _TrackBillingTrnID; } set { _TrackBillingTrnID = value; } }

            public Int16 FacilityType
            {
                get { return _FacilityType; }
                set { _FacilityType = value; }
            }
            #endregion " Property Procedures "
        }

        public class PaymentInsuranceLine
        {
            #region "Constructor & Distructor"

            public PaymentInsuranceLine()
            {
                //_EOBInsurancePaymentLineDetails = new Debits();
                //_LineResonCodes = new PaymentInsuranceLineResonCodes();
                //_LineNextAction = new PaymentInsuranceLineNextAction();
                //_LineNotes = new PaymentInsuranceLineNotes();

            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                    }
                }
                disposed = true;
            }

            ~PaymentInsuranceLine()
            {
                Dispose(false);
            }

            #endregion

            #region " Private & Public Variables "

            private Int64 _PatientID = 0;

            private Int64 _BLTransactionID = 0;
            private Int64 _BLTransactionDetailID = 0;
            private Int64 _BLTransactionLineNo = 0;
            private Int64 _ClaimNumber = 0;

            private Int64 _TrackBLTransactionID = 0;
            private Int64 _TrackBLTransactionDetailID = 0;
            private Int64 _TrackBLTransactionLineNo = 0;
            private string _SubClaimNumber = "";
            private string _ClaimOnHold = "";
            private string _sModifier = "";

            private Int64 _DOSFrom = 0;
            private Int64 _DOSTo = 0;
            private string _CPTCode = "";
            private string _CPTDescription = "";

            private Int64 _BLInsuranceId = 0;
            private string _BLInsuranceName = "";
            private InsuranceTypeFlagV2 _BLInsuranceFlag = InsuranceTypeFlagV2.None;

            private decimal _Charges = 0;
            private decimal _Unit = 0;
            private decimal _TotalCharges = 0;
            private decimal _Allowed = 0;
            private decimal _WriteOff = 0;
            private decimal _NonCovered = 0;
            private decimal _Insurance = 0;
            private decimal _Copay = 0;
            private decimal _Deductible = 0;
            private decimal _CoInsurance = 0;
            private decimal _Withhold = 0;

            private bool _IsNullCharges = true;
            private bool _IsNullUnit = true;
            private bool _IsNullTotalCharges = true;
            private bool _IsNullAllowed = false;
            private bool _IsNullWriteOff = true;
            private bool _IsNullNonCovered = true;
            private bool _IsNullInsurance = true;
            private bool _IsNullCopay = true;
            private bool _IsNullDeductible = true;
            private bool _IsNullCoInsurance = true;
            private bool _IsNullWithhold = true;


            private decimal _LinePaid = 0;
            private decimal _LinePaidByPatient = 0;
            private decimal _LinePaidByInsurance = 0;
            private decimal _LinePaidWriteOff = 0;
            private decimal _LinePaidWithHold = 0;
            private decimal _LineBalance = 0;


            private decimal _LinePreviousAdjuestment = 0;
            private decimal _LinePreviousPaid = 0;
            private decimal _LinePreviousPatientPaid = 0;
            private decimal _LinePatientDue = 0;
            private decimal _LineBadDebtDue = 0;
            private decimal _LinePrevPatientAdjustment = 0;

            //Fields used for modify 
            private Int64 _mEOBId = 0;
            private Int64 _mEOBDtlID = 0;
            private Int64 _mEOBPaymentId = 0;

            private Int64 _paymentTrayId = 0;
            private string _paymentTrayCode = "";
            private string _paymentTrayDesc = "";
            private DateTime _createdDateTime = DateTime.Now;
            private DateTime _modifiedDateTime = DateTime.Now;

            private Int64 _PatInsuranceId = 0;
            private Int64 _InsContactId = 0;

            private Int64 _userId = 0;
            private string _userName = "";

            private Int64 _clinicId = 0;

            //....Field to identify whether the eob entry is for patient payment or insurance
            private PaymentTypeV2 _eobType = PaymentTypeV2.None;

            private Debits _EOBInsurancePaymentLineDetails = null;
            private decimal _last_allowed = 0;
            private decimal _last_payment = 0;
            private decimal _last_writeoff = 0;
            private decimal _last_copay = 0;
            private decimal _last_deductible = 0;
            private decimal _last_coinsurance = 0;
            private decimal _last_withhold = 0;
            private Int64 _Last_EOBID = 0;
            private bool _iscorrection = false;
            private string _sModifiers = "";
            private Int64 _InsCompanyid = 0;

            private Int64 _closeDate = 0;

            private bool _IsSplitted = false;
            private bool _bNonServiceCode = false;
            private bool _bIsERAPayment = false;



            //Changes Made by Subashish date 13-01-2011---------------------Start------------------
            //For Declaring Property Variable 

            private Int64 _nPAccountID = 0;
            private Int64 _nAccountPatientID = 0;
            private Int64 _nGuarantorID = 0;

            //Changes Mage by Subashish date 13-01-2011---------------------Close-----------


            #endregion " Private & Public Variables "

            #region " Property Procedures "

            public string Modifiers
            { get { return _sModifiers; } set { _sModifiers = value; } }
            public Int64 PatientID
            { get { return _PatientID; } set { _PatientID = value; } }

            public Int64 BLTransactionID
            { get { return _BLTransactionID; } set { _BLTransactionID = value; } }
            public Int64 BLTransactionDetailID
            { get { return _BLTransactionDetailID; } set { _BLTransactionDetailID = value; } }
            public Int64 BLTransactionLineNo
            { get { return _BLTransactionLineNo; } set { _BLTransactionLineNo = value; } }
            public Int64 ClaimNumber
            { get { return _ClaimNumber; } set { _ClaimNumber = value; } }

            public Int64 TrackBLTransactionID
            { get { return _TrackBLTransactionID; } set { _TrackBLTransactionID = value; } }
            public Int64 TrackBLTransactionDetailID
            { get { return _TrackBLTransactionDetailID; } set { _TrackBLTransactionDetailID = value; } }
            public Int64 TrackBLTransactionLineNo
            { get { return _TrackBLTransactionLineNo; } set { _TrackBLTransactionLineNo = value; } }
            public string SubClaimNumber
            { get { return _SubClaimNumber; } set { _SubClaimNumber = value; } }
            public string ClaimOnHold
            { get { return _ClaimOnHold; } set { _ClaimOnHold = value; } }

            public Int64 DOSFrom
            { get { return _DOSFrom; } set { _DOSFrom = value; } }
            public Int64 DOSTo
            { get { return _DOSTo; } set { _DOSTo = value; } }

            public string CPTCode
            { get { return _CPTCode; } set { _CPTCode = value; } }
            public string CPTDescription
            { get { return _CPTDescription; } set { _CPTDescription = value; } }

            string _CrossWalkCPTCode = "";
            string _CrossWalkCPTDescription = "";

            public string CrossWalkCPTCode
            { get { return _CrossWalkCPTCode; } set { _CrossWalkCPTCode = value; } }
            public string CrossWalkCPTDescription
            { get { return _CrossWalkCPTDescription; } set { _CrossWalkCPTDescription = value; } }

            public string Modifier
            { get { return _sModifier; } set { _sModifier = value; } }

            public Int64 BLInsuranceID
            { get { return _BLInsuranceId; } set { _BLInsuranceId = value; } }
            public string BLInsuranceName
            { get { return _BLInsuranceName; } set { _BLInsuranceName = value; } }
            public InsuranceTypeFlagV2 BLInsuranceFlag
            { get { return _BLInsuranceFlag; } set { _BLInsuranceFlag = value; } }

            public decimal Charges
            { get { return _Charges; } set { _Charges = value; } }
            public decimal Unit
            { get { return _Unit; } set { _Unit = value; } }
            public decimal TotalCharges
            { get { return _TotalCharges; } set { _TotalCharges = value; } }
            public decimal Allowed
            { get { return _Allowed; } set { _Allowed = value; } }
            public decimal WriteOff
            { get { return _WriteOff; } set { _WriteOff = value; } }
            public decimal NonCovered
            { get { return _NonCovered; } set { _NonCovered = value; } }
            public decimal InsuranceAmount
            { get { return _Insurance; } set { _Insurance = value; } }
            public decimal Copay
            { get { return _Copay; } set { _Copay = value; } }
            public decimal Deductible
            { get { return _Deductible; } set { _Deductible = value; } }
            public decimal CoInsurance
            { get { return _CoInsurance; } set { _CoInsurance = value; } }
            public decimal Withhold
            { get { return _Withhold; } set { _Withhold = value; } }


            public bool IsNullCharges
            { get { return _IsNullCharges; } set { _IsNullCharges = value; } }
            public bool IsNullUnit
            { get { return _IsNullUnit; } set { _IsNullUnit = value; } }
            public bool IsNullTotalCharges
            { get { return _IsNullTotalCharges; } set { _IsNullTotalCharges = value; } }
            public bool IsNullAllowed
            { get { return _IsNullAllowed; } set { _IsNullAllowed = value; } }
            public bool IsNullWriteOff
            { get { return _IsNullWriteOff; } set { _IsNullWriteOff = value; } }
            public bool IsNullNonCovered
            { get { return _IsNullNonCovered; } set { _IsNullNonCovered = value; } }
            public bool IsNullInsurance
            { get { return _IsNullInsurance; } set { _IsNullInsurance = value; } }
            public bool IsNullCopay
            { get { return _IsNullCopay; } set { _IsNullCopay = value; } }
            public bool IsNullDeductible
            { get { return _IsNullDeductible; } set { _IsNullDeductible = value; } }
            public bool IsNullCoInsurance
            { get { return _IsNullCoInsurance; } set { _IsNullCoInsurance = value; } }
            public bool IsNullWithhold
            { get { return _IsNullWithhold; } set { _IsNullWithhold = value; } }

            public decimal LinePaidAmount
            { get { return _LinePaid; } set { _LinePaid = value; } }
            public decimal LinePaidByPatient
            { get { return _LinePaidByPatient; } set { _LinePaidByPatient = value; } }
            public decimal LinePaidByInsurance
            { get { return _LinePaidByInsurance; } set { _LinePaidByInsurance = value; } }
            public decimal LinePaidWriteOff
            { get { return _LinePaidWriteOff; } set { _LinePaidWriteOff = value; } }
            public decimal LinePaidWithHold
            { get { return _LinePaidWithHold; } set { _LinePaidWithHold = value; } }
            public decimal LineBalance
            { get { return _LineBalance; } set { _LineBalance = value; } }
            public decimal LinePreviousPaid
            { get { return _LinePreviousPaid; } set { _LinePreviousPaid = value; } }
            public decimal LinePreviousPatientPaid
            { get { return _LinePreviousPatientPaid; } set { _LinePreviousPatientPaid = value; } }
            public decimal LinePreviousAdjuestment
            { get { return _LinePreviousAdjuestment; } set { _LinePreviousAdjuestment = value; } }
            public decimal LinePrevPatientAdjustment
            { get { return _LinePrevPatientAdjustment; } set { _LinePrevPatientAdjustment = value; } }
            public decimal LinePatientDue
            { get { return _LinePatientDue; } set { _LinePatientDue = value; } }
            public decimal LineBadDebtDue
            { get { return _LineBadDebtDue; } set { _LineBadDebtDue = value; } }
            
            public Int64 mEOBID
            { get { return _mEOBId; } set { _mEOBId = value; } }
            public Int64 mEOBDtlID
            { get { return _mEOBDtlID; } set { _mEOBDtlID = value; } }
            public Int64 mEOBPaymentID
            { get { return _mEOBPaymentId; } set { _mEOBPaymentId = value; } }
            private Int64 _lastEOBPaymentId = 0;
            public Int64 PaymentTrayID
            { get { return _paymentTrayId; } set { _paymentTrayId = value; } }
            public string PaymentTrayCode
            { get { return _paymentTrayCode; } set { _paymentTrayCode = value; } }
            public string PaymentTrayDesc
            { get { return _paymentTrayDesc; } set { _paymentTrayDesc = value; } }
            public DateTime CreatedDateTime
            { get { return _createdDateTime; } set { _createdDateTime = value; } }
            public DateTime ModifiedDateTime
            { get { return _modifiedDateTime; } set { _modifiedDateTime = value; } }

            public Int64 PatInsuranceID
            { get { return _PatInsuranceId; } set { _PatInsuranceId = value; } }
            public Int64 InsContactID
            { get { return _InsContactId; } set { _InsContactId = value; } }
            public Int64 InsCompanyID
            { get { return _InsCompanyid; } set { _InsCompanyid = value; } }

            public Int64 UserID
            { get { return _userId; } set { _userId = value; } }
            public string UserName
            { get { return _userName; } set { _userName = value; } }

            public Int64 ClinicID
            { get { return _clinicId; } set { _clinicId = value; } }

            //....Field to identify whether the eob entry is for patient payment or insurance
            public PaymentTypeV2 EOBType
            { get { return _eobType; } set { _eobType = value; } }

            public Debits EOBInsurancePaymentLineDetails
            {
                get { return _EOBInsurancePaymentLineDetails; }
                set { _EOBInsurancePaymentLineDetails = value; }
            }

            //public PaymentInsuranceLineReasonCodes LineResonCodes
            //{ get { return _LineResonCodes; } set { _LineResonCodes = value; } }

            //public PaymentInsuranceLineNextAction LineNextAction
            //{ get { return _LineNextAction; } set { _LineNextAction = value; } }

            public decimal Last_allowed
            { get { return _last_allowed; } set { _last_allowed = value; } }
            public decimal Last_payment
            { get { return _last_payment; } set { _last_payment = value; } }
            public decimal Last_writeoff
            { get { return _last_writeoff; } set { _last_writeoff = value; } }
            public decimal Last_copay
            { get { return _last_copay; } set { _last_copay = value; } }
            public decimal Last_deductible
            { get { return _last_deductible; } set { _last_deductible = value; } }
            public decimal Last_coinsurance
            { get { return _last_coinsurance; } set { _last_coinsurance = value; } }
            public decimal Last_withhold
            { get { return _last_withhold; } set { _last_withhold = value; } }
            public Int64 Last_EOBID
            { get { return _Last_EOBID; } set { _Last_EOBID = value; } }

            public bool IsLast_allowedNull { get; set; }
            public bool IsLast_paymentNull { get; set; }
            public bool IsLast_writeoffNull { get; set; }
            public bool IsLast_copayNull { get; set; }
            public bool IsLast_deductibleNull { get; set; }
            public bool IsLast_coinsuranceNull { get; set; }
            public bool IsLast_withholdNull { get; set; }


            public bool Iscorrection
            { get { return _iscorrection; } set { _iscorrection = value; } }

            public Int64 CloseDate
            { get { return _closeDate; } set { _closeDate = value; } }

            public Int64 LastEOBPaymentId
            { get { return _lastEOBPaymentId; } set { _lastEOBPaymentId = value; } }
            private decimal _PatientPaidAmount = 0;
            public decimal PatientPaidAmount
            { get { return _PatientPaidAmount; } set { _PatientPaidAmount = value; } }

            public bool IsSplitted
            { get { return _IsSplitted; } set { _IsSplitted = value; } }

            public bool bNonServiceCode
            { get { return _bNonServiceCode; } set { _bNonServiceCode = value; } }

            private Int64 _SVCID = 0;
            private Int64 _CLPID = 0;
            //public decimal LinePreviousAdjuestment
            //{ get { return _LinePreviousAdjuestment; } set { _LinePreviousAdjuestment = value; } }

            private bool _bIsStopCharge = false;

            public Int64 SVCID
            { get { return _SVCID; } set { _SVCID = value; } }


            public Int64 CLPID
            { get { return _CLPID; } set { _CLPID = value; } }

            public bool IsStopCharge
            { get { return _bIsStopCharge; } set { _bIsStopCharge = value; } }

            public bool bIsERAPayment
            { get { return _bIsERAPayment; } set { _bIsERAPayment = value; } }


            //Changes Made by Subashish date 13-01-2011---------------------Start---------------
            //For Creating properties 

            public Int64 PAccountID
            { get { return _nPAccountID; } set { _nPAccountID = value; } }

            public Int64 AccountPatientID
            { get { return _nAccountPatientID; } set { _nAccountPatientID = value; } }

            public Int64 GuarantorID
            { get { return _nGuarantorID; } set { _nGuarantorID = value; } }

            //Changes Mage by Subashish date 13-01-2011---------------------Close------------


            #endregion " Property Procedures "
        }

        public class PaymentInsuranceLines
        {

            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public PaymentInsuranceLines()
            {
                _innerlist = new ArrayList();
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }


            ~PaymentInsuranceLines()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(PaymentInsuranceLine item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(PaymentInsuranceLine item)
            //Remark - Work Remining for comparision
            {
                bool result = false;


                return result;
            }

            public bool RemoveAt(int index)
            {
                bool result = false;
                _innerlist.RemoveAt(index);
                result = true;
                return result;
            }

            public void Clear()
            {
                _innerlist.Clear();
            }

            public PaymentInsuranceLine this[int index]
            {
                get
                { return (PaymentInsuranceLine)_innerlist[index]; }
            }

            public bool Contains(PaymentInsuranceLine item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(PaymentInsuranceLine item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(PaymentInsuranceLine[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        public class PaymentInsuranceLineNextAction
        {
            #region "Constructor & Distructor"

            public PaymentInsuranceLineNextAction()
            {
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~PaymentInsuranceLineNextAction()
            {
                Dispose(false);
            }

            #endregion

            #region "Variables Declarations"

            private Int64 _nID = 0;

            private Int64 _nEOBPaymentID = 0;
            private Int64 _nEOBID = 0;
            private Int64 _nEOBPaymentDetailID = 0;

            private Int64 _nClaimNo = 0;
            private Int64 _nBillingTransactionID = 0;
            private Int64 _nBillingTransactionDetailID = 0;

            private string _sSubClaimNo = "";
            private Int64 _nTrackBillingTransactionID = 0;
            private Int64 _nTrackBillingTransactionDetailID = 0;

            private Int64 _nNextActionPatientInsID = 0;
            private string _nNextActionPatientInsName = "";
            private int _nNextActionPartyNumber = 0;
            private Int64 _nNextActionContactID = 0;

            private string _sNextActionCode = "";
            private string _sNextActionDescription = "";
            private decimal _dNextActionAmount = 0;
            private bool _IsNullNextActionAmount = true;
            private Int64 _nClinicID = 0;
            private bool _HasData = false;
            private PayerTypeV2 _nextPartyType = PayerTypeV2.None;

            private bool _HasNextData = false;
            private bool _HasActionData = false;

            private Int64 _nCloseDate = 0;
            private Int64 _nUserID = 0;
            private string _sUserName = "";
            private DateTime _dtDate;

            #endregion"Variables Declarations"

            #region " Property Procedures "

            public Int64 ID
            { get { return _nID; } set { _nID = value; } }
            public Int64 ClaimNo
            { get { return _nClaimNo; } set { _nClaimNo = value; } }
            public Int64 EOBPaymentID
            { get { return _nEOBPaymentID; } set { _nEOBPaymentID = value; } }
            public Int64 EOBID
            { get { return _nEOBID; } set { _nEOBID = value; } }
            public Int64 EOBPaymentDetailID
            { get { return _nEOBPaymentDetailID; } set { _nEOBPaymentDetailID = value; } }
            public Int64 BillingTransactionID
            { get { return _nBillingTransactionID; } set { _nBillingTransactionID = value; } }
            public Int64 BillingTransactionDetailID
            { get { return _nBillingTransactionDetailID; } set { _nBillingTransactionDetailID = value; } }
            public Int64 NextActionPatientInsID
            { get { return _nNextActionPatientInsID; } set { _nNextActionPatientInsID = value; } }
            public string NextActionPatientInsName
            { get { return _nNextActionPatientInsName; } set { _nNextActionPatientInsName = value; } }
            public int NextActionPartyNumber
            { get { return _nNextActionPartyNumber; } set { _nNextActionPartyNumber = value; } }
            public Int64 NextActionContactID
            { get { return _nNextActionContactID; } set { _nNextActionContactID = value; } }
            public string NextActionCode
            { get { return _sNextActionCode; } set { _sNextActionCode = value; } }
            public string NextActionDescription
            { get { return _sNextActionDescription; } set { _sNextActionDescription = value; } }
            public decimal NextActionAmount
            { get { return _dNextActionAmount; } set { _dNextActionAmount = value; } }
            public bool IsNullNextActionAmount
            { get { return _IsNullNextActionAmount; } set { _IsNullNextActionAmount = value; } }
            public Int64 ClinicID
            { get { return _nClinicID; } set { _nClinicID = value; } }
            public bool HasData
            { get { return _HasData; } set { _HasData = value; } }
            public PayerTypeV2 NextPartyType
            { get { return _nextPartyType; } set { _nextPartyType = value; } }

            public bool HasNextData
            { get { return _HasNextData; } set { _HasNextData = value; } }
            public bool HasActionData
            { get { return _HasActionData; } set { _HasActionData = value; } }

            public Int64 CloseDate
            { get { return _nCloseDate; } set { _nCloseDate = value; } }
            public Int64 UserID
            { get { return _nUserID; } set { _nUserID = value; } }
            public string UserName
            { get { return _sUserName; } set { _sUserName = value; } }
            public DateTime LastUpdated
            { get { return _dtDate; } set { _dtDate = value; } }

            public string SubClaimNo
            { get { return _sSubClaimNo; } set { _sSubClaimNo = value; } }
            public Int64 TrackBillingTransactionID
            { get { return _nTrackBillingTransactionID; } set { _nTrackBillingTransactionID = value; } }
            public Int64 TrackBillingTransactionDetailID
            { get { return _nTrackBillingTransactionDetailID; } set { _nTrackBillingTransactionDetailID = value; } }

            Int64 _nHSTID = 0;
            public Int64 nHSTID
            { get { return _nHSTID; } set { _nHSTID = value; } }

            #endregion

        }

        public class PaymentInsuranceLineNextActions
        {

            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public PaymentInsuranceLineNextActions()
            {
                _innerlist = new ArrayList();
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }


            ~PaymentInsuranceLineNextActions()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(PaymentInsuranceLineNextAction item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(PaymentInsuranceLineNextAction item)
            {
                bool result = false;
                return result;
            }

            public bool RemoveAt(int index)
            {
                bool result = false;
                _innerlist.RemoveAt(index);
                result = true;
                return result;
            }

            public void Clear()
            {
                _innerlist.Clear();
            }

            public PaymentInsuranceLineNextAction this[int index]
            {
                get
                { return (PaymentInsuranceLineNextAction)_innerlist[index]; }
            }

            public bool Contains(PaymentInsuranceLineNextAction item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(PaymentInsuranceLineNextAction item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(PaymentInsuranceLineNextAction[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        public class gloEOBPaymentInsurance
        {

            #region "Private Variables"

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private string _databaseConnectionString = "";
            private Int64 _clinicId = 0;
            private Int64 _userId = 0;
            private string _userName = "";
            private string _messageBoxCaption = "";

            #endregion
            #region " Property Procedures "

            public string DatabaseConnectionString
            {
                get { return _databaseConnectionString; }
                set { _databaseConnectionString = value; }
            }
            public Int64 ClinicID
            {
                get { return _clinicId; }
                set { _clinicId = value; }
            }
            public Int64 UserID
            {
                get { return _userId; }
                set { _userId = value; }
            }
            public string UserName
            {
                get { return _userName; }
                set { _userName = value; }
            }

            #endregion " Property Procedures "
            #region "Constructor & Distructor"

            public gloEOBPaymentInsurance(string Databaseconnectionstring)
            {
                _databaseConnectionString = Databaseconnectionstring;

                #region " Retrive ClinicID from appSetting "

                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _clinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _clinicId = 0; }
                }
                else
                { _clinicId = 0; }

                #endregion " Retrive ClinicID from appSetting "

                #region " Retrive UserID from appSettings "

                if (appSettings["UserID"] != null)
                {
                    if (appSettings["UserID"] != "")
                    {
                        _userId = Convert.ToInt64(appSettings["UserID"]);
                    }
                }
                else
                {
                    _userId = 0;
                }

                #endregion

                #region " Retrive UserName from appSettings "

                if (appSettings["UserName"] != null)
                {
                    if (appSettings["UserName"] != "")
                    {
                        _userName = Convert.ToString(appSettings["UserName"]);
                    }
                }
                else
                {
                    _userName = "";
                }

                #endregion

                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _messageBoxCaption = "";
                    }
                }
                else
                { _messageBoxCaption = ""; }

                #endregion
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~gloEOBPaymentInsurance()
            {
                Dispose(false);
            }

            #endregion
            public void UpdateNextAction(gloAccountsV2.PaymentCollection.PaymentInsuranceLineNextActions oPaymentInsuranceLineNextActions)
            {
                System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(_databaseConnectionString);
                System.Data.SqlClient.SqlCommand _sqlCommand = null;
                System.Data.SqlClient.SqlTransaction _sqlTransaction = null;
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);    
                gloAccountsV2.PaymentCollection.PaymentInsuranceLineNextAction oLineNextAction = null;
                object _retVal = null;
                int _result = 0;

                try
                {
                    if (oPaymentInsuranceLineNextActions != null && oPaymentInsuranceLineNextActions.Count > 0)
                    {
                        _sqlConnection.Open();
                        _sqlTransaction = _sqlConnection.BeginTransaction();

                        for (int index = 0; index < oPaymentInsuranceLineNextActions.Count; index++)
                        {
                            oLineNextAction = new gloAccountsV2.PaymentCollection.PaymentInsuranceLineNextAction();
                            oLineNextAction = oPaymentInsuranceLineNextActions[index];

                            if (oLineNextAction != null && oLineNextAction.HasData == true)
                            {
                                oParameters.Clear();
                                oParameters.Add("@nID", oLineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                oParameters.Add("@nClaimNo", oLineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentID", oLineNextAction.EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBID", oLineNextAction.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentDetailID", oLineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nBillingTransactionID", oLineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                oParameters.Add("@nBillingTransactionDetailID", oLineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nNextActionPatientInsID", oLineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                                oParameters.Add("@nNextActionPatientInsName", oLineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                oParameters.Add("@nNextActionPartyNumber", oLineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                                oParameters.Add("@nNextPartyType", oLineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                                oParameters.Add("@nNextActionContactID", oLineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@sNextActionCode", oLineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                oParameters.Add("@sNextActionDescription", oLineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                if (oLineNextAction.IsNullNextActionAmount == false)
                                {
                                    oParameters.Add("@dNextActionAmount", oLineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                }
                                else
                                {
                                    oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                }

                                oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                //oParameters.Add("@nTrackTrnID", oLineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                oParameters.Add("@nTrackTrnID", oLineNextAction.TrackBillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),

                                oParameters.Add("@nTrackTrnDtlID", oLineNextAction.TrackBillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);// numeric(18,0),
                                oParameters.Add("@sSubClaimNo", oLineNextAction.SubClaimNo, ParameterDirection.Input, SqlDbType.VarChar);// varchar(50),
                                oParameters.Add("@nTrackTrnLineNo", 0, ParameterDirection.Input, SqlDbType.Int);// int  

                                // ----------------------------------------
                                // Parameters added - Pankaj bedse 29012010
                                oParameters.Add("@nCloseDate", oLineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nUserID", oLineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@sUserName", oLineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                                // ----------------------------------------

                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                //_sqlCommand.CommandText = "BL_UP_EOBNextAction";
                                _sqlCommand.CommandText = "BL_UP_EOBNextAction_Tracking";

                                _result = _sqlCommand.ExecuteNonQuery();

                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                else
                                { _retVal = 0; }

                                _sqlCommand.Dispose();
                            }


                            if (oLineNextAction != null) { oLineNextAction.Dispose(); }
                        }

                        _sqlTransaction.Commit();
                        _sqlConnection.Close();
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    _sqlTransaction.Rollback();
                    _sqlConnection.Close(); 
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    _sqlTransaction.Rollback();
                    _sqlConnection.Close();
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (oParameters != null) { oParameters.Dispose(); }
                    if (_retVal != null) { _retVal = null; }

                    if (_sqlConnection != null) { _sqlConnection.Dispose(); }

                    if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); }
                    if (_sqlTransaction != null) { _sqlTransaction.Dispose(); }
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                    }

                }
            }
            public void UpdateParty(gloAccountsV2.PaymentCollection.PaymentInsuranceLineNextActions oPaymentInsuranceLineNextActions)
            {
                System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(_databaseConnectionString);
                System.Data.SqlClient.SqlCommand _sqlCommand = null;
                System.Data.SqlClient.SqlTransaction _sqlTransaction = null;
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);    

                gloAccountsV2.PaymentCollection.PaymentInsuranceLineNextAction oLineNextAction = null;
                object _retVal = null;
                int _result = 0;

                try
                {
                    if (oPaymentInsuranceLineNextActions != null && oPaymentInsuranceLineNextActions.Count > 0)
                    {
                        _sqlConnection.Open();
                        _sqlTransaction = _sqlConnection.BeginTransaction();

                        for (int index = 0; index < oPaymentInsuranceLineNextActions.Count; index++)
                        {
                            oLineNextAction = new gloAccountsV2.PaymentCollection.PaymentInsuranceLineNextAction();
                            oLineNextAction = oPaymentInsuranceLineNextActions[index];

                            if (oLineNextAction != null && oLineNextAction.HasData == true)
                            {
                                oParameters.Clear();
                                oParameters.Add("@nID", oLineNextAction.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);//numeric(18, 0) OUTPUT,
                                oParameters.Add("@nClaimNo", oLineNextAction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentID", oLineNextAction.EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBID", oLineNextAction.EOBID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nEOBPaymentDetailID", oLineNextAction.EOBPaymentDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nBillingTransactionID", oLineNextAction.BillingTransactionID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),	
                                oParameters.Add("@nBillingTransactionDetailID", oLineNextAction.BillingTransactionDetailID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nNextActionPatientInsID", oLineNextAction.NextActionPatientInsID, ParameterDirection.Input, SqlDbType.BigInt);//numeric(18, 0),
                                oParameters.Add("@nNextActionPatientInsName", oLineNextAction.NextActionPatientInsName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                oParameters.Add("@nNextActionPartyNumber", oLineNextAction.NextActionPartyNumber, ParameterDirection.Input, SqlDbType.Int);//	int,
                                oParameters.Add("@nNextPartyType", oLineNextAction.NextPartyType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                                oParameters.Add("@nNextActionContactID", oLineNextAction.NextActionContactID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@sNextActionCode", oLineNextAction.NextActionCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(5),
                                oParameters.Add("@sNextActionDescription", oLineNextAction.NextActionDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                                if (oLineNextAction.IsNullNextActionAmount == false)
                                {
                                    oParameters.Add("@dNextActionAmount", oLineNextAction.NextActionAmount, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                }
                                else
                                {
                                    oParameters.Add("@dNextActionAmount", DBNull.Value, ParameterDirection.Input, SqlDbType.Decimal);//	decimal(18, 2),
                                }

                                oParameters.Add("@nClinicID", _clinicId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@MachineID", GetPrefixTransactionID(), ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0) 

                                // ----------------------------------------
                                // Parameters added - Pankaj bedse 29012010
                                oParameters.Add("@nCloseDate", oLineNextAction.CloseDate, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@nUserID", oLineNextAction.UserID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                                oParameters.Add("@sUserName", oLineNextAction.UserName, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(100),
                                // ----------------------------------------

                                _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "BL_UP_EOBParty";

                                _result = _sqlCommand.ExecuteNonQuery();

                                if (_sqlCommand.Parameters["@nID"].Value != null)
                                { _retVal = _sqlCommand.Parameters["@nID"].Value; }
                                else
                                { _retVal = 0; }

                                _sqlCommand.Dispose();
                            }


                            if (oLineNextAction != null) { oLineNextAction.Dispose(); }
                        }

                        _sqlTransaction.Commit();
                        _sqlConnection.Close();
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    _sqlTransaction.Rollback();
                    _sqlConnection.Close(); ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    _sqlTransaction.Rollback();
                    _sqlConnection.Close(); 
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (oParameters != null) { oParameters.Dispose(); }
                    if (_retVal != null) { _retVal = null; }

                    if (_sqlConnection != null) { _sqlConnection.Dispose(); }
                    if (_sqlCommand != null) { if (_sqlCommand.Parameters != null) { _sqlCommand.Parameters.Clear(); } _sqlCommand.Dispose(); }
                    if (_sqlTransaction != null) { _sqlTransaction.Dispose(); }
                    
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                    }

                }
            }
            public string GetPaymentActionStatus()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _payActionStatus = "";
                DataTable _dtPayActStatus = null;
                string _sqlQuery = "";
                string _conCodeDesc = "";

                try
                {
                    _sqlQuery = " SELECT ISNULL(nID,0) AS ID,ISNULL(sCode,'') AS Code, " +
                    " ISNULL(sDescription,'') AS Description, ISNULL(nIsSystem,'false') AS IsSystem, " +
                    " ISNULL(nIsBlock,'false') AS nIsBlock, ISNULL(nActionID,0) AS nActionID " +
                    " FROM BL_EOBPayment_ActionStatus " +
                    " WHERE nID > 0 AND sCode IS NOT NULL AND sDescription IS NOT NULL AND nClinicID = " + _clinicId + " " +
                    " ORDER BY nID";

                    oDB.Connect(false);
                    oDB.Retrive_Query(_sqlQuery, out _dtPayActStatus);
                    oDB.Disconnect();

                    if (_dtPayActStatus != null && _dtPayActStatus.Rows.Count > 0)
                    {
                        _payActionStatus = "|";

                        for (int i = 0; i < _dtPayActStatus.Rows.Count; i++)
                        {
                            _conCodeDesc = "";

                            if (Convert.ToString(_dtPayActStatus.Rows[i]["Code"]).Trim() != "" && Convert.ToString(_dtPayActStatus.Rows[i]["Description"]).Trim() != "")
                            {
                                _conCodeDesc = Convert.ToString(_dtPayActStatus.Rows[i]["Code"]).Trim().ToUpper() + "-" + Convert.ToString(_dtPayActStatus.Rows[i]["Description"]).Trim().ToUpper() + "|";
                                _payActionStatus += _conCodeDesc;
                            }
                        }

                        _payActionStatus = _payActionStatus.TrimEnd('|');
                    }

                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                   

                }
                finally
                {
                    if (oDB != null) { oDB.Disconnect(); }
                    if (_dtPayActStatus != null) { _dtPayActStatus.Dispose(); }
                    if (_sqlQuery != null) { _sqlQuery = null; }
                }

                return _payActionStatus;
            }
            public string GetPaymentActionStatus(string Code)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _payActionStatus = "";
                DataTable _dtPayActStatus = null;
                string _sqlQuery = "";
                string _conCodeDesc = "";

                try
                {
                    _sqlQuery = " SELECT ISNULL(nID,0) AS ID,ISNULL(sCode,'') AS Code, " +
                    " ISNULL(sDescription,'') AS Description, ISNULL(nIsSystem,'false') AS IsSystem, " +
                    " ISNULL(nIsBlock,'false') AS nIsBlock, ISNULL(nActionID,0) AS nActionID " +
                    " FROM BL_EOBPayment_ActionStatus " +
                    " WHERE nID > 0 AND sCode IS NOT NULL AND sDescription IS NOT NULL AND sCode = '" + Code + "'  AND nClinicID = " + _clinicId + " ";

                    oDB.Connect(false);
                    oDB.Retrive_Query(_sqlQuery, out _dtPayActStatus);
                    oDB.Disconnect();

                    if (_dtPayActStatus != null && _dtPayActStatus.Rows.Count > 0)
                    {

                        for (int i = 0; i < _dtPayActStatus.Rows.Count; i++)
                        {
                            _conCodeDesc = "";

                            if (Convert.ToString(_dtPayActStatus.Rows[i]["Code"]).Trim() != "" && Convert.ToString(_dtPayActStatus.Rows[i]["Description"]).Trim() != "")
                            {
                                _conCodeDesc = Convert.ToString(_dtPayActStatus.Rows[i]["Code"]).Trim().ToUpper() + "-" + Convert.ToString(_dtPayActStatus.Rows[i]["Description"]).Trim().ToUpper() + "|";
                                _payActionStatus += _conCodeDesc;
                            }
                        }

                        _payActionStatus = _payActionStatus.TrimEnd('|');
                    }

                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                { gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false); }
                finally
                {
                    if (oDB != null) { oDB.Disconnect(); }
                    if (_dtPayActStatus != null) { _dtPayActStatus.Dispose(); }
                    if (_sqlQuery != null) { _sqlQuery = null; }
                }

                return _payActionStatus;
            }
           
            public string GetPaymentPrefixNumber(string Prefix)
            {
                string sPaymetNo = "";
                DataTable _dtUniquePaymentPrfixNumber = null;
                try
                {
                  
                    _dtUniquePaymentPrfixNumber = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                    if (_dtUniquePaymentPrfixNumber != null && _dtUniquePaymentPrfixNumber.Rows.Count > 0)
                    {
                        sPaymetNo  = Convert.ToString(_dtUniquePaymentPrfixNumber.Rows[0]["ID"].ToString());
                    }

                    
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (_dtUniquePaymentPrfixNumber != null) { _dtUniquePaymentPrfixNumber.Dispose(); }
                   
                }
                return sPaymetNo;
            }
            public Int64 GetContactCompanyId(Int64 ContactId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _sqlQuery = "";
                object _retVal = null;
                Int64 _companyId = 0;

                try
                {
                    _sqlQuery = "SELECT ISNULL(nCompanyId,0) AS nCompanyId FROM Contact_InsurancePlan_Association " +
                    " WHERE  nContactId = " + ContactId + " AND nClinicId = " + _clinicId + " ";

                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    {
                        _companyId = Convert.ToInt64(_retVal);
                    }

                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);;
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                    if (_retVal != null) { _retVal = null; }
                }

                return _companyId;
            }
            public string GetContactCompanyName(Int64 ContactId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _sqlQuery = "";
                object _retVal = null;
                string _companyName = "";

                try
                {
                    //_sqlQuery = " SELECT ISNULL(sDescription,0) AS sCompanyName FROM Contact_InsurancePlan_Association " +
                    //" WHERE  nContactId = " + ContactId + " AND nClinicId = " + _clinicId + " ";

                    _sqlQuery = " SELECT TOP 1 " +
                    " ISNULL(Contacts_InsuranceCompany_MST.sDescription, 0) AS sCompanyName  " +
                    " FROM          " +
                    " Contact_InsurancePlan_Association INNER JOIN " +
                    " Contacts_InsuranceCompany_MST ON Contact_InsurancePlan_Association.nCompanyId = Contacts_InsuranceCompany_MST.nID " +
                    " WHERE      " +
                    " (Contact_InsurancePlan_Association.nContactId = " + ContactId + ")  " +
                    " AND (Contact_InsurancePlan_Association.nClinicId = " + _clinicId + ") ";


                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    {
                        _companyName = Convert.ToString(_retVal);
                    }

                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);;
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                    if (_retVal != null) { _retVal = null; }
                }

                return _companyName;
            }
            public bool IsContactCompanyAssociated(Int64 ContactId, Int64 CompanyId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _sqlQuery = "";
                object _retVal = null;
                bool _IsExists = false;

                try
                {
                    _sqlQuery = " SELECT ISNULL(COUNT(*),0) FROM Contact_InsurancePlan_Association  " +
                    " WHERE  nContactId = " + ContactId + " AND nCompanyId = " + CompanyId + " AND nClinicId = " + _clinicId + " ";

                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    {
                        _IsExists = Convert.ToBoolean(_retVal);
                    }

                }
                catch (gloDatabaseLayer.DBException dbEx)
                { dbEx.ERROR_Log(dbEx.ToString()); }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);;
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                    if (_retVal != null) { _retVal = null; }
                }

                return _IsExists;
            }
            
           
            private Int64 GetPrefixTransactionID()
            {
                Int64 _Result = 0;
                string _result = "";
                DateTime _PatientDOB = DateTime.Now;
                DateTime _CurrentDate = DateTime.Now;
                DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

                string strID1 = "";
                string strID2 = "";
                string strID3 = "";

                TimeSpan oTS;

                try
                {
                    _result = "";

                    oTS = new TimeSpan();
                    oTS = _CurrentDate.Subtract(_BaseDate);
                    strID1 = oTS.Days.ToString().Replace("-", "");

                    oTS = new TimeSpan();
                    oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                    strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                    oTS = new TimeSpan();
                    oTS = _PatientDOB.Subtract(_BaseDate);
                    strID3 = oTS.Days.ToString().Replace("-", "");

                    _result = strID1 + strID2 + strID3;

                    _Result = Convert.ToInt64(_result);

                }
                catch //(Exception ex)
                {
                    //returns random number if exception occures
                    Random oRan = new Random();
                    return oRan.Next(1, Int32.MaxValue);
                }
                finally
                {

                }
                return _Result;
            }
 
        }

        public class InsurancePaymentRefund
        {
            #region "Constructor & Destructor"

            public InsurancePaymentRefund()
            {
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~InsurancePaymentRefund()
            {
                Dispose(false);
            }

            #endregion

            #region "Variables Declarations"

            private Int64 _nRefundID = 0;
            private string _sRefundTo = "";
            private string _sRefundNotes = "";
            private Int64 _dtRefunddate = 0;
            private decimal _dRefundAmount = 0;
            private bool _bIsVoid = false;
            private Int64 _nVoidCloseDate = 0;
            private Int64 _nVoidTrayID = 0;
            private string _nVoidTrayDescription = "";


            #endregion"Variables Declarations"

            #region " Property Procedures "

            public Int64 RefundID
            { get { return _nRefundID; } set { _nRefundID = value; } }
            public string RefundTo
            { get { return _sRefundTo; } set { _sRefundTo = value; } }
            public string RefundNotes
            { get { return _sRefundNotes; } set { _sRefundNotes = value; } }
            public Int64 Refunddate
            { get { return _dtRefunddate; } set { _dtRefunddate = value; } }
            public decimal RefundAmount
            { get { return _dRefundAmount; } set { _dRefundAmount = value; } }
            public bool IsVoid
            { get { return _bIsVoid; } set { _bIsVoid = value; } }
            public Int64 VoidCloseDate
            { get { return _nVoidCloseDate; } set { _nVoidCloseDate = value; } }
            public Int64 VoidTrayID
            { get { return _nVoidTrayID; } set { _nVoidTrayID = value; } }
            public string VoidTrayDescription
            { get { return _nVoidTrayDescription; } set { _nVoidTrayDescription = value; } }

            public Int64 TransactionID { get; set; }
            public Int64 MasterTransactionID { get; set; }
            public Int64 PatientID { get; set; }
            public string ClaimNo { get; set; }


            #endregion

        }

        public class InsurancePaymentRefundV2
        {
            #region "Constructor & Destructor"

            public InsurancePaymentRefundV2()
            {
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~InsurancePaymentRefundV2()
            {
                Dispose(false);
            }

            #endregion


            #region " Property Procedures "

            public Int64 RefundID { get; set; }
            public string RefundTo { get; set; }
            public string RefundNotes { get; set; }
            public DateTime Refunddate { get; set; }
            public decimal RefundAmount { get; set; }
            public Int64 nCreditID { get; set; }
            public Int64 nPayerID { get; set; }
            public string sVoidNote { get; set; }


            #endregion

        }

        public class PaymentPatientClaim
        {
            #region "Private Variables"

            private Int64 _ClaimNo = 0;
            private string _DisplayClaimNo = "";
            private string _ClaimNoPrefix = "";
            private Int64 _BillingTransactionID = 0;
            private Int64 _BillingTransactionDate = 0;
            private Int64 _PatientID = 0;
            private string _PatientName = "";
            private PaymentInsuranceLines _CliamLines = null;
            private Int64 _ClinicId = 0;
            //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for declaring Property Variable 
            private string _sRespParty = "";
            private Int16 _FacilityType = 0;
            //End
            #endregion

            #region "Constructor & Distructor"


            public PaymentPatientClaim()
            {
                _CliamLines = new PaymentInsuranceLines();
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        _CliamLines.Dispose();
                    }
                }
                disposed = true;
            }

            ~PaymentPatientClaim()
            {
                Dispose(false);
            }

            #endregion

            #region " Property Procedures "

            public Int64 ClaimNo
            {
                get { return _ClaimNo; }
                set { _ClaimNo = value; }
            }
            public string DisplayClaimNo
            {
                get { return _DisplayClaimNo; }
                set { _DisplayClaimNo = value; }
            }
            public string ClaimNoPrefix
            {
                get { return _ClaimNoPrefix; }
                set { _ClaimNoPrefix = value; }
            }
            public Int64 BillingTransactionID
            {
                get { return _BillingTransactionID; }
                set { _BillingTransactionID = value; }
            }
            public Int64 BillingTransactionDate
            {
                get { return _BillingTransactionDate; }
                set { _BillingTransactionDate = value; }
            }
            public Int64 PatientID
            {
                get { return _PatientID; }
                set { _PatientID = value; }
            }
            public string PatientName
            {
                get { return _PatientName; }
                set { _PatientName = value; }
            }
            public PaymentInsuranceLines CliamLines
            {
                get { return _CliamLines; }
                set { _CliamLines = value; }
            }

            public Int64 ClinicID
            {
                get { return _ClinicId; }
                set { _ClinicId = value; }
            }
            //Added by Subashish_b on 25/Jan /2011 (integration made on date-10/May/2011) for  creating Property
            public string RespParty
            {
                get { return _sRespParty; }
                set { _sRespParty = value; }
            }
            //End
            public Int16 FacilityType
            {
                get { return _FacilityType; }
                set { _FacilityType = value; }
            }

            #endregion " Property Procedures "
        }

        public class PaymentPatientClaims
        {

            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public PaymentPatientClaims()
            {
                _innerlist = new ArrayList();
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }


            ~PaymentPatientClaims()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(PaymentPatientClaim item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(PaymentPatientClaim item)
            //Remark - Work Remining for comparision
            {
                bool result = false;


                return result;
            }

            public bool RemoveAt(int index)
            {
                bool result = false;
                _innerlist.RemoveAt(index);
                result = true;
                return result;
            }

            public void Clear()
            {
                _innerlist.Clear();
            }

            public PaymentPatientClaim this[int index]
            {
                get
                { return (PaymentPatientClaim)_innerlist[index]; }
            }

            public bool Contains(PaymentPatientClaim item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(PaymentPatientClaim item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(PaymentPatientClaim[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        public class PatientPaymentRefund
        {
            #region "Constructor & Destructor"

            public PatientPaymentRefund()
            {
            }

            private bool disposed = false;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }

            ~PatientPaymentRefund()
            {
                Dispose(false);
            }

            #endregion

            #region "Variables Declarations"

            private Int64 _nRefundID = 0;
            private string _sRefundTo = "";
            private string _sRefundNotes = "";
            private DateTime _dtRefunddate = DateTime.Now;
            private decimal _dRefundAmount = 0;
            private bool _bIsVoid = false;
            private Int64 _nVoidCloseDate = 0;
            private Int64 _nVoidTrayID = 0;
            private string _nVoidTrayDescription = "";




            #endregion"Variables Declarations"

            #region " Property Procedures "

            public Int64 RefundID
            { get { return _nRefundID; } set { _nRefundID = value; } }
            public string RefundTo
            { get { return _sRefundTo; } set { _sRefundTo = value; } }
            public string RefundNotes
            { get { return _sRefundNotes; } set { _sRefundNotes = value; } }
            public DateTime Refunddate
            { get { return _dtRefunddate; } set { _dtRefunddate = value; } }
            public decimal RefundAmount
            { get { return _dRefundAmount; } set { _dRefundAmount = value; } }
            public bool IsVoid
            { get { return _bIsVoid; } set { _bIsVoid = value; } }
            public Int64 VoidCloseDate
            { get { return _nVoidCloseDate; } set { _nVoidCloseDate = value; } }
            public Int64 VoidTrayID
            { get { return _nVoidTrayID; } set { _nVoidTrayID = value; } }
            public string VoidTrayDescription
            { get { return _nVoidTrayDescription; } set { _nVoidTrayDescription = value; } }

            #endregion

        }
    }
}
