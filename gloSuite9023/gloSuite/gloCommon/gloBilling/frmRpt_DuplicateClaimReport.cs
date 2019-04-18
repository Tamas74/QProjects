using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
//Duplicate Claim Report.
//Added for modify charge.
using gloAccountsV2;
using gloGlobal;

namespace gloBilling
{
    public partial class frmRpt_DuplicateClaimReport : Form
    {

        #region " Variable Declarations"

        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private String _databaseconnectionstring = "";
        private string _messageboxcaption = "";
        private Int64 _ClinicID = 0;

        private static frmRpt_DuplicateClaimReport frm;
        private bool blnDisposed;
        
        #endregion

        #region " Grid Constants "
        // C1 Grid Column Constants        
        const int COL_PATIENT_CODE = 0;
        const int COL_PATIENT_NAME = 1;
        const int COL_SERVICE_DATE = 2;
        const int COL_CLAIM_NO = 3;
        const int COL_CPT = 4;
        const int COL_DX1 = 5;
        const int COL_DX2 = 6;
        const int COL_DX3 = 7;
        const int COL_DX4 = 8;
        const int COL_M1 = 9;
        const int COL_M2 = 10;
        const int COL_CHARGES = 11;
        const int COL_UNIT = 12;
        const int COL_TOTAL = 13;
        const int COL_ALLOWED = 14;
        const int COL_PROVIDER = 16;
        const int COL_INSURANCE = 15;
        //Duplicate Claim Report.
        //New column constant added for grid. and set COL_COLCOUNT
        const int COL_PARTY = 17;
        const int COL_USERNAME = 18;
        const int COL_CREATEDDATE = 19;

        const int COL_COLCOUNT = 20;

        #endregion " Grid Constants "

        #region " Constructors"

        public frmRpt_DuplicateClaimReport(String DataBaseConnectionString)
        {
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
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM"; ;
                }
            }
            else
            { _messageboxcaption = "gloPM"; ; }

            #endregion

