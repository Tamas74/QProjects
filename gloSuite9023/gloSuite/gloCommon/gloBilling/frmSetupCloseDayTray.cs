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
    public partial class frmSetupCloseDayJournals : Form
    {
        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
    //    private Int64 ID = 0;
        public DataTable dt = null;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private Int64 _UserID = 0;

        private Int64 _RecordID = 0;
        string _sCode = "";
        bool _IsAdmin = false;

        #endregion " Declarations "

        #region " Property Procedures "

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }



        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public Int64 SelectedTrayID { get; set; }

        public string SelectedTrayName { get; set; }

        public bool IsOperationPerformed { get; set; }

        #endregion " Property Procedures "

        #region " Constructor "

        public frmSetupCloseDayJournals(Int64 ID, string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            #region ClinicID ID
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            #endregion

            //Get User ID
            #region User ID
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                //else
                //{ _UserID = 1; }
            }
            else
            { //_UserID = 1; 
            }
            #endregion

            _RecordID = ID;

            //...if record is opened for modify hide the save button only show saveNclose
            if (_RecordID > 0)
            {
                tsb_Save.Visible = false;
            }

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

        #endregion " Constructor "

        #region Private Methods

        private void FillData(Int64 ID)
        {
            try
            {
                gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                //string strquery = "Select nCloseDayTrayID, sCode, sDescription, nUserID, nNumberOfDays, nClinicID,bIsDefault from BL_CloseDayTray where nCloseDayTrayID = " + ID + " ";
                string strquery = "Select nCloseDayTrayID, sCode, sDescription, nUserID, nNumberOfDays, nClinicID,bIsDefault,isnull(nStartDate,0) as nStartDate,isnull(nEndDate,0) as nEndDate,IsNull(bIsActive,0) as bIsActive from BL_CloseDayTray WITH (NOLOCK) where nCloseDayTrayID = " + ID + " ";
                DataTable dtcloseday = new DataTable();
                ODB.Connect(false);
                ODB.Retrive_Query(strquery, out dtcloseday);
                if (dtcloseday != null)
                {
                    if (dtcloseday.Rows.Count > 0)
                    {
                        txtdescription.Text = dtcloseday.Rows[0]["sDescription"].ToString();
                        //this.dtpStartdate.ValueChanged -= new System.EventHandler(this.dtpStartdate_ValueChanged);
                        //this.dtpEndDate.ValueChanged -= new System.EventHandler(this.dtpStartdate_ValueChanged);
                        //numnoofdays.Value = Convert.ToDecimal(dtcloseday.Rows[0]["nNumberOfDays"].ToString());
                        txtcode.Text = dtcloseday.Rows[0]["sCode"].ToString();
                        //dtcloseday = dt.Copy();
                        _sCode = dtcloseday.Rows[0]["sCode"].ToString();
                        if (Convert.ToBoolean(dtcloseday.Rows[0]["bIsDefault"]) == true)
                        {
                            chkisdefault.Checked = true;
                        }
                        else
                        {
                            chkisdefault.Checked = false;
                        }
                        this.cmbUser.SelectedIndexChanged -= new System.EventHandler(this.cmbUser_SelectedIndexChanged);
                        if (_IsAdmin == true)
                        {
                            if (Convert.ToInt64(dtcloseday.Rows[0]["nUserID"]) > 0)
                            {
                                cmbUser.SelectedValue = Convert.ToInt64(dtcloseday.Rows[0]["nUserID"]);
                            }
                            else
                            {
                                cmbUser.SelectedIndex = -1;
                            }
                        }
                        if (Convert.ToInt64(dtcloseday.Rows[0]["nUserID"]) > 0)
                        {
                            
                                if (_UserID == Convert.ToInt64(dtcloseday.Rows[0]["nUserID"]))
                                {
                                    chkisdefault.Enabled = true;
                                }
                                else
                                {
                                    // chkisdefault.Enabled = false;
                                    //03/05/2010: Issue no GLO2010-0004683: commeneted for default selection user 
                                    chkisdefault.Enabled = true;
                                }
                                
                            
                        }

                        //MaheshB 20091119
                        if (ID > 0 && chkisdefault.Checked == true)
                        {
                           // chkisdefault.Enabled = false;
                            chkisdefault.Enabled = true;  // issue no GLO2010-0004683:
                        }
                        //MaheshB 20091120 For gloPM2010RTM1
                        if (ID == 0)
                        {
                            //chkIsActivated.Checked = true;
                            rbActive.Checked = true;
                        }
                        else
                        {
                            //chkIsActivated.Checked = Convert.ToBoolean(dtcloseday.Rows[0]["bIsActive"]);
                            if (Convert.ToBoolean(dtcloseday.Rows[0]["bIsActive"]) == true)
                            {
                                rbActive.Checked = true;
                            }
                            else
                            {
                                rbInactive.Checked = true;
                            }
                          
                        }

                        ////Added By Pramod Nair For Date Implementation
                        //dtpStartdate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtcloseday.Rows[0]["nStartDate"]));
                        //dtpEndDate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtcloseday.Rows[0]["nEndDate"]));   
                        ////End

                        //string _strquery = "select count(nCloseDayTrayID) as CountID from BL_Transaction_Payment_MST where nCloseDayTrayID = " + ID + " ";
                        //object _result = ODB.ExecuteScalar_Query(_strquery);

                        //_strquery="";
                        //_strquery = "select count(nCloseDayTrayID) as CountID from BL_Transaction_AdvancePayment_MST where nCloseDayTrayID = " + ID + " ";
                        //object _result1 = ODB.ExecuteScalar_Query(_strquery);

                        //if (_result != null)
                        //{
                        //    if ((int)_result > 0)
                        //    {
                        //        dtpStartdate.Enabled = false;
                        //        dtpStartdate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtcloseday.Rows[0]["nStartDate"]));
                        //    }
                        //}
                        //else
                        //{
                        //    dtpStartdate.Enabled = true;
                        //    dtpStartdate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtcloseday.Rows[0]["nStartDate"]));
                        //}


                        //if (_result1 != null)
                        //{
                        //    if ((int)_result1 > 0)
                        //    {
                        //        dtpStartdate.Enabled = false;
                        //        dtpStartdate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtcloseday.Rows[0]["nStartDate"]));
                        //    }
                        //}


                        //dtpEndDate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtcloseday.Rows[0]["nEndDate"]));
                        //End
                    }
                    else
                    {
                        string strcode = GeneratePatientCode();
                        txtcode.Text = strcode;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //return null;
            }
            finally
            {
                this.cmbUser.SelectedIndexChanged += new System.EventHandler(this.cmbUser_SelectedIndexChanged);
                this.dtpStartdate.ValueChanged += new System.EventHandler(this.dtpStartdate_ValueChanged);
                this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpStartdate_ValueChanged);
            }
        }

        private void FillUser()
        {
            string _SqlQuery = String.Empty;
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer ODB = null;
            gloDatabaseLayer.DBParameters oDbParameters = null;
            
            try
            {
                //Stored Procedure: gsp_GetUserList
                _SqlQuery = "gsp_GetUserList";
                oDbParameters = new gloDatabaseLayer.DBParameters();
                oDbParameters.Add(new gloDatabaseLayer.DBParameter("@nClinicId",this._ClinicID,ParameterDirection.Input,SqlDbType.BigInt));
                
                //if (_RecordID > 0)
                //{ oDbParameters.Add(new gloDatabaseLayer.DBParameter("@bIncludeBlockedUser", 1, ParameterDirection.Input, SqlDbType.TinyInt)); }
                //else
                //{ oDbParameters.Add(new gloDatabaseLayer.DBParameter("@bIncludeBlockedUser", 0, ParameterDirection.Input, SqlDbType.TinyInt)); }

                if (_RecordID > 0)
                {
                    oDbParameters.Add(new gloDatabaseLayer.DBParameter("@sTrayType", "PaymentTray", ParameterDirection.Input, SqlDbType.VarChar, 15));
                    oDbParameters.Add(new gloDatabaseLayer.DBParameter("@nTrayID", _RecordID, ParameterDirection.Input, SqlDbType.BigInt));
                }
                else
                {
                    oDbParameters.Add(new gloDatabaseLayer.DBParameter("@sTrayType", "PaymentTray", ParameterDirection.Input, SqlDbType.VarChar, 15));
                    oDbParameters.Add(new gloDatabaseLayer.DBParameter("@nTrayID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt));
                }

                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                ODB.Retrive(_SqlQuery, oDbParameters, out dt);
                //cmbUser.Items.Insert(0, "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.cmbUser.SelectedIndexChanged -= new System.EventHandler(this.cmbUser_SelectedIndexChanged);                               
                    cmbUser.DataSource = dt;
                    cmbUser.DisplayMember = dt.Columns["sLoginName"].ColumnName;
                    cmbUser.ValueMember = dt.Columns["nUserID"].ColumnName;
                }
                //Added By Debasish Das on 9th March 2010
                cmbUser.SelectedValue = _UserID;
                //******* Ends Here *********************

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.cmbUser.SelectedIndexChanged += new System.EventHandler(this.cmbUser_SelectedIndexChanged);

                if (oDbParameters != null) { oDbParameters.Clear(); oDbParameters.Dispose(); oDbParameters = null; };

                if (ODB != null)
                {
                    ODB.Disconnect();
                    ODB.Dispose();
                }
            }

        }

        private bool SaveData()
        {
            try
            {
                bool result = Validatedata();
                if (result == true)
                {
                    Int64 _selectedUserID = 0;

                    #region " get the user for which new tray will be added "
                    // Code added by Pankaj (15052010)
                    if (IsAdmin())
                    {
                        if (cmbUser != null)
                        {
                            if (cmbUser.SelectedValue != null)
                            {
                                _selectedUserID = Convert.ToInt64(cmbUser.SelectedValue);
                            }
                        }
                    }
                    else
                    { _selectedUserID = _UserID; }

                    #endregion   
                 
                    gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    ODB.Connect(false);
   
                    //To avoid same description for same user.
                    DataTable dttemp = new DataTable();

                    //QUERY updated by pankaj to resolve mantis issue (0001405: Patient Payment > Possible to Add Duplicate Tray. )
                    //string str = "Select nCloseDayTrayID from BL_CloseDayTray where  sDescription='" + txtdescription.Text.Replace("'", "''") + "' and nUserID='" + _UserID + "' and nCloseDayTrayID<>'" + _RecordID + "'";

                    string str = "Select nCloseDayTrayID from BL_CloseDayTray WITH (NOLOCK) where  sDescription='" + txtdescription.Text.Replace("'", "''") + "' and nUserID='" + _selectedUserID + "' and nCloseDayTrayID<>'" + _RecordID + "'";
                    ODB.Retrive_Query(str, out dttemp);
                    //
                    if (dttemp != null)
                    {
                        if (dttemp.Rows.Count <= 0)
                        {
                            // Added by Rahul Patel on 15/09/2010 
                            // Added Condition for checking change of Default payment tray
                            if (chkisdefault.Checked)
                            {
                                DataTable dtDefault = new DataTable();

                                string strQuery = "SELECT sDescription FROM BL_CloseDayTray WITH (NOLOCK) WHERE (bIsDefault = 1) AND nUserID='" + _selectedUserID + "' ";
                                ODB.Retrive_Query(strQuery, out dtDefault);
                                if (dtDefault != null)
                                { 
                                    if (dtDefault.Rows.Count > 0)
                                    {
                                        if (dtDefault.Rows[0]["sDescription"].ToString().Trim() != txtdescription.Text.Trim())
                                        {
                                            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to change default payment tray to '" + txtdescription.Text.Trim() + "'?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                                            {
                                                // int temp = 0;
                                                //if (chkisdefault.Checked = true)
                                                //{
                                                //    temp = 1;
                                                //}
                                                //Int64 _result = 0;
                                                //Added by Rahul Patel on 15/09/2010 .Call the function to add the payment tray.
                                                AddPaymentTray(ODB);
                                                //string strquery = "Update BL_CloseDayTray set sDescription='" + txtdescription.Text + "' where ID='" + Convert.ToInt64(dt.Rows[0]["nID"]) + "'";
                                                //ODB.Execute_Query(strquery);

                                                return true;
                                            }
                                            else
                                                return false;
                                        }
                                        else
                                        {
                                            AddPaymentTray(ODB);
                                            return true;
                                        }
                                    }
                                    else
                                    {
                                        //if (DialogResult.Yes == MessageBox.Show("As there is no default payment tray. Are you sure want to make is as default?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                                        if (DialogResult.Yes == MessageBox.Show("As there is no default payment tray, Are you sure you want to make this as default tray?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                                        {
                                            //Added by Rahul Patel on 15/09/2010 .Call the function to add the payment tray.
                                            AddPaymentTray(ODB);
                                            return true;
                                        }
                                        else
                                            return false;
                                    }
                                }
                            }
                            else
                            {
                                //Call function to add the payment tray details
                                AddPaymentTray(ODB);
                                return true;
                            }
                           
                          
                        }
                        else
                        {
                            MessageBox.Show("Duplicate description cannot be inserted. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                      // return false;
                    }
                }
                else
                {
                    //MessageBox.Show("Please enter Valid Data ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                return false;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                throw;
            }
        }
        //Added by Rahul Patel on 15/09/2010 .For adding the  Payment tray Details.
        private void AddPaymentTray(gloDatabaseLayer.DBLayer ODB)
        {
            DataTable dtGetDefaultPaymentTrayDescription = null;
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            //string strcode=ODB.
            oDBParameters.Add("@nCloseDayTrayID", _RecordID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
            oDBParameters.Add("@sCode", txtcode.Text, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
            oDBParameters.Add("@sDescription", txtdescription.Text, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
            if (_IsAdmin == true)
            {
                oDBParameters.Add("@nUserID", Convert.ToInt64(cmbUser.SelectedValue), ParameterDirection.Input, SqlDbType.BigInt);
            }
            else
            {
                oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
            }
            //oDBParameters.Add("@nNumberOfDays", numnoofdays.Value, ParameterDirection.Input, SqlDbType.Int);
            oDBParameters.Add("@nNumberOfDays", 0, ParameterDirection.Input, SqlDbType.Int);
            oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@MachineID", ODB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@bIsDefault", chkisdefault.Checked, ParameterDirection.Input, SqlDbType.BigInt);

            //Added By Pramod Nair For Date Implementation
            oDBParameters.Add("@nStartDt", gloDateMaster.gloDate.DateAsNumber(dtpStartdate.Value.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@nEndtDt", gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);

            oDBParameters.Add("@bIsClosed", 0, ParameterDirection.Input, SqlDbType.Bit);
            //End

            oDBParameters.Add("@bIsActive", Convert.ToBoolean(rbActive.Checked), ParameterDirection.Input, SqlDbType.Bit);
            ODB.Execute("BL_INUP_CLOSEDAYTRAY", oDBParameters);
            if (_RecordID > 0)
            {
                IsOperationPerformed = true;
                if (gloAccountsV2.gloBillingCommonV2.IsPaymentTrayActive(_RecordID))
                {
                    this.SelectedTrayID = _RecordID;
                    this.SelectedTrayName = txtdescription.Text;
                }
                else
                {
                    dtGetDefaultPaymentTrayDescription = gloAccountsV2.gloInsurancePaymentV2.GetDefaultPaymentTrayDescription(); 
                    if(dtGetDefaultPaymentTrayDescription != null && dtGetDefaultPaymentTrayDescription.Rows.Count > 0)
                    {
                        this.SelectedTrayID = Convert.ToInt64(dtGetDefaultPaymentTrayDescription.Rows[0]["nCloseDayTrayID"]);
                        this.SelectedTrayName = Convert.ToString(dtGetDefaultPaymentTrayDescription.Rows[0]["sDescription"]);
                    }
                }

            }
        } // AddPaymentTray

        private bool Validatedata()
        {
            bool result = false;
            if (txtdescription.Text.Trim() != "")
            {
                result = true;

                //if (dtpEndDate.Value.Date >= dtpStartdate.Value.Date)
                //{
                //    TimeSpan dtForValidate = dtpEndDate.Value.Date - dtpStartdate.Value.Date;
                //    if (dtForValidate.Days <= 364)
                //    {
                //        result = true;
                //    }
                //    else
                //    {
                //        MessageBox.Show("Please enter valid Date ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        result = false;
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Please enter valid Date ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    result = false;
                //}
                if (_IsAdmin == true)
                {
                    if (cmbUser.SelectedIndex < 0)
                    {
                        result = false;
                    }
                }
                if (chkisdefault.Checked == true && rbInactive.Checked == true)
                {
                    //Changed made by Rahul Patel on 15/09/2010
                    //Change the message box while changing the Default record to "InActive" state.
                    //MessageBox.Show("Default record should be Active. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Default record cannot be saved in the system as InActive. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    result = false;
                }

                Int64 _selecteduserid = 0;
                if (cmbUser != null)
                {
                    if (cmbUser.SelectedValue != null)
                    { _selecteduserid = Convert.ToInt64(cmbUser.SelectedValue); }

                    if (IsActiveUser(_selecteduserid) == false)
                    {
                        DialogResult dlgResult = MessageBox.Show(String.Format("User: '{0}' is currently blocked OR in-active. Continue Save?",Convert.ToString(cmbUser.Text)), _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (dlgResult == System.Windows.Forms.DialogResult.No)
                        {
                            result = false;
                        }
                    }
                }
                

            }
            else
            {
                MessageBox.Show("Enter valid description. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdescription.Text = "";
            }

            return result;
        }

        private bool IsActiveUser(Int64 userid)
        {
            bool retValue = false;
            gloDatabaseLayer.DBLayer odbLayer = null;
            gloDatabaseLayer.DBParameters odbParameters = null;
            Object outParamValue = null;

            try
            {
                odbParameters = new gloDatabaseLayer.DBParameters();
                odbParameters.Add(new gloDatabaseLayer.DBParameter("@nUserId", userid, ParameterDirection.Input, SqlDbType.BigInt));
                odbParameters.Add(new gloDatabaseLayer.DBParameter("@bIsActive", 0, ParameterDirection.InputOutput, SqlDbType.Bit));

                odbLayer = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                
                odbLayer.Connect(false);
                odbLayer.Execute("gsp_IsActiveUser", odbParameters, out outParamValue);
                odbLayer.Disconnect();

                Boolean.TryParse(Convert.ToString(outParamValue), out retValue);

            }
            catch (Exception ex)
            {
                retValue = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (outParamValue != null) { outParamValue = null; }
                if (odbParameters != null) { odbParameters.Clear(); odbParameters.Dispose(); odbParameters = null; }
                if (odbLayer != null) { odbLayer.Disconnect(); odbLayer.Dispose(); odbLayer = null; }
            }

            return retValue;
        }

        public bool IsAdmin()
        {
            string _SqlQuery = String.Empty;
            //DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer ODB = null;
            DataTable dt = null;
            try
            {
                _SqlQuery = "Select nUserID from User_MST WITH (NOLOCK) where nUserID='" + _UserID + "' and nClinicID='" + _ClinicID + "' and nAdministrator=1";
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                dt = new DataTable();
                ODB.Connect(false);
                ODB.Retrive_Query(_SqlQuery, out dt);
                if (dt != null)
                {
                    if (Convert.ToInt64(dt.Rows.Count) > 0 && Convert.ToInt64(dt.Rows[0]["nUserID"]) > 0)
                    {
                        _IsAdmin = true;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Disconnect();
                    ODB.Dispose();
                }
            }
            return _IsAdmin;
        }
        //private void updateData()
        //{
        //    try
        //    {
        //        if (dt != null)
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                if (dt.Rows[0]["description"].ToString() != txtdescription.Text || dt.Rows[0]["nNumberOfDays"].ToString() != txtnoofdays.Text)
        //                {
        //                    gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //                    ODB.Connect(false);
        //                    //To avoid same description for same user.
        //                    DataTable dttemp = new DataTable();
        //                    string str = "Select ID from BL_CloseDayTray where sDescription='" + txtdescription.Text.Replace("'","''") + "' and nUserID='" + _UserID + "'";
        //                    ODB.Retrive_Query(str, out dttemp);
        //                    //
        //                    if (dttemp != null)
        //                    {
        //                        if (dttemp.Rows.Count > 0)
        //                        {
        //                            Int64 _result = 0;
        //                            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

        //                            oDBParameters.Add("@nCloseDayTrayID", _RecordID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
        //                            oDBParameters.Add("@sCode", txtcode.Text.Replace("'", "''"), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //                            oDBParameters.Add("@sDescription", txtdescription.Text.Replace("'","''"), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
        //                            oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@nNumberOfDays",  txtnoofdays.Text,ParameterDirection.Input, SqlDbType.Int);
        //                            oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //                            oDBParameters.Add("@MachineID", ODB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
        //                            ODB.Execute("BL_INUP_CLOSEDAYTRAY", oDBParameters);
        //                            //string strquery = "Update BL_CloseDayTray set sDescription='" + txtdescription.Text + "' where ID='" + Convert.ToInt64(dt.Rows[0]["nID"]) + "'";
        //                            //ODB.Execute_Query(strquery);
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("Duplicate values cannot be inserted ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}

        #endregion

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            //    if(dt!=null)
            //    {
            //        if (dt.Rows.Count > 0)
            //        {
            //            updateData();
            //        }
            //        else
            //        {

            //    }
            //}
        }

        private void frmCloseDayTray_Load(object sender, EventArgs e)
        {
            _IsAdmin = IsAdmin();
            if (_IsAdmin == true)
            {
                //cmbUser.Visible = true;
                //lblUser.Visible=true;
                panel3.Visible = true ;
                FillUser();
            }
            else
            {
                //cmbUser.Visible = false;
                //lblUser.Visible = false;
                panel3.Visible = false ;
            }
            FillData(_RecordID);
           
            txtdescription.Select(); // issue no:
            txtdescription.Focus();
            
        }


        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            //Commented by Rahul Patel on 15/09/2010    
            //For disabling the close confirm  message box while closing.
            //if (DialogResult.Yes == MessageBox.Show("Are you sure you want to close? ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //{
                this.Close();
            //}
        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            bool _result = SaveData();
            if (_result == true)
            {
                //Clear cache
                gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.PaymentTrays); 
                this.Close();
            }

        }
        private void tsb_Save_Click_1(object sender, EventArgs e)
        {
            bool _result = SaveData();
            if (_result == true)
            {
                _RecordID = 0;
                txtcode.Text = "";
                txtdescription.Text = "";
                numnoofdays.Value = numnoofdays.Minimum;
                chkisdefault.Checked = false;

                string strcode = GeneratePatientCode();
                txtcode.Text = strcode;
                dtpStartdate.Value = DateTime.Now;
                dtpEndDate.Value = DateTime.Now;
                //Clear cache
                gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.PaymentTrays); 
            }
        }

        public string GeneratePatientCode()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            object value = null;
            string _result = "12345";
            string _Prefix = "";
            Int32 _Increment = 1;
            try
            {

                //ogloSettings.GetSetting("PatientCodePrefix", out value);
                if (appSettings["UserName"] != null)
                {
                    _Prefix = appSettings["UserName"].ToString();
                }
                value = null;

                ogloSettings.GetSetting("PatientCodeIncrement", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _Increment = Convert.ToInt32(value);
                }
                value = null;

                oDB.Connect(false);
                string strQuery = "SELECT ISNULL(MAX(convert(numeric,substring(sCode, " + (_Prefix.Length + 1) + ",len(sCode)- " + _Prefix.Length + "))),0) AS PatientCodeValue "
                                + " FROM BL_CloseDayTray WITH (NOLOCK) WHERE substring(sCode,1," + _Prefix.Length + ") = '" + _Prefix + "' AND  isnumeric(substring(sCode, " + (_Prefix.Length + 1) + ",len(sCode)- " + _Prefix.Length + ")) > 0 AND ISNULL(nClinicID,1) = " + _ClinicID + " AND  sCode <> '.'";
                string sqlQueryError = "";
                value = oDB.ExecuteScalar_Query(strQuery, out sqlQueryError);

                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _result = _Prefix + Convert.ToString(_Increment + Convert.ToInt64(value));
                }
                else
                {
                    _result = "12345";
                }

            }
            catch (gloDatabaseLayer.DBException)// dbEx)
            {
                //dbEx.ToString();
                //dbEx = null;
                _result = "12345";
            }
            catch (Exception)// ex)
            {
                _result = "12345";
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (ogloSettings != null)
                {
                    ogloSettings.Dispose();
                    ogloSettings = null;
                }
            }
            return _result;

        }

        private void dtpStartdate_ValueChanged(object sender, EventArgs e)
        {
            //dtpEndDate.Value
            TimeSpan Nodays = dtpEndDate.Value.Date - dtpStartdate.Value.Date;
            int _noofdays = Nodays.Days;
            // _noofdays = _noofdays + 1;
            if ((_noofdays >= numnoofdays.Minimum && _noofdays <= numnoofdays.Maximum) || _noofdays == 0)
            {
                if (_noofdays >= 0 && _noofdays != 365)
                {
                    this.numnoofdays.ValueChanged -= new System.EventHandler(this.numnoofdays_ValueChanged);
                    //numnoofdays.Value = numnoofdays.Value + 1;
                    numnoofdays.Value = _noofdays + 1;
                    this.numnoofdays.ValueChanged += new System.EventHandler(this.numnoofdays_ValueChanged);
                }
            }
            else
            {
                //MessageBox.Show("Selected Date not valid.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void numnoofdays_ValueChanged(object sender, EventArgs e)
        {
            this.dtpEndDate.ValueChanged -= new System.EventHandler(this.dtpStartdate_ValueChanged);
            this.dtpStartdate.ValueChanged -= new System.EventHandler(this.dtpStartdate_ValueChanged);
            dtpEndDate.Value = dtpStartdate.Value.AddDays(Convert.ToDouble(numnoofdays.Value - 1));
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpStartdate_ValueChanged);
            this.dtpStartdate.ValueChanged += new System.EventHandler(this.dtpStartdate_ValueChanged);
        }

        private void frmSetupCloseDayJournals_FormClosing(object sender, FormClosingEventArgs e)
        {
            //tsb_Cancel_Click(null, null);
        }

        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            gloDatabaseLayer.DBLayer ODB=null;
            try
            {   
                if (cmbUser.SelectedIndex >= 0)
                {
                    if (_UserID == Convert.ToInt64(cmbUser.SelectedValue))
                    {                        
                        chkisdefault.Enabled = true;
                    }
                    else
                    {
                        chkisdefault.Checked = false;
                        //chkisdefault.Enabled = false;
                        //03/05/2010: Issue no GLO2010-0004683: commeneted for default selection user 
                        chkisdefault.Enabled = true;
                    }
                }
                 ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                //string strquery = "Select nCloseDayTrayID, sCode, sDescription, nUserID, nNumberOfDays, nClinicID,bIsDefault from BL_CloseDayTray where nCloseDayTrayID = " + ID + " ";
                 string strquery = "Select bIsDefault from BL_CloseDayTray WITH (NOLOCK) where nCloseDayTrayID = " + _RecordID + " and nUserId='" + Convert.ToInt64(cmbUser.SelectedValue) + "'";
                DataTable dtcloseday = new DataTable();
                ODB.Connect(false);
                ODB.Retrive_Query(strquery, out dtcloseday);
                if (dtcloseday != null)
                {
                    if (dtcloseday.Rows.Count > 0)
                    {
                        
                        if (Convert.ToBoolean(dtcloseday.Rows[0]["bIsDefault"]) == true)
                        {
                            chkisdefault.Checked = true;
                        }
                        else
                        {
                            chkisdefault.Checked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally 
            {
                if (ODB != null)
                {
                    ODB.Disconnect();
                    ODB.Dispose();
                }
            }

        }

        private void lblUser_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

           
        }

        private void rbInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInactive.Checked == true)
            {
                rbInactive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbInactive.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbActive.Checked == true)
            {
                rbActive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbActive.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

       



    }
}