using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace gloCMSEDI
{
   

    public partial class UB04 : UserControl
    {
        #region " Variables "


        private string _databaseconnectionstring = "";
        string _InputFilePath = "";
         string _OutputFilePath = "";

        private string _messageboxcaption = "";
        UB04Transaction oUB04Transaction  = null;


      //  string _strPatientStatus = "";
      //  bool IsSecondaryInsurance = false;

        Int64 _ClinicID = 0;
        string _ClinicName = "";
        string _ClinicAddress = "";
        string _ClinicStreet = "";
        string _ClinicCity = "";
        string _ClinicState = "";
        string _ClinicZip = "";
        string _ClinicPhone = "";
        string _ClinicNPI = "";
        string _ClinicAreaCode = "";
        bool _IsCapatalize = false;
        bool _IsForPrintData = false;
/*
        string _PatientAccountCode = "Claim Number";

        //Facility Provider Address Type
        private AddressType _FacilityAddressType = AddressType.FacilityAddress;
        //Billing Provider NPI
        private NPIType _Facility_A_NPI = NPIType.FacilityNPI;
        private NPIType _Facility_B_NPI = NPIType.FacilityNPI;

        //Billing Provider Address Type
        private AddressType _BillingAddressType = AddressType.ProviderAddress;
        //Billing Provider NPI
        private NPIType _Billing_A_NPI = NPIType.BillingProviderNPI;
        private NPIType _Billing_B_NPI = NPIType.BillingProviderNPI;
*/
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _PatientID = 0;
        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }



        #region " Other Details Variables "
/*
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
        //private AddressType _BillingAddressType = AddressType.ProviderAddress;
        ////Billing Provider NPI
        //private NPIType _Billing_A_NPI = NPIType.BillingProviderNPI;
        //private NPIType _Billing_B_NPI = NPIType.BillingProviderNPI;
        //private NPIType _Billing_B_NPI = NPIType.BillingProviderNPI;
        private string _InsuranceTypeCode = String.Empty;
/*
        //Submitter
        private string _SubmitterName = "";
        private string _SubmitterContactPersonName = "";
        private string _SubmitterContactPersonNo = "";
        private string _SubmitterCity = "";
        private string _SubmitterState = "";
        private string _SubmitterZIP = "";
        private string _SubmitterETIN = "";
        private string _SubmitterAddress = "";

        //Receiver
        private string _ReceiverName = "";
        private string _ReceiverETIN = "";

        //Subscriber
        private string _SubscriberLName = "";
        private string _SubscriberInsurancePST = "";
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

        private string _PatientAccountNo = "";


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

        //Facility Provider Address Type
        //private AddressType _FacilityAddressType = AddressType.FacilityAddress;
        ////Billing Provider NPI
        //private NPIType _Facility_A_NPI = NPIType.FacilityNPI
            
        //private NPIType _Facility_B_NPI = NPIType.FacilityNPI;

        //Other Insurance
        private Int64 _PatientOtherInsuranceID = 0;
        private string _OtherInsuranceSubscriberLName = "";
        private string _OtherInsurancePST = "";
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
        private string _OtherInsuranceSubscriberPhone = "";
        private Int64 _OtherInsuranceContactID = 0;

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

        //Prior Authorization Number
        private string _PriorAuthorizationNo = "";
*/
        #endregion


        private Int64 _ContactID = 0;

        public Int64 ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; }
        }
        //Signature on File
     //   private bool _IsSignatureOnFile = false;
        private DateTime _dtSignatureOnFileDate = DateTime.Now;
     //   private string _InsuredPersonSign = "";


        private bool _IsAcceptAssignment = false;
        private bool _IsIncludePrimaryDxInBox69 = false;

        #endregion " Variables And Procedures "

        #region "UB 04 Extended Settings Enum"
        public enum ExtendedZip
        {
            None =0,
            Paper=1,
            Electronic =2,
            Both=3

        }
        public enum BOX81CCA
        {
            None = 0,
            BillingProvider = 1,
            AttendingProviderBox76 = 2,
            OperatingProviderBox77 = 3,
            BOX78=4,
            BOX79=5
          
        }
        public enum BOX81CCB
        {
            None = 0,
            BillingProvider = 1,
            AttendingProviderBox76 = 2,
            OperatingProviderBox77 = 3,
            BOX78 = 4,
            BOX79 = 5

        }
        public enum BOX81CCC
        {
            None = 0,
            BillingProvider = 1,
            AttendingProviderBox76 = 2,
            OperatingProviderBox77 = 3,
            BOX78 = 4,
            BOX79 = 5

        }
        public enum BOX81CCD
        {
            None = 0,
            BillingProvider = 1,
            AttendingProviderBox76 = 2,
            OperatingProviderBox77 = 3,
            BOX78 = 4,
            BOX79 = 5

        }
        DataTable _dtProviderTaxonomyCodes = null;
        DataTable _dtUB04ExtendedSettings = null;
        #endregion
        public UB04(String DatabaseConnectionString)
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

            #region " Get Clinic Information "

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt = new DataTable();
            String _sqlQuery = "Select ISNULL(sClinicName,'') as sClinicName,ISNULL(sAddress1,'') as sAddress1,ISNULL(sAddress2,'') as sAddress2,ISNULL(sStreet,'') as sStreet,ISNULL(sCity,'') as sCity,ISNULL(sState,'') as sState,ISNULL(sZIP,'') as sZIP,ISNULL(sAreaCode,'') AS sAreaCode,ISNULL(sPhoneNo,'') as sPhoneNo,ISNULL(sClinicNPI,'') as sClinicNPI " +
                        " from Clinic_MST where nClinicID=" + _ClinicID;
            oDB.Retrive_Query(_sqlQuery, out dt);
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
                _ClinicPhone = _ClinicPhone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");              
                _ClinicNPI = Convert.ToString(dt.Rows[0]["sClinicNPI"]);
            }
            dt = null;
            oDB.Dispose();

            #endregion

            InitializeComponent();
        }

        private void UB04_Load(object sender, EventArgs e)
        {
                                 
        }

        private void GetPhysicianOtherID(Int64 nProviderId, Int64 ContactID, Int64 ClinicID, ref string QualifierID, ref string Value)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlquery = String.Empty;
            DataTable dtOtherId = null;
          
            try
            {
                oDB.Connect(false);
                 _sqlquery = "SELECT * FROM dbo.[GET_ATTENDING_PROVIDER_QUALIFIER_UB04](" + ContactID + "," + nProviderId + "," + ClinicID + ")";
                oDB.Retrive_Query(_sqlquery, out dtOtherId);

                if (dtOtherId != null && dtOtherId.Rows.Count > 0)
                {
                    QualifierID = Convert.ToString(dtOtherId.Rows[0]["QualifierID"]).Trim();
                    Value = Convert.ToString(dtOtherId.Rows[0]["Value"]).Trim();
                }
            }
            catch (Exception ex)
            {              
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                oDB.Disconnect();
                if (oDB != null) { oDB.Dispose(); }
            }
           
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
                oDBParameters.Add("@nProviderID", nProviderId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nFacilityID", nFacilityId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nContactId", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sSettingName", SettingName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@bIsEDI", 0, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Retrive("BL_Get_AlternateID_Settings", oDBParameters, out dtProviders);

                oDB.Disconnect();
                string temp = "";

                if (dtProviders != null && dtProviders.Rows.Count > 0)
                {
                    if (Type == "Provider")
                    {
                        if (Convert.ToString(dtProviders.Rows[0]["sSuffix"]).Trim() != null)
                        {
                            lbl_1a.Text = Convert.ToString(dtProviders.Rows[0]["FirstName"]).Trim() + " " +
                                         Convert.ToString(dtProviders.Rows[0]["MiddleName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["LastName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["sSuffix"]).Trim(); //Provider Name
                        }
                        else
                        {
                            lbl_1a.Text = Convert.ToString(dtProviders.Rows[0]["FirstName"]).Trim() + " " +
                                         Convert.ToString(dtProviders.Rows[0]["MiddleName"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["LastName"]).Trim();
                        }

                        lbl_1b.Text = Convert.ToString(dtProviders.Rows[0]["Address1"]).Trim() + " " + Convert.ToString(dtProviders.Rows[0]["Address2"]).Trim();//Provider Address
                        if (Convert.ToString(dtProviders.Rows[0]["City"]) != "")
                            temp = Convert.ToString(dtProviders.Rows[0]["City"]).Trim() + " ";
                        if (Convert.ToString(dtProviders.Rows[0]["State"]).Trim() != "")
                            temp = temp + Convert.ToString(dtProviders.Rows[0]["State"]).Trim() + " ";
                        if (Convert.ToString(dtProviders.Rows[0]["ZIP"]).Trim() != "")
                            temp = temp + Convert.ToString(dtProviders.Rows[0]["ZIP"]).Trim();
                        if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                        {
                            if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nIncludeExtendedZip"]) == ExtendedZip.Paper.GetHashCode() || Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nIncludeExtendedZip"]) == ExtendedZip.Both.GetHashCode())
                            {
                                temp = temp + Convert.ToString(dtProviders.Rows[0]["AreaCode"]).Trim();
 
                            }
                        }
                        lbl_1c.Text = temp;
                        lbl_1d.Text = Convert.ToString(dtProviders.Rows[0]["PhoneNo"]).Trim();
            
                    }



                    #region "Box 76 & 56"

                    lbl_56.Text= Convert.ToString(dtProviders.Rows[0]["PrimaryQualifierValue"]).Trim();
                    if (Convert.ToString(dtProviders.Rows[0]["SecondaryQualifierValue"]).Trim() != "" && Convert.ToBoolean(dtProviders.Rows[0]["IsDefaultOther"]) ==false)
                    {
                        if (Convert.ToString(dtProviders.Rows[0]["SecondaryQualifier"]).Trim() == "0B" ||
                            Convert.ToString(dtProviders.Rows[0]["SecondaryQualifier"]).Trim() == "1G" ) 
                        {
                            lbl_76_QualID.Text = Convert.ToString(dtProviders.Rows[0]["SecondaryQualifier"]).Trim();
                            lbl_76_QualValue.Text = Convert.ToString(dtProviders.Rows[0]["SecondaryQualifierValue"]).Trim();
                        }
                        else if (Convert.ToString(dtProviders.Rows[0]["SecondaryQualifier"]).Trim() != "XX")
                        {
                            lbl_76_QualID.Text = "G2";
                            lbl_76_QualValue.Text = Convert.ToString(dtProviders.Rows[0]["SecondaryQualifierValue"]).Trim();
                        }
                        
                    }




                    #endregion
                    

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
        private DataSet GetProviderTaxonomyCodes(Int64 nTransactionID, Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _sqlquery = String.Empty;
            DataSet dtProviderTaxonomy = null;

            try
            {
                oDB.Connect(false);
                oParameters.Add("@nTransactionID", nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);               
                oParameters.Add("@nContactId", nContactID, ParameterDirection.Input, SqlDbType.BigInt);               
                oDB.Retrive("BL_GetProviderTaxonomy_UB04", oParameters, out dtProviderTaxonomy);
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
            return dtProviderTaxonomy;

        }
        public DataTable GetProviderDetails(Int64 TransactionId, Int64 MstTransactionId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            DataTable _dtProviderDetail=null;
            try
            {
                oDB.Connect(false);
                
                _strSQL = "SELECT sFirstName,sLastName ,Provider_MST.sNPI   FROM Provider_MST  INNER JOIN BL_Transaction_Claim_MST with (nolock) ON Provider_MST.nProviderID =BL_Transaction_Claim_MST.nTransactionProviderID   "
                           + " where BL_Transaction_Claim_MST.nTransactionID =" + TransactionId + " And BL_Transaction_Claim_MST.nTransactionMasterID=" + MstTransactionId;
                oDB.Retrive_Query(_strSQL, out _dtProviderDetail);
                
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return _dtProviderDetail;
        }

        public string GetOperatingProviderBox77Setting(Int64 TransactionId, Int64 MstTransactionId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            object _result;
            try
            {
                oDB.Connect(false);

                _strSQL = "SELECT TOP 1 sUBBox77 FROM Contacts_Insurance_DTL WHERE nContactID=(SELECT top 1 nContactID FROM BL_Transaction_Claim_MST WITH (NOLOCK) WHERE nTransactionID=" + TransactionId + " AND nTransactionMasterID=" + MstTransactionId + ")";
                _result=oDB.ExecuteScalar_Query(_strSQL);
                if (_result != null)
                {
                    return _result.ToString();
                }
                


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return "";
        }

        public void FillUB04Form(Int64 TransactionId, Int64 MstTransactionId)
        {
             gloUB04 oUB04 = new gloUB04(_databaseconnectionstring,_ClinicID);
             oUB04Transaction=new UB04Transaction(MstTransactionId ,TransactionId);
             oUB04Transaction=oUB04.GetUBClaim(MstTransactionId ,TransactionId);
             if (oUB04Transaction.ResponsibleContactID != null && oUB04Transaction.ResponsibleContactID.Trim() != "")
             {
                 _ContactID = Convert.ToInt64( oUB04Transaction.ResponsibleContactID);
             }
             try
             {
                 if (oUB04Transaction.Transaction != null)
                 {

                     //FillOtherDetails(oUB04Transaction.Transaction.ContactID);
                     getBox69settings(oUB04Transaction.Transaction.ContactID);
                     string temp = "";

                     DataSet _dsProviderSettings = GetProviderTaxonomyCodes(TransactionId, Convert.ToInt64(oUB04Transaction.Transaction.ContactID));
                     
                     if (_dsProviderSettings!=null && _dsProviderSettings.Tables.Count==2)
                     {
                        _dtProviderTaxonomyCodes = _dsProviderSettings.Tables[0];
                        _dtUB04ExtendedSettings = _dsProviderSettings.Tables[1];

                     }

                     #region "Box 1"


                     FillBillingProviderFacility(Convert.ToInt64(oUB04Transaction.Transaction.ProviderID), Convert.ToInt64(oUB04Transaction.Transaction.FacilityCode), Convert.ToInt64(oUB04Transaction.Transaction.ContactID), "Billing Provider Source", "Provider");

                     //if (oUB04Transaction.Transaction.ProviderSuffix != null)
                     //    lbl_1a.Text = oUB04Transaction.Transaction.ProviderName.ToString().Trim() + " " + oUB04Transaction.Transaction.ProviderSuffix.Trim(); //Provider Name
                     //else
                     //    lbl_1a.Text = oUB04Transaction.Transaction.ProviderName.ToString().Trim(); //Provider Name                    

                     //if (_BillingAddressType == AddressType.ProviderAddress)
                     //{
                         
                     //    lbl_1b.Text = oUB04Transaction.BillingProviderAddressLine1.ToString().Trim() + " " + oUB04Transaction.BillingProviderAddressLine2.ToString().Trim();//Provider Address
                     //    if (oUB04Transaction.BillingProviderCity.Trim() != "")
                     //        temp = oUB04Transaction.BillingProviderCity.Trim() + " ";
                     //    if (oUB04Transaction.BillingProviderState.Trim() != "")
                     //        temp = temp + oUB04Transaction.BillingProviderState.Trim() + " ";
                     //    if (oUB04Transaction.BillingProviderZip.Trim() != "")
                     //        temp = temp + oUB04Transaction.BillingProviderZip.Trim();
                     //    lbl_1c.Text = temp;
                     //    lbl_1d.Text = oUB04Transaction.BillingProviderPhoneNo.Trim();
                     //}
                     //else if (_BillingAddressType == AddressType.FacilityAddress)
                     //{
                     //    lbl_1b.Text = oUB04Transaction.Facilityadd1.ToString().Trim() + " " + oUB04Transaction.Facilityadd2.ToString().Trim();
                     //    if (oUB04Transaction.Facilitycity.Trim() != "")
                     //        temp = oUB04Transaction.Facilitycity.Trim() + " ";
                     //    if (oUB04Transaction.Facilitystate.Trim() != "")
                     //        temp = temp + oUB04Transaction.Facilitystate.Trim() + " ";
                     //    if (oUB04Transaction.Facilityzip.Trim() != "")
                     //        temp = temp + oUB04Transaction.Facilityzip.Trim();

                     //    lbl_1c.Text = temp;
                     //    lbl_1d.Text = oUB04Transaction.Facilityphone.Trim();
                     //}
                     //else if (_BillingAddressType == AddressType.ClinicAddress)
                     //{
                     //    lbl_1b.Text = _ClinicAddress.Trim();
                     //    if (_ClinicCity.Trim()  != "")
                     //        temp = _ClinicCity.Trim() + " ";
                     //    if (_ClinicState.Trim()  != "")
                     //        temp = temp + _ClinicState.Trim() + " ";
                     //    if (_ClinicZip.Trim() != "")
                     //        temp = temp + _ClinicZip.Trim();

                     //    lbl_1c.Text = temp;
                     //    lbl_1d.Text = _ClinicPhone.Trim();
                     //}
                     //else
                     //{          
               
                     //    lbl_1b.Text = oUB04Transaction.BillingProviderAddressLine1.ToString().Trim() + " " + oUB04Transaction.BillingProviderAddressLine2.ToString().Trim();//Provider Address
                     //    if (oUB04Transaction.BillingProviderCity.Trim() != "")
                     //        temp = oUB04Transaction.BillingProviderCity.Trim() + " ";
                     //    if (oUB04Transaction.BillingProviderState.Trim() != "")
                     //        temp = temp + oUB04Transaction.BillingProviderState.Trim() + " ";
                     //    if (oUB04Transaction.BillingProviderZip.Trim() != "")
                     //        temp = temp + oUB04Transaction.BillingProviderZip.Trim();
                     //    lbl_1c.Text = temp;
                     //    lbl_1d.Text = oUB04Transaction.BillingProviderPhoneNo.Trim();
                     //}

                                          
                     #endregion

                     #region "Box 3"
                     string sPatControlNo = string.Empty;
                     if (gloGlobal.gloPMGlobal.IsUseClaimPrefix && gloGlobal.gloPMGlobal.sClaimPrefix!="")
                     {
                        sPatControlNo = gloGlobal.gloPMGlobal.sClaimPrefix + oUB04Transaction.Transaction.ClaimNumber.ToString().Trim(); 
                     }
                     else
                     {
                        sPatControlNo = oUB04Transaction.Transaction.ClaimNumber.ToString().Trim();
                     }
                     //lbl_3a.Text = oUB04Transaction.Transaction.ClaimNumber.ToString().Trim();
                     lbl_3a.Text = sPatControlNo;
                     #endregion

                     #region "Box 4"                     
                     lbl_4.Text = oUB04Transaction.TypeofBilling.ToString().Trim();                     

                     #endregion

                     #region "Box 5"
                     lbl_5b.Text = oUB04Transaction.BillingProviderTaxID.ToString().Trim();
                     #endregion

                     #region "Box 6"
                     lbl_6a.Text = oUB04Transaction.MinDOS.ToString("MMddyy");
                     lbl_6b.Text = oUB04Transaction.MaxDOS.ToString("MMddyy");
                     #endregion

                     #region "Box 8"
                     lbl_8a.Text = oUB04Transaction.ResponsibleSubcriberID.Trim();
                     lbl_8b.Text = oUB04Transaction.PatientLastName.Trim() + " " + oUB04Transaction.PatientFirstName.Trim() + " " + oUB04Transaction.PatientMiddleName.Trim();
                     #endregion

                     #region "Box 9 & 10 & 11"
                     lbl_9a.Text = oUB04Transaction.PatientAddressLine1.Trim() +" "+ oUB04Transaction.PatientAddressLine2.ToString().Trim();
                     lbl_9b.Text = oUB04Transaction.PatientCity.Trim();
                     lbl_9c.Text = oUB04Transaction.PatientState.Trim();
                     lbl_9d.Text = oUB04Transaction.PatientZip.Trim();

                     lbl_10.Text = oUB04Transaction.PatientDOB.ToString("MMddyyyy");
                     if (oUB04Transaction.PatientGender.ToString().Trim() != string.Empty)
                     {
                         lbl_11.Text = oUB04Transaction.PatientGender.ToString().Substring(0, 1).Trim();
                     }
                     #endregion

                     #region "Box 12"
                     if (oUB04Transaction.admitDate != "" && oUB04Transaction.admitDate != null)
                     {
                         lbl_12.Text = Convert.ToDateTime(oUB04Transaction.admitDate).ToString("MMddyy");
                     }
                     else
                     {
                         lbl_12.Text = "";
                     }
                     #endregion

                     #region "Box 13"

                     if (oUB04Transaction.AdmissionHour.Trim().Length > 2)
                         lbl_13.Text = oUB04Transaction.AdmitHour.Substring(0, 2);
                     else
                         lbl_13.Text = oUB04Transaction.AdmitHour;

                     #endregion

                     #region "Box 14"
                     lbl_14.Text = oUB04Transaction.AdmissionTypeCode.ToString().Trim();
                     #endregion

                     #region "Box 15"
                     lbl_15.Text = oUB04Transaction.AdmissionSource.ToString().Trim();
                     #endregion

                     #region "Box 16"

                     if (oUB04Transaction.DischargeHour.Trim().Length > 2)
                         lbl_16.Text = oUB04Transaction.DischargeHour.Substring(0, 2);
                     else
                         lbl_16.Text = oUB04Transaction.DischargeHour;

                     #endregion

                     #region "Box 17"
                     lbl_17.Text = oUB04Transaction.DischargeStatus.ToString().Trim();
                     #endregion

                     #region "Box 18"
                     lbl_18.Text = oUB04Transaction.ConditionCode1;
                     #endregion

                     #region "Box 19"
                     lbl_19.Text = oUB04Transaction.ConditionCode2;
                     #endregion

                     #region "Box 20"
                     lbl_20.Text = oUB04Transaction.ConditionCode3;
                     #endregion

                     #region "Box 21"
                     lbl_21.Text = oUB04Transaction.ConditionCode4;
                     #endregion

                     #region "Box 22"
                     lbl_22.Text = oUB04Transaction.ConditionCode5;
                     #endregion

                     #region "Box 23"
                     lbl_23.Text = oUB04Transaction.ConditionCode6;
                     #endregion

                     #region "Box 24"
                     lbl_24.Text = oUB04Transaction.ConditionCode7;
                     #endregion

                     #region "Box 25"
                     lbl_25.Text = oUB04Transaction.ConditionCode8;
                     #endregion

                     #region "Box 26"
                     lbl_26.Text = oUB04Transaction.ConditionCode9;
                     #endregion

                     #region "Box 27"
                     lbl_27.Text = oUB04Transaction.ConditionCode10;
                     #endregion

                     #region "Box 28"
                     lbl_28.Text = oUB04Transaction.ConditionCode11;
                     #endregion

                     #region "Box 29"
                   
                     lbl_29.Text = oUB04Transaction.AccidentState.ToString().Trim();
                     #endregion

                     #region "Box 31a,32a,33a,34a"
                     lbl_31a1.Text = oUB04Transaction.OccurrenceCode1;
                     lbl_31a2.Text = oUB04Transaction.OccurrenceDate1;


                     lbl_32a1.Text = oUB04Transaction.OccurrenceCode2;
                     lbl_32a2.Text = oUB04Transaction.OccurrenceDate2;
                    
                   
                     lbl_33a1.Text = oUB04Transaction.OccurrenceCode3;
                     lbl_33a2.Text = oUB04Transaction.OccurrenceDate3;

                     lbl_34a1.Text = oUB04Transaction.OccurrenceCode4;
                     lbl_34a2.Text = oUB04Transaction.OccurrenceDate4;
                     #endregion

                     #region "Box 31b,32b,33b,34b"

                     lbl_31b1.Text = oUB04Transaction.OccurrenceCode5;
                     lbl_31b2.Text = oUB04Transaction.OccurrenceDate5;


                     lbl_32b1.Text = oUB04Transaction.OccurrenceCode6;
                     lbl_32b2.Text = oUB04Transaction.OccurrenceDate6;
                  

                     lbl_33b1.Text = oUB04Transaction.OccurrenceCode7;
                     lbl_33b2.Text = oUB04Transaction.OccurrenceDate7;

                     lbl_34b1.Text = oUB04Transaction.OccurrenceCode8;
                     lbl_34b2.Text = oUB04Transaction.OccurrenceDate8;
                     #endregion

                
                     #region "Box 35 a, 36 a"
                     lbl_35a1.Text = oUB04Transaction.OccurrenceSpanCode1;
                     lbl_35a2.Text = oUB04Transaction.OccurrenceSpanFromDate1;
                     lbl_35a3.Text = oUB04Transaction.OccurrenceSpanToDate1;

                     lbl_36a1.Text = oUB04Transaction.OccurrenceSpanCode2;
                     lbl_36a2.Text = oUB04Transaction.OccurrenceSpanFromDate2;
                     lbl_36a3.Text = oUB04Transaction.OccurrenceSpanToDate2;
                     #endregion

                     #region "Box 35b,36b"
                     lbl_35b1.Text = oUB04Transaction.OccurrenceSpanCode3;
                     lbl_35b2.Text = oUB04Transaction.OccurrenceSpanFromDate3;
                     lbl_35b3.Text = oUB04Transaction.OccurrenceSpanToDate3;

                     lbl_36b1.Text = oUB04Transaction.OccurrenceSpanCode4;
                     lbl_36b2.Text = oUB04Transaction.OccurrenceSpanFromDate4;
                     lbl_36b3.Text = oUB04Transaction.OccurrenceSpanToDate4;
                     #endregion

                 

                     #region "Box 38"
                     temp = "";
                     if (oUB04Transaction.Responsiblesubadd1.Trim() + oUB04Transaction.Responsiblesubadd2.ToString().Trim() == "")
                     {
                         temp = oUB04Transaction.ResponsiblesubcriberName.Trim() + "\n";
                     }
                     else
                     {
                         temp = oUB04Transaction.ResponsiblesubcriberName.Trim() + "\n" + oUB04Transaction.Responsiblesubadd1.Trim() + " " + oUB04Transaction.Responsiblesubadd2.ToString().Trim() + "\n";
                     }              
                     if( oUB04Transaction.Responsiblesubcity.Trim() != "")
                         temp = temp + oUB04Transaction.Responsiblesubcity.Trim() + " ";
                     if(oUB04Transaction.Responsiblesubstate.Trim() != "")
                         temp = temp + oUB04Transaction.Responsiblesubstate.Trim() + " ";
                     if(oUB04Transaction.Responsiblesubzip.Trim() != "")
                         temp = temp + oUB04Transaction.Responsiblesubzip.Trim();
                    // temp = temp + "\n" + oUB04Transaction.ResponsiblePhoneNo.Trim();
                     lbl_38.Text = temp;
                     #endregion

                     #region "Box 39a,40a,41a"

                     if (Convert.ToString(oUB04Transaction.ValueCode1).Length > 0)
                         lbl_39a1.Text = Convert.ToString(oUB04Transaction.ValueCode1);

                     if (Convert.ToString(oUB04Transaction.ValueAmount1).Length > 0)
                     {
                         lbl_39a2.Text = Convert.ToString(oUB04Transaction.ValueAmount1).Remove(oUB04Transaction.ValueAmount1.ToString().IndexOf(".")).Trim();
                         lbl_39a3.Text = Convert.ToString(oUB04Transaction.ValueAmount1).Substring(oUB04Transaction.ValueAmount1.ToString().IndexOf(".") + 1).Trim();
                     }

                     if (Convert.ToString(oUB04Transaction.ValueCode2).Length > 0)
                         lbl_40a1.Text = Convert.ToString(oUB04Transaction.ValueCode2);


                     if (Convert.ToString(oUB04Transaction.ValueAmount2).ToString().Length > 0)
                     {
                         lbl_40a2.Text = Convert.ToString(oUB04Transaction.ValueAmount2).Remove(oUB04Transaction.ValueAmount2.ToString().IndexOf(".")).Trim();
                         lbl_40a3.Text = Convert.ToString(oUB04Transaction.ValueAmount2).Substring(oUB04Transaction.ValueAmount2.ToString().IndexOf(".") + 1).Trim();
                     }

                     if (Convert.ToString(oUB04Transaction.ValueCode3).Length > 0)
                         lbl_41a1.Text = Convert.ToString(oUB04Transaction.ValueCode3);

                     if (Convert.ToString(oUB04Transaction.ValueAmount3).Length > 0)
                     {
                         lbl_41a2.Text = Convert.ToString(oUB04Transaction.ValueAmount3).Remove(oUB04Transaction.ValueAmount3.ToString().IndexOf(".")).Trim();
                         lbl_41a3.Text = Convert.ToString(oUB04Transaction.ValueAmount3).Substring(oUB04Transaction.ValueAmount3.ToString().IndexOf(".") + 1).Trim();
                     }
                     #endregion

                     #region "Box 39b,40b,41b"

                     if (Convert.ToString(oUB04Transaction.ValueCode4).Length > 0)
                         lbl_39b1.Text = Convert.ToString(oUB04Transaction.ValueCode4);

                     if (Convert.ToString(oUB04Transaction.ValueAmount4).Length > 0)
                     {
                         lbl_39b2.Text = Convert.ToString(oUB04Transaction.ValueAmount4).Remove(oUB04Transaction.ValueAmount4.ToString().IndexOf(".")).Trim();
                         lbl_39b3.Text = Convert.ToString(oUB04Transaction.ValueAmount4).Substring(oUB04Transaction.ValueAmount4.ToString().IndexOf(".") + 1).Trim();
                     }
                     if (Convert.ToString(oUB04Transaction.ValueCode5).Length > 0)
                         lbl_40b1.Text = Convert.ToString(oUB04Transaction.ValueCode5);

                     if (Convert.ToString(oUB04Transaction.ValueAmount5).ToString().Length > 0)
                     {
                         lbl_40b2.Text = Convert.ToString(oUB04Transaction.ValueAmount5).Remove(oUB04Transaction.ValueAmount5.ToString().IndexOf(".")).Trim();
                         lbl_40b3.Text = Convert.ToString(oUB04Transaction.ValueAmount5).Substring(oUB04Transaction.ValueAmount5.ToString().IndexOf(".") + 1).Trim();
                     }

                     if (Convert.ToString(oUB04Transaction.ValueCode6).Length > 0)
                         lbl_41b1.Text = oUB04Transaction.ValueCode6;

                     if (Convert.ToString(oUB04Transaction.ValueAmount6).ToString().Length > 0)
                     {
                         lbl_41b2.Text = Convert.ToString(oUB04Transaction.ValueAmount6).Remove(oUB04Transaction.ValueAmount6.ToString().IndexOf(".")).Trim() ;
                         lbl_41b3.Text = Convert.ToString(oUB04Transaction.ValueAmount6).Substring(oUB04Transaction.ValueAmount6.ToString().IndexOf(".") + 1).Trim();
                     }

                     #endregion
                     #region "Box 39c,40c,41c"
                   
                     if (Convert.ToString(oUB04Transaction.ValueCode7).Length > 0)
                         lbl_39c1.Text = Convert.ToString(oUB04Transaction.ValueCode7);

                     if (Convert.ToString(oUB04Transaction.ValueAmount7).ToString().Length > 0)
                     {
                         lbl_39c2.Text = Convert.ToString(oUB04Transaction.ValueAmount7).Remove(oUB04Transaction.ValueAmount7.ToString().IndexOf(".")).Trim();
                         lbl_39c3.Text = Convert.ToString(oUB04Transaction.ValueAmount7).Substring(oUB04Transaction.ValueAmount7.ToString().IndexOf(".") + 1).Trim();
                     }

                     if (Convert.ToString(oUB04Transaction.ValueCode8).Length > 0)
                         lbl_40c1.Text = Convert.ToString(oUB04Transaction.ValueCode8);

                     if (Convert.ToString(oUB04Transaction.ValueAmount8).ToString().Length > 0)
                     {
                         lbl_40c2.Text = Convert.ToString(oUB04Transaction.ValueAmount8).Remove(oUB04Transaction.ValueAmount8.ToString().IndexOf(".")).Trim();
                         lbl_40c3.Text = Convert.ToString(oUB04Transaction.ValueAmount8).Substring(oUB04Transaction.ValueAmount8.ToString().IndexOf(".") + 1).Trim();
                     }


                     if (oUB04Transaction.ValueCode9.Length > 0)
                         lbl_41c1.Text = oUB04Transaction.ValueCode9;

                     if (oUB04Transaction.ValueAmount9.ToString().Length > 0)
                     {
                         lbl_41c2.Text = oUB04Transaction.ValueAmount9.ToString().Remove(oUB04Transaction.ValueAmount9.ToString().IndexOf(".")).Trim();
                         lbl_41c3.Text = Convert.ToString(oUB04Transaction.ValueAmount9).Substring(oUB04Transaction.ValueAmount9.ToString().IndexOf(".") + 1).Trim();
                     }


                     #endregion
                     #region "Box 39d,40d,41d"

                     if (oUB04Transaction.ValueCode10.Length > 0)
                         lbl_39d1.Text = oUB04Transaction.ValueCode10;

                     if (oUB04Transaction.ValueAmount10.ToString().Length > 0)
                     {
                         lbl_39d2.Text = oUB04Transaction.ValueAmount10.ToString().Remove(oUB04Transaction.ValueAmount10.ToString().IndexOf(".")).Trim();
                         lbl_39d3.Text = Convert.ToString(oUB04Transaction.ValueAmount10).Substring(oUB04Transaction.ValueAmount10.ToString().IndexOf(".") + 1).Trim();
                     }


                     if (oUB04Transaction.ValueCode11.Length > 0)
                         lbl_40d1.Text = oUB04Transaction.ValueCode11;

                     if (oUB04Transaction.ValueAmount11.ToString().Length > 0)
                     {
                         lbl_40d2.Text = oUB04Transaction.ValueAmount11.ToString().Remove(oUB04Transaction.ValueAmount11.ToString().IndexOf(".")).Trim();
                         lbl_40d3.Text = Convert.ToString(oUB04Transaction.ValueAmount11).Substring(oUB04Transaction.ValueAmount11.ToString().IndexOf(".") + 1).Trim();
                     }

                     if (oUB04Transaction.ValueCode12.Length > 0)
                         lbl_41d1.Text = oUB04Transaction.ValueCode12;

                     if (oUB04Transaction.ValueAmount12.ToString().Length > 0)
                     {
                         lbl_41d2.Text = oUB04Transaction.ValueAmount12.ToString().Remove(oUB04Transaction.ValueAmount12.ToString().IndexOf(".")).Trim();
                         lbl_41d3.Text = Convert.ToString(oUB04Transaction.ValueAmount12).Substring(oUB04Transaction.ValueAmount12.ToString().IndexOf(".") + 1).Trim();
                     }

                     # endregion



                     #region "Box 42 43 44 45 46 47 48"
                     decimal _chargetotal = 0;
                     if (oUB04Transaction.Transaction.Lines != null && oUB04Transaction.Transaction.Lines.Count > 0)
                     {
                       
                         int i = 0;
                         for (i = 0; oUB04Transaction.Transaction.Lines.Count > i; i++)
                         {
                             //if (i == 0)
                             //{
                             //    lbl_74_Code.Text = oUB04Transaction.Transaction.Lines[i].CPTCode.ToString().Trim();
                             //    lbl_74_Date.Text = oUB04Transaction.Transaction.Lines[i].DateServiceFrom.ToString("MMddyy");
                             //}
                             //if (oUB04Transaction.Transaction.Lines[i].RevenueCode.ToString().Trim() == "")
                             //{
                             //    FindControl("lbl_42_" + (i + 1).ToString(), oUB04Transaction.RevenueCode.ToString().Trim());
                             //}
                             //else
                             //{
                                 FindControl("lbl_42_" + (i + 1).ToString(), oUB04Transaction.Transaction.Lines[i].RevenueCode.ToString().Trim());
                          //   }
                             FindControl("lbl_43_" + (i + 1).ToString(), oUB04Transaction.Transaction.Lines[i].CPTDescription.ToString().Replace("&","&&").Trim());
                             //Bug #51048: 00000399 : Claim set up
                             //Description: Replacement CPT not shown in UB04 form in box44 if CPTCrosswalk is asociated to patient.
                             //So commented the code and add condition.
                             //FindControl("lbl_44_" + (i + 1).ToString(), oUB04Transaction.Transaction.Lines[i].CPTCode.ToString().Trim() + oUB04Transaction.Transaction.Lines[i].Mod1Code.ToString().Trim() + oUB04Transaction.Transaction.Lines[i].Mod2Code.ToString().Trim() + oUB04Transaction.Transaction.Lines[i].Mod3Code.ToString().Trim() + oUB04Transaction.Transaction.Lines[i].Mod4Code.ToString().Trim());
                             //Check the Crosswalk
                             if (Convert.ToString(oUB04Transaction.Transaction.Lines[i].CPTCode).Trim() == Convert.ToString(oUB04Transaction.Transaction.Lines[i].CrosswalkCPTCode).Trim() || Convert.ToString(oUB04Transaction.Transaction.Lines[i].CrosswalkCPTCode).Trim() == "" || oUB04Transaction.Transaction.Lines[i].CrosswalkCPTCode == null)
                             {
                                 FindControl("lbl_44_" + (i + 1).ToString(), oUB04Transaction.Transaction.Lines[i].CPTCode.ToString().Trim() + oUB04Transaction.Transaction.Lines[i].Mod1Code.ToString().Trim() + oUB04Transaction.Transaction.Lines[i].Mod2Code.ToString().Trim() + oUB04Transaction.Transaction.Lines[i].Mod3Code.ToString().Trim() + oUB04Transaction.Transaction.Lines[i].Mod4Code.ToString().Trim());
                             }
                             else
                             {
                                 FindControl("lbl_44_" + (i + 1).ToString(), oUB04Transaction.Transaction.Lines[i].CrosswalkCPTCode.ToString().Trim() + oUB04Transaction.Transaction.Lines[i].Mod1Code.ToString().Trim() + oUB04Transaction.Transaction.Lines[i].Mod2Code.ToString().Trim() + oUB04Transaction.Transaction.Lines[i].Mod3Code.ToString().Trim() + oUB04Transaction.Transaction.Lines[i].Mod4Code.ToString().Trim());
                             }
                             FindControl("lbl_45_" + (i + 1).ToString(), oUB04Transaction.Transaction.Lines[i].DateServiceFrom.ToString("MMddyy"));
                             FindControl("lbl_46_" + (i + 1).ToString(), Convert.ToDecimal(oUB04Transaction.Transaction.Lines[i].Unit).ToString("#############0.####").Trim());
                             FindControl("lbl_47_a" + (i + 1).ToString(), oUB04Transaction.Transaction.Lines[i].Total.ToString().Remove(oUB04Transaction.Transaction.Lines[i].Total.ToString().IndexOf(".")).Trim());
                             FindControl("lbl_47_b" + (i + 1).ToString(), oUB04Transaction.Transaction.Lines[i].Total.ToString().Substring(oUB04Transaction.Transaction.Lines[i].Total.ToString().IndexOf(".") + 1).Trim());
                             _chargetotal += oUB04Transaction.Transaction.Lines[i].Total;
                         }
                         for (int j = i; j < 22; j++)
                         {

                             FindControl("lbl_42_" + (j + 1).ToString(), string.Empty);
                             FindControl("lbl_43_" + (j + 1).ToString(), string.Empty);
                             FindControl("lbl_44_" + (j + 1).ToString(), string.Empty);
                             FindControl("lbl_45_" + (j + 1).ToString(), string.Empty);
                             FindControl("lbl_46_" + (j + 1).ToString(), string.Empty);
                             FindControl("lbl_47_a" + (j + 1).ToString(), string.Empty);
                             FindControl("lbl_47_b" + (j + 1).ToString(), string.Empty);
                         }
                         lbl_Page.Text = "1";
                         lbl_OF.Text = "1";
                         if (_chargetotal.ToString().Contains("."))
                         {
                             lbl_47_a23.Text = _chargetotal.ToString().Remove(_chargetotal.ToString().IndexOf(".")).Trim();
                             lbl_47_b23.Text = _chargetotal.ToString().Substring(_chargetotal.ToString().IndexOf(".") + 1).Trim();
                         }
                         else
                         {
                             lbl_47_a23.Text = _chargetotal.ToString();
                             lbl_47_b23.Text = "";
                         }
                     }

                     lbl_42_23.Text = Convert.ToString(oUB04Transaction.RevenueCodeTotal);
                     #endregion

                     #region "Box 50"

                     //for (int i = 0; oUB04Transaction.Transaction.Insurances.Count > i; i++)
                     //{
                     //    string  temp="";
                     //    if(i==0)
                     //    {
                     //        temp ="a";
                     //    }
                     //    else if(i==1)
                     //    {
                     //       temp ="b";
                     //    }
                     //    else if(i==2)
                     //    {
                     //       temp ="c";
                     //    }

                     //     FindControl("lbl_50"+temp+"1", oUB04Transaction.
                     //     }

                     lbl_50_a1.Text = oUB04Transaction.PrimaryPayerName.Trim();
                     lbl_50_b2.Text = oUB04Transaction.SecondaryPayerName.Trim();
                     lbl_50_c3.Text = oUB04Transaction.TertiaryPayerName.Trim();
                     
                     #endregion

                     #region "BOX 51"
                     if (oUB04Transaction.PrimaryHealPlanID != null)
                     {
                         lbl_51_a1.Text = oUB04Transaction.PrimaryHealPlanID.Trim();
                     }
                     else
                     {
                         lbl_51_a1.Text = "";
                     }
                     if (oUB04Transaction.SecondaryHealPlanID != null)
                     {
                         lbl_51_b2.Text = oUB04Transaction.SecondaryHealPlanID.Trim();
                     }
                     else
                     {
                         lbl_51_b2.Text = "";
                     }
                     if (oUB04Transaction.TertiaryHealPlanID != null)
                     {
                         lbl_51_c3.Text = oUB04Transaction.TertiaryHealPlanID.Trim();
                     }
                     else
                     {
                         lbl_51_c3.Text = "";
                     }
                     #endregion

                     #region "BOX 52"
                     if (oUB04Transaction.SignatureonFile)
                     {
                         if (oUB04Transaction.PrimaryPayerName.Trim() != "")
                         {
                             lbl_52_a1.Text = "Y";
                         }
                         if (oUB04Transaction.SecondaryPayerName.Trim() != "")
                         {
                             lbl_52_b2.Text = "Y";
                         }
                         if (oUB04Transaction.TertiaryPayerName.Trim() != "")
                         {

                             lbl_52_c3.Text = "Y";
                         }
                     }
                     else
                     {
                         lbl_52_a1.Text = "";
                         lbl_52_b2.Text = "";
                         lbl_52_c3.Text = "";
                     }
                     #endregion
                        
                     #region "Box 53"

                         if (oUB04Transaction.PrimaryAssignmentOfBenefits.ToString().Trim() == "True")
                         {
                             lbl_53_a1.Text = "Y";
                         }
                         else if (oUB04Transaction.PrimaryAssignmentOfBenefits.ToString().Trim() == "False")
                         {
                             lbl_53_a1.Text = "N";
                         }
                    
                    
                         if (oUB04Transaction.SecondaryAssignmentOfBenefits.ToString().Trim() == "True")
                         {
                             lbl_53_b2.Text = "Y";
                         }
                         else if (oUB04Transaction.SecondaryAssignmentOfBenefits.ToString().Trim() == "False")
                         {
                             lbl_53_b2.Text = "N";
                         }
                    
                   
                         if (oUB04Transaction.TertiaryAssignmentOfBenefits.ToString().Trim() == "True")
                         {
                             lbl_53_c3.Text = "Y";
                         }
                         else if (oUB04Transaction.TertiaryAssignmentOfBenefits.ToString().Trim() == "False")
                         {
                             lbl_53_c3.Text = "N";
                         }
                    

                     #endregion

                     #region "Box 54"

                         decimal paidpayment = 0;
                        

                         if (oUB04Transaction.PrimaryPayment.Trim().Length > 0)
                         {
                             paidpayment = Convert.ToDecimal(oUB04Transaction.PrimaryPayment);
                             if (oUB04Transaction.PrimaryPayment.Trim().Contains("."))
                             {
                                 lbl_54_a1.Text = oUB04Transaction.PrimaryPayment.Trim().Remove(oUB04Transaction.PrimaryPayment.IndexOf("."));
                                 lbl_54_a2.Text = oUB04Transaction.PrimaryPayment.Trim().Substring(oUB04Transaction.PrimaryPayment.IndexOf(".") + 1);
                             }
                             else
                             {
                                 lbl_54_a1.Text = oUB04Transaction.PrimaryPayment.Trim();
                                 lbl_54_a2.Text = "00";
                             }
                         }
                         if (oUB04Transaction.SecondaryPayment.Trim().Length > 0)
                         {
                             if (oUB04Transaction.SecondaryPayment.Trim().Contains("."))
                             {
                                 lbl_54_b1.Text = oUB04Transaction.SecondaryPayment.Trim().Remove(oUB04Transaction.SecondaryPayment.IndexOf("."));
                                 lbl_54_b2.Text = oUB04Transaction.SecondaryPayment.Trim().Substring(oUB04Transaction.SecondaryPayment.IndexOf(".") + 1);
                             }
                             else
                             {
                                 lbl_54_b1.Text = oUB04Transaction.SecondaryPayment.Trim();
                                 lbl_54_b2.Text = "00";
                             }
                             paidpayment = paidpayment + Convert.ToDecimal (oUB04Transaction.SecondaryPayment); 
                            
                         }
                         if (oUB04Transaction.TertiaryPayment.Trim().Length > 0)
                         {
                             if (oUB04Transaction.TertiaryPayment.Trim().Contains("."))
                             {
                                 lbl_54_c1.Text = oUB04Transaction.TertiaryPayment.Trim().Remove(oUB04Transaction.TertiaryPayment.IndexOf("."));
                                 lbl_54_c2.Text = oUB04Transaction.TertiaryPayment.Trim().Substring(oUB04Transaction.TertiaryPayment.IndexOf(".") + 1);
                             }
                             else
                             {
                                 lbl_54_b1.Text = oUB04Transaction.TertiaryPayment.Trim();
                                 lbl_54_b2.Text = "00";
                             }
                             paidpayment = paidpayment + Convert.ToDecimal(oUB04Transaction.TertiaryPayment); 
                         }

                     #endregion

                     #region "Box 55"
                         decimal estimateamount = 0;
                         if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                         {
                             if (Convert.ToBoolean(_dtUB04ExtendedSettings.Rows[0]["bInlcudeEstAmtDueUB04"]))
                             {
                                 if (oUB04Transaction.ResponsibleContactID == oUB04Transaction.PrimaryContactID && oUB04Transaction.ResponsibleInsuranceID == oUB04Transaction.PrimaryInsuranceID)
                                 {
                                     estimateamount = _chargetotal - paidpayment;
                                     if (estimateamount.ToString().Contains("."))
                                     {
                                         lbl_55_a1.Text = estimateamount.ToString().Remove(estimateamount.ToString().IndexOf(".")).Trim();
                                         lbl_55_a2.Text = estimateamount.ToString().Substring(estimateamount.ToString().IndexOf(".") + 1).Trim();
                                     }
                                     else
                                     {
                                         lbl_55_a1.Text = estimateamount.ToString();
                                         lbl_55_a2.Text = "00";
                                     }
                                 }
                                 else if (oUB04Transaction.ResponsibleContactID == oUB04Transaction.SecondaryContactID && oUB04Transaction.ResponsibleInsuranceID == oUB04Transaction.SecondaryInsuranceID)
                                 {
                                     estimateamount = _chargetotal - paidpayment;
                                     if (estimateamount.ToString().Contains("."))
                                     {
                                         lbl_55_b1.Text = estimateamount.ToString().Remove(estimateamount.ToString().IndexOf(".")).Trim();
                                         lbl_55_b2.Text = estimateamount.ToString().Substring(estimateamount.ToString().IndexOf(".") + 1).Trim();
                                     }
                                     else
                                     {
                                         lbl_55_b1.Text = estimateamount.ToString();
                                         lbl_55_b2.Text = "00";
                                     }
                                 }
                                 else if (oUB04Transaction.ResponsibleContactID == oUB04Transaction.TertiaryContactID && oUB04Transaction.ResponsibleInsuranceID == oUB04Transaction.TertiaryInsuranceID)
                                 {
                                     estimateamount = _chargetotal - paidpayment;
                                     if (estimateamount.ToString().Contains("."))
                                     {
                                         lbl_55_c1.Text = estimateamount.ToString().Remove(estimateamount.ToString().IndexOf(".")).Trim();
                                         lbl_55_c2.Text = estimateamount.ToString().Substring(estimateamount.ToString().IndexOf(".") + 1).Trim();
                                     }
                                     else
                                     {
                                         lbl_55_c1.Text = estimateamount.ToString();
                                         lbl_55_c2.Text = "00";
                                     }
                                 }
                             }
                         }
                     #endregion


                     #region "Box 57" 
                    lbl_57_a.Text = Convert.ToString(oUB04Transaction.PrimaryOtherQualifierValue).Trim();
                    lbl_57_b.Text = Convert.ToString(oUB04Transaction.SecondaryOtherQualifierValue).Trim();
                    lbl_57_c.Text = Convert.ToString(oUB04Transaction.TertiaryOtherQualifierValue).Trim();
                     #endregion

                     #region "Box 58 59 60 61 62"
                         lbl_58_a.Text = oUB04Transaction.PrimarySubscriberName.Trim();
                     lbl_58_b.Text = oUB04Transaction.SecondarySubscriberName.Trim();
                     lbl_58_c.Text = oUB04Transaction.TertiarySubscriberName.Trim();

                     lbl_59_a.Text = oUB04Transaction.PrimaryRelShipId.Trim();
                     lbl_59_b.Text = oUB04Transaction.SecondaryRelShipId.Trim();
                     lbl_59_c.Text = oUB04Transaction.TertiaryRelShipId.Trim();
                     
                     lbl_60_a.Text = oUB04Transaction.PrimarySubscriberID.Trim();
                     lbl_60_b.Text = oUB04Transaction.SecondarySubscriberID.Trim();
                     lbl_60_c.Text = oUB04Transaction.TertiarySubscriberID.Trim();

                     lbl_61_a.Text = oUB04Transaction.PrimaryPayerName.Trim();
                     lbl_61_b.Text = oUB04Transaction.SecondaryPayerName.Trim();
                     lbl_61_c.Text = oUB04Transaction.TertiaryPayerName.Trim();

                     lbl_62_a.Text = oUB04Transaction.PrimaryGroupNumber.Trim();
                     lbl_62_b.Text = oUB04Transaction.SecondaryGroupNumber.Trim();
                     lbl_62_c.Text = oUB04Transaction.TertiaryGroupNumber.Trim();

                     #endregion

                     #region "Box 63"
                     lbl_63_a.Text = oUB04Transaction.PrimaryPriorAuthorization.ToString().Trim();

                     #endregion

                     #region "Box 64"

                     lbl_64_a.Text = oUB04Transaction.PrimaryClaimRemittance.ToString().Trim();
                     lbl_64_b.Text = Convert.ToString(oUB04Transaction.SecondaryClaimRemittance).Trim();
                     lbl_64_c.Text = Convert.ToString(oUB04Transaction.TertiaryClaimRemittance).Trim();
                        
                     #endregion

                     #region "Box 65"

                     lbl_65_a.Text = oUB04Transaction.PrimaryEmpName.ToString().Trim();
                     lbl_65_b.Text = oUB04Transaction.SecondaryEmpName.ToString().Trim();
                     lbl_65_c.Text = oUB04Transaction.TertiaryEmpName.ToString().Trim();

                     #endregion

                     #region "Box 66"

                     lbl_66.Text = oUB04Transaction.IcdCodeRevision.ToString().Trim();
                     
                     #endregion

                     #region "Box 67 & 69"
                     DataTable dx = null;
                     dx = GetClaimDiagnosis(TransactionId, MstTransactionId);
                     if (dx != null)
                     {
                         for (int i = 0; dx.Rows.Count > i; i++)
                         {
                             FindControl("lbl_67_" + (i + 1).ToString(), Convert.ToString(dx.Rows[i]["Dx"]).Trim());
                         }
                         if (_IsIncludePrimaryDxInBox69)
                         {
                             lbl_69.Text = Convert.ToString(dx.Rows[0]["Dx"]).Trim();
                         }
                         else
                         {
                             lbl_69.Text = "";
                         }
                     }


                     dx = null;                    

                     #endregion

                     #region "Box 76

                     //string QualifierID =string.Empty;
                     //string Value =string.Empty;  
                     

                     if (oUB04Transaction.sUBBox77Rendering.ToUpper() == "Operating".ToUpper())
                     {
                        
                         if (Convert.ToString(oUB04Transaction.RenderingProviderLName).Trim() != "" || Convert.ToString(oUB04Transaction.RenderingProviderNPI) != "")
                         {
                             lbl_77_First.Text = oUB04Transaction.RenderingProviderFName;
                             lbl_77_Last.Text = oUB04Transaction.RenderingProviderLName;
                             lbl_77_NPI.Text = oUB04Transaction.RenderingProviderNPI;
                             lbl_77_Qual1.Text = lbl_76_QualID.Text;
                             lbl_77_Qual2.Text = lbl_76_QualValue.Text;
                             if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                             {
                                 if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                 {
                                     DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                 lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                 lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                 lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                 lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     _dr = null;
                                 }
                                 
                             }
                             lbl_76_QualID.Text = "";
                             lbl_76_QualValue.Text = "";



                         }
                     }
                     else if (oUB04Transaction.sUBBox77Rendering.ToUpper() == "Both Operating and Attending".ToUpper())
                     {
                         if (Convert.ToString(oUB04Transaction.RenderingProviderLName).Trim() != "" || Convert.ToString(oUB04Transaction.RenderingProviderNPI) != "")
                         {
                             lbl_77_First.Text = oUB04Transaction.RenderingProviderFName;
                             lbl_77_Last.Text = oUB04Transaction.RenderingProviderLName;
                             lbl_77_NPI.Text = oUB04Transaction.RenderingProviderNPI;
                             lbl_77_Qual1.Text = lbl_76_QualID.Text;
                             lbl_77_Qual2.Text = lbl_76_QualValue.Text;
                             lbl_76_First.Text = oUB04Transaction.RenderingProviderFName;
                             lbl_76_Last.Text = oUB04Transaction.RenderingProviderLName;
                             lbl_76_NPI.Text = oUB04Transaction.RenderingProviderNPI; ;

                             if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                             {
                                 if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                 {
                                     DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                 lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                 lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                 lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                 lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     _dr = null;
                                 }

                             }

                             if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                             {
                                 if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                 {
                                     DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                 lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                 lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                 lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                 lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     _dr = null;
                                 }

                             }

                            
                             //DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                             //if (_dr != null && _dr.Length > 0)
                             //{
                             //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                             //    {
                             //        lbl_81_a1.Text = "B3";
                             //        lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                             //    }
                             //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                             //    {
                             //        lbl_81_b1.Text = "B3";
                             //        lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                             //    }
                             //}
                             //_dr = null;


                         }
                     }
                     else if (oUB04Transaction.sUBBox77Rendering.ToUpper() == "Attending".ToUpper())
                     {
                         if (Convert.ToString(oUB04Transaction.RenderingProviderLName).Trim() != "" || Convert.ToString(oUB04Transaction.RenderingProviderNPI) != "")
                         {
                             lbl_76_First.Text = oUB04Transaction.RenderingProviderFName;
                             lbl_76_Last.Text = oUB04Transaction.RenderingProviderLName;
                             lbl_76_NPI.Text = oUB04Transaction.RenderingProviderNPI;
                             //DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                             //if (_dr != null && _dr.Length > 0)
                             //{
                             //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                             //    {
                             //        lbl_81_a1.Text = "B3";
                             //        lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                             //    }
                             //}
                             //_dr = null;
                             if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                             {
                                 if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                 {
                                     DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                 lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                 lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                 lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                 lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     _dr = null;
                                 }

                             }

                         }
                     }


                     
                         DataTable _dtProvider = GetProviderDetails(TransactionId, MstTransactionId);
                  
                     #region "81CC Set As Billing Provider"
                         if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                         {
                             if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                             {
                                 DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='BillingProvider'");
                                 if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.BillingProvider.GetHashCode())
                                 {

                                     if (_dr != null && _dr.Length > 0)
                                     {
                                         if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                         {
                                             lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                             lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                         }
                                     }

                                 }
                                 if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.BillingProvider.GetHashCode())
                                 {

                                     if (_dr != null && _dr.Length > 0)
                                     {
                                         if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                         {
                                             lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                             lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                         }
                                     }

                                 }

                                 if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.BillingProvider.GetHashCode())
                                 {

                                     if (_dr != null && _dr.Length > 0)
                                     {
                                         if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                         {
                                             lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                             lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                         }
                                     }

                                 }
                                 if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.BillingProvider.GetHashCode())
                                 {

                                     if (_dr != null && _dr.Length > 0)
                                     {
                                         if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                         {
                                             lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                             lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                         }
                                     }

                                 }

                                 _dr = null;
                             }

                         }
                         #endregion
                         if (oUB04Transaction.sUBBox77Billing.ToUpper() == "Operating".ToUpper())
                         {

                             if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                             {

                               
                                 lbl_77_First.Text = _dtProvider.Rows[0]["sFirstName"].ToString();
                                 lbl_77_Last.Text = _dtProvider.Rows[0]["sLastName"].ToString();
                                 lbl_77_NPI.Text = _dtProvider.Rows[0]["sNPI"].ToString();
                                 lbl_77_Qual1.Text = lbl_76_QualID.Text;
                                 lbl_77_Qual2.Text = lbl_76_QualValue.Text;
                                 lbl_76_QualID.Text = "";
                                 lbl_76_QualValue.Text = "";

                                 //DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='BillingProvider'");
                                 //if (_dr != null && _dr.Length > 0)
                                 //{
                                 //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                 //    {
                                 //        lbl_81_b1.Text = "B3";
                                 //        lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                 //    }
                                 //}
                                 //_dr = null;
                                 if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                                 {
                                     if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                     {
                                         DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='BillingProvider'");
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                     lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                     lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                     lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                     lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         _dr = null;
                                     }

                                 }

                             }
                         }
                         else if (oUB04Transaction.sUBBox77Billing.ToUpper() == "Both Operating and Attending".ToUpper())
                         {
                             if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                             {
                                 lbl_77_First.Text = _dtProvider.Rows[0]["sFirstName"].ToString();
                                 lbl_77_Last.Text = _dtProvider.Rows[0]["sLastName"].ToString();
                                 lbl_77_NPI.Text = _dtProvider.Rows[0]["sNPI"].ToString();
                                 lbl_77_Qual1.Text = lbl_76_QualID.Text;
                                 lbl_77_Qual2.Text = lbl_76_QualValue.Text;
                             }
                             if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                             {
                                 lbl_76_First.Text = _dtProvider.Rows[0]["sFirstName"].ToString();
                                 lbl_76_Last.Text = _dtProvider.Rows[0]["sLastName"].ToString();
                                 lbl_76_NPI.Text = _dtProvider.Rows[0]["sNPI"].ToString();


                             }
                             if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                             {
                                 if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                 {
                                     DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='BillingProvider'");
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                 lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                 lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                 lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.OperatingProviderBox77.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                 lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     _dr = null;
                                 }

                             }
                             ////AttendingProvide
                             if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                             {
                                 if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                 {
                                     DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='BillingProvider'");
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                 lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                 lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                 lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                 lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     _dr = null;
                                 }

                             }
                             //DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='BillingProvider'");
                             //if (_dr != null && _dr.Length > 0)
                             //{
                             //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                             //    {
                             //        lbl_81_a1.Text = "B3";
                             //        lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                             //    }
                             //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                             //    {
                             //        lbl_81_b1.Text = "B3";
                             //        lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                             //    }
                             //}
                             //_dr = null;

                         }
                         else if (oUB04Transaction.sUBBox77Billing.ToUpper() == "Attending".ToUpper())
                         {
                             if (_dtProvider != null && _dtProvider.Rows.Count > 0)
                             {
                                 lbl_76_First.Text = _dtProvider.Rows[0]["sFirstName"].ToString();
                                 lbl_76_Last.Text = _dtProvider.Rows[0]["sLastName"].ToString();
                                 lbl_76_NPI.Text = _dtProvider.Rows[0]["sNPI"].ToString();

                                 //DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='BillingProvider'");
                                 //if (_dr != null && _dr.Length > 0)
                                 //{
                                 //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                 //    {
                                 //        lbl_81_a1.Text = "B3";
                                 //        lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                 //    }
                                   
                                 //}
                                 //_dr = null;
                                 if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                                 {
                                     if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                     {
                                         DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='BillingProvider'");
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                     lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                     lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                     lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.AttendingProviderBox76.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                     lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         _dr = null;
                                     }

                                 }
                             }
                         }
                     
                     //GetPhysicianOtherID(Convert.ToInt64(oUB04Transaction.Transaction.ProviderID), Convert.ToInt64(oUB04Transaction.Transaction.ContactID), _ClinicID, ref QualifierID, ref Value);
                     //if (!String.IsNullOrEmpty(QualifierID) && !String.IsNullOrEmpty(Value))
                     //{
                     //    lbl_76_QualID.Text = QualifierID;
                     //    lbl_76_QualValue.Text = Value;
                     //}

                     #endregion

                     #region "Box 78 & 79"

                     bool _IsRenderringAvailable=false;
                     bool _IsReferringAvailable=false;

                     bool _BothAreSame = false;

                         if (Convert.ToString(oUB04Transaction.RenderingProviderLName).Trim() != "" || Convert.ToString(oUB04Transaction.RenderingProviderNPI) != "")
                         {
                             _IsRenderringAvailable = true;
                         }
                         if (Convert.ToString(oUB04Transaction.ReferringProviderLName).Trim() != "" || Convert.ToString(oUB04Transaction.ReferringProviderNPI).Trim() != "")
                         {
                             _IsReferringAvailable = true;
                         }

                         if (Convert.ToString(oUB04Transaction.RenderingProviderLName).Trim() == Convert.ToString(lbl_76_Last.Text).Trim()
                         && Convert.ToString(oUB04Transaction.RenderingProviderNPI) == Convert.ToString(lbl_76_NPI.Text).Trim())
                     {
                         _BothAreSame = true;
                     }
                     else
                     {
                         _BothAreSame = false;
                     }

                     if (_BothAreSame == true)
                     {
                         #region

                         if (_IsReferringAvailable == true && _IsRenderringAvailable == true)
                         {
                             if (oUB04Transaction.IncludeRendering_Attending == true)
                             {
                                 lbl_78_First.Text = Convert.ToString(oUB04Transaction.RenderingProviderFName).Trim();
                                 lbl_78_Last.Text = Convert.ToString(oUB04Transaction.RenderingProviderLName).Trim();
                                 lbl_78_NPI.Text = Convert.ToString(oUB04Transaction.RenderingProviderNPI).Trim();
                                 lbl_78_NPIQualifier.Text = "82";
                                 if (Convert.ToString(oUB04Transaction.RenderingProviderOtherID).Trim() != "")
                                 {
                                     if (Convert.ToString(oUB04Transaction.RenderingProviderQualifierMstID).Trim() != "1")
                                     {
                                         lbl_78_OtherID.Text = Convert.ToString(oUB04Transaction.RenderingProviderOtherID).Trim();
                                         lbl_78_OtherQual.Text = Convert.ToString(oUB04Transaction.RenderingProviderOtherQualifier).Trim();
                                     }
                                 }

                                 lbl_79_First.Text = Convert.ToString(oUB04Transaction.ReferringProviderFName).Trim();
                                 lbl_79_Last.Text = Convert.ToString(oUB04Transaction.ReferringProviderLName).Trim();
                                 lbl_79_NPI.Text = Convert.ToString(oUB04Transaction.ReferringProviderNPI).Trim();
                                 lbl_79_NPIQualifier.Text = "DN";
                                 lbl_79_OtherID.Text = Convert.ToString(oUB04Transaction.ReferringProviderOtherID).Trim();
                                 lbl_79_OtherQual.Text = Convert.ToString(oUB04Transaction.ReferringProviderOtherQualifier).Trim();

                                 //DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                                 //if (_dr != null && _dr.Length > 0)
                                 //{
                                 //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                 //    {
                                 //        lbl_81_c1.Text = "B3";
                                 //        lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                 //    }

                                    
                                 //}
                                 //_dr = null;

                                 if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                                 {
                                     if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                     {
                                         DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                     lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                     lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                     lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                     lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         _dr = null;
                                     }

                                 }



                                //_dr = _dtProviderTaxonomyCodes.Select("Provider='ReferringProvider'");
                                // if (_dr != null && _dr.Length > 0)
                                // {
                                //     if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                //     {
                                //         lbl_81_d1.Text = "B3";
                                //         lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                //     }

                                // }
                                // _dr = null;
                                 if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                                 {
                                     if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                     {
                                         DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='ReferringProvider'");
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.BOX79.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                     lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.BOX79.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                     lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.BOX79.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                     lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.BOX79.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                     lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         _dr = null;
                                     }

                                 }

                                 

                             }
                             else
                             {
                                 lbl_78_First.Text = Convert.ToString(oUB04Transaction.ReferringProviderFName).Trim();
                                 lbl_78_Last.Text = Convert.ToString(oUB04Transaction.ReferringProviderLName).Trim();
                                 lbl_78_NPI.Text = Convert.ToString(oUB04Transaction.ReferringProviderNPI).Trim();
                                 lbl_78_NPIQualifier.Text = "DN";
                                 lbl_78_OtherID.Text = Convert.ToString(oUB04Transaction.ReferringProviderOtherID).Trim();
                                 lbl_78_OtherQual.Text = Convert.ToString(oUB04Transaction.ReferringProviderOtherQualifier).Trim();

                                 //DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='ReferringProvider'");
                                 //if (_dr != null && _dr.Length > 0)
                                 //{
                                 //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                 //    {
                                 //        lbl_81_c1.Text = "B3";
                                 //        lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                 //    }


                                 //}
                                 //_dr = null;
                                 if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                                 {
                                     if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                     {
                                         DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='ReferringProvider'");
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                     lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                     lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                     lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                     lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         _dr = null;
                                     }

                                 }
                             }
                         }
                         else if (oUB04Transaction.IncludeRendering_Attending == true && _IsRenderringAvailable == true)
                         {
                             //if (oUB04Transaction.IncludeRendering_Attending == true && _IsRenderringAvailable == true)
                             {
                                 lbl_78_First.Text = Convert.ToString(oUB04Transaction.RenderingProviderFName).Trim();
                                 lbl_78_Last.Text = Convert.ToString(oUB04Transaction.RenderingProviderLName).Trim();
                                 lbl_78_NPI.Text = Convert.ToString(oUB04Transaction.RenderingProviderNPI).Trim();
                                 lbl_78_NPIQualifier.Text = "82";
                                 if (Convert.ToString(oUB04Transaction.RenderingProviderOtherID).Trim() != "")
                                 {
                                     if (Convert.ToString(oUB04Transaction.RenderingProviderQualifierMstID).Trim() != "1")
                                     {
                                         lbl_78_OtherID.Text = Convert.ToString(oUB04Transaction.RenderingProviderOtherID).Trim();
                                         lbl_78_OtherQual.Text = Convert.ToString(oUB04Transaction.RenderingProviderOtherQualifier).Trim();
                                     }
                                 }

                                 //DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                                 //if (_dr != null && _dr.Length > 0)
                                 //{
                                 //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                 //    {
                                 //        lbl_81_c1.Text = "B3";
                                 //        lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                 //    }


                                 //}
                                 //_dr = null;
                                 if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                                 {
                                     if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                     {
                                         DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                     lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                     lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                     lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                     lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         _dr = null;
                                     }

                                 }
                             }
                         }
                         else if (_IsReferringAvailable == true)
                             {
                                 lbl_78_First.Text = Convert.ToString(oUB04Transaction.ReferringProviderFName).Trim();
                                 lbl_78_Last.Text = Convert.ToString(oUB04Transaction.ReferringProviderLName).Trim();
                                 lbl_78_NPI.Text = Convert.ToString(oUB04Transaction.ReferringProviderNPI).Trim();
                                 lbl_78_NPIQualifier.Text = "DN";
                                 lbl_78_OtherID.Text = Convert.ToString(oUB04Transaction.ReferringProviderOtherID).Trim();
                                 lbl_78_OtherQual.Text = Convert.ToString(oUB04Transaction.ReferringProviderOtherQualifier).Trim();
                                 //DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='ReferringProvider'");
                                 //if (_dr != null && _dr.Length > 0)
                                 //{
                                 //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                 //    {
                                 //        lbl_81_c1.Text = "B3";
                                 //        lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                 //    }


                                 //}
                                 //_dr = null;
                                 if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                                 {
                                     if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                     {
                                         DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='ReferringProvider'");
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                     lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                     lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                     lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                     lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         _dr = null;
                                     }

                                 }
                             }

                         #endregion
                     }
                     else
                     {
                         #region

                         if (_IsRenderringAvailable == true && _IsReferringAvailable == false)
                         {
                             lbl_78_First.Text = Convert.ToString(oUB04Transaction.RenderingProviderFName).Trim();
                             lbl_78_Last.Text = Convert.ToString(oUB04Transaction.RenderingProviderLName).Trim();
                             lbl_78_NPI.Text = Convert.ToString(oUB04Transaction.RenderingProviderNPI).Trim();
                             lbl_78_NPIQualifier.Text = "82";
                             if (Convert.ToString(oUB04Transaction.RenderingProviderOtherID).Trim() != "")
                             {
                                 if (Convert.ToString(oUB04Transaction.RenderingProviderQualifierMstID).Trim() != "1")
                                 {
                                     lbl_78_OtherID.Text = Convert.ToString(oUB04Transaction.RenderingProviderOtherID).Trim();
                                     lbl_78_OtherQual.Text = Convert.ToString(oUB04Transaction.RenderingProviderOtherQualifier).Trim();
                                 }
                             }

                             //DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                             //if (_dr != null && _dr.Length > 0)
                             //{
                             //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                             //    {
                             //        lbl_81_c1.Text = "B3";
                             //        lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                             //    }


                             //}
                             //_dr = null;
                             if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                             {
                                 if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                 {
                                     DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.BOX78.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                 lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.BOX78.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                 lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.BOX78.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                 lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.BOX78.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                 lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     _dr = null;
                                 }

                             }
                         }
                         else if (_IsReferringAvailable == true && _IsRenderringAvailable == false)
                         {
                             lbl_78_First.Text = Convert.ToString(oUB04Transaction.ReferringProviderFName).Trim();
                             lbl_78_Last.Text = Convert.ToString(oUB04Transaction.ReferringProviderLName).Trim();
                             lbl_78_NPI.Text = Convert.ToString(oUB04Transaction.ReferringProviderNPI).Trim();
                             lbl_78_NPIQualifier.Text = "DN";
                             if (Convert.ToString(oUB04Transaction.ReferringProviderOtherID).Trim() != "")
                             {
                                 lbl_78_OtherID.Text = Convert.ToString(oUB04Transaction.ReferringProviderOtherID).Trim();
                                 lbl_78_OtherQual.Text = Convert.ToString(oUB04Transaction.ReferringProviderOtherQualifier).Trim();
                             }

                             //DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='ReferringProvider'");
                             //if (_dr != null && _dr.Length > 0)
                             //{
                             //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                             //    {
                             //        lbl_81_c1.Text = "B3";
                             //        lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                             //    }


                             //}
                             //_dr = null;
                             if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                             {
                                 if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                 {
                                     DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='ReferringProvider'");
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.BOX78.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                 lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.BOX78.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                 lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.BOX78.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                 lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }
                                     if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.BOX78.GetHashCode())
                                     {

                                         if (_dr != null && _dr.Length > 0)
                                         {
                                             if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                             {
                                                 lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                 lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                             }
                                         }

                                     }

                                     _dr = null;
                                 }

                             }
                         }
                         else if (_IsReferringAvailable == true && _IsRenderringAvailable == true)
                         {
                                 lbl_78_First.Text = Convert.ToString(oUB04Transaction.RenderingProviderFName).Trim();
                                 lbl_78_Last.Text = Convert.ToString(oUB04Transaction.RenderingProviderLName).Trim();
                                 lbl_78_NPI.Text = Convert.ToString(oUB04Transaction.RenderingProviderNPI).Trim();
                                 lbl_78_NPIQualifier.Text = "82";
                                 if (Convert.ToString(oUB04Transaction.RenderingProviderOtherID).Trim() != "")
                                 {
                                     if (Convert.ToString(oUB04Transaction.RenderingProviderQualifierMstID).Trim() != "1")
                                     {
                                         lbl_78_OtherID.Text = Convert.ToString(oUB04Transaction.RenderingProviderOtherID).Trim();
                                         lbl_78_OtherQual.Text = Convert.ToString(oUB04Transaction.RenderingProviderOtherQualifier).Trim();
                                     }
                                 }

                                 lbl_79_First.Text = Convert.ToString(oUB04Transaction.ReferringProviderFName).Trim();
                                 lbl_79_Last.Text = Convert.ToString(oUB04Transaction.ReferringProviderLName).Trim();
                                 lbl_79_NPI.Text = Convert.ToString(oUB04Transaction.ReferringProviderNPI).Trim();
                                 lbl_79_NPIQualifier.Text = "DN";
                                 if (Convert.ToString(oUB04Transaction.ReferringProviderOtherID).Trim() != "")
                                 {
                                     lbl_79_OtherID.Text = Convert.ToString(oUB04Transaction.ReferringProviderOtherID).Trim();
                                     lbl_79_OtherQual.Text = Convert.ToString(oUB04Transaction.ReferringProviderOtherQualifier).Trim();
                                 }

                                 //DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                                 //if (_dr != null && _dr.Length > 0)
                                 //{
                                 //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                 //    {
                                 //        lbl_81_c1.Text = "B3";
                                 //        lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                 //    }


                                 //}
                                 //_dr = null;
                                 if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                                 {
                                     if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                     {
                                         DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='RenderingProvider'");
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                     lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                     lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                     lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.BOX78.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                     lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         _dr = null;
                                     }

                                 }

                                 //_dr = _dtProviderTaxonomyCodes.Select("Provider='ReferringProvider'");
                                 //if (_dr != null && _dr.Length > 0)
                                 //{
                                 //    if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                 //    {
                                 //        lbl_81_d1.Text = "B3";
                                 //        lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                 //    }


                                 //}
                                 //_dr = null;
                                 if (_dtUB04ExtendedSettings != null && _dtUB04ExtendedSettings.Rows.Count > 0)
                                 {
                                     if (_dtProviderTaxonomyCodes != null && _dtProviderTaxonomyCodes.Rows.Count > 0)
                                     {
                                         DataRow[] _dr = _dtProviderTaxonomyCodes.Select("Provider='ReferringProvider'");
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCa"]) == BOX81CCA.BOX79.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_a1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCa_Qual"]);
                                                     lbl_81_a2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCb"]) == BOX81CCA.BOX79.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_b1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCb_Qual"]);
                                                     lbl_81_b2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCc"]) == BOX81CCA.BOX79.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_c1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCc_Qual"]);
                                                     lbl_81_c2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }
                                         if (Convert.ToInt16(_dtUB04ExtendedSettings.Rows[0]["nBox81CCd"]) == BOX81CCA.BOX79.GetHashCode())
                                         {

                                             if (_dr != null && _dr.Length > 0)
                                             {
                                                 if (Convert.ToString(_dr[0].ItemArray[4]) != "")
                                                 {
                                                     lbl_81_d1.Text = Convert.ToString(_dtUB04ExtendedSettings.Rows[0]["sBox81CCd_Qual"]);
                                                     lbl_81_d2.Text = Convert.ToString(_dr[0].ItemArray[4]);
                                                 }
                                             }

                                         }

                                         _dr = null;
                                     }

                                 }

                         }

                         #endregion
                     }

                     #endregion

                     #region "Box 80"

                     // lbl_80.Text = lbl_38.Text;

                        //temp = "";
                        //if (oUB04Transaction.ResponsiblePayerAddressLine1.Trim() + oUB04Transaction.ResponsiblePayerAddressLine2.ToString().Trim() == "")
                        //{
                        //    temp = oUB04Transaction.ResponsiblePayerName.Trim() + "\n";
                        //}
                        //else
                        //{
                        //    temp = oUB04Transaction.ResponsiblePayerName.Trim() + "\n" + oUB04Transaction.ResponsiblePayerAddressLine1.Trim() + " " + oUB04Transaction.ResponsiblePayerAddressLine2.ToString().Trim() + "\n";
                        //}
                        //if (oUB04Transaction.ResponsiblePayerCity.Trim() != "")
                        //    temp = temp + oUB04Transaction.ResponsiblePayerCity.Trim() + " ";
                        //if (oUB04Transaction.ResponsiblePayerState.Trim() != "")
                        //    temp = temp + oUB04Transaction.ResponsiblePayerState.Trim() + " ";
                        //if (oUB04Transaction.ResponsiblePayerZip.Trim() != "")
                        //    temp = temp + oUB04Transaction.ResponsiblePayerZip.Trim();
                        //// temp = temp + "\n" + oUB04Transaction.ResponsiblePhoneNo.Trim();
                        //lbl_80.Text = temp;

                     lbl_80.Text = Convert.ToString(oUB04Transaction.Box19Note).Trim();

                     #endregion

                     #region " Make Secondary Boxes Blank if it contains NPI "

                     if (lbl_76_NPI.Text.Trim() == lbl_76_QualValue.Text.Trim())
                     {
                         lbl_76_QualValue.Text = "";
                         lbl_76_QualID.Text = "";
                     }
                     if (lbl_78_NPI.Text.Trim() == lbl_78_OtherID.Text.Trim())
                     {
                         lbl_78_OtherID.Text = "";
                         lbl_78_OtherQual.Text = "";
                     }
                     if (lbl_79_NPI.Text.Trim() == lbl_79_OtherID.Text.Trim())
                     {
                         lbl_79_OtherID.Text = "";
                         lbl_79_OtherQual.Text = "";
                     }

                     #endregion
                     //#region "Box "
                     //oUB04Transaction.Transactio
                 }
             }
             catch (Exception ex)
             {
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
             }

              
             
        }
        
        private void FindControl(String ControlName,String Value)
        {
            try
            {
                Control[] cnt = this.Controls.Find(ControlName, true);
                if (cnt.GetUpperBound(0) >= 0)
                {
                    Label lbl = (Label)cnt[0];
                    lbl.Text = Value.ToString();
                }                 
            }
            catch (Exception)
            { }
        }

        public void PrintForm(string FormName)
        {
            System.Data.SqlClient.SqlCommand UpdateCmd = null;
            try
            {

                _InputFilePath = Application.StartupPath.ToString() + "\\UB04.tif";

                ClsBL_UB04PaperForm oUB04PaperForm = new ClsBL_UB04PaperForm(true);

                gloPrintUB04PaperForm oPrintForm = new gloPrintUB04PaperForm(_databaseconnectionstring);

                if (System.IO.File.Exists(_InputFilePath) == true)
                {

                    _IsForPrintData = false ;
                    oUB04PaperForm = SetUB04(oUB04PaperForm);

                    if (oUB04PaperForm != null)
                    { _OutputFilePath = oPrintForm.PrintUB04Form(oUB04PaperForm, _InputFilePath, true,true); }

                    if (System.IO.File.Exists(_OutputFilePath) == true)
                    {
                        bool isPrinted = false;
                        if (gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            String convertedFile = _OutputFilePath;
                            if (!(gloGlobal.gloTSPrint.UseEMFForClaims))
                            {
                                String tempPDFFilePath = gloSettings.FolderSettings.AppTempFolderPath  + DateTime.Now.ToString("yyyyMMddhhmmsstt") + ".pdf";
                                convertedFile = clsClinicalChartPrinting.ConvertTiffToPDF(convertedFile, tempPDFFilePath, false, true, false);
                            }
                            isPrinted = gloClinicalQueueFunctions.CopyPrintDoc(convertedFile, "UB04", "PrintFile");
                        }
                        if( ! isPrinted)
                        {
                            System.Drawing.Printing.StandardPrintController oprintcontroller = new System.Drawing.Printing.StandardPrintController();
                            System.Drawing.Printing.PrinterSettings.PaperSizeCollection opapers = printdoc_UB04.PrinterSettings.PaperSizes;

                            for (int i = 0; i <= opapers.Count - 1; i++)
                            {
                                if (opapers[i].PaperName.ToString().ToUpper() == "LETTER")
                                {
                                    printdoc_UB04.PrinterSettings.DefaultPageSettings.PaperSize = opapers[i];
                                    break;
                                }
                            }
                            printdoc_UB04.PrinterSettings.DefaultPageSettings.Landscape = false;
                            printdoc_UB04.PrintController = oprintcontroller;
                            printdoc_UB04.Print();
                        }
                        #region "CODE FOR UPDATE THE PRINT CLAIM DATA"
                        //20100219 Mahesh Nawal
                        if (FormName == "Claimservice")
                        {
                            System.Data.SqlClient.SqlConnection oConnection = new System.Data.SqlClient.SqlConnection();
                            try
                            {


                                oConnection.ConnectionString = _databaseconnectionstring;
                                oConnection.Open();

                                string updateSql = "Update BL_CMSEDI_Claim_Send set bIsPrinted=@bIsprinted,dtprinteddate=@dtprinteddate,iCMSFile=@iCMSFile where nClaimNo=@nClaimno and sSubclaimNo=@sSubClaimno";
                                UpdateCmd = new System.Data.SqlClient.SqlCommand(updateSql, oConnection);


                                UpdateCmd.Parameters.Add("@nClaimno", SqlDbType.BigInt);

                                UpdateCmd.Parameters.Add("@bIsprinted", SqlDbType.Bit);
                                UpdateCmd.Parameters.Add("@iCMSFile", SqlDbType.Image);
                                UpdateCmd.Parameters.Add("@dtprinteddate", SqlDbType.DateTime);
                                UpdateCmd.Parameters.Add("@sSubClaimno", SqlDbType.VarChar);


                                UpdateCmd.Parameters["@nClaimno"].Value = oUB04Transaction.Transaction.ClaimNo;
                                UpdateCmd.Parameters["@sSubClaimno"].Value = oUB04Transaction.Transaction.SubClaimNo;
                                UpdateCmd.Parameters["@bIsprinted"].Value = 1;
                                UpdateCmd.Parameters["@dtprinteddate"].Value = System.DateTime.Now;
                                UpdateCmd.Parameters["@iCMSFile"].Value = ConvertFileToBinary(_OutputFilePath);

                                UpdateCmd.ExecuteNonQuery();

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
                                oConnection.Close();

                            }


                        }
                        #endregion
                    }
                   
               }
                else
                {
                    MessageBox.Show("Source UB04 file not present", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        public void PrintData(string FormName)
        {
            System.Data.SqlClient.SqlCommand UpdateCmd = null;
            try
            {
               // _InputFilePath = Application.StartupPath.ToString() + "\\UB04.tif";
                _InputFilePath = Application.StartupPath.ToString();
              //  ClsBL_UB04PaperForm oUB04PaperForm = new ClsBL_UB04PaperForm(false );
                ClsBL_UB04PaperForm oUB04PaperForm = null;
                _IsForPrintData = true;
                if (printdoc_UB04.PrinterSettings.DefaultPageSettings.PrinterSettings != null)
                {
                    if (printdoc_UB04.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName != "")
                    {
                        oUB04PaperForm = new ClsBL_UB04PaperForm(printdoc_UB04.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName.ToString());
                    }
                }
                else
                {
                    oUB04PaperForm = new ClsBL_UB04PaperForm(false);
                }


                gloPrintUB04PaperForm oPrintForm = new gloPrintUB04PaperForm(_databaseconnectionstring);

             //   if (System.IO.File.Exists(_InputFilePath) == true)
           //     {
                    oUB04PaperForm = SetUB04(oUB04PaperForm);
                    if (oUB04PaperForm != null)
                    { _OutputFilePath = oPrintForm.PrintUB04Form(oUB04PaperForm, _InputFilePath, false, true); }

                    if (System.IO.File.Exists(_OutputFilePath) == true)
                    {
                        bool isPrinted = false;
                        if (gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            string sPrinterName = "Default";
                            if (printdoc_UB04.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName != "")
                            {
                                sPrinterName = printdoc_UB04.PrinterSettings.DefaultPageSettings.PrinterSettings.PrinterName;
                            }
                            clsPrintDocumentConversion oConversion = new clsPrintDocumentConversion(_databaseconnectionstring, sPrinterName, "UB");
                            String convertedFile = printdoc_UB04_Conversion(oConversion,true);
                            String imageFile = convertedFile;
                            if (!(gloGlobal.gloTSPrint.UseEMFForClaims))
                            {
                                String tempPDFFilePath = gloSettings.FolderSettings.AppTempFolderPath + DateTime.Now.ToString("yyyyMMddhhmmsstt") + ".pdf";
                                convertedFile = clsClinicalChartPrinting.ConvertTiffToPDF(convertedFile, tempPDFFilePath, true, true, false);
                            }

                            isPrinted=gloClinicalQueueFunctions.CopyPrintDoc(convertedFile, "UB04", "PrintData");
                            if (isPrinted)
                            {
                                _OutputFilePath = imageFile;
                            }
                        }
                        if ( ! isPrinted)
                        {
                            System.Drawing.Printing.StandardPrintController oprintcontroller = new System.Drawing.Printing.StandardPrintController();
                            System.Drawing.Printing.PrinterSettings.PaperSizeCollection opapers = printdoc_UB04.PrinterSettings.PaperSizes;

                            for (int i = 0; i <= opapers.Count - 1; i++)
                            {
                                if (opapers[i].PaperName.ToString().ToUpper() == "LETTER")
                                {
                                    printdoc_UB04.PrinterSettings.DefaultPageSettings.PaperSize = opapers[i];
                                    break;
                                }
                            }
                            printdoc_UB04.PrinterSettings.DefaultPageSettings.Landscape = false;
                            printdoc_UB04.PrintController = oprintcontroller;
                            printdoc_UB04.Print();
                        }
                        #region "CODE FOR UPDATE THE PRINT CLAIM DATA"
                        //20100219 Mahesh Nawal
                        if (FormName == "Claimservice")
                        {
                            System.Data.SqlClient.SqlConnection oConnection = new System.Data.SqlClient.SqlConnection();
                            try
                            {


                                oConnection.ConnectionString = _databaseconnectionstring;
                                oConnection.Open();

                                string updateSql = "Update BL_CMSEDI_Claim_Send set bIsPrinted=@bIsprinted,dtprinteddate=@dtprinteddate,iCMSFile=@iCMSFile where nClaimNo=@nClaimno and sSubclaimNo=@sSubClaimno";
                                UpdateCmd = new System.Data.SqlClient.SqlCommand(updateSql, oConnection);


                                UpdateCmd.Parameters.Add("@nClaimno", SqlDbType.BigInt);

                                UpdateCmd.Parameters.Add("@bIsprinted", SqlDbType.Bit);
                                UpdateCmd.Parameters.Add("@iCMSFile", SqlDbType.Image);
                                UpdateCmd.Parameters.Add("@dtprinteddate", SqlDbType.DateTime);
                                UpdateCmd.Parameters.Add("@sSubClaimno", SqlDbType.VarChar);

                              
                                UpdateCmd.Parameters["@nClaimno"].Value = oUB04Transaction.Transaction.ClaimNo;
                                UpdateCmd.Parameters["@sSubClaimno"].Value = oUB04Transaction.Transaction.SubClaimNo;
                                UpdateCmd.Parameters["@bIsprinted"].Value = 1;
                                UpdateCmd.Parameters["@dtprinteddate"].Value = System.DateTime.Now;
                                UpdateCmd.Parameters["@iCMSFile"].Value = ConvertFileToBinary(_OutputFilePath);

                                UpdateCmd.ExecuteNonQuery();

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
                                oConnection.Close();

                            }


                        }
                        #endregion

                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog("Output UB04 file not found",false);
                        //MessageBox.Show("Source UB04 file not present", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
               // }
                //else
                //{
                //    MessageBox.Show("Source UB04 file not present", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //public string Print(bool PrintOnForm, string sPrinterName = "Default")
        //{
        //    string sReturnedPath = string.Empty;
        //    try
        //    {
                
        //        _InputFilePath = Application.StartupPath.ToString()+ "\\DLL\\UB04.tif";
        //        ClsBL_UB04PaperForm oUB04PaperForm = null;
        //        if (PrintOnForm)
        //        {
        //            oUB04PaperForm = new ClsBL_UB04PaperForm(true);
        //        }
        //        else
        //        {
        //            oUB04PaperForm = new ClsBL_UB04PaperForm(sPrinterName);
        //        }
        //        gloPrintUB04PaperForm oPrintForm = new gloPrintUB04PaperForm();
                
        //        oUB04PaperForm = SetUB04(oUB04PaperForm);
        //        if (oUB04PaperForm != null)
        //        { _OutputFilePath = oPrintForm.PrintUB04Form(oUB04PaperForm, _InputFilePath, true); }

        //        if (System.IO.File.Exists(_OutputFilePath) == true)
        //        {
        //            sReturnedPath = _OutputFilePath;
        //        }
        //    }
        //    catch //(Exception ex)
        //    {
        //        //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }

        //    return sReturnedPath;
        //}

        //New Funtion added to Print UB04 form from EMR Clinical Chart
        public string Print(bool PrintOnForm, string FilePath, string sPrinterName = "Default", bool isForPrint = false)
        {
            string sReturnedPath = string.Empty;
            string tempPDFFolderPath = string.Empty;
            string tempPDFFilePath = string.Empty;

            try
            {

                _InputFilePath = Application.StartupPath.ToString();

                tempPDFFolderPath = gloSettings.FolderSettings.AppTempFolderPath + "ConvertPDF";
                if (System.IO.Directory.Exists(tempPDFFolderPath) == false)
                { System.IO.Directory.CreateDirectory(tempPDFFolderPath); }
                tempPDFFilePath = tempPDFFolderPath + "\\" + DateTime.Now.ToString("yyyyMMddhhmmsstt") + ".pdf";

                ClsBL_UB04PaperForm oUB04PaperForm = null;
                if (PrintOnForm)
                {
                    oUB04PaperForm = new ClsBL_UB04PaperForm(true);
                    _IsForPrintData = false ;
                }
                else
                {
                    oUB04PaperForm = new ClsBL_UB04PaperForm(sPrinterName);
                    _IsForPrintData = true;
                }
                gloPrintUB04PaperForm oPrintForm = new gloPrintUB04PaperForm(_databaseconnectionstring);

                oUB04PaperForm = SetUB04(oUB04PaperForm);
                if (oUB04PaperForm != null)
                { _OutputFilePath = oPrintForm.PrintUB04Form(oUB04PaperForm, _InputFilePath, PrintOnForm, isForPrint); }

                if (PrintOnForm == false)
                {
                    clsPrintDocumentConversion oConversion = new clsPrintDocumentConversion(_databaseconnectionstring, sPrinterName, "UB");
                    _OutputFilePath = printdoc_UB04_Conversion(oConversion, isForPrint);
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }

            return sReturnedPath;
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

        private string formatedData(ClsBL_UB04PaperForm.FormFieldString BoxField, string sdata)
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
        public ClsBL_UB04PaperForm SetUB04(ClsBL_UB04PaperForm oUB04)
        {

            //  ClsBL_UB04PaperForm oUB04 = new ClsBL_UB04PaperForm();
            try
            {
                clsgloBilling oclsgloBilling = new clsgloBilling(_databaseconnectionstring);
                string _sResult = oclsgloBilling.GetFontSetupSettingformCmsAndUB("Capatalize UB04 data");
                _IsCapatalize = Convert.ToBoolean(_sResult == "" ? false : Convert.ToBoolean(_sResult));
                if (oclsgloBilling != null)
                {
                    oclsgloBilling.Dispose();
                    oclsgloBilling = null;
                }

                oUB04.UB04_1_ProviderName.Value = formatedData(oUB04.UB04_1_ProviderName, lbl_1a.Text.Trim());
                oUB04.UB04_1_ProviderAddress.Value = formatedData(oUB04.UB04_1_ProviderAddress, lbl_1b.Text.Trim());
                oUB04.UB04_1a_ProviderCity.Value = formatedData(oUB04.UB04_1a_ProviderCity, lbl_1c.Text.Trim());
                //oUB04.UB04_1b_ProviderState.Value = "PA";
                //oUB04.UB04_1c_ProviderZipCode.Value = "19103";
                oUB04.UB04_1a_ProviderPhone.Value = formatedData(oUB04.UB04_1a_ProviderPhone, lbl_1d.Text.Trim());
                oUB04.UB04_1b_ProviderFaxNumber.Value = "";
                oUB04.UB04_1c_ProviderCountryCode.Value = "";

                oUB04.UB04_2_PayToName.Value = "";
                oUB04.UB04_2_PayToAddress.Value = "";
                oUB04.UB04_2a_Pay_toCity.Value = "";
                oUB04.UB04_2b_Pay_toState.Value = "";
                oUB04.UB04_2a_Pay_toZip.Value = "";
                oUB04.UB04_2b_ReservedFL02.Value = "";

                // Patient Control Number
                oUB04.UB04_3a_PatientControlNumber.Value = formatedData(oUB04.UB04_3a_PatientControlNumber, lbl_3a.Text.Trim());
                oUB04.UB04_3b_MedicalHealthRecordNumber.Value = formatedData(oUB04.UB04_3b_MedicalHealthRecordNumber, lbl_3b.Text.Trim());
                //Type Of Bill
                oUB04.UB04_4_TypeofBill.Value = formatedData(oUB04.UB04_4_TypeofBill, lbl_4.Text.Trim());
                //oUB04.UB04_4_TypeofBillFrequencyCode.Value = "";

                //Fedaral Tax Number
                oUB04.UB04_5_FederalTaxNumber_Upperline.Value = formatedData(oUB04.UB04_5_FederalTaxNumber_Upperline, lbl_5a.Text.Trim());
                oUB04.UB04_5_FederalTaxNumber_Lowerline.Value = formatedData(oUB04.UB04_5_FederalTaxNumber_Lowerline, lbl_5b.Text.Trim());
                //Statement Covers Period(From - Through)
                oUB04.UB04_6a_StatementCoversPeriod_From.Value = formatedData(oUB04.UB04_6a_StatementCoversPeriod_From, lbl_6a.Text.Trim());
                oUB04.UB04_6b_StatementCoversPeriod_Through.Value = formatedData(oUB04.UB04_6b_StatementCoversPeriod_Through, lbl_6b.Text.Trim());
                //Reserved
                oUB04.UB04_7a_ReservedFL07A.Value = formatedData(oUB04.UB04_7a_ReservedFL07A, lbl_7.Text.Trim());
                oUB04.UB04_7b_ReservedFL07B.Value = "";
                //Patient Identifier ,Patient Social Security Number
                // oUB04.UB04_8_PatientIdentifier.Value = lbl_8a.Text.Trim());
                oUB04.UB04_8a_PatientSocialSecurityNumber.Value = formatedData(oUB04.UB04_8a_PatientSocialSecurityNumber, lbl_8a.Text.Trim());
                oUB04.UB04_8b_PatientName.Value = formatedData(oUB04.UB04_8b_PatientName, lbl_8b.Text.Trim());
                //Patient Address
                oUB04.UB04_9a_PatientStreetAddress.Value = formatedData(oUB04.UB04_9a_PatientStreetAddress, lbl_9a.Text.Trim());
                oUB04.UB04_9b_PatientCity.Value = formatedData(oUB04.UB04_9b_PatientCity, lbl_9b.Text.Trim());
                oUB04.UB04_9c_PatientState.Value = formatedData(oUB04.UB04_9c_PatientState, lbl_9c.Text.Trim());
                oUB04.UB04_9d_PatientZip.Value = formatedData(oUB04.UB04_9d_PatientZip, lbl_9d.Text.Trim());
                oUB04.UB04_9e_PatientCountryCode.Value = formatedData(oUB04.UB04_9e_PatientCountryCode, lbl_9e.Text.Trim());
                //Birth Date

                oUB04.UB04_10_PatientBirthDate.Value = formatedData(oUB04.UB04_10_PatientBirthDate, lbl_10.Text.ToString().Trim());
                //Gender Admission Date,Admission(Hour),Admission(Type),Admission(Source),Discharge(Hour),Discharge(Status)

                oUB04.UB04_11_PatientGender.Value = formatedData(oUB04.UB04_11_PatientGender, lbl_11.Text);

                // oUB04.UB04_11_PatientMaritalStatus.Value = lbl_11.Text.Trim());

                oUB04.UB04_12_Admission_Visit_StartofCareDate.Value = formatedData(oUB04.UB04_12_Admission_Visit_StartofCareDate, lbl_12.Text.Trim());
                oUB04.UB04_13_Admission_Visit_Hour.Value = formatedData(oUB04.UB04_13_Admission_Visit_Hour, lbl_13.Text.Trim());
                oUB04.UB04_14_AdmissionType.Value = formatedData(oUB04.UB04_14_AdmissionType, lbl_14.Text.Trim());
                oUB04.UB04_15_ReferralSource.Value = formatedData(oUB04.UB04_15_ReferralSource, lbl_15.Text.Trim());
                oUB04.UB04_16_DischargeHour.Value = formatedData(oUB04.UB04_16_DischargeHour, lbl_16.Text.Trim());
                oUB04.UB04_17_DischargeStatus.Value = formatedData(oUB04.UB04_17_DischargeStatus, lbl_17.Text.Trim());
                //Condition Code
                oUB04.UB04_18_ConditionCodes.Value = formatedData(oUB04.UB04_18_ConditionCodes, lbl_18.Text.Trim());
                oUB04.UB04_19_ConditionCodes.Value = formatedData(oUB04.UB04_19_ConditionCodes, lbl_19.Text.Trim());
                oUB04.UB04_20_ConditionCodes.Value = formatedData(oUB04.UB04_20_ConditionCodes, lbl_20.Text.Trim());
                oUB04.UB04_21_ConditionCodes.Value = formatedData(oUB04.UB04_21_ConditionCodes, lbl_21.Text.Trim());
                oUB04.UB04_22_ConditionCodes.Value = formatedData(oUB04.UB04_22_ConditionCodes, lbl_22.Text.Trim());
                oUB04.UB04_23_ConditionCodes.Value = formatedData(oUB04.UB04_23_ConditionCodes, lbl_23.Text.Trim());
                oUB04.UB04_24_ConditionCodes.Value = formatedData(oUB04.UB04_24_ConditionCodes, lbl_24.Text.Trim());
                oUB04.UB04_25_ConditionCodes.Value = formatedData(oUB04.UB04_25_ConditionCodes, lbl_25.Text.Trim());
                oUB04.UB04_26_ConditionCodes.Value = formatedData(oUB04.UB04_26_ConditionCodes, lbl_26.Text.Trim());
                oUB04.UB04_27_ConditionCodes.Value = formatedData(oUB04.UB04_27_ConditionCodes, lbl_27.Text.Trim());
                oUB04.UB04_28_ConditionCodes.Value = formatedData(oUB04.UB04_28_ConditionCodes, lbl_28.Text.Trim());
                //Accident State
                oUB04.UB04_29_AccidentState.Value = formatedData(oUB04.UB04_29_AccidentState, lbl_29.Text.Trim());
                //Reserved
                oUB04.UB04_30_ReservedFL30A.Value = formatedData(oUB04.UB04_30_ReservedFL30A, lbl_30.Text.Trim());
                oUB04.UB04_30_ReservedFL30B.Value = "";

                //Occurrence Code Details
                oUB04.UB04_31a_OccurrenceCode.Value = formatedData(oUB04.UB04_31a_OccurrenceCode, lbl_31a1.Text.Trim());
                oUB04.UB04_31a_OccurrenceDate.Value = formatedData(oUB04.UB04_31a_OccurrenceDate, lbl_31a2.Text.Trim());
                oUB04.UB04_31b_OccurrenceDate.Value = formatedData(oUB04.UB04_31b_OccurrenceDate, lbl_31b2.Text.Trim());
                oUB04.UB04_31b_OccurrenceCode.Value = formatedData(oUB04.UB04_31b_OccurrenceCode, lbl_31b1.Text.Trim());



                oUB04.UB04_32a_OccurrenceCode.Value = formatedData(oUB04.UB04_32a_OccurrenceCode, lbl_32a1.Text.Trim());
                oUB04.UB04_32a_OccurrenceDate.Value = formatedData(oUB04.UB04_32a_OccurrenceDate, lbl_32a2.Text.Trim());
                oUB04.UB04_32b_OccurrenceCode.Value = formatedData(oUB04.UB04_32b_OccurrenceCode, lbl_32b1.Text.Trim());
                oUB04.UB04_32b_OccurrenceDate.Value = formatedData(oUB04.UB04_32b_OccurrenceDate, lbl_32b2.Text.Trim());

                oUB04.UB04_33a_OccurrenceCode.Value = formatedData(oUB04.UB04_33a_OccurrenceCode, lbl_33a1.Text.Trim());
                oUB04.UB04_33a_OccurrenceDate.Value = formatedData(oUB04.UB04_33a_OccurrenceDate, lbl_33a2.Text.Trim());
                oUB04.UB04_33b_OccurrenceCode.Value = formatedData(oUB04.UB04_33b_OccurrenceCode, lbl_33b1.Text.Trim());
                oUB04.UB04_33b_OccurrenceDate.Value = formatedData(oUB04.UB04_33b_OccurrenceDate, lbl_33b2.Text.Trim());

                oUB04.UB04_34a_OccurrenceCode.Value = formatedData(oUB04.UB04_34a_OccurrenceCode, lbl_34a1.Text.Trim());
                oUB04.UB04_34a_OccurrenceDate.Value = formatedData(oUB04.UB04_34a_OccurrenceDate, lbl_34a2.Text.Trim());
                oUB04.UB04_34b_OccurrenceCode.Value = formatedData(oUB04.UB04_34b_OccurrenceCode, lbl_34b1.Text.Trim());
                oUB04.UB04_34b_OccurrenceDate.Value = formatedData(oUB04.UB04_34b_OccurrenceDate, lbl_34b2.Text.Trim());

                oUB04.UB04_35a_OccurrenceSpanCode.Value = formatedData(oUB04.UB04_35a_OccurrenceSpanCode, lbl_35a1.Text.Trim());
                oUB04.UB04_35a_OccurrenceSpanDateFrom.Value = formatedData(oUB04.UB04_35a_OccurrenceSpanDateFrom, lbl_35a2.Text.Trim());
                oUB04.UB04_35a_OccurrenceSpanDateThrough.Value = formatedData(oUB04.UB04_35a_OccurrenceSpanDateThrough, lbl_35a3.Text.Trim());
                oUB04.UB04_35b_OccurrenceSpanCode.Value = formatedData(oUB04.UB04_35b_OccurrenceSpanCode, lbl_35b1.Text.Trim());
                oUB04.UB04_35b_OccurrenceSpanDateFrom.Value = formatedData(oUB04.UB04_35b_OccurrenceSpanDateFrom, lbl_35b2.Text.Trim());
                oUB04.UB04_35b_OccurrenceSpanDateThrough.Value = formatedData(oUB04.UB04_35b_OccurrenceSpanDateThrough, lbl_35b3.Text.Trim());

                oUB04.UB04_36a_OccurrenceSpanCode.Value = formatedData(oUB04.UB04_36a_OccurrenceSpanCode, lbl_36a1.Text.Trim());
                oUB04.UB04_36a_OccurrenceSpanDateFrom.Value = formatedData(oUB04.UB04_36a_OccurrenceSpanDateFrom, lbl_36a2.Text.Trim());
                oUB04.UB04_36a_OccurrenceSpanDateThrough.Value = formatedData(oUB04.UB04_36a_OccurrenceSpanDateThrough, lbl_36a3.Text.Trim());
                oUB04.UB04_36b_OccurrenceSpanCode.Value = formatedData(oUB04.UB04_36b_OccurrenceSpanCode, lbl_36b1.Text.Trim());
                oUB04.UB04_36b_OccurrenceSpanDateFrom.Value = formatedData(oUB04.UB04_36b_OccurrenceSpanDateFrom, lbl_36b2.Text.Trim());
                oUB04.UB04_36b_OccurrenceSpanDateThrough.Value = formatedData(oUB04.UB04_36b_OccurrenceSpanDateThrough, lbl_36b3.Text.Trim());


                //Future Use
                //   oUB04.UB04_37_ReservedFL37.Value = "";
                //Responsible Party Name/Address
                oUB04.UB04_38_ResponsiblePartyNameAddress.Value = formatedData(oUB04.UB04_38_ResponsiblePartyNameAddress, lbl_38.Text.Trim());
                //Value Code,Value Code Amount
                oUB04.UB04_39a_ValueCode.Value = formatedData(oUB04.UB04_39a_ValueCode, lbl_39a1.Text.Trim());
                oUB04.UB04_39a_ValueCodeAmount = new ClsBL_UB04PaperForm.FormFieldString(oUB04.UB04_39a_ValueCodeAmount.Location.X - (GetControlValue("lbl_39a2").ToString().Length * 16) + 20, (oUB04.UB04_39a_ValueCodeAmount.Location.Y), oUB04.UB04_39a_ValueCodeAmount.CharSize);
                oUB04.UB04_39a_ValueCodeAmount.Value = formatedData(oUB04.UB04_39a_ValueCodeAmount, lbl_39a2.Text.Trim());
                oUB04.UB04_39a_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_39a_ValueCodeAmount_Cents, lbl_39a3.Text.Trim());

                oUB04.UB04_39b_ValueCode.Value = formatedData(oUB04.UB04_39b_ValueCode, lbl_39b1.Text.Trim());
                oUB04.UB04_39b_ValueCodeAmount = new ClsBL_UB04PaperForm.FormFieldString(oUB04.UB04_39b_ValueCodeAmount.Location.X - (GetControlValue("lbl_39b2").ToString().Length * 16) + 20, (oUB04.UB04_39b_ValueCodeAmount.Location.Y), oUB04.UB04_39b_ValueCodeAmount.CharSize );
                oUB04.UB04_39b_ValueCodeAmount.Value = formatedData(oUB04.UB04_39b_ValueCodeAmount, lbl_39b2.Text.Trim());
                oUB04.UB04_39b_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_39b_ValueCodeAmount_Cents, lbl_39b3.Text.Trim());

                oUB04.UB04_39c_ValueCode.Value = formatedData(oUB04.UB04_39c_ValueCode, lbl_39c1.Text.Trim());
                oUB04.UB04_39c_ValueCodeAmount = new ClsBL_UB04PaperForm.FormFieldString(oUB04.UB04_39c_ValueCodeAmount.Location.X - (GetControlValue("lbl_39c2").ToString().Length * 16) + 20, (oUB04.UB04_39c_ValueCodeAmount.Location.Y), oUB04.UB04_39c_ValueCodeAmount.CharSize );
                oUB04.UB04_39c_ValueCodeAmount.Value = formatedData(oUB04.UB04_39c_ValueCodeAmount, lbl_39c2.Text.Trim());
                oUB04.UB04_39c_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_39c_ValueCodeAmount_Cents, lbl_39c3.Text.Trim());

                oUB04.UB04_39d_ValueCode.Value = formatedData(oUB04.UB04_39d_ValueCode, lbl_39d1.Text.Trim());
                oUB04.UB04_39d_ValueCodeAmount = new ClsBL_UB04PaperForm.FormFieldString(oUB04.UB04_39d_ValueCodeAmount.Location.X - (GetControlValue("lbl_39d2").ToString().Length * 16) + 20, (oUB04.UB04_39d_ValueCodeAmount.Location.Y), oUB04.UB04_39d_ValueCodeAmount.CharSize );
                oUB04.UB04_39d_ValueCodeAmount.Value = formatedData(oUB04.UB04_39d_ValueCodeAmount, lbl_39d2.Text.Trim());
                oUB04.UB04_39d_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_39d_ValueCodeAmount_Cents, lbl_39d3.Text.Trim());

                oUB04.UB04_40a_ValueCode.Value = formatedData(oUB04.UB04_40a_ValueCode, lbl_40a1.Text.Trim());
                oUB04.UB04_40a_ValueCodeAmount = new ClsBL_UB04PaperForm.FormFieldString(oUB04.UB04_40a_ValueCodeAmount.Location.X - (GetControlValue("lbl_40a2").ToString().Length * 16) + 20, (oUB04.UB04_40a_ValueCodeAmount.Location.Y), oUB04.UB04_40a_ValueCodeAmount.CharSize );
                oUB04.UB04_40a_ValueCodeAmount.Value = formatedData(oUB04.UB04_40a_ValueCodeAmount, lbl_40a2.Text.Trim());
                oUB04.UB04_40a_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_40a_ValueCodeAmount_Cents, lbl_40a3.Text.Trim());

                oUB04.UB04_40b_ValueCode.Value = formatedData(oUB04.UB04_40b_ValueCode, lbl_40b1.Text.Trim());
                oUB04.UB04_40b_ValueCodeAmount = new ClsBL_UB04PaperForm.FormFieldString(oUB04.UB04_40b_ValueCodeAmount.Location.X - (GetControlValue("lbl_40b2").ToString().Length * 16) + 20, (oUB04.UB04_40b_ValueCodeAmount.Location.Y), oUB04.UB04_40b_ValueCodeAmount.CharSize );
                oUB04.UB04_40b_ValueCodeAmount.Value = formatedData(oUB04.UB04_40b_ValueCodeAmount, lbl_40b2.Text.Trim());
                oUB04.UB04_40b_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_40b_ValueCodeAmount_Cents, lbl_40b3.Text.Trim());

                oUB04.UB04_40c_ValueCode.Value = formatedData(oUB04.UB04_40c_ValueCode, lbl_40c1.Text.Trim());
                oUB04.UB04_40c_ValueCodeAmount = new ClsBL_UB04PaperForm.FormFieldString(oUB04.UB04_40c_ValueCodeAmount.Location.X - (GetControlValue("lbl_40c2").ToString().Length * 16) + 20, (oUB04.UB04_40c_ValueCodeAmount.Location.Y), oUB04.UB04_40c_ValueCodeAmount.CharSize );
                oUB04.UB04_40c_ValueCodeAmount.Value = formatedData(oUB04.UB04_40c_ValueCodeAmount, lbl_40c2.Text.Trim());
                oUB04.UB04_40c_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_40c_ValueCodeAmount_Cents, lbl_40c3.Text.Trim());

                oUB04.UB04_40d_ValueCode.Value = formatedData(oUB04.UB04_40d_ValueCode, lbl_40d1.Text.Trim());
                oUB04.UB04_40d_ValueCodeAmount = new ClsBL_UB04PaperForm.FormFieldString(oUB04.UB04_40d_ValueCodeAmount.Location.X - (GetControlValue("lbl_40d2").ToString().Length * 16) + 20, (oUB04.UB04_40d_ValueCodeAmount.Location.Y), oUB04.UB04_40d_ValueCodeAmount.CharSize );
                oUB04.UB04_40d_ValueCodeAmount.Value = formatedData(oUB04.UB04_40d_ValueCodeAmount, lbl_40d2.Text.Trim());
                oUB04.UB04_40d_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_40d_ValueCodeAmount_Cents, lbl_40d3.Text.Trim());

                oUB04.UB04_41a_ValueCode.Value = formatedData(oUB04.UB04_41a_ValueCode, lbl_41a1.Text.Trim());
                oUB04.UB04_41a_ValueCodeAmount = new ClsBL_UB04PaperForm.FormFieldString(oUB04.UB04_41a_ValueCodeAmount.Location.X - (GetControlValue("lbl_41a2").ToString().Length * 16) + 20, (oUB04.UB04_41a_ValueCodeAmount.Location.Y), oUB04.UB04_41a_ValueCodeAmount.CharSize );
                oUB04.UB04_41a_ValueCodeAmount.Value = formatedData(oUB04.UB04_41a_ValueCodeAmount, lbl_41a2.Text.Trim());
                oUB04.UB04_41a_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_41a_ValueCodeAmount_Cents, lbl_41a3.Text.Trim());

                oUB04.UB04_41b_ValueCode.Value = formatedData(oUB04.UB04_41b_ValueCode, lbl_41b1.Text.Trim());
                oUB04.UB04_41b_ValueCodeAmount = new ClsBL_UB04PaperForm.FormFieldString(oUB04.UB04_41b_ValueCodeAmount.Location.X - (GetControlValue("lbl_41b2").ToString().Length * 16) + 20, (oUB04.UB04_41b_ValueCodeAmount.Location.Y), oUB04.UB04_41b_ValueCodeAmount.CharSize );
                oUB04.UB04_41b_ValueCodeAmount.Value = formatedData(oUB04.UB04_41b_ValueCodeAmount, lbl_41b2.Text.Trim());
                oUB04.UB04_41b_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_41b_ValueCodeAmount_Cents, lbl_41b3.Text.Trim());

                oUB04.UB04_41c_ValueCode.Value = formatedData(oUB04.UB04_41c_ValueCode, lbl_41c1.Text.Trim());
                oUB04.UB04_41c_ValueCodeAmount = new ClsBL_UB04PaperForm.FormFieldString(oUB04.UB04_41c_ValueCodeAmount.Location.X - (GetControlValue("lbl_41c2").ToString().Length * 16) + 20, (oUB04.UB04_41c_ValueCodeAmount.Location.Y), oUB04.UB04_41c_ValueCodeAmount.CharSize );
                oUB04.UB04_41c_ValueCodeAmount.Value = formatedData(oUB04.UB04_41c_ValueCodeAmount, lbl_41c2.Text.Trim());
                oUB04.UB04_41c_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_41c_ValueCodeAmount_Cents, lbl_41c3.Text.Trim());

                oUB04.UB04_41d_ValueCode.Value = formatedData(oUB04.UB04_41d_ValueCode, lbl_41d1.Text.Trim());
                oUB04.UB04_41d_ValueCodeAmount = new ClsBL_UB04PaperForm.FormFieldString(oUB04.UB04_41d_ValueCodeAmount.Location.X - (GetControlValue("lbl_41d2").ToString().Length * 16) + 20, (oUB04.UB04_41d_ValueCodeAmount.Location.Y), oUB04.UB04_41d_ValueCodeAmount.CharSize );
                oUB04.UB04_41d_ValueCodeAmount.Value = formatedData(oUB04.UB04_41d_ValueCodeAmount, lbl_41d2.Text.Trim());
                oUB04.UB04_41d_ValueCodeAmount_Cents.Value = formatedData(oUB04.UB04_41d_ValueCodeAmount_Cents, lbl_41d3.Text.Trim());

                //Revenue Code


                for (int index = 0; index <= 22; index++)
                {

                    ServiceLine oServiceLine = new ServiceLine();

                    oServiceLine.UB04_42_RevenueCode = new FormFieldString(oUB04.UB04_42_RevenueCode.Location.X, (oUB04.UB04_42_RevenueCode.Location.Y + (index * 50)));
                    oServiceLine.UB04_42_RevenueCode.Value = formatedData(oUB04.UB04_42_RevenueCode, GetControlValue("lbl_42_" + (index + 1)));
                    oServiceLine.UB04_43_RevenueCodeDescription = new FormFieldString(oUB04.UB04_43_RevenueCodeDescription.Location.X, (oUB04.UB04_43_RevenueCodeDescription.Location.Y + (index * 50)));
                    oServiceLine.UB04_43_RevenueCodeDescription.Value = formatedData(oUB04.UB04_43_RevenueCodeDescription, GetControlValue("lbl_43_" + (index + 1)));
                    oServiceLine.UB04_44_RateCodes = new FormFieldString(oUB04.UB04_44_RateCodes.Location.X, (oUB04.UB04_44_RateCodes.Location.Y + (index * 50)));
                    oServiceLine.UB04_44_RateCodes.Value = formatedData(oUB04.UB04_44_RateCodes, GetControlValue("lbl_44_" + (index + 1)));
                    oServiceLine.UB04_45_ServiceDate_visit_ = new FormFieldString(oUB04.UB04_45_ServiceDate_visit_.Location.X, (oUB04.UB04_45_ServiceDate_visit_.Location.Y + (index * 50)));
                    oServiceLine.UB04_45_ServiceDate_visit_.Value = formatedData(oUB04.UB04_45_ServiceDate_visit_, GetControlValue("lbl_45_" + (index + 1)));
                    oServiceLine.UB04_46_ServiceUnits = new FormFieldString(oUB04.UB04_46_ServiceUnits.Location.X - (GetControlValue("lbl_46_" + (index + 1)).ToString().Length * 16) + 8, (oUB04.UB04_46_ServiceUnits.Location.Y + (index * 50)));
                    oServiceLine.UB04_46_ServiceUnits.Value = formatedData(oUB04.UB04_46_ServiceUnits, GetControlValue("lbl_46_" + (index + 1)));
                    oServiceLine.UB04_47a_TotalCharges_Dollars = new FormFieldString(oUB04.UB04_47a_TotalCharges_Dollars.Location.X - (GetControlValue("lbl_47_a" + (index + 1)).ToString().Length * 16) + 8, (oUB04.UB04_47a_TotalCharges_Dollars.Location.Y + (index * 50)));
                    oServiceLine.UB04_47a_TotalCharges_Dollars.Value = formatedData(oUB04.UB04_47a_TotalCharges_Dollars, GetControlValue("lbl_47_a" + (index + 1)));
                    //'total += Convert.ToInt16(oServiceLine.UB04_47a_TotalCharges_Dollars.Value)
                    oServiceLine.UB04_47b_TotalCharges_Cents = new FormFieldString(oUB04.UB04_47b_TotalCharges_Cents.Location.X, (oUB04.UB04_47b_TotalCharges_Cents.Location.Y + (index * 50)));
                    oServiceLine.UB04_47b_TotalCharges_Cents.Value = formatedData(oUB04.UB04_47b_TotalCharges_Cents, GetControlValue("lbl_47_b" + (index + 1)));
                    oServiceLine.UB04_48a_Non_coveredCharges_Dollars = new FormFieldString(oUB04.UB04_48a_Non_coveredCharges_Dollars.Location.X - oUB04.UB04_48a_Non_coveredCharges_Dollars.ToString().Length, (oUB04.UB04_48a_Non_coveredCharges_Dollars.Location.Y + (index * 50)));
                    oServiceLine.UB04_48a_Non_coveredCharges_Dollars.Value = formatedData(oUB04.UB04_48a_Non_coveredCharges_Dollars, GetControlValue("lbl_48_a" + (index + 1)));
                    //oServiceLine.UB04_48b_Non_coveredCharges_Cents = new FormFieldString(oUB04.UB04_48b_Non_coveredCharges_Cents.Location.X, (oUB04.UB04_48b_Non_coveredCharges_Cents.Location.Y + (index * 15)));
                    //oServiceLine.UB04_48b_Non_coveredCharges_Cents.Value = "";

                    oUB04.UB04_ServiceLines.Add(oServiceLine);
                }

                //for (int i = 0; i <= 22; i++)
                //{
                //    oUB04.UB04_42_RevenueCode.Value = GetControlValue("lbl_42_" + i);
                //    oUB04.UB04_43_RevenueCodeDescription.Value = GetControlValue("lbl_43_" + i);
                //    oUB04.UB04_44_RateCodes.Value = GetControlValue("lbl_44_" + i);
                //}
                //  oUB04.UB04_42_RevenueCode.Value = lbl_42_1.Text.Trim());
                //  oUB04.UB04_43_RevenueCodeDescription.Value = lbl_43_1.Text.Trim());
                //                oUB04.UB04_44_RateCodes.Value = lbl_44_1.Text.Trim());

                //  oUB04.UB04_45_ServiceDate_visit.Value = "0901110";


                //  oUB04.UB04_46_ServiceUnits.Value = lbl_46_1.Text.Trim());
                //  oUB04.UB04_47a_TotalCharges_Dollars.Value = lbl_47_a1.Text.Trim()) + lbl_47_a2.Text.Trim());
                //oUB04.UB04_47b_TotalCharges_Cents.Value = ;
                oUB04.UB04_48a_Non_coveredCharges_Dollars.Value = "";
                oUB04.UB04_48b_Non_coveredCharges_Cents.Value = "";
                oUB04.UB04_49_ReservedFL49.Value = "";
                oUB04.UB04_42L23_RevenueCode.Value = formatedData(oUB04.UB04_42L23_RevenueCode, lbl_42_23.Text.Trim());
                oUB04.UB04_47L23_SummaryTotalCharges_Dollars.Value = formatedData(oUB04.UB04_47L23_SummaryTotalCharges_Dollars, lbl_47_a23.Text.Trim());

                oUB04.UB04_47L23_SummaryTotalCharges_Cents.Value = formatedData(oUB04.UB04_47L23_SummaryTotalCharges_Cents, lbl_47_b23.Text.Trim());
                oUB04.UB04_48L23_SummaryNon_coveredCharges_Dollars.Value = "";
                oUB04.UB04_48L23_SummaryNon_coveredCharges_Cents.Value = "";
                oUB04.UB04_49L23_Reserved49L23.Value = "";
                oUB04.UB04_43L23_CurrentPage.Value = "1";
                oUB04.UB04_44L23_TotalPages.Value = "1";
                oUB04.UB04_44L23CreationDate.Value = "";
                //Primary,Secondary,Tertiary Payer Name
                oUB04.UB04_50_PayerName_Primary.Value = formatedData(oUB04.UB04_50_PayerName_Primary, lbl_50_a1.Text.Trim());
                oUB04.UB04_50_PayerName_Secondary.Value = formatedData(oUB04.UB04_50_PayerName_Secondary, lbl_50_b2.Text.Trim());
                oUB04.UB04_50_PayerName_Tertiary.Value = formatedData(oUB04.UB04_50_PayerName_Tertiary, lbl_50_c3.Text.Trim());
                //Helth Plan ID A,B,C,Release of information.,Assignment of benefits.

                oUB04.UB04_51_HealthPlanIDA.Value = formatedData(oUB04.UB04_51_HealthPlanIDA, lbl_51_a1.Text.Trim());
                oUB04.UB04_51_HealthPlanIDB.Value = formatedData(oUB04.UB04_51_HealthPlanIDB, lbl_51_b2.Text.Trim());
                oUB04.UB04_51_HealthPlanIDC.Value = formatedData(oUB04.UB04_51_HealthPlanIDC, lbl_51_c3.Text.Trim());
                oUB04.UB04_52_InformationRelease_Primary.Value = formatedData(oUB04.UB04_52_InformationRelease_Primary, lbl_52_a1.Text.Trim());
                oUB04.UB04_52_InformationRelease_Secondary.Value = formatedData(oUB04.UB04_52_InformationRelease_Secondary, lbl_52_b2.Text.Trim());
                oUB04.UB04_52_InformationRelease_Tertiary.Value = formatedData(oUB04.UB04_52_InformationRelease_Tertiary, lbl_52_c3.Text.Trim());
                oUB04.UB04_53_BenefitsAssignment_Primary.Value = formatedData(oUB04.UB04_53_BenefitsAssignment_Primary, lbl_53_a1.Text.Trim());
                oUB04.UB04_53_BenefitsAssignment_Secondary.Value = formatedData(oUB04.UB04_53_BenefitsAssignment_Secondary, lbl_53_b2.Text.Trim());
                oUB04.UB04_53_BenefitsAssignment_Tertiary.Value = formatedData(oUB04.UB04_53_BenefitsAssignment_Tertiary, lbl_53_c3.Text.Trim());
                //Prior Payment
                oUB04.UB04_54_PriorPaymentsDollars_Primary.Value = formatedData(oUB04.UB04_54_PriorPaymentsDollars_Primary, lbl_54_a1.Text.Trim());
                oUB04.UB04_54_PriorPaymentsCents_Primary.Value = formatedData(oUB04.UB04_54_PriorPaymentsCents_Primary, lbl_54_a2.Text.Trim());
                oUB04.UB04_54_PriorPaymentsDollars_Secondary.Value = formatedData(oUB04.UB04_54_PriorPaymentsDollars_Secondary, lbl_54_b1.Text.Trim());
                oUB04.UB04_54_PriorPaymentsCents_Secondary.Value = formatedData(oUB04.UB04_54_PriorPaymentsCents_Secondary, lbl_54_b2.Text.Trim());
                oUB04.UB04_54_PriorPaymentsDollars_Tertiary.Value = formatedData(oUB04.UB04_54_PriorPaymentsDollars_Tertiary, lbl_54_c1.Text.Trim());
                oUB04.UB04_54_PriorPaymentsCents_Tertiary.Value = formatedData(oUB04.UB04_54_PriorPaymentsCents_Tertiary, lbl_54_c2.Text.Trim());
                //Estimated amount due.
                oUB04.UB04_55a_EstimatedAmountDueDollars_Primary.Value = formatedData(oUB04.UB04_55a_EstimatedAmountDueDollars_Primary, lbl_55_a1.Text.Trim());
                oUB04.UB04_55a_EstimatedAmountDueCents_Primary.Value = formatedData(oUB04.UB04_55a_EstimatedAmountDueCents_Primary, lbl_55_a2.Text.Trim());
                oUB04.UB04_55b_EstimatedAmountDueDollars_Secondary.Value = formatedData(oUB04.UB04_55b_EstimatedAmountDueDollars_Secondary, lbl_55_b1.Text.Trim());
                oUB04.UB04_55b_EstimatedAmountDueCents_Secondary.Value = formatedData(oUB04.UB04_55b_EstimatedAmountDueCents_Secondary, lbl_55_b2.Text.Trim());
                oUB04.UB04_55c_EstimatedAmountDueDollars_Tertiary.Value = formatedData(oUB04.UB04_55c_EstimatedAmountDueDollars_Tertiary, lbl_55_c1.Text.Trim());
                oUB04.UB04_55c_EstimatedAmountDueCents_Tertiary.Value = formatedData(oUB04.UB04_55c_EstimatedAmountDueCents_Tertiary, lbl_55_c2.Text.Trim());
                //NPI Number
                oUB04.UB04_56_NationalProviderIdentifier_NPI_.Value = formatedData(oUB04.UB04_56_NationalProviderIdentifier_NPI_, lbl_56.Text.Trim());
                oUB04.UB04_57_OtherProvider_Primary.Value = formatedData(oUB04.UB04_57_OtherProvider_Primary, lbl_57_a.Text.Trim());
                oUB04.UB04_57_OtherProvider_Secondary.Value = formatedData(oUB04.UB04_57_OtherProvider_Secondary, lbl_57_b.Text.Trim());
                oUB04.UB04_57_OtherProvider_Tertiary.Value = formatedData(oUB04.UB04_57_OtherProvider_Tertiary, lbl_57_c.Text.Trim());
                //Othere Provider ID,Insured Name Primary,Secondary ,Tertiary,Patient Relationship,Insured Unique ID ,Group Name,Group Number

                oUB04.UB04_58_InsuredName_Primary.Value = formatedData(oUB04.UB04_58_InsuredName_Primary, lbl_58_a.Text.Trim());
                oUB04.UB04_58_InsuredName_Secondary.Value = formatedData(oUB04.UB04_58_InsuredName_Secondary, lbl_58_b.Text.Trim());
                oUB04.UB04_58_InsuredName_Tertiary.Value = formatedData(oUB04.UB04_58_InsuredName_Tertiary, lbl_58_c.Text.Trim());
                oUB04.UB04_59_PatientRelationshipToInsured_Primary.Value = formatedData(oUB04.UB04_59_PatientRelationshipToInsured_Primary, lbl_59_a.Text.Trim());
                oUB04.UB04_59_PatientRelationshipToInsured_Secondary.Value = formatedData(oUB04.UB04_59_PatientRelationshipToInsured_Secondary, lbl_59_b.Text.Trim());
                oUB04.UB04_59_PatientRelationshipToInsured_Tertiary.Value = formatedData(oUB04.UB04_59_PatientRelationshipToInsured_Tertiary, lbl_59_c.Text.Trim());
                oUB04.UB04_60_InsuredUniqueID_Primary.Value = formatedData(oUB04.UB04_60_InsuredUniqueID_Primary, lbl_60_a.Text.Trim());
                oUB04.UB04_60_InsuredUniqueID_Secondary.Value = formatedData(oUB04.UB04_60_InsuredUniqueID_Secondary, lbl_60_b.Text.Trim());
                oUB04.UB04_60_InsuredUniqueID_Tertiary.Value = formatedData(oUB04.UB04_60_InsuredUniqueID_Tertiary, lbl_60_c.Text.Trim());
                oUB04.UB04_61_InsuredGroupName_Primary.Value = formatedData(oUB04.UB04_61_InsuredGroupName_Primary, lbl_61_a.Text.Trim());
                oUB04.UB04_61_InsuredGroupName_Secondary.Value = formatedData(oUB04.UB04_61_InsuredGroupName_Secondary, lbl_61_b.Text.Trim());
                oUB04.UB04_61_InsuredGroupName_Tertiary.Value = formatedData(oUB04.UB04_61_InsuredGroupName_Tertiary, lbl_61_c.Text.Trim());
                oUB04.UB04_62_InsuredGroupNumber_Primary.Value = formatedData(oUB04.UB04_62_InsuredGroupNumber_Primary, lbl_62_a.Text.Trim());
                oUB04.UB04_62_InsuredGroupNumber_Secondary.Value = formatedData(oUB04.UB04_62_InsuredGroupNumber_Secondary, lbl_62_b.Text.Trim());
                oUB04.UB04_62_InsuredGroupNumber_Tertiary.Value = formatedData(oUB04.UB04_62_InsuredGroupNumber_Tertiary, lbl_62_c.Text.Trim());
                //Treatment Authorization Code Primary,Secondary,Tertiary,Document Control Number,Employer Name

                oUB04.UB04_63_TreatmentAuthorizationCode_Primary.Value = formatedData(oUB04.UB04_63_TreatmentAuthorizationCode_Primary, lbl_63_a.Text.Trim());
                oUB04.UB04_63_TreatmentAuthorizationCode_Secondary.Value = formatedData(oUB04.UB04_63_TreatmentAuthorizationCode_Secondary, lbl_63_b.Text.Trim());
                oUB04.UB04_63_TreatmentAuthorizationCode_Tertiary.Value = formatedData(oUB04.UB04_63_TreatmentAuthorizationCode_Tertiary, lbl_63_c.Text.Trim());
                oUB04.UB04_64_DocumentControlNumber_A.Value = formatedData(oUB04.UB04_64_DocumentControlNumber_A, lbl_64_a.Text.Trim());
                oUB04.UB04_64_DocumentControlNumber_B.Value = formatedData(oUB04.UB04_64_DocumentControlNumber_B, lbl_64_b.Text.Trim());
                oUB04.UB04_64_DocumentControlNumber_C.Value = formatedData(oUB04.UB04_64_DocumentControlNumber_C, lbl_64_c.Text.Trim());
                oUB04.UB04_65_EmployerName_Primary.Value = formatedData(oUB04.UB04_65_EmployerName_Primary, lbl_65_a.Text.Trim());
                oUB04.UB04_65_EmployerName_Secondary.Value = formatedData(oUB04.UB04_65_EmployerName_Secondary, lbl_65_b.Text.Trim());
                oUB04.UB04_65_EmployerName_Tertiary.Value = formatedData(oUB04.UB04_65_EmployerName_Tertiary, lbl_65_c.Text.Trim());
                //ICD Version Indicator,Principal Diagnosis Code,Reserved,Admitting diagnosis.,Patient Visit Reason,PPS(Code),External Cause of Injury Code,Procedure(Code)

                oUB04.UB04_66_ICDVersionIndicator.Value = formatedData(oUB04.UB04_66_ICDVersionIndicator, lbl_66.Text.Trim());
                oUB04.UB04_67_PrincipalDiagnosisCode.Value = formatedData(oUB04.UB04_67_PrincipalDiagnosisCode, lbl_67_1.Text.Trim());
                oUB04.UB04_67a_OtherDiagnosis_A.Value = formatedData(oUB04.UB04_67a_OtherDiagnosis_A, lbl_67_2.Text.Trim());
                oUB04.UB04_67b_OtherDiagnosis_B.Value = formatedData(oUB04.UB04_67b_OtherDiagnosis_B, lbl_67_3.Text.Trim());
                oUB04.UB04_67c_OtherDiagnosis_C.Value = formatedData(oUB04.UB04_67c_OtherDiagnosis_C, lbl_67_4.Text.Trim());
                oUB04.UB04_67d_OtherDiagnosis_D.Value = formatedData(oUB04.UB04_67d_OtherDiagnosis_D, lbl_67_5.Text.Trim());
                oUB04.UB04_67e_OtherDiagnosis_E.Value = formatedData(oUB04.UB04_67e_OtherDiagnosis_E, lbl_67_6.Text.Trim());
                oUB04.UB04_67f_OtherDiagnosis_F.Value = formatedData(oUB04.UB04_67f_OtherDiagnosis_F, lbl_67_7.Text.Trim());
                oUB04.UB04_67g_OtherDiagnosis_G.Value = formatedData(oUB04.UB04_67g_OtherDiagnosis_G, lbl_67_8.Text.Trim());
                oUB04.UB04_67h_OtherDiagnosis_H.Value = formatedData(oUB04.UB04_67h_OtherDiagnosis_H, lbl_67_9.Text.Trim());
                oUB04.UB04_67i_OtherDiagnosis_I.Value = formatedData(oUB04.UB04_67i_OtherDiagnosis_I, lbl_67_10.Text.Trim());
                oUB04.UB04_67j_OtherDiagnosis_J.Value = formatedData(oUB04.UB04_67j_OtherDiagnosis_J, lbl_67_11.Text.Trim());
                oUB04.UB04_67k_OtherDiagnosis_K.Value = formatedData(oUB04.UB04_67k_OtherDiagnosis_K, lbl_67_12.Text.Trim());
                oUB04.UB04_67l_OtherDiagnosis_L.Value = formatedData(oUB04.UB04_67l_OtherDiagnosis_L, lbl_67_13.Text.Trim());
                oUB04.UB04_67m_OtherDiagnosis_M.Value = formatedData(oUB04.UB04_67m_OtherDiagnosis_M, lbl_67_14.Text.Trim());
                oUB04.UB04_67n_OtherDiagnosis_N.Value = formatedData(oUB04.UB04_67n_OtherDiagnosis_N, lbl_67_15.Text.Trim());
                oUB04.UB04_67o_OtherDiagnosis_O.Value = formatedData(oUB04.UB04_67o_OtherDiagnosis_O, lbl_67_16.Text.Trim());
                oUB04.UB04_67p_OtherDiagnosis_P.Value = formatedData(oUB04.UB04_67p_OtherDiagnosis_P, lbl_67_17.Text.Trim());
                oUB04.UB04_67q_OtherDiagnosis_Q.Value = formatedData(oUB04.UB04_67q_OtherDiagnosis_Q, lbl_67_18.Text.Trim());
                oUB04.UB04_68_Reserved_68A.Value = "";
                oUB04.UB04_68_Reserved_68B.Value = "";
                oUB04.UB04_69_AdmittingDiagnosisCode.Value = formatedData(oUB04.UB04_69_AdmittingDiagnosisCode, lbl_69.Text.Trim());
                oUB04.UB04_70a_PatientVisitReason_A.Value = "";
                oUB04.UB04_70b_PatientVisitReason_B.Value = "";
                oUB04.UB04_70c_PatientVisitReason_C.Value = "";
                oUB04.UB04_71_PPSCode.Value = "";
                oUB04.UB04_72a_ExternalCauseofInjuryCode_A.Value = "";
                oUB04.UB04_72b_ExternalCauseofInjuryCode_B.Value = "";
                oUB04.UB04_72c_ExternalCauseofInjuryCode_C.Value = "";
                oUB04.UB04_73_ReservedFL73.Value = "";
                oUB04.UB04_74_ProcedureCode_Principal.Value = "";
                oUB04.UB04_74_ProcedureDate_Principal.Value = "";
                oUB04.UB04_74a_ProcedureCode_OtherA.Value = "";
                oUB04.UB04_74a_ProcedureDate_OtherA.Value = "";
                oUB04.UB04_74b_ProcedureCode_OtherB.Value = "";
                oUB04.UB04_74b_ProcedureDate_OtherB.Value = "";
                oUB04.UB04_74c_ProcedureCode_OtherC.Value = "";
                oUB04.UB04_74c_ProcedureDate_OtherC.Value = "";
                oUB04.UB04_74d_ProcedureCode_OtherD.Value = "";
                oUB04.UB04_74d_ProcedureDate_OtherD.Value = "";
                oUB04.UB04_74e_ProcedureCode_OtherE.Value = "";
                oUB04.UB04_74e_ProcedureDate_OtherE.Value = "";
                oUB04.UB04_75a_ReservedFL75A.Value = "";
                oUB04.UB04_75b_ReservedFL75B.Value = "";
                oUB04.UB04_75c_ReservedFL75C.Value = "";
                oUB04.UB04_75d_ReservedFL75D.Value = "";
                //Attending provider and identifiers,Operating provider and identifiers,Other provider name and identifiers.,other provider name and identifiers.

                oUB04.UB04_76_AttendingNPI.Value = formatedData(oUB04.UB04_76_AttendingNPI, lbl_76_NPI.Text.Trim());
                oUB04.UB04_76_AttendingQUAL.Value = formatedData(oUB04.UB04_76_AttendingQUAL, lbl_76_QualID.Text.Trim());
                oUB04.UB04_76_AttendingID.Value = formatedData(oUB04.UB04_76_AttendingID, lbl_76_QualValue.Text.Trim());
                oUB04.UB04_76a_AttendingLast.Value = formatedData(oUB04.UB04_76a_AttendingLast, lbl_76_Last.Text.Trim());
                oUB04.UB04_76b_AttendingFirst.Value = formatedData(oUB04.UB04_76b_AttendingFirst, lbl_76_First.Text.Trim());
                oUB04.UB04_77_OperatingNPI.Value = formatedData(oUB04.UB04_77_OperatingNPI, lbl_77_NPI.Text.Trim());
                oUB04.UB04_77_OperatingQUAL.Value = formatedData(oUB04.UB04_77_OperatingQUAL, lbl_77_Qual1.Text.Trim());
                oUB04.UB04_77_OperatingID.Value = formatedData(oUB04.UB04_77_OperatingID, lbl_77_Qual2.Text.Trim());
                oUB04.UB04_77a_OperatingLast.Value = formatedData(oUB04.UB04_77a_OperatingLast, lbl_77_Last.Text.Trim());
                oUB04.UB04_77b_OperatingFirst.Value = formatedData(oUB04.UB04_77b_OperatingFirst, lbl_77_First.Text.Trim());

                oUB04.UB04_78_OtherNPI.Value = formatedData(oUB04.UB04_78_OtherNPI, lbl_78_NPI.Text.Trim());
                oUB04.UB04_78_OtherProvider_QUAL.Value = formatedData(oUB04.UB04_78_OtherProvider_QUAL, lbl_78_NPIQualifier.Text.Trim());
                oUB04.UB04_78_OtherQUAL.Value = formatedData(oUB04.UB04_78_OtherQUAL, lbl_78_OtherQual.Text.Trim());
                oUB04.UB04_78_OtherID.Value = formatedData(oUB04.UB04_78_OtherID, lbl_78_OtherID.Text.Trim());
                oUB04.UB04_78_OtherLast.Value = formatedData(oUB04.UB04_78_OtherLast, lbl_78_Last.Text.Trim());
                oUB04.UB04_78_OtherFirst.Value = formatedData(oUB04.UB04_78_OtherFirst, lbl_78_First.Text.Trim());

                oUB04.UB04_79_OtherNPI.Value = formatedData(oUB04.UB04_79_OtherNPI, lbl_79_NPI.Text.Trim());
                oUB04.UB04_79_OtherProvider_QUAL.Value = formatedData(oUB04.UB04_79_OtherProvider_QUAL, lbl_79_NPIQualifier.Text.Trim());
                oUB04.UB04_79_OtherQUAL.Value = formatedData(oUB04.UB04_79_OtherQUAL, lbl_79_OtherQual.Text.Trim());
                oUB04.UB04_79_OtherID.Value = formatedData(oUB04.UB04_79_OtherID, lbl_79_OtherID.Text.Trim());
                oUB04.UB04_79_OtherLast.Value = formatedData(oUB04.UB04_79_OtherLast, lbl_79_Last.Text.Trim());
                oUB04.UB04_79_OtherFirst.Value = formatedData(oUB04.UB04_79_OtherFirst, lbl_79_First.Text.Trim());

                oUB04.PayerCodeA_Primary.Value = "";
                oUB04.PayerCodeB_Secondary.Value = "";
                oUB04.PayerCodeC_Tertiary.Value = "";
                //Remark
                string _split = lbl_80.Text.ToString();
                if (_split.Length > 40)
                    _split = _split.Insert(38, "\n");
                if (_split.Length > 80)
                    _split = _split.Insert(77, "\n");
                if (_split.Length > 120)
                    _split = _split.Insert(117, "\n");
                if (_split.Length > 160)
                    _split = _split.Insert(153, "\n");
                string[] temp = _split.Split('\n');
                for (int i = 0; i < temp.Length; i++)
                {
                    if (i == 0)
                    {
                        oUB04.UB04_80a_Remarks_1.Value = formatedData(oUB04.UB04_80a_Remarks_1, temp[i].ToString().Trim());
                    }
                    else if (i == 1)
                    {
                        oUB04.UB04_80b_Remarks_2.Value = formatedData(oUB04.UB04_80b_Remarks_2, temp[i].ToString().Trim());
                    }
                    else if (i == 2)
                    {
                        oUB04.UB04_80c_Remarks_3.Value = formatedData(oUB04.UB04_80c_Remarks_3, temp[i].ToString().Trim());
                    }
                    else if (i == 3)
                    {
                        // oUB04.UB04_80d_Remarks_4.Value = temp[i].ToString().Trim());
                    }
                }

                oUB04.UB04_81a_Code_Code_QUAL_A.Value = formatedData(oUB04.UB04_81a_Code_Code_QUAL_A, lbl_81_a1.Text.Trim());//"";
                oUB04.UB04_81a_Code_Code_CODE_A.Value = formatedData(oUB04.UB04_81a_Code_Code_CODE_A, lbl_81_a2.Text.Trim());//"";
                oUB04.UB04_81a_Code_Code_VALUE_A.Value ="";
                oUB04.UB04_81b_Code_Code_QUAL_B.Value = formatedData(oUB04.UB04_81b_Code_Code_QUAL_B, lbl_81_b1.Text.Trim());//"";
                oUB04.UB04_81b_Code_Code_CODE_B.Value = formatedData(oUB04.UB04_81b_Code_Code_CODE_B, lbl_81_b2.Text.Trim());//"";
                oUB04.UB04_81b_Code_Code_VALUE_B.Value = "";
                oUB04.UB04_81c_Code_Code_QUAL_C.Value = formatedData(oUB04.UB04_81a_Code_Code_QUAL_A, lbl_81_c1.Text.Trim());//"";
                oUB04.UB04_81c_Code_Code_CODE_C.Value = formatedData(oUB04.UB04_81a_Code_Code_QUAL_A, lbl_81_c2.Text.Trim());//"";
                oUB04.UB04_81c_Code_Code_VALUE_C.Value = "";
                oUB04.UB04_81d_Code_Code_QUAL_D.Value = formatedData(oUB04.UB04_81a_Code_Code_QUAL_A, lbl_81_d1.Text.Trim());//"";
                oUB04.UB04_81d_Code_Code_CODE_D.Value = formatedData(oUB04.UB04_81a_Code_Code_QUAL_A, lbl_81_d2.Text.Trim());//"";
                oUB04.UB04_81d_Code_Code_VALUE_D.Value = "";

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return oUB04;
        }

        private string  GetControlValue(String ControlName)
        {
            try
            {


                Control[] cnt = this.Controls.Find(ControlName, true);
                if (cnt.GetUpperBound(0) >= 0)
                {
                    Label lbl = (Label)cnt[0];
                    return Convert.ToString(lbl.Text).Trim();
                }
                return "";
            }
            catch (Exception)
            { 
                return "";
            }
        }

        public DataTable GetClaimDiagnosis(Int64 TransactionId, Int64 MstTransactionId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtDx = new DataTable();
            try
            {
                oDB.Connect(false);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nMasterTransactionid", MstTransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionid", TransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_Select_Transaction_DX", oDBParameters, out dtDx);
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
            return dtDx;
        }

        private void printdoc_UB04_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int _width = e.PageSettings.Bounds.Width - 30;
            int _height = e.PageSettings.Bounds.Height;
            int _X = e.PageSettings.Bounds.X;
            int _Y = e.PageSettings.Bounds.Y;

            e.Graphics.DrawImage(System.Drawing.Image.FromFile(_OutputFilePath), _X, _Y, _width, _height);
        }
        Rectangle _Bounds;
        bool toCreateEMF = gloGlobal.gloTSPrint.UseEMFForClaims && gloGlobal.gloTSPrint.isCopyPrint;
        private int CreateEMFForUB04Form(Graphics thisGraphics, float bmpWidth, float bmpHeight)
        {
            try
            {
                thisGraphics.Clear(Color.White);
                FitImageToScaleAndKeepAtCenter(thisGraphics);

                return 0;
            }
            catch
            {
                return 1;
            }

        }
        float _DpiX = 300f;
        float _DpiY = 300f;
        private string printdoc_UB04_Conversion(clsPrintDocumentConversion oConversion,Boolean isForPrint = false)
        {
            float PaperWidth = oConversion.PaperWidth;
            float PaperHeight = oConversion.PaperHeight;
            _DpiX = oConversion.DpiX;
            _DpiY = oConversion.DpiY;
            if (!isForPrint)
            {
                toCreateEMF = false;
            }
            _Bounds = new Rectangle(oConversion.BoundX, oConversion.BoundY, oConversion.BoundWidth, oConversion.BoundHeight);
            //Rectangle MarginBounds = new Rectangle(oConversion.MarginBoundsX, oConversion.MarginBoundsY, oConversion.MarginBoundsWidth, oConversion.MarginBoundsHeight);


            bool bAnyError = false;
            int bmpWidht = 0;
            int bmpHeight = 0;

            bmpWidht = Convert.ToInt32(PaperWidth * _DpiX);
            bmpHeight = Convert.ToInt32(PaperHeight * _DpiY);

            try
            {
                using (System.Drawing.Bitmap NewBitmap = new Bitmap(bmpWidht, bmpHeight))
                {
                    byte[] emfBytes=null;
                    try
                    {
                        NewBitmap.SetResolution(_DpiX, _DpiY);
                        if (toCreateEMF)
                        {
                            emfBytes = gloGlobal.CreateEMF.GetEMFBytes(PaperWidth, PaperHeight, bmpWidht, bmpHeight, CreateEMFForUB04Form);
                        }
                        else
                        {
                            using (System.Drawing.Graphics eGraphics = Graphics.FromImage(NewBitmap))
                            {
                                FitImageToScaleAndKeepAtCenter(eGraphics);
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
                                    FitImageToScaleAndKeepAtCenter(eGraphics);
                                }
                            }
                            catch (Exception ex)
                            {
                                bAnyError = true;
                                gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during scaling tiff file at printdoc_UB04_Conversion method", false);
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
                            _printFilePath = sPath + "\\" + DateTime.Now.ToString("yyyyMMddhhmmsstt") + "f_BLANK" + (toCreateEMF ? ".emf" : ".tif");

                            if (File.Exists(_printFilePath) == true) { File.Delete(_printFilePath); }

                            if (toCreateEMF)
                            {
                                File.WriteAllBytes(_printFilePath, emfBytes);
                            }
                            else
                            {
                                NewBitmap.Save(_printFilePath,  System.Drawing.Imaging.ImageFormat.Tiff);
                            }
                            return _printFilePath;

                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog("Error occured during new bitmap save at printdoc_UB04_Conversion method", false);
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                            return _OutputFilePath;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            return "";
        }

        private void FitImageToScaleAndKeepAtCenter(System.Drawing.Graphics eGraphics)
        {
            int _width = _Bounds.Width - Convert.ToInt32(0.3f * _DpiX);
            int _height = _Bounds.Height;
            int _X = _Bounds.X + Convert.ToInt32(0.05f * _DpiX);
            int _Y = _Bounds.Y + Convert.ToInt32(0.05f * _DpiY);

            using (System.Drawing.Image thisImage = System.Drawing.Image.FromFile(_OutputFilePath))
            {
                eGraphics.DrawImage(thisImage, _X, _Y, _width, _height);
            }
            return;
        }

        private void FillOtherDetails(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";         
            try
            {
                oDB.Connect(false);

                _ContactID = ContactID;
                //Get Box 32 and Box 33 Settings from Contacts Insurance Details
                DataTable dt = new DataTable();
                _sqlQuery = "select " +
                            "ISNULL(sBox33,'') as sBox33,ISNULL(sBox33A,'') as sBox33A,ISNULL(sBox33B,'') as sBox33B, " +
                            "isnull(sInsuranceTypeCode,'') as sInsuranceTypeCode," +
                             " isnull(bAccessAssignment,0) as bAccessAssignment, " +
                             " isnull(bIncludePrimaryDxInBox69,0) as bIncludePrimaryDxInBox69" +
                             " from Contacts_Insurance_DTL where nContactID = " + _ContactID;
                oDB.Retrive_Query(_sqlQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    

                    ////BOX 33
                    //if (Convert.ToString(dt.Rows[0]["sBox33"]) == "Provider Address" || Convert.ToString(dt.Rows[0]["sBox33"]) == "")
                    //    _BillingAddressType = AddressType.ProviderAddress;
                    //else if (Convert.ToString(dt.Rows[0]["sBox33"]) == "Facility Address")
                    //    _BillingAddressType = AddressType.FacilityAddress;
                    //else if (Convert.ToString(dt.Rows[0]["sBox33"]) == "Clinic Address")
                    //    _BillingAddressType = AddressType.ClinicAddress;
                    ////BOX 33A
                    //if (Convert.ToString(dt.Rows[0]["sBox33A"]) == "Billing Provider NPI" || Convert.ToString(dt.Rows[0]["sBox33A"]) == "")
                    //    _Billing_A_NPI = NPIType.BillingProviderNPI;
                    //else if (Convert.ToString(dt.Rows[0]["sBox33A"]) == "Facility NPI")
                    //    _Billing_A_NPI = NPIType.FacilityNPI;
                    //else if (Convert.ToString(dt.Rows[0]["sBox33A"]) == "Clinic NPI")
                    //    _Billing_A_NPI = NPIType.ClinicNPI;
                    ////BOX 33B
                    //if (Convert.ToString(dt.Rows[0]["sBox33B"]) == "Billing Provider NPI" || Convert.ToString(dt.Rows[0]["sBox33B"]) == "")
                    //    _Billing_B_NPI = NPIType.BillingProviderNPI;
                    //else if (Convert.ToString(dt.Rows[0]["sBox33B"]) == "Facility NPI")
                    //    _Billing_B_NPI = NPIType.FacilityNPI;
                    //else if (Convert.ToString(dt.Rows[0]["sBox33B"]) == "Clinic NPI")
                    //    _Billing_B_NPI = NPIType.ClinicNPI;
                  
                    _IsAcceptAssignment = Convert.ToBoolean(dt.Rows[0]["bAccessAssignment"]);
                }
                dt = null;             
            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }
        
        private string GetPatientSetting(Int64 _PatientID, string _SettingName)
        {
            string _result = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = new DataTable();
            string _sqlQuery = "";
            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT ISNULL(sValue,'') as sValue FROM PatientSettings  " +
                                        " WHERE nPatientID=" + _PatientID + " and sName='" + _SettingName + "'";
                oDB.Retrive_Query(_sqlQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    _result = Convert.ToString(dt.Rows[0]["sValue"]);
                }
            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                dt = null;
                oDB.Dispose();
            }
            return _result;
        }
      

        public void ClearForm(Panel oPanel)
        {
            if (oPanel == null)
            {
                oPanel = panel1;
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
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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


                UpdateCmd.Parameters["@nClaimno"].Value = oUB04Transaction.Transaction.ClaimNo;
                UpdateCmd.Parameters["@sSubClaimno"].Value = oUB04Transaction.Transaction.SubClaimNo;
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
        private void getBox69settings(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            try
            {
                oDB.Connect(false);

                _ContactID = ContactID;
                //Get Box 32 and Box 33 Settings from Contacts Insurance Details
                DataTable dt = new DataTable();
                _sqlQuery = "select " +
                            " isnull(bIncludePrimaryDxInBox69,0) as bIncludePrimaryDxInBox69" +
                            " from Contacts_Insurance_DTL where nContactID = " + _ContactID;
                oDB.Retrive_Query(_sqlQuery, out dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    _IsIncludePrimaryDxInBox69 = Convert.ToBoolean(dt.Rows[0]["bIncludePrimaryDxInBox69"]);
                }
                dt = null;
            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }
    }
}

