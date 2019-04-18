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

namespace gloContacts
{
    public partial class frmSetupPharmacy : Form
    {
        #region "Private Variables"

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private bool _IsInternetFax = false;

        private Int64 _ContactID = 0;
        private Int64 _ClinicID = 0;
        private bool _isReadonly = false;
      //  private bool _bIsothers = false;
        gloAddress.gloAddressControl oAddresscontrol = null;
        public Int64 ContactID
        {
            get { return _ContactID;}
            set { _ContactID = value; }
        }
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion "Private Variables"

   
        public frmSetupPharmacy(string DatabaseConnectionString)
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

      
        public frmSetupPharmacy(Int64 ContactId, string DatabaseConnectionString)
        {
            InitializeComponent();

            _databaseconnectionstring = DatabaseConnectionString;

            _ContactID = ContactId;

            //_ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
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
        //Constructor Added for 
        public frmSetupPharmacy(Int64 ContactId, Boolean  ISReadonly, string DatabaseConnectionString)
        {
            InitializeComponent();

            _databaseconnectionstring = DatabaseConnectionString;

            _ContactID = ContactId;
            _isReadonly = ISReadonly;
            //_ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
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
       
        private void frmSetupPharmacy_Load(object sender, EventArgs e)
        {
            Cls_TabIndexSettings tabSettings = new Cls_TabIndexSettings(this);
            tabSettings.SetTabOrder(Cls_TabIndexSettings.TabScheme.AcrossFirst);

            oAddresscontrol = new gloAddress.gloAddressControl(_databaseconnectionstring, true);
            oAddresscontrol.Dock = DockStyle.Fill;
            pnlAddresssControl.Controls.Add(oAddresscontrol);
            //Sandip Darade 20091006
            //If fax is not internet fax do no masking  for fax information
            if (_IsInternetFax == false)
            {
                txtFax.MaskType = gloMaskControl.gloMaskType.Other;
            }
            txtname.Select();
            
            if (_ContactID != 0)
            {
                if (_isReadonly == true)
                {
                    this.Size = new Size(458,574);
                    gBox_ePharmacy.Visible = true;
                    ts_btnSave.Enabled = false;
                    SetControls();
                    this.Text = "Pharmacy";
                }
                LoadPharmacy();
                
            }

            if (tabSettings != null) { tabSettings = null; }
        }

        private void LoadPharmacy()
        {

            gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
            gloPMContacts.Pharmacy oPharmacy = new gloPMContacts.Pharmacy();

            try
            {
                oPharmacy = oglocontact.SelectPharmacy(_ContactID);

                txtname.Text = oPharmacy.Name;
                txtcontact.Text = oPharmacy.ContactName;

                //txtAddressLine1.Text = oPharmacy.CompanyAddress.AddrressLine1;
                //txtAddressLine2.Text = oPharmacy.CompanyAddress.AddrressLine2;
                //txtCity.Text = oPharmacy.CompanyAddress.City;
                //txtState.Text = oPharmacy.CompanyAddress.State;
                //txtZip.Text = oPharmacy.CompanyAddress.ZIP;
                //Sandip Darade 20091009 gloAddress control implemented  replacing code for address info above with code below 

                oAddresscontrol.txtAddress1.Text = oPharmacy.CompanyAddress.AddrressLine1;
                oAddresscontrol.txtAddress2.Text = oPharmacy.CompanyAddress.AddrressLine2;
                oAddresscontrol.txtCity.Text = oPharmacy.CompanyAddress.City;
                oAddresscontrol.cmbState.Text = oPharmacy.CompanyAddress.State;
                oAddresscontrol._isTextBoxLoading = true;
                oAddresscontrol.txtZip.Text = oPharmacy.CompanyAddress.ZIP;
                oAddresscontrol._isTextBoxLoading = false;

                mtxtPhone.Text = oPharmacy.CompanyAddress.Phone;
                txtFax.Text = oPharmacy.CompanyAddress.Fax;
                txtEmail.Text = oPharmacy.CompanyAddress.Email;
                txtURL.Text = oPharmacy.CompanyAddress.URL;
                mtxtMobile.Text = oPharmacy.Mobile;

                mtxtPager.Text = oPharmacy.Pager;
                //txtPager.Text = oPharmacy.Pager;

                //Fields for ePharmacy
                txt_NCPDID.Text = oPharmacy.NCPDPID;
                txt_PharmacyStatus.Text = oPharmacy.PharmacyStatus;
                txt_ServiceLevel.Text = oPharmacy.ServiceLevel;
                if (oPharmacy.ActiveStartTime.HasValue == true)
                {
                    txt_ActivityStartTime.Text = Convert.ToDateTime(oPharmacy.ActiveStartTime).ToString("MM/dd/yyyy HH:mm");
                }
                if (oPharmacy.ActiveEndTime.HasValue == true)
                {
                    txt_ActivityEndTime.Text = Convert.ToDateTime(oPharmacy.ActiveEndTime).ToString("MM/dd/yyyy HH:mm");
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oglocontact != null) { oglocontact.Dispose(); oglocontact = null; }
                if (oPharmacy != null) { oPharmacy.Dispose(); oPharmacy = null; }
            }

        }

        private bool ModifyPatientPharmacy(gloPMContacts.Pharmacy oPharmacy)
        {
            gloPMContacts.gloContacts ogloContacts = new gloPMContacts.gloContacts(_databaseconnectionstring);
            bool _result = false;
            try
            {
                bool IsPatientPharmacy = false;
                
                IsPatientPharmacy = ogloContacts.IsPatientPharmacy(oPharmacy.ContactID);

                if (IsPatientPharmacy == true)
                {
                    DialogResult _DialogResult = DialogResult.None;
                    _DialogResult = MessageBox.Show("This pharmacy is associated with patient.  Do you want to modify pharmacy?  ", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (_DialogResult == DialogResult.Yes)
                    {
                        ogloContacts.ModifyPatientPharmacy(oPharmacy);
                        _result = true;
                    }
                    else if (_DialogResult == DialogResult.No)
                    {
                        _result = false;
                    }
                    else if (_DialogResult == DialogResult.Cancel)
                    {
                        _result = false;
                    }
                }
                else
                {
                    _result = true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloContacts != null) { ogloContacts.Dispose(); ogloContacts = null; }
            }
            return _result;
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void ts_btnSave_Click(object sender, EventArgs e)
        { 
            //Added By MaheshB
            gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
            try
            {
               
                    bool _result;
                    string _Pharmacy = txtname.Text.Trim();
                    _result = oglocontact.IsExistsPharmacy(_Pharmacy, _ContactID);
                    if (_result == true)
                    {
                        if (DialogResult.No == (MessageBox.Show("Contact name already exists. Do you want to register it anyway?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)))
                        {
                            return;
                        }

                    }
                    _Pharmacy = null;
                if (ValidateData() == true)
                {
                   
                    if (SaveData() == true)
                    {
                        this.DialogResult = DialogResult.OK; 
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oglocontact.Dispose();
            }
        }

     

      

        private bool SaveData()
        {
            gloPMContacts.Pharmacy oPharmacy = new gloPMContacts.Pharmacy();
            gloPMContacts.gloContacts ogloContacts = new gloPMContacts.gloContacts(_databaseconnectionstring);
            bool _result = false;
            try
            {
                oPharmacy = AddPharmacyContacts();
                //New Contact
                if (_ContactID == 0)
                {
                    _ContactID = ogloContacts.Add(oPharmacy);

                    if (_ContactID > 0)
                    {
                        _result = true;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Pharmacy, ActivityType.Add, "Pharmacy added", 0, _ContactID, 0, ActivityOutCome.Success);
                    }
                    else
                    {
                        _result = false;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Pharmacy, ActivityType.Add, "Add pharmacy", 0, _ContactID, 0, ActivityOutCome.Failure);
                    }
                }
                //contact to modify 
                else
                {
                    oPharmacy.ContactID = _ContactID;
                    if (ModifyPatientPharmacy(oPharmacy) == true)
                    {
                        if (ogloContacts.Add(oPharmacy) > 0)
                        {
                            _result = true;
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Pharmacy, ActivityType.Modify, "Pharmacy modified", 0, _ContactID, 0, ActivityOutCome.Success);
                        }
                        else
                        {
                            _result = false;
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Pharmacy, ActivityType.Modify, "Modify pharmacy", 0, _ContactID, 0, ActivityOutCome.Failure);
                        }
                    }
                    else
                    {
                        _result = false;
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Physician, ActivityType.Modify, "Modified physician", 0, _ContactID, 0, ActivityOutCome.Failure);
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oPharmacy != null) { oPharmacy.Dispose(); oPharmacy = null; }
                if (ogloContacts != null) { ogloContacts.Dispose(); ogloContacts = null; }
            }
            return _result; 
        }

        private gloPMContacts.Pharmacy AddPharmacyContacts()
        {
            gloPMContacts.Pharmacy oPharmacy = new gloPMContacts.Pharmacy();
            try
            {
                oPharmacy.Name = txtname.Text;
                oPharmacy.ContactName = txtcontact.Text;
                oPharmacy.Mobile = mtxtMobile.Text;

                //oPharmacy.Pager = txtPager.Text;
                oPharmacy.Pager = mtxtPager.Text;

                //oPharmacy.CompanyAddress.AddrressLine1 = txtAddressLine1.Text.Trim();
                //oPharmacy.CompanyAddress.AddrressLine2 = txtAddressLine2.Text.Trim();
                //oPharmacy.CompanyAddress.City = txtCity.Text.Trim();
                //oPharmacy.CompanyAddress.State = txtState.Text.Trim();
                //oPharmacy.CompanyAddress.ZIP = txtZip.Text.Trim();
                //Sandip Darade 20091009 gloAddress control implemented  replacing code for address info above with code below 

                //Sandip Darade 20091009 gloAddress control implemented  replacing code for address info above with code below 

                oPharmacy.CompanyAddress.AddrressLine1 = oAddresscontrol.txtAddress1.Text.Trim();
                oPharmacy.CompanyAddress.AddrressLine2 = oAddresscontrol.txtAddress2.Text.Trim();
                oPharmacy.CompanyAddress.City = oAddresscontrol.txtCity.Text.Trim();
                oPharmacy.CompanyAddress.State = oAddresscontrol.cmbState.Text.Trim();
                oPharmacy.CompanyAddress.ZIP = oAddresscontrol.txtZip.Text.Trim();



                oPharmacy.CompanyAddress.Phone = mtxtPhone.Text.Trim();
                oPharmacy.CompanyAddress.Fax = txtFax.Text.Trim();
                oPharmacy.CompanyAddress.Email = txtEmail.Text.Trim();
                oPharmacy.CompanyAddress.URL = txtURL.Text.Trim();
                oPharmacy.ContactType = gloPMContacts.ContactType.Pharmacy.ToString();
                return oPharmacy;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oPharmacy != null) { oPharmacy.Dispose(); oPharmacy = null; }
            }
        }

        private bool ValidateData()
        {
            if (txtname.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a pharmacy name.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtname.Focus();
                return false;
            }
            if (CheckEmailAddress(txtEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //Sandip Darade 20090424
            ///Validations  removed as gloMask control being used
            ////validate the phone no. and mobile no.
            //if (mtxtPhone.Text.Trim().Length > 0 & mtxtPhone.Text.Trim().Length < 10)
            //{
            //    MessageBox.Show("Please enter a 10 digit number for phone.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mtxtPhone.Focus();
            //    return false;
            //}
            if (mtxtPhone.IsValidated == false)
            {
                return false;
            }
            if (txtFax.IsValidated == false)
            {
                return false;
            }
            ////validate the phone no. and mobile no.
            //if (mtxtMobile.Text.Trim().Length > 0 & mtxtMobile.Text.Trim().Length < 10)
            //{
            //    MessageBox.Show("Please enter a 10 digit number for mobile.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    mtxtMobile.Focus();
            //    return false;
            //}
            if (mtxtMobile.IsValidated == false)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(txtURL.Text))
            {
                if (CheckURL(txtURL.Text) == false)
                {
                    MessageBox.Show("Please enter a valid URL. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                } 
            }

            return true; 
        }
        //Sandip Darade 20090924
        //make the controls readonly for e-Pharmacyies
        // BackColor Set for all the readonly controls by Pankaj Bedse 11012010
        // To resolve issue no #0000183 
        private void SetControls()
        {
            txtname.ReadOnly = true;
            txtname.BackColor = Color.LightGray;
            
            txtcontact.ReadOnly = true;
            txtcontact.BackColor = Color.LightGray;

            txt_ActivityEndTime.ReadOnly = true;
            txt_ActivityEndTime.BackColor = Color.LightGray;

            txt_ActivityStartTime.ReadOnly = true;
            txt_ActivityStartTime.BackColor = Color.LightGray;

            txt_NCPDID.ReadOnly = true;
            txt_NCPDID.BackColor = Color.LightGray;

            txt_PharmacyStatus.ReadOnly = true;
            txt_PharmacyStatus.BackColor = Color.LightGray;

            txt_ServiceLevel.ReadOnly = true;
            txt_ServiceLevel.BackColor = Color.LightGray;

            txtEmail.ReadOnly = true;
            txtEmail.BackColor = Color.LightGray;

            txtFax.ReadOnly = true;
            txtFax.BackColor = Color.LightGray;

            mtxtPhone.ReadOnly = true;
            mtxtPhone.BackColor = Color.LightGray;

            mtxtMobile.ReadOnly = true;
            mtxtMobile.BackColor = Color.LightGray;

            //txtPager.ReadOnly = true;
            //txtPager.BackColor = Color.LightGray;

            mtxtPager.ReadOnly = true;
            mtxtPager.BackColor = Color.LightGray;

            txtURL.ReadOnly = true;
            txtURL.BackColor = Color.LightGray;

            //txtState.ReadOnly = true;
            //txtAddressLine1.ReadOnly = true;
            //txtAddressLine2.ReadOnly = true;
            //txtCity.ReadOnly = true;
            //txtZip.ReadOnly = true;

            //Sandip Darade 20091022
            oAddresscontrol.txtAddress1.ReadOnly = true;
            oAddresscontrol.txtAddress1.BackColor = Color.LightGray;

            oAddresscontrol.txtAddress2.ReadOnly = true;
            oAddresscontrol.txtAddress2.BackColor = Color.LightGray;

            oAddresscontrol.txtCity.ReadOnly = true;
            oAddresscontrol.txtCity.BackColor = Color.LightGray;

            oAddresscontrol.txtCounty.ReadOnly = true;
            oAddresscontrol.txtCounty.BackColor = Color.LightGray;

            oAddresscontrol.cmbCountry.Enabled = false;
            oAddresscontrol.cmbCountry.BackColor = Color.LightGray;

            oAddresscontrol.txtZip.ReadOnly = true;
            oAddresscontrol.txtZip.BackColor = Color.LightGray;

            oAddresscontrol.cmbState.Enabled = false;
        }

        private void txtZip_TextChanged(object sender, EventArgs e)
        {
            if (txtZip.Text.Trim() != "")
            {
                DataTable dt = null;
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = '" + txtZip.Text.Trim() + "'";

                    txtState.Text = "";
                    txtCity.Text = "";

                    oDb.Retrive_Query(qry, out dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        txtState.Text = Convert.ToString(dt.Rows[0]["ST"]);
                        txtCity.Text = Convert.ToString(dt.Rows[0]["City"]);
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
                    dt.Dispose();
                    oDb.Disconnect();
                    oDb.Dispose();
                }
            }
            else
            {
                txtState.Text = ""; 
                txtCity.Text = "";
            }

        }

        private void txtZip_KeyPress(object sender, KeyPressEventArgs e)
        { //code to allow nos only 
            //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            //{

            //    e.Handled = true;
            //}
            //
            //Sandip Darade 20090925
            //allow alpha numeric characters for zip 
            if (!(e.KeyChar == Convert.ToChar(8)))
            {
                if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                {
                    e.Handled = true;
                }
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

        private void MaskTextBox_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        public bool CheckURL(string input)
        {
            bool response = false;

            System.Globalization.CompareInfo cmpUrl = System.Globalization.CultureInfo.InvariantCulture.CompareInfo;

            //if (cmpUrl.IsPrefix(input, "http://") == false)
            //{ input = "http://" + input; }

            Regex RgxUrl = new Regex("^(((ht|f){1}((tp|tps):[/][/]){1})|((www.){1}))[-a-zA-Z0-9@:%_\\+.~#?&//=]+$");

            if (RgxUrl.IsMatch(input))
            { response = true; }
            else
            { response = false; }

            RgxUrl = null;

            return response;
        }

        private void txtURL_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtURL.Text))
            {
                if (CheckURL(txtURL.Text) == false)
                {
                    MessageBox.Show("Please enter a valid URL ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
            }
        }
    }
}