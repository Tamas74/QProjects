using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;

namespace gloBilling
{
    public partial class frmSetupClearingHouse : Form
    {
        #region "Variable Declaration"

        private string _messageBoxCaption = String.Empty;
        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private Int64 _nClearingHouseID = 0;
        private const string _encryptionKey = "12345678";

        public Int64 nClearingHouseID
        {
            get { return _nClearingHouseID;}
            set { _nClearingHouseID = value;}

        }



        #endregion

        #region "Contructor"

        public frmSetupClearingHouse(string DatabaseConnectionString)
        {
            InitializeComponent();

            _databaseconnectionstring = DatabaseConnectionString;
            _nClearingHouseID = 0;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

        }

        public frmSetupClearingHouse(string DatabaseConnectionString, Int64 ClearingHouseID)
        {
            InitializeComponent();

            _databaseconnectionstring = DatabaseConnectionString;
            _nClearingHouseID = ClearingHouseID;

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

        #endregion

        #region " Form Load Event"

        private void frmSetupClearingHouse_Load(object sender, EventArgs e)
        {
            FillControls();
            if (_nClearingHouseID > 0)
            {
                LoadClearingHouse();
            }
        }

        #endregion
        
        #region "Toolstrip Buttons"

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ts_btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateData() == true)
                {
                    if (SaveData() == true)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        } 

        #endregion

        #region "Save / Fill Methods"

