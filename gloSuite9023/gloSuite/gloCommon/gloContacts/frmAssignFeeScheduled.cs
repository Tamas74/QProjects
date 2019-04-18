using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommon;

namespace gloContacts
{
    public partial class frmAssignFeeScheduled : Form
    {

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _messgeBoxCaption = String.Empty;
        public frmAssignFeeScheduled()
        {
            InitializeComponent();
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messgeBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messgeBoxCaption = "gloPM";
                }
            }
            else
            { _messgeBoxCaption = "gloPM"; }

            #endregion
        }
        private Int64 feescheduledid = 0;
        public Int64 FeescheduledID
        {
            get { return feescheduledid; }
        }

        private Int64 insurancecompanyid = 0;
        public Int64 InsurancecompanyID
        {
            get { return insurancecompanyid; }
            
        }

        private Int64 settingid = 0;
        public Int64 SettingID
        {
            get { return settingid; }

        }

        public frmAssignFeeScheduled(Int64 nFeeScheduledID, Int64 nInsuranceCompanyID, Int64 nSettingID)
        {
            InitializeComponent();

            feescheduledid = nFeeScheduledID;
            insurancecompanyid = nInsuranceCompanyID;
            settingid = nSettingID;
            //if (this.FeescheduledID > 0 && this.InsurancecompanyID > 0)
            //{ }
            //else
            //{ throw new Exception("Invalid parameters. Either FeeScheduleID or InsuranceCompanyID is zero"); }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messgeBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messgeBoxCaption = "gloPM";
                }
            }
            else
            { _messgeBoxCaption = "gloPM"; }

            #endregion

        }

        DataTable dtFeeScheduled;
        DataTable dtInsurancePlan;

        private const int COL_InsPlan_CompanyID = 0;
        private const int COL_InsPlan_ContactID = 1;
        private const int COL_InsPlan_FeeScheduledID = 2;
        private const int COL_InsPlan_Select = 3;
        private const int COL_InsPlan_Name = 4; 
        private const int COL_InsPlan_Company = 5;
        private const int COL_InsPlan_PayerID = 6;
        private const int COL_InsPlan_FeeScheduled = 7;
       


        private void frmAssignFeeScheduled_Load(object sender, EventArgs e)
        {
            try
            {
                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                tom.SetTabOrder(scheme);
                gloC1FlexStyle.Style(c1InsurancePlan, true);
                gloC1FlexStyle.Style(c1FeeScheduledInsPlan, true);
                FillSettingType();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in form load: "+ex.ToString(), true);
            }
        }

        private void FillSettingType()
        {

            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameter = null;
            DataTable dtSettingType = new DataTable();

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oDBParameter = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);

                oDBParameter.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("gsp_GetBillingMasterSettings", oDBParameter, out dtSettingType);

                oDB.Disconnect();

                cmbSettingType.SelectedIndexChanged -= new EventHandler(cmbSettingType_SelectedIndexChanged);
                if (dtSettingType != null && dtSettingType.Rows.Count > 0)
                {
                    DataRow dr = dtSettingType.NewRow();
                    dr["nSettingID"] = "0";
                    dr["sSettingDisplayName"] = "";

                    dtSettingType.Rows.InsertAt(dr, 0);
                    cmbSettingType.DataSource = dtSettingType.DefaultView;
                    cmbSettingType.DisplayMember = "sSettingDisplayName";
                    cmbSettingType.ValueMember = "nSettingID";

                    cmbSettingType.SelectedIndexChanged += new EventHandler(cmbSettingType_SelectedIndexChanged);
                    if (SettingID != 0)
                    {
                        cmbSettingType.SelectedValue = SettingID;
                    }
                    
                    if (Convert.ToInt64(cmbSettingType.SelectedValue)==0)
                    {
                        pnlFeeScheduledComboMain.Visible = false;
                    }
                    else
                    {
                        lblcmbFeeScheduled.Text = cmbSettingType.Text + " :";
                        pnlFeeScheduledComboMain.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                oDB.Disconnect();
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in FillSettingType(): " + ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBParameter != null)
                {
                    oDBParameter.Dispose();
                    oDBParameter = null;
                }
                if (dtSettingType != null)
                {
                    dtSettingType.Dispose();
                    dtSettingType = null;
                }
            }
        }

        private void LoadFormData(Int64 _ScheduledID = 0, Int64 _SettingType = 0, bool bIsRefreshedCalled=false)
        {
            DataSet ds = new DataSet();

            try
            {
                ds = GetFormData(_SettingType);
                if (ds != null && ds.Tables.Count > 0)
                {

                    dtFeeScheduled = ds.Tables[0];
                    dtInsurancePlan = ds.Tables[1];

                    cmbFeeScheduled.SelectedIndexChanged -= new EventHandler(cmbFeeScheduled_SelectedIndexChanged);
                    if (dtFeeScheduled != null && dtFeeScheduled.Rows.Count > 0)
                    {
                        DataRow dr = dtFeeScheduled.NewRow();
                        dr["nID"] = "0";
                        dr["sName"] = "";

                        dtFeeScheduled.Rows.InsertAt(dr, 0);
                        cmbFeeScheduled.DataSource = dtFeeScheduled;
                        cmbFeeScheduled.DisplayMember = "sName";
                        cmbFeeScheduled.ValueMember = "nID";


                        cmbFeeScheduled.SelectedValue = _ScheduledID;
                    }
                    cmbFeeScheduled.SelectedIndexChanged += new EventHandler(cmbFeeScheduled_SelectedIndexChanged);
                    if (cmbFeeScheduled.SelectedIndex == 0)
                    {
                        lblInsPlanFeeScheduled.Text = "Insurance plan associated";
                    }
                    else
                    {
                        lblInsPlanFeeScheduled.Text = "Insurance plan associated to \"" + cmbFeeScheduled.Text.Trim() + "\"";
                    }
                    if (dtInsurancePlan != null && dtInsurancePlan.Rows.Count > 0)
                    {

                        if (_ScheduledID == 0)
                        {
                            DesignGrid(dtInsurancePlan);
                            DataTable dtFeeSch = dtInsurancePlan.Clone();
                            DesignFeeScheduledGrid(dtFeeSch);
                        }
                        else
                        {
                            //getFeeScheduled_InsPlanAssigned(_ScheduledID);

                            GetFeeScheduled_InsPlanAssigned(_ScheduledID,bIsRefreshedCalled);
                        }
                    }
                }
                else
                {
                    lblInsPlanFeeScheduled.Text = "Insurance plan associated";
                    dtFeeScheduled = new DataTable();
                    dtInsurancePlan = new DataTable();
                    c1InsurancePlan.DataSource = dtInsurancePlan.DefaultView;
                    c1FeeScheduledInsPlan.DataSource = dtInsurancePlan.DefaultView;
                    cmbFeeScheduled.DataSource = dtFeeScheduled.DefaultView;
                }
                if (FeescheduledID>0)
                {
                    cmbSettingType.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in LoadFormData(): " + ex.ToString(), true);
            }
            finally
            {
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }
        }
        
        private void DesignGrid(DataTable dtInsurancePlan)
        {
            try
            {
                c1InsurancePlan.DataSource = null;

                c1InsurancePlan.Clear();

                c1InsurancePlan.DataSource = dtInsurancePlan.DefaultView;
                c1InsurancePlan.Rows.Fixed = 1;

                c1InsurancePlan.Cols[COL_InsPlan_Select].DataType = typeof(bool);
                c1InsurancePlan.SetCellCheck(0, COL_InsPlan_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked);


                c1InsurancePlan.Cols[COL_InsPlan_Select].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsurancePlan.Cols[COL_InsPlan_Name].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsurancePlan.Cols[COL_InsPlan_Company].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsurancePlan.Cols[COL_InsPlan_PayerID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1InsurancePlan.Cols[COL_InsPlan_FeeScheduled].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1InsurancePlan.Cols[COL_InsPlan_ContactID].Visible = false;
                c1InsurancePlan.Cols[COL_InsPlan_CompanyID].Visible = false;
                c1InsurancePlan.Cols[COL_InsPlan_FeeScheduledID].Visible = false;

                c1InsurancePlan.Cols[COL_InsPlan_Select].Width = Width / 14;
                c1InsurancePlan.Cols[COL_InsPlan_Company].Width = Width / 11;
                c1InsurancePlan.Cols[COL_InsPlan_Name].Width = Width / 7;
                c1InsurancePlan.Cols[COL_InsPlan_PayerID].Width = Width / 14;
                c1InsurancePlan.Cols[COL_InsPlan_FeeScheduled].Width = Width / 11;

                c1InsurancePlan.Cols[COL_InsPlan_Company].AllowEditing = false;
                c1InsurancePlan.Cols[COL_InsPlan_Name].AllowEditing = false;
                c1InsurancePlan.Cols[COL_InsPlan_PayerID].AllowEditing = false;
                c1InsurancePlan.Cols[COL_InsPlan_FeeScheduled].AllowEditing = false;

                c1InsurancePlan.Cols[COL_InsPlan_Select].AllowSorting = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in DesignGrid(): " + ex.ToString(), true);
            }
        }

        private void DesignFeeScheduledGrid(DataTable dt)
        {
            try
            {
                c1FeeScheduledInsPlan.DataSource = null;

                c1FeeScheduledInsPlan.Clear();
                if (dt != null)
                    c1FeeScheduledInsPlan.DataSource = dt.DefaultView;

                c1FeeScheduledInsPlan.Rows.Fixed = 1;

                c1FeeScheduledInsPlan.Cols[COL_InsPlan_Select].DataType = typeof(bool);
                c1FeeScheduledInsPlan.SetCellCheck(0, COL_InsPlan_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked);

                c1FeeScheduledInsPlan.Cols[COL_InsPlan_Select].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FeeScheduledInsPlan.Cols[COL_InsPlan_Name].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FeeScheduledInsPlan.Cols[COL_InsPlan_Company].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FeeScheduledInsPlan.Cols[COL_InsPlan_PayerID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1FeeScheduledInsPlan.Cols[COL_InsPlan_FeeScheduled].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1FeeScheduledInsPlan.Cols[COL_InsPlan_ContactID].Visible = false;
                c1FeeScheduledInsPlan.Cols[COL_InsPlan_CompanyID].Visible = false;
                c1FeeScheduledInsPlan.Cols[COL_InsPlan_FeeScheduledID].Visible = false;
                c1FeeScheduledInsPlan.Cols[COL_InsPlan_FeeScheduled].Visible = false;

                c1FeeScheduledInsPlan.Cols[COL_InsPlan_Select].Width = Width / 14;
                c1FeeScheduledInsPlan.Cols[COL_InsPlan_Company].Width = Width / 10;
                c1FeeScheduledInsPlan.Cols[COL_InsPlan_Name].Width = Width / 5;
                c1FeeScheduledInsPlan.Cols[COL_InsPlan_PayerID].Width = Width / 11;

                c1FeeScheduledInsPlan.Cols[COL_InsPlan_Company].AllowEditing = false;
                c1FeeScheduledInsPlan.Cols[COL_InsPlan_Name].AllowEditing = false;
                c1FeeScheduledInsPlan.Cols[COL_InsPlan_PayerID].AllowEditing = false;

                c1FeeScheduledInsPlan.Cols[COL_InsPlan_Select].AllowSorting = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in DesignFeeScheduledGrid(): " + ex.ToString(), true);
            }
        }
        
        private DataSet GetFormData(Int64 nSettingType)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameter = null;
            DataSet dsData=new DataSet();
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oDBParameter = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);

                oDBParameter.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameter.Add("@nSettingTypeID", nSettingType, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("gsp_GetInsurancePlansWithFeesScheduled", oDBParameter, out dsData);

                oDB.Disconnect();
                
            }
            catch (Exception ex)
            {
                oDB.Disconnect();
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in GetFormData(): " + ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBParameter != null)
                {
                    oDBParameter.Dispose();
                    oDBParameter = null;
                }
            }
            return dsData;
        }

        private void cmbFeeScheduled_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadFormData(Convert.ToInt64(cmbFeeScheduled.SelectedValue), settingType,true);
                if (cmbFeeScheduled.SelectedIndex == 0)
                {
                    lblInsPlanFeeScheduled.Text = "Insurance plan associated";
                }
                else
                {
                    lblInsPlanFeeScheduled.Text = "Insurance plan associated to \"" + cmbFeeScheduled.Text.Trim() + "\"";
                }
                txtSearch.Clear();
                txtAssociatedSearch.Clear();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in cmbFeeScheduled IndexChanged(): " + ex.ToString(), true);
            }
        }

        //private void getFeeScheduled_InsPlanAssignedByFeeSchedule(Int64 InsurancePlanID)
        //{
        //    DataTable dtFeeSch = null;
        //    DataTable dtInsPlan = null;
        //    try
        //    {
        //        if (dtInsurancePlan != null && dtInsurancePlan.Rows.Count > 0)
        //        {
        //            dtFeeSch = dtInsurancePlan.Clone();
        //            dtInsPlan = dtInsurancePlan.Copy();
        //            DataRow[] dFeeScheRows = null;
                    
        //                dFeeScheRows = dtInsPlan.Select("FeeScheduledID=" + InsurancePlanID);
        //            dtFeeSch.Rows.Clear();

        //            foreach (DataRow dr in dFeeScheRows)
        //            {
        //                dtFeeSch.ImportRow(dr);
        //            }
        //            foreach (DataRow dr in dtFeeSch.Rows)
        //            {
        //                if (Convert.ToBoolean(dr[COL_InsPlan_Select]) == true)
        //                {
        //                    dr[COL_InsPlan_Select] = false;
        //                }
        //            }
        //            DesignFeeScheduledGrid(dtFeeSch);
        //            for (int i = 0; i < dtInsPlan.Rows.Count; i++)
        //            {
        //                    if (Convert.ToInt64(dtInsPlan.Rows[i][COL_InsPlan_FeeScheduledID]) == InsurancePlanID)
        //                    {
        //                        dtInsPlan.Rows[i].Delete();
        //                    }
        //            }
        //            dtInsPlan.AcceptChanges();
        //            foreach (DataRow dr in dtInsPlan.Rows)
        //            {
        //                if (Convert.ToBoolean(dr[COL_InsPlan_Select]) == true)
        //                {
        //                    dr[COL_InsPlan_Select] = false;
        //                }
        //            }
        //            DesignGrid(dtInsPlan);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in getFeeScheduled_InsPlanAssigned(): " + ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (dtInsPlan!=null)
        //        {
        //            dtInsPlan.Dispose();
        //            dtInsPlan = null;
        //        }
        //        if (dtFeeSch!=null)
        //        {
        //            dtFeeSch.Dispose();
        //            dtFeeSch = null;
        //        }
        //    }
        //}

        private void GetFeeScheduled_InsPlanAssigned(Int64 InsurancePlanID,Boolean IsRefreshedCalled=false)
        {
            DataTable dtFeeSch = null;
            DataTable dtInsPlan = null;
            try
            {
                if (dtInsurancePlan != null && dtInsurancePlan.Rows.Count > 0)
                {
                    dtFeeSch = dtInsurancePlan.Clone();
                    dtInsPlan = dtInsurancePlan.Copy();
                    DataRow[] dFeeScheRows = null;

                    if (FeescheduledID != 0 && IsRefreshedCalled==false)
                    {
                        dFeeScheRows = dtInsPlan.Select("CompanyId=" + InsurancecompanyID + "Or FeeScheduledID=" + FeescheduledID);
                    }
                    else
                    {
                        dFeeScheRows = dtInsPlan.Select("FeeScheduledID=" + InsurancePlanID);
                    }
                    
                    dtFeeSch.Rows.Clear();

                    foreach (DataRow dr in dFeeScheRows)
                    {
                        dtFeeSch.ImportRow(dr);
                    }
                    foreach (DataRow dr in dtFeeSch.Rows)
                    {
                        if (Convert.ToBoolean(dr[COL_InsPlan_Select]) == true)
                        {
                            dr[COL_InsPlan_Select] = false;
                        }
                    }
                    DesignFeeScheduledGrid(dtFeeSch);
                    if (FeescheduledID != 0 && IsRefreshedCalled == false)
                    {
                        for (int i = 0; i < dtInsPlan.Rows.Count; i++)
                        {
                            if (Convert.ToInt64(dtInsPlan.Rows[i][COL_InsPlan_CompanyID]) == InsurancecompanyID || Convert.ToInt64(dtInsPlan.Rows[i][COL_InsPlan_FeeScheduledID]) == FeescheduledID)
                            {
                                dtInsPlan.Rows[i].Delete();
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dtInsPlan.Rows.Count; i++)
                        {
                            if (Convert.ToInt64(dtInsPlan.Rows[i][COL_InsPlan_FeeScheduledID]) == InsurancePlanID)
                            {
                                dtInsPlan.Rows[i].Delete();
                            }
                        }
                    }
                    dtInsPlan.AcceptChanges();
                    foreach (DataRow dr in dtInsPlan.Rows)
                    {
                        if (Convert.ToBoolean(dr[COL_InsPlan_Select]) == true)
                        {
                            dr[COL_InsPlan_Select] = false;
                        }
                    }
                    DesignGrid(dtInsPlan);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in getFeeScheduled_InsPlanAssigned(): " + ex.ToString(), true);
            }
            finally
            {
                if (dtInsPlan != null)
                {
                    dtInsPlan.Dispose();
                    dtInsPlan = null;
                }
                if (dtFeeSch != null)
                {
                    dtFeeSch.Dispose();
                    dtFeeSch = null;
                }
            }
        }

        private void tls_btnSave_Click(object sender, EventArgs e)
        {
            DataTable _dtFeeScheduled = null;
            DataTable dtCopyFeeSche = null;
            try
            {
                if (ValidateData())
                {
                    if (c1FeeScheduledInsPlan.Rows.Count == 1 && Convert.ToInt64(cmbFeeScheduled.SelectedValue) != 0)
                    {
                        string sValidationMessage = string.Format("\"{0}\" {1} is removed from existing associated insurance plan(s).\n Do you want to continue?", Convert.ToString(cmbFeeScheduled.Text), Convert.ToString(cmbSettingType.Text).ToLower());
                        if (DialogResult.No == MessageBox.Show(sValidationMessage, _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            LoadFormData(Convert.ToInt64(cmbFeeScheduled.SelectedValue), settingType, true);
                            return;
                        }
                    }
                    else if (c1FeeScheduledInsPlan.Rows.Count > 1 && Convert.ToInt64(cmbFeeScheduled.SelectedValue) != 0)
                    {
                        string sValidationMessage = string.Format("\"{0}\" {1} will be associate with associated insurance plan(s).\n Do you want to continue?", Convert.ToString(cmbFeeScheduled.Text), Convert.ToString(cmbSettingType.Text).ToLower());
                        if (DialogResult.No == MessageBox.Show(sValidationMessage, _messgeBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            LoadFormData(Convert.ToInt64(cmbFeeScheduled.SelectedValue), settingType, true);
                            return;
                        }

                    }
                    _dtFeeScheduled = ((DataView)c1FeeScheduledInsPlan.DataSource).Table;
                    dtCopyFeeSche = _dtFeeScheduled.Copy();
                    if (dtCopyFeeSche != null)
                    {
                        dtCopyFeeSche.Columns.RemoveAt(COL_InsPlan_FeeScheduled);
                        dtCopyFeeSche.Columns.RemoveAt(COL_InsPlan_Select);
                    }

                    Int64 nFeeScheduledID = Convert.ToInt64(cmbFeeScheduled.SelectedValue);
                    AssociateFeeScheduled(dtCopyFeeSche, nFeeScheduledID, settingType);
                    LoadFormData(0, settingType,true);
                    //string sMessage = string.Format("\"{0}\" {1} added for selected insurance plan.", Convert.ToString(cmbFeeScheduled.Text), Convert.ToString(cmbSettingType.Text).ToLower());
                    //MessageBox.Show(sMessage, _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in tls_btnSave_Click(): " + ex.ToString(), true);
            }
            finally
            {
                if (_dtFeeScheduled!=null)
                {
                    _dtFeeScheduled.Dispose();
                    _dtFeeScheduled = null;
                }
                if (dtCopyFeeSche!=null)
                {
                    dtCopyFeeSche.Dispose();
                    dtCopyFeeSche = null;
                }
            }
        }

        private void AssociateFeeScheduled(DataTable _dtInsPlanFeeScheduled, long nFeeScheduledID, long nSettingType)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameter = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oDBParameter = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);

                oDBParameter.Add("@TVPFeeScheduled", _dtInsPlanFeeScheduled, ParameterDirection.Input, SqlDbType.Structured);
                oDBParameter.Add("@nFeeScheduledID", nFeeScheduledID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameter.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameter.Add("@nSettingTypeID", nSettingType, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("gsp_INUP_FeeScheduledInsPlanAssociation", oDBParameter);

                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                oDB.Disconnect();
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in AssociateFeeScheduled(): " + ex.ToString(), true);
            }
            finally
            {
                if (oDB!=null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBParameter!=null)
                {
                    oDBParameter.Dispose();
                    oDBParameter = null;
                }
            }
        }

        private bool ValidateData()
        {
            if (Convert.ToInt64(cmbSettingType.SelectedValue) == 0)
            {
                MessageBox.Show("Select billing setting.", _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            } 
            else if (Convert.ToInt64(cmbFeeScheduled.SelectedValue) == 0)
            {
                string sMessage = string.Format("Select {0} to associate insurance plan.", Convert.ToString(cmbSettingType.Text).ToLower());
                MessageBox.Show(sMessage, _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
             

            return true;
        }

        private void tls_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddInFeeScheduled_Click(object sender, EventArgs e)
        {
            DataTable dtNewRow = null;
            string sInsPlanToRemove = string.Empty;
            try
            {
                if (c1InsurancePlan.Rows.Count > 1)
                {
                    int nSelectedCount = 0;
                    nSelectedCount = c1InsurancePlan.FindRow(true, 1, COL_InsPlan_Select, true);
                    if (nSelectedCount > 0)
                    {
                        if (c1InsurancePlan.Rows.Count > 1)
                        {
                            dtNewRow = dtInsurancePlan.Clone();
                            dtNewRow.Clear();
                            for (int i = c1InsurancePlan.Rows.Fixed; i < c1InsurancePlan.Rows.Count; i++)
                            {
                                if (c1InsurancePlan.GetCellCheck(i, COL_InsPlan_Select) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                {
                                    //c1FeeScheduledInsPlan.Rows.Insert(c1InsurancePlan.Row);

                                    DataRowView dr;
                                    dr = (DataRowView)c1InsurancePlan.Rows[i].DataSource;
                                    sInsPlanToRemove += "," + Convert.ToString(dr[COL_InsPlan_ContactID]);
                                    dr[COL_InsPlan_Select] = 0;
                                    dtNewRow.ImportRow(dr.Row);
                                }
                            }
                            AddInsurancePlanToFeeSchedule(dtNewRow, sInsPlanToRemove.Remove(0, 1));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select at least one insurance plan.", _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No insurance plan is available to associate.", _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in btnAddInFeeScheduled_Click(): " + ex.ToString(), true);
            }
            finally
            {
                if (dtNewRow!=null)
                {
                    dtNewRow.Dispose();
                    dtNewRow = null;
                }
            }
        }

        private void btnRemoveFromFeeScheduled_Click(object sender, EventArgs e)
        {
            DataTable dtNewRow = null;
            string sInsPlanToRemove = string.Empty;
            try
            {
                if (c1FeeScheduledInsPlan.Rows.Count > 1)
                {
                    int nSelectedCount = 0;
                    nSelectedCount = c1FeeScheduledInsPlan.FindRow(true, 1, COL_InsPlan_Select, true);
                    if (nSelectedCount > 0)
                    {
                        if (c1FeeScheduledInsPlan.Rows.Count > 1)
                        {
                            dtNewRow = dtInsurancePlan.Clone();
                            dtNewRow.Clear();
                            for (int i = c1FeeScheduledInsPlan.Rows.Fixed; i < c1FeeScheduledInsPlan.Rows.Count; i++)
                            {
                                if (c1FeeScheduledInsPlan.GetCellCheck(i, COL_InsPlan_Select) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                {
                                    //c1FeeScheduledInsPlan.Rows.Insert(c1InsurancePlan.Row);

                                    DataRowView dr;
                                    dr = (DataRowView)c1FeeScheduledInsPlan.Rows[i].DataSource;
                                    sInsPlanToRemove += "," + Convert.ToString(dr[COL_InsPlan_ContactID]);
                                    dr[COL_InsPlan_Select] = 0;
                                    dtNewRow.ImportRow(dr.Row);
                                }
                            }
                            RemoveInsurancePlanFromFeeSchedule(dtNewRow, sInsPlanToRemove.Remove(0, 1));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select at least one insurnace plan.", _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No insurance plan is available to remove.", _messgeBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in btnRemoveFromFeeScheduled_Click(): " + ex.ToString(), true);
            }
            finally
            {
                if (dtNewRow != null)
                {
                    dtNewRow.Dispose();
                    dtNewRow = null;
                }
            }
        }

        private void AddInsurancePlanToFeeSchedule(DataTable dtNewRow, string sInsPlanRemove)
        {
            try
            {
                if (c1FeeScheduledInsPlan.DataSource != null)
                {
                    DataView dt = (DataView)c1FeeScheduledInsPlan.DataSource;
                    DataTable dtData = dt.Table;
                    dtData.PrimaryKey = new[] { dtData.Columns["ContactID"] };
                    dtNewRow.PrimaryKey = new[] { dtNewRow.Columns["ContactID"] };
                    foreach (DataRow dr in dtNewRow.Rows)
                    {
                        if (Convert.ToBoolean(dr[COL_InsPlan_Select])==true)
                        {
                            dr[COL_InsPlan_Select] = false;
                        }
                    }

                    dtData.Merge(dtNewRow);
                    RemoveRows(sInsPlanRemove, c1InsurancePlan);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in AddInsurancePlanToFeeScheduled(): " + ex.ToString(), true);
            }
        }
        
        private void RemoveInsurancePlanFromFeeSchedule(DataTable dtNewRow, string sInsPlanRemove)
        {
            try
            {
                if (c1InsurancePlan.DataSource!=null)
                {
                    DataTable dtData = ((DataView)c1InsurancePlan.DataSource).Table;
                    dtData.PrimaryKey = new[] { dtData.Columns["ContactID"] };
                    dtNewRow.PrimaryKey = new[] { dtNewRow.Columns["ContactID"] };
                    foreach (DataRow dr in dtNewRow.Rows)
                    {
                        if (Convert.ToBoolean(dr[COL_InsPlan_Select]) == true)
                        {
                            dr[COL_InsPlan_Select] = false;
                        }
                    }

                    dtData.Merge(dtNewRow);

                    RemoveRows(sInsPlanRemove, c1FeeScheduledInsPlan);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in RemoveInsurancePlanFromFeeScheduled(): " + ex.ToString(), true);
            }
        }

        private void RemoveRows(string sInsPlanRemove, C1.Win.C1FlexGrid.C1FlexGrid c1GridToRemoveCols)
        {
            try
            {
                DataTable dtData = ((DataView)c1GridToRemoveCols.DataSource).Table;
                string[] sContactIDs = sInsPlanRemove.Split(',');
                for (int i = 0; i < c1GridToRemoveCols.Rows.Count; i++)
                {
                    for (int j = 0; j < sContactIDs.Length; j++)
                    {
                        if (Convert.ToString(c1GridToRemoveCols.Rows[i][COL_InsPlan_ContactID]).Contains(sContactIDs[j]))
                        {
                            c1GridToRemoveCols.RemoveItem(i);
                        }
                    }
                }
                dtData.AcceptChanges();
                c1GridToRemoveCols.SetCellCheck(0, COL_InsPlan_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in RemoveRows(): " + ex.ToString(), true);
            }
        }

        private void tls_btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtAssociatedSearch.Clear();
            LoadFormData(Convert.ToInt64(cmbFeeScheduled.SelectedValue),settingType,true);
        }

        private void c1InsurancePlan_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                c1InsurancePlan.Redraw = false;
                if (e.Row == 0 && e.Col == COL_InsPlan_Select)
                {
                    c1InsurancePlan.FinishEditing();
                    c1InsurancePlan.Select(0, COL_InsPlan_Select);
                    for (int i = 1; i < c1InsurancePlan.Rows.Count; i++)
                    {
                        if (c1InsurancePlan.GetCellCheck(e.Row, COL_InsPlan_Select) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            c1InsurancePlan.SetCellCheck(i, COL_InsPlan_Select, C1.Win.C1FlexGrid.CheckEnum.Checked);
                        }
                        else
                        {
                            c1InsurancePlan.SetCellCheck(i, COL_InsPlan_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in c1InsurancePlan AfterEdit(): " + ex.ToString(), true);
            }
            finally
            {
                c1InsurancePlan.Redraw = true;
                Cursor.Current = Cursors.Default;
            }
            
        }

        private void c1FeeScheduledInsPlan_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                c1FeeScheduledInsPlan.Redraw = false;
                if (e.Row == 0 && e.Col == COL_InsPlan_Select)
                {
                    c1FeeScheduledInsPlan.FinishEditing();
                    c1FeeScheduledInsPlan.Select(0, COL_InsPlan_Select);

                    for (int i = 1; i < c1FeeScheduledInsPlan.Rows.Count; i++)
                    {
                        if (c1FeeScheduledInsPlan.GetCellCheck(e.Row, COL_InsPlan_Select) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            c1FeeScheduledInsPlan.SetCellCheck(i, COL_InsPlan_Select, C1.Win.C1FlexGrid.CheckEnum.Checked);
                        }
                        else
                        {
                            c1FeeScheduledInsPlan.SetCellCheck(i, COL_InsPlan_Select, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in c1FeeScheduledInsPlan AfterEdit(): " + ex.ToString(), true);
            }
            finally
            {
                c1FeeScheduledInsPlan.Redraw = true;
                Cursor.Current = Cursors.Default;
            }
        }
        Int64 settingType;
        private void cmbSettingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbFeeScheduled.SelectedIndex = 0;
            try
            {
                if (Convert.ToInt64(cmbSettingType.SelectedValue) == 0)
                {
                    pnlFeeScheduledComboMain.Visible = false;
                }
                else
                {
                    lblcmbFeeScheduled.Text = cmbSettingType.Text + " :";
                    pnlFeeScheduledComboMain.Visible = true;
                }
                settingType = Convert.ToInt64(cmbSettingType.SelectedValue);
                if (FeescheduledID > 0)
                {
                    LoadFormData(FeescheduledID, settingType);
                }
                else
                {
                    LoadFormData(0, settingType);

                }
                txtSearch.Clear();
                txtAssociatedSearch.Clear();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in cmbSettingType IndexChanged(): " + ex.ToString(), true);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string[] strSearchArray = null;
            string sFilter = "";
            DataView _dv = new DataView();
            _dv = (DataView)c1InsurancePlan.DataSource;
            c1InsurancePlan.DataSource = _dv;
            if (_dv == null) return;
            this.Cursor = Cursors.WaitCursor;
            string strSearch = txtSearch.Text.Trim();

            try
            {

                //COMMENTED TO AVOID THE ERROR ON THE SEARCH STRING %%&(%^%
                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]");

                if (strSearch.StartsWith("*") == true)
                { strSearch = strSearch.Replace("*", "%"); }

                //ADDED TO AVOID THE ERROR ON THE SEARCH STRING %%&(%^%
                // strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]");

                strSearch = strSearch.Replace("*", "[*]");

                if (strSearch.Length > 1)
                {
                    //string str = strSearch.Substring(1).Replace("%", "");
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
                        _dv.RowFilter = "["+_dv.Table.Columns[COL_InsPlan_Name].ColumnName + "] Like '" + strSearch + "%' OR " +
                                        "["+_dv.Table.Columns[COL_InsPlan_Company].ColumnName + "] Like '" + strSearch + "%' OR " +
                                        "["+_dv.Table.Columns[COL_InsPlan_PayerID].ColumnName + "] Like '" + strSearch + "%'";
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
                                    sFilter = " ( ["  + _dv.Table.Columns[COL_InsPlan_Name].ColumnName + "] Like '" + strSearch + "%' OR " +
                                        "[" + _dv.Table.Columns[COL_InsPlan_Company].ColumnName + "] Like '" + strSearch + "%' OR " +
                                        "[" + _dv.Table.Columns[COL_InsPlan_PayerID].ColumnName + "] Like '" + strSearch + "%')";
                                }
                                else
                                {
                                    if (sFilter != "")
                                        sFilter = sFilter + " AND ";

                                    sFilter = sFilter + " ( [" +_dv.Table.Columns[COL_InsPlan_Name].ColumnName + "] Like '" + strSearch + "%' OR " +
                                        "["+_dv.Table.Columns[COL_InsPlan_Company].ColumnName + "] Like '" + strSearch + "%' OR " +
                                        "["+_dv.Table.Columns[COL_InsPlan_PayerID].ColumnName + "] Like '" + strSearch + "%')";
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

                this.Cursor = Cursors.Default;

            }

            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in txtSearch_TextChanged(): " + Ex.ToString(), true);
            }
            finally
            {
                strSearchArray = null;
                sFilter = null;
                strSearch = null;
            }
        }

        private void txtClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtSearch.Focus();
        }

        private void btnAssociatedClear_Click(object sender, EventArgs e)
        {
            txtAssociatedSearch.Clear();
            txtAssociatedSearch.Focus();
        }

        private void txtAssociatedSearch_TextChanged(object sender, EventArgs e)
        {
            string[] strSearchArray = null;
            string sFilter = "";
            DataView _dv = new DataView();
            _dv = (DataView)c1FeeScheduledInsPlan.DataSource;
            c1FeeScheduledInsPlan.DataSource = _dv;
            if (_dv == null) return;
            this.Cursor = Cursors.WaitCursor;
            string strSearch = txtAssociatedSearch.Text.Trim();

            try
            {

                //COMMENTED TO AVOID THE ERROR ON THE SEARCH STRING %%&(%^%
                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]");

                if (strSearch.StartsWith("*") == true)
                { strSearch = strSearch.Replace("*", "%"); }

                //ADDED TO AVOID THE ERROR ON THE SEARCH STRING %%&(%^%
                // strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]");

                strSearch = strSearch.Replace("*", "[*]");

                if (strSearch.Length > 1)
                {
                    //string str = strSearch.Substring(1).Replace("%", "");
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
                        _dv.RowFilter = "[" + _dv.Table.Columns[COL_InsPlan_Name].ColumnName + "] Like '" + strSearch + "%' OR " +
                                        "[" + _dv.Table.Columns[COL_InsPlan_Company].ColumnName + "] Like '" + strSearch + "%' OR " +
                                        "[" + _dv.Table.Columns[COL_InsPlan_PayerID].ColumnName + "] Like '" + strSearch + "%'";
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
                                    sFilter = " ( [" + _dv.Table.Columns[COL_InsPlan_Name].ColumnName + "] Like '" + strSearch + "%' OR " +
                                        "[" + _dv.Table.Columns[COL_InsPlan_Company].ColumnName + "] Like '" + strSearch + "%' OR " +
                                        "[" + _dv.Table.Columns[COL_InsPlan_PayerID].ColumnName + "] Like '" + strSearch + "%')";
                                }
                                else
                                {
                                    if (sFilter != "")
                                        sFilter = sFilter + " AND ";

                                    sFilter = sFilter + " ( [" + _dv.Table.Columns[COL_InsPlan_Name].ColumnName + "] Like '" + strSearch + "%' OR " +
                                        "[" + _dv.Table.Columns[COL_InsPlan_Company].ColumnName + "] Like '" + strSearch + "%' OR " +
                                        "[" + _dv.Table.Columns[COL_InsPlan_PayerID].ColumnName + "] Like '" + strSearch + "%')";
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

                this.Cursor = Cursors.Default;

            }

            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception in txtSearch_TextChanged(): " + Ex.ToString(), true);
            }
            finally
            {
                strSearchArray = null;
                sFilter = null;
                strSearch = null;
            }
        }

        
    }
}
