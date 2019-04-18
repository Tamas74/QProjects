using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloAppointmentBook.Books ;
using Janus.Windows.Schedule;
using Janus.Windows.Common;
using System.IO;
using gloSecurity;

namespace gloAppointmentBook
{
    public partial class frmSetupUser : Form
    {
        #region Declarations

        private string _databseConnectionString = "";
        private string _MessageBoxCaption = string.Empty;
        private const string _encryptionKey = "12345678";
        private Int64 _userId = 0;
        private User oUser;
        private Rights oRights;
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationSettings.AppSettings;


        public Int64 UserID
        {
            get { return _userId;}
            set { _userId = value; }
        }
        #endregion

        #region Constructor

        public frmSetupUser(string databseConnectionString)
        {
            _databseConnectionString = databseConnectionString;
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
         
        }

        public frmSetupUser(Int64 UserId, string databseConnectionString)
        {
            _databseConnectionString = databseConnectionString;

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
         
            _userId = UserId;
            InitializeComponent();
        }

        #endregion

        private void frmSetupUser_Load(object sender, EventArgs e)
        {
            pnlUserDetails.Visible = true;
            pnlUserDetails.BringToFront();
            fillProviders();
            DesignUserRightGrid();
            
            if (_userId != 0)
            {
                gloUser ogloUser = new gloUser(_databseConnectionString);
                DataTable dtUser = ogloUser.GetUser(_userId);
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    fillUser(dtUser);
                    btnBlock.Visible = true;
                }
                dtUser.Dispose();
                ogloUser.Dispose();
                fillUserRights();
            }
            
        }

