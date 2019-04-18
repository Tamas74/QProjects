using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloContacts
{
    // ------------------------abhisekh pandey 10/02/2010--------------------------------
    //-------------------new form for Insurance Reporting Category---------------------

    public partial class frmInsuranceReportingCategory : Form
    {


        #region "Private Variables"
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _ClinicID = 0;
        //private Int64 _ContactID = 1;
        //SHUBHANGI 20100219
        private bool _IsModified = false;
        private bool _IsSaveClicked = false;

        private Int64 _nInsuranceRepCtgryID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private gloListControl.gloListControl oListControl;
        //private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Insurance;

        string _InsuranceReportingCategory;
        Int64 _nId;

        DataTable _dtRemovedItem = new DataTable();

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
        public frmInsuranceReportingCategory(String DatabaseConnectionString)
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
        }
        //Modify Constructor
        public frmInsuranceReportingCategory(Int64 InsuranceRepCtgryID, String DatabaseConnectionString)
        {
            InitializeComponent();

            _nInsuranceRepCtgryID = InsuranceRepCtgryID;
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
        }



        private void ts_btnClose_Click(object sender, EventArgs e)
        {
           this.Close();

        }

        private void ts_btnSave_Click(object sender, EventArgs e)
        { 
           
            if (SaveInsuranceReportingCategory() == true)
            //If it saved successfully
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private bool SaveInsuranceReportingCategory()
        {
            bool _ReturnResult = true;


            gloDatabaseLayer.DBLayer oDB = null;
            gloPMContacts.gloContacts oglocontact = null;
            string _sCode = txtCode.Text;
            string strsql = null;
            try
            {
                if (ValidateData() == true)
                {

                    oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
                    bool _result;
                    // _Description = txtDescription.Text;
                    _InsuranceReportingCategory = txtDescription.Text.Trim();
                    _result = oglocontact.IsExistsInsuranceRepCtgry(_InsuranceReportingCategory, Convert.ToInt64(txtCode.Tag));
                    if (_result == true)
                    {
                        if (DialogResult.No == (MessageBox.Show("Insurance Reporting Category already exists. Do you want to register it anyway?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)))
                        {
                            txtDescription.Focus();
                            return false;
                        }

                    }

                    {
                        //Call the Save function
                        //TODO: Save 

                       // Int64 _Contactid;
                        _nId = SaveData(_nInsuranceRepCtgryID, _sCode, _InsuranceReportingCategory, _ClinicID);

                        //Associates insurances to the insurance Reporting Category

                        strsql = "Delete from Contact_InsurancePlanReportingCat_Association where nReportingCategoryId = " + _nInsuranceRepCtgryID + " and nClinicId = " + _ClinicID;
                        oDB.Execute_Query(strsql);

                        Int64 _PlanContactID;
                        for (int i = 1; i < c1Insurance.Rows.Count; i++)
                        {
                            //_PlanContactID = Convert.ToInt64(c1Insurance.GetData(i + 1, COL_ContactID));
                            _PlanContactID = Convert.ToInt64(c1Insurance.GetData(i, COL_ContactID));
                            strsql = "Delete from Contact_InsurancePlanReportingCat_Association where nContactId =" + _PlanContactID;
                            oDB.Execute_Query(strsql);
                            strsql = "";
                            strsql = "Insert into Contact_InsurancePlanReportingCat_Association (nReportingCategoryId,nContactId,nClinicId) Values(" + _nId + "," + _PlanContactID + "," + _ClinicID + "" + ")";
                            oDB.Execute_Query(strsql);


                        }
                    }
                    oDB.Disconnect();
                    _IsSaveClicked = true;
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
                if (oglocontact != null)
                { oglocontact.Dispose(); oglocontact = null; }
                _sCode = null;
                strsql = null;
            }

            return _ReturnResult;
        }

        private Int64 SaveData(Int64 _nInsuranceRptCatID, string Code, string Description, Int64 ClinicId)
        {

            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            try
            {
                _IsSaveClicked = true;
                //Int64 _Contactid;
                oDBParameters.Clear();
                object _intresult = 0;
                oDBParameters.Add("@nId", _nInsuranceRptCatID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@Code", Code, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@Description", Description, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@ClinicId", ClinicId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                int result = oDB.Execute("CO_INUP_InsuranceReportingCategory_MST", oDBParameters, out _intresult);

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
            }
            return _result;

        }



        #region "Private Methods"

        public DataTable FillInsuranceReportingCategory()
        {
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDb = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strsql = null;
            String _strsql1 = "";
            try
            {
                oDb.Connect(false);


                _strsql = "select nID,sCode,sDescription,nClinicID from dbo.Contacts_InsuranceReportingCategory_MST where nId= '" + _nInsuranceRepCtgryID + "'";
                oDb.Retrive_Query(_strsql, out dt);
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    txtCode.Text = Convert.ToString(dt.Rows[0]["sCode"]);
                    txtDescription.Text = Convert.ToString(dt.Rows[0]["sDescription"]);
                    //abhisekh 11/02/2010
                    //Assign Company Id to the tag property of code text box so that we can check the company Id at the time of duplicate recore 
                    txtCode.Tag = Convert.ToInt64(dt.Rows[0]["nID"]);
                }
                _strsql1 = "SELECT dbo.Contacts_MST.sName, dbo.Contact_InsurancePlanReportingCat_Association.nContactId,dbo.Contact_InsurancePlanReportingCat_Association.nReportingCategoryId FROM dbo.Contacts_MST INNER JOIN dbo.Contact_InsurancePlanReportingCat_Association ON dbo.Contacts_MST.nContactID = dbo.Contact_InsurancePlanReportingCat_Association.nContactId INNER JOIN dbo.Contacts_InsuranceReportingCategory_MST ON dbo.Contact_InsurancePlanReportingCat_Association.nReportingCategoryId = dbo.Contacts_InsuranceReportingCategory_MST.nID"
                + " WHERE  Contacts_InsuranceReportingCategory_MST.nID = " + _nInsuranceRepCtgryID + " AND Contact_InsurancePlanReportingCat_Association.nClinicID = " + _ClinicID + " ";

                oDb.Retrive_Query(_strsql1, out dt);

                oDb.Disconnect();
            }

            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            finally
            {
                _strsql = null;
                _strsql1 = null;
            }
            return dt;

            //throw new Exception("The method or operation is not implemented.");
        }


        private bool ValidateData()
        {
            //Commented
            //'Coz it is not required field. Now we are taking only company name from user
            //if (txtCode.Text.Trim() == "")
            //{
            //    MessageBox.Show("Please enter Code", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtCode.Focus();
            //    return false;
            //}
            if (txtDescription.Text.Trim() == "")
            {
                MessageBox.Show("Please enter reporting category name.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescription.Focus();
                return false;
            }
            return true;
            //throw new Exception("The method or operation is not implemented.");
        }

        #endregion


        private void FillInsurancePlans(DataTable dtInsurance)
        {
            if (dtInsurance != null)
            {
             //   c1Insurance.Clear();
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
                c1Insurance.Cols[COL_ReportingCategory].Caption = "Insurance Reporting Category";
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
                c1Insurance.Cols[COL_ReportingCategory].Visible = false;
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
            oListControl.ControlHeader = "Insurance Plans";
            oListControl.ShowInsPlansWithoutCategories = true;

           // _CurrentControlType = gloListControl.gloListControlType.Insurance;
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

            this.Controls.Add(oListControl);

            c1Insurance.Refresh();
            for (int i = 1; i < c1Insurance.Rows.Count; i++)
            {
                //checked the added insurances in olistcontrol.
                oListControl.SelectedItems.Add(Convert.ToInt64(c1Insurance.GetData(i, COL_ContactID)), Convert.ToString(c1Insurance.GetData(i, COL_PlanName)));
            }


            pnlTopToolStrip.Visible = false;
            pnl_Base.Visible = false;
            panel3.Visible = false;
            oListControl.OpenControl();
            oListControl.Dock = DockStyle.Fill;
            oListControl.BringToFront();

            btnBrowseInsurance.Focus();
        }

        private void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            _IsModified = true;
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
                            dtCompanyPlans = (DataTable)c1Insurance.DataSource;
                            _dvTemp = dtCompanyPlans.DefaultView;
                            _dvTemp.RowFilter = "ReportingCategory <> ''";
                            dtCompanyPlans = _dvTemp.ToTable();

                            dtCompanyPlans.Merge(dtSelectedPlans);
                            FillInsurancePlans(dtCompanyPlans);
                        }
                        else
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
                    panel3.Visible = true;
                    btnBrowseInsurance.Focus();
                }
                else
                {
                    if (c1Insurance.DataSource != null)
                    {
                        dtCompanyPlans = (DataTable)c1Insurance.DataSource;
                        _dvTemp = dtCompanyPlans.DefaultView;
                        _dvTemp.RowFilter = "ReportingCategory <> ''";
                        dtCompanyPlans = _dvTemp.ToTable();
                        FillInsurancePlans(dtCompanyPlans);
                    }
                }
                pnlTopToolStrip.Visible = true;
                pnl_Base.Visible = true;
                panel3.Visible = true;
                btnBrowseInsurance.Focus();
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
                panel3.Visible = true;
            }
        }

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {
            //c1Insurance.Rows.RemoveRange(1, c1Insurance.Rows.Count - 1);
            if (c1Insurance.DataSource != null)
            {

                DataTable _dtPlans = (DataTable)c1Insurance.DataSource;
                if (_dtPlans != null)
                {
                    for (int iRow = _dtPlans.Rows.Count - 1; iRow >= 0; iRow--)
                    {
                        if (_dtPlans.Rows[iRow]["ReportingCategory"].ToString() != "")
                        {
                            // TO ADD ROWs IN REMOVE TABLE LIST //
                            if (_dtRemovedItem != null && _dtRemovedItem.Rows.Count > 0)
                            { }
                            else
                            { _dtRemovedItem = _dtPlans.Clone(); }

                            _dtRemovedItem.Rows.Add(_dtPlans.Rows[iRow].ItemArray);
                        }
                    }
                }
                ((DataTable)c1Insurance.DataSource).Clear();
                _IsModified = true;
            }

        }

        private void frmInsuranceReportingCategory_Load(object sender, EventArgs e)
        {

            // gloC1FlexStyle.Style(c1Insurance, false);
            
            gloContacts.gloContact oContact = new gloContacts.gloContact(_databaseconnectionstring);
            DataTable dtInsPlans = null ;

            try
            {
                if (_nInsuranceRepCtgryID > 0)
                {
                    FillInsuranceReportingCategory();
                    dtInsPlans = oContact.GetCategoryInsurancePlans(_nInsuranceRepCtgryID);
                    _IsModified = false;
                }
                else
                {
                    txtCode.Tag = 0;
                    dtInsPlans = oContact.GetCategoryInsurancePlans(1);
                    dtInsPlans.Clear();
                }

                FillInsurancePlans(dtInsPlans);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dtInsPlans != null) { dtInsPlans.Dispose(); dtInsPlans = null; }
            }
           
        }

        public string ReportingCategoryName
        {
            get
            {
                return _InsuranceReportingCategory;
            }
        }

        public Int64 ReportingCategoryID
        {
            get
            {
                return _nId;
            }
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
                    DataTable dtPlans = (DataTable)c1Insurance.DataSource;
                    DataView _dvTemp = dtPlans.DefaultView;

                    if (c1Insurance.GetData(_Row, COL_ReportingCategory).ToString() != "")
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
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            _IsModified = true;
        }
        

        private void frmInsuranceReportingCategory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_IsModified == true  && _IsSaveClicked == false)
            {
                DialogResult _Result;
                _Result = MessageBox.Show("Do you want to save the changes?", _messageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (_Result == DialogResult.Yes)
                {
                    if (SaveInsuranceReportingCategory() == false)
                    {
                        e.Cancel = true;
                        _IsSaveClicked = false;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.Yes;
                    }
                }
                else if (_Result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

            }
        }

    }


}