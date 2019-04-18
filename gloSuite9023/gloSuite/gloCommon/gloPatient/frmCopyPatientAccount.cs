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
    public partial class frmCopyPatientAccount : Form
    {
        #region "Variable Declaration"

        //code added by Mahesh Satlapalli.
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        string _databaseconnectionstring = "";
        string _messageBoxCaption = "gloPM";
        public Int64 nAccountId;
        Int64 nPatientId;
        Int64 nGuarantorId;
        Int64 _ClinicID = 1;
        gloPatientCopyAccountControl oPatientCopyAccountControl = null;
        PatientOtherContacts oPatientGuarantors = null;
        PatientGuardian oPatientGuardian = null;
        PatientDemographics oPatientDemographicsDetails = null;
        PatientAccount oPatientAccount = null;
        PatientAccounts oPatientAccounts = null;

        #endregion

        //Code Added by Mahesh Satlapalli
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

        #endregion

        #region "Constructors"

        public frmCopyPatientAccount()
        {
            InitializeComponent();
        }

        public frmCopyPatientAccount(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
        }

        //Added By Mahesh Satlapalli (Apollo) on 12-Jan-2011 
        public frmCopyPatientAccount(string databaseconnectionstring, Int64 patientId, Int64 guarantorId, Int64 accountId)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;

            //changed my mahesh s on 26/may/2011.
            _messageBoxCaption = appSettings["MessageBOXCaption"] != null && appSettings["MessageBOXCaption"] != "" ? Convert.ToString(appSettings["MessageBOXCaption"]) : _messageBoxCaption = "gloPM";
            _ClinicID = appSettings["ClinicID"] == null ? 1 : appSettings["ClinicID"] == "" ? 1 : Convert.ToInt64(appSettings["ClinicID"]);
            //
            
            nAccountId = accountId;
            nPatientId = patientId;
            nGuarantorId = guarantorId;
            oPatientGuarantors = new PatientOtherContacts();
            oPatientGuardian = new PatientGuardian(databaseconnectionstring);
            oPatientDemographicsDetails = new PatientDemographics();

        }

        #endregion

        #region "Delegates"

        public delegate void SaveButtonClick(object sender, EventArgs e);
        public event SaveButtonClick SaveButton_Click;

        public delegate void CloseButtonClick(object sender, EventArgs e);
       // public event CloseButtonClick CloseButton_Click;

        #endregion

        #region "Methods"

        /// <summary>
        /// After creation of copyaccount get patientaccounts and patientguarantors, 
        /// assign patientaccounts to gloPatientdemographic control. 
        /// </summary>
        private void GetPatientAccounts()
        {

            DataTable dtPatientAccounts = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            PatientAccount oPatientAccount = null;
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nPatientID", nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_GetAllPatientAccounts", oParameters, out dtPatientAccounts);

                if (dtPatientAccounts != null && dtPatientAccounts.Rows.Count > 0)
                {
                    PatientAccounts = new PatientAccounts();
                    for (int i = 0; i < dtPatientAccounts.Rows.Count; i++)
                    {
                         //null handling by mahesh s on 26/may/2011.
                        oPatientAccount = new PatientAccount();
                        oPatientAccount.AccountPatientID = dtPatientAccounts.Rows[i]["nAccountPatientID"] == DBNull.Value ? Convert.ToInt64(0) : Convert.ToInt64(dtPatientAccounts.Rows[i]["nAccountPatientID"].ToString());
                        oPatientAccount.PAccountID = dtPatientAccounts.Rows[i]["nPAccountID"] == DBNull.Value ? Convert.ToInt64(0) : Convert.ToInt64(dtPatientAccounts.Rows[i]["nPAccountID"].ToString());
                        oPatientAccount.AccountNo = dtPatientAccounts.Rows[i]["sAccountNo"]==DBNull.Value ? string.Empty : dtPatientAccounts.Rows[i]["sAccountNo"].ToString();
                        oPatientAccount.PatientID = dtPatientAccounts.Rows[i]["nPatientID"] == DBNull.Value ? Convert.ToInt64(0) : Convert.ToInt64(dtPatientAccounts.Rows[i]["nPatientID"].ToString());
                        oPatientAccount.PatientCode = dtPatientAccounts.Rows[i]["sPatientCode"] == DBNull.Value ? string.Empty : dtPatientAccounts.Rows[i]["sPatientCode"].ToString();
                        oPatientAccount.ClinicID = dtPatientAccounts.Rows[i]["nClinicID"] == DBNull.Value ? Convert.ToInt64(0) : Convert.ToInt64(dtPatientAccounts.Rows[i]["nClinicID"].ToString());
                        oPatientAccount.SiteID = dtPatientAccounts.Rows[i]["nSiteID"] == DBNull.Value ? Convert.ToInt64(0) : Convert.ToInt64(dtPatientAccounts.Rows[i]["nSiteID"].ToString());
                        oPatientAccount.MachineName = dtPatientAccounts.Rows[i]["sMachineName"] == DBNull.Value ? string.Empty : dtPatientAccounts.Rows[i]["sMachineName"].ToString();
                        oPatientAccount.UserID = dtPatientAccounts.Rows[i]["nUserID"] == DBNull.Value ? Convert.ToInt64(0) : Convert.ToInt64(dtPatientAccounts.Rows[i]["nUserID"].ToString());
                        oPatientAccount.RecordDate =Convert.ToDateTime(dtPatientAccounts.Rows[i]["dtRecordDate"].ToString());
                        oPatientAccount.Active = Convert.ToBoolean(dtPatientAccounts.Rows[i]["bIsActive"].ToString());
                        oPatientAccount.OwnAccount =  Convert.ToBoolean(dtPatientAccounts.Rows[i]["bIsOwnAccount"].ToString());
                        PatientAccounts.Add(oPatientAccount);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                //object dispose by mahesh s on 26/may/2011.
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oPatientAccount != null)
                    oPatientAccount.Dispose();
            }

        }


        #endregion

        #region "Events"

        private void frmCopyPatientAccount_Load(object sender, EventArgs e)
        {

            //Code 
            oPatientCopyAccountControl = new gloPatientCopyAccountControl(_databaseconnectionstring, nPatientId, nGuarantorId, nAccountId);
            oPatientCopyAccountControl.onPatientAccountControl_Enter += new gloPatientCopyAccountControl.onPatientAccountControlEnter(gloPatientCopyAccountControl_onPatientAccountControlEnter);
            oPatientCopyAccountControl.onPatientAccountControl_Leave += new gloPatientCopyAccountControl.onPatientAccountControlLeave(gloPatientCopyAccountControl_onPatientAccountControlLeave);
            oPatientCopyAccountControl.PatientGuarantors = PatientGuarantors;
            oPatientCopyAccountControl.PatientGuardianDetails = PatientGuardianDetails;
            oPatientCopyAccountControl.PatientDemographicDetails = PatientDemographicDetails;
            oPatientCopyAccountControl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(oPatientCopyAccountControl);
        }
        //Code Added by Mahesh Satlapalli
        private void gloPatientCopyAccountControl_onPatientAccountControlEnter(object sender, EventArgs e)
        {
            //ts_Commands.Visible = true;
            pnlTop.Visible = true;
            pnlContainer.Dock = DockStyle.Fill;
            this.Refresh();

        }
        //Code Added by Mahesh Satlapalli
        private void gloPatientCopyAccountControl_onPatientAccountControlLeave(object sender, EventArgs e)
        {
            //ts_Commands.Visible = false;
            pnlTop.Visible = false;
            pnlContainer.Dock = DockStyle.Fill;
            this.Refresh();
        }
        //Code Added by Mahesh Satlapalli
        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        if (oPatientCopyAccountControl.GetData() == true)
                        {
                            oPatientCopyAccountControl.SaveAccount();
                            this.Close();
                        }
                        break;
                    case "Cancel":

                        DialogResult res = MessageBox.Show("Do you want to save changes to this record? ",_messageBoxCaption , MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                        if (res == DialogResult.Yes)
                        {
                            if (oPatientCopyAccountControl.GetData() == true)
                            {
                                oPatientCopyAccountControl.SaveAccount();
                                this.Close();
                            }
                        }
                        else if (res == DialogResult.No)
                        {
                            this.Close();
                           
                        }
                        else if (res == DialogResult.Cancel)
                        {
                            return;
                        }
                        break;
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

        }
        //Form Closing Event
        private void frmCopyPatientAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
           //after creation of account get patientaccounts and assign to glopatientdemographic control patientaccount property.
            GetPatientAccounts();
            SaveButton_Click(sender, e);
        }

        #endregion

        private void frmCopyPatientAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                oPatientCopyAccountControl.onPatientAccountControl_Enter -= new gloPatientCopyAccountControl.onPatientAccountControlEnter(gloPatientCopyAccountControl_onPatientAccountControlEnter);
                oPatientCopyAccountControl.onPatientAccountControl_Leave -= new gloPatientCopyAccountControl.onPatientAccountControlLeave(gloPatientCopyAccountControl_onPatientAccountControlLeave);
                pnlContainer.Controls.Remove(oPatientCopyAccountControl);
                if (oPatientCopyAccountControl != null) { oPatientCopyAccountControl.Dispose(); oPatientCopyAccountControl = null; }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

       
    }
}
