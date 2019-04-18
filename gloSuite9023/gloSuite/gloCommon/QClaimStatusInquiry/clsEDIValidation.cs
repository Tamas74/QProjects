using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;





namespace TriArqEDIRealTimeClaimStatus
{
    class ClsEDIValidation
    {
        #region CONSTRUCTOR AND DESCTRUCTOR

        public ClsEDIValidation()
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

        ~ClsEDIValidation()
        {
            Dispose(false);
        }
        #endregion

        #region "Private Variables"

        #region " Clearing House "
        private Boolean _SenderID;
        private Boolean _ReceiverID;
        private Boolean _SenderCode;
        private Boolean _ReceiverCode;
        #endregion

        #region " Submitter "
        private Boolean _SubmitterName;
        private Boolean _SubmitterContactName;
        private Boolean _SubmitterPhone;
        private Boolean _SubmitterCity;
        private Boolean _SubmitterState;
        private Boolean _SubmitterZIP;
        private Boolean _SubmitterAddress1;

        #endregion

        #region " Subscriber  "
        private Boolean _SubscriberLastName;
        private Boolean _SubscriberRelationship;
        private Boolean _PlanType;
        private Boolean _InsuranceTypeCode;
        private Boolean _SubscriberFirstName;
        private Boolean _InsuranceID;
        private Boolean _SubscriberAddress;
        private Boolean _SubscriberGroupID;
        private Boolean _SubscriberCity;
        private Boolean _SubscriberState;
        private Boolean _SubscriberZip;
        private Boolean _SubscriberDOB;
        private Boolean _SubscriberGender;

        #endregion

        #region " Payer "

        private Boolean _PayerName;
        private Boolean _PayerId;
        private Boolean _PayerAddress;
        private Boolean _PayerCity;
        private Boolean _PayerState;
        private Boolean _PayerZip;

        #endregion

        #region "Billing Provider"

        private Boolean _BillingFirstName;
        private Boolean _BillingLastName;
        private Boolean _BillingMiddleName;
        private Boolean _BillingCity;
        private Boolean _BillingState;
        private Boolean _BillingAddress;
        private Boolean _BillingZIP;
        private Boolean _BillingNPI;
        private Boolean _BillingSSN;
        private Boolean _BillingEmployerID;
        private Boolean _BillingStateMedicalNo;
        private Boolean _BillingTaxonomy;
        private Boolean _BillingCompanyTax;


        #endregion

        #region "Facility"

        private Boolean _FacilityName;
        private Boolean _FacilityAddress1;
        private Boolean _FacilityCity;
        private Boolean _FacilityState;
        private Boolean _FacilityZip;
        private Boolean _FacilityNPI;

        #endregion

        #region " Secondary Insurance "
        private Boolean _SecondarySubLName;
        private Boolean _SecondaryPlanType;
        private Boolean _SecondaryRelationshipCode;
        private Boolean _SecondaryInsuranceID;
        private Boolean _SecondaryGroupId;
        private Boolean _SecondaryInsAddress;
        private Boolean _SecondarySubFName;
        private Boolean _SecondaryInsName;
        private Boolean _SecondaryInsPayerID;
        private Boolean _SecondarySubCity;
        private Boolean _SecondarySubState;
        private Boolean _SecondarySubZip;
        private Boolean _SecondarySubDOB;
        private Boolean _SecondarySubGender;
        private Boolean _SecondarySubInsType;

        #endregion

        #region " Tertiary Insurance "

        private Boolean _TertiarySubLName;
        private Boolean _TertiaryPlanType;
        private Boolean _TertiaryRelationshipCode;
        private Boolean _TertiaryInsuranceID;
        private Boolean _TertiaryGroupId;
        private Boolean _TertiaryInsAddress;
        private Boolean _TertiarySubFName;
        private Boolean _TertiaryInsName;
        private Boolean _TertiaryInsPayerID;
        private Boolean _TertiarySubCity;
        private Boolean _TertiarySubState;
        private Boolean _TertiarySubZip;
        private Boolean _TertiarySubDOB;
        private Boolean _TertiarySubGender;
        private Boolean _TertiarySubInsType;

        #endregion

        #region " Patient Information "

        private Boolean _PatientAccNo;
        private Boolean _PatientLastName;
        private Boolean _PatientFirstName;
        private Boolean _PatientMiddleName;
        private Boolean _PatientSSN;
        private Boolean _PatientGender;
        private Boolean _PatientDOB;
        private Boolean _PatientAddress;
        private Boolean _PatientCity;
        private Boolean _PatientState;
        private Boolean _PatientZip;

        #endregion

        #region " Rendering Provider "

        private Boolean _RenderingProLastName;
        private Boolean _RenderingProFirstName;
        private Boolean _RenderingProNPI;
        private Boolean _RenderingTaxonomy;

        #endregion

        #region " Referring Provider  "

        private Boolean _ReferringProLastName;
        private Boolean _ReferringProFirstName;
        private Boolean _ReferringProNPI;
        private Boolean _ReferringTaxonomy;
        private Boolean _ReferringTaxId;

