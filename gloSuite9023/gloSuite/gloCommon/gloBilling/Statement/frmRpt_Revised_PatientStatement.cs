using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Wd = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.IO;
using C1.Win.C1FlexGrid;
using gloPatient;
using gloBilling.Collections;
using gloGlobal;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
using gloPatientPortalCommon;
//using CrystalDecisions.Shared;
//using System.Drawing.Printing;
//using System.Text.RegularExpressions;

namespace gloBilling.Statement
{
    public partial class frmRpt_Revised_PatientStatement : Form, IMessageFilter
    {

        #region " Declarations "

        bool _isBusinessCenterEnable = false;
        public bool IsBusinessCenterEnable
        {
            get { return _isBusinessCenterEnable; }
            set { _isBusinessCenterEnable = value; }
        }

        bool _isPatientAccountEnable = false;
        public bool IsPatientAccountEnable
        {
            get { return _isPatientAccountEnable; }
            set { _isPatientAccountEnable = value; }
        }

        Int64 _selectedPatientID = 0;
        private Int64 SelectedPatientID
        {
            get { return _selectedPatientID; }
            set { _selectedPatientID = value; }
        }

        Int64 _selectedAccountID = 0;
        private Int64 SelectedAccountID
        {
            get { return _selectedAccountID; }
            set { _selectedAccountID = value; }
        }

        string _selectedStatementDate;
        private string SelectedStatementDate
        {
            get { return _selectedStatementDate; }
            set { _selectedStatementDate = value; }
        }

        DateTime _StatementCreateDate;
        private DateTime StatementCreateDate
        {
            get { return _StatementCreateDate; }
            set { _StatementCreateDate = value; }
        }

        public enum SelectedView
        {
            InvidualView,
            BatchView
        }

        private SelectedView _selectedView = SelectedView.BatchView;

