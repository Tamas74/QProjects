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
    public partial class frmBillingTraySelection : Form
    {

        #region " Variable Declaration "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseConnectionString = "";
        private Int64 _clinicId = 0;
        private Int64 _userId = 0;
        private string _userName = "";
        private string _messageBoxCaption = "";

        private Int64 _selectedTrayId = 0;
        private bool _isFormLoading = false;
        private DialogResult _dlgRst = DialogResult.None;
        private bool _isChargeTray = false;
        private string _SelectedTrayName = "";

        #endregion

        #region " Grid Constants "

        private const int COL_SELECT = 0;
        private const int COL_TRAYID = 1;
        private const int COL_TRAYCODE = 2;
        private const int COL_TRAYDESC = 3;
        private const int COL_DEFAULT = 4;

        private const int COL_COUNT = 5;

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

        public bool IsOperationPerformed { get; set; }

        public bool IsChargeTrayLoaded { get; set; }

        public Int64 LoadedChargeTrayID { get; set; }

        public string LoadedChargeTray { get; set; }

        public bool IsPaymentTrayLoaded { get; set; }

        public Int64 LoadedPaymentTrayID { get; set; }

        public string LoadedPaymentTray { get; set; }

        #endregion 

        #region " Constructor "

        public frmBillingTraySelection(string Databaseconnectionstring)
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
         
        private void frmBillingTraySelection_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1TrayList, false);
            if (_isChargeTray == true)
            {
                this.Text = "Select Charge Tray";
                this.Icon = global :: gloBilling.Properties.Resources.Charges_Try;
            }
            else
            {
                this.Text = "Select Payment Tray";
                this.Icon = global :: gloBilling.Properties.Resources.Payment_Tray;
            }

            DesignGrid();
            FillUserTray();
        }

        #endregion  " Form Load Event "

        #region " Toolstrip button click event "

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            if (_isChargeTray)
            {
                SetDefaultChargeTray();
            }
            else
            {
                SetDefaultPaymentTray();
            }
            _dlgRst = DialogResult.OK;
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
                DesignGrid();
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

                if (c1TrayList.RowSel > 0)
                {
                    if (c1TrayList.GetData(c1TrayList.RowSel, COL_TRAYID) != null && c1TrayList.GetData(c1TrayList.RowSel, COL_TRAYID).ToString() != "")
                    {
                        _TrayID = Convert.ToInt64(c1TrayList.GetData(c1TrayList.RowSel, COL_TRAYID));
                        
                        if (_isChargeTray == true)
                        {
                            frmSetupChargesTray ofrmSetupChargesTray = new frmSetupChargesTray(_TrayID, _databaseConnectionString);
                            ofrmSetupChargesTray.StartPosition = FormStartPosition.CenterScreen;
                            ofrmSetupChargesTray.ShowDialog(this);
                            if (ofrmSetupChargesTray.IsOperationPerformed)
                            {
                                this.SelectedTrayID = ofrmSetupChargesTray.SelectedTrayID;
                                this.SelectedTrayName = ofrmSetupChargesTray.SelectedTrayName;
                                this.IsOperationPerformed = ofrmSetupChargesTray.IsOperationPerformed;
                            }
                            else
                            {
                                this.SelectedTrayID = 0;
                                this.SelectedTrayName = string.Empty;
                                this.IsOperationPerformed = false;
                            }
                            ofrmSetupChargesTray.Dispose();
                        }
                        else
                        {
                            frmSetupCloseDayJournals ofrmSetupCloseDayJournals = new frmSetupCloseDayJournals(_TrayID, _databaseConnectionString);
                            ofrmSetupCloseDayJournals.StartPosition = FormStartPosition.CenterScreen;
                            ofrmSetupCloseDayJournals.ShowDialog(this);
                            if (ofrmSetupCloseDayJournals.IsOperationPerformed)
                            {
                                this.SelectedTrayID = ofrmSetupCloseDayJournals.SelectedTrayID;
                                this.SelectedTrayName = ofrmSetupCloseDayJournals.SelectedTrayName;
                                this.IsOperationPerformed = ofrmSetupCloseDayJournals.IsOperationPerformed;
                            }
                            else
                            {
                                this.SelectedTrayID = 0;
                                this.SelectedTrayName = string.Empty;
                                this.IsOperationPerformed = false;
                            }
                            ofrmSetupCloseDayJournals.Dispose();
                        }
                        DesignGrid();
                        FillUserTray();
                    }
                }
                else
                {
                    if(_isChargeTray == true)
                    MessageBox.Show("Please select charge tray.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    MessageBox.Show("Please select payment tray.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    c1TrayList.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }                            
        }
        

        #endregion 

        #region " Private & Public Methods "

        private void DesignGrid()
        {
            try
            {
                _isFormLoading = true;

                c1TrayList.Clear(ClearFlags.All);

                c1TrayList.Cols.Count = COL_COUNT;
                c1TrayList.Rows.Count = 1;
                c1TrayList.ScrollBars = ScrollBars.Vertical;
                c1TrayList.AutoResize = false;
                c1TrayList.AllowResizing = AllowResizingEnum.None;
                
                c1TrayList.SetData(0, COL_SELECT, "Select");
                c1TrayList.SetData(0, COL_TRAYID, "ID");
                c1TrayList.SetData(0, COL_TRAYCODE, "Code");
                c1TrayList.SetData(0, COL_TRAYDESC, "Description");
                c1TrayList.SetData(0, COL_DEFAULT, "Default");

                c1TrayList.Cols[COL_SELECT].DataType = typeof(System.Boolean);
                c1TrayList.Cols[COL_TRAYID].DataType = typeof(System.String);
                c1TrayList.Cols[COL_TRAYCODE].DataType = typeof(System.String);
                c1TrayList.Cols[COL_TRAYDESC].DataType = typeof(System.String);
                c1TrayList.Cols[COL_DEFAULT].DataType = typeof(System.String);

                c1TrayList.Cols[COL_SELECT].Width = 50;
                c1TrayList.Cols[COL_TRAYID].Width = 0;
                c1TrayList.Cols[COL_TRAYCODE].Width = 0;
                c1TrayList.Cols[COL_TRAYDESC].Width = 325;
                c1TrayList.Cols[COL_DEFAULT].Width = 100;

                c1TrayList.Cols[COL_SELECT].Visible = true;
                c1TrayList.Cols[COL_TRAYID].Visible = false;
                c1TrayList.Cols[COL_TRAYCODE].Visible = false;
                c1TrayList.Cols[COL_TRAYDESC].Visible = true;
                c1TrayList.Cols[COL_DEFAULT].Visible = true;

                c1TrayList.AllowEditing = true;
                c1TrayList.Cols[COL_SELECT].AllowEditing = true;
                c1TrayList.Cols[COL_TRAYID].AllowEditing = false;
                c1TrayList.Cols[COL_TRAYCODE].AllowEditing = false;
                c1TrayList.Cols[COL_TRAYDESC].AllowEditing = false;
                c1TrayList.Cols[COL_DEFAULT].AllowEditing = false;


                c1TrayList.VisualStyle = VisualStyle.Office2007Blue;
                c1TrayList.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                c1TrayList.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                c1TrayList.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);
                c1TrayList.SelectionMode = SelectionModeEnum.Row;


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            { _isFormLoading = false; }
        }

        private void FillUserTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = null;
            string _sqlRetrieveQuery = String.Empty;
            try
            {
                _isFormLoading = true;

                //Code added on 20100115 by Mukesh Patel
                // To display only user try

                if (_isChargeTray == true)
                {
                    _sqlRetrieveQuery = "Select nChargeTrayID AS nID,sCode,sDescription,nNumberOfDays,Case when bIsDefault=0 Then '' else 'Default' end, " +
                                        " CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nStartDate), 101) AS nStartDate,CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nEndDate), 101) AS nEndDate ," +
                                        " Case when isnull(bIsClosed,0)=0 Then 'Active' else 'Closed' end from BL_ChargesTray WITH(NOLOCK) " +
                                        " where nUserID='" + _userId + "' AND ISNULL(bIsClosed,0) = 0 AND ISNULL(bIsActive,0)=1 AND nClinicID = " + _clinicId + "";
                    
                }
                else
                {
                    _sqlRetrieveQuery = "Select nCloseDayTrayID AS nID,sCode,sDescription,nNumberOfDays,Case when bIsDefault=0 " +
                        " Then '' else 'Default' end ,CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nStartDate), 101) AS nStartDate, " +
                        " CONVERT(VARCHAR, dbo.CONVERT_TO_DATE(nEndDate), 101) AS nEndDate,Case when isnull(bIsClosed,0)=0 " +
                        " Then 'Active' else 'Closed' end from BL_CloseDayTray WITH(NOLOCK) " +
                        " where nUserID='" + _userId + "' AND ISNULL(bIsClosed,0) = 0 AND  ISNULL(bIsActive,0)=1 AND nClinicID = " + _clinicId + "";
                }


                oDB.Connect(false);
                oDB.Retrive_Query(_sqlRetrieveQuery, out dt);
                oDB.Disconnect();

                if (dt != null && dt.Rows.Count > 0)
                {
                    int _rowIndex = 0;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _rowIndex = c1TrayList.Rows.Add().Index;
                        c1TrayList.SetData(_rowIndex, COL_SELECT, false);
                        c1TrayList.SetData(_rowIndex, COL_TRAYID, Convert.ToString(dt.Rows[i]["nID"]));
                        c1TrayList.SetData(_rowIndex, COL_TRAYCODE, Convert.ToString(dt.Rows[i]["sCode"]));
                        c1TrayList.SetData(_rowIndex, COL_TRAYDESC, Convert.ToString(dt.Rows[i]["sDescription"]));
                        c1TrayList.SetData(_rowIndex, COL_DEFAULT, Convert.ToString(dt.Rows[i]["Column1"]));
                    }

                    gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseConnectionString);
                    Object _retValue = null;

                    if (_isChargeTray == true)
                    { oSettings.GetSetting("CHARGES_LASTCLOSETRAYID", _userId, _clinicId, out _retValue); }
                    else
                    { oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", _userId, _clinicId, out _retValue); }

                    oSettings.Dispose();

                    if (_retValue != null && Convert.ToString(_retValue).Trim() != "")
                    {
                        if (c1TrayList != null && c1TrayList.Rows.Count > 1)
                        {
                            for (int rIndex = 1; rIndex < c1TrayList.Rows.Count; rIndex++)
                            {
                                if (c1TrayList.GetData(rIndex, COL_TRAYID) != null && Convert.ToString(c1TrayList.GetData(rIndex, COL_TRAYID)).Trim() != ""
                                 && Convert.ToString(c1TrayList.GetData(rIndex, COL_TRAYID)).Trim() == Convert.ToString(_retValue).Trim())
                                {
                                    c1TrayList.SetCellCheck(rIndex, COL_SELECT, CheckEnum.Checked);
                                    c1TrayList.Rows[rIndex].Selected = true;     
                                    c1TrayList.Rows[rIndex].Move(1);                                                                   
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                _isFormLoading = false;
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
                _SqlQuery = "Select nUserID from User_MST WITH(NOLOCK) where nUserID='" + _userId + "' and nClinicID='" + _clinicId + "' and nAdministrator=1";
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

        private void SetDefaultPaymentTray()
        {
            DataTable dtGetDefaultPaymentTrayDescription = null;
            dtGetDefaultPaymentTrayDescription = gloAccountsV2.gloInsurancePaymentV2.GetDefaultPaymentTrayDescription();
            if (SelectedTrayID == 0)
            {
                if (c1TrayList != null && c1TrayList.Rows.Count > 1)
                {
                    for (int rIndex = 1; rIndex < c1TrayList.Rows.Count; rIndex++)
                    {
                        // If payment tray selected on dialog then set the tray
                        if (c1TrayList.GetCellCheck(rIndex, COL_SELECT) == CheckEnum.Checked) 
                        {
                            if (Convert.ToInt64(c1TrayList.GetData(rIndex, COL_TRAYID)) > 0 && (gloAccountsV2.gloBillingCommonV2.IsPaymentTrayActive(Convert.ToInt64(c1TrayList.GetData(rIndex, COL_TRAYID)))))
                            {
                                this.SelectedTrayID = Convert.ToInt64(c1TrayList.GetData(rIndex, COL_TRAYID));
                                this.SelectedTrayName = Convert.ToString(c1TrayList.GetData(rIndex, COL_TRAYDESC));
                                break;
                            }
                        }
                        else if (!IsPaymentTrayLoaded) // If payment tray is not loaded on patient payment screen and no tray is checked on dialog then set the default tray
                        {
                            if (dtGetDefaultPaymentTrayDescription != null && dtGetDefaultPaymentTrayDescription.Rows.Count > 0)
                            {
                                this.SelectedTrayID = Convert.ToInt64(dtGetDefaultPaymentTrayDescription.Rows[0]["nCloseDayTrayID"]);
                                this.SelectedTrayName = Convert.ToString(dtGetDefaultPaymentTrayDescription.Rows[0]["sDescription"]);
                            }
                            else // If default,last selected and loaded trays are not present then set blank
                            {
                                this.SelectedTrayID = 0;
                                this.SelectedTrayName = string.Empty;
                            }
                        } // If both default and last selected tray are not present then set already loaded payment trays
                        else
                        {
                            if (gloAccountsV2.gloBillingCommonV2.IsPaymentTrayActive(this.LoadedPaymentTrayID))
                            {
                                this.SelectedTrayID = this.LoadedPaymentTrayID;
                                this.SelectedTrayName = this.LoadedPaymentTray;
                            }
                            else // If default,last selected and loaded trays are not present then set blank
                            {
                                this.SelectedTrayID = 0;
                                this.SelectedTrayName = string.Empty;
                            }
                        }
                    }
                }
            }
      }

        private void SetDefaultChargeTray()
        {
            DataTable dtGetDefaultChargeTrayDescription = null;
            dtGetDefaultChargeTrayDescription = gloAccountsV2.gloInsurancePaymentV2.GetDefaultChargeTrayDescription();
            if (SelectedTrayID == 0)
            {
                if (c1TrayList != null && c1TrayList.Rows.Count > 1)
                {
                    for (int rIndex = 1; rIndex < c1TrayList.Rows.Count; rIndex++)
                    {
                        // If payment tray selected on dialog then set the tray
                        if (c1TrayList.GetCellCheck(rIndex, COL_SELECT) == CheckEnum.Checked)
                        {
                            if (Convert.ToInt64(c1TrayList.GetData(rIndex, COL_TRAYID)) > 0 && (gloAccountsV2.gloBillingCommonV2.IsChargeTrayActive(Convert.ToInt64(c1TrayList.GetData(rIndex, COL_TRAYID)))))
                            {
                                this.SelectedTrayID = Convert.ToInt64(c1TrayList.GetData(rIndex, COL_TRAYID));
                                this.SelectedTrayName = Convert.ToString(c1TrayList.GetData(rIndex, COL_TRAYDESC));
                                break;
                            }
                        }
                        else if (!IsChargeTrayLoaded) // If payment tray is not loaded on patient payment screen and no tray is checked on dialog then set the default tray
                        {
                            if (dtGetDefaultChargeTrayDescription != null && dtGetDefaultChargeTrayDescription.Rows.Count > 0)
                            {
                                this.SelectedTrayID = Convert.ToInt64(dtGetDefaultChargeTrayDescription.Rows[0]["nChargeTrayID"]);
                                this.SelectedTrayName = Convert.ToString(dtGetDefaultChargeTrayDescription.Rows[0]["sDescription"]);
                            }
                            else // If default,last selected and loaded trays are not present then set blank
                            {
                                this.SelectedTrayID = 0;
                                this.SelectedTrayName = string.Empty;
                            }
                        } // If both default and last selected tray are not present then set already loaded payment trays
                        else
                        {
                            if (gloAccountsV2.gloBillingCommonV2.IsPaymentTrayActive(this.LoadedChargeTrayID))
                            {
                                this.SelectedTrayID = this.LoadedChargeTrayID;
                                this.SelectedTrayName = this.LoadedChargeTray;
                            }
                            else // If default,last selected and loaded trays are not present then set blank
                            {
                                this.SelectedTrayID = 0;
                                this.SelectedTrayName = string.Empty;
                            }
                        }
                    }
                }
            }
        }

        #endregion 

        #region " Grid Events "

        private void c1TrayList_CellChanged(object sender, RowColEventArgs e)
        {
            if (_isFormLoading == false)
            {
                try
                {
                    this.c1TrayList.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1TrayList_CellChanged);
                    if (c1TrayList != null && c1TrayList.Rows.Count > 1)
                    {
                        for (int rIndex = 1; rIndex < c1TrayList.Rows.Count; rIndex++)
                        {
                            if (e.Row != rIndex)
                            {
                                c1TrayList.SetCellCheck(rIndex, COL_SELECT, CheckEnum.Unchecked);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null; 
                }
                finally
                {
                    this.c1TrayList.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1TrayList_CellChanged);
                }
            }
        }

        private void c1TrayList_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        #endregion " Grid Events "

        

    }
}
