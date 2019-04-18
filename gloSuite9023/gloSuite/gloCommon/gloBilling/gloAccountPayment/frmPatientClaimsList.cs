using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloDatabaseLayer;
using gloGlobal;
using gloBilling;

namespace gloAccountsV2
{
    public partial class frmPatientClaimsList : Form
    {
        public frmPatientClaimsList()
        {
            InitializeComponent();
        }
        public Int64 PatientID { get; set; }
        public Int64 PatientAccountID { get; set; }
        

        private void frmPatientClaimsList_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtClaims = GetPatientClaims(PatientID, PatientAccountID);
                c1PatientClaimGrid.DataSource = dtClaims;
                DesignGrid();
                FillInsuranceList();
                LockUnlockClaims(0);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        private void LockUnlockClaims(int nLockUnlockCode)
        {
            DBLayer oDB = new DBLayer(gloPMGlobal.DatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            DataTable dt = null, dtClaimData=null;

            try
            {
                dt = ((DataTable)c1PatientClaimGrid.DataSource);
                dt.AcceptChanges();
                dtClaimData = FillLockClaims(dt);

                
                oParameters.Add("@nOperation", nLockUnlockCode, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@tvpLockClaimsDetail", dtClaimData, ParameterDirection.Input, SqlDbType.Structured);
                oDB.Connect(false);
                oDB.Execute("gsp_LockUnlockClaims", oParameters);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                
                if (dtClaimData != null) { dtClaimData.Dispose(); dtClaimData = null; }
            }
            
        }

        private DataTable FillLockClaims(DataTable dtClaimsDetails)
        {
            DataTable dtLoadedClaimDetails = null;
            try
            {
                if (dtClaimsDetails != null && dtClaimsDetails.Rows.Count > 0)
                {
                    DataColumn dcTransactionMasterID = new DataColumn();
                    dcTransactionMasterID.DataType = System.Type.GetType("System.Int64");
                    dcTransactionMasterID.ColumnName = "nTransactionMasterID";
                    dcTransactionMasterID.Caption = "nTransactionMasterID";

                    DataColumn dcClaimNo = new DataColumn();
                    dcClaimNo.DataType = System.Type.GetType("System.Int64");
                    dcClaimNo.ColumnName = "ClaimNo";
                    dcClaimNo.Caption = "ClaimNo";

                    DataColumn dcIsLock = new DataColumn();
                    dcIsLock.DataType = System.Type.GetType("System.Boolean");
                    dcIsLock.ColumnName = "bIsLock";
                    dcIsLock.Caption = "bIsLock";

                    DataColumn dcMachineName = new DataColumn();
                    dcMachineName.DataType = System.Type.GetType("System.String");
                    dcMachineName.ColumnName = "sMachineName";
                    dcMachineName.Caption = "sMachineName";

                    DataColumn dcUserName = new DataColumn();
                    dcUserName.DataType = System.Type.GetType("System.String");
                    dcUserName.ColumnName = "sUserName";
                    dcUserName.Caption = "sUserName";

                    DataColumn dcUserID = new DataColumn();
                    dcUserID.DataType = System.Type.GetType("System.Int64");
                    dcUserID.ColumnName = "nUserID";
                    dcUserID.Caption = "nUserID";


                    dtLoadedClaimDetails = new DataTable();
                    dtLoadedClaimDetails.Columns.Add(dcTransactionMasterID);
                    dtLoadedClaimDetails.Columns.Add(dcClaimNo);
                    dtLoadedClaimDetails.Columns.Add(dcIsLock);
                    dtLoadedClaimDetails.Columns.Add(dcMachineName);
                    dtLoadedClaimDetails.Columns.Add(dcUserName);
                    dtLoadedClaimDetails.Columns.Add(dcUserID);

                    for (int i = 0; i <= dtClaimsDetails.Rows.Count - 1; i++)
                    {
                        dtLoadedClaimDetails.Rows.Add();
                        dtLoadedClaimDetails.Rows[i]["nTransactionMasterID"] = dtClaimsDetails.Rows[i]["nTransactionMasterID"];

                        string sClaimNo = Convert.ToString(dtClaimsDetails.Rows[i]["Claim#"]);
                        if (sClaimNo.Contains("-"))
                        {
                            string[] sSubClaimNo = sClaimNo.Split('-');
                            sClaimNo = sSubClaimNo[0];
                        }

                        dtLoadedClaimDetails.Rows[i]["ClaimNo"] = Convert.ToInt64(sClaimNo);

                        dtLoadedClaimDetails.Rows[i]["bIsLock"] = true;
                        dtLoadedClaimDetails.Rows[i]["sMachineName"] = Environment.MachineName;
                        dtLoadedClaimDetails.Rows[i]["sUserName"] = gloPMGlobal.UserName;
                        dtLoadedClaimDetails.Rows[i]["nUserID"] = gloPMGlobal.UserID;
                    }

                    dtLoadedClaimDetails.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            return dtLoadedClaimDetails;
        }

        private void FillInsuranceList()
        {
            try
            {
                DataTable dtInsurnce = GetPatinetInsurance(PatientID);

                DataRow dr = dtInsurnce.NewRow();
                dr["Insurance"] = "Select";
                dr["nInsuranceID"] = "-1";
                dtInsurnce.Rows.InsertAt(dr, 0);
                cmbPatientInsurances.DisplayMember = "Insurance";
                cmbPatientInsurances.ValueMember = "nInsuranceID";
                cmbPatientInsurances.DataSource = dtInsurnce;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        private DataTable GetPatinetInsurance(long PatientID)
        {
            DBLayer oDB = new DBLayer(gloPMGlobal.DatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            DataTable dtInsurances = new DataTable();

            try
            {
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetPatientInsurancesList", oParameters, out dtInsurances);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtInsurances;
        }

        private DataTable GetPatientClaims(long PatientID, long PatientAccountID)
        {
            DBLayer oDB = new DBLayer(gloPMGlobal.DatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            DataTable dtClaims = new DataTable();

            try
            {
                oParameters.Add("@nPAccountID", PatientAccountID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetPatientClaimDetails", oParameters, out dtClaims);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtClaims;
        }

        private void DesignGrid(C1.Win.C1FlexGrid.CheckEnum checkEnum=C1.Win.C1FlexGrid.CheckEnum.Unchecked)
        {
            c1PatientClaimGrid.Cols[0].DataType = typeof(bool);
            if (checkEnum == C1.Win.C1FlexGrid.CheckEnum.Checked)
            {
                c1PatientClaimGrid.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                c1PatientClaimGrid.SetData(0, 0, "Select All", true);
            }
            else
            {
                c1PatientClaimGrid.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                c1PatientClaimGrid.SetData(0, 0, "Select All", false);
            }
            c1PatientClaimGrid.Cols[0].Style.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1PatientClaimGrid.Cols[0].AllowEditing = true;
            c1PatientClaimGrid.Cols[0].AllowSorting = false;

            c1PatientClaimGrid.Cols[1].Visible = false;
            c1PatientClaimGrid.Cols[2].Visible = false;
            c1PatientClaimGrid.Cols[4].Visible = false;
            c1PatientClaimGrid.Cols[5].Visible = false;
            c1PatientClaimGrid.Cols[8].Visible = false;
            c1PatientClaimGrid.Cols[10].Visible = false;
            c1PatientClaimGrid.Cols[11].Visible = false;

            c1PatientClaimGrid.Cols[3].AllowEditing = false;
            c1PatientClaimGrid.Cols[6].AllowEditing = false;
            c1PatientClaimGrid.Cols[7].AllowEditing = false;
            c1PatientClaimGrid.Cols[9].AllowEditing = false;

            //c1PatientClaimGrid.Cols[0].Width = 250;
            c1PatientClaimGrid.Cols[3].Width = 80;
            c1PatientClaimGrid.Cols[6].Width = 150;
            c1PatientClaimGrid.Cols[7].Width = 150;
            //c1PatientClaimGrid.Cols[9].Width = 250;

            for (int i = 1; i < c1PatientClaimGrid.Rows.Count; i++)
            {
                if (Convert.ToBoolean(c1PatientClaimGrid.GetData(i,"bIsHold"))==true)
                {
                    c1PatientClaimGrid.Rows[i].StyleNew.ForeColor = Color.Red;
                }
            }

        }

        private void c1PatientClaimGrid_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                c1PatientClaimGrid.Redraw = false;
                if (e.Row == 0 && e.Col == 0)
                {
                    c1PatientClaimGrid.FinishEditing();
                    c1PatientClaimGrid.Select(0, 0);
                    DataTable dt = (DataTable)c1PatientClaimGrid.DataSource;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (c1PatientClaimGrid.GetCellCheck(e.Row, e.Col) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            dt.Select().ToList<DataRow>().ForEach(r => r["bIsSelect"] = true);
                            c1PatientClaimGrid.SetDataBinding(dt.Copy(), "", true);
                        }
                        else
                        {
                            dt.Select().ToList<DataRow>().ForEach(r => r["bIsSelect"] = false);
                            c1PatientClaimGrid.SetDataBinding(dt.Copy(), "", true);
                        }
                        
                        
                        if (dt != null) { dt.Dispose(); dt = null; }
                    }
                }
                c1PatientClaimGrid.Redraw = true;
                Cursor.Current = Cursors.Default;

                bool IsAllSelect = false;
                for (int i = 1; i <= c1PatientClaimGrid.Rows.Count - 1; i++)
                {
                    if (c1PatientClaimGrid.GetCellCheck(i,0) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                    {
                        IsAllSelect = true;
                    }
                    else
                    {
                        IsAllSelect = false;
                        c1PatientClaimGrid.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                        return ;
                    }

                }
                if (IsAllSelect)
                {
                    c1PatientClaimGrid.SetCellCheck(0, 0, C1.Win.C1FlexGrid.CheckEnum.Checked);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
        }

        private void tsb_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_btnTransfer_Click(object sender, EventArgs e)
        {
            string nBillingTransactionID = string.Empty, TrackTransactionID = string.Empty, nCurrenContactID = string.Empty, nCurrentInsuranceID = string.Empty,sErrorMsg=string.Empty;
            string _InsTransferCloseDate = string.Empty;
            Int64 nTransferredInsuranceID = 0;
            DataRow[] drSelectedRows = null;
            DataTable dtClaimList = null;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                dtClaimList=((DataTable)c1PatientClaimGrid.DataSource);
                dtClaimList.AcceptChanges();
                drSelectedRows = (dtClaimList.Select("bIsSelect='True'"));

                if (ValidateData(drSelectedRows))
                {
                    TrackTransactionID = dtClaimList.AsEnumerable()
                                          .Where(r => Convert.ToBoolean(r["bIsSelect"]) == true)
                                          .Select(row => row["nTransactionID"].ToString())
                                          .Aggregate((s1, s2) => String.Concat(s1, "," + s2));
                    nBillingTransactionID = dtClaimList.AsEnumerable()
                                            .Where(r => Convert.ToBoolean(r["bIsSelect"]) == true)
                                          .Select(row => row["nTransactionMasterID"].ToString())
                                          .Aggregate((s1, s2) => String.Concat(s1, "," + s2));

                    nCurrenContactID = dtClaimList.AsEnumerable()
                                            .Where(r => Convert.ToBoolean(r["bIsSelect"]) == true)
                                          .Select(row => row["nContactID"].ToString())
                                          .Aggregate((s1, s2) => String.Concat(s1, "," + s2));
                    nCurrentInsuranceID = dtClaimList.AsEnumerable()
                                            .Where(r => Convert.ToBoolean(r["bIsSelect"]) == true)
                                          .Select(row => row["nInsuranceID"].ToString())
                                          .Aggregate((s1, s2) => String.Concat(s1, "," + s2));

                    nTransferredInsuranceID = Convert.ToInt64(cmbPatientInsurances.SelectedValue.ToString());
                    DataTable dtStatus = null;
                    if (nBillingTransactionID != "" && TrackTransactionID != "" && nCurrenContactID != "" && nCurrentInsuranceID != "")
                    {
                        frmInsTransCloseDate ofrmInsTransCloseDate = new frmInsTransCloseDate(gloGlobal.gloPMGlobal.DatabaseConnectionString, TrackTransactionID, nBillingTransactionID, Convert.ToString(DateTime.Now.Date));
                        ofrmInsTransCloseDate.ShowDialog(this);
                        if (ofrmInsTransCloseDate.oDialogResult)
                        {
                            _InsTransferCloseDate = ofrmInsTransCloseDate.InsTransferCloseDate;
                            if (_InsTransferCloseDate != "")
                            {
                                dtStatus = TransferResponsibility(nBillingTransactionID, TrackTransactionID, nCurrenContactID, nCurrentInsuranceID, sErrorMsg, nTransferredInsuranceID, Convert.ToDateTime(_InsTransferCloseDate));
                            }
                        }
                        ofrmInsTransCloseDate.Dispose();
                    }

                    if (_InsTransferCloseDate != "")
                    {
                        if (dtStatus != null && dtStatus.Rows.Count > 0)
                        {
                            frmTransferResponsibilityStatus ofrmTransferResponsibilityStatus = new frmTransferResponsibilityStatus();
                            ofrmTransferResponsibilityStatus.dtResponsibilityTransfer = dtStatus;
                            ofrmTransferResponsibilityStatus.ShowDialog(this);
                            ofrmTransferResponsibilityStatus.Dispose();
                            this.Close();
                            gloBilling.Collections.CL_FollowUpCode.SetAutoAccountFollowUp(PatientAccountID, PatientID, Convert.ToDateTime(_InsTransferCloseDate));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (drSelectedRows != null)
                {
                    drSelectedRows = null;
                }

                _InsTransferCloseDate = string.Empty;
                nBillingTransactionID = string.Empty;
                TrackTransactionID = string.Empty;
                nCurrenContactID = string.Empty;
                nCurrentInsuranceID = string.Empty;
                this.Cursor = Cursors.Default;
            }
        }

        private DataTable TransferResponsibility(string nBillingTransactionID, string TrackTransactionID, string nCurrenContactID, string nCurrentInsuranceID, string sErrorMsg, Int64 nTransferredInsuranceID, DateTime dtCloseDate)
        {
            DBLayer oDB = new DBLayer(gloPMGlobal.DatabaseConnectionString);
            DBParameters oParameters = new DBParameters();
            DataTable _dt=new DataTable();
            string outError = null;
            Int64 nAuditLogID = GetUniqueID();
            if (TrackTransactionID != "")
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.TransferClaim, gloAuditTrail.ActivityType.TransferClaimsInsuranceResponsibility, "Insurance Responsibility Transfer Started : List of Selected Claim(s) - " + TrackTransactionID, 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            }
            try
            {
                oParameters.Add("@nBillingTransactionID", nBillingTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@TrackTransactionID", TrackTransactionID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nCurrenContactID", nCurrenContactID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nCurrentInsuranceID", nCurrentInsuranceID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nTransferredInsuranceID", nTransferredInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtCloseDate", dtCloseDate, ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@sErrMessage", sErrorMsg, ParameterDirection.InputOutput, SqlDbType.VarChar);
                oParameters.Add("@PrintFlag", 0, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nUserID", gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", gloPMGlobal.UserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sMachineName", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("BL_SplitTransactionClaim_ResponsibilityTransfer", oParameters,out _dt,out outError);
                oDB.Disconnect();

                if (_dt!=null && _dt.Rows.Count>0)
                {
                    if (outError=="S")
                    {
                        if (_dt!=null&&_dt.Rows.Count>0)
                        {
                            DataRow[] drFailed = _dt.Select("Description<>'Success'");
                            if (drFailed != null && drFailed.Length > 0)
                            {
                                BulkLog(drFailed.CopyToDataTable<DataRow>(), gloAuditTrail.ActivityOutCome.Success, "Insurance Responsibility Transfer", "List of Skipped Claim(s)", nAuditLogID);
                            }

                            DataRow[] drSuccessed = _dt.Select("Description='Success'");
                            if (drSuccessed != null && drSuccessed.Length > 0)
                            {
                                BulkLog(drSuccessed.CopyToDataTable<DataRow>(), gloAuditTrail.ActivityOutCome.Success, "Insurance Responsibility Transfer", "List of Successed Claim(s)", nAuditLogID);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.TransferClaim, gloAuditTrail.ActivityType.TransferClaimsInsuranceResponsibility, "Failure Insurance Responsibility Transfer", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.TransferClaim, gloAuditTrail.ActivityType.TransferClaimsInsuranceResponsibility, "Insurance Responsibility Transfer Completed", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

            }
            return _dt;
        }

        private bool ValidateData(DataRow[] drRows)
        {
            try
            {
                if (cmbPatientInsurances.SelectedValue.ToString() == "-1")
                {
                    MessageBox.Show("Select Patient Insurance.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbPatientInsurances.Focus();
                    return false;
                }
                else if (drRows.Length == 0)
                {
                    MessageBox.Show("Select Claim(s) to transfer responsibility.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return true;
        }
                
        public Int64 GetUniqueID()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dtLineIds = null;
            Int64 nUniqueID = 0;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@IDCount", 1, ParameterDirection.Input, SqlDbType.Int);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetUniqueIDs", oParameters, out _dtLineIds);
                oDB.Disconnect();

                if (_dtLineIds != null && _dtLineIds.Rows.Count > 0)
                {
                    nUniqueID = Convert.ToInt64(_dtLineIds.Rows[0][1]);
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }

            }
            return nUniqueID;

        }
        private void BulkLog(DataTable dtResult, gloAuditTrail.ActivityOutCome Status, String sMessage1, String sMessage2, Int64 nAuditLogID)
        {
            string TranAccountIDS = "";
            try
            {

                if (dtResult != null)
                {

                    TranAccountIDS = dtResult.AsEnumerable()
                                    .Select(row => row["nTransactionID"].ToString())
                                    .Aggregate((s1, s2) => String.Concat(s1, "|" + s2));
                    if (TranAccountIDS != "")
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.TransferClaim, gloAuditTrail.ActivityType.TransferClaimsInsuranceResponsibility, sMessage1 + " : " + sMessage2 + " Claim(s) - " + TranAccountIDS, 0, nAuditLogID, 0, Status, gloAuditTrail.SoftwareComponent.gloPM, true);

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.TransferClaim, gloAuditTrail.ActivityType.TransferClaimsInsuranceResponsibility, "AuditTrail failed for Insurance Responsibility Transfer", 0, nAuditLogID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
            }
            

        }

        private void frmPatientClaimsList_FormClosed(object sender, FormClosedEventArgs e)
        {
            LockUnlockClaims(1);
        }

    }
}
