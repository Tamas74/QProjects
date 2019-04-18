using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace gloBilling
{
    public partial class frmBillingNDCCodeSelection : Form
    {

        #region " Variable Declaration "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseConnectionString = "";
        private Int64 _clinicId = 0;
        private Int64 _userId = 0;
        private string _userName = "";
        private string _messageBoxCaption = "";

        private Int64 _selectedTrayId = 0;
      //  private bool _isFormLoading = false;
        private DialogResult _dlgRst = DialogResult.None;
        private bool _isChargeTray = false;
        private string _SelectedTrayName = "";
        #endregion

        #region " Grid Constants "

        private const int COL_SELECT = 0;
        private const int COL_TRAYID = 1;
        private const int COL_TRAYCODE = 2;
        //private const int COL_TRAYDESC = 3;
        //private const int COL_DEFAULT = 4;

        private const int COL_COUNT = 3;

        #endregion 

        #region " Property Procedures "

        public string DatabaseConnectionString
        {
            get { return _databaseConnectionString; }
            set { _databaseConnectionString = value; }
        }
        public Int64 ClinicID
        {
            get { return _clinicId; }
            set { _clinicId = value; }
        }
        public Int64 UserID
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string SelectedTrayName
        {
            get { return _SelectedTrayName; }
            set { _SelectedTrayName = value; }
        }

        public Int64 SelectedTrayID
        {
            get { return _selectedTrayId; }
            set { _selectedTrayId = value; }
        }

        public DialogResult FormResult
        {
            get { return _dlgRst; }
            set { _dlgRst = value; }
        }

        public bool IsChargeTray
        {
            get { return _isChargeTray; }
            set { _isChargeTray = value; }
        }

        #endregion 

        #region " Constructor "

        public frmBillingNDCCodeSelection(string Databaseconnectionstring)
        {
            InitializeComponent();

            _databaseConnectionString = Databaseconnectionstring;

            #region " Retrive ClinicID from appSetting "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicId = 0; }
            }
            else
            { _clinicId = 0; }

            #endregion " Retrive ClinicID from appSetting "

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _userId = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _userId = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _userName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _userName = "";
            }

            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion
        }

        #endregion 

        #region  " Form Load Event "

        private void frmBillingNDCCodeSelection_Load(object sender, EventArgs e)
        {
            //if (_isChargeTray == true)
            //{
                //this.Text = "Select NDC Code";
                //this.Icon = global :: gloBilling.Properties.Resources.Charges_Try;
            //}
            //else
            //{
            //    this.Text = "Select Payment Tray";
            //    this.Icon = global :: gloBilling.Properties.Resources.Payment_Tray;
            //}
            tlsbtnAdd.Visible = false;
            tlsbtnModify.Visible = false;
            //DesignGrid();
            FillUserTray();
        }

        #endregion  " Form Load Event "

        #region " Toolstrip button click event "

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            _dlgRst = DialogResult.OK;
            if (c1NDCCodeList != null && c1NDCCodeList.Rows.Count > 1)
            {
                //for (int rIndex = 1; rIndex < c1NDCCodeList.Rows.Count; rIndex++)
                //{
                //    if (c1NDCCodeList.GetCellCheck(rIndex, COL_SELECT) == CheckEnum.Checked)
                //    {
                        _selectedTrayId = Convert.ToInt64(c1NDCCodeList.GetData(c1NDCCodeList.RowSel, 0));
                        _SelectedTrayName = Convert.ToString(c1NDCCodeList.GetData(c1NDCCodeList.RowSel, 1));
                //        break;
                //    }
                //}
            }
            this.Close();
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            _selectedTrayId = 0;
            _SelectedTrayName = "";
            _dlgRst = DialogResult.Cancel;
            this.Close();
        }

        private void tlsbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isChargeTray == true)
                {
                    frmSetupChargesTray ofrmSetupChargesTray = new frmSetupChargesTray(0, _databaseConnectionString);
                    ofrmSetupChargesTray.StartPosition = FormStartPosition.CenterScreen;
                    ofrmSetupChargesTray.ShowDialog(this);
                    ofrmSetupChargesTray.Dispose();
                }
                else
                {
                    frmSetupCloseDayJournals ofrmSetupCloseDayJournals = new frmSetupCloseDayJournals(0, _databaseConnectionString);
                    ofrmSetupCloseDayJournals.StartPosition = FormStartPosition.CenterScreen;
                    ofrmSetupCloseDayJournals.ShowDialog(this);
                    ofrmSetupCloseDayJournals.Dispose();
                }
                //DesignGrid();
                FillUserTray();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tlsbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 _TrayID = 0;

                if (c1NDCCodeList.RowSel > 0)
                {
                    if (c1NDCCodeList.GetData(c1NDCCodeList.RowSel, COL_TRAYID) != null && c1NDCCodeList.GetData(c1NDCCodeList.RowSel, COL_TRAYID).ToString() != "")
                    {
                        _TrayID = Convert.ToInt64(c1NDCCodeList.GetData(c1NDCCodeList.RowSel, COL_TRAYID));

                        if (_isChargeTray == true)
                        {
                            frmSetupChargesTray ofrmSetupChargesTray = new frmSetupChargesTray(_TrayID, _databaseConnectionString);
                            ofrmSetupChargesTray.StartPosition = FormStartPosition.CenterScreen;
                            ofrmSetupChargesTray.ShowDialog(this);
                            ofrmSetupChargesTray.Dispose();
                        }
                        else
                        {
                            frmSetupCloseDayJournals ofrmSetupCloseDayJournals = new frmSetupCloseDayJournals(_TrayID, _databaseConnectionString);
                            ofrmSetupCloseDayJournals.StartPosition = FormStartPosition.CenterScreen;
                            ofrmSetupCloseDayJournals.ShowDialog(this);
                            ofrmSetupCloseDayJournals.Dispose();
                        }
                        //DesignGrid();
                        FillUserTray();
                    }
                }
                else
                {
                    if(_isChargeTray == true)
                    MessageBox.Show("Please select charge tray.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    MessageBox.Show("Please select payment tray.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1NDCCodeList.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }                            
        }
        

        #endregion 

        #region " Private & Public Methods "

        //private void DesignGrid()
        //{
        //    try
        //    {
        //        _isFormLoading = true;

        //        c1NDCCodeList.Clear(ClearFlags.All);

        //        c1NDCCodeList.Cols.Count = COL_COUNT;
        //        c1NDCCodeList.Rows.Count = 1;
        //        c1NDCCodeList.ScrollBars = ScrollBars.Vertical;
        //        c1NDCCodeList.AutoResize = false;
        //        c1NDCCodeList.AllowResizing = AllowResizingEnum.None;
                
        //        c1NDCCodeList.SetData(0, COL_SELECT, "Select");
        //        c1NDCCodeList.SetData(0, COL_TRAYID, "ID");
        //        c1NDCCodeList.SetData(0, COL_TRAYCODE, "Code");
        //        //c1NDCCodeList.SetData(0, COL_TRAYDESC, "Description");
        //        //c1NDCCodeList.SetData(0, COL_DEFAULT, "Default");

        //        c1NDCCodeList.Cols[COL_SELECT].DataType = typeof(System.Boolean);
        //        c1NDCCodeList.Cols[COL_TRAYID].DataType = typeof(System.String);
        //        c1NDCCodeList.Cols[COL_TRAYCODE].DataType = typeof(System.String);
        //        //c1NDCCodeList.Cols[COL_TRAYDESC].DataType = typeof(System.String);
        //        //c1NDCCodeList.Cols[COL_DEFAULT].DataType = typeof(System.String);

        //        c1NDCCodeList.Cols[COL_SELECT].Width = 50;
        //        c1NDCCodeList.Cols[COL_TRAYID].Width = 0;
        //        c1NDCCodeList.Cols[COL_TRAYCODE].Width = 300;
        //        //c1NDCCodeList.Cols[COL_TRAYDESC].Width = 325;
        //        //c1NDCCodeList.Cols[COL_DEFAULT].Width = 100;

        //        c1NDCCodeList.Cols[COL_SELECT].Visible = true;
        //        c1NDCCodeList.Cols[COL_TRAYID].Visible = false;
        //        c1NDCCodeList.Cols[COL_TRAYCODE].Visible = true;
        //        //c1NDCCodeList.Cols[COL_TRAYDESC].Visible = true;
        //        //c1NDCCodeList.Cols[COL_DEFAULT].Visible = true;

        //        c1NDCCodeList.AllowEditing = true;
        //        c1NDCCodeList.Cols[COL_SELECT].AllowEditing = true;
        //        c1NDCCodeList.Cols[COL_TRAYID].AllowEditing = false;
        //        c1NDCCodeList.Cols[COL_TRAYCODE].AllowEditing = false;
        //        //c1NDCCodeList.Cols[COL_TRAYDESC].AllowEditing = false;
        //        //c1NDCCodeList.Cols[COL_DEFAULT].AllowEditing = false;


        //        c1NDCCodeList.VisualStyle = VisualStyle.Office2007Blue;
        //        c1NDCCodeList.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
        //        c1NDCCodeList.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
        //        c1NDCCodeList.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);
        //        c1NDCCodeList.SelectionMode = SelectionModeEnum.Row;


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    finally
        //    { _isFormLoading = false; }
        //}

        private void FillUserTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataSet dsNDCCode = null;
            string _sqlRetrieveQuery = String.Empty;
            try
            {
                //_isFormLoading = true;


                #region " Commented Code "

                //if (IsAdmin() != true)
                //{
                //    if (_isChargeTray == true)
                //    {
                //        _sqlRetrieveQuery = "Select nChargeTrayID AS nID,sCode,sDescription,nNumberOfDays,Case when bIsDefault=0 Then '' else 'Default' end, " +
                //                            " CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nStartDate), 101) AS nStartDate,CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nEndDate), 101) AS nEndDate ," +
                //                            " Case when isnull(bIsClosed,0)=0 Then 'Active' else 'Closed' end from BL_ChargesTray "+
                //                            " where nUserID='" + _userId + "' AND ISNULL(bIsClosed,0) = 0 AND bIsActive <> 0 AND nClinicID = " + _clinicId + "";

                //        //_sqlRetrieveQuery = "SELECT nChargeTrayID AS nID,sCode, " +
                //        //" sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                //        //" FROM BL_ChargesTray WHERE nChargeTrayID IS NOT NULL AND sDescription IS NOT NULL AND nChargeTrayID > 0 " +
                //        //"AND sDescription <> '' AND ISNULL(bIsClosed,0) = 0 AND nClinicID = " + _clinicId + "";
                //    }
                //    else
                //    {
                //        _sqlRetrieveQuery = "Select nCloseDayTrayID AS nID,sCode,sDescription,nNumberOfDays,Case when bIsDefault=0 "+
                //            " Then '' else 'Default' end ,CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nStartDate), 101) AS nStartDate, "+
                //            " CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nEndDate), 101) AS nEndDate,Case when isnull(bIsClosed,0)=0 "+
                //            " Then 'Active' else 'Closed' end from BL_CloseDayTray "+
                //            " where nUserID='" + _userId + "' AND ISNULL(bIsClosed,0) = 0 AND bIsActive <> 0 AND nClinicID = " + _clinicId + "";

                //        //_sqlRetrieveQuery = "SELECT nCloseDayTrayID AS nID,sCode, " +
                //        //" sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                //        //" FROM BL_CloseDayTray WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                //        //"AND sDescription <> '' AND ISNULL(bIsClosed,0) = 0 AND nClinicID = " + _clinicId + "";
                //    }
                //}
                //else
                //{
                //    if (_isChargeTray == true)
                //    {
                //        _sqlRetrieveQuery = "Select nChargeTrayID AS nID,sCode,sDescription,nNumberOfDays,Case when bIsDefault=0 Then '' else 'Default' end, " +
                //                             " CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nStartDate), 101) AS nStartDate,CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nEndDate), 101) AS nEndDate ," +
                //                             " Case when isnull(bIsClosed,0)=0 Then 'Active' else 'Closed' end from BL_ChargesTray where bIsActive <> 0 AND ISNULL(bIsClosed,0) = 0 AND nClinicID = " + _clinicId + "";

                //        //_sqlRetrieveQuery = "SELECT nChargeTrayID AS nID,sCode, " +
                //        //" sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                //        //" FROM BL_ChargesTray WHERE nChargeTrayID IS NOT NULL AND sDescription IS NOT NULL AND nChargeTrayID > 0 " +
                //        //"AND sDescription <> '' AND nUserID = " + _userId + " AND ISNULL(bIsClosed,0) = 0 AND nClinicID = " + _clinicId + "";
                //    }
                //    else
                //    {
                //        _sqlRetrieveQuery = "Select nCloseDayTrayID AS nID,sCode,sDescription,nNumberOfDays,Case when bIsDefault=0 "+
                //        " Then '' else 'Default' end ,CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nStartDate), 101) AS nStartDate, "+
                //        " CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nEndDate), 101) AS nEndDate,Case when isnull(bIsClosed,0)=0 "+
                //        " Then 'Active' else 'Closed' end from BL_CloseDayTray  where bIsActive <> 0 AND ISNULL(bIsClosed,0) = 0 AND nClinicID = " + _clinicId + "";

                //        //_sqlRetrieveQuery = "SELECT nCloseDayTrayID AS nID,sCode, " +
                //        //" sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                //        //" FROM BL_CloseDayTray WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                //        //"AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0  AND nUserID = " + _userId + " AND nClinicID = " + _clinicId + "";
                //    }
                //}
                #endregion " Commented Code "


                _sqlRetrieveQuery = "select sNDCCode,ndrugsid,0 as stype from drugs_mst";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlRetrieveQuery, out dsNDCCode);
                oDB.Disconnect();

                if (dsNDCCode != null && dsNDCCode.Tables[0].Rows.Count > 0)
                {
                    //int _rowIndex = 0;
                    c1NDCCodeList.AutoGenerateColumns = false;
                    dsNDCCode.Tables[0].TableName = "dtNDCCode";
                    c1NDCCodeList.DataMember = "dtNDCCode";
                    c1NDCCodeList.DataSource = dsNDCCode;
                    c1NDCCodeList.Refresh();
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    _rowIndex = c1NDCCodeList.Rows.Add().Index;
                    //    c1NDCCodeList.SetData(_rowIndex, COL_SELECT, false);
                    //    c1NDCCodeList.SetData(_rowIndex, COL_TRAYID, Convert.ToString(dt.Rows[i]["ndrugsid"]));
                    //    c1NDCCodeList.SetData(_rowIndex, COL_TRAYCODE, Convert.ToString(dt.Rows[i]["sNDCCode"]));
                    //    //c1NDCCodeList.SetData(_rowIndex, COL_TRAYDESC, Convert.ToString(dt.Rows[i]["sNDCCode"]));
                    //    //c1NDCCodeList.SetData(_rowIndex, COL_DEFAULT, Convert.ToString(dt.Rows[i]["Column1"]));
                    //}

                    //gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseConnectionString);
                    //Object _retValue = null;

                    //if (_isChargeTray == true)
                    //{ oSettings.GetSetting("CHARGES_LASTCLOSETRAYID", _userId, _clinicId, out _retValue); }
                    //else
                    //{ oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", _userId, _clinicId, out _retValue); }

                    //oSettings.Dispose();

                    //if (_retValue != null && Convert.ToString(_retValue).Trim() != "")
                    //{
                //        if (c1NDCCodeList != null && c1NDCCodeList.Rows.Count > 1)
                //        {
                //            for (int rIndex = 1; rIndex < c1NDCCodeList.Rows.Count; rIndex++)
                //            {
                //                if (c1NDCCodeList.GetData(rIndex, COL_TRAYID) != null && Convert.ToString(c1NDCCodeList.GetData(rIndex, COL_TRAYID)).Trim() != ""
                //                 && Convert.ToString(c1NDCCodeList.GetData(rIndex, COL_TRAYID)).Trim() == Convert.ToString(_retValue).Trim())
                //                {
                //                    c1NDCCodeList.SetCellCheck(rIndex, COL_SELECT, CheckEnum.Checked);
                //                    c1NDCCodeList.Rows[rIndex].Selected = true;     
                //                    c1NDCCodeList.Rows[rIndex].Move(1);                                                                   
                //                    break;
                //                }
                //            }
                //        }
                    //}
                    //this.c1NDCCodeList.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1NDCCodeList_CellChanged);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                //_isFormLoading = false;
                if (oDB != null) { oDB.Dispose(); }
            }

        }

        public bool IsAdmin()
        {
            string _SqlQuery = String.Empty;
            //DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer ODB = null;
            DataTable dt = null;
            bool _IsAdmin = false;
            try
            {
                _SqlQuery = "Select nUserID from User_MST where nUserID='" + _userId + "' and nClinicID='" + _clinicId + "' and nAdministrator=1";
                ODB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                dt = new DataTable();
                ODB.Connect(false);
                ODB.Retrive_Query(_SqlQuery, out dt);
                if (dt != null)
                {
                    if (Convert.ToInt32(dt.Rows.Count) > 0 && Convert.ToInt32(dt.Rows[0]["nUserID"]) > 0)
                    {
                        _IsAdmin = true;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null)
                {
                    ODB.Disconnect();
                    ODB.Dispose();
                }
            }
            return _IsAdmin;
        }

        #endregion 

        #region " Grid Events "

     
       

        private void c1NDCCodeList_CellChanged(object sender, RowColEventArgs e)
        {   
            //if (_isFormLoading == false)
            //{
            //    try
            //    {
                 
            //        //c1NDCCodeList.Clear(ClearFlags.All);
            //        this.c1NDCCodeList.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1NDCCodeList_CellChanged);
            //        if (c1NDCCodeList != null && c1NDCCodeList.Rows.Count > 1)
            //        {

            //            FillUserTray();

            //            //for (int rIndex = 1; rIndex < c1NDCCodeList.Rows.Count; rIndex++)
            //            //{
            //                //if (e.Row != rIndex)
            //                //{
            //                    c1NDCCodeList.SetCellCheck(e.Row, COL_SELECT, CheckEnum.Checked);
            //                    //this.c1NDCCodeList.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1NDCCodeList_CellChanged);
            //                //}
            //            //}
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //        ex = null;
            //    }
            //    finally
            //    {
            //        //_isFormLoading = false;
            //        //this.c1NDCCodeList.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1NDCCodeList_CellChanged);
            //    }
            //}
       }
        #endregion " Grid Events "


    }
}
