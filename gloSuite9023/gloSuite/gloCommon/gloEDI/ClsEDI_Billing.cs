using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Windows.Forms;
using gloEDI.gloBilling.Common;


namespace gloEDI
{
    namespace gloBilling
    {
        #region " Enumeratioins "

        public enum ClaimValidationService
        {
            YOST = 1,
            Alpha2 = 2,
            None = 0
        }

        public enum TransactionType
        {
            None = 1, Bill = 2, Payment = 3, Receipt = 4
        }

        public enum PaymentMode
        {
            None = 1, Insurance = 2, Self = 3, CoPay = 4
        }

        public enum PaymentModeType
        {
            None = 1, CoPayInsurance = 2, CoPaySelf = 3
        }

        public enum TaxType
        {
            None = 1, Discount = 2
        }

        public enum NoteType
        {
            GeneralNote = 0, CashPayment = 1
        }

        public enum ReceiptEntryType
        {
            None = 1,
            SinglePatient = 2,
            MultiplePatient = 3,
            Adjuestment = 4,
            Credit = 5
        }

        public enum TypeOfPayment
        {
            None = 1,
            Cash = 2,
            Check = 3,
            MoneyOrder = 4,
            CreditCard = 5
        }

        public enum PayerType
        {
            None = 1,
            Self = 2,
            Insurance = 3
        }

        public enum TransactionPaymentMode
        {
            None = 1,
            Self = 2,
            Insurance = 3,
            BalanceSelf = 4,
            BalanceInsurance = 5,
            Payment = 6,
            Adjustment = 7,
            Credit = 8
        }

        public enum TransactionStatus
        {
            Hold = 1, ReadyToSend = 2, Rejected = 3, Challenge = 4, Alert = 5, Done = 6, Pending = 7, None = 8, SendToEDI = 9, PartialPaid = 10, Paid = 11
        }

        public enum InsuranceTypeFlag
        {
            None = 0,
            Primary = 1,
            Secondary = 2,
            Tertiary = 3
        }

        public enum PayerMode
        {
            None = 0,
            Self = 1,
            Insurance = 2
        }

        #endregion

        namespace Common
        {
            //Transaction
            public class Transaction
            {
                #region "Constructor & Destructor"

                public Transaction()
                {
                    _Lines = new TransactionLines();
                    _Insurances = new TransactionInsurances();
                    _TransactionDetails = new TransactionDetails();
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
                            _Lines.Dispose();
                            _Insurances.Dispose();

                            if (bAssignedTransactionDetails)
                            {
                                if (_TransactionDetails != null)
                                {

                                    _TransactionDetails.Dispose();
                                    _TransactionDetails = null;

                                }
                                bAssignedTransactionDetails = false;
                            }


                        }
                    }
                    disposed = true;
                }

                ~Transaction()
                {
                    Dispose(false);
                }

                #endregion

                #region " Declarations "

                private Int64 _TransactionID;
                private Int64 _MasterAppointmentID;
                private Int64 _AppointmentID;
                private Int64 _VisitID;
                private TransactionType _TransactionType;
                private Int64 _OnsiteDate;
                private Int64 _InjuryDate;
                private Int64 _UnableToWorkFromDate;
                private Int64 _UnableToWorkTillDate;
                private Int64 _TransactionDate;
                private String _CaseNoPrefix;
                private Int64 _ClaimNo;
                private String _BatchNoPrefix;
                private Int64 _BatchNo;
                private Int64 _PatientID;
                private Int64 _ProviderID;
                private string _ProviderName;
                private string _MaritalStatus;
                private string _FacilityCode;
                private string _FacilityDescription;
                System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
                private Int64 _ClinicID = 0;
                private Int64 _PrefixID = 0;
                //Lines
                private TransactionLines _Lines;
                private TransactionInsurances _Insurances;

                private TransactionStatus _TransactionStatus = TransactionStatus.None;
                private string _State = "";

                private Int64 _HospitalizationDateFrom = 0;
                private Int64 _HospitalizationDateTo = 0;
                private bool _OutSideLab = false;
                private decimal _OutSideLabCharges = 0;

                private bool _AutoClaim = false;
                private Int64 _AccidentDate = 0;
                private bool _WorkersComp = false;

                private TransactionDetails _TransactionDetails = null;

                #endregion " Declarations "

                #region Property Procedures of Transaction Class

                public Int64 TransactionID
                {
                    get { return _TransactionID; }
                    set { _TransactionID = value; }
                }

                public Int64 MasterAppointmentID
                {
                    get { return _MasterAppointmentID; }
                    set { _MasterAppointmentID = value; }
                }

                public Int64 AppointmentID
                {
                    get { return _AppointmentID; }
                    set { _AppointmentID = value; }
                }

                public Int64 VisitID
                {
                    get { return _VisitID; }
                    set { _VisitID = value; }
                }

                public TransactionType TransactionMode
                {
                    get { return _TransactionType; }
                    set { _TransactionType = value; }
                }

                public Int64 OnsiteDate
                {
                    get { return _OnsiteDate; }
                    set { _OnsiteDate = value; }
                }

                public Int64 InjuryDate
                {
                    get { return _InjuryDate; }
                    set { _InjuryDate = value; }
                }

                public Int64 UnableToWorkFromDate
                {
                    get { return _UnableToWorkFromDate; }
                    set { _UnableToWorkFromDate = value; }
                }

                public Int64 UnableToWorkTillDate
                {
                    get { return _UnableToWorkTillDate; }
                    set { _UnableToWorkTillDate = value; }
                }

                public Int64 TransactionDate
                {
                    get { return _TransactionDate; }
                    set { _TransactionDate = value; }
                }

                public Int64 PatientID
                {
                    get { return _PatientID; }
                    set { _PatientID = value; }
                }

                public Int64 ProviderID
                {
                    get { return _ProviderID; }
                    set { _ProviderID = value; }
                }

                public String ProviderName
                {
                    get { return _ProviderName; }
                    set { _ProviderName = value; }
                }
                public String CaseNoPrefix
                {
                    get { return _CaseNoPrefix; }
                    set { _CaseNoPrefix = value; }
                }

                public Int64 ClaimNo
                {
                    get { return _ClaimNo; }
                    set { _ClaimNo = value; }
                }

                public String BatchNoPrefix
                {
                    get { return _BatchNoPrefix; }
                    set { _BatchNoPrefix = value; }
                }

                public Int64 BatchNo
                {
                    get { return _BatchNo; }
                    set { _BatchNo = value; }
                }

                public Int64 PrefixID
                {
                    get { return _PrefixID; }
                    set { _PrefixID = value; }
                }

                public Int64 ClinicID
                {
                    get { return _ClinicID; }
                    set { _ClinicID = value; }
                }

                public TransactionLines Lines
                {
                    get { return _Lines; }
                    set { _Lines = value; }
                }

                public TransactionInsurances Insurances
                {
                    get { return _Insurances; }
                    set { _Insurances = value; }
                }

                public string MaritalStatus
                {
                    get { return _MaritalStatus; }
                    set { _MaritalStatus = value; }
                }

                public string FacilityCode
                {
                    get { return _FacilityCode; }
                    set { _FacilityCode = value; }
                }

                public string FacilityDescription
                {
                    get { return _FacilityDescription; }
                    set { _FacilityDescription = value; }
                }

                public string State
                {
                    get { return _State; }
                    set { _State = value; }
                }

                public TransactionStatus Transaction_Status
                {
                    get { return _TransactionStatus; }
                    set { _TransactionStatus = value; }
                }

                public Int64 HospitalizationDateFrom
                {
                    get { return _HospitalizationDateFrom; }
                    set { _HospitalizationDateFrom = value; }
                }
                public Int64 HospitalizationDateTo
                {
                    get { return _HospitalizationDateTo; }
                    set { _HospitalizationDateTo = value; }
                }
                public bool OutSideLab
                {
                    get { return _OutSideLab; }
                    set { _OutSideLab = value; }
                }
                public decimal OutSideLabCharges
                {
                    get { return _OutSideLabCharges; }
                    set { _OutSideLabCharges = value; }
                }
                public bool AutoClaim
                {
                    get { return _AutoClaim; }
                    set { _AutoClaim = value; }
                }
                public Int64 AccidentDate
                {
                    get { return _AccidentDate; }
                    set { _AccidentDate = value; }
                }
                public bool WorkersComp
                {
                    get { return _WorkersComp; }
                    set { _WorkersComp = value; }
                }
                private Boolean bAssignedTransactionDetails = true;
                public TransactionDetails Transaction_Details
                {
                    get { return _TransactionDetails; }
                    set
                    {
                        if (bAssignedTransactionDetails)
                        {
                            if (_TransactionDetails != null)
                            {

                                _TransactionDetails.Dispose();
                                _TransactionDetails = null;

                            }
                            bAssignedTransactionDetails = false;
                        }
                        _TransactionDetails = value;
                    }
                }

                #endregion

            }

            public class TransactionDetails
            {
                #region "Constructor & Destructor"

                public TransactionDetails()
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

                ~TransactionDetails()
                {
                    Dispose(false);
                }

                #endregion

                #region "Variables"

                public string HCFA_PatientFName = "";
                public string HCFA_PatientLName = "";
                public string HCFA_PatientMName = "";
                public string HCFA_PatientGender = "";
                public Int64 HCFA_PatientDOB = 0;
                public bool HCFA_IsEmployed = false;
                public bool HCFA_IsFullTimeStudent = false;
                public bool HCFA_IsPartTimeStudent = false;
                public string HCFA_PatientAddress1 = "";
                public string HCFA_PatientAddress2 = "";
                public string HCFA_PatientCity = "";
                public string HCFA_PatientState = "";
                public string HCFA_PatientZip = "";
                public string HCFA_PatientCode = "";
                public string HCFA_PatientPhone = "";
                public string HCFA_PatientSSN = "";
                public string HCFA_PatientEmploymentStatus = "";
                public string HCFA_PriorAuthorizationNo = "";

                public string HCFA_ProviderFName = "";
                public string HCFA_ProviderMName = "";
                public string HCFA_ProviderLName = "";
                public string HCFA_ProviderAddress1 = "";
                public string HCFA_ProviderAddress2 = "";
                public string HCFA_ProviderCity = "";
                public string HCFA_ProviderState = "";
                public string HCFA_ProviderZip = "";
                public string HCFA_ProviderPhone = "";
                public string HCFA_ProviderNPI = "";
                public string HCFA_ProviderUPIN = "";
                public string HCFA_ProviderEIN = "";
                public string HCFA_ProviderSSN = "";
                public string HCFA_ProviderTaxonomy = "";
                public string HCFA_ProviderStateMedicalNo = "";

                public string HCFA_PayerAddress1 = "";
                public string HCFA_PayerAddress2 = "";
                public string HCFA_PayerCity = "";
                public string HCFA_PayerState = "";
                public string HCFA_PayerZip = "";

                public string HCFA_InsuranceName = "";
                public string HCFA_InsuredsFName = "";
                public string HCFA_InsuredsLName = "";
                public string HCFA_InsuredsMName = "";
                public Int64 HCFA_InsuredsDOB = 0;
                public string HCFA_InsuredsAddress1 = "";
                public string HCFA_InsuredsAddress2 = "";
                public string HCFA_InsuredsCity = "";
                public string HCFA_InsuredsState = "";
                public string HCFA_InsuredsZip = "";
                public string HCFA_InsuredsPhone = "";
                public string HCFA_InsuranceID = "";
                public string HCFA_PayerID = "";

                public string HCFA_OtherInsuranceName = "";
                public string HCFA_OtherInsuredsFName = "";
                public string HCFA_OtherInsuredsLName = "";
                public string HCFA_OtherInsuredsMName = "";
                public Int64 HCFA_OtherInsuredsDOB = 0;
                public string HCFA_OtherInsuredsAddress1 = "";
                public string HCFA_OtherInsuredsAddress2 = "";
                public string HCFA_OtherInsuredsCity = "";
                public string HCFA_OtherInsuredsState = "";
                public string HCFA_OtherInsuredsZip = "";
                public string HCFA_OtherInsuredsPhone = "";
                public string HCFA_OtherInsuranceID = "";
                public string HCFA_OtherPayerID = "";

                public string HCFA_FacilityCode = "";
                public string HCFA_FacilityName = "";
                public string HCFA_FacilityNPI = "";
                public string HCFA_FacilityAddress1 = "";
                public string HCFA_FacilityAddress2 = "";
                public string HCFA_FacilityCity = "";
                public string HCFA_FacilityState = "";
                public string HCFA_FacilityZip = "";

                public string HCFA_ReferringProviderFName = "";
                public string HCFA_ReferringProviderLName = "";
                public string HCFA_ReferringProviderMName = "";
                public string HCFA_ReferringProviderNPI = "";
                public string HCFA_ReferringProviderAddress1 = "";
                public string HCFA_ReferringProviderAddress2 = "";
                public string HCFA_ReferringProviderCity = "";
                public string HCFA_ReferringProviderState = "";
                public string HCFA_ReferringProviderZip = "";
                public string HCFA_ReferringProviderPhone = "";
                public string HCFA_ReferringProviderUPIN = "";
                public string HCFA_ReferringProviderSSN = "";
                public string HCFA_ReferringProviderEIN = "";
                public string HCFA_ReferringProviderTaxanomy = "";

                //public string HCFA_PriorAuthorizationNo = "";

                #endregion "Variables"

            }

            public class TransactionLine
            {
                #region " Constructor & Destructor "

                private bool disposed = false;

