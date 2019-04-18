using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace gloBilling
{
    namespace Common
    {
        public class PaymentTransaction
        {
            #region "Constructor & Destructor"

            public PaymentTransaction()
            {
                _TransactionPaymentClaims = new PaymentClaims();
                _PaymentModeDetail = new PaymentModeDetail();
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
                        if (_TransactionPaymentClaims != null) { _TransactionPaymentClaims.Dispose(); }
                        if (_PaymentModeDetail != null) { _PaymentModeDetail.Dispose(); }
                    }
                }
                disposed = true;
            }

            ~PaymentTransaction()
            {
                Dispose(false);
            }

            #endregion

            #region " Variable Declarations "

                private Int64 _nMultiplePaymentTransactionID = 0;//done
                private Int64 _nPaymentDate = 0;//done
                private Int64 _nPaymentTime = 0;//done
                //private TransactionType _TransactionTypeValue = TransactionType.None;
                //private Int64 _TransactionTypeDetailValue = 0;
                //private String _TransactionTypeDetailName = "";
               
                //Old one - it is in used but mostly we use following object to set this value
                private PaymentMode _PaymentModeValue = PaymentMode.None;
                private decimal _dPaymentAmount = 0;
                //this object is used on behalf of above properties
                private PaymentModeDetail _PaymentModeDetail = null;

                private PaymentClaims _TransactionPaymentClaims = null;    
                private Int64 _nClinicID = 0;
                private Int64 _userid = 0;
                private string _userName = "";

                private Int64 _insuranceId = 0;
                private string _insuranceName = "";
                private decimal _pendingPayment = 0;
                private decimal _patientPayment = 0;

                private Int64 _PaymentTransactionNo = 0;
                private Int64 _CloseDayTrayID = 0;
                private string _CloseDayTrayCode = "";
                private string _CloseDayTrayName = "";
                private TransactionType _masterTranType = TransactionType.None;
                private Int64 _nRefundId = 0;
                private string _sRefundCode = "";
                private string _sRefundDesc = "";

            private Int64 _remitId = 0;



            #endregion

            #region " Property Procedures "

                public Int64 MultiplePaymentTransactionID
                {
                    get { return _nMultiplePaymentTransactionID; }
                    set { _nMultiplePaymentTransactionID = value; }
                }

                public Int64 PaymentDate
                {
                    get { return _nPaymentDate; }
                    set { _nPaymentDate = value; }
                }

                public Int64 PaymentTime
                {
                    get { return _nPaymentTime; }
                    set { _nPaymentTime = value; }
                }

                public PaymentMode PaymentModeValue
                {
                    get { return _PaymentModeValue; }
                    set { _PaymentModeValue = value; }
                }

                public decimal PayerAmount
                {
                    get { return _dPaymentAmount; }
                    set { _dPaymentAmount = value; }
                }

                public PaymentClaims TransactionPaymentClaims
                {
                    get { return _TransactionPaymentClaims; }
                    set { _TransactionPaymentClaims = value; }
                }

                public Int64 ClinicID
                {
                    get { return _nClinicID; }
                    set { _nClinicID = value; }
                }

                public Int64 UserID
                {
                    get { return _userid; }
                    set { _userid = value; }
                }
                public string UserName
                {
                    get { return _userName; }
                    set { _userName = value; }
                }

                public Int64 InsuranceID
                {
                    get { return _insuranceId; }
                    set { _insuranceId = value; }
                }
                public string InsuranceName
                {
                    get { return _insuranceName; }
                    set { _insuranceName = value; }
                }
                public PaymentModeDetail PaymentModeDetail
                {
                    get { return _PaymentModeDetail; }
                    set { _PaymentModeDetail = value; }
                }

                public decimal PendingPayment
                {
                    get { return _pendingPayment; }
                    set { _pendingPayment = value; }
                }

                public decimal PatientPayment
                {
                    get { return _patientPayment; }
                    set { _patientPayment = value; }
                }

            //for retrive master data added on 20090811
                public Int64 PaymentNo
                {
                    get { return _PaymentTransactionNo; }
                    set { _PaymentTransactionNo = value; }
                }
                public Int64 CloseDayTrayID
                {
                    get { return _CloseDayTrayID; }
                    set { _CloseDayTrayID = value; }
                }
                public string CloseDayTrayCode
                {
                    get { return _CloseDayTrayCode; }
                    set { _CloseDayTrayCode = value; }
                }
                public string CloseDayTrayName
                {
                    get { return _CloseDayTrayName; }
                    set { _CloseDayTrayName = value; }
                }

                public TransactionType MasterTransactoinType
                {
                    get { return _masterTranType; }
                    set { _masterTranType = value; }
                }

                public Int64 RefundID
                {
                    get { return _nRefundId; }
                    set { _nRefundId = value; }
                }
                public string RefundCode
                {
                    get { return _sRefundCode; }
                    set { _sRefundCode = value; }
                }
                public string RefundDesc
                {
                    get { return _sRefundDesc; }
                    set { _sRefundDesc = value; }
                }

                public Int64 RemitID
                {
                    get { return _remitId; }
                    set { _remitId = value; }
                }


          #endregion

        }

        public class PaymentClaim
        {
            #region " Constructor & Destructor "

            private bool disposed = false;

            public PaymentClaim()
            {
                _PaymentClaimLines = new PaymentClaimLines();
                _Cliam_Lines = new ClaimLines();
            }

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
                        _PaymentClaimLines.Dispose();
                    }
                }
                disposed = true;
            }
            ~PaymentClaim()
            {
                Dispose(false);
            }

            #endregion " Constructor & Distructor "

            #region  " Variable Declarations "
            //Its billing transaction id
            public Int64 TransactionID = 0;
            public Int64 TransactionDate = 0;
            public Int64 PaymentTransactionNo = 0;
            public string ClaimNoPrefix = "";
            public Int64 ClaimNo = 0;
            public string DisplayClaimNo = "";
            public decimal TotalPayerAmount = 0; //insurance total amount with respective that particuler claim
            public Int64 PatientID = 0;
            public Int64 PaymentDate = 0;
            public Int64 PaymentTime = 0;
            public Int64 PaymentTransactionID = 0;
            public Int64 MultiplePaymentTransactionID = 0; //retirve purpose

            public Int64 CloseDayTrayID = 0;
            public string CloseDayTrayCode = "";
            public string CloseDayTrayName = "";
            public TransactionStatus PaymentTransactionStatus = TransactionStatus.None;

            public Int64 UserID = 0;
            public string UserName = "";

            public Int64 PaymentInsuranceID = 0;
            public string PaymentInsuranceName = "";

            public Int64 ReceivedPaymentCounter = 0;

            private PaymentClaimLines _PaymentClaimLines = null;//..Old Class
            private ClaimLines _Cliam_Lines = null; //.  new class

            public TransactionType MasterTransactionType = TransactionType.None;
            public Int64 RefundID = 0;
            public string RefundCode = "";
            public string RefundDesc = "";
            //public Int64 RemitID = "";
            
            #endregion " Variable Declarations "

            #region " Property Procedures "
            //OLD
            public PaymentClaimLines ClaimLines
            {
                get { return _PaymentClaimLines; }
                set { _PaymentClaimLines = value; }
            }
            //CURRENT
            public ClaimLines Cliam_Lines
            {
                get { return _Cliam_Lines; }
                set { _Cliam_Lines = value; }
            }

            #endregion " Property Procedures "

        }

        public class PaymentClaims
        {

            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public PaymentClaims()
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


            ~PaymentClaims()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(PaymentClaim item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(PaymentClaim item)
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

            public PaymentClaim this[int index]
            {
                get
                { return (PaymentClaim)_innerlist[index]; }
            }

            public bool Contains(PaymentClaim item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(PaymentClaim item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(PaymentClaim[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        #region " Commented Code on 20090623 "
        //public class PaymentClaimLine
        //{
        //    #region " Constructor & Destructor "

        //    private bool disposed = false;

        //    public PaymentClaimLine()
        //    {
        //        _PaymentNotes = new PaymentsNotes();
        //        _ExtendedLine = new PaymentClaimExtendedLine();
        //        _TransactionOtherPayments = new TransactionOtherPayments();
        //    }

        //    public void Dispose()
        //    {
        //        Dispose(true);
        //        GC.SuppressFinalize(this);
        //    }
        //    protected virtual void Dispose(bool disposing)
        //    {
        //        if (!this.disposed)
        //        {
        //            if (disposing)
        //            {
        //                if (_PaymentNotes != null) { _PaymentNotes.Dispose(); }
        //                if (_ExtendedLine != null) { _ExtendedLine.Dispose(); }
        //            }
        //        }
        //        disposed = true;
        //    }
        //    ~PaymentClaimLine()
        //    {
        //        Dispose(false);
        //    }

        //    #endregion " Constructor & Distructor "

        //    #region Declaration
        //        private Int64 _PaymentTransactionID = 0;
        //        private Int64 _PaymentTransactionDetailID = 0;
        //        private Int64 _PaymentDate = 0;
        //        private Int64 _PatientID = 0;
        //        private Int64 _TransactionID = 0;
        //        private Int64 _TransactionDetailID = 0;
        //        private Int64 _TransactionLineNo = 0;
        //        private Int64 _ClaimNumber = 0;
        //        private Int64 _DOSFrom = 0;
        //        private Int64 _DOSTo = 0;
        //        private string _CPTCode = "";
        //        private string _CPTDescription = "";
        //        private decimal _TransactionCharges = 0;
        //        private decimal _TransactionUnit = 0;
        //        private decimal _TransactionTotalCharges = 0;
        //        private decimal _TransactionAllowed = 0;
        //        private decimal _TransactionWriteOff = 0;
        //        private decimal _TransactionPaid = 0;
        //        private decimal _TransactionPaidByPatient = 0;
        //        private decimal _TransactionPaidByInsurance = 0;
                
        //        private decimal _CurrentPayment = 0;
        //        private decimal _CurrentPayment_Insurance = 0;
        //        private decimal _CurrentPayment_Patient = 0;
        //        private decimal _CurrentPayment_Copay = 0;
        //        private decimal _CurrentPayment_Deductable = 0;
        //        private decimal _CurrentPayment_Adjustment = 0;
        //        private decimal _CurrentPayment_CoInsurance = 0;
        //        private decimal _CurrentPayment_Refund = 0;
        //        private decimal _CurrentPayment_Withhold = 0;

        //        private decimal _TransactionBalance = 0;
        //        private TransactionType _TransactionTypeValue = TransactionType.None;
        //        private Int64 _TransactionTypeDetailValue = 0;
        //        private String _TransactionTypeDetailName = "";
        //        private PaymentMode _PaymentModeValue = PaymentMode.None;
        //        private PayerMode _PayerModeValue = PayerMode.None;
        //        private Int64 _PayerMode_InsuranceID = 0;
        //        private string _PayerMode_InsuranceName = "";
        //        private string _sCardNoNChkNoNMoneyOrdNo = "";
        //        private Int64 _nCardExpiryNChkDateNMoneyOrdDate = 0;
        //        private string _sSecurityNo = "";
        //        private string _sCardType = "";
        //        private TransactionStatus _LinePaymentStatus = TransactionStatus.None;
        //        private PaymentsNotes _PaymentNotes = null;
        //        private Int64 _CopayID = 0;
        //        private PaymentClaimExtendedLine _ExtendedLine = null;
        //        private Int64 _ClinicID = 0;
        //        private Int64 _PrePayID = 0;
        //        private PaymentModeDetails _PaymentModeDetails = null;
        //        private TransactionOtherPayments _TransactionOtherPayments = null;
        //        private LinePaymentDetails _LinePaymentDetails = null;

        //    #endregion

        //    #region " Property Procedures "

        //        public Int64 PaymentTransactionID
        //        {
        //            get { return _PaymentTransactionID; }
        //            set { _PaymentTransactionID = value; }
        //        }

        //        public Int64 PaymentTransactionDetailID
        //        {
        //            get { return _PaymentTransactionDetailID; }
        //            set { _PaymentTransactionDetailID = value; }
        //        }

        //        public Int64 PaymentDate
        //        {
        //            get { return _PaymentDate; }
        //            set { _PaymentDate = value; }
        //        }

        //        public Int64 PatientID
        //        {
        //            get { return _PatientID; }
        //            set { _PatientID = value; }
        //        }

        //        public Int64 TransactionID
        //        {
        //            get { return _TransactionID; }
        //            set { _TransactionID = value; }
        //        }

        //        public Int64 TransactionDetailID
        //        {
        //            get { return _TransactionDetailID; }
        //            set { _TransactionDetailID = value; }
        //        }

        //        public Int64 TransactionLineNo
        //        {
        //            get { return _TransactionLineNo; }
        //            set { _TransactionLineNo = value; }
        //        }

        //        public Int64 ClaimNo
        //        {
        //            get { return _ClaimNumber; }
        //            set { _ClaimNumber = value; }
        //        }

        //        public Int64 DOSFrom
        //        {
        //            get { return _DOSFrom; }
        //            set { _DOSFrom = value; }
        //        }

        //        public Int64 DOSTo
        //        {
        //            get { return _DOSTo; }
        //            set { _DOSTo = value; }
        //        }

        //        public string CPTCode
        //        {
        //            get { return _CPTCode; }
        //            set { _CPTCode = value; }
        //        }

        //        public string CPTDescription
        //        {
        //            get { return _CPTDescription; }
        //            set { _CPTDescription = value; }
        //        }

        //        public decimal TransactionCharges
        //        {
        //            get { return _TransactionCharges; }
        //            set { _TransactionCharges = value; }
        //        }

        //        public decimal TransactionUnit
        //        {
        //            get { return _TransactionUnit; }
        //            set { _TransactionUnit = value; }
        //        }

        //        public decimal TransactionTotalCharges
        //        {
        //            get { return _TransactionTotalCharges; }
        //            set { _TransactionTotalCharges = value; }
        //        }

        //        public decimal TransactionAllowed
        //        {
        //            get { return _TransactionAllowed; }
        //            set { _TransactionAllowed = value; }
        //        }

        //        public decimal TransactionWriteOff
        //        {
        //            get { return _TransactionWriteOff; }
        //            set { _TransactionWriteOff = value; }
        //        }

        //        public decimal TransactionPaid
        //        {
        //            get { return _TransactionPaid; }
        //            set { _TransactionPaid = value; }
        //        }

        //        public decimal TransactionPaidByPatient
        //        {
        //            get { return _TransactionPaidByPatient; }
        //            set { _TransactionPaidByPatient = value; }
        //        }

        //        public decimal TransactionPaidByInsurance
        //        {
        //            get { return _TransactionPaidByInsurance; }
        //            set { _TransactionPaidByInsurance = value; }
        //        }

        //        public decimal CurrentPayment
        //        {
        //            get { return _CurrentPayment; }
        //            set { _CurrentPayment = value; }
        //        }

        //        public decimal CurrentPayment_Insurance
        //        {
        //            get { return _CurrentPayment_Insurance; }
        //            set { _CurrentPayment_Insurance = value; }
        //        }

        //        public decimal CurrentPayment_Patient
        //        {
        //            get { return _CurrentPayment_Patient; }
        //            set { _CurrentPayment_Patient = value; }
        //        }

        //        public decimal CurrentPayment_Copay
        //        {
        //            get { return _CurrentPayment_Copay; }
        //            set { _CurrentPayment_Copay = value; }
        //        }

        //        public decimal CurrentPayment_Deductable
        //        {
        //            get { return _CurrentPayment_Deductable; }
        //            set { _CurrentPayment_Deductable = value; }
        //        }

        //        public decimal CurrentPayment_Adjustment
        //        {
        //            get { return _CurrentPayment_Adjustment; }
        //            set { _CurrentPayment_Adjustment = value; }
        //        }

        //        public decimal CurrentPayment_CoInsurance
        //        {
        //            get { return _CurrentPayment_CoInsurance; }
        //            set { _CurrentPayment_CoInsurance = value; }
        //        }

        //        public decimal CurrentPayment_Refund
        //        {
        //            get { return _CurrentPayment_Refund; }
        //            set { _CurrentPayment_Refund = value; }
        //        }

        //        public decimal CurrentPayment_Withhold
        //        {
        //            get { return _CurrentPayment_Withhold; }
        //            set { _CurrentPayment_Withhold = value; }
        //        }

        //        public decimal TransactionBalance
        //        {
        //            get { return _TransactionBalance; }
        //            set { _TransactionBalance = value; }
        //        }

        //        public TransactionType TransactionTypeValue
        //        {
        //            get { return _TransactionTypeValue; }
        //            set { _TransactionTypeValue = value; }
        //        }

        //        public Int64 TransactionTypeDetailValue
        //        {
        //            get { return _TransactionTypeDetailValue; }
        //            set { _TransactionTypeDetailValue = value; }
        //        }

        //        public String TransactionTypeDetailName
        //        {
        //            get { return _TransactionTypeDetailName; }
        //            set { _TransactionTypeDetailName = value; }
        //        }
                
        //        public PaymentMode PaymentModeValue
        //        {
        //            get { return _PaymentModeValue; }
        //            set { _PaymentModeValue = value; }
        //        }

        //        public PayerMode PayerModeValue
        //        {
        //            get { return _PayerModeValue; }
        //            set { _PayerModeValue = value; }
        //        }

        //        public Int64 PayerModeInsuranceID
        //        {
        //            get { return _PayerMode_InsuranceID; }
        //            set { _PayerMode_InsuranceID = value; }
        //        }

        //        public string PayerModeInsuranceName
        //        {
        //            get { return _PayerMode_InsuranceName; }
        //            set { _PayerMode_InsuranceName = value; }
        //        }

        //        public string CardNoAndCheckNoAndMoneyOrderNo
        //        {
        //            get { return _sCardNoNChkNoNMoneyOrdNo; }
        //            set { _sCardNoNChkNoNMoneyOrdNo = value; }
        //        }
        //        public Int64 CardExpiryAndCheckDateAndMoneyOrderDate
        //        {
        //            get { return _nCardExpiryNChkDateNMoneyOrdDate; }
        //            set { _nCardExpiryNChkDateNMoneyOrdDate = value; }
        //        }

        //        public string SecurityNo
        //        {
        //            get { return _sSecurityNo; }
        //            set { _sSecurityNo = value; }
        //        }

        //        public string CardType
        //        {
        //            get { return _sCardType; }
        //            set { _sCardType = value; }
        //        }

        //        public TransactionStatus PaymentLineStatus
        //        {
        //            get { return _LinePaymentStatus; }
        //            set { _LinePaymentStatus = value; }
        //        }

        //        public PaymentsNotes Notes
        //        {
        //            get { return _PaymentNotes; }
        //            set { _PaymentNotes = value; }
        //        }

        //        public PaymentClaimExtendedLine ExtendedLine
        //        {
        //            get { return _ExtendedLine; }
        //            set { _ExtendedLine = value; }
        //        }

        //        public Int64 CopayID
        //        {
        //            get { return _CopayID; }
        //            set { _CopayID = value; }
        //        }

        //        public Int64 PrePayID
        //        {
        //            get { return _PrePayID; }
        //            set { _PrePayID = value; }
        //        }

        //        public Int64 ClinicID
        //        {
        //            get { return _ClinicID; }
        //            set { _ClinicID = value; }
        //        }

        //        public PaymentModeDetails PaymentMode_Details
        //        {
        //            get { return _PaymentModeDetails; }
        //            set { _PaymentModeDetails = value; }
        //        }

        //        public TransactionOtherPayments Transaction_OtherPayments
        //        {
        //            get { return _TransactionOtherPayments; }
        //            set { _TransactionOtherPayments = value; }
        //        }

        //        public LinePaymentDetails LinePayment_Details
        //        {
        //            get { return _LinePaymentDetails; }
        //            set { _LinePaymentDetails = value; }
        //        }

        //    #endregion
        //    }
        #endregion 

        //OLD
        public class PaymentClaimLine
        {
            #region " Constructor & Destructor "

            private bool disposed = false;

            public PaymentClaimLine()
            {
                _PaymentNotes = new PaymentsNotes();
                _ExtendedLine = new PaymentClaimExtendedLine();
                _TransactionOtherPayments = new TransactionOtherPayments();
                _ClaimLinePayments = new ClaimLinePayments();
            }

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
                        if (_PaymentNotes != null) { _PaymentNotes.Dispose(); }
                        if (_ExtendedLine != null) { _ExtendedLine.Dispose(); }
                        if (_ClaimLinePayments != null) { _ClaimLinePayments.Dispose(); }
                        if (bAssignedTrans)
                        {
                            if (_TransactionOtherPayments != null)
                            {
                                _TransactionOtherPayments.Clear();
                                _TransactionOtherPayments.Dispose();
                                _TransactionOtherPayments = null;
                                
                            }
                            bAssignedTrans = false;
                        }
                    }
                }
                disposed = true;
            }
            ~PaymentClaimLine()
            {
                Dispose(false);
            }

            #endregion " Constructor & Distructor "

            #region Declaration

            private Int64 _PaymentTransactionID = 0;
            private Int64 _PaymentTransactionDetailID = 0;
            private Int64 _PaymentDate = 0;
            private Int64 _PatientID = 0;
            private Int64 _TransactionID = 0;
            private Int64 _TransactionDetailID = 0;
            private Int64 _TransactionLineNo = 0;
            private Int64 _ClaimNumber = 0;
            private Int64 _DOSFrom = 0;
            private Int64 _DOSTo = 0;
            private string _CPTCode = "";
            private string _CPTDescription = "";
            private decimal _TransactionCharges = 0;
            private decimal _TransactionUnit = 0;
            private decimal _TransactionTotalCharges = 0;
            private decimal _TransactionAllowed = 0;
            private decimal _TransactionWriteOff = 0;
            private decimal _TransactionPaid = 0;
            private decimal _TransactionPaidByPatient = 0;
            private decimal _TransactionPaidByInsurance = 0;

            private decimal _CurrentPayment = 0; //remove
            private decimal _CurrentPayment_Insurance = 0; //remove
            private decimal _CurrentPayment_Patient = 0; //remove
            private decimal _CurrentPayment_Copay = 0; //remove
            private decimal _CurrentPayment_Deductable = 0; //remove
            private decimal _CurrentPayment_Adjustment = 0; //remove
            private decimal _CurrentPayment_CoInsurance = 0; //remove
            private decimal _CurrentPayment_Refund = 0; //remove
            private decimal _CurrentPayment_Withhold = 0; //remove
            private decimal _TransactionBalance = 0; //remove

            private TransactionType _TransactionTypeValue = TransactionType.None;
            private Int64 _TransactionTypeDetailValue = 0;
            private String _TransactionTypeDetailName = "";
            private PaymentMode _PaymentModeValue = PaymentMode.None;
            private PayerMode _PayerModeValue = PayerMode.None;
            private Int64 _PayerMode_InsuranceID = 0;//Insurance ID from Transaction
            private string _PayerMode_InsuranceName = "";//Insurance ID from Transaction
            private string _sCardNoNChkNoNMoneyOrdNo = "";
            private Int64 _nCardExpiryNChkDateNMoneyOrdDate = 0;
            private string _sSecurityNo = "";
            private string _sCardType = "";
            private TransactionStatus _LinePaymentStatus = TransactionStatus.None;
            private PaymentsNotes _PaymentNotes = null; //remove
            private Int64 _CopayID = 0;
            private PaymentClaimExtendedLine _ExtendedLine = null;
            private Int64 _ClinicID = 0;
            private Int64 _PrePayID = 0;
            private PaymentModeDetails _PaymentModeDetails = null; //remove
            private TransactionOtherPayments _TransactionOtherPayments = null; //remove
            private LinePaymentDetails _LinePaymentDetails = null; //remove

            private ClaimLinePayments _ClaimLinePayments = null;

            private Int64 _PaymentTransactionServiceLineID = 0;

            
            
            #endregion

            #region " Property Procedures "

            public Int64 PaymentTransactionID
            {
                get { return _PaymentTransactionID; }
                set { _PaymentTransactionID = value; }
            }

            public Int64 PaymentTransactionDetailID
            {
                get { return _PaymentTransactionDetailID; }
                set { _PaymentTransactionDetailID = value; }
            }

            public Int64 PaymentDate
            {
                get { return _PaymentDate; }
                set { _PaymentDate = value; }
            }

            public Int64 PatientID
            {
                get { return _PatientID; }
                set { _PatientID = value; }
            }

            public Int64 TransactionID
            {
                get { return _TransactionID; }
                set { _TransactionID = value; }
            }

            public Int64 TransactionDetailID
            {
                get { return _TransactionDetailID; }
                set { _TransactionDetailID = value; }
            }

            public Int64 TransactionLineNo
            {
                get { return _TransactionLineNo; }
                set { _TransactionLineNo = value; }
            }

            public Int64 ClaimNo
            {
                get { return _ClaimNumber; }
                set { _ClaimNumber = value; }
            }

            public Int64 DOSFrom
            {
                get { return _DOSFrom; }
                set { _DOSFrom = value; }
            }

            public Int64 DOSTo
            {
                get { return _DOSTo; }
                set { _DOSTo = value; }
            }

            public string CPTCode
            {
                get { return _CPTCode; }
                set { _CPTCode = value; }
            }

            public string CPTDescription
            {
                get { return _CPTDescription; }
                set { _CPTDescription = value; }
            }

            public decimal TransactionCharges
            {
                get { return _TransactionCharges; }
                set { _TransactionCharges = value; }
            }

            public decimal TransactionUnit
            {
                get { return _TransactionUnit; }
                set { _TransactionUnit = value; }
            }

            public decimal TransactionTotalCharges
            {
                get { return _TransactionTotalCharges; }
                set { _TransactionTotalCharges = value; }
            }

            public decimal TransactionAllowed
            {
                get { return _TransactionAllowed; }
                set { _TransactionAllowed = value; }
            }

            public decimal TransactionWriteOff
            {
                get { return _TransactionWriteOff; }
                set { _TransactionWriteOff = value; }
            }

            public decimal TransactionPaid
            {
                get { return _TransactionPaid; }
                set { _TransactionPaid = value; }
            }

            public decimal TransactionPaidByPatient
            {
                get { return _TransactionPaidByPatient; }
                set { _TransactionPaidByPatient = value; }
            }

            public decimal TransactionPaidByInsurance
            {
                get { return _TransactionPaidByInsurance; }
                set { _TransactionPaidByInsurance = value; }
            }

            public decimal CurrentPayment
            {
                get { return _CurrentPayment; }
                set { _CurrentPayment = value; }
            }

            public decimal CurrentPayment_Insurance
            {
                get { return _CurrentPayment_Insurance; }
                set { _CurrentPayment_Insurance = value; }
            }

            public decimal CurrentPayment_Patient
            {
                get { return _CurrentPayment_Patient; }
                set { _CurrentPayment_Patient = value; }
            }

            public decimal CurrentPayment_Copay
            {
                get { return _CurrentPayment_Copay; }
                set { _CurrentPayment_Copay = value; }
            }

            public decimal CurrentPayment_Deductable
            {
                get { return _CurrentPayment_Deductable; }
                set { _CurrentPayment_Deductable = value; }
            }

            public decimal CurrentPayment_Adjustment
            {
                get { return _CurrentPayment_Adjustment; }
                set { _CurrentPayment_Adjustment = value; }
            }

            public decimal CurrentPayment_CoInsurance
            {
                get { return _CurrentPayment_CoInsurance; }
                set { _CurrentPayment_CoInsurance = value; }
            }

            public decimal CurrentPayment_Refund
            {
                get { return _CurrentPayment_Refund; }
                set { _CurrentPayment_Refund = value; }
            }

            public decimal CurrentPayment_Withhold
            {
                get { return _CurrentPayment_Withhold; }
                set { _CurrentPayment_Withhold = value; }
            }

            public decimal TransactionBalance
            {
                get { return _TransactionBalance; }
                set { _TransactionBalance = value; }
            }

            public TransactionType TransactionTypeValue
            {
                get { return _TransactionTypeValue; }
                set { _TransactionTypeValue = value; }
            }

            public Int64 TransactionTypeDetailValue
            {
                get { return _TransactionTypeDetailValue; }
                set { _TransactionTypeDetailValue = value; }
            }

            public String TransactionTypeDetailName
            {
                get { return _TransactionTypeDetailName; }
                set { _TransactionTypeDetailName = value; }
            }

            public PaymentMode PaymentModeValue
            {
                get { return _PaymentModeValue; }
                set { _PaymentModeValue = value; }
            }

            public PayerMode PayerModeValue
            {
                get { return _PayerModeValue; }
                set { _PayerModeValue = value; }
            }

            public Int64 PayerModeInsuranceID
            {
                get { return _PayerMode_InsuranceID; }
                set { _PayerMode_InsuranceID = value; }
            }

            public string PayerModeInsuranceName
            {
                get { return _PayerMode_InsuranceName; }
                set { _PayerMode_InsuranceName = value; }
            }

            public string CardNoAndCheckNoAndMoneyOrderNo
            {
                get { return _sCardNoNChkNoNMoneyOrdNo; }
                set { _sCardNoNChkNoNMoneyOrdNo = value; }
            }
            public Int64 CardExpiryAndCheckDateAndMoneyOrderDate
            {
                get { return _nCardExpiryNChkDateNMoneyOrdDate; }
                set { _nCardExpiryNChkDateNMoneyOrdDate = value; }
            }

            public string SecurityNo
            {
                get { return _sSecurityNo; }
                set { _sSecurityNo = value; }
            }

            public string CardType
            {
                get { return _sCardType; }
                set { _sCardType = value; }
            }

            public TransactionStatus PaymentLineStatus
            {
                get { return _LinePaymentStatus; }
                set { _LinePaymentStatus = value; }
            }

            public PaymentsNotes Notes
            {
                get { return _PaymentNotes; }
                set { _PaymentNotes = value; }
            }

            public PaymentClaimExtendedLine ExtendedLine
            {
                get { return _ExtendedLine; }
                set { _ExtendedLine = value; }
            }

            public Int64 CopayID
            {
                get { return _CopayID; }
                set { _CopayID = value; }
            }

            public Int64 PrePayID
            {
                get { return _PrePayID; }
                set { _PrePayID = value; }
            }

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

            public PaymentModeDetails PaymentMode_Details
            {
                get { return _PaymentModeDetails; }
                set { _PaymentModeDetails = value; }
            }
            private Boolean bAssignedTrans = true;
            public TransactionOtherPayments Transaction_OtherPayments
            {
                get { return _TransactionOtherPayments; }
                set 
                {

                    if (bAssignedTrans)
                    {
                        if (_TransactionOtherPayments != null)
                        {
                            _TransactionOtherPayments.Clear();
                            _TransactionOtherPayments.Dispose();
                            _TransactionOtherPayments = null;
                           
                        }
                        bAssignedTrans = false;
                    }
                    _TransactionOtherPayments = value; 
                
                }
            }

            public LinePaymentDetails LinePayment_Details
            {
                get { return _LinePaymentDetails; }
                set { _LinePaymentDetails = value; }
            }

            public Int64 PaymentTransactionServiceLineID
            {
                get { return _PaymentTransactionServiceLineID; }
                set { _PaymentTransactionServiceLineID = value; }
            }

            
            #endregion
        }

        public class PaymentClaimExtendedLine
        {
            #region " Constructor & Destructor "

            private bool disposed = false;

            public PaymentClaimExtendedLine()
            {
            }

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
            ~PaymentClaimExtendedLine()
            {
                Dispose(false);
            }

            #endregion " Constructor & Distructor "

            #region Declaration
                private string _posCode = "";
                private string _posDescription = "";
                private string _tosCode = "";
                private string _tosDescription = "";
                private string _cptcode = "";
                private string _cptDescription = "";
                private string _dx1Code = "";
                private string _dx1Description = "";
                private string _dx2Code = "";
                private string _dx2Description = "";
                private string _dx3Code = "";
                private string _dx3Description = "";
                private string _dx4Code = "";
                private string _dx4Description = "";
                private string _dx5Code = "";
                private string _dx5Description = "";
                private string _dx6Code = "";
                private string _dx6Description = "";
                private string _dx7Code = "";
                private string _dx7Description = "";
                private string _dx8Code = "";
                private string _dx8Description = "";
                private bool _dx1Ptr = false;
                private bool _dx2Ptr = false;
                private bool _dx3Ptr = false;
                private bool _dx4Ptr = false;
                private bool _dx5Ptr = false;
                private bool _dx6Ptr = false;
                private bool _dx7Ptr = false;
                private bool _dx8Ptr = false;
                private string _mod1Code = "";
                private string _mod1Description = "";
                private string _mod2Code = "";
                private string _mod2Description = "";
                private string _mod3Code = "";
                private string _mod3Description = "";
                private string _mod4Code = "";
                private string _mod4Description = "";
                private Int64 _refferingProviderId = 0;
                private string _refferingProvider = "";
                private Int64 _billingProviderId = 0;
                private string _billingProvider = "";
                private string _facilityCode = "";    
                private string _facilityDescription = "";
            #endregion

            #region " Property Procedures "
                public string POSCode
                {
                    get { return _posCode; }
                    set { _posCode = value; }
                }
                public string POSDescription
                {
                    get { return _posDescription; }
                    set { _posDescription = value; }

                }
                public string TOSCode
                {
                    get { return _tosCode; }
                    set { _tosCode = value; }
                }
                public string TOSDescription
                {
                    get { return _tosDescription; }
                    set { _tosDescription = value; }
                }
                public string CPTCode
                {
                    get { return _cptcode; }
                    set { _cptcode = value; }
                }
                public string CPTDescription
                {
                    get { return _cptDescription; }
                    set { _cptDescription = value; }
                }
                public string Dx1Code
                {
                    get { return _dx1Code; }
                    set { _dx1Code = value; }
                }
                public string Dx1Description
                {
                    get { return _dx1Description; }
                    set { _dx1Description = value; }
                }
                public string Dx2Code
                {
                    get { return _dx2Code; }
                    set { _dx2Code = value; }
                }
                public string Dx2Description
                {
                    get { return _dx2Description; }
                    set { _dx2Description = value; }
                }
                public string Dx3Code
                {
                    get { return _dx3Code; }
                    set { _dx3Code = value; }
                }
                public string Dx3Description
                {
                    get { return _dx3Description; }
                    set { _dx3Description = value; }
                }
                public string Dx4Code
                {
                    get { return _dx4Code; }
                    set { _dx4Code = value; }
                }
                public string Dx4Description
                {
                    get { return _dx4Description; }
                    set { _dx4Description = value; }
                }
                public string Dx5Code
                {
                    get { return _dx5Code; }
                    set { _dx5Code = value; }
                }
                public string Dx5Description
                {
                    get { return _dx5Description; }
                    set { _dx5Description = value; }
                }
                public string Dx6Code
                {
                    get { return _dx6Code; }
                    set { _dx6Code = value; }
                }
                public string Dx6Description
                {
                    get { return _dx6Description; }
                    set { _dx6Description = value; }
                }
                public string Dx7Code
                {
                    get { return _dx7Code; }
                    set { _dx7Code = value; }
                }
                public string Dx7Description
                {
                    get { return _dx7Description; }
                    set { _dx7Description = value; }
                }
                public string Dx8Code
                {
                    get { return _dx8Code; }
                    set { _dx8Code = value; }
                }
                public string Dx8Description
                {
                    get { return _dx8Description; }
                    set { _dx8Description = value; }
                }
                public bool Dx1Ptr
                {
                    get { return _dx1Ptr; }
                    set { _dx1Ptr = value; }
                }
                public bool Dx2Ptr
                {
                    get { return _dx2Ptr; }
                    set { _dx2Ptr = value; }
                }
                public bool Dx3Ptr
                {
                    get { return _dx3Ptr; }
                    set { _dx3Ptr = value; }
                }
                public bool Dx4Ptr
                {
                    get { return _dx4Ptr; }
                    set { _dx4Ptr = value; }
                }
                public bool Dx5Ptr
                {
                    get { return _dx5Ptr; }
                    set { _dx5Ptr = value; }
                }
                public bool Dx6Ptr
                {
                    get { return _dx6Ptr; }
                    set { _dx6Ptr = value; }
                }
                public bool Dx7Ptr
                {
                    get { return _dx7Ptr; }
                    set { _dx7Ptr = value; }
                }
                public bool Dx8Ptr
                {
                    get { return _dx8Ptr; }
                    set { _dx8Ptr = value; }
                }
                public string Mod1Code
                {
                    get { return _mod1Code; }
                    set { _mod1Code = value; }
                }
                public string Mod1Description
                {
                    get { return _mod1Description; }
                    set { _mod1Description = value; }
                }
                public string Mod2Code
                {
                    get { return _mod2Code; }
                    set { _mod2Code = value; }
                }
                public string Mod2Description
                {
                    get { return _mod2Description; }
                    set { _mod2Description = value; }
                }
                public string Mod3Code
                {
                    get { return _mod3Code; }
                    set { _mod3Code = value; }
                }
                public string Mod3Description
                {
                    get { return _mod3Description; }
                    set { _mod3Description = value; }
                }
                public string Mod4Code
                {
                    get { return _mod4Code; }
                    set { _mod4Code = value; }
                }
                public string Mod4Description
                {
                    get { return _mod4Description; }
                    set { _mod4Description = value; }
                }
                public Int64 RefferingProviderId
                {
                    get { return _refferingProviderId; }
                    set { _refferingProviderId = value; }
                }
                public string RefferingProvider
                {
                    get { return _refferingProvider; }
                    set { _refferingProvider = value; }
                }
                public Int64 BillingProviderId
                {
                    get { return _billingProviderId; }
                    set { _billingProviderId = value; }
                }
                public string BillingProvider
                {
                    get { return _billingProvider; }
                    set { _billingProvider = value; }
                }
                public string FacilityCode
                {
                    get { return _facilityCode; }
                    set { _facilityCode = value; }
                }
                public string FacilityDescription
                {
                    get { return _facilityDescription; }
                    set { _facilityDescription = value; }
                }
            #endregion
        }

        public class PaymentClaimLines
        {
            
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public PaymentClaimLines()
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


            ~PaymentClaimLines()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(PaymentClaimLine item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(PaymentClaimLine item)
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

            public PaymentClaimLine this[int index]
            {
                get
                { return (PaymentClaimLine)_innerlist[index]; }
            }

            public bool Contains(PaymentClaimLine item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(PaymentClaimLine item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(PaymentClaimLine[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        #region "Claim Line Payment"

        public class ClaimLine
        {
            #region " Constructor & Destructor "

            private bool disposed = false;

            public ClaimLine()
            {
                _ClaimLinePayments = new ClaimLinePayments();
                _ExtendedLine = new PaymentClaimExtendedLine();
            }

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
                        if (_ClaimLinePayments != null) { _ClaimLinePayments.Dispose(); }
                    }
                }
                disposed = true;
            }
            ~ClaimLine()
            {
                Dispose(false);
            }

            #endregion " Constructor & Distructor "

            #region Declaration

            private Int64 _PaymentTransactionID = 0;
            private Int64 _PaymentTransactionDetailID = 0;
            private Int64 _PaymentDate = 0;
            private Int64 _PatientID = 0;
            private Int64 _TransactionID = 0;
            private Int64 _TransactionDetailID = 0;
            private Int64 _TransactionLineNo = 0;
            private Int64 _ClaimNumber = 0;
            private Int64 _DOSFrom = 0;
            private Int64 _DOSTo = 0;
            private string _CPTCode = "";
            private string _CPTDescription = "";
            private decimal _TransactionCharges = 0;
            private decimal _TransactionUnit = 0;
            private decimal _TransactionTotalCharges = 0;
            private decimal _TransactionAllowed = 0;
            private decimal _TransactionWriteOff = 0;
            private decimal _TransactionPaid = 0;
            private decimal _TransactionPaidByPatient = 0;
            private decimal _TransactionPaidByInsurance = 0;
            private decimal _TransactionBalance = 0; 
            
    //        private decimal _CurrentTotalPayment = 0; 
            private PayerMode _PayerModeValue = PayerMode.None;
            private Int64 _PayerMode_InsuranceID = 0;
            private string _PayerMode_InsuranceName = "";
            private TransactionStatus _LinePaymentStatus = TransactionStatus.None;
            private PaymentClaimExtendedLine _ExtendedLine = null;
            private ClaimLinePayments _ClaimLinePayments = null;
            private Int64 _ClinicID = 0;

            private int _ClaimLineStatusID = 0;
            private string _ClaimLineStatus = "";
            private int _SendToFlag = 0;

            private Int64 _CloseDayTrayID = 0;
            private string _CloseDayTrayCode = "";
            private string _CloseDayTrayName = "";
            private Int64 _PaymentTransactionServiceLineID = 0;

            private TransactionType _masterTranType = TransactionType.None;
            private Int64 _nRefundId = 0;
            private string _sRefundCode = "";
            private string _sRefundDesc = "";

            private Int64 _RemitId = 0;

            //Fields added on 20090924 to resolve insurance id problem in multiple payment
            private Int64 _paymentInsuranceId = 0;
            private string _paymentInsuranceName = "";
            
            #endregion

            #region " Property Procedures "

            public Int64 PaymentTransactionID
            {
                get { return _PaymentTransactionID; }
                set { _PaymentTransactionID = value; }
            }

            public Int64 PaymentTransactionDetailID
            {
                get { return _PaymentTransactionDetailID; }
                set { _PaymentTransactionDetailID = value; }
            }

            public Int64 PaymentDate
            {
                get { return _PaymentDate; }
                set { _PaymentDate = value; }
            }

            public Int64 PatientID
            {
                get { return _PatientID; }
                set { _PatientID = value; }
            }

            public Int64 TransactionID
            {
                get { return _TransactionID; }
                set { _TransactionID = value; }
            }

            public Int64 TransactionDetailID
            {
                get { return _TransactionDetailID; }
                set { _TransactionDetailID = value; }
            }

            public Int64 TransactionLineNo
            {
                get { return _TransactionLineNo; }
                set { _TransactionLineNo = value; }
            }

            public Int64 ClaimNo
            {
                get { return _ClaimNumber; }
                set { _ClaimNumber = value; }
            }

            public Int64 DOSFrom
            {
                get { return _DOSFrom; }
                set { _DOSFrom = value; }
            }

            public Int64 DOSTo
            {
                get { return _DOSTo; }
                set { _DOSTo = value; }
            }

            public string CPTCode
            {
                get { return _CPTCode; }
                set { _CPTCode = value; }
            }

            public string CPTDescription
            {
                get { return _CPTDescription; }
                set { _CPTDescription = value; }
            }

            public decimal TransactionCharges
            {
                get { return _TransactionCharges; }
                set { _TransactionCharges = value; }
            }

            public decimal TransactionUnit
            {
                get { return _TransactionUnit; }
                set { _TransactionUnit = value; }
            }

            public decimal TransactionTotalCharges
            {
                get { return _TransactionTotalCharges; }
                set { _TransactionTotalCharges = value; }
            }

            public decimal TransactionAllowed
            {
                get { return _TransactionAllowed; }
                set { _TransactionAllowed = value; }
            }

            public decimal TransactionWriteOff
            {
                get { return _TransactionWriteOff; }
                set { _TransactionWriteOff = value; }
            }

            public decimal TransactionPaid
            {
                get { return _TransactionPaid; }
                set { _TransactionPaid = value; }
            }

            public decimal TransactionPaidByPatient
            {
                get { return _TransactionPaidByPatient; }
                set { _TransactionPaidByPatient = value; }
            }

            public decimal TransactionPaidByInsurance
            {
                get { return _TransactionPaidByInsurance; }
                set { _TransactionPaidByInsurance = value; }
            }
            
            public decimal TransactionBalance
            {
                get { return _TransactionBalance; }
                set { _TransactionBalance = value; }
            }

            public PayerMode PayerModeValue
            {
                get { return _PayerModeValue; }
                set { _PayerModeValue = value; }
            }

            public Int64 PayerModeInsuranceID
            {
                get { return _PayerMode_InsuranceID; }
                set { _PayerMode_InsuranceID = value; }
            }

            public string PayerModeInsuranceName
            {
                get { return _PayerMode_InsuranceName; }
                set { _PayerMode_InsuranceName = value; }
            }

            public TransactionStatus PaymentLineStatus
            {
                get { return _LinePaymentStatus; }
                set { _LinePaymentStatus = value; }
            }

            public PaymentClaimExtendedLine ExtendedLine
            {
                get { return _ExtendedLine; }
                set { _ExtendedLine = value; }
            }

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

            public ClaimLinePayments ClaimLine_Payments
            {
                get { return _ClaimLinePayments; }
                set { _ClaimLinePayments = value; }
            }

            public int ClaimLineStatusID
            {
                get { return _ClaimLineStatusID; }
                set { _ClaimLineStatusID = value; }
            }
            public string ClaimLineStatus
            {
                get { return _ClaimLineStatus; }
                set { _ClaimLineStatus = value; }
            }
            public int SendToFlag
            {
                get { return _SendToFlag; }
                set { _SendToFlag = value; }
            }

            public Int64 CloseDayTrayID
            {
                get { return _CloseDayTrayID; }
                set { _CloseDayTrayID = value; }
            }
            public string CloseDayTrayCode
            {
                get { return _CloseDayTrayCode; }
                set { _CloseDayTrayCode = value; }
            }
            public string CloseDayTrayName
            {
                get { return _CloseDayTrayName; }
                set { _CloseDayTrayName = value; }
            }

            public Int64 PaymentTransactionServiceLineID
            {
                get { return _PaymentTransactionServiceLineID; }
                set { _PaymentTransactionServiceLineID = value; }
            }

            public TransactionType MasterTransactoinType
            {
                get { return _masterTranType; }
                set { _masterTranType = value; }
            }

            public Int64 RefundID
            {
                get { return _nRefundId; }
                set { _nRefundId = value; }
            }
            public string RefundCode
            {
                get { return _sRefundCode; }
                set { _sRefundCode = value; }
            }
            public string RefundDesc
            {
                get { return _sRefundDesc; }
                set { _sRefundDesc = value; }
            }

            public Int64 RemitID
            {
                get { return _RemitId; }
                set { _RemitId = value; }
            }

            public Int64 PaymentInsuranceID
            {
                get { return _paymentInsuranceId; }
                set { _paymentInsuranceId = value; }
            }

            public string PaymentInsuranceName
            {
                get { return _paymentInsuranceName; }
                set { _paymentInsuranceName = value; }
            }

            

            #endregion
        }

        public class ClaimLines : IDisposable
        {
            #region " Constructor & Destructor "

            private bool disposed = false;

            public ClaimLines()
            {
                _innerlist = new ArrayList();
            }

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
            ~ClaimLines()
            {
                Dispose(false);
            }

            #endregion " Constructor & Distructor "

            #region " Variable Declarations "

            protected ArrayList _innerlist;

            #endregion " Variable Declarations "

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(ClaimLine item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(ClaimLine item)
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

            public ClaimLine this[int index]
            {
                get
                { return (ClaimLine)_innerlist[index]; }
            }

            //public ClaimLine this[TransactionType transactiontype]
            //{
            //    get
            //    {
            //        int ind = -1;
            //        for (int i = 0; i <= _innerlist.Count - 1; i++)
            //        {
            //            if (((ClaimLine)_innerlist[i]).Transaction_Type == transactiontype)
            //            {
            //                ind = i;
            //                break;
            //            }
            //        }
            //        if (ind >= 0)
            //        {
            //            return (ClaimLine)_innerlist[ind];
            //        }
            //        else
            //        {
            //            return null;
            //        }
            //    }
            //}

            public bool Contains(ClaimLine item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(ClaimLine item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(ClaimLine[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        public class ClaimLinePayment : IDisposable
        {
            #region " Constructor & Destructor "

            private bool disposed = false;

            public ClaimLinePayment()
            {
                _claimlinepaymentdetails = new ClaimLinePaymentDetails();
                _notes = new PaymentsNotes();
               
            }

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
            ~ClaimLinePayment()
            {
                Dispose(false);
            }

            #endregion " Constructor & Distructor "

            #region " Variable Declarations "

            private Int64 _TransactionID = 0;
            private Int64 _TransactionDetailID = 0;
            private Int64 _TransactionLineNo = 0;

            private decimal _chargedamount = 0;
            private decimal _current_payment = 0;
            private decimal _paid = 0;
            private decimal _pending_balance = 0;
            private PaymentsNotes _notes = null;
            private Int64 _insuranceid = 0;
            private string _insurancename = "";
            private TransactionType _transaction_type = TransactionType.None;
            private ClaimLinePaymentDetails _claimlinepaymentdetails = null;
            private decimal _dbPendingAmount = 0;

            private bool _DataHasToSave = false;

            //..Fields added on 20090807 by - Sagar Ghodke
            //..filelds added for modify payament logic
            private Int64 _paymenttransactionid = 0;
            private Int64 _paymenttransactiondetailid = 0;
            //..

            //...*** Field added on 20090918 by - Sagar Ghodke
            //...*** Field added to retrive the Remit Payment Number
            private Int64 _receivedpaymentcounter = 0;
            //...*** End Field add on 20090918 by - Sagar Ghodke

            #endregion

            #region " Property Procedures "

            public decimal ChargedAmount
            {
                get { return _chargedamount; }
                set { _chargedamount = value; }
            }
            public decimal Current_Payment
            {
                get { return _current_payment; }
                set { _current_payment = value; }
            }
            public decimal Paid
            {
                get { return _paid; }
                set { _paid = value; }
            }
            public decimal Pending_Balance
            {
                get { return _pending_balance; }
                set { _pending_balance = value; }
            }
            public PaymentsNotes Notes
            {
                get { return _notes; }
                set { _notes = value; }
            }
            public Int64 InsuranceID
            {
                get { return _insuranceid; }
                set { _insuranceid = value; }
            }
            public string InsuranceName
            {
                get { return _insurancename; }
                set { _insurancename = value; }
            }
            public TransactionType Transaction_Type
            {
                get { return _transaction_type; }
                set { _transaction_type = value; }
            }
            public ClaimLinePaymentDetails PaymentDetails
            {
                get { return _claimlinepaymentdetails; }
                set { _claimlinepaymentdetails = value; }
            }
            public Int64 TransactionID
            {
                get { return _TransactionID; }
                set { _TransactionID = value; }
            }
            public Int64 TransactionDetailID
            {
                get { return _TransactionDetailID; }
                set { _TransactionDetailID = value; }
            }
            public Int64 TransactionLineNo
            {
                get { return _TransactionLineNo; }
                set { _TransactionLineNo = value; }
            }
            public decimal DBPendingAmount
            {
                get { return _dbPendingAmount; }
                set { _dbPendingAmount = value; }
            }
            public bool DataHasToSave
            {
                get { return _DataHasToSave; }
                set { _DataHasToSave = value; }
            }

            public Int64 PaymentTransactionID
            {
                get { return _paymenttransactionid; }
                set { _paymenttransactionid = value; }
            }

            public Int64 PaymentTransactiondetailID
            {
                get { return _paymenttransactiondetailid; }
                set { _paymenttransactiondetailid = value; }
            }

            public Int64 ReceivedPaymentCounter
            {
                get { return _receivedpaymentcounter; }
                set { _receivedpaymentcounter = value; }
            }

            #endregion " Property Procedures "
        }

        public class ClaimLinePayments : IDisposable
        {
            #region " Constructor & Destructor "

            private bool disposed = false;

            public ClaimLinePayments()
            {
                 _innerlist = new ArrayList();
            }

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
            ~ClaimLinePayments()
            {
                Dispose(false);
            }

            #endregion " Constructor & Distructor "

            #region " Variable Declarations "

            protected ArrayList _innerlist;

            #endregion " Variable Declarations "

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(ClaimLinePayment item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(ClaimLinePayment item)
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

            public ClaimLinePayment this[int index]
            {
                get
                { return (ClaimLinePayment)_innerlist[index]; }
            }

            public ClaimLinePayment this[TransactionType transactiontype]
            {
                get
                {
                    int ind = -1;
                    for (int i = 0; i <= _innerlist.Count - 1; i++)
                    {
                        if (_innerlist[i] != null)
                        {
                            if (((ClaimLinePayment)_innerlist[i]).Transaction_Type == transactiontype)
                            {
                                ind = i;
                                break;
                            }
                        }
                    }
                    if (ind >= 0)
                    {
                        return (ClaimLinePayment)_innerlist[ind];
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public bool Contains(ClaimLinePayment item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(ClaimLinePayment item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(ClaimLinePayment[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }
            
        }

        public class ClaimLinePaymentDetail : IDisposable
        {
            #region " Constructor & Destructor "

            private bool disposed = false;

            public ClaimLinePaymentDetail()
            {
                _paymentmodedetail = new PaymentModeDetail();
            }

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
            ~ClaimLinePaymentDetail()
            {
                Dispose(false);
            }

            #endregion " Constructor & Distructor "

            #region " Variable Declartions "

            private Int64 _paymentid = 0;
            private decimal _payamount = 0;
            private PaymentOtherType _payamounttype = PaymentOtherType.None;
            private PaymentModeDetail _paymentmodedetail = null;

            #endregion " Variable Declartions "

            #region " Property Procedures "

            public Int64 PaymentID
            {
                get { return _paymentid; }
                set { _paymentid = value; }
            }
            public decimal PayAmount
            {
                get { return _payamount; }
                set { _payamount = value; }
            }
            public PaymentOtherType PayAmountType
            {
                get { return _payamounttype; }
                set { _payamounttype = value; }
            }
            public PaymentModeDetail AmountPaymentMode
            {
                get { return _paymentmodedetail; }
                set { _paymentmodedetail = value; }
            }

            #endregion " Property Procedures "
        }

        public class ClaimLinePaymentDetails : IDisposable
        {
            #region " Constructor & Destructor "

            private bool disposed = false;

            public ClaimLinePaymentDetails()
            {
                _innerlist = new ArrayList();
            }

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
            ~ClaimLinePaymentDetails()
            {
                Dispose(false);
            }

            #endregion " Constructor & Distructor "

            #region " Variable Declarations "

            protected ArrayList _innerlist;

            #endregion " Variable Declarations "

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(ClaimLinePaymentDetail item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(ClaimLinePaymentDetail item)
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

            public ClaimLinePaymentDetail this[int index]
            {
                get
                { 
                    return (ClaimLinePaymentDetail)_innerlist[index]; 
                }
            }

            public bool Contains(ClaimLinePaymentDetail item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(ClaimLinePaymentDetail item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(ClaimLinePaymentDetail[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        #endregion

        #region  " General Payment Detail Class to be deleted"

        public class LinePaymentDetail
        {
            #region "Constructor & Distructor"

            public LinePaymentDetail()
            {
                _PaymentModeDetail = new PaymentModeDetail();
                _PayDetails = new AmountDetails();
                _PaymentsNotes = new PaymentsNotes();
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

            ~LinePaymentDetail()
            {
                Dispose(false);
            }

            #endregion

            #region " Variable Declaration "

            private TransactionType _TransactionType = TransactionType.None;
            private Int64 _paymenttransactionid = 0;
            private Int64 _paymenttransactiondetailid = 0;
            private Int64 _transactionid = 0;
            private Int64 _transactiondetailid = 0;
            private Int64 _transactionlineno = 0;
            private Int64 _claimNo = 0;
            private string _view_claimNo = "";
            private decimal _linePaymentAmt = 0;
            private AmountDetails _PayDetails = null;
            private PaymentModeDetail _PaymentModeDetail = null;
            private PaymentsNotes _PaymentsNotes = null;
            private decimal _totalLineAmount = 0;
        //    private decimal _currentPending = 0;

            #endregion " Variable Declaration "

            #region " Property Procedures  "

            public Int64 PaymentTransactionID
            {
                get { return _paymenttransactionid; }
                set { _paymenttransactionid = value; }
            }
            public Int64 PaymentTransactionDetailID
            {
                get { return _paymenttransactiondetailid; }
                set { _paymenttransactiondetailid = value; }
            }
            public Int64 TransactionID
            {
                get { return _transactionid; }
                set { _transactionid = value; }
            }
            public Int64 TransactionDetailID
            {
                get { return _transactiondetailid; }
                set { _transactiondetailid = value; }
            }
            public Int64 TransactionLineNo
            {
                get { return _transactionlineno; }
                set { _transactionlineno = value; }
            }
            public Int64 ClaimNo
            {
                get { return _claimNo; }
                set { _claimNo = value; }
            }
            public string Display_ClaimNo
            {
                get { return _view_claimNo; }
                set { _view_claimNo = value; }
            }
            public decimal LinePaymentAmount
            {
                get { return _linePaymentAmt; }
                set { _linePaymentAmt = value; }
            }
            public AmountDetails PayDetails
            {
                get { return _PayDetails; }
                set { _PayDetails = value; }
            }
            public TransactionType Transaction_Type
            {
                get { return _TransactionType; }
                set { _TransactionType = value; }
            }
            public PaymentModeDetail PaymentMode_Detail
            {
                get { return _PaymentModeDetail; }
                set { _PaymentModeDetail = value; }
            }
            public PaymentsNotes Payments_Notes
            {
                get { return _PaymentsNotes; }
                set { _PaymentsNotes = value; }
            }
            public decimal TotalLineAmount
            {
                get { return _totalLineAmount; }
                set { _totalLineAmount = value; }
            }

            #endregion " Property Procedures  "

        }

        public class LinePaymentDetails
        {

            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public LinePaymentDetails()
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


            ~LinePaymentDetails()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(LinePaymentDetail item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(LinePaymentDetail item)
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

            public LinePaymentDetail this[int index]
            {
                get
                { return (LinePaymentDetail)_innerlist[index]; }
            }

            public LinePaymentDetail this[TransactionType transactiontype]
            {
                get
                {
                    int ind = -1;
                    for (int i = 0; i <= _innerlist.Count - 1; i++)
                    {
                        if (((LinePaymentDetail)_innerlist[i]).Transaction_Type == transactiontype)
                        {
                            ind = i;
                            break;
                        }
                    }
                    if (ind >= 0)
                    {
                        return (LinePaymentDetail)_innerlist[ind];
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public bool Contains(LinePaymentDetail item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(LinePaymentDetail item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(LinePaymentDetail[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        public class AmountDetail
        {
            #region "Constructor & Distructor"

            public AmountDetail()
            {
                _payModeDetail = new PaymentModeDetail();
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

            ~AmountDetail()
            {
                Dispose(false);
            }

            #endregion

            #region " Variable Declaration "

            private Int64 _paymentid = 0;
            private decimal _payAmount = 0;
            private PaymentOtherType _payType = PaymentOtherType.None;
            private TransactionType _appliedPayType = TransactionType.None;
            private bool _isPaid = false;
            private PaymentModeDetail _payModeDetail = null;

            #endregion " Variable Declaration "

            #region " Property Procedures  "

            public Int64 PaymentID
            {
                get { return _paymentid; }
                set { _paymentid = value; }
            }

            public decimal PayAmount
            {
                get { return _payAmount; }
                set { _payAmount = value; }
            }

            public PaymentOtherType PayType
            {
                get { return _payType; }
                set { _payType = value; }
            }
            public TransactionType AppliedToPayType
            {
                get { return _appliedPayType; }
                set { _appliedPayType = value; }
            }
            public bool IsPaid
            {
                get { return _isPaid; }
                set { _isPaid = value; }
            }
            public PaymentModeDetail AmountPayModeDetail
            {
                get { return _payModeDetail; }
                set { _payModeDetail = value; }
            }

            #endregion " Property Procedures  "

        }

        public class AmountDetails
        {

            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public AmountDetails()
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


            ~AmountDetails()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(AmountDetail item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(AmountDetail item)
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

            public AmountDetail this[int index]
            {
                get
                { return (AmountDetail)_innerlist[index]; }
            }

            public AmountDetail this[PaymentOtherType transactiontype]
            {
                get
                {
                    int ind = -1;
                    for (int i = 0; i <= _innerlist.Count - 1; i++)
                    {
                        if (((AmountDetail)_innerlist[i]).PayType == transactiontype)
                        {
                            ind = i;
                            break;
                        }
                    }
                    if (ind >= 0)
                    {
                        return (AmountDetail)_innerlist[ind];
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public bool Contains(AmountDetail item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(AmountDetail item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(AmountDetail[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        public class PendingAmount
        {
            #region "Constructor & Distructor"

            public PendingAmount()
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

            ~PendingAmount()
            {
                Dispose(false);
            }

            #endregion

            #region " Variable Declaration "

            private Int64 _PendingPaymentID = 0;
            private Int64 _TransactionID = 0;
            private Int64 _TransactionDetailID = 0;
            private Int64 _TransactionLineNo = 0;
            private Int64 _ClaimNo = 0;
            private TransactionType _AmountType = TransactionType.None; //(nTransactionType - Type of Pending Amount)
            private decimal _Amount = 0;
            private Int64 _VisitID = 0;
            private Int64 _AppointmentID = 0;
            private bool _IsPaid = false;
            private Int64 _PaymentID = 0;
            private Int64 _PaymentDetailID = 0;
            private Int64 _ReferenceID = 0;

            #endregion " Variable Declaration "

            #region " Property Procedures "

            public Int64 PendingPaymentID
            {
                get { return _PendingPaymentID; }
                set { _PendingPaymentID = value; }
            }
            public Int64 TransactionID
            {
                get { return _TransactionID; }
                set { _TransactionID = value; }
            }
            public Int64 TransactionDetailID
            {
                get { return _TransactionDetailID; }
                set { _TransactionDetailID = value; }
            }
            public Int64 TransactionLineNo
            {
                get { return _TransactionLineNo; }
                set { _TransactionLineNo = value; }
            }
            public Int64 ClaimNo
            {
                get { return _ClaimNo; }
                set { _ClaimNo = value; }
            }
            public TransactionType AmountType //(nTransactionType - Type of Pending Amount)
            {
                get { return _AmountType; }
                set { _AmountType = value; }
            }
            public decimal Amount
            {
                get { return _Amount; }
                set { _Amount = value; }
            }
            public Int64 VisitID
            {
                get { return _VisitID; }
                set { _VisitID = value; }
            }
            public Int64 AppointmentID
            {
                get { return _AppointmentID; }
                set { _AppointmentID = value; }
            }
            public bool IsPaid
            {
                get { return _IsPaid; }
                set { _IsPaid = value; }
            }
            public Int64 PaymentID
            {
                get { return _PaymentID; }
                set { _PaymentID = value; }
            }
            public Int64 PaymentDetailID
            {
                get { return _PaymentDetailID; }
                set { _PaymentDetailID = value; }
            }
            public Int64 ReferenceID
            {
                get { return _ReferenceID; }
                set { _ReferenceID = value; }
            }

            #endregion

        }

        #endregion  " General Payment Detail Class "

        #region "General Payment Mode Details"
        public class PaymentModeDetail
        {
            #region "Constructor & Distructor"

            public PaymentModeDetail()
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

            ~PaymentModeDetail()
            {
                Dispose(false);
            }

            #endregion

            #region " Variables Declarations "

            private TransactionType _TransactionType = TransactionType.None;
            private PaymentMode _PaymentMode = PaymentMode.None;
            private Int64 _CheckMoneyOrderCardExpiryEFT_Date = 0;
            private String _CheckMoneyOrderCardEFT_Number = "";
            private String _CardSecurityNumber = "";
            private Int64 _CardTypeId = 0;
            private String _CardType = "";
            private PayerMode _PayerMode = PayerMode.None;
            private string _CardAuthorizationNo = "";
            private string _AdjustmentType = "";
            private Int64 _AdjustmentTypeId = 0;

            #endregion " Variables Declarations "

            #region " Property Procedures "

            public TransactionType TransactionTypeMode
            {
                get { return _TransactionType; }
                set { _TransactionType = value; }
            }

            public PaymentMode PaymentMode
            {
                get { return _PaymentMode; }
                set { _PaymentMode = value; }
            }
            public Int64 CheckMoneyOrderCardExpiryEFT_Date
            {
                get { return _CheckMoneyOrderCardExpiryEFT_Date; }
                set { _CheckMoneyOrderCardExpiryEFT_Date = value; }
            }
            public String CheckMoneyOrderCardEFT_Number
            {
                get { return _CheckMoneyOrderCardEFT_Number; }
                set { _CheckMoneyOrderCardEFT_Number = value; }
            }
            public String CardSecurityNumber
            {
                get { return _CardSecurityNumber; }
                set { _CardSecurityNumber = value; }
            }

            public Int64 CardTypeID
            {
                get { return _CardTypeId; }
                set { _CardTypeId = value; }
            }

            public String CardType
            {
                get { return _CardType; }
                set { _CardType = value; }
            }
            public PayerMode PayerMode
            {
                get { return _PayerMode; }
                set { _PayerMode = value; }
            }

            public string CardAuthorizationNo
            {
                get { return _CardAuthorizationNo; }
                set { _CardAuthorizationNo = value; }
            }

            public string Adjustment_Type
            {
                get { return _AdjustmentType; }
                set { _AdjustmentType = value; }
            }
            public Int64 AdjustmentTypeID
            {
                get { return _AdjustmentTypeId; }
                set { _AdjustmentTypeId = value; }
            }

            #endregion

        }

        public class PaymentModeDetails
        {

            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public PaymentModeDetails()
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


            ~PaymentModeDetails()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(PaymentModeDetail item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(PaymentModeDetail item)
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

            public PaymentModeDetail this[int index]
            {
                get
                { return (PaymentModeDetail)_innerlist[index]; }
            }

            public PaymentModeDetail this[TransactionType transactiontype]
            {
                get
                {
                    int ind = -1;
                    for (int i = 0; i <= _innerlist.Count - 1; i++)
                    {
                        if (((PaymentModeDetail)_innerlist[i]).TransactionTypeMode == transactiontype)
                        {
                            ind = i;
                            break;
                        }
                    }
                    if (ind >= 0)
                    {
                        return (PaymentModeDetail)_innerlist[ind];
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public bool Contains(PaymentModeDetail item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(PaymentModeDetail item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(PaymentModeDetail[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }
        #endregion

        #region "Line Notes"

        public class PaymentNote
        {
            #region "Constructor & Destructor"

            public PaymentNote()
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

            ~PaymentNote()
            {
                Dispose(false);
            }

            #endregion

            #region Declaration

            private Int64 _PaymentnoteID = 0;
            private string _PaymentnoteDescription = "";
            private NoteType _noteType = NoteType.GeneralNote;
            private Int64 _transactionPaymentId = 0;
            private Int64 _userID = 0;
            private Int64 _noteDate = 0;
            private Int64 _ClinicID = 0;
            private Int64 _PaymentID = 0;

            #endregion

            #region Properties

            public Int64 PaymentNoteID
            {
                get { return _PaymentnoteID; }
                set { _PaymentnoteID = value; }
            }

            public Int64 PaymentID
            {
                get { return _PaymentID; }
                set { _PaymentID = value; }
            }

            public string NoteDescription
            {
                get { return _PaymentnoteDescription; }
                set { _PaymentnoteDescription = value; }
            }

            public NoteType NoteType
            {
                get { return _noteType; }
                set { _noteType = value; }
            }
            public Int64 TransactionPaymentDetailId
            {
                get { return _transactionPaymentId; }
                set { _transactionPaymentId = value; }
            }
            public Int64 UserID
            {
                get { return _userID; }
                set { _userID = value; }
            }
            public Int64 NoteDate
            {
                get { return _noteDate; }
                set { _noteDate = value; }
            }
            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }
            #endregion

        }

        public class PaymentsNotes
        {

            protected ArrayList _innerlist;

            #region "Constructor & Distructor"

            public PaymentsNotes()
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

            ~PaymentsNotes()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(PaymentNote item)
            {
                _innerlist.Add(item);
            }

            //Remark - Work Remaining for comparision
            public bool Remove(PaymentNote item)
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

            public PaymentNote this[int index]
            {
                get
                { return (PaymentNote)_innerlist[index]; }
            }

            public bool Contains(PaymentNote item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(PaymentNote item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(PaymentNote[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        #endregion

        #region "Patient Balance for Report"
        public class PatientBalance
        { 
            #region " Constructor & Destructor "

            private bool disposed = false;

            public PatientBalance()
            {
                
            }

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
            ~PatientBalance()
            {
                Dispose(false);
            }

            #endregion " Constructor & Distructor "

            private Int64 _SelfInsuranceID = 0;
            private PayerMode _SelfInsuranceType = PayerMode.None;
            private string _SelfInsuranceName = "";
            private decimal _SelfInsuranceTotalCharges = 0;
            private decimal _SelfInsuranceTotalAllowed = 0;
            private decimal _SelfInsurancePaid = 0;
            private decimal _SelfInsuranceBalance = 0;

            public Int64 SelfInsuranceID
            {
                get { return _SelfInsuranceID; }
                set { _SelfInsuranceID = value; }
            }

            public PayerMode SelfInsuranceType
            {
                get { return _SelfInsuranceType; }
                set { _SelfInsuranceType = value; }
            }

            public string SelfInsuranceName
            {
                get { return _SelfInsuranceName; }
                set { _SelfInsuranceName = value; }
            }

            public decimal SelfInsuranceCharges
            {
                get { return _SelfInsuranceTotalCharges; }
                set { _SelfInsuranceTotalCharges = value; }
            }

            public decimal SelfInsuranceAllowed
            {
                get { return _SelfInsuranceTotalAllowed; }
                set { _SelfInsuranceTotalAllowed = value; }
            }


            public decimal SelfInsurancePaid
            {
                get { return _SelfInsurancePaid; }
                set { _SelfInsurancePaid = value; }
            }

            public decimal SelfInsuranceBalance
            {
                get { return _SelfInsuranceBalance; }
                set { _SelfInsuranceBalance = value; }
            }
        }

        public class PatientBalances
        {

            protected ArrayList _innerlist;

            #region "Constructor & Distructor"

            public PatientBalances()
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

            ~PatientBalances()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(PatientBalance item)
            {
                _innerlist.Add(item);
            }

            //Remark - Work Remaining for comparision
            public bool Remove(PatientBalance item)
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

            public PatientBalance this[int index]
            {
                get
                { return (PatientBalance)_innerlist[index]; }
            }

            public bool Contains(PatientBalance item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(PatientBalance item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(PatientBalance[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        #endregion

        #region "Other Payment like pending copay, coinsurance. The entry which will record from payment form"
        public class TransactionOtherPayment
        {
            #region "Constructor & Destructor"

            public TransactionOtherPayment()
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

            ~TransactionOtherPayment()
            {
                Dispose(false);
            }

            #endregion

            #region " Private & Public Variables "

            private Int64 _otherPayId = 0;
            private TransactionType _otherPaymentTypeid = TransactionType.None;
            private decimal _PaymentAmount = 0;
            private bool _isOtherPaymentPaid = false;
            private Int64 _transactionId = 0;
            private Int64 _transactionDetailId = 0;
            private Int64 _transactionLineNo = 0;
            private Int64 _lineVisitid = 0;
            private Int64 _lineAppointmentid = 0;
            private Int64 _paymentId = 0;
            private Int64 _paymentDetailId = 0;
            private Int64 _referenceId = 0;

            #endregion " Private & Public Variables "

            #region " Property Procedures "

            public Int64 OtherPayID
            {
                get { return _otherPayId; }
                set { _otherPayId = value; }
            }
            public TransactionType OtherPayment_Type
            {
                get { return _otherPaymentTypeid; }
                set { _otherPaymentTypeid = value; }
            }
            public decimal PaymentAmount
            {
                get { return _PaymentAmount; }
                set { _PaymentAmount = value; }
            }
            public bool IsOtherPaymentPaid
            {
                get { return _isOtherPaymentPaid; }
                set { _isOtherPaymentPaid = value; }
            }
            public Int64 TransactionID
            {
                get { return _transactionId; }
                set { _transactionId = value; }
            }
            public Int64 TransactionDetailID
            {
                get { return _transactionDetailId; }
                set { _transactionDetailId = value; }
            }
            public Int64 TransactionLineNo
            {
                get { return _transactionLineNo; }
                set { _transactionLineNo = value; }
            }
            public Int64 LineVisitID
            {
                get { return _lineVisitid; }
                set { _lineVisitid = value; }
            }
            public Int64 LineAppointmentID
            {
                get { return _lineAppointmentid; }
                set { _lineAppointmentid = value; }
            }
            public Int64 PaymentID
            {
                get { return _paymentId; }
                set { _paymentId = value; }
            }
            public Int64 PaymentDetailID
            {
                get { return _paymentDetailId; }
                set { _paymentDetailId = value; }
            }
            public Int64 ReferenceID
            {
                get { return _referenceId; }
                set { _referenceId = value; }
            }

            #endregion " Property Procedures "

        }

        public class TransactionOtherPayments
        {

            protected ArrayList _innerlist;

            #region "Constructor & Distructor"

            public TransactionOtherPayments()
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

            ~TransactionOtherPayments()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(TransactionOtherPayment item)
            {
                _innerlist.Add(item);
            }

            //Remark - Work Remaining for comparision
            public bool Remove(TransactionOtherPayment item)
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

            public TransactionOtherPayment this[int index]
            {
                get
                { return (TransactionOtherPayment)_innerlist[index]; }
            }

            public bool Contains(TransactionOtherPayment item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(TransactionOtherPayment item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(TransactionOtherPayment[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        #endregion

        #region "Old Classes Temporary"
        public class TransactionPayment
        {
            #region "Constructor & Destructor"

            public TransactionPayment()
            {
                _TransactionPaymentDetails = new TransactionPaymentDetails();
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
                        if (_TransactionPaymentDetails != null)
                        {
                            _TransactionPaymentDetails.Dispose();
                            _TransactionPaymentDetails = null;
                        }
                    }
                }
                disposed = true;
            }

            ~TransactionPayment()
            {
                Dispose(false);
            }

            #endregion

            #region " Variable Declarations "

            private Int64 _nTransactionPaymentID = 0;
            private Int64 _nPaymentDate = 0;
            private Int64 _nPaymentTime = 0;
            private string _sBatchNumber = "";
            private PaymentMode _TypeOfPaymentValue = PaymentMode.None;
            private PayerMode _PayerTypeValue = PayerMode.None;
            private PayerMode _nPaymentMode = PayerMode.None;
            private decimal _dPayerAmount = 0;
            private string _sCheckNumber = "";
            private Int64 _nCheckDate = 0;
            private string _sMoneyOrderCheckNumber = "";
            private Int64 _nMoneyOrderDate = 0;
            private string _sCreditCardNumber = "";
            private string _sCreditSecurityNo = "";
            private string _sCardType = "";
            private Int64 _nCardExpiryDate = 0;
            private TransactionStatus _nTransactionPaymentStatus = TransactionStatus.None;
            private Int64 _nClinicID = 0;
            private decimal _dMasterTransactionBalance = 0;
            private TransactionPaymentDetails _TransactionPaymentDetails = null;

            #endregion

            #region " Property Procedures "

            public Int64 TransactionPaymentID
            {
                get { return _nTransactionPaymentID; }
                set { _nTransactionPaymentID = value; }
            }
            public Int64 PaymentDate
            {
                get { return _nPaymentDate; }
                set { _nPaymentDate = value; }
            }
            public Int64 PaymentTime
            {
                get { return _nPaymentTime; }
                set { _nPaymentTime = value; }
            }
            public string BatchNumber
            {
                get { return _sBatchNumber; }
                set { _sBatchNumber = value; }
            }

            public PaymentMode TypeOfPaymentValue
            {
                get { return _TypeOfPaymentValue; }
                set { _TypeOfPaymentValue = value; }
            }
            public PayerMode PayerTypeValue
            {
                get { return _PayerTypeValue; }
                set { _PayerTypeValue = value; }
            }
            public PayerMode PaymentModeValue
            {
                get { return _nPaymentMode; }
                set { _nPaymentMode = value; }
            }
            public decimal PayerAmount
            {
                get { return _dPayerAmount; }
                set { _dPayerAmount = value; }
            }
            public string CheckNumber
            {
                get { return _sCheckNumber; }
                set { _sCheckNumber = value; }
            }
            public Int64 CheckDate
            {
                get { return _nCheckDate; }
                set { _nCheckDate = value; }
            }
            public string MoneyOrderCheckNumber
            {
                get { return _sMoneyOrderCheckNumber; }
                set { _sMoneyOrderCheckNumber = value; }
            }
            public Int64 MoneyOrderDate
            {
                get { return _nMoneyOrderDate; }
                set { _nMoneyOrderDate = value; }
            }
            public string CreditCardNumber
            {
                get { return _sCreditCardNumber; }
                set { _sCreditCardNumber = value; }
            }
            public string CreditSecurityNo
            {
                get { return _sCreditSecurityNo; }
                set { _sCreditSecurityNo = value; }
            }
            public string CardType
            {
                get { return _sCardType; }
                set { _sCardType = value; }
            }
            public Int64 CardExpiryDate
            {
                get { return _nCardExpiryDate; }
                set { _nCardExpiryDate = value; }
            }
            public TransactionStatus TransactionPaymentStatusValue
            {
                get { return _nTransactionPaymentStatus; }
                set { _nTransactionPaymentStatus = value; }
            }
            public Int64 ClinicID
            {
                get { return _nClinicID; }
                set { _nClinicID = value; }
            }
            public TransactionPaymentDetails TransactionPayments
            {
                get { return _TransactionPaymentDetails; }
                set { _TransactionPaymentDetails = value; }
            }
            public decimal MasterTransactionBalance
            {
                get { return _dMasterTransactionBalance; }
                set { _dMasterTransactionBalance = value; }
            }

            #endregion

        }

        public class TransactionPaymentDetail
        {
            #region " Constructor & Destructor "

            private bool disposed = false;

            public TransactionPaymentDetail()
            {
            }

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
            ~TransactionPaymentDetail()
            {
                Dispose(false);
            }

            #endregion " Constructor & Distructor "

            #region Declaration

            private Int64 _TransactionPaymentID = 0;
            private Int64 _TransactionPaymentDetailID = 0;
            private Int64 _TransactionID = 0;
            private Int64 _TransactionDetailID = 0;
            private Int64 _TransactionLineNo = 0;
            private Int64 _PaymentDate = 0;
            private Int64 _PatientID = 0;

            private PaymentMode _TypeOfPaymentValue = PaymentMode.None;
            private PayerMode _PayerTypeValue = PayerMode.None;
            private Int64 _nDateOfServie = 0;
            private string _CPTCode = "";
            private string _CPTDescription = "";
            private decimal _ClinicAmt = 0;
            private decimal _ChargesAmt = 0;
            private decimal _AllowedAmt = 0;
            private decimal _PaymentAmt = 0;
            private decimal _AdjustmentAmt = 0;
            private decimal _WriteOffAmt = 0;
            private decimal _CoPayAmt = 0;
            private decimal _DeductibleAmt = 0;
            private Int64 _PaymentInsuranceID = 0;
            private string _InsuranceName = "";
            private Int32 _ItemNo = 0;
            private decimal _TransactionBalance = 0;
            private decimal _TotalBalance = 0;
            private PaymentsNotes _PaymentNotes = null;
            private TransactionStatus _LinePaymentStatus = TransactionStatus.None;
            private Int64 _ClinicID = 0;

            #endregion

            #region " Property Procedures "

            public Int64 TransactionPaymentID
            {
                get { return _TransactionPaymentID; }
                set { _TransactionPaymentID = value; }
            }
            public Int64 TransactionPaymentDetailID
            {
                get { return _TransactionPaymentDetailID; }
                set { _TransactionPaymentDetailID = value; }
            }
            public Int64 TransactionID
            {
                get { return _TransactionID; }
                set { _TransactionID = value; }
            }
            public Int64 TransactionDetailID
            {
                get { return _TransactionDetailID; }
                set { _TransactionDetailID = value; }
            }
            public Int64 TransactionLineNo
            {
                get { return _TransactionLineNo; }
                set { _TransactionLineNo = value; }
            }
            public Int64 PaymentDate
            {
                get { return _PaymentDate; }
                set { _PaymentDate = value; }
            }
            public Int64 PatientID
            {
                get { return _PatientID; }
                set { _PatientID = value; }
            }

            public PaymentMode TypeOfPaymentValue
            {
                get { return _TypeOfPaymentValue; }
                set { _TypeOfPaymentValue = value; }
            }
            public PayerMode PayerTypeValue
            {
                get { return _PayerTypeValue; }
                set { _PayerTypeValue = value; }
            }
            public Int64 DateOfServie
            {
                get { return _nDateOfServie; }
                set { _nDateOfServie = value; }
            }
            public string CPTCode
            {
                get { return _CPTCode; }
                set { _CPTCode = value; }
            }
            public string CPTDescription
            {
                get { return _CPTDescription; }
                set { _CPTDescription = value; }
            }
            public decimal ClinicAmt
            {
                get { return _ClinicAmt; }
                set { _ClinicAmt = value; }
            }
            public decimal ChargesAmt
            {
                get { return _ChargesAmt; }
                set { _ChargesAmt = value; }
            }
            public decimal AllowedAmt
            {
                get { return _AllowedAmt; }
                set { _AllowedAmt = value; }
            }
            public decimal PaymentAmt
            {
                get { return _PaymentAmt; }
                set { _PaymentAmt = value; }
            }
            public decimal AdjustmentAmt
            {
                get { return _AdjustmentAmt; }
                set { _AdjustmentAmt = value; }
            }
            public decimal WriteOffAmt
            {
                get { return _WriteOffAmt; }
                set { _WriteOffAmt = value; }
            }
            public decimal CoPayAmt
            {
                get { return _CoPayAmt; }
                set { _CoPayAmt = value; }
            }
            public decimal DeductibleAmt
            {
                get { return _DeductibleAmt; }
                set { _DeductibleAmt = value; }
            }
            public Int64 PaymentInsuranceID
            {
                get { return _PaymentInsuranceID; }
                set { _PaymentInsuranceID = value; }
            }
            public string InsuranceName
            {
                get { return _InsuranceName; }
                set { _InsuranceName = value; }
            }
            public Int32 ItemNo
            {
                get { return _ItemNo; }
                set { _ItemNo = value; }
            }
            public decimal TransactionBalance
            {
                get { return _TransactionBalance; }
                set { _TransactionBalance = value; }
            }
            public decimal TotalBalance
            {
                get { return _TotalBalance; }
                set { _TotalBalance = value; }
            }
            public PaymentsNotes PaymentNotes
            {
                get { return _PaymentNotes; }
                set { _PaymentNotes = value; }
            }
            public TransactionStatus LinePaymentStatus
            {
                get { return _LinePaymentStatus; }
                set { _LinePaymentStatus = value; }
            }
            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }


            #endregion
        }

        public class TransactionPaymentDetails
        {

            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public TransactionPaymentDetails()
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


            ~TransactionPaymentDetails()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(TransactionPaymentDetail item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(TransactionPaymentDetail item)
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

            public TransactionPaymentDetail this[int index]
            {
                get
                { return (TransactionPaymentDetail)_innerlist[index]; }
            }

            public bool Contains(TransactionPaymentDetail item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(TransactionPaymentDetail item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(TransactionPaymentDetail[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }
        #endregion
    }
}
