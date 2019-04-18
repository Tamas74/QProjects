using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using gloPMGeneral.gloPriorAuthorization;
using gloSettings;




namespace gloPMGeneral
{
    public partial class frmSetupAuthorization : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private Int64 _PatientID = 0;
        public string _PriorAuthorizationNo = "";
        public Int64 _PriorAuthorizationID = 0;
        public Int64 _ReferralID = 0;
        private gloListControl.gloListControl oListControl;
        private ComboBox combo;
        bool _isAdded = false;
        bool _tracklimit = false;
        ToolTip tooltip_Rpt = new ToolTip();
        #endregion " Declarations "
        private Int64 _ContactID = 0;

        #region " Property Procedures "

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

        public bool IsAdded
        {
            get { return _isAdded; }
            set { _isAdded = value; }
        }
        #endregion " Property Procedures "

        #region " Constructor "

        public frmSetupAuthorization(Int64 PatientID)
        {
            InitializeComponent();

            _UserID = AppSettings.UserID;
            _ClinicID = AppSettings.ClinicID;
            _PatientID = PatientID;
            _databaseconnectionstring = AppSettings.ConnectionStringPM;

            cmbReferralProvider.DrawMode = DrawMode.OwnerDrawFixed;
            cmbReferralProvider.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbGenInfoInsurance.DrawMode = DrawMode.OwnerDrawFixed;
            cmbGenInfoInsurance.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

        }

        #endregion " Constructor "

        #region " Private Methods "