                public TransactionLine()
                {
                    _generalNotes = new GeneralNotes();
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
                            _generalNotes.Dispose();
                        }
                    }
                    disposed = true;
                }
                ~TransactionLine()
                {
                    Dispose(false);
                }

                #endregion " Constructor & Distructor "

                #region " Private Variables "

                private string _caseNo = "";
                private Int64 _transactionId = 0;
                private Int64 _transactionLineId = 0;
                private Int64 _TransactionDetailId = 0;
                private DateTime _dateServiceFrom;
                private DateTime _dateServiceTill;
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
                private decimal _charges = 0;
                private Int16 _unit = 0;
                private decimal _total = 0;
                private decimal _allowedCharges = 0;
                private decimal _paidCharges = 0;
                private decimal _balanceCharges = 0;
                private Int64 _refferingProviderId = 0;
                private string _refferingProvider = "";
                private GeneralNotes _generalNotes = null;
                private Int64 _clinicID = 0;
                private Int64 _InsuranceID = 0;
                private string _InsuranceName = "";
                private string _InsurancePrmSecTer = "";
                private PayerMode _InsSelfMode = PayerMode.None;
                private TransactionStatus _LineStatus = TransactionStatus.None;
                private Int64 _ClaimNumber = 0;

                //Added by Anil on 20081113
                public string HCFA_RenderingFName = "";
                public string HCFA_RenderingMName = "";
                public string HCFA_RenderingLName = "";
                public string HCFA_RenderingProviderAddress1 = "";
                public string HCFA_RenderingProviderAddress2 = "";
                public string HCFA_RenderingProviderCity = "";
                public string HCFA_RenderingProviderState = "";
                public string HCFA_RenderingProviderZip = "";
                public string HCFA_RenderingProviderPhone = "";
                public string HCFA_RenderingProviderNPI = "";
                public string HCFA_RenderingProviderMedicalLicenceNo = "";
                public string HCFA_RenderingProviderUPIN = "";
                public string HCFA_RenderingProviderEIN = "";
                public string HCFA_RenderingProviderSSN = "";
                public string HCFA_RenderingProviderTaxonomy = "";

                #endregion " Private Variables "

                #region " Public Property Procedures "

                public string CaseNo
                {
                    get { return _caseNo; }
                    set { _caseNo = value; }
                }
                public Int64 TransactionId
                {
                    get { return _transactionId; }
                    set { _transactionId = value; }
                }
                public Int64 TransactionLineId
                {
                    get { return _transactionLineId; }
                    set { _transactionLineId = value; }
                }
                public Int64 TransactionDetailID
                {
                    get { return _TransactionDetailId; }
                    set { _TransactionDetailId = value; }
                }
                public DateTime DateServiceFrom
                {
                    get { return _dateServiceFrom; }
                    set { _dateServiceFrom = value; }
                }
                public DateTime DateServiceTill
                {
                    get { return _dateServiceTill; }
                    set { _dateServiceTill = value; }
                }
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
                public decimal Charges
                {
                    get { return _charges; }
                    set { _charges = value; }
                }
                public Int16 Unit
                {
                    get { return _unit; }
                    set { _unit = value; }
                }
                public decimal Total
                {
                    get { return _total; }
                    set { _total = value; }
                }
                public decimal AllowedCharges
                {
                    get { return _allowedCharges; }
                    set { _allowedCharges = value; }
                }
                public decimal PaidCharges
                {
                    get { return _paidCharges; }
                    set { _paidCharges = value; }
                }
                public decimal BalanceCharges
                {
                    get { return _balanceCharges; }
                    set { _balanceCharges = value; }
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
                public GeneralNotes LineNotes
                {
                    get { return _generalNotes; }
                    set { _generalNotes = value; }
                }
                public Int64 ClinicID
                {
                    get { return _clinicID; }
                    set { _clinicID = value; }
                }
                public Int64 InsuranceID
                {
                    get { return _InsuranceID; }
                    set { _InsuranceID = value; }
                }
                public string InsuranceName
                {
                    get { return _InsuranceName; }
                    set { _InsuranceName = value; }
                }
                public string InsurancePrimarySecondaryTertiary
                {
                    get { return _InsurancePrmSecTer; }
                    set { _InsurancePrmSecTer = value; }
                }
                public PayerMode InsuranceSelfMode
                {
                    get { return _InsSelfMode; }
                    set { _InsSelfMode = value; }
                }
                public TransactionStatus LineStatus
                {
                    get { return _LineStatus; }
                    set { _LineStatus = value; }
                }
                public Int64 ClaimNumber
                {
                    get { return _ClaimNumber; }
                    set { _ClaimNumber = value; }
                }

                #endregion " Public Property Procedures "

            }

            public class TransactionLines
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public TransactionLines()
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


                ~TransactionLines()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(TransactionLine item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(TransactionLine item)
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

                public TransactionLine this[int index]
                {
                    get
                    { return (TransactionLine)_innerlist[index]; }
                }

                public bool Contains(TransactionLine item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(TransactionLine item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(TransactionLine[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }

            public class GeneralNote
            {
                #region "Constructor & Destructor"

                public GeneralNote()
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

                ~GeneralNote()
                {
                    Dispose(false);
                }

                #endregion

                #region Declaration

                private Int64 _noteID = 0;
                private string _noteDescription = "";
                private NoteType _noteType = NoteType.GeneralNote;
                private Int64 _transactionLineId = 0;
                private Int64 _userID = 0;
                private Int64 _noteDate = 0;
                private Int64 _ClinicID = 0;
                private Int64 _TransactionID = 0;

                #endregion

                #region Properties

                public Int64 NoteID
                {
                    get { return _noteID; }
                    set { _noteID = value; }
                }

                public Int64 TransactionID
                {
                    get { return _TransactionID; }
                    set { _TransactionID = value; }
                }

                public string NoteDescription
                {
                    get { return _noteDescription; }
                    set { _noteDescription = value; }
                }

                public NoteType NoteType
                {
                    get { return _noteType; }
                    set { _noteType = value; }
                }
                public Int64 TransactionLineId
                {
                    get { return _transactionLineId; }
                    set { _transactionLineId = value; }
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

            public class GeneralNotes
            {

                protected ArrayList _innerlist;

                #region "Constructor & Distructor"

                public GeneralNotes()
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

                ~GeneralNotes()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(GeneralNote item)
                {
                    _innerlist.Add(item);
                }

                //Remark - Work Remaining for comparision
                public bool Remove(GeneralNote item)
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

                public GeneralNote this[int index]
                {
                    get
                    { return (GeneralNote)_innerlist[index]; }
                }

                public bool Contains(GeneralNote item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(GeneralNote item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(GeneralNote[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }

            public class TransactionInsurance
            {
                #region "Constructor & Destructor"

                public TransactionInsurance()
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

                ~TransactionInsurance()
                {
                    Dispose(false);
                }

                #endregion

                #region Declaration

                private Int64 _insuranceID = 0;
                private Int64 _ClinicID = 0;
                private Int64 _TransactionID = 0;
                private Int64 _TransactionDetailID = 0;
                private Int64 _TransactionLineNo = 0;
                #endregion

                #region Properties

                public Int64 InsuranceID
                {
                    get { return _insuranceID; }
                    set { _insuranceID = value; }
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

                public Int64 TransactionID
                {
                    get { return _TransactionID; }
                    set { _TransactionID = value; }
                }

                public Int64 ClinicID
                {
                    get { return _ClinicID; }
                    set { _ClinicID = value; }
                }

                #endregion
            }

            public class TransactionInsurances
            {

                protected ArrayList _innerlist;

                #region "Constructor & Destructor"

                public TransactionInsurances()
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

                ~TransactionInsurances()
                {
                    Dispose(false);
                }
                #endregion

                // Methods Add, Remove, Count , Item of TransactionLine
                public int Count
                {
                    get { return _innerlist.Count; }
                }

                public void Add(TransactionInsurance item)
                {
                    _innerlist.Add(item);
                }

                public bool Remove(TransactionInsurance item)
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

                public TransactionInsurance this[int index]
                {
                    get
                    { return (TransactionInsurance)_innerlist[index]; }
                }

                public bool Contains(TransactionInsurance item)
                {
                    return _innerlist.Contains(item);
                }

                public int IndexOf(TransactionInsurance item)
                {
                    return _innerlist.IndexOf(item);
                }

                public void CopyTo(TransactionInsurance[] array, int index)
                {
                    _innerlist.CopyTo(array, index);
                }

            }

        }

        public class gloBilling
        {
            #region "Constructor & Distructor"

            private string _databaseconnectionstring = "";
            private string _emrdatabaseconnectionstring = "";
            private string _messageBoxCaption = String.Empty;

            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            private Int64 _ClinicID = 0;
            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }


            public gloBilling(string DatabaseConnectionString, string EMRDatabaseConnectionString)
            {
                _databaseconnectionstring = DatabaseConnectionString;
                _emrdatabaseconnectionstring = EMRDatabaseConnectionString;
                //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _ClinicID = 0; }
                }
                else
                { _ClinicID = 0; }

                #region " Retrieve MessageBoxCaption from AppSettings "

                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _messageBoxCaption = "gloPM";
                    }
                }
                else
                { _messageBoxCaption = "gloPM"; }

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

            ~gloBilling()
            {
                Dispose(false);
            }

            #endregion

            public Transaction GetTransactionDetails(Int64 TransactionID, Int64 ClinicID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                DataTable dtTrans = new DataTable();
                Transaction oTransaction = new Transaction();
                TransactionLine oLine = null;
                try
                {
                    oDB.Connect(false);
                    // For BL_Transaction_MST Table.
                    oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_SELECT_Transaction_MST", oDBParameters, out dtTrans);

                    if (dtTrans != null)
                    {
                        if (dtTrans.Rows.Count > 0)
                        {
                            //nTransactionID, nMasterAppointmentID, nAppointmentID, nVisitID, nOnsiteDate, nInjuryDate, 
                            //nUnableToWorkFromDate, nUnableToWorkTillDate, nTransactionDate, sCaseNoPrefix, nClaimNo, 
                            //nPatientID, nTransactionProviderID, sMaritalStatus, sFacilityCode, sFacilityDescription, 
                            //nTransactionType, nClinicID, nTransactionStatusID, sState, nHopitalizationDateFrom, nHopitalizationDateTo,
                            //bOutSideLab, dOutSideLabCharges, bAutoClaim, nAccidentDate, bWorkersComp
                            oTransaction.TransactionID = TransactionID;
                            oTransaction.MasterAppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nMasterAppointmentID"]);
                            oTransaction.AppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nAppointmentID"]);
                            oTransaction.VisitID = Convert.ToInt64(dtTrans.Rows[0]["nVisitID"]);
                            oTransaction.OnsiteDate = Convert.ToInt64(dtTrans.Rows[0]["nOnsiteDate"]);
                            oTransaction.InjuryDate = Convert.ToInt64(dtTrans.Rows[0]["nInjuryDate"]);
                            oTransaction.UnableToWorkFromDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkFromDate"]);
                            oTransaction.UnableToWorkTillDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkTillDate"]);
                            oTransaction.TransactionDate = Convert.ToInt64(dtTrans.Rows[0]["nTransactionDate"]);
                            oTransaction.CaseNoPrefix = dtTrans.Rows[0]["sCaseNoPrefix"].ToString();
                            oTransaction.ClaimNo = Convert.ToInt64(dtTrans.Rows[0]["nClaimNo"]);

                            //Vinayak-Sagar for batch no, but we have to implement it in line class, - Remark - Pending
                            oTransaction.BatchNoPrefix = "Batch";
                            oTransaction.BatchNo = 0;

                            #region "Retrive Batch No"

                            DataTable dtBatchNo = new DataTable();
                            string _strSQLBatchNo = "";
                            _strSQLBatchNo = "SELECT  TOP 1   BL_Batch_MST.sBatchNoPrefix, BL_Batch_MST.nBatchNo " +
                            " FROM BL_Batch_DTL INNER JOIN BL_Batch_MST ON BL_Batch_DTL.nBatchID = BL_Batch_MST.nBatchID " +
                            " WHERE (BL_Batch_DTL.nTransactionID = " + TransactionID + ")";
                            oDB.Retrive_Query(_strSQLBatchNo, out dtBatchNo);
                            if (dtBatchNo != null)
                            {
                                if (dtBatchNo.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= dtBatchNo.Rows.Count - 1; i++)
                                    {
                                        if (dtBatchNo.Rows[0]["sBatchNoPrefix"].GetType() != typeof(System.DBNull))
                                        {
                                            oTransaction.BatchNoPrefix = dtBatchNo.Rows[0]["sBatchNoPrefix"].ToString();
                                        }
                                        if (dtBatchNo.Rows[0]["nBatchNo"].GetType() != typeof(System.DBNull))
                                        {
                                            oTransaction.BatchNo = Convert.ToInt64(dtBatchNo.Rows[0]["nBatchNo"].ToString());
                                        }
                                    }
                                }
                                dtBatchNo.Dispose();
                                dtBatchNo = null;
                            }
                            #endregion

                            oTransaction.PatientID = Convert.ToInt64(dtTrans.Rows[0]["nPatientID"]);
                            oTransaction.ProviderID = Convert.ToInt64(dtTrans.Rows[0]["nTransactionProviderID"]);
                            oTransaction.ProviderName = GetProvider(Convert.ToInt64(dtTrans.Rows[0]["nTransactionProviderID"]));
                            oTransaction.MaritalStatus = dtTrans.Rows[0]["sMaritalStatus"].ToString();
                            oTransaction.FacilityCode = dtTrans.Rows[0]["sFacilityCode"].ToString();
                            oTransaction.FacilityDescription = dtTrans.Rows[0]["sFacilityDescription"].ToString();
                            oTransaction.PrefixID = 0; ////This ID is use to generate a unique TransactionID in Stored Procedure.
                            oTransaction.ClinicID = ClinicID;
                            oTransaction.TransactionMode = (TransactionType)Convert.ToInt64(dtTrans.Rows[0]["nTransactionType"]);

                            oTransaction.Transaction_Status = (TransactionStatus)Convert.ToInt32(dtTrans.Rows[0]["nTransactionStatusID"]);
                            oTransaction.State = Convert.ToString(dtTrans.Rows[0]["sState"]);
                            oTransaction.HospitalizationDateFrom = Convert.ToInt64(dtTrans.Rows[0]["nHopitalizationDateFrom"]);
                            oTransaction.HospitalizationDateTo = Convert.ToInt64(dtTrans.Rows[0]["nHopitalizationDateTo"]);
                            oTransaction.OutSideLab = Convert.ToBoolean(dtTrans.Rows[0]["bOutSideLab"]);
                            oTransaction.OutSideLabCharges = Convert.ToDecimal(dtTrans.Rows[0]["dOutSideLabCharges"]);

                            oTransaction.WorkersComp = Convert.ToBoolean(dtTrans.Rows[0]["bWorkersComp"]);
                            oTransaction.AutoClaim = Convert.ToBoolean(dtTrans.Rows[0]["bAutoClaim"]);
                            oTransaction.AccidentDate = Convert.ToInt64(dtTrans.Rows[0]["nAccidentDate"]);

                        }
                        dtTrans.Dispose();
                        dtTrans = null;
                    }
                    

                    //BL_Transaction_MST_Ins
                    DataTable dtInsurance = new DataTable();
                    oTransaction.Insurances = new TransactionInsurances();

                    oDBParameters.Clear();
                    oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_SELECT_Transaction_MST_Ins", oDBParameters, out dtInsurance);

                    //if (dtInsurance != null)
                    //{
                    //    if (dtInsurance.Rows.Count > 0)
                    //    {
                    //        //Addded by Anil 20080912 
                    //        //nTransactionID,nInsuranceID,nClinicID nTransactionDetailID =1,nTransactionLineNo
                    //        for (int j = 0; j < dtInsurance.Rows.Count; j++)
                    //        {
                    //            TransactionInsurance oInsurance = new TransactionInsurance();
                    //            oInsurance.ClinicID = ClinicID;
                    //            oInsurance.InsuranceID = Convert.ToInt64(dtInsurance.Rows[j]["nInsuranceID"]);
                    //            oInsurance.TransactionDetailID = Convert.ToInt64(dtInsurance.Rows[j]["nTransactionDetailID"]);
                    //            oInsurance.TransactionLineNo = Convert.ToInt64(dtInsurance.Rows[j]["nTransactionLineNo"]);
                    //            oInsurance.TransactionID = TransactionID;
                    //            oTransaction.Insurances.Add(oInsurance);
                    //            oInsurance.Dispose();
                    //        }
                    //    }
                    //}

                    //BL_Transaction_Lines
                    DataTable dtLines = new DataTable();
                    oDBParameters.Clear();
                    oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nTransactionLineNo", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_SELECT_Transaction_Lines", oDBParameters, out dtLines);

                    if (dtLines != null)
                    {
                        if (dtLines.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtLines.Rows.Count; i++)
                            {
                                //nTransactionID
                                //nTransactionLineNo nFromDate nToDate sPOSCode sPOSDescription sTOSCode sTOSDescription sCPTCode sCPTDescription sDx1Code 
                                //sDx1Description sDx2Code sDx2Description sDx3Code sDx3Description sDx4Code sDx4Description sDx5Code sDx5Description sDx6Code 
                                //sDx6Description sDx7Code sDx7Description sDx8Code sDx8Description nDx1Pointer nDx2Pointer nDx3Pointer nDx4Pointer 
                                //nDx5Pointer nDx6Pointer nDx7Pointer nDx8Pointer sMod1Code sMod1Description sMod2Code sMod2Description sMod3Code
                                //sMod3Description sMod4Code sMod4Description dCharges dUnit dTotal dAllowed nProvider nClinicID

                                oLine = new TransactionLine();
                                oLine.TransactionId = TransactionID;
                                oLine.TransactionLineId = Convert.ToInt64(dtLines.Rows[i]["nTransactionLineNo"]);
                                oLine.TransactionDetailID = Convert.ToInt64(dtLines.Rows[i]["nTransactionDetailID"]);
                                oLine.DateServiceFrom = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nFromDate"]));
                                oLine.DateServiceTill = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nToDate"]));
                                oLine.POSCode = dtLines.Rows[i]["sPOSCode"].ToString();
                                oLine.POSDescription = dtLines.Rows[i]["sPOSDescription"].ToString();
                                oLine.TOSCode = dtLines.Rows[i]["sTOSCode"].ToString();
                                oLine.TOSDescription = dtLines.Rows[i]["sTOSDescription"].ToString();
                                oLine.CPTCode = dtLines.Rows[i]["sCPTCode"].ToString();
                                oLine.CPTDescription = dtLines.Rows[i]["sCPTDescription"].ToString();
                                oLine.Dx1Code = dtLines.Rows[i]["sDx1Code"].ToString();
                                oLine.Dx1Description = dtLines.Rows[i]["sDx1Description"].ToString();
                                oLine.Dx2Code = dtLines.Rows[i]["sDx2Code"].ToString();
                                oLine.Dx2Description = dtLines.Rows[i]["sDx2Description"].ToString();
                                oLine.Dx3Code = dtLines.Rows[i]["sDx3Code"].ToString();
                                oLine.Dx3Description = dtLines.Rows[i]["sDx3Description"].ToString();
                                oLine.Dx4Code = dtLines.Rows[i]["sDx4Code"].ToString();
                                oLine.Dx4Description = dtLines.Rows[i]["sDx4Description"].ToString();
                                oLine.Dx5Code = dtLines.Rows[i]["sDx5Code"].ToString();
                                oLine.Dx5Description = dtLines.Rows[i]["sDx5Description"].ToString();
                                oLine.Dx6Code = dtLines.Rows[i]["sDx6Code"].ToString();
                                oLine.Dx6Description = dtLines.Rows[i]["sDx6Description"].ToString();
                                oLine.Dx7Code = dtLines.Rows[i]["sDx7Code"].ToString();
                                oLine.Dx7Description = dtLines.Rows[i]["sDx7Description"].ToString();
                                oLine.Dx8Code = dtLines.Rows[i]["sDx8Code"].ToString();
                                oLine.Dx8Description = dtLines.Rows[i]["sDx8Description"].ToString();
                                oLine.Dx1Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx1Pointer"]);
                                oLine.Dx2Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx2Pointer"]);
                                oLine.Dx3Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx3Pointer"]);
                                oLine.Dx4Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx4Pointer"]);
                                oLine.Dx5Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx5Pointer"]);
                                oLine.Dx6Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx6Pointer"]);
                                oLine.Dx7Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx7Pointer"]);
                                oLine.Dx8Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx8Pointer"]);
                                oLine.Mod1Code = dtLines.Rows[i]["sMod1Code"].ToString();
                                oLine.Mod1Description = dtLines.Rows[i]["sMod1Description"].ToString();
                                oLine.Mod2Code = dtLines.Rows[i]["sMod2Code"].ToString();
                                oLine.Mod2Description = dtLines.Rows[i]["sMod2Description"].ToString();
                                oLine.Mod3Code = dtLines.Rows[i]["sMod3Code"].ToString();
                                oLine.Mod3Description = dtLines.Rows[i]["sMod3Description"].ToString();
                                oLine.Mod4Code = dtLines.Rows[i]["sMod4Code"].ToString();
                                oLine.Mod4Description = dtLines.Rows[i]["sMod4Description"].ToString();
                                oLine.Charges = Convert.ToDecimal(dtLines.Rows[i]["dCharges"]);
                                oLine.Unit = Convert.ToInt16(dtLines.Rows[i]["dUnit"]);
                                oLine.Total = Convert.ToDecimal(dtLines.Rows[i]["dTotal"]);
                                oLine.AllowedCharges = Convert.ToDecimal(dtLines.Rows[i]["dAllowed"]);
                                oLine.RefferingProviderId = Convert.ToInt64(dtLines.Rows[i]["nProvider"]);
                                oLine.ClinicID = ClinicID;
                                oLine.ClaimNumber = Convert.ToInt64(dtLines.Rows[i]["nClaimNumber"]);
                                oLine.LineStatus = (TransactionStatus)Convert.ToInt32(dtLines.Rows[i]["nTransactionLineStatus"]);

                                //BL_Transaction_Lines_Notes
                                DataTable dtNotes = new DataTable();
                                oDBParameters.Clear();
                                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nLineNo", Convert.ToInt64(dtLines.Rows[i]["nTransactionLineNo"]), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nNoteId", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDB.Retrive("BL_SELECT_Transaction_Lines_Notes", oDBParameters, out dtNotes);

                                if (dtNotes != null)
                                {
                                    Common.GeneralNote oLineNote = null;
                                    for (int j = 0; j < dtNotes.Rows.Count; j++)
                                    {
                                        //nTransactionID nLineNo nNoteType nNoteId nNoteDateTime nUserID sNoteDescription nClinicID
                                        //oLine.LineNotes[j].TransactionID = TransactionID;
                                        //oLine.LineNotes[j].TransactionLineId = Convert.ToInt64(dtNotes.Rows[j]["nLineNo"]);
                                        //oLine.LineNotes[j].NoteType = (NoteType)(dtNotes.Rows[j]["nNoteType"]);
                                        //oLine.LineNotes[j].NoteDate = Convert.ToInt64(dtNotes.Rows[j]["nNoteDateTime"]);
                                        //oLine.LineNotes[j].UserID = Convert.ToInt64(dtNotes.Rows[j]["nUserID"]);
                                        //oLine.LineNotes[j].NoteDescription = Convert.ToString(dtNotes.Rows[j]["sNoteDescription"]);
                                        //oLine.LineNotes[j].ClinicID = ClinicID;
                                        oLineNote = new GeneralNote();
                                        oLineNote.TransactionID = TransactionID;
                                        oLineNote.TransactionLineId = Convert.ToInt64(dtNotes.Rows[j]["nLineNo"]);
                                        oLineNote.NoteType = (NoteType)(dtNotes.Rows[j]["nNoteType"]);
                                        oLineNote.NoteDate = Convert.ToInt64(dtNotes.Rows[j]["nNoteDateTime"]);
                                        oLineNote.UserID = Convert.ToInt64(dtNotes.Rows[j]["nUserID"]);
                                        oLineNote.NoteDescription = Convert.ToString(dtNotes.Rows[j]["sNoteDescription"]);
                                        oLineNote.ClinicID = ClinicID;
                                        oLine.LineNotes.Add(oLineNote);
                                        if (oLineNote != null)
                                        { oLineNote.Dispose(); }
                                    }
                                    dtNotes.Dispose();
                                    dtNotes = null;
                                }

                                if (dtInsurance != null)
                                {
                                    if (dtInsurance.Rows.Count > 0)
                                    {
                                        //Addded by Anil 20080912 

                                        //nTransactionID,nInsuranceID,nClinicID nTransactionDetailID =1,nTransactionLineNo
                                        for (int j = 0; j < dtInsurance.Rows.Count; j++)
                                        {
                                            if (Convert.ToString(dtInsurance.Rows[j]["nTransactionLineNo"]) != "")
                                            {
                                                if (Convert.ToInt64(dtInsurance.Rows[j]["nTransactionLineNo"]) == oLine.TransactionLineId)
                                                {
                                                    oLine.InsuranceID = Convert.ToInt64(dtInsurance.Rows[j]["nInsuranceID"]);
                                                    oLine.InsuranceSelfMode = (PayerMode)Convert.ToInt32(dtInsurance.Rows[j]["nPaymentMode"]);

                                                    gloPatient.gloInsurance ogloInsurance = new gloPatient.gloInsurance(_databaseconnectionstring);
                                                    DataTable dtTempInsurance = new DataTable();
                                                    dtTempInsurance = ogloInsurance.GetInsurance(oLine.InsuranceID);
                                                    if (dtTempInsurance != null && dtTempInsurance.Rows.Count > 0)
                                                    {
                                                        //Contact
                                                        oLine.InsuranceName = Convert.ToString(dtTempInsurance.Rows[0]["Name"]);
                                                        //Vinayak - Is Primary/secondary/tertiary
                                                        if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Primary.GetHashCode())
                                                        {
                                                            oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Primary.ToString();
                                                        }
                                                        else if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Secondary.GetHashCode())
                                                        {
                                                            oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Secondary.ToString();
                                                        }
                                                        else if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Tertiary.GetHashCode())
                                                        {
                                                            oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Tertiary.ToString();
                                                        }
                                                        else
                                                        {
                                                            oLine.InsurancePrimarySecondaryTertiary = "";
                                                        }

                                                    }
                                                    if (dtTempInsurance != null) { dtTempInsurance.Dispose(); }
                                                    if (ogloInsurance != null) { ogloInsurance.Dispose(); }

                                                }
                                            }
                                        }
                                    }
                                }

                                //Transaction line is added in the Transaction
                                oTransaction.Lines.Add(oLine);
                            }
                        }
                        dtLines.Dispose();
                        dtLines = null;
                    }
                    if (dtInsurance != null)
                    {
                        dtInsurance.Dispose();
                        dtInsurance = null;
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (dtTrans != null)
                    {
                        dtTrans.Dispose();
                        dtTrans = null;
                    }
                    oDBParameters.Dispose();

                    oDB.Dispose();

                }
                return oTransaction;
            }

            public Transaction GetHCFATransactionDetails(Int64 TransactionID, Int64 ClinicID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                DataTable dtTrans = new DataTable();
                Transaction oTransaction = new Transaction();
                TransactionLine oLine = null;
                try
                {
                    oDB.Connect(false);
                    // For BL_Transaction_MST Table.
                    oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_SELECT_HCFA_Transaction", oDBParameters, out dtTrans);

                    if (dtTrans != null)
                    {
                        if (dtTrans.Rows.Count > 0)
                        {
                            //nTransactionID, nMasterAppointmentID, nAppointmentID, nVisitID, nOnsiteDate, nInjuryDate, 
                            //nUnableToWorkFromDate, nUnableToWorkTillDate, nTransactionDate, sCaseNoPrefix, nClaimNo, 
                            //nPatientID, nTransactionProviderID, sMaritalStatus, sFacilityCode, sFacilityDescription, 
                            //nTransactionType, nClinicID, nTransactionStatusID, sState, nHopitalizationDateFrom, nHopitalizationDateTo,
                            //bOutSideLab, dOutSideLabCharges, bAutoClaim, nAccidentDate, bWorkersComp
                            oTransaction.TransactionID = TransactionID;
                            oTransaction.MasterAppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nMasterAppointmentID"]);
                            oTransaction.AppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nAppointmentID"]);
                            oTransaction.VisitID = Convert.ToInt64(dtTrans.Rows[0]["nVisitID"]);
                            oTransaction.OnsiteDate = Convert.ToInt64(dtTrans.Rows[0]["nOnsiteDate"]);
                            oTransaction.InjuryDate = Convert.ToInt64(dtTrans.Rows[0]["nInjuryDate"]);
                            oTransaction.UnableToWorkFromDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkFromDate"]);
                            oTransaction.UnableToWorkTillDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkTillDate"]);
                            oTransaction.TransactionDate = Convert.ToInt64(dtTrans.Rows[0]["nTransactionDate"]);
                            oTransaction.CaseNoPrefix = dtTrans.Rows[0]["sCaseNoPrefix"].ToString();
                            oTransaction.ClaimNo = Convert.ToInt64(dtTrans.Rows[0]["nClaimNo"]);
                            //
                            oTransaction.Transaction_Details.HCFA_PatientFName = Convert.ToString(dtTrans.Rows[0]["PatientFName"]);
                            oTransaction.Transaction_Details.HCFA_PatientMName = Convert.ToString(dtTrans.Rows[0]["PatientMName"]);
                            oTransaction.Transaction_Details.HCFA_PatientLName = Convert.ToString(dtTrans.Rows[0]["PatientLName"]);
                            oTransaction.Transaction_Details.HCFA_PatientCode = Convert.ToString(dtTrans.Rows[0]["PatientCode"]);
                            oTransaction.Transaction_Details.HCFA_PatientSSN = Convert.ToString(dtTrans.Rows[0]["PatientSSN"]);
                            oTransaction.Transaction_Details.HCFA_PatientDOB = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtTrans.Rows[0]["PatientDOB"]));
                            oTransaction.Transaction_Details.HCFA_PatientGender = Convert.ToString(dtTrans.Rows[0]["PatientGender"]);

                            oTransaction.Transaction_Details.HCFA_PatientAddress1 = Convert.ToString(dtTrans.Rows[0]["PatientAddr1"]);
                            oTransaction.Transaction_Details.HCFA_PatientAddress2 = Convert.ToString(dtTrans.Rows[0]["PatientAddr2"]);
                            oTransaction.Transaction_Details.HCFA_PatientCity = Convert.ToString(dtTrans.Rows[0]["PatientCity"]);
                            oTransaction.Transaction_Details.HCFA_PatientState = Convert.ToString(dtTrans.Rows[0]["PatientState"]);
                            oTransaction.Transaction_Details.HCFA_PatientZip = Convert.ToString(dtTrans.Rows[0]["PatientZip"]);
                            oTransaction.Transaction_Details.HCFA_PatientPhone = Convert.ToString(dtTrans.Rows[0]["PatientPhone"]);
                            oTransaction.Transaction_Details.HCFA_PatientEmploymentStatus = Convert.ToString(dtTrans.Rows[0]["PatientEmploymentStatus"]);
                            oTransaction.Transaction_Details.HCFA_PriorAuthorizationNo = Convert.ToString(dtTrans.Rows[0]["PriorAuthorizationNo"]);

                            oTransaction.Transaction_Details.HCFA_FacilityCode = Convert.ToString(dtTrans.Rows[0]["FacilityCode"]);
                            oTransaction.Transaction_Details.HCFA_FacilityName = Convert.ToString(dtTrans.Rows[0]["FacilityDescription"]);
                            oTransaction.Transaction_Details.HCFA_FacilityNPI = Convert.ToString(dtTrans.Rows[0]["FacilityNPI"]);
                            oTransaction.Transaction_Details.HCFA_FacilityAddress1 = Convert.ToString(dtTrans.Rows[0]["FacilityAddr1"]);
                            oTransaction.Transaction_Details.HCFA_FacilityAddress2 = Convert.ToString(dtTrans.Rows[0]["FacilityAddr2"]);
                            oTransaction.Transaction_Details.HCFA_FacilityZip = Convert.ToString(dtTrans.Rows[0]["FacilityZip"]);
                            oTransaction.Transaction_Details.HCFA_FacilityCity = Convert.ToString(dtTrans.Rows[0]["FacilityCity"]);
                            oTransaction.Transaction_Details.HCFA_FacilityState = Convert.ToString(dtTrans.Rows[0]["FacilityState"]);

                            oTransaction.Transaction_Details.HCFA_ProviderFName = Convert.ToString(dtTrans.Rows[0]["ProviderFName"]);
                            oTransaction.Transaction_Details.HCFA_ProviderMName = Convert.ToString(dtTrans.Rows[0]["ProviderMName"]);
                            oTransaction.Transaction_Details.HCFA_ProviderLName = Convert.ToString(dtTrans.Rows[0]["ProviderLName"]);

                            oTransaction.Transaction_Details.HCFA_ProviderAddress1 = Convert.ToString(dtTrans.Rows[0]["ProviderBusAddr1"]);
                            oTransaction.Transaction_Details.HCFA_ProviderAddress2 = Convert.ToString(dtTrans.Rows[0]["ProviderBusAddr2"]);
                            oTransaction.Transaction_Details.HCFA_ProviderCity = Convert.ToString(dtTrans.Rows[0]["ProviderBusCity"]);
                            oTransaction.Transaction_Details.HCFA_ProviderState = Convert.ToString(dtTrans.Rows[0]["ProviderBusState"]);
                            oTransaction.Transaction_Details.HCFA_ProviderZip = Convert.ToString(dtTrans.Rows[0]["ProviderBusZip"]);
                            oTransaction.Transaction_Details.HCFA_ProviderPhone = Convert.ToString(dtTrans.Rows[0]["ProviderBusPhone"]);
                            oTransaction.Transaction_Details.HCFA_ProviderNPI = Convert.ToString(dtTrans.Rows[0]["ProviderNPI"]);
                            oTransaction.Transaction_Details.HCFA_ProviderUPIN = Convert.ToString(dtTrans.Rows[0]["ProviderUPIN"]);
                            oTransaction.Transaction_Details.HCFA_ProviderStateMedicalNo = Convert.ToString(dtTrans.Rows[0]["ProviderStateMedicalNo"]);

                            oTransaction.Transaction_Details.HCFA_ProviderTaxonomy = Convert.ToString(dtTrans.Rows[0]["ProviderTaxonomyCode"]);
                            //oTransaction.Transaction_Details.HCFA_Provider = Convert.ToString(dtTrans.Rows[0]["ProviderTaxonomyDesc"]);
                            oTransaction.Transaction_Details.HCFA_ProviderSSN = Convert.ToString(dtTrans.Rows[0]["ProviderSSN"]);
                            oTransaction.Transaction_Details.HCFA_ProviderEIN = Convert.ToString(dtTrans.Rows[0]["ProviderEmployerID"]);

                            oTransaction.PatientID = Convert.ToInt64(dtTrans.Rows[0]["nPatientID"]);
                            oTransaction.ProviderID = Convert.ToInt64(dtTrans.Rows[0]["nTransactionProviderID"]);
                            oTransaction.ProviderName = GetProvider(Convert.ToInt64(dtTrans.Rows[0]["nTransactionProviderID"]));
                            oTransaction.MaritalStatus = dtTrans.Rows[0]["sMaritalStatus"].ToString();
                            oTransaction.FacilityCode = dtTrans.Rows[0]["sFacilityCode"].ToString();
                            oTransaction.FacilityDescription = dtTrans.Rows[0]["sFacilityDescription"].ToString();
                            oTransaction.PrefixID = 0; ////This ID is use to generate a unique TransactionID in Stored Procedure.
                            oTransaction.ClinicID = ClinicID;
                            oTransaction.TransactionMode = (TransactionType)Convert.ToInt64(dtTrans.Rows[0]["nTransactionType"]);

                            oTransaction.Transaction_Status = (TransactionStatus)Convert.ToInt32(dtTrans.Rows[0]["nTransactionStatusID"]);
                            oTransaction.State = Convert.ToString(dtTrans.Rows[0]["sState"]);
                            oTransaction.HospitalizationDateFrom = Convert.ToInt64(dtTrans.Rows[0]["nHopitalizationDateFrom"]);
                            oTransaction.HospitalizationDateTo = Convert.ToInt64(dtTrans.Rows[0]["nHopitalizationDateTo"]);
                            oTransaction.OutSideLab = Convert.ToBoolean(dtTrans.Rows[0]["bOutSideLab"]);
                            oTransaction.OutSideLabCharges = Convert.ToDecimal(dtTrans.Rows[0]["dOutSideLabCharges"]);

                            oTransaction.WorkersComp = Convert.ToBoolean(dtTrans.Rows[0]["bWorkersComp"]);
                            oTransaction.AutoClaim = Convert.ToBoolean(dtTrans.Rows[0]["bAutoClaim"]);
                            oTransaction.AccidentDate = Convert.ToInt64(dtTrans.Rows[0]["nAccidentDate"]);

                        }
                        dtTrans.Dispose();
                        dtTrans = null;
                    }
                    

                    //BL_Transaction_MST_Ins
                    DataTable dtInsurance = new DataTable();
                    oTransaction.Insurances = new TransactionInsurances();

                    oDBParameters.Clear();
                    oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_SELECT_Transaction_MST_Ins", oDBParameters, out dtInsurance);


                    //BL_Transaction_Lines
                    DataTable dtLines = new DataTable();
                    oDBParameters.Clear();
                    oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nTransactionLineNo", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_SELECT_HCFA_TransactionLine", oDBParameters, out dtLines);

                    if (dtLines != null)
                    {
                        if (dtLines.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtLines.Rows.Count; i++)
                            {
                                //nTransactionID
                                //nTransactionLineNo nFromDate nToDate sPOSCode sPOSDescription sTOSCode sTOSDescription sCPTCode sCPTDescription sDx1Code 
                                //sDx1Description sDx2Code sDx2Description sDx3Code sDx3Description sDx4Code sDx4Description sDx5Code sDx5Description sDx6Code 
                                //sDx6Description sDx7Code sDx7Description sDx8Code sDx8Description nDx1Pointer nDx2Pointer nDx3Pointer nDx4Pointer 
                                //nDx5Pointer nDx6Pointer nDx7Pointer nDx8Pointer sMod1Code sMod1Description sMod2Code sMod2Description sMod3Code
                                //sMod3Description sMod4Code sMod4Description dCharges dUnit dTotal dAllowed nProvider nClinicID

                                oLine = new TransactionLine();
                                oLine.TransactionId = TransactionID;
                                oLine.TransactionLineId = Convert.ToInt64(dtLines.Rows[i]["nTransactionLineNo"]);
                                oLine.TransactionDetailID = Convert.ToInt64(dtLines.Rows[i]["nTransactionDetailID"]);
                                oLine.DateServiceFrom = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nFromDate"]));
                                oLine.DateServiceTill = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nToDate"]));
                                oLine.POSCode = dtLines.Rows[i]["sPOSCode"].ToString();
                                oLine.POSDescription = dtLines.Rows[i]["sPOSDescription"].ToString();
                                oLine.TOSCode = dtLines.Rows[i]["sTOSCode"].ToString();
                                oLine.TOSDescription = dtLines.Rows[i]["sTOSDescription"].ToString();
                                oLine.CPTCode = dtLines.Rows[i]["sCPTCode"].ToString();
                                oLine.CPTDescription = dtLines.Rows[i]["sCPTDescription"].ToString();
                                oLine.Dx1Code = dtLines.Rows[i]["sDx1Code"].ToString();
                                oLine.Dx1Description = dtLines.Rows[i]["sDx1Description"].ToString();
                                oLine.Dx2Code = dtLines.Rows[i]["sDx2Code"].ToString();
                                oLine.Dx2Description = dtLines.Rows[i]["sDx2Description"].ToString();
                                oLine.Dx3Code = dtLines.Rows[i]["sDx3Code"].ToString();
                                oLine.Dx3Description = dtLines.Rows[i]["sDx3Description"].ToString();
                                oLine.Dx4Code = dtLines.Rows[i]["sDx4Code"].ToString();
                                oLine.Dx4Description = dtLines.Rows[i]["sDx4Description"].ToString();
                                oLine.Dx5Code = dtLines.Rows[i]["sDx5Code"].ToString();
                                oLine.Dx5Description = dtLines.Rows[i]["sDx5Description"].ToString();
                                oLine.Dx6Code = dtLines.Rows[i]["sDx6Code"].ToString();
                                oLine.Dx6Description = dtLines.Rows[i]["sDx6Description"].ToString();
                                oLine.Dx7Code = dtLines.Rows[i]["sDx7Code"].ToString();
                                oLine.Dx7Description = dtLines.Rows[i]["sDx7Description"].ToString();
                                oLine.Dx8Code = dtLines.Rows[i]["sDx8Code"].ToString();
                                oLine.Dx8Description = dtLines.Rows[i]["sDx8Description"].ToString();
                                oLine.Dx1Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx1Pointer"]);
                                oLine.Dx2Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx2Pointer"]);
                                oLine.Dx3Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx3Pointer"]);
                                oLine.Dx4Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx4Pointer"]);
                                oLine.Dx5Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx5Pointer"]);
                                oLine.Dx6Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx6Pointer"]);
                                oLine.Dx7Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx7Pointer"]);
                                oLine.Dx8Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx8Pointer"]);
                                oLine.Mod1Code = dtLines.Rows[i]["sMod1Code"].ToString();
                                oLine.Mod1Description = dtLines.Rows[i]["sMod1Description"].ToString();
                                oLine.Mod2Code = dtLines.Rows[i]["sMod2Code"].ToString();
                                oLine.Mod2Description = dtLines.Rows[i]["sMod2Description"].ToString();
                                oLine.Mod3Code = dtLines.Rows[i]["sMod3Code"].ToString();
                                oLine.Mod3Description = dtLines.Rows[i]["sMod3Description"].ToString();
                                oLine.Mod4Code = dtLines.Rows[i]["sMod4Code"].ToString();
                                oLine.Mod4Description = dtLines.Rows[i]["sMod4Description"].ToString();
                                oLine.Charges = Convert.ToDecimal(dtLines.Rows[i]["dCharges"]);
                                oLine.Unit = Convert.ToInt16(dtLines.Rows[i]["dUnit"]);
                                oLine.Total = Convert.ToDecimal(dtLines.Rows[i]["dTotal"]);
                                oLine.AllowedCharges = Convert.ToDecimal(dtLines.Rows[i]["dAllowed"]);
                                oLine.RefferingProviderId = Convert.ToInt64(dtLines.Rows[i]["nProvider"]);
                                oLine.ClinicID = ClinicID;
                                oLine.ClaimNumber = Convert.ToInt64(dtLines.Rows[i]["nClaimNumber"]);
                                oLine.LineStatus = (TransactionStatus)Convert.ToInt32(dtLines.Rows[i]["nTransactionLineStatus"]);



                                oLine.HCFA_RenderingFName = Convert.ToString(dtLines.Rows[i]["RenderingProviderFName"]);
                                oLine.HCFA_RenderingMName = Convert.ToString(dtLines.Rows[i]["RenderingProviderMName"]);
                                oLine.HCFA_RenderingLName = Convert.ToString(dtLines.Rows[i]["RenderingProviderLName"]);
                                oLine.HCFA_RenderingProviderAddress1 = Convert.ToString(dtLines.Rows[i]["RenderringProviderBusAddr1"]);
                                oLine.HCFA_RenderingProviderAddress2 = Convert.ToString(dtLines.Rows[i]["RenderringProviderBusAddr2"]);
                                oLine.HCFA_RenderingProviderCity = Convert.ToString(dtLines.Rows[i]["RenderringProviderCity"]);
                                oLine.HCFA_RenderingProviderState = Convert.ToString(dtLines.Rows[i]["RenderringProviderState"]);
                                oLine.HCFA_RenderingProviderZip = Convert.ToString(dtLines.Rows[i]["RenderringProviderZip"]);
                                oLine.HCFA_RenderingProviderPhone = Convert.ToString(dtLines.Rows[i]["RenderringProviderPhone"]);
                                oLine.HCFA_RenderingProviderMedicalLicenceNo = Convert.ToString(dtLines.Rows[i]["RenderringProviderMedicalLicenceNo"]);
                                oLine.HCFA_RenderingProviderUPIN = Convert.ToString(dtLines.Rows[i]["RenderringProviderUPIN"]);
                                oLine.HCFA_RenderingProviderNPI = Convert.ToString(dtLines.Rows[i]["RenderringProviderNPI"]);
                                oLine.HCFA_RenderingProviderTaxonomy = Convert.ToString(dtLines.Rows[i]["RenderringProviderTaxonomy"]);
                                oLine.HCFA_RenderingProviderSSN = Convert.ToString(dtLines.Rows[i]["RenderringProviderSSN"]);
                                oLine.HCFA_RenderingProviderEIN = Convert.ToString(dtLines.Rows[i]["RenderringProviderEmployerID"]);


                                //BL_Transaction_Lines_Notes
                                DataTable dtNotes = new DataTable();
                                oDBParameters.Clear();
                                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nLineNo", Convert.ToInt64(dtLines.Rows[i]["nTransactionLineNo"]), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nNoteId", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDB.Retrive("BL_SELECT_Transaction_Lines_Notes", oDBParameters, out dtNotes);

                                if (dtNotes != null)
                                {
                                    Common.GeneralNote oLineNote = null;
                                    for (int j = 0; j < dtNotes.Rows.Count; j++)
                                    {
                                        //nTransactionID nLineNo nNoteType nNoteId nNoteDateTime nUserID sNoteDescription nClinicID
                                        //oLine.LineNotes[j].TransactionID = TransactionID;
                                        //oLine.LineNotes[j].TransactionLineId = Convert.ToInt64(dtNotes.Rows[j]["nLineNo"]);
                                        //oLine.LineNotes[j].NoteType = (NoteType)(dtNotes.Rows[j]["nNoteType"]);
                                        //oLine.LineNotes[j].NoteDate = Convert.ToInt64(dtNotes.Rows[j]["nNoteDateTime"]);
                                        //oLine.LineNotes[j].UserID = Convert.ToInt64(dtNotes.Rows[j]["nUserID"]);
                                        //oLine.LineNotes[j].NoteDescription = Convert.ToString(dtNotes.Rows[j]["sNoteDescription"]);
                                        //oLine.LineNotes[j].ClinicID = ClinicID;
                                        oLineNote = new GeneralNote();
                                        oLineNote.TransactionID = TransactionID;
                                        oLineNote.TransactionLineId = Convert.ToInt64(dtNotes.Rows[j]["nLineNo"]);
                                        oLineNote.NoteType = (NoteType)(dtNotes.Rows[j]["nNoteType"]);
                                        oLineNote.NoteDate = Convert.ToInt64(dtNotes.Rows[j]["nNoteDateTime"]);
                                        oLineNote.UserID = Convert.ToInt64(dtNotes.Rows[j]["nUserID"]);
                                        oLineNote.NoteDescription = Convert.ToString(dtNotes.Rows[j]["sNoteDescription"]);
                                        oLineNote.ClinicID = ClinicID;
                                        oLine.LineNotes.Add(oLineNote);
                                        if (oLineNote != null)
                                        { oLineNote.Dispose(); }
                                    }
                                    dtNotes.Dispose();
                                    dtNotes = null;
                                }

                                if (dtInsurance != null)
                                {
                                    if (dtInsurance.Rows.Count > 0)
                                    {
                                        //Addded by Anil 20080912 

                                        //nTransactionID,nInsuranceID,nClinicID nTransactionDetailID =1,nTransactionLineNo
                                        for (int j = 0; j < dtInsurance.Rows.Count; j++)
                                        {
                                            if (Convert.ToString(dtInsurance.Rows[j]["nTransactionLineNo"]) != "")
                                            {
                                                if (Convert.ToInt64(dtInsurance.Rows[j]["nTransactionLineNo"]) == oLine.TransactionLineId)
                                                {
                                                    oLine.InsuranceID = Convert.ToInt64(dtInsurance.Rows[j]["nInsuranceID"]);
                                                    oLine.InsuranceSelfMode = (PayerMode)Convert.ToInt32(dtInsurance.Rows[j]["nPaymentMode"]);

                                                    gloPatient.gloInsurance ogloInsurance = new gloPatient.gloInsurance(_databaseconnectionstring);
                                                    DataTable dtTempInsurance = new DataTable();
                                                    dtTempInsurance = ogloInsurance.GetInsurance(oLine.InsuranceID);
                                                    if (dtTempInsurance != null && dtTempInsurance.Rows.Count > 0)
                                                    {
                                                        //Contact
                                                        oLine.InsuranceName = Convert.ToString(dtTempInsurance.Rows[0]["Name"]);
                                                        //Vinayak - Is Primary/secondary/tertiary
                                                        if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Primary.GetHashCode())
                                                        {
                                                            oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Primary.ToString();
                                                        }
                                                        else if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Secondary.GetHashCode())
                                                        {
                                                            oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Secondary.ToString();
                                                        }
                                                        else if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Tertiary.GetHashCode())
                                                        {
                                                            oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Tertiary.ToString();
                                                        }
                                                        else
                                                        {
                                                            oLine.InsurancePrimarySecondaryTertiary = "";
                                                        }

                                                    }
                                                    if (dtTempInsurance != null) { dtTempInsurance.Dispose(); }
                                                    if (ogloInsurance != null) { ogloInsurance.Dispose(); }

                                                }
                                            }
                                        }
                                    }
                                }

                                //Transaction line is added in the Transaction
                                oTransaction.Lines.Add(oLine);
                            }
                        }
                        dtLines.Dispose();
                        dtLines = null;
                    }
                    if (dtInsurance != null)
                    {
                        dtInsurance.Dispose();
                        dtInsurance = null;
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (dtTrans != null)
                    {
                        dtTrans.Dispose();
                        dtTrans = null;
                    }
                    oDBParameters.Dispose();

                    oDB.Dispose();

                }
                return oTransaction;
            }

            public DataTable GetTransactionIDs(Int64 PatientID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = new DataTable();
                try
                {
                    oDB.Connect(false);
                    string sqlQuery = "SELECT DISTINCT BL_Transaction_MST.nTransactionID FROM BL_Transaction_MST  " +
                                      "WHERE BL_Transaction_MST.nPatientID = " + PatientID + " " +
                                      "ORDER BY BL_Transaction_MST.nTransactionID";
                    oDB.Retrive_Query(sqlQuery, out dt);
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Dispose();
                }
                return dt;
            }

            public DataTable GetBatchIDs()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = new DataTable();

                try
                {
                    oDB.Connect(false);
                    string sqlQuery = "SELECT DISTINCT nBatchID, nBatchDate, nBatchNo, sBatchNoPrefix, nBatchType, nClinicID FROM BL_Batch_MST ORDER BY nBatchDate desc";
                    oDB.Retrive_Query(sqlQuery, out dt);
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                finally
                {
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                }
                return dt;
            }

            public DataTable GetTransactionsDetailIDs(Int64 TransactionID, Int64 ClinicID)
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = new DataTable();
                try
                {
                    oDB.Connect(false);
                    string sqlQuery = " SELECT nTransactionDetailID,nTransactionLineNo FROM " +
                                      " BL_Transaction_Lines WHERE nTransactionID = " + TransactionID + " AND nClinicID = " + ClinicID + " ";
                    oDB.Retrive_Query(sqlQuery, out dt);
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Dispose();
                }
                return dt;
            }

            public string GetPatient(Int64 PatientID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = new DataTable();
                string strPatientName = "";
                try
                {
                    oDB.Connect(false);
                    string sqlQuery = "SELECT (ISNULL(Patient.sFirstName, '') + SPACE(1) +ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '')) AS PatientName FROM Patient  " +
                                      "WHERE nPatientID = " + PatientID;

                    oDB.Retrive_Query(sqlQuery, out dt);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            strPatientName = Convert.ToString(dt.Rows[0][0]);
                        }
                        else
                        {
                            strPatientName = "";
                        }
                        dt.Dispose();
                        dt = null;
                    }
                    else
                    {
                        strPatientName = "";
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Dispose();
                }
                return strPatientName;
            }

            public Int64 GetTransactionsPatientID(Int64 TransactionId, Int64 ClinicId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string _sqlQuery = "";
                Object retValue = new object();
                Int64 _TransactionPatientId = 0;

                try
                {

                    if (TransactionId > 0)
                    {
                        oDB.Connect(false);
                        _sqlQuery = "select  nPatientID  from BL_Transaction_MST where nTransactionID = " + TransactionId + " AND nClinicID = " + ClinicId + " ";
                        retValue = oDB.ExecuteScalar_Query(_sqlQuery);
                        if (retValue != null)
                        {
                            if (retValue != DBNull.Value && Convert.ToInt64(retValue) > 0)
                            {
                                _TransactionPatientId = Convert.ToInt64(retValue);
                            }
                        }
                    }
                }
                catch (gloDatabaseLayer.DBException dbEx)
                {
                    dbEx.ERROR_Log(dbEx.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                    if (retValue != null) { retValue = null; }
                }
                return _TransactionPatientId;
            }

            public string GetProvider(Int64 ProviderID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = new DataTable();
                string strProviderName = "";
                try
                {
                    oDB.Connect(false);
                    string sqlQuery = "SELECT (ISNULL(Provider_MST.sFirstName, '') + SPACE(1) +ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '')) AS ProviderName FROM Provider_MST  " +
                                      "WHERE nProviderID = " + ProviderID;

                    oDB.Retrive_Query(sqlQuery, out dt);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            strProviderName = Convert.ToString(dt.Rows[0][0]);
                        }
                        else
                        {
                            strProviderName = "";
                        }
                        dt.Dispose();
                        dt = null;
                    }
                    else
                    {
                        strProviderName = "";
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Dispose();
                }
                return strProviderName;
            }

            public DataTable GetTransaction(Int64 TransactionID, Int64 ClinicID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                DataTable dtTrans = new DataTable();
                //Transaction oTransaction = new Transaction();
                try
                {
                    oDB.Connect(false);
                    // For BL_Transaction_MST Table.
                    oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_SELECT_Transaction_MST", oDBParameters, out dtTrans);
                    /*
                    if (dtTrans != null)
                    {
                        if (dtTrans.Rows.Count > 0)
                        {
                            // nTransactionID,nMasterAppointmentID,nAppointmentID,nVisitID,nTransactionDate,sCaseNoPrefix,nCaseNo,nPatientID
                            // nTransactionProviderID,sMaritalStatus,sFacilityCode,sFacilityDescription, nTransactionType, nClinicID
                            // Added Later by Anil on 20080912
                            //nOnsiteDate, nInjuryDate, nUnableToWorkFromDate, nUnableToWorkTillDate
                            oTransaction.TransactionID = TransactionID;
                            oTransaction.MasterAppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nMasterAppointmentID"]);
                            oTransaction.AppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nAppointmentID"]);
                            oTransaction.VisitID = Convert.ToInt64(dtTrans.Rows[0]["nVisitID"]);
                            oTransaction.OnsiteDate = Convert.ToInt64(dtTrans.Rows[0]["nOnsiteDate"]);
                            oTransaction.InjuryDate = Convert.ToInt64(dtTrans.Rows[0]["nInjuryDate"]);
                            oTransaction.UnableToWorkFromDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkFromDate"]);
                            oTransaction.UnableToWorkTillDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkTillDate"]);
                            oTransaction.TransactionDate = Convert.ToInt64(dtTrans.Rows[0]["nTransactionDate"]);
                            oTransaction.CaseNoPrefix = dtTrans.Rows[0]["sCaseNoPrefix"].ToString();
                            oTransaction.ClaimNo = Convert.ToInt64(dtTrans.Rows[0]["nClaimNo"]);
                            oTransaction.PatientID = Convert.ToInt64(dtTrans.Rows[0]["nPatientID"]);
                            oTransaction.ProviderID = Convert.ToInt64(dtTrans.Rows[0]["nTransactionProviderID"]);
                            oTransaction.MaritalStatus = dtTrans.Rows[0]["sMaritalStatus"].ToString();
                            oTransaction.FacilityCode = dtTrans.Rows[0]["sFacilityCode"].ToString();
                            oTransaction.FacilityDescription = dtTrans.Rows[0]["sFacilityDescription"].ToString();
                            oTransaction.PrefixID = 0; ////This ID is use to generate a unique TransactionID in Stored Procedure.
                            oTransaction.ClinicID = ClinicID;
                            oTransaction.TransactionMode = (TransactionType)Convert.ToInt64(dtTrans.Rows[0]["nTransactionType"]);

                            oTransaction.Transaction_Status = (TransactionStatus)Convert.ToInt32(dtTrans.Rows[0]["nTransactionStatusID"]);
                            oTransaction.State = Convert.ToString(dtTrans.Rows[0]["sState"]);
                            oTransaction.HospitalizationDateFrom = Convert.ToInt64(dtTrans.Rows[0]["nHopitalizationDateFrom"]);
                            oTransaction.HospitalizationDateTo = Convert.ToInt64(dtTrans.Rows[0]["nHopitalizationDateTo"]);
                            oTransaction.OutSideLab = Convert.ToBoolean(dtTrans.Rows[0]["bOutSideLab"]);
                            oTransaction.OutSideLabCharges = Convert.ToDecimal(dtTrans.Rows[0]["dOutSideLabCharges"]);

                            oTransaction.AutoClaim = Convert.ToBoolean(dtTrans.Rows[0]["bAutoClaim"]);
                            oTransaction.AccidentDate = Convert.ToInt64(dtTrans.Rows[0]["nAccidentDate"]);
                            oTransaction.WorkersComp = Convert.ToBoolean(dtTrans.Rows[0]["bWorkersComp"]);
                        }
                    }
                    */
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Dispose();
                }
                return dtTrans;
            }

            public DataTable GetTransaction(Int64 BatchId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                string _sqlQuery = "";
                DataTable dtTrans = new DataTable();
                //Transaction oTransaction = new Transaction();
                try
                {
                    oDB.Connect(false);
                    _sqlQuery = " SELECT DISTINCT BL_Batch_DTL.nBatchID, BL_Transaction_MST.nTransactionID, BL_Transaction_MST.nMasterAppointmentID, " +
                                " BL_Transaction_MST.nAppointmentID, BL_Transaction_MST.nVisitID, BL_Transaction_MST.nOnsiteDate, " +
                                " BL_Transaction_MST.nInjuryDate, BL_Transaction_MST.nUnableToWorkFromDate, " +
                                " BL_Transaction_MST.nUnableToWorkTillDate, BL_Transaction_MST.nTransactionDate, " +
                                " BL_Transaction_MST.sCaseNoPrefix,BL_Transaction_MST.nClaimNo, BL_Transaction_MST.nPatientID AS PatientID, " +
                                " (ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'') + SPACE(1) + ISNULL(Patient.sLastName,'')) AS PatientName, " +
                                " BL_Transaction_MST.nTransactionProviderID, " +
                                " (ISNULL(Provider_MST.sFirstName,'')+ SPACE(1) + ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'')) AS TransactionProviderName , " +
                                " BL_Transaction_MST.sFacilityCode, BL_Transaction_MST.sFacilityDescription, " +
                                " BL_Transaction_MST.nTransactionType, BL_Transaction_MST.nClinicID, " +
                                " ISNULL(BL_Transaction_MST.nTransactionStatusID,0) AS nTransactionStatusID, " +
                                " ISNULL(BL_Transaction_MST.sState,'') AS sState, " +
                                " ISNULL(BL_Transaction_MST.nHopitalizationDateFrom,0) AS nHopitalizationDateFrom, " +
                                " ISNULL(BL_Transaction_MST.nHopitalizationDateTo,0) AS nHopitalizationDateTo, " +
                                " ISNULL(BL_Transaction_MST.bOutSideLab,0) AS bOutSideLab, " +
                                " ISNULL(BL_Transaction_MST.dOutSideLabCharges,0) AS dOutSideLabCharges, " +
                                " ISNULL(bAutoClaim,0) AS bAutoClaim, " +
                                " ISNULL(nAccidentDate,0) AS nAccidentDate, " +
                                " ISNULL(bWorkersComp,0) AS bWorkersComp " +
                                " FROM BL_Batch_DTL LEFT OUTER JOIN BL_Transaction_MST " +
                                " ON BL_Batch_DTL.nTransactionID = BL_Transaction_MST.nTransactionID LEFT OUTER JOIN " +
                                " Provider_MST ON BL_Transaction_MST.nTransactionProviderID = Provider_MST.nProviderID LEFT OUTER JOIN " +
                                " Patient ON BL_Transaction_MST.nPatientID = Patient.nPatientID " +
                                " WHERE BL_Batch_DTL.nBatchID = " + BatchId + " ";

                    oDB.Retrive_Query(_sqlQuery, out dtTrans);
                    /*
                    if (dtTrans != null)
                    {
                        if (dtTrans.Rows.Count > 0)
                        {
                            // nTransactionID,nMasterAppointmentID,nAppointmentID,nVisitID,nTransactionDate,sCaseNoPrefix,nCaseNo,nPatientID
                            // nTransactionProviderID,sMaritalStatus,sFacilityCode,sFacilityDescription, nTransactionType, nClinicID
                            // Added Later by Anil on 20080912
                            //nOnsiteDate, nInjuryDate, nUnableToWorkFromDate, nUnableToWorkTillDate
                            oTransaction.TransactionID = Convert.ToInt64(dtTrans.Rows[0]["nTransactionID"]);
                            oTransaction.MasterAppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nMasterAppointmentID"]);
                            oTransaction.AppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nAppointmentID"]);
                            oTransaction.VisitID = Convert.ToInt64(dtTrans.Rows[0]["nVisitID"]);
                            oTransaction.OnsiteDate = Convert.ToInt64(dtTrans.Rows[0]["nOnsiteDate"]);
                            oTransaction.InjuryDate = Convert.ToInt64(dtTrans.Rows[0]["nInjuryDate"]);
                            oTransaction.UnableToWorkFromDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkFromDate"]);
                            oTransaction.UnableToWorkTillDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkTillDate"]);
                            oTransaction.TransactionDate = Convert.ToInt64(dtTrans.Rows[0]["nTransactionDate"]);
                            oTransaction.CaseNoPrefix = dtTrans.Rows[0]["sCaseNoPrefix"].ToString();
                            oTransaction.ClaimNo = Convert.ToInt64(dtTrans.Rows[0]["nClaimNo"]);
                            oTransaction.PatientID = Convert.ToInt64(dtTrans.Rows[0]["PatientID"]);
                            oTransaction.ProviderID = Convert.ToInt64(dtTrans.Rows[0]["nTransactionProviderID"]);
                            //oTransaction.MaritalStatus = dtTrans.Rows[0]["sMaritalStatus"].ToString();
                            oTransaction.FacilityCode = dtTrans.Rows[0]["sFacilityCode"].ToString();
                            oTransaction.FacilityDescription = dtTrans.Rows[0]["sFacilityDescription"].ToString();
                            oTransaction.PrefixID = 0; ////This ID is use to generate a unique TransactionID in Stored Procedure.
                            oTransaction.ClinicID = ClinicID;
                            oTransaction.TransactionMode = (TransactionType)Convert.ToInt64(dtTrans.Rows[0]["nTransactionType"]);
                            oTransaction.State = Convert.ToString(dtTrans.Rows[0]["sState"]);
                        }
                    }
                    */
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Dispose();
                }
                return dtTrans;
            }

            public DataTable GetTransaction(DateTime ClaimDate)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                string _sqlQuery = "";
                DataTable dtTrans = new DataTable();
                //Transaction oTransaction = new Transaction();
                Int64 _ClaimDate = 0;
                try
                {
                    oDB.Connect(false);
                    _ClaimDate = gloDateMaster.gloDate.DateAsNumber(ClaimDate.ToShortDateString());

                    if (_ClaimDate != 0)
                    {
                        _sqlQuery = " SELECT DISTINCT " +
                                " BL_Transaction_MST.nTransactionID, BL_Transaction_MST.nMasterAppointmentID, BL_Transaction_MST.nAppointmentID,  " +
                                " BL_Transaction_MST.nVisitID, BL_Transaction_MST.nOnsiteDate, BL_Transaction_MST.nInjuryDate, BL_Transaction_MST.nUnableToWorkFromDate,  " +
                                " BL_Transaction_MST.nUnableToWorkTillDate, BL_Transaction_MST.nTransactionDate, BL_Transaction_MST.sCaseNoPrefix,  " +
                                " BL_Transaction_MST.nClaimNo, BL_Transaction_MST.nPatientID AS PatientID, ISNULL(Patient.sFirstName, '') + SPACE(1)  " +
                                " + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') AS PatientName, BL_Transaction_MST.nTransactionProviderID,  " +
                                " ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '')  " +
                                " AS TransactionProviderName, BL_Transaction_MST.sFacilityCode, BL_Transaction_MST.sFacilityDescription, BL_Transaction_MST.nTransactionType,  " +
                                " BL_Transaction_MST.nClinicID, ISNULL(BL_Transaction_MST.nTransactionStatusID, 0) AS nTransactionStatusID, ISNULL(BL_Transaction_MST.sState,  " +
                                " '') AS sState, ISNULL(BL_Transaction_MST.nHopitalizationDateFrom, 0) AS nHopitalizationDateFrom,  " +
                                " ISNULL(BL_Transaction_MST.nHopitalizationDateTo, 0) AS nHopitalizationDateTo, ISNULL(BL_Transaction_MST.bOutSideLab, 0) AS bOutSideLab,  " +
                                " ISNULL(BL_Transaction_MST.dOutSideLabCharges, 0) AS dOutSideLabCharges, ISNULL(BL_Transaction_MST.bAutoClaim, 0) AS bAutoClaim,  " +
                                " ISNULL(BL_Transaction_MST.nAccidentDate, 0) AS nAccidentDate, ISNULL(BL_Transaction_MST.bWorkersComp, 0) AS bWorkersComp,  " +
                                " BL_Transaction_MST.nTransactionStatusID AS nTransactionStatusID " +
                                " FROM         BL_Transaction_MST LEFT OUTER JOIN " +
                                " Provider_MST ON BL_Transaction_MST.nTransactionProviderID = Provider_MST.nProviderID LEFT OUTER JOIN " +
                                " Patient ON BL_Transaction_MST.nPatientID = Patient.nPatientID " +
                                " WHERE     (BL_Transaction_MST.nTransactionDate = " + _ClaimDate + ") ";

                        oDB.Retrive_Query(_sqlQuery, out dtTrans);
                    }


                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Dispose();
                }
                return dtTrans;
            }

            private Int64 GetPrefixTransactionID(Int64 PatientID)
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

                object _internalresult = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string _strSQL = "";
                try
                {
                    oDB.Connect(false);
                    _strSQL = "SELECT dtDOB FROM Patient WHERE nPatientID = " + PatientID + "";
                    _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                    if (_internalresult != null)
                    {
                        if (_internalresult.ToString() != null)
                        {
                            if (_internalresult.GetType() != typeof(System.DBNull))
                            {
                                if (_internalresult.ToString() != "")
                                {
                                    _PatientDOB = Convert.ToDateTime(_internalresult);
                                }
                            }
                        }
                    }
                    oDB.Disconnect();

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
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return 0;
                }
                finally
                {
                    _internalresult = null;
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _Result;
            }

            private void AddNote(Common.GeneralNotes oNotes)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                Common.GeneralNote oNote = null;

                try
                {
                    oDB.Connect(false);

                    if (oNote != null)
                    {
                        for (int i = 0; i < oNotes.Count; i++)
                        {
                            oNote = oNotes[i];

                            oDBParameters.Clear();
                            oDBParameters.Add("@nTransactionID", oNote.TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nLineNo", oNote.TransactionLineId, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nNoteType", oNote.NoteType, ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@nNoteId", oNote.NoteID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            oDBParameters.Add("@nNoteDateTime", oNote.NoteDate, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nUserID", oNote.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@sNoteDescription", oNote.NoteDescription, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@nClinicID", oNote.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                            oDB.Execute("BL_INUP_Transaction_Lines_Notes", oDBParameters);

                        }
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oDB.Connect(false))
                    { oDB.Disconnect(); }
                    if (oDB != null)
                    { oDB.Dispose(); }
                }
            }

            public DataTable GetPatientInsurances(Int64 PatientID)
            {
                System.Data.DataTable _result = new System.Data.DataTable();

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    string _sqlQuery = "SELECT Contacts_MST.nContactID, Isnull(Contacts_MST.sName,'') as InsuranceName, Isnull(Contacts_MST.sContact,'') as Contact, Isnull(Contacts_MST.sStreet,'') as Street, Contacts_MST.nSpecialtyID as nSpecialtyID, Contacts_MST.nInsuranceID as nInsuranceID, Isnull(Contacts_MST.sHospitalAffiliation,'') as HospitalAffiliation, Isnull(Contacts_MST.sContactType,'') as ContactType, Isnull(Contacts_MST.sExternalCode,'') as ExternalCode, Isnull(Contacts_MST.sDegree,'') as Degree, Contacts_MST.nClinicID, Contacts_MST.bIsBlocked, Isnull(Contacts_MST.sCity,'') as City, Isnull(Contacts_MST.sState,'') as State, Isnull(Contacts_MST.sZIP,'') as Zip, Isnull(Contacts_MST.sPhone,'') as Phone, Isnull(Contacts_MST.sFax,'') as Fax, Isnull(Contacts_MST.sMobile,'') as Mobile, Isnull(Contacts_MST.sPager,'') as Pager, Isnull(Contacts_MST.sEmail,'')as Email, Isnull(Contacts_MST.sURL,'')as URL, Isnull(Contacts_MST.sNotes,'') as Notes FROM PatientInsurance_DTL INNER JOIN Contacts_MST ON PatientInsurance_DTL.nInsuranceID = Contacts_MST.nContactID where bIsBlocked= 0 and sContactType='Insurance' And PatientInsurance_DTL.nPatientID='" + PatientID + "'";
                    oDB.Retrive_Query(_sqlQuery, out _result);
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                    DBErr = null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

            public DataTable GetPOS()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dtPOS = new DataTable();
                string strQuery = "";
                try
                {
                    oDB.Connect(false);


                    //strQuery = "select nPOSID,sPOSCode,sPOSName,sPOSDescription from BL_POS_MST where nClinicID="+_nClinicID+" ";
                    if (_ClinicID == 0)
                        strQuery = "select nPOSID,sPOSCode + '-' + sPOSName AS sPOSCode,sPOSDescription from BL_POS_MST where bIsBlocked='" + false + "' ORDER BY sPOSCode";
                    else
                        strQuery = "select nPOSID,sPOSCode + '-' + sPOSName AS sPOSCode,sPOSDescription from BL_POS_MST where bIsBlocked='" + false + "' AND nClinicID=" + _ClinicID + " ORDER BY sPOSCode";

                    oDB.Retrive_Query(strQuery, out dtPOS);
                    return dtPOS;

                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); 
                    return null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    dtPOS.Dispose();

                }
            }

            public DataTable GetTOS()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dtTOS = new DataTable();
                string strQuery = "";
                try
                {
                    oDB.Connect(false);

                    if (_ClinicID == 0)
                        strQuery = "select nTOSID,sDescription,sTOSCode from BL_TOS_MST where bIsBlocked = '" + false + "' ORDER BY sDescription";
                    else
                        strQuery = "select nTOSID,sDescription,sTOSCode from BL_TOS_MST where bIsBlocked = '" + false + "' AND nClinicID = " + _ClinicID + " ORDER BY sDescription";

                    oDB.Retrive_Query(strQuery, out dtTOS);
                    return dtTOS;

                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); 
                    return null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    dtTOS.Dispose();

                }
            }

            public DataTable GetProviderSettings(long ProviderID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dt = new DataTable();
                string strQuery = "";
                try
                {
                    oDB.Connect(false);

                    strQuery = "SELECT  sName, sValue, nProviderID FROM ProviderSettings WHERE  nProviderID = " + ProviderID + " AND nClinicID = " + _ClinicID;

                    oDB.Retrive_Query(strQuery, out dt);
                    return dt;

                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); 
                    return null;
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    dt.Dispose();

                }
            }

            public void AddSettings(long ProviderID, string SettingName, string SettingValue, long UserID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                try
                {
                    oDB.Connect(false);

                    oDBParameters.Add("@SettingName", SettingName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@SettingValue", SettingValue, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@SettingFlag", false, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@UserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Execute("gsp_InUpProviderSettings", oDBParameters);

                }
                catch (gloDatabaseLayer.DBException dbex)
                {
                    dbex.ERROR_Log(dbex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDBParameters.Dispose();
                    oDB.Dispose();
                }
            }

            public bool AssociatePatient(Int64 gloPMPatientID, Int64 gloEMRPatientID, string gloEMRPatientCode)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string _sqlQuery = "";
                int _result = 0;
                bool _Return = false;
                try
                {
                    if (gloPMPatientID > 0 && gloEMRPatientCode != "" && gloEMRPatientID > 0)
                    {
                        oDB.Connect(false);
                        _sqlQuery = "UPDATE Patient SET sExternalCode = '" + gloEMRPatientCode + "' WHERE nPatientID = " + gloPMPatientID + " ";
                        _result = oDB.Execute_Query(_sqlQuery);
                        if (_result > 0)
                        {
                            _Return = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    _Return = false;
                }
                finally
                { }
                return _Return;
            }

            public Int64 GetEMRPatientID(string EMRPatientCode, string EMRDatabaseConnectionString)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(EMRDatabaseConnectionString);
                Object _Result;
                string _sqlQuery = "";

                try
                {
                    oDB.Connect(false);
                    _sqlQuery = "SELECT ISNULL(Patient.nPatientID,0) AS PatientID FROM Patient WHERE UPPER(sPatientCode) = '" + EMRPatientCode.ToUpper() + "'";
                    _Result = oDB.ExecuteScalar_Query(_sqlQuery);
                    return Convert.ToInt64(_Result);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return 0;
                }
                finally
                {
                    if (oDB.Connect(false))
                    { oDB.Disconnect(); }

                    if (oDB != null)
                    { oDB.Dispose(); }
                }
            }

            public DataTable GetSubmitterInfo(Int64 ClinicID, Int64 _providerID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                string _strSQL = "";
                Object _objResult = null;
                DataTable dt = null;
                string strSetting = "";
                try
                {
                    oSettings.GetSetting("SubmitterSetting", _providerID, ClinicID, out _objResult);
                    if (_objResult != null)
                    {
                        // |Company|Practice|Business|Clinic"
                        strSetting = Convert.ToString(_objResult);
                    }

                    switch (strSetting)
                    {
                        case "Company":
                            {
                                _strSQL = " SELECT  sCompanyName AS SubmitterName, sCompanyAddressline1 AS SubmitterAddress1, sCompanyAddressline2 AS SubmitterAddress2, sCompanyCity AS SubmitterCity, sCompanyState AS SubmitterState, " +
                                          " sCompanyZIP AS SubmitterZip, sCompanyPhone AS SubmitterPhone, sCompanyContactName AS SubmitterContactName, sCompanyFax AS SubmitterFax " +
                                          " FROM  Provider_MST  " +
                                          " WHERE (nProviderID = " + _providerID + ") AND (nClinicID =" + ClinicID + ") ";
                            }
                            break;
                        case "Practice":
                            {
                                _strSQL = " SELECT  ISNULL(sLastName ,'')+' '+ ISNULL(sFirstName,'')+ ' '+ ISNULL(sMiddleName) AS SubmitterName, sPracticeAddressline1 AS SubmitterAddress1, sPracticeAddressline2 AS SubmitterAddress2, " +
                                           "  sPracticeCity AS SubmitterCity, sPracticeState AS SubmitterState, sPracticeZIP AS SubmitterZip, sPracPhoneNo AS SubmitterPhone, sPracFAX AS SubmitterFAX, " +
                                           " sCompanyContactName AS SubmitterContactName" +
                                           " FROM  Provider_MST  " +
                                           " WHERE (nProviderID = " + _providerID + ") AND (nClinicID =" + ClinicID + ") ";
                            }
                            break;
                        case "Business":
                            {
                                _strSQL = " SELECT  (ISNULL(sLastName ,'')+' '+ ISNULL(sFirstName,'')+ ' '+ ISNULL(sMiddleName,'') )AS SubmitterName,  sBusinessAddressline1 AS SubmitterAddress1, sBusinessAddressline2 AS SubmitterAddress2, sBusinessCity AS SubmitterCity, sBusinessState AS SubmitterState, sBusinessZIP AS SubmitterZip, " +
                                            " sBusPhoneNo AS SubmitterPhone, sBusFAX AS SubmitterFAX, " +
                                            " sCompanyContactName AS SubmitterContactName " +
                                            " FROM  Provider_MST  " +
                                            " WHERE (nProviderID = " + _providerID + ") AND (nClinicID =" + ClinicID + ") ";
                            }
                            break;
                        case "Clinic":
                            {
                                _strSQL = " SELECT sClinicName AS SubmitterName,sAddress1 AS SubmitterAddress1,sAddress2 AS SubmitterAddress2,sStreet AS SubmitterStreet,sCity AS SubmitterCity,sState AS SubmitterState,sZIP AS SubmitterZIP,sPhoneNo ,sMobileNo AS SubmitterMobile, " +
                                          " sFAX ,sTAXID AS SubmitterTAXID,sContactPersonName AS SubmitterContactName, sContactPersonAddress1,sContactPersonAddress2,sContactPersonPhone AS SubmitterPhone, " +
                                          " sContactPersonFAX AS SubmitterFAX,sContactPersonMobile FROM Clinic_MST WHERE nClinicID =" + ClinicID + " ";
                            }
                            break;
                        default:
                            _strSQL = " SELECT sClinicName AS SubmitterName,sAddress1 AS SubmitterAddress1,sAddress2 AS SubmitterAddress2,sStreet AS SubmitterStreet,sCity AS SubmitterCity,sState AS SubmitterState,sZIP AS SubmitterZIP,sPhoneNo ,sMobileNo AS SubmitterMobile, " +
                                          " sFAX ,sTAXID AS SubmitterTAXID,sContactPersonName AS SubmitterContactName, sContactPersonAddress1,sContactPersonAddress2,sContactPersonPhone AS SubmitterPhone, " +
                                          " sContactPersonFAX AS SubmitterFAX,sContactPersonMobile FROM Clinic_MST WHERE nClinicID =" + ClinicID + " ";
                            break;
                    }
                    if (_strSQL != "")
                    {
                        oDB.Connect(false);

                        oDB.Retrive_Query(_strSQL, out dt);
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oSettings.Dispose();
                    oSettings = null;
                }
                return dt;
            }

            #region "Need to Delete - Not in Use"
            public DataTable GetFacilityInfo(Int64 FacilityID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string _strSQL = "";
                DataTable dt = new DataTable();
                try
                {
                    _strSQL = "Select * FROM BL_Facility_MST WHERE nFacilityID=" + FacilityID + "";
                    oDB.Connect(false);
                    oDB.Retrive_Query(_strSQL, out dt);
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return dt;
            }
            #endregion

            public DataTable GetClearingHouseSettings()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string _strSQL = "";
                DataTable dt = new DataTable();
                try
                {
                    _strSQL = " SELECT nClearingHouseID, sClearingHouseCode, sReceiverID, sReceiverName, sSubmitterID, bIsOneJQulifier, sOneJQulifier, bIsSenderCode, sSenderCode,  " +
                              " bIsVenderIDCode, sVenderIDCode, bIsLoop1000BNM109, sLoop1000BNM109, nTypeOfData, bIsISA, nClinicID " +
                              " FROM BL_ClearingHouse_MST WHERE nClinicID=" + ClinicID + "";
                    oDB.Connect(false);
                    oDB.Retrive_Query(_strSQL, out dt);
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return dt;
            }

            public DataTable GetFacilityInfo(string FacilityCode, Int64 _ProviderID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string _strSQL = "";
                DataTable dt = new DataTable();
                try
                {
                    if (FacilityCode == "11")
                    {
                        _strSQL = " SELECT  ISNULL(sLastName ,'')+' '+ ISNULL(sFirstName,'')+ ' '+ ISNULL(sMiddleName) AS FacilityName,  sBusinessAddressline1 AS FacilityAddress1, sBusinessAddressline2 AS FacilityAddress2, sBusinessCity AS FacilityCity, sBusinessState AS FacilityState, sBusinessZIP AS FacilityZip, " +
                                            " sBusPhoneNo AS FacilityPhone, sBusFAX AS FacilityFAX, sNPI AS FacilityNPI " +
                                            " FROM  Provider_MST  " +
                                            " WHERE (nProviderID = " + _ProviderID + ") AND (nClinicID =" + ClinicID + ") ";
                    }
                    else
                    {
                        _strSQL = " SELECT nFacilityID, sFacilityName AS FacilityName, sNPI AS FacilityNPI, sMedicadID, sBlueShieldID, sMedicareID, nPOSID, sAddress1 AS FacilityAddress1,sAddress2 AS FacilityAddress2, sZip FacilityZIP, sCity AS FacilityCity, sState AS FacilityState, sPhone AS FacilityPhone, sFax AS FacilityFax, " +
                                  " nClinicID , bIsBlocked " +
                                  " FROM BL_Facility_MST WHERE UPPER(sFacilityCode)='" + FacilityCode.ToUpper() + "'";
                    }

                    oDB.Connect(false);
                    oDB.Retrive_Query(_strSQL, out dt);
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return dt;
            }

            #region "Need to delete - Not in Use"
            public DataTable GetFacilities()
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string _strSQL = "";
                DataTable dt = new DataTable();
                try
                {
                    _strSQL = "Select * FROM BL_Facility_MST";
                    oDB.Connect(false);
                    oDB.Retrive_Query(_strSQL, out dt);
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return dt;
            }
            #endregion

            public DataTable GetPatientBalance(Int64 PatientID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strSQL = "";
                DataTable dtBalance = new DataTable();
                try
                {
                    oDB.Connect(false);

                    strSQL = "SELECT BL_Transaction_Lines.nTransactionID, BL_Transaction_Lines.nTransactionDetailID, BL_Transaction_Lines.nTransactionLineNo, "
                            + " BL_Transaction_Lines.nFromDate, BL_Transaction_Lines.nToDate, BL_Transaction_Lines.sPOSCode, BL_Transaction_Lines.sPOSDescription, "
                            + " BL_Transaction_Lines.sTOSCode, BL_Transaction_Lines.sTOSDescription, BL_Transaction_Lines.sCPTCode, BL_Transaction_Lines.sCPTDescription, "
                            + " BL_Transaction_Lines.sDx1Code, BL_Transaction_Lines.sDx1Description, BL_Transaction_Lines.sDx2Code, BL_Transaction_Lines.sDx2Description, "
                            + " BL_Transaction_Lines.sDx3Code, BL_Transaction_Lines.sDx3Description, BL_Transaction_Lines.sDx4Code, BL_Transaction_Lines.sDx4Description, "
                            + " BL_Transaction_Lines.sDx5Code, BL_Transaction_Lines.sDx5Description, BL_Transaction_Lines.sDx6Code, BL_Transaction_Lines.sDx6Description, "
                            + " BL_Transaction_Lines.sDx7Code, BL_Transaction_Lines.sDx7Description, BL_Transaction_Lines.sDx8Code, BL_Transaction_Lines.sDx8Description, "
                            + " BL_Transaction_Lines.sMod1Code, BL_Transaction_Lines.sMod1Description, BL_Transaction_Lines.sMod2Code,   BL_Transaction_Lines.sMod2Description, "
                            + " BL_Transaction_Lines.sMod3Code, BL_Transaction_Lines.sMod3Description,  BL_Transaction_Lines.sMod4Code, BL_Transaction_Lines.sMod4Description, "
                            + " BL_Transaction_Lines.dCharges, BL_Transaction_Lines.dTotal, BL_Transaction_Lines.dAllowed, BL_Transaction_Lines.nClinicID, BL_Transaction_MST.nPatientID, "
                            + " BL_Transaction_MST.nClaimNo, BL_Transaction_Lines.dAllowed - (SELECT SUM(dPaymentAmt) AS PaymentAmount FROM   BL_Transaction_Payment_DTL "
                            + " WHERE  (nClinicID = BL_Transaction_Lines.nClinicID) AND (nPatientID = BL_Transaction_MST.nPatientID) AND (nTransactionID = BL_Transaction_Lines.nTransactionID) "
                            + " AND (nTransactionDetailID = BL_Transaction_Lines.nTransactionDetailID)) AS BalanceAmount "
                            + " FROM BL_Transaction_Lines INNER JOIN  BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID  "
                            + " WHERE (BL_Transaction_Lines.dAllowed - (SELECT SUM(dPaymentAmt) AS PaymentAmount  FROM  BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1  "
                            + " WHERE (nClinicID = BL_Transaction_Lines.nClinicID) AND (nPatientID = BL_Transaction_MST.nPatientID) AND (nTransactionID = BL_Transaction_Lines.nTransactionID) "
                            + " AND (nTransactionDetailID = BL_Transaction_Lines.nTransactionDetailID)) > 0) AND (BL_Transaction_MST.nPatientID = " + PatientID + ") AND (BL_Transaction_Lines.nClinicID = " + _ClinicID + ") "
                            + " UNION "
                            + " SELECT BL_Transaction_Lines.nTransactionID, BL_Transaction_Lines.nTransactionDetailID, BL_Transaction_Lines.nTransactionLineNo, "
                            + " BL_Transaction_Lines.nFromDate, BL_Transaction_Lines.nToDate, BL_Transaction_Lines.sPOSCode, BL_Transaction_Lines.sPOSDescription, "
                            + " BL_Transaction_Lines.sTOSCode, BL_Transaction_Lines.sTOSDescription, BL_Transaction_Lines.sCPTCode, BL_Transaction_Lines.sCPTDescription,   "
                            + " BL_Transaction_Lines.sDx1Code, BL_Transaction_Lines.sDx1Description, BL_Transaction_Lines.sDx2Code, BL_Transaction_Lines.sDx2Description,   "
                            + " BL_Transaction_Lines.sDx3Code, BL_Transaction_Lines.sDx3Description, BL_Transaction_Lines.sDx4Code, BL_Transaction_Lines.sDx4Description,   "
                            + " BL_Transaction_Lines.sDx5Code, BL_Transaction_Lines.sDx5Description, BL_Transaction_Lines.sDx6Code, BL_Transaction_Lines.sDx6Description,   "
                            + " BL_Transaction_Lines.sDx7Code, BL_Transaction_Lines.sDx7Description, BL_Transaction_Lines.sDx8Code, BL_Transaction_Lines.sDx8Description,   "
                            + " BL_Transaction_Lines.sMod1Code, BL_Transaction_Lines.sMod1Description, BL_Transaction_Lines.sMod2Code,   BL_Transaction_Lines.sMod2Description, "
                            + " BL_Transaction_Lines.sMod3Code, BL_Transaction_Lines.sMod3Description,  BL_Transaction_Lines.sMod4Code, BL_Transaction_Lines.sMod4Description, "
                            + " BL_Transaction_Lines.dCharges, BL_Transaction_Lines.dTotal, BL_Transaction_Lines.dAllowed, BL_Transaction_Lines.nClinicID, BL_Transaction_MST.nPatientID, "
                            + " BL_Transaction_MST.nClaimNo, BL_Transaction_Lines.dAllowed AS BalanceAmount  "
                            + " FROM BL_Transaction_Lines INNER JOIN  BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID  "
                            + " WHERE (BL_Transaction_MST.nTransactionID  NOT IN (SELECT nTransactionID FROM BL_Transaction_Payment_DTL WHERE nPatientID = " + PatientID + " AND BL_Transaction_Lines.nClinicID = " + _ClinicID + ") "
                            + " AND BL_Transaction_MST.nPatientID = " + PatientID + " AND BL_Transaction_Lines.nClinicID = " + _ClinicID + ") "
                            + " ORDER BY BL_Transaction_Lines.nFromDate DESC ";

                    oDB.Retrive_Query(strSQL, out dtBalance);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return null;
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                    }
                }
                return dtBalance;
            }

            public Decimal GetPatientTotalBalance(Int64 PatientID)
            {
                DataTable dtBalance = new DataTable();
                Decimal _dBalance = 0;
                try
                {
                    dtBalance = GetPatientBalance(PatientID);

                    if (dtBalance != null)
                    {
                        for (int i = 0; i < dtBalance.Rows.Count; i++)
                        {
                            _dBalance += Convert.ToDecimal(dtBalance.Rows[i]["BalanceAmount"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                return _dBalance;
            }

            public DataTable GetPatientLager(Int64 PatientID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string strSQL = "";
                DataTable dtBalance = new DataTable();
                try
                {
                    oDB.Connect(false);

                    strSQL = "SELECT BL_Transaction_Lines.nTransactionID, BL_Transaction_Lines.nTransactionDetailID, BL_Transaction_Lines.nTransactionLineNo, "
                            + " BL_Transaction_Lines.nFromDate, BL_Transaction_Lines.nToDate, BL_Transaction_Lines.sPOSCode, BL_Transaction_Lines.sPOSDescription, "
                            + " BL_Transaction_Lines.sTOSCode, BL_Transaction_Lines.sTOSDescription, BL_Transaction_Lines.sCPTCode, BL_Transaction_Lines.sCPTDescription, "
                            + " BL_Transaction_Lines.sDx1Code, BL_Transaction_Lines.sDx1Description, BL_Transaction_Lines.sDx2Code, BL_Transaction_Lines.sDx2Description, "
                            + " BL_Transaction_Lines.sDx3Code, BL_Transaction_Lines.sDx3Description, BL_Transaction_Lines.sDx4Code, BL_Transaction_Lines.sDx4Description, "
                            + " BL_Transaction_Lines.sDx5Code, BL_Transaction_Lines.sDx5Description, BL_Transaction_Lines.sDx6Code, BL_Transaction_Lines.sDx6Description, "
                            + " BL_Transaction_Lines.sDx7Code, BL_Transaction_Lines.sDx7Description, BL_Transaction_Lines.sDx8Code, BL_Transaction_Lines.sDx8Description, "
                            + " BL_Transaction_Lines.sMod1Code, BL_Transaction_Lines.sMod1Description, BL_Transaction_Lines.sMod2Code,   BL_Transaction_Lines.sMod2Description, "
                            + " BL_Transaction_Lines.sMod3Code, BL_Transaction_Lines.sMod3Description,  BL_Transaction_Lines.sMod4Code, BL_Transaction_Lines.sMod4Description, "
                            + " BL_Transaction_Lines.dCharges, BL_Transaction_Lines.dTotal, BL_Transaction_Lines.dAllowed, BL_Transaction_Lines.nClinicID, BL_Transaction_MST.nPatientID, "
                            + " BL_Transaction_MST.nClaimNo, BL_Transaction_Lines.dAllowed - (SELECT SUM(dPaymentAmt) AS PaymentAmount FROM   BL_Transaction_Payment_DTL "
                            + " WHERE  (nClinicID = BL_Transaction_Lines.nClinicID) AND (nPatientID = BL_Transaction_MST.nPatientID) AND (nTransactionID = BL_Transaction_Lines.nTransactionID) "
                            + " AND (nTransactionDetailID = BL_Transaction_Lines.nTransactionDetailID)) AS BalanceAmount "
                            + " FROM BL_Transaction_Lines INNER JOIN  BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID  "
                            + " WHERE (BL_Transaction_Lines.dAllowed - (SELECT SUM(dPaymentAmt) AS PaymentAmount  FROM  BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1  "
                            + " WHERE (nClinicID = BL_Transaction_Lines.nClinicID) AND (nPatientID = BL_Transaction_MST.nPatientID) AND (nTransactionID = BL_Transaction_Lines.nTransactionID) "
                            + " AND (nTransactionDetailID = BL_Transaction_Lines.nTransactionDetailID)) >= 0) AND (BL_Transaction_MST.nPatientID = " + PatientID + ") AND (BL_Transaction_Lines.nClinicID = " + _ClinicID + ") "
                            + " UNION "
                            + " SELECT BL_Transaction_Lines.nTransactionID, BL_Transaction_Lines.nTransactionDetailID, BL_Transaction_Lines.nTransactionLineNo, "
                            + " BL_Transaction_Lines.nFromDate, BL_Transaction_Lines.nToDate, BL_Transaction_Lines.sPOSCode, BL_Transaction_Lines.sPOSDescription, "
                            + " BL_Transaction_Lines.sTOSCode, BL_Transaction_Lines.sTOSDescription, BL_Transaction_Lines.sCPTCode, BL_Transaction_Lines.sCPTDescription,   "
                            + " BL_Transaction_Lines.sDx1Code, BL_Transaction_Lines.sDx1Description, BL_Transaction_Lines.sDx2Code, BL_Transaction_Lines.sDx2Description,   "
                            + " BL_Transaction_Lines.sDx3Code, BL_Transaction_Lines.sDx3Description, BL_Transaction_Lines.sDx4Code, BL_Transaction_Lines.sDx4Description,   "
                            + " BL_Transaction_Lines.sDx5Code, BL_Transaction_Lines.sDx5Description, BL_Transaction_Lines.sDx6Code, BL_Transaction_Lines.sDx6Description,   "
                            + " BL_Transaction_Lines.sDx7Code, BL_Transaction_Lines.sDx7Description, BL_Transaction_Lines.sDx8Code, BL_Transaction_Lines.sDx8Description,   "
                            + " BL_Transaction_Lines.sMod1Code, BL_Transaction_Lines.sMod1Description, BL_Transaction_Lines.sMod2Code,   BL_Transaction_Lines.sMod2Description, "
                            + " BL_Transaction_Lines.sMod3Code, BL_Transaction_Lines.sMod3Description,  BL_Transaction_Lines.sMod4Code, BL_Transaction_Lines.sMod4Description, "
                            + " BL_Transaction_Lines.dCharges, BL_Transaction_Lines.dTotal, BL_Transaction_Lines.dAllowed, BL_Transaction_Lines.nClinicID, BL_Transaction_MST.nPatientID, "
                            + " BL_Transaction_MST.nClaimNo, BL_Transaction_Lines.dAllowed AS BalanceAmount  "
                            + " FROM BL_Transaction_Lines INNER JOIN  BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID  "
                            + " WHERE (BL_Transaction_MST.nTransactionID  NOT IN (SELECT nTransactionID FROM BL_Transaction_Payment_DTL WHERE nPatientID = " + PatientID + " AND BL_Transaction_Lines.nClinicID = " + _ClinicID + ") "
                            + " AND BL_Transaction_MST.nPatientID = " + PatientID + " AND BL_Transaction_Lines.nClinicID = " + _ClinicID + ") "
                            + " ORDER BY BL_Transaction_Lines.nFromDate DESC ";

                    oDB.Retrive_Query(strSQL, out dtBalance);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return null;
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                    }
                }
                return dtBalance;
            }

            //To Display Transactions On Dashboard
            public DataTable GetPatientTransactions(Int64 PatientId)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string _strSQL = "";
                DataTable dt = new DataTable();
                try
                {
                    _strSQL = "SELECT ISNULL(BL_Transaction_MST.nClaimNo,0) AS nClaimNo, ISNULL(BL_Transaction_Lines.sCPTCode,'') AS sCPTCode, ISNULL(BL_Transaction_Lines.sCPTDescription,'') AS sCPTDescription, ISNULL(BL_Transaction_Lines.sDx1Code,'') AS sDx1Code, "
                            + " ISNULL(BL_Transaction_Lines.sDx1Description,'') AS sDx1Description, ISNULL(BL_Transaction_Lines.sDx2Code,'') AS sDx2Code, ISNULL(BL_Transaction_Lines.sDx2Description,'') AS sDx2Description, ISNULL(BL_Transaction_Lines.sDx3Code,'') AS sDx3Code, "
                            + " ISNULL(BL_Transaction_Lines.sDx3Description,'') AS sDx3Description, ISNULL(BL_Transaction_Lines.sDx4Code,'') AS sDx4Code, ISNULL(BL_Transaction_Lines.sDx4Description,'') AS sDx4Description, ISNULL(BL_Transaction_Lines.sMod1Code,'') AS sMod1Code,"
                            + " ISNULL(BL_Transaction_Lines.sMod1Description,'') AS sMod1Description, ISNULL(BL_Transaction_Lines.sMod2Code,'') AS sMod2Code, ISNULL(BL_Transaction_Lines.sMod2Description,'') AS sMod2Description, ISNULL(BL_Transaction_Lines.dCharges,0) AS dCharges, "
                            + " ISNULL(BL_Transaction_Lines.dTotal,0) AS dTotal, ISNULL(BL_Transaction_Lines.dUnit,0) AS dUnit,ISNULL(BL_Transaction_Lines.nFromDate,0) AS nFromDate, "
                            + " ISNULL(BL_Transaction_Lines.nTransactionLineStatus,'') AS nTransactionLineStatus,ISNULL(BL_Transaction_MST.nTransactionStatusID,0) AS nTransactionStatusID, "
                            + " ISNULL(BL_Transaction_MST.sState,'') AS sState, "
                            + " ISNULL(BL_Transaction_MST.nHopitalizationDateFrom,0) AS nHopitalizationDateFrom, "
                            + " ISNULL(BL_Transaction_MST.nHopitalizationDateTo,0) AS nHopitalizationDateTo, "
                            + " ISNULL(BL_Transaction_MST.bOutSideLab,0) AS bOutSideLab, "
                            + " ISNULL(BL_Transaction_MST.dOutSideLabCharges,0) AS dOutSideLabCharges, "
                            + " ISNULL(bAutoClaim,0) AS bAutoClaim, "
                            + " ISNULL(nAccidentDate,0) AS nAccidentDate, "
                            + " ISNULL(bWorkersComp,0) AS bWorkersComp "
                            + " FROM BL_Transaction_MST INNER JOIN BL_Transaction_Lines ON BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID"
                            + " WHERE (BL_Transaction_MST.nPatientID = " + PatientId + ") AND (BL_Transaction_MST.nClinicID = " + _ClinicID + ")"
                            + " ORDER BY BL_Transaction_Lines.nFromDate Desc";
                    oDB.Connect(false);
                    oDB.Retrive_Query(_strSQL, out dt);
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return dt;
            }

            public bool IsServiceDatePresent(Int64 PatientId, string ServiceDate)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string _sqlQuery = "";
                Object oResult = new object();
                bool _IsPresent = false;
                try
                {
                    oDB.Connect(false);
                    _sqlQuery = "select ISNULL(Count(dtDate),0) AS IsPresent from PatientTracking where nPatientID = " + PatientId + " and dtDate = '" + ServiceDate + "'";
                    oResult = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (oResult != null && Convert.ToInt64(oResult) > 0)
                    {
                        _IsPresent = true;
                    }
                }
                catch (gloDatabaseLayer.DBException dbEx)
                {
                    dbEx.ERROR_Log(dbEx.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                    if (oResult != null) { oResult = null; }
                }
                return _IsPresent;
            }
            
            public void GetSetting(string SettingName, Int64 UserID, Int64 ClinicID, out object Value)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDB.Connect(false);
                    Value = oDB.ExecuteScalar_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM WITH (NOLOCK) WHERE sSettingsName = '" + SettingName + "' AND nUserID = " + UserID + " AND nClinicID = " + ClinicID + "");
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    Value = null;
                    DBErr.ERROR_Log(DBErr.Message);
                }
                catch (Exception ex)
                {
                    Value = null;
                    MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            public System.Data.DataTable GetInsurance(Int64 InsuranceID)
            {
                System.Data.DataTable _result = new System.Data.DataTable();

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                try
                {
                    //string _sqlQuery = "SELECT nContactID, Isnull(sContact,'') as Contact, Isnull(sName,'')as Name, Isnull(sStreet,'')as Street, Isnull(sCity,'') as City, Isnull(sState,'') as State, Isnull(sZIP,'') as Zip, Isnull(sPhone,'') as Phone, Isnull(sFax,'') as Fax, Isnull(sMobile,'') as Mobile, Isnull(sPager,'') as Pager, Isnull(sEmail,'') as Email, Isnull(sURL,'') as URL, Isnull(sNotes,'') as Notes, nSpecialtyID, nInsuranceID, Isnull(sHospitalAffiliation,'') as HospitalAffiliation, Isnull(sContactType,'') as ContactType, Isnull(sExternalCode,'') as ExternalCode, Isnull(sDegree,'') as Degree, nClinicID, bIsBlocked FROM Contacts_MST where bIsBlocked=0 and sContactType='Insurance' and  nContactID='" + InsuranceID.ToString() + "'";

                    string _sqlQuery = " SELECT nInsuranceID, " +
                        " ISNULL(sInsuranceName,'') AS Name, " +
                        " ISNULL(sAddressLine1,'') AS sAddressLine1, " +
                        " ISNULL(sAddressLine2,'') AS sAddressLine2, " +
                        " Isnull(sCity,'') as City,  " +
                        " Isnull(sState,'') as State, " +
                        " Isnull(sZIP,'') as Zip, " +
                        " Isnull(sPhone,'') as Phone,  " +
                        " Isnull(sFax,'') as Fax, " +
                        " Isnull(sEmail,'') as Email, " +
                        " Isnull(sURL,'') as URL,  " +
                        " ISNULL(nInsuranceFlag,0) AS nInsuranceFlag " +
                        " FROM  " +
                        " PatientInsurance_DTL WITH (NOLOCK) " +
                        " WHERE nInsuranceID = " + InsuranceID.ToString() + " ";

                    oDB.Retrive_Query(_sqlQuery, out _result);
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                    DBErr = null;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                return _result;
            }

          /*  public string EDI837GenerationBCBSM(ArrayList SelectedTransactions, string _BatchName)
            {
                DataTable dtClearingHouse = new DataTable();
                DataTable dtSubmitter = new DataTable();
                DataTable dtReceiver = new DataTable();
                DataTable dtBillingProvider = new DataTable();
                DataTable dtRenderingProvider = new DataTable();
                DataTable dtFacility = new DataTable();
                DataTable dtPatientInsurances = new DataTable();
                DataTable dtReferral = new DataTable();
                string _result = "";
                string InterchangeHeader = "";
                string FunctionalGroupHeader = "";
                string TransactionSetHeader = "";
                string _ClaimStatus = "1";
                if (_IsSEFPresent == true)
                {
                    #region " Generate EDI "

                    string sEntity = "";
                    string sInstance = "";
                    string _strSQL = "";
                    DataTable dt;
                    string _BillingProviderDetails = "";
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
                    bool IsSecondaryInsurance = false;
                    Transaction oTransaction = new Transaction();
                    bool _SecondayInsuranceAddressDetailsRequired = false;
                    //string _result = "";
                    try
                    {
                        //Get Clearing House Information in Datatable

                        dtClearingHouse = new DataTable();
                        dtClearingHouse = ogloBilling.GetClearingHouseSettings();
                        if (dtClearingHouse == null && dtClearingHouse.Rows.Count < 1)
                        {
                            MessageBox.Show("Clearing House information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return "";
                        }
                        if (SelectedTransactions != null)
                        {
                            if (SelectedTransactions.Count > 0)
                            {
                                for (int i = 0; i < SelectedTransactions.Count; i++)
                                {
                                    oTransaction = new Transaction();
                                    TransactionLine oTransLine = null;
                                    oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicID);
                                    if (oTransaction != null)
                                    {
                                        if (oTransaction.Lines.Count > 0)
                                        {
                                            //Get Submitter Information in Datatable
                                            dtSubmitter = new DataTable();
                                            dtSubmitter = ogloBilling.GetSubmitterInfo(Convert.ToInt64(_ClinicID), oTransaction.ProviderID);
                                            if (dtSubmitter == null && dtSubmitter.Rows.Count < 1)
                                            {
                                                MessageBox.Show("Submitter information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return "";
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        oEdiDoc.New();
                        oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                        oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                        oEdiDoc.SegmentTerminator = "~\r\n";
                        oEdiDoc.ElementTerminator = "*";
                        oEdiDoc.CompositeTerminator = ":";

                        string _TypeOfData = "T";

                        #region " Interchange Segment "
                        //Create the interchange segment
                        ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                        if (Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 0 || Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 1)
                        {
                            _TypeOfData = "T";
                        }
                        else if (Convert.ToInt32(dtClearingHouse.Rows[0]["nTypeOfData"]) == 2)
                        {
                            _TypeOfData = "P";
                        }

                        oSegment.set_DataElementValue(1, 0, "00");
                        oSegment.set_DataElementValue(3, 0, "00");
                        oSegment.set_DataElementValue(5, 0, "ZZ");
                        oSegment.set_DataElementValue(6, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", ""));//_SenderID.Trim());//"1234545");//
                        oSegment.set_DataElementValue(7, 0, "ZZ");
                        //oSegment.set_DataElementValue(8, 0, Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim().Replace("*", ""));//_ReceiverID.Trim().Replace("*",""));//"V2EL");//
                        //This is the receiver ID given by BCBSM
                        oSegment.set_DataElementValue(8, 0, "382069753");
                        string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                        oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2));
                        string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                        oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim().Replace("*", ""));
                        oSegment.set_DataElementValue(11, 0, "U");
                        oSegment.set_DataElementValue(12, 0, "00401");
                        InterchangeHeader = ControlNumberGeneration("1");
                        oSegment.set_DataElementValue(13, 0, InterchangeHeader);//"000000020");//
                        oSegment.set_DataElementValue(14, 0, "0");
                        oSegment.set_DataElementValue(15, 0, _TypeOfData);
                        oSegment.set_DataElementValue(16, 0, ":");

                        #endregion " Interchange Segment "

                        #region " Functional Group "

                        //Create the functional group segment
                        ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X098A1"));
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                        oSegment.set_DataElementValue(1, 0, "HC");
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSenderCode"]).Trim().Replace("*", ""));////_SenderName);
                        //oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]).Trim().Replace("*", ""));//// _ReceiverCode.Trim());//"ClarEDI");
                        //This is the receiver ID given by BCBSM
                        oSegment.set_DataElementValue(3, 0, "382069753");
                        oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                        string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                        oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim().Replace("*", ""));
                        FunctionalGroupHeader = ControlNumberGeneration("2");
                        oSegment.set_DataElementValue(6, 0, FunctionalGroupHeader);
                        oSegment.set_DataElementValue(7, 0, "X");
                        oSegment.set_DataElementValue(8, 0, "004010X098A1");

                        #endregion " Functional Group "

                        #region ST - TRANSACTION SET HEADER

                        ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("837"));
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                        TransactionSetHeader = ControlNumberGeneration("3");
                        oSegment.set_DataElementValue(2, 0, TransactionSetHeader); //"00021");//"ControlNo"

                        #endregion ST - TRANSACTION SET HEADER

                        #region BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                        //Begining Segment 
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                        oSegment.set_DataElementValue(1, 0, "0019"); //Herarchical Structure Code
                        oSegment.set_DataElementValue(2, 0, "00"); //00-Original, 01-Re-issue
                        oSegment.set_DataElementValue(3, 0, TransactionSetHeader);//"1234"); //Reference identification
                        oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim().Replace("*", ""));//Date of claim
                        string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim().Replace("*", "");
                        oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim().Replace("*", "")); //"1230");
                        oSegment.set_DataElementValue(6, 0, "CH"); //CH-Chargeable, RP-Reporting
                        #endregion BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                        #region REF - TRANSMISSION TYPE IDENTIFICATION

                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("REF"));
                        oSegment.set_DataElementValue(1, 0, "87");
                        oSegment.set_DataElementValue(2, 0, "004010X098A1");//"ReferenceID"

                        #endregion REF - TRANSMISSION TYPE IDENTIFICATION

                        #region NM1 - SUBMITTER


                        //1000A SUBMITTER
                        //NM1 SUBMITTER

                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\NM1"));
                        oSegment.set_DataElementValue(1, 0, "41");
                        oSegment.set_DataElementValue(2, 0, "2");
                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim().Replace("*", ""));//_SubmitterName);//cmbClinic.Text.Trim());// clinic name
                        oSegment.set_DataElementValue(8, 0, "46"); // Identification Code Qualifier 
                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim().Replace("*", ""));//"C0923");//_SubmitterETIN);//txtSubIdentificationCode.Text.Trim().Replace("*",""));//clinic code or Electronic Transmitter Identification No.


                        //PER SUBMITTER EDI CONTACT INFORMATION
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\PER"));
                        oSegment.set_DataElementValue(1, 0, "IC");
                        if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim().Replace("*", "") == "")
                        {
                            oSegment.set_DataElementValue(2, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim().Replace("*", ""));//txtSubmitterContactName.Text.Trim().Replace("*",""));//Contact person name of clinic
                        }
                        else
                        {
                            oSegment.set_DataElementValue(2, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim().Replace("*", ""));
                        }

                        oSegment.set_DataElementValue(3, 0, "TE");
                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim().Replace("*", ""));//txtSubmitterPhone.Text.Trim().Replace("*","").Replace("(", "").Replace(")", "").Replace("-", ""));//clinic phone


                        #endregion NM1 - SUBMITTER

                        #region NM1 - RECEIVER NAME

                        //1000B RECEIVER
                        //NM1 RECEIVER NAME
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1(2)\\NM1"));
                        oSegment.set_DataElementValue(1, 0, "40");
                        oSegment.set_DataElementValue(2, 0, "2");
                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sClearingHouseCode"]).Trim().Replace("*", ""));//"GatewayEDI");//clearing house or contractor or carrier or FI name
                        oSegment.set_DataElementValue(8, 0, "46");// Identification Code Qualifier
                        //*ID Code?
                        ///This is the Receiver ID For BCBSM Clearing House
                        oSegment.set_DataElementValue(9, 0, "00710");// Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]));//"V2093");//code of carrier/contractor/FI or Electronic Transmitter Identification No.

                        #endregion NM1 - RECEIVER NAME

                        nHlCount = 0;

                        if (SelectedTransactions != null)
                        {
                            if (SelectedTransactions.Count > 0)
                            {
                                for (int i = 0; i < SelectedTransactions.Count; i++)
                                {
                                    oTransaction = new Transaction();
                                    TransactionLine oTransLine = null;
                                    oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicID);
                                    if (oTransaction != null)
                                    {
                                        if (oTransaction.Lines.Count > 0)
                                        {
                                            //FillAllDetails(oTransaction);
                                            Resource oResource = new Resource(_databaseconnectionstring);
                                            Provider _Provider = null;
                                            gloPatient.Patient oPatient = null;
                                            gloPatient.Referrals oReferral = new gloPatient.Referrals();
                                            if (Convert.ToInt64(oTransaction.ProviderID) != 0 && oTransaction.ProviderID.ToString() != "")
                                            {
                                                _Provider = oResource.GetProviderDetail(Convert.ToInt64(oTransaction.ProviderID));
                                                if (_Provider == null)
                                                {
                                                    MessageBox.Show("Provider information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    return "";
                                                }
                                                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                                                dtPatientInsurances = ogloPatient.getPatientInsurances(oTransaction.PatientID);
                                                oPatient = ogloPatient.GetPatient(oTransaction.PatientID);
                                                if (oPatient == null)
                                                {
                                                    MessageBox.Show("Patient information is not present for claim number " + FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNo)) + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    return "";
                                                }
                                                if (dtPatientInsurances == null || dtPatientInsurances.Rows.Count < 1)
                                                {
                                                    MessageBox.Show("Patient " + oPatient.DemographicsDetail.PatientFirstName + " " + oPatient.DemographicsDetail.PatientLastName + " Insurance details are missing for claim number " + FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNo)) + ".  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    return "";
                                                }
                                                dtFacility = ogloBilling.GetFacilityInfo(oTransaction.FacilityCode, oTransaction.ProviderID);


                                            }

                                            for (int nTransactionSet = 1; nTransactionSet <= 1; nTransactionSet++)
                                            {
                                                //**** BILLING/PAY-TO PROVIDER HIERARCHICAL LEVEL *******************************************

                                                nHlCount = nHlCount + 1;
                                                nHlProvParent = nHlCount;
                                                //2000A BILLING/PAY-TO PROVIDER HL LOOP
                                                //HL-BILLING PROVIDER

                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim().Replace("*", ""));
                                                oSegment.set_DataElementValue(3, 0, "20");
                                                oSegment.set_DataElementValue(4, 0, "1");

                                                #region Billing Provider


                                                //Get the Address Setting for Billing Provider
                                                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                                                Object _objResult = null;
                                                string strBillingSetting = "";
                                                oSettings.GetSetting("BillingSetting", Convert.ToInt64(oTransaction.ProviderID), _ClinicID, out _objResult);
                                                if (_objResult != null)
                                                {
                                                    // |Company|Practice|Business"
                                                    strBillingSetting = Convert.ToString(_objResult);
                                                }
                                                switch (strBillingSetting)
                                                {
                                                    case "Business":
                                                        {
                                                            //2010AA BILLING PROVIDER
                                                            //NM1 BILLING PROVIDER NAME
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                            oSegment.set_DataElementValue(1, 0, "85");
                                                            oSegment.set_DataElementValue(2, 0, "1");
                                                            oSegment.set_DataElementValue(3, 0, _Provider.LastName.Trim().Replace("*", ""));//Billing provider name
                                                            oSegment.set_DataElementValue(4, 0, _Provider.FirstName.Trim().Replace("*", ""));
                                                            oSegment.set_DataElementValue(5, 0, _Provider.MiddleName.Trim().Replace("*", ""));

                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            if (_Provider.NPI.Trim().Replace("*", "") != "")
                                                            {
                                                                oSegment.set_DataElementValue(9, 0, _Provider.NPI.Trim().Replace("*", ""));//Billing provider ID/NPI
                                                            }
                                                            //N3 BILLING PROVIDER ADDRESS
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                            oSegment.set_DataElementValue(1, 0, _Provider.BMAddress1.Trim().Replace("*", ""));//Provider Address

                                                            //N4 BILLING PROVIDER LOCATION
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                            oSegment.set_DataElementValue(1, 0, _Provider.BMCity.Trim().Replace("*", ""));////Provider City
                                                            oSegment.set_DataElementValue(2, 0, _Provider.BMState.Trim().Replace("*", ""));//Provider state
                                                            oSegment.set_DataElementValue(3, 0, _Provider.BMZIP.Trim().Replace("*", ""));//Provider ZIP

                                                            //REF 
                                                            if (_Provider.EmployerID.Trim().Replace("*", "") != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                oSegment.set_DataElementValue(1, 0, "EI");//Reference Identification Qualifier("EI" stands for-> Employer ID)
                                                                if (_Provider.EmployerID.Length > 9)
                                                                {
                                                                    oSegment.set_DataElementValue(2, 0, _Provider.EmployerID.Trim().Replace("*", "").Substring(0, 9));
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, _Provider.EmployerID.Trim().Replace("*", ""));
                                                            }
                                                            //REF 
                                                            else if (_Provider.SSN.Trim().Replace("*", "") != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                oSegment.set_DataElementValue(1, 0, "SY");//Reference Identification Qualifier("SY" stands for-> Social Security Number)
                                                                if (_Provider.SSN.Trim().Replace("*", "").Length > 9)
                                                                {
                                                                    oSegment.set_DataElementValue(2, 0, _Provider.SSN.Trim().Replace("*", "").Substring(0, 9));
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, _Provider.SSN.Trim().Replace("*", ""));
                                                            }

                                                        } break;
                                                    case "Practice":
                                                        {
                                                            //2010AA BILLING PROVIDER
                                                            //NM1 BILLING PROVIDER NAME
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                            oSegment.set_DataElementValue(1, 0, "85");
                                                            oSegment.set_DataElementValue(2, 0, "1");
                                                            oSegment.set_DataElementValue(3, 0, _Provider.LastName.Trim().Replace("*", ""));//Billing provider name
                                                            oSegment.set_DataElementValue(4, 0, _Provider.FirstName.Trim().Replace("*", ""));
                                                            oSegment.set_DataElementValue(5, 0, _Provider.MiddleName.Trim().Replace("*", ""));

                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            if (_Provider.NPI.Trim().Replace("*", "") != "")
                                                            {
                                                                oSegment.set_DataElementValue(9, 0, _Provider.NPI.Trim().Replace("*", ""));//Billing provider ID/NPI
                                                            }
                                                            //N3 BILLING PROVIDER ADDRESS
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                            oSegment.set_DataElementValue(1, 0, _Provider.BPracAddress1.Trim().Replace("*", ""));//Provider Address

                                                            //N4 BILLING PROVIDER LOCATION
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                            oSegment.set_DataElementValue(1, 0, _Provider.BPracCity.Trim().Replace("*", ""));////Provider City
                                                            oSegment.set_DataElementValue(2, 0, _Provider.BPracState.Trim().Replace("*", ""));//Provider state
                                                            oSegment.set_DataElementValue(3, 0, _Provider.BPracZIP.Trim().Replace("*", ""));//Provider ZIP

                                                            //REF 
                                                            if (_Provider.EmployerID.Trim().Replace("*", "") != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                oSegment.set_DataElementValue(1, 0, "EI");//Reference Identification Qualifier("EI" stands for-> Employer ID)
                                                                if (_Provider.EmployerID.Length > 9)
                                                                {
                                                                    oSegment.set_DataElementValue(2, 0, _Provider.EmployerID.Trim().Replace("*", "").Substring(0, 9));
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, _Provider.EmployerID.Trim().Replace("*", ""));
                                                            }
                                                            //REF 
                                                            else if (_Provider.SSN.Trim().Replace("*", "") != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                oSegment.set_DataElementValue(1, 0, "SY");//Reference Identification Qualifier("SY" stands for-> Social Security Number)
                                                                if (_Provider.SSN.Trim().Replace("*", "").Length > 9)
                                                                {
                                                                    oSegment.set_DataElementValue(2, 0, _Provider.SSN.Trim().Replace("*", "").Substring(0, 9));
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, _Provider.SSN.Trim().Replace("*", ""));
                                                            }

                                                        } break;
                                                    case "Company":
                                                        {
                                                            //2010AA BILLING PROVIDER
                                                            //NM1 BILLING PROVIDER NAME
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                            oSegment.set_DataElementValue(1, 0, "85");
                                                            oSegment.set_DataElementValue(2, 0, "2");
                                                            oSegment.set_DataElementValue(3, 0, _Provider.CompanyName.Trim().Replace("*", ""));//Billing provider name
                                                            //oSegment.set_DataElementValue(4, 0, _Provider.FirstName.Trim().Replace("*",""));
                                                            //oSegment.set_DataElementValue(5, 0, _Provider.MiddleName.Trim().Replace("*",""));

                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            if (_Provider.CompanyNPI.Trim().Replace("*", "") != "")
                                                            {
                                                                oSegment.set_DataElementValue(9, 0, _Provider.CompanyNPI.Trim().Replace("*", ""));//Billing provider ID/NPI
                                                            }
                                                            //N3 BILLING PROVIDER ADDRESS
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                            oSegment.set_DataElementValue(1, 0, _Provider.CompanyAddress1.Trim().Replace("*", ""));//Provider Address

                                                            //N4 BILLING PROVIDER LOCATION
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                            oSegment.set_DataElementValue(1, 0, _Provider.CompanyCity.Trim().Replace("*", ""));////Provider City
                                                            oSegment.set_DataElementValue(2, 0, _Provider.CompanyState.Trim().Replace("*", ""));//Provider state
                                                            oSegment.set_DataElementValue(3, 0, _Provider.CompanyZip.Trim().Replace("*", ""));//Provider ZIP

                                                            //REF 
                                                            if (_Provider.CompanyTaxID.Trim().Replace("*", "") != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                                oSegment.set_DataElementValue(1, 0, "EI");//Reference Identification Qualifier("EI" stands for-> Employer ID)
                                                                if (_Provider.CompanyTaxID.Length > 9)
                                                                {
                                                                    oSegment.set_DataElementValue(2, 0, _Provider.CompanyTaxID.Trim().Replace("*", "").Substring(0, 9));
                                                                }
                                                                oSegment.set_DataElementValue(2, 0, _Provider.CompanyTaxID.Trim().Replace("*", ""));
                                                            }

                                                        } break;
                                                    default:

                                                        //2010AA BILLING PROVIDER
                                                        //NM1 BILLING PROVIDER NAME
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "85");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, _Provider.LastName.Trim().Replace("*", ""));//Billing provider name
                                                        oSegment.set_DataElementValue(4, 0, _Provider.FirstName.Trim().Replace("*", ""));
                                                        oSegment.set_DataElementValue(5, 0, _Provider.MiddleName.Trim().Replace("*", ""));

                                                        oSegment.set_DataElementValue(8, 0, "XX");
                                                        if (_Provider.NPI.Trim().Replace("*", "") != "")
                                                        {
                                                            oSegment.set_DataElementValue(9, 0, _Provider.NPI.Trim().Replace("*", ""));//Billing provider ID/NPI
                                                        }

                                                        //N3 BILLING PROVIDER ADDRESS
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                        oSegment.set_DataElementValue(1, 0, _Provider.BMAddress1);//Provider Address

                                                        //N4 BILLING PROVIDER LOCATION
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                        oSegment.set_DataElementValue(1, 0, _Provider.BMCity);////Provider City
                                                        oSegment.set_DataElementValue(2, 0, _Provider.BMState);//Provider state
                                                        oSegment.set_DataElementValue(3, 0, _Provider.BMZIP);//Provider ZIP

                                                        //REF 
                                                        if (_Provider.EmployerID.Trim().Replace("*", "") != "")
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, "EI");//Reference Identification Qualifier("EI" stands for-> Employer ID)
                                                            if (_Provider.EmployerID.Length > 9)
                                                            {
                                                                oSegment.set_DataElementValue(2, 0, _Provider.EmployerID.Trim().Replace("*", "").Substring(0, 9));
                                                            }
                                                            oSegment.set_DataElementValue(2, 0, _Provider.EmployerID.Trim().Replace("*", ""));
                                                        }
                                                        //REF 
                                                        else if (_Provider.SSN.Trim().Replace("*", "") != "")
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, "SY");//Reference Identification Qualifier("SY" stands for-> Social Security Number)
                                                            if (_Provider.SSN.Trim().Replace("*", "").Length > 9)
                                                            {
                                                                oSegment.set_DataElementValue(2, 0, _Provider.SSN.Trim().Replace("*", "").Substring(0, 9));
                                                            }
                                                            oSegment.set_DataElementValue(2, 0, _Provider.SSN.Trim().Replace("*", ""));
                                                        }
                                                        break;
                                                }

                                                #endregion

                                                //'******************************************************************************************************
                                                //'******* SUBSCRIBER HIERARCHICAL LEVEL ****************************************************************
                                                //'******************************************************************************************************
                                                #region Subscriber
                                                if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                                                {
                                                    string _strRelation = "";
                                                    string _strInsuranceType = "";
                                                    _strRelation = Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim().Replace("*", "");
                                                    _strInsuranceType = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", "");
                                                    if (_strInsuranceType == "MB")
                                                    {
                                                        if (_strRelation != "18")
                                                        {
                                                            _strRelation = "18";
                                                        }
                                                    }

                                                    #region Subscriber HL Loop - 2000B

                                                    nHlCount = nHlCount + 1;
                                                    nHlSubscriberParent = nHlCount;

                                                    //2000B SUBSCRIBER HL LOOP
                                                    //HL-SUBSCRIBER
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                    oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim().Replace("*", ""));
                                                    oSegment.set_DataElementValue(2, 0, nHlProvParent.ToString().Trim().Replace("*", ""));
                                                    oSegment.set_DataElementValue(3, 0, "22");

                                                    if (_strRelation == "18")
                                                    {
                                                        oSegment.set_DataElementValue(4, 0, "0");
                                                    }
                                                    else
                                                    {
                                                        oSegment.set_DataElementValue(4, 0, "1");

                                                    }

                                                    //SBR SUBSCRIBER INFORMATION
                                                  
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\SBR"));
                              /////////////////////*Should not be T */
                                                  /*  oSegment.set_DataElementValue(1, 0, "P");//_SubscriberInsurancePST);//"P");
                                                    if (_strRelation == "18")
                                                    {
                                                        oSegment.set_DataElementValue(2, 0, _strRelation);
                                                    }
                                                    oSegment.set_DataElementValue(4, 0, "Insured's Group Name");
                                                    //oSegment.set_DataElementValue(5, 0, Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", ""));
                             ////////////////////////*BL for Blue Shield
                                       /*             oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]).Trim().Replace("*", ""));//"HM");

                                                    //2010BA SUBSCRIBER 
                                                    //NM1 SUBSCRIBER NAME
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "IL");
                                                    oSegment.set_DataElementValue(2, 0, "1");
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubLName"]).Trim().Replace("*", ""));//"SubscriberLastOrgName"
                                                    oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubFName"]).Trim().Replace("*", ""));//"SubscriberFirstname"
                                                    oSegment.set_DataElementValue(8, 0, "MI");
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]).Trim().Replace("*", ""));//"Insurance Id"

                                                    //N3 SUBSCRIBER ADDRESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr1"]).Trim().Replace("*", ""));//"SubscriberAddress"

                                                    //N4 SUBSCRIBER CITY
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCity"]).Trim().Replace("*", ""));//"SubscriberCity"
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberState"]).Trim().Replace("*", ""));//"SubscrberState"
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberZip"]).Trim().Replace("*", ""));//"SubscriberZip"

                                                    #endregion SubscriberHL Loop - 2000B

                                                    if (_strRelation == "18")
                                                    {

                                                        //DMG SUBSCRIBER DEMOGRAPHIC INFORMATION
                                                        string _SubscriberGender = "";
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                                        oSegment.set_DataElementValue(1, 0, "D8");
                                                        if (Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]).Trim().Replace("*", "") != "")
                                                            oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]))));//"SubscriberDOB"
                                                        if (Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim().Replace("*", "") != "")
                                                        {
                                                            _SubscriberGender = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim().Replace("*", "");
                                                            if (_SubscriberGender.Trim().Replace("*", "").ToUpper() == "OTHER")
                                                            {
                                                                _SubscriberGender = "U";
                                                            }
                                                            oSegment.set_DataElementValue(3, 0, _SubscriberGender.Trim().Replace("*", "").Substring(0, 1).ToUpper());//"SubscriberGender"
                                                        }


                                                        #region Payer Information Loop 2010BB
                                                        //2010BB SUBSCRIBER/PAYER
                                                        //NM1 PAYER NAME
                                                        string _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "");
                                                        if (Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Length > 35)
                                                        {
                                                            _ModifiedPayerName = "";
                                                            _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Substring(0, 34);
                                                        }
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "PR");
                                                        oSegment.set_DataElementValue(2, 0, "2");
                                                        oSegment.set_DataElementValue(3, 0, _ModifiedPayerName.Trim().Replace("*", ""));//"PayerLastOrgName"
                                                        oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                        //*Should be 00710 FOR BCBS
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", ""));//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID

                                                        ////////N3 PAYER ADDRESS
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N3"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress1"]).Trim().Replace("*", ""));//"InsuranceAddress"

                                                        ////////N4 PAYER CITY
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N4"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerCity"]).Trim().Replace("*", ""));//"InsuranceCity"
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerState"]).Trim().Replace("*", ""));//"InsuranceState"
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerZip"]).Trim().Replace("*", ""));//"InsuranceZip"

                                                        #endregion


                                                        //******* SUBSCRIBER CLAIM INFORMATION ***************************************************************
                                                        //TODO: Get Details in DATATABLE for the fields to be entered in EDI file.
                                                        string _FirstPOS = "";
                                                        string _NewPOS = "";
                                                        string _ClaimTotal = "";
                                                        iItemCount = 0;
                                                        decimal _claimAmount = 0;
                                                        for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                        {
                                                            oTransLine = new TransactionLine();
                                                            oTransLine = oTransaction.Lines[nLine];
                                                            _claimAmount = _claimAmount + oTransLine.Total;

                                                            _FirstPOS = oTransaction.Lines[0].POSCode;
                                                            _NewPOS = oTransLine.POSCode;
                                                        }

                                                        _ClaimTotal = _claimAmount.ToString("#0.00");

                                                        if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                                        {
                                                            _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                                        }
                                                        else if (_ClaimTotal.Substring(_ClaimTotal.Length - 1, 1) == "0")
                                                        {
                                                            _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 1);
                                                        }
                                                        //if (_FirstPOS ==_NewPOS)
                                                        //{
                                                        #region Claim Details - Loop 2300
                                                        //2300 CLAIM
                                                        //CLM CLAIM LEVEL INFORMATION
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
                                                        oSegment.set_DataElementValue(1, 0, FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNo).Trim().Replace("*", ""))); //Patient Account no         
                                                        oSegment.set_DataElementValue(2, 0, _ClaimTotal.Trim().Replace("*", ""));// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_TOTAL))); //Claim Amount

                                                        oSegment.set_DataElementValue(5, 1, _FirstPOS.Trim().Replace("*", "")); //21 - Inpatient Hospital

                                                        if (oTransaction.Transaction_Status == TransactionStatus.Batch)
                                                        {
                                                            _ClaimStatus = "1";
                                                        }
                                                        else if (oTransaction.Transaction_Status == TransactionStatus.ReBatch)
                                                        {
                                                            _ClaimStatus = "6";
                                                        }
                                                        oSegment.set_DataElementValue(5, 3, _ClaimStatus.Trim().Replace("*", ""));
                                                        oSegment.set_DataElementValue(6, 0, "Y");
                                                        oSegment.set_DataElementValue(7, 0, "A");
                                                        oSegment.set_DataElementValue(8, 0, "Y");
                                                        oSegment.set_DataElementValue(9, 0, "Y");
                                                        oSegment.set_DataElementValue(10, 0, "C");
                                                        if (oTransaction.AutoClaim == true)
                                                        {
                                                            if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
                                                            {
                                                                oSegment.set_DataElementValue(11, 1, "AA");
                                                                oSegment.set_DataElementValue(11, 4, oTransaction.State.Trim().Replace("*", ""));
                                                            }
                                                        }

                                                        string OnsetDate = "";
                                                        if (oTransaction.InjuryDate.ToString() != "" || oTransaction.OnsiteDate.ToString() != "" || oTransaction.AccidentDate.ToString() != "")
                                                        {
                                                            if (oTransaction.InjuryDate.ToString() != "" && oTransaction.InjuryDate > 0)
                                                            {
                                                                OnsetDate = Convert.ToString(oTransaction.InjuryDate);
                                                                ////DTP DATE OF ONSET OF CURRENT SYMPTOMS OR ILLNESS
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                oSegment.set_DataElementValue(1, 0, "431");
                                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                                oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                            }
                                                            else if (oTransaction.OnsiteDate.ToString() != "" && oTransaction.OnsiteDate > 0)
                                                            {
                                                                OnsetDate = Convert.ToString(oTransaction.OnsiteDate);
                                                                ////DTP DATE OF CURRENT INJURY
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                oSegment.set_DataElementValue(1, 0, "431");
                                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                                oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                            }
                                                            if (oTransaction.AutoClaim == true)
                                                            {
                                                                if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
                                                                {
                                                                    OnsetDate = Convert.ToString(oTransaction.AccidentDate);
                                                                    ////DTP DATE OF ACCIDENT 
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                    oSegment.set_DataElementValue(1, 0, "439");
                                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                                    oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                                }
                                                            }
                                                        }

                                                        //DTP DATE OF ONSET of similar symptoms or illness
                                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        //oSegment.set_DataElementValue(1, 0, "438");
                                                        //oSegment.set_DataElementValue(2, 0, "D8");
                                                        //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetoSimilarSyptomsorillness.Value.ToShortDateString())).Trim().Replace("*",""));
                                                        //
                                                        if (_FirstPOS.Trim().Replace("*", "") != "11")
                                                        {
                                                            if (oTransaction.HospitalizationDateFrom > 0 && oTransaction.HospitalizationDateFrom.ToString() != "")
                                                            {
                                                                //DTP DATE OF Hospitalization (Admission) 
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                oSegment.set_DataElementValue(1, 0, "435");
                                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                                oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateFrom.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalizationDate.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                            }

                                                            if (oTransaction.HospitalizationDateTo > 0 && oTransaction.HospitalizationDateTo.ToString() != "")
                                                            {
                                                                ////DTP DATE OF Discharge 
                                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                //oSegment.set_DataElementValue(1, 0, "096");
                                                                //oSegment.set_DataElementValue(2, 0, "D8");
                                                                //oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateTo.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalDischargeDate.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                                ////
                                                            }
                                                        }

                                                        if (oTransaction.WorkersComp == true)
                                                        {
                                                            if (oTransaction.UnableToWorkFromDate > 0 && oTransaction.UnableToWorkFromDate.ToString() != "")
                                                            {
                                                                //DTP DATE OF (Intial Disability period last day worked)
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                oSegment.set_DataElementValue(1, 0, "297");
                                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                                oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkFromDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpLastDayWorked.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                                //
                                                            }

                                                            if (oTransaction.UnableToWorkTillDate > 0 && oTransaction.UnableToWorkTillDate.ToString() != "")
                                                            {
                                                                //DTP DATE OF (Intial Disability period return to work)
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                oSegment.set_DataElementValue(1, 0, "296");
                                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                                oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkTillDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpReturnToWork.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                                //
                                                            }
                                                        }
                                                        if (GetPriorAuthorizationNumber(oTransaction.PatientID, Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"])).Trim().Replace("*", "") != "")
                                                        {
                                                            //REF CLEARING HOUSE CLAIM NUMBER
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, "G1");
                                                            oSegment.set_DataElementValue(2, 0, GetPriorAuthorizationNumber(oTransaction.PatientID, Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"])).Trim().Replace("*", "")); //Claim No
                                                        }



                                                        #endregion

                                                        #region HI - Diagnosis


                                                        //HI HEALTH CARE DIAGNOSIS CODES
                                                        #region Commented code
                                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                        //oSegment.set_DataElementValue(1, 1, "BK");
                                                        //if (oTransaction.Lines[0].Dx1Code.ToString().Trim() != "")
                                                        //{
                                                        //    oSegment.set_DataElementValue(1, 2, oTransaction.Lines[0].Dx1Code.ToString().Replace(".", "").Trim());// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE)));  // "0340");
                                                        //    if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx1Code.Trim())) == false)
                                                        //    {
                                                        //        return;
                                                        //    }
                                                        //}
                                                        //else
                                                        //{
                                                        //    //MessageBox.Show("Principle Diagnosis is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        //    //return;
                                                        //}
                                                        //if (oTransaction.Lines[0].Dx2Code.ToString().Trim() != "")
                                                        //{
                                                        //    oSegment.set_DataElementValue(2, 1, "BF");
                                                        //    oSegment.set_DataElementValue(2, 2, oTransaction.Lines[0].Dx2Code.ToString().Replace(".", "").Trim());//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE))); // oCase.CaseDiagnosis[0].DiagnosisCode.ToString().Replace(".", "").Trim());
                                                        //    if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx2Code.Trim())) == false)
                                                        //    {
                                                        //        return;
                                                        //    }
                                                        //}
                                                        //if (oTransaction.Lines[0].Dx3Code.ToString().Trim() != "")
                                                        //{
                                                        //    oSegment.set_DataElementValue(3, 1, "BF");
                                                        //    oSegment.set_DataElementValue(3, 2, oTransaction.Lines[0].Dx3Code.ToString().Replace(".", "").Trim());
                                                        //    if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx3Code.Trim())) == false)
                                                        //    {
                                                        //        return;
                                                        //    }
                                                        //}
                                                        //if (oTransaction.Lines[0].Dx4Code.ToString().Trim() != "")
                                                        //{
                                                        //    oSegment.set_DataElementValue(4, 1, "BF");
                                                        //    oSegment.set_DataElementValue(4, 2, oTransaction.Lines[0].Dx4Code.ToString().Replace(".", "").Trim());
                                                        //    if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx4Code.Trim())) == false)
                                                        //    {
                                                        //        return;
                                                        //    }
                                                        //}
                                                        //if (oTransaction.Lines[0].Dx5Code.ToString().Trim() != "")
                                                        //{
                                                        //    oSegment.set_DataElementValue(5, 1, "BF");
                                                        //    oSegment.set_DataElementValue(5, 2, oTransaction.Lines[0].Dx5Code.ToString().Replace(".", "").Trim());
                                                        //}
                                                        //if (oTransaction.Lines[0].Dx6Code.ToString().Trim() != "")
                                                        //{
                                                        //    oSegment.set_DataElementValue(6, 1, "BF");
                                                        //    oSegment.set_DataElementValue(6, 2, oTransaction.Lines[0].Dx6Code.ToString().Replace(".", "").Trim());
                                                        //}
                                                        //if (oTransaction.Lines[0].Dx7Code.ToString().Trim() != "")
                                                        //{
                                                        //    oSegment.set_DataElementValue(7, 1, "BF");
                                                        //    oSegment.set_DataElementValue(7, 2, oTransaction.Lines[0].Dx7Code.ToString().Replace(".", "").Trim());
                                                        //}
                                                        //if (oTransaction.Lines[0].Dx8Code.ToString().Trim() != "")
                                                        //{
                                                        //    oSegment.set_DataElementValue(8, 1, "BF");
                                                        //    oSegment.set_DataElementValue(8, 2, oTransaction.Lines[0].Dx8Code.ToString().Replace(".", "").Trim());
                                                        //}
                                                        ////} 
                                                        #endregion Commented code
                                                        DataTable dtDx = new DataTable();
                                                        dtDx = GetDistinctDiagnosis(oTransaction.TransactionID, oTransaction.ClinicID, oTransaction.ClaimNo);

                                                        if (dtDx != null && dtDx.Rows.Count > 0)
                                                        {


                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));

                                                            for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                            {
                                                                if (DxIndex == 0)
                                                                {
                                                                    if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "") != "")
                                                                    {
                                                                        if (IsValidICD9(Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "")) == false)
                                                                        {
                                                                            return "";
                                                                        }
                                                                        oSegment.set_DataElementValue(1, 1, "BK");
                                                                        oSegment.set_DataElementValue(DxIndex + 1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Replace(".", "").Trim().Replace("*", ""));
                                                                    }
                                                                }
                                                                if (DxIndex > 0)
                                                                {
                                                                    if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "") != "")
                                                                    {
                                                                        if (IsValidICD9(Convert.ToString(Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", ""))) == false)
                                                                        {
                                                                            return "";
                                                                        }
                                                                        oSegment.set_DataElementValue(DxIndex + 1, 1, "BF");
                                                                        oSegment.set_DataElementValue(DxIndex + 1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).ToString().Replace(".", "").Trim().Replace("*", ""));//
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        #endregion

                                                        #region Referring Provider - 2310A

                                                        if (oTransaction.ReferralProviderID > 0)
                                                        {
                                                            oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                                            dtReferral = new DataTable();
                                                            string _sqlQuery = "";

                                                            oDB.Connect(false);

                                                            _sqlQuery = " SELECT ISNULL(nContactId,0) AS nContactId, " +
                                                                       " ISNULL(sName,'') AS sName,  " +
                                                                       " ISNULL(sContact,'') AS sContact,   " +
                                                                       " ISNULL(sAddressLine1,'') AS sAddressLine1,   " +
                                                                       " ISNULL(sAddressLine2,'') AS sAddressLine2,   " +
                                                                       " ISNULL(sCity,'') AS sCity,   " +
                                                                       " ISNULL(sState,'') AS sState,   " +
                                                                       " ISNULL(sZIP,'') AS sZIP,   " +
                                                                       " ISNULL(sPhone,'') AS sPhone,   " +
                                                                       " ISNULL(sFax,'') AS sFax,   " +
                                                                       " ISNULL(sEmail,'') AS sEmail,   " +
                                                                       " ISNULL(sURL,'') AS sURL,   " +
                                                                       " ISNULL(sMobile,'') AS sMobile,   " +
                                                                       " ISNULL(sPager,'') AS sPager,   " +
                                                                       " ISNULL(sNotes,'') AS sNotes,   " +
                                                                       " ISNULL(sFirstName,'') AS sFirstName,   " +
                                                                       " ISNULL(sMiddleName,'') AS sMiddleName,   " +
                                                                       " ISNULL(sLastName,'') AS sLastName,   " +
                                                                       " ISNULL(sGender,'') AS sGender,   " +
                                                                       " ISNULL(sTaxonomy,'') AS sTaxonomy,   " +
                                                                       " ISNULL(sTaxonomyDesc,'') AS sTaxonomyDesc,   " +
                                                                       " ISNULL(sTaxID,'') AS sTaxID,   " +
                                                                       " ISNULL(sUPIN,'') AS sUPIN,   " +
                                                                       " ISNULL(sNPI,'') AS sNPI,   " +
                                                                       " ISNULL(sDegree,'') AS sDegree   " +
                                                                       " FROM  Patient_DTL " +
                                                                       " WHERE (nContactFlag = 3) AND (nPatientID = " + oTransaction.PatientID + ") AND (nPatientDetailID = " + oTransaction.ReferralProviderID + ") AND ISNULL(nClinicID,1)=" + ClinicID + "";

                                                            oDB.Retrive_Query(_sqlQuery, out dtReferral);
                                                            if (dtReferral != null && dtReferral.Rows.Count > 0)
                                                            {
                                                                //2310B Referring PROVIDER
                                                                //NM1 Referring PROVIDER NAME
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                                oSegment.set_DataElementValue(1, 0, "DN");
                                                                oSegment.set_DataElementValue(2, 0, "1");
                                                                oSegment.set_DataElementValue(3, 0, dtReferral.Rows[0]["sLastName"].ToString().Trim().Replace("*", "")); //"ReferringLastname"
                                                                oSegment.set_DataElementValue(4, 0, dtReferral.Rows[0]["sFirstName"].ToString().Trim().Replace("*", ""));//"ReferringFirstname"
                                                                oSegment.set_DataElementValue(5, 0, dtReferral.Rows[0]["sMiddleName"].ToString().Trim().Replace("*", ""));
                                                                oSegment.set_DataElementValue(8, 0, "XX");
                                                                oSegment.set_DataElementValue(9, 0, dtReferral.Rows[0]["sNPI"].ToString().Trim().Replace("*", ""));//"NPI"

                                                                //PRV REFERRING PROVIDER INFORMATION
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                                oSegment.set_DataElementValue(1, 0, "RF");
                                                                oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                                oSegment.set_DataElementValue(3, 0, dtReferral.Rows[0]["sTaxonomy"].ToString().Trim().Replace("*", ""));//Reference Identification

                                                                //REF
                                                                //if (dtReferral.Rows[0]["sTaxID"].ToString().Trim().Replace("*", "") != "")
                                                                //{
                                                                //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                                //    oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
                                                                //    oSegment.set_DataElementValue(2, 0, dtReferral.Rows[0]["sTaxID"].ToString().Trim().Replace("*", ""));//"1039255");// 
                                                                //}
                                                                //else //if (_ReferralSSN.Trim().Replace("*","") != "")
                                                                //{
                                                                //    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                                //    //oSegment.set_DataElementValue(1, 0, "SY");//// Social Security Number
                                                                //    //oSegment.set_DataElementValue(2, 0, dtReferral.Rows[0]["sTaxID"].ToString().Trim().Replace("*", ""));//"1039255");// 
                                                                //}
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (_Provider != null)
                                                            {
                                                                //2310B Referring PROVIDER
                                                                //NM1 Referring PROVIDER NAME
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                                oSegment.set_DataElementValue(1, 0, "DN");
                                                                oSegment.set_DataElementValue(2, 0, "1");
                                                                oSegment.set_DataElementValue(3, 0, _Provider.LastName.ToString().Trim().Replace("*", "")); //"ReferringLastname"
                                                                oSegment.set_DataElementValue(4, 0, _Provider.FirstName.ToString().Trim().Replace("*", ""));//"ReferringFirstname"
                                                                oSegment.set_DataElementValue(5, 0, _Provider.MiddleName.ToString().Trim().Replace("*", ""));
                                                                oSegment.set_DataElementValue(8, 0, "XX");
                                                                oSegment.set_DataElementValue(9, 0, _Provider.NPI.ToString().Trim().Replace("*", ""));//"NPI"

                                                                //PRV REFERRING PROVIDER INFORMATION
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                                oSegment.set_DataElementValue(1, 0, "RF");
                                                                oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                                oSegment.set_DataElementValue(3, 0, _Provider.Taxonomy.ToString().Trim().Replace("*", ""));//Reference Identification

                                                                ////REF
                                                                //if (_Provider.EmployerID.ToString().Trim().Replace("*", "") != "")
                                                                //{
                                                                //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                                //    oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
                                                                //    oSegment.set_DataElementValue(2, 0, _Provider.EmployerID.ToString().Trim().Replace("*", ""));//"1039255");// 
                                                                //}
                                                                //else //if (_ReferralSSN.Trim().Replace("*","") != "")
                                                                //{
                                                                //    if (_Provider.SSN.ToString().Trim().Replace("*", "") != "")
                                                                //    {
                                                                //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                                //        oSegment.set_DataElementValue(1, 0, "SY");//// Social Security Number
                                                                //        oSegment.set_DataElementValue(2, 0, _Provider.SSN.ToString().Trim().Replace("*", ""));//"1039255");// 
                                                                //    }
                                                                //}
                                                            }
                                                        }


                                                        //oReferral = oPatient.Referrals;
                                                        //if (oReferral.Count > 0)
                                                        //{
                                                        //    oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                                        //    dtReferral = new DataTable();
                                                        //    string _sqlQuery = "";

                                                        //    oDB.Connect(false);
                                                        //    _sqlQuery = " SELECT sStreet, sCity, sState, sZIP, sFirstName, sMiddleName, sLastName, sGender, nSpecialtyID, sTaxID, sUPIN, sNPI, sContactType, sTaxonomy, sTaxonomyDesc, nContactID " +
                                                        //                " FROM Contacts_MST  " +
                                                        //                " WHERE (nContactID = " + oReferral[0].ReferralID + ") AND (sContactType = 'Referral')";
                                                        //    oDB.Retrive_Query(_sqlQuery, out dtReferral);
                                                        //    if (dtReferral != null && dtReferral.Rows.Count > 0)
                                                        //    {
                                                        //        //2310B Referring PROVIDER
                                                        //        //NM1 Referring PROVIDER NAME
                                                        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                        //        oSegment.set_DataElementValue(1, 0, "DN");
                                                        //        oSegment.set_DataElementValue(2, 0, "1");
                                                        //        oSegment.set_DataElementValue(3, 0, dtReferral.Rows[0]["sLastName"].ToString().Trim().Replace("*", "")); //"ReferringLastname"
                                                        //        oSegment.set_DataElementValue(4, 0, dtReferral.Rows[0]["sFirstName"].ToString().Trim().Replace("*", ""));//"ReferringFirstname"
                                                        //        oSegment.set_DataElementValue(5, 0, dtReferral.Rows[0]["sMiddleName"].ToString().Trim().Replace("*", ""));
                                                        //        oSegment.set_DataElementValue(8, 0, "XX");
                                                        //        oSegment.set_DataElementValue(9, 0, dtReferral.Rows[0]["sNPI"].ToString().Trim().Replace("*", ""));//"NPI"

                                                        //        //PRV REFERRING PROVIDER INFORMATION
                                                        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                        //        oSegment.set_DataElementValue(1, 0, "RF");
                                                        //        oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                        //        oSegment.set_DataElementValue(3, 0, dtReferral.Rows[0]["sTaxonomy"].ToString().Trim().Replace("*", ""));//Reference Identification

                                                        //        //REF
                                                        //        if (dtReferral.Rows[0]["sTaxID"].ToString().Trim().Replace("*", "") != "")
                                                        //        {
                                                        //            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                        //            oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
                                                        //            oSegment.set_DataElementValue(2, 0, dtReferral.Rows[0]["sTaxID"].ToString().Trim().Replace("*", ""));//"1039255");// 
                                                        //        }
                                                        //        else //if (_ReferralSSN.Trim().Replace("*","") != "")
                                                        //        {
                                                        //            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                        //            oSegment.set_DataElementValue(1, 0, "SY");//// Social Security Number
                                                        //            oSegment.set_DataElementValue(2, 0, "232929");//"1039255");// 
                                                        //        }
                                                        //    }
                                                        //}
                                                        #endregion Referring Provider

                                                        #region Rendering Provider - 2310B

                                                        _Provider = null;
                                                        _Provider = oResource.GetProviderDetail(oTransaction.Lines[0].RefferingProviderId);
                                                        if (_Provider != null)
                                                        {
                                                            //2310B RENDERING PROVIDER
                                                            //NM1 RENDERING PROVIDER NAME
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                            oSegment.set_DataElementValue(1, 0, "82");
                                                            oSegment.set_DataElementValue(2, 0, "1");
                                                            //FillProviderDetails(oTransaction.Lines[0].RefferingProviderId, ProviderType.RenderingProvider);
                                                            oSegment.set_DataElementValue(3, 0, _Provider.LastName.Trim().Replace("*", ""));//Billing provider name
                                                            oSegment.set_DataElementValue(4, 0, _Provider.FirstName.Trim().Replace("*", ""));
                                                            oSegment.set_DataElementValue(5, 0, _Provider.MiddleName.Trim().Replace("*", ""));
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, _Provider.NPI.Trim().Replace("*", ""));//oProviderDetails.NPI);//Billing provider ID/NPI


                                                            //PRV RENDERING PROVIDER INFORMATION
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                            oSegment.set_DataElementValue(1, 0, "PE");
                                                            oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                            oSegment.set_DataElementValue(3, 0, _Provider.Taxonomy.Trim().Replace("*", ""));//Reference Identification
                                                        }


                                                        #endregion

                                                        #region Facility - 2310D

                                                        //2310D SERVICE LOCATION
                                                        //NM1 SERVICE FACILITY LOCATION
                                                        if (dtFacility != null && dtFacility.Rows.Count > 0)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
                                                            oSegment.set_DataElementValue(1, 0, "77");
                                                            oSegment.set_DataElementValue(2, 0, "2");
                                                            oSegment.set_DataElementValue(3, 0, dtFacility.Rows[0]["FacilityName"].ToString().Trim().Replace("*", ""));//"FacilityName"
                                                            oSegment.set_DataElementValue(8, 0, "XX");//NPI code
                                                            oSegment.set_DataElementValue(9, 0, dtFacility.Rows[0]["FacilityNPI"].ToString().Trim().Replace("*", ""));//NPI

                                                            //N3 SERVICE FACILITY ADDRESS
                                                            //* _FirstPOS=12 then don't provide this 
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
                                                            oSegment.set_DataElementValue(1, 0, dtFacility.Rows[0]["FacilityAddress1"].ToString().Trim().Replace("*", ""));//"FacilityAddr"

                                                            //N4 SERVICE FACILITY CITY/STATE/ZIP
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
                                                            oSegment.set_DataElementValue(1, 0, dtFacility.Rows[0]["FacilityCity"].ToString().Trim().Replace("*", ""));//"FacilityCity"
                                                            oSegment.set_DataElementValue(2, 0, dtFacility.Rows[0]["FacilityState"].ToString().Trim().Replace("*", ""));//"FacilityState"
                                                            oSegment.set_DataElementValue(3, 0, dtFacility.Rows[0]["FacilityZip"].ToString().Trim().Replace("*", ""));//"FacilityZip"
                                                        }
                                                        #endregion

                                                        for (int _Insrow = 0; _Insrow < dtPatientInsurances.Rows.Count; _Insrow++)
                                                        {
                                                            #region Subscriber Secondary Insurance - Loop 2320

                                                            //LOOP - 2320
                                                            if (_Insrow == 1)
                                                            {

                                                                #region SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR"));
                                                                //1.Payer Resposibilty Sequence No.
                                                                oSegment.set_DataElementValue(1, 0, "S");//_OtherInsurancePST.Trim().Replace("*","")); //P - Primary

                                                                //2.Individual Relationship code
                                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["RelationshipCode"]).Trim().Replace("*", ""));//"18"); // Hard coded(Individual Relationship code) 18 - Self

                                                                //3.Refrence identification
                                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sGroup"]).Trim().Replace("*", ""));//"22145");///Policy no

                                                                //5.Insurance Type Code
                                                                oSegment.set_DataElementValue(5, 0, "C1"); // C1 - Commercial (Insurance Type Code)

                                                                /*5 and 9 same?*/
                                                                //oSegment.set_DataElementValue(6, 0, "6"); // 6 - No Co-ordination of Benefit

                                                                ////8.Employment Status Code(Not Used)
                                                                //oSegment.set_DataElementValue(8, 0, "AC"); // Employment status (AC - Active)

                                                                //9.Claim Filing Indicator
                                                  /*              oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceTypeCode"]).Trim().Replace("*", "")); //Commercial Insurance company

                                                                #endregion SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                                #region CAS - Claim Adjustment

                                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\CAS"));
                                                                //oSegment.set_DataElementValue(1, 0, "PI");//PR - Patient Responsibility
                                                                //oSegment.set_DataElementValue(2, 0, "96");
                                                                //oSegment.set_DataElementValue(3, 0, "300");

                                                                #endregion CAS - Claim Adjustment

                                                                #region AMT - Amount

                                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                                //oSegment.set_DataElementValue(1, 0, "D");
                                                                //oSegment.set_DataElementValue(2, 0, "0");

                                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                                //oSegment.set_DataElementValue(1, 0, "F2");
                                                                //oSegment.set_DataElementValue(2, 0, "100");

                                                                #endregion AMT - Amount

                                                                #region MOA - Medicare Outpatient Adjudication

                                                                //ediDataSegment.Set(ref oSegment,(ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\MOA"));
                                                                //oSegment.set_DataElementValue(1,0,"20");
                                                                //oSegment.set_DataElementValue(2,0,"300");
                                                                //oSegment.set_DataElementValue(3,0,"125");

                                                                #endregion

                                                                #region DMG  - Demographic

                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\DMG"));
                                                                oSegment.set_DataElementValue(1, 0, "D8");
                                                                oSegment.set_DataElementValue(2, 0, gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["dtDOB"])).ToString());//"SubscriberDOB"
                                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberGender"]));//"SubscriberGender"

                                                                #endregion DMG  - Demographic

                                                                #region OI - Other Insurance

                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\OI"));
                                                                oSegment.set_DataElementValue(3, 0, "Y");
                                                                oSegment.set_DataElementValue(4, 0, "C");
                                                                oSegment.set_DataElementValue(6, 0, "Y");

                                                                #endregion OI - Other Insurance

                                                                //2330A SUBSCRIBER
                                                                #region NM1 SUBSCRIBER NAME - 2330A

                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                                oSegment.set_DataElementValue(1, 0, "IL");
                                                                oSegment.set_DataElementValue(2, 0, "1");
                                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", ""));//"SubscriberLastOrgName"
                                                                oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubFName"]).Trim().Replace("*", ""));//"SubscriberFirstname"
                                                                oSegment.set_DataElementValue(8, 0, "MI");
                                                                /*Other Insured Identifier*/
                                                            /*    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", ""));//"SubscriberMemberID"

                                                                //N3 SUBSCRIBER ADDRESS
                               /*                                 ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N3"));
                                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr1"]).Trim().Replace("*", ""));//"SubscriberAddress"

                                                                //N4 SUBSCRIBER CITY
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N4"));
                                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberCity"]).Trim().Replace("*", ""));//"SubscriberCity"
                                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberState"]).Trim().Replace("*", ""));//"SubscrberState"
                                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberZip"]).Trim().Replace("*", ""));//"SubscriberZip"

                                                                #endregion NM1 SUBSCRIBER NAME

                                                                #region Payer Information - 2330B

                                                                //2330B SUBSCRIBER/PAYER
                                                                //NM1 PAYER NAME
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                                oSegment.set_DataElementValue(1, 0, "PR");
                                                                oSegment.set_DataElementValue(2, 0, "2");

                                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceName"]).Trim().Replace("*", ""));//dtInsurance.Rows[0]["sSubscriberName"].ToString().Trim().Replace("*",""));//"PayerLastOrgName"

                                                                oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                                oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerID"]).Trim().Replace("*", ""));//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID
                                                                //}

                                                                if (_SecondayInsuranceAddressDetailsRequired)
                                                                {
                                                                    ////////N3 PAYER ADDRESS
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N3"));
                                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerAddress1"]).Trim().Replace("*", ""));//"InsuranceAddress"

                                                                    ////////N4 PAYER CITY
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N4"));
                                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerCity"]).Trim().Replace("*", ""));//"InsuranceCity"
                                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerState"]).Trim().Replace("*", ""));//"InsuranceState"
                                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerZip"]).Trim().Replace("*", ""));//"InsuranceZip"
                                                                }
                                                                #endregion Payer Information

                                                            }

                                                            #endregion Subscriber Secondary Insurance
                                                        }//End for loop of Patient Insurance 
                                                        //}//end of IF loop for POS
                                                        for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                        {
                                                            iItemCount = 1;
                                                            iItemCount = iItemCount + nLine;
                                                            oTransLine = new TransactionLine();
                                                            oTransLine = oTransaction.Lines[nLine];

                                                            #region Service Line
                                                            //******* SUBSCRIBER SERVICE LINE *************************************************************
                                                            //TODO: Get the datatable for service info to add fields of service in EDI file.
                                                            //2400 SERVICE LINE
                                                            sInstance = iItemCount.ToString().Trim().Replace("*", "");
                                                            //LX SERVICE LINE COUNTER
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                            oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                            //SV1 PROFESSIONAL SERVICE
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
                                                            oSegment.set_DataElementValue(1, 1, "HC");
                                                            oSegment.set_DataElementValue(1, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
                                                            if (oTransLine.Mod1Code.ToString().Trim().Replace("*", "") != "")
                                                            {
                                                                oSegment.set_DataElementValue(1, 3, oTransLine.Mod1Code.ToString());//Modifier 1
                                                            }
                                                            if (oTransLine.Mod2Code.ToString().Trim().Replace("*", "") != "")
                                                            {
                                                                if (oTransLine.Mod1Code.ToString().Trim().Replace("*", "") == "")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 3, oTransLine.Mod2Code.ToString());
                                                                }
                                                                else
                                                                {
                                                                    oSegment.set_DataElementValue(1, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                                }
                                                            }
                                                            string _ClaimLineCharges = Convert.ToString(oTransLine.Total);

                                                            if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                            {
                                                                _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                            }
                                                            else if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 1, 1) == "0")
                                                            {
                                                                _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 1);
                                                            }
                                                            oSegment.set_DataElementValue(2, 0, _ClaimLineCharges);//"ServiceAmount"
                                                            oSegment.set_DataElementValue(3, 0, "UN");//UN stands for UNITS
                                                            oSegment.set_DataElementValue(4, 0, oTransLine.Unit.ToString());//Unit/Quantity

                                                            if (dtDx != null && dtDx.Rows.Count > 0)
                                                            {
                                                                int _CompTerminatorPos = 0;

                                                                for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                                {
                                                                    if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "") == oTransaction.Lines[nLine].Dx1Code.Trim().Replace("*", ""))
                                                                    {
                                                                        _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                        oSegment.set_DataElementValue(7, _CompTerminatorPos, "1");
                                                                    }
                                                                    if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "") == oTransaction.Lines[nLine].Dx2Code.Trim().Replace("*", ""))
                                                                    {
                                                                        _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                        oSegment.set_DataElementValue(7, _CompTerminatorPos, "2");
                                                                    }
                                                                    if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "") == oTransaction.Lines[nLine].Dx3Code.Trim().Replace("*", ""))
                                                                    {
                                                                        _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                        oSegment.set_DataElementValue(7, _CompTerminatorPos, "3");
                                                                    }
                                                                    if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "") == oTransaction.Lines[nLine].Dx4Code.Trim().Replace("*", ""))
                                                                    {
                                                                        _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                        oSegment.set_DataElementValue(7, _CompTerminatorPos, "4");
                                                                    }
                                                                }
                                                            }



                                                            //DTP DATE - SERVICE DATE(S)
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                            oSegment.set_DataElementValue(1, 0, "472");
                                                            oSegment.set_DataElementValue(2, 0, "D8");
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"

                                                            #endregion

                                                            #region " CLIA (Clinical Laboratory Improvement Amendment Number) "
                                                            if (oTransaction.Lines[nLine].AuthorizationNo.Trim() != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX\\REF"));
                                                                oSegment.set_DataElementValue(1, 0, "X4"); //Clinical Laboratory Improvement Amendment Number
                                                                oSegment.set_DataElementValue(2, 0, oTransaction.Lines[nLine].AuthorizationNo.Trim());//
                                                            }
                                                            #endregion " CLIA (Clinical Laboratory Improvement Amendment Number) "
                                                        }
                                                #endregion " Subscriber "

                                                    }//end of if loop for Subscriber as Patient
                                                    else
                                                    {
                                                        #region "Dependent Loop"

                                                        //////*****************************************************************************************************
                                                        //////******* DEPENDENT HIERARCHICAL LEVEL ****************************************************************
                                                        //////*****************************************************************************************************
                                                        ////TODO: Get the datatable for dependent info to add fields of service in EDI file.

                                                        #region Payer Information Loop 2010BB
                                                        //2010BB SUBSCRIBER/PAYER
                                                        //NM1 PAYER NAME
                                                        string _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "");
                                                        if (Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Length > 35)
                                                        {
                                                            _ModifiedPayerName = "";
                                                            _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Replace("*", "").Substring(0, 34);
                                                        }
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "PR");
                                                        oSegment.set_DataElementValue(2, 0, "2");
                                                        oSegment.set_DataElementValue(3, 0, _ModifiedPayerName.Trim().Replace("*", ""));//"PayerLastOrgName"
                                                        oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim().Replace("*", ""));//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID

                                                        ////////N3 PAYER ADDRESS
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N3"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress1"]).Trim().Replace("*", ""));//"InsuranceAddress"

                                                        ////////N4 PAYER CITY
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N4"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerCity"]).Trim().Replace("*", ""));//"InsuranceCity"
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerState"]).Trim().Replace("*", ""));//"InsuranceState"
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerZip"]).Trim().Replace("*", ""));//"InsuranceZip"

                                                        #endregion

                                                        nHlCount = nHlCount + 1;

                                                        //2000B DEPENDENT HL LOOP
                                                        //HL-DEPENDENT
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                        oSegment.set_DataElementValue(1, 0, nHlCount.ToString());
                                                        oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());
                                                        oSegment.set_DataElementValue(3, 0, "23");
                                                        oSegment.set_DataElementValue(4, 0, "0");

                                                        //PAT - PATIENT/DEPENDENT INFORMATION
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\PAT"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim().Replace("*", "")); //01 - Spouse 19 - Child

                                                        #region " Patient Info"

                                                        //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "QC");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, oPatient.DemographicsDetail.PatientLastName.Trim().Replace("*", ""));//Patient Last Name
                                                        oSegment.set_DataElementValue(4, 0, oPatient.DemographicsDetail.PatientFirstName.Trim().Replace("*", ""));//Patient First Name

                                                        //N3 - ADDRESS INFORMATION
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                        oSegment.set_DataElementValue(1, 0, oPatient.DemographicsDetail.PatientAddress1.Trim().Replace("*", ""));//"Address"

                                                        //N4 - GEOGRAPHIC LOCATION
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                        oSegment.set_DataElementValue(1, 0, oPatient.DemographicsDetail.PatientCity.Trim().Replace("*", ""));//"City"
                                                        oSegment.set_DataElementValue(2, 0, oPatient.DemographicsDetail.PatientState.Trim().Replace("*", ""));//"State"
                                                        oSegment.set_DataElementValue(3, 0, oPatient.DemographicsDetail.PatientZip.Trim().Replace("*", ""));//"Zip"

                                                        //DMG - DEMOGRAPHIC INFORMATION
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                                        oSegment.set_DataElementValue(1, 0, "D8");
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oPatient.DemographicsDetail.PatientDOB.ToShortDateString())));
                                                        oSegment.set_DataElementValue(3, 0, oPatient.DemographicsDetail.PatientGender.Trim().Replace("*", ""));

                                                        #endregion " Patient Info"

                                                        //******* DEPENDENT CLAIM INFORMATION *************************************************************
                                                        //TODO: Get the datatable for Claim info to add fields of service in EDI file
                                                        string _FirstPOS = "";
                                                        string _NewPOS = "";
                                                        string _ClaimTotal = "";
                                                        iItemCount = 0;
                                                        iItemCount = 1;
                                                        decimal _claimAmount = 0;
                                                        for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                        {
                                                            oTransLine = new TransactionLine();
                                                            oTransLine = oTransaction.Lines[nLine];
                                                            _claimAmount = _claimAmount + oTransLine.Total;

                                                            _FirstPOS = oTransaction.Lines[0].POSCode;
                                                            _NewPOS = oTransLine.POSCode;
                                                        }
                                                        _ClaimTotal = _claimAmount.ToString("#0.00");
                                                        if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                                        {
                                                            _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                                        }
                                                        else if (_ClaimTotal.Substring(_ClaimTotal.Length - 1, 1) == "0")
                                                        {
                                                            _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 1);
                                                        }

                                                        #region "Dependent Claim Level"
                                                        //2300 CLAIM
                                                        //CLM CLAIM LEVEL INFORMATION
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
                                                        oSegment.set_DataElementValue(1, 0, FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNo).Trim().Replace("*", ""))); //Patient Account no         
                                                        oSegment.set_DataElementValue(2, 0, _ClaimTotal.Trim().Replace("*", "")); //Claim Amount
                                                        oSegment.set_DataElementValue(5, 1, _FirstPOS.Trim().Replace("*", "")); //21 - Inpatient Hospital

                                                        if (oTransaction.Transaction_Status == TransactionStatus.Batch)
                                                        {
                                                            _ClaimStatus = "1";
                                                        }
                                                        else if (oTransaction.Transaction_Status == TransactionStatus.ReBatch)
                                                        {
                                                            _ClaimStatus = "6";
                                                        }
                                                        oSegment.set_DataElementValue(5, 3, _ClaimStatus);
                                                        oSegment.set_DataElementValue(6, 0, "Y");
                                                        oSegment.set_DataElementValue(7, 0, "A");
                                                        oSegment.set_DataElementValue(8, 0, "Y");
                                                        oSegment.set_DataElementValue(9, 0, "Y");
                                                        oSegment.set_DataElementValue(10, 0, "C");

                                                        string OnsetDate = "";
                                                        if (oTransaction.InjuryDate.ToString() != "" || oTransaction.OnsiteDate.ToString() != "")
                                                        {
                                                            if (oTransaction.InjuryDate.ToString() != "" && oTransaction.InjuryDate > 0)
                                                            {
                                                                OnsetDate = Convert.ToString(oTransaction.InjuryDate);
                                                                ////DTP DATE OF ONSET of current symptoms or illness
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                oSegment.set_DataElementValue(1, 0, "431");
                                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                                oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                            }
                                                            else if (oTransaction.OnsiteDate.ToString() != "" && oTransaction.OnsiteDate > 0)
                                                            {
                                                                OnsetDate = Convert.ToString(oTransaction.OnsiteDate);
                                                                ////DTP DATE OF ONSET of current symptoms or illness
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                oSegment.set_DataElementValue(1, 0, "431");
                                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                                oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                            }

                                                            if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
                                                            {
                                                                OnsetDate = Convert.ToString(oTransaction.AccidentDate);
                                                                ////DTP DATE OF ACCIDENT 
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                oSegment.set_DataElementValue(1, 0, "439");
                                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                                oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                            }
                                                        }

                                                        //DTP DATE OF ONSET of similar symptoms or illness
                                                        if (oTransaction.OnsiteDate > 0 && oTransaction.OnsiteDate.ToString() != "")
                                                        {
                                                            //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                            //oSegment.set_DataElementValue(1, 0, "438");
                                                            //oSegment.set_DataElementValue(2, 0, "D8");
                                                            //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransaction.OnsiteDate.ToString())).Trim().Replace("*",""));
                                                        }
                                                        //
                                                        if (_FirstPOS.Trim().Replace("*", "") != "11")
                                                        {
                                                            if (oTransaction.HospitalizationDateFrom > 0 && oTransaction.HospitalizationDateFrom.ToString() != "")
                                                            {
                                                                //DTP DATE OF Hospitalization (Admission) 
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                oSegment.set_DataElementValue(1, 0, "435");
                                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                                oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateFrom.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalizationDate.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                            }

                                                            if (oTransaction.HospitalizationDateTo > 0 && oTransaction.HospitalizationDateTo.ToString() != "")
                                                            {
                                                                ////DTP DATE OF Discharge 
                                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                //oSegment.set_DataElementValue(1, 0, "096");
                                                                //oSegment.set_DataElementValue(2, 0, "D8");
                                                                //oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateTo.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalDischargeDate.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                                ////
                                                            }
                                                        }
                                                        if (oTransaction.WorkersComp == true)
                                                        {
                                                            if (oTransaction.UnableToWorkFromDate > 0 && oTransaction.UnableToWorkFromDate.ToString() != "")
                                                            {
                                                                //DTP DATE OF (Intial Disability period last day worked)
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                oSegment.set_DataElementValue(1, 0, "297");
                                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                                oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkFromDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpLastDayWorked.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                                //
                                                            }

                                                            if (oTransaction.UnableToWorkTillDate > 0 && oTransaction.UnableToWorkTillDate.ToString() != "")
                                                            {
                                                                //DTP DATE OF (Intial Disability period return to work)
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                                oSegment.set_DataElementValue(1, 0, "296");
                                                                oSegment.set_DataElementValue(2, 0, "D8");
                                                                oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkTillDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpReturnToWork.Value.ToShortDateString())).Trim().Replace("*",""));     //Claim Date
                                                                //
                                                            }
                                                        }

                                                        //REF CLEARING HOUSE CLAIM NUMBER
                                                        if (GetPriorAuthorizationNumber(oTransaction.PatientID, Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"])).Trim().Replace("*", "") != "")
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, "G1");
                                                            oSegment.set_DataElementValue(2, 0, GetPriorAuthorizationNumber(oTransaction.PatientID, Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"])).Trim().Replace("*", "")); //Claim No
                                                        }
                                                        #endregion "Dependent Claim Level"


                                                        #region HI - Diagnosis for Dependent
                                                        //HI HEALTH CARE DIAGNOSIS CODES

                                                        DataTable dtDx = new DataTable();
                                                        dtDx = GetDistinctDiagnosis(oTransaction.TransactionID, oTransaction.ClinicID, oTransaction.ClaimNo);

                                                        if (dtDx != null && dtDx.Rows.Count > 0)
                                                        {


                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));

                                                            for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                            {
                                                                if (DxIndex == 0)
                                                                {
                                                                    if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "") != "")
                                                                    {
                                                                        if (IsValidICD9(Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "")) == false)
                                                                        {
                                                                            return "";
                                                                        }
                                                                        oSegment.set_DataElementValue(1, 1, "BK");
                                                                        oSegment.set_DataElementValue(DxIndex + 1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Replace(".", "").Trim().Replace("*", ""));
                                                                    }
                                                                }
                                                                if (DxIndex > 0)
                                                                {
                                                                    if (Convert.ToString(dtDx.Rows[DxIndex][0]).Trim().Replace("*", "") != "")
                                                                    {
                                                                        if (IsValidICD9(Convert.ToString(Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", ""))) == false)
                                                                        {
                                                                            return "";
                                                                        }
                                                                        oSegment.set_DataElementValue(DxIndex + 1, 1, "BF");
                                                                        oSegment.set_DataElementValue(DxIndex + 1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).ToString().Replace(".", "").Trim().Replace("*", ""));//
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        #endregion

                                                        #region Referring Provider - 2310A

                                                        if (oTransaction.ReferralProviderID > 0)
                                                        {
                                                            oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                                            dtReferral = new DataTable();
                                                            string _sqlQuery = "";

                                                            oDB.Connect(false);

                                                            _sqlQuery = " SELECT ISNULL(nContactId,0) AS nContactId, " +
                                                                        " ISNULL(sName,'') AS sName,  " +
                                                                        " ISNULL(sContact,'') AS sContact,   " +
                                                                        " ISNULL(sAddressLine1,'') AS sAddressLine1,   " +
                                                                        " ISNULL(sAddressLine2,'') AS sAddressLine2,   " +
                                                                        " ISNULL(sCity,'') AS sCity,   " +
                                                                        " ISNULL(sState,'') AS sState,   " +
                                                                        " ISNULL(sZIP,'') AS sZIP,   " +
                                                                        " ISNULL(sPhone,'') AS sPhone,   " +
                                                                        " ISNULL(sFax,'') AS sFax,   " +
                                                                        " ISNULL(sEmail,'') AS sEmail,   " +
                                                                        " ISNULL(sURL,'') AS sURL,   " +
                                                                        " ISNULL(sMobile,'') AS sMobile,   " +
                                                                        " ISNULL(sPager,'') AS sPager,   " +
                                                                        " ISNULL(sNotes,'') AS sNotes,   " +
                                                                        " ISNULL(sFirstName,'') AS sFirstName,   " +
                                                                        " ISNULL(sMiddleName,'') AS sMiddleName,   " +
                                                                        " ISNULL(sLastName,'') AS sLastName,   " +
                                                                        " ISNULL(sGender,'') AS sGender,   " +
                                                                        " ISNULL(sTaxonomy,'') AS sTaxonomy,   " +
                                                                        " ISNULL(sTaxonomyDesc,'') AS sTaxonomyDesc,   " +
                                                                        " ISNULL(sTaxID,'') AS sTaxID,   " +
                                                                        " ISNULL(sUPIN,'') AS sUPIN,   " +
                                                                        " ISNULL(sNPI,'') AS sNPI,   " +
                                                                        " ISNULL(sDegree,'') AS sDegree   " +
                                                                        " FROM  Patient_DTL " +
                                                                        " WHERE (nContactFlag = 3) AND (nPatientID = " + oTransaction.PatientID + ") AND (nPatientDetailID = " + oTransaction.ReferralProviderID + ") AND ISNULL(nClinicID,1)=" + ClinicID + "";

                                                            oDB.Retrive_Query(_sqlQuery, out dtReferral);
                                                            if (dtReferral != null && dtReferral.Rows.Count > 0)
                                                            {
                                                                //2310B Referring PROVIDER
                                                                //NM1 Referring PROVIDER NAME
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                                oSegment.set_DataElementValue(1, 0, "DN");
                                                                oSegment.set_DataElementValue(2, 0, "1");
                                                                oSegment.set_DataElementValue(3, 0, dtReferral.Rows[0]["sLastName"].ToString().Trim().Replace("*", "")); //"ReferringLastname"
                                                                oSegment.set_DataElementValue(4, 0, dtReferral.Rows[0]["sFirstName"].ToString().Trim().Replace("*", ""));//"ReferringFirstname"
                                                                oSegment.set_DataElementValue(5, 0, dtReferral.Rows[0]["sMiddleName"].ToString().Trim().Replace("*", ""));
                                                                oSegment.set_DataElementValue(8, 0, "XX");
                                                                oSegment.set_DataElementValue(9, 0, dtReferral.Rows[0]["sNPI"].ToString().Trim().Replace("*", ""));//"NPI"

                                                                //PRV REFERRING PROVIDER INFORMATION
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                                oSegment.set_DataElementValue(1, 0, "RF");
                                                                oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                                oSegment.set_DataElementValue(3, 0, dtReferral.Rows[0]["sTaxonomy"].ToString().Trim().Replace("*", ""));//Reference Identification

                                                                ////REF
                                                                //if (dtReferral.Rows[0]["sTaxID"].ToString().Trim().Replace("*", "") != "")
                                                                //{
                                                                //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                                //    oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
                                                                //    oSegment.set_DataElementValue(2, 0, dtReferral.Rows[0]["sTaxID"].ToString().Trim().Replace("*", ""));//"1039255");// 
                                                                //}
                                                                //else //if (_ReferralSSN.Trim().Replace("*","") != "")
                                                                //{
                                                                //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                                //    oSegment.set_DataElementValue(1, 0, "SY");//// Social Security Number
                                                                //    oSegment.set_DataElementValue(2, 0, dtReferral.Rows[0]["sTaxID"].ToString().Trim().Replace("*", ""));//"1039255");// 
                                                                //}
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (_Provider != null)
                                                            {
                                                                //2310B Referring PROVIDER
                                                                //NM1 Referring PROVIDER NAME
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                                oSegment.set_DataElementValue(1, 0, "DN");
                                                                oSegment.set_DataElementValue(2, 0, "1");
                                                                oSegment.set_DataElementValue(3, 0, _Provider.LastName.ToString().Trim().Replace("*", "")); //"ReferringLastname"
                                                                oSegment.set_DataElementValue(4, 0, _Provider.FirstName.ToString().Trim().Replace("*", ""));//"ReferringFirstname"
                                                                oSegment.set_DataElementValue(5, 0, _Provider.MiddleName.ToString().Trim().Replace("*", ""));
                                                                oSegment.set_DataElementValue(8, 0, "XX");
                                                                oSegment.set_DataElementValue(9, 0, _Provider.NPI.ToString().Trim().Replace("*", ""));//"NPI"

                                                                //PRV REFERRING PROVIDER INFORMATION
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                                oSegment.set_DataElementValue(1, 0, "RF");
                                                                oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                                oSegment.set_DataElementValue(3, 0, _Provider.Taxonomy.ToString().Trim().Replace("*", ""));//Reference Identification

                                                                //REF
                                                                //if (_Provider.EmployerID.ToString().Trim().Replace("*", "") != "")
                                                                //{
                                                                //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                                //    oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
                                                                //    oSegment.set_DataElementValue(2, 0, _Provider.EmployerID.ToString().Trim().Replace("*", ""));//"1039255");// 
                                                                //}
                                                                //else //if (_ReferralSSN.Trim().Replace("*","") != "")
                                                                //{
                                                                //    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                                //    oSegment.set_DataElementValue(1, 0, "SY");//// Social Security Number
                                                                //    oSegment.set_DataElementValue(2, 0, _Provider.SSN);//"1039255");// 
                                                                //}
                                                            }
                                                        }

                                                        //oReferral = oPatient.Referrals;
                                                        //if (oReferral.Count > 0)
                                                        //{
                                                        //    oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                                        //    dtReferral = new DataTable();
                                                        //    string _sqlQuery = "";

                                                        //    oDB.Connect(false);
                                                        //    _sqlQuery = " SELECT sStreet, sCity, sState, sZIP, sFirstName, sMiddleName, sLastName, sGender, nSpecialtyID, sTaxID, sUPIN, sNPI, sContactType, sTaxonomy, sTaxonomyDesc, nContactID " +
                                                        //                " FROM Contacts_MST  " +
                                                        //                " WHERE (nContactID = " + oReferral[0].ReferralID + ") AND (sContactType = 'Referral')";
                                                        //    oDB.Retrive_Query(_sqlQuery, out dtReferral);
                                                        //    if (dtReferral != null && dtReferral.Rows.Count > 0)
                                                        //    {
                                                        //        //2310B Referring PROVIDER
                                                        //        //NM1 Referring PROVIDER NAME
                                                        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                        //        oSegment.set_DataElementValue(1, 0, "DN");
                                                        //        oSegment.set_DataElementValue(2, 0, "1");
                                                        //        oSegment.set_DataElementValue(3, 0, dtReferral.Rows[0]["sLastName"].ToString().Trim().Replace("*", "")); //"ReferringLastname"
                                                        //        oSegment.set_DataElementValue(4, 0, dtReferral.Rows[0]["sFirstName"].ToString().Trim().Replace("*", ""));//"ReferringFirstname"
                                                        //        oSegment.set_DataElementValue(5, 0, dtReferral.Rows[0]["sMiddleName"].ToString().Trim().Replace("*", ""));
                                                        //        oSegment.set_DataElementValue(8, 0, "XX");
                                                        //        oSegment.set_DataElementValue(9, 0, dtReferral.Rows[0]["sNPI"].ToString().Trim().Replace("*", ""));//"NPI"

                                                        //        //PRV REFERRING PROVIDER INFORMATION
                                                        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                        //        oSegment.set_DataElementValue(1, 0, "RF");
                                                        //        oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                        //        oSegment.set_DataElementValue(3, 0, dtReferral.Rows[0]["sTaxonomy"].ToString().Trim().Replace("*", ""));//Reference Identification

                                                        //        //REF
                                                        //        if (dtReferral.Rows[0]["sTaxID"].ToString().Trim().Replace("*", "") != "")
                                                        //        {
                                                        //            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                        //            oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
                                                        //            oSegment.set_DataElementValue(2, 0, dtReferral.Rows[0]["sTaxID"].ToString().Trim().Replace("*", ""));//"1039255");// 
                                                        //        }
                                                        //        else //if (_ReferralSSN.Trim() != "")
                                                        //        {
                                                        //            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                        //            oSegment.set_DataElementValue(1, 0, "SY");//// Social Security Number
                                                        //            oSegment.set_DataElementValue(2, 0, "32432432");//dtReferral.Rows[0]["sTaxID"].ToString().Trim().Replace("*",""));//"1039255");// 
                                                        //        }
                                                        //    }
                                                        //}
                                                        #endregion Referring Provider

                                                        #region Rendering Provider - 2310B

                                                        _Provider = null;
                                                        _Provider = oResource.GetProviderDetail(oTransaction.Lines[0].RefferingProviderId);
                                                        if (_Provider != null)
                                                        {

                                                            //2310B RENDERING PROVIDER
                                                            //NM1 RENDERING PROVIDER NAME
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                            oSegment.set_DataElementValue(1, 0, "82");
                                                            oSegment.set_DataElementValue(2, 0, "1");
                                                            //FillProviderDetails(oTransaction.Lines[0].RefferingProviderId, ProviderType.RenderingProvider);
                                                            oSegment.set_DataElementValue(3, 0, _Provider.LastName.Trim().Replace("*", ""));//Billing provider name
                                                            oSegment.set_DataElementValue(4, 0, _Provider.FirstName.Trim().Replace("*", ""));
                                                            oSegment.set_DataElementValue(5, 0, _Provider.MiddleName.Trim().Replace("*", ""));
                                                            oSegment.set_DataElementValue(8, 0, "XX");
                                                            oSegment.set_DataElementValue(9, 0, _Provider.NPI.Trim().Replace("*", ""));//oProviderDetails.NPI);//Billing provider ID/NPI


                                                            //PRV RENDERING PROVIDER INFORMATION
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                            oSegment.set_DataElementValue(1, 0, "PE");
                                                            oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                            oSegment.set_DataElementValue(3, 0, _Provider.Taxonomy.Trim().Replace("*", ""));//Reference Identification
                                                        }

                                                        #endregion

                                                        #region Facility - 2310D

                                                        //2310D SERVICE LOCATION
                                                        //NM1 SERVICE FACILITY LOCATION
                                                        if (dtFacility != null && dtFacility.Rows.Count > 0)
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
                                                            oSegment.set_DataElementValue(1, 0, "77");
                                                            oSegment.set_DataElementValue(2, 0, "2");
                                                            oSegment.set_DataElementValue(3, 0, dtFacility.Rows[0]["FacilityName"].ToString().Trim().Replace("*", ""));//"FacilityName"
                                                            oSegment.set_DataElementValue(8, 0, "XX");//NPI code
                                                            /*Do not report service/facility  */
                    /*                                        oSegment.set_DataElementValue(9, 0, dtFacility.Rows[0]["FacilityNPI"].ToString().Trim().Replace("*", ""));//NPI
                                                            /*location when 2300 CLM5-1 is equal to 12*/
                                                            /*(home) _FirstPOS!=12 */
                                                            //N3 SERVICE FACILITY ADDRESS
                           /*                                  ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
                                                           oSegment.set_DataElementValue(1, 0, dtFacility.Rows[0]["FacilityAddress1"].ToString().Trim().Replace("*", ""));//"FacilityAddr"

                                                            //N4 SERVICE FACILITY CITY/STATE/ZIP
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
                                                            oSegment.set_DataElementValue(1, 0, dtFacility.Rows[0]["FacilityCity"].ToString().Trim().Replace("*", ""));//"FacilityCity"
                                                            oSegment.set_DataElementValue(2, 0, dtFacility.Rows[0]["FacilityState"].ToString().Trim().Replace("*", ""));//"FacilityState"
                                                            oSegment.set_DataElementValue(3, 0, dtFacility.Rows[0]["FacilityZip"].ToString().Trim().Replace("*", ""));//"FacilityZip"
                                                        }
                                                        #endregion

                                                        for (int _Insrow = 0; _Insrow < dtPatientInsurances.Rows.Count; _Insrow++)
                                                        {
                                                            #region Subscriber Secondary Insurance - Loop 2320

                                                            //LOOP - 2320
                                                            if (_Insrow == 1)
                                                            {

                                                                #region SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR"));
                                                                //1.Payer Resposibilty Sequence No.
                                                                oSegment.set_DataElementValue(1, 0, "S");//_OtherInsurancePST.Trim().Replace("*","")); //P - Primary

                                                                //2.Individual Relationship code
                                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["RelationshipCode"]).Trim().Replace("*", ""));//"18"); // Hard coded(Individual Relationship code) 18 - Self
                                                                //Chec for SBR05,SBR09
                                                                //3.Refrence identification
                                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sGroup"]).Trim().Replace("*", ""));//"22145");///Policy no

                                                                //5.Insurance Type Code
                                                                oSegment.set_DataElementValue(5, 0, "C1"); // C1 - Commercial (Insurance Type Code)


                                                                //oSegment.set_DataElementValue(6, 0, "6"); // 6 - No Co-ordination of Benefit

                                                                ////8.Employment Status Code(Not Used)
                                                                //oSegment.set_DataElementValue(8, 0, "AC"); // Employment status (AC - Active)

                                                                //9.Claim Filing Indicator
                                                                oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceTypeCode"]).Trim().Replace("*", "")); //Commercial Insurance company

                                                                #endregion SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                                #region CAS - Claim Adjustment

                                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\CAS"));
                                                                //oSegment.set_DataElementValue(1, 0, "PI");//PR - Patient Responsibility
                                                                //oSegment.set_DataElementValue(2, 0, "96");
                                                                //oSegment.set_DataElementValue(3, 0, "300");

                                                                #endregion CAS - Claim Adjustment

                                                                #region AMT - Amount

                                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                                //oSegment.set_DataElementValue(1, 0, "D");
                                                                //oSegment.set_DataElementValue(2, 0, "0");

                                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                                //oSegment.set_DataElementValue(1, 0, "F2");
                                                                //oSegment.set_DataElementValue(2, 0, "100");

                                                                #endregion AMT - Amount

                                                                #region MOA - Medicare Outpatient Adjudication

                                                                //ediDataSegment.Set(ref oSegment,(ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\MOA"));
                                                                //oSegment.set_DataElementValue(1,0,"20");
                                                                //oSegment.set_DataElementValue(2,0,"300");
                                                                //oSegment.set_DataElementValue(3,0,"125");

                                                                #endregion

                                                                #region DMG  - Demographic

                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\DMG"));
                                                                oSegment.set_DataElementValue(1, 0, "D8");
                                                                oSegment.set_DataElementValue(2, 0, gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["dtDOB"])).ToString());//"SubscriberDOB"
                                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberGender"]));//"SubscriberGender"

                                                                #endregion DMG  - Demographic

                                                                #region OI - Other Insurance

                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\OI"));
                                                                oSegment.set_DataElementValue(3, 0, "Y");
                                                                /*If OI06 present*/
                                     /*                           oSegment.set_DataElementValue(4, 0, "C");
                                                                oSegment.set_DataElementValue(6, 0, "Y");

                                                                #endregion OI - Other Insurance

                                                                //2330A SUBSCRIBER
                                                                #region NM1 SUBSCRIBER NAME - 2330A

                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                                oSegment.set_DataElementValue(1, 0, "IL");
                                                                oSegment.set_DataElementValue(2, 0, "1");
                                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim().Replace("*", ""));//"SubscriberLastOrgName"
                                                                oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubFName"]).Trim().Replace("*", ""));//"SubscriberFirstname"
                                                                oSegment.set_DataElementValue(8, 0, "MI");
                                                                oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim().Replace("*", ""));//"SubscriberMemberID"

                                                                //N3 SUBSCRIBER ADDRESS
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N3"));
                                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr1"]).Trim().Replace("*", ""));//"SubscriberAddress"

                                                                //N4 SUBSCRIBER CITY
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N4"));
                                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberCity"]).Trim().Replace("*", ""));//"SubscriberCity"
                                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberState"]).Trim().Replace("*", ""));//"SubscrberState"
                                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberZip"]).Trim().Replace("*", ""));//"SubscriberZip"

                                                                #endregion NM1 SUBSCRIBER NAME

                                                                #region Payer Information - 2330B

                                                                //2330B SUBSCRIBER/PAYER
                                                                //NM1 PAYER NAME
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                                oSegment.set_DataElementValue(1, 0, "PR");
                                                                oSegment.set_DataElementValue(2, 0, "2");

                                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceName"]).Trim().Replace("*", ""));//dtInsurance.Rows[0]["sSubscriberName"].ToString().Trim().Replace("*",""));//"PayerLastOrgName"

                                                                oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                                oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerID"]).Trim().Replace("*", ""));//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID
                                                                //}

                                                                if (_SecondayInsuranceAddressDetailsRequired)
                                                                {
                                                                    ////////N3 PAYER ADDRESS
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N3"));
                                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerAddress1"]).Trim().Replace("*", ""));//"InsuranceAddress"

                                                                    ////////N4 PAYER CITY
                                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N4"));
                                                                    oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerCity"]).Trim().Replace("*", ""));//"InsuranceCity"
                                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerState"]).Trim().Replace("*", ""));//"InsuranceState"
                                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerZip"]).Trim().Replace("*", ""));//"InsuranceZip"
                                                                }
                                                                #endregion Payer Information

                                                            }

                                                            #endregion Subscriber Secondary Insurance
                                                        }//End for loop of Patient Insurance 

                                                        for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                        {
                                                            //iItemCount = 1;
                                                            //iItemCount = iItemCount + nLine;
                                                            //oTransLine = new TransactionLine();
                                                            //oTransLine = oTransaction.Lines[nLine];

                                                            #region Service Line
                                                            ////******* SUBSCRIBER SERVICE LINE *************************************************************
                                                            ////TODO: Get the datatable for service info to add fields of service in EDI file.
                                                            ////2400 SERVICE LINE
                                                            //sInstance = iItemCount.ToString().Trim();
                                                            ////LX SERVICE LINE COUNTER
                                                            //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                            //oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                            ////SV1 PROFESSIONAL SERVICE
                                                            //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
                                                            //oSegment.set_DataElementValue(1, 1, "HC");
                                                            //oSegment.set_DataElementValue(1, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
                                                            //if (oTransLine.Mod1Code.ToString() != "")
                                                            //{
                                                            //    oSegment.set_DataElementValue(1, 3, oTransLine.Mod1Code.ToString());//Modifier 1
                                                            //}
                                                            //if (oTransLine.Mod2Code.ToString() != "")
                                                            //{
                                                            //    oSegment.set_DataElementValue(1, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                            //}
                                                            //string _ClaimLineCharges = Convert.ToString(oTransLine.Total);

                                                            //if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                            //{
                                                            //    _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                            //}
                                                            //oSegment.set_DataElementValue(2, 0, _ClaimLineCharges);//"ServiceAmount"
                                                            //oSegment.set_DataElementValue(3, 0, "UN");//UN stands for UNITS
                                                            //oSegment.set_DataElementValue(4, 0, oTransLine.Unit.ToString());//Unit/Quantity


                                                            ////if (oTransLine.Dx1Ptr.ToString() == "True")
                                                            ////{
                                                            ////    oSegment.set_DataElementValue(7, 1, "1");//"Diagnosis Pointer1" (Must Use)
                                                            ////    if (oTransLine.Dx2Ptr.ToString() == "True")
                                                            ////    {
                                                            ////        oSegment.set_DataElementValue(7, 2, "2");//"Diagnosis Pointer2"
                                                            ////    }
                                                            ////    if (oTransLine.Dx3Ptr.ToString() == "True")
                                                            ////    {
                                                            ////        oSegment.set_DataElementValue(7, 3, "3");//"Diagnosis Pointer3"
                                                            ////    }
                                                            ////    if (oTransLine.Dx4Ptr.ToString() == "True")
                                                            ////    {
                                                            ////        oSegment.set_DataElementValue(7, 4, "4");//"Diagnosis Pointer4"
                                                            ////    }
                                                            ////}
                                                            ////else if (oTransLine.Dx2Ptr.ToString() == "True")
                                                            ////{
                                                            ////    oSegment.set_DataElementValue(7, 1, "2");//"Diagnosis Pointer1"
                                                            ////}
                                                            ////else if (oTransLine.Dx3Ptr.ToString() == "True")
                                                            ////{
                                                            ////    oSegment.set_DataElementValue(7, 1, "3");//"Diagnosis Pointer1"
                                                            ////}
                                                            ////else if (oTransLine.Dx4Ptr.ToString() == "True")
                                                            ////{
                                                            ////    oSegment.set_DataElementValue(7, 1, "4");//"Diagnosis Pointer1"
                                                            ////}

                                                            ////////oSegment.set_DataElementValue(9, 0, "N");////Y=Yes, N=No

                                                            //if (dtDx != null && dtDx.Rows.Count > 0)
                                                            //{
                                                            //    for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                            //    {
                                                            //        if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx1Code.Trim())
                                                            //        {
                                                            //            oSegment.set_DataElementValue(7, DxIndex + 1, Convert.ToString(DxIndex + 1));
                                                            //        }
                                                            //        if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx2Code.Trim())
                                                            //        {
                                                            //            oSegment.set_DataElementValue(7, DxIndex + 1, Convert.ToString(DxIndex + 1));
                                                            //        }
                                                            //        if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx3Code.Trim())
                                                            //        {
                                                            //            oSegment.set_DataElementValue(7, DxIndex + 1, Convert.ToString(DxIndex + 1));
                                                            //        }
                                                            //        if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx4Code.Trim())
                                                            //        {
                                                            //            oSegment.set_DataElementValue(7, DxIndex + 1, Convert.ToString(DxIndex + 1));
                                                            //        }
                                                            //    }
                                                            //}

                                                            ////DTP DATE - SERVICE DATE(S)
                                                            //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                            //oSegment.set_DataElementValue(1, 0, "472");
                                                            //oSegment.set_DataElementValue(2, 0, "D8");
                                                            //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"

                                                            #endregion

                                                            iItemCount = 1;
                                                            iItemCount = iItemCount + nLine;
                                                            oTransLine = new TransactionLine();
                                                            oTransLine = oTransaction.Lines[nLine];

                                                            #region Service Line
                                                            //******* SUBSCRIBER SERVICE LINE *************************************************************
                                                            //TODO: Get the datatable for service info to add fields of service in EDI file.
                                                            //2400 SERVICE LINE
                                                            sInstance = iItemCount.ToString().Trim().Replace("*", "");
                                                            //LX SERVICE LINE COUNTER
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                            oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                            //SV1 PROFESSIONAL SERVICE
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
                                                            oSegment.set_DataElementValue(1, 1, "HC");
                                                            oSegment.set_DataElementValue(1, 2, oTransLine.CPTCode.ToString().Replace(".", "").Trim().Replace("*", ""));//"ServiceID"
                                                            if (oTransLine.Mod1Code.ToString().Trim().Replace("*", "") != "")
                                                            {
                                                                oSegment.set_DataElementValue(1, 3, oTransLine.Mod1Code.ToString().Trim().Replace("*", ""));//Modifier 1
                                                            }
                                                            if (oTransLine.Mod2Code.ToString().Trim().Replace("*", "") != "")
                                                            {
                                                                if (oTransLine.Mod1Code.ToString().Trim().Replace("*", "") == "")
                                                                {
                                                                    oSegment.set_DataElementValue(1, 3, oTransLine.Mod2Code.ToString());
                                                                }
                                                                else
                                                                //*AJ,AH
                                                                {
                                                                    oSegment.set_DataElementValue(1, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                                }
                                                            }
                                                            string _ClaimLineCharges = Convert.ToString(oTransLine.Total);

                                                            if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                            {
                                                                _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                            }
                                                            else if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 1, 1) == "0")
                                                            {
                                                                _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 1);
                                                            }
                                                            oSegment.set_DataElementValue(2, 0, _ClaimLineCharges);//"ServiceAmount"
                                                            oSegment.set_DataElementValue(3, 0, "UN");//UN stands for UNITS
                                                            oSegment.set_DataElementValue(4, 0, oTransLine.Unit.ToString());//Unit/Quantity
                                                            if (dtDx != null && dtDx.Rows.Count > 0)
                                                            {
                                                                int _CompTerminatorPos = 0;

                                                                for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                                {
                                                                    if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "") == oTransaction.Lines[nLine].Dx1Code.Trim().Replace("*", ""))
                                                                    {
                                                                        _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                        oSegment.set_DataElementValue(7, _CompTerminatorPos, "1");
                                                                    }
                                                                    if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "") == oTransaction.Lines[nLine].Dx2Code.Trim().Replace("*", ""))
                                                                    {
                                                                        _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                        oSegment.set_DataElementValue(7, _CompTerminatorPos, "2");
                                                                    }
                                                                    if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "") == oTransaction.Lines[nLine].Dx3Code.Trim().Replace("*", ""))
                                                                    {
                                                                        _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                        oSegment.set_DataElementValue(7, _CompTerminatorPos, "3");
                                                                    }
                                                                    if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim().Replace("*", "") == oTransaction.Lines[nLine].Dx4Code.Trim().Replace("*", ""))
                                                                    {
                                                                        _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                        oSegment.set_DataElementValue(7, _CompTerminatorPos, "4");
                                                                    }
                                                                }
                                                            }

                                                            //DTP DATE - SERVICE DATE(S)
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                            oSegment.set_DataElementValue(1, 0, "472");
                                                            oSegment.set_DataElementValue(2, 0, "D8");
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"
                                                            #endregion

                                                            #region " CLIA (Clinical Laboratory Improvement Amendment Number) "
                                                            if (oTransaction.Lines[nLine].AuthorizationNo.Trim() != "")
                                                            {
                                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX\\REF"));
                                                                oSegment.set_DataElementValue(1, 0, "X4"); //Clinical Laboratory Improvement Amendment Number
                                                                oSegment.set_DataElementValue(2, 0, oTransaction.Lines[nLine].AuthorizationNo.Trim());//
                                                            }
                                                            #endregion " CLIA (Clinical Laboratory Improvement Amendment Number) "
                                                        }

                                                        #endregion " Dependent "
                                                    }//end of else loop for dependent

                                                }//If loop for Patient Insurance
                                                //Transaction Line Loop
                                            }//Transaction SETS Loop
                                        }
                                    }
                                }

                                #region " Save EDI File "

                                //Save to a file
                                //SaveFileDialog oSave = new SaveFileDialog();
                                //oSave.Filter = "TEXT Files (*.txt)|*.txt|EDI Files (*.edi)|*.edi|X12 Files (*.X12)|*.X12";
                                //if (oSave.ShowDialog() == DialogResult.OK)
                                //{
                                // sPath = sPath + "837 EDI\\";
                                sPath = "";
                                sPath = AppDomain.CurrentDomain.BaseDirectory + "837 EDI\\";
                                if (System.IO.Directory.Exists(sPath) == false) { System.IO.Directory.CreateDirectory(sPath); }

                                sEdiFile = GetEDIFileName(sPath, _BatchName);

                                oEdiDoc.Save(sEdiFile);
                                System.IO.StreamReader oReader = new System.IO.StreamReader(sEdiFile);
                                string strData;
                                strData = oReader.ReadToEnd();
                                oReader.Close();

                                System.IO.StreamWriter oWriter = new System.IO.StreamWriter(sEdiFile);
                                oWriter.Write(strData);
                                oWriter.Close();
                                _result = sEdiFile;
                                MessageBox.Show("EDI claim generated successfully.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //}


                                #endregion " Save EDI File "

                                #region " Update Claim Manager Table "
                                Int64 _date = 0;
                                Int64 _time = 0;
                                _date = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString());
                                _time = gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToString());
                                gloClaimManager ogloClaimManager = new gloClaimManager(_databaseconnectionstring, "");
                                Int64 _id = ogloClaimManager.InsertUpdateClaimManager(0, _BatchID, oTransaction.TransactionID, oTransaction.ClaimNo, oTransaction.PatientID, Convert.ToInt64(dtClearingHouse.Rows[0]["nClearingHouseID"]), InterchangeHeader, TransactionSetHeader, FunctionalGroupHeader, _date, _time, _UserID, gloPatient.TypeOfBilling.Electronic.GetHashCode(), this.ClinicID);
                                ogloClaimManager.Dispose();
                                #endregion

                                //DESTROYS OBJECTS
                                oSegment.Dispose();
                                oTransactionset.Dispose();
                                oGroup.Dispose();
                                oInterchange.Dispose();

                            }
                        }
                    }
                    catch (System.Runtime.CompilerServices.RuntimeWrappedException Rex)
                    {
                        string _strEx = "";
                        ediException oException = null;
                        oException = (ediException)Rex.WrappedException;
                        _strEx = oException.get_Description();
                        gloAuditTrail.gloAuditTrail.ExceptionLog(_strEx, true);
                        _result = "";
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        _result = "";
                    }
                    finally
                    {
                        if (dtClearingHouse != null) { dtClearingHouse.Dispose(); }
                        if (dtSubmitter != null) { dtSubmitter.Dispose(); }
                        if (dtReceiver != null) { dtReceiver.Dispose(); }
                        if (dtBillingProvider != null) { dtBillingProvider.Dispose(); }
                        if (dtRenderingProvider != null) { dtRenderingProvider.Dispose(); }
                        if (dtFacility != null) { dtFacility.Dispose(); }
                        if (dtPatientInsurances != null) { dtPatientInsurances.Dispose(); }
                        if (dtReferral != null) { dtReferral.Dispose(); }
                    }
                    #endregion " Generate EDI "

                }//SEF File present IF loop
                return _result;
            }*/

        }
    } 


}