        private SelectedView CurrentView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                if (_selectedView == SelectedView.InvidualView)
                {
                    _isIndvidualView = true;
                    SetButtonVisibility("Individual");
                }
                else
                {
                    _isIndvidualView = false;
                    SetButtonVisibility("Batch");
                }
            }
        }

        public bool _isIndvidualView = false;
        private bool IsIndvidualView
        {
            get { return _isIndvidualView; }
            set { value = _isIndvidualView; }
        }

        public Boolean IsCalledFromPatAcct { get; set; }
        public Boolean IsCalledFromInsPmt { get; set; }

        Rpt_Paper_PatientStatement objrptPatientStatementForGateWayEDI = new Rpt_Paper_PatientStatement();
        Rpt_Paper_PatientStatementGW objrptPatientStatementForGateWayEDI_GW = new Rpt_Paper_PatientStatementGW();

        dsRevisedPatientStatement dsPatientStatementMain = null;

        private string LastCloseDate = "";

        bool _isGWPatientStatement = false;
        private bool IsGWPatientStatement
        {
            get { return _isGWPatientStatement; }
            set { _isGWPatientStatement = value; }
        }

        // TODO : Need to check the impact
        public bool IsAllAccPatSelected { get; set; }
        private bool _cmbPatientLoadFlag = true;
        private bool _cmbAccountLoadFlag = true;


        // Singletone : GetInstance
        private static frmRpt_Revised_PatientStatement frm;

        //[DllImport("user32.dll")]
        //private static extern IntPtr GetActiveWindow();
        //[DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        //public static extern IntPtr GetParent(IntPtr hWnd);

        // can be deleted
        //DataTable dtTemp;
        //gloDatabaseLayer.DBLayer oDB;
        //string _sqlQuery = string.Empty;
        //Int64 nStatementCriteriaID = 0;
        //private bool CloseDateNotFound = false;
        //public bool _ExcludedShown = false;
        ////DataView _dv = new DataView();
        ////DateTime _UnclosedDateTime = new DateTime();

        //For Creating the Object of the Report


        gloStatment objClsgloStatment = new gloStatment();
        private string _databaseconnectionstring = "";


        public bool _isGenerateBatch = false;
        public bool _generateBatchFlag = false;
        
        public bool _IsExcluded = false;

        int _x = 0;
        int _y = 0;

        Rectangle Oldpostion = Rectangle.Empty;
      //  private bool _IsMouseEnable = false;

        private ComboBox combo;
        ToolTip tooltip_Rpt = new ToolTip();
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private bool _isPatientFinancialView = false;
        //private bool _isPatientBatchPrint = false;
        private bool IsUnclosedDay = false;
        private bool _IsCancel = false;

   //     private AxDSOFramer.AxFramerControl wdTemplate;

       // private Wd.Document oCurDoc;
       // private Wd.Document oTempDoc;
       // private Wd.Application oWordApp;

        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

        public delegate void onFromClose(object seneder, EventArgs e);
        public event onFromClose on_FromClose;

        gloAccount objgloAccount = null;

     //   Int32 nDelay = 0;
        ToolTip toolTip1_Rpt = new ToolTip();
        DataTable _dtStPatient = null;

        public bool IsStatementNotificationOn { get; set; }
        #endregion " Declarations "

        #region "Constructors"

        public frmRpt_Revised_PatientStatement(string databaseconnectionstring, Int64 nPatientID)
        {
            InitializeComponent();

            SelectedPatientID = nPatientID;
            _databaseconnectionstring = databaseconnectionstring;
            _MessageBoxCaption = gloPMGlobal.MessageBoxCaption;

            cmbBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbAccounts.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAccounts.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbPatients.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPatients.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbSettings.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSettings.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
        }
        public frmRpt_Revised_PatientStatement(string databaseconnectionstring, Int64 nPatientID, bool isPatientFinancialView)
        {
            InitializeComponent();
            SelectedPatientID = nPatientID;
            _databaseconnectionstring = databaseconnectionstring;
            _isPatientFinancialView = isPatientFinancialView;
            _MessageBoxCaption = gloPMGlobal.MessageBoxCaption;

            cmbBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbAccounts.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAccounts.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbPatients.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPatients.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbSettings.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSettings.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
        }

        public frmRpt_Revised_PatientStatement(string databaseconnectionstring, Int64 nPatientID, Int64 nPAccountID, bool isPatientFinancialView)
        {
            InitializeComponent();

            SelectedPatientID = nPatientID;
            SelectedAccountID = nPAccountID;
            _databaseconnectionstring = databaseconnectionstring;
            _isPatientFinancialView = isPatientFinancialView;

            _MessageBoxCaption = gloPMGlobal.MessageBoxCaption;
            cmbBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbAccounts.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAccounts.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbPatients.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPatients.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbSettings.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSettings.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
        }

        public frmRpt_Revised_PatientStatement(string databaseconnectionstring, Int64 nPatientID, Int64 nPAccountID, bool isPatientFinancialView, bool bIsGWPatientStatement, string sStatementDate, DateTime dtStmCreateDate)
        {
            InitializeComponent();

            SelectedPatientID = nPatientID;
            SelectedAccountID = nPAccountID;
            _databaseconnectionstring = databaseconnectionstring;
            _isPatientFinancialView = isPatientFinancialView;
            _isGWPatientStatement = bIsGWPatientStatement;
            SelectedStatementDate = sStatementDate;
            _StatementCreateDate = dtStmCreateDate;

            _MessageBoxCaption = gloPMGlobal.MessageBoxCaption;
            cmbBusinessCenter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbBusinessCenter.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbAccounts.DrawMode = DrawMode.OwnerDrawFixed;
            cmbAccounts.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbPatients.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPatients.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
            cmbSettings.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSettings.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);
        }

        #endregion

        #region " Form Get Instance Methods "

        private bool blnDisposed;

        protected override void Dispose(bool disposing)
        {
            System.Windows.Forms.DateTimePicker[] cntdtControls = { dtpEndDate, dtpStartDate, dtCriteriaEndDate, dtCriteriaStartDate, dateTimePicker1, dateTimePicker2 };
            System.Windows.Forms.Control[] cntControls = { dtpEndDate, dtpStartDate, dtCriteriaEndDate, dtCriteriaStartDate, dateTimePicker1, dateTimePicker2 };
 
            // Check to see if Dispose has already been called. 
            if (!(this.blnDisposed))
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
 
                if ((disposing))
                {

                    try
                    {
                        if ((components != null))
                        {
                            components.Dispose();
                        }


                        if (cntdtControls != null)
                        {
                            if (cntdtControls.Length > 0)
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(ref cntdtControls);

                            }
                        }
                        if (cntControls != null)
                        {
                            if (cntControls.Length > 0)
                            {
                                gloGlobal.cEventHelper.DisposeAllControls(ref cntControls);

                            }
                        }
                        //if (dtpEndDate != null)
                        //{
                        //    try
                        //    {
                        //        gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpEndDate);
                        //    }
                        //    catch
                        //    {
                        //    }
                        //    dtpEndDate.Dispose();
                        //    dtpEndDate = null;
                        //}
                    }
                    catch
                    {
                    }

                    //try
                    //{
                    //    if (dtpStartDate != null)
                    //    {
                    //        try
                    //        {
                    //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpStartDate);
                    //        }
                    //        catch
                    //        {
                    //        }
                    //        dtpStartDate.Dispose();
                    //        dtpStartDate = null;
                    //    }
                    //}
                    //catch
                    //{
                    //}

                    //try
                    //{
                    //    if (dtCriteriaEndDate != null)
                    //    {
                    //        try
                    //        {
                    //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtCriteriaEndDate);
                    //        }
                    //        catch
                    //        {
                    //        }
                    //        dtCriteriaEndDate.Dispose();
                    //        dtCriteriaEndDate = null;
                    //    }
                    //}
                    //catch
                    //{
                    //}

                    //try
                    //{
                    //    if (dtCriteriaStartDate != null)
                    //    {
                    //        try
                    //        {
                    //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtCriteriaStartDate);
                    //        }
                    //        catch
                    //        {
                    //        }
                    //        dtCriteriaStartDate.Dispose();
                    //        dtCriteriaStartDate = null;
                    //    }
                    //}
                    //catch
                    //{
                    //}

                    //try
                    //{
                    //    if (dateTimePicker1 != null)
                    //    {
                    //        try
                    //        {
                    //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dateTimePicker1);
                    //        }
                    //        catch
                    //        {
                    //        }
                    //        dateTimePicker1.Dispose();
                    //        dateTimePicker1 = null;
                    //    }
                    //}
                    //catch
                    //{
                    //}

                    //try
                    //{
                    //    if (dateTimePicker2 != null)
                    //    {
                    //        try
                    //        {
                    //            gloGlobal.cEventHelper.RemoveAllEventHandlers(dateTimePicker2);
                    //        }
                    //        catch
                    //        {
                    //        }
                    //        dateTimePicker2.Dispose();
                    //        dateTimePicker2 = null;
                    //    }
                    //}
                    //catch
                    //{
                    //}

                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    // Dispose managed resources. 
                  
                    if (tooltip_Rpt != null)
                    {
                        tooltip_Rpt.Dispose();
                        tooltip_Rpt = null;
                    }
                    if (toolTip1_Rpt != null)
                    {
                        toolTip1_Rpt.Dispose();
                        toolTip1_Rpt = null;
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
            base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            // Take yourself off of the finalization queue 
            // to prevent finalization code for this object 
            // from executing a second time. 
            System.GC.SuppressFinalize(this);
        }

        ~frmRpt_Revised_PatientStatement()
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            { }
            return frm;
        }
        #endregion " Form Get Instance Methods "

        #region "Form Events"

        // Reviewed on 12/03/2013 - seems to be ok 
        private void frmRpt_Revised_PatientStatement_Load(object sender, EventArgs e)
        {
            try
            {
               
                SetButtonVisibility("FormLoad");
                btnUp.BackgroundImage = global::gloBilling.Properties.Resources.UP;
                btnUp.BackgroundImageLayout = ImageLayout.Center;

                #region "Settings - Business Center / Close Date / Patient Account Feature "

                IsBusinessCenterEnable = gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_Statment");

                if (IsBusinessCenterEnable)
                {
                    pnlMainStatement.Height = 92;
                    FetchBusinessCenterCombo();
                }
                else
                {
                    pnlBusinessCenter.Visible = false;
                    pnlMainStatement.Height = 62;
                }

                LastCloseDate = objClsgloStatment.getCloseDate();
                if (LastCloseDate == "")
                {
                    LastCloseDate = System.DateTime.Now.ToShortDateString();
                }
                else
                {
                    if (_isGWPatientStatement && !string.IsNullOrEmpty(SelectedStatementDate))
                    {
                        dtpEndDate.Value = Convert.ToDateTime(SelectedStatementDate);
                        dtCriteriaEndDate.Value = Convert.ToDateTime(SelectedStatementDate);
                    }
                    else
                    {
                        dtpEndDate.Value = Convert.ToDateTime(LastCloseDate);
                        dtCriteriaEndDate.Value = Convert.ToDateTime(LastCloseDate);
                    }
                }

                objgloAccount = new gloAccount(_databaseconnectionstring);
                IsPatientAccountEnable = objgloAccount.GetPatientAccountFeatureSetting();
                if (IsPatientAccountEnable == true)
                {
                    label31.Text = "Batch Accounts :";
                    label9.Text = "Include Guarantors :";
                    label50.Text = "Accounts";
                }
                else
                {
                    label31.Text = "Batch Patients :";
                    label9.Text = "Include Patients :";
                    label50.Text = "Patients";
                }

                #endregion
                
                if (IsCalledFromPatAcct) 
                { SetButtonVisibility("Individual"); }
                

                if (SelectedPatientID != 0 || SelectedAccountID != 0)
                {
                    FillAccountsDetails(SelectedPatientID);

                    if (_isPatientFinancialView)
                    {
                        cmbAccounts.SelectedValue = SelectedAccountID;
                    }
                    else
                    {
                        if (Convert.ToInt64(cmbAccounts.SelectedValue) != 0)
                        { SelectedAccountID = Convert.ToInt64(cmbAccounts.SelectedValue); }
                    }

                    FillPatientDetails(SelectedAccountID);
                }

                FetchCriteriasCombo();

                if (_isGWPatientStatement)
                {
                    pnlMainStatement.Visible = false;
                }

                if (IsCalledFromInsPmt)
                { generateIndividualStmt(); }

                IsStatementNotificationOn = GetPatientPortalStatementNotificationSetting();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            if (objgloAccount != null)
            {
                objgloAccount.Dispose();
            }

            gloC1FlexStyle.Style(c1PatientList, true);
            btnDown.Visible = false;
        }

        // Reviewed on 12/03/2013 - seems to be ok
        private void frmRpt_Revised_PatientStatement_Shown(object sender, EventArgs e)
        {
            TurnOffFormLevelDoubleBuffering();
        }

        // Reviewed on 12/03/2013 - TODO : need to check ->  this.Dispose();
        private void frmRpt_Revised_PatientStatement_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (objrptPatientStatementForGateWayEDI != null)
                {
                    if (objrptPatientStatementForGateWayEDI.IsLoaded)
                    {
                        objrptPatientStatementForGateWayEDI.Close();
                    }
                    objrptPatientStatementForGateWayEDI.Dispose();
                }

                if (objrptPatientStatementForGateWayEDI_GW != null)
                {
                    if (objrptPatientStatementForGateWayEDI_GW.IsLoaded)
                    {
                        objrptPatientStatementForGateWayEDI_GW.Close();
                    }
                    objrptPatientStatementForGateWayEDI_GW.Dispose();
                }

                this.Text = string.Empty;
                this.Dispose();

                if (on_FromClose != null)
                {
                    on_FromClose(sender, e);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception In closed . "+ex.ToString(), false);
            }
        }

        int originalExStyle = -1;
        bool enableFormLevelDoubleBuffering = true;

        protected override CreateParams CreateParams
        {
            get
            {
                if (originalExStyle == -1)
                    originalExStyle = base.CreateParams.ExStyle;

                CreateParams cp = base.CreateParams;
                if (enableFormLevelDoubleBuffering)
                    cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                else
                    cp.ExStyle = originalExStyle;

                return cp;
            }
        }

        private void TurnOffFormLevelDoubleBuffering()
        {
            enableFormLevelDoubleBuffering = false;
            this.MaximizeBox = true;
        }

        #endregion "Form Events"

        #region "Toolstrip Events"

        private void tsb_ViewStatement_Click(object sender, EventArgs e)
        {
            _x = Cursor.Position.X;
            _y = Cursor.Position.Y;

        //    nDelay = 1000;

            ViewStatement();
        }

        private void ViewStatement()
        {
            try
            {
                if (ValidateStatement())
                {
                    SetButtonVisibility("Generate");
                    ProcessIndividualStatement(StatementAction.ViewOnly);
                }
                else
                {
                    if (LastCloseDate != "")
                    {
                        dtpEndDate.Value = Convert.ToDateTime(LastCloseDate);
                        dtCriteriaEndDate.Value = Convert.ToDateTime(LastCloseDate);
                    }
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
            CurrentView = SelectedView.InvidualView;

        //    nDelay = 100;
            _x = Cursor.Position.X;
            _y = Cursor.Position.Y;

            try
            {
                if (IsPatientAccountEnable)
                {
                    cmbAccounts.Visible = true;
                    lblAccount.Visible = true;
                    btnBrowseAcount.Visible = true;

                    this.cmbAccounts.SelectedIndexChanged -= new System.EventHandler(this.cmbAccounts_SelectedIndexChanged);

                    //If patientaccount having morethan one patient then default to All Acct Patients
                    if (cmbPatients.Items.Count > 1 && (!_isGWPatientStatement))
                    {
                        _cmbPatientLoadFlag = false;
                        cmbPatients.SelectedIndex = 0;
                        _cmbPatientLoadFlag = true;
                    }
                    else //else default to that patient.
                    {
                        _cmbPatientLoadFlag = false;
                        cmbPatients.SelectedValue = SelectedPatientID;
                        _cmbPatientLoadFlag = true;
                    }
                }
                else
                {
                    cmbAccounts.Visible = false;
                    lblAccount.Visible = false;
                    btnBrowseAcount.Visible = false;
                    cmbPatients.SelectedIndex = 0;
                }

                if (_isGWPatientStatement || ValidateStatement())
                {
                    ProcessIndividualStatement(StatementAction.ViewOnly);
                }
                else
                {
                    //TODO : Need to check
                    //fillRevisedPatientStatement(0, 0);
                }

                FillIndividualBatchSummary();

                gloC1FlexStyle.Style(c1PatientList, true);
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                this.cmbAccounts.SelectedIndexChanged += new System.EventHandler(this.cmbAccounts_SelectedIndexChanged);
            }
        }

        private void tsb_btnBatch_Click(object sender, EventArgs e)
        {
            CurrentView = SelectedView.BatchView;

            dtpEndDate.Text = LastCloseDate;
        }

        private void tsb_Send_Electronic_Click(object sender, EventArgs e)
        {
            if (ValidateStatement())
            {
                ts_Commands.Enabled = false;
                try
                {
                    if (IsIndvidualView)
                    {
                        ProcessIndividualStatement(StatementAction.SendOnly);
                    }
                    else
                    {
                        ProcessBatchStatement(StatementAction.SendOnly);
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally { ts_Commands.Enabled = true; }
            }
        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            if (ValidateStatement())
            {
                ts_Commands.Enabled = false;
                try
                {
                    if (IsIndvidualView)
                    {
                        ProcessIndividualStatement(StatementAction.PrintOnly);
                    }
                    else
                    {
                        ProcessBatchStatement(StatementAction.PrintOnly);
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally { ts_Commands.Enabled = true; }
            }
        }

        private void tsb_GenerateBatch_Click(object sender, EventArgs e)
        {
            _isGenerateBatch = true;

            IsIndvidualView = false;
            CurrentView = SelectedView.BatchView;

            this.Parent.Cursor = Cursors.WaitCursor;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (ValidateStatementDates())
                {
                    SetButtonVisibility("GenerateBatch");

                    if (IsBusinessCenterEnable)
                    { pnlMainStatement.Height = 150; }
                    else
                    { pnlMainStatement.Height = 120; }

                    if (cmbSettings.SelectedValue != null)
                    {
                        if (cmbSettings.SelectedValue.ToString() != "0")
                        {
                            pnlCurrentBatch.Visible = true;
                            FillControlsPerCriteria(Convert.ToInt64(cmbSettings.SelectedValue));

                            if (IsPatientAccountEnable)
                            { ShowAccountListOnC1Grid(); }
                            else
                            { ShowPatientListOnC1Grid(); }

                            lblSettings.Text = cmbSettings.Text.ToString();
                            FillBatchDetails();

                            if (c1PatientList.Rows.Count > 1)
                            {
                                tsb_ViewStatement.Visible = true;
                            }
                            else
                            {
                                tsb_ViewStatement.Visible = false;

                                if (IsPatientAccountEnable == true)
                                { MessageBox.Show("No Account found for selected setting.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                else
                                { MessageBox.Show("No patient found for selected setting.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
            finally
            {
                this.Parent.Cursor = Cursors.Default;
                this.Cursor = Cursors.Default;
            }
        }

        private void tsb_btnShowList_Click(object sender, EventArgs e)
        {
            tsb_GenerateBatch.Visible = true;
            tsb_PatAcctAccount.Visible = true;
            tsb_btnShowList.Visible = false;
            pnlFilteredPatList.Visible = true;
            btnDown.Visible = false;
            btnUp.Visible = true;
        }

        private void tsb_PatAcctAccount_Click(object sender, EventArgs e)
        {
            gloAccountsV2.frmPatientFinancialViewV2 objfrm = null;
            try
            {
                if (c1PatientList.RowSel > 0)
                {
                    Int64 PatientID = 0;
                    Int64.TryParse(Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index)), out PatientID);
                    Int64 PAccountID = 0;
                    Int64.TryParse(Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["nPAccountId"].Index)), out PAccountID);

                    objfrm = new gloAccountsV2.frmPatientFinancialViewV2(PatientID)
                    {

                    };
                    objfrm._nSelectAccountId = PAccountID;
                    objfrm.StartPosition = FormStartPosition.CenterScreen;
                    objfrm.WindowState = FormWindowState.Maximized;
                    objfrm.ShowDialog(this);
                    objfrm.Dispose();


                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (objfrm != null) { objfrm.Dispose(); }
            }
        }

        private bool IsValidSelectedPatientAcc()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool bReturn = true;
            int patientCount = 1;
            int AccCount = 1;
            string _sqlQuery = string.Empty;
            try
            {
                Int64 _nselectedPatient = Convert.ToInt64(cmbPatients.SelectedValue);
                Int64 _nselectedAcc = Convert.ToInt64(cmbAccounts.SelectedValue);
                oDB.Connect(false);

                if (cmbPatients.Text != "All Acct Patients")
                {
                    _sqlQuery = "select COUNT(1) from patient where nPatientID = " + _nselectedPatient;
                    patientCount = Convert.ToInt16(oDB.ExecuteScalar_Query(_sqlQuery));
                }


                _sqlQuery = "select COUNT(1) from PA_Accounts_Patients where nPAccountID = " + _nselectedAcc;
                AccCount = Convert.ToInt16(oDB.ExecuteScalar_Query(_sqlQuery));

                if (patientCount <= 0 || AccCount <= 0)
                {
                    bReturn = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return bReturn;
        }

        #endregion

        #region "Paper Patient Statment"

        private void FilterPatientByName()
        {
            try
            {
                if (c1PatientList.DataSource != null)
                {
                    if (_generateBatchFlag)
                    {
                        int _c1PatientListColumnCount = c1PatientList.Rows.Count - 1;
                        lblCount.Text = _c1PatientListColumnCount.ToString();
                        Decimal _TotalPatientDue = 0;
                        if (_c1PatientListColumnCount > 0)
                        {
                            for (int i = 1; i < c1PatientList.Rows.Count; i++)
                            {
                                if (IsPatientAccountEnable == true)
                                {
                                    _TotalPatientDue = _TotalPatientDue + Convert.ToDecimal(c1PatientList.GetData(i, "sAccountDue"));
                                }
                                else
                                {
                                    _TotalPatientDue = _TotalPatientDue + Convert.ToDecimal(c1PatientList.GetData(i, "spatientDue"));
                                }
                            }
                            lblTotaldue.Text = _TotalPatientDue.ToString();
                        }
                        else
                        {
                            tsb_Send.Visible = false;
                            tsb_ViewStatement.Visible = false;
                            tsb_GenerateBatch.Visible = true;
                            tsb_btnShowList.Visible = true;
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
            DataTable dt = null;
            try
            {
                dt = objClsgloStatment.FillDetails(cmbSettings.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        lblSettings.Text = dt.Rows[0]["sSettingName"].ToString() + " :";
                        lblUserName.Text = dt.Rows[0]["sUserName"].ToString();
                        //Date Format.
                        lbldtStatementDate.Text = Convert.ToDateTime(dt.Rows[0]["dtStatementDate"]).ToString("MM/dd/yyyy");
                        lblmaxCreateDate.Text = Convert.ToDateTime(dt.Rows[0]["dtCreateDate"]).ToString("MM/dd/yyyy");

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
            Int64 nPatientID = 0;
            Int64 nAccountID = 0;

            try
            {
                if (IsCalledFromPatAcct && IsAllAccPatSelected && IsPatientAccountEnable)
                {
                    if (cmbAccounts.SelectedValue != null)
                    {
                        if (cmbAccounts.SelectedValue.ToString() != "0")
                        {
                            nAccountID = Convert.ToInt64(cmbAccounts.SelectedValue);
                        }
                        else
                        {
                            nAccountID = 0;
                        }
                    }
                    if (dsPatientStatementMain != null && dsPatientStatementMain.Tables.Count > 0)
                    {
                        lblPatientDue.Text = Convert.ToString(Convert.ToDecimal(dsPatientStatementMain.Tables["dt_PatientReserve"].Rows[0]["PatientDue"]) - Convert.ToDecimal(dsPatientStatementMain.Tables["dt_PatientReserve"].Rows[0]["AvailableReserve"]));
                    }
                }
                else
                {
                    if (cmbPatients.SelectedValue != null)
                    {
                        if (cmbPatients.SelectedValue.ToString() != "0")
                        {
                            nPatientID = Convert.ToInt64(cmbPatients.SelectedValue);
                        }
                        else
                        {
                            nPatientID = 0;
                        }

                        if (cmbAccounts.SelectedValue != null)
                        {
                            if (cmbAccounts.SelectedValue.ToString() != "0")
                            {
                                nAccountID = Convert.ToInt64(cmbAccounts.SelectedValue);
                            }
                            else
                            {
                                nAccountID = 0;
                            }
                        }
                        if (dsPatientStatementMain != null && dsPatientStatementMain.Tables.Count>0)
                        {
                            lblPatientDue.Text = Convert.ToString(Convert.ToDecimal(dsPatientStatementMain.Tables["dt_PatientReserve"].Rows[0]["PatientDue"]) - Convert.ToDecimal(dsPatientStatementMain.Tables["dt_PatientReserve"].Rows[0]["AvailableReserve"]));
                        }
                    }
                    else
                    {
                        lblPatientDue.Text = "0";
                    }
                }

                if (nPatientID > 0)// Indicates patient
                {
                    lblptName.Text = cmbPatients.Text.Trim().ToString().Split('-')[1] + " :";
                }
                else //Indicates Account
                {
                    if (cmbAccounts.Text != "")
                    {
                        lblptName.Text = cmbAccounts.Text.Trim().ToString().Split('-')[1] + " :";
                    }
                    else
                    {
                        lblptName.Text = " :";
                    }
                }


                if (lblptName.Text != " :")
                {
                    DataTable dtIndividual = null;
                    if (nPatientID > 0)
                    {
                        dtIndividual = objClsgloStatment.FillIndividualDetails(cmbPatients.Text.Trim().Split('-')[1].Split(' ')[0].Replace("'", "''"), nAccountID, nPatientID);
                    }
                    else
                    {
                        dtIndividual = objClsgloStatment.FillIndividualDetails(cmbAccounts.Text.Trim().ToString().Split('-')[1].Split(' ')[0].Replace("'", "''"), nAccountID, nPatientID);
                    }
                    if (dtIndividual != null && dtIndividual.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtIndividual.Rows.Count; i++)
                        {
                            lbldtcreate.Text = Convert.ToDateTime(dtIndividual.Rows[0]["dtCreateDate"]).ToString("MM/dd/yyyy").Trim();
                            lblUName.Text = dtIndividual.Rows[0]["sUserName"].ToString();
                            lbldtstdate.Text = Convert.ToDateTime(dtIndividual.Rows[0]["dtStatementDate"]).ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    {
                        lbldtcreate.Text = "";
                        lblUName.Text = "";
                        lbldtstdate.Text = "";
                    }

                    if (dtIndividual != null)
                    { dtIndividual.Dispose(); dtIndividual = null; }
                }
                else
                {
                    lbldtcreate.Text = "";
                    lblUName.Text = "";
                    lbldtstdate.Text = "";
                    lblPatientDue.Text = "0";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void ShowPatientListOnC1Grid()
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
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

                if (IsIndvidualView == false)
                {
                    dtCriteriaEndDate.Checked = true;
                    endDate = gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString());
                }
                else
                {
                    endDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString());
                }

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "PA_GET_Patient_DueList_V2";
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;

                if (endDate != 0)
                {
                    _sqlcommand.Parameters.Add("@dtDate", System.Data.SqlDbType.DateTime);
                    _sqlcommand.Parameters["@dtDate"].Value = gloDateMaster.gloDate.DateAsDate(endDate);
                }
                if (Convert.ToString(lblDueAmt.Text) != "")
                {
                    dueAmt = Convert.ToDecimal(Convert.ToString(lblDueAmt.Text));
                }

                _sqlcommand.Parameters.Add("@nDueAmt", System.Data.SqlDbType.Decimal);
                _sqlcommand.Parameters["@nDueAmt"].Value = dueAmt;

                _sqlcommand.Parameters.Add("@nDateCriteria", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nDateCriteria"].Value = gloDateMaster.gloDate.DateAsNumber(_filterDate.ToShortDateString().ToString());

                _sqlcommand.Parameters.Add("@sLastName", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@sLastName"].Value = "[" + lblNameFrom.Text + "-" + lblNameTo.Text + "]";

                _sqlcommand.Parameters.Add("@nCount", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nCount"].Value = numQueueClaimCount.Value;

                //_sqlcommand.Parameters.Add("@nBusinessCenterID", System.Data.SqlDbType.BigInt);
                //_sqlcommand.Parameters["@nBusinessCenterID"].Value = cmbBusinessCenter.SelectedValue;

                da.Fill(_dtPatients);
                da.Dispose();

                if (_dtPatients != null && _dtPatients.Rows.Count > 0)
                {
                    C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1PatientList.Styles.Add("cs_CurrencyStyle");
                    try
                    {
                        if (c1PatientList.Styles.Contains("cs_CurrencyStyle"))
                        {
                            csCurrencyStyle = c1PatientList.Styles["cs_CurrencyStyle"];
                        }
                        else
                        {
                            csCurrencyStyle = c1PatientList.Styles.Add("cs_CurrencyStyle");
                            //csCurrencyStyle.DataType = typeof(System.Decimal);
                            //csCurrencyStyle.Format = "c";
                            //csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                            //csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                        }

                    }
                    catch
                    {
                        csCurrencyStyle = c1PatientList.Styles.Add("cs_CurrencyStyle");
                        //csCurrencyStyle.DataType = typeof(System.Decimal);
                        //csCurrencyStyle.Format = "c";
                        //csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        //csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                    }
                    DataView _dv = _dtPatients.DefaultView;
                    c1PatientList.DataSource = _dv;

                    c1PatientList.Cols[0].DataType = typeof(bool);
                    c1PatientList.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    c1PatientList.SetData(0, 0, "Select All", false);

                    tsb_PatAcctAccount.Visible = true;

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
                    c1PatientList.Cols["spatientDue"].Visible = true;
                    c1PatientList.Cols["spatientDue"].Format = "c";
                    c1PatientList.Cols["spatientDue"].DataType = typeof(System.Decimal);
                    c1PatientList.Cols["nPAccountID"].Visible = false;

                    c1PatientList.Cols["Select"].DataType = typeof(System.Boolean);
                    c1PatientList.Cols["Select"].AllowEditing = true;
                    c1PatientList.Cols["Select"].AllowSorting = false;

                    c1PatientList.Cols["sLastName"].AllowEditing = false;
                    c1PatientList.Cols["sPatientCode"].AllowEditing = false;
                    c1PatientList.Cols["sMiddleName"].AllowEditing = false;
                    c1PatientList.Cols["sFirstName"].AllowEditing = false;
                    c1PatientList.Cols["dtDOB"].AllowEditing = false;
                    c1PatientList.Cols["sPhone"].AllowEditing = false;
                    c1PatientList.Cols["sMobile"].AllowEditing = false;
                    c1PatientList.Cols["sSSN"].AllowEditing = false;
                    c1PatientList.Cols["sProviderName"].AllowEditing = false;
                    c1PatientList.Cols["spatientDue"].AllowEditing = false;

                    c1PatientList.Cols["sPatientCode"].Caption = "Code";
                    c1PatientList.Cols["sLastName"].Caption = "Last Name";
                    c1PatientList.Cols["sMiddleName"].Caption = "MI";
                    c1PatientList.Cols["sFirstName"].Caption = "First Name";
                    c1PatientList.Cols["dtDOB"].Caption = "DOB";
                    c1PatientList.Cols["sPhone"].Caption = "Phone";
                    c1PatientList.Cols["sMobile"].Caption = "Mobile";
                    c1PatientList.Cols["sSSN"].Caption = "SSN";
                    c1PatientList.Cols["sProviderName"].Caption = "Provider";
                    c1PatientList.Cols["spatientDue"].Caption = "Patient Due";

                    c1PatientList.Cols["Select"].Width = 90;
                    c1PatientList.Cols["sPatientCode"].Width = 130;
                    c1PatientList.Cols["sLastName"].Width = 140;
                    c1PatientList.Cols["sMiddleName"].Width = 40;
                    c1PatientList.Cols["sFirstName"].Width = 140;
                    c1PatientList.Cols["dtDOB"].Width = 130;
                    c1PatientList.Cols["sPhone"].Width = 130;
                    c1PatientList.Cols["sMobile"].Width = 130;
                    c1PatientList.Cols["sSSN"].Width = 130;
                    c1PatientList.Cols["sProviderName"].Width = 170;
                    c1PatientList.Cols["spatientDue"].Width = 100;
                    c1PatientList.Rows[0].Selected = true;

                    c1PatientList.Cols["dtDOB"].DataType = typeof(System.DateTime);
                    c1PatientList.Cols["dtDOB"].Format = "MM/dd/yyyy";


                }
                else
                {
                   // c1PatientList.Clear();
                    c1PatientList.DataSource = null;
                    c1PatientList.Rows.Count = 1;
                    c1PatientList.Rows.Fixed = 1;
                    c1PatientList.Cols.Count = 1;
                    tsb_Send.Visible = false;
                    tsb_PatAcctAccount.Visible = false;

                }
                c1PatientList.AutoResize = false;
                //c1PatientList.AllowEditing = false;
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
                if (_sqlcommand != null) 
                {
                    if (_sqlcommand.Parameters != null) { _sqlcommand.Parameters.Clear(); }
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                if (oConnection != null) { oConnection.Close(); oConnection.Dispose(); }
                if (_dtPatients != null) { _dtPatients.Dispose(); }
            }
        }

        private void ShowAccountListOnC1Grid()
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();
            Int64 endDate = 0;
            Decimal dueAmt = 0;
            DateTime _filterDate = new DateTime();
            TimeSpan _tWaitDays = new TimeSpan();
            SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
            DataTable _dtPatients = new DataTable();
            int iPatientRow = 0;
            // Get all account to be listed in batch statement
            try
            {
                if (lblWaitDays.Text.ToString() != "")
                {
                    _tWaitDays = new TimeSpan(Convert.ToInt32(lblWaitDays.Text.ToString()), 0, 0, 0);
                    _filterDate = dtCriteriaEndDate.Value.Subtract(_tWaitDays);
                }

                if (IsIndvidualView == false)
                {
                    dtCriteriaEndDate.Checked = true;
                    endDate = gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString());
                }
                else
                {
                    endDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString());
                }

                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "PA_GET_Account_DueList_V2";
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;

                if (endDate != 0)
                {
                    _sqlcommand.Parameters.Add("@dtDate", System.Data.SqlDbType.DateTime);
                    _sqlcommand.Parameters["@dtDate"].Value = gloDateMaster.gloDate.DateAsDate(endDate);
                }
                if (Convert.ToString(lblDueAmt.Text) != "")
                {
                    dueAmt = Convert.ToDecimal(Convert.ToString(lblDueAmt.Text));
                }

                _sqlcommand.Parameters.Add("@nDueAmt", System.Data.SqlDbType.Decimal);
                _sqlcommand.Parameters["@nDueAmt"].Value = dueAmt;

                _sqlcommand.Parameters.Add("@nDateCriteria", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nDateCriteria"].Value = gloDateMaster.gloDate.DateAsNumber(_filterDate.ToShortDateString().ToString());

                _sqlcommand.Parameters.Add("@sLastName", System.Data.SqlDbType.VarChar);
                _sqlcommand.Parameters["@sLastName"].Value = "[" + lblNameFrom.Text + "-" + lblNameTo.Text + "]";

                _sqlcommand.Parameters.Add("@nCount", System.Data.SqlDbType.BigInt);
                _sqlcommand.Parameters["@nCount"].Value = numQueueClaimCount.Value;

                if (IsBusinessCenterEnable)
                {
                    if (Convert.ToString(cmbBusinessCenter.SelectedValue) != "" && Convert.ToString(cmbBusinessCenter.SelectedValue) != "0")
                    {
                        _sqlcommand.Parameters.Add("@nBusinessCenterID", System.Data.SqlDbType.BigInt);
                        _sqlcommand.Parameters["@nBusinessCenterID"].Value = cmbBusinessCenter.SelectedValue;
                    }
                }
                else
                {
                    _sqlcommand.Parameters.Add("@nBusinessCenterID", System.Data.SqlDbType.BigInt);
                    _sqlcommand.Parameters["@nBusinessCenterID"].Value = DBNull.Value;
                }


                da.Fill(_dtPatients);
                da.Dispose();

                if (_dtPatients != null && _dtPatients.Rows.Count > 0)
                {
                    C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1PatientList.Styles.Add("cs_CurrencyStyle");
                    try
                    {
                        if (c1PatientList.Styles.Contains("cs_CurrencyStyle"))
                        {
                            csCurrencyStyle = c1PatientList.Styles["cs_CurrencyStyle"];
                        }
                        else
                        {
                            csCurrencyStyle = c1PatientList.Styles.Add("cs_CurrencyStyle");
                            //csCurrencyStyle.DataType = typeof(System.Decimal);
                            //csCurrencyStyle.Format = "c";
                            //csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                            //csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                        }

                    }
                    catch
                    {
                        csCurrencyStyle = c1PatientList.Styles.Add("cs_CurrencyStyle");
                        //csCurrencyStyle.DataType = typeof(System.Decimal);
                        //csCurrencyStyle.Format = "c";
                        //csCurrencyStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        //csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                    }
                    DataView _dv = _dtPatients.DefaultView;
                    c1PatientList.DataSource = _dv;
                    if (IsBusinessCenterEnable)
                    {
                        iPatientRow = c1PatientList.FindRow("False", 1, 0, true);
                        {
                            if (iPatientRow > 0)
                            {
                                c1PatientList.Cols[0].DataType = typeof(bool);
                                c1PatientList.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                                c1PatientList.SetData(0, 0, "Select All", false);
                            }
                            else
                            {
                                c1PatientList.Cols[0].DataType = typeof(bool);
                                c1PatientList.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                                c1PatientList.SetData(0, 0, "Select All", false);
                            }
                        }
                    }
                    else
                    {
                        c1PatientList.Cols[0].DataType = typeof(bool);
                        c1PatientList.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                        c1PatientList.SetData(0, 0, "Select All", false);
                    }

                    tsb_PatAcctAccount.Visible = true;

                    c1PatientList.Cols["nPAccountID"].Visible = false;
                    c1PatientList.Cols["sAccountNo"].Visible = true;
                    c1PatientList.Cols["sAccountDesc"].Visible = true;
                    c1PatientList.Cols["PatientID"].Visible = false;
                    c1PatientList.Cols["Guarantor"].Visible = true;
                    if (IsBusinessCenterEnable)
                        c1PatientList.Cols["sBusinessCenterCode"].Visible = true;
                    else
                        c1PatientList.Cols["sBusinessCenterCode"].Visible = false;
                    c1PatientList.Cols["sFirstName"].Visible = false;
                    c1PatientList.Cols["sMiddleName"].Visible = false;
                    c1PatientList.Cols["sLastName"].Visible = false;
                    //c1PatientList.Cols["sGuarantorType"].Visible = true;
                    c1PatientList.Cols["IsBusinessCenterAssociated"].Visible = false;
                    c1PatientList.Cols["sAddress"].Visible = true;
                    c1PatientList.Cols["sAccountDue"].Visible = true;
                    c1PatientList.Cols["sAccountDue"].Format = "c";
                    c1PatientList.Cols["sAccountDue"].DataType = typeof(System.Decimal);
                    c1PatientList.Cols["Patient"].Visible = true;
                    c1PatientList.Cols["Select"].DataType = typeof(System.Boolean);
                    c1PatientList.Cols["Select"].AllowEditing = true;
                    c1PatientList.Cols["Select"].AllowSorting = false;

                    c1PatientList.Cols["sAccountNo"].AllowEditing = false;
                    c1PatientList.Cols["sAccountDesc"].AllowEditing = false;
                    c1PatientList.Cols["Guarantor"].AllowEditing = false;
                    c1PatientList.Cols["sLastName"].AllowEditing = false;
                    c1PatientList.Cols["sMiddleName"].AllowEditing = false;
                    c1PatientList.Cols["sFirstName"].AllowEditing = false;
                    c1PatientList.Cols["sBusinessCenterCode"].AllowEditing = false;
                    //c1PatientList.Cols["sGuarantorType"].AllowEditing = false;
                    c1PatientList.Cols["sAddress"].AllowEditing = false;
                    c1PatientList.Cols["sAccountDue"].AllowEditing = false;
                    c1PatientList.Cols["Patient"].AllowEditing = false;

                    c1PatientList.Cols["sAccountNo"].Caption = "Acct.#";
                    c1PatientList.Cols["sAccountDesc"].Caption = "Account Desc";
                    c1PatientList.Cols["Guarantor"].Caption = "Guarantor";
                    c1PatientList.Cols["sLastName"].Caption = "Last Name";
                    c1PatientList.Cols["sMiddleName"].Caption = "MI";
                    c1PatientList.Cols["sFirstName"].Caption = "First Name";
                    //c1PatientList.Cols["sGuarantorType"].Caption = "Guar.Type";
                    c1PatientList.Cols["sBusinessCenterCode"].Caption = "BUS";
                    c1PatientList.Cols["sAddress"].Caption = "Address";
                    c1PatientList.Cols["sAccountDue"].Caption = "Account Due";
                    c1PatientList.Cols["Patient"].Caption = "Patient";

                    if (IsBusinessCenterEnable)
                    {
                        c1PatientList.Cols["sAccountNo"].Width = 110;
                        c1PatientList.Cols["sAccountDesc"].Width = 190;
                        c1PatientList.Cols["Guarantor"].Width = 190;
                        //c1PatientList.Cols["sGuarantorType"].Width = 100;
                        c1PatientList.Cols["sBusinessCenterCode"].Width = 60;
                        c1PatientList.Cols["sAddress"].Width = 240;
                        c1PatientList.Cols["sAccountDue"].Width = 100;
                        c1PatientList.Cols["Patient"].Width = 190;
                        c1PatientList.Cols["Select"].Width = 90;
                        c1PatientList.Rows[0].Selected = true;
                    }
                    else
                    {
                        c1PatientList.Cols["sAccountNo"].Width = 130;
                        c1PatientList.Cols["sAccountDesc"].Width = 210;
                        c1PatientList.Cols["Guarantor"].Width = 210;
                        //c1PatientList.Cols["sGuarantorType"].Width = 100;
                        c1PatientList.Cols["sBusinessCenterCode"].Width = 60;
                        c1PatientList.Cols["sAddress"].Width = 250;
                        c1PatientList.Cols["sAccountDue"].Width = 100;
                        c1PatientList.Cols["Patient"].Width = 210;
                        c1PatientList.Cols["Select"].Width = 90;
                        c1PatientList.Rows[0].Selected = true;
                    }

                }
                else
                {
                   // c1PatientList.Clear();
                    c1PatientList.DataSource = null;
                    c1PatientList.Rows.Count = 1;
                    c1PatientList.Rows.Fixed = 1;
                    c1PatientList.Cols.Count = 1;
                    tsb_Send.Visible = false;
                    tsb_PatAcctAccount.Visible = false;

                }
                c1PatientList.AutoResize = false;
                //c1PatientList.AllowEditing = false;
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
                if (_sqlcommand != null)
                {
                    if (_sqlcommand.Parameters != null) { _sqlcommand.Parameters.Clear(); }
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                if (oConnection != null) { oConnection.Close(); oConnection.Dispose(); }
                if (_dtPatients != null) { _dtPatients.Dispose(); }
            }
        }

        private void SetButtonVisibility(string tabName)
        {
            try
            {
                switch (tabName)
                {
                    #region "Individual"

                    case "Individual":
                        if (_isPatientFinancialView == true)
                        {
                            // view and send buttons not required in case of gateway statement
                            if (_isGWPatientStatement)
                            {
                                tsb_ViewStatement.Visible = false;
                                tsb_Send.Visible = false;
                            }
                            else
                            {
                                tsb_ViewStatement.Visible = true;
                                tsb_Send.Visible = true;
                            }
                            tsb_ViewStatement.Enabled = true;
                            pnlCriteria.Visible = false;
                            pnlPatientList.Visible = true;
                            lblTotaldue.Visible = true;
                            tsb_btnBatch.Visible = false;
                            tsb_btnIndividual.Visible = false;
                            pnlc1PatientListHeader.Visible = false;
                            pnlFilteredPatList.Visible = false;
                            pnlMainStatement.Height = 120;
                            tsb_GenerateBatch.Visible = false;
                            tsb_btnShowList.Visible = false;
                            tsb_PatAcctAccount.Visible = false;
                        }
                        else
                        {
                            tsb_ViewStatement.Visible = true;
                            tsb_Send.Visible = true;
                            tsb_ViewStatement.Enabled = true;
                            //pnlCriteria.Visible = false;
                            pnlPatientList.Visible = true;
                            pnlPatientList.BringToFront();
                            lblTotaldue.Visible = true;
                            tsb_btnBatch.Visible = true;
                            tsb_btnIndividual.Visible = false;
                            pnlc1PatientListHeader.Visible = false;
                            pnlFilteredPatList.Visible = false;
                            pnlMainStatement.Height = 120;
                            tsb_GenerateBatch.Visible = false;
                            tsb_btnShowList.Visible = false;
                            tsb_PatAcctAccount.Visible = false;
                        }
                        break;
                    #endregion "Individual"

                    #region "Batch"
                    case "Batch":
                        if (IsBusinessCenterEnable)
                        {
                            if (lblCount.Visible == true && lblmaxCreateDate.Visible == true) pnlMainStatement.Height = 150; else pnlMainStatement.Height = 92;
                        }
                        else { if (lblCount.Visible == true && lblmaxCreateDate.Visible == true) { pnlMainStatement.Height = 120; } else { pnlMainStatement.Height = 62; } }
                        if (_isGenerateBatch && c1PatientList.Rows.Count > 1)
                        {
                            tsb_Send.Visible = true;
                            tsb_ViewStatement.Visible = true;
                            tsb_btnShowList.Visible = false;
                        }
                        else
                        {
                            tsb_ViewStatement.Visible = false;
                            tsb_btnShowList.Visible = false;
                            tsb_Send.Visible = false;
                        }
                        pnlCurrentBatch.Visible = true;
                        pnlCriteria.Visible = true;
                        tsb_GenerateBatch.Visible = true;
                        pnlPatientList.Visible = false;
                        tsb_btnBatch.Visible = false;
                        tsb_btnIndividual.Visible = true;
                        pnlCriteria.Enabled = true;
                        pnlc1PatientListHeader.Visible = true;
                        pnlFilteredPatList.Visible = true;
                        if (c1PatientList.Rows.Count > 1)
                        {

                            tsb_PatAcctAccount.Visible = true;
                        }
                        else
                        {

                            tsb_PatAcctAccount.Visible = false;
                        }

                        break;
                    #endregion "Batch"

                    #region "Generate"
                    case "Generate":
                        tsb_GenerateBatch.Visible = false;
                        tsb_PatAcctAccount.Visible = false;
                        tsb_btnShowList.Visible = false;
                        if (_isGenerateBatch && c1PatientList.Rows.Count > 1 && !IsIndvidualView)
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
                        if (IsBusinessCenterEnable)
                            pnlMainStatement.Height = 150;
                        else pnlMainStatement.Height = 120;
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

                        tsb_PatAcctAccount.Visible = false;
                        break;
                    #endregion "FormLoad"

                    #region "SendBatch"
                    case "SendBatch":
                      //  c1PatientList.Clear();
                        c1PatientList.DataSource = null;
                        c1PatientList.Rows.Count = 1;
                        c1PatientList.Rows.Fixed = 1;
                        c1PatientList.Cols.Count = 1;
                        if (!IsIndvidualView)
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public void generateIndividualStmt()
        {
            tsb_btnIndividual_Click(null, null);
        }

        #endregion "Paper Patient Statment"

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
                    try
                    {
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch { }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_AccountSelectClick);
                        }
                        catch { }
                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch { }
                        try
                        {
                            oListControl.chkShowActpatient_Change -= new gloListControl.gloListControl.chkShowActpatientChange(chkSelectallPatient_Change);
                        }
                        catch { }
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }


                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, this.Width);


                oListControl.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                oListControl.ControlHeader = " Patient";
                oListControl.IsFromStatement = true;

                _CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.chkShowActpatient_Change += new gloListControl.gloListControl.chkShowActpatientChange(chkSelectallPatient_Change);
             
                this.Controls.Add(oListControl);

                oListControl.OpenControl();

                if (oListControl != null && oListControl.dgListView != null && oListControl.dgListView.DataSource != null)
                {
                    _dtStPatient = ((DataView)oListControl.dgListView.DataSource).Table;
                }
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
          //  cmbPatients.Items.Clear();
            cmbPatients.DataSource = null;
            cmbPatients.Items.Clear();
            cmbPatients.Refresh();
            FillIndividualBatchSummary();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            pnlFilteredPatList.Visible = false;
            btnDown.Visible = true;
            btnDown.BackgroundImage = global::gloBilling.Properties.Resources.Down;
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

        private void cmbSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            _generateBatchFlag = false;

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

                    gloC1FlexStyle.Style(c1PatientList, true);
                }
                combo = cmbSettings;
                if (cmbSettings.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbSettings.Items[cmbSettings.SelectedIndex])["sStatementCriteriaName"]), cmbSettings) >= cmbSettings.DropDownWidth - 20)
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbSettings, Convert.ToString(((DataRowView)cmbSettings.Items[cmbSettings.SelectedIndex])["sStatementCriteriaName"]));
                    }
                    else
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbSettings, "");
                    }
                }

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {

            this.cmbAccounts.SelectedIndexChanged -= new System.EventHandler(this.cmbAccounts_SelectedIndexChanged);
            this.cmbPatients.SelectedIndexChanged -= new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
            int _Counter = 0;
            Int64 _existingSelectedAccountID = 0;

            try
            {
                switch (_CurrentControlType)
                {

                    case gloListControl.gloListControlType.Patient:
                        {

                            if (oListControl.SelectedItems.Count > 0)
                            {

                                for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                                {
                                    SelectedPatientID = Convert.ToInt64(oListControl.SelectedItems[_Counter].ID);
                                }

                                if (oListControl.chkShowActpatient.Checked == true)
                                {
                                    if (cmbAccounts.SelectedValue.ToString() != "" && Convert.ToInt64(cmbAccounts.SelectedValue) != 0)
                                    {
                                        _existingSelectedAccountID = Convert.ToInt64(cmbAccounts.SelectedValue);
                                    }

                                    FillAccountsDetails(SelectedPatientID,_existingSelectedAccountID);

                                    if (cmbAccounts.SelectedValue.ToString() != "" && Convert.ToInt64(cmbAccounts.SelectedValue) != 0)
                                    {
                                        SelectedAccountID = Convert.ToInt64(cmbAccounts.SelectedValue);
                                    }
                                }
                                else
                                {
                                    FillAccountsDetails(SelectedPatientID, _existingSelectedAccountID);

                                    if (cmbAccounts.SelectedValue.ToString() != "" && Convert.ToInt64(cmbAccounts.SelectedValue) != 0)
                                    {
                                        SelectedAccountID = Convert.ToInt64(cmbAccounts.SelectedValue);
                                    }
                                }
                                
                                FillPatientDetails(SelectedAccountID);

                                //If patientaccount having morethan one patient then default to All Acct Patients
                                if (cmbPatients.Items.Count > 1)
                                {
                                    _cmbPatientLoadFlag = false;
                                    if (oListControl.chkShowActpatient.Checked == true)
                                        cmbPatients.SelectedValue = SelectedPatientID;
                                    else
                                        cmbPatients.SelectedIndex = 0;
                                    _cmbPatientLoadFlag = true;
                                }
                                else //else default to that patient.
                                {
                                    //Set Patient Index
                                    _cmbPatientLoadFlag = false;
                                    cmbPatients.SelectedValue = SelectedPatientID;
                                    _cmbPatientLoadFlag = true;
                                }
                                FillIndividualBatchSummary();
                            }

                        }
                        break;

                    default:
                        break;
                }
            }
            catch //(Exception ex)
            { }
            finally
            {
                this.cmbAccounts.SelectedIndexChanged += new System.EventHandler(this.cmbAccounts_SelectedIndexChanged);
                this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.cmbPatients_SelectedIndexChanged);

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
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    }
                    catch { }
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_AccountSelectClick);
                    }
                    catch { }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    try
                    {
                        oListControl.chkShowActpatient_Change -= new gloListControl.gloListControl.chkShowActpatientChange(chkSelectallPatient_Change);
                    }
                    catch { }
                }
                catch { }
                 
            }
        }

        void chkSelectallPatient_Change(object sender, EventArgs e)
        {

            DataTable dtPatients=null;
            try 
            {	     
                if (oListControl == null)
                 return;
   
                 if (oListControl.chkShowActpatient.Checked == true)
                 {
                   dtPatients = objClsgloStatment.FillPatients(Convert.ToInt64(cmbAccounts.SelectedValue));
                    if (dtPatients != null && dtPatients.Rows.Count > 0)
                    {
                        dtPatients.Columns["nPatientID"].ColumnName = "PatientID";
                        oListControl.FillListAsCriteria(0, dtPatients);
                    }
                 }
                else
                {
                    oListControl.dgListView.DataSource = _dtStPatient;
                }
   
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),false );
            }
            finally
            {
                if (dtPatients != null)
                {
                    dtPatients.Dispose();
                    dtPatients = null;
                }
            }
            
        }
        #endregion

        #region "Filter Criteria"

        private void FetchCriteriasCombo()
        {
            PatinetStatementCriteria oPatinetStatementCriteria = new PatinetStatementCriteria(_databaseconnectionstring);
            DataTable _dtFilterCriterias = null;

            try
            {
                _dtFilterCriterias = oPatinetStatementCriteria.GetPatinetStatementFilter();
                this.cmbSettings.SelectedIndexChanged -= new System.EventHandler(this.cmbSettings_SelectedIndexChanged);
                if (_dtFilterCriterias != null)
                {
                    cmbSettings.ValueMember = "nStatementCriteriaID";
                    cmbSettings.DisplayMember = "sStatementCriteriaName";
                    cmbSettings.DataSource = _dtFilterCriterias.Copy();

                    DataRow[] drDefault = _dtFilterCriterias.Select("isDefault = 'true'");

                    if (drDefault.Length > 0)
                    { cmbSettings.SelectedValue = Convert.ToInt64(drDefault[0]["nStatementCriteriaID"]); }

                    FillControlsPerCriteria(Convert.ToInt64(cmbSettings.SelectedValue));
                }
                this.cmbSettings.SelectedIndexChanged += new System.EventHandler(this.cmbSettings_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oPatinetStatementCriteria != null)
                {
                    oPatinetStatementCriteria.Dispose();
                    oPatinetStatementCriteria = null;
                }
                if (_dtFilterCriterias != null)
                {
                    _dtFilterCriterias.Dispose();
                    _dtFilterCriterias = null;
                }

                lblSettings.Text = cmbSettings.Text.ToString();
            }
        }

        private void FetchBusinessCenterCombo()
        {
            DataTable dtBusinessCenter = null;

            #region "Fill Business Center Combo box"
            try
            {

                dtBusinessCenter = gloGlobal.gloPMMasters.GetBusinessCenter();

                if (dtBusinessCenter != null)
                {
                    DataRow dr = dtBusinessCenter.NewRow();
                    dr["BusinessCenter"] = "";
                    dr["nBusinessCenterID"] = 0;

                    dtBusinessCenter.Rows.InsertAt(dr, 0);
                    cmbBusinessCenter.ValueMember = dtBusinessCenter.Columns["nBusinessCenterID"].ColumnName;
                    cmbBusinessCenter.DisplayMember = dtBusinessCenter.Columns["BusinessCenter"].ColumnName;
                    cmbBusinessCenter.DataSource = dtBusinessCenter.Copy();

                    Int64 _DefaultBusinessCenter = gloGlobal.gloPMGlobal.GetDefaultBusinessCenterForUser(gloGlobal.gloPMGlobal.UserID);
                    if (_DefaultBusinessCenter != 0)
                    {
                        cmbBusinessCenter.SelectedValue = _DefaultBusinessCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (dtBusinessCenter != null) { dtBusinessCenter.Dispose(); dtBusinessCenter = null; }
            }

            #endregion "Fill Business Center Combo box"
        }

        private void FillControlsPerCriteria(Int64 CriteriaID)
        {
            DataRow dr;
            PatinetStatementCriteria oPatinetStatementCriteria = new PatinetStatementCriteria(_databaseconnectionstring);
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
                //if ((oCurDoc != null))
                //{
                //    Marshal.ReleaseComObject(oCurDoc);
                //    oCurDoc = null;
                //}
                //if ((oTempDoc != null))
                //{
                //    Marshal.ReleaseComObject(oTempDoc);
                //    oTempDoc = null;
                //}
                //if ((oWordApp != null))
                //{
                //  //  Marshal.FinalReleaseComObject(oWordApp);
                //    oWordApp = null;
                //}
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
            catch { }

        }

        #endregion

        #region " Date and Other Validations Methods "

        private bool ValidateStatement()
        {
            bool isValidatd = true;

            if (!ValidateStatementDates())
            {
                isValidatd = false;
            }
            else
            {
                if (!ExcludeFromStatement())
                { 
                    isValidatd = false;
                }

                // Bug #61862: gloPM - Patient Statement- Application does not send/generate the batch statement
                if (IsPatientAccountEnable)
                {
                    if (!PatientAccountValidation())
                    {
                        isValidatd = false;
                    }
                }
            }
            return isValidatd;
        }

        // If Business center feature is enabled then While generating electronic Batch if account(s) does not associated 
        // with business center and included (Checked in batch view) in batch then restrict to generate electronic batch.
        private bool PatientAccountValidation()
        {
            bool isValidated = true;
            System.Collections.ArrayList arBusinessCenter = new System.Collections.ArrayList();
            System.Collections.ArrayList arMissingAddress = new System.Collections.ArrayList();

            gloStatment ObjClsgloStatment = new gloStatment();
            gloAccount objgloAccount = new gloAccount(_databaseconnectionstring);

            try
            {
                if (!IsIndvidualView)
                {
                    for (int i = 1; i < c1PatientList.Rows.Count; i++)
                    {
                        if (c1PatientList.GetData(i, c1PatientList.Cols["Select"].Index).ToString().ToLower() == "true")
                        {
                            if (IsBusinessCenterEnable)
                            {
                                if (c1PatientList.Rows[i]["IsBusinessCenterAssociated"] != null)
                                {
                                    if (Convert.ToBoolean(c1PatientList.Rows[i]["IsBusinessCenterAssociated"]) == false)
                                    {
                                        arBusinessCenter.Add(c1PatientList.Rows[i]["sAccountNo"].ToString());
                                    }
                                }
                            }

                            if (Convert.ToString(c1PatientList.Rows[i]["sAddress"]).Trim() == "")
                            {
                                arMissingAddress.Add(c1PatientList.Rows[i]["sAccountNo"].ToString());
                            }
                        }
                    }

                    if (arBusinessCenter.Count > 0)
                    {
                        MessageBox.Show("Following accounts are not assigned to a Business Center. Statements will not be generated for those accounts." + Environment.NewLine + string.Join(",", arBusinessCenter.ToArray()), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                    if (arMissingAddress.Count > 0)
                    {
                        DialogResult oResult = MessageBox.Show("Guarantor address is missing for Acct.# " + string.Join(",", arMissingAddress.ToArray()) + Environment.NewLine + "Continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (oResult == System.Windows.Forms.DialogResult.No)
                        {
                            return false;
                        }
                    }
                }
                else // Validation on generating individual statement if no Business Center Associated
                {
                    if (Convert.ToString(cmbAccounts.SelectedValue) != "" && Convert.ToString(cmbAccounts.SelectedValue) != "0")
                    {
                        if (IsBusinessCenterEnable)
                        {
                            if (!ObjClsgloStatment.IsBusinessCenterAssociated(Convert.ToInt64(cmbAccounts.SelectedValue)))
                            {
                                MessageBox.Show("Account statement may not be generated.  Account is not assigned to a Business Center.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                        }

                        if (!objgloAccount.ChecktAccountAddressAvailable(Convert.ToInt64(cmbAccounts.SelectedValue)))
                        {
                            DialogResult oResult = MessageBox.Show("Guarantor address is missing for Acct.# " + Convert.ToString(cmbAccounts.Text) + Environment.NewLine + "Continue?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (oResult == System.Windows.Forms.DialogResult.No)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at Statement validation : " + ex.ToString(), false);
                return false;
            }
            finally
            {
                if (ObjClsgloStatment != null)
                { ObjClsgloStatment.Dispose(); }

                if (objgloAccount != null)
                { objgloAccount.Dispose(); }

                arBusinessCenter = null;
                arMissingAddress = null;
            }

            return isValidated;
        }

        private bool ExcludeFromStatement()
        {
            bool isValidated = false;

            Int64 patientID = 0;
            Int64 accountID = 0;

            if (IsIndvidualView)
            {
                patientID = Convert.ToInt64(cmbPatients.SelectedValue);
                accountID = Convert.ToInt64(cmbAccounts.SelectedValue);
            }
            else
            {
                if (c1PatientList.DataSource != null)
                {
                    if (c1PatientList.Rows.Count > 1)
                    {
                        if (Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["nPAccountID"].Index)) != "")
                        {
                            accountID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["nPAccountID"].Index));
                        }
                        patientID = 0;
                    }
                }
            }

            bool isExcluded = objClsgloStatment.ExcludeFromStatment(accountID);
            if (isExcluded)
            {
                if (MessageBox.Show(((patientID == 0) ? "Account" : "Patient") + " is marked to suppress Statements.\nContinue with a new Statement?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    isValidated = false;

                    pnlcrvReportViewer.BackColor = Color.White;

                    if (crvReportViewer.ReportSource == null)
                    { tsb_Send.Enabled = false; }
                }
                else
                {
                    isValidated = true;
                    tsb_Send.Enabled = true;
                    SetButtonVisibility("Individual");
                }
            }
            else
            {
                isValidated = true;
                tsb_Send.Enabled = true;
            }

            return isValidated;
        }

        private bool ValidateStatementDates()
        {
            bool isValidated = false;
            bool isDayClosed = false;
            DateTime UncloseDate = DateTime.Now;

            LastCloseDate = objClsgloStatment.getCloseDate();

            if (LastCloseDate == "")
            { 
                LastCloseDate = System.DateTime.Now.ToShortDateString(); 
            }

            if (IsIndvidualView)
            {
                isDayClosed = objClsgloStatment.IsDayClosed(gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()));
                UncloseDate = dtpEndDate.Value;
            }
            else
            {
                isDayClosed = objClsgloStatment.IsDayClosed(gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString()));
                UncloseDate = dtCriteriaEndDate.Value;
            }

            // If allow statements for unclosed days setting is not turned on
            if (!objClsgloStatment.IsGenerateStatementOnUnclosedDay() && !isDayClosed) 
            {
                if (IsIndvidualView && LastCloseDate != "")
                {
                    // If closed days are available
                    if (Convert.ToDateTime(UncloseDate.ToShortDateString()) > Convert.ToDateTime(LastCloseDate)) 
                    {
                        MessageBox.Show(UncloseDate.ToShortDateString() + " has not been daily closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    { 
                        isValidated = true; 
                    }
                }
                else if (!IsIndvidualView && !isDayClosed)
                {
                    MessageBox.Show(UncloseDate.ToShortDateString() + " has not been daily closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(UncloseDate.ToShortDateString() + " has not been daily closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            // If allow statements for unclosed days setting is turned on
            else 
            {
                if (Convert.ToDateTime(UncloseDate.ToShortDateString()) > Convert.ToDateTime(LastCloseDate) && !isDayClosed) // If closed days are available
                {
                    if (MessageBox.Show("Warning – Statement Date has not been daily closed. This could mean the financial information on the account may not been reviewed and could change after the statement has been generated.\nContinue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        IsUnclosedDay = true;
                        isValidated = true;
                    }
                }
                else if (LastCloseDate == "")// In case there is no closed day in system 
                {
                    if (MessageBox.Show("Warning – Statement Date has not been daily closed. This could mean the financial information on the account may not been reviewed and could change after the statement has been generated.\nContinue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        IsUnclosedDay = true;
                        isValidated = true;
                    }
                }
                else
                {
                    isValidated = true;
                }
            }
            return isValidated;
        }

        private bool ValidateData()
        {
            Boolean _result = false;
            bool IsDayClosed = false;
            DateTime _dtUnclosedDate = DateTime.Now;
            string _dtLastClosedDate = objClsgloStatment.getCloseDate();
            LastCloseDate = objClsgloStatment.getCloseDate();
            if (LastCloseDate == "")
            {
                LastCloseDate = System.DateTime.Now.ToShortDateString();
                //CloseDateNotFound = true;
            }
            // Check whether selected day is closed or not
            if (IsIndvidualView)
            {
                IsDayClosed = objClsgloStatment.IsDayClosed(gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()));
                _dtUnclosedDate = dtpEndDate.Value;
            }
            else
            {
                IsDayClosed = objClsgloStatment.IsDayClosed(gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString()));
                _dtUnclosedDate = dtCriteriaEndDate.Value;
            }
            // Check whether selected day is closed or not

            if (!objClsgloStatment.IsGenerateStatementOnUnclosedDay() && !IsDayClosed) // If allow statements for unclosed days setting is not turned on
            {
                if (IsIndvidualView && _dtLastClosedDate != "")
                {
                    if (Convert.ToDateTime(_dtUnclosedDate.ToShortDateString()) > Convert.ToDateTime(_dtLastClosedDate)) // If closed days are available
                    {
                        MessageBox.Show(_dtUnclosedDate.ToShortDateString() + " has not been daily closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    { _result = true; }
                }
                else if (!IsIndvidualView && !IsDayClosed)
                {
                    MessageBox.Show(_dtUnclosedDate.ToShortDateString() + " has not been daily closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(_dtUnclosedDate.ToShortDateString() + " has not been daily closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else // If allow statements for unclosed days setting is turned on
            {
                if (Convert.ToDateTime(_dtUnclosedDate.ToShortDateString()) > Convert.ToDateTime(LastCloseDate) && !IsDayClosed) // If closed days are available
                {
                    if (MessageBox.Show("Warning – Statement Date has not been daily closed. This could mean the financial information on the account may not been reviewed and could change after the statement has been generated.\nContinue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        IsUnclosedDay = true;
                        _result = true;
                    }
                }
                else if (_dtLastClosedDate == "")// In case there is no closed day in system 
                {
                    if (MessageBox.Show("Warning – Statement Date has not been daily closed. This could mean the financial information on the account may not been reviewed and could change after the statement has been generated.\nContinue? ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        IsUnclosedDay = true;
                        _result = true;
                    }
                }
                else
                {
                    _result = true;
                }
            }
            return _result;
        }

        #endregion " Date and Other Validations Methods "

        #region "Change Events"

        private void btnDown_MouseHover(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloBilling.Properties.Resources.DownHover;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown_MouseLeave(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloBilling.Properties.Resources.Down;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnUp_MouseHover(object sender, EventArgs e)
        {
            btnUp.BackgroundImage = global::gloBilling.Properties.Resources.UPHover;
            btnUp.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnUp_MouseLeave(object sender, EventArgs e)
        {
            btnUp.BackgroundImage = global::gloBilling.Properties.Resources.UP;
            btnUp.BackgroundImageLayout = ImageLayout.Center;
        }

        #endregion

        #region "Tool Tip Methods and events"

        void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        {

            try
            {
                combo = (ComboBox)sender;
                if (combo != null)
                {
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
                                    this.toolTip1_Rpt.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 180, e.Bounds.Bottom);
                            }
                            else
                            {
                                toolTip1_Rpt.Hide(combo);
                            }
                        }
                        else
                        {
                            toolTip1_Rpt.Hide(combo);
                        }
                        e.DrawFocusRectangle();
                    }
                }
            }
            catch (Exception ex)
            {
                
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g != null)
            {
                SizeF s = g.MeasureString(_text, cmbSettings.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }

            return width;
        }

        private void cmbBusinessCenter_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                combo = cmbBusinessCenter;
                if (Convert.ToString(cmbBusinessCenter.SelectedValue) != "" && Convert.ToString(cmbBusinessCenter.SelectedValue) != "0")
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]), cmbBusinessCenter) >= cmbBusinessCenter.DropDownWidth - 20)
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbBusinessCenter, Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbBusinessCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                combo = cmbBusinessCenter;
                if (Convert.ToString(cmbBusinessCenter.SelectedValue) != "" && Convert.ToString(cmbBusinessCenter.SelectedValue) != "0")
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]), cmbBusinessCenter) >= cmbBusinessCenter.DropDownWidth - 20)
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbBusinessCenter, Convert.ToString(((DataRowView)cmbBusinessCenter.Items[cmbBusinessCenter.SelectedIndex])["BusinessCenter"]));
                    }
                    else
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbBusinessCenter, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbAccounts_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                combo = cmbAccounts;
                if (cmbAccounts.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbAccounts.Items[cmbAccounts.SelectedIndex])["sAccount"]), cmbAccounts) >= cmbAccounts.DropDownWidth - 20)
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbAccounts, Convert.ToString(((DataRowView)cmbAccounts.Items[cmbAccounts.SelectedIndex])["sAccount"]));
                    }
                    else
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbAccounts, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbSettings_MouseEnter(object sender, EventArgs e)
        {

            try
            {
                combo = cmbSettings;
                if (cmbSettings.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbSettings.Items[cmbSettings.SelectedIndex])["sStatementCriteriaName"]), cmbSettings) >= cmbSettings.DropDownWidth - 20)
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbSettings, Convert.ToString(((DataRowView)cmbSettings.Items[cmbSettings.SelectedIndex])["sStatementCriteriaName"]));
                    }
                    else
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbSettings, "");
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbPatients_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                combo = cmbPatients;
                if (cmbPatients.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbPatients.Items[cmbPatients.SelectedIndex])["PatientDisplayName"]), cmbPatients) >= cmbPatients.DropDownWidth - 20)
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbPatients, Convert.ToString(((DataRowView)cmbPatients.Items[cmbPatients.SelectedIndex])["PatientDisplayName"]));
                    }
                    else
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbPatients, "");
                    }
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        #endregion "Tool Tip Methods events"

        #region "C1 FlexGrid Events"
        private void c1PatientList_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            C1.Win.C1FlexGrid.HitTestInfo ht = c1PatientList.HitTest(e.Location);
            try
            {
                if (ht.Row > 0)
                {
                    ViewStatement();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

            //C1.Win.C1FlexGrid.HitTestInfo ht = c1PatientList.HitTest(e.Location);
            //try
            //{
            //    if (ht.Row > 0)
            //    {
            //        if (_isGenerateBatch && c1PatientList.Rows.Count > 1)
            //        {
            //            tsb_btnShowList.Visible = true;
            //            tsb_PatAcctAccount.Visible = false;
            //        }
            //        else
            //        {
            //            tsb_btnShowList.Visible = false;
            //            tsb_PatAcctAccount.Visible = true;
            //        }

            //        tsb_GenerateBatch.Visible = false;
            //        if (ValidateStatementDates())
            //        {
            //            if (LastCloseDate == "")
            //            {
            //                MessageBox.Show(Convert.ToDateTime(dtCriteriaEndDate.Value).ToShortDateString() + " has not been Daily Closed.  Please select a closed day. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                dtCriteriaEndDate.Text = LastCloseDate;
            //                tsb_GenerateBatch_Click(null, null);
            //                tsb_GenerateBatch.Visible = true;

            //            }
            //            if (LastCloseDate != "")
            //            {
            //                if (Convert.ToDateTime(dtCriteriaEndDate.Value.ToShortDateString()) > Convert.ToDateTime(LastCloseDate))
            //                {
            //                    Int64 nPatientID = 0;
            //                    Int64 nAcccountID = 0;
            //                    btnUp_Click(null, null);
            //                    if (c1PatientList.DataSource != null)
            //                    {
            //                        if (IsIndvidualView == false)
            //                        {
            //                            if (c1PatientList.Rows.Count > 1)
            //                            {
            //                                nPatientID = 0;
            //                                if (c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["nPAccountID"].Index) != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["nPAccountID"].Index).ToString() != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString().Trim() != "")
            //                                {
            //                                    nAcccountID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["nPAccountID"].Index).ToString());
            //                                }
            //                                //fillRevisedPatientStatement(nPatientID, nAcccountID);
            //                            }
            //                        }

            //                    }
            //                    else
            //                    {
            //                        if (c1PatientList.Rows.Count > 1) { tsb_Send.Visible = true; }
            //                        tsb_GenerateBatch.Visible = true;
            //                        tsb_btnShowList.Visible = false;
            //                    }
            //                }

            //                else
            //                {
            //                    Int64 nPatientID = 0;
            //                    Int64 nAcccountID = 0;
            //                    btnUp_Click(null, null);
            //                    if (c1PatientList.DataSource != null)
            //                    {
            //                        if (c1PatientList.DataSource != null)
            //                        {
            //                            if (IsIndvidualView == false)
            //                            {
            //                                if (c1PatientList.Rows.Count > 1)
            //                                {
            //                                    nPatientID = 0;
            //                                    if (c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["nPAccountID"].Index) != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["nPAccountID"].Index).ToString() != null && c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["PatientID"].Index).ToString().Trim() != "")
            //                                    {
            //                                        nAcccountID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["nPAccountID"].Index).ToString());
            //                                    }

            //                                    //fillRevisedPatientStatement(nPatientID, nAcccountID);

            //                                }
            //                            }
            //                        }
            //                    }
            //                }

            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //}

        }

        private void setGridStyle(C1FlexGrid C1Flex, Int32 iRowNumber, Int32 iColNumber, Int32 iColCount)
        {

            CellStyle csSubTotalRow;

          //  csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
            try
            {
                if (C1Flex.Styles.Contains("SubTotalRow"))
                {
                    csSubTotalRow = C1Flex.Styles["SubTotalRow"];
                }
                else
                {
                    csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
                    csSubTotalRow.Format = "c";
                    csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);
                    csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                    csSubTotalRow.ForeColor = Color.Blue;
                    csSubTotalRow.TextAlign = TextAlignEnum.RightCenter;
                }

            }
            catch
            {
                csSubTotalRow = C1Flex.Styles.Add("SubTotalRow");
                csSubTotalRow.Format = "c";
                csSubTotalRow.BackColor = Color.FromArgb(255, 255, 255);
                csSubTotalRow.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                csSubTotalRow.TextEffect = TextEffectEnum.Flat;
                csSubTotalRow.ForeColor = Color.Blue;
                csSubTotalRow.TextAlign = TextAlignEnum.RightCenter;

            }
       
            CellRange subTotalRange;
            subTotalRange = C1Flex.GetCellRange(iRowNumber, 0, iRowNumber, iColCount);
            subTotalRange.Style = csSubTotalRow;
        }
        #endregion "C1 FlexGrid Events"

        #region "Account Related Mehtods and Events"

        private void FillAccountsDetails(Int64 _nPatientID, Int64 accountIdToDefault = 0)
        {
            DataTable dtAccounts = null;

            try
            {
                if (IsBusinessCenterEnable)
                    dtAccounts = objClsgloStatment.FillAccounts(_nPatientID);
                else
                    dtAccounts = objClsgloStatment.FillAccountsForBCFeatureDisabled(_nPatientID);
                if (dtAccounts != null && dtAccounts.Rows.Count > 0)
                {
                    _cmbAccountLoadFlag = false;
                    cmbAccounts.ValueMember = dtAccounts.Columns["nPAccountID"].ColumnName;
                    cmbAccounts.DisplayMember = dtAccounts.Columns["sAccount"].ColumnName; ;
                    cmbAccounts.DataSource = dtAccounts.Copy();
                    if (accountIdToDefault != 0) { cmbAccounts.SelectedValue = accountIdToDefault; }
                    _cmbAccountLoadFlag = true;
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);

            }
            finally
            {
                if (dtAccounts != null) { dtAccounts.Dispose(); dtAccounts = null; }
            }
        }

        private void FillPatientDetails(Int64 _nPAccountID)
        {
            DataTable dtPatients = new DataTable();

            try
            {
                dtPatients = objClsgloStatment.FillPatients(_nPAccountID);
                if (dtPatients != null && dtPatients.Rows.Count > 0)
                {
                    _cmbPatientLoadFlag = false;

                    if (IsPatientAccountEnable == true)
                    {
                        if (dtPatients != null && dtPatients.Rows.Count > 1)
                        {
                            DataRow oNewRow = dtPatients.NewRow();
                            oNewRow["PatientDisplayName"] = "All Acct Patients";
                            oNewRow["nPatientId"] = 0;
                            dtPatients.Rows.InsertAt(oNewRow, 0);
                            dtPatients.AcceptChanges();

                            if (oNewRow != null) { oNewRow = null; }
                        }
                    }

                    // merging changes from 7007 - Incident# - 4613 (Hot fix (DLL) given for 7006 version)
                    cmbPatients.ValueMember = dtPatients.Columns["nPatientId"].ColumnName;
                    cmbPatients.DisplayMember = dtPatients.Columns["PatientDisplayName"].ColumnName; ;
                    cmbPatients.DataSource = dtPatients.Copy();

                    _cmbPatientLoadFlag = true;
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
            }
            finally
            {
                if (dtPatients != null) { dtPatients.Dispose(); dtPatients = null; }
            }

        }

        private void cmbPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_cmbPatientLoadFlag == true)
                {
                    if (Convert.ToInt64(cmbPatients.SelectedValue) != 0)
                    {
                        Int64 prevAccountId = Convert.ToInt64(cmbAccounts.SelectedValue);
                        FillAccountsDetails(Convert.ToInt64(cmbPatients.SelectedValue));
                        _cmbAccountLoadFlag = false;
                        cmbAccounts.SelectedValue = prevAccountId;
                        _cmbAccountLoadFlag = true;
                        FillIndividualBatchSummary();

                    }
                    else
                    {
                        FillIndividualBatchSummary();
                    }
                }
                combo = cmbPatients;
                if (cmbPatients.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbPatients.Items[cmbPatients.SelectedIndex])["PatientDisplayName"]), cmbPatients) >= cmbPatients.DropDownWidth - 20)
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbPatients, Convert.ToString(((DataRowView)cmbPatients.Items[cmbPatients.SelectedIndex])["PatientDisplayName"]));
                    }
                    else
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbPatients, "");
                    }
                }
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
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch { }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_AccountSelectClick);
                        }
                        catch { }
                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch { }
                        try
                        {
                            oListControl.chkShowActpatient_Change -= new gloListControl.gloListControl.chkShowActpatientChange(chkSelectallPatient_Change);
                        }
                        catch { }
                    }
                    catch { }
                  
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);

            }
        }

        private void cmbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_cmbAccountLoadFlag == true)
                {

                    if (Convert.ToInt64(cmbAccounts.SelectedValue) != 0)
                    {
                        Int64 prevPatientID = Convert.ToInt64(cmbPatients.SelectedValue);
                        FillPatientDetails(Convert.ToInt64(cmbAccounts.SelectedValue));
                        if (cmbPatients.Items.Count > 1)
                        {
                            _cmbPatientLoadFlag = false;
                            cmbPatients.SelectedIndex = 0;
                            _cmbPatientLoadFlag = true;
                        }
                        else
                        {
                            if (prevPatientID != 0)
                            {
                                _cmbPatientLoadFlag = false;
                                cmbPatients.SelectedValue = prevPatientID;
                                _cmbPatientLoadFlag = true;
                            }
                            else
                            {
                                _cmbPatientLoadFlag = false;
                                cmbPatients.SelectedIndex = 0;
                                _cmbPatientLoadFlag = true;
                            }
                        }
                        FillIndividualBatchSummary();
                    }
                }

                combo = cmbAccounts;
                if (cmbAccounts.SelectedItem != null)
                {
                    if (getWidthofListItems(Convert.ToString(((DataRowView)cmbAccounts.Items[cmbAccounts.SelectedIndex])["sAccount"]), cmbAccounts) >= cmbAccounts.DropDownWidth - 20)
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbAccounts, Convert.ToString(((DataRowView)cmbAccounts.Items[cmbAccounts.SelectedIndex])["sAccount"]));
                    }
                    else
                    {
                        this.toolTip1_Rpt.SetToolTip(cmbAccounts, "");
                    }
                }
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
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch { }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_AccountSelectClick);
                        }
                        catch { }
                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch { }
                        try
                        {
                            oListControl.chkShowActpatient_Change -= new gloListControl.gloListControl.chkShowActpatientChange(chkSelectallPatient_Change);
                        }
                        catch { }
                    }
                    catch { }
                   
                }

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);

            }
        }

        private void btnBrowseAcount_Click(object sender, EventArgs e)
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
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                        }
                        catch { }
                        try
                        {
                            oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_AccountSelectClick);
                        }
                        catch { }
                        try
                        {
                            oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                        }
                        catch { }
                        try
                        {
                            oListControl.chkShowActpatient_Change -= new gloListControl.gloListControl.chkShowActpatientChange(chkSelectallPatient_Change);
                        }
                        catch { }
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.GuarantorsAccounts, false, this.Width);
                oListControl.ControlHeader = "Accounts";
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_AccountSelectClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                oListControl.OpenControl();

                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
            }
        }

        private void oListControl_AccountSelectClick(object sender, EventArgs e)
        {
            this.cmbAccounts.SelectedIndexChanged -= new System.EventHandler(this.cmbAccounts_SelectedIndexChanged);
            this.cmbPatients.SelectedIndexChanged -= new System.EventHandler(this.cmbPatients_SelectedIndexChanged);

            try
            {
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {

                        //Fill patients of selected account
                        if (Convert.ToInt64(oListControl.SelectedItems[i].ID) != 0)
                        {
                            FillPatientDetails(Convert.ToInt64(oListControl.SelectedItems[i].ID));
                        }
                        //If patientaccount having morethan one patient then default to All Acct Patients
                        if (cmbPatients.Items.Count > 1)
                        {
                            _cmbPatientLoadFlag = false;
                            cmbPatients.SelectedIndex = 0;
                            _cmbPatientLoadFlag = true;
                        }
                        else //else default to that patient.
                        {
                            //Set Patient Index
                            _cmbPatientLoadFlag = false;
                            cmbPatients.SelectedIndex = 0;
                            _cmbPatientLoadFlag = true;
                        }

                        //Fill Accounts  of selecteted patient
                        if (cmbPatients.SelectedValue.ToString() != "" && Convert.ToInt64(cmbPatients.SelectedValue) != 0)
                        {
                            FillAccountsDetails(Convert.ToInt64(cmbPatients.SelectedValue));
                        }
                        else
                        {
                            //DataTable dtAccountDetails = null;
                            DataTable dtAccounts = new DataTable();

                            try
                            {
                                //dtAccounts.Columns.Add("sAccount");
                                //dtAccounts.Columns.Add("nPAccountID");
                                //DataRow dr = dtAccounts.NewRow();

                                objgloAccount = new gloAccount(_databaseconnectionstring);
                                
                                //if (!IsBusinessCenterEnable)
                                //{
                                //    dtAccountDetails = objgloAccount.GetAccountDetailsById(Convert.ToInt64(oListControl.SelectedItems[0].ID));
                                //    //get AccountDetails by AccountID
                                //    if (dtAccountDetails != null && dtAccountDetails.Rows.Count > 0)
                                //    {
                                //        dr["sAccount"] = dtAccountDetails.Rows[0]["sAccountNo"].ToString() + "-" + dtAccountDetails.Rows[0]["sFirstName"].ToString() + ' ' + dtAccountDetails.Rows[0]["sLastName"].ToString();
                                //    }
                                //}

                                dtAccounts = objgloAccount.GetAccountForStatement(Convert.ToInt64(oListControl.SelectedItems[0].ID));

                                
                                //dr["nPAccountID"] = oListControl.SelectedItems[0].ID; ;
                                //dtAccounts.Rows.Add(dr);

                                if (dtAccounts != null && dtAccounts.Rows.Count > 0)
                                {
                                    _cmbAccountLoadFlag = false;
                                    cmbAccounts.ValueMember = dtAccounts.Columns["nPAccountID"].ColumnName;
                                    cmbAccounts.DisplayMember = dtAccounts.Columns["sAccount"].ColumnName; ;
                                    cmbAccounts.DataSource = dtAccounts.Copy();
                                    _cmbAccountLoadFlag = true;
                                }
                            }
                            catch (Exception Ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);

                            }
                            finally
                            {
                                if (objgloAccount != null) { objgloAccount.Dispose(); objgloAccount = null; }
                                //if (dtAccountDetails != null) { dtAccountDetails.Dispose(); dtAccountDetails = null; }
                                if (dtAccounts != null) { dtAccounts.Dispose(); dtAccounts = null; }
                            }
                        }
                        FillIndividualBatchSummary();

                    }
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);

            }
            finally
            {
                this.cmbAccounts.SelectedIndexChanged += new System.EventHandler(this.cmbAccounts_SelectedIndexChanged);
                this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
            }
        }

        private bool GetPatientPortalStatementNotificationSetting()
        {
            bool result = false;
            
            DataTable dtValue = null;
            clsgloPatientPortalEmail objclsgloPatientPortalEmail=null;
            try
            {
                if (!String.IsNullOrWhiteSpace(_databaseconnectionstring))
                {
                    objclsgloPatientPortalEmail = new clsgloPatientPortalEmail(_databaseconnectionstring);

                    dtValue = objclsgloPatientPortalEmail.GetPortalStatmentNotificationStatus();
                    if (dtValue != null & dtValue.Rows.Count > 0)
                    {
                        result = Convert.ToBoolean(dtValue.Rows[0][0]);
                    }
                    dtValue = null;
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at ReadStatentNotificationSetting : " + ex.ToString(), false);

            }
            finally 
            {
                if (dtValue != null)
                {
                    dtValue.Dispose();
                    dtValue = null;
                }                
                  objclsgloPatientPortalEmail = null;
            }
            return result;
        }
        #endregion "Account Related Mehtods and Events"


        private void c1PatientList_Click(object sender, EventArgs e)
        {

        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x201 || m.Msg == 0x202 || m.Msg == 0x203) return true;
            if (m.Msg == 0x204 || m.Msg == 0x205 || m.Msg == 0x206) return true;
            return false;
        }

        private void DisableMouse()
        {
            //this.Cursor = new Cursor(Cursor.Current.Handle);
            //int y = Cursor.Position.Y;
            //_x = Cursor.Position.X;
            //_y = Cursor.Position.Y;

            Oldpostion = Cursor.Clip;
            // Arbitrary location.
            Rectangle BoundRect = new Rectangle(_x, _y, 1, 1);
            Cursor.Clip = BoundRect;
            Cursor.Hide();
            Application.AddMessageFilter(this);
        }

        private void EnableMouse()
        {
            Cursor.Clip = Oldpostion;
            Cursor.Position = new Point(_x, _y);
            Cursor.Show();

            Application.RemoveMessageFilter(this);
        }

        private void c1PatientList_AfterEdit(object sender, RowColEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            c1PatientList.Redraw = false;
            if (e.Row == 0 && e.Col == 0)
            {
                //c1PatientList.EnterCell -= new System.EventHandler(this.c1PALogView_EnterCell);
                int i = 0;
                if (c1PatientList.GetCellCheck(e.Row, e.Col) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                {
                    for (i = 1; i <= c1PatientList.Rows.Count - 1; i++)
                    {
                        c1PatientList.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    }

                }
                else
                {
                    for (i = 1; i <= c1PatientList.Rows.Count - 1; i++)
                    {
                        c1PatientList.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    }
                }
                //c1PatientList.EnterCell += new System.EventHandler(this.c1PALogView_EnterCell);
            }
            else
            {
                if (c1PatientList.Rows.Count > 1)
                {
                    int i = 0;
                    i = c1PatientList.FindRow("False", 1, 0, false);
                    if (i > 0)
                    {
                        c1PatientList.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    }
                    else
                    {
                        c1PatientList.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                    }
                }
            }

            c1PatientList.Redraw = true;
            Cursor.Current = Cursors.Default;

        }

        private void btnClearBusinessCenter_Click(object sender, EventArgs e)
        {
            //cmbBusinessCenter.DataSource = null;
            //cmbBusinessCenter.Items.Clear();
            //cmbBusinessCenter.Refresh();
            cmbBusinessCenter.SelectedValue = 0;
        }


        #region "Statement Revised functions"
        public class StatementTotals : IDisposable
        {
            private bool disposed = false;

            public StatementTotals()
            { }

            public decimal totalStatementChargePatientDue { get; set; }
            public decimal totalStatementChargeInsuranceDue { get; set; }
            public decimal totalStatementChargeAmount { get; set; }
            public decimal totalStatementPaymentAndAdjustments { get; set; }
            public decimal statementAvailableReserve { get; set; }
            public decimal totalStatementPatientDue { get; set; }

            #region IDisposable Members

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
                //throw new NotImplementedException();
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }
            #endregion

        }

        private enum StatementAction
        {
            ViewOnly,
            SendOnly,
            PrintOnly
        }

        private Int64 GetBusinessCenterID()
        {
            Int64 businessCenterID = 0;

            if (!IsIndvidualView)
            {
                if (Convert.ToString(cmbBusinessCenter.SelectedValue) != "" && Convert.ToString(cmbBusinessCenter.SelectedValue) != "0")
                { businessCenterID = Convert.ToInt64(cmbBusinessCenter.SelectedValue); }
            }
            else
            {
                if (Convert.ToString(cmbAccounts.SelectedValue) != "" && Convert.ToString(cmbAccounts.SelectedValue) != "0")
                {
                    Int64 _nPAccountID = Convert.ToInt64(cmbAccounts.SelectedValue);
                    businessCenterID = gloGlobal.gloPMGlobal.GetAccountBusinessCenterID(_nPAccountID);
                }
            }
            return businessCenterID;
        }

        private int GetStatementDate()
        {
            int statementDate = 0;

            if (IsIndvidualView)
            { statementDate = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()); }
            else
            { statementDate = gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString()); }

            return statementDate;
        }

        private Int64 GetStatementCriteriaID(Int64 businessCenterID)
        {
            Int64 criteriaID = 0;

            gloStatment ObjClsgloStatment = new gloStatment();
            try
            {
                if (IsBusinessCenterEnable && businessCenterID > 0)
                { criteriaID = ObjClsgloStatment.GetBusinessCenterDisplaySettings(businessCenterID); }
                else
                { criteriaID = ObjClsgloStatment.GetStatmentCriteriaID(); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at GetStatementCriteriaID : " + ex.ToString(), false);
            }
            finally
            {
                if (ObjClsgloStatment != null)
                {
                    ObjClsgloStatment.Dispose();
                    ObjClsgloStatment = null;
                }
            }
            return criteriaID;
        }

        private decimal GetDueAmount(Int64 AccountID)
        {
            decimal dueAmount = 0;

            try
            {
                if (IsIndvidualView)
                {
                    if ((dsPatientStatementMain.Tables["dt_PatientReserve"] != null) && (dsPatientStatementMain.Tables["dt_PatientReserve"].Rows.Count > 0))
                    { dueAmount = Convert.ToDecimal(dsPatientStatementMain.Tables["dt_PatientReserve"].Rows[0]["PatientDue"]) - Convert.ToDecimal(dsPatientStatementMain.Tables["dt_PatientReserve"].Rows[0]["AvailableReserve"]); }
                }
                else
                {
                    if (c1PatientList.DataSource != null)
                    {
                        using (DataTable dtPatientDue = ((DataView)c1PatientList.DataSource).Table)
                        {
                            if (dtPatientDue != null)
                            {
                                DataRow dr = dtPatientDue.Select("nPAccountID =" + AccountID.ToString()).FirstOrDefault();
                                if (dr != null)
                                {
                                    if (IsPatientAccountEnable)
                                    { dueAmount = Convert.ToDecimal(dr["sAccountDue"]); }
                                    else
                                    { dueAmount = Convert.ToDecimal(dr["sPatientDue"]); }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at GetDueAmount : " + ex.ToString(), false);
            }
            finally
            {

            }

            return dueAmount;
        }

        //Bug #61582: gloPM - Patient Statement- Application does not create new folder for each statement/batch at server path in claim management folder 
        //Description: Change parameter from BatchName to BatchID
        private string GetStatementFileName(Int64 BatchID)
        {
            gloStatment objClsgloStatment = new gloStatment();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            
            //Bug #61582: gloPM - Patient Statement- Application does not create new folder for each statement/batch at server path in claim management folder 
            //Description: Find the batch name using batchID passed and then use that batch name for for creating the folder.
            string BatchName = string.Empty;
            string _sqlQuery = "select sBatchName from dbo.BL_Batch_PatientStatement_Mst WITH (NOLOCK) where dbo.BL_Batch_PatientStatement_Mst.nBatchPateintStatMstID = " + BatchID;
            oDB.Connect(false);
            BatchName = oDB.ExecuteScalar_Query(_sqlQuery).ToString();

            string statementFileExtension = objClsgloStatment.getStatementFileExtension();
            string serverPath = objClsgloStatment.GetServerPath();

            string baseFolder = "Claim Management";
            string outboxFolder = "OutBox";
            string claimFolder = "Statements";
            string claimFolderPath = serverPath + "\\" + baseFolder + "\\" + outboxFolder + "\\" + claimFolder;

            string filePath = gloSettings.FolderSettings.AppTempFolderPath + BatchName + statementFileExtension;

            try
            {
                if (System.IO.Directory.Exists(claimFolderPath) == false)
                { System.IO.Directory.CreateDirectory(claimFolderPath); }

                if (objClsgloStatment.getCopyEDIFiles() == 1)
                {

                    string _BatchFolderPath = claimFolderPath + "\\" + BatchName;
                    if (System.IO.Directory.Exists(_BatchFolderPath) == false)
                    {
                        System.IO.Directory.CreateDirectory(_BatchFolderPath);
                    }
                    filePath = _BatchFolderPath + "\\" + BatchName + statementFileExtension;
                }

            }
            catch (Exception EX)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(EX.ToString(), false);
                EX = null;
            }
            finally
            {
                if (objClsgloStatment != null) { objClsgloStatment.Dispose(); objClsgloStatment = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            return filePath;
        }

        private int GetStatementCount(Int64 AccountID, Int32 Statementdate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTemp = new DataTable();
            object statementcount = null;
            int iCount = 0;
            try
            {
                oDB.Connect(false);
                string _sqlQuery = "select dbo.PA_Get_StatementCount_V2(" + AccountID + ",'" + gloDateMaster.gloDate.DateAsDate(Statementdate) + "')";
                statementcount = oDB.ExecuteScalar_Query(_sqlQuery);
                iCount = Convert.ToInt32(statementcount);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (statementcount != null) { statementcount = null; }
                if (dtTemp != null) { dtTemp.Dispose(); dtTemp = null; }
            }

            return iCount;
        }

        private void BusinessCenterValidation()
        {
            // If Business center feature is enabled then While generating electronic Batch if account(s) does not associated 
            // with business center and included (Checked in batch view) in batch then restrict to generate electronic batch.
            if (IsBusinessCenterEnable)
            {
                if (!IsIndvidualView)
                {
                    for (int i = 1; i < c1PatientList.Rows.Count; i++)
                    {
                        if (c1PatientList.GetData(i, c1PatientList.Cols["Select"].Index).ToString().ToLower() == "true")
                        {
                            if (c1PatientList.Rows[i]["IsBusinessCenterAssociated"] != null)
                            {
                                if (Convert.ToBoolean(c1PatientList.Rows[i]["IsBusinessCenterAssociated"]) == false) // Checked for Column [IsBusinessCenterAssociated]
                                {
                                    MessageBox.Show("Some accounts are not assigned to a Business Center. Statements will not be generated for those accounts.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }
                }
                else // Validation on generating individual statement if no Business Center Associated
                {
                    gloStatment ObjClsgloStatment = new gloStatment();
                    if (Convert.ToString(cmbAccounts.SelectedValue) != "" && Convert.ToString(cmbAccounts.SelectedValue) != "0")
                    {
                        if (!ObjClsgloStatment.IsBusinessCenterAssociated(Convert.ToInt64(cmbAccounts.SelectedValue)))
                        {
                            MessageBox.Show("Account statement may not be generated.  Account is not assigned to a Business Center.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    if (ObjClsgloStatment != null)
                    { ObjClsgloStatment.Dispose(); }
                }
            }
        }

        private Int64 CreateBatchMaster(out string BatchName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Int64 _nResult = 0;

            try
            {
                //Master Batch Entry in BL_Batch_PatientStatement_Mst table to maintain statement generated history.....
                oDB.Connect(false);
                string sBatchName = "";

                if (IsIndvidualView == false)
                { sBatchName = cmbSettings.Text.ToString() + " " + gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString()); }
                else
                {
                    if (Convert.ToInt64(cmbPatients.SelectedValue) != 0)//Indicates Patient
                    { sBatchName = cmbPatients.Text.Trim().ToString().Split('-')[1] + " " + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()); }
                    else //Indicates Account
                    { sBatchName = cmbAccounts.Text.Trim().ToString().Split('-')[1] + " " + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + "_Acc"; }
                }

                if (IsIndvidualView == false)
                {
                    oDBParameters.Add("@nBatchPateintStatMstID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@nPatientID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@dtCreateDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBParameters.Add("@nSettingID", cmbSettings.SelectedValue, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sSettingName", cmbSettings.Text.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@sBatchName", sBatchName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@nUserID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@dtStatementDate", dtCriteriaEndDate.Value, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBParameters.Add("@nCrWaitDays", 30, ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@nCrBalance", Convert.ToDecimal(lblDueAmt.Text.ToString()), ParameterDirection.Input, SqlDbType.Decimal);
                    oDBParameters.Add("@sCrPateintFromName", lblNameFrom.Text.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@sCrPateintToName", lblNameTo.Text.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@bStatus", true, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@bIsUnclosedDay", IsUnclosedDay, ParameterDirection.Input, SqlDbType.Bit);
                }
                else
                {
                    oDBParameters.Add("@nBatchPateintStatMstID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@nPatientID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@dtCreateDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBParameters.Add("@nSettingID", cmbPatients.SelectedValue, ParameterDirection.Input, SqlDbType.BigInt);

                    if (Convert.ToInt64(cmbPatients.SelectedValue) != 0) //Indicates Patient
                    { oDBParameters.Add("@sSettingName", cmbPatients.Text.ToString().Split('-')[1], ParameterDirection.Input, SqlDbType.VarChar, 50); }
                    else //Indicates Account
                    { oDBParameters.Add("@sSettingName", cmbAccounts.Text.ToString().Split('-')[1], ParameterDirection.Input, SqlDbType.VarChar, 50); }

                    oDBParameters.Add("@sBatchName", sBatchName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@nUserID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@dtStatementDate", dtpEndDate.Value, ParameterDirection.Input, SqlDbType.DateTime);
                    oDBParameters.Add("@nCrWaitDays", 30, ParameterDirection.Input, SqlDbType.Int);

                    if (lblDueAmt.Text.ToString() != String.Empty)
                    { oDBParameters.Add("@nCrBalance", Convert.ToDecimal(lblDueAmt.Text.ToString()), ParameterDirection.Input, SqlDbType.Decimal); }
                    else
                    { oDBParameters.Add("@nCrBalance", 0, ParameterDirection.Input, SqlDbType.Decimal); }

                    oDBParameters.Add("@sCrPateintFromName", lblNameFrom.Text.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@sCrPateintToName", lblNameTo.Text.ToString(), ParameterDirection.Input, SqlDbType.VarChar, 50);
                    oDBParameters.Add("@bStatus", true, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@bIsUnclosedDay", IsUnclosedDay, ParameterDirection.Input, SqlDbType.Bit);
                }

                Object oValue = null;
                oDB.Execute("PA_sp_INSERT_BL_Batch_PatientStatement_Mst", oDBParameters, out oValue);

                if (Convert.ToString(oValue) != "")
                { _nResult = Convert.ToInt64(oValue); }

                BatchName = sBatchName;
                return _nResult;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                BatchName = "";
                return _nResult;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                IsUnclosedDay = false;
            }
        }

        private bool SavePatientStatementReport(string sTemplateName, Int64 nPatientID, Int64 nBatchPateintStatMstID, Int64 nAccountID, Decimal nPatientDue, Byte[] fileArray)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            gloStatment objStatement = new gloStatment();

            try
            {
                #region "Saving Patient Template"

                string err;
                string strSQL = "SELECT nCategoryID FROM Category_MST WITH (NOLOCK) WHERE sDescription = 'MIS Reports' AND sCategoryType='Template'";

                oDB.Connect(false);
                object res = oDB.ExecuteScalar_Query(strSQL, out err);

                Int64 categoryID = 0;
                if (res != null) { categoryID = Convert.ToInt64(res); }

                Int64 nTempleteTransactionID = objStatement.SavePatientTemplate(sTemplateName, categoryID, "MIS Reports", nPatientID, nAccountID, fileArray);

                #endregion "Saving Patient Template"

                #region "Saving Batch Details"

                if (nTempleteTransactionID > 0)
                {
                    oDBParameters.Clear();

                    string sBatchName = cmbSettings.Text.ToString() + " " + gloDateMaster.gloDate.DateAsNumber(DateTime.Today.ToShortDateString());

                    oDBParameters.Add("@nBatchPateintStatDtlID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@nBatchPateintStatMstID", nBatchPateintStatMstID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDBParameters.Add("@nUserID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                    if (IsIndvidualView == false)
                    {
                        oDBParameters.Add("@dtStatementDate", gloDateMaster.gloDate.DateAsNumber(dtCriteriaEndDate.Value.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                    }
                    else
                    {
                        oDBParameters.Add("@dtStatementDate", gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                    }
                    oDBParameters.Add("@nCrWaitDays", 30, ParameterDirection.Input, SqlDbType.Int);

                    oDBParameters.Add("@bStatus", true, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@nTempleteTransactionID", nTempleteTransactionID, ParameterDirection.Input, SqlDbType.BigInt);


                    oDBParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                    oDBParameters.Add("@nPAccountID", nAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@nPatientDue", nPatientDue, ParameterDirection.Input, SqlDbType.Decimal);

                    Object oResult;
                    oDB.Execute("gsp_PA_INSERT_BL_Batch_PatientStatement_Dtl", oDBParameters, out oResult);
                    oResult = null;

                    return true;
                }
                else
                {
                    return false;
                }

                #endregion "Saving Batch Details"
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }
            finally
            {
                if (objStatement != null)
                {
                    objStatement.Dispose();
                    objStatement = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                }
            }
        }

        private bool SavePatientStatementFile(Int64 BatchID, string filePath)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);

            Byte[] oTemplate = null;
            try
            {
                oTemplate = ogloTemplate.ConvertFileToBinary(filePath);

                oDBParameters.Clear();
                oDB.Connect(false);

                oDBParameters.Add("@BatchID", BatchID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@iBatchStatementFile", oTemplate, ParameterDirection.Input, SqlDbType.Image);
                oDB.Execute("gsp_IniBatchStatementFile_MST", oDBParameters);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                if (ogloTemplate != null) { ogloTemplate.Dispose(); ogloTemplate = null; }
                if (oTemplate != null) { oTemplate = null; }
            }
            return true;
        }

        private void SetupFollowUp(Int64 AccountID, Int64 PatientID, Decimal AccoountOrPatientDue)
        {
            int _NoofDays = 0;
            int _NoofStmt = 0;
            int _nStmtCount = 0;

            bool bHasWorked = false;
            string _sDefaultDesc = string.Empty;
            string _sDefaultCode = string.Empty;

            DataSet dsDefaultAccountFollowUp = null;
            DateTime dtLogDate = DateTime.Now;

            CL_FollowUpCode oCollection = new CL_FollowUpCode();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                if (!IsIndvidualView)
                { objClsgloStatment.ResetStatementCountAfterStatementSend(AccountID, dtCriteriaEndDate.Value, false); }
                else
                { objClsgloStatment.ResetStatementCountAfterStatementSend(AccountID, dtpEndDate.Value, false); }

                dsDefaultAccountFollowUp = gloAccountsV2.gloBillingCommonV2.GetDefaultAccountFollowUp(AccountID);

                if (dsDefaultAccountFollowUp.Tables.Count > 0)
                {
                    _NoofStmt = Convert.ToInt32(dsDefaultAccountFollowUp.Tables[0].Rows[0]["nNoOfStmt"].ToString());
                    _NoofDays = Convert.ToInt32(dsDefaultAccountFollowUp.Tables[1].Rows[0]["nNoOfDays"].ToString());
                    _sDefaultCode = Convert.ToString(dsDefaultAccountFollowUp.Tables[2].Rows[0]["sFollowUpActionCode"].ToString());
                    _sDefaultDesc = Convert.ToString(dsDefaultAccountFollowUp.Tables[2].Rows[0]["sFollowUpActionDescription"].ToString());
                    _nStmtCount = Convert.ToInt32(dsDefaultAccountFollowUp.Tables[3].Rows[0]["nStmtCount"].ToString());
                }

                if (_nStmtCount >= _NoofStmt)
                {
                    Decimal PatientDue = 0;
                    dtLogDate = CL_FollowUpCode.GetServerDate();
                    dtLogDate = dtLogDate.AddDays(_NoofDays);
                    Decimal.TryParse(lblPatientDue.Text, out PatientDue);

                    if (IsIndvidualView)
                    {
                        if (PatientDue > 0)
                        { oCollection.SaveFollowUpScedule(CollectionEnums.FollowUpType.PatientAccount, AccountID, PatientID, dtLogDate, _sDefaultCode, _sDefaultDesc, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.System, DateTime.MinValue, ref bHasWorked); }
                    }
                    else
                    {
                        if (AccoountOrPatientDue > 0)
                        { oCollection.SaveFollowUpScedule(CollectionEnums.FollowUpType.PatientAccount, AccountID, PatientID, dtLogDate, _sDefaultCode, _sDefaultDesc, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, CollectionEnums.ScheduleType.System, DateTime.MinValue, ref bHasWorked); }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                { oDB.Disconnect(); oDB.Dispose(); }

                if (oCollection != null)
                { oCollection.Dispose(); oCollection = null; }

                if (dsDefaultAccountFollowUp != null)
                { dsDefaultAccountFollowUp.Dispose(); dsDefaultAccountFollowUp = null; }
            }
        }

        public StringBuilder GetStatementFileHeader(int StatementDate, gloStatment.StatementSettings StatementSettings)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("@StartStatementFile");
                sb.AppendLine("@StatementFileVersion|" + StatementSettings.StatementVersion);
                sb.AppendLine("@StatementFileGenerateDate|" + DateTime.Now.ToString("MM/dd/yyyy"));
                sb.AppendLine("@StatementFileComment|This file was sent from gloStream gloPM " + StatementSettings.gloSuiteVersion + "");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at GetStatementFileHeader " + ex.ToString(), false);
            }
            finally
            {

            }

            return sb;
        }

        public StringBuilder GetStatementHeader(Int64 AccountID, Int64 PatientID, int StatementDate, gloStatment.StatementSettings StatementSettings)
        {
            string patientCode = string.Empty;
            string patientName = string.Empty;
            string patientAddress = string.Empty;

            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("@StartStatement");
                sb.AppendLine("@StatementDate|" + gloDateMaster.gloDate.DateAsDate(StatementDate).ToString("MM/dd/yyyy"));

                #region "Practice Info Section"

                string practiceName = string.Empty;
                string practiceAddress = string.Empty;
                string practiceWebsite = string.Empty;
                string practiceEMail = string.Empty;
                string practicePhoneNo = string.Empty;

                if ((dsPatientStatementMain.Tables["dt_DisplaySettings"] != null) && (dsPatientStatementMain.Tables["dt_DisplaySettings"].Rows.Count > 0))
                {
                    practiceName = Convert.ToString(dsPatientStatementMain.Tables["dt_DisplaySettings"].Rows[0]["sPracName"]);
                    practiceAddress = Convert.ToString(dsPatientStatementMain.Tables["dt_DisplaySettings"].Rows[0]["sPracAddress1"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_DisplaySettings"].Rows[0]["sPracAddress2"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_DisplaySettings"].Rows[0]["sPracCity"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_DisplaySettings"].Rows[0]["sPracState"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_DisplaySettings"].Rows[0]["sPracZip"]);
                    practiceWebsite = Convert.ToString(dsPatientStatementMain.Tables["dt_DisplaySettings"].Rows[0]["sPracURL"]);
                    practiceEMail = Convert.ToString(dsPatientStatementMain.Tables["dt_DisplaySettings"].Rows[0]["sPracEmail"]);
                    practicePhoneNo = Convert.ToString(dsPatientStatementMain.Tables["dt_DisplaySettings"].Rows[0]["sPracPhone"]);
                    if (practicePhoneNo != "") { practicePhoneNo = "(" + practicePhoneNo.Substring(0, 3) + ") " + practicePhoneNo.Substring(3, 3) + "-" + practicePhoneNo.Substring(6, practicePhoneNo.Length - 6); }
                }

                sb.AppendLine("@PracticeName|" + practiceName);
                sb.AppendLine("@PracticeAddress|" + practiceAddress);
                sb.AppendLine("@PracticePhone|" + practicePhoneNo);
                sb.AppendLine("@PracticeWebsite|" + practiceWebsite);
                sb.AppendLine("@PracticeEMail|" + practiceEMail);
                sb.AppendLine("@PracticeMisc|");

                #endregion

                #region "Remit Info Section "

                string remitName = string.Empty;
                string remitAddress = string.Empty;
                string clinicMsg1 = string.Empty;
                string clinicMsg2;

                if ((dsPatientStatementMain.Tables["dt_RemitInfo"] != null) && (dsPatientStatementMain.Tables["dt_RemitInfo"].Rows.Count > 0))
                {
                    remitName = Convert.ToString(dsPatientStatementMain.Tables["dt_RemitInfo"].Rows[0]["sRemitName"]);
                    remitAddress = Convert.ToString(dsPatientStatementMain.Tables["dt_RemitInfo"].Rows[0]["sRemitAddress1"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_RemitInfo"].Rows[0]["sRemitAddress2"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_RemitInfo"].Rows[0]["sRemitCity"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_RemitInfo"].Rows[0]["sRemitState"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_RemitInfo"].Rows[0]["sRemitZip"]);
                    clinicMsg1 = Convert.ToString(dsPatientStatementMain.Tables["dt_RemitInfo"].Rows[0]["sClinicMessage1"]);
                    clinicMsg2 = Convert.ToString(dsPatientStatementMain.Tables["dt_RemitInfo"].Rows[0]["sClinicMessage2"]);
                }

                sb.AppendLine("@RemitToName|" + remitName);
                sb.AppendLine("@RemitToAddress|" + remitAddress);

                #endregion

                if (StatementSettings.StatementVersion == "2")
                { 
                    sb.Append(GetPatientLoopHeader(patientCode, patientName, patientAddress, StatementSettings)); 
                }

                sb.Append(GetStatementBodyRevised(AccountID, PatientID, StatementDate, StatementSettings).ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at GetStatementFileHeader " + ex.ToString(), false);
            }
            finally
            {

            }

            return sb;
        }

        public StringBuilder GetStatementBodyRevised(Int64 AccountID, Int64 PatientID, int StatementDate, gloStatment.StatementSettings StatementSettings)
        {
            //string statVersion = string.Empty;
            string patientCode = string.Empty;
            string patientName = string.Empty;
            string patientAddress = string.Empty;

            string practicePhone = string.Empty;

            //string receiptNo = string.Empty;
            //string lastPaymentDate = string.Empty;
            //string lastPaymentAmount = string.Empty;

            StringBuilder sb = new StringBuilder();
            StringBuilder sbFutureAppointment = new StringBuilder();
            StringBuilder sbPaymentLines = new StringBuilder();
            StringBuilder sbPaymentNoteLine = new StringBuilder();

            gloStatment objStatement = new gloStatment();

            DataTable dtPatientInAccount = null;
            DataView dvChargeLines = null;
            DataView dvPaymentLines = null;

            try
            {
                //Int64 _patientID = 0;

                StatementTotals statementTotals = new StatementTotals();

                dtPatientInAccount = dsPatientStatementMain.Tables["dt_PatientStatement_Revised"].DefaultView.ToTable(true, new string[] { "nPatientID" });

                if (dtPatientInAccount != null)
                {
                    foreach (DataRow dr_patient in dtPatientInAccount.Rows)
                    {
                        Int64 _patientID = Convert.ToInt64(dr_patient["nPatientID"]);

                        #region "Patient info & Future Appointments"

                        using (DataSet dsPatientInfo = objStatement.GetPatientInfo(_patientID, StatementDate))
                        {
                            if (dsPatientInfo != null && dsPatientInfo.Tables.Count >= 1)
                            {
                                string practicePhoneNo = string.Empty;
                                if ((dsPatientInfo != null) && (dsPatientInfo.Tables.Count >= 0) && (dsPatientInfo.Tables[0].Rows.Count > 0))
                                {
                                    patientCode = Convert.ToString(dsPatientInfo.Tables[0].Rows[0]["sPatientCode"]);
                                    patientName = Convert.ToString(dsPatientInfo.Tables[0].Rows[0]["sFirstName"]) + "|" + Convert.ToString(dsPatientInfo.Tables[0].Rows[0]["sLastName"]);
                                    patientAddress = Convert.ToString(dsPatientInfo.Tables[0].Rows[0]["PatientAddress"]);
                                }

                                #region "Fetching Future appointments"

                                if ((dsPatientStatementMain.Tables["dt_DisplaySettings"] != null) && (dsPatientStatementMain.Tables["dt_DisplaySettings"].Rows.Count > 0))
                                {
                                    practicePhoneNo = Convert.ToString(dsPatientStatementMain.Tables["dt_DisplaySettings"].Rows[0]["sPracPhone"]);
                                    if (practicePhoneNo != "") { practicePhoneNo = "(" + practicePhoneNo.Substring(0, 3) + ") " + practicePhoneNo.Substring(3, 3) + "-" + practicePhoneNo.Substring(6, practicePhoneNo.Length - 6); }
                                }

                                if ((dsPatientInfo != null) && (dsPatientInfo.Tables.Count >= 1) && (dsPatientInfo.Tables[1].Rows.Count > 0))
                                {
                                    //Bug #61524: gloPM - Patient Statement- Application does not display all future appointments in patient statement
                                    //Description: For version 1 we shows all the future appointment for the patient and for version 2 we show only top 1 future appointment.
                                    foreach (DataRow drFutureAppointment in dsPatientInfo.Tables[1].Rows)
                                    {
                                        sbFutureAppointment.AppendLine("@FutureAppointmentStart");
                                        sbFutureAppointment.AppendLine(" @Date|" + drFutureAppointment["dtStartDate"].ToString());
                                        sbFutureAppointment.AppendLine(" @Time|" + drFutureAppointment["dtStartTime"].ToString());
                                        sbFutureAppointment.AppendLine(" @PatientName|" + patientName);
                                        sbFutureAppointment.AppendLine(" @LocationAddress|" + drFutureAppointment["LocationAddress"].ToString());
                                        sbFutureAppointment.AppendLine(" @LocationPhone|" + practicePhoneNo);
                                        sbFutureAppointment.AppendLine(" @AppointmentProvider|" + drFutureAppointment["sProviderName"].ToString());
                                        sbFutureAppointment.AppendLine("@FutureAppointmentEnd");
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion

                        if (StatementSettings.StatementVersion == "2")
                        {
                            sb.AppendLine("@PatientLoopStart");
                            sb.AppendLine("@Patient|" + patientName);
                        }
                        else
                        { 
                            sb.Append(GetPatientLoopHeader(patientCode, patientName, patientAddress, StatementSettings));
                        }

                        decimal totalChargePatientDue = 0;
                        decimal totalChargeInsuranceDue = 0;
                        decimal totalChargeAmount = 0;
                        decimal totalPaymentAndAdjustments = 0;

                        string responsibility = string.Empty;

                        Int64 nBillingTransactionID = 0;
                        Int64 nBillingTransactionDetailID = 0;

                        dvChargeLines = dsPatientStatementMain.Tables["dt_PatientStatement_Revised1"].DefaultView;
                        dvChargeLines.RowFilter = "nPatientID=" + Convert.ToString(_patientID);

                        foreach (DataRowView dr_charges in dvChargeLines)
                        {
                            decimal insurancePaid = 0;
                            decimal patientPaid = 0;
                            decimal insAdjustments = 0;

                            nBillingTransactionID = Convert.ToInt64(dr_charges["nBillingTransactionID"]);
                            nBillingTransactionDetailID = Convert.ToInt64(dr_charges["nBillingTransactionDetailID"]);

                            if (Convert.ToString(dr_charges["Party"]) == "2")
                            { responsibility = "I"; }
                            else
                            { responsibility = "P"; }

                            #region "Calculating InsurancePaid, PatientPaid & Insurance Adjustments"

                            dvPaymentLines = dsPatientStatementMain.Tables["dt_PatientStatement_Revised"].DefaultView;
                            dvPaymentLines.RowFilter = "nBillingTransactionDetailID=" + Convert.ToString(nBillingTransactionDetailID);

                            if (dvPaymentLines != null && dvPaymentLines.Table.Rows.Count > 0)
                            {
                                foreach (DataRowView row in dvPaymentLines)
                                {
                                    if (Convert.ToString(row["nsorttype"]) == "4" || Convert.ToString(row["nsorttype"]) == "7")
                                    { insurancePaid += Convert.ToDecimal(row["namount"]); }
                                    if (Convert.ToString(row["nsorttype"]) == "1" || Convert.ToString(row["nsorttype"]) == "2")
                                    { patientPaid += Convert.ToDecimal(row["namount"]); }
                                    if (Convert.ToString(row["nsorttype"]) == "3" || Convert.ToString(row["nsorttype"]) == "5" || Convert.ToString(row["nsorttype"]) == "6")
                                    { insAdjustments += Convert.ToDecimal(row["namount"]); }
                                }
                            }

                            #endregion

                            #region "Charge Line Section"

                            sb.AppendLine(" @ChargeLineStart");
                            sb.AppendLine("   @Date|" + Convert.ToString(dr_charges["sFromDate"]));
                            sb.AppendLine("   @Patient|" + patientName);

                            sb.AppendLine("   @Responsibility|" + responsibility);
                            sb.AppendLine("   @Procedure|" + Convert.ToString(dr_charges["sCPTCode"]));
                            sb.AppendLine("   @Description|" + Convert.ToString(dr_charges["sCPTDescription"]));
                            sb.AppendLine("   @Amount|" + Convert.ToDecimal(dr_charges["ChargeTotal"]).ToString("#0.00"));

                            sb.AppendLine("   @ChargeTotalPatPaid|" + Convert.ToDecimal(patientPaid).ToString("#0.00"));
                            sb.AppendLine("   @ChargeTotalInsPaid|" + Convert.ToDecimal(insurancePaid).ToString("#0.00"));
                            sb.AppendLine("   @ChargeTotalAdj|" + Convert.ToDecimal(insAdjustments).ToString("#0.00"));

                            if (responsibility == "P")
                            {
                                sb.AppendLine("   @ChargeInsPending|" + "0.00");
                                sb.AppendLine("   @ChargePatientDue|" + Convert.ToDecimal(dr_charges["Pending"]).ToString("#0.00"));

                                totalChargePatientDue += Convert.ToDecimal(dr_charges["Pending"]);
                            }
                            else
                            {
                                sb.AppendLine("   @ChargeInsPending|" + Convert.ToDecimal(dr_charges["Pending"]).ToString("#0.00"));
                                sb.AppendLine("   @ChargePatientDue|" + "0.00");

                                totalChargeInsuranceDue += Convert.ToDecimal(dr_charges["Pending"]);
                            }

                            totalChargeAmount += Convert.ToDecimal(dr_charges["ChargeTotal"]);

                            sb.AppendLine(" @ChargeLineEnd");

                            #endregion

                            #region "Payment, Adjustment & Notes Section"

                            string sortType = string.Empty;
                            string actionCode = string.Empty;
                            string closeDate = string.Empty;
                            sbPaymentNoteLine = new StringBuilder();
                            //Bug #61549: gloPM - Patient Statement- Application does not display all Insurance Payment Remit correctly in patient statement
                            //Added flag to check payment note added or not.
                            bool IsPaymentNoteAdded = false;

                            foreach (DataRowView dr_payments in dvPaymentLines)
                            {
                                sortType = Convert.ToString(dr_payments["nSortType"]);
                                actionCode = Convert.ToString(dr_payments["ActionCode"]);
                                closeDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dr_payments["CloseDate"])).ToString("MM/dd/yyyy");

                                if (Convert.ToString(dr_payments["Party"]) == "2")
                                { responsibility = "I"; }
                                else
                                { responsibility = "P"; }

                                if (sortType.Equals("1") || sortType.Equals("2") || sortType.Equals("4") || sortType.Equals("7"))
                                {
                                    sb.AppendLine(" @PaymentLineStart");
                                    sb.AppendLine("   @Date|" + closeDate);
                                    sb.AppendLine("   @Source|" + responsibility);
                                    sb.AppendLine("   @Description|" + Convert.ToString(dr_payments["sDescription"]));
                                    sb.AppendLine("   @Amount|" + Convert.ToString(dr_payments["nAmount"]));
                                    sb.AppendLine(" @PaymentLineEnd");
                                    //Bug #61549: gloPM - Patient Statement- Application does not display all Insurance Payment Remit correctly in patient statement
                                    //Added notes for payment in note section of electronic file.
                                    if (Convert.ToString(dr_payments["sPaymentNoteDescription"]) != "")
                                    {
                                        sbPaymentNoteLine.AppendLine(" @NoteLineStart");
                                        sbPaymentNoteLine.AppendLine("   @Date|" + Convert.ToString(dr_payments["PayCloseDate"]));
                                        sbPaymentNoteLine.AppendLine("   @Description|" + Convert.ToString(dr_payments["sPaymentNoteDescription"]));
                                        sbPaymentNoteLine.AppendLine(" @NoteLineEnd");
                                    }
                                }
                                if (sortType.Equals("3") || sortType.Equals("5") || sortType.Equals("6"))
                                {
                                    sb.AppendLine(" @AdjustmentLineStart");
                                    sb.AppendLine("   @Date|" + closeDate);
                                    sb.AppendLine("   @Source|" + responsibility);
                                    sb.AppendLine("   @Description|" + Convert.ToString(dr_payments["sDescription"]));
                                    sb.AppendLine("   @Amount|" + Convert.ToString(dr_payments["nAmount"]));
                                    sb.AppendLine(" @AdjustmentLineEnd");
                                    //Bug #61549: gloPM - Patient Statement- Application does not display all Insurance Payment Remit correctly in patient statement
                                    //Added notes for payment in note section of electronic file.
                                    if (Convert.ToString(dr_payments["sPaymentNoteDescription"]) != "")
                                    {
                                        sbPaymentNoteLine.AppendLine(" @NoteLineStart");
                                        sbPaymentNoteLine.AppendLine("   @Date|" + Convert.ToString(dr_payments["PayCloseDate"]));
                                        sbPaymentNoteLine.AppendLine("   @Description|" + Convert.ToString(dr_payments["sPaymentNoteDescription"]));
                                        sbPaymentNoteLine.AppendLine(" @NoteLineEnd");
                                    }
                                }
                                if (sortType.Equals("8") || sortType.Equals("9"))
                                {
                                    if (Convert.ToString(dr_payments["sDescription"]) != "")
                                    {
                                        if (actionCode.Equals("R") && insurancePaid == 0)
                                        { }
                                        else
                                        {
                                            //Bug #61549: gloPM - Patient Statement- Application does not display all Insurance Payment Remit correctly in patient statement
                                            //Added notes for payment in note section of electronic file.
                                            //Added if condition to resolve the bug.
                                            if (!IsPaymentNoteAdded)
                                            {
                                                sb.Append(sbPaymentNoteLine);
                                                IsPaymentNoteAdded = true;
                                            }
                                            sb.AppendLine(" @NoteLineStart");
                                            sb.AppendLine("   @Date|" + Convert.ToString(dr_payments["PayCloseDate"]));
                                            sb.AppendLine("   @Description|" + Convert.ToString(dr_payments["sDescription"]));
                                            sb.AppendLine(" @NoteLineEnd");
                                        }
                                    }
                                }
                                
                                totalPaymentAndAdjustments += Convert.ToDecimal(dr_payments["nAmount"]);
                            }
                            //Bug #61549: gloPM - Patient Statement- Application does not display all Insurance Payment Remit correctly in patient statement
                            //Added condition if sbpaymentnoteline has value and not added in note section.
                            if (sbPaymentNoteLine.Length != 0 && IsPaymentNoteAdded==false)
                            {
                                sb.Append(sbPaymentNoteLine);
                            }
                            #endregion
                        }

                        #region "Patient loop Footer "

                        if (StatementSettings.StatementVersion == "2")
                        {
                            sb.AppendLine("@PatientTotalChargeAmount|" + Convert.ToDecimal(totalChargeAmount).ToString("#0.00"));
                            sb.AppendLine("@PatientTotalPaymentsandAdjustments|" + Convert.ToDecimal(totalPaymentAndAdjustments).ToString("#0.00"));
                            sb.AppendLine("@PatientTotalChargeInsPending|" + Convert.ToDecimal(totalChargeInsuranceDue).ToString("#0.00"));
                            sb.AppendLine("@PatientTotalChargePatientDue|" + Convert.ToDecimal(totalChargePatientDue).ToString("#0.00"));

                            sb.AppendLine("@PatientLoopEnd");
                        }

                        #endregion

                        statementTotals.totalStatementChargeAmount += totalChargeAmount;
                        statementTotals.totalStatementPaymentAndAdjustments += totalPaymentAndAdjustments;
                        statementTotals.totalStatementChargeInsuranceDue += totalChargeInsuranceDue;
                        statementTotals.totalStatementChargePatientDue += totalChargePatientDue;
                    }

                    //Statement Footer
                    sb.Append(GetStatementFooter(statementTotals, sbFutureAppointment.ToString(), StatementSettings).ToString());
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at generating a electronic statement file for account #" + Convert.ToString(AccountID) + "  " + ex.ToString(), false);
            }
            finally
            {
                if (objStatement != null) { objStatement.Dispose(); objStatement = null; }
                if (dtPatientInAccount != null) { dtPatientInAccount.Dispose(); dtPatientInAccount = null; }
                if (dvChargeLines != null) { dvChargeLines.Dispose(); dvChargeLines = null; }
                if (dvPaymentLines != null) { dvPaymentLines.Dispose(); dvPaymentLines = null; }
                if (sbFutureAppointment != null) { sbFutureAppointment = null; }
                if (sbPaymentNoteLine != null) { sbPaymentNoteLine = null; }
            }
            return sb;

        }

        private StringBuilder GetPatientLoopHeader(string patientCode, string patientName, string patientAddress, gloStatment.StatementSettings StatementSettings)
        {
            string guarantorName = string.Empty;
            string guarantorAddress = string.Empty;

            string lastPaymentDate = string.Empty;
            string lastPaymentAmount = string.Empty;

            StringBuilder sb = new StringBuilder();

            if ((dsPatientStatementMain.Tables["dt_GuarantorInfo"] != null) && (dsPatientStatementMain.Tables["dt_GuarantorInfo"].Rows.Count > 0))
            {
                patientCode = Convert.ToString(dsPatientStatementMain.Tables["dt_GuarantorInfo"].Rows[0]["nPatientCode"]); 
                guarantorName = Convert.ToString(dsPatientStatementMain.Tables["dt_GuarantorInfo"].Rows[0]["GuarantorFName"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_GuarantorInfo"].Rows[0]["GuarantorMName"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_GuarantorInfo"].Rows[0]["GuarantorLName"]);
                guarantorAddress = Convert.ToString(dsPatientStatementMain.Tables["dt_GuarantorInfo"].Rows[0]["GuarantorAdd1"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_GuarantorInfo"].Rows[0]["GuarantorAdd2"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_GuarantorInfo"].Rows[0]["GuarantorCity"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_GuarantorInfo"].Rows[0]["GuarantorState"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_GuarantorInfo"].Rows[0]["GuarantorZip"]) + "|" + Convert.ToString(dsPatientStatementMain.Tables["dt_GuarantorInfo"].Rows[0]["GuarantorCountry"]);
            }
            else
            {
                guarantorName = patientName;
                guarantorAddress = patientAddress;
            }

            if ((dsPatientStatementMain.Tables["dt_LastAccountPayment"] != null) && (dsPatientStatementMain.Tables["dt_LastAccountPayment"].Rows.Count > 0))
            {
                lastPaymentDate = Convert.ToString(dsPatientStatementMain.Tables["dt_LastAccountPayment"].Rows[0]["Closedate"]);
                lastPaymentAmount = Convert.ToString(dsPatientStatementMain.Tables["dt_LastAccountPayment"].Rows[0]["dReceiptAmount"]);
            }

            if (StatementSettings.StatementVersion == "1")
            {
                sb.AppendLine("@GuarantorAccountNumber|" + patientCode);
            }
            else
            {
                sb.AppendLine("@AccountNumber|" + patientCode);
            }

            sb.AppendLine("@GuarantorName|" + guarantorName);
            sb.AppendLine("@GuarantorAddress|" + guarantorAddress);

            if (StatementSettings.StatementVersion == "1")
            {
                sb.AppendLine("@Patient|" + patientName);
                sb.AppendLine("@LastPatientPayment|" + lastPaymentDate + "|" + lastPaymentAmount);
            }
            else
            {
                sb.AppendLine("@LastAccountPayment|" + lastPaymentDate + "|" + lastPaymentAmount);
                //sb.AppendLine("@PatientLoopStart|");
                //sb.AppendLine("@Patient|" + patientName);
            }

            return sb;
        }

        public StringBuilder GetStatementFooter(StatementTotals statementTotal, string sbFutureAppointments, gloStatment.StatementSettings StatementSettings)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                #region "Statement total Section"

                sb.AppendLine("@StatementTotalChargeAmount|" + statementTotal.totalStatementChargeAmount.ToString("#0.00"));
                sb.AppendLine("@StatementTotalPaymentsandAdjustments|" + statementTotal.totalStatementPaymentAndAdjustments.ToString("#0.00"));
                sb.AppendLine("@StatementTotalChargeInsPending|" + statementTotal.totalStatementChargeInsuranceDue.ToString("#0.00"));
                sb.AppendLine("@StatementTotalChargePatientDue|" + statementTotal.totalStatementChargePatientDue.ToString("#0.00"));

                if ((dsPatientStatementMain.Tables["dt_PatientReserve"] != null) && (dsPatientStatementMain.Tables["dt_PatientReserve"].Rows.Count > 0))
                {
                    statementTotal.statementAvailableReserve = Convert.ToDecimal(dsPatientStatementMain.Tables["dt_PatientReserve"].Rows[0]["AvailableReserve"]);
                    statementTotal.totalStatementPatientDue = Convert.ToDecimal(dsPatientStatementMain.Tables["dt_PatientReserve"].Rows[0]["PatientDue"]) - statementTotal.statementAvailableReserve;

                    sb.AppendLine("@StatementAvailablePayments|" + statementTotal.statementAvailableReserve.ToString("#0.00"));
                    sb.AppendLine("@StatementTotalPatientDue|" + statementTotal.totalStatementPatientDue.ToString("#0.00"));
                }

                #endregion

                #region "Ageing Bucket "

                string agingVal1 = string.Empty;
                string agingVal2 = string.Empty;
                string agingVal3 = string.Empty;
                string agingVal4 = string.Empty;
                string agingVal5 = string.Empty;

                if ((dsPatientStatementMain.Tables["dt_AgeingBucket"] != null) && (dsPatientStatementMain.Tables["dt_AgeingBucket"].Rows.Count > 0))
                {
                    DataRow dr = dsPatientStatementMain.Tables["dt_AgeingBucket"].Rows[0];
                    agingVal1 = Convert.ToString(dr[0]);
                    agingVal2 = Convert.ToString(dr[1]);
                    agingVal3 = Convert.ToString(dr[2]);
                    agingVal4 = Convert.ToString(dr[3]);
                    agingVal5 = Convert.ToString(dr[4]);
                    dr = null;
                }

                sb.AppendLine("@AgingBucketLabel1|0-30");
                sb.AppendLine("@AgingBucketValue1|" + agingVal1);
                sb.AppendLine("@AgingBucketLabel2|31-60");
                sb.AppendLine("@AgingBucketValue2|" + agingVal2);
                sb.AppendLine("@AgingBucketLabel3|61-90");
                sb.AppendLine("@AgingBucketValue3|" + agingVal3);
                sb.AppendLine("@AgingBucketLabel4|91-120");
                sb.AppendLine("@AgingBucketValue4|" + agingVal4);
                sb.AppendLine("@AgingBucketLabel5|120+");
                sb.AppendLine("@AgingBucketValue5|" + agingVal5);

                #endregion

                #region "Account message "

                string clinicMsg1 = string.Empty;

                if ((dsPatientStatementMain.Tables["dt_RemitInfo"] != null) && (dsPatientStatementMain.Tables["dt_RemitInfo"].Rows.Count > 0))
                { clinicMsg1 = Convert.ToString(dsPatientStatementMain.Tables["dt_RemitInfo"].Rows[0]["sClinicMessage1"]); }

                sb.AppendLine("@AccountMessage1|" + clinicMsg1);

                string statementNotes = string.Empty;
                if ((dsPatientStatementMain.Tables["dt_StatementNote"] != null) && (dsPatientStatementMain.Tables["dt_StatementNote"].Rows.Count > 0))
                {
                    foreach (DataRow row in dsPatientStatementMain.Tables["dt_StatementNote"].Rows)
                    { statementNotes = statementNotes + Convert.ToString(row["sStatementNotes"]); }
                }

                sb.AppendLine("@AccountMessage2|" + statementNotes);

                #endregion

                #region "Future Appointment section"
                //Bug #67760: 00000688: Extra blank line is appeaaring in electronic statement
                sb.Append(sbFutureAppointments);

                #endregion

                #region "Last account payment section"

                if (StatementSettings.StatementVersion == "2")
                {
                    string receiptNo = string.Empty;
                    string lastPaymentDate = string.Empty;
                    string lastPaymentAmount = string.Empty;

                    if ((dsPatientStatementMain.Tables["dt_LastAccountPayment"] != null) && (dsPatientStatementMain.Tables["dt_LastAccountPayment"].Rows.Count > 0))
                    {
                        receiptNo = Convert.ToString(dsPatientStatementMain.Tables["dt_LastAccountPayment"].Rows[0]["sReceiptNo"]);
                        lastPaymentDate = Convert.ToString(dsPatientStatementMain.Tables["dt_LastAccountPayment"].Rows[0]["Closedate"]);
                        lastPaymentAmount = Convert.ToDecimal(dsPatientStatementMain.Tables["dt_LastAccountPayment"].Rows[0]["dReceiptAmount"]).ToString("#0.00");
                    }

                    sb.AppendLine("@LastAccountPaymentsStart");
                    sb.AppendLine(" @Date|" + lastPaymentDate);
                    sb.AppendLine(" @ReferenceNumber|" + receiptNo);
                    sb.AppendLine(" @Amount|" + lastPaymentAmount);
                    sb.AppendLine("@LastAccountPaymentsEnd");
                }

                #endregion

                sb.AppendLine("@EndStatement");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at GetStatementFileFooter " + ex.ToString(), false);
            }
            finally
            {

            }

            return sb;
        }

        DataSet dsMain = null;

        private void FillStatementDetailsRevised(Int64 AccountID, Int64 PatientID, Int64 StatementCriteriaID, int StatementDate, int StatementCount, bool IncludeElectronicFileIno)
        {
            SqlConnection oConnection = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            //DataSet dsMain = new DataSet();
            dsMain = new DataSet();
            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;

                sqlCmd.CommandType = CommandType.StoredProcedure;

                if (_isGWPatientStatement)
                {
                    sqlCmd.CommandText = "PA_RPT_PatientStatement_Main_GW";
                }
                else
                {
                    sqlCmd.CommandText = "PA_RPT_PatientStatement_Main";
                }

                SqlParameter ParAccount = new SqlParameter();
                {
                    ParAccount.ParameterName = "@nPAccountID";
                    ParAccount.Value = AccountID;
                    ParAccount.Direction = ParameterDirection.Input;
                    ParAccount.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParAccount);
                ParAccount = null;

                SqlParameter ParaPatientID = new SqlParameter();
                {
                    ParaPatientID.ParameterName = "@nPatientID";
                    ParaPatientID.Value = PatientID;
                    ParaPatientID.Direction = ParameterDirection.Input;
                    ParaPatientID.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaPatientID);
                ParaPatientID = null;

                SqlParameter ParCriteriaId = new SqlParameter();
                {
                    ParCriteriaId.ParameterName = "@nCriteriaID";
                    ParCriteriaId.Value = StatementCriteriaID;
                    ParCriteriaId.Direction = ParameterDirection.Input;
                    ParCriteriaId.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParCriteriaId);
                ParCriteriaId = null;

                SqlParameter ParaEndDate = new SqlParameter();
                {
                    ParaEndDate.ParameterName = "@dtDate";
                    ParaEndDate.Value = gloDateMaster.gloDate.DateAsDate(StatementDate);
                    ParaEndDate.Direction = ParameterDirection.Input;
                    ParaEndDate.SqlDbType = SqlDbType.DateTime;
                }
                sqlCmd.Parameters.Add(ParaEndDate);
                ParaEndDate = null;

                SqlParameter ParaEndDate1 = new SqlParameter();
                {
                    ParaEndDate1.ParameterName = "@dtDate1";
                    ParaEndDate1.Value = StatementDate;
                    ParaEndDate1.Direction = ParameterDirection.Input;
                    ParaEndDate1.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaEndDate1);
                ParaEndDate1 = null;

                SqlParameter ParaStatementCount = new SqlParameter();
                {
                    ParaStatementCount.ParameterName = "@nStatementCount";
                    ParaStatementCount.Value = StatementCount;
                    ParaStatementCount.Direction = ParameterDirection.Input;
                    ParaStatementCount.SqlDbType = SqlDbType.BigInt;
                }

                sqlCmd.Parameters.Add(ParaStatementCount);
                ParaStatementCount = null;

                if (_isGWPatientStatement)
                {
                    SqlParameter ParaStatCreateDate = new SqlParameter();
                    {
                        ParaStatCreateDate.ParameterName = "@dtCreateDate";
                        ParaStatCreateDate.Value = StatementCreateDate;
                        ParaStatCreateDate.Direction = ParameterDirection.Input;
                        ParaStatCreateDate.SqlDbType = SqlDbType.DateTime;
                    }
                    sqlCmd.Parameters.Add(ParaStatCreateDate);
                    ParaStatCreateDate = null;
                }

                sqlCmd.Connection = oConnection;
                sqlCmd.CommandTimeout = 0;

                da = new SqlDataAdapter(sqlCmd);
                da.Fill(dsMain);

                if ((dsMain != null && dsMain.Tables.Count == 10) || (_isGWPatientStatement && dsMain != null && dsMain.Tables.Count == 11))
                {
                    if (dsMain.Tables[0] != null && dsMain.Tables[0].Rows.Count > 0)
                    {
                        dsPatientStatementMain.Tables["dt_PatientStatement_Revised"].Merge(dsMain.Tables[0], true, MissingSchemaAction.Ignore);
                    }
                    if (dsMain.Tables[1] != null && dsMain.Tables[1].Rows.Count > 0)
                    {
                        dsMain.Tables[1].TableName = "dt_PatientStatement_Revised1";
                        dsPatientStatementMain.Merge(dsMain.Tables[1]);
                    }
                    if (dsMain.Tables[2] != null && dsMain.Tables[2].Rows.Count > 0)
                    {
                        dsPatientStatementMain.Tables["dt_AgeingBucket"].Merge(dsMain.Tables[2], true, MissingSchemaAction.Ignore);
                    }
                    if (dsMain.Tables[3] != null && dsMain.Tables[3].Rows.Count > 0)
                    {
                        dsPatientStatementMain.Tables["dt_PatientReserve"].Merge(dsMain.Tables[3], true, MissingSchemaAction.Ignore);
                    }
                    if (dsMain.Tables[4] != null && dsMain.Tables[4].Rows.Count > 0)
                    {
                        dsMain.Tables[4].TableName = "dt_GuarantorInfo";
                        dsPatientStatementMain.Merge(dsMain.Tables[4]);
                    }
                    if (dsMain.Tables[5] != null && dsMain.Tables[5].Rows.Count > 0)
                    {
                        try
                        {
                            string statementNotes = string.Empty;

                            foreach (DataRow row in dsMain.Tables[5].Rows)
                            { statementNotes = statementNotes + Convert.ToString(row["sStatementNote"]); }

                            if (statementNotes != string.Empty)
                            {
                                dsPatientStatementMain.Tables["dt_StatementNote"].Rows.Add();
                                dsPatientStatementMain.Tables["dt_StatementNote"].Rows[0][0] = statementNotes;
                            }
                            else
                            {
                                dsPatientStatementMain.Tables["dt_StatementNote"].Rows.Add();
                                dsPatientStatementMain.Tables["dt_StatementNote"].Rows[0][0] = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog("AccountID : " + AccountID.ToString() + " PatientID : " + PatientID.ToString() + " Exception :" + ex.ToString(), false);
                        }
                    }
                    if (dsMain.Tables[6] != null && dsMain.Tables[6].Rows.Count > 0)
                    {
                        dsPatientStatementMain.Tables["dt_RemitInfo"].Merge(dsMain.Tables[6]);
                    }
                    if (dsMain.Tables[7] != null && dsMain.Tables[7].Rows.Count > 0)
                    {
                        dsPatientStatementMain.Tables["dt_PayTo"].Merge(dsMain.Tables[7], true, MissingSchemaAction.Ignore);
                    }
                    if (dsMain.Tables[8] != null && dsMain.Tables[8].Rows.Count > 0)
                    {
                        dsPatientStatementMain.Tables["dt_DisplaySettings"].Merge(dsMain.Tables[8], true, MissingSchemaAction.Ignore);
                    }
                    if (dsMain.Tables[9] != null && dsMain.Tables[9].Rows.Count > 0)
                    {
                        dsMain.Tables[9].TableName = "dt_LastAccountPayment";
                        dsPatientStatementMain.Merge(dsMain.Tables[9]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at FillStatementDetails " + ex.ToString(), false);
            }
            finally
            {
                if (oConnection != null) { oConnection.Close(); oConnection.Dispose(); oConnection = null; }
                if (sqlCmd != null) { if (sqlCmd.Parameters != null) { sqlCmd.Parameters.Clear(); } sqlCmd.Dispose(); sqlCmd = null; }
                if (da != null) { da.Dispose(); da = null; }
            }
        }

        private void ProcessIndividualStatement(StatementAction action)
        {
            StringBuilder sbMainFile = new StringBuilder();
            gloStatment objStatement = new gloStatment();

            gloStatment.StatementSettings settings = null;
            DataTable dtPortalStatementEmailAccountIds = null;
            System.Drawing.Printing.PrinterSettings printSettings = null;
            try
            {
                int rowsSelected = 0;
                Int64 batchID = 0;
                string batchName = string.Empty;

                int statementDate = GetStatementDate();
                Int64 businessCenterID = GetBusinessCenterID();
                Int64 criteriaID = GetStatementCriteriaID(businessCenterID);

                if (action != StatementAction.ViewOnly)
                {
                    rowsSelected = 1;

                    #region "Set progress bar"

                    prgFileGeneration.Maximum = rowsSelected;
                    prgFileGeneration.Minimum = 0;
                    prgFileGeneration.Value = 0;
                    prgFileGeneration.Step = 1;

                    if (rowsSelected > 0)
                    {
                        pnlProgressBar.Visible = true;
                        prgFileGeneration.Visible = true;
                        if (this.Parent != null) { this.Parent.Cursor = Cursors.WaitCursor; }
                        this.Cursor = Cursors.WaitCursor;
                        pnlPleasewait.Visible = true;
                    }

                    #endregion

                    #region "Creating a batch "

                    if (rowsSelected > 0)
                    {
                        batchID = CreateBatchMaster(out batchName);
                        if (batchID == 0)
                        {
                            MessageBox.Show("Unable to generate batch. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    #endregion

                    settings = objStatement.GetStatementSettings();

                    if (action == StatementAction.PrintOnly)
                    {
                        //ConverttoPDF();
                        if (!Convert.ToBoolean(appSettings["DefaultPrinter"]))
                        {
                            if (gloGlobal.gloTSPrint.isCopyPrint)
                            {
                                if (!gloWord.LoadAndCloseWord.showTSPrintPopup())
                                {
                                    return;
                                }
                            }
                            else
                            {
                                using (PrintDialog _PrintDialog = new PrintDialog())
                                {
                                    DialogResult res = _PrintDialog.ShowDialog(this);
                                    if (res == System.Windows.Forms.DialogResult.Cancel)
                                    { return; }
                                    else
                                    { printSettings = _PrintDialog.PrinterSettings; }
                                }
                            }
                        }
                    }
                    else if (action == StatementAction.SendOnly)
                    {
                        // Statement File - Header
                        sbMainFile.Append(GetStatementFileHeader(statementDate, settings));
                    }
                }

                decimal patientDue = 0;
                Int64 patientID = 0;
                Int64 accountID = 0;

                if (IsStatementNotificationOn)
                {
                    dtPortalStatementEmailAccountIds = new DataTable();
                    dtPortalStatementEmailAccountIds.Columns.Add(new DataColumn("nAccountID", typeof(long)));
                }
                if (IsIndvidualView)
                {
                    patientID = Convert.ToInt64(cmbPatients.SelectedValue);
                    accountID = Convert.ToInt64(cmbAccounts.SelectedValue);
                }
                else
                {
                    if (c1PatientList.DataSource != null)
                    {
                        if (c1PatientList.Rows.Count > 1)
                        {
                            if (Convert.ToString(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["nPAccountID"].Index)) != "")
                            {
                                accountID = Convert.ToInt64(c1PatientList.GetData(c1PatientList.RowSel, c1PatientList.Cols["nPAccountID"].Index));
                            }
                            patientID = 0;
                        }
                    }
                    //patientID = Convert.ToInt64(cmbPatients.SelectedValue);
                    //accountID = Convert.ToInt64(cmbAccounts.SelectedValue);
                    btnUp_Click(null, null);
                }
            
                try
                {
                    if (action != StatementAction.ViewOnly)
                    {
                        prgFileGeneration.PerformStep();
                        // Progress bar setup
                        if (action == StatementAction.SendOnly)
                        { lblFile.Text = "Processing Batch " + prgFileGeneration.Value + "/" + rowsSelected; }
                        else if (action == StatementAction.PrintOnly)
                        { lblFile.Text = "Printing Batch " + prgFileGeneration.Value + "/" + rowsSelected; }

                        
                        Application.DoEvents();
                    }

                    using (dsPatientStatementMain = new dsRevisedPatientStatement())
                    {
                        int statementCount = GetStatementCount(accountID, statementDate); ;

                        FillStatementDetailsRevised(accountID, patientID, criteriaID, statementDate, statementCount, true);

                        if ((dsPatientStatementMain.Tables["dt_PatientReserve"] != null) && (dsPatientStatementMain.Tables["dt_PatientReserve"].Rows.Count > 0))
                        {
                            patientDue = Convert.ToDecimal(dsPatientStatementMain.Tables["dt_PatientReserve"].Rows[0]["PatientDue"]) - Convert.ToDecimal(dsPatientStatementMain.Tables["dt_PatientReserve"].Rows[0]["AvailableReserve"]);
                            lblPatientDue.Text = patientDue.ToString("#0.00");
                        }

                        if (!object.ReferenceEquals(dsPatientStatementMain, null))
                        {
                            if (_isGWPatientStatement)
                            {
                                ((TextObject)objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["txtPayPlan"]).Text = "";
                                objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["txtPayPlan"].ObjectFormat.EnableSuppress = true;
                                objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["picRed"].ObjectFormat.EnableSuppress = true;
                                objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["picGreen"].ObjectFormat.EnableSuppress = true;
                                objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["Text10"].ObjectFormat.EnableSuppress = true;

                                if (dsMain.Tables[10] != null && dsMain.Tables[10].Rows.Count > 0)
                                {
                                    if (Convert.ToDecimal(dsMain.Tables[10].Rows[0]["nPlanID"]) != 0 && dsMain.Tables[10].Rows[0]["dPlanAmount"].ToString() != "")
                                    {
                                        if (Convert.ToDecimal(dsMain.Tables[10].Rows[0]["PlanStatus"]) == 1)
                                        {
                                            objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["txtPayPlan"].ObjectFormat.EnableSuppress = false;
                                            ((TextObject)objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["txtPayPlan"]).Text = Convert.ToDouble(dsMain.Tables[10].Rows[0]["dPlanAmount"]).ToString();
                                            objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["picRed"].ObjectFormat.EnableSuppress = true;
                                            objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["picGreen"].ObjectFormat.EnableSuppress = false;
                                            objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["Text10"].ObjectFormat.EnableSuppress = false;
                                        }
                                        else if (Convert.ToDecimal(dsMain.Tables[10].Rows[0]["PlanStatus"]) == 2)
                                        {
                                            objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["txtPayPlan"].ObjectFormat.EnableSuppress = false;
                                            ((TextObject)objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["txtPayPlan"]).Text = Convert.ToDouble(dsMain.Tables[10].Rows[0]["dPlanAmount"]).ToString();
                                            objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["picRed"].ObjectFormat.EnableSuppress = false;
                                            objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["picGreen"].ObjectFormat.EnableSuppress = true;
                                            objrptPatientStatementForGateWayEDI_GW.ReportHeaderSection2.ReportObjects["Text10"].ObjectFormat.EnableSuppress = false;
                                        }
                                    }
                                }
                                objrptPatientStatementForGateWayEDI_GW.SetDataSource(dsPatientStatementMain);
                                objrptPatientStatementForGateWayEDI_GW.Refresh();
                            }
                            else
                            {
                                objrptPatientStatementForGateWayEDI.SetDataSource(dsPatientStatementMain);
                                objrptPatientStatementForGateWayEDI.Refresh();
                            }

                            if (action == StatementAction.ViewOnly)
                            {
                                DisableMouse();
                                //crvReportViewer.CloseView(null);
                                //crvReportViewer.RefreshReport();
                                crvReportViewer.Enabled = false;

                                if (_isGWPatientStatement)
                                {
                                    crvReportViewer.ReportSource = objrptPatientStatementForGateWayEDI_GW;
                                }
                                else
                                {
                                    crvReportViewer.ReportSource = objrptPatientStatementForGateWayEDI;
                                }

                                crvReportViewer.Enabled = true;
                                //if (!_isPatientBatchPrint)
                                //{
                                    crvReportViewer.Show();
                                //}
                                //System.Threading.Thread.Sleep(nDelay);
                                EnableMouse();
                            }
                        }

                        if (action != StatementAction.ViewOnly)
                        {
                            #region "Save Patient Statement Report to DB "
                            byte[] myBinary;
                            if (_isGWPatientStatement)
                            {
                                if (objrptPatientStatementForGateWayEDI_GW != null && objrptPatientStatementForGateWayEDI_GW.IsLoaded)
                                {
                                    string templateName = "PatientStatement_" + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + "_To_" + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + "_" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + System.DateTime.Now.Millisecond;
                                    using (Stream fs = objrptPatientStatementForGateWayEDI_GW.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows))
                                    {
                                        myBinary = new byte[fs.Length];
                                        fs.Seek(0, System.IO.SeekOrigin.Begin);
                                        fs.Read(myBinary, 0, (int)fs.Length);
                                        SavePatientStatementReport(templateName, patientID, batchID, accountID, patientDue, myBinary);
                                        //myBinary = null;
                                        fs.Close();
                                    }

                                 

                                    if (action == StatementAction.PrintOnly)
                                    {
                                        if (!PrintAsDoc(myBinary))
                                        {
                                            if (printSettings == null)
                                            {
                                                printSettings = new System.Drawing.Printing.PrinterSettings();
                                            }
                                            objrptPatientStatementForGateWayEDI_GW.PrintOptions.PrinterName = printSettings.PrinterName;
                                            objrptPatientStatementForGateWayEDI_GW.PrintToPrinter(printSettings.Copies, printSettings.Collate, printSettings.FromPage, printSettings.ToPage);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (objrptPatientStatementForGateWayEDI != null && objrptPatientStatementForGateWayEDI.IsLoaded)
                                {
                                    string templateName = "PatientStatement_" + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + "_To_" + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + "_" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + System.DateTime.Now.Millisecond;
                                    using (Stream fs = objrptPatientStatementForGateWayEDI.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows))
                                    {
                                        myBinary = new byte[fs.Length];
                                        fs.Seek(0, System.IO.SeekOrigin.Begin);
                                        fs.Read(myBinary, 0, (int)fs.Length);
                                        SavePatientStatementReport(templateName, patientID, batchID, accountID, patientDue, myBinary);
                                        //myBinary = null;
                                        fs.Close();
                                    }

                                    if (action == StatementAction.PrintOnly)
                                    {
                                        if (!PrintAsDoc(myBinary))
                                        {
                                            if (printSettings == null)
                                            {
                                                printSettings = new System.Drawing.Printing.PrinterSettings();
                                            }
                                            objrptPatientStatementForGateWayEDI.PrintOptions.PrinterName = printSettings.PrinterName;
                                            objrptPatientStatementForGateWayEDI.PrintToPrinter(printSettings.Copies, printSettings.Collate, printSettings.FromPage, printSettings.ToPage);
                                        }
                                    }
                                }
                            }
                            myBinary = null;
                          
                            #endregion
                        }

                        if (action == StatementAction.SendOnly)
                        {
                            #region "Generating an Electronic file details"

                            // Statement Header : Fill - Practice, Remit & Guarantor Info
                            sbMainFile.Append(GetStatementHeader(accountID, patientID, statementDate, settings).ToString());

                            #endregion
                        }
                       
                        //here new code added form email regarding statment
                        if (IsStatementNotificationOn)
                        {
                            if (action == StatementAction.SendOnly || action == StatementAction.PrintOnly)
                            {
                                dtPortalStatementEmailAccountIds.Rows.Add(accountID);
                            }
                        }
                        //Bug #61484: gloPM - Patient Statement- Application increments statement count without sending patient statement 
                        //statement Count will be increment only for send and print not for view
                        if (action!=StatementAction.ViewOnly)
                        {
                           SetupFollowUp(accountID, patientID, patientDue); 
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at GenerateStatement : " + ex.ToString(), false);
                }

                if (action == StatementAction.SendOnly)
                {
                    // Statement File - Footer 
                    sbMainFile.Append("@EndStatementFile");

                    #region "Generate and Save Electronic file "
                    
                    //Bug #61582: gloPM - Patient Statement- Application does not create new folder for each statement/batch at server path in claim management folder 
                    //Description: Pass BatchID instead of batch name to find batch name.
                    string fileName = GetStatementFileName(batchID);
                    System.IO.File.WriteAllText(fileName, sbMainFile.ToString());

                    SavePatientStatementFile(batchID, fileName);

                    #endregion

                    prgFileGeneration.PerformStep();
                    Application.DoEvents();
                    if (IsStatementNotificationOn)
                    {
                        clsgloPatientPortalEmail oclsgloPatientPortalEmail = new clsgloPatientPortalEmail(_databaseconnectionstring);
                        oclsgloPatientPortalEmail.SendBatchPortalEmail(dtPortalStatementEmailAccountIds, gloGlobal.gloPMGlobal.ClinicID, "PatientStatements");
                    }
                    MessageBox.Show("Generation of Batch Done.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (action == StatementAction.PrintOnly && !_IsCancel)
                {
                    if (IsStatementNotificationOn)
                    {
                        clsgloPatientPortalEmail oclsgloPatientPortalEmail = new clsgloPatientPortalEmail(_databaseconnectionstring);
                        oclsgloPatientPortalEmail.SendBatchPortalEmail(dtPortalStatementEmailAccountIds, gloGlobal.gloPMGlobal.ClinicID, "PatientStatements");
                    }
                    MessageBox.Show("Printing of batch done.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at ProcessIndividualStatement: " + ex.ToString(), false);
            }
            finally
            {
                if (sbMainFile != null) { sbMainFile = null; }
                if (objStatement != null) { objStatement.Dispose(); objStatement = null; }
                if (printSettings != null) { printSettings = null; }
                if (gloGlobal.gloTSPrint.isCopyPrint)
                {
                    gloWord.LoadAndCloseWord.cleatTSPrintPopupDetails();
                }

                //SetButtonVisibility("SendBatch");
                tsb_PatAcctAccount.Visible = false;

                //ToDo : Need to check
                FillBatchDetails();

                pnlPleasewait.Visible = false;
                pnlProgressBar.Visible = false;
                prgFileGeneration.Visible = false;

                if (this.Parent != null)
                { this.Parent.Cursor = Cursors.Default; }

                this.Cursor = Cursors.Default;
            }
        }

        private bool PrintAsDoc(byte[] stmtBytes)
        {
            bool Copied = false;
            if (gloGlobal.gloTSPrint.isCopyPrint)
            {
                gloWord.LoadAndCloseWord objWord = new gloWord.LoadAndCloseWord();
                Microsoft.Office.Interop.Word.Document wd = default(Microsoft.Office.Interop.Word.Document);
                string outputFileName = gloGlobal.clsFileExtensions.NewDocumentName(gloGlobal.gloTSPrint.TempPath, ".doc", "MMddyyyyHHmmssffff");
                try
                {
                    gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(_databaseconnectionstring);
                    object objTemplateDocument;
                    objTemplateDocument = stmtBytes;
                    ogloTemplate.ConvertBinaryToFile(objTemplateDocument, outputFileName);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while copy file in DocumentPrintOut. " + ex.Message, false);
                    throw;
                }
                wd = objWord.LoadWordApplication(outputFileName);
                //oFileName = (object)sFileName;
                //oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                //wd.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                Copied = gloWord.LoadAndCloseWord.CopyPrintDoc(wd, 0, bClearPopUp:false);
                objWord.CloseWordApplication(ref wd);
                objWord = null;
            }
            return Copied;
        }

        //private void ConverttoPDF()
        //{
        //    ReportDocument crReportDocument = (ReportDocument)crvReportViewer.ReportSource;
        //    DiskFileDestinationOptions rptFileDestOption = new DiskFileDestinationOptions();
        //    PdfRtfWordFormatOptions rptFormatOption = new PdfRtfWordFormatOptions();
        //    string[] Nm = crvReportViewer.ReportSource.ToString().ToLower().Split('.');
        //    string reportFileName = "";
        //    reportFileName = gloSettings.FolderSettings.AppTempFolderPath + Nm[2].ToString() + ".PDF";
        //    rptFileDestOption.DiskFileName = reportFileName;
        //    crReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, reportFileName);
        //    Print(reportFileName, Nm[2].ToString(), "Success", true);          
        //}

        //private void Print(string _PDFFileName, string rptName, string msgboxcaption, Boolean blnPrintDialog)
        //{



        //    gloPrintDialog.gloPrintProgressController ogloPrintProgressController = null;

        //    try
        //    {
        //        using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog(true))
        //        {
        //            oDialog.ConnectionString = _databaseconnectionstring;
        //            oDialog.TopMost = true;
        //            oDialog.ShowPrinterProfileDialog = true;


        //            oDialog.ModuleName = "CrystalReports";
        //            oDialog.RegistryModuleName = "CrystalReports";
        //            System.Drawing.Printing.PrinterSettings _printsettings = new PrinterSettings();
        //            oDialog.PrinterSettings = _printsettings;
        //            if (blnPrintDialog == false)
        //            {
        //                oDialog.bUseDefaultPrinter = true;
        //            }
        //            if (oDialog != null)
        //            {


        //                IntPtr handle = GetActiveWindow();
        //                Control NetControl = null;
        //                if(!handle.Equals(0))
        //                {
        //                    NetControl = ControlFromHandle(handle);
        //                }
                        
        //                try
        //                {
                            
        //                    FileStream fs = new FileStream(_PDFFileName, FileMode.Open, FileAccess.Read);
        //                    StreamReader r = new StreamReader(fs);
        //                    string pdfText = r.ReadToEnd();
        //                    Regex rx1 = new Regex(@"/Type\s*/Page[^s]");
        //                    MatchCollection matches = rx1.Matches(pdfText);
        //                    fs.Close();
        //                    fs.Dispose();
        //                    r.Close();
        //                    r.Dispose();
        //                    fs = null;
        //                    r = null;
        //                    pdfText = null;
        //                    if (matches.Count > 0)
        //                    {
        //                        oDialog.AllowSomePages = true;
        //                        oDialog.PrinterSettings.ToPage = matches.Count;
        //                        oDialog.PrinterSettings.FromPage = 1;
        //                    }
        //                }
        //                catch
        //                {
        //                }
        //                if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //                {
        //                    if (Convert.ToBoolean(appSettings["DefaultPrinter"]))
        //                    {
        //                        oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
        //                        oDialog.CustomPrinterExtendedSettings.IsShowProgress = true;
        //                    }
        //                    if (rptName.Contains("rptPatientPrescriptions") || rptName.Contains("rpteRx") || rptName.Contains("rptFinanciaDetails") || rptName.Contains("rptCPTAnalysisDetailsReport") || rptName.Contains("PrescriptionUsageReport") || rptName.Contains("rptPatientList") || rptName.Contains("rptMU") || rptName.Contains("rptMU_stage1") || rptName.Contains("rptMU_stage2") || rptName.Contains("rptLabFlowSheet") || rptName.Contains("RptPatientImmunizationSummary") || rptName.Contains("rptPatientImmSummaryByTrade") || rptName.Contains("RptVaccineInventory") || rptName.Contains("rpt_InsurancePymtLog") || rptName.Contains("%ExpCollectionReport") || rptName.Contains("rptAgingReport") || rptName.Contains("rptFinancialReport") || rptName.Contains("rptAvailableReserves") || rptName.Contains("rptDailyCloseSummary") || rptName.Contains("rptMonthlyCloseSummary") || rptName.Contains("rptFinProReport") || rptName.Contains("rptFinProReport - ICD") || rptName.Contains("rptMissOppReport") || rptName.Contains("RptBatchEligiility") || rptName.Contains("rptPriorReport") || rptName.Contains("rptExcludePatientDue") || rptName.Contains("rptBatchReport") || rptName.Contains("rptFeeScheduleUnderReimbursement") || rptName.Contains("rptBatchLagReport"))
        //                    {
        //                        oDialog.PrinterSettings.DefaultPageSettings.Landscape = true;
        //                    }



        //                    ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController(_PDFFileName, oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings);

        //                    if (!handle.Equals(0))
        //                    {
        //                        ogloPrintProgressController.ShowProgress(NetControl);
        //                    }
        //                    else { ogloPrintProgressController.ShowProgress(this); }




        //                }
        //                else 
        //                {
        //                    _IsCancel = true;
        //                }

        //            }
        //            else
        //            {
        //                string _ErrorMessage = "Error in Showing Print Dialog";

        //                if (_ErrorMessage.Trim() != "")
        //                {
        //                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;

        //                    _MessageString = "";
        //                }


        //                MessageBox.Show(_ErrorMessage, msgboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        #region " Make Log Entry "

        //        string _ErrorMessage = ex.ToString();
        //        //Code added on 7rd October 2008 By - Sagar Ghodke
        //        //Make Log entry in DMSExceptionLog file for any exceptions found
        //        if (_ErrorMessage.Trim() != "")
        //        {
        //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
        //            //  gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
        //            _MessageString = "";
        //        }

        //        //End Code add
        //        #endregion " Make Log Entry "

        //        MessageBox.Show(ex.Message, msgboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        ex = null;
        //    }
        //    finally
        //    {

        //    }

        //}
        //private static Control ControlFromHandle(IntPtr hWND)
        //{
        //    while (hWND != IntPtr.Zero)
        //    {
        //        Control control = Control.FromChildHandle(hWND);
        //        if (control != null)
        //        {
        //            if (control is Form)
        //            {
        //                Control childControl = ((Form)control).ActiveControl;
        //                if (childControl != null)
        //                {
        //                    control = childControl;
        //                }
        //            }
        //        }
        //        if (control != null)
        //            return control;

        //        hWND = GetParent(hWND);

        //        //  Control control2 = System.Windows.Forms.Control.FromChildHandle(hWND);
        //        // IntPtr hwnd = (IntPtr)this.Handle.ToPointer();
        //    }

        //    return null;
        //}
        /// <summary>
        /// ProcessBatchStatement is used only for sending and printing the batch.
        /// </summary>
        /// <param name="action">Action is SendOnly and PrintOnly</param>
        private void ProcessBatchStatement(StatementAction action)
        {
            StringBuilder sbMainFile = new StringBuilder();
            gloStatment objStatement = new gloStatment();
            DataTable dtPortalStatementEmailAccountIds = null;
            System.Drawing.Printing.PrinterSettings printSettings = null;

            try
            {
                #region "Set progress bar"

                int rowsSelected = 0;

                for (int i = 1; i < c1PatientList.Rows.Count; i++)
                { if (c1PatientList.GetCellCheck(i, c1PatientList.Cols["Select"].Index) == CheckEnum.Checked) { rowsSelected += 1; } }

                prgFileGeneration.Maximum = rowsSelected;
                prgFileGeneration.Minimum = 0;
                prgFileGeneration.Value = 0;
                prgFileGeneration.Step = 1;

                if (rowsSelected > 0)
                {
                    pnlProgressBar.Visible = true;
                    prgFileGeneration.Visible = true;
                    if (this.Parent != null) { this.Parent.Cursor = Cursors.WaitCursor; }
                    this.Cursor = Cursors.WaitCursor;
                    pnlPleasewait.Visible = true;
                }

                #endregion

                int statementDate = GetStatementDate();
                Int64 businessCenterID = GetBusinessCenterID();
                Int64 criteriaID = GetStatementCriteriaID(businessCenterID);
                gloStatment.StatementSettings settings = objStatement.GetStatementSettings();

                #region "Creating a batch "

                Int64 batchID = 0;
                string batchName = string.Empty;

                if (rowsSelected > 0)
                {
                    batchID = CreateBatchMaster(out batchName);

                    if (batchID == 0)
                    {
                        MessageBox.Show("Unable to generate batch. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select atleast one");
                    return;
                }

                #endregion

                if (action == StatementAction.PrintOnly)
                {
                    if (!Convert.ToBoolean(appSettings["DefaultPrinter"]))
                    {
                        if (gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            if (!gloWord.LoadAndCloseWord.showTSPrintPopup())
                            {
                                return;
                            }
                        }
                        else
                        {
                            using (PrintDialog _PrintDialog = new PrintDialog())
                            {
                                DialogResult res = _PrintDialog.ShowDialog(this);
                                if (res == System.Windows.Forms.DialogResult.Cancel)
                                { return; }
                                else
                                { printSettings = _PrintDialog.PrinterSettings; }
                            }
                        }
                    }
                }
                else if (action == StatementAction.SendOnly)
                {
                    // Statement File - Header
                    sbMainFile.Append(GetStatementFileHeader(statementDate, settings));
                }

                decimal patientDue = 0;
             //   Boolean IsBatchDone = true;
                Int64 patientID = 0;
                Int64 accountID = 0;
                int statementCount = 0;
                if (IsStatementNotificationOn)
                {
                    dtPortalStatementEmailAccountIds = new DataTable();
                    dtPortalStatementEmailAccountIds.Columns.Add(new DataColumn("nAccountID", typeof(long)));
                }
                foreach (Row row in c1PatientList.Rows)
                {
                    if (row.Index != 0 && (Convert.ToBoolean(c1PatientList.GetData(row.Index, c1PatientList.Cols["Select"].Index)) == true))
                    {
                        prgFileGeneration.PerformStep();
                        // Progress bar setup
                        if (action == StatementAction.PrintOnly)
                        { lblFile.Text = "Printing Batch " + prgFileGeneration.Value + "/" + rowsSelected; }
                        else if (action == StatementAction.SendOnly)
                        { lblFile.Text = "Processing Batch " + prgFileGeneration.Value + "/" + rowsSelected; }

                        
                        Application.DoEvents();

                        try
                        {
                            using (dsPatientStatementMain = new dsRevisedPatientStatement())
                            {
                                accountID = Convert.ToInt64(c1PatientList.GetData(row.Index, c1PatientList.Cols["nPAccountId"].Index));
                            
                                statementCount = GetStatementCount(accountID, statementDate); 

                                if (IsPatientAccountEnable)
                                {
                                    patientID = 0;
                                    patientDue = Convert.ToDecimal(c1PatientList.GetData(row.Index, "sAccountDue"));
                                }
                                else
                                {
                                    patientID = Convert.ToInt64(c1PatientList.GetData(row.Index, c1PatientList.Cols["PatientID"].Index));
                                    patientDue = Convert.ToDecimal(c1PatientList.GetData(row.Index, "spatientDue"));
                                }
                                
                                FillStatementDetailsRevised(accountID, patientID, criteriaID, statementDate, statementCount, true);
                                decimal chkpatientDue = 0;
                                if ((dsPatientStatementMain.Tables["dt_PatientReserve"] != null) && (dsPatientStatementMain.Tables["dt_PatientReserve"].Rows.Count > 0))
                                {
                                    chkpatientDue = Convert.ToDecimal(dsPatientStatementMain.Tables["dt_PatientReserve"].Rows[0]["PatientDue"]) - Convert.ToDecimal(dsPatientStatementMain.Tables["dt_PatientReserve"].Rows[0]["AvailableReserve"]);
                                    
                                }

                                if (chkpatientDue <= 0)
                                {
                                    //IsBatchDone = false;
                                    continue;
                                }

                                #region "Generate and Save Statement to DB"

                                if (!object.ReferenceEquals(dsPatientStatementMain, null))
                                {
                                    objrptPatientStatementForGateWayEDI.SetDataSource(dsPatientStatementMain);
                                    objrptPatientStatementForGateWayEDI.Refresh();
                                }

                                if (objrptPatientStatementForGateWayEDI != null && objrptPatientStatementForGateWayEDI.IsLoaded)
                                {
                                    string templateName = "PatientStatement_" + gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString()) + "_To_" + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + "_" + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + System.DateTime.Now.Millisecond;
                                    byte[] myBinary;
                                    using (Stream fs = objrptPatientStatementForGateWayEDI.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows))
                                    {
                                        myBinary = new byte[fs.Length];
                                        fs.Seek(0, System.IO.SeekOrigin.Begin);
                                        fs.Read(myBinary, 0, (int)fs.Length);
                                        SavePatientStatementReport(templateName, patientID, batchID, accountID, patientDue, myBinary);
                                        //myBinary = null;
                                        fs.Close();
                                    }
                                   

                                    if (action == StatementAction.PrintOnly)
                                    {
                                        if (!PrintAsDoc(myBinary))
                                        {
                                            if (printSettings == null)
                                            {
                                                printSettings = new System.Drawing.Printing.PrinterSettings();
                                            }
                                            objrptPatientStatementForGateWayEDI.PrintOptions.PrinterName = printSettings.PrinterName;
                                            objrptPatientStatementForGateWayEDI.PrintToPrinter(printSettings.Copies, printSettings.Collate, printSettings.FromPage, printSettings.ToPage);
                                        }
                                    }
                                    myBinary = null;
                                }

                                #endregion

                                if (action == StatementAction.SendOnly)
                                {
                                    #region "Generating an Electronic file details"

                                    // Statement Header : Fill - Practice, Remit & Guarantor Info
                                    sbMainFile.Append(GetStatementHeader(accountID, patientID, statementDate, settings).ToString());

                                    #endregion
                                }
                                // here new code added form email regarding stament
                                if (IsStatementNotificationOn)
                                {
                                    if (action == StatementAction.SendOnly || action == StatementAction.PrintOnly)
                                    {
                                        dtPortalStatementEmailAccountIds.Rows.Add(accountID);
                                    }
                                }
                                SetupFollowUp(accountID, patientID, patientDue);

                                if (dsPatientStatementMain != null)
                                {
                                    for (int c = dsPatientStatementMain.Tables.Count-1; c >= 0; c--)
                                    {
                                        //SLR: Problem to be fixed on 4/2/2014
                                        //SLR:  fixed on 10/7/2014
                                        for (int t = dsPatientStatementMain.Tables[c].Columns.Count - 1; t >= 0; t--)
                                        {
                                            dsPatientStatementMain.Tables[c].Columns[t].Dispose();
                                            dsPatientStatementMain.Tables[c].Columns.RemoveAt(t);
                                        }
                                        dsPatientStatementMain.Tables[c].Dispose();
                                        dsPatientStatementMain.Tables.RemoveAt(c);
                                    }
                                    dsPatientStatementMain.Tables.Clear();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at GenerateStatement : " + ex.ToString(), false);
                        }
                    }
                }
                if (action == StatementAction.SendOnly)
                {
                    // Statement File - Footer 
                    sbMainFile.Append("@EndStatementFile");

                    #region "Generate and Save Electronic file "
                    
                    //Bug #61582: gloPM - Patient Statement- Application does not create new folder for each statement/batch at server path in claim management folder 
                    //Description: Pass BatchID instead of batch name to find batch name.
                    string fileName = GetStatementFileName(batchID);
                    System.IO.File.WriteAllText(fileName, sbMainFile.ToString());

                    SavePatientStatementFile(batchID, fileName);

                    #endregion
                }


                if (action == StatementAction.SendOnly)
                {
                    if (IsStatementNotificationOn)
                    {
                        clsgloPatientPortalEmail oclsgloPatientPortalEmail = new clsgloPatientPortalEmail(_databaseconnectionstring);
                        oclsgloPatientPortalEmail.SendBatchPortalEmail(dtPortalStatementEmailAccountIds, gloGlobal.gloPMGlobal.ClinicID, "PatientStatements");
                    }
                    MessageBox.Show("Generation of Batch Done.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (action == StatementAction.PrintOnly)
                {
                    if (IsStatementNotificationOn)
                    {
                        clsgloPatientPortalEmail oclsgloPatientPortalEmail = new clsgloPatientPortalEmail(_databaseconnectionstring);
                        oclsgloPatientPortalEmail.SendBatchPortalEmail(dtPortalStatementEmailAccountIds, gloGlobal.gloPMGlobal.ClinicID, "PatientStatements");
                    }
                    MessageBox.Show("Printing of batch done.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at ProcessIndividualStatement: " + ex.ToString(), false);
            }
            finally
            {
                if (sbMainFile != null) { sbMainFile = null; }
                if (objStatement != null) { objStatement.Dispose(); objStatement = null; }
                if (printSettings != null) { printSettings = null; }
                if (gloGlobal.gloTSPrint.isCopyPrint)
                {
                    gloWord.LoadAndCloseWord.cleatTSPrintPopupDetails();
                }

                SetButtonVisibility("SendBatch");
                tsb_PatAcctAccount.Visible = false;

                //ToDo : Need to check
                FillBatchDetails();

                pnlPleasewait.Visible = false;
                pnlProgressBar.Visible = false;
                prgFileGeneration.Visible = false;

                if (this.Parent != null)
                { this.Parent.Cursor = Cursors.Default; }

                this.Cursor = Cursors.Default;
            }
        }
        
        //Unused kept for reference.
        private void FillStatementDetails(Int64 AccountID, Int64 PatientID, Int64 StatementCriteriaID, int StatementDate, int StatementCount, bool IncludeElectronicFileIno)
        {
            SqlConnection oConnection = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            gloStatment objStatement = new gloStatment();

            DataTable dtlocal = null;
            DataSet _dsAgeing = null;
            DataSet _dslocal = new DataSet();

            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;

                #region "Fetch Display Settings, PayToInfo, GuarantorInfo, StatementNotes"

                dtlocal = objStatement.GetDisplaySettings(AccountID, PatientID, StatementCriteriaID);
                if (dtlocal != null && dtlocal.Rows.Count > 0) { dsPatientStatementMain.Tables["dt_DisplaySettings"].Merge(dtlocal); dtlocal.Clear(); }

                dtlocal = objStatement.GetRemitDetails(AccountID, PatientID, StatementCriteriaID, StatementCount);
                if (dtlocal != null && dtlocal.Rows.Count > 0) { dsPatientStatementMain.Tables["dt_RemitInfo"].Merge(dtlocal); dtlocal.Clear(); }

                dtlocal = objStatement.GetPayToInfo(AccountID, PatientID, StatementCriteriaID);
                if (dtlocal != null && dtlocal.Rows.Count > 0) { dsPatientStatementMain.Tables["dt_PayTo"].Merge(dtlocal); dtlocal.Clear(); }

                string statementNotes = string.Empty;
                using (DataSet dsGuarantorInfo = objStatement.GetGuarantorInfo(AccountID, StatementDate))
                {
                    if (dsGuarantorInfo != null && dsGuarantorInfo.Tables.Count >= 3)
                    {
                        if (IncludeElectronicFileIno)
                        {
                            dsGuarantorInfo.Tables[1].TableName = "dt_GuarantorInfo";
                            dsPatientStatementMain.Merge(dsGuarantorInfo.Tables[1]);
                        }

                        foreach (DataRow row in dsGuarantorInfo.Tables[2].Rows)
                        { statementNotes = statementNotes + Convert.ToString(row["sStatementNote"]); }
                    }
                }

                if (statementNotes != string.Empty)
                {
                    dsPatientStatementMain.Tables["dt_StatementNote"].Rows.Add();
                    dsPatientStatementMain.Tables["dt_StatementNote"].Rows[0][0] = statementNotes;
                }
                else
                {
                    dsPatientStatementMain.Tables["dt_StatementNote"].Rows.Add();
                    dsPatientStatementMain.Tables["dt_StatementNote"].Rows[0][0] = "";
                }

                #endregion

                #region "Fetch Patient Statement Info"

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "PA_RPT_PatientStatement_Revised_V2";

                SqlParameter ParaPatientID = new SqlParameter();
                {
                    ParaPatientID.ParameterName = "@nPatientID";
                    ParaPatientID.Value = PatientID;
                    ParaPatientID.Direction = ParameterDirection.Input;
                    ParaPatientID.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParaPatientID);
                ParaPatientID = null;

                SqlParameter ParaEndDate = new SqlParameter();
                {
                    ParaEndDate.ParameterName = "@dtDate";
                    ParaEndDate.Value = gloDateMaster.gloDate.DateAsDate(StatementDate);
                    ParaEndDate.Direction = ParameterDirection.Input;
                    ParaEndDate.SqlDbType = SqlDbType.DateTime;
                }
                sqlCmd.Parameters.Add(ParaEndDate);
                ParaEndDate = null;

                SqlParameter ParAccount = new SqlParameter();
                {
                    ParAccount.ParameterName = "@nPAccountID";
                    ParAccount.Value = AccountID;
                    ParAccount.Direction = ParameterDirection.Input;
                    ParAccount.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParAccount);
                ParAccount = null;

                SqlParameter ParCriteriaId = new SqlParameter();
                {
                    ParCriteriaId.ParameterName = "@nCriteriaId";
                    ParCriteriaId.Value = StatementCriteriaID;
                    ParCriteriaId.Direction = ParameterDirection.Input;
                    ParCriteriaId.SqlDbType = SqlDbType.BigInt;
                }
                sqlCmd.Parameters.Add(ParCriteriaId);
                ParCriteriaId = null;

                sqlCmd.Connection = oConnection;
                sqlCmd.CommandTimeout = 0;

                da = new SqlDataAdapter(sqlCmd);
                da.Fill(_dslocal);

                if (_dslocal != null)
                {
                    if (_dslocal.Tables.Count >= 1)
                    {
                        dsPatientStatementMain.Tables["dt_PatientStatement_Revised"].Merge(_dslocal.Tables[0], true, MissingSchemaAction.Ignore);
                    }
                    if (_dslocal.Tables.Count >= 2)
                    {
                        _dslocal.Tables[1].TableName = "dt_PatientStatement_Revised1";
                        dsPatientStatementMain.Merge(_dslocal.Tables[1]);
                    }
                }

                #endregion

                #region "Fetch Ageing Bucket Info and patient account balance"

                oDBParameters.Clear();

                if (PatientID == 0)
                { oDBParameters.Add("@nPatientID", DBNull.Value, ParameterDirection.Input, SqlDbType.BigInt); }
                else
                { oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt); }

                oDBParameters.Add("@dtDate", gloDateMaster.gloDate.DateAsDate(StatementDate), ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@nClinicID", 1, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPAccountID", AccountID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("PA_BL_SELECT_AgingBuckets_V2", oDBParameters, out _dsAgeing);

                if (_dsAgeing != null)
                {
                    if (_dsAgeing.Tables.Count >= 1)
                    { dsPatientStatementMain.Tables["dt_AgeingBucket"].Merge(_dsAgeing.Tables[0]); }
                    if (_dsAgeing.Tables.Count >= 2)
                    { dsPatientStatementMain.Tables["dt_PatientReserve"].Merge(_dsAgeing.Tables[1]); }
                }

                #endregion "Fetch Ageing Bucket Info and patient account balance"

                #region "Fetch Electronic file Details"

                if (IncludeElectronicFileIno)
                {
                    using (DataTable dtLastAccountPayment = objStatement.GetLastAccountPayment(AccountID, PatientID, StatementDate))
                    {
                        if (dtLastAccountPayment != null && dtLastAccountPayment.Rows.Count > 0)
                        {
                            dtLastAccountPayment.TableName = "dt_LastAccountPayment";
                            dsPatientStatementMain.Merge(dtLastAccountPayment);
                        }
                    }
                }

                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at FillStatementDetails " + ex.ToString(), false);
            }
            finally
            {
                if (oConnection != null) { oConnection.Close(); oConnection.Dispose(); oConnection = null; }
                if (sqlCmd != null) { if (sqlCmd.Parameters != null) { sqlCmd.Parameters.Clear(); } sqlCmd.Dispose(); sqlCmd = null; }
                if (da != null) { da.Dispose(); da = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters = null; }
                if (_dsAgeing != null) { _dsAgeing.Dispose(); _dsAgeing = null; }
                if (_dslocal != null) { _dslocal.Dispose(); _dslocal = null; }
                if (dtlocal != null) { dtlocal.Dispose(); dtlocal = null; }
            }
        }

        #endregion

        private void CRV_ClickPage(object sender, CrystalDecisions.Windows.Forms.PageMouseEventArgs e)
        {
            string sGroupNamePath = null;
            string[] sGroupIDs = null;
            DataRow[] drRows = null;

            try
            {
                if (!_isGWPatientStatement)
                { return; }

                if (e.ObjectInfo.Name == "sDescription2")
                {
                    Int64 nPatientID = 0;
                    Int64 nBillingTransactionDetailID = 0;
                    Int64 nDetailID = 0;
                    Int64 nEOBID = 0;
                    Int64 nEOBPaymentID = 0;
                    Int64 nContactID = 0;
                    Int64 nBillingTransactionID = 0;
                    Int32 nParty = 0;
                    Int32 nSortType = 0;

                    sGroupNamePath = e.ObjectInfo.GroupNamePath.Trim('/');
                    sGroupIDs = sGroupNamePath.Split('/');
                    for (Int32 i = 0; i < sGroupIDs.Length; i++)
                    {
                        if (sGroupIDs[i].Contains("nPatientID"))
                        {
                            nPatientID = Convert.ToInt64(sGroupIDs[i].Replace("nPatientID", "").Replace("[", "").Replace("]", ""));
                        }
                        else if (sGroupIDs[i].Contains("nBillingTransactionDetailID"))
                        {
                            nBillingTransactionDetailID = Convert.ToInt64(sGroupIDs[i].Replace("nBillingTransactionDetailID", "").Replace("[", "").Replace("]", ""));
                        }
                        else if (sGroupIDs[i].Contains("nDetailID"))
                        {
                            nDetailID = Convert.ToInt64(sGroupIDs[i].Replace("nDetailID", "").Replace("[", "").Replace("]", ""));
                        }
                    }

                    if (dsMain != null && dsMain.Tables.Count > 0)
                    {
                        drRows = dsMain.Tables[0].Select("nBillingTransactionDetailID=" + nBillingTransactionDetailID + " AND nDetailID=" + nDetailID);

                        if (drRows != null && drRows.Length > 0)
                        {
                            nEOBID = Convert.ToInt64(drRows[0]["nEOBID"]);
                            nEOBPaymentID = Convert.ToInt64(drRows[0]["nEOBPaymentID"]);
                            nContactID = Convert.ToInt64(drRows[0]["nContactID"]);
                            nBillingTransactionID = Convert.ToInt64(drRows[0]["nBillingTransactionID"]);
                            nParty = Convert.ToInt32(drRows[0]["Party"]);
                            nSortType = Convert.ToInt32(drRows[0]["nSortType"]);
                        }
                    }


                    if (nParty == 1 && (nSortType == 1 || nSortType == 2))
                    {
                        gloAccountsV2.frmViewPatientPaymentV2 ofrmViewPatientPayment = new gloAccountsV2.frmViewPatientPaymentV2(gloGlobal.gloPMGlobal.DatabaseConnectionString, nPatientID, gloGlobal.gloPMGlobal.ClinicID, nEOBPaymentID);
                        ofrmViewPatientPayment.StartPosition = FormStartPosition.CenterScreen;
                        ofrmViewPatientPayment.ts_VoidPayment.Enabled = false;
                        ofrmViewPatientPayment.tls_btnReceipt.Enabled = false;
                        ofrmViewPatientPayment.ShowDialog(this);
                        ofrmViewPatientPayment.Dispose();
                        ofrmViewPatientPayment = null;

                    }
                    else if (nParty == 2 && (nSortType == 4 || nSortType == 7))
                    {
                        gloAccountsV2.frmViewClaimRemittanceV2 ofrmClaimChargeHistory = new gloAccountsV2.frmViewClaimRemittanceV2(gloGlobal.gloPMGlobal.DatabaseConnectionString, nPatientID, gloGlobal.gloPMGlobal.ClinicID, nEOBPaymentID, nEOBID, true);
                        ofrmClaimChargeHistory.StartPosition = FormStartPosition.CenterScreen;
                        ofrmClaimChargeHistory.IsVoidEOBPayment = false;
                        ofrmClaimChargeHistory.VoidRefEOBPaymentID = 0;
                        ofrmClaimChargeHistory.CallingContainer = this.Name;
                        ofrmClaimChargeHistory.TransactionID = nBillingTransactionID;
                        ofrmClaimChargeHistory.ContactID = nContactID;
                        ofrmClaimChargeHistory.tsbViewInsPmnt.Enabled = false;
                        ofrmClaimChargeHistory.ShowDialog(this);
                        ofrmClaimChargeHistory.Dispose();
                        ofrmClaimChargeHistory = null;
                    }

                }
                else if (e.ObjectInfo.Name == "AvailableReserve1")
                {
                    if (e.ObjectInfo.Text != "$0.00")
                    {
                        gloAccountsV2.frmViewAvailableReserveV2 ofrmViewPatientPayment = new gloAccountsV2.frmViewAvailableReserveV2(gloGlobal.gloPMGlobal.DatabaseConnectionString, _selectedPatientID, gloGlobal.gloPMGlobal.ClinicID, _selectedAccountID);
                        ofrmViewPatientPayment.StartPosition = FormStartPosition.CenterScreen;
                        ofrmViewPatientPayment.ShowDialog(this);
                        ofrmViewPatientPayment.Dispose();
                        ofrmViewPatientPayment = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception at CRV_ClickPage " + ex.ToString(), false);
                ex = null;
            }
            finally
            {
                sGroupNamePath = null;
                sGroupIDs = null;
                drRows = null;
            }

        }
    }
}
