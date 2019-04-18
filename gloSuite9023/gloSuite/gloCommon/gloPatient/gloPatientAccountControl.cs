
#region " Namespaces "

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloAddress;
using gloPatient.Classes;
using gloListControl;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlClient;

#endregion

namespace gloPatient
{
    public partial class gloPatientAccountControl : UserControl
    {

        #region " C1 Constants "

        //Added By Mahesh Satlapalli (Apollo)
        private const int COL_AccountPatientId = 0;
        private const int COL_PAccountId = 1;
        private const int COL_PatientId = 2;
        private const int COL_AccountNo = 3;
        private const int COL_PatientCode = 4;
        private const int COL_FirstName = 5;
        private const int COL_MiddleName = 6;
        private const int COL_LastName = 7;
        private const int COL_SSNNO = 8;
        private const int COL_PatientDOB = 9;
        private const int COL_Status = 10;
        private const int COL_OwnAccount = 11;
        private const int COL_PatientDisplayName = 12;

        #endregion

        #region " Variables "

        //Added By Mahesh Satlapalli (Apollo)
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        string _MessageBoxCaption = string.Empty;
        string _databaseconnectionstring = "", sGuarantorCode = "";

        //Added by SaiKrishna 
        Int64 _nAccountId;
        Int64 _nPatientId;
        Int64 _nGuarantorId;
      //  Int64 _nExistAccountGuarantorId;
        Int64 nClinicId = 1;
        DateTime dtAccountCloseDate;
        public Int64 _nExistAccountId;
        public bool _IsOwnAccount = true;
        bool IsCmbSameAsGuardianLoadFlag = true;

        //comment by mahesh s on 03-may-2011 , this functionality moved to tools.
        //public bool IsMergeAccountFeature = false;

        DataTable dtAccountPatient = new DataTable();

        gloListControl.gloListControl oListControl = null;
        gloGeneralItem.gloItems _selectedItemsColl;
        Account oAccount;
        PatientAccount oPatientAccount;
        IList<PatientAccount> oListAccountPatient;
        //gloPatientGuarantorControl ogloPatientGuarantorControl = null;
        gloPAGuarantorControl  ogloPatientGuarantorControl = null;
        PatientOtherContacts oPatientGuarantors = null;

        //Added by Saikrishna
        gloAddressControl oAddressControl = null;
        PatientGuardian oPatientGuardian = null;
        PatientDemographics oPatientDemographicsDetails = null;
       
        gloAccount objgloAccount = null;
        ToolTip oToolTip1 = new ToolTip();
        private Int64 nBusinessCenterID = 0;


        ComboBox combo;

        #endregion
       
        #region " Properties "

        //Code Added by SaiKrishna 
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
       
        #endregion

        #region " Delegates "

        //Added By mahesh S (apollo) : for pnlTop show in Parent form.
        public delegate void onPatientAccountControlEnter(object sender, EventArgs e);
        public event onPatientAccountControlEnter onPatientAccountControl_Enter;

        //Added By mahesh S (apollo) : for pnlTop hide in Parent form.
        public delegate void onPatientAccountControlLeave(object sender, EventArgs e);
        public event onPatientAccountControlLeave onPatientAccountControl_Leave;

        ////comment by mahesh s on 03-may-2011 , this functionality moved to tools.
        ////Added By Mahesh S(Apollo) : for MergeButton show.
        //public delegate void mergeAccountChecked(object sender, EventArgs e);
        //public event mergeAccountChecked mergeAccountChecked_Clicked;

        ////Added By Mahesh S(Apollo) : for MergeButton Hide.
        //public delegate void mergeAccountUnChecked(object sender, EventArgs e);
        //public event mergeAccountUnChecked mergeAccountUnChecked_Clicked;

        #endregion

        #region " Constructors "

        //Added By Mahesh Satlapalli (Apollo)

        //Empty constructor
        public gloPatientAccountControl()
        {
           
            InitializeComponent();
          
        }

        //Connection string need to pass.
        public gloPatientAccountControl(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;

            cmbBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
           
        }

        //PatientId, GuarantorID and AccountIDs assigned to local variables.
        public gloPatientAccountControl(string databaseconnectionstring, Int64 patientId, Int64 guarantorId, Int64 accountId)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            oPatientGuarantors = new PatientOtherContacts();
            PatientGuardianDetails = new PatientGuardian(databaseconnectionstring);
            oPatientDemographicsDetails = new PatientDemographics();
            _nAccountId = accountId;
            _nPatientId = patientId;
            _nGuarantorId = guarantorId;
            objgloAccount = new gloAccount(databaseconnectionstring);

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

            cmbBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
           
        }

        #endregion

        #region " Form Events "

        private void gloPatientAccountControl_Load(object sender, EventArgs e)
        {
            try
            {
                oAddressControl = new gloAddressControl(_databaseconnectionstring);
                oAddressControl.Dock = DockStyle.Fill;
                oAddressControl.Name = "AddressControl";
                pnlAddresssControl.Controls.Add(oAddressControl);
                
                //comment by mahesh s on 03-may-2011 , this functionality moved to tools.
                //pnlMergeAccount.Visible = false;

                Boolean _IsRequireBusinessCenterOnPAccounts = gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_PatientAccount");
                if (_IsRequireBusinessCenterOnPAccounts)
                {
                    pnlBusinessCenter.Visible = true;
                    //FillBusinessCenter();
                    //Int64 _DefaultBusinessCenter = gloGlobal.gloPMGlobal.GetDefaultBusinessCenterForUser(gloGlobal.gloPMGlobal.UserID);
                    //if (_DefaultBusinessCenter != 0)
                    //{
                    //    cmbBusinessCenter.SelectedValue = _DefaultBusinessCenter;
                    //}
                }
                else
                {
                    pnlBusinessCenter.Visible = false;
                }

                SetData();
                oToolTip1.SetToolTip(btnNewGuarantor, "Add Guarantor");

                //If it is existing account 
                if (_IsOwnAccount == false)
                {
                    btnNewGuarantor.Visible = false;
                    btnGuarantorExistingPatientBrowse.Visible = false;
                    btnGuarantorClear.Visible = false;
                    btnGuarantorClear.Visible = false;

                    //comment by mahesh s on 03-may-2011 , this functionality moved to tools.
                    //chkMergeAccount.Enabled = false;

                    cmbSameAsGuardian.Enabled = false;
                    btnAddPatient.Enabled = false;
                    btnPatientDeactivate.Enabled = false;
                }

                //comment by mahesh s on 03-may-2011 , this functionality moved to tools.
                //Check/Verify MergeAccount Feature enable or not.
                //IsMergeAccountFeature = objgloAccount.GetMergeAccountFeatureSetting();
                //if(IsMergeAccountFeature == true)
                //   chkMergeAccount.Visible = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
       
        #endregion

        #region " Button events "

        //Added By Mahesh Satlapalli(Apollo)

        //for add Patients To Patient Grid.
        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            try
            {
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, true, true, this.Width);
                oListControl.ControlHeader = "Patients";
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_PatientSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                this.Controls.Add(oListControl);

                //To allow the user to add multiple guarantors at one time 
                for (Int32 iPatRow = 1; iPatRow < gvPatient.Rows.Count; iPatRow++)
                {
                    //Added by SaiKrishna:2011-01-06(yyyy-mm-dd) For Existing patient as guarantor(based on patienid)
                    if (Convert.ToInt64(gvPatient.Rows[iPatRow][COL_PatientId].ToString()) > 0)
                        oListControl.SelectedItems.Add(Convert.ToInt64(gvPatient.Rows[iPatRow][COL_PatientId].ToString()), gvPatient.Rows[iPatRow][COL_FirstName].ToString());
                }

                oListControl.OpenControl();
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                }
                onPatientAccountControl_Leave(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            
        }

        //for change the "btnPatientDeactivate" Text based on selected patient status in PatientGrid.
        private void gvPatient_Click(object sender, EventArgs e)
        {
            if (gvPatient.Rows[gvPatient.RowSel][COL_Status].ToString().ToLower() == "deactive")
            {
                btnPatientDeactivate.Text = "Activate Patient";
                btnPatientDeactivate.Tag = "Activate Patient";
            }
            else
            {
                btnPatientDeactivate.Text = "Deactivate Patient";
                btnPatientDeactivate.Tag = "DeActivate Patient";
            }
        }

        //for change the selected Patient status in PatientGrid.
        private void btnPatientDeactivate_Click(object sender, EventArgs e)
        {
            if (btnPatientDeactivate.Text == "Deactivate Patient")
            {
                //Confirmation message when account have only one active patient.
                if (ConfirmDeactivateStatus())
                {
                    gvPatient.Rows[gvPatient.RowSel][COL_Status] = "DeActive";
                    btnPatientDeactivate.Text = "Activate Patient";
                    btnPatientDeactivate.Tag = "Activate Patient";
                }
            }
            else
            {
                gvPatient.Rows[gvPatient.RowSel][COL_Status] = "Active";
                btnPatientDeactivate.Text = "Deactivate Patient";
                btnPatientDeactivate.Tag = "Deactivate Patient";
            }
        }

        //comment by mahesh s on 03-may-2011 , this functionality moved to tools.
        //for Merge two Accounts into single Account.
        //private void chkMergeAccount_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkMergeAccount.Checked == true)
        //    {
        //        pnlMergeAccount.Visible = true;
        //        MergeControlsHideAndShow(false, true);
        //        mergeAccountChecked_Clicked(sender, e);
        //    }
        //    else
        //    {
        //        pnlMergeAccount.Visible = false;
        //        ClearMergeAccount();
        //        MergeControlsHideAndShow(true, false);
        //        mergeAccountUnChecked_Clicked(sender, e);
        //    }
        //}

        private void btnGuarantorClear_Click(object sender, EventArgs e)
        {
            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
            {
                DialogResult res = MessageBox.Show("Are you sure you want to remove selected guarantor? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {

                    PatientOtherContact oSelectedGuarantor = null;
                    oSelectedGuarantor = oPatientGuarantors[0];
                    if (oSelectedGuarantor.PatientContactID != 0)
                    {
                        bool IsTransExist = false;
                        IsTransExist = objgloAccount.CheckTransactionsExistForAccountGuarantor(oSelectedGuarantor.PAccountID, oSelectedGuarantor.PatientContactID);

                        if (IsTransExist == true)
                        {
                            DialogResult result = MessageBox.Show("Selected guarantor is associated with transactions. Do you want to continue to remove.", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (result == DialogResult.Yes)
                            {
                                //objgloAccount.DeactivateAccountGuarantor(oSelectedGuarantor.PAccountID, oSelectedGuarantor.PatientContactID);
                                oPatientGuarantors.Clear();
                            }
                        }
                        else
                        {
                            oPatientGuarantors.Clear();
                        }
                    }
                    else
                    {
                        oPatientGuarantors.Clear();
                    }
                }
                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                {
                    for (Int32 gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                    {
                        txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;

                        //showing selected gurantor address.
                        oAddressControl.isFormLoading = true;
                        oAddressControl.txtAddress1.Text = oPatientGuarantors[gindex].AddressLine1.Trim().ToString();
                        oAddressControl.txtAddress2.Text = oPatientGuarantors[gindex].AddressLine2.Trim().ToString();
                        oAddressControl.txtCity.Text = oPatientGuarantors[gindex].City.Trim().ToString();
                        oAddressControl.txtZip.Text = oPatientGuarantors[gindex].Zip.Trim().ToString();
                        oAddressControl.cmbState.Text = oPatientGuarantors[gindex].State.Trim().ToString();
                        oAddressControl.cmbCountry.Text = oPatientGuarantors[gindex].Country.Trim().ToString();
                        oAddressControl.txtCounty.Text = oPatientGuarantors[gindex].County.Trim().ToString();
                        oAddressControl.isFormLoading = false;
                        setCmbSameAsGuardianIndex();
                    }

                }
                else
                {
                    txtAccGuarantor.Text = "";
                    oAddressControl.isFormLoading = true;
                    oAddressControl.txtAddress1.Text = "";
                    oAddressControl.txtAddress2.Text = "";
                    oAddressControl.txtCity.Text = "";
                    oAddressControl.txtZip.Text = "";
                    oAddressControl.cmbState.Text = "";
                    oAddressControl.cmbCountry.Text = "";
                    oAddressControl.txtCounty.Text = "";
                    oAddressControl.isFormLoading = false;
                    IsCmbSameAsGuardianLoadFlag = false;
                    cmbSameAsGuardian.SelectedIndex = -1;
                    IsCmbSameAsGuardianLoadFlag = true;

                }
            }
        }

        #endregion

        #region " Guarantor related Events "

        //Code Added by SaiKrishna 
        private void btnGuarantorExistingPatientBrowse_Click(object sender, EventArgs e)
        {

            try
            {
                if (oListControl != null)
                {
                    for (Int32 iControlCnt = this.Controls.Count - 1; iControlCnt >= 0; iControlCnt--)
                    {
                        if (this.Controls[iControlCnt].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[iControlCnt]);
                            break;
                        }
                    }
                    try
                    {
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_GaurantorSelectedClick);
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

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, this.Width);
                oListControl.ControlHeader = "Guarantors";
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_GaurantorSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                this.Controls.Add(oListControl);

                //To allow the user to add multiple guarantors at one time 
                //for (Int32 _GuarantorCnt = 0; _GuarantorCnt < cmbGuarantor.Items.Count; _GuarantorCnt++)
                //{
                //    cmbGuarantor.SelectedIndex = _GuarantorCnt;
                //    //Existing patient as guarantor(based on patienid)
                //    if (Convert.ToInt64(cmbGuarantor.SelectedValue.ToString()) > 0)
                //        oListControl.SelectedItems.Add(Convert.ToInt64(cmbGuarantor.SelectedValue.ToString()), cmbGuarantor.Text);
                //}

                oListControl.OpenControl();

                //oListControl is disposed in OpenControl() Method if Zero records found
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                }
                onPatientAccountControl_Leave(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            if (oListControl != null)
            {
                for (Int32 iControlCnt = this.Controls.Count - 1; iControlCnt >= 0; iControlCnt--)
                {
                    if (this.Controls[iControlCnt].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[iControlCnt]);
                        break;
                    }
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_GaurantorSelectedClick);
                    }
                    catch
                    {
                    }


                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                }
                catch
                {
                }
                onPatientAccountControl_Enter(sender, e);
            }

        }

