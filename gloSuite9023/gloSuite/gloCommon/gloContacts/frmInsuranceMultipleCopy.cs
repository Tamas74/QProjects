using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using System.Text.RegularExpressions;
using System.Collections;
using gloSettings;
using gloCommon;

namespace gloContacts
{
    public partial class frmInsuranceMultipleCopy : Form
    {
        private string _databaseconnectionstring = "";
        private const int COL_InsPlan_ContactID = 0;
        private const int COL_InsPlan_Select = 1;
        private const int COL_InsPlan_PhyisicianName = 2;
        private const int COL_InsPlan_Name = 3;
       
        private const int COL_InsPlan_Company = 4;
        private const int COL_InsPlan_sPayerId = 5;
        //private const int COL_InsPlan_ReportingCategory =6;
       
        private const int COL_InsPlan_AddressLine1 = 6;
        private const int COL_InsPlan_AddressLine2 = 7;
        private const int COL_InsPlan_City = 8;
        private const int COL_InsPlan_State = 9;
        private const int COL_InsPlan_Zip = 10;
        private const int COL_InsPlan_InsuranceTypeCode = 11;
        private const int COL_InsPlan_InsuranceTypeDesc = 12;
        private const int COL_InsPlan_CompanyId = 13;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
       
        gloPMContacts.Insurance idealInsurance;
        bool isFormLoading = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


        public frmInsuranceMultipleCopy()
        {
            InitializeComponent();
        }
        
