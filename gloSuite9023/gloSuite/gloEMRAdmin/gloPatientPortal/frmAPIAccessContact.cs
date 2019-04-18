using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using System.Text.RegularExpressions;
using gloCommon;

namespace gloPatientPortal
{
    public partial class frmAPIAccessContact : Form
    {
        public bool _showGrid = false;
        public bool ShowGrid
        {
            get { return _showGrid; }
            set
            {
                _showGrid = value;
                ShowGridPanel(value);
            }
        }



        #region "Private Variables"
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private bool _IsInternetFax = false;
        gloAddress.gloAddressControl oAddresscontrol = null;
        private Int64 _ContactID = 0;
        private Int64 _loginUserId = 0;
        private Int64 _ClinicID = 0;
        private bool _IsModified = false; // added on 20100617
        private bool _IsSaveClicked = false;
        public Int64 ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; }
        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        #endregion "Private Variables"

        #region "Contructor"

        public frmAPIAccessContact(string DatabaseConnectionString)
        {
            InitializeComponent();

            _databaseconnectionstring = DatabaseConnectionString;
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            //Sandip Darade  20090428
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

            //Sandip Darade  20091006
            #region " Retrieve Internet fax setting from AppSettings "

            if (appSettings["Internet Fax"] != null)
            {
                if (appSettings["Internet Fax"] != "")
                {
                    _IsInternetFax = Convert.ToBoolean(appSettings["Internet Fax"]);
                }
                else
                {
                    _IsInternetFax = false;
                }
            }
            else
            { _IsInternetFax = false; }

            #endregion

        }
        public frmAPIAccessContact(string DatabaseConnectionString, long ContactID , long loginuserid)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _ContactID = ContactID;
            _loginUserId = loginuserid;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }


            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloEMR";
                }
            }
            else
            { _messageBoxCaption = "gloEMR"; }

            #endregion



        }
        #endregion

        #region "ToolStrip Buttons"

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

                    clsAPIContact objAPIcontact = new clsAPIContact(_databaseconnectionstring);
                    objAPIcontact.APIAccessUserId = _ContactID;
                    objAPIcontact.FirstName = txtFirstName.Text.Trim();
                    objAPIcontact.MiddleName = txtMiddleName.Text.Trim();
                    objAPIcontact.LastName = txtLastname.Text.Trim();

                    objAPIcontact.Gender = cmbGender.Text;
                    objAPIcontact.DOB = Convert.ToDateTime(mtxtPADOB.Text);
                    objAPIcontact.RoleID =Convert.ToInt64( cmbRole.SelectedValue);
                    objAPIcontact.AddressLine1 = oAddresscontrol.txtAddress1.Text.Trim();
                    objAPIcontact.AddressLine2 = oAddresscontrol.txtAddress2.Text.Trim();
                    objAPIcontact.City = oAddresscontrol.txtCity.Text.Trim();
                    objAPIcontact.State = oAddresscontrol.cmbState.Text.Trim();
                    objAPIcontact.Zip = oAddresscontrol.txtZip.Text.Trim();
                    objAPIcontact.Country = oAddresscontrol.cmbCountry.Text.Trim();
                    objAPIcontact.County = oAddresscontrol.txtCounty.Text.Trim();

                    objAPIcontact.Phone = mtxtPhone.Text.Trim();
                    objAPIcontact.Mobile = mtxtMobile.Text.Trim();
                    objAPIcontact.Email = txtEmail.Text.Trim();
                    objAPIcontact.UserID = _loginUserId;

                    if (_IsModified)
                    {
                        if (objAPIcontact.Modify(objAPIcontact))
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIUser, gloAuditTrail.ActivityType.Modify, "API User "+ txtFirstName.Text.Trim()+" "+txtLastname.Text.Trim()+"for role "+cmbRole.Text+" modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                            this.Close();
                        
                        }
                    }
                    else
                    {
                        if (objAPIcontact.Add(objAPIcontact))
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.API, gloAuditTrail.ActivityCategory.APIUser, gloAuditTrail.ActivityType.Add, "API User " + txtFirstName.Text.Trim() + " " + txtLastname.Text.Trim() + "for role " + cmbRole.Text + " added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                            this.Close();

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                 gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion
        private bool ValidateData()
        {
            if (txtFirstName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter First name.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFirstName.Focus();
                return false;
            }
          

           
            if (txtLastname.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Last name.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLastname.Focus();
                return false;
            }

          
            if (mtxtPADOB.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Date of birth.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtxtPADOB.Focus();
                return false;
            }
            //date of birth   
            mtxtPADOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (mtxtPADOB.Text.Length > 0 && mtxtPADOB.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid Date of birth.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtxtPADOB.Focus();
                return false;
            }
            else
            {
                if (mtxtPADOB.MaskCompleted == true)
                {
                    try
                    {
                        mtxtPADOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        if (IsValidDate(mtxtPADOB.Text))
                        {
                            if (Convert.ToDateTime(mtxtPADOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtPADOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(mtxtPADOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                            {
                                MessageBox.Show("Enter a valid Date of birth.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mtxtPADOB.Focus();
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter a valid Date of birth.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mtxtPADOB.Focus();
                            return false;
                        }
                    }
                    catch (Exception) // ex)
                    {
                        //ex.ToString();
                        //ex = null;
                        MessageBox.Show("Enter a valid Date of birth.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mtxtPADOB.Focus();
                        return false;
                    }
                }
            }
            if (cmbGender.Text.Trim() == "")
            {
                MessageBox.Show("Please select Gender.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtxtPADOB.Focus();
                return false;
            }
            if (cmbRole.Text.Trim() == "")
            {
                MessageBox.Show("Please select Role.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtxtPADOB.Focus();
                return false;
            }
            //if (oAddresscontrol.txtZip.Text.Trim() == "")
            //{
            //    MessageBox.Show("Please select Zip code.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    oAddresscontrol.txtZip.Focus();
            //    return false;
            //}
            if (mtxtPhone.Text == "")
            {
                MessageBox.Show("Please enter Phone", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtxtPhone.Focus();
                return false;
            }
            if (mtxtPhone.IsValidated == false)
            {
                MessageBox.Show("Please enter valid Phone", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtxtPhone.Focus();
                return false;
            }
            if (txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Email ID.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }
          
            if (CheckEmailAddress(txtEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid Email ID.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }

            return true;
        }
        private bool IsValidDate(string DOB)
        {
            Int32 year = 0; Int32 month = 0; Int32 day = 0;

            if (DOB.Trim().Length <= 4)   // for blank date,length=4 ,including '/' character...  
            {
                return true;
            }

            //*****
            string[] _Date = DOB.Split('/');
            if (_Date.Length == 3)
            {
                for (int i = 0; i < _Date.Length; i++)
                {
                    if (_Date[i].Trim() != "")
                    {
                        if (i == 0)
                        {
                            month = Convert.ToInt32(_Date[i]);
                        }
                        if (i == 1)
                        {
                            day = Convert.ToInt32(_Date[i]);
                        }
                        if (i == 2)
                        {

                            if (_Date[i].Trim().Replace("_", "").Length == 4)
                                year = Convert.ToInt32(_Date[i]);
                            else
                                return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }

                if (month > 12)
                {
                    return false;
                }

                if (day == 29)
                {
                    if (month == 2)
                    {
                        if (year % 4 == 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    else
                    {
                        return true;
                    }
                }
                else if (day > 31)
                {
                    return false;
                }
                else if (day == 0)
                {
                    return false;
                }
                else if (day == 31)
                {
                    if (month == 2 || month == 4 || month == 6 || month == 9 || month == 11)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        public void ShowGridPanel(bool showListmode)
        {
            if (showListmode)
            {
                pnlGrid.BringToFront();
                pnl_Base.SendToBack();
            }
            else
            {
                pnl_Base.BringToFront();
                pnlGrid.SendToBack();
            }
        }

        private void FILLRoleCombo()
        {
            try
            {
                clsAPIRole objAPIRole = new clsAPIRole(_databaseconnectionstring);
                DataTable dtRoleData = objAPIRole.GetAPIRoles(0,true);
                cmbRole.DataSource = dtRoleData;
                cmbRole.DisplayMember = "sRoleName";
                cmbRole.ValueMember = "nRoleId";
              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        private void frmAPIAccessContact_Load(object sender, EventArgs e)
        {
            try
            {
                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                tom.SetTabOrder(scheme);
                FILLRoleCombo();
                oAddresscontrol = new gloAddress.gloAddressControl(_databaseconnectionstring, false);
                oAddresscontrol.Dock = DockStyle.Fill;
                 
                oAddresscontrol.Name = "APIUserAddessControl";
                pnlAddresssControl.Controls.Add(oAddresscontrol);

                if (_ContactID > 0)
                {
                    _IsModified = true;
                    clsAPIContact objAPIContact = new clsAPIContact(_databaseconnectionstring);

                    DataTable dtRoleData = objAPIContact.GetAPIContacts(_ContactID);
                    if (dtRoleData != null && dtRoleData.Rows.Count > 0)
                    {
                        txtFirstName.Text = dtRoleData.Rows[0]["sFirstName"].ToString();
                        txtLastname.Text = dtRoleData.Rows[0]["sLastName"].ToString();
                        cmbRole.SelectedValue = dtRoleData.Rows[0]["nRoleId"].ToString();
                        mtxtPADOB.Text = Convert.ToDateTime(dtRoleData.Rows[0]["dtDOB"]).ToString("MM/dd/yyyy");
                        cmbGender.Text = dtRoleData.Rows[0]["sGender"].ToString();
                        txtEmail.Text = dtRoleData.Rows[0]["sEmail"].ToString();
                        txtMiddleName.Text = dtRoleData.Rows[0]["sMiddleName"].ToString();
                       
                        oAddresscontrol.txtAddress1.Text = dtRoleData.Rows[0]["sAddressLine1"].ToString();
                        oAddresscontrol.txtAddress2.Text = dtRoleData.Rows[0]["sAddressLine2"].ToString();
                        oAddresscontrol.txtCity.Text = dtRoleData.Rows[0]["sCity"].ToString();
                        oAddresscontrol.cmbState.Text = dtRoleData.Rows[0]["sState"].ToString();
                        oAddresscontrol.cmbCountry.Text = dtRoleData.Rows[0]["sCountry"].ToString();
                        oAddresscontrol._isTextBoxLoading = true;
                        oAddresscontrol.txtZip.Text = dtRoleData.Rows[0]["sZip"].ToString();
                        oAddresscontrol._isTextBoxLoading = false;
                        oAddresscontrol.txtCounty.Text = dtRoleData.Rows[0]["sCounty"].ToString();

                        mtxtPhone.Text = dtRoleData.Rows[0]["sPhone"].ToString();
                        mtxtMobile.Text = dtRoleData.Rows[0]["sMobile"].ToString();
                       
                    }
                }
                else
                {
                    _IsModified = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        #region "Email Address Validation"

        public bool CheckEmailAddress(string input)
        {
            bool response = false;
            if (Regex.IsMatch(input, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") || input.Trim() == "")
            {
                response = true;
            }
            else
            {
                response = false;
            }
            return response;
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txtEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        #endregion

        private void ts_btnSaveandActivate_Click(object sender, EventArgs e)
        {

        }

        private void mtxtPADOB_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        private void mtxtPADOB_Validating(object sender, CancelEventArgs e)
        {
            mtxtPADOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (mtxtPADOB.Text.Length > 0 && mtxtPADOB.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid date of birth.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                if (mtxtPADOB.MaskCompleted == true)
                {
                    try
                    {
                        mtxtPADOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        //Code review changes Replace IsValidDate() by gloDateMaster.gloDate.IsValidDateV2()
                        if (IsValidDate(mtxtPADOB.Text))
                        {
                            //if (Convert.ToDateTime(mtxtPADOB.Text).Date >= DateTime.Now.Date)
                            //if (Convert.ToDateTime(mtxtPADOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtPADOB.Text).Date >= DateTime.Now.Date || Convert.ToDateTime(mtxtPADOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                            if (Convert.ToDateTime(mtxtPADOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mtxtPADOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(mtxtPADOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                            {
                                MessageBox.Show("Enter a valid date of birth.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mtxtPADOB.Focus();
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter a valid date of birth.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                    catch (Exception) // ex)
                    {
                        //ex.ToString();
                        //ex = null;
                        MessageBox.Show("Enter a valid date of birth.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
        }






    }
}