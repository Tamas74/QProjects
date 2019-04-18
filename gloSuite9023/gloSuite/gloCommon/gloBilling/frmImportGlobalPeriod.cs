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

namespace gloBilling
{
    public partial class frmImportGlobalPeriod : Form
    {

        #region "Private Variables"
        private ComboBox combo;
        private Int64 _UserID = 0;
        private gloListControl.gloListControl oListControl;
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "";
        private string _strFile = "";
        private string _strFileType = "";
       // private Int32 _nFilePEType = 0;
        private Int64 _ClinicID = 0;
        string strEffectiveDate=string.Empty;
      //  private bool _isChargeFeeSchedule = false;
      //  private bool _bIsValidated = false;
        DataTable dtImportData = new DataTable();
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion "Private Variables"

        #region "Properties"

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
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
       
        #endregion

        #region " Public & Private Methods "                 
     

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
                    if (objCmdSelect != null) {objCmdSelect.Dispose(); }
                    if (conn != null) { conn.Close(); conn.Dispose(); }
                    strMsg = "Invalid file Format";
                    return null;
                }

                if (objdt != null && objdt.Rows.Count > 0 && objdt.Columns.Count == 2)
                {

                    if (objdt.Rows.Count > 0)
                    {                      
                       
                        objdt.Columns[0].ColumnName = "sCPTCode";
                        objdt.Columns[1].ColumnName = "GLOBALDAYS";                      
                        objdt.AcceptChanges();
                    }


                }
                else
                {
                    strMsg = "Invalid file Format";
                    return null;
                }


            }
            catch //(Exception Ex)
            {
                strMsg = "Invalid file Format";
                objdt = null;
            }
            return objdt;
        }

      

        public DataTable GetExcel(string fileName, ref string strMsg)
        {
            DataTable dtExcelData = new DataTable();
            String tempstrfileName = "";
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel.Workbook oWB;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;
            oXL = new Microsoft.Office.Interop.Excel.Application();
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
                string strSubMsg="";
                dtExcelData = ImportExcelXLS(tempstrfileName, ref strSubMsg);
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

        private DataTable ImportExcelXLS(string FileName, ref string strMsg)
        {
            DataTable objdt = new DataTable();
            string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR:YES;\"";
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connstr);
            conn.Open();
            System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter();
            System.Data.OleDb.OleDbCommand objCmdSelect = null;
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

                if (objdt != null && objdt.Rows.Count > 5 && objdt.Columns.Count == 37)
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
                        objdt.Columns[0].ColumnName = "sCPTCode";
                        objdt.Columns[20].ColumnName = "GLOBALDAYS";
                        objdt.AcceptChanges();
                }
                for (int i = 0; i <= objdt.Columns.Count - 1; i++)
                {
                    //SLR: Logic problem to be changed on 4/2/2014
                    if (objdt.Columns[i].ColumnName != "sCPTCode" && objdt.Columns[i].ColumnName != "GLOBALDAYS") // && objdt.Columns[i].ColumnName != "dWorkUnits" && objdt.Columns[i].ColumnName != "dMPUnits" && objdt.Columns[i].ColumnName != "dPEUnits" && objdt.Columns[i].ColumnName != "sModifier" && objdt.Columns[i].ColumnName != "nRVUID" && objdt.Columns[i].ColumnName != "nRVUDtlID")
                    {
                        objdt.Columns.RemoveAt(i);
                        i--;
                        objdt.AcceptChanges();
                    }
                }
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

        private void SaveGlobalDays()
        {
      //      Boolean _bResult = false;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DataTable dtFinalData = new DataTable();

                DataColumn sCPTCode = new DataColumn();
                sCPTCode.DataType = System.Type.GetType("System.String");
                sCPTCode.ColumnName = "sCPTCode";
                dtFinalData.Columns.Add(sCPTCode); 

                // Create the second, calculated, column.
                DataColumn nDays = new DataColumn();
                nDays.DataType = System.Type.GetType("System.Int32");
                nDays.ColumnName = "nDays";
                dtFinalData.Columns.Add(nDays); 

                DataRow _dr = null;
               
                if (c1RVUSchedule.DataSource != null)
                {
                    if (c1RVUSchedule.Rows.Count > 1)
                    {
                        for (int iRowCoun = 1; iRowCoun <= c1RVUSchedule.Rows.Count - 1; iRowCoun++)
                        {

                            Int32 _resultPeriodDays = -1;
                            if (Int32.TryParse(Convert.ToString(c1RVUSchedule.GetData(iRowCoun, COL_GLOB_DAYS)), out _resultPeriodDays))
                            {
                                if (_resultPeriodDays <= 9999)
                                {
                                    _dr = dtFinalData.NewRow();
                                    _dr["nDays"] = _resultPeriodDays;
                                    _dr["sCPTCode"] = Convert.ToString(c1RVUSchedule.GetData(iRowCoun, COL_CPT_CODE)).Trim();
                                    dtFinalData.Rows.Add(_dr);
                                }
                            }
                            else if (Convert.ToString(c1RVUSchedule.GetData(iRowCoun, COL_GLOB_DAYS)).Trim() == "")
                            {
                                _dr = dtFinalData.NewRow();
                                _dr["nDays"] = -1;
                                _dr["sCPTCode"] = Convert.ToString(c1RVUSchedule.GetData(iRowCoun, COL_CPT_CODE)).Trim();
                                dtFinalData.Rows.Add(_dr);
                            }
                        }
                        dtFinalData.AcceptChanges();

                        string strInsCompanyIds = "";
                        if (cmbInsCompany.DataSource != null)
                        {
                            for (int i = 0; i < cmbInsCompany.Items.Count; i++)
                            {
                                cmbInsCompany.SelectedIndex = i;
                                cmbInsCompany.Refresh();
                                if (i > 0)
                                    strInsCompanyIds = strInsCompanyIds + "," + cmbInsCompany.SelectedValue.ToString();
                                else
                                    strInsCompanyIds = cmbInsCompany.SelectedValue.ToString();
                            }
                            cmbInsCompany.SelectedIndex = 0;
                        }
                        
                        clsGlobalPeriods objclsGlobalPeriods = new clsGlobalPeriods();
                       
                        pnlSpeciality.Enabled = false;
                        ts_btnSaveCls.Enabled = false;
                        ts_btnClose.Enabled = false;
                        //pnlDetails.Enabled = false;
                        c1RVUSchedule.Cols[COL_GLOB_DAYS].AllowEditing = false;
                        pnlSearch.Enabled = false;
                        Application.DoEvents();

                        int FailCnt = 0;
                        if (rbSpecificInsCompany.Checked == false)
                        {
                            try
                            {
                                objclsGlobalPeriods.SaveGlobalPeriod_CPTLevel(dtFinalData);
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            }
                        }

                        if (rbSpecificInsCompany.Checked == true)
                        {
                            string[] InsuranceId = strInsCompanyIds.Split(',');
                            if (InsuranceId != null)
                            {
                                CPTProgressBar.Maximum = InsuranceId.Length;
                                for (int i = 0; i < InsuranceId.Length; i++)
                                {
                                    //lblStatus.Text = InsuranceId[i].ToString()  + " In Progess...";
                                    //Application.DoEvents();                                  
                                    
                                    try
                                    {
                                        objclsGlobalPeriods.SaveGlobalPeriod_Inslevel(dtFinalData, Convert.ToInt64(InsuranceId[i].ToString()), _UserID);
                                    }
                                    catch (Exception ex)
                                    {
                                        FailCnt = FailCnt + 1;
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    }
                                    finally
                                    {
                                        CPTProgressBar.Value = i + 1;                                       
                                        Application.DoEvents();
                                    }
                                }
                               
                            }






                            //for (int i = 0; i <= dtFinalData.Rows.Count - 1; i++)
                            //{
                            //    try
                            //    {
                            //        lblStatus.Text = dtFinalData.Rows[i]["sCPTCode"].ToString() + " In Progess...";
                            //        objclsGlobalPeriods.SaveGlobalPeriod_Inslevel(dtFinalData.Rows[i]["sCPTCode"].ToString(), Convert.ToInt32(dtFinalData.Rows[i]["nDays"]), strInsCompanyIds.Trim(), _UserID);
                            //        //  gloAuditTrail.gloAuditTrail.ActivityLog(i.ToString()+") " +dtFinalData.Rows[i]["sCPTCode"].ToString() + " Done"); 
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        FailCnt = FailCnt + 1;
                            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            //    }
                            //    finally
                            //    {

                            //        CPTProgressBar.Value = i + 1;
                            //        Application.DoEvents();
                            //    }
                            //}
                        }
                        else
                        {
                            CPTProgressBar.Value = 1;
                        }

                        objclsGlobalPeriods.update_CPT_Description();

                        objclsGlobalPeriods.Dispose();

                        if (FailCnt > 0)
                            MessageBox.Show("Completed with errors.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); 


                        this.Close();
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("No record to save. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No record to save. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                   
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
          
        }

       

        private void LoadFile()
        {                   
           
            try
            {
               
                    Cursor.Current = Cursors.WaitCursor;
                    DataTable dt = new DataTable();
                    if (txtImportFile.Text.Trim() != "")
                    {
                        string DirectoryName = System.IO.Path.GetDirectoryName(txtImportFile.Text.Trim());
                        ImportFileName = txtImportFile.Text.Trim();
                        String sFileExt = ImportFileName.Trim().Substring(ImportFileName.Trim().LastIndexOf("."));
                        if (sFileExt.ToLower() == ".xlsx" || sFileExt.ToLower() == ".xls")
                        {
                            if (ImportFileType == "Standard")
                            {
                                string strMsg = "";
                                dtImportData = GetExcelGlo(ImportFileName, ref strMsg);
                                if (dtImportData == null)
                                {
                                    MessageBox.Show(strMsg, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                                    
                                    return;
                                }
                            }
                            else if (ImportFileType == "Custom")
                            {
                                string strMsg = "";
                                dtImportData = GetExcel(ImportFileName, ref strMsg);
                                if (dtImportData == null)
                                {
                                    MessageBox.Show(strMsg, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }                      

                    }
                    if (dtImportData != null && dtImportData.Rows.Count > 0)
                    {
                        try
                        {
                            //c1RVUSchedule.DataSource = null;
                            DataView DV = dtImportData.DefaultView;
                            DV.RowFilter = "scptcode<>''";
                            c1RVUSchedule.DataSource = DV;
                            c1RVUSchedule.Refresh();                      
                        }
                        catch //(Exception Ex)
                        {
                            //gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                            MessageBox.Show("Invalid data in file.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Improper File Format.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }

                    Cursor.Current = Cursors.Default;
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                MessageBox.Show("Improper File Format.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            finally
            {
              

            }
        }      


        #endregion

        #region "Column Declaration"

      
        private const int COL_CPT_CODE = 0;
        private const int COL_GLOB_DAYS =1 ;
        
        public gloGridListControl ogloGridListControl = null;
       // private string[] strSearchArray;
         


        #endregion

        #region Constructor

        public frmImportGlobalPeriod(Int64 nRVUId,string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
        }

        #endregion 
     
        #region "Form Load Events"

        private void frmImportGlobalPeriod_Load(object sender, EventArgs e)
        {
            ImportFileType = "Custom";
            //lblStatus.Text = ""; 
            cmbInsCompany.Visible = false;
            btnBrowseInsCompany.Visible = false;
            btnClearInsCompany.Visible = false;
            rbCustom.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
            rbAllInsCompany.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold); 
            gloC1FlexStyle.Style(c1RVUSchedule, false);           
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            tom.SetTabOrder(scheme);
           
        }

        #endregion        

        #region " Form Control Events "

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (dtImportData == null)
            {
                if (c1RVUSchedule.DataSource != null)
                    dtImportData = (DataTable)c1RVUSchedule.DataSource;  
            }
                if (txtSearch.Text.Trim() != "")
                {
                    DataView DV = new DataView(dtImportData);
                    if (DV != null && DV.Count > 0)
                    {
                        DV.RowFilter = "sCPTCode LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                        c1RVUSchedule.DataSource = DV;
                    }
                }
                else
                {
                    c1RVUSchedule.DataSource = dtImportData;
                }
            
        }
    
            
        private void btn_Browse_Click(object sender, EventArgs e)
            {
                try
                {
                    dlgBrowseFile.FileName = "";  
                    dlgBrowseFile.Title = " Browse File ";               
                    dlgBrowseFile.Filter = "Office Documents(*.xls, *.xlsx)|*.xls;*.xlsx";               
                    dlgBrowseFile.CheckFileExists = true;
                    dlgBrowseFile.Multiselect = false;
                    dlgBrowseFile.ShowHelp = false;
                    dlgBrowseFile.ShowReadOnly = false;

                    if (dlgBrowseFile.ShowDialog(this) == DialogResult.OK)
                    {
                        txtImportFile.Text = dlgBrowseFile.FileName;
                        LoadFile(); 
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
            }

            private void rbCustom_Click(object sender, EventArgs e)
            {
               
            }

            private void rbStandard_CheckedChanged(object sender, EventArgs e)
            {
                if (rbStandard.Checked == true)
                {
                    ImportFileType = "Standard";
                    rbStandard.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
                    if (txtImportFile.Text.Trim() != "" && c1RVUSchedule.Rows.Count == 1)
                    {
                        LoadFile();
                    }
                }
                else
                    rbStandard.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular); 

            }

            private void btnBrowseInsCompany_Click(object sender, EventArgs e)
            {
                try
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
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch { }
                        oListControl.Dispose();
                        oListControl = null;
                    }

                    oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.InsuranceCompany , true, this.Width);

                    oListControl.ClinicID = _ClinicID;
                    oListControl.ControlHeader = " Insurance Company ";

                    oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                    this.Controls.Add(oListControl);

                    if (cmbInsCompany.DataSource != null)
                    {
                        for (int i = 0; i < cmbInsCompany.Items.Count; i++)
                        {
                            cmbInsCompany.SelectedIndex = i;
                            cmbInsCompany.Refresh();
                            oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsCompany.SelectedValue), cmbInsCompany.Text);
                        }
                    }
                    oListControl.OpenControl();
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    ex = null;
                }       

            }

            private void btnClearInsCompany_Click(object sender, EventArgs e)
            {
               // cmbInsCompany.Items.Clear();
                cmbInsCompany.DataSource = null;
                cmbInsCompany.Items.Clear();
                cmbInsCompany.Refresh();
                //rbAllInsCompany.Checked = true;  
            }

            private void oListControl_ItemSelectedClick(object sender, EventArgs e)
            {
                try
                {
                  //  cmbInsCompany.Items.Clear();
                    cmbInsCompany.DataSource = null;
                    cmbInsCompany.Items.Clear();
                    DataTable dtReff = new DataTable();
                    DataColumn dcId = new DataColumn("ID");
                    DataColumn dcDescription = new DataColumn("Description");
                    dtReff.Columns.Add(dcId);
                    dtReff.Columns.Add(dcDescription);
                    if (oListControl.SelectedItems.Count > 0)
                    {
                        for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                        {
                            DataRow drTemp = dtReff.NewRow();
                            drTemp["ID"] = oListControl.SelectedItems[i].ID;
                            drTemp["Description"] = oListControl.SelectedItems[i].Description;
                            dtReff.Rows.Add(drTemp);
                        }
                        rbSpecificInsCompany.Checked = true;  
                    }
                    cmbInsCompany.DataSource = dtReff;
                    cmbInsCompany.ValueMember = dtReff.Columns["ID"].ColumnName;
                    cmbInsCompany.DisplayMember = dtReff.Columns["Description"].ColumnName;

                
                }
                catch (Exception)// ex)
                {
                    //ex.ToString();
                    //ex = null;
                }
                finally
                {
               
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
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                     
                }
                //if(cmbInsCompany.Items.Count >0 )
                //    rbSpecificInsCompany.Checked = true;  
                //else
                //    rbAllInsCompany.Checked = true;  
            }

            private void rbAllInsCompany_CheckedChanged(object sender, EventArgs e)
            {
                if (rbAllInsCompany.Checked == true)
                {
                    btnClearInsCompany_Click(null, null);
                    cmbInsCompany.Visible = false;
                    btnBrowseInsCompany.Visible = false;
                    btnClearInsCompany.Visible = false;
                    rbAllInsCompany.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);
                }
                else
                    rbAllInsCompany.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular); 
            }

            private void rbSpecificInsCompany_CheckedChanged(object sender, EventArgs e)
            {
                if (rbSpecificInsCompany.Checked == true)
                {
                    cmbInsCompany.Visible = true;
                    btnBrowseInsCompany.Visible = true;
                    btnClearInsCompany.Visible = true;
                    rbSpecificInsCompany.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);

                }
                else
                    rbSpecificInsCompany.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular); 
            }

            private void rbCustom_CheckedChanged(object sender, EventArgs e)
            {
                if (rbCustom.Checked == true)
                {                     
                    ImportFileType = "Custom";
                    rbCustom.Font = gloGlobal.clsgloFont.gFont_BOLD; //new Font("Tahoma", 9, FontStyle.Bold);

                    if (txtImportFile.Text.Trim() != "" && c1RVUSchedule.Rows.Count == 1)
                    {
                        LoadFile();  
                    }

                }
                else
                    rbCustom.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
            }


        #endregion

        #region " Tool strip Events  "
       
        private void ts_btnSaveCls_Click(object sender, EventArgs e)
        {
            try
            {
                
                Cursor.Current = Cursors.WaitCursor;
                if (validateform())
                    SaveGlobalDays();
                
            }
            catch
            {
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {            
            this.Close();
        }  
        

        private bool validateform ()
        {
            bool result = true ;

            if (rbSpecificInsCompany.Checked == true && cmbInsCompany.Items.Count == 0)
            {
                MessageBox.Show("Select Insurance Comapny", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);               
                result = false;
            }

            return result;
        }
        #endregion     


        private void c1RVUSchedule_SetupEditor(object sender, RowColEventArgs e)
        {
           // ((TextBox)c1RVUSchedule.Editor).MaxLength = 4;
        }

        private void c1RVUSchedule_KeyPressEdit(object sender, KeyPressEditEventArgs e)
        {
            //if ((!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || e.KeyChar == '' || e.KeyChar == 13)
            //{
            //    e.Handled = true;
            //}
            //else
            //{
            //    e.Handled = false;
            //}
        }

        private void btnClearFile_Click(object sender, EventArgs e)
        {
            txtImportFile.Text = "";  
            if (c1RVUSchedule.Rows.Count > 1)
            {
               
                dtImportData.Rows.Clear();
                c1RVUSchedule.DataSource = dtImportData;
               
            }       
           
        }

        private void c1RVUSchedule_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(c1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void txtImportFile_MouseMove(object sender, MouseEventArgs e)
        {
          
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
        private int getWidthofText(string _text, TextBox textbox)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g!=null)
            {
                SizeF s = g.MeasureString(_text, textbox.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            
            return width;
        }

        private void cmbInsCompany_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void cmbInsCompany_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                combo = (ComboBox)sender;

                if (cmbInsCompany.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsCompany.Items[cmbInsCompany.SelectedIndex])["Description"]), cmbInsCompany) >= cmbInsCompany.DropDownWidth - 20)
                    {
                        tooltip_Billing.SetToolTip(cmbInsCompany, Convert.ToString(((DataRowView)cmbInsCompany.Items[cmbInsCompany.SelectedIndex])["Description"]));
                    }
                    else
                    {
                        this.tooltip_Billing.Hide(cmbInsCompany);
                    }
                }
                else
                {
                    this.tooltip_Billing.Hide(cmbInsCompany);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void cmbInsCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                combo = (ComboBox)sender;

                if (cmbInsCompany.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbInsCompany.Items[cmbInsCompany.SelectedIndex])["Description"]), cmbInsCompany) >= cmbInsCompany.DropDownWidth - 20)
                    {
                        tooltip_Billing.SetToolTip(cmbInsCompany, Convert.ToString(((DataRowView)cmbInsCompany.Items[cmbInsCompany.SelectedIndex])["Description"]));
                    }
                    else
                    {
                        this.tooltip_Billing.Hide(cmbInsCompany);
                    }
                }
                else
                {
                    this.tooltip_Billing.Hide(cmbInsCompany);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void txtImportFile_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                TextBox textbox = (TextBox)sender;

                if (txtImportFile.Text.Trim() != "")
                {
                    if (getWidthofText(txtImportFile.Text.Trim(), txtImportFile) >= txtImportFile.Width - 20)
                    {
                        tooltip_Billing.SetToolTip(txtImportFile, txtImportFile.Text.Trim());
                    }
                    else
                    {
                        this.tooltip_Billing.Hide(txtImportFile);
                    }
                }
                else
                {
                    this.tooltip_Billing.Hide(txtImportFile);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

      
    }
}