        #endregion

        #endregion

        #region " Property Procedures "

        #region " Clearing House "

        public Boolean SenderID
        {
            get { return _SenderID; }
            set { _SenderID = value; }
        }

        public Boolean ReceiverID
        {
            get { return _ReceiverID; }
            set { _ReceiverID = value; }
        }

        public Boolean SenderCode
        {
            get { return _SenderCode; }
            set { _SenderCode = value; }
        }
        public Boolean ReceiverCode
        {
            get { return _ReceiverCode; }
            set { _ReceiverCode = value; }
        }

        #endregion

        #region " Submitter "

        public Boolean SubmitterName
        {
            get { return _SubmitterName; }
            set { _SubmitterName = value; }
        }

        public Boolean SubmitterContactName
        {
            get { return _SubmitterContactName; }
            set { _SubmitterContactName = value; }
        }

        public Boolean SubmitterPhone
        {
            get { return _SubmitterPhone; }
            set { _SubmitterPhone = value; }
        }
        public Boolean SubmitterCity
        {
            get { return _SubmitterCity; }
            set { _SubmitterCity = value; }
        }


        public Boolean SubmitterState
        {
            get { return _SubmitterState; }
            set { _SubmitterState = value; }
        }

        public Boolean SubmitterZIP
        {
            get { return _SubmitterZIP; }
            set { _SubmitterZIP = value; }
        }
        public Boolean SubmitterAddress1
        {
            get { return _SubmitterAddress1; }
            set { _SubmitterAddress1 = value; }
        }

        #endregion

        #region " Subscriber  "

        public Boolean SubscriberLastName
        {
            get { return _SubscriberLastName; }
            set { _SubscriberLastName = value; }
        }

        public Boolean SubscriberRelationship
        {
            get { return _SubscriberRelationship; }
            set { _SubscriberRelationship = value; }
        }

        public Boolean PlanType
        {
            get { return _PlanType; }
            set { _PlanType = value; }
        }

        public Boolean InsuranceTypeCode
        {
            get { return _InsuranceTypeCode; }
            set { _InsuranceTypeCode = value; }
        }

        public Boolean SubscriberFirstName
        {
            get { return _SubscriberFirstName; }
            set { _SubscriberFirstName = value; }
        }

        public Boolean InsuranceID
        {
            get { return _InsuranceID; }
            set { _InsuranceID = value; }
        }

        public Boolean SubscriberAddress
        {
            get { return _SubscriberAddress; }
            set { _SubscriberAddress = value; }
        }

        public Boolean SubscriberGroupID
        {
            get { return _SubscriberGroupID; }
            set { _SubscriberGroupID = value; }
        }
        public Boolean SubscriberCity
        {
            get { return _SubscriberCity; }
            set { _SubscriberCity = value; }
        }

        public Boolean SubscriberState
        {
            get { return _SubscriberState; }
            set { _SubscriberState = value; }
        }

        public Boolean SubscriberZip
        {
            get { return _SubscriberZip; }
            set { _SubscriberZip = value; }
        }
        public Boolean SubscriberDOB
        {
            get { return _SubscriberDOB; }
            set { _SubscriberDOB = value; }
        }
        public Boolean SubscriberGender
        {
            get { return _SubscriberGender; }
            set { _SubscriberGender = value; }
        }
        #endregion

        #region " Payer "

        public Boolean PayerName
        {
            get { return _PayerName; }
            set { _PayerName = value; }
        }

        public Boolean PayerId
        {
            get { return _PayerId; }
            set { _PayerId = value; }
        }

        public Boolean PayerAddress
        {
            get { return _PayerAddress; }
            set { _PayerAddress = value; }
        }
        public Boolean PayerCity
        {
            get { return _PayerCity; }
            set { _PayerCity = value; }
        }

        public Boolean PayerState
        {
            get { return _PayerState; }
            set { _PayerState = value; }
        }

        public Boolean PayerZip
        {
            get { return _PayerZip; }
            set { _PayerZip = value; }
        }

        #endregion

        #region "Billing Provider"


        public Boolean BillingFirstName
        {
            get { return _BillingFirstName; }
            set { _BillingFirstName = value; }
        }

        public Boolean BillingLastName
        {
            get { return _BillingLastName; }
            set { _BillingLastName = value; }
        }

        public Boolean BillingMiddleName
        {
            get { return _BillingMiddleName; }
            set { _BillingMiddleName = value; }
        }
        public Boolean BillingCity
        {
            get { return _BillingCity; }
            set { _BillingCity = value; }
        }

        public Boolean BillingState
        {
            get { return _BillingState; }
            set { _BillingState = value; }
        }

        public Boolean BillingAddress
        {
            get { return _BillingAddress; }
            set { _BillingAddress = value; }
        }
        public Boolean BillingZIP
        {
            get { return _BillingZIP; }
            set { _BillingZIP = value; }
        }



