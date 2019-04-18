using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using gloOffice;
using System.Windows.Forms;
using System.Collections;
using gloBilling.Statement;
using System.IO;
using C1.Win.C1FlexGrid;
using System.Runtime.InteropServices;
using gloPatient;


namespace gloBilling
{
    public partial class frmWd_BatchPatientStatement : Form
    {
        //[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern bool SetDefaultPrinter(string Name);
        #region " Variable Declarations"

        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private String _databaseconnectionstring = "";
        private string _messageboxcaption = "";
        private Int64 _PatientID = 0;
        private Int64 _PatientID_To_Delete = 0;
        private DataView _dv = new DataView();
        private string[] strSearchArray;
        gloPatientStripControl.gloPatientStripControl oPatientControl = null;
        int _rowselect = 0;
        private Int64 _nTrasactionID = 0;
        private Int64 _ClinicID = 0;


        //Code Added by Mayuri:20091208
        private const int COL_sPatientCode = 0;
        private const int COL_sAccountNo = 1;
        private const int COL_sFirstName = 2;
        private const int COL_sMiddleName = 3;
        private const int COL_sLastName = 4;
        //private const int COL_sFirsrName = 4;
        private const int COL_sUserName = 5;
        private const int COL_dtCreateDate = 6;
        private const int COL_dtStatementDate = 7;

        private const int COL_PatientDue = 8;

        private const int COL_nTemplateTransactionID = 9;
        private const int COL_nBatchDetailID = 10;

        //Roopali For void functionality on 17 August 2010
        private const int COL_sVoidNotes = 11;

        private const int COL_bIsVoid = 12;
        private const int COL_nPatientID = 13;
        //Roopali For void functionality on 17 August 2010
        private const int COL_sVoidButton = 14;
        private const int COL_sVoidUserName = 15;
        private const int COL_sVoidStatementDateTime = 16;

        //Code added by SaiKrishna
        private const int COL_nAccountID = 17;
        private const int COL_sResult = 18;



        private const int COL_Count = 19;



        DataTable dtBatch = new DataTable();
        //End code Added by Mayuri:20091208

        private static frmWd_BatchPatientStatement frm;
        private bool blnDisposed;
        bool _isBusinessCenterEnable = false;
        // We need to use unmanaged code

        [DllImport("user32.dll")]

        // GetCursorPos() makes everything possible

        static extern bool GetCursorPos(ref Point lpPoint);

        //Code added by SaiKrishna on 04-12-2011(mm-dd-yyyy)
        public bool _IsPatientAccountFeature = false;
        gloAccount objgloAccount = null;

        #endregion

        #region " Constructors"

        public frmWd_BatchPatientStatement(Int64 PatientID, String DataBaseConnectionString)
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
            _PatientID = PatientID;
            InitializeComponent();
        }

