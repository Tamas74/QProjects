using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloEDI.gloBilling.Common;
using System.Collections;
using gloAppointmentBook.Books;
using System.Xml;
using System.IO;
using gloAuditTrail;
using gloEDI.gloBilling;

namespace gloEDI
{
    public partial class frmXMLClaim1500 : Form
    {
        #region " Variables "

        private ArrayList _arrHCFAData = null;
        private string _databaseconnectionstring = "";
        private string _messageboxcaption = String.Empty;
        private ArrayList _arrSelectedTransactions = null;
        private Transaction oTransaction = null;
        private Int64 _TransactionId = 0;
        bool IsSecondaryInsurance = false;
        Int64 _ClinicID = 0;
       
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _PatientID = 0;
        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        public ArrayList SelectedTransactions
        {
            get { return _arrSelectedTransactions; }
            set { _arrSelectedTransactions = value; }
        }

        public Int64 TransactionID
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        public ArrayList HCFAData
        {
            get { return _arrHCFAData; }
            set { _arrHCFAData = value; }
        }
        private ClaimValidationService _claimValidationService = ClaimValidationService.None;
        public ClaimValidationService ClaimValidationServiceType
        {
            get { return _claimValidationService; }
            set { _claimValidationService = value; }
        }
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

        //Submitter
        private string _SubmitterName = "";
        private string _SubmitterContactPersonName = "";
        private string _SubmitterContactPersonNo = "";
        private string _SubmitterCity = "";
        private string _SubmitterState = "";
        private string _SubmitterZIP = "";
        //private string _SubmitterETIN = "";
        private string _SubmitterAddress = "";

        //Receiver
        //private string _ReceiverName = "";
       // private string _ReceiverETIN = "";

        //Subscriber
        private string _SubscriberLName = "";
        //private string _SubscriberInsurancePST = "";
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
        private string _PayerName = "";
        private string _PayerID = "";
        private string _PayerAddress = "";
        private string _PayerCity = "";
        private string _PayerState = "";
        private string _PayerZip = "";

        private string _PatientAccountNo = "";


        //Facility
       // private string _FacilityCode = "";
       // private string _FacilityName = "";
      //  private string _FacilityAddress = "";
      //  private string _FacilityCity = "";
      //  private string _FacilityState = "";
       // private string _FacilityZip = "";
       // private string _FacilityNPI = "";


        //Other Insurance
        private string _OtherInsuranceSubscriberLName = "";
       // private string _OtherInsurancePST = "";
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
        //private string _OtherInsuranceSubscriberPhone = "";

        //ISA and GS Settings
       // private string _SenderID = "";
       // private string _ReceiverID = "";
       // private string _SenderName = "";
       // private string _ReceiverCode = "";


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
        
        private bool _IsAlphaII = true;

        public bool IsAlphaII
        {
            get { return _IsAlphaII; }
            set { _IsAlphaII = value; }
        }

        #endregion " Variables And Procedures "

        #region " Constructor "

        public frmXMLClaim1500(string DatabaseConnection, Int64 TransactionId)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnection;
            HCFAData = new ArrayList();
            SelectedTransactions = new ArrayList();
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrieve ClaimValidation from AppSettings "

            if (appSettings["ClaimValidationSetting"] != null)
            {
                if (appSettings["ClaimValidationSetting"] != "")
                {
                    if (appSettings["ClaimValidationSetting"] == "Alpha2")
                    {
                        _claimValidationService = ClaimValidationService.Alpha2;
                    }
                    else if (appSettings["ClaimValidationSetting"] == "YOST")
                    {
                        _claimValidationService = ClaimValidationService.YOST;
                    }
                }
            }
            else
            { _claimValidationService = ClaimValidationService.None; }

            #endregion

            this.TransactionID = TransactionId;

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
        }

        #endregion " Constructor "

        #region " Form Load "