        public Boolean BillingNPI
        {
            get { return _BillingNPI; }
            set { _BillingNPI = value; }
        }
        public Boolean BillingSSN
        {
            get { return _BillingSSN; }
            set { _BillingSSN = value; }
        }

        public Boolean BillingEmployerID
        {
            get { return _BillingEmployerID; }
            set { _BillingEmployerID = value; }
        }

        public Boolean BillingStateMedicalNo
        {
            get { return _BillingStateMedicalNo; }
            set { _BillingStateMedicalNo = value; }
        }
        public Boolean BillingTaxonomy
        {
            get { return _BillingTaxonomy; }
            set { _BillingTaxonomy = value; }
        }

        public Boolean BillingCompanyTax
        {
            get { return _BillingCompanyTax; }
            set { _BillingCompanyTax = value; }
        }




        #endregion

        #region "Facility"

        public Boolean FacilityName
        {
            get { return _FacilityName; }
            set { _FacilityName = value; }
        }

        public Boolean FacilityAddress1
        {
            get { return _FacilityAddress1; }
            set { _FacilityAddress1 = value; }
        }

        public Boolean FacilityCity
        {
            get { return _FacilityCity; }
            set { _FacilityCity = value; }
        }
        public Boolean FacilityState
        {
            get { return _FacilityState; }
            set { _FacilityState = value; }
        }
        public Boolean FacilityZip
        {
            get { return _FacilityZip; }
            set { _FacilityZip = value; }
        }
        public Boolean FacilityNPI
        {
            get { return _FacilityNPI; }
            set { _FacilityNPI = value; }
        }


        #endregion


        #region " Secondary Insurance "

        public Boolean SecondarySubLName
        {
            get { return _SecondarySubLName; }
            set { _SecondarySubLName = value; }
        }

        public Boolean SecondaryPlanType
        {
            get { return _SecondaryPlanType; }
            set { _SecondaryPlanType = value; }
        }

        public Boolean SecondaryRelationshipCode
        {
            get { return _SecondaryRelationshipCode; }
            set { _SecondaryRelationshipCode = value; }
        }
        public Boolean SecondaryInsuranceID
        {
            get { return _SecondaryInsuranceID; }
            set { _SecondaryInsuranceID = value; }
        }

        public Boolean SecondaryGroupId
        {
            get { return _SecondaryGroupId; }
            set { _SecondaryGroupId = value; }
        }

        public Boolean SecondaryInsAddress
        {
            get { return _SecondaryInsAddress; }
            set { _SecondaryInsAddress = value; }
        }

        public Boolean SecondarySubFName
        {
            get { return _SecondarySubFName; }
            set { _SecondarySubFName = value; }
        }
        public Boolean SecondaryInsName
        {
            get { return _SecondaryInsName; }
            set { _SecondaryInsName = value; }
        }
        public Boolean SecondaryInsPayerID
        {
            get { return _SecondaryInsPayerID; }
            set { _SecondaryInsPayerID = value; }
        }

        public Boolean SecondarySubCity
        {
            get { return _SecondarySubCity; }
            set { _SecondarySubCity = value; }
        }

        public Boolean SecondarySubState
        {
            get { return _SecondarySubState; }
            set { _SecondarySubState = value; }
        }

        public Boolean SecondarySubZip
        {
            get { return _SecondarySubZip; }
            set { _SecondarySubZip = value; }
        }

        public Boolean SecondarySubDOB
        {
            get { return _SecondarySubDOB; }
            set { _SecondarySubDOB = value; }
        }

        public Boolean SecondarySubGender
        {
            get { return _SecondarySubGender; }
            set { _SecondarySubGender = value; }
        }

        public Boolean SecondarySubInsType
        {
            get { return _SecondarySubInsType; }
            set { _SecondarySubInsType = value; }
        }
        #endregion

        #region " Tertiary Insurance "

        public Boolean TertiarySubLName
        {
            get { return _TertiarySubLName; }
            set { _TertiarySubLName = value; }
        }

        public Boolean TertiaryPlanType
        {
            get { return _TertiaryPlanType; }
            set { _TertiaryPlanType = value; }
        }

        public Boolean TertiaryRelationshipCode
        {
            get { return _TertiaryRelationshipCode; }
            set { _TertiaryRelationshipCode = value; }
        }
        public Boolean TertiaryInsuranceID
        {
            get { return _TertiaryInsuranceID; }
            set { _TertiaryInsuranceID = value; }
        }

        public Boolean TertiaryGroupId
        {
            get { return _TertiaryGroupId; }
            set { _TertiaryGroupId = value; }
        }

        public Boolean TertiaryInsAddress
        {
            get { return _TertiaryInsAddress; }
            set { _TertiaryInsAddress = value; }
        }

        public Boolean TertiarySubFName
        {
            get { return _TertiarySubFName; }
            set { _TertiarySubFName = value; }
        }
        public Boolean TertiaryInsName
        {
            get { return _TertiaryInsName; }
            set { _TertiaryInsName = value; }
        }
        public Boolean TertiaryInsPayerID
        {
            get { return _TertiaryInsPayerID; }
            set { _TertiaryInsPayerID = value; }
        }

