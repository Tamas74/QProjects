using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using Microsoft.SqlServer.Server;

namespace gloBilling
{


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
            private Int64 _InjuryDate;
            private Int64 _UnableToWorkFromDate;
            private Int64 _UnableToWorkTillDate;
            private Int64 _TransactionDate;
            private String _CaseNoPrefix = "";
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

            //Transaction Insurance Plans
            private TransactionInsurancePlans _InsurancePlans;


            private TransactionStatus _TransactionStatus = TransactionStatus.None;
            private string _State = "";

            private Int64 _HospitalizationDateFrom = 0;
            private Int64 _HospitalizationDateTo = 0;
            private bool _OutSideLab = false;
            private decimal _OutSideLabCharges = 0;
            private string _CLIANumber = "";

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


            private Int64 _TransactionMasterID = 0;
            private String _SubClaimNo = String.Empty;

            private Int64 _InsuranceID = 0;
            private String _InsuranceName = "";
            private Int64 _InsuranceFlag = 0;
            private Int64 _ContactID = 0;
            private Int16 _ResponsibilityNo = 0;
            private PayerMode _ResponsibilityType = PayerMode.None;

            private ClaimStatus _ClaimStatus = ClaimStatus.None;


            //Parent Claim Information for splitted Claim
            private Int64 _ParentTransactionID = 0;
            private String _ParentClaimNo = String.Empty;

            //Hold Fee Schedule
            private FeeScheduleType _FeeScheduleType = FeeScheduleType.None;
            private Int64 _FeeScheduleID = 0;
            private FacilityType _FacilityType = FacilityType.None;

            private String _ClaimNumber = String.Empty;
            private String _sMainClaimNo = String.Empty;
            private ClaimHold _ClaimHold = null; //Added by Debasish Das on 19th Apr 201

            private Boolean _IsRebill = false;

            private Boolean _IsReplacementClaim = false; //Added by Abhisekh Pandey on 18 Aug
            private String _sClaimRefNo = ""; //Added by Abhisekh Pandey on 18 Aug

            //..Code added on 2010719 by Sagar Ghodke
            //..Code added to implement sql transaction for ERA Posting 

            private bool _UseExtSqlConnection = false;
            private System.Data.SqlClient.SqlConnection _ExtSqlConnection = null;
            private System.Data.SqlClient.SqlTransaction _ExtSqlTransaction = null;
            private bool _ExtTransactionErrorValue = false;
            private string _ExtTransactionErrorMsg = String.Empty;


            //..End code add on 20100719 by Sagar Ghodke


            //Added by Mukesh Patel on 12 Nov 2010

            private Int64 _IllnessDate;            

            //..End Addedd by Mukesh Patel on 12 Nov 2010


            //Added by Subashish_b on 06/Jan /2011 (integration made on date-10/May/2011) for declaring property variable for storing PAF Values
            private Int64 _nPAccountID = 0;
            private Int64 _nAccountPatientID = 0;
            private Int64 _nGuarantorID = 0;
            private int _nICDRevision = 0;
            private Int64 _nClaimCategoryID = 0;
            //End

