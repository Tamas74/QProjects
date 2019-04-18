using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloSettings;



namespace gloContacts
{
    public partial class frmInsuranceCompany : Form
    {
        #region "Private Variables"
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _ClinicID = 0;
        //private Int64 _ContactID = 1;
        private Int64 _nInsuranceCopanyID = 0;
        //Shubhangi 20100219
        private bool _IsModified = false;
        private bool _IsSaveClicked = false;
        DataTable _dtRemovedItem = new DataTable();
        //End
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private gloListControl.gloListControl oListControl;
        private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Insurance;
        private ComboBox combo;
        ToolTip tooltip_Rpt = new ToolTip();
        gloAddress.gloAddressControl oAddresscontrol = null;

        bool nonNumberEntered = false;
        bool bBillingtype = false;
        #endregion

        #region " C1 Constants "
        private const int COL_ContactID = 1;
        private const int COL_PhysicianName = 2;
        private const int COL_LastName = 3;
        private const int COL_PlanName = 4;
        private const int COL_InsCompnay = 5;
        private const int COL_ReportingCategory = 6;
        private const int COL_InsuranceTypeDescription = 7;
        private const int COL_Gender = 8;
        private const int COL_AddressLine1 = 9;
        private const int COL_AddressLine2 = 10;
        private const int COL_City = 11;
        private const int COL_State = 12;
        private const int COL_Zip = 13;
        private const int COL_ContactName = 14;
        private const int COL_Phone = 15;
        private const int COL_Fax = 16;
        private const int COL_Mobile = 17;
        private const int COL_Email = 18;
        private const int COL_InsTypeCode = 19;
        private const int COL_COUNT = 20;
        #endregion

        //Constructor
        public frmInsuranceCompany(String DatabaseConnectionString)
        {
            InitializeComponent();

            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

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
            cmbCptCrosswalk.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCptCrosswalk.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
        }
        //Modify Constructor
        public frmInsuranceCompany(Int64 InsuranceCopanyID, String DatabaseConnectionString)
        {
            InitializeComponent();

            _nInsuranceCopanyID = InsuranceCopanyID;
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }


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
            cmbCptCrosswalk.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCptCrosswalk.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
        }

        private void frmInsuranceCompany_Load(object sender, EventArgs e)
        {

            //gloC1FlexStyle.Style(c1Insurance, false);

            gloContacts.gloContact oContact = new gloContacts.gloContact(_databaseconnectionstring);

            try
            {
                DataTable dtInsPlans;

                txtDescription.Focus();
                oAddresscontrol = new gloAddress.gloAddressControl(_databaseconnectionstring, true);
                oAddresscontrol.Dock = DockStyle.Fill;
                pnlAddresssControl.Controls.Add(oAddresscontrol);
                Fill_CPTMapping();
                FillPaperBilling();  //Fill Paper Billing Cmb 
                if (_nInsuranceCopanyID > 0)
                {
                    FillInsuranceComapany();
                    dtInsPlans = oContact.GetInsurancePlans(_nInsuranceCopanyID);
                    _IsModified = false;


                    dtInsPlans.Columns.Remove("sPayerId");
                    //Get Expanded Claim Settings
                    dtInsPlans.AcceptChanges();
                    GetExpandedClaimSettings();

                    if (cmbTypeOFBilling.Text != "")
                    {
                        bBillingtype = true;
                    }
                    //

                }
                else
                {
                    txtCode.Tag = 0;
                    dtInsPlans = oContact.GetInsurancePlans(1);
                    dtInsPlans.Columns.Remove("sPayerId");
                    //Get Expanded Claim Settings
                    dtInsPlans.AcceptChanges();
                    dtInsPlans.Clear();

                    bBillingtype = true;
                    txtTFL.Text = "180";
                    txtDFL.Text = "180";
                }


                FillInsurancePlans(dtInsPlans);

                gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
                if (!oglocontact.IsenableUB04(_ClinicID))
                {
                    chkIsInstitutionalBilling.Visible = false;
                }


                cmbBox29.DrawMode = DrawMode.OwnerDrawFixed;
                cmbBox29.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

                cmbBox30.DrawMode = DrawMode.OwnerDrawFixed;
                cmbBox30.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

                oglocontact.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (oContact != null) { oContact.Dispose(); oContact = null; }
            }
        }


        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ts_btnSave_Click(object sender, EventArgs e)
        {

            if (SaveInsuranceCompnay() == true)
                this.Close();
        }

