using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Office.Core;
using Wd = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;

namespace gloReports
{
    public partial class frmRpt_Revised_PatientStatement : Form
    {

        #region " Declarations "
        //For Creating the Object of the Report
        Rpt_Paper_PatientStatement objrptPatientStatementForGateWayEDI = new Rpt_Paper_PatientStatement();
        dsRevisedPatientStatement _dsPatientStatement = null;
        private string _databaseconnectionstring = "";
        DataView _dv = new DataView();
        private Int64 _nPatientID;
        private Int64 _UserID = 0;
        private string _UserName = "";
        public bool _isIndvidual = false;
        public bool _isGenerateBatch = false;
        public bool _generateBatchFlag = false;
        public bool _IsIndividualTrue = false;
        public bool _IsExcluded = false;
        private ComboBox combo;
        ToolTip tooltip_Rpt = new ToolTip();
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationSettings.AppSettings;
        private Int64 _ClinicID = 0;
        private AxDSOFramer.AxFramerControl wdTemplate;
        private Wd.Document oCurDoc;
        private Wd.Document oTempDoc;
        private Wd.Application oWordApp;
        DataTable dtTemp;
        gloDatabaseLayer.DBLayer oDB;
        string _sqlQuery = string.Empty;
        Int64 nStatementCriteriaID = 0;
        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        private gloGeneralItem.gloItems ogloItems = null;
        private static frmRpt_Revised_PatientStatement frm;

        #endregion " Declarations "

        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "

        #region "Constructors"

