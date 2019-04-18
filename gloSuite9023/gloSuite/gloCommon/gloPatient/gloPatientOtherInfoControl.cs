using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using gloPatient.Classes;
namespace gloPatient
{
    public partial class gloPatientOtherInfoControl : UserControl
    {
        #region " Declarations "
        private ComboBox combo;
        private string _databaseconnectionstring = "";
        private Int64 _clinicID = 0;
        private string _messageBoxCaption = "gloPM";
        private PatientDemographicOtherInfo _oPatientDemographicOtherInfo = null;
        private const int COL_SELECT = 0;
        private const int COL_WCOMPTYPE = 1;
        private const int COL_CLAIMNO = 2;
        private const int COL_FROMDATE = 3;
        private const int COL_TODATE = 4;
        private const int COL_NOTES = 5;
        private const int COL_NOTE_BTN = 6;
        private const int COL_COUNT = 7;
        private int _parentHeight = 0;

        private bool _IsWorkersCompModified = false;
        private gloListControl.gloListControl oListControl;
        private bool OpenForOtherDetails = true;
        #endregion "Declarations "

        #region Property Procedures

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }
        public Int64 ClinicID
        {
            get { return _clinicID; }
            set { _clinicID = value; }
        }
        public PatientDemographicOtherInfo PatientDemographicOtherInfo
        {
            get { return _oPatientDemographicOtherInfo; }
            set { _oPatientDemographicOtherInfo = value; }
        }
        private enum ColServiceLineType
        {
            None = 0, Claim = 1, ServiceLine = 2, Patient = 3
        }

        //private PatientDetails oMedicalCondition = null;

        #endregion

        #region Constructor