        private bool SaveInsuranceCompnay()
        {
            bool _ReturnResult = true;
            gloDatabaseLayer.DBLayer oDB = null;
            gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
            string _sCode = null;
            string _Insurancecmpny = null;
            try
            {
                if (ValidateData() == true)
                {
                    oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    bool _result;
                    _sCode = txtCode.Text;
                    // _Description = txtDescription.Text;
                    _Insurancecmpny = txtDescription.Text.Trim();
                    Int64 _InsuranceType = Convert.ToInt64(txtDefaultInsBillingType.Tag.ToString());
                    Int64 _ReportCategoryID = Convert.ToInt64(txtDefaultReportingCategory.Tag.ToString());
                    Int64 _FeeScheduleID = Convert.ToInt64(txtDefaultFeeSchedule.Tag.ToString());
                    bool _bIsInstitutionalBilling = false;
                    int _TypeofBilling = 0;
                    int _nTFL = 0;
                    int _nDFL = 0;

                    string _PayerID = txtDefaultPayerID.Text;
                    _result = oglocontact.IsExistsInsurancecmpny(_Insurancecmpny, Convert.ToInt64(txtCode.Tag));
                    _bIsInstitutionalBilling = chkIsInstitutionalBilling.Checked;
                    if (cmbTypeOFBilling.Text == "Paper")
                    {
                        _TypeofBilling = Convert.ToInt16(gloPMContacts.TypeOfBilling.Paper);
                    }
                    else if (cmbTypeOFBilling.Text == "Electronic")
                    {
                        _TypeofBilling = Convert.ToInt16(gloPMContacts.TypeOfBilling.Electronic);
                    }
                    else
                    {
                        _TypeofBilling = Convert.ToInt16(gloPMContacts.TypeOfBilling.None);
                    }
                    //
                    if (_result == true)
                    {
                        if (DialogResult.No == (MessageBox.Show("Contact name already exists. Do you want to register it anyway?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)))
                        {
                            txtDescription.Focus();
                            return false;
                        }

                    }
                   int  userValTFL;
                   if (int.TryParse(txtTFL.Text, out userValTFL))
                   {
                       _nTFL = Convert.ToInt16(txtTFL.Text);
                   }
                   else
                   { 
                       _nTFL = 0;
                   }

                   int userValDFL;
                   if (int.TryParse(txtDFL.Text, out userValDFL))
                   {
                       _nDFL = Convert.ToInt16(txtDFL.Text);
                   }
                   else
                   {
                       _nDFL = 0;
                   }
                    Int64 _nId;

                    _nId = SaveData(_nInsuranceCopanyID, _sCode, _Insurancecmpny, _InsuranceType, _ReportCategoryID, _FeeScheduleID, oAddresscontrol.txtAddress1.Text.Trim(), oAddresscontrol.txtAddress2.Text.Trim(), oAddresscontrol.txtCity.Text.Trim(), oAddresscontrol.cmbState.Text.Trim(), oAddresscontrol.txtZip.Text.Trim(), txtDefaultPayerID.Text.Trim(), _ClinicID, _TypeofBilling, _bIsInstitutionalBilling,_nTFL,_nDFL);
                    //shubhangi 20091105
                    //Associates insurances to the insurance company


                    string strsql = "Delete from Contact_InsurancePlan_Association where nCompanyId = " + _nInsuranceCopanyID + " and nClinicId = " + _ClinicID;
                    oDB.Execute_Query(strsql);

                    Int64 _ContactId;
                    for (int i = 1; i <= c1Insurance.Rows.Count - 1; i++)
                    {
                        //_ContactId = Convert.ToInt64(c1Insurance.GetData(i + 1, COL_ContactID));
                        _ContactId = Convert.ToInt64(c1Insurance.GetData(i, COL_ContactID));

                        strsql = "Delete from Contact_InsurancePlan_Association where nContactId = " + _ContactId + " and nClinicId = " + _ClinicID;
                        oDB.Execute_Query(strsql);

                        strsql = "Insert into Contact_InsurancePlan_Association (nCompanyId,nContactId,nClinicId) Values(" + _nId + "," + _ContactId + "," + _ClinicID + "" + ")";
                        oDB.Execute_Query(strsql);
                    }
                    strsql = null;
                    oDB.Disconnect();
                    _IsSaveClicked = true;
                    _nInsuranceCopanyID = _nId;

                    //// save Expanded claim settings               
                    SaveExpandedClaimSettings();
                    //
                    ///Save Paper Billing Setting
                    SavePaperBillingSetting();
                    ///

                    gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.InsuranceCompanies);  
                }
                else
                {
                    _ReturnResult = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _ReturnResult = false;
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }

                if (oglocontact != null) { oglocontact.Dispose(); oglocontact = null; }
                _sCode = null;
                _Insurancecmpny = null;
            }
            return _ReturnResult;
        }

        #region "CPT Mapping"

        private void Fill_CPTMapping()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtClearing = null;
            string _sqlQuery = "";
            try
            {


                oDB.Connect(false);

                _sqlQuery = "SELECT ISNULL(nCPTMappingID,0) as nCPTMappingID,ISNULL(sCPTMappingName,'') as sCPTMappingName from CPT_Mapping_MST order by sCPTMappingName";

                oDB.Retrive_Query(_sqlQuery, out dtClearing);


                if (dtClearing != null && dtClearing.Rows.Count > 0)
                {
                    DataRow dr = dtClearing.NewRow();
                    dr["nCPTMappingID"] = 0;
                    dr["sCPTMappingName"] = "";
                    dtClearing.Rows.InsertAt(dr, 0);
                    dtClearing.AcceptChanges();

                    cmbCptCrosswalk.DataSource = dtClearing;
                    cmbCptCrosswalk.DisplayMember = "sCPTMappingName";
                    cmbCptCrosswalk.ValueMember = "nCPTMappingID";
                    cmbCptCrosswalk.Refresh();
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
                oDB.Disconnect();
                oDB.Dispose();
                _sqlQuery = null;
            }


        }

        #endregion "CPT Mapping"
        #region "Paper Billing setting"
        private void FillPaperBilling()
        {
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = null;
            try
            {
                oDB.Connect(false);

                _sqlQuery = "SELECT ISNULL(nID,0) AS nID ,ISNULL(sBox29,'') AS sBox29 ,ISNULL(sBox30,'') AS sBox30 from BL_PaperBillingdefaultSetting";
                oDB.Retrive_Query(_sqlQuery, out dt);

                if (dt != null)
                {
                    cmbBox29.DataSource = dt.Copy();
                    cmbBox29.ValueMember = "nID";
                    cmbBox29.DisplayMember = "sBox29";
                    cmbBox29.Refresh();

                    cmbBox30.DataSource = dt.Copy();
                    cmbBox30.ValueMember = "nID";
                    cmbBox30.DisplayMember = "sBox30";
                    cmbBox30.Refresh();
                }


                _sqlQuery = " SELECT ISNULL(nSettingValue,0) AS nSettingValue"
                + " FROM BL_PaperBillingSetting WHERE nSettingLevel=20 And nSettingType=29 And nClinicID = " + _ClinicID + " AND nCompanyID =" + _nInsuranceCopanyID + "order by nSettingType";

                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();

                if (dt != null && dt.Rows.Count > 0)
                    cmbBox29.SelectedValue = Convert.ToInt16(dt.Rows[0]["nSettingValue"].ToString());

                _sqlQuery = " SELECT ISNULL(nSettingValue,0) AS nSettingValue"
               + " FROM BL_PaperBillingSetting WHERE nSettingLevel=20 And nSettingType=30 And nClinicID = " + _ClinicID + " AND nCompanyID =" + _nInsuranceCopanyID + "order by nSettingType";

                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();

                if (dt != null && dt.Rows.Count > 0)
                    cmbBox30.SelectedValue = Convert.ToInt16(dt.Rows[0]["nSettingValue"].ToString());

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
                _sqlQuery = null;
            }

        }
        #endregion


        private Int64 SaveData(Int64 _nInsuranceCopanyID, string Code, string Description, Int64 InsuranceType, Int64 ReportCategoryID, Int64 FeeScheduleID, string AddressLine1, string AddressLine2, string City, string State, string Zip, string PayerID, Int64 ClinicId, int TypeofBilling, bool IsInstitutionalBilling, int nTFL, int nDFL)
        {

            Int64 _result = 0;
            Int64 CPTCrosswalkID = Convert.ToInt64(cmbCptCrosswalk.SelectedValue);
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            object _intresult = 0;

            try
            {
                //commeneted by dipak as not used
                //Int64 _Contactid;
                oDBParameters.Clear();
                oDBParameters.Add("@nId", _nInsuranceCopanyID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@Code", Code, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@Description", Description, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@ClinicId", ClinicId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nInsuranceType", InsuranceType, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nReportCategoryID", ReportCategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nFeeScheduleID", FeeScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sAddressLine1", AddressLine1, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sAddressLine2", AddressLine2, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sCity", City, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sState", State, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sZip", Zip, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPayerID", PayerID, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nCPTMappingID", CPTCrosswalkID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTypeOBilling", TypeofBilling, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@bIsInstitutionalBilling", IsInstitutionalBilling, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@nTFL", nTFL, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@nDFL", nDFL, ParameterDirection.Input, SqlDbType.Int);

                int result = oDB.Execute("CO_INUP_InsuranceCompany_MST", oDBParameters, out _intresult);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult);
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
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                _intresult = null;
            }
            return _result;

        }
        //End Shubhangi


        #region "Private Methods"

        public void FillInsuranceComapany()
        {
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strsql = null;
            try
            {
                oDb.Connect(false);


                //string _strsql = "select nID,sCode,sDescription,nClinicID from dbo.Contacts_InsuranceCompany_MST where nId= '" + _nInsuranceCopanyID + "'";
                _strsql = "SELECT nID,ISNULL( sCode,'') as sCode ,ISNULL(sDescription,'')as sDescription,ISNULL(nInsuranceType,0) as nInsuranceType,ISNULL(nReportCategoryID,0) as nReportCategoryID,ISNULL(nFeeScheduleID,0) as nFeeScheduleID ,ISNULL(sAddressLine1,'')as sAddressLine1,ISNULL(sAddressLine2,'')as sAddressLine2,ISNULL(sCity,'')as sCity,ISNULL(sState,'') as sState,ISNULL(sZip,'')as sZip,ISNULL(sPayerID,'')as sPayerID,Contacts_InsuranceCompany_MST.nClinicID, " +
                                 "ISNULL(nCPTMappingID,0) AS nCPTMappingID,ISNULL(nTypeOBilling,0) AS nTypeOBilling,ISNULL(bIsInstitutionalBilling,0) AS bIsInstitutionalBilling , ISNULL(nTFL,0) as nTFL, ISNULL(nDFL,0) as nDFL FROM Contacts_InsuranceCompany_MST WHERE nId= '" + _nInsuranceCopanyID + "'";

                oDb.Retrive_Query(_strsql, out dt);
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    txtCode.Text = Convert.ToString(dt.Rows[0]["sCode"]);
                    txtDescription.Text = Convert.ToString(dt.Rows[0]["sDescription"]);
                    cmbCptCrosswalk.SelectedValue = Convert.ToInt64(dt.Rows[0]["nCPTMappingID"]);
                    //Shubhangi 20091106
                    //Assign Company Id to the tag property of code text box so that we can check the company Id at the time of duplicate recore 
                    txtCode.Tag = Convert.ToInt64(dt.Rows[0]["nID"]);
                    txtDefaultInsBillingType.Tag = Convert.ToInt64(dt.Rows[0]["nInsuranceType"]);
                    txtDefaultReportingCategory.Tag = Convert.ToInt64(dt.Rows[0]["nReportCategoryID"]);
                    txtDefaultFeeSchedule.Tag = Convert.ToInt64(dt.Rows[0]["nFeeScheduleID"]);
                    oAddresscontrol.isFormLoading = true;
                    oAddresscontrol.txtAddress1.Text = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                    oAddresscontrol.txtAddress2.Text = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                    oAddresscontrol.txtCity.Text = Convert.ToString(dt.Rows[0]["sCity"]);
                    oAddresscontrol.txtZip.Text = Convert.ToString(dt.Rows[0]["sZip"]);
                    oAddresscontrol.cmbState.Text = Convert.ToString(dt.Rows[0]["sState"]);
                    oAddresscontrol.isFormLoading = false;
                    txtDefaultPayerID.Text = Convert.ToString(dt.Rows[0]["sPayerID"]);
                    chkIsInstitutionalBilling.Checked = Convert.ToBoolean(dt.Rows[0]["bIsInstitutionalBilling"]);

                    if (Convert.ToInt16(dt.Rows[0]["nTypeOBilling"]) == Convert.ToInt16(gloPMContacts.TypeOfBilling.Paper))
                    {
                        cmbTypeOFBilling.Text = "Paper";

                    }
                    if (Convert.ToInt16(dt.Rows[0]["nTypeOBilling"]) == Convert.ToInt16(gloPMContacts.TypeOfBilling.Electronic))
                    {
                        cmbTypeOFBilling.Text = "Electronic";
                    }
                    if (Convert.ToInt16(dt.Rows[0]["nTypeOBilling"]) == Convert.ToInt16(gloPMContacts.TypeOfBilling.None))
                    {
                        cmbTypeOFBilling.SelectedIndex = -1;
                    }

                    txtTFL.Text = Convert.ToString(dt.Rows[0]["nTFL"]);
                    txtDFL.Text = Convert.ToString(dt.Rows[0]["nDFL"]);


                }
                _strsql = "SELECT ISNULL(sInsuranceTypeDesc,'') as sInsuranceTypeDesc FROM InsuranceType_MST ";
                if ((txtDefaultInsBillingType.Tag != null) && (txtDefaultInsBillingType.Tag.ToString().Trim() != ""))
                {
                    _strsql = _strsql + " WHERE nInsuranceTypeID=" + txtDefaultInsBillingType.Tag;
                }
                else
                {
                    _strsql = _strsql + " WHERE nInsuranceTypeID=0";
                }
                oDb.Retrive_Query(_strsql, out dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtDefaultInsBillingType.Text = Convert.ToString(dt.Rows[0]["sInsuranceTypeDesc"]);
                    }
                }

                _strsql = "SELECT ISNULL(sDescription,'')as sDescription FROM Contacts_InsuranceReportingCategory_MST ";
                if ((txtDefaultReportingCategory.Tag != null) && (txtDefaultReportingCategory.Tag.ToString().Trim() != ""))
                {
                    _strsql = _strsql + " WHERE nID=" + txtDefaultReportingCategory.Tag;
                }
                else
                {
                    _strsql = _strsql + " WHERE nID=0";
                }
                oDb.Retrive_Query(_strsql, out dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtDefaultReportingCategory.Text = Convert.ToString(dt.Rows[0]["sDescription"]);
                    }
                }
                _strsql = "SELECT ISNULL(sFeeScheduleName,'') AS sFeeScheduleName FROM BL_FeeSchedule_MST ";
                if ((txtDefaultFeeSchedule.Tag != null) && (txtDefaultFeeSchedule.Tag.ToString().Trim() != ""))
                {
                    _strsql = _strsql + " WHERE nFeeScheduleID=" + txtDefaultFeeSchedule.Tag;
                }
                else
                {
                    _strsql = _strsql + " WHERE nFeeScheduleID=0";
                }
                oDb.Retrive_Query(_strsql, out dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtDefaultFeeSchedule.Text = Convert.ToString(dt.Rows[0]["sFeeScheduleName"]);
                    }
                }