            private String _sPWKReportTypeCode = string.Empty;
            private String _sPWKReportTransmissionCode = string.Empty;
            private String _sPWKAttachmentControlNumber = string.Empty;
            private String _sMammogramCertNumber = string.Empty;
            private String _sIDENo = string.Empty;

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
            public string ClaimBox14QualifierCode { get; set; }

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
            public string CLIANumber
            {
                get { return _CLIANumber; }
                set { _CLIANumber = value; }
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


            public Int64 TransactionMasterID
            {
                get { return _TransactionMasterID; }
                set { _TransactionMasterID = value; }
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
            public Int64 ContactID
            {
                get { return _ContactID; }
                set { _ContactID = value; }
            }
            public Int16 ResponsibilityNo
            {
                get { return _ResponsibilityNo; }
                set { _ResponsibilityNo = value; }
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

            public String SubClaimNo
            {
                get { return _SubClaimNo; }
                set { _SubClaimNo = value; }
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
            public ClaimHold Hold  //Added by Debasish Das on 19th Apr 2010
            {
                get { return _ClaimHold; }
                set { _ClaimHold = value; }
            }

            public ClaimBox19Note ClaimBox19Note { get; set; }


            public ClaimBox19Notes ClaimBox19Notes { get; set; }

            public string ClaimBox10dNote = string.Empty;
            public Boolean IsRebill
            {
                get { return _IsRebill; }
                set { _IsRebill = value; }
            }

            public Boolean IsReplacementClaim
            {
                get { return _IsReplacementClaim; }
                set { _IsReplacementClaim = value; }
            }
            public string sClaimRefNo
            {
                get { return _sClaimRefNo; }
                set { _sClaimRefNo = value; }
            }
            public Boolean IsSameAsBillingProvider { get; set; }

            public Int64 ReferalProviderID_New { get; set; }

            public Boolean IsResend { get; set; }

            public Boolean IsVoid { get; set; }
            
            public Boolean IsBadDebt { get; set; }

            public DateTime VoidedDate { get; set; }

            public Int64 VoidByID { get; set; }

            public Int64 nVoidedDate { get; set; }

            public Int64 VoidedTrayID { get; set; }

            public String VoidByName { get; set; }

            public int NoOfServiceLine { get; set; }

            public int NoOfDiagnosis { get; set; }

            //add on 20110630 by Mahesh Nawal for 6031
            public String DelayReasonCodeID { get; set; }

            public String ServiceAuthExceCode { get; set; }

            public Boolean IsClaimSplitted { get; set; }
 			public Nullable<DateTime> dtInitTreatmentDate { get; set; }

            public Int64 nClaimCategoryID
            {
                get { return _nClaimCategoryID; }
                set { _nClaimCategoryID = value; }
            }

            public int nICDRevision
            {
                get { return _nICDRevision; }
                set { _nICDRevision = value; }
            }
            public String MedicaidResubmissioncode { get; set; }
            //end code

            public String ClaimNumber
            {
                get
                {
                    if (_SubClaimNo.Trim() != "" && _SubClaimNo.Contains("-") == false)
                    {
                        return (FormattedClaimNumberGeneration(_ClaimNo.ToString()) + '-' + _SubClaimNo.ToString());
                    }
                    else
                    {
                        if (_SubClaimNo.Contains("-") == true && _sMainClaimNo != String.Empty)
                        { return (FormattedClaimNumberGeneration(_ClaimNo.ToString()) + '-' + _sMainClaimNo.ToString()); }
                        else
                        { return FormattedClaimNumberGeneration(_ClaimNo.ToString()); }
                    }
                }
            }

            public String MainClaimNo
            {
                get { return _sMainClaimNo; }
                set { _sMainClaimNo = value; }
            }

            //..Code added on 2010719 by Sagar Ghodke
            //..Code added to implement sql transaction for ERA Posting 

            public bool UseExtSqlConnection
            { get { return _UseExtSqlConnection; } set { _UseExtSqlConnection = value; } }
            public System.Data.SqlClient.SqlConnection ExtSqlConnection
            { get { return _ExtSqlConnection; } set { _ExtSqlConnection = value; } }
            public System.Data.SqlClient.SqlTransaction ExtSqlTransaction
            { get { return _ExtSqlTransaction; } set { _ExtSqlTransaction = value; } }
            public bool ExtTransactionErrorValue
            { get { return _ExtTransactionErrorValue; } set { _ExtTransactionErrorValue = value; } }
            public string ExtTransactionErrorMsg
            { get { return _ExtTransactionErrorMsg; } set { _ExtTransactionErrorMsg = value; } }


            //..End code add on 20100719 by Sagar Ghodke


            //Code Added by Mukesh Patel on 12 Nov 2010

            public Int64 IllnessDate
            { get { return _IllnessDate; } set { _IllnessDate = value; } }           

            //..End Code Addedd by Mukesh Patel on 12 Nov 2010

            //Added by Subashish_b on 06/Jan /2011 (integration made on date-10/May/2011) for  creating properties to store PAF Values
            public Int64 PAccountID
            { get { return _nPAccountID; } set { _nPAccountID = value; } }

            public Int64 AccountPatientID
            { get { return _nAccountPatientID; } set { _nAccountPatientID = value; } }

            public Int64 GuarantorID
            { get { return _nGuarantorID; } set { _nGuarantorID = value; } }
            //End

            public Int64 CaseID { get; set; }

            public string CaseName { get; set; }

            public Boolean bIsRefprovAsSupervisor { get; set; }

            public EPSDTFamilyPlanningClaim ClaimEPSDT { get; set; }

            public Int64 ActualTransactionID { get; set; }

            public Int64 LastSeenDate
            { get; set; }

            public string ProviderQualifierCode { get; set; }
            public string ClaimBox15QualifierCode { get; set; }
            public Nullable<DateTime> ClaimBox15Date { get; set; }

            public string PWKReportTypeCode
            {
                get { return _sPWKReportTypeCode; }
                set { _sPWKReportTypeCode = value; }
            }

            public string PWKReportTransmissionCode
            {
                get { return _sPWKReportTransmissionCode; }
                set { _sPWKReportTransmissionCode = value; }
            }

            public string PWKAttachmentControlNumber
            {
                get { return _sPWKAttachmentControlNumber; }
                set { _sPWKAttachmentControlNumber = value; }
            }
            public string MammogramCertNumber
            {
                get { return _sMammogramCertNumber; }
                set { _sMammogramCertNumber=value; }
            }
            public string IDENo
            {
                get { return _sIDENo; }
                set { _sIDENo = value; }
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
                  //  NumberSize = NumberSize;
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
            public string HCFA_PatientCode = "";
            public string HCFA_PatientPhone = "";
            public string HCFA_PatientSSN = "";
            public string HCFA_PatientEmploymentStatus = "";
            public string HCFA_PatientEmploymentType = ""; //full time/part time
            public string HCFA_PatientEmployer_School_Name = "";

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

            //DOS TO Implementation - now we are just makeing provision of this property but null date logic will be implemented on 0 value as it is numeric in database
            //only will use to check at frontend as it is datetime value for respective property
            private bool _dateServiceFromIsNull = false;
            private bool _dateServiceTillIsNull = false;

            private string _posCode = "";
            private string _posDescription = "";
            private string _tosCode = "";
            private string _tosDescription = "";
            private string _cptcode = "";
            private string _crosswalkcptcode = "";
            private string _cptDescription = "";
            private string _crosswalkcptDescription = "";
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
            private decimal _BilledAmount = 0;
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
            private string _NDCCode = "";

            //Hold Fee Schedule
            private FeeScheduleType _FeeScheduleType = FeeScheduleType.None;
            private Int64 _FeeScheduleID = 0;
            private FacilityType _FacilityType = FacilityType.None;

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
            public string HCFA_RenderingProviderPhone = "";
            public string HCFA_RenderingProviderNPI = "";
            public string HCFA_RenderingProviderMedicalLicenceNo = "";
            public string HCFA_RenderingProviderUPIN = "";
            public string HCFA_RenderingProviderEIN = "";
            public string HCFA_RenderingProviderSSN = "";
            public string HCFA_RenderingProviderTaxonomy = "";

            //**Code added on 20090605 by - Sagar Ghodke

        //    private TransactionOtherPayments transaction_otherPayments = null;

            //Master Claim Transaction ID and Detail ID

            private Int64 _TransactionMasterID = 0;
            private Int64 _TransactionMasterDetailID = 0;

            //Parent Claim Transaction ID and Detail ID
            private Int64 _ParentTransactionID = 0;
            private Int64 _ParentTransactionDetailID = 0;
            private Boolean _IsLineSplitted = false;

            //Added by Mukesh Patel on 12 Nov 2010            

            private bool _EMG;

            private Boolean _bIsSelfClaim;
            //..End Addedd by Mukesh Patel on 12 Nov 2010

            //Added by Subashish_b on 06/Jan /2011 (integration made on date-10/May/2011) for declaring property pariable 
            private Int64 _nPAccountID;
            private Int64 _nAccountPatientID;
            private Int64 _nGuarantorID;
            //End

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

            public bool DateServiceFromIsNull
            {
                get { return _dateServiceFromIsNull; }
                set { _dateServiceFromIsNull = value; }
            }

            public bool DateServiceTillIsNull
            {
                get { return _dateServiceTillIsNull; }
                set { _dateServiceTillIsNull = value; }
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
            public string CrosswalkCPTDescription
            {
                get { return _crosswalkcptDescription; }
                set { _crosswalkcptDescription = value; }
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
            public decimal BilledAmount
            {
                get { return _BilledAmount; }
                set { _BilledAmount = value; }
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

            //gloPM5070 For UB-04
            public string RevenueCode
            {
                get { return _RevenueCode; }
                set { _RevenueCode = value; }
            }
            //added by abhisekh on 19 aug 
            //For NDC code implementation
            public string NDCCode
            {
                get { return _NDCCode; }
                set { _NDCCode = value; }
            }
            public Int64 NDCID { get; set; }
            public String NDCCodeQualifier { get; set; }
            //public String NDCCode { get; set; }
            public String NDCDescription { get; set; }
            public String NDCUnitCode { get; set; }
            public String NDCUnitDescription { get; set; }
            public String NDCUnit { get; set; }
            public String NDCUnitPricing { get; set; }
            public String DisplayNDCCode_HCFA { get; set; }
            //Prescription number
            public String Prescription { get; set; }

            public String PrescriptionDescription { get; set; }

            public bool EMG
            { get { return _EMG; } set { _EMG = value; } }

            //Added by Subashish_b on 06/Jan /2011 (integration made on date-10/May/2011) for  creating properties to store PAF Values
            public Int64 PAccountID
            { get { return _nPAccountID; } set { _nPAccountID = value; } }

            public Int64 AccountPatientID
            { get { return _nAccountPatientID; } set { _nAccountPatientID = value; } }

            public Int64 GuarantorID
            { get { return _nGuarantorID; } set { _nGuarantorID = value; } }
            //End

            public Int64 RenderingProviderID
            {
                get;
                set;
            }
            public string RenderingProviderName
            {
                get;
                set;
            }

            #region "Property's for EPSDT/Family planning"

            public bool ServiceIsTheScreening { get; set; }
            public bool ServiceIsTheResultOfScreening { get; set; }
            public bool ServiceFamilyPlanningIndicator { get; set; } 

            #endregion

            #region " Anesthesia"

            public Boolean bIsAneshtesia { get; set; }
            public Int64 AnesthesiaID { get; set; }
            public DateTime? AnesthesiaStartTime { get; set; }
            public DateTime? AnesthesiaEndTime { get; set; }

            public Int64 AnesthesiaTotalMinutes { get; set; }
            public Int64 AnesthesiaMinPerUnit { get; set; }
            public Decimal AnesthesiaTimeUnits { get; set; }
            public Decimal AnesthesiaBaseUnits { get; set; }
            public Decimal AnesthesiaOtherUnits { get; set; }
            public Decimal AnesthesiaTotalUnits { get; set; }
            public Boolean bIsAutoCalculateAnesthesia { get; set; }

            public Int64 ActualTransactionID { get; set; }
            public Int64 ActualTransactionDetailID { get; set; }


            #endregion


            public Int64 EMRTreatmentLineNo
            {
                get;
                set;
            }

            public Boolean bIsSelfClaim { get { return _bIsSelfClaim; } set { _bIsSelfClaim = value; } }
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
            //private BillingNoteType _BillingnoteType = BillingNoteType.ChargesBillingNote;
            private EOBPaymentSubType _BillingnoteType = EOBPaymentSubType.Charges_BillingNote;
            private Int64 _transactionLineId = 0;
            private Int64 _transactionDetailId = 0;
            private Int64 _userID = 0;
            private Int64 _noteDate = 0;
            private Int64 _ClinicID = 0;
            private Int64 _TransactionID = 0;
            private Int64 _statementNoteDate = 0;
            private DateTime _NoteDateTime;
            private string _username;
            private Int32 _noteRowID;
            private DateTime _dtCreatedDatetime ;

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


            public EOBPaymentSubType BillingNoteType
            {
                get { return _BillingnoteType; }
                set { _BillingnoteType = value; }
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

            public Int64 StatementNoteDate
            {
                get { return _statementNoteDate; }
                set { _statementNoteDate = value; }
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

            public DateTime NoteDateTime //20100222
            {
                get { return _NoteDateTime; }
                set { _NoteDateTime = value; }
            }

            public string UserName  //20100222
            {
                get { return _username; }
                set { _username = value; }
            }

            public Int32 NoteRowID //20100222
            {
                get { return _noteRowID; }
                set { _noteRowID = value; }
            }

            public DateTime dtCreatedDatetime
            {
                get { return _dtCreatedDatetime; }
                set { _dtCreatedDatetime = value; }
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

        public class ClaimBox19Note
        {
            #region "Constructor & Destructor"

            public ClaimBox19Note()
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

            ~ClaimBox19Note()
            {
                Dispose(false);
            }

            #endregion

            #region Declaration

            private Int64 _noteID = 0;
            private string _Box19noteDescription = "";
            private NoteType _noteType = NoteType.GeneralNote;
            //private BillingNoteType _BillingnoteType = BillingNoteType.ChargesBillingNote;
            private EOBPaymentSubType _BillingnoteType = EOBPaymentSubType.Charges_BillingNote;
            //private Int64 _transactionLineId = 0;
            //private Int64 _transactionDetailId = 0;
            private Int64 _userID = 0;
            private Int64 _noteDate = 0;
            private Int64 _ClinicID = 0;
            private Int64 _TransactionID = 0;
            private DateTime _NoteDateTime;
            private string _username;
            private Int32 _noteRowID;
            private Boolean _IsReplacementClaim;

            ////Added by Mukesh Patel on 12 Nov 2010

            //private Int64 _IllnessDate;

            ////..End Addedd by Mukesh Patel on 12 Nov 2010


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

            public string Box19NoteDescription
            {
                get { return _Box19noteDescription; }
                set { _Box19noteDescription = value; }
            }

            public NoteType NoteType
            {
                get { return _noteType; }
                set { _noteType = value; }
            }


            public EOBPaymentSubType BillingNoteType
            {
                get { return _BillingnoteType; }
                set { _BillingnoteType = value; }
            }

            //public Int64 TransactionLineId
            //{
            //    get { return _transactionLineId; }
            //    set { _transactionLineId = value; }
            //}
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

            //public Int64 TransactionDetailID
            //{
            //    get { return _transactionDetailId; }
            //    set { _transactionDetailId = value; }
            //}

            public DateTime NoteDateTime //20100222
            {
                get { return _NoteDateTime; }
                set { _NoteDateTime = value; }
            }

            public string UserName  //20100222
            {
                get { return _username; }
                set { _username = value; }
            }

            public Int32 NoteRowID //20100222
            {
                get { return _noteRowID; }
                set { _noteRowID = value; }
            }

            public Boolean IsReplacementClaim
            {
                get { return _IsReplacementClaim; }
                set { _IsReplacementClaim = value; }
            }

            public String sClaimRemittRefNo
            { get; set; }

              //Code Added by Mukesh Patel on 12 Nov 2010

              //public Int64 IllnessDate
              //{ get { return _IllnessDate; } set { _IllnessDate = value; } }

            //..End Code Addedd by Mukesh Patel on 12 Nov 2010
            

            #endregion

        }

        public class ClaimBox19Notes
        {

            protected ArrayList _innerlist;

            #region "Constructor & Distructor"

            public ClaimBox19Notes()
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

            ~ClaimBox19Notes()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(ClaimBox19Note item)
            {
                _innerlist.Add(item);
            }

            //Remark - Work Remaining for comparision
            public bool Remove(ClaimBox19Note item)
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

            public ClaimBox19Note this[int index]
            {
                get
                { return (ClaimBox19Note)_innerlist[index]; }
            }

            public bool Contains(ClaimBox19Note item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(ClaimBox19Note item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(ClaimBox19Note[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }
        public class UB04Data
        {
            #region "Constructor & Destructor"

            public UB04Data()
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

            ~UB04Data()
            {
                Dispose(false);
            }

            #endregion

            #region Declaration
            private Int64  _nID;
            private Int64 _nTransactionMasterID;
            private Int64 _nTransactionID;
            private Int64 _nClaimNo;
            private Boolean _IsModify = false;
            private Boolean _HasOtherData = false;
            private Boolean _TypeofbillDeleted = false;
            private Boolean _AdmitTypeDeleted = false;
            private Boolean _DischargeStatus = false;
            private Boolean _MinDOSDeleted = true;
            string _sTypeofbill;
            string _sConditionCode01="";
            string _sConditionCode02="";
            string _sConditionCode03="";
            string _sConditionCode04="";
            string _sConditionCode05="";
            string _sConditionCode06="";
            string _sConditionCode07="";
            string _sConditionCode08="";
            string _sConditionCode09="";
            string _sConditionCode10="";
            string _sConditionCode11="";
            string _sConditionCode12="";
            string _sOccurrenceCode01="";
            string _sOccurrenceDate01="";
            string _sOccurrenceCode02="";
            string _sOccurrenceDate02="";
            string _sOccurrenceCode03="";
            string _sOccurrenceDate03="";
            string _sOccurrenceCode04="";
            string _sOccurrenceDate04="";
            string _sOccurrenceCode05="";
            string _sOccurrenceDate05="";
            string _sOccurrenceCode06="";
            string _sOccurrenceDate06="";
            string _sOccurrenceCode07="";
            string _sOccurrenceDate07="";
            string _sOccurrenceCode08="";
            string _sOccurrenceDate08="";
            string _sOccurrenceSpanCode01="";
            string _sOccurrenceFromSpanDate01="";
            string _sOccurrenceTOSpanDate01="";
            string _sOccurrenceSpanCode02="";
            string _sOccurrenceFromSpanDate02="";
            string _sOccurrenceToSpanDate02="";
            string _sOccurrenceSpanCode03="";
            string _sOccurrenceFromSpanDate03="";
            string _sOccurrenceToSpanDate03="";
            string _sOccurrenceSpanCode04="";
            string _sOccurrenceFromSpanDate04="";
            string _sOccurrenceToSpanDate04="";
            string _sAdmitDate = "";
            string _sAdmitHour = "";
            string _sAdmissionType = "";
            string _sDischargeHour = "";
            string _sDischargeStatus = "";

            string _sValueCode01="";
            private decimal? _nValueAmount01=null;
            string _sValueCode02="";
            private decimal? _nValueAmount02=null;
            string _sValueCode03="";
            private decimal? _nValueAmount03;
            string _sValueCode04="";
            private decimal? _nValueAmount04;
            string _sValueCode05="";
            private decimal? _nValueAmount05;
            string _sValueCode06="";
            private decimal? _nValueAmount06;
            string _sValueCode07="";
            private decimal? _nValueAmount07;
            string _sValueCode08="";
            private decimal? _nValueAmount08;
            string _sValueCode09="";
            private decimal? _nValueAmount09;
            string _sValueCode10="";
            private decimal? _nValueAmount10;
            string _sValueCode11="";
            private decimal? _nValueAmount11;
            string _sValueCode12="";
            private decimal? _nValueAmount12;
           


            #endregion

            #region Properties

            public Int64 nID
            {
                get { return _nID; }
                set { _nID = value; }
             }
       
            public Int64 nTransactionMasterID
            {
                get { return _nTransactionMasterID; }
                set { _nTransactionMasterID = value; }
            }
           
       
            public Int64 nTransactionID
            {
                get { return _nTransactionID; }
                set { _nTransactionID = value; }
            }
         
            public Int64 nClaimNo
            {
                get { return _nClaimNo; }
                set { _nClaimNo = value; }
            }

            public Boolean IsModify
            {
                get {return _IsModify;}
                set {_IsModify=value; }
            }

            public Boolean HasOtherData
            {
                get { return _HasOtherData; }
                set { _HasOtherData = value; }
            }

            public string sTypeofbill
            {
                get { return _sTypeofbill; }
                set { _sTypeofbill = value; }
            }
            
            public string sConditionCode01
            {
                get { return _sConditionCode01; }
                set { _sConditionCode01 = value; }
            }
           
            public string sConditionCode02
            {
                get { return _sConditionCode02; }
                set { _sConditionCode02 = value; }
            }
            
            public string sConditionCode03
            {
                get { return _sConditionCode03; }
                set { _sConditionCode03 = value; }
            }
           
            public string sConditionCode04
            {
                get { return _sConditionCode04; }
                set { _sConditionCode04 = value; }
            }
            
            public string sConditionCode05
            {
                get { return _sConditionCode05; }
                set { _sConditionCode05 = value; }
            }
            public string sConditionCode06
            {
                get { return _sConditionCode06; }
                set { _sConditionCode06 = value; }
            }
            public string sConditionCode07
            {
                get { return _sConditionCode07; }
                set { _sConditionCode07 = value; }
            }
            public string sConditionCode08
            {
                get { return _sConditionCode08; }
                set { _sConditionCode08 = value; }
            }
            public string sConditionCode09
            {
                get { return _sConditionCode09; }
                set { _sConditionCode09 = value; }
            }
            public string sConditionCode10
            {
                get { return _sConditionCode10; }
                set { _sConditionCode10 = value; }
            }
            public string sConditionCode11
            {
                get { return _sConditionCode11; }
                set { _sConditionCode11 = value; }
            }
            public string sConditionCode12
            {
                get { return _sConditionCode12; }
                set { _sConditionCode12 = value; }
            }
            
            public string sOccurrenceCode01
            {
                get { return _sOccurrenceCode01; }
                set { _sOccurrenceCode01 = value; }
            }
           
            public string sOccurrenceDate01
            {
                get { return _sOccurrenceDate01;}
                set {
                    if (value == "")                    
                        _sOccurrenceDate01 = null ;                    
                    else                    
                        _sOccurrenceDate01 = value;                                 
                    }
            }
            
            public string sOccurrenceCode02
            {
                get { return _sOccurrenceCode02; }
                set { _sOccurrenceCode02 = value; }
            }
            
            public string sOccurrenceDate02
            {
                get { return _sOccurrenceDate02; }
                set {
                    if (value == "")
                        _sOccurrenceDate02 = null;
                    else
                        _sOccurrenceDate02 = value;     
                    }
            }
            
            public string sOccurrenceCode03
            {
                get { return _sOccurrenceCode03; }
                set { _sOccurrenceCode03 = value; }
            }
            
            public string sOccurrenceDate03
            {
                get { return _sOccurrenceDate03; }
                set {
                    if (value == "")
                        _sOccurrenceDate03 = null;
                    else
                        _sOccurrenceDate03 = value;  
                    }
            }
           
            public string sOccurrenceCode04
            {
                get { return _sOccurrenceCode04; }
                set { _sOccurrenceCode04 = value; }
            }
            
            public string sOccurrenceDate04
            {
                get { return _sOccurrenceDate04; }
                set {
                    if (value == "")
                        _sOccurrenceDate04 = null;
                    else
                        _sOccurrenceDate04 = value;                      
                    }
            }
            
            public string sOccurrenceCode05
            {
                get { return _sOccurrenceCode05; }
                set { _sOccurrenceCode05 = value; }
            }
            
            public string sOccurrenceDate05
            {
                get { return  _sOccurrenceDate05; }
                set {
                    if (value == "")
                        _sOccurrenceDate05 = null;
                    else
                        _sOccurrenceDate05 = value;  
                    }
            }
            
            public string sOccurrenceCode06
            {
                get { return _sOccurrenceCode06; }
                set { _sOccurrenceCode06 = value; }
            }
            
            public string sOccurrenceDate06
            {
                get { return _sOccurrenceDate06; }
                set {
                    if (value == "")
                        _sOccurrenceDate06 = null;
                    else
                        _sOccurrenceDate06 = value;  
                    }
            }
            
            public string sOccurrenceCode07
            {
                get { return _sOccurrenceCode07; }
                set { _sOccurrenceCode07 = value; }
            }
            
            public string sOccurrenceDate07
            {
                get { return _sOccurrenceDate07; }
                set {
                      if (value == "")
                        _sOccurrenceDate07 = null;
                    else
                        _sOccurrenceDate07 = value;  
                    }                    
            }
            
            public string sOccurrenceCode08
            {
                get { return _sOccurrenceCode08; }
                set { _sOccurrenceCode08 = value; }
            }
            
            public string sOccurrenceDate08
            {
                get { return _sOccurrenceDate08; }
                set { 
                      if (value == "")
                        _sOccurrenceDate08 = null;
                    else
                        _sOccurrenceDate08 = value;  
                    }                    
            }
            
            public string sOccurrenceSpanCode01
            {
                get { return _sOccurrenceSpanCode01; }
                set { _sOccurrenceSpanCode01 = value; }
            }
            
            public string sOccurrenceFromSpanDate01
            {
                get { return _sOccurrenceFromSpanDate01; }
                set { 
                      if (value == "")
                        _sOccurrenceFromSpanDate01 = null;
                    else
                        _sOccurrenceFromSpanDate01 = value;  
                    }
                   
            }
          
            public string sOccurrenceTOSpanDate01
            {
                get { return _sOccurrenceTOSpanDate01; }
                set {
                    if (value == "")
                        _sOccurrenceTOSpanDate01 = null;
                    else
                        _sOccurrenceTOSpanDate01 = value;  
                    }                    
            }
           
            public string sOccurrenceSpanCode02
            {
                get { return _sOccurrenceSpanCode02; }
                set { _sOccurrenceSpanCode02 = value; }
            }
            
            public string sOccurrenceFromSpanDate02
            {
                get { return _sOccurrenceFromSpanDate02; }
                set { 
                    if (value == "")
                        _sOccurrenceFromSpanDate02 = null;
                    else
                        _sOccurrenceFromSpanDate02 = value;  
                    }     
            }
            
            public string sOccurrenceToSpanDate02
            {
                get { return _sOccurrenceToSpanDate02; }
                set {
                    if (value == "")
                        _sOccurrenceToSpanDate02 = null;
                    else
                        _sOccurrenceToSpanDate02 = value;  
                    }     
            }
            
            public string sOccurrenceSpanCode03
            {
                get { return _sOccurrenceSpanCode03; }
                set { _sOccurrenceSpanCode03 = value; }
            }
            
            public string sOccurrenceFromSpanDate03
            {
                get { return _sOccurrenceFromSpanDate03; }
                set {
                    if (value == "")
                        _sOccurrenceFromSpanDate03 = null;
                    else
                        _sOccurrenceFromSpanDate03 = value;  
                    } 
            }
            
            public string sOccurrenceToSpanDate03
            {
                get { return _sOccurrenceToSpanDate03; }
                set {
                    if (value == "")
                        _sOccurrenceToSpanDate03 = null;
                    else
                        _sOccurrenceToSpanDate03 = value;  
                    }                    
            }
            
            public string sOccurrenceSpanCode04
            {
                get { return _sOccurrenceSpanCode04; }
                set { _sOccurrenceSpanCode04 = value; }
            }
            
            public string sOccurrenceFromSpanDate04
            {
                get { return _sOccurrenceFromSpanDate04; }
                set {
                    if (value == "")
                        _sOccurrenceFromSpanDate04 = null;
                    else
                        _sOccurrenceFromSpanDate04 = value;  
                    }
            }
            
            public string sOccurrenceToSpanDate04
            {
                get {return _sOccurrenceToSpanDate04; }
                set {
                    if (value == "")
                        _sOccurrenceToSpanDate04 = null;
                    else
                        _sOccurrenceToSpanDate04 = value;  
                    }
 
            }

           
            public string sValueCode01
            {
                get { return _sValueCode01; }
                set { _sValueCode01 = value; }
            }
            
            public decimal? nValueAmount01
            {
                get { return _nValueAmount01; }
                set { _nValueAmount01 = value; }
            }
            
            public string sValueCode02
            {
                get { return _sValueCode02; }
                set { _sValueCode02 = value; }
            }

            public decimal? nValueAmount02
            {
                get { return _nValueAmount02; }
                set { _nValueAmount02 = value; }
            }
            
            public string sValueCode03
            {
                get { return _sValueCode03; }
                set { _sValueCode03 = value; }
            }

            public decimal? nValueAmount03
            {
                get { return _nValueAmount03; }
                set { _nValueAmount03 = value; }
            }

            public string sValueCode04
            {
                get { return _sValueCode04; }
                set { _sValueCode04 = value; }
            }

            public decimal? nValueAmount04
            {
                get { return _nValueAmount04; }
                set { _nValueAmount04 = value; }
            }

            public string sValueCode05
            {
                get { return _sValueCode05; }
                set { _sValueCode05 = value; }
            }

            public decimal? nValueAmount05
            {
                get { return _nValueAmount05; }
                set { _nValueAmount05 = value; }
            }

            public string sValueCode06
            {
                get { return _sValueCode06; }
                set { _sValueCode06 = value; }
            }

            public decimal? nValueAmount06
            {
                get { return _nValueAmount06; }
                set { _nValueAmount06 = value; }
            }

            public string sValueCode07
            {
                get { return _sValueCode07; }
                set { _sValueCode07 = value; }
            }

            public decimal? nValueAmount07
            {
                get { return _nValueAmount07; }
                set { _nValueAmount07 = value; }
            }

            public string sValueCode08
            {
                get { return _sValueCode08; }
                set { _sValueCode08 = value; }
            }

            public decimal? nValueAmount08
            {
                get { return _nValueAmount08; }
                set { _nValueAmount08 = value; }
            }

            public string sValueCode09
            {
                get { return _sValueCode09; }
                set { _sValueCode09 = value; }
            }
            public decimal? nValueAmount09
            {
                get { return _nValueAmount09; }
                set { _nValueAmount09 = value; }
            }

            public string sValueCode10
            {
                get { return _sValueCode10; }
                set { _sValueCode10 = value; }
            }

            public decimal? nValueAmount10
            {
                get { return _nValueAmount10; }
                set { _nValueAmount10 = value; }
            }

            public string sValueCode11
            {
                get { return _sValueCode11; }
                set { _sValueCode11 = value; }
            }


            public decimal? nValueAmount11
            {
                get { return _nValueAmount11; }
                set { _nValueAmount11 = value; }
            }

            public string sValueCode12
            {
                get { return _sValueCode12; }
                set { _sValueCode12 = value; }
            }

            public decimal? nValueAmount12
            {
                get { return _nValueAmount12; }
                set { _nValueAmount12 = value; }
            }

            public bool TypeofbillDeleted
            {
                get { return _TypeofbillDeleted; }
                set { _TypeofbillDeleted = value; }
            }
            public bool AdmitTypeDeleted
            {
                get { return _AdmitTypeDeleted; }
                set { _AdmitTypeDeleted = value; }
            }
            public bool DischargeStatusDeleted
            {
                get { return _DischargeStatus; }
                set { _DischargeStatus = value; }
            }
            public bool MinDOSDeleted
            {
                get { return _MinDOSDeleted; }
                set { _MinDOSDeleted = value; }
            }
            public string sAdmitDate
            {
                get { return _sAdmitDate; }
                set { _sAdmitDate = value; }
            }
            public string sAdmitHour
            {
                get { return _sAdmitHour; }
                set { _sAdmitHour = value; }
            }
            public string sAdmissionType
            {
                get { return _sAdmissionType; }
                set { _sAdmissionType = value; }
            }

            public string sDischargeHour
            {
                get { return _sDischargeHour; }
                set { _sDischargeHour = value; }
            }
            public string sDischargeStatus
            {
                get { return _sDischargeStatus; }
                set { _sDischargeStatus = value; }
            } 
          
          //  string _sDischargeStatud = "";
            #endregion

        }

        public class UB04Datas
        {
             protected ArrayList _innerlist;

            #region "Constructor & Distructor"

            public UB04Datas()
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

            ~UB04Datas()
            {
                Dispose(false);
            }
            #endregion

            // Methods Add, Remove, Count , Item of TransactionLine
            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(UB04Data item)
            {
                _innerlist.Add(item);
            }

            //Remark - Work Remaining for comparision
            public bool Remove(UB04Data item)
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

            public UB04Datas this[int index]
            {
                get
                { return (UB04Datas)_innerlist[index]; }
            }

            public bool Contains(UB04Data item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(UB04Data item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(UB04Data[] array, int index)
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
            private String _InsuranceName = "";
            private Int64 _ContactID = 0;
            private int _ResponsibilityType = 0;
            private Int16 _ResponsibilityNo = 0;
            private Int64 _ClinicID = 0;
            private Boolean _IsWorkerComp = false;
            private Decimal _CopayAmt = 0;

            //Added By Pramod Nair For Tracking the Responsiblity for individual Claims
            private Int64 _nTransactionMasterID = 0;
            private string sSubClaimNo = "";

            #endregion

            #region Properties

            public Int64 TransactionId
            {
                get { return _TransactionId; }
                set { _TransactionId = value; }
            }

            public Int64 TransactionMasterID
            {
                get { return _nTransactionMasterID; }
                set { _nTransactionMasterID = value; }
            }

            public String SubClaimNo
            {
                get { return sSubClaimNo; }
                set { sSubClaimNo = value; }
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
            public Int16 ResponsibilityNo
            {
                get { return _ResponsibilityNo; }
                set { _ResponsibilityNo = value; }
            }
            public int ResponsibilityType
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

            public Boolean IsInstitutional
            {
                get ;
                set ;
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

    #region "Old Transaction Objects"
    ////Transaction
    //public class Transaction
    //{
    //    #region "Constructor & Distructor"

    //    private string _databaseconnectionstring = "";

    //    public Transaction(string DatabaseConnectionString)
    //    {
    //        _databaseconnectionstring = DatabaseConnectionString;
    //        _TransactionLines = new TransactionLines();
    //        _TransactionPayment = new TransactionPayment();
    //    }

    //    private bool disposed = false;

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
    //                _TransactionLines.Dispose();
    //                _TransactionPayment.Dispose();
    //            }
    //        }
    //        disposed = true;
    //    }

    //    ~Transaction()
    //    {
    //        Dispose(false);
    //    }

    //    #endregion

    //    /// <summary>
    //    ///     Private Veriables to Used in Property Procedures
    //    /// </summary>
    //    /// <remarks>
    //    ///     
    //    /// </remarks>
    //    //Master
    //    private Int64 _TransactionID;
    //    private TransactionType _TransactionType;
    //    private Int64 _VisitID;
    //    private Int64 _PatientID;
    //    private Int64 _ProviderID;
    //    private String _BillNoPrefix;
    //    private Int64 _BillNo;
    //    private Int64 _BillDate;
    //    private Boolean _IsBlock;

    //    //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //    System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
    //    //
    //    private Int64 _ClinicID = 0;

    //    //LineS
    //    private TransactionLines _TransactionLines;

    //    //Header Payment
    //    private TransactionPayment _TransactionPayment;

    //    //PROVISIONAL payment properties, if in case user need to paid against total lines then this properties will used, otherwise payment will happened from transaction lines 

    //    #region Property Procedures of Transaction Class
    //    /// <summary>
    //    ///    Gets or sets Billing TransactionID
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass TransacrtionID to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 TransactionID
    //    {
    //        get { return _TransactionID; }
    //        set { _TransactionID = value; }
    //    }

    //    public TransactionType TransactionMode
    //    {
    //        get { return _TransactionType; }
    //        set { _TransactionType = value; }
    //    }

    //    public Int64 VisitID
    //    {
    //        get { return _VisitID; }
    //        set { _VisitID = value; }
    //    }
    //    /// <summary>
    //    ///    Gets or sets PatientID
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass PatientID to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 PatientID
    //    {
    //        get { return _PatientID; }
    //        set { _PatientID = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Patient's ProviderID
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Patient's ProviderID to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 ProviderID
    //    {
    //        get { return _ProviderID; }
    //        set { _ProviderID = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Bill No's Prefix
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Bill No's Prefix to set it    
    //    ///     </para>
    //    /// </value>
    //    public String BillNoPrefix
    //    {
    //        get { return _BillNoPrefix; }
    //        set { _BillNoPrefix = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Bill No
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Bill No to set it    
    //    ///     </para>
    //    /// </value>
    //    /// 
    //    public Int64 BillNo
    //    {
    //        get { return _BillNo; }
    //        set { _BillNo = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Bill Date
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Bill Date to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 BillDate
    //    {
    //        get { return _BillDate; }
    //        set { _BillDate = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Block Status of Transaction
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Block Status (true/false) of Transaction to set it    
    //    ///     </para>
    //    /// </value>
    //    public Boolean IsBlock
    //    {
    //        get { return _IsBlock; }
    //        set { _IsBlock = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets ClinicID Transaction
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass ClinicID of Transaction to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 ClinicID
    //    {
    //        get { return _ClinicID; }
    //        set { _ClinicID = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Transaction Lines of Current Transaction
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Transaction Lines  of Transaction to set it    
    //    ///     </para>
    //    /// </value>
    //    public TransactionLines Lines
    //    {
    //        get { return _TransactionLines; }
    //        set { _TransactionLines = value; }
    //    }

    //    public TransactionPayment Payment
    //    {
    //        get { return _TransactionPayment; }
    //        set { _TransactionPayment = value; }
    //    }

    //    #endregion

    //}
    ////Transaction Line
    //public class TransactionLine
    //{


    //    #region "Constructor & Distructor"

    //    public TransactionLine()
    //    {
    //        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //        if (appSettings["ClinicID"] != null)
    //        {
    //            if (appSettings["ClinicID"] != "")
    //            { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
    //            else { _ClinicID = 0; }
    //        }
    //        else
    //        { _ClinicID = 0; }
    //        //
    //        _TransactionLineProcedures = new TransactionLineProcedures();
    //    }

    //    private bool disposed = false;

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
    //                _TransactionLineProcedures.Dispose();
    //            }
    //        }
    //        disposed = true;
    //    }

    //    ~TransactionLine()
    //    {
    //        Dispose(false);
    //    }

    //    #endregion

    //    /// <summary>
    //    ///     Private Veriables to Used in Property Procedures
    //    /// </summary>
    //    /// <remarks>
    //    ///     
    //    /// </remarks>
    //    /// 
    //    //Master
    //    private Int64 _TransactionID;
    //    private Int64 _LineNo;
    //    private Int64 _StartDate;
    //    private Int64 _EndDate;
    //    private Int64 _BillingProviderID;
    //    private Int64 _LocationID;
    //    //Procedure
    //    private TransactionLineProcedures _TransactionLineProcedures;

    //    //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //    System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
    //    //
    //    private Int64 _ClinicID;

    //    #region Property Procedures of TransactionLine

    //    /// <summary>
    //    ///    Gets or sets Billing TransactionID
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass TransacrtionID to set it    
    //    ///     </para>
    //    /// </value>
    //    public long TransactionID
    //    {
    //        get { return _TransactionID; }
    //        set { _TransactionID = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Line / Rows of Bill
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass LineNo to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 LineNo
    //    {
    //        get { return _LineNo; }
    //        set { _LineNo = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets StartDate
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass StartDate 
    //    ///     </para>
    //    /// </value>
    //    public Int64 StartDate
    //    {
    //        get { return _StartDate; }
    //        set { _StartDate = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets EndDate
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass EndDate
    //    ///     </para>
    //    /// </value>
    //    public Int64 EndDate
    //    {
    //        get { return _EndDate; }
    //        set { _EndDate = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets ProviderID of Selected Taransaction Line
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass ProviderID of Selected Taransaction Line
    //    ///     </para>
    //    /// </value>
    //    public Int64 BillingProviderID
    //    {
    //        get { return _BillingProviderID; }
    //        set { _BillingProviderID = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Location of Selected Taransaction Line
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Location of Selected Taransaction Line
    //    ///     </para>
    //    /// </value>
    //    public Int64 Location
    //    {
    //        get { return _LocationID; }
    //        set { _LocationID = value; }
    //    }

    //    public TransactionLineProcedures Procedures
    //    {
    //        get { return _TransactionLineProcedures; }
    //        set { _TransactionLineProcedures = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets ClinicID of Selected Taransaction Line
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass ClinicID of Selected Taransaction Line
    //    ///     </para>
    //    /// </value>
    //    public Int64 ClinicID
    //    {
    //        get { return _ClinicID; }
    //        set { _ClinicID = value; }
    //    }

    //    #endregion

    //}

    ////Transaction Line(s)
    //public class TransactionLines
    //{


    //    protected ArrayList _innerlist;

    //    #region "Constructor & Distructor"



    //    public TransactionLines()
    //    {
    //        _innerlist = new ArrayList();

    //    }

    //    private bool disposed = false;

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

    //            }
    //        }
    //        disposed = true;
    //    }


    //    ~TransactionLines()
    //    {
    //        Dispose(false);
    //    }
    //    #endregion


    //    // Methods Add, Remove, Count , Item of TransactionLine
    //    public int Count
    //    {
    //        get { return _innerlist.Count; }
    //    }

    //    public void Add(TransactionLine item)
    //    {
    //        _innerlist.Add(item);
    //    }


    //    public bool Remove(TransactionLine item)
    //    //Remark - Work Remining for comparision
    //    {
    //        bool result = false;


    //        return result;
    //    }

    //    public bool RemoveAt(int index)
    //    {
    //        bool result = false;
    //        _innerlist.RemoveAt(index);
    //        result = true;
    //        return result;
    //    }

    //    public void Clear()
    //    {
    //        _innerlist.Clear();
    //    }

    //    public TransactionLine this[int index]
    //    {
    //        get
    //        { return (TransactionLine)_innerlist[index]; }
    //    }

    //    public bool Contains(TransactionLine item)
    //    {
    //        return _innerlist.Contains(item);
    //    }

    //    public int IndexOf(TransactionLine item)
    //    {
    //        return _innerlist.IndexOf(item);
    //    }

    //    public void CopyTo(TransactionLine[] array, int index)
    //    {
    //        _innerlist.CopyTo(array, index);
    //    }

    //}

    //#region "Line Procedures with Amount, Modifiers, ICD9s, Taxes"
    ////Transaction - Line Procedure
    //public class TransactionLineProcedure
    //{
    //    #region "Constructor & Distructor"

    //    public TransactionLineProcedure()
    //    {
    //        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //        if (appSettings["ClinicID"] != null)
    //        {
    //            if (appSettings["ClinicID"] != "")
    //            { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
    //            else { _ClinicID = 0; }
    //        }
    //        else
    //        { _ClinicID = 0; }
    //        //

    //        _TransactionLineICD9s = new TransactionLineICD9s();
    //        _TransactionLineModifiers = new TransactionLineModifiers();
    //        _TransactionPayment = new TransactionPayment();
    //    }

    //    private bool disposed = false;

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
    //                _TransactionLineICD9s.Dispose();
    //                _TransactionLineModifiers.Dispose();
    //                _TransactionPayment.Dispose();
    //            }
    //        }
    //        disposed = true;
    //    }

    //    ~TransactionLineProcedure()
    //    {
    //        Dispose(false);
    //    }

    //    #endregion

    //    /// <summary>
    //    ///     Private Veriables to Used in Property Procedures
    //    /// </summary>
    //    /// <remarks>
    //    ///     
    //    /// </remarks>
    //    /// 
    //    private Int64 _TransactionID;
    //    private Int64 _LineNo;
    //    private Int64 _ColumnNo;
    //    private String _ProcedureCode;
    //    private String _ProcedureDescription;
    //    private TransactionLineICD9s _TransactionLineICD9s;
    //    private TransactionLineModifiers _TransactionLineModifiers;
    //    private TransactionPayment _TransactionPayment;

    //    //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //    System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
    //    //
    //    private Int64 _ClinicID = 0;

    //    #region Property Procedures of TransactionLineProcedure
    //    public long TransactionID
    //    {
    //        get { return _TransactionID; }
    //        set { _TransactionID = value; }
    //    }
    //    /// <summary>
    //    ///    Gets or sets Billing LineNo
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Billing LineNo to set it    
    //    ///     </para>
    //    /// </value>
    //    public long LineNo
    //    {
    //        get { return _LineNo; }
    //        set { _LineNo = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Column No of Modifier
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Column No of Modifier to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 ColumnNo
    //    {
    //        get { return _ColumnNo; }
    //        set { _ColumnNo = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Code of Modifier
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Code of Modifier to set it    
    //    ///     </para>
    //    /// </value>
    //    public String ProcedureCode
    //    {
    //        get { return _ProcedureCode; }
    //        set { _ProcedureCode = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Modifier Description
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Modifier Description to set it    
    //    ///     </para>
    //    /// </value>
    //    public String ProcedureDescription
    //    {
    //        get { return _ProcedureDescription; }
    //        set { _ProcedureDescription = value; }
    //    }


    //    public TransactionLineICD9s ICD9s
    //    {
    //        get { return _TransactionLineICD9s; }
    //        set { _TransactionLineICD9s = value; }
    //    }


    //    public TransactionLineModifiers Modifiers
    //    {
    //        get { return _TransactionLineModifiers; }
    //        set { _TransactionLineModifiers = value; }
    //    }


    //    public TransactionPayment Payment
    //    {
    //        get { return _TransactionPayment; }
    //        set { _TransactionPayment = value; }
    //    }


    //    public Int64 ClinicID
    //    {
    //        get { return _ClinicID; }
    //        set { _ClinicID = value; }
    //    }
    //    #endregion
    //}

    ////Transaction - Line Procedure(s)
    //public class TransactionLineProcedures
    //{
    //    protected ArrayList _innerlist;

    //    #region "Constructor & Distructor"



    //    public TransactionLineProcedures()
    //    {
    //        _innerlist = new ArrayList();

    //    }

    //    private bool disposed = false;

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

    //            }
    //        }
    //        disposed = true;
    //    }


    //    ~TransactionLineProcedures()
    //    {
    //        Dispose(false);
    //    }
    //    #endregion


    //    // Methods Add, Remove, Count , Item of TransactionLineModifier
    //    public int Count
    //    {
    //        get { return _innerlist.Count; }
    //    }

    //    public void Add(TransactionLineProcedure item)
    //    {
    //        _innerlist.Add(item);
    //    }


    //    public bool Remove(TransactionLineProcedure item)
    //    //Remark - Work Remining for comparision
    //    {
    //        bool result = false;


    //        return result;
    //    }

    //    public bool RemoveAt(int index)
    //    {
    //        bool result = false;
    //        _innerlist.RemoveAt(index);
    //        result = true;
    //        return result;
    //    }

    //    public void Clear()
    //    {
    //        _innerlist.Clear();
    //    }

    //    public TransactionLineProcedure this[int index]
    //    {
    //        get
    //        { return (TransactionLineProcedure)_innerlist[index]; }
    //    }

    //    public bool Contains(TransactionLineProcedure item)
    //    {
    //        return _innerlist.Contains(item);
    //    }

    //    public int IndexOf(TransactionLineProcedure item)
    //    {
    //        return _innerlist.IndexOf(item);
    //    }

    //    public void CopyTo(TransactionLineProcedure[] array, int index)
    //    {
    //        _innerlist.CopyTo(array, index);
    //    }

    //}

    ////Transaction - Line Modifier
    //public class TransactionLineModifier
    //{
    //    #region "Constructor & Distructor"

    //    public TransactionLineModifier()
    //    {
    //        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //        if (appSettings["ClinicID"] != null)
    //        {
    //            if (appSettings["ClinicID"] != "")
    //            { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
    //            else { _ClinicID = 0; }
    //        }
    //        else
    //        { _ClinicID = 0; }
    //    }

    //    private bool disposed = false;

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

    //            }
    //        }
    //        disposed = true;
    //    }

    //    ~TransactionLineModifier()
    //    {
    //        Dispose(false);
    //    }

    //    #endregion

    //    /// <summary>
    //    ///     Private Veriables to Used in Property Procedures
    //    /// </summary>
    //    /// <remarks>
    //    ///     
    //    /// </remarks>
    //    private Int64 _LineNo;
    //    private Int64 _ColumnNo;
    //    private String _ModifierCode;
    //    private String _ModifierDescription;

    //    //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //    System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
    //    private Int64 _ClinicID = 0;

    //    #region Property Procedures of TransactionLineModifier

    //    /// <summary>
    //    ///    Gets or sets Billing LineNo
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Billing LineNo to set it    
    //    ///     </para>
    //    /// </value>
    //    public long LineNo
    //    {
    //        get { return _LineNo; }
    //        set { _LineNo = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Column No of Modifier
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Column No of Modifier to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 ColumnNo
    //    {
    //        get { return _ColumnNo; }
    //        set { _ColumnNo = value; }
    //    }


    //    /// <summary>
    //    ///    Gets or sets Code of Modifier
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Code of Modifier to set it    
    //    ///     </para>
    //    /// </value>
    //    public String ModifierCode
    //    {
    //        get { return _ModifierCode; }
    //        set { _ModifierCode = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Modifier Description
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Modifier Description to set it    
    //    ///     </para>
    //    /// </value>
    //    public String ModifierDescription
    //    {
    //        get { return _ModifierDescription; }
    //        set { _ModifierDescription = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Clinic ID
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Clinic ID to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 ClinicID
    //    {
    //        get { return _ClinicID; }
    //        set { _ClinicID = value; }
    //    }
    //    #endregion
    //}

    ////Transaction - Line Modifier(s)
    //public class TransactionLineModifiers
    //{
    //    protected ArrayList _innerlist;

    //    #region "Constructor & Distructor"



    //    public TransactionLineModifiers()
    //    {
    //        _innerlist = new ArrayList();

    //    }

    //    private bool disposed = false;

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

    //            }
    //        }
    //        disposed = true;
    //    }


    //    ~TransactionLineModifiers()
    //    {
    //        Dispose(false);
    //    }
    //    #endregion


    //    // Methods Add, Remove, Count , Item of TransactionLineModifier
    //    public int Count
    //    {
    //        get { return _innerlist.Count; }
    //    }

    //    public void Add(TransactionLineModifier item)
    //    {
    //        _innerlist.Add(item);
    //    }


    //    public bool Remove(TransactionLineModifier item)
    //    //Remark - Work Remining for comparision
    //    {
    //        bool result = false;


    //        return result;
    //    }

    //    public bool RemoveAt(int index)
    //    {
    //        bool result = false;
    //        _innerlist.RemoveAt(index);
    //        result = true;
    //        return result;
    //    }

    //    public void Clear()
    //    {
    //        _innerlist.Clear();
    //    }

    //    public TransactionLineModifier this[int index]
    //    {
    //        get
    //        { return (TransactionLineModifier)_innerlist[index]; }
    //    }

    //    public bool Contains(TransactionLineModifier item)
    //    {
    //        return _innerlist.Contains(item);
    //    }

    //    public int IndexOf(TransactionLineModifier item)
    //    {
    //        return _innerlist.IndexOf(item);
    //    }

    //    public void CopyTo(TransactionLineModifier[] array, int index)
    //    {
    //        _innerlist.CopyTo(array, index);
    //    }

    //}

    ////Transaction - Line ICD9/Dignosis
    //public class TransactionLineICD9
    //{
    //    #region "Constructor & Distructor"

    //    public TransactionLineICD9()
    //    {
    //        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //        if (appSettings["ClinicID"] != null)
    //        {
    //            if (appSettings["ClinicID"] != "")
    //            { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
    //            else { _ClinicID = 0; }
    //        }
    //        else
    //        { _ClinicID = 0; }

    //    }

    //    private bool disposed = false;

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

    //            }
    //        }
    //        disposed = true;
    //    }

    //    ~TransactionLineICD9()
    //    {
    //        Dispose(false);
    //    }

    //    #endregion
    //    /// <summary>
    //    ///     Private Veriables to Used in Property Procedures
    //    /// </summary>
    //    /// <remarks>
    //    ///     
    //    /// </remarks>
    //    private Int64 _LineNo;
    //    private Int64 _ColumnNo;
    //    private String _ICD9Code;
    //    private String _ICD9Description;

    //    //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //    System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
    //    private Int64 _ClinicID = 0;

    //    /// <summary>
    //    ///    Gets or sets Line No 
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Line No to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 LineNo
    //    {
    //        get { return _LineNo; }
    //        set { _LineNo = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Column No 
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Column No to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 ColumnNo
    //    {
    //        get { return _ColumnNo; }
    //        set { _ColumnNo = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets ICD9 Code
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass ICD9 Code to set it    
    //    ///     </para>
    //    /// </value>
    //    public String ICD9Code
    //    {
    //        get { return _ICD9Code; }
    //        set { _ICD9Code = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets ICD9 Description
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass ICD9 Description to set it    
    //    ///     </para>
    //    /// </value>
    //    public String ICD9Description
    //    {
    //        get { return _ICD9Description; }
    //        set { _ICD9Description = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Clinic ID
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Clinic ID to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 ClinicID
    //    {
    //        get { return _ClinicID; }
    //        set { _ClinicID = value; }
    //    }
    //}

    ////Transaction - Line ICD9(s)/Dignosis
    //public class TransactionLineICD9s
    //{
    //    protected ArrayList _innerlist;

    //    #region "Constructor & Distructor"



    //    public TransactionLineICD9s()
    //    {
    //        _innerlist = new ArrayList();

    //    }

    //    private bool disposed = false;

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

    //            }
    //        }
    //        disposed = true;
    //    }


    //    ~TransactionLineICD9s()
    //    {
    //        Dispose(false);
    //    }
    //    #endregion


    //    // Methods Add, Remove, Count , Item of TransactionLineICD9
    //    public int Count
    //    {
    //        get { return _innerlist.Count; }
    //    }

    //    public void Add(TransactionLineICD9 item)
    //    {
    //        _innerlist.Add(item);
    //    }


    //    public bool Remove(TransactionLineICD9 item)
    //    //Remark - Work Remining for comparision
    //    {
    //        bool result = false;


    //        return result;
    //    }

    //    public bool RemoveAt(int index)
    //    {
    //        bool result = false;
    //        _innerlist.RemoveAt(index);
    //        result = true;
    //        return result;
    //    }

    //    public void Clear()
    //    {
    //        _innerlist.Clear();
    //    }

    //    public TransactionLineICD9 this[int index]
    //    {
    //        get
    //        { return (TransactionLineICD9)_innerlist[index]; }
    //    }

    //    public bool Contains(TransactionLineICD9 item)
    //    {
    //        return _innerlist.Contains(item);
    //    }

    //    public int IndexOf(TransactionLineICD9 item)
    //    {
    //        return _innerlist.IndexOf(item);
    //    }

    //    public void CopyTo(TransactionLineICD9[] array, int index)
    //    {
    //        _innerlist.CopyTo(array, index);
    //    }

    //}

    //#endregion

    //// currently not in used
    //#region "Transaction Header Payments"
    ////Transaction - Line Procedure
    //public class TransactionPayment
    //{
    //    #region "Constructor & Distructor"

    //    public TransactionPayment()
    //    {
    //        _TransactionLineTaxes = new TransactionLineTaxes();
    //        _TransactionLinePayments = new TransactionLinePayments();
    //        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //        if (appSettings["ClinicID"] != null)
    //        {
    //            if (appSettings["ClinicID"] != "")
    //            { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
    //            else { _ClinicID = 0; }
    //        }
    //        else
    //        { _ClinicID = 0; }

    //    }

    //    private bool disposed = false;

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
    //                _TransactionLineTaxes.Dispose();
    //                _TransactionLinePayments.Dispose();
    //            }
    //        }
    //        disposed = true;
    //    }

    //    ~TransactionPayment()
    //    {
    //        Dispose(false);
    //    }

    //    #endregion

    //    /// <summary>
    //    ///     Private Veriables to Used in Property Procedures
    //    /// </summary>
    //    /// <remarks>
    //    ///     
    //    /// </remarks>
    //    /// 
    //    private Int64 _TransactionID;
    //    private Int64 _LineNo;
    //    private Int64 _ColumnNo;
    //    private Int64 _CPTAutoNo;
    //    private Decimal _RateAmount;
    //    private Decimal _UnitQuantity;
    //    private Decimal _SubTotalAmount;
    //    private TransactionLineTaxes _TransactionLineTaxes;
    //    private Decimal _TotalAmount;
    //    private TransactionLinePayments _TransactionLinePayments;

    //    //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //    System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
    //    //
    //    private Int64 _ClinicID = 0;

    //    #region Property Procedures of TransactionLineProcedure

    //    public long TransactionID
    //    {
    //        get { return _TransactionID; }
    //        set { _TransactionID = value; }
    //    }
    //    /// <summary>
    //    ///    Gets or sets Billing LineNo
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Billing LineNo to set it    
    //    ///     </para>
    //    /// </value>
    //    public long LineNo
    //    {
    //        get { return _LineNo; }
    //        set { _LineNo = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Column No of Modifier
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Column No of Modifier to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 ColumnNo
    //    {
    //        get { return _ColumnNo; }
    //        set { _ColumnNo = value; }
    //    }

    //    public Int64 CPTAutoNo
    //    {
    //        get { return _CPTAutoNo; }
    //        set { _CPTAutoNo = value; }
    //    }

    //    public Decimal RateAmount
    //    {
    //        get { return _RateAmount; }
    //        set { _RateAmount = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Unit/Quantity of Selected Taransaction Line
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Unit/Quantity of Selected Taransaction Line
    //    ///     </para>
    //    /// </value>
    //    public Decimal UnitQuantity
    //    {
    //        get { return _UnitQuantity; }
    //        set { _UnitQuantity = value; }
    //    }

    //    public Decimal SubTotalAmount
    //    {
    //        get { return _SubTotalAmount; }
    //        set { _SubTotalAmount = value; }
    //    }

    //    public TransactionLineTaxes Taxes
    //    {
    //        get { return _TransactionLineTaxes; }
    //        set { _TransactionLineTaxes = value; }
    //    }

    //    public Decimal TotalAmount
    //    {
    //        get { return _TotalAmount; }
    //        set { _TotalAmount = value; }
    //    }

    //    public TransactionLinePayments Payments
    //    {
    //        get { return _TransactionLinePayments; }
    //        set { _TransactionLinePayments = value; }
    //    }


    //    /// <summary>
    //    ///    Gets or sets Clinic ID
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Clinic ID to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 ClinicID
    //    {
    //        get { return _ClinicID; }
    //        set { _ClinicID = value; }
    //    }
    //    #endregion
    //}

    ////Transaction - Payment(s)
    //public class TransactionPayments
    //{
    //    protected ArrayList _innerlist;

    //    #region "Constructor & Distructor"



    //    public TransactionPayments()
    //    {
    //        _innerlist = new ArrayList();

    //    }

    //    private bool disposed = false;

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

    //            }
    //        }
    //        disposed = true;
    //    }


    //    ~TransactionPayments()
    //    {
    //        Dispose(false);
    //    }
    //    #endregion


    //    // Methods Add, Remove, Count , Item of TransactionPayment
    //    public int Count
    //    {
    //        get { return _innerlist.Count; }
    //    }

    //    public void Add(TransactionPayment item)
    //    {
    //        _innerlist.Add(item);
    //    }


    //    public bool Remove(TransactionPayment item)
    //    //Remark - Work Remining for comparision
    //    {
    //        bool result = false;


    //        return result;
    //    }

    //    public bool RemoveAt(int index)
    //    {
    //        bool result = false;
    //        _innerlist.RemoveAt(index);
    //        result = true;
    //        return result;
    //    }

    //    public void Clear()
    //    {
    //        _innerlist.Clear();
    //    }

    //    public TransactionPayment this[int index]
    //    {
    //        get
    //        { return (TransactionPayment)_innerlist[index]; }
    //    }

    //    public bool Contains(TransactionPayment item)
    //    {
    //        return _innerlist.Contains(item);
    //    }

    //    public int IndexOf(TransactionPayment item)
    //    {
    //        return _innerlist.IndexOf(item);
    //    }

    //    public void CopyTo(TransactionPayment[] array, int index)
    //    {
    //        _innerlist.CopyTo(array, index);
    //    }


    //}

    ////Transaction - Line Tax
    //public class TransactionLineTax
    //{
    //    #region "Constructor & Distructor"

    //    public TransactionLineTax()
    //    {
    //        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //        if (appSettings["ClinicID"] != null)
    //        {
    //            if (appSettings["ClinicID"] != "")
    //            { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
    //            else { _ClinicID = 0; }
    //        }
    //        else
    //        { _ClinicID = 0; }
    //    }

    //    private bool disposed = false;

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

    //            }
    //        }
    //        disposed = true;
    //    }

    //    ~TransactionLineTax()
    //    {
    //        Dispose(false);
    //    }

    //    #endregion

    //    /// <summary>
    //    ///     Private Veriables to Used in Property Procedures
    //    /// </summary>
    //    /// <remarks>
    //    ///     
    //    /// </remarks>
    //    private Int64 _LineNo;
    //    private Int64 _ColumnNo;
    //    private Int64 _TaxID;
    //    private TaxType _TaxType;
    //    private String _TaxName = "";
    //    private Decimal _TaxPercentage;
    //    private Decimal _TaxAmount;

    //    //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //    System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
    //    //
    //    private Int64 _ClinicID = 0;


    //    /// <summary>
    //    ///    Gets or sets Billing LineNo
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Billing LineNo to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 LineNo
    //    {
    //        get { return _LineNo; }
    //        set { _LineNo = value; }
    //    }


    //    /// <summary>
    //    ///    Gets or sets Column No 
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Column No to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 ColumnNo
    //    {
    //        get { return _ColumnNo; }
    //        set { _ColumnNo = value; }
    //    }


    //    /// <summary>
    //    ///    Gets or sets TaxID
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass TaxID to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 TaxID
    //    {
    //        get { return _TaxID; }
    //        set { _TaxID = value; }
    //    }



    //    /// <summary>
    //    ///    Gets or sets TaxType
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass TaxType to set it    
    //    ///     </para>
    //    /// </value>
    //    public TaxType TaxType
    //    {
    //        get { return _TaxType; }
    //        set { _TaxType = value; }
    //    }


    //    /// <summary>
    //    ///    Gets or sets Tax Name
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Tax Name to set it    
    //    ///     </para>
    //    /// </value>
    //    public String TaxName
    //    {
    //        get { return _TaxName; }
    //        set { _TaxName = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Tax Percentage
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Tax Percentage to set it    
    //    ///     </para>
    //    /// </value>
    //    public Decimal TaxPercentage
    //    {
    //        get { return _TaxPercentage; }
    //        set { _TaxPercentage = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Tax Amount
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Tax Amount to set it    
    //    ///     </para>
    //    /// </value>
    //    public Decimal TaxAmount
    //    {
    //        get { return _TaxAmount; }
    //        set { _TaxAmount = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Clinic ID
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass ClinicID to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 ClinicID
    //    {
    //        get { return _ClinicID; }
    //        set { _ClinicID = value; }
    //    }


    //}

    ////Transaction - Line Tax(s)
    //public class TransactionLineTaxes
    //{
    //    protected ArrayList _innerlist;

    //    #region "Constructor & Distructor"



    //    public TransactionLineTaxes()
    //    {
    //        _innerlist = new ArrayList();

    //    }

    //    private bool disposed = false;

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

    //            }
    //        }
    //        disposed = true;
    //    }


    //    ~TransactionLineTaxes()
    //    {
    //        Dispose(false);
    //    }
    //    #endregion


    //    // Methods Add, Remove, Count , Item of TransactionLineTax
    //    public int Count
    //    {
    //        get { return _innerlist.Count; }
    //    }

    //    public void Add(TransactionLineTax item)
    //    {
    //        _innerlist.Add(item);
    //    }


    //    public bool Remove(TransactionLineTax item)
    //    //Remark - Work Remining for comparision
    //    {
    //        bool result = false;


    //        return result;
    //    }

    //    public bool RemoveAt(int index)
    //    {
    //        bool result = false;
    //        _innerlist.RemoveAt(index);
    //        result = true;
    //        return result;
    //    }

    //    public void Clear()
    //    {
    //        _innerlist.Clear();
    //    }

    //    public TransactionLineTax this[int index]
    //    {
    //        get
    //        { return (TransactionLineTax)_innerlist[index]; }
    //    }

    //    public bool Contains(TransactionLineTax item)
    //    {
    //        return _innerlist.Contains(item);
    //    }

    //    public int IndexOf(TransactionLineTax item)
    //    {
    //        return _innerlist.IndexOf(item);
    //    }

    //    public void CopyTo(TransactionLineTax[] array, int index)
    //    {
    //        _innerlist.CopyTo(array, index);
    //    }

    //}

    ////Transaction - Line Procedure
    //public class TransactionLinePayment
    //{
    //    #region "Constructor & Distructor"

    //    public TransactionLinePayment()
    //    {
    //        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //        if (appSettings["ClinicID"] != null)
    //        {
    //            if (appSettings["ClinicID"] != "")
    //            { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
    //            else { _ClinicID = 0; }
    //        }
    //        else
    //        { _ClinicID = 0; }
    //    }

    //    private bool disposed = false;

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

    //            }
    //        }
    //        disposed = true;
    //    }

    //    ~TransactionLinePayment()
    //    {
    //        Dispose(false);
    //    }

    //    #endregion

    //    /// <summary>
    //    ///     Private Veriables to Used in Property Procedures
    //    /// </summary>
    //    /// <remarks>
    //    ///     
    //    /// </remarks>
    //    /// 
    //    private Int64 _TransactionID;
    //    private Int64 _LineNo;
    //    private Int64 _ColumnNo;
    //    private PaymentMode _PaymentMode;
    //    private PaymentModeType _PaymentModeType;
    //    private Decimal _PaymentPercentage = 100;
    //    private Decimal _PaymentAmount;
    //    private String _PaymentNotes = "";

    //    //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
    //    System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
    //    //
    //    private Int64 _ClinicID = 0;

    //    #region Property Procedures of TransactionLineProcedure

    //    public long TransactionID
    //    {
    //        get { return _TransactionID; }
    //        set { _TransactionID = value; }
    //    }
    //    /// <summary>
    //    ///    Gets or sets Billing LineNo
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Billing LineNo to set it    
    //    ///     </para>
    //    /// </value>
    //    public long LineNo
    //    {
    //        get { return _LineNo; }
    //        set { _LineNo = value; }
    //    }

    //    /// <summary>
    //    ///    Gets or sets Column No of Modifier
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Column No of Modifier to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 ColumnNo
    //    {
    //        get { return _ColumnNo; }
    //        set { _ColumnNo = value; }
    //    }

    //    public PaymentMode PaymentMode
    //    {
    //        get { return _PaymentMode; }
    //        set { _PaymentMode = value; }
    //    }

    //    public PaymentModeType PaymentModeType
    //    {
    //        get { return _PaymentModeType; }
    //        set { _PaymentModeType = value; }
    //    }

    //    public decimal PaymentPercentage
    //    {
    //        get { return _PaymentPercentage; }
    //        set { _PaymentPercentage = value; }
    //    }

    //    public decimal PaymentAmount
    //    {
    //        get { return _PaymentAmount; }
    //        set { _PaymentAmount = value; }
    //    }

    //    public string PaymentNotes
    //    {
    //        get { return _PaymentNotes; }
    //        set { _PaymentNotes = value; }
    //    }


    //    /// <summary>
    //    ///    Gets or sets Clinic ID
    //    /// </summary>
    //    /// <value>
    //    ///     <para>
    //    ///    Pass Clinic ID to set it    
    //    ///     </para>
    //    /// </value>
    //    public Int64 ClinicID
    //    {
    //        get { return _ClinicID; }
    //        set { _ClinicID = value; }
    //    }
    //    #endregion
    //}

    ////Transaction - Payment(s)
    //public class TransactionLinePayments
    //{
    //    protected ArrayList _innerlist;

    //    #region "Constructor & Distructor"



    //    public TransactionLinePayments()
    //    {
    //        _innerlist = new ArrayList();

    //    }

    //    private bool disposed = false;

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

    //            }
    //        }
    //        disposed = true;
    //    }


    //    ~TransactionLinePayments()
    //    {
    //        Dispose(false);
    //    }
    //    #endregion


    //    // Methods Add, Remove, Count , Item of TransactionPayment
    //    public int Count
    //    {
    //        get { return _innerlist.Count; }
    //    }

    //    public void Add(TransactionLinePayment item)
    //    {
    //        _innerlist.Add(item);
    //    }


    //    public bool Remove(TransactionLinePayment item)
    //    //Remark - Work Remining for comparision
    //    {
    //        bool result = false;


    //        return result;
    //    }

    //    public bool RemoveAt(int index)
    //    {
    //        bool result = false;
    //        _innerlist.RemoveAt(index);
    //        result = true;
    //        return result;
    //    }

    //    public void Clear()
    //    {
    //        _innerlist.Clear();
    //    }

    //    public TransactionLinePayment this[int index]
    //    {
    //        get
    //        { return (TransactionLinePayment)_innerlist[index]; }
    //    }

    //    public bool Contains(TransactionLinePayment item)
    //    {
    //        return _innerlist.Contains(item);
    //    }

    //    public int IndexOf(TransactionLinePayment item)
    //    {
    //        return _innerlist.IndexOf(item);
    //    }

    //    public void CopyTo(TransactionLinePayment[] array, int index)
    //    {
    //        _innerlist.CopyTo(array, index);
    //    }


    //}

    //#endregion

    #endregion

    public class ClaimHold
    {

        #region "Constructor & Destructor"

        public ClaimHold()
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

        ~ClaimHold()
        {
            Dispose(false);
        }

        #endregion

        #region Declaration

        private Int64 _nHoldID = 0;
        private Int64 _nTransactionMasterID = 0;
        private Int64 _nTransactionID = 0;
        private Int64 _nClaimID = 0;
        private Int64 _nSubClaimID = 0;
        private string _sHoldReason = "";
        private DateTime _dtHoldDateTime = DateTime.Now;
        private DateTime _dtHoldModDateTime = DateTime.Now;
        private Int64 _nHoldUserID = 0;
        private Int64 _nHoldModUserID = 0;
        private Boolean _bHoldModified = false;
        private Int64 _ClinicID = 0;
        private Boolean _bIsHold = false;

        #endregion

        #region Properties

        public Int64 HoldID
        {
            get { return _nHoldID; }
            set { _nHoldID = value; }
        }

        public Int64 TransactionMasterID
        {
            get { return _nTransactionMasterID; }
            set { _nTransactionMasterID = value; }
        }

        public Int64 TransactionID
        {
            get { return _nTransactionID; }
            set { _nTransactionID = value; }
        }

        public Int64 ClaimID
        {
            get { return _nClaimID; }
            set { _nClaimID = value; }
        }


        public Int64 SubClaimID
        {
            get { return _nSubClaimID; }
            set { _nSubClaimID = value; }
        }

        public string HoldReason
        {
            get { return _sHoldReason; }
            set { _sHoldReason = value; }
        }
        public Int64 HoldUserID
        {
            get { return _nHoldUserID; }
            set { _nHoldUserID = value; }
        }
        public Int64 HoldModUserID
        {
            get { return _nHoldModUserID; }
            set { _nHoldModUserID = value; }
        }
        public Boolean HoldModified
        {
            get { return _bHoldModified; }
            set { _bHoldModified = value; }
        }
        public Boolean IsHold
        {
            get { return _bIsHold; }
            set { _bIsHold = value; }
        }
        public DateTime HoldDateTime
        {
            get { return _dtHoldDateTime; }
            set { _dtHoldDateTime = value; }
        }
        public DateTime HoldModDateTime
        {
            get { return _dtHoldModDateTime; }
            set { _dtHoldModDateTime = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion

    }

    public class CPTDetails
    {
        public int nSLNo;
        public string sCPT;
        public DateTime dtStartDate;
    }

    public class CPTCollection : List<CPTDetails>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var sdr = new SqlDataRecord(
             new SqlMetaData("nSLNo", SqlDbType.Int),
             new SqlMetaData("sCPT", SqlDbType.VarChar,255),
             new SqlMetaData("dtStartDate", SqlDbType.Date));

            foreach (CPTDetails cpt in this)
            {
                sdr.SetInt32(0, cpt.nSLNo);
                sdr.SetString(1, cpt.sCPT);
                sdr.SetDateTime(2, cpt.dtStartDate);
                yield return sdr;
            }
        }
    }

    public class EPSDTFamilyPlanningClaim
    {
        public bool ClaimIncludeEPSDTScreening { get; set; }
        public bool PatientGivenEPSDTReferral { get; set; }
        public String ReferralType { get; set; }
        public String ReferralCode { get; set; }
    }
}
