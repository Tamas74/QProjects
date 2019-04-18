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
    public partial class gloPatientGuarantorControl : UserControl
    {

        #region "Constructor And Destructor"

        public gloPatientGuarantorControl(string ConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = ConnectionString;
            //_PatientGuarantor = new PatientGuarantor(_databaseconnectionstring);
            oPatientGuarantors = new PatientOtherContacts(); 
            oListControl = new gloListControl.gloListControl();
           
            //Code added by SaiKrishna date 02-07-2011
            objgloAccount = new gloAccount(_databaseconnectionstring);

            //Sandip Darade  20090428
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

        #region "Private Variables"
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseconnectionstring = "";
        private string _messageboxcaption = "gloPM";
        //private PatientGuarantor _PatientGuarantor = null;
        private PatientOtherContacts oPatientGuarantors = null;
        private gloListControl.gloListControl oListControl;
        private bool _IsGuarantorModified = false;
        private bool _IsGuarantorDeleted = false;

        private bool _IsInternetFax = false;

        gloAddressControl oAddresscontrol = null;

        //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
        gloAccount objgloAccount = null;
        bool _validationFlag = true;
        public bool FromAccountGuarantor = false;
        public Int64 PatientId = 0;
        public bool _IsAllowMultipleGuarantors = false;
        

        #endregion

        #region "Properties & Procedures"

        //public PatientGuarantor GuarantorDetail
        //{
        //    get { return _PatientGuarantor; }
        //    set { _PatientGuarantor = value; }
        //}

        public PatientOtherContacts PatientGuarantors
        {
            get { return oPatientGuarantors; }
            set { oPatientGuarantors = value; }
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
                /// 
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
            
            //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
            chkCommercialGuarantor.Checked = false;
            rbPrimary.Checked = true;

            //Sandip Darade 20091006
            //If fax is not internet fax do no masking  for fax information
                if (_IsInternetFax == false)
            {
                txtGIFax.MaskType = gloMaskControl.gloMaskType.Other;
            }
           
            oAddresscontrol = new gloAddressControl(_databaseconnectionstring);
            oAddresscontrol.Dock = DockStyle.Fill;
            oAddresscontrol.Name = "PatientOtherGuarantorAddressControl";
            pnlAddresssControl.Controls.Add(oAddresscontrol);
            try
            {
                //Fill all controls
                pnlGaurantorInfo.Visible = true;
                pnlGaurantorInfo.BringToFront();

                fillStates();

                //Added By MaheshB
                fillRelationShip();
                //SetGuarantor();
                txtGIPatFName.Focus();


                //--------------------------------
                trvGuarantors.Nodes.Add("Guarantors");

                SetData();
                //trvGuarantors.ExpandAll();

                //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                //chkCommercialGuarantor.Enabled = false;
                _IsAllowMultipleGuarantors = objgloAccount.GetAllowMultipleGuarantorsFeatureSetting();
                if (FromAccountGuarantor == false || _IsAllowMultipleGuarantors == false)
                {
                    tsb_SelectGuarantor.Visible = false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                chkCommercialGuarantor.Focus();
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
                            if (oAddresscontrol.txtZip.Focused == true)
                            {
                                oAddresscontrol.txtZip_LostFocus(null, null);
                            }
                            bool proceedSave = false;
                            bool proceedValidation = false;
                            if (ValidateData())
                            {
                           
                            if (!string.IsNullOrEmpty(txtGIPatFName.Text) || !string.IsNullOrEmpty(txtGIPatLName.Text) || !string.IsNullOrEmpty(txtCommercialName.Text))
                            { 
                                proceedSave = true;
                                proceedValidation = true;
                            }
                            else
                            {
                                if (_IsGuarantorDeleted)
                                { proceedSave = true; }
                                else
                                { proceedSave = false; }
                            }

                            if (trvGuarantors.Nodes[0].Nodes.Count > 0)
                            {
                                if (trvGuarantors.SelectedNode != null && trvGuarantors.SelectedNode.Level != 0)
                                {
                                    if (_IsGuarantorModified == false)
                                    { _IsGuarantorModified = IsModified((PatientOtherContact)trvGuarantors.SelectedNode.Tag); }
                                }

                                if (_IsGuarantorModified || _IsGuarantorDeleted)
                                { proceedSave = true; }
                            }

                            //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                            // User should not be able to edit guarantor information when that guarantor is a patient already in the system
                            if (_IsGuarantorModified == true)
                            {
                                if (trvGuarantors.SelectedNode != null)
                                {
                                    PatientOtherContact oGuar = (PatientOtherContact)trvGuarantors.SelectedNode.Tag;
                                    if (oGuar != null)
                                    {
                                        if (oGuar.GuarantorAsPatientID > 0)
                                        {
                                            MessageBox.Show("Guarantor information cannot be edit.\nGuarantor is a patient already in the system.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            return;
                                        }
                                    }
                                }

                            }

                            if (proceedSave)
                            {
                                if (proceedValidation)
                                {
                                    if (AddGuarantor() == false)
                                    { break; }
                                }

                                if (GetData())
                                { SaveButton_Click(sender, e); }
                                
                            }
                            else
                            {
                                txtGIFax.AllowValidate = false;
                                mtxtGISSN.AllowValidate = false;
                                mskGIPhone.AllowValidate = false;
                                mskGIMobile.AllowValidate = false;
                               
                                CloseButton_Click(sender, e); }
                            }
                            break;
                        }
                    case "Cancel":
                        {
                            if (trvGuarantors.SelectedNode != null && trvGuarantors.SelectedNode.Level != 0)
                            {
                                if (_IsGuarantorModified == false)
                                {
                                    _IsGuarantorModified = IsModified((PatientOtherContact)trvGuarantors.SelectedNode.Tag);
                                }
                            }
                            if (_IsGuarantorModified == false && _IsGuarantorDeleted == false)
                            {
                                mtxtGISSN.AllowValidate = false;
                                mskGIPhone.AllowValidate = false;
                                mskGIMobile.AllowValidate = false;
                                txtGIFax.AllowValidate = false;
                                CloseButton_Click(sender, e);
                            }
                            else
                            {
                                DialogResult res = MessageBox.Show("Do you want to save changes to this record? ", _messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                if (res == DialogResult.Yes)
                                {
                                    //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                                    // User should not be able to edit guarantor information when that guarantor is a patient already in the system
                                    if (_IsGuarantorModified == true)
                                    {
                                        if (trvGuarantors.SelectedNode != null)
                                        {
                                            PatientOtherContact oGuar = (PatientOtherContact)trvGuarantors.SelectedNode.Tag;
                                            if (oGuar != null)
                                            {
                                                if (oGuar.GuarantorAsPatientID > 0)
                                                {
                                                    MessageBox.Show("Guarantor information cannot be edit.\nGuarantor is a patient already in the system.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    return;
                                                }
                                            }
                                            if (oAddresscontrol.txtZip.Focused == true)
                                            {
                                                oAddresscontrol.txtZip_LostFocus(null, null);
                                            }
                                        }

                                    }

                                    if (trvGuarantors.SelectedNode != null && trvGuarantors.SelectedNode.Level != 0)
                                    {
                                        if (AddGuarantor() == false)
                                        {
                                            break;
                                        }
                                    }
                                    if (GetData())
                                    {

                                        SaveButton_Click(sender, e);
                                    }
                                }
                                else if (res == DialogResult.No)
                                {
                                    mtxtGISSN.AllowValidate = false;
                                    mskGIPhone.AllowValidate = false;
                                    mskGIMobile.AllowValidate = false;
                                    txtGIFax.AllowValidate = false;
                                    CloseButton_Click(sender, e);
                                }
                            }
                        }
                        break;
                    case "Add":
                        {
                            //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                            if (FromAccountGuarantor == true)
                            {
                                if (trvGuarantors.Nodes[0].Nodes.Count == 1)
                                {

                                    MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            //_IsGuarantorModified = true;

                            //..Start  : Sagar Ghodke - 20110825
                            //..Code added as the patient as guarantor information cannot be edited
                            //...Code added to fix BUG#7965
                            if (trvGuarantors.Nodes[0].Nodes.Count > 0)
                            {
                                bool _isGuarantorDetailsChanged = false;
                                if (trvGuarantors.SelectedNode != null && trvGuarantors.SelectedNode.Level != 0)
                                {
                                    if (_isGuarantorDetailsChanged == false)
                                    { _isGuarantorDetailsChanged = IsModified((PatientOtherContact)trvGuarantors.SelectedNode.Tag); }
                                }
                                
                                // User should not be able to edit guarantor information when that guarantor is a patient already in the system
                                if (_isGuarantorDetailsChanged == true)
                                {
                                    if (trvGuarantors.SelectedNode != null)
                                    {
                                        PatientOtherContact oGuar = (PatientOtherContact)trvGuarantors.SelectedNode.Tag;
                                        if (oGuar != null)
                                        {
                                            if (oGuar.GuarantorAsPatientID > 0)
                                            {
                                                MessageBox.Show("Guarantor information cannot be edit.\nGuarantor is a patient already in the system.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                        }
                                    }

                                }
                                
                            }

                            

                            //..End  : Sagar Ghodke - 20110825

                            if (AddGuarantor())
                            {
                                _IsGuarantorModified = true;
                                chkCommercialGuarantor.Enabled = true;
                                ClearGuarantorDetails();
                                trvGuarantors.ExpandAll();
                                trvGuarantors.SelectedNode = trvGuarantors.Nodes[0];
                                //Guarantors
                            }
                            
                                
                        }
                        break;
                    case "Remove":
                        {
                            
                                //_IsGuarantorModified = true;
                                RemoveGuarantor();
                            
                        }
                        break;
                    //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                    case "Select Guarantor":
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
                                oListControl.ControlHeader = "Patient Other Guarantors";
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

        private void fillStates()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                DataTable dtStates = new DataTable();
                string _sqlQuery = "SELECT distinct ST FROM CSZ_MST order by ST";
                oDB.Retrive_Query(_sqlQuery, out dtStates);
                oDB.Disconnect();

                if (dtStates != null)
                {
                    DataRow dr = dtStates.NewRow();
                    dr["ST"] = "";
                    dtStates.Rows.InsertAt(dr, 0);
                    dtStates.AcceptChanges();

                    cmbState.DataSource = dtStates;
                    cmbState.DisplayMember = "ST";
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

        }

        private void SetData()
        {
            try
            {
                if (oPatientGuarantors.Count != 0)
                {
                    for (int i = 0; i < oPatientGuarantors.Count; i++)
                    {
                        TreeNode oNode = new TreeNode();
                        oNode.Text = oPatientGuarantors[i].FirstName.Trim() + " " + oPatientGuarantors[i].MiddleName.Trim() + " " + oPatientGuarantors[i].LastName.Trim();
                        oNode.Tag = oPatientGuarantors[i];
                        oNode.ImageIndex = 1;
                        oNode.SelectedImageIndex = 1;
                      
                        if (oPatientGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Primary.GetHashCode())
                        {
                            oNode.ForeColor = Color.DarkRed;
                        }
                        else if (oPatientGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Secondary.GetHashCode())
                        {
                            oNode.ForeColor = Color.OrangeRed;
                        }
                        else if (oPatientGuarantors[i].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Tertiary.GetHashCode())
                        {
                            oNode.ForeColor = Color.ForestGreen;
                        }
                        else
                        {
                            oNode.ForeColor = Color.Black;
                        }

                        trvGuarantors.Nodes[0].Nodes.Add(oNode);
                        oNode = null;
                        //Code Added by Mayuri:20091202
                        //by default first node should be selected 
                        if (trvGuarantors.Nodes[0].Nodes.Count > 0)
                        {
                            trvGuarantors.HideSelection = false;
                            trvGuarantors.SelectedNode = trvGuarantors.Nodes[0].Nodes[0];
                            TreeViewEventArgs eArg = new TreeViewEventArgs(trvGuarantors.Nodes[0].Nodes[0]);
                            trvGuarantors_AfterSelect(null, eArg);
                        }
                    }
                }
                else
                {
                    // If no guarantor exist, set default type to primary.
                    rbPrimary.Checked = true;
                    chkCommercialGuarantor.Enabled = true;
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool AddGuarantor()
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
                PatientOtherContact oGuarantor = null;
                oNode = null;

                if (txtGIPatFName.Tag != null && Convert.ToString(txtGIPatFName.Tag) != "")
                {
                    oNode = trvGuarantors.Nodes[0].Nodes[Convert.ToInt32(txtGIPatFName.Tag)];
                    oGuarantor = (PatientOtherContact)oNode.Tag;

                    //_IsGuarantorModified is set to true if any info from this control is modified
                    if(_IsGuarantorModified == false) 
                        _IsGuarantorModified = IsModified(oGuarantor);

                    //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                    oGuarantor.IsDataModified = _IsGuarantorModified;
                    IsModify = true; 
                }
                else
                {
                    oNode = new TreeNode();
                    oNode.ImageIndex = 1;
                    oNode.SelectedImageIndex = 1; 

                    oGuarantor = new PatientOtherContact();
                }

                //Code added by SaiKrishna:2011-01-13(yyyy-mm-dd)
                //personal Guarantor
                if (chkCommercialGuarantor.Checked == false)
                {
                    oGuarantor.FirstName = txtGIPatFName.Text.Trim();
                    oGuarantor.MiddleName = txtGIMName.Text.Trim();
                    oGuarantor.LastName = txtGIPatLName.Text.Trim();
                    if (mskGIDOB.MaskCompleted == true)
                        oGuarantor.DOB = Convert.ToDateTime(mskGIDOB.Text);
                    else
                        oGuarantor.DOB = DateTime.MinValue;
                    if (mtxtGISSN.IsValidated == true)
                        oGuarantor.SSN = mtxtGISSN.Text;
                    else
                        oGuarantor.SSN = "";

                    if (radbtnGIMale.Checked == true)
                        oGuarantor.Gender = "Male";
                    else if (radbtnGIFemale.Checked == true)
                        oGuarantor.Gender = "Female";
                    else if (radbtnGIOthers.Checked == true)
                        oGuarantor.Gender = "Other";
                    oGuarantor.Relation = cmbGIRelation.Text;
                    oGuarantor.GurantorType = GuarantorType.Personal;
                }
                else //Commercial Guarantor
                {
                    oGuarantor.FirstName = txtCommercialName.Text.Trim();
                    oGuarantor.MiddleName = "";
                    oGuarantor.LastName = "";
                    oGuarantor.SSN = "";
                    oGuarantor.Gender = "";
                    oGuarantor.Relation = "";
                    oGuarantor.DOB = DateTime.MinValue;
                    oGuarantor.GurantorType = GuarantorType.Commercial;
                }
                

                DialogResult _dlgRst = DialogResult.None;
                PatientOtherContact _Guarantor = null;
                if (rbPrimary.Checked == true)
                {
                    if (trvGuarantors.Nodes[0].Nodes != null && trvGuarantors.Nodes[0].Nodes.Count > 0)
                    {
                        foreach (TreeNode oTreeNode in trvGuarantors.Nodes[0].Nodes)
                        {
                            _Guarantor = ((PatientOtherContact)oTreeNode.Tag);
                            if (_Guarantor != null)
                            {
                                if (_Guarantor.nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Primary.GetHashCode() &&
                                    oGuarantor != _Guarantor)
                                {
                                    _dlgRst = MessageBox.Show(this, "Primary Guarantor is already present.\n Do you want to replace with this Guarantor", _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                    if (_dlgRst == DialogResult.OK)
                                    {
                                        _Guarantor.nGuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Tertiary.GetHashCode();
                                        oTreeNode.ForeColor = Color.ForestGreen;
                                    }
                                    else
                                    { return false; }
                                    break;
                                }
                            }
                        }
                    }
                    oGuarantor.nGuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Primary.GetHashCode();
                    oNode.ForeColor = Color.DarkRed;
                }
                else if (rbSecondary.Checked == true)
                {
                    if (trvGuarantors.Nodes[0].Nodes != null && trvGuarantors.Nodes[0].Nodes.Count > 0)
                    {
                        foreach (TreeNode oTreeNode in trvGuarantors.Nodes[0].Nodes)
                        {
                            _Guarantor = ((PatientOtherContact)oTreeNode.Tag);
                            if (_Guarantor != null)
                            {
                                if (_Guarantor.nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Secondary.GetHashCode()
                                    && oGuarantor != _Guarantor)
                                {
                                    _dlgRst = MessageBox.Show(this, "Secondary Guarantor is already present.\n Do you want to replace with this Guarantor", _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                    if (_dlgRst == DialogResult.OK)
                                    {
                                        _Guarantor.nGuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Tertiary.GetHashCode();
                                        oTreeNode.ForeColor = Color.ForestGreen;
                                    }
                                    else
                                    { return false; }
                                    break;
                                }
                            }
                        }
                    }
                    oGuarantor.nGuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Secondary.GetHashCode();
                    oNode.ForeColor = Color.OrangeRed;
                }
                else if (rbTertiary.Checked == true)
                {
                    oGuarantor.nGuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Tertiary.GetHashCode();
                    oNode.ForeColor = Color.ForestGreen;
                }
                else
                {
                    oGuarantor.nGuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Inactive.GetHashCode();
                    oNode.ForeColor = Color.Black;
                }

                //Sandip Darade 20091009 gloAddress control implemented  replacing code for address info above with code below 

                oGuarantor.AddressLine1 = oAddresscontrol.txtAddress1.Text.Trim();
                oGuarantor.AddressLine2 = oAddresscontrol.txtAddress2.Text.Trim();
                oGuarantor.City = oAddresscontrol.txtCity.Text.Trim();
                oGuarantor.State = oAddresscontrol.cmbState.Text.Trim();
                oGuarantor.Zip = oAddresscontrol.txtZip.Text.Trim();
                oGuarantor.County = oAddresscontrol.txtCounty.Text.Trim();
                oGuarantor.Country = oAddresscontrol.cmbCountry.Text.Trim();
    
                oGuarantor.Phone = mskGIPhone.Text;
                oGuarantor.Mobile = mskGIMobile.Text;
                oGuarantor.Email = txtGIEmail.Text.Trim();
                oGuarantor.Fax = txtGIFax.Text.Trim();

                // New guarantor
                if (IsModify == false)
                {
                    oGuarantor.OtherConatctType = PatientOtherContactType.Guarantor;
                    oGuarantor.GuarantorAsPatientID = 0;
                    //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                    oGuarantor.PatientContactID = 0;
                    oGuarantor.IsActive = true;
                    if (FromAccountGuarantor == true)
                    {
                        oGuarantor.IsAccountGuarantor = true;
                    }
                }
                else
                {
                    // Do not modify these properties keep them as it is 
                }

                oNode.Text = oGuarantor.FirstName.Trim() + " " + oGuarantor.MiddleName.Trim()  + " " + oGuarantor.LastName.Trim() ;
                oNode.Tag = oGuarantor;

                if (IsModify == false)
                {
                    //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                    if (trvGuarantors.Nodes[0].Nodes.Count == 0)
                    {
                        trvGuarantors.Nodes[0].Nodes.Add(oNode);
                    }
                    else
                    {
                        if (trvGuarantors.Nodes[0].Nodes.Count != 0 && FromAccountGuarantor == true)
                        {
                            if (_validationFlag == true)
                            {
                                MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _validationFlag = false;
                                return false;
                            }
                        }
                        else
                        {
                            trvGuarantors.Nodes[0].Nodes.Add(oNode);
                        }
                    }

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

        private void RemoveGuarantor()
        {
            try
            {
                if (trvGuarantors.SelectedNode != null)
                {
                    if (trvGuarantors.SelectedNode.Level != 0)
                    {
                        //Code Added by Mayuri:20091202
                        //To Fix issue-#353-Display message before removing node
                        if (FromAccountGuarantor == false)
                        {
                            DialogResult res = MessageBox.Show("Are you sure you want to remove selected guarantor? ", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (res == DialogResult.Yes)
                            {
                                _IsGuarantorDeleted = true;
                                ClearGuarantorDetails();
                                trvGuarantors.SelectedNode.Remove();
                                if (trvGuarantors.Nodes[0].Nodes.Count > 0)
                                {
                                    trvGuarantors.HideSelection = false;
                                    trvGuarantors.SelectedNode = trvGuarantors.Nodes[0].Nodes[0];
                                    TreeViewEventArgs eArg = new TreeViewEventArgs(trvGuarantors.Nodes[0].Nodes[0]);
                                    trvGuarantors_AfterSelect(null, eArg);
                                }
                            }
                        }
                        else
                        {
                            DialogResult res = MessageBox.Show("Are you sure you want to remove selected guarantor? ", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (res == DialogResult.Yes)
                            {

                                PatientOtherContact oSelectedGuarantor = null;
                                oSelectedGuarantor = (PatientOtherContact)trvGuarantors.SelectedNode.Tag;
                                if (oSelectedGuarantor.PatientContactID != 0)
                                {
                                    bool IsTransExist = false;
                                    IsTransExist = objgloAccount.CheckTransactionsExistForAccountGuarantor(oSelectedGuarantor.PAccountID, oSelectedGuarantor.PatientContactID);

                                    if (IsTransExist == true)
                                    {
                                        DialogResult result = MessageBox.Show("Selected guarantor can not be deleted because associated with transactions.Do you want to continue to remove", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                        if (result == DialogResult.Yes)
                                        {
                                            //objgloAccount.DeactivateAccountGuarantor(oSelectedGuarantor.PAccountID, oSelectedGuarantor.PatientContactID);
                                            _IsGuarantorDeleted = true;
                                            ClearGuarantorDetails();
                                            trvGuarantors.SelectedNode.Remove();
                                            if (trvGuarantors.Nodes[0].Nodes.Count > 0)
                                            {
                                                trvGuarantors.HideSelection = false;
                                                trvGuarantors.SelectedNode = trvGuarantors.Nodes[0].Nodes[0];
                                                TreeViewEventArgs eArg = new TreeViewEventArgs(trvGuarantors.Nodes[0].Nodes[0]);
                                                trvGuarantors_AfterSelect(null, eArg);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _IsGuarantorDeleted = true;
                                        ClearGuarantorDetails();
                                        trvGuarantors.SelectedNode.Remove();
                                        if (trvGuarantors.Nodes[0].Nodes.Count > 0)
                                        {
                                            trvGuarantors.HideSelection = false;
                                            trvGuarantors.SelectedNode = trvGuarantors.Nodes[0].Nodes[0];
                                            TreeViewEventArgs eArg = new TreeViewEventArgs(trvGuarantors.Nodes[0].Nodes[0]);
                                            trvGuarantors_AfterSelect(null, eArg);
                                        }
                                    }
                                }
                                else
                                {
                                    _IsGuarantorDeleted = true;
                                    ClearGuarantorDetails();
                                    trvGuarantors.SelectedNode.Remove();
                                    if (trvGuarantors.Nodes[0].Nodes.Count > 0)
                                    {
                                        trvGuarantors.HideSelection = false;
                                        trvGuarantors.SelectedNode = trvGuarantors.Nodes[0].Nodes[0];
                                        TreeViewEventArgs eArg = new TreeViewEventArgs(trvGuarantors.Nodes[0].Nodes[0]);
                                        trvGuarantors_AfterSelect(null, eArg);
                                    }
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

        public bool GetData()
        {
            try
            {
                oPatientGuarantors.Clear();
                if (trvGuarantors.Nodes[0].Nodes.Count > 0)
                {
                    for (int i = 0; i < trvGuarantors.Nodes[0].Nodes.Count; i++)
                    {
                        PatientOtherContact oGurantor = (PatientOtherContact)trvGuarantors.Nodes[0].Nodes[i].Tag;
                        oPatientGuarantors.Add(oGurantor);
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

        private void ClearGuarantorDetails()
        {
            //Added by Sai Krishna for PAF 2011-05-09(yyyy-mm-dd)
            txtCommercialName.Text = "";
            txtGIPatFName.Text = "";
            txtGIMName.Text = "";
            txtGIPatLName.Text = "";
            mskGIDOB.Text = "";
            mtxtGISSN.Text = ""; 
            radbtnGIMale.Checked = true;
            
            oAddresscontrol.txtAddress1.Text = "";
            oAddresscontrol.txtAddress2.Text = "";
            oAddresscontrol.isFormLoading = true;
            oAddresscontrol.txtZip.Text = "";
            oAddresscontrol.isFormLoading = false;
            oAddresscontrol.txtCity.Text = "";
            oAddresscontrol.cmbState.Text = "";
            oAddresscontrol.txtCounty.Text = "";

            mskGIPhone.Text = "";
            mskGIMobile.Text = "";
            txtGIEmail.Text = "";
            txtGIFax.Text = "";
            cmbGIRelation.Text = "";

            radbtnGIMale.Checked = false;
            radbtnGIFemale.Checked = false;
            radbtnGIOthers.Checked = false;

            rbTertiary.Checked = true;
            _validationFlag = true; 
        }

        private bool ValidateData()
        {
            //******first name
            //Code modified by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
            if (chkCommercialGuarantor.Checked == false)
            {
                //******first name
                if (txtGIPatFName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter first name for the guarantor.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGIPatFName.Focus();
                    return false;
                }

                //******Last name
                if (txtGIPatLName.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter last name for the guarantor.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGIPatLName.Focus();
                    return false;
                }
            }
            else if (chkCommercialGuarantor.Checked == true)
            {
                if (string.IsNullOrEmpty(txtCommercialName.Text.ToString().Trim()))
                {
                    MessageBox.Show("Please enter Commercial guarantor.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCommercialName.Focus();
                    return false;
                }
            }
            
            if (mtxtGISSN.IsValidated == false)
            {
                return false;
            }
            if (txtGIFax .IsValidated == false)
            {
                return false;
            }
            //date of birth   
            mskGIDOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (mskGIDOB.Text.Length > 0 && mskGIDOB.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                if (mskGIDOB.MaskCompleted == true)
                {
                    try
                    {
                        mskGIDOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        if (IsValidDate(mskGIDOB.Text))
                        {
                            if (Convert.ToDateTime(mskGIDOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mskGIDOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(mskGIDOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                            {
                                MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskGIDOB.Focus();
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
            
            if (mskGIPhone.IsValidated == false)
            {
                return false;
            }
            
            if (mskGIMobile.IsValidated == false)
            {
                return false;
            }
            if (CheckEmailAddress(txtGIEmail.Text) == false)
            {
                MessageBox.Show("Please enter valid email id.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGIEmail.Focus();
                return false;
            }
            return true;
        }

        private bool IsModified(PatientOtherContact oGuarantor)
        {
            bool _Result = true;
            try
            {
                String guarantorfax = "";
                if (_IsInternetFax)
                {
                    if (oGuarantor.Fax.Contains("(") && oGuarantor.Fax.Contains(")") && oGuarantor.Fax.Contains("-"))
                    {
                        guarantorfax = oGuarantor.Fax.Replace("(", "").Replace(")","").Replace("-","").Replace(" ","");
                    }
                    else
                    {
                        guarantorfax = oGuarantor.Fax.Replace(" ", "");
                    }
                }
                else
                {
                    guarantorfax = oGuarantor.Fax.Replace(" ", "");
                }
                //..Sagar Ghodke - 20110825
                //..Code added to check if the guarantor details are changed, the mask controls are compared with the literals in them
                //..& the guarantor object was behaving in random fashion for having and not having literals in ssn,phone,mobile values
                //..so 1. if mask is completed made it to include literals before comparison
                //..2. if specially for ssn mask if the guarantor object having literals then only turned on the ssn mask literals
                //..Code changes for BUG#7965
                if (mtxtGISSN.MaskFull == true)
                {
                    if (oGuarantor.SSN.Contains("-"))
                    { mtxtGISSN.IncludeLiteralsAndPrompts = true; }
                }

                if (mskGIMobile.MaskFull == true)
                {
                    if (oGuarantor.Mobile.Contains("("))
                    { mskGIMobile.IncludeLiteralsAndPrompts = true; }
                }

                if (mskGIPhone.MaskFull == true)
                {
                    if (oGuarantor.Phone.Contains("("))
                    { mskGIPhone.IncludeLiteralsAndPrompts = true; }
                }


                //Check if Guarantor details are modified
                if (txtGIPatFName.Text == oGuarantor.FirstName
                    && txtGIMName.Text == oGuarantor.MiddleName
                    && txtGIPatLName.Text == oGuarantor.LastName
                    && mtxtGISSN.Text == oGuarantor.SSN
                    && (mskGIDOB.MaskCompleted && mskGIDOB.Text == oGuarantor.DOB.ToString("MM/dd/yyyy") || (mskGIDOB.Text == "  /  /" && oGuarantor.DOB == DateTime.MinValue))

                    && oAddresscontrol.txtAddress1.Text == oGuarantor.AddressLine1
                    && oAddresscontrol.txtAddress2.Text == oGuarantor.AddressLine2
                    && oAddresscontrol.txtCity.Text == oGuarantor.City
                    && oAddresscontrol.cmbState.Text == oGuarantor.State
                    && oAddresscontrol.txtZip.Text == oGuarantor.Zip

                    && mskGIPhone.Text == oGuarantor.Phone.Replace(" ","")
                    && mskGIMobile.Text == oGuarantor.Mobile.Replace(" ","")
                    && txtGIEmail.Text == oGuarantor.Email.Replace(" ","")
                    && txtGIFax.Text == guarantorfax.Replace(" ","")
                    && cmbGIRelation.Text == oGuarantor.Relation)
                {

                    //Check if Guarantor Gender is modified
                    if ((oGuarantor.Gender == "Male" && radbtnGIMale.Checked == true)
                         || (oGuarantor.Gender == "Female" && radbtnGIFemale.Checked == true)
                         || (oGuarantor.Gender == "Other" && radbtnGIOthers.Checked == true)
                         || (oGuarantor.Gender.ToString().Trim().Length == 0 && (radbtnGIMale.Checked == false && radbtnGIFemale.Checked == false && radbtnGIOthers.Checked == false))//for other guardian
                        )
                    {

                        //Check if Guarantor Flag is modified
                        if ((oGuarantor.nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Primary.GetHashCode() && rbPrimary.Checked == true)
                            || (oGuarantor.nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Secondary.GetHashCode() && rbSecondary.Checked == true)
                            || (oGuarantor.nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Tertiary.GetHashCode() && rbTertiary.Checked == true)
                            || (oGuarantor.nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Inactive.GetHashCode() && rbInactive.Checked == true)
                           )
                        {
                            _Result = false;
                        }
                    }
                }
                if ((oGuarantor.GurantorType == GuarantorType.Personal && chkCommercialGuarantor.Checked == true) || (oGuarantor.GurantorType == GuarantorType.Commercial && chkCommercialGuarantor.Checked == false))
                    _Result = true;

                if (oGuarantor.GurantorType == GuarantorType.Commercial && txtCommercialName.Text != oGuarantor.FirstName)
                    _Result = true;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                mtxtGISSN.IncludeLiteralsAndPrompts = false;
                mskGIMobile.IncludeLiteralsAndPrompts = false;
                mskGIPhone.IncludeLiteralsAndPrompts = false;
            }
            return _Result; 
        }

        //Added By MaheshB

        private void fillRelationShip()
        {

            // To Fill The Relationships
            RelationShip oRelationShip = new RelationShip(_databaseconnectionstring);
            DataTable dtRelation = new DataTable();
            dtRelation = oRelationShip.GetList();
            if (dtRelation != null)
            {
                DataRow dr = dtRelation.NewRow();
                dr["nPatientRelID"] = "0";
                dr["sRelationshipDesc"] = "";
                dtRelation.Rows.InsertAt(dr, 0);
                dtRelation.AcceptChanges();

                cmbGIRelation.DataSource = dtRelation;
                cmbGIRelation.ValueMember = dtRelation.Columns["nPatientRelID"].ColumnName;
                cmbGIRelation.DisplayMember = dtRelation.Columns["sRelationshipDesc"].ColumnName;
                //_SubscriberRelationShip = cmbRelationShip.Text;
                if (dtRelation.Rows.Count > 0)
                {
                    cmbGIRelation.SelectedIndex = 0;
                }

            }
            //


        }

        #endregion

        private void trvGuarantors_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            //Code added on 20091112
            //Fix issue:#0000218-Pat Reg>Guarantor>Error message is displayed multiple times on clicking any text on left panel after entering only one mandatory field.
            if (e.Node.Level != 0)
            {
                if (txtGIPatFName.Text.Trim() != "")
                {
                    if (AddGuarantor() == false)
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
                    PatientOtherContact oGuarantor = (PatientOtherContact)e.Node.Tag;
                    //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                    ClearGuarantorDetails();
                    txtGIPatFName.Tag = e.Node.Index;
                    //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                    chkCommercialGuarantor.Enabled = false;
                    if (oGuarantor.GurantorType == GuarantorType.Personal)
                    {
                        chkCommercialGuarantor.Checked = false;
                        pnlPersonalGuarantor.Visible = true;
                        pnlCommercialGuarantor.Visible = false;

                        txtGIPatFName.Text = oGuarantor.FirstName.Trim();
                        txtGIMName.Text = oGuarantor.MiddleName.Trim();
                        txtGIPatLName.Text = oGuarantor.LastName.Trim();
                        mtxtGISSN.Text = oGuarantor.SSN;
                        //Commented by Mayuri:20091112
                        //To fix issue:#0000225-County field display as blank
                        //txtGICounty.Text = oGuarantor.County;

                        if (oGuarantor.DOB != null && oGuarantor.DOB != DateTime.MinValue)
                        {
                            mskGIDOB.Text = oGuarantor.DOB.ToString("MM/dd/yyyy");
                        }

                        radbtnGIMale.Checked = false;
                        radbtnGIFemale.Checked = false;
                        radbtnGIOthers.Checked = false;
                        
                        if (oGuarantor.Gender == "Male")
                            radbtnGIMale.Checked = true;
                        else if (oGuarantor.Gender == "Female")
                            radbtnGIFemale.Checked = true;
                        else if (oGuarantor.Gender == "Other")
                            radbtnGIOthers.Checked = true;
                    }
                    else if (oGuarantor.GurantorType == GuarantorType.Commercial)
                    {
                        chkCommercialGuarantor.Checked = true;
                        pnlCommercialGuarantor.Visible = true;
                        pnlPersonalGuarantor.Visible = false;
                        txtCommercialName.Text = oGuarantor.FirstName.Trim();
                    }

                    if (oGuarantor.nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Primary.GetHashCode())
                    {
                        rbPrimary.Checked = true;  
                    }
                    else if (oGuarantor.nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Secondary.GetHashCode())
                    {
                        rbSecondary.Checked = true;  
                    }
                    else if (oGuarantor.nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Tertiary.GetHashCode())
                    {
                        rbTertiary.Checked = true;  
                    }
                    else
                    {
                        rbInactive.Checked = true;  
                    }

                    //Sandip Darade 20091008
                    //Above code for address replaced by code below as we now use addrescontrol 
                    oAddresscontrol.txtAddress1.Text   = oGuarantor.AddressLine1;
                    oAddresscontrol.txtAddress2.Text = oGuarantor.AddressLine2;
                    oAddresscontrol.isFormLoading = true;
                    oAddresscontrol.txtZip.Text = oGuarantor.Zip;
                    oAddresscontrol.isFormLoading = false;
                    oAddresscontrol.txtCity.Text = oGuarantor.City;
                    oAddresscontrol.cmbCountry.Text = oGuarantor.Country;
                    
                    oAddresscontrol.cmbState.Text = oGuarantor.State;
                    oAddresscontrol.txtCounty.Text = oGuarantor.County;
                    //Sanjog - Added on 20111 Jan 19 to show masking no
                    mskGIPhone.Text = oGuarantor.Phone.ToString().Replace(" ","");
                    mskGIMobile.Text = oGuarantor.Mobile.ToString().Replace(" ", "");
                    txtGIEmail.Text = oGuarantor.Email.Trim();
                    txtGIFax.Text = oGuarantor.Fax.ToString().Replace(" ", "").Trim();
                    cmbGIRelation.Text = oGuarantor.Relation;
                    //Sanjog - Added on 20111 Jan 19 to show masking no
                }
                else
                {
                    txtGIPatFName.Tag = null;
                    ClearGuarantorDetails();
                    //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
                    chkCommercialGuarantor.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void txtGIZip_Leave(object sender, EventArgs e)
        {
            if (txtGIZip.Text.Trim() != "" && Regex.IsMatch(txtGIZip.Text.Trim(), @"^[0-9]+$") == true)
            {
                DataTable dt = new System.Data.DataTable();
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                try
                {
                    oDb.Connect(false);
                    string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = " + txtGIZip.Text.Trim() + "";
                    oDb.Retrive_Query(qry, out dt);
                    if (dt != null && dt.Rows.Count > 0 )
                    {
                        cmbState.Text = Convert.ToString(dt.Rows[0]["ST"]);
                        if (txtGICity.Text.Trim() == "")
                            txtGICity.Text =Convert.ToString(dt.Rows[0]["City"]);

                        txtGICounty.Text = Convert.ToString(dt.Rows[0]["County"]);
                        cmbGICountry.Text = "US"; 
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
                    if (dt != null)
                    {
                        dt.Dispose();
                    }
                    if (oDb != null)
                    {
                        oDb.Disconnect();
                        oDb.Dispose();
                    }
                }
            }
        }

        private void txtGIZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!(e.KeyChar == Convert.ToChar(8)))
                {
                    if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
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

        private void txtPAEmail_Validating(object sender, CancelEventArgs e)
        {
            if (CheckEmailAddress(txtGIEmail.Text) == false)
            {
                MessageBox.Show("Please enter a valid email id.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        #endregion

        #region "Designer Events"
		        //event to change buttons color on MouseOver 
        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongYellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }
        //event to change buttons color on MouseLeave 
        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_Button;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void tsb_MouseHover(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongYellow;
            ((ToolStripButton)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void tsb_MouseLeave(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).BackgroundImage = null;
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
                radbtnGIMale.Font = gloGlobal.clsgloFont.gFont_BOLD ;//new Font("Tahoma", 9, FontStyle.Bold);
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
                radbtnGIFemale.Font = gloGlobal.clsgloFont.gFont_BOLD ;//new Font("Tahoma", 9, FontStyle.Bold);
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
                radbtnGIOthers.Font = gloGlobal.clsgloFont.gFont_BOLD ;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                radbtnGIOthers.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbPrimary_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPrimary.Checked == true)
            {
                rbPrimary.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbPrimary.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbSecondary_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSecondary.Checked == true)
            {
                rbSecondary.Font = gloGlobal.clsgloFont.gFont_BOLD ;//  new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbSecondary.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbTertiary_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTertiary.Checked == true)
            {
                rbTertiary.Font = gloGlobal.clsgloFont.gFont_BOLD  ;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbTertiary.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInactive.Checked == true)
            {
                rbInactive.Font = gloGlobal.clsgloFont.gFont_BOLD ;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {
                rbInactive.Font = gloGlobal.clsgloFont.gFont ;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void pnlTreeView_Paint(object sender, PaintEventArgs e)
        {

        }
        //Added by Sai Krishna for PAF 2011-06-27(yyyy-mm-dd)
        #region "Personal and Commercial Guarantor CheckBox"

        private void chkCommercialGuarantor_CheckedChanged(object sender, EventArgs e)
        {
            //Indicates Commercial Guarantor
            if (chkCommercialGuarantor.Checked == true)
            {
                pnlCommercialGuarantor.Visible = true;
                pnlPersonalGuarantor.Visible = false;
                panel3.Height = 114;
            
                //pnlGIPersonalDetails.Width = 524;
                //pnlGIPersonalDetails.Height = 114;
            }
            else //Indicates Personal Guarantor
            {
                pnlPersonalGuarantor.Visible = true;
                pnlCommercialGuarantor.Visible = false;
                panel3.Height = 30;
                //pnlGIPersonalDetails.Width = 524;
                //pnlGIPersonalDetails.Height = 194;
            }
        }

        #endregion

        #region "Select OtherPatientGuarantor as AccountGuarantor"

        private void oListControl_SelectedClick(object sender, EventArgs e)
        {
            try
            {

                if (oListControl.SelectedItems.Count > 0)
                {
                    if (oPatientGuarantors.Count == 1)
                    {
                        MessageBox.Show("Multiple guarantors per patient account is not allowed.\nTo change guarantor remove existing guarantor.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        //Represents guarantorId
                        Int64 guarantorId = Convert.ToInt64(oListControl.SelectedItems[0].Code);
                        DataTable dtGuarantor = objgloAccount.GetGuarantorDetailsById(guarantorId);

                        if (dtGuarantor != null && dtGuarantor.Rows.Count > 0)
                        {
                            PatientOtherContact oGuarantor = new PatientOtherContact();
                            oGuarantor.GuarantorAsPatientID = Convert.ToInt64(dtGuarantor.Rows[0]["nGuarantorAsPatientID"].ToString());
                            oGuarantor.PatientContactID = Convert.ToInt64(dtGuarantor.Rows[0]["nPatientContactID"].ToString());
                            oGuarantor.IsActive = true;
                            oGuarantor.FirstName = dtGuarantor.Rows[0]["sFirstName"].ToString();
                            oGuarantor.MiddleName = dtGuarantor.Rows[0]["sMiddleName"].ToString();
                            oGuarantor.LastName = dtGuarantor.Rows[0]["sLastName"].ToString();
                            if (dtGuarantor.Rows[0]["nDOB"] != null && dtGuarantor.Rows[0]["nDOB"].ToString() != "")
                            {
                                oGuarantor.DOB = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtGuarantor.Rows[0]["nDOB"]));
                            }
                            oGuarantor.SSN = dtGuarantor.Rows[0]["sSSN"].ToString();
                            oGuarantor.Gender = dtGuarantor.Rows[0]["sGender"].ToString();
                            oGuarantor.Relation = dtGuarantor.Rows[0]["sRelation"].ToString();
                            oGuarantor.AddressLine1 = dtGuarantor.Rows[0]["sAddressLine1"].ToString();
                            oGuarantor.AddressLine2 = dtGuarantor.Rows[0]["sAddressLine2"].ToString();
                            oGuarantor.City = dtGuarantor.Rows[0]["sCity"].ToString();
                            oGuarantor.State = dtGuarantor.Rows[0]["sState"].ToString();
                            oGuarantor.Zip = dtGuarantor.Rows[0]["sZIP"].ToString();
                            oGuarantor.Country = dtGuarantor.Rows[0]["sCountry"].ToString();
                            oGuarantor.County = dtGuarantor.Rows[0]["sCounty"].ToString();
                            oGuarantor.Phone = dtGuarantor.Rows[0]["sPhone"].ToString();
                            oGuarantor.Mobile = dtGuarantor.Rows[0]["sMobile"].ToString();
                            oGuarantor.Email = dtGuarantor.Rows[0]["sEmail"].ToString();
                            oGuarantor.Fax = dtGuarantor.Rows[0]["sFax"].ToString();

                            oGuarantor.OtherConatctType = (PatientOtherContactType)Convert.ToInt32(dtGuarantor.Rows[0]["nPatientContactType"]);
                            oGuarantor.GurantorType = (GuarantorType)Convert.ToInt32(dtGuarantor.Rows[0]["nGuarantorType"]);
                            oGuarantor.nGuarantorTypeFlag = Convert.ToInt32(dtGuarantor.Rows[0]["nPatientContactTypeFlag"].ToString());
                            oGuarantor.IsAccountGuarantor = true;

                            if (oPatientGuarantors.Count == 0)
                            {
                                this.oPatientGuarantors.Add(oGuarantor);
                                SaveButton_Click(sender, e);
                            }
                            else
                            {
                                if (oPatientGuarantors.Count >= 1)
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

        #endregion

        private void mskGIDOB_Validating(object sender, CancelEventArgs e)
        {
            mskGIDOB.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (mskGIDOB.Text.Length > 0 && mskGIDOB.MaskCompleted == false)
            {
                MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {
                if (mskGIDOB.MaskCompleted == true)
                {
                    try
                    {
                        mskGIDOB.TextMaskFormat = MaskFormat.IncludeLiterals;
                        if (IsValidDate(mskGIDOB.Text))
                        {
                            if (Convert.ToDateTime(mskGIDOB.Text.Trim()) == DateTime.MinValue || Convert.ToDateTime(mskGIDOB.Text).Date > DateTime.Now.Date || Convert.ToDateTime(mskGIDOB.Text.Trim()) < Convert.ToDateTime("01/01/1900"))
                            {
                                MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskGIDOB.Focus();
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Enter a valid date of birth.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                    catch (Exception)// ex)
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
                if (oPatientGuarantors != null) { oPatientGuarantors.Dispose(); }
                if (oAddresscontrol != null) { oAddresscontrol.Dispose(); }
                if (oListControl != null) { oListControl.Dispose(); }
                if (objgloAccount != null) { objgloAccount.Dispose(); }             
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
    }
}
