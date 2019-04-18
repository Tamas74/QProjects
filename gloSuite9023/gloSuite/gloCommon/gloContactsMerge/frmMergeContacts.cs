using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloSecurity;
using System.IO;

namespace gloContactsMerge
{
    public partial class frmMergeContacts : Form
    {
        private int COL_PlanId = 0;
        private int COL_MergeTo = 1;
        private int COL_MergeFrom = 2;
        private int COL_Code = 3;
        private int COL_PlanName = 4;
        private int COL_InfoWeight = 5;
        private int COL_AssociatedPatientRecords = 6;
        private int COL_CompanyName = 7;
        private int COL_InsuranceType = 8;
        private int COL_PayerID = 9;
        private int COL_CPTCrosswalk = 10;
        private int COL_AddLine1 = 11;
        private int COL_AddLine2 = 12;
        private int COL_City = 13;
        private int COL_State = 14;
        private int COL_Zip = 15;
        private int COL_Phone = 16;
        private int COL_Fax = 17;
        private int COL_ReportingCategory = 18;
        private int COL_IncludeTaxonomyForElectronic = 19;
        private int COL_IncludeTaxonomyForPaper = 20;
        private int COL_PaperBillingTaxonomy = 21;
        private int COL_PaperRenderingTaxonomy = 22;
        private int COL_ElectronicBillingTaxonomy = 23;
        private int COL_ElectronicRenderingTaxonomy = 24;
        private int COL_IsInstitutionalBilling = 25;
        private int COL_DefaultBillingMethod = 26;
        private int COL_ClearingHouse = 27;
        private int COL_Count = 28;

        private string sConnectionString = String.Empty;
        private string _messageboxcaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


        private Boolean bIsFromPMAdmin = false;
        public bool IsInsurancePlanPresent { get; set; }

        public frmMergeContacts()
        {
            InitializeComponent();
            //sConnectionString = @"SERVER=glosvr02\sql2008r2;DATABASE=glo8090_DEV;User ID=sa;Password=saglosvr02";
            sConnectionString = @"SERVER=dev155;DATABASE=glo8090Demo;User ID=sa;Password=sadev155";

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion
        }

        public frmMergeContacts(string ConnectionString, bool _isFromPMAdmin)
        {
            InitializeComponent();
            //sConnectionString = @"SERVER=glosvr02\sql2008r2;DATABASE=glo8090_DEV;User ID=sa;Password=saglosvr02";
            sConnectionString = ConnectionString;
            bIsFromPMAdmin = _isFromPMAdmin;
            if (LoadInsuranceGrid())
            {
                IsInsurancePlanPresent = true;
            }
            else
            {
                IsInsurancePlanPresent = false;
            }
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM";
                }
            }
            else
            { _messageboxcaption = "gloPM"; }

