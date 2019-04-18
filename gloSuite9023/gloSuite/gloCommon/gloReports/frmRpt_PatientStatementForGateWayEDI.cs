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
using gloBilling.Statement;
using gloWord;

namespace gloReports
{
    public partial class frmRpt_PatientStatementForGateWayEDI : Form
    {


        #region " Declarations "


        //For Creating the Object of the Report
        Rpt_PatientStatementForGateWayEDI objrptPatientStatementForGateWayEDI;
        dsReports _dsReports = null;  
        private string _databaseconnectionstring = "";

        private Int64 _nPatientID;

        private StringBuilder sbCPTCode = new StringBuilder();
        private StringBuilder sbPatientID = new StringBuilder();

        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private AxDSOFramer.AxFramerControl wdTemplate;
        private Wd.Document oCurDoc;

        private Wd.Document oTempDoc;
       // private Wd.Application oWordApp;


        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        private gloGeneralItem.gloItems ogloItems = null;

        Font boldFont = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
        Font regularFont = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
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

        public frmRpt_PatientStatementForGateWayEDI(string databaseconnectionstring, Int64 nPatientID)
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

                #endregion
        }

        #endregion

        #region "Form Events"

        private void frmRpt_PatientStatementForGateWayEDI_Load(object sender, EventArgs e)
        {

            try
            {
               // dsReports dsReports = new dsReports();

                btnUp.Visible = true;
                btnUp.BackgroundImage = global::gloReports.Properties.Resources.UP;
                btnUp.BackgroundImageLayout = ImageLayout.Center;

                pnlCriteria.Visible = false;
                pnlPatientList.Visible = true;
                rbCriteria.Checked = false;
                pnlc1PatientListHeader.Visible = false;
                pnlFilteredPatList.Visible = false;
                btnDown.Visible = false;

                Fill_FilterDatesCombo();
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

                    FillPatientStatement(_nPatientID);
                }
                
                //FetchPatientId();
                FillChargesTray();
                FillPaymentTray();
                FillFacilities();
                FetchCriteriasCombo();

                FillPatientNameCriteria();

                panel2.Height = 125;


                gloC1FlexStyle.Style(c1PatientList, true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 
            }  
        }

        private void frmRpt_PatientStatementForGateWayEDI_Paint(object sender, PaintEventArgs e)
        {
        }

        #endregion

        #region "Fill Methods"

       
        //Added By Pramod Nair For including Charges Tray Criteria 20090826
        private void FillChargesTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtChargesTray=null;
        
