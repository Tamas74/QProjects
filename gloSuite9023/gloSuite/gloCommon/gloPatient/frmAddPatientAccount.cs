using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace gloPatient
{
    public partial class frmAddPatientAccount : Form
    {
        #region "Variable Declaration"

        Int64 patientId;
        string databaseconnectionstring = "";
        private string _messageBoxCaption = "gloPM";
        private Int64 _ClinicID = 1;
        int _ownAccountCount = 0;
        int _FormInitialTop, _FormInitialLeft;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        gloPatientAddAccountControl gloPatientAddAccountControl = null;
        PatientGuardian oPatientGuardian = null;
        PatientOtherContacts oPatientGuarantors = null;
        PatientDemographics oPatientDemographicsDetails = null;
        Account oAccount = null;
        PatientAccount oPatientAccount = null;
        PatientAccounts oPatientAccounts = null;

        #endregion

        #region "Properties"

        public PatientOtherContacts PatientGuarantors
        {
            get { return oPatientGuarantors; }
            set { oPatientGuarantors = value; }
        }

        public PatientGuardian PatientGuardianDetails
        {
            get { return oPatientGuardian; }
            set { oPatientGuardian = value; }
        }

        public PatientDemographics PatientDemographicDetails
        {
            get { return oPatientDemographicsDetails; }
            set { oPatientDemographicsDetails = value; }
        }

        public Account Account
        {
            get { return oAccount; }
            set { oAccount = value; }
        }

        public PatientAccount PatientAccount
        {
            get { return oPatientAccount; }
            set { oPatientAccount = value; }
        }

        public PatientAccounts PatientAccounts
        {
            get { return oPatientAccounts; }
            set { oPatientAccounts = value; }
        }

        public int OwnAccountCount
        {
            get { return _ownAccountCount; }
            set { _ownAccountCount = value; }
        }

        #endregion

        #region "Constructor"

        public frmAddPatientAccount()
        {
            InitializeComponent();
        }

        public frmAddPatientAccount(Int64 _patientId, string _databaseconnectionstring)
        {
            InitializeComponent();
            patientId = _patientId;
            databaseconnectionstring = _databaseconnectionstring;
            oPatientGuarantors = new PatientOtherContacts();
            oPatientGuardian = new PatientGuardian(_databaseconnectionstring);
            oPatientDemographicsDetails = new PatientDemographics();

            #region " Retrieve MessageBoxCaption"

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                else
                    _messageBoxCaption = "gloPM";

            }

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                    _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                else _ClinicID = 1;
            }

            #endregion

        }

        #endregion

        #region "Delegates"

        public delegate void SaveButtonClick(object sender, EventArgs e);
        public event SaveButtonClick SaveButton_Click;

        #endregion

        #region "Events"

        private void frmAddPatientAccount_Load(object sender, EventArgs e)
        {
            try
            {
                gloPatientAddAccountControl = new gloPatientAddAccountControl(patientId, databaseconnectionstring);
                gloPatientAddAccountControl.onPatientAddAccountControl_Enter += new gloPatientAddAccountControl.onPatientAddAccountControlEnter(gloPatientAddAccountControl_onPatientAddAccountControl_Enter);
                gloPatientAddAccountControl.onPatienAddtAccountControl_Leave += new gloPatientAddAccountControl.onPatientAddAccountControlLeave(gloPatientAddAccountControl_onPatienAddtAccountControl_Leave);
                gloPatientAddAccountControl.PatientGuarantors = PatientGuarantors;
                gloPatientAddAccountControl.PatientGuardianDetails = PatientGuardianDetails;
                gloPatientAddAccountControl.PatientDemographicDetails = PatientDemographicDetails;
                gloPatientAddAccountControl.PatientAccounts = PatientAccounts;
                gloPatientAddAccountControl.OwnAccountCount = OwnAccountCount;
                gloPatientAddAccountControl.Dock = DockStyle.Fill;
                pnlAccountInfo.Controls.Add(gloPatientAddAccountControl);

                _FormInitialLeft = this.Left;
                _FormInitialTop = this.Top;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void gloPatientAddAccountControl_onPatientAddAccountControl_Enter(object sender, EventArgs e)
        {
            try
            {
                //this.StartPosition = FormStartPosition.CenterScreen;
                this.Left = _FormInitialLeft;
                this.Top = _FormInitialTop;
                this.Width = 435;
                this.Height = 306;
                pnlTop.Visible = true;
                pnlAccountInfo.Dock = DockStyle.Fill;
                this.Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void gloPatientAddAccountControl_onPatienAddtAccountControl_Leave(object sender, EventArgs e)
        {
            try
            {
                this.Left = 300;
                this.Top = 200;
                this.Width = 700;
                this.Height = 680;
                pnlTop.Visible = false;
                pnlAccountInfo.Dock = DockStyle.Fill;
                this.Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        {
                            if (gloPatientAddAccountControl.GetData() == true)
                            {
                                gloPatientAddAccountControl.SaveAccount();
                                this.Close();
                            }
                        }
                        break;
                    case "Cancel":
                        {
                            this.Close();
                        }
                        break;
                    default:
                        {

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void frmAddPatientAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            //after creation of account get patientaccounts and assign to glopatientdemographic control patientaccount property.
            try
            {
                GetPatientAccounts();
                SaveButton_Click(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// After creation of account get patientaccounts  
        /// assign patientaccounts to gloPatientdemographic control. 
        /// </summary>
        private void GetPatientAccounts()
        {

            DataTable dtPatientAccounts = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nPatientID", patientId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_GetAllPatientAccounts", oParameters, out dtPatientAccounts);

                if (dtPatientAccounts != null && dtPatientAccounts.Rows.Count > 0)
                {
                    PatientAccounts = new PatientAccounts();
                    for (int i = 0; i < dtPatientAccounts.Rows.Count; i++)
                    {
                        PatientAccount oPatientAccount = new PatientAccount();
                        oPatientAccount.AccountPatientID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nAccountPatientID"].ToString());
                        oPatientAccount.PAccountID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nPAccountID"].ToString());
                        oPatientAccount.AccountNo = dtPatientAccounts.Rows[i]["sAccountNo"].ToString();
                        oPatientAccount.PatientID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nPatientID"].ToString());
                        oPatientAccount.PatientCode = dtPatientAccounts.Rows[i]["sPatientCode"].ToString();
                        oPatientAccount.ClinicID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nClinicID"].ToString());
                        oPatientAccount.SiteID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nSiteID"].ToString());
                        oPatientAccount.MachineName = dtPatientAccounts.Rows[i]["sMachineName"].ToString();
                        oPatientAccount.UserID = Convert.ToInt64(dtPatientAccounts.Rows[i]["nUserID"].ToString());
                        oPatientAccount.RecordDate = Convert.ToDateTime(dtPatientAccounts.Rows[i]["dtRecordDate"].ToString());
                        oPatientAccount.Active = Convert.ToBoolean(dtPatientAccounts.Rows[i]["bIsActive"].ToString());
                        oPatientAccount.OwnAccount = Convert.ToBoolean(dtPatientAccounts.Rows[i]["bIsOwnAccount"].ToString());
                        PatientAccounts.Add(oPatientAccount);
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }

        }

        #endregion
    }
}

