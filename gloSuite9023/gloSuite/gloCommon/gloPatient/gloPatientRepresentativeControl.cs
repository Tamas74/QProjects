using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using gloAddress;
using gloPatient.Classes;

namespace gloPatient
{
    public partial class gloPatientRepresentativeControl : UserControl
    {

        #region "Constructor And Destructor"
        public int IsForAPIAccess = 0;//0 means  for portal 1 means for API
        public gloPatientRepresentativeControl(string ConnectionString ,int mode =0)
        {
            InitializeComponent();
            _databaseconnectionstring = ConnectionString;
            oPatientRepresentatives = new PatientRepresentatives(); 
            oListControl = new gloListControl.gloListControl();
           
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "";
                }
            }
            else
            { _messageboxcaption = ""; }

            #endregion 
            IsForAPIAccess = mode;
            if (IsForAPIAccess == 1)
            {
                label24.Visible = false;
                label25.Visible = false;
                label26.Visible = false;
                label27.Visible = false;
                cmbPRSecurityQuestion.Visible = false;
                txtPRSecurityAnswer.Visible = false;
            }
            else
            {
                label24.Visible = true;
                label25.Visible = true;
                label26.Visible = true;
                label27.Visible = true;
                cmbPRSecurityQuestion.Visible = true;
                txtPRSecurityAnswer.Visible = true;


            }
        }

        #endregion

        #region "Private Variables"
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private string _messageboxcaption = "gloPM";
        private PatientRepresentatives oPatientRepresentatives = null;
        private gloListControl.gloListControl oListControl;
        private bool _IsPRModified = false;
        private bool _IsPRDeleted = false;


        //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
   //     bool _validationFlag = true;
        public Int64 PatientId = 0;
        public bool _IsAllowMultiplePRs = false;
        

        #endregion

        #region "Properties & Procedures"

        public PatientRepresentatives PatientRepresentatives
        {
            get { return oPatientRepresentatives; }
            set { oPatientRepresentatives = value; }
        }

        #endregion

        #region Events And Delegates

        public delegate void CloseButtonClick(object sender, EventArgs e);
        public event CloseButtonClick CloseButton_Click;

        public delegate void SaveButtonClick(object sender, EventArgs e);
        public event SaveButtonClick SaveButton_Click;

        #region Events to select Provider 
        private void btnProviderSelect_Click(object sender, EventArgs e)
        {
            //onProviderSelect_Clicked(sender, e);
            try
            {
                // onPharmaBrowse_Clicked(sender, e);               
                
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ProviderSelectedClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_SelectedClick);
                        }
                        catch
                        {
                        }
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }
                
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, false, this.Width);
                //oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Providers";

                //_CurrentControlType = gloListControl.gloListControlType.Pharmacy;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ProviderSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                this.Controls.Add(oListControl);

                //

                //
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void oListControl_ProviderSelectedClick(object sender, EventArgs e)
        {
            //try
            //{
            //    txtProvider.Text = "";
            //    txtProvider.Tag = null;
            //    if (oListControl.SelectedItems.Count > 0)
            //    {
            //        for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
            //        {
            //            txtProvider.Tag = oListControl.SelectedItems[i].ID;
            //            txtProvider.Text = oListControl.SelectedItems[i].Description;
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            if (oListControl != null)
            {
                for (int i = this.Controls.Count - 1; i >= 0; i--)
                {
                    if (this.Controls[i].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[i]);
                        break;
                    }
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ProviderSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_SelectedClick);
                    }
                    catch
                    {
                    }
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                }
                catch
                {
                }
                
            }
        }
        #endregion

        #endregion

        private void gloPatientGuarantor_Load(object sender, EventArgs e)
        {
            
           

      
            try
            {
                //Fill all controls
                pnlPatientRepresentativeInfo.Visible = true;
                pnlPatientRepresentativeInfo.BringToFront();

              

                //--------------------------------
                trvPatientRepresentative.Nodes.Add("Patient Representatives");

                SetData();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }
        }
         public void Save(object sender, ToolStripItemClickedEventArgs e)
        {

            bool proceedSave = false;
            bool proceedValidation = false;
            if (ValidateData())
            {

                if (!string.IsNullOrEmpty(txtPRFirstName.Text) || !string.IsNullOrEmpty(txtPRLastName.Text) || !string.IsNullOrEmpty(mskPRDOB.Text.ToString())
                 || !string.IsNullOrEmpty(txtPREmail.Text)
                 || !string.IsNullOrEmpty(mtxtPRPhone.Text) || !string.IsNullOrEmpty(txtPRUserName.Text) || !string.IsNullOrEmpty(txtPRPassword.Text)
                 || !string.IsNullOrEmpty(txtPRConfirmPassword.Text) || !string.IsNullOrEmpty(cmbPRSecurityQuestion.Text) || !string.IsNullOrEmpty(txtPRSecurityAnswer.Text))
                {
                    proceedSave = true;
                    proceedValidation = true;
                }
                else
                {
                    if (_IsPRDeleted)
                    { proceedSave = true; }
                    else
                    { proceedSave = false; }
                }

                if (trvPatientRepresentative.Nodes[0].Nodes.Count > 0)
                {
                    if (trvPatientRepresentative.SelectedNode != null && trvPatientRepresentative.SelectedNode.Level != 0)
                    {
                        if (_IsPRModified == false)
                        { _IsPRModified = IsModified((PatientRepresentative)trvPatientRepresentative.SelectedNode.Tag); }
                    }

                    if (_IsPRModified || _IsPRDeleted)
                    { proceedSave = true; }
                }



                if (proceedSave)
                {
                    if (proceedValidation)
                    {
                        if (AddPatientRepresentative() == false)
                        { return; }
                    }

                    if (GetData())
                    {
                        SaveButton_Click(sender, e);
                        ClearPRDetails();
                        trvPatientRepresentative.SelectedNode = trvPatientRepresentative.Nodes[0];

                    }

                }
                else
                {
                    mtxtPRPhone.AllowValidate = false;
                    //CloseButton_Click(sender, e); 
                }
            }  
        }
        public Boolean Cancel(object sender, ToolStripItemClickedEventArgs e)
        {
            if (trvPatientRepresentative.SelectedNode != null && trvPatientRepresentative.SelectedNode.Level != 0)
            {
                if (_IsPRModified == false)
                {
                    _IsPRModified = IsModified((PatientRepresentative)trvPatientRepresentative.SelectedNode.Tag);
                }
            }
            if (_IsPRModified == false && _IsPRDeleted == false)
            {
                mtxtPRPhone.AllowValidate = false;
                CloseButton_Click(sender, e);
            }
            else
            {
                if (trvPatientRepresentative.SelectedNode.Level == 0)
                {
                    if (GetData())
                    {

                        SaveButton_Click(sender, e);
                    }
                }
                else
                {
                    DialogResult res = MessageBox.Show("Do you want to save changes in Patient Representative? ", _messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {


                        if (trvPatientRepresentative.SelectedNode != null && trvPatientRepresentative.SelectedNode.Level != 0)
                        {
                            if (AddPatientRepresentative() == false)
                            {
                                return false;

                            }
                        }
                        if (GetData())
                        {

                            SaveButton_Click(sender, e);
                        }
                    }
                    else if (res == DialogResult.No)
                    {
                        mtxtPRPhone.AllowValidate = false;
                        CloseButton_Click(sender, e);
                    }
                    else
                    {
                        return false;
                    
                    }
                }
           
            }
            return true;
        }
        public void Add(object sender, ToolStripItemClickedEventArgs e)
        {
            if (trvPatientRepresentative.Nodes[0].Nodes.Count > 0)
            {
                bool _isPatientRepresentativeChanged = false;
                if (trvPatientRepresentative.SelectedNode != null && trvPatientRepresentative.SelectedNode.Level != 0)
                {
                    if (_isPatientRepresentativeChanged == false)
                    { _isPatientRepresentativeChanged = IsModified((PatientRepresentative)trvPatientRepresentative.SelectedNode.Tag); }
                }

            }
            if (trvPatientRepresentative.SelectedNode != null && trvPatientRepresentative.SelectedNode.Level != 0)
            {
                if (AddPatientRepresentative())
                {
                    _IsPRModified = true;
                    ClearPRDetails();
                    trvPatientRepresentative.ExpandAll();
                    trvPatientRepresentative.SelectedNode = trvPatientRepresentative.Nodes[0];
                    txtPRFirstName.Focus();
                }
            }
            else
            {
                _IsPRModified = true;
                ClearPRDetails();
                trvPatientRepresentative.ExpandAll();
                trvPatientRepresentative.SelectedNode = trvPatientRepresentative.Nodes[0];
                txtPRFirstName.Focus();
            }
                           
        }
        public void BrowsePR()
        {
            try
            {
                oListControl = new gloListControl.gloListControl();
                if (oListControl != null)
                {
                    for (int i = this.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this.Controls[i].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[i]);
                            break;
                        }
                    }
                    try
                    {
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ProviderSelectedClick);
                        }
                        catch
                        {
                        }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_SelectedClick);
                        }
                        catch
                        {
                        }
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.PatientOtherGuarantors, false, this.Width);
                oListControl.ControlHeader = "Patient Representatives";
                oListControl.PatientID = PatientId;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_SelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                this.Controls.Add(oListControl);
                oListControl.OpenControl();

                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }  
        }
        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        {
                            Save(sender,e);
                          
                            break;
                        }
                    case "Cancel":
                        {
                            Cancel(sender, e);
                           
                        }
                        break;
                    case "Add":
                        {
                            Add(sender, e);
                        }
                        break;
                    case "Remove":
                        {
                                RemovePR();
                        }
                        break;
                    //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                    case "Select Guarantor":
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
        

        #region Methods GetData, SetData, Validations


        public void SetData(PatientRepresentative oPatientRepresentative = null,Int64 _PRId=0)
        {
            try
            {
                if (_PRId != 0)
                {
                    for (int i = 0; i < trvPatientRepresentative.Nodes[0].Nodes.Count; i++)
                    {
                        PatientRepresentative _oPatientRepresentative = (PatientRepresentative)trvPatientRepresentative.Nodes[0].Nodes[i].Tag;
                        if (_oPatientRepresentative.PRId == _PRId)
                        {
                            callbeforeselect = false;
                            trvPatientRepresentative.Nodes.Remove(trvPatientRepresentative.Nodes[0].Nodes[i]);
                            callbeforeselect = true;
                            break;
                        }
                    }

                    return;
                }
                if (oPatientRepresentative == null)
                {
                    if (oPatientRepresentatives.Count != 0)
                    {
                        for (int i = 0; i < oPatientRepresentatives.Count; i++)
                        {
                            TreeNode oNode = new TreeNode();
                            oNode.Text = oPatientRepresentatives[i].FirstName.Trim() + " " + oPatientRepresentatives[i].LastName.Trim();
                            oNode.Tag = oPatientRepresentatives[i];
                            oNode.ImageIndex = 1;
                            oNode.SelectedImageIndex = 1;



                            trvPatientRepresentative.Nodes[0].Nodes.Add(oNode);
                            oNode = null;
                            //Code Added by Mayuri:20091202
                            //by default first node should be selected 
                            if (trvPatientRepresentative.Nodes[0].Nodes.Count > 0)
                            {
                                trvPatientRepresentative.HideSelection = false;
                                trvPatientRepresentative.SelectedNode = trvPatientRepresentative.Nodes[0].Nodes[0];
                                TreeViewEventArgs eArg = new TreeViewEventArgs(trvPatientRepresentative.Nodes[0].Nodes[0]);
                                trvGuarantors_AfterSelect(null, eArg);
                            }
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    TreeNode oNode = new TreeNode();
                    oNode.Text = oPatientRepresentative.FirstName.Trim() + " " + oPatientRepresentative.LastName.Trim();
                    oNode.Tag = oPatientRepresentative;
                    oNode.ImageIndex = 1;
                    oNode.SelectedImageIndex = 1;



                    trvPatientRepresentative.Nodes[0].Nodes.Add(oNode);
                    oNode = null;
                    //Code Added by Mayuri:20091202
                    //by default first node should be selected 
                    if (trvPatientRepresentative.Nodes[0].Nodes.Count > 0)
                    {
                        trvPatientRepresentative.HideSelection = false;
                        trvPatientRepresentative.SelectedNode = trvPatientRepresentative.Nodes[0].Nodes[trvPatientRepresentative.Nodes[0].Nodes.Count - 1];
                        TreeViewEventArgs eArg = new TreeViewEventArgs(trvPatientRepresentative.Nodes[0].Nodes[trvPatientRepresentative.Nodes[0].Nodes.Count - 1]);
                        trvGuarantors_AfterSelect(null, eArg);
                    }
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        public void SetPR()
        {
            if (trvPatientRepresentative.Nodes.Count > 0)
            {
                if (trvPatientRepresentative.Nodes[0].Nodes.Count > 0)
                {
                    trvPatientRepresentative.HideSelection = false;
                    trvPatientRepresentative.SelectedNode = trvPatientRepresentative.Nodes[0].Nodes[trvPatientRepresentative.Nodes[0].Nodes.Count - 1];
                    TreeViewEventArgs eArg = new TreeViewEventArgs(trvPatientRepresentative.Nodes[0].Nodes[trvPatientRepresentative.Nodes[0].Nodes.Count - 1]);
                    trvGuarantors_AfterSelect(null, eArg);
                }
            }
         
        }
        string _encryptionKey = "12345678";
        private bool AddPatientRepresentative()
        {
            bool _Result = false;
            try
            {

                if (ValidateData() == false)
                {
                    return false;
                }
                //if(ValidateData());

                Boolean IsModify = false;
                TreeNode oNode;
                PatientRepresentative oPatientRepresentative = null;
                oNode = null;

                if (txtPRFirstName.Tag != null && Convert.ToString(txtPRFirstName.Tag) != "")
                {
                    oNode = trvPatientRepresentative.Nodes[0].Nodes[Convert.ToInt32(txtPRFirstName.Tag)];
                    oPatientRepresentative = (PatientRepresentative)oNode.Tag;

                    //_IsGuarantorModified is set to true if any info from this control is modified
                    if (_IsPRModified == false)
                        _IsPRModified = IsModified(oPatientRepresentative);

                    //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                    oPatientRepresentative.IsDataModified = _IsPRModified;
                    IsModify = true;
                }
                else
                {
                    oNode = new TreeNode();
                    oNode.ImageIndex = 1;
                    oNode.SelectedImageIndex = 1;

                    oPatientRepresentative = new PatientRepresentative();
                }


                oPatientRepresentative.FirstName = txtPRFirstName.Text.Trim();
                oPatientRepresentative.LastName = txtPRLastName.Text.Trim();
                if (mskPRDOB.MaskCompleted == true)
                    oPatientRepresentative.DOB = Convert.ToDateTime(mskPRDOB.Text);
                oPatientRepresentative.Email = txtPREmail.Text.Trim();

                if (radbtnGIMale.Checked == true)
                    oPatientRepresentative.Gender = "Male";
                else if (radbtnGIFemale.Checked == true)
                    oPatientRepresentative.Gender = "Female";
                else if (radbtnGIOthers.Checked == true)
                    oPatientRepresentative.Gender = "Other";
                oPatientRepresentative.Phone = mtxtPRPhone.Text.Trim();
                oPatientRepresentative.UserName = txtPRUserName.Text.Trim();


                gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
                oPatientRepresentative.Password = oClsEncryption.EncryptToBase64String(txtPRPassword.Text, _encryptionKey);
                if (oClsEncryption != null)
                {
                    oClsEncryption.Dispose();
                }
                if (IsForAPIAccess!=1)
                {
                    oPatientRepresentative.SecurityQuestion = cmbPRSecurityQuestion.Text.Trim();
                    oPatientRepresentative.SecurityAnswer = txtPRSecurityAnswer.Text.Trim();
              
                }
                else
                {
                    oPatientRepresentative.SecurityQuestion = "";
                    oPatientRepresentative.SecurityAnswer ="";
              
                }
            

               

                oNode.Text = oPatientRepresentative.FirstName.Trim() + " " + oPatientRepresentative.LastName.Trim();
                oNode.Tag = oPatientRepresentative;

                if (IsModify == false)
                {
                    trvPatientRepresentative.Nodes[0].Nodes.Add(oNode);
                }
                _Result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _Result = false;
            }
            return _Result;
        }

        public void RemovePR()
        {
            try
            {
                if (trvPatientRepresentative.SelectedNode != null)
                {
                    if (trvPatientRepresentative.SelectedNode.Level != 0)
                    {
                        DialogResult res = MessageBox.Show("Are you sure you want to remove selected Patient Representative ? ", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {

                            _IsPRDeleted = true;
                            ClearPRDetails();
                            trvPatientRepresentative.SelectedNode.Remove();
                            if (trvPatientRepresentative.Nodes[0].Nodes.Count > 0)
                            {
                                trvPatientRepresentative.HideSelection = false;
                                trvPatientRepresentative.SelectedNode = trvPatientRepresentative.Nodes[0].Nodes[0];
                                TreeViewEventArgs eArg = new TreeViewEventArgs(trvPatientRepresentative.Nodes[0].Nodes[0]);
                                trvGuarantors_AfterSelect(null, eArg);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public bool GetData()
        {
            try
            {
                oPatientRepresentatives.Clear();
                if (trvPatientRepresentative.Nodes[0].Nodes.Count > 0)
                {
                    for (int i = 0; i < trvPatientRepresentative.Nodes[0].Nodes.Count; i++)
                    {
                        PatientRepresentative oPatientRepresentative = (PatientRepresentative)trvPatientRepresentative.Nodes[0].Nodes[i].Tag;
                        oPatientRepresentatives.Add(oPatientRepresentative);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
        }

        private void ClearPRDetails()
        {
            txtPRFirstName.Text = "";
            txtPRLastName.Text = "";
            mskPRDOB.Text = "";
            txtPREmail.Text = ""; 
            mtxtPRPhone.Text = "";
            txtPRUserName.Text = "";
            txtPRPassword.Text = "";
            txtPRConfirmPassword.Text = "";
            cmbPRSecurityQuestion.Text = "";
            txtPRSecurityAnswer.Text = "";

            radbtnGIMale.Checked = false;
            radbtnGIFemale.Checked = false;
            radbtnGIOthers.Checked = false;

            //_validationFlag = true; 
        }

        private bool ValidateData()
        {
            if (txtPRFirstName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter first name.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPRFirstName.Focus();
                return false;
            }
            if (txtPRFirstName.Text.Trim().Length > 50)
            {
                MessageBox.Show("Length of first name should not be greater than 50  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPRFirstName.Focus();
                return false;
            }
            if (txtPRLastName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter last name.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPRLastName.Focus();
                return false;
            }
            if (txtPRLastName.Text.Trim().Length > 50)
            {
                MessageBox.Show("Length of last name should not be greater than 50  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPRLastName.Focus();
                return false;
            }
            if (mskPRDOB.Text.Trim() == "")
            {
                MessageBox.Show("Please enter date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskPRDOB.Focus();
                return false;
            }
          
            //date of birth   
            mskPRDOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (mskPRDOB.Text.Length > 0 && mskPRDOB.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                if (mskPRDOB.MaskCompleted == true)
                {
                    try
                    {
                        mskPRDOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        if (IsValidDate(mskPRDOB.Text))
                        {
                            if (Convert.ToDateTime(mskPRDOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mskPRDOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(mskPRDOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                            {
                                MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskPRDOB.Focus();
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                    catch (Exception) // ex)
                    {
                        //ex.ToString();
                        //ex = null;
                        MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }

            if (txtPREmail.Text.Trim() == "")
            {
                MessageBox.Show("Please enter email id.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPREmail.Focus();
                return false;
            }
            if (txtPREmail.Text.Trim().Length > 50)
            {
                MessageBox.Show("Length of email id should not be greater than 50  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPREmail.Focus();
                return false;
            }
            if (CheckEmailAddress(txtPREmail.Text) == false)
            {
                MessageBox.Show("Please enter valid email id.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPREmail.Focus();
                return false;
            }
            if (IsDuplicatePR("2"))
            {
                MessageBox.Show("Duplicate email id", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPREmail.Focus();
                return false;
            }
            if (!radbtnGIMale.Checked && !radbtnGIFemale.Checked && !radbtnGIOthers.Checked)
            {
                MessageBox.Show("Please select Gender.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            
            }
            if (mtxtPRPhone.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Phone No.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtxtPRPhone.Focus();
                return false;
            }
           

            if (mtxtPRPhone.IsValidated == false)
            {
                MessageBox.Show("Please enter valid Phone No.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtxtPRPhone.Focus();
                return false;
            }

          
            if (txtPRUserName.Text.Trim() == ""  )
            {
                MessageBox.Show("Please enter User Name", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPRUserName.Focus();
                return false;
            }
            if (txtPRUserName.Text.Trim().Length > 50)
            {
                MessageBox.Show("Length of User Name should not be greater than 50  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPRUserName.Focus();
                return false;
            }
            if (IsDuplicatePR("1"))
            {
                MessageBox.Show("Duplicate User Name", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPRUserName.Focus();
                return false;
            }
            if (txtPRPassword.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Password", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPRPassword.Focus();
                return false;
            }
            if (txtPRPassword.Text.Trim().Length > 50)
            {
                MessageBox.Show("Length of Password should not be greater than 50  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPRPassword.Focus();
                return false;
            }
            if (string.Compare(txtPRPassword.Text, txtPRConfirmPassword.Text) != 0)
            {
                MessageBox.Show("Password and Confirm Password must be same.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPRConfirmPassword.Focus();
                return false;
            }
            if (IsForAPIAccess != 1)
            {


                if (cmbPRSecurityQuestion.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter Security Question", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPRSecurityQuestion.Focus();
                    return false;
                }
                if (txtPRSecurityAnswer.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter Security Answer", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPRSecurityAnswer.Focus();
                    return false;
                }
                if (txtPRSecurityAnswer.Text.Trim().Length > 50)
                {
                    MessageBox.Show("Length of Security Answer should not be greater than 50 ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPRSecurityAnswer.Focus();
                    return false;
                }
            }
            return true;
        }

        gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
        private bool IsModified(PatientRepresentative oPatientRepresentative)
        {
            bool _Result = true;
            try
            {

                //..Sagar Ghodke - 20110825
                //..Code added to check if the guarantor details are changed, the mask controls are compared with the literals in them
                //..& the guarantor object was behaving in random fashion for having and not having literals in ssn,phone,mobile values
                //..so 1. if mask is completed made it to include literals before comparison
                //..2. if specially for ssn mask if the guarantor object having literals then only turned on the ssn mask literals
                //..Code changes for BUG#7965
               
             
                if (mtxtPRPhone.MaskFull == true)
                {
                    if (oPatientRepresentative.Phone.Contains("("))
                    { mtxtPRPhone.IncludeLiteralsAndPrompts = true; }
                }

               
                //Check if Guarantor details are modified
                if (txtPRFirstName.Text == oPatientRepresentative.FirstName
                    && txtPRLastName.Text == oPatientRepresentative.LastName
                    && (mskPRDOB.MaskCompleted && mskPRDOB.Text == oPatientRepresentative.DOB.ToString("MM/dd/yyyy") || (mskPRDOB.Text == "  /  /" && oPatientRepresentative.DOB == DateTime.MinValue))
                    && txtPREmail.Text == oPatientRepresentative.Email.Replace(" ", "")
                    && mtxtPRPhone.Text == oPatientRepresentative.Phone.Replace(" ", "")
                    && txtPRUserName.Text == oPatientRepresentative.UserName
                    && oClsEncryption.EncryptToBase64String(txtPRPassword.Text, _encryptionKey) == oPatientRepresentative.Password
                    && cmbPRSecurityQuestion.Text == oPatientRepresentative.SecurityQuestion
                    && txtPRSecurityAnswer.Text == oPatientRepresentative.SecurityAnswer
                    )
                {

                    //Check if Guarantor Gender is modified
                    if ((oPatientRepresentative.Gender == "Male" && radbtnGIMale.Checked == true)
                         || (oPatientRepresentative.Gender == "Female" && radbtnGIFemale.Checked == true)
                         || (oPatientRepresentative.Gender == "Other" && radbtnGIOthers.Checked == true)
                         || (oPatientRepresentative.Gender.ToString().Trim().Length == 0 && (radbtnGIMale.Checked == false && radbtnGIFemale.Checked == false && radbtnGIOthers.Checked == false))//for other guardian
                        )
                    {

                        _Result = false;
                      
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
                mtxtPRPhone.IncludeLiteralsAndPrompts = false;
            }
            return _Result; 
        }

     

        #endregion
        Boolean callbeforeselect = true;
        private void trvGuarantors_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (!callbeforeselect) return;
            if (e.Node.Level != 0)
            {
                if (txtPRFirstName.Text.Trim() != "")
                {
                    if (AddPatientRepresentative() == false)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void trvGuarantors_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node.Level != 0)
                {
                    PatientRepresentative oPatientRepresentative = (PatientRepresentative)e.Node.Tag;
                    
                    ClearPRDetails();
                    txtPRFirstName.Tag = e.Node.Index;
                    txtPRFirstName.Text = oPatientRepresentative.FirstName;
                    txtPRLastName.Text = oPatientRepresentative.LastName;
                    if (oPatientRepresentative.DOB != null && oPatientRepresentative.DOB != DateTime.MinValue)
                    {
                        mskPRDOB.Text = oPatientRepresentative.DOB.ToString("MM/dd/yyyy");
                    }
                    txtPREmail.Text = oPatientRepresentative.Email;

                    radbtnGIMale.Checked = false;
                    radbtnGIFemale.Checked = false;
                    radbtnGIOthers.Checked = false;

                    if (oPatientRepresentative.Gender == "Male")
                        radbtnGIMale.Checked = true;
                    else if (oPatientRepresentative.Gender == "Female")
                        radbtnGIFemale.Checked = true;
                    else if (oPatientRepresentative.Gender == "Other")
                        radbtnGIOthers.Checked = true;

                    mtxtPRPhone.Text = oPatientRepresentative.Phone;
                    txtPRUserName.Text = oPatientRepresentative.UserName;
                    gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
                    txtPRPassword.Text = oClsEncryption.DecryptFromBase64String(oPatientRepresentative.Password, _encryptionKey);
                    txtPRConfirmPassword.Text = txtPRPassword.Text;
                    if (oClsEncryption != null)
                    {
                        oClsEncryption.Dispose();
                    }
                    cmbPRSecurityQuestion.Text = oPatientRepresentative.SecurityQuestion;
                    txtPRSecurityAnswer.Text = oPatientRepresentative.SecurityAnswer;
                }
                else
                {
                    txtPRFirstName.Tag = null;
                    ClearPRDetails();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnProviderDelete_Click(object sender, EventArgs e)
        {
                //txtProvider.Text = "";
                //txtProvider.Tag = null;
           
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

        private void txtPREmail_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txtPREmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void radbtnGIMale_CheckedChanged(object sender, EventArgs e)
        {
            if (radbtnGIMale.Checked == true)
            {
                radbtnGIMale.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                radbtnGIMale.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void radbtnGIFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (radbtnGIFemale.Checked == true)
            {
                radbtnGIFemale.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                radbtnGIFemale.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void radbtnGIOthers_CheckedChanged(object sender, EventArgs e)
        {
            if (radbtnGIOthers.Checked == true)
            {
                radbtnGIOthers.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                radbtnGIOthers.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

       

        private void pnlTreeView_Paint(object sender, PaintEventArgs e)
        {

        }
      
        private void oListControl_SelectedClick(object sender, EventArgs e)
        {
            try
            {

                if (oListControl.SelectedItems.Count > 0)
                {
                    if (oPatientRepresentatives.Count == 1)
                    {
                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        //Represents guarantorId
                        PatientId = Convert.ToInt64(oListControl.SelectedItems[0].Code);
                        DataTable dtPR = GetPatientRepresentativesByPatientId();

                        if (dtPR != null && dtPR.Rows.Count > 0)
                        {
                            PatientRepresentative oPatientRepresentative = new PatientRepresentative();
                            oPatientRepresentative.FirstName = dtPR.Rows[0]["sFirstName"].ToString();
                            oPatientRepresentative.LastName = dtPR.Rows[0]["sLastName"].ToString();
                            if (dtPR.Rows[0]["dtDOB"] != null && dtPR.Rows[0]["dtDOB"].ToString() != "")
                            {
                                oPatientRepresentative.DOB = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtPR.Rows[0]["dtDOB"]));
                            }
                            oPatientRepresentative.Email = dtPR.Rows[0]["sEmail"].ToString();
                            oPatientRepresentative.Gender = dtPR.Rows[0]["sGender"].ToString();
                            oPatientRepresentative.Phone = dtPR.Rows[0]["sPhone"].ToString();
                            if (oPatientRepresentatives.Count == 0)
                            {
                                this.oPatientRepresentatives.Add(oPatientRepresentative);
                                SaveButton_Click(sender, e);
                                SetData();
                            }
                            else
                            {
                                if (oPatientRepresentatives.Count >= 1)
                                {
                                    MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public DataTable GetPatientRepresentativesByPatientId()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPR = new DataTable();
            try
            {
                oDB.Connect(false);

                string _strSqlQuery = "Select  sFirstName,sLastName,dtDOB,sGender,sEmail,sPhone " +
                         "From PatientRepresentative_dtl inner join PatientRepresentative_mst on PatientRepresentative_dtl.nprid=PatientRepresentative_mst.nprid WITH (NOLOCK) " +
                         "Where PatientRepresentative_dtl.nPatientID = " + PatientId;

                oDB.Retrive_Query(_strSqlQuery, out dtPR);

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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dtPR;
        }
        public Boolean IsDuplicatePR(string ValidationType)
        {
            //for (int i = 0; i <= oPatientRepresentatives.Count - 1; i++)
            //{
            //    if (oPatientRepresentatives[i] != null)
            //    {
            //        if (((PatientRepresentative)oPatientRepresentatives[i]).Email.Trim() == txtPREmail.Text.Trim())
            //        {
            //            return false;
            //        }
            //    }
                
            //}
            for (int i = 0; i <= trvPatientRepresentative.Nodes[0].Nodes.Count - 1; i++)
            {

                if (trvPatientRepresentative.Nodes[0].Nodes[i].Tag != null)
                {
                    if (trvPatientRepresentative.SelectedNode != null)
                    {
                        if (trvPatientRepresentative.SelectedNode.Tag != null)
                        {
                            if (trvPatientRepresentative.SelectedNode.Index == i)
                            {
                                continue;
                            }
                        }
                    }
                    if (((PatientRepresentative)trvPatientRepresentative.Nodes[0].Nodes[i].Tag).Email.Trim() == txtPREmail.Text.Trim())
                    {
                        return true;
                    }
                }

            }
            DataTable dtPR = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = default(gloDatabaseLayer.DBParameters);
            try
            {
                oDB.Connect(false);
                odbParams = new gloDatabaseLayer.DBParameters();
                long nvPRId = 0;
                if (trvPatientRepresentative.SelectedNode != null)
                {
                    if (trvPatientRepresentative.SelectedNode.Tag != null)
                    {
                            if (((PatientRepresentative)trvPatientRepresentative.SelectedNode.Tag).PRId != 0)
                            {
                                nvPRId = ((PatientRepresentative)trvPatientRepresentative.SelectedNode.Tag).PRId;
                            }
                    }
                }
                odbParams.Add("@nPatientID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@sFirstName","", ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sLastName", "", ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@dtDOB", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);
                odbParams.Add("@sGender", "", ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sEmail", txtPREmail.Text.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sPhone", "", ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sUserName", txtPRUserName.Text.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sPassword", "", ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@nvPRId", nvPRId, ParameterDirection.Input, SqlDbType.BigInt);
                odbParams.Add("@sValidate", ValidationType, ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sSecurityQuestion", cmbPRSecurityQuestion.Text.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                odbParams.Add("@sSecurityAnswer", txtPRSecurityAnswer.Text.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                if (IsForAPIAccess==0)
                {
                    oDB.Retrive("gsp_INUP_PatientRepresentative", odbParams, out dtPR);

                }
                else
                {//IsForAPIAccess==1
                    oDB.Retrive("gsp_INUP_PatientAPIRepresentative", odbParams, out dtPR);
                }
                oDB.Disconnect();

                if (dtPR != null)
                {
                    if (dtPR.Rows.Count > 0)
                    {
                        if (dtPR.Rows[0][0].ToString() == "1")
                        {
                            return true;
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
            }
            finally
            {
         
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }

                if (dtPR != null)
                {
                    dtPR.Dispose();
                    dtPR = null;
                }
            }

            return false;


        }

        private void mskPRDOB_Validating(object sender, CancelEventArgs e)
        {
            mskPRDOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (mskPRDOB.Text.Length > 0 && mskPRDOB.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                if (mskPRDOB.MaskCompleted == true)
                {
                    try
                    {
                        mskPRDOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        if (IsValidDate(mskPRDOB.Text))
                        {
                            if (Convert.ToDateTime(mskPRDOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mskPRDOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(mskPRDOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                            {
                                MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskPRDOB.Focus();
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                    catch (Exception) // ex)
                    {
                        //ex.ToString();
                        //ex = null;
                        MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
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

        public void DisposeAllControls()
        {
            try
            {
                if (oPatientRepresentatives != null) { oPatientRepresentatives.Dispose(); }
                if (oListControl != null) { oListControl.Dispose(); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void mskPRDOB_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        private void mskPRDOB_Validating_1(object sender, CancelEventArgs e)
        {
            mskPRDOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (mskPRDOB.Text.Length > 0 && mskPRDOB.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                if (mskPRDOB.MaskCompleted == true)
                {
                    try
                    {
                        mskPRDOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        //Code review changes Replace IsValidDate() by gloDateMaster.gloDate.IsValidDateV2()
                        if (IsValidDate(mskPRDOB.Text))
                        {
                            //if (Convert.ToDateTime(mskPRDOB.Text).Date >= DateTime.Now.Date)
                            //if (Convert.ToDateTime(mskPRDOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mskPRDOB.Text).Date >= DateTime.Now.Date || Convert.ToDateTime(mskPRDOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                            if (Convert.ToDateTime(mskPRDOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mskPRDOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(mskPRDOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                            {
                                MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskPRDOB.Focus();
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                    catch (Exception) // ex)
                    {
                        //ex.ToString();
                        //ex = null;
                        MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
