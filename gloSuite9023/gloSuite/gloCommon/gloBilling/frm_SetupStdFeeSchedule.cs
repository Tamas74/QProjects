using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using System.Data.OleDb;
using C1.Win.C1FlexGrid;
using System.Collections;
 
namespace gloBilling
{
    public partial class frmSetupStdFeeSchedule : Form
    {

        #region "Private Variables"

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "";

        public gloGridListControl ogloGridListControl = null;
       // private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

        private Int64 _StdFeeScheeduleID = 0;       
        private Int64 _ClinicID = 0;      
        private string _Specialitycode = "";
        private string _CPTCode = "";        
        private string _Schedulename = "";        
        private bool _bIsValidated = false;
        private DataTable _RemovedCharged = new DataTable();        
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private bool _IsCopyFeeSchedule = false;
        private bool _IsValidate = true;
       // private bool _IsParametersValidated = true;
        Panel pnlContainedGloListcontrol = new Panel();
        private bool _IsCptSelectedFromgloList = false;
        private int iPreCol = 0;
        private int iPreRow = 0;
        public delegate void onGridSelChanged(object sender, RangeEventArgs e);
        ClsFeeSchedule oClsFeeSchedule = new ClsFeeSchedule(gloGlobal.gloPMGlobal.DatabaseConnectionString);

        private bool isOpenForEdit = false;
        private gloListControl.gloListControl oListControl;
        DataTable dtInsuranceCompany = null;

        #endregion "Private Variables"

        #region "Properties"

        public Int64 StdFeeScheeduleID
        {
            get { return _StdFeeScheeduleID; }
            set { _StdFeeScheeduleID = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public bool IsInternalControlActive
        {
            get { return pnlInternalControl.Visible; }
            set
            {
                pnlInternalControl.Visible = value;

                if (!pnlInternalControl.Visible)
                {
                    CloseInternalControl();
                }
            }
        }
        #endregion

        #region "Column Declaration"

        private const int COL_FEE_SCHEDULE_ID = 0;        
        private const int COL_HCPCS = 1;
      //  private const int COL_HCPCS_DESC = 2;
        private const int COL_MODIFIER = 2;
        private const int COL_MODIFIER_DESC = 3;
        private const int COL_NON_FACILITY_FEESCHEDULE_AMOUNT = 4;
        private const int COL_FACILITY_FEESCHEDULE_AMOUNT = 5;
        private const int COL_SPECIALTY_ID = 6;
        private const int COL_SPECIALITY_DESC = 7;
        private const int COL_CLINIC_CHARGES = 8;
        private const int COL_LIMIT_CHARGES = 9;
        private const int COL_ALLOWED_CHARGES = 10;
        private const int COL_CHARGES_PERCENTAGE = 11;
        private const int COL_VARIANT_AMOUNT = 12;       
        private const int COL_FACILITY_CHARGE_AMOUNT = 14;
        private const int COL_NON_FACILITY_CHARGE_AMOUNT =13 ;
        private const int COL_COUNT = 15;



        #endregion

        #region " Contructors "

        public frmSetupStdFeeSchedule(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;

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
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
        }

        public frmSetupStdFeeSchedule(Int64 ID,string CPTCode,string Speciality,string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _StdFeeScheeduleID = ID;
            _CPTCode = CPTCode;
            _Specialitycode = Speciality;


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
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion

        }

        public frmSetupStdFeeSchedule(Int64 ID,string FeeScheduleName,string DatabaseConnectionString,bool IsCopyFeeSchedule)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _Schedulename = FeeScheduleName;
            _StdFeeScheeduleID = ID;            
            _IsCopyFeeSchedule = IsCopyFeeSchedule;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
           // this.pnlSpeciality.Height = 75;
           

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
        }      

        public frmSetupStdFeeSchedule(Int64 ID, string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _StdFeeScheeduleID = ID;
            
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
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
        }

        public frmSetupStdFeeSchedule(Int64 ID, string Schedulename, int Specialitycode,string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _StdFeeScheeduleID = ID;          
            _Schedulename = Schedulename;  
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            //this.pnlSpeciality.Height = 75;
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
        }

        #endregion

        #region "Form Load Event"

        private void frmSetupStdFeeSchedule_Load(object sender, EventArgs e)
        {
            
            gloC1FlexStyle.Style(c1StdFeeSchedule, false);
            DesignGrid();
            ts_GenerateEOBFeeSchedule.Visible = false;
            mskEobStartDate.Text = System.DateTime.Now.AddYears(-1).ToString("MM/dd/yyyy");
            mskEobToDate.Text = System.DateTime.Now.ToString("MM/dd/yyyy");
            if (_StdFeeScheeduleID != 0)
            {
                isOpenForEdit = true;
                pnlEOBFeeSchedule.Visible = false;
                ts_btnAddLine.Visible = true;
                ts_btnRemoveLine.Visible = true;
                lbl_Keyremoveline.Visible = true;
                lbl_lshrtctKeyremoveline.Visible = true;
                lbl_KeyAddline.Visible = true;
                lbl_shrtctKeyAddline.Visible = true;
                txt_StdFeeScheduleType.Enabled = true;
                txt_StdFeeScheduleType.Text = _Schedulename;
                GetStdFeeSchedule();

                c1StdFeeSchedule.Cols[COL_HCPCS].AllowEditing = false;
                c1StdFeeSchedule.Cols[COL_MODIFIER].AllowEditing = false;
                c1StdFeeSchedule.Cols[COL_SPECIALTY_ID].AllowEditing = false;
                c1StdFeeSchedule.Cols[COL_FACILITY_CHARGE_AMOUNT].AllowEditing = true;
                c1StdFeeSchedule.Cols[COL_NON_FACILITY_CHARGE_AMOUNT].AllowEditing = true;
                //_IsParametersValidated = true;

            }
            else
            {
                isOpenForEdit = false;
                pnlEOBFeeSchedule.Visible = true;
                GetStdFeeSchedule();
                ts_btnRemoveLine.Visible = false;
            }
            txt_StdFeeScheduleType.Focus();
            _RemovedCharged.Columns.Add("FeeSchduleType");
            _RemovedCharged.Columns.Add("FeeSheduleID");
            _RemovedCharged.Columns.Add("Modifier");
            if (_IsCopyFeeSchedule)
            {
                txt_StdFeeScheduleType.Text = "";
                mskStartDate.Text = "";
                mskEndDate.Text = "";
            }
            RdbSetCharges.Checked = true;
            rdbNone.Checked = true;   
        }

        #endregion

        #region " List Control Methods & Events "   
        void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                CloseInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { }
        }
        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            try
            {

                C1FlexGrid _c1flexGrid = new C1FlexGrid();
                string currentFlexcontrol = ControlType.ToString();

                _c1flexGrid = c1StdFeeSchedule;
                pnlContainedGloListcontrol = pnlInternalControl;


                if (ogloGridListControl != null)
                {
                    CloseInternalControl();
                }
                ogloGridListControl = new gloGridListControl(ControlType, false, pnlContainedGloListcontrol.Width + 500, RowIndex, ColIndex);
                ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                ogloGridListControl.ControlHeader = ControlHeader;
                pnlContainedGloListcontrol.Controls.Add(ogloGridListControl);
                ogloGridListControl.Dock = DockStyle.Fill;
                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, SearchColumn.Code);
                }
                ogloGridListControl.Show();


                int _x = _c1flexGrid.Cols[ColIndex].Left;
                int _y = _c1flexGrid.Rows[RowIndex].Bottom;// +(RowIndex * 10);
                int _width = pnlContainedGloListcontrol.Width;
                int _height = pnlContainedGloListcontrol.Height;



                int _parentleft = pnlContainedGloListcontrol.Parent.Bounds.Left;
                int _parentwidth = pnlContainedGloListcontrol.Parent.Bounds.Width;
                int _parentsReamainingHieght = pnlContainedGloListcontrol.Parent.Height - _y;
                int _parentRemainingWidth = pnlContainedGloListcontrol.Parent.Width - _x;


