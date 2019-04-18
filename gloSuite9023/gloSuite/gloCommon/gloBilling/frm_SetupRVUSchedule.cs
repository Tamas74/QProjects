using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using C1.Win.C1FlexGrid;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Linq;


namespace gloBilling
{
    public partial class frm_SetupRVUSchedule : Form
    {

        #region "Private Variables"

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "";
        private string _strFile = "";
        private string _strFileType = "";
        private Int32 _nFilePEType = 0;
        private Int64 _ClinicID = 0;
        string strEffectiveDate = string.Empty;
        private bool _isChargeFeeSchedule = false;
        // private bool _bIsValidated = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion "Private Variables"

        #region "Properties"

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Boolean IsChargeFeeSchedule
        {
            get { return _isChargeFeeSchedule; }
            set { _isChargeFeeSchedule = value; }
        }
        public string ImportFileName
        {
            get { return _strFile; }
            set { _strFile = value; }
        }

        public string ImportFileType
        {
            get { return _strFileType; }
            set { _strFileType = value; }
        }
        public Int32 ImportPEId
        {
            get { return _nFilePEType; }
            set { _nFilePEType = value; }
        }
        #endregion

        #region " Public & Private Methods "

        private void fillRVUSchedule(long _nRVUId)
        {
            CLsBL_RVUSchedule objRVUSchedule = new CLsBL_RVUSchedule();
            try
            {
                DataTable dt = new DataTable();
                DataTable dtDetail = new DataTable();
                dt = objRVUSchedule.fillRVUScheduleMst(_nRVUId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dtpEffectivedate.Text = Convert.ToString(dt.Rows[0]["dtEffectiveDate"]);
                    strEffectiveDate = Convert.ToString(dt.Rows[0]["dtEffectiveDate"]);
                    txt_RVUScheduleNote.Text = Convert.ToString(dt.Rows[0]["sStatementNote"]);
                    if (Convert.ToBoolean(dt.Rows[0]["bIsActive"]) == true)
                    {
                        rbActive.Checked = true;
                    }
                    else
                    {
                        rbInactive.Checked = true;
                    }
                    ImportPEId = Convert.ToInt32(dt.Rows[0]["nScheduleType"]);
                }
                dtDetail = objRVUSchedule.fillRVUScheduleDtl(_nRVUId);
                if (dtDetail != null && dtDetail.Rows.Count > 0)
                {
                    c1RVUSchedule.DataSource = dtDetail.DefaultView;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            finally
            {
                if (objRVUSchedule != null) { objRVUSchedule.Dispose(); }
            }

        }

        private bool SaveRVUSchedule(DataTable dtDetail)
        {
            CLsBL_RVUSchedule objRVUSchedule = new CLsBL_RVUSchedule();
            bool _result = false;
            try
            {
                string strOpFlag = "I";
                DataTable dtMst = new DataTable();
                dtMst.Columns.Add("nRVUID");
                dtMst.Columns.Add("dtEffectiveDate");
                dtMst.Columns.Add("nScheduleType");
                dtMst.Columns.Add("sStatementNote");
                dtMst.Columns.Add("bIsActive");
                dtMst.Rows.Add();
                dtMst.Rows[0]["nRVUID"] = _nRVUId;
                dtMst.Rows[0]["dtEffectiveDate"] = dtpEffectivedate.Text;
                dtMst.Rows[0]["nScheduleType"] = ImportPEId;
                dtMst.Rows[0]["sStatementNote"] = txt_RVUScheduleNote.Text;
                dtMst.Rows[0]["bIsActive"] = (rbActive.Checked == true ? true : (rbInactive.Checked == true ? false : false));
                dtMst.AcceptChanges();
                if (dtDetail != null && dtDetail.Rows.Count > 0)
                {
                    if (objRVUSchedule.SaveRVUScheduleTVP(dtMst, dtDetail, strOpFlag))
                    {
                        _result = true;
                    }
                }

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, true);
                return false;
            }
            finally
            {
                if (objRVUSchedule != null) { objRVUSchedule.Dispose(); }
            }
            return _result;
        }

        private void ResetFields()
        {
            txt_RVUScheduleNote.Text = "";
            _nRVUId = 0;
            ImportFileName = "";
            ImportFileType = "Standard";
            txtSearch.Text = "";
            txt_RVUScheduleNote.Text = "";
            dtpEffectivedate.Text = DateTime.Now.Date.ToShortDateString();
            ImportPEId = 0;
            rbActive.Checked = true;
            rbInactive.Checked = false;
            chkHideZeroRVU.Checked = false;
            tsb_ImportCSV.Enabled = true;
           // c1RVUSchedule.Clear();
            c1RVUSchedule.DataSource = null;
            c1RVUSchedule.Rows.Fixed = 1;
            c1RVUSchedule.SetData(0, COL_RVUID, "nRVUID");
            c1RVUSchedule.SetData(0, COL_RVUID, "nRVUDtlID");
            c1RVUSchedule.SetData(0, COL_CPT_CODE, "CPT");
            c1RVUSchedule.SetData(0, COL_CPT_DESC, "sCPTDescription");
            c1RVUSchedule.SetData(0, COL_WORK_UNITS, "Work Units");
            c1RVUSchedule.SetData(0, COL_PM_UNITS, "Practice Expense Units");
            c1RVUSchedule.SetData(0, COL_PE_UNITS, "Malpractice Units");
            c1RVUSchedule.SetData(0, COL_TOTALRVU_UNITS, "Total RVU");
            c1RVUSchedule.Rows.Count = 1;
        }


        private bool ModifySchedule(DataTable dtDetail)
        {
            CLsBL_RVUSchedule objRVUSchedule = new CLsBL_RVUSchedule();
            string strOpFlag = "U";
            bool _result = false;
            try
            {
                DataTable dtMst = new DataTable();
                dtMst.Columns.Add("nRVUID");
                dtMst.Columns.Add("dtEffectiveDate");
                dtMst.Columns.Add("nScheduleType");
                dtMst.Columns.Add("sStatementNote");
                dtMst.Columns.Add("bIsActive");
                dtMst.Rows.Add();
                dtMst.Rows[0]["nRVUID"] = _nRVUId;
                dtMst.Rows[0]["dtEffectiveDate"] = dtpEffectivedate.Text;
                dtMst.Rows[0]["nScheduleType"] = ImportPEId;
                dtMst.Rows[0]["sStatementNote"] = txt_RVUScheduleNote.Text;
                dtMst.Rows[0]["bIsActive"] = (rbActive.Checked == true ? true : (rbInactive.Checked == true ? false : false));
                dtMst.AcceptChanges();

                if (dtDetail != null && dtDetail.Rows.Count > 0)
                {
                    if (objRVUSchedule.SaveRVUScheduleTVP(dtMst, dtDetail, strOpFlag))
                    {
                        _result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                return false;
            }
            finally
            {
                if (objRVUSchedule != null) { objRVUSchedule.Dispose(); }
            }
            return _result;
        }

        private void getTotalRVU()
        {
            if (c1RVUSchedule.Rows.Count > 1)
            {
                if (c1RVUSchedule.RowSel != 0)
                {
                    if (Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PE_UNITS)) == "" && Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_WORK_UNITS)) == "" && Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PM_UNITS)) == "")
                    {
                        c1RVUSchedule.SetData(c1RVUSchedule.RowSel, COL_TOTALRVU_UNITS, DBNull.Value);
                    }
                    else
                    {
                        Decimal dPEUnits = Convert.ToDecimal(Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PE_UNITS)) == "" ? 0 : c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PE_UNITS));
                        Decimal dWUnits = Convert.ToDecimal(Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_WORK_UNITS)) == "" ? 0 : c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_WORK_UNITS));
                        Decimal dPMUnits = Convert.ToDecimal(Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PM_UNITS)) == "" ? 0 : c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PM_UNITS));
                        Decimal dTotalRVU = Convert.ToDecimal(dPEUnits + dWUnits + dPMUnits);
                        c1RVUSchedule.SetData(c1RVUSchedule.RowSel, COL_TOTALRVU_UNITS, dTotalRVU);
                    }
                }
            }
        }

        private void AddLine()
        {
            try
            {
                if (c1RVUSchedule != null)
                {
                    c1RVUSchedule.Rows.Add();
                    int rowIndex = c1RVUSchedule.Rows.Count - 1;
                    //SetCurrencyCellValue(rowIndex);
                    c1RVUSchedule.Select(rowIndex, COL_CPT_CODE, true);
                    c1RVUSchedule.Focus();
                    //c1RVUSchedule.Row = rowIndex;
                    //c1RVUSchedule.Col = COL_CPT_CODE;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupStandardFeeSchedule, ActivityType.Add, "Add New Line", 0, rowIndex, 0, ActivityOutCome.Success);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
        }

        private bool ValidateRVUSchedule(DataTable dtFinalData)
        {
            CLsBL_RVUSchedule objRVUSchedule = new CLsBL_RVUSchedule();
            bool _ReturnValue = true;
            try
            {
                //Check for Effective date Empty
                if (dtpEffectivedate.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Effective Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpEffectivedate.Focus();
                    _ReturnValue = false;
                    return _ReturnValue;
                }

                //Check for Duplicate Effective date
                string _sqlQuery = string.Empty;
                if (_nRVUId != 0)
                {
                    if (strEffectiveDate != string.Empty && Convert.ToDateTime(strEffectiveDate).Date != Convert.ToDateTime(dtpEffectivedate.Text).Date)
                    {
                        object _Name = objRVUSchedule.getEffectiveDate(dtpEffectivedate.Text.ToString().Replace("'", "''"));
                        if (Convert.ToInt64(_Name) > 0)
                        {
                            MessageBox.Show("RVU schedule Effective Date already exists.  Select a unique Effective Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpEffectivedate.Focus();
                            return false;
                        }
                    }
                }
                else
                {
                    object _Name = objRVUSchedule.getEffectiveDate(dtpEffectivedate.Text.ToString().Replace("'", "''"));
                    if (Convert.ToInt64(_Name) > 0)
                    {
                        MessageBox.Show("RVU schedule Effective Date already exists.  Select a unique Effective Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpEffectivedate.Focus();
                        return false;
                    }
                }

                //Check for Record in Grid Exists
                if (c1RVUSchedule.DataSource != null && c1RVUSchedule.Rows.Count > 1)
                {
                    Hashtable hTable = new Hashtable();
                    for (int i = 1; i <= c1RVUSchedule.Rows.Count - 1; i++)
                    {
                        if (Convert.ToString(c1RVUSchedule.GetData(i, COL_CPT_CODE)).Trim() == string.Empty)
                        {
                            MessageBox.Show("Enter CPT Code.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1RVUSchedule.Select(i, COL_CPT_CODE, true);
                            return false;
                        }
                        string _PEUnit = Convert.ToString(c1RVUSchedule.GetData(i, COL_PE_UNITS)).Trim();
                        string _MPUnit = Convert.ToString(c1RVUSchedule.GetData(i, COL_PM_UNITS)).Trim();
                        string _WUUnit = Convert.ToString(c1RVUSchedule.GetData(i, COL_WORK_UNITS)).Trim();
                        if ((_PEUnit == string.Empty && _MPUnit == string.Empty && _WUUnit == string.Empty) || (_PEUnit != string.Empty && _MPUnit != string.Empty && _WUUnit != string.Empty))
                        {
                        }
                        else
                        {
                            string Unit = (_WUUnit == string.Empty ? "Work Unit" : (_PEUnit == string.Empty ? "Practice Expense Unit" : "Malpractice Unit"));
                            string cptcode = Convert.ToString(c1RVUSchedule.GetData(i, COL_CPT_CODE)).Trim();
                            MessageBox.Show("Enter " + Unit + " for CPT Code : " + cptcode, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1RVUSchedule.Select(i, (Unit == "Work Unit" ? COL_WORK_UNITS : (Unit == "Malpractice Unit" ? COL_PM_UNITS : COL_PE_UNITS)), true);
                            return false;
                        }


                        if (hTable.Contains(Convert.ToString(c1RVUSchedule.GetData(i, COL_CPT_CODE)).Trim()))
                        {
                            MessageBox.Show("Duplicate CPT Code Exists.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1RVUSchedule.Select(i, COL_CPT_CODE, true);
                            return false;
                        }
                        else
                        {
                            hTable.Add(Convert.ToString(c1RVUSchedule.GetData(i, COL_CPT_CODE)).Trim(), string.Empty);
                        }
                    }
                    //Int64 iRowCount = Convert.ToInt64(dtFinalData.Rows.Count);
                    //Int64 iNewRowCount = Convert.ToInt64(dtFinalData.DefaultView.ToTable(true, "sCPTCode").Rows.Count);
                    //if (iRowCount != iNewRowCount)
                    //{
                    //    MessageBox.Show("Duplicate CPT Code Exists.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    c1RVUSchedule.Focus();
                    //    return false;
                    //}

                }
                else
                {
                    MessageBox.Show("Add Record to save.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1RVUSchedule.Focus();
                    return false;
                }
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            finally
            {
                if (objRVUSchedule != null) { objRVUSchedule.Dispose(); }
            }

            return _ReturnValue;
        }

        private bool ValidateForCPT(int rowIndex, int colIndex)
        {
            //CLsBL_RVUSchedule objRVUSchedule = new CLsBL_RVUSchedule();
            bool _isValid = true;
            try
            {
                //DataTable dtExistingData = null;
                //if (c1RVUSchedule.DataSource != null)
                //{
                //    dtExistingData = ((DataView)c1RVUSchedule.DataSource).ToTable();
                //}
                //if (dtExistingData != null && dtExistingData.Rows.Count > 0)
                //{
                //    DataView dv = dtExistingData.DefaultView;
                //    dv.RowFilter = "IsNull(sCPTCode, '') = ''";
                //    if (dv != null && dv.ToTable().Rows.Count > 0)
                //    {
                //        MessageBox.Show("Enter CPT Code for blank line. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        _isValid = false;
                //        return false;
                //    }
                //    dv.RowFilter = "";
                //    dtExistingData = dv.ToTable();
                //}

                int i = 0;
                if (c1RVUSchedule.Rows.Count > 1)
                {
                    i = c1RVUSchedule.FindRow(null, 1, COL_CPT_CODE, true);
                    if (i > 0)
                    {
                        c1RVUSchedule.Select(i, COL_CPT_CODE, true);
                        MessageBox.Show("Enter CPT Code for blank line. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isValid = false;
                    }
                    else
                    {
                        i = c1RVUSchedule.FindRow("", 1, COL_CPT_CODE, true);
                        {
                            if (i > 0)
                            {
                                c1RVUSchedule.Select(i, COL_CPT_CODE, true);
                                MessageBox.Show("Enter CPT Code for blank line. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _isValid = false;
                            }
                        }
                    }

                }
                //if (c1RVUSchedule.Rows.Count > 1)
                //{
                //    string _CptCode = String.Empty;
                //    if (rowIndex > 0 && colIndex == COL_CPT_CODE)
                //    {

                //        _CptCode = Convert.ToString(c1RVUSchedule.GetData(rowIndex, COL_CPT_CODE));
                //        if (_CptCode != "" || Convert.ToString(c1RVUSchedule.GetData(rowIndex, COL_CPT_CODE)) != "")
                //        {
                //            if (objRVUSchedule.IsValidCPT(_CptCode.Trim()) == false)
                //            {
                //                MessageBox.Show(_CptCode + "CPT Code is not Valid.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                c1RVUSchedule.Select(rowIndex, COL_CPT_CODE);
                //                c1RVUSchedule.SetData(rowIndex, COL_CPT_CODE, "");
                //                _isValid = false;
                //            }

                //        }
                //        else
                //        {
                //            if (!_bIsValidated)
                //            {
                //                MessageBox.Show("Enter CPT Code. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            }
                //            c1RVUSchedule.Select(rowIndex, COL_CPT_CODE);
                //            c1RVUSchedule.SetData(rowIndex, COL_CPT_CODE, "");
                //            _isValid = false;
                //        }
                //        //}// IF Close
                //        //else
                //        //{
                //        //        MessageBox.Show(" Please Enter CPT Code. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        //        c1RVUSchedule.Select(c1RVUSchedule.Rows.Count - 1, COL_CPT_CODE);

                //        //    _isValid = false;
                //        //}
                //    }
                //}
                return _isValid;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                _isValid = false;
                return _isValid;
            }
        }

        // IMPORTING EXCEL USING NPOI
        private DataTable GetExcelGlo_NPOI(string ImportFileName, ref string strMsg)
        {
            DataSet dsExcel = new DataSet();
            DataTable objdt = new DataTable();
            try
            {
                gloGlobal.NPOI_Excel oNpoi = new gloGlobal.NPOI_Excel();
                dsExcel = oNpoi.ImportRVUExcel_NPOI(ImportFileName, gloGlobal.NPOI_Excel.ExcelFileFormat.RVU_GlostreamFormat);
                if (dsExcel != null)
                {
                    if (dsExcel.Tables.Count > 0)
                    {
                        objdt = dsExcel.Tables[0];
                        if (objdt != null && objdt.Rows.Count > 0 && objdt.Columns.Count == 7)
                        {
                            try
                            {
                                DateTime dtEffectivedate = DateTime.Now.Date;
                                if (DateTime.TryParse(Convert.ToString(objdt.Rows[0].ItemArray[6]), out dtEffectivedate))
                                {
                                    dtpEffectivedate.Text = string.Format("{0:MM/dd/yyyy}", dtEffectivedate.ToShortDateString());
                                }
                                else
                                {
                                    dtpEffectivedate.Text = string.Format("{0:MM/dd/yyyy}", dtEffectivedate.ToShortDateString());
                                }
                            }
                            catch(Exception  ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                                strMsg = "Invalid file Format";
                                return null;
                            }
                            if (objdt.Rows.Count > 0)
                            {
                                DataColumn nRVUID = new DataColumn();
                                nRVUID.DataType = System.Type.GetType("System.Int64");
                                nRVUID.DefaultValue = 0;
                                nRVUID.ColumnName = "nRVUID";
                                nRVUID.Caption = "nRVUID";

                                DataColumn nRVUDtlID = new DataColumn();
                                nRVUDtlID.DataType = System.Type.GetType("System.Int64");
                                nRVUDtlID.DefaultValue = 0;
                                nRVUDtlID.ColumnName = "nRVUDtlID";
                                nRVUDtlID.Caption = "nRVUDtlID";

                                objdt.Columns.Add(nRVUID);
                                objdt.Columns.Add(nRVUDtlID);
                                objdt.Columns[0].ColumnName = "sCPTCode";
                                objdt.Columns[1].ColumnName = "sModifier";
                                objdt.Columns[2].ColumnName = "sCPTDescription";
                                objdt.Columns[3].ColumnName = "dWorkUnits";
                                objdt.Columns[4].ColumnName = "dMPUnits";
                                objdt.Columns[5].ColumnName = "dPEUnits";
                                objdt.AcceptChanges();
                                DataView dv = new DataView(objdt);
                                dv.RowFilter = "IsNull(sCPTDescription,'') <> '' "; //"IsNull(sModifier, '') = '' AND IsNull(sCPTDescription,'') <> '' ";
                                objdt = dv.ToTable();
                                objdt.Columns["nRVUID"].SetOrdinal(0);
                                objdt.Columns["nRVUDtlID"].SetOrdinal(1);
                                objdt.Columns.Add("dTotalRVU");
                                objdt.AcceptChanges();
                                //SLR: Logic problem to be changed on 2/4/2014
                                for (int i = 0; i <= objdt.Columns.Count - 1; i++)
                                {
                                    if (objdt.Columns[i].ColumnName != "sCPTCode" && objdt.Columns[i].ColumnName != "sModifier" && objdt.Columns[i].ColumnName != "sCPTDescription" && objdt.Columns[i].ColumnName != "dWorkUnits" && objdt.Columns[i].ColumnName != "dMPUnits" && objdt.Columns[i].ColumnName != "dPEUnits" && objdt.Columns[i].ColumnName != "dTotalRVU" && objdt.Columns[i].ColumnName != "nRVUID" && objdt.Columns[i].ColumnName != "nRVUDtlID")
                                    {
                                        objdt.Columns.RemoveAt(i);
                                        i--;
                                        objdt.AcceptChanges();
                                    }
                                }
                            }
                        }
                        else
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog("File Not Converted Properly in ImportRVUExcel_NPOI()", false);
                            strMsg = "Invalid file Format";
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                strMsg = "Invalid file Format";
                objdt = null;
            }
            return objdt;
        }
      
        // IMPORTING EXCEL USING OLEDB & Microsoft.Office.Interop
        private DataTable GetExcelGlo(string ImportFileName, ref string strMsg)
        {
            DataTable objdt = new DataTable();
            try
            {
                string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ImportFileName + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR:YES;\"";
                System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connstr);
                conn.Open();
                System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter();
                System.Data.OleDb.OleDbCommand objCmdSelect = null;
                try
                {
                    DataTable schemaTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    string sheet = schemaTable.Rows[0]["TABLE_NAME"].ToString();
                    string[] restrictions = { null, null, sheet, null };
                    DataTable columns = conn.GetSchema("Columns", restrictions);
                    string strquery = "SELECT * FROM  [" + sheet + "]";
                    objCmdSelect = new System.Data.OleDb.OleDbCommand(strquery, conn);
                    objAdapter.SelectCommand = objCmdSelect;
                    objAdapter.Fill(objdt);
                    //Disposing Connection objects
                    objAdapter.Dispose();
                    objCmdSelect.Dispose();
                    conn.Close();
                    conn.Dispose();
                }
                catch
                {
                    if (objCmdSelect != null) { objCmdSelect.Dispose(); }
                    if (conn != null) { conn.Close(); conn.Dispose(); }
                    strMsg = "Invalid file Format";
                    return null;
                }

                if (objdt != null && objdt.Rows.Count > 0 && objdt.Columns.Count == 7)
                {
                    try
                    {
                        DateTime dtEffectivedate = DateTime.Now.Date;
                        if (DateTime.TryParse(Convert.ToString(objdt.Rows[0].ItemArray[6]), out dtEffectivedate))
                        {
                            dtpEffectivedate.Text = string.Format("{0:MM/dd/yyyy}", dtEffectivedate.ToShortDateString());
                        }
                        else
                        {
                            dtpEffectivedate.Text = string.Format("{0:MM/dd/yyyy}", dtEffectivedate.ToShortDateString());
                        }
                    }
                    catch
                    {
                        strMsg = "Invalid file Format";
                        return null;
                    }
                    if (objdt.Rows.Count > 0)
                    {
                        DataColumn nRVUID = new DataColumn();
                        nRVUID.DataType = System.Type.GetType("System.Int64");
                        nRVUID.DefaultValue = 0;
                        nRVUID.ColumnName = "nRVUID";
                        nRVUID.Caption = "nRVUID";

                        DataColumn nRVUDtlID = new DataColumn();
                        nRVUDtlID.DataType = System.Type.GetType("System.Int64");
                        nRVUDtlID.DefaultValue = 0;
                        nRVUDtlID.ColumnName = "nRVUDtlID";
                        nRVUDtlID.Caption = "nRVUDtlID";

                        objdt.Columns.Add(nRVUID);
                        objdt.Columns.Add(nRVUDtlID);

                        //objdt.Columns.Add("nRVUID", System.Type.GetType("System.Int64"));
                        //objdt.Columns.Add("nRVUDtlID", System.Type.GetType("System.Int64"));
                        //objdt.Columns["nRVUID"].DefaultValue = 0;
                        //objdt.Columns["nRVUDtlID"].DefaultValue = 0;
                        objdt.Columns[0].ColumnName = "sCPTCode";
                        objdt.Columns[1].ColumnName = "sModifier";
                        objdt.Columns[2].ColumnName = "sCPTDescription";
                        objdt.Columns[3].ColumnName = "dWorkUnits";
                        objdt.Columns[4].ColumnName = "dMPUnits";
                        objdt.Columns[5].ColumnName = "dPEUnits";
                        objdt.AcceptChanges();
                        DataView dv = new DataView(objdt);

                        //skipped validation for IsNull(sModifier, '')  in 8060 in order to import Modifiers that should be respected in RVU report, also importing logic for new RVU file has being added
                        dv.RowFilter = "IsNull(sCPTDescription,'') <> '' "; //"IsNull(sModifier, '') = '' AND IsNull(sCPTDescription,'') <> '' ";

                        objdt = dv.ToTable();
                        //objdt.Columns.Remove("sModifier");//commented in 8060 in order to import Modifiers that should be respected in RVU report, also importing logic for new RVU file has being added
                        objdt.Columns["nRVUID"].SetOrdinal(0);
                        objdt.Columns["nRVUDtlID"].SetOrdinal(1);
                        objdt.Columns.Add("dTotalRVU");
                        objdt.AcceptChanges();
                        //SLR: Logic problem to be changed on 2/4/2014
                        for (int i = 0; i <= objdt.Columns.Count - 1; i++)
                        {
                            if (objdt.Columns[i].ColumnName != "sCPTCode" && objdt.Columns[i].ColumnName != "sModifier" && objdt.Columns[i].ColumnName != "sCPTDescription" && objdt.Columns[i].ColumnName != "dWorkUnits" && objdt.Columns[i].ColumnName != "dMPUnits" && objdt.Columns[i].ColumnName != "dPEUnits" && objdt.Columns[i].ColumnName != "dTotalRVU" && objdt.Columns[i].ColumnName != "nRVUID" && objdt.Columns[i].ColumnName != "nRVUDtlID")
                            {
                                objdt.Columns.RemoveAt(i);
                                i--;
                                objdt.AcceptChanges();
                            }
                        }

                    }


                }
                else
                {
                    strMsg = "Invalid file Format";
                    return null;
                }


            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, false);
                strMsg = "Invalid file Format";
                objdt = null;
            }
            return objdt;
        }

      
        public DataTable TransferCSVToTable(string strfile, ref string strMsg)
        {
            DataTable dt = new DataTable();
            DataColumn nRVUID = new DataColumn();
            nRVUID.DataType = System.Type.GetType("System.Int64");
            nRVUID.DefaultValue = 0;
            nRVUID.ColumnName = "nRVUID";
            nRVUID.Caption = "nRVUID";

            DataColumn nRVUDtlID = new DataColumn();
            nRVUDtlID.DataType = System.Type.GetType("System.Int64");
            nRVUDtlID.DefaultValue = 0;
            nRVUDtlID.ColumnName = "nRVUDtlID";
            nRVUDtlID.Caption = "nRVUDtlID";

            dt.Columns.Add(nRVUID);
            dt.Columns.Add(nRVUDtlID);
            dt.Columns.Add("sCPTCode");
            dt.Columns.Add("sCPTDescription");
            dt.Columns.Add("dWorkUnits");
            dt.Columns.Add("dMPUnits");
            dt.Columns.Add("dPEUnits");
            dt.Columns.Add("dTotalRVU");
            dt.Columns.Add("sModifier");
            try
            {
                string[] csvRows = null;
                try
                {
                    csvRows = System.IO.File.ReadAllLines(strfile);
                }
                catch
                {
                    strMsg = "File is already open on your system. Close file first before Import.";
                    return null;
                }

                string[] fields = null;
                if (csvRows != null && csvRows.Length > 0)
                {
                    for (Int64 i = 0; i <= csvRows.LongLength - 1; i++)
                    {
                        fields = csvRows[i].Split('~');
                        if (fields != null && fields.Length > 0 && fields.Length == 37)
                        {
                            string sRangeEffDate = "";
                            if (i == 4)
                            {
                                sRangeEffDate = Convert.ToString(fields[2]);
                            }
                            try
                            {
                                DateTime dtEffectivedate = DateTime.Now.Date;
                                if (DateTime.TryParse(sRangeEffDate, out dtEffectivedate))
                                {
                                    dtpEffectivedate.Text = string.Format("{0:MM/dd/yyyy}", dtEffectivedate.ToShortDateString());
                                }
                                else
                                {
                                    dtpEffectivedate.Text = string.Format("{0:MM/dd/yyyy}", dtEffectivedate.ToShortDateString());
                                }
                            }
                            catch
                            {
                                strMsg = "Invalid file Format";
                                return null;
                            }

                            if (i > 9)
                            {
                                DataRow row = dt.NewRow();
                                //if (fields.Length == row.ItemArray.Length)
                                //{
                                row["sCPTCode"] = fields[0];
                                row["sCPTDescription"] = fields[2];
                                row["dWorkUnits"] = fields[5];
                                row["dMPUnits"] = fields[14];

                                if (ImportPEId == 1)
                                {
                                    row["dPEUnits"] = fields[6];
                                    row["dTotalRVU"] = fields[15];
                                }
                                else if (ImportPEId == 2)
                                {
                                    row["dPEUnits"] = fields[10];
                                    row["dTotalRVU"] = fields[17];
                                }
                                else if (ImportPEId == 3)
                                {
                                    row["dPEUnits"] = fields[8];
                                    row["dTotalRVU"] = fields[16];
                                }
                                else if (ImportPEId == 4)
                                {
                                    row["dPEUnits"] = fields[12];
                                    row["dTotalRVU"] = fields[18];
                                }
                                row["sModifier"] = fields[1];
                                //}
                                dt.Rows.Add(row);
                            }
                            dt.AcceptChanges();

                        }
                        else
                        {
                            strMsg = "Invalid File Format.";
                            return null;
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {

                        DataView dv = new DataView(dt);
                        dv.RowFilter = "IsNull(sModifier, '') = '' AND IsNull(sCPTDescription,'') <> '' ";
                        dt = dv.ToTable();
                        dt.Columns.Remove("sModifier");
                        dt.Columns.Remove("sModifier");
                        dt.Columns["nRVUID"].SetOrdinal(0);
                        dt.Columns["nRVUDtlID"].SetOrdinal(1);
                        dt.AcceptChanges();
                    }
                }
                else
                {
                    strMsg = "Invalid File Format.";
                    return null;
                }

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, true);
                strMsg = "Invalid data in file.";
                dt = null;
            }
            return dt;
        }

        public DataTable ReadStandardCSVToTable(string filename, ref string strMsg)
        {
            DataTable dt = new DataTable();
            DataColumn nRVUID = new DataColumn();
            nRVUID.DataType = System.Type.GetType("System.Int64");
            nRVUID.DefaultValue = 0;
            nRVUID.ColumnName = "nRVUID";
            nRVUID.Caption = "nRVUID";

            DataColumn nRVUDtlID = new DataColumn();
            nRVUDtlID.DataType = System.Type.GetType("System.Int64");
            nRVUDtlID.DefaultValue = 0;
            nRVUDtlID.ColumnName = "nRVUDtlID";
            nRVUDtlID.Caption = "nRVUDtlID";

            dt.Columns.Add(nRVUID);
            dt.Columns.Add(nRVUDtlID);
            dt.Columns.Add("sCPTCode");
            dt.Columns.Add("sCPTDescription");
            dt.Columns.Add("dWorkUnits");
            dt.Columns.Add("dMPUnits");
            dt.Columns.Add("dPEUnits");
            dt.Columns.Add("sModifier");
            dt.Columns.Add("dtEffectiveDate");
            try
            {
                string[] csvRows = null;
                try
                {
                    csvRows = System.IO.File.ReadAllLines(filename);
                }
                catch
                {
                    strMsg = "File is already open on your system. Close file first before Import.";
                    return null;
                }

                string[] fields = null;
                if (csvRows != null && csvRows.Length > 0)
                {
                    for (Int64 i = 1; i <= csvRows.Length - 1; i++)
                    {
                        fields = csvRows[i].Split('~');

                        if (fields != null && fields.Length > 0 && fields.Length == 10)
                        {
                            DataRow row = dt.NewRow();
                            //if (fields.Length == row.ItemArray.Length)
                            //{
                            row["sCPTCode"] = fields[0];
                            row["sModifier"] = fields[1];
                            row["sCPTDescription"] = fields[2];
                            row["dWorkUnits"] = fields[3];
                            row["dMPUnits"] = fields[4];
                            row["dtEffectiveDate"] = fields[9];

                            if (ImportPEId == 1)
                            {
                                row["dPEUnits"] = fields[5];
                            }
                            else if (ImportPEId == 2)
                            {
                                row["dPEUnits"] = fields[6];
                            }
                            else if (ImportPEId == 3)
                            {
                                row["dPEUnits"] = fields[7];
                            }
                            else if (ImportPEId == 4)
                            {
                                row["dPEUnits"] = fields[8];
                            }

                            dt.Rows.Add(row);

                        }
                        else
                        {
                            strMsg = "Invalid File Format.";
                            return null;
                        }
                    }
                    dt.AcceptChanges();
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            DateTime dtEffectivedate = DateTime.Now.Date;
                            if (DateTime.TryParse(Convert.ToString(dt.Rows[0]["dtEffectiveDate"]), out dtEffectivedate))
                            {
                                dtpEffectivedate.Text = string.Format("{0:MM/dd/yyyy}", dtEffectivedate.ToShortDateString());
                            }
                            else
                            {
                                dtpEffectivedate.Text = string.Format("{0:MM/dd/yyyy}", dtEffectivedate.ToShortDateString());
                            }
                        }
                        catch
                        {
                            strMsg = "Invalid file Format";
                            return null;
                        }

                        DataView dv = new DataView(dt);
                        dv.RowFilter = "IsNull(sModifier, '') = '' AND IsNull(sCPTDescription,'') <> '' ";
                        dt = dv.ToTable();
                        dt.Columns.Remove("sModifier");
                        dt.Columns.Remove("dtEffectiveDate");
                        dt.Columns["nRVUID"].SetOrdinal(0);
                        dt.Columns["nRVUDtlID"].SetOrdinal(1);
                        dt.Columns.Add("dTotalRVU");
                        dt.AcceptChanges();

                    }

                }
                else
                {
                    strMsg = "Invalid File Format.";
                    return null;
                }

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, true);
                strMsg = "Invalid data in file.";
                dt = null;
            }
            return dt;



        }


        #region Standard RUV File Conversion

        // IMPORTING EXCEL USING NPOI
        public DataTable GetExcel_NPOI(string fileName, ref string strMsg)
        {
            DataSet dsExcel = new DataSet();
            DataTable dtExcelReleaseDate = new DataTable();
            DataTable dtExcelData = new DataTable();
            Boolean IsRVUFileNewFormat = false;

            gloGlobal.NPOI_Excel oNpoi = new gloGlobal.NPOI_Excel();
            try
            {
                dsExcel = oNpoi.ImportRVUExcel_NPOI(fileName, gloGlobal.NPOI_Excel.ExcelFileFormat.RVU_Original);

                if (dsExcel != null)
                {
                    if (dsExcel.Tables.Count > 0)
                    {
                        #region Check Excel File Format
                        dtExcelReleaseDate = dsExcel.Tables[1];
                        if (dtExcelReleaseDate != null)
                        {
                            if (dtExcelReleaseDate.Rows.Count > 0)
                            {
                                string sRangeEffDate = Convert.ToString(dtExcelReleaseDate.Rows[0]["RVU File Release Date"]);
                                if (sRangeEffDate.Trim() != string.Empty)
                                {

                                    string sRDate = "";
                                    sRDate = sRangeEffDate.Trim().Substring(sRangeEffDate.Trim().LastIndexOf(" ") + 1);
                                    DateTime dtEffectivedate = DateTime.Now.Date;
                                    if (DateTime.TryParse(sRDate, out dtEffectivedate))
                                    {
                                        dtpEffectivedate.Text = string.Format("{0:MM/dd/yyyy}", dtEffectivedate.ToShortDateString());
                                    }
                                    else
                                    {
                                        dtpEffectivedate.Text = string.Format("{0:MM/dd/yyyy}", dtEffectivedate.ToShortDateString());
                                    }

                                    DateTime dtdate = DateTime.Parse(sRDate);
                                    //as discussed with Julie on 23 july 2015 the new format file should import for only [Fully Implemented Facility and Non-Facility] Practice Expense type and give validation for [Transitioned and Non-Transitioned ] Practice Expense type.
                                    //1 = Transitioned, Facility
                                    //2 = Non-Transitioned, Facility
                                    //3 = Fully Implemented, Non-Facility
                                    //4 = Fully Implemented, Facility
                                    //As per our discussion with OM team following will be our new implementation from 8060 onwards. refer email julie dt: 22 July 2015 Sub: Copy of PPRRVU15_V1223c.xlsx
                                    //1.       Any file that has RELEASED year “Greater than equal to 2014 i.e. year >= 2014” will be consider as NEW FILE format.
                                    //2.       We want your confirmation for what should the CUT OFF month ??? i.e. as shown in highlighted screen shot the Month is December i.e. 12, so any file having Released Month as December and year greater than 2014 will be considered as new file format. 

                                    if (dtdate.Year > 2014)//means  year is 2015, 2016 ... or more 
                                    {
                                        if (ImportPEId == 1 || ImportPEId == 2)
                                        {
                                            strMsg = "The selected file does not support [Transitioned, Facility] and [Transitioned, Non-Facility] Practice Expense type!." + Environment.NewLine + "Please select one of following options from Practice Expense type." + Environment.NewLine + "1. Fully Implemented Facility " + Environment.NewLine + "2. Fully implemented Non-Facility";
                                            return null;
                                        }
                                        else
                                        {
                                            IsRVUFileNewFormat = true;
                                        }
                                    }
                                    else//year is 2014 or less than that ie 2013, 2012 ...etc
                                    {
                                        //specifically in case of year = 2014 and month = 12 i.e. december then file is new format.
                                        if (dtdate.Year == 2014 && dtdate.Month == 12)//year is 2014 and month = 12 specificialy then new file format
                                        {
                                            if (ImportPEId == 1 || ImportPEId == 2)
                                            {
                                                strMsg = "The selected file does not support [Transitioned, Facility] and [Transitioned, Non-Facility] Practice Expense type!." + Environment.NewLine + "Please select one of following options from Practice Expense type." + Environment.NewLine + "1. Fully Implemented Facility " + Environment.NewLine + "2. Fully implemented Non-Facility";
                                                return null;
                                            }
                                            else//the option selected is 1 or 2 so import using new format file
                                            {
                                                IsRVUFileNewFormat = true;
                                            }
                                        }
                                        else
                                        {
                                            IsRVUFileNewFormat = false;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        string strSubMsg = "";
                        //passed parameter to know whether RVU file format is old or New
                        dtExcelData = ImportExcelXLS_NPOI(dsExcel.Tables[0], ref strSubMsg, IsRVUFileNewFormat);
                    }
                }
                return dtExcelData;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                strMsg = "Improper file Format";
                return null;
            }
            finally
            {
                if (dsExcel != null)
                {
                    dsExcel.Dispose();
                    dsExcel = null;
                }

                if (oNpoi  != null )
                {
                    oNpoi = null;
                }
            }
           
        }

        private DataTable ImportExcelXLS_NPOI(DataTable objdt, ref string strMsg, bool IsRVUFileNewFormat)
        {
            int nColCnt = 0;
            //in new RVU file format use there are total [31] coloumn in the file
            if (IsRVUFileNewFormat == false)
            {
                nColCnt = 37;//as implemented from 6050 version
            }
            else
            {
                nColCnt = 31;
            }

            try
            {
                if (objdt != null && objdt.Rows.Count > 5 && objdt.Columns.Count == nColCnt)
                {
                    for (int j = 0; j <= objdt.Columns.Count - 1; j++)
                    {
                        string strCol = string.Empty;
                        try
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (strCol != "")
                                    strCol += " " + Convert.ToString(objdt.Rows[i].ItemArray[j]);
                                else
                                    strCol = Convert.ToString(objdt.Rows[i].ItemArray[j]);
                            }
                            if (IsRVUFileNewFormat == false)
                            {
                                if (j == 31)
                                {
                                    strCol = "PHYSICIAN SUPERVISION OF " + strCol;
                                }
                                else if (j == 33)
                                {
                                    strCol = "DIAGNOSTIC IMAGING " + strCol;
                                }
                                else if (j == 34)
                                {
                                    strCol = "NON-FACILITY PE USED FOR OPPS " + strCol;
                                }
                                else if (j == 35)
                                {
                                    strCol = "FACILITY PE USED FOR OPPS " + strCol;
                                }
                                else if (j == 36)
                                {
                                    strCol = "MP USED FOR OPPS " + strCol;
                                }
                            }
                            objdt.Columns[j].ColumnName = strCol;
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                        }
                    }
                    objdt.AcceptChanges();
                    int totalRows = objdt.Rows.Count;
                    while (objdt.Rows.Count != totalRows - 5)
                    {
                        objdt.Rows[0].Delete();
                        objdt.AcceptChanges();
                    }
                    objdt.AcceptChanges();
                    if (objdt != null && objdt.Rows.Count > 0)
                    {
                        DataColumn nRVUID = new DataColumn();
                        nRVUID.DataType = System.Type.GetType("System.Int64");
                        nRVUID.DefaultValue = 0;
                        nRVUID.ColumnName = "nRVUID";
                        nRVUID.Caption = "nRVUID";

                        DataColumn nRVUDtlID = new DataColumn();
                        nRVUDtlID.DataType = System.Type.GetType("System.Int64");
                        nRVUDtlID.DefaultValue = 0;
                        nRVUDtlID.ColumnName = "nRVUDtlID";
                        nRVUDtlID.Caption = "nRVUDtlID";

                        objdt.Columns.Add(nRVUID);
                        objdt.Columns.Add(nRVUDtlID);

                        objdt.Columns[0].ColumnName = "sCPTCode";
                        objdt.Columns[1].ColumnName = "sModifier";//modifier coloumn added from 8060
                        objdt.Columns[2].ColumnName = "sCPTDescription";
                        objdt.Columns[5].ColumnName = "dWorkUnits";

                        //for new RVU file format use col [10] for Mal Practice value
                        if (IsRVUFileNewFormat == false)
                        {
                            objdt.Columns[14].ColumnName = "dMPUnits";
                        }
                        else
                        {
                            objdt.Columns[10].ColumnName = "dMPUnits";
                        }

                        if (ImportPEId == 1)//Transitioned, Non-Facility here for PE units col [G i.e. NON-FAC PE RVU] is read from excel.
                        {
                            objdt.Columns[6].ColumnName = "dPEUnits";//common coloumn used for old and new RVU file format
                        }

                        else if (ImportPEId == 2)//Transitioned,Facility here for PE units col [K i.e. FACILITY PE RVU] is read from excel.
                        {
                            //for new RVU file format use col [8] for Facility PE units
                            if (IsRVUFileNewFormat == false)
                            {
                                objdt.Columns[10].ColumnName = "dPEUnits";//as implemented from 6050 version
                            }
                            else
                            {
                                objdt.Columns[8].ColumnName = "dPEUnits";
                            }
                        }
                        else if (ImportPEId == 3)//Fully Implemented, Non-Facility here for PE units col [G] is read from excel.
                        {
                            objdt.Columns[6].ColumnName = "dPEUnits";
                        }
                        else if (ImportPEId == 4)//Fully Implemented, Facility here for PE units col [I] is read from excel.
                        {
                            objdt.Columns[8].ColumnName = "dPEUnits";
                        }
                        objdt.AcceptChanges();
                    }

                    for (int i = 0; i <= objdt.Columns.Count - 1; i++)
                    {
                        if (objdt.Columns[i].ColumnName != "sCPTCode" && objdt.Columns[i].ColumnName != "sCPTDescription" && objdt.Columns[i].ColumnName != "dWorkUnits" && objdt.Columns[i].ColumnName != "dMPUnits" && objdt.Columns[i].ColumnName != "dPEUnits" && objdt.Columns[i].ColumnName != "sModifier" && objdt.Columns[i].ColumnName != "nRVUID" && objdt.Columns[i].ColumnName != "nRVUDtlID")
                        {
                            objdt.Columns.RemoveAt(i);
                            i--;
                            objdt.AcceptChanges();
                        }
                    }
                    objdt.AcceptChanges();

                    DataView dv = new DataView(objdt);
                    dv.RowFilter = "IsNull(sCPTDescription,'') <> '' ";//"IsNull(sModifier, '') = '' AND IsNull(sCPTDescription,'') <> '' ";
                    objdt = dv.ToTable();
                    objdt.Columns["nRVUID"].SetOrdinal(0);
                    objdt.Columns["nRVUDtlID"].SetOrdinal(1);
                    int col_Ordinal = objdt.Columns["dPEUnits"].Ordinal;
                    objdt.Columns["dPEUnits"].SetOrdinal(objdt.Columns["dMPUnits"].Ordinal);
                    objdt.Columns["dMPUnits"].SetOrdinal(col_Ordinal);
                    objdt.Columns.Add("dTotalRVU");
                    objdt.AcceptChanges();
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                strMsg = "Invalid file Format";
                objdt = null;
            }
            return objdt;
        }

        // IMPORTING EXCEL USING OLEDB & Microsoft.Office.Interop
        public DataTable GetExcel(string fileName, ref string strMsg)
        {
            DataTable dtExcelData = new DataTable();
            String tempstrfileName = "";
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel.Workbook oWB;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;
            oXL = new Microsoft.Office.Interop.Excel.Application();

            //variable taken to know whether it is old RVU file format or new
            Boolean IsRVUFileNewFormat = false;
            try
            {
                //  creat a Application object

                //   get   WorkBook  object
                oWB = oXL.Workbooks.Open(fileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value);

                //   get   WorkSheet object 
                oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oWB.Sheets[1];
                System.Data.DataTable dt = new System.Data.DataTable("dtExcel");
                Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range("A1", "A4".ToString());
                string sRangeEffDate = Convert.ToString(oSheet.get_Range("C5", "C5".ToString()).Cells.Value);
                if (sRangeEffDate.Trim() != string.Empty)
                {

                    string sRDate = "";
                    sRDate = sRangeEffDate.Trim().Substring(sRangeEffDate.Trim().LastIndexOf(" ") + 1);
                    DateTime dtEffectivedate = DateTime.Now.Date;
                    if (DateTime.TryParse(sRDate, out dtEffectivedate))
                    {
                        dtpEffectivedate.Text = string.Format("{0:MM/dd/yyyy}", dtEffectivedate.ToShortDateString());
                    }
                    else
                    {
                        dtpEffectivedate.Text = string.Format("{0:MM/dd/yyyy}", dtEffectivedate.ToShortDateString());
                    }

                    DateTime dtdate = DateTime.Parse(sRDate);
                    //as discussed with Julie on 23 july 2015 the new format file should import for only [Fully Implemented Facility and Non-Facility] Practice Expense type and give validation for [Transitioned and Non-Transitioned ] Practice Expense type.
                    //1 = Transitioned, Facility
                    //2 = Non-Transitioned, Facility
                    //3 = Fully Implemented, Non-Facility
                    //4 = Fully Implemented, Facility
                    //As per our discussion with OM team following will be our new implementation from 8060 onwards. refer email julie dt: 22 July 2015 Sub: Copy of PPRRVU15_V1223c.xlsx
                    //1.       Any file that has RELEASED year “Greater than equal to 2014 i.e. year >= 2014” will be consider as NEW FILE format.
                    //2.       We want your confirmation for what should the CUT OFF month ??? i.e. as shown in highlighted screen shot the Month is December i.e. 12, so any file having Released Month as December and year greater than 2014 will be considered as new file format. 

                    if (dtdate.Year > 2014)//means  year is 2015, 2016 ... or more 
                    {

                        if (ImportPEId == 1 || ImportPEId == 2)
                        {
                            //message changed after discussion with julie on 23 July 2015
                            strMsg = "The selected file does not support [Transitioned, Facility] and [Transitioned, Non-Facility] Practice Expense type!." + Environment.NewLine + "Please select one of following options from Practice Expense type." + Environment.NewLine + "1. Fully Implemented Facility " + Environment.NewLine + "2. Fully implemented Non-Facility";

                            return null;
                        }
                        else//the option selected is 1 or 2 so import using new format file
                        {
                            IsRVUFileNewFormat = true;
                        }
                    }
                    else//year is 2014 or less than that ie 2013, 2012 ...etc
                    {
                        //specifically in case of year = 2014 and month = 12 i.e. december then file is new format.
                        if (dtdate.Year == 2014 && dtdate.Month == 12)//year is 2014 and month = 12 specificialy then new file format
                        {
                            if (ImportPEId == 1 || ImportPEId == 2)
                            {
                                strMsg = "The selected file does not support [Transitioned, Facility] and [Transitioned, Non-Facility] Practice Expense type!." + Environment.NewLine + "Please select one of following options from Practice Expense type." + Environment.NewLine + "1. Fully Implemented Facility " + Environment.NewLine + "2. Fully implemented Non-Facility";

                                return null;
                            }
                            else//the option selected is 1 or 2 so import using new format file
                            {
                                IsRVUFileNewFormat = true;
                            }
                        }
                        else
                        {
                            //MessageBox.Show("Old Format");
                            //file is of old format so no change in old logic.
                            IsRVUFileNewFormat = false;
                        }

                    }
                }
                //setting the range for deleting the rows

                range.EntireRow.Delete(Microsoft.Office.Interop.Excel.XlDirection.xlUp);

                // oWB.Save();
                string ext = fileName.Substring(fileName.LastIndexOf(".") + 1);
                string path = appSettings["StartupPath"] + "\\RVU";
                if (!Directory.Exists(path))
                {
                    try
                    {
                        DirectoryInfo di = Directory.CreateDirectory(path);
                    }
                    catch
                    {
                        strMsg = "Not Sufficient permissions to write a file.";
                        return null;
                    }
                }

                string strTmpPath = appSettings["StartupPath"] + "\\RVU\\" + fileName.Substring(fileName.LastIndexOf("\\") + 1);
                tempstrfileName = strTmpPath.Substring(0, strTmpPath.LastIndexOf(".")) + "_" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now) + "." + ext;
                oWB.SaveAs(tempstrfileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                         Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value
                        );
                //save the workbook

                oWB.Close(false, "", false);
                //close the workbook
                oXL.Workbooks.Close();
                oXL.Quit();
                string strSubMsg = "";

                //passed parameter to know whether RVU file format is old or New
                dtExcelData = ImportExcelXLS(tempstrfileName, ref strSubMsg, IsRVUFileNewFormat);

                if (dtExcelData == null)
                {
                    File.Delete(tempstrfileName);
                    strMsg = strSubMsg;
                    return null;
                }
                File.Delete(tempstrfileName);

            }
            catch
            {
                strMsg = "Invalid File Format.";
                if (oXL != null) { oXL.Quit(); }
                dtExcelData = null;
            }
            finally
            {
                if (oXL != null) { oXL.Quit(); }
                if (File.Exists(tempstrfileName))
                {
                    File.Delete(tempstrfileName);
                }

            }
            return dtExcelData;
        }
              
        private DataTable ImportExcelXLS(string FileName, ref string strMsg, bool IsRVUFileNewFormat)
        {
            DataTable objdt = new DataTable();
            string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR:YES;\"";
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connstr);
            conn.Open();
            System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter();
            System.Data.OleDb.OleDbCommand objCmdSelect = null;
            int nColCnt = 0;
            try
            {

                try
                {
                    DataTable schemaTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    string sheet = schemaTable.Rows[0]["TABLE_NAME"].ToString();
                    string strquery = "SELECT * FROM  [" + sheet + "]";
                    objCmdSelect = new System.Data.OleDb.OleDbCommand(strquery, conn);
                    objAdapter.SelectCommand = objCmdSelect;
                    objAdapter.Fill(objdt);
                    //Disposing Connection objects
                    objAdapter.Dispose();
                    objCmdSelect.Dispose();
                    conn.Close();
                    conn.Dispose();
                }
                catch
                {
                    if (objCmdSelect != null) { objCmdSelect.Dispose(); }
                    if (objAdapter != null) { objAdapter.Dispose(); }
                    if (conn != null) { conn.Close(); conn.Dispose(); }
                    strMsg = "Invalid file Format";
                    return null;
                }

                //in new RVU file format use there are total [31] coloumn in the file
                if (IsRVUFileNewFormat == false)
                {
                    nColCnt = 37;//as implemented from 6050 version
                }
                else
                {
                    nColCnt = 31;
                }


                if (objdt != null && objdt.Rows.Count > 5 && objdt.Columns.Count == nColCnt)
                {
                    for (int j = 0; j <= objdt.Columns.Count - 1; j++)
                    {
                        string strCol = string.Empty;

                        for (int i = 0; i < 5; i++)
                        {
                            if (strCol != "")
                                strCol += " " + Convert.ToString(objdt.Rows[i].ItemArray[j]);
                            else
                                strCol = Convert.ToString(objdt.Rows[i].ItemArray[j]);
                        }

                        objdt.Columns[j].ColumnName = strCol;


                    }
                    objdt.AcceptChanges();
                    int totalRows = objdt.Rows.Count;
                    while (objdt.Rows.Count != totalRows - 5)
                    {
                        objdt.Rows[0].Delete();
                        objdt.AcceptChanges();
                    }
                    objdt.AcceptChanges();
                    if (objdt != null && objdt.Rows.Count > 0)
                    {
                        DataColumn nRVUID = new DataColumn();
                        nRVUID.DataType = System.Type.GetType("System.Int64");
                        nRVUID.DefaultValue = 0;
                        nRVUID.ColumnName = "nRVUID";
                        nRVUID.Caption = "nRVUID";

                        DataColumn nRVUDtlID = new DataColumn();
                        nRVUDtlID.DataType = System.Type.GetType("System.Int64");
                        nRVUDtlID.DefaultValue = 0;
                        nRVUDtlID.ColumnName = "nRVUDtlID";
                        nRVUDtlID.Caption = "nRVUDtlID";

                        objdt.Columns.Add(nRVUID);
                        objdt.Columns.Add(nRVUDtlID);



                        objdt.Columns[0].ColumnName = "sCPTCode";
                        objdt.Columns[1].ColumnName = "sModifier";//modifier coloumn added from 8060
                        objdt.Columns[2].ColumnName = "sCPTDescription";
                        objdt.Columns[5].ColumnName = "dWorkUnits";

                        //for new RVU file format use col [10] for Mal Practice value
                        if (IsRVUFileNewFormat == false)
                        {
                            objdt.Columns[14].ColumnName = "dMPUnits";
                        }
                        else
                        {
                            objdt.Columns[10].ColumnName = "dMPUnits";
                        }


                        if (ImportPEId == 1)//Transitioned, Non-Facility here for PE units col [G i.e. NON-FAC PE RVU] is read from excel.
                        {
                            objdt.Columns[6].ColumnName = "dPEUnits";//common coloumn used for old and new RVU file format
                        }

                        else if (ImportPEId == 2)//Transitioned,Facility here for PE units col [K i.e. FACILITY PE RVU] is read from excel.
                        {
                            //for new RVU file format use col [8] for Facility PE units
                            if (IsRVUFileNewFormat == false)
                            {
                                objdt.Columns[10].ColumnName = "dPEUnits";//as implemented from 6050 version
                            }
                            else
                            {
                                objdt.Columns[8].ColumnName = "dPEUnits";
                            }

                        }

                        ////******commented as per change request discussion with julie. email ref dt: 04 dec 2015 sub: RVU- I could use your help please. 

                        //else if (ImportPEId == 3)//Fully Implemented, Non-Facility here for PE units col [I i.e. FULLY IMPLEMENTED NON-FAC PE RVU] is read from excel.
                        //{
                        //    objdt.Columns[8].ColumnName = "dPEUnits";
                        //}
                        //else if (ImportPEId == 4)//Fully Implemented, Facility here for PE units col [M i.e. FULLY IMPLEMENTED FACILITY PE RVU] is read from excel.
                        //{
                        //    objdt.Columns[12].ColumnName = "dPEUnits";
                        //}
                        ////******

                        //following is new logic to import coloum after discussion with julie. email ref dt: 04 dec 2015 sub: RVU- I could use your help please. 
                        //as per julie use given excel for implementation...\\glosvr01\gloDocuments\gloSuite 8060\gloReports\RVU Project\PPRRVU15_V1223c.xlsx
                        else if (ImportPEId == 3)//Fully Implemented, Non-Facility here for PE units col [G] is read from excel.
                        {
                            objdt.Columns[6].ColumnName = "dPEUnits";
                        }
                        else if (ImportPEId == 4)//Fully Implemented, Facility here for PE units col [I] is read from excel.
                        {
                            objdt.Columns[8].ColumnName = "dPEUnits";
                        }


                        objdt.AcceptChanges();
                    }
                    //SLR: Logic problem to be changed on 2/4/2014
                    for (int i = 0; i <= objdt.Columns.Count - 1; i++)
                    {
                        if (objdt.Columns[i].ColumnName != "sCPTCode" && objdt.Columns[i].ColumnName != "sCPTDescription" && objdt.Columns[i].ColumnName != "dWorkUnits" && objdt.Columns[i].ColumnName != "dMPUnits" && objdt.Columns[i].ColumnName != "dPEUnits" && objdt.Columns[i].ColumnName != "sModifier" && objdt.Columns[i].ColumnName != "nRVUID" && objdt.Columns[i].ColumnName != "nRVUDtlID")
                        {
                            objdt.Columns.RemoveAt(i);
                            i--;
                            objdt.AcceptChanges();
                        }
                    }
                    objdt.AcceptChanges();


                    DataView dv = new DataView(objdt);

                    //IsNull(sModifier, '') validation skipped because from 8060 we need to consider modifier while importing and showing on Productivity by RVU report
                    dv.RowFilter = "IsNull(sCPTDescription,'') <> '' ";//"IsNull(sModifier, '') = '' AND IsNull(sCPTDescription,'') <> '' ";
                    objdt = dv.ToTable();
                    //objdt.Columns.Remove("sModifier");//modifier needs to be considered from 8060 onwards
                    objdt.Columns["nRVUID"].SetOrdinal(0);
                    objdt.Columns["nRVUDtlID"].SetOrdinal(1);
                    int col_Ordinal = objdt.Columns["dPEUnits"].Ordinal;
                    objdt.Columns["dPEUnits"].SetOrdinal(objdt.Columns["dMPUnits"].Ordinal);
                    objdt.Columns["dMPUnits"].SetOrdinal(col_Ordinal);
                    objdt.Columns.Add("dTotalRVU");
                    objdt.AcceptChanges();
                }
                else
                {
                    strMsg = "Invalid file Format";
                    return null;
                }

            }
            catch
            {
                if (objCmdSelect != null) { objCmdSelect.Dispose(); }
                if (objAdapter != null) { objAdapter.Dispose(); }
                if (conn != null) { conn.Close(); conn.Dispose(); }
                strMsg = "Invalid file Format";
                objdt = null;
            }
            return objdt;
        }

        #endregion


        private Boolean SaveSchedule()
        {
            Boolean _bResult = false;

            try
            {
                DataTable dtFinalData = null;
                c1RVUSchedule.FinishEditing();
                if (c1RVUSchedule.DataSource != null)
                {
                    dtFinalData = ((DataView)c1RVUSchedule.DataSource).ToTable();
                }
                else
                {
                    MessageBox.Show("Add Record to save. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                //Commeted the ValidateRVUSchedule(dtFinalData) functionality since from 8060 it is decided to import modifiers, due to which duplicate CPT codes will also get imported from old as well as new RVU file format. 
                //if (ValidateRVUSchedule(dtFinalData) == true)
                //{
                if (_nRVUId == 0)
                {
                    _bResult = SaveRVUSchedule(dtFinalData);
                }
                else
                {
                    _bResult = ModifySchedule(dtFinalData);
                }
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                _bResult = false;
            }
            return _bResult;
        }

        public decimal FormatNumber(decimal Number)
        {
            Decimal _result = Number;
            try
            {
                String[] no = _result.ToString().Split('.');
                if (no.GetUpperBound(0) > 0)
                {
                    if (no[1].ToString().Length > 4)
                    {
                        no[1] = no[1].Substring(0, 4);
                    }
                    _result = Convert.ToDecimal(no[0] + "." + no[1]);
                }
                _result = Convert.ToDecimal(_result.ToString("####0.##"));

            }
            catch
            {
                _result = Number;
            }
            return _result;
        }

        private Boolean CPTCodeBlankValidation(int RowNo, int ColNo)
        {
            bool IsCPTBlank = false;
            try
            {
                if (ColNo == COL_MOD || ColNo == COL_PE_UNITS || ColNo == COL_WORK_UNITS || ColNo == COL_PM_UNITS || ColNo == COL_TOTALRVU_UNITS)
                {
                    int i = 0;
                    string sCptCode = string.Empty;
                    if (c1RVUSchedule.Rows.Count > 1)
                    {
                        i = c1RVUSchedule.FindRow(null, 1, COL_CPT_CODE, true);
                        if (i > 0)
                        {
                            c1RVUSchedule.Select(i, COL_CPT_CODE, true);
                            sCptCode = "";

                        }
                        else
                        {
                            sCptCode = c1RVUSchedule.GetData(RowNo, COL_CPT_CODE).ToString();
                        }


                    }
                    //first check for cpt code coloumn is not not blank before editing modifier.
                    if (sCptCode == "")
                    {
                        MessageBox.Show("CPT code cannot be blank.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1RVUSchedule.Select(c1RVUSchedule.RowSel, COL_CPT_CODE, true);
                        IsCPTBlank = true;
                        return IsCPTBlank;
                    }
                }

                return IsCPTBlank;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return false;
            }

        }

        #endregion

        #region "Column Declaration"

        private const int COL_RVUID = 0;
        private const int COL_RVUDtlID = 1;
        private const int COL_CPT_CODE = 2;
        private const int COL_CPT_DESC = 3;
        private const int COL_MOD = 4;
        private const int COL_WORK_UNITS = 5;
        private const int COL_PE_UNITS = 6;
        private const int COL_PM_UNITS = 7;
        private const int COL_TOTALRVU_UNITS = 8;

        private const int COL_RVU_COUNT = 9;
        public gloGridListControl ogloGridListControl = null;
        //  private string[] strSearchArray;
        private Int64 _nRVUId = 0;


        #endregion

        #region Constructor

        public frm_SetupRVUSchedule(Int64 nRVUId, string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
            _nRVUId = nRVUId;

        }

        #endregion

        #region "Form Load Events"

        private void frm_SetupRVUSchedule_Load(object sender, EventArgs e)
        {

            gloC1FlexStyle.Style(c1RVUSchedule, false);
            c1RVUSchedule.Cols[COL_WORK_UNITS].Format = "####0.##";
            c1RVUSchedule.Cols[COL_PE_UNITS].Format = "####0.##";
            c1RVUSchedule.Cols[COL_PM_UNITS].Format = "####0.##";
            c1RVUSchedule.Cols[COL_TOTALRVU_UNITS].Format = "####0.##";
            if (_nRVUId != 0)
            {
                tsb_ImportCSV.Enabled = false;
                ts_btnSave.Visible = false;
                fillRVUSchedule(_nRVUId);
            }

            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            tom.SetTabOrder(scheme);
            dtpEffectivedate.Focus();

        }

        #endregion

        #region " User Control Events "

        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {

                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }
                if (ogloGridListControl != null)
                {
                    try
                    {
                        ogloGridListControl.ItemSelected -= new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                        ogloGridListControl.InternalGridKeyDown -= new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                    }
                    catch { }
                    ogloGridListControl.Dispose(); ogloGridListControl = null;
                }
                pnlInternalControl.Visible = false;
                //pnlInternalControl.SendToBack();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                _result = false;
            }
            finally
            { }
            return _result;
        }

        private void RePositionInternalControl()
        {
            try
            {
                if (c1RVUSchedule.Parent.Bottom - c1RVUSchedule.Rows[c1RVUSchedule.RowSel].Bottom - 150 < pnlInternalControl.Height)
                {
                    pnlInternalControl.SetBounds((c1RVUSchedule.Cols[c1RVUSchedule.ColSel].Left + c1RVUSchedule.ScrollPosition.X), (c1RVUSchedule.Rows[c1RVUSchedule.RowSel].Top - pnlInternalControl.Height) + c1RVUSchedule.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);

                }
                else
                {
                    pnlInternalControl.SetBounds(c1RVUSchedule.Cols[c1RVUSchedule.ColSel].Left, c1RVUSchedule.Rows[c1RVUSchedule.RowSel].Bottom + c1RVUSchedule.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            try
            {

                if (ogloGridListControl != null)
                {
                    CloseInternalControl();
                }
                ogloGridListControl = new gloGridListControl(ControlType, false, pnlInternalControl.Width, RowIndex, ColIndex);
                ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                ogloGridListControl.ControlHeader = ControlHeader;
                pnlInternalControl.Controls.Add(ogloGridListControl);
                ogloGridListControl.Dock = DockStyle.Fill;
                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, SearchColumn.Code);
                }
                ogloGridListControl.Show();

                int _x = c1RVUSchedule.Cols[ColIndex].Left;
                int _y = c1RVUSchedule.Rows[RowIndex].Bottom;
                int _width = pnlInternalControl.Width;
                int _height = pnlInternalControl.Height;



                int _parentleft = pnlInternalControl.Parent.Bounds.Left;
                int _parentwidth = pnlInternalControl.Parent.Bounds.Width;
                int _diffFactor = _parentwidth - _x;

                if (_diffFactor < _width)
                {
                    _x = pnlInternalControl.Parent.Bounds.Width + (_diffFactor);
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                else
                {
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }

                //pnlInternalControl.SetBounds(c1RVUSchedule.Cols[ColIndex].Left, _y + c1RVUSchedule.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                pnlInternalControl.Visible = true;
                //pnlInternalControl.BringToFront();
                ogloGridListControl.Focus();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                _result = false;
            }
            finally
            {
                RePositionInternalControl();
            }
            return _result;
        }

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {

            #region "Custom Event"
            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            #endregion

            try
            {

                int _rowIndex = 0;
                switch (c1RVUSchedule.ColSel)
                {
                    case COL_CPT_CODE:
                        if (ogloGridListControl.SelectedItems != null && ogloGridListControl.SelectedItems.Count > 0)
                        {
                            int nCurrentRowIndex = c1RVUSchedule.RowSel;

                            //...Check if code exists
                            bool _isExistsCode = false;
                            string sCPTCode = string.Empty;
                            string sModifier = string.Empty;
                            if (c1RVUSchedule != null && c1RVUSchedule.Rows.Count > 1)
                            {
                                for (int rIndex = 1; rIndex < c1RVUSchedule.Rows.Count; rIndex++)
                                {
                                    if (rIndex != ogloGridListControl.ParentRowIndex)
                                    {
                                        //if (c1RVUSchedule.GetData(rIndex, COL_CPT_CODE) != null && Convert.ToString(c1RVUSchedule.GetData(rIndex, COL_CPT_CODE)).Trim() != "")
                                        //{
                                        //    sCPTCode = c1RVUSchedule.GetData(rIndex, COL_CPT_CODE).ToString();
                                        //}

                                        //if (c1RVUSchedule.GetData(rIndex, COL_MOD) != null && Convert.ToString(c1RVUSchedule.GetData(rIndex, COL_MOD)).Trim() != "")
                                        //{
                                        //    sCPTCode = c1RVUSchedule.GetData(rIndex, COL_MOD).ToString();
                                        //}
                                        //if (c1RVUSchedule.GetData(rIndex, COL_CPT_CODE) != null && Convert.ToString(c1RVUSchedule.GetData(rIndex, COL_CPT_CODE)).Trim() != ""
                                        //    && Convert.ToString(c1RVUSchedule.GetData(rIndex, COL_CPT_CODE)).Trim().ToUpper() == ogloGridListControl.SelectedItems[0].Code.Trim().ToUpper())
                                        //{
                                        //    _isExistsCode = true;
                                        //    break;
                                        //}
                                    }
                                }
                            }

                            if (_isExistsCode == false)
                            {
                                if (c1RVUSchedule.DataSource != null)
                                {
                                    _rowIndex = ogloGridListControl.ParentRowIndex;
                                    c1RVUSchedule.SetData(c1RVUSchedule.RowSel, COL_CPT_CODE, ogloGridListControl.SelectedItems[0].Code.Trim());
                                    c1RVUSchedule.SetData(c1RVUSchedule.RowSel, COL_CPT_DESC, ogloGridListControl.SelectedItems[0].Description.Trim());
                                    if (_nRVUId > 0)
                                    {
                                        c1RVUSchedule.SetData(c1RVUSchedule.RowSel, COL_RVUID, _nRVUId);
                                        c1RVUSchedule.SetData(c1RVUSchedule.RowSel, COL_RVUDtlID, 0);
                                    }

                                    //c1RVUSchedule.Select(c1RVUSchedule.RowSel, COL_WORK_UNITS, true);//commented for RVU modifier changes in 8060.
                                    c1RVUSchedule.Select(c1RVUSchedule.RowSel, COL_MOD, true);
                                }
                                else
                                {
                                    DataTable dt = new DataTable();
                                    DataColumn nRVUID = new DataColumn();
                                    nRVUID.DataType = System.Type.GetType("System.Int64");
                                    nRVUID.DefaultValue = 0;
                                    nRVUID.ColumnName = "nRVUID";
                                    nRVUID.Caption = "nRVUID";

                                    DataColumn nRVUDtlID = new DataColumn();
                                    nRVUDtlID.DataType = System.Type.GetType("System.Int64");
                                    nRVUDtlID.DefaultValue = 0;
                                    nRVUDtlID.ColumnName = "nRVUDtlID";
                                    nRVUDtlID.Caption = "nRVUDtlID";

                                    dt.Columns.Add(nRVUID);
                                    dt.Columns.Add(nRVUDtlID);
                                    dt.Columns.Add("sCPTCode");
                                    dt.Columns.Add("sModifier");
                                    dt.Columns.Add("sCPTDescription");
                                    dt.Columns.Add("dWorkUnits");
                                    dt.Columns.Add("dMPUnits");
                                    dt.Columns.Add("dPEUnits");
                                    dt.Columns.Add("dTotalRVU");
                                    dt.Rows.Add();
                                    dt.Rows[dt.Rows.Count - 1]["sCPTCode"] = ogloGridListControl.SelectedItems[0].Code.Trim();
                                    dt.Rows[dt.Rows.Count - 1]["sCPTDescription"] = ogloGridListControl.SelectedItems[0].Description.Trim();
                                    dt.Rows[dt.Rows.Count - 1]["sModifier"] = "";//since we are entering data in CPT coloumn
                                    dt.Rows[dt.Rows.Count - 1]["dWorkUnits"] = (Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_WORK_UNITS)) == string.Empty ? null : string.Format("{0:####0.##}", c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_WORK_UNITS)));
                                    dt.Rows[dt.Rows.Count - 1]["dMPUnits"] = (Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PM_UNITS)) == string.Empty ? null : string.Format("{0:####0.##}", c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PM_UNITS)));
                                    dt.Rows[dt.Rows.Count - 1]["dPEUnits"] = (Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PE_UNITS)) == string.Empty ? null : string.Format("{0:####0.##}", c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PE_UNITS)));
                                    dt.Rows[dt.Rows.Count - 1]["dTotalRVU"] = (Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_TOTALRVU_UNITS)) == string.Empty ? null : string.Format("{0:####0.##}", c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_TOTALRVU_UNITS)));
                                    if (_nRVUId > 0)
                                    {
                                        dt.Rows[dt.Rows.Count - 1]["nRVUID"] = _nRVUId;
                                    }
                                    dt.AcceptChanges();
                                    c1RVUSchedule.DataSource = dt.DefaultView;
                                    c1RVUSchedule.Select(c1RVUSchedule.RowSel, COL_MOD, true);
                                }

                            }
                            else
                            {
                                MessageBox.Show("CPT Code already exists.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _rowIndex = ogloGridListControl.ParentRowIndex;
                                c1RVUSchedule.SetData(_rowIndex, COL_CPT_DESC, null);
                                c1RVUSchedule.SetData(_rowIndex, COL_CPT_CODE, null);
                                c1RVUSchedule.Select(_rowIndex, COL_CPT_CODE, true);
                            }
                        }
                        else
                        {
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            c1RVUSchedule.SetData(_rowIndex, COL_CPT_CODE, null);
                            c1RVUSchedule.SetData(_rowIndex, COL_CPT_DESC, null);
                            c1RVUSchedule.Focus();
                            c1RVUSchedule.Select(_rowIndex, COL_CPT_CODE, true);
                            //c1RVUSchedule.Select(_rowIndex, COL_MAPCPTCODE, true);

                        }
                        break;

                    case COL_MOD:
                        if (ogloGridListControl.SelectedItems != null && ogloGridListControl.SelectedItems.Count > 0)
                        {
                            int nCurrentRowIndex = c1RVUSchedule.RowSel;

                            //...Check if code exists
                            bool _isExistsCode = false;
                            string sCPTCode = string.Empty;
                            string sModifier = string.Empty;
                            //if (c1RVUSchedule != null && c1RVUSchedule.Rows.Count > 1)
                            //{
                            //    for (int rIndex = 1; rIndex < c1RVUSchedule.Rows.Count; rIndex++)
                            //    {
                            //        if (rIndex != ogloGridListControl.ParentRowIndex)
                            //        {
                            //            if (c1RVUSchedule.GetData(rIndex, COL_CPT_CODE) != null && Convert.ToString(c1RVUSchedule.GetData(rIndex, COL_CPT_CODE)).Trim() != "")
                            //            {
                            //                sCPTCode = c1RVUSchedule.GetData(rIndex, COL_CPT_CODE).ToString();
                            //            }

                            //            if (c1RVUSchedule.GetData(rIndex, COL_MOD) != null && Convert.ToString(c1RVUSchedule.GetData(rIndex, COL_MOD)).Trim() != "")
                            //            {
                            //                sModifier = c1RVUSchedule.GetData(rIndex, COL_MOD).ToString();
                            //            }
                            //            if ((sCPTCode != "" || sModifier != "") && ((sCPTCode.ToUpper() == ogloGridListControl.SelectedItems[0].Code.Trim().ToUpper() || sModifier.ToUpper() == ogloGridListControl.SelectedItems[0].Code.Trim().ToUpper())))
                            //            {
                            //                _isExistsCode = true;
                            //                break;
                            //            }
                            //        }
                            //    }
                            //}

                            if (_isExistsCode == false)
                            {
                                if (c1RVUSchedule.DataSource != null)
                                {
                                    _rowIndex = ogloGridListControl.ParentRowIndex;
                                    c1RVUSchedule.SetData(c1RVUSchedule.RowSel, COL_MOD, ogloGridListControl.SelectedItems[0].Code.Trim());
                                    //c1RVUSchedule.SetData(c1RVUSchedule.RowSel, COL_CPT_DESC, ogloGridListControl.SelectedItems[0].Description.Trim());
                                    if (_nRVUId > 0)
                                    {
                                        c1RVUSchedule.SetData(c1RVUSchedule.RowSel, COL_RVUID, _nRVUId);
                                        c1RVUSchedule.SetData(c1RVUSchedule.RowSel, COL_RVUDtlID, 0);
                                    }

                                    c1RVUSchedule.Select(c1RVUSchedule.RowSel, COL_WORK_UNITS, true);

                                }
                                else
                                {
                                    DataTable dt = new DataTable();
                                    DataColumn nRVUID = new DataColumn();
                                    nRVUID.DataType = System.Type.GetType("System.Int64");
                                    nRVUID.DefaultValue = 0;
                                    nRVUID.ColumnName = "nRVUID";
                                    nRVUID.Caption = "nRVUID";

                                    DataColumn nRVUDtlID = new DataColumn();
                                    nRVUDtlID.DataType = System.Type.GetType("System.Int64");
                                    nRVUDtlID.DefaultValue = 0;
                                    nRVUDtlID.ColumnName = "nRVUDtlID";
                                    nRVUDtlID.Caption = "nRVUDtlID";

                                    dt.Columns.Add(nRVUID);
                                    dt.Columns.Add(nRVUDtlID);
                                    dt.Columns.Add("sCPTCode");
                                    dt.Columns.Add("sModifier");
                                    dt.Columns.Add("sCPTDescription");
                                    dt.Columns.Add("dWorkUnits");
                                    dt.Columns.Add("dMPUnits");
                                    dt.Columns.Add("dPEUnits");
                                    dt.Columns.Add("dTotalRVU");
                                    dt.Rows.Add();
                                    dt.Rows[dt.Rows.Count - 1]["sCPTCode"] = "";//since we are in modifier coloumn
                                    dt.Rows[dt.Rows.Count - 1]["sCPTDescription"] = "";//since we are in modifier coloumn
                                    dt.Rows[dt.Rows.Count - 1]["sModifier"] = ogloGridListControl.SelectedItems[0].Code.Trim();
                                    dt.Rows[dt.Rows.Count - 1]["dWorkUnits"] = (Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_WORK_UNITS)) == string.Empty ? null : string.Format("{0:####0.##}", c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_WORK_UNITS)));
                                    dt.Rows[dt.Rows.Count - 1]["dMPUnits"] = (Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PM_UNITS)) == string.Empty ? null : string.Format("{0:####0.##}", c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PM_UNITS)));
                                    dt.Rows[dt.Rows.Count - 1]["dPEUnits"] = (Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PE_UNITS)) == string.Empty ? null : string.Format("{0:####0.##}", c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_PE_UNITS)));
                                    dt.Rows[dt.Rows.Count - 1]["dTotalRVU"] = (Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_TOTALRVU_UNITS)) == string.Empty ? null : string.Format("{0:####0.##}", c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_TOTALRVU_UNITS)));
                                    if (_nRVUId > 0)
                                    {
                                        dt.Rows[dt.Rows.Count - 1]["nRVUID"] = _nRVUId;
                                    }
                                    dt.AcceptChanges();
                                    c1RVUSchedule.DataSource = dt.DefaultView;
                                    c1RVUSchedule.Select(c1RVUSchedule.RowSel, COL_WORK_UNITS, true);
                                }

                            }
                            else
                            {
                                MessageBox.Show("CPT Code along with modifier combination already exists.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _rowIndex = ogloGridListControl.ParentRowIndex;
                                c1RVUSchedule.SetData(_rowIndex, COL_CPT_DESC, null);
                                c1RVUSchedule.SetData(_rowIndex, COL_CPT_CODE, null);
                                c1RVUSchedule.Select(_rowIndex, COL_CPT_CODE, true);
                            }
                        }
                        else
                        {
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            switch (c1RVUSchedule.ColSel)
                            {
                                case COL_CPT_CODE:
                                    c1RVUSchedule.SetData(_rowIndex, COL_CPT_CODE, null);
                                    c1RVUSchedule.SetData(_rowIndex, COL_CPT_DESC, null);
                                    c1RVUSchedule.Focus();
                                    c1RVUSchedule.Select(_rowIndex, COL_CPT_CODE, true);
                                    break;
                                case COL_MOD:
                                    c1RVUSchedule.SetData(_rowIndex, COL_MOD, null);
                                    c1RVUSchedule.Focus();
                                    c1RVUSchedule.Select(_rowIndex, COL_MOD, true);
                                    break;
                            }


                            //c1RVUSchedule.Select(_rowIndex, COL_MAPCPTCODE, true);

                        }
                        break;

                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);

            }
            finally
            {
                CloseInternalControl();

                c1RVUSchedule.Cols[COL_WORK_UNITS].Format = "####0.##";
                c1RVUSchedule.Cols[COL_PE_UNITS].Format = "####0.##";
                c1RVUSchedule.Cols[COL_PM_UNITS].Format = "####0.##";
                c1RVUSchedule.Cols[COL_TOTALRVU_UNITS].Format = "####0.##";
            }
        }

        void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                CloseInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
            }
            finally
            { }
        }


        #endregion

        #region " C1 Grid Events "

        private void c1RVUSchedule_KeyUp(object sender, KeyEventArgs e)
        {
            //int _id = 0;
            string _code = "";
            string _description = "";
            bool _isdeleted = true;


            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            RowColEventArgs e1 = null;

            try
            {

                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    #region "Enter Key"

                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            bool _IsItemSelected = ogloGridListControl.GetCurrentSelectedItem();
                            if (_IsItemSelected)
                            {
                            }
                        }
                    }


                    #endregion
                }
                else if (e.KeyCode == Keys.Down)
                {
                    e.SuppressKeyPress = true;
                    #region "Down Key"
                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            ogloGridListControl.Focus();
                        }
                    }
                    #endregion
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    #region "Escape Key"
                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            CloseInternalControl();

                            if (c1RVUSchedule.RowSel > 0)
                            {

                            }
                        }
                    }
                    #endregion
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    //CellNote oCellNotes = null;

                    if (c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_CPT_CODE) != null)
                    {
                        _code = c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_CPT_CODE).ToString().Trim();
                    }
                    if (c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_CPT_DESC) != null)
                    {
                        _description = c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_CPT_DESC).ToString().Trim();
                    }



                    e2.oType = TransactionLineColumnType.None;

                    e.SuppressKeyPress = true;

                    #region "Delete Key"
                    switch (c1RVUSchedule.ColSel)
                    {

                        case COL_CPT_CODE:
                            {

                                c1RVUSchedule.SetData(c1RVUSchedule.RowSel, c1RVUSchedule.ColSel, "");
                                c1RVUSchedule.SetData(c1RVUSchedule.RowSel, c1RVUSchedule.ColSel + 1, "");

                                //CellRange rg = c1RVUSchedule.GetCellRange(c1RVUSchedule.RowSel, c1RVUSchedule.ColSel);
                                //rg.UserData = oCellNotes;
                                e2.oType = TransactionLineColumnType.CPT;

                            }
                            break;
                        case COL_PE_UNITS:
                            {
                                c1RVUSchedule.SetData(c1RVUSchedule.RowSel, c1RVUSchedule.ColSel, null);
                            }
                            break;
                        case COL_PM_UNITS:
                            {

                                c1RVUSchedule.SetData(c1RVUSchedule.RowSel, c1RVUSchedule.ColSel, null);
                            }
                            break;
                        case COL_WORK_UNITS:
                            {
                                c1RVUSchedule.SetData(c1RVUSchedule.RowSel, c1RVUSchedule.ColSel, null);
                            }
                            break;
                        case COL_MOD:
                            {
                                c1RVUSchedule.SetData(c1RVUSchedule.RowSel, c1RVUSchedule.ColSel, null);
                            }
                            break;
                    }
                    _code = "";
                    e1 = new RowColEventArgs(c1RVUSchedule.RowSel, c1RVUSchedule.ColSel);
                    e2.code = _code;
                    e2.description = _description;
                    e2.isdeleted = true;


                    e2.code = _code;
                    e2.description = _description;
                    e2.isdeleted = _isdeleted;

                    getTotalRVU();
                    #endregion
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);

            }
            finally
            {
            }
        }

        private void c1RVUSchedule_BeforeSelChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (ogloGridListControl != null)
                {
                    if (e.OldRange.r1 != e.NewRange.r1)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private void c1RVUSchedule_LeaveEdit(object sender, RowColEventArgs e)
        {

            try
            {
                switch (e.Col)
                {
                    case COL_CPT_CODE:
                        if (c1RVUSchedule.Editor != null)
                        {
                            c1RVUSchedule.ChangeEdit -= new System.EventHandler(this.c1RVUSchedule_ChangeEdit);
                            c1RVUSchedule.Editor.Text = "";
                            c1RVUSchedule.ChangeEdit += new System.EventHandler(this.c1RVUSchedule_ChangeEdit);

                        }

                        break;
                    case COL_MOD:

                        if (c1RVUSchedule.Editor != null)
                        {
                            c1RVUSchedule.ChangeEdit -= new System.EventHandler(this.c1RVUSchedule_ChangeEdit);
                            c1RVUSchedule.Editor.Text = "";
                            c1RVUSchedule.ChangeEdit += new System.EventHandler(this.c1RVUSchedule_ChangeEdit);

                        }

                        break;


                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        private void c1RVUSchedule_ChangeEdit(object sender, EventArgs e)
        {
            string _strSearchString = "";
            try
            {
                _strSearchString = c1RVUSchedule.Editor.Text;

                if (ogloGridListControl != null)
                {

                    if (c1RVUSchedule.Col == COL_CPT_CODE || c1RVUSchedule.Col == COL_MOD)
                    {
                        string _COL_CODE = "";
                        string _COL_DESC = "";

                        if (c1RVUSchedule != null && c1RVUSchedule.Rows.Count > 0)
                        {
                            if (c1RVUSchedule.Col == COL_CPT_CODE)
                            {
                                _COL_CODE = Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.Row, COL_CPT_CODE));
                                _COL_DESC = Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.Row, COL_CPT_DESC));
                            }
                            else
                            {
                                _COL_CODE = Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.Row, COL_MOD));
                                //_COL_DESC = Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.Row, COL_CPT_DESC));
                            }

                            ogloGridListControl.SelectedCPTCode = _strSearchString;

                        }


                        ogloGridListControl.FillControl(_strSearchString);
                        if (_strSearchString != "" && ogloGridListControl != null)
                        {
                            ogloGridListControl.AdvanceSearch(_strSearchString);
                        }
                    }



                    ogloGridListControl.FillControl(_strSearchString);
                    if (_strSearchString != "" && ogloGridListControl != null)
                    {
                        ogloGridListControl.AdvanceSearch(_strSearchString);
                    }
                }
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);

            }
            finally
            {
            }

        }

        private void c1RVUSchedule_MouseMove(object sender, MouseEventArgs e)
        {
            if (c1RVUSchedule.Rows.Count > 1)
            {
                if (c1RVUSchedule.HitTest(e.X, e.Y).Column == COL_CPT_CODE)
                {
                    gloC1FlexStyle.ShowToolTipForBillingServiceLine(C1SuperTooltip1, (C1FlexGrid)sender, e.Location, true);
                }
                else
                {
                    gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
                }
            }
        }

        private void c1RVUSchedule_SetupEditor(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col == COL_PE_UNITS || e.Col == COL_PM_UNITS || e.Col == COL_WORK_UNITS)
            {
                ((TextBox)c1RVUSchedule.Editor).MaxLength = 8;
            }
            else if (e.Col == COL_CPT_CODE)
            {
                ((TextBox)c1RVUSchedule.Editor).MaxLength = 50;
            }
        }

        private void c1RVUSchedule_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                if (((e.OldRange.c1 == COL_CPT_CODE) && (e.NewRange.c1 != COL_CPT_CODE)) || ((e.OldRange.c1 == COL_MOD) && (e.NewRange.c1 != COL_MOD)))
                { CloseInternalControl(); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
            finally
            {

            }
        }

        private void c1RVUSchedule_StartEdit(object sender, RowColEventArgs e)
        {
            switch (c1RVUSchedule.ColSel)
            {
                case COL_CPT_CODE://Col no 2....open cpt codes and description
                    if (e.Col == COL_CPT_CODE)
                    {
                        OpenInternalControl(gloGridListControlType.CPT, "CPT Code", false, e.Row, e.Col, "");
                        string _SearchText = "";
                        if (c1RVUSchedule != null && c1RVUSchedule.Rows.Count > 0)
                        {
                            _SearchText = Convert.ToString(c1RVUSchedule.GetData(e.Row, COL_CPT_CODE));
                            if (_SearchText != "" && ogloGridListControl != null)
                            {
                                ogloGridListControl.FillControl(_SearchText);
                            }
                        }
                    }
                    break;
                case COL_MOD://col no 4...open modifier codes and description


                    if (e.Col == COL_MOD)
                    {


                        //first check for cpt code coloumn is not not blank before editing modifier.sCptCode == ""
                        if (CPTCodeBlankValidation(e.Row, COL_MOD) == true)
                        {

                            //do nothing...validation message alreay given in function.

                        }
                        else
                        {
                            OpenInternalControl(gloGridListControlType.Modifier, "Modifier", false, e.Row, e.Col, "");
                            string _SearchText = "";
                            if (c1RVUSchedule != null && c1RVUSchedule.Rows.Count > 0)
                            {
                                _SearchText = Convert.ToString(c1RVUSchedule.GetData(e.Row, COL_MOD));
                                if (_SearchText != "" && ogloGridListControl != null)
                                {
                                    ogloGridListControl.FillControl(_SearchText);
                                }
                            }
                        }

                    }
                    break;
                case COL_PE_UNITS:

                    //check is cpt code blank validation
                    CPTCodeBlankValidation(e.Row, COL_PE_UNITS);

                    break;
                case COL_WORK_UNITS:
                    //check is cpt code blank validation
                    CPTCodeBlankValidation(e.Row, COL_WORK_UNITS);
                    break;

                case COL_PM_UNITS:
                    //check is cpt code blank validation
                    CPTCodeBlankValidation(e.Row, COL_PM_UNITS);
                    break;

                case COL_TOTALRVU_UNITS:
                    //check is cpt code blank validation
                    CPTCodeBlankValidation(e.Row, COL_TOTALRVU_UNITS);
                    break;

            }


            c1RVUSchedule.Editor = (TextBox)c1RVUSchedule.Editor;
        }


        private void c1RVUSchedule_KeyDownEdit(object sender, KeyEditEventArgs e)
        {
            #region "Numeric Validation"
            if (c1RVUSchedule.ColSel == COL_PE_UNITS || c1RVUSchedule.ColSel == COL_PM_UNITS || c1RVUSchedule.ColSel == COL_WORK_UNITS)
            {
                if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
                {
                    e.Handled = true;
                }

            }
            #endregion
        }

        private void c1RVUSchedule_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            #region "Numeric Validation"
            if (c1RVUSchedule.ColSel == COL_PE_UNITS || c1RVUSchedule.ColSel == COL_PM_UNITS || c1RVUSchedule.ColSel == COL_WORK_UNITS)
            {
                if (e.KeyChar == Convert.ToChar("-"))
                {
                    e.Handled = true;
                }

            }
            #endregion
        }

        private void c1RVUSchedule_AfterEdit(object sender, RowColEventArgs e)
        {
            c1RVUSchedule.FinishEditing();
            try
            {
                if (e.Col == COL_CPT_CODE)  //Check for CPT CODE if blank then change CPT DEsc to blank
                {
                    if (c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_CPT_CODE) != null)
                    {
                        if (Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_CPT_CODE)) == "")
                        {
                            c1RVUSchedule.SetData(c1RVUSchedule.RowSel, COL_CPT_DESC, "");
                        }
                    }
                }
                else if (e.Col == COL_WORK_UNITS || e.Col == COL_PE_UNITS || e.Col == COL_PM_UNITS)
                {
                    if (c1RVUSchedule.GetData(c1RVUSchedule.RowSel, e.Col) != null)
                    {
                        if (Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, e.Col)) != "")
                        {
                            c1RVUSchedule.SetData(c1RVUSchedule.RowSel, e.Col, FormatNumber(Convert.ToDecimal(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, e.Col))));
                        }
                    }
                }
                else if (e.Col == COL_MOD)
                {
                    if (c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_CPT_CODE) != null)
                    {
                        if (Convert.ToString(c1RVUSchedule.GetData(c1RVUSchedule.RowSel, COL_CPT_CODE)) == "")
                        {
                            c1RVUSchedule.SetData(c1RVUSchedule.RowSel, COL_CPT_DESC, "");
                        }
                    }
                }
                getTotalRVU();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex = null;
            }
        }

        #endregion " C1 Grid Events "

        #region " Form Control Events "

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() != string.Empty)
            {
                var filter = new ConditionFilter();
                filter.AndConditions = false;
                c1RVUSchedule.Cols["sCPTCode"].AllowFiltering = AllowFiltering.ByCondition;
                filter.Condition1.Operator = ConditionOperator.BeginsWith;
                filter.Condition1.Parameter = "";
                filter.Condition1.Parameter = Convert.ToString(txtSearch.Text);
                c1RVUSchedule.Cols["sCPTCode"].Filter = filter;
            }
            else
            {
                c1RVUSchedule.Cols["sCPTCode"].Filter = null;
            }

            if (chkHideZeroRVU.Checked)
            {
                var filterZero = new ConditionFilter();
                c1RVUSchedule.Cols["dTotalRVU"].AllowFiltering = AllowFiltering.ByCondition;
                filterZero.Condition1.Operator = ConditionOperator.GreaterThan;
                filterZero.Condition1.Parameter = 0;
                //c1RVUSchedule.Cols["dTotalRVU"].DataType = System.Type.GetType("System.Decimal");
                c1RVUSchedule.Cols["dTotalRVU"].Filter = filterZero;
                //c1RVUSchedule.Cols["dTotalRVU"].DataType = System.Type.GetType("System.string");
            }
            else
            {

                c1RVUSchedule.Cols["dTotalRVU"].Filter = null;

            }
            c1RVUSchedule.ApplyFilters();
        }

        private void chkHideZeroRVU_CheckedChanged(object sender, EventArgs e)
        {
            txtSearch_TextChanged(null, null);
        }

        #region " Menu events for shortcut keys"

        private void mnuFeeSchedule_AddLine_Click(object sender, EventArgs e)
        {
            ts_btnAddLine_Click(null, null);

        }

        private void mnuFeeSchedule_RemoveLine_Click(object sender, EventArgs e)
        {
            ts_btnRemoveLine_Click(null, null);

        }

        private void mnuFeeSchedule_Save_Click(object sender, EventArgs e)
        {
            ts_btnSaveCls_Click(null, null);

        }

        private void mnuFeeSchedule_Close_Click(object sender, EventArgs e)
        {
            ts_btnClose_Click(null, null);

        }
        #endregion

        #endregion

        #region " Tool strip Events  "

        private void ts_btnSave_Click(object sender, EventArgs e)
        {
            try
            {



                Cursor.Current = Cursors.WaitCursor;
                if (SaveSchedule())
                {
                    ResetFields();
                }
            }
            catch
            {
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ts_btnSaveCls_Click(object sender, EventArgs e)
        {
            DataTable dtExistingData = null;
            StringBuilder strbldr = null;
            bool blnSave = true;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (ogloGridListControl != null)
                {
                   
                    CloseInternalControl();
                    switch (c1RVUSchedule.ColSel)
                    {
                        case COL_CPT_CODE:
                            c1RVUSchedule.Select(c1RVUSchedule.RowSel, COL_MOD, true);
                            break;

                        case COL_MOD:
                            c1RVUSchedule.Select(c1RVUSchedule.RowSel, COL_WORK_UNITS, true);
                            break;
                    }

                }

                if (c1RVUSchedule.DataSource != null)
                {

                    dtExistingData = ((DataView)c1RVUSchedule.DataSource).ToTable();
                    for (int i = dtExistingData.Rows.Count; i > 0; i--)
                    {
                        if (dtExistingData.Rows[i - 1]["sCPTCode"].ToString() == "")
                        {
                            MessageBox.Show("CPT Code cannot be blank.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1RVUSchedule.Select(c1RVUSchedule.RowSel, COL_CPT_CODE, true);
                            blnSave = false;
                            break;
                        }


                    }
                    var groupedData = (from r in dtExistingData.AsEnumerable()
                                       group r by new { CPT = r.Field<string>("sCPTCode"), Mod = r.Field<string>("sModifier") }
                                           into dtGroup
                                           where dtGroup.Count() > 1
                                           select new
                                           {
                                               CPT = dtGroup.Key,
                                               DuplicateCount = dtGroup.Count()
                                           }).ToList();


                    if (groupedData.Count > 0)
                    {
                        strbldr = new StringBuilder();
                        for (int i = 0; i < groupedData.Count; i++)
                        {

                            string sCPTMOD = string.Empty;
                            string sCPTCode = string.Empty;
                            string sMod = string.Empty;
                            string sDupCnt = string.Empty;

                            sCPTCode = groupedData[i].CPT.CPT.ToString();
                            sDupCnt = groupedData[i].DuplicateCount.ToString();
                            //if (groupedData[i].CPT.Mod == null)
                            //{
                            //    sCPTMOD = sCPTCode ;
                            //}
                            //else
                            //{
                            //    sCPTMOD = sCPTCode + ", " + groupedData[i].CPT.Mod.ToString();

                            //}

                            //strbldr.Append(sCPTMOD + Environment.NewLine);
                            strbldr.Append(sCPTCode + Environment.NewLine);

                        }
                    }

                }


                if (strbldr != null)
                {
                    MessageBox.Show("following CPT codes are found to be duplicated. please add valid Modifier combination to CPT code or remove them from list." + Environment.NewLine + strbldr.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (blnSave == true)
                    {
                        if (SaveSchedule())
                        {
                            this.Close();

                        }
                    }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);

            }
            finally
            {
                Cursor.Current = Cursors.Default;
                if (strbldr != null)
                {
                    strbldr = null;
                }
                if (dtExistingData != null)
                {
                    dtExistingData.Dispose();
                    dtExistingData = null;
                }
            }

        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            _nRVUId = 0;
            this.Close();
        }

        private void ts_btnAddLine_Click(object sender, EventArgs e)
        {
            c1RVUSchedule.FinishEditing();
            if (c1RVUSchedule.Rows.Count > 0)
            {
                this.c1RVUSchedule.AfterRowColChange -= new C1.Win.C1FlexGrid.RangeEventHandler(this.c1RVUSchedule_AfterRowColChange);
                this.c1RVUSchedule.AfterEdit -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1RVUSchedule_AfterEdit);
                c1RVUSchedule.Select(c1RVUSchedule.RowSel, COL_CPT_CODE);
                this.c1RVUSchedule.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1RVUSchedule_AfterEdit);
                this.c1RVUSchedule.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1RVUSchedule_AfterRowColChange);
            }

            if (ValidateForCPT(c1RVUSchedule.RowSel, c1RVUSchedule.ColSel) == true)
            {
                AddLine();
            }

        }

        private void ts_btnRemoveLine_Click(object sender, EventArgs e)
        {

            if (c1RVUSchedule != null && c1RVUSchedule.Rows.Count > 1)
            {
                int rowIndex = c1RVUSchedule.RowSel;
                c1RVUSchedule.Rows.Remove(rowIndex);
                CloseInternalControl();
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupStandardFeeSchedule, ActivityType.Remove, "Remove Line", 0, rowIndex, 0, ActivityOutCome.Success);
            }

        }

        private void tsb_ImportCSV_Click(object sender, EventArgs e)
        {
            CloseInternalControl();
            DataTable dtImportData = new DataTable();
            frmImportRVUSchedule objfrmImportRVUSchedule = new frmImportRVUSchedule(_databaseconnectionstring);
            objfrmImportRVUSchedule.ShowDialog(this);
            try
            {
                if (objfrmImportRVUSchedule.FrmDlgRst == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    tsb_ImportCSV.Enabled = false;
                    DataTable dt = new DataTable();
                    if (objfrmImportRVUSchedule.ImportFileName.Trim() != "")
                    {
                        string DirectoryName = System.IO.Path.GetDirectoryName(objfrmImportRVUSchedule.ImportFileName);
                        ImportFileName = objfrmImportRVUSchedule.ImportFileName;
                        ImportPEId = objfrmImportRVUSchedule.ImportPEId;
                        ImportFileType = objfrmImportRVUSchedule.ImportFileType;
                        String sFileExt = ImportFileName.Trim().Substring(ImportFileName.Trim().LastIndexOf("."));

                        bool UseNPOIForExcelIntegration = GetExcelIntegartionMethod();
                        if (sFileExt.ToLower() == ".xlsx" || sFileExt.ToLower() == ".xls")
                        {
                            if (ImportFileType == "Standard")
                            {
                                string strMsg = "";
                                if (UseNPOIForExcelIntegration == true)
                                {
                                    dtImportData = GetExcelGlo_NPOI(ImportFileName, ref strMsg); //GetExcelGlo(ImportFileName, ref strMsg); //
                                }
                                else
                                {
                                    dtImportData = GetExcelGlo(ImportFileName, ref strMsg);
                                }
                                
                                if (dtImportData == null)
                                {
                                    MessageBox.Show(strMsg, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tsb_ImportCSV.Enabled = true;
                                    return;
                                }
                            }
                            else if (ImportFileType == "Custom")
                            {
                                string strMsg = "";
                                if (UseNPOIForExcelIntegration == true)
                                {
                                    dtImportData = GetExcel_NPOI(ImportFileName, ref strMsg);// GetExcel(ImportFileName, ref strMsg);// 
                                }
                                else
                                {
                                    dtImportData =  GetExcel(ImportFileName, ref strMsg);
                                }
                               
                                if (dtImportData == null)
                                {
                                    MessageBox.Show(strMsg, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tsb_ImportCSV.Enabled = true;
                                    return;
                                }
                            }
                        }
                        else if (sFileExt.ToLower() == ".csv")
                        {
                            if (ImportFileType == "Custom")
                            {
                                //string strMsg = "";
                                //dtImportData = TransferCSVToTable(ImportFileName, ref strMsg);
                                //if (dtImportData == null)
                                //{
                                //    MessageBox.Show(strMsg, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    tsb_ImportCSV.Enabled = true;
                                //    return;
                                //}
                                MessageBox.Show("Invalid File Format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tsb_ImportCSV.Enabled = true;
                                return;
                            }
                            else if (ImportFileType == "Standard")
                            {
                                string strMsg = "";
                                dtImportData = ReadStandardCSVToTable(ImportFileName, ref strMsg);
                                if (dtImportData == null)
                                {
                                    MessageBox.Show(strMsg, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    tsb_ImportCSV.Enabled = true;
                                    return;
                                }
                            }
                        }

                    }
                    if (dtImportData != null && dtImportData.Rows.Count > 0)
                    {
                        try
                        {

                            DataTable dtExistingData = null;
                            if (c1RVUSchedule.DataSource != null)
                            {
                                dtExistingData = ((DataView)c1RVUSchedule.DataSource).ToTable();
                            }
                            if (dtExistingData != null)
                            {
                                if (dtImportData.Rows.Count > 0)
                                {
                                    decimal WUnit, PEUnit, MPUnit = 0;
                                    foreach (DataRow row_loopVariable in dtImportData.Rows)
                                    {
                                        if (Convert.ToString(row_loopVariable[5]) == "")
                                            row_loopVariable[5] = null;
                                        else if (Decimal.TryParse(Convert.ToString(row_loopVariable[5]), out WUnit))
                                            row_loopVariable[5] = FormatNumber(WUnit);
                                        else
                                            row_loopVariable[5] = null;

                                        if (Convert.ToString(row_loopVariable[6]) == "")
                                            row_loopVariable[6] = null;
                                        else if (Decimal.TryParse(Convert.ToString(row_loopVariable[6]), out MPUnit))
                                            row_loopVariable[6] = FormatNumber(MPUnit);
                                        else
                                            row_loopVariable[6] = null;

                                        if (Convert.ToString(row_loopVariable[7]) == "")
                                            row_loopVariable[7] = null;
                                        else if (Decimal.TryParse(Convert.ToString(row_loopVariable[7]), out PEUnit))
                                            row_loopVariable[7] = FormatNumber(PEUnit);
                                        else
                                            row_loopVariable[7] = null;
                                        //row_loopVariable[4] = Convert.ToString(row_loopVariable[4]);
                                        //row_loopVariable[5] = Convert.ToString(row_loopVariable[5]);
                                        //row_loopVariable[6] = Convert.ToString(row_loopVariable[6]);
                                        row_loopVariable[8] = Convert.ToString(Convert.ToDecimal((Convert.ToString(row_loopVariable[5]) == string.Empty ? null : row_loopVariable[5])) + Convert.ToDecimal((Convert.ToString(row_loopVariable[6]) == string.Empty ? null : row_loopVariable[6])) + Convert.ToDecimal((Convert.ToString(row_loopVariable[7]) == string.Empty ? null : row_loopVariable[7])));
                                    }
                                    DataTable dtFormatted = dtExistingData.Clone();
                                    foreach (DataRow row_loopVariable in dtImportData.Rows)
                                    {
                                        dtFormatted.ImportRow(row_loopVariable);
                                    }
                                    dtFormatted.AcceptChanges();
                                    dtExistingData.Merge(dtFormatted);
                                }
                                dtExistingData.AcceptChanges();
                                c1RVUSchedule.DataSource = dtExistingData.DefaultView;
                            }
                            else
                            {
                                decimal WUnit, PEUnit, MPUnit = 0;
                                foreach (DataRow row_loopVariable in dtImportData.Rows)
                                {
                                    if (Convert.ToString(row_loopVariable[5]) == "")//4
                                        row_loopVariable[5] = null;//4
                                    else if (Decimal.TryParse(Convert.ToString(row_loopVariable[5]), out WUnit))//4
                                        row_loopVariable[5] = FormatNumber(WUnit);//4
                                    else
                                        row_loopVariable[5] = null;//4

                                    if (Convert.ToString(row_loopVariable[6]) == "")//5
                                        row_loopVariable[6] = null;//5
                                    else if (Decimal.TryParse(Convert.ToString(row_loopVariable[6]), out MPUnit))//5
                                        row_loopVariable[6] = FormatNumber(MPUnit);//5
                                    else
                                        row_loopVariable[6] = null;//5

                                    if (Convert.ToString(row_loopVariable[7]) == "")//6
                                        row_loopVariable[7] = null;//6
                                    else if (Decimal.TryParse(Convert.ToString(row_loopVariable[7]), out PEUnit))//6
                                        row_loopVariable[7] = FormatNumber(PEUnit);//6
                                    else
                                        row_loopVariable[7] = null;//6

                                    //row_loopVariable[5] = Convert.ToString(row_loopVariable[5]);
                                    //row_loopVariable[6] = Convert.ToString(row_loopVariable[6]);
                                    /////row_loopVariable[7] = Convert.ToString(Convert.ToDecimal((Convert.ToString(row_loopVariable[5]) == string.Empty ? null : row_loopVariable[5])) + Convert.ToDecimal((Convert.ToString(row_loopVariable[6]) == string.Empty ? null : row_loopVariable[6])) + Convert.ToDecimal((Convert.ToString(row_loopVariable[7]) == string.Empty ? null : row_loopVariable[7])));
                                    row_loopVariable[8] = Convert.ToString(Convert.ToDecimal((Convert.ToString(row_loopVariable[5]) == string.Empty ? null : row_loopVariable[5])) + Convert.ToDecimal((Convert.ToString(row_loopVariable[6]) == string.Empty ? null : row_loopVariable[6])) + Convert.ToDecimal((Convert.ToString(row_loopVariable[7]) == string.Empty ? null : row_loopVariable[7])));
                                }

                                c1RVUSchedule.DataSource = dtImportData.DefaultView;
                            }
                        }
                        catch (Exception Ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(Ex, false);
                            MessageBox.Show("Invalid data in file.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tsb_ImportCSV.Enabled = true;
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Improper File Format.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tsb_ImportCSV.Enabled = true;
                    }

                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, true);
                MessageBox.Show("Improper File Format.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                tsb_ImportCSV.Enabled = true;
            }
            finally
            {
                if (objfrmImportRVUSchedule != null) { objfrmImportRVUSchedule.Dispose(); }

            }
        }

        #endregion

        private void pnlDetails_Leave(object sender, EventArgs e)
        {
            CloseInternalControl();
        }

        private void rbInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInactive.Checked == true)
                rbInactive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbInactive.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular); 
        }

        private void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbActive.Checked == true)
                rbActive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbActive.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular); 
        }



        public bool GetExcelIntegartionMethod()
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dt = new DataTable();
            bool _returnvalue = false;
            Object count = 0;
            string sqlQuery = "SELECT sSettingsValue from Settings where sSettingsName = 'Use NPOI Library for Excel Integration'";
            try
            {
                ODB.Connect(false);
                count = ODB.ExecuteScalar_Query(sqlQuery);
                ODB.Disconnect();
                if (Convert.ToString(count) == "True")
                {
                    _returnvalue = true;
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                ODB.Dispose();

            }
            return _returnvalue;

        }

    }
}