        private void LoadClearingHouse()
        {
            ClearingHouse oClearingHouse = new ClearingHouse(_databaseconnectionstring);
            gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();  
            try
            {
                oClearingHouse = oClearingHouse.GetClearingHouse(_nClearingHouseID);

                if (oClearingHouse != null)
                {
                    _nClearingHouseID = oClearingHouse.ClearingHouseID;
                    txtName.Text = oClearingHouse.ClearingHouseName;
                    txtNameofReceiver.Text = oClearingHouse.RecieverName;
                    txtReceiverID.Text = oClearingHouse.RecieverID;
                    txtSubmitterID.Text = oClearingHouse.SubmitterID;
                    chk1JQulifier.Checked = oClearingHouse.IsOneJQualifier;
                    txt1JQulifier.Text = oClearingHouse.OneJQualifier;
                    chkSenderCode.Checked = oClearingHouse.IsSenderCode;
                    txtSenderCode.Text = oClearingHouse.SenderCode;
                    chkVenderCode.Checked = oClearingHouse.IsVenderID;
                    txtVenderCode.Text = oClearingHouse.VenderID;
                    chkLoop1000BNM109.Checked = oClearingHouse.IsLoop1000B;
                    txtLoop1000BNM109.Text = oClearingHouse.Loop1000B;

                    switch (oClearingHouse.TypeOfData)
                    {
                        case TypeOfData.None:
                            cmbTypeofData.SelectedIndex = -1;
                            break;
                        case TypeOfData.TestData:
                            cmbTypeofData.Text = "Test Data";
                            break;
                        case TypeOfData.ProductionData:
                            cmbTypeofData.Text = "Production Data";
                            break;
                        case TypeOfData.Blank:
                            cmbTypeofData.Text = "Blank";
                            break;
                        default:
                            break;
                    }
                    cmbTypeofData.Refresh();

                    chkISA.Checked = oClearingHouse.IsISA;


                    #region Load Detail 

                    txt_ftpURL.Text = oClearingHouse.URL;
                    txt_Username.Text = oClearingHouse.UserName;
                    txt_Password.Text = oClsEncryption.DecryptFromBase64String(oClearingHouse.Password , _encryptionKey);
                    txt_271EligibilityResponse.Text = oClearingHouse.In_271_ElgibilityResponse;
                    txt_276Eligibilityenquiry.Text = oClearingHouse.Out_276_ElgibilityEnquiry;
                    txt_277ClaimStatusResponse.Text = oClearingHouse.In_277_ClaimStatus;
                    txt_835RemittanceAdvice.Text = oClearingHouse.In_835_Remitance;
                    txt_837PclaimSubmission.Text = oClearingHouse.Out_837P_ClaimSubmition;
                    txt_997INAcknowledgement.Text = oClearingHouse.In_997_Acknowledge;
                    txt_997OUTAcknowledgement.Text = oClearingHouse.Out_997_Acknowledge;
                    txt_CSRReports.Text = oClearingHouse.Gen_CSRReports;
                    txt_Letters.Text = oClearingHouse.Gen_Letters;
                    txt_Reports.Text = oClearingHouse.Gen_Reports;
                    txt_Statements.Text = oClearingHouse.Gen_Statements;
                    txt_WorkedTransactions.Text = oClearingHouse.Gen_WorkedTrans;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillControls()
        {
            try
            {
                cmbTypeofData.Items.Clear();
                cmbTypeofData.Items.Add("");
                cmbTypeofData.Items.Add("Test Data");
                cmbTypeofData.Items.Add("Production Data");
                cmbTypeofData.Items.Add("Blank");
                cmbTypeofData.Refresh();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private bool SaveData()
        {
            ClearingHouse oClearingHouse = new ClearingHouse(_databaseconnectionstring);
            gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();  
            bool _result = false;
            try
            {
                oClearingHouse.ClearingHouseID = _nClearingHouseID;
                oClearingHouse.ClearingHouseName = txtName.Text;
                oClearingHouse.RecieverName = txtNameofReceiver.Text;
                oClearingHouse.RecieverID = txtReceiverID.Text;
                oClearingHouse.SubmitterID = txtSubmitterID.Text;

                if (chk1JQulifier.Checked == true)
                {
                    oClearingHouse.IsOneJQualifier = true;
                    oClearingHouse.OneJQualifier = txt1JQulifier.Text;
                }
                else
                {
                    oClearingHouse.IsOneJQualifier = false;
                    oClearingHouse.OneJQualifier = "";
                }

                if (chkSenderCode.Checked == true)
                {
                    oClearingHouse.IsSenderCode = true;
                    oClearingHouse.SenderCode = txtSenderCode.Text;
                }
                else
                {
                    oClearingHouse.IsSenderCode = false;
                    oClearingHouse.SenderCode = "";
                }

                if (chkVenderCode.Checked == true)
                {
                    oClearingHouse.IsVenderID = true;
                    oClearingHouse.VenderID = txtVenderCode.Text;
                }
                else
                {
                    oClearingHouse.IsVenderID = false;
                    oClearingHouse.VenderID = "";
                }

                if (chkLoop1000BNM109.Checked == true)
                {
                    oClearingHouse.IsLoop1000B = true;
                    oClearingHouse.Loop1000B = txtLoop1000BNM109.Text;
                }
                else
                {
                    oClearingHouse.IsLoop1000B = false;
                    oClearingHouse.Loop1000B = "";
                }

                switch (cmbTypeofData.Text.Trim())
                {
                    case "Test Data":
                        oClearingHouse.TypeOfData = TypeOfData.TestData;
                        break;
                    case "Production Data":
                        oClearingHouse.TypeOfData = TypeOfData.ProductionData;
                        break;
                    case "Blank":
                        oClearingHouse.TypeOfData = TypeOfData.Blank;
                        break;
                    default:
                        oClearingHouse.TypeOfData = TypeOfData.None;
                        break;
                }

                oClearingHouse.IsISA = chkISA.Checked;
                oClearingHouse.ClinicID = _ClinicID;

                #region "set detail fields"

                oClearingHouse.ClearingHouseCode = "";
                oClearingHouse.FolderCategory = 0;//none
                oClearingHouse.Gen_CSRReports = txt_CSRReports.Text.Trim();
                oClearingHouse.Gen_Letters = txt_Letters.Text.Trim();
                oClearingHouse.Gen_Reports = txt_Reports.Text.Trim();
                oClearingHouse.Gen_Statements = txt_Statements.Text.Trim();
                oClearingHouse.Gen_WorkedTrans = txt_WorkedTransactions.Text.Trim();
                oClearingHouse.In_271_ElgibilityResponse = txt_271EligibilityResponse.Text.Trim();
                oClearingHouse.In_277_ClaimStatus = txt_277ClaimStatusResponse.Text.Trim();
                oClearingHouse.In_835_Remitance = txt_835RemittanceAdvice.Text.Trim();
                oClearingHouse.In_997_Acknowledge = txt_997INAcknowledgement.Text.Trim();
                oClearingHouse.Out_276_ElgibilityEnquiry = txt_276Eligibilityenquiry.Text.Trim();
                oClearingHouse.Out_837P_ClaimSubmition = txt_837PclaimSubmission.Text.Trim();
                oClearingHouse.Out_997_Acknowledge = txt_997OUTAcknowledgement.Text.Trim();
                oClearingHouse.Password = oClsEncryption.EncryptToBase64String(txt_Password.Text.Trim(), _encryptionKey);
                oClearingHouse.UserName = txt_Username.Text.Trim();
                oClearingHouse.URL = txt_ftpURL.Text.Trim();

                #endregion


                Int64 _nTempID = oClearingHouse.Add(oClearingHouse);
                _nClearingHouseID = _nTempID;

                if (_nTempID > 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.ClearingHouse, ActivityType.Add, "Add Clearing House", 0, _nTempID, 0, ActivityOutCome.Success);
                    _result = true;
                }
                else 
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.ClearingHouse, ActivityType.Add, "Add Clearing House", 0, _nTempID, 0, ActivityOutCome.Failure);
                    _result = false;

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.ClearingHouse, ActivityType.Add, "Add Clearing House", 0, _nClearingHouseID, 0, ActivityOutCome.Failure);

                _result = false;
            }
            return _result;
        }

        private bool ValidateData()
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Plaese enter the clearinghouse name.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }
            return true;
        } 

        #endregion

        #region "Form Control Events"

        private void chk1JQulifier_CheckedChanged(object sender, EventArgs e)
        {
            txt1JQulifier.Enabled = chk1JQulifier.Checked;
        }

        private void chkSenderCode_CheckedChanged(object sender, EventArgs e)
        {
            txtSenderCode.Enabled = chkSenderCode.Checked;
        }

        private void chkVenderCode_CheckedChanged(object sender, EventArgs e)
        {
            txtVenderCode.Enabled = chkVenderCode.Checked;
        }

        private void chkLoop1000BNM109_CheckedChanged(object sender, EventArgs e)
        {
            txtLoop1000BNM109.Enabled = chkLoop1000BNM109.Checked;
        }

        private void chkISA_CheckedChanged(object sender, EventArgs e)
        {

        }

        #endregion

        private void txt_997OUTAcknowledgement_TextChanged(object sender, EventArgs e)
        {

        }

      
        
    }
}