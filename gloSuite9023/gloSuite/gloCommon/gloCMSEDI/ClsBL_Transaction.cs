using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;

namespace gloCMSEDI
{
    public class ClsBL_Transaction
    {
    }
    public class Transaction
    {
        #region "Constructor & Destructor"

        public Transaction()
        {
            _Lines = new TransactionLines();
            _Insurances = new TransactionInsurances();
            _TransactionDetails = new TransactionDetails();
            _InsurancePlans = new TransactionInsurancePlans();
            PatientName = "";
            PatientCode = "";
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
                    if (bAssignedInsurancePlans)
                    {
                        if (_InsurancePlans != null)
                        {
                            _InsurancePlans.Clear();
                            _InsurancePlans.Dispose();
                            _InsurancePlans = null;

                        }
                        bAssignedInsurancePlans = false;
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
        private string _sBox14DateQualifier;
        private Int64 _InjuryDate;
        private Int64 _UnableToWorkFromDate;
        private Int64 _UnableToWorkTillDate;
        private Int64 _TransactionDate;
        private String _CaseNoPrefix;
        private Int64 _ClaimNo;
        private Int64 _ContactID = 0;
        private String _BatchNoPrefix;
        private Int64 _BatchNo;
        private Int64 _PatientID;
        private Int64 _ProviderID;
        private string _ProviderName;
        private string _Box10d;
        private string _ProviderSuffix;//gloPM5040
        private string _MaritalStatus;
        private string _FacilityCode;
        private string _FacilityDescription;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private Int64 _PrefixID = 0;
        //Lines
        private TransactionLines _Lines;
        private TransactionInsurances _Insurances;

        //Transaction Insurance Plans
        private TransactionInsurancePlans _InsurancePlans;


        private TransactionStatus _TransactionStatus = TransactionStatus.None;
        private string _State = "";

        private Int64 _HospitalizationDateFrom = 0;
        private Int64 _HospitalizationDateTo = 0;
        private bool _OutSideLab = false;
        private decimal _OutSideLabCharges = 0;

        private bool _AutoClaim = false;
        private Int64 _AccidentDate = 0;

        //Added By Saket 20090710
        private bool _OtherAccident = false;
        private Int64 _OtherAccidentDate = 0;

        private bool _WorkersComp = false;
        private string _WorkersCompNo = "";
        private bool _IsWorkersCompPrintonCMS1500 = false;

        private Int64 _PriorAuthorizationId = 0;
        private string _PriorAuthorizationNo = "";

        private Int64 _ReferralProviderId = 0;
        private string _ReferralProviderName = "";

        private TransactionDetails _TransactionDetails = null;

        private int _SendCounter = 0;
        private int _SendToRejection = 0;

        private Int64 _LastStatusId = 0;
        private Int64 _userId = 0;
        private string _userName = "";

        private InsuranceTypeFlag _sendToInsuranceFlag = InsuranceTypeFlag.None;

        private Int64 _CloseDayTrayID = 0;
        private string _CloseDayTrayCode = "";
        private string _CloseDayTrayName = "";

        //MaheshB 02152010

        private Int64 _TransactionMasterID = 0;

        //MaheshB 02162010

        private string _sSubClaimNo = "";
        private String _sMainClaimNo = String.Empty;

        //MaheshB 20100311
        private int _nResponsibilityNo;

        //MaheshB 20100426 gloPM5040
        private Boolean _IsRebill = false;

        private Int64 _ReferalProviderID_New = 0;

        bool _IsSameAsBillingProvider = false;

        private Boolean _IsReplacementClaim = false; //Added by Abhisekh Pandey on 18 Aug

        private string _sBox19Notes = ""; //Added by Abhisekh Pandey on 18 Aug
        private Boolean _IsBox19CorrectRplmnt = false;//Added by Abhisekh Pandey on 18 Aug
        private Int64 _InsuranceID = 0;
        private string _InsuranceName = "";
        private Int64 _InsuranceFlag = 0;
        private PayerMode _ResponsibilityType = PayerMode.None;
        private ClaimStatus _ClaimStatus = ClaimStatus.None;
        private Int64 _ParentTransactionID = 0;
        private string _ParentClaimNo = "";
        private FeeScheduleType _FeeScheduleType = FeeScheduleType.None;
        private Int64 _FeeScheduleID = 0;
        private FacilityType _FacilityType = FacilityType.None;
        private string _SMedicaidResubmissionCode = "";
        private bool _bIsEPSDTScreening;
        private bool _bIsEPSDTReferral;
        private string _sRefrralCode = "";
        private string _sReferralType = "";
        //Added by Mukesh Patel on 12 Nov 2010

        private Int64 _ndtBox15Date;

        private string _sBox15DateQualifier;

        private DataTable _dtclaimInsurance=null;
        private DataTable _dtPatientSettings = null;
        private DataTable _dtInsurancesBoxSettings = null;
        private DataTable _dtInsuranceCompanyName = null;
        private DataTable _dtBox19AndIncludeReferal = null;
        //..End Addedd by Mukesh Patel on 12 Nov 2010
       
        // CMS1500 08/05 Box 19 changes 01032014 Samneer
        private string _sProviderQualifier = "";
        // Line added on 02072014 for icd 10 changes Sameer
        private int _nICDRevision;
        private string _HCFA_FacilityMammogramCertNumber = "";
        #endregion " Declarations "

        #region Property Procedures of Transaction Class

        public DataTable Box19AndIncludeReferal
        {
            get { return _dtBox19AndIncludeReferal; }
            set { _dtBox19AndIncludeReferal = value; }
        }

        public DataTable InsuranceCompanyName
        {

            get { return _dtInsuranceCompanyName; }
            set { _dtInsuranceCompanyName = value; }
        }


        
        public DataTable PatientSettings
        {
             get { return _dtPatientSettings; }
            set { _dtPatientSettings = value; }
        }

        public DataTable InsurancesBoxSettings
        {
            get { return _dtInsurancesBoxSettings; }
            set { _dtInsurancesBoxSettings = value; }
        }

        public DataTable  ClaimInsurance
        {
            get { return _dtclaimInsurance; }
            set { _dtclaimInsurance = value; }
        }
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
        public string sBox14DateQualifier
        {
            get { return _sBox14DateQualifier; }
            set { _sBox14DateQualifier = value; }
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

        public String PatientName { get; set; }

        public String PatientCode { get; set; }

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


        public String Box10d
        {
            get { return _Box10d; }
            set { _Box10d = value; }
        }

        public String ProviderSuffix
        {
            get { return _ProviderSuffix; }
            set { _ProviderSuffix = value; }
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

        private Boolean bAssignedInsurancePlans = true;
        public TransactionInsurancePlans InsurancePlans
        {
            get { return _InsurancePlans; }
            set
            {
                if (bAssignedInsurancePlans)
                {
                    if (_InsurancePlans != null)
                    {
                        _InsurancePlans.Clear();
                        _InsurancePlans.Dispose();
                        _InsurancePlans = null;

                    }
                    bAssignedInsurancePlans = false;
                }
                _InsurancePlans = value;
            }
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
        public Int64 ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; }
        }

        //Added By Saket 20090710
        public bool OtherAccident
        {
            get { return _OtherAccident; }
            set { _OtherAccident = value; }
        }
        public Int64 OtherAccidentDate
        {
            get { return _OtherAccidentDate; }
            set { _OtherAccidentDate = value; }
        }


        public bool WorkersComp
        {
            get { return _WorkersComp; }
            set { _WorkersComp = value; }
        }

        public string WorkersCompNo
        {
            get { return _WorkersCompNo; }
            set { _WorkersCompNo = value; }
        }

        public bool WorkersCompPrintonCMS1500
        {
            get { return _IsWorkersCompPrintonCMS1500; }
            set { _IsWorkersCompPrintonCMS1500 = value; }
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
        public Int64 PriorAuthorizationID
        {
            get { return _PriorAuthorizationId; }
            set { _PriorAuthorizationId = value; }
        }
        public string PriorAuthorizationNo
        {
            get { return _PriorAuthorizationNo; }
            set { _PriorAuthorizationNo = value; }
        }

        public Int64 ReferralProviderID
        {
            get { return _ReferralProviderId; }
            set { _ReferralProviderId = value; }
        }
        public string ReferralProviderName
        {
            get { return _ReferralProviderName; }
            set { _ReferralProviderName = value; }
        }

        public int SendCounter
        {
            get { return _SendCounter; }
            set { _SendCounter = value; }
        }
        public int SendToRejection
        {
            get { return _SendToRejection; }
            set { _SendToRejection = value; }
        }

        public Int64 LastStatusId
        {
            get { return _LastStatusId; }
            set { _LastStatusId = value; }
        }

        public Int64 TransactionUserID
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string TransactionUserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public InsuranceTypeFlag SendToInsuranceFlag
        {
            get { return _sendToInsuranceFlag; }
            set { _sendToInsuranceFlag = value; }
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

        //MaheshB 02152010
        public Int64 TransactionMasterID
        {
            get { return _TransactionMasterID; }
            set { _TransactionMasterID = value; }
        }

        //MaheshB 02162010
        public string SubClaimNo
        {
            get { return _sSubClaimNo; }
            set { _sSubClaimNo = value; }
        }

        //MaheshB 20101103
        public int ResponsibilityNo
        {
            get { return _nResponsibilityNo; }
            set { _nResponsibilityNo = value; }
        }
        //20100617 gloPM5050
        public Int64 ReferalProviderID_New
        {
            get { return _ReferalProviderID_New; }
            set { _ReferalProviderID_New = value; }
        }
        public string ProviderIDQualifier
        {
            get;  set;
        }
        public Boolean IsSameAsBillingProvider
        {
            get { return _IsSameAsBillingProvider; }
            set { _IsSameAsBillingProvider = value; }
        }
        public Boolean IsReplacementClaim
        {
            get { return _IsReplacementClaim; }
            set { _IsReplacementClaim = value; }
        }

        public string sBox19Notes
        {
            get { return _sBox19Notes; }
            set { _sBox19Notes = value; }
        }

        public Boolean IsBox19CorrectRplmnt
        {
            get { return _IsBox19CorrectRplmnt; }
            set { _IsBox19CorrectRplmnt = value; }
        }

        public Int64 InsuranceID
        {
            get { return _InsuranceID; }
            set { _InsuranceID = value; }
        }

        public String InsuranceName
        {
            get { return _InsuranceName; }
            set { _InsuranceName = value; }
        }

        public Int64 InsuranceFlag
        {
            get { return _InsuranceFlag; }
            set { _InsuranceFlag = value; }
        }

        public PayerMode ResponsibilityType
        {
            get { return _ResponsibilityType; }
            set { _ResponsibilityType = value; }
        }

        public ClaimStatus ClaimStatus
        {
            get { return _ClaimStatus; }
            set { _ClaimStatus = value; }
        }

        public Int64 ParentTransactionID
        {
            get { return _ParentTransactionID; }
            set { _ParentTransactionID = value; }
        }

        public String ParentClaimNo
        {
            get { return _ParentClaimNo; }
            set { _ParentClaimNo = value; }
        }
        public FeeScheduleType FeeScheduleType
        {
            get { return _FeeScheduleType; }
            set { _FeeScheduleType = value; }
        }
        public Int64 FeeScheduleID
        {
            get { return _FeeScheduleID; }
            set { _FeeScheduleID = value; }
        }
        public FacilityType FacilityType
        {
            get { return _FacilityType; }
            set { _FacilityType = value; }
        }
        public String MainClaimNo
        {
            get { return _sMainClaimNo; }
            set { _sMainClaimNo = value; }
        }

        public Boolean IsRebill
        {
            get { return _IsRebill; }
            set { _IsRebill = value; }
        }
        
        public Boolean IsResend { get; set; }

        public Boolean IsVoid { get; set; }
        public string MedicaidResubmissionCode
        {
            get { return _SMedicaidResubmissionCode; }
            set { _SMedicaidResubmissionCode = value; }
        }

        public bool bIsEPSDTScreening
        {
            get { return _bIsEPSDTScreening; }
            set { _bIsEPSDTScreening = value; }
        }
        public bool IsEPSDTReferral
        {
            get { return _bIsEPSDTReferral; }
            set { _bIsEPSDTReferral = value; }
        }
        public string ReferralCode
        {
            get { return _sRefrralCode; }
            set { _sRefrralCode = value; }
        }
        public string ReferralType
        {
            get { return _sReferralType; }
            set { _sReferralType = value; }
        }
        ////MaheshB 20102003
        //public String ClaimNumber
        //{
        //    get
        //    {
        //        if (_sSubClaimNo.Trim() != "" && _sSubClaimNo.Contains("-") == false)
        //        {
        //            return (FormattedClaimNumberGeneration(_ClaimNo.ToString()) + '-' + _sSubClaimNo.ToString());
        //        }
        //        else
        //        {
        //            return FormattedClaimNumberGeneration(_ClaimNo.ToString());
        //        }
        //    }
        //}

        ////MukeshP 20100427
        // Get Sub Claim No
        public String ClaimNumber
        {
            get
            {
                if (_sSubClaimNo.Trim() != "" && _sSubClaimNo.Contains("-") == false)
                {
                    return (FormattedClaimNumberGeneration(_ClaimNo.ToString()) + '-' + _sSubClaimNo.ToString());
                }
                else
                {
                    if (_sSubClaimNo.Contains("-") == true && _sMainClaimNo != String.Empty)
                    { return (FormattedClaimNumberGeneration(_ClaimNo.ToString()) + '-' + _sMainClaimNo.ToString()); }
                    else
                    { return FormattedClaimNumberGeneration(_ClaimNo.ToString()); }
                }
            }
        }

        //Code Added by Mukesh Patel on 12 Nov 2010

        public Int64 ndtBox15Date 
        { get { return _ndtBox15Date; } set { _ndtBox15Date = value; } }

        //..End Code Addedd by Mukesh Patel on 12 Nov 2010
        // Property added on 12162013 Sameer
        public string sBox15DateQualifier
        {
            get { return _sBox15DateQualifier; }
            set { _sBox15DateQualifier = value; }
        }
        // CMS1500 08/05 Box 19 changes 01032014 Samneer
        public string sProviderQualifier
        {
            get { return _sProviderQualifier; }
            set { _sProviderQualifier = value; }
        }
        // Prpperty added on 02072014 for icd 10 changes Sameer
        public int nICDRevision
        {
            get { return _nICDRevision; }
            set { _nICDRevision = value; } 
        }
        public string HCFA_FacilityMammogramCertNumber
       {
           get { return _HCFA_FacilityMammogramCertNumber; }
           set { _HCFA_FacilityMammogramCertNumber = value; }
        }
        #endregion

      
        private string FormattedClaimNumberGeneration(string NumberSize)
        {
            int _length = 0;
            _length = NumberSize.Length;
            if (_length == 1)
            {
                NumberSize = "0000" + NumberSize;
            }
            else if (_length == 2)
            {
                NumberSize = "000" + NumberSize;
            }
            else if (_length == 3)
            {
                NumberSize = "00" + NumberSize;
            }
            else if (_length == 4)
            {
                NumberSize = "0" + NumberSize;
            }
            else if (_length == 5)
            {
               // NumberSize = NumberSize;
            }
            return NumberSize;
        }


       
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
        public string HCFA_PatientAreaCode = "";
        public string HCFA_PatientCode = "";
        public string HCFA_PatientPhone = "";
        public string HCFA_PatientSSN = "";
        public string HCFA_PatientEmploymentStatus = "";
        public string HCFA_PatientEmploymentType = ""; //full time/part time
        public string HCFA_PatientEmployer_School_Name = "";
        public string HCFA_OtherPatientEmployer_School_Name = "";

        public string HCFA_PriorAuthorizationNo = "";

        public string HCFA_ProviderFName = "";
        public string HCFA_ProviderMName = "";
        public string HCFA_ProviderLName = "";
        public string HCFA_ProviderAddress1 = "";
        public string HCFA_ProviderAddress2 = "";
        public string HCFA_ProviderCity = "";
        public string HCFA_ProviderState = "";
        public string HCFA_ProviderZip = "";
        public string HCFA_ProviderAreaCode = "";
        public string HCFA_ProviderPhone = "";
        public string HCFA_ProviderNPI = "";
        public string HCFA_ProviderUPIN = "";
        public string HCFA_ProviderEIN = "";
        public string HCFA_ProviderSSN = "";
        public string HCFA_ProviderTaxonomy = "";
        public string HCFA_ProviderStateMedicalNo = "";
        public string HCFA_ProviderSuffix = "";//gloPM5040

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
        public string HCFA_FacilityAreaCode = "";
        public string HCFA_FacilityPhone = "";
        public string HCFA_FacilityID = "";

        public string HCFA_ReferringProviderFName = "";
        public string HCFA_ReferringProviderLName = "";
        public string HCFA_ReferringProviderMName = "";
        public string HCFA_ReferringProviderNPI = "";
        public string HCFA_ReferringProviderAddress1 = "";
        public string HCFA_ReferringProviderAddress2 = "";
        public string HCFA_ReferringProviderCity = "";
        public string HCFA_ReferringProviderState = "";
        public string HCFA_ReferringProviderZip = "";
        public string HCFA_ReferringProviderAreaCode = "";
        public string HCFA_ReferringProviderPhone = "";
        public string HCFA_ReferringProviderUPIN = "";
        public string HCFA_ReferringProviderSSN = "";
        public string HCFA_ReferringProviderEIN = "";
        public string HCFA_ReferringProviderTaxanomy = "";

        

        //public string HCFA_PriorAuthorizationNo = "";
        public string HCFA_LastSeenDate = "";
        public bool HCFA_bIsRefProvAsSupervisor = false;
        public bool HCFA_bIsSignatureOnFile = false;
        public string HCFA_PatientSuffix = "";
       
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
        private string _crosswalkcptcode = "";
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
        private decimal _unit = 1;
        private decimal _total = 0;
        private decimal _allowedCharges = 0;
        private decimal _PatientResponsibility = 0;
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
        private string _RevenueCode = "";
        private Int64 _TransactionMasterID = 0;
        private Int64 _TransactionMasterDetailID = 0;
        private decimal _BilledAmount = 0;

        private Int64 _ParentTransactionID = 0;
        private Int64 _ParentTransactionDetailID = 0;
        private Boolean _IsLineSplitted = false;

        //Code added on 20090509 by - Sagar Ghodke
        //Code added to implement LabCharges for lines

        private bool _isLabCPT = false;
        private string _sAuthorizationNo = "";

        //Field to send the service line in claim file out of all lines in transaction
        private bool _sendToClaim = true;


        private Int64 _CloseDayTrayID = 0;
        private string _CloseDayTrayCode = "";
        private string _CloseDayTrayName = "";

        private string _lineprimarydxCode = "";
        private string _lineprimarydxDesc = "";

        private bool _ishold = false;
        private string _holdreason = "";

        private Boolean _IsWorkerComp = false;
        private Decimal _CopayAmt = 0;
        private string _Anesthesia = "";

        //private string _line2primarydxCode = "";
        //private string _line2primarydxDesc = "";
        //private string _line3primarydxCode = "";
        //private string _line3primarydxDesc = "";
        //private string _line4primarydxCode = "";
        //private string _line4primarydxDesc = "";
        //private string _line5primarydxCode = "";
        //private string _line5primarydxDesc = "";
        //private string _line6primarydxCode = "";
        //private string _line6primarydxDesc = "";
        //private string _line7primarydxCode = "";
        //private string _line7primarydxDesc = "";
        //private string _line8primarydxCode = "";
        //private string _line8primarydxDesc = "";



        //End code add 20090509,Sagar Ghodke

        //Added by Anil on 20081113
        public string HCFA_RenderingFName = "";
        public string HCFA_RenderingMName = "";
        public string HCFA_RenderingLName = "";
        public string HCFA_RenderingProviderAddress1 = "";
        public string HCFA_RenderingProviderAddress2 = "";
        public string HCFA_RenderingProviderCity = "";
        public string HCFA_RenderingProviderState = "";
        public string HCFA_RenderingProviderZip = "";
        public string HCFA_RenderingProviderAreaCode = "";
        public string HCFA_RenderingProviderPhone = "";
        public string HCFA_RenderingProviderNPI = "";
        public string HCFA_RenderingProviderMedicalLicenceNo = "";
        public string HCFA_RenderingProviderUPIN = "";
        public string HCFA_RenderingProviderEIN = "";
        public string HCFA_RenderingProviderSSN = "";
        public string HCFA_RenderingProviderTaxonomy = "";

        //**Code added on 20090605 by - Sagar Ghodke
        
      
        
        //private TransactionOtherPayments transaction_otherPayments = null;

        //
        private FeeScheduleType _FeeScheduleType = FeeScheduleType.None;
        private Int64 _FeeScheduleID = 0;
        private FacilityType _FacilityType = FacilityType.None;

        //Added by Mukesh Patel on 12 Nov 2010            

        private bool _EMG;
        private bool _IsServiceScreening;
        private bool _IsFamilyPlanningIndicator;

        private bool _IsMammogramCertNumber;
        //..End Addedd by Mukesh Patel on 12 Nov 2010

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
        public string CrosswalkCPTCode
        {
            get { return _crosswalkcptcode; }
            set { _crosswalkcptcode = value; }
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
        public decimal Unit
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
        public decimal PatientResponsibility
        {
            get { return _PatientResponsibility; }
            set { _PatientResponsibility = value; }
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

        public bool IsLabCPT
        {
            get { return _isLabCPT; }
            set { _isLabCPT = value; }
        }

        public string AuthorizationNo
        {
            get { return _sAuthorizationNo; }
            set { _sAuthorizationNo = value; }
        }

        public bool SendToClaim
        {
            get { return _sendToClaim; }
            set { _sendToClaim = value; }
        }

        public string LinePrimaryDxCode
        {
            get { return _lineprimarydxCode; }
            set { _lineprimarydxCode = value; }
        }
        public string LinePrimaryDxDesc
        {
            get { return _lineprimarydxDesc; }
            set { _lineprimarydxDesc = value; }
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
        public bool IsHold
        {
            get { return _ishold; }
            set { _ishold = value; }
        }
        public string HoldReason
        {
            get { return _holdreason; }
            set { _holdreason = value; }
        }

        public string RevenueCode
        {
            get { return _RevenueCode; }
            set { _RevenueCode = value; }
        }

        public Int64 TransactionMasterID
        {
            get { return _TransactionMasterID; }
            set { _TransactionMasterID = value; }
        }

        public Int64 TransactionMasterDetailID
        {
            get { return _TransactionMasterDetailID; }
            set { _TransactionMasterDetailID = value; }
        }
        public decimal BilledAmount
        {
            get { return _BilledAmount; }
            set { _BilledAmount = value; }
        }

        public Int64 ParentTransactionID
        {
            get { return _ParentTransactionID; }
            set { _ParentTransactionID = value; }
        }

        public Int64 ParentTransactionDetailID
        {
            get { return _ParentTransactionDetailID; }
            set { _ParentTransactionDetailID = value; }
        }

        public Boolean IsLineSplitted
        {
            get { return _IsLineSplitted; }
            set { _IsLineSplitted = value; }
        }

        public FeeScheduleType FeeScheduleType
        {
            get { return _FeeScheduleType; }
            set { _FeeScheduleType = value; }
        }
        public Int64 FeeScheduleID
        {
            get { return _FeeScheduleID; }
            set { _FeeScheduleID = value; }
        }
        public FacilityType FacilityType
        {
            get { return _FacilityType; }
            set { _FacilityType = value; }
        }



        public Boolean IsWorkerComp
        {
            get { return _IsWorkerComp; }
            set { _IsWorkerComp = value; }
        }

        public Decimal CopayAmount
        {
            get { return _CopayAmt; }
            set { _CopayAmt = value; }
        }


        public string Anesthesia
        {
            get { return _Anesthesia; }
            set { _Anesthesia = value; }
        }        

        public Int64 NDCID { get; set; }
        public String NDCCodeQualifier { get; set; }
        public String NDCCode { get; set; }
        public String NDCDescription { get; set; }
        public String NDCUnitCode { get; set; }
        public String NDCUnitDescription { get; set; }
        public String NDCUnit { get; set; }
        public String NDCUnitPricing { get; set; }
        public String DisplayNDCCode_HCFA { get; set; }
        public String NDCPrescriptionDesc  { get; set; }
     
        
        public bool EMG
        { get { return _EMG; } set { _EMG = value; } }
        public bool IsServiceScreening
        { get { return _IsServiceScreening; } set { _IsServiceScreening = value; } }
        public bool IsFamilyPlanningIndicator
        { get { return _IsFamilyPlanningIndicator; } set { _IsFamilyPlanningIndicator = value; } }
        #endregion " Public Property Procedures "


        public bool IsMammogramCertNumber
        {
            get { return _IsMammogramCertNumber; }
            set { _IsMammogramCertNumber = value; }
        }
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
        private Int64 _transactionDetailId = 0;
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

        public Int64 TransactionDetailID
        {
            get { return _transactionDetailId; }
            set { _transactionDetailId = value; }
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

    public class TransactionInsurancePlan
    {
        #region "Constructor & Destructor"

        public TransactionInsurancePlan()
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

        ~TransactionInsurancePlan()
        {
            Dispose(false);
        }

        #endregion

        #region Declaration

        private Int64 _TransactionId = 0;
        private Int64 _PatientId = 0;
        private Int64 _ClaimNo = 0;
        private Int64 _InsuranceID = 0;
        private Int64 _ContactID = 0;
        private Int64 _ResponsibilityType = 0;
        private String _InsuranceName = "";
        private Boolean _IsWorkerComp = false;
        private Decimal _CopayAmt = 0;
        private Int64 _ResponsibilityNo = 0;
        private Int64 _ClinicID = 0;

        #endregion

        #region Properties

        public Int64 TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        public Int64 PatientId
        {
            get { return _PatientId; }
            set { _PatientId = value; }
        }

        public Int64 ClaimNo
        {
            get { return _ClaimNo; }
            set { _ClaimNo = value; }
        }

        public Int64 InsuranceID
        {
            get { return _InsuranceID; }
            set { _InsuranceID = value; }
        }
        public Int64 ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; }
        }
        public Int64 ResponsibilityNo
        {
            get { return _ResponsibilityNo; }
            set { _ResponsibilityNo = value; }
        }
        public Int64 ResponsibilityType
        {
            get { return _ResponsibilityType; }
            set { _ResponsibilityType = value; }
        }

        public String InsuranceName
        {
            get { return _InsuranceName; }
            set { _InsuranceName = value; }
        }

        public Boolean IsWorkerComp
        {
            get { return _IsWorkerComp; }
            set { _IsWorkerComp = value; }
        }

        public Decimal CopayAmount
        {
            get { return _CopayAmt; }
            set { _CopayAmt = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion

    }

    public class TransactionInsurancePlans
    {

        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public TransactionInsurancePlans()
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

        ~TransactionInsurancePlans()
        {
            Dispose(false);
        }
        #endregion

        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(TransactionInsurancePlan item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(TransactionInsurancePlan item)
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

        public TransactionInsurancePlan this[int index]
        {
            get
            { return (TransactionInsurancePlan)_innerlist[index]; }
        }

        public bool Contains(TransactionInsurancePlan item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(TransactionInsurancePlan item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(TransactionInsurancePlan[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

}