        public Boolean TertiarySubCity
        {
            get { return _TertiarySubCity; }
            set { _TertiarySubCity = value; }
        }

        public Boolean TertiarySubState
        {
            get { return _TertiarySubState; }
            set { _TertiarySubState = value; }
        }

        public Boolean TertiarySubZip
        {
            get { return _TertiarySubZip; }
            set { _TertiarySubZip = value; }
        }

        public Boolean TertiarySubDOB
        {
            get { return _TertiarySubDOB; }
            set { _TertiarySubDOB = value; }
        }

        public Boolean TertiarySubGender
        {
            get { return _TertiarySubGender; }
            set { _TertiarySubGender = value; }
        }

        public Boolean TertiarySubInsType
        {
            get { return _TertiarySubInsType; }
            set { _TertiarySubInsType = value; }
        }
        #endregion

        #region " Patient Information "

        public Boolean PatientAccNo
        {
            get { return _PatientAccNo; }
            set { _PatientAccNo = value; }
        }

        public Boolean PatientLastName
        {
            get { return _PatientLastName; }
            set { _PatientLastName = value; }
        }

        public Boolean PatientFirstName
        {
            get { return _PatientFirstName; }
            set { _PatientFirstName = value; }
        }
        public Boolean PatientMiddleName
        {
            get { return _PatientMiddleName; }
            set { _PatientMiddleName = value; }
        }

        public Boolean PatientSSN
        {
            get { return _PatientSSN; }
            set { _PatientSSN = value; }
        }

        public Boolean PatientGender
        {
            get { return _PatientGender; }
            set { _PatientGender = value; }
        }

        public Boolean PatientDOB
        {
            get { return _PatientDOB; }
            set { _PatientDOB = value; }
        }
        public Boolean PatientAddress
        {
            get { return _PatientAddress; }
            set { _PatientAddress = value; }
        }
        public Boolean PatientCity
        {
            get { return _PatientCity; }
            set { _PatientCity = value; }
        }

        public Boolean PatientState
        {
            get { return _PatientState; }
            set { _PatientState = value; }
        }

        public Boolean PatientZip
        {
            get { return _PatientZip; }
            set { _PatientZip = value; }
        }

        #endregion

        #region " Rendering Provider "

        public Boolean RenderingProLastName
        {
            get { return _RenderingProLastName; }
            set { _RenderingProLastName = value; }
        }

        public Boolean RenderingProFirstName
        {
            get { return _RenderingProFirstName; }
            set { _RenderingProFirstName = value; }
        }

        public Boolean RenderingProNPI
        {
            get { return _RenderingProNPI; }
            set { _RenderingProNPI = value; }
        }
        public Boolean RenderingTaxonomy
        {
            get { return _RenderingTaxonomy; }
            set { _RenderingTaxonomy = value; }
        }

        #endregion

        #region " Referring Provider  "

        public Boolean ReferringProLastName
        {
            get { return _ReferringProLastName; }
            set { _ReferringProLastName = value; }
        }

        public Boolean ReferringProFirstName
        {
            get { return _ReferringProFirstName; }
            set { _ReferringProFirstName = value; }
        }

        public Boolean ReferringProNPI
        {
            get { return _ReferringProNPI; }
            set { _ReferringProNPI = value; }
        }
        public Boolean ReferringTaxonomy
        {
            get { return _ReferringTaxonomy; }
            set { _ReferringTaxonomy = value; }
        }
        public Boolean ReferringTaxId
        {
            get { return _ReferringTaxId; }
            set { _ReferringTaxId = value; }
        }

        #endregion

        #endregion " Property Procedures "

