#region "Namespaces"

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
    public partial class gloPatientCopyAccountControl : UserControl
    {

        #region " C1 Constants "

        //Addd By Mahesh S(Apollo)
        private const int COL_AccountPatientId = 1;
        private const int COL_PAccountId = 2;
        private const int COL_PatientId = 3;
        private const int COL_AccountNo = 4;
        private const int COL_PatientCode = 5;
        private const int COL_FirstName = 6;
        private const int COL_MiddleName = 7;
        private const int COL_LastName = 8;
        private const int COL_SSNNO = 9;
        private const int COL_PatientDOB = 10;
        private const int COL_Status = 11;

        //Added by SaiKrishna
        private const int COL_OwnAccount = 12;
        private const int COL_PatientDisplayName = 13;

        #endregion

        #region " Variables "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        string _databaseconnectionstring = "";
        string sGuarantorCode = "";
        string _MessageBoxCaption = string.Empty;
        string _sAccountDesc = "";
        Int64 _nAccountId;
        Int64 _nPatientId;
        Int64 _nGuarantorId;
        Int64 nClinicId = 1;
        Int64 rowNo = 1;
        DateTime dtAccountCloseDate;
        DataTable dtAccountPatient = new DataTable();
        
        Account oAccount;
        PatientAccount oPatientAccount;
        IList<PatientAccount> oListAccountPatient;
        gloListControl.gloListControl oListControl = null;
        gloGeneralItem.gloItems _selectedItemsColl;
        
        //gloPatientGuarantorControl ogloPatientGuarantorControl = null;

        gloPAGuarantorControl ogloPatientGuarantorControl = null;

        PatientOtherContacts oPatientGuarantors = null;
        gloAddressControl oAddressControl = null;
        PatientGuardian oPatientGuardian = null;
        PatientDemographics oPatientDemographicsDetails = null;
        bool IsCmbSameAsGuardianLoadFlag = true;
        gloAccount objgloAccount = null;
        ToolTip oToolTip1 = new ToolTip();

        ComboBox combo;

        #endregion

        #region " Properties "

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

        #endregion

        #region " Constructors "

        //Added By Mahesh Satlapalli (Apollo)

        //Empty constructor
        public gloPatientCopyAccountControl()
        {
            InitializeComponent();
        }

        //Connection string need to pass.
        public gloPatientCopyAccountControl(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
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

        //PatientId, GuarantorID and AccountIDs assigned to local variables.
        public gloPatientCopyAccountControl(string databaseconnectionstring, Int64 patientId, Int64 guarantorId, Int64 accountId)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            oPatientGuarantors = new PatientOtherContacts();
            PatientGuardianDetails = new PatientGuardian(databaseconnectionstring);
            oPatientDemographicsDetails = new PatientDemographics();
            objgloAccount = new gloAccount(databaseconnectionstring);
            _nAccountId = accountId;
            _nPatientId = patientId;
            _nGuarantorId = guarantorId;

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

        private void gloPatientCopyAccountControl_Load(object sender, EventArgs e)
        {
            try
            {
                oToolTip1.SetToolTip(btnNewGuarantor, "Add Guarantor");
                oAddressControl = new gloAddressControl(_databaseconnectionstring);
                oAddressControl.Dock = DockStyle.Fill;
                pnlAddresssControl.Controls.Add(oAddressControl);

                //Form load time counts the account patients.
                rowNo = gvAccountPatient.Rows.Count;

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
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        #endregion

        #region " Button Events "

        //Added By Mahesh Satlapalli (Apollo)
        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            if (_nAccountId == 0)
            {
                MessageBox.Show("Select Account.", _MessageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAccountNo.Focus();
                return;
            }
            try
            {
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, true, true, this.Width);
                oListControl.ControlHeader = "Patients";
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_PatientSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                this.Controls.Add(oListControl);

                //To allow the user to add multiple guarantors at one time 
                for (Int32 _PatientCount = 1; _PatientCount < gvAccountPatient.Rows.Count; _PatientCount++)
                {
                    //Added by Mahesh Satlapalli:2011-01-06(yyyy-mm-dd) For Existing patient as guarantor(based on patienid)
                    if (Convert.ToInt64(gvAccountPatient.Rows[_PatientCount][COL_PatientId].ToString()) > 0)
                        oListControl.SelectedItems.Add(Convert.ToInt64(gvAccountPatient.Rows[_PatientCount][COL_PatientId].ToString()), gvAccountPatient.Rows[_PatientCount][COL_FirstName].ToString());
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

        //Added by Mahesh Satlapalli (Apollo)
        private void btnPatientDeactivate_Click(object sender, EventArgs e)
        {
            if (_nAccountId == 0)
            {
                MessageBox.Show("Select Account.", _MessageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAccountNo.Focus();
                return;
            }
            if (btnPatientDeactivate.Text == "Deactivate Patient")
            {
                if (gvAccountPatient.Rows.Count > 1)
                {
                    if (ConfirmDeactivateStatus())
                    {
                        gvAccountPatient.Rows[gvAccountPatient.RowSel][COL_Status] = "DeActive";
                        btnPatientDeactivate.Text = "Activate Patient";
                        btnPatientDeactivate.Tag = "Activate Patient";
                    }
                }
            }
            else
            {
                if (gvAccountPatient.Rows.Count > 1)
                {
                    gvAccountPatient.Rows[gvAccountPatient.RowSel][COL_Status] = "Active";
                    btnPatientDeactivate.Text = "Deactivate Patient";
                    btnPatientDeactivate.Tag = "Deactivate Patient";
                }
            }
        }

        //Added By Mahesh Satlapalli(Apollo)
        private void gvPatient_Click(object sender, EventArgs e)
        {
            ActivateDeactivateButtonTextChange();
        }

        //Added By Mahesh Satlapalli(Apollo)
        private void btnRemovePatient_Click(object sender, EventArgs e)
        {
            if (_nAccountId == 0)
            {
                MessageBox.Show("Select Account.", _MessageBoxCaption , MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAccountNo.Focus();
                return;
            }
            try
            {
                if (gvAccountPatient.Rows.Count > 2)
                {
                    DialogResult res = MessageBox.Show("Are you sure? Do you want to delete Patient?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (res == DialogResult.Yes)
                    {
                        gvAccountPatient.Cols[COL_AccountPatientId].Visible = true;

                        dtAccountPatient.Rows.Remove(dtAccountPatient.Select("PatientId=" + gvAccountPatient.Rows[gvAccountPatient.RowSel]["PatientId"])[0]);
                        gvAccountPatient.Cols[COL_AccountPatientId].Visible = false;
                        FillAccountPatients(dtAccountPatient);
                        ActivateDeactivateButtonTextChange();
                    }
                    else
                    {
                        return;
                    }
                }
                else if (gvAccountPatient.Rows.Count == 2)
                {
                    MessageBox.Show("Account should have at least one Patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        //Added by Mahesh Satlapalli-Apollo on 5/Jan/2011-Purpose : Get the exist patients from patient screen
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
                        if (dtSelectedPatient != null)
                        {
                            if (dtSelectedPatient.Rows.Count > 0)
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
                            dtSelectedPatient.Dispose();
                            dtSelectedPatient = null;
                        }
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

        #region " Address control related Events "

        //Code Added by Mahesh Satlapalli 
        private void txtAccZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //_bZipChanged = true;
                if (!(e.KeyChar == Convert.ToChar(8)))
                {
                    if (Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void txtAccZip_Leave(object sender, EventArgs e)
        {
            if (txtAccZip.Text.Trim() != "" && Regex.IsMatch(txtAccZip.Text.Trim(), @"^[0-9]+$") == true)
            {
                DataTable dt = new System.Data.DataTable();
                gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDb.Connect(false);

                try
                {
                    string qry = "SELECT City,ST,County FROM CSZ_MST where ZIP = " + txtAccZip.Text.Trim() + "";
                    oDb.Retrive_Query(qry, out dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cmbState.Text = Convert.ToString(dt.Rows[0]["ST"]);

                        if (txtAccCity.Text.Trim() == "")
                            txtAccCity.Text = dt.Rows[0]["City"] == DBNull.Value ? string.Empty : dt.Rows[0]["City"].ToString();

                        txtAccCounty.Text = dt.Rows[0]["County"] == DBNull.Value ? string.Empty : dt.Rows[0]["County"].ToString();
                        cmbCountry.Text = "US";
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

        #endregion

        #region " Guarantor related Events "

        //Code Added by Mahesh Satlapalli 
        private void btnGuarantorExistingPatientBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    for (Int32 _ControlsCount = this.Controls.Count - 1; _ControlsCount >= 0; _ControlsCount--)
                    {
                        if (this.Controls[_ControlsCount].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[_ControlsCount]);
                            break;
                        }
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
                for (Int32 _ControlsCount = this.Controls.Count - 1; _ControlsCount >= 0; _ControlsCount--)
                {
                    if (this.Controls[_ControlsCount].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[_ControlsCount]);
                        break;
                    }
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
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                }
                catch
                {
                }
                //oListControl.Dispose();
                //oListControl = null;
                onPatientAccountControl_Enter(sender, e);
            }

        }

        private void oListControl_GaurantorSelectedClick(object sender, EventArgs e)
        {
            gloPatient ogloPatient = new gloPatient(_databaseconnectionstring);

            try
            {
                Int64 _TempPatientID = 0;

                if (oListControl.SelectedItems.Count > 1)
                {
                    MessageBox.Show("Multiple guarantors per patient account are not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (oListControl.SelectedItems.Count > 0)
                {
                    //Remove Guarantor and add selected guarantor
                    if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                    {
                        oPatientGuarantors.RemoveAt(0);
                    }
                    for (Int16 _ItemCount = 0; _ItemCount <= oListControl.SelectedItems.Count - 1; _ItemCount++)
                    {
                        _TempPatientID = Convert.ToInt64(oListControl.SelectedItems[_ItemCount].ID);
                        bool _ShouldAdd = true;
                        if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                        {
                            for (Int32 _Count = 0; _Count < oPatientGuarantors.Count; _Count++)
                            {
                                if (Convert.ToInt64(oListControl.SelectedItems[_ItemCount].ID) == oPatientGuarantors[_Count].GuarantorAsPatientID)
                                {
                                    _ShouldAdd = false;
                                    break;
                                }
                            }
                        }
                        if (_ShouldAdd == true)
                        {
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
                                    oPatientGuarantors.Add(oGuarantor);
                                }
                                else
                                {
                                    if (oPatientGuarantors.Count >= 1)
                                    {
                                        MessageBox.Show("Multiple guarantors per patient account are not allowed.\nTo change guarantor remove existing guarantor.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        onPatientAccountControl_Enter(sender, e);
                                        if (oPatientTemp != null)
                                        {
                                            oPatientTemp.Dispose();
                                            oPatientTemp = null;
                                        }
                                        return;
                                    }
                                }
                                if (oPatientTemp != null)
                                {
                                    oPatientTemp.Dispose();
                                    oPatientTemp = null;
                                }
                                if (oGuarantor != null)
                                {
                                    oGuarantor.Dispose();
                                    oGuarantor = null;
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
                if (ogloPatient != null)
                {
                    ogloPatient.Dispose();
                    ogloPatient = null;
                }
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
                //ogloPatientGuarantorControl.FromAccountGuarantor = true;
                //ogloPatientGuarantorControl.PatientId = _nPatientId;
                //ogloPatientGuarantorControl.SaveButton_Click += new gloPatientGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                //ogloPatientGuarantorControl.CloseButton_Click += new gloPatientGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);
                //this.Controls.Add(ogloPatientGuarantorControl);
                //ogloPatientGuarantorControl.Dock = DockStyle.Fill;
                //ogloPatientGuarantorControl.BringToFront();
                //onPatientAccountControl_Leave(sender, e);
                if (ogloPatientGuarantorControl != null)
                {
                    try
                    {
                        ogloPatientGuarantorControl.SaveButton_Click -= new gloPAGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                        ogloPatientGuarantorControl.CloseButton_Click -= new gloPAGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);
                    }
                    catch
                    { }
                    ogloPatientGuarantorControl.Dispose();
                    ogloPatientGuarantorControl = null;
                }

                ogloPatientGuarantorControl = new gloPAGuarantorControl(_databaseconnectionstring);
                ogloPatientGuarantorControl.PatientGuarantors = oPatientGuarantors;
                ogloPatientGuarantorControl.FromAccountGuarantor = true;
                ogloPatientGuarantorControl.PatientId = _nPatientId;
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
                oPatientGuarantors = ogloPatientGuarantorControl.PatientGuarantors;
                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                {
                    for (Int32 gindex = 0; gindex < oPatientGuarantors.Count; gindex++)
                    {
                        txtAccGuarantor.Text = oPatientGuarantors[gindex].FirstName + " " + ((oPatientGuarantors[gindex].MiddleName != "") ? oPatientGuarantors[gindex].MiddleName + " " : "") + oPatientGuarantors[gindex].LastName;
                        oAddressControl.isFormLoading = true;
                        //fill Account Address with Selected Guarantor Address when it is not empty
                        if (oPatientGuarantors[gindex].AddressLine1.Trim().ToString() != "")
                        {
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
                { }
                onPatientAccountControl_Enter(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ogloPatientGuarantorControl_CloseButton_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(ogloPatientGuarantorControl);
            try
            {
                ogloPatientGuarantorControl.SaveButton_Click -= new gloPAGuarantorControl.SaveButtonClick(ogloPatientGuarantorControl_SaveButton_Click);
                ogloPatientGuarantorControl.CloseButton_Click -= new gloPAGuarantorControl.CloseButtonClick(ogloPatientGuarantorControl_CloseButton_Click);
            }
            catch
            { }
            onPatientAccountControl_Enter(sender, e);
        }

        private void cmbSameAsGuardian_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (IsCmbSameAsGuardianLoadFlag == true)
            {
                //Remove guarantor
                if (oPatientGuarantors != null && oPatientGuarantors.Count > 0)
                {
                    oPatientGuarantors.RemoveAt(0);
                }
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
                            oGuarantor.AddressLine1 = oPatientDemographicsDetails.PatientAddress1.ToString().Trim();
                            oGuarantor.AddressLine2 = oPatientDemographicsDetails.PatientAddress2.ToString().Trim();
                            oGuarantor.City = oPatientDemographicsDetails.PatientCity.ToString().Trim();
                            oGuarantor.County = oPatientDemographicsDetails.PatientCountry.ToString().Trim();
                            oGuarantor.Zip = oPatientDemographicsDetails.PatientZip.ToString().Trim();
                            oGuarantor.State = oPatientDemographicsDetails.PatientState.ToString().Trim();
                            oGuarantor.Country = oPatientDemographicsDetails.PatientCountry.ToString().Trim();
                            oGuarantor.County = oPatientDemographicsDetails.PatientCounty.ToString().Trim();
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
                                oPatientGuarantors.Add(oGuarantor);
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
                            //Contact Details
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
                                oPatientGuarantors.Add(oGuarantor);
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

                            //Contact details
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

                            //Address
                            oGuarantor.AddressLine1 = PatientGuardianDetails.PatientGuardianAddress1.Trim().ToString();
                            oGuarantor.AddressLine2 = PatientGuardianDetails.PatientGuardianAddress2.Trim().ToString();
                            oGuarantor.City = PatientGuardianDetails.PatientGuardianCity.Trim().ToString();
                            oGuarantor.County = PatientGuardianDetails.PatientGuardianCounty.Trim().ToString();
                            oGuarantor.Zip = PatientGuardianDetails.PatientGuardianZip.Trim().ToString();
                            oGuarantor.State = PatientGuardianDetails.PatientGuardianState.Trim().ToString();
                            oGuarantor.Country = PatientGuardianDetails.PatientGuardianCountry.Trim().ToString();

                            //Contact Details
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
            //code End Added by kanchan on 20130610 to solve bug :50859
            IsCmbSameAsGuardianLoadFlag = true;
        }

        #endregion
        
        #region " Account related Events "

        //Code Added by Mahesh Satlapalli 
        private void btnExistingAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (oListControl != null)
                {
                    for (Int32 _ControlsCount = this.Controls.Count - 1; _ControlsCount >= 0; _ControlsCount--)
                    {
                        if (this.Controls[_ControlsCount].Name == oListControl.Name)
                        {
                            this.Controls.Remove(this.Controls[_ControlsCount]);
                            break;
                        }
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
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                    }
                    catch
                    {
                    }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.GuarantorsAccounts, false, this.Width);
                oListControl.ControlHeader = "Guarantors Accounts";
                oListControl.PatientID = _nPatientId;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ExistingAccountSelectClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_AccountClosedClick);
                this.Controls.Add(oListControl);
                this.Width = 700;
                this.Height = 650;
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

        private void oListControl_ExistingAccountSelectClick(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                txtAccountNo.Text = "";
                txtAccountDescription.Text = "";
                //set frmAddPatientAccount width and height.
                this.Width = 395;
                this.Height = 275;

                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 _ItemCount = 0; _ItemCount <= oListControl.SelectedItems.Count - 1; _ItemCount++)
                    {
                        //AccountId
                        _nAccountId = oListControl.SelectedItems[_ItemCount].ID;
                        txtAccountNo.Tag = oListControl.SelectedItems[_ItemCount].ID;
                        txtAccountNo.Text = oListControl.SelectedItems[_ItemCount].Code;
                        txtAccountDescription.Text = oListControl.SelectedItems[_ItemCount].Description;
                        _sAccountDesc = txtAccountDescription.Text;

                        dt = GetAccountDetailsById(Convert.ToInt64(txtAccountNo.Tag.ToString()));

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            string guarantordetails = dt.Rows[0]["sFirstName"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFirstName"].ToString().Trim() + ' ' + (dt.Rows[0]["sMiddleName"] == DBNull.Value ? string.Empty : dt.Rows[0]["sMiddleName"].ToString()) + ' ' + (dt.Rows[0]["sLastName"] == DBNull.Value ? string.Empty : dt.Rows[0]["sLastName"].ToString()) + Environment.NewLine;

                            guarantordetails = guarantordetails + (dt.Rows[0]["sAddressLine1"] == DBNull.Value ? string.Empty : dt.Rows[0]["sAddressLine1"].ToString()) + ',';
                            guarantordetails = guarantordetails + (dt.Rows[0]["sAddressLine2"] == DBNull.Value ? string.Empty : dt.Rows[0]["sAddressLine2"].ToString()) + ',';
                            guarantordetails = guarantordetails + (dt.Rows[0]["sCity"] == DBNull.Value ? string.Empty : dt.Rows[0]["sCity"].ToString()) + ' '
                                                                + (dt.Rows[0]["sState"] == DBNull.Value ? string.Empty : dt.Rows[0]["sState"].ToString()) + ' '
                                                                + (dt.Rows[0]["sZip"] == DBNull.Value ? string.Empty : dt.Rows[0]["sZip"].ToString());
                            _nGuarantorId = Convert.ToInt64(dt.Rows[0]["nGuarantorId"].ToString());
                        }
                        SetData();
                    }
                }
                onPatientAccountControl_Enter(sender, e);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dt != null)
                    dt.Dispose();
            }
        }

        private void oListControl_AccountClosedClick(object sender, EventArgs e)
        {
            if (oListControl != null)
            {
                for (Int32 _ControlsCount = this.Controls.Count - 1; _ControlsCount >= 0; _ControlsCount--)
                {
                    if (this.Controls[_ControlsCount].Name == oListControl.Name)
                    {
                        this.Controls.Remove(this.Controls[_ControlsCount]);
                        //set frmAddPatientAccount width and height.
                        this.Width = 395;
                        this.Height = 275;
                        break;
                    }
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
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                }
                catch
                {
                }
            }
            onPatientAccountControl_Enter(sender, e);
        }

        #endregion
        
        #region " Private Methods "

        //Added by Mahesh Satlapalli (Apollo).

        //set the data to form controls
        private void SetData()
        {
            FillSameAsGuardian();
            FillGuarantors();
            FillData();
            FillGrid();
        }

        //fill same as guardian combo fill.
        private void FillSameAsGuardian()
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
        }

        //fill same as guarantors combo fill.
        private void FillGuarantors()
        {
            DataTable dtGurantorDetails;
            oPatientGuarantors = new PatientOtherContacts();
            PatientOtherContact oGuarantor = null;
            gloAccount objgloAccount = new gloAccount(_databaseconnectionstring);

            //GetAccountGuarantors by AccountId and clicnicId
            dtGurantorDetails = objgloAccount.GetAccountGuarantors(_nAccountId, nClinicId);

            if (dtGurantorDetails != null && dtGurantorDetails.Rows.Count > 0)
            {
                for (Int32 _GuarantorCount = 0; _GuarantorCount < dtGurantorDetails.Rows.Count; _GuarantorCount++)
                {
                    oGuarantor = new PatientOtherContact();
                    oGuarantor.PatientID = Convert.ToInt64(dtGurantorDetails.Rows[_GuarantorCount]["nPatientID"].ToString());
                    oGuarantor.PatientContactID = 0;
                    oGuarantor.GuarantorAsPatientID = Convert.ToInt64(dtGurantorDetails.Rows[_GuarantorCount]["nPatientID"].ToString());
                    oGuarantor.IsActive = true;
                    oGuarantor.FirstName = dtGurantorDetails.Rows[_GuarantorCount]["sFirstName"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sFirstName"].ToString();
                    oGuarantor.MiddleName = dtGurantorDetails.Rows[_GuarantorCount]["sMiddleName"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sMiddleName"].ToString();
                    oGuarantor.LastName = dtGurantorDetails.Rows[_GuarantorCount]["sLastName"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sLastName"].ToString();
            
                    if (dtGurantorDetails.Rows[_GuarantorCount]["nDOB"] != null && dtGurantorDetails.Rows[_GuarantorCount]["nDOB"].ToString() != "")
                        oGuarantor.DOB = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtGurantorDetails.Rows[_GuarantorCount]["nDOB"]));
                    else
                        oGuarantor.DOB = DateTime.MinValue;

                    oGuarantor.SSN = dtGurantorDetails.Rows[_GuarantorCount]["sSSN"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sSSN"].ToString();
                    oGuarantor.Gender = dtGurantorDetails.Rows[_GuarantorCount]["sGender"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sGender"].ToString();
                    oGuarantor.AddressLine1 = dtGurantorDetails.Rows[_GuarantorCount]["sAddressLine1"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sAddressLine1"].ToString();
                    oGuarantor.AddressLine2 = dtGurantorDetails.Rows[_GuarantorCount]["sAddressLine2"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sAddressLine2"].ToString();
                    oGuarantor.City = dtGurantorDetails.Rows[_GuarantorCount]["sCity"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sCity"].ToString();
                    oGuarantor.State = dtGurantorDetails.Rows[_GuarantorCount]["sState"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sState"].ToString();
                    oGuarantor.Zip = dtGurantorDetails.Rows[_GuarantorCount]["sZip"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sZip"].ToString();
                    oGuarantor.Country = dtGurantorDetails.Rows[_GuarantorCount]["sCountry"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sCountry"].ToString();
                    oGuarantor.County = dtGurantorDetails.Rows[_GuarantorCount]["sCounty"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sCounty"].ToString();
                    oGuarantor.Relation = dtGurantorDetails.Rows[_GuarantorCount]["sRelation"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sRelation"].ToString();
                    oGuarantor.IsActive = Convert.ToBoolean(dtGurantorDetails.Rows[_GuarantorCount]["bIsActive"].ToString());
                    oGuarantor.Phone = dtGurantorDetails.Rows[_GuarantorCount]["sPhone"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sPhone"].ToString();
                    oGuarantor.Mobile = dtGurantorDetails.Rows[_GuarantorCount]["sMobile"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sMobile"].ToString();
                    oGuarantor.Email = dtGurantorDetails.Rows[_GuarantorCount]["sEmail"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sEmail"].ToString();
                    oGuarantor.Fax = dtGurantorDetails.Rows[_GuarantorCount]["sFax"] == DBNull.Value ? string.Empty : dtGurantorDetails.Rows[_GuarantorCount]["sFax"].ToString();
                    oGuarantor.nGuarantorTypeFlag = Convert.ToInt32(dtGurantorDetails.Rows[_GuarantorCount]["nPatientContactTypeFlag"]);
                    oGuarantor.OtherConatctType = (PatientOtherContactType)Convert.ToInt32(dtGurantorDetails.Rows[_GuarantorCount]["nPatientContactType"]);
                    oGuarantor.GurantorType = (GuarantorType)Convert.ToInt32(dtGurantorDetails.Rows[_GuarantorCount]["nGuarantorType"]);
                    oGuarantor.IsAccountGuarantor = Convert.ToBoolean(dtGurantorDetails.Rows[_GuarantorCount]["bIsAccountGuarantor"].ToString());
                    oPatientGuarantors.Add(oGuarantor);
                }
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
        }

        //fill account data
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

                if (dtAccount.Rows.Count > 0)
                {
                    oAccount.PAccountID = dtAccount.Rows[0]["PatientAccountId"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtAccount.Rows[0]["PatientAccountId"].ToString());
                    oAccount.AccountNo = dtAccount.Rows[0]["AccountNo"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["AccountNo"].ToString();
                    oAccount.AccountDesc = dtAccount.Rows[0]["AccountDesc"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["AccountDesc"].ToString();

                    oAccount.nBusinessCenterID = Convert.ToInt64(dtAccount.Rows[0]["nBusinessCenterID"] == DBNull.Value ? "0" : dtAccount.Rows[0]["nBusinessCenterID"].ToString());

                    oAccount.GuarantorID = dtAccount.Rows[0]["nGuarantorID"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtAccount.Rows[0]["nGuarantorID"].ToString());
                    _nGuarantorId = oAccount.GuarantorID;
                    oAccount.GuarantorCode = dtAccount.Rows[0]["sGuarantorCode"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["sGuarantorCode"].ToString();
                    oAccount.FirstName = dtAccount.Rows[0]["GuarantorName"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["GuarantorName"].ToString();
                    oAccount.AccountClosedDate = Convert.ToDateTime(dtAccount.Rows[0]["dtAccountClosedDate"].ToString() == "" ? DateTime.MinValue.ToString() : dtAccount.Rows[0]["dtAccountClosedDate"].ToString());
                    oAccount.AddressLine1 = dtAccount.Rows[0]["Address1"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["Address1"].ToString();
                    oAccount.AddressLine2 = dtAccount.Rows[0]["Address2"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["Address2"].ToString();
                    oAccount.City = dtAccount.Rows[0]["City"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["City"].ToString();
                    oAccount.State = dtAccount.Rows[0]["State"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["State"].ToString();
                    oAccount.Zip = dtAccount.Rows[0]["Zip"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["Zip"].ToString();
                    oAccount.Country = dtAccount.Rows[0]["Country"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["Country"].ToString();
                    oAccount.County = dtAccount.Rows[0]["County"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["County"].ToString();
                    oAccount.AreaCode = dtAccount.Rows[0]["AreaCode"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["AreaCode"].ToString();
                    oAccount.ExcludeStatement = Convert.ToBoolean(dtAccount.Rows[0]["bIsExcludeStatement"]);
                    oAccount.SentToCollection = Convert.ToBoolean(dtAccount.Rows[0]["bIsSentToCollection"]);
                    oAccount.ClinicID = dtAccount.Rows[0]["nClinicID"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtAccount.Rows[0]["nClinicID"].ToString());
                    oAccount.SiteID = dtAccount.Rows[0]["nSiteID"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtAccount.Rows[0]["nSiteID"].ToString());
                    oAccount.UserID = dtAccount.Rows[0]["nUserID"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtAccount.Rows[0]["nUserID"].ToString());
                    oAccount.MachineName = dtAccount.Rows[0]["sMachineName"] == DBNull.Value ? string.Empty : dtAccount.Rows[0]["sMachineName"].ToString();
                    oAccount.RecordDate = dtAccount.Rows[0]["dtRecordDate"] == DBNull.Value ? DateTime.MinValue : dtAccount.Rows[0]["dtRecordDate"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(dtAccount.Rows[0]["dtRecordDate"].ToString());
                    oAccount.Active = Convert.ToBoolean(dtAccount.Rows[0]["IsActive"].ToString());

                    txtAccountNo.Text = oAccount.AccountNo;
                    txtAccountDescription.Text = oAccount.AccountDesc;
                    sGuarantorCode = oAccount.GuarantorCode;

                    FillBusinessCenter(oAccount.nBusinessCenterID);

                    cmbBusinessCenter.SelectedValue = oAccount.nBusinessCenterID;


                    oAddressControl.isFormLoading = true;

                    //Added by Mayuri : 20151006-2.	Update Guarantor Address if Guarantor is “Same as Patient” when patient address is updated.

                    if (cmbSameAsGuardian.Text == "Patient")
                    {
                        oAddressControl.txtAddress1.Text = oPatientDemographicsDetails.PatientAddress1.ToString().Trim();
                        oAddressControl.txtAddress2.Text =  oPatientDemographicsDetails.PatientAddress2.ToString().Trim();
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

        private void FillGrid()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            try
            {
                oParameters.Add("@nPAccountId", _nAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_Select_Accounts_Patients", oParameters, out dtAccountPatient);
                FillAccountPatients(dtAccountPatient);

                //loading time change the button status
                ActivateDeactivateButtonTextChange();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception gex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }
        }

        //Design DataGrid.
        private void FillAccountPatients(DataTable dtAccount)
        {
            gvAccountPatient.DataSource = dtAccount;

            gvAccountPatient.Cols[COL_AccountPatientId].Caption = "AccountPatientId";
            gvAccountPatient.Cols[COL_PAccountId].Caption = "PAccountId";
            gvAccountPatient.Cols[COL_PatientId].Caption = "PatientId";
            gvAccountPatient.Cols[COL_AccountNo].Caption = "AccountNo";
            gvAccountPatient.Cols[COL_PatientCode].Caption = "Code";
            gvAccountPatient.Cols[COL_FirstName].Caption = "First Name";
            gvAccountPatient.Cols[COL_MiddleName].Caption = "MI";
            gvAccountPatient.Cols[COL_LastName].Caption = "Last Name";
            gvAccountPatient.Cols[COL_SSNNO].Caption = "SSN";
            gvAccountPatient.Cols[COL_PatientDOB].Caption = "DOB";
            gvAccountPatient.Cols[COL_Status].Caption = "Status";
            //Added by SaiKrishna
            gvAccountPatient.Cols[COL_OwnAccount].Caption = "Own Account";
            gvAccountPatient.Cols[COL_PatientDisplayName].Caption = "PatientDisplayName";

            gvAccountPatient.Cols[0].Visible = false;

            gvAccountPatient.Cols[COL_AccountPatientId].Visible = false;
            gvAccountPatient.Cols[COL_PAccountId].Visible = false;
            gvAccountPatient.Cols[COL_AccountNo].Visible = false;
            gvAccountPatient.Cols[COL_PatientId].Visible = false;
            //Added by SaiKrishna
            gvAccountPatient.Cols[COL_OwnAccount].Visible = false;
            gvAccountPatient.Cols[COL_PatientDisplayName].Visible = false;

            gvAccountPatient.Cols[COL_PatientCode].Width = 80;
            gvAccountPatient.Cols[COL_FirstName].Width = 150;
            gvAccountPatient.Cols[COL_MiddleName].Width = 40;
            gvAccountPatient.Cols[COL_LastName].Width = 150;
            gvAccountPatient.Cols[COL_SSNNO].Width = 90;
            gvAccountPatient.Cols[COL_PatientDOB].Width = 80;
            gvAccountPatient.Cols[COL_Status].Width = 80;

            Int32 afterRowNo = gvAccountPatient.Rows.Count;

            //Added on 03-Feb-2011:for select the first row of Grid(When Grid is empty) after added Patients.
            if (rowNo == 1 && afterRowNo > 1)
                gvAccountPatient.Select(1, 0);

            ActivateDeactivateButtonTextChange();
            gvAccountPatient.ScrollBars = ScrollBars.Both;
        }

        //Save and Update Account.
        public void SaveAccount()
        {
            #region "Patient Guarantors"

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbParams = new gloDatabaseLayer.DBParameters();
            object accountId = 0;

            try
            {
                if (oAccount != null)
                {
                    oDB.Connect(false);
                    odbParams.Clear();
                    //Account params
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

                    foreach (PatientAccount actPatient in oListAccountPatient)
                    {
                        odbParams.Clear();
                        odbParams.Dispose();
                        odbParams = new gloDatabaseLayer.DBParameters();
                        odbParams.Add("@nAccountPatientID", actPatient.AccountPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@nPAccountID", accountId, ParameterDirection.Input, SqlDbType.BigInt);
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
                        //Added by SaiKrishna
                        odbParams.Add("@bIsOwnAccount", actPatient.OwnAccount, ParameterDirection.Input, SqlDbType.Bit);
                        oDB.ExecuteWithTransaction("PA_InUp_Accounts_Patients", odbParams);
                    }
                }
                if (oPatientGuarantors != null)
                {
                    for (Int32 _GuarantorCount = 0; _GuarantorCount < PatientGuarantors.Count; _GuarantorCount++)
                    {
                        Object GuarantorId;
                        odbParams.Clear();

                        odbParams.Add("@nPatientID", Convert.ToInt64(_nPatientId), ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@nPatientContactID", oPatientGuarantors[_GuarantorCount].PatientContactID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                        odbParams.Add("@nLineNumber", _GuarantorCount, ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@nPatientContactType", oPatientGuarantors[_GuarantorCount].OtherConatctType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                        odbParams.Add("@sFirstName", PatientGuarantors[_GuarantorCount].FirstName, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sMiddleName", PatientGuarantors[_GuarantorCount].MiddleName, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sLastName", PatientGuarantors[_GuarantorCount].LastName, ParameterDirection.Input, SqlDbType.VarChar);
                     
                        if (PatientGuarantors[_GuarantorCount].DOB != null && PatientGuarantors[_GuarantorCount].DOB != DateTime.MinValue)
                            odbParams.Add("@nDOB", gloDateMaster.gloDate.DateAsNumber(PatientGuarantors[_GuarantorCount].DOB.ToShortDateString()), ParameterDirection.Input, SqlDbType.Int);
                        else
                            odbParams.Add("@nDOB", DBNull.Value, ParameterDirection.Input, SqlDbType.Int);
                        
                        odbParams.Add("@sSSN", PatientGuarantors[_GuarantorCount].SSN, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sGender", PatientGuarantors[_GuarantorCount].Gender, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sRelation", PatientGuarantors[_GuarantorCount].Relation, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sAddressLine1", PatientGuarantors[_GuarantorCount].AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sAddressLine2", PatientGuarantors[_GuarantorCount].AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sCity", PatientGuarantors[_GuarantorCount].City, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sState", PatientGuarantors[_GuarantorCount].State, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sZIP", PatientGuarantors[_GuarantorCount].Zip, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sPhone", PatientGuarantors[_GuarantorCount].Phone, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sMobile", PatientGuarantors[_GuarantorCount].Mobile, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sFax", PatientGuarantors[_GuarantorCount].Fax, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sEmail", PatientGuarantors[_GuarantorCount].Email, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@bIsActive", PatientGuarantors[_GuarantorCount].IsActive, ParameterDirection.Input, SqlDbType.Bit);
                        odbParams.Add("@nVisitID", PatientGuarantors[_GuarantorCount].VisitID, ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@nAppointmentID", PatientGuarantors[_GuarantorCount].AppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@nGuarantorAsPatientID", PatientGuarantors[_GuarantorCount].GuarantorAsPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@nPatientContactTypeFlag", PatientGuarantors[_GuarantorCount].nGuarantorTypeFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                        odbParams.Add("@sCounty", PatientGuarantors[_GuarantorCount].County, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@sCountry", PatientGuarantors[_GuarantorCount].Country, ParameterDirection.Input, SqlDbType.VarChar);
                        odbParams.Add("@nClinicID", nClinicId, ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@nGuarantorType", PatientGuarantors[_GuarantorCount].GurantorType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                        odbParams.Add("@nPAccountID", accountId, ParameterDirection.Input, SqlDbType.BigInt);
                        odbParams.Add("@bIsAccountGuarantor", PatientGuarantors[_GuarantorCount].IsAccountGuarantor, ParameterDirection.Input, SqlDbType.Bit);

                        oDB.Execute("PA_INUP_PatientGuarantor", odbParams, out GuarantorId);
                    }
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
                odbParams.Dispose();
            }

            #endregion

        }

        //Added By Mahesh Satlapalli (Apollo) for Prepare Guarantor,Account and Patient Objects.
        private void PrepareAccountAndAcountPatient()
        {
            oListAccountPatient = new List<PatientAccount>();

            oAccount.PAccountID = 0;
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
            oAccount.AccountClosedDate =oAccount.Active == false? DateTime.Today : DateTime.MinValue;
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
                    //assign guarantor as account guarantor.
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

                        //assign entered address if gurantor address is empty.
                        //if (oPatientGuarantors[gIndex].AddressLine1.ToString().Trim() == "")
                        //{
                        //    oPatientGuarantors[gIndex].AddressLine1 = oAddressControl.txtAddress1.Text;
                        //    oPatientGuarantors[gIndex].AddressLine2 = oAddressControl.txtAddress2.Text;
                        //    oPatientGuarantors[gIndex].City = oAddressControl.txtCity.Text;
                        //    oPatientGuarantors[gIndex].State = oAddressControl.cmbState.Text;
                        //    oPatientGuarantors[gIndex].Zip = oAddressControl.txtZip.Text;
                        //    oPatientGuarantors[gIndex].County = oAddressControl.txtCounty.Text;
                        //    oPatientGuarantors[gIndex].Country = oAddressControl.cmbCountry.Text;
                        //}
                    }
                }
            }
           
            if (dtAccountPatient.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAccountPatient.Rows)
                {
                    oPatientAccount = new PatientAccount();
                    oPatientAccount.AccountPatientID = 0;
                    oPatientAccount.PAccountID = 0;
                    oPatientAccount.PatientID = dr["PatientId"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dr["PatientId"].ToString());
                    oPatientAccount.PatientCode = dr["PatientCode"] == DBNull.Value ? string.Empty : dr["PatientCode"].ToString();
                    nClinicId = Convert.ToInt64(appSettings["ClinicID"].ToString());
                    oPatientAccount.ClinicID = nClinicId;
                    oPatientAccount.SiteID = Convert.ToInt64("1");
                    oPatientAccount.MachineName = System.Windows.Forms.SystemInformation.ComputerName;
                    oPatientAccount.UserID = Convert.ToInt64(appSettings["UserID"].ToString());
                    oPatientAccount.RecordDate = DateTime.Today;
                    
                    if (dr["Status"].ToString() == "Active")
                        oPatientAccount.Active = true;
                    else
                    {
                        oPatientAccount.Active = false;
                        oPatientAccount.AccountClosedDate = DateTime.Today;
                    }

                    //This account is other patients existing account.
                    if (Convert.ToInt64(dr["PatientId"].ToString()) != _nPatientId)
                        oPatientAccount.OwnAccount = false;
                    else
                    {
                        oPatientAccount.OwnAccount = true;
                    }
                    oListAccountPatient.Add(oPatientAccount);
                    oPatientAccount = null;
                }
            }

        }

        private PatientOtherContact.GuarantorTypeFlag GetNextTypeFlag(bool CallFromSameAsPatient)
        {
            PatientOtherContact.GuarantorTypeFlag _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.None;

            bool isPrimaryPresent = false;
            bool isSecondaryPresent = false;
            bool isTertioryPresent = false;

            if (oPatientGuarantors != null && oPatientGuarantors.Count != 0)
            {
                for (Int32 _GuarantorCount = 0; _GuarantorCount < oPatientGuarantors.Count; _GuarantorCount++)
                {
                    if (oPatientGuarantors[_GuarantorCount].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Primary.GetHashCode())
                    { isPrimaryPresent = true; }
                    else if (oPatientGuarantors[_GuarantorCount].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Secondary.GetHashCode())
                    { isSecondaryPresent = true; }
                    else if (oPatientGuarantors[_GuarantorCount].nGuarantorTypeFlag == PatientOtherContact.GuarantorTypeFlag.Tertiary.GetHashCode())
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
                _GuarantorTypeFlag = PatientOtherContact.GuarantorTypeFlag.Primary;
            
            return _GuarantorTypeFlag;
        }

        //Validations
        private bool ValidateData()
        {
            //Condition added by Mahesh Satlapalli (Apollo)
            if (txtAccountNo.Text.Trim().Length == 0)
            {
                MessageBox.Show("Enter Acct.#.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAccountNo.Focus();
                return false;
            }
            //if (txtAccountDescription.Text.ToString().Trim().Length == 0)
            //{
            //    MessageBox.Show("Enter Acct. Desc.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtAccountDescription.Focus();
            //    return false;
            //}
            else if (CheckAccountNoExistsForGuarantor(txtAccountNo.Text.Trim()) == true)
            {
                string strMessage = "Acct.# " + txtAccountNo.Text.Trim() + " already exists.";
                MessageBox.Show(strMessage, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (txtAccGuarantor.Text == "")
            {
                MessageBox.Show("Select guarantor for Account.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //Added by SaiKrishna for Address validation
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
            return true;
        }

        //Get Account and AccountPatients.
        public bool GetData()
        {
            bool _result = false;
            if (ValidateData() == true)
            {
                //Original account has multiple patients 
                if (dtAccountPatient.Rows.Count > 1)
                {
                    if (MessageBox.Show("Original account has multiple members.\nThese patients will also become members of the new account.", _MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        PrepareAccountAndAcountPatient();
                        _result = true;
                    }
                    else
                    {
                        _result = false;
                    }
                }
                else
                {
                    PrepareAccountAndAcountPatient();
                    _result = true;
                }
                
            }
            else
            {
                _result= false;
            }
            return _result;
        }

        private bool CheckAccountNoExistsForGuarantor(string accountNo)
        {

            object result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);

            try
            {
                string _strSqlQuery = "select COUNT(*) from PA_Accounts where sAccountNo='" + accountNo.Trim().Replace("'", "''") + "'";
                result = oDB.ExecuteScalar_Query(_strSqlQuery);
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
                oDB.Disconnect();
                oDB.Dispose();
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

        //Added By Mahesh S(Apollo) for Get Patient Guarantors
        private void GetPatientGuarantor()
        {
            DataTable dtGuarantors;
            DataTable dtPatientId = new DataTable();
            Patient opatient = new Patient();
            DataTable dt;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbparam = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            try
            {
                string _strSqlQuery = "Select nPatientId  from Patient_OtherContacts where nPatientContactID = " + _nGuarantorId;

                oDB.Retrive_Query(_strSqlQuery, out dtPatientId);

                if (dtPatientId.Rows.Count > 0)
                    _nPatientId = Convert.ToInt64(dtPatientId.Rows[0][0].ToString());

                //for getting Patient Guarantors.
                _strSqlQuery = "";
                _strSqlQuery = "SELECT nPatientID, nPatientContactID, nLineNumber,ISNULL(nPatientContactType,0) AS nPatientContactType, "
                 + " ISNULL(sFirstName,'') AS sFirstName,ISNULL(sMiddleName,'') AS sMiddleName,ISNULL(sLastName,'') AS sLastName,"
                 + " nDOB,ISNULL(sSSN,'') AS sSSN,ISNULL(sGender,'') AS sGender,ISNULL(sRelation,'') As sRelation,ISNULL(sAddressLine1,'') As sAddressLine1,"
                 + " ISNULL(sAddressLine2,'') AS sAddressLine2,ISNULL(sCity,'') AS sCity,ISNULL(sState,'') AS sState,ISNULL(sZIP,'') AS sZIP,ISNULL(sCounty,'') AS sCounty,ISNULL(sCountry,'') AS sCountry,"
                 + " ISNULL(sPhone,'') AS sPhone,ISNULL(sMobile,'') AS sMobile,ISNULL(sFax,'') AS sFax,ISNULL(sEmail,'') AS sEmail,"
                 + " ISNULL(nVisitID,0) AS nVisitID,ISNULL(nAppointmentID,0) As nAppointmentID,ISNULL(bIsActive,'false') As  bIsActive,ISNULL(nGuarantorAsPatientID,0) AS nGuarantorAsPatientID,ISNULL(nPatientContactTypeFlag,4) AS nPatientContactTypeFlag,nClinicID "
                 + " FROM Patient_OtherContacts WHERE nPatientId=" + _nPatientId + " ORDER BY nPatientContactTypeFlag";

                oDB.Retrive_Query(_strSqlQuery, out dtGuarantors);
            
                if (dtGuarantors != null && dtGuarantors.Rows.Count > 0)
                {
                    oPatientGuarantors = new PatientOtherContacts();
                    for (Int32 _GuarantorCount = 0; _GuarantorCount < dtGuarantors.Rows.Count; _GuarantorCount++)
                    {
                        PatientOtherContact oGuarantor = new PatientOtherContact();
                        oGuarantor.PatientID = dtGuarantors.Rows[_GuarantorCount]["nPatientID"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtGuarantors.Rows[_GuarantorCount]["nPatientID"].ToString());
                        oGuarantor.PatientContactID = Convert.ToInt64(dtGuarantors.Rows[_GuarantorCount]["nPatientContactID"]);
                        oGuarantor.FirstName = dtGuarantors.Rows[_GuarantorCount]["sFirstName"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sFirstName"].ToString();
                        oGuarantor.MiddleName = dtGuarantors.Rows[_GuarantorCount]["sMiddleName"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sMiddleName"].ToString();
                        oGuarantor.LastName = dtGuarantors.Rows[_GuarantorCount]["sLastName"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sLastName"].ToString();
                        oGuarantor.DOB = dtGuarantors.Rows[_GuarantorCount]["nDOB"] == DBNull.Value ? DateTime.MinValue : dtGuarantors.Rows[_GuarantorCount]["nDOB"].ToString() == "" ? DateTime.MinValue : gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtGuarantors.Rows[_GuarantorCount]["nDOB"]));
                        oGuarantor.SSN = dtGuarantors.Rows[_GuarantorCount]["sSSN"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sSSN"].ToString();
                        oGuarantor.Relation = dtGuarantors.Rows[_GuarantorCount]["sRelation"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sRelation"].ToString();
                        oGuarantor.Gender = dtGuarantors.Rows[_GuarantorCount]["sGender"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sGender"].ToString();
                        oGuarantor.AddressLine1 = dtGuarantors.Rows[_GuarantorCount]["sAddressLine1"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sAddressLine1"].ToString();
                        oGuarantor.AddressLine2 = dtGuarantors.Rows[_GuarantorCount]["sAddressLine2"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sAddressLine2"].ToString();
                        oGuarantor.City = dtGuarantors.Rows[_GuarantorCount]["sCity"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sCity"].ToString();
                        oGuarantor.State =dtGuarantors.Rows[_GuarantorCount]["sState"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sState"].ToString();
                        oGuarantor.County = dtGuarantors.Rows[_GuarantorCount]["sCounty"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sCounty"].ToString();
                        oGuarantor.Country = dtGuarantors.Rows[_GuarantorCount]["sCountry"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sCountry"].ToString();
                        oGuarantor.Zip = dtGuarantors.Rows[_GuarantorCount]["sZIP"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sZIP"].ToString();
                        oGuarantor.Phone = dtGuarantors.Rows[_GuarantorCount]["sPhone"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sPhone"].ToString();
                        oGuarantor.Mobile = dtGuarantors.Rows[_GuarantorCount]["sMobile"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sMobile"].ToString();
                        oGuarantor.Email = dtGuarantors.Rows[_GuarantorCount]["sEmail"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sEmail"].ToString();
                        oGuarantor.Fax = dtGuarantors.Rows[_GuarantorCount]["sFax"] == DBNull.Value ? string.Empty : dtGuarantors.Rows[_GuarantorCount]["sFax"].ToString();
                        oGuarantor.IsActive = Convert.ToBoolean(dtGuarantors.Rows[_GuarantorCount]["bIsActive"]);
                        oGuarantor.VisitID = Convert.ToInt64(dtGuarantors.Rows[_GuarantorCount]["nVisitID"]);
                        oGuarantor.AppointmentID = Convert.ToInt64(dtGuarantors.Rows[_GuarantorCount]["nAppointmentID"]);
                        oGuarantor.GuarantorAsPatientID =dtGuarantors.Rows[_GuarantorCount]["nGuarantorAsPatientID"] == DBNull.Value ? Convert.ToInt64("0") : Convert.ToInt64(dtGuarantors.Rows[_GuarantorCount]["nGuarantorAsPatientID"]);
                        oGuarantor.nGuarantorTypeFlag = Convert.ToInt32(dtGuarantors.Rows[_GuarantorCount]["nPatientContactTypeFlag"]);
                        oGuarantor.OtherConatctType = (PatientOtherContactType)Convert.ToInt32(dtGuarantors.Rows[_GuarantorCount]["nPatientContactType"]);
                        oPatientGuarantors.Add(oGuarantor);
                    }
                }

                odbparam.Add("@PatienID", _nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                odbparam.Add("@ClinicID", nClinicId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("PA_Select_Patient", odbparam, out  dt);

                oPatientGuardian = new PatientGuardian(_databaseconnectionstring);
                oPatientDemographicsDetails = new PatientDemographics();

                oPatientDemographicsDetails.PatientCode = dt.Rows[0]["sPatientCode"] == DBNull.Value ? string.Empty : dt.Rows[0]["sPatientCode"].ToString();

                //sMother_Address1, sMother_Address2, sMother_City, sMother_State, sMother_ZIP, 
                oPatientGuardian.PatientMotherAddress1 = dt.Rows[0]["sMother_Address1"] == DBNull.Value ? string.Empty : dt.Rows[0]["sMother_Address1"].ToString();
                oPatientGuardian.PatientMotherAddress2 = dt.Rows[0]["sMother_Address2"] == DBNull.Value ? string.Empty : dt.Rows[0]["sMother_Address2"].ToString();
                oPatientGuardian.PatientMotherCity = dt.Rows[0]["sMother_City"] == DBNull.Value ? string.Empty : dt.Rows[0]["sMother_City"].ToString();
                oPatientGuardian.PatientMotherState = dt.Rows[0]["sMother_State"] == DBNull.Value ? string.Empty : dt.Rows[0]["sMother_State"].ToString();
                oPatientGuardian.PatientMotherZip = dt.Rows[0]["sMother_ZIP"] == DBNull.Value ? string.Empty : dt.Rows[0]["sMother_ZIP"].ToString();

                //sMother_County, sMother_Phone, sMother_Mobile, sMother_FAX, sMother_Email, sFather_fName,
                oPatientGuardian.PatientMotherCounty = dt.Rows[0]["sMother_County"] == DBNull.Value ? string.Empty : dt.Rows[0]["sMother_County"].ToString();
                oPatientGuardian.PatientMotherCountry = dt.Rows[0]["sMother_Country"] == DBNull.Value ? string.Empty : dt.Rows[0]["sMother_Country"].ToString();
                oPatientGuardian.PatientMotherPhone = dt.Rows[0]["sMother_Phone"] == DBNull.Value ? string.Empty : dt.Rows[0]["sMother_Phone"].ToString();
                oPatientGuardian.PatientMotherMobile = dt.Rows[0]["sMother_Mobile"] == DBNull.Value ? string.Empty : dt.Rows[0]["sMother_Mobile"].ToString();
                oPatientGuardian.PatientMotherFAX = dt.Rows[0]["sMother_FAX"] == DBNull.Value ? string.Empty : dt.Rows[0]["sMother_FAX"].ToString();
                oPatientGuardian.PatientMotherEmail = dt.Rows[0]["sMother_Email"] == DBNull.Value ? string.Empty : dt.Rows[0]["sMother_Email"].ToString();
                oPatientGuardian.PatientFatherFirstName = dt.Rows[0]["sFather_fName"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_fName"].ToString();

                //sFather_mName, sFather_lName, sFather_Address1, sFather_Address2, sFather_City, 
                oPatientGuardian.PatientFatherMiddleName = dt.Rows[0]["sFather_mName"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_mName"].ToString();
                oPatientGuardian.PatientFatherLastName = dt.Rows[0]["sFather_lName"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_lName"].ToString();
                oPatientGuardian.PatientFatherAddress1 = dt.Rows[0]["sFather_Address1"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_Address1"].ToString();
                oPatientGuardian.PatientFatherAddress2 = dt.Rows[0]["sFather_Address2"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_Address2"].ToString();
                oPatientGuardian.PatientFatherCity = dt.Rows[0]["sFather_City"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_City"].ToString();

                //sFather_State, sFather_ZIP, sFather_County, sFather_Phone, sFather_Mobile, sFather_FAX, 
                oPatientGuardian.PatientFatherState = dt.Rows[0]["sFather_State"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_State"].ToString();
                oPatientGuardian.PatientFatherZip = dt.Rows[0]["sFather_ZIP"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_ZIP"].ToString();
                oPatientGuardian.PatientFatherCounty =dt.Rows[0]["sFather_County"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_County"].ToString();
                oPatientGuardian.PatientFatherCountry = dt.Rows[0]["sFather_Country"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_Country"].ToString();
                oPatientGuardian.PatientFatherPhone = dt.Rows[0]["sFather_Phone"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_Phone"].ToString();
                oPatientGuardian.PatientFatherMobile = dt.Rows[0]["sFather_Mobile"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_Mobile"].ToString();
                oPatientGuardian.PatientFatherFAX = dt.Rows[0]["sFather_FAX"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_FAX"].ToString();

                //sFather_Email, sGuardian_fName, sGuardian_mName, sGuardian_lName, sGuardian_Address1, 
                oPatientGuardian.PatientFatherEmail = dt.Rows[0]["sFather_Email"] == DBNull.Value ? string.Empty : dt.Rows[0]["sFather_Email"].ToString();
                oPatientGuardian.PatientGuardianFirstName = dt.Rows[0]["sGuardian_fName"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_fName"].ToString();
                oPatientGuardian.PatientGuardianMiddleName = dt.Rows[0]["sGuardian_mName"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_mName"].ToString();
                oPatientGuardian.PatientGuardianLastName = dt.Rows[0]["sGuardian_lName"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_lName"].ToString();
                oPatientGuardian.PatientGuardianAddress1 = dt.Rows[0]["sGuardian_Address1"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_Address1"].ToString();

                //sGuardian_Address2, sGuardian_City, sGuardian_State, sGuardian_ZIP, sGuardian_County, 
                oPatientGuardian.PatientGuardianAddress2 = dt.Rows[0]["sGuardian_Address2"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_Address2"].ToString();
                oPatientGuardian.PatientGuardianCity = dt.Rows[0]["sGuardian_City"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_City"].ToString();
                oPatientGuardian.PatientGuardianState = dt.Rows[0]["sGuardian_State"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_State"].ToString();
                oPatientGuardian.PatientGuardianZip = dt.Rows[0]["sGuardian_ZIP"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_ZIP"].ToString();
                oPatientGuardian.PatientGuardianCounty = dt.Rows[0]["sGuardian_County"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_County"].ToString();
                oPatientGuardian.PatientGuardianCountry = dt.Rows[0]["sGuardian_Country"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_Country"].ToString();

                //sGuardian_Phone, sGuardian_Mobile, sGuardian_FAX, sGuardian_Email, nPatientDirective, 
                oPatientGuardian.PatientGuardianPhone = dt.Rows[0]["sGuardian_Phone"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_Phone"].ToString();
                oPatientGuardian.PatientGuardianMobile = dt.Rows[0]["sGuardian_Mobile"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_Mobile"].ToString();
                oPatientGuardian.PatientGuardianFAX = dt.Rows[0]["sGuardian_FAX"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_FAX"].ToString();
                oPatientGuardian.PatientGuardianEmail = dt.Rows[0]["sGuardian_Email"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_Email"].ToString();


                //start ::Patient Guardian relationship
                oPatientGuardian.PatientGuardianRelationCD = dt.Rows[0]["sGuardian_RelationshipCD"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_RelationshipCD"].ToString();
                oPatientGuardian.PatientGuardianRelationDS = dt.Rows[0]["sGuardian_RelationshipDS"] == DBNull.Value ? string.Empty : dt.Rows[0]["sGuardian_RelationshipDS"].ToString();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                odbparam.Dispose();
                oDB.Disconnect();
                oDB.Dispose();

                if (dtPatientId != null)
                    dtPatientId.Dispose();
            }

        }

        //btnPatientDeactivate Button Text change.
        private void ActivateDeactivateButtonTextChange()
        {
            if (gvAccountPatient.Rows.Count > 1)
            {
                if (gvAccountPatient.Rows[gvAccountPatient.RowSel][COL_Status].ToString().ToLower() == "deactive")
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

        //Added By Mahesh (Apollo) on 20-Jan-2011 fro Get Patient Account Count
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
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }
            return blnRetvalue;
        }

        // Added by Mahesh Satlapalli (Apollo) - Purpose: Confirmation from user Deactivate all patients
        private bool ConfirmDeactivateStatus()
        {
            if (!ValidStatus())
            {
                DialogResult res = MessageBox.Show("Account has no other active Patient. Are you sure?" + Environment.NewLine + "Do you want to deactivate selected Patient?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }

        // Added by Mahesh Satlapalli (Apollo)- Purpose : Check the active patients count
        private bool ValidStatus()
        {
            Int32 activaPatCount = 0;

            foreach (C1.Win.C1FlexGrid.Row oRow in gvAccountPatient.Rows)
            {
                if (oRow[COL_Status].ToString().ToUpper() == "ACTIVE")
                {
                    activaPatCount = activaPatCount + 1;
                }
                // last patient record with active status
                if (activaPatCount > 1)
                {
                    return true;
                }
            }

            return false;
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

        private DataTable GetAccountDetailsById(Int64 accountId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtAccountDetails = new DataTable();
            oDB.Connect(false);

            try
            {
                string _strSqlQuery = "Select nPAccountID, sAccountNo, sAccountDesc, nGuarantorID, sGuarantorCode, dtAccountClosedDate, sFirstName,sMiddleName,";
                _strSqlQuery = _strSqlQuery + " sLastName,nEntityType,sAddressLine1,sAddressLine2,sCity,sState,sZip,sCountry,sAreaCode,bIsExcludeStatement,";
                _strSqlQuery = _strSqlQuery + " bIsSentToCollection,nClinicID,nSiteID,sMachineName,nUserID,dtRecordDate,bIsActive ";
                _strSqlQuery = _strSqlQuery + " from PA_Accounts ";
                _strSqlQuery = _strSqlQuery + " where nPAccountID = " + accountId;

                oDB.Retrive_Query(_strSqlQuery, out dtAccountDetails);
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
                oDB.Disconnect();
                oDB.Dispose();
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
                if (oAccount != null) { oAccount.Dispose(); }
                if (oPatientAccount != null) { oPatientAccount.Dispose(); }
                if (ogloPatientGuarantorControl != null) { ogloPatientGuarantorControl.Dispose(); }
                if (oAddressControl != null) { oAddressControl.Dispose(); }
                if (oPatientGuardian != null) { oPatientGuardian.Dispose(); }
                if (oPatientDemographicsDetails != null) { oPatientDemographicsDetails.Dispose(); }
                if (objgloAccount != null) { objgloAccount.Dispose(); }
                if (oListControl != null) { oListControl.Dispose(); }
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
