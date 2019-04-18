using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using gloAuditTrail;

namespace gloAppointmentBook
{
    internal partial class frmSetupResource : Form
    {

        #region " Declarations "

        private string _MessageBoxCaption = string.Empty;

        private string _databaseconnectionstring = "";

        private Int64 _nResourceID = 0;

        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;


        #endregion " Declarations "

        #region  " Property Procedures "

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
        public Int64 nResourceID
        {
            get { return _nResourceID; }
            set { _nResourceID = value; }
        }

        #endregion  " Property Procedures "

        #region " Constructor "

        public frmSetupResource()
        {
            InitializeComponent();
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
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
         

        }

        public frmSetupResource(Int64 ResourceID, string DatabaseConnectionString)
        {
            InitializeComponent();
            _nResourceID = ResourceID;
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
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
         

        }

        #endregion " Constructor "

        #region " Form Load "

        private void frmSetupResource_Load(object sender, EventArgs e)
        {
            //fill the resource type combo
            //FillResourceTypes();
            
            if (cmbResourceType.Items.Count > 0)
            {
                cmbResourceType.SelectedIndex = 0;
                if (cmbResourceType.SelectedItem.ToString() == "General")
                {
                    pnlResource.BringToFront();
                  
                }
            }
            FillUserNames();
            //fill provider specific controls
           // FillProviderTypes();
            cmbResourceType.Enabled = true;
            //chk if the form opened in modify mode
            //_nResourceID = 29;
            if (_nResourceID != 0)
            {
                FillResource(_nResourceID);
                cmbResourceType.Enabled = false;
            }//if  -- opened for modify
        }

        #endregion " Form Load "

        #region " Tool Strip Event "

