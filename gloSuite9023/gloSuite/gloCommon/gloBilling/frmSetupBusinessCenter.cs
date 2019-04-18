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
    public partial class frmSetupBusinessCenter : Form
    {
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _nBusinessCenterID = 0;
      //  private Int64 _ClinicID = 0;
        private bool isClosed = false;
        string _ControlType = "";
      //  private ComboBox combo;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        Int64 _StmtDisplaySettingsID = 0;
        DataTable _dtRemovedItem = new DataTable();


        #region 

        private const int COL_USERID = 0;
        private const int COL_PROVIDERID = 1;
        private const int COL_LOGIN_NAME = 2;
        private const int COL_FIRST_NAME = 3;
        private const int COL_MIDDLE_NAME = 4;
        private const int COL_LAST_NAME = 5;
        private const int COL_EMAIL_ID = 6;
        private const int COL_EXCHANGE_LOGIN = 7;

        private const int COL_COUNT = 8;
        #endregion

        public Int64 StmtDisplaySettingsID
        {
            get { return _StmtDisplaySettingsID; }
            set { _StmtDisplaySettingsID = value; }
        }



        public Int64 ValueID
        {
            set {
                _nBusinessCenterID = value;
            }

               get {
                return _nBusinessCenterID;
            }
        }
        public frmSetupBusinessCenter()
        {
            InitializeComponent();
        }
        public frmSetupBusinessCenter(Int64 ID, String databaseconnectionstring)
        {
            _databaseconnectionstring = databaseconnectionstring;
            _nBusinessCenterID = ID;
            InitializeComponent();
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
        private void frmSetupBusinessCenter_Load(object sender, EventArgs e)
        {
            Cls_TabIndexSettings tabSettings = null;
            try
            {
                if (_nBusinessCenterID != 0)
                {
                    tsb_Save.Visible = false;
                    Fill_BusinessCenterCodes();
                    //Fill_UsersWithDefaultBusinessCenter();
                    Fill_AssociatedUserList();
                }
                else
                {
                    DesignBusinessCenterGrid();
                }

                tabSettings = new Cls_TabIndexSettings(this);
                tabSettings.SetTabOrder(Cls_TabIndexSettings.TabScheme.AcrossFirst);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(),true);
            }
        }
        private void Fill_BusinessCenterCodes()
        {
           // bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                strQuery = "SELECT TOP 1 nBusinessCenterID , " +
                           "         sBusinessCenterCode , " +
                           "         sDescription , " +
                           "         bIsActive , " +
                           " ISNULL(sStatementCriteriaName,'') AS sStatementCriteriaName, " +
                           " nStatementDisplaySettingsID " +
                           " FROM    BL_BusinessCenterCodes " +
                           "         LEFT OUTER JOIN dbo.RPT_PatStatementCriteria_MST ON dbo.BL_BusinessCenterCodes.nStatementDisplaySettingsID = dbo.RPT_PatStatementCriteria_MST.nStatementCriteriaID " +
                           " WHERE   nBusinessCenterID = " + _nBusinessCenterID;
                    

                DataTable dt = new DataTable();
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtCode.Text = Convert.ToString(dt.Rows[0]["sBusinessCenterCode"]);
                    txtDescription.Text = Convert.ToString(dt.Rows[0]["sDescription"]);
                    txtDefaultStmtDisplaySettings.Text = Convert.ToString(dt.Rows[0]["sStatementCriteriaName"]);
                    txtDefaultStmtDisplaySettings.Tag = Convert.ToInt64(dt.Rows[0]["nStatementDisplaySettingsID"]);
                    if (Convert.ToBoolean(dt.Rows[0]["bIsActive"]) == true)
                    {
                        rbActive.Checked = true;
                        rbInactive.Checked = false;
                    }
                    else
                    {
                        rbActive.Checked = false;
                        rbInactive.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {

            }
        }
        private void Fill_UsersWithDefaultBusinessCenter()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = " SELECT BL_BusinessCenter_UsersAssociation.nUserID AS ID , " +
                           "         dbo.User_MST.sLoginName AS DispName " +
                           "  FROM   BL_BusinessCenter_UsersAssociation "+
                           "         INNER JOIN dbo.User_MST ON BL_BusinessCenter_UsersAssociation.nUserID = dbo.User_MST.nUserID "+
                           "  WHERE  BL_BusinessCenter_UsersAssociation.nBusinessCenterID =  " + _nBusinessCenterID;


                DataTable dt = new DataTable();
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    cmbUsers.DataSource = dt;
                    cmbUsers.DisplayMember = "DispName";
                    cmbUsers.ValueMember = "ID";
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {

            }
        }
        private void tsb_Save_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
              
                BusinessCenter ObjBusinessCenterCode = new BusinessCenter(_databaseconnectionstring);
                // if (ObjValueCodes.sBusinessCenterCode(_ValueCodeID, txtCode.Text.Trim()))
                if (ObjBusinessCenterCode.IsExistsBusinessCenterCode(_nBusinessCenterID, txtCode.Text.Trim()))
                {
                    MessageBox.Show("Code is already in use by another entry.  Please select a unique code.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCode.Focus();
                    return;
                }
               
           
                Int64 _tempResult = ObjBusinessCenterCode.AddBusinessCenterCode(_nBusinessCenterID, txtCode.Text.Trim(), txtDescription.Text.Trim(), (rbActive.Checked ? true : false), Convert.ToInt64(txtDefaultStmtDisplaySettings.Tag));
                if (_tempResult > 0)
                {
                    _nBusinessCenterID = 0;// _tempResult;
                    gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.BusinessCenter); 
                }
                else
                {
                    txtCode.Text = "";
                    txtDescription.Text = "";
                    _nBusinessCenterID = 0;
                }

                txtCode.Text = "";
                txtDescription.Text = "";

                if (isClosed == true)
                {
                    this.Close();
                }
            }
            
        }

        private void tsb_Saveclose_Click(object sender, EventArgs e)
        {
            isClosed = true; 
            tsb_Save_Click(null, null);
            //this.Close(); 
        }

        private void tsb_close_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private bool  Validate()
        {
            if (txtCode.Text.Trim()=="")
            {
                MessageBox.Show("Enter a code.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Focus();  
                return false ;
            }
            if (txtDescription.Text.Trim() == "")
            {
                MessageBox.Show("Enter a description.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescription.Focus();
                return false;
            }
            else
            {
                return true;
            }
             
 
        }
        

        # region " Events "
        private void rbInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInactive.Checked == true)
            {
                rbInactive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                rbActive.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
            else
            {
                rbInactive.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
                rbActive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
        }

        private void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbActive.Checked == true)
            {
                rbActive.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
                rbInactive.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
            else
            {
                rbInactive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
                rbActive.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
        }
        #endregion

        private void pnlText_Paint(object sender, PaintEventArgs e)
        {

        }

        #region "Default Statement Display Settings "

        private void btnBrowseStmtDisplaySettings_Click(object sender, EventArgs e)
        {
            try
            {
                this.Height = 500;
                this.Width = 675;
                pnltlsStrip.Visible = false;
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
                oListControl = new gloListControl.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.StmtDisplaySettings, false, this.Width);
                oListControl.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                oListControl.ControlHeader = " Statement Display Settings ";

                _CurrentControlType = gloListControl.gloListControlType.StmtDisplaySettings;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

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

        private void btnClearStmtDisplaySettings_Click(object sender, EventArgs e)
        {
            txtDefaultStmtDisplaySettings.Text = "";
            txtDefaultStmtDisplaySettings.Tag = 0;
            StmtDisplaySettingsID = 0;
        }

        #region "User control events"

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            ComboBox oComboBox = null;

            if (_ControlType == "cmbUsers")
                oComboBox = cmbUsers;
            int _Counter = 0;
            try
            {
                switch (_CurrentControlType)
                {
                    case gloListControl.gloListControlType.Users:
                        {
                            //if (oListControl.SelectedItems.Count > 0)
                            //{
                            //    //DataTable dtSelectedUsers = oListControl.SelectedRecords;
                            //    DataTable oBindTable = new DataTable();

                            //    oBindTable.Columns.Add("ID");
                            //    oBindTable.Columns.Add("DispName");

                            //    for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            //    {
                            //        DataRow oRow;
                            //        oRow = oBindTable.NewRow();
                            //        oRow[0] = oListControl.SelectedItems[_Counter].ID;
                            //        oRow[1] = oListControl.SelectedItems[_Counter].Description;
                            //        oBindTable.Rows.Add(oRow);
                            //    }

                            //    if (_dtRemovedItem != null && _dtRemovedItem.Rows.Count > 0)
                            //    {
                            //        for (int iRow = 0; iRow < oBindTable.Rows.Count; iRow++)
                            //        {
                            //            for (int jRow = _dtRemovedItem.Rows.Count - 1; jRow >= 0; jRow--)
                            //            {
                            //                if (_dtRemovedItem.Rows[jRow]["nUserID"].ToString() == oBindTable.Rows[iRow]["ID"].ToString())
                            //                {
                            //                    _dtRemovedItem.Rows.RemoveAt(jRow);
                            //                }
                            //            }
                            //        }
                            //    }

                            //    oComboBox.DataSource = oBindTable;
                            //    oComboBox.DisplayMember = "DispName";
                            //    oComboBox.ValueMember = "ID";
                            //}
                            DataTable dtUsers;
                            if (oListControl.SelectedItems.Count > 0)
                            {
                                DataTable oBindTable = new DataTable();
                                oBindTable.Columns.Add("ID", typeof(Int64));
                                oBindTable.Columns.Add("DispName", typeof(string));

                                for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                                {
                                    DataRow oRow;
                                    oRow = oBindTable.NewRow();
                                    oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                    oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                    oBindTable.Rows.Add(oRow);
                                }

                                if (_dtRemovedItem != null && _dtRemovedItem.Rows.Count > 0)
                                {
                                    for (int iRow = 0; iRow < oBindTable.Rows.Count; iRow++)
                                    {
                                        for (int jRow = _dtRemovedItem.Rows.Count - 1; jRow >= 0; jRow--)
                                        {
                                            if (_dtRemovedItem.Rows[jRow]["nUserID"].ToString() == oBindTable.Rows[iRow]["ID"].ToString())
                                            {
                                                _dtRemovedItem.Rows.RemoveAt(jRow);
                                            }
                                        }
                                    }
                                }

                                if (cmbUsers.DataSource != null && ((DataTable)cmbUsers.DataSource).Rows.Count > 0)
                                {
                                    dtUsers = (DataTable)cmbUsers.DataSource;
                                    if (oBindTable != null && oBindTable.Rows.Count > 0)
                                    {
                                        for (int iRow = 0; iRow < oBindTable.Rows.Count; iRow++)
                                        {
                                            for (int jRow = dtUsers.Rows.Count - 1; jRow >= 0; jRow--)
                                            {
                                                if (dtUsers.Rows[jRow]["ID"].ToString() == oBindTable.Rows[iRow]["ID"].ToString())
                                                {
                                                    dtUsers.Rows.RemoveAt(jRow);
                                                }
                                            }
                                        }
                                    }
                                    dtUsers.Merge(oBindTable, true, MissingSchemaAction.Ignore);
                                }
                                else
                                {
                                    oComboBox.DataSource = oBindTable;
                                    oComboBox.DisplayMember = "DispName";
                                    oComboBox.ValueMember = "ID";
                                }
                            }

                        }
                        break;
                    case gloListControl.gloListControlType.StmtDisplaySettings:
                        {
                            txtDefaultStmtDisplaySettings.Text = "";
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

                                txtDefaultStmtDisplaySettings.Tag = Convert.ToInt64(oBindTable.Rows[0]["ID"]);
                                txtDefaultStmtDisplaySettings.Text = Convert.ToString(oBindTable.Rows[0]["DispName"]);
                                StmtDisplaySettingsID = Convert.ToInt64(oBindTable.Rows[0]["ID"]);
                            }
                        }
                        break;
                    default:
                        {
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            { this.Width = 600; this.Height = 376; pnltlsStrip.Visible = true; }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
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
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            { this.Width = 600; this.Height = 376; pnltlsStrip.Visible = true; }
        }

        #endregion

        #endregion "Default Statement Display Settings "

        private void btnBrowseUsers_Click(object sender, EventArgs e)
        {
        //    _ControlType = cmbUsers.Name.ToString();

        //    try
        //    {
        //        this.Height = 500;
        //        this.Width = 675;
        //        pnltlsStrip.Visible = false;
        //        if (oListControl != null)
        //        {
        //            for (int i = this.Controls.Count - 1; i >= 0; i--)
        //            {
        //                if (this.Controls[i].Name == oListControl.Name)
        //                {
        //                    this.Controls.Remove(this.Controls[i]);
        //                    break;
        //                }
        //            }
        //        }
        //        oListControl = new gloListControl.gloListControl(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloListControl.gloListControlType.Users, true, this.Width);
        //        oListControl.IsBusinessCenterUsers = true;
        //        oListControl.InputBusinessCenterUsersTable = _dtRemovedItem;
        //        oListControl.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
        //        oListControl.ControlHeader = " Users ";

        //        _CurrentControlType = gloListControl.gloListControlType.Users;
        //        oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
        //        oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
        //        this.Controls.Add(oListControl);

        //        DataTable oFillTable = new DataTable();
        //        oFillTable = (DataTable)cmbUsers.DataSource;

        //        if (oFillTable != null && oFillTable.Rows.Count > 0)
        //        {
        //            for (int i = 0; i <= oFillTable.Rows.Count - 1; i++)
        //            {
        //                Int64 _TagValue = 0;
        //                if (Convert.ToString(cmbUsers.Tag) == "")
        //                {
        //                    _TagValue = 0;
        //                }
        //                else
        //                {
        //                    _TagValue = Convert.ToInt64(cmbUsers.Tag);
        //                }
        //                oListControl.SelectedItems.Add(Convert.ToInt64(oFillTable.Rows[i][0].ToString()), oFillTable.Rows[i][1].ToString(), _TagValue);
        //            }
        //        }

        //        if (oFillTable != null)
        //        {
        //            oFillTable = null;
        //        }

        //        oListControl.OpenControl();
        //        oListControl.Dock = DockStyle.Fill;
        //        oListControl.BringToFront();
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //        ex = null;
        //    }
        }

        private void btnClearUsers_Click(object sender, EventArgs e)
        {

        //    DataTable _dtUsers = GetUserList();//null; //

        //    for (int iRow = _dtUsers.Rows.Count - 1; iRow >= 0; iRow--)
        //    {
        //        if (_dtUsers.Rows[iRow]["sLoginName"].ToString() != "")
        //        {
        //            if (_dtRemovedItem != null && _dtRemovedItem.Rows.Count > 0)
        //            { }
        //            else
        //            { _dtRemovedItem = _dtUsers.Clone(); }

        //            _dtRemovedItem.Rows.Add(_dtUsers.Rows[iRow].ItemArray);
        //            _dtUsers.Rows.RemoveAt(iRow);
        //        }
        //        else
        //        { 
        //            _dtUsers.Rows.RemoveAt(iRow);
        //        }
        //    }

        //    if (_dtRemovedItem != null && _dtRemovedItem.Rows.Count > 0)
        //    {
        //        _dtRemovedItem = _dtRemovedItem.DefaultView.ToTable(true);
        //    }
            
        //    ClearCombo(cmbUsers);
       } 

        private void ClearCombo(ComboBox oComboBox)
        {
           // oComboBox.Items.Clear();
            oComboBox.DataSource = null;
            oComboBox.Items.Clear();
            oComboBox.Refresh();
        }

        public void Fill_AssociatedUserList()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt = new DataTable();
            string _strSQL = "";

            try
            {
                _strSQL = " SELECT User_MST.nUserID,nProviderID,sLoginName,sFirstName,sMiddleName,sLastName,sEmail,sExchangeLogin " +
                           "  FROM   BL_BusinessCenter_UsersAssociation " +
                           "         INNER JOIN dbo.User_MST ON BL_BusinessCenter_UsersAssociation.nUserID = dbo.User_MST.nUserID " +
                           "  WHERE BL_BusinessCenter_UsersAssociation.nBusinessCenterID =  " + _nBusinessCenterID +
                           "  ORDER BY sLoginName";

                oDB.Retrive_Query(_strSQL, out dt);

                DesignBusinessCenterGrid();               

                    if (dt.Rows.Count > 0 && dt != null)
                    {


                        int _RowIndex = 0;
                      //  C1BusinessCenter_UserAssociation.Clear();
                        C1BusinessCenter_UserAssociation.DataSource = null;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            _RowIndex = C1BusinessCenter_UserAssociation.Rows.Add().Index;

                            C1BusinessCenter_UserAssociation.SetData(_RowIndex, COL_USERID, Convert.ToInt64(dt.Rows[i]["nUserID"]));
                            C1BusinessCenter_UserAssociation.SetData(_RowIndex, COL_PROVIDERID,Convert.ToInt64(dt.Rows[i]["nProviderID"]));
                            C1BusinessCenter_UserAssociation.SetData(_RowIndex, COL_LOGIN_NAME,Convert.ToString( dt.Rows[i]["sLoginName"]));
                            C1BusinessCenter_UserAssociation.SetData(_RowIndex, COL_FIRST_NAME,Convert.ToString(dt.Rows[i]["sFirstName"]));
                            C1BusinessCenter_UserAssociation.SetData(_RowIndex, COL_MIDDLE_NAME, Convert.ToString(dt.Rows[i]["sMiddleName"]));
                            C1BusinessCenter_UserAssociation.SetData(_RowIndex, COL_LAST_NAME,Convert.ToString( dt.Rows[i]["sLastName"]));
                            C1BusinessCenter_UserAssociation.SetData(_RowIndex, COL_EMAIL_ID, Convert.ToString(dt.Rows[i]["sEmail"]));
                            C1BusinessCenter_UserAssociation.SetData(_RowIndex, COL_EXCHANGE_LOGIN, Convert.ToString(dt.Rows[i]["sExchangeLogin"]));


                        }
                }

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
              //  System.Windows.Forms.MessageBox.Show(DBErr.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        private void DesignBusinessCenterGrid()
        {
            C1BusinessCenter_UserAssociation.Rows.Count = 1;
            C1BusinessCenter_UserAssociation.Rows.Fixed = 1;
            C1BusinessCenter_UserAssociation.Cols.Count = COL_COUNT;

            C1BusinessCenter_UserAssociation.SetData(0, COL_USERID, "User ID");
            C1BusinessCenter_UserAssociation.SetData(0, COL_PROVIDERID, "Provider ID");
            C1BusinessCenter_UserAssociation.SetData(0, COL_LOGIN_NAME, "Login Name");
            C1BusinessCenter_UserAssociation.SetData(0, COL_FIRST_NAME, "First Name");
            C1BusinessCenter_UserAssociation.SetData(0, COL_MIDDLE_NAME, "Middle Name");
            C1BusinessCenter_UserAssociation.SetData(0, COL_LAST_NAME, "Last Name");
            C1BusinessCenter_UserAssociation.SetData(0, COL_EMAIL_ID, "Email ID");
            C1BusinessCenter_UserAssociation.SetData(0, COL_EXCHANGE_LOGIN, "Exchange Login");

            C1BusinessCenter_UserAssociation.Cols[COL_USERID].Visible = false;
            C1BusinessCenter_UserAssociation.Cols[COL_PROVIDERID].Visible = false;  
            C1BusinessCenter_UserAssociation.Cols[COL_EXCHANGE_LOGIN].Visible = false;

            int _Width = C1BusinessCenter_UserAssociation.Width-5;
            C1BusinessCenter_UserAssociation.Cols[COL_USERID].Width =0;
            C1BusinessCenter_UserAssociation.Cols[COL_PROVIDERID].Width = 0;
            C1BusinessCenter_UserAssociation.Cols[COL_LOGIN_NAME].Width = (int)(_Width * 0.2);
            C1BusinessCenter_UserAssociation.Cols[COL_FIRST_NAME].Width = (int)(_Width * 0.2);
            C1BusinessCenter_UserAssociation.Cols[COL_MIDDLE_NAME].Width = (int)(_Width * 0.2);
            C1BusinessCenter_UserAssociation.Cols[COL_LAST_NAME].Width = (int)(_Width * 0.2);
            C1BusinessCenter_UserAssociation.Cols[COL_EMAIL_ID].Width = (int)(_Width * 0.4);
            C1BusinessCenter_UserAssociation.Cols[COL_EXCHANGE_LOGIN].Width = 0;

            C1BusinessCenter_UserAssociation.Cols[COL_USERID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1BusinessCenter_UserAssociation.Cols[COL_PROVIDERID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1BusinessCenter_UserAssociation.Cols[COL_LOGIN_NAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1BusinessCenter_UserAssociation.Cols[COL_FIRST_NAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1BusinessCenter_UserAssociation.Cols[COL_MIDDLE_NAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1BusinessCenter_UserAssociation.Cols[COL_LAST_NAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1BusinessCenter_UserAssociation.Cols[COL_EMAIL_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1BusinessCenter_UserAssociation.Cols[COL_EXCHANGE_LOGIN].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        }

        private void C1BusinessCenter_UserAssociation_MouseMove(object sender, MouseEventArgs e)
        {
            if (C1BusinessCenter_UserAssociation.HitTest(e.X, e.Y).Column >= 14 && C1BusinessCenter_UserAssociation.HitTest(e.X, e.Y).Column <= 27)
            {                
                gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1FlexGrid)sender, e.Location, true);
            }
            else
            {
                gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
            }
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

        private void txtDescription_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                TextBox textbox = (TextBox)sender;

                if (txtDescription.Text.Trim() != "")
                {
                    if (getWidthofText(txtDescription.Text.Trim(), txtDescription) >= txtDescription.Width - 20)
                    {
                        toolTip1.SetToolTip(txtDescription, txtDescription.Text.Trim());
                    }
                    else
                    {
                        this.toolTip1.Hide(txtDescription);
                    }
                }
                else
                {
                    this.toolTip1.Hide(txtDescription);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

      

        private void txtDefaultStmtDisplaySettings_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                TextBox textbox = (TextBox)sender;

                if (txtDefaultStmtDisplaySettings.Text.Trim() != "")
                {
                    if (getWidthofText(txtDefaultStmtDisplaySettings.Text.Trim(), txtDefaultStmtDisplaySettings) >= txtDefaultStmtDisplaySettings.Width - 20)
                    {
                        toolTip1.SetToolTip(txtDefaultStmtDisplaySettings, txtDefaultStmtDisplaySettings.Text.Trim());
                    }
                    else
                    {
                        this.toolTip1.Hide(txtDefaultStmtDisplaySettings);
                    }
                }
                else
                {
                    this.toolTip1.Hide(txtDefaultStmtDisplaySettings);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }

        private void txtCode_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                TextBox textbox = (TextBox)sender;

                if (txtCode.Text.Trim() != "")
                {
                    if (getWidthofText(txtCode.Text.Trim(), txtCode) >= txtCode.Width - 20)
                    {
                        toolTip1.SetToolTip(txtCode, txtCode.Text.Trim());
                    }
                    else
                    {
                        this.toolTip1.Hide(txtCode);
                    }
                }
                else
                {
                    this.toolTip1.Hide(txtCode);
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