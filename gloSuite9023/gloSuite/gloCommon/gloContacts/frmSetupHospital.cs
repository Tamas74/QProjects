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
    public partial class frmSetupHospital : Form
    {
        #region "Private Variables"

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private bool _IsInternetFax = false;
        gloAddress.gloAddressControl oAddresscontrol = null;

        private Int64 _ContactID = 0;
        private Int64 _ClinicID = 0;

        private bool _IsModified = false; // added on 20100617
        private bool _IsSaveClicked = false;

        private bool _IsOtherContact = false;   

        public Int64 ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; }

        }

        public bool IsOtherContact
        {
            get { return _IsOtherContact; }
            set { _IsOtherContact = value; }
        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion "Private Variables"

        #region "Contructor"

        public frmSetupHospital(string DatabaseConnectionString)
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
        public frmSetupHospital(Int64 ContactId, string DatabaseConnectionString)
        {
            InitializeComponent();

            _databaseconnectionstring = DatabaseConnectionString;

            _ContactID = ContactId;

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
        #endregion

        #region "ToolStrip Buttons"

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();           
            
        }

        private void ts_btnSave_Click(object sender, EventArgs e)
        {
            gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
            string _HospitalName = txtname.Text.Trim();
            try
            {
                //pnlTopToolStrip.Focus();
                //Added By MaheshB
                bool _result;
                _result = oglocontact.IsExistsHospital(_HospitalName, _ContactID, _IsOtherContact);
                if (_result == true)
                {
                    if (DialogResult.No == (MessageBox.Show("Contact name already exists. Do you want to register it anyway?  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)))
                    {
                        return;
                    }

                }
                if (ValidateData() == true)
                {
                    if (SaveData() == true)
                    {
                        _IsSaveClicked = false;
                        _IsModified = false;
                        this.Close();
                    }
                    _IsSaveClicked = false; // added on 20100617
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally 
            {
                if (oglocontact != null) { oglocontact.Dispose(); oglocontact = null; }
                _HospitalName = null;
            }
        }

        #endregion

        private void LoadHospital()
        {
            gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
            gloPMContacts.Hospital oHospital = new gloPMContacts.Hospital();
            try
            {
                if (_IsOtherContact == false)
                {
                    oHospital = oglocontact.SelectHospital(_ContactID);
                }
                else
                {
                    oHospital = oglocontact.SelectOthers(_ContactID);
                }

                txtname.Text = oHospital.Name;
                txtcontact.Text = oHospital.ContactName;

                //txtAddressLine1.Text = oHospital.CompanyAddress.AddrressLine1;
                //txtAddressLine2.Text = oHospital.CompanyAddress.AddrressLine2;
                //txtCity.Text = oHospital.CompanyAddress.City;
                //txtState.Text = oHospital.CompanyAddress.State;
                //txtZip.Text = oHospital.CompanyAddress.ZIP;
                //Sandip Darade 20091009 gloAddress control implemented  replacing code for address info above with code below 

                oAddresscontrol.txtAddress1.Text = oHospital.CompanyAddress.AddrressLine1;
                oAddresscontrol.txtAddress2.Text = oHospital.CompanyAddress.AddrressLine2;
                oAddresscontrol.txtCity.Text = oHospital.CompanyAddress.City;
                oAddresscontrol.cmbState.Text = oHospital.CompanyAddress.State;
                oAddresscontrol._isTextBoxLoading = true;
                oAddresscontrol.txtZip.Text = oHospital.CompanyAddress.ZIP;
                oAddresscontrol._isTextBoxLoading = false;

                mtxtPhone.Text = oHospital.CompanyAddress.Phone;
                txtFax.Text = oHospital.CompanyAddress.Fax;
                txtEmail.Text = oHospital.CompanyAddress.Email;
                txtURL.Text = oHospital.CompanyAddress.URL;
                mtxtMobile.Text = oHospital.Mobile;
                mtxtPager.Text = oHospital.Pager;
                //txtPager.Text = oHospital.Pager;
                txtNPI.Text = oHospital.HospitalNPI;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oglocontact != null) { oglocontact.Dispose(); oglocontact = null; }
                if (oHospital != null) { oHospital.Dispose(); oHospital = null; }
            }

        }

        private bool SaveData()
        {
            //Shubhangi
             //frmViewContacts oContact;
                gloPMContacts.Hospital oHospital = null;
                gloPMContacts.gloContacts ogloContacts = null;
                bool _result = false;
                 try
                 {
                   
                     if (_IsOtherContact == false)
                     {  
                         oHospital = new gloPMContacts.Hospital();
                        ogloContacts = new gloPMContacts.gloContacts(_databaseconnectionstring);
                         try
                         {
                             _IsSaveClicked = true; // added on 20100617

                             oHospital = AddHospitalContacts();
                             //New Contact
                             if (_ContactID == 0)
                             {
                                 _ContactID = ogloContacts.Add(oHospital);

                                 if (_ContactID > 0)
                                 {
                                     _result = true;
                                     gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.Add, "Hospital added", 0, _ContactID, 0, ActivityOutCome.Success);
                                 }
                                 else
                                 {
                                     _result = false;
                                     gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.Add, "Add hospital", 0, _ContactID, 0, ActivityOutCome.Failure);
                                 }
                             }
                             //contact to modify 
                             else
                             {
                                 oHospital.ContactID = _ContactID;

                                 if (ogloContacts.Add(oHospital) > 0)
                                 {
                                     _result = true;
                                     gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.Modify, "Hospital modified", 0, _ContactID, 0, ActivityOutCome.Success);
                                 }
                                 else
                                 {
                                     _result = false;
                                 }
                             }

                         }
                         catch (Exception ex)
                         {
                             gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                         }
                         finally
                         {}
                         return _result;

                     }
                     else //if (oContact.IsForModify = true)
                     {
                         oHospital = new gloPMContacts.Hospital();
                         ogloContacts = new gloPMContacts.gloContacts(_databaseconnectionstring);
                         //bool _result = false;
                         try
                         {
                             oHospital = AddHospitalContacts();
                             //New Contact
                             if (_ContactID == 0)
                             {
                                 _ContactID = ogloContacts.Add(oHospital);

                                 if (_ContactID > 0)
                                 {
                                     _result = true;
                                     gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.Add, "Hospital added", 0, _ContactID, 0, ActivityOutCome.Success);
                                 }
                                 else
                                 {
                                     _result = false;
                                     gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.Add, "Add hospital", 0, _ContactID, 0, ActivityOutCome.Failure);
                                 }
                             }
                             //contact to modify 
                             else
                             {
                                 oHospital.ContactID = _ContactID;

                                 if (ogloContacts.Add(oHospital) > 0)
                                 {
                                     _result = true;
                                     gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Hospital, ActivityType.Modify, "Hospital modified", 0, _ContactID, 0, ActivityOutCome.Success);
                                 }
                                 else
                                 {
                                     _result = false;
                                 }
                             }
                         }
                         catch
                         {

                         }
                         finally
                         {

                         }
                         return _result;
                     }
                 }
                 catch (Exception ex)
                 {
                     gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                 }
                 finally
                 {
                     if (ogloContacts != null) { ogloContacts.Dispose(); ogloContacts = null; }
                     if (oHospital != null) { oHospital.Dispose(); oHospital = null; }
                 }
            return _result;
         
            //}
             
            
            
          
           
       // }
       }

        private gloPMContacts.Hospital AddHospitalContacts()
        {
            try
            {
                gloPMContacts.Hospital oHospital = new gloPMContacts.Hospital();
                oHospital.Name = txtname.Text;
                oHospital.ContactName = txtcontact.Text;
                oHospital.Mobile = mtxtMobile.Text;
                oHospital.Pager = mtxtPager.Text;

                //oHospital.Pager = txtPager.Text;
                //oHospital.CompanyAddress.AddrressLine1 = txtAddressLine1.Text.Trim();
                //oHospital.CompanyAddress.AddrressLine2 = txtAddressLine2.Text.Trim();
                //oHospital.CompanyAddress.City = txtCity.Text.Trim();
                //oHospital.CompanyAddress.State = txtState.Text.Trim();
                //oHospital.CompanyAddress.ZIP = txtZip.Text.Trim();

                //Sandip Darade 20091009 gloAddress control implemented  replacing code for address info above with code below 

                oHospital.CompanyAddress.AddrressLine1 = oAddresscontrol.txtAddress1.Text.Trim();
                oHospital.CompanyAddress.AddrressLine2 = oAddresscontrol.txtAddress2.Text.Trim();
                oHospital.CompanyAddress.City = oAddresscontrol.txtCity.Text.Trim();
                oHospital.CompanyAddress.State = oAddresscontrol.cmbState.Text.Trim();
                oHospital.CompanyAddress.ZIP = oAddresscontrol.txtZip.Text.Trim();

                oHospital.CompanyAddress.Phone = mtxtPhone.Text.Trim();
                oHospital.CompanyAddress.Fax = txtFax.Text.Trim();
                oHospital.CompanyAddress.Email = txtEmail.Text.Trim();
                oHospital.CompanyAddress.URL = txtURL.Text.Trim();
                oHospital.HospitalNPI = txtNPI.Text.Trim();
                if (_IsOtherContact == false)
                {
                    oHospital.ContactType = gloPMContacts.ContactType.Hospital.ToString();
                }
                else
                {
                    oHospital.ContactType = gloPMContacts.ContactType.Others.ToString();
                }
                return oHospital;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }

        }

        private bool ValidateData()
        {
           // frmViewContacts oContact;
            if (_IsOtherContact == false)
            {
                if (txtname.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a hospital name.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtname.Focus();
                    return false;
                }
            //}
            }  //Shubhangi
            else // (oContact.IsForModify = true)
            {
                if (txtname.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a Contact name.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtname.Focus();
                    return false;
                }
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
            if (CheckEmailAddress(txtEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(txtURL.Text))
            {
                if (CheckURL(txtURL.Text) == false)
                {
                    MessageBox.Show("Please enter a valid URL ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtURL.Focus();
                    return false;
                }
            }
            return true;
        }

        private void txtZip_Leave(object sender, EventArgs e)
        {
            if (txtZip.Text.Trim() != "")
            {
                DataTable dt = null;
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string qry = null; 
                try
                {
                    oDb.Connect(false);
                    qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = '" + txtZip.Text.Trim() + "'";

                    txtCity.Text = "";
                    txtState.Text = "";
                    // txtPACountry.Text = "";

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
                    MessageBox.Show(this, ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    dt.Dispose();
                    oDb.Disconnect();
                    oDb.Dispose();
                    qry = null; 
                }
            }
            else
            {
                txtState.Text = "";
                txtCity.Text = "";
            }
        }

        private void frmSetupHospital_Load(object sender, EventArgs e)
        {
            Cls_TabIndexSettings tabSettings = new Cls_TabIndexSettings(this);
            tabSettings.SetTabOrder(Cls_TabIndexSettings.TabScheme.AcrossFirst);

            //Sandip Darade 20091006
            //If fax is not internet fax do no masking  for fax information
            if (_IsInternetFax == false)
            {
                txtFax.MaskType = gloMaskControl.gloMaskType.Other;
            }

            oAddresscontrol = new gloAddress.gloAddressControl(_databaseconnectionstring, true);
            oAddresscontrol.Dock = DockStyle.Fill;
            pnlAddresssControl.Controls.Add(oAddresscontrol);

            txtname.Select();

            if (_ContactID > 0)
            {
                LoadHospital();
            }
            if (tabSettings != null) { tabSettings = null; }
            _IsModified = false; // added on 20100617
        }

        private void txtZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////code to allow nos only 
            //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            //{

            //    e.Handled = true;
            //}
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

        private void AllControlValueChanged_Event(object sender, System.EventArgs e)
        {
            _IsModified = true;
        }

        private void frmSetupHospital_FormClosing(object sender, FormClosingEventArgs e)
        {
            gloPMContacts.gloContacts oglocontact = null;
            string _HospitalName = null;
            try
            {
                if (_IsSaveClicked == true | _IsModified == true)
                {
                    //if (txtname.Text.Trim() == "")
                    //{                       
                    //    this.Close();
                    //}
                    //else
                    //{
                    if (txtname.Text.Trim().Length > 0)
                    {
                        DialogResult _Result;
                        _Result = MessageBox.Show("Do you want to save the changes?", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (_Result == DialogResult.Yes)
                        {
                            //save
                            //ts_btnSave_Click(null, null);
                            //-----------
                            oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
                            bool _result;
                            _HospitalName = txtname.Text.Trim();
                            _result = oglocontact.IsExistsHospital(_HospitalName, _ContactID, _IsOtherContact);
                            if (_result == true)
                            {
                                if (DialogResult.No == (MessageBox.Show("Contact name already exists. Do you want to register it anyway?  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)))
                                {
                                    _IsSaveClicked = false; // added on 20100617

                                    return;
                                }
                            }
                            if (ValidateData() == true)
                            {
                                if (SaveData() == true)
                                {
                                    //e.Cancel = true;
                                    e.Cancel = false;
                                    //this.Close();
                                }
                                _IsSaveClicked = false; // added on 20100617
                            }
                            //-----------


                        }
                        else if (_Result == DialogResult.Cancel)
                        {

                            //e.Cancel = true;
                            e.Cancel = true;
                        }
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oglocontact != null) { oglocontact.Dispose(); oglocontact = null; }
                _HospitalName = null;
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