            try
            {
      //          cmbChargesTray.Items.Clear();
                cmbChargesTray.DataSource = null;
                cmbChargesTray.Items.Clear();
                oDB.Connect(false);

                oDB.Retrive_Query(" select nChargeTrayID,sDescription from BL_ChargesTray", out  _dtChargesTray);

                this.cmbChargesTray.SelectedIndexChanged -= new System.EventHandler(this.cmbChargesTray_SelectedIndexChanged);

                if (_dtChargesTray != null && _dtChargesTray.Rows.Count > 0)
                {
                    DataTable dtChargesTray = new DataTable();
                    dtChargesTray.Columns.Add("nChargeTrayID");
                    dtChargesTray.Columns.Add("sDescription");

                    dtChargesTray.Clear();
                    dtChargesTray.Rows.Add(0, "");

                    for (int i = 0; i < _dtChargesTray.Rows.Count; i++)
                    {
                        dtChargesTray.Rows.Add(_dtChargesTray.Rows[i]["nChargeTrayID"], _dtChargesTray.Rows[i]["sDescription"]);
                    }

                    cmbChargesTray.DataSource = dtChargesTray;
                    cmbChargesTray.DisplayMember = "sDescription";
                    cmbChargesTray.ValueMember = "nChargeTrayID";
                }
                this.cmbChargesTray.SelectedIndexChanged += new System.EventHandler(this.cmbChargesTray_SelectedIndexChanged);
                if (_dtChargesTray != null)
                {
                    _dtChargesTray.Dispose();
                    _dtChargesTray = null;
                }
                
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


        //Added By Pramod Nair For including Charges Tray Criteria 20090826
        private void FillPaymentTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtPaymentTray = null;
         
            try
            {
             //   cmbPaymentTray.Items.Clear();
                cmbPaymentTray.DataSource = null;
                cmbPaymentTray.Items.Clear();
                oDB.Connect(false);

                oDB.Retrive_Query(" select nCloseDayTrayID,sDescription from BL_CloseDayTray", out  _dtPaymentTray);

                this.cmbPaymentTray.SelectedIndexChanged -= new System.EventHandler(this.cmbPaymentTray_SelectedIndexChanged);

                if (_dtPaymentTray != null && _dtPaymentTray.Rows.Count > 0)
                {
                    DataTable dtPaymentTray = new DataTable();
                    dtPaymentTray.Columns.Add("nCloseDayTrayID");
                    dtPaymentTray.Columns.Add("sDescription");

                    dtPaymentTray.Clear();
                    dtPaymentTray.Rows.Add(0, "");

                    for (int i = 0; i < _dtPaymentTray.Rows.Count; i++)
                    {
                        dtPaymentTray.Rows.Add(_dtPaymentTray.Rows[i]["nCloseDayTrayID"], _dtPaymentTray.Rows[i]["sDescription"]);
                    }

                    cmbPaymentTray.DataSource = dtPaymentTray;
                    cmbPaymentTray.DisplayMember = "sDescription";
                    cmbPaymentTray.ValueMember = "nCloseDayTrayID";
                }

                this.cmbPaymentTray.SelectedIndexChanged += new System.EventHandler(this.cmbPaymentTray_SelectedIndexChanged);
                if (_dtPaymentTray != null)
                {
                    _dtPaymentTray.Dispose();
                    _dtPaymentTray = null;
                }
                
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

        // For Filling The Facilities Combo
        private void FillFacilities()
        {
            try
            {
                DataTable _dtLocations = null;
                gloBilling.gloFacility ogloFacilities = new gloBilling.gloFacility(_databaseconnectionstring);
                _dtLocations = ogloFacilities.GetFacilities();

               

                this.cmbFacility.SelectedIndexChanged -= new System.EventHandler(this.cmbFacility_SelectedIndexChanged);

                if (_dtLocations != null && _dtLocations.Rows.Count > 0)
                {
                    DataTable dtLocations = new DataTable();
                    dtLocations.Columns.Add("sFacilityCode");
                    dtLocations.Columns.Add("sFacilityName");

                    dtLocations.Clear();
                    dtLocations.Rows.Add(0, "");

                    for (int i = 0; i < _dtLocations.Rows.Count; i++)
                    {
                        dtLocations.Rows.Add(_dtLocations.Rows[i]["sFacilityCode"], _dtLocations.Rows[i]["sFacilityName"]);
                    }

                    cmbFacility.DataSource = dtLocations;
                    cmbFacility.DisplayMember = "sFacilityName";
                    cmbFacility.ValueMember = "sFacilityCode";
                }

                this.cmbFacility.SelectedIndexChanged += new System.EventHandler(this.cmbFacility_SelectedIndexChanged);
                if (_dtLocations != null)
                {
                    _dtLocations.Dispose();
                    _dtLocations = null;
                }
                ogloFacilities.Dispose();


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillPatientNameCriteria()
        {
            try
            {
                cmbNameFrom.Items.Add("A");
                cmbNameFrom.Items.Add("B");
                cmbNameFrom.Items.Add("C");
                cmbNameFrom.Items.Add("D");
                cmbNameFrom.Items.Add("E");
                cmbNameFrom.Items.Add("F");
                cmbNameFrom.Items.Add("G");
                cmbNameFrom.Items.Add("H");
                cmbNameFrom.Items.Add("I");
                cmbNameFrom.Items.Add("J");
                cmbNameFrom.Items.Add("K");
                cmbNameFrom.Items.Add("L");
                cmbNameFrom.Items.Add("M");
                cmbNameFrom.Items.Add("N");
                cmbNameFrom.Items.Add("O");
                cmbNameFrom.Items.Add("P");
                cmbNameFrom.Items.Add("Q");
                cmbNameFrom.Items.Add("R");
                cmbNameFrom.Items.Add("S");
                cmbNameFrom.Items.Add("T");
                cmbNameFrom.Items.Add("U");
                cmbNameFrom.Items.Add("V");
                cmbNameFrom.Items.Add("W");
                cmbNameFrom.Items.Add("X");
                cmbNameFrom.Items.Add("Y");
                cmbNameFrom.Items.Add("Z");
                cmbNameFrom.Text = "A";

                cmbNameTo.Items.Add("A");
                cmbNameTo.Items.Add("B");
                cmbNameTo.Items.Add("C");
                cmbNameTo.Items.Add("D");
                cmbNameTo.Items.Add("E");
                cmbNameTo.Items.Add("F");
                cmbNameTo.Items.Add("G");
                cmbNameTo.Items.Add("H");
                cmbNameTo.Items.Add("I");
                cmbNameTo.Items.Add("J");
                cmbNameTo.Items.Add("K");
                cmbNameTo.Items.Add("L");
                cmbNameTo.Items.Add("M");
                cmbNameTo.Items.Add("N");
                cmbNameTo.Items.Add("O");
                cmbNameTo.Items.Add("P");
                cmbNameTo.Items.Add("Q");
                cmbNameTo.Items.Add("R");
                cmbNameTo.Items.Add("S");
                cmbNameTo.Items.Add("T");
                cmbNameTo.Items.Add("U");
                cmbNameTo.Items.Add("V");
                cmbNameTo.Items.Add("W");
                cmbNameTo.Items.Add("X");
                cmbNameTo.Items.Add("Y");
                cmbNameTo.Items.Add("Z");
                cmbNameTo.Text = "Z";

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #endregion

        #region "Toolstrip Events"

        //Generate Report
        private void tsb_GenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
               // dsReports dsReports = new dsReports();
                Int64 _nPatientID = 0;
                if (rbCriteria.Checked)
                {
                    GetPatientForCriteria();

                    if (c1PatientList.Cols["PatientID"] != null)
                    {
                        if (c1PatientList.Cols["PatientID"].Index != 0)
                        {
                            if (c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index) != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString() != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString().Trim() != "")
                            {
                                _nPatientID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString());

                            }
                        }
                    }
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

                FillPatientStatement(_nPatientID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);  
            }
        }

        //Export Report
        private void tsb_btnExportReport_Click(object sender, EventArgs e)
        {
            crvReportViewer.ExportReport();
        }

        //Export To Text (EDI Statement)
        private void tsb_btnExportToTxt_Click(object sender, EventArgs e)
        {
           // dsReports dsReports = new dsReports();
            try
            {
                Int64 _nPatientID = 0;
                if (rbCriteria.Checked)
                {
                    if (c1PatientList.Rows.Count > 1 && c1PatientList.RowSel > 0)
                    {
                        if (c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index) != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString() != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString().Trim() != "")
                        {
                            _nPatientID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString());
                        }
                    }
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

                if (_nPatientID > 0)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text File(.txt)|*.txt";
                    saveFileDialog.DefaultExt = ".txt";
                    saveFileDialog.AddExtension = true;
                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        string _FilePath = saveFileDialog.FileName;
                        if (_FilePath.Trim() != "")
                        {
                            ExportPatientStatementToText(_nPatientID,_FilePath, false);

                            if (MessageBox.Show("Export Completed. Do you want to save patient statement template ? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                DateTime dtStatementDate = DateTime.Now;
                                if (rbCriteria.Checked)
                                { dtStatementDate = dtCriteriaStartDate.Value; }
                                else
                                { dtStatementDate = dtpStartDate.Value; }

                                SavePatientStatementDate(_nPatientID, dtStatementDate);
                                SavePatientStatementTemplate(_nPatientID);
                            }

                        }
                    }
                    saveFileDialog.Dispose();
                    saveFileDialog = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                pnlPleasewait.Visible = false;  

            }

        }

        //Export To Batch Text (EDI Statement)
        private void tsb_btnGenerateBatchTxt_Click(object sender, EventArgs e)
        {
          
            try
            {

                Int64  _nPatientID = 0;
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
                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        _FilePath = saveFileDialog.FileName;
                        saveFileDialog.Dispose();
                        saveFileDialog = null;
                    }
                    else
                    {
                        _FilePath = "";
                    }
                    if (_FilePath == "")
                    {
                        if (saveFileDialog != null)
                        {
                            saveFileDialog.Dispose();
                            saveFileDialog = null;
                        }
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
                            ExportPatientStatementToText(_nPatientID,_FilePath, isAppendText);
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
                        //for (int i = 0; i < oListPatientIds.Count; i++)
                        //{
                        //    _nPatientID = Convert.ToInt64(oListPatientIds[i]);
                        //    DateTime dtStatementDate = DateTime.Now;
                        //    if (rbCriteria.Checked)
                        //    { dtStatementDate = dtCriteriaStartDate.Value; }
                        //    else
                        //    { dtStatementDate = dtpStartDate.Value; }

                        //    SavePatientStatementDate(_nPatientID, dtStatementDate);
                        //    SavePatientStatementTemplate(_nPatientID);
                        //}
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
            pnlPleasewait.Visible = true;
            try
            {

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

                for (int i = 0; i < oListPatientIds.Count; i++)
                {
                    string _FileName = "PatientStatement_" + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + "_To_" + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + "_" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + System.DateTime.Now.Millisecond + ".doc";
                    
                    _FilePath = gloSettings.FolderSettings.AppTempFolderPath + "MIStemp";
                    if (Directory.Exists(_FilePath) == false)
                    {
                        Directory.CreateDirectory(_FilePath);
                    }
                    _FilePath = _FilePath + "\\" + _FileName;


                    nPatientID = Convert.ToInt64(oListPatientIds[i]);

                    //To fill the Reports 
                    FillPatientStatement(nPatientID);

                    //if (_dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows.Count > 0)
                    if (objrptPatientStatementForGateWayEDI != null && objrptPatientStatementForGateWayEDI.IsLoaded)
                    {
                        #region "Exporting to Doc"

                        objrptPatientStatementForGateWayEDI.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, _FilePath);

                        wdTemplate = new AxDSOFramer.AxFramerControl();
                        wdTemplate.OnDocumentClosed += wdTemplate_OnDocumentClosed;
                        this.Controls.Add(wdTemplate);
                      //  wdTemplate.Open(_FilePath);
                        object thisObject = (object)_FilePath;
                        Wd.Application oWordApp = null;
                        gloWord.LoadAndCloseWord.OpenDSO(ref wdTemplate, ref thisObject, ref oCurDoc, ref oWordApp);
                        _FilePath = (string)thisObject;
                        oCurDoc = new Microsoft.Office.Interop.Word.Document();
                        oCurDoc = (Microsoft.Office.Interop.Word.Document)wdTemplate.ActiveDocument;

                        //oCurDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                        String sFileName = gloOffice.Supporting.NewDocumentName();

                        object oFileName = (object)sFileName;
                        object missing = System.Reflection.Missing.Value;
                        object oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                        oCurDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                        wdTemplate.Close();
                        wdTemplate.OnDocumentClosed -= wdTemplate_OnDocumentClosed;

                        this.Controls.Remove(wdTemplate);
                        wdTemplate.Dispose();
                        #endregion

                        SavePatientTemplate(_FileName, sFileName, nPatientID);
                    }

                    Application.DoEvents();
                    prgFileGeneration.Value = i + 1;
                    lblFile.Text = "Processing Batch " + prgFileGeneration.Value + "/" + oListPatientIds.Count;

                    Application.DoEvents();

                    System.IO.Directory.Delete(gloSettings.FolderSettings.AppTempFolderPath + "MIStemp", true);
                }


                MessageBox.Show("Generation of Batch Done. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                pnlProgressBar.Visible = false;
                prgFileGeneration.Visible = false;
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
                pnlPleasewait.Visible = false;
            }
        }

        //Close
        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Print
        private void tsb_Print_Click(object sender, EventArgs e)
        {
            crvReportViewer.PrintReport();
        }
       
        //Hide Criteria
        private void tsb_btnHideCriteria_Click(object sender, EventArgs e)
        {
            pnlCriteria.Visible = false;
            pnlPatientList.Visible = true;
            tsb_btnCriteria.Visible = true;
            tsb_btnHideCriteria.Visible = false;
            rbCriteria.Checked = false;
            pnlc1PatientListHeader.Visible = false;
            pnlFilteredPatList.Visible = false;
            panel2.Height = 125;
            //pnlcrvReportViewer.Visible = true;
            tsb_btnShowList.Visible = false;
        }

        //Show Criteria
        private void tsb_btnCriteria_Click(object sender, EventArgs e)
        {
            pnlCriteria.Visible = true;
            pnlPatientList.Visible = false;
            tsb_btnCriteria.Visible = false;
            tsb_btnHideCriteria.Visible = true;
            rbCriteria.Checked = true;
            pnlCriteria.Enabled = true;
            pnlc1PatientListHeader.Visible = true;
            pnlFilteredPatList.Visible = true;
            //pnlcrvReportViewer.Visible = false;
            panel2.Height = 180;
           // panel2.BringToFront();
            tsb_btnShowList.Visible = true;
            tsb_btnShowList.Enabled = false;
        }

        #endregion

        #region "Private Methods"

        private void FillPatientStatement(Int64 nPatientID)
        {

            if (objrptPatientStatementForGateWayEDI != null)
            {
                if (objrptPatientStatementForGateWayEDI.IsLoaded)
                {
                    objrptPatientStatementForGateWayEDI.Close();
                }
                objrptPatientStatementForGateWayEDI.Dispose();
            }
            crvReportViewer.ReportSource = null;

            objrptPatientStatementForGateWayEDI = new Rpt_PatientStatementForGateWayEDI();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            if (_dsReports != null)
            {
                _dsReports.Dispose();
                _dsReports = null;
            }
            _dsReports = new dsReports();
            try
            {
                #region "Fetch Report Header Settings"

                if (cmbSettings.SelectedValue != null && cmbSettings.SelectedValue.ToString() != "")
                {
                    FetchDisplaySettings(Convert.ToInt64(cmbSettings.SelectedValue),nPatientID);
                }
                else
                {
                    FetchDisplaySettings(0, nPatientID);
                }

                #endregion

                #region "Fetch Statement Details "

                Int32 stDate = 0;
                Int32 endDate = 0;

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
                //FetchCPTCode();


                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Patient_Statement_GatewayEDI";
                _sqlcommand.Connection = oConnection;

                 _sqlcommand.Parameters.Add("@nPatientID", System.Data.SqlDbType.NVarChar);
                 _sqlcommand.Parameters["@nPatientID"].Value = nPatientID.ToString();
               
                if (stDate != 0)
                {
                    _sqlcommand.Parameters.Add("@nStartDate", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@nStartDate"].Value = stDate;
                }

                if (endDate != 0)
                {
                    _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@nEndDate"].Value = endDate;
                }


                #region "Show Hide The Charges And Allowed Colums"

                ////If Allowed Amount Pass the Parameter as 1
                //if (_ogloReportViewer.bAllowed)
                //{
                //    _sqlcommand.Parameters.Add("@Mode", System.Data.SqlDbType.Int);
                //    _sqlcommand.Parameters["@Mode"].Value = 1;
                //}

                #endregion

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(_dsReports, "dt_PatientStatementForGateWayEDI");
                da.Dispose();

                #endregion

                #region "Fetch Report Footer settings"

                FetchGuarantorDetails(nPatientID);

                #endregion

                //For Assigning the Reports with a Datatable 
                objrptPatientStatementForGateWayEDI.SetDataSource(_dsReports);

                //Binds the Report to the Report viewer
                crvReportViewer.ReportSource = objrptPatientStatementForGateWayEDI;
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
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                if (oConnection != null && oConnection.State == ConnectionState.Open)
                {
                    oConnection.Close();
                    

                }
                if (oConnection != null)
                {
                    oConnection.Dispose();
                }
                _dsReports = null; 
                //_dsReports.Dispose();
                 
            }
            
        }

        private void FetchGuarantorDetails(Int64 nPatientID)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;
            try
            {


                oConnection.ConnectionString = _databaseconnectionstring;
                //_sqlcommand.CommandText = " select sFirstName as GuarantorFName,sMiddleName as GuarantorMName,sLastName as GuarantorLName,sAddressLine1 as GuarantorAdd1,sAddressLine2 as GuarantorAdd2,sCity as GuarantorCity,sState as GuarantorState,sZIP as GuarantorZip from Patient_OtherContacts where nPatientID=" + PatientID + " and nPatientContactTypeFlag=1";

                _sqlcommand.CommandText = " select sFirstName as GuarantorFName,sMiddleName as GuarantorMName,sLastName as GuarantorLName, " +
                                           " CASE sAddressLine1 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sAddressLine1 END AS GuarantorAdd1, " +
                                           " CASE sAddressLine2 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sAddressLine2 END AS GuarantorAdd2, " +
                                           " CASE sCity WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sCity END AS GuarantorCity, " +
                                           " CASE sState WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sState END AS GuarantorState, " +
                                           " CASE sZIP WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sZIP END AS GuarantorZip " +
                                           " from Patient_OtherContacts where nPatientID=" + nPatientID + " and nPatientContactTypeFlag =1 ";

                _sqlcommand.Connection = oConnection;

                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(_dsReports, "dt_GuarantorDetails");
                da.Dispose();
                da = null;
                if (_dsReports.Tables["dt_GuarantorDetails"].Rows.Count == 0)
                {
                    //_sqlcommand.CommandText = " select sFirstName as GuarantorFName,sMiddleName as GuarantorMName,sLastName as GuarantorLName,sAddressLine1 as GuarantorAdd1,sAddressLine2 as GuarantorAdd2,sCity as GuarantorCity,sState as GuarantorState,sZIP as GuarantorZip from Patient where nPatientID=" + PatientID + "";

                    _sqlcommand.CommandText = " select sFirstName as GuarantorFName,sMiddleName as GuarantorMName,sLastName as GuarantorLName, " +
                                             " CASE sAddressLine1 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sAddressLine1 END AS GuarantorAdd1, " +
                                             " CASE sAddressLine2 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sAddressLine2 END AS GuarantorAdd2, " +
                                             " CASE sCity WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sCity END AS GuarantorCity, " +
                                             " CASE sState WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sState END AS GuarantorState, " +
                                             " CASE sZIP WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sZIP END AS GuarantorZip " +
                                             " from Patient where nPatientID=" + nPatientID + " ";

                    _sqlcommand.Connection = oConnection;

                    da = new SqlDataAdapter(_sqlcommand);
                    da.Fill(_dsReports, "dt_GuarantorDetails");

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                if (oConnection != null && oConnection.State == ConnectionState.Open)
                {
                    oConnection.Close();
                   

                }
                if (oConnection != null)
                {
                    oConnection.Dispose();
                }
                if (da != null) { da.Dispose(); }
            }
        }

        private void FetchDisplaySettings(Int64 CriteriaID,Int64 nPatientID)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            SqlDataAdapter da = null;
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;


                _sqlcommand.CommandText = " SELECT nStatementCriteriaID,CASE sPracAddress1 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracAddress1 END AS sPracAddress1, " +
                                            " CASE sPracAddress2 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracAddress2 END AS sPracAddress2," +
                                            " CASE sPracCity WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracCity END AS sPracCity," +
                                            " CASE sPracState WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracState END AS sPracState," +
                                            " CASE sPracZip WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracZip END AS sPracZip," +
                                            " sBillingContactName, sCreditCard, sBillingContactPhone,   " +
                                            " dbo.CONVERT_TO_TIME(nOfficeEndTime) as nOfficeEndTime,  " +
                                            " dbo.CONVERT_TO_TIME(nOfficeStartTime) as nOfficeStartTime, sPracticeTaxID, " +
                                            " CASE sRemitAddress1 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sRemitAddress1 END AS sRemitAddress1," +
                                            " CASE sRemitAddress2 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sRemitAddress2 END AS sRemitAddress2," +
                                            " CASE sRemitCity WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sRemitCity END AS sRemitCity," +
                                            " CASE sRemitState WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracState END AS sRemitState," +
                                            " CASE sRemitZip WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracZip END AS sRemitZip," +
                                            " bitIsPendingInsurance,  sClinicMessage1, sClinicMessage2, bitIsGuarantorIndicator, nClinicId " +
                                            " FROM RPT_PatStatementCriteria_Display  where nStatementCriteriaID= " + CriteriaID + "";



                _sqlcommand.Connection = oConnection;
                da = new SqlDataAdapter(_sqlcommand);
                da.Fill(_dsReports, "dt_DisplaySettings");
                da.Dispose();
                da = null;
                if (_dsReports.Tables["dt_DisplaySettings"].Rows.Count == 0)
                {

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    oDB.Connect(false);
                    Object criteria = oDB.ExecuteScalar_Query(" select nStatementCriteriaID from RPT_PatStatementCriteria_MST where bitIsDefault=1");
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                    Int64 _criteriaID = 0;
                    if (criteria != null && Convert.ToString(criteria) != "")
                    {
                        _criteriaID = Convert.ToInt64(criteria);
                    }

                    _sqlcommand.CommandText = " SELECT nStatementCriteriaID,CASE sPracAddress1 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracAddress1 END AS sPracAddress1, " +
                                        " CASE sPracAddress2 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracAddress2 END AS sPracAddress2," +
                                        " CASE sPracCity WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracCity END AS sPracCity," +
                                        " CASE sPracState WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracState END AS sPracState," +
                                        " CASE sPracZip WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracZip END AS sPracZip," +
                                        " sBillingContactName, sCreditCard, sBillingContactPhone,   " +
                                        " dbo.CONVERT_TO_TIME(nOfficeEndTime) as nOfficeEndTime,  " +
                                        " dbo.CONVERT_TO_TIME(nOfficeStartTime) as nOfficeStartTime, sPracticeTaxID, " +
                                        " CASE sRemitAddress1 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sRemitAddress1 END AS sRemitAddress1," +
                                        " CASE sRemitAddress2 WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sRemitAddress2 END AS sRemitAddress2," +
                                        " CASE sRemitCity WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sRemitCity END AS sRemitCity," +
                                        " CASE sRemitState WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracState END AS sRemitState," +
                                        " CASE sRemitZip WHEN '' THEN '-' WHEN NULL THEN '-' ELSE sPracZip END AS sRemitZip," +
                                        " bitIsPendingInsurance,  sClinicMessage1, sClinicMessage2, bitIsGuarantorIndicator, nClinicId " +
                                        " FROM RPT_PatStatementCriteria_Display  where nStatementCriteriaID= " + _criteriaID + "";


                    SqlDataAdapter da2 = new SqlDataAdapter(_sqlcommand);
                    da2.Fill(_dsReports, "dt_DisplaySettings");
                    da2.Dispose();
                    da2 = null;
                }

                _sqlcommand.CommandText = "select sStatementNote From Patient_Statement_Notes where nPatientId=" + nPatientID + " AND nFromDate <= " + gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()) + " AND nToDate >= " + gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()) + " ";
                da = new SqlDataAdapter(_sqlcommand);
                DataTable dtMessage1 = new DataTable();
                da.Fill(dtMessage1);
                da.Dispose();
                da = null;
                if (dtMessage1 != null && dtMessage1.Rows.Count > 0)
                {
                    if (_dsReports.Tables["dt_DisplaySettings"] != null && _dsReports.Tables["dt_DisplaySettings"].Rows.Count > 0)
                    {
                        _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sClinicMessage1"] = Convert.ToString(dtMessage1.Rows[0]["sStatementNote"]);
                        _dsReports.AcceptChanges();
                    }
                }
                if (dtMessage1 != null)
                {
                    dtMessage1.Dispose();
                    dtMessage1 = null;
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                if (oConnection != null && oConnection.State == ConnectionState.Open)
                {
                    oConnection.Close();
                     

                }
                if (oConnection != null)
                {
                    oConnection.Dispose();
                }
                if (da != null) { da.Dispose(); }
            }
        }

        private void ExportPatientStatementToText(Int64 nPatientID, string _FilePath, bool isAppendText)
        {
            if (_FilePath.Trim() == "")
            {
                return;
            }

            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            if (_dsReports != null)
            {
                _dsReports.Dispose();
                _dsReports = null;
            }
              _dsReports = new dsReports();

          
            string _sqlQuery = "";
            StreamWriter tw = null;
            try
            {
                oDB.Connect(false);

           
                tw = new StreamWriter(_FilePath, isAppendText);

                #region "Report Header Settings"

                if (cmbSettings.SelectedValue != null && cmbSettings.SelectedValue.ToString() != "")
                {
                    FetchDisplaySettings(Convert.ToInt64(cmbSettings.SelectedValue), nPatientID);
                }
                else
                {
                    FetchDisplaySettings(0, nPatientID);
                }

                #endregion


                #region " Details Section"

                Int32 stDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());
                Int32 endDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString());

                if (rbCriteria.Checked)
                {
                    stDate = gloDateMaster.gloDate.DateAsNumber(dtCriteriaStartDate.Value.ToShortDateString());
                    endDate = gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString());

                }
                else
                {
                    stDate = gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString());
                    endDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString());
                }
                //FetchCPTCode();


                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Patient_Statement_GatewayEDI";
                _sqlcommand.Connection = oConnection;

                _sqlcommand.Parameters.Add("@nPatientID", System.Data.SqlDbType.NVarChar);
                _sqlcommand.Parameters["@nPatientID"].Value = nPatientID.ToString();

                if (stDate != 0)
                {
                    _sqlcommand.Parameters.Add("@nStartDate", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@nStartDate"].Value = stDate;
                }

                if (endDate != 0)
                {
                    _sqlcommand.Parameters.Add("@nEndDate", System.Data.SqlDbType.Int);
                    _sqlcommand.Parameters["@nEndDate"].Value = endDate;
                }



                #region "Show Hide The Charges And Allowed Colums"

                ////If Allowed Amount Pass the Parameter as 1
                //if (_ogloReportViewer.bAllowed)
                //{
                //    _sqlcommand.Parameters.Add("@Mode", System.Data.SqlDbType.Int);
                //    _sqlcommand.Parameters["@Mode"].Value = 1;
                //}

                #endregion

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(_dsReports, "dt_PatientStatementForGateWayEDI");
                da.Dispose();
                #endregion


                #region "Footer settings"

                FetchGuarantorDetails(nPatientID);

                #endregion
                DataTable _dtPatientDetails=null;

                oDB.Retrive_Query("SELECT sPatientCode, sFirstName, sMiddleName, sLastName, sAddressLine1, sAddressLine2, sCity, sState, sZIP " +
                                   " FROM Patient where nPatientId=" + nPatientID + "", out  _dtPatientDetails);


                #region "To write the Display settings to a Text file"


                if (_dsReports.Tables["dt_DisplaySettings"].Rows.Count > 0 && _dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows.Count > 0)
                {
                    tw.WriteLine("StartStatement");
                    tw.WriteLine("StatementGenerateDate\t" + DateTime.Now.ToShortDateString());
                    tw.WriteLine("PracticeName\t" + _dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[0]["ClinicName"].ToString());
                    tw.WriteLine("PracticeAddress\t" + _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sPracAddress1"].ToString() + "\t" + _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sPracAddress2"].ToString() + "\t" + _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sPracCity"].ToString() + "\t" + _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sPracState"].ToString() + "\t" + _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sPracZip"].ToString());
                    //tw.WriteLine("CreditCardInfo\t" + dsReports.Tables["dt_DisplaySettings"].Rows[0]["sCreditCard"].ToString().Replace(",","\t"));
                    //tw.WriteLine("BillingContactInfo\t" + dsReports.Tables["dt_DisplaySettings"].Rows[0]["sBillingContactName"].ToString() + dsReports.Tables["dt_DisplaySettings"].Rows[0]["sBillingContactPhone"].ToString());
                    //tw.WriteLine("OfficeHrs\t" + dsReports.Tables["dt_DisplaySettings"].Rows[0]["nOfficeStartTime"].ToString() + "\t" + dsReports.Tables["dt_DisplaySettings"].Rows[0]["nOfficeEndTime"].ToString());
                    tw.WriteLine("TaxID\t" + _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sPracticeTaxID"].ToString());
                    if (_dtPatientDetails != null && _dtPatientDetails.Rows.Count > 0)
                    {
                        tw.WriteLine("Account\t" + _dtPatientDetails.Rows[0]["sPatientCode"].ToString().Trim());
                        tw.WriteLine("PatientName\t" + _dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[0]["sPatientName"].ToString());
                        tw.WriteLine("SendToAddress\t" + _dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[0]["sPatientName"].ToString() + "\t" + _dtPatientDetails.Rows[0]["sAddressLine1"].ToString() + "\t" + _dtPatientDetails.Rows[0]["sAddressLine2"].ToString() + "\t" + _dtPatientDetails.Rows[0]["sCity"].ToString() + "\t" + _dtPatientDetails.Rows[0]["sState"].ToString() + "\t" + _dtPatientDetails.Rows[0]["sZip"].ToString());
                    }

                    tw.WriteLine("RemitToName\t" + _dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[0]["ClinicName"].ToString());
                    tw.WriteLine("RemitAddress\t" + _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sRemitAddress1"].ToString() + "\t" + _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sRemitAddress2"].ToString() + "\t" + _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sRemitCity"].ToString() + "\t" + _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sRemitState"].ToString() + "\t" + _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sRemitZip"].ToString());
                }
                if (_dtPatientDetails != null)
                {
                    _dtPatientDetails.Dispose();
                    _dtPatientDetails = null;
                }
                #endregion


                #region "Write the details into the text file"

                if (_dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows.Count > 0)
                {
                    tw.WriteLine("TransactionsLineStart");
                    //for (int i = 0; i <= dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows.Count - 1; i++)
                    //{
                    //    tw.WriteLine("\t" + dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[i]["nTransactionDate"].ToString() + "\t" + dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[i]["sCPTCode"].ToString() + "\t" + dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[i]["sCPTDescription"].ToString() + "\t" + dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[i]["dCharges"].ToString() + "\t" + dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[i]["dNetPayments"].ToString());
                    //}

                    //-------------
                    #region "Take Transaction details in temp table for formatting purpose"

                    Int64 _TransactionID = 0;
                    Int64 _TransactionDetailID = 0;
                    DataTable dtTransToWrite = new DataTable();

                    dtTransToWrite.Columns.Add("nTransactionID", typeof(Int64));
                    dtTransToWrite.Columns.Add("nTransactionDetailID", typeof(Int64));
                    dtTransToWrite.Columns.Add("DOS", typeof(String));
                    dtTransToWrite.Columns.Add("CPTCode", typeof(String));
                    dtTransToWrite.Columns.Add("CPTDesc", typeof(String));
                    dtTransToWrite.Columns.Add("Charges", typeof(Decimal));
                    dtTransToWrite.Columns.Add("Insurance", typeof(Decimal));
                    dtTransToWrite.Columns.Add("CoInsurance", typeof(Decimal));
                    dtTransToWrite.Columns.Add("Copay", typeof(Decimal));
                    dtTransToWrite.Columns.Add("Deductible", typeof(Decimal));
                    dtTransToWrite.Columns.Add("Adjustment", typeof(Decimal));
                    dtTransToWrite.Columns.Add("InPending", typeof(Decimal));
                    dtTransToWrite.Columns.Add("IsInPending", typeof(Boolean));

                    DataTable dtTransactions = null;
                    if (_dsReports.Tables["dt_PatientStatementForGateWayEDI"] != null)
                    {
                        dtTransactions = _dsReports.Tables["dt_PatientStatementForGateWayEDI"].Copy();
                    }
                    else
                    {
                        dtTransactions = new DataTable();
                    }

                    for (int i = 0; i < dtTransactions.Rows.Count; i++)
                    {
                        //--------------
                        _TransactionID = Convert.ToInt64(dtTransactions.Rows[i]["nTransactionID"]);
                        _TransactionDetailID = Convert.ToInt64(dtTransactions.Rows[i]["nTransactionDetailID"]);

                        Int32 TransRowIndex = 0;
                        Boolean IsRowFound = false;

                        for (int k = 0; k < dtTransToWrite.Rows.Count; k++)
                        {
                            if (_TransactionID == Convert.ToInt64(dtTransToWrite.Rows[k]["nTransactionID"]) && _TransactionDetailID == Convert.ToInt64(dtTransToWrite.Rows[k]["nTransactionDetailID"]))
                            {
                                TransRowIndex = k;
                                IsRowFound = true;
                                break;
                            }
                        }


                        if (IsRowFound == false)
                        {
                            DataRow dr = dtTransToWrite.NewRow();
                            dr["nTransactionID"] = Convert.ToInt64(dtTransactions.Rows[i]["nTransactionID"]);
                            dr["nTransactionDetailID"] = Convert.ToInt64(dtTransactions.Rows[i]["nTransactionDetailID"]);
                            dr["DOS"] = Convert.ToString(dtTransactions.Rows[i]["nTransactionDate"]);
                            dr["CPTCode"] = Convert.ToString(dtTransactions.Rows[i]["sCPTCode"]);
                            dr["CPTDesc"] = Convert.ToString(dtTransactions.Rows[i]["sCPTDescription"]);
                            dr["Charges"] = Convert.ToDecimal(dtTransactions.Rows[i]["dCharges"]);
                            dr["Insurance"] = 0;
                            dr["CoInsurance"] = 0;
                            dr["Copay"] = 0;
                            dr["Deductible"] = 0;
                            dr["Adjustment"] = 0;
                            dr["InPending"] = 0;
                            dr["IsInPending"] = false;

                            gloBilling.TransactionType TransType = (gloBilling.TransactionType)Convert.ToInt32(dtTransactions.Rows[i]["nTransactionType"]);
                            switch (TransType)
                            {
                                case gloBilling.TransactionType.Adjustment:
                                    dr["Adjustment"] = Convert.ToDecimal(dtTransactions.Rows[i]["dNetPayments"]);
                                    break;
                                case gloBilling.TransactionType.Copay:
                                    dr["Copay"] = Convert.ToDecimal(dtTransactions.Rows[i]["dNetPayments"]);
                                    break;
                                case gloBilling.TransactionType.Deductible:
                                    dr["Deductible"] = Convert.ToDecimal(dtTransactions.Rows[i]["dNetPayments"]);
                                    break;
                                case gloBilling.TransactionType.InsuracePayment:
                                    dr["Insurance"] = Convert.ToDecimal(dtTransactions.Rows[i]["dNetPayments"]);
                                    break;
                                case gloBilling.TransactionType.Coinsurance:
                                    dr["CoInsurance"] = Convert.ToDecimal(dtTransactions.Rows[i]["dNetPayments"]);
                                    break;
                                case gloBilling.TransactionType.WithHold:
                                case gloBilling.TransactionType.PatientPayment:
                                case gloBilling.TransactionType.Refund:
                                case gloBilling.TransactionType.Reassign:
                                case gloBilling.TransactionType.None:
                                case gloBilling.TransactionType.Payment:
                                case gloBilling.TransactionType.Billed:
                                case gloBilling.TransactionType.Receipt:
                                case gloBilling.TransactionType.Writeoff:
                                case gloBilling.TransactionType.Coverage:
                                default:
                                    break;
                            }

                            #region "Find Insurance Pending"
                            _sqlQuery = "SELECT Count(BL_Transaction_Payment_DTL.nPaymentTransactionID) "
                            + " FROM BL_Transaction_Lines INNER JOIN BL_Transaction_MST ON BL_Transaction_Lines.nTransactionID = BL_Transaction_MST.nTransactionID  "
                            + " INNER JOIN BL_Transaction_Payment_DTL ON BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_Payment_DTL.nBillingTransactionDetailID  "
                            + " AND BL_Transaction_Lines.nTransactionID = BL_Transaction_Payment_DTL.nBillingTransactionID AND BL_Transaction_Lines.nTransactionLineNo = BL_Transaction_Payment_DTL.nBillingTransactionLineNo "
                            + " WHERE BL_Transaction_MST.nSendToInsFlag = 1  "
                            + " AND BL_Transaction_Payment_DTL.nTransactionType = 9 "
                            + " AND BL_Transaction_Lines.nTransactionDetailID = " + Convert.ToInt64(dtTransactions.Rows[i]["nTransactionDetailID"]) + " "
                            + " AND BL_Transaction_Lines.nTransactionID = " + Convert.ToInt64(dtTransactions.Rows[i]["nTransactionID"]) + "";

                            object oPaymentCount = oDB.ExecuteScalar_Query(_sqlQuery);

                            if (oPaymentCount != null && Convert.ToString(oPaymentCount) != "")
                            {
                                if (Convert.ToInt64(oPaymentCount) == 0)
                                {
                                    dr["InPending"] = Convert.ToDecimal(dtTransactions.Rows[i]["dCharges"]);
                                    dr["IsInPending"] = true;
                                }
                            }

                            #endregion

                            dtTransToWrite.Rows.Add(dr);
                        }
                        else
                        {
                            gloBilling.TransactionType TransType = (gloBilling.TransactionType)Convert.ToInt32(dtTransactions.Rows[i]["nTransactionType"]);
                            switch (TransType)
                            {
                                case gloBilling.TransactionType.Adjustment:
                                    dtTransToWrite.Rows[TransRowIndex]["Adjustment"] = Convert.ToDecimal(dtTransactions.Rows[i]["dNetPayments"]);
                                    break;
                                case gloBilling.TransactionType.Copay:
                                    dtTransToWrite.Rows[TransRowIndex]["Copay"] = Convert.ToDecimal(dtTransactions.Rows[i]["dNetPayments"]);
                                    break;
                                case gloBilling.TransactionType.Deductible:
                                    dtTransToWrite.Rows[TransRowIndex]["Deductible"] = Convert.ToDecimal(dtTransactions.Rows[i]["dNetPayments"]);
                                    break;
                                case gloBilling.TransactionType.InsuracePayment:
                                    dtTransToWrite.Rows[TransRowIndex]["Insurance"] = Convert.ToDecimal(dtTransactions.Rows[i]["dNetPayments"]);
                                    break;
                                case gloBilling.TransactionType.Coinsurance:
                                    dtTransToWrite.Rows[TransRowIndex]["CoInsurance"] = Convert.ToDecimal(dtTransactions.Rows[i]["dNetPayments"]);
                                    break;
                                case gloBilling.TransactionType.WithHold:
                                case gloBilling.TransactionType.PatientPayment:
                                case gloBilling.TransactionType.Refund:
                                case gloBilling.TransactionType.Reassign:
                                case gloBilling.TransactionType.None:
                                case gloBilling.TransactionType.Payment:
                                case gloBilling.TransactionType.Billed:
                                case gloBilling.TransactionType.Receipt:
                                case gloBilling.TransactionType.Writeoff:
                                case gloBilling.TransactionType.Coverage:
                                default:
                                    break;
                            }
                        }

                        //--------------
                        //tw.WriteLine("\t" + dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[i]["nTransactionDate"].ToString() + "\t" + dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[i]["sCPTCode"].ToString() + "\t" + dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[i]["sCPTDescription"].ToString() + "\t" + dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[i]["dCharges"].ToString() + "\t" + dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows[i]["dNetPayments"].ToString());
                    }
                    dtTransactions.Dispose();
                    dtTransactions = null;

                    #endregion

                    Decimal _TotalBalanceDue = 0;
                    Decimal _TotalInsurancePending = 0;
                    Decimal _TotalCharges = 0;
                    Decimal _TotalPaid = 0;

                    Decimal _30DaysBalanceDue = 0;
                    Decimal _30DaysCharges = 0;
                    Decimal _30DayesPaid = 0;

                    Decimal _60DaysBalanceDue = 0;
                    Decimal _60DaysCharges = 0;
                    Decimal _60DayesPaid = 0;

                    Decimal _90DaysBalanceDue = 0;
                    Decimal _90DaysCharges = 0;
                    Decimal _90DayesPaid = 0;

                    Decimal _120DaysBalanceDue = 0;
                    Decimal _120DaysCharges = 0;
                    Decimal _120DayesPaid = 0;

                    Decimal _Above120DaysBalanceDue = 0;
                    Decimal _Above120DaysCharges = 0;
                    Decimal _Above120DayesPaid = 0;


                    for (int i = 0; i < dtTransToWrite.Rows.Count; i++)
                    {

                        Decimal CopayCoinsuDeduct = Convert.ToDecimal(dtTransToWrite.Rows[i]["CoInsurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Copay"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Deductible"]);

                        //Calculate InsurancePending
                        Decimal InsurancePending = 0;
                        if (Convert.ToBoolean(dtTransToWrite.Rows[i]["IsInPending"]) == true)
                        {
                            InsurancePending = Convert.ToDecimal(dtTransToWrite.Rows[i]["InPending"]) - (Convert.ToDecimal(dtTransToWrite.Rows[i]["Insurance"]) - Convert.ToDecimal(dtTransToWrite.Rows[i]["CoInsurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Copay"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Deductible"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Adjustment"]));
                        }
                        DateTime dtDOS = Convert.ToDateTime(dtTransToWrite.Rows[i]["DOS"]);

                        //[DOS]	[CPTCode]	[CPT description]   [Charges]	[Insurance]	[CoInsurance + Copay + deductible]	[Adjustments]	[InsPending*]
                        tw.WriteLine("\t" + dtTransToWrite.Rows[i]["DOS"].ToString() + "\t" + dtTransToWrite.Rows[i]["CPTCode"].ToString() + "\t" + dtTransToWrite.Rows[i]["CPTDesc"].ToString() + "\t" + Convert.ToDecimal(dtTransToWrite.Rows[i]["Charges"]).ToString("#0.00") + "\t" + Convert.ToDecimal(dtTransToWrite.Rows[i]["Insurance"]).ToString("#0.00") + "\t" + CopayCoinsuDeduct.ToString("#0.00") + "\t" + Convert.ToDecimal(dtTransToWrite.Rows[i]["Adjustment"]).ToString("#0.00") + "\t" + InsurancePending.ToString("#0.00") + "*");
                        //tw.WriteLine("\t" + dtTransToWrite.Rows[i]["DOS"].ToString() + "\t" + dtTransToWrite.Rows[i]["CPTCode"].ToString() + "\t" + dtTransToWrite.Rows[i]["CPTDesc"].ToString() + "\t" + Convert.ToDecimal(dtTransToWrite.Rows[i]["Charges"]).ToString("#0.00") + "\t" + Convert.ToDecimal(dtTransToWrite.Rows[i]["Insurance"]).ToString("#0.00") + "\t" + Convert.ToDecimal(dtTransToWrite.Rows[i]["CoInsurance"]).ToString("#0.00") + "\t" + Convert.ToDecimal(dtTransToWrite.Rows[i]["Copay"]).ToString("#0.00") + "\t" + Convert.ToDecimal(dtTransToWrite.Rows[i]["Deductible"]).ToString("#0.00") + "\t" + Convert.ToDecimal(dtTransToWrite.Rows[i]["Adjustment"]).ToString("#0.00"));

                        if (dtDOS <= DateTime.Now.Date && dtDOS >= DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)))
                        {
                            _30DaysCharges += Convert.ToDecimal(dtTransToWrite.Rows[i]["Charges"]);
                            _30DayesPaid += Convert.ToDecimal(dtTransToWrite.Rows[i]["Insurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["CoInsurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Copay"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Deductible"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Adjustment"]);
                        }
                        else if (dtDOS < DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)) && dtDOS >= DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)))
                        {
                            _60DaysCharges += Convert.ToDecimal(dtTransToWrite.Rows[i]["Charges"]);
                            _60DayesPaid += Convert.ToDecimal(dtTransToWrite.Rows[i]["Insurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["CoInsurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Copay"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Deductible"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Adjustment"]);
                        }
                        else if (dtDOS < DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)) && dtDOS >= DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)))
                        {
                            _90DaysCharges += Convert.ToDecimal(dtTransToWrite.Rows[i]["Charges"]);
                            _90DayesPaid += Convert.ToDecimal(dtTransToWrite.Rows[i]["Insurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["CoInsurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Copay"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Deductible"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Adjustment"]);
                        }
                        else if (dtDOS < DateTime.Now.Subtract(new TimeSpan(90, 0, 0, 0)) && dtDOS >= DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
                        {
                            _120DaysCharges += Convert.ToDecimal(dtTransToWrite.Rows[i]["Charges"]);
                            _120DayesPaid += Convert.ToDecimal(dtTransToWrite.Rows[i]["Insurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["CoInsurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Copay"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Deductible"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Adjustment"]);
                        }
                        else if (dtDOS < DateTime.Now.Subtract(new TimeSpan(120, 0, 0, 0)))
                        {
                            _Above120DaysCharges += Convert.ToDecimal(dtTransToWrite.Rows[i]["Charges"]);
                            _Above120DayesPaid += Convert.ToDecimal(dtTransToWrite.Rows[i]["Insurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["CoInsurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Copay"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Deductible"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Adjustment"]);
                        }

                        _TotalInsurancePending += InsurancePending;
                        _TotalCharges += Convert.ToDecimal(dtTransToWrite.Rows[i]["Charges"]);
                        _TotalPaid += Convert.ToDecimal(dtTransToWrite.Rows[i]["Insurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["CoInsurance"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Copay"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Deductible"]) + Convert.ToDecimal(dtTransToWrite.Rows[i]["Adjustment"]);
                    }
                    dtTransToWrite.Dispose();
                    //-------------

                    tw.WriteLine("TransactionsLineEnd");
                    //tw.WriteLine("Status\t Current30Days\t" + (_30DaysCharges - _30DayesPaid) + "\t" + (_60DaysCharges - _60DayesPaid) + "\t" + (_90DaysCharges - _90DayesPaid));

                    _TotalBalanceDue = _TotalCharges - (_TotalPaid + _TotalInsurancePending);
                    //_TotalBalanceDue = _TotalCharges - _TotalPaid;

                    _30DaysBalanceDue = _30DaysCharges - _30DayesPaid;
                    _60DaysBalanceDue = _60DaysCharges - _60DayesPaid;
                    _90DaysBalanceDue = _90DaysCharges - _90DayesPaid;
                    _120DaysBalanceDue = _120DaysCharges - _120DayesPaid;
                    _Above120DaysBalanceDue = _Above120DaysCharges - _Above120DayesPaid;


                    tw.WriteLine("TotalBalance\t" + _TotalBalanceDue.ToString("#0.00"));
                    tw.WriteLine("Status\t30Days\t60Days\t90Days\t120Days\tAbove120Days");
                    tw.WriteLine("\t" + _30DaysBalanceDue + "\t" + _60DaysBalanceDue + "\t" + _90DaysBalanceDue + "\t" + _120DaysBalanceDue + "\t" + _Above120DaysBalanceDue);
                    string sDueDate = "";


                    if (rbCriteria.Checked)
                    {
                        if (numCriteriaDuration != null)
                        {
                            Int32 CDuration = Convert.ToInt32(numCriteriaDuration.Value);
                            if (rdbCriteriaDays.Checked)
                            {
                                sDueDate = DateTime.Now.AddDays(CDuration).ToShortDateString();
                            }
                            else if (rdbCriteriaWeek.Checked)
                            {
                                sDueDate = DateTime.Now.AddDays(CDuration * 7).ToShortDateString();
                            }
                            else if (rdbCriteriaMonth.Checked)
                            {
                                sDueDate = DateTime.Now.AddMonths(CDuration).ToShortDateString();
                            }
                        }

                    }
                    else
                    {
                        if (numDuration != null)
                        {
                            Int32 Duration = Convert.ToInt32(numDuration.Value);
                            if (rdbDays.Checked)
                            {
                                sDueDate = DateTime.Now.AddDays(Duration).ToShortDateString();
                            }
                            else if (rdbWeek.Checked)
                            {
                                sDueDate = DateTime.Now.AddDays(Duration * 7).ToShortDateString();
                            }
                            else if (rdbMonth.Checked)
                            {
                                sDueDate = DateTime.Now.AddMonths(Duration).ToShortDateString();
                            }
                        }
                    }


                    tw.WriteLine("DueDate\t" + sDueDate.Trim());
                }

                #endregion


                #region " To Write the Footer Details to a text file"

                if (_dsReports.Tables["dt_GuarantorDetails"].Rows.Count > 0 && _dsReports.Tables["dt_DisplaySettings"].Rows.Count > 0 && _dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows.Count > 0)
                {
                    tw.WriteLine("Message1\t" + _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sClinicMessage1"].ToString());
                    tw.WriteLine("Message2\t" + _dsReports.Tables["dt_DisplaySettings"].Rows[0]["sClinicMessage2"].ToString());

                    tw.WriteLine("GuarantorName\t" + _dsReports.Tables["dt_GuarantorDetails"].Rows[0]["GuarantorFName"].ToString() + "\t" + _dsReports.Tables["dt_GuarantorDetails"].Rows[0]["GuarantorMName"].ToString() + "\t" + _dsReports.Tables["dt_GuarantorDetails"].Rows[0]["GuarantorLName"].ToString());
                    tw.WriteLine("GuarantorAddress\t" + _dsReports.Tables["dt_GuarantorDetails"].Rows[0]["GuarantorAdd1"].ToString() + "\t" + _dsReports.Tables["dt_GuarantorDetails"].Rows[0]["GuarantorAdd2"].ToString() + "\t" + _dsReports.Tables["dt_GuarantorDetails"].Rows[0]["GuarantorCity"].ToString() + "\t" + _dsReports.Tables["dt_GuarantorDetails"].Rows[0]["GuarantorState"].ToString() + "\t" + _dsReports.Tables["dt_GuarantorDetails"].Rows[0]["GuarantorZip"].ToString());
                }

                if (_dsReports.Tables["dt_PatientStatementForGateWayEDI"].Rows.Count > 0)
                {
                    tw.WriteLine("@EndStatement");
                    tw.WriteLine("");
                }
                #endregion



                oDB.Disconnect();
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
                if (oDB != null) { oDB.Dispose(); }
                if (tw != null)
                {
                    tw.Close();
                    tw.Dispose();
                }
                if (_dsReports != null)
                {
                    _dsReports.Dispose();
                }
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                if (oConnection != null && oConnection.State == ConnectionState.Open)
                {
                    oConnection.Close();
                     

                }
                if (oConnection != null)
                {
                    oConnection.Dispose();
                }
            }
        }

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

                oDB.Execute("gsp_InUpPatientSettings", oDBParameters);

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

            
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _strSQL = "";
            string _result = "";
            try
            {
                oDB.Connect(false);
                DataTable dtPatient = null;
                //get the provider details in the datatable -- dtProvider
                _strSQL = "select ISNULL( sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) + ISNULL(sLastName,'') AS PatientName FROM Patient WHERE nPatientID = " + patientID;
                oDB.Retrive_Query(_strSQL, out dtPatient);
                if (dtPatient != null)
                {
                    if (dtPatient.Rows.Count > 0)
                    {
                        _result = Convert.ToString(dtPatient.Rows[0]["PatientName"]);
                    }
                    dtPatient.Dispose();
                    dtPatient = null;
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

                
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return _result;

        }

        //Find Patient According to filter criteria
        private void GetPatientForCriteria()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtPatients;


            try
            {
                string _strsqlFetch = "";

                #region "Commented Queries"

                //_strsqlFetch = "SELECT DISTINCT PatientID,sPatientName FROM " +
                //                "( " +
                //                " SELECT DISTINCT " +
                //                " ISNULL(Patient.nPatientID, 0) AS PatientID, ISNULL(Patient.sFirstName, '') + '' + ISNULL(Patient.sMiddleName, '') + ' ' + ISNULL(Patient.sLastName, '') " +
                //                " AS sPatientName, ISNULL(BL_Transaction_MST.sFacilityCode, '-') AS sFacilityCode, Patient.sZIP, ISNULL(BL_Transaction_MST.nChargesDayTrayID, " +
                //                " 0) AS nChargesDayTrayID, ISNULL(BL_Transaction_Payment_DTL_3.nCloseDayTrayID, 0) AS nPaymentTrayID, " +
                //                " ISNULL(BL_Transaction_Lines.sCPTCode, '-') AS sCPTCode, ISNULL(BL_Transaction_Lines.sCPTDescription, '-') AS sCPTDescription, " +
                //                " ISNULL(BL_Transaction_Lines.dCharges, 0) - (ISNULL(BL_Transaction_Payment_DTL_3.dCurrentPaymentAmt, 0) + ISNULL" +
                //                " ((SELECT SUM(dCurrentPaymentAmt) AS Adjustments " +
                //                " FROM BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_1 " +
                //                " WHERE (BL_Transaction_Lines.nTransactionID = nBillingTransactionID) AND " +
                //                " (BL_Transaction_Lines.nTransactionDetailID = nBillingTransactionDetailID)), 0)) AS Balance,  " +
                //                " isnull(Contacts_MST.nContactID,0) as nContactID " +
                //                " FROM BL_Transaction_MST RIGHT OUTER JOIN " +
                //                " Patient ON BL_Transaction_MST.nPatientID = Patient.nPatientID LEFT OUTER JOIN " +
                //                " BL_Transaction_Lines LEFT OUTER JOIN " +
                //                " BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_3 LEFT OUTER JOIN " +
                //                " Contacts_MST INNER JOIN " +
                //                " PatientInsurance_DTL ON Contacts_MST.nContactID = PatientInsurance_DTL.nContactID ON " +
                //                " BL_Transaction_Payment_DTL_3.nPaymentInsuranceID = PatientInsurance_DTL.nInsuranceID ON " +
                //                " BL_Transaction_Lines.nTransactionID = BL_Transaction_Payment_DTL_3.nBillingTransactionID AND " +
                //                " BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_Payment_DTL_3.nBillingTransactionDetailID AND " +
                //                " BL_Transaction_Lines.nTransactionLineNo = BL_Transaction_Payment_DTL_3.nBillingTransactionLineNo ON " +
                //                " BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID " +
                //                " )Criteria " +
                //                " WHERE PatientID <> 0 ";


                //_strsqlFetch = "SELECT DISTINCT PatientID,sPatientName FROM " +
                //               "( " +
                //               " SELECT DISTINCT " +
                //               " ISNULL(Patient.nPatientID, 0) AS PatientID, ISNULL(Patient.sFirstName, '') + space(1) + ISNULL(Patient.sMiddleName, '') +  space(1) + ISNULL(Patient.sLastName, '') " +
                //               " AS sPatientName, ISNULL(BL_Transaction_MST.sFacilityCode, '-') AS sFacilityCode, Patient.sZIP, ISNULL(BL_Transaction_MST.nChargesDayTrayID, " +
                //               " 0) AS nChargesDayTrayID, ISNULL(BL_Transaction_Payment_DTL_3.nCloseDayTrayID, 0) AS nPaymentTrayID, " +
                //               " ISNULL(BL_Transaction_Lines.sCPTCode, '-') AS sCPTCode, ISNULL(BL_Transaction_Lines.sCPTDescription, '-') AS sCPTDescription, " +
                //               " ISNULL(BL_Transaction_Lines.dCharges, 0) - (ISNULL(BL_Transaction_Payment_DTL_3.dCurrentPaymentAmt, 0))AS Balance,  " +
                //               " isnull(Contacts_MST.nContactID,0) as nContactID,BL_Transaction_MST.nTransactionDate AS nTransactionDate " +
                //               " FROM BL_Transaction_MST RIGHT OUTER JOIN " +
                //               " Patient ON BL_Transaction_MST.nPatientID = Patient.nPatientID LEFT OUTER JOIN " +
                //               " BL_Transaction_Lines LEFT OUTER JOIN " +
                //               " BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_3 LEFT OUTER JOIN " +
                //               " Contacts_MST INNER JOIN " +
                //               " PatientInsurance_DTL ON Contacts_MST.nContactID = PatientInsurance_DTL.nContactID ON " +
                //               " BL_Transaction_Payment_DTL_3.nPaymentInsuranceID = PatientInsurance_DTL.nInsuranceID ON " +
                //               " BL_Transaction_Lines.nTransactionID = BL_Transaction_Payment_DTL_3.nBillingTransactionID AND " +
                //               " BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_Payment_DTL_3.nBillingTransactionDetailID AND " +
                //               " BL_Transaction_Lines.nTransactionLineNo = BL_Transaction_Payment_DTL_3.nBillingTransactionLineNo ON " +
                //               " BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID " +
                //               " )Criteria " +
                //               " WHERE PatientID <> 0 ";


                //   _strsqlFetch = "SELECT DISTINCT PatientID,sPatientCode, sPatientName,sFirstName,sMiddleName,sLastName,dtDOB,sPhone,sMobile,sSSN,sProviderName FROM  (  "
                //+ " SELECT DISTINCT ISNULL(Patient.nPatientID, 0) AS PatientID, ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '')  + SPACE(1) + ISNULL(Patient.sLastName, '') AS sPatientName, "
                //+ " ISNULL(patient.sPatientCode,'') AS sPatientCode,ISNULL(Patient.sFirstName, '') AS sFirstName,ISNULL(Patient.sMiddleName, '') AS sMiddleName, ISNULL(Patient.sLastName, '') AS sLastName,ISNULL(patient.dtDOB,'') AS dtDOB,ISNULL(patient.sPhone,'') AS sPhone,ISNULL(patient.sMobile,'') As sMobile ,ISNULL(patient.nSSN,'') As sSSN , "
                //+ " ISNULL(Provider_MST.sFirstName,'') + SPACE(1) + ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) + ISNULL(Provider_MST.sLastName,'') AS sProviderName, "
                //+ " isnull(BL_Transaction_MST.nTransactionID,0) as nTransactionID, isnull(BL_Transaction_MST.nTransactionDate,0) as nTransactionDate, "
                //+ " ISNULL(BL_Transaction_Lines.sCPTCode, '-') AS sCPTCode,  ISNULL(BL_Transaction_Lines.sCPTDescription, '-') AS sCPTDescription,ISNULL(BL_Transaction_MST.sFacilityCode, '-') AS sFacilityCode,  Patient.sZIP, ISNULL(Contacts_MST.nContactID, 0) AS nContactID, "
                //+ " ISNULL(BL_Transaction_MST.nChargesDayTrayID, 0) AS nChargesDayTrayID,  ISNULL(BL_Transaction_Payment_DTL_3.nCloseDayTrayID,0) AS nPaymentTrayID,   "
                //+ " ISNULL(   "
                //+ " 		(SELECT SUM(ISNULL(BL_Transaction_Payment_DTL.dCurrentPaymentAmt,0)) "
                //+ " 		 FROM BL_Transaction_Payment_DTL   "
                //+ " 		 where BL_Transaction_Payment_DTL.nBillingTransactionID = BL_Transaction_MST.nTransactionID  "
                //+ " 				and BL_Transaction_Payment_DTL.nBillingTransactionDetailID = BL_Transaction_Lines.nTransactiondetailID   "
                //+ " 		) ,0)  "
                //+ " AS NetPayments,  "
                //+ " 	((	ISNULL(BL_Transaction_Lines.dCharges, 0)  "
                //+ " 		*  "
                //+ " 		ISNULL(BL_Transaction_Lines.dUnit, 1) "
                //+ " 	)) "
                //+ " as dCharges,  "
                //+ " ISNULL(((dcharges * dUnit) - isnull(   "
                //+ " 							(SELECT SUM(ISNULL(BL_Transaction_Payment_DTL.dCurrentPaymentAmt,0))   "
                //+ " 							 FROM BL_Transaction_Payment_DTL   "
                //+ " 							 where BL_Transaction_Payment_DTL.nBillingTransactionID = BL_Transaction_MST.nTransactionID   "
                //+ " 									and BL_Transaction_Payment_DTL.nBillingTransactionDetailID = BL_Transaction_Lines.nTransactiondetailID   "
                //+ " 							 ) ,0)  ),0)  "
                //+ " as Balance   "
                //+ "  "
                //+ " FROM BL_Transaction_MST RIGHT OUTER JOIN Patient ON BL_Transaction_MST.nPatientID = Patient.nPatientID  LEFT OUTER JOIN BL_Transaction_Lines LEFT OUTER JOIN BL_Transaction_Payment_DTL AS BL_Transaction_Payment_DTL_3  "
                //+ " LEFT OUTER JOIN Contacts_MST INNER JOIN PatientInsurance_DTL ON Contacts_MST.nContactID = PatientInsurance_DTL.nContactID ON  BL_Transaction_Payment_DTL_3.nPaymentInsuranceID = PatientInsurance_DTL.nInsuranceID "
                //+ " ON  BL_Transaction_Lines.nTransactionID = BL_Transaction_Payment_DTL_3.nBillingTransactionID AND  BL_Transaction_Lines.nTransactionDetailID = BL_Transaction_Payment_DTL_3.nBillingTransactionDetailID AND  BL_Transaction_Lines.nTransactionLineNo = BL_Transaction_Payment_DTL_3.nBillingTransactionLineNo "
                //+ " ON  BL_Transaction_MST.nTransactionID = BL_Transaction_Lines.nTransactionID  "
                //+ " GROUP BY Patient.nPatientID,Patient.sPatientCode,Patient.sFirstName,Patient.sMiddleName, Patient.sLastName,Patient.dtDOB,Patient.sPhone,Patient.sMobile,BL_Transaction_MST.nTransactionID,BL_Transaction_Lines.nTransactiondetailID,BL_Transaction_MST.nTransactionDate, BL_Transaction_Lines.dCharges,BL_Transaction_Lines.dUnit,  BL_Transaction_MST.sFacilityCode, Patient.sZIP,  BL_Transaction_MST.nChargesDayTrayID, BL_Transaction_Payment_DTL_3.nCloseDayTrayID,  BL_Transaction_Lines.sCPTCode, BL_Transaction_Lines.sCPTDescription,Contacts_MST.nContactID   "
                //+ "  "
                //+ " )  AS Criteria  "
                //+ " WHERE  PatientID NOT IN (select nPatientID from PatientSettings where svalue='1' and sName='Exclude from Statement') ";


                #endregion

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

                string sCPT = "";
                if (cmbCPT != null)
                {
                    for (int i = 0; i < cmbCPT.Items.Count; i++)
                    {
                        cmbCPT.SelectedIndex = i;
                        cmbCPT.Refresh();

                        if (i == cmbCPT.Items.Count - 1)
                        {
                            sCPT = sCPT + "'" + Convert.ToString(cmbCPT.Text) + "'";
                        }
                        else
                        {
                            sCPT = sCPT + "'" + Convert.ToString(cmbCPT.Text) + "',";
                        }
                    }

                    if (sCPT != "")
                    {
                        _strsqlFetch = _strsqlFetch + "AND sCPTCode IN(" + sCPT + ")";
                    }
                }

                #endregion

                #region "Charges Tray"

                if (cmbChargesTray != null)
                {
                    if (cmbChargesTray.SelectedValue != null && cmbChargesTray.SelectedValue.ToString() != "0")
                    {
                        _strsqlFetch = _strsqlFetch + " AND nChargesDayTrayID=" + Convert.ToInt64(cmbChargesTray.SelectedValue) + "";
                    }
                }

                #endregion

                #region "Payment Tray"

                if (cmbPaymentTray != null)
                {
                    if (cmbPaymentTray.SelectedValue != null && cmbPaymentTray.SelectedValue.ToString() != "0")
                    {
                        _strsqlFetch = _strsqlFetch + " AND nPaymentTrayID=" + Convert.ToInt64(cmbPaymentTray.SelectedValue) + "";
                    }
                }

                #endregion

                #region "Facility Code"

                if (cmbFacility != null)
                {
                    if (cmbFacility.SelectedValue != null && cmbFacility.SelectedValue.ToString() != "0")
                    {
                        _strsqlFetch = _strsqlFetch + " AND sFacilityCode ='" + cmbFacility.SelectedValue.ToString() + "'";
                    }
                }

                #endregion

                #region "Insurance "

                string sInsurance = "";
                if (cmbInsurance != null)
                {
                    for (int i = 0; i < cmbInsurance.Items.Count; i++)
                    {
                        cmbInsurance.SelectedIndex = i;
                        cmbInsurance.Refresh();

                        if (i == cmbInsurance.Items.Count - 1)
                        {
                            sInsurance = sInsurance + Convert.ToString(cmbInsurance.SelectedValue);
                        }
                        else
                        {
                            sInsurance = sInsurance + Convert.ToString(cmbInsurance.SelectedValue) + ",";
                        }
                    }

                    if (sInsurance != "")
                    {
                        _strsqlFetch = _strsqlFetch + " AND Criteria.nContactID IN(" + sInsurance + ")";
                    }
                }


                #endregion

                #region "Zip Code "

                if (txtZip != null)
                {
                    if (txtZip.Text.ToString() != "")
                    {
                        _strsqlFetch = _strsqlFetch + " AND szip ='" + txtZip.Text.ToString() + "'";
                    }
                }
                #endregion

                #region "Transaction Date"

                if (dtCriteriaStartDate.Checked == true)
                {
                    _strsqlFetch = _strsqlFetch + " AND (nTransactionDate >= " + gloDateMaster.gloDate.DateAsNumber(dtCriteriaStartDate.Value.ToShortDateString()) + " AND nTransactionDate <= " + gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString()) + ") ";
                }

                #endregion

                #region "Due Amount"

                Decimal DueAmt = 0;
                if (txtDueAmt.ToString().Trim() != "")
                {
                    DueAmt = Convert.ToDecimal(txtDueAmt.Text);

                    if (rbGreater.Checked == true)
                    {
                        _strsqlFetch = _strsqlFetch + " group by PatientID,sPatientCode, sPatientName,sFirstName,sMiddleName,sLastName,dtDOB,sPhone,sMobile,sSSN,sProviderName  HAVING   SUM(Criteria.Balance) > " + DueAmt + "";
                    }
                    else
                    {
                        _strsqlFetch = _strsqlFetch + " group by PatientID,sPatientCode, sPatientName,sFirstName,sMiddleName,sLastName,dtDOB,sPhone,sMobile,sSSN,sProviderName  HAVING   SUM(Criteria.Balance) < " + DueAmt + "";
                    }
                }

                #endregion

                oDB.Connect(false);
                oDB.Retrive_Query(_strsqlFetch, out  _dtPatients);

                //if (_dtPatients != null && _dtPatients.Rows.Count > 0)
                //{
                //    cmbPatients.DataSource = _dtPatients;
                //    cmbPatients.DisplayMember = "sPatientName";
                //    cmbPatients.ValueMember = "PatientID";
                //}
                //if(_patientId != 0)
                //{
                //    cmbPatients.SelectedValue = _patientId;
                //}
                //_dtPatients = null;


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
                 //   c1PatientList.Clear();
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
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
                    DataView _dv = ((DataView)c1PatientList.DataSource);
                    if (_dv != null)
                    {
                        //Display Patients Name Start with From [A] To [Z]
                        char[] From = cmbNameFrom.Text.ToCharArray();
                        char[] To = cmbNameTo.Text.ToCharArray();
                        string sColumnName = "sLastName";
                        string sFilter = "";

                        if (rbFirstName.Checked)
                        {
                            sColumnName = "sFirstName";
                        }

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

                        _dv.RowFilter = sFilter;
                        c1PatientList.DataSource = _dv;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #endregion

        #region "User Control Events"

        private void removeOListControl()
        {
            if (oListControl != null)
            {
                if (this.Controls.Contains(oListControl))
                {
                    this.Controls.Remove(oListControl);
                }
                oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.Dispose();
                oListControl = null;
            }
        }
        private void btnBrowsePatient_Click(object sender, EventArgs e)
        {
            try
            {
                removeOListControl();
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}


                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, true, this.Width);


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
         //   cmbPatients.Items.Clear();
            cmbPatients.DataSource = null;
            cmbPatients.Items.Clear();
            cmbPatients.Refresh();
        }

        private void btnBrowseCPT_Click(object sender, EventArgs e)
        {
            try
            {
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.CPT, true, this.Width);

                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " CPT";
                _CurrentControlType = gloListControl.gloListControlType.CPT;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbCPT.DataSource != null)
                {
                    for (int i = 0; i < cmbCPT.Items.Count; i++)
                    {
                        cmbCPT.SelectedIndex = i;
                        cmbCPT.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbCPT.SelectedValue), cmbCPT.Text, "");
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

        private void btnClearCPT_Click(object sender, EventArgs e)
        {
            
            cmbCPT.DataSource = null;
            cmbCPT.Items.Clear();
            cmbCPT.Refresh();
        }

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {

            if (cmbInsurance.Items.Count > 0)
            {

            }
         
            cmbInsurance.DataSource = null;
            cmbInsurance.Items.Clear();
            cmbInsurance.Refresh();


        }

        private void btnBrowseInsurance_Click(object sender, EventArgs e)
        {
            try
            {
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Insurance, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Insurances";
                _CurrentControlType = gloListControl.gloListControlType.AllPatientInsurances;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);
                if (cmbInsurance.DataSource != null)
                {
                    for (int i = 0; i < cmbInsurance.Items.Count; i++)
                    {
                        cmbInsurance.SelectedIndex = i;
                        cmbInsurance.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsurance.SelectedValue), cmbInsurance.Text);
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
                        cmbPatients.Items.Clear();
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

                case gloListControl.gloListControlType.AllPatientInsurances:
                    {
                       
                        cmbInsurance.DataSource = null;
                        cmbInsurance.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");
                            ogloItems = new gloGeneralItem.gloItems();
                            gloGeneralItem.gloItem ogloItem = null;
                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                                ogloItem = new gloGeneralItem.gloItem();
                                ogloItem.ID = oListControl.SelectedItems[_Counter].ID;
                                ogloItem.Description = oListControl.SelectedItems[_Counter].Description;
                                ogloItems.Add(ogloItem);
                                ogloItem.Dispose();
                                ogloItem = null;
                            }

                            cmbInsurance.DataSource = oBindTable;
                            cmbInsurance.DisplayMember = "DispName";
                            cmbInsurance.ValueMember = "ID";
                            ogloItems.Clear();
                            ogloItems.Dispose();
                            ogloItems = null;
                        }


                    }
                    break;

                case gloListControl.gloListControlType.CPT:
                    {
                      
                        cmbCPT.DataSource = null;
                        cmbCPT.Items.Clear();
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
                                oRow[1] = oListControl.SelectedItems[_Counter].Code;
                                oBindTable.Rows.Add(oRow);
                            }

                            cmbCPT.DataSource = oBindTable;
                            cmbCPT.DisplayMember = "DispName";
                            cmbCPT.ValueMember = "ID";
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
            //if (oListControl != null)
            //{
            //    for (int i = this.Controls.Count - 1; i >= 0; i--)
            //    {
            //        if (this.Controls[i].Name == oListControl.Name)
            //        {
            //            this.Controls.Remove(this.Controls[i]);
            //            break;
            //        }
            //    }
            //}
            removeOListControl();
        }

        private void cmbSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSettings.SelectedValue != null)
            {
                txtDueAmt.Text = "";
              
                cmbInsurance.DataSource = null;
                cmbInsurance.Items.Clear();
               
                cmbCPT.DataSource = null;
                cmbCPT.Items.Clear();
                txtZip.Text = "";

                if (cmbSettings.SelectedValue != null)
                {
                    if (cmbSettings.SelectedValue.ToString() != "0")
                    {
                        FillControlsPerCriteria(Convert.ToInt64(cmbSettings.SelectedValue));
                    }
                }
                //if (rbCriteria.Checked)
                //{
                //    GetPatientForCriteria();
                //}

                GetPatientForCriteria();

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
                ofrmSetupPatientStatementCriteria.ShowDialog(this);
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
                gloBilling.frmSetupPatientStatementCriteria ofrmSetupPatientStatementCriteria = new gloBilling.frmSetupPatientStatementCriteria(0, _databaseconnectionstring);
                ofrmSetupPatientStatementCriteria.StartPosition = FormStartPosition.CenterScreen;
                ofrmSetupPatientStatementCriteria.ShowDialog(this);
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

        private void txtZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!(e.KeyChar == Convert.ToChar(8)))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9a-zA-Z]*$") == false)
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
       
        #endregion

        #region "Filter Criteria"

      

        private void FetchCriteriasCombo()
        {
            gloBilling.Statement.PatinetStatementCriteria oPatinetStatementCriteria = new gloBilling.Statement.PatinetStatementCriteria(_databaseconnectionstring);

            #region "Fill Filter Combo box"

            try
            {
                DataTable _dtFilterCriterias = null;
             
                _dtFilterCriterias = oPatinetStatementCriteria.GetPatinetStatementCriterias();
                this.cmbSettings.SelectedIndexChanged -= new System.EventHandler(this.cmbSettings_SelectedIndexChanged);
                if (_dtFilterCriterias != null)
                {
                    DataTable dtFilterCriterias = new DataTable();
                    dtFilterCriterias.Columns.Add("nStatementCriteriaID");
                    dtFilterCriterias.Columns.Add("sStatementCriteriaName");

                    dtFilterCriterias.Clear();
                    dtFilterCriterias.Rows.Add(0, "");

                    for (int i = 0; i < _dtFilterCriterias.Rows.Count; i++)
                    {
                        dtFilterCriterias.Rows.Add(_dtFilterCriterias.Rows[i]["nStatementCriteriaID"], _dtFilterCriterias.Rows[i]["sStatementCriteriaName"]);
                    }

                    cmbSettings.DataSource = dtFilterCriterias;
                    cmbSettings.DisplayMember = "sStatementCriteriaName";
                    cmbSettings.ValueMember = "nStatementCriteriaID";
                    _dtFilterCriterias.Dispose();
                    _dtFilterCriterias = null;
                }
                this.cmbSettings.SelectedIndexChanged += new System.EventHandler(this.cmbSettings_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                oPatinetStatementCriteria.Dispose();
                oPatinetStatementCriteria = null;
            }
            #endregion
            
        }

        private void FillControlsPerCriteria(Int64 CriteriaID)
        {

            gloBilling.Statement.PatinetStatementCriteria oPatinetStatementCriteria = new gloBilling.Statement.PatinetStatementCriteria(_databaseconnectionstring);

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
                                case "Due Amount Grater Then":
                                    if (Convert.ToInt32(dr[1]) == 1)
                                    {
                                        this.rbGreater.CheckedChanged -= new System.EventHandler(this.rbLess_CheckedChanged);
                                        rbGreater.Checked = true;
                                        this.rbGreater.CheckedChanged += new System.EventHandler(this.rbLess_CheckedChanged);
                                        txtDueAmt.Text = Convert.ToString(dr[2]);
                                    }
                                    break;
                                case "Due Amount Less Then":
                                    if (Convert.ToInt32(dr[1]) == 1)
                                    {
                                        this.rbLess.CheckedChanged -= new System.EventHandler(this.rbLess_CheckedChanged);
                                        rbLess.Checked = true;
                                        this.rbLess.CheckedChanged += new System.EventHandler(this.rbLess_CheckedChanged);
                                        txtDueAmt.Text = Convert.ToString(dr[2]);
                                    }
                                    break;

                                case "CPT":
                                    if (Convert.ToString(dr[2]).Trim() != "")
                                    {
                                        oRow = oBindTableCPT.NewRow();
                                        oRow[0] = Convert.ToString(dr[2]).Trim();
                                        oRow[1] = Convert.ToString(dr[3]).Trim();
                                        oBindTableCPT.Rows.Add(oRow);
                                    }
                                    break;
                                case "Insurance":
                                    if (Convert.ToString(dr[1]).Trim() != "")
                                    {
                                        oRow = oBindTableInsurance.NewRow();
                                        oRow[0] = Convert.ToString(dr[1]).Trim();
                                        oRow[1] = Convert.ToString(dr[3]).Trim();
                                        oBindTableInsurance.Rows.Add(oRow);
                                    }
                                    break;
                                case "Charge Tray":
                                    if (Convert.ToString(dr[1]).Trim() != "")
                                    {
                                        cmbChargesTray.SelectedIndex = cmbChargesTray.FindStringExact(Convert.ToString(dr[3]));
                                    }
                                    break;
                                case "Payment Tray":
                                    if (Convert.ToString(dr[1]).Trim() != "")
                                    {
                                        cmbPaymentTray.SelectedIndex = cmbPaymentTray.FindStringExact(Convert.ToString(dr[3]));
                                    }
                                    break;
                                case "Facility":
                                    if (Convert.ToString(dr[1]).Trim() != "")
                                    {
                                        cmbFacility.SelectedIndex = cmbFacility.FindStringExact(Convert.ToString(dr[3]));
                                    }
                                    break;
                                case "Zip Code":
                                    if (Convert.ToString(dr[2]).Trim() != "")
                                    {
                                        txtZip.Text = Convert.ToString(dr[2]).Trim();
                                    }
                                    break;
                            }

                        }
                        if (oBindTableInsurance != null)
                        {
                            if (oBindTableInsurance.Rows.Count > 0)
                            {
                                cmbInsurance.DataSource = oBindTableInsurance;
                                cmbInsurance.DisplayMember = "DispName";
                                cmbInsurance.ValueMember = "ID";
                            }
                        }
                        if (oBindTableCPT != null)
                        {
                            if (oBindTableCPT.Rows.Count > 0)
                            {
                                cmbCPT.DataSource = oBindTableCPT;
                                cmbCPT.DisplayMember = "DispName";
                                cmbCPT.ValueMember = "ID";
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                oPatinetStatementCriteria.Dispose();
                oPatinetStatementCriteria = null;
            }
        }

        private void rbNoCriteria_CheckedChanged(object sender, EventArgs e)
        {
            //pnlSelectSet.Enabled = false;
            //grpFilterCriteria.Enabled = false;
            //pnlNoFiliterPatient.Enabled = true;
            //pnlFilteredPatList.Enabled = false;
        }

        private void rbCriteria_CheckedChanged(object sender, EventArgs e)
        {
            //pnlSelectSet.Enabled = true;
            //grpFilterCriteria.Enabled = true;
            //pnlNoFiliterPatient.Enabled = false;
            //pnlFilteredPatList.Enabled = true;
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
                //if ((oWordApp != null))
                //{
                //   // Marshal.FinalReleaseComObject(oWordApp);
                //    oWordApp = null;
                //}
              
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }


        }

        private bool SavePatientTemplate(string sFileName, string sFilePath,Int64 nPatientID)
        {
            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);
  
            try
            {
               ogloTemplate.ClinicID = _ClinicID;

                ogloTemplate.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Today.ToShortDateString());
                ogloTemplate.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Today.ToShortDateString());

                ogloTemplate.TemplateID = 0;
                ogloTemplate.PatientID = nPatientID;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                DataTable dtTemplateID = null;
                oDB.Connect(false);
                string strSQL = "";
                strSQL = "SELECT nCategoryID FROM Category_MST WHERE sDescription = 'MIS Reports' AND sCategoryType='Template'";
                oDB.Retrive_Query(strSQL, out dtTemplateID);
                oDB.Disconnect();
                oDB.Dispose();
                if( dtTemplateID != null )
                {
                    ogloTemplate.CategoryID = Convert.ToInt64(dtTemplateID.Rows[0]["nCategoryID"]);
                    dtTemplateID.Dispose();
                    dtTemplateID=null;
                }
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
            finally
            {
                ogloTemplate.Dispose();
                ogloTemplate = null;
            }
        }

        private void SavePatientStatementTemplate(Int64 nPatientID)
        {
            try
            {
                string _FileName = "PatientStatement_" + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + "_To_" + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + "_" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + System.DateTime.Now.Millisecond + ".doc";                
                string _FilePath = gloSettings.FolderSettings.AppTempFolderPath + "MIStemp";
                if (Directory.Exists(_FilePath) == false)
                {
                    Directory.CreateDirectory(_FilePath);
                }
                _FilePath = _FilePath + "\\" + _FileName;

                //To fill the Reports 
                FillPatientStatement(nPatientID);

                if (objrptPatientStatementForGateWayEDI != null && objrptPatientStatementForGateWayEDI.IsLoaded == true)
                {
                    #region "Exporting to Doc"

                    objrptPatientStatementForGateWayEDI.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, _FilePath);

                    //Rpt_PatientStatementForGateWayEDI oTempReport = null;

                    //oTempReport = crvReportViewer.ReportSource as Rpt_PatientStatementForGateWayEDI;
                    //if (oTempReport != null)
                    //{
                    //    oTempReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, _FilePath);

                        wdTemplate = new AxDSOFramer.AxFramerControl();
                        wdTemplate.OnDocumentClosed += wdTemplate_OnDocumentClosed;
                        this.Controls.Add(wdTemplate);
                      //  wdTemplate.Open(_FilePath);
                        object thisObject = (object)_FilePath;
                        Wd.Application oWordApp = null;
                        gloWord.LoadAndCloseWord.OpenDSO(ref wdTemplate, ref thisObject, ref oCurDoc, ref oWordApp);
                        _FilePath = (string)thisObject;
                       // oCurDoc = new Microsoft.Office.Interop.Word.Document();
                        oCurDoc = (Microsoft.Office.Interop.Word.Document)wdTemplate.ActiveDocument;

                        //oCurDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                        String sFileName = gloOffice.Supporting.NewDocumentName();

                        object oFileName = (object)sFileName;
                        object missing = System.Reflection.Missing.Value;
                        object oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                        oCurDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                        wdTemplate.Close();
                        wdTemplate.OnDocumentClosed -= wdTemplate_OnDocumentClosed;
                        this.Controls.Remove(wdTemplate);
                        wdTemplate.Dispose();
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

        // For Filling the Dates Combo
        private void Fill_FilterDatesCombo()
        {
            try
            {
                cmb_datefilter.Items.Clear();
                cmb_datefilter.Items.Add("Custom");
                cmb_datefilter.Items.Add("Today");
                cmb_datefilter.Items.Add("Tomorrow");
                cmb_datefilter.Items.Add("Yesterday");
                cmb_datefilter.Items.Add("This Week");
                cmb_datefilter.Items.Add("Last Week");
                cmb_datefilter.Items.Add("Current Month");
                cmb_datefilter.Items.Add("Last Month");
                cmb_datefilter.Items.Add("Current Year");
                cmb_datefilter.Items.Add("Last 30 Days");
                cmb_datefilter.Items.Add("Last 60 Days");
                cmb_datefilter.Items.Add("Last 90 Days");
                cmb_datefilter.Items.Add("Last 120 Days");
                cmb_datefilter.Refresh();

                cmbCriteriaTransactionDate.Items.Clear();
                cmbCriteriaTransactionDate.Items.Add("Custom");
                cmbCriteriaTransactionDate.Items.Add("Today");
                cmbCriteriaTransactionDate.Items.Add("Tomorrow");
                cmbCriteriaTransactionDate.Items.Add("Yesterday");
                cmbCriteriaTransactionDate.Items.Add("This Week");
                cmbCriteriaTransactionDate.Items.Add("Last Week");
                cmbCriteriaTransactionDate.Items.Add("Current Month");
                cmbCriteriaTransactionDate.Items.Add("Last Month");
                cmbCriteriaTransactionDate.Items.Add("Current Year");
                cmbCriteriaTransactionDate.Items.Add("Last 30 Days");
                cmbCriteriaTransactionDate.Items.Add("Last 60 Days");
                cmbCriteriaTransactionDate.Items.Add("Last 90 Days");
                cmbCriteriaTransactionDate.Items.Add("Last 120 Days");
                cmbCriteriaTransactionDate.Refresh();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

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
            dtCriteriaStartDate.Value = dtpStartDate.Value;
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            dtCriteriaEndDate.Value = dtpEndDate.Value;
        }


        #endregion

        #region "C1 Flex Grid Events"

        //Show Patient Statement Report
        private void c1PatientList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                
                Int64 nPatientID = 0;
                btnUp_Click(null, null);

                if (c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index) != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString() != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString().Trim() != "")
                {
                    nPatientID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString());

                }

                if (nPatientID > 0)
                {
                    FillPatientStatement(nPatientID);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        #endregion

        #region "Change Events"

        private void txtDueAmt_TextChanged(object sender, EventArgs e)
        {
            GetPatientForCriteria();
        }

        private void txtZip_TextChanged(object sender, EventArgs e)
        {
            GetPatientForCriteria();
        }

        private void cmbChargesTray_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPatientForCriteria();
        }

        private void cmbPaymentTray_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPatientForCriteria();

        }

        private void cmbFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPatientForCriteria();
        }

        private void cmbNameFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterPatientByName();
        }

        private void dtCriteriaStartDate_EnabledChanged(object sender, EventArgs e)
        {
            GetPatientForCriteria();
        }

        private void rbLastName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLastName.Checked == true)
            {
                rbLastName.Font = boldFont;
                FilterPatientByName(); 
            }
            else
            {
                rbLastName.Font = regularFont;
            }
        }

        private void rbFirstName_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFirstName.Checked == true)
            {
                rbFirstName.Font = boldFont;
                FilterPatientByName(); 
            }
            else
            {
                rbFirstName.Font = regularFont;
            }
        }


        private void rbGreater_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGreater.Checked)
            {
                rbGreater.Font = boldFont;
            }
            else
            {
                rbGreater.Font = regularFont;
            }

            GetPatientForCriteria();

        }

        private void rbLess_CheckedChanged(object sender, EventArgs e)
        {

            if (rbLess.Checked)
            {
                rbLess.Font = boldFont;
            }
            else
            {
                rbLess.Font = regularFont;
            }

            GetPatientForCriteria();

        }

        private void dtCriteriaStartDate_ValueChanged(object sender, EventArgs e)
        {
            GetPatientForCriteria();
        }

        private void dtCriteriaEndDate_ValueChanged(object sender, EventArgs e)
        {
            GetPatientForCriteria();
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
            tsb_btnShowList.Enabled = false;
            pnlFilteredPatList.Visible = true;
            btnDown.Visible = false;
            btnUp.Visible = true;
        }

        #endregion

        #region "Due Date Check Events"

        private void rdbDays_CheckedChanged(object sender, EventArgs e)
        {

            if (rdbDays.Checked == true)
            {
                rdbDays.Font = boldFont;
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

                rdbDays.Font = regularFont;
            }
        }

        private void rdbWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbWeek.Checked == true)
            {
                rdbWeek.Font = boldFont;

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

                rdbWeek.Font = regularFont;
            }
        }

        private void rdbMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMonth.Checked == true)
            {
                rdbMonth.Font = boldFont;

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

                rdbMonth.Font = regularFont;
            }
        }

        private void rdbYear_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbYear.Checked == true)
            {
                rdbYear.Font = boldFont;
                numDuration.Minimum = 1;
                numDuration.Maximum = 25;
            }
            else
            {

                rdbYear.Font = regularFont;
            }
        }

        private void rdbCriteriaDays_CheckedChanged(object sender, EventArgs e)
        {

            if (rdbCriteriaDays.Checked == true)
            {
                rdbCriteriaDays.Font = boldFont;
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

                rdbCriteriaDays.Font = regularFont;
            }
        }

        private void rdbCriteriaWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCriteriaWeek.Checked == true)
            {
                rdbCriteriaWeek.Font = boldFont;

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

                rdbCriteriaWeek.Font = regularFont;
            }
        }

        private void rdbCriteriaMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCriteriaMonth.Checked == true)
            {
                rdbCriteriaMonth.Font = boldFont;

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

                rdbCriteriaMonth.Font = regularFont;
            }
        }

        private void rdbCriteriaYear_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCriteriaYear.Checked == true)
            {
                rdbCriteriaYear.Font = boldFont;
                numCriteriaDuration.Minimum = 1;
                numCriteriaDuration.Maximum = 25;
            }
            else
            {

                rdbCriteriaYear.Font = regularFont;
            }
        }

        private void grpFilterCriteria_Enter(object sender, EventArgs e)
        {

        }


        #endregion

        private void frmRpt_PatientStatementForGateWayEDI_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //boldFont.Dispose();
                //regularFont.Dispose();
            }
            catch
            {
            }
        }

      
     
       

       
    
    }
}
