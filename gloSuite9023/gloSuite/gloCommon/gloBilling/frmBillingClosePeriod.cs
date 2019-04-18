using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmBillingClosedJournals : Form
    {
        

        #region " Variable Declarations"
        
        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        private Int64 _userid = 0;
        private string _username = "";
        private bool _isBatchTreeLoading = false;
        DataView dvClaims = new DataView();
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private string _TagClosePeriod = "ClosePeriod";
        private string _TagClosedClaims = "Closed Journals";

        private bool blnDisposed;
        private static frmBillingClosedJournals frm;

        #endregion

        
        #region "Constructors"

        public frmBillingClosedJournals(string DatabaseConnectionString, Int64 UserID, string UserName)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _userid = UserID;
            _username = UserName;
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion
        }


        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!(this.blnDisposed))
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if ((disposing))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
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

        ~frmBillingClosedJournals()
        {
            Dispose(false);
        }

        public static frmBillingClosedJournals GetInstance(string DatabaseConnectionString, Int64 UserID, string UserName)
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmBillingClosedJournals(DatabaseConnectionString, UserID, UserName);
                }
            }
            finally
            {

            }
            return frm;
        }


        #endregion


        #region "Form Events"

        private void frmBillingClosePeriod_Load(object sender, EventArgs e)
        {
            tsb_Select.Text = "Select All";
            tsb_Select.Tag = "Select";
            pnlProgressBar.Visible = false;
            tp_User.Text = _username;

            //Added By Pramod Nair For Saving the Settings on 20090811
            LoadLastSavedSetting();

            FillCloseDayTray();
        }

        private void frmBillingClosedJournals_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }


        #endregion


        #region "Tool Bar Events"

        private void tsb_Select_Click(object sender, EventArgs e)
        {
            ChargesSelection();
        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_Modify_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1ClaimGrid != null && c1ClaimGrid.Rows.Count > 1)
                {
                    if (c1ClaimGrid.RowSel > 0)
                    {
                        Int64 _PatientID = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["nPatientID"].Index));
                        Int64 _PaymentId = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["nPaymentTransactionID"].Index));
                        Int64 _ClaimNo = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["nClaimNo"].Index));
                        Int64 _MultiplePaymentNo = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["MultiPaymentTransactionID"].Index));

                        //frmBillingPayment ofrmBillingPayment = frmBillingPayment.GetInstance(_databaseconnectionstring, _PatientID, _PaymentId, _ClaimNo, _MultiplePaymentNo, true);
                        //ofrmBillingPayment.WindowState = FormWindowState.Maximized;
                        ////ofrmBillingPayment.MdiParent = this.Parent;
                        //ofrmBillingPayment.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void tabClosePeriodTray_Selected(object sender, TabControlEventArgs e)
        {
            SetView();
        }

        private void btnClosedClaims_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            string _strSQLQuery = "";
            int _SelectedPayments = 0;
            try
            {
                C1.Win.C1FlexGrid.C1FlexGrid ClaimFillGrid = null;

                #region "Find respective control values as per claim type"
                if (tabClosePeriodTray.SelectedTab.Tag.ToString().ToUpper() == _TagClosePeriod.ToUpper())
                {
                    ClaimFillGrid = c1ClaimGrid;
                }
                #endregion


                if (ClaimFillGrid != null && ClaimFillGrid.Rows.Count > 0)
                {
                    for (int i = 1; i < ClaimFillGrid.Rows.Count; i++)
                    {
                        if (ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["Select"].Index) != null && ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["Select"].Index).ToString() != "")
                        {
                            if (ClaimFillGrid.GetCellCheck(i, ClaimFillGrid.Cols["Select"].Index) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                            {
                                _SelectedPayments = _SelectedPayments + 1;
                            }
                        }
                    }

                    if (_SelectedPayments > 0)
                    {
                        pnlProgressBar.Visible = true;
                        prgPaymentClosing.Minimum = 1;
                        prgPaymentClosing.Maximum = _SelectedPayments;

                        for (int i = 1; i < ClaimFillGrid.Rows.Count; i++)
                        {

                            if (ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["Select"].Index) != null && ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["Select"].Index).ToString() != "")
                            {
                                if (ClaimFillGrid.GetCellCheck(i, ClaimFillGrid.Cols["Select"].Index) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                {
                                    if (ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nPaymentTransactionID"].Index) != null && ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nPaymentTransactionID"].Index).ToString() != "" &&
                                        ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nClaimNo"].Index) != null && ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nClaimNo"].Index).ToString() != "" &&
                                        ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nPatientID"].Index) != null && ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nPatientID"].Index).ToString() != ""
                                        )
                                    {
                                        Int64 _ClosePaymentTransactionID = 0;
                                        Int64 _ClosePaymentClaimNo = 0;
                                        Int64 _ClosePatientID = 0;

                                        _ClosePaymentTransactionID = Convert.ToInt64(ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nPaymentTransactionID"].Index).ToString());
                                        _ClosePaymentClaimNo = Convert.ToInt64(ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nClaimNo"].Index).ToString());
                                        _ClosePatientID = Convert.ToInt64(ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nPatientID"].Index).ToString());

                                        if (_ClosePaymentTransactionID > 0 && _ClosePaymentClaimNo > 0 && _ClosePatientID > 0)
                                        {
                                            _strSQLQuery = "UPDATE BL_Transaction_Payment_MST SET nIsTrayClose = " + PaymentCloseTrayStatus.Closed.GetHashCode() + " " +
                                                " WHERE nPaymentTransactionID = " + _ClosePaymentTransactionID + " AND nClaimNo = " + _ClosePaymentClaimNo + " AND nPatientID = " + _ClosePatientID + " ";
                                            oDB.Execute_Query(_strSQLQuery);
                                            _strSQLQuery = "UPDATE BL_Transaction_Payment_DTL SET nIsTrayClose = " + PaymentCloseTrayStatus.Closed.GetHashCode() + " " +
                                                " WHERE nPaymentTransactionID = " + _ClosePaymentTransactionID + " AND nClaimNo = " + _ClosePaymentClaimNo + " AND nPatientID = " + _ClosePatientID + " ";
                                            oDB.Execute_Query(_strSQLQuery);
                                            prgPaymentClosing.Increment(1);
                                        }
                                    }
                                }
                            }
                        }

                        FillClaimsOnFindingCriteria(_TagClosedClaims, false);
                        FillCloseDayTray();

                    }
                    else
                    {
                        MessageBox.Show("Please select claims to close Journals.", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
                pnlProgressBar.Visible = false;
            }
        }

        private void btnCloseJournals_Click(object sender, EventArgs e)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            string _strSQLQuery = "";
            string _strUpdateQuery = "";
            
            try
            {
                C1.Win.C1FlexGrid.C1FlexGrid ClaimFillGrid = null;

                #region "Find respective control values as per claim type"
                if (tabClosePeriodTray.SelectedTab.Tag.ToString().ToUpper() == _TagClosePeriod.ToUpper())
                {
                    ClaimFillGrid = c1ClaimGrid;
                }
                #endregion

                for (int i = 1; i < ClaimFillGrid.Rows.Count; i++)
                {

                    if (ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["Select"].Index) != null && ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["Select"].Index).ToString() != "")
                    {
                        
                            if (ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nPaymentTransactionID"].Index) != null && ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nPaymentTransactionID"].Index).ToString() != "" &&
                                ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nClaimNo"].Index) != null && ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nClaimNo"].Index).ToString() != "" &&
                                ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nPatientID"].Index) != null && ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nPatientID"].Index).ToString() != ""
                                )
                            {
                                Int64 _ClosePaymentTransactionID = 0;
                                Int64 _ClosePaymentClaimNo = 0;
                                Int64 _ClosePatientID = 0;

                                _ClosePaymentTransactionID = Convert.ToInt64(ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nPaymentTransactionID"].Index).ToString());
                                _ClosePaymentClaimNo = Convert.ToInt64(ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nClaimNo"].Index).ToString());
                                _ClosePatientID = Convert.ToInt64(ClaimFillGrid.GetData(i, ClaimFillGrid.Cols["nPatientID"].Index).ToString());

                                if (_ClosePaymentTransactionID > 0 && _ClosePaymentClaimNo > 0 && _ClosePatientID > 0)
                                {
                                    _strSQLQuery = "UPDATE BL_Transaction_Payment_MST SET nIsTrayClose = " + PaymentCloseTrayStatus.Closed.GetHashCode() + " " +
                                        " WHERE nPaymentTransactionID = " + _ClosePaymentTransactionID + " AND nClaimNo = " + _ClosePaymentClaimNo + " AND nPatientID = " + _ClosePatientID + " ";
                                    oDB.Execute_Query(_strSQLQuery);
                                    _strSQLQuery = "UPDATE BL_Transaction_Payment_DTL SET nIsTrayClose = " + PaymentCloseTrayStatus.Closed.GetHashCode() + " " +
                                        " WHERE nPaymentTransactionID = " + _ClosePaymentTransactionID + " AND nClaimNo = " + _ClosePaymentClaimNo + " AND nPatientID = " + _ClosePatientID + " ";
                                    oDB.Execute_Query(_strSQLQuery);
                                    prgPaymentClosing.Increment(1);
                                }
                            }
                        
                    }
                }
                if (trvClosePeriod.SelectedNode != null)
                {
                    _strUpdateQuery = "Update BL_CloseDayTray set bIsClosed=1 Where nCloseDayTrayID=" + Convert.ToInt64(trvClosePeriod.SelectedNode.Tag.ToString()) + " ";
                    oDB.Execute_Query(_strUpdateQuery);
                }
                FillClaimsOnFindingCriteria(_TagClosePeriod, false);
                FillCloseDayTray();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
           

        }

        #endregion


        #region "Fill Methods"

        private void FillCloseDayTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable oDataTable = new DataTable();
            _isBatchTreeLoading = true;

            try
            {
                trvClosePeriod.Nodes.Clear();
                trvClosedClaims.Nodes.Clear();
                oDB.Connect(false);
                bool IsAdmin = false;
                IsAdmin = GetAdmin(_userid);
                if (IsAdmin == true)
                {
                    oDB.Retrive_Query("SELECT nCloseDayTrayID, sCode, sDescription, nUserID, nNumberOfDays, nClinicID FROM BL_CloseDayTray WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 AND sDescription <> '' ", out oDataTable);
                }
                else
                {
                    oDB.Retrive_Query("SELECT nCloseDayTrayID, sCode, sDescription, nUserID, nNumberOfDays, nClinicID FROM BL_CloseDayTray WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 AND sDescription <> '' and nUserID='" + _userid + "'", out oDataTable);
                }
                if (oDataTable != null && oDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                    {
                        TreeNode oNode = new TreeNode();
                        if (IsAdmin == true)
                        {
                            oNode.Text = oDataTable.Rows[i]["sCode"].ToString() + " - " + oDataTable.Rows[i]["sDescription"].ToString();
                            oNode.Tag = oDataTable.Rows[i]["nCloseDayTrayID"].ToString();
                        }
                        else
                        {
                            oNode.Text = oDataTable.Rows[i]["sDescription"].ToString();
                            oNode.Tag = oDataTable.Rows[i]["nCloseDayTrayID"].ToString();
                        }
                        //oNode.Tag = oDataTable.Rows[i]["nCloseDayTrayID"].ToString();
                        oNode.ImageIndex = 0;
                        oNode.SelectedImageIndex = 0;
                        trvClosePeriod.Nodes.Add(oNode);
                        if (oNode != null) { oNode = null; }

                        TreeNode oNodeClosed = new TreeNode();
                        if (IsAdmin == true)
                        {
                            oNodeClosed.Text = oDataTable.Rows[i]["sCode"].ToString()+" - "+oDataTable.Rows[i]["sDescription"].ToString();
                            oNodeClosed.Tag = oDataTable.Rows[i]["nCloseDayTrayID"].ToString();
                        }
                        else
                        {
                            oNodeClosed.Text = oDataTable.Rows[i]["sDescription"].ToString();
                            oNodeClosed.Tag = oDataTable.Rows[i]["nCloseDayTrayID"].ToString();
                        }
                        oNodeClosed.ImageIndex = 0;
                        oNodeClosed.SelectedImageIndex = 0;
                        trvClosedClaims.Nodes.Add(oNodeClosed);
                        if (oNodeClosed != null) { oNodeClosed = null; }
                        
                    }
                }
                oDB.Disconnect();
                _isBatchTreeLoading = false;

                if (trvClosePeriod.Nodes.Count > 0) { trvClosePeriod.SelectedNode = trvClosePeriod.Nodes[0]; }

                if (trvClosedClaims.Nodes.Count > 0) { trvClosedClaims.SelectedNode = trvClosedClaims.Nodes[0]; }
            }
            catch (Exception ex)
            {
                _isBatchTreeLoading = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oDataTable != null) { oDataTable.Dispose(); }
            }
        }

        private bool GetAdmin(Int64 _PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable oDataTable = new DataTable();
            bool result = false;
            oDB.Connect(false);
            oDB.Retrive_Query("Select nAdministrator from User_MST where nUserID='" + _userid + "' and nAdministrator=1", out oDataTable);
            if (oDataTable != null)
            {
                if (oDataTable.Rows.Count > 0)
                {
                    result = true;
                }
            }
            oDataTable.Dispose();
            oDB.Dispose();
            return result;


        }

        #region "Fill Claims "

        private void FillClaimsOnFindingCriteria(string ClaimType, bool IsSearching)
        {
            try
            {
                if (_isBatchTreeLoading == false)
                {
                    string _ClaimFill_SearchText = "";
                    Int64 _ClaimFill_CloseDayTrayID = 0;
                    decimal _ClaimFill_NoOfRecord = 0;
                    bool _ClaimFill_NoOfRecordApplicable = false;
                    bool _ClaimFill_ShowAllinBatch = false;
                    C1.Win.C1FlexGrid.C1FlexGrid oFillGrid = null;
                    int SelectIsClosed = 0;

                    #region "Find respective control values as per claim type"
                    if (ClaimType.ToUpper() == _TagClosePeriod.ToUpper())
                    {
                        #region "Batch"
                        //Search Text
                        if (IsSearching == true) { _ClaimFill_SearchText = txtCloseDayTraySearch.Text.Trim(); }

                        //Batch ID
                        if (trvClosePeriod.SelectedNode != null && trvClosePeriod.SelectedNode.Tag != null && trvClosePeriod.SelectedNode.Tag.ToString().Length > 0)
                        {
                            _ClaimFill_CloseDayTrayID = Convert.ToInt64(trvClosePeriod.SelectedNode.Tag.ToString());
                        }

                        //Show all in selected batch
                        //_ClaimFill_ShowAllinBatch = chkCloseDayTrayClaimCount.Checked;

                        //No of record count
                        _ClaimFill_NoOfRecord = numCloseDayTrayClaimCount.Value;


                        //Added By Pramod Nair For Fetching the settings For No Of Records to be Displayed
                        _ClaimFill_ShowAllinBatch = chkCloseDayTrayClaimCount.Checked;

                        if (_ClaimFill_NoOfRecord > 0)
                        {
                            _ClaimFill_NoOfRecordApplicable = true;
                        }

                        //if (_ClaimFill_CloseDayTrayID <= 0) 
                        //{
                        //    _ClaimFill_NoOfRecordApplicable = true; 
                        //}

                        SelectIsClosed = 0;
                        //Grid
                        oFillGrid = c1ClaimGrid;
                        #endregion
                    }
                    else if (ClaimType.ToUpper() == _TagClosedClaims.ToUpper())
                    {
                        #region "Batch"
                        //Search Text
                        if (IsSearching == true) { _ClaimFill_SearchText = txtClosedClaimSearch.Text.Trim(); }

                        //Batch ID
                        if (trvClosedClaims.SelectedNode != null && trvClosedClaims.SelectedNode.Tag != null && trvClosedClaims.SelectedNode.Tag.ToString().Length > 0)
                        {
                            _ClaimFill_CloseDayTrayID = Convert.ToInt64(trvClosedClaims.SelectedNode.Tag.ToString());
                        }

                        //Show all in selected batch
                        //_ClaimFill_ShowAllinBatch = chkClosedClaimsCount.Checked;

                        //No of record count
                        _ClaimFill_NoOfRecord = numClosedClaimsCount.Value;

                        //Added By Pramod Nair For Fetching the settings For No Of Records to be Displayed
                        _ClaimFill_ShowAllinBatch = chkClosedClaimsCount.Checked;

                        if (_ClaimFill_NoOfRecord > 0)
                        {
                            _ClaimFill_NoOfRecordApplicable = true;
                        }

                        //End

                        //_ClaimFill_NoOfRecordApplicable = false;
                        //if (_ClaimFill_CloseDayTrayID <= 0) { _ClaimFill_NoOfRecordApplicable = true; }

                        //Grid
                        oFillGrid = c1ClosedClaimGrid;

                        SelectIsClosed = 1;
                        #endregion
                    }
                    #endregion

                    if (oFillGrid != null)
                    {
                        FillClaims(ClaimType, _ClaimFill_SearchText, _ClaimFill_CloseDayTrayID, _ClaimFill_NoOfRecord, _ClaimFill_NoOfRecordApplicable, _ClaimFill_ShowAllinBatch, oFillGrid, IsSearching, _userid, _username, SelectIsClosed);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void FillClaims(string ClaimType, string SearchText, Int64 CloseDayTrayID, decimal NoOfClaims, bool NoOfClaimsApplicable, bool ShowAll, C1.Win.C1FlexGrid.C1FlexGrid ClaimFillGrid, bool IsSearching, Int64 UserID, string UserName, int SelectIsClosed)
        {
            gloClaimManager ogloClaimManager = new gloClaimManager(_databaseconnectionstring, "");
            DataTable dtClaims = null;

            try
            {
                SearchText = SearchText.Replace("'", "''");
                if (ClaimFillGrid != null)
                {
                    //ClaimFillGrid.Clear();
                    ClaimFillGrid.DataSource = null;


                    //dtClaims = ogloClaimManager.GetBatchClaims(ClaimType, SearchText, BatchID, Convert.ToInt32(NoOfClaims), NoOfClaimsApplicable, ShowAll, IsSearching);

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                    oDBParameters.Add("@SearchText", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@CloseDayTrayID", CloseDayTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@NoOfClaims", NoOfClaims, ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@NoOfClaimsApplicable", NoOfClaimsApplicable, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@ShowAll", ShowAll, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@IsSearching", IsSearching, ParameterDirection.Input, SqlDbType.Bit);
                    oDBParameters.Add("@UserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@UserName", UserName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@SelectIsClosed", SelectIsClosed, ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Connect(false);

                    //oDB.Retrive("BL_SELECT_PaymentClaims_Old", oDBParameters, out  dtClaims);
                    oDB.Retrive("BL_SELECT_PaymentClaims", oDBParameters, out  dtClaims);
                    oDB.Disconnect();
                    if (oDB != null) { oDB.Dispose(); };
                    if (oDBParameters != null) { oDBParameters.Dispose(); };

                    //********************
                    if (dtClaims != null)
                    {
                        if (dtClaims.Rows.Count <= 0)
                        {
                            if (dvClaims != null && dvClaims.Table != null) { dvClaims.AddNew(); }
                            ClaimFillGrid.Rows.Count = 1;
                            ClaimFillGrid.Rows.Fixed = 1;
                        }

                        dvClaims = dtClaims.DefaultView;
                        ClaimFillGrid.DataSource = dvClaims;

                        #region " Show Hide Column "
                        if (SelectIsClosed == 1)
                        {
                            ClaimFillGrid.Cols["Select"].Visible = false;
                        }
                        else
                        {
                            ClaimFillGrid.Cols["Select"].Visible = true;
                        }

                        ClaimFillGrid.Cols["nPaymentTransactionID"].Visible = false;
                        ClaimFillGrid.Cols["nPatientID"].Visible = false;
                        ClaimFillGrid.Cols["nBillingTransactionID"].Visible = false;
                        ClaimFillGrid.Cols["nPaymentDate"].Visible = false;
                        ClaimFillGrid.Cols["nClaimNo"].Visible = false;
                        ClaimFillGrid.Cols["nPaymentNo"].Visible = false;
                        ClaimFillGrid.Cols["Date"].Visible = true;
                        ClaimFillGrid.Cols["Claim"].Visible = true;
                        ClaimFillGrid.Cols["Payment"].Visible = true;
                        ClaimFillGrid.Cols["PatientCode"].Visible = true;
                        ClaimFillGrid.Cols["PatientFirstName"].Visible = true;
                        ClaimFillGrid.Cols["PatientMIName"].Visible = true;
                        ClaimFillGrid.Cols["PatientLastName"].Visible = true;
                        ClaimFillGrid.Cols["PatientSSN"].Visible = true;
                        ClaimFillGrid.Cols["PaymentInsuranceID"].Visible = false;
                        ClaimFillGrid.Cols["SubscriberID"].Visible = true;
                        ClaimFillGrid.Cols["PaymentInsuranceName"].Visible = true;
                        ClaimFillGrid.Cols["FacilityCode"].Visible = false;
                        ClaimFillGrid.Cols["FacilityDescription"].Visible = true;
                        ClaimFillGrid.Cols["ProviderID"].Visible = false;
                        ClaimFillGrid.Cols["ProviderFirstName"].Visible = true;
                        ClaimFillGrid.Cols["ProviderMiddleName"].Visible = true;
                        ClaimFillGrid.Cols["ProviderLastName"].Visible = true;
                        ClaimFillGrid.Cols["TotalPayment"].Visible = true;
                        ClaimFillGrid.Cols["CloseTrayID"].Visible = false;
                        ClaimFillGrid.Cols["MultiPaymentTransactionID"].Visible = false;
                        #endregion

                        #region " Set Width of Column "
                        if (SelectIsClosed == 1)
                        {
                            ClaimFillGrid.Cols["Select"].Width = 0;
                        }
                        else
                        {
                            ClaimFillGrid.Cols["Select"].Width = 50;
                        }
                        ClaimFillGrid.Cols["nPaymentTransactionID"].Width = 0;
                        ClaimFillGrid.Cols["nPatientID"].Width = 0;
                        ClaimFillGrid.Cols["nBillingTransactionID"].Width = 0;
                        ClaimFillGrid.Cols["nPaymentDate"].Width = 0;
                        ClaimFillGrid.Cols["nClaimNo"].Width = 0;
                        ClaimFillGrid.Cols["nPaymentNo"].Width = 0;
                        ClaimFillGrid.Cols["Date"].Width = 80;
                        ClaimFillGrid.Cols["Claim"].Width = 100;
                        ClaimFillGrid.Cols["Payment"].Width = 100;
                        ClaimFillGrid.Cols["PatientCode"].Width = 100;
                        ClaimFillGrid.Cols["PatientFirstName"].Width = 100;
                        ClaimFillGrid.Cols["PatientMIName"].Width = 50;
                        ClaimFillGrid.Cols["PatientLastName"].Width = 100;
                        ClaimFillGrid.Cols["PatientSSN"].Width = 100;
                        ClaimFillGrid.Cols["PaymentInsuranceID"].Width = 0;
                        ClaimFillGrid.Cols["SubscriberID"].Width = 100;
                        ClaimFillGrid.Cols["PaymentInsuranceName"].Width = 300;
                        ClaimFillGrid.Cols["FacilityCode"].Width = 100;
                        ClaimFillGrid.Cols["FacilityDescription"].Width = 100;
                        ClaimFillGrid.Cols["ProviderID"].Width = 0;
                        ClaimFillGrid.Cols["ProviderFirstName"].Width = 100;
                        ClaimFillGrid.Cols["ProviderMiddleName"].Width = 100;
                        ClaimFillGrid.Cols["ProviderLastName"].Width = 100;
                        ClaimFillGrid.Cols["TotalPayment"].Width = 100;
                        ClaimFillGrid.Cols["CloseTrayID"].Width = 0;
                        ClaimFillGrid.Cols["MultiPaymentTransactionID"].Width = 0;
                        #endregion

                        #region " Set Header "
                        ClaimFillGrid.Cols["Select"].Caption = "Select";
                        ClaimFillGrid.Cols["nPaymentTransactionID"].Caption = "PaymentTransactionID";
                        ClaimFillGrid.Cols["nPatientID"].Caption = "PatientID";
                        ClaimFillGrid.Cols["nBillingTransactionID"].Caption = "BillingTransactionID";
                        ClaimFillGrid.Cols["nPaymentDate"].Caption = "PaymentDate";
                        ClaimFillGrid.Cols["nClaimNo"].Caption = "ClaimNo";
                        ClaimFillGrid.Cols["nPaymentNo"].Caption = "PaymentNo";
                        ClaimFillGrid.Cols["Date"].Caption = "Date";
                        ClaimFillGrid.Cols["Claim"].Caption = "Claim No";
                        ClaimFillGrid.Cols["Payment"].Caption = "Payment No";
                        ClaimFillGrid.Cols["PatientCode"].Caption = "Patient Code";
                        ClaimFillGrid.Cols["PatientFirstName"].Caption = "First";
                        ClaimFillGrid.Cols["PatientMIName"].Caption = "MI";
                        ClaimFillGrid.Cols["PatientLastName"].Caption = "Last";
                        ClaimFillGrid.Cols["PatientSSN"].Caption = "SSN";
                        ClaimFillGrid.Cols["PaymentInsuranceID"].Caption = "Payment Insurance ID";
                        ClaimFillGrid.Cols["SubscriberID"].Caption = "Insurance ID";
                        ClaimFillGrid.Cols["PaymentInsuranceName"].Caption = "Insurance Name";
                        ClaimFillGrid.Cols["FacilityCode"].Caption = "Facility Code";
                        ClaimFillGrid.Cols["FacilityDescription"].Caption = "Facility";
                        ClaimFillGrid.Cols["ProviderID"].Caption = "ProviderID";
                        ClaimFillGrid.Cols["ProviderFirstName"].Caption = "Provider First";
                        ClaimFillGrid.Cols["ProviderMiddleName"].Caption = "MI";
                        ClaimFillGrid.Cols["ProviderLastName"].Caption = "Last";
                        ClaimFillGrid.Cols["TotalPayment"].Caption = "Amount";
                        ClaimFillGrid.Cols["CloseTrayID"].Caption = "CloseTrayID";
                        ClaimFillGrid.Cols["MultiPaymentTransactionID"].Caption = "MultiPaymentTransactionID";
                        #endregion

                    }

                    C1.Win.C1FlexGrid.CellStyle csCurrency;// = ClaimFillGrid.Styles.Add("csCurrencyCell");
                    try
                    {
                        if (ClaimFillGrid.Styles.Contains("csCurrencyCell"))
                        {
                            csCurrency = ClaimFillGrid.Styles["csCurrencyCell"];
                        }
                        else
                        {
                            csCurrency = ClaimFillGrid.Styles.Add("csCurrencyCell");
                            csCurrency.DataType = typeof(System.Decimal);
                            csCurrency.Format = "c";
                            csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL; //new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                        }

                    }
                    catch
                    {
                        csCurrency = ClaimFillGrid.Styles.Add("csCurrencyCell");
                        csCurrency.DataType = typeof(System.Decimal);
                        csCurrency.Format = "c";
                        csCurrency.Font = gloGlobal.clsgloFont.gFont_SMALL; //new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    }
                     ClaimFillGrid.Cols["TotalPayment"].Style = csCurrency;


                    ClaimFillGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                    ClaimFillGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                    ClaimFillGrid.AutoResize = false;


                    for (int i = 0; i <= ClaimFillGrid.Cols.Count - 1; i++)
                    {
                        ClaimFillGrid.Cols[i].AllowEditing = false;
                    }
                    ClaimFillGrid.Cols["Select"].AllowEditing = true;

                    if (c1ClaimGrid.Rows.Count > 0 && c1ClaimGrid.RowSel > 0)
                    {
                        if (c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["CloseTrayID"].Index) != null && c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["CloseTrayID"].Index).ToString() != null && c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["CloseTrayID"].Index).ToString().Trim() != "")
                        {
                            Int64 _FillCloseDayTrayID = 0;
                            _FillCloseDayTrayID = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["CloseTrayID"].Index).ToString());

                            if (_FillCloseDayTrayID > 0)
                            {
                                if (trvClosePeriod.Nodes.Count > 0)
                                {
                                    for (int i = 0; i <= trvClosePeriod.Nodes.Count - 1; i++)
                                    {
                                        if (trvClosePeriod.Nodes[i].Tag != null && trvClosePeriod.Nodes[i].Tag.ToString().Trim() != "")
                                        {
                                            if (Convert.ToInt64(trvClosePeriod.Nodes[i].Tag.ToString()) == _FillCloseDayTrayID)
                                            {
                                                trvClosePeriod.SelectedNode = trvClosePeriod.Nodes[i];
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
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
                if (ogloClaimManager != null) { ogloClaimManager.Dispose(); }
            }

        }

        #endregion

        private void ChargesSelection()
        {
            C1.Win.C1FlexGrid.CheckEnum oStatus = C1.Win.C1FlexGrid.CheckEnum.None;
            try
            {
                if (tsb_Select.Tag.ToString() == "Select")
                {
                    tsb_Select.Text = "DeSelect All";
                    tsb_Select.Tag = "Deselect";
                    oStatus = C1.Win.C1FlexGrid.CheckEnum.Checked;
                }
                else if (tsb_Select.Tag.ToString() == "Deselect")
                {
                    tsb_Select.Text = "Select All";
                    tsb_Select.Tag = "Select";
                    oStatus = C1.Win.C1FlexGrid.CheckEnum.Unchecked;
                }

                C1.Win.C1FlexGrid.C1FlexGrid ClaimFillGrid = null;

                #region "Find respective control values as per claim type"
                if (tabClosePeriodTray.SelectedTab.Tag.ToString().ToUpper() == _TagClosePeriod.ToUpper())
                {
                    ClaimFillGrid = c1ClaimGrid;
                }
                #endregion

                if (Convert.ToString(tabClosePeriodTray.SelectedTab.Tag) == _TagClosePeriod)
                {
                    if (ClaimFillGrid != null && ClaimFillGrid.Rows.Count > 0)
                    {
                        for (int i = 0; i < ClaimFillGrid.Rows.Count; i++)
                        {
                            ClaimFillGrid.SetCellCheck(i + 1, 0, oStatus);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                //MessageBox.Show("ERROR : " +ex.Message
            }
        }

        private void SetView()
        {
            try
            {
                tsb_Select.Text = "Select All";
                tsb_Select.Tag = "Select";

                if (Convert.ToString(tabClosePeriodTray.SelectedTab.Tag).ToUpper() == _TagClosePeriod.ToUpper())
                {
                    #region " Show/Hide Buttons "

                    FillClaimsOnFindingCriteria(_TagClosePeriod, false);

                    tsb_Modify.Visible = true;
                    tsb_Select.Visible = true;
                    btnClose.Visible = true;
                    btnClosedJournals.Visible = true;
                    btnClosedClaims.Visible = true;

                    #endregion " Show/Hide Buttons "
                }
                else if (Convert.ToString(tabClosePeriodTray.SelectedTab.Tag).ToUpper() == _TagClosedClaims.ToUpper())
                {
                    #region " Show/Hide Buttons "

                    FillClaimsOnFindingCriteria(_TagClosedClaims, false);

                    tsb_Modify.Visible = false;
                    tsb_Select.Visible = false;
                    btnClose.Visible = true;
                    btnClosedJournals.Visible = false;
                    btnClosedClaims.Visible = false;
                    #endregion " Show/Hide Buttons "
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        #endregion

       
        #region " Tree View & Numeric & Search Event "

        private void trvClosePeriod_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (((TreeView)sender).Tag != null && ((TreeView)sender).Tag.ToString().Length > 0)
            {
                #region "Control Enable/Disable"

                TreeView oTreeView = (TreeView)sender;
              //  bool _IsAllSelected = false;

                if (oTreeView.SelectedNode.Tag != null && oTreeView.SelectedNode.Tag.ToString() != null && oTreeView.SelectedNode.Tag.ToString().Trim().Length > 0)
                {
                    
                    //if (oTreeView.Tag.ToString().ToUpper() == _TagClosePeriod.ToUpper())
                    //{
                    //    if (_IsAllSelected == true) { chkCloseDayTrayClaimCount.Enabled = false; chkCloseDayTrayClaimCount.Checked = false; } else { chkCloseDayTrayClaimCount.Enabled = true; }
                    //}
                    //else if (oTreeView.Tag.ToString().ToUpper() == _TagClosedClaims.ToUpper())
                    //{
                    //    if (_IsAllSelected == true) { chkClosedClaimsCount.Enabled = false; chkClosedClaimsCount.Checked = false; } else { chkClosedClaimsCount.Enabled = true; }
                    //}
                }

                #endregion

                FillClaimsOnFindingCriteria(((TreeView)sender).Tag.ToString(), false);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Tag != null && ((TextBox)sender).Tag.ToString().Length > 0)
            {
                FillClaimsOnFindingCriteria(((TextBox)sender).Tag.ToString(), true);

                tsb_Select.Text = "Select All";
                tsb_Select.Tag = "Select";
            }
        }

        private void numClaimCount_ValueChanged(object sender, EventArgs e)
        {
            if (((NumericUpDown)sender).Tag != null && ((NumericUpDown)sender).Tag.ToString().Length > 0)
            {
                FillClaimsOnFindingCriteria(((NumericUpDown)sender).Tag.ToString(), false);
            }
        }

        private void chkClaimCount_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Tag != null && ((CheckBox)sender).Tag.ToString().Length > 0)
            {
                #region "Control Enable/Disable"

                //CheckBox oCheckBox = (CheckBox)sender;

                //if (oCheckBox.Tag.ToString().ToUpper() == _TagCharges.ToUpper())
                //{
                //    if (oCheckBox.Checked == true) { numChargesClaimCount.Enabled = false; } else { numChargesClaimCount.Enabled = true; }
                //}
                //else if (oCheckBox.Tag.ToString().ToUpper() == _TagQueue.ToUpper())
                //{
                //    if (oCheckBox.Checked == true) { numQueueClaimCount.Enabled = false; } else { numQueueClaimCount.Enabled = true; }
                //}
                //else if (oCheckBox.Tag.ToString().ToUpper() == _TagBatch.ToUpper())
                //{
                //    if (oCheckBox.Checked == true) { numBatchClaimCount.Enabled = false; } else { numBatchClaimCount.Enabled = true; }
                //}
                //else if (oCheckBox.Tag.ToString().ToUpper() == _TagAccepted.ToUpper())
                //{
                //    if (oCheckBox.Checked == true) { numAcceptedClaimCount.Enabled = false; } else { numAcceptedClaimCount.Enabled = true; }
                //}
                //else if (oCheckBox.Tag.ToString().ToUpper() == _TagRejected.ToUpper())
                //{
                //    if (oCheckBox.Checked == true) { numRejectedClaimCount.Enabled = false; } else { numRejectedClaimCount.Enabled = true; }
                //}
                //else if (oCheckBox.Tag.ToString().ToUpper() == _TagFinished.ToUpper())
                //{
                //    if (oCheckBox.Checked == true) { numFinishClaimCount.Enabled = false; } else { numFinishClaimCount.Enabled = true; }
                //}
                #endregion

                
                FillClaimsOnFindingCriteria(((CheckBox)sender).Tag.ToString(), false);
            }
        }

        #endregion


        #region "Grid Events"

        private void c1ClaimGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                       C1.Win.C1FlexGrid.HitTestInfo hitInfo = c1ClaimGrid.HitTest(e.X, e.Y);
                       if (hitInfo.Row > 0)
                       {
                           Int64 _PatientID = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["nPatientID"].Index));
                           Int64 _PaymentId = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["nPaymentTransactionID"].Index));
                           Int64 _ClaimNo = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["nClaimNo"].Index));
                           Int64 _MultiplePaymentNo = Convert.ToInt64(c1ClaimGrid.GetData(c1ClaimGrid.RowSel, c1ClaimGrid.Cols["MultiPaymentTransactionID"].Index));

                           //frmBillingPayment ofrmBillingPayment = frmBillingPayment.GetInstance(_databaseconnectionstring, _PatientID, _PaymentId, _ClaimNo, _MultiplePaymentNo, true);
                           //ofrmBillingPayment.WindowState = FormWindowState.Maximized;
                           ////ofrmBillingPayment.MdiParent = this.Parent;
                           //ofrmBillingPayment.ShowDialog(this);

                          //Added  By Pramod Nair To Relaod the Grid
                          FillClaimsOnFindingCriteria("ClosePeriod", false);
                       }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }


        #endregion
        
        
        #region "For Saving the Settings"  

        private void frmBillingClosedJournals_FormClosing(object sender, FormClosingEventArgs e)
        {
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            try
            {

                oSettings.WriteSettings_XML("ClosedJournals", numCloseDayTrayClaimCount.Name.ToString(), numCloseDayTrayClaimCount.Value.ToString());
                oSettings.WriteSettings_XML("ClosedJournals", chkCloseDayTrayClaimCount.Name.ToString(), chkCloseDayTrayClaimCount.Checked.ToString());

                oSettings.WriteSettings_XML("ClosedJournals", numClosedClaimsCount.Name.ToString(), numClosedClaimsCount.Value.ToString());
                oSettings.WriteSettings_XML("ClosedJournals", chkClosedClaimsCount.Name.ToString(), chkClosedClaimsCount.Checked.ToString());

              

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

        #endregion


        #region " For Reading the Saved Settings"
        private void LoadLastSavedSetting()
        {
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            try
            {
                string _sValue = "";

                //-------------Charges
              
                _sValue = Convert.ToString(oSettings.ReadSettings_XML("ClosedJournals", numCloseDayTrayClaimCount.Name.ToString()));
                if (_sValue.Trim() != "")
                {
                    numCloseDayTrayClaimCount.Value = Convert.ToDecimal(_sValue);
                }

                _sValue = Convert.ToString(oSettings.ReadSettings_XML("ClosedJournals", chkCloseDayTrayClaimCount.Name.ToString()));
                if (_sValue.Trim() != "")
                {
                    chkCloseDayTrayClaimCount.Checked = Convert.ToBoolean(_sValue);
                }
                //-------------

                //-------------Queue
               

                _sValue = Convert.ToString(oSettings.ReadSettings_XML("ClosedJournals", numClosedClaimsCount.Name.ToString()));
                if (_sValue.Trim() != "")
                {
                    numClosedClaimsCount.Value = Convert.ToDecimal(_sValue);
                }

                _sValue = Convert.ToString(oSettings.ReadSettings_XML("ClosedJournals", chkClosedClaimsCount.Name.ToString()));
                if (_sValue.Trim() != "")
                {
                    chkClosedClaimsCount.Checked = Convert.ToBoolean(_sValue);
                }
                //-------------

                

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

        #endregion


    }
}