            _databaseconnectionstring = DataBaseConnectionString;
            InitializeComponent();
        }

        public static frmRpt_DuplicateClaimReport GetInstance(String DataBaseConnectionString)
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmRpt_DuplicateClaimReport(DataBaseConnectionString);
                }
            }
            finally
            {

            }
            return frm;
        }

        #endregion

        #region " C1 Flex Grid"

        //Design C1 Grid To Display Remittance Details 
        
        //Duplicate Claim Report.
        //New Column added for Party, User, Created Date.
        //Reset width for all column. Make Charge, Allowed,Insurance, Provider Column visible false.
        private void DesignGrid()
        {
            c1Claim.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
           // c1Claim.Clear();
            c1Claim.DataSource = null;

            C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1Claim.Styles.Add("cs_CurrencyStyle");
            try
            {
                if (c1Claim.Styles.Contains("cs_CurrencyStyle"))
                {
                    csCurrencyStyle = c1Claim.Styles["cs_CurrencyStyle"];
                }
                else
                {
                    csCurrencyStyle = c1Claim.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    //Bug #49613: PM-Duplicate Claim Report-Application displays small font size for Charge column
                    //change font size from 8.25 to 9 
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_BOLD; //new System.Drawing.Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }

            }
            catch
            {
                csCurrencyStyle = c1Claim.Styles.Add("cs_CurrencyStyle");
                csCurrencyStyle.DataType = typeof(System.Decimal);
                csCurrencyStyle.Format = "c";
                //Bug #49613: PM-Duplicate Claim Report-Application displays small font size for Charge column
                //change font size from 8.25 to 9 
                csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

            }
  




            c1Claim.Cols.Count = COL_COLCOUNT;
            c1Claim.Rows.Count = 1;
            c1Claim.Rows.Fixed = 1;

            c1Claim.SetData(0, COL_SERVICE_DATE, "Service Date");
            c1Claim.SetData(0, COL_PATIENT_CODE, "Patient Code");
            c1Claim.SetData(0, COL_PATIENT_NAME, "Patient");
            c1Claim.SetData(0, COL_CLAIM_NO, "Claim #");
            c1Claim.SetData(0, COL_CPT, "CPT");
            c1Claim.SetData(0, COL_DX1, "DX1");
            c1Claim.SetData(0, COL_DX2, "DX2");
            c1Claim.SetData(0, COL_DX3, "DX3");
            c1Claim.SetData(0, COL_DX4, "DX4");
            c1Claim.SetData(0, COL_M1, "M1");
            c1Claim.SetData(0, COL_M2, "M2");
            c1Claim.SetData(0, COL_CHARGES, "Charges");
            c1Claim.SetData(0, COL_UNIT, "Unit");
            c1Claim.SetData(0, COL_TOTAL, "Charge");
            c1Claim.SetData(0, COL_ALLOWED, "Allowed");
            c1Claim.SetData(0, COL_PARTY, "Party");
            c1Claim.SetData(0, COL_PROVIDER, "Provider");
            c1Claim.SetData(0, COL_INSURANCE, "Insurance");
            c1Claim.SetData(0, COL_USERNAME, "User");
            c1Claim.SetData(0, COL_CREATEDDATE, "Created Date");

            c1Claim.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

            //Set C1 Grid Column Width
            int width = pnl_View.Width - 1;
            if (width < 1051)
                width = 1051;
            c1Claim.Cols[COL_SERVICE_DATE].Width = (int)(width * 0.07);
            c1Claim.Cols[COL_PATIENT_CODE].Width = (int)(width * 0.07);
            c1Claim.Cols[COL_PATIENT_NAME].Width = (int)(width * 0.12);
            c1Claim.Cols[COL_CLAIM_NO].Width = (int)(width * 0.05);
            c1Claim.Cols[COL_CPT].Width = (int)(width * 0.06);
            c1Claim.Cols[COL_DX1].Width = (int)(width * 0.04);
            c1Claim.Cols[COL_DX2].Width = (int)(width * 0.04);
            c1Claim.Cols[COL_DX3].Width = (int)(width * 0.04);
            c1Claim.Cols[COL_DX4].Width = (int)(width * 0.04);
            c1Claim.Cols[COL_M1].Width = (int)(width * 0.03);
            c1Claim.Cols[COL_M2].Width = (int)(width * 0.03);
            c1Claim.Cols[COL_CHARGES].Width = (int)(width * 0.08);
            c1Claim.Cols[COL_UNIT].Width = (int)(width * 0.03);
            c1Claim.Cols[COL_TOTAL].Width = (int)(width * 0.05);
            c1Claim.Cols[COL_ALLOWED].Width = (int)(width * 0.08);
            c1Claim.Cols[COL_PARTY].Width = (int)(width * 0.03);
            c1Claim.Cols[COL_PROVIDER].Width = (int)(width * 0.15);
            c1Claim.Cols[COL_INSURANCE].Width = (int)(width * 0.15);
            c1Claim.Cols[COL_USERNAME].Width = (int)(width * 0.08);
            c1Claim.Cols[COL_CREATEDDATE].Width = (int)(width * 0.12);


            c1Claim.Cols[COL_SERVICE_DATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_PATIENT_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_PATIENT_NAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_CLAIM_NO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_CPT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_DX1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_DX2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_DX3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_DX4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_M1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_M2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_CHARGES].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1Claim.Cols[COL_UNIT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1Claim.Cols[COL_TOTAL].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1Claim.Cols[COL_ALLOWED].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1Claim.Cols[COL_PARTY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
            c1Claim.Cols[COL_PROVIDER].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_INSURANCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_USERNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            c1Claim.Cols[COL_CREATEDDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;


            c1Claim.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            c1Claim.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;


            c1Claim.Cols[COL_CHARGES].Style = csCurrencyStyle;
            c1Claim.Cols[COL_TOTAL].Style = csCurrencyStyle;
            c1Claim.Cols[COL_ALLOWED].Style = csCurrencyStyle;
            c1Claim.Cols[COL_CHARGES].Visible = false;
            c1Claim.Cols[COL_ALLOWED].Visible = false;
            c1Claim.Cols[COL_PROVIDER].Visible = false;
            c1Claim.Cols[COL_INSURANCE].Visible = false;

            c1Claim.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            c1Claim.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;


        }

        #endregion

        #region " Form Events"

        protected override void Dispose(bool disposing)
        {

            if (!(this.blnDisposed))
            {
                if ((disposing))
                {

                    try
                    {
                        if (dtpEndDate != null)
                        {
                            try
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpEndDate);

                            }
                            catch
                            {
                            }


                            dtpEndDate.Dispose();
                            dtpEndDate = null;
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        if (dtpStartDate != null)
                        {
                            try
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpStartDate);

                            }
                            catch
                            {
                            }


                            dtpStartDate.Dispose();
                            dtpStartDate = null;
                        }
                    }
                    catch
                    {
                    }



                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    if ((components != null))
                    {
                        components.Dispose();
                    }

                }

            }
            frm = null;
            this.blnDisposed = true;
            base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        ~frmRpt_DuplicateClaimReport()
        {
            Dispose(false);
        }

        private void frmRpt_DuplicateClaimReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void frmRpt_DuplicateClaimReport_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1Claim, false);
            //Duplicate Claim Report.
            //Set other button enable false till report is loaded.
            try
            {
                if (c1Claim.Rows.Count <= 1)
                {
                    tls_btnExportToExcel.Enabled = false;
                    tls_btnExportToExcelOpen.Enabled = false;
                    tls_btnCharge.Enabled = false;
                    tls_btnPrint.Enabled = false;
                }
                else
                {
                    tls_btnExportToExcel.Enabled = true;
                    tls_btnExportToExcelOpen.Enabled = true;
                    tls_btnCharge.Enabled = true;
                    tls_btnPrint.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #endregion

        #region " Fill Form "
        
        //Duplicate Claim Report.
        //Call DesignGrid(), make chages in function restect to new Query.
        private void FillDuplicateClaim()
        {
            DesignGrid();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataSet dt=null;
            DataTable dtDistinct=null;
            DataRow[] drResult;
            string _sKey = "";
            try
            {
                oDB.Connect(false);
                gloDatabaseLayer.DBParameters oParam = new gloDatabaseLayer.DBParameters();
                oParam.Add("@StartDate", dtpStartDate.Value.ToShortDateString(), ParameterDirection.Input, SqlDbType.Date);
                oParam.Add("@EndDate", dtpEndDate.Value.ToShortDateString(), ParameterDirection.Input, SqlDbType.Date);
                //oDB.Retrive("gSP_GetPatientAppointments", oParam, out dt);

                oDB.Retrive("rpt_DuplicateClaimReport", oParam, out dt);

                string[] TobeDistinct = { "nPatientID", "nServiceDate", "sCPTCode", "sPatientCode", "sPatientName" };
                dtDistinct = GetDistinctRecords(dt.Tables[0], TobeDistinct);
                if (dt != null)
                {
                    c1Claim.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple;
                    c1Claim.Tree.Column = COL_PATIENT_CODE;
                    C1.Win.C1FlexGrid.Node oClaimNode;

                    int rowIndex = 0;
                    foreach (DataRow rowMain in dtDistinct.Rows)
                    {
                        string strSelect = string.Format("nPatientID = '{0}' AND nServiceDate = '{1}' AND sCPTCode = '{2}'", rowMain[0].ToString(), rowMain[1], rowMain[2].ToString().Replace("'",""));
                        drResult = dt.Tables[0].Select(strSelect);

                        c1Claim.Rows.Add();
                        rowIndex = c1Claim.Rows.Count - 1;

                        c1Claim.SetData(rowIndex, COL_PATIENT_CODE, Convert.ToString(rowMain["sPatientCode"])); //
                        c1Claim.SetData(rowIndex, COL_PATIENT_NAME, Convert.ToString(rowMain["sPatientName"])); //
                        c1Claim.SetData(rowIndex, COL_SERVICE_DATE, Convert.ToDateTime(rowMain["nServiceDate"]).ToString("MM/dd/yyyy"));//                      

                        c1Claim.Rows[rowIndex].IsNode = true;
                        c1Claim.Rows[rowIndex].Node.Data = Convert.ToString(rowMain["sPatientCode"]);
                        c1Claim.Rows[rowIndex].Node.Level = 1;

                        oClaimNode = c1Claim.Rows[rowIndex].Node;
                        int cnt = 1;
                        foreach (DataRow rowDetail in drResult)
                        {
                            // Detail
                            
                            //Key = nTransactionID~PatientID~ClaimNo~Party
                            _sKey = Convert.ToInt64(rowDetail["nTransactionID"]).ToString();
                            _sKey += "~" + Convert.ToString(rowDetail["nPatientID"]);
                            _sKey += "~" + Convert.ToString(rowDetail["nClaimNumber"]);
                            _sKey += "~" + Convert.ToString(rowDetail["Party"]);

                            oClaimNode.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, cnt, _sKey, null);
                            rowIndex = oClaimNode.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild).Row.Index;
                            c1Claim.SetData(rowIndex, COL_CLAIM_NO, Convert.ToString(rowDetail["nClaimNumber"])); //
                            c1Claim.SetData(rowIndex, COL_CPT, Convert.ToString(rowDetail["sCPTCode"])); //
                            c1Claim.SetData(rowIndex, COL_DX1, Convert.ToString(rowDetail["sDx1Code"]));
                            c1Claim.SetData(rowIndex, COL_DX2, Convert.ToString(rowDetail["sDx2Code"])); //
                            c1Claim.SetData(rowIndex, COL_DX3, Convert.ToString(rowDetail["sDx3Code"])); //
                            c1Claim.SetData(rowIndex, COL_DX4, Convert.ToString(rowDetail["sDx4Code"])); //
                            c1Claim.SetData(rowIndex, COL_M1, Convert.ToString(rowDetail["sMod1Code"])); //
                            c1Claim.SetData(rowIndex, COL_M2, Convert.ToString(rowDetail["sMod2Code"])); //
                            c1Claim.SetData(rowIndex, COL_CHARGES, Convert.ToDecimal(rowDetail["dCharges"])); //
                            c1Claim.SetData(rowIndex, COL_UNIT, Convert.ToDecimal(rowDetail["dUNIT"])); //
                            c1Claim.SetData(rowIndex, COL_TOTAL, Convert.ToDecimal(rowDetail["dTotal"])); //
                            c1Claim.SetData(rowIndex, COL_ALLOWED, Convert.ToDecimal(rowDetail["dAllowed"])); //
                            c1Claim.SetData(rowIndex, COL_PARTY, Convert.ToString(rowDetail["Party"]));
                            c1Claim.SetData(rowIndex, COL_PROVIDER, Convert.ToString(rowDetail["sProviderName"]));
                            c1Claim.SetData(rowIndex, COL_USERNAME, Convert.ToString(rowDetail["UserName"]));
                            c1Claim.SetData(rowIndex, COL_CREATEDDATE, Convert.ToDateTime(rowDetail["CreatedDate"]).ToString("MM/dd/yyyy hh:mm:ss tt"));

                            cnt++;
                        }
                    }
                }

                dt.Dispose();
                dtDistinct.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message+_sKey, true);
            }
            finally
            {
                if (dt!=null)
                {
                    dt.Dispose();
                }
                if (dtDistinct!=null)
                {
                    dtDistinct.Dispose();
                }
                oDB.Disconnect();
                oDB.Dispose();
            }
        }
        
        //Duplicate Claim Report.
        //Added to get distinct record in table according to column name specify in string[] Columns
        private DataTable GetDistinctRecords(DataTable dt, string[] Columns)
        {
            DataTable dtUniqRecords = new DataTable();
            dtUniqRecords = dt.DefaultView.ToTable(true, Columns);
            return dtUniqRecords;
        }

        #endregion

        #region " ToolStrip Button Click Event "

        private void tls_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tls_btnExportToExcel_Click(object sender, EventArgs e)
        {

            if (c1Claim != null && c1Claim.Rows.Count > 1)
            {
                ExportReportToExcel(false);

            }
        }

        private void tls_btnExportToExcelOpen_Click(object sender, EventArgs e)
        {

            if (c1Claim != null && c1Claim.Rows.Count > 1)
            {
                ExportReportToExcel(true);
            }
        }


        #endregion " ToolStrip Button Click Event "

        #region " Export Report "
        private void ExportReportToExcel(bool OpenReport)
        {
         //   gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            string _DefaultLocationPath = "";
            string _FilePath = "";
            bool _Checked = false;
            try
            {
                gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
                if (Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation")) != "")
                {
                    _Checked = Convert.ToBoolean(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocation"));
                }
                else
                {
                    _Checked = false;
                }
                _DefaultLocationPath = Convert.ToString(oSettings.ReadSettings_XML("Reports", "ExportToDefaultLocationPath"));
                oSettings.Dispose();

                FileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel File(.xls)|*.xls";
                saveFileDialog.DefaultExt = ".xls";
                saveFileDialog.AddExtension = true;

                if (_DefaultLocationPath != "" && _Checked == true)
                {
                    if (_DefaultLocationPath.EndsWith("\\"))
                    {
                        char[] trimChars = { '\\' };
                        _DefaultLocationPath = _DefaultLocationPath.TrimEnd(trimChars);
                    }
                    // If not exist create directory
                    if (Directory.Exists(_DefaultLocationPath) == false)
                    {
                        Directory.CreateDirectory(_DefaultLocationPath);
                    }

                    saveFileDialog.InitialDirectory = _DefaultLocationPath;
                }
               
                    if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                    {
                        saveFileDialog.Dispose();
                        saveFileDialog = null;
                        return;
                    }
                    _FilePath = saveFileDialog.FileName;
                    saveFileDialog.Dispose();
                    saveFileDialog = null;
                c1Claim.SaveExcel(_FilePath, "sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells);

                if (OpenReport == true)
                {
                    if (File.Exists(_FilePath) == true)
                    {
                        System.Diagnostics.Process.Start(_FilePath);
                    }
                }
                else
                {
                    MessageBox.Show("File saved successfully.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (IOException)// ioEx)
            {
                MessageBox.Show("File in use. Fail to export report.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ioEx.ToString();
                //ioEx = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }
        #endregion

        #region  " C1 Flex Grid Events "
        
        //Duplicate Claim Report.
        //changes to generate key as in FillDuplicateClaim().
        private void c1Claim_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                C1.Win.C1FlexGrid.HitTestInfo hitInfo = c1Claim.HitTest(e.X, e.Y);

                //if hitrow is not header row open entry for edit 
                //else set the search column header 
                if (hitInfo.Row > 0)
                {
                    if (c1Claim.Rows[c1Claim.RowSel].Node.Key != null)
                    {
                        if (c1Claim.Rows[c1Claim.RowSel].Node.Level == 2)
                        {
                            //Key = TransactionID~TransactionDetailID~TransactionLineNo~PatientID~ClaimNo~InsuranceID
                            string _sKey = GetTransactionPatientClaimIDs();//c1Claim.Rows[c1Claim.RowSel].Node.Key.ToString();
                            //string _ids=GetTransactionPatientClaimIDs();
                            string[] sKey = _sKey.Split('~');
                            if (sKey.Length == 4)
                            {

                                Int64 _transactionId = Convert.ToInt64(sKey[0].ToString());
                                //Int64 _transactionDetailID = Convert.ToInt64(sKey[1].ToString());
                                //Int64 _transactionLineNo = Convert.ToInt64(sKey[2].ToString());
                                Int64 _patinetID = Convert.ToInt64(sKey[1].ToString());
                                Int64 _claimNo = Convert.ToInt64(sKey[2].ToString());
                                string _party = sKey[3].ToString();
                                //MessageBox.Show(_sKey);
                            }
                            
                        }
                    }
                }
            }
        }
        
        //Duplicate Claim Report.
        //added to get required ID from Key associated to that node.
        private string GetTransactionPatientClaimIDs(string idName="All")
        {
            string _id =string.Empty;
            if (c1Claim.Rows[c1Claim.RowSel].Node.Key != null)
            {
                if (c1Claim.Rows[c1Claim.RowSel].Node.Level == 2)
                {
                    //Key = TransactionID~PatientID~ClaimNo~Party
                    string _sKey = c1Claim.Rows[c1Claim.RowSel].Node.Key.ToString();
                    string[] sKey = _sKey.Split('~');
                    if (sKey.Length == 4)
                    {
                        if (idName=="TransactionID")
                        {
                            _id  = sKey[0].ToString();
                        }
                        if (idName=="PatientID")
                        {
                            _id = sKey[1].ToString();
                        }
                        if (idName=="ClaimNo")
                        {
                            _id = sKey[2].ToString();
                        }
                        if (idName == "Party")
                        {
                            _id = sKey[3].ToString();
                        }
                        if (idName=="All")
                        {
                            _id = sKey[0].ToString() + "~" + sKey[1].ToString() + "~" + sKey[2].ToString() + "~" + sKey[3].ToString();
                        }
                    }
                }
            }
            return _id;
        }
        
        //Duplicate Claim Report.
        //Added code to open modify charges form when double click on grid.
        private void c1Claim_DoubleClick(object sender, EventArgs e)
        {
            Boolean _IsModified = false;
            try
            {
                if (c1Claim.Rows[c1Claim.RowSel].Node.Key != null)
                {
                    if (c1Claim.Rows[c1Claim.RowSel].Node.Level == 2)
                    {
                        _IsModified = ModifyCharge();
                        if (_IsModified)
                        {
                            FillDuplicateClaim();
                        }
                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        #endregion
        
        //Duplicate Claim Report.
        //New event added on show report button to generate report. using filter criteria of startdate and enddate.
        private void tls_btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                FillDuplicateClaim();

                if (c1Claim.Rows.Count <= 1)
                {
                    tls_btnExportToExcel.Enabled = false;
                    tls_btnExportToExcelOpen.Enabled = false;
                    tls_btnCharge.Enabled = false;
                    tls_btnPrint.Enabled = false;
                }
                else
                {
                    tls_btnExportToExcel.Enabled = true;
                    tls_btnExportToExcelOpen.Enabled = true;
                    tls_btnCharge.Enabled = true;
                    tls_btnPrint.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        
        //Duplicate Claim Report.
        //New event added on Modify charge button for open modify charges form when clicking.
        private void tls_btnCharge_Click(object sender, EventArgs e)
        {
            Boolean _IsModified = false;
            try
            {
                if (c1Claim.Rows[c1Claim.RowSel].Node.Key != null)
                {
                    if (c1Claim.Rows[c1Claim.RowSel].Node.Level == 2)
                    {
                        _IsModified = ModifyCharge();
                        if (_IsModified)
                        {
                            FillDuplicateClaim();
                        }
                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }
        
        //Duplicate Claim Report.
        //New function added to call selected claim in modify mode.
        private Boolean ModifyCharge()
        {
            Boolean _IsModified = false;

            try
            {
                gloPatientFinancialViewV2 objPatFinacialView = null;
                
                Int64 ParamTransactionId = 0;
                Int64 PatientID = 0;
                Int64 ClaimNo = 0;
                string party = string.Empty;

                ClaimNo =Convert.ToInt64(GetTransactionPatientClaimIDs("ClaimNo"));
                PatientID = Convert.ToInt64(GetTransactionPatientClaimIDs("PatientID"));
                party = GetTransactionPatientClaimIDs("Party");

                objPatFinacialView = new gloPatientFinancialViewV2(PatientID);

                gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");

                if (party == "V")
                {
                    ParamTransactionId = objPatFinacialView.GetClaimTransactionID(ClaimNo, "", true);
                    _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, true, true, this);
                }
                else
                {
                    ParamTransactionId = objPatFinacialView.GetClaimTransactionID(ClaimNo, "", false);
                    _IsModified = ogloBilling.ShowModifyCharges(PatientID, ParamTransactionId, false, true, this);
                }
                ogloBilling.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
            return _IsModified;
        }
        
        //Duplicate Claim Report.
        //New event is added on Print button to open same report in SSRS to print.
        private void tls_btnPrint_Click(object sender, EventArgs e)
        {
            Image img = this.Icon.ToBitmap();
            ShowSSRSReport("rptDuplicateClaimReport", "Duplicate Claim Report", true, img);
        }

        private void ShowSSRSReport(string ReportName, string ReportTitle, bool blnIsgloStreamReport, Image img)
        {
            Cursor.Current = Cursors.WaitCursor;
            SSRSApplication.frmSSRSViewer frmSSRS = new SSRSApplication.frmSSRSViewer();
            frmSSRS.Conn = gloPMGlobal.DatabaseConnectionString;
            frmSSRS.reportName = ReportName;
            frmSSRS.reportTitle = ReportTitle;
            frmSSRS.formIcon = img;
            frmSSRS.IsgloStreamReport = blnIsgloStreamReport;
            frmSSRS.MdiParent = this.ParentForm;
            Cursor.Current = Cursors.Default;
            frmSSRS.parameterName = "StartDate,EndDate";
            frmSSRS.ParameterValue = dtpStartDate.Value.ToShortDateString()+","+dtpEndDate.Value.ToShortDateString();
            frmSSRS.Show();
        }
        
    }
}