        public frmRpt_Revised_PatientStatement(string databaseconnectionstring, Int64 nPatientID)
        {
            InitializeComponent();
            _nPatientID = nPatientID;
            _databaseconnectionstring = databaseconnectionstring;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrive UserID from appSettings "

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

            #endregion
            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "";
                }
            }
            else
            { _MessageBoxCaption = ""; }
            cmbSettings.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSettings.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            #endregion
            
        }

        #endregion

        #region " Form Get Instance Methods "

        private bool blnDisposed;

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!(this.blnDisposed))
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if ((disposing))
                {
                    // Dispose managed resources. 
                    if ((components != null))
                    {
                        components.Dispose();
                    }
                    //frm = Nothing 
                }
                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed. 

                // Note that this is not thread safe. 
                // Another thread could start disposing the object 
                // after the managed resources are disposed, 
                // but before the disposed flag is set to true. 
                // If thread safety is necessary, it must be 
                // implemented by the client. 
            }
            frm = null;
            this.blnDisposed = true;

        }

        public void Dispose()
        {
            Dispose(true);
            // Take yourself off of the finalization queue 
            // to prevent finalization code for this object 
            // from executing a second time. 
            System.GC.SuppressFinalize(this);
        }

        protected void Finalize()
        {
            Dispose(false);
        }

        public static frmRpt_Revised_PatientStatement GetInstance(string databaseconnectionstring, Int64 nPatientID)
        {
            try
            {
                if (frm != null)
                {
                    frm.Show();
                    frm.BringToFront();
                }
                else
                {
                    frm = new frmRpt_Revised_PatientStatement(databaseconnectionstring, nPatientID);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            { }
            return frm;
        }
        #endregion " Form Get Instance Methods "

        #region "Form Events"

        private void frmRpt_Revised_PatientStatement_Load(object sender, EventArgs e)
        {
            string CloseDate = getCloseDate();
            try
            {
                dsEOBPaymentReports dsReports = new dsEOBPaymentReports();
                SetButtonVisibility("FormLoad");
                btnUp.BackgroundImage = global::gloReports.Properties.Resources.UP;
                btnUp.BackgroundImageLayout = ImageLayout.Center;
                if (CloseDate != "")
                {
                    dtpEndDate.Value = Convert.ToDateTime(CloseDate);
                    dtCriteriaEndDate.Value = Convert.ToDateTime(CloseDate);
                }
                FetchCriteriasCombo();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            gloC1FlexStyle.Style(c1PatientList, true);
        }

        private void frmRpt_Revised_PatientStatement_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (objrptPatientStatementForGateWayEDI != null)
            {
                if (objrptPatientStatementForGateWayEDI.IsLoaded)
                {
                    objrptPatientStatementForGateWayEDI.Close();
                }
                objrptPatientStatementForGateWayEDI.Dispose();
            }
            this.Dispose();
        }

        #endregion "Form Events"

        #region "Toolstrip Events"

        private void tsb_ViewStatement_Click(object sender, EventArgs e)
        {
            SetButtonVisibility("Generate");
            Decimal _TotalPatientDue = 0;
            try
            {
                if (ValidateData())
                {
                        Int64 _nPatientID = 0;
                        if (_isIndvidual == false) // Batch
                        {
                            if (c1PatientList.DataSource != null)
                            {
                                if (c1PatientList.Rows.Count > 1)
                                {
                                        if (Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index))!= "")
                                        {
                                            _nPatientID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index));
                                        }
                                }
                            }
                            _TotalPatientDue = GetIndividualPatientBalance(_nPatientID);
                            label81.Text = Convert.ToString(_TotalPatientDue);
                            btnUp_Click(null, null);
                        }
                        else // Individual
                        {
                            if (Convert.ToString(cmbPatients.SelectedValue) != "" && Convert.ToString(cmbPatients.SelectedValue) != "0")
                            {
                                _nPatientID = Convert.ToInt64(cmbPatients.SelectedValue);
                            }
                        }
                        _TotalPatientDue = GetIndividualPatientBalance(_nPatientID);
                        label81.Text = Convert.ToString(_TotalPatientDue);
                        fillRevisedPatientStatement(_nPatientID);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_btnIndividual_Click(object sender, EventArgs e)
        {
            _isIndvidual = true;
            SetButtonVisibility("Individual");
            string CloseDate = getCloseDate();
            dtCriteriaEndDate.Text = CloseDate;
            DataTable dtPat = new DataTable();
            DataTable dtIndividual = new DataTable();
            Decimal _TotalPatientDue = 0;
            try
            {
                if (_nPatientID != 0)
                {
                    string sPatientName = GetPatientName(_nPatientID);
                    dtPat.Columns.Add("ID");
                    dtPat.Columns.Add("Name");
                    DataRow dr = dtPat.NewRow();
                    dr["ID"] = _nPatientID;
                    dr["Name"] = sPatientName;
                    dtPat.Rows.Add(dr);
                    dtPat.AcceptChanges();
                    cmbPatients.DataSource = dtPat;
                    cmbPatients.DisplayMember = "Name";
                    cmbPatients.ValueMember = "ID";
                    cmbPatients.SelectedIndex = 0;
                    lblptName.Text = cmbPatients.Text.Trim() + " :";
                    dtIndividual = FillIndividualDetails();
                    #region "Fill Individual Details"
                    _TotalPatientDue = GetIndividualPatientBalance(_nPatientID);
                    label81.Text = Convert.ToString(_TotalPatientDue);
                    if (dtIndividual != null && dtIndividual.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtIndividual.Rows.Count; i++)
                        {
                            lbldtcreate.Text = dtIndividual.Rows[0]["dtcreateDate"].ToString();
                            lblUName.Text = dtIndividual.Rows[0]["sUserName"].ToString();
                            lbldtstdate.Text = Convert.ToDateTime(dtIndividual.Rows[0]["dtStatementDate"].ToString()).ToShortDateString();
                        }
                    }
                    #endregion

                    fillRevisedPatientStatement(_nPatientID);
                }
                gloC1FlexStyle.Style(c1PatientList, true);
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
        }

        private void tsb_btnBatch_Click(object sender, EventArgs e)
        {
            _isIndvidual = false;
            SetButtonVisibility("Batch");
         }

        private void tsb_Send_Electronic_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isIndvidual && cmbPatients.SelectedValue == null)
                {
                    MessageBox.Show("Please select the patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (ValidateData())
                {
                    Int64 BatchID = generateElectronicStatementBatch(false);
                    SetButtonVisibility("SendBatch");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isIndvidual && cmbPatients.SelectedValue == null)
                {
                    MessageBox.Show("Please select the patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (ValidateData())
                {
                    generatePaperStatementBatch(true);
                    SetButtonVisibility("SendBatch");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_GenerateBatch_Click(object sender, EventArgs e)
        {
            _isGenerateBatch = true;
            SetButtonVisibility("GenerateBatch");
            try
            {
                if (ValidateData())
                {

                    if (cmbSettings.SelectedValue != null)
                    {
                        if (cmbSettings.SelectedValue.ToString() != "0")
                        {
                            panel5.Visible = true;
                            FillControlsPerCriteria(Convert.ToInt64(cmbSettings.SelectedValue));
                            ShowPatientListOnC1Grid();
                            #region "Fill Batch Details"

                            if (cmbSettings.SelectedValue != null)
                            {
                                if (cmbSettings.SelectedValue.ToString() != "0")
                                {
                                    lblSettings.Text = cmbSettings.Text.ToString();
                                    FillBatchDetails();
                                }
                            }

                            #endregion
                            if (c1PatientList.Rows.Count > 1)
                                tsb_ViewStatement.Visible = true;
                            else
                            {
                                MessageBox.Show("No patient found for selected setting.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tsb_ViewStatement.Visible = false;
                            }
                            gloC1FlexStyle.Style(c1PatientList, true);
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }

        }

        private void tsb_btnShowList_Click(object sender, EventArgs e)
        {
            tsb_GenerateBatch.Visible = true;
            tsb_btnShowList.Visible = false;
            pnlFilteredPatList.Visible = true;
            btnDown.Visible = false;
            btnUp.Visible = true;
        }

        #endregion

        #region "Private Methods"

        #region "Send Electronic and paper Statement"

        private Int64 generateElectronicStatementBatch(bool _blnPrintReport)
        {

            Int64 BatchID = 0;
            pnlPleasewait.Visible = true;
            Boolean _bPrinted = false;
            string _sPrinterName = "";
            PrintDialog _PrintDialog = null;
            try
            {

                Int64 nPatientID = 0;
                int _PageCount = 0;
                ArrayList oListPatientIds = FetchPatientId();
                string _FilePath = string.Empty;
                prgFileGeneration.Value = 0;
                prgFileGeneration.Minimum = 0;
                prgFileGeneration.Maximum = oListPatientIds.Count;
                if (oListPatientIds.Count > 0)
                {
                    pnlProgressBar.Visible = true;
                    prgFileGeneration.Visible = true;
                    this.Parent.Cursor = Cursors.WaitCursor;
                    this.Cursor = Cursors.WaitCursor;
                }
                Application.DoEvents();

                BatchID = CreateBatch_Mst();
                if (BatchID > 0)
                {
                    for (int i = 0; i < oListPatientIds.Count; i++)
                    {
                        string _FileName = "PatientStatement_" + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + "_To_" + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + "_" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + System.DateTime.Now.Millisecond + ".doc";
                        if (Directory.Exists(appSettings["StartupPath"].ToString() + "\\MIStemp") == false)
                        {
                            Directory.CreateDirectory(appSettings["StartupPath"].ToString() + "\\MIStemp");
                        }

                        _FilePath = appSettings["StartupPath"].ToString() + "\\MIStemp\\" + _FileName;


                        nPatientID = Convert.ToInt64(oListPatientIds[i]);

                        //To fill the Reports 
                        fillRevisedPatientStatement(nPatientID);

                        if (objrptPatientStatementForGateWayEDI != null && objrptPatientStatementForGateWayEDI.IsLoaded)
                        {
                            #region "Exporting to Doc"

                            objrptPatientStatementForGateWayEDI.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, _FilePath);

                            wdTemplate = new AxDSOFramer.AxFramerControl();
                            wdTemplate.OnDocumentClosed += wdTemplate_OnDocumentClosed;
                            this.Controls.Add(wdTemplate);
                            wdTemplate.Open(_FilePath);
                            oCurDoc = new Microsoft.Office.Interop.Word.Document();
                            oCurDoc = (Microsoft.Office.Interop.Word.Document)wdTemplate.ActiveDocument;

                            //oCurDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                            String sFileName = gloOffice.Supporting.NewDocumentName();

                            object oFileName = (object)sFileName;
                            object missing = System.Reflection.Missing.Value;
                            object oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                            oCurDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                            wdTemplate.Close();

                            #endregion

                            SavePatientTemplate(_FileName, sFileName, nPatientID, BatchID);
                            if (_blnPrintReport == true)
                            {
                                if (!_bPrinted)
                                {
                                    _PrintDialog.AllowSomePages = true;
                                    crvReportViewer.ShowLastPage();
                                    _PageCount = crvReportViewer.GetCurrentPageNumber();

                                    _PrintDialog.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.SomePages;
                                    _PrintDialog.PrinterSettings.FromPage = 1;
                                    _PrintDialog.PrinterSettings.ToPage = _PageCount;
                                    if (_PrintDialog.PrinterSettings.ToPage > _PageCount)
                                    {
                                        MessageBox.Show("The page range is invalid. Enter number between " + _PrintDialog.PrinterSettings.FromPage + " and " + _PageCount + "", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return 0;
                                    }
                                    if (_PrintDialog.ShowDialog() == DialogResult.OK)
                                    {
                                        objrptPatientStatementForGateWayEDI.PrintOptions.PrinterName = _PrintDialog.PrinterSettings.PrinterName;
                                        _sPrinterName = _PrintDialog.PrinterSettings.PrinterName.ToString();
                                        objrptPatientStatementForGateWayEDI.PrintToPrinter(0, false, _PrintDialog.PrinterSettings.FromPage, _PageCount);
                                        _PrintDialog.AllowSomePages = true;
                                    }
                                    _bPrinted = true;
                                }
                                else
                                {
                                    objrptPatientStatementForGateWayEDI.PrintOptions.PrinterName = _sPrinterName;
                                    objrptPatientStatementForGateWayEDI.PrintToPrinter(0, false, _PrintDialog.PrinterSettings.FromPage, _PageCount);
                                    _PrintDialog.AllowSomePages = true;
                                }

                            }
                        }

                        Application.DoEvents();
                        prgFileGeneration.Value = i + 1;
                        lblFile.Text = "Processing Batch " + prgFileGeneration.Value + "/" + oListPatientIds.Count;
                    }
                    try
                    {
                        System.IO.Directory.Delete(appSettings["StartupPath"].ToString() + "\\MIStemp", true);
                    }
                    catch (Exception EX)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), false);
                        EX = null;
                    }

                    #region "Generate electronic file"

                    string _BatchName = string.Empty;

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                    try
                    {
                        string _sqlQuery = "select sBatchName from dbo.BL_Batch_PatientStatement_Mst where dbo.BL_Batch_PatientStatement_Mst.nBatchPateintStatMstID = " + BatchID;
                        oDB.Connect(false);
                        _BatchName = oDB.ExecuteScalar_Query(_sqlQuery).ToString();

                        //#region "Get File path"

                        //string _ServerPath = Application.StartupPath;
                        string _Path = appSettings["StartupPath"].ToString();
                        string _BsFolder = "Temp";
                        string _claimFldrPath = "";

                        _claimFldrPath = _Path + "\\" + _BsFolder;

                        if (System.IO.Directory.Exists(_claimFldrPath) == false)
                        {
                            System.IO.Directory.CreateDirectory(_claimFldrPath);
                        }
                        _FilePath = _claimFldrPath + "\\" + _BatchName + ".txt";

                        //#endregion "Get File path"

                     
                        ClsgloElectronic objClsgloElectronic = new ClsgloElectronic(_databaseconnectionstring);
                        prgFileGeneration.Value = 0;
                        prgFileGeneration.Minimum = 0;
                        prgFileGeneration.Maximum = oListPatientIds.Count;
                        if (oListPatientIds.Count > 0)
                        {
                            pnlProgressBar.Visible = true;
                            prgFileGeneration.Visible = true;
                            this.Parent.Cursor = Cursors.WaitCursor;
                            this.Cursor = Cursors.WaitCursor;
                        }
                        Application.DoEvents();
                        _sqlQuery = " SELECT nStatementCriteriaID FROM RPT_Patstatementcriteria_MST WHERE bitIsDefault = 1 AND criteriaType = 'DISPLAY' ";
                        dtTemp = new DataTable();
                        oDB.Retrive_Query(_sqlQuery, out dtTemp);
                        if (dtTemp.Rows.Count > 0)
                            nStatementCriteriaID = Convert.ToInt64(dtTemp.Rows[0]["nStatementCriteriaID"].ToString());
                        else
                            nStatementCriteriaID = 0;

                        string EndDate = string.Empty;
                        if (_isIndvidual)
                        {
                            EndDate = dtpEndDate.Value.ToShortDateString();
                        }
                        else
                        {
                            EndDate = dtCriteriaEndDate.Value.ToShortDateString();
                        }


                        //string _FilePath = string.Empty;
                        string _ServerPath = GetServerPath();
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

                                _FilePath = _BatchFolderPath + "\\" + _BatchName + ".txt";
                            }

                        }
                        catch (Exception EX)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), false);
                            EX = null;
                        }
                            #endregion

                        objClsgloElectronic.GenerateElectonicClaimFile(oListPatientIds, EndDate, nStatementCriteriaID, _FilePath);

                        oDBParameters.Add("@BatchID", BatchID, ParameterDirection.Input, SqlDbType.BigInt);
                        gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);
                        Byte[] oTemplate = ogloTemplate.ConvertFileToBinary(_FilePath);
                        oDBParameters.Add("@iBatchStatementFile", oTemplate, ParameterDirection.Input, SqlDbType.Image);

                        oDB.Execute("sp_IniBatchStatementFile_MST", oDBParameters);

                     
                        }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    finally
                    {
                        if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                        if (oDBParameters != null) { oDBParameters.Dispose(); }
                    }
                    MessageBox.Show("Generation of Batch Done. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    #endregion "Generate electronic file"

                    if (_isIndvidual == true)
                        FillIndividualBatchSummary();
                    else
                        FillBatchDetails();
                   

                    pnlProgressBar.Visible = false;
                    prgFileGeneration.Visible = false;
                    this.Parent.Cursor = Cursors.Default;
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    MessageBox.Show("Unable to generate batch. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                pnlPleasewait.Visible = false;
                this.Parent.Cursor = Cursors.Default;
                this.Cursor = Cursors.Default;
            }

            return BatchID;
        }

        private Int64 generatePaperStatementBatch(bool _blnPrintReport)
        {

            pnlPleasewait.Visible = true;

            Int64 _nBatchID = 0;
            Boolean _bPrinted = false;
            string _sPrinterName = "";
            PrintDialog _PrintDialog = null;
            int _PageCount = 0;

            try
            {
                _PrintDialog = new PrintDialog();
                if (_isIndvidual)
                {
                    _PrintDialog.AllowSomePages = true;
                    crvReportViewer.ShowLastPage();
                    _PageCount = crvReportViewer.GetCurrentPageNumber();
                    _PrintDialog.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.SomePages;
                    _PrintDialog.PrinterSettings.FromPage = 1;
                    _PrintDialog.PrinterSettings.ToPage = _PageCount;
                }

                if (_PrintDialog.ShowDialog() != DialogResult.Cancel)
                {
                    if (_isIndvidual)
                    {
                        if (_PrintDialog.PrinterSettings.ToPage > _PageCount)
                        {
                            MessageBox.Show("The page range is invalid. Enter number between " + _PrintDialog.PrinterSettings.FromPage + " and " + _PageCount + "", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return 0;
                        }
                    }
                   
                    ArrayList oListPatientIds = FetchPatientId();
                    string _FilePath = "";
                    prgFileGeneration.Value = 0;
                    prgFileGeneration.Minimum = 0;
                    prgFileGeneration.Maximum = oListPatientIds.Count;
                    if (oListPatientIds.Count > 0)
                    {
                        pnlProgressBar.Visible = true;
                        prgFileGeneration.Visible = true;
                        this.Parent.Cursor = Cursors.WaitCursor;
                        this.Cursor = Cursors.WaitCursor;
                    }
                    this.Invalidate();
                    this.Refresh();

                    if (oListPatientIds.Count > 0)
                    {
                        _nBatchID = CreateBatch_Mst();
                    }

                    if (_nBatchID != null && _nBatchID > 0)
                    {
                        Int64 nPatientID = 0;
                        string _FileName = String.Empty;  
                        for (int i = 0; i < oListPatientIds.Count; i++)
                        {
                            _FileName = "PatientStatement_" + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + "_To_" + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + "_" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + System.DateTime.Now.Millisecond + ".doc";
                            
                            if (Directory.Exists(appSettings["StartupPath"].ToString() + "\\MIStemp") == false)
                            {
                                Directory.CreateDirectory(appSettings["StartupPath"].ToString() + "\\MIStemp");
                            }
                            
                            _FilePath = appSettings["StartupPath"].ToString() + "\\MIStemp\\" + _FileName;

                            nPatientID = Convert.ToInt64(oListPatientIds[i]);

                            //To fill the Reports 
                            fillRevisedPatientStatement(nPatientID);

                            if (objrptPatientStatementForGateWayEDI != null && objrptPatientStatementForGateWayEDI.IsLoaded)
                            {
                                #region "Exporting to Doc"

                                objrptPatientStatementForGateWayEDI.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, _FilePath);

                                wdTemplate = new AxDSOFramer.AxFramerControl();
                                wdTemplate.OnDocumentClosed += wdTemplate_OnDocumentClosed;
                                this.Controls.Add(wdTemplate);
                                wdTemplate.Open(_FilePath);
                                oCurDoc = new Microsoft.Office.Interop.Word.Document();
                                oCurDoc = (Microsoft.Office.Interop.Word.Document)wdTemplate.ActiveDocument;

                                //oCurDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                                String sFileName = gloOffice.Supporting.NewDocumentName();

                                object oFileName = (object)sFileName;
                                object missing = System.Reflection.Missing.Value;
                                object oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                                oCurDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                                wdTemplate.Close();

                                #endregion

                                SavePatientTemplate(_FileName, sFileName, nPatientID, _nBatchID);
                                
                                if (_blnPrintReport == true)
                                {
                                    #region "Print report"

                                    if (!_bPrinted)
                                    {

                                        if (_isIndvidual)
                                        {
                                            objrptPatientStatementForGateWayEDI.PrintOptions.PrinterName = _PrintDialog.PrinterSettings.PrinterName;
                                            _sPrinterName = _PrintDialog.PrinterSettings.PrinterName.ToString();
                                            objrptPatientStatementForGateWayEDI.PrintToPrinter(0, false, _PrintDialog.PrinterSettings.FromPage, _PageCount);
                                            _bPrinted = true;
                                        }
                                        else
                                        {
                                            objrptPatientStatementForGateWayEDI.PrintOptions.PrinterName = _PrintDialog.PrinterSettings.PrinterName;
                                            _sPrinterName = _PrintDialog.PrinterSettings.PrinterName.ToString();
                                            objrptPatientStatementForGateWayEDI.PrintToPrinter(0, false, 0, 0);
                                            _bPrinted = true;
                                        }

                                    }
                                    else
                                    {
                                        if (_isIndvidual)
                                        {
                                            objrptPatientStatementForGateWayEDI.PrintOptions.PrinterName = _sPrinterName;
                                            objrptPatientStatementForGateWayEDI.PrintToPrinter(0, false, _PrintDialog.PrinterSettings.FromPage, _PageCount);
                                        }
                                        else
                                        {
                                            objrptPatientStatementForGateWayEDI.PrintOptions.PrinterName = _sPrinterName;
                                            objrptPatientStatementForGateWayEDI.PrintToPrinter(0, false, 0, 0);
                                        }
                                    }
                                    #endregion "Print report"
                                }
                            }
                           
                            prgFileGeneration.Value = i + 1;
                            lblFile.Text = "Processing Batch " + prgFileGeneration.Value + "/" + oListPatientIds.Count;
                            this.Invalidate();
                            this.Refresh();  

                        }
                        
                        if(_isIndvidual == true)
                            FillIndividualBatchSummary();
                        else
                            FillBatchDetails();

                        pnlProgressBar.Visible = false;
                        prgFileGeneration.Visible = false;
                        this.Parent.Cursor = Cursors.Default;
                        this.Cursor = Cursors.Default;
                        try
                        {
                            System.IO.Directory.Delete(appSettings["StartupPath"].ToString() + "\\MIStemp", true);
                        }
                        catch (IOException exIo)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(exIo.ToString(), false);
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unable to generate batch. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
                pnlPleasewait.Visible = false;
                this.Parent.Cursor = Cursors.Default;
                this.Cursor = Cursors.Default;
            }

            return _nBatchID;
        }

        #endregion "Send Electronic and paper Statement"

        #region "Paper Patient Statment"

        private void fillRevisedPatientStatement(Int64 PatientID)
        {
           oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
                {
                    oDB.Connect(false);
                    string CloseDate = getCloseDate();

                    if (CloseDate == "")
                    {
                        _dsPatientStatement = new dsRevisedPatientStatement();
                        objrptPatientStatementForGateWayEDI.SetDataSource(_dsPatientStatement);
                        crvReportViewer.ReportSource = objrptPatientStatementForGateWayEDI;
                        objrptPatientStatementForGateWayEDI.SetParameterValue(0, "");
                    }
                    else
                    {
                        #region "Exclude from Statement" 

                        _sqlQuery = " SELECT nPatientID FROM PatientSettings WHERE sValue = 1 AND sName = 'Exclude from Statement' AND nPatientID = " + _nPatientID + " ";
                        dtTemp = new DataTable();
                        oDB.Retrive_Query(_sqlQuery, out dtTemp);
                        
                        if (dtTemp.Rows.Count > 0)
                        {
                            if (MessageBox.Show("Patient is marked to suppress Statements.\nContinue with a new Statement?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                tsb_Send.Visible = false;
                                _IsExcluded = false;
                                return;
                            }
                            else { _IsExcluded = true; }
                        }

                        #endregion "Exclude from Statement"

                        crvReportViewer.ReportSource = null;
                        _dsPatientStatement = new dsRevisedPatientStatement();

                        #region "Remit Settings"
                        _sqlQuery = " SELECT nStatementCriteriaID FROM RPT_Patstatementcriteria_MST WHERE bitIsDefault = 1 AND criteriaType = 'DISPLAY' ";
                        dtTemp = new DataTable();
                        oDB.Retrive_Query(_sqlQuery, out dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            nStatementCriteriaID = Convert.ToInt64(dtTemp.Rows[0]["nStatementCriteriaID"].ToString());
                            fetchRevisedRemitDetails(nStatementCriteriaID, _dsPatientStatement);
                            //******* added on 20100611 by sandip dhakane******
                            fetchPayToDetails(nStatementCriteriaID, PatientID, _dsPatientStatement);
                        }
                        else
                        {
                            fetchRevisedRemitDetails(0, _dsPatientStatement);
                            //******** added on 20100611 by sandip dhakane*****
                            fetchPayToDetails(nStatementCriteriaID, PatientID, _dsPatientStatement);
                        }
                        #endregion

                        #region "Display Settings"
                        
                        fetchRevisedDisplaySettings(_ClinicID, PatientID, _dsPatientStatement);
                        
                        #endregion
                        
                        #region "Statement Notes"

                        int EndDate = 0;
                        if (_isIndvidual)
                        {
                            EndDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Text);
                        }
                        else
                        {
                            EndDate = gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Text);
                        }

                        _sqlQuery = "SELECT sStatementNote FROM Patient_Statement_Notes WHERE nPatientID = " + PatientID + " AND nClinicID = " + _ClinicID + " AND ( nfromdate <= " + EndDate + " AND nToDate >= " + EndDate + ")";
                        dtTemp = new DataTable();
                        oDB.Retrive_Query(_sqlQuery, out dtTemp);

                        string _sStatementNotes = string.Empty;
                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtTemp.Rows.Count; i++)
                            {
                                _sStatementNotes = _sStatementNotes + Convert.ToString(dtTemp.Rows[i]["sStatementNote"]);
                            }
                        }
                        #endregion

                        objrptPatientStatementForGateWayEDI = CreateReport(PatientID, EndDate, ClinicID, _dsPatientStatement, objrptPatientStatementForGateWayEDI);
                        objrptPatientStatementForGateWayEDI.SetParameterValue(0, _sStatementNotes);
                        crvReportViewer.ReportSource = objrptPatientStatementForGateWayEDI;
                        
                    }
                }
                catch (gloDatabaseLayer.DBException ex)
                {
                    ex.ERROR_Log(ex.ToString());
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    _dsPatientStatement = null;
                    if (oDB != null) {oDB.Disconnect();  oDB.Dispose(); }
                }
            
        }

        private void fetchRevisedRemitDetails(Int64 CriteriaID, dsRevisedPatientStatement _dsPatientStatement)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;
            try
            {

                oConnection.ConnectionString = _databaseconnectionstring;

                _sqlcommand.CommandText = "SELECT ISNULL(sRemitName,'') as sRemitName, ISNULL(sRemitAddress1,'') as sRemitAddress1," +
                                          " ISNULL(sRemitAddress2,'') as sRemitAddress2," +
                                          " ISNULL(sRemitCity,'') as sRemitCity ," +
                                          " ISNULL(sRemitState,'') as sRemitState ," +
                                          " ISNULL(sRemitZip,'') as sRemitZip," +
                                          " ISNULL(sClinicMessage1,'') as sClinicMessage1 " +
                                          " from RPT_PatStatementCriteria_Display where nStatementCriteriaID =" + CriteriaID + "";
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(_dsPatientStatement, "dt_RemitInfo");

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null) { _sqlcommand.Dispose(); }
                if (oConnection != null) { oConnection.Dispose(); }
                if (da != null) { da.Dispose(); }
            }
        }


        //************************ Added on 20100611 by sandip dhakane*************************

        private void fetchPayToDetails(Int64 CriteriaID, Int64 PatientID, dsRevisedPatientStatement _dsPatientStatement)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;
            Int64 nPayableTo = 0;
            try
            {


                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                _sqlQuery = "SELECT ISNULL(nPayableTo,0) as nPayableTo from RPT_PatStatementCriteria_MST where nStatementCriteriaID =" + CriteriaID + "";
                oDB.Connect(false);
                dtTemp = new DataTable();
                oDB.Retrive_Query(_sqlQuery, out dtTemp);

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    nPayableTo = Convert.ToInt64(dtTemp.Rows[0]["nPayableTo"]);
                }

                oConnection.ConnectionString = _databaseconnectionstring;

                if (nPayableTo != 0)
                {
                    _sqlcommand.CommandText = "SELECT ISNULL(sRemitName,'') as sPayToName, ISNULL(sRemitAddress1,'') as sPayToAddress1," +
                                              " ISNULL(sRemitAddress2,'') as sPayToAddress2," +
                                              " ISNULL(sRemitCity,'') as sPayToCity ," +
                                              " ISNULL(sRemitState,'') as sPayToState ," +
                                              " ISNULL(sRemitZip,'') as sPayToZip " +
                                              " from RPT_PatStatementCriteria_Display where nStatementCriteriaID =" + CriteriaID + " AND nAddressType = " + nPayableTo + "";
                }
                else
                {
                    _sqlcommand.CommandText = "SELECT ISNULL(Patient.nPatientID, 0) AS PatientID, " +
                                      " ISNULL(Patient.sFirstName, '''') + SPACE(1) + ISNULL(Patient.sMiddleName, '''') + SPACE(1) + ISNULL(Patient.sLastName, '''') AS sPatientName, " +
                                      " ISNULL(Patient.sAddressLine1, '''') AS sPatAddress1, ISNULL(Patient.sAddressLine2, '''') AS sPatAddress2, ISNULL(Patient.sCity, '''') AS sPatCity, " +
                                      " ISNULL(Patient.sState, '''') AS sPatState, ISNULL(Patient.sZIP, '''') AS sPatZip,ISNULL(Patient.sPhone, '''') AS sPatPhone," +
                                      " ISNULL(Provider_MST.sFirstName, '''') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '''') + SPACE(1) + ISNULL(Provider_MST.sLastName, '''') AS sPayToName, " +
                                      " ISNULL(Provider_MST.sPracticeAddressline1, '''') AS sPayToAddress1,ISNULL(Provider_MST.sPracticeAddressline2, '''') AS sPayToAddress2," +
                                      " ISNULL(Provider_MST.sPracticeCity, '''') AS sPayToCity,ISNULL(Provider_MST.sPracticeState, '''') AS sPayToState, ISNULL(Provider_MST.sPracticeZIP, '''') AS sPayToZip, " +
                                      " ISNULL(Provider_MST.sPracPhoneNo, '''') AS sProviderPhone, ISNULL(Clinic_MST.sClinicName, '') AS sPracName, ISNULL(Clinic_MST.sAddress1, '') AS sPracAddress1, " +
                                      " ISNULL(Clinic_MST.sAddress2, '') AS sPracAddress2, ISNULL(Clinic_MST.sCity, '') AS sPracCity, ISNULL(Clinic_MST.sState, '') AS sPracState," +
                                      " ISNULL(Clinic_MST.sZIP, '') AS sPracZip, ISNULL(   replace(replace(replace(replace(Clinic_MST.sphoneno,'(',''),')',''),'-',''),' ',''), '') AS sPracPhone, ISNULL(Clinic_MST.sURL, '') AS sPracURL, ISNULL(Clinic_MST.sEmail, '') AS sPracEmail " +
                                      " FROM Patient INNER JOIN " +
                                      " Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID INNER JOIN " +
                                      " Clinic_MST ON Patient.nClinicID = Clinic_MST.nClinicID " +
                                      " WHERE Patient.nPatientID= " + PatientID;

                }
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(_dsPatientStatement, "dt_PayTo");

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null) { _sqlcommand.Dispose(); }
                if (oConnection != null) { oConnection.Dispose(); }
                if (da != null) { da.Dispose(); }
            }
        }




        //************************************************************************



        private void fetchRevisedDisplaySettings(Int64 _nClinicID, Int64 PatientID, dsRevisedPatientStatement _dsPatientStatement)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;
            try
            {

                //oConnection.ConnectionString = _databaseconnectionstring;
                //_sqlcommand.CommandText = "SELECT ISNULL(Patient.nPatientID, 0) AS PatientID, isnull(Patient.sPatientCode,0) as sPatientCode, " +
                //                        " ISNULL(Patient.sFirstName, '''') + SPACE(1) + ISNULL(Patient.sMiddleName, '''') + SPACE(1) + ISNULL(Patient.sLastName, '''') AS sPatientName, " +
                //                        " ISNULL(Patient.sAddressLine1, '''') AS sPatAddress1, ISNULL(Patient.sAddressLine2, '''') AS sPatAddress2, ISNULL(Patient.sCity, '''') AS sPatCity, " +
                //                        " ISNULL(Patient.sState, '''') AS sPatState, ISNULL(Patient.sZIP, '''') AS sPatZip,ISNULL(Patient.sPhone, '''') AS sPatPhone," +
                //                        " ISNULL(Provider_MST.sFirstName, '''') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '''') + SPACE(1) + ISNULL(Provider_MST.sLastName, '''') AS sProviderName, " +
                //                        " ISNULL(Provider_MST.sPracticeAddressline1, '''') AS sProviderAddress1,ISNULL(Provider_MST.sPracticeAddressline2, '''') AS sProviderAddress2," +
                //                        " ISNULL(Provider_MST.sPracticeCity, '''') AS sProviderCity,ISNULL(Provider_MST.sPracticeState, '''') AS sProviderState, ISNULL(Provider_MST.sPracticeZIP, '''') AS sProviderZip, " +
                //                        " ISNULL(Provider_MST.sPracPhoneNo, '''') AS sProviderPhone, ISNULL(Clinic_MST.sClinicName, '') AS sPracName, ISNULL(Clinic_MST.sAddress1, '') AS sPracAddress1, " +
                //                        " ISNULL(Clinic_MST.sAddress2, '') AS sPracAddress2, ISNULL(Clinic_MST.sCity, '') AS sPracCity, ISNULL(Clinic_MST.sState, '') AS sPracState," +
                //                        " ISNULL(Clinic_MST.sZIP, '') AS sPracZip, ISNULL(   replace(replace(replace(replace(Clinic_MST.sphoneno,'(',''),')',''),'-',''),' ',''), '') AS sPracPhone, ISNULL(Clinic_MST.sURL, '') AS sPracURL, ISNULL(Clinic_MST.sEmail, '') AS sPracEmail " +
                //                        " FROM Patient INNER JOIN " +
                //                        " Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID INNER JOIN " +
                //                        " Clinic_MST ON Patient.nClinicID = Clinic_MST.nClinicID " +
                //                        " WHERE Patient.nPatientID= " + PatientID;

                //_sqlcommand.Connection = oConnection;
                //_sqlcommand.CommandTimeout = 0;
                //da = new SqlDataAdapter(_sqlcommand);
                //da.Fill(_dsPatientStatement, "dt_DisplaySettings");

                oConnection.ConnectionString = _databaseconnectionstring;

                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "GET_Patient_DisplaySettings";
                SqlParameter ParaPatient = new SqlParameter();
                {
                    ParaPatient.ParameterName = "@nPatientID";
                    ParaPatient.Value = PatientID;
                    ParaPatient.Direction = ParameterDirection.Input;
                    ParaPatient.SqlDbType = SqlDbType.BigInt;
                }
                _sqlcommand.Parameters.Add(ParaPatient);
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;
                oConnection.Open();
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(_dsPatientStatement, "dt_DisplaySettings");

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null) { _sqlcommand.Dispose(); }
                if (oConnection != null) { oConnection.Dispose(); }
                if (da != null) { da.Dispose(); }
            }
        }

        public Rpt_Paper_PatientStatement CreateReport(Int64 PatientID, int EndDate, Int64 ClinicID, dsRevisedPatientStatement dsPatientStatement, Rpt_Paper_PatientStatement objoCharge)
        {
            SqlConnection oConnection = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                #region "Fetch Ageing Bucket Info"

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "BL_SELECT_AgingBuckets";
                SqlParameter ParaPatient = new SqlParameter();
                {
                    ParaPatient.ParameterName = "@nPatientID";
                    ParaPatient.Value = PatientID;
                    ParaPatient.Direction = ParameterDirection.Input;
                    ParaPatient.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaPatient);
                SqlParameter ParaEnd = new SqlParameter();
                {
                    ParaEnd.ParameterName = "@nEndDate";
                    ParaEnd.Value = EndDate;
                    ParaEnd.Direction = ParameterDirection.Input;
                    ParaEnd.SqlDbType = SqlDbType.Int;
                }
                sqlCmd.Parameters.Add(ParaEnd);
                SqlParameter ParaClinic = new SqlParameter();
                {
                    ParaClinic.ParameterName = "@nClinicID ";
                    ParaClinic.Value = ClinicID;
                    ParaClinic.Direction = ParameterDirection.Input;
                    ParaClinic.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaClinic);
                sqlCmd.Connection = oConnection;
                sqlCmd.CommandTimeout = 0;
                oConnection.Open();
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(dsPatientStatement, "dt_AgeingBucket");
                #endregion "Fetch Ageing Bucket Info"

                #region "get_patient_account_balance"

                sqlCmd = new SqlCommand();
                string CloseDate = getCloseDate();
                //Fill the dt_PatientCharges_payment table in dataset present in gloReports using store procedure 
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "BL_GET_PATIENT_BALANCE";
                SqlParameter ParaAccPatientID = new SqlParameter();
                {
                    ParaAccPatientID.ParameterName = "@nPatientID";
                    ParaAccPatientID.Value = PatientID;
                    ParaAccPatientID.Direction = ParameterDirection.Input;
                    ParaAccPatientID.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaAccPatientID);
                SqlParameter ParaEndByDate = new SqlParameter();
                {
                    ParaEndByDate.ParameterName = "@nDate";
                    ParaEndByDate.Value = EndDate;
                    ParaEndByDate.Direction = ParameterDirection.Input;
                    ParaEndByDate.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaEndByDate);
                SqlParameter ParaAccClinicID = new SqlParameter();
                {
                    ParaAccClinicID.ParameterName = "@nClinicID";
                    ParaAccClinicID.Value = ClinicID;
                    ParaAccClinicID.Direction = ParameterDirection.Input;
                    ParaAccClinicID.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaAccClinicID);
                sqlCmd.Connection = oConnection;
                sqlCmd.CommandTimeout = 0;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(dsPatientStatement, "dt_PatientReserve");

                #endregion

                #region "Fetch Patient Statement Info"

                sqlCmd = new SqlCommand();

                //Fill the dt_PatientCharges_payment table in dataset present in gloReports using store procedure 
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "RPT_PatientStatement_Revised";
                SqlParameter ParaPatientID = new SqlParameter();
                {
                    ParaPatientID.ParameterName = "@nPatientID";
                    ParaPatientID.Value = PatientID;
                    ParaPatientID.Direction = ParameterDirection.Input;
                    ParaPatientID.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaPatientID);
                SqlParameter ParaEndDate = new SqlParameter();
                {
                    ParaEndDate.ParameterName = "@nDate";
                    ParaEndDate.Value = EndDate;
                    ParaEndDate.Direction = ParameterDirection.Input;
                    ParaEndDate.SqlDbType = SqlDbType.Int;
                }
                sqlCmd.Parameters.Add(ParaEndDate);
                SqlParameter ParaClinicID = new SqlParameter();
                {
                    ParaClinicID.ParameterName = "@nClinicID";
                    ParaClinicID.Value = ClinicID;
                    ParaClinicID.Direction = ParameterDirection.Input;
                    ParaClinicID.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaClinicID);
                sqlCmd.Connection = oConnection;
                sqlCmd.CommandTimeout = 0;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(dsPatientStatement, "dt_PatientStatement_Revised");
                #endregion

                //Assign dataset to the report
                if (!object.ReferenceEquals(dsPatientStatement, null))
                    objoCharge.SetDataSource(dsPatientStatement);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (sqlCmd != null) { sqlCmd.Dispose(); }
                if (oConnection != null) { oConnection.Close(); oConnection.Dispose(); }
                if (da != null) { da.Dispose(); }
            }
            return objoCharge;
        }

        #endregion

        private ArrayList FetchPatientId()
        {
            ArrayList oList = new ArrayList();
            try
            {
                if (_isIndvidual == false) //Batch 
                {
                    for (int i = 1; i < c1PatientList.Rows.Count; i++)
                    {
                        oList.Add(c1PatientList.GetData(i, c1PatientList.Cols["PatientID"].Index));
                    }
                }
                else //Individual
                {
                        if (Convert.ToString(cmbPatients.SelectedValue) != "")
                        {
                            oList.Add(cmbPatients.SelectedValue);
                        }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return oList;
        }

        private void FilterPatientByName()
        {
            try
            {
                if (c1PatientList.DataSource != null)
                {
                    _dv = ((DataView)c1PatientList.DataSource);
                    string sColumnName = "sLastName";
                    string sFilter = "(";

                    //Display Patients Name Start with From [A] To [Z]
                    if (Convert.ToString(lblNameFrom.Text) != "" && Convert.ToString(lblNameTo.Text) != "")
                    {
                        char[] From = lblNameFrom.Text.ToCharArray();
                        char[] To = lblNameTo.Text.ToCharArray();

                        if (From[0] <= To[0])
                        {
                            for (char i = From[0]; i <= To[0]; i++)
                            {
                                if (i == From[0])
                                    sFilter += sColumnName + " like '" + i.ToString() + "%' ";
                                else
                                    sFilter += " OR " + sColumnName + " like '" + i.ToString() + "%' ";
                            }
                        }
                        else
                        {
                            for (char i = To[0]; i <= From[0]; i++)
                            {
                                if (i == To[0])
                                    sFilter += sColumnName + " like '" + i.ToString() + "%' ";
                                else
                                    sFilter += " OR " + sColumnName + " like '" + i.ToString() + "%' ";
                            }
                        }
                        _dv.RowFilter = sFilter + " )";
                    }

                    c1PatientList.DataSource = _dv;
                    _dv = (DataView)c1PatientList.DataSource;
                    _dv.EndInit();
                    if (_generateBatchFlag)
                    {
                        int _c1PatientListColumnCount = c1PatientList.Rows.Count - 1;
                        lblCount.Text = _c1PatientListColumnCount.ToString();
                        Decimal _TotalPatientDue = 0;
                        if (_c1PatientListColumnCount > 0)
                        {
                                for (int i = 1; i < c1PatientList.Rows.Count; i++)
                                {
                                    _TotalPatientDue = _TotalPatientDue + Convert.ToDecimal(c1PatientList.GetData(i, "spatientDue"));
                                }
                                lblTotaldue.Text = _TotalPatientDue.ToString();
                        }
                        else
                        {
                            tsb_Send.Visible = false;
                            tsb_ViewStatement.Visible = false;
                            tsb_GenerateBatch.Visible = true;
                            tsb_btnShowList.Visible = false;
                            lblCount.Text = "0";
                            lblTotaldue.Text = "0";
                        }
                    }
                }
                else
                {
                    lblCount.Text = "0";
                    lblTotaldue.Text = "0";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillBatchDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = FillDetails();
                if (dt != null && dt.Rows.Count > 0)
                {                  
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                                                       
                                lblSettings.Text = dt.Rows[0]["sSettingName"].ToString() + " :";
                                lblUserName.Text = dt.Rows[0]["sUserName"].ToString();
                                lbldtStatementDate.Text = Convert.ToDateTime(dt.Rows[0]["dtStatementDate"].ToString()).ToShortDateString();
                                lblmaxCreateDate.Text = dt.Rows[0]["dtCreateDate"].ToString();
                                                       
                        }                                        
                }
                else
                {
                   
                    lbldtStatementDate.Text = "";
                    lblUserName.Text = "";
                    lblmaxCreateDate.Text = "";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dt != null) { dt.Dispose(); }
            }
        }

        private void FillIndividualBatchSummary()
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Decimal _TotalPatientDue = 0;
            Int64 nPatientID = 0;

            try
            {
                ODB.Connect(false);
                if (cmbPatients.SelectedValue != null)
                {
                    if (cmbPatients.SelectedValue.ToString() != "0")
                    {
                        nPatientID = Convert.ToInt64(cmbPatients.SelectedValue);
                    }
                    _TotalPatientDue = GetIndividualPatientBalance(nPatientID);
                    label81.Text = Convert.ToString(_TotalPatientDue);
                }
                else
                {
                    label81.Text = "0";
                }
                lblptName.Text = cmbPatients.Text.Trim() + " :";

                if (lblptName.Text != " :")
                {
                    DataTable dtIndividual = new DataTable();
                    dtIndividual = FillIndividualDetails();
                    if (dtIndividual != null && dtIndividual.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtIndividual.Rows.Count; i++)
                        {
                            lbldtcreate.Text = dtIndividual.Rows[0]["dtCreateDate"].ToString().Trim();
                            lblUName.Text = dtIndividual.Rows[0]["sUserName"].ToString();
                            lbldtstdate.Text = Convert.ToDateTime(dtIndividual.Rows[0]["dtStatementDate"].ToString()).ToShortDateString();
                        }
                    }
                }
                else
                {
                    lbldtcreate.Text = "";
                    lblUName.Text = "";
                    lbldtstdate.Text = "";
                    label81.Text = "0";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
            }
        }
        
        private System.Data.DataTable FillDetails()
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ODB.Connect(false);
            DataTable dt = new DataTable();
            try
            {
                string sqlQuery = "";
                sqlQuery = "select sUserName,dtCreateDate ,dtStatementDate,sSettingName from BL_Batch_PatientStatement_MST where sBatchName like '" + cmbSettings.Text + "%' order by  dtCreateDate desc";
                ODB.Retrive_Query(sqlQuery, out dt);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
            }
            return dt;
        }
        
        private System.Data.DataTable FillIndividualDetails()
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ODB.Connect(false);
            DataTable dt = new DataTable();
            try
            {
                string sqlQuery = "";
                sqlQuery = "select sUserName, dtCreateDate,dtStatementDate from BL_Batch_PatientStatement_Mst where sBatchName like '" + cmbPatients.Text.Trim().Replace("'", "''") + "%' order by dtcreateDate desc";
                ODB.Retrive_Query(sqlQuery, out dt);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
                if (dt != null) { dt.Dispose(); }
            }
            return dt;
        }

        private Int64 CreateBatch_Mst()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Int64 _nResult = 0;
            
            try
            {
                //Master Batch Entry in BL_Batch_PatientStatement_Mst table to maintain statement generated history.....
                oDB.Connect(false);
                string sBatchName = "";

                if (_isIndvidual == false)
                { sBatchName = cmbSettings.Text.ToString() + " " + gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString()); }
                else
                { sBatchName = cmbPatients.Text.ToString() + " " + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()); }

                if (_isIndvidual == false)
                {
                    oDBParameters.Add("@nBatchPateintStatMstID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@nPatientID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@dtCreateDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBParameters.Add("@nSettingID", cmbSettings.SelectedValue, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sSettingName", cmbSettings.Text.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@sBatchName", sBatchName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sUserName", _UserName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@dtStatementDate", dtCriteriaEndDate.Value, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBParameters.Add("@nCrWaitDays", 30, ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@nCrBalance", Convert.ToDecimal(lblDueAmt.Text.ToString()), ParameterDirection.Input, SqlDbType.Decimal);
                    oDBParameters.Add("@sCrPateintFromName", lblNameFrom.Text.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@sCrPateintToName", lblNameTo.Text.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@bStatus", true, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                }
                else
                {
                    oDBParameters.Add("@nBatchPateintStatMstID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@nPatientID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@dtCreateDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBParameters.Add("@nSettingID", cmbPatients.SelectedValue, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sSettingName", cmbPatients.Text.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@sBatchName", sBatchName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sUserName", _UserName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@dtStatementDate", dtpEndDate.Value, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBParameters.Add("@nCrWaitDays", 30, ParameterDirection.Input, SqlDbType.Int);
                    if (lblDueAmt.Text.ToString() != String.Empty)
                    {
                        oDBParameters.Add("@nCrBalance", Convert.ToDecimal(lblDueAmt.Text.ToString()), ParameterDirection.Input, SqlDbType.Decimal);
                    }
                    else
                    {
                        oDBParameters.Add("@nCrBalance",0, ParameterDirection.Input, SqlDbType.Decimal);
                    }
                    oDBParameters.Add("@sCrPateintFromName", lblNameFrom.Text.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@sCrPateintToName", lblNameTo.Text.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@bStatus", true, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                }

                Object oValue = null;
                oDB.Execute("sp_INSERT_BL_Batch_PatientStatement_Mst", oDBParameters, out oValue);

                if (Convert.ToString(oValue) != "")
                {
                    _nResult = Convert.ToInt64(oValue);
                }
               

            }
            catch (Exception ex)
            {

               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);     
               
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            return _nResult;

        }

        private void ShowPatientListOnC1Grid()
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            Int64 stDate = 0;
            Int64 endDate = 0;
            Decimal dueAmt = 0;
            DateTime _filterDate = new DateTime();
            TimeSpan _tWaitDays = new TimeSpan();
            SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
            DataTable _dtPatients = new DataTable();
            try
            {
                if (lblWaitDays.Text.ToString() != "")
                {
                    _tWaitDays = new TimeSpan(Convert.ToInt32(lblWaitDays.Text.ToString()), 0, 0, 0);
                    _filterDate = dtCriteriaEndDate.Value.Subtract(_tWaitDays);
                }

                if (_isIndvidual == false)
                {
                    dtCriteriaStartDate.Checked = true;
                    dtCriteriaEndDate.Checked = true;
                    stDate = gloDateMaster.gloDate.DateAsNumber(dtCriteriaStartDate.Value.ToShortDateString());
                    endDate = gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString());

                }
                else
                {
                    stDate = gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString());
                    endDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString());
                }

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "GET_Patient_DueList";
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;

                if (endDate != 0)
                {
                    _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.BigInt);
                    _sqlcommand.Parameters["@nEndDate"].Value = endDate;
                }
                if (Convert.ToString(lblDueAmt.Text) != "")
                {
                    dueAmt = Convert.ToDecimal(Convert.ToString(lblDueAmt.Text));
                }

                _sqlcommand.Parameters.Add("@nDueAmt", System.Data.SqlDbType.Decimal);
                _sqlcommand.Parameters["@nDueAmt"].Value = dueAmt;

                _sqlcommand.Parameters.Add("@nClinicID", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nClinicID"].Value = _ClinicID;

                _sqlcommand.Parameters.Add("@nDateCriteria", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nDateCriteria"].Value = gloDateMaster.gloDate.DateAsNumber(_filterDate.ToShortDateString().ToString());

                da.Fill(_dtPatients);
                da.Dispose();

                if (_dtPatients != null && _dtPatients.Rows.Count > 0)
                {
                    DataView _dv = _dtPatients.DefaultView;
                    c1PatientList.DataSource = _dv;
                    c1PatientList.Cols["PatientID"].Visible = false;
                    c1PatientList.Cols["sLastName"].Visible = true;
                    c1PatientList.Cols["sPatientCode"].Visible = true;
                    c1PatientList.Cols["sMiddleName"].Visible = true;
                    c1PatientList.Cols["sFirstName"].Visible = true;
                    c1PatientList.Cols["dtDOB"].Visible = true;
                    c1PatientList.Cols["sPhone"].Visible = true;
                    c1PatientList.Cols["sMobile"].Visible = true;
                    c1PatientList.Cols["sPatientName"].Visible = false;
                    c1PatientList.Cols["sSSN"].Visible = true;
                    c1PatientList.Cols["sProviderName"].Visible = true;
                    c1PatientList.Cols["spatientDue"].Visible = false;

                    c1PatientList.Cols["sPatientCode"].Caption = "Code";
                    c1PatientList.Cols["sLastName"].Caption = "Last Name";
                    c1PatientList.Cols["sMiddleName"].Caption = "MI";
                    c1PatientList.Cols["sFirstName"].Caption = "First Name";
                    c1PatientList.Cols["dtDOB"].Caption = "DOB";
                    c1PatientList.Cols["sPhone"].Caption = "Phone";
                    c1PatientList.Cols["sMobile"].Caption = "Mobile";
                    c1PatientList.Cols["sSSN"].Caption = "SSN";
                    c1PatientList.Cols["sProviderName"].Caption = "Provider";

                    c1PatientList.Cols["sPatientCode"].Width = 130;
                    c1PatientList.Cols["sLastName"].Width = 130;
                    c1PatientList.Cols["sMiddleName"].Width = 40;
                    c1PatientList.Cols["sFirstName"].Width = 130;
                    c1PatientList.Cols["dtDOB"].Width = 130;
                    c1PatientList.Cols["sPhone"].Width = 130;
                    c1PatientList.Cols["sMobile"].Width = 130;
                    c1PatientList.Cols["sSSN"].Width = 130;
                    c1PatientList.Cols["sProviderName"].Width = 160;
                    c1PatientList.Rows[0].Selected = true;
                }
                else
                {
                    c1PatientList.DataSource = null;
                    c1PatientList.Rows.Count = 1;
                    c1PatientList.Rows.Fixed = 1;
                    c1PatientList.Cols.Count = 1;
                    tsb_Send.Visible = false;
                }
                c1PatientList.AutoResize = false;
                c1PatientList.AllowEditing = false;
                _dtPatients = null;

                FilterPatientByName();

                gloC1FlexStyle.Style(c1PatientList, true);
            }
            catch (Exception ex)
            {
                tsb_Send.Visible = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null) { _sqlcommand.Dispose(); }
                if (oConnection != null) { oConnection.Close(); oConnection.Dispose(); }
                if (_dtPatients != null) { _dtPatients.Dispose(); }
            }
        }

        private Decimal GetIndividualPatientBalance(Int64 _nPatientID)
        {
            int _CloseDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Text);
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
            Decimal _TotalPatientDue = 0;
            DataTable dtTotalPatientDue = new DataTable();
            try
            {

                ODB.Connect(false);
                oDBPatameters.Add("@nPatientId", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@nDate", _CloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@nClinicId", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                ODB.Retrive("BL_GET_PATIENT_BALANCE", oDBPatameters, out dtTotalPatientDue);
                if (dtTotalPatientDue != null && dtTotalPatientDue.Rows.Count > 0)
                {
                    _TotalPatientDue = Convert.ToDecimal(dtTotalPatientDue.Rows[0]["PatientDue"].ToString()) - Convert.ToDecimal(dtTotalPatientDue.Rows[0]["AvailableReserve"].ToString());
                }
                else
                {
                    _TotalPatientDue = 0;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Disconnect(); ODB.Dispose(); }
                if (oDBPatameters != null) { oDBPatameters.Dispose(); }
                if (dtTotalPatientDue != null) { dtTotalPatientDue.Dispose(); }
            }
            return _TotalPatientDue;
        }

        private string GetPatientName(Int64 patientID)
        {

            DataTable dtPatient = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _strSQL = "";
            string _result = "";
            try
            {
                oDB.Connect(false);

                //get the provider details in the datatable -- dtProvider
                _strSQL = "select ISNULL( sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) + ISNULL(sLastName,'') AS PatientName FROM Patient WHERE nPatientID = " + patientID;
                oDB.Retrive_Query(_strSQL, out dtPatient);
                if (dtPatient.Rows.Count > 0)
                {
                    _result = Convert.ToString(dtPatient.Rows[0]["PatientName"]);
                }

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), true);
                DBErr = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _result;

        }

        private void SetButtonVisibility(string tabName)
        {
            switch (tabName)
            {
                #region "Individual"
                case "Individual":
                    tsb_ViewStatement.Visible = true;
                    tsb_Send.Visible = true;
                    tsb_ViewStatement.Enabled = true;
                    pnlCriteria.Visible = false;
                    pnlPatientList.Visible = true;
                    lblTotaldue.Visible = true;
                    tsb_btnBatch.Visible = true;
                    tsb_btnIndividual.Visible = false;
                    pnlc1PatientListHeader.Visible = false;
                    pnlFilteredPatList.Visible = false;
                    panel2.Height = 120;
                    tsb_GenerateBatch.Visible = false;
                    tsb_btnShowList.Visible = false;
                    break;
                #endregion "Individual"

                #region "Batch"
                case "Batch":
                    if (_isGenerateBatch && c1PatientList.Rows.Count > 1)
                    {
                        tsb_Send.Visible = true;
                        tsb_ViewStatement.Visible = true;
                    }
                    else
                    {
                        tsb_ViewStatement.Visible = false;
                        tsb_btnShowList.Visible = false;
                        tsb_Send.Visible = false;
                    }
                    panel5.Visible = true;
                    pnlCriteria.Visible = true;
                    lblTotaldue.Visible = false;
                    label51.Visible = false;
                    label50.Visible = false;
                    lblCount.Visible = false;
                    label49.Visible = false;
                    tsb_GenerateBatch.Visible = true;
                    pnlPatientList.Visible = false;
                    tsb_btnBatch.Visible = false;
                    tsb_btnIndividual.Visible = true;
                    pnlCriteria.Enabled = true;
                    pnlc1PatientListHeader.Visible = true;
                    pnlFilteredPatList.Visible = true;
                    panel2.Height = 120;
                    break;
                #endregion "Batch"

                #region "Generate"
                case "Generate":
                    tsb_GenerateBatch.Visible = false;
                    if (_isGenerateBatch && c1PatientList.Rows.Count > 1 && !_isIndvidual)
                    {
                        tsb_btnShowList.Visible = true;
                    }
                    else
                    {
                        tsb_btnShowList.Visible = false;
                    }

                    break;
                #endregion "Generate"

                #region "FormLoad"
                case "FormLoad":
                    tsb_ViewStatement.Visible = false;
                    tsb_btnShowList.Visible = false;
                    btnUp.Visible = true;
                    pnlCriteria.Visible = true;
                    pnlPatientList.Visible = false;
                    tsb_btnBatch.Visible = false;
                    tsb_btnIndividual.Visible = true;
                    pnlCriteria.Enabled = true;
                    pnlc1PatientListHeader.Visible = true;
                    pnlFilteredPatList.Visible = true;
                    panel2.Height = 120;
                    panel6.Visible = false;
                    lbldtStatementDate.Visible = false;
                    label46.Visible = false;
                    lblSettings.Visible = false;
                    label47.Visible = false;
                    lblTotaldue.Visible = false;
                    label51.Visible = false;
                    label50.Visible = false;
                    lblCount.Visible = false;
                    label49.Visible = false;
                    tsb_Send.Visible = false;
                    break;
                #endregion "FormLoad"

                #region "SendBatch"
                case "SendBatch":
                    c1PatientList.DataSource = null;
                    c1PatientList.Rows.Count = 1;
                    c1PatientList.Rows.Fixed = 1;
                    c1PatientList.Cols.Count = 1;
                    if (!_isIndvidual)
                    {
                        tsb_Send.Visible = false;
                        tsb_ViewStatement.Visible = false;
                        tsb_GenerateBatch.Visible = true;
                        tsb_btnShowList.Visible = false;
                        lblCount.Text = "0";
                        lblTotaldue.Text = "0";
                    }
                    break;
                #endregion "SendBatch"

                #region "GenerateBatch"
                case "GenerateBatch":

                    panel6.Visible = true;
                    lbldtStatementDate.Visible = true;
                    label46.Visible = true;
                    lblSettings.Visible = true;
                    label47.Visible = true;
                    pnlFilteredPatList.Visible = true;

                    lblTotaldue.Visible = true;
                    label51.Visible = true;
                    label50.Visible = true;
                    lblCount.Visible = true;
                    label49.Visible = true;

                    btnDown.Visible = false;
                    btnUp.Visible = true;
                    _generateBatchFlag = true;
                    tsb_ViewStatement.Enabled = true;
                    tsb_Send.Visible = true;
                    break;
                #endregion "GenerateBatch"
            }

        }

        public string GetServerPath()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Object retVal = new object();
            string _serverPath = "";
            string _sqlQuery = "";
            string _isValidPath = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT sSettingsValue FROM Settings WHERE UPPER(sSettingsName) = 'SERVERPATH'";
                retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                if (retVal != null && retVal != DBNull.Value)
                {
                    _isValidPath = Convert.ToString(retVal);
                    try
                    {
                        if (System.IO.Directory.Exists(_isValidPath) == false)
                        { _isValidPath = ""; }
                    }
                    catch (Exception ex)
                    { _isValidPath = ""; }
                }
                else
                { _isValidPath = ""; }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (retVal != null) { retVal = null; }
            }
            return _isValidPath;
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


        #endregion "Private Methods"

        #region "User Control Events"

        private void btnBrowsePatient_Click(object sender, EventArgs e)
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
                }


                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, this.Width);


                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Patient";
                if (_IsExcluded)
                    oListControl.ExcludeFromStatement = false;
                else
                    oListControl.ExcludeFromStatement = true;

                _CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbPatients.DataSource != null)
                {
                    for (int i = 0; i < cmbPatients.Items.Count; i++)
                    {
                        cmbPatients.SelectedIndex = i;
                        cmbPatients.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbPatients.SelectedValue), cmbPatients.Text);
                    }
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnClearPatient_Click(object sender, EventArgs e)
        {
            cmbPatients.DataSource = null;
            cmbPatients.Items.Clear();
            cmbPatients.Refresh();
            FillIndividualBatchSummary();
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;

            switch (_CurrentControlType)
            {

                case gloListControl.gloListControlType.Patient:
                    {

                        cmbPatients.DataSource = null;
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);

                            }


                            cmbPatients.DataSource = oBindTable;
                            cmbPatients.DisplayMember = "DispName";
                            cmbPatients.ValueMember = "ID";

                            FillIndividualBatchSummary();
                        }

                    }
                    break;

               

                default:
                    {
                    }
                    break;
            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
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
            }
        }

        private void cmbSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            _generateBatchFlag = false;
            panel5.Visible = false;

            panel6.Visible = false;
            lbldtStatementDate.Visible = false;
            label46.Visible = false;
            lblSettings.Visible = false;
            label47.Visible = false;

            lblTotaldue.Visible = false;
            label51.Visible = false;
            label50.Visible = false;
            lblCount.Visible = false;
            label49.Visible = false;

            DataTable dt = new DataTable();
            try
            {
                if (cmbSettings.SelectedValue != null)
                {
                    if (cmbSettings.SelectedValue != null)
                    {
                        if (cmbSettings.SelectedValue.ToString() != "0")
                        {
                            lblSettings.Text = cmbSettings.Text.ToString();
                            FillBatchDetails();
                            FillControlsPerCriteria(Convert.ToInt64(cmbSettings.SelectedValue));
                        }
                    }

                    //GetPatientWithBalance();
                    gloC1FlexStyle.Style(c1PatientList, true);
                }
                //Code Added by Mayuri:20091210-To display Selected value in combo into label
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null; 
            }

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            pnlFilteredPatList.Visible = false;
            btnDown.Visible = true;
            btnDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
            btnUp.Visible = false;
            tsb_btnShowList.Enabled = true;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            pnlFilteredPatList.Visible = true;
            btnDown.Visible = false;
            btnUp.Visible = true;
            tsb_btnShowList.Enabled = false;

        }

        #endregion

        #region "Filter Criteria"

        private void FetchCriteriasCombo()
        {
            gloBilling.PatinetStatementCriteria oPatinetStatementCriteria = new gloBilling.PatinetStatementCriteria(_databaseconnectionstring);
            #region "Fill Filter Combo box"
            try
            {
                DataTable _dtFilterCriterias = new DataTable();
                 _dtFilterCriterias = oPatinetStatementCriteria.GetPatinetStatementFilter();
                this.cmbSettings.SelectedIndexChanged -= new System.EventHandler(this.cmbSettings_SelectedIndexChanged);
                if (_dtFilterCriterias != null)
                {
                    cmbSettings.DataSource = _dtFilterCriterias;
                    cmbSettings.DisplayMember = "sStatementCriteriaName";
                    cmbSettings.ValueMember = "nStatementCriteriaID";

                    DataRow[] drDefault = _dtFilterCriterias.Select("isDefault = 'true'");

                    if (drDefault.Length > 0)
                    {
                        cmbSettings.SelectedValue = Convert.ToInt64(drDefault[0]["nStatementCriteriaID"]);  
                    }

                    FillControlsPerCriteria(Convert.ToInt64(cmbSettings.SelectedValue));
                }
                this.cmbSettings.SelectedIndexChanged += new System.EventHandler(this.cmbSettings_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);     

            }
            #endregion
            lblSettings.Text = cmbSettings.Text.ToString();
        }

        private void FillControlsPerCriteria(Int64 CriteriaID)
        {
            DataRow dr;
            gloBilling.PatinetStatementCriteria oPatinetStatementCriteria = new gloBilling.PatinetStatementCriteria(_databaseconnectionstring);
            try
            {
                if (oPatinetStatementCriteria.GetPatinetStatementCriteria(CriteriaID))
                {
                    if (oPatinetStatementCriteria.PatStatementCriteriaFilter != null)
                    {
                        for (int i = 0; i < oPatinetStatementCriteria.PatStatementCriteriaFilter.Rows.Count; i++)
                        {
                            dr = oPatinetStatementCriteria.PatStatementCriteriaFilter.Rows[i];

                            switch (Convert.ToString(dr[0]))
                            {
                                case "Balance":
                                    if (Convert.ToInt32(dr[1]) == 0)
                                    {
                                        lblDueAmt.Text = Convert.ToString(dr[2]);
                                    }
                                    break;
                                case "From":
                                    if (Convert.ToInt32(dr[1]) == 0)
                                    {
                                        lblNameFrom.Text = Convert.ToString(dr[2]);
                                    }
                                    break;
                                case "To":
                                    if (Convert.ToInt32(dr[1]) == 0)
                                    {
                                        lblNameTo.Text = Convert.ToString(dr[2]);
                                    }
                                    break;
                                case "Wait Days":
                                    if (Convert.ToInt32(dr[1]) == 0)
                                    {
                                        lblWaitDays.Text = Convert.ToString(dr[2]);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region "Word Template"

        private void wdTemplate_OnDocumentClosed(object sender, EventArgs e)
        {
            try
            {
                if ((oCurDoc != null))
                {
                    //' RemoveHandler oCurDoc1.ContentControlOnExit, AddressOf onCtrlExit 
                    Marshal.ReleaseComObject(oCurDoc);
                    oCurDoc = null;
                }
                if ((oTempDoc != null))
                {
                    Marshal.ReleaseComObject(oTempDoc);
                    oTempDoc = null;
                }
                if ((oWordApp != null))
                {
                    Marshal.FinalReleaseComObject(oWordApp);
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {

            }


        }

        private bool SavePatientTemplate(string sFileName, string sFilePath, Int64 nPatientID, Int64 nBatchPateintStatMstID)
        {
            try
            {
                gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);
                ogloTemplate.ClinicID = _ClinicID;

                ogloTemplate.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Today.ToShortDateString());
                ogloTemplate.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Today.ToShortDateString());

                ogloTemplate.TemplateID = 0;
                ogloTemplate.PatientID = nPatientID;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dtTemplateID = new DataTable();
                oDB.Connect(false);
                string strSQL = "";
                strSQL = "SELECT nCategoryID FROM Category_MST WHERE sDescription = 'MIS Reports' AND sCategoryType='Template'";
                oDB.Retrive_Query(strSQL, out dtTemplateID);
                oDB.Disconnect();
                oDB.Dispose();

                ogloTemplate.CategoryID = Convert.ToInt64(dtTemplateID.Rows[0]["nCategoryID"]);
                ogloTemplate.TemplateName = sFileName.Replace(".doc", "");
                ogloTemplate.CategoryName = "MIS Reports";


                ogloTemplate.TemplateFilePath = sFilePath;

                if (ogloTemplate.SavePatientTemplate(0) > 0)
                {

                    oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                    oDB.Connect(false);

                    string sBatchName = cmbSettings.Text.ToString() + " " + gloDateMaster.gloDate.DateAsNumber(DateTime.Today.ToShortDateString());


                    oDBParameters.Add("@nBatchPateintStatDtlID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@nBatchPateintStatMstID", nBatchPateintStatMstID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sUserName", _UserName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@dtStatementDate", gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);


                    oDBParameters.Add("@nCrWaitDays", 30, ParameterDirection.Input, SqlDbType.Int);

                    oDBParameters.Add("@bStatus", true, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@nTempleteTransactionID", Convert.ToInt64(ogloTemplate.SavePatientTemplate(0).ToString()), ParameterDirection.Input, SqlDbType.BigInt);


                    oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                    Object oResult;
                    oDB.Execute("sp_INSERT_BL_Batch_PatientStatement_Dtl", oDBParameters, out oResult);
                    oDB.Disconnect();
                    oDB.Dispose();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
        }

        #endregion

        #region " Date and Other Validations Methods "
        private string getCloseDate()
        {
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                object _Result = oDB.ExecuteScalar_Query("SELECT dbo.Convert_to_date(max(nCloseDayDate)) As CloseDate from BL_CloseDays");
                if (_Result.ToString() != "")
                {
                    return _Result.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }
        private bool ValidateData()
        {
            Boolean _result = false;
            string CloseDate = getCloseDate();
            if (CloseDate == "")
            {
                MessageBox.Show(Convert.ToDateTime(dtpEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (CloseDate != "")
            {
                if (_isIndvidual)
                {
                    if (Convert.ToDateTime(dtpEndDate.Value.ToShortDateString()) > Convert.ToDateTime(CloseDate))
                    {
                        MessageBox.Show(Convert.ToDateTime(dtpEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tsb_GenerateBatch.Visible = false;
                        dtpEndDate.Text = CloseDate;
                        tsb_btnIndividual_Click(null, null);
                    }
                    else
                    {
                        _result = true;
                    }
                }
                else
                {
                    if (Convert.ToDateTime(dtCriteriaEndDate.Value.ToShortDateString()) > Convert.ToDateTime(CloseDate))
                    {
                        MessageBox.Show(Convert.ToDateTime(dtCriteriaEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tsb_GenerateBatch.Visible = true;
                        dtCriteriaEndDate.Text = CloseDate;
                        tsb_GenerateBatch_Click(null, null);
                    }
                    else
                    {
                        _result = true;
                    }
                }
            }
            return _result;
        }
        #endregion " Date and Other Validations Methods "

        #region "Change Events"

        private void btnDown_MouseHover(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloReports.Properties.Resources.DownHover;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown_MouseLeave(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnUp_MouseHover(object sender, EventArgs e)
        {
            btnUp.BackgroundImage = global::gloReports.Properties.Resources.UPHover;
            btnUp.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnUp_MouseLeave(object sender, EventArgs e)
        {
            btnUp.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnUp.BackgroundImageLayout = ImageLayout.Center;
        }

        #endregion

        #region "Tool Tip Methods"

        private void cmbSettings_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                combo = (ComboBox)sender;
                if (cmbSettings.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbSettings.Items[cmbSettings.SelectedIndex])["sStatementCriteriaName"]), cmbSettings) >= cmbSettings.DropDownWidth)
                    {
                        tooltip_Rpt.Show(Convert.ToString(((DataRowView)cmbSettings.Items[cmbSettings.SelectedIndex])["sStatementCriteriaName"]), cmbSettings, cmbSettings.Right - 200, cmbSettings.Bottom - 50);
                    }
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null; 
            }
           
        }

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

            Graphics g = this.CreateGraphics();
            SizeF s = g.MeasureString(_text, cmbSettings.Font);
            int width = Convert.ToInt32(s.Width);
            return width;
        }

        #endregion "Tool Tip Methods"

        #region "C1 FlexGrid Events"
        private void c1PatientList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_isGenerateBatch && c1PatientList.Rows.Count>1)
            {
                tsb_btnShowList.Visible = true;
            }
            else
            {
                tsb_btnShowList.Visible = false;
            }
            C1.Win.C1FlexGrid.HitTestInfo ht  = c1PatientList.HitTest(e.Location); 
            try
            {
                if (ht.Row > 0)
                {
                    tsb_GenerateBatch.Visible = false;
                    string CloseDate = getCloseDate();
                    if (CloseDate == "")
                    {
                        MessageBox.Show(Convert.ToDateTime(dtCriteriaEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtCriteriaEndDate.Text = CloseDate;
                        tsb_GenerateBatch_Click(null, null);
                        tsb_GenerateBatch.Visible = true;
                    }
                    if (CloseDate != "")
                    {
                        if (Convert.ToDateTime(dtCriteriaEndDate.Value.ToShortDateString()) > Convert.ToDateTime(CloseDate))
                        {
                            MessageBox.Show(Convert.ToDateTime(dtCriteriaEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtCriteriaEndDate.Text = CloseDate;
                            tsb_GenerateBatch_Click(null, null);
                            tsb_GenerateBatch.Visible = true;
                        }

                        else
                        {
                            Int64 nPatientID = 0;
                            btnUp_Click(null, null);
                            if (c1PatientList.DataSource != null)
                            {
                                if (c1PatientList.Rows.Count > 1)
                                {
                                    if (c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index) != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString() != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString().Trim() != "")
                                    {
                                        nPatientID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString());
                                    }
                                    if (nPatientID > 0)
                                    {
                                        fillRevisedPatientStatement(nPatientID);
                                    }
                                }
                            }
                        }
                        //dtCriteriaEndDate.Text = CloseDate;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }
        #endregion "C1 FlexGrid Events"

    }
}
