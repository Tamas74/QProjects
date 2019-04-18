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
    public partial class frmEditPatientAccount : Form
    {
        #region "Variable Declaration"
        
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        string _databaseconnectionstring = "";
        string _messageBoxCaption = "gloPM";
        gloPatientAccountControl oPatientAccountControl = null;

        //code added by SaiKrishna.
        public Int64 nAccountId;
        Int64 nPatientId;
        Int64 nGuarantorId;
        private Int64 _ClinicID = 1;
        PatientOtherContacts oPatientGuarantors = null;
        PatientGuardian oPatientGuardian = null;
        PatientDemographics oPatientDemographicsDetails = null;
        PatientAccount oPatientAccount = null;
        PatientAccounts oPatientAccounts = null;
        public bool _ownAccount = true;

        #endregion

        #region "Properties"
        //Code Added by SaiKrishna
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

        public frmEditPatientAccount()
        {
            InitializeComponent();
        }

        public frmEditPatientAccount(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
        }

        //Added By Mahesh Satlapalli (Apollo) on 12-Jan-2011 
        public frmEditPatientAccount(string databaseconnectionstring, Int64 patientId, Int64 guarantorId, Int64 accountId)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;

            //changed my mahesh s on 26/may/2011.
            _messageBoxCaption = appSettings["MessageBOXCaption"] != null && appSettings["MessageBOXCaption"] != "" ? Convert.ToString(appSettings["MessageBOXCaption"]) : _messageBoxCaption = "gloPM";
            _ClinicID = appSettings["ClinicID"] == null ? 1 : appSettings["ClinicID"] == "" ? 1 : Convert.ToInt64(appSettings["ClinicID"]);
            //

            //Code Added by SaiKirshna
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
     //   public event CloseButtonClick CloseButton_Click;

        #endregion

        #region "Methods"

        /// <summary>
        /// After edit the account get patientaccounts, 
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
                        oPatientAccount.OwnAccount = Convert.ToBoolean(dtPatientAccounts.Rows[i]["bIsOwnAccount"].ToString());
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

        private void frmEditPatientAccount_Load(object sender, EventArgs e)
        {
            //comment by mahesh s on 03-may-2011 , this functionality moved to tools.
            //Code changed By Mahesh Satlapalli (Apollo)
            //tsb_Merge.Visible = false;

            oPatientAccountControl = new gloPatientAccountControl(_databaseconnectionstring, nPatientId, nGuarantorId, nAccountId);
            oPatientAccountControl.onPatientAccountControl_Enter += new gloPatientAccountControl.onPatientAccountControlEnter(gloPatientAccountControl_onPatientAccountControlEnter);
            oPatientAccountControl.onPatientAccountControl_Leave += new gloPatientAccountControl.onPatientAccountControlLeave(gloPatientAccountControl_onPatientAccountControlLeave);

            ////comment by mahesh s on 03-may-2011 , this functionality moved to tools.
            ////Binding methods to "mergeAccountChecked_Clicked,mergeAccountUnChecked_Clicked" events
            //oPatientAccountControl.mergeAccountChecked_Clicked += new gloPatientAccountControl.mergeAccountChecked(gloPatientAccountControl_MergeVissible);
            //oPatientAccountControl.mergeAccountUnChecked_Clicked += new gloPatientAccountControl.mergeAccountUnChecked(gloPatientAccountControl_MergeInVissible);

            oPatientAccountControl.PatientGuarantors = PatientGuarantors;
            oPatientAccountControl.PatientGuardianDetails = PatientGuardianDetails;
            oPatientAccountControl.PatientDemographicDetails = PatientDemographicDetails;
            oPatientAccountControl._IsOwnAccount = _ownAccount;
            oPatientAccountControl.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(oPatientAccountControl);

        }

        //comment by mahesh s on 03-may-2011 , this functionality moved to Tools.
        ////Code Added By Mahesh S(Apollo) on 1-Feb-2011 for show merge button
        //private void gloPatientAccountControl_MergeVissible(object sender, EventArgs e)
        //{
        //    tsb_Merge.Visible = true;
        //    tsb_OK.Visible = false;
        //}

        ////Code Added By Mahesh S(Apollo) on 1-Feb-2011 for hide merge button
        //private void gloPatientAccountControl_MergeInVissible(object sender, EventArgs e)
        //{
        //    tsb_Merge.Visible = false;
        //    tsb_OK.Visible = true;
        //}

        //Code Added by SaiKrishna
        private void gloPatientAccountControl_onPatientAccountControlEnter(object sender, EventArgs e)
        {
            pnlTop.Visible = true;
            pnlContainer.Dock = DockStyle.Fill;
            this.Refresh();
        }

        //Code Added by SaiKrishna
        private void gloPatientAccountControl_onPatientAccountControlLeave(object sender, EventArgs e)
        {
            pnlTop.Visible = false;
            pnlContainer.Dock = DockStyle.Fill;
            this.Refresh();
        }

        //Code Added by SaiKrishna
        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        if (oPatientAccountControl.GetData() == true)
                        {
                            oPatientAccountControl.SaveAccount();
                            this.Close();
                        }

                        break;
                    case "Cancel":
                        DialogResult res = MessageBox.Show("Do you want to save changes to this record? ", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                        if (res == DialogResult.Yes)
                        {
                            if (oPatientAccountControl.GetData() == true)
                            {
                                oPatientAccountControl.SaveAccount();
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
                    //comment by mahesh s on 03-may-2011 , this functionality moved to tools.
                    //case "Merge": //Added By Mahesh S(Apollo) on 01-Feb-2011 for call MergeAccount Feature

                    //    if (oPatientAccountControl.ValidateMergeData() == true)
                    //    {
                    //       string strMessage="Merging account  "+oPatientAccountControl.txtAccountNo.Text.ToString() +"  into  "+oPatientAccountControl.txtToBeAccountNo.Text.ToString() +". Continue?";
                    //       DialogResult mergeRes = MessageBox.Show(strMessage, "gloPM", MessageBoxButtons.OKCancel , MessageBoxIcon.Information);
                    //        if (mergeRes == DialogResult.OK)
                    //        {
                    //            oPatientAccountControl.MergeAccount();
                    //            //open from Modify Patient
                    //            nAccountId = oPatientAccountControl._nExistAccountId;
                    //            this.Close();
                    //            MessageBox.Show("Merging of accounts has been done successfully.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        }
                    //        else
                    //        {
                    //            //open from Modify Patient
                    //            return;
                    //        }
                    //    }

                    //    break;
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
        
        //Code Added by SaiKrishna
        private void frmEditPatientAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            GetPatientAccounts();
            if (SaveButton_Click != null)
            {
                SaveButton_Click(sender, e);
            }
        }

        #endregion

        private void frmEditPatientAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                oPatientAccountControl.onPatientAccountControl_Enter -= new gloPatientAccountControl.onPatientAccountControlEnter(gloPatientAccountControl_onPatientAccountControlEnter);
                oPatientAccountControl.onPatientAccountControl_Leave -= new gloPatientAccountControl.onPatientAccountControlLeave(gloPatientAccountControl_onPatientAccountControlLeave);
                if (oPatientAccountControl.pnlAddresssControl.Controls.Count > 0)
                {
                    Control[] myControl = oPatientAccountControl.pnlAddresssControl.Controls.Find("AddressControl", true);
                    if (myControl.Length > 0)
                    {
                        ((gloAddress.gloAddressControl)myControl[0]).ControlClosing = true;
                        oPatientAccountControl.pnlAddresssControl.Controls.Remove(myControl[0]);
                        pnlContainer.Controls.Remove(oPatientAccountControl);
                    }
                }
                if (oPatientAccountControl != null) { oPatientAccountControl.Dispose(); oPatientAccountControl = null; }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }
    }
}