        private void oListControl_GaurantorSelectedClick(object sender, EventArgs e)
        {
            Int64 _TempPatientID = 0;
            gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);

            try
            {
                if (oListControl.SelectedItems.Count > 1)
                {
                    MessageBox.Show("Multiple guarantors per patient account are not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int32 _itemsCnt = 0; _itemsCnt <= oListControl.SelectedItems.Count - 1; _itemsCnt++)
                    {
                        _TempPatientID = Convert.ToInt64(oListControl.SelectedItems[_itemsCnt].ID);
                        bool _ShouldAdd = true;
                        for (Int32 _Count = 0; _Count < oPatientGuarantors.Count; _Count++)
                        {
                            if (Convert.ToInt64(oListControl.SelectedItems[_itemsCnt].ID) == oPatientGuarantors[_Count].GuarantorAsPatientID)
                            {
                                _ShouldAdd = false;
                                break;
                            }
                        }
                        if (_ShouldAdd == true)
                        {
                            Patient oPatientTemp = ogloPatient.GetPatientDemo(_TempPatientID);
                            if (oPatientTemp != null)
                            {
                                PatientOtherContact oGuarantor = new PatientOtherContact();
                                oGuarantor.GuarantorAsPatientID = _TempPatientID;
                                oGuarantor.PatientID = _nPatientId;
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
                                oGuarantor.Country = oPatientTemp.DemographicsDetail.PatientCounty;
                                oGuarantor.County = oPatientTemp.DemographicsDetail.PatientCounty;
                                oGuarantor.Phone = oPatientTemp.DemographicsDetail.PatientPhone;
                                oGuarantor.Mobile = oPatientTemp.DemographicsDetail.PatientMobile;
                                oGuarantor.Email = oPatientTemp.DemographicsDetail.PatientEmail;
                                oGuarantor.Fax = oPatientTemp.DemographicsDetail.PatientFax;
                                if (PatientDemographicDetails.PatientCode.Trim() != oPatientTemp.DemographicsDetail.PatientCode.Trim())
                                {
                                    oGuarantor.OtherConatctType = PatientOtherContactType.Patient;
                                }
                                else
                                {
                                    oGuarantor.OtherConatctType = PatientOtherContactType.SameAsPatient;
                                }
                                //Represents Personal Guarantor
                                oGuarantor.GurantorType = GuarantorType.Personal;
                                PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = GetNextTypeFlag(false);
                                oGuarantor.nGuarantorTypeFlag = _GuarantorTypeFlag.GetHashCode();
                                oGuarantor.IsAccountGuarantor = true;

                                if (oPatientGuarantors.Count == 0)
                                {
                                    oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account are not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        onPatientAccountControl_Enter(sender, e);
                                        return;
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
            finally
            {
                if(ogloPatient != null)
                ogloPatient.Dispose();
            }
            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
            {
                for (Int32 gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                {
                    txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;

                    oAddressControl.isFormLoading = true;
                    oAddressControl.txtAddress1.Text = oPatientGuarantors[gindex].AddressLine1.Trim().ToString();
                    oAddressControl.txtAddress2.Text = oPatientGuarantors[gindex].AddressLine2.Trim().ToString();
                    oAddressControl.txtCity.Text = oPatientGuarantors[gindex].City.Trim().ToString();
                    oAddressControl.txtZip.Text = oPatientGuarantors[gindex].Zip.Trim().ToString();
                    oAddressControl.cmbState.Text = oPatientGuarantors[gindex].State.Trim().ToString();
                    oAddressControl.cmbCountry.Text = oPatientGuarantors[gindex].Country.Trim().ToString();
                    oAddressControl.txtCounty.Text = oPatientGuarantors[gindex].County.Trim().ToString();
                    oAddressControl.isFormLoading = false;
                    setCmbSameAsGuardianIndex();
                }
            }
            onPatientAccountControl_Enter(sender, e);
        }

        private void btnNewGuarantor_Click(object sender, EventArgs e)
        {
            try
            {
                //ogloPatientGuarantorControl = new gloPatientGuarantorControl(_databaseconnectionstring);
                //ogloPatientGuarantorControl.PatientGuarantors = oPatientGuarantors;
                //ogloPatientGuarantorControl.PatientId = _nPatientId;
                //ogloPatientGuarantorControl.FromAccountGuarantor = true;
                //ogloPatientGuarantorControl.SaveButton_Click += new gloPatientGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                //ogloPatientGuarantorControl.CloseButton_Click += new gloPatientGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);
                //this.Controls.Add(ogloPatientGuarantorControl);
                //ogloPatientGuarantorControl.Dock = DockStyle.Fill;
                //ogloPatientGuarantorControl.BringToFront();
                //onPatientAccountControl_Leave(sender, e);
                if (ogloPatientGuarantorControl != null)
                {
                    ogloPatientGuarantorControl.Dispose();
                    ogloPatientGuarantorControl = null;
                }
                ogloPatientGuarantorControl = new gloPAGuarantorControl(_databaseconnectionstring);
                ogloPatientGuarantorControl.PatientGuarantors = oPatientGuarantors;
                ogloPatientGuarantorControl.PatientId = _nPatientId;
                ogloPatientGuarantorControl.FromAccountGuarantor = true;
                ogloPatientGuarantorControl.SaveButton_Click += new gloPAGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                ogloPatientGuarantorControl.CloseButton_Click += new gloPAGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);
                this.Controls.Add(ogloPatientGuarantorControl);
                ogloPatientGuarantorControl.Dock = DockStyle.Fill;
                ogloPatientGuarantorControl.BringToFront();
                onPatientAccountControl_Leave(sender, e);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            
        }

        private void ogloPatientGuarantorControl_SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Code changed by SaiKrishna:2010-12-31(yyyy-mm-dd) for patient account feature.
                oPatientGuarantors = ogloPatientGuarantorControl.PatientGuarantors;
                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                {
                    for (Int32 gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                    {
                        txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                        //showing selected gurantor address.
                        //fill Account Address with Selected Guarantor Address when it is not empty
                        if (oPatientGuarantors[gindex].AddressLine1.Trim().ToString() != "")
                        {
                            oAddressControl.isFormLoading = true;
                            oAddressControl.txtAddress1.Text = oPatientGuarantors[gindex].AddressLine1.Trim().ToString();
                            oAddressControl.txtAddress2.Text = oPatientGuarantors[gindex].AddressLine2.Trim().ToString();
                            oAddressControl.txtCity.Text = oPatientGuarantors[gindex].City.Trim().ToString();

                            oAddressControl.txtZip.Text = oPatientGuarantors[gindex].Zip.Trim().ToString();
                            oAddressControl.cmbState.Text = oPatientGuarantors[gindex].State.Trim().ToString();
                            oAddressControl.cmbCountry.Text = oPatientGuarantors[gindex].Country.Trim().ToString();
                            oAddressControl.txtCounty.Text = oPatientGuarantors[gindex].County.Trim().ToString();
                            oAddressControl.isFormLoading = false;
                        }
                        setCmbSameAsGuardianIndex();
                    }
                }
                else
                {
                    txtAccGuarantor.Text = "";

                    oAddressControl.isFormLoading = true;
                    oAddressControl.txtAddress1.Text = "";
                    oAddressControl.txtAddress2.Text = "";
                    oAddressControl.txtCity.Text = "";
                    oAddressControl.txtZip.Text = "";
                    oAddressControl.cmbState.Text = "";
                    oAddressControl.cmbCountry.Text = "";
                    oAddressControl.txtCounty.Text = "";
                    oAddressControl.isFormLoading = false;
                    IsCmbSameAsGuardianLoadFlag = false;
                    cmbSameAsGuardian.SelectedIndex = -1;
                    IsCmbSameAsGuardianLoadFlag = true;
                }
                this.Controls.Remove(ogloPatientGuarantorControl);
                try
                {
                    ogloPatientGuarantorControl.SaveButton_Click -= new gloPAGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                    ogloPatientGuarantorControl.CloseButton_Click -= new gloPAGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);
                }
                catch
                {
                }
                onPatientAccountControl_Enter(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ogloPatientGuarantorControl_CloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ogloPatientGuarantorControl.pnlAddresssControl.Controls.Count > 0)
                {
                    Control[] myControl = ogloPatientGuarantorControl.pnlAddresssControl.Controls.Find("GuarantorAddressControl", true);
                    if (myControl.Length > 0)
                    {
                        ((gloAddress.gloAddressControl)myControl[0]).ControlClosing = true;
                        ogloPatientGuarantorControl.pnlAddresssControl.Controls.Remove(myControl[0]);
                    }
                }

                this.Controls.Remove(ogloPatientGuarantorControl);
                try
                {
                    ogloPatientGuarantorControl.SaveButton_Click -= new gloPAGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                    ogloPatientGuarantorControl.CloseButton_Click -= new gloPAGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);
                }
                catch
                {
                }
                onPatientAccountControl_Enter(sender, e);
            }
            catch //(Exception ex)
            {

            }
        }

        private void cmbSameAsGuardian_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (IsCmbSameAsGuardianLoadFlag == true)
            {
                if (cmbSameAsGuardian.Text == "Patient")
                {
                    if (PatientDemographicDetails.PatientFirstName.ToString().Trim() != "" && PatientDemographicDetails.PatientLastName.ToString().Trim() != "")
                    {
                        bool _shouldAdd = true;
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            for (Int32 gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
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
                                oGuarantor.GuarantorAsPatientID = _nPatientId;
                                oGuarantor.IsActive = true;
                                oGuarantor.FirstName = PatientDemographicDetails.PatientFirstName.ToString().Trim();
                                oGuarantor.MiddleName = PatientDemographicDetails.PatientMiddleName.ToString().Trim();
                                oGuarantor.LastName = PatientDemographicDetails.PatientLastName.ToString().Trim();

                                oGuarantor.DOB = Convert.ToDateTime(PatientDemographicDetails.PatientDOB);
                                oGuarantor.SSN = PatientDemographicDetails.PatientSSN.ToString().Trim();
                                oGuarantor.Gender = PatientDemographicDetails.PatientGender.ToString().Trim();
                                //Added by Mayuri : 20151006-2.	Update Guarantor Address if Guarantor is “Same as Patient” when patient address is updated.
                                //Address
                                oGuarantor.AddressLine1 = oPatientDemographicsDetails.PatientAddress1.ToString().Trim();
                                oGuarantor.AddressLine2 = oPatientDemographicsDetails.PatientAddress2.ToString().Trim();
                                oGuarantor.City = oPatientDemographicsDetails.PatientCity.ToString().Trim();
                                oGuarantor.County = oPatientDemographicsDetails.PatientCounty.ToString().Trim();
                                oGuarantor.Zip = oPatientDemographicsDetails.PatientZip.ToString().Trim();
                                oGuarantor.State = oPatientDemographicsDetails.PatientState.ToString().Trim();
                                oGuarantor.Country = oPatientDemographicsDetails.PatientCountry.ToString().Trim();
                                
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
                                    oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account are not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        setCmbSameAsGuardianIndex();
                                        return;

                                    }
                                }
                        }
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            for (Int32 gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                            {
                                txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                               
                                //fill Account Address with Selected Guarantor Address when it is not empty
                                if (oPatientGuarantors[gindex].AddressLine1.Trim().ToString() != "")
                                {
                                    oAddressControl.isFormLoading = true;
                                    //Added by Mayuri : 20151006-2.	Update Guarantor Address if Guarantor is “Same as Patient” when patient address is updated.
                                    oAddressControl.txtAddress1.Text = oPatientDemographicsDetails.PatientAddress1.ToString().Trim();
                                    oAddressControl.txtAddress2.Text = oPatientDemographicsDetails.PatientAddress2.ToString().Trim();
                                    oAddressControl.txtCity.Text = oPatientDemographicsDetails.PatientCity.ToString().Trim();
                                    oAddressControl.cmbCountry.Text = oPatientDemographicsDetails.PatientCountry.ToString().Trim();
                                    oAddressControl.txtZip.Text  = oPatientDemographicsDetails.PatientZip.ToString().Trim();
                                    oAddressControl.cmbState.Text  = oPatientDemographicsDetails.PatientState.ToString().Trim();
                                    oAddressControl.txtCounty.Text  = oPatientDemographicsDetails.PatientCounty.ToString().Trim();
                                    oAddressControl.isFormLoading = false;
                                }
                                setCmbSameAsGuardianIndex();
                            }
                        }
                    }
                }
                if (cmbSameAsGuardian.Text == "Mother")
                {
                    if (PatientGuardianDetails.PatientMotherFirstName.Trim() != "" && PatientGuardianDetails.PatientMotherLastName.Trim() != "")
                    {
                        bool _shouldAdd = true;
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            for (Int32 gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
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
                                oPatientGuarantors.Add(oGuarantor);
                            }
                            else
                            {
                                if (oPatientGuarantors.Count >= 1)
                                {
                                    MessageBox.Show("Multiple guarantors per patient account are not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    setCmbSameAsGuardianIndex();
                                    return;
                                }
                            }
                        }
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            for (Int32 gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                            {
                                txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;

                                //fill Account Address with Selected Guarantor Address when it is not empty
                                if (oPatientGuarantors[gindex].AddressLine1.Trim().ToString() != "")
                                {
                                    oAddressControl.isFormLoading = true;
                                    oAddressControl.txtAddress1.Text = oPatientGuarantors[gindex].AddressLine1.Trim().ToString();
                                    oAddressControl.txtAddress2.Text = oPatientGuarantors[gindex].AddressLine2.Trim().ToString();
                                    oAddressControl.txtCity.Text = oPatientGuarantors[gindex].City.Trim().ToString();

                                    oAddressControl.txtZip.Text = oPatientGuarantors[gindex].Zip.Trim().ToString();
                                    oAddressControl.cmbState.Text = oPatientGuarantors[gindex].State.Trim().ToString();
                                    oAddressControl.cmbCountry.Text = oPatientGuarantors[gindex].Country.Trim().ToString();
                                    oAddressControl.txtCounty.Text = oPatientGuarantors[gindex].County.Trim().ToString();
                                    oAddressControl.isFormLoading = false;
                                }
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
                            for (Int32 gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
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
                                oPatientGuarantors.Add(oGuarantor);
                            }
                            else
                            {
                                if (oPatientGuarantors.Count >= 1)
                                {
                                    MessageBox.Show("Multiple guarantors per patient account are not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    setCmbSameAsGuardianIndex();
                                    return;
                                }
                            }
                        }
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            for (Int32 gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                            {
                                txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                //fill Account Address with Selected Guarantor Address when it is not empty
                                if (oPatientGuarantors[gindex].AddressLine1.Trim().ToString() != "")
                                {
                                    oAddressControl.isFormLoading = true;
                                    oAddressControl.txtAddress1.Text = oPatientGuarantors[gindex].AddressLine1.Trim().ToString();
                                    oAddressControl.txtAddress2.Text = oPatientGuarantors[gindex].AddressLine2.Trim().ToString();
                                    oAddressControl.txtCity.Text = oPatientGuarantors[gindex].City.Trim().ToString();

                                    oAddressControl.txtZip.Text = oPatientGuarantors[gindex].Zip.Trim().ToString();
                                    oAddressControl.cmbState.Text = oPatientGuarantors[gindex].State.Trim().ToString();
                                    oAddressControl.cmbCountry.Text = oPatientGuarantors[gindex].Country.Trim().ToString();
                                    oAddressControl.txtCounty.Text = oPatientGuarantors[gindex].County.Trim().ToString();
                                    oAddressControl.isFormLoading = false;
                                }
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
                            for (Int32 gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
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
                                oPatientGuarantors.Add(oGuarantor);
                            }
                            else
                            {
                                if (oPatientGuarantors.Count >= 1)
                                {
                                    MessageBox.Show("Multiple guarantors per patient account are not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    setCmbSameAsGuardianIndex();
                                    return;
                                }
                            }
                        }
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            for (Int32 gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                            {
                                txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                                //fill Account Address with Selected Guarantor Address when it is not empty
                                if (oPatientGuarantors[gindex].AddressLine1.Trim().ToString() != "")
                                {
                                    oAddressControl.isFormLoading = true;
                                    oAddressControl.txtAddress1.Text = oPatientGuarantors[gindex].AddressLine1.Trim().ToString();
                                    oAddressControl.txtAddress2.Text = oPatientGuarantors[gindex].AddressLine2.Trim().ToString();
                                    oAddressControl.txtCity.Text = oPatientGuarantors[gindex].City.Trim().ToString();
                                    oAddressControl.txtZip.Text = oPatientGuarantors[gindex].Zip.Trim().ToString();
                                    oAddressControl.cmbState.Text = oPatientGuarantors[gindex].State.Trim().ToString();
                                    oAddressControl.cmbCountry.Text = oPatientGuarantors[gindex].Country.Trim().ToString();
                                    oAddressControl.txtCounty.Text = oPatientGuarantors[gindex].County.Trim().ToString();
                                    oAddressControl.isFormLoading = false;
                                }
                                setCmbSameAsGuardianIndex();
                            }
                        }

                    }

                }
            }
            //code start Added by kanchan on 20130610 to solve bug :50859
            if (cmbSameAsGuardian.Text == "Patient")
            {
                pnlGIAddressDetails.Enabled = false;
            }
            else
            {
                pnlGIAddressDetails.Enabled = true;
            }
            //code end Added by kanchan on 20130610 to solve bug :50859
            IsCmbSameAsGuardianLoadFlag = true;
        }

        #endregion
        
        #region " ListControl related Events "

        //Get the exist patients from patient screen
        private void oListControl_PatientSelectedClick(object sender, EventArgs e)
        {
            //Check for Patient exist or not
            bool exist;
            DataTable dtSelectedPatient = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);

            try
            {
            //    _selectedItemsColl = new gloGeneralItem.gloItems();

                _selectedItemsColl = oListControl.SelectedItems;

                foreach (gloGeneralItem.gloItem gl in _selectedItemsColl)
                {
                    exist = false;
                    DataRow[] dRow = dtAccountPatient.Select("PatientId = " + gl.ID.ToString());
                    if (dRow != null)
                    {
                        if (dRow.Length > 0) { continue; }
                    }
                    // If is not required just for safe side
                    if (exist == false)
                    {
                        //Added by mahesh S (Apollo) , Purpose: To Get Patient data based selected patients from gloListControl
                        string _sqlQuery = " SELECT  Patient.nPatientID AS PatientID,Patient.sPatientCode AS PatientCode,  "
                                      + " ISNULL(Patient.sFirstName, '') as FirstName, ISNULL(Patient.sMiddleName,'') MiddleName, Patient.sLastName as LastName,   "
                                      + " Convert(Varchar(10),Patient.nSSN) AS SSNNo,     "
                                      + " convert(varchar,Patient.dtDOB,101) as PatientDOB, ISNULL(sPatientStatus,'') AS PatientStatus   "
                                      + " FROM Patient WHERE Patient.nPatientID = " + gl.ID;
                        oDB.Retrive_Query(_sqlQuery, out dtSelectedPatient);

                        if (dtSelectedPatient != null && dtSelectedPatient.Rows.Count > 0)
                        {
                            //dtAccountPatient contains the account exist patient List
                            DataRow newDr = dtAccountPatient.NewRow();
                            newDr["PatientId"] = dtSelectedPatient.Rows[0]["PatientID"];
                            newDr["PatientCode"] = dtSelectedPatient.Rows[0]["PatientCode"];
                            newDr["FirstName"] = dtSelectedPatient.Rows[0]["FirstName"];
                            newDr["MiddleName"] = dtSelectedPatient.Rows[0]["MiddleName"];
                            newDr["LastName"] = dtSelectedPatient.Rows[0]["LastName"];
                            newDr["SSNNO"] = dtSelectedPatient.Rows[0]["SSNNo"];
                            newDr["PatientDOB"] = dtSelectedPatient.Rows[0]["PatientDOB"];
                            newDr["Status"] = "Active";
                            dtAccountPatient.Rows.Add(newDr);
                            newDr = null;
                        }
                        if (dtSelectedPatient != null)
                            dtSelectedPatient.Dispose();
                        dtSelectedPatient = null;
                    }
                }
                FillAccountPatients(dtAccountPatient);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();

                if (dtSelectedPatient != null)
                    dtSelectedPatient.Dispose();
            }
            onPatientAccountControl_Enter(sender, e);
        }

        #endregion

        #region " Private Methods "

        //Added By Mahesh Satlapalli on 04-Jan-2011

        // fill the form controls.
        private void FillData()
        {
            DataTable dtAccount = new DataTable();
            oAccount = new Account();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            oDB.Connect(false);

            try
            {
                oParameters.Add("@nPAccountId", _nAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_Select_Accounts", oParameters, out dtAccount);

                if (dtAccount != null &&  dtAccount.Rows.Count > 0)
                {
                    oAccount.PAccountID = Convert.ToInt64(dtAccount.Rows[0]["PatientAccountId"] == DBNull.Value ? "0" : dtAccount.Rows[0]["PatientAccountId"].ToString());
                    oAccount.AccountNo =dtAccount.Rows[0]["AccountNo"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["AccountNo"].ToString();
                    oAccount.nBusinessCenterID = Convert.ToInt64(dtAccount.Rows[0]["nBusinessCenterID"] == DBNull.Value ? "0" : dtAccount.Rows[0]["nBusinessCenterID"].ToString());
                    oAccount.AccountDesc =dtAccount.Rows[0]["AccountDesc"] ==DBNull.Value ? string.Empty : dtAccount.Rows[0]["AccountDesc"].ToString();
                    oAccount.GuarantorID = Convert.ToInt64(dtAccount.Rows[0]["nGuarantorID"] == DBNull.Value ? "0" : dtAccount.Rows[0]["nGuarantorID"].ToString());
                    oAccount.GuarantorCode = dtAccount.Rows[0]["sGuarantorCode"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["sGuarantorCode"].ToString();
                    oAccount.FirstName =dtAccount.Rows[0]["GuarantorName"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["GuarantorName"].ToString();
                    oAccount.AccountClosedDate = dtAccount.Rows[0]["dtAccountClosedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dtAccount.Rows[0]["dtAccountClosedDate"].ToString());
                    oAccount.AddressLine1 = dtAccount.Rows[0]["Address1"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["Address1"].ToString();
                    oAccount.AddressLine2 =dtAccount.Rows[0]["Address2"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["Address2"].ToString();
                    oAccount.City =dtAccount.Rows[0]["City"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["City"].ToString();
                    oAccount.State =dtAccount.Rows[0]["State"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["State"].ToString();
                    oAccount.Zip = dtAccount.Rows[0]["Zip"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["Zip"].ToString();
                    oAccount.Country = dtAccount.Rows[0]["Country"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["Country"].ToString();
                    oAccount.County = dtAccount.Rows[0]["County"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["County"].ToString();
                    oAccount.AreaCode =dtAccount.Rows[0]["AreaCode"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["AreaCode"].ToString();
                    oAccount.ExcludeStatement = Convert.ToBoolean(dtAccount.Rows[0]["bIsExcludeStatement"]);
                    oAccount.SentToCollection = Convert.ToBoolean(dtAccount.Rows[0]["bIsSentToCollection"]);
                    oAccount.ClinicID = dtAccount.Rows[0]["nClinicID"] == DBNull.Value ? 0 : Convert.ToInt64(dtAccount.Rows[0]["nClinicID"].ToString());
                    oAccount.SiteID = dtAccount.Rows[0]["nSiteID"] == DBNull.Value ? 0 : Convert.ToInt64(dtAccount.Rows[0]["nSiteID"].ToString());
                    oAccount.UserID = dtAccount.Rows[0]["nUserID"] == DBNull.Value ? 0 : Convert.ToInt64(dtAccount.Rows[0]["nUserID"].ToString());
                    oAccount.MachineName = dtAccount.Rows[0]["sMachineName"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["sMachineName"].ToString();
                    oAccount.RecordDate = dtAccount.Rows[0]["dtRecordDate"] == DBNull.Value  ? DateTime.MinValue : dtAccount.Rows[0]["dtRecordDate"].ToString()=="" ? DateTime.MinValue : Convert.ToDateTime(dtAccount.Rows[0]["dtRecordDate"].ToString());
                    oAccount.Active = Convert.ToBoolean(dtAccount.Rows[0]["IsActive"].ToString());

                    txtAccountNo.Text = oAccount.AccountNo;
                    txtAccountDescription.Text = oAccount.AccountDesc;

                    FillBusinessCenter(oAccount.nBusinessCenterID);

                    cmbBusinessCenter.SelectedValue = oAccount.nBusinessCenterID;
                    nBusinessCenterID = Convert.ToInt64(oAccount.nBusinessCenterID);
                    sGuarantorCode = oAccount.GuarantorCode;

                    oAddressControl.isFormLoading = true;
                    //Added by Mayuri : 20151006-2.	Update Guarantor Address if Guarantor is “Same as Patient” when patient address is updated.
                    if (cmbSameAsGuardian.Text == "Patient")
                    {
                        oAddressControl.txtAddress1.Text = oPatientDemographicsDetails.PatientAddress1.ToString().Trim();
                        oAddressControl.txtAddress2.Text =oPatientDemographicsDetails.PatientAddress2.ToString().Trim();
                        oAddressControl.txtCity.Text = oPatientDemographicsDetails.PatientCity.ToString().Trim();
                        oAddressControl.txtZip.Text = oPatientDemographicsDetails.PatientZip.ToString().Trim();
                        oAddressControl.cmbCountry.Text = oPatientDemographicsDetails.PatientCountry.ToString().Trim();
                        oAddressControl.txtCounty.Text = oPatientDemographicsDetails.PatientCounty.ToString().Trim();
                        oAddressControl.cmbState.Text = oPatientDemographicsDetails.PatientState.ToString().Trim();

                    }
                    else
                    {
                        oAddressControl.txtAddress1.Text = oAccount.AddressLine1;
                        oAddressControl.txtAddress2.Text = oAccount.AddressLine2;
                        oAddressControl.txtCity.Text = oAccount.City;
                        oAddressControl.txtZip.Text = dtAccount.Rows[0]["Zip"].ToString();
                        oAddressControl.cmbCountry.Text = oAccount.Country;
                        oAddressControl.txtCounty.Text = oAccount.County;
                        oAddressControl.cmbState.Text = oAccount.State;
                    }
                    
                   
                    oAddressControl.isFormLoading = false;
                    chkAccountActive.Checked = Convert.ToBoolean(oAccount.Active);
                    chkSetToCollection.Checked = Convert.ToBoolean(oAccount.SentToCollection);
                    chkExcludefromStatement.Checked = Convert.ToBoolean(oAccount.ExcludeStatement);
                    dtAccountCloseDate = oAccount.AccountClosedDate;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();

                if (dtAccount != null)
                    dtAccount.Dispose();
            }

        }

        // display patients on Grid.
        private void FillGrid()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Add("@nPAccountId", _nAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_Select_Accounts_Patients", oParameters, out dtAccountPatient);
                FillAccountPatients(dtAccountPatient);

                //loading time change the "btnPatientDeactivate" Text.
                PatientActivateDeActivate();

                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception gex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
                oParameters.Dispose();
            }
        }

        //Design Grid 
        private void FillAccountPatients(DataTable dtAccount)
        {
            gvPatient.DataSource = dtAccount;

            gvPatient.Cols[COL_AccountPatientId].Caption = "AccountPatientId";
            gvPatient.Cols[COL_PAccountId].Caption = "PAccountId";
            gvPatient.Cols[COL_PatientId].Caption = "PatientId";
            gvPatient.Cols[COL_AccountNo].Caption = "AccountNo";
            gvPatient.Cols[COL_PatientCode].Caption = "Code";
            gvPatient.Cols[COL_FirstName].Caption = "First Name";
            gvPatient.Cols[COL_MiddleName].Caption = "MI";
            gvPatient.Cols[COL_LastName].Caption = "Last Name";
            gvPatient.Cols[COL_SSNNO].Caption = "SSN";
            gvPatient.Cols[COL_PatientDOB].Caption = "DOB";
            gvPatient.Cols[COL_Status].Caption = "Status";
            gvPatient.Cols[COL_OwnAccount].Caption = "OwnAccount";
            gvPatient.Cols[COL_PatientDisplayName].Caption = "PatientDisplayName";

            gvPatient.Cols[COL_AccountPatientId].Visible = false;
            gvPatient.Cols[COL_PAccountId].Visible = false;
            gvPatient.Cols[COL_AccountNo].Visible = false;
            gvPatient.Cols[COL_PatientId].Visible = false;
            gvPatient.Cols[COL_OwnAccount].Visible = false;
            gvPatient.Cols[COL_PatientDisplayName].Visible = false;
            gvPatient.Cols[COL_PatientCode].Width = 80;
            gvPatient.Cols[COL_FirstName].Width = 150;
            gvPatient.Cols[COL_MiddleName].Width = 40;
            gvPatient.Cols[COL_LastName].Width = 150;
            gvPatient.Cols[COL_SSNNO].Width = 90;
            gvPatient.Cols[COL_PatientDOB].Width = 80;
            gvPatient.Cols[COL_Status].Width = 80;
            
            gvPatient.ScrollBars = ScrollBars.Both;
        }

        // For Save and Update Account details.
        public void SaveAccount()
        {
            #region "Patient AccountGuarantors and Account"

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();

            try
            {
                if (oPatientGuarantors != null)
                {
                    oDB.Connect(false);
                    if (oAccount != null)
                    {
                        if (IsAccountDataModified() == true)
                        {
                            if (nBusinessCenterID != Convert.ToInt64(cmbBusinessCenter.SelectedValue))
                            {
                                DataTable dtAccountUsed = null;
                                odbParams.Clear();
                                odbParams.Add("@nPAccountID", oAccount.PAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDB.Retrive("PA_Verify_AccountsInUse", odbParams, out dtAccountUsed);
                                if (dtAccountUsed != null && dtAccountUsed.Rows.Count > 0)
                                {
                                    MessageBox.Show("Account Business Center is already in use. Modification of Business Center is not allowed.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            odbParams.Clear();
                            object accountId;
                            odbParams.Add("@nPAccountID", oAccount.PAccountID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            odbParams.Add("@sAccountNo", oAccount.AccountNo, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sAccountDesc", oAccount.AccountDesc, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nGuarantorID", oAccount.GuarantorID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sGuarantorCode", oAccount.GuarantorCode, ParameterDirection.Input, SqlDbType.VarChar);

                            if (oAccount.Active == false)
                                odbParams.Add("@dtAccountClosedDate", DateTime.Today, ParameterDirection.Input, SqlDbType.DateTime);
                            else
                                odbParams.Add("@dtAccountClosedDate", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);

                            odbParams.Add("@sFirstName", oAccount.FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sMiddleName", oAccount.MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sLastName", oAccount.LastName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nEntityType", oAccount.EntityType, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sAddressLine1", oAccount.AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sAddressLine2", oAccount.AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sCity", oAccount.City, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sState", oAccount.State, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sZip", oAccount.Zip, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sCountry", oAccount.Country, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sCounty", oAccount.County, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sAreaCode", oAccount.AreaCode, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@bIsExcludeStatement", oAccount.ExcludeStatement, ParameterDirection.Input, SqlDbType.Bit);
                            odbParams.Add("@bIsSentToCollection", oAccount.SentToCollection, ParameterDirection.Input, SqlDbType.Bit);
                            odbParams.Add("@nClinicID", oAccount.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@nSiteID", oAccount.SiteID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sMachineName", oAccount.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nUserID", oAccount.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@dtRecordDate", oAccount.RecordDate, ParameterDirection.Input, SqlDbType.DateTime);
                            odbParams.Add("@bIsActive", oAccount.Active, ParameterDirection.Input, SqlDbType.Bit);

                            odbParams.Add("@nBusinessCenterID", oAccount.nBusinessCenterID, ParameterDirection.Input, SqlDbType.BigInt);

                            oDB.Execute("PA_InUp_Accounts", odbParams, out accountId);
                        }

                        foreach (PatientAccount actPatient in oListAccountPatient)
                        {
                            odbParams.Clear();
                            odbParams.Add("@nAccountPatientID", actPatient.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@nPAccountID", _nAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@nPatientID", actPatient.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sAccountNo", oAccount.AccountNo, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@sPatientCode", actPatient.PatientCode, ParameterDirection.Input, SqlDbType.VarChar);

                            if (actPatient.Active == false)
                                odbParams.Add("@dtAccountClosedDate", DateTime.Today, ParameterDirection.Input, SqlDbType.DateTime);
                            else
                                odbParams.Add("@dtAccountClosedDate", DBNull.Value, ParameterDirection.Input, SqlDbType.DateTime);

                            odbParams.Add("@nClinicID", actPatient.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@nSiteID", actPatient.SiteID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@sMachineName", actPatient.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                            odbParams.Add("@nUserID", actPatient.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                            odbParams.Add("@dtRecordDate", actPatient.RecordDate, ParameterDirection.Input, SqlDbType.DateTime);
                            odbParams.Add("@bIsActive", actPatient.Active, ParameterDirection.Input, SqlDbType.Bit);
                            odbParams.Add("@bIsOwnAccount", actPatient.OwnAccount, ParameterDirection.Input, SqlDbType.Bit);
                            oDB.ExecuteWithTransaction("PA_InUp_Accounts_Patients", odbParams);
                        }
                    }
                    if (_IsOwnAccount == true)
                    {
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            //when account guarantor changed then Insert chnaged guarantor into history table and delete guarantor and insert new guarantor 
                            if ((_nGuarantorId != oPatientGuarantors[0].PatientContactID) || (oPatientGuarantors[0].PatientContactID != 0 && oPatientGuarantors[0].IsDataModified == true))
                            {
                                //Insert deleted guarnator in history table.
                                odbParams.Clear();
                                odbParams.Add("@nPatientContactID", _nGuarantorId, ParameterDirection.Input, SqlDbType.BigInt);
                                odbParams.Add("@nUserID", Convert.ToInt64(appSettings["UserID"].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                odbParams.Add("@sHstMachineName", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDB.Execute("PA_IN_Patient_OtherContacts_Hst", odbParams);

                                //Delete guarantor 
                                string sqlquery = "delete from Patient_OtherContacts where nPatientID = '" + Convert.ToInt64(_nPatientId) + "' and nPAccountID =" + Convert.ToInt64(_nAccountId);
                                oDB.Execute_Query(sqlquery);

                                for (Int32 _GuarantorCnt = 0; _GuarantorCnt < PatientGuarantors.Count; _GuarantorCnt++)
                                {
                                    Object GuarantorId;
                                    odbParams.Clear();
                                    odbParams.Add("@nPatientID", Convert.ToInt64(_nPatientId), ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nPatientContactID", oPatientGuarantors[_GuarantorCnt].PatientContactID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                    odbParams.Add("@nLineNumber", _GuarantorCnt, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nPatientContactType", oPatientGuarantors[_GuarantorCnt].OtherConatctType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    odbParams.Add("@sFirstName", PatientGuarantors[_GuarantorCnt].FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sMiddleName", PatientGuarantors[_GuarantorCnt].MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sLastName", PatientGuarantors[_GuarantorCnt].LastName, ParameterDirection.Input, SqlDbType.VarChar);
                                    if (PatientGuarantors[_GuarantorCnt].DOB != null && PatientGuarantors[_GuarantorCnt].DOB != DateTime.MinValue)
                                    {
                                        odbParams.Add("@nDOB", gloDateMaster.gloDate.DateAsNumber(PatientGuarantors[_GuarantorCnt].DOB.ToShortDateString()), ParameterDirection.Input, SqlDbType.Int);
                                    }
                                    else
                                    {
                                        odbParams.Add("@nDOB", DBNull.Value, ParameterDirection.Input, SqlDbType.Int);
                                    }
                                    odbParams.Add("@sSSN", PatientGuarantors[_GuarantorCnt].SSN, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sGender", PatientGuarantors[_GuarantorCnt].Gender, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sRelation", PatientGuarantors[_GuarantorCnt].Relation, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sAddressLine1", PatientGuarantors[_GuarantorCnt].AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sAddressLine2", PatientGuarantors[_GuarantorCnt].AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sCity", PatientGuarantors[_GuarantorCnt].City, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sState", PatientGuarantors[_GuarantorCnt].State, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sZIP", PatientGuarantors[_GuarantorCnt].Zip, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sPhone", PatientGuarantors[_GuarantorCnt].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sMobile", PatientGuarantors[_GuarantorCnt].Mobile, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sFax", PatientGuarantors[_GuarantorCnt].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sEmail", PatientGuarantors[_GuarantorCnt].Email, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@bIsActive", PatientGuarantors[_GuarantorCnt].IsActive, ParameterDirection.Input, SqlDbType.Bit);
                                    odbParams.Add("@nVisitID", PatientGuarantors[_GuarantorCnt].VisitID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nAppointmentID", PatientGuarantors[_GuarantorCnt].AppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nGuarantorAsPatientID", PatientGuarantors[_GuarantorCnt].GuarantorAsPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nPatientContactTypeFlag", PatientGuarantors[_GuarantorCnt].nGuarantorTypeFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    odbParams.Add("@sCounty", PatientGuarantors[_GuarantorCnt].County, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@sCountry", PatientGuarantors[_GuarantorCnt].Country, ParameterDirection.Input, SqlDbType.VarChar);
                                    odbParams.Add("@nClinicID", nClinicId, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@nGuarantorType", PatientGuarantors[_GuarantorCnt].GurantorType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    odbParams.Add("@nPAccountID", _nAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                                    odbParams.Add("@bIsAccountGuarantor", PatientGuarantors[_GuarantorCnt].IsAccountGuarantor, ParameterDirection.Input, SqlDbType.Bit);
                                    oDB.Execute("PA_INUP_PatientGuarantor", odbParams, out GuarantorId);
                                }
                            }
                        }
                    }
                }
            }

            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception gex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
            }

            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (odbParams != null)
                {
                    odbParams.Dispose();
                    odbParams = null;
                }
            }
            #endregion
        }

        //comment by mahesh s on 03-may-2011 , this functionality moved to tools.
        //Merge two Accounts into single account.
        //public void MergeAccount()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();
        //    oDB.Connect(false);

        //    string machineName=System.Environment.MachineName;
        //    Int64 userID= Convert.ToInt64(appSettings["UserID"].ToString());

        //    try
        //    {
        //        odbParams.Add("@From_nPAccountID", _nAccountId, ParameterDirection.Input, SqlDbType.BigInt);
        //        odbParams.Add("@To_nPAccountID", _nExistAccountId, ParameterDirection.Input, SqlDbType.BigInt);
        //        odbParams.Add("@MachineName",machineName , ParameterDirection.Input, SqlDbType.VarChar);
        //        odbParams.Add("@nUserID", userID, ParameterDirection.Input, SqlDbType.BigInt);
        //        odbParams.Add("@nClinicID", nClinicId, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDB.Execute("PA_MergeAccount", odbParams);
        //    }
        //    catch (gloDatabaseLayer.DBException ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    catch (Exception gex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
        //    }
        //    finally
        //    {
        //        oDB.Dispose();
        //        odbParams.Dispose();
        //    }
        //}

        //Assign modified data to account and patientaccount object
        public bool GetData()
        {
            if (ValidateData() == true)
            {
                PrepareAccountAndAcountPatient();
                return true;
            }
            else
            {
                return false;
            }
        }

        //Account and AccountPatient Objects prepare code.
        private void PrepareAccountAndAcountPatient()
        {
            oListAccountPatient = new List<PatientAccount>();
            oAccount.PAccountID = _nAccountId;
            oAccount.AccountNo = txtAccountNo.Text;
            oAccount.AccountDesc = txtAccountDescription.Text;

            if (cmbBusinessCenter.SelectedIndex != -1)
            {
                oAccount.nBusinessCenterID = Convert.ToInt64(cmbBusinessCenter.SelectedValue);

            }

            oAccount.AddressLine1 = oAddressControl.txtAddress1.Text;
            oAccount.AddressLine2 = oAddressControl.txtAddress2.Text;
            oAccount.City = oAddressControl.txtCity.Text;
            oAccount.State = oAddressControl.cmbState.Text;
            oAccount.Zip = oAddressControl.txtZip.Text;
            oAccount.Country = oAddressControl.cmbCountry.Text;
            oAccount.County = oAddressControl.txtCounty.Text;
            oAccount.Active = Convert.ToBoolean(chkAccountActive.Checked);
            oAccount.AccountClosedDate = oAccount.Active == false ? DateTime.Today : oAccount.AccountClosedDate;
            oAccount.ExcludeStatement = Convert.ToBoolean(chkExcludefromStatement.Checked);
            oAccount.SentToCollection = Convert.ToBoolean(chkSetToCollection.Checked);
            oAccount.MachineName = System.Environment.MachineName;
            oAccount.UserID = Convert.ToInt64(appSettings["UserID"].ToString());
            oAccount.RecordDate = DateTime.Today;
            oAccount.AccountClosedDate = dtAccountCloseDate;

            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
            {
                for (Int32 gIndex = 0; gIndex < oPatientGuarantors.Count; gIndex++)
                {
                    //assign  guarantor as account guarantor.
                    if (oPatientGuarantors[gIndex].IsAccountGuarantor==true)
                    {
                        oAccount.FirstName = oPatientGuarantors[gIndex].FirstName.Trim().ToString();
                        oAccount.LastName = oPatientGuarantors[gIndex].LastName.Trim().ToString();
                        oAccount.MiddleName = oPatientGuarantors[gIndex].MiddleName.Trim().ToString();
                        oAccount.ClinicID = nClinicId;
                        oAccount.MachineName = System.Environment.MachineName;
                        oAccount.GuarantorCode = "";
                        oAccount.AreaCode = "";
                        oAccount.EntityType = oPatientGuarantors[gIndex].GurantorType.GetHashCode();
                        oAccount.SiteID = 1;
                        break;
                    }
                }
            }
            if (dtAccountPatient.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAccountPatient.Rows)
                {
                    oPatientAccount = new PatientAccount();
                    oPatientAccount.AccountPatientID = dr["AccountPatientId"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dr["AccountPatientId"].ToString());
                    oPatientAccount.PAccountID = dr["PAccountId"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dr["PAccountId"].ToString());
                    oPatientAccount.PatientID = dr["PatientId"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dr["PatientId"].ToString());
                    oPatientAccount.PatientCode = dr["PatientCode"] == DBNull.Value ? string.Empty : dr["PatientCode"].ToString();
                    nClinicId = Convert.ToInt64(appSettings["ClinicID"].ToString());
                    oPatientAccount.ClinicID = nClinicId;
                    oPatientAccount.SiteID = Convert.ToInt64("1");
                    oPatientAccount.MachineName = System.Windows.Forms.SystemInformation.ComputerName;
                    oPatientAccount.UserID = Convert.ToInt64(appSettings["UserID"].ToString());
                    oPatientAccount.RecordDate = DateTime.Today;
                    oPatientAccount.Active = dr["Status"].ToString() == "Active" ? true : false;
                    oPatientAccount.AccountClosedDate = dr["Status"].ToString() == "Active" ? oPatientAccount.AccountClosedDate : DateTime.Today;
                    //means add other patient in this account
                    oPatientAccount.OwnAccount = oPatientAccount.AccountPatientID == 0 ? false : Convert.ToBoolean(dr["bIsOwnAccount"].ToString());
                    oListAccountPatient.Add(oPatientAccount);
                    oPatientAccount = null;
                }
            }

        }

        //btnPatientDeactivate button text change at form load.
        private void PatientActivateDeActivate()
        {
            if (gvPatient.Rows.Count > 1)
            {
                if (gvPatient.Rows[1][COL_Status].ToString().ToLower() == "deactive")
                {
                    btnPatientDeactivate.Text = "Activate Patient";
                    btnPatientDeactivate.Tag = "Activate Patient";
                }
                else
                {
                    btnPatientDeactivate.Text = "Deactivate Patient";
                    btnPatientDeactivate.Tag = "DeActivate Patient";
                }
            }
        }

        //Guarantor type
        private PatientOtherContact.GuarantorTypeFlag GetNextTypeFlag(bool CallFromSameAsPatient)
        {
            PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.None;

            bool isPrimaryPresent = false;
            bool isSecondaryPresent = false;
            bool isTertioryPresent = false;

            if (oPatientGuarantors.Count != 0)
            {
                for (Int32 _GuarantorCnt = 0; _GuarantorCnt < oPatientGuarantors.Count; _GuarantorCnt++)
                {
                    if (oPatientGuarantors[_GuarantorCnt].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Primary.GetHashCode())
                    { isPrimaryPresent = true; }
                    else if (oPatientGuarantors[_GuarantorCnt].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Secondary.GetHashCode())
                    { isSecondaryPresent = true; }
                    else if (oPatientGuarantors[_GuarantorCnt].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Tertiary.GetHashCode())
                    { isTertioryPresent = true; }
                }

                if (!isPrimaryPresent)
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Primary; }
                else if (!isSecondaryPresent)
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Secondary; }
                else if (!isTertioryPresent)
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Tertiary; }
                else
                { _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Tertiary; }
            }
            else
            {
                _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Primary;
            }
            return _GuarantorTypeFlag;
        }

        //Validations
        private bool ValidateData()
        {

            if (oAddressControl.txtZip.Focused == true)
            {
                oAddressControl.txtZip_LostFocus(null, null);
            }  
            if (txtAccountNo.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Enter Acct.#.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccountNo.Focus();
                    return false;
                }

                //Remove Account Description validations
                
                //else if (txtAccountDescription.Text.ToString().Trim().Length == 0)
                //{
                //    MessageBox.Show("Enter Acct. Desc.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtAccountDescription.Focus();
                //    return false;
                //}
                if (txtAccGuarantor.Text == "")
                {
                    MessageBox.Show("Select guarantor for Account.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //else if (oAddressControl.txtAddress1.Text.Trim().Length == 0)
                //{
                //    MessageBox.Show("Enter Address.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtAccAddressLine1.Focus();
                //    return false;
                //}
                //else if (oAddressControl.txtCity.Text.ToString().Trim() == "")
                //{
                //    MessageBox.Show("Enter city.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtAccCity.Focus();
                //    return false;
                //}
                //else if (oAddressControl.cmbState.Text.ToString().Trim() == "")
                //{
                //    MessageBox.Show("Select state.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    cmbState.Focus();
                //    return false;
                //}
                //else if (oAddressControl.txtZip.Text.ToString().Trim() == "")
                //{
                //    MessageBox.Show("Enter zip.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtAccZip.Focus();
                //    return false;
                //}
                else if (GetPatientAccountCount() && chkAccountActive.Checked == false)
                {
                    DialogResult res = MessageBox.Show("Patient’s account will be deactivated." + Environment.NewLine + "The Patient has no other active account.", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                      
                    if (res == DialogResult.Cancel)
                    {
                        chkAccountActive.Focus();
                        return false;
                    }
                }
            

            return true;
        }

        //comment by mahesh s on 03-may-2011 , this functionality moved to tools.
        //Merge Account validations
        //public bool ValidateMergeData()
        //{
        //    if (ValidateData() == true)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //Same as Guardian combo fill.
        private void FillSameAsGuardian()
        {
            //Patient
            cmbSameAsGuardian.Items.Add("Patient");

            //if (_IsOwnAccount == true)
            //{
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
                // Gaurdian
                if ((oPatientGuardian.PatientGuardianFirstName.ToString() + " " + oPatientGuardian.PatientGuardianLastName.ToString()).Trim() != "")
                {
                    cmbSameAsGuardian.Items.Add("Other Guardian");
                }

            //}
        }

        //Code Added by SaiKrishna : Guaranctors combo fill.
        private void FillGuarantors()
        {
            DataTable dtGurantorDetails;
            oPatientGuarantors = new PatientOtherContacts();
            PatientOtherContact oGuarantor = null;

            //GetAccountGuarantors
            dtGurantorDetails = objgloAccount.GetAccountGuarantors(_nAccountId, nClinicId);

            if (dtGurantorDetails != null && dtGurantorDetails.Rows.Count > 0)
            {
                for (Int32 _GuarantorCnt = 0; _GuarantorCnt < dtGurantorDetails.Rows.Count; _GuarantorCnt++)
                {
                    oGuarantor = new PatientOtherContact();
                    oGuarantor.PatientID = dtGurantorDetails.Rows[_GuarantorCnt]["nPatientID"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtGurantorDetails.Rows[_GuarantorCnt]["nPatientID"].ToString());
                    oGuarantor.PatientContactID = dtGurantorDetails.Rows[_GuarantorCnt]["nPatientContactID"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtGurantorDetails.Rows[_GuarantorCnt]["nPatientContactID"].ToString());
                    oGuarantor.GuarantorAsPatientID = dtGurantorDetails.Rows[_GuarantorCnt]["nGuarantorAsPatientID"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtGurantorDetails.Rows[_GuarantorCnt]["nGuarantorAsPatientID"].ToString());
                    oGuarantor.IsActive = true;
                    oGuarantor.FirstName = dtGurantorDetails.Rows[_GuarantorCnt]["sFirstName"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sFirstName"].ToString();
                    oGuarantor.MiddleName = dtGurantorDetails.Rows[_GuarantorCnt]["sMiddleName"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sMiddleName"].ToString();
                    oGuarantor.LastName = dtGurantorDetails.Rows[_GuarantorCnt]["sLastName"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sLastName"].ToString();
                    oGuarantor.DOB = dtGurantorDetails.Rows[_GuarantorCnt]["nDOB"] == DBNull.Value ? DateTime.MinValue : dtGurantorDetails.Rows[_GuarantorCnt]["nDOB"].ToString()=="" ? DateTime.MinValue : gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtGurantorDetails.Rows[_GuarantorCnt]["nDOB"]));
                    oGuarantor.SSN = dtGurantorDetails.Rows[_GuarantorCnt]["sSSN"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sSSN"].ToString();
                    oGuarantor.Gender = dtGurantorDetails.Rows[_GuarantorCnt]["sGender"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sGender"].ToString();
                    oGuarantor.AddressLine1 =  dtGurantorDetails.Rows[_GuarantorCnt]["sAddressLine1"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sAddressLine1"].ToString();
                    oGuarantor.AddressLine2 =  dtGurantorDetails.Rows[_GuarantorCnt]["sAddressLine2"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sAddressLine2"].ToString();
                    oGuarantor.City = dtGurantorDetails.Rows[_GuarantorCnt]["sCity"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sCity"].ToString();
                    oGuarantor.State = dtGurantorDetails.Rows[_GuarantorCnt]["sState"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sState"].ToString();
                    oGuarantor.Zip =dtGurantorDetails.Rows[_GuarantorCnt]["sZip"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sZip"].ToString();
                    oGuarantor.Country = dtGurantorDetails.Rows[_GuarantorCnt]["sCountry"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sCountry"].ToString();
                    oGuarantor.County = dtGurantorDetails.Rows[_GuarantorCnt]["sCounty"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sCounty"].ToString();
                    oGuarantor.Relation = dtGurantorDetails.Rows[_GuarantorCnt]["sRelation"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sRelation"].ToString();
                    oGuarantor.IsActive = Convert.ToBoolean(dtGurantorDetails.Rows[_GuarantorCnt]["bIsActive"].ToString());
                    oGuarantor.Phone = dtGurantorDetails.Rows[_GuarantorCnt]["sPhone"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sPhone"].ToString();
                    oGuarantor.Mobile = dtGurantorDetails.Rows[_GuarantorCnt]["sMobile"] == DBNull.Value ?string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sMobile"].ToString();
                    oGuarantor.Email = dtGurantorDetails.Rows[_GuarantorCnt]["sEmail"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sEmail"].ToString();
                    oGuarantor.Fax = dtGurantorDetails.Rows[_GuarantorCnt]["sFax"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCnt]["sFax"].ToString();
                    oGuarantor.nGuarantorTypeFlag = Convert.ToInt32(dtGurantorDetails.Rows[_GuarantorCnt]["nPatientContactTypeFlag"]);
                    oGuarantor.OtherConatctType = (PatientOtherContactType)Convert.ToInt32(dtGurantorDetails.Rows[_GuarantorCnt]["nPatientContactType"]);
                    oGuarantor.GurantorType = (GuarantorType)Convert.ToInt32(dtGurantorDetails.Rows[_GuarantorCnt]["nGuarantorType"]);
                    oGuarantor.IsAccountGuarantor = Convert.ToBoolean(dtGurantorDetails.Rows[_GuarantorCnt]["bIsAccountGuarantor"].ToString());
                    oGuarantor.PAccountID = _nAccountId;
                    oPatientGuarantors.Add(oGuarantor);
                }
            }
            if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
            {
                for (Int32 gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                {
                    setCmbSameAsGuardianIndex();
                }
            }

       }

        //Code Added by SaiKrishna For setting Account Data
        private void SetData()
        {

            FillSameAsGuardian();
            FillGuarantors();
            FillData();
            FillGrid();
        }

        private void setCmbSameAsGuardianIndex()
        {
            for (Int32 gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
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

        private bool IsAccountDataModified()
        {
            bool _Result = true;

              //Check if Account details are modified or not
                DataTable dtAccount = new DataTable();
                Account oAccountOrg = new Account();
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);

            try
            {
                oParameters.Add("@nPAccountId", _nAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_Select_Accounts", oParameters, out dtAccount);

                if (dtAccount.Rows.Count > 0)
                {
                    oAccountOrg.PAccountID = Convert.ToInt64(dtAccount.Rows[0]["PatientAccountId"].ToString());
                    oAccountOrg.AccountNo = dtAccount.Rows[0]["AccountNo"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["AccountNo"].ToString();
                    oAccountOrg.AccountDesc = dtAccount.Rows[0]["AccountDesc"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["AccountDesc"].ToString();
                    oAccountOrg.GuarantorID = dtAccount.Rows[0]["nGuarantorID"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtAccount.Rows[0]["nGuarantorID"].ToString());
                    oAccountOrg.GuarantorCode = dtAccount.Rows[0]["sGuarantorCode"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["sGuarantorCode"].ToString();
                    oAccountOrg.FirstName = dtAccount.Rows[0]["GuarantorName"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["GuarantorName"].ToString();
                    oAccountOrg.MiddleName = "";
                    oAccountOrg.LastName = "";
                    oAccountOrg.AccountClosedDate = dtAccount.Rows[0]["dtAccountClosedDate"] == DBNull.Value ? DateTime.MinValue : dtAccount.Rows[0]["dtAccountClosedDate"] .ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dtAccount.Rows[0]["dtAccountClosedDate"].ToString());
                    oAccountOrg.AddressLine1 = dtAccount.Rows[0]["Address1"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["Address1"].ToString();
                    oAccountOrg.AddressLine2 = dtAccount.Rows[0]["Address2"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["Address2"].ToString();
                    oAccountOrg.City = dtAccount.Rows[0]["City"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["City"].ToString();
                    oAccountOrg.State = dtAccount.Rows[0]["State"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["State"].ToString();
                    oAccountOrg.Zip = dtAccount.Rows[0]["Zip"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["Zip"].ToString();
                    oAccountOrg.Country = dtAccount.Rows[0]["Country"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["Country"].ToString();
                    oAccountOrg.County = dtAccount.Rows[0]["County"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["County"].ToString();
                    oAccountOrg.AreaCode = dtAccount.Rows[0]["AreaCode"] ==DBNull.Value ? string.Empty : dtAccount.Rows[0]["AreaCode"].ToString();
                    oAccountOrg.ExcludeStatement = Convert.ToBoolean(dtAccount.Rows[0]["bIsExcludeStatement"]);
                    oAccountOrg.SentToCollection = Convert.ToBoolean(dtAccount.Rows[0]["bIsSentToCollection"]);
                    oAccountOrg.ClinicID =dtAccount.Rows[0]["nClinicID"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtAccount.Rows[0]["nClinicID"].ToString());
                    oAccountOrg.SiteID = dtAccount.Rows[0]["nSiteID"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtAccount.Rows[0]["nSiteID"].ToString());
                    oAccountOrg.UserID = dtAccount.Rows[0]["nUserID"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtAccount.Rows[0]["nUserID"].ToString());
                    oAccountOrg.MachineName = dtAccount.Rows[0]["sMachineName"].ToString();
                    oAccountOrg.RecordDate =dtAccount.Rows[0]["dtRecordDate"] ==DBNull.Value ? DateTime.MinValue : dtAccount.Rows[0]["dtRecordDate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dtAccount.Rows[0]["dtRecordDate"].ToString());
                    oAccountOrg.Active = Convert.ToBoolean(dtAccount.Rows[0]["IsActive"].ToString());
                }


                if (txtAccountDescription.Text == oAccountOrg.AccountDesc
                     && oAddressControl.txtAddress1.Text == oAccountOrg.AddressLine1
                     && oAddressControl.txtAddress2.Text == oAccountOrg.AddressLine2
                     && oAddressControl.txtCity.Text == oAccountOrg.City
                     && oAddressControl.cmbState.Text == oAccountOrg.State
                     && oAddressControl.txtZip.Text == oAccountOrg.Zip && _nGuarantorId == oPatientGuarantors[0].PatientContactID
                     && chkExcludefromStatement.Checked == oAccountOrg.ExcludeStatement
                     && chkSetToCollection.Checked == oAccountOrg.SentToCollection
                     && chkAccountActive.Checked == oAccountOrg.Active
                     && oAddressControl.cmbCountry.Text == oAccountOrg.Country
                     && oPatientGuarantors[0].FirstName.Trim().ToUpper() == oAccountOrg.FirstName.Trim().ToUpper()
                     && oPatientGuarantors[0].MiddleName.Trim().ToUpper() == oAccountOrg.MiddleName.Trim().ToUpper()
                     && oPatientGuarantors[0].LastName.Trim().ToUpper() == oAccountOrg.LastName.Trim().ToUpper()
                    )
                {
                    _Result = false;
                }

                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
                oParameters.Dispose();

                if (dtAccount != null)
                    dtAccount.Dispose();

                if (oAccountOrg != null)
                    oAccountOrg.Dispose();
            }
            return _Result;
        }

        //Confirmation from user for Deactivate all patients
        private bool ConfirmDeactivateStatus()
        {
            if (!ValidStatus())
            {
                DialogResult res = MessageBox.Show("Patient will be deactivated from this Account. " + Environment.NewLine + "This Account has no other active Patient.", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        //Check the active patients count
        private bool ValidStatus()
        {
            Int32 activeStatusCnt = 0;

            foreach (C1.Win.C1FlexGrid.Row oRow in gvPatient.Rows)
            {
                if (oRow[COL_Status].ToString().ToUpper() == "ACTIVE")
                {
                    activeStatusCnt = activeStatusCnt + 1;
                }
                // last patient record with active status
                if (activeStatusCnt > 1)
                {
                    return true;
                }
            }
            return false;
        }

        //Added By Mahesh (Apollo) on 20-Jan-2011 - get patient's account count.
        private bool GetPatientAccountCount()
        {
            Int32 nAccountCount = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            bool blnRetvalue = false;
            oDB.Connect(false);

            try
            {
                oParameters.Add("@nPatientId", _nPatientId, ParameterDirection.Input, SqlDbType.BigInt);

                nAccountCount = Convert.ToInt16(oDB.ExecuteScalar("PA_GetPatientAccountCount", oParameters));

                if (nAccountCount <= 1)
                {
                    blnRetvalue = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
                oParameters.Dispose();
            }
            return blnRetvalue;
        }

        //Get account details by account id.
        private DataTable GetAccountDetailsById(Int64 accountId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtAccountDetails = new DataTable();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nPAccountId", _nExistAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_Select_Accounts", oParameters, out dtAccountDetails);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }
            return dtAccountDetails;

        }


        private void FillBusinessCenter(Int64 nBusinesscenterID)
        {
            DataTable dtBusinessCenterTemp = null;
            DataTable dtBusinessCenter = null;
            try
            {

                dtBusinessCenterTemp = gloGlobal.gloPMMasters.GetBusinessCenter();
                if (dtBusinessCenterTemp != null && dtBusinessCenterTemp.Rows.Count > 0)
                {
                    dtBusinessCenter = dtBusinessCenterTemp.Clone();
                    DataRow[] _drBusinessCenter = null;
                    _drBusinessCenter = dtBusinessCenterTemp.Select("bIsActive = 1  OR nBusinessCenterID IN(" + nBusinesscenterID + ") ");
                    foreach (DataRow dr in _drBusinessCenter)
                    {
                        dtBusinessCenter.ImportRow(dr);
                    }
                }

                if (dtBusinessCenter != null)
                {
                    if (dtBusinessCenter.Rows.Count > 0)
                    {
                        cmbBusinessCenter.BeginUpdate();
                        cmbBusinessCenter.DataSource = dtBusinessCenter.Copy();
                        cmbBusinessCenter.DisplayMember = dtBusinessCenter.Columns["BusinessCenter"].ColumnName;
                        cmbBusinessCenter.ValueMember = dtBusinessCenter.Columns["nBusinessCenterID"].ColumnName;
                        cmbBusinessCenter.EndUpdate();

                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (dtBusinessCenter != null) { dtBusinessCenter.Dispose(); }
                if (dtBusinessCenterTemp != null) { dtBusinessCenterTemp.Dispose(); }
            }
        }

        #endregion

        public void DisposeAllControls()
        {
            try
            {
                if (oPatientGuarantors != null) { oPatientGuarantors.Dispose(); }
                if (objgloAccount != null) { objgloAccount.Dispose(); }
                if (oListControl != null) { oListControl.Dispose(); }
                if (oPatientAccount != null) { oPatientAccount.Dispose(); }
                if (ogloPatientGuarantorControl != null) { ogloPatientGuarantorControl.Dispose(); }
                if (oAddressControl != null) { oAddressControl.Dispose(); }
                if (oPatientGuardian != null) { oPatientGuardian.Dispose(); }
                if (oPatientDemographicsDetails != null) { oPatientDemographicsDetails.Dispose(); }
           
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