                if (_parentsReamainingHieght > 0 && (_height - 10 > _parentsReamainingHieght))
                {
                    _y = _y - _height - 20;
                    pnlContainedGloListcontrol.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                else if (_parentsReamainingHieght < 0 && (_height - 10 > _parentsReamainingHieght))
                {
                    _y = _y - _height - 20 + _parentsReamainingHieght;
                    pnlContainedGloListcontrol.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                else
                {
                    pnlContainedGloListcontrol.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }

                //pnlContainedGloListcontrol.SetBounds(_c1flexGrid.Cols[ColIndex].Left, _y + _c1flexGrid.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                if (_width >= _parentRemainingWidth)
                {
                    pnlContainedGloListcontrol.Width = _parentRemainingWidth;
                    ogloGridListControl.Width = _parentRemainingWidth;
                }
                else
                {
                    pnlContainedGloListcontrol.Width = 400;
                    ogloGridListControl.Width = 400;
                }

                pnlContainedGloListcontrol.Visible = true;
                pnlContainedGloListcontrol.BringToFront();
                ogloGridListControl.Visible = true;
                ogloGridListControl.BringToFront();
                ogloGridListControl.Focus();

                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {

            }
            return _result;
        }
        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        
        {

            #region "Custom Event"
            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            C1FlexGrid _c1flexGrid = new C1FlexGrid();

            _c1flexGrid = c1StdFeeSchedule;

            #endregion

            try
            {

                int _rowIndex = 0;
                switch (COL_HCPCS)
                {
                    case 1:
                        if (ogloGridListControl.SelectedItems != null && ogloGridListControl.SelectedItems.Count > 0)
                        {
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            _c1flexGrid.SetData(_rowIndex, COL_HCPCS, ogloGridListControl.SelectedItems[0].Code.Trim());
                            _c1flexGrid.Focus();
                            _IsCptSelectedFromgloList = true; 
                        }
                        else
                        {
                            {
                                string _SearchText = "";
                                if (ogloGridListControl.ControlSearchText != null && ogloGridListControl.ControlSearchText.Trim() != "")
                                {
                                    _SearchText = ogloGridListControl.ControlSearchText.Trim();
                                }

                                if (ogloGridListControl.ControlType == gloGridListControlType.CPT)
                                {
                                    int _codeColIndex = ogloGridListControl.ParentColIndex;

                                    #region "Add CPT"
                                    frmSetupCPT ofrmCPT = new frmSetupCPT(_databaseconnectionstring);
                                    ofrmCPT.CPTCode = _SearchText;
                                    ofrmCPT.ShowDialog(this);
                                    if (ofrmCPT.CPTID > 0)
                                    {
                                        _c1flexGrid.SetData(_c1flexGrid.RowSel, COL_HCPCS, ofrmCPT.CPTCode);
                                        _IsCptSelectedFromgloList = true; 

                                    }
                                    else
                                        _c1flexGrid.SetData(_c1flexGrid.RowSel, COL_HCPCS, "");
                                    ofrmCPT.Dispose();
                                    #endregion

                                }


                            }

                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            _c1flexGrid.Focus();
                            _c1flexGrid.Select(_rowIndex, COL_HCPCS, true);

                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                CloseInternalControl();
            }
        }
        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changed on 2/4/2014
                for (int i = pnlContainedGloListcontrol.Controls.Count - 1; i >= 0; i--)
                {
                    pnlContainedGloListcontrol.Controls.RemoveAt(i);
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
                pnlContainedGloListcontrol.Visible = false;
                pnlInternalControl.Visible = false;
                pnlContainedGloListcontrol.SendToBack();
                if (!_IsCptSelectedFromgloList)
                {

                }
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            { }
            return _result;
        }       
        #endregion

        #region " Public & Private Methods "   
     
        private bool SaveFeeSchedule()
        { 
            if (c1StdFeeSchedule.Rows.Count <= 1)
            {
                MessageBox.Show("Add record to save.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            
            bool _result = false;         
            try
            {

                if (_StdFeeScheeduleID!=0&&!_IsCopyFeeSchedule)
                {
                   return SaveNewChargesForFeeSchedule();
                    
                }
                else
                {
                    if (_StdFeeScheeduleID == 0||_IsCopyFeeSchedule)
                    {
                        if (AddNewSchedule())
                        {
                            _IsCopyFeeSchedule = false;
                            return true;
                        }
                    }                  
                   
                }
                
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            return _result;
        }
      
        private bool AddNewSchedule()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);          
            bool _result = false;
            string sScheduleType;
            try
            {
                oDB.Connect(false);              
                
                c1StdFeeSchedule.FinishEditing();               
                
                sScheduleType = Convert.ToString(txt_StdFeeScheduleType.Text.Trim());
                sScheduleType = sScheduleType.Replace("'", "''");
                _StdFeeScheeduleID = 0;
                bool _isduplicateCPT = oClsFeeSchedule._IsDuplicate(txt_StdFeeScheduleType.Text, _StdFeeScheeduleID);
                if (_isduplicateCPT)
                {
                    //string FeeSchedulename = _dtDuplicateCPT.Rows[0]["sFeeScheduleName"].ToString();
                    //string FromDate = gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(_dtDuplicateCPT.Rows[0]["nFromDate"]));
                    //string ToDate = gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(_dtDuplicateCPT.Rows[0]["nToDate"]));
                    //MessageBox.Show("The effective date range overlaps with the following fee schedule:" + Environment.NewLine + FeeSchedulename + " [" + FromDate + "-" + ToDate + "]" + Environment.NewLine + "Please enter a different effective date range", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Fee Schedule already exist", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //_StdFeeScheeduleID = 0;
                    return false;
                }

                if (c1StdFeeSchedule.Rows.Count > 1)
                {
                    DataView dv = null;
                    DataTable dtTvpFeeScheduleCharges = null;
                    dv = (DataView)c1StdFeeSchedule.DataSource;
                    if (dv == null) return false;
                    dtTvpFeeScheduleCharges = dv.ToTable();
                    if (txtChargesPercentage.Text == "")
                    {
                        txtChargesPercentage.Text = "0";
                    }

                    dtTvpFeeScheduleCharges.Columns.Remove("sModDesc");
                    dtTvpFeeScheduleCharges.Columns.Remove("sSpecialtyID");
                    dtTvpFeeScheduleCharges.Columns.Remove("sSpecialityDesc");
                    dtTvpFeeScheduleCharges.Columns.Remove("nClinicCharges");
                    dtTvpFeeScheduleCharges.Columns.Remove("nLimitCharges");
                    dtTvpFeeScheduleCharges.Columns.Remove("nAllowedCharges");
                    dtTvpFeeScheduleCharges.Columns.Remove("nVariantAmount");                
                    dtTvpFeeScheduleCharges.AcceptChanges();
                    if (oClsFeeSchedule.SaveFeeSchedultDTL(dtTvpFeeScheduleCharges, Convert.ToDecimal(txtChargesPercentage.Text), ref _StdFeeScheeduleID))
                    {
                        MessageBox.Show("Fee schedule was saved successfully. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    }
                    else
                    {
                        return false;
                    }
                    if (c1StdFeeSchedule.Rows.Count > 1)
                    {
                      return   oClsFeeSchedule.InsertMasterDataAndAllocationForNewSchedule(mskStartDate.Text, mskEndDate.Text, _ClinicID, txt_StdFeeScheduleType.Text, _StdFeeScheeduleID);
                   }

                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _result;
        }

        private void AddLine()
        {
            try
            {
                if (c1StdFeeSchedule.DataSource == null && StdFeeScheeduleID == 0)
                {
                    GetStdFeeSchedule();
                }

                if (ogloGridListControl != null)
                    CloseInternalControl();  
                c1StdFeeSchedule.Sort(SortFlags.None, COL_HCPCS);     
                if (c1StdFeeSchedule.Rows.Count > 1)
                {
                    if (txtChargesPercentage.Text.Trim() != "")
                    c1StdFeeSchedule.SetData(c1StdFeeSchedule.RowSel, COL_CHARGES_PERCENTAGE, txtChargesPercentage.Text.Trim());
                   else
                       c1StdFeeSchedule.SetData(c1StdFeeSchedule.RowSel, COL_CHARGES_PERCENTAGE, "0");
                        
                }
                if (c1StdFeeSchedule != null)
                {
                    c1StdFeeSchedule.Rows.Add();
                    int rowIndex = c1StdFeeSchedule.Rows.Count - 1;
                    if (txtChargesPercentage.Text.Trim()!="")
                    c1StdFeeSchedule.SetData(rowIndex, COL_CHARGES_PERCENTAGE, txtChargesPercentage.Text.Trim());
                    else
                        c1StdFeeSchedule.SetData(rowIndex, COL_CHARGES_PERCENTAGE,"0");
                    c1StdFeeSchedule.SetData(rowIndex, COL_HCPCS, "");
                    c1StdFeeSchedule.SetData(rowIndex, COL_MODIFIER, "");

                    c1StdFeeSchedule.Cols[COL_HCPCS].AllowEditing = true;
                    c1StdFeeSchedule.Cols[COL_MODIFIER].AllowEditing = true;

                    SetCurrencyCellValue(rowIndex);
                    c1StdFeeSchedule.Select(rowIndex, COL_FEE_SCHEDULE_ID, true);
                    c1StdFeeSchedule.SetData(rowIndex, COL_FEE_SCHEDULE_ID, 0);
                    c1StdFeeSchedule.Focus();
                    c1StdFeeSchedule.Row = rowIndex;
                    c1StdFeeSchedule.Col = COL_HCPCS;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupStandardFeeSchedule, ActivityType.Add, "Add New Line", 0, rowIndex, 0, ActivityOutCome.Success);
                    ts_btnRemoveLine.Visible = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void SetCurrencyCellValue(int rowIndex)
        {
            try
            {
                if (c1StdFeeSchedule != null && c1StdFeeSchedule.Rows.Count > 0)
                {
                    if (rowIndex > 0 && rowIndex < c1StdFeeSchedule.Rows.Count)
                    {
                        c1StdFeeSchedule.SetData(rowIndex, COL_CLINIC_CHARGES, 0.00);
                        c1StdFeeSchedule.SetData(rowIndex, COL_LIMIT_CHARGES, 0.00);
                        c1StdFeeSchedule.SetData(rowIndex, COL_ALLOWED_CHARGES, 0.00);
                        c1StdFeeSchedule.SetData(rowIndex, COL_NON_FACILITY_FEESCHEDULE_AMOUNT, 0.00);
                        c1StdFeeSchedule.SetData(rowIndex, COL_FACILITY_FEESCHEDULE_AMOUNT, 0.00);
                        c1StdFeeSchedule.SetData(rowIndex, COL_VARIANT_AMOUNT, 0.00);
                        c1StdFeeSchedule.SetData(rowIndex, COL_NON_FACILITY_CHARGE_AMOUNT, 0.00);
                        c1StdFeeSchedule.SetData(rowIndex, COL_FACILITY_CHARGE_AMOUNT, 0.00);
                        c1StdFeeSchedule.SetData(rowIndex, COL_CHARGES_PERCENTAGE, 0.00);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool ValidateParameters()
        {
            bool _ReturnValue = true;
            try
            {
                if (txt_StdFeeScheduleType.Text.Trim() == "")
                {
                    MessageBox.Show("Enter name for fee schedule type.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ReturnValue = false;
                    //_IsParametersValidated = false;
                    txt_StdFeeScheduleType.Focus();
                    return _ReturnValue;
                }

                if (!mskStartDate.MaskCompleted)
                {
                    MessageBox.Show("Enter Effective Start Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ReturnValue = false;
                    //_IsParametersValidated = false;
                    mskStartDate.Focus();
                    return _ReturnValue;
                }
                if (!mskEndDate.MaskCompleted)
                {
                    MessageBox.Show("Enter Effective End Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _ReturnValue = false;
                    //_IsParametersValidated = false;
                    mskEndDate.Focus();
                    return _ReturnValue;
                }
              if ((txtChargesPercentage.Text.Length >1) && (!System.Text.RegularExpressions.Regex.IsMatch(txtChargesPercentage.Text, "^\\d*(?:\\.\\d{1,9})?$")))
               {
                        MessageBox.Show("Enter valid charges percentage.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _ReturnValue = false;
                        //_IsParametersValidated = false; 
                        return _ReturnValue;
               }
              
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _ReturnValue;
        }
        private bool ValidateFeeSchedule()
        {
            bool _ReturnValue = true;
            string _CptCode = String.Empty;
            string _Modifier = String.Empty;
            try
            {
                
                c1StdFeeSchedule.FinishEditing();
                if (c1StdFeeSchedule.Rows.Count > 1)
                {
                    if (Convert.ToDateTime((mskEndDate.Text)) < Convert.ToDateTime((mskStartDate.Text)) && Convert.ToDateTime(mskEndDate.Text) != Convert.ToDateTime(mskStartDate.Text))
                    {
                        MessageBox.Show("Start date should be less than End date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskEndDate.Focus();
                        return false;
                    }
                    //Bug : 00000837: Fee Schedule Save Performance issue
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CPT");
                    dt.Columns.Add("Modifier");
                    dt.PrimaryKey = new DataColumn[] { dt.Columns["CPT"], dt.Columns["Modifier"] };
                    String[] dtrow;
                    for (int i = 1; i < c1StdFeeSchedule.Rows.Count; i++)
                    {
                        if (c1StdFeeSchedule.GetData(i, COL_HCPCS).ToString() == "")
                        {
                            MessageBox.Show(" Enter CPT Code. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1StdFeeSchedule.Focus();
                            c1StdFeeSchedule.Select(i, COL_HCPCS);
                            return false; 
                        }
                        //Bug : 00000837: Fee Schedule Save Performance issue
                        _CptCode = Convert.ToString(c1StdFeeSchedule.GetData(i, COL_HCPCS)).Trim();
                        _Modifier = Convert.ToString(c1StdFeeSchedule.GetData(i, COL_MODIFIER)).Trim();
                        dtrow = new String[] { _CptCode, _Modifier };
                        if (!dt.Rows.Contains(dtrow))
                        {
                            dt.Rows.Add(_CptCode, _Modifier);
                        }
                        else
                        {
                            if (_Modifier != "")
                                MessageBox.Show("CPT, " + _CptCode + ", with Modifier, " + _Modifier + ", already exists for this fee schedule.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            else
                                MessageBox.Show("CPT, " + _CptCode + " already exists for this fee schedule.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            c1StdFeeSchedule.Focus();
                            c1StdFeeSchedule.Select(i, COL_MODIFIER);
                            return false;
                        }
                    }
                    if (dt != null)
                    {
                        dt.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            
            return _ReturnValue;
        }
               
        private bool ValidateForCPT(int rowIndex, int colIndex)
        {
            bool _isValid = true;
            try
            {
               
                string _CptCode = String.Empty;
                if (rowIndex > 0 && colIndex == COL_HCPCS)
                {
                    switch (COL_HCPCS)
                    {
                        case COL_HCPCS:

                            if (c1StdFeeSchedule.GetData(rowIndex, COL_HCPCS) != null)
                            {
                                _CptCode = Convert.ToString(c1StdFeeSchedule.GetData(rowIndex, COL_HCPCS));
                                if (_CptCode != "")
                                {
                                    if (oClsFeeSchedule.IsValidCPT(_CptCode.Trim()) == false)
                                    {
                                        MessageBox.Show(_CptCode + " CPT Code is not Valid.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        c1StdFeeSchedule.Select(rowIndex, COL_HCPCS);
                                        c1StdFeeSchedule.SetData(rowIndex, COL_HCPCS, "");
                                        _isValid = false;
                                    }

                                }
                                else
                                {
                                    if (!_bIsValidated)
                                    {
                                        MessageBox.Show(" Please Enter CPT Code. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    c1StdFeeSchedule.Select(rowIndex, COL_HCPCS);
                                    c1StdFeeSchedule.SetData(rowIndex, COL_HCPCS, "");
                                    _isValid = false;
                                }
                            }// IF Close
                            break;
                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
              
            }
            return _isValid;
        }

        private void DesignGrid()
        {
            try
            {
                //c1StdFeeSchedule.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                
                c1StdFeeSchedule.Cols.Count = COL_COUNT;
                if (c1StdFeeSchedule.Rows.Count<=1)
                  c1StdFeeSchedule.Rows.Count = 1;
                c1StdFeeSchedule.Rows.Fixed = 1;
                
                c1StdFeeSchedule.Dock = DockStyle.Fill;
        
                c1StdFeeSchedule.Cols[COL_FEE_SCHEDULE_ID].DataType = typeof(System.Int64);              
                c1StdFeeSchedule.Cols[COL_HCPCS].DataType = typeof(System.String);            
                c1StdFeeSchedule.Cols[COL_MODIFIER].DataType = typeof(System.String);
                c1StdFeeSchedule.Cols[COL_MODIFIER_DESC].DataType = typeof(System.String);
                c1StdFeeSchedule.Cols[COL_NON_FACILITY_FEESCHEDULE_AMOUNT].DataType = typeof(System.Decimal);
                c1StdFeeSchedule.Cols[COL_FACILITY_FEESCHEDULE_AMOUNT].DataType = typeof(System.Decimal);                
                c1StdFeeSchedule.Cols[COL_SPECIALTY_ID].DataType = typeof(System.String);
                c1StdFeeSchedule.Cols[COL_SPECIALITY_DESC].DataType = typeof(System.String);
                c1StdFeeSchedule.Cols[COL_CLINIC_CHARGES].DataType = typeof(System.Decimal);
                c1StdFeeSchedule.Cols[COL_LIMIT_CHARGES].DataType = typeof(System.Decimal);
                c1StdFeeSchedule.Cols[COL_ALLOWED_CHARGES].DataType = typeof(System.Decimal);
                c1StdFeeSchedule.Cols[COL_CHARGES_PERCENTAGE].DataType = typeof(System.Decimal);
                c1StdFeeSchedule.Cols[COL_VARIANT_AMOUNT].DataType = typeof(System.Decimal);
                c1StdFeeSchedule.Cols[COL_FACILITY_CHARGE_AMOUNT].DataType = typeof(System.Decimal);
                c1StdFeeSchedule.Cols[COL_NON_FACILITY_CHARGE_AMOUNT].DataType = typeof(System.Decimal);

                c1StdFeeSchedule.SetData(0, COL_FEE_SCHEDULE_ID, "Fee Schedule ID");              
                c1StdFeeSchedule.SetData(0, COL_HCPCS, "CPT Code");              
                c1StdFeeSchedule.SetData(0, COL_MODIFIER, "Modifier");
                c1StdFeeSchedule.SetData(0, COL_MODIFIER_DESC, "Modifier Desc");
                c1StdFeeSchedule.SetData(0, COL_NON_FACILITY_FEESCHEDULE_AMOUNT, "Non Facility Allowed");
                c1StdFeeSchedule.SetData(0, COL_FACILITY_FEESCHEDULE_AMOUNT, "Facility Allowed");
                c1StdFeeSchedule.SetData(0, COL_SPECIALTY_ID, "Specialty ID");
                c1StdFeeSchedule.SetData(0, COL_SPECIALITY_DESC, "Specialty");
                c1StdFeeSchedule.SetData(0, COL_CLINIC_CHARGES, "Clinic Charges");
                c1StdFeeSchedule.SetData(0, COL_LIMIT_CHARGES, "Limit Charges");
                c1StdFeeSchedule.SetData(0, COL_ALLOWED_CHARGES, "Allowed Charges");
              

                c1StdFeeSchedule.SetData(0, COL_CHARGES_PERCENTAGE, "Charges Percentage");
                c1StdFeeSchedule.SetData(0, COL_VARIANT_AMOUNT, "Variant Charges");

                c1StdFeeSchedule.SetData(0, COL_FACILITY_CHARGE_AMOUNT, "Facility Charges");
                c1StdFeeSchedule.SetData(0, COL_NON_FACILITY_CHARGE_AMOUNT, "Non Facility Charges");

                c1StdFeeSchedule.Cols[COL_FEE_SCHEDULE_ID].Width = 100;               
                c1StdFeeSchedule.Cols[COL_HCPCS].Width = 120;           
                c1StdFeeSchedule.Cols[COL_MODIFIER].Width = 120;
                c1StdFeeSchedule.Cols[COL_MODIFIER_DESC].Width = 0;
                c1StdFeeSchedule.Cols[COL_NON_FACILITY_FEESCHEDULE_AMOUNT].Width = 146;
                c1StdFeeSchedule.Cols[COL_FACILITY_FEESCHEDULE_AMOUNT].Width = 146;               
                c1StdFeeSchedule.Cols[COL_SPECIALTY_ID].Width = 100;
                c1StdFeeSchedule.Cols[COL_SPECIALITY_DESC].Width = 0;
                c1StdFeeSchedule.Cols[COL_CLINIC_CHARGES].Width = 100;
                c1StdFeeSchedule.Cols[COL_LIMIT_CHARGES].Width = 100;
                c1StdFeeSchedule.Cols[COL_ALLOWED_CHARGES].Width = 100;

                c1StdFeeSchedule.Cols[COL_CHARGES_PERCENTAGE].Width = 100;
                c1StdFeeSchedule.Cols[COL_VARIANT_AMOUNT].Width = 100;

                c1StdFeeSchedule.Cols[COL_FACILITY_CHARGE_AMOUNT].Width = 146;
                c1StdFeeSchedule.Cols[COL_NON_FACILITY_CHARGE_AMOUNT].Width = 146;

                c1StdFeeSchedule.Cols[COL_FEE_SCHEDULE_ID].Visible = false; 
              
                c1StdFeeSchedule.Cols[COL_MODIFIER_DESC].Visible = false;              
              
                c1StdFeeSchedule.Cols[COL_SPECIALTY_ID].Visible = false;
                c1StdFeeSchedule.Cols[COL_SPECIALITY_DESC].Visible = false;
                c1StdFeeSchedule.Cols[COL_CLINIC_CHARGES].Visible = false;
                c1StdFeeSchedule.Cols[COL_LIMIT_CHARGES].Visible = false;
                c1StdFeeSchedule.Cols[COL_ALLOWED_CHARGES].Visible = false;   
                // ------------------------------
                // Code added by Pankaj Bedse on 12012010 (Ref issue Id 0000293)
                // To Make Columns ReadOnly except NonFacility Amount & Facility amount)                
                c1StdFeeSchedule.Cols[COL_HCPCS].AllowEditing = false;
                c1StdFeeSchedule.Cols[COL_MODIFIER].AllowEditing = false;
                c1StdFeeSchedule.Cols[COL_NON_FACILITY_FEESCHEDULE_AMOUNT].AllowEditing = true;
                c1StdFeeSchedule.Cols[COL_FACILITY_FEESCHEDULE_AMOUNT].AllowEditing = true;
                c1StdFeeSchedule.Cols[COL_FACILITY_CHARGE_AMOUNT].AllowEditing = true;
                c1StdFeeSchedule.Cols[COL_NON_FACILITY_CHARGE_AMOUNT].AllowEditing = true;
                // ------------------------------

                c1StdFeeSchedule.Cols[COL_CHARGES_PERCENTAGE].Visible = false;
                c1StdFeeSchedule.Cols[COL_VARIANT_AMOUNT].Visible = false;

                C1.Win.C1FlexGrid.CellStyle csCurrency;// = c1StdFeeSchedule.Styles.Add("cs_Currency");
                try
                {
                    if (c1StdFeeSchedule.Styles.Contains("cs_Currency"))
                    {
                        csCurrency = c1StdFeeSchedule.Styles["cs_Currency"];
                    }
                    else
                    {
                        csCurrency = c1StdFeeSchedule.Styles.Add("cs_Currency");
                        csCurrency.DataType = typeof(System.Decimal);
                        csCurrency.Format = "c";
                        csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));                

                    }

                }
                catch
                {
                    csCurrency = c1StdFeeSchedule.Styles.Add("cs_Currency");
                    csCurrency.DataType = typeof(System.Decimal);
                    csCurrency.Format = "c";
                    csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));                

                }
   
  
                c1StdFeeSchedule.Cols[COL_CLINIC_CHARGES].Style = csCurrency;
                c1StdFeeSchedule.Cols[COL_LIMIT_CHARGES].Style = csCurrency;
                c1StdFeeSchedule.Cols[COL_ALLOWED_CHARGES].Style = csCurrency;
                c1StdFeeSchedule.Cols[COL_NON_FACILITY_FEESCHEDULE_AMOUNT].Style = csCurrency;
                c1StdFeeSchedule.Cols[COL_FACILITY_FEESCHEDULE_AMOUNT].Style = csCurrency;
                c1StdFeeSchedule.Cols[COL_VARIANT_AMOUNT].Style = csCurrency;
                c1StdFeeSchedule.Cols[COL_FACILITY_CHARGE_AMOUNT].Style = csCurrency;
                c1StdFeeSchedule.Cols[COL_NON_FACILITY_CHARGE_AMOUNT].Style = csCurrency;
            //   c1StdFeeSchedule.Cols[COL_MODIFIER ].s 
                //if (c1StdFeeSchedule != null && c1StdFeeSchedule.Rows.Count > 1)
                //{
                //    c1StdFeeSchedule.SetData(c1StdFeeSchedule.Rows.Count - 1, COL_HCPCS, "");
                //    c1StdFeeSchedule.SetData(c1StdFeeSchedule.Rows.Count - 1, COL_CLINIC_CHARGES, 0.00);
                //    c1StdFeeSchedule.SetData(c1StdFeeSchedule.Rows.Count - 1, COL_LIMIT_CHARGES, 0.00);
                //    c1StdFeeSchedule.SetData(c1StdFeeSchedule.Rows.Count - 1, COL_ALLOWED_CHARGES, 0.00);
                //    c1StdFeeSchedule.SetData(c1StdFeeSchedule.Rows.Count - 1, COL_NON_FACILITY_FEESCHEDULE_AMOUNT, 0.00);
                //    c1StdFeeSchedule.SetData(c1StdFeeSchedule.Rows.Count - 1, COL_FACILITY_FEESCHEDULE_AMOUNT, 0.00);
                //    c1StdFeeSchedule.SetData(c1StdFeeSchedule.Rows.Count - 1, COL_VARIANT_AMOUNT, 0.00);
                //    c1StdFeeSchedule.SetData(c1StdFeeSchedule.Rows.Count - 1, COL_FACILITY_CHARGE_AMOUNT, 0.00);
                //    c1StdFeeSchedule.SetData(c1StdFeeSchedule.Rows.Count - 1, COL_NON_FACILITY_CHARGE_AMOUNT, 0.00);

                //}
                ////FillSpeciality();
                c1StdFeeSchedule.Row = 0;
            }
            catch (Exception ex)
            {
              gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            
            }
        }
     
        private void GetStdFeeSchedule()
        {
         
            DataTable dtTemp = new DataTable();
            try
            {
               
                Int32 Index = c1StdFeeSchedule.Rows.Count - 1;

                dtTemp = null;
                if (_StdFeeScheeduleID != 0)
                {
                    dtTemp = oClsFeeSchedule.GetDates(_StdFeeScheeduleID); 
                }
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    mskStartDate.Text = gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64( dtTemp.Rows[0]["StartDate"]));
                    mskEndDate.Text = gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(dtTemp.Rows[0]["EndDate"]));

                }
                dtTemp = null;
                dtTemp = oClsFeeSchedule.GetCharges(_StdFeeScheeduleID,_IsCopyFeeSchedule);
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    txtChargesPercentage.Text = Convert.ToString(dtTemp.Rows[0]["nChargePercentage"]);
                 //   c1StdFeeSchedule.Clear();
                    c1StdFeeSchedule.DataSource = null;
                    c1StdFeeSchedule.DataSource = dtTemp.DefaultView;
                    DesignGrid();
                    if (c1StdFeeSchedule.Rows.Count > 1)
                    {
                        c1StdFeeSchedule.Select(1, COL_HCPCS); 
                    }
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupStandardFeeSchedule, ActivityType.View, "View Scheduled Fee", 0, _StdFeeScheeduleID, 0, ActivityOutCome.Success);
                }
                //c1StdFeeSchedule.Clear();
                c1StdFeeSchedule.DataSource = null;
                c1StdFeeSchedule.DataSource = dtTemp.DefaultView;
                DesignGrid();
                if (c1StdFeeSchedule.Rows.Count > 1)
                {
                    c1StdFeeSchedule.Select(1, COL_HCPCS);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        public DataTable GetAllStdFeeSchedules()
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = new DataTable();
            string sqlgetQuery = "SELECT ISNULL(BL_FeeSchedule_MST.nFeeScheduleID,0) AS nFeeScheduleID, ISNULL(nFeeScheduleType,0) As nFeeScheduleType,  ISNULL(sFeeScheduleName,'') AS sFeeScheduleName ,dbo.CONVERT_TO_DATE(BL_FeeSchedule_Allocation.nFromDate)  as StartDate ,dbo.CONVERT_TO_DATE(BL_FeeSchedule_Allocation.nToDate) as EndDate FROM BL_FeeSchedule_MST Left OUTER JOIN BL_FeeSchedule_Allocation ON BL_FeeSchedule_Allocation.nFeeScheduleID=BL_FeeSchedule_MST.nFeeScheduleID ORDER BY sFeeScheduleName";

            try
            {
                ODB.Connect(false);
                ODB.Retrive_Query(sqlgetQuery, out dt);
                return dt;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                ODB.Dispose();
                dt.Dispose();
            }
            return null;

        }

        private bool SaveNewChargesForFeeSchedule()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();          
            DataTable dtCPT = new DataTable();
            Int64 _nFeeSheduledID = 0;            
            try
            {
                oDB.Connect(false);
                if (_RemovedCharged.Rows.Count > 0)
                {
                   
                    Boolean _isRemoved = oClsFeeSchedule.RemoveCharge(_RemovedCharged);  
                }
                bool _isduplicateCPT = oClsFeeSchedule._IsDuplicate(txt_StdFeeScheduleType.Text, _StdFeeScheeduleID);
                if (_isduplicateCPT)
                {
                    //string FeeSchedulename = _dtDuplicateCPT.Rows[0]["sFeeScheduleName"].ToString();
                   // string FromDate = gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(_dtDuplicateCPT.Rows[0]["nFromDate"]));
                    //string ToDate = gloDateMaster.gloDate.DateAsDateString(Convert.ToInt64(_dtDuplicateCPT.Rows[0]["nToDate"]));
                   // MessageBox.Show("The effective date range overlaps with the following fee schedule:" + Environment.NewLine + FeeSchedulename + " [" + FromDate + "-" + ToDate + "]" + Environment.NewLine + "Please enter a different effective date range", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Fee Schedule already exist", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //_StdFeeScheeduleID = 0;
                    return false;
                }
               if(c1StdFeeSchedule.Rows.Count>1)
                {
                    if (txtChargesPercentage.Text == "")
                    {
                        txtChargesPercentage.Text = "0";
                    } 
                   DataView dv = null;
                    DataTable dtTvpFeeScheduleCharges = null;
                    dv = (DataView)c1StdFeeSchedule.DataSource;
                    if (dv == null) return false;
                    dtTvpFeeScheduleCharges = dv.ToTable();
                    dtTvpFeeScheduleCharges.Columns.Remove("sModDesc");
                    dtTvpFeeScheduleCharges.Columns.Remove("sSpecialtyID");
                    dtTvpFeeScheduleCharges.Columns.Remove("sSpecialityDesc");
                    dtTvpFeeScheduleCharges.Columns.Remove("nClinicCharges");
                    dtTvpFeeScheduleCharges.Columns.Remove("nLimitCharges");
                    dtTvpFeeScheduleCharges.Columns.Remove("nAllowedCharges");
                    dtTvpFeeScheduleCharges.Columns.Remove("nVariantAmount");                
                    dtTvpFeeScheduleCharges.AcceptChanges();
                    if (oClsFeeSchedule.SaveFeeSchedultDTL(dtTvpFeeScheduleCharges,Convert.ToDecimal(txtChargesPercentage.Text),   ref _StdFeeScheeduleID))
                    {
                        _nFeeSheduledID = _StdFeeScheeduleID;
                        MessageBox.Show("Fee schedule was saved successfully. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);  

                    }
                    else
                    {
                        return false;
                    }
                   
                   
                }
                if (_nFeeSheduledID != 0)
                {
                    Boolean _isUpdate = oClsFeeSchedule.UpdateFeeAllocationAndMaster(mskStartDate.Text, mskEndDate.Text, _ClinicID, txt_StdFeeScheduleType.Text, _nFeeSheduledID);
                    return _isUpdate;
                }
                

                 oDB.Disconnect();
                 oDB.Dispose();
                 oDBParameters.Dispose(); 
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                oDB.Disconnect();
                oDB.Dispose();  
                return false;
            }
        }
      
        #endregion

        #region "Form Control Events"

        #region " C1 Flexgrid Event"

        public void c1StdFeeSchedule_KeyPressEditEvent(object sender,C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            String prd = c1StdFeeSchedule.Editor.Text;
            Int64 nFeeScheduleID = 0;

            switch (e.Col)
            {
                
                case COL_HCPCS:
                   nFeeScheduleID = 0;
                    nFeeScheduleID = Convert.ToInt64(c1StdFeeSchedule.GetData(e.Row, COL_FEE_SCHEDULE_ID ));
                    if (nFeeScheduleID > 0)
                        e.Handled = true;  
                    break; 
                case COL_MODIFIER :
                     nFeeScheduleID = 0;
                     nFeeScheduleID = Convert.ToInt64(c1StdFeeSchedule.GetData(e.Row, COL_FEE_SCHEDULE_ID));
                    if (nFeeScheduleID > 0)
                        e.Handled = true;                     
                    if (prd.Length > 9)
                        e.Handled = true;
                      break; 
                case COL_FACILITY_CHARGE_AMOUNT :
                    if (e.KeyChar == Convert.ToChar("-"))
                    {
                        e.Handled = true;
                    }
                      if (prd.Length > 10)
                        e.Handled = true;
                    break;
                   
                case COL_NON_FACILITY_CHARGE_AMOUNT :
                    if (e.KeyChar == Convert.ToChar("-"))
                    {
                        e.Handled = true;
                    }
                      if (prd.Length > 10)
                        e.Handled = true;
                    break;
                  
                case COL_NON_FACILITY_FEESCHEDULE_AMOUNT:
                    if (e.KeyChar == Convert.ToChar("-"))
                    {
                        e.Handled = true;
                    }
                    if (prd.Length > 10)
                        e.Handled = true;
                    break;
                case COL_FACILITY_FEESCHEDULE_AMOUNT:
                    if (e.KeyChar == Convert.ToChar("-"))
                    {
                        e.Handled = true;
                    }
                    if (prd.Length > 10)
                        e.Handled = true;
                    break;
               
            }
        }

       
        #endregion
       
        #region "Button Control Events"

        private void ts_btnAddLine_Click(object sender, EventArgs e)
        {
            // ValidateFeeSchedule() Function is added by Pankaj Bedse 30122009
            // Line was got added without submitting mandatory fields like Year, Carrier No etc.
            TopToolStrip.Select();
            if (!_IsValidate)
                return;
            if (c1StdFeeSchedule.Rows.Count > 1)
            {
                c1StdFeeSchedule.Select(c1StdFeeSchedule.RowSel, 1);
                c1StdFeeSchedule.Select(c1StdFeeSchedule.RowSel, 4);
                           
            }
                 AddLine();
                    
        }

        private void ts_btnRemoveLine_Click(object sender, EventArgs e)
        {
            if (ogloGridListControl != null)
                CloseInternalControl();
            TopToolStrip.Select();
            if (!_IsValidate)
                return;
            if (c1StdFeeSchedule != null && c1StdFeeSchedule.Rows.Count > 1)
            {
                int rowIndex = c1StdFeeSchedule.RowSel;
                if (c1StdFeeSchedule.GetData(rowIndex, COL_HCPCS) != (object)null && c1StdFeeSchedule.GetData(rowIndex, COL_FEE_SCHEDULE_ID) != (object)null )
                    _RemovedCharged.Rows.Add(c1StdFeeSchedule.GetData(rowIndex, COL_HCPCS).ToString(), c1StdFeeSchedule.GetData(rowIndex, COL_FEE_SCHEDULE_ID).ToString(), c1StdFeeSchedule.GetData(rowIndex, COL_MODIFIER).ToString());
                c1StdFeeSchedule.Rows.Remove(rowIndex);                             
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupStandardFeeSchedule, ActivityType.Remove, "Remove Line", 0, rowIndex, 0, ActivityOutCome.Success);
            }
            if (c1StdFeeSchedule.Rows.Count <= 1)
                ts_btnRemoveLine.Visible = false;
            
        }

        private void ts_btnSave_FeeSchedule_Click_1(object sender, EventArgs e)
        {
            if (ogloGridListControl != null)
                CloseInternalControl();
            //_IsParametersValidated = true;
            TopToolStrip.Select();
            if (!_IsValidate)
                return;
            if (ValidateParameters() == true && ValidateFeeSchedule() == true)
            {

                if (SaveFeeSchedule() == true)
                {
                    //_IsParametersValidated = true;
                    _IsValidate = true;


                    if (isOpenForEdit == false)
                    {
                        if (Convert.ToInt64(cmbInsuranceCompany.SelectedValue) != 0)
                        {
                            SaveFeeScheduleInsCompanyLink();
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupStandardFeeSchedule, gloAuditTrail.ActivityType.Add, "Fee Schdule " + txt_StdFeeScheduleType.Text + " is generated using expected collection report for isurance plan  " + cmbInsuranceCompany.Text + ".", StdFeeScheeduleID, Convert.ToInt64(cmbInsuranceCompany.SelectedValue), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                        }

                        DialogResult dResult = MessageBox.Show("Do you want to associate fee schedule to insurance plan?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dResult.ToString() == "Yes")
                        {
                            long SettingID = GetClinicBillingSetting();
                            gloContacts.frmAssignFeeScheduled ofrmAssignFeeScheduled = new gloContacts.frmAssignFeeScheduled(StdFeeScheeduleID, Convert.ToInt64(cmbInsuranceCompany.SelectedValue), SettingID);
                            ofrmAssignFeeScheduled.ShowDialog(this);
                        }
                    }

                    if (txtFromCPT.Text != "" && txtToCPT.Text != "")
                    {
                        tsbShow_Click(sender, e);
                    }
                    else
                    {
                        GetStdFeeSchedule();
                        DesignGrid();
                        if (c1StdFeeSchedule.Rows.Count > 1)
                        {
                            c1StdFeeSchedule.Select(1, COL_HCPCS);
                        }
                    }
                }
            }
        }

        private void ts_btnClose_FeeSchedule_Click(object sender, EventArgs e)
        {
            _StdFeeScheeduleID = 0;
            this.Dispose();
        }

        private void tsbShow_Click(object sender, EventArgs e)
        {
            if (ogloGridListControl != null)
                CloseInternalControl();
            DataTable dtCPT = new DataTable();
            DataTable dtTemp = new DataTable();
            try
            {
                if (txtFromCPT.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter start range of CPT.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtToCPT.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter end range of CPT.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //DesignGrid();
                dtCPT = oClsFeeSchedule.GetShowCPTList(txtFromCPT.Text.Trim(), txtToCPT.Text.Trim(), _StdFeeScheeduleID,_IsCopyFeeSchedule);

               // c1StdFeeSchedule.Rows.Count = 1;
                if (dtCPT != null && dtCPT.Rows.Count > 0)
                {
                   // c1StdFeeSchedule.Clear();
                    c1StdFeeSchedule.DataSource = null;
                    c1StdFeeSchedule.DataSource = dtCPT.DefaultView;
                    DesignGrid();
                    c1StdFeeSchedule.Select(1, COL_HCPCS); 

                }
                else
                {
                    c1StdFeeSchedule.DataSource = dtCPT.DefaultView ;
                    DesignGrid();
                }
                ts_btnShowAll.Visible = true;
                tsbShow.Visible = false;
                if (c1StdFeeSchedule.Rows.Count <= 1)
                    ts_btnRemoveLine.Visible = false;
                else
                    ts_btnRemoveLine.Visible = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void txtChargesPercentage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) && e.KeyChar <= Convert.ToChar(57)) || e.KeyChar == Convert.ToChar(8) || e.KeyChar == Convert.ToChar(46)))
            {
                e.Handled = true;
            }
        }

        private void tsb_Apply_Click(object sender, EventArgs e)
        {
            if (ogloGridListControl != null)
                CloseInternalControl();
            TopToolStrip.Select();
            if (!_IsValidate)
                return;

            if ((txtChargesPercentage.Text.Length > 1) && (!System.Text.RegularExpressions.Regex.IsMatch(txtChargesPercentage.Text, "^\\d*(?:\\.\\d{1,9})?$")))
            {
                MessageBox.Show("Enter valid charges percentage.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);              
                return ;
            }
            try
            {
                if (txtChargesPercentage.Text == "" || Convert.ToDecimal(txtChargesPercentage.Text) <= 0)
                {
                    MessageBox.Show("Enter charges percentage. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception )
            {

                MessageBox.Show("Enter valid charges percentage.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ValidateParameters() == true)
            {
                try
                {
                    c1StdFeeSchedule.FinishEditing();

                    //Bug : 00000837: Fee Schedule Save Performance issue
                    if (txtChargesPercentage.Text == "")
                    {
                        txtChargesPercentage.Text = "0";
                    }
                    double per = Convert.ToDouble(txtChargesPercentage.Text) / 100;
                    double FACILITY_CHARGE_AMOUNT, NON_FACILITY_CHARGE_AMOUNT;
                    int COL_FACILITY = 0, COL_NON_FACILITY = 0;
                    if (RdbIncreaseChrgeby.Checked)
                    {
                        COL_FACILITY = COL_FACILITY_CHARGE_AMOUNT;
                        COL_NON_FACILITY = COL_NON_FACILITY_CHARGE_AMOUNT;
                    }
                    if (RdbSetCharges.Checked)
                    {
                        COL_FACILITY = COL_FACILITY_FEESCHEDULE_AMOUNT;
                        COL_NON_FACILITY = COL_NON_FACILITY_FEESCHEDULE_AMOUNT;
                    }


                    DataTable dtTemp = ((DataView)c1StdFeeSchedule.DataSource).ToTable();
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {

                        FACILITY_CHARGE_AMOUNT = Convert.ToDouble(dtTemp.Rows[i][COL_FACILITY].ToString());
                        NON_FACILITY_CHARGE_AMOUNT = Convert.ToDouble(dtTemp.Rows[i][COL_NON_FACILITY].ToString());


                        FACILITY_CHARGE_AMOUNT += (FACILITY_CHARGE_AMOUNT * per);
                        NON_FACILITY_CHARGE_AMOUNT += (NON_FACILITY_CHARGE_AMOUNT * per);

                        if (rdbNone.Checked != true)
                        {
                            if (rdbroundUp.Checked == true)
                            {
                                FACILITY_CHARGE_AMOUNT = Math.Ceiling(FACILITY_CHARGE_AMOUNT);
                                NON_FACILITY_CHARGE_AMOUNT = Math.Ceiling(NON_FACILITY_CHARGE_AMOUNT);
                            }
                            else
                            {
                                FACILITY_CHARGE_AMOUNT = Math.Floor(FACILITY_CHARGE_AMOUNT);
                                NON_FACILITY_CHARGE_AMOUNT = Math.Floor(NON_FACILITY_CHARGE_AMOUNT);
                            }
                        }
                        dtTemp.Rows[i][COL_FACILITY_CHARGE_AMOUNT] = FACILITY_CHARGE_AMOUNT;
                        dtTemp.Rows[i][COL_NON_FACILITY_CHARGE_AMOUNT] = NON_FACILITY_CHARGE_AMOUNT;
                    }
                    //c1StdFeeSchedule.Clear();
                    c1StdFeeSchedule.DataSource = null;
                    c1StdFeeSchedule.DataSource = dtTemp.DefaultView;
                    DesignGrid();
                    if (c1StdFeeSchedule.Rows.Count > 1)
                    {
                        c1StdFeeSchedule.Select(1, COL_HCPCS);
                    }
                }
                catch (Exception ex)
                {

                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); ;
                }
            }
        }

        private void rdbroundUp_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbroundUp.Checked == true)
                rdbroundUp.Font =gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            else
                rdbroundUp.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void rdbroundDown_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbroundDown.Checked == true)
                rdbroundDown.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rdbroundDown.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void rdbNone_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNone.Checked == true)
                rdbNone.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rdbNone.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void ts_btnSave_Click(object sender, EventArgs e)
        {
            if (ogloGridListControl != null)
                CloseInternalControl();
          //  _IsParametersValidated = true;
            TopToolStrip.Select();
            if (!_IsValidate)
                return;
            if (ValidateParameters()==true  && ValidateFeeSchedule() == true)
            {

                if (SaveFeeSchedule() == true)
                {
                  //  _IsParametersValidated = true;
                    _IsValidate = true;

                    if (isOpenForEdit == false)
                    {
                        if (Convert.ToInt64(cmbInsuranceCompany.SelectedValue) != 0)
                        {
                            SaveFeeScheduleInsCompanyLink();
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.SetupStandardFeeSchedule, gloAuditTrail.ActivityType.Add, "Fee Schdule " + txt_StdFeeScheduleType.Text + " is generated using expected collection report for isurance plan  " + cmbInsuranceCompany.Text + ".", StdFeeScheeduleID, Convert.ToInt64(cmbInsuranceCompany.SelectedValue), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                        }

                        DialogResult dResult = MessageBox.Show("Do you want to associate fee schedule to insurance plan?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dResult.ToString() == "Yes")
                        {
                            long SettingID = GetClinicBillingSetting();
                            gloContacts.frmAssignFeeScheduled ofrmAssignFeeScheduled = new gloContacts.frmAssignFeeScheduled(StdFeeScheeduleID, Convert.ToInt64(cmbInsuranceCompany.SelectedValue), SettingID);
                            ofrmAssignFeeScheduled.ShowDialog(this);
                        }
                    }

                    if (txtFromCPT.Text != "" && txtToCPT.Text != "")
                    {
                        tsbShow_Click(sender, e);
                    }
                    else
                    {
                        GetStdFeeSchedule();
                        DesignGrid();
                        if (c1StdFeeSchedule.Rows.Count > 1)
                        {
                            c1StdFeeSchedule.Select(1, COL_HCPCS);   
                        }
                    }
                }
            }
        }

        private void ts_btnShowAll_Click(object sender, EventArgs e)
        {
            if (ogloGridListControl != null)
                CloseInternalControl();
            TopToolStrip.Select();
            if (!_IsValidate)
                return;
            DesignGrid();
            GetStdFeeSchedule();
            ts_btnShowAll.Visible = false;
            tsbShow.Visible = true;
            txtFromCPT.Text = "";
            txtToCPT.Text = "";
            txt_StdFeeScheduleType.Focus();

        }

        private void RdbIncreaseChrgeby_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbIncreaseChrgeby.Checked == true)
                RdbIncreaseChrgeby.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                RdbIncreaseChrgeby.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void RdbSetCharges_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbSetCharges.Checked == true)
                RdbSetCharges.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                RdbSetCharges.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void rdbNone_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rdbNone.Checked == true)
                rdbNone.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rdbNone.Font = gloGlobal.clsgloFont.gFont; //new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void txtChargesPercentage_TextChanged(object sender, EventArgs e)
        {



        }

        #endregion

        #endregion

        #region " Menu events for shortcut keys"

        private void mnuFeeSchedule_AddLine_Click(object sender, EventArgs e)
        {
            TopToolStrip.Select();
            if (!_IsValidate)
                return;
            ts_btnAddLine_Click(null,null);
            
        }

        private void mnuFeeSchedule_RemoveLine_Click(object sender, EventArgs e)
        {
            if (!_IsValidate)
                return;
            TopToolStrip.Select();
            ts_btnRemoveLine_Click(null, null);

        }

        private void mnuFeeSchedule_Save_Click(object sender, EventArgs e)
        {
            ts_btnSave_FeeSchedule_Click_1(null, null);

        }

        private void mnuFeeSchedule_Close_Click(object sender, EventArgs e)
        {
            ts_btnClose_FeeSchedule_Click(null, null);

        }
        #endregion
              
        #region " c1StdFeeSchedule Events "       
        
        private void c1StdFeeSchedule_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col==COL_HCPCS)
            _IsCptSelectedFromgloList = false; 
            C1FlexGrid _c1flexGrid = new C1FlexGrid();
            C1FlexGrid GridName = (C1FlexGrid)sender;
            Int64   nFeeScheduleID = 0;
            nFeeScheduleID = Convert.ToInt64(c1StdFeeSchedule.GetData(e.Row, COL_FEE_SCHEDULE_ID));
            if (nFeeScheduleID > 0)
                return;
           
            try
            {

                _c1flexGrid = c1StdFeeSchedule;

                //if (e.Row > 0)
                //{
                //    CellNote _cellNote = null;
                //    CellRange _cellRange = _c1flexGrid.GetCellRange(e.Row, e.Col);
                //    _cellRange.UserData = _cellNote;
                //}

                switch (e.Col)
                {

                    case 1:
                        {

                            OpenInternalControl(gloGridListControlType.CPT, "CPT", false, e.Row, e.Col, "");
                            string _SearchText = "";
                            if (_c1flexGrid != null && _c1flexGrid.Rows.Count > 0)
                            {
                                _SearchText = Convert.ToString(c1StdFeeSchedule.GetData(c1StdFeeSchedule.RowSel, COL_HCPCS));
                                if (_SearchText != "" && ogloGridListControl != null)
                                {
                                    ogloGridListControl.FillControl(_SearchText);
                                }

                            }

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        private void c1StdFeeSchedule_ChangeEdit(object sender, EventArgs e)
        {
            string _strSearchString = "";
            try
            {

                _strSearchString = c1StdFeeSchedule.Editor.Text;

                if (ogloGridListControl != null)
                {
                    //ogloGridListControl.Search(_strSearchString, SearchColumn.Code);
                    //ogloGridListControl.InStringSearch(_strSearchString);

                    if (c1StdFeeSchedule.Col == COL_HCPCS)
                    {
                        //ogloGridListControl.FillControl(_strSearchString);

                        string _cptCode = "";
                      //  string _facilityCode = "";

                        if (c1StdFeeSchedule != null && c1StdFeeSchedule.Rows.Count > 0)
                        {
                            _cptCode = Convert.ToString(c1StdFeeSchedule.GetData(c1StdFeeSchedule.Row, COL_HCPCS));
                            ogloGridListControl.SelectedCPTCode = _cptCode;

                        }


                        ogloGridListControl.FillControl(_strSearchString);
                    }
                    else
                    {
                        ogloGridListControl.AdvanceSearch(_strSearchString);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {

            }
        }

        private void c1StdFeeSchedule_KeyUp(object sender, KeyEventArgs e)
        {
            C1FlexGrid _c1flexGrid = new C1FlexGrid();
            C1FlexGrid aaa = (C1FlexGrid)sender;
            string c1name = aaa.Name;

            try
            {

                _c1flexGrid = c1StdFeeSchedule;
            //    string _code = "";
            //    string _description = "";
            //    bool _isdeleted = true;
            //    int COL_CODE = 1;
             //   int COL_Description = 2;
                TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
                //RowColEventArgs e1 = null;
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    #region "Enter Key"

                    if (pnlContainedGloListcontrol.Visible)
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
                    if (pnlContainedGloListcontrol.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            ogloGridListControl.Focus();
                        }
                    }
                    #endregion
                }
                else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Tab)
                {
                    e.SuppressKeyPress = true;
                    #region "Escape Key"
                    if (pnlContainedGloListcontrol.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            CloseInternalControl();

                            if (_c1flexGrid.RowSel > 0)
                            {

                            }
                        }
                    }
                    #endregion
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }

        #endregion

        #region "Validate Date"
        private bool IsValidDate(object strDate)
        {
            bool Success;
            try
            {
                DateTime validatedDate;
                Success = DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);
                if (validatedDate != null && Success == true)
                {
                    if (validatedDate < DateTime.MaxValue && validatedDate >= Convert.ToDateTime("01/01/1900"))
                    {
                        Success = true;
                    }
                    else
                    {
                        Success = false;
                    }

                }
            }
            catch (FormatException)
            {
                Success = false;

            }
            return Success;
        }
        private void mskStartDate_Validating(object sender, CancelEventArgs e)
        {
            
            try
            {
                MaskedTextBox mskStartDate = (MaskedTextBox)sender;
                mskStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskStartDate.Text;
                mskStartDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                _IsValidate = true;
                if (mskStartDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskStartDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //Specifies that the Date is InValid
                         
                            e.Cancel = true;
                            _IsValidate=false ; 
                        }
                       
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);           
                e.Cancel = true;
                _IsValidate = false;
            }
        }

        private void mskStartDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void mskEndDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void mskEndDate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                MaskedTextBox mskEndDate = (MaskedTextBox)sender;
                mskEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskEndDate.Text;
                mskEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                _IsValidate = true;
                if (mskEndDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskEndDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //Specifies that the Date is InValid

                            e.Cancel = true;
                            _IsValidate = false;
                        }

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _IsValidate = false;
            }
        }
#endregion

        private void c1StdFeeSchedule_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                HitTestInfo hitInfo = c1StdFeeSchedule.HitTest(e.X, e.Y);
                c1StdFeeSchedule.RowSel = hitInfo.Row;
                if (hitInfo.Column != 0)
                {
                    c1StdFeeSchedule.ColSel = hitInfo.Column;
                    c1StdFeeSchedule.Select(hitInfo.Row, hitInfo.Column);
                }

                if (hitInfo.Row > 0)
                {
                    pnlInternalControl.Visible = false;
                    pnlInternalControl.SendToBack();
                    IsInternalControlActive = false;
                   
                }
              
                if (c1StdFeeSchedule.RowSel <= 0)
                {
                    if (c1StdFeeSchedule != null && c1StdFeeSchedule.Rows.Count > 1)
                    {
                        //c1Transaction.Focus();
                        c1StdFeeSchedule.Select(c1StdFeeSchedule.Rows.Count - 1, hitInfo.Column, true);
                    }
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void c1StdFeeSchedule_BeforeSelChange(object sender, RangeEventArgs e)
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
                if ( (e.OldRange.r1 >= 0) && (e.OldRange.r1 < c1StdFeeSchedule.Rows.Count) )
                {
                    if (c1StdFeeSchedule.Cols.Count > COL_HCPCS)
                    {
                        if (c1StdFeeSchedule.GetData(e.OldRange.r1, COL_HCPCS).ToString() != "")
                        {
                            if (e.OldRange.r1 > 0)
                            {
                                if (c1StdFeeSchedule.Cols.Count > COL_FEE_SCHEDULE_ID)
                                {
                                    if (c1StdFeeSchedule.GetData(e.OldRange.r1, COL_FEE_SCHEDULE_ID).ToString() == "0" && !oClsFeeSchedule.IsValidCPT(c1StdFeeSchedule.GetData(e.OldRange.r1, COL_HCPCS).ToString()))
                                    {
                                        c1StdFeeSchedule.SetData(e.OldRange.r1, COL_HCPCS, "");
                                    }
                                }
                            }
                        }
                    }
                }
                iPreCol = e.OldRange.c1;
                iPreRow = e.OldRange.r1;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        #region "Fee Schedule Using Exepted Collection"
        
       

        private void btnGetInsuranceCompany_Click(object sender, EventArgs e)
        {
            
            oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.InsuranceCompany,false, this.Width);
            oListControl.ControlHeader = "Insurance Company";
            oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_InsCompanySelectedClick);
            oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
            oListControl.Dock = DockStyle.Fill;
            this.Controls.Add(oListControl);
            for (int i = 0; i < cmbInsuranceCompany.Items.Count; i++)
            {
                cmbInsuranceCompany.SelectedIndex = i;
                if (cmbInsuranceCompany.Text == "")
                { }
                else
                {
                    oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsuranceCompany.SelectedValue), cmbInsuranceCompany.Text);
                }
            }
            if (cmbInsuranceCompany.Items.Count > 0)
                cmbInsuranceCompany.SelectedIndex = 0;

            oListControl.OpenControl();

            if (oListControl.IsDisposed == false)
            {
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
        }

        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            removeOListControl();
        }
       
        private void oListControl_InsCompanySelectedClick(object sender, EventArgs e)
        {

            string CategoryID;
            CategoryID = "";
            try
            {
                cmbInsuranceCompany.DataSource = null;
                cmbInsuranceCompany.Items.Clear();
                if (dtInsuranceCompany == null)
                {
                    dtInsuranceCompany = new DataTable();
                    dtInsuranceCompany.Columns.Clear();
                }
                dtInsuranceCompany.Rows.Clear();

                if (dtInsuranceCompany.Columns.Contains("nInsCompanyID") == false)
                {
                    DataColumn dcId = new DataColumn("nInsCompanyID");
                    dtInsuranceCompany.Columns.Add(dcId);
                }
                if (dtInsuranceCompany.Columns.Contains("sInsCompany") == false)
                {
                    DataColumn dcDescription = new DataColumn("sInsCompany");
                    dtInsuranceCompany.Columns.Add(dcDescription);
                }
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        DataRow drTemp = dtInsuranceCompany.NewRow();
                        drTemp["nInsCompanyID"] = oListControl.SelectedItems[i].ID;
                        drTemp["sInsCompany"] = oListControl.SelectedItems[i].Description;
                        dtInsuranceCompany.Rows.Add(drTemp);

                        if (CategoryID == "")
                        {
                            CategoryID = Convert.ToString(oListControl.SelectedItems[i].ID);
                        }
                        else
                        {
                            CategoryID = CategoryID + "," + Convert.ToString(oListControl.SelectedItems[i].ID);
                        }
                    }

                    cmbInsuranceCompany.ValueMember = dtInsuranceCompany.Columns["nInsCompanyID"].ColumnName;
                    cmbInsuranceCompany.DisplayMember = dtInsuranceCompany.Columns["sInsCompany"].ColumnName;
                    cmbInsuranceCompany.DataSource = dtInsuranceCompany;
                }


            }
            catch (Exception ex) // ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
              
            }
            cmbInsuranceCompany.Focus();
        }

        private void removeOListControl()
        {
            if (oListControl != null)
            {
                if (this.Controls.Contains(oListControl))
                {
                    this.Controls.Remove(oListControl);
                }
                try
                {
                    try
                    {
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_InsCompanySelectedClick);
                    }
                    catch(Exception Ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                    }
                    try
                    {
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch (Exception Ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                    }

                }
                catch
                {

                }
                oListControl.Dispose();
                oListControl = null;
            }
        }

        private void btnClearInsuranceCompany_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCmbTemp = (DataTable)cmbInsuranceCompany.DataSource;
                if (dtCmbTemp != null)
                {
                    foreach (DataRow dr in dtCmbTemp.Rows)
                    {
                        if (Convert.ToInt64(dr[0]) == Convert.ToInt64(cmbInsuranceCompany.SelectedValue))
                        {
                            dtCmbTemp.Rows.Remove(dr);
                            break;
                        }
                    }
                    cmbInsuranceCompany.DataSource = null;
                    if (cmbInsuranceCompany.Items != null)
                    {
                        cmbInsuranceCompany.Items.Clear();
                    }
                    cmbInsuranceCompany.DataSource = dtCmbTemp;
                    if (dtCmbTemp != null)
                    {
                        if (dtCmbTemp.Columns.Contains("nInsCompanyID"))
                        {
                            cmbInsuranceCompany.ValueMember = dtCmbTemp.Columns["nInsCompanyID"].ColumnName;
                        }
                        if (dtCmbTemp.Columns.Contains("sInsCompany"))
                        {
                            cmbInsuranceCompany.DisplayMember = dtCmbTemp.Columns["sInsCompany"].ColumnName;
                        }
                    }
                    c1StdFeeSchedule.DataSource = null;
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void ts_GenerateEOBFeeSchedule_Click(object sender, EventArgs e)
        {
            
        }

        private void btnGenerateFeeSchedule_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            if (cmbInsuranceCompany.Items.Count == 0)
            {
                MessageBox.Show("Select Insurance Company.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                isValid = false;
                cmbInsuranceCompany.Focus();
            }
            else  if (!mskEobStartDate.MaskCompleted)
            {
                MessageBox.Show("Enter Start Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                isValid = false;
                mskEobStartDate.Focus();
            }
            else if (!mskEobToDate.MaskCompleted)
            {
                MessageBox.Show("Enter End Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                isValid = false;
                mskEobToDate.Focus();
            }
            else if (Convert.ToDateTime((mskEobToDate.Text)) < Convert.ToDateTime((mskEobStartDate.Text)) && Convert.ToDateTime(mskEobToDate.Text) != Convert.ToDateTime(mskEobStartDate.Text))
            {
                MessageBox.Show("Start date should be less than End date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                isValid = false;
                mskEobToDate.Focus();
            }

            if (isValid)
            {
                GenerateFeeScheduele();
            }
        }

        private void cmbInsuranceCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearGrid();
        }
        
        private DataTable GenerateExpCollection()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtCPT = null;
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@SDate", mskEobStartDate.Text, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@EDate", mskEobToDate.Text, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@insuranceCompanies", cmbInsuranceCompany.SelectedValue, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("gsp_GenerateExpCollection", oDBParameters, out dtCPT);
                oDB.Disconnect();
                oDB.Dispose();
                oDBParameters.Dispose();
                return dtCPT;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                oDB.Disconnect();
                oDB.Dispose();
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }
        }

        private void GenerateFeeScheduele()
        {
            DataTable dtCPTEOB = null;
            try
            {
                dtCPTEOB = GenerateExpCollection();
                c1StdFeeSchedule.DataSource = null;
                c1StdFeeSchedule.DataSource = dtCPTEOB.DefaultView;
                DesignGrid();
                if (c1StdFeeSchedule.Rows.Count > 1)
                {
                    c1StdFeeSchedule.Select(1, COL_HCPCS);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (dtCPTEOB != null) { dtCPTEOB.Dispose(); dtCPTEOB = null; }
            }
        }
        
        private void SaveFeeScheduleInsCompanyLink()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtCPT = new DataTable();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@FeeScheduleName", txt_StdFeeScheduleType.Text, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@InsCompanyId", cmbInsuranceCompany.SelectedValue, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@LoginUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@CollectionStartDate", mskEobStartDate.Text, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@CollectionEndDate", mskEobToDate.Text, ParameterDirection.Input, SqlDbType.DateTime);
                oDB.Execute("gsp_IN_FeeSchInsCompLink", oDBParameters);
                oDB.Disconnect();
                oDB.Dispose();
                oDBParameters.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                oDB.Disconnect();
                oDB.Dispose();
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (dtCPT != null) { dtCPT.Dispose(); dtCPT = null; }
            }
        }

        private void ClearGrid()
        {
            try
            {
                if (c1StdFeeSchedule != null && c1StdFeeSchedule.Rows.Count > 0)
                {
                    if (c1StdFeeSchedule.DataSource != null) { c1StdFeeSchedule.DataSource = null; }
                    c1StdFeeSchedule.Rows.RemoveRange(1, c1StdFeeSchedule.Rows.Count - 1);
                    DesignGrid();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private long GetClinicBillingSetting()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = null;
            long SettingID = 0;
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID , ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sSettingName", "FeeSchedule", ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("gsp_GetBillingMasterSettings", oDBParameters, out dt);
                oDB.Disconnect();
                oDB.Dispose();
                oDBParameters.Dispose();
                if (dt != null && dt.Rows.Count > 0)
                {
                    SettingID = Convert.ToInt64(dt.Rows[0]["nSettingID"]);
                }
                return SettingID;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                oDB.Disconnect();
                oDB.Dispose();
                return SettingID;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }
        }

        private void mskEobStartDate_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateMaskedDate(sender))
            {
                e.Cancel = true;
            }
        }

        private void mskEobToDate_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidateMaskedDate(sender))
            {
                e.Cancel = true;
            }
        }

        private bool ValidateMaskedDate(object sender)
        {
            bool isValid = true;
            try
            {
                MaskedTextBox mskEndDate = (MaskedTextBox)sender;
                mskEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskEndDate.Text;
                mskEndDate.TextMaskFormat = MaskFormat.IncludeLiterals;

                if (mskEndDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskEndDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            isValid = false;
                        }

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                isValid = false;
            }
            return isValid;
        }
        #endregion

        
    }
}