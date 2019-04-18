using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Edidev.FrameworkEDI;
using gloBilling.Common;
using System.Collections;
using gloAppointmentBook.Books;
using System.Runtime.CompilerServices;



namespace gloBilling
{
    
    public partial class frmEDI276Generation : Form
    {
        
        #region Constructor

        public frmEDI276Generation(string DataBaseConnectionString, Int64 PatientID)
        {
            InitializeComponent();
            _databaseConnectionString = DataBaseConnectionString;
            _PatientId = PatientID;

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicId = 0; }
            }
            else
            { _ClinicId = 0; }

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
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
        }

        #endregion Constructor

        #region " Variables "

        private string _databaseConnectionString = "";
        private Int64 _PatientId = 0;
        private Int64 _ClinicId = 0;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private ArrayList _arrSelectedTransactions = null;
      //  private bool bSecondaryInsurance = false;
        private Transaction _Transaction = null;
        public ArrayList SelectedTransactions
        {
            get { return _arrSelectedTransactions; }
            set { _arrSelectedTransactions = value; }
        }

        ediDocument oEdiDoc = null;
        ediInterchange oInterchange = null;
        ediGroup oGroup = null;
        ediTransactionSet oTransactionset = null;
        ediDataSegment oSegment = null;
        ediSchema oSchema = null;
        ediSchemas oSchemas = null;
       // ediWarnings oWarnings = null;
       // ediWarning oWarning = null;

        string sPath = "";
        string sSEFFile = "";
        string sEdiFile = "";
     //   string sEntity = "";
     //   string sInstance = "";

        //Referral Provider  //Anil 20081108
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
    //    private string _ReceiverName = "";
    //    private string _ReceiverETIN = "";

        //Subscriber
        private string _SubscriberLName = "";
    //    private string _SubscriberInsurancePST = "";
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

        //Payer
        private string _PayerName = "";
        private string _PayerID = "";
        private string _PayerAddress = "";
        private string _PayerCity = "";
        private string _PayerState = "";
        private string _PayerZip = "";

        private string _PatientAccountNo = "";


        //Facility
     //   private string _FacilityCode = "";
        private string _FacilityName = "";
        private string _FacilityAddress = "";
        private string _FacilityCity = "";
        private string _FacilityState = "";
        private string _FacilityZip = "";
        private string _FacilityNPI = "";


        //Other Insurance
        private string _OtherInsuranceSubscriberLName = "";
   //     private string _OtherInsurancePST = "";
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

        //ISA and GS Settings
        private string _SenderID = "";
        private string _ReceiverID = "";
        private string _SenderCode = "";
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


        //Prior Authorization Number
        private string _PriorAuthorizationNo = "";


        #endregion " Variables "
       
        #region Private and Public Methods

        private void FillCombos()
        {
            try
            {
                cmbAmountQualifierCode.SelectedIndex = 0;
                cmbBillTypeIDQualifier.SelectedIndex = 0;
                cmbDateQualifierCode.SelectedIndex = 0;
                cmbInfoReceiverEntityQualifier.SelectedIndex = 0;
                cmbInfoSourceEntityQualifierCode.SelectedIndex = 0;
                cmbInfoSourceIDCodeQualifier.SelectedIndex = 0;
                cmbInfoSourCommQualCode.SelectedIndex = 0;
                cmbInfoSourCommQualCode2.SelectedIndex = 0;
                cmbMedicalIDQualifier.SelectedIndex = 0;
                cmbProvEntityQualCode.SelectedIndex = 0;
                cmbProvEntityQualCode.SelectedIndex = 0;
                cmbProvIdentificationCode.SelectedIndex = 0;
                cmbRecIDCodeQualifier.SelectedIndex = 0;
                cmbReferenceIDQualifier.SelectedIndex = 0;
                cmbServiceIDQualifierCode.SelectedIndex = 0;
                cmbServiceLineDateQualifier.SelectedIndex = 0;
                cmbServiceLineItemQualifier.SelectedIndex = 0;
                cmbSubscriberEntityQualifierCode.SelectedIndex = 0;
                cmbSubscriberIDQualifier.SelectedIndex = 0;
                cmbTypeOfTransaction.SelectedIndex = 0;
                cmbInfoSourceContactQual.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void FillProviderDetails(long _SelectedProviderId, ProviderType _ProviderType)
        {
            Resource oResource = new Resource(_databaseConnectionString);
            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseConnectionString);
         //   DataTable dtProviderDetails = null;
            Provider _Provider = null;
            Object _objResult = null;
            string strBillingSetting = "";
            string strRenderingSetting = "";
            try
            {

                _Provider = oResource.GetProviderDetail(_SelectedProviderId);

                if (_Provider != null)
                {
                    switch (_ProviderType)
                    {
                        case ProviderType.BillingProvider:
                            {
                                oSettings.GetSetting("BillingSetting", _SelectedProviderId, _ClinicId, out _objResult);
                                if (_objResult != null)
                                {
                                    // |Company|Practice|Business"
                                    strBillingSetting = Convert.ToString(_objResult);
                                }

                                _BillingFName = _Provider.FirstName;
                                _BillingLName = _Provider.LastName;
                                _BillingMName = _Provider.MiddleName;
                                _BillingNPI = _Provider.NPI;
                                _BillingStateMedicalNo = _Provider.StateMedicalNo;
                                _BillingSSN = _Provider.SSN;
                                _BillingEmployerID = _Provider.EmployerID;
                                _BillingTaxonomy = _Provider.Taxonomy;

                                switch (strBillingSetting)
                                {
                                    case "Business":
                                        {
                                            _BillingAddress = _Provider.BMAddress1;
                                            _BillingCity = _Provider.BMCity;
                                            _BillingState = _Provider.BMState;
                                            _BillingZIP = _Provider.BMZIP;
                                        } break;
                                    case "Practice":
                                        {
                                            _BillingAddress = _Provider.BPracAddress1;
                                            _BillingCity = _Provider.BPracCity;
                                            _BillingState = _Provider.BPracState;
                                            _BillingZIP = _Provider.BPracZIP;
                                        } break;
                                    case "Company":
                                        {
                                            _BillingAddress = _Provider.CompanyAddress1;
                                            _BillingCity = _Provider.CompanyCity;
                                            _BillingState = _Provider.CompanyState;
                                            _BillingZIP = _Provider.CompanyZip;
                                        } break;
                                    default:
                                        _BillingAddress = _Provider.BMAddress1;
                                        _BillingCity = _Provider.BMCity;
                                        _BillingState = _Provider.BMState;
                                        _BillingZIP = _Provider.BMZIP;
                                        break;
                                }
                            }
                            break;
                        case ProviderType.PayToProvider:
                            {
                                //txtPTPAddress.Text = _Provider.BMAddress1;
                                //txtPTPCity.Text = _Provider.BMCity;
                                //txtPTPState.Text = _Provider.BMState;
                                //txtPTPZip.Text = _Provider.BMZIP;
                                //txtPTPNPI_ID.Text = _Provider.NPI;
                                //txtPTPUPIN.Text = _Provider.UPIN;
                            }
                            break;
                        case ProviderType.RefferingProvider:
                            {
                                _ReferralFName = _Provider.FirstName;
                                _ReferralAddress = _Provider.BMAddress1;
                                _ReferralLName = _Provider.LastName;
                                _ReferralMName = _Provider.MiddleName;
                                _ReferralCity = _Provider.BMCity;
                                _ReferralState = _Provider.BMState;
                                _ReferralZIP = _Provider.BMZIP;
                                _ReferralNPI = _Provider.NPI;
                                _ReferralStateMedicalNo = _Provider.StateMedicalNo;
                                _ReferralSSN = _Provider.SSN;
                                _ReferralEmployerID = _Provider.EmployerID;
                                _ReferralTaxonomy = _Provider.Taxonomy;

                            }
                            break;
                        case ProviderType.RenderingProvider:
                            {
                                oSettings.GetSetting("RenderingSetting", _SelectedProviderId, _ClinicId, out _objResult);
                                if (_objResult != null)
                                {
                                    // |Company|Practice|Business"
                                    strRenderingSetting = Convert.ToString(_objResult);
                                }

                                _RenderingFName = _Provider.FirstName;
                                _RenderingLName = _Provider.LastName;
                                _RenderingMName = _Provider.MiddleName;
                                _RenderingNPI = _Provider.NPI;
                                _RenderingStateMedicalNo = _Provider.StateMedicalNo;
                                _RenderingSSN = _Provider.SSN;
                                _RenderingEmployerID = _Provider.EmployerID;
                                _RenderingTaxonomy = _Provider.Taxonomy;

                                switch (strRenderingSetting)
                                {
                                    case "Business":
                                        {
                                            _RenderingAddress = _Provider.BMAddress1;
                                            _RenderingCity = _Provider.BMCity;
                                            _RenderingState = _Provider.BMState;
                                            _RenderingZIP = _Provider.BMZIP;

                                        } break;
                                    case "Practice":
                                        {
                                            _RenderingAddress = _Provider.BPracAddress1;
                                            _RenderingCity = _Provider.BPracCity;
                                            _RenderingState = _Provider.BPracState;
                                            _RenderingZIP = _Provider.BPracZIP;
                                        } break;
                                    case "Company":
                                        {
                                            _RenderingAddress = _Provider.CompanyAddress1;
                                            _RenderingCity = _Provider.CompanyCity;
                                            _RenderingState = _Provider.CompanyState;
                                            _RenderingZIP = _Provider.CompanyZip;
                                        } break;
                                    default:
                                        _RenderingAddress = _Provider.BMAddress1;
                                        _RenderingCity = _Provider.BMCity;
                                        _RenderingState = _Provider.BMState;
                                        _RenderingZIP = _Provider.BMZIP;
                                        break;
                                }

                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (_Provider != null) { _Provider.Dispose(); }
                if (oResource != null) { oResource.Dispose(); }
                if (oSettings != null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }
            }
        }

        private void GetTransaction()
        {
            gloBilling ogloBilling = new gloBilling(_databaseConnectionString, "");
            Common.Transaction oTransaction = null;
            try
            {
                if (SelectedTransactions != null)
                {
                    if (SelectedTransactions.Count > 0)
                    {
                        for (int i = 0; i < SelectedTransactions.Count; i++)
                        {
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicId);
                            if (oTransaction != null)
                            {
                                
                                if (oTransaction.Lines != null)
                                {
                                    if (oTransaction.Lines.Count > 0)
                                    {

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected Transaction have no transaction lines", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Select Transaction", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _Transaction = oTransaction;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oTransaction != null)
                {
                    oTransaction.Dispose();
                }
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }
        }

        private void FillSubmitterInfo(Int64 _SelectedClinicId, Int64 _nProviderID)
        {
            gloBilling oBill = new gloBilling(_databaseConnectionString, "");
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
                    if (Convert.ToString(dt.Rows[0]["SubmitterContactName"]) == "")
                    {
                        _SubmitterContactPersonName = Convert.ToString(dt.Rows[0]["SubmitterName"]);
                    }
                    else
                    {
                        _SubmitterContactPersonName = Convert.ToString(dt.Rows[0]["SubmitterContactName"]);
                    }
                    _SubmitterContactPersonNo = Convert.ToString(dt.Rows[0]["SubmitterPhone"]);
                    _SubmitterETIN = "C0923";

                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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

        private void FillFacilityInfo(string FacilityCode, Int64 _nProviderID)
        {
            gloBilling oBill = new gloBilling(_databaseConnectionString, "");
            DataTable dt = null;
            //DataTable dtFacility = new DataTable();
            try
            {
                if (FacilityCode != null && FacilityCode != "")
                {
                    dt = oBill.GetFacilityInfo(FacilityCode, _nProviderID);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    //_FacilityCode = dt.Rows[0]["sFacilityCode"].ToString();
                    _FacilityAddress = dt.Rows[0]["FacilityAddress1"].ToString();
                    _FacilityCity = dt.Rows[0]["FacilityCity"].ToString();
                    _FacilityName = dt.Rows[0]["FacilityName"].ToString();
                    _FacilityState = dt.Rows[0]["FacilityState"].ToString();
                    _FacilityZip = dt.Rows[0]["FacilityZip"].ToString();
                    _FacilityNPI = dt.Rows[0]["FacilityNPI"].ToString();
                }

                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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

        private void FillClearingHouseInfo()
        {
            gloBilling oBill = new gloBilling(_databaseConnectionString, "");
            DataTable dt = null;
            try
            {
                dt = oBill.GetClearingHouseSettings();
                if (dt != null && dt.Rows.Count > 0)
                {
                    _SenderID = Convert.ToString(dt.Rows[0]["sSubmitterID"]);
                    _ReceiverID = Convert.ToString(dt.Rows[0]["sReceiverID"]);
                    _SenderCode = Convert.ToString(dt.Rows[0]["sSenderCode"]);
                    _ReceiverCode = Convert.ToString(dt.Rows[0]["sVenderIDCode"]);

                }

                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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

        private string FormattedTime(string TimeFormat)
        {
            int _length = 0;
            _length = TimeFormat.Length;
            if (_length == 0)
            {
                TimeFormat = "0000";
            }
            if (_length == 1)
            {
                TimeFormat = "000" + TimeFormat;
            }
            else if (_length == 2)
            {
                TimeFormat = "00" + TimeFormat;
            }
            else if (_length == 3)
            {
                TimeFormat = "0" + TimeFormat;
            }
            else if (_length == 4)
            {
        //        TimeFormat = TimeFormat;
            }
            return TimeFormat;
        }

        private string GetPriorAuthorizationNumber(Int64 PatientID, Int64 InsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            string _strSQL = "";
            Object _result = null;
            string _PriorAuthorizationNo = "";
            try
            {
                _strSQL = "SELECT sAuthorizationNumber FROM PatientPriorAuthorization WHERE nPatientID=" + PatientID + "  AND nInsuranceID=" + InsuranceID + " ";
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(_strSQL);
                if (_result != null)
                {
                    _PriorAuthorizationNo = Convert.ToString(_result);
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
            }
            return _PriorAuthorizationNo;
        }

        public void FillInsurances(Int64 PatientID)
        {
            DataTable dtPatientInsurances = null;
           // gloBilling ogloBilling = new gloBilling(_databaseConnectionString, "");
            try
            {
                //bSecondaryInsurance = false;
                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseConnectionString);
                dtPatientInsurances = ogloPatient.getPatientInsurances(PatientID);
                ogloPatient.Dispose();
                ogloPatient = null;

                if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPatientInsurances.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            //Primary Insurance
                            _PayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]);
                            _SubscriberAddress = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr1"]);
                            _SubscriberCity = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCity"]);
                            _SubscriberState = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberState"]);
                            _SubscriberZIP = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberZip"]);
                            _SubscriberRelationshipCode = Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]);
                            _SubscriberMName = Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]);
                            _SubscriberLName = Convert.ToString(dtPatientInsurances.Rows[0]["SubLName"]);
                            _SubscriberFName = Convert.ToString(dtPatientInsurances.Rows[0]["SubFName"]);
                            _SubscriberDOB = Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]);
                            _SubscriberGender = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]);
                            _SubscriberInsuranceBelongs = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]);//"CI"; 
                            _SubscriberInsuranceID = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]);
                            //_SubscriberInsurancePST = "P";//Convert.ToString(dtPatientInsurances.Rows[0][""]);
                            _SubscriberGroupID = Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"]);
                            _PayerID = Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]);
                            _PayerAddress = Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress1"]); ;
                            _PayerCity = Convert.ToString(dtPatientInsurances.Rows[0]["PayerCity"]); ;
                            _PayerState = Convert.ToString(dtPatientInsurances.Rows[0]["PayerState"]); ;
                            _PayerZip = Convert.ToString(dtPatientInsurances.Rows[0]["PayerZip"]); ;

                            //Anil Added on 20081030
                            _PriorAuthorizationNo = GetPriorAuthorizationNumber(PatientID, Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"]));

                        }
                        else if (i == 1)
                        {
                            //Secondary Insurance
                            //bSecondaryInsurance = true;
                            _OtherInsuranceAddress = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberAddr1"]);
                            _OtherInsuranceName = Convert.ToString(dtPatientInsurances.Rows[i]["InsuranceName"]);
                            _OtherInsuranceSubscriberFName = Convert.ToString(dtPatientInsurances.Rows[i]["SubFName"]);
                            _OtherInsuranceSubscriberLName = Convert.ToString(dtPatientInsurances.Rows[i]["SubLName"]);
                            _OtherInsuranceSubscriberMName = Convert.ToString(dtPatientInsurances.Rows[i]["SubMName"]);
                            _OtherInsuranceSubscriberGender = Convert.ToString(dtPatientInsurances.Rows[i]["sSubscriberGender"]);
                            _OtherInsuranceSubscriberDOB = Convert.ToString(dtPatientInsurances.Rows[i]["dtDOB"]);
                            _OtherInsuranceZIP = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberZip"]);
                            _OtherInsuranceType = Convert.ToString(dtPatientInsurances.Rows[i]["InsuranceTypeCode"]);//"CI"
                            _OtherInsuranceState = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberState"]);
                            _OtherInsuranceRelationshipCode = Convert.ToString(dtPatientInsurances.Rows[i]["RelationshipCode"]);
                            //_OtherInsurancePST = "S"; //Convert.ToString(dtPatientInsurances.Rows[i][""]);
                            _OtherInsurancePayerID = Convert.ToString(dtPatientInsurances.Rows[i]["PayerID"]);
                            _OtherInsuranceID = Convert.ToString(dtPatientInsurances.Rows[i]["sSubscriberID"]);
                            _OtherInsuranceGroupID = Convert.ToString(dtPatientInsurances.Rows[i]["sGroup"]);
                            _OtherInsuranceCity = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberCity"]);
                        }
                    }
                }
                if (dtPatientInsurances != null)
                {
                    dtPatientInsurances.Dispose();
                    dtPatientInsurances = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void FillPatientInformation(Int64 PatientID)
        {
           // gloBilling oBill = new gloBilling(_databaseConnectionString, "");
          
            //DataTable dt = new DataTable();
            //DataTable dtClinic = new DataTable();
            gloPatient.Patient oPatient = null;
        //    gloPatient.Referrals oReferral = new gloPatient.Referrals();
            try
            {
                //oPatient = ogloPatient.GetPatientDemo(PatientID);
                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseConnectionString);
                oPatient = ogloPatient.GetPatient(PatientID);
                ogloPatient.Dispose();
                ogloPatient = null;
                if (oPatient != null)
                {
                    _PatientAccountNo = oPatient.DemographicsDetail.PatientCode;
                    //Added on 20081030 by Anil
                    _PatientAddress = oPatient.DemographicsDetail.PatientAddress1;
                    _PatientCity = oPatient.DemographicsDetail.PatientCity;
                    _PatientCode = oPatient.DemographicsDetail.PatientCode;
                    _PatientDOB = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oPatient.DemographicsDetail.PatientDOB.ToShortDateString()));
                    _PatientFirstName = oPatient.DemographicsDetail.PatientFirstName;
                    _PatientGender = oPatient.DemographicsDetail.PatientGender;
                    _PatientLastName = oPatient.DemographicsDetail.PatientLastName;
                    _PatientMiddleName = oPatient.DemographicsDetail.PatientMiddleName;
                    _PatientSSN = oPatient.DemographicsDetail.PatientSSN;
                    _PatientState = oPatient.DemographicsDetail.PatientState;
                    _PatientZip = oPatient.DemographicsDetail.PatientZip;

                    gloPatient.Referrals oReferral;
                    oReferral = oPatient.Referrals;
                    if (oReferral.Count > 0)
                    {
                        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                        DataTable dtReferral = new DataTable();
                        string _sqlQuery = "";

                        oDB.Connect(false);
                        _sqlQuery = " SELECT sStreet, sCity, sState, sZIP, sFirstName, sMiddleName, sLastName, sGender, nSpecialtyID, sTaxID, sUPIN, sNPI, sContactType, sTaxonomy, sTaxonomyDesc, nContactID " +
                                    " FROM Contacts_MST  " +
                                    " WHERE (nContactID = " + oReferral[0].ReferralID + ") AND (sContactType = 'Referral')";
                        oDB.Retrive_Query(_sqlQuery, out dtReferral);
                        if (dtReferral != null && dtReferral.Rows.Count > 0)
                        {
                            _ReferralFName = dtReferral.Rows[0]["sFirstName"].ToString();
                            _ReferralLName = dtReferral.Rows[0]["sLastName"].ToString();
                            _ReferralMName = dtReferral.Rows[0]["sMiddleName"].ToString();
                            _ReferralCity = dtReferral.Rows[0]["sCity"].ToString();
                            _ReferralState = dtReferral.Rows[0]["sState"].ToString();
                            _ReferralZIP = dtReferral.Rows[0]["sZIP"].ToString();
                            _ReferralNPI = dtReferral.Rows[0]["sNPI"].ToString();
                            _ReferralEmployerID = dtReferral.Rows[0]["sTaxID"].ToString();
                            _ReferralTaxonomy = dtReferral.Rows[0]["sTaxonomy"].ToString();
                        }
                        if (dtReferral != null)
                        {
                            dtReferral.Dispose();
                            dtReferral = null;
                        }
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    oPatient.Dispose();
                    oPatient = null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillAllDetails(Transaction _Transaction)
        {
            try
            {
                if (_Transaction != null)
                {
                    if (Convert.ToInt64(_Transaction.ProviderID) != 0 && _Transaction.ProviderID.ToString() != "")
                    {
                        FillProviderDetails(Convert.ToInt64(_Transaction.ProviderID), ProviderType.BillingProvider);
                        // FillPatientInsurances(_Transaction.PatientID);
                        FillInsurances(_Transaction.PatientID);
                        FillPatientInformation(_Transaction.PatientID);
                        FillSubmitterInfo(Convert.ToInt64(_Transaction.ClinicID), Convert.ToInt64(_Transaction.ProviderID));
                        //FillFacilityInfo(_Transaction.FacilityCode, _Transaction.ProviderID);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private void LoadEDIObject()
        {
            try
            {
                sPath = AppDomain.CurrentDomain.BaseDirectory;
                sSEFFile = "276_X093A1.SEF";
                sEdiFile = "276OUTPUT.x12";
                
                oEdiDoc = new ediDocument();
                ediDocument.Set(ref oEdiDoc, new ediDocument());
              
                ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());
                oSchemas.EnableStandardReference = false;

                //oEdiDoc.SegmentTerminator = "~\r\n";
                //oEdiDoc.ElementTerminator = "*";
                //oEdiDoc.CompositeTerminator = ":";
                //oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                //oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema("276_X093A1.SEF", 0));
           

                System.IO.FileInfo ofile = new System.IO.FileInfo(sPath + sSEFFile);
                if (ofile.Exists == false)
                {
                    MessageBox.Show("SEF file is not present in the Base Directory", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
        }

        #endregion Private and Public Methods

        #region Form Load

        private void frmEDI276Generation_Load(object sender, EventArgs e)
        {
            try
            {
                LoadEDIObject();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        #endregion Form Load

        #region Button Click Events

        private void ts_btnGenerateEDI_Click(object sender, EventArgs e)
        {
            Generate276EDI();
        }

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Button Click Events

        #region Generate EDI Method

        //private void Generate276EDI()
        //{
        //    oEdiDoc = new ediDocument();

        //    Transaction oTransaction = null;
        //    int nHlCounter = 0;
        //    int nHlInfoReceiverParent;
        //    int nHlServiceProviderParent;
        //    int nHlSubscriberParent;
        //    int nHlDependentParent;
        //    try
        //    {

        //        FillClearingHouseInfo();


        //        #region Interchange Segment

        //        //Create the interchange segment
        //        ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

        //        oSegment.set_DataElementValue(1, 0, "00");
        //        oSegment.set_DataElementValue(3, 0, "00");
        //        oSegment.set_DataElementValue(5, 0, "12");
        //        oSegment.set_DataElementValue(6, 0, _SenderID);//txtSenderID1.Text.Trim());// "Sender");
        //        oSegment.set_DataElementValue(7, 0, "12");
        //        oSegment.set_DataElementValue(8, 0, _ReceiverID);//txtReceiverID1.Text.Trim());//"ReceiverID");
        //        string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
        //        oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
        //        string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
        //        oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());//txtEnquiryTime.Text.Trim());//"1548");
        //        oSegment.set_DataElementValue(11, 0, "U");
        //        oSegment.set_DataElementValue(12, 0, "00401");
        //        oSegment.set_DataElementValue(13, 0, "00020");//txtControlNo.Text.Trim());//"000000020");
        //        oSegment.set_DataElementValue(14, 0, "0");
        //        oSegment.set_DataElementValue(15, 0, "T");
        //        oSegment.set_DataElementValue(16, 0, ":");

        //        #endregion Interchange Segment

        //        #region Functional Group

        //        //Create the functional group segment
        //        ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X093"));
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
        //        oSegment.set_DataElementValue(1, 0, "HR");//txtFunctionID.Text.Trim());
        //        oSegment.set_DataElementValue(2, 0, _SenderCode.Trim());//txtSenderDept.Text.Trim());//"SenderDept");
        //        oSegment.set_DataElementValue(3, 0, _ReceiverCode.Trim());//txtReceiverDept.Text.Trim());//"ReceiverDept");
        //        string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
        //        oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//txtFunGroupDate.Text.Trim());//"20010821");
        //        oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());//txtFuncGroupTime.Text.Trim());//"1548");
        //        oSegment.set_DataElementValue(6, 0, "11000");//txtControlNo.Text.Trim());//"000001");
        //        oSegment.set_DataElementValue(7, 0, "X");
        //        oSegment.set_DataElementValue(8, 0, "004010X093");

        //        #endregion Functional Group

        //        #region Transaction Set
        //        //HEADER
        //        //ST TRANSACTION SET HEADER
        //        ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("276"));
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
        //        oSegment.set_DataElementValue(2, 0, "000231");
        //        #endregion Transaction Set

        //        #region BHT Segment

        //        //Begining of Herarchical Transaction Segment 
        //        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
        //        oSegment.set_DataElementValue(1, 0, "0010");
        //        oSegment.set_DataElementValue(2, 0, "13");//Code 13=Request
        //        oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());//txtBHTDate.Text.Trim());//"19990501");//Date

        //        #endregion BHT Segment


        //        if (SelectedTransactions != null)
        //        {
        //            if (SelectedTransactions.Count > 0)
        //            {
        //                for (int i = 0; i < SelectedTransactions.Count; i++)
        //                {
        //                    oTransaction = new Transaction();
        //                    TransactionLine oTransLine = null;

        //                    gloBilling ogloBilling = new gloBilling(_databaseConnectionString, "");

        //                    oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicId);
        //                    FillAllDetails(oTransaction);
        //                    if (oTransaction != null)
        //                    {
        //                        if (oTransaction.Lines.Count > 0)
        //                        {

        //                            #region Calculate Claim Amount

        //                            string _ClaimTotal = "";

        //                            decimal _claimAmount = 0;
        //                            for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
        //                            {
        //                                _claimAmount = _claimAmount + oTransaction.Lines[nLine].Total;
        //                            }

        //                            _ClaimTotal = _claimAmount.ToString("#0.00");

        //                            if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
        //                            {
        //                                _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
        //                            }

        //                            #endregion " Calculate Claim Amount "

        //                            #region HL Loop
        //                            //'*************************************************************************************************
        //                            //'DETAIL INFORMATION SOURCE LEVEL
        //                            //Do While nInfoSourceCounter <= nInfoSources

        //                            nHlCounter = nHlCounter + 1;
        //                            nHlInfoReceiverParent = nHlCounter;
        //                            //'HL - HIERARCHICAL LEVEL
        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
        //                            oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
        //                            oSegment.set_DataElementValue(3, 0, "20");//txtInfoSourceLevel.Text.Trim());//"20");      //Hierarchical Level Code
        //                            oSegment.set_DataElementValue(4, 0, "1");//txtInfoSourceLevel.Text.Trim());//"1");     //Hierarchical Child Code

        //                            #endregion HL Loop

        //                            #region Payer Loop

        //                            //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
        //                            oSegment.set_DataElementValue(1, 0, "PR");  //Entity Identifier Code - PAYER
        //                            oSegment.set_DataElementValue(2, 0, "2");    //Entity Type Qualifier
        //                            oSegment.set_DataElementValue(3, 0, _PayerName.Trim());//"Payer Name");     //Name Last or Organization Name
        //                            oSegment.set_DataElementValue(8, 0, "PI");    //Identification Code Qualifier
        //                            oSegment.set_DataElementValue(9, 0, _PayerID.Trim());//"12345");     //Identification Code

        //                            #endregion Payer Loop

        //                            #region HL Loop
        //                            //*************************************************************************************************
        //                            //DETAIL INFORMATION RECEIVER LEVEL
        //                            //Do While nInfoReceiverCounter <= nInfoReceivers
        //                            string _strDI = oEdiDoc.GetEdiString();
        //                            nHlCounter = nHlCounter + 1;
        //                            nHlServiceProviderParent = nHlCounter;

        //                            //HL - HIERARCHICAL LEVEL

        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
        //                            oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
        //                            oSegment.set_DataElementValue(2, 0, nHlInfoReceiverParent.ToString());      //Hierarchical Parent ID Number
        //                            oSegment.set_DataElementValue(3, 0, "21");    //Hierarchical Level Code
        //                            oSegment.set_DataElementValue(4, 0, "1");    //Hierarchical Child Code
        //                            #endregion HL Loop

        //                            #region Submitter
        //                            //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
        //                            oSegment.set_DataElementValue(1, 0, "41");     //Entity Identifier Code - SUBMITTER
        //                            oSegment.set_DataElementValue(2, 0, "2");   //Entity Type Qualifier
        //                            oSegment.set_DataElementValue(3, 0, _SubmitterName.Trim());//"Receiver Name");     //Name Last or Organization Name
        //                            oSegment.set_DataElementValue(8, 0, "46");     //Identification Code Qualifier
        //                            oSegment.set_DataElementValue(9, 0, _SubmitterETIN.Trim());//"X67E");    //Identification Code
        //                            #endregion Submitter
        //                            _strDI = oEdiDoc.GetEdiString();
        //                            #region HL Loop
        //                            //*************************************************************************************************
        //                            //DETAIL SERVICE PROVIDER LEVEL
        //                            //Do While nServiceProviderCounter <= nServiceProviders

        //                            nHlCounter = nHlCounter + 1;
        //                            nHlSubscriberParent = nHlCounter;
        //                            //HL - HIERARCHICAL LEVEL
        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
        //                            oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
        //                            oSegment.set_DataElementValue(2, 0, nHlServiceProviderParent.ToString());     //Hierarchical Parent ID Number
        //                            oSegment.set_DataElementValue(3, 0, "19");     //Hierarchical Level Code
        //                            oSegment.set_DataElementValue(4, 0, "1");   //Hierarchical Child Code
        //                            #endregion HL Loop

        //                            #region Provider/Receiver Loop
        //                            //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
        //                            oSegment.set_DataElementValue(1, 0, "1P");     //Entity Identifier Code - PROVIDER
        //                            oSegment.set_DataElementValue(2, 0, "1");    //Entity Type Qualifier
        //                            oSegment.set_DataElementValue(3, 0, _BillingFName.Trim() + " " + _BillingMName.Trim() + " " + _BillingLName);     //Name Last or Organization Name
        //                            oSegment.set_DataElementValue(8, 0, "XX");   //Identification Code Qualifier
        //                            oSegment.set_DataElementValue(9, 0, _BillingNPI.Trim());//"987666");    //Identification Code
        //                            #endregion Provider/Receiver Loop

        //                            #region HL Loop
        //                            //*************************************************************************************************
        //                            //DETAIL SUBSCRIBER LEVEL
        //                            //Do While nSubscriberCounter <= nSubscribers
        //                            nHlCounter = nHlCounter + 1;
        //                            nHlDependentParent = nHlCounter;

        //                            //nDependents = Val(txtNoDependents.Lines(nSubscriberCounter - 1))
        //                            //HL - HIERARCHICAL LEVEL
        //                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
        //                            oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
        //                            oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());   //Hierarchical Parent ID Number
        //                            oSegment.set_DataElementValue(3, 0, "22");      //Hierarchical Level Code
        //                            if (_SubscriberRelationshipCode.Trim().ToUpper() == "18")
        //                                oSegment.set_DataElementValue(4, 0, "0");     //Hierarchical Child Code
        //                            else
        //                                oSegment.set_DataElementValue(4, 0, "1");//Hierarchical Child Code
        //                            #endregion HL Loop

        //                            if (_SubscriberRelationshipCode.Trim().ToUpper() == "18")
        //                            {
        //                                #region Subscriber Demographics

        //                                //DMG - DEMOGRAPHIC INFORMATION
        //                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\DMG"));
        //                                oSegment.set_DataElementValue(1, 0, "D8");    //Date Time Period Format Qualifier
        //                                if (_SubscriberDOB.Trim() != "")
        //                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_SubscriberDOB.Trim())));//"19201210");     //Date Time Period
        //                                if (_SubscriberGender.Trim() != "")
        //                                {
        //                                    if (_SubscriberGender.Trim().ToUpper() == "OTHER")
        //                                    {
        //                                        _SubscriberGender = "U";
        //                                    }
        //                                    oSegment.set_DataElementValue(3, 0, _SubscriberGender.Trim().Substring(0, 1).ToUpper());//"SubscriberGender"
        //                                }
        //                                #endregion Subscriber Demographics

        //                                #region Subscriber
        //                                //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
        //                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
        //                                if (_SubscriberRelationshipCode.Trim().ToUpper() == "18")
        //                                    oSegment.set_DataElementValue(1, 0, "QC");    //Entity Identifier Code
        //                                else
        //                                    oSegment.set_DataElementValue(1, 0, "IL");      //Entity Identifier Code

        //                                oSegment.set_DataElementValue(2, 0, "1");     //Entity Type Qualifier
        //                                oSegment.set_DataElementValue(3, 0, _SubscriberLName.Trim());//"Subscriber Last Name");     //Name Last or Organization Name
        //                                oSegment.set_DataElementValue(4, 0, _SubscriberFName.Trim());//"Subscriber First Name");      //Name First
        //                                oSegment.set_DataElementValue(8, 0, "MI");     //Identification Code Qualifier
        //                                oSegment.set_DataElementValue(9, 0, _SubscriberInsuranceID.Trim());//"123456789A");     //Identification Code
        //                                #endregion Subscriber

        //                                #region Trace Number
        //                                //TRN - TRACE
        //                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\TRN"));
        //                                oSegment.set_DataElementValue(1, 0, "1");    //Trace Type Code
        //                                oSegment.set_DataElementValue(2, 0, _SubscriberGroupID.Trim());//"1625032606");     //Reference Identification
        //                                #endregion Trace Number

        //                                #region Reference ID
        //                                //REF - REFERENCE IDENTIFICATION
        //                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\REF"));
        //                                oSegment.set_DataElementValue(1, 0, "BLT");    //Reference Identification Qualifier
        //                                oSegment.set_DataElementValue(2, 0, "111");//"111");     //Reference Identification
        //                                #endregion Reference ID

        //                                #region AMT Loop
        //                                //AMT - MONETARY AMOUNT
        //                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\AMT"));
        //                                oSegment.set_DataElementValue(1, 0, "T3");     //Amount Qualifier Code
        //                                oSegment.set_DataElementValue(2, 0, _ClaimTotal);//"8513.88");      //Monetary Amount
        //                                #endregion AMT Loop

        //                                for (int j = 0; j < oTransaction.Lines.Count; j++)
        //                                {
        //                                    #region Service Date Loop

        //                                    //DTP - DATE OR TIME OR PERIOD
        //                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\DTP"));
        //                                    oSegment.set_DataElementValue(1, 0, "232");    //Date/Time Qualifier
        //                                    oSegment.set_DataElementValue(2, 0, "RD8");  //Date Time Period Format Qualifier
        //                                    oSegment.set_DataElementValue(3, 0, gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceFrom.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceTill.ToShortDateString()));//"19960831-19960906");    //Date Time Period

        //                                    #endregion Service Date Loop

        //                                    #region Service Information Loop

        //                                    ////SVC Service Information
        //                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\SVC"));
        //                                    oSegment.set_DataElementValue(1, 1, "HC");     //Product/Service ID Qualifier
        //                                    oSegment.set_DataElementValue(1, 2, oTransaction.Lines[j].CPTCode.Trim());     //Product/Service ID
        //                                    string _charges = "";
        //                                    if (Convert.ToString(oTransaction.Lines[j].Total).Substring(Convert.ToString(oTransaction.Lines[j].Total).Length - 2, 2) == "00")
        //                                    {
        //                                        _charges = Convert.ToString(oTransaction.Lines[j].Total).Substring(0, Convert.ToString(oTransaction.Lines[j].Total).Length - 3);
        //                                    }

        //                                    oSegment.set_DataElementValue(2, 0, _charges);     //Monetary Amount

        //                                    #endregion Service Information Loop

        //                                    #region REF Loop
        //                                    ////REF - REFERENCE IDENTIFICATION
        //                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\REF"));
        //                                    //oSegment.set_DataElementValue(1, 0, "FJ");     //Reference Identification Qualifier
        //                                    //oSegment.set_DataElementValue(2, 0, "02");      //Reference Identification
        //                                    #endregion REF Loop

        //                                    #region Service Line Date
        //                                    ////DTP - DATE OR TIME OR PERIOD
        //                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\DTP"));
        //                                    oSegment.set_DataElementValue(1, 0, "472");     //Date/Time Qualifier
        //                                    oSegment.set_DataElementValue(2, 0, "RD8");      //Date Time Period Format Qualifier
        //                                    oSegment.set_DataElementValue(3, 0, gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceFrom.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceTill.ToShortDateString()));//"19960931-19961030");     //Date Time Period
        //                                    // }
        //                                    #endregion Service Line Date
        //                                }
        //                            }
        //                            else
        //                            {
        //                                #region " DETAIL DEPENDENT LEVEL "

        //                                #region HL Loop

        //                                ////DETAIL DEPENDENT LEVEL
        //                                //Do While nDependentCounter <= nDependents
        //                                nHlCounter = nHlCounter + 1;

        //                                //HL - HIERARCHICAL LEVEL
        //                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
        //                                oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
        //                                oSegment.set_DataElementValue(2, 0, nHlDependentParent.ToString());     //Hierarchical Parent ID Number
        //                                oSegment.set_DataElementValue(3, 0, "23");    //Hierarchical Level Code

        //                                #endregion HL Loop

        //                                #region Patient Demographics

        //                                //DMG - DEMOGRAPHIC INFORMATION
        //                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\DMG"));
        //                                oSegment.set_DataElementValue(1, 0, "D8");     //Date Time Period Format Qualifier
        //                                oSegment.set_DataElementValue(2, 0, Convert.ToString(_PatientDOB.Trim()));     //Date Time Period
        //                                if (_PatientGender.Trim() != "")
        //                                {
        //                                    if (_PatientGender.Trim().ToUpper() == "OTHER")
        //                                    {
        //                                        _PatientGender = "U";
        //                                    }
        //                                    oSegment.set_DataElementValue(3, 0, _PatientGender.Trim().Substring(0, 1).ToUpper());//"SubscriberGender"
        //                                }
        //                                #endregion Patient Demographics

        //                                #region Patient Info
        //                                //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME\
        //                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
        //                                oSegment.set_DataElementValue(1, 0, "QC");                        //Entity Identifier Code
        //                                oSegment.set_DataElementValue(2, 0, "1");                         //Entity Type Qualifier
        //                                oSegment.set_DataElementValue(3, 0, _PatientLastName.Trim());     //Name Last or Organization Name
        //                                oSegment.set_DataElementValue(4, 0, _PatientFirstName.Trim());    //Name First
        //                                oSegment.set_DataElementValue(8, 0, "SY");                        //Identification Code Qualifier
        //                                oSegment.set_DataElementValue(9, 0, _PatientSSN.Trim());          //"9876453B");      //Identification Code
        //                                #endregion Patient Info

        //                                #region Trace Loop
        //                                //TRN - TRACE
        //                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\TRN"));
        //                                oSegment.set_DataElementValue(1, 0, "1");               //Trace Type Code
        //                                oSegment.set_DataElementValue(2, 0, "1347897353");      //Reference Identification
        //                                #endregion Trace Loop

        //                                #region REF Loop
        //                                //REF - REFERENCE IDENTIFICATION
        //                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\REF"));
        //                                oSegment.set_DataElementValue(1, 0, "BLT");      //Reference Identification Qualifier
        //                                oSegment.set_DataElementValue(2, 0, "111");      //Reference Identification
        //                                #endregion REF Loop

        //                                #region AMT Loop
        //                                //AMT - MONETARY AMOUNT
        //                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\AMT"));
        //                                oSegment.set_DataElementValue(1, 0, "T3");       //Amount Qualifier Code
        //                                oSegment.set_DataElementValue(2, 0, "820");      //Monetary Amount
        //                                #endregion AMT Loop

        //                                for (int j = 0; j < oTransaction.Lines.Count; j++)
        //                                {
        //                                    #region Service Date Loop

        //                                    //DTP - DATE OR TIME OR PERIOD
        //                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\DTP"));
        //                                    oSegment.set_DataElementValue(1, 0, "232");    //Date/Time Qualifier
        //                                    oSegment.set_DataElementValue(2, 0, "RD8");  //Date Time Period Format Qualifier
        //                                    oSegment.set_DataElementValue(3, 0, gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceFrom.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceTill.ToShortDateString()));//"19960831-19960906");    //Date Time Period

        //                                    #endregion Service Date Loop

        //                                    #region Service Information Loop

        //                                    //if ((nDependents == 0 )
        //                                    //{
        //                                    ////SVC Service Information
        //                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\SVC"));
        //                                    oSegment.set_DataElementValue(1, 1, "HC");     //Product/Service ID Qualifier
        //                                    oSegment.set_DataElementValue(1, 2, oTransaction.Lines[j].CPTCode.Trim());     //Product/Service ID
        //                                    string _charges = "";
        //                                    if (Convert.ToString(oTransaction.Lines[j].Total).Substring(Convert.ToString(oTransaction.Lines[j].Total).Length - 2, 2) == "00")
        //                                    {
        //                                        _charges = Convert.ToString(oTransaction.Lines[j].Total).Substring(0, Convert.ToString(oTransaction.Lines[j].Total).Length - 3);
        //                                    }

        //                                    oSegment.set_DataElementValue(2, 0, _charges);     //Monetary Amount

        //                                    #endregion Service Information Loop

        //                                    #region REF Loop
        //                                    ////REF - REFERENCE IDENTIFICATION
        //                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\REF"));
        //                                    //oSegment.set_DataElementValue(1, 0, "FJ");     //Reference Identification Qualifier
        //                                    //oSegment.set_DataElementValue(2, 0, "02");      //Reference Identification
        //                                    #endregion REF Loop

        //                                    #region Service Line Date
        //                                    ////DTP - DATE OR TIME OR PERIOD
        //                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\DTP"));
        //                                    oSegment.set_DataElementValue(1, 0, "472");     //Date/Time Qualifier
        //                                    oSegment.set_DataElementValue(2, 0, "RD8");      //Date Time Period Format Qualifier
        //                                    oSegment.set_DataElementValue(3, 0, gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceFrom.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceTill.ToShortDateString()));//"19960931-19961030");     //Date Time Period
        //                                    // }
        //                                    #endregion Service Line Date
        //                                }

        //                                #endregion " DETAIL DEPENDENT LEVEL "
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        #region Save EDI File

        //        //string _strEDI = oEdiDoc.GetEdiString();
        //        oEdiDoc.Save(sPath + sEdiFile);
        //        string _strEDI = oEdiDoc.GetEdiString();

        //        SaveFileDialog oSave = new SaveFileDialog();
        //        oSave.Filter = "TEXT Files (*.txt)|*.txt|EDI Files (*.edi)|*.edi";
        //        if (oSave.ShowDialog() == DialogResult.OK)
        //        {
        //            System.IO.StreamReader oReader = new System.IO.StreamReader(sPath + sEdiFile);
        //            string strData;
        //            strData = oReader.ReadToEnd();
        //            oReader.Close();

        //            System.IO.StreamWriter oWriter = new System.IO.StreamWriter(oSave.FileName);
        //            oWriter.Write(strData);
        //            oWriter.Close();
        //            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.ClaimStatus, gloAuditTrail.ActivityType.GenerateEDI, "Generate EDI 276", _PatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success);
        //            MessageBox.Show("File Created Successfully", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        #endregion Save EDI
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

        //    }
        //}

        private void Generate276EDI()
        {
            Transaction oTransaction = null;
            gloBilling ogloBilling = new gloBilling(_databaseConnectionString, "");

          
            int nHlCounter = 0;
            int nHlInfoReceiverParent;
            int nHlServiceProviderParent;
            int nHlSubscriberParent;
            int nHlDependentParent;

            try
            {
                FillClearingHouseInfo();
                //This New method clears the oEdiDoc object except the schema loaded
                oEdiDoc.New();
                //Set the properties for oEdiDoc object
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);
                oEdiDoc.SegmentTerminator = "~\r\n";
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";


                #region Interchange Segment

                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "00");
                oSegment.set_DataElementValue(5, 0, "12");
                oSegment.set_DataElementValue(6, 0, _SenderID);//txtSenderID1.Text.Trim());// "Sender");
                oSegment.set_DataElementValue(7, 0, "12");
                oSegment.set_DataElementValue(8, 0, _ReceiverID);//txtReceiverID1.Text.Trim());//"ReceiverID");
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());//txtEnquiryTime.Text.Trim());//"1548");
                oSegment.set_DataElementValue(11, 0, "U");
                oSegment.set_DataElementValue(12, 0, "00401");
                oSegment.set_DataElementValue(13, 0, "00020");//txtControlNo.Text.Trim());//"000000020");
                oSegment.set_DataElementValue(14, 0, "0");
                oSegment.set_DataElementValue(15, 0, "T");
                oSegment.set_DataElementValue(16, 0, ":");

                #endregion Interchange Segment

                #region Functional Group

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X093A1"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HR");//txtFunctionID.Text.Trim());
                oSegment.set_DataElementValue(2, 0, _SenderCode.Trim());//txtSenderDept.Text.Trim());//"SenderDept");
                oSegment.set_DataElementValue(3, 0, _ReceiverCode.Trim());//txtReceiverDept.Text.Trim());//"ReceiverDept");
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//txtFunGroupDate.Text.Trim());//"20010821");
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());//txtFuncGroupTime.Text.Trim());//"1548");
                oSegment.set_DataElementValue(6, 0, "11000");//txtControlNo.Text.Trim());//"000001");
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X093A1");

                #endregion Functional Group

                #region Transaction Set
                //HEADER
                //ST TRANSACTION SET HEADER
                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("276"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                oSegment.set_DataElementValue(2, 0, "000020");
                #endregion Transaction Set

                #region BHT Segment

                //Begining of Herarchical Transaction Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0010");
                oSegment.set_DataElementValue(2, 0, "13");//Code 13=Request
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());//txtBHTDate.Text.Trim());//"19990501");//Date

                #endregion BHT Segment

                nHlCounter = 0;

                if (SelectedTransactions != null)
                {
                    if (SelectedTransactions.Count > 0)
                    {
                        for (int i = 0; i < SelectedTransactions.Count; i++)
                        {
                           // oTransaction = new Transaction();
                            //   TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicId);
                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    FillAllDetails(oTransaction);

                                    #region Calculate Claim Amount

                                    string _ClaimTotal = "";

                                    decimal _claimAmount = 0;
                                    for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                    {
                                        _claimAmount = _claimAmount + oTransaction.Lines[nLine].Total;
                                    }

                                    _ClaimTotal = _claimAmount.ToString("#0.00");

                                    if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                    {
                                        _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                    }

                                    #endregion " Calculate Claim Amount "

                                    #region HL Loop
                                    //'*************************************************************************************************
                                    //'DETAIL INFORMATION SOURCE LEVEL
                                    //Do While nInfoSourceCounter <= nInfoSources

                                    nHlCounter = nHlCounter + 1;
                                    nHlInfoReceiverParent = nHlCounter;
                                    //'HL - HIERARCHICAL LEVEL
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                    oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                    oSegment.set_DataElementValue(3, 0, "20");//txtInfoSourceLevel.Text.Trim());//"20");      //Hierarchical Level Code
                                    oSegment.set_DataElementValue(4, 0, "1");//txtInfoSourceLevel.Text.Trim());//"1");     //Hierarchical Child Code

                                    #endregion HL Loop

                                    #region Payer Loop

                                    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                    oSegment.set_DataElementValue(1, 0, "PR");  //Entity Identifier Code - PAYER
                                    oSegment.set_DataElementValue(2, 0, "2");    //Entity Type Qualifier
                                    oSegment.set_DataElementValue(3, 0, _PayerName.Trim());//"Payer Name");     //Name Last or Organization Name
                                    oSegment.set_DataElementValue(8, 0, "PI");    //Identification Code Qualifier
                                    oSegment.set_DataElementValue(9, 0, _PayerID.Trim());//"12345");     //Identification Code

                                    #endregion Payer Loop

                                    #region HL Loop
                                    //*************************************************************************************************
                                    //DETAIL INFORMATION RECEIVER LEVEL
                                    //Do While nInfoReceiverCounter <= nInfoReceivers

                                    nHlCounter = nHlCounter + 1;
                                    nHlServiceProviderParent = nHlCounter;
                                    //HL - HIERARCHICAL LEVEL
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                    oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                    oSegment.set_DataElementValue(2, 0, nHlInfoReceiverParent.ToString());      //Hierarchical Parent ID Number
                                    oSegment.set_DataElementValue(3, 0, "21");    //Hierarchical Level Code
                                    oSegment.set_DataElementValue(4, 0, "1");    //Hierarchical Child Code
                                    #endregion HL Loop

                                    #region Submitter
                                    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                    oSegment.set_DataElementValue(1, 0, "41");     //Entity Identifier Code - SUBMITTER
                                    oSegment.set_DataElementValue(2, 0, "2");   //Entity Type Qualifier
                                    oSegment.set_DataElementValue(3, 0, _SubmitterName.Trim());//"Receiver Name");     //Name Last or Organization Name
                                    oSegment.set_DataElementValue(8, 0, "46");     //Identification Code Qualifier
                                    oSegment.set_DataElementValue(9, 0, _SubmitterETIN.Trim());//"X67E");    //Identification Code
                                    #endregion Submitter

                                    #region HL Loop
                                    //*************************************************************************************************
                                    //DETAIL SERVICE PROVIDER LEVEL
                                    //Do While nServiceProviderCounter <= nServiceProviders

                                    nHlCounter = nHlCounter + 1;
                                    nHlSubscriberParent = nHlCounter;
                                    //HL - HIERARCHICAL LEVEL
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                    oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                    oSegment.set_DataElementValue(2, 0, nHlServiceProviderParent.ToString());     //Hierarchical Parent ID Number
                                    oSegment.set_DataElementValue(3, 0, "19");     //Hierarchical Level Code
                                    oSegment.set_DataElementValue(4, 0, "1");   //Hierarchical Child Code
                                    #endregion HL Loop

                                    #region Provider/Receiver Loop
                                    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                    oSegment.set_DataElementValue(1, 0, "1P");     //Entity Identifier Code - PROVIDER
                                    oSegment.set_DataElementValue(2, 0, "1");    //Entity Type Qualifier
                                    oSegment.set_DataElementValue(3, 0, _BillingFName.Trim() + " " + _BillingMName.Trim() + " " + _BillingLName);     //Name Last or Organization Name
                                    oSegment.set_DataElementValue(8, 0, "XX");   //Identification Code Qualifier
                                    oSegment.set_DataElementValue(9, 0, _BillingNPI.Trim());//"987666");    //Identification Code
                                    #endregion Provider/Receiver Loop

                                    #region HL Loop
                                    //*************************************************************************************************
                                    //DETAIL SUBSCRIBER LEVEL
                                    //Do While nSubscriberCounter <= nSubscribers

                                    nHlCounter = nHlCounter + 1;
                                    nHlDependentParent = nHlCounter;

                                    //nDependents = Val(txtNoDependents.Lines(nSubscriberCounter - 1))
                                    //HL - HIERARCHICAL LEVEL
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                    oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                    oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());   //Hierarchical Parent ID Number
                                    oSegment.set_DataElementValue(3, 0, "22");      //Hierarchical Level Code
                                    if (_SubscriberRelationshipCode.Trim().ToUpper() == "18")
                                        oSegment.set_DataElementValue(4, 0, "0");     //Hierarchical Child Code
                                    else
                                        oSegment.set_DataElementValue(4, 0, "1");//Hierarchical Child Code
                                    #endregion HL Loop

                                    if (_SubscriberRelationshipCode.Trim().ToUpper() == "18")
                                    {
                                        #region Subscriber Demographics

                                        //DMG - DEMOGRAPHIC INFORMATION
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\DMG"));
                                        oSegment.set_DataElementValue(1, 0, "D8");    //Date Time Period Format Qualifier
                                        if (_SubscriberDOB.Trim() != "")
                                            oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_SubscriberDOB.Trim())));//"19201210");     //Date Time Period
                                        if (_SubscriberGender.Trim() != "")
                                        {
                                            if (_SubscriberGender.Trim().ToUpper() == "OTHER")
                                            {
                                                _SubscriberGender = "U";
                                            }
                                            oSegment.set_DataElementValue(3, 0, _SubscriberGender.Trim().Substring(0, 1).ToUpper());//"SubscriberGender"
                                        }
                                        #endregion Subscriber Demographics

                                        #region Subscriber
                                        //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                        if (_SubscriberRelationshipCode.Trim().ToUpper() == "18")
                                            oSegment.set_DataElementValue(1, 0, "QC");    //Entity Identifier Code
                                        else
                                            oSegment.set_DataElementValue(1, 0, "IL");      //Entity Identifier Code

                                        oSegment.set_DataElementValue(2, 0, "1");     //Entity Type Qualifier
                                        oSegment.set_DataElementValue(3, 0, _SubscriberLName.Trim());//"Subscriber Last Name");     //Name Last or Organization Name
                                        oSegment.set_DataElementValue(4, 0, _SubscriberFName.Trim());//"Subscriber First Name");      //Name First
                                        oSegment.set_DataElementValue(8, 0, "MI");     //Identification Code Qualifier
                                        oSegment.set_DataElementValue(9, 0, _SubscriberInsuranceID.Trim());//"123456789A");       //Identification Code
                                        #endregion Subscriber

                                        #region Trace Number
                                        //TRN - TRACE
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\TRN"));
                                        oSegment.set_DataElementValue(1, 0, "1");    //Trace Type Code
                                        oSegment.set_DataElementValue(2, 0, _SubscriberGroupID.Trim());//"1625032606");     //Reference Identification
                                        #endregion Trace Number

                                        #region Reference ID
                                        //REF - REFERENCE IDENTIFICATION
                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\REF"));
                                        //oSegment.set_DataElementValue(1, 0, "BLT");    //Reference Identification Qualifier
                                        //oSegment.set_DataElementValue(2, 0, "111");//"111");     //Reference Identification
                                        #endregion Reference ID

                                        #region AMT Loop
                                        //AMT - MONETARY AMOUNT
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\AMT"));
                                        oSegment.set_DataElementValue(1, 0, "T3");     //Amount Qualifier Code
                                        oSegment.set_DataElementValue(2, 0, _ClaimTotal);//"8513.88");      //Monetary Amount
                                        #endregion AMT Loop

                                        #region Service Date Loop
                                        ////DTP - DATE OR TIME OR PERIOD
                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\DTP"));
                                        //oSegment.set_DataElementValue(1, 0, "232");    //Date/Time Qualifier
                                        //oSegment.set_DataElementValue(2, 0, "RD8");  //Date Time Period Format Qualifier
                                        //oSegment.set_DataElementValue(3, 0, gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[0].DateServiceFrom.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[oTransaction.Lines.Count-1].DateServiceTill.ToShortDateString()));//"19960831-19960906");    //Date Time Period

                                        #endregion Service Date Loop

                                        for (int j = 0; j < oTransaction.Lines.Count; j++)
                                        {
                                            #region Service Information Loop
                                            ////SVC Service Information
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\SVC"));
                                            oSegment.set_DataElementValue(1, 1, "HC");     //Product/Service ID Qualifier
                                            oSegment.set_DataElementValue(1, 2, oTransaction.Lines[j].CPTCode.Trim());     //Product/Service ID
                                            string _charges = "";
                                            if (Convert.ToString(oTransaction.Lines[j].Total).Substring(Convert.ToString(oTransaction.Lines[j].Total).Length - 2, 2) == "00")
                                            {
                                                _charges = Convert.ToString(oTransaction.Lines[j].Total).Substring(0, Convert.ToString(oTransaction.Lines[j].Total).Length - 3);
                                            }
                                            oSegment.set_DataElementValue(2, 0, _charges);     //Monetary Amount
                                            #endregion Service Information Loop

                                            #region REF Loop
                                            ////REF - REFERENCE IDENTIFICATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\REF"));
                                            oSegment.set_DataElementValue(1, 0, "FJ");     //Reference Identification Qualifier
                                            oSegment.set_DataElementValue(2, 0, "02");      //Reference Identification
                                            #endregion REF Loop

                                            #region Service Line Date
                                            ////DTP - DATE OR TIME OR PERIOD
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\DTP"));
                                            oSegment.set_DataElementValue(1, 0, "472");     //Date/Time Qualifier
                                            oSegment.set_DataElementValue(2, 0, "RD8");      //Date Time Period Format Qualifier
                                            oSegment.set_DataElementValue(3, 0, gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceFrom.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceTill.ToShortDateString()));//"19960931-19961030");     //Date Time Period

                                            #endregion Service Line Date
                                        }
                                    }
                                    else
                                    {
                                        #region " DETAIL DEPENDENT LEVEL "

                                        #region Subscriber
                                        //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                        if (_SubscriberRelationshipCode.Trim().ToUpper() == "18")
                                            oSegment.set_DataElementValue(1, 0, "QC");    //Entity Identifier Code
                                        else
                                            oSegment.set_DataElementValue(1, 0, "IL");      //Entity Identifier Code

                                        oSegment.set_DataElementValue(2, 0, "1");     //Entity Type Qualifier
                                        oSegment.set_DataElementValue(3, 0, _SubscriberLName.Trim());//"Subscriber Last Name");     //Name Last or Organization Name
                                        oSegment.set_DataElementValue(4, 0, _SubscriberFName.Trim());//"Subscriber First Name");      //Name First
                                        oSegment.set_DataElementValue(8, 0, "MI");     //Identification Code Qualifier
                                        oSegment.set_DataElementValue(9, 0, _SubscriberInsuranceID.Trim());//"123456789A");       //Identification Code
                                        #endregion Subscriber

                                        #region HL Loop

                                        ////DETAIL DEPENDENT LEVEL
                                        //Do While nDependentCounter <= nDependents
                                        nHlCounter = nHlCounter + 1;

                                        //HL - HIERARCHICAL LEVEL
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                        oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                        oSegment.set_DataElementValue(2, 0, nHlDependentParent.ToString());     //Hierarchical Parent ID Number
                                        oSegment.set_DataElementValue(3, 0, "23");    //Hierarchical Level Code

                                        #endregion HL Loop

                                        #region Patient Demographics

                                        //DMG - DEMOGRAPHIC INFORMATION
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\DMG"));
                                        oSegment.set_DataElementValue(1, 0, "D8");     //Date Time Period Format Qualifier
                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(_PatientDOB.Trim()));     //Date Time Period
                                        if (_PatientGender.Trim() != "")
                                        {
                                            if (_PatientGender.Trim().ToUpper() == "OTHER")
                                            {
                                                _PatientGender = "U";
                                            }
                                            oSegment.set_DataElementValue(3, 0, _PatientGender.Trim().Substring(0, 1).ToUpper());//"SubscriberGender"
                                        }
                                        #endregion Patient Demographics

                                        #region Patient Info
                                        //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME\
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                        oSegment.set_DataElementValue(1, 0, "QC");                        //Entity Identifier Code
                                        oSegment.set_DataElementValue(2, 0, "1");                         //Entity Type Qualifier
                                        oSegment.set_DataElementValue(3, 0, _PatientLastName.Trim());     //Name Last or Organization Name
                                        oSegment.set_DataElementValue(4, 0, _PatientFirstName.Trim());    //Name First
                                        oSegment.set_DataElementValue(8, 0, "MI");                        //Identification Code Qualifier
                                        oSegment.set_DataElementValue(9, 0, _PatientSSN.Trim());            //"9876453B");      //Identification Code
                                        #endregion Patient Info

                                        #region Trace Loop
                                        //TRN - TRACE
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\TRN"));
                                        oSegment.set_DataElementValue(1, 0, "1");               //Trace Type Code
                                        oSegment.set_DataElementValue(2, 0, "1347897353");      //Reference Identification
                                        #endregion Trace Loop

                                        #region REF Loop
                                        ////REF - REFERENCE IDENTIFICATION
                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\REF"));
                                        //oSegment.set_DataElementValue(1, 0, "BLT");      //Reference Identification Qualifier
                                        //oSegment.set_DataElementValue(2, 0, "111");      //Reference Identification
                                        #endregion REF Loop

                                        #region AMT Loop
                                        //AMT - MONETARY AMOUNT
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\AMT"));
                                        oSegment.set_DataElementValue(1, 0, "T3");       //Amount Qualifier Code
                                        oSegment.set_DataElementValue(2, 0, _ClaimTotal);      //Monetary Amount
                                        #endregion AMT Loop

                                        #region Date Time Loop
                                        //DTP - DATE OR TIME OR PERIOD
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\DTP"));
                                        oSegment.set_DataElementValue(1, 0, "232");    //Date/Time Qualifier
                                        oSegment.set_DataElementValue(2, 0, "RD8");  //Date Time Period Format Qualifier
                                        oSegment.set_DataElementValue(3, 0, gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[0].DateServiceFrom.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[oTransaction.Lines.Count - 1].DateServiceTill.ToShortDateString()));//"19960831-19960906");    //Date Time Period

                                        #endregion Date Time Loop

                                        for (int j = 0; j < oTransaction.Lines.Count; j++)
                                        {
                                            #region SVC Loop
                                            //SVC Service Information
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\SVC"));
                                            oSegment.set_DataElementValue(1, 1, "HC");     //Product/Service ID Qualifier
                                            oSegment.set_DataElementValue(1, 2, oTransaction.Lines[j].CPTCode.Trim());     //Product/Service ID
                                            string _charges = "";
                                            if (Convert.ToString(oTransaction.Lines[j].Total).Substring(Convert.ToString(oTransaction.Lines[j].Total).Length - 2, 2) == "00")
                                            {
                                                _charges = Convert.ToString(oTransaction.Lines[j].Total).Substring(0, Convert.ToString(oTransaction.Lines[j].Total).Length - 3);
                                            }

                                            oSegment.set_DataElementValue(2, 0, _charges);     //Monetary Amount

                                            #endregion SVC Loop

                                            #region REF Loop
                                            //REF - REFERENCE IDENTIFICATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\REF"));
                                            oSegment.set_DataElementValue(1, 0, "FJ");      //Reference Identification Qualifier
                                            oSegment.set_DataElementValue(2, 0, "78");      //Reference Identification
                                            #endregion REF Loop

                                            #region Date Time Loop for Service Line

                                            //DTP - DATE OR TIME OR PERIOD
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\DTP"));
                                            oSegment.set_DataElementValue(1, 0, "472");     //Date/Time Qualifier
                                            oSegment.set_DataElementValue(2, 0, "RD8");      //Date Time Period Format Qualifier
                                            oSegment.set_DataElementValue(3, 0, gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceFrom.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceTill.ToShortDateString()));//"19960931-19961030");     //Date Time Period
                                            // }
                                            #endregion Date Time Loop for Service Line
                                        }
                                        #endregion " DETAIL DEPENDENT LEVEL "
                                    }
                                }
                                if (oTransaction != null)
                                {
                                    oTransaction.Dispose();
                                    oTransaction = null;
                                }
                            }
                        }

                        #region Save EDI File

                        oEdiDoc.Save(sPath + sEdiFile);

                        SaveFileDialog oSave = new SaveFileDialog();
                        oSave.Filter = "TEXT Files (*.txt)|*.txt|EDI Files (*.edi)|*.edi";
                        if (oSave.ShowDialog(this) == DialogResult.OK)
                        {
                            System.IO.StreamReader oReader = new System.IO.StreamReader(sPath + sEdiFile);
                            string strData;
                            strData = oReader.ReadToEnd();
                            oReader.Close();

                            System.IO.StreamWriter oWriter = new System.IO.StreamWriter(oSave.FileName);
                            oWriter.Write(strData);
                            oWriter.Close();
                            //MessageBox.Show("File Created Successfully", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        oSave.Dispose();
                        oSave = null;
                        #endregion Save EDI

                        //DESTROY OBJECTS
                        oSegment.Dispose();
                        oTransactionset.Dispose();
                        oGroup.Dispose();
                        oInterchange.Dispose();
                    }
                }
            }
            catch (RuntimeWrappedException exEDI)
            {
                MessageBox.Show(exEDI.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }

            }
        }

        private void Generate276EDI_OLD()
        {
           
            Transaction oTransaction = null;

            int nHlCounter = 0;
            int nHlInfoReceiverParent;
            int nHlServiceProviderParent;
            int nHlSubscriberParent;
            int nHlDependentParent;
            try
            {
                sPath = AppDomain.CurrentDomain.BaseDirectory;
                sSEFFile = "276_X093.SEF";
                sEdiFile = "276OUTPUT.x12";
                gloBilling ogloBilling = new gloBilling(_databaseConnectionString, "");
                ediDocument.Set(ref oEdiDoc, new ediDocument());

                ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());
                oSchemas.EnableStandardReference = false;

                oEdiDoc.SegmentTerminator = "~\r\n";
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";

                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;

                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema("276_X093.SEF", 0));

                FillClearingHouseInfo();
                if (SelectedTransactions != null)
                {
                    if (SelectedTransactions.Count > 0)
                    {
                        for (int i = 0; i < SelectedTransactions.Count; i++)
                        {
                           // oTransaction = new Transaction();
                           // TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicId);
                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    FillSubmitterInfo(Convert.ToInt64(_ClinicId), oTransaction.ProviderID);
                                }
                                oTransaction.Dispose();
                                oTransaction = null;
                            }
                        }
                    }
                }
                //oEdiDoc = new ediDocument();
                #region Interchange Segment

                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "00");
                oSegment.set_DataElementValue(5, 0, "12");
                oSegment.set_DataElementValue(6, 0, _SenderID);//txtSenderID1.Text.Trim());// "Sender");
                oSegment.set_DataElementValue(7, 0, "12");
                oSegment.set_DataElementValue(8, 0, _ReceiverID);//txtReceiverID1.Text.Trim());//"ReceiverID");
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());//txtEnquiryTime.Text.Trim());//"1548");
                oSegment.set_DataElementValue(11, 0, "U");
                oSegment.set_DataElementValue(12, 0, "00401");
                oSegment.set_DataElementValue(13, 0, "00020");//txtControlNo.Text.Trim());//"000000020");
                oSegment.set_DataElementValue(14, 0, "0");
                oSegment.set_DataElementValue(15, 0, "T");
                oSegment.set_DataElementValue(16, 0, ":");

                #endregion Interchange Segment

                #region Functional Group

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X093"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HS");//txtFunctionID.Text.Trim());
                oSegment.set_DataElementValue(2, 0, _SenderCode.Trim());//txtSenderDept.Text.Trim());//"SenderDept");
                oSegment.set_DataElementValue(3, 0, _ReceiverCode.Trim());//txtReceiverDept.Text.Trim());//"ReceiverDept");
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//txtFunGroupDate.Text.Trim());//"20010821");
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());//txtFuncGroupTime.Text.Trim());//"1548");
                oSegment.set_DataElementValue(6, 0, "00011");//txtControlNo.Text.Trim());//"000001");
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X093");

                #endregion Functional Group

                #region Transaction Set
                //HEADER
                //ST TRANSACTION SET HEADER
                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("276"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, txtTSControlNumber.Text.Trim());
                #endregion Transaction Set

                #region BHT Segment

                //Begining of Herarchical Transaction Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0022"); // txtBHT_HerarchicalStrCode.Text.Trim());//"0022");
                string[] Transactiontype = cmbTypeOfTransaction.Text.Split('-');
                oSegment.set_DataElementValue(2, 0, "13");//Transactiontype[0].Trim());//"13");//Code 13=Request
                oSegment.set_DataElementValue(3, 0, "999999");//txtBHTRefIdentification.Text.Trim());////"10001234");//ReferenceID
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());//txtBHTDate.Text.Trim());//"19990501");//Date

                #endregion BHT Segment


                nHlCounter = 0;

                if (SelectedTransactions != null)
                {
                    if (SelectedTransactions.Count > 0)
                    {
                        for (int i = 0; i < SelectedTransactions.Count; i++)
                        {
                          //  oTransaction = new Transaction();
                          //  TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicId);
                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    FillAllDetails(oTransaction);


                                    #region HL Loop
                                    //'*************************************************************************************************
                                    //'DETAIL INFORMATION SOURCE LEVEL
                                    //Do While nInfoSourceCounter <= nInfoSources

                                    nHlCounter = nHlCounter + 1;
                                    nHlInfoReceiverParent = nHlCounter;
                                    //'HL - HIERARCHICAL LEVEL
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                    oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                    oSegment.set_DataElementValue(3, 0, "20");//txtInfoSourceLevel.Text.Trim());//"20");      //Hierarchical Level Code
                                    oSegment.set_DataElementValue(4, 0, "1");//txtInfoSourceLevel.Text.Trim());//"1");     //Hierarchical Child Code

                                    #endregion HL Loop

                                    #region Payer Loop

                                    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                    oSegment.set_DataElementValue(1, 0, "PR");  //Entity Identifier Code - PAYER
                                    oSegment.set_DataElementValue(2, 0, "2");    //Entity Type Qualifier
                                    oSegment.set_DataElementValue(3, 0, _PayerName.Trim());//"Payer Name");     //Name Last or Organization Name
                                    oSegment.set_DataElementValue(8, 0, "PI");    //Identification Code Qualifier
                                    oSegment.set_DataElementValue(9, 0, _PayerID.Trim());//"12345");     //Identification Code

                                    #endregion Payer Loop

                                    #region HL Loop
                                    //*************************************************************************************************
                                    //DETAIL INFORMATION RECEIVER LEVEL
                                    //Do While nInfoReceiverCounter <= nInfoReceivers

                                    nHlCounter = nHlCounter + 1;
                                    nHlServiceProviderParent = nHlCounter;
                                    //HL - HIERARCHICAL LEVEL
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                    oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                    oSegment.set_DataElementValue(2, 0, nHlInfoReceiverParent.ToString());      //Hierarchical Parent ID Number
                                    oSegment.set_DataElementValue(3, 0, "21");    //Hierarchical Level Code
                                    oSegment.set_DataElementValue(4, 0, "1");    //Hierarchical Child Code
                                    #endregion HL Loop

                                    #region Submitter
                                    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                    oSegment.set_DataElementValue(1, 0, "41");     //Entity Identifier Code - SUBMITTER
                                    oSegment.set_DataElementValue(2, 0, "2");   //Entity Type Qualifier
                                    oSegment.set_DataElementValue(3, 0, _SubmitterName.Trim());//"Receiver Name");     //Name Last or Organization Name
                                    oSegment.set_DataElementValue(8, 0, "46");     //Identification Code Qualifier
                                    oSegment.set_DataElementValue(9, 0, _SubmitterETIN.Trim());//"X67E");    //Identification Code
                                    #endregion Submitter

                                    #region HL Loop
                                    //*************************************************************************************************
                                    //DETAIL SERVICE PROVIDER LEVEL
                                    //Do While nServiceProviderCounter <= nServiceProviders

                                    nHlCounter = nHlCounter + 1;
                                    nHlSubscriberParent = nHlCounter;
                                    //HL - HIERARCHICAL LEVEL
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                    oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                    oSegment.set_DataElementValue(2, 0, nHlServiceProviderParent.ToString());     //Hierarchical Parent ID Number
                                    oSegment.set_DataElementValue(3, 0, "19");     //Hierarchical Level Code
                                    oSegment.set_DataElementValue(4, 0, "1");   //Hierarchical Child Code
                                    #endregion HL Loop

                                    #region Provider/Receiver Loop
                                    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                    oSegment.set_DataElementValue(1, 0, "1P");     //Entity Identifier Code - PROVIDER
                                    oSegment.set_DataElementValue(2, 0, "1");    //Entity Type Qualifier
                                    oSegment.set_DataElementValue(3, 0, _BillingFName.Trim() + " " + _BillingMName.Trim() + " " + _BillingLName);     //Name Last or Organization Name
                                    oSegment.set_DataElementValue(8, 0, "XX");   //Identification Code Qualifier
                                    oSegment.set_DataElementValue(9, 0, _BillingNPI.Trim());//"987666");    //Identification Code
                                    #endregion Provider/Receiver Loop

                                    #region HL Loop
                                    //*************************************************************************************************
                                    //DETAIL SUBSCRIBER LEVEL
                                    //Do While nSubscriberCounter <= nSubscribers
                                    nHlCounter = nHlCounter + 1;
                                    nHlDependentParent = nHlCounter;

                                    //nDependents = Val(txtNoDependents.Lines(nSubscriberCounter - 1))
                                    //HL - HIERARCHICAL LEVEL
                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                    oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                    oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());   //Hierarchical Parent ID Number
                                    oSegment.set_DataElementValue(3, 0, "22");      //Hierarchical Level Code
                                    if (_SubscriberRelationshipCode.Trim().ToUpper() == "18")
                                        oSegment.set_DataElementValue(4, 0, "0");     //Hierarchical Child Code
                                    else
                                        oSegment.set_DataElementValue(4, 0, "1");//Hierarchical Child Code
                                    #endregion HL Loop

                                    if (_SubscriberRelationshipCode.Trim().ToUpper() == "18")
                                    {
                                        #region Subscriber Demographics

                                        //DMG - DEMOGRAPHIC INFORMATION
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\DMG"));
                                        oSegment.set_DataElementValue(1, 0, "D8");    //Date Time Period Format Qualifier
                                        if (_SubscriberDOB.Trim() != "")
                                            oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_SubscriberDOB.Trim())));//"19201210");     //Date Time Period
                                        if (_SubscriberGender.Trim() != "")
                                        {
                                            if (_SubscriberGender.Trim().ToUpper() == "OTHER")
                                            {
                                                _SubscriberGender = "U";
                                            }
                                            oSegment.set_DataElementValue(3, 0, _SubscriberGender.Trim().Substring(0, 1).ToUpper());//"SubscriberGender"
                                        }
                                        #endregion Subscriber Demographics

                                        #region Subscriber
                                        //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                        if (_SubscriberRelationshipCode.Trim().ToUpper() == "18")
                                            oSegment.set_DataElementValue(1, 0, "QC");    //Entity Identifier Code
                                        else
                                            oSegment.set_DataElementValue(1, 0, "IL");      //Entity Identifier Code

                                        oSegment.set_DataElementValue(2, 0, "1");     //Entity Type Qualifier
                                        oSegment.set_DataElementValue(3, 0, _SubscriberLName.Trim());//"Subscriber Last Name");     //Name Last or Organization Name
                                        oSegment.set_DataElementValue(4, 0, _SubscriberFName.Trim());//"Subscriber First Name");      //Name First
                                        oSegment.set_DataElementValue(8, 0, "MI");     //Identification Code Qualifier
                                        oSegment.set_DataElementValue(9, 0, _SubscriberInsuranceID.Trim());//"123456789A");     //Identification Code
                                        #endregion Subscriber

                                        #region Trace Number
                                        //TRN - TRACE
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\TRN"));
                                        oSegment.set_DataElementValue(1, 0, "1");    //Trace Type Code
                                        oSegment.set_DataElementValue(2, 0, _SubscriberGroupID.Trim());//"1625032606");     //Reference Identification
                                        #endregion Trace Number

                                        #region Reference ID
                                        //REF - REFERENCE IDENTIFICATION
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\REF"));
                                        oSegment.set_DataElementValue(1, 0, "BLT");    //Reference Identification Qualifier
                                        oSegment.set_DataElementValue(2, 0, "111");//"111");     //Reference Identification
                                        #endregion Reference ID

                                        #region AMT Loop
                                        //AMT - MONETARY AMOUNT
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\AMT"));
                                        oSegment.set_DataElementValue(1, 0, "T3");     //Amount Qualifier Code
                                        oSegment.set_DataElementValue(2, 0, "100.00");//"8513.88");      //Monetary Amount
                                        #endregion AMT Loop

                                        #region Service Date Loop
                                        //DTP - DATE OR TIME OR PERIOD
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\DTP"));
                                        oSegment.set_DataElementValue(1, 0, "232");    //Date/Time Qualifier
                                        oSegment.set_DataElementValue(2, 0, "RD8");  //Date Time Period Format Qualifier
                                        oSegment.set_DataElementValue(3, 0, "19960831-19960906");//"19960831-19960906");    //Date Time Period
                                        #endregion Service Date Loop

                                        #region Service Information Loop
                                        //if ((nDependents == 0 )
                                        //{
                                        ////SVC Service Information
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\SVC"));
                                        oSegment.set_DataElementValue(1, 1, "AD");     //Product/Service ID Qualifier
                                        oSegment.set_DataElementValue(1, 2, "CD");     //Product/Service ID
                                        oSegment.set_DataElementValue(2, 0, "200");     //Monetary Amount
                                        #endregion Service Information Loop

                                        #region REF Loop
                                        ////REF - REFERENCE IDENTIFICATION
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\REF"));
                                        oSegment.set_DataElementValue(1, 0, "FJ");     //Reference Identification Qualifier
                                        oSegment.set_DataElementValue(2, 0, "02");      //Reference Identification
                                        #endregion REF Loop

                                        #region Service Line Date
                                        ////DTP - DATE OR TIME OR PERIOD
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\DTP"));
                                        oSegment.set_DataElementValue(1, 0, "472");     //Date/Time Qualifier
                                        oSegment.set_DataElementValue(2, 0, "RD8");      //Date Time Period Format Qualifier
                                        oSegment.set_DataElementValue(3, 0, "19960931-19961030");//"19960931-19961030");     //Date Time Period
                                        // }
                                        #endregion Service Line Date

                                    }
                                    else
                                    {
                                        #region " DETAIL DEPENDENT LEVEL "

                                        #region HL Loop

                                        ////DETAIL DEPENDENT LEVEL
                                        //Do While nDependentCounter <= nDependents
                                        nHlCounter = nHlCounter + 1;

                                        //HL - HIERARCHICAL LEVEL
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                        oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                        oSegment.set_DataElementValue(2, 0, nHlDependentParent.ToString());     //Hierarchical Parent ID Number
                                        oSegment.set_DataElementValue(3, 0, "23");    //Hierarchical Level Code

                                        #endregion HL Loop

                                        #region Patient Demographics

                                        //DMG - DEMOGRAPHIC INFORMATION
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\DMG"));
                                        oSegment.set_DataElementValue(1, 0, "D8");     //Date Time Period Format Qualifier
                                        oSegment.set_DataElementValue(2, 0, _PatientDOB.Trim());     //Date Time Period
                                        if (_PatientGender.Trim() != "")
                                        {
                                            if (_PatientGender.Trim().ToUpper() == "OTHER")
                                            {
                                                _PatientGender = "U";
                                            }
                                            oSegment.set_DataElementValue(3, 0, _PatientGender.Trim().Substring(0, 1).ToUpper());//"SubscriberGender"
                                        }
                                        #endregion Patient Demographics

                                        #region Patient Info
                                        //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME\
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                        oSegment.set_DataElementValue(1, 0, "QC");                        //Entity Identifier Code
                                        oSegment.set_DataElementValue(2, 0, "1");                         //Entity Type Qualifier
                                        oSegment.set_DataElementValue(3, 0, _PatientLastName.Trim());     //Name Last or Organization Name
                                        oSegment.set_DataElementValue(4, 0, _PatientFirstName.Trim());    //Name First
                                        oSegment.set_DataElementValue(8, 0, "SY");                        //Identification Code Qualifier
                                        oSegment.set_DataElementValue(9, 0, _PatientSSN.Trim());          //"9876453B");      //Identification Code
                                        #endregion Patient Info

                                        #region Trace Loop
                                        //TRN - TRACE
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\TRN"));
                                        oSegment.set_DataElementValue(1, 0, "1");               //Trace Type Code
                                        oSegment.set_DataElementValue(2, 0, "1347897353");      //Reference Identification
                                        #endregion Trace Loop

                                        #region REF Loop
                                        //REF - REFERENCE IDENTIFICATION
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\REF"));
                                        oSegment.set_DataElementValue(1, 0, "BLT");      //Reference Identification Qualifier
                                        oSegment.set_DataElementValue(2, 0, "111");      //Reference Identification
                                        #endregion REF Loop

                                        #region AMT Loop
                                        //AMT - MONETARY AMOUNT
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\AMT"));
                                        oSegment.set_DataElementValue(1, 0, "T3");       //Amount Qualifier Code
                                        oSegment.set_DataElementValue(2, 0, "820");      //Monetary Amount
                                        #endregion AMT Loop

                                        #region Date Time Loop
                                        //DTP - DATE OR TIME OR PERIOD
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\DTP"));
                                        oSegment.set_DataElementValue(1, 0, "232");     //Date/Time Qualifier
                                        oSegment.set_DataElementValue(2, 0, "RD8");     //Date Time Period Format Qualifier
                                        oSegment.set_DataElementValue(3, 0, "");        //"19960831-19960906");      //Date Time Period
                                        #endregion Date Time Loop

                                        #region SVC Loop
                                        //SVC Service Information
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\SVC"));
                                        oSegment.set_DataElementValue(1, 1, "AD");     //Product/Service ID Qualifier
                                        oSegment.set_DataElementValue(1, 2, "CD");     //Product/Service ID
                                        oSegment.set_DataElementValue(2, 0, "820");    //Monetary Amount
                                        #endregion SVC Loop

                                        #region REF Loop
                                        //REF - REFERENCE IDENTIFICATION
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\REF"));
                                        oSegment.set_DataElementValue(1, 0, "FJ");      //Reference Identification Qualifier
                                        oSegment.set_DataElementValue(2, 0, "78");      //Reference Identification
                                        #endregion REF Loop

                                        #region Date Time Loop for Service Line

                                        //DTP - DATE OR TIME OR PERIOD
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\DTP"));
                                        oSegment.set_DataElementValue(1, 0, "472");      //Date/Time Qualifier
                                        oSegment.set_DataElementValue(2, 0, "RD8");      //Date Time Period Format Qualifier
                                        oSegment.set_DataElementValue(3, 0, "");         //"19970219-19971103");      //Date Time Period

                                        #endregion Date Time Loop for Service Line

                                        #endregion " DETAIL DEPENDENT LEVEL "
                                    }
                                }
                                if (oTransaction != null)
                                {
                                    oTransaction.Dispose();
                                    oTransaction = null;
                                }
                            }
                        }

                        #region Save EDI File

                        oEdiDoc.Save(sPath + sEdiFile);

                        SaveFileDialog oSave = new SaveFileDialog();
                        oSave.Filter = "TEXT Files (*.txt)|*.txt|EDI Files (*.edi)|*.edi";
                        if (oSave.ShowDialog(this) == DialogResult.OK)
                        {
                            System.IO.StreamReader oReader = new System.IO.StreamReader(sPath + sEdiFile);
                            string strData;
                            strData = oReader.ReadToEnd();
                            oReader.Close();

                            System.IO.StreamWriter oWriter = new System.IO.StreamWriter(oSave.FileName);
                            oWriter.Write(strData);
                            oWriter.Close();
                            MessageBox.Show("File Created Successfully", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        oSave.Dispose();
                        oSave = null;
                        #endregion Save EDI
                    }
                }
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Generate EDI Method

        private void frmEDI276Generation_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //if (oEdiDoc != null)
                //{
                //    if (oGroup != null)
                //    {
                //        oGroup.Dispose();
                //    }
                //    if (oSegment != null)
                //    {
                //        oSegment.Dispose();
                //    }
                //    if (oInterchange != null)
                //    {
                //        oInterchange.Dispose();
                //    }
                //    if (oSchema != null)
                //    {
                //        oSchema.Dispose();
                //    }
                //    if (oSchemas != null)
                //    {
                //        oSchemas.Dispose();
                //    }
                //    if (oWarning != null)
                //    {
                //        oWarning.Dispose();
                //    }
                //    if (oWarnings != null)
                //    {
                //        oWarnings.Dispose();
                //    }
                //    oEdiDoc.Dispose();
                //}
               
                oEdiDoc.Close();
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }

 }