        void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
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
                        if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth)
                            this.tooltip_Rpt.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 180, e.Bounds.Bottom);
                    }
                    else
                    {
                        tooltip_Rpt.Hide(combo);
                    }
                }
                else
                {
                    tooltip_Rpt.Hide(combo);
                }
                e.DrawFocusRectangle();
            }
        }

        private void oListControl_ModifyFormHandlerClick(object sender, EventArgs e)
        {

            if (oListControl.ControlHeader == "Referral Provider")
            {
                if (oListControl.dgListView.CurrentRow != null)
                {
                    _ContactID = Convert.ToInt64(oListControl.dgListView["nContactID", oListControl.dgListView.CurrentRow.Index].Value);
                }
                if (oListControl.dgListView.Rows.Count != 0)
                {
                    gloContacts.frmSetupPhysician ofrmModifyContact = new gloContacts.frmSetupPhysician(_ContactID, _databaseconnectionstring);
                    ofrmModifyContact.ShowDialog(this);

                    if (ofrmModifyContact.DialogResult == DialogResult.OK)
                    {
                        // _Ismodify = true;
                        oListControl.FillListAsCriteria(ofrmModifyContact.ContactID);

                        // oListControl.FillListAsCriteria1(ofrmModifyContact.ContactID, true);

                    }
                    ofrmModifyContact.Dispose();
                }

            }
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            int width = 0;
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



        private bool Validate()
        {
            try
            {
                if (txtauth.Text.Trim() == "")
                {
                    MessageBox.Show("Enter prior authorization #.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtauth.Focus();
                    return false;
                }
                if (rdbreferralout.Checked == false)
                {
                    if (chklimityes.Checked == false && chklimitno.Checked == false)
                    {
                        MessageBox.Show("Select track authorization limits.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chklimitno.Focus();
                        return false;
                    }
                }

              

                if (rdbreferralout.Checked == false)
                {
                    if (chklimityes.Checked == true)
                    {
                        if (mskAuthorizationstart.Text.Replace("/", "").Trim().Length == 0)
                        {
                            MessageBox.Show("Enter start date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskAuthorizationstart.Focus();
                            return false;
                        }
                        if (mskauthexp.Text.Replace("/", "").Trim().Length == 0)
                        {
                            MessageBox.Show("Enter expiration date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskauthexp.Focus();
                            return false;
                        }

                        if (IsValidDate(mskAuthorizationstart.Text.Trim()) == false)
                        {
                            MessageBox.Show("Enter valid date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskAuthorizationstart.Focus();
                            return false;
                        }
                        if (IsValidDate(mskauthexp.Text.Trim()) == false)
                        {
                            MessageBox.Show("Enter valid date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskauthexp.Focus();
                            return false;
                        }

                        if (cmbGenInfoInsurance.Text.Trim() == "" && txtInsnote.Text.Trim() == "")
                        {
                            MessageBox.Show("Insurance information is required for track authorization limits.\nSelect the patient's insurance plan or describe the insurance in the insurance note.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbGenInfoInsurance.Focus();
                            return false;
                        }
                      
                        if (Convert.ToDateTime(mskAuthorizationstart.Text.Trim()).Date > Convert.ToDateTime(mskauthexp.Text.Trim()).Date)
                        {
                            MessageBox.Show("Expiration date should be equal to or greater than start date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskauthexp.Focus();
                            return false;
                        }
                    }
                }
                if (IsNumberUsed(txtauth.Text.Trim(), _PatientID, ClinicID) == true)
                {
                    if (DialogResult.Yes != MessageBox.Show("Prior authorization " + txtauth.Text.Trim() + " already exists for patient " + FillPatient(_PatientID) + ".\nContinue? ", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        return false;
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {

            }
        }

        private bool IsNumberUsed(string _AuthNo, Int64 PatientID, Int64 ClinicID)
        {
            string _strquery = "";
            gloDatabaseLayer.DBLayer ODB = null;
            object _retval = null;
            bool _result = false;
            try
            {
                ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                ODB.Connect(false);
                _strquery = "Select  Count(PriorAuthorization_Mst.nPriorAuthorizationID) from  PriorAuthorization_Mst WITH(NOLOCK) " +
                          "where ISNULL(PriorAuthorization_Mst.sPriorAuthorizationNo,'')='" + _AuthNo.Replace("'", "''") + "' and " +
                          " PriorAuthorization_Mst.nPatientID=" + PatientID + " and PriorAuthorization_Mst.nClinicID=" + ClinicID + " ";
                _retval = ODB.ExecuteScalar_Query(_strquery);
                if (_retval != null && Convert.ToInt64(_retval) > 0)
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
                if (ODB != null)
                {
                    ODB.Dispose();
                    ODB.Disconnect();
                }
            }
            return _result;
        }



        private bool IsValidDate(object strDate)
        {
            bool Success;
            try
            {
                DateTime validatedDate;
                Success = DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);
                if (validatedDate != null && Success == true)
                {
                    if (validatedDate < DateTime.MaxValue && validatedDate >= Convert.ToDateTime("01/01/1900"))
                    {
                        Success = true;
                    }
                    else
                    {
                        Success = false;
                    }

                }
            }
            catch (FormatException)
            {
                Success = false; // If this line is reached, an exception was thrown

            }
            return Success;
        }
        private bool SavePriorAuthorization()
        {
            
            try
            {
                _PriorAuthorizationNo = txtauth.Text;
                clsgloPriorAuthorization objgloPriorAuth = new clsgloPriorAuthorization();
                objgloPriorAuth.PriorAuthorizationNo = txtauth.Text.Trim();
                objgloPriorAuth.PatientID = _PatientID;
                if (rdbreferralin.Checked == true)
                {
                    objgloPriorAuth.ReferralID = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                }
                else if (rdbreferralout.Checked == true)
                {
                    objgloPriorAuth.ToReferralID = Convert.ToInt64(txttoprovider.Tag);
                }
                else if (rdbboth.Checked == true)
                {
                    objgloPriorAuth.ReferralID = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                    objgloPriorAuth.ToReferralID = Convert.ToInt64(txttoprovider.Tag);
                }
                objgloPriorAuth.InsuranceID = Convert.ToInt64(cmbGenInfoInsurance.SelectedValue);
                if (mskAuthorizationstart.Enabled == false)
                {
                    objgloPriorAuth.StartDate = 0;
                }
                else
                {
                    if (mskAuthorizationstart.MaskCompleted == true)
                    {
                        mskAuthorizationstart.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        objgloPriorAuth.StartDate = gloDateMaster.gloDate.DateAsNumber(mskAuthorizationstart.Text);
                    }
                }
                if (mskauthexp.Enabled == false)
                {
                    objgloPriorAuth.ExpDate = 0;
                }
                else
                {
                    if (mskauthexp.MaskCompleted == true)
                    {
                        mskauthexp.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        objgloPriorAuth.ExpDate = gloDateMaster.gloDate.DateAsNumber(mskauthexp.Text);
                    }
                }

                if (rdbreferralout.Checked == false)
                {
                    if (chklimityes.Checked == true)
                    {
                        objgloPriorAuth.IsTrackAuthLimit = true;
                    }
                    else
                    {
                        objgloPriorAuth.IsTrackAuthLimit = false;
                    }
                }
                objgloPriorAuth.IsActive = true;
                if (txtvisitsallow.Text.ToString().Trim() == "")
                {
                    objgloPriorAuth.VisitsAllowed = null;
                }
                else
                {
                    objgloPriorAuth.VisitsAllowed = Convert.ToInt64(txtvisitsallow.Text.ToString().Trim());
                }
                objgloPriorAuth.InsuranceNote = txtInsnote.Text.Trim();
                if (rdbreferralin.Checked == true)
                {
                    objgloPriorAuth.AuthorizationType = clsgloPriorAuthorization.AuthorizationTypeEnum.ReferralIn.GetHashCode();
                }
                else if (rdbreferralout.Checked == true)
                {
                    objgloPriorAuth.AuthorizationType = clsgloPriorAuthorization.AuthorizationTypeEnum.ReferralOut.GetHashCode();
                }
                else if (rdbboth.Checked == true)
                {
                    objgloPriorAuth.AuthorizationType = clsgloPriorAuthorization.AuthorizationTypeEnum.Both.GetHashCode();
                }

                objgloPriorAuth.AuthorizationNote = txtAuthorizationNote.Text.ToString().Trim();

                objgloPriorAuth.Add();
                _PriorAuthorizationID = objgloPriorAuth.PriorAuthorizationID;
                _ReferralID = Convert.ToInt64(cmbReferralProvider.SelectedValue.ToString());
                _isAdded = true;

                objgloPriorAuth.Dispose();
                objgloPriorAuth = null;
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {

            }

        }

        private void FillReferralProviders(Int64 PatientId)
        {
            try
            {
                DataTable dt;
                dt = GetPatientReferral(PatientId);

                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["nReferralID"] = 0;
                    dr["sReferralName"] = "";
                    dt.Rows.InsertAt(dr, 0);

                    if (dt.Rows.Count > 0)
                    {
                        cmbReferralProvider.DataSource = dt;
                        cmbReferralProvider.ValueMember = dt.Columns["nReferralID"].ColumnName;
                        cmbReferralProvider.DisplayMember = dt.Columns["sReferralName"].ColumnName;
                        cmbReferralProvider.Refresh();

                        if (cmbReferralProvider.Items.Count > 1)
                        { cmbReferralProvider.SelectedIndex = 0; }
                        else
                        { cmbReferralProvider.SelectedIndex = 0; }
                    }

                }
                dt = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }

        }

        private DataTable GetPatientReferral(Int64 PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtReferrals = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT  ISNULL(Contacts_MST.nContactID,0) as nReferralID, " +
                                " ISNULL(Contacts_MST.sFirstName,'')+SPACE(1)+ISNULL(Contacts_MST.sMiddleName,'') +SPACE(1)+ISNULL(Contacts_MST.sLastName,'') AS sReferralName  " +
                                " FROM Contacts_MST WITH(NOLOCK) LEFT OUTER JOIN Contacts_Physician_DTL WITH(NOLOCK) ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID " +
                                " WHERE(ISNULL(Contacts_MST.bIsBlocked,0) = 0) AND (Contacts_MST.sContactType = 'Physician')  and Contacts_MST.nContactID IN " +
                                " (Select nContactID from Patient_DTL WITH(NOLOCK) where nPatientID=" + PatientId + " and ISNULL(nContactFlag,0)=3) order by sReferralName";

                oDB.Retrive_Query(_sqlQuery, out dtReferrals);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                //if (dtReferrals != null) { dtReferrals.Dispose(); }
            }

            return dtReferrals;
        }

        private void FillInsurance(Int64 PatientId)
        {
            try
            {
                DataTable dt;
                dt = GetPatientInsurance(PatientId);

                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["nInsuranceID"] = 0;
                    dr["sInsuranceName"] = "";
                    dt.Rows.InsertAt(dr, 0);

                    if (dt.Rows.Count > 0)
                    {
                        cmbGenInfoInsurance.DataSource = dt;
                        cmbGenInfoInsurance.ValueMember = dt.Columns["nInsuranceID"].ColumnName;
                        cmbGenInfoInsurance.DisplayMember = dt.Columns["sInsuranceName"].ColumnName;
                        cmbGenInfoInsurance.Refresh();

                        if (cmbGenInfoInsurance.Items.Count > 1)
                        { cmbGenInfoInsurance.SelectedIndex = 1; }
                        else
                        { cmbGenInfoInsurance.SelectedIndex = 0; }
                    }


                }
                dt = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }

        }

        private DataTable GetPatientInsurance(Int64 PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtInsurances = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT nInsuranceID,sInsuranceName,CASE ISNULL(nInsuranceFlag, 0) WHEN 0 THEN 4 WHEN 1 THEN 1 WHEN 2 THEN 2 WHEN 3 THEN 3 END As SortOrder FROM PatientInsurance_DTL WITH(NOLOCK) WHERE nPatientID=" + PatientId +" ORDER BY SortOrder";
                oDB.Retrive_Query(_sqlQuery, out dtInsurances);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                //if (dtInsurances != null) { dtInsurances.Dispose(); }
            }

            return dtInsurances;
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            pnltlsStrip.Visible = true;
            //this.Width = 936;
            //this.Height = 345;
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            if (oListControl.SelectedItems.Count > 0)
            {
                //if (rdbreferralout.Checked == true)
                //{
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dtReferrals = null;
                string _sqlQuery = "";
                try
                {
               
                    oDB.Connect(false);
                    for (int _Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                    {
                        txttoprovider.Tag = oListControl.SelectedItems[0].ID;


                        _sqlQuery = "SELECT ISNULL(Contacts_MST.sFirstName,'')+SPACE(1)+ISNULL(Contacts_MST.sMiddleName,'') +SPACE(1)+ISNULL(Contacts_MST.sLastName,'') AS sReferralName  " +
                                    " FROM Contacts_MST WITH(NOLOCK) where  ISNULL(Contacts_MST.nContactID,0)=" + oListControl.SelectedItems[0].ID;
                        oDB.Retrive_Query(_sqlQuery, out dtReferrals);
                        txttoprovider.Text = dtReferrals.Rows[0][0].ToString().Trim();

                    }
                    oDB.Disconnect();
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                    if (dtReferrals != null) { dtReferrals.Dispose(); }
                }
            }
            pnltlsStrip.Visible = true;

            //this.Width = 936;
            //this.Height = 345;
        }

        void oListControl_AddFormHandlerClick(object sender, EventArgs e)
        {
            if (oListControl.ControlHeader == "Referral Provider")
            {

                gloContacts.frmSetupPhysician ofrmAddContact = new gloContacts.frmSetupPhysician(_databaseconnectionstring);
                ofrmAddContact.ShowDialog(this);
                ofrmAddContact.Dispose();
                oListControl.FillListAsCriteria(ofrmAddContact.ContactID);

            }
        }

        private string FillPatient(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPatient = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT sPatientCode +'-'+dbo.GET_NAME(sFirstName, sMiddleName, sLastName) AS Patientname FROM Patient WITH(NOLOCK) WHERE nPatientID=" + PatientID;
                oDB.Retrive_Query(_sqlQuery, out dtPatient);
                if (dtPatient != null && dtPatient.Rows.Count > 0)
                {
                    lblPatientName1.Text = dtPatient.Rows[0]["Patientname"].ToString().Trim();
                    return Convert.ToString(dtPatient.Rows[0]["Patientname"]).Trim();
                }
                else
                {
                    MessageBox.Show("Select Patient", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return "";
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtPatient != null) { dtPatient.Dispose(); }
            }
            return "";
        }

        #endregion " Private Methods "

        #region " Form Load "

        private void frmSetupAuthorization_Load_1(object sender, EventArgs e)
        {
            FillPatient(_PatientID);
            FillInsurance(_PatientID);
            FillReferralProviders(_PatientID);
            txttoprovider.Visible = false;
            label16.Visible = false;
            btnToProvider.Visible = false;
            rdbreferralin.Checked = true;
            btnrefremove.Visible = false;
            btnremove.Visible = false;

            mskauthexp.Enabled = false;
            mskAuthorizationstart.Enabled = false;
            txtvisitsallow.Enabled = false;
            txtvisitsallow.Text = "";
            txtauth.Focus();
            txtauth.Select();
        }

        #endregion

        #region " Form Control Event"

        private void btnAdd_Referral_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._PatientID > 0)
                {

                    if (rdbreferralout.Checked == true)
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
                                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                                }
                                catch { }
                                try
                                {
                                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                                }
                                catch { }
                                try
                                {
                                    oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                                }
                                catch { }
                                try
                                {
                                    oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                                }
                                catch { }
                            }
                            catch
                            {
                            }
                            oListControl.Dispose();
                            oListControl = null;
                        }
                        pnltlsStrip.Visible = false;
                        oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Referrals, false, this.Width);
                        oListControl.ClinicID = _ClinicID;
                        oListControl.ControlHeader = "Referral Provider";

                        oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                        oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                        //this.Width = 680;
                        //this.Height = 530;
                        this.Controls.Add(oListControl);
                        oListControl.BringToFront();
                        oListControl.Dock = DockStyle.Fill;
                        oListControl.OpenControl();
                        //this.Width = 680;
                        //this.Height = 530;
                    }

                    else
                    {

                        //if (this._PatientID > 0)
                        //{

                        //    if (oListControl != null)
                        //    {
                        //        for (int i = this.Controls.Count - 1; i >= 0; i--)
                        //        {
                        //            if (this.Controls[i].Name == oListControl.Name)
                        //            {
                        //                this.Controls.Remove(this.Controls[i]);
                        //                break;
                        //            }
                        //        }
                        //    }
                        //    pnltlsStrip.Visible = false;
                        //    oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Referrals, true, this.Width);
                        //    oListControl.ClinicID = _ClinicID;
                        //    oListControl.ControlHeader = "Referral Provider";

                        //    oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        //    oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        //    oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                        //    //this.Width = 680;
                        //    //this.Height = 530;
                        //    this.Controls.Add(oListControl);

                        //    oListControl.OpenControl();
                        //    oListControl.Dock = DockStyle.Fill;
                        //    oListControl.BringToFront();
                        //this.Width = 680;
                        //this.Height = 530;
                        Int64 _currentPatientId = 0;
                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                        ogloPatient.ShowPatientRegistration(this._PatientID, gloPatient.ModifyPatientDetailType.Referral, out _currentPatientId,this);
                        FillPatient(_currentPatientId);
                        FillInsurance(_currentPatientId);
                        FillReferralProviders(_currentPatientId);
                        ogloPatient.Dispose();
                        ogloPatient = null;

                        //  }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {

            }
        }

        private void rdblimityes_CheckedChanged(object sender, EventArgs e)
        {
            if (chklimityes.Checked == true)
            {
                mskauthexp.Enabled = true;
                mskAuthorizationstart.Enabled = true;
                txtvisitsallow.Enabled = true;
            }
            else
            {
                mskauthexp.Enabled = false;
                mskAuthorizationstart.Enabled = false;
                txtvisitsallow.Enabled = false;
                txtvisitsallow.Text = "";
                mskauthexp.ResetText();
                mskAuthorizationstart.ResetText();
            }
        }

        private void txtvisitsallow_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != Convert.ToChar(8) && e.KeyChar != Convert.ToChar(46))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9]*$") == false)
                    {
                        e.Handled = true;
                    }
                }
                else if (e.KeyChar == Convert.ToChar(46))
                {
                    e.Handled = true;
                }


            }
            catch (System.OverflowException ex)
            {
                MessageBox.Show("Visits allow is invalid.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                return;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void radreferralin_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbreferralin.Checked == true)
            {
                cmbReferralProvider.Visible = true;
                txttoprovider.Visible = false;
                txttoprovider.Location = new Point(339, 38);
                label10.Text = "Referring Provider :";
                panel3.Enabled = true;
                label16.Visible = false;
                btnToProvider.Visible = false;
                btnrefremove.Visible = false;
                btnremove.Visible = false;
                
                txtauth.Focus();
                txtauth.Select();
            }
            else if (rdbreferralout.Checked == true)
            {
                btnToProvider.Visible = false;
                cmbReferralProvider.Visible = false;
                txttoprovider.Location = new Point(339, 38);
                txttoprovider.Visible = true;
                label10.Text = "To Provider :";
                panel3.Enabled = false;
                label16.Visible = false;
                btnToProvider.Visible = false;
                btnrefremove.Visible = true;
                btnremove.Visible = false;
                cmbGenInfoInsurance.SelectedIndex = 0;
                txtInsnote.Text = "";
                txtvisitsallow.Text = "";
                mskauthexp.ResetText();
                mskAuthorizationstart.ResetText();
                txtauth.Focus();
                txtauth.Select();
                chklimitno.Checked = false;
                chklimityes.Checked = false;
            }
            else if (rdbboth.Checked == true)
            {

                cmbReferralProvider.Visible = true;
                btnToProvider.Visible = true;
                label10.Text = "Referring Provider :";
                panel3.Enabled = true;
                txttoprovider.Location = new Point(339, 66);
                txttoprovider.Visible = true;
                label16.Visible = true;
                btnToProvider.Visible = true;
                btnrefremove.Visible = false;
                btnremove.Visible = true;
                txtauth.Focus();
                txtauth.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    }
                    catch { }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                    }
                    catch { }
                    try
                    {
                        oListControl.AddFormHandlerClick -= new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
                    }
                    catch { }
                    try
                    {
                        oListControl.ModifyFormHandlerClick -= new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
                    }
                    catch { }
                }
                catch
                {
                }
                oListControl.Dispose();
                oListControl = null;
            }
            pnltlsStrip.Visible = false;
            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Referrals, false, this.Width);
            oListControl.ClinicID = _ClinicID;
            oListControl.ControlHeader = "Referral Provider";

            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
            oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
            //this.Width = 680;
            //this.Height = 530;
            this.Controls.Add(oListControl);
            oListControl.BringToFront();
            oListControl.Dock = DockStyle.Fill;
            oListControl.OpenControl();
            //this.Width = 680;
            //this.Height = 530;
        }

        private void btnrefremove_Click(object sender, EventArgs e)
        {
            txttoprovider.Text = "";
            txttoprovider.Tag = 0;
        }

        private void btnremove_Click(object sender, EventArgs e)
        {
            txttoprovider.Text = "";
            txttoprovider.Tag = 0;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (SavePriorAuthorization())
                {
                    this.Close();
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbReferralProvider_MouseMove(object sender, MouseEventArgs e)
        {
            //combo = (ComboBox)sender;
            //if (cmbReferralProvider.SelectedItem != null)
            //{
            //    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbReferralProvider.Items[cmbReferralProvider.SelectedIndex])["sReferralName"]), cmbReferralProvider) >= cmbReferralProvider.DropDownWidth - 18)
            //        this.toolTip1.Show(Convert.ToString(((DataRowView)cmbReferralProvider.Items[cmbReferralProvider.SelectedIndex])["sReferralName"]), cmbReferralProvider, 0, cmbReferralProvider.Bottom - 40);
            //    else
            //        this.toolTip1.Hide(combo);

            //}
        }

        private void cmbGenInfoInsurance_MouseMove(object sender, MouseEventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbGenInfoInsurance.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbGenInfoInsurance.Items[cmbGenInfoInsurance.SelectedIndex])["sInsuranceName"]), cmbGenInfoInsurance) >= cmbGenInfoInsurance.DropDownWidth - 18)
                    this.toolTip1.Show(Convert.ToString(((DataRowView)cmbGenInfoInsurance.Items[cmbGenInfoInsurance.SelectedIndex])["sInsuranceName"]), cmbGenInfoInsurance, 0, cmbGenInfoInsurance.Bottom - 90);
                else
                    this.toolTip1.Hide(combo);

            }
        }

        private void cmbGenInfoInsurance_MouseHover(object sender, EventArgs e)
        {
            //combo = (ComboBox)sender;
            this.toolTip1.Hide(combo);

        }

        private void cmbReferralProvider_MouseLeave(object sender, EventArgs e)
        {
           //this.toolTip1.Hide(combo);
        }

        private void mskAuthorizationstart_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Enter valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }

        }

        private void mskauthexp_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Enter valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void mskAuthorizationstart_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void mskauthexp_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void chklimityes_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox chk = (CheckBox )sender;
            //if (chk.Name.ToString() == "chklimityes")
            //{
            //    if (chk.Checked == true)
            //    {
            //        chklimitno.Checked = false;
            //    }
            //    else if (chk.Checked == false)
            //    {
            //        chklimitno.Checked = false;
            //    }
            //}
            //else if (chk.Name.ToString() == "chklimitno")
            //{
            //    if (chk.Checked == true)
            //    {
            //        chklimityes.Checked = false;
            //    }
            //    else if (chk.Checked == false)
            //    {
            //        chklimitno.Checked = false;
            //    }
            //}
            if (_tracklimit == false)
            {
                _tracklimit = true;
                chklimitno.Checked = false;
            }
            if (chklimityes.Checked == true)
            {
                mskauthexp.Enabled = true;
                mskAuthorizationstart.Enabled = true;
                txtvisitsallow.Enabled = true;
                _tracklimit = false;
            }
            else
            {
                mskauthexp.Enabled = false;
                mskAuthorizationstart.Enabled = false;
                txtvisitsallow.Enabled = false;
                txtvisitsallow.Text = "";
                mskauthexp.ResetText();
                mskAuthorizationstart.ResetText();
                _tracklimit = false;
            }
        }

        private void chklimitno_CheckedChanged(object sender, EventArgs e)
        {
            if (_tracklimit == false)
            {
                _tracklimit = true;
                chklimityes.Checked = false;
            }
            if (chklimityes.Checked == true)
            {
                mskauthexp.Enabled = true;
                mskAuthorizationstart.Enabled = true;
                txtvisitsallow.Enabled = true;
                _tracklimit = false;
            }
            else
            {
                mskauthexp.Enabled = false;
                mskAuthorizationstart.Enabled = false;
                txtvisitsallow.Enabled = false;
                txtvisitsallow.Text = "";
                mskauthexp.ResetText();
                mskAuthorizationstart.ResetText();
                _tracklimit = false;
            }
        }

        #endregion

        private void cmbReferralProvider_MouseEnter(object sender, EventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbGenInfoInsurance.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbGenInfoInsurance.Items[cmbGenInfoInsurance.SelectedIndex])["sInsuranceName"]), cmbGenInfoInsurance) >= cmbGenInfoInsurance.DropDownWidth - 18)
                    this.toolTip1.Show(Convert.ToString(((DataRowView)cmbGenInfoInsurance.Items[cmbGenInfoInsurance.SelectedIndex])["sInsuranceName"]), cmbGenInfoInsurance, 0, cmbGenInfoInsurance.Bottom - 90);
                else
                    this.toolTip1.Hide(combo);

            }
        }

        
       
    
    }
}