        public void GetEDIValidation(string _databaseconnectionstring)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            DataTable _result = null;
            try
            {
                oDB.Connect(false);
                _strSQL = "SELECT  sSettingsName,sSettingsValue FROM BL_Settings_EDI";
                oDB.Retrive_Query(_strSQL, out _result);

                if (_result != null)
                {
                    for (int i = 0; i <= _result.Rows.Count - 1; i++)
                    {

                        #region " Clearing House "

                        if (_result.Rows[i]["sSettingsName"].ToString() == "Sender ID")
                        {
                            _SenderID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Receiver ID")
                        {
                            _ReceiverID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Sender Code")
                        {
                            _SenderCode = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Receiver Code")
                        {
                            _ReceiverCode = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Submitter "

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter Name")
                        {
                            _SubmitterName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter Contact Person Name")
                        {
                            _SubmitterContactName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter Contact Person Number")
                        {
                            _SubmitterPhone = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter City")
                        {
                            _SubmitterCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter State")
                        {
                            _SubmitterState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter Zip")
                        {
                            _SubmitterZIP = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter Address")
                        {
                            _SubmitterAddress1 = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Subscriber  "

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Last Name")
                        {
                            _SubscriberLastName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Relationship")
                        {
                            _SubscriberRelationship = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Plan Type")
                        {
                            _PlanType = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Insurance Type")
                        {
                            _InsuranceTypeCode = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber First Name")
                        {
                            _SubscriberFirstName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Insurance ID")
                        {
                            _InsuranceID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Address")
                        {
                            _SubscriberAddress = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Group ID")
                        {
                            _SubscriberGroupID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber City")
                        {
                            _SubscriberCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber State")
                        {
                            _SubscriberState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Zip")
                        {
                            _SubscriberZip = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Date of Birth")
                        {
                            _SubscriberDOB = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Gender")
                        {
                            _SubscriberGender = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Payer "

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Payer Name")
                        {
                            _PayerName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Payer ID")
                        {
                            _PayerId = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Payer Address")
                        {
                            _PayerAddress = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Payer City")
                        {
                            _PayerCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Payer State")
                        {
                            _PayerState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Payer Zip")
                        {
                            _PayerZip = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region "Billing Provider"

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider First Name")
                        {
                            _BillingFirstName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Last Name")
                        {
                            _BillingLastName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Middle Name")
                        {
                            _BillingMiddleName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider City")
                        {
                            _BillingCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider State")
                        {
                            _BillingState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Address")
                        {
                            _BillingAddress = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Zip")
                        {
                            _BillingZIP = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider NPI")
                        {
                            _BillingNPI = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider SSN")
                        {
                            _BillingSSN = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Employer ID")
                        {
                            _BillingEmployerID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Taxonomy")
                        {
                            _BillingTaxonomy = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider State Medical No")
                        {
                            _BillingStateMedicalNo = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Company Tax ID")
                        {
                            _BillingCompanyTax = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region "Facility"


                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Facility Name")
                        {
                            _FacilityName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Facility Address")
                        {
                            _FacilityAddress1 = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Facility City")
                        {
                            _FacilityCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Facility State")
                        {
                            _FacilityState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Facility Zip")
                        {
                            _FacilityZip = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Facility NPI")
                        {
                            _FacilityNPI = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }


                        #endregion

                        #region " Secondary Insurance "


                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Subscriber Last Name")
                        {
                            _SecondarySubLName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Plan Type")
                        {
                            _SecondaryPlanType = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Subscriber Relationship")
                        {
                            _SecondaryRelationshipCode = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance ID")
                        {
                            _SecondaryInsuranceID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Group ID")
                        {
                            _SecondaryGroupId = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Address")
                        {
                            _SecondaryInsAddress = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Subscriber First Name")
                        {
                            _SecondarySubFName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Name")
                        {
                            _SecondaryInsName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Payer ID")
                        {
                            _SecondaryInsPayerID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance City")
                        {
                            _SecondarySubCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance State")
                        {
                            _SecondarySubState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Zip")
                        {
                            _SecondarySubZip = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Subscriber Date of Birth")
                        {
                            _SecondarySubDOB = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Subscriber Gender")
                        {
                            _SecondarySubGender = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Type")
                        {
                            _SecondarySubInsType = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Tertiary  Insurance "


                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Subscriber Last Name")
                        {
                            _TertiarySubLName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Plan Type")
                        {
                            _TertiaryPlanType = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Subscriber Relationship")
                        {
                            _TertiaryRelationshipCode = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance ID")
                        {
                            _TertiaryInsuranceID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Group ID")
                        {
                            _TertiaryGroupId = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Address")
                        {
                            _TertiaryInsAddress = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Subscriber First Name")
                        {
                            _TertiarySubFName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Name")
                        {
                            _TertiaryInsName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Payer ID")
                        {
                            _TertiaryInsPayerID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance City")
                        {
                            _TertiarySubCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance State")
                        {
                            _TertiarySubState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Zip")
                        {
                            _TertiarySubZip = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Subscriber Date of Birth")
                        {
                            _TertiarySubDOB = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Subscriber Gender")
                        {
                            _TertiarySubGender = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Type")
                        {
                            _TertiarySubInsType = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Patient Information "

                        //else if (_result.Rows[i]["sSettingsName"].ToString().Contains("Patient Account No"))
                        //{
                        //    _PatientAccNo = _result.Rows[i]["sSettingsValue"];
                        //}

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient Last Name")
                        {
                            _PatientLastName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient First Name")
                        {
                            _PatientFirstName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient Middle Name")
                        {
                            _PatientMiddleName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient SSN")
                        {
                            _PatientSSN = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient Gender")
                        {
                            _PatientGender = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient Date of Birth")
                        {
                            _PatientDOB = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient Address")
                        {
                            _PatientAddress = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient City")
                        {
                            _PatientCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient State")
                        {
                            _PatientState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient Zip")
                        {
                            _PatientZip = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Rendering Provider "

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Rendering Provider Last Name")
                        {
                            _RenderingProLastName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString().Contains("Rendering Provider First Name"))
                        {
                            _RenderingProFirstName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString().Contains("Rendering Provider NPI"))
                        {
                            _RenderingProNPI = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Rendering Provider Taxonomy Code")
                        {
                            _RenderingTaxonomy = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Referring Provider  "

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Referring Provider Last Name")
                        {
                            _ReferringProLastName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Referring Provider First Name")
                        {
                            _ReferringProFirstName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Referring Provider NPI")
                        {
                            _ReferringProNPI = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Referring Provider Taxonomy")
                        {
                            _ReferringTaxonomy = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Referring Provider TaxID")
                        {
                            _ReferringTaxId = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion
                    }
                }

            }
            catch (Exception)
            {
               // gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

        }

        public void GetEDIValidation(DataTable _result)
        {
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //string _strSQL = "";
            //DataTable _result = null;
            try
            {
                //oDB.Connect(false);
                //_strSQL = "SELECT  sSettingsName,sSettingsValue FROM BL_Settings_EDI";
                //oDB.Retrive_Query(_strSQL, out _result);

                if (_result != null)
                {
                    for (int i = 0; i <= _result.Rows.Count - 1; i++)
                    {

                        #region " Clearing House "

                        if (_result.Rows[i]["sSettingsName"].ToString() == "Sender ID")
                        {
                            _SenderID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Receiver ID")
                        {
                            _ReceiverID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Sender Code")
                        {
                            _SenderCode = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Receiver Code")
                        {
                            _ReceiverCode = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Submitter "

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter Name")
                        {
                            _SubmitterName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter Contact Person Name")
                        {
                            _SubmitterContactName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter Contact Person Number")
                        {
                            _SubmitterPhone = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter City")
                        {
                            _SubmitterCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter State")
                        {
                            _SubmitterState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter Zip")
                        {
                            _SubmitterZIP = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Submitter Address")
                        {
                            _SubmitterAddress1 = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Subscriber  "

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Last Name")
                        {
                            _SubscriberLastName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Relationship")
                        {
                            _SubscriberRelationship = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Plan Type")
                        {
                            _PlanType = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Insurance Type")
                        {
                            _InsuranceTypeCode = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber First Name")
                        {
                            _SubscriberFirstName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Insurance ID")
                        {
                            _InsuranceID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Address")
                        {
                            _SubscriberAddress = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Group ID")
                        {
                            _SubscriberGroupID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber City")
                        {
                            _SubscriberCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber State")
                        {
                            _SubscriberState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Zip")
                        {
                            _SubscriberZip = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Date of Birth")
                        {
                            _SubscriberDOB = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Subscriber Gender")
                        {
                            _SubscriberGender = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Payer "

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Payer Name")
                        {
                            _PayerName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Payer ID")
                        {
                            _PayerId = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Payer Address")
                        {
                            _PayerAddress = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Payer City")
                        {
                            _PayerCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Payer State")
                        {
                            _PayerState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Payer Zip")
                        {
                            _PayerZip = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region "Billing Provider"

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider First Name")
                        {
                            _BillingFirstName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Last Name")
                        {
                            _BillingLastName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Middle Name")
                        {
                            _BillingMiddleName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider City")
                        {
                            _BillingCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider State")
                        {
                            _BillingState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Address")
                        {
                            _BillingAddress = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Zip")
                        {
                            _BillingZIP = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider NPI")
                        {
                            _BillingNPI = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider SSN")
                        {
                            _BillingSSN = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Employer ID")
                        {
                            _BillingEmployerID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Taxonomy")
                        {
                            _BillingTaxonomy = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider State Medical No")
                        {
                            _BillingStateMedicalNo = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Billing Provider Company Tax ID")
                        {
                            _BillingCompanyTax = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region "Facility"


                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Facility Name")
                        {
                            _FacilityName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Facility Address")
                        {
                            _FacilityAddress1 = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Facility City")
                        {
                            _FacilityCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Facility State")
                        {
                            _FacilityState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Facility Zip")
                        {
                            _FacilityZip = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Facility NPI")
                        {
                            _FacilityNPI = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }


                        #endregion

                        #region " Secondary Insurance "


                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Subscriber Last Name")
                        {
                            _SecondarySubLName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Plan Type")
                        {
                            _SecondaryPlanType = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Subscriber Relationship")
                        {
                            _SecondaryRelationshipCode = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance ID")
                        {
                            _SecondaryInsuranceID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Group ID")
                        {
                            _SecondaryGroupId = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Address")
                        {
                            _SecondaryInsAddress = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Subscriber First Name")
                        {
                            _SecondarySubFName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Name")
                        {
                            _SecondaryInsName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Payer ID")
                        {
                            _SecondaryInsPayerID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance City")
                        {
                            _SecondarySubCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance State")
                        {
                            _SecondarySubState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Zip")
                        {
                            _SecondarySubZip = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Subscriber Date of Birth")
                        {
                            _SecondarySubDOB = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Subscriber Gender")
                        {
                            _SecondarySubGender = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Secondary Insurance Type")
                        {
                            _SecondarySubInsType = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Tertiary  Insurance "


                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Subscriber Last Name")
                        {
                            _TertiarySubLName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Plan Type")
                        {
                            _TertiaryPlanType = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Subscriber Relationship")
                        {
                            _TertiaryRelationshipCode = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance ID")
                        {
                            _TertiaryInsuranceID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Group ID")
                        {
                            _TertiaryGroupId = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Address")
                        {
                            _TertiaryInsAddress = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Subscriber First Name")
                        {
                            _TertiarySubFName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Name")
                        {
                            _TertiaryInsName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Payer ID")
                        {
                            _TertiaryInsPayerID = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance City")
                        {
                            _TertiarySubCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance State")
                        {
                            _TertiarySubState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Zip")
                        {
                            _TertiarySubZip = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Subscriber Date of Birth")
                        {
                            _TertiarySubDOB = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Subscriber Gender")
                        {
                            _TertiarySubGender = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Tertiary Insurance Type")
                        {
                            _TertiarySubInsType = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Patient Information "

                        //else if (_result.Rows[i]["sSettingsName"].ToString().Contains("Patient Account No"))
                        //{
                        //    _PatientAccNo = _result.Rows[i]["sSettingsValue"];
                        //}

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient Last Name")
                        {
                            _PatientLastName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient First Name")
                        {
                            _PatientFirstName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient Middle Name")
                        {
                            _PatientMiddleName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient SSN")
                        {
                            _PatientSSN = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient Gender")
                        {
                            _PatientGender = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient Date of Birth")
                        {
                            _PatientDOB = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient Address")
                        {
                            _PatientAddress = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient City")
                        {
                            _PatientCity = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient State")
                        {
                            _PatientState = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Patient Zip")
                        {
                            _PatientZip = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Rendering Provider "

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Rendering Provider Last Name")
                        {
                            _RenderingProLastName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString().Contains("Rendering Provider First Name"))
                        {
                            _RenderingProFirstName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString().Contains("Rendering Provider NPI"))
                        {
                            _RenderingProNPI = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Rendering Provider Taxonomy Code")
                        {
                            _RenderingTaxonomy = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region " Referring Provider  "

                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Referring Provider Last Name")
                        {
                            _ReferringProLastName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Referring Provider First Name")
                        {
                            _ReferringProFirstName = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Referring Provider NPI")
                        {
                            _ReferringProNPI = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Referring Provider Taxonomy")
                        {
                            _ReferringTaxonomy = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "Referring Provider TaxID")
                        {
                            _ReferringTaxId = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion
                    }
                }

            }
            catch (Exception)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (_result != null) { _result.Dispose(); }
            }

        }
    }

    class ClsAlphaValidation
    {
        #region CONSTRUCTOR AND DESCTRUCTOR

        public ClsAlphaValidation()
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

        ~ClsAlphaValidation()
        {
            Dispose(false);
        }
        #endregion

        #region "Private Variables"

        #region " Alpha "
        private string _AlphaServerName;
        private string _AlphaDatabaseName;
        private string _AlphaAuthentication;
        private string _AlphaUserName;
        private string _AlphaPassword;

        #endregion

        #region "Other setting"

        private string _ClaimValidationSetting;
        private Boolean _IsCheckInvalidICD9;
        private Boolean _IsUseScrubber;
        private Boolean _ShowMessageForValidation;

        #endregion

        #endregion

        #region "Properties"

        #region "Alpha"

        public string AlphaServerName
        {
            get { return _AlphaServerName; }
            set { _AlphaServerName = value; }
        }
        public string AlphaDatabaseName
        {
            get { return _AlphaDatabaseName; }
            set { _AlphaDatabaseName = value; }
        }
        public string AlphaAuthentication
        {
            get { return _AlphaAuthentication; }
            set { _AlphaAuthentication = value; }
        }
        public string AlphaUserName
        {
            get { return _AlphaUserName; }
            set { _AlphaUserName = value; }
        }
        public string AlphaPassword
        {
            get { return _AlphaPassword; }
            set { _AlphaPassword = value; }
        }

        #endregion

        #region "Other Setting "

        public string ClaimValidationSetting
        {
            get { return _ClaimValidationSetting; }
            set { _ClaimValidationSetting = value; }
        }

        public Boolean IsCheckInvalidICD9
        {
            get { return _IsCheckInvalidICD9; }
            set { _IsCheckInvalidICD9 = value; }
        }
        public Boolean IsUseScrubber
        {
            get { return _IsUseScrubber; }
            set { _IsUseScrubber = value; }
        }
        public Boolean ShowMessageForValidation
        {
            get { return _ShowMessageForValidation; }
            set { _ShowMessageForValidation = value; }
        }

        #endregion

        #endregion

        public void GetAlphaValidation(string _databaseconnectionstring, long _ClinicId)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            DataTable _result = null;
            try
            {
                oDB.Connect(false);
                _strSQL = "SELECT UPPER(sSettingsName)  AS sSettingsName,ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE UPPER(sSettingsName) = 'ALPHAII SQL SERVER NAME' " +
                " OR UPPER(sSettingsName) = 'ALPHAII DATABASE NAME' OR UPPER(sSettingsName) = 'ALPHAII AUTHENTICATION' OR UPPER(sSettingsName) = 'ALPHAII USER NAME'" +
                " OR UPPER(sSettingsName) = 'ALPHAII PASSWORD' OR UPPER(sSettingsName) = 'CLAIMVALIDATIONSETTING' OR UPPER(sSettingsName) = 'ISCHECKINVALIDICD9'" +
                " OR UPPER(sSettingsName) = 'ISUSESCRUBBER' OR UPPER(sSettingsName) = 'SHOWMESSAGEIFNOVALIDATION'" +
                " AND nClinicID = " + _ClinicId + "";
                oDB.Retrive_Query(_strSQL, out _result);

                if (_result != null)
                {
                    for (int i = 0; i <= _result.Rows.Count - 1; i++)
                    {
                        #region "Alpha"
                        if (_result.Rows[i]["sSettingsName"].ToString() == "ALPHAII SQL SERVER NAME")
                        {
                            _AlphaServerName = Convert.ToString(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "ALPHAII DATABASE NAME")
                        {
                            _AlphaDatabaseName = Convert.ToString(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "ALPHAII AUTHENTICATION")
                        {
                            _AlphaAuthentication = Convert.ToString(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "ALPHAII USER NAME")
                        {
                            _AlphaUserName = Convert.ToString(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "ALPHAII PASSWORD")
                        {
                            _AlphaPassword = Convert.ToString(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region "Other Setting"
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "CLAIMVALIDATIONSETTING")
                        {
                            _ClaimValidationSetting = Convert.ToString(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "ISCHECKINVALIDICD9")
                        {
                            _IsCheckInvalidICD9 = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "ISUSESCRUBBER")
                        {
                            _IsUseScrubber = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "SHOWMESSAGEIFNOVALIDATION")
                        {
                            _ShowMessageForValidation = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        #endregion
                    }
                }
            }
            catch (Exception)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }

        public void GetAlphaValidation(DataTable _result)
        {

            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //string _strSQL = "";
            //DataTable _result = null;
            try
            {
                //oDB.Connect(false);
                //_strSQL = "SELECT UPPER(sSettingsName)  AS sSettingsName,ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE UPPER(sSettingsName) = 'ALPHAII SQL SERVER NAME' " +
                //" OR UPPER(sSettingsName) = 'ALPHAII DATABASE NAME' OR UPPER(sSettingsName) = 'ALPHAII AUTHENTICATION' OR UPPER(sSettingsName) = 'ALPHAII USER NAME'" +
                //" OR UPPER(sSettingsName) = 'ALPHAII PASSWORD' OR UPPER(sSettingsName) = 'CLAIMVALIDATIONSETTING' OR UPPER(sSettingsName) = 'ISCHECKINVALIDICD9'" +
                //" OR UPPER(sSettingsName) = 'ISUSESCRUBBER' OR UPPER(sSettingsName) = 'SHOWMESSAGEIFNOVALIDATION'" +
                //" AND nClinicID = " + _ClinicId + "";
                //oDB.Retrive_Query(_strSQL, out _result);

                if (_result != null)
                {
                    for (int i = 0; i <= _result.Rows.Count - 1; i++)
                    {
                        #region "Alpha"
                        if (_result.Rows[i]["sSettingsName"].ToString() == "ALPHAII SQL SERVER NAME")
                        {
                            _AlphaServerName = Convert.ToString(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "ALPHAII DATABASE NAME")
                        {
                            _AlphaDatabaseName = Convert.ToString(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "ALPHAII AUTHENTICATION")
                        {
                            _AlphaAuthentication = Convert.ToString(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "ALPHAII USER NAME")
                        {
                            _AlphaUserName = Convert.ToString(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "ALPHAII PASSWORD")
                        {
                            _AlphaPassword = Convert.ToString(_result.Rows[i]["sSettingsValue"]);
                        }

                        #endregion

                        #region "Other Setting"
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "CLAIMVALIDATIONSETTING")
                        {
                            _ClaimValidationSetting = Convert.ToString(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "ISCHECKINVALIDICD9")
                        {
                            _IsCheckInvalidICD9 = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "ISUSESCRUBBER")
                        {
                            _IsUseScrubber = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        else if (_result.Rows[i]["sSettingsName"].ToString() == "SHOWMESSAGEIFNOVALIDATION")
                        {
                            _ShowMessageForValidation = Convert.ToBoolean(_result.Rows[i]["sSettingsValue"]);
                        }
                        #endregion
                    }
                }
            }
            catch (Exception)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (_result != null) { _result.Dispose(); }
            }
        }

    }
}