            #endregion
        }

        private void frmMergeContacts_Load(object sender, EventArgs e)
        {
            if (bIsFromPMAdmin == false)
            {
                string[] sConnProperties = sConnectionString.Split(';');
                if (sConnProperties != null)
                {
                    lblServerName.Text = sConnProperties[0].Replace("=", ": ");
                    lblDatabaseName.Text = sConnProperties[1].Replace("=", ": ");
                }
            }
            else
            {
                pnlMergeStatus.Visible = false;
                pnlAdditionalColumns.Dock = DockStyle.Fill;
            }
            FillInsuranceGrid();
            FillColumnCombobox();
        }

        private void FillColumnCombobox()
        {
            DataTable dtInsurance = null;
            MergeContacts clsMergeContact = null;
            try
            {
                clsMergeContact = new MergeContacts(sConnectionString);
                dtInsurance = clsMergeContact.GetInsuranceContactList();

                if (dtInsurance != null && dtInsurance.Rows.Count > 0)
                {
                    foreach (DataColumn item in dtInsurance.Columns)
                    {
                        if (item.ColumnName != "Plan ID" && item.ColumnName != "Merge To" && item.ColumnName != "Merge From" && item.ColumnName != "Status" && item.ColumnName != "Plan Name" && item.ColumnName != "Info Weight" && item.ColumnName != "Associated Patient Records" && item.ColumnName != "Company Name" && item.ColumnName != "Insurance Type" && item.ColumnName != "Payer ID" && item.ColumnName != "CPT Crosswalk")
                        {
                            chkLstGridColumn.Items.Add(item.ColumnName);
                        }

                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dtInsurance != null)
                {
                    dtInsurance.Dispose();
                    dtInsurance = null;
                }
                if (clsMergeContact != null)
                {
                    clsMergeContact.Dispose();
                    clsMergeContact = null;
                }
                c1InsuranceList.EndUpdate();
            }
        }

        private void FillInsuranceGrid()
        {
            try
            {
                if (LoadInsuranceGrid())
                {
                    DesignGrid();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }



        private bool LoadInsuranceGrid()
        {
            bool bIsGridLoaded = false;
            DataTable dtInsurance = null;
            MergeContacts clsMergeContact = null;
            try
            {
                clsMergeContact = new MergeContacts(sConnectionString);
                dtInsurance = clsMergeContact.GetInsuranceContactList();

                if (dtInsurance != null && dtInsurance.Rows.Count > 0)
                {
                    c1InsuranceList.DataSource = null;
                    c1InsuranceList.BeginUpdate();
                    c1InsuranceList.DataSource = dtInsurance.Copy().DefaultView;

                    bIsGridLoaded = true;
                    c1InsuranceList.EndUpdate();
                }
                else
                {
                    bIsGridLoaded = false;
                }
            }
            catch (Exception ex)
            {
                bIsGridLoaded = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dtInsurance != null)
                {
                    dtInsurance.Dispose();
                    dtInsurance = null;
                }
                if (clsMergeContact != null)
                {
                    clsMergeContact.Dispose();
                    clsMergeContact = null;
                }
                c1InsuranceList.EndUpdate();
            }
            return bIsGridLoaded;
        }

        private void DesignGrid()
        {
            if (c1InsuranceList.DataSource != null)
            {
                for (int i = 0; i <= c1InsuranceList.Cols.Count - 1; i++)
                {
                    c1InsuranceList.Cols[i].AllowEditing = false;
                }

                c1InsuranceList.Cols[COL_PlanId].Visible = false;
                SetColumnVisibility();

                c1InsuranceList.Cols[COL_MergeTo].AllowEditing = true;
                c1InsuranceList.Cols[COL_MergeFrom].AllowEditing = true;
                c1InsuranceList.Cols[COL_MergeTo].DataType = typeof(bool);
                c1InsuranceList.Cols[COL_MergeFrom].DataType = typeof(bool);

                int _width = Width;
                c1InsuranceList.Cols[COL_MergeTo].Width = Convert.ToInt32(_width * 0.06 - 5);
                c1InsuranceList.Cols[COL_MergeFrom].Width = Convert.ToInt32(_width * 0.07 - 5);
                c1InsuranceList.Cols[COL_Code].Width = Convert.ToInt32(_width * 0.07 - 5);
                c1InsuranceList.Cols[COL_PlanName].Width = Convert.ToInt32(_width * 0.17 - 5);
                c1InsuranceList.Cols[COL_InfoWeight].Width = Convert.ToInt32(_width * 0.075 - 5);
                c1InsuranceList.Cols[COL_AssociatedPatientRecords].Width = Convert.ToInt32(_width * 0.1 - 5);
                c1InsuranceList.Cols[COL_CompanyName].Width = Convert.ToInt32(_width * 0.1 - 5);
                c1InsuranceList.Cols[COL_InsuranceType].Width = Convert.ToInt32(_width * 0.13 - 5);
                c1InsuranceList.Cols[COL_PayerID].Width = Convert.ToInt32(_width * 0.07 - 5);
                c1InsuranceList.Cols[COL_CPTCrosswalk].Width = Convert.ToInt32(_width * 0.07 - 5);

                c1InsuranceList.Cols[COL_InfoWeight].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1InsuranceList.Cols[COL_AssociatedPatientRecords].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1InsuranceList.Cols[COL_PayerID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

                c1InsuranceList.Rows.Fixed = 1;
            }
        }

        private void SetColumnVisibility()
        {
            for (int i = 11; i < c1InsuranceList.Cols.Count; i++)
            {
                c1InsuranceList.Cols[i].Visible = false;
            }
            //c1InsuranceList.Cols[COL_AddLine1].Visible = false;
            //c1InsuranceList.Cols[COL_AddLine2].Visible = false;
            //c1InsuranceList.Cols[COL_City].Visible = false;
            //c1InsuranceList.Cols[COL_State].Visible = false;
            //c1InsuranceList.Cols[COL_Zip].Visible = false;
            //c1InsuranceList.Cols[COL_Phone].Visible = false;
            //c1InsuranceList.Cols[COL_Fax].Visible = false;
            //c1InsuranceList.Cols[COL_ReportingCategory].Visible = false;
            //c1InsuranceList.Cols[COL_IncludeTaxonomyForElectronic].Visible = false;
            //c1InsuranceList.Cols[COL_IncludeTaxonomyForPaper].Visible = false;
            //c1InsuranceList.Cols[COL_PaperBillingTaxonomy].Visible = false;
            //c1InsuranceList.Cols[COL_PaperRenderingTaxonomy].Visible = false;
            //c1InsuranceList.Cols[COL_ElectronicBillingTaxonomy].Visible = false;
            //c1InsuranceList.Cols[COL_ElectronicRenderingTaxonomy].Visible = false;
            //c1InsuranceList.Cols[COL_IsInstitutionalBilling].Visible = false;
            //c1InsuranceList.Cols[COL_DefaultBillingMethod].Visible = false;
            //c1InsuranceList.Cols[COL_ClearingHouse].Visible = false;
        }

        private void c1InsuranceList_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //Console.WriteLine(String.Format("START: c1InsuranceList_AfterEdit - {0}", DateTime.Now.ToString("hh:mm:ss")));
            //try
            //{
            //    c1InsuranceList.BeginUpdate();

            //    c1InsuranceList.AfterEdit -= new C1.Win.C1FlexGrid.RowColEventHandler(c1InsuranceList_AfterEdit);

            //    if (c1InsuranceList.Rows.Count > 0)
            //    {
            //        if (e.Col == COL_MergeTo)
            //        {
            //            for (int i = 1; i <= c1InsuranceList.Rows.Count - 1; i++)
            //            {
            //                if (c1InsuranceList.GetCellCheck(i, COL_MergeTo) == C1.Win.C1FlexGrid.CheckEnum.Checked && c1InsuranceList.RowSel == i)
            //                {
            //                    c1InsuranceList.SetCellCheck(i, COL_MergeFrom, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
            //                    c1InsuranceList.Rows[i].AllowEditing = false;
            //                }
            //                else
            //                {
            //                    c1InsuranceList.Rows[i].AllowEditing = true;
            //                    c1InsuranceList.SetCellCheck(i, COL_MergeTo, C1.Win.C1FlexGrid.CheckEnum.Unchecked);

            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}
            //finally
            //{
            //    c1InsuranceList.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(c1InsuranceList_AfterEdit);
            //    c1InsuranceList.EndUpdate();
            //    Console.WriteLine(String.Format("Finish: c1InsuranceList_AfterEdit - {0}", DateTime.Now.ToString("hh:mm:ss")));
            //}
        }
        Int64 _id;
        private void c1InsuranceList_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            try
            {
                if ((c1InsuranceList.Rows.Count > 1))
                {
                    //Code changes for maintaining the selected row after sorting 
                    int _index = c1InsuranceList.FindRow(Convert.ToString(_id), 0, COL_PlanId, false, false, false);
                    c1InsuranceList.ShowCell(_index, COL_PlanName);
                    c1InsuranceList.Row = _index;
                    c1InsuranceList.Select();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void c1InsuranceList_BeforeSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
        {
            if ((c1InsuranceList.Rows.Count > 1))
            {
                _id = Convert.ToInt64(c1InsuranceList.Rows[c1InsuranceList.RowSel][COL_PlanId]);
            }
        }

        private void c1InsuranceList_MouseMove(object sender, MouseEventArgs e)
        {
            //gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1.Win.C1FlexGrid.C1FlexGrid)sender, e.Location);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string sFilter = string.Empty;
            string[] strSearchArray = null;
            if (c1InsuranceList.DataSource != null)//For Blank DataBase
            {
                DataView dv = (DataView)c1InsuranceList.DataSource;
                if (txtSearch.Text.Trim() != "")
                {
                    string strSearch = txtSearch.Text.Trim();
                    sFilter = "";

                    strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");

                    if (strSearch.Trim() != "")
                    {
                        strSearchArray = strSearch.Split(',');
                    }
                    if (strSearch.Trim() != "")
                    {

                        if (strSearchArray.Length == 1)
                        {
                            
                            strSearch = strSearchArray[0].Trim();
                            if (strSearch.Trim()!="")
                            {
                                sFilter += " ( ";
                                if (strSearch.Length > 1)
                                {
                                    string str = strSearch.Substring(1).Replace("%", "");
                                    strSearch = strSearch.Substring(0, 1) + str;
                                }

                                for (int i = 0; i < dv.Table.Columns.Count; i++) //This loop is for Column Names.
                                {
                                    if (dv.Table.Columns[i].DataType == typeof(System.String))
                                    {
                                        if (dv.Table.Columns[i].ColumnName == "Plan Name" || dv.Table.Columns[i].ColumnName == "Payer ID" || dv.Table.Columns[i].ColumnName == "Company Name" || dv.Table.Columns[i].ColumnName == "Insurance Type")
                                        {
                                            if (sFilter != " ( ")//Add OR Except First Time
                                            {
                                                sFilter += " OR ";
                                            }
                                            sFilter += "[" + dv.Table.Columns[i].ColumnName + "] Like '%" + strSearch + "%'";
                                        }
                                    }
                                }
                                sFilter += " )"; 
                            }
                        }
                        else
                        {
                            //For Comma separated  value search
                            for (int k = 0; k < strSearchArray.Length; k++) //Loop is for Search Text.
                            {

                                
                                strSearch = strSearchArray[k].Trim();

                                if (strSearch.Trim()!="")
                                {
                                    if (sFilter != "")
                                    {
                                        sFilter += " AND ";
                                    }

                                    string sTempFilter = " ( ";
                                    if (strSearch.Length > 1)
                                    {
                                        string str = strSearch.Substring(1).Replace("%", "");
                                        strSearch = strSearch.Substring(0, 1) + str;
                                        str = null;
                                    }

                                    for (int i = 0; i < dv.Table.Columns.Count; i++) //This loop is for Column Names.
                                    {
                                        if (dv.Table.Columns[i].DataType == typeof(System.String))
                                        {
                                            if (dv.Table.Columns[i].ColumnName == "Plan Name" || dv.Table.Columns[i].ColumnName == "Payer ID" || dv.Table.Columns[i].ColumnName == "Company Name" || dv.Table.Columns[i].ColumnName == "Insurance Type")
                                            {
                                                if (sTempFilter != " ( ")//Add OR Except First Time
                                                {
                                                    sTempFilter += " OR ";
                                                }
                                                sTempFilter += "[" + dv.Table.Columns[i].ColumnName + "] Like '%" + strSearch + "%'";
                                            }
                                        }
                                    }
                                    sTempFilter += " )";

                                    sFilter = sTempFilter;
                                    sTempFilter = null; 
                                }
                            }

                        }
                        dv.RowFilter = sFilter;
                    }
                }
                else
                {
                    dv.RowFilter = "";
                }
                c1InsuranceList.DataSource = dv;
            }
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            if (bIsFromPMAdmin == false)
            {
                Application.Exit();
            }
            else
            {
                this.Close();
            }
        }

        private void tsbtn_Merge_Click(object sender, EventArgs e)
        {
            tsbtn_Merge.Enabled = false;
            Int64 nMergeToInsuranceID = 0;
            List<string> lstMergeToInsurance = null;
            MergeContacts clsMergeContacts = null;
            string[] stringSeparators = new string[] { "!@#$" };
            try
            {
                int nMergeToRow = 0;
                int nMergeFromRow = 0;

                Boolean bIsMergeToInsuranceHold = false;
                Boolean bIsMergeFromInsuranceHold = false;
                string sMergeToInsuranceHold = string.Empty, sMergeFromInsuranceHold = string.Empty;
                string sMergeToInsurance = string.Empty, sMergeFromInsurance = string.Empty;

                if (c1InsuranceList.Rows.Count > 0)
                {
                    nMergeToRow = c1InsuranceList.FindRow(Convert.ToBoolean(C1.Win.C1FlexGrid.CheckEnum.Checked.GetHashCode()), 1, COL_MergeTo, true);
                    nMergeFromRow = c1InsuranceList.FindRow(Convert.ToBoolean(C1.Win.C1FlexGrid.CheckEnum.Checked.GetHashCode()), 1, COL_MergeFrom, true);
                }

                if (nMergeToRow <= 0)
                {
                    MessageBox.Show("Select \"Merge To\" Insurance Plan", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tsbtn_Merge.Enabled = true;
                    return;
                }
                else
                {
                    nMergeToInsuranceID = Convert.ToInt64(c1InsuranceList.GetData(nMergeToRow, COL_PlanId));
                    sMergeToInsurance = Convert.ToString(c1InsuranceList.GetData(nMergeToRow, COL_PlanName));
                    //bIsMergeToInsuranceHold = Convert.ToBoolean(c1InsuranceList.GetData(nMergeToRow, "Plan Hold"));
                    //if (bIsMergeToInsuranceHold)
                    //{
                    //    sMergeToInsurance += "[Hold]";
                    //}
                    
                    //sMergeToInsuranceHold = Convert.ToString(c1InsuranceList.GetData(nMergeToRow, COL_PlanName));
                }

                if (nMergeFromRow <= 0)
                {
                    MessageBox.Show("Select at least one \"Merge From\" Insurance Plan", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tsbtn_Merge.Enabled = true;
                    return;
                }

                lstMergeToInsurance = new List<string>();
                for (int i = 1; i < c1InsuranceList.Rows.Count; i++)
                {
                    if (c1InsuranceList.GetCellCheck(i, COL_MergeFrom) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        lstMergeToInsurance.Add(Convert.ToString(c1InsuranceList.GetData(i, COL_PlanId)) + stringSeparators[0] + Convert.ToString(c1InsuranceList.GetData(i, COL_PlanName)) + stringSeparators[0] + Convert.ToString(i));
                        sMergeFromInsurance = sMergeFromInsurance + "- " + Convert.ToString(c1InsuranceList.GetData(i, COL_PlanName)) + "\n";
                        //bIsMergeFromInsuranceHold=Convert.ToBoolean(c1InsuranceList.GetData(i, "Plan Hold"));
                        //if (bIsMergeFromInsuranceHold)
                        //{
                        //    //sMergeFromInsuranceHold = sMergeFromInsuranceHold + "- " + Convert.ToString(c1InsuranceList.GetData(i, COL_PlanName)) + "\n";
                        //    sMergeFromInsurance = sMergeFromInsurance + "- " + Convert.ToString(c1InsuranceList.GetData(i, COL_PlanName)) + "[Hold]\n";
                        //}
                        //else
                        //{
                        //    sMergeFromInsurance = sMergeFromInsurance + "- " + Convert.ToString(c1InsuranceList.GetData(i, COL_PlanName)) + "\n";
                        //}
                    }
                }

                if (lstMergeToInsurance != null && lstMergeToInsurance.Count > 0)
                {
                    string sMessage = string.Format("Insurance Plan Merge is permanent.\n Insurance plan(s): \n{0} \nwill be merged into \"{1}\".\nAll patients with old insurance plan(s) will be found under {1} insurance plan. Continue? \n\nNOTE: \"Merge From\" insurance plan(s) \"Hold/Un-hold\" status will be based upon \"Merge To\" hold status.", sMergeFromInsurance, sMergeToInsurance);

                    if (MessageBox.Show(sMessage, _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        string sErrorMsg = string.Empty;
                        bool bIsErrorOccour = false;
                        c1InsuranceList.Refresh();
                        Application.DoEvents();

                        foreach (var item in lstMergeToInsurance)
                        {
                            string[] sFromInsurace = item.Split(stringSeparators, StringSplitOptions.None);
                            clsMergeContacts = new MergeContacts(sConnectionString);
                            string sAuditMessage = string.Format("Insurance Plan {0}({1}) merging with {2}({3})", sFromInsurace[1], sFromInsurace[0], sMergeToInsurance, Convert.ToString(nMergeToInsuranceID));
                            if (clsMergeContacts.MergeInsurancePlan(Convert.ToInt64(sFromInsurace[0]), nMergeToInsuranceID, sMergeToInsurance, sFromInsurace[1], ref sErrorMsg))
                            {
                                c1InsuranceList.SetData(Convert.ToInt32(sFromInsurace[2]), COL_Code, "Success");
                                WriteLog(sAuditMessage + " is successed.");
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Insurance, gloAuditTrail.ActivityType.merge, sAuditMessage, gloAuditTrail.ActivityOutCome.Success);
                            }
                            else
                            {
                                bIsErrorOccour = true;
                                c1InsuranceList.SetData(Convert.ToInt32(sFromInsurace[2]), COL_Code, "Failed");
                                WriteLog(sAuditMessage + " is failed with error: " + sErrorMsg);
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Insurance, gloAuditTrail.ActivityType.merge, sAuditMessage + " is failed with error: " + sErrorMsg, gloAuditTrail.ActivityOutCome.Failure);
                            }
                            Application.DoEvents();
                        }

                        if (bIsErrorOccour)
                        {
                            MessageBox.Show("Insurance merging is done with error. Please check the log for more details.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Insurance, gloAuditTrail.ActivityType.merge, sErrorMsg, gloAuditTrail.ActivityOutCome.Failure);
                        }
                        else
                        {
                            MessageBox.Show("Insurance merging is done successfully.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.Insurance, gloAuditTrail.ActivityType.merge, "Insurance merging is done successfully.", gloAuditTrail.ActivityOutCome.Success);
                        }

                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        tsbtn_Merge.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                tsbtn_Merge.Enabled = true;
                //WriteExceptionLog(ex);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (clsMergeContacts != null)
                {
                    clsMergeContacts.Dispose();
                    clsMergeContacts = null;
                }
                if (lstMergeToInsurance != null)
                {
                    lstMergeToInsurance = null;
                }
            }
        }
        
        List<string> lstVisibleColumns = new List<string>();
        private void chkLstGridColumn_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                lstVisibleColumns.Add(chkLstGridColumn.SelectedItem.ToString());
            }
            else
            {
                lstVisibleColumns.Remove(chkLstGridColumn.SelectedItem.ToString());
            }
        }

        private void btnShowColumns_Click(object sender, EventArgs e)
        {
            ShowHideGridCloumns();
        }

        private void ShowHideGridCloumns()
        {
            SetColumnVisibility();
            if (lstVisibleColumns != null && lstVisibleColumns.Count > 0)
            {
                string sCoulms = string.Empty;
                foreach (var item in lstVisibleColumns)
                {
                    c1InsuranceList.Cols[c1InsuranceList.Cols.IndexOf(item)].Visible = true;
                }
            }
        }

        private void c1InsuranceList_CellChecked(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {

            //Console.WriteLine(String.Format("START: c1InsuranceList_AfterEdit - {0}", DateTime.Now.ToString("hh:mm:ss")));
            #region "Another way to do this"
            //try
            //{
            //    // c1InsuranceList.CellChecked -= new C1.Win.C1FlexGrid.RowColEventHandler(c1InsuranceList_CellChecked);
            //    this.Cursor = Cursors.WaitCursor;
            //    c1InsuranceList.BeginUpdate();



            //    if (c1InsuranceList.Rows.Count > 0)
            //    {
            //        if (e.Col == COL_MergeTo)
            //        {
            //            C1.Win.C1FlexGrid.CellRange range = c1InsuranceList.GetCellRange(1, 1, c1InsuranceList.Rows.Count - 1, 1);
            //            //range.Data = false;
            //            range.Checkbox = C1.Win.C1FlexGrid.CheckEnum.Unchecked;
            //            //range.Clear(C1.Win.C1FlexGrid.ClearFlags.Content);
            //            //range.Style = c1InsuranceList.Styles["CheckBoxStyle"];
            //            //for (int i = 1; i <= c1InsuranceList.Rows.Count - 1; i++)
            //            //{
            //            //    if (c1InsuranceList.GetCellCheck(i, COL_MergeTo) == C1.Win.C1FlexGrid.CheckEnum.Checked && c1InsuranceList.RowSel == i)
            //            //    {
            //            //        c1InsuranceList.SetCellCheck(i, COL_MergeFrom, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
            //            //        c1InsuranceList.Rows[i].AllowEditing = false;
            //            //    }
            //            //    else
            //            //    {
            //            //        c1InsuranceList.Rows[i].AllowEditing = true;
            //            //        c1InsuranceList.SetCellCheck(i, COL_MergeTo, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
            //            //    }
            //            //}
            //            c1InsuranceList.SetCellCheck(e.Row, COL_MergeTo, C1.Win.C1FlexGrid.CheckEnum.Checked);
            //            c1InsuranceList.SetCellCheck(e.Row, COL_MergeFrom, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
            //        }
            //        if (e.Col == COL_MergeFrom)
            //        {
            //            if (c1InsuranceList.GetCellCheck(e.Row, COL_MergeTo) == C1.Win.C1FlexGrid.CheckEnum.Checked)
            //            {
            //                c1InsuranceList.SetCellCheck(e.Row, COL_MergeFrom, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}
            //finally
            //{
            //    // c1InsuranceList.CellChecked += new C1.Win.C1FlexGrid.RowColEventHandler(c1InsuranceList_CellChecked);
            //    //c1InsuranceList.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(c1InsuranceList_AfterEdit);

            //    c1InsuranceList.EndUpdate();
            //    this.Cursor = Cursors.Default;
            //    Console.WriteLine(String.Format("Finish: c1InsuranceList_AfterEdit - {0}", DateTime.Now.ToString("hh:mm:ss")));
            //} 
            #endregion
            try
            {
                c1InsuranceList.BeginUpdate();

                //c1InsuranceList.AfterEdit -= new C1.Win.C1FlexGrid.RowColEventHandler(c1InsuranceList_AfterEdit);

                if (c1InsuranceList.Rows.Count > 0)
                {
                    if (e.Col == COL_MergeTo)
                    {
                        for (int i = 1; i <= c1InsuranceList.Rows.Count - 1; i++)
                        {
                            if (c1InsuranceList.GetCellCheck(i, COL_MergeTo) == C1.Win.C1FlexGrid.CheckEnum.Checked && c1InsuranceList.RowSel == i)
                            {
                                c1InsuranceList.SetCellCheck(i, COL_MergeFrom, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                c1InsuranceList.Rows[i].AllowEditing = false;
                            }
                            else
                            {
                                c1InsuranceList.Rows[i].AllowEditing = true;
                                c1InsuranceList.SetCellCheck(i, COL_MergeTo, C1.Win.C1FlexGrid.CheckEnum.Unchecked);

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
                //c1InsuranceList.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(c1InsuranceList_AfterEdit);
                c1InsuranceList.EndUpdate();
                //Console.WriteLine(String.Format("Finish: c1InsuranceList_AfterEdit - {0}", DateTime.Now.ToString("hh:mm:ss")));
            }
        }

        private void ts_btnRefresh_Click(object sender, EventArgs e)
        {
            FillInsuranceGrid();
            ShowHideGridCloumns();
            txtSearch.Clear();
            tsbtn_Merge.Enabled = true;
        }

        static string sBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
        public void WriteExceptionLog(Exception ex)
        {
            StreamWriter sw = null;

            string sDirectoryPath = Path.Combine(sBasePath, "Exception Log");
            if (!Directory.Exists(sDirectoryPath))
            {
                Directory.CreateDirectory(sDirectoryPath);
            }

            try
            {
                sw = new StreamWriter(Path.Combine(sDirectoryPath, "ExceptionLogFile.txt"), true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public void WriteLog(string message)
        {
            string sDirectoryPath = Path.Combine(sBasePath, "MergeLogs");
            if (!Directory.Exists(sDirectoryPath))
            {
                Directory.CreateDirectory(sDirectoryPath);
            }
            string sLogFileName = Path.Combine(sDirectoryPath, "MergingLogFile.txt");
            if (IsFileSizeMorethan(2, sLogFileName))
            {
                string sFilename = "BackupMergingLogFile_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
                System.IO.File.Move(sLogFileName, Path.Combine(sDirectoryPath, sFilename));
                File.AppendAllText(sLogFileName, "------------------------------------------------------------------------------------------------------------------------------------------------------");
                File.AppendAllText(sLogFileName, Environment.NewLine + "As log file size exceeded 2 MB, New file is Logfile is renamed." + System.Environment.NewLine + " old file present with name " + sFilename + " for reference please verify." + Environment.NewLine);
                File.AppendAllText(sLogFileName, "------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(sLogFileName, true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        private bool IsFileSizeMorethan(double fileSizeinMB, string filefullpath)
        {
            bool Sizemore = false;
            try
            {
                if (File.Exists(filefullpath))
                {
                    FileInfo oFileInfo = new FileInfo(filefullpath);
                    if (oFileInfo.Length > (1048576 * fileSizeinMB))
                    {
                        Sizemore = true;
                    }
                    oFileInfo = null;
                }
            }
            catch (Exception ex) { }
            return Sizemore;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
        }

    }
}
