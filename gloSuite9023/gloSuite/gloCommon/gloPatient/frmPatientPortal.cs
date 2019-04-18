using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using gloPatientPortalCommon;
using System.Linq;


//test1
namespace gloPatient
{
    public partial class frmPatientPortal : Form
    {

        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private gloListControl.gloListControl oListControl;
        //SLR: Changes on 7/30/2014
        //Patient oPatient = new Patient();
        gloPatientRepresentativeControl ogloPatientRepresentativeControl = null;
        private string _databaseconnectionstring = "";
        // private bool _isPatientRepresentativeModified = false;
        PatientRepresentatives oPatientRepresentatives = null;
        Int64 nPatientId = 0;
        Int64 nUserId = 0;
        Boolean MUAccessStatus = false;
        public PatientRepresentatives PatientRepresentatives
        {
            get
            { return oPatientRepresentatives; }
            set
            { oPatientRepresentatives = value; }
        }
        Boolean _IsSaveAsCopy;
        Boolean gblnPatientPortalEnabled;
        string email = "";
        string zip = "";

        Int64 nProviderAssociationID = 0;
        string sProviderTaxID = "";

        PatientPortalAccount oPatientPortalAccount = null;
        public PatientPortalAccount PatientPortalAccount
        {
            get
            { return oPatientPortalAccount; }
            set
            { oPatientPortalAccount = value; }
        }

        String PatientLastname  = "";
        String PatientFirstname = "";
        String PatientDOB = "";
      
        public frmPatientPortal(PatientRepresentatives _PatientRepresentatives, Int64 PatientId, string databaseConnectionString, Boolean IsSaveAsCopy, Boolean _gblnPatientPortalEnabled, string _email, long ClinicID, string _zip,string _Patientlastname = "", string _Patientfirstname = "",string _PatientDOB= "")
        {
            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
            nPatientId = PatientId;
            _IsSaveAsCopy = IsSaveAsCopy;
            gblnPatientPortalEnabled = _gblnPatientPortalEnabled;
            _databaseconnectionstring = databaseConnectionString;
            email = _email;
            zip = _zip;
            _ClinicID = ClinicID;
            //SLR: Changes on 7/30/2014
            //oPatientRepresentatives = new PatientRepresentatives();
            PatientRepresentatives = _PatientRepresentatives;
            PatientLastname = _Patientlastname;
            PatientFirstname = _Patientfirstname;
            PatientDOB = _PatientDOB;
            //PatientZip = _Patientzip;

        }

        private void frmAddNotes_Load(object sender, EventArgs e)
        {

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { nUserId = Convert.ToInt64(appSettings["UserID"]); }
            }
            //SLR: Changes on 7/30/2014
            removeListControl();
            oListControl = new gloListControl.gloListControl();
            showcontrols();
            GetPatientPortalStatus();
            if (oPatientRepresentatives != null && oPatientRepresentatives.Count > 0)
            {
                DataTable dtPatientRepresentative = new DataTable();
                DataColumn dcId = new DataColumn("Id");
                DataColumn dcDescription = new DataColumn("Description");
                dtPatientRepresentative.Columns.Add(dcId);
                dtPatientRepresentative.Columns.Add(dcDescription);

                for (int i = 0; i < oPatientRepresentatives.Count; i++)
                {
                    DataRow drTemp = dtPatientRepresentative.NewRow();
                    drTemp["Id"] = oPatientRepresentatives[i].PRId;
                    drTemp["Description"] = oPatientRepresentatives[i].FirstName + " " + oPatientRepresentatives[i].LastName;
                    dtPatientRepresentative.Rows.Add(drTemp);
                }
                //cmbPatientRepresentative.Items.Clear();
                cmbPatientRepresentative.DataSource = null;
                cmbPatientRepresentative.Items.Clear();
                cmbPatientRepresentative.DataSource = dtPatientRepresentative;

                cmbPatientRepresentative.ValueMember = dtPatientRepresentative.Columns["Id"].ColumnName;
                cmbPatientRepresentative.DisplayMember = dtPatientRepresentative.Columns["Description"].ColumnName;

            }
            if (cmbPatientRepresentative.Items.Count != 0)
            {
                cmbPatientRepresentative.SelectedIndex = 0;
            }

            if (oPatientPortalAccount != null)
            {

                if (oPatientPortalAccount.IsTrainingProvided == false)
                {
                    mskDOInfoProvided.Checked = false;
                }
                else
                {
                    mskDOInfoProvided.Checked = true;
                    if (oPatientPortalAccount.DateOfTraining != null)
                    {
                        mskDOInfoProvided.Value = Convert.ToDateTime(oPatientPortalAccount.DateOfTraining);
                    }
                }
            }
            else
            {
                mskDOInfoProvided.Checked = false;
            }