        private void fillUserRights()
        {

            try
            {
                gloRights ogloRights = new gloRights(_databseConnectionString);
                DataTable dtModules = ogloRights.GetModuleNames();

                for (int i = 0; i < dtModules.Rows.Count; i++)
                {
                    DataTable dtUserRights = ogloRights.GetUserRights(_userId, Convert.ToInt32(dtModules.Rows[i]["nModuleID"]));
                    
                    Modules oModule = (Modules)Convert.ToInt32(dtModules.Rows[i]["nModuleID"]);                    
                    int parentRowIndex = c1UserRights.FindRow(oModule.ToString(), 1, 0, false);
                                      
                    for (int k = 0; k < dtUserRights.Rows.Count; k++)
                    {
                        c1UserRights.SetData(parentRowIndex + Convert.ToInt32(dtUserRights.Rows[k]["nActivityID"]), 1, true);                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        
        private void fillUser(DataTable dtUser)
        {
            try
            {
                ClsEncryption oDecrypt = new ClsEncryption();

                txtLoginName.Text = Convert.ToString(dtUser.Rows[0]["sLoginName"]);
                txtPassword.Text = oDecrypt.DecryptFromBase64String(Convert.ToString(dtUser.Rows[0]["sPassword"]), _encryptionKey);
                txtConfirmPassword.Text = txtPassword.Text;
                txtNickName.Text = Convert.ToString(dtUser.Rows[0]["sNickName"]);

                if (System.DBNull.Value != dtUser.Rows[0]["nAdministrator"])
                    chkgloPMSAdmin.Checked = Convert.ToBoolean((dtUser.Rows[0]["nAdministrator"]));
                else
                    chkgloPMSAdmin.Checked = false;

                if (System.DBNull.Value != dtUser.Rows[0]["IsAuditTrail"])
                    chkAuditTrails.Checked = Convert.ToBoolean(dtUser.Rows[0]["IsAuditTrail"]);
                else
                    chkAuditTrails.Checked = false;


                if (System.DBNull.Value != dtUser.Rows[0]["nAccessDenied"])
                    chkAccessDenied.Checked = Convert.ToBoolean(dtUser.Rows[0]["nAccessDenied"]);
                else
                    chkAccessDenied.Checked = false;


                if (System.DBNull.Value != dtUser.Rows[0]["bCoSign"])
                    chkCoSign.Checked = Convert.ToBoolean(dtUser.Rows[0]["bCoSign"]);
                else
                    chkCoSign.Checked = false;

                if (System.DBNull.Value != dtUser.Rows[0]["IsPasswordSettings"])
                    chkApplyPwdSettings.Checked = Convert.ToBoolean(dtUser.Rows[0]["IsPasswordSettings"]);
                else
                    chkCoSign.Checked = false;

                cmbProvider.SelectedValue = Convert.ToInt64(dtUser.Rows[0]["nProviderID"]);
                txtFirstName.Text = Convert.ToString(dtUser.Rows[0]["sFirstName"]);
                txtMidleName.Text = Convert.ToString(dtUser.Rows[0]["sMiddleName"]);
                txtLastName.Text = Convert.ToString(dtUser.Rows[0]["sLastName"]);
                txtAddress1.Text = Convert.ToString(dtUser.Rows[0]["sAddress"]);
                txtAddress2.Text = Convert.ToString(dtUser.Rows[0]["sAddress2"]);
                //txtStreet.Text = Convert.ToString(dtUser.Rows[0]["sStreet"]);
                txtCity.Text = Convert.ToString(dtUser.Rows[0]["sCity"]);
                txtState.Text = Convert.ToString(dtUser.Rows[0]["sState"]);
                txtZip.Text = Convert.ToString(dtUser.Rows[0]["sZIP"]);
                txtPhoneNo.Text = Convert.ToString(dtUser.Rows[0]["sPhoneNo"]);
                txtMobileNo.Text = Convert.ToString(dtUser.Rows[0]["sMobileNo"]);
                txtFax.Text = Convert.ToString(dtUser.Rows[0]["sFAX"]);
                txtEmail.Text = Convert.ToString(dtUser.Rows[0]["sEmail"]);
                txtSSN.Text = Convert.ToString(dtUser.Rows[0]["sSSNNo"]);
                cmbGender.SelectedIndex = cmbGender.FindStringExact(Convert.ToString(dtUser.Rows[0]["sGender"]));
                cmbMaritualStatus.SelectedIndex = cmbMaritualStatus.FindStringExact(Convert.ToString(dtUser.Rows[0]["sMaritalStatus"]));

                if (System.DBNull.Value != dtUser.Rows[0]["dtDOB"])
                    dtDOB.Value = Convert.ToDateTime(dtUser.Rows[0]["dtDOB"]);
                else
                    dtDOB.Text = "";

                //Display Block/Unblock option according to current status
                if (System.DBNull.Value != dtUser.Rows[0]["nBlockStatus"])
                {
                    if (Convert.ToBoolean(dtUser.Rows[0]["nBlockStatus"]) == true)
                        btnBlock.Text = "Unblock";
                    else
                        btnBlock.Text = "Block";
                }
                else
                {
                    btnBlock.Text = "Block";
                }

                //Display Signature image
                if (System.DBNull.Value != dtUser.Rows[0]["imgSignature"])
                {
                    Byte[] arrImage = (Byte[])((dtUser.Rows[0]["imgSignature"]));
                    MemoryStream ms = new MemoryStream(arrImage);
                    picSignature.Image = Image.FromStream(ms);
                    picSignature.SizeMode = PictureBoxSizeMode.CenterImage;
                }

                chkExchnageUser.Checked = Convert.ToBoolean(dtUser.Rows[0]["IsExchangeUser"]);
                txtExchangeLogin.Text = Convert.ToString(dtUser.Rows[0]["sExchangeLogin"]);
                txtExchangePwd.Text = Convert.ToString(dtUser.Rows[0]["sExchangePassword"]);
                txtExchangePwdConfirm.Text = Convert.ToString(dtUser.Rows[0]["sExchangePassword"]);

            }
            catch (Exception ex)
            {
                ex.ToString();
                ex = null;
            }

        }

        private void fillProviders()
        {
            try
            {
                gloUser ogloUser = new gloUser(_databseConnectionString);
                DataTable dtProviders = ogloUser.GetAllProviders();
                if (dtProviders != null && dtProviders.Rows.Count > 0)
                {
                    cmbProvider.DataSource = dtProviders;
                    cmbProvider.ValueMember = dtProviders.Columns[0].ColumnName;
                    cmbProvider.DisplayMember = dtProviders.Columns[1].ColumnName;
                    cmbProvider.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                ex = null;
            }
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                gloUser ogloUser = new gloUser(_databseConnectionString);
                gloRights ogloRights = new gloRights (_databseConnectionString);
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        // SetUseRights();
                        if (ValidateData())
                        {
                            oUser = SetUserData();
                            oRights = SetUseRights();
                            _userId = ogloUser.Add(oUser);
                            if (_userId != 0)
                            {
                                oRights = SetUseRights();
                                oRights.UserId = _userId;

                                if (ogloRights.Add(oRights))
                                    this.Close();
                                else
                                    MessageBox.Show(" Can not Add/modify User Rights ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                MessageBox.Show(" Can not Add/modify User ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            oUser = null;
                        }
                        break;


                    case "Cancel":
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);               
            }
        }

        private Rights SetUseRights()
        {
            Rights ointernelRights = new Rights();
            for (int i = 1; i < c1UserRights.Rows.Count; i++)
            {
                if (Convert.ToBoolean(c1UserRights.GetData(i, 1)) == true) //if Activity  is Selected
                {
                    Node oChildNode = c1UserRights.Rows[i].Node;
                    if (oChildNode.Children == 0)                          //if Child Node
                    {
                        Node oParentNode = oChildNode.GetNode(NodeTypeEnum.Parent);

                        int ModuleId = Convert.ToInt32(oParentNode.Key);
                        int ActivityId = Convert.ToInt32(oChildNode.Key);
                        switch ((Modules)(ModuleId))
                        {
                            case Modules.Appointment:         //Appointments 
                                #region Set Appointmet Rights
                                switch ((Activities)ActivityId)
                                {
                                    case Activities.Add:
                                        ointernelRights.Appointment.Add = true;
                                        break;
                                    case Activities.Modify:
                                        ointernelRights.Appointment.Modify = true;
                                        break;
                                    case Activities.Delete:
                                        ointernelRights.Appointment.Delete = true;
                                        break;
                                    case Activities.View:
                                        ointernelRights.Appointment.View = true;
                                        break;
                                    case Activities.SendExchange:
                                        ointernelRights.Appointment.SendToExchange = true;
                                        break;
                                    case Activities.RetriveExchange:
                                        ointernelRights.Appointment.RetriveFromExchange = true;
                                        break;
                                } 
                                #endregion
                                break;
                            case Modules.Schedule:            //Schedule
                                #region Set Schedule Rights
                                switch ((Activities)ActivityId)
                                {
                                    case Activities.Add:
                                        ointernelRights.Schedule.Add = true;
                                        break;
                                    case Activities.Modify:
                                        ointernelRights.Schedule.Modify = true;
                                        break;
                                    case Activities.Delete:
                                        ointernelRights.Schedule.Delete = true;
                                        break;
                                    case Activities.View:
                                        ointernelRights.Schedule.View = true;
                                        break;
                                } 
                                #endregion
                                break;
                            case Modules.Billing:             //Billing  
                                #region Set billing Rights
                                switch ((Activities)ActivityId)
                                {
                                    case Activities.Add:
                                        ointernelRights.Billing.Add = true;
                                        break;
                                    case Activities.Modify:
                                        ointernelRights.Billing.Modify = true;
                                        break;
                                    case Activities.Delete:
                                        ointernelRights.Billing.Delete = true;
                                        break;
                                    case Activities.View:
                                        ointernelRights.Billing.View = true;
                                        break;
                                } 
                                #endregion
                                break;
                            case Modules.AppointmentBook:     //Appointment Book
                                #region Set AppointmentBook Rights
                                switch ((Activities)ActivityId)
                                {
                                    case Activities.Add:
                                        ointernelRights.AppointmentBook.Add = true;
                                        break;
                                    case Activities.Modify:
                                        ointernelRights.AppointmentBook.Modify = true;
                                        break;
                                    case Activities.Delete:
                                        ointernelRights.AppointmentBook.Delete = true;
                                        break;
                                    case Activities.View:
                                        ointernelRights.AppointmentBook.View = true;
                                        break;
                                } 
                                #endregion
                                break;
                            case Modules.BillingBook:         //Billing Book    
                                #region Set BillingBook Rights
                                switch ((Activities)ActivityId)
                                {
                                    case Activities.Add:
                                        ointernelRights.BillingBook.Add = true;
                                        break;
                                    case Activities.Modify:
                                        ointernelRights.BillingBook.Modify = true;
                                        break;
                                    case Activities.Delete:
                                        ointernelRights.BillingBook.Delete = true;
                                        break;
                                    case Activities.View:
                                        ointernelRights.BillingBook.View = true;
                                        break;
                                } 
                                #endregion
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return ointernelRights;
        }
       
        private User SetUserData()
        {
            User ointernelUser = new User();
            ClsEncryption oEncrypt = new ClsEncryption();

            ointernelUser.UserID = _userId;
            ointernelUser.UserName = txtLoginName.Text.Trim();
            ointernelUser.Password = oEncrypt.EncryptToBase64String(txtPassword.Text.Trim(), _encryptionKey);
            ointernelUser.NickName = txtNickName.Text.Trim();
            ointernelUser.IsAdministrator = chkgloPMSAdmin.Checked;
            ointernelUser.IsAuditTrail = chkAuditTrails.Checked;
            ointernelUser.IsAccessDenied = chkAccessDenied.Checked;
            ointernelUser.IsCoSign = chkCoSign.Checked;
            ointernelUser.IsPasswordSettings = chkApplyPwdSettings.Checked;
            ointernelUser.ProviderID = Convert.ToInt64(cmbProvider.SelectedValue);
            ointernelUser.FirstName = txtFirstName.Text.Trim();
            ointernelUser.MiddleName = txtMidleName.Text.Trim();
            ointernelUser.LastName = txtLastName.Text.Trim();
            ointernelUser.Address1 = txtAddress1.Text.Trim();
            ointernelUser.Address2 = txtAddress2.Text.Trim();
            //ointernelUser.Street = txtStreet.Text.Trim();
            ointernelUser.City = txtCity.Text.Trim();
            ointernelUser.State = txtState.Text.Trim();
            ointernelUser.ZIP = txtZip.Text.Trim();
            ointernelUser.PhoneNo = txtPhoneNo.Text.Trim();
            ointernelUser.MobileNo = txtMobileNo.Text.Trim();
            ointernelUser.FAX = txtFax.Text.Trim();
            ointernelUser.Email = txtEmail.Text.Trim();
            ointernelUser.SSNno = txtSSN.Text.Trim();
            ointernelUser.Gender = Convert.ToString(cmbGender.SelectedItem);
            ointernelUser.MaritalStatus = Convert.ToString(cmbMaritualStatus.SelectedItem);
            ointernelUser.DateOfBirth = dtDOB.Value;
            ointernelUser.Signature = picSignature.Image;
            ointernelUser.IsExchangeUser = chkExchnageUser.Checked;
            if (ointernelUser.IsExchangeUser == true)
            {
                ointernelUser.ExchangeLogin = txtExchangeLogin.Text.Trim();
                ointernelUser.ExchangePassword = txtExchangePwd.Text.Trim();
            }

            return ointernelUser;
        }

        // validate information entered by user before adding/updating        
        private bool ValidateData()
        {
            gloUser oUser = new gloUser(_databseConnectionString);
            if (txtLoginName.Text.Trim() == "")
            {
                MessageBox.Show(" Login Name must be entered ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLoginName.Focus();
                return false;
            }

            // While adding a new User 
            //check if Login name already exists
            //if (_userId == 0)
            {
                if (oUser.CheckUserExists(txtLoginName.Text.Trim(), _userId) == true)
                {
                    MessageBox.Show(" Login name already exists ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLoginName.Focus();
                    return false;
                   
                    //MessageBox.Show(" Login name already exists ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //txtLoginName.Focus();
                    //return false;
                }
                else 
                {
                    return true;
                }
            }


            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show(" Password must be entered ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return false;
            }

            //Password and Confirm Password must be same

            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                MessageBox.Show("Password and Confirm Password must be same", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return false;
            }

            //validate password if password settings are applied
            if (chkApplyPwdSettings.Checked)
            {
                if (!validatePassword())
                {
                    txtPassword.Focus();
                    return false;
                }
            }

            //validate phone number
            if (txtPhoneNo.Text.Trim().Length > 0 && txtPhoneNo.Text.Trim().Length < 10)
            {
                MessageBox.Show(" Invalid Phone Number ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNo.Focus();
                return false;
            }

            //validate SSN number
            if (txtSSN.Text.Trim().Length > 0 && txtSSN.Text.Trim().Length < 9)
            {
                MessageBox.Show(" Invalid SSN Number ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSSN.Focus();
                return false;
            }

            //validate Mobile number
            if (txtMobileNo.Text.Trim().Length > 0 && txtMobileNo.Text.Trim().Length < 10)
            {
                MessageBox.Show(" Invalid Mobile Number ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMobileNo.Focus();
                return false;
            }

            if (chkExchnageUser.Checked == true)
            {
                if (txtExchangeLogin.Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter Exchange Login Name", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtExchangeLogin.Focus();
                    return false;
                }

                if (txtExchangePwd.Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter Exchange Password", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtExchangePwd.Focus();
                    return false;
                }
                
                if (txtExchangePwd.Text.Trim() != txtExchangePwdConfirm.Text.Trim())
                {
                    MessageBox.Show("Exchange Password and Confirm Exchange Password must be same", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtExchangePwd.Focus();
                    return false;
                }
            }

            return true;
        }

        // validate Password if Password Comlexity is Set
         private bool validatePassword()//commented -sandip darade-20080907
        {
            // get the settings from database
            //PasswordComplexity oSetting = new PasswordComplexity(_databaseconnectionstring);
            //DataTable dtSetting = oSetting.getSettings();
            //if (dtSetting != null && dtSetting.Rows.Count > 0)
            //{
            //    oSetting.CapLetters = Convert.ToInt32(dtSetting.Rows[0]["ExpCapitalLetters"]);
            //    oSetting.Letters = Convert.ToInt32(dtSetting.Rows[0]["ExpNoOfLetters"]);
            //    oSetting.Digits = Convert.ToInt32(dtSetting.Rows[0]["ExpNoOfDigits"]);
            //    oSetting.SpecialChar = Convert.ToInt32(dtSetting.Rows[0]["ExpNoOfSpecChars"]);
            //    oSetting.MinLength = Convert.ToInt32(dtSetting.Rows[0]["ExpPwdLength"]);
            //    oSetting.NoOfDays = Convert.ToInt32(dtSetting.Rows[0]["ExpTimeFrameinDays"]);
            //}
            //// if no settings present then use default settings
            //else
            //{
            //    oSetting.CapLetters = 0;
            //    oSetting.Letters = 1;
            //    oSetting.Digits = 0;
            //    oSetting.SpecialChar = 0;
            //    oSetting.MinLength = 8;
            //    oSetting.NoOfDays = 0;
            //}

            ////create regular expressions for validation
            //Regex upper = new Regex("[A-Z]");
            //Regex letters = new Regex("[a-zA-Z]");
            //Regex digits = new Regex("[0-9]");
            //Regex specialChar = new Regex("[^a-zA-Z0-9]");
            //string password = txtPassword.Text.Trim();

            //// Validate Length
            //if (password.Length < oSetting.MinLength)
            //{
            //    MessageBox.Show("The length of the password should be at least " + oSetting.MinLength, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            ////validate no of letters
            //if (letters.Matches(password).Count < oSetting.Letters)
            //{
            //    MessageBox.Show("The password should contain at least " + oSetting.Letters + " letters", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            ////validate no of Capital letters
            //if (upper.Matches(password).Count < oSetting.CapLetters)
            //{
            //    MessageBox.Show("The password should contain at least " + oSetting.CapLetters + " Capital letters", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            ////validate no of digits
            //if (digits.Matches(password).Count < oSetting.Digits)
            //{
            //    MessageBox.Show("The password should contain at least " + oSetting.Digits + " digits", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            ////validate no of Special char
            //if (specialChar.Matches(password).Count < oSetting.SpecialChar)
            //{
            //    MessageBox.Show("The password should contain at least " + oSetting.SpecialChar + " special characters", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            return true;
        }
       
        private void rbtn_CheckedChange(object sender, EventArgs e)
        {
            try
            {
                if (((RadioButton)sender).Text == "Other Information")
                {
                    if (((RadioButton)sender).Checked == true)
                    {
                        pnl_OtherInfo.Visible = true;
                        pnl_OtherInfo.BringToFront();
                        txtFirstName.Focus();
                    }
                }
                if (((RadioButton)sender).Text == "User Details")
                {
                    if (((RadioButton)sender).Checked == true)
                    {
                        pnlUserDetails.Visible = true;
                        pnlUserDetails.BringToFront();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                ex = null;                
            }
        }

        // Attach  Digital Signature        
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                picSignature.SizeMode = PictureBoxSizeMode.StretchImage;
                dlgOpenSignature.Title = " Select Signature ";
                dlgOpenSignature.Filter = "Images Files(*.bmp,*.tif,*.jpg,*.jpeg,*.gif)|*.bmp;*.tif;*.jpg;*.jpeg;*.gif";
                dlgOpenSignature.CheckFileExists = true;
                dlgOpenSignature.Multiselect = false;
                dlgOpenSignature.ShowHelp = false;
                dlgOpenSignature.ShowReadOnly = false;

                if (dlgOpenSignature.ShowDialog() == DialogResult.OK)
                {
                    picSignature.Visible = true;
                    picSignature.Image = Image.FromFile(dlgOpenSignature.FileName);                    
                    Image img;
                    int nHight;
                    int nWidth;

                    img = picSignature.Image;
                    nHight = img.Height;
                    nWidth = img.Width;
                    if (nWidth > 150)
                        nWidth = 150;
                    if (nWidth > 75)
                        nHight = 75;
                    img = new Bitmap(img, new Size(nWidth, nHight));
                    picSignature.Image = img;
                    picSignature.SizeMode = PictureBoxSizeMode.CenterImage;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                ex = null;
            }
        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            picSignature.Image = null;
            picSignature.Refresh();
        }

        // Block or Unblock Selected User
        private void btnBlock_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(" Do you want to " + btnBlock.Text + " this User ? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                {
                    gloUser ogloUser = new gloUser(_databseConnectionString);
                    if (btnBlock.Text == "Block")
                    {
                        ogloUser.BlockUser(_userId, true);
                    }
                    if (btnBlock.Text == "Unblock")
                    {
                        ogloUser.BlockUser(_userId, false);
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        #region UserRights Flex Grid Design

        private void DesignUserRightGrid()
        {
            try
            {
                #region Design Grid

                c1UserRights.Cols.Count = 3;
                c1UserRights.Rows.Count = 1;
                c1UserRights.SetData(0, 0, "Module");
                c1UserRights.SetData(0, 1, "");
                c1UserRights.SetData(0, 2, "Rights");

                int nWidth;
                nWidth = pnlUserRights.Width;

                c1UserRights.Cols[0].Width = Convert.ToInt32(nWidth * 0.45);


                c1UserRights.Cols[1].Width = Convert.ToInt32(nWidth * 0.08);
                c1UserRights.Cols[1].DataType = System.Type.GetType("System.Boolean");//Select Column

                c1UserRights.Cols[2].Width = Convert.ToInt32(nWidth * 0.39);



                CellStyle cs = c1UserRights.Styles.Normal; //Normal;
                cs.Border.Direction = BorderDirEnum.Horizontal; //Vertical;            
                cs.WordWrap = true;
                cs = c1UserRights.Styles.Add("Parent");
                cs.Font = new Font(c1UserRights.Font, FontStyle.Bold);
                cs.BackColor = Color.Lavender;

                cs = c1UserRights.Styles.Add("Child");
                cs.BackColor = Color.AliceBlue;


                // Make tree node
                c1UserRights.Tree.Column = 0; //COL_PatientName  ;
                c1UserRights.Tree.Style = TreeStyleFlags.Simple; // Simple;
                c1UserRights.AllowMerging = AllowMergingEnum.None; //Nodes;  
                c1UserRights.AllowEditing = true;
                c1UserRights.Cols[0].AllowEditing = false;
                c1UserRights.Cols[1].AllowEditing = true;
                c1UserRights.Cols[2].AllowEditing = false;

                #endregion

                #region Display All rights from Rights_MST Table

                gloRights ogloRights = new gloRights(_databseConnectionString);
                DataTable dtModules = ogloRights.GetModuleNames();

                if (dtModules != null)
                {
                    for (int i = 0; i < dtModules.Rows.Count; i++)
                    {
                        c1UserRights.Rows.Add();
                        int ParentRowIndex = c1UserRights.Rows.Count - 1;

                        Modules ModuleName = (Modules)(Convert.ToInt16(dtModules.Rows[i]["nModuleID"]));
                        c1UserRights.SetData(ParentRowIndex, 0, Convert.ToString(ModuleName));

                        Node oParent;
                        c1UserRights.Rows[ParentRowIndex].IsNode = true;
                        c1UserRights.Rows[ParentRowIndex].Node.Key = Convert.ToString(dtModules.Rows[i]["nModuleID"]);
                        c1UserRights.Rows[ParentRowIndex].Node.Data = Convert.ToString(ModuleName);
                        oParent = c1UserRights.Rows[ParentRowIndex].Node;

                        DataTable dtActivities = ogloRights.GetActivityNames(Convert.ToInt32(dtModules.Rows[i]["nModuleID"]));

                        if (dtActivities != null)
                        {
                            for (int k = 0; k < dtActivities.Rows.Count; k++)
                            {
                                Node oChildNode;
                                oChildNode = oParent.AddNode(NodeTypeEnum.LastChild, "", Convert.ToInt16(dtActivities.Rows[k]["nActivityID"]), null);
                                int ChildRowIndex = oChildNode.Row.Index;

                                c1UserRights.SetData(ChildRowIndex, 1, false);
                                Activities ActivityName = (Activities)Convert.ToInt16(dtActivities.Rows[k]["nActivityID"]);
                                c1UserRights.SetData(ChildRowIndex, 2, Convert.ToString(ActivityName));

                                CellStyle chieldCellStyle = c1UserRights.Styles["Child"];
                                if (chieldCellStyle != null)
                                {
                                    for (int l = 0; l < c1UserRights.Cols.Count; l++)
                                    {
                                        c1UserRights.SetCellStyle(ChildRowIndex, l, chieldCellStyle);
                                    }
                                }

                            }
                        }


                        CellStyle parentCellStyle = c1UserRights.Styles["Parent"];
                        if (parentCellStyle != null)
                        {
                            for (int k = 0; k < c1UserRights.Cols.Count; k++)
                            {
                                c1UserRights.SetCellStyle(ParentRowIndex, k, parentCellStyle);
                            }
                        }

                    }// End For 
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.ToString();
                ex = null;
            }
        }

        #endregion

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < c1UserRights.Rows.Count; i++)
                {                    
                    Node oChildNode = c1UserRights.Rows[i].Node;
                    if (oChildNode.Children == 0)                          //if Child Node
                    {
                        c1UserRights.SetData(i, 1, true);
                    }
                 }
            }
            catch (Exception ex)
            {
                ex.ToString();
                ex = null;
            }
        }

        private void chkAuditTrails_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < c1UserRights.Rows.Count; i++)
                {
                    Node oChildNode = c1UserRights.Rows[i].Node;
                    if (oChildNode.Children == 0)                          //if Child Node
                    {
                        c1UserRights.SetData(i, 1, false);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                ex = null;
            }
        }

        private void chkApplyPwdSettings_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkExchnageUser_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExchnageUser.Checked == true)
            {
                txtExchangeLogin.Enabled = true;
                txtExchangePwd.Enabled = true;
                txtExchangePwdConfirm.Enabled = true;
            }
            else
            {
                txtExchangeLogin.Enabled = false;
                txtExchangePwd.Enabled = false;
                txtExchangePwdConfirm.Enabled = false;
            }
        }
       
    }
}