        public gloPatientOtherInfoControl(string DatabaseConnectionstring,string openFor="")
        {
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicID = 1; }
            }
            else
            { _clinicID = 1; }

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
            _databaseconnectionstring = DatabaseConnectionstring;
            _oPatientDemographicOtherInfo = new PatientDemographicOtherInfo();



            InitializeComponent();

            cmbPAMedicalCategory.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPAMedicalCategory.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbSexualOrientation.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSexualOrientation.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            cmbGenderIdentity.DrawMode = DrawMode.OwnerDrawFixed;
            cmbGenderIdentity.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

            if (openFor != "")
            {
                OpenForOtherDetails = false;
                panel3.Visible = false;
                pnlCMS1500Box13.Visible = false;
                pnlPreviousName.Visible = false;
                pnlSexualOrientationHeader.Visible = true;
                pnlBirthSex.Visible = true;
            }
            else
            {
                OpenForOtherDetails = true;
                pnlSexualOrientation.Visible = false;
                pnlSexualOrientationHeader.Visible = false;
                pnlOtherSexualOrientation.Visible = false;
                pnlGenderIdentity.Visible = false;
                pnlOtherGenderIdentity.Visible = false;
                pnlBirthSex.Visible = false;
                pnlMultipleBirthIndicator.Visible = false;
                pnlBirthOrder.Visible = false;
            }
        }

        #endregion

        #region "Delegates"

        public delegate void onOtherDetailsSaveClicked(object sender, EventArgs e);
        public event onOtherDetailsSaveClicked onOtherDetails_SaveClicked;

        public delegate void onOtherDetailsCloseClicked(object sender, EventArgs e);
        public event onOtherDetailsCloseClicked onOtherDetailsClose_Clicked;


        #endregion "Delegates"

        private void gloPatientOtherInfoControl_Load(object sender, EventArgs e)
        {
            gloCommon.Cls_TabIndexSettings tabSettings = null;
            _parentHeight = this.Parent.Parent.Parent.Height;
            try
            {
                FillGender();
                SetData();
                //GetPatientMedicalConditon(_oPatientDemographicOtherInfo.PatientID );
                txtLawyerName.Focus();
                tabSettings = new gloCommon.Cls_TabIndexSettings(this);
                tabSettings.SetTabOrder(gloCommon.Cls_TabIndexSettings.TabScheme.AcrossFirst);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }


        }
        private void FillGender()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dtBirthSex = null;
            try
            {
                string _sqlQuery = "SELECT  nCategoryid id,sCode,sDescription name FROM category_mst WHERE UPPER(sCategoryType) ='Birth Sex' AND bIsBlocked = '" + false + "' AND nClinicID = " + _clinicID + " order by sDescription ";
                oDB.Retrive_Query(_sqlQuery, out dtBirthSex);
                oDB.Disconnect();

                if (dtBirthSex != null)
                {
                    DataRow dr = dtBirthSex.NewRow();

                    dr["id"] = 0;
                    dr["name"] = "";
                    dtBirthSex.Rows.InsertAt(dr, 0);
                    dtBirthSex.AcceptChanges();
                    cmbBirthSex.BeginUpdate();
                    cmbBirthSex.DataSource = dtBirthSex;
                    cmbBirthSex.DisplayMember = "name";
                    cmbBirthSex.EndUpdate();
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

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        {

                            //Code Added by Mayuri:20091120-Version No-gloPM20101.1
                            //Notes should get save if user adds it without hit enter
                            mtxtSpousePhone.AllowValidate = false;

                            mtxtSpousePhone.AllowValidate = true;
                            //End code Added by Mayuri:20091120
                            if (Validate() == true)
                            {
                                //Dhruv 20091229 
                                //to validate the mask text box
                                if (mtxtSpousePhone.IsValidated == true)
                                {
                                    btnSaveOtherDetails_Click(null, null);

                                }
                            }

                            break;
                        }
                    case "Cancel":
                        {
                            btnCloseOtherDetails_Click(null, null);
                        }
                        break;

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        #region " Events "

        private void btnSaveOtherDetails_Click(object sender, EventArgs e)
        {
            if (GetSelectedCode(cmbSexualOrientation) == "OTH" && txtOtherSexualOrientation.Text.Trim() == "")
            {
                MessageBox.Show("Please specify other sexual orientation.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (GetSelectedCode(cmbGenderIdentity) == "OTH" && txtOtherGenderIdentity.Text.Trim() == "")
            {
                MessageBox.Show("Please specify other gender identity.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            this.Parent.Parent.Parent.Height = _parentHeight;
            _oPatientDemographicOtherInfo.PatientLawyer = txtLawyerName.Text.Trim();
            _oPatientDemographicOtherInfo.SpouseName = txtSpouseName.Text.Trim();

            _oPatientDemographicOtherInfo.SpousePhone = mtxtSpousePhone.Text.Trim();
            _oPatientDemographicOtherInfo.Status = cmbPatientStatus.Text.Trim();
            _oPatientDemographicOtherInfo.RegistrationDate = dtpRegDate.Value;
            _oPatientDemographicOtherInfo.SOFDate = dtpSignatureDate.Value;
            _oPatientDemographicOtherInfo.CMS1500Box13 = cmbCMS1500Box13.Text.Trim();
            _oPatientDemographicOtherInfo.MedicalConditions.Clear();
            for (int i = 0; i < cmbPAMedicalCategory.Items.Count; i++)
            {
                DataRow drCmb = ((DataRowView)cmbPAMedicalCategory.Items[i]).Row;
                long catId = Convert.ToInt64(drCmb[0]);
                if (!_oPatientDemographicOtherInfo.MedicalConditions.Contains(catId))
                {
                    _oPatientDemographicOtherInfo.MedicalConditions.Add(catId);
                }
            }
            
            if (cmbSexualOrientation.Visible == true)
            {
                _oPatientDemographicOtherInfo.SexualOrientationID = Convert.ToInt64(cmbSexualOrientation.SelectedValue);
                _oPatientDemographicOtherInfo.SexualOrientationCode = GetSelectedCode(cmbSexualOrientation);
                _oPatientDemographicOtherInfo.SexualOrientationDesc = cmbSexualOrientation.Text.Trim();
            }
            
            _oPatientDemographicOtherInfo.SexualOrientationOtherSpecification = txtOtherSexualOrientation.Text.Trim();

            
            if (cmbGenderIdentity.Visible == true)
            {
                _oPatientDemographicOtherInfo.GenderIdentityID = Convert.ToInt64(cmbGenderIdentity.SelectedValue);
                _oPatientDemographicOtherInfo.GenderIdentityCode = GetSelectedCode(cmbGenderIdentity);
                _oPatientDemographicOtherInfo.GenderIdentityDesc = cmbGenderIdentity.Text.Trim();
            }

            _oPatientDemographicOtherInfo.GenderIdentityOtherSpecification = txtOtherGenderIdentity.Text.Trim();

            _oPatientDemographicOtherInfo.sPatientPrevFName = txtPatPRVFname.Text.Trim();
            _oPatientDemographicOtherInfo.sPatientPrevMName = txtPatPRVMName.Text.Trim();
            _oPatientDemographicOtherInfo.sPatientPrevLName = txtPatPRVLName.Text.Trim();
            bool patReminder = false;

            if (chkReminder.Checked == true)
            {
                patReminder = true;
            }
            else
            {
                patReminder = false;
            }

            _oPatientDemographicOtherInfo.Reminders = patReminder;
            _oPatientDemographicOtherInfo.isBadDebtPatient = chkBadDebtPatient.Checked;

            _oPatientDemographicOtherInfo.sMultipleBirthIndicator = cmbMultipleBirthIndicator.Text.Trim();
            if (cmbBirthOrder.Text != "")
            {
                _oPatientDemographicOtherInfo.BirthOrder = Convert.ToInt64(cmbBirthOrder.Text.Trim());
            }
            
            _oPatientDemographicOtherInfo.PatientBirthSex = cmbBirthSex.Text;

            if (cmbImRegStatus.Text!="")
            {
                _oPatientDemographicOtherInfo.ImmunizationRegistryStatus = cmbImRegStatus.Text;
            }
            onOtherDetails_SaveClicked(sender, e);
        }

        private void btnCloseOtherDetails_Click(object sender, EventArgs e)
        {

            if (IsModified() == false)
            {
                this.Parent.Parent.Parent.Height = _parentHeight;
                onOtherDetailsClose_Clicked(sender, e);
            }
            else
            {
                DialogResult res = MessageBox.Show("Do you want to save changes to this record? ", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    this.Parent.Parent.Parent.Height = _parentHeight;
                    if (Validate() == true)
                    {
                        //Dhruv 20091229
                        //to validate the mask text box
                        if (mtxtSpousePhone.IsValidated == true)
                        {
                            btnSaveOtherDetails_Click(null, null);
                        }
                    }
                }
                else if (res == DialogResult.No)
                {
                    //Dhruv 20091224
                    //to not to allow the validation 
                    this.Parent.Parent.Parent.Height = _parentHeight;
                    mtxtSpousePhone.AllowValidate = false;
                    onOtherDetailsClose_Clicked(sender, e);
                }
            }

        }

        #endregion " Events "

        #region " Get & Set Data Methods "

        public void GetPatientMedicalConditon()
        {
            DataTable dtMedicalCondition = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParam = default(gloDatabaseLayer.DBParameters);

            try
            {
                oDB.Connect(false);
                oParam = new gloDatabaseLayer.DBParameters();
                DataTable dtMedCat = new DataTable();
                DataColumn dcPatId = new DataColumn("nPatientID");
                DataColumn dcMedCatID = new DataColumn("nMedicalCategoryId");
                dtMedCat.Columns.Add(dcPatId);
                dtMedCat.Columns.Add(dcMedCatID);

                for (int i = 0; i < _oPatientDemographicOtherInfo.MedicalConditions.Count; i++)
                {
                    DataRow dtR = dtMedCat.NewRow();
                    dtR["nPatientID"] = Convert.ToInt64(0);
                    dtR["nMedicalCategoryId"] = Convert.ToInt64(_oPatientDemographicOtherInfo.MedicalConditions[i]);
                    dtMedCat.Rows.Add(dtR);
                }
                if (dtMedCat.Rows.Count >= 1)
                {
                    oParam.Add("@tvpMedicalCategory", dtMedCat, ParameterDirection.Input, SqlDbType.Structured);
                    oDB.Retrive("gsp_GetPatientMedicalCondition_TVP", oParam, out dtMedicalCondition);
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oParam != null)
                {
                    oParam.Dispose();
                    oParam = null;
                }

                //if (dtIM != null)
                //{ dtIM.Dispose();
                //  dtIM = null;
                //}
            }

            if (dtMedicalCondition != null)
            {
                cmbPAMedicalCategory.DataSource = dtMedicalCondition;
                cmbPAMedicalCategory.DisplayMember = "sMedicalCategory";
                cmbPAMedicalCategory.ValueMember = "nMedicalCategoryID";
            }
        }

        public bool SetData()
        {
            bool result = false;
            try
            {

                txtLawyerName.Text = this.PatientDemographicOtherInfo.PatientLawyer;
                txtSpouseName.Text = this.PatientDemographicOtherInfo.SpouseName;
                mtxtSpousePhone.Text = this.PatientDemographicOtherInfo.SpousePhone;
                cmbPatientStatus.Text = this.PatientDemographicOtherInfo.Status;
                dtpRegDate.Value = this.PatientDemographicOtherInfo.RegistrationDate;
                dtpSignatureDate.Value = this.PatientDemographicOtherInfo.SOFDate;
                cmbCMS1500Box13.Text = this.PatientDemographicOtherInfo.CMS1500Box13;
                if (this.PatientDemographicOtherInfo.Reminders == true)
                {
                    chkReminder.Checked = true;
                }
                else
                {
                    chkReminder.Checked = false;
                }
                GetPatientMedicalConditon();

                chkBadDebtPatient.Checked = this.PatientDemographicOtherInfo.isBadDebtPatient;

                dtpRegDate.Checked = true;
                result = true;
                //Sandip  Darade 20091021
                if (this.PatientDemographicOtherInfo.PatientID == 0)
                {
                    cmbPatientStatus.Text = "Active";
                    cmbPatientStatus.Text = this.PatientDemographicOtherInfo.Status;

                }
                SetGenderIdentityAndSexualOrientation();
                cmbGenderIdentity.SelectedValue = this.PatientDemographicOtherInfo.GenderIdentityID;
                if (GetSelectedCode(cmbGenderIdentity) == "OTH")
                {
                    txtOtherGenderIdentity.Text = this.PatientDemographicOtherInfo.GenderIdentityOtherSpecification;
                }

                cmbSexualOrientation.SelectedValue = this.PatientDemographicOtherInfo.SexualOrientationID;
                if (GetSelectedCode(cmbSexualOrientation) == "OTH")
                {
                    txtOtherSexualOrientation.Text = this.PatientDemographicOtherInfo.SexualOrientationOtherSpecification;
                }

                txtPatPRVFname.Text = this.PatientDemographicOtherInfo.sPatientPrevFName;
                txtPatPRVMName.Text = this.PatientDemographicOtherInfo.sPatientPrevMName;
                txtPatPRVLName.Text = this.PatientDemographicOtherInfo.sPatientPrevLName;

                cmbMultipleBirthIndicator.Text = this.PatientDemographicOtherInfo.sMultipleBirthIndicator;
                if (cmbMultipleBirthIndicator.Text == "")
                {
                    cmbMultipleBirthIndicator.SelectedIndex = 0;
                }
                else
                {
                    cmbMultipleBirthIndicator_SelectedIndexChanged(null, null);
                }
                
                if (Convert.ToString(this.PatientDemographicOtherInfo.BirthOrder) == "0")
                {
                    cmbBirthOrder.Text = "";
                }
                else
                {
                    cmbBirthOrder.Text = Convert.ToString(this.PatientDemographicOtherInfo.BirthOrder);
                }

                cmbBirthSex.Text = this.PatientDemographicOtherInfo.PatientBirthSex;

                if (this.PatientDemographicOtherInfo.ImmunizationRegistryStatus == "")
                {
                    cmbImRegStatus.SelectedIndex = 0;
                }
                else
                {
                    cmbImRegStatus.Text = this.PatientDemographicOtherInfo.ImmunizationRegistryStatus;
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                result = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                result = false;
            }
            return result;
        }

        private void SetGenderIdentityAndSexualOrientation()
        {
            DataTable dtSexualOrintation = null, dtGenderIdentity = null;
            dtSexualOrintation = FillOtherDetails(0);
            if (dtSexualOrintation != null && dtSexualOrintation.Rows.Count > 0)
            {
                if (dtSexualOrintation != null)
                {
                    DataRow dr = dtSexualOrintation.NewRow();

                    dr["id"] = 0;
                    dr["Description"] = "";
                    dtSexualOrintation.Rows.InsertAt(dr, 0);
                    dtSexualOrintation.AcceptChanges();
                }
                cmbSexualOrientation.SelectedIndexChanged -= new EventHandler(cmbSexualOrientation_SelectedIndexChanged);
                cmbSexualOrientation.DataSource = dtSexualOrintation;
                cmbSexualOrientation.DisplayMember = "Description";
                cmbSexualOrientation.ValueMember = "ID";
                cmbSexualOrientation.SelectedIndexChanged += new EventHandler(cmbSexualOrientation_SelectedIndexChanged);
            }
            dtGenderIdentity = FillOtherDetails(1);
            if (dtGenderIdentity != null && dtGenderIdentity.Rows.Count > 0)
            {
                if (dtGenderIdentity != null)
                {
                    DataRow dr = dtGenderIdentity.NewRow();

                    dr["id"] = 0;
                    dr["Description"] = "";
                    dtGenderIdentity.Rows.InsertAt(dr, 0);
                    dtGenderIdentity.AcceptChanges();
                }
                cmbGenderIdentity.SelectedIndexChanged -= new EventHandler(cmbGenderIdentity_SelectedIndexChanged);
                cmbGenderIdentity.DataSource = dtGenderIdentity;
                cmbGenderIdentity.DisplayMember = "Description";
                cmbGenderIdentity.ValueMember = "ID";
                cmbGenderIdentity.SelectedIndexChanged += new EventHandler(cmbGenderIdentity_SelectedIndexChanged);
            }
        }

        private DataTable FillOtherDetails(int DemoType)
        {
            DataTable dtOtherDetails = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParam = default(gloDatabaseLayer.DBParameters);

            try
            {
                oDB.Connect(false);
                oParam = new gloDatabaseLayer.DBParameters();
                oParam.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParam.Add("@DemoType", DemoType, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("gsp_GetDemographicsOtherDetails", oParam, out dtOtherDetails);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

                if (oParam != null)
                {
                    oParam.Dispose();
                    oParam = null;
                }

                //if (dtIM != null)
                //{ dtIM.Dispose();
                //  dtIM = null;
                //}
            }
            return dtOtherDetails;
        }


        #endregion " Get & Set Data Methods "

        private bool Validate()
        {
            bool _result = true;
            try
            {

                //**********************

                if (dtpRegDate.Value > Convert.ToDateTime("12/31/2100"))
                {
                    MessageBox.Show("Check the date it should not be greater than 12/31/2100.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpRegDate.Value = DateTime.Now;
                    dtpRegDate.Focus();
                    return false;
                }

                return _result;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false; ;
            }
        }



        private void MaskTextBox_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        private bool IsModified()
        {
            bool _result = true;

            try
            {
                //if (txtLawyerName.Text.Trim() == _oPatientDemographicOtherInfo.PatientLawyer
                //    && txtSpouseName.Text.Trim() == _oPatientDemographicOtherInfo.SpouseName
                //    && mtxtSpousePhone.Text.Trim() == _oPatientDemographicOtherInfo.SpousePhone
                //    && cmbPatientStatus.Text.Trim() == _oPatientDemographicOtherInfo.Status
                //    && dtpRegDate.Value.Date == _oPatientDemographicOtherInfo.RegistrationDate.Date
                //    && dtpSignatureDate.Value.Date == _oPatientDemographicOtherInfo.SOFDate.Date
                //    && cmbCMS1500Box13.Text.Trim() == _oPatientDemographicOtherInfo.CMS1500Box13
                //    && _IsWorkersCompModified == false)



                if (txtLawyerName.Text.Trim() == this.PatientDemographicOtherInfo.PatientLawyer
                    && txtSpouseName.Text == this.PatientDemographicOtherInfo.SpouseName
                    && mtxtSpousePhone.Text == this.PatientDemographicOtherInfo.SpousePhone
                    && cmbPatientStatus.Text == this.PatientDemographicOtherInfo.Status
                    && dtpRegDate.Value == this.PatientDemographicOtherInfo.RegistrationDate
                    && dtpSignatureDate.Value == this.PatientDemographicOtherInfo.SOFDate
                    && cmbCMS1500Box13.Text == this.PatientDemographicOtherInfo.CMS1500Box13
                    && _IsWorkersCompModified == false)
                {
                    _result = false;
                }

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
                _result = true;
            }
            return _result;
        }

        public static void ShowToolTips(C1.Win.C1SuperTooltip.C1SuperTooltip oC1ToolTip, C1.Win.C1FlexGrid.C1FlexGrid oGrid, System.Drawing.Point nLocation)
        {
            try
            {
                System.Drawing.Font myfont = oGrid.Font;
                System.Drawing.SizeF stringsize;

                int colsize = 0;
                string sText = "";
                int nRow = 0;
                int nCol = 0;

                if (oGrid.MouseCol > -1 && oGrid.MouseRow > -1)
                {
                    oC1ToolTip.Font = myfont;
                    oC1ToolTip.MaximumWidth = 400;

                    nRow = oGrid.MouseRow;
                    nCol = oGrid.MouseCol;

                    if (oGrid.Cols[nCol].DataType != typeof(System.Boolean))
                    {
                        if (nRow > 0)
                        {
                            if (oGrid.GetData(nRow, nCol) != null)
                            {
                                sText = oGrid.GetData(nRow, nCol).ToString();
                            }
                            colsize = oGrid.Cols[nCol].WidthDisplay;
                        }
                        System.Drawing.Graphics oGrp = oGrid.CreateGraphics();
                        stringsize = oGrp.MeasureString(sText, myfont);
                        //Code Review Changes: Dispose Graphics object
                        oGrp.Dispose();
                        if (stringsize.Width > colsize)
                        {
                            oC1ToolTip.SetToolTip(oGrid, sText);
                        }
                        else
                        {
                            oC1ToolTip.SetToolTip(oGrid, sText);
                        }
                    }
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        public void DisposeAllControls()
        {
            try
            {
                if (_oPatientDemographicOtherInfo != null) { _oPatientDemographicOtherInfo.Dispose(); }

                if (dtMedCat != null)
                {
                    dtMedCat.Dispose();
                    dtMedCat = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongYellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloPatient.Properties.Resources.Img_LongButton;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void btn_MedicalCategory_Click(object sender, EventArgs e)
        {
            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.MedicalCategory, true, this.Width);

            oListControl.ControlHeader = "Medical Category";

            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_MedicalConditonSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            //oListControl.AddFormHandlerClick += new gloListControl.gloListControl.AddFormHandler(oListControl_AddFormHandlerClick);
            //oListControl.ModifyFormHandlerClick += new gloListControl.gloListControl.ModifyFormHandler(oListControl_ModifyFormHandlerClick);
            oListControl.Dock = DockStyle.Fill;
            this.Controls.Add(oListControl);

            for (int i = 0; i < cmbPAMedicalCategory.Items.Count; i++)
            {
                cmbPAMedicalCategory.SelectedIndex = i;
                if (cmbPAMedicalCategory.Text == "")
                { }
                else
                {
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbPAMedicalCategory.SelectedValue), cmbPAMedicalCategory.Text);
                }
            }
            if (cmbPAMedicalCategory.Items.Count > 0)
                cmbPAMedicalCategory.SelectedIndex = 0;

            oListControl.OpenControl();

            //oListControl is disposed in OpenControl() Method if Zero records found
            if (oListControl.IsDisposed == false)
            {
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
                //onDemographicControl_Leave(sender, e);
            }
        }

        // public delegate void onDemographicControlEnter(object sender, EventArgs e);
        //public event onDemographicControlEnter onDemographicControl_Enter;

        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            removeOListControl();
            // onDemographicControl_Enter(sender, e);
        }
        DataTable dtMedCat = null;
        private void oListControl_MedicalConditonSelectedClick(object sender, EventArgs e)
        {

            string CategoryID;
            CategoryID = "";

            try
            {
                //  cmbPAMedicalCategory.Items.Clear();
                cmbPAMedicalCategory.DataSource = null;
                cmbPAMedicalCategory.Items.Clear();
                if (dtMedCat == null)
                {
                    dtMedCat = new DataTable();
                    dtMedCat.Columns.Clear();
                }
                dtMedCat.Rows.Clear();

                //condition added for bugid 83556
                if (dtMedCat.Columns.Contains("nMedicalCategoryID") == false)
                {
                    DataColumn dcId = new DataColumn("nMedicalCategoryID");
                    dtMedCat.Columns.Add(dcId);
                }
                if (dtMedCat.Columns.Contains("sMedicalCategory") == false)
                {
                    DataColumn dcDescription = new DataColumn("sMedicalCategory");
                    dtMedCat.Columns.Add(dcDescription);
                }
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        DataRow drTemp = dtMedCat.NewRow();
                        drTemp["nMedicalCategoryID"] = oListControl.SelectedItems[i].ID;
                        drTemp["sMedicalCategory"] = oListControl.SelectedItems[i].Description;
                        dtMedCat.Rows.Add(drTemp);

                        if (CategoryID == "")
                        {
                            CategoryID = Convert.ToString(oListControl.SelectedItems[i].ID);
                        }
                        else
                        {
                            CategoryID = CategoryID + "," + Convert.ToString(oListControl.SelectedItems[i].ID);
                        }
                    }

                    cmbPAMedicalCategory.DataSource = dtMedCat;
                    cmbPAMedicalCategory.ValueMember = dtMedCat.Columns["nMedicalCategoryID"].ColumnName;
                    cmbPAMedicalCategory.DisplayMember = dtMedCat.Columns["sMedicalCategory"].ColumnName;
                    //cmbPARace.DrawMode = DrawMode.Normal;
                }


            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                //onDemographicControl_Enter(sender, e);
            }
            cmbPAMedicalCategory.Focus();
        }

        private void removeOListControl()
        {
            if (oListControl != null)
            {
                if (this.Controls.Contains(oListControl))
                {
                    this.Controls.Remove(oListControl);
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_MedicalConditonSelectedClick);
                    }
                    catch
                    {
                    }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch
                    {
                    }

                }
                catch
                {

                }
                oListControl.Dispose();
                oListControl = null;
            }
        }

        private void btn_MedicalCategoryDel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCmbTemp = (DataTable)cmbPAMedicalCategory.DataSource;
                if (dtCmbTemp != null)
                {
                    foreach (DataRow dr in dtCmbTemp.Rows)
                    {
                        if (Convert.ToInt64(dr[0]) == Convert.ToInt64(cmbPAMedicalCategory.SelectedValue))
                        {
                            dtCmbTemp.Rows.Remove(dr);
                            break;
                        }
                    }
                    // cmbPAMedicalCategory.Items.Clear();
                    cmbPAMedicalCategory.DataSource = null;
                    if (cmbPAMedicalCategory.Items != null)
                    {
                        cmbPAMedicalCategory.Items.Clear();
                    }
                    cmbPAMedicalCategory.DataSource = dtCmbTemp;
                    if (dtCmbTemp != null)
                    {
                        if (dtCmbTemp.Columns.Contains("nMedicalCategoryID"))
                        {
                            cmbPAMedicalCategory.ValueMember = dtCmbTemp.Columns["nMedicalCategoryID"].ColumnName;
                        }
                        if (dtCmbTemp.Columns.Contains("sMedicalCategory"))
                        {
                            cmbPAMedicalCategory.DisplayMember = dtCmbTemp.Columns["sMedicalCategory"].ColumnName;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void cmbPAMedicalCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPAMedicalCategory.SelectedItem != null)
                {

                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbPAMedicalCategory.Items[cmbPAMedicalCategory.SelectedIndex])["sMedicalCategory"]), cmbPAMedicalCategory) >= cmbPAMedicalCategory.DropDownWidth - 20)
                    {
                        toolTip1.SetToolTip(cmbPAMedicalCategory, Convert.ToString(((DataRowView)cmbPAMedicalCategory.Items[cmbPAMedicalCategory.SelectedIndex])["sMedicalCategory"]));
                    }
                    else
                    {
                        toolTip1.SetToolTip(cmbPAMedicalCategory, "");
                        this.toolTip1.Hide(cmbPAMedicalCategory);
                    }
                }
                else
                {
                    this.toolTip1.Hide(cmbPAMedicalCategory);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

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
                        if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth - 20)
                            this.toolTip1.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 100, e.Bounds.Bottom + 25);
                        else
                            this.toolTip1.Hide(combo);
                    }
                    else
                    {
                        this.toolTip1.Hide(combo);
                    }
                }
                else
                {
                    toolTip1.Hide(combo);
                }
                e.DrawFocusRectangle();
            }
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {//code review changes 
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g != null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            return width;
        }

        private void cmbGenderIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                if (GetSelectedCode(cmbGenderIdentity) == "OTH")
                {
                    if (OpenForOtherDetails == true)
                    {
                        pnlOtherGenderIdentity.Visible = false;
                    }
                    else
                    {
                        pnlOtherGenderIdentity.Visible = true;
                    }
                }
                else
                {
                    pnlOtherGenderIdentity.Visible = false;
                    txtOtherGenderIdentity.Text = "";
                }
            
        }

        private string GetSelectedCode(ComboBox cmbComboBox)
        {
            string sSelectedCode = string.Empty;
            DataRowView drv = (DataRowView)cmbComboBox.SelectedItem;
            if (drv != null)
            {
                sSelectedCode = drv.Row.ItemArray[1].ToString();
            }

            return sSelectedCode;
        }

        private void cmbSexualOrientation_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                if (GetSelectedCode(cmbSexualOrientation) == "OTH")
                {
                    if (OpenForOtherDetails == true)
                    {
                        pnlOtherSexualOrientation.Visible = false;
                    }
                    else
                    {
                        pnlOtherSexualOrientation.Visible = true;
                    }
                }
                else
                {
                    pnlOtherSexualOrientation.Visible = false;
                    txtOtherSexualOrientation.Text = "";
                }
           
        }

        private void cmbMultipleBirthIndicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBirthOrder.DataSource = null;
            cmbBirthOrder.Items.Clear();
            if (cmbMultipleBirthIndicator.Text=="1")
            {
                cmbBirthOrder.Items.Add("1");
                cmbBirthOrder.Enabled = false;
            }
            else
            {
                for (int i = 0; i < Convert.ToInt16(cmbMultipleBirthIndicator.Text); i++)
                {
                    cmbBirthOrder.Items.Add(i + 1);
                }

                cmbBirthOrder.Enabled = true;
            }
            cmbBirthOrder.SelectedIndex = 0;
        }
    }
}