        public static frmWd_BatchPatientStatement GetInstance(Int64 PatientID, String DataBaseConnectionString)
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmWd_BatchPatientStatement(PatientID, DataBaseConnectionString);
                }
            }
            finally
            {

            }
            return frm;
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

        ~frmWd_BatchPatientStatement()
        {
            Dispose(false);
        }

        private void frmWd_BatchPatientStatement_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void frmWd_BatchPatientStatement_Load(object sender, EventArgs e)
        {
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            gloC1FlexStyle.Style(c1PatientTemplates, false);

            try
            {
                _isBusinessCenterEnable = gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_Statment");
                if (oSettings.ReadSettings_XML("VoidedStatementBatch", "ChkVoidBatch") != null && oSettings.ReadSettings_XML("VoidedStatementBatch", "ChkVoidBatch") != "")
                    ChkIncludeVoidedBatch.Checked = Convert.ToBoolean(oSettings.ReadSettings_XML("VoidedStatementBatch", "ChkVoidBatch"));
                else
                    ChkIncludeVoidedBatch.Checked = false;

                //Code added by SaiKrishna on 04-14-2011(mm-dd-yyyy)
                objgloAccount = new gloAccount(_databaseconnectionstring);
                _IsPatientAccountFeature = objgloAccount.GetPatientAccountFeatureSetting();

                FillCatTemplates(tsb_CategoryTemplates);
                DesignGrid();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oSettings != null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }
            }
        }

        private void frm_Form_Closed(object sender, ToolStripItemClickedEventArgs e)
        {
            gloOffice.frmWd_PatientTemplate frm = null;
            try
            {
                frm = (gloOffice.frmWd_PatientTemplate)sender;
            }
            catch
            {
            }
            try
            {
                frm.Form_Closed -= new frmWd_PatientTemplate.FormClosed(frm_Form_Closed);
            }
            catch
            {
            }
            try
            {
                if (frm != null)
                {
                    frm.Close();
                }
                if (frm != null)
                {
                    frm.Dispose();
                    frm = null;
                }
            }
            catch
            {
            }
            Fill_PatientTemplate();
            c1PatientTemplates.Row = _rowselect;
        }

        #endregion

        #region " Fill Methods"



        //Added by Mayuri:20091208
        private void Fill_PatientTemplate()
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //code changed by SaiKrishna 04-19-2011
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            ODB.Connect(false);

            //DataTable dtBatch = new DataTable();
            try
            {
                foreach (TreeNode oBatchNode in trv_viewPatientStatement.Nodes)
                {
                    if (oBatchNode.IsSelected == true)
                    {

                        string BatchPatientID = Convert.ToString(oBatchNode.Tag);
                        //Added By Pramod Nair
                        //string _sqlQuery = "";


                        //        _sqlQuery = "SELECT Patient.sPatientCode, Patient.sFirstName, Patient.sMiddleName, Patient.sLastName, BL_Batch_PatientStatement_Mst.sUserName," +

                        //         //"BL_Batch_PatientStatement_Mst.dtCreateDate, BL_Batch_PatientStatement_Mst.dtStatementDate, " +
                        //         " BL_Batch_PatientStatement_Mst.dtCreateDate,CASE BL_Batch_PatientStatement_Mst.bIsUnclosedDay "+
                        //         " WHEN 1 THEN CONVERT(VARCHAR(10),BL_Batch_PatientStatement_Mst.dtStatementDate,101) + ' [Unclosed]' "+
                        //         " ELSE CONVERT(VARCHAR(10),BL_Batch_PatientStatement_Mst.dtStatementDate,101) END AS dtStatementDate,   " +
                        //         " BL_Batch_PatientStatement_DTL.nTempleteTransactionID,BL_Batch_PatientStatement_DTL.nBatchPateintStatDtlID, BL_Batch_PatientStatement_DTL.sVoidNotes As sVoidNotes, " +
                        //         " CASE WHEN isnull(BL_Batch_PatientStatement_DTL.bIsVoid,0) = 0 then ' ' ELSE 'Voided' END As bIsVoid,BL_Batch_PatientStatement_DTL.nPatientID As nPatientID, " +
                        //         " '' AS sVoidButton,ISNULL(BL_Batch_PatientStatement_DTL.sVoidUserName,'') AS UserName,ISNULL(BL_Batch_PatientStatement_DTL.dtVoidDate,0) AS VoidDateTime FROM BL_Batch_PatientStatement_DTL WITH (NOLOCK) INNER JOIN " +
                        //          "Patient WITH (NOLOCK) ON BL_Batch_PatientStatement_DTL.nPatientID = Patient.nPatientID INNER JOIN " +
                        //          "BL_Batch_PatientStatement_Mst WITH (NOLOCK) ON BL_Batch_PatientStatement_DTL.nBatchPateintStatMstID = BL_Batch_PatientStatement_Mst.nBatchPateintStatMstID " +

                        //        "where BL_Batch_PatientStatement_Mst.nBatchPateintstatMstID='" + (Convert.ToInt64(BatchPatientID)) + "' order by dtCreateDate desc ";
                        //        ODB.Retrive_Query(_sqlQuery, out dtBatch);

                        //ODB.Disconnect();

                        //code changed by SaiKrishna 04-19-2011
                        oParameters.Add("@nBatchPateintstatMstID", BatchPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        ODB.Retrive("PA_Fill_PatientTemplate", oParameters, out dtBatch);
                        _dv = dtBatch.DefaultView;
                        Decimal TotalDue = 0;
                        if (dtBatch.Compute("Sum(PatientDue)", "bIsVoid <> 'Voided'") != DBNull.Value)
                        {
                            TotalDue = Convert.ToDecimal(dtBatch.Compute("Sum(PatientDue)", "bIsVoid <> 'Voided'"));
                            lblPatientDue.Text = "Total Patient Due : $" + TotalDue.ToString();
                        }
                        else
                            lblPatientDue.Text = "";

                        Decimal TotalStatmt = Convert.ToDecimal(dtBatch.Compute("Count(nBatchPateintStatDtlID)", "bIsVoid <> 'Voided'"));
                        //c1PatientTemplates.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                        c1PatientTemplates.DataSource = _dv;

                        //c1PatientTemplates.Cols.Count = COL_Count;
                        lblStmtCount.Text = "Number of statements : " + TotalStatmt.ToString();

                        DesignGrid();

                    }
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (ODB != null) { ODB.Dispose(); }
                if (dtBatch != null) { dtBatch.Dispose(); }
            }

        }
        //End code Added by Mayuri:20091208     


        private object GetTagElement(string TagContent, Char Delimeter, Int64 Position)
        {
            string[] temp;
            if (TagContent.Contains(Delimeter.ToString()))
            {
                temp = TagContent.Split(Delimeter);
                return temp[Position - 1];
            }
            else
            {
                return TagContent;
            }
        }

        private void FillCatTemplates(ToolStripDropDownButton tsbCats)
        {
            trv_viewPatientStatement.Nodes.Clear();
            gloTemplate oTemplate = new gloTemplate(_databaseconnectionstring);
            //Added by Mayuri:20091208
            //DataTable dtCategories = new DataTable();
            DataTable dtBatchNames = new DataTable();
            //end code Added by Mayuri:20091208
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTemplates = new DataTable();

            try
            {
                oDB.Connect(false);

                //Code Added by Mayuri:20091208
                //To retrive Batch Names
                dtBatchNames = oTemplate.GetBatchNames();
                if (dtBatchNames != null && dtBatchNames.Rows.Count > 0)
                {
                    for (int i = 0; i < dtBatchNames.Rows.Count; i++)
                    {
                        TreeNode oBatchNode = new TreeNode();
                        oBatchNode.Text = dtBatchNames.Rows[i]["sBatchname"].ToString();
                        oBatchNode.Tag = dtBatchNames.Rows[i]["nBatchPateintStatMstID"].ToString();
                        oBatchNode.Checked = Convert.ToBoolean(dtBatchNames.Rows[i]["bIsVoid"].ToString());
                        if (oBatchNode.Checked && ChkIncludeVoidedBatch.Checked)
                        {
                            trv_viewPatientStatement.Nodes.Add(oBatchNode);
                        }
                        else if (!oBatchNode.Checked && !ChkIncludeVoidedBatch.Checked)
                        {
                            trv_viewPatientStatement.Nodes.Add(oBatchNode);
                        }
                        else if (ChkIncludeVoidedBatch.Checked)
                        {
                            trv_viewPatientStatement.Nodes.Add(oBatchNode);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }
        private void ViewPatientTemplate()
        {
            frmWd_PatientTemplate frm = new frmWd_PatientTemplate(_databaseconnectionstring, _nTrasactionID, true);
            frm.MdiParent = this.ParentForm;
            frm.IsView = true;
            frm.Show();
            frm.WindowState = FormWindowState.Maximized;
        }


        private void PrintBatch(Int64 _nTempleteId)
        {
            //-----------------------------------------------------------------------------
            //frmWd_PatientTemplate frm = new frmWd_PatientTemplate(_databaseconnectionstring, _nTempleteId, true);
            //frm.PrintTemplate();
            //frm.Dispose();
            //-----------------------------------------------------------------------------
        }

        private void ModifyPatientTemplate()
        {
            frmWd_PatientTemplate frm = new frmWd_PatientTemplate(_databaseconnectionstring, _nTrasactionID, true);
            frm.MdiParent = this.ParentForm;
            frm.Show();
            frm.WindowState = FormWindowState.Maximized;
        }

        private void Delete_PatientTemplates()
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ODB.Connect(false);
            string sqlQuery = " DELETE  FROM  PatientTemplates  WHERE  nClinicID =" + _ClinicID + " AND nPatientID = " + _PatientID_To_Delete + " AND  nTransactionID = " + _nTrasactionID + " ";
            try
            {
                ODB.Execute_Query(sqlQuery);
                ODB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (ODB != null) { ODB.Dispose(); }
            }

        }


        #endregion

        #region " C1 Flex Grid"

        private void DesignGrid()
        {
            if (c1PatientTemplates.DataSource == null)
            {
                c1PatientTemplates.Clear(C1.Win.C1FlexGrid.ClearFlags.All);

                c1PatientTemplates.Rows.Count = 1;
                c1PatientTemplates.Rows.Fixed = 1;
                c1PatientTemplates.Cols.Count = COL_Count;
            }

            c1PatientTemplates.AllowEditing = true;

            c1PatientTemplates.Cols[COL_sPatientCode].Caption = "Patient Code";
            //Code added by SaiKrishna
            c1PatientTemplates.Cols[COL_sAccountNo].Caption = "Acct.#";
            c1PatientTemplates.Cols[COL_sFirstName].Caption = "First Name";
            c1PatientTemplates.Cols[COL_sLastName].Caption = "Last Name";
            c1PatientTemplates.Cols[COL_sMiddleName].Caption = "MI";
            c1PatientTemplates.Cols[COL_sUserName].Caption = "User Name";
            c1PatientTemplates.Cols[COL_dtCreateDate].Caption = "Create Date";
            c1PatientTemplates.Cols[COL_dtCreateDate].DataType = typeof(System.DateTime);
            c1PatientTemplates.Cols[COL_dtCreateDate].Format = "MM/dd/yyyy";
            c1PatientTemplates.Cols[COL_dtStatementDate].Caption = "Statement Date";
            c1PatientTemplates.Cols[COL_sVoidNotes].Caption = "Notes";

            c1PatientTemplates.Cols[COL_bIsVoid].Caption = "Status";


            c1PatientTemplates.Cols[COL_sVoidButton].Caption = " ";
            c1PatientTemplates.Cols[COL_sVoidButton].Name = "Note";
            c1PatientTemplates.Cols[COL_PatientDue].Caption = "Patient Due";

            c1PatientTemplates.Cols[COL_sPatientCode].Visible = false;
            c1PatientTemplates.Cols[COL_sFirstName].Visible = true;
            c1PatientTemplates.Cols[COL_sMiddleName].Visible = true;
            c1PatientTemplates.Cols[COL_sLastName].Visible = true;
            c1PatientTemplates.Cols[COL_sUserName].Visible = true;
            c1PatientTemplates.Cols[COL_dtCreateDate].Visible = true;
            c1PatientTemplates.Cols[COL_dtStatementDate].Visible = true;
            c1PatientTemplates.Cols[COL_nTemplateTransactionID].Visible = false;
            c1PatientTemplates.Cols[COL_nBatchDetailID].Visible = false;
            c1PatientTemplates.Cols[COL_nPatientID].Visible = false;
            c1PatientTemplates.Cols[COL_sVoidNotes].Visible = false;
            c1PatientTemplates.Cols[COL_sVoidUserName].Visible = false;
            c1PatientTemplates.Cols[COL_sVoidStatementDateTime].Visible = false;
            //Code added by SaiKrishna
            c1PatientTemplates.Cols[COL_sAccountNo].Visible = true;
            c1PatientTemplates.Cols[COL_nAccountID].Visible = false;
            c1PatientTemplates.Cols[COL_sResult].Visible = false;

            c1PatientTemplates.Cols[COL_sVoidButton].Visible = true;
            c1PatientTemplates.Cols[COL_PatientDue].Visible = true;

            C1.Win.C1FlexGrid.CellStyle csVoidButton;// = c1PatientTemplates.Styles.Add("cs_VoidButton");
            try
            {
                if (c1PatientTemplates.Styles.Contains("cs_VoidButton"))
                {
                    csVoidButton = c1PatientTemplates.Styles["cs_VoidButton"];
                }
                else
                {
                    csVoidButton = c1PatientTemplates.Styles.Add("cs_VoidButton");
                    csVoidButton.DataType = typeof(C1.Win.C1FlexGrid.C1FlexGrid);
                    csVoidButton.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csVoidButton.ComboList = "...";
                }

            }
            catch
            {
                csVoidButton = c1PatientTemplates.Styles.Add("cs_VoidButton");
                csVoidButton.DataType = typeof(C1.Win.C1FlexGrid.C1FlexGrid);
                csVoidButton.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                csVoidButton.ComboList = "...";

            }



            c1PatientTemplates.Cols[COL_PatientDue].Format = "c";
            c1PatientTemplates.Cols[COL_PatientDue].DataType = typeof(System.Decimal);

            if (c1PatientTemplates != null && c1PatientTemplates.Rows.Count > 0)
            {
                //code added by SaiKrishna 04-19-2011
                if (_IsPatientAccountFeature == true)
                {
                    c1PatientTemplates.Cols[COL_sAccountNo].Visible = true;

                }
                else
                {
                    c1PatientTemplates.Cols[COL_sAccountNo].Visible = false;

                }

                for (int RowInd = 1; RowInd < c1PatientTemplates.Rows.Count; RowInd++)
                {
                    //code added by SaiKrishna 04-19-2011
                    //Indicates Account
                    if (Convert.ToInt32(c1PatientTemplates.Rows[1]["sResult"].ToString()) != 1)
                    {
                        c1PatientTemplates.Cols[COL_sPatientCode].Visible = true;
                    }
                    c1PatientTemplates.SetCellStyle(RowInd, COL_sVoidButton, c1PatientTemplates.Styles["cs_VoidButton"]);
                }
            }

            int width = pnl_View.Width - 1;
            c1PatientTemplates.Cols[COL_nTemplateTransactionID].Width = 0;
            c1PatientTemplates.Cols[COL_sPatientCode].Width = 150;
            c1PatientTemplates.Cols[COL_sMiddleName].Width = 100;
            c1PatientTemplates.Cols[COL_sLastName].Width = 150;
            c1PatientTemplates.Cols[COL_sUserName].Width = 150;
            c1PatientTemplates.Cols[COL_dtCreateDate].Width = 90;
            c1PatientTemplates.Cols[COL_dtStatementDate].Width = 152;
            c1PatientTemplates.Cols[COL_nTemplateTransactionID].Width = 150;
            c1PatientTemplates.Cols[COL_sVoidButton].Width = 18;

            c1PatientTemplates.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;
            c1PatientTemplates.Cols[COL_dtStatementDate].Format = "d";

            if (c1PatientTemplates != null && c1PatientTemplates.Cols.Count > 0)
            {
                for (int colInd = 0; colInd < c1PatientTemplates.Cols.Count; colInd++)
                {
                    if (c1PatientTemplates.Cols[colInd].Name != "Note")
                    {
                        c1PatientTemplates.Cols[colInd].AllowEditing = false;
                    }
                }
            }
            if (c1PatientTemplates != null && c1PatientTemplates.Rows.Count > 0)
            {
                for (int RowInd = 1; RowInd < c1PatientTemplates.Rows.Count; RowInd++)
                {
                    if (Convert.ToString(c1PatientTemplates.Rows[RowInd]["bIsVoid"].ToString()) != "Voided")
                    {
                        c1PatientTemplates.Rows[RowInd].AllowEditing = false;
                    }
                }
            }

        }

        private void c1PatientTemplates_DoubleClick(object sender, EventArgs e)
        {
            if (c1PatientTemplates.Rows.Count > 1)
            {
                _nTrasactionID = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nTemplateTransactionID));
                ViewPatientTemplate();
            }
        }

        private void c1PatientTemplates_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        #endregion

        #region " Search"

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sFilter = "";

                string strSearch = txt_Search.Text.Trim();

                //Added By Mukesh Patel For implement Instring Search 20090720
                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");


                if (strSearch.Trim() != "")
                {
                    strSearchArray = strSearch.Split(',');
                }
                //
                if (strSearch.Trim() != "")
                {
                    if (strSearchArray.Length == 1)
                    {
                        //For Single value search 
                        strSearch = strSearchArray[0].Trim();
                        if (strSearch.Length > 1)
                        {
                            string str = strSearch.Substring(1).Replace("%", "");
                            strSearch = strSearch.Substring(0, 1) + str;
                        }
                        if (dtBatch != null && dtBatch.Rows.Count > 0)
                        {
                            //if (c1PatientTemplates != null && c1PatientTemplates.Rows.Count > 1)
                            //{
                            if (c1PatientTemplates.Cols[COL_sPatientCode].Visible == false)
                            {
                                _dv.RowFilter = _dv.Table.Columns["sAccountNo"].ColumnName + " Like '" + strSearch + "%' OR " +
                                _dv.Table.Columns["sFirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                _dv.Table.Columns["sMiddleName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                _dv.Table.Columns["sLastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                _dv.Table.Columns["sUserName"].ColumnName + " Like '" + strSearch + "%'";
                            }
                            else
                            {
                                _dv.RowFilter = _dv.Table.Columns["sPatientCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                _dv.Table.Columns["sFirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                _dv.Table.Columns["sMiddleName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                _dv.Table.Columns["sLastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                _dv.Table.Columns["sUserName"].ColumnName + " Like '" + strSearch + "%'";
                            }

                            //}               
                        }

                    }
                    else
                    {
                        //For Comma separated  value search
                        for (int i = 0; i < strSearchArray.Length; i++)
                        {
                            strSearch = strSearchArray[i].Trim();
                            if (strSearch.Length > 1)
                            {
                                string str = strSearch.Substring(1).Replace("%", "");
                                strSearch = strSearch.Substring(0, 1) + str;
                            }
                            if (strSearch.Trim() != "")
                            {


                                if (sFilter == "")//(i == 0)
                                {
                                    if (dtBatch != null && dtBatch.Rows.Count > 0)
                                    {
                                        if (c1PatientTemplates.Cols[COL_sPatientCode].Visible == false)
                                        {
                                            sFilter = " ( " + _dv.Table.Columns["sAccountNo"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["sFirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["sMiddleName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["sLastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["sUserName"].ColumnName + " Like '" + strSearch + "%')";
                                        }
                                        else
                                        {
                                            sFilter = " ( " + _dv.Table.Columns["sPatientCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["sFirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["sMiddleName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["sLastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["sUserName"].ColumnName + " Like '" + strSearch + "%')";
                                        }
                                    }
                                }
                                else
                                {
                                    if (dtBatch != null && dtBatch.Rows.Count > 0)
                                    {
                                        if (c1PatientTemplates.Cols[COL_sPatientCode].Visible == false)
                                        {
                                            sFilter = sFilter + " AND (" + _dv.Table.Columns["sAccountNo"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["sFirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["sMiddleName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["sLastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                 _dv.Table.Columns["sUserName"].ColumnName + " Like '" + strSearch + "%')";
                                        }
                                        else
                                        {
                                            sFilter = sFilter + " AND (" + _dv.Table.Columns["sPatientCode"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                _dv.Table.Columns["sFirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                _dv.Table.Columns["sMiddleName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                _dv.Table.Columns["sLastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                _dv.Table.Columns["sUserName"].ColumnName + " Like '" + strSearch + "%')";
                                        }

                                    }
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
                //
                DesignGrid();
            }


            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        //Mayuri
        private void HideColumn()
        {
            c1PatientTemplates.Cols[COL_sPatientCode].Caption = "Patient Code";
            c1PatientTemplates.Cols[COL_sFirstName].Caption = "First Name";
            c1PatientTemplates.Cols[COL_sLastName].Caption = "Last Name";
            c1PatientTemplates.Cols[COL_sMiddleName].Caption = "MI";
            c1PatientTemplates.Cols[COL_sUserName].Caption = "User Name";
            c1PatientTemplates.Cols[COL_dtCreateDate].Caption = "Create Date";
            c1PatientTemplates.Cols[COL_dtStatementDate].Caption = "Statement Date";
            c1PatientTemplates.Cols[COL_nTemplateTransactionID].Visible = false;
        }


        #endregion

        #region " Other Events"

        private void cmnuTemplateItem_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                ToolStripMenuItem cmnuTemplateItem = new ToolStripMenuItem();
                cmnuTemplateItem = (ToolStripMenuItem)sender;


                gloTemplate ogloTemplate = new gloTemplate(_databaseconnectionstring);
                ogloTemplate.CategoryID = Convert.ToInt64(cmnuTemplateItem.OwnerItem.Tag);
                ogloTemplate.CategoryName = cmnuTemplateItem.OwnerItem.Text;
                ogloTemplate.TemplateID = Convert.ToInt64(cmnuTemplateItem.Tag);
                ogloTemplate.PrimeryID = Convert.ToInt64(cmnuTemplateItem.Tag);
                ogloTemplate.TemplateName = cmnuTemplateItem.Text;
                if (trv_viewPatientStatement.SelectedNode.Text == "Patient Forms" || trv_viewPatientStatement.SelectedNode.Text == "MIS Reports")
                {
                    ogloTemplate.PatientID = Convert.ToInt64(_PatientID);
                }
                else
                {
                    ogloTemplate.PatientID = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.Row, 12));
                }

                gloOffice.frmWd_PatientTemplate frm = new gloOffice.frmWd_PatientTemplate(_databaseconnectionstring, ogloTemplate, true);
                frm.Form_Closed += new frmWd_PatientTemplate.FormClosed(frm_Form_Closed);
                frm.Text = ogloTemplate.TemplateName;
                frm.MdiParent = this.MdiParent;
                frm.Show();
                frm.WindowState = FormWindowState.Maximized;
            }
        }



        private void pnl_View_SizeChanged(object sender, EventArgs e)
        {
            c1PatientTemplates.Width = pnl_View.Width - 1;

        }

        #endregion

        #region " Patient Strip Control Events "

        void oPatientControl_OnPatientSearchKeyPress(object sender, KeyPressEventArgs e)
        {
            if (oPatientControl.PatientID > 0)
            {
                _PatientID = oPatientControl.PatientID;
                oPatientControl.FillDetails(_PatientID, gloPatientStripControl.FormName.None, 1, false);
                Fill_PatientTemplate();
            }
        }


        void oPatientControl_PatientModified(object sender, EventArgs e)
        {
            try
            {

                if (oPatientControl.PatientID > 0)
                {
                    _PatientID = oPatientControl.PatientID;
                    Fill_PatientTemplate();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void LoadPatientStrip(Int64 PatientId, Int64 PatientProviderId, bool SearchEnable)
        {
            try
            {

                if (oPatientControl != null)
                {
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (oPatientControl.Name == this.Controls[i].Name)
                        {
                            this.Controls.RemoveAt(i);
                            break;
                        }
                    }
                    try
                    {
                        oPatientControl.OnPatientSearchKeyPress -= new gloPatientStripControl.gloPatientStripControl.PatientSearchKeyPressHandler(oPatientControl_OnPatientSearchKeyPress);
                        oPatientControl.PatientModified -= new gloPatientStripControl.gloPatientStripControl.Patient_Modified(oPatientControl_PatientModified);

                    }
                    catch { }
                    oPatientControl.Dispose();
                    oPatientControl = null;
                }
                oPatientControl = new gloPatientStripControl.gloPatientStripControl(_databaseconnectionstring, SearchEnable);
                //oPatientControl.ControlSize_Changed += new gloPatientStripControl.gloPatientStripControl.ControlSizeChanged(oPatientControl_ControlSize_Changed);
                oPatientControl.OnPatientSearchKeyPress += new gloPatientStripControl.gloPatientStripControl.PatientSearchKeyPressHandler(oPatientControl_OnPatientSearchKeyPress);
                oPatientControl.PatientModified += new gloPatientStripControl.gloPatientStripControl.Patient_Modified(oPatientControl_PatientModified);
                oPatientControl.btnDownEnable = true;
                oPatientControl.btnUpEnable = true;
                oPatientControl.DTP.Visible = false;
                oPatientControl.FillDetails(PatientId, gloPatientStripControl.FormName.None, PatientProviderId, false);
                pnlTemplateDetails.Controls.Add(oPatientControl);
                oPatientControl.Dock = DockStyle.Top;
                oPatientControl.Padding = new Padding(0, 0, 3, 0);
                oPatientControl.BringToFront();
                panel2.BringToFront();



                panel1.SendToBack();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        #endregion " Patient Strip Control Events "

        #region " Tree View Events"

        private void trv_viewTemplates_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txt_Search.Text = "";
            if (trv_viewPatientStatement.SelectedNode.Text != "MIS Reports")
            {
                if (e.Node.Tag != null)
                {

                    Fill_PatientTemplate();
                    panel2.Visible = true;
                    if (oPatientControl != null)
                        oPatientControl.Visible = false;
                }

            }
            else
            {
                //To Populate the Child Nodes for the Batch
                //FillSubTree();
            }

        }

        #endregion

        private void tls_btnView_Click(object sender, EventArgs e)
        {
            if (c1PatientTemplates.RowSel < c1PatientTemplates.Rows.Count)
            {
                if (c1PatientTemplates.Rows.Selected != null)
                {
                    if (c1PatientTemplates.RowSel > 0)
                    {
                        //_nTrasactionID = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nTransactionID));
                        _nTrasactionID = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nTemplateTransactionID));
                        //_PatientID_To_Delete = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nPatientID));
                    }
                }
            }
            if (c1PatientTemplates.RowSel < c1PatientTemplates.Rows.Count)
            {
                if (c1PatientTemplates.Rows.Selected != null)
                {
                    if (c1PatientTemplates.RowSel > 0)
                    {
                        if (_nTrasactionID > 0)
                        {
                            //Added by Mayuri:20091210
                            //To open word Template in "Non Editable" mode
                            //ModifyPatientTemplate();
                            ViewPatientTemplate();
                            //End code Added by Mayuri:20091210
                        }
                    }
                }
            }

        }

        private void tls_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_VoidStatment_Click(object sender, EventArgs e)
        {

            try
            {

                Int64 MasterId = Convert.ToInt64(trv_viewPatientStatement.SelectedNode.Tag.ToString().Trim());
                Int32 NodeIndex = trv_viewPatientStatement.SelectedNode.Index;
                Int64 DetailId = 0;
                String Status = "";
                if (c1PatientTemplates.RowSel < c1PatientTemplates.Rows.Count)
                {
                    if (c1PatientTemplates.Rows.Selected != null)
                    {
                        if (c1PatientTemplates.RowSel > 0)
                        {
                            //_nTrasactionID = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nTransactionID));
                            DetailId = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nBatchDetailID));
                            Status = (c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_bIsVoid)).ToString();
                            //_PatientID_To_Delete = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nPatientID));
                        }
                    }
                }
                if (c1PatientTemplates != null && c1PatientTemplates.Rows.Count > 1)
                {
                    if (Status != "Voided")
                    {
                        DialogResult dlgRst = MessageBox.Show("Do you want to void the statement? ", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dlgRst == DialogResult.Yes)
                        {
                            frmVoidStatmentBatch Objfrm = new frmVoidStatmentBatch(MasterId, DetailId, false);
                            Objfrm.PAccountID = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nAccountID));
                            Objfrm.ShowDialog(this);
                            Objfrm.Dispose();
                            Objfrm = null;
                            Fill_PatientTemplate();
                            FillCatTemplates(tsb_CategoryTemplates);
                            trv_viewPatientStatement.SelectedNode = trv_viewPatientStatement.Nodes[NodeIndex];
                        }
                    }
                    else
                    {
                        MessageBox.Show("Statement is already voided.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }

        }

        private void tsb_VoidBatch_Click(object sender, EventArgs e)
        {


            try
            {

                Int64 MasterId = Convert.ToInt64(trv_viewPatientStatement.SelectedNode.Tag.ToString().Trim());
                Boolean IsVoid = trv_viewPatientStatement.SelectedNode.Checked;
                Int32 NodeIndex = trv_viewPatientStatement.SelectedNode.Index;
                Int64 DetailId = 0;
                String Status = "";
                if (c1PatientTemplates.RowSel < c1PatientTemplates.Rows.Count)
                {
                    if (c1PatientTemplates.Rows.Selected != null)
                    {
                        if (c1PatientTemplates.RowSel > 0)
                        {
                            //_nTrasactionID = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nTransactionID));
                            DetailId = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nBatchDetailID));
                            Status = (c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_bIsVoid)).ToString();
                            //_PatientID_To_Delete = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nPatientID));
                        }
                    }
                }
                if (c1PatientTemplates != null && c1PatientTemplates.Rows.Count > 1)
                {
                    if (!IsVoid)
                    {
                        DialogResult dlgRst = MessageBox.Show("Do you want to void the batch? ", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dlgRst == DialogResult.Yes)
                        {
                            frmVoidStatmentBatch Objfrm = new frmVoidStatmentBatch(MasterId, DetailId, true);
                            Objfrm.PAccountID = Convert.ToInt64(c1PatientTemplates.GetData(c1PatientTemplates.RowSel, COL_nAccountID));
                            Objfrm.ShowDialog(this);
                            Objfrm.Dispose();
                            Objfrm = null;

                            Fill_PatientTemplate();
                            FillCatTemplates(tsb_CategoryTemplates);
                            trv_viewPatientStatement.SelectedNode = trv_viewPatientStatement.Nodes[NodeIndex];
                        }
                    }
                    else
                    {
                        MessageBox.Show("Statement batch is already voided.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }

        private void tsb_Send_ResendElectronic_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1PatientTemplates.Rows.Count > 1)
                {
                    if (!trv_viewPatientStatement.SelectedNode.Checked)
                    {
                        generateElectronicStatementBatch(false);
                    }
                    else
                    {
                        MessageBox.Show("Voided batch cannot be resend.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
        }



      //  bool blnBatchPrintinProgress = false;
        private bool CheckBatchPrintProcessRunning()
        {

            try
            {


                foreach (Form oFrm in System.Windows.Forms.Application.OpenForms)
                {

                    if (oFrm.Name == "frmgloPrintBatchPatientStatementController")
                    {
                        DialogResult dg = MessageBox.Show("Background printing is in progress. Do you want to cancel the printing?", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if ((dg == DialogResult.Yes))
                        {
                            oFrm.Close();
                          //  blnBatchPrintinProgress = false;
                            return false;
                           // break; // TODO: might not be correct. Was : Exit For
                        }
                        else
                        {
                            oFrm.Visible = true;
                            return true;
                            //break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }
                return false;
            }
            catch //(Exception ex)
            {
               // ex = null;
                return false;
            }
        }

        private static System.Drawing.Printing.PrinterSettings myPrinterSetting = new System.Drawing.Printing.PrinterSettings();

        private void tsb_Send_Reprint_Click(object sender, EventArgs e)
        {
            
           // frmgloPrintBatchPatientStatementController to check
            try
            {
                if (CheckBatchPrintProcessRunning() == false)
                {
                   // if (blnBatchPrintinProgress == false)
                   // {
                    string OldPrinterName = "";
                        if (c1PatientTemplates.Rows.Count > 1)
                        {
                            if (trv_viewPatientStatement.Nodes.Count > 0)
                            {
                                if (!trv_viewPatientStatement.SelectedNode.Checked)
                                {
                                    ArrayList oListTempleteIds = FetchTempleteId();

                                    if (oListTempleteIds.Count > 0)
                                    {
                                        gloOffice.gloTemplate ogloTemplate;
                                        using (ogloTemplate = new gloTemplate(_databaseconnectionstring))
                                        {
                                          //  Int32 iCount = 0;
                                           // Int64 _ntempTemplateID = 0;
                                            //  Microsoft.Office.Interop.Word.Application oWordApp = new Microsoft.Office.Interop.Word.Application();
                                           // gloWord.LoadAndCloseWord myLoadWord = new gloWord.LoadAndCloseWord();



                                            using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog(true))
                                            {
                                                oDialog.ConnectionString = _databaseconnectionstring;
                                                // blnBatchPrintinProgress = true;
                                                oDialog.TopMost = true;

                                                oDialog.ModuleName = "Printing Batch ReferralLetter";

                                                oDialog.RegistryModuleName = "PrintBatchDocuments";
                                                if (!gloGlobal.gloTSPrint.isCopyPrint)
                                                {
                                                    try
                                                    {
                                                        OldPrinterName = myPrinterSetting.PrinterName;//printDocument1.PrinterSettings.PrinterName;
                                                    }
                                                    catch
                                                    {
                                                    }

                                                    if (oDialog != null)
                                                    {

                                                        oDialog.PrinterSettings = myPrinterSetting;//printDocument1.PrinterSettings;
                                                        oDialog.AllowSomePages = true;
                                                        //oDialog.ShowPrinterProfileDialog = true;
                                                        oDialog.bUseDefaultPrinter = true;

                                                        oDialog.PrinterSettings.ToPage = 1;
                                                        ////maxPage;
                                                        oDialog.PrinterSettings.FromPage = 1;
                                                        oDialog.PrinterSettings.MaximumPage = 1;
                                                        //// maxPage;
                                                        oDialog.PrinterSettings.MinimumPage = 1;
                                                    }
                                                }
                                                oDialog.AllowSomePages = true;

                                                if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                                {

                                                    if ((oDialog.bUseDefaultPrinter == true))
                                                    {
                                                        oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                                        oDialog.CustomPrinterExtendedSettings.IsShowProgress = true;
                                                    }

                                                    frmgloPrintBatchPatientStatementController ogloPrintProgressController = new frmgloPrintBatchPatientStatementController(oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings, null);
                                                    ogloPrintProgressController.OldPrinterName = OldPrinterName;
                                                    ogloPrintProgressController.oListTempleteIds = oListTempleteIds;
                                                    // ogloPrintProgressController.lstgloTemplate = gloTemplates;
                                                    // ogloPrintProgressController.oPatientMessages = oPatientMessages;
                                                    //  ogloPrintProgressController.AccountID = AccountID;

                                                    ogloPrintProgressController._databaseConnectionString = _databaseconnectionstring;
                                                    if (oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint)
                                                    {

                                                        if (oDialog.CustomPrinterExtendedSettings.IsShowProgress)
                                                        {


                                                            ogloPrintProgressController.Show();

                                                        }
                                                        else
                                                        {
                                                            ogloPrintProgressController.Show();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ogloPrintProgressController.TopMost = true;
                                                        ogloPrintProgressController.ShowInTaskbar = false;

                                                        ogloPrintProgressController.ShowDialog();
                                                        if (ogloPrintProgressController != null)
                                                        {
                                                            ogloPrintProgressController.Dispose();
                                                        }
                                                        ogloPrintProgressController = null;


                                                    }
                                                }



                                                //for (iCount = 0; iCount < oListTempleteIds.Count; iCount++)
                                                //{
                                                //    _ntempTemplateID = Convert.ToInt64(oListTempleteIds[iCount]);
                                                //    try
                                                //    {
                                                //        ogloTemplate.PrintTemplate(_ntempTemplateID, ref myLoadWord);
                                                //    }
                                                //    catch (Exception ex1)
                                                //    {
                                                //        MessageBox.Show("Error while Printing.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex1.Message, false);
                                                //    }
                                                //    _ntempTemplateID = 0;
                                                //}
                                                //  myLoadWord.CloseApplicationOnly();
                                                //  myLoadWord = null;




                                                //if (oWordApp != null)
                                                //{   
                                                //    oWordApp.Quit(SaveChanges: false);
                                                //    try
                                                //    {
                                                //        System.Runtime.InteropServices.Marshal.ReleaseComObject(oWordApp);
                                                //        oWordApp = null;
                                                //    }
                                                //    catch
                                                //    {
                                                //    }
                                                //}
                                            }
                                        }
                                        ogloTemplate = null;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Voided batch cannot be reprint.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                   // }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                ex = null;
            }
            finally
            { }
        }

        private ArrayList FetchTempleteId()
        {
            Fill_PatientTemplate();
            ArrayList oList = new ArrayList();
            String Status = string.Empty;
            try
            {

                for (int i = 1; i < c1PatientTemplates.Rows.Count; i++)
                {
                    Status = (c1PatientTemplates.GetData(i, COL_bIsVoid)).ToString();
                    if (Status != "Voided")
                        oList.Add(c1PatientTemplates.GetData(i, COL_nTemplateTransactionID));
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return oList;
            // throw new NotImplementedException();
        }

        private Int64 generateElectronicStatementBatch(bool _blnPrintReport)
        {

            Int64 BatchID = 0;

            try
            {
                ArrayList oListPatientIds = FetchPatientId();
                //Code added by SaiKrishna
                ArrayList oListAccountIds = FetchAccountId();

                string _FilePath = string.Empty;
                if (oListPatientIds.Count > 0)
                {
                    this.Parent.Cursor = Cursors.WaitCursor;
                    this.Cursor = Cursors.WaitCursor;
                }
                Application.DoEvents();

                BatchID = Convert.ToInt64(trv_viewPatientStatement.SelectedNode.Tag.ToString().Trim());
                Fill_PatientTemplate();

                #region "Generate electronic file"

                string _BatchName = trv_viewPatientStatement.SelectedNode.Text.ToString().Trim();
                //7022Items:.STA extension for GatewayEDI statements
                //Pass file extension to file path depends on default clearing house in gloPMAdmin.
                gloStatment ObjClsgloStatment = new gloStatment();
                string _sStmtFileExtent = ObjClsgloStatment.getStatementFileExtension();

                gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                try
                {
                    oDB.Connect(false);
                    //#region "Get File path"
                    //string _ServerPath = Application.StartupPath;

                    //7022Items:.STA extension for GatewayEDI statements
                    //Pass file extension to file path depends on default clearing house in gloPMAdmin.
                    _FilePath = gloSettings.FolderSettings.AppTempFolderPath + _BatchName + _sStmtFileExtent;

                    //#endregion "Get File path"
                    ClsgloElectronic objClsgloElectronic = new ClsgloElectronic(_databaseconnectionstring);

                    if (oListPatientIds.Count > 0)
                    {
                        //pnlProgressBar.Visible = true;
                        //prgFileGeneration.Visible = true;
                        this.Parent.Cursor = Cursors.WaitCursor;
                        this.Cursor = Cursors.WaitCursor;
                    }
                    Application.DoEvents();
                    string _sqlQuery = string.Empty;
                    _sqlQuery = " SELECT nStatementCriteriaID FROM RPT_Patstatementcriteria_MST WITH (NOLOCK) WHERE bitIsDefault = 1 AND criteriaType = 'DISPLAY' ";
                    DataTable dtTemp = new DataTable();
                    Int64 nStatementCriteriaID = 0;
                    oDB.Retrive_Query(_sqlQuery, out dtTemp);
                    if (dtTemp.Rows.Count > 0)
                        nStatementCriteriaID = Convert.ToInt64(dtTemp.Rows[0]["nStatementCriteriaID"].ToString());
                    else
                        nStatementCriteriaID = 0;
                    //EndDate = dtCriteriaEndDate.Value.ToShortDateString();



                    //string _FilePath = string.Empty;
                    //gloStatment ObjClsgloStatment = new gloStatment();
                    string _ServerPath = ObjClsgloStatment.GetServerPath();
                    ObjClsgloStatment.Dispose();
                    string _BaseFolder = "Claim Management";
                    string _OutInFolder = "OutBox";
                    string _ClaimFolder = "Statements";
                    string _claimFolderPath = "";

                    _claimFolderPath = _ServerPath + "\\" + _BaseFolder + "\\" + _OutInFolder + "\\" + _ClaimFolder;

                    #region Save File
                    try
                    {
                        if (System.IO.Directory.Exists(_claimFolderPath) == false)
                        {
                            System.IO.Directory.CreateDirectory(_claimFolderPath);
                        }

                        if (getCopyEDIFiles() == 1)
                        {

                            string _BatchFolderPath = _claimFolderPath + "\\" + _BatchName;
                            if (System.IO.Directory.Exists(_BatchFolderPath) == false)
                            {
                                System.IO.Directory.CreateDirectory(_BatchFolderPath);
                            }
                            //7022Items:.STA extension for GatewayEDI statements
                            //Pass file extension to file path depends on default clearing house in gloPMAdmin.
                            _FilePath = _BatchFolderPath + "\\" + _BatchName + _sStmtFileExtent;
                        }

                    }
                    catch (Exception EX)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), false);
                        EX = null;
                    }
                    #endregion

                    if (System.IO.File.Exists(_FilePath) == true)
                    {
                        System.IO.File.Delete(_FilePath);
                    }
                    String UnclosedDate = c1PatientTemplates.GetData(1, COL_dtStatementDate).ToString().Substring(0, 10);
                    String EndDate = Convert.ToDateTime(UnclosedDate).ToShortDateString();

                    //Code added by SaiKrishna o4-27-2011
                    if (_IsPatientAccountFeature == true)
                    {
                        objClsgloElectronic.IsBusinessCenterEnable = _isBusinessCenterEnable;
                        Int64 _nPAccountID = Convert.ToInt64(oListAccountIds[0].ToString());
                        objClsgloElectronic.BusinessCenterID = gloGlobal.gloPMGlobal.GetAccountBusinessCenterID(_nPAccountID);

                        objClsgloElectronic.GenerateElectonicClaimFileWithVersion2(oListPatientIds, EndDate, nStatementCriteriaID, _FilePath, oListAccountIds, false);
                    }
                    else
                    {
                        object value = null;
                        ogloSettings.GetSetting("STATEMENT_VERSION", out value);

                        if (value != null && Convert.ToString(value) != "")
                        {
                            objClsgloElectronic.IsBusinessCenterEnable = _isBusinessCenterEnable;
                            Int64 _nPAccountID = Convert.ToInt64(oListAccountIds[0].ToString());
                            objClsgloElectronic.BusinessCenterID = gloGlobal.gloPMGlobal.GetAccountBusinessCenterID(_nPAccountID);

                            if (Convert.ToString(value) == "2")
                            {

                                objClsgloElectronic.GenerateElectonicClaimFileWithVersion2(oListPatientIds, EndDate, nStatementCriteriaID, _FilePath, oListAccountIds, false);
                            }
                            else
                            {
                                objClsgloElectronic.GenerateElectonicClaimFile(oListPatientIds, EndDate, nStatementCriteriaID, _FilePath, oListAccountIds);
                            }
                        }
                        else
                        {
                            objClsgloElectronic.GenerateElectonicClaimFile(oListPatientIds, EndDate, nStatementCriteriaID, _FilePath, oListAccountIds);
                        }

                    }

                    oDBParameters.Add("@BatchID", BatchID, ParameterDirection.Input, SqlDbType.BigInt);
                    gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);
                    Byte[] oTemplate = ogloTemplate.ConvertFileToBinary(_FilePath);
                    oDBParameters.Add("@iBatchStatementFile", oTemplate, ParameterDirection.Input, SqlDbType.Image);
                    oDB.Execute("gsp_IniBatchStatementFile_MST", oDBParameters);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Batch, gloAuditTrail.ActivityType.ResendToClaimManager, "Statement named-" + Convert.ToString(_BatchName) + ", Resend to Claim Manager", 0, BatchID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    MessageBox.Show("Resend Batch Done. ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (ogloSettings != null) { ogloSettings.Dispose(); }
                    if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                    if (oDBParameters != null) { oDBParameters.Dispose(); }
                    //7022Items:.STA extension for GatewayEDI statements
                    //dispose object.
                    if (ObjClsgloStatment != null) { ObjClsgloStatment.Dispose(); }
                }
                #endregion "Generate electronic file"


                //pnlProgressBar.Visible = false;
                //prgFileGeneration.Visible = false;
                this.Parent.Cursor = Cursors.Default;
                this.Cursor = Cursors.Default;

            }
            catch (IOException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //pnlPleasewait.Visible = false;
                this.Parent.Cursor = Cursors.Default;
                this.Cursor = Cursors.Default;
            }

            return BatchID;
        }

        public Int32 getCopyEDIFiles()
        {
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(_databaseconnectionstring);
            DataTable dtversion = new DataTable();
            dtversion = oSetting.GetSetting("COPY_EDI_FILES", 0);
            if (dtversion != null && dtversion.Rows.Count > 0)
            {
                return Convert.ToInt32(dtversion.Rows[0]["sSettingsValue"]);
            }
            else
            {
                return 1;
            }
        }

        private ArrayList FetchPatientId()
        {
            ArrayList oList = new ArrayList();
            String Status = string.Empty;
            try
            {

                for (int i = 1; i < c1PatientTemplates.Rows.Count; i++)
                {
                    Status = (c1PatientTemplates.GetData(i, COL_bIsVoid)).ToString();
                    if (Status != "Voided")
                        oList.Add(c1PatientTemplates.GetData(i, COL_nPatientID));
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return oList;
        }

        //Code added by SaiKrishna
        private ArrayList FetchAccountId()
        {
            ArrayList oList = new ArrayList();
            String Status = string.Empty;
            try
            {

                for (int i = 1; i < c1PatientTemplates.Rows.Count; i++)
                {
                    Status = (c1PatientTemplates.GetData(i, COL_bIsVoid)).ToString();
                    if (Status != "Voided")
                        oList.Add(c1PatientTemplates.GetData(i, COL_nAccountID));
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return oList;
        }

        private void frmWd_BatchPatientStatement_FormClosing(object sender, FormClosingEventArgs e)
        {
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            try
            {
                oSettings.WriteSettings_XML("VoidedStatementBatch", "ChkVoidBatch", ChkIncludeVoidedBatch.Checked.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oSettings != null) { oSettings.Dispose(); }
            }
        }

        private void ChkIncludeVoidedBatch_CheckedChanged(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1PatientTemplates, false);
          //  c1PatientTemplates.Clear();
            c1PatientTemplates.DataSource = null;
            c1PatientTemplates.Row = _rowselect;
            try
            {
                FillCatTemplates(tsb_CategoryTemplates);
                if (trv_viewPatientStatement.Nodes.Count > 0)
                {
                    trv_viewPatientStatement.SelectedNode = trv_viewPatientStatement.Nodes[0];
                }
                DesignGrid();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void c1PatientTemplates_CellButtonClick(object sender, RowColEventArgs e)
        {
            string _voidNotes = "";
            string _voidUserName = "";
            int _xCord = 0;
            int _yCord = 0;
            int _Height = 0;
            Point defPnt = new Point();
            int _rowIndex = c1PatientTemplates.RowSel - 1;
            GetCursorPos(ref defPnt);
            _xCord = defPnt.X;
            _yCord = defPnt.Y;
            _Height = c1PatientTemplates.Rows[0].Height + 10;
            DateTime _voidDateTime = new DateTime();
            if (c1PatientTemplates != null && c1PatientTemplates.Rows.Count > 0)
            {
                _rowIndex = c1PatientTemplates.RowSel;
                _voidDateTime = Convert.ToDateTime(c1PatientTemplates.GetData(_rowIndex, "VoidDateTime"));
                _voidUserName = Convert.ToString(c1PatientTemplates.GetData(_rowIndex, "UserName"));
                _voidNotes = Convert.ToString(c1PatientTemplates.GetData(_rowIndex, "sVoidNotes"));
            }

            frmVoidStatementInfoDialog objVoidNotes = new frmVoidStatementInfoDialog(_voidDateTime, _voidUserName, _voidNotes, _xCord, _yCord + _Height);
            objVoidNotes.ShowDialog(this);
            objVoidNotes.Dispose();
            objVoidNotes = null;
        }

    }
}