        //Toolstrip buttons Clicked
        private void tls_SetupResource_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        if (SaveResource())
                        {
                            if (_nResourceID == 0)
                            {
                                ClearControls();
                            }
                            this.Close();
                        }
                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    case "Save":
                        if (SaveResource())
                        {
                           ClearControls();
                           _nResourceID = 0;
                            
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);
                this.Close();
            }
        }//FillResourceDetails

        #endregion " Tool Strip Event "

        #region " Button Click Events "

        private void btnOk_Click(object sender, EventArgs e)
        {

            SaveResource();

            if (_nResourceID == 0)
            {
                ClearControls();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }//catch
        }
        #endregion " Button Click Events "

        #region  " Combo Events "

        private void cmbResourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbResourceType.SelectedIndex != -1)
            {
                #region " Commented Code "
                //if (Convert.ToInt64(((System.Data.DataRowView)(cmbResourceType.SelectedItem)).Row.ItemArray[2]) == (Int64)ResourceType.Provider.GetHashCode())
                //{
                //    this.Height = 664;
                //    this.Width = 712;
                //    tc_Resource.Height = 572;
                //    tc_Resource.Width = 695;

                //    tc_Resource.TabPages.Remove(tp_Provider);
                //    tc_Resource.TabPages.Remove(tp_Equipment);
                //    tc_Resource.TabPages.Remove(tp_Resource);

                //    tc_Resource.TabPages.Add(tp_Provider);

                //}
                //else if (Convert.ToInt64(((System.Data.DataRowView)(cmbResourceType.SelectedItem)).Row.ItemArray[2]) == (Int64)ResourceType.Equipment.GetHashCode())
                //{
                //    //this.Height = 664;
                //    //this.Width = 712;
                //    //tc_Resource.Height = 572;
                //    //tc_Resource.Width = 695;

                //    this.Height = 247;
                //    this.Width = 462;
                //    tc_Resource.Height = 145;
                //    tc_Resource.Width = 440;

                //    tc_Resource.TabPages.Remove(tp_Provider);
                //    tc_Resource.TabPages.Remove(tp_Equipment);
                //    tc_Resource.TabPages.Remove(tp_Resource);

                //    tc_Resource.TabPages.Add(tp_Equipment);
                //}
                //else if (Convert.ToInt64(((System.Data.DataRowView)(cmbResourceType.SelectedItem)).Row.ItemArray[2]) == (Int64)ResourceType.Other.GetHashCode())
                //{
                //    this.Height = 247;
                //    this.Width = 462;
                //    tc_Resource.Height = 145;
                //    tc_Resource.Width = 440;

                //    tc_Resource.TabPages.Remove(tp_Provider);
                //    tc_Resource.TabPages.Remove(tp_Equipment);
                //    tc_Resource.TabPages.Remove(tp_Resource);

                //    tc_Resource.TabPages.Add(tp_Resource);
                //}

                //************************ 
                //if (Convert.ToInt64(((System.Data.DataRowView)(cmbResourceType.SelectedItem)).Row.ItemArray[2]) == (Int64)ResourceType.Provider.GetHashCode())
                //{
                //    //pnlProvider.BringToFront();
                //    //pnlEquipement.SendToBack();                    
                //    //pnlResource.SendToBack();
                //    //panel10.BringToFront();
                //    //panel8.SendToBack();
                //    //panel7.SendToBack();
                //    //this.Height = 622;
                //    //this.Width = 605;
                //    panel8.BringToFront();
                //    panel10.SendToBack();
                //    panel7.SendToBack();
                //    this.Height = 250;
                //    this.Width = 475;
                //}
                //if (Convert.ToInt64(((System.Data.DataRowView)(cmbResourceType.SelectedItem)).Row.ItemArray[2]) == (Int64)ResourceType.Equipment.GetHashCode())
                //{
                //    //pnlEquipement.BringToFront();
                //    //pnlProvider.SendToBack();
                //    //pnlResource.SendToBack();
                //    panel8.BringToFront();
                //    panel10.SendToBack();
                //    panel7.SendToBack();
                //    this.Height = 250;
                //    this.Width = 475;
                //}
                //if (Convert.ToInt64(((System.Data.DataRowView)(cmbResourceType.SelectedItem)).Row.ItemArray[2]) == (Int64)ResourceType.Other.GetHashCode())
                //{
                #endregion " Commented Code "


                if (cmbResourceType.SelectedItem.ToString()=="General")
                {
                    pnlResource.BringToFront();
                }
            }
        }

        #endregion  " Combo Events "

        #region "Provider Related"

            //private void btnClear_Click(object sender, EventArgs e)
            //{
            //    try
            //    {
            //        txtImagePath.Text = "";
            //        if (optBrowse.Checked == true)
            //        {
            //            picSignature.Image = null;
            //        }
            //        else
            //        {
            //            btnCapture.Enabled = true;
            //            //initialization related to sig plus
            //        }//else 
            //    }//try
            //    catch (Exception ex)
            //    {
            //        //MessageBox.Show("Unable to add Provider", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); 
            //    }//catch
            //}

            //private void btnBrowse_Click(object sender, EventArgs e)
            //{
            //    picSignature.SizeMode = PictureBoxSizeMode.StretchImage;

            //    dlgOpenFile.Title = "Select Clinic Logo";
            //    dlgOpenFile.Filter = "Images Files(*.bmp,*.tif,*.jpg,*.jpeg,*.gif)|*.bmp;*.tif;*.jpg;*.jpeg;*.gif";
            //    dlgOpenFile.CheckFileExists = true;
            //    dlgOpenFile.Multiselect = false;
            //    dlgOpenFile.ShowHelp = false;
            //    dlgOpenFile.ShowReadOnly = false;

            //    if (dlgOpenFile.ShowDialog() == DialogResult.OK)
            //    {

            //        picSignature.Visible = true;
            //        picSignature.Image = Image.FromFile(dlgOpenFile.FileName);

            //        txtImagePath.Text = dlgOpenFile.FileName;
            //        btnCapture.Enabled = false;

            //        Image img;
            //        int nWidth;
            //        int nHeight;
            //        img = picSignature.Image;
            //        nHeight = img.Height;
            //        nWidth = img.Width;
            //        if (nWidth > 150)
            //        {
            //            nWidth = 150;
            //        }
            //        if (nHeight > 75)
            //        {
            //            nHeight = 75;
            //        }
            //        img = new Bitmap(img, new Size(nWidth, nHeight));
            //        picSignature.Image = img;
            //        picSignature.SizeMode = PictureBoxSizeMode.CenterImage;
            //    }

            //}

            //private void btnCapture_Click(object sender, EventArgs e)
            //{
            //    //capture image/signature drawn using signature pad


            //}//function

        //private void FillProviderTypes()
        //{
        //    //function to fill the Provider type combo in Provider tab
        //    //  Books.Provider objProvider = new Books.Provider(_databaseconnectionstring);
        //    Books.Resource oResource = new Books.Resource(_databaseconnectionstring);
        //    DataTable dtProviderType = new DataTable();

        //    try
        //    {
        //        dtProviderType = oResource.GetProviderTypes();

        //        if (dtProviderType != null)
        //        {
        //            cmbDoctorType.DataSource = dtProviderType;
        //            cmbDoctorType.DisplayMember = "sProviderType";
        //            cmbDoctorType.ValueMember = "nProviderTypeID";
        //        }//if

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {

        //        if (oResource != null)
        //            oResource.Dispose();

        //    }
        //}

        #endregion
       
        #region "Procedures & Functions"

            private void ClearControls()
            {
                if (cmbResourceType.SelectedItem != null)
                {

                    txtCode.Text = "";
                    txtDescription.Text = "";

                    #region " Commented Code "
                    //if (Convert.ToInt32(cmbResourceType.SelectedValue.ToString()) == ResourceType.Provider.GetHashCode())
                    //{
                    //    txtPrefix.Text = "";
                    //    txtFirstName.Text = "";
                    //    txtMiddleName.Text = "";
                    //    txtLastName.Text = "";
                    //    txtAddress.Text = "";
                    //    txtAddress2.Text = "";
                    //    txtCity.Text = "";
                    //    txtState.Text = "";
                    //    txtZIP.Text = "";
                    //    txtUserName.Text = "";
                    //    txtPassword.Text = "";
                    //    txtConfirmPassword.Text = "";
                    //    txtEmailAddress.Text = "";
                    //    txtURL.Text = "";
                    //    txtPager.Text = "";
                    //    mskPhoneNo.Text = "";
                    //    mskMobileNo.Text = "";
                    //    txtFAX.Text = "";
                    //    txtDEA.Text = "";
                    //    txtNPI.Text = "";
                    //    txtUPIN.Text = "";
                    //    txtStateMedicalLicenseNo.Text = "";
                    //    optMale.Checked = true;

                    //    picSignature = null;
                    //    optBrowse.Checked = true;
                    //    txtImagePath.Text = "";

                    //    txtPrefix.Select();
                    //}
                    ////else if (Convert.ToInt32(cmbResourceType.SelectedValue.ToString()) == ResourceType.Equipment.GetHashCode())
                    //if (Convert.ToInt32(cmbResourceType.SelectedValue.ToString()) == ResourceType.Equipment.GetHashCode())
                    //{
                    //    txtEQCode.Text = "";
                    //    txtEquipment.Text = "";
                    //}
                    //else if (Convert.ToInt32(cmbResourceType.SelectedValue.ToString()) == ResourceType.Other.GetHashCode())
                    //{

                    //}
                    #endregion " Commented Code "
                }
            }

            private bool SaveResource()
            {
                Books.Resource oResource = null;
                try
                {
                    if (cmbResourceType.Items.Count > 0)
                    {
                        if (cmbResourceType.SelectedItem != null)
                        {
                            oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);


                            #region " Code For Provider As Resource is Commented "
                            //if (Convert.ToInt32(cmbResourceType.SelectedValue.ToString()) == ResourceType.Provider.GetHashCode())
                            //{

                            //    //Prefix  and First Name should be compulsorily entered.
                            //    if (txtPrefix.Text.Trim() == "")
                            //    {
                            //        MessageBox.Show("Please enter Prefix", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //        txtPrefix.Focus();
                            //        return false; // TODO: might not be correct. Was : Exit Sub 
                            //    }

                            //    if (txtFirstName.Text.Trim() == "")
                            //    {
                            //        MessageBox.Show("Please enter First Name", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //        txtFirstName.Focus();
                            //        return false;
                            //    }

                            //    //validate the phone no. and mobile no.
                            //    if (mskPhoneNo.Text.Trim().Length > 0 & mskPhoneNo.Text.Trim().Length < 10)
                            //    {
                            //        MessageBox.Show("Phone Details Incomplete", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //        mskPhoneNo.Focus();
                            //        return false; // TODO: might not be correct. Was : Exit Sub 
                            //    }

                            //    if (mskMobileNo.Text.Trim().Length > 0 & mskMobileNo.Text.Trim().Length < 10)
                            //    {
                            //        MessageBox.Show("Mobile No. Details Incomplete", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //        mskMobileNo.Focus();
                            //        return false; // TODO: might not be correct. Was : Exit Sub 
                            //    }

                            //    //--------------------------------------------------------------
                            //    // Validate user details

                            //    //User Name must be entered.
                            //    if (txtUserName.Text.Trim() == "")
                            //    {
                            //        MessageBox.Show("Please enter User Name", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //        txtUserName.Focus();
                            //        return false; // TODO: might not be correct. Was : Exit Sub 
                            //    }//if

                            //    //Password and Confirm Password must be entered.
                            //    if (txtPassword.Text.Trim() == "")
                            //    {
                            //        MessageBox.Show("Please enter Password", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //        txtPassword.Focus();
                            //        return false; // TODO: might not be correct. Was : Exit Sub 
                            //    }//if
                            //    if (txtConfirmPassword.Text.Trim() == "")
                            //    {
                            //        MessageBox.Show("Please enter Confirm Password", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //        txtConfirmPassword.Focus();
                            //        return false; // TODO: might not be correct. Was : Exit Sub 
                            //    }//if

                            //    //Check whether password and confirm password is same or not

                            //    if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                            //    {
                            //        MessageBox.Show("Password and Confirm Password must be same", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //        txtConfirmPassword.Focus();
                            //        return false; // TODO: might not be correct. Was : Exit Sub 
                            //    }//if


                            //    //----------------------------------------------------------------
                            //    // check whether the username is already present or not.
                            //    //oResource.gloUser ogloUser = new gloSecurity.gloUser(_databaseconnectionstring);
                            //    // if (_nResourceID == 0)
                            //    //if (ogloUser.CheckUserExists(txtUserName.Text))
                            //    if (oResource.CheckProviderExistsAsUser(txtUserName.Text.Trim(), _nResourceID))
                            //    {
                            //        MessageBox.Show(this, "User is already exist with this UserName. Two users cannot have same Name.", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //        txtUserName.Focus();
                            //        return false;
                            //    }
                            //    //{
                            //    //    String strQuery = "SELECT Count(*) FROM User_MST WHERE sLoginName = '" + loginName + "'";
                            //    //    count = (Int32)oDB.ExecuteScalar_Query(strQuery);
                            //    //    if (count > 0)
                            //    //    {
                            //    //        //Login name exists
                            //    //        return true;
                            //    //    }
                            //    //    else
                            //    //    {
                            //    //        //Login name does not exists
                            //    //        return false;
                            //    //    }
                            //    //}
                            //    //-----------------------------------------------------------------

                            //    oResource.Code = txtCode.Text.Trim();
                            //    oResource.Description = txtDescription.Text.Trim();


                            //    //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                            //    //oResource.ClinicID = 1;
                            //    oResource.ClinicID = ClinicID;
                            //    //


                            //    DataRowView drv;
                            //    drv = (DataRowView)cmbResourceType.SelectedItem;

                            //    oResource.ResourceTypeID = Convert.ToInt64(drv["nResourceTypeID"]);
                            //    oResource.IsBlocked = false;
                            //    oResource.ResourceID = _nResourceID;
                            //    oResource.ResourceType = ResourceType.Provider;
                            //    oResource.ResourceTypeDescription = cmbResourceType.SelectedText.ToString();

                            //    //assign values to the Provider object for add/modify
                            //    oResource.ProviderDetail.Prefix = txtPrefix.Text.Trim();
                            //    oResource.ProviderDetail.FirstName = txtFirstName.Text.Trim();
                            //    oResource.ProviderDetail.MiddleName = txtMiddleName.Text.Trim();
                            //    oResource.ProviderDetail.LastName = txtLastName.Text.Trim();
                            //    oResource.ProviderDetail.Address = txtAddress.Text.Trim();
                            //    oResource.ProviderDetail.Street = txtAddress2.Text.Trim();
                            //    oResource.ProviderDetail.State = txtState.Text.Trim();
                            //    oResource.ProviderDetail.City = txtCity.Text.Trim();
                            //    oResource.ProviderDetail.ZIP = txtZIP.Text.Trim();
                            //    oResource.ProviderDetail.Phone = mskPhoneNo.Text.Trim();
                            //    oResource.ProviderDetail.Mobile = mskMobileNo.Text.Trim();
                            //    oResource.ProviderDetail.Pager = txtPager.Text.Trim();
                            //    oResource.ProviderDetail.FAX = txtFAX.Text.Trim();
                            //    oResource.ProviderDetail.Email = txtEmailAddress.Text.Trim();
                            //    oResource.ProviderDetail.URL = txtURL.Text.Trim();

                            //    if (optMale.Checked == true)
                            //    {
                            //        oResource.ProviderDetail.Gender = "Male";
                            //    }
                            //    else
                            //    {
                            //        oResource.ProviderDetail.Gender = "Female";
                            //    }

                            //    oResource.ProviderDetail.DEA = txtDEA.Text.Trim();

                            //    //get the provider type id 
                            //    //Providerid= getprovidertypeid(cmbDoctorType.Text )

                            //    oResource.ProviderDetail.ProviderTypeID = 0;

                            //    oResource.ProviderDetail.NPI = txtNPI.Text.Trim();
                            //    oResource.ProviderDetail.UPIN = txtUPIN.Text.Trim();
                            //    oResource.ProviderDetail.StateMedicalNo = txtStateMedicalLicenseNo.Text.Trim();
                            //    oResource.ProviderDetail.UserName = txtUserName.Text.Trim();
                            //    oResource.ProviderDetail.Password = txtPassword.Text.Trim();

                            //    //get the Provider signature
                            //    //if (optBrowse.Checked == false)
                            //    //{
                            //    //    picSignature.Image = null;
                            //    //    if (File.Exists(Application.StartupPath + "\\DoctorSign.tif") == true)
                            //    //    {
                            //    //        picSignature.Image = Image.FromFile(Application.StartupPath + "\\DoctorSign.tif");
                            //    //        picSignature.SizeMode = PictureBoxSizeMode.CenterImage;
                            //    //    }
                            //    //}
                            //    //else
                            //    //{
                            //    //    picSignature.Image = null;
                            //    //    if (File.Exists(txtImagePath.Text.Trim().ToString()) == true)
                            //    //    {
                            //    //        picSignature.Image = Image.FromFile(txtImagePath.Text.Trim().ToString());
                            //    //        picSignature.SizeMode = PictureBoxSizeMode.CenterImage;
                            //    //    }
                            //    //}//else

                            //    if (picSignature.Image != null)
                            //    {
                            //        oResource.ProviderDetail.Signature = picSignature.Image;
                            //    }
                            //    else
                            //    {
                            //        oResource.ProviderDetail.Signature = null;
                            //    }

                            //    oResource.ProviderDetail.ResourceTypeID = Convert.ToInt64(drv["nResourceTypeID"]);

                            //    //check whether form opened in add mode or modify mode
                            //    oResource.ProviderDetail.ProviderTypeID = Convert.ToInt64(cmbDoctorType.SelectedValue);
                            //    if (_nResourceID == 0)
                            //    {
                            //        if (oResource.Add() > 0)
                            //        {
                            //            //MessageBox.Show("Provider added successfully", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //            //clear the controls on the form when the provider is successfully added
                            //            ClearControls();
                            //            return true;
                            //        }
                            //        else
                            //        {
                            //            MessageBox.Show("Unable to add Provider", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //            return false;
                            //        }
                            //    }
                            //    else
                            //    {

                            //        if (oResource.Modify(_nResourceID) == true)
                            //        {
                            //            //MessageBox.Show("Provider modified successfully", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //            return true;
                            //        }//if
                            //        else
                            //        {
                            //            MessageBox.Show("Unable to modify Provider", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //            return false;
                            //        }

                            //    }
                            //}
                            // else if (Convert.ToInt32(cmbResourceType.SelectedValue.ToString()) == ResourceType.Equipment.GetHashCode())
                            #endregion " Code For Provider As Resource is Commented "


                            if (cmbResourceType.SelectedItem.ToString() == "General")
                            {
                                // add/update this record in AB_Resource_MST

                                //description compulsory

                                //check description entered or not
                                if (txtDescription.Text.Trim() == "")
                                {
                                    MessageBox.Show("Please enter a description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtDescription.Focus();
                                    return false; // TODO: might not be correct. Was : Exit Sub 
                                }//if

                                //chk code already exists

                                if (oResource.IsExists(_nResourceID, txtDescription.Text.Trim()))
                                {
                                    MessageBox.Show("Resource description is already in use by another entry. Select a unique resource description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtDescription.Focus();
                                    return false; // TODO: might not be correct. Was : Exit Sub 
                                }//if

                                oResource.Code = txtCode.Text.Trim();
                                oResource.Description = txtDescription.Text.Trim();
                                if (cmbUsers.Text != "")
                                {
                                    oResource.UserName = cmbUsers.Text;
                                }
                                //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                                //oResource.ClinicID = 1;
                                oResource.ClinicID = ClinicID;
                                //

                                //DataRowView drv;
                                //drv = (DataRowView)cmbResourceType.SelectedItem;

                                oResource.ResourceTypeID = Convert.ToInt64(1);
                                oResource.IsBlocked = false;
                                oResource.ResourceID = _nResourceID;
                                oResource.ResourceType = ResourceType.General;
                                oResource.ResourceTypeDescription = cmbResourceType.SelectedItem.ToString();

                                oResource.IsTurnOffReminder = chkTurnOffReminders.Checked;

                                if (_nResourceID == 0)
                                {
                                    _nResourceID = oResource.Add();
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Resource, ActivityType.Add, "Add Resource", 0, _nResourceID, 0, ActivityOutCome.Success);

                                    if (_nResourceID > 0)
                                    {
                                        //MessageBox.Show("Equipment added successfully", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        ClearControls();
                                        return true;
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Resource, ActivityType.Add, "Add Resource", 0, _nResourceID, 0, ActivityOutCome.Failure);
                                        MessageBox.Show("Unable to add resource.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return false;
                                    }
                                }
                                else
                                {

                                    if (oResource.Modify(_nResourceID) == true)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Resource, ActivityType.Add, "Add Resource", 0, _nResourceID, 0, ActivityOutCome.Success);

                                        //MessageBox.Show("Equipment modified successfully", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return true;
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Resource, ActivityType.Add, "Add Resource", 0, _nResourceID, 0, ActivityOutCome.Failure);

                                        MessageBox.Show("Unable to modify resource.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return false;
                                    }
                                }
                            }
                            //        else if (Convert.ToInt32(cmbResourceType.SelectedValue.ToString()) == ResourceType.Other.GetHashCode())
                            //        {
                            //            // add/update this record in AB_Resource_MST

                            //            //description compulsory

                            //            //check description entered or not
                            //            if (txtDescription.Text.Trim() == "")
                            //            {
                            //                MessageBox.Show("Please enter Resource Description", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //                txtDescription.Focus();
                            //                return false; // TODO: might not be correct. Was : Exit Sub 
                            //            }//if

                            //            //chk code already exists

                            //            if (oResource.IsExists(_nResourceID, txtDescription.Text.Trim()))
                            //            {
                            //                MessageBox.Show("This Resource Description already exists. Please enter different Resource Description", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //                txtDescription.Focus();
                            //                return false; // TODO: might not be correct. Was : Exit Sub 
                            //            }//if


                            //            oResource.Code = txtCode.Text.Trim();
                            //            oResource.Description = txtDescription.Text.Trim();

                            //            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
                            //            //oResource.ClinicID = 1;
                            //            oResource.ClinicID = ClinicID;
                            //            //

                            //            DataRowView drv;
                            //            drv = (DataRowView)cmbResourceType.SelectedItem;

                            //            oResource.ResourceTypeID = Convert.ToInt64(drv["nResourceTypeID"]);
                            //            oResource.IsBlocked = false;
                            //            oResource.ResourceID = _nResourceID;
                            //            oResource.ResourceType = ResourceType.Other;
                            //            oResource.ResourceTypeDescription = cmbResourceType.SelectedText.ToString();

                            //            if (_nResourceID == 0)
                            //            {
                            //                if (oResource.Add() > 0)
                            //                {
                            //                    //MessageBox.Show("Resource added successfully", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //                    ClearControls();
                            //                    return true;
                            //                }
                            //                else
                            //                {
                            //                    MessageBox.Show("Unable to add Resource", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //                    return false;
                            //                }
                            //            }
                            //            else
                            //            {
                            //                if (oResource.Modify(_nResourceID) == true)
                            //                {
                            //                    //MessageBox.Show("Resource modified successfully", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //                    return true;
                            //                }
                            //                else
                            //                {
                            //                    MessageBox.Show("Unable to modify Resource", _MessageboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //                    return false;
                            //                }
                            //            }
                            //        }
                            //        oResource.Dispose();
                            //    }
                            //}
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return false;
                }
                finally
                {
                    if (oResource != null) { oResource.Dispose(); oResource = null; }
                }
            }

            private void FillResource(Int64 resourceid)
            {
                try
                {
                    Books.Resource oResource = new global::gloAppointmentBook.Books.Resource(_databaseconnectionstring);
                    oResource.GetResource(resourceid);

                    if (cmbResourceType.Items.Count > 0)
                    {
                        cmbResourceType.Text = oResource.ResourceTypeDescription;
                        for (int i = 0; i <= cmbResourceType.Items.Count - 1; i++)
                        {
                            if (cmbResourceType.Items[i].ToString() == cmbResourceType.Text)
                            {
                                cmbResourceType.SelectedIndex = i;
                                cmbResourceType.Enabled = false;
                                break;
                            }
                        }

                        #region " Commented Code for Provider "
                        //if (oResource.ResourceType == ResourceType.Provider)
                        //{
                        //    //Fill Provider
                        //    txtPrefix.Text = oResource.ProviderDetail.Prefix;
                        //    txtFirstName.Text = oResource.ProviderDetail.FirstName;
                        //    txtMiddleName.Text = oResource.ProviderDetail.MiddleName;
                        //    txtLastName.Text = oResource.ProviderDetail.LastName;

                        //    txtAddress.Text = oResource.ProviderDetail.Address;
                        //    txtAddress2.Text = oResource.ProviderDetail.Street;
                        //    txtState.Text = oResource.ProviderDetail.State;
                        //    txtCity.Text = oResource.ProviderDetail.City;
                        //    txtZIP.Text = oResource.ProviderDetail.ZIP;
                        //    txtCity.Text = oResource.ProviderDetail.City;
                        //    txtDEA.Text = oResource.ProviderDetail.DEA;
                        //    txtEmailAddress.Text = oResource.ProviderDetail.Email;
                        //    mskPhoneNo.Text = oResource.ProviderDetail.Phone;
                        //    mskMobileNo.Text = oResource.ProviderDetail.Mobile;

                        //    if (oResource.ProviderDetail.Gender == "Male")
                        //    {
                        //        optMale.Checked = true;
                        //    }//if
                        //    else if (oResource.ProviderDetail.Gender == "Female")
                        //    {
                        //        optFemale.Checked = true;
                        //    }//else

                        //    txtFAX.Text = oResource.ProviderDetail.FAX;
                        //    txtPager.Text = oResource.ProviderDetail.Pager;
                        //    txtUPIN.Text = oResource.ProviderDetail.UPIN;
                        //    txtURL.Text = oResource.ProviderDetail.URL;
                        //    txtNPI.Text = oResource.ProviderDetail.NPI;
                        //    txtStateMedicalLicenseNo.Text = oResource.ProviderDetail.StateMedicalNo;
                        //    //Temp code added to make User_MST Entry
                        //    txtUserName.Text = oResource.ProviderDetail.UserName;
                        //    txtPassword.Text = oResource.ProviderDetail.Password;
                        //    txtConfirmPassword.Text = oResource.ProviderDetail.Password;

                        //    //Temp code added to make User_MST Entry

                        //    cmbDoctorType.SelectedValue = oResource.ProviderDetail.ProviderTypeID;

                        //    if (oResource.ProviderDetail.Signature != null)
                        //    {
                        //        picSignature.Image = oResource.ProviderDetail.Signature;
                        //        picSignature.SizeMode = PictureBoxSizeMode.CenterImage;
                        //    }//if
                        //}
                        //else if (oResource.ResourceType == ResourceType.Equipment)
                        #endregion " Commented Code for Provider "


                        if (oResource.ResourceType == ResourceType.General)
                        {
                            //Fill Equipment
                            txtCode.Text = oResource.Code;
                            txtDescription.Text = oResource.Description;
                            cmbUsers.Text = oResource.UserName;
                            chkTurnOffReminders.Checked = oResource.IsTurnOffReminder;
                        }
                        //else if (oResource.ResourceType == ResourceType.Other)
                        //{
                        //    txtCode.Text = oResource.Code;
                        //    txtDescription.Text = oResource.Description;
                        //}

                    }
                    oResource.Dispose();
                }
                catch (Exception ex)    
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
            }

            private void FillResourceTypes()
            {

                //DataTable dtResourceType = new DataTable();
                //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

                //try
                //{
                //    //oDB.Connect(false);
                //    ////string _sqlQuery = "SELECT * FROM AB_ResourceType_MST WHERE bitIsBlocked ='" + false + "'";
                //    //string _sqlQuery = "SELECT * FROM AB_ResourceType_MST WHERE bitIsBlocked ='" + false + "' AND nClinicID = "+this.ClinicID+" ";
                //    ////
                //    //oDB.Retrive_Query(_sqlQuery, out dtResourceType);

                //    ////bind this datatable to the Resource Type combo
                //    //cmbResourceType.DataSource = dtResourceType;
                //    //cmbResourceType.DisplayMember = "sResourceTypeDescription";
                //    //cmbResourceType.ValueMember = "nResourceType";
                   
                //}
                //catch (gloDatabaseLayer.DBException DBErr)
                //{
                //}
                //catch (Exception ex)
                //{
                //}
                //finally
                //{
                //    oDB.Disconnect();
                //    oDB.Dispose();
                //}

            }//FillResourceTypes()

            private void FillResourceControls(Books.Provider oProvider, Int64 nResourceTypeID)
            {
                try
                {
                    if (nResourceTypeID == Convert.ToInt64(ResourceType.General))
                    {

                        txtCode.Text = oProvider.Code;
                        txtDescription.Text = oProvider.Description;

                    }//if
                    else
                    {
                        //resource type = other

                        txtCode.Text = oProvider.Code;
                        txtDescription.Text = oProvider.Description;
                    }//else
                }//try
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
            }

            private void FillUserNames()
            {
                DataTable dtUsers = null;
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

                try
                {
                    oDB.Connect(false);
                    //string _sqlQuery = "SELECT nUserID,  isnull(sFirstName,'')+' '+ isnull(sMiddleName,'') +' '+ isnull(sLastName,'') as UserName FROM User_MST Where nClinicID = " + this.ClinicID + " ";
                    string _sqlQuery = "SELECT nUserID,sLoginName as UserName FROM User_MST Where nClinicID = " + this.ClinicID + " ";
                    oDB.Retrive_Query(_sqlQuery, out dtUsers);

                    //bind this datatable to the Resource Type combo
                    cmbUsers.DataSource = dtUsers;
                    cmbUsers.DisplayMember = "UserName";
                    cmbUsers.ValueMember = "nUserID";

                    _sqlQuery = null;
                }
                catch (gloDatabaseLayer.DBException DBErr)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), true);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }

        private void tlsp_btnOK_Click(object sender, EventArgs e)
            {

            }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "~")
            {
                e.Handled = true;
            }
        }

        //FillUsers()

        #endregion
 
    }//class
}//namespace gloAppointmentBook
