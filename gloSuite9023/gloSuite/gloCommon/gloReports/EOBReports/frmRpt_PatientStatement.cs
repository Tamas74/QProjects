 

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
//using gloReports.EOBReports;

namespace gloReports
{
    public partial class frmRpt_PatientStatement : Form
    {

        #region " Declarations "


        //For Creating the Object of the Report
        // Rpt_PatientStatement objrptPatientStatementForGateWayEDI;
        Rpt_Paper_PatientStatement objrptPatientStatementForGateWayEDI = new Rpt_Paper_PatientStatement();
        //dsEOBPaymentReports _dsReports = null;
        dsRevisedPatientStatement _dsPatientStatement = null;
        private string _databaseconnectionstring = "";
        DataView _dv = new DataView();
        private Int64 _nPatientID;
        private Int64 _UserID = 0;
        private string _UserName = "";
        public bool _chkTabFlag = false;
        public bool _isGenerateBatch = false;
        public bool _generateBatchFlag = false;
        public bool _IsIndividualTrue = false;
        private ComboBox combo;
        ToolTip tooltip_Rpt = new ToolTip();

        private StringBuilder sbCPTCode = new StringBuilder();
        private StringBuilder sbPatientID = new StringBuilder();

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
        string _sStatementNotes = string.Empty;
        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        private gloGeneralItem.gloItems ogloItems = null;
        bool _printFlag = false;
        bool _ediFlag = false;
        //private dsReports dsReports = new dsReports();

        #endregion " Declarations "

        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "

        #region "Constructors"

        public frmRpt_PatientStatement(string databaseconnectionstring, Int64 nPatientID)
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

        private void frmRpt_PatientStatement_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dtIndividual = new DataTable();
            DataTable dtBatch = new DataTable();
            string CloseDate = getCloseDate();
            try
            {
                dsEOBPaymentReports dsReports = new dsEOBPaymentReports();
                SetButtonVisibility("FormLoad");
                btnUp.BackgroundImage = global::gloReports.Properties.Resources.UP;
                btnUp.BackgroundImageLayout = ImageLayout.Center;
                
                #region "Commented"
                if (CloseDate != "")
                {
                    dtpEndDate.Value = Convert.ToDateTime(CloseDate);
                    dtCriteriaEndDate.Value = Convert.ToDateTime(CloseDate);
                }

                #endregion

                FetchCriteriasCombo();
                lblSettings.Text = cmbSettings.Text.ToString();

            }
            catch (Exception ex)
            {

            }
            gloC1FlexStyle.Style(c1PatientList, true);
        }
        #region "Toolstrip Events"

