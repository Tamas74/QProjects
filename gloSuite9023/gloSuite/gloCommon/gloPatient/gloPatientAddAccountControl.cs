using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace gloPatient
{
    public partial class gloPatientAddAccountControl : UserControl
    {

        #region"Variable Declaration"

        Int64 patientId;
        string databaseconnectionstring = string.Empty;
        private string _messageBoxCaption = "gloPM";
        private Int64 _ClinicID = 1;
        private Int64 _UserID = 0;
        int _ownAccountCount = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        //gloPatientGuarantorControl ogloPatientGuarantorControl = null;
        gloPAGuarantorControl  ogloPatientGuarantorControl = null;
        private gloListControl.gloListControl oListControl;
        PatientGuardian oPatientGuardian = null;
        PatientOtherContacts oPatientGuarantors = null;
        PatientDemographics oPatientDemographicsDetails = null;
        Account oAccount = null;
        PatientAccount oPatientAccount = null;
        PatientAccounts oPatientAccounts = null;
        gloAccount objgloAccount = null;
        bool IsCmbSameAsGuardianLoadFlag = true;
        private ToolTip oToolTip1 = new ToolTip();

        ComboBox combo;

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

        #region "Delegates"

        public delegate void onPatientAddAccountControlEnter(object sender, EventArgs e);
        public event onPatientAddAccountControlEnter onPatientAddAccountControl_Enter;

        public delegate void onPatientAddAccountControlLeave(object sender, EventArgs e);
        public event onPatientAddAccountControlLeave onPatienAddtAccountControl_Leave;

        #endregion

        #region "Constructors"

        public gloPatientAddAccountControl()
        {
            InitializeComponent();
        }

        public gloPatientAddAccountControl(Int64 _patientId, string _databaseconnectionstring)
        {
            InitializeComponent();

            patientId = _patientId;
            databaseconnectionstring = _databaseconnectionstring;

            oPatientGuarantors = new PatientOtherContacts();
            oPatientGuardian = new PatientGuardian(_databaseconnectionstring);
            oPatientDemographicsDetails = new PatientDemographics();
            oAccount = new Account();
            oPatientAccount = new PatientAccount();
            objgloAccount = new gloAccount(_databaseconnectionstring);

            #region " Retrieve MessageBoxCaption and ClinicId from AppSettings "

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
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }

            #endregion

            cmbBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Method to fill SameAsGuardian dropdown.Based 
        /// On Guardian Information SameAsGuardian dropdown data added. 
        /// </summary>
        private void FillSameAsGuardian()
        {
            try
            {
                //Patient
                cmbSameAsGuardian.Items.Add("Patient");
                // Mother
                if ((oPatientGuardian.PatientMotherFirstName.ToString() + " " + oPatientGuardian.PatientMotherLastName.ToString()).Trim() != "")
                {
                    cmbSameAsGuardian.Items.Add("Mother");
                }
                // Father
                if ((oPatientGuardian.PatientFatherFirstName.ToString() + " " + oPatientGuardian.PatientFatherLastName.ToString()).Trim() != "")
                {
                    cmbSameAsGuardian.Items.Add("Father");
                }
                //Gaurdian
                if ((oPatientGuardian.PatientGuardianFirstName.ToString() + " " + oPatientGuardian.PatientGuardianLastName.ToString()).Trim() != "")
                {
                    cmbSameAsGuardian.Items.Add("Other Guardian");
                }
                cmbSameAsGuardian.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// This method retruns either ture or false.
        /// Fills account and patientaccount objects based on selection(new or exisitng)
        /// </summary>
        /// <returns></returns>
        public bool GetData()
        {
            bool _Result = false;
            try
            {
                if (ValidateData() == true)
                {
                    if (rbNew.Checked == true)
                    {
                        //Indicates New account
                        oAccount.IsExistingAccount = false;
                        oAccount.PAccountID = 0;
                        oAccount.AccountNo = txtAccountNo.Text.Trim();
                        oAccount.AccountDesc = txtAccountDescription.Text;

                        if (cmbBusinessCenter.SelectedIndex != -1)
                        {
                            oAccount.nBusinessCenterID = Convert.ToInt64(cmbBusinessCenter.SelectedValue);

                        }

                        oAccount.SentToCollection = chkSetToCollection.Checked;
                        oAccount.ExcludeStatement = chkExcludefromStatement.Checked;
                        oAccount.AccountClosedDate = DateTime.MinValue;
                        oAccount.RecordDate = DateTime.Now;
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                            {
                                //assign  guarantor as account guarantor.
                                if (oPatientGuarantors[gIndex].IsAccountGuarantor == true)
                                {
                                    //validation for selected guarantor address
                                    //if (oPatientGuarantors[gIndex].AddressLine1.ToString().Trim() == "")
                                    //{
                                    //    MessageBox.Show("Provide address for selected guarantor", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    return false;
                                    //}
                                    //if (oPatientGuarantors[gIndex].City.ToString().Trim() == "")
                                    //{
                                    //    MessageBox.Show("Provide city for selected guarantor", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    return false;
                                    //}

                                    //if (oPatientGuarantors[gIndex].State.ToString().Trim() == "")
                                    //{
                                    //    MessageBox.Show("Provide state for selected guarantor", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    return false;
                                    //}

                                    //if (oPatientGuarantors[gIndex].Zip.ToString().Trim() == "")
                                    //{
                                    //    MessageBox.Show("Provide zip for selected guarantor", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    return false;
                                    //}
                                    oAccount.FirstName = oPatientGuarantors[gIndex].FirstName.ToString().Trim();
                                    oAccount.LastName = oPatientGuarantors[gIndex].LastName.ToString().Trim();
                                    oAccount.MiddleName = oPatientGuarantors[gIndex].MiddleName.ToString().Trim();
                                    oAccount.AddressLine1 = oPatientGuarantors[gIndex].AddressLine1.ToString().Trim();
                                    oAccount.AddressLine2 = oPatientGuarantors[gIndex].AddressLine2.ToString().Trim();
                                    oAccount.Active = true;
                                    oAccount.City = oPatientGuarantors[gIndex].City.ToString().Trim();
                                    oAccount.Zip = oPatientGuarantors[gIndex].Zip.ToString().Trim();
                                    oAccount.State = oPatientGuarantors[gIndex].State.ToString().Trim();
                                    oAccount.Country = oPatientGuarantors[gIndex].Country.ToString().Trim();
                                    oAccount.County = oPatientGuarantors[gIndex].County.ToString().Trim();
                                    oAccount.ClinicID = _ClinicID;
                                    oAccount.MachineName = System.Environment.MachineName;
                                    oAccount.GuarantorCode = "";
                                    oAccount.AreaCode = "";
                                    oAccount.UserID = _UserID;
                                    oAccount.EntityType = oPatientGuarantors[gIndex].GurantorType.GetHashCode();
                                    oAccount.SiteID = 1;

                                }
                            }
                        }

                        oPatientAccount.AccountPatientID = 0;
                        oPatientAccount.PatientID = patientId;
                        oPatientAccount.AccountNo = txtAccountNo.Text.Trim();
                        oPatientAccount.PatientCode = oPatientDemographicsDetails.PatientCode;
                        oPatientAccount.AccountClosedDate = DateTime.MinValue;
                        oPatientAccount.ClinicID = _ClinicID;
                        oPatientAccount.SiteID = 1;
                        oPatientAccount.UserID = _UserID;
                        oPatientAccount.MachineName = System.Environment.MachineName;
                        oPatientAccount.RecordDate = DateTime.Now;
                        oPatientAccount.Active = true;
                        oPatientAccount.OwnAccount = true;

                    }
                    else if (rbExisting.Checked == true)
                    {

                        DataTable dt = objgloAccount.GetAccountDetailsById(Convert.ToInt64(txtAccountNo.Tag.ToString()));
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            oAccount.PAccountID = Convert.ToInt64(dt.Rows[0]["nPAccountID"].ToString());
                            //Indicates Existing account
                            oAccount.IsExistingAccount = true;
                        }
                        if (dt != null)
                        {
                            dt.Dispose();
                            dt = null;
                        }
                        oPatientAccount.AccountPatientID = 0;
                        oPatientAccount.PatientID = patientId;
                        oPatientAccount.AccountNo = txtAccountNo.Text.Trim();
                        oPatientAccount.PatientCode = oPatientDemographicsDetails.PatientCode;
                        oPatientAccount.AccountClosedDate = DateTime.MinValue;
                        oPatientAccount.ClinicID = _ClinicID;
                        oPatientAccount.SiteID = 1;
                        oPatientAccount.UserID = _UserID;
                        oPatientAccount.MachineName = System.Environment.MachineName;
                        oPatientAccount.RecordDate = DateTime.Now;
                        oPatientAccount.Active = true;
                        oPatientAccount.OwnAccount = false;
                    }
                    _Result = true;
                }
                else
                {
                    _Result = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _Result;

        }

        /// <summary>
        /// Method to Account validation.
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            if (txtAccountNo.Text.Trim() == "")
            {
                MessageBox.Show(((rbNew.Checked == true) ? "Enter" : "Select") + " Acct.#", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAccountNo.Focus();
                return false;
            }

            //Remove Account Description Validations

            //if (txtAccountDescription.Text.Trim() == "")
            //{
            //    MessageBox.Show("Enter Acct. Desc. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtAccountDescription.Focus();
            //    return false;
            //}
            
            if (txtAccGuarantor.Text == "" && txtAccGuarantor.Visible == true)
            {
                MessageBox.Show("Select guarantor for Account.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (CheckAccountNoExistsForGuarantor(txtAccountNo.Text.Trim()) == true && rbNew.Checked == true)
            {
                MessageBox.Show("Acct.#/Patient Code already exists.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Method to SaveAccount
        /// </summary>
        public void SaveAccount()
        {

            #region "Patient Account"

            SqlConnection _sqlConnection = new SqlConnection(databaseconnectionstring);
            SqlCommand _sqlCommand = null;
            SqlTransaction _sqlTransaction = null;
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(databaseconnectionstring);  
            int _result = 0;
            object accountId = 0;
            try
            {
                _sqlConnection.Open();
                _sqlTransaction = _sqlConnection.BeginTransaction();

                if (Account != null)
                {

                    if (Account.IsExistingAccount == false)
                    {

                        #region "Account Saving"

                        oParameters.Clear();
                        //Account params
                        oParameters.Add("@nPAccountID", Account.PAccountID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                        oParameters.Add("@sAccountNo", Account.AccountNo.Trim().Replace("'","''"), ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sAccountDesc", Account.AccountDesc.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@nGuarantorID", Account.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@sGuarantorCode", Account.GuarantorCode, ParameterDirection.Input, SqlDbType.VarChar);
                        if (Account.AccountClosedDate != null && Account.AccountClosedDate != DateTime.MinValue)
                        {
                            oParameters.Add("@dtAccountClosedDate", Account.AccountClosedDate, ParameterDirection.Input, SqlDbType.DateTime);
                        }
                        else
                        {
                            oParameters.Add("@dtAccountClosedDate", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                        }
                        oParameters.Add("@sFirstName", Account.FirstName.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sMiddleName", Account.MiddleName.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sLastName", Account.LastName.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@nEntityType", Account.EntityType, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@sAddressLine1", Account.AddressLine1.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sAddressLine2", Account.AddressLine2.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sCity", Account.City.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sState", Account.State, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sZip", Account.Zip, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sCountry", Account.Country, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sCounty", Account.County.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@sAreaCode", Account.AreaCode, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@bIsExcludeStatement", Account.ExcludeStatement, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@bIsSentToCollection", Account.SentToCollection, ParameterDirection.Input, SqlDbType.Bit);
                        oParameters.Add("@nClinicID", Account.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@nSiteID", Account.SiteID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@sMachineName", Account.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                        oParameters.Add("@nUserID", Account.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                        oParameters.Add("@dtRecordDate", Account.RecordDate, ParameterDirection.Input, SqlDbType.DateTime);
                        oParameters.Add("@bIsActive", Account.Active, ParameterDirection.Input, SqlDbType.Bit);

                        oParameters.Add("@nBusinessCenterID", Account.nBusinessCenterID, ParameterDirection.Input, SqlDbType.BigInt);

                       // _sqlCommand = new SqlCommand();
                        _sqlCommand = oDB.GetCmdParameters(oParameters);
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandText = "PA_InUp_Accounts";

                        _result = _sqlCommand.ExecuteNonQuery();

                        if (_sqlCommand.Parameters["@nPAccountID"].Value != null)
                        {
                            accountId = _sqlCommand.Parameters["@nPAccountID"].Value;
                        }
                        _sqlCommand.Parameters.Clear();
                        _sqlCommand.Dispose();
                        _sqlCommand = null;
                        #endregion "Account Saving"

                        #region "PatientAccountGuarantors Saving"

                        if (oPatientGuarantors != null)
                        {
                            for (int i = 0; i < PatientGuarantors.Count; i++)
                            {

                                Object GuarantorId = 0;
                                oParameters.Clear();

                                oParameters.Add("@nPatientID", Convert.ToInt64(patientId), ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nPatientContactID", oPatientGuarantors[i].PatientContactID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oParameters.Add("@nLineNumber", i, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nPatientContactType", oPatientGuarantors[i].OtherConatctType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oParameters.Add("@sFirstName", PatientGuarantors[i].FirstName.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sMiddleName", PatientGuarantors[i].MiddleName.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sLastName", PatientGuarantors[i].LastName.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                                if (PatientGuarantors[i].DOB != null && PatientGuarantors[i].DOB != DateTime.MinValue)
                                {
                                    oParameters.Add("@nDOB", gloDateMaster.gloDate.DateAsNumber(PatientGuarantors[i].DOB.ToShortDateString()), ParameterDirection.Input, SqlDbType.Int);
                                }
                                else
                                {
                                    oParameters.Add("@nDOB", DBNull.Value, ParameterDirection.Input, SqlDbType.Int);
                                }
                                oParameters.Add("@sSSN", PatientGuarantors[i].SSN, ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sGender", PatientGuarantors[i].Gender, ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sRelation", PatientGuarantors[i].Relation, ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sAddressLine1", PatientGuarantors[i].AddressLine1.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sAddressLine2", PatientGuarantors[i].AddressLine2.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sCity", PatientGuarantors[i].City.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sState", PatientGuarantors[i].State, ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sZIP", PatientGuarantors[i].Zip, ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sPhone", PatientGuarantors[i].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sMobile", PatientGuarantors[i].Mobile, ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sFax", PatientGuarantors[i].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sEmail", PatientGuarantors[i].Email, ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@bIsActive", PatientGuarantors[i].IsActive, ParameterDirection.Input, SqlDbType.Bit);
                                oParameters.Add("@nVisitID", PatientGuarantors[i].VisitID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nAppointmentID", PatientGuarantors[i].AppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nGuarantorAsPatientID", PatientGuarantors[i].GuarantorAsPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nPatientContactTypeFlag", PatientGuarantors[i].nGuarantorTypeFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oParameters.Add("@sCounty", PatientGuarantors[i].County.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@sCountry", PatientGuarantors[i].Country, ParameterDirection.Input, SqlDbType.VarChar);
                                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@nGuarantorType", PatientGuarantors[i].GurantorType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oParameters.Add("@nPAccountID", accountId, ParameterDirection.Input, SqlDbType.BigInt);
                                oParameters.Add("@bIsAccountGuarantor", PatientGuarantors[i].IsAccountGuarantor, ParameterDirection.Input, SqlDbType.Bit);
                         //       _sqlCommand = new System.Data.SqlClient.SqlCommand();
                                _sqlCommand = oDB.GetCmdParameters(oParameters);
                                _sqlCommand.Connection = _sqlConnection;
                                _sqlCommand.Transaction = _sqlTransaction;
                                _sqlCommand.CommandType = CommandType.StoredProcedure;
                                _sqlCommand.CommandText = "PA_INUP_PatientGuarantor";
                                _result = _sqlCommand.ExecuteNonQuery();


                                if (_sqlCommand.Parameters["@nPatientContactID"].Value != null)
                                    GuarantorId = _sqlCommand.Parameters["@nPatientContactID"].Value;
                                _sqlCommand.Parameters.Clear();
                                _sqlCommand.Dispose();
                                _sqlCommand = null;
                            }

                        }
                        #endregion "Patient Guarantors Saving"

                    }
                    else
                    {
                        accountId = Account.PAccountID;
                    }

                    #region "Patient Account Saving"

                    oParameters.Clear();
                    oParameters.Add("@nAccountPatientID", oPatientAccount.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPAccountID", accountId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nPatientID", oPatientAccount.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sAccountNo", oPatientAccount.AccountNo.Trim().Replace("'", "''"), ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sPatientCode", oPatientAccount.PatientCode, ParameterDirection.Input, SqlDbType.VarChar);
                    if (oPatientAccount.AccountClosedDate != null && oPatientAccount.AccountClosedDate != DateTime.MinValue)
                    {
                        oParameters.Add("@dtAccountClosedDate", oPatientAccount.AccountClosedDate, ParameterDirection.Input, SqlDbType.DateTime);
                    }
                    else
                    {
                        oParameters.Add("@dtAccountClosedDate", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                    }
                    oParameters.Add("@nClinicID", oPatientAccount.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nSiteID", oPatientAccount.SiteID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sMachineName", oPatientAccount.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nUserID", oPatientAccount.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@dtRecordDate", oPatientAccount.RecordDate, ParameterDirection.Input, SqlDbType.DateTime);
                    oParameters.Add("@bIsActive", oPatientAccount.Active, ParameterDirection.Input, SqlDbType.Bit);
                    oParameters.Add("@bIsOwnAccount", oPatientAccount.OwnAccount, ParameterDirection.Input, SqlDbType.Bit);

                    //_sqlCommand = new SqlCommand();
                    _sqlCommand = oDB.GetCmdParameters(oParameters);
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.Transaction = _sqlTransaction;
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = "PA_InUp_Accounts_Patients";

                    _result = _sqlCommand.ExecuteNonQuery();
                    _sqlCommand.Parameters.Clear();
                    _sqlCommand.Dispose();
                    _sqlCommand = null;

                    _sqlTransaction.Commit();
                    _sqlConnection.Close();

                    #endregion "Patient Account Saving"

                    //if the patient is receiving an “existing account” as a new account 
                    // and there are any previous accounts and that accounts has no transactions in it at all and there are no other patients on that accounts then
                    //  1.	Present a message “Remove Account <Account Number> +”-“+<Account Description>?”  Yes, No
                    //  2.	If Yes, deactivate and DELETE that previous account.
                    if (oPatientAccount.OwnAccount == false)
                    {
                        if (oPatientAccounts != null && oPatientAccounts.Count > 0)

                            for (int i = 0; i < oPatientAccounts.Count; i++)
                            {
                                string strMessage = string.Empty;
                                if (oPatientAccounts[i].OwnAccount == true)
                                {
                                    bool accountToRemove = false;
                                    accountToRemove = objgloAccount.CheckTransactionsExistForAccountOrAnyPatientsForAccount(oPatientAccounts[i].PAccountID, oPatientAccounts[i].PatientID);

                                    //no transactions done on this account and no other patients on this account. 
                                    if (accountToRemove == true)
                                    {
                                        DataTable dt = objgloAccount.GetAccountDetailsById(oPatientAccounts[i].PAccountID);
                                        if (dt != null)
                                        {
                                            if ( dt.Rows.Count > 0)
                                            {
                                                strMessage = "No Transactions found.\nRemove Account <" + dt.Rows[0]["sAccountNo"].ToString().Trim() + ">";

                                                if (dt.Rows[0]["sAccountDesc"].ToString().Trim() != "")
                                                    strMessage += " - <" + dt.Rows[0]["sAccountDesc"].ToString().Trim() + ">";
                                            }
                                            strMessage += "?";

                                            if (MessageBox.Show(strMessage, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                                objgloAccount.DeletePatientAccount(oPatientAccounts[i].PAccountID, oPatientAccounts[i].PatientID);
                                            dt.Dispose();
                                            dt = null;
                                        }
                                    }
                                }
                            }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                _sqlTransaction.Rollback();
                _sqlConnection.Close();
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                _sqlTransaction.Rollback();
                _sqlConnection.Close();
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if ((_sqlCommand != null))
                {
                    _sqlCommand.Parameters.Clear();
                    _sqlCommand.Dispose();
                    _sqlCommand = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose(); 
                }
                if (_sqlConnection != null)
                {
                    _sqlConnection.Dispose();
                    _sqlConnection = null;
                }
                if (_sqlTransaction != null)
                {
                    _sqlTransaction.Dispose();
                    _sqlTransaction = null;
                }
            }

            #endregion

        }

        /// <summary>
        /// Method to check AccountNo exist or not.
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        private bool CheckAccountNoExistsForGuarantor(string accountNo)
        {

            object result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(databaseconnectionstring);
            try
            {
                oDB.Connect(false);

                string _strSqlQuery = "Select COUNT(*) from " +
                                            " (Select sAccountNo as sCode from PA_Accounts " +
                                            " Union " +
                                            " Select sPatientCode as sCode From Patient " +
                                                    " Where nPatientID <> " + patientId + ") as Main " +
                                            " Where sCode = ltrim(rtrim('" + accountNo.Trim().Replace("'", "''") + "'))";

                result = oDB.ExecuteScalar_Query(_strSqlQuery);

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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            if (Convert.ToInt64(result) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private void setCmbSameAsGuardianIndex()
        {
            try
            {
                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                {
                    for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                    {
                        txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;

                        IsCmbSameAsGuardianLoadFlag = false;
                        if (oPatientGuarantors[gindex].OtherConatctType.GetHashCode() == PatientOtherContactType.SameAsPatient.GetHashCode())
                            cmbSameAsGuardian.SelectedIndex = cmbSameAsGuardian.Items.IndexOf("Patient");
                        else if (oPatientGuarantors[gindex].OtherConatctType.GetHashCode() == PatientOtherContactType.Mother.GetHashCode())
                            cmbSameAsGuardian.SelectedIndex = cmbSameAsGuardian.Items.IndexOf("Mother");
                        else if (oPatientGuarantors[gindex].OtherConatctType.GetHashCode() == PatientOtherContactType.Father.GetHashCode())
                            cmbSameAsGuardian.SelectedIndex = cmbSameAsGuardian.Items.IndexOf("Father");
                        else if (oPatientGuarantors[gindex].OtherConatctType.GetHashCode() == PatientOtherContactType.OtherGuardian.GetHashCode())
                            cmbSameAsGuardian.SelectedIndex = cmbSameAsGuardian.Items.IndexOf("Other Guardian");
                        else
                            cmbSameAsGuardian.SelectedIndex = -1;

                        IsCmbSameAsGuardianLoadFlag = true;
                    }
                }
                else
                {
                    IsCmbSameAsGuardianLoadFlag = false;
                    cmbSameAsGuardian.SelectedIndex = -1;
                    IsCmbSameAsGuardianLoadFlag = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private PatientOtherContact.GuarantorTypeFlag GetNextTypeFlag(bool CallFromSameAsPatient)
        {
            PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.None;

            bool isPrimaryPresent = false;
            bool isSecondaryPresent = false;
            bool isTertioryPresent = false;

            if (oPatientGuarantors.Count != 0)
            {
                for (int i = 0; i < oPatientGuarantors.Count; i++)
                {
                    if (oPatientGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Primary.GetHashCode())
                        isPrimaryPresent = true;
                    else if (oPatientGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Secondary.GetHashCode())
                        isSecondaryPresent = true;
                    else if (oPatientGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Tertiary.GetHashCode())
                        isTertioryPresent = true;
                }

                if (!isPrimaryPresent)
                    _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Primary;
                else if (!isSecondaryPresent)
                    _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Secondary;
                else if (!isTertioryPresent)
                    _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Tertiary;
                else
                    _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Tertiary;
            }
            else
                _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Primary;

            return _GuarantorTypeFlag;
        }
        
        private void FillBusinessCenter()
        {
            DataTable dtBusinessCenter = null;
            try
            {
                dtBusinessCenter = gloGlobal.gloPMMasters.GetBusinessCenterByState(true);
                if (dtBusinessCenter != null && dtBusinessCenter.Rows.Count > 0)
                {
                    //DataRow dr = dtBusinessCenter.NewRow();
                    //dr["nBusinessCenterID"] = "";
                    //dr["BusinessCenter"] = "";
                    //dtBusinessCenter.Rows.InsertAt(dr, 0);

                    cmbBusinessCenter.BeginUpdate();
                    cmbBusinessCenter.DataSource = dtBusinessCenter.Copy();
                    cmbBusinessCenter.DisplayMember = dtBusinessCenter.Columns["BusinessCenter"].ColumnName;
                    cmbBusinessCenter.ValueMember = dtBusinessCenter.Columns["nBusinessCenterID"].ColumnName;
                    cmbBusinessCenter.EndUpdate();
                   
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (dtBusinessCenter != null) { dtBusinessCenter.Dispose(); }
            }
        }
        #endregion

        #region "Events"

        /// <summary>
        /// Control Load Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gloPatientAddAccountControl_Load(object sender, EventArgs e)
        {
            try
            {
                oToolTip1.SetToolTip(btnNewGuarantor, "Add Guarantor");
                //based on OwnAccountCount generate accountno
                if (_ownAccountCount > 0)
                {
                    txtAccountNo.Text = oPatientDemographicsDetails.PatientCode + "_" + _ownAccountCount;
                }
                else
                {
                    txtAccountNo.Text = oPatientDemographicsDetails.PatientCode;
                }
                FillSameAsGuardian();

                Boolean _IsRequireBusinessCenterOnPAccounts = gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_PatientAccount");
                if (_IsRequireBusinessCenterOnPAccounts)
                {
                    pnlBusinessCenter.Visible = true;
                    FillBusinessCenter();
                    Int64 _DefaultBusinessCenter = gloGlobal.gloPMGlobal.GetDefaultBusinessCenterForUser(gloGlobal.gloPMGlobal.UserID);
                    if (_DefaultBusinessCenter != 0)
                    {
                        cmbBusinessCenter.SelectedValue = _DefaultBusinessCenter;
                    }
                    if (cmbBusinessCenter.Items.Count > 0)
                    {
                        if (cmbBusinessCenter.SelectedIndex == -1)
                        {
                            cmbBusinessCenter.SelectedIndex = 0;
                        }
                    }
                    lblBusinessCenter.Visible = false;
                }
                else
                {
                    pnlBusinessCenter.Visible = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            
        }

        /// <summary>
        ///  Event to create new account.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbNew_CheckedChanged(object sender, EventArgs e)
        {
            txtAccountNo.Text = "";
            txtAccountDescription.Text = "";
            txtAccountNo.ReadOnly = false;
            txtAccountDescription.ReadOnly = false;

            //based on OwnAccountCount generate accountno
            if (_ownAccountCount > 0)
            {
                txtAccountNo.Text = oPatientDemographicsDetails.PatientCode + "_" + _ownAccountCount;
            }
            else
            {
                txtAccountNo.Text = oPatientDemographicsDetails.PatientCode;
            }

            btnExistingAccountSelect.Visible = false;
            btnExistingAccountDelete.Visible = false;
            lblGuarantorDetails.Visible = false;

            txtAccGuarantor.Visible = true;
            btnGuarantorExistingPatientBrowse.Visible = true;
            btnNewGuarantor.Visible = true;
            chkExcludefromStatement.Visible = true;
            chkSetToCollection.Visible = true;
            lblSameAsGuardian.Visible = true;
            cmbSameAsGuardian.Visible = true;

            lblGuarantorDetails.Height = 21;
            lblGuarantorDetails.Width = 203;

            chkExcludefromStatement.Checked = false;
            chkSetToCollection.Checked = false;

            lblBusinessCenter.Visible = false;
            cmbBusinessCenter.Visible = true;
        }

        /// <summary>
        /// Event to select existing account.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbExisting_CheckedChanged(object sender, EventArgs e)
        {

            txtAccountDescription.Text = "";
            txtAccountNo.Text = "";
            lblGuarantorDetails.Text = "";
            lblBusinessCenter.Text = "";
            
            txtAccountNo.ReadOnly = true;
            txtAccountDescription.ReadOnly = true;

            txtAccGuarantor.Visible = false;
            btnGuarantorExistingPatientBrowse.Visible = false;
            btnNewGuarantor.Visible = false;
            chkExcludefromStatement.Visible = false;
            chkSetToCollection.Visible = false;
            lblSameAsGuardian.Visible = false;
            cmbSameAsGuardian.Visible = false;

            btnExistingAccountSelect.Visible = true;
            btnExistingAccountDelete.Visible = true;
            lblGuarantorDetails.Visible = true;

            lblGuarantorDetails.Height = 53;
            lblGuarantorDetails.Width = 275;


            lblBusinessCenter.Visible = true;
            cmbBusinessCenter.Visible = false;

        }
        private void removeOListControl()
        {
            if (oListControl != null)
            {
                if (this.Controls.Contains(oListControl))
                {
                    this.Controls.Remove(oListControl);
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ExistingAccountSelectClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_GaurantorSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }
                }
                catch 
                {

                }
                oListControl.Dispose();
                oListControl = null;
            }
        }
        /// <summary>
        /// Event to list Existing accounts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExistingAccountSelect_Click(object sender, EventArgs e)
        {
            try
            {

                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(databaseconnectionstring, gloListControl.gloListControlType.GuarantorsAccounts, false, this.Width);
                oListControl.ControlHeader = "Guarantors Accounts";
                oListControl.PatientID = patientId;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ExistingAccountSelectClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                oListControl.OpenControl();
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                }
                onPatienAddtAccountControl_Leave(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Event to clear Existing account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExistingAccountDelete_Click(object sender, EventArgs e)
        {
            try
            {
                txtAccountNo.Text = "";
                txtAccountNo.Tag = null;
                txtAccountDescription.Text = "";
                lblGuarantorDetails.Text = "";
                lblBusinessCenter.Text = "";
               
                oAccount = new Account();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Event to open patient list to select Gaurantor 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuarantorExistingPatientBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(databaseconnectionstring, gloListControl.gloListControlType.Patient,false, this.Width);
                oListControl.ControlHeader = "Guarantor";
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_GaurantorSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                this.Controls.Add(oListControl);

                oListControl.OpenControl();

                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                }
                onPatienAddtAccountControl_Leave(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Create New Guarantor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGuarantor_Click(object sender, EventArgs e)
        {
            try
            {
                //ogloPatientGuarantorControl = new gloPatientGuarantorControl(databaseconnectionstring);
                //ogloPatientGuarantorControl.PatientGuarantors = oPatientGuarantors;
                //ogloPatientGuarantorControl.FromAccountGuarantor = true;
                //ogloPatientGuarantorControl.PatientId = patientId;
                //ogloPatientGuarantorControl.SaveButton_Click += new gloPatientGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                //ogloPatientGuarantorControl.CloseButton_Click += new gloPatientGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);
                //this.Controls.Add(ogloPatientGuarantorControl);
                //this.Left = 300;
                //this.Top = 200;
                //this.Width = 700;
                //this.Height = 680;
                //ogloPatientGuarantorControl.Dock = DockStyle.Fill;
                //ogloPatientGuarantorControl.BringToFront();
                //onPatienAddtAccountControl_Leave(sender, e);

                if (ogloPatientGuarantorControl != null)
                {
                    if (this.Controls.Contains(ogloPatientGuarantorControl))
                    {
                        this.Controls.Remove(ogloPatientGuarantorControl);
                       
                    }
                    try
                    {
                        ogloPatientGuarantorControl.SaveButton_Click -= new gloPAGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                        ogloPatientGuarantorControl.CloseButton_Click -= new gloPAGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);
                    }
                    catch
                    {
                    }
                    ogloPatientGuarantorControl.Dispose();
                    ogloPatientGuarantorControl = null;
                }
                ogloPatientGuarantorControl = new gloPAGuarantorControl(databaseconnectionstring);
                ogloPatientGuarantorControl.PatientGuarantors = oPatientGuarantors;
                ogloPatientGuarantorControl.FromAccountGuarantor = true;
                ogloPatientGuarantorControl.PatientId = patientId;
                ogloPatientGuarantorControl.SaveButton_Click += new gloPAGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                ogloPatientGuarantorControl.CloseButton_Click += new gloPAGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);
                this.Controls.Add(ogloPatientGuarantorControl);
                this.Left = 300;
                this.Top = 200;
                this.Width = 700;
                this.Height = 680;
                ogloPatientGuarantorControl.Dock = DockStyle.Fill;
                ogloPatientGuarantorControl.BringToFront();
                onPatienAddtAccountControl_Leave(sender, e);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        ///  Event to CmbSameAsGuardian selected index changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSameAsGuardian_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsCmbSameAsGuardianLoadFlag == true)
                {

                    //Remove guarantor
                    if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                    {
                        oPatientGuarantors.RemoveAt(0);
                        txtAccGuarantor.Text = "";
                    }

                    if (cmbSameAsGuardian.Text == "Patient")
                    {
                        if (PatientDemographicDetails.PatientFirstName.ToString().Trim() != "" && PatientDemographicDetails.PatientLastName.ToString().Trim() != "")
                        {
                            bool _shouldAdd = true;

                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                {
                                    if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.SameAsPatient)
                                    {
                                        _shouldAdd = false;
                                        break;

                                    }

                                }
                            }
                            if (_shouldAdd == true)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(true);
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.GuarantorAsPatientID = patientId;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = PatientDemographicDetails.PatientFirstName.ToString().Trim();
                                oGuarantor.MiddleName = PatientDemographicDetails.PatientMiddleName.ToString().Trim();
                                oGuarantor.LastName = PatientDemographicDetails.PatientLastName.ToString().Trim();

                                oGuarantor.DOB = Convert.ToDateTime(PatientDemographicDetails.PatientDOB);
                                oGuarantor.SSN = PatientDemographicDetails.PatientSSN.ToString().Trim();
                                oGuarantor.Gender = PatientDemographicDetails.PatientGender.ToString().Trim();


                                oGuarantor.AddressLine1 = PatientDemographicDetails.PatientAddress1.ToString().Trim();
                                oGuarantor.AddressLine2 = PatientDemographicDetails.PatientAddress2.ToString().Trim();
                                oGuarantor.City = PatientDemographicDetails.PatientCity.ToString().Trim();
                                oGuarantor.County = PatientDemographicDetails.PatientCountry.ToString().Trim();
                                oGuarantor.Zip = PatientDemographicDetails.PatientZip.ToString().Trim();
                                oGuarantor.State = PatientDemographicDetails.PatientState.ToString().Trim();
                                oGuarantor.Country = PatientDemographicDetails.PatientCountry.ToString().Trim();
                                oGuarantor.Relation = "Self";
                                oGuarantor.Phone = PatientDemographicDetails.PatientPhone.ToString().Trim();

                                oGuarantor.Mobile = PatientDemographicDetails.PatientMobile.ToString().Trim();
                                oGuarantor.Email = PatientDemographicDetails.PatientEmail.ToString().Trim();
                                oGuarantor.Fax = PatientDemographicDetails.PatientFax.ToString().Trim();
                                oGuarantor.OtherConatctType = PatientOtherContactType.SameAsPatient;

                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;
                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;


                                if (oPatientGuarantors.Count == 0)
                                {
                                    this.oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        setCmbSameAsGuardianIndex();
                                        return;
                                    }

                                }
                            }
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                                {
                                    txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                    setCmbSameAsGuardianIndex();
                                }
                            }

                            //Code moved. If account no address gauarantor is not loading. By Mahesh Satlapalli(Apollo).
                            //if (PatientDemographicDetails.PatientAddress1.ToString().Trim() == "")
                            //{
                            //    MessageBox.Show("Enter address for selected guarantor.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    IsCmbSameAsGuardianLoadFlag = false;
                            //    setCmbSameAsGuardianIndex();
                            //    IsCmbSameAsGuardianLoadFlag = true;
                            //    cmbSameAsGuardian.Focus();
                            //    return;
                            //}
                            //if (PatientDemographicDetails.PatientCity.ToString().Trim() == "")
                            //{
                            //    MessageBox.Show("Enter city for selected guarantor.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    IsCmbSameAsGuardianLoadFlag = false;
                            //    setCmbSameAsGuardianIndex();
                            //    IsCmbSameAsGuardianLoadFlag = true;
                            //    cmbSameAsGuardian.Focus();
                            //    return;
                            //}
                            //if (PatientDemographicDetails.PatientState.ToString().Trim() == "")
                            //{
                            //    MessageBox.Show("Enter state for selected guarantor.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    IsCmbSameAsGuardianLoadFlag = false;
                            //    setCmbSameAsGuardianIndex();
                            //    IsCmbSameAsGuardianLoadFlag = true;
                            //    cmbSameAsGuardian.Focus();
                            //    return;
                            //}
                            //if (PatientDemographicDetails.PatientZip.ToString().Trim() == "")
                            //{
                            //    MessageBox.Show("Enter zip for selected guarantor.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    IsCmbSameAsGuardianLoadFlag = false;
                            //    setCmbSameAsGuardianIndex();
                            //    IsCmbSameAsGuardianLoadFlag = true;
                            //    cmbSameAsGuardian.Focus();
                            //    return;
                            //}


                        }
                    }
                    if (cmbSameAsGuardian.Text == "Mother")
                    {

                        if (PatientGuardianDetails.PatientMotherFirstName.Trim() != "" && PatientGuardianDetails.PatientMotherLastName.Trim() != "")
                        {
                            bool _shouldAdd = true;
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                {
                                    if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.Mother)
                                    {
                                        _shouldAdd = false;
                                        break;

                                    }

                                }
                            }
                            if (_shouldAdd == true)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(true);

                                oGuarantor.PatientContactID = 0;
                                oGuarantor.GuarantorAsPatientID = 0;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = PatientGuardianDetails.PatientMotherFirstName.Trim();
                                oGuarantor.MiddleName = PatientGuardianDetails.PatientMotherMiddleName.Trim();
                                oGuarantor.LastName = PatientGuardianDetails.PatientMotherLastName.Trim();
                                oGuarantor.Relation = "Mother";
                                oGuarantor.Gender = "Female";
                                //if (PatientGuardianDetails.PatientMotherAddress1.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide address for Guardian(Mother).", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (PatientGuardianDetails.PatientMotherCity.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide city for Guardian(Mother).", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (PatientGuardianDetails.PatientMotherState.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide state for Guardian(Mother).", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (PatientGuardianDetails.PatientMotherZip.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide zip for Guardian(Mother).", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                oGuarantor.AddressLine1 = PatientGuardianDetails.PatientMotherAddress1.Trim().ToString();
                                oGuarantor.AddressLine2 = PatientGuardianDetails.PatientMotherAddress2.Trim().ToString();
                                oGuarantor.City = PatientGuardianDetails.PatientMotherCity.Trim().ToString();
                                oGuarantor.County = PatientGuardianDetails.PatientMotherCounty.Trim().ToString();
                                oGuarantor.Zip = PatientGuardianDetails.PatientMotherZip.Trim().ToString();
                                oGuarantor.State = PatientGuardianDetails.PatientMotherState.Trim().ToString();
                                oGuarantor.Country = PatientGuardianDetails.PatientMotherCountry.Trim().ToString();

                                oGuarantor.Phone = PatientGuardianDetails.PatientMotherPhone.Trim().ToString();
                                oGuarantor.Mobile = PatientGuardianDetails.PatientMotherMobile.Trim().ToString();
                                oGuarantor.Email = PatientGuardianDetails.PatientMotherEmail.Trim().ToString();
                                oGuarantor.Fax = PatientGuardianDetails.PatientMotherFAX.Trim().ToString();
                                oGuarantor.OtherConatctType = PatientOtherContactType.Mother;
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;
                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;

                                if (oPatientGuarantors.Count == 0)
                                {
                                    this.oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        setCmbSameAsGuardianIndex();
                                        return;
                                    }

                                }
                            }
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                                {
                                    txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                    setCmbSameAsGuardianIndex();
                                }
                            }
                        }

                    }
                    if (cmbSameAsGuardian.Text == "Father")
                    {

                        if (PatientGuardianDetails.PatientFatherFirstName.Trim() != "" && PatientGuardianDetails.PatientFatherLastName.Trim() != "")
                        {
                            bool _shouldAdd = true;

                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                {
                                    if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.Father)
                                    {
                                        _shouldAdd = false;
                                        break;

                                    }

                                }
                            }
                            if (_shouldAdd == true)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(true);
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.GuarantorAsPatientID = 0;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = PatientGuardianDetails.PatientFatherFirstName;
                                oGuarantor.MiddleName = PatientGuardianDetails.PatientFatherMiddleName.Trim();
                                oGuarantor.LastName = PatientGuardianDetails.PatientFatherLastName.Trim();
                                oGuarantor.Relation = "Father";
                                oGuarantor.Gender = "Male";
                                //if (PatientGuardianDetails.PatientFatherAddress1.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide address for Guardian(Father).", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (PatientGuardianDetails.PatientFatherCity.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide city for Guardian(Father).", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (PatientGuardianDetails.PatientFatherState.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide state for Guardian(Father).", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (PatientGuardianDetails.PatientFatherZip.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide zip for Guardian(Father).", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                oGuarantor.AddressLine1 = PatientGuardianDetails.PatientFatherAddress1.Trim().ToString();
                                oGuarantor.AddressLine2 = PatientGuardianDetails.PatientFatherAddress2.Trim().ToString();
                                oGuarantor.City = PatientGuardianDetails.PatientFatherCity.Trim().ToString();
                                oGuarantor.County = PatientGuardianDetails.PatientFatherCounty.Trim().ToString();
                                oGuarantor.Zip = PatientGuardianDetails.PatientFatherZip.Trim().ToString();
                                oGuarantor.State = PatientGuardianDetails.PatientFatherState.Trim().ToString();
                                oGuarantor.Country = PatientGuardianDetails.PatientFatherCountry.Trim().ToString();


                                oGuarantor.Phone = PatientGuardianDetails.PatientFatherPhone.Trim().ToString();
                                oGuarantor.Mobile = PatientGuardianDetails.PatientFatherMobile.Trim().ToString();

                                oGuarantor.Email = PatientGuardianDetails.PatientFatherEmail.Trim().ToString();
                                oGuarantor.Fax = PatientGuardianDetails.PatientFatherFAX.Trim().ToString();
                                oGuarantor.OtherConatctType = PatientOtherContactType.Father;

                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;
                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;

                                if (oPatientGuarantors.Count == 0)
                                {
                                    this.oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        setCmbSameAsGuardianIndex();
                                        return;
                                    }
                                }
                            }
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                                {
                                    txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                    setCmbSameAsGuardianIndex();
                                }
                            }
                        }


                    }
                    if (cmbSameAsGuardian.Text == "Other Guardian")
                    {

                        if (PatientGuardianDetails.PatientGuardianFirstName.Trim() != "" && PatientGuardianDetails.PatientGuardianLastName.Trim() != "")
                        {
                            bool _shouldAdd = true;

                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                                {
                                    if (oPatientGuarantors[gIndex].OtherConatctType == PatientOtherContactType.OtherGuardian)
                                    {
                                        _shouldAdd = false;
                                        break;
                                    }

                                }
                            }
                            if (_shouldAdd == true)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(true);
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.GuarantorAsPatientID = 0;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = PatientGuardianDetails.PatientGuardianFirstName.Trim();
                                oGuarantor.MiddleName = PatientGuardianDetails.PatientGuardianMiddleName.Trim();
                                oGuarantor.LastName = PatientGuardianDetails.PatientGuardianLastName.Trim();
                                oGuarantor.Relation = PatientGuardianDetails.PatientGuardianRelationDS.Trim().ToString();

                                //if (PatientGuardianDetails.PatientGuardianAddress1.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide address for Guardian(Other).", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (PatientGuardianDetails.PatientGuardianCity.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide city for Guardian(Other).", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (PatientGuardianDetails.PatientGuardianState.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide state for Guardian(Other).", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                //if (PatientGuardianDetails.PatientGuardianZip.ToString().Trim() == "")
                                //{
                                //    MessageBox.Show("Provide zip for Guardian(Other).", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    IsCmbSameAsGuardianLoadFlag = false;
                                //    setCmbSameAsGuardianIndex();
                                //    IsCmbSameAsGuardianLoadFlag = true;
                                //    cmbSameAsGuardian.Focus();
                                //    return;
                                //}
                                oGuarantor.AddressLine1 = PatientGuardianDetails.PatientGuardianAddress1.Trim().ToString();
                                oGuarantor.AddressLine2 = PatientGuardianDetails.PatientGuardianAddress2.Trim().ToString();
                                oGuarantor.City = PatientGuardianDetails.PatientGuardianCity.Trim().ToString();
                                oGuarantor.County = PatientGuardianDetails.PatientGuardianCounty.Trim().ToString();
                                oGuarantor.Zip = PatientGuardianDetails.PatientGuardianZip.Trim().ToString();
                                oGuarantor.State = PatientGuardianDetails.PatientGuardianState.Trim().ToString();
                                oGuarantor.Country = PatientGuardianDetails.PatientGuardianCountry.Trim().ToString();


                                oGuarantor.Phone = PatientGuardianDetails.PatientGuardianPhone.Trim().ToString();
                                oGuarantor.Mobile = PatientGuardianDetails.PatientGuardianMobile.Trim().ToString();

                                oGuarantor.Email = PatientGuardianDetails.PatientGuardianEmail.Trim().ToString();
                                oGuarantor.Fax = PatientGuardianDetails.PatientGuardianFAX.Trim().ToString();
                                oGuarantor.OtherConatctType = PatientOtherContactType.OtherGuardian;
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;
                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;

                                if (oPatientGuarantors.Count == 0)
                                {
                                    this.oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        setCmbSameAsGuardianIndex();
                                        return;
                                    }

                                }
                            }
                            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                            {
                                for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                                {
                                    txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                    setCmbSameAsGuardianIndex();
                                }
                            }

                        }

                    }
                }
                IsCmbSameAsGuardianLoadFlag = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

       
    #endregion

        #region "ListControlEvents"

        /// <summary>
        /// Event to select Existing account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oListControl_ExistingAccountSelectClick(object sender, EventArgs e)
        {
            try
            {
                txtAccountNo.Text = "";
                txtAccountDescription.Text = "";
                lblGuarantorDetails.Text = "";

                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        //AccountId
                        txtAccountNo.Tag = oListControl.SelectedItems[i].ID;
                        txtAccountNo.Text = oListControl.SelectedItems[i].Code;
                        txtAccountDescription.Text = oListControl.SelectedItems[i].Description;

                        DataTable dt = objgloAccount.GetAccountDetailsById(Convert.ToInt64(txtAccountNo.Tag.ToString()));

                        if (dt != null && dt.Rows.Count > 0)
                        {

                            string guarantordetails = dt.Rows[0]["sFirstName"].ToString().Trim() + ' ' + dt.Rows[0]["sMiddleName"].ToString() + ' ' + dt.Rows[0]["sLastName"].ToString() + Environment.NewLine;

                            if (dt.Rows[0]["sAddressLine1"].ToString() != "")
                                guarantordetails = guarantordetails + dt.Rows[0]["sAddressLine1"].ToString() + ',';

                            if (dt.Rows[0]["sAddressLine2"].ToString() != "")
                                guarantordetails = guarantordetails + dt.Rows[0]["sAddressLine2"].ToString() + ',' + Environment.NewLine; 
                            else { guarantordetails = guarantordetails +  Environment.NewLine; }

                            if (dt.Rows[0]["sCity"].ToString() != "")
                                guarantordetails = guarantordetails + dt.Rows[0]["sCity"].ToString() + ' ' + dt.Rows[0]["sState"].ToString() + ' ' + dt.Rows[0]["sZip"].ToString();

                            lblGuarantorDetails.Text = guarantordetails;

                            if (Convert.ToString(dt.Rows[0]["BusinessCenter"]) != "")
                            {
                                lblBusinessCenter.Text = Convert.ToString(dt.Rows[0]["BusinessCenter"]);
                            }
                            else
                            {
                                lblBusinessCenter.Text = "";
                            }
                        }
                        if (dt != null)
                        {
                            dt.Dispose();
                            dt = null;
                        }
                    }
                }
                onPatientAddAccountControl_Enter(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        /// <summary>
        /// Event to Close ListControl.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    removeOListControl();
                    onPatientAddAccountControl_Enter(sender, e);

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// select existing patient as guarantor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oListControl_GaurantorSelectedClick(object sender, EventArgs e)
        {
            try
            {
                Int64 _TempPatientID = 0;
                

                if (oListControl.SelectedItems.Count > 1)
                {
                    MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (oListControl.SelectedItems.Count > 0)
                {
                    //Remove Guarantor and add selected guarantor
                    if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                    {
                        oPatientGuarantors.RemoveAt(0);
                        txtAccGuarantor.Text = "";
                    }
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        _TempPatientID = Convert.ToInt64(oListControl.SelectedItems[i].ID);
                        bool _ShouldAdd = true;

                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            for (int _Count = 0; _Count < oPatientGuarantors.Count; _Count++)
                            {
                                if (Convert.ToInt64(oListControl.SelectedItems[i].ID) == oPatientGuarantors[_Count].GuarantorAsPatientID)
                                {
                                    _ShouldAdd = false;
                                    break;
                                }
                            }
                        }
                        if (_ShouldAdd == true)
                        {
                            gloPatient ogloPatient = new gloPatient(databaseconnectionstring);
                            Patient oPatientTemp = ogloPatient.GetPatientDemo(_TempPatientID);
                            if (oPatientTemp != null)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();

                                oGuarantor.GuarantorAsPatientID = _TempPatientID;
                                oGuarantor.PatientContactID = 0;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = oPatientTemp.DemographicsDetail.PatientFirstName;
                                oGuarantor.MiddleName = oPatientTemp.DemographicsDetail.PatientMiddleName;
                                oGuarantor.LastName = oPatientTemp.DemographicsDetail.PatientLastName;
                                oGuarantor.DOB = oPatientTemp.DemographicsDetail.PatientDOB;
                                oGuarantor.SSN = oPatientTemp.DemographicsDetail.PatientSSN;
                                oGuarantor.Gender = oPatientTemp.DemographicsDetail.PatientGender;
                                oGuarantor.Relation = "";
                                oGuarantor.AddressLine1 = oPatientTemp.DemographicsDetail.PatientAddress1;
                                oGuarantor.AddressLine2 = oPatientTemp.DemographicsDetail.PatientAddress2;
                                oGuarantor.City = oPatientTemp.DemographicsDetail.PatientCity;
                                oGuarantor.State = oPatientTemp.DemographicsDetail.PatientState;
                                oGuarantor.Zip = oPatientTemp.DemographicsDetail.PatientZip;
                                oGuarantor.Country = oPatientTemp.DemographicsDetail.PatientCountry;
                                oGuarantor.County = oPatientTemp.DemographicsDetail.PatientCounty;
                                oGuarantor.Phone = oPatientTemp.DemographicsDetail.PatientPhone;
                                oGuarantor.Mobile = oPatientTemp.DemographicsDetail.PatientMobile;
                                oGuarantor.Email = oPatientTemp.DemographicsDetail.PatientEmail;
                                oGuarantor.Fax = oPatientTemp.DemographicsDetail.PatientFax;
                                oGuarantor.OtherConatctType = PatientOtherContactType.Patient;
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;

                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(false);
                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;

                                if (oPatientGuarantors.Count == 0)
                                {
                                    this.oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        onPatientAddAccountControl_Enter(sender, e);
                                        return;
                                    }

                                }
                                oPatientTemp.Dispose();
                                oPatientTemp = null;
                            }
                            ogloPatient.Dispose();
                            ogloPatient = null;
                        }
                    }
                }

                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                {
                    for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                    {
                        txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                        setCmbSameAsGuardianIndex();
                    }
                }
                onPatientAddAccountControl_Enter(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        /// <summary>
        /// Save New Guarantor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ogloPatientGuarantorControl_SaveButton_Click(object sender, EventArgs e)
        {

            try
            {
                //assign guarantors of ogloPatientGuarantorControl to PatientGuarantors property of frmAddPatientAccount
                this.PatientGuarantors = ogloPatientGuarantorControl.PatientGuarantors;
                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                {
                    for (int gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                    {
                        txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                        setCmbSameAsGuardianIndex();
                    }
                }
                else
                {
                    txtAccGuarantor.Text = "";
                    IsCmbSameAsGuardianLoadFlag = false;
                    cmbSameAsGuardian.SelectedIndex = -1;
                    IsCmbSameAsGuardianLoadFlag = true;

                }
                this.Controls.Remove(ogloPatientGuarantorControl);
                try
                {
                    ogloPatientGuarantorControl.SaveButton_Click -= new gloPAGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                    ogloPatientGuarantorControl.CloseButton_Click -= new gloPAGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);

                    ogloPatientGuarantorControl.Dispose();
                    ogloPatientGuarantorControl = null;
                }
                catch
                {
                }
                onPatientAddAccountControl_Enter(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        /// <summary>
        /// Event to Close Guarantor Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ogloPatientGuarantorControl_CloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Controls.Remove(ogloPatientGuarantorControl);
                try
                {
                    ogloPatientGuarantorControl.SaveButton_Click -= new gloPAGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                    ogloPatientGuarantorControl.CloseButton_Click -= new gloPAGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);

                    ogloPatientGuarantorControl.Dispose();
                    ogloPatientGuarantorControl = null;
                }
                catch
                {
                }
                onPatientAddAccountControl_Enter(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        public void DisposeAllControls()
        {
            try
            {
                if (oPatientGuarantors != null) { oPatientGuarantors.Dispose(); }
                if (oPatientGuardian != null) { oPatientGuardian.Dispose(); }
                if (objgloAccount != null) { objgloAccount.Dispose(); }
                if (oPatientDemographicsDetails != null) { oPatientDemographicsDetails.Dispose(); }
                if (oListControl != null) { removeOListControl(); }
                if (oAccount != null) { oAccount.Dispose(); }
                if (oPatientAccount != null) { oPatientAccount.Dispose(); }
                if (oPatientAccounts != null) { oPatientAccounts.Dispose(); }
                if (ogloPatientGuarantorControl != null) 
                {
                    try
                    {
                        ogloPatientGuarantorControl.SaveButton_Click -= new gloPAGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                        ogloPatientGuarantorControl.CloseButton_Click -= new gloPAGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);

                        ogloPatientGuarantorControl.Dispose();
                        ogloPatientGuarantorControl = null;
                    }
                    catch
                    {
                    }
                     
                }
                try
                {
                    if (oToolTip1 != null)
                    {
                        oToolTip1.Dispose();
                        oToolTip1 = null;
                    }
                }
                catch
                {
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbBusinessCenter_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                combo = cmbBusinessCenter;
                if (cmbBusinessCenter.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]), cmbBusinessCenter) >= cmbBusinessCenter.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbBusinessCenter, Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbBusinessCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                combo = cmbBusinessCenter;
                if (cmbBusinessCenter.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]), cmbBusinessCenter) >= cmbBusinessCenter.DropDownWidth - 20)
                    {
                        this.toolTip1.SetToolTip(cmbBusinessCenter, Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip1.SetToolTip(cmbBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            int width=0;
            Graphics g = this.CreateGraphics();
            if (g!=null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            
            return width;
        }

        void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        {
            try
            {
                combo = (ComboBox)sender;
                if (combo.Items.Count > 0 && e.Index >= 0)
                {

                    e.DrawBackground();
                    using (SolidBrush br = new SolidBrush(e.ForeColor))
                    {
                        e.Graphics.DrawString(combo.GetItemText(combo.Items[e.Index]).ToString(), e.Font, br, e.Bounds);
                    }

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        if (combo.DroppedDown)
                        {
                            string txt = combo.GetItemText(combo.Items[e.Index]).ToString();


                            if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth - 20)
                            {
                                if (toolTip1.GetToolTip(combo) != txt)
                                {
                                    this.toolTip1.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 180, e.Bounds.Bottom);
                                }
                            }
                            else
                            {
                                this.toolTip1.SetToolTip(combo, "");
                            }
                        }
                        else
                        {
                            this.toolTip1.Hide(combo);
                        }
                    }
                    else
                    {
                        
                    }
                    e.DrawFocusRectangle();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
    }
}