        public frmInsuranceMultipleCopy(string dbConn)
        {
            InitializeComponent();
            _databaseconnectionstring = dbConn;
            

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }


            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }
        }

        private void frmInsuranceMultipleCopy_Load(object sender, EventArgs e)
        {
            isFormLoading = true;
           
            FillInsuranceCombo();
            FillInsuranceList();

            isFormLoading = false;
        }


        private void FillInsuranceCombo()
        {
            gloContacts.gloContact oContact = new gloContact(_databaseconnectionstring);
            DataTable dtInsurances = null;
            try
            {
                dtInsurances = oContact.GetInsurancePlans(0);
                DataRow dr = dtInsurances.NewRow();
                dr["ContactId"] = 0;
                dr["sName"] = "";
                dtInsurances.Rows.InsertAt(dr, 0);
                dtInsurances.AcceptChanges();

                oContact.Dispose();
                oContact = null;
                if (dtInsurances != null)
                {
                    cmbInsurance.DisplayMember = "sName";
                    cmbInsurance.DataSource = dtInsurances;
                    cmbInsurance.ValueMember = "ContactId";
                    cmbInsurance.Refresh();
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                if (oContact != null)
                {
                    oContact.Dispose();
                    oContact = null;
                }
                //if (dtInsurances != null) { dtInsurances.Dispose(); dtInsurances = null; }
            }
        }

        private void FillInsuranceList()
        {
           
            gloContacts.gloContact oContact = new gloContact(_databaseconnectionstring);
            DataTable dtInsurances = null;
            try
            {
                txt_search.Text = "";
                dtInsurances = oContact.GetInsurancePlans();
                if (dtInsurances != null)
                {
                    DesignGridforInsuranceCompany(dtInsurances);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                if (oContact != null)
                {
                    oContact.Dispose();
                    oContact = null;
                }
                if (dtInsurances != null) { dtInsurances.Dispose(); dtInsurances = null; }
            }
        }

        private void DesignGridforInsuranceCompany(DataTable dtInsurances)
        {
            try
            {

                c1ViewContacts.DataSource = null;
                c1ViewContacts.Clear();

                c1ViewContacts.DataSource = dtInsurances.DefaultView;
                c1ViewContacts.Rows.Fixed = 1;

                c1ViewContacts.SetData(0, COL_InsPlan_ContactID, "Contact ID");
                c1ViewContacts.SetData(0, COL_InsPlan_PhyisicianName, "Name");
                c1ViewContacts.SetData(0, COL_InsPlan_Name, "Insurance Plan");
                c1ViewContacts.SetData(0, COL_InsPlan_Company, "Insurance Company");
                c1ViewContacts.SetData(0, COL_InsPlan_sPayerId, "Payer ID");
                c1ViewContacts.SetData(0, COL_InsPlan_AddressLine1, "Address Line 1");
                c1ViewContacts.SetData(0, COL_InsPlan_AddressLine2, "Address Line 2");
                c1ViewContacts.SetData(0, COL_InsPlan_City, "City");
                c1ViewContacts.SetData(0, COL_InsPlan_State, "State");
                c1ViewContacts.SetData(0, COL_InsPlan_Zip, "Zip");
                c1ViewContacts.SetData(0, COL_InsPlan_InsuranceTypeCode, "Insurance Type");
                c1ViewContacts.SetData(0, COL_InsPlan_InsuranceTypeDesc, "Insurance Code");
                c1ViewContacts.SetData(0, COL_InsPlan_CompanyId, "Insurance Desc");

                c1ViewContacts.Cols[COL_InsPlan_Name].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1ViewContacts.Cols[COL_InsPlan_ContactID].Visible = false;
                c1ViewContacts.Cols[COL_InsPlan_PhyisicianName].Visible = false;
                c1ViewContacts.Cols[COL_InsPlan_CompanyId].Visible = false;
                c1ViewContacts.Cols[COL_InsPlan_InsuranceTypeDesc].Visible = false;


                c1ViewContacts.Cols[COL_InsPlan_Select].Width = Width / 22;
                c1ViewContacts.Cols[COL_InsPlan_Name].Width = Width / 7;
                c1ViewContacts.Cols[COL_InsPlan_Company].Width = Width / 8;
                c1ViewContacts.Cols[COL_InsPlan_sPayerId].Width = Width / 16;
                c1ViewContacts.Cols[COL_InsPlan_AddressLine1].Width = Width / 8;
                c1ViewContacts.Cols[COL_InsPlan_AddressLine2].Width = Width / 8;
                c1ViewContacts.Cols[COL_InsPlan_City].Width = Width / 12;
                c1ViewContacts.Cols[COL_InsPlan_State].Width = Width / 14;
                c1ViewContacts.Cols[COL_InsPlan_Zip].Width = Width / 16;
                c1ViewContacts.Cols[COL_InsPlan_InsuranceTypeCode].Width = Width / 8;

                c1ViewContacts.Cols[COL_InsPlan_ContactID].AllowEditing = false;
                c1ViewContacts.Cols[COL_InsPlan_PhyisicianName].AllowEditing = false;
                c1ViewContacts.Cols[COL_InsPlan_CompanyId].AllowEditing = false;
                c1ViewContacts.Cols[COL_InsPlan_InsuranceTypeDesc].AllowEditing = false;
                c1ViewContacts.Cols[COL_InsPlan_Name].AllowEditing = false;
                c1ViewContacts.Cols[COL_InsPlan_Company].AllowEditing = false;
                c1ViewContacts.Cols[COL_InsPlan_sPayerId].AllowEditing = false;
                c1ViewContacts.Cols[COL_InsPlan_AddressLine1].AllowEditing = false;
                c1ViewContacts.Cols[COL_InsPlan_AddressLine2].AllowEditing = false;
                c1ViewContacts.Cols[COL_InsPlan_City].AllowEditing = false;
                c1ViewContacts.Cols[COL_InsPlan_State].AllowEditing = false;
                c1ViewContacts.Cols[COL_InsPlan_Zip].AllowEditing = false;
                c1ViewContacts.Cols[COL_InsPlan_InsuranceTypeCode].AllowEditing = false;
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }

        }


        private void ts_btnCopy_Click(object sender, EventArgs e)
        {
            string CopyPlan = "";
            string CopyPlanName="";
            int MessageCounter = 0;
            if (ValidateData())
            {

                for (int i = 1; i < c1ViewContacts.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(c1ViewContacts.GetData(i, COL_InsPlan_Select)) == true)
                    {
                        MessageCounter++;
                        if (CopyPlan == "")
                        {
                            CopyPlan = Convert.ToString(c1ViewContacts.GetData(i, COL_InsPlan_ContactID));
                            CopyPlanName = Convert.ToString(c1ViewContacts.GetData(i, COL_InsPlan_Name));

                        }
                        else
                        {
                            CopyPlan = CopyPlan + "," + Convert.ToString(c1ViewContacts.GetData(i, COL_InsPlan_ContactID)); ;
                            if (MessageCounter <= 10)
                            {
                                CopyPlanName = CopyPlanName + "," + Environment.NewLine + Convert.ToString(c1ViewContacts.GetData(i, COL_InsPlan_Name));
                            }
                            else if (MessageCounter == 11)
                            {
                                CopyPlanName = CopyPlanName +"...";
                            }
                        }
                    }
                }
                if (CopyPlan != "")
                {

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                    string Result = "";

                    try
                    {


                        oDB.Connect(false);

                        oDBParameters.Add("@sSuffix", txtSuffix.Text, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDBParameters.Add("@sSourceContactIDs", CopyPlan, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDBParameters.Add("@UserID", _UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nIdealPlanContactID", Convert.ToInt64(cmbInsurance.SelectedValue), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@sResult", Result, System.Data.ParameterDirection.Output, System.Data.SqlDbType.VarChar);
                        oDB.Execute("BL_MultiplecopyInsurancePlans", oDBParameters);

                        Result = Convert.ToString(oDBParameters["@sResult"].Value);
                        if (Result == "S")
                        {
                            MessageBox.Show("Selected Insuance Plan(s) are copied successfully.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Copy, "Insurance plans " + CopyPlanName + " Coppied successfully", 0, 0, 0, ActivityOutCome.Success);
                        }
                        else
                        {
                            MessageBox.Show("Error: Copy Insurance plan failed.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Contact, ActivityCategory.Insurance, ActivityType.Copy, "Copy Insurance plan failed for " + CopyPlanName + "", 0, 0, 0, ActivityOutCome.Failure);
                        }
                        oDB.Disconnect();

                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                    }
                    finally
                    {


                        oDB.Disconnect();
                        oDB.Dispose();
                        if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                    }
                    FillInsuranceList();
                }
                txt_search.Text = "";
                txtSuffix.Text = "";
                cmbInsurance.Text = "";
                
            }
        }
      
        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbInsurance_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFormLoading == false)
            {
                long ContactId = Convert.ToInt64(cmbInsurance.SelectedValue);
                if (ContactId == 0)
                {
                    idealInsurance = null;
                }
                else
                {
                    idealInsurance = LoadInsurance(ContactId);
                }
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string[] strSearchArray = null;
            string sFilter = "";
            DataView _dv = new DataView();
            _dv = (DataView)c1ViewContacts.DataSource;
            c1ViewContacts.DataSource = _dv;
            if (_dv == null) return;
            this.Cursor = Cursors.WaitCursor;
            string strSearch = txt_search.Text.Trim();

            try
            {
              this.Cursor = Cursors.Default;
              strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]");

              if (strSearch.StartsWith("*") == true)
              { strSearch = strSearch.Replace("*", "%"); }

              strSearch = strSearch.Replace("*", "[*]");

              if (strSearch.Length > 1)
              {
                  string str = strSearch.Substring(1);
                  strSearch = strSearch.Substring(0, 1) + str;
              }
              if (strSearch.Trim() != "")
              {
                  strSearchArray = strSearch.Split(',');
              }

              if (strSearch.Trim() != "")
              {

                  if (strSearchArray.Length == 1)
                  {
                      strSearch = strSearchArray[0].Trim();
                      _dv.RowFilter = _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                      _dv.Table.Columns["sName"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                      _dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                      _dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                      _dv.Table.Columns["Company"].ColumnName + " Like '" + strSearch + "%' OR " +
                                      //_dv.Table.Columns["ReportingCategory"].ColumnName + " Like '" + strSearch + "%'  OR " +
                                      _dv.Table.Columns["sPayerId"].ColumnName + " Like '" + strSearch + "%'";
                  }
                  else
                  {
                      //For Comma separated  value search
                      for (int i = 0; i < strSearchArray.Length; i++)
                      {
                          strSearch = strSearchArray[i].Trim();
                          if (strSearch.Trim() != "")
                          {
                              if (i == 0)
                              {
                                  sFilter = " ( " + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                   _dv.Table.Columns["sName"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    _dv.Table.Columns["Company"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                   // _dv.Table.Columns["ReportingCategory"].ColumnName + " Like '" + strSearch + "%'OR " +
                                                    _dv.Table.Columns["sPayerID"].ColumnName + " Like '" + strSearch + "%')";
                              }
                              else
                              {
                                  if (sFilter != "")
                                      sFilter = sFilter + " AND ";

                                  sFilter = sFilter + " (" + _dv.Table.Columns["PhysicianName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                             _dv.Table.Columns["sName"].ColumnName + " Like '%" + strSearch + "%' OR " +
                                                             _dv.Table.Columns["insuranceTypeDesc"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                             _dv.Table.Columns["InsuranceTypeCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                             _dv.Table.Columns["Company"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                             //_dv.Table.Columns["ReportingCategory"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                             _dv.Table.Columns["sPayerID"].ColumnName + " Like '" + strSearch + "%')";
                              }

                          }
                      }
                      _dv.RowFilter = sFilter;
                  }

              }
              else
              {
                  _dv.RowFilter = "";
              }
            }

            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                strSearchArray = null;
                sFilter = null;
                strSearch = null;
            }
        }

        private void c1ViewContacts_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //if (e.Col == 1)
            //{
            //    if( Convert.ToBoolean(c1ViewContacts.GetData(e.Row, COL_InsPlan_Select))==true)
            //    {
            //        gloPMContacts.Insurance oInsurance = LoadInsurance(Convert.ToInt64(c1ViewContacts.GetData(e.Row, COL_InsPlan_ContactID)));
            //        oInsurance.ContactID = Convert.ToInt64(c1ViewContacts.GetData(e.Row, COL_InsPlan_ContactID));
            //        oInsurance.ContactType = "Insurance";
            //        lstInsurance.Add(oInsurance);
            //    }
            //}
        }


        private gloPMContacts.Insurance LoadInsurance(long _ContactID)
        {
            gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
            gloPMContacts.Insurance oInsurance = new gloPMContacts.Insurance();
            try
            {

                oInsurance = oglocontact.SelectInsurance(_ContactID);

                return oInsurance;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oglocontact != null) { oglocontact.Dispose(); oglocontact = null; }
                if (oInsurance != null) { oInsurance.Dispose(); oInsurance = null; }
            }
        }

        private void SaveCorrectedReplacement(gloPMContacts.Insurance oInsurance)
        {
            
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            gloPMContacts.PlanCorrectedReplacement _oCorrectedReplacement = new gloPMContacts.PlanCorrectedReplacement();
            gloPMContacts.gloContacts oglocontact = new gloPMContacts.gloContacts(_databaseconnectionstring);
            DataTable dt;
            long PlanCompanyId = 0;
            try
            {
                oDB.Connect(false);
                try
                {
                    oDB.Connect(false);
                    string SqlQuery = "select nCompanyId from Contact_InsurancePlan_Association where nContactId=" + oInsurance.ContactID;
                    oDB.Retrive_Query(SqlQuery, out  dt);
                    SqlQuery = string.Empty;
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            PlanCompanyId = Convert.ToInt64(dt.Rows[0][0]);
                        }
                    }

                    _oCorrectedReplacement = oglocontact.SetCorrectedReplacementInfo(oInsurance.ContactID);

                    oDBParameters.Add("@nContactID", oInsurance.ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nInsCompanyID", PlanCompanyId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@bIsCorrectRplmnt", (oInsurance.IsCorrectedReplacement == true ? 1 : 0), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@nCreatedUserID", _oCorrectedReplacement.nCreatedUserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nModifiedUserID", _UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDB.Execute("BL_INUP_CorrectedReplacement_Plan", oDBParameters);
                    oDB.Disconnect();
                }
                catch (gloDatabaseLayer.DBException dbEx)
                {
                    dbEx.ERROR_Log(dbEx.ToString());

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
            }

        }

        public bool IsExistsInsurance(string _Insurance, Int64 _nCompanyID)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "";
                _sqlQuery = "SELECT COUNT(*) AS nCount FROM Contacts_MST INNER JOIN Contact_InsurancePlan_Association ON " +
                            "Contacts_MST.nContactID = Contact_InsurancePlan_Association.nContactId WHERE Contacts_MST.sContactType = 'Insurance' " +
                            "AND (Contact_InsurancePlan_Association.nCompanyId =" + _nCompanyID + ") AND " +
                            "Contacts_MST.sName ='" + _Insurance.Replace("'", "''") + "' AND Contacts_MST.bIsBlocked !=1 ";
              
                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ToString();
                DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        private bool ValidateData()
        {
            if (txtSuffix.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Suffix for insurance plan name.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSuffix.Clear();
                txtSuffix.Focus();
                return false;
            }
            bool isPlanSelected = false;
            for (int i = 1; i < c1ViewContacts.Rows.Count; i++)
            {
                if (Convert.ToBoolean(c1ViewContacts.GetData(i, COL_InsPlan_Select)) == true)
                {
                    isPlanSelected = true;
                    string PlanName = Convert.ToString(c1ViewContacts.GetData(i, COL_InsPlan_Name)) + Convert.ToString(txtSuffix.Text);
                    bool _result = IsExistsInsurance(PlanName, Convert.ToInt64(c1ViewContacts.GetData(i, COL_InsPlan_CompanyId)));
                    if (_result)
                    {
                        DialogResult dResult = MessageBox.Show("Contact name '" + PlanName + "' already exists. Do you want to register it anyway? ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dResult == DialogResult.No)
                        {
                            c1ViewContacts.SetData(i, COL_InsPlan_Select, 0);
                        }
                    }

                    //try
                    //{
                    //    IEnumerable<C1.Win.C1FlexGrid.Row> index = (from r in c1ViewContacts.Rows.Cast<C1.Win.C1FlexGrid.Row>()
                    //                                                where Convert.ToString(r[COL_InsPlan_Name]) == PlanName
                    //                                                select r);
                    //    if (index.Count() > 0)
                    //    {
                    //        DialogResult dResult = MessageBox.Show("Contact name '" + PlanName + "' already exists. Do you want to register it anyway? ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    //        if (dResult == DialogResult.No)
                    //        {
                    //            c1ViewContacts.SetData(i, COL_InsPlan_Select, 0);
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    //}
                }
            }

            if (isPlanSelected == false)
            {
                MessageBox.Show("Please select insurance plan to copy.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                c1ViewContacts.Focus();
                return false;
            }
            return true;
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txt_search.Clear();
            txt_search.Focus();
        }

      
        

    }
}