                oDb.Disconnect();
            }

            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            finally
            { _strsql = null; }

            //throw new Exception("The method or operation is not implemented.");
        }



        private bool ValidateData()
        {

            if (txtDescription.Text.Trim() == "")
            {
                MessageBox.Show("Please enter insurance company name.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescription.Focus();
                return false;
            }

            // Expanded claim settings Warning message
            if (bBillingtype == true)
            {
                if (cmbTypeOFBilling.Text == "")
                {
                    if (DialogResult.No == (MessageBox.Show("You have not selected any default billing method." + Environment.NewLine + "Continue?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)))
                    {
                        cmbTypeOFBilling.Focus();
                        return false;
                    }

                }
            }


            if (ValidateExpClaimData() == false)
            {
                return false;
            }

            
            return true;
            //throw new Exception("The method or operation is not implemented.");
        }

        private void FillInsurancePlans(DataTable dtInsurance)
        {
            if (dtInsurance != null)
            {

            //    c1Insurance.Clear();
                c1Insurance.DataSource = null;
                DataView _dv = dtInsurance.DefaultView;
                _dv.Sort = "sName";
                 c1Insurance.DataSource = _dv.ToTable();

                c1Insurance.Cols.Count = COL_COUNT;
                c1Insurance.Cols[COL_ContactID].Caption = "ContactID";
                c1Insurance.Cols[COL_PhysicianName].Caption = "Physician Name";
                c1Insurance.Cols[COL_LastName].Caption = "Last Name";
                c1Insurance.Cols[COL_PlanName].Caption = "Insurance Plan";
                c1Insurance.Cols[COL_InsCompnay].Caption = "Insurance Company";
                c1Insurance.Cols[COL_ReportingCategory].Caption = "Reporting Category";
                c1Insurance.Cols[COL_InsuranceTypeDescription].Caption = "Plan Type";
                c1Insurance.Cols[COL_Gender].Caption = "Gender";
                c1Insurance.Cols[COL_AddressLine1].Caption = "Address 1";
                c1Insurance.Cols[COL_AddressLine2].Caption = "Address 2";
                c1Insurance.Cols[COL_City].Caption = "City";
                c1Insurance.Cols[COL_State].Caption = "State";
                c1Insurance.Cols[COL_Zip].Caption = "Zip";
                c1Insurance.Cols[COL_ContactName].Caption = "Contact";
                c1Insurance.Cols[COL_Phone].Caption = "Phone";
                c1Insurance.Cols[COL_Fax].Caption = "Fax";
                c1Insurance.Cols[COL_Mobile].Caption = "Mobile";
                c1Insurance.Cols[COL_Email].Caption = "Email";
                c1Insurance.Cols[COL_InsTypeCode].Caption = "InsuranceTypeCode";

                c1Insurance.Cols[0].Visible = false;
                c1Insurance.Cols[COL_ContactID].Visible = false;
                c1Insurance.Cols[COL_InsCompnay].Visible = false;
                c1Insurance.Cols[COL_PhysicianName].Visible = false;
                c1Insurance.Cols[COL_LastName].Visible = false;
                c1Insurance.Cols[COL_Gender].Visible = false;
                c1Insurance.Cols[COL_Mobile].Visible = false;
                c1Insurance.Cols[COL_InsTypeCode].Visible = false;

                c1Insurance.Cols[COL_PlanName].Width = 230;
                c1Insurance.Cols[COL_InsCompnay].Width = 200;
                c1Insurance.Cols[COL_ReportingCategory].Width = 200;
                c1Insurance.Cols[COL_InsuranceTypeDescription].Width = 200;
                c1Insurance.Cols[COL_AddressLine1].Width = 130;
                c1Insurance.Cols[COL_AddressLine2].Width = 130;
                c1Insurance.Cols[COL_City].Width = 100;
                c1Insurance.Cols[COL_State].Width = 60;
                c1Insurance.Cols[COL_Zip].Width = 60;
                c1Insurance.Cols[COL_ContactName].Width = 90;
                c1Insurance.Cols[COL_Phone].Width = 90;
                c1Insurance.Cols[COL_Fax].Width = 90;
                c1Insurance.Cols[COL_Email].Width = 120;
            }

        }

        #endregion


        private void btnBrowseInsurance_Click(object sender, EventArgs e)
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
                //SLR30:
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

                }
                catch { }
                oListControl.Dispose();
                oListControl = null;
            }

            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Insurance, true, this.Width);
            oListControl.InputTable = _dtRemovedItem;
            oListControl.ClinicID = _ClinicID;
            oListControl.ControlHeader = "Insurance Plan";
            oListControl.ShowInsPlansWithoutCompany = true;

            _CurrentControlType = gloListControl.gloListControlType.Insurance;
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);


            this.Controls.Add(oListControl);


            c1Insurance.Refresh();
            for (int i = 1; i < c1Insurance.Rows.Count; i++)
            {
                oListControl.SelectedItems.Add(Convert.ToInt64(c1Insurance.GetData(i, COL_ContactID)), Convert.ToString(c1Insurance.GetData(i, COL_PlanName)));
            }


            pnlTopToolStrip.Visible = false;
            pnl_Base.Visible = false;
            panel3.Visible = false;
            oListControl.Dock = DockStyle.Fill;
            oListControl.OpenControl();
            oListControl.BringToFront();

            btnBrowseInsurance.Focus();
        }

        private void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            _IsModified = true;
            switch (_CurrentControlType)
            {
                case gloListControl.gloListControlType.Insurance:
                    {
                        try
                        {
                            // MERGE EXISTING PLANS WITH SELECTED PLANS //
                            DataView _dvTemp;
                            DataTable dtCompanyPlans;
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                if (oListControl.SelectedRecords.Rows.Count > 0)
                                {
                                    DataTable dtSelectedPlans = oListControl.SelectedRecords;

                                    if (c1Insurance.DataSource != null && ((DataTable)c1Insurance.DataSource).Rows.Count > 0)
                                    {
                                        // BY FILTERIGN OUT PLANS, KEEP PLANS IN _dvTemp WHICH ARE ALREADY SAVED FOR THIS COMPANY //
                                        dtCompanyPlans = (DataTable)c1Insurance.DataSource;
                                        _dvTemp = dtCompanyPlans.DefaultView;
                                        _dvTemp.RowFilter = "Company <> ''";
                                        dtCompanyPlans = _dvTemp.ToTable();

                                        // NOW MERGE FILTERED PLANS AND SELECTED PLANS IN ONE DT AND FILL IT //
                                        dtCompanyPlans.Merge(dtSelectedPlans);
                                        FillInsurancePlans(dtCompanyPlans);
                                    }
                                    else // IF COMPANY DOESN'T HAVE PLANS ASSOCIATED TO IT, THEN FILL ONLY SELECTED PLANS FROM LISTCONTROL //
                                        FillInsurancePlans(dtSelectedPlans);


                                    // IF SELECTED ROWS ARE FROM REMOVED DATATABLE THEN REMOVE IT FROM _dtRemovedItem //
                                    if (_dtRemovedItem != null && _dtRemovedItem.Rows.Count > 0)
                                    {
                                        for (int iRow = 0; iRow < dtSelectedPlans.Rows.Count; iRow++)
                                        {
                                            for (int jRow = _dtRemovedItem.Rows.Count - 1; jRow >= 0; jRow--)
                                            {
                                                if (_dtRemovedItem.Rows[jRow]["ContactID"].ToString() == dtSelectedPlans.Rows[iRow]["ContactID"].ToString())
                                                {
                                                    _dtRemovedItem.Rows.RemoveAt(jRow);
                                                }
                                            }
                                        }
                                    }

                                }

                                pnlTopToolStrip.Visible = true;
                                pnl_Base.Visible = true;
                                //   pnlHeader.Visible = true;
                                panel3.Visible = true;
                            }
                            else
                            {
                                if (c1Insurance.DataSource != null)
                                {
                                    dtCompanyPlans = (DataTable)c1Insurance.DataSource;
                                    _dvTemp = dtCompanyPlans.DefaultView;
                                    _dvTemp.RowFilter = "Company <> ''";
                                    dtCompanyPlans = _dvTemp.ToTable();
                                    FillInsurancePlans(dtCompanyPlans);
                                }
                            }
                            pnlTopToolStrip.Visible = true;
                            pnl_Base.Visible = true;
                            //pnlHeader.Visible = true;
                            panel3.Visible = true;
                        }

                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        }
                    }
                    break;
                case gloListControl.gloListControlType.InsuranceReportingCategory:
                    {
                        try
                        {
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                txtDefaultReportingCategory.Tag = oListControl.SelectedItems[0].ID;
                                txtDefaultReportingCategory.Text = oListControl.SelectedItems[0].Description;
                                pnlTopToolStrip.Visible = true;
                                pnl_Base.Visible = true;
                                // pnlHeader.Visible = true;
                                panel3.Visible = true;
                            }
                            pnlTopToolStrip.Visible = true;
                            pnl_Base.Visible = true;
                            //pnlHeader.Visible = true;
                            panel3.Visible = true;
                            btnRptCatBrowse.Focus();
                        }

                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        }
                    }
                    break;
                case gloListControl.gloListControlType.BillingType:
                    {
                        try
                        {
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                txtDefaultInsBillingType.Tag = oListControl.SelectedItems[0].ID;
                                txtDefaultInsBillingType.Text = oListControl.SelectedItems[0].Description;
                                pnlTopToolStrip.Visible = true;
                                pnl_Base.Visible = true;
                                //pnlHeader.Visible = true;
                                panel3.Visible = true;
                            }
                            pnlTopToolStrip.Visible = true;
                            pnl_Base.Visible = true;
                            //pnlHeader.Visible = true;
                            panel3.Visible = true;
                            btnBillingTypeBrowse.Focus();
                        }

                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        }
                    }
                    break;
                case gloListControl.gloListControlType.FeeShedule:
                    {
                        try
                        {
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                txtDefaultFeeSchedule.Tag = oListControl.SelectedItems[0].ID;
                                txtDefaultFeeSchedule.Text = oListControl.SelectedItems[0].Description;
                                pnlTopToolStrip.Visible = true;
                                pnl_Base.Visible = true;
                                //pnlHeader.Visible = true;
                                panel3.Visible = true;
                            }
                            pnlTopToolStrip.Visible = true;
                            pnl_Base.Visible = true;
                            //pnlHeader.Visible = true;
                            panel3.Visible = true;
                            btnDefaultFeeSheduleBrowse.Focus();
                        }

                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        }
                    }
                    break;
            }


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
                //SLR30:
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

                }
                catch { }
                
                pnlTopToolStrip.Visible = true;
                pnl_Base.Visible = true;
                //pnlHeader.Visible = true;
                panel3.Visible = true;
            }
        }

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {
            //c1Insurance.Rows.RemoveRange(1, c1Insurance.Rows.Count - 1);
            Cursor = Cursors.WaitCursor;
            c1Insurance.ScrollBars = ScrollBars.None;
            try
            {
                if (c1Insurance.DataSource != null)
                {
                    //gloContacts.gloContact oContact = new gloContact(_databaseconnectionstring);
                    DataTable _dtPlans = (DataTable)c1Insurance.DataSource;

                    for (int iRow = _dtPlans.Rows.Count - 1; iRow >= 0; iRow--)
                    {
                        if (_dtPlans.Rows[iRow]["Company"].ToString() != "")
                        {
                            // REMOVE PLANS FROM LIST IF THEY ARE NOT IN TRANSACTION //
                            // COMMENT BY SUDHIR 20100510 //
                            //if (oContact.IsInsurancePlanUsed(Convert.ToInt64(_dtPlans.Rows[iRow]["ContactID"])) == false)
                            //{

                            // TO ADD ROWs IN REMOVE TABLE LIST //
                            if (_dtRemovedItem != null && _dtRemovedItem.Rows.Count > 0)
                            { }
                            else
                            { _dtRemovedItem = _dtPlans.Clone(); }

                            _dtRemovedItem.Rows.Add(_dtPlans.Rows[iRow].ItemArray);

                            // REMOVE PHYSICAL DATAROW //
                            _dtPlans.Rows.RemoveAt(iRow);
                            //}
                        }
                        else
                        { // IF PLAN IS NOT SAVED AGAINST THIS COMPANY, THEN IT CAN BE REMOVED ON CLEAR //
                            _dtPlans.Rows.RemoveAt(iRow);
                        }
                    }

                    if (_dtPlans.Rows.Count > 0)
                    {
                        MessageBox.Show("Can not remove some insurance plans. Insurance plans are used for billing.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    _IsModified = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            Cursor = Cursors.Default;
            c1Insurance.ScrollBars = ScrollBars.Both;

        }

        private void btnRptCatBrowse_Click(object sender, EventArgs e)
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
                //SLR30:
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

                }
                catch { }
                oListControl.Dispose();
                oListControl = null;
            }

            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.InsuranceReportingCategory, false, this.Width);
            oListControl.ClinicID = _ClinicID;
            oListControl.ControlHeader = "Reporting Category";

            _CurrentControlType = gloListControl.gloListControlType.InsuranceReportingCategory;
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

            this.Controls.Add(oListControl);

            oListControl.SelectedItems.Add(Convert.ToInt64(txtDefaultReportingCategory.Tag), Convert.ToString(txtDefaultReportingCategory.Text));

            pnlTopToolStrip.Visible = false;
            pnl_Base.Visible = false;
            //pnlHeader.Visible = false;
            panel3.Visible = false;
            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();

            btnRptCatBrowse.Focus();
        }

        private void btnInsDelete_Click(object sender, EventArgs e)
        {
            txtDefaultReportingCategory.Text = "";
            txtDefaultReportingCategory.Tag = 0;
        }

        private void btn_AddReportingCategory_Click(object sender, EventArgs e)
        {
            try
            {

                frmInsuranceReportingCategory ofrmInsuranceReportingCategory = new frmInsuranceReportingCategory(_databaseconnectionstring);
                if (ofrmInsuranceReportingCategory.ShowDialog(this) == DialogResult.Yes)
                {
                    txtDefaultReportingCategory.Text = ofrmInsuranceReportingCategory.ReportingCategoryName;
                    txtDefaultReportingCategory.Tag = ofrmInsuranceReportingCategory.ReportingCategoryID;
                }
                ofrmInsuranceReportingCategory.Dispose();

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnModifyReportingCategory_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 InsuranceReportingCategoryID = Convert.ToInt64(txtDefaultReportingCategory.Tag.ToString());
                if (InsuranceReportingCategoryID > 0 && txtDefaultReportingCategory.Text.Trim() != "")
                {
                    frmInsuranceReportingCategory ofrmInsuranceReportingCategory = new frmInsuranceReportingCategory(InsuranceReportingCategoryID, _databaseconnectionstring);
                    // ofrmInsuranceReportingCategory.ShowDialog();
                    if (ofrmInsuranceReportingCategory.ShowDialog(this) == DialogResult.Yes)
                    {
                        txtDefaultReportingCategory.Text = ofrmInsuranceReportingCategory.ReportingCategoryName;
                        txtDefaultReportingCategory.Tag = ofrmInsuranceReportingCategory.ReportingCategoryID;
                    }
                    //txtInsurance.Text = ofrmInsuranceReportingCategory.InsuranceName;
                    //txtInsurance.Tag = ofrmInsuranceReportingCategory.ContactID;

                    ofrmInsuranceReportingCategory.Dispose();
                }

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void pnl_Base_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_AddBillingType_Click(object sender, EventArgs e)
        {

            frmSetup_InsuranceType ofrmSetupInsuranceType = new frmSetup_InsuranceType(_databaseconnectionstring);
            if (ofrmSetupInsuranceType.ShowDialog(this) == DialogResult.Yes)
            {
                txtDefaultInsBillingType.Tag = ofrmSetupInsuranceType.InsurancetypeID;
                txtDefaultInsBillingType.Text = ofrmSetupInsuranceType.InsuranceTypeDescription;

            }
            ofrmSetupInsuranceType.Dispose();

        }

        private void btnModifyBillingType_Click(object sender, EventArgs e)
        {
            Int64 ID = 0;
            ID = Convert.ToInt64(txtDefaultInsBillingType.Tag.ToString());
            if (ID > 0 && txtDefaultInsBillingType.Text.Trim() != "")
            {
                frmSetup_InsuranceType ofrmSetupInsuranceType = new frmSetup_InsuranceType(_databaseconnectionstring, ID);
                ofrmSetupInsuranceType.tls_btnSave.Visible = false;
                if (ofrmSetupInsuranceType.ShowDialog(this) == DialogResult.Yes)
                {
                    txtDefaultInsBillingType.Tag = ofrmSetupInsuranceType.InsurancetypeID;
                    txtDefaultInsBillingType.Text = ofrmSetupInsuranceType.InsuranceTypeDescription;
                }
                ofrmSetupInsuranceType.Dispose();
                ofrmSetupInsuranceType = null;
            }
        }

        private void btnDefaultFeeSheduleBrowse_Click(object sender, EventArgs e)
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
                //SLR30:
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

                }
                catch { }
                oListControl.Dispose();
                oListControl = null;
            }

            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.FeeShedule, false, this.Width);
            oListControl.ClinicID = _ClinicID;
            oListControl.ControlHeader = "Fee Schedule";

            _CurrentControlType = gloListControl.gloListControlType.FeeShedule;
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

            this.Controls.Add(oListControl);
            oListControl.SelectedItems.Add(Convert.ToInt64(txtDefaultFeeSchedule.Tag), Convert.ToString(txtDefaultFeeSchedule.Text));

            pnlTopToolStrip.Visible = false;
            pnl_Base.Visible = false;
            //pnlHeader.Visible = false;
            panel3.Visible = false;
            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
            btnBrowseInsurance.Focus();


        }

        private void btnBillingTypeBrowse_Click(object sender, EventArgs e)
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
                //SLR30:
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

                }
                catch { }
                oListControl.Dispose();
                oListControl = null;
            }

            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.BillingType, false, this.Width);
            oListControl.ClinicID = _ClinicID;
            oListControl.ControlHeader = "Plan Type";

            _CurrentControlType = gloListControl.gloListControlType.BillingType;
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

            this.Controls.Add(oListControl);

            oListControl.SelectedItems.Add(Convert.ToInt64(txtDefaultInsBillingType.Tag), Convert.ToString(txtDefaultInsBillingType.Text));

            pnlTopToolStrip.Visible = false;
            pnl_Base.Visible = false;
            // pnlHeader.Visible = false;
            panel3.Visible = false;
            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();
            btnBillingTypeBrowse.Focus();
        }

        private void btnBillingTypeDelete_Click(object sender, EventArgs e)
        {
            txtDefaultInsBillingType.Text = "";
            txtDefaultInsBillingType.Tag = 0;
        }

        private void btnDefaultFeeSheduleDelete_Click(object sender, EventArgs e)
        {
            txtDefaultFeeSchedule.Text = "";
            txtDefaultFeeSchedule.Tag = 0;
        }

        private void c1Insurance_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                try
                {
                    if (c1Insurance.ContextMenu != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(c1Insurance.ContextMenu);
                        if (c1Insurance.ContextMenu.MenuItems != null)
                        {
                            c1Insurance.ContextMenu.MenuItems.Clear();
                        }
                        c1Insurance.ContextMenu.Dispose();
                        c1Insurance.ContextMenu = null;
                    }
                }
                catch
                {
                }
                c1Insurance.ContextMenu = null;
                if (e.Button == MouseButtons.Right)
                {
                    C1.Win.C1FlexGrid.HitTestInfo oHit = c1Insurance.HitTest(e.X, e.Y);
                    if (oHit.Row > 0)
                    {
                        c1Insurance.Row = oHit.Row;

                        ContextMenu oContext = new ContextMenu();
                        MenuItem oItem = new MenuItem("Remove Insurance Plan");
                        oContext.MenuItems.Add(oItem);
                        oItem.Click += new EventHandler(oItem_Click);
                        try
                        {
                            if (c1Insurance.ContextMenu != null)
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(c1Insurance.ContextMenu);
                                if (c1Insurance.ContextMenu.MenuItems != null)
                                {
                                    c1Insurance.ContextMenu.MenuItems.Clear();
                                }
                                c1Insurance.ContextMenu.Dispose();
                                c1Insurance.ContextMenu = null;
                            }
                        }
                        catch
                        {
                        }
                        c1Insurance.ContextMenu = oContext;
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        void oItem_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _Row = c1Insurance.Row;
                if (_Row > 0)
                {
                    Int64 _ContactID = Convert.ToInt64(c1Insurance.GetData(_Row, COL_ContactID));
                    if (_ContactID > 0)
                    {
                        gloContacts.gloContact oContact = new gloContacts.gloContact(_databaseconnectionstring);

                        // COMMENT BY SUDHIR 20100510 // 
                        //if (oContact.IsInsurancePlanUsed(_ContactID) == false)
                        //{
                        DataTable dtPlans = (DataTable)c1Insurance.DataSource;
                        DataView _dvTemp = dtPlans.DefaultView;

                        // TO MAINTAINS REMOVED RECORDS DATATABLE //    
                        if (c1Insurance.GetData(_Row, COL_InsCompnay).ToString() != "")
                        {
                            _dvTemp.RowFilter = "ContactID = " + _ContactID;
                            if (_dtRemovedItem != null && _dtRemovedItem.Rows.Count > 0)
                            { }
                            else
                            { _dtRemovedItem = _dvTemp.ToTable().Clone(); }

                            _dtRemovedItem.Rows.Add(_dvTemp.ToTable().Rows[0].ItemArray);

                        }


                        _dvTemp.RowFilter = "ContactID <> " + _ContactID;
                        FillInsurancePlans(_dvTemp.ToTable());
                        _IsModified = true;
                        //}
                        //else
                        //    MessageBox.Show("Can not remove this insurance plan. Insurance plan is used for billing.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        oContact.Dispose();
                        oContact = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void AllTextChanged_Event(object sender, System.EventArgs e)
        {
            _IsModified = true;
        }

        private void frmInsuranceCompany_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((oAddresscontrol.AddressModified == true || _IsModified == true) && _IsSaveClicked == false)
            {
                DialogResult _Result;
                _Result = MessageBox.Show("Do you want to save the changes?", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (_Result == DialogResult.Yes)
                {
                    if (SaveInsuranceCompnay() == false)
                    {
                        e.Cancel = true;
                        _IsSaveClicked = false;
                    }
                }
                else if (_Result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

            }

        }

        #region "Tool Tip Methods"

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

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
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

        #endregion "Tool Tip Methods"

        private void cmbCptCrosswalk_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                combo = (ComboBox)sender;
                if (cmbCptCrosswalk.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbCptCrosswalk.Items[cmbCptCrosswalk.SelectedIndex])["sCPTMappingName"]), cmbCptCrosswalk) >= cmbCptCrosswalk.DropDownWidth)
                    {
                        tooltip_Rpt.Show(Convert.ToString(((DataRowView)cmbCptCrosswalk.Items[cmbCptCrosswalk.SelectedIndex])["sCPTMappingName"]), cmbCptCrosswalk, cmbCptCrosswalk.Right - 375, cmbCptCrosswalk.Bottom - 300);
                    }
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }



        private void SaveExpandedClaimSettings()
        {


            try
            {
                string _strSQL = "";

                //Read UB04 Settings
                gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
                if (!oglocontact.IsenableUB04(_ClinicID) )
                {
                    chkIsInstitutionalBilling.Checked = false;
                }
                oglocontact.Dispose();
                //

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

                oDB.Connect(false);

                _strSQL = "delete from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode() + " and nCompanyID = " + _nInsuranceCopanyID;
                oDB.Execute_Query(_strSQL);

                oDB.Disconnect();
                oDB.Dispose();

                gloSettings.GeneralSettings ogloSettings = new GeneralSettings(_databaseconnectionstring);
                Int32 dCharges = 0;
                Int32 dDiagnoses = 0;
                //Int16 dCharges = (Int16)numup_dn_ChargesPerClaim.Value;
                //Int16 dDiagnoses = (Int16)numup_dn_DiagnosisPerClaim.Value;
                if (TxtChargesperClaim.Text == "")
                {
                    dCharges = 0;
                }
                else
                {
                    dCharges = Convert.ToInt32(TxtChargesperClaim.Text);
                }
                if (TxtDiagnosisperClaim.Text == "")
                {
                    dDiagnoses = 0;
                }
                else
                {
                    dDiagnoses = Convert.ToInt32(TxtDiagnosisperClaim.Text);
                }
                if ((cmbTypeOFBilling.Text == "") && (chkIsInstitutionalBilling.Checked == false))
                {
                    ogloSettings.AddExpandedClaimSettings(0, _nInsuranceCopanyID, 0, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.Paper.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                    ogloSettings.AddExpandedClaimSettings(0, _nInsuranceCopanyID, 0, Convert.ToInt16(gloSettings.AlternateIDSettingLevel.ClinicProvider.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.Electronic.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                }
                else if ((cmbTypeOFBilling.Text == "") && (chkIsInstitutionalBilling.Checked == true))
                {
                    ogloSettings.AddExpandedClaimSettings(0, _nInsuranceCopanyID, 0, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.UB04Paper.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                    ogloSettings.AddExpandedClaimSettings(0, _nInsuranceCopanyID, 0, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.UB04Electronic.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                }
                else if ((cmbTypeOFBilling.Text == "Electronic") && (chkIsInstitutionalBilling.Checked == true))
                {
                    ogloSettings.AddExpandedClaimSettings(0, _nInsuranceCopanyID, 0, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.UB04Electronic.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                }
                else if ((cmbTypeOFBilling.Text == "Paper") && (chkIsInstitutionalBilling.Checked == true))
                {
                    ogloSettings.AddExpandedClaimSettings(0, _nInsuranceCopanyID, 0, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.UB04Paper.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                }
                else if ((cmbTypeOFBilling.Text == "Electronic") && (chkIsInstitutionalBilling.Checked == false))
                {
                    ogloSettings.AddExpandedClaimSettings(0, _nInsuranceCopanyID, 0, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.Electronic.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                }
                else if ((cmbTypeOFBilling.Text == "Paper") && (chkIsInstitutionalBilling.Checked == false))
                {
                    ogloSettings.AddExpandedClaimSettings(0, _nInsuranceCopanyID, 0, Convert.ToInt16(gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode()), Convert.ToInt16(gloSettings.TypeOfBilling.Paper.GetHashCode()), dCharges, dDiagnoses, _ClinicID, SettingFlag.User.GetHashCode());
                }

                ogloSettings.Dispose();
                ogloSettings = null;
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }

        }

        private void SavePaperBillingSetting()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            string _sqlQuery = null;

            try
            {
                oDB.Connect(false);
                _sqlQuery = " DELETE FROM BL_PaperBillingSetting WHERE nSettingLevel=20 And nClinicID = " + _ClinicID + " AND nCompanyID= " + _nInsuranceCopanyID;
                oDB.Execute_Query(_sqlQuery);

                try
                {

                    if (cmbBox29.SelectedIndex == -1 || cmbBox29.SelectedIndex == 0)
                    {
                    }
                    else
                    {

                        oDBParameters.Add("@nCompanyID", _nInsuranceCopanyID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nContactID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nSettingLevel", PaperBillingSettingLevel.InsuranceCompany.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nSettingType", PaperBillingBoxtype.Box29.GetHashCode() , System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nSettingValue", Convert.ToInt16(cmbBox29.SelectedValue), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nClinicID", Convert.ToInt64(gloSettings.AppSettings.ClinicID), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nUserID", Convert.ToInt64(gloSettings.AppSettings.UserID), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                        oDB.Execute("BL_INUP_PaperBillingSettings", oDBParameters);
                    }
                    if (cmbBox30.SelectedIndex == -1 || cmbBox30.SelectedIndex == 0)
                    {
                        oDB.Disconnect();
                        return;
                    }
                    else
                    {
                        oDBParameters.Clear();
                        oDBParameters.Add("@nCompanyID", _nInsuranceCopanyID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nContactID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nSettingLevel", PaperBillingSettingLevel.InsuranceCompany.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nSettingType", PaperBillingBoxtype.Box30.GetHashCode() , System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nSettingValue", Convert.ToInt16(cmbBox30.SelectedValue), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                        oDBParameters.Add("@nClinicID",Convert.ToInt64(gloSettings.AppSettings.ClinicID), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nUserID", Convert.ToInt64(gloSettings.AppSettings.UserID), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                        oDB.Execute("BL_INUP_PaperBillingSettings", oDBParameters);

                    }
                    oDB.Disconnect();
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
                    oDB.Dispose();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Dispose();
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                _sqlQuery = null;
            }
        }

        private void GetExpandedClaimSettings()
        {
            string _strSQL = string.Empty;
            DataTable dtClaim = null;
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                //Read UB04 Settings
                gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
                if (!oglocontact.IsenableUB04(_ClinicID))
                {
                    chkIsInstitutionalBilling.Checked = false;
                }
                oglocontact.Dispose();
                //

                _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode() + " and nCompanyID = " + _nInsuranceCopanyID;//+ " and nSettingType = " + gloSettings.TypeOfBilling.Paper.GetHashCode();

                //if ((cmbTypeOFBilling.Text == "") && (chkIsInstitutionalBilling.Checked == false))
                //{
                //    _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode() + " and nCompanyID = " + _nInsuranceCopanyID + " and nSettingType = " + gloSettings.TypeOfBilling.Paper.GetHashCode();
                //}
                //else if ((cmbTypeOFBilling.Text == "") && (chkIsInstitutionalBilling.Checked == true))
                //{
                //    _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode() + " and nCompanyID = " + _nInsuranceCopanyID + " and nSettingType = " + gloSettings.TypeOfBilling.UB04Paper.GetHashCode();
                //}
                //else if ((cmbTypeOFBilling.Text == "Electronic") && (chkIsInstitutionalBilling.Checked == true))
                //{
                //    _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode() + " and nCompanyID = " + _nInsuranceCopanyID + " and nSettingType = " + gloSettings.TypeOfBilling.UB04Electronic.GetHashCode();
                //}
                //else if ((cmbTypeOFBilling.Text == "Paper") && (chkIsInstitutionalBilling.Checked == true))
                //{
                //    _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode() + " and nCompanyID = " + _nInsuranceCopanyID + " and nSettingType = " + gloSettings.TypeOfBilling.UB04Paper.GetHashCode();
                //}
                //else if ((cmbTypeOFBilling.Text == "Electronic") && (chkIsInstitutionalBilling.Checked == false))
                //{
                //    _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode() + " and nCompanyID = " + _nInsuranceCopanyID + " and nSettingType = " + gloSettings.TypeOfBilling.Electronic.GetHashCode();
                //}
                //else if ((cmbTypeOFBilling.Text == "Paper") && (chkIsInstitutionalBilling.Checked == false))
                //{
                //    _strSQL = "select isnull(nServiceLines,0) as nServiceLines,isnull(nDiagnosis,0) as nDiagnosis  from BL_ExpandedClaimSettings where nSettingLevel= " + gloSettings.ExpandedClaimSettingLevel.InsuranceCompany.GetHashCode() + " and nCompanyID = " + _nInsuranceCopanyID + " and nSettingType = " + gloSettings.TypeOfBilling.Paper.GetHashCode();
                //}

                oDB.Retrive_Query(_strSQL, out dtClaim);

                if (dtClaim != null && dtClaim.Rows.Count > 0)
                {

                    if (dtClaim.Rows[0]["nServiceLines"].ToString() == "0") { TxtChargesperClaim.Text = ""; } else { TxtChargesperClaim.Text = dtClaim.Rows[0]["nServiceLines"].ToString(); }
                    if (dtClaim.Rows[0]["nDiagnosis"].ToString() == "0") { TxtDiagnosisperClaim.Text = ""; } else { TxtDiagnosisperClaim.Text = dtClaim.Rows[0]["nDiagnosis"].ToString(); }

                }
                else
                {
                    TxtChargesperClaim.Text = "";
                    TxtDiagnosisperClaim.Text = "";
                }

                oDB.Disconnect();
                oDB.Dispose();
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
            finally
            {
                _strSQL = null;
                if (dtClaim != null) { dtClaim.Dispose(); dtClaim = null; }
            }
        }

        private bool ValidateExpClaimData()
        {
            Int32 dCharges = 0;
            Int32 dDiagnoses = 0;
            //Read UB04 Settings
            gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
            if (!oglocontact.IsenableUB04(_ClinicID) )
            {
                chkIsInstitutionalBilling.Checked = false;
            }
            oglocontact.Dispose(); 
            //

            if (TxtChargesperClaim.Text == "")
            {
                dCharges = 0;
            }
            else
            {
                dCharges = Convert.ToInt32(TxtChargesperClaim.Text);
            }
            if (TxtDiagnosisperClaim.Text == "")
            {
                dDiagnoses = 0;
            }
            else
            {
                dDiagnoses = Convert.ToInt32(TxtDiagnosisperClaim.Text);
            }

           
             if ((cmbTypeOFBilling.Text == "Electronic") && (chkIsInstitutionalBilling.Checked == true))
            {
                //Institutional Edi
                if (dCharges > 999)
                {
                    if (DialogResult.Cancel == (MessageBox.Show("System limits Institutional Electronic Claims (837I 4010) to 999 service lines. ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                    {
                        TxtChargesperClaim.Focus(); 
                        return false;
                    }
                }

                if (dDiagnoses > 18)
                {
                    if (DialogResult.Cancel == (MessageBox.Show("System limits Institutional Electronic Claims (837I 4010) to 18 diagnoses. ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                    {
                        TxtDiagnosisperClaim.Focus();  
                        return false;
                    }
                }
            }
            else if (((cmbTypeOFBilling.Text == "Paper") || (cmbTypeOFBilling.Text == "")) && (chkIsInstitutionalBilling.Checked == true)) //If default billing method is blank then take paper validations
            {
                if (dCharges > 22)
                {
                    if (DialogResult.Cancel == (MessageBox.Show("System limits CMS1450 to 22 service lines. ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                    {
                        TxtChargesperClaim.Focus(); 
                        return false;
                    }
                }

                if (dDiagnoses > 18)
                {
                    if (DialogResult.Cancel == (MessageBox.Show("System limits CMS1450 to 18 diagnoses. ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                    {
                        TxtDiagnosisperClaim.Focus(); 
                        return false;
                    }
                }
            }
            else if ((cmbTypeOFBilling.Text == "Electronic") && (chkIsInstitutionalBilling.Checked == false))
            {
                if (dDiagnoses > 8)
                {
                    if (DialogResult.Cancel == (MessageBox.Show("Electronic Claims (837P 4010) may only display up to 8 diagnoses.  ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                    {
                        TxtDiagnosisperClaim.Focus(); 
                        return false;
                    }
                }


            }
             else if (((cmbTypeOFBilling.Text == "Paper") || (cmbTypeOFBilling.Text == "")) && (chkIsInstitutionalBilling.Checked == false)) //If default billing method is blank then take paper validations
            {

                if (dCharges > 6)
                {
                    if (DialogResult.Cancel == (MessageBox.Show("CMS1500 may only display up to 6 service lines. ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                    {
                        TxtChargesperClaim.Focus(); 
                        return false;
                    }
                }


                if (dDiagnoses > 4)
                {
                    if (DialogResult.Cancel == (MessageBox.Show("CMS1500 may only display up to 4 diagnoses. ", _messageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)))
                    {
                        TxtDiagnosisperClaim.Focus(); 
                        return false;
                    }
                }

            }

            return true;
        }



        private void TxtChargesperClaim_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;


            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {

                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {

                    if (e.KeyCode != Keys.Back | e.KeyCode == Keys.Decimal)
                    {
                        nonNumberEntered = true;
                    }
                }
            }

            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }

        }

        private void TxtChargesperClaim_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {
                e.Handled = true;
            }
        }

        private void TxtChargesperClaim_Validating(object sender, CancelEventArgs e)
        {
            //if (TxtChargesperClaim.Text != "")
            //{
            //    if ((Convert.ToInt16(TxtChargesperClaim.Text) < 6 || Convert.ToInt16(TxtChargesperClaim.Text) > 30) && Convert.ToInt16(TxtChargesperClaim.Text) != 0)
            //    {
            //        MessageBox.Show("Max charges per claim should not be less than 6 and greater than 30.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        e.Cancel = true;
            //    }
            //}
        }

        private void TxtDiagnosisperClaim_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;


            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {

                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {

                    if (e.KeyCode != Keys.Back | e.KeyCode == Keys.Decimal)
                    {
                        nonNumberEntered = true;
                    }
                }
            }

            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }

        }

        private void TxtDiagnosisperClaim_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered == true)
            {
                e.Handled = true;
            }
        }

        private void TxtDiagnosisperClaim_Validating(object sender, CancelEventArgs e)
        {
            //if (TxtDiagnosisperClaim.Text != "")
            //{
            //    if ((Convert.ToInt16(TxtDiagnosisperClaim.Text) < 4 || Convert.ToInt16(TxtDiagnosisperClaim.Text) > 8) && Convert.ToInt16(TxtDiagnosisperClaim.Text) != 0)
            //    {
            //        MessageBox.Show("Max diagnosis per claim should not be less than 4 and greater than 8.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        e.Cancel = true;
            //    }
            //}
        }

        private void cmbBox29_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                combo = (ComboBox)sender;

                if (cmbBox29.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBox29.Items[cmbBox29.SelectedIndex])["sBOX29"]), cmbBox29) >= cmbBox29.DropDownWidth - 60)
                    {
                        //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                        string temp = Convert.ToString(((DataRowView)cmbBox29.Items[cmbBox29.SelectedIndex])["sBOX29"]);
                        this.toolTip1.SetToolTip(cmbBox29, Convert.ToString(((DataRowView)cmbBox29.Items[cmbBox29.SelectedIndex])["sBOX29"]));
                    }
                    else
                    {
                        this.toolTip1.Hide(cmbBox29);
                    }
                }
                else
                {
                    this.toolTip1.Hide(cmbBox29);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void txtTFL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txtDFL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

      
    }
}
