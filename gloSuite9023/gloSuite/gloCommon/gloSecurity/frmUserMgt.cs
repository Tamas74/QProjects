using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace gloSecurity
{
    public partial class frmUserMgt : Form
    {
        #region Declarations
       
        private string _databseConnectionString = "";
       // private string _messageBoxCaption = "gloPMS";
        
        //Added By Pramod For Message Box
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private const string _encryptionKey = "12345678";
        private Int64  _userId = 0;
        private User oUser;
        
#endregion

        #region Constructor
        
        public frmUserMgt(string databseConnectionString)
        {
            //Added By Pramod Nair For Messagebox Caption 
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

            _databseConnectionString = databseConnectionString;
            InitializeComponent();

        }

        public frmUserMgt(Int64 UserId, string databseConnectionString)
        {
            //Added By Pramod Nair For Messagebox Caption 
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

            _databseConnectionString = databseConnectionString;
            _userId = UserId;
            InitializeComponent();
        }

        #endregion
        
        private void frmUserMgt_Load(object sender, EventArgs e)
        {
            pnlUserDetails.Visible = true;
            pnlUserDetails.BringToFront();
            fillProviders();
            if (_userId != 0)
            {
                gloUser ogloUser = new gloUser(_databseConnectionString);
                DataTable dtUser = ogloUser.GetUser(_userId);
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    displayUserInfo(dtUser);
                    btnBlock.Visible = true;
                }
                dtUser.Dispose();
                ogloUser.Dispose();
            }            
        }

        /// <summary>
        /// Display the information of selected user for modification
        /// </summary>
        /// <param name="dtUser"></param>
        private void displayUserInfo(DataTable dtUser)
        {
            ClsEncryption oDecrypt = new ClsEncryption();
            try
            {

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
                txtStreet.Text = Convert.ToString(dtUser.Rows[0]["sStreet"]);
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
                    try
                    {
                        ms.Close();
                        ms.Dispose();
                        ms = null;
                    }
                    catch
                    {
                    }
                    finally
                    {
                        arrImage = null;
                    }
                }

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;

            }
            finally
            {
                if (oDecrypt != null) { oDecrypt.Dispose(); oDecrypt = null; }
            }
           
        }

        /// <summary>
        /// Fill the list of providers into combo box
        /// </summary>
        private void fillProviders()
        {
            gloUser ogloUser = null;
            DataTable dtProviders = null;
            try
            {
                ogloUser = new gloUser(_databseConnectionString);
                dtProviders = ogloUser.GetAllProviders();

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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                //if (dtProviders != null) { dtProviders.Dispose(); dtProviders = null; }
                if (ogloUser != null) { ogloUser.Dispose(); ogloUser = null; }
            }
        }


        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            gloUser ogloUser = new gloUser(_databseConnectionString); 
            switch (e.ClickedItem.Tag.ToString())
            {
                case "OK":
                    if (ValidateData())
                    {
                        oUser = setUserData();
                        if (ogloUser.AddUser(oUser))
                        {
                            this.Close();                            
                        }
                        else
                        {
                            MessageBox.Show(" Can not Add/modify User ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                          
                        }
                        oUser = null;
                    }                    
                    break;


                case "Cancel":
                    try
                    {
                        this.Close();
                    }
                    catch (Exception) // ex)
                    {
                        //ex.ToString();
                        //ex = null;
                    }//catch

                    break;
            }
            if (ogloUser != null) { ogloUser.Dispose(); ogloUser = null; }
        }

        /// <summary>
        /// set the User object with all the information user has entered 
        /// 
        /// this method is called after validating data
        /// </summary>
        private User setUserData()
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
            ointernelUser.Street = txtStreet.Text.Trim();
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

            if (oEncrypt != null) { oEncrypt.Dispose(); oEncrypt = null; }

            return ointernelUser;
        }

        /// <summary>
        /// validate information entered by user before adding/updating
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            gloUser oUser = new gloUser(_databseConnectionString);


            if (txtLoginName.Text.Trim() == "")
            {
                MessageBox.Show(" Login Name must be entered ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLoginName.Focus();
                return false;
            }

            // While adding a new User 
            //check if Login name already exists
            if (_userId == 0)
            {
                if (oUser.CheckUserExists(txtLoginName.Text.Trim()))
                {
                    MessageBox.Show(" Login name already exists ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtLoginName.Focus();
                    return false;
                }
            }


            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show(" Password must be entered ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return false;
            }

            // While adding a new User 
            //Password and Confirm Password must be same
           
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                MessageBox.Show("Password and Confirm Password must be same", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(" Invalid Phone Number ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNo.Focus();
                return false;
            }

            //validate SSN number
            if (txtSSN.Text.Trim().Length > 0 && txtSSN.Text.Trim().Length < 9)
            {
                MessageBox.Show(" Invalid SSN Number ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSSN.Focus();
                return false;
            }

            //validate Mobile number
            if (txtMobileNo.Text.Trim().Length > 0 && txtMobileNo.Text.Trim().Length < 10)
            {
                MessageBox.Show(" Invalid Mobile Number ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMobileNo.Focus();
                return false;
            }

            if (oUser != null) { oUser.Dispose(); oUser = null; }

            return true;
        }

        private bool validatePassword()
        {
            // get the settings from database
            ClsPwdSettings oSetting = new ClsPwdSettings(_databseConnectionString);
            DataTable dtSetting = oSetting.getSettings();
            if (dtSetting != null && dtSetting.Rows.Count > 0)
            {
                oSetting.CapLetters = Convert.ToInt32(dtSetting.Rows[0]["ExpCapitalLetters"]);
                oSetting.Letters = Convert.ToInt32(dtSetting.Rows[0]["ExpNoOfLetters"]);
                oSetting.Digits = Convert.ToInt32(dtSetting.Rows[0]["ExpNoOfDigits"]);
                oSetting.SpecialChar = Convert.ToInt32(dtSetting.Rows[0]["ExpNoOfSpecChars"]);
                oSetting.MinLength = Convert.ToInt32(dtSetting.Rows[0]["ExpPwdLength"]);
                oSetting.NoOfDays = Convert.ToInt32(dtSetting.Rows[0]["ExpTimeFrameinDays"]);
            }
            // if no settings present then use default settings
            else
            {
                oSetting.CapLetters = 0;
                oSetting.Letters = 1;
                oSetting.Digits = 0;
                oSetting.SpecialChar = 0;
                oSetting.MinLength = 8;
                oSetting.NoOfDays = 0;
            }
            
            //create regular expressions for validation
            Regex upper = new Regex("[A-Z]");            
            Regex letters  = new Regex("[a-zA-Z]"); 
            Regex digits  = new Regex("[0-9]");
            Regex specialChar = new Regex("[^a-zA-Z0-9]"); 
            string password = txtPassword.Text.Trim();

            // Validate Length
            if (password.Length < oSetting.MinLength)
            {
                 MessageBox.Show("The length of the password should be at least " + oSetting.MinLength, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return false;
            }

            //validate no of letters
            if (letters.Matches(password).Count < oSetting.Letters)
            {
                MessageBox.Show("The password should contain at least " + oSetting.Letters + " letters", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            //validate no of Capital letters
            if (upper.Matches(password).Count < oSetting.CapLetters)
            {
                MessageBox.Show("The password should contain at least " + oSetting.CapLetters + " Capital letters", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            //validate no of digits
            if (digits.Matches(password).Count < oSetting.Digits)
            {
                MessageBox.Show("The password should contain at least " + oSetting.Digits + " digits", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            //validate no of Special char
            if (specialChar.Matches(password).Count < oSetting.SpecialChar)
            {
                MessageBox.Show("The password should contain at least " + oSetting.SpecialChar + " special characters", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            upper = null;
            letters = null;
            digits = null;
            specialChar = null;
            password = null;

            if (oSetting != null) { oSetting.Dispose(); oSetting = null; }
            if (dtSetting != null) { dtSetting.Dispose(); dtSetting = null; }

            return true;
        }

     
        /// <summary>
        /// Event for displaying selected panel to User
        /// depending on the radio button he has checked
        /// i.e : User Details , Other information , Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn_CheckedChange(object sender, EventArgs e)
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

        /// <summary>
        /// Attach  Digital Signature
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                if (dlgOpenSignature.ShowDialog(this) == DialogResult.OK)
                {
                    picSignature.Visible = true;
                    if (picSignature.Image != null)
                    {
                        picSignature.Image.Dispose();
                    }
                    picSignature.Image = Image.FromFile(dlgOpenSignature.FileName);
                    txtSignatureImage.Text = dlgOpenSignature.FileName;
                    Image img=(Image) picSignature.Image.Clone();
                    int nHight;
                    int nWidth;

                    
                    nHight = img.Height;
                    nWidth = img.Width;
                    if (nWidth > 150)
                        nWidth = 150;
                    if (nWidth > 75)
                        nHight = 75;
                    img = new Bitmap(img, new Size(nWidth, nHight));
                    picSignature.Image = (Image) img.Clone();
                    img.Dispose();
                    img = null;
                    picSignature.SizeMode = PictureBoxSizeMode.CenterImage;
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            picSignature.Image = null;
            picSignature.Refresh();            
        }

        /// <summary>
        /// Block or Unblock Selected User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBlock_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Do you want to " + btnBlock.Text + " this User ? ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
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

      
        
       
    }
}