        //Generate Report
        private void tsb_GenerateReport_Click(object sender, EventArgs e)
        {
            SetButtonVisibility("Generate");
            try
            {
                string CloseDate = getCloseDate();
                if (CloseDate == "")
                {
                    MessageBox.Show(Convert.ToDateTime(dtpEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (CloseDate != "")
                {
                    if (_chkTabFlag)
                    {
                        if (Convert.ToDateTime(dtpEndDate.Value.ToShortDateString()) > Convert.ToDateTime(CloseDate))
                        {
                            MessageBox.Show(Convert.ToDateTime(dtpEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tsb_GenerateBatch.Visible = false;
                            dtpEndDate.Text = CloseDate;
                            tsb_btnHideCriteria_Click(null, null);
                        }
                        else
                        {
                            _IsIndividualTrue = true;
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
                            _IsIndividualTrue = true;

                        }
                    }
                }
                if (_IsIndividualTrue)
                {
                    if (Convert.ToDateTime(dtpEndDate.Value.ToShortDateString()) <= Convert.ToDateTime(CloseDate))
                    {
                        Int64 _nPatientID = 0;
                        if (rbCriteria.Checked)
                        {
                            if (c1PatientList.DataSource != null)
                            {
                                if (c1PatientList.Rows.Count > 1)
                                {
                                    if (c1PatientList.Cols["PatientID"] != null)
                                    {
                                        if (c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index) != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString() != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString().Trim() != "")
                                        {
                                            _nPatientID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString());
                                        }
                                    }
                                }
                            }
                            GetIndividualPatientBlance(_nPatientID);
                            btnUp_Click(null, null);
                        }
                        else
                        {
                            if (cmbPatients.SelectedValue != null)
                            {
                                if (cmbPatients.SelectedValue.ToString() != "0")
                                {
                                    _nPatientID = Convert.ToInt64(cmbPatients.SelectedValue);
                                }
                            }
                        }
                        GetIndividualPatientBlance(_nPatientID);
                        fillRevisedPatientStatement(_nPatientID);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        //Export Report
        private void tsb_btnExportReport_Click(object sender, EventArgs e)
        {
            //crvReportViewer.ExportReport();
        }

        //Export To Batch Text (EDI Statement)
        private void tsb_btnGenerateBatchTxt_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 _nPatientID = 0;
                ArrayList oListPatientIds = FetchPatientId();
                string _FilePath = "";

                prgFileGeneration.Value = 0;
                prgFileGeneration.Minimum = 0;
                prgFileGeneration.Maximum = oListPatientIds.Count;
                if (oListPatientIds.Count > 0)
                {
                    pnlProgressBar.Visible = true;
                    prgFileGeneration.Visible = true;
                    this.Cursor = Cursors.WaitCursor;

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text File(.txt)|*.txt";
                    saveFileDialog.DefaultExt = ".txt";
                    saveFileDialog.AddExtension = true;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        _FilePath = saveFileDialog.FileName;
                    }

                    if (_FilePath == "")
                    {
                        return;
                    }

                    Application.DoEvents();
                    bool isAppendText = false;
                    for (int i = 0; i < oListPatientIds.Count; i++)
                    {
                        _nPatientID = Convert.ToInt64(oListPatientIds[i]);
                        Application.DoEvents();

                        if (_FilePath != "")
                        {
                            //ExportPatientStatementToText(_nPatientID, _FilePath, isAppendText);
                            isAppendText = true;
                        }
                        prgFileGeneration.Value = i + 1;
                        lblFile.Text = "Exporting Files " + prgFileGeneration.Value + "/" + oListPatientIds.Count;
                        Application.DoEvents();
                    }
                    //MessageBox.Show("Export Completed. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (MessageBox.Show("Export Completed. Do you want to save patient statment template  ? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        tsb_btnGenerateBatch_Click(null, null);
                    }

                    pnlProgressBar.Visible = false;
                    prgFileGeneration.Visible = false;
                    this.Parent.Cursor = Cursors.Default;
                    this.Cursor = Cursors.Default;
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
                this.Cursor = Cursors.Default;
            }
        }

        //Batch Word Template
        private void tsb_btnGenerateBatch_Click(object sender, EventArgs e)
        {
        }

        //Close
        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private Int64 genrateBatch(bool _blnPrintReport)
        {

            Int64 BatchID = 0;
            pnlPleasewait.Visible = true;
            Boolean _bPrinted=false;
            string _sPrinterName="";
            PrintDialog _PrintDialog=null;
            try
            {
                
                    Int64 nPatientID = 0;
                    int _PageCount = 0;
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
                    Application.DoEvents();

                    BatchID = CreateBatch_Mst();
                    if (BatchID != null)
                    {

                        for (int i = 0; i < oListPatientIds.Count; i++)
                        {
                            string _FileName = "PatientStatement_" + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + "_To_" + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + "_" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + System.DateTime.Now.Millisecond + ".doc";
                            // If not exist create directory
                            //if (Directory.Exists(Application.StartupPath + "\\MIStemp") == false)
                            //{
                            //    Directory.CreateDirectory(Application.StartupPath + "\\MIStemp");
                            //}

                            //_FilePath = Application.StartupPath + "\\MIStemp\\" + _FileName;
                            if (Directory.Exists(appSettings["StartupPath"].ToString() + "\\MIStemp") == false)
                            {
                                Directory.CreateDirectory(appSettings["StartupPath"].ToString() + "\\MIStemp");
                            }

                            _FilePath = appSettings["StartupPath"].ToString() + "\\MIStemp\\" + _FileName;


                            nPatientID = Convert.ToInt64(oListPatientIds[i]);

                            //To fill the Reports 
                            //FillPatientStatement(nPatientID);
                            fillRevisedPatientStatement(nPatientID);

                            //if (_dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows.Count > 0)
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
                                    //crvReportViewer.PrintReport();
                                    //objrptPatientStatementForGateWayEDI

                                    if (!_bPrinted)
                                    {
                                        _PrintDialog.AllowSomePages = true;
                                        crvReportViewer.ShowLastPage();
                                        _PageCount = crvReportViewer.GetCurrentPageNumber();

                                        _PrintDialog.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.SomePages;
                                        _PrintDialog.PrinterSettings.FromPage = 1;
                                        _PrintDialog.PrinterSettings.ToPage = _PageCount;
                                        if(_PrintDialog.PrinterSettings.ToPage > _PageCount)
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
                        //MessageBox.Show("Generation of Batch Done. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        try
                        {
                            //System.IO.Directory.Delete(Application.StartupPath + "\\MIStemp", true);
                            System.IO.Directory.Delete(appSettings["StartupPath"].ToString() + "\\MIStemp", true);
                        }
                        catch (Exception EX)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), false);
                            EX = null;   
                        }
                        
                        FillBatchDetails();
                        GetIndividualDetails();
                        pnlProgressBar.Visible = false;
                        prgFileGeneration.Visible = false;
                        this.Parent.Cursor = Cursors.Default;
                        this.Cursor = Cursors.Default;
                        //GetPatientWithBalance();
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
            }

            return BatchID;
        }


        private Int64 genratePrintBatch(bool _blnPrintReport)
        {

            Int64 BatchID = 0;
            pnlPleasewait.Visible = true;
            Boolean _bPrinted = false;
            string _sPrinterName = "";
            PrintDialog _PrintDialog = null;
            int _PageCount = 0;
            try
            {
                _PrintDialog = new PrintDialog();
                if (_chkTabFlag)
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
                    if (_chkTabFlag)
                    {
                        if (_PrintDialog.PrinterSettings.ToPage > _PageCount)
                        {
                            MessageBox.Show("The page range is invalid. Enter number between " + _PrintDialog.PrinterSettings.FromPage + " and " + _PageCount + "", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return 0;
                        }
                    }
                    Int64 nPatientID = 0;
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
                    Application.DoEvents();

                    BatchID = CreateBatch_Mst();
                    if (BatchID != null)
                    {

                        for (int i = 0; i < oListPatientIds.Count; i++)
                        {
                            string _FileName = "PatientStatement_" + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + "_To_" + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + "_" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + System.DateTime.Now.Millisecond + ".doc";
                            // If not exist create directory
                            //if (Directory.Exists(Application.StartupPath + "\\MIStemp") == false)
                            //{
                            //    Directory.CreateDirectory(Application.StartupPath + "\\MIStemp");
                            //}

                            //_FilePath = Application.StartupPath + "\\MIStemp\\" + _FileName;
                            if (Directory.Exists(appSettings["StartupPath"].ToString() + "\\MIStemp") == false)
                            {
                                Directory.CreateDirectory(appSettings["StartupPath"].ToString() + "\\MIStemp");
                            }

                            _FilePath = appSettings["StartupPath"].ToString() + "\\MIStemp\\" + _FileName;

                            nPatientID = Convert.ToInt64(oListPatientIds[i]);

                            //To fill the Reports 
                            //FillPatientStatement(nPatientID);
                            fillRevisedPatientStatement(nPatientID);

                            //if (_dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows.Count > 0)
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
                                    //crvReportViewer.PrintReport();
                                    //objrptPatientStatementForGateWayEDI

                                    if (!_bPrinted)
                                    {

                                        if (_chkTabFlag)
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
                                        if (_chkTabFlag)
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

                                }
                            }

                            Application.DoEvents();
                            prgFileGeneration.Value = i + 1;
                            lblFile.Text = "Processing Batch " + prgFileGeneration.Value + "/" + oListPatientIds.Count;

                            Application.DoEvents();

                        }
                        //MessageBox.Show("Generation of Batch Done. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        FillBatchDetails();
                        GetIndividualDetails();
                        pnlProgressBar.Visible = false;
                        prgFileGeneration.Visible = false;
                        this.Parent.Cursor = Cursors.Default;
                        this.Cursor = Cursors.Default;
                        try
                        {
                            System.IO.Directory.Delete(appSettings["StartupPath"].ToString() + "\\MIStemp", true);
                        }
                        catch (Exception EX)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), false);
                            EX = null;     
                        }
                        
                        //GetPatientWithBalance();
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
            }

            return BatchID;
        }

        //Print
        private void tsb_Print_Click(object sender, EventArgs e)
        {
//            _printFlag = true;
            try
            {
                //genrateBatch(true);
                genratePrintBatch(true);
                SetButtonVisibility("SendBatch");
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
        }

        //Hide Criteria
        private void tsb_btnHideCriteria_Click(object sender, EventArgs e)
        {
            _chkTabFlag = true;
            SetButtonVisibility("Individual");
            string CloseDate = getCloseDate();
            dtCriteriaEndDate.Text = CloseDate;
            
            DataTable dtIndividual = new DataTable();
            try
            {
                if (_nPatientID != 0)
                {
                    string sPatientName = GetPatientName(_nPatientID);
                    
                    DataTable dtPat = new DataTable();
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
                    GetIndividualPatientBlance(_nPatientID);
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

        //Show Criteria
        private void tsb_btnCriteria_Click(object sender, EventArgs e)
        {
            _chkTabFlag = false;
            SetButtonVisibility("Batch");
            string CloseDate = getCloseDate();
            dtpEndDate.Text = CloseDate;
    
         }

       
        private void tsb_Send_ResendElectronic_Click(object sender, EventArgs e)
        {
            bool _IsIndividualTrue = false;
            _ediFlag = true;
            try
            {
                string CloseDate = getCloseDate();
                if (_chkTabFlag && cmbPatients.SelectedValue == null)
                {
                    MessageBox.Show("Please select the patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (CloseDate == "")
                {
                    MessageBox.Show(Convert.ToDateTime(dtpEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (CloseDate != "")
                {
                    if (_chkTabFlag)
                    {
                        if (Convert.ToDateTime(dtpEndDate.Value.ToShortDateString()) > Convert.ToDateTime(CloseDate))
                        {
                            MessageBox.Show(Convert.ToDateTime(dtpEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpEndDate.Text = CloseDate;
                        }
                        else
                        {
                            _IsIndividualTrue = true;
                        }
                    }
                    else
                    {
                        if (Convert.ToDateTime(dtCriteriaEndDate.Value.ToShortDateString()) > Convert.ToDateTime(CloseDate))
                        {
                            MessageBox.Show(Convert.ToDateTime(dtCriteriaEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtCriteriaEndDate.Text = CloseDate;
                        }
                        else
                        {
                            _IsIndividualTrue = true;
                        }
                    }
                }
                if (_IsIndividualTrue)
                {
                    Int64 BatchID = genrateBatch(false);
                    string _BatchName = string.Empty;
                    try
                    {
                        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

                        string _sqlQuery = "select sBatchName from dbo.BL_Batch_PatientStatement_Mst where dbo.BL_Batch_PatientStatement_Mst.nBatchPateintStatMstID = " + BatchID;

                        oDB.Connect(false);

                        _BatchName = oDB.ExecuteScalar_Query(_sqlQuery).ToString();
                        oDB.Disconnect();
                    }
                    catch (Exception Ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                        Ex = null; 
                    }

                    #region Save File
                    string _FilePath = string.Empty;
                    //string _ServerPath = Application.StartupPath;
                    string _ServerPath = appSettings["StartupPath"].ToString();
                    string _BaseFolder = "Temp";
                    string _OutInFolder = "OutBox";
                    string _ClaimFolder = "Statements";
                    string _claimFolderPath = "";

                    _claimFolderPath = _ServerPath + "\\" + _BaseFolder;

                    if (System.IO.Directory.Exists(_claimFolderPath) == false)
                    {
                        System.IO.Directory.CreateDirectory(_claimFolderPath);
                    }

                    _FilePath = _claimFolderPath + "\\" + _BatchName + ".txt";

                    #endregion
                    ClsgloElectronic objClsgloElectronic = new ClsgloElectronic(_databaseconnectionstring);

                    Int64 nPatientID = 0;

                    ArrayList oListPatientIds = FetchPatientId();

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
                    try
                    {
                        oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        _sqlQuery = " SELECT * FROM RPT_Patstatementcriteria_MST WHERE bitIsDefault = 1 AND criteriaType = 'DISPLAY' ";
                        oDB.Connect(false);
                        dtTemp = new DataTable();
                        oDB.Retrive_Query(_sqlQuery, out dtTemp);
                        oDB.Disconnect();
                    }
                    catch (Exception Ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                    }
                    nStatementCriteriaID = Convert.ToInt64(dtTemp.Rows[0]["nStatementCriteriaID"].ToString());
                    objClsgloElectronic.GenerateElectonicClaimFile(oListPatientIds, dtpEndDate.Value.ToShortDateString(), nStatementCriteriaID, _FilePath);

                    try
                    {
                        oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                        oDB.Connect(false);

                        oDBParameters.Add("@BatchID", BatchID, ParameterDirection.Input, SqlDbType.BigInt);
                        gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);
                        Byte[] oTemplate = ogloTemplate.ConvertFileToBinary(_FilePath);
                        oDBParameters.Add("@iBatchStatementFile", oTemplate, ParameterDirection.Input, SqlDbType.Image);

                        oDB.Execute("sp_IniBatchStatementFile_MST", oDBParameters);
                        oDB.Disconnect();
                        oDB.Dispose();
                        //string _sqlQuery = "update BL_Batch_PatientStatement_Mst Set iBatchStatementFile = " + oTemplate.ToString()  + " where dbo.BL_Batch_PatientStatement_Mst.nBatchPateintStatMstID = " + BatchID;
                        try
                        {
                        System.IO.File.Delete(_FilePath);
                        //System.IO.Directory.Delete(_claimFolderPath);
                        }
                        catch (Exception EX)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), false);
                            EX = null;     
                        }

                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    }
                    
                    pnlProgressBar.Visible = false;
                    prgFileGeneration.Visible = false;
                    this.Parent.Cursor = Cursors.Default;
                    this.Cursor = Cursors.Default;
                    //GetPatientWithBalance();
                    MessageBox.Show("Generation of Batch Done. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetButtonVisibility("SendBatch");
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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


        private void tsb_Send_Reprint_Click(object sender, EventArgs e)
        {
            bool _IsIndividualTrue = false;
            try
            {
                string CloseDate = getCloseDate();
                if (_chkTabFlag && cmbPatients.SelectedValue == null)
                {
                    MessageBox.Show("Please select the patient.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (CloseDate == "")
                {
                    MessageBox.Show(Convert.ToDateTime(dtpEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (CloseDate != "")
                {
                    if (_chkTabFlag)
                    {
                        if (Convert.ToDateTime(dtpEndDate.Value.ToShortDateString()) > Convert.ToDateTime(CloseDate))
                        {
                            MessageBox.Show(Convert.ToDateTime(dtpEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpEndDate.Text = CloseDate;
                        }
                        else
                        {
                            _IsIndividualTrue = true;
                        }
                    }
                    else
                    {
                        if (Convert.ToDateTime(dtCriteriaEndDate.Value.ToShortDateString()) > Convert.ToDateTime(CloseDate))
                        {
                            MessageBox.Show(Convert.ToDateTime(dtCriteriaEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtCriteriaEndDate.Text = CloseDate;
                        }
                        else
                        {
                            _IsIndividualTrue = true;
                        }
                    }
                }
                if (_IsIndividualTrue)
                {
                    tsb_Print_Click(null, null);
                }
                //SetButtonVisibility("SendBatch");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        #endregion

        #region "Private Methods"
        #region "Commented Code _Prev Patient Stmt"
        //private void FillPatientStatement(Int64 nPatientID)
        //{

        //    if (objrptPatientStatementForGateWayEDI != null)
        //    {
        //        if (objrptPatientStatementForGateWayEDI.IsLoaded)
        //        {
        //            objrptPatientStatementForGateWayEDI.Close();
        //        }
        //        objrptPatientStatementForGateWayEDI.Dispose();
        //    }
        //    crvReportViewer.ReportSource = null;

        //    //objrptPatientStatementForGateWayEDI = new Rpt_PatientStatement();
        //    objrptPatientStatementForGateWayEDI = new Rpt_Paper_PatientStatement();
        //    SqlCommand _sqlcommand = new SqlCommand();
        //    SqlConnection oConnection = new SqlConnection();
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    _dsReports = new dsEOBPaymentReports();
        //    try
        //    {
        //        #region "Fetch Report Header Settings"

        //        if (cmbSettings.SelectedValue != null && cmbSettings.SelectedValue.ToString() != "")
        //        {
        //            FetchDisplaySettings(Convert.ToInt64(cmbSettings.SelectedValue), nPatientID);
        //        }
        //        else
        //        {
        //            FetchDisplaySettings(0, nPatientID);
        //        }

        //        #endregion

        //        #region "Fetch Statement Details "

        //        Int32 stDate = 0;
        //        Int32 endDate = 0;
        //        Int64 nClinicID = 1;


        //        if (rbCriteria.Checked)
        //        {
        //            dtCriteriaStartDate.Checked = true;
        //            dtCriteriaEndDate.Checked = true;
        //            stDate = gloDateMaster.gloDate.DateAsNumber(dtCriteriaStartDate.Value.ToShortDateString());
        //            endDate = gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString());

        //        }
        //        else
        //        {
        //            stDate = gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString());
        //            endDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString());
        //        }
        //        //FetchCPTCode();


        //        oConnection.ConnectionString = _databaseconnectionstring;
        //        _sqlcommand.CommandType = CommandType.StoredProcedure;
        //        _sqlcommand.CommandText = "Rpt_Patient_Statement";
        //        _sqlcommand.Connection = oConnection;

        //        _sqlcommand.Parameters.Add("@nPatientID", System.Data.SqlDbType.NVarChar);
        //        _sqlcommand.Parameters["@nPatientID"].Value = nPatientID.ToString();




        //        if (stDate != 0)
        //        {
        //            _sqlcommand.Parameters.Add("@nStartDate", System.Data.SqlDbType.Int);
        //            _sqlcommand.Parameters["@nStartDate"].Value = stDate;
        //        }

        //        if (endDate != 0)
        //        {
        //            _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.Int);
        //            _sqlcommand.Parameters["@nEndDate"].Value = endDate;
        //        }
        //        _sqlcommand.Parameters.Add("@nClinicID", System.Data.SqlDbType.NVarChar);
        //        _sqlcommand.Parameters["@nClinicID"].Value = nClinicID.ToString();


        //        #region "Show Hide The Charges And Allowed Colums"

        //        ////If Allowed Amount Pass the Parameter as 1
        //        //if (_ogloReportViewer.bAllowed)
        //        //{
        //        //    _sqlcommand.Parameters.Add("@Mode", System.Data.SqlDbType.Int);
        //        //    _sqlcommand.Parameters["@Mode"].Value = 1;
        //        //}

        //        #endregion

        //        da = new SqlDataAdapter(_sqlcommand);
        //        da.Fill(_dsReports, "dt_PatientStatement");
        //        da.Dispose();
        //        _sqlcommand.Dispose();


        //        _sqlcommand = new SqlCommand();
        //        da = new SqlDataAdapter(_sqlcommand);
        //        _sqlcommand.CommandType = CommandType.StoredProcedure;
        //        _sqlcommand.CommandText = "Sp_Get_Patient_Insurence_Pending";
        //        _sqlcommand.Connection = oConnection;

        //        _sqlcommand.Parameters.Add("@nPatientID", System.Data.SqlDbType.NVarChar);
        //        _sqlcommand.Parameters["@nPatientID"].Value = nPatientID.ToString();


        //        _sqlcommand.Parameters.Add("@nClinicID", System.Data.SqlDbType.NVarChar);
        //        _sqlcommand.Parameters["@nClinicID"].Value = nClinicID.ToString();
        //        da.Fill(_dsReports, "dt_PatientInsurencePaid");
        //        da.Dispose();
        //        _sqlcommand.Dispose();


        //        for (int i = 0; i < _dsReports.Tables["dt_PatientInsurencePaid"].Rows.Count; i++)
        //        {
        //            for (int j = 0; j < _dsReports.Tables["dt_PatientStatement"].Rows.Count; j++)
        //            {
        //                if (_dsReports.Tables["dt_PatientInsurencePaid"].Rows[i]["nTransactionID"].ToString() == _dsReports.Tables["dt_PatientStatement"].Rows[j]["nTransactionID"].ToString() && _dsReports.Tables["dt_PatientInsurencePaid"].Rows[i]["nTransactionDetailID"].ToString() == _dsReports.Tables["dt_PatientStatement"].Rows[j]["nTransactionDetailID"].ToString() && _dsReports.Tables["dt_PatientInsurencePaid"].Rows[i]["nTransactionLineNo"].ToString() == _dsReports.Tables["dt_PatientStatement"].Rows[j]["nTransactionLineNo"].ToString() && _dsReports.Tables["dt_PatientInsurencePaid"].Rows[i]["PatientID"].ToString() == _dsReports.Tables["dt_PatientStatement"].Rows[j]["PatientID"].ToString())
        //                {

        //                    _dsReports.Tables["dt_PatientStatement"].Rows[j]["InsurencePaid"] = _dsReports.Tables["dt_PatientInsurencePaid"].Rows[j]["InsurencePaid"];
        //                    _dsReports.Tables["dt_PatientStatement"].Rows[j]["OtherPaid"] = _dsReports.Tables["dt_PatientInsurencePaid"].Rows[j]["OtherPaid"];
        //                    _dsReports.Tables["dt_PatientStatement"].Rows[j]["dPatientResponsibility"] = _dsReports.Tables["dt_PatientInsurencePaid"].Rows[j]["dPatientResponsibility"];
        //                }
        //            }

        //        }




        //        #endregion

        //        #region "Fetch Report Footer settings"

        //        FetchGuarantorDetails(nPatientID);

        //        #endregion



        //        //For Assigning the Reports with a Datatable 
        //        objrptPatientStatementForGateWayEDI.SetDataSource(_dsReports);

        //        //Binds the Report to the Report viewer
        //        crvReportViewer.ReportSource = objrptPatientStatementForGateWayEDI;
        //    }
        //    catch (gloDatabaseLayer.DBException ex)
        //    {
        //        ex.ERROR_Log(ex.ToString());
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        _dsReports = null;
        //        //_dsReports.Dispose();

        //    }

        //}

        //private void FetchGuarantorDetails(Int64 nPatientID)
        //{
        //    SqlCommand _sqlcommand = new SqlCommand();
        //    SqlConnection oConnection = new SqlConnection();
        //    SqlDataAdapter da = null;
        //    try
        //    {


        //        oConnection.ConnectionString = _databaseconnectionstring;
        //        //_sqlcommand.CommandText = " select sFirstName as GuarantorFName,sMiddleName as GuarantorMName,sLastName as GuarantorLName,sAddressLine1 as GuarantorAdd1,sAddressLine2 as GuarantorAdd2,sCity as GuarantorCity,sState as GuarantorState,sZIP as GuarantorZip from Patient_OtherContacts where nPatientID=" + PatientID + " and nPatientContactTypeFlag=1";

        //        _sqlcommand.CommandText = " select sFirstName as GuarantorFName,sMiddleName as GuarantorMName,sLastName as GuarantorLName, " +
        //                                   " CASE sAddressLine1 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sAddressLine1 END AS GuarantorAdd1, " +
        //                                   " CASE sAddressLine2 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sAddressLine2 END AS GuarantorAdd2, " +
        //                                   " CASE sCity WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sCity END AS GuarantorCity, " +
        //                                   " CASE sState WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sState END AS GuarantorState, " +
        //                                   " CASE sZIP WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sZIP END AS GuarantorZip " +
        //                                   " from Patient_OtherContacts where nPatientID=" + nPatientID + " and nPatientContactTypeFlag =1 ";

        //        _sqlcommand.Connection = oConnection;

        //        da = new SqlDataAdapter(_sqlcommand);
        //        da.Fill(_dsReports, "dt_GuarantorDetails");
        //        if (_dsReports.Tables["dt_GuarantorDetails"].Rows.Count == 0)
        //        {
        //            //_sqlcommand.CommandText = " select sFirstName as GuarantorFName,sMiddleName as GuarantorMName,sLastName as GuarantorLName,sAddressLine1 as GuarantorAdd1,sAddressLine2 as GuarantorAdd2,sCity as GuarantorCity,sState as GuarantorState,sZIP as GuarantorZip from Patient where nPatientID=" + PatientID + "";

        //            _sqlcommand.CommandText = " select sFirstName as GuarantorFName,sMiddleName as GuarantorMName,sLastName as GuarantorLName, " +
        //                                     " CASE sAddressLine1 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sAddressLine1 END AS GuarantorAdd1, " +
        //                                     " CASE sAddressLine2 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sAddressLine2 END AS GuarantorAdd2, " +
        //                                     " CASE sCity WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sCity END AS GuarantorCity, " +
        //                                     " CASE sState WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sState END AS GuarantorState, " +
        //                                     " CASE sZIP WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sZIP END AS GuarantorZip " +
        //                                     " from Patient where nPatientID=" + nPatientID + " ";

        //            _sqlcommand.Connection = oConnection;

        //            da = new SqlDataAdapter(_sqlcommand);
        //            da.Fill(_dsReports, "dt_GuarantorDetails");

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    finally
        //    {
        //        if (_sqlcommand != null) { _sqlcommand.Dispose(); }
        //        if (oConnection != null) { oConnection.Dispose(); }
        //        if (da != null) { da.Dispose(); }
        //    }
        //}

        //private void FetchDisplaySettings(Int64 CriteriaID, Int64 nPatientID)
        //{
        //    SqlCommand _sqlcommand = new SqlCommand();
        //    SqlConnection oConnection = new SqlConnection();
        //    SqlDataAdapter da = null;
        //    try
        //    {
        //        oConnection.ConnectionString = _databaseconnectionstring;


        //        _sqlcommand.CommandText = " SELECT nStatementCriteriaID,CASE sPracAddress1 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracAddress1 END AS sPracAddress1, " +
        //                                    " CASE sPracAddress2 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracAddress2 END AS sPracAddress2," +
        //                                    " CASE sPracCity WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracCity END AS sPracCity," +
        //                                    " CASE sPracState WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracState END AS sPracState," +
        //                                    " CASE sPracZip WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracZip END AS sPracZip," +
        //                                    " sBillingContactName, sCreditCard, sBillingContactPhone,   " +
        //                                    " dbo.CONVERT_TO_TIME(nOfficeEndTime) as nOfficeEndTime,  " +
        //                                    " dbo.CONVERT_TO_TIME(nOfficeStartTime) as nOfficeStartTime, sPracticeTaxID, " +
        //                                    " CASE sRemitAddress1 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sRemitAddress1 END AS sRemitAddress1," +
        //                                    " CASE sRemitAddress2 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sRemitAddress2 END AS sRemitAddress2," +
        //                                    " CASE sRemitCity WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sRemitCity END AS sRemitCity," +
        //                                    " CASE sRemitState WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracState END AS sRemitState," +
        //                                    " CASE sRemitZip WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracZip END AS sRemitZip," +
        //                                    " bitIsPendingInsurance,  sClinicMessage1, sClinicMessage2, bitIsGuarantorIndicator, nClinicId " +
        //                                    " FROM RPT_PatStatementCriteria_Display  where nStatementCriteriaID= " + CriteriaID + "";



        //        _sqlcommand.Connection = oConnection;
        //        da = new SqlDataAdapter(_sqlcommand);
        //        da.Fill(_dsReports, "dt_DisplaySettings");

        //        if (_dsReports.Tables["dt_DisplaySettings"].Rows.Count == 0)
        //        {

        //            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //            oDB.Connect(false);
        //            Object criteria = oDB.ExecuteScalar_Query(" select nStatementCriteriaID from RPT_PatStatementCriteria_MST where bitIsDefault=1");
        //            Int64 _criteriaID = 0;
        //            if (criteria != null && Convert.ToString(criteria) != "")
        //            {
        //                _criteriaID = Convert.ToInt64(criteria);
        //            }

        //            _sqlcommand.CommandText = " SELECT nStatementCriteriaID,CASE sPracAddress1 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracAddress1 END AS sPracAddress1, " +
        //                                " CASE sPracAddress2 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracAddress2 END AS sPracAddress2," +
        //                                " CASE sPracCity WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracCity END AS sPracCity," +
        //                                " CASE sPracState WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracState END AS sPracState," +
        //                                " CASE sPracZip WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracZip END AS sPracZip," +
        //                                " sBillingContactName, sCreditCard, sBillingContactPhone,   " +
        //                                " dbo.CONVERT_TO_TIME(nOfficeEndTime) as nOfficeEndTime,  " +
        //                                " dbo.CONVERT_TO_TIME(nOfficeStartTime) as nOfficeStartTime, sPracticeTaxID, " +
        //                                " CASE sRemitAddress1 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sRemitAddress1 END AS sRemitAddress1," +
        //                                " CASE sRemitAddress2 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sRemitAddress2 END AS sRemitAddress2," +
        //                                " CASE sRemitCity WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sRemitCity END AS sRemitCity," +
        //                                " CASE sRemitState WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracState END AS sRemitState," +
        //                                " CASE sRemitZip WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracZip END AS sRemitZip," +
        //                                " bitIsPendingInsurance,  sClinicMessage1, sClinicMessage2, bitIsGuarantorIndicator, nClinicId " +
        //                                " FROM RPT_PatStatementCriteria_Display  where nStatementCriteriaID= " + _criteriaID + "";


        //            SqlDataAdapter da2 = new SqlDataAdapter(_sqlcommand);
        //            da2.Fill(_dsReports, "dt_DisplaySettings");
        //        }

        //        _sqlcommand.CommandText = "select sStatementNote From Patient_Statement_Notes where nPatientId=" + nPatientID + " AND nFromDate <= " + gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()) + " AND nToDate >= " + gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()) + " ";
        //        da = new SqlDataAdapter(_sqlcommand);
        //        DataTable dtMessage1 = new DataTable();
        //        da.Fill(dtMessage1);

        //        if (dtMessage1 != null && dtMessage1.Rows.Count > 0)
        //        {
        //            if (_dsReports.Tables["dt_DisplaySettings"] != null && _dsReports.Tables["dt_DisplaySettings"].Rows.Count > 0)
        //            {
        //                _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sClinicMessage1"] = Convert.ToString(dtMessage1.Rows[0]["sStatementNote"]);
        //                _dsReports.AcceptChanges();
        //            }
        //        }

        //        da.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        if (_sqlcommand != null) { _sqlcommand.Dispose(); }
        //        if (oConnection != null) { oConnection.Dispose(); }
        //        if (da != null) { da.Dispose(); }
        //    }
        //}
        #endregion

        #region "Paper Patient Statment"
        private void fillRevisedPatientStatement(Int64 PatientID)
        {
            bool _isExclude = false;
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
                try
                {
                    oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    _sqlQuery = " SELECT nPatientID FROM PatientSettings WHERE sValue = 1 AND sName = 'Exclude from Statement'";
                    oDB.Connect(false);
                    dtTemp = new DataTable();
                    oDB.Retrive_Query(_sqlQuery, out dtTemp);
                    oDB.Disconnect();
                }
                catch (Exception Ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                }

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        if (PatientID == Convert.ToInt64(dtTemp.Rows[i]["nPatientID"].ToString()))
                        {
                            _isExclude = true;
                        }
                    }
                }

                if (!_isExclude)
                {
                    //if (objrptPatientStatementForGateWayEDI != null)
                    //{
                    //    if (objrptPatientStatementForGateWayEDI.IsLoaded)
                    //    {
                    //        objrptPatientStatementForGateWayEDI.Close();
                    //    }
                    //    objrptPatientStatementForGateWayEDI.Dispose();
                    //    //objrptPatientStatementForGateWayEDI = null;
                    //}
                    crvReportViewer.ReportSource = null;


                    //objrptPatientStatementForGateWayEDI = new Rpt_PatientStatement();
                    //objrptPatientStatementForGateWayEDI = new Rpt_Paper_PatientStatement();
                    SqlCommand _sqlcommand = new SqlCommand();
                    SqlConnection oConnection = new SqlConnection();
                    SqlDataAdapter da = new SqlDataAdapter();
                    _dsPatientStatement = new dsRevisedPatientStatement();
                    try
                    {
                        #region "Fetch Report Info"
                        #region "Remit Settings"

                        try
                        {
                            oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            _sqlQuery = " SELECT * FROM RPT_Patstatementcriteria_MST WHERE bitIsDefault = 1 AND criteriaType = 'DISPLAY' ";
                            oDB.Connect(false);
                            dtTemp = new DataTable();
                            oDB.Retrive_Query(_sqlQuery, out dtTemp);
                            oDB.Disconnect();
                        }
                        catch (Exception Ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                        }
                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            nStatementCriteriaID = Convert.ToInt64(dtTemp.Rows[0]["nStatementCriteriaID"].ToString());
                            fetchRevisedRemitDetails(nStatementCriteriaID, _dsPatientStatement);
                            fetchPayToDetails(nStatementCriteriaID, PatientID,_dsPatientStatement);
                        }
                        else
                        {
                            fetchRevisedRemitDetails(0, _dsPatientStatement);
                            fetchPayToDetails(nStatementCriteriaID, PatientID, _dsPatientStatement);
                        }
                        #endregion
                        #region "Display Settings"

                        fetchRevisedDisplaySettings(_ClinicID, PatientID, _dsPatientStatement);
                        #endregion

                        #region "Statement Notes"
                        //string _CloseDate = getCloseDate(); 
                        int EndDate = 0;
                        if (_chkTabFlag)
                        {
                            EndDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Text);
                        }
                        else
                        {
                            EndDate = gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Text);
                        }
                        try
                        {
                            oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            //_sqlQuery = "SELECT * FROM Patient_Statement_Notes WHERE nPatientID = " + PatientID + " AND nClinicID = " + _ClinicID + " AND nToDate = " + gloDateMaster.gloDate.DateAsNumber(_CloseDate) + "";
                            _sqlQuery = "SELECT * FROM Patient_Statement_Notes WHERE nPatientID = " + PatientID + " AND nClinicID = " + _ClinicID + " AND ( nfromdate <= " + EndDate + " AND nToDate >= " + EndDate + ")";
                            oDB.Connect(false);
                            dtTemp = new DataTable();
                            oDB.Retrive_Query(_sqlQuery, out dtTemp);
                            oDB.Disconnect();
                        }
                        catch (Exception Ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
                        }
                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            _sStatementNotes = string.Empty;
                            for (int i = 0; i < dtTemp.Rows.Count; i++)
                            {
                                _sStatementNotes = _sStatementNotes + dtTemp.Rows[i]["sStatementNote"].ToString();
                            }
                        }
                        else
                        {
                            _sStatementNotes = string.Empty;
                        }
                        #endregion

                        //int EndDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Text);
                        EndDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Text);
                        objrptPatientStatementForGateWayEDI = CreateReport(PatientID, EndDate, ClinicID, _dsPatientStatement, objrptPatientStatementForGateWayEDI);
                        objrptPatientStatementForGateWayEDI.SetParameterValue(0, _sStatementNotes);
                        //'To view report in report viewer


                        crvReportViewer.ReportSource = objrptPatientStatementForGateWayEDI;
                        #endregion

                        //For Assigning the Reports with a Datatable 
                        //objrptPatientStatementForGateWayEDI.SetDataSource(_dsReports);

                        //Binds the Report to the Report viewer
                        //crvReportViewer.ReportSource = objrptPatientStatementForGateWayEDI;
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
                        //_dsReports.Dispose();

                    }
                }
                else
                {
                    MessageBox.Show("Selected patient is excluded from statement.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tsb_Send.Visible = false;
                }
            }
        }

        private void fetchRevisedRemitDetails(Int64 CriteriaID, dsRevisedPatientStatement _dsPatientStatement)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;
            // _dsPatientStatement = new dsRevisedPatientStatement();
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


        private void fetchPayToDetails(Int64 CriteriaID, Int64 PatientID, dsRevisedPatientStatement _dsPatientStatement)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;
            Int64 nPayableTo = 0;
            try
            {


                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                _sqlQuery = "SELECT ISNULL(nPayableTo,'') as nPayableTo from RPT_PatStatementCriteria_MST where nStatementCriteriaID =" + CriteriaID + "";
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


        private void fetchRevisedDisplaySettings(Int64 _nClinicID, Int64 PatientID, dsRevisedPatientStatement _dsPatientStatement)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;
            // _dsPatientStatement = new dsRevisedPatientStatement();

            try
            {

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandText = "SELECT ISNULL(Patient.nPatientID, 0) AS PatientID, " +
                                        " ISNULL(Patient.sFirstName, '''') + SPACE(1) + ISNULL(Patient.sMiddleName, '''') + SPACE(1) + ISNULL(Patient.sLastName, '''') AS sPatientName, " +
                                        " ISNULL(Patient.sAddressLine1, '''') AS sPatAddress1, ISNULL(Patient.sAddressLine2, '''') AS sPatAddress2, ISNULL(Patient.sCity, '''') AS sPatCity, " +
                                        " ISNULL(Patient.sState, '''') AS sPatState, ISNULL(Patient.sZIP, '''') AS sPatZip,ISNULL(Patient.sPhone, '''') AS sPatPhone," +
                                        " ISNULL(Provider_MST.sFirstName, '''') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '''') + SPACE(1) + ISNULL(Provider_MST.sLastName, '''') AS sProviderName, " +
                                        " ISNULL(Provider_MST.sPracticeAddressline1, '''') AS sProviderAddress1,ISNULL(Provider_MST.sPracticeAddressline2, '''') AS sProviderAddress2," +
                                        " ISNULL(Provider_MST.sPracticeCity, '''') AS sProviderCity,ISNULL(Provider_MST.sPracticeState, '''') AS sProviderState, ISNULL(Provider_MST.sPracticeZIP, '''') AS sProviderZip, " +
                                        " ISNULL(Provider_MST.sPracPhoneNo, '''') AS sProviderPhone, ISNULL(Clinic_MST.sClinicName, '') AS sPracName, ISNULL(Clinic_MST.sAddress1, '') AS sPracAddress1, " +
                                        " ISNULL(Clinic_MST.sAddress2, '') AS sPracAddress2, ISNULL(Clinic_MST.sCity, '') AS sPracCity, ISNULL(Clinic_MST.sState, '') AS sPracState," +
                                        " ISNULL(Clinic_MST.sZIP, '') AS sPracZip, ISNULL(   replace(replace(replace(replace(Clinic_MST.sphoneno,'(',''),')',''),'-',''),' ',''), '') AS sPracPhone, ISNULL(Clinic_MST.sURL, '') AS sPracURL, ISNULL(Clinic_MST.sEmail, '') AS sPracEmail " +
                                        " FROM Patient INNER JOIN " +
                                        " Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID INNER JOIN " +
                                        " Clinic_MST ON Patient.nClinicID = Clinic_MST.nClinicID " +
                                        " WHERE Patient.nPatientID= " + PatientID;

                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(_dsPatientStatement, "dt_DisplaySettings");

                //_sqlcommand.CommandText = "select sStatementNote From Patient_Statement_Notes where nPatientId=" + PatientID + " AND nFromDate <= " + gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()) + " AND nToDate >= " + gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()) + " ";
                //da = new SqlDataAdapter(_sqlcommand);
                //DataTable dtMessage1 = new DataTable();
                //da.Fill(dtMessage1);

                //if (dtMessage1 != null && dtMessage1.Rows.Count > 0)
                //{
                //    if (_dsPatientStatement.Tables["dt_DisplaySettings"] != null && _dsPatientStatement.Tables["dt_DisplaySettings"].Rows.Count > 0)
                //    {
                //        _dsPatientStatement.Tables["dt_DisplaySettings"].Rows[0]["sClinicMessage1"] = Convert.ToString(dtMessage1.Rows[0]["sStatementNote"]);
                //        _dsPatientStatement.AcceptChanges();
                //    }
                //}

                //da.Dispose();
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
            //Rpt_Paper_PatientStatement oCPT = new Rpt_Paper_PatientStatement();
            try
            {

                SqlConnection oConnection = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                //DataTable dt = new DataTable();
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
                //dsPatientStatement.Clear();
                #endregion "Fetch Ageing Bucket Info"

                #region "Fetch Reserve amount Info"

                #endregion

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
                //oConnection.Open();
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(dsPatientStatement, "dt_PatientReserve");

                #endregion

                #region "Fetch Patient Statement Info"

                sqlCmd = new SqlCommand();

                //Fill the dt_PatientCharges_payment table in dataset present in gloReports using store procedure 
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "Rpt_Paper_PatientStatement";
                SqlParameter ParaPatientID = new SqlParameter();
                {
                    ParaPatientID.ParameterName = "@PatientID";
                    ParaPatientID.Value = PatientID;
                    ParaPatientID.Direction = ParameterDirection.Input;
                    ParaPatientID.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaPatientID);
                SqlParameter ParaEndDate = new SqlParameter();
                {
                    ParaEndDate.ParameterName = "@EndDate";
                    ParaEndDate.Value = EndDate;
                    ParaEndDate.Direction = ParameterDirection.Input;
                    ParaEndDate.SqlDbType = SqlDbType.Int;
                }
                sqlCmd.Parameters.Add(ParaEndDate);
                SqlParameter ParaClinicID = new SqlParameter();
                {
                    ParaClinicID.ParameterName = "@ClinicID";
                    ParaClinicID.Value = ClinicID;
                    ParaClinicID.Direction = ParameterDirection.Input;
                    ParaClinicID.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaClinicID);
                sqlCmd.Connection = oConnection;
                sqlCmd.CommandTimeout = 0;
                //oConnection.Open();
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(dsPatientStatement, "dt_PatientCharges_payment");
                #endregion


                //Assign dataset to the report
                if (!object.ReferenceEquals(dsPatientStatement,null))
                objoCharge.SetDataSource(dsPatientStatement);
                //dsPatientStatement.Clear();
            }
            catch (Exception ex)
            {

            }
            return objoCharge;
        }
        #endregion


        private void SavePatientStatementDate(long patientID, DateTime StatementDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                oDBParameters.Add("@SettingName", "SatementDate", ParameterDirection.Input, SqlDbType.VarChar);      //Varchar(250),       
                oDBParameters.Add("@SettingValue", StatementDate.ToShortDateString(), ParameterDirection.Input, SqlDbType.VarChar);      //varchar(250),    
                oDBParameters.Add("@PatientID", patientID, ParameterDirection.Input, SqlDbType.BigInt);      //numeric(18,0),    
                oDBParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);      //numeric(18,0)  

                oDB.Execute("sp_InUpPatientSettings", oDBParameters);

                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private ArrayList FetchPatientId()
        {
            ArrayList oList = new ArrayList();
            try
            {
                if (rbCriteria.Checked)
                {
                    for (int i = 1; i <= c1PatientList.Rows.Count - 1; i++)
                    {
                        oList.Add(c1PatientList.GetData(i, c1PatientList.Cols["PatientID"].Index));
                    }
                }
                else
                {
                    for (int i = 0; i <= cmbPatients.Items.Count - 1; i++)
                    {
                        if (cmbPatients.SelectedValue != null)
                        {
                            cmbPatients.SelectedIndex = i;
                            cmbPatients.Refresh();

                            oList.Add(cmbPatients.SelectedValue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return oList;
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                DBErr = null; 
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
            finally
            {

                oDB.Disconnect();
                if (oDB != null)
                    oDB.Dispose();
            }

            return _result;

        }

        //private void GetPatientWithBalance()
        //{

        //    try
        //    {

        //        SqlCommand _sqlcommand = new SqlCommand();
        //        SqlConnection oConnection = new SqlConnection();

        //        Int64 stDate = 0;
        //        Int64 endDate = 0;
        //        Decimal dueAmt = 0;
        //        DateTime _filterDate = new DateTime();
        //        TimeSpan _tWaitDays = new TimeSpan();
        //        if (lblWaitDays.Text.ToString() != "")
        //        {
        //            _tWaitDays = new TimeSpan(Convert.ToInt32(lblWaitDays.Text.ToString()), 0, 0, 0);
        //            _filterDate = dtCriteriaEndDate.Value.Subtract(_tWaitDays);
        //        }

        //        if (rbCriteria.Checked)
        //        {
        //            dtCriteriaStartDate.Checked = true;
        //            dtCriteriaEndDate.Checked = true;
        //            stDate = gloDateMaster.gloDate.DateAsNumber(dtCriteriaStartDate.Value.ToShortDateString());
        //            endDate = gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString());

        //        }
        //        else
        //        {
        //            stDate = gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString());
        //            endDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString());
        //        }

        //        oConnection.ConnectionString = _databaseconnectionstring;
        //        _sqlcommand.CommandType = CommandType.StoredProcedure;
        //        //_sqlcommand.CommandText = "GetPatientWithCreteria";
        //        _sqlcommand.CommandText = "GET_PATIENT_DUE_WITH_CRITERIA";
        //        _sqlcommand.Connection = oConnection;

        //        if (endDate != 0)
        //        {
        //            _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.BigInt);
        //            _sqlcommand.Parameters["@nEndDate"].Value = endDate;
        //        }
        //        //gloDateMaster.gloDate.DateAsNumber(_filterDate.ToShortDateString().ToString())
        //        //Code Added by Mayuri:20091210
        //        //dueAmt = Convert.ToDecimal(txtDueAmt.Text.ToString());
        //        if(lblDueAmt.Text.ToString()!="")
        //        {
        //            dueAmt = Convert.ToDecimal(lblDueAmt.Text.ToString());
        //        }

        //        //End code Added by Mayuri:20091210

        //        _sqlcommand.Parameters.Add("@nDueAmt", System.Data.SqlDbType.Decimal);
        //        _sqlcommand.Parameters["@nDueAmt"].Value = dueAmt;

        //        _sqlcommand.Parameters.Add("@nClinicID", System.Data.SqlDbType.BigInt);
        //        _sqlcommand.Parameters["@nClinicID"].Value = _ClinicID;

        //        _sqlcommand.Parameters.Add("@nDateCriteria", System.Data.SqlDbType.BigInt);
        //        _sqlcommand.Parameters["@nDateCriteria"].Value = gloDateMaster.gloDate.DateAsNumber(_filterDate.ToShortDateString().ToString()); 


        //        SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
        //        DataTable _dtPatients = new DataTable();
        //        da.Fill(_dtPatients);
        //        da.Dispose();

        //        if (_dtPatients != null && _dtPatients.Rows.Count > 0)
        //        {
        //            if (_dtPatients.Rows.Count <= 0)
        //            {

        //                c1PatientList.Rows.Count = 1;
        //                c1PatientList.Rows.Fixed = 1;
        //            }

        //            DataView _dv = _dtPatients.DefaultView;
        //            c1PatientList.DataSource = _dv;
        //            ////Code Added by Mayuri:20091211
        //            //End code Added by Mayuri:20091211
        //            c1PatientList.Cols["PatientID"].Visible = false;
        //            c1PatientList.Cols["sLastName"].Visible = true;
        //            c1PatientList.Cols["sPatientCode"].Visible = true;
        //            c1PatientList.Cols["sMiddleName"].Visible = true;
        //            c1PatientList.Cols["sFirstName"].Visible = true;
        //            c1PatientList.Cols["dtDOB"].Visible = true;
        //            c1PatientList.Cols["sPhone"].Visible = true;
        //            c1PatientList.Cols["sMobile"].Visible = true;
        //            c1PatientList.Cols["sPatientName"].Visible = false;
        //            c1PatientList.Cols["sSSN"].Visible = true;
        //            c1PatientList.Cols["sProviderName"].Visible = true;
        //            c1PatientList.Cols["sPatientDue"].Visible = false;

        //            c1PatientList.Cols["sPatientCode"].Caption = "Code";
        //            c1PatientList.Cols["sLastName"].Caption = "Last Name";
        //            c1PatientList.Cols["sMiddleName"].Caption = "MI";
        //            c1PatientList.Cols["sFirstName"].Caption = "First Name";
        //            c1PatientList.Cols["dtDOB"].Caption = "DOB";
        //            c1PatientList.Cols["sPhone"].Caption = "Phone";
        //            c1PatientList.Cols["sMobile"].Caption = "Mobile";
        //            c1PatientList.Cols["sSSN"].Caption = "SSN";
        //            c1PatientList.Cols["sProviderName"].Caption = "Provider";

        //            c1PatientList.Cols["sPatientCode"].Width = 130;
        //            c1PatientList.Cols["sLastName"].Width = 130;
        //            c1PatientList.Cols["sMiddleName"].Width = 40;
        //            c1PatientList.Cols["sFirstName"].Width = 130;
        //            c1PatientList.Cols["dtDOB"].Width = 130;
        //            c1PatientList.Cols["sPhone"].Width = 130;
        //            c1PatientList.Cols["sMobile"].Width = 130;
        //            c1PatientList.Cols["sSSN"].Width = 130;
        //            c1PatientList.Cols["sProviderName"].Width = 160;
        //            c1PatientList.Rows[0].Selected = true;
        //        }
        //        else
        //        {

        //            c1PatientList.DataSource = null;
        //            c1PatientList.Rows.Count = 1;
        //            c1PatientList.Rows.Fixed = 1;
        //            c1PatientList.Cols.Count = 1;
        //            //lblTotaldue.Text = _TotalPtdue.ToString();

        //        }
        //        c1PatientList.AutoResize = false;
        //        c1PatientList.AllowEditing = false;
        //        _dtPatients = null;


        //        FilterPatientByName();

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        //if (oDB != null) { oDB.Dispose(); }
        //    }
        //}

        private void GetPatient()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtPatients;


            try
            {
                string _strsqlFetch = "";

                _strsqlFetch = "SELECT DISTINCT PatientID ,sPatientName, "
                               + " sPatientCode,sFirstName,sMiddleName,sLastName,dtDOB,sPhone, "
                               + " sMobile,sSSN,sProviderName From ( "
                               + " SELECT DISTINCT ISNULL(Patient.nPatientID, 0) AS PatientID, "
                               + " ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') "
                               + " + SPACE(1) + ISNULL(Patient.sLastName, '') AS sPatientName,  ISNULL(patient.sPatientCode,'') AS sPatientCode, "
                               + " ISNULL(Patient.sFirstName, '') AS sFirstName,ISNULL(Patient.sMiddleName, '') AS sMiddleName, "
                               + " ISNULL(Patient.sLastName, '') AS sLastName,ISNULL(patient.dtDOB,'') AS dtDOB,ISNULL(patient.sPhone,'') AS sPhone, "
                               + " ISNULL(patient.sMobile,'') As sMobile ,ISNULL(patient.nSSN,'') As sSSN , "
                               + " ISNULL(Provider_MST.sFirstName,'') "
                               + " + SPACE(1) + ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName, "
                               + " isnull(BL_Transaction_MST.nTransactionID,0) as nTransactionID, "
                               + " isnull(BL_Transaction_MST.nTransactionDate,0) as nTransactionDate, "
                               + " ISNULL(BL_Transaction_Lines.sCPTCode, '-') AS sCPTCode, "
                               + " ISNULL(BL_Transaction_Lines.sCPTDescription, '-') AS sCPTDescription, "
                               + " ISNULL(BL_Transaction_MST.sFacilityCode, '-') AS sFacilityCode, "
                               + " Patient.sZIP, "
                    //+" ISNULL(Contacts_MST.nContactID, 0) AS nContactID, "
                               + " ISNULL(BL_EOBPayment_DTL.nPaymentTrayID, 0) AS nPaymentTrayID, "
                               + " ISNULL(BL_Transaction_MST.nChargesDayTrayID, 0) AS nChargesDayTrayID "
                               + " FROM Patient  LEFT OUTER JOIN Provider_MST ON Patient.nProviderID=Provider_MST.nProviderID "
                               + " INNER JOIN BL_Transaction_MST ON BL_Transaction_MST.nPatientID=Patient.nPatientID "
                               + " LEFT OUTER JOIN BL_Transaction_Lines On "
                               + " BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID "
                               + " LEFT OUTER JOIN BL_EOBPayment_DTL ON BL_Transaction_Lines.nTransactionID = BL_EOBPayment_DTL.nBillingTransactionID AND "
                               + " BL_Transaction_Lines.nTransactionDetailID = BL_EOBPayment_DTL.nBillingTransactionDetailID AND "
                               + " BL_Transaction_Lines.nTransactionLineNo = BL_EOBPayment_DTL.nBillingTransactionLineNo "
                               + ")AS Criteria "
                               + " WHERE  PatientID NOT IN (select nPatientID from PatientSettings where svalue='1' and sName='Exclude from Statement') ";

                #region "CPT"

                #endregion

                #region "Charges Tray"

                #endregion

                #region "Payment Tray"

                #endregion

                #region "Facility Code"

                #endregion

                #region "Insurance "

                
                #endregion

                #region "Zip Code "

                #endregion

                #region "Transaction Date"

                if (dtCriteriaStartDate.Checked == true)
                {
                    _strsqlFetch = _strsqlFetch + " AND (nTransactionDate >= " + gloDateMaster.gloDate.DateAsNumber(dtCriteriaStartDate.Value.ToShortDateString()) + " AND nTransactionDate <= " + gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString()) + ") ";
                }

                #endregion

                #region "Due Amount"

                #endregion

                oDB.Connect(false);
                oDB.Retrive_Query(_strsqlFetch, out  _dtPatients);

                if (_dtPatients != null && _dtPatients.Rows.Count > 0)
                {
                    if (_dtPatients.Rows.Count <= 0)
                    {

                        c1PatientList.Rows.Count = 1;
                        c1PatientList.Rows.Fixed = 1;
                    }

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

                    //c1PatientList.Cols["PatientID"].Width = 0;
                    //c1PatientList.Cols["sPatientName"].Width = pnlFilteredPatList.Width -5;

                    c1PatientList.Cols["PatientID"].Caption = "PatientID";
                    c1PatientList.Cols["sPatientCode"].Caption = "Code";
                    c1PatientList.Cols["sPatientName"].Caption = "Patient";
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
                }
                else
                {

                    c1PatientList.DataSource = null;
                    c1PatientList.Rows.Count = 1;
                    c1PatientList.Rows.Fixed = 1;
                    c1PatientList.Cols.Count = 1;

                }
                c1PatientList.AutoResize = false;
                c1PatientList.AllowEditing = false;
                _dtPatients = null;


                FilterPatientByName();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }


        //Find Patient According to filter criteria
        private void GetPatientForCriteria()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtPatients;


            try
            {
                string _strsqlFetch = "";


                _strsqlFetch = "SELECT DISTINCT PatientID,sPatientCode, sPatientName,sFirstName,sMiddleName,sLastName,dtDOB,sPhone,sMobile,sSSN,sProviderName FROM  (  "
                + " SELECT DISTINCT ISNULL(Patient.nPatientID, 0) AS PatientID, ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '')  + SPACE(1) + ISNULL(Patient.sLastName, '') AS sPatientName, "
                + " ISNULL(patient.sPatientCode,'') AS sPatientCode,ISNULL(Patient.sFirstName, '') AS sFirstName,ISNULL(Patient.sMiddleName, '') AS sMiddleName, ISNULL(Patient.sLastName, '') AS sLastName,ISNULL(patient.dtDOB,'') AS dtDOB,ISNULL(patient.sPhone,'') AS sPhone,ISNULL(patient.sMobile,'') As sMobile ,ISNULL(patient.nSSN,'') As sSSN ,"
                + " ISNULL(Provider_MST.sFirstName,'') + SPACE(1) + ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName, "
                + " isnull(BL_Transaction_MST.nTransactionID,0) as nTransactionID, isnull(BL_Transaction_MST.nTransactionDate,0) as nTransactionDate, "
                + " ISNULL(BL_Transaction_Lines.sCPTCode, '-') AS sCPTCode,  ISNULL(BL_Transaction_Lines.sCPTDescription, '-') AS sCPTDescription,ISNULL(BL_Transaction_MST.sFacilityCode, '-') AS sFacilityCode,  Patient.sZIP, ISNULL(Contacts_MST.nContactID, 0) AS nContactID, "
                + " ISNULL(BL_Transaction_MST.nChargesDayTrayID, 0) AS nChargesDayTrayID,  ISNULL(BL_Transaction_Payment_DTL_3.nCloseDayTrayID,0) AS nPaymentTrayID,   "
                + " ISNULL(   "
                + " 		(SELECT SUM(ISNULL(BL_Transaction_Payment_DTL.dCurrentPaymentAmt,0)) "
                + " 		 FROM BL_Transaction_Payment_DTL   "
                + " 		 where BL_Transaction_Payment_DTL.nBillingTransactionID = BL_Transaction_MST.nTransactionID  "
                + " 				and BL_Transaction_Payment_DTL.nBillingTransactionDetailID = BL_Transaction_Lines.nTransactiondetailID   "
                + " 		) ,0)  "
                + " AS NetPayments,  "
                + " 	((	ISNULL(BL_Transaction_Lines.dCharges, 0)  "
                + " 		*  "
                + " 		ISNULL(BL_Transaction_Lines.dUnit, 1) "
                + " 	)) "
                + " as dCharges,  "
                + " ISNULL(((dcharges * dUnit) - isnull(   "
                + " 							(SELECT SUM(ISNULL(BL_Transaction_Payment_DTL.dCurrentPaymentAmt,0))   "
                + " 							 FROM BL_Transaction_Payment_DTL   "
                + " 							 where BL_Transaction_Payment_DTL.nBillingTransactionID = BL_Transaction_MST.nTransactionID   "
                + " 									and BL_Transaction_Payment_DTL.nBillingTransactionDetailID = BL_Transaction_Lines.nTransactiondetailID   "
                + " 							 ) ,0)  ),0)  "
                + " as Balance   "
                + "  "
                + " FROM Provider_MST RIGHT OUTER JOIN Patient ON Provider_MST.nProviderID = Patient.nProviderID LEFT OUTER JOIN BL_Transaction_MST ON Patient.nPatientID = BL_Transaction_MST.nPatientID  "
                + " LEFT OUTER JOIN BL_Transaction_Lines LEFT OUTER JOIN BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_3 LEFT OUTER JOIN Contacts_MST INNER JOIN "
                + " PatientInsurance_DTL ON Contacts_MST.nContactID = PatientInsurance_DTL.nContactID ON BL_Transaction_Payment_DTL_3.nPaymentInsuranceID = PatientInsurance_DTL.nInsuranceID ON  "
                + " BL_Transaction_Lines.nTransactionID = BL_Transaction_Payment_DTL_3.nBillingTransactionID AND  "
                + " BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_Payment_DTL_3.nBillingTransactionDetailID AND  "
                + " BL_Transaction_Lines.nTransactionLineNo = BL_Transaction_Payment_DTL_3.nBillingTransactionLineNo ON  "
                + " BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID "
                + " GROUP BY Patient.nPatientID, Patient.sPatientCode, Patient.sFirstName, Patient.sMiddleName, Patient.sLastName, Patient.dtDOB, Patient.sPhone,  "
                + " Patient.sMobile, Patient.nSSN, BL_Transaction_MST.nTransactionID, BL_Transaction_Lines.nTransactionDetailID,  "
                + " BL_Transaction_MST.nTransactionDate, BL_Transaction_Lines.dCharges, BL_Transaction_Lines.dUnit, BL_Transaction_MST.sFacilityCode,  "
                + " Patient.sZIP, BL_Transaction_MST.nChargesDayTrayID, BL_Transaction_Payment_DTL_3.nCloseDayTrayID, BL_Transaction_Lines.sCPTCode,  "
                + " BL_Transaction_Lines.sCPTDescription, Contacts_MST.nContactID, Provider_MST.sFirstName, Provider_MST.sMiddleName, Provider_MST.sLastName "
                + " )  AS Criteria  "
                + " WHERE  PatientID NOT IN (select nPatientID from PatientSettings where svalue='1' and sName='Exclude from Statement') ";

                #region "CPT"


                #endregion

                #region "Charges Tray"

                #endregion

                #region "Payment Tray"


                #endregion

                #region "Facility Code"

                #endregion

                #region "Insurance "

                #endregion

                #region "Zip Code "

                #endregion

                #region "Transaction Date"

                if (dtCriteriaStartDate.Checked == true)
                {
                    _strsqlFetch = _strsqlFetch + " AND (nTransactionDate >= " + gloDateMaster.gloDate.DateAsNumber(dtCriteriaStartDate.Value.ToShortDateString()) + " AND nTransactionDate <= " + gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString()) + ") ";
                }

                #endregion

                #region "Due Amount"

                Decimal DueAmt = 0;
                //Added by Mayuri:20091210
                //if (txtDueAmt.ToString().Trim() != "")
                if (lblDueAmt.ToString().Trim() != "")
                {
                    //DueAmt = Convert.ToDecimal(txtDueAmt.Text);
                    DueAmt = Convert.ToDecimal(lblDueAmt.Text);

                    //if (rbGreater.Checked == true)
                    //{
                    _strsqlFetch = _strsqlFetch + " group by PatientID,sPatientCode, sPatientName,sFirstName,sMiddleName,sLastName,dtDOB,sPhone,sMobile,sSSN,sProviderName  HAVING   SUM(Criteria.Balance) > " + DueAmt + "";
                    //}
                    //else
                    //{
                    //    _strsqlFetch = _strsqlFetch + " group by PatientID,sPatientCode, sPatientName,sFirstName,sMiddleName,sLastName,dtDOB,sPhone,sMobile,sSSN,sProviderName  HAVING   SUM(Criteria.Balance) < " + DueAmt + "";
                    //}
                }

                #endregion

                oDB.Connect(false);
                oDB.Retrive_Query(_strsqlFetch, out  _dtPatients);
                if (_dtPatients != null && _dtPatients.Rows.Count > 0)
                {
                    if (_dtPatients.Rows.Count <= 0)
                    {

                        c1PatientList.Rows.Count = 1;
                        c1PatientList.Rows.Fixed = 1;
                    }

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

                    c1PatientList.Cols["PatientID"].Caption = "PatientID";
                    c1PatientList.Cols["sPatientCode"].Caption = "Code";
                    c1PatientList.Cols["sPatientName"].Caption = "Patient";
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
                    c1PatientList.Cols["sMiddleName"].Width = 120;
                    c1PatientList.Cols["sFirstName"].Width = 130;
                    c1PatientList.Cols["dtDOB"].Width = 130;
                    c1PatientList.Cols["sPhone"].Width = 130;
                    c1PatientList.Cols["sMobile"].Width = 130;
                    c1PatientList.Cols["sSSN"].Width = 130;
                    c1PatientList.Cols["sProviderName"].Width = 160;
                }
                else
                {

                    c1PatientList.DataSource = null;
                    c1PatientList.Rows.Count = 1;
                    c1PatientList.Rows.Fixed = 1;
                    c1PatientList.Cols.Count = 1;

                }
                c1PatientList.AutoResize = false;
                c1PatientList.AllowEditing = false;
                _dtPatients = null;


                FilterPatientByName();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        //Filter Patients by Name [A-Z] 
        private void FilterPatientByName()
        {
            try
            {
                if (c1PatientList.DataSource != null)
                {
                    //DataView _dv = ((DataView)c1PatientList.DataSource);
                    _dv = ((DataView)c1PatientList.DataSource);
                    string sColumnName = "sLastName";
                    string sFilter = "(";

                    //Display Patients Name Start with From [A] To [Z]
                    if (lblNameFrom.Text.ToString() != "" && lblNameTo.Text.ToString() != "")
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
                        DataTable dtTotalPatientDue = new DataTable();
                        gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                        int CloseDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Text);
                        if (_c1PatientListColumnCount > 0)
                        {
                            if (crTotalDue.Checked)
                            {
                                for (int i = 1; i < c1PatientList.Rows.Count; i++)
                                {
                                    _TotalPatientDue = _TotalPatientDue + Convert.ToDecimal(c1PatientList.GetData(i, "sInsuranceDue"));
                                }
                                lblTotaldue.Text = _TotalPatientDue.ToString();
                            }
                            else if (crPatientDue.Checked)
                            {
                                for (int i = 1; i < c1PatientList.Rows.Count; i++)
                                {
                                    _TotalPatientDue = _TotalPatientDue + Convert.ToDecimal(c1PatientList.GetData(i, "spatientDue"));
                                }
                                lblTotaldue.Text = _TotalPatientDue.ToString();
                            }

                        }
                        else
                        {
                            tsb_Send.Visible = false;
                            tsb_GenerateReport.Visible = false;
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
                ODB.Disconnect();
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        private void FillBatchDetails()
        {
            DataTable dt = new DataTable();
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
        private void GetIndividualDetails()
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
            ODB.Connect(false);
            DataTable dt = new DataTable();
            Decimal _TotalPatientDue = 0;
            Int64 nPatientID = 0;

            try
            {
                if (cmbPatients.SelectedValue != null)
                {
                    if (cmbPatients.SelectedValue.ToString() != "0")
                    {
                        nPatientID = Convert.ToInt64(cmbPatients.SelectedValue);
                    }
                    DataTable dtTotalPatientDue = new DataTable();
                    int CloseDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Text);
                    oDBPatameters.Add("@nPatientId", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPatameters.Add("@nDate", CloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPatameters.Add("@nClinicId", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    ODB.Retrive("BL_GET_PATIENT_BALANCE", oDBPatameters, out dtTotalPatientDue);
                    if (indTotalDue.Checked)
                    {
                        _TotalPatientDue = Convert.ToDecimal(dtTotalPatientDue.Rows[0]["TotalBalance"].ToString());
                    }
                    else if (indPatientDue.Checked)
                    {
                        _TotalPatientDue = Convert.ToDecimal(dtTotalPatientDue.Rows[0]["PatientDue"].ToString()) - Convert.ToDecimal(dtTotalPatientDue.Rows[0]["AvailableReserve"].ToString());
                    }
                    label81.Text = Convert.ToString(_TotalPatientDue);
                    ODB.Disconnect();
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
                    //Code Added by Mayuri:20091214
                    if (dtIndividual != null && dtIndividual.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtIndividual.Rows.Count; i++)
                        {
                            lbldtcreate.Text = dtIndividual.Rows[0]["dtCreateDate"].ToString().Trim();
                            lblUName.Text = dtIndividual.Rows[0]["sUserName"].ToString();
                            lbldtstdate.Text = Convert.ToDateTime(dtIndividual.Rows[0]["dtStatementDate"].ToString()).ToShortDateString();
                            //label53.Text = dtIndividual.Rows[0]["dtcreateDate"].ToString();

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
            }
            //return dt;
        }
        private System.Data.DataTable FillIndividualDetails()
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ODB.Connect(false);
            DataTable dt = new DataTable();
            try
            {
                string sqlQuery = "";
                sqlQuery = "select sUserName, dtCreateDate,dtStatementDate from BL_Batch_PatientStatement_Mst where sBatchName like '" + cmbPatients.Text.Trim().Replace("'","''")  + "%' order by dtcreateDate desc";
                ODB.Retrive_Query(sqlQuery, out dt);
                ODB.Disconnect();
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        //Shweta
        private string getBatchName()
        {

            string sBatchName = "";

            if (rbCriteria.Checked == false)
                sBatchName = cmbPatients.Text.ToString() + " " + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString());
            else
                sBatchName = cmbSettings.Text.ToString() + " " + gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString());
            return sBatchName;

        }
        //Shweta 
        private Int64 CreateBatch_Mst()
        {


            //Master Batch Entry in BL_Batch_PatientStatement_Mst table to maintain statement generated history.....

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);


            string sBatchName = "";
            sBatchName = getBatchName();

            if (rbCriteria.Checked == true)
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
                oDBParameters.Add("@nCrBalance", Convert.ToDecimal(lblDueAmt.Text.ToString()), ParameterDirection.Input, SqlDbType.Decimal);
                oDBParameters.Add("@sCrPateintFromName", lblNameFrom.Text.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@sCrPateintToName", lblNameTo.Text.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@bStatus", true, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
            }

            Object oResult;
            oDB.Execute("sp_INSERT_BL_Batch_PatientStatement_Mst", oDBParameters, out oResult);
            oDB.Disconnect();
            return Convert.ToInt64(oResult);

        }

        #endregion

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
        }

      
        private void cmb_datefilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _filterby = 0;
            _filterby = ((ComboBox)sender).SelectedIndex;
            switch (_filterby)
            {
                case 0://Date Range
                    FilterBy_DateRange();
                    break;
                case 1://Today
                    FilterBy_Today();
                    break;

                case 2://Tomorrow
                    FilterBy_Tomorrow();
                    break;

                case 3://Yesterday
                    FilterBy_Yesterday();
                    break;

                case 4://This week
                    FilterBy_Thisweek();
                    break;

                case 5://Last Week
                    FilterBy_lastweek();
                    break;

                case 6://Current Month
                    FilterBy_currentmonth();
                    break;

                case 7://Last Month
                    FilterBy_lastmonth();
                    break;

                case 8://Current Year
                    FilterBy_currenYear();
                    break;

                case 9://Last 30 days
                    FilterBy_last30days();
                    break;

                case 10://Last 60 days
                    FilterBy_last60days();
                    break;

                case 11://Last 90 days
                    FilterBy_last90days();
                    break;

                case 12://Last 120 days
                    FilterBy_last120days();
                    break;
            }

            if (((ComboBox)sender).Name == "cmbCriteriaTransactionDate" && _filterby != 0)
            {
                dtCriteriaStartDate.Enabled = false;
                dtCriteriaEndDate.Enabled = false;
            }
            else
            {
                dtCriteriaStartDate.Enabled = true;
                dtCriteriaEndDate.Enabled = true;
            }
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

        private void btnModifySettings_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 _nStatementCriteriaID = 0;
                if (cmbSettings != null)
                {
                    if (cmbSettings.SelectedValue.ToString() != "")
                    {
                        _nStatementCriteriaID = Convert.ToInt64(cmbSettings.SelectedValue.ToString());
                    }
                }

                gloBilling.frmSetupPatientStatementCriteria ofrmSetupPatientStatementCriteria = new gloBilling.frmSetupPatientStatementCriteria(_nStatementCriteriaID, _databaseconnectionstring);
                ofrmSetupPatientStatementCriteria.StartPosition = FormStartPosition.CenterScreen;
                ofrmSetupPatientStatementCriteria.ShowDialog();
                ofrmSetupPatientStatementCriteria.Dispose();
                FetchCriteriasCombo();

                cmbSettings.SelectedValue = _nStatementCriteriaID;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSetupSettings_Click(object sender, EventArgs e)
        {
            try
            {
                gloBilling.frmSetupPatientStatementCriteria ofrmSetupPatientStatementCriteria = new gloBilling.frmSetupPatientStatementCriteria(Convert.ToInt64(cmbSettings.SelectedValue.ToString()), _databaseconnectionstring);
                ofrmSetupPatientStatementCriteria.StartPosition = FormStartPosition.CenterScreen;
                ofrmSetupPatientStatementCriteria.ShowDialog();
                ofrmSetupPatientStatementCriteria.Dispose();
                FetchCriteriasCombo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void txtDueAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != Convert.ToChar(8) && e.KeyChar != Convert.ToChar(46))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9]*$") == false)
                    {
                        e.Handled = true;
                    }
                }
                else if (txtDueAmt.Text.Contains(".") && e.KeyChar == Convert.ToChar(46))
                {
                    e.Handled = true;
                }

            }
            catch (Exception ex)
            {
            }
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
                DataTable dtFilterCriterias = new DataTable();
                //_dtFilterCriterias = oPatinetStatementCriteria.GetPatinetStatementCriterias();
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

        }

        private System.Data.DataTable GetDefaultCriteria()
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ODB.Connect(false);
            DataTable dt = new DataTable();
            try
            {
                string sqlQuery = "";
                ODB.Retrive_Query(sqlQuery, out dt);
                ODB.Disconnect();


            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        
        private void FillControlsPerCriteria(Int64 CriteriaID)
        {

            gloBilling.PatinetStatementCriteria oPatinetStatementCriteria = new gloBilling.PatinetStatementCriteria(_databaseconnectionstring);
            try
            {
                if (oPatinetStatementCriteria.GetPatinetStatementCriteria(CriteriaID))
                {

                    if (oPatinetStatementCriteria.PatStatementCriteriaFilter != null)
                    {
                        DataTable oBindTableInsurance = new DataTable();
                        DataRow oRow;
                        oBindTableInsurance.Columns.Add("ID");
                        oBindTableInsurance.Columns.Add("DispName");

                        DataTable oBindTableCPT = new DataTable();
                        oBindTableCPT.Columns.Add("ID");
                        oBindTableCPT.Columns.Add("DispName");


                        for (int i = 0; i < oPatinetStatementCriteria.PatStatementCriteriaFilter.Rows.Count; i++)
                        {
                            DataRow dr = oPatinetStatementCriteria.PatStatementCriteriaFilter.Rows[i];

                            switch (Convert.ToString(dr[0]))
                            {
                                case "Balance":
                                    if (Convert.ToInt32(dr[1]) == 0)
                                    {
                                        //Code Added by Mayuri:20091210-Replaced text by label
                                        //txtDueAmt.Text = Convert.ToString(dr[2]);
                                        lblDueAmt.Text = Convert.ToString(dr[2]);
                                    }
                                    break;
                                case "From":
                                    if (Convert.ToInt32(dr[1]) == 0)
                                    {
                                        //Code Added by Mayuri:20091210
                                        lblNameFrom.Text = Convert.ToString(dr[2]);
                                        //cmbNameFrom.Text  = Convert.ToString(dr[2]);
                                    }
                                    break;
                                case "To":
                                    if (Convert.ToInt32(dr[1]) == 0)
                                    {
                                        //Code Added by Mayuri:20091210
                                        lblNameTo.Text = Convert.ToString(dr[2]);
                                        //cmbNameTo.Text = Convert.ToString(dr[2]);
                                    }
                                    break;
                                case "Wait Days":
                                    if (Convert.ToInt32(dr[1]) == 0)
                                    {
                                        //Code Added by Mayuri:20091210-Replaced cmbWaitDays by lblWaitDays
                                        //cmbWaitDays.Text = Convert.ToString(dr[2]);
                                        lblWaitDays.Text = Convert.ToString(dr[2]);
                                        //End code Added by Mayuri:20091210
                                    }
                                    break;
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void rbNoCriteria_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rbCriteria_CheckedChanged(object sender, EventArgs e)
        {
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

        private bool SavePatientTemplate(string sFileName, string sFilePath, Int64 nPatientID)
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

        private void SavePatientStatementTemplate(Int64 nPatientID)
        {
            try
            {
                string _FileName = "PatientStatement_" + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + "_To_" + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + "_" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + System.DateTime.Now.Millisecond + ".doc";
                // If not exist create directory
                //if (Directory.Exists(Application.StartupPath + "\\MIStemp") == false)
                //{
                //    Directory.CreateDirectory(Application.StartupPath + "\\MIStemp");
                //}
                //string _FilePath = Application.StartupPath + "\\MIStemp\\" + _FileName;
                if (Directory.Exists( appSettings["StartupPath"].ToString()+ "\\MIStemp") == false)
                {
                    Directory.CreateDirectory(appSettings["StartupPath"].ToString() + "\\MIStemp");
                }
                string _FilePath =appSettings["StartupPath"].ToString()  + "\\MIStemp\\" + _FileName;

                //To fill the Reports 
                //FillPatientStatement(nPatientID);
                fillRevisedPatientStatement(nPatientID);

                if (objrptPatientStatementForGateWayEDI != null && objrptPatientStatementForGateWayEDI.IsLoaded == true)
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
                    SavePatientTemplate(_FileName, sFileName, nPatientID);



                    //}
                    #endregion
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        #endregion

        #region " Date Methods "

     




        private void FilterBy_Today()
        {
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_Tomorrow()
        {
            dtpStartDate.Value = DateTime.Now.AddDays(1);
            dtpEndDate.Value = DateTime.Now.AddDays(1);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_Yesterday()
        {
            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));
            dtpEndDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_Thisweek()
        {

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpStartDate.Value = DateTime.Today;
                dtpEndDate.Value = DateTime.Now.Date.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(1, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(2, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(3, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(4, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(5, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(6, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_lastweek()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(7, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(8, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(9, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(10, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(11, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(12, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(13, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_currentmonth()
        {
            //DateTime dtFrom = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, 1);
            //DateTime dtTo = new DateTime(DateTime.Now.Year, dtpStartDate.Value.Month, 1);
            DateTime dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtTo = dtTo.AddMonths(1);
            dtTo = dtTo.AddDays(-(dtTo.Day));
            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = Convert.ToDateTime(dtTo.Date);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_lastmonth()
        {
            DateTime firstDay = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);
            int DaysinMonth = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);
            DateTime lastDay = firstDay.AddMonths(1).AddTicks(-1);
            dtpStartDate.Value = Convert.ToDateTime(firstDay.Date);
            dtpEndDate.Value = Convert.ToDateTime(lastDay.Date);
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_currenYear()
        {

            DateTime dtFrom = new DateTime(DateTime.Now.Year, 1, 1);
            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = DateTime.Today;
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_last30days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(30, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_last60days()
        {
            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(60, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_last90days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(90, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_last120days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(120, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_DateRange()
        {

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;

        }


        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            //dtCriteriaStartDate.Value = dtpStartDate.Value;
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
           
        }
        private string getCloseDate()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            object _Result = oDB.ExecuteScalar_Query("select dbo.Convert_to_date(max(nCloseDayDate)) As CloseDate from BL_CloseDays");
            if (_Result.ToString() != "")
            {
                return _Result.ToString();
            }
            else
            {

                return "";

            }
        }

        #endregion

        #region "C1 Flex Grid Events"

        //Show Patient Statement Report
        private void c1PatientList_DoubleClick(object sender, EventArgs e)
        {
            //tsb_GenerateBatch.Visible = false;
            //tsb_GenerateBatch.Visible = false;
            //try
            //{
                
            //    if (c1PatientList.RowSel > 0)
            //    {
            //        string CloseDate = getCloseDate();
            //        if (CloseDate == "")
            //        {
            //            MessageBox.Show(Convert.ToDateTime(dtpEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        if (CloseDate != "")
            //        {
            //            if (Convert.ToDateTime(dtpEndDate.Value.ToShortDateString()) > Convert.ToDateTime(CloseDate))
            //                MessageBox.Show(Convert.ToDateTime(dtpEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            if (Convert.ToDateTime(dtCriteriaEndDate.Value.ToShortDateString()) > Convert.ToDateTime(CloseDate))
            //                MessageBox.Show(Convert.ToDateTime(dtCriteriaEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //            else
            //            {
            //                Int64 nPatientID = 0;
            //                btnUp_Click(null, null);
            //                if (c1PatientList.DataSource != null)
            //                {
            //                    if (c1PatientList.Rows.Count > 1)
            //                    {
            //                        if (c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index) != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString() != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString().Trim() != "")
            //                        {
            //                            nPatientID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString());

            //                        }

            //                        if (nPatientID > 0)
            //                        {
            //                            //FillPatientStatement(nPatientID);
            //                            fillRevisedPatientStatement(nPatientID);

            //                        }
            //                    }
            //                }
            //            }

            //            dtCriteriaEndDate.Text = CloseDate;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}

        }

        #endregion

        #region "Change Events"

        private void txtDueAmt_TextChanged(object sender, EventArgs e)
        {
            // GetPatient();
            //Commnetd by Mayuri:20091210
            //GetPatientWithBalance();

        }

        private void txtZip_TextChanged(object sender, EventArgs e)
        {
            if (crTotalDue.Checked)
            {
                GetPatientTotalBalance(false);
            }
            else if (crPatientDue.Checked)
            {
                GetPatientBalance(false);
            }
            //GetPatient();
        }

        private void cmbChargesTray_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetPatientWithBalance();
            //GetPatient();
        }

        private void cmbPaymentTray_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetPatient();
            //GetPatientWithBalance();

        }

        private void cmbFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetPatientWithBalance();
            //GetPatient();
        }

        private void cmbNameFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterPatientByName();
        }

        private void dtCriteriaStartDate_EnabledChanged(object sender, EventArgs e)
        {
            //GetPatientWithBalance();
            //GetPatient();
        }

        private void rbLastName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLastName.Checked == true)
            {
                rbLastName.Font = new Font("Tahoma", 9, FontStyle.Bold);
                FilterPatientByName();
            }
            else
            {
                rbLastName.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbFirstName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFirstName.Checked == true)
            {
                rbFirstName.Font = new Font("Tahoma", 9, FontStyle.Bold);
                FilterPatientByName();
            }
            else
            {
                rbFirstName.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }


       

        private void dtCriteriaStartDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtCriteriaEndDate_ValueChanged(object sender, EventArgs e)
        {
            
            //GetPatientWithBalance();

        }

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

        private void tsb_btnShowList_Click(object sender, EventArgs e)
        {
            tsb_GenerateBatch.Visible = true;
            tsb_btnShowList.Visible = false;
            pnlFilteredPatList.Visible = true;
            btnDown.Visible = false;
            btnUp.Visible = true;
            crTotalDue.Visible = false;
            crPatientDue.Visible = false;
            //crPatientDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
            //crTotalDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Bold);
            //crTotalDue.Checked = true;
        }

        #endregion

        #region "Due Date Check Events"

        private void rdbDays_CheckedChanged(object sender, EventArgs e)
        {

            if (rdbDays.Checked == true)
            {
                rdbDays.Font = new Font("Tahoma", 9, FontStyle.Bold);
                //Added By Pramod For Not Reseting the Duration Value
                if (numDuration.Value > 365)
                {
                    numDuration.Value = 1;
                }
                numDuration.Minimum = 1;
                //numDuration.Maximum = 7;
                numDuration.Maximum = 365;
            }
            else
            {

                rdbDays.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rdbWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbWeek.Checked == true)
            {
                rdbWeek.Font = new Font("Tahoma", 9, FontStyle.Bold);

                //Added By Pramod For Not Reseting the Duration Value
                if (numDuration.Value > 48)
                {
                    numDuration.Value = 1;
                    //numDuration.Maximum = 4;

                }
                numDuration.Minimum = 1;
                numDuration.Maximum = 48;
            }
            else
            {

                rdbWeek.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rdbMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMonth.Checked == true)
            {
                rdbMonth.Font = new Font("Tahoma", 9, FontStyle.Bold);

                //Added By Pramod For Not Reseting the Duration Value
                if (numDuration.Value > 18)
                {
                    numDuration.Value = 1;
                }

                numDuration.Minimum = 1;
                numDuration.Maximum = 12;
            }
            else
            {

                rdbMonth.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rdbYear_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbYear.Checked == true)
            {
                rdbYear.Font = new Font("Tahoma", 9, FontStyle.Bold);
                numDuration.Minimum = 1;
                numDuration.Maximum = 25;
            }
            else
            {

                rdbYear.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rdbCriteriaDays_CheckedChanged(object sender, EventArgs e)
        {

            if (rdbCriteriaDays.Checked == true)
            {
                rdbCriteriaDays.Font = new Font("Tahoma", 9, FontStyle.Bold);
                //Added By Pramod For Not Reseting the Duration Value
                if (numCriteriaDuration.Value > 365)
                {
                    numCriteriaDuration.Value = 1;
                }
                numCriteriaDuration.Minimum = 1;
                //numDuration.Maximum = 7;
                numCriteriaDuration.Maximum = 365;
            }
            else
            {

                rdbCriteriaDays.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rdbCriteriaWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCriteriaWeek.Checked == true)
            {
                rdbCriteriaWeek.Font = new Font("Tahoma", 9, FontStyle.Bold);

                //Added By Pramod For Not Reseting the Duration Value
                if (numCriteriaDuration.Value > 48)
                {
                    numCriteriaDuration.Value = 1;
                    //numDuration.Maximum = 4;

                }
                numCriteriaDuration.Minimum = 1;
                numCriteriaDuration.Maximum = 48;
            }
            else
            {

                rdbCriteriaWeek.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rdbCriteriaMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCriteriaMonth.Checked == true)
            {
                rdbCriteriaMonth.Font = new Font("Tahoma", 9, FontStyle.Bold);

                //Added By Pramod For Not Reseting the Duration Value
                if (numCriteriaDuration.Value > 18)
                {
                    numCriteriaDuration.Value = 1;
                }

                numCriteriaDuration.Minimum = 1;
                numCriteriaDuration.Maximum = 12;
            }
            else
            {

                rdbCriteriaMonth.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rdbCriteriaYear_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCriteriaYear.Checked == true)
            {
                rdbCriteriaYear.Font = new Font("Tahoma", 9, FontStyle.Bold);
                numCriteriaDuration.Minimum = 1;
                numCriteriaDuration.Maximum = 25;
            }
            else
            {

                rdbCriteriaYear.Font = new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void grpFilterCriteria_Enter(object sender, EventArgs e)
        {

        }


        #endregion

        private void lblDueAmt_TextChanged(object sender, EventArgs e)
        {
            //GetPatientWithBalance();
        }

        private void cmbPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSettings.SelectedValue != null)
            {
                if (cmbSettings.SelectedValue.ToString() != "0")
                {
                    GetIndividualDetails();
                }
            }

        }

        private void tsb_GenerateBatch_Click(object sender, EventArgs e)
        {
            _isGenerateBatch = true;
            _IsIndividualTrue = false;
            SetButtonVisibility("GenerateBatch");
            DataTable dtIndividual = new DataTable();
            try
            {
                string CloseDate = getCloseDate();
                if (CloseDate == "")
                {
                    MessageBox.Show(Convert.ToDateTime(dtpEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (CloseDate != "")
                {
                    if (_chkTabFlag)
                    {
                        if (Convert.ToDateTime(dtpEndDate.Value.ToShortDateString()) > Convert.ToDateTime(CloseDate))
                        {
                            MessageBox.Show(Convert.ToDateTime(dtpEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tsb_GenerateBatch.Visible = false;
                            dtpEndDate.Text = CloseDate;
                            tsb_btnHideCriteria_Click(null, null);
                        }
                        else
                        {
                            _IsIndividualTrue = true;
                        }
                    }
                    else
                    {
                        if (Convert.ToDateTime(dtCriteriaEndDate.Value.ToShortDateString()) > Convert.ToDateTime(CloseDate))
                        {
                            MessageBox.Show(Convert.ToDateTime(dtCriteriaEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tsb_GenerateBatch.Visible = true;
                            dtCriteriaEndDate.Text = CloseDate;
                        }
                        else
                        {
                            _IsIndividualTrue = true;
                        }
                    }
                }
                if (_IsIndividualTrue)
                {
                    if (Convert.ToDateTime(dtpEndDate.Value.ToShortDateString()) <= Convert.ToDateTime(CloseDate))
                    {
                        if (cmbSettings.SelectedValue != null)
                        {
                            if (cmbSettings.SelectedValue != null)
                            {
                                if (cmbSettings.SelectedValue.ToString() != "0")
                                {

                                    panel5.Visible = true;
                                    FillControlsPerCriteria(Convert.ToInt64(cmbSettings.SelectedValue));
                                    if (crTotalDue.Checked)
                                    {
                                        GetPatientTotalBalance(false);
                                    }
                                    else if (crPatientDue.Checked)
                                    {
                                        GetPatientBalance(false);
                                    }

                                    #region "Fill Batch Details"
                                    if (cmbSettings.SelectedValue != null)
                                    {
                                        if (cmbSettings.SelectedValue != null)
                                        {
                                            if (cmbSettings.SelectedValue.ToString() != "0")
                                            {
                                                lblSettings.Text = cmbSettings.Text.ToString();
                                                FillBatchDetails();
                                            }
                                        }
                                    }
                                    #endregion
                                    if (c1PatientList.Rows.Count > 1)
                                        tsb_GenerateReport.Visible = true;
                                    else
                                    {
                                        MessageBox.Show("No patient found for selected setting.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        tsb_GenerateReport.Visible = false;
                                    }
                                    gloC1FlexStyle.Style(c1PatientList, true);
                                }
                            }
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

        //Event for showing the ToolTip on DropList 
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
                                        //FillPatientStatement(nPatientID);
                                        fillRevisedPatientStatement(nPatientID);

                                    }
                                }
                            }
                        }
                        dtCriteriaEndDate.Text = CloseDate;
                    }
                    crTotalDue.Visible = false;
                    crPatientDue.Visible = false;

                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void indTotalDue_Click(object sender, EventArgs e)
        {
            indPatientDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
            indTotalDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Bold);

        }
        
        private void indPatientDue_Click(object sender, EventArgs e)
        {
            indPatientDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Bold);
            indTotalDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);

        }


        private void crTotalDue_Click_1(object sender, EventArgs e)
        {
            crPatientDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
            crTotalDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Bold);
            
        }

        private void crPatientDue_Click(object sender, EventArgs e)
        {
            crPatientDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Bold);
            crTotalDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
        }

        private void GetPatientBalance(bool _sendFlag)
        {
         try
            {

                SqlCommand _sqlcommand = new SqlCommand();
                SqlConnection oConnection = new SqlConnection();

                Int64 stDate = 0;
                Int64 endDate = 0;
                Decimal dueAmt = 0;
                DateTime _filterDate = new DateTime();
                TimeSpan _tWaitDays = new TimeSpan();
                if (lblWaitDays.Text.ToString() != "")
                {
                    _tWaitDays = new TimeSpan(Convert.ToInt32(lblWaitDays.Text.ToString()), 0, 0, 0);
                    _filterDate = dtCriteriaEndDate.Value.Subtract(_tWaitDays);
                }

                if (rbCriteria.Checked)
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
                if(lblDueAmt.Text.ToString()!="")
                {
                    dueAmt = Convert.ToDecimal(lblDueAmt.Text.ToString());
                }

                _sqlcommand.Parameters.Add("@nDueAmt", System.Data.SqlDbType.Decimal);
                _sqlcommand.Parameters["@nDueAmt"].Value = dueAmt;

                _sqlcommand.Parameters.Add("@nClinicID", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nClinicID"].Value = _ClinicID;

                _sqlcommand.Parameters.Add("@nDateCriteria", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nDateCriteria"].Value = gloDateMaster.gloDate.DateAsNumber(_filterDate.ToShortDateString().ToString()); 


                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                DataTable _dtPatients = new DataTable();
                da.Fill(_dtPatients);
                da.Dispose();

                if (_dtPatients != null && _dtPatients.Rows.Count > 0)
                {
                    if (_dtPatients.Rows.Count <= 0)
                    {

                        c1PatientList.Rows.Count = 1;
                        c1PatientList.Rows.Fixed = 1;
                    }

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
                    crPatientDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                    crTotalDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Bold);
                    crTotalDue.Checked = false;
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
                //if (oDB != null) { oDB.Dispose(); }
            }
        }

        private void GetPatientTotalBalance(bool _sendFlag)
        {
            try
            {

                SqlCommand _sqlcommand = new SqlCommand();
                SqlConnection oConnection = new SqlConnection();

                Int64 stDate = 0;
                Int64 endDate = 0;
                Decimal dueAmt = 0;
                DateTime _filterDate = new DateTime();
                TimeSpan _tWaitDays = new TimeSpan();
                if (lblWaitDays.Text.ToString() != "")
                {
                    _tWaitDays = new TimeSpan(Convert.ToInt32(lblWaitDays.Text.ToString()), 0, 0, 0);
                    _filterDate = dtCriteriaEndDate.Value.Subtract(_tWaitDays);
                }

                if (rbCriteria.Checked)
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
                _sqlcommand.CommandText = "GET_Insurance_DueList";
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;
                if (endDate != 0)
                {
                    _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.BigInt);
                    _sqlcommand.Parameters["@nEndDate"].Value = endDate;
                }
                if (lblDueAmt.Text.ToString() != "")
                {
                    dueAmt = Convert.ToDecimal(lblDueAmt.Text.ToString());
                }

                _sqlcommand.Parameters.Add("@nDueAmt", System.Data.SqlDbType.Decimal);
                _sqlcommand.Parameters["@nDueAmt"].Value = dueAmt;

                _sqlcommand.Parameters.Add("@nClinicID", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nClinicID"].Value = _ClinicID;

                _sqlcommand.Parameters.Add("@nDateCriteria", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nDateCriteria"].Value = gloDateMaster.gloDate.DateAsNumber(_filterDate.ToShortDateString().ToString());


                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                DataTable _dtPatients = new DataTable();
                da.Fill(_dtPatients);
                da.Dispose();

                if (_dtPatients != null && _dtPatients.Rows.Count > 0)
                {
                    if (_dtPatients.Rows.Count <= 0)
                    {

                        c1PatientList.Rows.Count = 1;
                        c1PatientList.Rows.Fixed = 1;
                    }

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
                    c1PatientList.Cols["sInsuranceDue"].Visible = false;

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
                    if (!_sendFlag)
                    MessageBox.Show("No patients found for selected setting.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    crPatientDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                    crTotalDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Bold);
                    crTotalDue.Checked = false;
                    c1PatientList.DataSource = null;
                    c1PatientList.Rows.Count = 1;
                    c1PatientList.Rows.Fixed = 1;
                    c1PatientList.Cols.Count = 1;
                    //tsb_Send.Visible = false;

                }
                c1PatientList.AutoResize = false;
                c1PatientList.AllowEditing = false;
                _dtPatients = null;


                FilterPatientByName();
                gloC1FlexStyle.Style(c1PatientList, true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                //if (oDB != null) { oDB.Dispose(); }
            }
        }

        private void GetIndividualPatientBlance(Int64 _nPatientID)
        {
            int _CloseDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Text);
            try
            {
                gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBPatameters = new gloDatabaseLayer.DBParameters();
                ODB.Connect(false);
                Decimal _TotalPatientDue = 0;
                DataTable dtTotalPatientDue = new DataTable();

                oDBPatameters.Add("@nPatientId", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@nDate", _CloseDate, ParameterDirection.Input, SqlDbType.BigInt);
                oDBPatameters.Add("@nClinicId", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                ODB.Retrive("BL_GET_PATIENT_BALANCE", oDBPatameters, out dtTotalPatientDue);
                if (indTotalDue.Checked)
                {
                    _TotalPatientDue = Convert.ToDecimal(dtTotalPatientDue.Rows[0]["TotalBalance"].ToString());
                }
                else if (indPatientDue.Checked)
                {
                    _TotalPatientDue = Convert.ToDecimal(dtTotalPatientDue.Rows[0]["PatientDue"].ToString()) - Convert.ToDecimal(dtTotalPatientDue.Rows[0]["AvailableReserve"].ToString());
                }
                label81.Text = Convert.ToString(_TotalPatientDue);
                ODB.Disconnect();
            }
            catch (Exception ex)
            {

            }
        }

        private void SetButtonVisibility(string tabName)
        {
            switch (tabName)
            {
                #region "Individual"
                case "Individual" :
                                indPatientDue.Visible = false;
                                tsb_GenerateReport.Visible = true;
                                indTotalDue.Visible = false;
                                indPatientDue.Checked = true;
                                indPatientDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                                indTotalDue.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Bold);
                                tsb_Send.Visible = true;
                                tsb_GenerateReport.Enabled = true;
                                pnlCriteria.Visible = false;
                                pnlPatientList.Visible = true;
                                lblTotaldue.Visible = true;
                                tsb_btnCriteria.Visible = true;
                                tsb_btnHideCriteria.Visible = false;
                                rbCriteria.Checked = false;
                                pnlc1PatientListHeader.Visible = false;
                                pnlFilteredPatList.Visible = false;
                                panel2.Height = 120;
                                tsb_GenerateBatch.Visible = false;
                                tsb_btnShowList.Visible = false;
                    break;
                #endregion "Individual"

                #region "Batch"
                case "Batch" :
                                if (_isGenerateBatch && c1PatientList.Rows.Count > 1)
                                {
                                    tsb_Send.Visible = true;
                                    tsb_GenerateReport.Visible = true;
                                }
                                else
                                {
                                    tsb_GenerateReport.Visible = false;
                                    tsb_btnShowList.Visible = false;
                                    tsb_Send.Visible = false;
                                }
                                crPatientDue.Visible = false;
                                crTotalDue.Visible = false;
                                panel5.Visible = true;
                                pnlCriteria.Visible = true;
                                lblTotaldue.Visible = false;
                                label51.Visible = false;
                                label50.Visible = false;
                                lblCount.Visible = false;
                                label49.Visible = false;
                                tsb_GenerateBatch.Visible = true;
                                pnlPatientList.Visible = false;
                                tsb_btnCriteria.Visible = false;
                                tsb_btnHideCriteria.Visible = true;
                                rbCriteria.Checked = true;
                                pnlCriteria.Enabled = true;
                                pnlc1PatientListHeader.Visible = true;
                                pnlFilteredPatList.Visible = true;
                                panel2.Height = 120;
                    break;
                #endregion "Batch"

                #region "Generate"
                case "Generate" :
                                tsb_GenerateBatch.Visible = false;
                                if (_isGenerateBatch && c1PatientList.Rows.Count > 1 && !_chkTabFlag)
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
                case "FormLoad" :
                                tsb_GenerateReport.Visible = false;
                                tsb_btnShowList.Visible = false;
                                btnUp.Visible = true;
                                pnlCriteria.Visible = true;
                                pnlPatientList.Visible = false;
                                tsb_btnCriteria.Visible = false;
                                tsb_btnHideCriteria.Visible = true;
                                rbCriteria.Checked = true;
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
                                crTotalDue.Visible = false;
                                crPatientDue.Visible = false;
                                indPatientDue.Visible = false;
                                indTotalDue.Visible = false;
                    break;
                #endregion "FormLoad"

                #region "SendBatch"
                case "SendBatch" :
                                c1PatientList.DataSource = null;
                                c1PatientList.Rows.Count = 1;
                                c1PatientList.Rows.Fixed = 1;
                                c1PatientList.Cols.Count = 1;
                                if (!_chkTabFlag)
                                {
                                    tsb_Send.Visible = false;
                                    tsb_GenerateReport.Visible = false;
                                    tsb_GenerateBatch.Visible = true;
                                    tsb_btnShowList.Visible = false;
                                    lblCount.Text = "0";
                                    lblTotaldue.Text = "0";
                                }
                    break;
                #endregion "SendBatch"

                #region "GenerateBatch"
                case "GenerateBatch" :

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
                                crTotalDue.Visible = false;
                                crPatientDue.Visible = false;
                                _generateBatchFlag = true;
                                tsb_GenerateReport.Enabled = true;
                                tsb_Send.Visible = true;
                    break;
                #endregion "GenerateBatch"
            }

        }

        private void frmRpt_PatientStatement_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (objrptPatientStatementForGateWayEDI != null)
            {
                if (objrptPatientStatementForGateWayEDI.IsLoaded)
                {
                    objrptPatientStatementForGateWayEDI.Close();
                }
                objrptPatientStatementForGateWayEDI.Dispose();
                //objrptPatientStatementForGateWayEDI = null;
            }
        }
    }
}