        private void frmXMLClaim1500_Load(object sender, EventArgs e)
        {
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(_databaseconnectionstring, "");
           
            try
            {
                if (TransactionID > 0)
                {
                    if (oTransaction != null)
                    {
                        oTransaction.Dispose();
                        oTransaction = null;
                    }
                    oTransaction = ogloBilling.GetHCFATransactionDetails(TransactionID, _ClinicID);
                    GenerateH1500(oTransaction);
                }
                else
                {
                    if (oTransaction != null)
                    {
                        oTransaction.Dispose();
                        oTransaction = null;
                    }
                    oTransaction = new Transaction();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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

        #endregion " Form Load "

        #region " Button Click Events "

        private void tls_HCFA1500_Click(object sender, EventArgs e)
        {
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(_databaseconnectionstring, "");
           
            try
            {
                if (TransactionID > 0)
                {
                    if (oTransaction != null)
                    {
                        oTransaction.Dispose();
                        oTransaction = null;
                    }
                    oTransaction = ogloBilling.GetHCFATransactionDetails(TransactionID, _ClinicID);
                    GenerateH1500(oTransaction);
                }
                else
                {
                    if (oTransaction != null)
                    {
                        oTransaction.Dispose();
                        oTransaction = null;
                    }
                    oTransaction = new Transaction();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); 
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

        public void LoadTransaction()
        {
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(_databaseconnectionstring, "");
           
           // string strSQL = "";
            try
            {
                if (TransactionID > 0)
                {
                    if (oTransaction != null)
                    {
                        oTransaction.Dispose();
                        oTransaction = null;
                    }
                    oTransaction = ogloBilling.GetHCFATransactionDetails(TransactionID, _ClinicID);
                    GenerateH1500(oTransaction);

                    switch (ClaimValidationServiceType)
                    {
                        case ClaimValidationService.YOST:
                            ValidateClaim(oTransaction);
                            break;
                        case ClaimValidationService.Alpha2:
                            SendClaimForValidation();
                            break;
                        case ClaimValidationService.None:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (oTransaction != null)
                    {
                        oTransaction.Dispose();
                        oTransaction = null;
                    }
                    oTransaction = new Transaction();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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

        private void tsb_Send_Click(object sender, EventArgs e)
        {
            try
            {
                pnlResult.Visible = false;
                txtXMLReport.Visible = false;
               

                // create a new scrubber object - this is the web reference seen as 'CWCS_Client_Demo'
                AlphaIIWebService.EnterpriseServices Scrubber = new gloEDI.AlphaIIWebService.EnterpriseServices();

                string sData, sData1, rptType;
                rptType = "XML";
                // Make the call to the scrubber web service
                // it requires these parameters: 
                // System Type: LIVE
                // Submitter ID
                // User ID
                // User Password
                // Output Type: HTM | XML 
                // EDI Data (Formats include MEGAS XML, X12 837 X096/X098 4010A, VHA Flat File, NSF 3.X)
                //sData = Scrubber.Scrub(txSystem.Text.ToString(), txSubmitter.Text.ToString(), txUserID.Text.ToString(), txPassword.Text.ToString(), rptType.ToUpper(), EDIData.Text.ToString());
                sData = Scrubber.Scrub("Live", "glostream", "admin", "6632glostream", rptType.ToUpper(), txtEDIData.Text.ToString());
                // sData1 will hold the XML or HTML report
                if (rptType == "XML")
                {
                    txtXMLReport.Visible = true;
                    sData1 = sData.Replace("\n", "\r\n");
                    txtXMLReport.Text = sData1;
                    ExtractXML(sData1);
                }
                else if (rptType == "HTM")
                {
                    txtXMLReport.Visible = false;
                }
                else
                {
                    txtXMLReport.Visible = true;
                    sData1 = sData.Replace("\n", "\r\n");
                    txtXMLReport.Text = sData1;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }

        }

        private void ExtractXML(string  strXMLFile)
        {
            System.Xml.XmlDocument odoc = new System.Xml.XmlDocument();
            try
            {
                odoc.LoadXml(strXMLFile);
                XmlNodeList results = odoc.GetElementsByTagName("claimerror");
                System.Text.StringBuilder strError = new System.Text.StringBuilder();
                foreach (XmlNode node in results)
                {

                    strError.Append("Claim Error Code: "+ node.SelectSingleNode("claimerrorcode").InnerText);
                    strError.Append(System.Environment.NewLine);

                    strError.Append("Claim Error Subcode: "+ node.SelectSingleNode("claimerrorsubcode").InnerText);
                    strError.Append(System.Environment.NewLine);

                    strError.Append("Claim Error Message: " + node.SelectSingleNode("claimerrormsg").InnerText);
                    strError.Append(System.Environment.NewLine);

                    strError.Append("Claim Error Action: " + node.SelectSingleNode("claimerroraction").InnerText);
                    strError.Append(System.Environment.NewLine);

                    strError.Append("Claim Error Data: "+ node.SelectSingleNode("claimerrordata").InnerText);
                    strError.Append(System.Environment.NewLine);

                    strError.Append("Claim Error Category: " + node.SelectSingleNode("claimerrorcategory").InnerText);
                    strError.Append(System.Environment.NewLine);

                    strError.Append("Claim ANSI Reason: " + node.SelectSingleNode("claimansireason").InnerText);
                    strError.Append(System.Environment.NewLine);

                    if (strError.Length > 0)
                    {
                        //if (Directory.Exists(Application.StartupPath + "\\ClaimResult") == false)
                        //{
                        //    Directory.CreateDirectory(Application.StartupPath + "\\ClaimResult");
                        //}
                        if (Directory.Exists(appSettings["StartupPath"].ToString() + "\\ClaimResult") == false)
                        {
                            Directory.CreateDirectory(appSettings["StartupPath"].ToString() + "\\ClaimResult");
                        }

                        string _fileName = "ClaimResult " + DateTime.Now.Date.ToString("MM-dd-yyyy") + ".txt";
                        string _FilePath=appSettings["StartupPath"].ToString() + "\\ClaimResult\\" + _fileName;
                        //File.AppendAllText(_FilePath, strError.ToString());
                        System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                        oStreamWriter.WriteLine(strError.ToString());
                        oStreamWriter.Close();
                        oStreamWriter.Dispose();
                        System.Diagnostics.Process.Start(_FilePath);
                       
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                throw;
            }

            finally
            {
                if (odoc != null)
                {
                    odoc = null;
                }
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SendClaimForValidation()
        {
            try
            {
                pnlResult.Visible = false;
                txtXMLReport.Visible = false;
               // string strSQL = "";

                // create a new scrubber object - this is the web reference seen as 'CWCS_Client_Demo'
                AlphaIIWebService.EnterpriseServices Scrubber = new gloEDI.AlphaIIWebService.EnterpriseServices();

                string sData, sData1, rptType;
                rptType = "XML";
                // Make the call to the scrubber web service
                // it requires these parameters: 
                // System Type: LIVE
                // Submitter ID
                // User ID
                // User Password
                // Output Type: HTM | XML 
                // EDI Data (Formats include MEGAS XML, X12 837 X096/X098 4010A, VHA Flat File, NSF 3.X)
                //sData = Scrubber.Scrub(txSystem.Text.ToString(), txSubmitter.Text.ToString(), txUserID.Text.ToString(), txPassword.Text.ToString(), rptType.ToUpper(), EDIData.Text.ToString());
                sData = Scrubber.Scrub("Live", "glostream", "admin", "6632glostream", rptType.ToUpper(), txtEDIData.Text.ToString());
                // sData1 will hold the XML or HTML report
                if (rptType == "XML")
                {
                    txtXMLReport.Visible = true;
                    sData1 = sData.Replace("\n", "\r\n");
                    txtXMLReport.Text = sData1;
                    ExtractXML(sData1);
                }
                else if (rptType == "HTM")
                {
                    txtXMLReport.Visible = false;
                }
                else
                {
                    txtXMLReport.Visible = true;
                    sData1 = sData.Replace("\n", "\r\n");
                    txtXMLReport.Text = sData1;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }

        }

        #endregion " Button Click Events "

        #region " Private and Public Methods "

        private DataTable GetDistinctDiagnosis(Int64 TransactionID,Int64 ClinicID, Int64 ClaimNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dtDX = new DataTable();
            try
            {
                oDB.Connect(false);

                strSQL = " Select ISNULL(sDx1Code,'') AS DX from BL_Transaction_Lines " +
                         " WHERE (nTransactionID = " + TransactionID + ") AND (nClinicID = " + ClinicID + ") AND (nClaimNumber = " + ClaimNo + ") " +
                         " Union " +
                         " Select ISNULL(sDx2Code,'') AS DX from BL_Transaction_Lines  " +
                         " WHERE (nTransactionID = " + TransactionID + ") AND (nClinicID = " + ClinicID + ") AND (nClaimNumber = " + ClaimNo + ") " +
                         " Union  " +
                         " Select ISNULL(sDx3Code,'') AS DX from BL_Transaction_Lines " +
                         " WHERE (nTransactionID = " + TransactionID + ") AND (nClinicID = " + ClinicID + ") AND (nClaimNumber = " + ClaimNo + ") " +
                         " Union  " +
                         " Select ISNULL(sDx4Code,'') AS DX from BL_Transaction_Lines  " +
                         " WHERE (nTransactionID = " + TransactionID + ") AND (nClinicID = " + ClinicID + ") AND (nClaimNumber = " + ClaimNo + ") ";

                oDB.Retrive_Query(strSQL, out dtDX);
                if (dtDX != null)
                {
                    return dtDX;
                }
                return null;
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
        }

        public void FillInsurances(Int64 PatientID)
        {
            DataTable dtPatientInsurances = null;
            //gloEDI.gloBilling.gloBilling oglobilling = new gloEDI.gloBilling.gloBilling(_databaseconnectionstring, "");
            try
            {
                IsSecondaryInsurance = false;
                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                dtPatientInsurances = ogloPatient.getPatientInsurances(PatientID);
                ogloPatient.Dispose();
                ogloPatient=null;
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
                           // _SubscriberInsurancePST = "P";//Convert.ToString(dtPatientInsurances.Rows[0][""]);
                            _SubscriberGroupID = Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"]);
                            _PayerID = Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]);
                            _PayerAddress = Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress1"]);
                            _PayerCity = Convert.ToString(dtPatientInsurances.Rows[0]["PayerCity"]);
                            _PayerState = Convert.ToString(dtPatientInsurances.Rows[0]["PayerState"]);
                            _PayerZip = Convert.ToString(dtPatientInsurances.Rows[0]["PayerZip"]);
                            _SubscriberPhone = Convert.ToString(dtPatientInsurances.Rows[0]["sPhone"]);
                            //Anil Added on 20081030
                            _PriorAuthorizationNo = GetPriorAuthorizationNumber(_PatientID, Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"]));

                        }
                        else if (i == 1)
                        {
                            //Secondary Insurance
                            IsSecondaryInsurance = true;
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
                           // _OtherInsurancePST = "S"; //Convert.ToString(dtPatientInsurances.Rows[i][""]);
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

        private string GetPriorAuthorizationNumber(Int64 PatientID, Int64 InsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
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

        private void FillPatientInformation(Int64 PatientID)
        {
           // gloEDI.gloBilling.gloBilling oBill = new gloEDI.gloBilling.gloBilling(_databaseconnectionstring, "");
           
            //DataTable dt = new DataTable();
            //DataTable dtClinic = new DataTable();
            gloPatient.Patient oPatient = null;
         //   gloPatient.Referrals oReferral = new gloPatient.Referrals();
            try
            {
                //oPatient = ogloPatient.GetPatientDemo(PatientID);
                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
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
                    _PatientPhone = oPatient.DemographicsDetail.PatientPhone;
                    gloPatient.Referrals oReferral;
                    oReferral = oPatient.Referrals;
                    if (oReferral.Count > 0)
                    {
                        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
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

        private void FillSubmitterInfo(Int64 _SelectedClinicId, Int64 _nProviderID)
        {
            gloBilling.gloBilling oBill = new gloBilling.gloBilling(_databaseconnectionstring, "");
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
                    //_SubmitterETIN = "C0923";

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

        private void FillProviderDetails(long _SelectedProviderId, ProviderType _ProviderType)
        {
            Resource oResource = new Resource(_databaseconnectionstring);
            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
           // DataTable dtProviderDetails = null;
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
                                oSettings.GetSetting("BillingSetting", _SelectedProviderId, _ClinicID, out _objResult);
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
                                            _BillingPhone = _Provider.BMPhone;
                                        } break;
                                    case "Practice":
                                        {
                                            _BillingAddress = _Provider.BPracAddress1;
                                            _BillingCity = _Provider.BPracCity;
                                            _BillingState = _Provider.BPracState;
                                            _BillingZIP = _Provider.BPracZIP;
                                            _BillingPhone = _Provider.BPracPhone;
                                        } break;
                                    case "Company":
                                        {
                                            _BillingAddress = _Provider.CompanyAddress1;
                                            _BillingCity = _Provider.CompanyCity;
                                            _BillingState = _Provider.CompanyState;
                                            _BillingZIP = _Provider.CompanyZip;
                                            _BillingPhone = _Provider.CompanyPhone;
                                        } break;
                                    default:
                                        _BillingAddress = _Provider.BMAddress1;
                                        _BillingCity = _Provider.BMCity;
                                        _BillingState = _Provider.BMState;
                                        _BillingZIP = _Provider.BMZIP;
                                        _BillingPhone = _Provider.BMPhone;
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
                                _ReferralId = Convert.ToString(_Provider.ProviderID);
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
                                oSettings.GetSetting("RenderingSetting", _SelectedProviderId, _ClinicID, out _objResult);
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

        private bool ValidateData()
        {
            string strDataMissing = "";
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(_databaseconnectionstring, "");
            TransactionDetails oTranDetails = null;
            //TransactionLine oTransLine = null;
            
            try
            {

                if (oTransaction != null)
                {
                    if (oTransaction.Lines.Count > 0)
                    {
                        FillInsurances(oTransaction.PatientID);
                        FillSubmitterInfo(oTransaction.ClinicID, oTransaction.ProviderID);
                        oTranDetails = oTransaction.Transaction_Details;

                        if (_SubscriberAddress.Trim() == "")
                        {
                            strDataMissing += " Subscriber Address";
                        }
                        if (_SubscriberCity.Trim() == "")
                        {
                            strDataMissing += " Subscriber City";
                        }
                        if (_SubscriberState.Trim() == "")
                        {
                            strDataMissing += " Subscriber State";
                        }

                        if (_SubscriberDOB.Trim() == "")
                        {

                        }
                        if (_SubscriberFName.Trim() == "")
                        {

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }

            return true;
        }
        
        public void GenerateH1500(Transaction oTransaction)
        {
            H1500 form = new H1500();
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(_databaseconnectionstring, "");
            TransactionDetails oTranDetails = null;
            //TransactionLine oTransLine = null;
            Decimal TotalCharges = 0;
            Decimal PaidAmount = 0;
            
            try
            {
               
                if (oTransaction != null)
                {
                    if (oTransaction.Lines.Count > 0)
                    {
                        FillInsurances(oTransaction.PatientID);
                        FillSubmitterInfo(oTransaction.ClinicID, oTransaction.ProviderID);

                        oTranDetails = oTransaction.Transaction_Details;

                        form.StartEncounters(); // Start the encounters (Submitter ID)
                        form.StartEncounter(1);	// Start Encounter/Claim 1

                        // Set the XML data elements.
                        #region Type of Insurance

                        if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "MB")
                        {
                            form.set_Element(1, "medicare", "1");
                        }
                        else if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "MC")
                        {
                            form.set_Element(1, "medicaid", "1");
                        }
                        else if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "CH")
                        {
                            form.set_Element(1, "champus", "1");
                        }
                        else if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "CI" || _SubscriberInsuranceBelongs.Trim().ToUpper() == "HM")
                        {
                            form.set_Element(1, "group", "1");
                        }
                        else
                        {
                            form.set_Element(1, "oth", "1");
                        }

                        //form.set_Element(1, "feca", "1");

                        form.set_Element(1, "ins-id", _SubscriberInsuranceID.Trim());
                        form.set_Element(1, "auto", "");
                        form.set_Element(1, "wc", "");
                        form.set_Element(1, "fbcs", "");

                        #endregion Type of Insurance

                        #region Patient Demographics

                        form.set_Element(2, "last", oTranDetails.HCFA_PatientLName.Trim());
                        form.set_Element(2, "first", oTranDetails.HCFA_PatientFName.Trim());
                        form.set_Element(2, "middle", oTranDetails.HCFA_PatientMName.Trim());
                        if (oTranDetails.HCFA_PatientDOB > 0)
                        {
                            DateTime PatientDOB = gloDateMaster.gloDate.DateAsDate(oTranDetails.HCFA_PatientDOB);

                            form.set_Element(3, "m", PatientDOB.Month.ToString());
                            form.set_Element(3, "d", PatientDOB.Day.ToString());
                            form.set_Element(3, "y",PatientDOB.Year.ToString().Trim().Substring(2,2));
                        }
                        else
                        {
                            form.set_Element(3, "m", "");
                            form.set_Element(3, "d", "");
                            form.set_Element(3, "y", "");
                        }

                        if (oTranDetails.HCFA_PatientGender.Trim() != "")
                        {
                            if (oTranDetails.HCFA_PatientGender.ToUpper().Substring(0, 1) == "M")
                            {
                                form.set_Element(3, "sex-m", "1");
                                form.set_Element(3, "sex-f", "");
                            }
                            else if (oTranDetails.HCFA_PatientGender.ToUpper().Substring(0, 1) == "F")
                            {
                                form.set_Element(3, "sex-m", "");
                                form.set_Element(3, "sex-f", "1");
                            }
                        }
                        else
                        {
                            form.set_Element(3, "sex-m", "");
                            form.set_Element(3, "sex-f", "");
                        }

                        #endregion  Patient Demographics

                        #region Insured's Name

                        form.set_Element(4, "last", _SubscriberLName.Trim());
                        form.set_Element(4, "first", _SubscriberFName.Trim());
                        form.set_Element(4, "middle", _SubscriberMName.Trim());

                        #endregion Insured's Name

                        #region Patient's Address

                        form.set_Element(5, "addr", oTranDetails.HCFA_PatientAddress1.Trim());
                        form.set_Element(5, "city", oTranDetails.HCFA_PatientCity.Trim());
                        form.set_Element(5, "state", oTranDetails.HCFA_PatientState.Trim());
                        form.set_Element(5, "zip", oTranDetails.HCFA_PatientZip.Trim());
                        form.set_Element(5, "phone", oTranDetails.HCFA_PatientPhone.Trim());

                        #endregion Patient's Address

                        #region Patient Relationship to Insured

                        if (_SubscriberRelationshipCode == "01")//01=Spouse
                        {
                            form.set_Element(6, "self", "");
                            form.set_Element(6, "spouse", "1");
                            form.set_Element(6, "child", "");
                            form.set_Element(6, "other", "");

                        }
                        else if (_SubscriberRelationshipCode == "18")//18=Self
                        {
                            form.set_Element(6, "self", "1");
                            form.set_Element(6, "spouse", "");
                            form.set_Element(6, "child", "");
                            form.set_Element(6, "other", "");

                        }
                        else if (_SubscriberRelationshipCode == "19")//19=Child
                        {
                            form.set_Element(6, "self", "");
                            form.set_Element(6, "spouse", "");
                            form.set_Element(6, "child", "1");
                            form.set_Element(6, "other", "");

                        }
                        else
                        {
                            form.set_Element(6, "self", "");
                            form.set_Element(6, "spouse", "");
                            form.set_Element(6, "child", "");
                            form.set_Element(6, "other", "1");

                        }

                        #endregion Patient Relationship to Insured

                        #region Insured's Address

                        form.set_Element(7, "addr", _SubscriberAddress.Trim());
                        form.set_Element(7, "city", _SubscriberCity.Trim());
                        form.set_Element(7, "state", _SubscriberState.Trim());
                        form.set_Element(7, "zip", _SubscriberZIP.Trim());
                        form.set_Element(7, "phone", _SubscriberPhone.Trim());

                        #endregion Insured's Address

                        #region Patient Information
                        if (oTransaction.MaritalStatus == "Single")
                        {
                            form.set_Element(8, "single", "1");
                            form.set_Element(8, "married", "");
                            form.set_Element(8, "other", "");
                        }
                        else if (oTransaction.MaritalStatus == "Married")
                        {
                            form.set_Element(8, "single", "");
                            form.set_Element(8, "married", "1");
                            form.set_Element(8, "other", "");
                        }
                        else
                        {
                            form.set_Element(8, "single", "");
                            form.set_Element(8, "married", "");
                            form.set_Element(8, "other", "1");
                        }

                        form.set_Element(8, "employed", "");
                        form.set_Element(8, "full-time", "");
                        form.set_Element(8, "part-time", "");

                        #endregion Patient Information

                        #region Other Insurance Details

                        form.set_Element(9, "last", _OtherInsuranceSubscriberLName.Trim());
                        form.set_Element(9, "first", _OtherInsuranceSubscriberFName.Trim());
                        form.set_Element(9, "middle", _OtherInsuranceSubscriberMName.Trim());
                        if (_OtherInsuranceID.Trim() != "")
                        {
                            if (_OtherInsuranceGroupID.Trim() != "")
                            {
                                form.set_Element(9, "policy", _OtherInsuranceGroupID.Trim() + "-" + _OtherInsuranceID.Trim());
                            }
                            else
                            {
                                form.set_Element(9, "policy", _OtherInsuranceID.Trim());
                            }
                           
                        }
                        else
                        {
                            form.set_Element(9, "policy", _OtherInsuranceGroupID.Trim());
                        }


                        if (_OtherInsuranceSubscriberDOB.Trim() != "")
                        {
                            DateTime dtOtherInsuredDOB = Convert.ToDateTime(_OtherInsuranceSubscriberDOB);
                            form.set_Element(9, "dobm", dtOtherInsuredDOB.Month.ToString());
                            form.set_Element(9, "dobd", dtOtherInsuredDOB.Day.ToString());
                            form.set_Element(9, "doby", dtOtherInsuredDOB.Year.ToString().Trim().Substring(2, 2));
                        }
                        else
                        {
                            form.set_Element(9, "dobm", "");
                            form.set_Element(9, "dobd", "");
                            form.set_Element(9, "doby", "");
                        }
                        //Gender for Other Insurance Subscriber
                        if (_OtherInsuranceSubscriberGender.Trim() != "")
                        {
                            if (_OtherInsuranceSubscriberGender.ToUpper().Trim().Substring(0, 1) == "M")
                            {
                                form.set_Element(9, "sex-m", "1");
                                form.set_Element(9, "sex-f", "");
                            }
                            else if (_OtherInsuranceSubscriberGender.ToUpper().Trim().Substring(0, 1) == "F")
                            {
                                form.set_Element(9, "sex-m", "");
                                form.set_Element(9, "sex-f", "1");
                            }
                            else
                            {
                                form.set_Element(9, "sex-m", "");
                                form.set_Element(9, "sex-f", "");
                            }
                        }
                        else
                        {
                            form.set_Element(9, "sex-m", "");
                            form.set_Element(9, "sex-f", "");
                        }

                        #endregion Other Insurance Details

                        #region Patient Condition Related To

                        if (oTransaction.WorkersComp == true)
                        {
                            form.set_Element(10, "employ-yes", "1");
                            form.set_Element(10, "employ-no", "");
                        }
                        else
                        {
                            form.set_Element(10, "employ-yes", "");
                            form.set_Element(10, "employ-no", "1");
                        }

                        if (oTransaction.AutoClaim == true)
                        {
                            form.set_Element(10, "auto-yes", "1");
                            form.set_Element(10, "auto-no", "");
                            form.set_Element(10, "auto-state", oTransaction.State.Trim());
                        }
                        else
                        {
                            form.set_Element(10, "auto-yes", "");
                            form.set_Element(10, "auto-no", "1");
                            form.set_Element(10, "auto-state", "");
                        }
                        form.set_Element(10, "oth-yes", "");
                        form.set_Element(10, "oth-no", "");

                        #endregion Patient Condition Related To

                        #region Insurance Details

                        form.set_Element(11, "policy", _SubscriberGroupID.Trim());

                        if (_SubscriberDOB.Trim() != "")
                        {
                            DateTime dtInsuredDOB = Convert.ToDateTime(_SubscriberDOB.Trim());
                            form.set_Element(11, "m", dtInsuredDOB.Month.ToString());
                            form.set_Element(11, "d", dtInsuredDOB.Day.ToString());
                            form.set_Element(11, "y", dtInsuredDOB.Year.ToString().Trim().Substring(2, 2));
                        }
                        else
                        {
                            form.set_Element(11, "m", "");
                            form.set_Element(11, "d", "");
                            form.set_Element(11, "y", "");
                        }
                        if (_SubscriberGender.Trim() != "")
                        {
                            if (_SubscriberGender.ToUpper().Trim().Substring(0, 1) == "M")
                            {
                                form.set_Element(11, "sex-m", "1");
                                form.set_Element(11, "sex-f", "");
                            }
                            else if (_SubscriberGender.ToUpper().Trim().Substring(0, 1) == "F")
                            {
                                form.set_Element(11, "sex-m", "");
                                form.set_Element(11, "sex-f", "1");
                            }
                            else
                            {
                                form.set_Element(11, "sex-m", "");
                                form.set_Element(11, "sex-f", "");
                            }
                        }
                        else
                        {
                            form.set_Element(11, "sex-m", "");
                            form.set_Element(11, "sex-f", "");
                        }

                        form.set_Element(11, "emp-name", "");
                        form.set_Element(11, "plan-name", _PayerName.Trim());
                        if (IsSecondaryInsurance == true)
                        {
                            form.set_Element(11, "oth-yes", "1");
                            form.set_Element(11, "oth-no", "");
                        }
                        else
                        {
                            form.set_Element(11, "oth-yes", "");
                            form.set_Element(11, "oth-no", "1");
                        }

                        #endregion Insurance Details

                        #region Patient Signature and Date

                        form.set_Element(12, "signed", "Signature on File");
                        form.set_Element(12, "m", DateTime.Now.Month.ToString());
                        form.set_Element(12, "d", DateTime.Now.Day.ToString());
                        form.set_Element(12, "y", DateTime.Now.Year.ToString().Trim().Substring(2, 2));

                        #endregion Patient Signature and Date

                        #region Insured's Signature

                        form.set_Element(13, "signed", "Signature on File");

                        #endregion Insured's Signature

                        #region Current illness/Injury Date

                        if (oTransaction.WorkersComp == true)
                        {
                            if (oTransaction.InjuryDate > 0)
                            {
                                DateTime dtInjuryDate = gloDateMaster.gloDate.DateAsDate(oTransaction.InjuryDate);
                                form.set_Element(14, "m", dtInjuryDate.Month.ToString());
                                form.set_Element(14, "d", dtInjuryDate.Day.ToString());
                                form.set_Element(14, "y", dtInjuryDate.Year.ToString().Trim().Substring(2, 2));
                            }
                            else
                            {
                                form.set_Element(14, "m", "");
                                form.set_Element(14, "d", "");
                                form.set_Element(14, "y", "");
                            }
                        }
                        else
                        {
                            if (oTransaction.OnsiteDate > 0)
                            {
                                DateTime dtOnsiteDate = gloDateMaster.gloDate.DateAsDate(oTransaction.OnsiteDate);
                                form.set_Element(14, "m", dtOnsiteDate.Month.ToString());
                                form.set_Element(14, "d", dtOnsiteDate.Day.ToString());
                                form.set_Element(14, "y", dtOnsiteDate.Year.ToString().Trim().Substring(2, 2));
                            }
                            else
                            {
                                form.set_Element(14, "m", "");
                                form.set_Element(14, "d", "");
                                form.set_Element(14, "y", "");
                            }
                        }


                        form.set_Element(15, "m", "");
                        form.set_Element(15, "d", "");
                        form.set_Element(15, "y", "");

                        #endregion Current illness/Injury Date

                        #region Unable to Work Dates

                        if (oTransaction.UnableToWorkFromDate > 0 && oTransaction.UnableToWorkTillDate > 0)
                        {
                            DateTime dtUnableToWorkFrom = gloDateMaster.gloDate.DateAsDate(oTransaction.UnableToWorkFromDate);
                            DateTime dtUnableToWorkTo = gloDateMaster.gloDate.DateAsDate(oTransaction.UnableToWorkTillDate);
                            form.set_Element(16, "fm", dtUnableToWorkFrom.Month.ToString());
                            form.set_Element(16, "fd", dtUnableToWorkFrom.Day.ToString());
                            form.set_Element(16, "fy", dtUnableToWorkFrom.Year.ToString().Trim().Substring(2, 2));
                            form.set_Element(16, "tm", dtUnableToWorkTo.Month.ToString());
                            form.set_Element(16, "td", dtUnableToWorkTo.Day.ToString());
                            form.set_Element(16, "ty", dtUnableToWorkTo.Year.ToString().Trim().Substring(2, 2));
                        }
                        else
                        {
                            form.set_Element(16, "fm", "");
                            form.set_Element(16, "fd", "");
                            form.set_Element(16, "fy", "");
                            form.set_Element(16, "tm", "");
                            form.set_Element(16, "td", "");
                            form.set_Element(16, "ty", "");
                        }

                        #endregion Unable to Work Dates

                        #region Referral Physician Name and NPI
                        GetReferralProvider(oTransaction.PatientID);
                        form.set_Element(17, "name", _ReferralFName.Trim() + " " + _ReferralMName.Trim() + " " + _ReferralLName.Trim());
                        form.set_Element(17, "id-qual", "");
                        form.set_Element(17, "id-num", "");
                        form.set_Element(17, "npi", _ReferralNPI.Trim());

                        #endregion Referral Physician Name and NPI

                        #region Hospitalization Dates

                        if (oTransaction.HospitalizationDateFrom > 0 && oTransaction.HospitalizationDateTo > 0)
                        {
                            DateTime dtHospitalisationFrom = gloDateMaster.gloDate.DateAsDate(oTransaction.HospitalizationDateFrom);
                            DateTime dtHospitalisationTo = gloDateMaster.gloDate.DateAsDate(oTransaction.HospitalizationDateTo);
                            form.set_Element(18, "fm", dtHospitalisationFrom.Month.ToString());
                            form.set_Element(18, "fd", dtHospitalisationFrom.Day.ToString());
                            form.set_Element(18, "fy", dtHospitalisationFrom.Year.ToString().Trim().Substring(2, 2));
                            form.set_Element(18, "tm", dtHospitalisationTo.Month.ToString());
                            form.set_Element(18, "td", dtHospitalisationTo.Day.ToString());
                            form.set_Element(18, "ty", dtHospitalisationTo.Year.ToString().Trim().Substring(2, 2));
                        }
                        else
                        {
                            form.set_Element(18, "fm", "");
                            form.set_Element(18, "fd", "");
                            form.set_Element(18, "fy", "");
                            form.set_Element(18, "tm", "");
                            form.set_Element(18, "td", "");
                            form.set_Element(18, "ty", "");
                        }

                        #endregion Hospitalization Dates

                        #region Not Used Area
                        form.set_Element(19, "area1", "");
                        form.set_Element(19, "area2", "");
                        #endregion Not Used Area

                        #region Outside Lab Charges

                        if (oTransaction.OutSideLab == true)
                        {
                            form.set_Element(20, "out-yes", "1");
                            form.set_Element(20, "out-no", "");
                            form.set_Element(20, "charges", Convert.ToString(oTransaction.OutSideLabCharges));
                        }
                        else
                        {
                            form.set_Element(20, "out-yes", "");
                            form.set_Element(20, "out-no", "1");
                            form.set_Element(20, "charges", "");
                        }

                        #endregion Outside Lab Charges


                        #region Diagnosis

                        DataTable dtDx = new DataTable();
                        dtDx = GetDistinctDiagnosis(oTransaction.TransactionID, oTransaction.ClinicID, oTransaction.ClaimNo);
                        if (dtDx != null && dtDx.Rows.Count > 0)
                        {
                            for (int k = 0; k < dtDx.Rows.Count; k++)
                            {
                              
                                string Dx = Convert.ToString(dtDx.Rows[k]["DX"]);
                                if (Dx != "")
                                {
                                    if (k == 1)
                                    {
                                        form.set_Element(21, "dx1", Dx);
                                    }
                                    else if (k == 2)
                                    {
                                        form.set_Element(21, "dx2", Dx);
                                    }
                                    else if (k == 3)
                                    {
                                        form.set_Element(21, "dx3", Dx);
                                    }
                                    else if (k == 4)
                                    {
                                        form.set_Element(21, "dx4", Dx);
                                    }
                                }
                            }
                        }

                        #endregion Diagnosis

                        #region Medicaid Resubmission Code

                        form.set_Element(22, "resub", "");
                        form.set_Element(22, "org", "");

                        #endregion Medicaid Resubmission Code

                        #region Prior Authorisation

                        form.set_Element(23, "prior", GetPriorAuthorizationNumber(oTransaction.PatientID, oTransaction.Lines[0].InsuranceID).Trim());

                        #endregion Prior Authorisation

                        for (int i = 0; i < oTransaction.Lines.Count; i++)
                        {
                            #region Transaction Line 1
                            if (i == 0)
                            {
                                // Line Item # 1
                                form.set_LineItemElement(1, "fm", oTransaction.Lines[i].DateServiceFrom.Month.ToString());
                                form.set_LineItemElement(1, "fd", oTransaction.Lines[i].DateServiceFrom.Day.ToString());
                                form.set_LineItemElement(1, "fy", oTransaction.Lines[i].DateServiceFrom.Year.ToString().Trim().Substring(2, 2));
                                form.set_LineItemElement(1, "tm", oTransaction.Lines[i].DateServiceTill.Month.ToString());
                                form.set_LineItemElement(1, "td", oTransaction.Lines[i].DateServiceTill.Day.ToString());
                                form.set_LineItemElement(1, "ty", oTransaction.Lines[i].DateServiceTill.Year.ToString().Trim().Substring(2, 2));
                                form.set_LineItemElement(1, "pos", Convert.ToString(oTransaction.Lines[i].POSCode));
                                form.set_LineItemElement(1, "tos", Convert.ToString(oTransaction.Lines[i].TOSCode));
                                form.set_LineItemElement(1, "cpt", Convert.ToString(oTransaction.Lines[i].CPTCode));
                                form.set_LineItemElement(1, "m1", Convert.ToString(oTransaction.Lines[i].Mod1Code));
                                form.set_LineItemElement(1, "m2", Convert.ToString(oTransaction.Lines[i].Mod2Code));
                                form.set_LineItemElement(1, "m3", Convert.ToString(oTransaction.Lines[i].Mod3Code));
                                form.set_LineItemElement(1, "m4", Convert.ToString(oTransaction.Lines[i].Mod4Code));
                                if (oTransaction.Lines[i].Dx1Ptr == true)
                                {
                                    form.set_LineItemElement(1, "ptr", "1");
                                }
                                else
                                {
                                    form.set_LineItemElement(1, "ptr", "");
                                }
                                form.set_LineItemElement(1, "charge", Convert.ToString(oTransaction.Lines[i].Charges));
                                form.set_LineItemElement(1, "unit", Convert.ToString(oTransaction.Lines[i].Unit));
                            }
                            #endregion Transaction Line 1

                            #region Transaction Line 2
                            if (i == 1)
                            {
                                // Line Item # 2
                                form.set_LineItemElement(2, "fm", oTransaction.Lines[i].DateServiceFrom.Month.ToString());
                                form.set_LineItemElement(2, "fd", oTransaction.Lines[i].DateServiceFrom.Day.ToString());
                                form.set_LineItemElement(2, "fy", oTransaction.Lines[i].DateServiceFrom.Year.ToString().Trim().Substring(2, 2));
                                form.set_LineItemElement(2, "tm", oTransaction.Lines[i].DateServiceTill.Month.ToString());
                                form.set_LineItemElement(2, "td", oTransaction.Lines[i].DateServiceTill.Day.ToString());
                                form.set_LineItemElement(2, "ty", oTransaction.Lines[i].DateServiceTill.Year.ToString().Trim().Substring(2, 2));
                                form.set_LineItemElement(2, "pos", Convert.ToString(oTransaction.Lines[i].POSCode));
                                form.set_LineItemElement(2, "tos", Convert.ToString(oTransaction.Lines[i].TOSCode));
                                form.set_LineItemElement(2, "cpt", Convert.ToString(oTransaction.Lines[i].CPTCode));
                                form.set_LineItemElement(2, "m1", Convert.ToString(oTransaction.Lines[i].Mod1Code));
                                form.set_LineItemElement(2, "m2", Convert.ToString(oTransaction.Lines[i].Mod2Code));
                                form.set_LineItemElement(2, "m3", Convert.ToString(oTransaction.Lines[i].Mod3Code));
                                form.set_LineItemElement(2, "m4", Convert.ToString(oTransaction.Lines[i].Mod4Code));
                                if (oTransaction.Lines[i].Dx2Ptr == true)
                                {
                                    form.set_LineItemElement(2, "ptr", "1");
                                }
                                else
                                {
                                    form.set_LineItemElement(2, "ptr", "");
                                }
                                form.set_LineItemElement(2, "charge", Convert.ToString(oTransaction.Lines[i].Charges));
                                form.set_LineItemElement(2, "unit", Convert.ToString(oTransaction.Lines[i].Unit));
                            }
                            #endregion Transaction Line 2

                            #region Transaction Line 3

                            if (i == 2)
                            {
                                // Line Item # 3
                                form.set_LineItemElement(3, "fm", oTransaction.Lines[i].DateServiceFrom.Month.ToString());
                                form.set_LineItemElement(3, "fd", oTransaction.Lines[i].DateServiceFrom.Day.ToString());
                                form.set_LineItemElement(3, "fy", oTransaction.Lines[i].DateServiceFrom.Year.ToString().Trim().Substring(2, 2));
                                form.set_LineItemElement(3, "tm", oTransaction.Lines[i].DateServiceTill.Month.ToString());
                                form.set_LineItemElement(3, "td", oTransaction.Lines[i].DateServiceTill.Day.ToString());
                                form.set_LineItemElement(3, "ty", oTransaction.Lines[i].DateServiceTill.Year.ToString().Trim().Substring(2, 2));
                                form.set_LineItemElement(3, "pos", Convert.ToString(oTransaction.Lines[i].POSCode));
                                form.set_LineItemElement(3, "tos", Convert.ToString(oTransaction.Lines[i].TOSCode));
                                form.set_LineItemElement(3, "cpt", Convert.ToString(oTransaction.Lines[i].CPTCode));
                                form.set_LineItemElement(3, "m1", Convert.ToString(oTransaction.Lines[i].Mod1Code));
                                form.set_LineItemElement(3, "m2", Convert.ToString(oTransaction.Lines[i].Mod2Code));
                                form.set_LineItemElement(3, "m3", Convert.ToString(oTransaction.Lines[i].Mod3Code));
                                form.set_LineItemElement(3, "m4", Convert.ToString(oTransaction.Lines[i].Mod4Code));
                                if (oTransaction.Lines[i].Dx3Ptr == true)
                                {
                                    form.set_LineItemElement(3, "ptr", "1");
                                }
                                else
                                {
                                    form.set_LineItemElement(3, "ptr", "");
                                }
                                form.set_LineItemElement(3, "charge", Convert.ToString(oTransaction.Lines[i].Charges));
                                form.set_LineItemElement(3, "unit", Convert.ToString(oTransaction.Lines[i].Unit));
                            }

                            #endregion Transaction Line 3

                            #region Transaction Line 4

                            if (i == 3)
                            {
                                // Line Item # 4
                                form.set_LineItemElement(4, "fm", oTransaction.Lines[i].DateServiceFrom.Month.ToString());
                                form.set_LineItemElement(4, "fd", oTransaction.Lines[i].DateServiceFrom.Day.ToString());
                                form.set_LineItemElement(4, "fy", oTransaction.Lines[i].DateServiceFrom.Year.ToString().Trim().Substring(2, 2));
                                form.set_LineItemElement(4, "tm", oTransaction.Lines[i].DateServiceTill.Month.ToString());
                                form.set_LineItemElement(4, "td", oTransaction.Lines[i].DateServiceTill.Day.ToString());
                                form.set_LineItemElement(4, "ty", oTransaction.Lines[i].DateServiceTill.Year.ToString().Trim().Substring(2, 2));
                                form.set_LineItemElement(4, "pos", Convert.ToString(oTransaction.Lines[i].POSCode));
                                form.set_LineItemElement(4, "tos", Convert.ToString(oTransaction.Lines[i].TOSCode));
                                form.set_LineItemElement(4, "cpt", Convert.ToString(oTransaction.Lines[i].CPTCode));
                                form.set_LineItemElement(4, "m1", Convert.ToString(oTransaction.Lines[i].Mod1Code));
                                form.set_LineItemElement(4, "m2", Convert.ToString(oTransaction.Lines[i].Mod2Code));
                                form.set_LineItemElement(4, "m3", Convert.ToString(oTransaction.Lines[i].Mod3Code));
                                form.set_LineItemElement(4, "m4", Convert.ToString(oTransaction.Lines[i].Mod4Code));
                                if (oTransaction.Lines[i].Dx4Ptr == true)
                                {
                                    form.set_LineItemElement(4, "ptr", "1");
                                }
                                else
                                {
                                    form.set_LineItemElement(4, "ptr", "");
                                }
                                form.set_LineItemElement(4, "charge", Convert.ToString(oTransaction.Lines[i].Charges));
                                form.set_LineItemElement(4, "unit", Convert.ToString(oTransaction.Lines[i].Unit));
                            }
                            #endregion Transaction Line 4

                            #region Transaction Line 5
                            if (i == 4)
                            {
                                // Line Item # 5
                                form.set_LineItemElement(5, "fm", oTransaction.Lines[i].DateServiceFrom.Month.ToString());
                                form.set_LineItemElement(5, "fd", oTransaction.Lines[i].DateServiceFrom.Day.ToString());
                                form.set_LineItemElement(5, "fy", oTransaction.Lines[i].DateServiceFrom.Year.ToString().Trim().Substring(2, 2));
                                form.set_LineItemElement(5, "tm", oTransaction.Lines[i].DateServiceTill.Month.ToString());
                                form.set_LineItemElement(5, "td", oTransaction.Lines[i].DateServiceTill.Day.ToString());
                                form.set_LineItemElement(5, "ty", oTransaction.Lines[i].DateServiceTill.Year.ToString().Trim().Substring(2, 2));
                                form.set_LineItemElement(5, "pos", Convert.ToString(oTransaction.Lines[i].POSCode));
                                form.set_LineItemElement(5, "tos", Convert.ToString(oTransaction.Lines[i].TOSCode));
                                form.set_LineItemElement(5, "cpt", Convert.ToString(oTransaction.Lines[i].CPTCode));
                                form.set_LineItemElement(5, "m1", Convert.ToString(oTransaction.Lines[i].Mod1Code));
                                form.set_LineItemElement(5, "m2", Convert.ToString(oTransaction.Lines[i].Mod2Code));
                                form.set_LineItemElement(5, "m3", Convert.ToString(oTransaction.Lines[i].Mod3Code));
                                form.set_LineItemElement(5, "m4", Convert.ToString(oTransaction.Lines[i].Mod4Code));
                                if (oTransaction.Lines[i].Dx5Ptr == true)
                                {
                                    form.set_LineItemElement(5, "ptr", "1");
                                }
                                else
                                {
                                    form.set_LineItemElement(5, "ptr", "");
                                }
                                form.set_LineItemElement(5, "charge", Convert.ToString(oTransaction.Lines[i].Charges));
                                form.set_LineItemElement(5, "unit", Convert.ToString(oTransaction.Lines[i].Unit));
                            }
                            #endregion Transaction Line 1

                            #region Transaction Line 6
                            if (i == 5)
                            {
                                // Line Item # 6
                                form.set_LineItemElement(6, "fm", oTransaction.Lines[i].DateServiceFrom.Month.ToString());
                                form.set_LineItemElement(6, "fd", oTransaction.Lines[i].DateServiceFrom.Day.ToString());
                                form.set_LineItemElement(6, "fy", oTransaction.Lines[i].DateServiceFrom.Year.ToString().Trim().Substring(2, 2));
                                form.set_LineItemElement(6, "tm", oTransaction.Lines[i].DateServiceTill.Month.ToString());
                                form.set_LineItemElement(6, "td", oTransaction.Lines[i].DateServiceTill.Day.ToString());
                                form.set_LineItemElement(6, "ty", oTransaction.Lines[i].DateServiceTill.Year.ToString().Trim().Substring(2, 2));
                                form.set_LineItemElement(6, "pos", Convert.ToString(oTransaction.Lines[i].POSCode));
                                form.set_LineItemElement(6, "tos", Convert.ToString(oTransaction.Lines[i].TOSCode));
                                form.set_LineItemElement(6, "cpt", Convert.ToString(oTransaction.Lines[i].CPTCode));
                                form.set_LineItemElement(6, "m1", Convert.ToString(oTransaction.Lines[i].Mod1Code));
                                form.set_LineItemElement(6, "m2", Convert.ToString(oTransaction.Lines[i].Mod2Code));
                                form.set_LineItemElement(6, "m3", Convert.ToString(oTransaction.Lines[i].Mod3Code));
                                form.set_LineItemElement(6, "m4", Convert.ToString(oTransaction.Lines[i].Mod4Code));
                                if (oTransaction.Lines[i].Dx6Ptr == true)
                                {
                                    form.set_LineItemElement(6, "ptr", "1");
                                }
                                else
                                {
                                    form.set_LineItemElement(6, "ptr", "");
                                }
                                form.set_LineItemElement(6, "charge", Convert.ToString(oTransaction.Lines[i].Charges));
                                form.set_LineItemElement(6, "unit", Convert.ToString(oTransaction.Lines[i].Unit));
                            }

                            #endregion Transaction Line 6

                            TotalCharges += oTransaction.Lines[i].Total;
                            PaidAmount = 0;
                        }

                        #region Provider Tax ID

                        if (oTranDetails.HCFA_ProviderEIN.Trim() != "")
                        {
                            form.set_Element(25, "taxid", oTranDetails.HCFA_ProviderEIN.Trim());

                            form.set_Element(25, "ssn", "");
                            form.set_Element(25, "ein", "1");
                        }
                        else if (oTranDetails.HCFA_ProviderSSN.Trim() != "")
                        {
                            form.set_Element(25, "taxid", oTranDetails.HCFA_ProviderSSN.Trim());
                            form.set_Element(25, "ssn", "1");
                            form.set_Element(25, "ein", "");
                        }

                        #endregion Provider Tax ID

                        #region Patient Account No

                        form.set_Element(26, "acct-num", oTranDetails.HCFA_PatientCode.Trim());

                        #endregion Patient Account No

                        #region Assignment of Benefits

                        form.set_Element(27, "asg-yes", "1");
                        form.set_Element(27, "asg-no", "");

                        #endregion Assignment of Benefits

                        #region Total
                        form.set_Element(28, "total", Convert.ToString(TotalCharges));
                        #endregion Total

                        #region Paid Amount
                        form.set_Element(29, "paid", Convert.ToString(PaidAmount));
                        #endregion  Paid Amount

                        #region Balance Due
                        form.set_Element(30, "due", Convert.ToString(TotalCharges - PaidAmount));
                        #endregion Balance Due

                        #region Provider Sign, Name and Date

                        form.set_Element(31, "signed", "Signature on File");
                        form.set_Element(31, "m", DateTime.Now.Month.ToString());
                        form.set_Element(31, "d", DateTime.Now.Day.ToString());
                        form.set_Element(31, "y", DateTime.Now.Year.ToString().Trim().Substring(2, 2));
                        form.set_Element(31, "name", oTranDetails.HCFA_ProviderFName.Trim() + " " + oTranDetails.HCFA_ProviderLName.Trim());

                        #endregion Provider Sign, Name and Date

                        #region Facility Details

                        form.set_Element(32, "name", oTranDetails.HCFA_FacilityName.Trim());
                        form.set_Element(32, "addr", oTranDetails.HCFA_FacilityAddress1.Trim());
                        form.set_Element(32, "city", oTranDetails.HCFA_FacilityCity.Trim());
                        form.set_Element(32, "state", oTranDetails.HCFA_FacilityState.Trim());
                        form.set_Element(32, "zip", oTranDetails.HCFA_FacilityZip.Trim());
                        form.set_Element(32, "npi", oTranDetails.HCFA_FacilityNPI.Trim());
                        form.set_Element(32, "id-num", "");
                        form.set_Element(32, "clia", "");
                        form.set_Element(32, "mammo", "");

                        #endregion Facility Details

                        #region Billing Provider

                        form.set_Element(33, "name", oTranDetails.HCFA_ProviderFName.Trim() + " " + oTranDetails.HCFA_ProviderLName.Trim());
                        form.set_Element(33, "addr", oTranDetails.HCFA_ProviderAddress1.Trim());
                        form.set_Element(33, "city", oTranDetails.HCFA_ProviderCity.Trim());
                        form.set_Element(33, "state", oTranDetails.HCFA_ProviderState.Trim());
                        form.set_Element(33, "zip", oTranDetails.HCFA_ProviderZip.Trim());
                        form.set_Element(33, "phone", oTranDetails.HCFA_ProviderPhone.Trim());
                        form.set_Element(33, "npi", oTranDetails.HCFA_ProviderNPI.Trim());
                        form.set_Element(33, "id-num", "");

                        #endregion Billing Provider

                        form.EndEncounter();	// End Encounter 
                        form.EndEncounters(); // End Encounters

                        string xml = form.XML;
                        txtEDIData.Text = "";
                        txtEDIData.Text = xml;

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupHCFA1500, gloAuditTrail.ActivityType.ValidateClaim, "Generate XML for HCFA 1500", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                    }
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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

        private void GetReferralProvider(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _strSQL = "";
            DataTable dtProvider = new DataTable();

            try
            {
                oDB.Connect(false);
                _strSQL = " SELECT     nContactId, sAddressLine1, sAddressLine2, sCity, sState, sZIP, sPhone, sFirstName, sMiddleName, sLastName, sTaxonomy, sTaxonomyDesc,  " +
                          " sTaxID, sUPIN, sNPI, sDegree, nContactFlag, sNotes " +
                          " FROM         Patient_DTL " +
                          " WHERE     (nClinicID = " + _ClinicID + ") AND (nPatientID = " + PatientID + ") AND (nContactFlag = 3)";
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
                    }
                }
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
                if (oDB != null)
                    oDB.Dispose();
            }
        }

        #endregion " Private and Public Methods "

        #region "Validate Claim using YOST SDK"

        private void ValidateClaim(Transaction oTransaction)
        {
            TransactionLines oTransactionLines = null;
            ClsTransaction oClaimTransaction = new ClsTransaction();
            ClsServiceInfo oServiceInfo = new ClsServiceInfo();

            try
            {
                oTransactionLines = oTransaction.Lines;
                if (oTransactionLines != null && oTransactionLines.Count > 0)
                {
                    FillPatientInformation(oTransaction.PatientID);
                    for (int i = 0; i <= oTransactionLines.Count - 1; i++)
                    {
                        oServiceInfo = new ClsServiceInfo();
                        oClaimTransaction.ActionCode = "V";
                        oClaimTransaction.CodeSet = "";
                        oClaimTransaction.SearchTerms = "";
                        oClaimTransaction.TransactionID = i.ToString();
                        if (_PatientGender == "Male")
                        {
                            oClaimTransaction.Patient.Gender = "M";
                        }
                        else if (_PatientGender == "Female")
                        {
                            oClaimTransaction.Patient.Gender = "F";
                        }


                        oClaimTransaction.Patient.Day = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_PatientDOB)).Day.ToString();// oPatientControl.PatientDateOfBirth.Day.ToString();
                        oClaimTransaction.Patient.Month = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_PatientDOB)).Month.ToString();// oPatientControl.PatientDateOfBirth.Month.ToString();
                        oClaimTransaction.Patient.Year = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_PatientDOB)).Year.ToString();//oPatientControl.PatientDateOfBirth.Year.ToString();

                        oServiceInfo.FromDay = oTransactionLines[i].DateServiceFrom.Day.ToString();
                        oServiceInfo.FromMonth = oTransactionLines[i].DateServiceFrom.Month.ToString();
                        oServiceInfo.FromYear = oTransactionLines[i].DateServiceFrom.Year.ToString();

                        oServiceInfo.ToDay = oTransactionLines[i].DateServiceTill.Day.ToString();
                        oServiceInfo.ToMonth = oTransactionLines[i].DateServiceTill.Month.ToString();
                        oServiceInfo.ToYear = oTransactionLines[i].DateServiceTill.Year.ToString();

                        oServiceInfo.POS = oTransactionLines[i].POSCode;
                        oServiceInfo.ProcCode = oTransactionLines[i].CPTCode;


                        if (oTransactionLines[i].Dx1Ptr == true)
                        {
                            if (oTransactionLines[i].Dx1Code.Trim() != "")
                            {
                                if (oTransactionLines[i].Dx1Code.Trim() != "")
                                {
                                    oServiceInfo.DiagnosisList.Add(oTransactionLines[i].Dx1Code.Trim());
                                }
                                else
                                {
                                    MessageBox.Show("Diagnosis is not present, cannot validate.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Diagnosis is not present, cannot validate.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Diagnosis is not present, cannot validate.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (oTransactionLines[i].Dx2Ptr == true)
                        {
                            if (oTransactionLines[i].Dx2Code.Trim() != "")
                            {
                                oServiceInfo.DiagnosisList.Add(oTransactionLines[i].Dx2Code.Trim());
                            }
                        }

                        if (oTransactionLines[i].Dx3Ptr == true)
                        {
                            if (oTransactionLines[i].Dx3Code.Trim() != "")
                            {
                                oServiceInfo.DiagnosisList.Add(oTransactionLines[i].Dx3Code.Trim());
                            }
                        }

                        if (oTransactionLines[i].Dx4Ptr == true)
                        {
                            if (oTransactionLines[i].Dx4Code.Trim() != "")
                            {
                                oServiceInfo.DiagnosisList.Add(oTransactionLines[i].Dx4Code.Trim());
                            }
                        }

                        if (oTransactionLines[i].Mod1Code.Trim() != "") { oServiceInfo.ModifierList.Add(oTransactionLines[i].Mod1Code.Trim()); }
                        if (oTransactionLines[i].Mod2Code.Trim() != "") { oServiceInfo.ModifierList.Add(oTransactionLines[i].Mod2Code.Trim()); }


                        oClaimTransaction.Services.Add(oServiceInfo);

                        oServiceInfo = null;
                    }
                    SendClaimtoValidate(oClaimTransaction);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
              //  if (oTransactionLines != null) { oTransactionLines.Dispose(); }
                if (oClaimTransaction != null) { oClaimTransaction = null; }
                if (oServiceInfo != null) { oServiceInfo = null; }

            }
        }

        private void SendClaimtoValidate(ClsTransaction oClaimTransaction)
        {
            ClsClaimSubMittal oClaimSubMittal = default(ClsClaimSubMittal);
            //string _FilePath = AppDomain.CurrentDomain.BaseDirectory;
            string _FilePath = AppDomain.CurrentDomain.BaseDirectory + "\\";
            try
            {

                if (oClaimTransaction != null)
                {
                    oClaimSubMittal = new ClsClaimSubMittal();
                    oClaimSubMittal.ChargesTransaction = oClaimTransaction;
                    oClaimSubMittal.ApplicationPath = appSettings["StartupPath"].ToString();
                    oClaimSubMittal.PostXML();
                    System.Text.StringBuilder strSearchResponse = default(System.Text.StringBuilder);
                    strSearchResponse = new System.Text.StringBuilder();

                    System.Text.StringBuilder strValidationResponse = default(System.Text.StringBuilder);
                    strValidationResponse = new System.Text.StringBuilder();

                    if (oClaimSubMittal.oSearchResponseList.Count > 0)
                    {
                        foreach (ClsSearchResponse oSearchResponse in oClaimSubMittal.oSearchResponseList)
                        {
                            strSearchResponse.Append("Code: " + oSearchResponse.Code);
                            strSearchResponse.Append("Description: " + oSearchResponse.Description);
                            strSearchResponse.Append(Environment.NewLine);
                        }
                    }
                    if (oClaimSubMittal.oValidationResponseList.Count > 0)
                    {
                        int icnt = 1;
                        foreach (ClsValidationResponse oValidationResponse in oClaimSubMittal.oValidationResponseList)
                        {
                            strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append("Result " + icnt.ToString());
                            //strValidationResponse.Append("Code: " + oValidationResponse.Code);
                            strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append("MedicalNecessity: " + FormatString(oValidationResponse.MedicalNecessity));
                            //strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append(Environment.NewLine);
                            //strValidationResponse.Append("Modifiers: " + oValidationResponse.Modifiers);
                            strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append("CCI: " + FormatString(oValidationResponse.CCI));
                            strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append("Usage: " + FormatString(oValidationResponse.Usage));
                            strValidationResponse.Append(Environment.NewLine);
                            strValidationResponse.Append("----------------------------------------------------------------------");
                            strValidationResponse.Append(Environment.NewLine);
                            icnt++;
                        }
                    }

                    //MessageBox.Show(strSearchResponse.ToString() + Environment.NewLine + strValidationResponse.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);

                    if (strValidationResponse.ToString().Trim() != "")
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.ValidateClaim, "Claim Validated", 0, Convert.ToInt64(oClaimTransaction.TransactionID), 0, ActivityOutCome.Success);

                        _FilePath = _FilePath + "ClaimValidation.txt";
                        System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                        oStreamWriter.WriteLine(strValidationResponse.ToString());
                        oStreamWriter.Close();
                        oStreamWriter.Dispose();
                        System.Diagnostics.Process.Start(_FilePath);

                    }

                }
            }
            catch (ClsClaimSubmittalException claimEx)
            {
                MessageBox.Show("ERROR : " + claimEx.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private string FormatString(string strResponse)
        {
            System.Text.RegularExpressions.Regex rxChar = new System.Text.RegularExpressions.Regex("[a-zA-Z]");
            System.Text.RegularExpressions.Regex rxNum = new System.Text.RegularExpressions.Regex("[0-9]");
            for (int i = strResponse.Length - 1; i > 0; i--)
            {
                if (rxChar.Match(strResponse[i].ToString()).Success == true && rxNum.Match(strResponse[i - 1].ToString()).Success == true)
                {
                    strResponse = strResponse.Insert(i, " ");
                }
                if (rxNum.Match(strResponse[i].ToString()).Success == true && rxChar.Match(strResponse[i - 1].ToString()).Success == true)
                {
                    strResponse = strResponse.Insert(i, " ");
                }
            }
            strResponse = strResponse.Replace("CPT ", "CPT");
            strResponse = strResponse.Replace("ICD ", Environment.NewLine + "ICD");
            strResponse = strResponse.Replace("MN ", "MN");
            strResponse = strResponse.Replace("CCI ", "CCI");

            return strResponse;
        }
        #endregion


    }
}