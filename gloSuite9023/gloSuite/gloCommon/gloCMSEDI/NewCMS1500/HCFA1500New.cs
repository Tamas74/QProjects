using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Printing;
using System.IO;
using gloCMSEDI.NewCMS1500;
//Test Comment

namespace gloCMSEDI
{
    //public enum AddressType
    //{
    //    None = 0,
    //    ProviderAddress = 1,
    //    FacilityAddress = 2,
    //    ClinicAddress = 3
    //}

    //public enum NPIType
    //{
    //    None = 0,
    //    BillingProviderNPI = 1,
    //    FacilityNPI = 2,
    //    ClinicNPI = 3,
    //}


    public partial class HCFA1500New : UserControl
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //cp.ExStyle &=~0x02000000;
                cp.ExStyle &=~0x00000020;//WS_EX_TRANSPARENT
                return cp;
            }
        }

        public HCFA1500New(String DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Get Clinic Information "
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt = new DataTable();
            String _sqlQuery = "Select ISNULL(sClinicName,'') as sClinicName,ISNULL(sAddress1,'') as sAddress1,ISNULL(sAddress2,'') as sAddress2,ISNULL(sStreet,'') as sStreet,ISNULL(sCity,'') as sCity,ISNULL(sState,'') as sState,ISNULL(sZIP,'') as sZIP,ISNULL(sAreaCode,'') AS sAreaCode,ISNULL(sPhoneNo,'') as sPhoneNo,ISNULL(sClinicNPI,'') as sClinicNPI " +
                        " from Clinic_MST WITH(NOLOCK) where nClinicID=" + _ClinicID;
            oDB.Retrive_Query(_sqlQuery, out dt);
            oDB.Disconnect();
            if (dt != null && dt.Rows.Count > 0)
            {
                _ClinicName = Convert.ToString(dt.Rows[0]["sClinicName"]);
                _ClinicAddress = Convert.ToString(dt.Rows[0]["sAddress1"]) + " " + Convert.ToString(dt.Rows[0]["sAddress2"]);
                _ClinicStreet = Convert.ToString(dt.Rows[0]["sStreet"]);
                _ClinicCity = Convert.ToString(dt.Rows[0]["sCity"]);
                _ClinicState = Convert.ToString(dt.Rows[0]["sState"]);
                _ClinicZip = Convert.ToString(dt.Rows[0]["sZIP"]);
                _ClinicAreaCode = Convert.ToString(dt.Rows[0]["sAreaCode"]);
                _ClinicPhone = Convert.ToString(dt.Rows[0]["sPhoneNo"]);
                // -------------------------------
                // Code added by Pankaj Bedse on 13012010
                // To make Clinic Phone as a single string of Numbers only
                _ClinicPhone = _ClinicPhone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                // -------------------------------
                _ClinicNPI = Convert.ToString(dt.Rows[0]["sClinicNPI"]);
            }
            dt = null;
            oDB.Dispose();
            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion

            InitializeComponent();

            // Code added by Pankaj Bedse 04012010 to Hide TextBox Panel & Display ReadOnly Panel.
            pnlTextBox.Visible = false;
            pnlLabel.Visible = true;
          
        }


        #region " Variables "

        
        private string _databaseconnectionstring = "";
        
        private string _messageboxcaption = "";       
        private Transaction oTransaction = null;
      
       
        string _strPatientStatus = "";
        bool IsSecondaryInsurance = false;
        
        Int64 _ClinicID = 0;
        string _ClinicName = "";
        string _ClinicAddress = "";
        string _ClinicStreet = "";
        string _ClinicCity = "";
        string _ClinicState = "";
        string _ClinicZip = "";
        string _ClinicAreaCode = "";
        string _ClinicPhone = "";
        string _ClinicNPI = "";
        string _strYearFormat =  "yyyy";
		string _strWholeDateFormat = "MM/dd/yyyy";
        
        string _PatientAccountCode = "Claim Number";

        bool _IsPrintForm =false;
        bool _IsForPrintData = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _PatientID = 0;
        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        public bool  IsPrintForm
        {

            set { _IsPrintForm = value; }
        }



        #region " Other Details Variables "

        //Referral Provider
        private string _ReferralId = "";
        private string _ReferralFName = "";
        private string _ReferralLName = "";
        private string _ReferralMName = "";
        private string _ReferralAddress = "";
        private string _ReferralCity = "";
        private string _ReferralState = "";
        private string _ReferralZIP = "";
        private string _ReferralNPI = "";
        private string _ReferralSSN = "";
        private string _ReferralEmployerID = "";
        private string _ReferralStateMedicalNo = "";
        private string _ReferralTaxonomy = "";
        private string _ReferralsSuffix = "";
/*
        //Rendering Provider
        private string _RenderingFName = "";
        private string _RenderingLName = "";
        private string _RenderingMName = "";
        private string _RenderingAddress = "";
        private string _RenderingCity = "";
        private string _RenderingState = "";
        private string _RenderingZIP = "";
        private string _RenderingNPI = "";
        private string _RenderingSSN = "";
        private string _RenderingEmployerID = "";
        private string _RenderingStateMedicalNo = "";
        private string _RenderingTaxonomy = "";

        //Billing Provider
        private string _BillingFName = "";
        private string _BillingLName = "";
        private string _BillingMName = "";
        private string _BillingCity = "";
        private string _BillingState = "";
        private string _BillingAddress = "";
        private string _BillingZIP = "";
        private string _BillingNPI = "";
        private string _BillingSSN = "";
        private string _BillingEmployerID = "";
        private string _BillingStateMedicalNo = "";
        private string _BillingTaxonomy = "";
        private string _BillingPhone = "";
*/
        //Billing Provider Address Type
        private AddressType _BillingAddressType = AddressType.ProviderAddress;
        //Billing Provider NPI
        private NPIType _Billing_A_NPI = NPIType.BillingProviderNPI;
  //      private NPIType _Billing_B_NPI = NPIType.BillingProviderNPI;
        //private NPIType _Billing_B_NPI = NPIType.BillingProviderNPI;
        private string _InsuranceTypeCode = String.Empty;

        //Submitter
        private string _SubmitterName = "";
        private string _SubmitterContactPersonName = "";
        private string _SubmitterContactPersonNo = "";
        private string _SubmitterCity = "";
        private string _SubmitterState = "";
        private string _SubmitterZIP = "";
        private string _SubmitterAreaCode = "";
    //    private string _SubmitterETIN = "";
        private string _SubmitterAddress = "";

        //Receiver
   //     private string _ReceiverName = "";
   //     private string _ReceiverETIN = "";

        //Subscriber
        private string _SubscriberLName = "";
  //      private string _SubscriberInsurancePST = "";
        private string _SubscriberRelationshipCode = "";
        private string _SubscriberInsuranceBelongs = "";
        private string _SubscriberFName = "";
        private string _SubscriberMName = "";
        private string _SubscriberInsuranceID = "";
        private string _SubscriberAddress = "";
        private string _SubscriberGroupID = "";
        private string _SubscriberCity = "";
        private string _SubscriberState = "";
        private string _SubscriberZIP = "";
        private string _SubscriberDOB = "";
        private string _SubscriberGender = "";
        private string _SubscriberPhone = "";
        //Payer
        private Int64 _PatientInsuranceID = 0;
        private string _PayerName = "";
        private string _PayerID = "";
        private string _PayerAddress = "";
        private string _PayerAddress2 = "";
        private string _PayerCity = "";
        private string _PayerState = "";
        private string _PayerZip = "";
        private string _CompanyName = "";
        private string _EmployerName = "";  //Added By MaheshB on 20001124 //Box No:-11b
        private string _OtherEmployerName = "";//Box No:- 9c
        private string _ClaimRemittanceRefNo = "";
   //     private string _PatientAccountNo = "";

/*
        //Facility
        private string _FacilityCode = "";
        private string _FacilityName = "";
        private string _FacilityAddress = "";
        private string _FacilityCity = "";
        private string _FacilityState = "";
        private string _FacilityZip = "";
        private string _FacilityNPI = "";
        private string _FacilityID = "";
        private string _FacilityPhone = "";
*/
        //Facility Provider Address Type
        private AddressType _FacilityAddressType = AddressType.FacilityAddress;
        //Billing Provider NPI
        private NPIType _Facility_A_NPI = NPIType.FacilityNPI;
  //      private NPIType _Facility_B_NPI = NPIType.FacilityNPI;

        //Other Insurance
        private Int64 _PatientOtherInsuranceID = 0;
        private string _OtherInsuranceSubscriberLName = "";
     //   private string _OtherInsurancePST = "";
        private string _OtherInsuranceType = "";
        private string _OtherInsuranceRelationshipCode = "";
        private string _OtherInsuranceID = "";
        private string _OtherInsuranceGroupID = "";
        private string _OtherInsuranceAddress = "";
        private string _OtherInsuranceSubscriberFName = "";
        private string _OtherInsuranceSubscriberMName = "";
        private string _OtherInsuranceName = "";
        private string _OtherInsurancePayerID = "";
        private string _OtherInsuranceCity = "";
        private string _OtherInsuranceState = "";
        private string _OtherInsuranceZIP = "";
        private string _OtherInsuranceSubscriberDOB = "";
        private string _OtherInsuranceSubscriberGender = "";
  //      private string _OtherInsuranceSubscriberPhone = "";
        private Int64  _OtherInsuranceContactID=0;
/*
        //ISA and GS Settings
        private string _SenderID = "";
        private string _ReceiverID = "";
        private string _SenderName = "";
        private string _ReceiverCode = "";


        //Patient Information
        private string _PatientLastName = "";
        private string _PatientFirstName = "";
        private string _PatientMiddleName = "";
        private string _PatientSSN = "";
        private string _PatientGender = "";
        private string _PatientDOB = "";
        private string _PatientAddress = "";
        private string _PatientCity = "";
        private string _PatientState = "";
        private string _PatientZip = "";
        private string _PatientCode = "";
        private string _PatientPhone = "";
*/
        //Prior Authorization Number
        private string _PriorAuthorizationNo = "";
        #endregion

      

        private Int64 _ContactID = 0;

        public Int64 ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; }
        }

        //Signature on File
        private bool _IsSignatureOnFile = false;
        private DateTime _dtSignatureOnFileDate = DateTime.Now;

        //BOX13
        private string _InsuredPersonSign = "";

        //MaheshB 20091224
        private bool _IsAcceptAssignment = false; 

        private bool _IsPaperDisplayMailingAddress = false;
        private bool _IsSwap1a9a1aMCare = false;

        string _sInsuredIdNumber = "";
        string _sOtherInsuredPolicyNo = "";



        //Shweta 20100328
        bool _IsEMGAsX = false;
        bool _bShowClaimNo = false;
        int _nBox11bSetting = 0;
        bool _IsCapatalize = false;
        #endregion " Variables And Procedures "

        #region "Printing Variables"
        string _InputFilePath = "";
        string _OutputFilePath = "";
        #endregion

        public void FillReadOnlyPanelForm(int ServiceLinesCount)
        {
          
            ClearForm(pnlMainFormLabel);

            #region "Header Insurance Address"
            lblPayerNameAndAddress.Text = txtPayerNameAndAddress.Text;
            #endregion

            #region "BOX 1"
            // Block 1
            if (chkMedicare.Checked) { lblMedicare.Text = "X"; } else { lblMedicare.Text = string.Empty; }
            if (chkMedicaid.Checked) { lblMedicaid.Text = "X"; } else { lblMedicaid.Text = string.Empty; }
            if (chkTricareChampus.Checked) { lblTricareChampus.Text = "X"; } else { lblTricareChampus.Text = string.Empty; }
            if (chkCHAMPVA.Checked) { lblCHAMPVA.Text = "X"; } else { lblCHAMPVA.Text = string.Empty; }
            if (chkGroupHealthPlan.Checked) { lblGroupHealthPlan.Text = "X"; } else { lblGroupHealthPlan.Text = string.Empty; }
            if (chkFECABlackLung.Checked) { lblFECABlackLung.Text = "X"; } else { lblFECABlackLung.Text = string.Empty; }
            if (chkOtherInsuranceType.Checked) { lblOtherInsuranceType.Text = "X"; } else { lblOtherInsuranceType.Text = string.Empty; }

            lblInsuredIdNumber.Text = txtInsuredIdNumber.Text;
            #endregion

            #region BOX 2
            // Block 2
            lblPatientName.Text = txtPatientName.Text;
            #endregion

            #region BOX 3
            // Block 3

            if ((dtpPatientDOB.Checked) && (dtpPatientDOB.Enabled))
            {
                DateTime dtPatientDOB = (DateTime)Convert.ToDateTime(dtpPatientDOB.Value);
                lblPatientDOB_MM.Text = dtPatientDOB.ToString("MM");
                lblPatientDOB_DD.Text = dtPatientDOB.ToString("dd");
                lblPatientDOB_YY.Text = dtPatientDOB.ToString(_strYearFormat);
            }
            else
            {
                lblPatientDOB_MM.Text = string.Empty ;
                lblPatientDOB_DD.Text = string.Empty;
                lblPatientDOB_YY.Text = string.Empty;
            }
            if (chkPatient_Male.Checked) { lblPatient_Male.Text = "X"; } else {lblPatient_Male.Text = string.Empty;}
            if (chkPatient_Female.Checked) { lblPatient_Female.Text = "X"; } else { lblPatient_Female.Text = string.Empty; }

            #endregion

            #region BOX 4
            // Block 4
            lblInsuredName.Text = txtInsuredName.Text;
            #endregion

            #region BOX 5
            // Block 5
            lblPatientAddress.Text = txtPatientAddress.Text;
            lblPatientCity.Text = txtPatientCity.Text;
            lblPatientState.Text = txtPatientState.Text;
            lblPatientZip.Text = txtPatientZip.Text;
            lblPatientTelephone1.Text = txtPatientTelephone1.Text;
            lblPatientTelephone2.Text = txtPatientTelephone2.Text;

            #endregion

            #region BOX 6
            // Block 6
            if (chkRelationship_Self.Checked) { lblRelationship_Self.Text = "X"; } else { lblRelationship_Self.Text = string.Empty; }
            if (chkRelationship_Spouse.Checked) { lblRelationship_Spouse.Text = "X"; } else { lblRelationship_Spouse.Text = string.Empty; }
            if (chkRelationship_Child.Checked) { lblRelationship_Child.Text = "X"; } else { lblRelationship_Child.Text = string.Empty; }
            if (chkRelationship_Other.Checked) { lblRelationship_Other.Text = "X"; } else { lblRelationship_Other.Text = string.Empty; }

            #endregion

            #region BOX 7
            // Block 7
            lblInsuredsAddress.Text = txtInsuredsAddress.Text;
            lblInsuredsCity.Text = txtInsuredsCity.Text;
            lblInsuredsState.Text = txtInsuredsState.Text;
            lblInsuredsZip.Text = txtInsuredsZip.Text;
            lblInsuredTelephone1.Text = txtInsuredTelephone1.Text;
            lblInsuredTelephone2.Text = txtInsuredTelephone2.Text;
            #endregion

            #region BOX 8
            ///Blank
            #endregion

            #region BOX 9
            // Block 9
            lblOtherInsuredName.Text = txtOtherInsuredName.Text;
            // 9.a
            lblOtherInsuredPolicyNo.Text = txtOtherInsuredPolicyNo.Text;
            // 9.b
           //Blank 
           // lblOtherInsuredEmployerORSchoolName.Text = txtOtherInsuredEmployerORSchoolName.Text;
            // 9.d
            lblOtherInsuredInsuranceName.Text = txtOtherInsuredInsuranceName.Text;
            #endregion

            #region BOX 10
            // Block 10
            // 10.a
            if (chkPatientCoditionRelatedTo_Employment_Yes.Checked) { lblPatientCoditionRelatedTo_Employment_Yes.Text = "X"; } else { lblPatientCoditionRelatedTo_Employment_Yes.Text = string.Empty; }
            if (chkPatientCoditionRelatedTo_Employment_No.Checked) { lblPatientCoditionRelatedTo_Employment_No.Text = "X"; } else { lblPatientCoditionRelatedTo_Employment_No.Text = string.Empty; }
            // 10.b
            if (chkPatientCoditionRelatedTo_AutoAccident_Yes.Checked) { lblPatientCoditionRelatedTo_AutoAccident_Yes.Text = "X"; } else { lblPatientCoditionRelatedTo_AutoAccident_Yes.Text = string.Empty; }
            if (chkPatientCoditionRelatedTo_AutoAccident_No.Checked) { lblPatientCoditionRelatedTo_AutoAccident_No.Text = "X"; } else { lblPatientCoditionRelatedTo_AutoAccident_No.Text = string.Empty; }
            lblPatientCoditionRelatedTo_State.Text = txtPatientCoditionRelatedTo_State.Text;

            // 10.c
            if (chkPatientCoditionRelatedTo_OtherAccident_Yes.Checked) { lblPatientCoditionRelatedTo_OtherAccident_Yes.Text = "X"; } else { lblPatientCoditionRelatedTo_OtherAccident_Yes.Text = string.Empty; }
            if (chkPatientCoditionRelatedTo_OtherAccident_No.Checked) { lblPatientCoditionRelatedTo_OtherAccident_No.Text = "X"; } else { lblPatientCoditionRelatedTo_OtherAccident_No.Text = string.Empty; }
            // 10.d
            lblReserveForLocalUse.Text = txtReserveForLocalUse.Text;

            #endregion

            #region BOX 11
            // Block 11
            lblInsuredPolicyorFECANo.Text = txtInsuredPolicyorFECANo.Text;
            // 11.a
            if ((dtpInsuredsDOB.Checked) && (dtpInsuredsDOB.Enabled))
            {
                DateTime dtInsuredDOB = (DateTime)Convert.ToDateTime(dtpInsuredsDOB.Value);
                lblInsuredsDOB_MM.Text = dtInsuredDOB.ToString("MM");
                lblInsuredsDOB_DD.Text = dtInsuredDOB.ToString("dd");
                lblInsuredsDOB_YY.Text = dtInsuredDOB.ToString(_strYearFormat);
            }
            else
            {
                lblInsuredsDOB_MM.Text = string.Empty;
                lblInsuredsDOB_DD.Text = string.Empty;
                lblInsuredsDOB_YY.Text = string.Empty;
            }
                       
            if (chkInsuredSex_Male.Checked) { lblInsuredSex_Male.Text = "X"; } else { lblInsuredSex_Male.Text = string.Empty; }
            if (chkInsuredSex_Female.Checked) { lblInsuredSex_Female.Text = "X"; } else { lblInsuredSex_Female.Text = string.Empty; }
            // 11.b
            lblInsuredEmployerORSchoolName.Text = txtInsuredEmployerORSchoolName.Text;
            
            switch (_nBox11bSetting)
            {
                case 1:
                    { lblInsuredEmployerORSchoolName.Text = "";
                    txtInsuredEmployerORSchoolName.Clear();
                    }
                    break; 
                case 2: { lblInsuredEmployerORSchoolName.Text = txtInsuredEmployerORSchoolName.Text; }//_ds.Tables["ClaimPatient"].Rows[0]["sEmployerName"]); }

                    break;
                case 3:
                    {
                        txtInsuredEmployerORSchoolName.Text = Convert.ToString(oTransaction.WorkersCompNo); 
                    lblInsuredEmployerORSchoolName.Text = Convert.ToString(oTransaction.WorkersCompNo); } //Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["sWorkersCompNo"]); }
               
                    break;
                case 4: {
                    txtInsuredEmployerORSchoolName.Text = _SubscriberInsuranceID;
                    lblInsuredEmployerORSchoolName.Text = _SubscriberInsuranceID; }//Convert.ToString(_ds.Tables["ClaimInsurance"].Rows[0]["sSubscriberID"]); }

                    break;
                case 5:
                    {
                        txtInsuredEmployerORSchoolName.Text = _SubscriberGroupID; 
                    lblInsuredEmployerORSchoolName.Text = _SubscriberGroupID; 
                    }

                    break;
            }

            // 11.c
            lblInsuredInsurancePlanName.Text = txtInsuredInsurancePlanName.Text;
            // 11.d
            if (chkIsOtherHealthPlan_Yes.Checked) { lblIsOtherHealthPlan_Yes.Text = "X"; } else { lblIsOtherHealthPlan_Yes.Text = string.Empty; }
            if (chkIsOtherHealthPlan_No.Checked) { lblIsOtherHealthPlan_No.Text = "X"; } else { lblIsOtherHealthPlan_No.Text = string.Empty; }
            if (lblInsuredEmployerORSchoolName.Text.Trim() != "")
            {
                lblOtherClaimIDQualifier.Text = txtOtherClaimIDQualifier.Text;
            }
            else
            {
                txtOtherClaimIDQualifier.Clear();
            }
            #endregion

            #region BOX 12
            // Block 12
            lblPatientSignature.Text = txtPatientSignature.Text;
            if ((dtpPatientSignDate.Checked) && (dtpPatientSignDate.Enabled))
            {
                DateTime dtPatientSignDate = (DateTime)Convert.ToDateTime(dtpPatientSignDate.Value);
                lblPatientSignDate_MM.Text = dtPatientSignDate.ToString(_strWholeDateFormat);
                //lblPatientSignDate_DD.Text = dtPatientSignDate.ToString("dd");
                //lblPatientSignDate_YY.Text = dtPatientSignDate.ToString("yy");
            }
            else
            {
                lblPatientSignDate_MM.Text = string.Empty;
            }

            #endregion

            #region BOX 13
            // Block 13
            lblInsuredPersonSign.Text = txtInsuredPersonSign.Text;
            #endregion

            #region BOX 14
            // Block 14
            if ((dtpDateOfCurrentIllness.Checked) && (dtpDateOfCurrentIllness.Enabled))
            {
                DateTime dtDateOfCurrentIllness = (DateTime)Convert.ToDateTime(dtpDateOfCurrentIllness.Value);
                lblDateOfCurrentIllness_MM.Text = dtDateOfCurrentIllness.ToString("MM");
                lblDateOfCurrentIllness_DD.Text = dtDateOfCurrentIllness.ToString("dd");
                lblDateOfCurrentIllness_YY.Text = dtDateOfCurrentIllness.ToString(_strYearFormat);
                lblDateOfCurrentIllnessQualifier.Text = txtDateOfCurrentIllnessQualifier.Text; 
            }
            else
            {
                lblDateOfCurrentIllness_MM.Text = string.Empty;
                lblDateOfCurrentIllness_DD.Text = string.Empty;
                lblDateOfCurrentIllness_YY.Text = string.Empty;
                lblDateOfCurrentIllnessQualifier.Text = string.Empty; 
            }
            #endregion

            #region BOX 15
            // Block 15
            if ((dtpSimilarIllnessFirstDate.Checked) && (dtpSimilarIllnessFirstDate.Enabled))
            {
                DateTime dtSimilarIllnessFirstDate = (DateTime)Convert.ToDateTime(dtpSimilarIllnessFirstDate.Value);
                lblSimilarIllnessFirstDate_MM.Text = dtSimilarIllnessFirstDate.ToString("MM");
                lblSimilarIllnessFirstDate_DD.Text = dtSimilarIllnessFirstDate.ToString("dd");
                lblSimilarIllnessFirstDate_YY.Text = dtSimilarIllnessFirstDate.ToString(_strYearFormat);
                lblSimilarIllnessFirstDateQualifier.Text = txtSimilarIllnessFirstDateQualifier.Text; 
            }
            else
            {
                lblSimilarIllnessFirstDate_MM.Text = string.Empty;
                lblSimilarIllnessFirstDate_DD.Text = string.Empty;
                lblSimilarIllnessFirstDate_YY.Text = string.Empty;
                lblSimilarIllnessFirstDateQualifier.Text = "";
            }
            #endregion

            #region BOX 16
            // Block 16
            if ((dtpUnableToWorkFrom.Checked) && (dtpUnableToWorkFrom.Enabled))
            {
                DateTime dtUnableToWorkFromDate = (DateTime)Convert.ToDateTime(dtpUnableToWorkFrom.Value);
                lblUnableToWorkFrom_MM.Text = dtUnableToWorkFromDate.ToString("MM");
                lblUnableToWorkFrom_DD.Text = dtUnableToWorkFromDate.ToString("dd");
                lblUnableToWorkFrom_YY.Text = dtUnableToWorkFromDate.ToString(_strYearFormat);
            }
            else
            {
                lblUnableToWorkFrom_MM.Text = string.Empty;
                lblUnableToWorkFrom_DD.Text = string.Empty;
                lblUnableToWorkFrom_YY.Text = string.Empty;
            }
            if ((dtpUnableToWorkTill.Checked) && (dtpUnableToWorkTill.Enabled))
            {
                DateTime dtUnableToWorkToDate = (DateTime)Convert.ToDateTime(dtpUnableToWorkTill.Value);
                lblUnableToWorkTill_MM.Text = dtUnableToWorkToDate.ToString("MM");
                lblUnableToWorkTill_DD.Text = dtUnableToWorkToDate.ToString("dd");
                lblUnableToWorkTill_YY.Text = dtUnableToWorkToDate.ToString(_strYearFormat);
            }
            else
            {
                lblUnableToWorkTill_MM.Text = string.Empty;
                lblUnableToWorkTill_DD.Text = string.Empty;
                lblUnableToWorkTill_YY.Text = string.Empty;
            }
            #endregion
        
            #region BOX 17
            // Block 17
            lblReferringProviderName.Text = txtReferringProviderName.Text;
            lblReferringProviderQualifier.Text = txtReferringProviderQualifier.Text; 
            // 17.a
            lblReferringProvider_OtherValue.Text = txtReferringProvider_OtherValue.Text.Trim();
            if (lblReferringProvider_OtherValue.Text.Trim() != string.Empty)
            {
                lblReferringProvider_OtherType.Text = txtReferringProvider_OtherType.Text.Trim();
                AdjustStringFont(lblReferringProvider_OtherType);
            }
            // .......
            // 17.b
            lblReferringProvider_NPI.Text = txtReferringProvider_NPI.Text;
            #endregion

            #region BOX 18
            // Block 18
            if ((dtpHospitalisationFrom.Checked) && (dtpHospitalisationFrom.Enabled))
            {
                DateTime dtHospitalisationFromDate = (DateTime)Convert.ToDateTime(dtpHospitalisationFrom.Value);
                lblHospitalisationFrom_MM.Text = dtHospitalisationFromDate.ToString("MM");
                lblHospitalisationFrom_DD.Text = dtHospitalisationFromDate.ToString("dd");
                lblHospitalisationFrom_YY.Text = dtHospitalisationFromDate.ToString(_strYearFormat);
            }
            else
            {
                lblHospitalisationFrom_MM.Text = string.Empty;
                lblHospitalisationFrom_DD.Text = string.Empty;
                lblHospitalisationFrom_YY.Text = string.Empty;
            }
            if ((dtpHospitalisationTo.Checked) && (dtpHospitalisationTo.Enabled))
            {
                DateTime dtHospitalisationToDate = (DateTime)Convert.ToDateTime(dtpHospitalisationTo.Value);
                lblHospitalisationTo_MM.Text = dtHospitalisationToDate.ToString("MM");
                lblHospitalisationTo_DD.Text = dtHospitalisationToDate.ToString("dd");
                lblHospitalisationTo_YY.Text = dtHospitalisationToDate.ToString(_strYearFormat);
            }
            else
            {
                lblHospitalisationTo_MM.Text = string.Empty;
                lblHospitalisationTo_DD.Text = string.Empty;
                lblHospitalisationTo_YY.Text = string.Empty;
            }
            #endregion

            #region BOX 19
            // Block 19
            lblReservedForLocalUse.Text = txt19ReservedForLocalUse.Text;
            #endregion

            #region BOX 20
            // Block 20
            if (chkOutsideLab_Yes.Checked) { lblOutsideLab_Yes.Text = "X"; } else { lblOutsideLab_Yes.Text = string.Empty; }
            if (chkOutsideLab_No.Checked) { lblOutsideLab_No.Text = "X"; } else { lblOutsideLab_No.Text = string.Empty; }
            lblOutsideLabCharges1.Text = txtOutsideLabCharges1.Text; 
            lblOutsideLabCharges2.Text = txtOutsideLabCharges2.Text;

            #endregion

            #region BOX 21
            // Block 21

            lblIcdInd1.Text = txtIcdInd1.Text;
            lblIcdInd2.Text = txtIcdInd2.Text;
            lblDiagnosisCode11.Text = txtDiagnosisCode11.Text; 
            //lblDiagnosisCode12.Text = txtDiagnosisCode12.Text;
            lblDiagnosisCode21.Text = txtDiagnosisCode21.Text; 
            //lblDiagnosisCode22.Text = txtDiagnosisCode22.Text;
            lblDiagnosisCode31.Text = txtDiagnosisCode31.Text; 
            //lblDiagnosisCode32.Text = txtDiagnosisCode32.Text;
            lblDiagnosisCode41.Text = txtDiagnosisCode41.Text; 
            //lblDiagnosisCode42.Text = txtDiagnosisCode42.Text;

            lblDiagnosisCodeE.Text = txtDiagnosisCodeE.Text;
            lblDiagnosisCodeF.Text = txtDiagnosisCodeF.Text;
            lblDiagnosisCodeG.Text = txtDiagnosisCodeG.Text;
            lblDiagnosisCodeH.Text = txtDiagnosisCodeH.Text;
            lblDiagnosisCodeI.Text = txtDiagnosisCodeI.Text;
            lblDiagnosisCodeJ.Text = txtDiagnosisCodeJ.Text;
            lblDiagnosisCodeK.Text = txtDiagnosisCodeK.Text;
            lblDiagnosisCodeL.Text = txtDiagnosisCodeL.Text;
            #endregion

            #region BOX 22
            // Block 22
            lblMedicaidResubmissionCode.Text = txtMedicaidResubmissionCode.Text;
            lblOriginalRefNumber.Text = txtOriginalRefNumber.Text;
            #endregion

            #region BOX 23
            // Block 23
            lblPriorAuthorizationNumber.Text = txtPriorAuthorizationNumber.Text;
            #endregion

            #region BOX 24
            // Block 24
            // Service Line 1
            if (ServiceLinesCount > 0)
            {
                #region Service Line 1
                lblNotes1.Text = txtNotes1.Text;
                if ((dtpDOS1From.Checked) && (dtpDOS1From.Enabled))
                {
                    DateTime dtDOS1FromDate = (DateTime)Convert.ToDateTime(dtpDOS1From.Value);
                    lblDOS1From_MM.Text = dtDOS1FromDate.ToString("MM");
                    lblDOS1From_DD.Text = dtDOS1FromDate.ToString("dd");
                    lblDOS1From_YY.Text = dtDOS1FromDate.ToString("yy");
                }
                else
                {
                    lblDOS1From_MM.Text = string.Empty;
                    lblDOS1From_DD.Text = string.Empty;
                    lblDOS1From_YY.Text = string.Empty;
                }
                if ((dtpDOS1To.Checked) && (dtpDOS1To.Enabled))
                {
                    DateTime dtDOS1To = (DateTime)Convert.ToDateTime(dtpDOS1To.Value);
                    lblDOS1To_MM.Text = dtDOS1To.ToString("MM");
                    lblDOS1To_DD.Text = dtDOS1To.ToString("dd");
                    lblDOS1To_YY.Text = dtDOS1To.ToString("yy");
                }
                else
                {
                    lblDOS1To_MM.Text = string.Empty;
                    lblDOS1To_DD.Text = string.Empty;
                    lblDOS1To_YY.Text = string.Empty;
                }
                lblPOS1.Text = txtPOS1.Text;
                lblEMG1.Text = txtEMG1.Text;
                lblCPT1.Text = txtCPT1.Text;
                lblMOD11.Text = txtMOD11.Text;
                lblMOD12.Text = txtMOD12.Text;
                lblMOD13.Text = txtMOD13.Text;
                lblMOD14.Text = txtMOD14.Text;
                lblDxPtr1.Text = txtDxPtr1.Text;
                lblCharges1.Text = txtCharges1.Text;
                lblCharges11.Text = txtCharges11.Text;
                lblUnits1.Text = txtUnits1.Text;
                lblEPSDT1.Text = txtEPSDT1.Text;
                lblRenderingProvider1_NPI.Text = txtRenderingProvider1_NPI.Text;

                lblRenderingProvider1_QF.Text = txtRenderingProvider1_Qualifier.Text;
                lblRenderingProvider1_QFValue.Text = txtRenderingProvider1_QualifierValue.Text;

                lblRenderingProvider2_QF.Text = txtRenderingProvider2_Qualifier.Text;
                lblRenderingProvider2_QFValue.Text = txtRenderingProvider2_QualifierValue.Text;

                lblRenderingProvider3_QF.Text = txtRenderingProvider3_Qualifier.Text;
                lblRenderingProvider3_QFValue.Text = txtRenderingProvider3_QualifierValue.Text;

                lblRenderingProvider4_QF.Text = txtRenderingProvider4_Qualifier.Text;
                lblRenderingProvider4_QFValue.Text = txtRenderingProvider4_QualifierValue.Text;

                lblRenderingProvider5_QF.Text = txtRenderingProvider5_Qualifier.Text;
                lblRenderingProvider5_QFValue.Text = txtRenderingProvider5_QualifierValue.Text;

                lblRenderingProvider6_QF.Text = txtRenderingProvider6_Qualifier.Text;
                lblRenderingProvider6_QFValue.Text = txtRenderingProvider6_QualifierValue.Text;
                lblEPSDT1.Text = txtEPSDT1.Text;
                lblShadedEPSDT1.Text = txtEPSDTShaded1.Text;
               

                # endregion
            }
            else
            {
                #region Clear Service Line 1
                lblNotes1.Text = string.Empty;
                lblDOS1From_MM.Text = string.Empty;
                lblDOS1From_DD.Text = string.Empty;
                lblDOS1From_YY.Text = string.Empty;
                lblDOS1To_MM.Text = string.Empty;
                lblDOS1To_DD.Text = string.Empty;
                lblDOS1To_YY.Text = string.Empty;
                lblPOS1.Text = string.Empty;
                lblEMG1.Text = string.Empty;
                lblCPT1.Text = string.Empty;
                lblMOD11.Text = string.Empty;
                lblMOD12.Text = string.Empty;
                lblMOD13.Text = string.Empty;
                lblMOD14.Text = string.Empty;
                lblDxPtr1.Text = string.Empty;
                lblCharges1.Text = string.Empty;
                lblCharges11.Text = string.Empty;
                lblUnits1.Text = string.Empty;
                lblEPSDT1.Text = string.Empty;
                lblRenderingProvider1_NPI.Text = string.Empty;
                lblRenderingProvider1_QF.Text = string.Empty;
                lblRenderingProvider1_QFValue.Text = string.Empty;
                lblRenderingProvider2_QF.Text = string.Empty;
                lblRenderingProvider2_QFValue.Text = string.Empty;
                lblRenderingProvider3_QF.Text = string.Empty;
                lblRenderingProvider3_QFValue.Text = string.Empty;
                lblRenderingProvider4_QF.Text = string.Empty;
                lblRenderingProvider4_QFValue.Text = string.Empty;
                lblRenderingProvider5_QF.Text = string.Empty;
                lblRenderingProvider5_QFValue.Text = string.Empty;
                lblRenderingProvider6_QF.Text = string.Empty;
                lblRenderingProvider6_QFValue.Text = string.Empty;
                lblEPSDT1.Text = string.Empty;
                lblShadedEPSDT1.Text = string.Empty;
                # endregion
            }
            if (ServiceLinesCount > 1)
            {
                // Service Line 2
                #region Service Line 2
                lblNotes2.Text = txtNotes2.Text;
                if ((dtpDOS2From.Checked) && (dtpDOS2From.Enabled))
                {
                    DateTime dtDOS2FromDate = (DateTime)Convert.ToDateTime(dtpDOS2From.Value);
                    lblDOS2From_MM.Text = dtDOS2FromDate.ToString("MM");
                    lblDOS2From_DD.Text = dtDOS2FromDate.ToString("dd");
                    lblDOS2From_YY.Text = dtDOS2FromDate.ToString("yy");
                }
                else
                {
                    lblDOS2From_MM.Text = string.Empty;
                    lblDOS2From_DD.Text = string.Empty;
                    lblDOS2From_YY.Text = string.Empty;
                }
                if ((dtpDOS2To.Checked) && (dtpDOS2To.Enabled))
                {
                    DateTime dtDOS2To = (DateTime)Convert.ToDateTime(dtpDOS2To.Value);
                    lblDOS2To_MM.Text = dtDOS2To.ToString("MM");
                    lblDOS2To_DD.Text = dtDOS2To.ToString("dd");
                    lblDOS2To_YY.Text = dtDOS2To.ToString("yy");
                }
                else
                {
                    lblDOS2To_MM.Text = string.Empty;
                    lblDOS2To_DD.Text = string.Empty;
                    lblDOS2To_YY.Text = string.Empty;
                }
                lblPOS2.Text = txtPOS2.Text;
                lblEMG2.Text = txtEMG2.Text;
                lblCPT2.Text = txtCPT2.Text;
                lblMOD21.Text = txtMOD21.Text;
                lblMOD22.Text = txtMOD22.Text;
                lblMOD23.Text = txtMOD23.Text;
                lblMOD24.Text = txtMOD24.Text;
                lblDxPtr2.Text = txtDxPtr2.Text;
                lblCharges2.Text = txtCharges2.Text;
                lblCharges21.Text = txtCharges21.Text;
                lblUnits2.Text = txtUnits2.Text;
                lblEPSDT2.Text = txtEPSDT2.Text;
                lblRenderingProvider2_NPI.Text = txtRenderingProvider2_NPI.Text;
                lblEPSDT2.Text = txtEPSDT2.Text;
                lblShadedEPSDT2.Text = txtEPSDTShaded2.Text;
                # endregion
            }
            else
            {
                #region Clear Service Line 2
                lblNotes2.Text = string.Empty;
                lblDOS2From_MM.Text = string.Empty;
                lblDOS2From_DD.Text = string.Empty;
                lblDOS2From_YY.Text = string.Empty;
                lblDOS2To_MM.Text = string.Empty;
                lblDOS2To_DD.Text = string.Empty;
                lblDOS2To_YY.Text = string.Empty;
                lblPOS2.Text = string.Empty;
                lblEMG2.Text = string.Empty;
                lblCPT2.Text = string.Empty;
                lblMOD21.Text = string.Empty;
                lblMOD22.Text = string.Empty;
                lblMOD23.Text = string.Empty;
                lblMOD24.Text = string.Empty;
                lblDxPtr2.Text = string.Empty;
                lblCharges2.Text = string.Empty;
                lblCharges21.Text = string.Empty;
                lblUnits2.Text = string.Empty;
                lblEPSDT2.Text = string.Empty;
                lblRenderingProvider2_NPI.Text = string.Empty;
                lblEPSDT2.Text = string.Empty;
                lblShadedEPSDT2.Text = string.Empty;
                # endregion
            }
            if (ServiceLinesCount > 2)
            {
                // Service Line 3
                #region Service Line 3
                lblNotes3.Text = txtNotes3.Text;
                if ((dtpDOS3From.Checked) && (dtpDOS3From.Enabled))
                {
                    DateTime dtDOS3FromDate = (DateTime)Convert.ToDateTime(dtpDOS3From.Value);
                    lblDOS3From_MM.Text = dtDOS3FromDate.ToString("MM");
                    lblDOS3From_DD.Text = dtDOS3FromDate.ToString("dd");
                    lblDOS3From_YY.Text = dtDOS3FromDate.ToString("yy");
                }
                else
                {
                    lblDOS3From_MM.Text = string.Empty;
                    lblDOS3From_DD.Text = string.Empty;
                    lblDOS3From_YY.Text = string.Empty;
                }
                if ((dtpDOS3To.Checked) && (dtpDOS3To.Enabled))
                {
                    DateTime dtDOS3To = (DateTime)Convert.ToDateTime(dtpDOS3To.Value);
                    lblDOS3To_MM.Text = dtDOS3To.ToString("MM");
                    lblDOS3To_DD.Text = dtDOS3To.ToString("dd");
                    lblDOS3To_YY.Text = dtDOS3To.ToString("yy");
                }
                else
                {
                    lblDOS3To_MM.Text = string.Empty;
                    lblDOS3To_DD.Text = string.Empty;
                    lblDOS3To_YY.Text = string.Empty;
                }
                lblPOS3.Text = txtPOS3.Text;
                lblEMG3.Text = txtEMG3.Text;
                lblCPT3.Text = txtCPT3.Text;
                lblMOD31.Text = txtMOD31.Text;
                lblMOD32.Text = txtMOD32.Text;
                lblMOD33.Text = txtMOD33.Text;
                lblMOD34.Text = txtMOD34.Text;
                lblDxPtr3.Text = txtDxPtr3.Text;
                lblCharges3.Text = txtCharges3.Text;
                lblCharges31.Text = txtCharges31.Text;
                lblUnits3.Text = txtUnits3.Text;
                lblEPSDT3.Text = txtEPSDT3.Text;
                lblRenderingProvider3_NPI.Text = txtRenderingProvider3_NPI.Text;
                lblEPSDT3.Text = txtEPSDT3.Text;
                lblEPSDTShaded3.Text = txtEPSDTShaded3.Text;
                # endregion
            }
            else
            {
                #region Clear Service Line 3
                lblNotes3.Text = string.Empty;
                lblDOS3From_MM.Text = string.Empty;
                lblDOS3From_DD.Text = string.Empty;
                lblDOS3From_YY.Text = string.Empty;
                lblDOS3To_MM.Text = string.Empty;
                lblDOS3To_DD.Text = string.Empty;
                lblDOS3To_YY.Text = string.Empty;
                lblPOS3.Text = string.Empty;
                lblEMG3.Text = string.Empty;
                lblCPT3.Text = string.Empty;
                lblMOD31.Text = string.Empty;
                lblMOD32.Text = string.Empty;
                lblMOD33.Text = string.Empty;
                lblMOD34.Text = string.Empty;
                lblDxPtr3.Text = string.Empty;
                lblCharges3.Text = string.Empty;
                lblCharges31.Text = string.Empty;
                lblUnits3.Text = string.Empty;
                lblEPSDT3.Text = string.Empty;
                lblRenderingProvider3_NPI.Text = string.Empty;
                lblEPSDT3.Text = string.Empty;
                lblEPSDTShaded3.Text = string.Empty;
                # endregion
            }
            if (ServiceLinesCount > 3)
            {
                // Service Line 4
                #region Service Line 4
                lblNotes4.Text = txtNotes4.Text;
                if ((dtpDOS4From.Checked) && (dtpDOS4From.Enabled))
                {
                    DateTime dtDOS4FromDate = (DateTime)Convert.ToDateTime(dtpDOS4From.Value);
                    lblDOS4From_MM.Text = dtDOS4FromDate.ToString("MM");
                    lblDOS4From_DD.Text = dtDOS4FromDate.ToString("dd");
                    lblDOS4From_YY.Text = dtDOS4FromDate.ToString("yy");
                }
                else
                {
                    lblDOS4From_MM.Text = string.Empty;
                    lblDOS4From_DD.Text = string.Empty;
                    lblDOS4From_YY.Text = string.Empty;
                }
                if ((dtpDOS4To.Checked) && (dtpDOS4To.Enabled))
                {
                    DateTime dtDOS4To = (DateTime)Convert.ToDateTime(dtpDOS4To.Value);
                    lblDOS4To_MM.Text = dtDOS4To.ToString("MM");
                    lblDOS4To_DD.Text = dtDOS4To.ToString("dd");
                    lblDOS4To_YY.Text = dtDOS4To.ToString("yy");
                }
                else
                {
                    lblDOS4To_MM.Text = string.Empty;
                    lblDOS4To_DD.Text = string.Empty;
                    lblDOS4To_YY.Text = string.Empty;
                }
                lblPOS4.Text = txtPOS4.Text;
                lblEMG4.Text = txtEMG4.Text;
                lblCPT4.Text = txtCPT4.Text;
                lblMOD41.Text = txtMOD41.Text;
                lblMOD42.Text = txtMOD42.Text;
                lblMOD43.Text = txtMOD43.Text;
                lblMOD44.Text = txtMOD44.Text;
                lblDxPtr4.Text = txtDxPtr4.Text;
                lblCharges4.Text = txtCharges4.Text;
                lblCharges41.Text = txtCharges41.Text;
                lblUnits4.Text = txtUnits4.Text;
                lblEPSDT4.Text = txtEPSDT4.Text;
                lblRenderingProvider4_NPI.Text = txtRenderingProvider4_NPI.Text;
                lblEPSDT4.Text = txtEPSDT4.Text;
                lblShadedEPSDT4.Text = txtEPSDTShaded4.Text;
                 
                # endregion
            }
            else
            {
                #region Clear Service Line 4
                lblNotes4.Text = string.Empty;
                lblDOS4From_MM.Text = string.Empty;
                lblDOS4From_DD.Text = string.Empty;
                lblDOS4From_YY.Text = string.Empty;
                lblDOS4To_MM.Text = string.Empty;
                lblDOS4To_DD.Text = string.Empty;
                lblDOS4To_YY.Text = string.Empty;
                lblPOS4.Text = string.Empty;
                lblEMG4.Text = string.Empty;
                lblCPT4.Text = string.Empty;
                lblMOD41.Text = string.Empty;
                lblMOD42.Text = string.Empty;
                lblMOD43.Text = string.Empty;
                lblMOD44.Text = string.Empty;
                lblDxPtr4.Text = string.Empty;
                lblCharges4.Text = string.Empty;
                lblCharges41.Text = string.Empty;
                lblUnits4.Text = string.Empty;
                lblEPSDT4.Text = string.Empty;
                lblRenderingProvider4_NPI.Text = string.Empty;
                lblEPSDT4.Text = string.Empty;
                lblShadedEPSDT4.Text = string.Empty;
                # endregion
            }
            if (ServiceLinesCount > 4)
            {
                // Service Line 5
                #region Service Line 5
                lblNotes5.Text = txtNotes5.Text;
                if ((dtpDOS5From.Checked) && (dtpDOS5From.Enabled))
                {
                    DateTime dtDOS5FromDate = (DateTime)Convert.ToDateTime(dtpDOS5From.Value);
                    lblDOS5From_MM.Text = dtDOS5FromDate.ToString("MM");
                    lblDOS5From_DD.Text = dtDOS5FromDate.ToString("dd");
                    lblDOS5From_YY.Text = dtDOS5FromDate.ToString("yy");
                }
                else
                {
                    lblDOS5From_MM.Text = string.Empty;
                    lblDOS5From_DD.Text = string.Empty;
                    lblDOS5From_YY.Text = string.Empty;
                }
                if ((dtpDOS5To.Checked) && (dtpDOS5To.Enabled))
                {
                    DateTime dtDOS5To = (DateTime)Convert.ToDateTime(dtpDOS5To.Value);
                    lblDOS5To_MM.Text = dtDOS5To.ToString("MM");
                    lblDOS5To_DD.Text = dtDOS5To.ToString("dd");
                    lblDOS5To_YY.Text = dtDOS5To.ToString("yy");
                }
                else
                {
                    lblDOS5To_MM.Text = string.Empty;
                    lblDOS5To_DD.Text = string.Empty;
                    lblDOS5To_YY.Text = string.Empty;
                }
                lblPOS5.Text = txtPOS5.Text;
                lblEMG5.Text = txtEMG5.Text;
                lblCPT5.Text = txtCPT5.Text;
                lblMOD51.Text = txtMOD51.Text;
                lblMOD52.Text = txtMOD52.Text;
                lblMOD53.Text = txtMOD53.Text;
                lblMOD54.Text = txtMOD54.Text;
                lblDxPtr5.Text = txtDxPtr5.Text;
                lblCharges5.Text = txtCharges5.Text;
                lblCharges51.Text = txtCharges51.Text;
                lblUnits5.Text = txtUnits5.Text;
                lblEPSDT5.Text = txtEPSDT5.Text;
                lblRenderingProvider5_NPI.Text = txtRenderingProvider5_NPI.Text;
                lblEPSDT5.Text = txtEPSDT5.Text;
                lblShadedEPSDT5.Text = txtEPSDTShaded5.Text;
                # endregion
            }
            else
            {
                #region Clear Service Line 5
                lblNotes5.Text = string.Empty;
                lblDOS5From_MM.Text = string.Empty;
                lblDOS5From_DD.Text = string.Empty;
                lblDOS5From_YY.Text = string.Empty;
                lblDOS5To_MM.Text = string.Empty;
                lblDOS5To_DD.Text = string.Empty;
                lblDOS5To_YY.Text = string.Empty;
                lblPOS5.Text = string.Empty;
                lblEMG5.Text = string.Empty;
                lblCPT5.Text = string.Empty;
                lblMOD51.Text = string.Empty;
                lblMOD52.Text = string.Empty;
                lblMOD53.Text = string.Empty;
                lblMOD54.Text = string.Empty;
                lblDxPtr5.Text = string.Empty;
                lblCharges5.Text = string.Empty;
                lblCharges51.Text = string.Empty;
                lblUnits5.Text = string.Empty;
                lblEPSDT5.Text = string.Empty;
                lblRenderingProvider5_NPI.Text = string.Empty;
                lblEPSDT5.Text = string.Empty;
                lblShadedEPSDT5.Text = string.Empty;
                # endregion
            }
            if (ServiceLinesCount > 5)
            {
                // Service Line 6
                #region Service Line 6
                lblNotes6.Text = txtNotes6.Text;
                if ((dtpDOS6From.Checked) && (dtpDOS6From.Enabled))
                {
                    DateTime dtDOS6FromDate = (DateTime)Convert.ToDateTime(dtpDOS6From.Value);
                    lblDOS6From_MM.Text = dtDOS6FromDate.ToString("MM");
                    lblDOS6From_DD.Text = dtDOS6FromDate.ToString("dd");
                    lblDOS6From_YY.Text = dtDOS6FromDate.ToString("yy");
                }
                else
                {
                    lblDOS6From_MM.Text = string.Empty;
                    lblDOS6From_DD.Text = string.Empty;
                    lblDOS6From_YY.Text = string.Empty;
                }
                if ((dtpDOS6To.Checked) && (dtpDOS6To.Enabled))
                {
                    DateTime dtDOS6To = (DateTime)Convert.ToDateTime(dtpDOS6To.Value);
                    lblDOS6To_MM.Text = dtDOS6To.ToString("MM");
                    lblDOS6To_DD.Text = dtDOS6To.ToString("dd");
                    lblDOS6To_YY.Text = dtDOS6To.ToString("yy");
                }
                else
                {
                    lblDOS6To_MM.Text = string.Empty;
                    lblDOS6To_DD.Text = string.Empty;
                    lblDOS6To_YY.Text = string.Empty;
                }
                lblPOS6.Text = txtPOS6.Text;
                lblEMG6.Text = txtEMG6.Text;
                lblCPT6.Text = txtCPT6.Text;
                lblMOD61.Text = txtMOD61.Text;
                lblMOD62.Text = txtMOD62.Text;
                lblMOD63.Text = txtMOD63.Text;
                lblMOD64.Text = txtMOD64.Text;
                lblDxPtr6.Text = txtDxPtr6.Text;
                lblCharges6.Text = txtCharges6.Text;
                lblCharges61.Text = txtCharges61.Text;
                lblUnits6.Text = txtUnits6.Text;
                lblEPSDT6.Text = txtEPSDT6.Text;
                lblRenderingProvider6_NPI.Text = txtRenderingProvider6_NPI.Text;
                lblEPSDT6.Text = txtEPSDT6.Text;
                lblShadedEPSDT6.Text = txtEPSDTShaded6.Text;
                # endregion
            }
            else
            {
                #region Clear Service Line 6
                lblNotes6.Text = string.Empty;
                lblDOS6From_MM.Text = string.Empty;
                lblDOS6From_DD.Text = string.Empty;
                lblDOS6From_YY.Text = string.Empty;
                lblDOS6To_MM.Text = string.Empty;
                lblDOS6To_DD.Text = string.Empty;
                lblDOS6To_YY.Text = string.Empty;
                lblPOS6.Text = string.Empty;
                lblEMG6.Text = string.Empty;
                lblCPT6.Text = string.Empty;
                lblMOD61.Text = string.Empty;
                lblMOD62.Text = string.Empty;
                lblMOD63.Text = string.Empty;
                lblMOD64.Text = string.Empty;
                lblDxPtr6.Text = string.Empty;
                lblCharges6.Text = string.Empty;
                lblCharges61.Text = string.Empty;
                lblUnits6.Text = string.Empty;
                lblEPSDT6.Text = string.Empty;
                lblRenderingProvider6_NPI.Text = string.Empty;
                lblEPSDT6.Text = string.Empty;
                lblShadedEPSDT6.Text = string.Empty;
                # endregion
            }

            #endregion

            #region BOX 25
            // Block 25
            lblFederalTaxID.Text = txtFederalTaxID.Text;
            if (chkFederalTaxID_SSN.Checked) { lblFederalTaxID_SSN.Text = "X"; } else { lblFederalTaxID_SSN.Text = string.Empty; }
            if (chkFederalTaxID_EIN.Checked) { lblFederalTaxID_EIN.Text = "X"; } else { lblFederalTaxID_EIN.Text = string.Empty; }
            #endregion
            
            #region BOX 26
            // Block 26
            lblPatientAccountNo.Text = txtPatientAccountNo.Text;
            #endregion

            #region BOX 27

            // Block 27
            if (chkAcceptAssignment_Yes.Checked) { lblAcceptAssignment_Yes.Text = "X"; } else { lblAcceptAssignment_Yes.Text = string.Empty; }         
            if (chkAcceptAssignment_Yes.Checked) { lblAcceptAssignment_Yes.Text = "X"; } else { lblAcceptAssignment_Yes.Text = string.Empty; }
            if (chkAcceptAssignment_No.Checked) { lblAcceptAssignment_No.Text = "X"; } else { lblAcceptAssignment_No.Text = string.Empty; }

            #endregion

            #region BOX 28
            // Block 28
            lblTotalCharges.Text = txtTotalCharges.Text;
            lblTotalCharges2.Text = txtTotalCharges2.Text;
            #endregion

            #region BOX 29
            // Block 29
            lblAmountPaid.Text = txtAmountPaid.Text;
            lblAmountPaid1.Text = txtAmountPaid2.Text;
            #endregion

            #region BOX 30
            // Block 30
            //reserved for NUCC USE
            #endregion

            #region BOX 31
            // Block 31
            if (ReportClinicName(_ContactID) == true)
            {
                txtPhyscianSignature.Text = _ClinicName;
            }
            lblPhyscianSignature.Text = txtPhyscianSignature.Text;
            if ((dtpPhysicianSignDate.Checked) && (dtpPhysicianSignDate.Enabled))
            {
                DateTime dtPhysicianSignDate = (DateTime)Convert.ToDateTime(dtpPhysicianSignDate.Value);
                lblPhysicianSignDate_MM.Text = dtPhysicianSignDate.ToString(_strWholeDateFormat);
                
            }
            else
            {
                lblPhysicianSignDate_MM.Text = string.Empty;
            }
            
            lblPhyscianQualifierValue.Text = txtPhyscianQualifierValue.Text;

            #endregion

            #region BOX 32
            // Block 32
            //lblFacilityInfo.Text = txtFacilityInfo.Text;
            lblFacilityInfo.Text = getStringFitinTextBox(txtFacilityInfo, txtFacilityInfo.Text.Trim());
             txtFacilityInfo.Text = lblFacilityInfo.Text.Trim();
            ///lblFacilityInfo.AutoEllipsis = true;
            lblFacility_a_NPI.Text = txtFacility_a_NPI.Text;
            lblFacility_b.Text = txtFacility_b.Text;

            #endregion

            #region BOX 33
            // Block 33
            lblBillingProviderPhone1.Text = txtBillingProviderPhone1.Text;
            lblBillingProviderPhone2.Text = txtBillingProviderPhone2.Text;

            //lblBillingProviderInfo.Text = txtBillingProviderInfo.Text;
            lblBillingProviderInfo.Text = getStringFitinTextBox(txtBillingProviderInfo, txtBillingProviderInfo.Text.Trim());
            txtBillingProviderInfo.Text = lblBillingProviderInfo.Text;

            lblBillingProv_a_NPI.Text = txtBillingProv_a_NPI.Text;
            lblBillingProv_b_UPIN.Text = txtBillingProv_b_UPIN.Text;
            #endregion

        }

        /// <summary>
        /// 
        /// To clear all the content of the previously loaded Transaction 
        /// </summary>
        /// <param name="oPanel">
        /// pnlMainForm is to be passed in case of TextBoxPanel to be clear
        /// pnlMainFormLabel is to be passed in case of LabelsPanel to be clear
        /// </param>
        public void ClearForm(Panel oPanel)
        {
            if (oPanel == null)
            {
                oPanel = pnlMainFormLabel;
            }
            Control.ControlCollection controlsCollections = oPanel.Controls;
            foreach (Control oControl in controlsCollections)
            {
                if (oControl != null)
                {
                    if (oControl is Label)
                    {
                        Label oLabel = (Label)oControl;
                        oLabel.Text = string.Empty;
                    }
                    if (oControl is TextBox)
                    {
                        TextBox oTextBox = (TextBox)oControl;
                        oTextBox.Text = string.Empty;
                    }
                    if (oControl is CheckBox)
                    {
                        CheckBox oCheckBox = (CheckBox)oControl;
                        oCheckBox.Checked = false;
                    }
                    if (oControl is ComboBox)
                    {
                        ComboBox oComboBox = (ComboBox)oControl;
                        oComboBox.Items.Clear();
                    }
                }
            }
        }
      
        public void FillTransactionOnForm(Int64 TransactionId, Int64 MstTransactionId)
        {
            
            ClearForm(pnlMainForm);
        
            clsgloBilling ogloBilling = new clsgloBilling(_databaseconnectionstring);
            TransactionDetails oTranDetails = null;
           
            bool IsNotesInLines = true;
            string _sNotes = "";
            string _billingProviderAddress = String.Empty;

            DataTable dtProviders = null;
            Int64 MidLevelRenderingProviderID = 0;
            Int64 MidLevelBillingProviderID = 0;
            Int64 _MidLevelRenderingProviderIDLineWise = 0; 
            try
            {
                if (TransactionId > 0)
                {
                    oTransaction = ogloBilling.GetHCFATransactionDetailsNew(TransactionId, MstTransactionId, _PatientID);
                    if (oTransaction != null)
                    {
                        decimal TotCharges = 0;
                      _strYearFormat= GetYearFormat(oTransaction.ContactID);
                      if (_strYearFormat == "yy")
                      {
                          _strWholeDateFormat = "MM/dd/yy";
                      }
                        FillAllDetails(oTransaction);

                        oTranDetails = oTransaction.Transaction_Details;

                        if (oTransaction.Box19AndIncludeReferal != null && oTransaction.Box19AndIncludeReferal.Rows.Count > 0)
                        {
                            if (oTransaction.Box19AndIncludeReferal.Rows[0]["bNotesInBox19"] != null)
                            {
                                if (Convert.ToBoolean( oTransaction.Box19AndIncludeReferal.Rows[0]["bNotesInBox19"])==false)
                                {
                                    IsNotesInLines = true;
                                }
                                else
                                {
                                    IsNotesInLines = false;
                                }
                            }
                        }
                    

                        #region Billing Provider Info
                        
                        dtProviders = ogloBilling.GetMidLevelProviders(oTransaction.Lines[0].RefferingProviderId, oTransaction.ProviderID, oTransaction.ContactID, oTransaction.ClinicID);
                        if (dtProviders != null && dtProviders.Rows.Count > 0)
                        {
                            MidLevelRenderingProviderID = Convert.ToInt64(dtProviders.Rows[0]["nORenderingProviderID"]);
                            MidLevelBillingProviderID = Convert.ToInt64(dtProviders.Rows[0]["nOBillingProviderID"]);
                        }

                        FillPhysician(MidLevelBillingProviderID);

                        if (oTranDetails.HCFA_FacilityID == "")
                        {
                            FillBillingProviderFacility(MidLevelBillingProviderID, 0, Convert.ToInt64(oTransaction.ContactID), "Billing Provider Source", "Provider");
                        }
                        else
                        {
                            FillBillingProviderFacility(MidLevelBillingProviderID, Convert.ToInt64(oTranDetails.HCFA_FacilityID), Convert.ToInt64(oTransaction.ContactID), "Billing Provider Source", "Provider");
                        }
                        

                        if (oTranDetails.HCFA_ProviderEIN.Trim() != "")
                        {
                            txtFederalTaxID.Text = oTranDetails.HCFA_ProviderEIN.Trim();
                            chkFederalTaxID_EIN.Checked = true;
                        }
                        else if (oTranDetails.HCFA_ProviderSSN.Trim() != "")
                        {
                            txtFederalTaxID.Text = oTranDetails.HCFA_ProviderSSN.Trim();
                            chkFederalTaxID_SSN.Checked = true;
                        }

                        #endregion Billing Provider Info

                        #region Outside Lab & Insurance Details

                        chkAcceptAssignment_Yes.Checked = _IsAcceptAssignment;
                        if (_IsAcceptAssignment == true)
                        {
                            chkAcceptAssignment_No.Checked = false;
                        }
                        else if (_IsAcceptAssignment == false)
                        {
                            chkAcceptAssignment_No.Checked = true;
                        }
                       txtPayerNameAndAddress.Text = _PayerName + Environment.NewLine;
                       txtPayerNameAndAddress.Text += _PayerAddress + Environment.NewLine;
                       txtPayerNameAndAddress.Text += _PayerAddress2 + Environment.NewLine;
                       txtPayerNameAndAddress.Text += _PayerCity.Trim() + " " + _PayerState.Trim() + " " + _PayerZip.Trim();
              
                        if (oTransaction.OutSideLab == true)
                        {
                            chkOutsideLab_Yes.Checked = true;
                        }
                        else if (oTransaction.OutSideLab == false)
                        {
                            chkOutsideLab_No.Checked = true;
                        }
                        if (oTransaction.OutSideLabCharges.ToString().Trim() != "" && oTransaction.OutSideLab == true)
                        {
                            string[] OutsideLabCharges = Convert.ToString(oTransaction.OutSideLabCharges).Split('.');
                            if (OutsideLabCharges.Length > 1)
                            {
                                txtOutsideLabCharges1.Text = OutsideLabCharges[0].Trim();
                                txtOutsideLabCharges2.Text = OutsideLabCharges[1].Trim();
                            }
                            else if (OutsideLabCharges.Length == 1)
                            {
                                txtOutsideLabCharges1.Text = OutsideLabCharges[0].Trim();
                                txtOutsideLabCharges2.Text = "00";
                            }

                        }
                        #endregion Other Details

                        #region Facility Information

                        Int64 temp = _PatientInsuranceID;
                        Int64 tem1p = Convert.ToInt64(txtInsuredInsurancePlanName.Tag);
                        string _POS = String.Empty;
                        if (oTransaction.Lines != null && oTransaction.Lines.Count > 0)
                        {
                            
                                    _POS = Convert.ToString(oTransaction.Lines[0].POSCode);
                            
                        }

                        gloDatabaseLayer.DBLayer oDB = null;
                       
                        if (oTranDetails.HCFA_FacilityID == "")
                        {
                            FillBillingProviderFacility(MidLevelBillingProviderID, 0, Convert.ToInt64(oTransaction.ContactID), "Service Facility Source", "Facility");
                        }
                        else
                        {
                            FillBillingProviderFacility(MidLevelBillingProviderID, Convert.ToInt64(oTranDetails.HCFA_FacilityID), Convert.ToInt64(oTransaction.ContactID), "Service Facility Source", "Facility");
                        }
                       
                        if (_POS.Trim() == "11" ||_POS.Trim() == "12")
                        { //Check Plan Settings.                
                            string _sqlquery = "Select ISNULL(sIncludeFacilitieswithPOS11onClaim,'') as sIncludeFacilitieswithPOS11onClaim from Contacts_Insurance_DTL WITH(NOLOCK)  where nContactID='" + _ContactID + "'";
                            
                            try
                            {
                                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                oDB.Connect(false);
                                DataTable dt = new DataTable();
                                oDB.Retrive_Query(_sqlquery, out dt);

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    if (Convert.ToString(dt.Rows[0]["sIncludeFacilitieswithPOS11onClaim"]) != "")
                                    {
                                        if (Convert.ToString(dt.Rows[0]["sIncludeFacilitieswithPOS11onClaim"]).ToUpper().Trim() == "NO")
                                        {
                                            txtFacilityInfo.Text = "";
                                            txtFacilityCode.Text = "";
                                            txtFacilityDescription.Text = "";
                                            txtFacilityCode.Tag = null;
                                            txtFacility_a_NPI.Text = "";
                                            txtFacility_b.Text = "";
                                        }
                                    }
                                    else
                                    { //If No settings for Plan check Admin Settings.
                                        _sqlquery = "";
                                        _sqlquery = "Select sSettingsValue from settings WITH(NOLOCK) where sSettingsName='IncludeFacilitieswithPOS11onClaim' and nClinicID='" + _ClinicID + "'";
                                        DataTable dtAdmin = new DataTable();
                                        oDB.Retrive_Query(_sqlquery, out dtAdmin);
                                        if (dtAdmin != null && dtAdmin.Rows.Count > 0)
                                        {
                                            if (Convert.ToString(dtAdmin.Rows[0]["sSettingsValue"]) == "False")
                                            {
                                                txtFacilityInfo.Text = "";
                                                txtFacilityCode.Text = "";
                                                txtFacilityDescription.Text = "";
                                                txtFacilityCode.Tag = null;
                                                txtFacility_a_NPI.Text = "";
                                                txtFacility_b.Text = "";
                                            }
                                        }
                                    }


                                }




                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            }
                            finally
                            {
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                    oDB.Dispose();
                                }
                            }
                        }
                        #endregion Facility Information

                        #region Patient Information

                        txtPatientName.Text = oTranDetails.HCFA_PatientLName.Trim() + " " + oTranDetails.HCFA_PatientFName.Trim()
                            + " " + oTranDetails.HCFA_PatientMName.Trim();  //= NameFormation(oTranDetails.HCFA_PatientLName.Trim(), oTranDetails.HCFA_PatientSuffix.Trim(), oTranDetails.HCFA_PatientFName.Trim(), oTranDetails.HCFA_PatientMName.Trim());
                        txtPatientCity.Text = oTranDetails.HCFA_PatientCity.Trim();
                        txtPatientAddress.Text = oTranDetails.HCFA_PatientAddress1.Trim() + " " + oTranDetails.HCFA_PatientAddress2.Trim().Trim();
                        txtPatientState.Text = oTranDetails.HCFA_PatientState.Trim();
                        if (oTranDetails.HCFA_PatientAreaCode.Trim() != "")
                        {
                            txtPatientZip.Text = oTranDetails.HCFA_PatientZip.Trim() + "-" + oTranDetails.HCFA_PatientAreaCode.Trim();
                        }
                        else
                        {
                            txtPatientZip.Text = oTranDetails.HCFA_PatientZip.Trim();
                        }
                        //txtPatientName.Text = "";
                        txtPatientName.Tag = oTransaction.PatientID;

                        if (oTranDetails.HCFA_PatientPhone.Trim() != "")
                        {
                            txtPatientTelephone1.Text = "";//oTranDetails.HCFA_PatientPhone.Trim().Substring(0, 3);
                            txtPatientTelephone2.Text = "";// oTranDetails.HCFA_PatientPhone.Trim().Substring(3, oTranDetails.HCFA_PatientPhone.Trim().Length - 3);
                        }

                        if (oTranDetails.HCFA_PatientDOB > 0)
                        {
                            dtpPatientDOB.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oTranDetails.HCFA_PatientDOB));
                        }
                        if (oTranDetails.HCFA_PatientGender.Trim() != "")
                        {
                            if (oTranDetails.HCFA_PatientGender.ToUpper().Trim().Substring(0, 1) == "M")
                            {
                                chkPatient_Male.Checked = true;
                            }
                            else if (oTranDetails.HCFA_PatientGender.ToUpper().Trim().Substring(0, 1) == "F")
                            {
                                chkPatient_Female.Checked = true;
                            }
                        }
                        else
                        {
                            chkPatient_Male.Checked = false;
                            chkPatient_Female.Checked = false;
                        }

                       
                        if (_IsSignatureOnFile == true)
                        {
                            
                            dtpPatientSignDate.Checked = true;
                            txtPatientSignature.Text = "SIGNATURE ON FILE";
                            dtpPatientSignDate.Checked = true;
                            dtpPatientSignDate.Enabled = true;
                        }
                        else
                        {
                          
                            dtpPatientSignDate.Checked = false;
                            txtPatientSignature.Text = "SIGNATURE NOT ON  FILE";
                            dtpPatientSignDate.Checked = false;
                            dtpPatientSignDate.Enabled = false;
                        }

                        #endregion Patient Information

                        #region Insurance And Insured Person Details

                        txtInsuredIdNumber.Text = _SubscriberInsuranceID.Trim();
                        _sInsuredIdNumber = _SubscriberInsuranceID.Trim();

                        txtInsuredInsurancePlanName.Text = _PayerName.Trim();
                        txtInsuredInsurancePlanName.Tag = _PatientInsuranceID;
                        txtInsuredName.Text = _SubscriberLName.Trim() + " " + _SubscriberFName.Trim() + " " + _SubscriberMName.Trim(); //NameFormation(_SubscriberLName.Trim(),"",_SubscriberFName.Trim(), _SubscriberMName.Trim());
                        txtInsuredsAddress.Text = _SubscriberAddress.Trim();
                        txtInsuredsCity.Text = _SubscriberCity.Trim();
                        txtInsuredsState.Text = _SubscriberState.Trim();
                        txtInsuredsZip.Text = _SubscriberZIP.Trim();
                        txtInsuredPolicyorFECANo.Text = _SubscriberGroupID.Trim();
                        if (_SubscriberPhone.Trim() != "")
                        {
                            txtInsuredTelephone1.Text = _SubscriberPhone.Trim().Substring(0, 3);
                            txtInsuredTelephone2.Text = _SubscriberPhone.Trim().Substring(3, _SubscriberPhone.Trim().Length - 3);
                        }

                        //Gender for Insurance Subscriber
                        if (_SubscriberGender.Trim() != "" && _SubscriberGender.Trim().Length > 0)
                        {
                            if (_SubscriberGender.ToUpper().Trim().Substring(0, 1) == "M")
                            {
                                chkInsuredSex_Male.Checked = true;
                            }
                            else if (_SubscriberGender.ToUpper().Trim().Substring(0, 1) == "F")
                            {
                                chkInsuredSex_Female.Checked = true;
                            }
                        }
                        else
                        {
                            chkInsuredSex_Male.Checked = false;
                            chkInsuredSex_Female.Checked = false;
                        }
                      

                        if (_SubscriberDOB.Trim() != "")
                        {
                            dtpInsuredsDOB.Checked = true;
                            dtpInsuredsDOB.Value = Convert.ToDateTime(_SubscriberDOB.Trim());
                            dtpInsuredsDOB.Enabled = true;
                        }
                        else
                        {
                            dtpInsuredsDOB.Checked = false;
                        }

                        #endregion Insurance And Insured Person Details

                        #region Referral/Physicain Info
                    
                        GetReferralProvider(oTransaction.PatientID, oTransaction.ProviderID, oTransaction.ReferalProviderID_New, oTransaction.IsSameAsBillingProvider, _databaseconnectionstring);
                        dtpPhysicianSignDate.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.TransactionDate);
                        txtReferringProvider_NPI.Text = _ReferralNPI.Trim();
                        txtReferringProviderName.Text = _ReferralFName.Trim() + " " + _ReferralMName.Trim() + " " + _ReferralLName.Trim() + " " + _ReferralsSuffix.Trim();
                        if (txtReferringProviderName.Text.Trim() == oTransaction.ProviderName.Trim())
                        {
                            txtReferringProviderName.Text = txtReferringProviderName.Text.Trim() + " " + oTransaction.ProviderSuffix;
                        }
                        txtReferringProviderName.Tag = Convert.ToString(_ReferralId);
                        if (txtReferringProviderName.Text.Trim()  != "" && oTransaction.ProviderIDQualifier!="")
                        {
                            txtReferringProviderQualifier.Text = oTransaction.ProviderIDQualifier;
                        }

                        #region Referral/Physicain Other Info
                        if (!oTransaction.IsSameAsBillingProvider)
                        {
                            FillOtherReferrinfProviderInfo(Convert.ToInt64(oTransaction.ContactID), "Referring Provider Other ID Type", Convert.ToInt64(oTransaction.ReferalProviderID_New), oTransaction.IsSameAsBillingProvider);
                        }
                        else
                        {
                            FillOtherReferrinfProviderInfo(Convert.ToInt64(oTransaction.ContactID), "Referring Provider Other ID Type", Convert.ToInt64(oTransaction.ProviderID), oTransaction.IsSameAsBillingProvider);
                        }
                        #endregion  Referral/Physicain Other Info

                        #endregion Referral/Physicain Info

                        #region Patient Condition Related To Region

                        if (oTransaction.WorkersComp == true)
                        {

                            if (oTransaction.InjuryDate > 0)
                            {
                                dtpDateOfCurrentIllness.Checked = true;
                                dtpDateOfCurrentIllness.Enabled = true;
                                dtpDateOfCurrentIllness.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.InjuryDate);
                                txtDateOfCurrentIllnessQualifier.Text = "431";
                            }
                            else
                            {
                                dtpDateOfCurrentIllness.Checked = false;
                                dtpDateOfCurrentIllness.Enabled = false;
                                txtDateOfCurrentIllnessQualifier.Text = "";
                            }
                        }
                        else if (oTransaction.AutoClaim == true)
                        {
                            if (oTransaction.AccidentDate > 0)
                            {
                                dtpDateOfCurrentIllness.Checked = true;
                                dtpDateOfCurrentIllness.Enabled = true;
                                dtpDateOfCurrentIllness.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.AccidentDate);
                                txtDateOfCurrentIllnessQualifier.Text = "431";
                            }
                            else
                            {
                                dtpDateOfCurrentIllness.Checked = false;
                                dtpDateOfCurrentIllness.Enabled = false;
                                txtDateOfCurrentIllnessQualifier.Text = "";
                            }
                        }
                        else if (oTransaction.OtherAccident == true)
                        {
                            if (oTransaction.OtherAccidentDate > 0)
                            {
                                dtpDateOfCurrentIllness.Checked = true;
                                dtpDateOfCurrentIllness.Enabled = true;
                                dtpDateOfCurrentIllness.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.OtherAccidentDate);
                                txtDateOfCurrentIllnessQualifier.Text = "431";
                            }
                            else
                            {
                                dtpDateOfCurrentIllness.Checked = false;
                                dtpDateOfCurrentIllness.Enabled = false;
                            }
                        }
                        else
                        {
                            if (oTransaction.OnsiteDate > 0)
                            {
                                dtpDateOfCurrentIllness.Checked = true;
                                dtpDateOfCurrentIllness.Enabled = true;
                                dtpDateOfCurrentIllness.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.OnsiteDate);
                                txtDateOfCurrentIllnessQualifier.Text = oTransaction.sBox14DateQualifier;
                            }
                            else
                            {
                                dtpDateOfCurrentIllness.Checked = false;
                                dtpDateOfCurrentIllness.Enabled = false;
                                txtDateOfCurrentIllnessQualifier.Text = "";
                                
                            }
                        }

                        if (oTransaction.ndtBox15Date > 0)
                        {
                            dtpSimilarIllnessFirstDate.Enabled = true;
                            dtpSimilarIllnessFirstDate.Checked = true;
                            dtpSimilarIllnessFirstDate.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.ndtBox15Date);
                            // Line edited on 12162013 Sameer
                            txtSimilarIllnessFirstDateQualifier.Text = oTransaction.sBox15DateQualifier;//"454";
                        }
                        else
                        {
                            dtpSimilarIllnessFirstDate.Enabled = false;
                            dtpSimilarIllnessFirstDate.Checked = false;
                            txtSimilarIllnessFirstDateQualifier.Text = "";
                        }


                        if (oTransaction.UnableToWorkFromDate > 0)
                        {
                            dtpUnableToWorkFrom.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.UnableToWorkFromDate);
                            dtpUnableToWorkFrom.Checked = true;
                            dtpUnableToWorkFrom.Enabled = true;
                        }
                      
                        else
                        {
                            dtpUnableToWorkFrom.Checked = false;
                            dtpUnableToWorkFrom.Enabled = false;
                        }

                        if (oTransaction.UnableToWorkTillDate > 0)
                        {
                            dtpUnableToWorkTill.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.UnableToWorkTillDate);
                            dtpUnableToWorkTill.Checked = true;
                            dtpUnableToWorkTill.Enabled = true;
                        }
                        
                        else
                        {
                            dtpUnableToWorkTill.Checked = false;
                            dtpUnableToWorkTill.Enabled = false;
                        }

                        if (oTransaction.AutoClaim == true)
                        {
                            chkPatientCoditionRelatedTo_AutoAccident_Yes.Checked = true;
                            txtPatientCoditionRelatedTo_State.Text = oTransaction.State.Trim();
                            if (oTransaction.AccidentDate > 0)
                            {

                            }
                        }
                        else
                        {
                            chkPatientCoditionRelatedTo_AutoAccident_No.Checked = true;
                            txtPatientCoditionRelatedTo_State.Text = "";
                        }

                        if (oTransaction.WorkersComp == true)
                        {
                            chkPatientCoditionRelatedTo_Employment_Yes.Checked = true;
                        }
                        else
                        {
                            chkPatientCoditionRelatedTo_Employment_No.Checked = true;
                        }
                 
                        if (oTransaction.OtherAccident == true)
                        {
                            chkPatientCoditionRelatedTo_OtherAccident_Yes.Checked = true;
                            chkPatientCoditionRelatedTo_OtherAccident_No.Checked = false;
                        }
                        else
                        {
                            chkPatientCoditionRelatedTo_OtherAccident_No.Checked = true;
                            chkPatientCoditionRelatedTo_OtherAccident_Yes.Checked = false;
                        }

                        if (oTransaction.HospitalizationDateFrom > 0)
                        {
                            dtpHospitalisationFrom.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.HospitalizationDateFrom);
                            dtpHospitalisationFrom.Checked = true;
                            dtpHospitalisationFrom.Enabled = true;
                        }
                   
                        else
                        {
                            dtpHospitalisationFrom.Checked = false;
                            dtpHospitalisationFrom.Enabled = false;
                        }


                        if (oTransaction.HospitalizationDateTo > 0)
                        {
                            dtpHospitalisationTo.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.HospitalizationDateTo);
                            dtpHospitalisationTo.Checked = true;
                            dtpHospitalisationTo.Enabled = true;
                        }
                   
                        else
                        {
                            dtpHospitalisationTo.Checked = false;
                            dtpHospitalisationTo.Enabled = false;
                        }


                        #endregion Patient Condition Related To Region

                       #region Patient Relationship To Insured

                        if (_SubscriberRelationshipCode == "01")//01=Spouse
                        {
                            chkRelationship_Spouse.Checked = true;
                        }
                        else if (_SubscriberRelationshipCode == "18")//18=Self
                        {
                            chkRelationship_Self.Checked = true;
                        }
                        else if (_SubscriberRelationshipCode == "19")//19=Child
                        {
                            chkRelationship_Child.Checked = true;
                        }
                        else
                        {
                            chkRelationship_Other.Checked = true;
                        }

                        #endregion Patient Relationship To Insured

                        #region Patient Insurance Type

                        //Insurance Type
                        if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "MB")
                        {
                            chkMedicare.Checked = true;
                        }
                        else if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "MC")
                        {
                            chkMedicaid.Checked = true;
                        }
                        else if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "CH")
                        {
                            chkTricareChampus.Checked = true;
                        }
                        else if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "CI" || _SubscriberInsuranceBelongs.Trim().ToUpper() == "HM")
                        {
                            chkGroupHealthPlan.Checked = true;
                        }
                        else
                        {
                            chkOtherInsuranceType.Checked = true;
                        }

                        #endregion Patient Insurance Type

                        #region Other Insurance Details

                        if (IsSecondaryInsurance == true)
                        {
                            //Gender for Other Insurance Subscriber
                            if (_OtherInsuranceSubscriberGender.ToUpper().Trim() != "")
                            {
                                if (_OtherInsuranceSubscriberGender.ToUpper().Trim().Substring(0, 1) == "M")
                                {
                             
                                }
                                else if (_OtherInsuranceSubscriberGender.ToUpper().Trim().Substring(0, 1) == "F")
                                {
                             
                                }
                            }
                            else
                            {
                               // chkOtherInsuredSex_Male.Checked = false;
                               // chkOtherInsuredSex_Female.Checked = false;
                            }

                            chkIsOtherHealthPlan_Yes.Checked = true;
                            txtOtherInsuredInsuranceName.Text = _OtherInsuranceName.Trim();
                            txtOtherInsuredInsuranceName.Tag = _PatientOtherInsuranceID;

                            txtOtherInsuredName.Text = _OtherInsuranceSubscriberLName.Trim() + " " + _OtherInsuranceSubscriberFName.Trim() + " " + _OtherInsuranceSubscriberMName.Trim();//NameFormation(_OtherInsuranceSubscriberLName.Trim(),"", _OtherInsuranceSubscriberFName.Trim(),_OtherInsuranceSubscriberMName.Trim());

                            if (_OtherInsuranceID.Trim() != "")
                            {
                                txtOtherInsuredPolicyNo.Text = _OtherInsuranceID.Trim();
                                if (_OtherInsuranceGroupID.Trim() != "")
                                {
                                    txtOtherInsuredPolicyNo.Text += " - " + _OtherInsuranceGroupID.Trim();
                                }
                            }
                            else if (_OtherInsuranceGroupID.Trim() != "")
                            {
                                txtOtherInsuredPolicyNo.Text += _OtherInsuranceGroupID.Trim();
                            }
                            _sOtherInsuredPolicyNo = txtOtherInsuredPolicyNo.Text;

                        }
                        else
                        {
                          chkIsOtherHealthPlan_No.Checked = true;
                        }

                        #endregion Other Insurance Details

                        #region "Referral Code for Box10 d"

                        if (oTransaction.Box10d != "")
                            txtReserveForLocalUse.Text = oTransaction.Box10d;
                        else
                        {
                            if (oTransaction.Box19AndIncludeReferal != null && oTransaction.Box19AndIncludeReferal.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(oTransaction.Box19AndIncludeReferal.Rows[0]["bPaperIncludeReferralCode"]))
                                {
                                    txtReserveForLocalUse.Text = oTransaction.ReferralCode;
                                }
                            }
                            
                        }
                        #endregion
                      
                        #region Employers Name

                        txtInsuredEmployerORSchoolName.Text = _EmployerName;
                        //txtOtherInsuredEmployerORSchoolName.Text = _OtherEmployerName;
                        txtOtherClaimIDQualifier.Text = "Y4";

                        #endregion

                        #region Prior Authorization And Patient Account No

                        txtPriorAuthorizationNumber.Text = GetPriorAuthorizationNumber(oTransaction.TransactionMasterID).Trim().Replace("*","");

                        if (_PatientAccountCode == "Claim Number")
                        {
                            if (gloGlobal.gloPMGlobal.IsUseClaimPrefix && gloGlobal.gloPMGlobal.sClaimPrefix!="")
                            {
                                txtPatientAccountNo.Text = gloGlobal.gloPMGlobal.sClaimPrefix + oTransaction.ClaimNumber;
                            }
                            else
                            {
                                txtPatientAccountNo.Text = oTransaction.ClaimNumber;
                            }
                        }
                        else
                        {
                            txtPatientAccountNo.Text = oTranDetails.HCFA_PatientCode.Trim();
                        }


                        #endregion Prior Authorization And Patient Account No

                        #region " Diagnosis  "

                                               ArrayList alDiagnosis = new ArrayList();
                        // -------------------------------

                        DataTable dtDx = new DataTable();
                        dtDx = GetClaimPrimaryDiagnosis(oTransaction.TransactionID, oTransaction.ClinicID, oTransaction.ClaimNo);
                        txtIcdInd1.Text = Convert.ToString(oTransaction.nICDRevision).Replace("1", ""); // Removed 1 from nICDRevision because ICD 10's indicator is 0 as per instruction manual
                        txtIcdInd2.Text = string.Empty;
                        #region "Remove Dignosis As per Selection"
                        //bool _IsDXFound = false;
                        //for (int _Dxcount =dtDx.Rows.Count-1; _Dxcount >=0 ; _Dxcount--)
                        //{
                        //    string sDx = String.Empty;
                        //    sDx = Convert.ToString(dtDx.Rows[_Dxcount]["Dx"]).Trim();
                        //    for (int _RemoveCount = 0; _RemoveCount < oTransaction.Lines.Count; _RemoveCount++)
                        //    {
                        //        _IsDXFound = false;
                        //        if ((oTransaction.Lines[_RemoveCount].Dx1Code.Equals(sDx)) || (oTransaction.Lines[_RemoveCount].Dx2Code.Equals(sDx)) || (oTransaction.Lines[_RemoveCount].Dx3Code.Equals(sDx)) || (oTransaction.Lines[_RemoveCount].Dx4Code.Equals(sDx)))
                        //        {
                        //            _IsDXFound = true;
                        //            break;
                        //        }
                        //    }
                        //    if(_IsDXFound==false)
                        //    {
                        //        dtDx.Rows[_Dxcount].Delete();
                        //        dtDx.AcceptChanges();
                        //    }
                        //}

                        #endregion

                            if (dtDx != null && dtDx.Rows.Count > 0)
                            {
                                for (int k = 0; k < dtDx.Rows.Count; k++)
                                {
                                    // -------------------------------
                                    // Line added by Pankaj Bedse on 30012010
                                    // For Dx Pointer Issue
                                    alDiagnosis.Add(Convert.ToString(dtDx.Rows[k]["DX"]));
                                    // -------------------------------


                                    //string[] Dx = Convert.ToString(dtDx.Rows[k]["DX"]).Split('-');
                                    string Dx = Convert.ToString(dtDx.Rows[k]["DX"]);
                                    if (k == 0)
                                    {
                                        if (Dx.Length > 0)
                                        {
                                         
                                                txtDiagnosisCode11.Text = Dx.Replace(".","");
                                             
                                        }
                                    }
                                    if (k == 1)
                                    {
                                        
                                            if (Dx.Length > 1)
                                            {
                                                txtDiagnosisCode21.Text = Dx.Replace(".", "");
                                            }
                                    }
                                    if (k == 2)
                                    {
                                        if (Dx.Length > 0)
                                        {
                                            
                                               txtDiagnosisCode31.Text = Dx.Replace(".", "");
                                        }
                                    }
                                    if (k == 3)
                                    {
                                        if (Dx.Length > 0)
                                        {
                                           
                                                txtDiagnosisCode41.Text = Dx.Replace(".", "");
                                              
                                        }
                                    }

                                    if (k == 4)
                                    {
                                        if (Dx.Length > 0)
                                        {
                                            
                                                txtDiagnosisCodeE.Text = Dx.Replace(".", "");
                                            
                                        }
                                    }


                                    if (k == 5)
                                    {
                                        if (Dx.Length > 0)
                                        {
                                           
                                                txtDiagnosisCodeF.Text = Dx.Replace(".", "");
                                              
                                        }
                                    }

                                    if (k == 6)
                                    {
                                        if (Dx.Length > 0)
                                        {
                                            
                                                txtDiagnosisCodeG.Text = Dx.Replace(".", "");
                                               
                                        }
                                    }

                                    if (k == 7)
                                    {
                                        if (Dx.Length > 0)
                                        {
                                           
                                                txtDiagnosisCodeH.Text = Dx.Replace(".", "");
                                              
                                        }
                                    }

                                    if (k == 8)      //   if (k == 8)
                                    {
                                        if (Dx.Length > 0)
                                        {
                                           
                                                txtDiagnosisCodeI.Text = Dx.Replace(".", "");
                                              
                                        }
                                    }

                                    if (k == 9)  //  if (k == 9)
                                    {
                                        if (Dx.Length > 0)
                                        {
                                          
                                                txtDiagnosisCodeJ.Text = Dx.Replace(".", "");
                                              
                                        }
                                    }

                                    if (k == 10)     // if (k == 10)
                                    {
                                        if (Dx.Length > 0)
                                        {
                                           
                                                txtDiagnosisCodeK.Text = Dx.Replace(".", "");
                                             
                                        }
                                    }

                                    if (k == 11)      //if (k == 11)
                                    {
                                        if (Dx.Length > 0)
                                        {
                                          
                                                txtDiagnosisCodeL.Text = Dx.Replace(".", "");
                                            
                                        }
                                    }
                                }
                            }

                        #endregion " Diagnosis  "
                            //
                        
                        #region "GetEPSDT Code,Code Box and Claim Family Planning code ,claim Family Family CodeBox"
                            DataTable _dtEpsdt = new DataTable();
                            _dtEpsdt = GetEpsdtAndFamilyplanningCode(_ContactID);
                        #endregion
                            if (oTransaction.Lines.Count > 0)
                        {
                            //Set Transaction Lines
                            for (int j = 0; j < oTransaction.Lines.Count; j++)
                            {
                                if (j == 6) { break; }
                                //txtPriorAuthorizationNumber.Text = "";
                                if (gloSettings.Box23Setting.Box23.GetHashCode() == GetPaperBillingSettings(gloSettings.PaperBillingBoxtype.Box23))
                                {
                                    if (txtPriorAuthorizationNumber.Text.Trim() == "")
                                    {
                                        if (oTransaction.Lines[j].IsLabCPT)
                                        {
                                            txtPriorAuthorizationNumber.Text = Convert.ToString(oTransaction.Lines[j].AuthorizationNo);
                                        }
                                    }
                                }
                                else 
                                {
                                    if (txt19ReservedForLocalUse.Text == "")
                                    {
                                        if (oTransaction.Lines[j].IsLabCPT)
                                            txt19ReservedForLocalUse.Text = Convert.ToString(oTransaction.Lines[j].AuthorizationNo);
                                    }
                                }
                               
                                #region Transaction Line No 1

                                if (j == 0)
                                {
                                    //..Append the Anesthesia data before the note
                                    if (oTransaction.Lines[j].Anesthesia != null && oTransaction.Lines[j].Anesthesia.Trim() != "")
                                    {
                                        txtNotes1.Text = oTransaction.Lines[j].Anesthesia + "  ";
                                    }



                                    //..Append the NDC code data before the note
                                    if (oTransaction.Lines[j].DisplayNDCCode_HCFA != null && oTransaction.Lines[j].DisplayNDCCode_HCFA.Trim() != "")
                                    {
                                        //txtNotes1.Text = oTransaction.Lines[j].DisplayNDCCode_HCFA + "      ;";
                                        txtNotes1.Text += oTransaction.Lines[j].DisplayNDCCode_HCFA + "  ";
                                    }

                                    if (IsNotesInLines == true)
                                    {
                                        if (oTransaction.Lines[j].LineNotes.Count > 0)
                                        {
                                            if (txtNotes1.Text.Trim() != "")
                                            { txtNotes1.Text += oTransaction.Lines[j].LineNotes[0].NoteDescription + " "; }
                                            else
                                            { txtNotes1.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription + " "; }
                                        }
                                    }

                                    if (oTransaction.Lines[j].NDCPrescriptionDesc != "")
                                    { 
                                            if (txtNotes1.Text.Trim() != "")
                                            { txtNotes1.Text += oTransaction.Lines[j].NDCPrescriptionDesc.Replace("\r\n", ""); }
                                            else
                                            { txtNotes1.Text = oTransaction.Lines[j].NDCPrescriptionDesc.Replace("\r\n", ""); }
                                        
                                    }
                                    if(oTransaction.Lines[j].IsMammogramCertNumber==true)
                                    {
                                        if(txtNotes1.Text.Trim()!="")
                                        {
                                           txtNotes1.Text += oTransaction.HCFA_FacilityMammogramCertNumber.Replace("\r\n", "");
                                        }
                                        else
                                        {
                                            txtNotes1.Text=oTransaction.HCFA_FacilityMammogramCertNumber.Replace("\r\n","");
                                        }
                                    }

                                   

                                    if (txtNotes1.Text.Trim() != "")
                                    {
                                     
                                        string strNotes = getStringFitinTextBox(txtNotes1, txtNotes1.Text.Trim());
                                        txtNotes1.Text = strNotes.Trim();
                                    }

                                    dtpDOS1From.Value = oTransaction.Lines[j].DateServiceFrom;
                                    dtpDOS1To.Value = oTransaction.Lines[j].DateServiceTill;

                                    txtPOS1.Text = Convert.ToString(oTransaction.Lines[j].POSCode);
                                    txtPOS1.Tag = Convert.ToString(oTransaction.Lines[j].POSDescription);

                                    txtEMG1.Text = "";
                                    if(Convert.ToBoolean(oTransaction.Lines[j].EMG)==true)
                                    {
                                        if (_IsEMGAsX)
                                        {
                                            txtEMG1.Text = "X";
                                        }
                                        else
                                        {
                                            txtEMG1.Text = "Y";
                                        }
                                    }

                                  
                                    if(Convert.ToString(oTransaction.Lines[j].CPTCode).Trim() == Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode).Trim() || Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode).Trim() == "" || oTransaction.Lines[j].CrosswalkCPTCode == null )
                                    {
                                    txtCPT1.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
                                    }
                                    else
                                    {
                                        txtCPT1.Text = Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode);
                                    }
                                   
                                    if (Convert.ToString(oTransaction.Lines[j].DateServiceFrom) != "")
                                    {
                                        //dtpPhysicianSignDate.Value = oTransaction.Lines[j].DateServiceFrom;
                                        //dtpPatientSignDate.Value = oTransaction.Lines[j].DateServiceFrom;
                                    }
                                    
                                    txtMOD11.Text = Convert.ToString(oTransaction.Lines[j].Mod1Code);
                                    txtMOD11.Tag = Convert.ToString(oTransaction.Lines[j].Mod1Description);
                                    txtMOD12.Text = Convert.ToString(oTransaction.Lines[j].Mod2Code);
                                    txtMOD12.Tag = Convert.ToString(oTransaction.Lines[j].Mod2Description);
                                    txtMOD13.Text = Convert.ToString(oTransaction.Lines[j].Mod3Code);
                                    txtMOD13.Tag = Convert.ToString(oTransaction.Lines[j].Mod3Description);
                                    txtMOD14.Text = Convert.ToString(oTransaction.Lines[j].Mod4Code);
                                    txtMOD14.Tag = Convert.ToString(oTransaction.Lines[j].Mod4Description);
                                    txtDxPtr1.Text = GetDxPointers(alDiagnosis, j);
                                    txtUnits1.Text = Convert.ToDecimal(oTransaction.Lines[j].Unit).ToString("#############0.####");
                                    if (Convert.ToBoolean(_dtEpsdt.Rows[0]["bBillEPSDTorFamilyPlanning"]))
                                    {
                                        if ((oTransaction.Lines[j].IsFamilyPlanningIndicator))
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCode"].ToString() != "")
                                            {
                                                if (_dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCodeBox"].ToString() == "24h unshaded")
                                                    txtEPSDT1.Text = _dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCode"].ToString();
                                            }


                                        }
                                        if ((oTransaction.Lines[j].IsServiceScreening) && oTransaction.IsEPSDTReferral)
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                txtEPSDT1.Text = oTransaction.ReferralType;
                                            else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                                txtEMG1.Text = oTransaction.ReferralType;
                                            else
                                                txtEPSDTShaded1.Text = oTransaction.ReferralType;
                                        }
                                        else if ((oTransaction.Lines[j].IsServiceScreening) && !oTransaction.IsEPSDTReferral)
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                txtEPSDT1.Text = "NU";
                                            else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                            {
                                                txtEMG1.Text = "NU";
                                            }
                                            else
                                                txtEPSDTShaded1.Text = "NU";
                                        }

                                        if ((oTransaction.Lines[j].IsServiceScreening))
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString() != "")
                                            {
                                                if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                    txtEPSDT1.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                                else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                                    txtEMG1.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                                else
                                                    txtEPSDTShaded1.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                            }
                                        }
                                    }
                                    string[] Charges = Convert.ToString(oTransaction.Lines[j].Total).Split('.');
                                    if (Charges.Length > 0)
                                    {
                                        if (Charges.Length > 1)
                                        {
                                            txtCharges1.Text = Charges[0];
                                            txtCharges11.Text = Charges[1];
                                        }
                                        else
                                        {
                                            txtCharges1.Text = Charges[0];
                                        }
                                    }
                                    cmbRenderingProvider1.SelectedValue = oTransaction.Lines[j].RefferingProviderId;
                                    if (!Convert.ToBoolean(_dtEpsdt.Rows[0]["bSupressRenderEPSDTClaimOnPaperEDI"])||!oTransaction.bIsEPSDTScreening)
                                    {
                                            dtProviders = ogloBilling.GetMidLevelProviders(oTransaction.Lines[0].RefferingProviderId, oTransaction.ProviderID, oTransaction.ContactID, oTransaction.ClinicID);
                                            if (dtProviders != null && dtProviders.Rows.Count > 0)
                                            {
                                                _MidLevelRenderingProviderIDLineWise = Convert.ToInt64(dtProviders.Rows[0]["nORenderingProviderID"]);
                                            }

                                            DataTable _dtProvider;
                                            if (oTranDetails.HCFA_FacilityID == "")
                                            {
                                                _dtProvider = FillRenderingProvider(_MidLevelRenderingProviderIDLineWise, 0, Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type");
                                            }
                                            else
                                            {
                                                _dtProvider = FillRenderingProvider(_MidLevelRenderingProviderIDLineWise, Convert.ToInt64(oTranDetails.HCFA_FacilityID), Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type");
                                            }

                                            if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                                            {
                                                //if (Convert.ToString(_dtProvider.Rows[0]["QualifierDescription"]) != "" && !Convert.ToString(_dtProvider.Rows[0]["QualifierDescription"]).Contains("NPI"))
                                                if (Convert.ToString(_dtProvider.Rows[0]["QualifierMstID"]) != "1" && _dtProvider.Rows[0]["QualifierValue"] != null)
                                                {
                                                    if (Convert.ToString(_dtProvider.Rows[0]["QualifierValue"]) != "")
                                                    {
                                                        txtRenderingProvider1_QualifierValue.Text = Convert.ToString(_dtProvider.Rows[0]["QualifierValue"]);
                                                        txtRenderingProvider1_Qualifier.Text = Convert.ToString(_dtProvider.Rows[0]["Qualifier"]);

                                                    }
                                                    txtRenderingProvider1_NPI.Text = Convert.ToString(_dtProvider.Rows[0]["ProviderNPI"]);
                                                    txtRenderingProvider1_NPI.Tag = Convert.ToString(oTransaction.Lines[j].RefferingProviderId).Trim();
                                                }
                                                else
                                                {
                                                    txtRenderingProvider1_NPI.Text = Convert.ToString(_dtProvider.Rows[0]["ProviderNPI"]);
                                                    txtRenderingProvider1_NPI.Tag = Convert.ToString(oTransaction.Lines[j].RefferingProviderId).Trim();
                                                }
                                            }
                                        
                                    }     
                                }//end of if(j==0)

                                #endregion

                                #region Transaction Line No 2

                                else if (j == 1)
                                {

                                    //..Append the Anesthesia data before the note
                                    if (oTransaction.Lines[j].Anesthesia != null && oTransaction.Lines[j].Anesthesia.Trim() != "")
                                    {
                                        txtNotes2.Text = oTransaction.Lines[j].Anesthesia + "  ";
                                    }

                                    //..Append the NDC code data before the note
                                    if (oTransaction.Lines[j].DisplayNDCCode_HCFA != null && oTransaction.Lines[j].DisplayNDCCode_HCFA.Trim() != "")
                                    {
                                        txtNotes2.Text += oTransaction.Lines[j].DisplayNDCCode_HCFA + "  ";
                                    }

                                    if (IsNotesInLines == true)
                                    {
                                        if (oTransaction.Lines[j].LineNotes.Count > 0)
                                        {
                                            if (txtNotes2.Text.Trim() != "")
                                            { txtNotes2.Text += oTransaction.Lines[j].LineNotes[0].NoteDescription + " "; }
                                            else
                                            { txtNotes2.Text += oTransaction.Lines[j].LineNotes[0].NoteDescription + " "; }
                                        }
                                    }

                                    if (oTransaction.Lines[j].NDCPrescriptionDesc != "")
                                    {
                                        if (txtNotes2.Text.Trim() != "")
                                        { txtNotes2.Text += oTransaction.Lines[j].NDCPrescriptionDesc.Replace("\r\n", ""); }
                                        else
                                        { txtNotes2.Text += oTransaction.Lines[j].NDCPrescriptionDesc.Replace("\r\n", "");  }

                                    }

                                    if (oTransaction.Lines[j].IsMammogramCertNumber == true)
                                    {
                                        if (txtNotes2.Text.Trim() != "")
                                        {
                                            txtNotes2.Text += oTransaction.HCFA_FacilityMammogramCertNumber.Replace("\r\n", "");
                                        }
                                        else
                                        {
                                            txtNotes2.Text = oTransaction.HCFA_FacilityMammogramCertNumber.Replace("\r\n", "");
                                        }
                                    }
                                    if (txtNotes2.Text.Trim() != "")
                                    {
                                        string strNotes = getStringFitinTextBox(txtNotes2, txtNotes2.Text.Trim());
                                        txtNotes2.Text = strNotes.Trim().Replace("\r\n", "");
                                    }

                                    dtpDOS2From.Value = oTransaction.Lines[j].DateServiceFrom;
                                    dtpDOS2To.Value = oTransaction.Lines[j].DateServiceTill;

                                    txtPOS2.Text = Convert.ToString(oTransaction.Lines[j].POSCode);
                                    txtPOS2.Tag = Convert.ToString(oTransaction.Lines[j].POSDescription);

                                    txtEMG2.Text = "";
                                    if (Convert.ToBoolean(oTransaction.Lines[j].EMG) == true)
                                    {
                                        if (_IsEMGAsX)
                                        {
                                            txtEMG2.Text = "X";
                                        }
                                        else
                                        {
                                            txtEMG2.Text = "Y";
                                        }
                                    }

                                   if (Convert.ToString(oTransaction.Lines[j].CPTCode).Trim() == Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode).Trim() || Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode).Trim() == "" || oTransaction.Lines[j].CrosswalkCPTCode == null)
                                    {
                                        txtCPT2.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
                                    }
                                    else
                                    {
                                        txtCPT2.Text = Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode);
                                    }

                                    txtMOD21.Text = Convert.ToString(oTransaction.Lines[j].Mod1Code);
                                    txtMOD21.Tag = Convert.ToString(oTransaction.Lines[j].Mod1Description);
                                    txtMOD22.Text = Convert.ToString(oTransaction.Lines[j].Mod2Code);
                                    txtMOD22.Tag = Convert.ToString(oTransaction.Lines[j].Mod2Description);
                                    txtMOD23.Text = Convert.ToString(oTransaction.Lines[j].Mod3Code);
                                    txtMOD23.Tag = Convert.ToString(oTransaction.Lines[j].Mod3Description);
                                    txtMOD24.Text = Convert.ToString(oTransaction.Lines[j].Mod4Code);
                                    txtMOD24.Tag = Convert.ToString(oTransaction.Lines[j].Mod4Description);

                                   
                                    txtDxPtr2.Text = GetDxPointers(alDiagnosis, j); 
                                  
                                    txtUnits2.Text = Convert.ToDecimal(oTransaction.Lines[j].Unit).ToString("#############0.####");
                                    if (Convert.ToBoolean(_dtEpsdt.Rows[0]["bBillEPSDTorFamilyPlanning"]))
                                    {
                                        if ((oTransaction.Lines[j].IsFamilyPlanningIndicator))
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCode"].ToString() != "")
                                            {
                                                if (_dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCodeBox"].ToString() == "24h unshaded")
                                                    txtEPSDT2.Text = _dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCode"].ToString();
                                            }


                                        }
                                        if ((oTransaction.Lines[j].IsServiceScreening) && oTransaction.IsEPSDTReferral)
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                txtEPSDT2.Text = oTransaction.ReferralType;
                                            else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                                txtEMG2.Text = oTransaction.ReferralType;
                                            else
                                                txtEPSDTShaded2.Text = oTransaction.ReferralType;
                                        }
                                        else if ((oTransaction.Lines[j].IsServiceScreening) && !oTransaction.IsEPSDTReferral)
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                txtEPSDT2.Text = "NU";
                                            else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                            {
                                                txtEMG2.Text = "NU";
                                            }
                                            else
                                                txtEPSDTShaded2.Text = "NU";

                                        }
                                        if ((oTransaction.Lines[j].IsServiceScreening))
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString() != "")
                                            {
                                                if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                    txtEPSDT2.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                                else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                                    txtEMG2.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                                else
                                                    txtEPSDTShaded2.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                            }
                                        }
                                    }
                                    string[] Charges = Convert.ToString(oTransaction.Lines[j].Total).Split('.');
                                    if (Charges.Length > 0)
                                    {
                                        if (Charges.Length > 1)
                                        {
                                            txtCharges2.Text = Charges[0];
                                            txtCharges21.Text = Charges[1];
                                        }
                                        else
                                        {
                                            txtCharges2.Text = Charges[0];
                                        }
                                    }
                              
                                    cmbRenderingProvider2.SelectedValue = oTransaction.Lines[j].RefferingProviderId;
                                    
                                    if (!Convert.ToBoolean(_dtEpsdt.Rows[0]["bSupressRenderEPSDTClaimOnPaperEDI"])||!oTransaction.bIsEPSDTScreening)
                                    {
                                        
                                            dtProviders = ogloBilling.GetMidLevelProviders(oTransaction.Lines[1].RefferingProviderId, oTransaction.ProviderID, oTransaction.ContactID, oTransaction.ClinicID);
                                            if (dtProviders != null && dtProviders.Rows.Count > 0)
                                            {
                                                _MidLevelRenderingProviderIDLineWise = Convert.ToInt64(dtProviders.Rows[0]["nORenderingProviderID"]);
                                            }


                                            DataTable _dtProvider = null;

                                            if (oTranDetails.HCFA_FacilityID == "")
                                            {
                                                _dtProvider = FillRenderingProvider(_MidLevelRenderingProviderIDLineWise, 0, Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type");
                                            }
                                            else
                                            {
                                                _dtProvider = FillRenderingProvider(_MidLevelRenderingProviderIDLineWise, Convert.ToInt64(oTranDetails.HCFA_FacilityID), Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type");
                                            }

                                            if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                                            {
                                                //if (Convert.ToString(_dtProvider.Rows[0]["QualifierDescription"]) != "" && !Convert.ToString(_dtProvider.Rows[0]["QualifierDescription"]).Contains("NPI"))
                                                if (Convert.ToString(_dtProvider.Rows[0]["QualifierMstID"]) != "1" && _dtProvider.Rows[0]["QualifierValue"] != null)
                                                {
                                                    if (Convert.ToString(_dtProvider.Rows[0]["QualifierValue"]) != "")
                                                    {
                                                        txtRenderingProvider2_QualifierValue.Text = Convert.ToString(_dtProvider.Rows[0]["QualifierValue"]);
                                                        txtRenderingProvider2_Qualifier.Text = Convert.ToString(_dtProvider.Rows[0]["Qualifier"]);
                                                    }
                                                    txtRenderingProvider2_NPI.Text = Convert.ToString(_dtProvider.Rows[0]["ProviderNPI"]);
                                                }
                                                else
                                                {
                                                    txtRenderingProvider2_NPI.Text = Convert.ToString(_dtProvider.Rows[0]["ProviderNPI"]);
                                                }
                                            }
                                       
                                    }
                                }//end of if(j==1)

                                #endregion

                                #region Transaction Line No 3

                                else if (j == 2)
                                {
                                    //if (IsNotesInLines == true)
                                    //{
                                    //    if (oTransaction.Lines[j].LineNotes.Count > 0)
                                    //    {
                                    //        txtNotes3.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription;
                                    //    }
                                    //}

                                    //..Append the Anesthesia data before the note
                                    if (oTransaction.Lines[j].Anesthesia != null && oTransaction.Lines[j].Anesthesia.Trim() != "")
                                    {
                                        txtNotes3.Text = oTransaction.Lines[j].Anesthesia + "  ";
                                    }

                                    //..Append the NDC code data before the note
                                    if (oTransaction.Lines[j].DisplayNDCCode_HCFA != null && oTransaction.Lines[j].DisplayNDCCode_HCFA.Trim() != "")
                                    {
                                        txtNotes3.Text += oTransaction.Lines[j].DisplayNDCCode_HCFA + "  ";
                                    }

                                    if (IsNotesInLines == true)
                                    {
                                        if (oTransaction.Lines[j].LineNotes.Count > 0)
                                        {
                                            if (txtNotes3.Text.Trim() != "")
                                            { txtNotes3.Text += oTransaction.Lines[j].LineNotes[0].NoteDescription+ " "; }
                                            else
                                            { txtNotes3.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription+ " "; }
                                        }
                                    }

                                    if (oTransaction.Lines[j].NDCPrescriptionDesc != "")
                                    {
                                        if (txtNotes3.Text.Trim() != "")
                                        { txtNotes3.Text += oTransaction.Lines[j].NDCPrescriptionDesc.Replace("\r\n", ""); }
                                        else
                                        { txtNotes3.Text = oTransaction.Lines[j].NDCPrescriptionDesc.Replace("\r\n", ""); }

                                    }

                                    if (oTransaction.Lines[j].IsMammogramCertNumber == true)
                                    {
                                        if (txtNotes3.Text.Trim() != "")
                                        {
                                            txtNotes3.Text += oTransaction.HCFA_FacilityMammogramCertNumber.Replace("\r\n", "");
                                        }
                                        else
                                        {
                                            txtNotes3.Text = oTransaction.HCFA_FacilityMammogramCertNumber.Replace("\r\n", "");
                                        }
                                    }
                                    if (txtNotes3.Text.Trim() != "")
                                    {
                                        //if (txtNotes3.Text.Trim().Length > 40)
                                        //{
                                        //    txtNotes3.Text = txtNotes3.Text.Trim().Substring(0, 39);
                                        //}
                                        //else
                                        //{
                                        //    txtNotes3.Text = txtNotes3.Text.Trim();
                                        //}
                                        string strNotes = getStringFitinTextBox(txtNotes3, txtNotes3.Text.Trim());
                                        txtNotes3.Text = strNotes.Trim().Replace("\r\n", "");
                                        //if (strNotes.Trim().Length >= 40 && strNotes.Trim().Length <= 47)
                                        //    txtNotes3.Text = strNotes.Trim().Substring(0, 40);
                                        //else
                                        //    txtNotes3.Text = (strNotes.Trim().Length > 40 ? strNotes.Trim().Substring(0, strNotes.Length - 7) : strNotes.Trim());
                                    }

                                    dtpDOS3From.Value = oTransaction.Lines[j].DateServiceFrom;
                                    dtpDOS3To.Value = oTransaction.Lines[j].DateServiceTill;

                                    txtPOS3.Text = Convert.ToString(oTransaction.Lines[j].POSCode);
                                    txtPOS3.Tag = Convert.ToString(oTransaction.Lines[j].POSDescription);

                                    txtEMG3.Text = "";
                                    if (Convert.ToBoolean(oTransaction.Lines[j].EMG) == true)
                                    {
                                        if (_IsEMGAsX)
                                        {
                                            txtEMG3.Text = "X";
                                        }
                                        else
                                        {
                                            txtEMG3.Text = "Y";
                                        }
                                    }

                                 //   txtCPT3.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
                                   // txtCPT3.Tag = Convert.ToString(oTransaction.Lines[j].CPTDescription);
                                    //Mahesh Nawal 20100713 Check the CrossWalk CPT
                                    if (Convert.ToString(oTransaction.Lines[j].CPTCode).Trim() == Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode).Trim() || Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode).Trim() == "" || oTransaction.Lines[j].CrosswalkCPTCode == null)
                                    {
                                        txtCPT3.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
                                    }
                                    else
                                    {
                                        txtCPT3.Text = Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode);
                                    }

                                    //dtpPatientSignDate.Value = oTransaction.Lines[j].DateServiceFrom;

                                    //if (oTransaction.Lines[j].Dx1Code != "")
                                    //{
                                    //    string[] Dx1Code = Convert.ToString(oTransaction.Lines[j].Dx1Code).Split('.');
                                    //    if (Dx1Code.Length > 0)
                                    //    {
                                    //        if (Dx1Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode11.Text = Dx1Code[0];
                                    //            txtDiagnosisCode12.Text = Dx1Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode11.Text = Convert.ToString(oTransaction.Lines[j].Dx1Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode11.Tag = Convert.ToString(oTransaction.Lines[j].Dx1Description);
                                    //}

                                    //if (oTransaction.Lines[j].Dx2Code != "")
                                    //{
                                    //    string[] Dx2Code = Convert.ToString(oTransaction.Lines[j].Dx2Code).Split('.');
                                    //    if (Dx2Code.Length > 0)
                                    //    {
                                    //        if (Dx2Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode21.Text = Dx2Code[0];
                                    //            txtDiagnosisCode22.Text = Dx2Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode21.Text = Convert.ToString(oTransaction.Lines[j].Dx2Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode21.Tag = Convert.ToString(oTransaction.Lines[j].Dx2Description);
                                    //}

                                    //if (oTransaction.Lines[j].Dx3Code != "")
                                    //{
                                    //    string[] Dx3Code = Convert.ToString(oTransaction.Lines[j].Dx3Code).Split('.');
                                    //    if (Dx3Code.Length > 0)
                                    //    {
                                    //        if (Dx3Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode31.Text = Dx3Code[0];
                                    //            txtDiagnosisCode32.Text = Dx3Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode31.Text = Convert.ToString(oTransaction.Lines[j].Dx3Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode31.Tag = Convert.ToString(oTransaction.Lines[j].Dx3Description);
                                    //}

                                    //if (oTransaction.Lines[j].Dx4Code != "")
                                    //{
                                    //    string[] Dx4Code = Convert.ToString(oTransaction.Lines[j].Dx4Code).Split('.');
                                    //    if (Dx4Code.Length > 0)
                                    //    {
                                    //        if (Dx4Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode41.Text = Dx4Code[0];
                                    //            txtDiagnosisCode42.Text = Dx4Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode41.Text = Convert.ToString(oTransaction.Lines[j].Dx4Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode41.Tag = Convert.ToString(oTransaction.Lines[j].Dx4Description);
                                    //}

                                    txtMOD31.Text = Convert.ToString(oTransaction.Lines[j].Mod1Code);
                                    txtMOD31.Tag = Convert.ToString(oTransaction.Lines[j].Mod1Description);
                                    txtMOD32.Text = Convert.ToString(oTransaction.Lines[j].Mod2Code);
                                    txtMOD32.Tag = Convert.ToString(oTransaction.Lines[j].Mod2Description);
                                    txtMOD33.Text = Convert.ToString(oTransaction.Lines[j].Mod3Code);
                                    txtMOD33.Tag = Convert.ToString(oTransaction.Lines[j].Mod3Description);
                                    txtMOD34.Text = Convert.ToString(oTransaction.Lines[j].Mod4Code);
                                    txtMOD34.Tag = Convert.ToString(oTransaction.Lines[j].Mod4Description);

                                    // --------------------------------------
                                    // Code added by Pankaj Bedse on 30012010
                                    // For Dx Pointer Issue
                                    //StringBuilder sbDxPts = new StringBuilder();
                                    //foreach (string sDx in alDiagnosis)
                                    //{
                                    //    if ((oTransaction.Lines[j].Dx1Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())) || (oTransaction.Lines[j].Dx2Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())) || (oTransaction.Lines[j].Dx3Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())) || (oTransaction.Lines[j].Dx4Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())))
                                    //    {
                                    //        int iDxPtr = alDiagnosis.IndexOf(sDx) + 1;
                                    //        sbDxPts = sbDxPts.Append(iDxPtr + ",");
                                    //    }
                                    //}

                                    //// solving Problem# - 231 
                                    //// show the Dx Pointer in sequence as they are enter.
                                    txtDxPtr3.Text = GetDxPointers(alDiagnosis, j); 

                                    // --------------------------------------
                                    // Old Code for Dxpointers
                                    // --------------------------------------
                                    //
                                    //if (oTransaction.Lines[j].Dx1Ptr)
                                    //{
                                    //    txtDxPtr3.Text = "1";
                                    //}
                                    //if (oTransaction.Lines[j].Dx2Ptr)
                                    //{
                                    //    txtDxPtr3.Text = txtDxPtr3.Text + "," + "2";
                                    //}
                                    //if (oTransaction.Lines[j].Dx3Ptr)
                                    //{
                                    //    txtDxPtr3.Text = txtDxPtr3.Text + "," + "3";
                                    //}
                                    //if (oTransaction.Lines[j].Dx4Ptr)
                                    //{
                                    //    txtDxPtr3.Text = txtDxPtr3.Text + "," + "4";
                                    //}
                                    // --------------------------------------

                                    txtUnits3.Text = Convert.ToDecimal(oTransaction.Lines[j].Unit).ToString("#############0.####");
                                    if (Convert.ToBoolean(_dtEpsdt.Rows[0]["bBillEPSDTorFamilyPlanning"]))
                                    {
                                        if ((oTransaction.Lines[j].IsFamilyPlanningIndicator))
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCode"].ToString() != "")
                                            {
                                                if (_dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCodeBox"].ToString() == "24h unshaded")
                                                    txtEPSDT3.Text = _dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCode"].ToString();
                                            }


                                        }
                                        if ((oTransaction.Lines[j].IsServiceScreening) && oTransaction.IsEPSDTReferral)
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                txtEPSDT3.Text = oTransaction.ReferralType;
                                            else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                                txtEMG3.Text = oTransaction.ReferralType;
                                            else
                                                txtEPSDTShaded3.Text = oTransaction.ReferralType;
                                        }
                                        else if ((oTransaction.Lines[j].IsServiceScreening) && !oTransaction.IsEPSDTReferral)
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                txtEPSDT3.Text = "NU";
                                            else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                            {
                                                txtEMG3.Text = "NU";
                                            }
                                            else
                                                txtEPSDTShaded3.Text = "NU";
                                        }
                                        if ((oTransaction.Lines[j].IsServiceScreening))
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString() != "")
                                            {
                                                if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                    txtEPSDT3.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                                else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                                    txtEMG3.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                                else
                                                    txtEPSDTShaded3.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                            }
                                        }
                                    }
                                    string[] Charges = Convert.ToString(oTransaction.Lines[j].Total).Split('.');
                                    if (Charges.Length > 0)
                                    {
                                        if (Charges.Length > 1)
                                        {
                                            txtCharges3.Text = Charges[0];
                                            txtCharges31.Text = Charges[1];
                                        }
                                        else
                                        {
                                            txtCharges3.Text = Charges[0];
                                        }
                                    }
                                    //FillProviderDetails(oTransaction.Lines[j].RefferingProviderId, ProviderType.RenderingProvider);
                                    cmbRenderingProvider3.SelectedValue = oTransaction.Lines[j].RefferingProviderId;
                                    //if (oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim() != "")
                                    //{
                                    //    txtRenderingProvider3_NPI.Text = FillRenderingProvider(Convert.ToInt64(oTransaction.Lines[j].RefferingProviderId), Convert.ToInt64(oTranDetails.HCFA_FacilityID), Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type"); 
                                    //    txtRenderingProvider3_NPI.Tag = Convert.ToString(oTransaction.Lines[j].RefferingProviderId);
                                    //}
                                     if (!Convert.ToBoolean(_dtEpsdt.Rows[0]["bSupressRenderEPSDTClaimOnPaperEDI"])||!oTransaction.bIsEPSDTScreening)
                                        {
                                            dtProviders = ogloBilling.GetMidLevelProviders(oTransaction.Lines[2].RefferingProviderId, oTransaction.ProviderID, oTransaction.ContactID, oTransaction.ClinicID);
                                            if (dtProviders != null && dtProviders.Rows.Count > 0)
                                            {
                                                _MidLevelRenderingProviderIDLineWise = Convert.ToInt64(dtProviders.Rows[0]["nORenderingProviderID"]);
                                            }

                                            DataTable _dtProvider = null;
                                            if (oTranDetails.HCFA_FacilityID == "")
                                            {
                                                _dtProvider = FillRenderingProvider(_MidLevelRenderingProviderIDLineWise, 0, Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type");
                                            }
                                            else
                                            {
                                                _dtProvider = FillRenderingProvider(_MidLevelRenderingProviderIDLineWise, Convert.ToInt64(oTranDetails.HCFA_FacilityID), Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type");
                                            }
                                            if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                                            {
                                                //if (Convert.ToString(_dtProvider.Rows[0]["QualifierDescription"]) != "" && !Convert.ToString(_dtProvider.Rows[0]["QualifierDescription"]).Contains("NPI"))
                                                if (Convert.ToString(_dtProvider.Rows[0]["QualifierMstID"]) != "1" && _dtProvider.Rows[0]["QualifierValue"] != null)
                                                {
                                                    if (Convert.ToString(_dtProvider.Rows[0]["QualifierValue"]) != "")
                                                    {
                                                        txtRenderingProvider3_QualifierValue.Text = Convert.ToString(_dtProvider.Rows[0]["QualifierValue"]);
                                                        txtRenderingProvider3_Qualifier.Text = Convert.ToString(_dtProvider.Rows[0]["Qualifier"]);
                                                    }
                                                    txtRenderingProvider3_NPI.Text = Convert.ToString(_dtProvider.Rows[0]["ProviderNPI"]);
                                                }
                                                else
                                                {
                                                    txtRenderingProvider3_NPI.Text = Convert.ToString(_dtProvider.Rows[0]["ProviderNPI"]);
                                                }
                                            }
                                        
                                    }
                                }//end of if(j==2)

                                #endregion

                                #region Transaction Line No 4

                                else if (j == 3)
                                {
                                    //if (IsNotesInLines == true)
                                    //{
                                    //    if (oTransaction.Lines[j].LineNotes.Count > 0)
                                    //    {
                                    //        txtNotes4.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription;
                                    //    }
                                    //}

                                    //..Append the Anesthesia data before the note
                                    if (oTransaction.Lines[j].Anesthesia != null && oTransaction.Lines[j].Anesthesia.Trim() != "")
                                    {
                                        txtNotes4.Text = oTransaction.Lines[j].Anesthesia + "  ";
                                    }


                                    //..Append the NDC code data before the note
                                    if (oTransaction.Lines[j].DisplayNDCCode_HCFA != null && oTransaction.Lines[j].DisplayNDCCode_HCFA.Trim() != "")
                                    {
                                        txtNotes4.Text += oTransaction.Lines[j].DisplayNDCCode_HCFA + "  ";
                                    }

                                    if (IsNotesInLines == true)
                                    {
                                        if (oTransaction.Lines[j].LineNotes.Count > 0)
                                        {
                                            if (txtNotes4.Text.Trim() != "")
                                            { txtNotes4.Text += oTransaction.Lines[j].LineNotes[0].NoteDescription + " "; }
                                            else
                                            { txtNotes4.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription + " "; }
                                        }
                                    }


                                    if (oTransaction.Lines[j].NDCPrescriptionDesc != "")
                                    {
                                        if (txtNotes4.Text.Trim() != "")
                                        { txtNotes4.Text += oTransaction.Lines[j].NDCPrescriptionDesc.Replace("\r\n", ""); }
                                        else
                                        { txtNotes4.Text = oTransaction.Lines[j].NDCPrescriptionDesc.Replace("\r\n", ""); }

                                    }

                                    if (oTransaction.Lines[j].IsMammogramCertNumber == true)
                                    {
                                        if (txtNotes4.Text.Trim() != "")
                                        {
                                            txtNotes4.Text += oTransaction.HCFA_FacilityMammogramCertNumber.Replace("\r\n", "");
                                        }
                                        else
                                        {
                                            txtNotes4.Text = oTransaction.HCFA_FacilityMammogramCertNumber.Replace("\r\n", "");
                                        }
                                    }
                                    if (txtNotes4.Text.Trim() != "")
                                    {
                                        //if (txtNotes4.Text.Trim().Length > 40)
                                        //{
                                        //    txtNotes4.Text = txtNotes4.Text.Trim().Substring(0, 39);
                                        //}
                                        //else
                                        //{
                                        //    txtNotes4.Text = txtNotes4.Text.Trim();
                                        //}
                                        string strNotes = getStringFitinTextBox(txtNotes4, txtNotes4.Text.Trim());
                                        txtNotes4.Text = strNotes.Trim().Replace("\r\n", "");
                                        //if (strNotes.Trim().Length >= 40 && strNotes.Trim().Length <= 47)
                                        //    txtNotes4.Text = strNotes.Trim().Substring(0, 40);
                                        //else
                                        //    txtNotes4.Text = (strNotes.Trim().Length > 40 ? strNotes.Trim().Substring(0, strNotes.Length - 7) : strNotes.Trim());
                                    }

                                    dtpDOS4From.Value = oTransaction.Lines[j].DateServiceFrom;
                                    dtpDOS4To.Value = oTransaction.Lines[j].DateServiceTill;

                                    txtPOS4.Text = Convert.ToString(oTransaction.Lines[j].POSCode);
                                    txtPOS4.Tag = Convert.ToString(oTransaction.Lines[j].POSDescription);

                                    txtEMG4.Text = "";
                                    if (Convert.ToBoolean(oTransaction.Lines[j].EMG) == true)
                                    {
                                        if (_IsEMGAsX)
                                        {
                                            txtEMG4.Text = "X";
                                        }
                                        else
                                        {
                                            txtEMG4.Text = "Y";
                                        }
                                    }

                                    //txtCPT4.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
                                    //txtCPT4.Tag = Convert.ToString(oTransaction.Lines[j].CPTDescription);

                                    //Mahesh Nawal 20100713 Check the CrossWalk CPT
                                    if (Convert.ToString(oTransaction.Lines[j].CPTCode).Trim() == Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode).Trim() || Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode).Trim() == "" || oTransaction.Lines[j].CrosswalkCPTCode == null)
                                    {
                                        txtCPT4.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
                                    }
                                    else
                                    {
                                        txtCPT4.Text = Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode);
                                    }

                                    //dtpPatientSignDate.Value = oTransaction.Lines[j].DateServiceFrom;
                                    //if (oTransaction.Lines[j].Dx1Code != "")
                                    //{
                                    //    string[] Dx1Code = Convert.ToString(oTransaction.Lines[j].Dx1Code).Split('.');
                                    //    if (Dx1Code.Length > 0)
                                    //    {
                                    //        if (Dx1Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode11.Text = Dx1Code[0];
                                    //            txtDiagnosisCode12.Text = Dx1Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode11.Text = Convert.ToString(oTransaction.Lines[j].Dx1Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode11.Tag = Convert.ToString(oTransaction.Lines[j].Dx1Description);
                                    //}

                                    //if (oTransaction.Lines[j].Dx2Code != "")
                                    //{
                                    //    string[] Dx2Code = Convert.ToString(oTransaction.Lines[j].Dx2Code).Split('.');
                                    //    if (Dx2Code.Length > 0)
                                    //    {
                                    //        if (Dx2Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode21.Text = Dx2Code[0];
                                    //            txtDiagnosisCode22.Text = Dx2Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode21.Text = Convert.ToString(oTransaction.Lines[j].Dx2Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode21.Tag = Convert.ToString(oTransaction.Lines[j].Dx2Description);
                                    //}

                                    //if (oTransaction.Lines[j].Dx3Code != "")
                                    //{
                                    //    string[] Dx3Code = Convert.ToString(oTransaction.Lines[j].Dx3Code).Split('.');
                                    //    if (Dx3Code.Length > 0)
                                    //    {
                                    //        if (Dx3Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode31.Text = Dx3Code[0];
                                    //            txtDiagnosisCode32.Text = Dx3Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode31.Text = Convert.ToString(oTransaction.Lines[j].Dx3Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode31.Tag = Convert.ToString(oTransaction.Lines[j].Dx3Description);
                                    //}

                                    //if (oTransaction.Lines[j].Dx4Code != "")
                                    //{
                                    //    string[] Dx4Code = Convert.ToString(oTransaction.Lines[j].Dx4Code).Split('.');
                                    //    if (Dx4Code.Length > 0)
                                    //    {
                                    //        if (Dx4Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode41.Text = Dx4Code[0];
                                    //            txtDiagnosisCode42.Text = Dx4Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode41.Text = Convert.ToString(oTransaction.Lines[j].Dx4Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode41.Tag = Convert.ToString(oTransaction.Lines[j].Dx4Description);
                                    //}

                                    txtMOD41.Text = Convert.ToString(oTransaction.Lines[j].Mod1Code);
                                    txtMOD41.Tag = Convert.ToString(oTransaction.Lines[j].Mod1Description);
                                    txtMOD42.Text = Convert.ToString(oTransaction.Lines[j].Mod2Code);
                                    txtMOD42.Tag = Convert.ToString(oTransaction.Lines[j].Mod2Description);
                                    txtMOD43.Text = Convert.ToString(oTransaction.Lines[j].Mod3Code);
                                    txtMOD43.Tag = Convert.ToString(oTransaction.Lines[j].Mod3Description);
                                    txtMOD44.Text = Convert.ToString(oTransaction.Lines[j].Mod4Code);
                                    txtMOD44.Tag = Convert.ToString(oTransaction.Lines[j].Mod4Description);

                                    // --------------------------------------
                                    // Code added by Pankaj Bedse on 30012010
                                    // For Dx Pointer Issue
                                    //StringBuilder sbDxPts = new StringBuilder();
                                    //foreach (string sDx in alDiagnosis)
                                    //{
                                    //    if ((oTransaction.Lines[j].Dx1Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())) || (oTransaction.Lines[j].Dx2Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())) || (oTransaction.Lines[j].Dx3Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())) || (oTransaction.Lines[j].Dx4Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())))
                                    //    {
                                    //        int iDxPtr = alDiagnosis.IndexOf(sDx) + 1;
                                    //        sbDxPts = sbDxPts.Append(iDxPtr + ",");
                                    //    }
                                    //}

                                    //// solving Problem# - 231 
                                    //// show the Dx Pointer in sequence as they are enter.
                                    txtDxPtr4.Text = GetDxPointers(alDiagnosis, j);

                                    // --------------------------------------
                                    // Old Code for Dxpointers
                                    // --------------------------------------
                                    //
                                    //if (oTransaction.Lines[j].Dx1Ptr)
                                    //{
                                    //    txtDxPtr4.Text = "1";
                                    //}
                                    //if (oTransaction.Lines[j].Dx2Ptr)
                                    //{
                                    //    txtDxPtr4.Text = txtDxPtr4.Text + "," + "2";
                                    //}
                                    //if (oTransaction.Lines[j].Dx3Ptr)
                                    //{
                                    //    txtDxPtr4.Text = txtDxPtr4.Text + "," + "3";
                                    //}
                                    //if (oTransaction.Lines[j].Dx4Ptr)
                                    //{
                                    //    txtDxPtr4.Text = txtDxPtr4.Text + "," + "4";
                                    //}
                                    // --------------------------------------

                                    txtUnits4.Text = Convert.ToDecimal(oTransaction.Lines[j].Unit).ToString("#############0.####");
                                    if (Convert.ToBoolean(_dtEpsdt.Rows[0]["bBillEPSDTorFamilyPlanning"]))
                                    {
                                        if ((oTransaction.Lines[j].IsFamilyPlanningIndicator))
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCode"].ToString() != "")
                                            {
                                                if (_dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCodeBox"].ToString() == "24h unshaded")
                                                    txtEPSDT4.Text = _dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCode"].ToString();
                                            }


                                        }
                                        if ((oTransaction.Lines[j].IsServiceScreening) && oTransaction.IsEPSDTReferral)
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                txtEPSDT4.Text = oTransaction.ReferralType;
                                            else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                                txtEMG4.Text = oTransaction.ReferralType;
                                            else
                                                txtEPSDTShaded4.Text = oTransaction.ReferralType;
                                        }
                                        else if ((oTransaction.Lines[j].IsServiceScreening) && !oTransaction.IsEPSDTReferral)
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                txtEPSDT4.Text = "NU";
                                            else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                            {
                                                txtEMG4.Text = "NU";
                                            }
                                            else
                                                txtEPSDTShaded4.Text = "NU";
                                        }
                                        if ((oTransaction.Lines[j].IsServiceScreening))
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString().ToString() != "")
                                            {
                                                if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                    txtEPSDT4.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                                else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                                    txtEMG4.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                                else
                                                    txtEPSDTShaded4.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                            }
                                        }
                                    }
                                    string[] Charges = Convert.ToString(oTransaction.Lines[j].Total).Split('.');
                                    if (Charges.Length > 0)
                                    {
                                        if (Charges.Length > 1)
                                        {
                                            txtCharges4.Text = Charges[0];
                                            txtCharges41.Text = Charges[1];
                                        }
                                        else
                                        {
                                            txtCharges4.Text = Charges[0];
                                        }
                                    }
                                    //FillProviderDetails(oTransaction.Lines[j].RefferingProviderId, ProviderType.RenderingProvider);
                                    cmbRenderingProvider4.SelectedValue = oTransaction.Lines[j].RefferingProviderId;
                                    //if (oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim() != "")
                                    //{
                                    //    txtRenderingProvider4_NPI.Text = FillRenderingProvider(Convert.ToInt64(oTransaction.Lines[j].RefferingProviderId), Convert.ToInt64(oTranDetails.HCFA_FacilityID),Convert.ToInt64( oTransaction.ContactID), "Paper Rendering Provider ID Type"); 
                                    //    txtRenderingProvider4_NPI.Tag = Convert.ToString(oTransaction.Lines[j].RefferingProviderId);
                                    //}
                                    if (!Convert.ToBoolean(_dtEpsdt.Rows[0]["bSupressRenderEPSDTClaimOnPaperEDI"])||!oTransaction.bIsEPSDTScreening)
                                    {
                                        
                                            dtProviders = ogloBilling.GetMidLevelProviders(oTransaction.Lines[3].RefferingProviderId, oTransaction.ProviderID, oTransaction.ContactID, oTransaction.ClinicID);
                                            if (dtProviders != null && dtProviders.Rows.Count > 0)
                                            {
                                                _MidLevelRenderingProviderIDLineWise = Convert.ToInt64(dtProviders.Rows[0]["nORenderingProviderID"]);
                                            }

                                            DataTable _dtProvider = null;
                                            if (oTranDetails.HCFA_FacilityID == "")
                                            {
                                                _dtProvider = FillRenderingProvider(_MidLevelRenderingProviderIDLineWise, 0, Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type");
                                            }
                                            else
                                            {
                                                _dtProvider = FillRenderingProvider(_MidLevelRenderingProviderIDLineWise, Convert.ToInt64(oTranDetails.HCFA_FacilityID), Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type");
                                            }
                                            if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                                            {
                                                //if (Convert.ToString(_dtProvider.Rows[0]["QualifierDescription"]) != "" && !Convert.ToString(_dtProvider.Rows[0]["QualifierDescription"]).Contains("NPI"))
                                                if (Convert.ToString(_dtProvider.Rows[0]["QualifierMstID"]) != "1" && _dtProvider.Rows[0]["QualifierValue"] != null)
                                                {
                                                    if (Convert.ToString(_dtProvider.Rows[0]["QualifierValue"]) != "")
                                                    {
                                                        txtRenderingProvider4_QualifierValue.Text = Convert.ToString(_dtProvider.Rows[0]["QualifierValue"]);
                                                        txtRenderingProvider4_Qualifier.Text = Convert.ToString(_dtProvider.Rows[0]["Qualifier"]);
                                                    }
                                                    txtRenderingProvider4_NPI.Text = Convert.ToString(_dtProvider.Rows[0]["ProviderNPI"]);
                                                }
                                                else
                                                {
                                                    txtRenderingProvider4_NPI.Text = Convert.ToString(_dtProvider.Rows[0]["ProviderNPI"]);
                                                }
                                            }
                                        
                                    }
                                }//End of if(j==3)

                                #endregion

                                #region Transaction Line No 5

                                else if (j == 4)
                                {
                                    //if (IsNotesInLines == true)
                                    //{
                                    //    if (oTransaction.Lines[j].LineNotes.Count > 0)
                                    //    {
                                    //        txtNotes5.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription;
                                    //    }
                                    //}


                                    //..Append the Anesthesia data before the note
                                    if (oTransaction.Lines[j].Anesthesia != null && oTransaction.Lines[j].Anesthesia.Trim() != "")
                                    {
                                        txtNotes5.Text = oTransaction.Lines[j].Anesthesia + "  ";
                                    }


                                    //..Append the NDC code data before the note
                                    if (oTransaction.Lines[j].DisplayNDCCode_HCFA != null && oTransaction.Lines[j].DisplayNDCCode_HCFA.Trim() != "")
                                    {
                                        txtNotes5.Text += oTransaction.Lines[j].DisplayNDCCode_HCFA + "  ";
                                    }

                                    if (IsNotesInLines == true)
                                    {
                                        if (oTransaction.Lines[j].LineNotes.Count > 0)
                                        {
                                            if (txtNotes5.Text.Trim() != "")
                                            { txtNotes5.Text += oTransaction.Lines[j].LineNotes[0].NoteDescription+ " "; }
                                            else
                                            { txtNotes5.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription+ " "; }
                                        }
                                    }

                                    if (oTransaction.Lines[j].NDCPrescriptionDesc != "")
                                    {
                                        if (txtNotes5.Text.Trim() != "")
                                        { txtNotes5.Text += oTransaction.Lines[j].NDCPrescriptionDesc.Replace("\r\n", ""); }
                                        else
                                        { txtNotes5.Text = oTransaction.Lines[j].NDCPrescriptionDesc.Replace("\r\n", ""); }

                                    }

                                    if (oTransaction.Lines[j].IsMammogramCertNumber == true)
                                    {
                                        if (txtNotes5.Text.Trim() != "")
                                        {
                                            txtNotes5.Text += oTransaction.HCFA_FacilityMammogramCertNumber.Replace("\r\n", "");
                                        }
                                        else
                                        {
                                            txtNotes5.Text = oTransaction.HCFA_FacilityMammogramCertNumber.Replace("\r\n", "");
                                        }
                                    }
                                    if (txtNotes5.Text.Trim() != "")
                                    {
                                        //if (txtNotes5.Text.Trim().Length > 40)
                                        //{
                                        //    txtNotes5.Text = txtNotes5.Text.Trim().Substring(0, 39);
                                        //}
                                        //else
                                        //{
                                        //    txtNotes5.Text = txtNotes5.Text.Trim();
                                        //}
                                        string strNotes = getStringFitinTextBox(txtNotes5, txtNotes5.Text.Trim());
                                        txtNotes5.Text = strNotes.Trim().Replace("\r\n", "");
                                        //if (strNotes.Trim().Length >= 40 && strNotes.Trim().Length <= 47)
                                        //    txtNotes5.Text = strNotes.Trim().Substring(0, 40);
                                        //else 
                                        //txtNotes5.Text = (strNotes.Trim().Length > 40 ? strNotes.Trim().Substring(0, strNotes.Length - 7) : strNotes.Trim());
                                    }

                                    dtpDOS5From.Value = oTransaction.Lines[j].DateServiceFrom;
                                    dtpDOS5To.Value = oTransaction.Lines[j].DateServiceTill;

                                    txtPOS5.Text = Convert.ToString(oTransaction.Lines[j].POSCode);
                                    txtPOS5.Tag = Convert.ToString(oTransaction.Lines[j].POSDescription);

                                    txtEMG5.Text = "";
                                    if (Convert.ToBoolean(oTransaction.Lines[j].EMG) == true)
                                    {
                                        if (_IsEMGAsX)
                                        {
                                            txtEMG5.Text = "X";
                                        }
                                        else
                                        {
                                            txtEMG5.Text = "Y";
                                        }
                                    }


                                   // txtCPT5.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
                                  //  txtCPT5.Tag = Convert.ToString(oTransaction.Lines[j].CPTDescription);

                                    //Mahesh Nawal 20100713 Check the CrossWalk CPT
                                    if (Convert.ToString(oTransaction.Lines[j].CPTCode).Trim() == Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode).Trim() || Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode).Trim() == "" || oTransaction.Lines[j].CrosswalkCPTCode == null)
                                    {
                                        txtCPT5.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
                                    }
                                    else
                                    {
                                        txtCPT5.Text = Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode);
                                    }


                                    //dtpPatientSignDate.Value = oTransaction.Lines[j].DateServiceFrom;
                                    //if (oTransaction.Lines[j].Dx1Code != "")
                                    //{
                                    //    string[] Dx1Code = Convert.ToString(oTransaction.Lines[j].Dx1Code).Split('.');
                                    //    if (Dx1Code.Length > 0)
                                    //    {
                                    //        if (Dx1Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode11.Text = Dx1Code[0];
                                    //            txtDiagnosisCode12.Text = Dx1Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode11.Text = Convert.ToString(oTransaction.Lines[j].Dx1Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode11.Tag = Convert.ToString(oTransaction.Lines[j].Dx1Description);
                                    //}

                                    //if (oTransaction.Lines[j].Dx2Code != "")
                                    //{
                                    //    string[] Dx2Code = Convert.ToString(oTransaction.Lines[j].Dx2Code).Split('.');
                                    //    if (Dx2Code.Length > 0)
                                    //    {
                                    //        if (Dx2Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode21.Text = Dx2Code[0];
                                    //            txtDiagnosisCode22.Text = Dx2Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode21.Text = Convert.ToString(oTransaction.Lines[j].Dx2Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode21.Tag = Convert.ToString(oTransaction.Lines[j].Dx2Description);
                                    //}

                                    //if (oTransaction.Lines[j].Dx3Code != "")
                                    //{
                                    //    string[] Dx3Code = Convert.ToString(oTransaction.Lines[j].Dx3Code).Split('.');
                                    //    if (Dx3Code.Length > 0)
                                    //    {
                                    //        if (Dx3Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode31.Text = Dx3Code[0];
                                    //            txtDiagnosisCode32.Text = Dx3Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode31.Text = Convert.ToString(oTransaction.Lines[j].Dx3Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode31.Tag = Convert.ToString(oTransaction.Lines[j].Dx3Description);
                                    //}

                                    //if (oTransaction.Lines[j].Dx4Code != "")
                                    //{
                                    //    string[] Dx4Code = Convert.ToString(oTransaction.Lines[j].Dx4Code).Split('.');
                                    //    if (Dx4Code.Length > 0)
                                    //    {
                                    //        if (Dx4Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode41.Text = Dx4Code[0];
                                    //            txtDiagnosisCode42.Text = Dx4Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode41.Text = Convert.ToString(oTransaction.Lines[j].Dx4Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode41.Tag = Convert.ToString(oTransaction.Lines[j].Dx4Description);
                                    //}

                                    txtMOD51.Text = Convert.ToString(oTransaction.Lines[j].Mod1Code);
                                    txtMOD51.Tag = Convert.ToString(oTransaction.Lines[j].Mod1Description);
                                    txtMOD52.Text = Convert.ToString(oTransaction.Lines[j].Mod2Code);
                                    txtMOD52.Tag = Convert.ToString(oTransaction.Lines[j].Mod2Description);
                                    txtMOD53.Text = Convert.ToString(oTransaction.Lines[j].Mod3Code);
                                    txtMOD53.Tag = Convert.ToString(oTransaction.Lines[j].Mod3Description);
                                    txtMOD54.Text = Convert.ToString(oTransaction.Lines[j].Mod4Code);
                                    txtMOD54.Tag = Convert.ToString(oTransaction.Lines[j].Mod4Description);

                                    // --------------------------------------
                                    // Code added by Pankaj Bedse on 30012010
                                    // For Dx Pointer Issue
                                    //StringBuilder sbDxPts = new StringBuilder();
                                    //foreach (string sDx in alDiagnosis)
                                    //{
                                    //    if ((oTransaction.Lines[j].Dx1Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())) || (oTransaction.Lines[j].Dx2Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())) || (oTransaction.Lines[j].Dx3Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())) || (oTransaction.Lines[j].Dx4Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())))
                                    //    {
                                    //        int iDxPtr = alDiagnosis.IndexOf(sDx) + 1;
                                    //        sbDxPts = sbDxPts.Append(iDxPtr + ",");
                                    //    }
                                    //}

                                    //// solving Problem# - 231 
                                    //// show the Dx Pointer in sequence as they are enter.
                                    txtDxPtr5.Text = GetDxPointers(alDiagnosis, j);

                                    // --------------------------------------
                                    // Old Code for Dxpointers
                                    // --------------------------------------
                                    //
                                    //if (oTransaction.Lines[j].Dx1Ptr)
                                    //{
                                    //    txtDxPtr5.Text = "1";
                                    //}
                                    //if (oTransaction.Lines[j].Dx2Ptr)
                                    //{
                                    //    txtDxPtr5.Text = txtDxPtr5.Text + "," + "2";
                                    //}
                                    //if (oTransaction.Lines[j].Dx3Ptr)
                                    //{
                                    //    txtDxPtr5.Text = txtDxPtr5.Text + "," + "3";
                                    //}
                                    //if (oTransaction.Lines[j].Dx4Ptr)
                                    //{
                                    //    txtDxPtr5.Text = txtDxPtr5.Text + "," + "4";
                                    //}
                                    // --------------------------------------

                                    txtUnits5.Text = Convert.ToDecimal(oTransaction.Lines[j].Unit).ToString("#############0.####");
                                    if (Convert.ToBoolean(_dtEpsdt.Rows[0]["bBillEPSDTorFamilyPlanning"]))
                                    {
                                        if ((oTransaction.Lines[j].IsFamilyPlanningIndicator))
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCode"].ToString() != "")
                                            {
                                                if (_dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCodeBox"].ToString() == "24h unshaded")
                                                    txtEPSDT5.Text = _dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCode"].ToString();
                                            }


                                        }
                                        if ((oTransaction.Lines[j].IsServiceScreening) && oTransaction.IsEPSDTReferral)
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                txtEPSDT5.Text = oTransaction.ReferralType;
                                            else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                                txtEMG5.Text = oTransaction.ReferralType;
                                            else
                                                txtEPSDTShaded5.Text = oTransaction.ReferralType;
                                        }
                                        else if ((oTransaction.Lines[j].IsServiceScreening) && !oTransaction.IsEPSDTReferral)
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                txtEPSDT5.Text = "NU";
                                            else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                            {
                                                txtEMG5.Text = "NU";
                                            }
                                            else
                                                txtEPSDTShaded5.Text = "NU";
                                        }
                                        if ((oTransaction.Lines[j].IsServiceScreening))
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString().ToString() != "")
                                            {
                                                if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                    txtEPSDT5.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                                else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                                    txtEMG5.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                                else
                                                    txtEPSDTShaded5.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                            }
                                        }
                                    }
                                    string[] Charges = Convert.ToString(oTransaction.Lines[j].Total).Split('.');
                                    if (Charges.Length > 0)
                                    {
                                        if (Charges.Length > 1)
                                        {
                                            txtCharges5.Text = Charges[0];
                                            txtCharges51.Text = Charges[1];
                                        }
                                        else
                                        {
                                            txtCharges5.Text = Charges[0];
                                        }
                                    }
                                    //FillProviderDetails(oTransaction.Lines[j].RefferingProviderId, ProviderType.RenderingProvider);
                                    cmbRenderingProvider5.SelectedValue = oTransaction.Lines[j].RefferingProviderId;
                                    //if (oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim() != "")
                                    //{
                                    //    txtRenderingProvider5_NPI.Text = FillRenderingProvider(Convert.ToInt64(oTransaction.Lines[j].RefferingProviderId), Convert.ToInt64(oTranDetails.HCFA_FacilityID), Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type"); 
                                    //    txtRenderingProvider5_NPI.Tag = Convert.ToString(oTransaction.Lines[j].RefferingProviderId);
                                    //}
                                    if (!Convert.ToBoolean(_dtEpsdt.Rows[0]["bSupressRenderEPSDTClaimOnPaperEDI"])||!oTransaction.bIsEPSDTScreening)
                                    {
                                        
                                            dtProviders = ogloBilling.GetMidLevelProviders(oTransaction.Lines[4].RefferingProviderId, oTransaction.ProviderID, oTransaction.ContactID, oTransaction.ClinicID);
                                            if (dtProviders != null && dtProviders.Rows.Count > 0)
                                            {
                                                _MidLevelRenderingProviderIDLineWise = Convert.ToInt64(dtProviders.Rows[0]["nORenderingProviderID"]);
                                            }

                                            DataTable _dtProvider = null;
                                            if (oTranDetails.HCFA_FacilityID == "")
                                            {
                                                _dtProvider = FillRenderingProvider(_MidLevelRenderingProviderIDLineWise, 0, Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type");
                                            }
                                            else
                                            {
                                                _dtProvider = FillRenderingProvider(_MidLevelRenderingProviderIDLineWise, Convert.ToInt64(oTranDetails.HCFA_FacilityID), Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type");
                                            }
                                            if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                                            {
                                                //if (Convert.ToString(_dtProvider.Rows[0]["QualifierDescription"]) != "" && !Convert.ToString(_dtProvider.Rows[0]["QualifierDescription"]).Contains("NPI"))
                                                if (Convert.ToString(_dtProvider.Rows[0]["QualifierMstID"]) != "1" && _dtProvider.Rows[0]["QualifierValue"] != null)
                                                {
                                                    if (Convert.ToString(_dtProvider.Rows[0]["QualifierValue"]) != "")
                                                    {
                                                        txtRenderingProvider5_QualifierValue.Text = Convert.ToString(_dtProvider.Rows[0]["QualifierValue"]);
                                                        txtRenderingProvider5_Qualifier.Text = Convert.ToString(_dtProvider.Rows[0]["Qualifier"]);
                                                    }
                                                    txtRenderingProvider5_NPI.Text = Convert.ToString(_dtProvider.Rows[0]["ProviderNPI"]);
                                                }
                                                else
                                                {
                                                    txtRenderingProvider5_NPI.Text = Convert.ToString(_dtProvider.Rows[0]["ProviderNPI"]);
                                                }
                                            }
                                        
                                    }
                                }//End of if(j==4)

                                #endregion

                                #region Transaction Line No 6

                                else if (j == 5)
                                {
                                    //if (IsNotesInLines == true)
                                    //{
                                    //    if (oTransaction.Lines[j].LineNotes.Count > 0)
                                    //    {
                                    //        txtNotes6.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription;
                                    //    }
                                    //}


                                    //..Append the Anesthesia data before the note
                                    if (oTransaction.Lines[j].Anesthesia != null && oTransaction.Lines[j].Anesthesia.Trim() != "")
                                    {
                                        txtNotes6.Text = oTransaction.Lines[j].Anesthesia + "  ";
                                    }

                                    //..Append the NDC code data before the note
                                    if (oTransaction.Lines[j].DisplayNDCCode_HCFA != null && oTransaction.Lines[j].DisplayNDCCode_HCFA.Trim() != "")
                                    {
                                        txtNotes6.Text += oTransaction.Lines[j].DisplayNDCCode_HCFA + "  ";
                                    }

                                    if (IsNotesInLines == true)
                                    {
                                        if (oTransaction.Lines[j].LineNotes.Count > 0)
                                        {
                                            if (txtNotes6.Text.Trim() != "")
                                            { txtNotes6.Text += oTransaction.Lines[j].LineNotes[0].NoteDescription + " "; }
                                            else
                                            { txtNotes6.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription+ " "; }
                                        }
                                    }

                                    if (oTransaction.Lines[j].NDCPrescriptionDesc != "")
                                    {
                                        if (txtNotes6.Text.Trim() != "")
                                        { txtNotes6.Text += oTransaction.Lines[j].NDCPrescriptionDesc.Replace("\r\n", ""); }
                                        else
                                        { txtNotes6.Text = oTransaction.Lines[j].NDCPrescriptionDesc.Replace("\r\n", ""); }

                                    }
                                    if (oTransaction.Lines[j].IsMammogramCertNumber == true)
                                    {
                                        if (txtNotes6.Text.Trim() != "")
                                        {
                                            txtNotes6.Text += oTransaction.HCFA_FacilityMammogramCertNumber.Replace("\r\n", "");
                                        }
                                        else
                                        {
                                            txtNotes6.Text = oTransaction.HCFA_FacilityMammogramCertNumber.Replace("\r\n", "");
                                        }
                                    }
                                    if (txtNotes6.Text.Trim() != "")
                                    {
                                        //if (txtNotes6.Text.Trim().Length > 40)
                                        //{
                                        //    txtNotes6.Text = txtNotes6.Text.Trim().Substring(0, 39);
                                        //}
                                        //else
                                        //{
                                        //    txtNotes6.Text = txtNotes6.Text.Trim();
                                        //}
                                        string strNotes = getStringFitinTextBox(txtNotes6, txtNotes6.Text.Trim());
                                        txtNotes6.Text = strNotes.Trim().Replace("\r\n", ""); 
                                        //if (strNotes.Trim().Length >= 40 && strNotes.Trim().Length <= 47)
                                        //    txtNotes6.Text = strNotes.Trim().Substring(0, 40);
                                        //else
                                        //    txtNotes6.Text = (strNotes.Trim().Length > 40 ? strNotes.Trim().Substring(0, strNotes.Length - 7) : strNotes.Trim());
                                    }

                                    dtpDOS6From.Value = oTransaction.Lines[j].DateServiceFrom;
                                    dtpDOS6To.Value = oTransaction.Lines[j].DateServiceTill;

                                    txtPOS6.Text = Convert.ToString(oTransaction.Lines[j].POSCode);
                                    txtPOS6.Tag = Convert.ToString(oTransaction.Lines[j].POSDescription);

                                    txtEMG6.Text = "";
                                    if (Convert.ToBoolean(oTransaction.Lines[j].EMG) == true)
                                    {
                                        if (_IsEMGAsX)
                                        {
                                            txtEMG6.Text = "X";
                                        }
                                        else
                                        {
                                            txtEMG6.Text = "Y";
                                        }
                                    }

                                    //txtCPT6.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
                                    //txtCPT6.Tag = Convert.ToString(oTransaction.Lines[j].CPTDescription);

                                    //Mahesh Nawal 20100713 Check the CrossWalk CPT
                                    if (Convert.ToString(oTransaction.Lines[j].CPTCode).Trim() == Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode).Trim() || Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode).Trim() == "" || oTransaction.Lines[j].CrosswalkCPTCode == null)
                                    {
                                        txtCPT6.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
                                    }
                                    else
                                    {
                                        txtCPT6.Text = Convert.ToString(oTransaction.Lines[j].CrosswalkCPTCode);
                                    }


                                    //dtpPatientSignDate.Value = oTransaction.Lines[j].DateServiceFrom;
                                    //if (oTransaction.Lines[j].Dx1Code != "")
                                    //{
                                    //    string[] Dx1Code = Convert.ToString(oTransaction.Lines[j].Dx1Code).Split('.');
                                    //    if (Dx1Code.Length > 0)
                                    //    {
                                    //        if (Dx1Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode11.Text = Dx1Code[0];
                                    //            txtDiagnosisCode12.Text = Dx1Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode11.Text = Convert.ToString(oTransaction.Lines[j].Dx1Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode11.Tag = Convert.ToString(oTransaction.Lines[j].Dx1Description);
                                    //}

                                    //if (oTransaction.Lines[j].Dx2Code != "")
                                    //{
                                    //    string[] Dx2Code = Convert.ToString(oTransaction.Lines[j].Dx2Code).Split('.');
                                    //    if (Dx2Code.Length > 0)
                                    //    {
                                    //        if (Dx2Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode21.Text = Dx2Code[0];
                                    //            txtDiagnosisCode22.Text = Dx2Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode21.Text = Convert.ToString(oTransaction.Lines[j].Dx2Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode21.Tag = Convert.ToString(oTransaction.Lines[j].Dx2Description);
                                    //}

                                    //if (oTransaction.Lines[j].Dx3Code != "")
                                    //{
                                    //    string[] Dx3Code = Convert.ToString(oTransaction.Lines[j].Dx3Code).Split('.');
                                    //    if (Dx3Code.Length > 0)
                                    //    {
                                    //        if (Dx3Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode31.Text = Dx3Code[0];
                                    //            txtDiagnosisCode32.Text = Dx3Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode31.Text = Convert.ToString(oTransaction.Lines[j].Dx3Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode31.Tag = Convert.ToString(oTransaction.Lines[j].Dx3Description);
                                    //}

                                    //if (oTransaction.Lines[j].Dx4Code != "")
                                    //{
                                    //    string[] Dx4Code = Convert.ToString(oTransaction.Lines[j].Dx4Code).Split('.');
                                    //    if (Dx4Code.Length > 0)
                                    //    {
                                    //        if (Dx4Code.Length > 1)
                                    //        {
                                    //            txtDiagnosisCode41.Text = Dx4Code[0];
                                    //            txtDiagnosisCode42.Text = Dx4Code[1];
                                    //        }
                                    //        else
                                    //        {
                                    //            txtDiagnosisCode41.Text = Convert.ToString(oTransaction.Lines[j].Dx4Code);
                                    //        }
                                    //    }
                                    //    txtDiagnosisCode41.Tag = Convert.ToString(oTransaction.Lines[j].Dx4Description);
                                    //}

                                    txtMOD61.Text = Convert.ToString(oTransaction.Lines[j].Mod1Code);
                                    txtMOD61.Tag = Convert.ToString(oTransaction.Lines[j].Mod1Description);
                                    txtMOD62.Text = Convert.ToString(oTransaction.Lines[j].Mod2Code);
                                    txtMOD62.Tag = Convert.ToString(oTransaction.Lines[j].Mod2Description);
                                    txtMOD63.Text = Convert.ToString(oTransaction.Lines[j].Mod3Code);
                                    txtMOD63.Tag = Convert.ToString(oTransaction.Lines[j].Mod3Description);
                                    txtMOD64.Text = Convert.ToString(oTransaction.Lines[j].Mod4Code);
                                    txtMOD64.Tag = Convert.ToString(oTransaction.Lines[j].Mod4Description);

                                    // --------------------------------------
                                    // Code added by Pankaj Bedse on 30012010
                                    // For Dx Pointer Issue
                                    //StringBuilder sbDxPts = new StringBuilder();
                                    //foreach (string sDx in alDiagnosis)
                                    //{
                                    //    if ((oTransaction.Lines[j].Dx1Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())) || (oTransaction.Lines[j].Dx2Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())) || (oTransaction.Lines[j].Dx3Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())) || (oTransaction.Lines[j].Dx4Code.Trim().ToUpper().Equals(sDx.Trim().ToUpper())))
                                    //    {
                                    //        int iDxPtr = alDiagnosis.IndexOf(sDx) + 1;
                                    //        sbDxPts = sbDxPts.Append(iDxPtr + ",");
                                    //    }
                                    //}

                                    //// solving Problem# - 231 
                                    //// show the Dx Pointer in sequence as they are enter.
                                    txtDxPtr6.Text = GetDxPointers(alDiagnosis, j);

                                    // --------------------------------------
                                    // Old Code for Dxpointers
                                    // --------------------------------------
                                    //
                                    //if (oTransaction.Lines[j].Dx1Ptr)
                                    //{
                                    //    txtDxPtr6.Text = "1";
                                    //}
                                    //if (oTransaction.Lines[j].Dx2Ptr)
                                    //{
                                    //    txtDxPtr6.Text = txtDxPtr6.Text + "," + "2";
                                    //}
                                    //if (oTransaction.Lines[j].Dx3Ptr)
                                    //{
                                    //    txtDxPtr6.Text = txtDxPtr6.Text + "," + "3";
                                    //}
                                    //if (oTransaction.Lines[j].Dx4Ptr)
                                    //{
                                    //    txtDxPtr6.Text = txtDxPtr6.Text + "," + "4";
                                    //}
                                    // --------------------------------------

                                    txtUnits6.Text = Convert.ToDecimal(oTransaction.Lines[j].Unit).ToString("#############0.####");
                                    if (Convert.ToBoolean(_dtEpsdt.Rows[0]["bBillEPSDTorFamilyPlanning"]))
                                    {
                                        if ((oTransaction.Lines[j].IsFamilyPlanningIndicator))
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCode"].ToString() != "")
                                            {
                                                if (_dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCodeBox"].ToString() == "24h unshaded")
                                                    txtEPSDT6.Text = _dtEpsdt.Rows[0]["sPaperClaimFamilyPlanningCode"].ToString();
                                            }


                                        }
                                        if ((oTransaction.Lines[j].IsServiceScreening) && oTransaction.IsEPSDTReferral)
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                txtEPSDT6.Text = oTransaction.ReferralType;
                                            else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                                txtEMG6.Text = oTransaction.ReferralType;
                                            else
                                                txtEPSDTShaded6.Text = oTransaction.ReferralType;
                                        }
                                        else if ((oTransaction.Lines[j].IsServiceScreening) && !oTransaction.IsEPSDTReferral)
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                txtEPSDT6.Text = "NU";
                                            else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                            {
                                                txtEMG6.Text = "NU";
                                            }
                                            else
                                                txtEPSDTShaded6.Text = "NU";
                                        }
                                        if ((oTransaction.Lines[j].IsServiceScreening))
                                        {
                                            if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString().ToString() != "")
                                            {
                                                if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24h unshaded")
                                                    txtEPSDT6.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                                else if (_dtEpsdt.Rows[0]["sPaperClaimEPSDTCodeBox"].ToString() == "24c unshaded")
                                                    txtEMG6.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                                else
                                                    txtEPSDTShaded6.Text = _dtEpsdt.Rows[0]["sPaperClaimEPSDTCode"].ToString();
                                            }
                                        }
                                    }
                                    string[] Charges = Convert.ToString(oTransaction.Lines[j].Total).Split('.');
                                    if (Charges.Length > 0)
                                    {
                                        if (Charges.Length > 1)
                                        {
                                            txtCharges6.Text = Charges[0];
                                            txtCharges61.Text = Charges[1];
                                        }
                                        else
                                        {
                                            txtCharges6.Text = Charges[0];
                                        }
                                    }
                                    //FillProviderDetails(oTransaction.Lines[j].RefferingProviderId, ProviderType.RenderingProvider);
                                    cmbRenderingProvider6.SelectedValue = oTransaction.Lines[j].RefferingProviderId;
                                    //if (oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim() != "")
                                    //{
                                    //    txtRenderingProvider6_NPI.Text = FillRenderingProvider(Convert.ToInt64(oTransaction.Lines[j].RefferingProviderId), Convert.ToInt64(oTranDetails.HCFA_FacilityID), Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type"); 
                                    //    txtRenderingProvider6_NPI.Tag = Convert.ToString(oTransaction.Lines[j].RefferingProviderId);
                                    //}
                                    if (!Convert.ToBoolean(_dtEpsdt.Rows[0]["bSupressRenderEPSDTClaimOnPaperEDI"])||!oTransaction.bIsEPSDTScreening)
                                    {
                                        
                                            dtProviders = ogloBilling.GetMidLevelProviders(oTransaction.Lines[5].RefferingProviderId, oTransaction.ProviderID, oTransaction.ContactID, oTransaction.ClinicID);
                                            if (dtProviders != null && dtProviders.Rows.Count > 0)
                                            {
                                                _MidLevelRenderingProviderIDLineWise = Convert.ToInt64(dtProviders.Rows[0]["nORenderingProviderID"]);

                                            }

                                            DataTable _dtProvider = null;
                                            if (oTranDetails.HCFA_FacilityID == "")
                                            {
                                                _dtProvider = FillRenderingProvider(_MidLevelRenderingProviderIDLineWise, 0, Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type");
                                            }
                                            else
                                            {
                                                _dtProvider = FillRenderingProvider(_MidLevelRenderingProviderIDLineWise, Convert.ToInt64(oTranDetails.HCFA_FacilityID), Convert.ToInt64(oTransaction.ContactID), "Paper Rendering Provider ID Type");
                                            }
                                            if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                                            {
                                                //if (Convert.ToString(_dtProvider.Rows[0]["QualifierDescription"]) != "" && !Convert.ToString(_dtProvider.Rows[0]["QualifierDescription"]).Contains("NPI"))
                                                if (Convert.ToString(_dtProvider.Rows[0]["QualifierMstID"]) != "1" && _dtProvider.Rows[0]["QualifierValue"] != null)
                                                {
                                                    if (Convert.ToString(_dtProvider.Rows[0]["QualifierValue"]) != "")
                                                    {
                                                        txtRenderingProvider6_QualifierValue.Text = Convert.ToString(_dtProvider.Rows[0]["QualifierValue"]);
                                                        txtRenderingProvider6_Qualifier.Text = Convert.ToString(_dtProvider.Rows[0]["Qualifier"]);
                                                    }
                                                    txtRenderingProvider6_NPI.Text = Convert.ToString(_dtProvider.Rows[0]["ProviderNPI"]);
                                                }
                                                else
                                                {
                                                    txtRenderingProvider6_NPI.Text = Convert.ToString(_dtProvider.Rows[0]["ProviderNPI"]);
                                                }
                                            }
                                        }
                                    
                                }//End of if(j==5)

                                #endregion


                                DateTime _MaxDate = DateTime.MinValue;
                                DateTime _MinDate = DateTime.MaxValue;
                                //MaheshB 20091201
                                for (int _Count = 0; _Count < oTransaction.Lines.Count; _Count++)
                                {
                                    if (_MaxDate.Date < oTransaction.Lines[_Count].DateServiceFrom.Date)
                                    {
                                        _MaxDate = oTransaction.Lines[_Count].DateServiceFrom;
                                    }
                                    if (_MinDate.Date > oTransaction.Lines[_Count].DateServiceFrom.Date)
                                    {
                                        _MinDate = oTransaction.Lines[_Count].DateServiceFrom;
                                    }
                                }
                                dtpPhysicianSignDate.Value = _MaxDate;
                                if (_IsSignatureOnFile == true)
                                {
                                    dtpPatientSignDate.Checked = true;
                                    dtpPatientSignDate.Value = _MinDate;
                                    dtpPatientSignDate.Enabled = true;
                                }
                             
                                #region Claim Total

                                //TotCharges += oTransaction.Lines[j].Total;

                                ////Sum = Sum + TotCharges;
                                //string[] TotalCharges = Convert.ToString(TotCharges).Split('.');
                                //if (TotalCharges.Length > 0)
                                //{
                                //    if (TotalCharges.Length > 1)
                                //    {
                                //        txtTotalCharges.Text = TotalCharges[0];
                                //        txtTotalCharges2.Text = TotalCharges[1];
                                //        //txtBalanceDue.Text = TotalCharges[0];
                                //        //txtBalanceDue2.Text = TotalCharges[1];
                                //    }
                                //    else
                                //    {
                                //        txtTotalCharges.Text = TotalCharges[0];
                                //        //txtBalanceDue.Text = TotalCharges[0];
                                //    }
                                //}
                                #endregion Claim Total

                                if (IsNotesInLines == false)
                                {
                                    if (oTransaction.Lines[j].LineNotes.Count > 0)
                                    {
                                        if (oTransaction.Lines[j].LineNotes[0].NoteDescription != null && oTransaction.Lines[j].LineNotes[0].NoteDescription.Trim() != "")
                                        {
                                            _sNotes += " " + oTransaction.Lines[j].LineNotes[0].NoteDescription.Trim();
                                        }
                                    }
                                }

                            }

                            //gloPM6010
                            #region Claim Total

                            for (int j = 0; j < oTransaction.Lines.Count; j++)
                            {
                                TotCharges += oTransaction.Lines[j].Total;

                                
                                string[] TotalCharges = Convert.ToString(TotCharges).Split('.');
                                if (TotalCharges.Length > 0)
                                {
                                    if (TotalCharges.Length > 1)
                                    {
                                        txtTotalCharges.Text = TotalCharges[0];
                                        txtTotalCharges2.Text = TotalCharges[1];
                                    }
                                    else
                                    {
                                        txtTotalCharges.Text = TotalCharges[0];
                                    }
                                }
                            }
                            #endregion Claim Total

                        }
                     

                        if (IsNotesInLines == false)
                        {
                            if (((oTransaction.IsReplacementClaim == true) || (oTransaction.IsRebill)) && (oTransaction.IsBox19CorrectRplmnt))
                            {
                                if (oTransaction.sBox19Notes.Trim() != "")
                                {
                                    if (_sNotes != string.Empty && _sNotes != null)
                                    {
                                        //_sNotes.Insert(0, "Replacement Claim; " + oTransaction.sBox19Notes.Trim().Replace("\n"," ").Replace("\r"," ") + " ");
                                        _sNotes = "Replacement Claim; " + oTransaction.sBox19Notes.Trim().Replace("\n", " ").Replace("\r", " ") + "; " + _sNotes;
                                    }
                                    else
                                    {
                                        _sNotes = "Replacement Claim; " + oTransaction.sBox19Notes.Trim().Replace("\n", " ").Replace("\r", " ");
                                    }
                                }
                                else if (_sNotes != string.Empty && _sNotes != null)
                                { //_sNotes.Insert(0, "Replacement Claim;");
                                    _sNotes = "Replacement Claim;" + _sNotes;
                                }
                                else
                                {
                                    _sNotes = "Replacement Claim";
                                }
                            }
                            else
                            {
                                if (oTransaction.sBox19Notes.Trim() != "")
                                {
                                    if (_sNotes != string.Empty && _sNotes != null)
                                    {
                                        //_sNotes.Insert(0, "Replacement Claim; " + oTransaction.sBox19Notes.Trim().Replace("\n"," ").Replace("\r"," ") + " ");
                                        _sNotes = oTransaction.sBox19Notes.Trim().Replace("\n", " ").Replace("\r", " ") + "; " + _sNotes;
                                    }
                                    else
                                    {
                                        _sNotes =oTransaction.sBox19Notes.Trim().Replace("\n", " ").Replace("\r", " ");
                                    }
                                }   
                            }
                        }
                        else
                        {
                            if (((oTransaction.IsReplacementClaim == true) || (oTransaction.IsRebill)) && (oTransaction.IsBox19CorrectRplmnt))
                            {
                                if (oTransaction.sBox19Notes.Trim() != "")
                                { //_sNotes.Insert(0, "Replacement Claim; " + oTransaction.sBox19Notes.Trim().Replace("\n"," ").Replace("\r"," ") + " ");
                                    _sNotes = "Replacement Claim; " + oTransaction.sBox19Notes.Trim().Replace("\n", " ").Replace("\r", " ");
                                }
                                else
                                { //_sNotes.Insert(0, "Replacement Claim;");
                                    _sNotes = "Replacement Claim";
                                }
                            }
                            else
                            {
                                if (oTransaction.sBox19Notes.Trim() != "")
                                { //_sNotes.Insert(0, "Replacement Claim; " + oTransaction.sBox19Notes.Trim().Replace("\n"," ").Replace("\r"," ") + " ");
                                    _sNotes = oTransaction.sBox19Notes.Trim().Replace("\n", " ").Replace("\r", " ");
                                }
                            }
                        }
                       
                        string _sNote = "";

                        //if (oTransaction.Transaction_Details.HCFA_LastSeenDate != "0")
                        //    _sNote = "LastSeenDate " + oTransaction.Transaction_Details.HCFA_LastSeenDate.Trim();

                        //if (oTransaction.Transaction_Details.HCFA_ReferringProviderNPI.Trim() != "" && oTransaction.Transaction_Details.HCFA_bIsRefProvAsSupervisor == true)
                        //    _sNote = _sNote + " NPI " + oTransaction.Transaction_Details.HCFA_ReferringProviderNPI.Trim() + "; ";
                        //else 
                            
                            if (_sNote.Trim() != "")    
                            _sNote = _sNote + "; ";
                        if (_sNote.Trim() != "")                            
                        _sNotes = _sNote + _sNotes;
                        
                            if (_sNotes.Trim() != "")
                            {
                                
                                string strNotes = getStringFitinTextBox(txt19ReservedForLocalUse, _sNotes.Trim());
                                txt19ReservedForLocalUse.Text = strNotes.Trim().Replace("\r\n", "");
                            }
                                              

                        #region Workers Comp //MaheshB 20091117

                        if (oTransaction.WorkersCompNo != "")
                        {
                            
                            if(_bShowClaimNo ==true)
                            {
                                txtInsuredIdNumber.Text = oTransaction.WorkersCompNo;
                                _sInsuredIdNumber = oTransaction.WorkersCompNo;
                            }
                          
                        }
                        #endregion

                        #region Claim Remittance Referance #

                        if ( _ClaimRemittanceRefNo != "")
                        {
                            txtOriginalRefNumber.Text = _ClaimRemittanceRefNo.Trim();
                        }
                            
                        #endregion  

                        //oTransaction.

                        #region Paid Amount.

                        Int64 _trnID = 0;
                        //_trnID = oTransaction.Lines[0].TransactionId;
                        _trnID = oTransaction.TransactionID;

                        #region Commented Code

                        //string _NewQuery = "SELECT     ISNULL(SUM(BL_EOBPayment_DTL.nAmount), 0) AS TotalPaid "+
                        //                   " FROM         BL_EOBPayment_DTL INNER JOIN BL_Transaction_Lines "+
                        //                   " ON BL_EOBPayment_DTL.nBillingTransactionDetailID = BL_Transaction_Lines.nTransactionDetailID "+
                        //                   " INNER JOIN BL_Transaction_Claim_Lines ON "+
                        //                   " BL_Transaction_Lines.nTransactionID = BL_Transaction_Claim_Lines.nTransactionMasterID AND "+ 
                        //                   " BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_Claim_Lines.nTransactionMasterDetailID "+
                        //                   " WHERE     (BL_EOBPayment_DTL.nPaymentType = 6) AND (BL_EOBPayment_DTL.nPaymentSubType = 8) AND (BL_EOBPayment_DTL.nPaySign = 2) AND "+
                        //                   " (BL_Transaction_Lines.nClinicID = 1) OR "+
                        //                   " (BL_EOBPayment_DTL.nPaymentType = 6) AND (BL_EOBPayment_DTL.nPaymentSubType = 13) AND (BL_EOBPayment_DTL.nPaySign = 1) AND "+
                        //                   " (BL_Transaction_Lines.nClinicID = 1) OR "+
                        //                   " (BL_EOBPayment_DTL.nPaymentType = 4) AND (BL_EOBPayment_DTL.nPaymentSubType = 1) AND (BL_EOBPayment_DTL.nPaySign = 2) AND "+
                        //                  " (BL_Transaction_Lines.nClinicID = 1) OR "+
                        //                  " (BL_EOBPayment_DTL.nPaymentType = 4) AND (BL_EOBPayment_DTL.nPaymentSubType = 13) AND (BL_EOBPayment_DTL.nPaySign = 1) AND "+
                        //                  " (BL_Transaction_Lines.nClinicID = 1) "+
                        //                  " GROUP BY BL_Transaction_Claim_Lines.nTransactionID "+
                        //                 " HAVING      (BL_Transaction_Claim_Lines.nTransactionID = "+_trnID+")";

                        //Commented in 6010
                        //string _NewQuery = " SELECT     " +
                        //" ISNULL(SUM(BL_EOBPayment_DTL.nAmount), 0) AS TotalPaid   " +
                        //" FROM          " +
                        //"  BL_EOBPayment_DTL INNER JOIN BL_Transaction_Lines   " +
                        //"  ON BL_EOBPayment_DTL.nBillingTransactionDetailID = BL_Transaction_Lines.nTransactionDetailID   " +
                        //"  INNER JOIN BL_Transaction_Claim_Lines ON   " +
                        //"  BL_Transaction_Lines.nTransactionID = BL_Transaction_Claim_Lines.nTransactionMasterID  " +
                        //"  AND  BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_Claim_Lines.nTransactionMasterDetailID   " +
                        //"  WHERE      " +
                        //"  ((BL_EOBPayment_DTL.nPaymentType = 6) AND (BL_EOBPayment_DTL.nPaymentSubType = 8) AND (BL_EOBPayment_DTL.nPaySign = 2) AND  (BL_Transaction_Lines.nClinicID = " + _ClinicID + "))  " +
                        //"  OR   " +
                        //"  ((BL_EOBPayment_DTL.nPaymentType = 6) AND (BL_EOBPayment_DTL.nPaymentSubType = 13) AND (BL_EOBPayment_DTL.nPaySign = 1) AND  (BL_Transaction_Lines.nClinicID = " + _ClinicID + ")) " +
                        //"  OR   " +
                        //" ((BL_EOBPayment_DTL.nPaymentType = 4) AND (BL_EOBPayment_DTL.nPaymentSubType = 1) AND (BL_EOBPayment_DTL.nPaySign = 2) AND  (BL_Transaction_Lines.nClinicID = " + _ClinicID + ") " +
                        //" AND  ((ISNULL(bIsPaymentVoid,0) = 0) AND (ISNULL(nVoidType,0) NOT IN (3,5))))  " +
                        //" OR   " +
                        //" ((BL_EOBPayment_DTL.nPaymentType = 4) AND (BL_EOBPayment_DTL.nPaymentSubType = 13) AND (BL_EOBPayment_DTL.nPaySign = 1) AND  (BL_Transaction_Lines.nClinicID = " + _ClinicID + ") " +
                        //"  AND  ((ISNULL(bIsPaymentVoid,0) = 0) AND (ISNULL(nVoidType,0) NOT IN (3,5))))   " +
                        //"  GROUP BY BL_Transaction_Claim_Lines.nTransactionID   " +
                        //"  HAVING      (BL_Transaction_Claim_Lines.nTransactionID = " + _trnID + ") ";

                        //oDB.Connect(false);
                        //DataTable dtTotalPaid = new DataTable();
                        //oDB.Retrive_Query(_NewQuery, out dtTotalPaid);
                        //string _AmountPaid = String.Empty;
                        //Decimal _AmountPaid1 = 0;
                        //if (dtTotalPaid != null && dtTotalPaid.Rows.Count > 0)
                        //{
                        //    _AmountPaid = Convert.ToString(dtTotalPaid.Rows[0]["TotalPaid"]);
                        //    _AmountPaid1 = Convert.ToDecimal(dtTotalPaid.Rows[0]["TotalPaid"]);
                        //}

                        #endregion

                        string _AmountPaid = String.Empty;
                        _AmountPaid = GetBox29Setting(29,_trnID);
                         
                        string[] TotalPaid = Convert.ToString(_AmountPaid).Split('.');
                        if (TotalPaid.Length > 0)
                        {
                            if (TotalPaid.Length > 1)
                            {
                                txtAmountPaid.Text = TotalPaid[0];
                                txtAmountPaid2.Text = TotalPaid[1];
                            }
                            else
                            {
                                txtAmountPaid.Text = TotalPaid[0];



                            }
                        }
                           
                        #endregion Paid Amount

                        #region Balance

                        Decimal _balance = 0;
                        string _strbalance = String.Empty;

                        ////...Code commented on 20100908 by Sagar Ghodke 
                        ////...Code commented for 5051 urgent outage from Mellisa for consideration of adjustment in due calculation
                        ////...Below commented code is original code

                        //if (TotCharges != 0)
                        //{
                        //    _balance = TotCharges - _AmountPaid1;
                        //    _strbalance = _balance.ToString();
                        //}
                        //string[] BalanceDue = Convert.ToString(_strbalance).Split('.');
                        //if (BalanceDue.Length > 0)
                        //{
                        //    if (BalanceDue.Length > 1)
                        //    {
                        //        txtBalanceDue.Text = BalanceDue[0];
                        //        txtBalanceDue2.Text = BalanceDue[1];
                        //    }
                        //    else
                        //    {
                        //        txtBalanceDue.Text = BalanceDue[0];
                        //    }
                        //}


                        try
                        {
                            string _NewQuery = "";
                            _NewQuery = "SELECT ISNULL(dbo.GET_HCFA1500_BalanceDue(" + _trnID + "),0) AS BalanceDue";
                            Object _balanceDue = null;
                            if (oDB == null)
                                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            oDB.Connect(false);
                            _balanceDue = oDB.ExecuteScalar_Query(_NewQuery);
                            oDB.Disconnect();

                            if (_balanceDue != null && Convert.ToString(_balanceDue).Trim() != "")
                            {
                                _strbalance = Convert.ToString(_balanceDue);
                            }
                            else
                            {
                                _strbalance = "0";
                            }
                        }
                        catch //(Exception ex)
                        {
                        }
                        finally
                        {
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                            }
                        }


                        string[] BalanceDue = Convert.ToString(_strbalance).Split('.');
                       // if (BalanceDue.Length > 0)
                       // {
                            switch (GetPaperBillingSettings(gloSettings.PaperBillingBoxtype.Box30))
                            {
                                // Balance  
                               case 1:
                                    if (BalanceDue.Length > 0)
                                    {
                                        //if (BalanceDue.Length > 1)
                                        //{
                                        //    txtBalanceDue.Text = BalanceDue[0];
                                        //    txtBalanceDue2.Text = BalanceDue[1];
                                        //}
                                        //else
                                        //{
                                        //    txtBalanceDue.Text = BalanceDue[0];
                                        //}
                                    }
                                break;

                               //Box 28 Minus Box 29
                               case 2:
                                if (_AmountPaid != "")
                                    _balance = TotCharges - Convert.ToDecimal(_AmountPaid);
                                else
                                    _balance = TotCharges;
                                BalanceDue = Convert.ToString(_balance).Split('.');
                                if (BalanceDue.Length > 0)
                                {
                                    //if (BalanceDue.Length > 1)
                                    //{
                                    //    txtBalanceDue.Text = BalanceDue[0];
                                    //    txtBalanceDue2.Text = BalanceDue[1];
                                    //}
                                    //else
                                    //{
                                    //    txtBalanceDue.Text = BalanceDue[0];
                                    //}
                                }
                                break;

                               //Original Charge Amount  
                               case 3:
                                _balance = TotCharges;
                                BalanceDue = Convert.ToString(_balance).Split('.');
                                if (BalanceDue.Length > 0)
                                {
                                    //if (BalanceDue.Length > 1)
                                    //{
                                    //    txtBalanceDue.Text = BalanceDue[0];
                                    //    txtBalanceDue2.Text = BalanceDue[1];
                                    //}
                                    //else
                                    //{
                                    //    txtBalanceDue.Text = BalanceDue[0];
                                    //}
                                }
                                break;

                                //Blank
                               case 4:
                                       //txtBalanceDue.Text = "";
                                       //txtBalanceDue2.Text = "";
                                        break;
                               //0.00
                               case 5:
                                        //txtBalanceDue.Text = "0";
                                        //txtBalanceDue2.Text = "00";
                                        break; 
                     
                            
                        }

                        #endregion

                        #region "MidlevelRendering Provider"

                        //if (oTransaction.Lines[0].RefferingProviderId != MidLevelRenderingProviderID)
                        //{
                            //FillMidlevelRenderingProvider(MidLevelRenderingProviderID);
                        //}

                        #endregion

                        #region "MedicaidResubmissionCode"
                            if (oTransaction.MedicaidResubmissionCode != null || oTransaction.MedicaidResubmissionCode != "")
                                txtMedicaidResubmissionCode.Text = oTransaction.MedicaidResubmissionCode;
                        #endregion
                    }

                    #region "Medicaid CMS1500 rules"

                    if (!_IsPaperDisplayMailingAddress)
                    {
                        txtPayerNameAndAddress.Text = "";
                    }

                    if (_IsSwap1a9a1aMCare)
                    {
                        _sInsuredIdNumber = txtInsuredIdNumber.Text;
                        _sOtherInsuredPolicyNo = txtOtherInsuredPolicyNo.Text;

                        txtOtherInsuredPolicyNo.Text = _sInsuredIdNumber;
                        txtInsuredIdNumber.Text = _sOtherInsuredPolicyNo;

                        if (_OtherInsuranceType != "MA" && _OtherInsuranceType != "MB")
                        {
                            txtInsuredIdNumber.Text = "";
                        }
                    }

                    #endregion "Medicaid CMS1500 rules"

                    
                    FillReadOnlyPanelForm(oTransaction.Lines.Count);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                btnPatientBrowse.Hide();
                btnBrowsePatientRegt.Hide();
                btn_BrowseReferral.Hide();
                btn_BrowseFacility.Hide();
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }
        }
              
        private string GetDxPointers(ArrayList alDiagnosis, int lineNumber)
        {
            StringBuilder sbDxPts = new StringBuilder();

            if ((alDiagnosis.Contains(oTransaction.Lines[lineNumber].Dx1Code.Trim().ToUpper())))
            {
                int iDxPtr = alDiagnosis.IndexOf(oTransaction.Lines[lineNumber].Dx1Code.Trim().ToUpper()) + 1;
                sbDxPts = sbDxPts.Append(Convert.ToChar(iDxPtr + 64));
            }
            if ((alDiagnosis.Contains(oTransaction.Lines[lineNumber].Dx2Code.Trim().ToUpper())))
            {
                int iDxPtr = alDiagnosis.IndexOf(oTransaction.Lines[lineNumber].Dx2Code.Trim().ToUpper()) + 1;
                sbDxPts = sbDxPts.Append("," + Convert.ToChar(iDxPtr + 64));
            }
            if ((alDiagnosis.Contains(oTransaction.Lines[lineNumber].Dx3Code.Trim().ToUpper())))
            {
                int iDxPtr = alDiagnosis.IndexOf(oTransaction.Lines[lineNumber].Dx3Code.Trim().ToUpper()) + 1;
                sbDxPts = sbDxPts.Append("," + Convert.ToChar(iDxPtr + 64));
            }
            if ((alDiagnosis.Contains(oTransaction.Lines[lineNumber].Dx4Code.Trim().ToUpper())))
            {
                int iDxPtr = alDiagnosis.IndexOf(oTransaction.Lines[lineNumber].Dx4Code.Trim().ToUpper()) + 1;
                sbDxPts = sbDxPts.Append("," + Convert.ToChar(iDxPtr + 64));
            }
            return Convert.ToString(sbDxPts);
        }
       
        private string getStringFitinTextBox(TextBox txt, string str)
        {
            try
            {
                string strText = str.Trim();
                Graphics g = txt.CreateGraphics();
                //int width = (int)(g.MeasureString(strText.Trim(), txt.Font).Width);
                int width = (int)(MeasureDisplayStringWidth(g, strText, txt.Font));
                //if (width > txt.Width - txt.Margin.Horizontal - 20)
                //{
                //    while ((int)(MeasureDisplayStringWidth(g, strText.Trim(), txt.Font)) >= (txt.Width - txt.Margin.Horizontal - 20))
                //    {
                //        strText = strText.Remove(strText.Trim().Length - 1).Trim();
                //    }
                //}

                //g.Dispose();
                 if (strText.Length >= 1)
                 {

                    if (width > txt.Width - txt.Margin.Horizontal - 20)
                    {

                        
                        while ((int)(MeasureDisplayStringWidth(g, strText, txt.Font)) >= (txt.Width - txt.Margin.Horizontal - 20))
                        {

                            strText = strText.Remove(strText.Length - 1).Trim();
                            if (strText.Length <= 1)
                            {
                                break;
                            }
                        }
                    }
                }
                
                g.Dispose();
                return strText;
            }
          
                catch //(Exception ex)
            {
                return str;
            }
        }
        private static Font shortFont = null;
        private static Font longFont = null;
        private string AdjustStringFont(Label Control)
        {
            try
            {
                string strText = Control.Text.Trim();
                if (strText != string.Empty)
                {
                    Graphics g = Control.CreateGraphics();

                    //int width = (int)(MeasureDisplayStringWidth(g, strText.Trim(), Control.Font));
                    //int height = (int)(MeasureDisplayStringHeight(g, strText.Trim(), Control.Font));
                    //if (width < Control.Width - Control.Margin.Horizontal)
                    //{
                    //while ((int)(MeasureDisplayStringWidth_New(g, strText.Trim(), Control.Font)) < (Control.Width - Control.Margin.Horizontal) && (int)(MeasureDisplayStringHeight(g, strText.Trim(), Control.Font)) < (Control.Height - Control.Margin.Vertical))
                    //{
                    //    Control.Font = new Font("Calibri", Control.Font.Size + 0.1F, FontStyle.Bold);
                    //}
                    //}
                    if (strText.Length == 2)
                    {
                        if (longFont == null)
                        {
                            longFont = new Font("Calibri", 8.75F, FontStyle.Bold);
                        }
                        Control.Font = longFont;
                    }
                    else if(strText.Length == 3)
                    {
                        if (shortFont == null)
                        {
                            shortFont = new Font("Calibri", 7F, FontStyle.Bold);
                        }
                        Control.Font = shortFont;
                    }

                    g.Dispose();
                }
                return strText;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        static public int MeasureDisplayStringWidth(Graphics graphics, string text, Font font)
        {
            System.Drawing.StringFormat format = new System.Drawing.StringFormat();
            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(0,0,1000,1000);
            System.Drawing.CharacterRange[] ranges = 
                                       { new System.Drawing.CharacterRange(0, 
                                                               text.Length) };
          //  System.Drawing.Region[] regions = new System.Drawing.Region[1];

            format.SetMeasurableCharacterRanges(ranges);

            System.Drawing.Region[] regions = graphics.MeasureCharacterRanges(text.Trim(), font, rect, format);
            if (regions.Length > 0)
            {
                rect = regions[0].GetBounds(graphics);
                try
                {
                    regions[0].Dispose();
                }
                catch
                {
                }
            }
            format.Dispose();
            format = null;

            return (int)(rect.Right + 1.0f);
        }

        static public int MeasureDisplayStringWidth_New(Graphics graphics, string text, Font font)
        {
            System.Drawing.StringFormat format = new System.Drawing.StringFormat();
            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(0, 0, 1000, 1000);
            System.Drawing.CharacterRange[] ranges = 
                                       { new System.Drawing.CharacterRange(0, 
                                                               text.Length) };
        //    System.Drawing.Region[] regions = new System.Drawing.Region[1];

            format.SetMeasurableCharacterRanges(ranges);

            System.Drawing.Region[] regions = graphics.MeasureCharacterRanges(text.Trim(), font, rect, format);
            if (regions.Length > 0)
            {
                rect = regions[0].GetBounds(graphics);
                try
                {
                    regions[0].Dispose();
                }
                catch
                {
                }
            }
            format.Dispose();
            format = null;

            return (int)(rect.Right + 6.0f);

        }

        static public int MeasureDisplayStringHeight(Graphics graphics, string text, Font font)
        {
            System.Drawing.StringFormat format = new System.Drawing.StringFormat();
            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(0, 0, 1000, 1000);
            System.Drawing.CharacterRange[] ranges = 
                                       { new System.Drawing.CharacterRange(0, 
                                                               text.Length) };
           // System.Drawing.Region[] regions = new System.Drawing.Region[1];

            format.SetMeasurableCharacterRanges(ranges);

            System.Drawing.Region[] regions = graphics.MeasureCharacterRanges(text.Trim(), font, rect, format);
            if (regions.Length > 0)
            {
                rect = regions[0].GetBounds(graphics);
                try
                {
                    regions[0].Dispose();
                }
                catch
                {
                }
            }
            format.Dispose();
            format = null;

            return (int)(rect.Bottom);

        }
        private DataTable GetEpsdtAndFamilyplanningCode(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlquery = String.Empty;
            DataTable EpsdtAndFamilyplanningCode = null;          
            try
            {
                oDB.Connect(false);
                _sqlquery = "SELECT ISNULL(bBillEPSDTorFamilyPlanning,0) AS bBillEPSDTorFamilyPlanning, ISNULL(sPaperClaimEPSDTCode,'') AS sPaperClaimEPSDTCode ,ISNULL(sPaperClaimEPSDTCodeBox,'') AS sPaperClaimEPSDTCodeBox,ISNULL(sPaperClaimFamilyPlanningCode,'') AS sPaperClaimFamilyPlanningCode, ISNULL(sPaperClaimFamilyPlanningCodeBox,'') AS  sPaperClaimFamilyPlanningCodeBox, ISNULL(bSupressRenderEPSDTClaimOnPaperEDI,0) AS bSupressRenderEPSDTClaimOnPaperEDI FROM dbo.Contacts_Insurance_DTL WHERE nContactID=" + ContactID;
                oDB.Retrive_Query(_sqlquery, out EpsdtAndFamilyplanningCode);

            }
            catch (Exception ex)
            {
                EpsdtAndFamilyplanningCode.Dispose();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }
            }
            return EpsdtAndFamilyplanningCode;
        }

        private void FillAllDetails(Transaction _Transaction)
        {
            try
            {
                if (_Transaction != null)
                {
                    if (Convert.ToInt64(_Transaction.ProviderID) != 0 && _Transaction.ProviderID.ToString() != "")
                    {
                        if (_Transaction.ClaimInsurance != null && _Transaction.ClaimInsurance.Rows.Count > 0)
                        {
                            FillCMSInsurancesNew(_Transaction.ClaimInsurance); 
                        }
                       
                        FillSubmitterInfo(Convert.ToInt64(_Transaction.ClinicID), Convert.ToInt64(_Transaction.ProviderID));
                        _PatientID = _Transaction.PatientID;
                    }

                    if (Convert.ToInt64(_Transaction.PatientID) != 0)
                    {
                        FillOtherDetails(oTransaction);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }
        private void FillOtherDetails(Transaction oTransaction)
        {
           
            String _result = "";
             DataTable dt = null;
            try
            {
               
               
                if (oTransaction.PatientSettings != null && oTransaction.PatientSettings.Rows.Count > 0)
                {
                    dt = oTransaction.PatientSettings.Copy(); 
                }
              
                _IsSignatureOnFile = oTransaction.Transaction_Details.HCFA_bIsSignatureOnFile;
                
                foreach (DataRow _dr in dt.Rows)
                {
                    if (_dr.ItemArray[1].ToString() == "Signature on File Date")
                    {
                        _result = _dr.ItemArray[0].ToString();
                    }
                    if (_dr.ItemArray[1].ToString() == "CMS1500 Box13")
                    {
                        _InsuredPersonSign = _dr.ItemArray[0].ToString();
                    }
                }

                //Get Signature on File Date
                if (oTransaction.Transaction_Details.HCFA_bIsSignatureOnFile == true)
                {
                    if (_result != "")
                    {
                        if (Convert.ToInt64(_result) > 0)
                            _dtSignatureOnFileDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_result));
                    }
                    else
                    {
                        _dtSignatureOnFileDate = DateTime.Now;
                    }
                }
                else
                {
                    _dtSignatureOnFileDate = DateTime.Now;
                }


              
                if (_InsuredPersonSign.ToUpper() == "PAY TO PROVIDER")
                {
                    _InsuredPersonSign = "SIGNATURE ON FILE";
                }
                else if (_IsSignatureOnFile == false)
                {
                    _InsuredPersonSign = "SIGNATURE NOT ON  FILE";
                    dtpPatientSignDate.Checked = false;
                    dtpPatientSignDate.Enabled = false;
                }
                dt = null;

                if (oTransaction.InsurancesBoxSettings != null)
                {
                    dt = oTransaction.InsurancesBoxSettings;
                }


                //switch (dt.Rows[0]["nBox11bSettingID"].ToString())
                //{
                //    case "1":
                //        { _EmployerName = ""; }
                //        break;
                //    case "2": { _EmployerName = Convert.ToString(dt.Rows[0]["sEmployer"]); }//_ds.Tables["ClaimPatient"].Rows[0]["sEmployerName"]); }

                //        break;
                //    case "3": { _EmployerName = Convert.ToString(oTransaction.WorkersCompNo); } //Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["sWorkersCompNo"]); }

                //        break;
                //    case "4": { _EmployerName = Convert.ToString(dt.Rows[0]["sSubscriberID"]); }//Convert.ToString(_ds.Tables["ClaimInsurance"].Rows[0]["sSubscriberID"]); }

                //        break;
                //    case "5": { _EmployerName = Convert.ToString(dt.Rows[0]["sGroup"]); }

                //        break;
                //}


                if (oTransaction.ClaimInsurance != null && oTransaction.ClaimInsurance.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(oTransaction.ClaimInsurance.Rows[0]["bAssignmentofBenifit"]) == true)
                    {
                        txtInsuredPersonSign.Text = "SIGNATURE ON FILE";
                        dtpPatientSignDate.Checked = true;
                        dtpPatientSignDate.Enabled = true;
                    }
                    else
                    {
                        txtInsuredPersonSign.Text = "PAY TO PATIENT";
                        dtpPatientSignDate.Checked = false;
                        dtpPatientSignDate.Enabled = false;
                    }
                }
              

                                if (dt != null && dt.Rows.Count > 0)
                {
                    //BOX 32
                    if (Convert.ToString(dt.Rows[0]["sBox32"]) == "Provider Address")
                        _FacilityAddressType = AddressType.ProviderAddress;
                    else if (Convert.ToString(dt.Rows[0]["sBox32"]) == "Facility Address" || Convert.ToString(dt.Rows[0]["sBox32"]) == "")
                        _FacilityAddressType = AddressType.FacilityAddress;
                    else if (Convert.ToString(dt.Rows[0]["sBox32"]) == "Clinic Address")
                        _FacilityAddressType = AddressType.ClinicAddress;
                    //BOX 32A
                    if (Convert.ToString(dt.Rows[0]["sBox32A"]) == "Billing Provider NPI")
                        _Facility_A_NPI = NPIType.BillingProviderNPI;
                    else if (Convert.ToString(dt.Rows[0]["sBox32A"]) == "Facility NPI" || Convert.ToString(dt.Rows[0]["sBox32A"]) == "")
                        _Facility_A_NPI = NPIType.FacilityNPI;
                    else if (Convert.ToString(dt.Rows[0]["sBox32A"]) == "Clinic NPI")
                        _Facility_A_NPI = NPIType.ClinicNPI;
                    //BOX 32B
                    //if (Convert.ToString(dt.Rows[0]["sBox32B"]) == "Billing Provider NPI")
                    //    _Facility_B_NPI = NPIType.BillingProviderNPI;
                    //else if (Convert.ToString(dt.Rows[0]["sBox32B"]) == "Facility NPI" || Convert.ToString(dt.Rows[0]["sBox32B"]) == "")
                    //    _Facility_B_NPI = NPIType.FacilityNPI;
                    //else if (Convert.ToString(dt.Rows[0]["sBox32B"]) == "Clinic NPI")
                    //    _Facility_B_NPI = NPIType.ClinicNPI;


                    //BOX 33
                    if (Convert.ToString(dt.Rows[0]["sBox33"]) == "Provider Address" || Convert.ToString(dt.Rows[0]["sBox33"]) == "")
                        _BillingAddressType = AddressType.ProviderAddress;
                    else if (Convert.ToString(dt.Rows[0]["sBox33"]) == "Facility Address")
                        _BillingAddressType = AddressType.FacilityAddress;
                    else if (Convert.ToString(dt.Rows[0]["sBox33"]) == "Clinic Address")
                        _BillingAddressType = AddressType.ClinicAddress;
                    //BOX 33A
                    if (Convert.ToString(dt.Rows[0]["sBox33A"]) == "Billing Provider NPI" || Convert.ToString(dt.Rows[0]["sBox33A"]) == "")
                        _Billing_A_NPI = NPIType.BillingProviderNPI;
                    else if (Convert.ToString(dt.Rows[0]["sBox33A"]) == "Facility NPI")
                        _Billing_A_NPI = NPIType.FacilityNPI;
                    else if (Convert.ToString(dt.Rows[0]["sBox33A"]) == "Clinic NPI")
                        _Billing_A_NPI = NPIType.ClinicNPI;
                    //BOX 33B
                    //if (Convert.ToString(dt.Rows[0]["sBox33B"]) == "Billing Provider NPI" || Convert.ToString(dt.Rows[0]["sBox33B"]) == "")
                    //    _Billing_B_NPI = NPIType.BillingProviderNPI;
                    //else if (Convert.ToString(dt.Rows[0]["sBox33B"]) == "Facility NPI")
                    //    _Billing_B_NPI = NPIType.FacilityNPI;
                    //else if (Convert.ToString(dt.Rows[0]["sBox33B"]) == "Clinic NPI")
                    //    _Billing_B_NPI = NPIType.ClinicNPI;


                    if (Convert.ToString(dt.Rows[0]["sInsuranceTypeCode"]) == "MA" || Convert.ToString(dt.Rows[0]["sInsuranceTypeCode"]) == "MB")
                    {
                        _InsuranceTypeCode = Convert.ToString(dt.Rows[0]["sInsuranceTypeCode"]);
                    }

                    if (_InsuranceTypeCode == "MA" || _InsuranceTypeCode == "MB")
                    {
                        txtFacilityInfo.Text = "";
                    }
                   
                    _IsAcceptAssignment = Convert.ToBoolean(dt.Rows[0]["bAccessAssignment"]);
                    _IsPaperDisplayMailingAddress = Convert.ToBoolean(dt.Rows[0]["bPaperDisplayMailingAddress"]);
                    _IsSwap1a9a1aMCare = Convert.ToBoolean(dt.Rows[0]["bSwap1a9a1aMCare"]);
                    _IsEMGAsX = Convert.ToBoolean(dt.Rows[0]["bEMGAsX"]);
                    _bShowClaimNo = Convert.ToBoolean(dt.Rows[0]["bShowClaimNo"]);
                    _nBox11bSetting= Convert.ToInt16(dt.Rows[0]["nBox11bSettingID"]);
                   
                }
                dt = null;

                #region Company Name as Plan Name
                if (oTransaction.InsuranceCompanyName != null)
                {
                    dt = oTransaction.InsuranceCompanyName.Copy();
                }
 
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToString(dt.Rows[0]["ComapanyName"]).Trim() != "")
                    {
                        _CompanyName = Convert.ToString(dt.Rows[0]["ComapanyName"]);
                    }
                }

                
               
                
                #endregion


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }
       

        public void FillCMSInsurancesNew(DataTable dtPatientInsurances)
        {
          
            try
            {
                IsSecondaryInsurance = false;
                if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPatientInsurances.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            //Primary Insurance
                            _PayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]);
                            _PatientInsuranceID = Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"]);
                            _SubscriberAddress = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr1"]) + " " + Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr2"]);
                            _SubscriberCity = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCity"]);
                            _SubscriberState = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberState"]);
                            _SubscriberZIP = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberZip"]);
                            _SubscriberRelationshipCode = Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]);
                            _SubscriberMName = Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]);
                            if (Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]) != "18" && Convert.ToBoolean(dtPatientInsurances.Rows[0]["bIsCompnay"]) == true)
                                _SubscriberLName = Convert.ToString(dtPatientInsurances.Rows[0]["sCompanyName"]);
                            else
                                _SubscriberLName = Convert.ToString(dtPatientInsurances.Rows[0]["SubLName"]);
                            _SubscriberFName = Convert.ToString(dtPatientInsurances.Rows[0]["SubFName"]);
                            _SubscriberDOB = Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]);
                            _SubscriberGender = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]);
                            _SubscriberInsuranceBelongs = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]);//"CI"; 
                            _SubscriberInsuranceID = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]);
                            //_SubscriberInsurancePST = "P";//Convert.ToString(dtPatientInsurances.Rows[0][""]);
                            _SubscriberGroupID = Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"]);
                            _PayerID = Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]);
                            _PayerAddress = Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress1"]);
                            _PayerAddress2 = Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress2"]);
                            _PayerCity = Convert.ToString(dtPatientInsurances.Rows[0]["PayerCity"]);
                            _PayerState = Convert.ToString(dtPatientInsurances.Rows[0]["PayerState"]);
                            _PayerZip = Convert.ToString(dtPatientInsurances.Rows[0]["PayerZip"]);
                            _SubscriberPhone = Convert.ToString(dtPatientInsurances.Rows[0]["sPhone"]);
                            _ContactID = Convert.ToInt64(dtPatientInsurances.Rows[0]["nContactID"]);
                            _PriorAuthorizationNo = GetPriorAuthorizationNumber(oTransaction.TransactionMasterID);

                            _EmployerName = Convert.ToString(dtPatientInsurances.Rows[0]["sEmployer"]);
                            
                            _ClaimRemittanceRefNo = Convert.ToString(dtPatientInsurances.Rows[0]["sClaimRemittanceRefNo"]);
                            
                        }
                        else if (i == 1)
                        {
                            //Secondary Insurance
                            IsSecondaryInsurance = true;
                            _OtherInsuranceAddress = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberAddr1"]);
                            _OtherInsuranceName = Convert.ToString(dtPatientInsurances.Rows[i]["InsuranceName"]);
                            _PatientOtherInsuranceID = Convert.ToInt64(dtPatientInsurances.Rows[i]["nInsuranceID"]);
                            _OtherInsuranceSubscriberFName = Convert.ToString(dtPatientInsurances.Rows[i]["SubFName"]);
                            _OtherInsuranceSubscriberLName = Convert.ToString(dtPatientInsurances.Rows[i]["SubLName"]);
                            _OtherInsuranceSubscriberMName = Convert.ToString(dtPatientInsurances.Rows[i]["SubMName"]);
                            _OtherInsuranceSubscriberGender = Convert.ToString(dtPatientInsurances.Rows[i]["sSubscriberGender"]);
                            _OtherInsuranceSubscriberDOB = Convert.ToString(dtPatientInsurances.Rows[i]["dtDOB"]);
                            _OtherInsuranceZIP = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberZip"]);
                            _OtherInsuranceType = Convert.ToString(dtPatientInsurances.Rows[i]["InsuranceTypeCode"]);//"CI"
                            _OtherInsuranceState = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberState"]);
                            _OtherInsuranceRelationshipCode = Convert.ToString(dtPatientInsurances.Rows[i]["RelationshipCode"]);
                            //_OtherInsurancePST = "S"; 
                            _OtherInsurancePayerID = Convert.ToString(dtPatientInsurances.Rows[i]["PayerID"]);
                            _OtherInsuranceID = Convert.ToString(dtPatientInsurances.Rows[i]["sSubscriberID"]);
                            _OtherInsuranceGroupID = Convert.ToString(dtPatientInsurances.Rows[i]["sGroup"]);
                            _OtherInsuranceCity = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberCity"]);
                            _OtherEmployerName = Convert.ToString(dtPatientInsurances.Rows[i]["sEmployer"]);
                           _OtherInsuranceContactID = Convert.ToInt64(dtPatientInsurances.Rows[i]["nContactID"]);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);


            }
        }
       
     

        //Mahesh Nawal 20102307
        private string GetPriorAuthorizationNumber(Int64 TransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            Object _result = null;
            string _PriorAuthorizationNo = "";
            try
            {
                _strSQL = "SELECT PriorAuthorization_Mst.sPriorAuthorizationNo FROM BL_Transaction_MST WITH(NOLOCK) " +
                        " INNER JOIN PriorAuthorization_Mst WITH(NOLOCK) ON BL_Transaction_MST.nAuthorizationID=PriorAuthorization_Mst.nPriorAuthorizationID " +
                        " WHERE BL_Transaction_MST.nTransactionID=" + TransactionID;
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(_strSQL);
                oDB.Disconnect();
                if (_result != null)
                {
                    _PriorAuthorizationNo = Convert.ToString(_result);
                }
                oDB.Disconnect();
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return _PriorAuthorizationNo;
        }

        private void FillSubmitterInfo(Int64 _SelectedClinicId, Int64 _nProviderID)
        {
            clsgloBilling oBill = new clsgloBilling(_databaseconnectionstring);
            DataTable dt = null;
            try
            {
                dt = oBill.GetSubmitterInfo(_SelectedClinicId, _nProviderID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //nClinicID,sAddress1,sAddress2,sStreet,sCity,sState,sZIP,sPhoneNo,sMobileNo,
                    //sFAX,sTAXID,sContactPersonName,sContactPersonAddress1,sContactPersonAddress2,sContactPersonPhone,
                    //sContactPersonFAX,sContactPersonMobile
                    _SubmitterName = Convert.ToString(dt.Rows[0]["SubmitterName"]);
                    _SubmitterAddress = Convert.ToString(dt.Rows[0]["SubmitterAddress1"]) + " " + Convert.ToString(dt.Rows[0]["SubmitterAddress2"]);
                    _SubmitterCity = Convert.ToString(dt.Rows[0]["SubmitterCity"]);
                    _SubmitterState = Convert.ToString(dt.Rows[0]["SubmitterState"]);
                    _SubmitterZIP = Convert.ToString(dt.Rows[0]["SubmitterZIP"]);
                    _SubmitterAreaCode = Convert.ToString(dt.Rows[0]["SubmitterAreaCode"]);
                    
                    if (Convert.ToString(dt.Rows[0]["SubmitterContactName"]) == "")
                    {
                        _SubmitterContactPersonName = Convert.ToString(dt.Rows[0]["SubmitterName"]);
                    }
                    else
                    {
                        _SubmitterContactPersonName = Convert.ToString(dt.Rows[0]["SubmitterContactName"]);
                    }
                    _SubmitterContactPersonNo = Convert.ToString(dt.Rows[0]["SubmitterPhone"]);
                    //_SubmitterETIN = "C0923";

                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oBill != null)
                {
                    oBill.Dispose();
                    oBill = null;
                }
            }

        }

        private DataTable GetClaimPrimaryDiagnosis(Int64 TransactionID, Int64 ClinicID, Int64 ClaimNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dtAllDx = new DataTable();
            DataTable dtClaimDx = new DataTable();
            dtClaimDx.Columns.Add("DX");

            try
            {
                oDB.Connect(false);

                strSQL = "Select ISNULL(sDx1Code,'') AS sDx1Code,ISNULL(sDx2Code,'') AS sDx2Code, " +
                     " ISNULL(sDx3Code,'') AS sDx3Code,ISNULL(sDx4Code,'') AS sDx4Code,ISNULL(sLinePrimaryDxCode,'') AS sLinePrimaryDxCode, " +
                     " ISNULL(ntransactionlineno,0) AS ntransactionlineno " +
                     " from BL_Transaction_Claim_Lines WITH(NOLOCK)  WHERE (nTransactionID = " + TransactionID + ") AND (nClinicID = " + ClinicID + ") " +
                     " order by ntransactionlineno";

                oDB.Retrive_Query(strSQL, out dtAllDx);
                DataRow dr;
                ArrayList _claimDx = new ArrayList();
                string _tempDxCode = "";

                if (dtAllDx != null && dtAllDx.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAllDx.Rows.Count; i++)
                    {

                        //...Line 1 Primary Diagnosis
                        _tempDxCode = "";
                        _tempDxCode = Convert.ToString(dtAllDx.Rows[i]["sLinePrimaryDxCode"]).Trim().ToUpper();
                        if (Convert.ToInt32(dtAllDx.Rows[i]["ntransactionlineno"]) == 1 && _tempDxCode != "")
                        { _claimDx.Add(_tempDxCode); }

                        //..... Line Dx1
                        _tempDxCode = "";
                        _tempDxCode = Convert.ToString(dtAllDx.Rows[i]["sDx1Code"]).Trim().ToUpper();
                        if (_tempDxCode != "" && _claimDx.Contains(_tempDxCode) == false)
                        { _claimDx.Add(_tempDxCode); }

                        //..... Line Dx2
                        _tempDxCode = "";
                        _tempDxCode = Convert.ToString(dtAllDx.Rows[i]["sDx2Code"]).Trim().ToUpper();
                        if (_tempDxCode != "" && _claimDx.Contains(_tempDxCode) == false)
                        { _claimDx.Add(_tempDxCode); }

                        //..... Line Dx3
                        _tempDxCode = "";
                        _tempDxCode = Convert.ToString(dtAllDx.Rows[i]["sDx3Code"]).Trim().ToUpper();
                        if (_tempDxCode != "" && _claimDx.Contains(_tempDxCode) == false)
                        { _claimDx.Add(_tempDxCode); }

                        //..... Line Dx4
                        _tempDxCode = "";
                        _tempDxCode = Convert.ToString(dtAllDx.Rows[i]["sDx4Code"]).Trim().ToUpper();
                        if (_tempDxCode != "" && _claimDx.Contains(_tempDxCode) == false)
                        { _claimDx.Add(_tempDxCode); }

                    }

                    if (_claimDx != null && _claimDx.Count > 0)
                    {
                        for (int DxIndex = 0; DxIndex < _claimDx.Count; DxIndex++)
                        {
                            dr = dtClaimDx.NewRow();
                            dr["DX"] = _claimDx[DxIndex].ToString();
                            dtClaimDx.Rows.Add(dr);
                            dr = null;
                        }

                        if (dtClaimDx != null)
                        { return dtClaimDx; }
                    }
                }
                
                return null;
            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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
        }

        //gloPM5070
        private void FillMidlevelBillingProvider(Int64 nRenderingProviderID,string Type)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlquery = String.Empty;
            DataTable dtProviders = null;
            try
            {
                oDB.Connect(false);
                _sqlquery="Select Provider_MST.sFirstName AS ProviderFName, Provider_MST.nProviderID AS ProviderID, "+
                           " Provider_MST.sMiddleName AS ProviderMName, Provider_MST.sLastName AS ProviderLName,"+
                            " Provider_MST.sBusinessAddressline1 AS ProviderBusAddr1, Provider_MST.sBusinessAddressline2 AS ProviderBusAddr2," +               
                            " Provider_MST.sBusinessCity AS ProviderBusCity, Provider_MST.sBusinessState AS ProviderBusState,"   +
                            " Provider_MST.sBusinessZIP AS ProviderBusZip,ISNULL(Provider_MST.sBusinessAreaCode,'') AS ProviderBusAreaCode, Provider_MST.sPracticeAddressline1 AS ProviderPracAddr1, " +
                            " Provider_MST.sPracticeAddressline2 AS ProviderPracAddr2, Provider_MST.sPracticeCity AS ProviderPracCity, "                 +
                            " Provider_MST.sPracticeState AS ProviderPracState, Provider_MST.sPracticeZIP AS ProviderPracZip, "  +           
                            " ISNULL(Provider_MST.sPracticeAreaCode,'') AS ProviderPracAreaCode, " +                
                            " Provider_MST.sBusPhoneNo AS ProviderBusPhone, Provider_MST.sBusFAX AS ProviderBusFAX, " +
                            " Provider_MST.sPracPhoneNo AS ProviderPracPhone, Provider_MST.sNPI AS ProviderNPI, Provider_MST.sUPIN AS ProviderUPIN," +
                            " Provider_MST.sMedicalLicenseNo AS ProviderStateMedicalNo, Provider_MST.sTaxonomy AS ProviderTaxonomyCode,  " +
                            " Provider_MST.sTaxonomyDesc AS ProviderTaxonomyDesc, Provider_MST.sCompanyName AS ProviderCompName," +
                            " Provider_MST.sCompanyAddressline1 AS ProviderCompAddr1, Provider_MST.sCompanyAddressline2 AS ProviderCompAddr2," +          
                            " Provider_MST.sCompanyCity AS ProviderCompCity, Provider_MST.sCompanyState AS ProviderCompState," +
                            " Provider_MST.sCompanyZIP AS ProviderCompZip,ISNULL(Provider_MST.sCompanyAreaCode,'') AS ProviderCompAreaCode, Provider_MST.sCompanyPhone AS ProviderCompPhone, "+             
                            " Provider_MST.sCompanyFax AS ProviderCompFax, Provider_MST.sCompanyContactName AS ProviderCompContactName," +
                            " Provider_MST.sBusinessContactName AS ProviderBusContactName, Provider_MST.sPracContactName AS ProviderPracContactName,"+
                            " Provider_MST.sSSN AS ProviderSSN, Provider_MST.sEmployerID AS ProviderEmployerID,Provider_MST.ssuffix as  ssuffix from Provider_MST WITH(NOLOCK) where nProviderID=" + nRenderingProviderID + "";
                oDB.Retrive_Query(_sqlquery,out dtProviders);
                oDB.Disconnect();


                if (dtProviders != null && dtProviders.Rows.Count > 0)
                {
                    if (Type == "Provider")
                    {
                        if (_BillingAddressType == AddressType.ProviderAddress)
                        {
                            if (Convert.ToString(dtProviders.Rows[0]["ProviderBusZip"]).Trim() != "" && Convert.ToString(dtProviders.Rows[0]["ProviderBusAreaCode"]).Trim() != "")
                            {
                                txtBillingProviderInfo.Text =
                                                  Convert.ToString(dtProviders.Rows[0]["ProviderFName"]).Trim() + " " +
                                                  Convert.ToString(dtProviders.Rows[0]["ProviderMName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["ProviderLName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["ssuffix"]).Trim()
                                                  + Environment.NewLine + Convert.ToString(dtProviders.Rows[0]["ProviderBusAddr1"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["ProviderBusAddr2"]).Trim() + Environment.NewLine +
                                                  Convert.ToString(dtProviders.Rows[0]["ProviderBusCity"]) + " " + Convert.ToString(dtProviders.Rows[0]["ProviderBusState"]).Trim() + "   " +
                                                  Convert.ToString(dtProviders.Rows[0]["ProviderBusZip"]).Trim() + "-" + Convert.ToString(dtProviders.Rows[0]["ProviderBusAreaCode"]).Trim();
                            }
                            else
                            {
                                txtBillingProviderInfo.Text =
                                                 Convert.ToString(dtProviders.Rows[0]["ProviderFName"]).Trim() + " " +
                                                 Convert.ToString(dtProviders.Rows[0]["ProviderMName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["ProviderLName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["ssuffix"]).Trim()
                                                 + Environment.NewLine + Convert.ToString(dtProviders.Rows[0]["ProviderBusAddr1"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["ProviderBusAddr2"]).Trim() + Environment.NewLine +
                                                 Convert.ToString(dtProviders.Rows[0]["ProviderBusCity"]) + " " + Convert.ToString(dtProviders.Rows[0]["ProviderBusState"]).Trim() + "   " +
                                                 Convert.ToString(dtProviders.Rows[0]["ProviderBusZip"]).Trim();
                            }
                        }
                        if (_Billing_A_NPI == NPIType.BillingProviderNPI)
                        {
                            txtBillingProviderInfo.Tag = Convert.ToString(dtProviders.Rows[0]["ProviderID"]).Trim();
                            txtBillingProv_a_NPI.Text = Convert.ToString(dtProviders.Rows[0]["ProviderNPI"]).Trim();

                        }
                        txtPhyscianSignature.Text = Convert.ToString(dtProviders.Rows[0]["ProviderFName"]).Trim() + " " +
                                                    Convert.ToString(dtProviders.Rows[0]["ProviderMName"]).Trim() + " " + 
                                                    Convert.ToString(dtProviders.Rows[0]["ProviderLName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["ssuffix"]).Trim();
                    }
                    else if (Type == "Facility")
                    {
                        if (_FacilityAddressType == AddressType.ProviderAddress)
                        {
                            if (Convert.ToString(dtProviders.Rows[0]["ProviderBusZip"]).Trim() != "" && Convert.ToString(dtProviders.Rows[0]["ProviderBusAreaCode"]).Trim() != "")
                            {
                                txtFacilityInfo.Text =
                                                  Convert.ToString(dtProviders.Rows[0]["ProviderFName"]).Trim() + " " +
                                                  Convert.ToString(dtProviders.Rows[0]["ProviderMName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["ProviderLName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["ssuffix"]).Trim()
                                                  + Environment.NewLine + Convert.ToString(dtProviders.Rows[0]["ProviderBusAddr1"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["ProviderBusAddr2"]).Trim() + Environment.NewLine +
                                                  Convert.ToString(dtProviders.Rows[0]["ProviderBusCity"]) + " " + Convert.ToString(dtProviders.Rows[0]["ProviderBusState"]).Trim() + "   " +
                                                  Convert.ToString(dtProviders.Rows[0]["ProviderBusZip"]).Trim() + "-" + Convert.ToString(dtProviders.Rows[0]["ProviderBusAreaCode"]).Trim();
                            }
                            else
                            {
                                txtFacilityInfo.Text =
                                                 Convert.ToString(dtProviders.Rows[0]["ProviderFName"]).Trim() + " " +
                                                 Convert.ToString(dtProviders.Rows[0]["ProviderMName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["ProviderLName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["ssuffix"]).Trim()
                                                 + Environment.NewLine + Convert.ToString(dtProviders.Rows[0]["ProviderBusAddr1"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["ProviderBusAddr2"]).Trim() + Environment.NewLine +
                                                 Convert.ToString(dtProviders.Rows[0]["ProviderBusCity"]) + " " + Convert.ToString(dtProviders.Rows[0]["ProviderBusState"]).Trim() + "   " +
                                                 Convert.ToString(dtProviders.Rows[0]["ProviderBusZip"]).Trim();
                            }
                        }

                        if (_Facility_A_NPI == NPIType.BillingProviderNPI)
                        {
                            txtFacility_a_NPI.Tag = Convert.ToString(dtProviders.Rows[0]["ProviderID"]).Trim();
                            txtFacility_a_NPI.Text = Convert.ToString(dtProviders.Rows[0]["ProviderNPI"]).Trim();

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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private void FillMidlevelRenderingProvider(Int64 nRenderingProviderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlquery = String.Empty;
            DataTable dtProviders = null;
            try
            {
                oDB.Connect(false);
                _sqlquery = "Select nProviderID,sNPI from Provider_MST WITH(NOLOCK) where nProviderID=" + nRenderingProviderID + "";
                oDB.Retrive_Query(_sqlquery, out dtProviders);
                oDB.Disconnect();

                if (dtProviders != null && dtProviders.Rows.Count > 0)
                {
                    if (txtCPT1.Text.Trim() != "")
                    {
                        txtRenderingProvider1_NPI.Text = Convert.ToString(dtProviders.Rows[0]["sNPI"]);
                        txtRenderingProvider1_NPI.Tag = Convert.ToString(dtProviders.Rows[0]["nProviderID"]);
                    }

                    if (txtCPT2.Text.Trim() != "")
                    {
                        txtRenderingProvider2_NPI.Text = Convert.ToString(dtProviders.Rows[0]["sNPI"]);
                        txtRenderingProvider2_NPI.Tag = Convert.ToString(dtProviders.Rows[0]["nProviderID"]);
                    }

                     if (txtCPT3.Text.Trim() != "")
                    {
                        txtRenderingProvider3_NPI.Text = Convert.ToString(dtProviders.Rows[0]["sNPI"]);
                        txtRenderingProvider3_NPI.Tag = Convert.ToString(dtProviders.Rows[0]["nProviderID"]);
                    }

                    if (txtCPT4.Text.Trim() != "")
                    {
                        txtRenderingProvider4_NPI.Text = Convert.ToString(dtProviders.Rows[0]["sNPI"]);
                        txtRenderingProvider4_NPI.Tag = Convert.ToString(dtProviders.Rows[0]["nProviderID"]);
                    }
                                
                    if (txtCPT5.Text.Trim() != "")
                    {
                        txtRenderingProvider5_NPI.Text = Convert.ToString(dtProviders.Rows[0]["sNPI"]);
                        txtRenderingProvider5_NPI.Tag = Convert.ToString(dtProviders.Rows[0]["nProviderID"]);
                    }

                    if (txtCPT6.Text.Trim() != "")
                    {
                        txtRenderingProvider6_NPI.Text = Convert.ToString(dtProviders.Rows[0]["sNPI"]);
                        txtRenderingProvider6_NPI.Tag = Convert.ToString(dtProviders.Rows[0]["nProviderID"]);
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private string GetPhysicianOtherID(Int64 nProviderId, Int64 ContactID, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlquery = String.Empty;
            DataTable dtOtherId = null;
            string sReturn = string.Empty;
            try
            {
                 oDB.Connect(false);
                 _sqlquery = "SELECT dbo.[GET_PHYSICIAN_OTHERID](" + ContactID + "," + nProviderId + "," + ClinicID + ")";
                 oDB.Retrive_Query(_sqlquery, out dtOtherId);
               
                if (dtOtherId != null && dtOtherId.Rows.Count > 0)
                {
                    sReturn = Convert.ToString(dtOtherId.Rows[0][0]).Trim();
                }
            }
            catch (Exception ex)
            {
                sReturn = "";
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }
            }
            return sReturn;
        }

        private void FillBillingProviderFacility(Int64 nProviderId, Int64 nFacilityId, Int64 ContactID, string SettingName, string Type)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlquery = String.Empty;
            DataTable dtProviders = null;
            try
            {
                oDB.Connect(false);

                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nProviderID",nProviderId , ParameterDirection.Input, SqlDbType.BigInt);
                 oDBParameters.Add("@nFacilityID",nFacilityId , ParameterDirection.Input, SqlDbType.BigInt);
                 oDBParameters.Add("@nContactId", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                 oDBParameters.Add("@sSettingName",SettingName , ParameterDirection.Input, SqlDbType.VarChar);
                 oDBParameters.Add("@bIsEDI", 0, ParameterDirection.Input, SqlDbType.Bit);
                 oDBParameters.Add("@bIsPhysician", true, ParameterDirection.Input, SqlDbType.Bit);
                 oDBParameters.Add("@nPatientId", PatientID, ParameterDirection.Input, SqlDbType.BigInt); 
                oDB.Retrive ("BL_Get_AlternateID_Settings", oDBParameters,out dtProviders);

                if (dtProviders != null && dtProviders.Rows.Count > 0)
                {
                    if (Type == "Provider")
                    {
                       
                            if (Convert.ToString(dtProviders.Rows[0]["ZIP"]).Trim() != "" && Convert.ToString(dtProviders.Rows[0]["AreaCode"]).Trim() != "")
                            {
                                txtBillingProviderInfo.Text =
                                                  Convert.ToString(dtProviders.Rows[0]["FirstName"]).Trim() + " " +
                                                  Convert.ToString(dtProviders.Rows[0]["MiddleName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["LastName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["sSuffix"]).Trim()
                                                  + Environment.NewLine + Convert.ToString(dtProviders.Rows[0]["Address1"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["Address2"]).Trim() + Environment.NewLine +
                                                  Convert.ToString(dtProviders.Rows[0]["City"]) + " " + Convert.ToString(dtProviders.Rows[0]["State"]).Trim() + "   " +
                                                  Convert.ToString(dtProviders.Rows[0]["ZIP"]).Trim() + "-" + Convert.ToString(dtProviders.Rows[0]["AreaCode"]).Trim();
                            }
                            else
                            {
                                txtBillingProviderInfo.Text =
                                                 Convert.ToString(dtProviders.Rows[0]["FirstName"]).Trim() + " " +
                                                 Convert.ToString(dtProviders.Rows[0]["MiddleName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["LastName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["sSuffix"]).Trim()
                                                 + Environment.NewLine + Convert.ToString(dtProviders.Rows[0]["Address1"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["Address2"]).Trim() + Environment.NewLine +
                                                 Convert.ToString(dtProviders.Rows[0]["City"]) + " " + Convert.ToString(dtProviders.Rows[0]["State"]).Trim() + "   " +
                                                 Convert.ToString(dtProviders.Rows[0]["ZIP"]).Trim();
                            }


                            if (dtProviders.Rows[0]["PhoneNo"].ToString().Trim() != "")
                            {
                                txtBillingProviderPhone1.Text = Convert.ToString(dtProviders.Rows[0]["PhoneNo"]).Trim().Substring(0, 3);
                                txtBillingProviderPhone2.Text = Convert.ToString(dtProviders.Rows[0]["PhoneNo"]).Trim().Substring(3, Convert.ToString(dtProviders.Rows[0]["PhoneNo"]).Trim().Length - 3);
                            }

                            txtBillingProviderInfo.Tag = Convert.ToString(nProviderId).Trim();
                            txtBillingProv_a_NPI.Text = Convert.ToString(dtProviders.Rows[0]["PrimaryQualifierValue"]).Trim();

                            if (Convert.ToString(dtProviders.Rows[0]["SecondaryQualifierValue"]).Trim() != "" && Convert.ToBoolean(dtProviders.Rows[0]["Isdefaultother"]) == false)
                            {
                                txtBillingProv_b_UPIN.Text = Convert.ToString(dtProviders.Rows[0]["SecondaryQualifier"]).Trim() + Convert.ToString(dtProviders.Rows[0]["SecondaryQualifierValue"]).Trim();
                            }

                            #region Taxonomy "
                            
                                oDBParameters.Clear();
                                DataTable _dtTaxonomy = null;
                                oDBParameters.Add("@nProviderID", nProviderId, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nFacilityID", nFacilityId, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nContactId", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sSettingName", SettingName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@bIsEDI", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.VarChar);
                                oDB.Retrive("BL_Get_BillingProvider_Taxonomy", oDBParameters, out _dtTaxonomy);

                            #endregion

                            if (dtProviders != null && _dtTaxonomy != null && dtProviders.Rows.Count > 0 && _dtTaxonomy.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dtProviders.Rows[0]["IsSwapID"]) == true && Convert.ToString(_dtTaxonomy.Rows[0]["sTaxonomyCode"]).Trim() != "" && Convert.ToString(_dtTaxonomy.Rows[0]["sTaxonomyQualifier"]) != "")
                                {
                                    txtBillingProviderInfo.Tag = Convert.ToString(nProviderId).Trim();
                                    txtBillingProv_a_NPI.Text = Convert.ToString(_dtTaxonomy.Rows[0]["sTaxonomyQualifier"]).Trim() + Convert.ToString(_dtTaxonomy.Rows[0]["sTaxonomyCode"]).Trim();
                                }
                                else if (Convert.ToString(_dtTaxonomy.Rows[0]["sTaxonomyCode"]).Trim() != "" && Convert.ToString(_dtTaxonomy.Rows[0]["sTaxonomyQualifier"]) != "")
                                {
                                    txtBillingProv_b_UPIN.Text = Convert.ToString(_dtTaxonomy.Rows[0]["sTaxonomyQualifier"]).Trim() + Convert.ToString(_dtTaxonomy.Rows[0]["sTaxonomyCode"]).Trim();
                                }

                            }
                    }
                    else if (Type == "Facility")
                    {
                       
                            if (Convert.ToString(dtProviders.Rows[0]["ZIP"]).Trim() != "" && Convert.ToString(dtProviders.Rows[0]["AreaCode"]).Trim() != "")
                            {
                                txtFacilityInfo.Text =
                                                  Convert.ToString(dtProviders.Rows[0]["FirstName"]).Trim() + " " +
                                                  Convert.ToString(dtProviders.Rows[0]["MiddleName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["LastName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["sSuffix"]).Trim()
                                                  + Environment.NewLine + Convert.ToString(dtProviders.Rows[0]["Address1"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["Address2"]).Trim() + Environment.NewLine +
                                                  Convert.ToString(dtProviders.Rows[0]["City"]) + " " + Convert.ToString(dtProviders.Rows[0]["State"]).Trim() + "   " +
                                                  Convert.ToString(dtProviders.Rows[0]["ZIP"]).Trim() + "-" + Convert.ToString(dtProviders.Rows[0]["AreaCode"]).Trim();
                            }
                            else
                            {
                                txtFacilityInfo.Text =
                                                 Convert.ToString(dtProviders.Rows[0]["FirstName"]).Trim() + " " +
                                                 Convert.ToString(dtProviders.Rows[0]["MiddleName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["LastName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["sSuffix"]).Trim()
                                                 + Environment.NewLine + Convert.ToString(dtProviders.Rows[0]["Address1"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["Address2"]).Trim() + Environment.NewLine +
                                                 Convert.ToString(dtProviders.Rows[0]["City"]) + " " + Convert.ToString(dtProviders.Rows[0]["State"]).Trim() + "   " +
                                                 Convert.ToString(dtProviders.Rows[0]["ZIP"]).Trim();
                            }

                            txtFacility_a_NPI.Tag = Convert.ToString(nProviderId).Trim();
                            txtFacility_a_NPI.Text = Convert.ToString(dtProviders.Rows[0]["PrimaryQualifierValue"]).Trim();

                            //txtFacility_b.Tag = Convert.ToString(nProviderId).Trim();
                            if (Convert.ToString(dtProviders.Rows[0]["SecondaryQualifierValue"]).Trim() != "" && Convert.ToBoolean(dtProviders.Rows[0]["Isdefaultother"])==false)
                            {
                                txtFacility_b.Text = Convert.ToString(dtProviders.Rows[0]["SecondaryQualifier"]).Trim() + Convert.ToString(dtProviders.Rows[0]["SecondaryQualifierValue"]).Trim();
                            }
                    }
                    oDB.Disconnect();
                    
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private void FillOtherReferrinfProviderInfo(Int64 ContactID, string SettingName, Int64 ProviderID, bool bSameasBillingProvider)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlquery = String.Empty;
            DataTable dtOthersInfo = null;
            try
            {
                oDB.Connect(false);

                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nFacilityID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nContactId", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sSettingName", SettingName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@bIsEDI", 0, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@bIsPhysician", !bSameasBillingProvider, ParameterDirection.Input, SqlDbType.Bit); 

                oDB.Retrive("BL_Get_AlternateID_Settings", oDBParameters, out dtOthersInfo);
                       
                oDB.Disconnect();
                
                if (dtOthersInfo != null && dtOthersInfo.Rows.Count > 0)
                {
                    txtReferringProvider_OtherType.Tag = Convert.ToString(dtOthersInfo.Rows[0]["QualifierID"]).Trim();
                    txtReferringProvider_OtherType.Text = Convert.ToString(dtOthersInfo.Rows[0]["Code"]).Trim();
                    txtReferringProvider_OtherValue.Text = Convert.ToString(dtOthersInfo.Rows[0]["Value"]).Trim();

                }
                //txtReferringProvider_OtherType.Text = "MM";
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }



        private DataTable  FillRenderingProvider(Int64 nProviderId, Int64 nFacilityId, Int64 ContactID, string SettingName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlquery = String.Empty;
            //string _NPTID = "";
            DataTable dtProviders = null;
            try
            {
                oDB.Connect(false);

                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nProviderID", nProviderId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nFacilityID", nFacilityId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nContactId", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sSettingName", SettingName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@bIsEDI", 0, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@bIsPhysician", true, ParameterDirection.Input, SqlDbType.Bit); 
                oDB.Retrive("BL_Get_AlternateID_Settings", oDBParameters, out dtProviders);

                oDB.Disconnect();
                //if (dtProviders != null && dtProviders.Rows.Count > 0)
                //{
                //    _NPTID= Convert.ToString(dtProviders.Rows[0]["QualifierValue"]).Trim();
                //}
                //return _NPTID;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return dtProviders;
        }

        private DataTable FillBillingProviderTaxonomy(Int64 nProviderId, Int64 nFacilityId, Int64 ContactID, string SettingName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlquery = String.Empty;
            DataTable dtProviders = null;
            try
            {
                oDB.Connect(false);

                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nProviderID", nProviderId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nFacilityID", nFacilityId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nContactId", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sSettingName", SettingName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_Get_BillingProvider_Taxonomy", oDBParameters, out dtProviders);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return dtProviders;
        }

        private void FillPhysician(Int64 nMidlevelProviderId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlquery = String.Empty;
            DataTable dtProviders = null;
            try
            {
                oDB.Connect(false);
                _sqlquery = "Select (Isnull(Provider_MST.sFirstName,'') + ' ' +  " +
                            " Isnull(Provider_MST.sMiddleName,'') +' '+IsNull(Provider_MST.sLastName,'') +" +
                            " ' '+Isnull(Provider_MST.ssuffix,'') )as  ProviderName from Provider_MST WITH(NOLOCK) where nProviderID=" + nMidlevelProviderId + "";
                oDB.Retrive_Query(_sqlquery, out dtProviders);
                oDB.Disconnect();

                if (dtProviders != null && dtProviders.Rows.Count > 0)
                {
                    txtPhyscianSignature.Text = Convert.ToString(dtProviders.Rows[0]["ProviderName"]).Trim(); 
                    txtPhyscianQualifierValue.Text = GetPhysicianOtherID(nMidlevelProviderId,Convert.ToInt64(oTransaction.ContactID), _ClinicID);
                }
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }
        }

        private void GetReferralProvider_Old(Int64 PatientID, Int64 TransactionID, Int64 TransactionMasterID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _strSQL = "";
            DataTable dtProvider = new DataTable();

            try
            {
                oDB.Connect(false);

                //MaheshB 02152010 Added BL_Transaction_Claim_MST and nTransactionMasterId.
                _strSQL = "SELECT   Patient_DTL.nContactId, Patient_DTL.sAddressLine1, Patient_DTL.sAddressLine2, " +
                    " Patient_DTL.sCity, Patient_DTL.sState, Patient_DTL.sZIP, Patient_DTL.sPhone, Patient_DTL.sFirstName, " +
                    " Patient_DTL.sMiddleName, Patient_DTL.sLastName, Patient_DTL.sTaxonomy, " +
                    " Patient_DTL.sTaxonomyDesc, Patient_DTL.sTaxID, Patient_DTL.sUPIN, Patient_DTL.sNPI, Patient_DTL.sDegree, " +
                    " Patient_DTL.nContactFlag, Patient_DTL.sNotes " +
                    " FROM Patient_DTL WITH(NOLOCK) INNER JOIN BL_Transaction_Claim_MST WITH(NOLOCK) ON Patient_DTL.nPatientDetailID = BL_Transaction_Claim_MST.nReferralID " +
                    " WHERE     (Patient_DTL.nClinicID = " + _ClinicID + ") AND (Patient_DTL.nPatientID = " + PatientID + ") " +
                    " AND (Patient_DTL.nContactFlag = 3) AND (BL_Transaction_Claim_MST.nTransactionId = " + TransactionID + ") and (BL_Transaction_Claim_MST.nTransactionMasterId=nTransactionMasterID)";
                oDB.Retrive_Query(_strSQL, out dtProvider);
                oDB.Disconnect();

                if (dtProvider != null && dtProvider.Rows.Count > 0)
                {
                    for (int l = 0; l < dtProvider.Rows.Count; l++)
                    {
                        _ReferralId = Convert.ToString(dtProvider.Rows[l]["nContactId"]);
                        _ReferralFName = Convert.ToString(dtProvider.Rows[l]["sFirstName"]);
                        _ReferralLName = Convert.ToString(dtProvider.Rows[l]["sLastName"]);
                        _ReferralMName = Convert.ToString(dtProvider.Rows[l]["sMiddleName"]);
                        _ReferralAddress = Convert.ToString(dtProvider.Rows[l]["sAddressLine1"]);
                        _ReferralCity = Convert.ToString(dtProvider.Rows[l]["sCity"]);
                        _ReferralState = Convert.ToString(dtProvider.Rows[l]["sState"]);
                        _ReferralZIP = Convert.ToString(dtProvider.Rows[l]["sZIP"]);
                        _ReferralNPI = Convert.ToString(dtProvider.Rows[l]["sNPI"]);
                        _ReferralStateMedicalNo = Convert.ToString(dtProvider.Rows[l]["sUPIN"]);
                        _ReferralSSN = Convert.ToString(dtProvider.Rows[l]["sUPIN"]);
                        _ReferralEmployerID = Convert.ToString(dtProvider.Rows[l]["sTaxID"]);
                        _ReferralTaxonomy = Convert.ToString(dtProvider.Rows[l]["sTaxonomy"]);
                        _ReferralsSuffix = Convert.ToString(dtProvider.Rows[l]["sDegree"]);
                    }
                }
                // -------------------------------------
                // Added by Pankaj Bedse on 23012010 
                // As it was giving wrong output for Next & Previous Buttoms
                else
                {
                    _ReferralId = string.Empty ;
                    _ReferralFName = string.Empty;
                    _ReferralLName = string.Empty;
                    _ReferralMName = string.Empty;
                    _ReferralAddress = string.Empty;
                    _ReferralCity = string.Empty;
                    _ReferralState = string.Empty;
                    _ReferralZIP = string.Empty;
                    _ReferralNPI = string.Empty;
                    _ReferralStateMedicalNo = string.Empty;
                    _ReferralSSN = string.Empty;
                    _ReferralEmployerID = string.Empty;
                    _ReferralTaxonomy = string.Empty;
                }
                // -------------------------------------
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }
        }

        //20100616 gloPM5050 
        
        private DataTable GetReferralProvider(Int64 PatientID, Int64 BillingProviderID, Int64 ReferrinProviderID, bool IssameAsBillingProvider, string databaseconnectionstring)
        { //ReferrinProviderID is contactID from Patient_DTL Table.
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(databaseconnectionstring);
            String _strSQL = "";
            DataTable dtProvider = new DataTable();

            try
            {
                oDB.Connect(false);

                if (IssameAsBillingProvider == true)
                {
                    _strSQL = "Select    Isnull(sBusinessAddressline1,'') as Addressline1,Isnull(sBusinessAddressline2,'') as sAddressLine2, " +
                             " IsNull(sSuffix,'') as sDegree,Isnull(sBusinessCity,'') as sCity, Isnull(sBusinessState,'') as sState,Isnull(sBusinessZIP,'') as sZip,Isnull(sBusinessAreaCode,'') as sAreaCode,Isnull(sBusPhoneNo,'') as sPhone,Isnull(sFirstName,'') as sFirstName, " +
                             " Isnull(sMiddleName,'') as sMiddleName,Isnull(sLastName,'') as sLastName,Isnull(sTaxonomy,'') as sTaxonomy, " +
                             " Isnull(sTaxonomyDesc,'') as sTaxonomyDesc,Isnull(sCompanyTaxID,'') as sTaxID,Isnull(sUPIN,'') as sUPIN,Isnull(sNPI,'') as sNPI,Isnull(sSuffix,'') as sSuffix from Provider_Mst WITH(NOLOCK) where nProviderID=" + BillingProviderID + " and nClinicID=" + 1 + "";
                }
                else
                {
                    _strSQL = "SELECT ISNULL(Contacts_MST.sFirstName,'') AS sFirstName, " +
                               "ISNULL(Contacts_MST.sMiddleName,'') AS sMiddleName ,ISNULL(Contacts_Physician_DTL.sdegree,'') as sdegree,ISNULL(Contacts_MST.sLastName,'') AS sLastName ,ISNULL(Contacts_MST.sGender,'') AS  sGender ,  " +
                               "ISNULL(Contacts_Physician_DTL.sTaxonomy,'') AS sTaxonomy , ISNULL(Contacts_Physician_DTL.sTaxonomyDesc,'') AS sTaxonomyDesc, " +
                               "ISNULL(Contacts_Physician_DTL.sTaxID,'') AS sTaxID,ISNULL(Contacts_Physician_DTL.sNPI,'') AS sNPI  " +
                               "FROM Contacts_MST WITH(NOLOCK) left outer join Contacts_Physician_DTL WITH(NOLOCK) ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID  " +
                               " WHERE  Contacts_MST.nContactID=" + ReferrinProviderID + "";
                }
                oDB.Retrive_Query(_strSQL, out dtProvider);

                if (dtProvider != null && dtProvider.Rows.Count > 0)
                {

                            //_ReferralId = Convert.ToString(dtProvider.Rows[0]["nContactId"]);
                            _ReferralFName = Convert.ToString(dtProvider.Rows[0]["sFirstName"]);
                            _ReferralLName = Convert.ToString(dtProvider.Rows[0]["sLastName"]);
                            _ReferralMName = Convert.ToString(dtProvider.Rows[0]["sMiddleName"]);
                            //_ReferralAddress = Convert.ToString(dtProvider.Rows[0]["sAddressLine1"]);
                            //_ReferralCity = Convert.ToString(dtProvider.Rows[0]["sCity"]);
                            //_ReferralState = Convert.ToString(dtProvider.Rows[0]["sState"]);
                            //_ReferralZIP = Convert.ToString(dtProvider.Rows[0]["sZIP"]);
                            _ReferralNPI = Convert.ToString(dtProvider.Rows[0]["sNPI"]);
                            //_ReferralStateMedicalNo = Convert.ToString(dtProvider.Rows[0]["sUPIN"]);
                            //_ReferralSSN = Convert.ToString(dtProvider.Rows[0]["sUPIN"]);
                            //_ReferralEmployerID = Convert.ToString(dtProvider.Rows[0]["sTaxID"]);
                            _ReferralTaxonomy = Convert.ToString(dtProvider.Rows[0]["sTaxonomy"]);
                            _ReferralsSuffix = Convert.ToString(dtProvider.Rows[0]["sDegree"]);
                    
                }
                else
                {
                    //_ReferralId = string.Empty;
                    _ReferralFName = string.Empty;
                    _ReferralLName = string.Empty;
                    _ReferralMName = string.Empty;
                    //_ReferralAddress = string.Empty;
                    //_ReferralCity = string.Empty;
                    //_ReferralState = string.Empty;
                    //_ReferralZIP = string.Empty;
                    _ReferralNPI = string.Empty;
                    //_ReferralStateMedicalNo = string.Empty;
                    //_ReferralSSN = string.Empty;
                    //_ReferralEmployerID = string.Empty;
                    _ReferralTaxonomy = string.Empty;
                    _ReferralsSuffix = "";
                }
              

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }
            return dtProvider;
        }
            
        public bool SendPaperClaim(Int64 BatchID, Int64 ClaimType, Int64 ClaimStatus)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _strSQL = "";
            DataTable dtBatch = new DataTable();
            bool _result = false;

            try
            {
                oDB.Connect(false);

                _strSQL = "select dbo.BL_Transaction_Batch.nBatchID,dbo.BL_Transaction_Batch.sBatchName," +
                           "dbo.BL_Transaction_Batch_DTL.nClaimNo,dbo.BL_Transaction_Batch_DTL.nTransactionMasterID As nTransactionMasterID "+
                           ",dbo.BL_Transaction_Batch_DTL.nTransactionID AS nTransactionID " +
                           "from dbo.BL_Transaction_Batch WITH(NOLOCK) inner join " +
                           "BL_Transaction_Batch_DTL WITH(NOLOCK) on dbo.BL_Transaction_Batch.nBatchID =dbo.BL_Transaction_Batch_DTL.nBatchID " +
                           "WHERE   dbo.BL_Transaction_Batch.nBatchID = " + BatchID ;
                oDB.Retrive_Query(_strSQL, out dtBatch);
                oDB.Disconnect();

                if (dtBatch != null && dtBatch.Rows.Count > 0)
                { 
                    oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    for (int l = 0; l < dtBatch.Rows.Count; l++)
                    {
                       
                        gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                        oDBParameters.Add("@nBatchID", dtBatch.Rows[l]["nBatchID"], ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@sBatchName", dtBatch.Rows[l]["sBatchName"].ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@nClaimNo", Convert.ToInt64(dtBatch.Rows[l]["nClaimNo"].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nTransactionID", Convert.ToInt64(dtBatch.Rows[l]["nTransactionID"].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nClinicID", _ClinicID , ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nStatus", ClaimStatus, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nClaimType", ClaimType, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nMasterTransactionID", Convert.ToInt64(dtBatch.Rows[l]["nTransactionMasterID"].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Execute("BL_INUP_CMSEDI_Claim_Send", oDBParameters);

                        //From HotFix5020.2 On 20100325.
                        try
                        {
                            string _strquery = "";
                            _strquery = "Update BL_CMSEDI_Claim_Send WITH(READPAST) set bIsPrinted=0 , iCMSFile=null , dtPrintedDate= null where nTransactionID=" + Convert.ToInt64(Convert.ToString(dtBatch.Rows[l]["nTransactionID"])) + "";
                            oDB.Execute_Query(_strquery);
                        }
                        catch
                        {
                        }
                    }
                    _result = true;
                    oDB.Disconnect();
                }
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
            }
            return _result;
        }

        private void GetPatientStatusSettings(string PatientMaritalStatus)
        {
            GeneralSettings ogloSettings = new GeneralSettings(_databaseconnectionstring);
            object value = new object();
            try
            {
                ogloSettings.GetSetting(PatientMaritalStatus, out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _strPatientStatus = Convert.ToString(value.ToString());
                    value = null;
                }
            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                ogloSettings.Dispose();
                ogloSettings = null;
                value = null;
            }
        }

        private void btnBrowseFacility_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    frmSetupFacility ofrmSetupFacility = new frmSetupFacility(Convert.ToInt64(txtFacilityCode.Tag), _databaseconnectionstring);
            //    ofrmSetupFacility.ShowDialog();
            //    ofrmSetupFacility.Dispose();
            //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.View, "Browse Facility Information", gloAuditTrail.ActivityOutCome.Success);
            //    FillFacilityInfo(Convert.ToInt64(txtFacilityCode.Tag));
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}
        }


        public void PrintData(string FormName)
        {
            try
            {               
              //  _InputFilePath = Application.StartupPath.ToString() + "\\CMS1500_NEW.tif";
                _InputFilePath = Application.StartupPath.ToString();
                gloHCFA1500PaperFormNew oHCFA1500PaperForm = null;
                if (printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings != null)
                {
                    if (printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName != "")
                    {
                         oHCFA1500PaperForm = new gloHCFA1500PaperFormNew(printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName.ToString());
                    }
                }
                else
                {
                    oHCFA1500PaperForm = new gloHCFA1500PaperFormNew();
                }
                gloPrintPaperFormNew oPrintForm = new gloPrintPaperFormNew(_databaseconnectionstring);

                //if (System.IO.File.Exists(_InputFilePath) == true)
                //{
                _IsForPrintData = true;
                oHCFA1500PaperForm = SetHCFA1500PaperForm(oHCFA1500PaperForm);
                    if (oHCFA1500PaperForm != null)
                    { _OutputFilePath = oPrintForm.PrintHCFA1500FormNew(oHCFA1500PaperForm, _InputFilePath, false,true); }

                    if (System.IO.File.Exists(_OutputFilePath) == true)
                    {
                        bool isPrinted = false;
                        if (gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            string sPrinterName = "Default";
                            if (printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName != "")
                            {
                                sPrinterName = printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName;
                            }
                            clsPrintDocumentConversion oConversion = new clsPrintDocumentConversion(_databaseconnectionstring, sPrinterName, "CMS");
                            String convertedFile = printdoc_HCFA1500_ConversionWithLesserResolution(oConversion,true);
                            String imageFile = convertedFile;
                            if (!(gloGlobal.gloTSPrint.UseEMFForClaims))
                            {
                                String tempPDFFilePath = gloSettings.FolderSettings.AppTempFolderPath  + DateTime.Now.ToString("yyyyMMddhhmmsstt") + ".pdf";
                                convertedFile = clsClinicalChartPrinting.ConvertTiffToPDF(convertedFile, tempPDFFilePath,true, true, false);
                            }
                            isPrinted = gloClinicalQueueFunctions.CopyPrintDoc(convertedFile, "CMS1500", "PrintData");
                            if (isPrinted)
                            {
                                _OutputFilePath = imageFile;
                            }
                        }
                        if ( ! isPrinted)
                        {
                            System.Drawing.Printing.StandardPrintController oprintcontroller = new System.Drawing.Printing.StandardPrintController();
                            System.Drawing.Printing.PrinterSettings.PaperSizeCollection opapers = printdoc_HCFA1500.PrinterSettings.PaperSizes;

                            for (int i = 0; i <= opapers.Count - 1; i++)
                            {
                                if (opapers[i].PaperName.ToString().ToUpper() == "LETTER")
                                {
                                    printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PaperSize = opapers[i];
                                    break;
                                }
                            }
                            printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.Landscape = false;
                            printdoc_HCFA1500.PrintController = oprintcontroller;
                            printdoc_HCFA1500.Print();
                        }

                        #region "CODE FOR UPDATE THE PRINT CLAIM DATA"
                        //20100219 Mahesh Nawal

                        if (FormName == "Claimservice")
                        {
                                    //gloDatabaseLayer.DBLayer oDB = null;
                           
                                  //  oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                    System.Data.SqlClient.SqlConnection oConnection = new System.Data.SqlClient.SqlConnection();
                                    System.Data.SqlClient.SqlCommand UpdateCmd = null;
                                    //gloDatabaseLayer.DBParameters oDBParameters;
                                    try
                                    {

                                        //oDBParameters = new gloDatabaseLayer.DBParameters();
                                        //oDBParameters.Add("@nClaimno", oTransaction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
                                        //oDBParameters.Add("@bIsprinted", 1, ParameterDirection.Input, SqlDbType.Bit);
                                        //oDBParameters.Add("@dtprinteddate", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                                        //oDBParameters.Add("@iCMSFile", ConvertFileToBinary(_OutputFilePath), ParameterDirection.Input, SqlDbType.Image);


                                        //oDB.Connect(false);
                                        //oDB.Execute("BL_INUP_CMSEDI_PaperClaim_Update", oDBParameters);
                                        ////oDB.Execute_Query("Update BL_CMSEDI_Claim_Send set bIsPrinted=@bIsprinted,dtprinteddate=@dtprinteddate,iCMSFile=@iCMSFile where nCMSEDIClaimID=@nCMSEDIClaimID");


                                        
                                        oConnection.ConnectionString = _databaseconnectionstring;
                                        oConnection.Open();

                                        string updateSql = "Update BL_CMSEDI_Claim_Send WITH(READPAST) set bIsPrinted=@bIsprinted,dtprinteddate=@dtprinteddate,iCMSFile=@iCMSFile where nClaimNo=@nClaimno and sSubclaimNo=@sSubClaimno";
                                        UpdateCmd = new System.Data.SqlClient.SqlCommand(updateSql, oConnection);


                                        UpdateCmd.Parameters.Add("@nClaimno", SqlDbType.BigInt);

                                        UpdateCmd.Parameters.Add("@bIsprinted", SqlDbType.Bit);
                                        UpdateCmd.Parameters.Add("@iCMSFile", SqlDbType.Image);
                                        UpdateCmd.Parameters.Add("@dtprinteddate", SqlDbType.DateTime);
                                        UpdateCmd.Parameters.Add("@sSubClaimno", SqlDbType.VarChar);

                                        UpdateCmd.Parameters["@nClaimno"].Value = oTransaction.ClaimNo;
                                        UpdateCmd.Parameters["@sSubClaimno"].Value = oTransaction.SubClaimNo;
                                        UpdateCmd.Parameters["@bIsprinted"].Value = 1;
                                        UpdateCmd.Parameters["@dtprinteddate"].Value = System.DateTime.Now;
                                        UpdateCmd.Parameters["@iCMSFile"].Value = ConvertFileToBinary(_OutputFilePath);

                                        UpdateCmd.ExecuteNonQuery();
                                        oConnection.Close();

                                    }
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    }
                                    finally
                                    {


                                        if (UpdateCmd != null)
                                        {
                                            UpdateCmd.Parameters.Clear();
                                            UpdateCmd.Dispose();
                                            UpdateCmd = null;
                                         
                                        }

                                        if (oConnection != null)
                                        {
                                            oConnection.Close();
                                            oConnection.Dispose();
                                        }
                                    }
                            
                        }
#endregion
                        //File Delete 
            //            FileStream oFile = new FileStream(_OutputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous);
              //          oFile.Close();
                    //    File.Delete(_OutputFilePath);
                        
                       // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.Print, "Print HCFA1500 Claim file : " + _OutputFilePath + "", _PatientID, _TransactionId, 0, gloAuditTrail.ActivityOutCome.Success);
                    }
                //}
                //else
                //{
                //    MessageBox.Show("Source CMS1500 file not present", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

                if (oHCFA1500PaperForm != null) { oHCFA1500PaperForm.Dispose(); }
                if (oPrintForm != null) { oPrintForm.Dispose(); }

            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        public byte[] ConvertFileToBinary(string sFileName)
        {
            if (File.Exists(sFileName) == false)
                return null;

            try
            {
                FileStream oFile = default(FileStream);
                BinaryReader oReader = default(BinaryReader);

                //'Please uncomment the following line of code to read the file, even the file is in use by same or another process
                //oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous)

                //'To read the file only when it is not in use by any process
                oFile = new FileStream(sFileName, FileMode.Open, FileAccess.Read);

                oReader = new BinaryReader(oFile);
                byte[] bytesRead = oReader.ReadBytes(Convert.ToInt32(oFile.Length));


                oFile.Close();
                oReader.Close();
                oFile.Dispose();
                oReader.Dispose();
                oFile = null;
                oReader = null;

                return bytesRead;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
        }



        //New Function added to Print CMS form From EMR Clinical Chart
        public string Print(bool PrintOnForm, string FilePath, string sPrinterName = "Default", bool isForPrint = false)
        {
            string sReturnedPath = string.Empty;
            string tempPDFFolderPath = string.Empty;
            string tempPDFFilePath = string.Empty;

            try
            {
                _InputFilePath = Application.StartupPath.ToString() + "\\DLL\\CMS1500_NEW_WITH_BARCODE.jpg";

                tempPDFFolderPath = gloSettings.FolderSettings.AppTempFolderPath + "ConvertPDF";
                if (System.IO.Directory.Exists(tempPDFFolderPath) == false)
                { System.IO.Directory.CreateDirectory(tempPDFFolderPath); }
                tempPDFFilePath = tempPDFFolderPath + "\\" + DateTime.Now.ToString("yyyyMMddhhmmsstt") + ".pdf";


                gloHCFA1500PaperFormNew oHCFA1500PaperForm = null;
                if (PrintOnForm)
                {
                    _IsForPrintData = false;
                    oHCFA1500PaperForm = new gloHCFA1500PaperFormNew(true);
                    oHCFA1500PaperForm = SetHCFA1500PaperForm(oHCFA1500PaperForm);
                }
                else
                {
                    _IsForPrintData = true ;
                    oHCFA1500PaperForm = new gloHCFA1500PaperFormNew(sPrinterName);
                    oHCFA1500PaperForm = SetHCFA1500PaperForm(oHCFA1500PaperForm);
                }

                gloPrintPaperFormNew oPrintForm = new gloPrintPaperFormNew(_databaseconnectionstring);

                if (System.IO.File.Exists(_InputFilePath) == true)
                {



                    if (oHCFA1500PaperForm != null)
                    { _OutputFilePath = oPrintForm.PrintHCFA1500FormNew(oHCFA1500PaperForm, _InputFilePath, PrintOnForm, isForPrint); }

                    if (PrintOnForm == false)
                    {
                        clsPrintDocumentConversion oConversion = new clsPrintDocumentConversion(_databaseconnectionstring, sPrinterName, "CMS");
                        //_OutputFilePath = printdoc_HCFA1500_Conversion(oConversion);
                        _OutputFilePath = printdoc_HCFA1500_ConversionWithLesserResolution(oConversion, isForPrint);
                    }
                    if (!isForPrint)
                    {
                         _OutputFilePath = clsClinicalChartPrinting.ConvertTiffToPDF(_OutputFilePath, tempPDFFilePath, !PrintOnForm, true, false);
                    }
                    else if (!(gloGlobal.gloTSPrint.isCopyPrint && gloGlobal.gloTSPrint.UseEMFForClaims))
                    {
                        _OutputFilePath = clsClinicalChartPrinting.ConvertTiffToPDF(_OutputFilePath, tempPDFFilePath, !PrintOnForm, true, false);
                    }

                    if (System.IO.File.Exists(_OutputFilePath) == true)
                    {
                        sReturnedPath = _OutputFilePath;
                    }
                }
                else
                {
                    MessageBox.Show("Source CMS1500 file not present", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                if (oHCFA1500PaperForm != null) { oHCFA1500PaperForm.Dispose(); }
                if (oPrintForm != null) { oPrintForm.Dispose(); }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            return sReturnedPath;
        }

        //public string Print(bool PrintOnForm, string sPrinterName = "Default")
        //{
        //    string sReturnedPath = string.Empty;

        //    try
        //    {                
        //        _InputFilePath = Application.StartupPath.ToString() + "\\DLL\\CMS1500_NEW_WITH_BARCODE.jpg";


        //        gloHCFA1500PaperFormNew oHCFA1500PaperForm = null;
        //        if (PrintOnForm)
        //        {
        //            oHCFA1500PaperForm = new gloHCFA1500PaperFormNew(true);
        //        }
        //        else
        //        {
        //            oHCFA1500PaperForm = new gloHCFA1500PaperFormNew(sPrinterName);
        //        }

        //        gloPrintPaperFormNew oPrintForm = new gloPrintPaperFormNew();

        //        if (System.IO.File.Exists(_InputFilePath) == true)
        //        {
                
        //            oHCFA1500PaperForm = SetHCFA1500PaperForm(oHCFA1500PaperForm);

        //            if (oHCFA1500PaperForm != null)
        //            { _OutputFilePath = oPrintForm.PrintHCFA1500FormNew(oHCFA1500PaperForm, _InputFilePath, PrintOnForm); }

        //            if (System.IO.File.Exists(_OutputFilePath) == true)
        //            {
        //              sReturnedPath = _OutputFilePath;                    
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Source CMS1500 file not present", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }


        //        if (oHCFA1500PaperForm != null) { oHCFA1500PaperForm.Dispose(); }
        //        if (oPrintForm != null) { oPrintForm.Dispose(); }

                
        //    }
        //    catch //(Exception ex)
        //    {
        //        //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    return sReturnedPath;
        //}

        public void PrintForm(string FormName)
        {
           
            
            try 
            {
                _InputFilePath = Application.StartupPath.ToString() + "\\DLL\\CMS1500_NEW_WITH_BARCODE.jpg";

                // Below Line was updated by Pankaj 23122009 for PrintForm Setup
                // Passing True will indicate constructor to call InitializeBoxesForPrintForm() Method for PrintForm
                gloHCFA1500PaperFormNew oHCFA1500PaperForm = new gloHCFA1500PaperFormNew(true);

                gloPrintPaperFormNew oPrintForm = new gloPrintPaperFormNew(_databaseconnectionstring);
                
                if (System.IO.File.Exists(_InputFilePath) == true)
                {

                    // Below Line was updated by Pankaj 23122009 for PrintForm Setup
                    // Passing oHCFA1500PaperForm will call Overrided SetHCFA1500PaperForm(gloHCFA1500PaperForm oHCFA1500PaperForm) Method for PrintForm
                    _IsForPrintData = false;
                    oHCFA1500PaperForm = SetHCFA1500PaperForm(oHCFA1500PaperForm);

                    if (oHCFA1500PaperForm != null)
                    { _OutputFilePath = oPrintForm.PrintHCFA1500FormNew(oHCFA1500PaperForm, _InputFilePath, true,true); }

                    if (System.IO.File.Exists(_OutputFilePath) == true)
                    {
                        bool isPrinted = false;
                        if (gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            String convertedFile = _OutputFilePath;
                            if (!(gloGlobal.gloTSPrint.UseEMFForClaims))
                            {
                                String tempPDFFilePath = gloSettings.FolderSettings.AppTempFolderPath + DateTime.Now.ToString("yyyyMMddhhmmsstt") + ".pdf";
                                convertedFile = clsClinicalChartPrinting.ConvertTiffToPDF(convertedFile, tempPDFFilePath, false, true, false);
                            }
                            isPrinted = gloClinicalQueueFunctions.CopyPrintDoc(convertedFile, "CMS1500", "PrintFile");
                        }
                        if ( ! isPrinted)
                        {
                            System.Drawing.Printing.StandardPrintController oprintcontroller = new System.Drawing.Printing.StandardPrintController();
                            System.Drawing.Printing.PrinterSettings.PaperSizeCollection opapers = printdoc_HCFA1500.PrinterSettings.PaperSizes;

                            for (int i = 0; i <= opapers.Count - 1; i++)
                            {
                                if (opapers[i].PaperName.ToString().ToUpper() == "LETTER")
                                {
                                    printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PaperSize = opapers[i];
                                    break;
                                }
                            }
                            printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.Landscape = false;
                            printdoc_HCFA1500.PrintController = oprintcontroller;
                            printdoc_HCFA1500.Print();
                        }

                        #region "CODE FOR UPDATE THE PRINT CLAIM DATA"
                        //20100219 Mahesh Nawal
                        if (FormName == "Claimservice")
                        {
                           // gloDatabaseLayer.DBLayer oDB = null;
                         
                                   // oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                    System.Data.SqlClient.SqlConnection oConnection = new System.Data.SqlClient.SqlConnection();
                                    System.Data.SqlClient.SqlCommand UpdateCmd = null;
                                    //gloDatabaseLayer.DBParameters oDBParameters;
                                    try
                                    {

                                        //oDBParameters = new gloDatabaseLayer.DBParameters();
                                        //oDBParameters.Add("@nClaimno", oTransaction.ClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
                                        //oDBParameters.Add("@bIsprinted", 1, ParameterDirection.Input, SqlDbType.Bit);
                                        //oDBParameters.Add("@dtprinteddate", System.DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                                        //oDBParameters.Add("@iCMSFile", ConvertFileToBinary(_OutputFilePath), ParameterDirection.Input, SqlDbType.Image);


                                        //oDB.Connect(false);
                                        //oDB.Execute("BL_INUP_CMSEDI_PaperClaim_Update", oDBParameters);
                                        ////oDB.Execute_Query("Update BL_CMSEDI_Claim_Send set bIsPrinted=@bIsprinted,dtprinteddate=@dtprinteddate,iCMSFile=@iCMSFile where nCMSEDIClaimID=@nCMSEDIClaimID");


                                        oConnection.ConnectionString = _databaseconnectionstring;
                                        oConnection.Open();

                                        string updateSql = "Update BL_CMSEDI_Claim_Send WITH(READPAST) set bIsPrinted=@bIsprinted,dtprinteddate=@dtprinteddate,iCMSFile=@iCMSFile where nClaimNo=@nClaimno and sSubclaimNo=@sSubClaimno";
                                         UpdateCmd = new System.Data.SqlClient.SqlCommand(updateSql, oConnection);


                                        UpdateCmd.Parameters.Add("@nClaimno", SqlDbType.BigInt);

                                        UpdateCmd.Parameters.Add("@bIsprinted", SqlDbType.Bit);
                                        UpdateCmd.Parameters.Add("@iCMSFile", SqlDbType.Image);
                                        UpdateCmd.Parameters.Add("@dtprinteddate", SqlDbType.DateTime);
                                        UpdateCmd.Parameters.Add("@sSubClaimno", SqlDbType.VarChar);


                                        UpdateCmd.Parameters["@nClaimno"].Value = oTransaction.ClaimNo;
                                        UpdateCmd.Parameters["@sSubClaimno"].Value = oTransaction.SubClaimNo;
                                        UpdateCmd.Parameters["@bIsprinted"].Value = 1;
                                        UpdateCmd.Parameters["@dtprinteddate"].Value = System.DateTime.Now;
                                        UpdateCmd.Parameters["@iCMSFile"].Value = ConvertFileToBinary(_OutputFilePath);

                                        UpdateCmd.ExecuteNonQuery();
                                        oConnection.Close();

                                    }
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    }
                                    finally
                                    {


                                        if (UpdateCmd != null)
                                        {
                                            UpdateCmd.Parameters.Clear();
                                            UpdateCmd.Dispose();
                                            UpdateCmd = null;

                                        }

                                        if (oConnection != null)
                                        {
                                            oConnection.Close();
                                            oConnection.Dispose();
                                        }
                                      
                                    }


                        }
                        #endregion
                        //Delete File.

                       // File.Delete(_OutputFilePath);
                        //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.Print, "Print HCFA1500 Claim file : " + _OutputFilePath + "", _PatientID, _TransactionId, 0, gloAuditTrail.ActivityOutCome.Success);
                    }
                }
                else
                {
                    MessageBox.Show("Source CMS1500 file not present", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                if (oHCFA1500PaperForm != null) { oHCFA1500PaperForm.Dispose(); }
                if (oPrintForm != null) { oPrintForm.Dispose(); }


            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        public void markPrinted()
        {
            #region "CODE FOR UPDATE THE PRINT CLAIM DATA"
           
                System.Data.SqlClient.SqlConnection oConnection = new System.Data.SqlClient.SqlConnection();
                System.Data.SqlClient.SqlCommand UpdateCmd = null;
                try
                {

                    oConnection.ConnectionString = _databaseconnectionstring;
                    oConnection.Open();

                    string updateSql = "Update BL_CMSEDI_Claim_Send WITH(READPAST) set bIsPrinted=@bIsprinted,dtprinteddate=@dtprinteddate,iCMSFile=@iCMSFile where nClaimNo=@nClaimno and sSubclaimNo=@sSubClaimno";
                    UpdateCmd = new System.Data.SqlClient.SqlCommand(updateSql, oConnection);


                    UpdateCmd.Parameters.Add("@nClaimno", SqlDbType.BigInt);

                    UpdateCmd.Parameters.Add("@bIsprinted", SqlDbType.Bit);
                    UpdateCmd.Parameters.Add("@iCMSFile", SqlDbType.Image);
                    UpdateCmd.Parameters.Add("@dtprinteddate", SqlDbType.DateTime);
                    UpdateCmd.Parameters.Add("@sSubClaimno", SqlDbType.VarChar);


                    UpdateCmd.Parameters["@nClaimno"].Value = oTransaction.ClaimNo;
                    UpdateCmd.Parameters["@sSubClaimno"].Value = oTransaction.SubClaimNo;
                    UpdateCmd.Parameters["@bIsprinted"].Value = 1;
                    UpdateCmd.Parameters["@dtprinteddate"].Value = System.DateTime.Now;
                    UpdateCmd.Parameters["@iCMSFile"].Value = ConvertFileToBinary(_OutputFilePath);

                    UpdateCmd.ExecuteNonQuery();
                    oConnection.Close();

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {

                    if (UpdateCmd != null)
                    {
                        UpdateCmd.Parameters.Clear();
                        UpdateCmd.Dispose();
                        UpdateCmd = null;

                    }
                    if (oConnection != null)
                    {
                        oConnection.Close();
                        oConnection.Dispose();
                    }

                }


            
            #endregion
        }


        //private gloHCFA1500PaperFormNew SetHCFA1500PaperForm_ClinicalChart(gloHCFA1500PaperFormNew oHCFA1500)
        //{
        //    //gloHCFA1500PaperFormNew oHCFA1500 = null;
        //    //if (printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings != null)
        //    //{
        //    //    if (printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName != "")
        //    //    {
        //    //        oHCFA1500 = new gloHCFA1500PaperFormNew(printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName.ToString());
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    oHCFA1500 = new gloHCFA1500PaperFormNew();
        //    //}

        //    try
        //    {
        //        oHCFA1500.CF_Top_InsuranceHeader.Value = txtPayerNameAndAddress.Text;


        //        if (chkCHAMPVA.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_Champva.Value = true;
        //        }
        //        else if (chkFECABlackLung.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_FECA.Value = true;
        //        }
        //        else if (chkGroupHealthPlan.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_GroupHealthPlan.Value = true;
        //        }
        //        else if (chkOtherInsuranceType.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_Other.Value = true;
        //        }
        //        else if (chkMedicaid.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_Medicaid.Value = true;
        //        }
        //        else if (chkMedicare.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_Medicare.Value = true;
        //        }
        //        else if (chkTricareChampus.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_Tricare.Value = true;
        //        }

        //        if (chkPatientCoditionRelatedTo_AutoAccident_No.Checked == true)
        //        {
        //            oHCFA1500.CF_10_PatientConditionTo_AutoAccident_No.Value = true;
        //        }
        //        else if (chkPatientCoditionRelatedTo_AutoAccident_Yes.Checked == true)
        //        {
        //            oHCFA1500.CF_10_PatientConditionTo_AutoAccident_Yes.Value = true;
        //            oHCFA1500.CF_10_PatientConditionTo_AutoAccident_State.Value = txtPatientCoditionRelatedTo_State.Text.Trim();
        //        }

        //        if (chkPatientCoditionRelatedTo_Employment_No.Checked == true)
        //        {
        //            oHCFA1500.CF_10_PatientConditionTo_Employement_No.Value = true;
        //        }
        //        else if (chkPatientCoditionRelatedTo_Employment_Yes.Checked == true)
        //        {
        //            oHCFA1500.CF_10_PatientConditionTo_Employement_Yes.Value = true;
        //        }

        //        if (chkPatientCoditionRelatedTo_OtherAccident_No.Checked == true)
        //        {
        //            oHCFA1500.CF_10_PatientConditionTo_OtherAccident_No.Value = true;
        //        }
        //        else if (chkPatientCoditionRelatedTo_OtherAccident_Yes.Checked == true)
        //        {
        //            oHCFA1500.CF_10_PatientConditionTo_OtherAccident_Yes.Value = true;
        //            //oHCFA1500.CF_10_PatientConditionTo_AutoAccident_State.Value = txtPatientCoditionRelatedTo_State.Text.Trim();
        //        }

        //        //if (chkPatientStatus_Employed.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_8_PatientStatus_Employed.Value = true;
        //        //}
        //        //if (chkPatientStatus_FullTimeStudent.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_8_PatientStatus_FullTimeStudent.Value = true;
        //        //}
        //        //if (chkPatientStatus_Married.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_8_PatientStatus_Married.Value = true;
        //        //}
        //        //if (chkPatientStatus_Other.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_8_PatientStatus_Other.Value = true;
        //        //}
        //        //if (chkPatientStatus_PartTimeStudent.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_8_PatientStatus_PartTimeStudent.Value = true;
        //        //}
        //        //if (chkPatientStatus_Single.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_8_PatientStatus_Single.Value = true;
        //        //}
        //        if (chkRelationship_Child.Checked == true)
        //        {
        //            oHCFA1500.CF_6_PatientRelationship_Child.Value = true;
        //        }
        //        if (chkRelationship_Other.Checked == true)
        //        {
        //            oHCFA1500.CF_6_PatientRelationship_Other.Value = true;
        //        }
        //        if (chkRelationship_Self.Checked == true)
        //        {
        //            oHCFA1500.CF_6_PatientRelationship_Self.Value = true;
        //        }
        //        if (chkRelationship_Spouse.Checked == true)
        //        {
        //            oHCFA1500.CF_6_PatientRelationship_Spouse.Value = true;
        //        }

        //        oHCFA1500.CF_7_Insureds_Address.Value = txtInsuredsAddress.Text.Trim();
        //        oHCFA1500.CF_7_Insureds_City.Value = txtInsuredsCity.Text.Trim();
        //        oHCFA1500.CF_7_Insureds_State.Value = txtInsuredsState.Text.Trim();
        //        oHCFA1500.CF_7_Insureds_Tel_AreaCode.Value = txtInsuredTelephone1.Text.Trim();
        //        oHCFA1500.CF_7_Insureds_Tel_Number.Value = txtInsuredTelephone2.Text.Trim();
        //        oHCFA1500.CF_7_Insureds_Zip.Value = txtInsuredsZip.Text.Trim();
        //        if (dtpInsuredsDOB.Checked == true)
        //        {
        //            oHCFA1500.CF_11_Insureds_DOB_DD.Value = dtpInsuredsDOB.Value.ToString("dd");
        //            oHCFA1500.CF_11_Insureds_DOB_MM.Value = dtpInsuredsDOB.Value.ToString("MM");
        //            oHCFA1500.CF_11_Insureds_DOB_YY.Value = dtpInsuredsDOB.Value.ToString(_strYearFormat);
        //        }
        //        oHCFA1500.CF_11_Other_Claim_ID_Designated_by_NUCC.Value = txtInsuredEmployerORSchoolName.Text.Trim();
        //        oHCFA1500.CF_11_Insureds_InsuracnePlan.Value = txtInsuredInsurancePlanName.Text.Trim();
        //        if (chkIsOtherHealthPlan_No.Checked == true)
        //        {
        //            oHCFA1500.CF_11_Insureds_OtherHealthPlan_No.Value = true;
        //        }
        //        else if (chkIsOtherHealthPlan_Yes.Checked == true)
        //        {
        //            oHCFA1500.CF_11_Insureds_OtherHealthPlan_Yes.Value = true;
        //        }

        //        oHCFA1500.CF_11_Insureds_PolicyGroupNo.Value = txtInsuredPolicyorFECANo.Text.Trim();
        //        if (chkInsuredSex_Female.Checked == true)
        //        {
        //            oHCFA1500.CF_11_Insureds_Sex_Female.Value = true;
        //        }
        //        if (chkInsuredSex_Male.Checked == true)
        //        {
        //            oHCFA1500.CF_11_Insureds_Sex_Male.Value = true;
        //        }

        //        oHCFA1500.CF_11_Qualifier_No.Value = txtOtherClaimIDQualifier.Text;
        //        oHCFA1500.CF_1a_InsuredsIDNumber.Value = txtInsuredIdNumber.Text.Trim();
        //        oHCFA1500.CF_2_Patient_Name.Value = txtPatientName.Text.Trim();
        //        oHCFA1500.CF_3_Patient_DOB_DD.Value = dtpPatientDOB.Value.ToString("dd");
        //        oHCFA1500.CF_3_Patient_DOB_MM.Value = dtpPatientDOB.Value.ToString("MM");
        //        oHCFA1500.CF_3_Patient_DOB_YY.Value = dtpPatientDOB.Value.ToString(_strYearFormat);
        //        if (chkPatient_Female.Checked == true)
        //        {
        //            oHCFA1500.CF_3_Patient_Sex_Female.Value = true;
        //        }
        //        if (chkPatient_Male.Checked == true)
        //        {
        //            oHCFA1500.CF_3_Patient_Sex_Male.Value = true;
        //        }
        //        oHCFA1500.CF_4_Insureds_Name.Value = txtInsuredName.Text.Trim();

        //        oHCFA1500.CF_5_Patient_Address.Value = txtPatientAddress.Text.Trim();
        //        oHCFA1500.CF_5_Patient_City.Value = txtPatientCity.Text.Trim();
        //        oHCFA1500.CF_5_Patient_State.Value = txtPatientState.Text.Trim();
        //        oHCFA1500.CF_5_Patient_Tel_AreaCode.Value = txtPatientTelephone1.Text.Trim();
        //        oHCFA1500.CF_5_Patient_Tel_Number.Value = txtPatientTelephone2.Text.Trim();
        //        oHCFA1500.CF_5_Patient_Zip.Value = txtPatientZip.Text.Trim();

        //        //if (dtpOtherInsuredDOB.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_9_Other_Insureds_DOB_DD.Value = dtpOtherInsuredDOB.Value.ToString("dd");
        //        //    oHCFA1500.CF_9_Other_Insureds_DOB_MM.Value = dtpOtherInsuredDOB.Value.ToString("MM");
        //        //    oHCFA1500.CF_9_Other_Insureds_DOB_YY.Value = dtpOtherInsuredDOB.Value.ToString("yy");
        //        //}
        //        //oHCFA1500.CF_9_Other_Insureds_EmployerName.Value = txtOtherInsuredEmployerORSchoolName.Text.Trim();
        //        oHCFA1500.CF_9_Other_Insureds_InsuracnePlan.Value = txtOtherInsuredInsuranceName.Text.Trim();
        //        oHCFA1500.CF_9_Other_Insureds_Name.Value = txtOtherInsuredName.Text.Trim();
        //        oHCFA1500.CF_9_Other_Insureds_PolicyGroupNo.Value = txtOtherInsuredPolicyNo.Text.Trim();

        //        //if (chkOtherInsuredSex_Female.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_9_Other_Insureds_Sex_Female.Value = true;
        //        //}
        //        //if (chkOtherInsuredSex_Male.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_9_Other_Insureds_Sex_Male.Value = true;
        //        //}
        //        // oHCFA1500.CF_10_PatientConditionTo_ResForLocaluse.Value = txtReserveForLocalUse.Text.Trim();
        //        oHCFA1500.CF_12_PatientAuthorizedPersons_Signature.Value = txtPatientSignature.Text.Trim();
        //        if (dtpPatientSignDate.Checked == true)
        //        {
        //            oHCFA1500.CF_12_PatientAuthorizedPersons_Signature_Date.Value = dtpPatientSignDate.Value.ToString(_strWholeDateFormat);
        //        }
        //        oHCFA1500.CF_13_InsuredsAuthorizedPersons_Signature.Value = txtInsuredPersonSign.Text.Trim();

        //        if (dtpDateOfCurrentIllness.Checked == true)
        //        {
        //            oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_DD.Value = dtpDateOfCurrentIllness.Value.ToString("dd");
        //            oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_MM.Value = dtpDateOfCurrentIllness.Value.ToString("MM");
        //            oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_YY.Value = dtpDateOfCurrentIllness.Value.ToString(_strYearFormat);
        //        }

        //        oHCFA1500.CF_14_Qualifier_No.Value = txtDateOfCurrentIllnessQualifier.Text;
        //        if (dtpSimilarIllnessFirstDate.Checked == true)
        //        {
        //            oHCFA1500.CF_15_Other_Date_DD.Value = dtpSimilarIllnessFirstDate.Value.ToString("dd");
        //            oHCFA1500.CF_15_Other_Dates_MM.Value = dtpSimilarIllnessFirstDate.Value.ToString("MM");
        //            oHCFA1500.CF_15_Other_Date_YY.Value = dtpSimilarIllnessFirstDate.Value.ToString(_strYearFormat);
        //        }

        //        oHCFA1500.CF_15_Qualifier_No.Value = txtSimilarIllnessFirstDateQualifier.Text;

        //        if (dtpUnableToWorkFrom.Checked == true)
        //        {
        //            oHCFA1500.CF_16_UnableToWorkFromDate_DD.Value = dtpUnableToWorkFrom.Value.ToString("dd");
        //            oHCFA1500.CF_16_UnableToWorkFromDate_MM.Value = dtpUnableToWorkFrom.Value.ToString("MM");
        //            oHCFA1500.CF_16_UnableToWorkFromDate_YY.Value = dtpUnableToWorkFrom.Value.ToString(_strYearFormat);
        //        }
        //        if (dtpUnableToWorkTill.Checked == true)
        //        {
        //            oHCFA1500.CF_16_UnableToWorkTillDate_DD.Value = dtpUnableToWorkTill.Value.ToString("dd");
        //            oHCFA1500.CF_16_UnableToWorkTillDate_MM.Value = dtpUnableToWorkTill.Value.ToString("MM");
        //            oHCFA1500.CF_16_UnableToWorkTillDate_YY.Value = dtpUnableToWorkTill.Value.ToString(_strYearFormat);
        //        }



        //        oHCFA1500.CF_17_ReferringProvider_Name.Value = txtReferringProviderName.Text.Trim();
        //        oHCFA1500.CF_17_ReferringProvider_Qaulifier.Value = txtReferringProviderQualifier.Text.Trim();
        //        oHCFA1500.CF_17a_ReferringProvider_OtherQualifier.Value = txtReferringProvider_OtherType.Text.Trim();
        //        oHCFA1500.CF_17a_ReferringProvider_OtherID.Value = txtReferringProvider_OtherValue.Text.Trim();
        //        oHCFA1500.CF_17b_ReferringProvider_NPI.Value = txtReferringProvider_NPI.Text.Trim();


        //        if (dtpHospitalisationFrom.Checked == true)
        //        {
        //            oHCFA1500.CF_18_HospitalizationFromDate_DD.Value = dtpHospitalisationFrom.Value.ToString("dd");
        //            oHCFA1500.CF_18_HospitalizationFromDate_MM.Value = dtpHospitalisationFrom.Value.ToString("MM");
        //            oHCFA1500.CF_18_HospitalizationFromDate_YY.Value = dtpHospitalisationFrom.Value.ToString(_strYearFormat);
        //        }
        //        if (dtpHospitalisationTo.Checked == true)
        //        {

        //            oHCFA1500.CF_18_HospitalizationTillDate_DD.Value = dtpHospitalisationTo.Value.ToString("dd");
        //            oHCFA1500.CF_18_HospitalizationTillDate_MM.Value = dtpHospitalisationTo.Value.ToString("MM");
        //            oHCFA1500.CF_18_HospitalizationTillDate_YY.Value = dtpHospitalisationTo.Value.ToString(_strYearFormat);
        //        }


        //        oHCFA1500.CF_19_LocalUse_Field.Value = txt19ReservedForLocalUse.Text.Trim();
        //        oHCFA1500.CF_20_OutsideLab_Charges_Principal.Value = txtOutsideLabCharges1.Text.Trim();
        //        oHCFA1500.CF_20_OutsideLab_Charges_Secondary.Value = txtOutsideLabCharges2.Text.Trim();
        //        if (chkOutsideLab_No.Checked == true)
        //        {
        //            oHCFA1500.CF_20_OutsideLab_No.Value = true;
        //        }
        //        if (chkOutsideLab_Yes.Checked == true)
        //        {
        //            oHCFA1500.CF_20_OutsideLab_Yes.Value = true;
        //        }

        //        oHCFA1500.CF_21_Icd_Ind_No.Value = txtIcdInd1.Text;
        //        oHCFA1500.CF_21_Icd_Ind.Value = txtIcdInd2.Text;
        //        oHCFA1500.CF_21_Diagnosis_A_Principal.Value = txtDiagnosisCode11.Text;
        //        // oHCFA1500.CF_21_Diagnosis_1_Secondary.Value = txtDiagnosisCode12.Text;

        //        oHCFA1500.CF_21_Diagnosis_B_Principal.Value = txtDiagnosisCode21.Text;
        //        //  oHCFA1500.CF_21_Diagnosis_2_Secondary.Value = txtDiagnosisCode32.Text;

        //        oHCFA1500.CF_21_Diagnosis_C_Principal.Value = txtDiagnosisCode31.Text;
        //        //    oHCFA1500.CF_21_Diagnosis_3_Secondary.Value = txtDiagnosisCode22.Text;

        //        oHCFA1500.CF_21_Diagnosis_D_Principal.Value = txtDiagnosisCode41.Text;
        //        //    oHCFA1500.CF_21_Diagnosis_4_Secondary.Value = txtDiagnosisCode42.Text;

        //        oHCFA1500.CF_21_Diagnosis_E_Principal.Value = txtDiagnosisCodeE.Text;
        //        oHCFA1500.CF_21_Diagnosis_F_Principal.Value = txtDiagnosisCodeF.Text;
        //        oHCFA1500.CF_21_Diagnosis_G_Principal.Value = txtDiagnosisCodeG.Text;
        //        oHCFA1500.CF_21_Diagnosis_H_Principal.Value = txtDiagnosisCodeH.Text;
        //        oHCFA1500.CF_21_Diagnosis_I_Principal.Value = txtDiagnosisCodeI.Text;
        //        oHCFA1500.CF_21_Diagnosis_J_Principal.Value = txtDiagnosisCodeJ.Text;
        //        oHCFA1500.CF_21_Diagnosis_K_Principal.Value = txtDiagnosisCodeK.Text;
        //        oHCFA1500.CF_21_Diagnosis_L_Principal.Value = txtDiagnosisCodeL.Text;

        //        oHCFA1500.CF_22_Resubmission.Value = txtMedicaidResubmissionCode.Text.Trim();
        //        oHCFA1500.CF_22_Original_Refrence_No.Value = txtOriginalRefNumber.Text.Trim();
        //        oHCFA1500.CF_23_PriorAuthorization_No.Value = txtPriorAuthorizationNumber.Text.Trim();


        //        oHCFA1500.CF_24A_L1_DOS_From_DD.Value = dtpDOS1From.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L1_DOS_From_MM.Value = dtpDOS1From.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L1_DOS_From_YY.Value = dtpDOS1From.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L1_DOS_To_DD.Value = dtpDOS1To.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L1_DOS_To_MM.Value = dtpDOS1To.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L1_DOS_To_YY.Value = dtpDOS1To.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L2_DOS_From_DD.Value = dtpDOS2From.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L2_DOS_From_MM.Value = dtpDOS2From.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L2_DOS_From_YY.Value = dtpDOS2From.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L2_DOS_To_DD.Value = dtpDOS2To.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L2_DOS_To_MM.Value = dtpDOS2To.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L2_DOS_To_YY.Value = dtpDOS2To.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L3_DOS_From_DD.Value = dtpDOS3From.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L3_DOS_From_MM.Value = dtpDOS3From.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L3_DOS_From_YY.Value = dtpDOS3From.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L3_DOS_To_DD.Value = dtpDOS3To.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L3_DOS_To_MM.Value = dtpDOS3To.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L3_DOS_To_YY.Value = dtpDOS3To.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L4_DOS_From_DD.Value = dtpDOS4From.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L4_DOS_From_MM.Value = dtpDOS4From.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L4_DOS_From_YY.Value = dtpDOS4From.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L4_DOS_To_DD.Value = dtpDOS4To.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L4_DOS_To_MM.Value = dtpDOS4To.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L4_DOS_To_YY.Value = dtpDOS4To.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L5_DOS_From_DD.Value = dtpDOS5From.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L5_DOS_From_MM.Value = dtpDOS5From.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L5_DOS_From_YY.Value = dtpDOS5From.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L5_DOS_To_DD.Value = dtpDOS5To.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L5_DOS_To_MM.Value = dtpDOS5To.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L5_DOS_To_YY.Value = dtpDOS5To.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L6_DOS_From_DD.Value = dtpDOS6From.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L6_DOS_From_MM.Value = dtpDOS6From.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L6_DOS_From_YY.Value = dtpDOS6From.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L6_DOS_To_DD.Value = dtpDOS6To.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L6_DOS_To_MM.Value = dtpDOS6To.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L6_DOS_To_YY.Value = dtpDOS6To.Value.ToString("yy");


        //        oHCFA1500.CF_24B_L1_POS_Code.Value = txtPOS1.Text;
        //        oHCFA1500.CF_24B_L2_POS_Code.Value = txtPOS2.Text;
        //        oHCFA1500.CF_24B_L3_POS_Code.Value = txtPOS3.Text;
        //        oHCFA1500.CF_24B_L4_POS_Code.Value = txtPOS4.Text;
        //        oHCFA1500.CF_24B_L5_POS_Code.Value = txtPOS5.Text;
        //        oHCFA1500.CF_24B_L6_POS_Code.Value = txtPOS6.Text;


        //        oHCFA1500.CF_24C_L1_EMG_Code.Value = txtEMG1.Text;
        //        oHCFA1500.CF_24C_L2_EMG_Code.Value = txtEMG2.Text;
        //        oHCFA1500.CF_24C_L3_EMG_Code.Value = txtEMG3.Text;
        //        oHCFA1500.CF_24C_L4_EMG_Code.Value = txtEMG4.Text;
        //        oHCFA1500.CF_24C_L5_EMG_Code.Value = txtEMG5.Text;
        //        oHCFA1500.CF_24C_L6_EMG_Code.Value = txtEMG6.Text;


        //        oHCFA1500.CF_24D_L1_CPT_HCPCS_Code.Value = txtCPT1.Text;
        //        oHCFA1500.CF_24D_L2_CPT_HCPCS_Code.Value = txtCPT2.Text;
        //        oHCFA1500.CF_24D_L3_CPT_HCPCS_Code.Value = txtCPT3.Text;
        //        oHCFA1500.CF_24D_L4_CPT_HCPCS_Code.Value = txtCPT4.Text;
        //        oHCFA1500.CF_24D_L5_CPT_HCPCS_Code.Value = txtCPT5.Text;
        //        oHCFA1500.CF_24D_L6_CPT_HCPCS_Code.Value = txtCPT6.Text;

        //        if (txtCPT1.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line1 = true; }
        //        if (txtCPT2.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line2 = true; }
        //        if (txtCPT3.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line3 = true; }
        //        if (txtCPT4.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line4 = true; }
        //        if (txtCPT5.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line5 = true; }
        //        if (txtCPT6.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line6 = true; }

        //        oHCFA1500.CF_24D_L1_Modifier_1_Code.Value = txtMOD11.Text;
        //        oHCFA1500.CF_24D_L1_Modifier_2_Code.Value = txtMOD12.Text;
        //        oHCFA1500.CF_24D_L1_Modifier_3_Code.Value = txtMOD13.Text;
        //        oHCFA1500.CF_24D_L1_Modifier_4_Code.Value = txtMOD14.Text;
        //        oHCFA1500.CF_24D_L2_Modifier_1_Code.Value = txtMOD21.Text;
        //        oHCFA1500.CF_24D_L2_Modifier_2_Code.Value = txtMOD22.Text;
        //        oHCFA1500.CF_24D_L2_Modifier_3_Code.Value = txtMOD23.Text;
        //        oHCFA1500.CF_24D_L2_Modifier_4_Code.Value = txtMOD24.Text;
        //        oHCFA1500.CF_24D_L3_Modifier_1_Code.Value = txtMOD31.Text;
        //        oHCFA1500.CF_24D_L3_Modifier_2_Code.Value = txtMOD32.Text;
        //        oHCFA1500.CF_24D_L3_Modifier_3_Code.Value = txtMOD33.Text;
        //        oHCFA1500.CF_24D_L3_Modifier_4_Code.Value = txtMOD34.Text;
        //        oHCFA1500.CF_24D_L4_Modifier_1_Code.Value = txtMOD41.Text;
        //        oHCFA1500.CF_24D_L4_Modifier_2_Code.Value = txtMOD42.Text;
        //        oHCFA1500.CF_24D_L4_Modifier_3_Code.Value = txtMOD43.Text;
        //        oHCFA1500.CF_24D_L4_Modifier_4_Code.Value = txtMOD44.Text;
        //        oHCFA1500.CF_24D_L5_Modifier_1_Code.Value = txtMOD51.Text;
        //        oHCFA1500.CF_24D_L5_Modifier_2_Code.Value = txtMOD52.Text;
        //        oHCFA1500.CF_24D_L5_Modifier_3_Code.Value = txtMOD53.Text;
        //        oHCFA1500.CF_24D_L5_Modifier_4_Code.Value = txtMOD54.Text;
        //        oHCFA1500.CF_24D_L6_Modifier_1_Code.Value = txtMOD61.Text;
        //        oHCFA1500.CF_24D_L6_Modifier_2_Code.Value = txtMOD62.Text;
        //        oHCFA1500.CF_24D_L6_Modifier_3_Code.Value = txtMOD63.Text;
        //        oHCFA1500.CF_24D_L6_Modifier_4_Code.Value = txtMOD64.Text;

        //        oHCFA1500.CF_24E_L1_Diagnosis_Pointers.Value = txtDxPtr1.Text;
        //        oHCFA1500.CF_24E_L2_Diagnosis_Pointers.Value = txtDxPtr2.Text;
        //        oHCFA1500.CF_24E_L3_Diagnosis_Pointers.Value = txtDxPtr3.Text;
        //        oHCFA1500.CF_24E_L4_Diagnosis_Pointers.Value = txtDxPtr4.Text;
        //        oHCFA1500.CF_24E_L5_Diagnosis_Pointers.Value = txtDxPtr5.Text;
        //        oHCFA1500.CF_24E_L6_Diagnosis_Pointers.Value = txtDxPtr6.Text;


        //        oHCFA1500.CF_24F_L1_Charges_Principal.Value = txtCharges1.Text;
        //        oHCFA1500.CF_24F_L1_Charges_Secondary.Value = txtCharges11.Text;
        //        oHCFA1500.CF_24F_L2_Charges_Principal.Value = txtCharges2.Text;
        //        oHCFA1500.CF_24F_L2_Charges_Secondary.Value = txtCharges21.Text;
        //        oHCFA1500.CF_24F_L3_Charges_Principal.Value = txtCharges3.Text;
        //        oHCFA1500.CF_24F_L3_Charges_Secondary.Value = txtCharges31.Text;
        //        oHCFA1500.CF_24F_L4_Charges_Principal.Value = txtCharges4.Text;
        //        oHCFA1500.CF_24F_L4_Charges_Secondary.Value = txtCharges41.Text;
        //        oHCFA1500.CF_24F_L5_Charges_Principal.Value = txtCharges5.Text;
        //        oHCFA1500.CF_24F_L5_Charges_Secondary.Value = txtCharges51.Text;
        //        oHCFA1500.CF_24F_L6_Charges_Principal.Value = txtCharges6.Text;
        //        oHCFA1500.CF_24F_L6_Charges_Secondary.Value = txtCharges61.Text;

        //        oHCFA1500.CF_24G_L1_Days_Units.Value = txtUnits1.Text;
        //        oHCFA1500.CF_24G_L2_Days_Units.Value = txtUnits2.Text;
        //        oHCFA1500.CF_24G_L3_Days_Units.Value = txtUnits3.Text;
        //        oHCFA1500.CF_24G_L4_Days_Units.Value = txtUnits4.Text;
        //        oHCFA1500.CF_24G_L5_Days_Units.Value = txtUnits5.Text;
        //        oHCFA1500.CF_24G_L6_Days_Units.Value = txtUnits6.Text;


        //        oHCFA1500.CF_24H_L1_EPSDT_FamilyPlan_Shaded.Value = txtEPSDTShaded1.Text.ToString().Length > 2 ? txtEPSDTShaded1.Text.ToString().Substring(0, 2) : txtEPSDTShaded1.Text.ToString();
        //        oHCFA1500.CF_24H_L2_EPSDT_FamilyPlan_Shaded.Value = txtEPSDTShaded2.Text.ToString().Length > 2 ? txtEPSDTShaded2.Text.ToString().Substring(0, 2) : txtEPSDTShaded2.Text.ToString();
        //        oHCFA1500.CF_24H_L3_EPSDT_FamilyPlan_Shaded.Value = txtEPSDTShaded3.Text.ToString().Length > 2 ? txtEPSDTShaded3.Text.ToString().Substring(0, 2) : txtEPSDTShaded3.Text.ToString();
        //        oHCFA1500.CF_24H_L4_EPSDT_FamilyPlan_Shaded.Value = txtEPSDTShaded4.Text.ToString().Length > 2 ? txtEPSDTShaded4.Text.ToString().Substring(0, 2) : txtEPSDTShaded4.Text.ToString();
        //        oHCFA1500.CF_24H_L5_EPSDT_FamilyPlan_Shaded.Value = txtEPSDTShaded5.Text.ToString().Length > 2 ? txtEPSDTShaded5.Text.ToString().Substring(0, 2) : txtEPSDTShaded5.Text.ToString();
        //        oHCFA1500.CF_24H_L6_EPSDT_FamilyPlan_Shaded.Value = txtEPSDTShaded6.Text.ToString().Length > 2 ? txtEPSDTShaded6.Text.ToString().Substring(0, 2) : txtEPSDTShaded6.Text.ToString();

        //        oHCFA1500.CF_24H_L1_EPSDT_FamilyPlan.Value = txtEPSDT1.Text.ToString().Length > 2 ? txtEPSDT1.Text.ToString().Substring(0, 2) : txtEPSDT1.Text.ToString();
        //        oHCFA1500.CF_24H_L2_EPSDT_FamilyPlan.Value = txtEPSDT2.Text.ToString().Length > 2 ? txtEPSDT2.Text.ToString().Substring(0, 2) : txtEPSDT2.Text.ToString();
        //        oHCFA1500.CF_24H_L3_EPSDT_FamilyPlan.Value = txtEPSDT3.Text.ToString().Length > 2 ? txtEPSDT3.Text.ToString().Substring(0, 2) : txtEPSDT3.Text.ToString();
        //        oHCFA1500.CF_24H_L4_EPSDT_FamilyPlan.Value = txtEPSDT4.Text.ToString().Length > 2 ? txtEPSDT4.Text.ToString().Substring(0, 2) : txtEPSDT4.Text.ToString();
        //        oHCFA1500.CF_24H_L5_EPSDT_FamilyPlan.Value = txtEPSDT5.Text.ToString().Length > 2 ? txtEPSDT5.Text.ToString().Substring(0, 2) : txtEPSDT5.Text.ToString();
        //        oHCFA1500.CF_24H_L6_EPSDT_FamilyPlan.Value = txtEPSDT6.Text.ToString().Length > 2 ? txtEPSDT6.Text.ToString().Substring(0, 2) : txtEPSDT6.Text.ToString();

        //        oHCFA1500.CF_24J_L1_RenderingProvider_NPI.Value = txtRenderingProvider1_NPI.Text;
        //        oHCFA1500.CF_24J_L2_RenderingProvider_NPI.Value = txtRenderingProvider2_NPI.Text;
        //        oHCFA1500.CF_24J_L3_RenderingProvider_NPI.Value = txtRenderingProvider3_NPI.Text;
        //        oHCFA1500.CF_24J_L4_RenderingProvider_NPI.Value = txtRenderingProvider4_NPI.Text;
        //        oHCFA1500.CF_24J_L5_RenderingProvider_NPI.Value = txtRenderingProvider5_NPI.Text;
        //        oHCFA1500.CF_24J_L6_RenderingProvider_NPI.Value = txtRenderingProvider6_NPI.Text;


        //        oHCFA1500.CF_24J_L1_RenderingProvider_OtherQualifier.Value = txtRenderingProvider1_Qualifier.Text;
        //        oHCFA1500.CF_24J_L2_RenderingProvider_OtherQualifier.Value = txtRenderingProvider2_Qualifier.Text;
        //        oHCFA1500.CF_24J_L3_RenderingProvider_OtherQualifier.Value = txtRenderingProvider3_Qualifier.Text;
        //        oHCFA1500.CF_24J_L4_RenderingProvider_OtherQualifier.Value = txtRenderingProvider4_Qualifier.Text;
        //        oHCFA1500.CF_24J_L5_RenderingProvider_OtherQualifier.Value = txtRenderingProvider5_Qualifier.Text;
        //        oHCFA1500.CF_24J_L6_RenderingProvider_OtherQualifier.Value = txtRenderingProvider6_Qualifier.Text;

        //        oHCFA1500.CF_24J_L1_RenderingProvider_OtherQualifiervalue.Value = txtRenderingProvider1_QualifierValue.Text;
        //        oHCFA1500.CF_24J_L2_RenderingProvider_OtherQualifiervalue.Value = txtRenderingProvider2_QualifierValue.Text;
        //        oHCFA1500.CF_24J_L3_RenderingProvider_OtherQualifiervalue.Value = txtRenderingProvider3_QualifierValue.Text;
        //        oHCFA1500.CF_24J_L4_RenderingProvider_OtherQualifiervalue.Value = txtRenderingProvider4_QualifierValue.Text;
        //        oHCFA1500.CF_24J_L5_RenderingProvider_OtherQualifiervalue.Value = txtRenderingProvider5_QualifierValue.Text;
        //        oHCFA1500.CF_24J_L6_RenderingProvider_OtherQualifiervalue.Value = txtRenderingProvider6_QualifierValue.Text;



        //        oHCFA1500.CF_24A_L1_Note.Value = txtNotes1.Text.Trim();
        //        oHCFA1500.CF_24A_L2_Note.Value = txtNotes2.Text.Trim();
        //        oHCFA1500.CF_24A_L3_Note.Value = txtNotes3.Text.Trim();
        //        oHCFA1500.CF_24A_L4_Note.Value = txtNotes4.Text.Trim();
        //        oHCFA1500.CF_24A_L5_Note.Value = txtNotes5.Text.Trim();
        //        oHCFA1500.CF_24A_L6_Note.Value = txtNotes6.Text.Trim();

        //        oHCFA1500.CF_25_FederalTax_ID_No.Value = txtFederalTaxID.Text;
        //        if (chkFederalTaxID_EIN.Checked == true)
        //        {
        //            oHCFA1500.CF_25_FederalTaxID_Qualifier_EIN.Value = true;
        //        }
        //        if (chkFederalTaxID_SSN.Checked == true)
        //        {
        //            oHCFA1500.CF_25_FederalTaxID_Qualifier_SSN.Value = true;
        //        }

        //        oHCFA1500.CF_26_PatientAccount_No.Value = txtPatientAccountNo.Text;
        //        if (chkAcceptAssignment_No.Checked == true)
        //        {
        //            oHCFA1500.CF_27_AcceptAssignment_NO.Value = true;
        //        }
        //        if (chkAcceptAssignment_Yes.Checked == true)
        //        {
        //            oHCFA1500.CF_27_AcceptAssignment_YES.Value = true;
        //        }

        //        oHCFA1500.CF_28_TotalCharge_Principal.Value = txtTotalCharges.Text;
        //        oHCFA1500.CF_28_TotalCharge_Secondary.Value = txtTotalCharges2.Text;
        //        oHCFA1500.CF_29_AmountPaid_Principal.Value = txtAmountPaid.Text;
        //        oHCFA1500.CF_29_AmountPaid_Secondary.Value = txtAmountPaid2.Text;
        //        //oHCFA1500.CF_30_BalanceDue_Principal.Value = txtBalanceDue.Text;
        //        //oHCFA1500.CF_30_BalanceDue_Secondary.Value = txtBalanceDue2.Text;

        //        oHCFA1500.CF_31_Physician_Supplier_Signature.Value = txtPhyscianSignature.Text;
        //        oHCFA1500.CF_31_Physician_Supplier_Signature_Date.Value = dtpPhysicianSignDate.Value.ToString(_strWholeDateFormat);
        //        oHCFA1500.CF_31_Physician_Supplier_QualifierValue.Value = txtPhyscianQualifierValue.Text;

        //        string[] FacilityInfo = txtFacilityInfo.Text.Split('\n');

        //        //Commented and Added on 27th Nov 2010
        //        //if (FacilityInfo.Length >= 3)
        //        //{
        //        //    oHCFA1500.CF_32_Service_Facility_Name.Value = FacilityInfo[0];
        //        //    oHCFA1500.CF_32_Service_Facility_Address_Line1.Value = FacilityInfo[1];
        //        //    oHCFA1500.CF_32_Service_Facility_Address_Line2.Value = FacilityInfo[2];
        //        //}

        //        if (FacilityInfo.Length > 0)
        //        {
        //            //if (FacilityInfo.Length >= 3)
        //            //{
        //            oHCFA1500.CF_32_Service_Facility_Name.Value = FacilityInfo[0];
        //            if (FacilityInfo.Length > 1)
        //            {
        //                oHCFA1500.CF_32_Service_Facility_Address_Line1.Value = FacilityInfo[1];
        //            }
        //            if (FacilityInfo.Length > 2)
        //            {
        //                oHCFA1500.CF_32_Service_Facility_Address_Line2.Value = FacilityInfo[2];
        //            }
        //        }

        //        //**

        //        oHCFA1500.CF_32a_Service_Facility_NPI.Value = txtFacility_a_NPI.Text;
        //        oHCFA1500.CF_32b_Service_Facility_UPIN_OtherID.Value = txtFacility_b.Text;

        //        string[] BillingProviderInfo = txtBillingProviderInfo.Text.Split('\n');

        //        //Commented and Added on 27th Nov 2010

        //        //if (BillingProviderInfo.Length >= 3)
        //        //{
        //        //    oHCFA1500.CF_33_BillingProvider_Name.Value = BillingProviderInfo[0];
        //        //    oHCFA1500.CF_33_BillingProvider_Address_Line1.Value = BillingProviderInfo[1];
        //        //    oHCFA1500.CF_33_BillingProvider_Address_Line2.Value = BillingProviderInfo[2];
        //        //}

        //        if (BillingProviderInfo.Length > 0)
        //        {
        //            //if (FacilityInfo.Length >= 3)
        //            //{
        //            oHCFA1500.CF_33_BillingProvider_Name.Value = BillingProviderInfo[0];
        //            if (BillingProviderInfo.Length > 1)
        //            {
        //                oHCFA1500.CF_33_BillingProvider_Address_Line1.Value = BillingProviderInfo[1];
        //            }
        //            if (BillingProviderInfo.Length > 2)
        //            {
        //                oHCFA1500.CF_33_BillingProvider_Address_Line2.Value = BillingProviderInfo[2];
        //            }
        //        }

        //        //**

        //        oHCFA1500.CF_33_BillingProvider_Tel_AreaCode.Value = txtBillingProviderPhone1.Text;
        //        oHCFA1500.CF_33_BillingProvider_Tel_Number.Value = txtBillingProviderPhone2.Text;
        //        oHCFA1500.CF_33a_BillingProvider_NPI.Value = txtBillingProv_a_NPI.Text;
        //        oHCFA1500.CF_33b_BillingProvider_UPIN_OtherID.Value = txtBillingProv_b_UPIN.Text;


        //    }
        //    catch //(Exception ex)
        //    {
        //        //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

        //        oHCFA1500 = null;
        //    }
        //    finally
        //    {
        //    }
        //    return oHCFA1500;
        //}

        //private gloHCFA1500PaperFormNew SetHCFA1500PaperForm()
        //{
        //   gloHCFA1500PaperFormNew oHCFA1500 = null;
        //    if (printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings != null)
        //    {
        //        if (printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName != "")
        //        {
        //            oHCFA1500 = new gloHCFA1500PaperFormNew(printdoc_HCFA1500.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName.ToString());
        //        }
        //    }
        //    else
        //    {
        //        oHCFA1500 = new gloHCFA1500PaperFormNew();
        //    }

        //    try
        //    {
        //        oHCFA1500.CF_Top_InsuranceHeader.Value = txtPayerNameAndAddress.Text;


        //        if (chkCHAMPVA.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_Champva.Value = true;
        //        }
        //        else if (chkFECABlackLung.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_FECA.Value = true;
        //        }
        //        else if (chkGroupHealthPlan.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_GroupHealthPlan.Value = true;
        //        }
        //        else if (chkOtherInsuranceType.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_Other.Value = true;
        //        }
        //        else if (chkMedicaid.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_Medicaid.Value = true;
        //        }
        //        else if (chkMedicare.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_Medicare.Value = true;
        //        }
        //        else if (chkTricareChampus.Checked == true)
        //        {
        //            oHCFA1500.CF_1_Insuracne_Type_Tricare.Value = true;
        //        }

        //        if (chkPatientCoditionRelatedTo_AutoAccident_No.Checked == true)
        //        {
        //            oHCFA1500.CF_10_PatientConditionTo_AutoAccident_No.Value = true;
        //        }
        //        else if (chkPatientCoditionRelatedTo_AutoAccident_Yes.Checked == true)
        //        {
        //            oHCFA1500.CF_10_PatientConditionTo_AutoAccident_Yes.Value = true;
        //            oHCFA1500.CF_10_PatientConditionTo_AutoAccident_State.Value = txtPatientCoditionRelatedTo_State.Text.Trim();
        //        }

        //        if (chkPatientCoditionRelatedTo_Employment_No.Checked == true)
        //        {
        //            oHCFA1500.CF_10_PatientConditionTo_Employement_No.Value = true;
        //        }
        //        else if (chkPatientCoditionRelatedTo_Employment_Yes.Checked == true)
        //        {
        //            oHCFA1500.CF_10_PatientConditionTo_Employement_Yes.Value = true;
        //        }

        //        if (chkPatientCoditionRelatedTo_OtherAccident_No.Checked == true)
        //        {
        //            oHCFA1500.CF_10_PatientConditionTo_OtherAccident_No.Value = true;
        //        }
        //        else if (chkPatientCoditionRelatedTo_OtherAccident_Yes.Checked == true)
        //        {
        //            oHCFA1500.CF_10_PatientConditionTo_OtherAccident_Yes.Value = true;
        //            //oHCFA1500.CF_10_PatientConditionTo_AutoAccident_State.Value = txtPatientCoditionRelatedTo_State.Text.Trim();
        //        }

        //        //if (chkPatientStatus_Employed.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_8_PatientStatus_Employed.Value = true;
        //        //}
        //        //if (chkPatientStatus_FullTimeStudent.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_8_PatientStatus_FullTimeStudent.Value = true;
        //        //}
        //        //if (chkPatientStatus_Married.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_8_PatientStatus_Married.Value = true;
        //        //}
        //        //if (chkPatientStatus_Other.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_8_PatientStatus_Other.Value = true;
        //        //}
        //        //if (chkPatientStatus_PartTimeStudent.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_8_PatientStatus_PartTimeStudent.Value = true;
        //        //}
        //        //if (chkPatientStatus_Single.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_8_PatientStatus_Single.Value = true;
        //        //}
        //        if (chkRelationship_Child.Checked == true)
        //        {
        //            oHCFA1500.CF_6_PatientRelationship_Child.Value = true;
        //        }
        //        if (chkRelationship_Other.Checked == true)
        //        {
        //            oHCFA1500.CF_6_PatientRelationship_Other.Value = true;
        //        }
        //        if (chkRelationship_Self.Checked == true)
        //        {
        //            oHCFA1500.CF_6_PatientRelationship_Self.Value = true;
        //        }
        //        if (chkRelationship_Spouse.Checked == true)
        //        {
        //            oHCFA1500.CF_6_PatientRelationship_Spouse.Value = true;
        //        }

        //        oHCFA1500.CF_7_Insureds_Address.Value = txtInsuredsAddress.Text.Trim();
        //        oHCFA1500.CF_7_Insureds_City.Value = txtInsuredsCity.Text.Trim();
        //        oHCFA1500.CF_7_Insureds_State.Value = txtInsuredsState.Text.Trim();
        //        oHCFA1500.CF_7_Insureds_Tel_AreaCode.Value = txtInsuredTelephone1.Text.Trim();
        //        oHCFA1500.CF_7_Insureds_Tel_Number.Value = txtInsuredTelephone2.Text.Trim();
        //        oHCFA1500.CF_7_Insureds_Zip.Value = txtInsuredsZip.Text.Trim();
        //        if (dtpInsuredsDOB.Checked == true)
        //        {
        //            oHCFA1500.CF_11_Insureds_DOB_DD.Value = dtpInsuredsDOB.Value.ToString("dd");
        //            oHCFA1500.CF_11_Insureds_DOB_MM.Value = dtpInsuredsDOB.Value.ToString("MM");
        //            oHCFA1500.CF_11_Insureds_DOB_YY.Value = dtpInsuredsDOB.Value.ToString(_strYearFormat);
        //        }
        //        oHCFA1500.CF_11_Other_Claim_ID_Designated_by_NUCC.Value = txtInsuredEmployerORSchoolName.Text.Trim();
        //        oHCFA1500.CF_11_Insureds_InsuracnePlan.Value = txtInsuredInsurancePlanName.Text.Trim();
        //        if (chkIsOtherHealthPlan_No.Checked == true)
        //        {
        //            oHCFA1500.CF_11_Insureds_OtherHealthPlan_No.Value = true;
        //        }
        //        else if (chkIsOtherHealthPlan_Yes.Checked == true)
        //        {
        //            oHCFA1500.CF_11_Insureds_OtherHealthPlan_Yes.Value = true;
        //        }

        //        oHCFA1500.CF_11_Insureds_PolicyGroupNo.Value = txtInsuredPolicyorFECANo.Text.Trim();
        //        if (chkInsuredSex_Female.Checked == true)
        //        {
        //            oHCFA1500.CF_11_Insureds_Sex_Female.Value = true;
        //        }
        //        if (chkInsuredSex_Male.Checked == true)
        //        {
        //            oHCFA1500.CF_11_Insureds_Sex_Male.Value = true;
        //        }

        //        oHCFA1500.CF_11_Qualifier_No.Value = txtOtherClaimIDQualifier.Text;
        //        oHCFA1500.CF_1a_InsuredsIDNumber.Value = txtInsuredIdNumber.Text.Trim();
        //        oHCFA1500.CF_2_Patient_Name.Value = txtPatientName.Text.Trim();
        //        oHCFA1500.CF_3_Patient_DOB_DD.Value = dtpPatientDOB.Value.ToString("dd");
        //        oHCFA1500.CF_3_Patient_DOB_MM.Value = dtpPatientDOB.Value.ToString("MM");
        //        oHCFA1500.CF_3_Patient_DOB_YY.Value = dtpPatientDOB.Value.ToString(_strYearFormat);
        //        if (chkPatient_Female.Checked == true)
        //        {
        //            oHCFA1500.CF_3_Patient_Sex_Female.Value = true;
        //        }
        //        if (chkPatient_Male.Checked == true)
        //        {
        //            oHCFA1500.CF_3_Patient_Sex_Male.Value = true;
        //        }
        //        oHCFA1500.CF_4_Insureds_Name.Value = txtInsuredName.Text.Trim();

        //        oHCFA1500.CF_5_Patient_Address.Value = txtPatientAddress.Text.Trim();
        //        oHCFA1500.CF_5_Patient_City.Value = txtPatientCity.Text.Trim();
        //        oHCFA1500.CF_5_Patient_State.Value = txtPatientState.Text.Trim();
        //        oHCFA1500.CF_5_Patient_Tel_AreaCode.Value = txtPatientTelephone1.Text.Trim();
        //        oHCFA1500.CF_5_Patient_Tel_Number.Value = txtPatientTelephone2.Text.Trim();
        //        oHCFA1500.CF_5_Patient_Zip.Value = txtPatientZip.Text.Trim();

        //        //if (dtpOtherInsuredDOB.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_9_Other_Insureds_DOB_DD.Value = dtpOtherInsuredDOB.Value.ToString("dd");
        //        //    oHCFA1500.CF_9_Other_Insureds_DOB_MM.Value = dtpOtherInsuredDOB.Value.ToString("MM");
        //        //    oHCFA1500.CF_9_Other_Insureds_DOB_YY.Value = dtpOtherInsuredDOB.Value.ToString("yy");
        //        //}
        //        //oHCFA1500.CF_9_Other_Insureds_EmployerName.Value = txtOtherInsuredEmployerORSchoolName.Text.Trim();
        //        oHCFA1500.CF_9_Other_Insureds_InsuracnePlan.Value = txtOtherInsuredInsuranceName.Text.Trim();
        //        oHCFA1500.CF_9_Other_Insureds_Name.Value = txtOtherInsuredName.Text.Trim();
        //        oHCFA1500.CF_9_Other_Insureds_PolicyGroupNo.Value = txtOtherInsuredPolicyNo.Text.Trim();

        //        //if (chkOtherInsuredSex_Female.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_9_Other_Insureds_Sex_Female.Value = true;
        //        //}
        //        //if (chkOtherInsuredSex_Male.Checked == true)
        //        //{
        //        //    oHCFA1500.CF_9_Other_Insureds_Sex_Male.Value = true;
        //        //}
        //       // oHCFA1500.CF_10_PatientConditionTo_ResForLocaluse.Value = txtReserveForLocalUse.Text.Trim();
        //        oHCFA1500.CF_12_PatientAuthorizedPersons_Signature.Value = txtPatientSignature.Text.Trim();
        //        if (dtpPatientSignDate.Checked == true)
        //        {
        //            oHCFA1500.CF_12_PatientAuthorizedPersons_Signature_Date.Value = dtpPatientSignDate.Value.ToString(_strWholeDateFormat);
        //        }
        //        oHCFA1500.CF_13_InsuredsAuthorizedPersons_Signature.Value = txtInsuredPersonSign.Text.Trim();

        //        if (dtpDateOfCurrentIllness.Checked == true)
        //        {
        //            oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_DD.Value = dtpDateOfCurrentIllness.Value.ToString("dd");
        //            oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_MM.Value = dtpDateOfCurrentIllness.Value.ToString("MM");
        //            oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_YY.Value = dtpDateOfCurrentIllness.Value.ToString(_strYearFormat);
        //        }

        //        oHCFA1500.CF_14_Qualifier_No.Value = txtDateOfCurrentIllnessQualifier.Text;
        //        if (dtpSimilarIllnessFirstDate.Checked == true)
        //        {
        //            oHCFA1500.CF_15_Other_Date_DD.Value = dtpSimilarIllnessFirstDate.Value.ToString("dd");
        //            oHCFA1500.CF_15_Other_Dates_MM.Value = dtpSimilarIllnessFirstDate.Value.ToString("MM");
        //            oHCFA1500.CF_15_Other_Date_YY.Value = dtpSimilarIllnessFirstDate.Value.ToString(_strYearFormat);
        //        }

        //        oHCFA1500.CF_15_Qualifier_No.Value = txtSimilarIllnessFirstDateQualifier.Text;

        //        if (dtpUnableToWorkFrom.Checked == true)
        //        {
        //            oHCFA1500.CF_16_UnableToWorkFromDate_DD.Value = dtpUnableToWorkFrom.Value.ToString("dd");
        //            oHCFA1500.CF_16_UnableToWorkFromDate_MM.Value = dtpUnableToWorkFrom.Value.ToString("MM");
        //            oHCFA1500.CF_16_UnableToWorkFromDate_YY.Value = dtpUnableToWorkFrom.Value.ToString(_strYearFormat);
        //        }
        //        if (dtpUnableToWorkTill.Checked == true)
        //        {
        //            oHCFA1500.CF_16_UnableToWorkTillDate_DD.Value = dtpUnableToWorkTill.Value.ToString("dd");
        //            oHCFA1500.CF_16_UnableToWorkTillDate_MM.Value = dtpUnableToWorkTill.Value.ToString("MM");
        //            oHCFA1500.CF_16_UnableToWorkTillDate_YY.Value = dtpUnableToWorkTill.Value.ToString(_strYearFormat);
        //        }



        //        oHCFA1500.CF_17_ReferringProvider_Name.Value = txtReferringProviderName.Text.Trim();
        //        oHCFA1500.CF_17_ReferringProvider_Qaulifier.Value = txtReferringProviderQualifier.Text.Trim();
        //        oHCFA1500.CF_17a_ReferringProvider_OtherQualifier.Value = txtReferringProvider_OtherType.Text.Trim();
        //        oHCFA1500.CF_17a_ReferringProvider_OtherID.Value = txtReferringProvider_OtherValue.Text.Trim();
        //        oHCFA1500.CF_17b_ReferringProvider_NPI.Value = txtReferringProvider_NPI.Text.Trim();


        //        if (dtpHospitalisationFrom.Checked == true)
        //        {
        //            oHCFA1500.CF_18_HospitalizationFromDate_DD.Value = dtpHospitalisationFrom.Value.ToString("dd");
        //            oHCFA1500.CF_18_HospitalizationFromDate_MM.Value = dtpHospitalisationFrom.Value.ToString("MM");
        //            oHCFA1500.CF_18_HospitalizationFromDate_YY.Value = dtpHospitalisationFrom.Value.ToString(_strYearFormat);
        //        }
        //        if (dtpHospitalisationTo.Checked == true)
        //        {

        //            oHCFA1500.CF_18_HospitalizationTillDate_DD.Value = dtpHospitalisationTo.Value.ToString("dd");
        //            oHCFA1500.CF_18_HospitalizationTillDate_MM.Value = dtpHospitalisationTo.Value.ToString("MM");
        //            oHCFA1500.CF_18_HospitalizationTillDate_YY.Value = dtpHospitalisationTo.Value.ToString(_strYearFormat);
        //        }


        //        oHCFA1500.CF_19_LocalUse_Field.Value = txt19ReservedForLocalUse.Text.Trim();
        //        oHCFA1500.CF_20_OutsideLab_Charges_Principal.Value = txtOutsideLabCharges1.Text.Trim();
        //        oHCFA1500.CF_20_OutsideLab_Charges_Secondary.Value = txtOutsideLabCharges2.Text.Trim();
        //        if (chkOutsideLab_No.Checked == true)
        //        {
        //            oHCFA1500.CF_20_OutsideLab_No.Value = true;
        //        }
        //        if (chkOutsideLab_Yes.Checked == true)
        //        {
        //            oHCFA1500.CF_20_OutsideLab_Yes.Value = true;
        //        }

        //        oHCFA1500.CF_21_Icd_Ind_No.Value = txtIcdInd1.Text;
        //        oHCFA1500.CF_21_Icd_Ind.Value = txtIcdInd2.Text;
        //        oHCFA1500.CF_21_Diagnosis_A_Principal.Value = txtDiagnosisCode11.Text;
        //       // oHCFA1500.CF_21_Diagnosis_1_Secondary.Value = txtDiagnosisCode12.Text;

        //        oHCFA1500.CF_21_Diagnosis_B_Principal.Value = txtDiagnosisCode21.Text;
        //      //  oHCFA1500.CF_21_Diagnosis_2_Secondary.Value = txtDiagnosisCode32.Text;

        //        oHCFA1500.CF_21_Diagnosis_C_Principal.Value = txtDiagnosisCode31.Text;
        //    //    oHCFA1500.CF_21_Diagnosis_3_Secondary.Value = txtDiagnosisCode22.Text;

        //        oHCFA1500.CF_21_Diagnosis_D_Principal.Value = txtDiagnosisCode41.Text;
        //    //    oHCFA1500.CF_21_Diagnosis_4_Secondary.Value = txtDiagnosisCode42.Text;

        //        oHCFA1500.CF_21_Diagnosis_E_Principal.Value = txtDiagnosisCodeE.Text;
        //        oHCFA1500.CF_21_Diagnosis_F_Principal.Value = txtDiagnosisCodeF.Text;
        //        oHCFA1500.CF_21_Diagnosis_G_Principal.Value = txtDiagnosisCodeG.Text;
        //        oHCFA1500.CF_21_Diagnosis_H_Principal.Value = txtDiagnosisCodeH.Text;
        //        oHCFA1500.CF_21_Diagnosis_I_Principal.Value = txtDiagnosisCodeI.Text;
        //        oHCFA1500.CF_21_Diagnosis_J_Principal.Value = txtDiagnosisCodeJ.Text;
        //        oHCFA1500.CF_21_Diagnosis_K_Principal.Value = txtDiagnosisCodeK.Text;
        //        oHCFA1500.CF_21_Diagnosis_L_Principal.Value = txtDiagnosisCodeL.Text;

        //        oHCFA1500.CF_22_Resubmission.Value = txtMedicaidResubmissionCode.Text.Trim();
        //        oHCFA1500.CF_22_Original_Refrence_No.Value = txtOriginalRefNumber.Text.Trim();
        //        oHCFA1500.CF_23_PriorAuthorization_No.Value = txtPriorAuthorizationNumber.Text.Trim();


        //        oHCFA1500.CF_24A_L1_DOS_From_DD.Value = dtpDOS1From.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L1_DOS_From_MM.Value = dtpDOS1From.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L1_DOS_From_YY.Value = dtpDOS1From.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L1_DOS_To_DD.Value = dtpDOS1To.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L1_DOS_To_MM.Value = dtpDOS1To.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L1_DOS_To_YY.Value = dtpDOS1To.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L2_DOS_From_DD.Value = dtpDOS2From.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L2_DOS_From_MM.Value = dtpDOS2From.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L2_DOS_From_YY.Value = dtpDOS2From.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L2_DOS_To_DD.Value = dtpDOS2To.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L2_DOS_To_MM.Value = dtpDOS2To.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L2_DOS_To_YY.Value = dtpDOS2To.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L3_DOS_From_DD.Value = dtpDOS3From.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L3_DOS_From_MM.Value = dtpDOS3From.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L3_DOS_From_YY.Value = dtpDOS3From.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L3_DOS_To_DD.Value = dtpDOS3To.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L3_DOS_To_MM.Value = dtpDOS3To.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L3_DOS_To_YY.Value = dtpDOS3To.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L4_DOS_From_DD.Value = dtpDOS4From.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L4_DOS_From_MM.Value = dtpDOS4From.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L4_DOS_From_YY.Value = dtpDOS4From.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L4_DOS_To_DD.Value = dtpDOS4To.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L4_DOS_To_MM.Value = dtpDOS4To.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L4_DOS_To_YY.Value = dtpDOS4To.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L5_DOS_From_DD.Value = dtpDOS5From.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L5_DOS_From_MM.Value = dtpDOS5From.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L5_DOS_From_YY.Value = dtpDOS5From.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L5_DOS_To_DD.Value = dtpDOS5To.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L5_DOS_To_MM.Value = dtpDOS5To.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L5_DOS_To_YY.Value = dtpDOS5To.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L6_DOS_From_DD.Value = dtpDOS6From.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L6_DOS_From_MM.Value = dtpDOS6From.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L6_DOS_From_YY.Value = dtpDOS6From.Value.ToString("yy");
        //        oHCFA1500.CF_24A_L6_DOS_To_DD.Value = dtpDOS6To.Value.ToString("dd");
        //        oHCFA1500.CF_24A_L6_DOS_To_MM.Value = dtpDOS6To.Value.ToString("MM");
        //        oHCFA1500.CF_24A_L6_DOS_To_YY.Value = dtpDOS6To.Value.ToString("yy");


        //        oHCFA1500.CF_24B_L1_POS_Code.Value = txtPOS1.Text;
        //        oHCFA1500.CF_24B_L2_POS_Code.Value = txtPOS2.Text;
        //        oHCFA1500.CF_24B_L3_POS_Code.Value = txtPOS3.Text;
        //        oHCFA1500.CF_24B_L4_POS_Code.Value = txtPOS4.Text;
        //        oHCFA1500.CF_24B_L5_POS_Code.Value = txtPOS5.Text;
        //        oHCFA1500.CF_24B_L6_POS_Code.Value = txtPOS6.Text;


        //        oHCFA1500.CF_24C_L1_EMG_Code.Value = txtEMG1.Text;
        //        oHCFA1500.CF_24C_L2_EMG_Code.Value = txtEMG2.Text;
        //        oHCFA1500.CF_24C_L3_EMG_Code.Value = txtEMG3.Text;
        //        oHCFA1500.CF_24C_L4_EMG_Code.Value = txtEMG4.Text;
        //        oHCFA1500.CF_24C_L5_EMG_Code.Value = txtEMG5.Text;
        //        oHCFA1500.CF_24C_L6_EMG_Code.Value = txtEMG6.Text;


        //        oHCFA1500.CF_24D_L1_CPT_HCPCS_Code.Value = txtCPT1.Text;
        //        oHCFA1500.CF_24D_L2_CPT_HCPCS_Code.Value = txtCPT2.Text;
        //        oHCFA1500.CF_24D_L3_CPT_HCPCS_Code.Value = txtCPT3.Text;
        //        oHCFA1500.CF_24D_L4_CPT_HCPCS_Code.Value = txtCPT4.Text;
        //        oHCFA1500.CF_24D_L5_CPT_HCPCS_Code.Value = txtCPT5.Text;
        //        oHCFA1500.CF_24D_L6_CPT_HCPCS_Code.Value = txtCPT6.Text;

        //        if (txtCPT1.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line1 = true; }
        //        if (txtCPT2.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line2 = true; }
        //        if (txtCPT3.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line3 = true; }
        //        if (txtCPT4.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line4 = true; }
        //        if (txtCPT5.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line5 = true; }
        //        if (txtCPT6.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line6 = true; }

        //        oHCFA1500.CF_24D_L1_Modifier_1_Code.Value = txtMOD11.Text;
        //        oHCFA1500.CF_24D_L1_Modifier_2_Code.Value = txtMOD12.Text;
        //        oHCFA1500.CF_24D_L1_Modifier_3_Code.Value = txtMOD13.Text;
        //        oHCFA1500.CF_24D_L1_Modifier_4_Code.Value = txtMOD14.Text;
        //        oHCFA1500.CF_24D_L2_Modifier_1_Code.Value = txtMOD21.Text;
        //        oHCFA1500.CF_24D_L2_Modifier_2_Code.Value = txtMOD22.Text;
        //        oHCFA1500.CF_24D_L2_Modifier_3_Code.Value = txtMOD23.Text;
        //        oHCFA1500.CF_24D_L2_Modifier_4_Code.Value = txtMOD24.Text;
        //        oHCFA1500.CF_24D_L3_Modifier_1_Code.Value = txtMOD31.Text;
        //        oHCFA1500.CF_24D_L3_Modifier_2_Code.Value = txtMOD32.Text;
        //        oHCFA1500.CF_24D_L3_Modifier_3_Code.Value = txtMOD33.Text;
        //        oHCFA1500.CF_24D_L3_Modifier_4_Code.Value = txtMOD34.Text;
        //        oHCFA1500.CF_24D_L4_Modifier_1_Code.Value = txtMOD41.Text;
        //        oHCFA1500.CF_24D_L4_Modifier_2_Code.Value = txtMOD42.Text;
        //        oHCFA1500.CF_24D_L4_Modifier_3_Code.Value = txtMOD43.Text;
        //        oHCFA1500.CF_24D_L4_Modifier_4_Code.Value = txtMOD44.Text;
        //        oHCFA1500.CF_24D_L5_Modifier_1_Code.Value = txtMOD51.Text;
        //        oHCFA1500.CF_24D_L5_Modifier_2_Code.Value = txtMOD52.Text;
        //        oHCFA1500.CF_24D_L5_Modifier_3_Code.Value = txtMOD53.Text;
        //        oHCFA1500.CF_24D_L5_Modifier_4_Code.Value = txtMOD54.Text;
        //        oHCFA1500.CF_24D_L6_Modifier_1_Code.Value = txtMOD61.Text;
        //        oHCFA1500.CF_24D_L6_Modifier_2_Code.Value = txtMOD62.Text;
        //        oHCFA1500.CF_24D_L6_Modifier_3_Code.Value = txtMOD63.Text;
        //        oHCFA1500.CF_24D_L6_Modifier_4_Code.Value = txtMOD64.Text;

        //        oHCFA1500.CF_24E_L1_Diagnosis_Pointers.Value = txtDxPtr1.Text;
        //        oHCFA1500.CF_24E_L2_Diagnosis_Pointers.Value = txtDxPtr2.Text;
        //        oHCFA1500.CF_24E_L3_Diagnosis_Pointers.Value = txtDxPtr3.Text;
        //        oHCFA1500.CF_24E_L4_Diagnosis_Pointers.Value = txtDxPtr4.Text;
        //        oHCFA1500.CF_24E_L5_Diagnosis_Pointers.Value = txtDxPtr5.Text;
        //        oHCFA1500.CF_24E_L6_Diagnosis_Pointers.Value = txtDxPtr6.Text;


        //        oHCFA1500.CF_24F_L1_Charges_Principal.Value = txtCharges1.Text;
        //        oHCFA1500.CF_24F_L1_Charges_Secondary.Value = txtCharges11.Text;
        //        oHCFA1500.CF_24F_L2_Charges_Principal.Value = txtCharges2.Text;
        //        oHCFA1500.CF_24F_L2_Charges_Secondary.Value = txtCharges21.Text;
        //        oHCFA1500.CF_24F_L3_Charges_Principal.Value = txtCharges3.Text;
        //        oHCFA1500.CF_24F_L3_Charges_Secondary.Value = txtCharges31.Text;
        //        oHCFA1500.CF_24F_L4_Charges_Principal.Value = txtCharges4.Text;
        //        oHCFA1500.CF_24F_L4_Charges_Secondary.Value = txtCharges41.Text;
        //        oHCFA1500.CF_24F_L5_Charges_Principal.Value = txtCharges5.Text;
        //        oHCFA1500.CF_24F_L5_Charges_Secondary.Value = txtCharges51.Text;
        //        oHCFA1500.CF_24F_L6_Charges_Principal.Value = txtCharges6.Text;
        //        oHCFA1500.CF_24F_L6_Charges_Secondary.Value = txtCharges61.Text;

        //        oHCFA1500.CF_24G_L1_Days_Units.Value = txtUnits1.Text;
        //        oHCFA1500.CF_24G_L2_Days_Units.Value = txtUnits2.Text;
        //        oHCFA1500.CF_24G_L3_Days_Units.Value = txtUnits3.Text;
        //        oHCFA1500.CF_24G_L4_Days_Units.Value = txtUnits4.Text;
        //        oHCFA1500.CF_24G_L5_Days_Units.Value = txtUnits5.Text;
        //        oHCFA1500.CF_24G_L6_Days_Units.Value = txtUnits6.Text;


        //        oHCFA1500.CF_24H_L1_EPSDT_FamilyPlan_Shaded.Value = txtEPSDTShaded1.Text.ToString().Length > 2 ? txtEPSDTShaded1.Text.ToString().Substring(0, 2) : txtEPSDTShaded1.Text.ToString();
        //        oHCFA1500.CF_24H_L2_EPSDT_FamilyPlan_Shaded.Value = txtEPSDTShaded2.Text.ToString().Length > 2 ? txtEPSDTShaded2.Text.ToString().Substring(0, 2) : txtEPSDTShaded2.Text.ToString();
        //        oHCFA1500.CF_24H_L3_EPSDT_FamilyPlan_Shaded.Value = txtEPSDTShaded3.Text.ToString().Length > 2 ? txtEPSDTShaded3.Text.ToString().Substring(0, 2) : txtEPSDTShaded3.Text.ToString();
        //        oHCFA1500.CF_24H_L4_EPSDT_FamilyPlan_Shaded.Value = txtEPSDTShaded4.Text.ToString().Length > 2 ? txtEPSDTShaded4.Text.ToString().Substring(0, 2) : txtEPSDTShaded4.Text.ToString();
        //        oHCFA1500.CF_24H_L5_EPSDT_FamilyPlan_Shaded.Value = txtEPSDTShaded5.Text.ToString().Length > 2 ? txtEPSDTShaded5.Text.ToString().Substring(0, 2) : txtEPSDTShaded5.Text.ToString();
        //        oHCFA1500.CF_24H_L6_EPSDT_FamilyPlan_Shaded.Value = txtEPSDTShaded6.Text.ToString().Length > 2 ? txtEPSDTShaded6.Text.ToString().Substring(0, 2) : txtEPSDTShaded6.Text.ToString();

        //        oHCFA1500.CF_24H_L1_EPSDT_FamilyPlan.Value = txtEPSDT1.Text.ToString().Length>2 ? txtEPSDT1.Text.ToString().Substring(0, 2) : txtEPSDT1.Text.ToString();
        //        oHCFA1500.CF_24H_L2_EPSDT_FamilyPlan.Value = txtEPSDT2.Text.ToString().Length > 2 ? txtEPSDT2.Text.ToString().Substring(0, 2) : txtEPSDT2.Text.ToString();
        //        oHCFA1500.CF_24H_L3_EPSDT_FamilyPlan.Value = txtEPSDT3.Text.ToString().Length > 2 ? txtEPSDT3.Text.ToString().Substring(0, 2) : txtEPSDT3.Text.ToString();
        //        oHCFA1500.CF_24H_L4_EPSDT_FamilyPlan.Value = txtEPSDT4.Text.ToString().Length > 2 ? txtEPSDT4.Text.ToString().Substring(0, 2) : txtEPSDT4.Text.ToString();
        //        oHCFA1500.CF_24H_L5_EPSDT_FamilyPlan.Value = txtEPSDT5.Text.ToString().Length > 2 ? txtEPSDT5.Text.ToString().Substring(0, 2) : txtEPSDT5.Text.ToString();
        //        oHCFA1500.CF_24H_L6_EPSDT_FamilyPlan.Value = txtEPSDT6.Text.ToString().Length > 2 ? txtEPSDT6.Text.ToString().Substring(0, 2) : txtEPSDT6.Text.ToString();

        //        oHCFA1500.CF_24J_L1_RenderingProvider_NPI.Value = txtRenderingProvider1_NPI.Text;
        //        oHCFA1500.CF_24J_L2_RenderingProvider_NPI.Value = txtRenderingProvider2_NPI.Text;
        //        oHCFA1500.CF_24J_L3_RenderingProvider_NPI.Value = txtRenderingProvider3_NPI.Text;
        //        oHCFA1500.CF_24J_L4_RenderingProvider_NPI.Value = txtRenderingProvider4_NPI.Text;
        //        oHCFA1500.CF_24J_L5_RenderingProvider_NPI.Value = txtRenderingProvider5_NPI.Text;
        //        oHCFA1500.CF_24J_L6_RenderingProvider_NPI.Value = txtRenderingProvider6_NPI.Text;


        //        oHCFA1500.CF_24J_L1_RenderingProvider_OtherQualifier.Value = txtRenderingProvider1_Qualifier.Text;
        //        oHCFA1500.CF_24J_L2_RenderingProvider_OtherQualifier.Value = txtRenderingProvider2_Qualifier.Text;
        //        oHCFA1500.CF_24J_L3_RenderingProvider_OtherQualifier.Value = txtRenderingProvider3_Qualifier.Text;
        //        oHCFA1500.CF_24J_L4_RenderingProvider_OtherQualifier.Value = txtRenderingProvider4_Qualifier.Text;
        //        oHCFA1500.CF_24J_L5_RenderingProvider_OtherQualifier.Value = txtRenderingProvider5_Qualifier.Text;
        //        oHCFA1500.CF_24J_L6_RenderingProvider_OtherQualifier.Value = txtRenderingProvider6_Qualifier.Text;

        //        oHCFA1500.CF_24J_L1_RenderingProvider_OtherQualifiervalue.Value = txtRenderingProvider1_QualifierValue.Text;
        //        oHCFA1500.CF_24J_L2_RenderingProvider_OtherQualifiervalue.Value = txtRenderingProvider2_QualifierValue.Text;
        //        oHCFA1500.CF_24J_L3_RenderingProvider_OtherQualifiervalue.Value = txtRenderingProvider3_QualifierValue.Text;
        //        oHCFA1500.CF_24J_L4_RenderingProvider_OtherQualifiervalue.Value = txtRenderingProvider4_QualifierValue.Text;
        //        oHCFA1500.CF_24J_L5_RenderingProvider_OtherQualifiervalue.Value = txtRenderingProvider5_QualifierValue.Text;
        //        oHCFA1500.CF_24J_L6_RenderingProvider_OtherQualifiervalue.Value = txtRenderingProvider6_QualifierValue.Text;



        //        oHCFA1500.CF_24A_L1_Note.Value = txtNotes1.Text.Trim();
        //        oHCFA1500.CF_24A_L2_Note.Value = txtNotes2.Text.Trim();
        //        oHCFA1500.CF_24A_L3_Note.Value = txtNotes3.Text.Trim();
        //        oHCFA1500.CF_24A_L4_Note.Value = txtNotes4.Text.Trim();
        //        oHCFA1500.CF_24A_L5_Note.Value = txtNotes5.Text.Trim();
        //        oHCFA1500.CF_24A_L6_Note.Value = txtNotes6.Text.Trim();

        //        oHCFA1500.CF_25_FederalTax_ID_No.Value = txtFederalTaxID.Text;
        //        if (chkFederalTaxID_EIN.Checked == true)
        //        {
        //            oHCFA1500.CF_25_FederalTaxID_Qualifier_EIN.Value = true;
        //        }
        //        if (chkFederalTaxID_SSN.Checked == true)
        //        {
        //            oHCFA1500.CF_25_FederalTaxID_Qualifier_SSN.Value = true;
        //        }

        //        oHCFA1500.CF_26_PatientAccount_No.Value = txtPatientAccountNo.Text;
        //        if (chkAcceptAssignment_No.Checked == true)
        //        {
        //            oHCFA1500.CF_27_AcceptAssignment_NO.Value = true;
        //        }
        //        if (chkAcceptAssignment_Yes.Checked == true)
        //        {
        //            oHCFA1500.CF_27_AcceptAssignment_YES.Value = true;
        //        }

        //        oHCFA1500.CF_28_TotalCharge_Principal.Value = txtTotalCharges.Text;
        //        oHCFA1500.CF_28_TotalCharge_Secondary.Value = txtTotalCharges2.Text;
        //        oHCFA1500.CF_29_AmountPaid_Principal.Value = txtAmountPaid.Text;
        //        oHCFA1500.CF_29_AmountPaid_Secondary.Value = txtAmountPaid2.Text;
        //        //oHCFA1500.CF_30_BalanceDue_Principal.Value = txtBalanceDue.Text;
        //        //oHCFA1500.CF_30_BalanceDue_Secondary.Value = txtBalanceDue2.Text;

        //        oHCFA1500.CF_31_Physician_Supplier_Signature.Value = txtPhyscianSignature.Text;
        //        oHCFA1500.CF_31_Physician_Supplier_Signature_Date.Value = dtpPhysicianSignDate.Value.ToString(_strWholeDateFormat);
        //        oHCFA1500.CF_31_Physician_Supplier_QualifierValue.Value = txtPhyscianQualifierValue.Text;

        //        string[] FacilityInfo = txtFacilityInfo.Text.Split('\n');

        //        //Commented and Added on 27th Nov 2010
        //        //if (FacilityInfo.Length >= 3)
        //        //{
        //        //    oHCFA1500.CF_32_Service_Facility_Name.Value = FacilityInfo[0];
        //        //    oHCFA1500.CF_32_Service_Facility_Address_Line1.Value = FacilityInfo[1];
        //        //    oHCFA1500.CF_32_Service_Facility_Address_Line2.Value = FacilityInfo[2];
        //        //}

        //        if (FacilityInfo.Length > 0)
        //        {
        //            //if (FacilityInfo.Length >= 3)
        //            //{
        //            oHCFA1500.CF_32_Service_Facility_Name.Value = FacilityInfo[0];
        //            if (FacilityInfo.Length > 1)
        //            {
        //                oHCFA1500.CF_32_Service_Facility_Address_Line1.Value = FacilityInfo[1];
        //            }
        //            if (FacilityInfo.Length > 2)
        //            {
        //                oHCFA1500.CF_32_Service_Facility_Address_Line2.Value = FacilityInfo[2];
        //            }
        //        }

        //        //**

        //        oHCFA1500.CF_32a_Service_Facility_NPI.Value = txtFacility_a_NPI.Text;
        //        oHCFA1500.CF_32b_Service_Facility_UPIN_OtherID.Value = txtFacility_b.Text;

        //        string[] BillingProviderInfo = txtBillingProviderInfo.Text.Split('\n');

        //        //Commented and Added on 27th Nov 2010

        //        //if (BillingProviderInfo.Length >= 3)
        //        //{
        //        //    oHCFA1500.CF_33_BillingProvider_Name.Value = BillingProviderInfo[0];
        //        //    oHCFA1500.CF_33_BillingProvider_Address_Line1.Value = BillingProviderInfo[1];
        //        //    oHCFA1500.CF_33_BillingProvider_Address_Line2.Value = BillingProviderInfo[2];
        //        //}

        //        if (BillingProviderInfo.Length > 0)
        //        {
        //            //if (FacilityInfo.Length >= 3)
        //            //{
        //            oHCFA1500.CF_33_BillingProvider_Name.Value = BillingProviderInfo[0];
        //            if (BillingProviderInfo.Length > 1)
        //            {
        //                oHCFA1500.CF_33_BillingProvider_Address_Line1.Value = BillingProviderInfo[1];
        //            }
        //            if (BillingProviderInfo.Length > 2)
        //            {
        //                oHCFA1500.CF_33_BillingProvider_Address_Line2.Value = BillingProviderInfo[2];
        //            }
        //        }

        //        //**

        //        oHCFA1500.CF_33_BillingProvider_Tel_AreaCode.Value = txtBillingProviderPhone1.Text;
        //        oHCFA1500.CF_33_BillingProvider_Tel_Number.Value = txtBillingProviderPhone2.Text;
        //        oHCFA1500.CF_33a_BillingProvider_NPI.Value = txtBillingProv_a_NPI.Text;
        //        oHCFA1500.CF_33b_BillingProvider_UPIN_OtherID.Value = txtBillingProv_b_UPIN.Text;


        //    }
        //    catch //(Exception ex)
        //    {
        //        //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

        //        oHCFA1500 = null;
        //    }
        //    finally
        //    {
        //    }
        //    return oHCFA1500;
        //}

        private string formatedData(FormFieldString BoxField, string sdata)
        {
            try
            {
                if (_IsForPrintData)
                {
                    if (sdata.Trim().Length > Convert.ToInt32(BoxField.CharSize))
                    {
                        if (BoxField.CharSize > 0)
                        {
                            sdata = Convert.ToString(sdata).Substring(0, Convert.ToInt32(BoxField.CharSize));
                        }
                        else
                        {
                            sdata = "";
                        }
                    }
                    if (_IsCapatalize)
                    {
                        return sdata.ToUpper();
                    }
                    else
                    {
                        return sdata;
                    }
                }
                else
                {
                    if (_IsCapatalize)
                    {
                        return sdata.ToUpper();
                    }
                    else
                    {
                        return sdata;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return "";
            }
        }

        #region "Code added by Pankaj 23122009 for PrintForm Setup"
        private gloHCFA1500PaperFormNew SetHCFA1500PaperForm(gloHCFA1500PaperFormNew oHCFA1500)
        {
            try
            {
                clsgloBilling oclsgloBilling = new clsgloBilling(_databaseconnectionstring);
                string _sResult = oclsgloBilling.GetFontSetupSettingformCmsAndUB("Capatalize CMS1500 02/12 data");
                _IsCapatalize = Convert.ToBoolean(_sResult == "" ? false : Convert.ToBoolean(_sResult));
                if (oclsgloBilling != null)
                {
                    oclsgloBilling.Dispose();
                    oclsgloBilling = null;
                }
                oHCFA1500.CF_Top_InsuranceHeader.Value = formatedData(oHCFA1500.CF_Top_InsuranceHeader, txtPayerNameAndAddress.Text);
                if (chkCHAMPVA.Checked == true)
                {
                    oHCFA1500.CF_1_Insuracne_Type_Champva.Value = true;
                }
                else if (chkFECABlackLung.Checked == true)
                {
                    oHCFA1500.CF_1_Insuracne_Type_FECA.Value = true;
                }
                else if (chkGroupHealthPlan.Checked == true)
                {
                    oHCFA1500.CF_1_Insuracne_Type_GroupHealthPlan.Value = true;
                }
                else if (chkOtherInsuranceType.Checked == true)
                {
                    oHCFA1500.CF_1_Insuracne_Type_Other.Value = true;
                }
                else if (chkMedicaid.Checked == true)
                {
                    oHCFA1500.CF_1_Insuracne_Type_Medicaid.Value = true;
                }
                else if (chkMedicare.Checked == true)
                {
                    oHCFA1500.CF_1_Insuracne_Type_Medicare.Value = true;
                }
                else if (chkTricareChampus.Checked == true)
                {
                    oHCFA1500.CF_1_Insuracne_Type_Tricare.Value = true;
                }

                if (chkPatientCoditionRelatedTo_AutoAccident_No.Checked == true)
                {
                    oHCFA1500.CF_10_PatientConditionTo_AutoAccident_No.Value = true;
                }
                else if (chkPatientCoditionRelatedTo_AutoAccident_Yes.Checked == true)
                {
                    oHCFA1500.CF_10_PatientConditionTo_AutoAccident_Yes.Value = true;
                    oHCFA1500.CF_10_PatientConditionTo_AutoAccident_State.Value = formatedData(oHCFA1500.CF_10_PatientConditionTo_AutoAccident_State, txtPatientCoditionRelatedTo_State.Text.Trim());
                }

                if (chkPatientCoditionRelatedTo_Employment_No.Checked == true)
                {
                    oHCFA1500.CF_10_PatientConditionTo_Employement_No.Value = true;
                }
                else if (chkPatientCoditionRelatedTo_Employment_Yes.Checked == true)
                {
                    oHCFA1500.CF_10_PatientConditionTo_Employement_Yes.Value = true;
                }

                if (chkPatientCoditionRelatedTo_OtherAccident_No.Checked == true)
                {
                    oHCFA1500.CF_10_PatientConditionTo_OtherAccident_No.Value = true;
                    //oHCFA1500.CF_10_PatientConditionTo_AutoAccident_State.Value = txtPatientCoditionRelatedTo_State.Text.Trim();
                }
                else if (chkPatientCoditionRelatedTo_OtherAccident_Yes.Checked == true)
                {
                    oHCFA1500.CF_10_PatientConditionTo_OtherAccident_Yes.Value = true;
                    //oHCFA1500.CF_10_PatientConditionTo_AutoAccident_State.Value = txtPatientCoditionRelatedTo_State.Text.Trim();
                }

                //if (chkPatientStatus_Employed.Checked == true)
                //{
                //    oHCFA1500.CF_8_PatientStatus_Employed.Value = true;
                //}
                //if (chkPatientStatus_FullTimeStudent.Checked == true)
                //{
                //    oHCFA1500.CF_8_PatientStatus_FullTimeStudent.Value = true;
                //}
                //if (chkPatientStatus_Married.Checked == true)
                //{
                //    oHCFA1500.CF_8_PatientStatus_Married.Value = true;
                //}
                //if (chkPatientStatus_Other.Checked == true)
                //{
                //    oHCFA1500.CF_8_PatientStatus_Other.Value = true;
                //}
                //if (chkPatientStatus_PartTimeStudent.Checked == true)
                //{
                //    oHCFA1500.CF_8_PatientStatus_PartTimeStudent.Value = true;
                //}
                //if (chkPatientStatus_Single.Checked == true)
                //{
                //    oHCFA1500.CF_8_PatientStatus_Single.Value = true;
                //}
                if (chkRelationship_Child.Checked == true)
                {
                    oHCFA1500.CF_6_PatientRelationship_Child.Value = true;
                }
                if (chkRelationship_Other.Checked == true)
                {
                    oHCFA1500.CF_6_PatientRelationship_Other.Value = true;
                }
                if (chkRelationship_Self.Checked == true)
                {
                    oHCFA1500.CF_6_PatientRelationship_Self.Value = true;
                }
                if (chkRelationship_Spouse.Checked == true)
                {
                    oHCFA1500.CF_6_PatientRelationship_Spouse.Value = true;
                }

                oHCFA1500.CF_7_Insureds_Address.Value = formatedData(oHCFA1500.CF_7_Insureds_Address, txtInsuredsAddress.Text.Trim());
                oHCFA1500.CF_7_Insureds_City.Value = formatedData(oHCFA1500.CF_7_Insureds_City, txtInsuredsCity.Text.Trim());
                oHCFA1500.CF_7_Insureds_State.Value = formatedData(oHCFA1500.CF_7_Insureds_State, txtInsuredsState.Text.Trim());
                oHCFA1500.CF_7_Insureds_Tel_AreaCode.Value = formatedData(oHCFA1500.CF_7_Insureds_Tel_AreaCode, txtInsuredTelephone1.Text.Trim());
                oHCFA1500.CF_7_Insureds_Tel_Number.Value = formatedData(oHCFA1500.CF_7_Insureds_Tel_Number, txtInsuredTelephone2.Text.Trim());
                oHCFA1500.CF_7_Insureds_Zip.Value = formatedData(oHCFA1500.CF_7_Insureds_Zip, txtInsuredsZip.Text.Trim());
                if (dtpInsuredsDOB.Checked == true)
                {
                    oHCFA1500.CF_11_Insureds_DOB_DD.Value = formatedData(oHCFA1500.CF_11_Insureds_DOB_DD,dtpInsuredsDOB.Value.ToString("dd"));
                    oHCFA1500.CF_11_Insureds_DOB_MM.Value = formatedData(oHCFA1500.CF_11_Insureds_DOB_MM,dtpInsuredsDOB.Value.ToString("MM"));
                    oHCFA1500.CF_11_Insureds_DOB_YY.Value = formatedData(oHCFA1500.CF_11_Insureds_DOB_YY,dtpInsuredsDOB.Value.ToString(_strYearFormat));
                }
                oHCFA1500.CF_11_Other_Claim_ID_Designated_by_NUCC.Value = formatedData(oHCFA1500.CF_11_Other_Claim_ID_Designated_by_NUCC, txtInsuredEmployerORSchoolName.Text.Trim());
                oHCFA1500.CF_11_Insureds_InsuracnePlan.Value = formatedData(oHCFA1500.CF_11_Insureds_InsuracnePlan, txtInsuredInsurancePlanName.Text.Trim());
                oHCFA1500.CF_11_Qualifier_No.Value = formatedData(oHCFA1500.CF_11_Qualifier_No, txtOtherClaimIDQualifier.Text);
                if (chkIsOtherHealthPlan_No.Checked == true)
                {
                    oHCFA1500.CF_11_Insureds_OtherHealthPlan_No.Value = true;
                }
                else if (chkIsOtherHealthPlan_Yes.Checked == true)
                {
                    oHCFA1500.CF_11_Insureds_OtherHealthPlan_Yes.Value = true;
                }

                oHCFA1500.CF_11_Insureds_PolicyGroupNo.Value = formatedData(oHCFA1500.CF_11_Insureds_PolicyGroupNo, txtInsuredPolicyorFECANo.Text.Trim());
                if (chkInsuredSex_Female.Checked == true)
                {
                    oHCFA1500.CF_11_Insureds_Sex_Female.Value = true;
                }
                if (chkInsuredSex_Male.Checked == true)
                {
                    oHCFA1500.CF_11_Insureds_Sex_Male.Value = true;
                }

                oHCFA1500.CF_1a_InsuredsIDNumber.Value = formatedData(oHCFA1500.CF_1a_InsuredsIDNumber, txtInsuredIdNumber.Text.Trim());
                oHCFA1500.CF_2_Patient_Name.Value = formatedData(oHCFA1500.CF_2_Patient_Name, txtPatientName.Text.Trim());
                oHCFA1500.CF_3_Patient_DOB_DD.Value = formatedData(oHCFA1500.CF_3_Patient_DOB_DD,dtpPatientDOB.Value.ToString("dd"));
                oHCFA1500.CF_3_Patient_DOB_MM.Value = formatedData(oHCFA1500.CF_3_Patient_DOB_MM,dtpPatientDOB.Value.ToString("MM"));
                oHCFA1500.CF_3_Patient_DOB_YY.Value = formatedData(oHCFA1500.CF_3_Patient_DOB_YY,dtpPatientDOB.Value.ToString(_strYearFormat));
                if (chkPatient_Female.Checked == true)
                {
                    oHCFA1500.CF_3_Patient_Sex_Female.Value = true;
                }
                if (chkPatient_Male.Checked == true)
                {
                    oHCFA1500.CF_3_Patient_Sex_Male.Value = true;
                }
                oHCFA1500.CF_4_Insureds_Name.Value = formatedData(oHCFA1500.CF_4_Insureds_Name, txtInsuredName.Text.Trim());

                oHCFA1500.CF_5_Patient_Address.Value = formatedData(oHCFA1500.CF_5_Patient_Address, txtPatientAddress.Text.Trim());
                oHCFA1500.CF_5_Patient_City.Value = formatedData(oHCFA1500.CF_5_Patient_City, txtPatientCity.Text.Trim());
                oHCFA1500.CF_5_Patient_State.Value = formatedData(oHCFA1500.CF_5_Patient_State, txtPatientState.Text.Trim());
                oHCFA1500.CF_5_Patient_Tel_AreaCode.Value = formatedData(oHCFA1500.CF_5_Patient_Tel_AreaCode, txtPatientTelephone1.Text.Trim());
                oHCFA1500.CF_5_Patient_Tel_Number.Value = formatedData(oHCFA1500.CF_5_Patient_Tel_Number, txtPatientTelephone2.Text.Trim());
                oHCFA1500.CF_5_Patient_Zip.Value = formatedData(oHCFA1500.CF_5_Patient_Zip, txtPatientZip.Text.Trim());

                //if (dtpOtherInsuredDOB.Checked == true)
                //{
                //    oHCFA1500.CF_9_Other_Insureds_DOB_DD.Value = dtpOtherInsuredDOB.Value.ToString("dd");
                //    oHCFA1500.CF_9_Other_Insureds_DOB_MM.Value = dtpOtherInsuredDOB.Value.ToString("MM");
                //    oHCFA1500.CF_9_Other_Insureds_DOB_YY.Value = dtpOtherInsuredDOB.Value.ToString("yy");
                //}
                //oHCFA1500.CF_9_Other_Insureds_EmployerName.Value = txtOtherInsuredEmployerORSchoolName.Text.Trim();
                oHCFA1500.CF_9_Other_Insureds_InsuracnePlan.Value = formatedData(oHCFA1500.CF_9_Other_Insureds_InsuracnePlan, txtOtherInsuredInsuranceName.Text.Trim());
                oHCFA1500.CF_9_Other_Insureds_Name.Value = formatedData(oHCFA1500.CF_9_Other_Insureds_Name, txtOtherInsuredName.Text.Trim());
                oHCFA1500.CF_9_Other_Insureds_PolicyGroupNo.Value = formatedData(oHCFA1500.CF_9_Other_Insureds_PolicyGroupNo, txtOtherInsuredPolicyNo.Text.Trim());

                //if (chkOtherInsuredSex_Female.Checked == true)
                //{
                //    oHCFA1500.CF_9_Other_Insureds_Sex_Female.Value = true;
                //}
                //if (chkOtherInsuredSex_Male.Checked == true)
                //{
                //    oHCFA1500.CF_9_Other_Insureds_Sex_Male.Value = true;
                //}
                oHCFA1500.CF_10_PatientConditionTo_ResForLocaluse.Value = formatedData(oHCFA1500.CF_10_PatientConditionTo_ResForLocaluse, txtReserveForLocalUse.Text.Trim());
                oHCFA1500.CF_12_PatientAuthorizedPersons_Signature.Value = formatedData(oHCFA1500.CF_12_PatientAuthorizedPersons_Signature, txtPatientSignature.Text.Trim());
                if (dtpPatientSignDate.Checked == true)
                {
                    oHCFA1500.CF_12_PatientAuthorizedPersons_Signature_Date.Value = formatedData(oHCFA1500.CF_12_PatientAuthorizedPersons_Signature_Date,dtpPatientSignDate.Value.ToString(_strWholeDateFormat));
                }
                oHCFA1500.CF_13_InsuredsAuthorizedPersons_Signature.Value = formatedData(oHCFA1500.CF_13_InsuredsAuthorizedPersons_Signature, txtInsuredPersonSign.Text.Trim());

                if (dtpDateOfCurrentIllness.Checked == true)
                {
                    oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_DD.Value = formatedData(oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_DD,dtpDateOfCurrentIllness.Value.ToString("dd"));
                    oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_MM.Value = formatedData(oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_MM,dtpDateOfCurrentIllness.Value.ToString("MM"));
                    oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_YY.Value = formatedData(oHCFA1500.CF_14_Date_Of_Current_Illness_Injury_or_Pregnancy_LMP_YY,dtpDateOfCurrentIllness.Value.ToString(_strYearFormat));
                }
                oHCFA1500.CF_14_Qualifier_No.Value = formatedData(oHCFA1500.CF_14_Qualifier_No, txtDateOfCurrentIllnessQualifier.Text);
                if (dtpSimilarIllnessFirstDate.Checked == true)
                {
                    oHCFA1500.CF_15_Other_Date_DD.Value = formatedData(oHCFA1500.CF_15_Other_Date_DD,dtpSimilarIllnessFirstDate.Value.ToString("dd"));
                    oHCFA1500.CF_15_Other_Dates_MM.Value = formatedData(oHCFA1500.CF_15_Other_Dates_MM,dtpSimilarIllnessFirstDate.Value.ToString("MM"));
                    oHCFA1500.CF_15_Other_Date_YY.Value = formatedData(oHCFA1500.CF_15_Other_Date_YY,dtpSimilarIllnessFirstDate.Value.ToString(_strYearFormat));
                }
                oHCFA1500.CF_15_Qualifier_No.Value = formatedData(oHCFA1500.CF_15_Qualifier_No, txtSimilarIllnessFirstDateQualifier.Text.Trim());
                if (dtpUnableToWorkFrom.Checked == true)
                {
                    oHCFA1500.CF_16_UnableToWorkFromDate_DD.Value = formatedData(oHCFA1500.CF_16_UnableToWorkFromDate_DD,dtpUnableToWorkFrom.Value.ToString("dd"));
                    oHCFA1500.CF_16_UnableToWorkFromDate_MM.Value = formatedData(oHCFA1500.CF_16_UnableToWorkFromDate_MM,dtpUnableToWorkFrom.Value.ToString("MM"));
                    oHCFA1500.CF_16_UnableToWorkFromDate_YY.Value = formatedData(oHCFA1500.CF_16_UnableToWorkFromDate_YY,dtpUnableToWorkFrom.Value.ToString(_strYearFormat));
                }
                if (dtpUnableToWorkTill.Checked == true)
                {
                    oHCFA1500.CF_16_UnableToWorkTillDate_DD.Value = formatedData(oHCFA1500.CF_16_UnableToWorkTillDate_DD,dtpUnableToWorkTill.Value.ToString("dd"));
                    oHCFA1500.CF_16_UnableToWorkTillDate_MM.Value = formatedData(oHCFA1500.CF_16_UnableToWorkTillDate_MM,dtpUnableToWorkTill.Value.ToString("MM"));
                    oHCFA1500.CF_16_UnableToWorkTillDate_YY.Value = formatedData(oHCFA1500.CF_16_UnableToWorkTillDate_YY,dtpUnableToWorkTill.Value.ToString(_strYearFormat));
                }



                oHCFA1500.CF_17_ReferringProvider_Name.Value = formatedData(oHCFA1500.CF_17_ReferringProvider_Name, txtReferringProviderName.Text.Trim());
                oHCFA1500.CF_17_ReferringProvider_Qaulifier.Value = formatedData(oHCFA1500.CF_17_ReferringProvider_Qaulifier, txtReferringProviderQualifier.Text.Trim());

                oHCFA1500.CF_17a_ReferringProvider_OtherQualifier.Value = formatedData(oHCFA1500.CF_17a_ReferringProvider_OtherQualifier, txtReferringProvider_OtherType.Text.Trim());
                oHCFA1500.CF_17a_ReferringProvider_OtherID.Value = formatedData(oHCFA1500.CF_17a_ReferringProvider_OtherID, txtReferringProvider_OtherValue.Text.Trim());
                oHCFA1500.CF_17b_ReferringProvider_NPI.Value = formatedData(oHCFA1500.CF_17b_ReferringProvider_NPI, txtReferringProvider_NPI.Text.Trim());


                if (dtpHospitalisationFrom.Checked == true)
                {
                    oHCFA1500.CF_18_HospitalizationFromDate_DD.Value = formatedData(oHCFA1500.CF_18_HospitalizationFromDate_DD,dtpHospitalisationFrom.Value.ToString("dd"));
                    oHCFA1500.CF_18_HospitalizationFromDate_MM.Value = formatedData(oHCFA1500.CF_18_HospitalizationFromDate_MM,dtpHospitalisationFrom.Value.ToString("MM"));
                    oHCFA1500.CF_18_HospitalizationFromDate_YY.Value = formatedData(oHCFA1500.CF_18_HospitalizationFromDate_YY,dtpHospitalisationFrom.Value.ToString(_strYearFormat));
                }
                if (dtpHospitalisationTo.Checked == true)
                {

                    oHCFA1500.CF_18_HospitalizationTillDate_DD.Value = formatedData(oHCFA1500.CF_18_HospitalizationTillDate_DD,dtpHospitalisationTo.Value.ToString("dd"));
                    oHCFA1500.CF_18_HospitalizationTillDate_MM.Value = formatedData(oHCFA1500.CF_18_HospitalizationTillDate_MM,dtpHospitalisationTo.Value.ToString("MM"));
                    oHCFA1500.CF_18_HospitalizationTillDate_YY.Value = formatedData(oHCFA1500.CF_18_HospitalizationTillDate_YY,dtpHospitalisationTo.Value.ToString(_strYearFormat));
                }
                oHCFA1500.CF_19_LocalUse_Field.Value = formatedData(oHCFA1500.CF_19_LocalUse_Field, txt19ReservedForLocalUse.Text.Trim());
                oHCFA1500.CF_20_OutsideLab_Charges_Principal.Value = formatedData(oHCFA1500.CF_20_OutsideLab_Charges_Principal, txtOutsideLabCharges1.Text.Trim().PadLeft("##########".Length, '#').Replace("#", "  "));
                oHCFA1500.CF_20_OutsideLab_Charges_Secondary.Value = formatedData(oHCFA1500.CF_20_OutsideLab_Charges_Secondary, txtOutsideLabCharges2.Text.Trim());
                if (chkOutsideLab_No.Checked == true)
                {
                    oHCFA1500.CF_20_OutsideLab_No.Value = true;
                }
                if (chkOutsideLab_Yes.Checked == true)
                {
                    oHCFA1500.CF_20_OutsideLab_Yes.Value = true;
                }

                oHCFA1500.CF_21_Icd_Ind_No.Value = formatedData(oHCFA1500.CF_21_Icd_Ind_No, txtIcdInd1.Text);
                oHCFA1500.CF_21_Icd_Ind.Value = formatedData(oHCFA1500.CF_21_Icd_Ind, txtIcdInd2.Text);
                oHCFA1500.CF_21_Diagnosis_A_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_A_Principal, txtDiagnosisCode11.Text);
                // oHCFA1500.CF_21_Diagnosis_1_Secondary.Value = txtDiagnosisCode12.Text;

                oHCFA1500.CF_21_Diagnosis_B_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_B_Principal, txtDiagnosisCode21.Text);
                // oHCFA1500.CF_21_Diagnosis_2_Secondary.Value = txtDiagnosisCode32.Text;

                oHCFA1500.CF_21_Diagnosis_C_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_C_Principal, txtDiagnosisCode31.Text);
                //  oHCFA1500.CF_21_Diagnosis_3_Secondary.Value = txtDiagnosisCode22.Text;

                oHCFA1500.CF_21_Diagnosis_D_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_D_Principal, txtDiagnosisCode41.Text);
                //   oHCFA1500.CF_21_Diagnosis_4_Secondary.Value = txtDiagnosisCode42.Text;

                oHCFA1500.CF_21_Diagnosis_E_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_E_Principal, txtDiagnosisCodeE.Text);
                oHCFA1500.CF_21_Diagnosis_F_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_F_Principal, txtDiagnosisCodeF.Text);
                oHCFA1500.CF_21_Diagnosis_G_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_G_Principal, txtDiagnosisCodeG.Text);
                oHCFA1500.CF_21_Diagnosis_H_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_H_Principal, txtDiagnosisCodeH.Text);
                oHCFA1500.CF_21_Diagnosis_I_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_I_Principal, txtDiagnosisCodeI.Text);
                oHCFA1500.CF_21_Diagnosis_J_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_J_Principal, txtDiagnosisCodeJ.Text);
                oHCFA1500.CF_21_Diagnosis_K_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_K_Principal, txtDiagnosisCodeK.Text);
                oHCFA1500.CF_21_Diagnosis_L_Principal.Value = formatedData(oHCFA1500.CF_21_Diagnosis_L_Principal, txtDiagnosisCodeL.Text);

                oHCFA1500.CF_22_Resubmission.Value = formatedData(oHCFA1500.CF_22_Resubmission, txtMedicaidResubmissionCode.Text.Trim());
                oHCFA1500.CF_22_Original_Refrence_No.Value = formatedData(oHCFA1500.CF_22_Original_Refrence_No, txtOriginalRefNumber.Text.Trim());
                oHCFA1500.CF_23_PriorAuthorization_No.Value = formatedData(oHCFA1500.CF_23_PriorAuthorization_No, txtPriorAuthorizationNumber.Text.Trim());


                oHCFA1500.CF_24A_L1_DOS_From_DD.Value = formatedData(oHCFA1500.CF_24A_L1_DOS_From_DD,dtpDOS1From.Value.ToString("dd"));
                oHCFA1500.CF_24A_L1_DOS_From_MM.Value = formatedData(oHCFA1500.CF_24A_L1_DOS_From_MM,dtpDOS1From.Value.ToString("MM"));
                oHCFA1500.CF_24A_L1_DOS_From_YY.Value = formatedData(oHCFA1500.CF_24A_L1_DOS_From_YY,dtpDOS1From.Value.ToString("yy"));
                oHCFA1500.CF_24A_L1_DOS_To_DD.Value = formatedData(oHCFA1500.CF_24A_L1_DOS_To_DD,dtpDOS1To.Value.ToString("dd"));
                oHCFA1500.CF_24A_L1_DOS_To_MM.Value = formatedData(oHCFA1500.CF_24A_L1_DOS_To_MM,dtpDOS1To.Value.ToString("MM"));
                oHCFA1500.CF_24A_L1_DOS_To_YY.Value = formatedData(oHCFA1500.CF_24A_L1_DOS_To_YY,dtpDOS1To.Value.ToString("yy"));
                oHCFA1500.CF_24A_L2_DOS_From_DD.Value = formatedData(oHCFA1500.CF_24A_L2_DOS_From_DD,dtpDOS2From.Value.ToString("dd"));
                oHCFA1500.CF_24A_L2_DOS_From_MM.Value = formatedData(oHCFA1500.CF_24A_L2_DOS_From_MM,dtpDOS2From.Value.ToString("MM"));
                oHCFA1500.CF_24A_L2_DOS_From_YY.Value = formatedData(oHCFA1500.CF_24A_L2_DOS_From_YY,dtpDOS2From.Value.ToString("yy"));
                oHCFA1500.CF_24A_L2_DOS_To_DD.Value = formatedData(oHCFA1500.CF_24A_L2_DOS_To_DD,dtpDOS2To.Value.ToString("dd"));
                oHCFA1500.CF_24A_L2_DOS_To_MM.Value = formatedData(oHCFA1500.CF_24A_L2_DOS_To_MM,dtpDOS2To.Value.ToString("MM"));
                oHCFA1500.CF_24A_L2_DOS_To_YY.Value = formatedData(oHCFA1500.CF_24A_L2_DOS_To_YY,dtpDOS2To.Value.ToString("yy"));
                oHCFA1500.CF_24A_L3_DOS_From_DD.Value = formatedData(oHCFA1500.CF_24A_L3_DOS_From_DD,dtpDOS3From.Value.ToString("dd"));
                oHCFA1500.CF_24A_L3_DOS_From_MM.Value = formatedData(oHCFA1500.CF_24A_L3_DOS_From_MM,dtpDOS3From.Value.ToString("MM"));
                oHCFA1500.CF_24A_L3_DOS_From_YY.Value = formatedData(oHCFA1500.CF_24A_L3_DOS_From_YY,dtpDOS3From.Value.ToString("yy"));
                oHCFA1500.CF_24A_L3_DOS_To_DD.Value = formatedData(oHCFA1500.CF_24A_L3_DOS_To_DD,dtpDOS3To.Value.ToString("dd"));
                oHCFA1500.CF_24A_L3_DOS_To_MM.Value = formatedData(oHCFA1500.CF_24A_L3_DOS_To_MM,dtpDOS3To.Value.ToString("MM"));
                oHCFA1500.CF_24A_L3_DOS_To_YY.Value = formatedData(oHCFA1500.CF_24A_L3_DOS_To_YY,dtpDOS3To.Value.ToString("yy"));
                oHCFA1500.CF_24A_L4_DOS_From_DD.Value = formatedData(oHCFA1500.CF_24A_L4_DOS_From_DD,dtpDOS4From.Value.ToString("dd"));
                oHCFA1500.CF_24A_L4_DOS_From_MM.Value = formatedData(oHCFA1500.CF_24A_L4_DOS_From_MM,dtpDOS4From.Value.ToString("MM"));
                oHCFA1500.CF_24A_L4_DOS_From_YY.Value = formatedData(oHCFA1500.CF_24A_L4_DOS_From_YY,dtpDOS4From.Value.ToString("yy"));
                oHCFA1500.CF_24A_L4_DOS_To_DD.Value = formatedData(oHCFA1500.CF_24A_L4_DOS_To_DD,dtpDOS4To.Value.ToString("dd"));
                oHCFA1500.CF_24A_L4_DOS_To_MM.Value = formatedData(oHCFA1500.CF_24A_L4_DOS_To_MM,dtpDOS4To.Value.ToString("MM"));
                oHCFA1500.CF_24A_L4_DOS_To_YY.Value = formatedData(oHCFA1500.CF_24A_L4_DOS_To_YY,dtpDOS4To.Value.ToString("yy"));
                oHCFA1500.CF_24A_L5_DOS_From_DD.Value = formatedData(oHCFA1500.CF_24A_L5_DOS_From_DD,dtpDOS5From.Value.ToString("dd"));
                oHCFA1500.CF_24A_L5_DOS_From_MM.Value =formatedData(oHCFA1500.CF_24A_L5_DOS_From_MM, dtpDOS5From.Value.ToString("MM"));
                oHCFA1500.CF_24A_L5_DOS_From_YY.Value =formatedData(oHCFA1500.CF_24A_L5_DOS_From_YY, dtpDOS5From.Value.ToString("yy"));
                oHCFA1500.CF_24A_L5_DOS_To_DD.Value =formatedData(oHCFA1500.CF_24A_L5_DOS_To_DD, dtpDOS5To.Value.ToString("dd"));
                oHCFA1500.CF_24A_L5_DOS_To_MM.Value = formatedData(oHCFA1500.CF_24A_L5_DOS_To_MM,dtpDOS5To.Value.ToString("MM"));
                oHCFA1500.CF_24A_L5_DOS_To_YY.Value = formatedData(oHCFA1500.CF_24A_L5_DOS_To_YY,dtpDOS5To.Value.ToString("yy"));
                oHCFA1500.CF_24A_L6_DOS_From_DD.Value =formatedData(oHCFA1500.CF_24A_L6_DOS_From_DD, dtpDOS6From.Value.ToString("dd"));
                oHCFA1500.CF_24A_L6_DOS_From_MM.Value = formatedData(oHCFA1500.CF_24A_L6_DOS_From_MM,dtpDOS6From.Value.ToString("MM"));
                oHCFA1500.CF_24A_L6_DOS_From_YY.Value = formatedData(oHCFA1500.CF_24A_L6_DOS_From_YY,dtpDOS6From.Value.ToString("yy"));
                oHCFA1500.CF_24A_L6_DOS_To_DD.Value = formatedData(oHCFA1500.CF_24A_L6_DOS_To_DD,dtpDOS6To.Value.ToString("dd"));
                oHCFA1500.CF_24A_L6_DOS_To_MM.Value = formatedData(oHCFA1500.CF_24A_L6_DOS_To_MM,dtpDOS6To.Value.ToString("MM"));
                oHCFA1500.CF_24A_L6_DOS_To_YY.Value = formatedData(oHCFA1500.CF_24A_L6_DOS_To_YY,dtpDOS6To.Value.ToString("yy"));


                oHCFA1500.CF_24B_L1_POS_Code.Value = formatedData(oHCFA1500.CF_24B_L1_POS_Code, txtPOS1.Text);
                oHCFA1500.CF_24B_L2_POS_Code.Value = formatedData(oHCFA1500.CF_24B_L2_POS_Code, txtPOS2.Text);
                oHCFA1500.CF_24B_L3_POS_Code.Value = formatedData(oHCFA1500.CF_24B_L3_POS_Code, txtPOS3.Text);
                oHCFA1500.CF_24B_L4_POS_Code.Value = formatedData(oHCFA1500.CF_24B_L4_POS_Code, txtPOS4.Text);
                oHCFA1500.CF_24B_L5_POS_Code.Value = formatedData(oHCFA1500.CF_24B_L5_POS_Code, txtPOS5.Text);
                oHCFA1500.CF_24B_L6_POS_Code.Value = formatedData(oHCFA1500.CF_24B_L6_POS_Code, txtPOS6.Text);


                oHCFA1500.CF_24C_L1_EMG_Code.Value = formatedData(oHCFA1500.CF_24C_L1_EMG_Code, txtEMG1.Text);
                oHCFA1500.CF_24C_L2_EMG_Code.Value = formatedData(oHCFA1500.CF_24C_L2_EMG_Code, txtEMG2.Text);
                oHCFA1500.CF_24C_L3_EMG_Code.Value = formatedData(oHCFA1500.CF_24C_L3_EMG_Code, txtEMG3.Text);
                oHCFA1500.CF_24C_L4_EMG_Code.Value = formatedData(oHCFA1500.CF_24C_L4_EMG_Code, txtEMG4.Text);
                oHCFA1500.CF_24C_L5_EMG_Code.Value = formatedData(oHCFA1500.CF_24C_L5_EMG_Code, txtEMG5.Text);
                oHCFA1500.CF_24C_L6_EMG_Code.Value = formatedData(oHCFA1500.CF_24C_L6_EMG_Code, txtEMG6.Text);


                oHCFA1500.CF_24D_L1_CPT_HCPCS_Code.Value = formatedData(oHCFA1500.CF_24D_L1_CPT_HCPCS_Code, txtCPT1.Text);
                oHCFA1500.CF_24D_L2_CPT_HCPCS_Code.Value = formatedData(oHCFA1500.CF_24D_L2_CPT_HCPCS_Code, txtCPT2.Text);
                oHCFA1500.CF_24D_L3_CPT_HCPCS_Code.Value = formatedData(oHCFA1500.CF_24D_L3_CPT_HCPCS_Code, txtCPT3.Text);
                oHCFA1500.CF_24D_L4_CPT_HCPCS_Code.Value = formatedData(oHCFA1500.CF_24D_L4_CPT_HCPCS_Code, txtCPT4.Text);
                oHCFA1500.CF_24D_L5_CPT_HCPCS_Code.Value = formatedData(oHCFA1500.CF_24D_L5_CPT_HCPCS_Code, txtCPT5.Text);
                oHCFA1500.CF_24D_L6_CPT_HCPCS_Code.Value = formatedData(oHCFA1500.CF_24D_L6_CPT_HCPCS_Code, txtCPT6.Text);

                if (txtCPT1.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line1 = true; }
                if (txtCPT2.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line2 = true; }
                if (txtCPT3.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line3 = true; }
                if (txtCPT4.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line4 = true; }
                if (txtCPT5.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line5 = true; }
                if (txtCPT6.Text.Trim() != "") { oHCFA1500.CF_IsPresent_Line6 = true; }

                oHCFA1500.CF_24D_L1_Modifier_1_Code.Value = formatedData(oHCFA1500.CF_24D_L1_Modifier_1_Code, txtMOD11.Text);
                oHCFA1500.CF_24D_L1_Modifier_2_Code.Value = formatedData(oHCFA1500.CF_24D_L1_Modifier_2_Code, txtMOD12.Text);
                oHCFA1500.CF_24D_L1_Modifier_3_Code.Value = formatedData(oHCFA1500.CF_24D_L1_Modifier_3_Code, txtMOD13.Text);
                oHCFA1500.CF_24D_L1_Modifier_4_Code.Value = formatedData(oHCFA1500.CF_24D_L1_Modifier_4_Code, txtMOD14.Text);
                oHCFA1500.CF_24D_L2_Modifier_1_Code.Value = formatedData(oHCFA1500.CF_24D_L2_Modifier_1_Code, txtMOD21.Text);
                oHCFA1500.CF_24D_L2_Modifier_2_Code.Value = formatedData(oHCFA1500.CF_24D_L2_Modifier_2_Code, txtMOD22.Text);
                oHCFA1500.CF_24D_L2_Modifier_3_Code.Value = formatedData(oHCFA1500.CF_24D_L2_Modifier_3_Code, txtMOD23.Text);
                oHCFA1500.CF_24D_L2_Modifier_4_Code.Value = formatedData(oHCFA1500.CF_24D_L2_Modifier_4_Code, txtMOD24.Text);
                oHCFA1500.CF_24D_L3_Modifier_1_Code.Value = formatedData(oHCFA1500.CF_24D_L3_Modifier_1_Code, txtMOD31.Text);
                oHCFA1500.CF_24D_L3_Modifier_2_Code.Value = formatedData(oHCFA1500.CF_24D_L3_Modifier_2_Code, txtMOD32.Text);
                oHCFA1500.CF_24D_L3_Modifier_3_Code.Value = formatedData(oHCFA1500.CF_24D_L3_Modifier_3_Code, txtMOD33.Text);
                oHCFA1500.CF_24D_L3_Modifier_4_Code.Value = formatedData(oHCFA1500.CF_24D_L3_Modifier_4_Code, txtMOD34.Text);
                oHCFA1500.CF_24D_L4_Modifier_1_Code.Value = formatedData(oHCFA1500.CF_24D_L4_Modifier_1_Code, txtMOD41.Text);
                oHCFA1500.CF_24D_L4_Modifier_2_Code.Value = formatedData(oHCFA1500.CF_24D_L4_Modifier_2_Code, txtMOD42.Text);
                oHCFA1500.CF_24D_L4_Modifier_3_Code.Value = formatedData(oHCFA1500.CF_24D_L4_Modifier_3_Code, txtMOD43.Text);
                oHCFA1500.CF_24D_L4_Modifier_4_Code.Value = formatedData(oHCFA1500.CF_24D_L4_Modifier_4_Code, txtMOD44.Text);
                oHCFA1500.CF_24D_L5_Modifier_1_Code.Value = formatedData(oHCFA1500.CF_24D_L5_Modifier_1_Code, txtMOD51.Text);
                oHCFA1500.CF_24D_L5_Modifier_2_Code.Value = formatedData(oHCFA1500.CF_24D_L5_Modifier_2_Code, txtMOD52.Text);
                oHCFA1500.CF_24D_L5_Modifier_3_Code.Value = formatedData(oHCFA1500.CF_24D_L5_Modifier_3_Code, txtMOD53.Text);
                oHCFA1500.CF_24D_L5_Modifier_4_Code.Value = formatedData(oHCFA1500.CF_24D_L5_Modifier_4_Code, txtMOD54.Text);
                oHCFA1500.CF_24D_L6_Modifier_1_Code.Value = formatedData(oHCFA1500.CF_24D_L6_Modifier_1_Code, txtMOD61.Text);
                oHCFA1500.CF_24D_L6_Modifier_2_Code.Value = formatedData(oHCFA1500.CF_24D_L6_Modifier_2_Code, txtMOD62.Text);
                oHCFA1500.CF_24D_L6_Modifier_3_Code.Value = formatedData(oHCFA1500.CF_24D_L6_Modifier_3_Code, txtMOD63.Text);
                oHCFA1500.CF_24D_L6_Modifier_4_Code.Value = formatedData(oHCFA1500.CF_24D_L6_Modifier_4_Code, txtMOD64.Text);

                oHCFA1500.CF_24E_L1_Diagnosis_Pointers.Value = formatedData(oHCFA1500.CF_24E_L1_Diagnosis_Pointers, txtDxPtr1.Text);
                oHCFA1500.CF_24E_L2_Diagnosis_Pointers.Value = formatedData(oHCFA1500.CF_24E_L2_Diagnosis_Pointers, txtDxPtr2.Text);
                oHCFA1500.CF_24E_L3_Diagnosis_Pointers.Value = formatedData(oHCFA1500.CF_24E_L3_Diagnosis_Pointers, txtDxPtr3.Text);
                oHCFA1500.CF_24E_L4_Diagnosis_Pointers.Value = formatedData(oHCFA1500.CF_24E_L4_Diagnosis_Pointers, txtDxPtr4.Text);
                oHCFA1500.CF_24E_L5_Diagnosis_Pointers.Value = formatedData(oHCFA1500.CF_24E_L5_Diagnosis_Pointers, txtDxPtr5.Text);
                oHCFA1500.CF_24E_L6_Diagnosis_Pointers.Value = formatedData(oHCFA1500.CF_24E_L6_Diagnosis_Pointers, txtDxPtr6.Text);


                oHCFA1500.CF_24F_L1_Charges_Principal.Value = formatedData(oHCFA1500.CF_24F_L1_Charges_Principal, txtCharges1.Text.PadLeft("#######".Length, '#').Replace("#", "  "));
                oHCFA1500.CF_24F_L1_Charges_Secondary.Value = formatedData(oHCFA1500.CF_24F_L1_Charges_Secondary, txtCharges11.Text);
                oHCFA1500.CF_24F_L2_Charges_Principal.Value = formatedData(oHCFA1500.CF_24F_L2_Charges_Principal, txtCharges2.Text.PadLeft("#######".Length, '#').Replace("#", "  "));
                oHCFA1500.CF_24F_L2_Charges_Secondary.Value = formatedData(oHCFA1500.CF_24F_L2_Charges_Secondary, txtCharges21.Text);
                oHCFA1500.CF_24F_L3_Charges_Principal.Value = formatedData(oHCFA1500.CF_24F_L3_Charges_Principal, txtCharges3.Text.PadLeft("#######".Length, '#').Replace("#", "  "));
                oHCFA1500.CF_24F_L3_Charges_Secondary.Value = formatedData(oHCFA1500.CF_24F_L3_Charges_Secondary, txtCharges31.Text);
                oHCFA1500.CF_24F_L4_Charges_Principal.Value = formatedData(oHCFA1500.CF_24F_L4_Charges_Principal, txtCharges4.Text.PadLeft("#######".Length, '#').Replace("#", "  "));
                oHCFA1500.CF_24F_L4_Charges_Secondary.Value = formatedData(oHCFA1500.CF_24F_L4_Charges_Secondary, txtCharges41.Text);
                oHCFA1500.CF_24F_L5_Charges_Principal.Value = formatedData(oHCFA1500.CF_24F_L5_Charges_Principal, txtCharges5.Text.PadLeft("#######".Length, '#').Replace("#", "  "));
                oHCFA1500.CF_24F_L5_Charges_Secondary.Value = formatedData(oHCFA1500.CF_24F_L5_Charges_Secondary, txtCharges51.Text);
                oHCFA1500.CF_24F_L6_Charges_Principal.Value = formatedData(oHCFA1500.CF_24F_L6_Charges_Principal, txtCharges6.Text.PadLeft("#######".Length, '#').Replace("#", "  "));
                oHCFA1500.CF_24F_L6_Charges_Secondary.Value = formatedData(oHCFA1500.CF_24F_L6_Charges_Secondary, txtCharges61.Text);

                oHCFA1500.CF_24G_L1_Days_Units.Value = formatedData(oHCFA1500.CF_24G_L1_Days_Units, txtUnits1.Text);
                oHCFA1500.CF_24G_L2_Days_Units.Value = formatedData(oHCFA1500.CF_24G_L2_Days_Units, txtUnits2.Text);
                oHCFA1500.CF_24G_L3_Days_Units.Value = formatedData(oHCFA1500.CF_24G_L3_Days_Units, txtUnits3.Text);
                oHCFA1500.CF_24G_L4_Days_Units.Value = formatedData(oHCFA1500.CF_24G_L4_Days_Units, txtUnits4.Text);
                oHCFA1500.CF_24G_L5_Days_Units.Value = formatedData(oHCFA1500.CF_24G_L5_Days_Units, txtUnits5.Text);
                oHCFA1500.CF_24G_L6_Days_Units.Value = formatedData(oHCFA1500.CF_24G_L6_Days_Units, txtUnits6.Text);

                oHCFA1500.CF_24H_L1_EPSDT_FamilyPlan_Shaded.Value = formatedData(oHCFA1500.CF_24H_L1_EPSDT_FamilyPlan_Shaded, txtEPSDTShaded1.Text.ToString().Length > 2 ? txtEPSDTShaded1.Text.ToString().Substring(0, 2) : txtEPSDTShaded1.Text.ToString());
                oHCFA1500.CF_24H_L2_EPSDT_FamilyPlan_Shaded.Value = formatedData(oHCFA1500.CF_24H_L2_EPSDT_FamilyPlan_Shaded, txtEPSDTShaded2.Text.ToString().Length > 2 ? txtEPSDTShaded2.Text.ToString().Substring(0, 2) : txtEPSDTShaded2.Text.ToString());
                oHCFA1500.CF_24H_L3_EPSDT_FamilyPlan_Shaded.Value = formatedData(oHCFA1500.CF_24H_L3_EPSDT_FamilyPlan_Shaded, txtEPSDTShaded3.Text.ToString().Length > 2 ? txtEPSDTShaded3.Text.ToString().Substring(0, 2) : txtEPSDTShaded3.Text.ToString());
                oHCFA1500.CF_24H_L4_EPSDT_FamilyPlan_Shaded.Value = formatedData(oHCFA1500.CF_24H_L4_EPSDT_FamilyPlan_Shaded, txtEPSDTShaded4.Text.ToString().Length > 2 ? txtEPSDTShaded4.Text.ToString().Substring(0, 2) : txtEPSDTShaded4.Text.ToString());
                oHCFA1500.CF_24H_L5_EPSDT_FamilyPlan_Shaded.Value = formatedData(oHCFA1500.CF_24H_L5_EPSDT_FamilyPlan_Shaded, txtEPSDTShaded5.Text.ToString().Length > 2 ? txtEPSDTShaded5.Text.ToString().Substring(0, 2) : txtEPSDTShaded5.Text.ToString());
                oHCFA1500.CF_24H_L6_EPSDT_FamilyPlan_Shaded.Value = formatedData(oHCFA1500.CF_24H_L6_EPSDT_FamilyPlan_Shaded, txtEPSDTShaded6.Text.ToString().Length > 2 ? txtEPSDTShaded6.Text.ToString().Substring(0, 2) : txtEPSDTShaded6.Text.ToString());

                oHCFA1500.CF_24H_L1_EPSDT_FamilyPlan.Value = formatedData(oHCFA1500.CF_24H_L1_EPSDT_FamilyPlan, txtEPSDT1.Text.ToString().Length > 2 ? txtEPSDT1.Text.ToString().Substring(0, 2) : txtEPSDT1.Text.ToString());
                oHCFA1500.CF_24H_L2_EPSDT_FamilyPlan.Value = formatedData(oHCFA1500.CF_24H_L2_EPSDT_FamilyPlan, txtEPSDT2.Text.ToString().Length > 2 ? txtEPSDT2.Text.ToString().Substring(0, 2) : txtEPSDT2.Text.ToString());
                oHCFA1500.CF_24H_L3_EPSDT_FamilyPlan.Value = formatedData(oHCFA1500.CF_24H_L3_EPSDT_FamilyPlan, txtEPSDT3.Text.ToString().Length > 2 ? txtEPSDT3.Text.ToString().Substring(0, 2) : txtEPSDT3.Text.ToString());
                oHCFA1500.CF_24H_L4_EPSDT_FamilyPlan.Value = formatedData(oHCFA1500.CF_24H_L4_EPSDT_FamilyPlan, txtEPSDT4.Text.ToString().Length > 2 ? txtEPSDT4.Text.ToString().Substring(0, 2) : txtEPSDT4.Text.ToString());
                oHCFA1500.CF_24H_L5_EPSDT_FamilyPlan.Value = formatedData(oHCFA1500.CF_24H_L5_EPSDT_FamilyPlan, txtEPSDT5.Text.ToString().Length > 2 ? txtEPSDT5.Text.ToString().Substring(0, 2) : txtEPSDT5.Text.ToString());
                oHCFA1500.CF_24H_L6_EPSDT_FamilyPlan.Value = formatedData(oHCFA1500.CF_24H_L6_EPSDT_FamilyPlan, txtEPSDT6.Text.ToString().Length > 2 ? txtEPSDT6.Text.ToString().Substring(0, 2) : txtEPSDT6.Text.ToString());

                oHCFA1500.CF_24J_L1_RenderingProvider_NPI.Value = formatedData(oHCFA1500.CF_24J_L1_RenderingProvider_NPI, txtRenderingProvider1_NPI.Text);
                oHCFA1500.CF_24J_L2_RenderingProvider_NPI.Value = formatedData(oHCFA1500.CF_24J_L2_RenderingProvider_NPI, txtRenderingProvider2_NPI.Text);
                oHCFA1500.CF_24J_L3_RenderingProvider_NPI.Value = formatedData(oHCFA1500.CF_24J_L3_RenderingProvider_NPI, txtRenderingProvider3_NPI.Text);
                oHCFA1500.CF_24J_L4_RenderingProvider_NPI.Value = formatedData(oHCFA1500.CF_24J_L4_RenderingProvider_NPI, txtRenderingProvider4_NPI.Text);
                oHCFA1500.CF_24J_L5_RenderingProvider_NPI.Value = formatedData(oHCFA1500.CF_24J_L5_RenderingProvider_NPI, txtRenderingProvider5_NPI.Text);
                oHCFA1500.CF_24J_L6_RenderingProvider_NPI.Value = formatedData(oHCFA1500.CF_24J_L6_RenderingProvider_NPI, txtRenderingProvider6_NPI.Text);

                oHCFA1500.CF_24J_L1_RenderingProvider_OtherQualifier.Value = formatedData(oHCFA1500.CF_24J_L1_RenderingProvider_OtherQualifier, txtRenderingProvider1_Qualifier.Text);
                oHCFA1500.CF_24J_L2_RenderingProvider_OtherQualifier.Value = formatedData(oHCFA1500.CF_24J_L2_RenderingProvider_OtherQualifier, txtRenderingProvider2_Qualifier.Text);
                oHCFA1500.CF_24J_L3_RenderingProvider_OtherQualifier.Value = formatedData(oHCFA1500.CF_24J_L3_RenderingProvider_OtherQualifier, txtRenderingProvider3_Qualifier.Text);
                oHCFA1500.CF_24J_L4_RenderingProvider_OtherQualifier.Value = formatedData(oHCFA1500.CF_24J_L4_RenderingProvider_OtherQualifier, txtRenderingProvider4_Qualifier.Text);
                oHCFA1500.CF_24J_L5_RenderingProvider_OtherQualifier.Value = formatedData(oHCFA1500.CF_24J_L5_RenderingProvider_OtherQualifier, txtRenderingProvider5_Qualifier.Text);
                oHCFA1500.CF_24J_L6_RenderingProvider_OtherQualifier.Value = formatedData(oHCFA1500.CF_24J_L6_RenderingProvider_OtherQualifier, txtRenderingProvider6_Qualifier.Text);

                oHCFA1500.CF_24J_L1_RenderingProvider_OtherQualifiervalue.Value = formatedData(oHCFA1500.CF_24J_L1_RenderingProvider_OtherQualifiervalue, txtRenderingProvider1_QualifierValue.Text);
                oHCFA1500.CF_24J_L2_RenderingProvider_OtherQualifiervalue.Value = formatedData(oHCFA1500.CF_24J_L2_RenderingProvider_OtherQualifiervalue, txtRenderingProvider2_QualifierValue.Text);
                oHCFA1500.CF_24J_L3_RenderingProvider_OtherQualifiervalue.Value = formatedData(oHCFA1500.CF_24J_L3_RenderingProvider_OtherQualifiervalue, txtRenderingProvider3_QualifierValue.Text);
                oHCFA1500.CF_24J_L4_RenderingProvider_OtherQualifiervalue.Value = formatedData(oHCFA1500.CF_24J_L4_RenderingProvider_OtherQualifiervalue, txtRenderingProvider4_QualifierValue.Text);
                oHCFA1500.CF_24J_L5_RenderingProvider_OtherQualifiervalue.Value = formatedData(oHCFA1500.CF_24J_L5_RenderingProvider_OtherQualifiervalue, txtRenderingProvider5_QualifierValue.Text);
                oHCFA1500.CF_24J_L6_RenderingProvider_OtherQualifiervalue.Value = formatedData(oHCFA1500.CF_24J_L6_RenderingProvider_OtherQualifiervalue, txtRenderingProvider6_QualifierValue.Text);

                oHCFA1500.CF_24A_L1_Note.Value = formatedData(oHCFA1500.CF_24A_L1_Note, txtNotes1.Text.Trim());
                oHCFA1500.CF_24A_L2_Note.Value = formatedData(oHCFA1500.CF_24A_L2_Note, txtNotes2.Text.Trim());
                oHCFA1500.CF_24A_L3_Note.Value = formatedData(oHCFA1500.CF_24A_L3_Note, txtNotes3.Text.Trim());
                oHCFA1500.CF_24A_L4_Note.Value = formatedData(oHCFA1500.CF_24A_L4_Note, txtNotes4.Text.Trim());
                oHCFA1500.CF_24A_L5_Note.Value = formatedData(oHCFA1500.CF_24A_L5_Note, txtNotes5.Text.Trim());
                oHCFA1500.CF_24A_L6_Note.Value = formatedData(oHCFA1500.CF_24A_L6_Note, txtNotes6.Text.Trim());

                oHCFA1500.CF_25_FederalTax_ID_No.Value = formatedData(oHCFA1500.CF_25_FederalTax_ID_No, txtFederalTaxID.Text);
                if (chkFederalTaxID_EIN.Checked == true)
                {
                    oHCFA1500.CF_25_FederalTaxID_Qualifier_EIN.Value = true;
                }
                if (chkFederalTaxID_SSN.Checked == true)
                {
                    oHCFA1500.CF_25_FederalTaxID_Qualifier_SSN.Value = true;
                }

                oHCFA1500.CF_26_PatientAccount_No.Value = formatedData(oHCFA1500.CF_26_PatientAccount_No, txtPatientAccountNo.Text);
                if (chkAcceptAssignment_No.Checked == true)
                {
                    oHCFA1500.CF_27_AcceptAssignment_NO.Value = true;
                }
                if (chkAcceptAssignment_Yes.Checked == true)
                {
                    oHCFA1500.CF_27_AcceptAssignment_YES.Value = true;
                }

                oHCFA1500.CF_28_TotalCharge_Principal.Value = formatedData(oHCFA1500.CF_28_TotalCharge_Principal, txtTotalCharges.Text).PadLeft("##########".Length, '#').Replace("#", "  ");
                oHCFA1500.CF_28_TotalCharge_Secondary.Value = formatedData(oHCFA1500.CF_28_TotalCharge_Secondary, txtTotalCharges2.Text);
                oHCFA1500.CF_29_AmountPaid_Principal.Value = formatedData(oHCFA1500.CF_29_AmountPaid_Principal, txtAmountPaid.Text).PadLeft("##########".Length, '#').Replace("#", "  ");
                oHCFA1500.CF_29_AmountPaid_Secondary.Value = formatedData(oHCFA1500.CF_29_AmountPaid_Secondary, txtAmountPaid2.Text);
                //oHCFA1500.CF_30_BalanceDue_Principal.Value = txtBalanceDue.Text.PadLeft("##########".Length, '#').Replace("#", "  ");
                //oHCFA1500.CF_30_BalanceDue_Secondary.Value = txtBalanceDue2.Text;

                oHCFA1500.CF_31_Physician_Supplier_Signature.Value = formatedData(oHCFA1500.CF_31_Physician_Supplier_Signature, txtPhyscianSignature.Text);
                oHCFA1500.CF_31_Physician_Supplier_Signature_Date.Value =formatedData(oHCFA1500.CF_31_Physician_Supplier_Signature_Date, dtpPhysicianSignDate.Value.ToString(_strWholeDateFormat));
                oHCFA1500.CF_31_Physician_Supplier_QualifierValue.Value = formatedData(oHCFA1500.CF_31_Physician_Supplier_QualifierValue, txtPhyscianQualifierValue.Text);

                string[] FacilityInfo = txtFacilityInfo.Text.Split('\n');

                if (FacilityInfo.Length > 0)
                {
                    //if (FacilityInfo.Length >= 3)
                    //{
                    oHCFA1500.CF_32_Service_Facility_Name.Value = formatedData(oHCFA1500.CF_32_Service_Facility_Name, FacilityInfo[0]);
                    if (FacilityInfo.Length > 1)
                    {
                        oHCFA1500.CF_32_Service_Facility_Address_Line1.Value = formatedData(oHCFA1500.CF_32_Service_Facility_Address_Line1, FacilityInfo[1]);
                    }
                    if (FacilityInfo.Length > 2)
                    {
                        oHCFA1500.CF_32_Service_Facility_Address_Line2.Value = formatedData(oHCFA1500.CF_32_Service_Facility_Address_Line2, FacilityInfo[2]);
                    }
                }
                //}

                oHCFA1500.CF_32a_Service_Facility_NPI.Value = formatedData(oHCFA1500.CF_32a_Service_Facility_NPI, txtFacility_a_NPI.Text);
                oHCFA1500.CF_32b_Service_Facility_UPIN_OtherID.Value = formatedData(oHCFA1500.CF_32b_Service_Facility_UPIN_OtherID, txtFacility_b.Text);

                string[] BillingProviderInfo = txtBillingProviderInfo.Text.Split('\n');

                //Commented and Added on 27th Nov 2010

                //if (BillingProviderInfo.Length >= 3)
                //{
                //    oHCFA1500.CF_33_BillingProvider_Name.Value = BillingProviderInfo[0];
                //    oHCFA1500.CF_33_BillingProvider_Address_Line1.Value = BillingProviderInfo[1];
                //    oHCFA1500.CF_33_BillingProvider_Address_Line2.Value = BillingProviderInfo[2];
                //}

                if (BillingProviderInfo.Length > 0)
                {
                    //if (FacilityInfo.Length >= 3)
                    //{
                    oHCFA1500.CF_33_BillingProvider_Name.Value = formatedData(oHCFA1500.CF_33_BillingProvider_Name, BillingProviderInfo[0]);
                    if (BillingProviderInfo.Length > 1)
                    {
                        oHCFA1500.CF_33_BillingProvider_Address_Line1.Value = formatedData(oHCFA1500.CF_33_BillingProvider_Address_Line1, BillingProviderInfo[1]);
                    }
                    if (BillingProviderInfo.Length > 2)
                    {
                        oHCFA1500.CF_33_BillingProvider_Address_Line2.Value = formatedData(oHCFA1500.CF_33_BillingProvider_Address_Line2, BillingProviderInfo[2]);
                    }
                }

                //**

                oHCFA1500.CF_33_BillingProvider_Tel_AreaCode.Value = formatedData(oHCFA1500.CF_33_BillingProvider_Tel_AreaCode, txtBillingProviderPhone1.Text);
                oHCFA1500.CF_33_BillingProvider_Tel_Number.Value = formatedData(oHCFA1500.CF_33_BillingProvider_Tel_Number, txtBillingProviderPhone2.Text);
                oHCFA1500.CF_33a_BillingProvider_NPI.Value = formatedData(oHCFA1500.CF_33a_BillingProvider_NPI, txtBillingProv_a_NPI.Text);
                oHCFA1500.CF_33b_BillingProvider_UPIN_OtherID.Value = formatedData(oHCFA1500.CF_33b_BillingProvider_UPIN_OtherID, txtBillingProv_b_UPIN.Text);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                oHCFA1500 = null;
            }
            finally
            {
            }
            return oHCFA1500;
        }
        #endregion

        private void printdoc_HCFA1500_BeginPrint(object sender, PrintEventArgs e)
        {

        }

        private void printdoc_HCFA1500_EndPrint(object sender, PrintEventArgs e)
        {

        }

        private void printdoc_HCFA1500_PrintPage(object sender, PrintPageEventArgs e)
        {
            int _width = e.PageSettings.Bounds.Width - 30;
            int _height = e.PageSettings.Bounds.Height;
            int _X = e.PageSettings.Bounds.X;
            int _Y = e.PageSettings.Bounds.Y;
            if (!_IsPrintForm)
            {
                double cardHeight = 0;
                double cardWidth = 0;
                //Dim Offset As Int64 = 10    
                Rectangle MyBounds = e.MarginBounds;
                Size MySize = new Size((int)e.Graphics.DpiX, (int)e.Graphics.DpiY);
                Rectangle MarginRect = new Rectangle(0, 1, 1, 1);
                MarginRect.Y = MarginRect.Y * MySize.Height / 72;
                MarginRect.X = MarginRect.X * MySize.Width / 72;
                MarginRect.Height = MarginRect.Height * MySize.Height / 72;
                MarginRect.Width = MarginRect.Width * MySize.Width / 72;

                MyBounds.Y = MyBounds.Y + MarginRect.Top;
                MyBounds.X = MyBounds.X + MarginRect.Left;
                MyBounds.Height = MyBounds.Height * MySize.Height / 72 - (2 * MarginRect.Height);
                MyBounds.Width = MyBounds.Width * MySize.Width / 72 - (2 * MarginRect.Width);
                Int64 y = -10; // e.PageSettings.Bounds.Y; //MyBounds.Top; 
                Int64 x = -120; // e.PageSettings.Bounds.X; //MyBounds.Left;

                System.Drawing.Image thisImage = System.Drawing.Image.FromFile(_OutputFilePath);

                GraphicsUnit units = e.Graphics.PageUnit; //graphicsunits
                RectangleF thisImageBound = thisImage.GetBounds(ref units);
                e.Graphics.PageUnit = units;
                //thisImageBound.Height = 2181;
                cardHeight = thisImageBound.Height * MySize.Height / thisImage.VerticalResolution;
                cardWidth = thisImageBound.Width * MySize.Width / thisImage.HorizontalResolution;
                double ScaleX = MyBounds.Width / cardWidth;
                double ScaleY = MyBounds.Height / cardHeight;
                double ScaleZ = 1.0;
                if (ScaleZ < ScaleX) ScaleZ = ScaleX;
                if (ScaleZ < ScaleY) ScaleZ = ScaleY;
                cardHeight *= ScaleZ; ;
                cardWidth *= ScaleZ;
                e.Graphics.DrawImage(thisImage, x, y, Convert.ToInt64(cardWidth), Convert.ToInt64(cardHeight));
            }
            else
            {
                System.Drawing.Image thisImage = System.Drawing.Image.FromFile(_OutputFilePath);
                e.Graphics.DrawImage(thisImage, _X, _Y, _width, _height);
            }
        }
        //SLR: added on 4/28 to get resolution dependent tiff due to above print logic which is printerspecific, resolution specific transformation
        private float _DpiX = 96f;
        private float _DpiY = 96f;
        private Rectangle _Bounds;
        private Rectangle _MarginBounds;
        private Image _thisImage = null;
        private float _cPiX = 96f;
        private float _cPiY = 96f;

        bool toCreateEMF = gloGlobal.gloTSPrint.UseEMFForClaims && gloGlobal.gloTSPrint.isCopyPrint;
        //private int CreateEMFForCMS1500Form(Graphics thisGraphics, float bmpWidth, float bmpHeight)
        //{
        //    try
        //    {
        //        thisGraphics.Clear(Color.White);
        //        FitImageToScaleAndKeepAtCenter(thisGraphics);

        //        return 0;
        //    }
        //    catch
        //    {
        //        return 1;
        //    }

        //}
        private int CreateEMFForCMS1500FormWithLesserResolution(Graphics thisGraphics, float bmpWidth, float bmpHeight)
        {
            try
            {
                thisGraphics.Clear(Color.White);
                FitToImageWithLesserResolutionToScaleAndKeepAtCenter(thisGraphics);

                return 0;
            }
            catch
            {
                return 1;
            }

        }

        //private string printdoc_HCFA1500_Conversion(clsPrintDocumentConversion oConversion)
        //{
        //    float PaperWidth = oConversion.PaperWidth;
        //    float PaperHeight = oConversion.PaperHeight;
        //    _DpiX = oConversion.DpiX;
        //    _DpiY = oConversion.DpiY;
        //    _Bounds = new Rectangle(oConversion.BoundX, oConversion.BoundY, oConversion.BoundWidth, oConversion.BoundHeight);
        //    _MarginBounds = new Rectangle(oConversion.MarginBoundsX, oConversion.MarginBoundsY, oConversion.MarginBoundsWidth, oConversion.MarginBoundsHeight);


        //    bool bAnyError = false;
        //    int bmpWidht = 0;
        //    int bmpHeight = 0;

        //    bmpWidht = Convert.ToInt32(PaperWidth * _DpiX);
        //    bmpHeight = Convert.ToInt32(PaperHeight * _DpiY);

        //    try
        //    {
        //        byte[] emfBytes = null;
        //        using (System.Drawing.Bitmap NewBitmap = new Bitmap(bmpWidht, bmpHeight))
        //        {
        //            try
        //            {
        //                NewBitmap.SetResolution(_DpiX, _DpiY);
        //                if (toCreateEMF)
        //                {
        //                    emfBytes = gloGlobal.CreateEMF.GetEMFBytes((float)NewBitmap.Width / NewBitmap.HorizontalResolution, (float)NewBitmap.Height / NewBitmap.VerticalResolution, NewBitmap.Width, NewBitmap.Height, CreateEMFForCMS1500Form);
        //                }
        //                else
        //                {
        //                    using (System.Drawing.Graphics eGraphics = Graphics.FromImage(NewBitmap))
        //                    {
        //                        FitImageToScaleAndKeepAtCenter(eGraphics);
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                if (toCreateEMF)
        //                {
        //                    toCreateEMF = false;
        //                    try
        //                    {
        //                        using (System.Drawing.Graphics eGraphics = Graphics.FromImage(NewBitmap))
        //                        {
        //                            FitImageToScaleAndKeepAtCenter(eGraphics);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        bAnyError = true;
        //                        gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during scaling tiff file at printdoc_HCFA1500_Conversion method", false);
        //                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
        //                    }
        //                }
        //            }
        //            if (bAnyError)
        //            {
        //                return _OutputFilePath;
        //            }
        //            else
        //            {


        //                string _printFilePath = "";
        //                string sPath = "";
        //                try
        //                {
        //                    sPath = gloSettings.FolderSettings.AppTempFolderPath + "Paper_Claim_Files";

        //                    if (System.IO.Directory.Exists(sPath) == false)
        //                    {
        //                        System.IO.Directory.CreateDirectory(sPath);
        //                    }

        //                    if (_IsPrintForm == true)
        //                        _printFilePath = sPath + "\\" + DateTime.Now.ToString("yyyyMMddhhmmsstt") + "f"+ (toCreateEMF ? ".emf" : ".jpg");
        //                    else
        //                        _printFilePath = sPath + "\\" + DateTime.Now.ToString("yyyyMMddhhmmsstt") + "f_BLANK"+ (toCreateEMF ? ".emf" : ".tif");

        //                    if (File.Exists(_printFilePath) == true) { File.Delete(_printFilePath); }

        //                    if (toCreateEMF)
        //                    {
        //                        File.WriteAllBytes(_printFilePath, emfBytes);
        //                    }
        //                    else
        //                    {
        //                        NewBitmap.Save(_printFilePath, _IsPrintForm ? System.Drawing.Imaging.ImageFormat.Jpeg : System.Drawing.Imaging.ImageFormat.Tiff);
        //                    }
        //                    return _printFilePath;

        //                }
        //                catch (Exception ex)
        //                {
        //                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during new bitmap save at printdoc_HCFA1500_Conversion method", false);
        //                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
        //                    return _OutputFilePath;

        //                }



        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
        //    }
        //    return "";
        //}

        //private void FitImageToScaleAndKeepAtCenter(System.Drawing.Graphics eGraphics)
        //{
        //    int _width = _Bounds.Width - 30;
        //    int _height = _Bounds.Height;
        //    int _X = _Bounds.X;
        //    int _Y = _Bounds.Y;
        //    if (!_IsPrintForm)
        //    {
        //        double cardHeight = 0;
        //        double cardWidth = 0;
        //        //Dim Offset As Int64 = 10    
        //        Rectangle MyBounds = _MarginBounds;
        //        Size MySize = new Size((int)_DpiX, (int)_DpiY);
        //        Rectangle MarginRect = new Rectangle(0, 1, 1, 1);
        //        MarginRect.Y = MarginRect.Y * MySize.Height / 72;
        //        MarginRect.X = MarginRect.X * MySize.Width / 72;
        //        MarginRect.Height = MarginRect.Height * MySize.Height / 72;
        //        MarginRect.Width = MarginRect.Width * MySize.Width / 72;

        //        MyBounds.Y = MyBounds.Y + MarginRect.Top;
        //        MyBounds.X = MyBounds.X + MarginRect.Left;
        //        MyBounds.Height = MyBounds.Height * MySize.Height / 72 - (2 * MarginRect.Height);
        //        MyBounds.Width = MyBounds.Width * MySize.Width / 72 - (2 * MarginRect.Width);
        //        Int64 y = -10; // e.PageSettings.Bounds.Y; //MyBounds.Top; 
        //        Int64 x = -120; // e.PageSettings.Bounds.X; //MyBounds.Left;

        //        using (System.Drawing.Image thisImage = System.Drawing.Image.FromFile(_OutputFilePath))
        //        {

        //            GraphicsUnit units = eGraphics.PageUnit; //graphicsunits
        //            RectangleF thisImageBound = thisImage.GetBounds(ref units);
        //            eGraphics.PageUnit = units;
        //            //thisImageBound.Height = 2181;
        //            cardHeight = thisImageBound.Height * MySize.Height / thisImage.VerticalResolution;
        //            cardWidth = thisImageBound.Width * MySize.Width / thisImage.HorizontalResolution;
        //            double ScaleX = MyBounds.Width / cardWidth;
        //            double ScaleY = MyBounds.Height / cardHeight;
        //            double ScaleZ = 1.0;
        //            if (ScaleZ < ScaleX) ScaleZ = ScaleX;
        //            if (ScaleZ < ScaleY) ScaleZ = ScaleY;
        //            cardHeight *= ScaleZ; ;
        //            cardWidth *= ScaleZ;
        //            eGraphics.DrawImage(thisImage, x, y, Convert.ToInt64(cardWidth), Convert.ToInt64(cardHeight));
        //        }
        //    }
        //    else
        //    {
        //        using (System.Drawing.Image thisImage = System.Drawing.Image.FromFile(_OutputFilePath))
        //        {
        //            eGraphics.DrawImage(thisImage, _X, _Y, _width, _height);
        //        }
        //    }
        //}
        //SLR: added on 5/4 to minimize the tiff size since it is of 200 dpi where as printer dpi is of 600 dpi which would be 9 times the size of original tiff.
        private string printdoc_HCFA1500_ConversionWithLesserResolution(clsPrintDocumentConversion oConversion, Boolean isForPrint = false)
        {
            if (!isForPrint)
            {
                toCreateEMF = false;
            }
            float PaperWidth = oConversion.PaperWidth;
            float PaperHeight = oConversion.PaperHeight;
            _DpiX = oConversion.DpiX;
            _DpiY = oConversion.DpiY;
            _Bounds = new Rectangle(oConversion.BoundX, oConversion.BoundY, oConversion.BoundWidth, oConversion.BoundHeight);
            _MarginBounds = new Rectangle(oConversion.MarginBoundsX, oConversion.MarginBoundsY, oConversion.MarginBoundsWidth, oConversion.MarginBoundsHeight);


            bool bAnyError = false;



            try
            {
                using (System.Drawing.Image thisImage = System.Drawing.Image.FromFile(_OutputFilePath))
                {
                    _thisImage = thisImage;
                    try
                    {
                        
                        if (toCreateEMF)
                        {
                            _cPiX = _DpiX;
                            _cPiY = _DpiY;
                        }
                        else
                        {
                            _cPiX = Math.Max(((float)thisImage.Width / PaperWidth), ((float)thisImage.Height / PaperHeight));
                            _cPiX = Math.Min(thisImage.HorizontalResolution, _cPiX);
                            _cPiX = Math.Min(_DpiY, _cPiX);
                            //_cPiY = Math.Min(_DpiY, (float)thisImage.Height / PaperHeight);

                            _cPiY = _cPiX * thisImage.VerticalResolution / thisImage.HorizontalResolution;
                        }
                        byte[] emfBytes = null;
                        int bmpWidht = Convert.ToInt32(PaperWidth * _DpiX);
                        int bmpHeight = Convert.ToInt32(PaperHeight * _DpiY);
                        using (System.Drawing.Bitmap NewBitmap = new Bitmap(bmpWidht, bmpHeight))
                        {
                            try
                            {
                                NewBitmap.SetResolution(_cPiX, _cPiY);
                                if (toCreateEMF)
                                {
                                    emfBytes = gloGlobal.CreateEMF.GetEMFBytes(PaperWidth, PaperHeight, bmpWidht, bmpHeight, CreateEMFForCMS1500FormWithLesserResolution);
                                }
                                else
                                {
                                    using (System.Drawing.Graphics eGraphics = Graphics.FromImage(NewBitmap))
                                    {
                                        FitToImageWithLesserResolutionToScaleAndKeepAtCenter(eGraphics);
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                if (toCreateEMF)
                                {
                                    toCreateEMF = false;
                                    try
                                    {
                                        using (System.Drawing.Graphics eGraphics = Graphics.FromImage(NewBitmap))
                                        {
                                            FitToImageWithLesserResolutionToScaleAndKeepAtCenter(eGraphics);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        bAnyError = true;
                                        gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during scaling tiff file at printdoc_HCFA1500_ConversionWithLesserResolution method", false);
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                                    }
                                }

                            }
                            if (bAnyError)
                            {
                                return _OutputFilePath;
                            }
                            else
                            {


                                string _printFilePath = "";
                                string sPath = "";
                                try
                                {
                                    sPath = gloSettings.FolderSettings.AppTempFolderPath + "Paper_Claim_Files";

                                    if (System.IO.Directory.Exists(sPath) == false)
                                    {
                                        System.IO.Directory.CreateDirectory(sPath);
                                    }

                                    if (_IsPrintForm == true)
                                        _printFilePath = sPath + "\\" + DateTime.Now.ToString("yyyyMMddhhmmsstt") + "f" + (toCreateEMF ? ".emf" : ".jpg");
                                    else
                                        _printFilePath = sPath + "\\" + DateTime.Now.ToString("yyyyMMddhhmmsstt") + "f_BLANK" + (toCreateEMF ? ".emf" : ".tif");

                                    if (File.Exists(_printFilePath) == true) { File.Delete(_printFilePath); }

                                    if (toCreateEMF)
                                    {
                                        File.WriteAllBytes(_printFilePath, emfBytes);
                                    }
                                    else
                                    {
                                        NewBitmap.Save(_printFilePath, _IsPrintForm ? System.Drawing.Imaging.ImageFormat.Jpeg : System.Drawing.Imaging.ImageFormat.Tiff);
                                    }
                                    return _printFilePath;

                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during new bitmap save at printdoc_HCFA1500_ConversionWithLesserResolution method", false);
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                                    return _OutputFilePath;

                                }



                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during opening bitmap save at printdoc_HCFA1500_ConversionWithLesserResolution method", false);
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                        return _OutputFilePath;

                    }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return _OutputFilePath;
            }

        }
        private void FitToImageWithLesserResolutionToScaleAndKeepAtCenter(System.Drawing.Graphics eGraphics)
        {

            if (!_IsPrintForm)
            {
                //Dim Offset As Int64 = 10    
                RectangleF MyBounds = new RectangleF(_MarginBounds.Location, _MarginBounds.Size);

                RectangleF MarginRect = new RectangleF(0f, 1f, 1f, 1f);
                //MarginRect.Y = MarginRect.Y * _DpiY / 72;
                //MarginRect.X = MarginRect.X * _DpiX / 72;
                //MarginRect.Height = MarginRect.Height * _DpiY / 72;
                //MarginRect.Width = MarginRect.Width * _DpiX / 72;

                //MyBounds.Y = MyBounds.Y + MarginRect.Top;
                //MyBounds.X = MyBounds.X + MarginRect.Left;
                MyBounds.Height = (MyBounds.Height - (2f * MarginRect.Height)) / 72f;
                MyBounds.Width = (MyBounds.Width - (2f * MarginRect.Width)) / 72f;
                float y = 25f * _cPiY / _DpiY; // e.PageSettings.Bounds.Y; //MyBounds.Top; 
                float x = -80f * _cPiX / _DpiX; // e.PageSettings.Bounds.X; //MyBounds.Left;


                //GraphicsUnit units = eGraphics.PageUnit; //graphicsunits
                //RectangleF thisImageBound = _thisImage.GetBounds(ref units);
                //eGraphics.PageUnit = units;
                //thisImageBound.Height = 2181;


                float cardHeight = _thisImage.Height / _thisImage.VerticalResolution;
                float cardWidth = _thisImage.Width / _thisImage.HorizontalResolution;
                float ScaleX = MyBounds.Width / cardWidth;
                float ScaleY = MyBounds.Height / cardHeight;
                float ScaleZ = 1.0f;
                if (ScaleZ < ScaleX) ScaleZ = ScaleX;
                if (ScaleZ < ScaleY) ScaleZ = ScaleY;
                cardHeight *= (ScaleZ * _cPiY);
                cardWidth *= (ScaleZ * _cPiX);
                eGraphics.DrawImage(_thisImage, new RectangleF(x, y, cardWidth, cardHeight));
            }
            else
            {
                int _width = _Bounds.Width - 30;
                int _height = _Bounds.Height;
                int _X = _Bounds.X;
                int _Y = _Bounds.Y;
                eGraphics.DrawImage(_thisImage, (float)_X * _cPiY / _DpiY, (float)_Y * _cPiY / _DpiY, (float)_width * _cPiY / _DpiY, (float)_height * _cPiY / _DpiY);
            }
        }
        private void printdoc_HCFA1500_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {

        }
        private int GetPaperBillingSettings(Enum _SettingType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            int _result=0;
            string _NewQuery = null;
            try
            {
                _NewQuery = "SELECT ISNULL(dbo.GetPaperBillingSetting(" + _ContactID + "," + _SettingType.GetHashCode()+ "," + _ClinicID + "),0) AS BalanceDue";
                 Object _balanceDue = null;
                 oDB.Connect(false);
                _balanceDue = oDB.ExecuteScalar_Query(_NewQuery);
                _result = Convert.ToInt16(_balanceDue);
                oDB.Disconnect();
                //return _result;
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
                oDB.Dispose();
            }

            return _result;
           
        }

        private string GetBox29Setting(Int16 _SettingType,Int64 TrnsID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            string _result = "";
            object  Amount = null; 
            try
            {
                oDB.Connect(false);  
                oDBParameters.Add("@nTransactionID", TrnsID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", _ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nSettingType", gloSettings.PaperBillingBoxtype.Box29.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sAmount", string.Empty, System.Data.ParameterDirection.Output, System.Data.SqlDbType.VarChar, 255);
                oDB.Execute("BL_GetPaperPaperBillingAmountPaid", oDBParameters, out Amount);
                oDB.Disconnect();
                _result = Amount.ToString();   

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
                oDB.Dispose();
            }
            return _result;
        }

        private string NameFormation(string sLastname,string sSuffix,string sFirstName,string sLastName)
        { 
            //Removed comma on 12162013

            string sFormatedPatientName = "";
            try
            {
                sFormatedPatientName =sLastname.Trim();
                if (sSuffix.Trim()  != "")
                {
                    sFormatedPatientName = sFormatedPatientName + " " + sSuffix.Trim(); 
                }
                sFormatedPatientName = sFormatedPatientName + " " + sFirstName.Trim();
                if (sLastName.Trim() != "")
                {
                    sFormatedPatientName = sFormatedPatientName + " " + sLastName.Substring(0, 1).ToString();
                }
                return sFormatedPatientName;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                
            }
            return sFormatedPatientName;
        }
     
        public String GetYearFormat(Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBPara = null;
            String sYearFormat = "YYYY";

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDBPara = new gloDatabaseLayer.DBParameters();

                oDBPara.Add("@nContactID", nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                sYearFormat = Convert.ToString(oDB.ExecuteScalar("gsp_CheckYearFormat", oDBPara)).ToLower();
            }

            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDBPara != null) { oDBPara.Dispose(); oDBPara = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }

            }
            return sYearFormat;

        }

        public bool ReportClinicName(Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBPara = null;
            bool ReportClinicName = false;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDBPara = new gloDatabaseLayer.DBParameters();

                oDBPara.Add("@nContactID", nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                ReportClinicName = Convert.ToBoolean(oDB.ExecuteScalar("gsp_ReportClinicName", oDBPara));
            }

            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDBPara != null) { oDBPara.Dispose(); oDBPara = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }

            }
            return ReportClinicName;

        }
      
    }

      

    }