            AddDemographicsControl();

        }



        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
        }

        //SLR: Changes on 7/30/2014
        private void removeListControl()
        {
            if (oListControl != null)
            {
                if (pnlPatientRepresentative.Controls.Contains(oListControl))
                {
                    pnlPatientRepresentative.Controls.Remove(oListControl);
                 
                }
                try
                {
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_PatientRepresentativeSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                }
                catch
                {
                }
                oListControl.Dispose();
                oListControl = null;
            }

        }

        private void BrowsePR()
        {
            try
            {
                ogloPatientRepresentativeControl.GetData();
                fillPRcombo();
                //SLR: Changes on 7/30/2014
                removeListControl();

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.PatientRepresentative, true, true, this.Width);
                oListControl.ControlHeader = "Patient Representative";

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_PatientRepresentativeSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                pnlPatientRepresentative.Controls.Add(oListControl);

                //To allow the user to add multiple guarantors at one time 

                DataTable dtPatientRepresentative = (DataTable)cmbPatientRepresentative.DataSource;
                if (dtPatientRepresentative != null && dtPatientRepresentative.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPatientRepresentative.Rows.Count; i++)
                    {
                        if (Convert.ToInt64(dtPatientRepresentative.Rows[i]["Id"]) > 0)
                        {
                            oListControl.SelectedItems.Add(Convert.ToInt64(dtPatientRepresentative.Rows[i]["Id"]), Convert.ToString(dtPatientRepresentative.Rows[i]["Description"]));
                        }
                    }
                }


                oListControl.OpenControl();

                //oListControl is disposed in OpenControl() Method if Zero records found
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        private void btnPatientRepresentativeBrowse_Click(object sender, EventArgs e)
        {
            BrowsePR();
        }
        private void oListControl_PatientRepresentativeSelectedClick(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPatientRepresentative = new DataTable();
                DataColumn dcId = new DataColumn("Id");
                DataColumn dcDescription = new DataColumn("Description");
                dtPatientRepresentative.Columns.Add(dcId);
                dtPatientRepresentative.Columns.Add(dcDescription);

                Int64 _TempPRID = 0;
              

                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        _TempPRID = Convert.ToInt64(oListControl.SelectedItems[i].ID);

                        bool _ShouldAdd = true;

                        for (int _Count = 0; _Count < oPatientRepresentatives.Count; _Count++)
                        {
                            if (Convert.ToInt64(oListControl.SelectedItems[i].ID) == oPatientRepresentatives[_Count].PRId)
                            {
                                _ShouldAdd = false;
                                break;
                            }
                        }
                        if (_ShouldAdd == true)
                        {
                            gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);
                            PatientRepresentative oPatientRepresentative = ogloPatient.GetPatientRepresentativesById(_TempPRID);

                            if (oPatientRepresentative != null)
                            {
                                oPatientRepresentatives.Add(oPatientRepresentative);
                                ogloPatientRepresentativeControl.SetData(oPatientRepresentative);
                                oPatientRepresentative.Dispose();
                                oPatientRepresentative = null;
                            }
                            ogloPatient.Dispose();
                            ogloPatient = null;
                        }

                    }
                }
                //SLR: Logic Changed on 7/30/2014
                for (int _Count = oPatientRepresentatives.Count - 1; _Count >= 0; _Count--)
                {
                    if (oPatientRepresentatives[_Count].PRId == 0) continue;
                    //SLR: Logic to be changed on 2/4/2014
                    bool _found = false;
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        if (Convert.ToInt64(oListControl.SelectedItems[i].ID) == oPatientRepresentatives[_Count].PRId)
                        {
                            _found = true;
                            break;
                        }
                    }
                    if (_found == false)
                    {
                        ogloPatientRepresentativeControl.SetData(null, oPatientRepresentatives[_Count].PRId);
                        oPatientRepresentatives.RemoveAt(_Count);
                        //_Count -= 1;

                    }

                }



                for (int i = 0; i < oPatientRepresentatives.Count; i++)
                {
                    DataRow drTemp = dtPatientRepresentative.NewRow();
                    drTemp["Id"] = oPatientRepresentatives[i].PRId;
                    drTemp["Description"] = oPatientRepresentatives[i].FirstName + " " + oPatientRepresentatives[i].LastName;
                    dtPatientRepresentative.Rows.Add(drTemp);
                }
               // cmbPatientRepresentative.Items.Clear();
                cmbPatientRepresentative.DataSource = null;
                cmbPatientRepresentative.Items.Clear();
                cmbPatientRepresentative.DataSource = dtPatientRepresentative;
                cmbPatientRepresentative.ValueMember = dtPatientRepresentative.Columns["Id"].ColumnName;
                cmbPatientRepresentative.DisplayMember = dtPatientRepresentative.Columns["Description"].ColumnName;

                ogloPatientRepresentativeControl.SetPR();
                //    _isPatientRepresentativeModified = true;



                //cmbPatientRepresentative.DataSource = null;
                //cmbPatientRepresentative.Items.Clear();
                //DataTable dtPatientRepresentative = new DataTable();
                //DataColumn dcId = new DataColumn("Id");
                //DataColumn dcDescription = new DataColumn("Description");
                //dtPatientRepresentative.Columns.Add(dcId);
                //dtPatientRepresentative.Columns.Add(dcDescription);

                //if (oListControl.SelectedItems.Count > 0)
                //{
                //    for (int i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                //    {
                //        DataRow drTemp = dtPatientRepresentative.NewRow();

                //        drTemp["ID"] = oListControl.SelectedItems[i].ID;
                //        drTemp["Description"] = oListControl.SelectedItems[i].Description;
                //        cmbPatientRepresentative.Tag = Convert.ToInt64(oListControl.SelectedItems[i].ID);
                //        dtPatientRepresentative.Rows.Add(drTemp);

                //        for (int k = 0 ;k<=oPatientRepresentatives.Count-1;k++)
                //        {

                //            if (oPatientRepresentatives[k].PRId == oListControl.SelectedItems[i].ID)
                //            {
                //                oPatientRepresentatives.RemoveAt(k);
                //            }
                //        }
                //    }
                //}
                //cmbPatientRepresentative.DataSource = dtPatientRepresentative;
                //cmbPatientRepresentative.ValueMember = dtPatientRepresentative.Columns["ID"].ColumnName;
                //cmbPatientRepresentative.DisplayMember = dtPatientRepresentative.Columns["Description"].ColumnName;
                //if (Convert.ToInt64(cmbPatientRepresentative.Tag) == 0)
                //    cmbPatientRepresentative.SelectedValue = dtPatientRepresentative.Rows[0]["ID"].ToString();
                //else
                //    cmbPatientRepresentative.SelectedValue = cmbPatientRepresentative.Tag;
                //cmbPatientRepresentative.DrawMode = DrawMode.Normal;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }
            tls_btnAdd.Visible = true;
            tls_btnSave.Visible = true;
            tls_btnRemove.Visible = true;
            tls_btnBrowse.Visible = true;

            //cmbPatientRepresentative.Focus();
        }
        private void btnPatientRepresentativeNew_Click(object sender, EventArgs e)
        {
            AddDemographicsControl();
        }

        private void AddDemographicsControl()
        {
            try
            {
                //SLR: Changes on 7/30/2014
                if (ogloPatientRepresentativeControl != null)
                {
                    MessageBox.Show("Already Patient Account screen was opened, Please close");
                    return;
                }
                ogloPatientRepresentativeControl = new gloPatientRepresentativeControl(_databaseconnectionstring);
                ogloPatientRepresentativeControl.PatientRepresentatives = oPatientRepresentatives;
                ogloPatientRepresentativeControl.SaveButton_Click += new gloPatientRepresentativeControl.SaveButtonClick(ogloPatientRepresentativeControl_SaveButton_Click);
                ogloPatientRepresentativeControl.CloseButton_Click += new gloPatientRepresentativeControl.CloseButtonClick(ogloPatientRepresentativeControl_CloseButton_Click);
                pnlPatientRepresentative.Controls.Add(ogloPatientRepresentativeControl);
                ogloPatientRepresentativeControl.Dock = DockStyle.Fill;
                ogloPatientRepresentativeControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        private void ogloPatientRepresentativeControl_SaveButton_Click(object sender, EventArgs e)
        {
            fillPRcombo();
        }
        private void fillPRcombo()
        {
            try
            {
                oPatientRepresentatives = ogloPatientRepresentativeControl.PatientRepresentatives;
                if (oPatientRepresentatives != null && oPatientRepresentatives.Count > 0)
                {

                    DataTable dtRepresentative = new DataTable();
                    DataColumn dcId = new DataColumn("Id");
                    DataColumn dcDescription = new DataColumn("Description");
                    dtRepresentative.Columns.Add(dcId);
                    dtRepresentative.Columns.Add(dcDescription);

                    for (int i = 0; i < oPatientRepresentatives.Count; i++)
                    {
                        DataRow drTemp = dtRepresentative.NewRow();
                        drTemp["Id"] = oPatientRepresentatives[i].PRId;
                        drTemp["Description"] = oPatientRepresentatives[i].FirstName + " " + oPatientRepresentatives[i].LastName;
                        dtRepresentative.Rows.Add(drTemp);
                    }
                   // cmbPatientRepresentative.Items.Clear();
                    cmbPatientRepresentative.DataSource = null;
                    cmbPatientRepresentative.Items.Clear();
                    cmbPatientRepresentative.ValueMember = dtRepresentative.Columns["Id"].ColumnName;
                    cmbPatientRepresentative.DisplayMember = dtRepresentative.Columns["Description"].ColumnName;
                    cmbPatientRepresentative.DataSource = dtRepresentative;
                }
                else
                {
                   // cmbPatientRepresentative.Items.Clear();
                    cmbPatientRepresentative.DataSource = null;
                    cmbPatientRepresentative.Items.Clear();
                }
                //RemovePatientRepresentative();
                //_isPatientRepresentativeModified = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }
        }
        private void RemovePatientRepresentative()
        {
            //SLR: Changed the logic on 7/30/2014
            try
            {
                if (ogloPatientRepresentativeControl != null)
                {
                    if (pnlPatientRepresentative.Controls.Contains(ogloPatientRepresentativeControl))
                    {
                        pnlPatientRepresentative.Controls.Remove(ogloPatientRepresentativeControl);
                      
                    }
                    try
                    {
                        ogloPatientRepresentativeControl.SaveButton_Click -= new gloPatientRepresentativeControl.SaveButtonClick(ogloPatientRepresentativeControl_SaveButton_Click);
                        ogloPatientRepresentativeControl.CloseButton_Click -= new gloPatientRepresentativeControl.CloseButtonClick(ogloPatientRepresentativeControl_CloseButton_Click);
                    }
                    catch
                    {
                    }
                    ogloPatientRepresentativeControl.Dispose();
                    ogloPatientRepresentativeControl = null;
                }
            }
            catch //(Exception ex)
            {

            }
        }

        private void ogloPatientRepresentativeControl_CloseButton_Click(object sender, EventArgs e)
        {
            RemovePatientRepresentative();
        }
        //Close list control
        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            //SLR: Changed the logic on 7/30/2014
            removeListControl();
            tls_btnAdd.Visible = true;
            tls_btnSave.Visible = true;
            tls_btnRemove.Visible = true;
            tls_btnBrowse.Visible = true;

        }





        private void tls_btnSendInvitation_Click(object sender, EventArgs e)
        {
            if ((email.Trim() == "") && (zip.Trim() == ""))
            {
                MessageBox.Show("You must enter a valid Email address and a Zip Code in Patient Demographics Screen to send Patient Invitation.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (email.Trim() == "")
            {
                MessageBox.Show("You must enter a valid Email address in Patient Demographics Screen to send Patient Invitation.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (zip.Trim() == "")
            {
                MessageBox.Show("You must enter a Zip Code in Patient Demographics Screen to send Patient Invitation.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Int64 nPatientProviderID = gloGlobal.TIN.clsSelectProviderTaxID.getPatientProviderID(nPatientId);
            Int64 nProviderID = gloGlobal.gloPMGlobal.LoginProviderID == 0 ? nPatientProviderID : gloGlobal.gloPMGlobal.LoginProviderID;
            if (!getProviderTaxID(nProviderID))
            {
                return;
            }
            if (UpdateEmail(email.Trim()))
            {
                this.Cursor = Cursors.WaitCursor;
                IsMailSend = false;
                SendPatientPortalEmails(nPatientId);
                Boolean IsFirstTimeInvite = false;
                if (oPatientPortalAccountStatus == PatientPortalAccountStatus.PatientNotInvited)
                    IsFirstTimeInvite = true;
                GetPatientPortalStatus();
                if (IsMailSend)
                {
                    Int64 nMessageQueueID = GetMessageQueueOrPoratlAccessID(nPatientId, 0);
                    gloGlobal.TIN.clsSelectProviderTaxID oclsselectProviderTaxID = new gloGlobal.TIN.clsSelectProviderTaxID(nProviderID);
                    oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, nMessageQueueID, sProviderTaxID, nProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.PatientPortalInvitation);
                    oclsselectProviderTaxID = null;

                    if (IsFirstTimeInvite)
                        MessageBox.Show("Invitation to Patient has been sent.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Invitation to Patient has been resent.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Invitation to Patient has been failed. Please contact your System Administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                this.Cursor = Cursors.Arrow;
            }
            else
            {
                MessageBox.Show("Error updating Patient Demographics", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPatientID">Patient ID</param>
        /// <param name="nRetriveID">0: retrive nMessageID from Gl_MessageQueue.
        /// 1: retrive nPortalAccessID from PateintPortalAccess.</param>
        /// <returns></returns>
        private long GetMessageQueueOrPoratlAccessID(Int64 nPatientID, int nRetriveID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            string _sqlQuery = "";
            Int64 nID = 0;
            try
            {
                oDB.Connect(false);
                if (nPatientId > 0)  //Warning Removed at the time of Change made to solve memory Leak and word crash issue
                {
                    //Bug #93369: 00001074: setup Appointment
                    if (nRetriveID == 0)
                    {
                        _sqlQuery = " SELECT nMessageID as nMessageID FROM dbo.Gl_Messagequeue WHERE nPatientID =  " + nPatientId + "AND sMessageName='PATIENTINVITATION' AND sServiceName = 'PatientPortal' AND nStatus IN (1, 0) ";
                    }
                    else if (nRetriveID==1)
                    {
                        _sqlQuery = " SELECT nPatientPortalAccessID as nPatientPortalAccessID  FROM dbo.PatientPortalAccess WHERE nPatientID =  " + nPatientId + "AND bIsBlocked=1 AND bIsQuickActivated=1"; 
                    }
                    oDB.Retrive_Query(_sqlQuery, out dt);
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    if (nRetriveID == 0)
                    {
                        nID = Convert.ToInt64(dt.Rows[0]["nMessageID"]);
                    }
                    else if (nRetriveID == 1)
                    {
                        nID = Convert.ToInt64(dt.Rows[0]["nPatientPortalAccessID"]); 
                    } 
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }

            }
            return nID;
        }

        public bool getProviderTaxID(Int64 nProviderID = 0)
        {
            sProviderTaxID = "";
            nProviderAssociationID = 0;
            try
            {
                DialogResult oResult = System.Windows.Forms.DialogResult.OK;
                gloGlobal.frmSelectProviderTaxID oForm = new gloGlobal.frmSelectProviderTaxID(Convert.ToInt64(nProviderID));
                if (oForm.dtProviderTaxIDs != null && oForm.dtProviderTaxIDs.Rows.Count > 1)
                {
                    oForm.ShowDialog(this);
                    oResult = oForm.DialogResult;
                    nProviderAssociationID = oForm.nAssociationID;
                    sProviderTaxID = oForm.sProviderTaxID;

                    oForm = null;
                }
                else if (oForm.dtProviderTaxIDs != null && oForm.dtProviderTaxIDs.Rows.Count == 1)
                {
                    //'oResult = oForm.DialogResult
                    nProviderAssociationID = Convert.ToInt64(oForm.dtProviderTaxIDs.Rows[0]["nAssociationID"]);
                    sProviderTaxID = Convert.ToString(oForm.dtProviderTaxIDs.Rows[0]["sTIN"]);
                    oForm = null;
                }
                else
                {
                    nProviderAssociationID = 0;
                    sProviderTaxID = "";
                }

                if (oResult == System.Windows.Forms.DialogResult.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;

            }
            finally
            {
            }
        }
        Boolean IsMailSend = false;
        string password = "";
        string _encryptionKey = "12345678";
        private void tls_btnQuickActivate_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                    if (MessageBox.Show("Do you want to quick activate the patient?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                
                if (txtUserName.Text.Trim() == "")
                    txtUserName.Text = email;
                  
                 //Commented to Quick activate the patient without mail address
                //if ((txtUserName.Text.Trim() == "") && (zip.Trim() == ""))
                //{
                //    MessageBox.Show("You must enter a valid Email address and a Zip Code in Patient Demographics Screen for Quick Activation.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                if (txtUserName.Text.Trim() == "")
                {
                    txtUserName.Text = GetRandomUserName();
                    //MessageBox.Show("You must enter a valid Email address in Patient Demographics Screen for Quick Activation.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;

                }
                if (zip.Trim() == "")
                {
                    MessageBox.Show("You must enter a Zip Code in Patient Demographics Screen to send Patient Invitation.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                if (UpdateEmail(email.Trim()))
                {
                    this.Cursor = Cursors.WaitCursor;
                    string temppassword = "";
                    password = GetRandomNumber();
                    temppassword = password;

                    gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
                    password = oClsEncryption.EncryptToBase64String(password, _encryptionKey);
                    //SLR: Changes on 7/30/2014
                    oClsEncryption.Dispose();
                    oClsEncryption = null;

                    DataTable dtPatientPortalAccess = new DataTable();
                    dtPatientPortalAccess.Columns.Add("PatientUID");
                    dtPatientPortalAccess.Columns.Add("AppCode");
                    dtPatientPortalAccess.Columns.Add("AppType");
                    dtPatientPortalAccess.Columns.Add("Onlineaccess");
                    dtPatientPortalAccess.Columns.Add("PortalAccessUserName");
                    dtPatientPortalAccess.Columns.Add("PortalAccessPassword");
                    dtPatientPortalAccess.Columns.Add("UserFullName");
                    dtPatientPortalAccess.Columns.Add("IsSuccess");
                    dtPatientPortalAccess.Columns.Add("Description");
                    dtPatientPortalAccess.Columns.Add("StrPortalAccessPassword");
                    dtPatientPortalAccess.Columns.Add("SecurityQuestion");
                    dtPatientPortalAccess.Columns.Add("SecurityAnswer");
                    dtPatientPortalAccess.Columns.Add("IsBlocked");
                    dtPatientPortalAccess.Columns.Add("GUID");
                    dtPatientPortalAccess.Columns.Add("LoginUserType");



                    dtPatientPortalAccess.Rows.Add(nPatientId, "", "PatientPortal", 1, txtUserName.Text, password, "", 0, "", password, "", "", 1, 0, 0);

                    DataTable dtPatient_Profile = new DataTable();
                    dtPatient_Profile.Columns.Add("PatientUID");
                    dtPatient_Profile.Columns.Add("nTransactionID");
                    dtPatient_Profile.Columns.Add("sFieldName");
                    dtPatient_Profile.Columns.Add("sFieldValue");

                    //SLR: Changes on 7/30/2014

                    long loginID = 0;
                    string loginName = string.Empty;

                    if (appSettings["UserID"] != null)
                    {
                        if (appSettings["UserID"] != "")
                        { loginID = Convert.ToInt64(appSettings["UserID"]); }
                    }

                    if (appSettings["UserName"] != null)
                    {
                        if (appSettings["UserName"] != "")
                        { loginName = Convert.ToString(appSettings["UserName"]); }
                    }

                    Int64 _ClinicID = 0;
                    if (appSettings["ClinicID"] != null)
                    {
                        if (appSettings["ClinicID"] != "")
                        { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                        else { _ClinicID = 1; }
                    }
                    else
                    { _ClinicID = 1; }

                    DataTable dtPatientPortalStatus = null;
                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters odbParams = default(gloDatabaseLayer.DBParameters);
                    oDB.Connect(false);
                    odbParams = new gloDatabaseLayer.DBParameters();
                    odbParams.Add("@TVP", dtPatientPortalAccess, ParameterDirection.Input, SqlDbType.Structured);
                    odbParams.Add("@TVP2", dtPatient_Profile, ParameterDirection.Input, SqlDbType.Structured);
                    odbParams.Add("@IsInsert", 0, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@PracticeID", "", ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@IsQuickActivated", 1, ParameterDirection.Input, SqlDbType.Bit);
                    odbParams.Add("@nEMRloginID", loginID, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@sEMRloginName", loginName, ParameterDirection.Input, SqlDbType.VarChar);
                    odbParams.Add("@nClinicId", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@sMachineName", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.Retrive("WS_InsertPatientPortalAccess", odbParams, out dtPatientPortalStatus);
                    oDB.Disconnect();
                    //SLR: Changes on 7/30/2014
                    oDB.Dispose();
                    oDB = null;
                    odbParams.Dispose();
                    odbParams = null;
                    if (dtPatientPortalStatus != null)
                    {
                        if (dtPatientPortalStatus.Rows.Count > 0)
                        {
                            if (dtPatientPortalStatus.Rows[0][0].ToString() == "-1")
                            {

                                this.Cursor = Cursors.Arrow;
                                pnlValidation.Visible = true;
                                pnlValidation.BringToFront();
                                pnlUserName.Visible = true;
                                lblGIHeader.Text = "  Portal login username";

                                pnlPatientBlock.Visible = false;
                                // MessageBox.Show("The current patient email address \"" + txtUserName.Text.Trim() + "\" has been chosen as portal login username for some other patient. They are allowed to choose same email address for portal communication but need to provide a different portal login username.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MessageBox.Show("The entered portal login username is already used. Please enter different username", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else if (dtPatientPortalStatus.Rows[0][0].ToString() == "0")
                            {
                                pnlValidation.Visible = false;

                                this.Cursor = Cursors.Arrow;
                                MessageBox.Show("Patient already activated", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;

                            }
                            else if (dtPatientPortalStatus.Rows[0][0].ToString() != "")
                            {
                                Int64 nPatientProviderID = gloGlobal.TIN.clsSelectProviderTaxID.getPatientProviderID(nPatientId);
                                Int64 nProviderID = gloGlobal.gloPMGlobal.LoginProviderID == 0 ? nPatientProviderID : gloGlobal.gloPMGlobal.LoginProviderID;
                                if (!getProviderTaxID(nProviderID))
                                {
                                    return;
                                }
                                lblUserName.Text = txtUserName.Text;
                                lblPassword.Text = temppassword;
                                pnlValidation.Visible = false;

                                ShowbtnSendInvitation = false;
                                GetPatientPortalStatus();
                                tabControl1.Select();
                                tabControl1.SelectedTab = tabPage1;
                                tabControl1.SelectTab(0);
                                MessageBox.Show("You have activated the Patient successfully.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //tls_btnPrint.Visible = true;
                                
                                Int64 nMessageQueueID = GetMessageQueueOrPoratlAccessID(nPatientId,1);
                                gloGlobal.TIN.clsSelectProviderTaxID oclsselectProviderTaxID = new gloGlobal.TIN.clsSelectProviderTaxID(nProviderID);
                                oclsselectProviderTaxID.InsertProviderTaxID(nProviderAssociationID, nMessageQueueID, sProviderTaxID, nProviderID, 0, gloGlobal.TIN.clsSelectProviderTaxID.TransactionType.PatientPortalQuickActivate);
                                oclsselectProviderTaxID = null;

                                pnlPortalLoginCredentials.Visible = true;
                                lblPortalLoginCredentials.Visible = true;

                            }

                        }
                        //SLR: Changes on 7/30/2014
                        dtPatientPortalStatus.Dispose();
                        dtPatientPortalStatus = null;
                    }
                    //SLR: Changes on 7/30/2014
                    dtPatient_Profile.Dispose();
                    dtPatient_Profile = null;
                    dtPatientPortalAccess.Dispose();
                    dtPatientPortalAccess = null;

                    this.Cursor = Cursors.Arrow;
                }
                else
                {
                    MessageBox.Show("Error updating Patient Demographics", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            finally
            {
            }
        }
        private void tls_btnResetTempPassword_Click(object sender, EventArgs e)
        {
            //         DialogResult _result;

            if (MessageBox.Show("Are you sure you want to reset current patient login password?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                pnlPortalLoginCredentials.Visible = false;
                lblPortalLoginCredentials.Visible = false;
                return;
            }
            string temppassword = "";
            password = GetRandomNumber();
            temppassword = password;
            gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
            password = oClsEncryption.EncryptToBase64String(password, _encryptionKey);
            //SLR: Changes on 7/30/2014
            oClsEncryption.Dispose();
            oClsEncryption = null;

            //SLR: Changes on 7/30/2014
            DataTable dtPatientPortalAccess = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = default(gloDatabaseLayer.DBParameters);
            try
            {
                oDB.Connect(false);
                odbParams = new gloDatabaseLayer.DBParameters();
                odbParams.Add("@nPatientID", nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@sPortalPassword", password, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("gsp_UP_PatientPortalAccess", odbParams, out dtPatientPortalAccess);
                oDB.Disconnect();

                if (dtPatientPortalAccess != null)
                {
                    if (dtPatientPortalAccess.Rows.Count > 0)
                    {
                        if (dtPatientPortalAccess.Rows[0][0].ToString() == "1")
                        {
                            MessageBox.Show("You have reset the password successfully", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            pnlPortalLoginCredentials.Visible = true;
                            lblPortalLoginCredentials.Visible = true;
                            lblUserName.Text = txtUserName.Text;
                            lblPassword.Text = temppassword;
                            tabControl1.Select();
                            tabControl1.SelectedTab = tabPage1;
                            tabControl1.SelectTab(0);
                            //tls_btnResetTempPassword.Visible = false;
                        }
                        else if (dtPatientPortalAccess.Rows[0][0].ToString() == "-1")
                        {
                            pnlPortalLoginCredentials.Visible = false;
                            lblPortalLoginCredentials.Visible = false;
                            ShowbtnResetTempPassword = false;
                            tls_btnResetTempPassword.Visible = false;
                            MessageBox.Show("Patient already logged in to the Portal. This feature functions only until the Patient logs in to the Portal.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                    }
                }
                //SLR: Changes on 7/30/2014
                dtPatientPortalAccess.Dispose();
                dtPatientPortalAccess = null;
            }
            finally
            {
                //SLR: Changes on 7/30/2014
                oDB.Dispose();
                oDB = null;
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
            }
        }
        private void tls_btnPrint_Click(object sender, EventArgs e)
        {
        }
        private void btnPrintLoginCredentials_Click(object sender, EventArgs e)
        {
             MUAccessStatus = mskDOInfoProvided.Checked;
           
            PrintReport();

        }

        private void PrintReport()
        {
            gloSSRSApplication.clsPrintReport clsPrntRpt = null;
            string _MessageBoxCaption = string.Empty;
            string _databaseConnectionString = string.Empty;
            string _LoginName = string.Empty;
            string gstrSQLServerName = string.Empty;
            string gstrDatabaseName = string.Empty;
            bool gblnSQLAuthentication = false;
            string gstrSQLUserEMR = string.Empty;
            string gstrSQLPasswordEMR = string.Empty;
            bool gblnDefaultPrinter = false;
            //SLR: Changes on 7/30/2014
            //SqlConnection Con = new SqlConnection(_databaseConnectionString);


            try
            {
                if (appSettings["DataBaseConnectionString"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["DataBaseConnectionString"]))
                    {
                        _databaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                        //SLR: Changes on 7/30/2014
                        //Con = new SqlConnection(_databaseConnectionString);
                    }
                }

                if (appSettings["UserName"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["UserName"]))
                    {
                        _LoginName = Convert.ToString(appSettings["UserName"]);
                    }
                }

                if (appSettings["SQLServerName"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["SQLServerName"]))
                    {
                        gstrSQLServerName = Convert.ToString(appSettings["SQLServerName"]);
                    }
                }

                if (appSettings["DatabaseName"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["DatabaseName"]))
                    {
                        gstrDatabaseName = Convert.ToString(appSettings["DatabaseName"]);
                    }
                }

                if (appSettings["SQLLoginName"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["SQLLoginName"]))
                    {
                        gstrSQLUserEMR = Convert.ToString(appSettings["SQLLoginName"]);
                    }
                }

                if (appSettings["SQLPassword"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["SQLPassword"]))
                    {
                        gstrSQLPasswordEMR = Convert.ToString(appSettings["SQLPassword"]);
                    }
                }

                if (appSettings["DefaultPrinter"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["DefaultPrinter"]))
                    {
                        gblnDefaultPrinter = !Convert.ToBoolean(appSettings["DefaultPrinter"]);
                    }
                }

                if (appSettings["WindowAuthentication"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["WindowAuthentication"]))
                    {
                        gblnSQLAuthentication = !Convert.ToBoolean(appSettings["WindowAuthentication"]);
                    }
                }


                string ParameterValue = null;
                ParameterValue = nPatientId.ToString() + "," + lblPassword.Text;

                string ParameterName = null;
                ParameterName = "nPatientId,Password";


                clsPrntRpt = new gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR);
                clsPrntRpt.PrintReport("rptPatientPortalLoginCredentials", ParameterName, ParameterValue, gblnDefaultPrinter, "");
                //SLR: Changes on 7/30/2014
                //clsPrntRpt.Dispose();
                clsPrntRpt = null;
                EnableMUAccess();
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PrintLoginCredentials, gloAuditTrail.ActivityType.Print, "Patient portal login credentials printed.MU access status before Print :- " + MUAccessStatus + ", and after print :- " + mskDOInfoProvided.Checked + "", nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
        private void EnableMUAccess()
        {
            if (email == null || email == "")
            {
                if (lblUserName.Text != null && lblUserName.Text != "")
                {
                    mskDOInfoProvided.Checked = true;
                }

            }
        
        }
        private Boolean _PatientBlocked;
        private Boolean PatientBlocked
        {
            get { return _PatientBlocked; }
            set
            {
                _PatientBlocked = value;
                if (_PatientBlocked)
                {
                    tls_btnActivatePatient.Text = "Unblock/Active";
                    txtBlockReason1.Visible = true;
                    //lblBlockDate.Visible = true;
                    lblBlockReason1.Visible = true;
                    //lblBlockDate1.Visible = true;
                }
                else
                {
                    tls_btnActivatePatient.Text = "Block/InActive";
                    txtBlockReason1.Visible = false;
                    lblBlockDate.Visible = false;
                    lblBlockReason1.Visible = false;
                    lblBlockDate1.Visible = false;
                }
            }
        }
        private Boolean ShowbtnActivate;
        private Boolean ShowbtnQuickActivate;
        private Boolean ShowbtnResetTempPassword;
        private Boolean ShowbtnSendInvitation;
        private enum PatientPortalAccountStatus
        {
            PatientNotInvited = 0,
            PatientInvited = 1,
            PatientActivated = 2,
            PatientBlocked = 3
        }

        private void btnBlockProceed_Click(object sender, EventArgs e)
        {
            try
            {


                //SLR: Changes on 7/30/2014
                DataTable dtPatientPortalStatus = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters odbParams = default(gloDatabaseLayer.DBParameters);
                oDB.Connect(false);
                odbParams = new gloDatabaseLayer.DBParameters();
                odbParams.Add("@nPatientID", nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
               


                if (!PatientBlocked)
                {
                    odbParams.Add("@BlockPatient", 1, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@UnblockPatient", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@nBlockUserId", nUserId, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@sBlockReason", txtBlockReason.Text.Trim(), ParameterDirection.Input, SqlDbType.VarChar);

                }
                else
                {
                    odbParams.Add("@BlockPatient", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@UnblockPatient", 1, ParameterDirection.Input, SqlDbType.BigInt);
                    odbParams.Add("@nBlockUserId", nUserId, ParameterDirection.Input, SqlDbType.BigInt);
                }
                oDB.Retrive("gsp_INUP_PatientPortalStatus", odbParams, out dtPatientPortalStatus);
                oDB.Disconnect();
                //SLR: Changes on 7/30/2014
                oDB.Dispose();
                oDB = null;
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
                if (dtPatientPortalStatus != null)
                {
                    if (dtPatientPortalStatus.Rows.Count > 0)
                    {
                        if (dtPatientPortalStatus.Rows[0][0].ToString() == "1")
                        {
                            if (PatientBlocked)
                            {
                                PatientBlocked = false;
                                MessageBox.Show("Patient has been unblocked/activated", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                tls_Top.Enabled = true;
                                pnlNotes.Enabled = true;
                                pnlValidation.Visible = false;
                                PatientBlocked = true;
                                MessageBox.Show("Patient has been blocked/inactivated", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            GetPatientPortalStatus();

                        }
                        else
                        {

                        }

                    }
                    //SLR: Changes on 7/30/2014
                    dtPatientPortalStatus.Dispose();
                    dtPatientPortalStatus = null;
                }
            }
            finally
            {
            }

        }
        private string GetRandomNumber()
        {

            var chars = "";
            for (int i = 65; i < 91; i++)
            {
                chars += char.ConvertFromUtf32(i);
            }
            for (int i = 97; i < 123; i++)
            {
                chars += char.ConvertFromUtf32(i);
            }
            for (int i = 48; i < 58; i++)
            {
                chars += char.ConvertFromUtf32(i);
            }

            var random = new Random();

            var result = new string(

                Enumerable.Repeat(chars, 7)

                          .Select(s => s[random.Next(s.Length)])

                          .ToArray());

            chars = "";


            chars += char.ConvertFromUtf32(33);
            chars += char.ConvertFromUtf32(35);
            chars += char.ConvertFromUtf32(36);
            chars += char.ConvertFromUtf32(37);
            chars += char.ConvertFromUtf32(38);
            chars += char.ConvertFromUtf32(42);
            chars += char.ConvertFromUtf32(64);


            var random1 = new Random();

            var result1 = new string(

                Enumerable.Repeat(chars, 1)

                          .Select(s => s[random1.Next(s.Length)])

                          .ToArray());

            var t1 = new Random();
            var t2 = t1.Next(0, 7);

            result = result.Insert(t2, result1);

            if (result.Contains("@"))
            {
                result = result.Replace("@", "$");
            }
            return result;

        }
        private string GetRandomUserName()
        {
            string username = "";
            try
            {
                string DateOFBirth = PatientDOB.Replace("/", "");
                //string uniquenumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() ;
                username = PatientLastname + PatientFirstname + DateOFBirth + zip;
                
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.GenerateUserName, gloAuditTrail.ActivityType.Add, "Portal username generated without Mail ID.", nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success);

                return username;
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return "";
            }
           
        }

        private void btnblockcancel_Click(object sender, EventArgs e)
        {
            tls_Top.Enabled = true;
            pnlNotes.Enabled = true;
            pnlValidation.Visible = false;
        }
        private void tls_btnActivatePatient_Click(object sender, EventArgs e)
        {

            if (PatientBlocked)
            {
                btnBlockProceed_Click(sender, e);

            }
            else
            {
                tls_Top.Enabled = false;
                pnlNotes.Enabled = false;
                pnlValidation.Visible = true;
                pnlValidation.BringToFront();
                txtBlockReason.Text = "";
                pnlUserName.Visible = false;
                pnlPatientBlock.Visible = true;
                lblGIHeader.Text = "  Patient Block/InActivate Reason";
            }
        }


        PatientPortalAccountStatus oPatientPortalAccountStatus = new PatientPortalAccountStatus();
        private void GetPatientPortalStatus()
        {
            lblPortalAccountStatus.Text = "";
            lblDateOfInvitation.Text = "";
            lblDateOfActivation.Text = "";
            lblDateOfLastLogin.Text = "";
            txtBlockReason1.Text = "";
            lblBlockDate.Text = "";
            lblPortalUserName.Text = "";
            txtBlockReason1.ScrollBars = ScrollBars.None;
            txtBlockReason1.BorderStyle = BorderStyle.None;
            ShowbtnResetTempPassword = false;
            ShowbtnQuickActivate = false;
            ShowbtnActivate = false;
            if (nPatientId != 0)
            {
                //SLR: Changes on 7/30/2014
                DataTable dtPatientPortalStatus = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters odbParams = default(gloDatabaseLayer.DBParameters);
                try
                {
                    oDB.Connect(false);
                    odbParams = new gloDatabaseLayer.DBParameters();
                    odbParams.Add("@nPatientID", nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("gsp_PatientPortalStatus", odbParams, out dtPatientPortalStatus);
                    oDB.Disconnect();
                    //SLR: Changes on 7/30/2014
                    oDB.Dispose();
                    oDB = null;
                    if (odbParams != null)
                    {
                        odbParams.Dispose();
                        odbParams = null;
                    }
                    if (dtPatientPortalStatus != null)
                    {
                        if (dtPatientPortalStatus.Rows.Count > 0)
                        {
                            if (dtPatientPortalStatus.Rows[0]["PatientPortalStatus"] != null)
                            {
                                lblPortalAccountStatus.Text = dtPatientPortalStatus.Rows[0]["PatientPortalStatus"].ToString();
                            }
                            if (dtPatientPortalStatus.Rows[0]["DateofInvitation"] != null)
                            {
                                lblDateOfInvitation.Text = dtPatientPortalStatus.Rows[0]["DateofInvitation"].ToString();
                            }
                            if (dtPatientPortalStatus.Rows[0]["DateofActivation"] != null)
                            {
                                lblDateOfActivation.Text = dtPatientPortalStatus.Rows[0]["DateofActivation"].ToString();
                            }
                            if (dtPatientPortalStatus.Rows[0]["DateofLastAccessDate"] != null)
                            {
                                lblDateOfLastLogin.Text = dtPatientPortalStatus.Rows[0]["DateofLastAccessDate"].ToString();
                            }
                            if (dtPatientPortalStatus.Rows[0]["PatientLoginName"] != null)
                            {
                                lblPortalUserName.Text = dtPatientPortalStatus.Rows[0]["PatientLoginName"].ToString();
                            }
                            if (dtPatientPortalStatus.Rows[0]["PatientBlocked"] != null)
                            {
                                if (dtPatientPortalStatus.Rows[0]["PatientBlocked"].ToString() == "0")
                                {
                                    PatientBlocked = false;
                                }
                                else
                                {
                                    PatientBlocked = true;
                                    if (dtPatientPortalStatus.Rows[0]["PatientBlockReason"] != null)
                                    {
                                        if (dtPatientPortalStatus.Rows[0]["PatientBlockReason"].ToString().Trim() != "")
                                        {
                                            txtBlockReason1.Text = dtPatientPortalStatus.Rows[0]["PatientBlockReason"].ToString();
                                            int ind = -1;
                                            int stind = 0;
                                            int count = 0;
                                            ind = txtBlockReason1.Text.IndexOf("\r\n", stind);
                                            stind = ind + 1;
                                            while (ind != -1)
                                            {
                                                count += 1;
                                                ind = txtBlockReason1.Text.IndexOf("\r\n", stind);
                                                stind = ind + 1;
                                            }
                                            if (count > 3)
                                            {
                                                txtBlockReason1.ScrollBars = ScrollBars.Both;
                                                txtBlockReason1.BorderStyle = BorderStyle.FixedSingle;
                                            }
                                        }
                                        else
                                        {
                                            txtBlockReason1.Text = "Not specified.";
                                        }

                                    }
                                    if (dtPatientPortalStatus.Rows[0]["PatientBlockDate"] != null)
                                    {
                                        lblBlockDate.Text = dtPatientPortalStatus.Rows[0]["PatientBlockDate"].ToString();
                                    }
                                }
                            }

                            if (dtPatientPortalStatus.Rows[0]["IsQuickActivated"] != null)
                            {
                                if (Convert.ToBoolean(dtPatientPortalStatus.Rows[0]["IsQuickActivated"]))
                                {
                                    lblDateOfActivation.Text += " (Quick Activated)";
                                }

                            }


                            if (dtPatientPortalStatus.Rows[0]["PatientLoginName"] != null)
                            {
                                txtUserName.Text = dtPatientPortalStatus.Rows[0]["PatientLoginName"].ToString();
                            }
                        }
                        //SLR: Changes on 7/30/2014
                        dtPatientPortalStatus.Dispose();
                        dtPatientPortalStatus = null;
                    }
                }
                finally
                {
                }

                if (lblPortalAccountStatus.Text.Trim().ToLower() == "Not Invited".ToLower())
                {
                    oPatientPortalAccountStatus = PatientPortalAccountStatus.PatientNotInvited;
                    SetPatientInvitation(_IsSaveAsCopy);
                    ShowbtnQuickActivate = true;
                }
                else if (lblPortalAccountStatus.Text.Trim().ToLower() == "Invited".ToLower())
                {
                    oPatientPortalAccountStatus = PatientPortalAccountStatus.PatientInvited;
                    SetPatientInvitation(_IsSaveAsCopy);
                    ShowbtnQuickActivate = true;
                }
                else if (lblPortalAccountStatus.Text.Trim().ToLower() == "Activated".ToLower())
                {
                    oPatientPortalAccountStatus = PatientPortalAccountStatus.PatientActivated;
                    ShowbtnActivate = true;
                    ShowbtnResetTempPassword = true;
                }
                else if (lblPortalAccountStatus.Text.Trim().ToLower() == "Blocked".ToLower())
                {
                    oPatientPortalAccountStatus = PatientPortalAccountStatus.PatientBlocked;
                    ShowbtnActivate = true;
                }
            }

            ShowPortalAccountToolStripButtons();


        }
        //Patient Portal

        long _ClinicID = 0;
        ClsMessageQueue oclsMessageQueue;
        Boolean gblnPatientPortalSendActivationEmail = true;
        Boolean gblnPatientPortalActivationEmailAlreadySent = false;
        //   long LoginID = 0;
        private Boolean UpdateEmail(string emailID)
        {
            try
            {

                //SLR: Changes on 7/30/2014
                DataTable dtPatientPortalStatus = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters odbParams = default(gloDatabaseLayer.DBParameters);
                oDB.Connect(false);
                odbParams = new gloDatabaseLayer.DBParameters();
                odbParams.Add("@nPatientID", nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@sEmail", emailID, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sZip", zip, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("gsp_UP_PatientPortalDemographicsUpdate", odbParams, out dtPatientPortalStatus);
                oDB.Disconnect();
                //SLR: Changes on 7/30/2014
                oDB.Dispose();
                oDB = null;
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
                if (dtPatientPortalStatus != null)
                {
                    if (dtPatientPortalStatus.Rows.Count > 0)
                    {
                        if (dtPatientPortalStatus.Rows[0][0].ToString() == "1")
                        {
                        }
                        else
                        {
                            //SLR: Changes on 7/30/2014
                            dtPatientPortalStatus.Dispose();
                            dtPatientPortalStatus = null;
                            return false;
                        }

                    }
                    //SLR: Changes on 7/30/2014
                    dtPatientPortalStatus.Dispose();
                    dtPatientPortalStatus = null;
                }
            }
            catch //(Exception ex)
            {
                return false;
            }
            finally
            {

            }
            return true;
        }

        private void SendPatientPortalEmails(Int64 PatientID)
        {
            if (gblnPatientPortalSendActivationEmail)
            {
                if ((oclsMessageQueue != null))
                {
                    oclsMessageQueue = null;
                }
                oclsMessageQueue = new ClsMessageQueue(_databaseconnectionstring, DateTime.Now, PatientID);
                IsMailSend = oclsMessageQueue.SendPortalEmails("PatientPortal", gblnPatientPortalSendActivationEmail, gblnPatientPortalActivationEmailAlreadySent);
                oclsMessageQueue = null;
            }

        }
        public void SetPatientInvitation(Boolean _IsSaveAsCopy)
        {

            if (_IsSaveAsCopy)
            {
                tls_btnSendInvitation.Text = "Send Invitation";
                gblnPatientPortalActivationEmailAlreadySent = false;
                ShowbtnSendInvitation = true;
            }
            else
            {
                Boolean EmailSent = false;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                //SLR: Changes on 7/30/2014
                DataTable dtPatientPortalEmail = null;
                try
                {
                    string _sqlQuery = "SELECT case when count(nMessageID)>0 then 1 else 0 end ";
                    _sqlQuery += " EmailSent FROM dbo.Gl_Messagequeue WHERE nPatientID = " + nPatientId + " AND sServiceName = 'PatientPortal' AND nStatus IN (1, 0) ";
                    oDB.Retrive_Query(_sqlQuery, out dtPatientPortalEmail);
                    oDB.Disconnect();
                    if (dtPatientPortalEmail != null)
                    {
                        if (dtPatientPortalEmail.Rows.Count > 0)
                        {
                            if (dtPatientPortalEmail.Rows[0]["EmailSent"] != null)
                            {
                                if (dtPatientPortalEmail.Rows[0]["EmailSent"].ToString() == "1")
                                {
                                    EmailSent = true;
                                }
                            }
                        }
                        //SLR: Changes on 7/30/2014
                        dtPatientPortalEmail.Dispose();
                        dtPatientPortalEmail = null;
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                }

                if (!EmailSent)
                {
                    tls_btnSendInvitation.Text = "Send Invitation";
                    gblnPatientPortalActivationEmailAlreadySent = false;
                    ShowbtnSendInvitation = true;
                }
                else
                {
                    tls_btnSendInvitation.Text = "Resend Invitation";
                    gblnPatientPortalActivationEmailAlreadySent = true;
                    ShowbtnSendInvitation = true;
                }
            }

            ShowPortalAccountToolStripButtons();

        }

        private void ShowPortalAccountToolStripButtons()
        {
            tls_btnActivatePatient.Visible = false;
            tls_btnQuickActivate.Visible = false;
            tls_btnResetTempPassword.Visible = false;
            tls_btnSendInvitation.Visible = false;
            if (ShowbtnActivate)
                tls_btnActivatePatient.Visible = true;
            if (ShowbtnQuickActivate)
                tls_btnQuickActivate.Visible = true;
            if (ShowbtnResetTempPassword)
                tls_btnResetTempPassword.Visible = true;
            if (ShowbtnSendInvitation)
                tls_btnSendInvitation.Visible = true;
        }
        private void showcontrols()
        {
            tls_btnActivatePatient.Visible = false;
            tls_btnQuickActivate.Visible = false;
            tls_btnResetTempPassword.Visible = false;
            tls_btnSendInvitation.Visible = false;

            tls_btnAdd.Visible = false;
            tls_btnRemove.Visible = false;
            tls_btnSave.Visible = false;
            tls_btnBrowse.Visible = false;

            if (tabControl1.SelectedTab == tabPage1)
            {
                ShowPortalAccountToolStripButtons();
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                tls_btnAdd.Visible = true;
                tls_btnRemove.Visible = true;
                tls_btnSave.Visible = true;
                tls_btnBrowse.Visible = true;
            }

        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showcontrols();

        }

        private void tls_btnSave_Click(object sender, EventArgs e)
        {
        }

        private void tls_Top_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {


            try
            {

                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        {
                            //SLR: Changes on 7/30/2014
                            if (ogloPatientRepresentativeControl != null)
                            {
                                ogloPatientRepresentativeControl.Save(sender, e);
                            }

                            break;
                        }
                    case "Cancel":
                        {
                            //SLR: Changes on 7/30/2014
                            if (oPatientPortalAccount != null)
                            {
                                oPatientPortalAccount.Dispose();
                                oPatientPortalAccount = null;
                            }

                            oPatientPortalAccount = new PatientPortalAccount();
                            if (mskDOInfoProvided.Checked == true)
                            {
                                oPatientPortalAccount.IsTrainingProvided = true;
                                oPatientPortalAccount.DateOfTraining = mskDOInfoProvided.Value;
                            }
                            else
                            {
                                oPatientPortalAccount.IsTrainingProvided = false;
                                oPatientPortalAccount.DateOfTraining = null;
                            }

                            if (ogloPatientRepresentativeControl.Cancel(sender, e))
                                this.Close();

                        }
                        break;
                    case "Add":
                        {
                            ogloPatientRepresentativeControl.Add(sender, e);
                        }
                        break;
                    case "Remove":
                        {
                            ogloPatientRepresentativeControl.RemovePR();
                        }
                        break;
                    case "Browse":
                        {
                            BrowsePR();
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

        //private bool FilloPatientPortalAccount()
        //{


        //    if (mskDOInfoProvided.MaskCompleted == true)
        //    {
        //        try
        //        {
        //            mskDOInfoProvided.TextMaskFormat = MaskFormat.IncludeLiterals;
        //            //Code review changes Replace IsValidDate() by gloDateMaster.gloDate.IsValidDateV2()
        //            if (gloDateMaster.gloDate.IsValidDateV2(mskDOInfoProvided.Text))
        //            {
        //                //if (Convert.ToDateTime(mtxtPADOB.Text).Date >= DateTime.Now.Date)
        //                //if (Convert.ToDateTime(mtxtPADOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtPADOB.Text).Date >= DateTime.Now.Date || Convert.ToDateTime(mtxtPADOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
        //                if (Convert.ToDateTime(mskDOInfoProvided.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mskDOInfoProvided.Text).Date > DateTime.Now.Date || Convert.ToDateTime(mskDOInfoProvided.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
        //                {
        //                    MessageBox.Show("Enter a valid date of training.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    //mtxtPADOB.Focus();
        //                    return false;

        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("Enter a valid date of training.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return false;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Enter a valid date of training.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //            ex = null;
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Enter a valid date of training.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return false;
        //    }

        //    oPatientPortalAccount = new PatientPortalAccount();
        //    if (mskDOInfoProvided.Text != "")
        //    {
        //        oPatientPortalAccount.DateOfTraining = Convert.ToDateTime(mskDOInfoProvided.Text);

        //    }
        //    else
        //    {
        //        oPatientPortalAccount.DateOfTraining = null;
        //    }
        //    if (chkDOInfoProvided.Checked == true)
        //    {
        //        oPatientPortalAccount.IsTrainingProvided = true;
        //    }
        //    else
        //    {
        //        oPatientPortalAccount.IsTrainingProvided = false;
        //    }

        //    return true;
        //}

        private void tls_btnBrowse_Click(object sender, EventArgs e)
        {
            tls_btnAdd.Visible = false;
            tls_btnSave.Visible = false;
            tls_btnRemove.Visible = false;
            tls_btnBrowse.Visible = false;

        }

        private void btnProceedUserName_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Enter username", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtUserName.Text.Contains(" "))
            {
                MessageBox.Show("No space(s) are allowed in username", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            tls_btnQuickActivate_Click(null, null);
        }

        private void btnCancelUserName_Click(object sender, EventArgs e)
        {
            txtUserName.Text = email;
            pnlValidation.Visible = false;
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 32)
            {
                e.Handled = true;
            }

        }

        private void btnSendLoginCredentialsEmail_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable _dt = null;
            string _sServiceuri = string.Empty;
            string _sPortalSiteName = string.Empty;
            ClsMessageQueue _ClsMessageQueue = new ClsMessageQueue();
            try
            {
                _ClsMessageQueue.ConnectionString = _databaseconnectionstring;
                _dt = _ClsMessageQueue.GetSetting("PatientPortalEmailService");
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    _sServiceuri = _dt.Rows[0]["sSettingsValue"].ToString();
                }
                _dt.Dispose();
                _dt = null;

                _dt = _ClsMessageQueue.GetSetting("PatientPortalSiteName");
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    _sPortalSiteName = _dt.Rows[0]["sSettingsValue"].ToString();
                }
                _dt.Dispose();
                _dt = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (_dt!=null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
            }

            clsgloPatientPortalEmail _clsgloPatientPortalEmail = new clsgloPatientPortalEmail(_databaseconnectionstring);
            bool IsMailSend = _clsgloPatientPortalEmail.SendEmail(nPatientId, _sServiceuri, _sPortalSiteName, _ClinicID, "FORGOT PASSWORD",false);
            this.Cursor = Cursors.Arrow;
            if (IsMailSend)
            {
                MessageBox.Show("Password has been sent successfully.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Password sending to patient has been failed. Please contact your System Administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }









    }
}
