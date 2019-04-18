using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using System.Text.RegularExpressions;
using gloSettings;

namespace gloBilling
{
    public partial class frmSetupCPT : Form
    {

        #region " constructor & destructor "
        //frmSetupCPT constructor for the form taking databaseconnectionstring variable 
        //for setting local _databaseconnectionstring varilable
        public frmSetupCPT(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;

            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
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

        public frmSetupCPT(Int64 CPTID,Boolean IsEditMultiple,string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnectionstring = databaseconnectionstring;
            _CPTID = CPTID;
            _IsEditMultiple = IsEditMultiple;

            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
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
        #endregion

        #region " Declaration"

        private Int64 _CPTID=0;
        private string _databaseconnectionstring;
     //   private Int64 _CPTCode;
        public string _MessageBoxCaption = String.Empty;
        private DataView _dv;
        private string _Code = "";
        private string _Description = "";

        //solving issue TFSID-1612(mantis id-134)
        //After Adding New CPT from Service Line,Units,Charges,Allowed assigned to that CPT
        private decimal _units;
        private decimal _charges;
        private decimal _allowed;
     //   private decimal _actualallowed;
        // end
        private Boolean _IsEditMultiple = false;
        private string _sInitialCPTCode = "";

        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //
        public Int64 CPTID
        {
            get { return _CPTID; }
            set { _CPTID = value; }
        }
        public string CPTCode
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public string CPTDescription
        {
            get { return _Description; }
            set { _Description = value; }
        }
        //solving issue TFSID-1612(mantis id-134)
        //After Adding New CPT from Service Line,Units,Charges,Allowed assigned to that CPT
        public Decimal  Units
        {
            get { return _units; }
            set { _units = value; }
        }
        public decimal Charges
        {
            get { return _charges; }
            set { _charges = value; }
        }
        public decimal Allowed
        {
            get { return _allowed; }
            set { _allowed = value; }
        }
        //end
        public Boolean IsEditMultiple
        {
            get { return _IsEditMultiple; }
            set { _IsEditMultiple = value; }
        }
        #endregion 'Declaration'
        
        #region " Form Load "
        
        private void frmSetupCPT_Load(object sender, EventArgs e)
        {
            this.cmbRevenueCode.DrawMode = DrawMode.OwnerDrawFixed;
            this.cmbRevenueCode.DrawItem += new DrawItemEventHandler(cmbRevenueCode_DrawItem);
            tabCPTMaster.TabPages.Remove(tpAnesthesia);
            GeneralSettings oSettings = null;
            oSettings = new GeneralSettings(_databaseconnectionstring);
            object oEPSDTSetting = null;
            oSettings.GetSetting("EnableEPSDTFamilyPlanning", 0, _ClinicID, out oEPSDTSetting);
            if (!string.IsNullOrEmpty(Convert.ToString(oEPSDTSetting)) && (Convert.ToString(oEPSDTSetting).ToUpper() == "TRUE" || Convert.ToString(oEPSDTSetting).ToUpper() == "FALSE"))
            {
                if (!Convert.ToBoolean(oEPSDTSetting))
                {
                    pnlEPSDT.Visible = false;
                }
            }
            oSettings.Dispose();
            if (IsAnesthesiaBillingEnabled())
            { tabCPTMaster.TabPages.Add(tpAnesthesia); }
            else { tabCPTMaster.TabPages.Remove(tpAnesthesia); }
            FillCodeTypeCombo();
            FillSpecialtyCombo();
            FillCategoryCombo();
            FillRevenueCode();
            cbx_IsCPTdrug_CheckedChanged(null, null);
            if (_IsEditMultiple == false)
            {
                FillfrmSetupCPT();
            }
            if (_CPTID != 0)
            {
                txtCPTCode.Select();
                txtCPTCode.Focus();
            }
            else
            {
                if (_Code.Trim() != "")
                {
                    txtCPTCode.Text = _Code;
                    txtDescription.Select();
                    txtDescription.Focus();
                }
            }
            if (_CPTID == 0)
            {
                rbtUnit.Checked = true;
            }
            if (_IsEditMultiple == true)
            {
                this.Height = 720;
                this.Width = 750;
                txtCPTFrom.Visible = true;
                txtCPTTo.Visible = true;
                dgMasters.Visible = true;
                Panel2.Visible = true;
                txtCPTCode.Visible = false;
                txtDescription.Visible = false;
                lblCPTCode.Visible = false;
                lblDescription.Visible = false;
                lblCPTFrom.Visible = true;
                lblCPTTO.Visible = true;
                TabPage objtab = tabCPTMaster.TabPages["tpGlobalPeriods"];
                tabCPTMaster.TabPages.RemoveAt(tabCPTMaster.TabPages.IndexOf(objtab));


                Fill_CPT();
                Search_CPT();

            }
            else
            {
                this.Height = 641;
                this.Width = 668;
                txtCPTFrom.Visible = false;
                txtCPTTo.Visible = false;
                dgMasters.Visible = false;
                Panel2.Visible = false;
                txtCPTCode.Visible = true;
                txtDescription.Visible = true;
                lblCPTCode.Visible = true;
                lblDescription.Visible = true;
                lblCPTFrom.Visible = false;
                lblCPTTO.Visible = false;
            }



            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            // This method actually sets the order all the way down the control hierarchy.
            gloBilling oglobilling = new gloBilling(_databaseconnectionstring, "");
            if (oglobilling.IsenableUB04(_ClinicID))
            {
                pnlrevenue.Visible = true;
            }
            else
            {
                pnlrevenue.Visible = false;
            }

            tom.SetTabOrder(scheme);
            txtCPTCode.Focus();

            if (oglobilling != null)
            {
                oglobilling.Dispose();
                oglobilling = null;
            }
        }//end frmSetupCPT_Load
        
        #endregion
        
        #region " Form Fill Method "

        private void FillGlobalPeriodTAB()
        {
            try
            {
                
                DataTable _dtGlobalPeriodDetails = GetGlobalPeriod(txtCPTCode.Text.Trim(),0);                
                //Insurance Level Settings

                if (_dtGlobalPeriodDetails != null && _dtGlobalPeriodDetails.Rows.Count > 0)
                {
                    this.c1GPInsurance.EnterCell -= new System.EventHandler(this.c1GPInsurance_EnterCell);
                    c1GPInsurance.DataSource = _dtGlobalPeriodDetails;
                    c1GPInsurance.Refresh();
                    c1GPInsurance.Cols["nID"].Visible = false;
                    c1GPInsurance.Cols["Insurance Company"].AllowEditing = false;
                    //c1GPInsurance.Cols["Triggers Global Period"].AllowEditing = false;

                    c1GPInsurance.Cols["Triggers Global Period"].ComboList = "| |Yes|No";
                    c1GPInsurance.Cols["Period Days"].ComboList = "| |0|10|90";                   


                    c1GPInsurance.Cols["Insurance Company"].Width = 150;
                    c1GPInsurance.Cols["Triggers Global Period"].Width = 155;
                    c1GPInsurance.Cols["Period Days"].Width = 85;

                    c1GPInsurance.ExtendLastCol = true;
                    c1GPInsurance.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                    c1GPInsurance.Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                    

                    this.c1GPInsurance.EnterCell += new System.EventHandler(this.c1GPInsurance_EnterCell);


                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private DataTable FillCPTInsDatatable()
        {
            DataTable _dt = new DataTable();
            DataRow _dr = null;
            try
            {
                DataColumn _dc = new DataColumn();
                _dc.ColumnName = "nID";
                _dc.DataType = typeof(System.Int64);
                _dt.Columns.Add(_dc);

                _dc = new DataColumn();
                _dc.ColumnName = "nInsuranceCompID";
                _dc.DataType = typeof(System.Int64);
                _dt.Columns.Add(_dc);

                _dc = new DataColumn();
                _dc.ColumnName = "bCPTTriggrs";
                _dc.DataType = typeof(System.Boolean);
                _dt.Columns.Add(_dc);

                _dc = new DataColumn();
                _dc.ColumnName = "sCPT";
                _dc.DataType = typeof(System.String);
                _dt.Columns.Add(_dc);
                
                _dc = new DataColumn();
                _dc.ColumnName = "sCPTDescription";
                _dc.DataType = typeof(System.String);
                _dt.Columns.Add(_dc);

                _dc = new DataColumn();
                _dc.ColumnName = "nPeriodDays";
                _dc.DataType = typeof(System.String);
                _dt.Columns.Add(_dc);
                

                _dc = new DataColumn();
                _dc.ColumnName = "sBillingReminder";
                _dc.DataType = typeof(System.String);
                _dt.Columns.Add(_dc);

                _dc = new DataColumn();
                _dc.ColumnName = "dtCreatedDateTime";
                _dc.DataType = typeof(System.DateTime);
                _dt.Columns.Add(_dc);

                _dc = new DataColumn();
                _dc.ColumnName = "nUserID";
                _dc.DataType = typeof(System.Int64);
                _dt.Columns.Add(_dc);

                DataTable _dtUniqueID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(c1GPInsurance.Rows.Count);
                c1GPInsurance.FinishEditing();
                for (int iRowCoun = 1; iRowCoun <= c1GPInsurance.Rows.Count-1; iRowCoun++)
                {

                    _dr = _dt.NewRow();
                    _dr["nID"] = Convert.ToInt64(_dtUniqueID.Rows[iRowCoun]["ID"]);
                    _dr["nInsuranceCompID"] = Convert.ToInt64(c1GPInsurance.GetData(iRowCoun, 0));

                    if (Convert.ToString(c1GPInsurance.GetData(iRowCoun, 2)).Trim() == "Yes")
                    {
                        _dr["bCPTTriggrs"] = true;
                    }
                    else if (Convert.ToString(c1GPInsurance.GetData(iRowCoun, 2)).Trim() == "No")
                    {
                        _dr["bCPTTriggrs"] = false;
                    }
                    else
                        _dr["bCPTTriggrs"] = DBNull.Value;

                    _dr["sCPT"] = Convert.ToString(txtCPTCode.Text.Trim());
                    _dr["sCPTDescription"] = Convert.ToString(txtDescription.Text.Trim());
                  
                    Int32 _resultPeriodDays = -1;
                    if (Int32.TryParse(Convert.ToString(c1GPInsurance.GetData(iRowCoun, 3)), out _resultPeriodDays))
                    {
                        _dr["nPeriodDays"] = _resultPeriodDays;
                    }
                    else
                    {
                        _dr["nPeriodDays"] = -1;
                    }

                    _dr["sBillingReminder"] = Convert.ToString(c1GPInsurance.GetData(iRowCoun, 4));
                    _dr["dtCreatedDateTime"] = DateTime.Now;
                    _dr["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                    _dt.Rows.Add(_dr);

                }
                
                _dt.AcceptChanges();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _dt;
        }       
      

         //<summary>
        //FillfrmSetupCPT function is used fill the CPT form
        //with previous CPT information incase of modification.
        //</summary>
        private void FillfrmSetupCPT()
        {
            CPT oCPT = new CPT(_databaseconnectionstring);

            try
            {
                //Call to getCPT funtion for getting the CPT information
                //defined in ClsBL_CPT class.
                oCPT.CPTID = this._CPTID;
                DataTable dtCPT = oCPT.getCPT();

                //nCPTID, sCPTCode, sDescription, sSpecialityCode, sCategoryType, sCategoryDesc, sCodeTypeCode, sCodeTypeDesc, sDefaultModifier1Code, sDefaultModifier1Desc, sDefaultModifier2Code, sDefaultModifier2Desc, sDefaultModifier3Code, sDefaultModifier3Desc, sDefaultModifier4Code, sDefaultModifier4Desc, nDefaultUnits, bIsCPTDrug, sNDCCode, bIsTaxable, nRate, nCharges, nAllowed, nClinicFee, bInactive, nClinicID
                if (dtCPT != null)
                {
                    if (dtCPT.Rows.Count > 0)
                    {
                        txtCPTCode.Text = dtCPT.Rows[0]["sCPTCode"].ToString();
                        txtDescription.Text = dtCPT.Rows[0]["sDescription"].ToString();

                        _sInitialCPTCode = txtCPTCode.Text.Trim();

                        cmbCategory.SelectedIndex = cmbCategory.FindStringExact(dtCPT.Rows[0]["sCategory"].ToString());

                        cmbSpecialty.SelectedIndex = cmbSpecialty.FindStringExact(dtCPT.Rows[0]["sSpecialityCode"].ToString());
                        //cmb_CodeType.SelectedIndex = cmb_CodeType.FindStringExact(dtCPT.Rows[0]["sCodeTypeCode"].ToString());


                        txt_Mod1Code.Text = dtCPT.Rows[0]["Modifier1Code"].ToString();
                        txt_Mod1Code.Tag = dtCPT.Rows[0]["Modifier1Desc"].ToString();
                        txt_Mod2Code.Text = dtCPT.Rows[0]["Modifier2Code"].ToString();
                        txt_Mod2Code.Tag = dtCPT.Rows[0]["Modifier2Desc"].ToString();
                        txt_Mod3Code.Text = dtCPT.Rows[0]["Modifier3Code"].ToString();
                        txt_Mod3Code.Tag = dtCPT.Rows[0]["Modifier3Desc"].ToString();
                        txt_Mod4Code.Text = dtCPT.Rows[0]["Modifier4Code"].ToString();
                        txt_Mod4Code.Tag = dtCPT.Rows[0]["Modifier4Desc"].ToString();
                        txt_DeafultUnits.Text = (Convert.ToString(dtCPT.Rows[0]["nUnits"]) == "" ? "" : String.Format("{0:##0.####}", dtCPT.Rows[0]["nUnits"]));
                        cbx_IsCPTdrug.Checked = Convert.ToBoolean(dtCPT.Rows[0]["bIsCPTDrug"].ToString());
                        txt_Ndccode.Text = dtCPT.Rows[0]["sNDCCode"].ToString();
                        cbx_IsTaxable.Checked = Convert.ToBoolean(dtCPT.Rows[0]["bIsTaxable"].ToString());
                        //txt_Rate1.Text = dtCPT.Rows[0]["nRate"].ToString();
                        txt_Rate2.Text = dtCPT.Rows[0]["nRate"].ToString();
                        txt_Charges.Text = dtCPT.Rows[0]["nCharges"].ToString();
                        //txt_Allowed.Text = dtCPT.Rows[0]["nAllowed"].ToString();
                        txt_ClinicFee.Text = dtCPT.Rows[0]["nClinicFee"].ToString();
                        cbx_Inactive.Checked = Convert.ToBoolean(dtCPT.Rows[0]["bInactive"].ToString());
                        //cbx_IsFeeSchedule.Checked = Convert.ToBoolean(dtCPT.Rows[0]["bIsUseFromFeeSchedule"].ToString());
                        txtStatementdesc.Text = dtCPT.Rows[0]["sStatementDesc"].ToString();
                        cmbRevenueCode.SelectedValue = dtCPT.Rows[0]["RevenueID"].ToString();

                        if (Convert.ToBoolean(dtCPT.Rows[0]["bCPTTriggrs"]))
                        {
                            cmbGPtriggers.Text = "Yes";
                        }
                        else
                        {
                            cmbGPtriggers.Text = "No";
                        }

                        cmbPeriodDays.Text = Convert.ToString(dtCPT.Rows[0]["nPeriodDays"]);                       
                        //cmbPeriodDays.SelectedText  = Convert.ToString(dtCPT.Rows[0]["nPeriodDays"]);

                        txtBillingReminder.Text = Convert.ToString(dtCPT.Rows[0]["sBillingReminder"]);
                        chkEpsdtFamPlan.Checked = Convert.ToBoolean(dtCPT.Rows[0]["bIsPromptforEpsdtFamPlan"].ToString());
                        chkAnesthesia.Checked = Convert.ToBoolean(dtCPT.Rows[0]["bIsAnesthesia"].ToString());
                        txtBaseUnits.Text = (Convert.ToString(dtCPT.Rows[0]["nAnesthesiaBaseUnits"]) == "" ? "" : String.Format("{0:##0.####}", dtCPT.Rows[0]["nAnesthesiaBaseUnits"])); //Convert.ToString(dtCPT.Rows[0]["nAnesthesiaBaseUnits"]);
                        txtCptCLIANo.Text = Convert.ToString(dtCPT.Rows[0]["sCLIANo"]);  //  added on 15Apr2014 for CLIANumber on CPT Master - Sameer
                        chkDefaultSelf.Checked = Convert.ToBoolean(dtCPT.Rows[0]["bDefaultSelf"].ToString());
                        chkNonServiceCode.Checked = Convert.ToBoolean(dtCPT.Rows[0]["bNonserviceCode"].ToString());
                        chkMammogram.Checked = Convert.ToBoolean(dtCPT.Rows[0]["bIsMammogram"].ToString());
                        mskCPTActivationDate.Text = (Convert.ToString(dtCPT.Rows[0]["dtCPTActivateDate"]) == "" ? "" : Convert.ToDateTime(dtCPT.Rows[0]["dtCPTActivateDate"]).ToString("MMddyyyy"));
                        mskCPTInactivationDate.Text = (Convert.ToString(dtCPT.Rows[0]["dtCPTInactivateDate"]) == "" ? "" : Convert.ToDateTime(dtCPT.Rows[0]["dtCPTInactivateDate"]).ToString("MMddyyyy"));

                        txtProductCost.Text = dtCPT.Rows[0]["dProductCost"].ToString();

                        if (Convert.ToInt16(dtCPT.Rows[0]["nCostPer"]) == (Int16)nCostPer.Unit)
                        {
                            rbtUnit.Checked = true;
                        }
                        else if (Convert.ToInt16(dtCPT.Rows[0]["nCostPer"]) == (Int16)nCostPer.Patient)
                        {
                            rbtPatient.Checked = true;
                        }
                        else if (Convert.ToInt16(dtCPT.Rows[0]["nCostPer"]) == (Int16)nCostPer.Visit)
                        {
                            rbtVisit.Checked = true;
                        }

                    }
                    else
                    {
                        cmbGPtriggers.Text = "No";                       
                    }



                }

                FillGlobalPeriodTAB();

                


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oCPT != null) { oCPT.Dispose(); }
            }
        }

        private void FillSpecialtyCombo()
        {
            try
            {
                // Creating object of CPT class
                CPT oCPT = new CPT(_databaseconnectionstring);

                DataTable dtSpecialty = oCPT.GetSpecialitys();
                if (dtSpecialty != null && dtSpecialty.Rows.Count > 0)
                {
                    cmbSpecialty.DataSource = dtSpecialty;
                    cmbSpecialty.ValueMember = dtSpecialty.Columns[0].ColumnName;
                    cmbSpecialty.DisplayMember = dtSpecialty.Columns[1].ColumnName;
                    cmbSpecialty.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillCategoryCombo()
        {

            try
            {
                CPT oCPT = new CPT(_databaseconnectionstring);
                DataTable dtCategory = oCPT.GetCategorys();
                if (dtCategory != null && dtCategory.Rows.Count > 0)
                {
                    DataRow dr = dtCategory.NewRow();
                    dr[0] = "0";
                    dr[1] = "";
                    dtCategory.Rows.InsertAt(dr, 0);
                    dtCategory.AcceptChanges();  

                    cmbCategory.DataSource = dtCategory;
                    cmbCategory.ValueMember = dtCategory.Columns[0].ColumnName;
                    cmbCategory.DisplayMember = dtCategory.Columns[1].ColumnName;
                    cmbCategory.SelectedIndex = 0;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void FillCodeTypeCombo()
        {
            try
            {
                //codetype yet  to be considered from code type masters tables  
                CodeType oCtype = new CodeType(_databaseconnectionstring);
                DataTable dt = new DataTable();
                DataTable dtBindTable = new DataTable();
                dtBindTable.Columns.Add("CodeType");

                dt = oCtype.GetCodetypes();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataRow dr = dtBindTable.NewRow();
                    dr["CodeType"] = Convert.ToString(dt.Rows[i]["Code"]) + " - " + Convert.ToString(dt.Rows[i]["CodeTypedesc"]);
                    dtBindTable.Rows.Add(dr);

                    dr = null;
                }
                //dr["CodeType"] ="CodeType-Test";

                if (dtBindTable != null && dtBindTable.Rows.Count > 0)
                {

                    DataRow dr = dtBindTable.NewRow();
                    dr[0] = "";
                    dtBindTable.Rows.InsertAt(dr, 0);
                    dtBindTable.AcceptChanges();  

                    //cmb_CodeType.DataSource = dtBindTable;
                    //cmb_CodeType.ValueMember = "CodeType";
                    //cmb_CodeType.DisplayMember = "CodeType";

                    //if (dtBindTable != null && dtBindTable.Rows.Count > 0)
                    //    cmb_CodeType.SelectedIndex = 0;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            

        }

        private void FillRevenueCode()
        {

            try
            {

                CLsBL_RevenueCode oRevenue = new CLsBL_RevenueCode(_databaseconnectionstring);
                CPT oCPT = new CPT(_databaseconnectionstring);
                DataTable dtRevenue = oRevenue.GetRevenueCode(0, _CPTID, false);
                DataRow dr = dtRevenue.NewRow();
                dr[0] = "0";
                dr[1] = "";
                dr[2] = "";
                dr[3] = "";
                dtRevenue.Rows.InsertAt(dr, 0);
                if (dtRevenue != null && dtRevenue.Rows.Count > 0)
                {                    
                    dtRevenue.AcceptChanges();
                    cmbRevenueCode.DataSource = dtRevenue;
                    cmbRevenueCode.ValueMember = dtRevenue.Columns[0].ColumnName;
                    cmbRevenueCode.DisplayMember = dtRevenue.Columns[4].ColumnName;
                    cmbRevenueCode.SelectedIndex = 0;
                   
                }
               

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        public void Fill_CPT()
        {
            CPT oCPT = new CPT(_databaseconnectionstring);
            DataTable dt = new DataTable();
            try
            {
                dt = oCPT.GetCPTs();

                DataTable newDataTable = dt.Clone();
                //newDataTable.Columns[1].DataType = typeof(Int32);
                //foreach (DataRow dr in dt.Rows)
                //{
                //    newDataTable.ImportRow(dr);
                //}


                if (dt != null)
                    _dv = dt.DefaultView;                
                else
                    return;

                //nCPTID, sCPTCode, sDescription, sSpecialityCode, sCategoryType, sCategoryDesc, sCodeTypeCode, sCodeTypeDesc, 
                //sDefaultMoodifier1Code, sDefaultMoodifier1Desc, sDefaultMoodifier2Code, sDefaultMoodifier2Desc, 
                //sDefaultMoodifier3Code, sDefaultMoodifier3Desc, sDefaultMoodifier4Code, sDefaultMoodifier4Desc, nDefaultUnits, 
                //bIsCPTDrug, sNDCCode, bIsTaxable, nRate, nCharges, nAllowed, nClinicFee, bInactive, nClinicID


                // nCPTID,sCPTCode,Description,sSpecialityCode,sCategory,sCodeType,Moodifier1,Moodifier1,Moodifier2,Moodifier3,Moodifier4,
                //nDefaultUnits,bIsCPTDrug,sNDCCode,bIsTaxable,nRate,nRate,nAllowed,nClinicFee,bInactive
                //Set dataview as datasource for grid(dgMasters)

                

                dgMasters.DataSource = null;
                dgMasters.DataSource = _dv;
                dgMasters.Columns[0].HeaderText = "CPTID";
                dgMasters.Columns[1].HeaderText = "CPTCode";
                dgMasters.Columns[2].HeaderText = "Description";
                dgMasters.Columns[3].HeaderText = "sStatementDesc";
                dgMasters.Columns[4].HeaderText = "Speciality";
                dgMasters.Columns[5].HeaderText = "Category";
              
                dgMasters.Columns[6].HeaderText = "Modifier1";
                dgMasters.Columns[7].HeaderText = "Modifier2";
                dgMasters.Columns[8].HeaderText = "Modifier3";
                dgMasters.Columns[9].HeaderText = "Modifier4";
                dgMasters.Columns[10].HeaderText = "Units";
                dgMasters.Columns[11].HeaderText = "CPTDrug";
                dgMasters.Columns[12].HeaderText = "NDCCode";
                dgMasters.Columns[13].HeaderText = "IsTaxable";
                dgMasters.Columns[14].HeaderText = "Rate";
                dgMasters.Columns[15].HeaderText = "Charges";
            
                dgMasters.Columns[16].HeaderText = "ClinicFee";
                dgMasters.Columns[17].HeaderText = "Inactive";
                dgMasters.Columns[18].HeaderText = "sRevenueCode";
                dgMasters.Columns[19].HeaderText = "CLIA #";

                dgMasters.Columns[20].HeaderText = "Activation Date";
                dgMasters.Columns[21].HeaderText = "Inactivation Date";

                dgMasters.Columns[20].DefaultCellStyle.Format = "MM/dd/yyyy";
                dgMasters.Columns[21].DefaultCellStyle.Format = "MM/dd/yyyy";
                //Make columns visible true or false
                dgMasters.Columns[0].Visible = false;
                dgMasters.Columns[1].Visible = true;
                dgMasters.Columns[2].Visible = true;
                dgMasters.Columns[3].Visible = false;
                dgMasters.Columns[4].Visible = true;
                dgMasters.Columns[5].Visible = true;
              
                dgMasters.Columns[6].Visible = false;
                dgMasters.Columns[7].Visible = false;
                dgMasters.Columns[8].Visible = false;
                dgMasters.Columns[9].Visible = false;
                dgMasters.Columns[10].Visible = false;
                dgMasters.Columns[11].Visible = false;
                dgMasters.Columns[12].Visible = false;
                dgMasters.Columns[13].Visible = false;
                dgMasters.Columns[14].Visible = false;
                dgMasters.Columns[15].Visible = true;
           
                dgMasters.Columns[16].Visible = true;
                dgMasters.Columns[17].Visible = false;
                dgMasters.Columns[18].Visible = false;
                dgMasters.Columns[19].Visible = true;

                dgMasters.Columns[20].Visible = true;
                dgMasters.Columns[21].Visible = true;
                //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.CPT, ActivityType.View, "View CPT", ActivityOutCome.Success);
                //Added Rahul on 20101012
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.View, "View CPT", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                //
            }//Catch Exceptions
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log("Billing Book - CPT : " + ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oCPT.Dispose();
            }
        }

        public void Search_CPT()
        {
            string strSearch = "";
            _dv = (DataView)dgMasters.DataSource;
            if (_dv != null)
            {
                strSearch =  _dv.Table.Columns["sCPTCode"].ColumnName + " in ('')";

                //20100107 Mahesh Nawal Code for getting the CPT code in range
                if (txtCPTFrom.Text.Trim() != "" && txtCPTTo.Text.Trim() != "")
                {
                    //  strSearch = _dv.Table.Columns["sCPTCode"].ColumnName + " not LIKE '%[^0-9]%' and sCPTCode > '" + txtCPTFrom.Text.Trim() + "' and sCPTCode < '" + txtCPTTo.Text.Trim() + "'";
                    CPT oCPT = new CPT(_databaseconnectionstring);
                    DataTable dt = new DataTable();
                    try
                    {
                        dt = oCPT.GetCPTsInRange(txtCPTFrom.Text.Trim(), txtCPTTo.Text.Trim());
                        //DataTable newDataTable = dt.Clone();
                        if (dt != null)
                        {
                            _dv = dt.DefaultView;
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        ex = null; 
                    }
                }
                else
                {
                    _dv.RowFilter = strSearch;
                }
                dgMasters.DataSource = _dv;
            }
        }

        public String GetRange()
        {
            String sRange = "''";
            if (txtCPTFrom.Text.Trim() != "" && txtCPTTo.Text.Trim() != "")
            {
                for (Int64 i = Convert.ToInt64(txtCPTFrom.Text); i <= Convert.ToInt64(txtCPTTo.Text); i++)
                {
                    if (sRange == "")
                        sRange = "'" + i.ToString() + "'";
                    else
                        sRange += ",'" + i.ToString() + "'";
                }
            }            
            return sRange;
        }

        #endregion

        #region " Tool Strip Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (IsEditMultiple == false)
            {
                if (c1GPInsurance.Rows.Count > 1)
                {
                    c1GPInsurance.Select(0, 0);
                }
            }
 
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        if (_IsEditMultiple == true)
                        {
                            if (validatemultiplecpt()==true )
                            {
                                if (SaveMultipleCPT() == true)
                                {
                                    this.Close();
                                }
                            }
                        }
                        else
                        {
                            if (ValidateData() ==true)
                            {
                                if (SaveCPT() == true)
                                {
                                    this.Close();
                                }
                            }
                        }
                        break;

                    case "Cancel":
                        try
                        {
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            ex = null; 
                        }//catch

                        break;

                    case "Save":
                        try
                        {
                            if (_IsEditMultiple == true)
                            {
                                if (validatemultiplecpt()== true)
                                {
                                    if (SaveMultipleCPT() == true)
                                    {
                                        _CPTID = 0;
                                        //_IsEditMultiple = false;
                                        txtCPTCode.Text = "";
                                        txtCPTTo.Text = "";
                                        txtCPTFrom.Text = "";
                                        txtDescription.Text = "";
                                        cmbSpecialty.SelectedIndex = -1;
                                        cmbCategory.SelectedIndex = -1;
                                        //cmb_CodeType.SelectedIndex = -1;
                                        txt_Mod1Code.Text = "";
                                        txt_Mod2Code.Text = "";
                                        txt_Mod3Code.Text = "";
                                        txt_Mod4Code.Text = "";
                                        txt_Charges.Text = "";
                                        //txt_Allowed.Text = "";
                                        //cbx_IsFeeSchedule.Checked = false;
                                        txt_DeafultUnits.Text = "";
                                        txt_ClinicFee.Text = "";
                                        cbx_IsCPTdrug.Checked = false;
                                        txt_Ndccode.Text = "";
                                        cbx_IsTaxable.Checked = false;
                                        txt_Rate1.Text = "";
                                        txt_Rate2.Text = "";
                                        cbx_Inactive.Checked = false;
                                        txtStatementdesc.Text = "";
                                        txtCptCLIANo.Clear();
                                        chkDefaultSelf.Checked = false;
                                        chkNonServiceCode.Checked = false;
                                        chkMammogram.Checked = false;
                                        txtCPTFrom.Focus();
                                        txtCPTFrom.Select();
                                        Search_CPT();
                                    

                                    }
                                }
                            }
                            else
                            {
                                if (ValidateData() == true)
                                {
                                    if (SaveCPT() == true)
                                    {
                                        _CPTID = 0;
                                        _IsEditMultiple = false;
                                        txtCPTCode.Text = "";
                                        txtCPTTo.Text = "";
                                        txtCPTFrom.Text = "";
                                        txtDescription.Text = "";
                                        cmbSpecialty.SelectedIndex = -1;
                                        cmbCategory.SelectedIndex = -1;
                                        //cmb_CodeType.SelectedIndex = -1;
                                        txt_Mod1Code.Text = "";
                                        txt_Mod2Code.Text = "";
                                        txt_Mod3Code.Text = "";
                                        txt_Mod4Code.Text = "";
                                        txt_Charges.Text = "";
                                        //txt_Allowed.Text = "";
                                        txtStatementdesc.Text = "";
                                        //cbx_IsFeeSchedule.Checked = false;
                                        txt_DeafultUnits.Text = "";
                                        txt_ClinicFee.Text = "";
                                        cbx_IsCPTdrug.Checked = false;
                                        txt_Ndccode.Text = "";
                                        cbx_IsTaxable.Checked = false;
                                        txt_Rate1.Text = "";
                                        txt_Rate2.Text = "";
                                        cbx_Inactive.Checked = false;
                                        txtCPTFrom.Focus();
                                        //dgMasters.Rows.Clear();
                                        txtCptCLIANo.Clear();
                                        chkDefaultSelf.Checked = false;
                                        chkNonServiceCode.Checked = false;
                                        chkMammogram.Checked = false;
                                        txtCPTFrom.Focus();
                                        txtCPTFrom.Select();

                                        cmbPeriodDays.Text = "";
                                        cmbGPtriggers.Text = "No";
                                        txtBillingReminder.Text = "";
                                        txtProductCost.Text = "";
                                        rbtUnit.Checked = true;
                                        if (IsEditMultiple == false)
                                        {
                                            for (int iRowCoun = 1; iRowCoun <= c1GPInsurance.Rows.Count - 1; iRowCoun++)
                                            {
                                                if (Convert.ToString(c1GPInsurance.GetData(iRowCoun, 2)).Trim() != "" || Convert.ToString(c1GPInsurance.GetData(iRowCoun, 3)).Trim() != "" || Convert.ToString(c1GPInsurance.GetData(iRowCoun, 4)).Trim() != "")
                                                {
                                                    c1GPInsurance.SetData(iRowCoun, 2, "");
                                                    c1GPInsurance.SetData(iRowCoun, 3, "");
                                                    c1GPInsurance.SetData(iRowCoun, 4, "");
                                                }

                                            }
                                        }

                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            ex = null; 
                        }//catch

                        break;
                }//endSwitch   
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        #endregion

        # region " Save CPT "
        
        private bool SaveMultipleCPT()
        {
            foreach (DataGridViewRow dr in dgMasters.Rows)
                {
                    _CPTID = Convert.ToInt64(dr.Cells[0].Value);                
                    txtCPTCode.Text = dr.Cells[1].Value.ToString();
                    txtDescription.Text = dr.Cells[2].Value.ToString();
                    SaveCPT();                    
                }            
            return true;
        }

        private DataTable GetGlobalPeriod(string sCPT,Int64 nInsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtGlobalPeriods = null;
            try
            {
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@sCPT", sCPT, ParameterDirection.Input, SqlDbType.VarChar);             
                oDB.Connect(false);
                oDB.Retrive("GET_CPT_Ins_Global_Periods", oParameters, out _dtGlobalPeriods);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _dtGlobalPeriods;
        }

        private bool IsAnesthesiaBillingEnabled()
        {
            GeneralSettings oSettings = null;
            bool IsAnesthesiaBillingEnabled = false;
            try
            {
                oSettings = new GeneralSettings(_databaseconnectionstring);
                object oAnesthesiaBillingSetting = null;
                oSettings.GetSetting("EnableAnesthesiaBilling", 0, _ClinicID, out oAnesthesiaBillingSetting);
                if (!string.IsNullOrEmpty(Convert.ToString(oAnesthesiaBillingSetting)) && (Convert.ToString(oAnesthesiaBillingSetting).ToUpper() == "TRUE" || Convert.ToString(oAnesthesiaBillingSetting).ToUpper() == "FALSE"))
                {
                    if (Convert.ToBoolean(oAnesthesiaBillingSetting))
                    {
                        IsAnesthesiaBillingEnabled = true;
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            { if (oSettings != null) { oSettings.Dispose(); } }
            return IsAnesthesiaBillingEnabled;
        }

        private bool SaveGlobalPeriodInformation(string sCPT, string sCPTDesc,DataTable dtCPTIns)
        {            
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            bool _Return = false;

            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();                                       
                oParameters.Add("@tvpCPTIns", dtCPTIns, ParameterDirection.Input, SqlDbType.Structured);
                oDB.Connect(false);
                int _result = oDB.Execute("BL_INUP_CPT_Ins_Global_Periods", oParameters);

                if (_result > 0)
                {
                    _Return = true;
                }
                else
                {
                    _Return = false;
                }

            }
            catch (Exception ex)
            {
                _Return = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return _Return;         
        }

        private bool SaveCPT()
        {
            bool _result = false;

            //if (ValidateData() == false)
            //{
            //    return false;
            //}

            CPT oCPT = new CPT(_databaseconnectionstring);

            try
            {
                oCPT.CPTID = _CPTID;
                oCPT.CPTCode = txtCPTCode.Text.Trim();
                oCPT.Description = txtDescription.Text.Trim();

                _Code = txtCPTCode.Text.Trim();
                _Description = txtDescription.Text.Trim(); 

             //   string _strCPTCode = "";
            //    string _strCPTDesc = "";
                //int nStartPosition = cmb_CodeType.Text.IndexOf('-');
                //oCPT.CodeTypeCode = cmb_CodeType.Text;
                oCPT.SpecialityCode = Convert.ToString(cmbSpecialty.Text);
                oCPT.nSpecialtyID = Convert.ToInt64(cmbSpecialty.SelectedValue);
                oCPT.CategoryType = Convert.ToString(cmbCategory.Text);
                oCPT.nCategoryID = Convert.ToInt64(cmbCategory.SelectedValue);
                oCPT.Modifier1Code = Convert.ToString(txt_Mod1Code.Text);
                oCPT.Modifier1Desc = Convert.ToString(txt_Mod1Code.Tag);
                oCPT.Modifier2Code = Convert.ToString(txt_Mod2Code.Text);
                oCPT.Modifier2Desc = Convert.ToString(txt_Mod2Code.Tag);
                oCPT.Modifier3Code = Convert.ToString(txt_Mod3Code.Text);
                oCPT.Modifier3Desc = Convert.ToString(txt_Mod3Code.Tag);
                oCPT.Modifier4Code = Convert.ToString(txt_Mod4Code.Text);
                oCPT.Modifier4Desc = Convert.ToString(txt_Mod4Code.Tag);
                if (cmbRevenueCode.Text.Trim() != "")
                {
                    oCPT.RevenueCode = Convert.ToString(cmbRevenueCode.Text.Remove(cmbRevenueCode.Text.IndexOf("-") - 1));
                }
                else
                {
                    oCPT.RevenueCode = "";
                }

                oCPT.Units = 0;
                if (txt_DeafultUnits.Text != "")
                {
                    if (txt_DeafultUnits.Text.Trim().Length > 3 && txt_DeafultUnits.Text.Trim().IndexOf(".") < 0)
                    {
                        MessageBox.Show("Invalid Default Unit.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    decimal dUnits = 0;
                    if (decimal.TryParse(txt_DeafultUnits.Text, out dUnits))
                    {
                        oCPT.Units = dUnits;
                        //solving issue TFSID-1612(mantis id-134)
                        //After Adding New CPT from Service Line,Units,Charges,Allowed assigned to that CPT

                        _units = dUnits;
                        //end
                    }
                    else
                    {
                        MessageBox.Show("Invalid Default Unit.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }

                oCPT.IsCPTDrug = false;
                if (cbx_IsCPTdrug.Checked == true)
                {
                    oCPT.IsCPTDrug = true;
                    //oCPT.NDCCode = Convert.ToString(txt_Ndccode.Text);
                }
                oCPT.NDCCode = Convert.ToString(txt_Ndccode.Text);
                oCPT.IsTaxable = false;
                if (cbx_IsTaxable.Checked == true)
                {
                    oCPT.IsTaxable = true;
                    //if (txt_Rate2.Text == "0.00" || txt_Rate2.Text.Length == 0)
                    //{
                    //    oCPT.Rate = Convert.ToDecimal(txt_Rate1.Text);
                    //}
                    //if (txt_Rate1.Text == "0.00" || txt_Rate1.Text.Length == 0)
                    //{
                    //    oCPT.Rate = Convert.ToDecimal(txt_Rate2.Text);
                    //}
                    if (txt_Rate2.Text != "")
                    {
                        oCPT.Rate = Convert.ToDecimal(txt_Rate2.Text);
 
                    }
                   

                }
                oCPT.Charges = 0;
                //oCPT.Allowed = 0;
                oCPT.ClinicFee = 0;
                if (txt_Charges.Text != "")
                {
                    oCPT.Charges = Convert.ToDecimal(txt_Charges.Text);

                    //solving issue TFSID-1612(mantis id-134)
                    //After Adding New CPT from Service Line,Units,Charges,Allowed assigned to that CPT

                    _charges = Convert.ToDecimal(txt_Charges.Text);
                    // end
                }
                          
                //if (txt_Allowed.Text != "")
                //{
                //    oCPT.Allowed = Convert.ToDecimal(txt_Allowed.Text);

                //    //solving issue TFSID-1612(mantis id-134)
                //    //After Adding New CPT from Service Line,Units,Charges,Allowed assigned to that CPT

                //     _actualallowed = Convert.ToDecimal(txt_Allowed.Text);
                //     _allowed = Convert.ToDecimal(_actualallowed * _units);
                //    // end
                //}
                
                //if (cbx_IsFeeSchedule.Checked == true)
                //{
                //    oCPT.IsUseFromFeeSchedule = true;
                //}

                if (txt_ClinicFee.Text != "")
                {
                    oCPT.ClinicFee = Convert.ToDecimal(txt_ClinicFee.Text);
                }
                oCPT.Inactive = false;
                if (cbx_Inactive.Checked == true)
                {
                    oCPT.Inactive = true;
                }

                if (txt_Ndccode.Text.Trim() != "" && txt_Ndccode.Text.Trim() != null && txt_Ndccode.Text.Trim().Length < 11)
                {

                    MessageBox.Show("NDC Code cannot be less than 11 characters.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_Ndccode.Focus();
                    return false;
                }

                if (oCPT.IsExistsCPT(oCPT.CPTID,oCPT.CPTCode ,oCPT.Description) )
                {
                    MessageBox.Show("Code or description is already in use by another entry.  Select a unique code or description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                oCPT.StatementDesc = txtStatementdesc.Text.Trim();

                //Global Period Information

                if (cmbGPtriggers.Text.Trim() == "Yes")
                {
                    oCPT.CPTTriggrs = true;
                }
                else
                {
                    oCPT.CPTTriggrs = false;
                }

                Int32 _resultPeriodDays = -1;
                if (Int32.TryParse(cmbPeriodDays.Text.Trim(),out _resultPeriodDays))
                {
                    oCPT.PeriodDays = _resultPeriodDays;
                }
                else
                {
                     oCPT.PeriodDays = -1;
                }

                oCPT.BillingReminder = txtBillingReminder.Text.Trim();

                oCPT.bIsPromptforEpsdtFamPlan = chkEpsdtFamPlan.Checked;
                oCPT.bIsAnesthesia = chkAnesthesia.Checked;
                if (txtBaseUnits.Text != "")
                {
                    oCPT.AnesthesiaUnits = Convert.ToDecimal(txtBaseUnits.Text.Trim());
                }

                oCPT.CptCLIANumber = txtCptCLIANo.Text.Trim(); //

                oCPT.bDefaultSelf = chkDefaultSelf.Checked;
                oCPT.bNonServiceCode = chkNonServiceCode.Checked;
                oCPT.bIsMammogram = chkMammogram.Checked;
                mskCPTActivationDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                if (mskCPTActivationDate.Text.Replace("/", "").Trim() != string.Empty)
                {
                    DateTime validatedDate;
                    bool Success;
                    Success = DateTime.TryParseExact(mskCPTActivationDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);

                    if (Success) { oCPT.CPTActivationDate = validatedDate; }
                }

                mskCPTInactivationDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                if (mskCPTInactivationDate.Text.Replace("/", "").Trim() != string.Empty)
                {
                    DateTime validatedDate;
                    bool Success;
                    Success = DateTime.TryParseExact(mskCPTInactivationDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);

                    if (Success) { oCPT.CPTInactivationDate = validatedDate; }
                }

                if (txtProductCost.Text != "")
                {
                    oCPT.ProductCost = Convert.ToDecimal(txtProductCost.Text);
                }
                else 
                {
                    oCPT.ProductCost = 0;
                }

                if (rbtPatient.Checked == true)
                {
                    oCPT.CostPer = (Int16) nCostPer.Patient;
                }
                else if (rbtVisit.Checked == true)
                {
                    oCPT.CostPer = (Int16)nCostPer.Visit;
                }
                else if (rbtUnit.Checked == true)
                {
                    oCPT.CostPer = (Int16)nCostPer.Unit;
                }

                _CPTID =oCPT.Add();

                // Save  global period Settings for Insurance Company Level
                if (_CPTID >= 0)
                {
                    if (_IsEditMultiple == false)
                    {
                        DataTable _dtCPTIns = FillCPTInsDatatable();
                        SaveGlobalPeriodInformation(txtCPTCode.Text.Trim(), txtDescription.Text.Trim(), _dtCPTIns);
                    }
                }
                //****

                if (_CPTID >= 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.CPT, ActivityType.Add, "Add CPT", 0, _CPTID, 0, ActivityOutCome.Success);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.CPT, ActivityType.Add, "Add CPT,Mammogram value set to :" + chkMammogram.Checked + "", 0, _CPTID, 0, ActivityOutCome.Success);
                    _result = true;
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.CPT, ActivityType.Add, "Add CPT", 0, _CPTID, 0, ActivityOutCome.Failure);
                    _result = false;
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.CPT, ActivityType.Add, "Add CPT", 0, _CPTID, 0, ActivityOutCome.Failure);
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (oCPT != null) { oCPT.Dispose(); }
            }
            return _result; 
        }

        private bool ValidateData()
        {
            // Validations for Add CPT form
            if (txtCPTCode.Text.Trim() == "")
            {
                MessageBox.Show("Enter a CPT code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCPTCode.Focus();
                return false;
            }
            if (txtDescription.Text.Trim() == "")
            {
                MessageBox.Show("Enter a Description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescription.Focus();
                return false;
            }
            if (cmbGPtriggers.Text == "Yes" && cmbPeriodDays.Text == "")
            {
                MessageBox.Show("Enter period days.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbPeriodDays.Focus();
                return false;
            }

            for (int iRowCoun = 1; iRowCoun <= c1GPInsurance.Rows.Count - 1; iRowCoun++)
            {
                if (Convert.ToString(c1GPInsurance.GetData(iRowCoun, 2)).Trim() == "Yes" && Convert.ToString(c1GPInsurance.GetData(iRowCoun, 3)).Trim() =="")                    
                {
                    MessageBox.Show("Enter period days for insurance company.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                    return false;
                }

            }

            
            if (_sInitialCPTCode.Trim() != txtCPTCode.Text.Trim())
            {
                CPT oCPT = new CPT(_databaseconnectionstring);
                if (oCPT.IsCPTCodeInUse(_sInitialCPTCode))
                {
                    MessageBox.Show("CPT Code is in use.  It cannot be modified.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            mskCPTActivationDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            if (mskCPTActivationDate.Text.Replace("/", "").Trim() != string.Empty)
            {
                if (!IsValidDate(mskCPTActivationDate.Text))
                {
                    MessageBox.Show("Please enter a valid CPT Activation Date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCPTActivationDate.Focus();
                    return false;
                }
            }

            mskCPTInactivationDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            if (mskCPTInactivationDate.Text.Replace("/", "").Trim() != string.Empty)
            {
                if (!IsValidDate(mskCPTInactivationDate.Text.Replace("//", "")))
                {
                    MessageBox.Show("Please enter a valid CPT Inactivation Date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCPTInactivationDate.Focus();
                    return false;
                }
            }

            if (mskCPTActivationDate.Text.Replace("/", "").Trim() != string.Empty && mskCPTInactivationDate.Text.Replace("/", "").Trim() != string.Empty)
            {
                bool bSuccessStartDate;
                bool bSuccessEndDate;

                DateTime dtStart;
                DateTime dtEnd;

                bSuccessStartDate = DateTime.TryParseExact(mskCPTActivationDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out dtStart);
                bSuccessEndDate = DateTime.TryParseExact(mskCPTInactivationDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out dtEnd);

                if ((bSuccessStartDate && bSuccessEndDate) && (dtStart > dtEnd))
                {
                    MessageBox.Show("CPT Activation Date cannot be greater than CPT Inactivation Date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCPTActivationDate.Focus();
                    return false;
                }

            }

            //if (cmbSpecialty.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Please Select Specialty", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cmbSpecialty.Focus();
            //    return false;
            //}
            //if (cmbCategory.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Please Select Category", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cmbCategory.Focus();
            //    return false;
            //}
            var regex = new System.Text.RegularExpressions.Regex(@"[^a-zA-Z0-9\b]");
            if (regex.IsMatch(txt_Ndccode.Text.Trim()))
            {
                MessageBox.Show("NDC Code cannot contains special character(s) please enter valid NDC Code.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Ndccode.Focus();
                return false;
            }


            return true;
        }
        
        #endregion
        
        #region " Form Control Event "

        private void cbx_IsCPTdrug_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbx_IsCPTdrug.Checked == true)
            //{
            //    txt_Ndccode.Enabled = true;
            //}
            //else
            //{
            //    txt_Ndccode.Enabled = false;
            //}
        }

        private void txt_Charges_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Dhruv 20091230 to Enter only Single dot
            if ((char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) == true) || e.KeyChar == '.')
            {
                if (txt_Charges.Text.IndexOf(".") != -1 && e.KeyChar =='.')
                {
                    e.Handled = true;
                }
            }
            else 
            {
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(13))
            {
                txt_Amount_Leave(sender, null);
                //txt_Allowed.Focus();
            }
        }

        private void txt_ClinicFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            //code to allow nos only 
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                e.Handled = true;
            }
            if (e.KeyChar == Convert.ToChar(46) && txt_ClinicFee.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void txt_DeafultUnits_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46) || e.KeyChar == '\b'))
                {
                    e.Handled = true;
                }
                else if (txt_DeafultUnits.Text.Contains(".") == true)
                {
                    if (txt_DeafultUnits.Text.Substring(txt_DeafultUnits.Text.IndexOf(".") + 1, txt_DeafultUnits.Text.Length - (txt_DeafultUnits.Text.IndexOf(".") + 1)).Length == 4)
                    {
                        if (txt_DeafultUnits.Text.Trim().IndexOf(".") >= 3 && e.KeyChar != '\b' && txt_DeafultUnits.SelectionLength == 0)
                        {
                            e.Handled = true;
                        }
                        else if (txt_DeafultUnits.SelectionStart >= 3  && e.KeyChar != '\b')
                        {
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        if (txt_DeafultUnits.Text.Trim().IndexOf(".") >= 3 && e.KeyChar != '\b')
                        {
                            if (txt_DeafultUnits.SelectionStart <= txt_DeafultUnits.Text.IndexOf(".") && e.KeyChar != '\b' && txt_DeafultUnits.SelectionLength==0)
                            {
                                e.Handled = true;
                            }
                        }
                    }
                }
                else if (txt_DeafultUnits.Text.Contains(".") == false)
                {
                    if (txt_DeafultUnits.Text.Length >= 3 && e.KeyChar != '\b' && e.KeyChar != 46 && txt_DeafultUnits.SelectionLength==0)
                    {
                        e.Handled = true;
                    }
                }
                    if (e.KeyChar == Convert.ToChar(46) && txt_DeafultUnits.Text.Contains("."))
                    {
                        e.Handled = true;
                    }

                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }
        }

        private void txt_Rate1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////code to allow nos only 
            //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            //{
            //    e.Handled = true;
            //}
            //if (e.KeyChar == Convert.ToChar(46) && txt_Rate1.Text.Contains("."))
            //{
            //    e.Handled = true;
            //}
            AllowDecimal(e);
        }

        private void txt_Rate2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////code to allow nos only 
            //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            //{
            //    e.Handled = true;
            //}
            //if (e.KeyChar == Convert.ToChar(46) && txt_Rate2.Text.Contains("."))
            //{
            //    e.Handled = true;
            //}
            AllowDecimal(e);
        }

        private void txt_Allowed_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            //{
            //    e.Handled = true;
            //}
            //if (e.KeyChar == Convert.ToChar(46) && txt_Allowed.Text.Contains("."))
            //{
            //    e.Handled = true;
            //}
            //Dhruv 20091230 to Enter only Single dot
            //if ((char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar) == true) || e.KeyChar == '.')
            //{
            //    if (txt_Allowed.Text.IndexOf(".") != -1 && e.KeyChar == '.')
            //    {
            //        e.Handled = true;
            //    }

            //}
            //else
            //{
            //    e.Handled = true;
            //}
           
            AllowDecimal(e);

            if (e.KeyChar == Convert.ToChar(13))
            {
                txt_Amount_Leave(sender, null);
                txt_DeafultUnits.Focus();
            }
        }

        private void txtCPTFrom_Validated(object sender, EventArgs e)
        {
            Search_CPT();
        }

        private void txtCPTFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            {
                e.Handled = true;
            }
        }
        
        private void txtCPTTo_Validated(object sender, EventArgs e)
        {
            Search_CPT();
        }

        private void txtCPTTo_KeyPress(object sender, KeyPressEventArgs e)
        {
             if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8)))
            {
                e.Handled = true;
            }
        }
               
        #endregion

        #region " Form Control Event "

        private void AllowDecimal ( KeyPressEventArgs e )
    {
        try
        {
            //Allow only numeric and Not decimal point keys 
            if (!((e.KeyChar >= Convert.ToChar(48) & e.KeyChar <= Convert.ToChar(57)) | (e.KeyChar == Convert.ToChar(8)) | (e.KeyChar == Convert.ToChar(46))))
            {
                e.Handled = true;
            }
        }
        catch (Exception)
        {
        }
    }
        #endregion

        private void txt_ClinicFee_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            AllowDecimal(e);
        }

        private void lblCPTStatementdesc_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void lblCPTTO_Click(object sender, EventArgs e)
        {

        }

        private void lblDescription_Click(object sender, EventArgs e)
        {

        }

      
        public bool validatemultiplecpt()
        {

            if (txtCPTFrom.Text.Trim() == "")
            {
                MessageBox.Show("Enter a CPT code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCPTFrom.Focus();
                return false;
            }
            if (txtCPTTo.Text.Trim() == "")
            {
                MessageBox.Show("Enter a CPT code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCPTTo.Focus();
                return false;
            }
            if (cmbGPtriggers.Text == "Yes" && cmbPeriodDays.Text == "")
            {
                MessageBox.Show("Enter period days.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbPeriodDays.Focus();
                return false;
            }
            for (int iRowCoun = 1; iRowCoun <= c1GPInsurance.Rows.Count - 1; iRowCoun++)
            {
                if (Convert.ToString(c1GPInsurance.GetData(iRowCoun, 2)).Trim() == "Yes" && Convert.ToString(c1GPInsurance.GetData(iRowCoun, 3)) == "")
                {
                    MessageBox.Show("Enter period days for insurance company.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

            }
            mskCPTActivationDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            if (mskCPTActivationDate.Text.Replace("/", "").Trim() != string.Empty)
            {
                if (!IsValidDate(mskCPTActivationDate.Text))
                {
                    MessageBox.Show("Please enter a valid CPT Activation Date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCPTActivationDate.Focus();
                    return false;
                }
            }

            mskCPTInactivationDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            if (mskCPTInactivationDate.Text.Replace("/", "").Trim() != string.Empty)
            {
                if (!IsValidDate(mskCPTInactivationDate.Text.Replace("//", "")))
                {
                    MessageBox.Show("Please enter a valid CPT Inactivation Date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCPTInactivationDate.Focus();
                    return false;
                }
            }

            if (mskCPTActivationDate.Text.Replace("/", "").Trim() != string.Empty && mskCPTInactivationDate.Text.Replace("/", "").Trim() != string.Empty)
            {
                bool bSuccessStartDate;
                bool bSuccessEndDate;

                DateTime dtStart;
                DateTime dtEnd;

                bSuccessStartDate = DateTime.TryParseExact(mskCPTActivationDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out dtStart);
                bSuccessEndDate = DateTime.TryParseExact(mskCPTInactivationDate.Text.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out dtEnd);

                if ((bSuccessStartDate && bSuccessEndDate) && (dtStart > dtEnd))
                {
                    MessageBox.Show("CPT Activation Date cannot be greater than CPT Inactivation Date.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCPTActivationDate.Focus();
                    return false;
                }

            }

            return true;
        }

        private void cbx_IsCPTdrug_CheckedChanged_1(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cbx_IsCPTdrug.Checked)
            //    {
            //        txt_Ndccode.Enabled = true;
            //    }
            //    else
            //    {
            //        txt_Ndccode.Enabled = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}
                        
        }

     
        private void txt_Ndccode_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////e.Handled = !(e.KeyChar >= 48 && e.KeyChar<57)||!(e.KeyChar >= 65 && e.KeyChar<91)||!(e.KeyChar >= 98 && e.KeyChar<123) && e.KeyChar != '\b';
            //String abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            //if ((abc.IndexOf(e.KeyChar.ToString().ToLower()) == -1) && (e.KeyChar != '\b'))
            //    e.Handled = true;
        }


        void cmbRevenueCode_DrawItem(object sender, DrawItemEventArgs e)
        {

            string text = this.cmbRevenueCode.GetItemText(cmbRevenueCode.Items[e.Index]);

            e.DrawBackground();

            using (SolidBrush br = new SolidBrush(e.ForeColor))
            {

                e.Graphics.DrawString(text, e.Font, br, e.Bounds);

            }          

            e.DrawFocusRectangle();

        }

        private void txt_ModCode_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txt_ModCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals(Convert.ToChar(Keys.Back)))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9]+$") == false)
                {
                    e.Handled = true;
                }
            }
            
        }

        private void txt_Amount_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            decimal amount = 0;

            try
            { amount = Convert.ToDecimal(txt.Text); }
            catch
            { amount = 0; }

            txt.Text = amount.ToString("#0.00");
        }

        private void txt_DeafultUnits_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_DeafultUnits.Text.Contains(".") == true)
            {
                if (txt_DeafultUnits.Text.Substring(txt_DeafultUnits.Text.IndexOf(".") + 1, txt_DeafultUnits.Text.Length - (txt_DeafultUnits.Text.IndexOf(".") + 1)).Length > 4)
                {
                    //txt_DeafultUnits.Text = txt_DeafultUnits.Text.Trim().Remove(txt_DeafultUnits.Text.Trim().IndexOf(".") +5);
                    txt_DeafultUnits.Text = txt_DeafultUnits.Text.Trim().Remove(txt_DeafultUnits.Text.Trim().IndexOf("."),1);
                }
                else if (txt_DeafultUnits.Text.Trim().IndexOf(".") > 3)
                {
                  //txt_DeafultUnits.Text = txt_DeafultUnits.Text.Trim().Remove(3, txt_DeafultUnits.Text.Trim().IndexOf(".")-3);
                    txt_DeafultUnits.Text = txt_DeafultUnits.Text.Trim().Remove(txt_DeafultUnits.Text.Trim().IndexOf("."),1);
                }
            }
        }

        private void cmbGPtriggers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGPtriggers.Text == "Yes")
            {
                cmbPeriodDays.Enabled = true;
                txtBillingReminder.Enabled = true;
            }
            else if (cmbGPtriggers.Text == "No")
            {
                cmbPeriodDays.Enabled = false;
                txtBillingReminder.Enabled = false;
            }
        }

        private void tabCPTMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            switch (tabCPTMaster.SelectedTab.Name)
            {
                case "tpCPT":
                    if (_IsEditMultiple)
                    {
                        this.Height = 750;
                    }
                    else
                    {
                        this.Height = 641;
                    }
                    
                    break;
                case "tpGlobalPeriods":
                    this.Height = 596;
                    break;
                case "tpAnesthesia":
                    this.Height = 300;
                    break;
                default:
                    break;
            }
        }

        private void c1GPInsurance_EnterCell(object sender, EventArgs e)
        {
            if (c1GPInsurance.Rows.Count > 1)
            {
                if (Convert.ToString(c1GPInsurance.GetData(c1GPInsurance.RowSel, 2)).Trim() == "Yes")
                {
                    c1GPInsurance.Cols["Period Days"].AllowEditing = true;
                    c1GPInsurance.Cols["Billing Reminder"].AllowEditing = true;
                }
                else
                {
                    c1GPInsurance.Cols["Period Days"].AllowEditing = false;
                    c1GPInsurance.Cols["Billing Reminder"].AllowEditing = false;
                }
            }
        }

        private void cmbPeriodDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || e.KeyChar == '')
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void c1GPInsurance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((C1.Win.C1FlexGrid.C1FlexGridBase)(c1GPInsurance)).Col == 2)
                e.Handled = true;
            else if (((C1.Win.C1FlexGrid.C1FlexGridBase)(c1GPInsurance)).Col == 3 && Convert.ToString(c1GPInsurance.GetData(c1GPInsurance.RowSel, 2)).Trim() == "Yes")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }          
        }

        private void c1GPInsurance_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (((C1.Win.C1FlexGrid.C1FlexGridBase)(c1GPInsurance)).Col == 2)
                e.Handled = true;
            else if (((C1.Win.C1FlexGrid.C1FlexGridBase)(c1GPInsurance)).Col == 3 && Convert.ToString(c1GPInsurance.GetData(c1GPInsurance.RowSel, 2)).Trim() == "Yes")
            {
                if ((!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || e.KeyChar == '')
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }

        private void cmbPeriodDays_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenu cntMenu = null;// new System.Windows.Forms.ContextMenu();
                cmbPeriodDays.ContextMenu = cntMenu;
            }
        }

        private void c1GPInsurance_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //c1GPInsurance.Editor = (TextBox)c1GPInsurance.Editor;
        }

        private void c1GPInsurance_SetupEditor(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col == 3)
            {

                ((ComboBox)c1GPInsurance.Editor).MaxLength = 4;

            }
            else if (e.Col == 4)
            {
                ((TextBox)c1GPInsurance.Editor).MaxLength = 255;
            }

        }

        private void c1GPInsurance_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(c1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void txtBaseUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
        && !char.IsDigit(e.KeyChar)
        && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar))
            {

                TextBox textBox = (TextBox)sender;

                if (textBox.Text.IndexOf('.') > -1 &&
                         textBox.Text.Substring(textBox.Text.IndexOf('.')).Length >= 5)
                {
                    e.Handled = true;
                }
            }
        }

        private void mskCPTActivationDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
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
                Success = false;

            }
            return Success;
        }

        private void txtProductCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            AllowDecimal(e);
        }

    }//end class frmSetupCPT

} //end namespace gloBilling