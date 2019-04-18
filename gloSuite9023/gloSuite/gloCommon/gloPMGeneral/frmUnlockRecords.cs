using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; //For SQL command object.
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloPMGeneral
{
    public partial class frmUnlockRecords : Form
    {

        #region "Private and Public variables"

        private string _databaseConnectionString = "";
        private Int64 _ClinicId = 0;
        private Int64 _UserID = 0;
        private String _UserName = "";
        private string _messageBoxCaption = "";
        private bool _isFormLoading = false;
        private bool _bSelected = false;
        
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        
        #region " Constants  For ERA Grid"

        private const int COL_SELECT = 0;
        private const int COL_ERAFileID = 1;
        private const int COL_ISAID = 2;
        private const int COL_BPRID = 3;
        private const int COL_UpdateDate = 4;
        private const int COL_ImportDate = 5;
        private const int COL_FileName = 6;
        private const int COL_OrigFileName = 7;
        private const int COL_ProdDate = 8;
        private const int COL_PayerID = 9;
        private const int COL_PayerName = 10;
        private const int COL_PayerContact = 11;
        private const int COL_PayMethod = 12;
        private const int COL_CheckNo = 13;
        private const int COL_CheckDate = 14;
        private const int COL_CheckDateNumeric = 15;
        private const int COL_CheckAmount = 16;
        private const int COL_CheckAmountHidden = 17;
        private const int COL_TotalClaimPaid = 18;
        private const int COL_TotalPLBAmount = 19;
        private const int COL_IsFullyPosted = 20;
        private const int COL_CheckNotesCount = 21;
        private const int COL_CloseDate = 22;
        private const int COL_PaymentTray = 23;
        private const int COL_UserName = 24;
        private const int COL_Total = 25;

        //private const int COL_SearchImportDate = 26;
        //private const int COL_SearchCheckDate = 27;

        private const int COL_WorkersCompFormID = 1;
        private const int COL_WorkersCompFormPatientName = 2;
        private const int COL_WorkersCompFormDateOfInjury = 3;
        private const int COL_WorkersCompFormType = 4;
        private const int COL_WorkersCompFormClaimNo = 5;
        private const int COL_WorkersCompFormDOS = 6;
        private const int COL_WorkersCompFormCreatedBy = 7;
        private const int COL_WorkersCompFormCreatedDate = 8;
        private const int COL_WorkersCompFormUserName = 9;
        private const int COL_WorkersCompFormMachineName = 10;

        #endregion
        
        #endregion

        #region " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicId; }
            set { _ClinicId = value; }
        }

        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        #endregion " Property Procedures "

        #region " Constructors "

        public frmUnlockRecords(String DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseConnectionString = DatabaseConnectionString;
                        
            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicId = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicId = 0; }
            }
            else
            { _ClinicId = 0; }

            #endregion

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

        #region " Form Load "

        private void frmUnlockRecords_Load(object sender, EventArgs e)
        {
            try
            {
                _isFormLoading = true;

                tsb_Select.Text = "&Select All";
                tsb_Select.Tag = "Select";


                #region "Tree View "

                //Add Nodes To the Tree View

                TreeNode oNodeCharges = new TreeNode();
                oNodeCharges.Text = "Charges";
                oNodeCharges.Tag = "Charges";
                oNodeCharges.ImageIndex = 0;
                oNodeCharges.SelectedImageIndex = 0;
                trvClosePeriod.Nodes.Add(oNodeCharges);           

                TreeNode oNodeERAChecks = new TreeNode();
                oNodeERAChecks.Text = "ERA Checks";
                oNodeERAChecks.Tag = "ERAChecks";
                oNodeERAChecks.ImageIndex = 1;
                oNodeERAChecks.SelectedImageIndex = 1;
                trvClosePeriod.Nodes.Add(oNodeERAChecks);

                TreeNode oNodeWorkersCompForm = new TreeNode();
                oNodeWorkersCompForm.Text = "Workers Comp Form";
                oNodeWorkersCompForm.Tag = "Workers Comp Form";
                oNodeWorkersCompForm.ImageIndex = 2;
                oNodeWorkersCompForm.SelectedImageIndex = 2;
                trvClosePeriod.Nodes.Add(oNodeWorkersCompForm);

                if (trvClosePeriod.Nodes.Count > 0) { trvClosePeriod.SelectedNode = trvClosePeriod.Nodes[0]; }
                #endregion

                _isFormLoading = false;

                //To Show and hide the select buttons if record exists in the grid
                IsRecordExist();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #endregion

        #region "Treeview Events"

        private void trvClosePeriod_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtBatchSearch.Clear();
            if (((TreeView)sender).Tag != null && ((TreeView)sender).Tag.ToString().Length > 0)
            {

                UnlockRecords(trvClosePeriod.SelectedNode.Tag.ToString());
                
            }
        }

        #endregion

        #region "Tool strip Events"

        private void tsb_Unlock_Click(object sender, EventArgs e)
        {
            try
            {


                DialogResult _DlgClearData = DialogResult.None;
                if (c1UnLockGrid.Rows.Count > 1)
                {
                    for (int i = 1; i <= c1UnLockGrid.Rows.Count - 1; i++)
                    {
                        if (Convert.ToBoolean(c1UnLockGrid.GetData(i, 0)) == true)
                        {
                            _bSelected = true;
                            break;
                        }
                    }

                    if (_bSelected == true)
                    {
                        _bSelected = false;
                        _DlgClearData = MessageBox.Show("Do you want to unlock selected record(s)? ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_DlgClearData == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select the record(s). ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    return;
                }

                for (int i = 1; i <= c1UnLockGrid.Rows.Count - 1; i++)
                {
                    if (Convert.ToBoolean(c1UnLockGrid.GetData(i, 0)) == true)
                    {
                        string _Node = trvClosePeriod.SelectedNode.Tag.ToString();
                        if (_Node == "ERAChecks")
                        {
                            UnlockCheck(Convert.ToInt64(c1UnLockGrid.GetData(i, COL_BPRID)));
                        }
                        else if (_Node == "Workers Comp Form")
                        {
                            UnLockWorkersCompForm(Convert.ToInt64(c1UnLockGrid.GetData(i, COL_WorkersCompFormID)));
                        }
                        else
                        {
                            Int64 TransID = Convert.ToInt64(c1UnLockGrid.GetData(i, 1));
                            Int64 UId = Convert.ToInt64(c1UnLockGrid.GetData(i, 15));
                            string _MachineId = Convert.ToString(c1UnLockGrid.GetData(i, 14));

                            //Method to Unlock the Records 
                            UpdateRecordStatus(TransID, UId, _MachineId, false);
                        }


                    }
                }
                //Method to Fill the Grid
                UnlockRecords(trvClosePeriod.SelectedNode.Tag.ToString());

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }

        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (trvClosePeriod.SelectedNode.Tag != null)
                {

                    UnlockRecords(trvClosePeriod.SelectedNode.Tag.ToString());

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void tsb_Select_Click(object sender, EventArgs e)
        {
            try
            {

                C1.Win.C1FlexGrid.CheckEnum oStatus = C1.Win.C1FlexGrid.CheckEnum.None;
                if (tsb_Select.Tag.ToString() == "Select")
                {
                    tsb_Select.Text = "&DeSelect All";
                    tsb_Select.Tag = "Deselect";
                    oStatus = C1.Win.C1FlexGrid.CheckEnum.Checked;
                }
                else if (tsb_Select.Tag.ToString() == "Deselect")
                {
                    tsb_Select.Text = "&Select All";
                    tsb_Select.Tag = "Select";
                    oStatus = C1.Win.C1FlexGrid.CheckEnum.Unchecked;
                }

                if (trvClosePeriod.SelectedNode.Tag != null)
                {

                    switch (trvClosePeriod.SelectedNode.Tag.ToString())
                    {
                        case "Charges":
                            {
                                if (c1UnLockGrid != null && c1UnLockGrid.Rows.Count > 0)
                                {
                                    for (int i = 1; i < c1UnLockGrid.Rows.Count; i++)
                                    {
                                        c1UnLockGrid.SetCellCheck(i, 0, oStatus);
                                    }
                                }
                            }
                            break;
                        case "ERAChecks":
                            {
                                if (c1UnLockGrid != null && c1UnLockGrid.Rows.Count > 0)
                                {
                                    for (int i = 1; i < c1UnLockGrid.Rows.Count; i++)
                                    {
                                        c1UnLockGrid.SetCellCheck(i, 0, oStatus);
                                    }
                                }
                            }
                            break;
                        case "Workers Comp Form":
                            {
                                if (c1UnLockGrid != null && c1UnLockGrid.Rows.Count > 0)
                                {
                                    for (int i = 1; i < c1UnLockGrid.Rows.Count; i++)
                                    {
                                        c1UnLockGrid.SetCellCheck(i, 0, oStatus);
                                    }
                                }
                            }
                            break;
                    }
                }

                //Select button visibility
                IsRecordExist();


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #endregion

        #region "Fill Methods"

        private void UnlockCheck(Int64 nBPRID)
        {
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                string _Query = "UPDATE ERA_BPR SET nCheckStatus = 1 WHERE nBPRID = " + nBPRID + "";
                oDB.Connect(false);
                oDB.Execute_Query(_Query);
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void UnlockRecords(string sType)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            DataTable dtLocked = null;
            Int16 _Mode = 0;

            tsb_Select.Text = "&Select All";
            tsb_Select.Tag = "Select";

            try
            {
                if (c1UnLockGrid != null)
                {
                    if (sType == "Charges")
                    {
                     //   c1UnLockGrid.Clear();
                        c1UnLockGrid.DataSource = null;
                        c1UnLockGrid.Visible = true;
                        oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                        oDBParameters = new gloDatabaseLayer.DBParameters();

                        if (sType != null)
                        {
                            //Has to Pass Parameters Based on the Condition 
                            //1 - Charges 2- Appointments 3- Payments

                            if (sType == "Payments")
                                _Mode = 1;
                            else if (sType == "Appointments")
                                _Mode = 1;
                            else
                                _Mode = 1;
                        }


                        oDBParameters.Add("@Mode", _Mode, ParameterDirection.Input, SqlDbType.Int);

                        oDB.Connect(false);

                        oDB.Retrive("BL_UnlockRecords", oDBParameters, out  dtLocked);
                        oDB.Disconnect();

                        if (dtLocked != null)
                        {
                            if (dtLocked.Rows.Count <= 0)
                            {
                                c1UnLockGrid.Rows.Count = 1;
                                c1UnLockGrid.Rows.Fixed = 1;
                            }


                            c1UnLockGrid.DataSource = dtLocked;

                            #region " Show Hide Column "

                            c1UnLockGrid.Cols["Select"].Visible = true;
                            c1UnLockGrid.Cols["nTransactionID"].Visible = false;
                            c1UnLockGrid.Cols["nTransactionDate"].Visible = true;
                            c1UnLockGrid.Cols["nClaimNo"].Visible = true;
                            c1UnLockGrid.Cols["nPatientID"].Visible = false;
                            c1UnLockGrid.Cols["PatientName"].Visible = true;

                            c1UnLockGrid.Cols["PatientFName"].Visible = false;
                            c1UnLockGrid.Cols["PatientMName"].Visible = false;
                            c1UnLockGrid.Cols["PatientLName"].Visible = false;


                            c1UnLockGrid.Cols["nProviderID"].Visible = false;
                            c1UnLockGrid.Cols["ProviderName"].Visible = true;

                            c1UnLockGrid.Cols["ProviderFName"].Visible = false;
                            c1UnLockGrid.Cols["ProviderMName"].Visible = false;
                            c1UnLockGrid.Cols["ProviderLName"].Visible = false;


                            c1UnLockGrid.Cols["sMachineID"].Visible = true;
                            c1UnLockGrid.Cols["nUserID"].Visible = false;
                            c1UnLockGrid.Cols["sUserName"].Visible = true;
                            c1UnLockGrid.AllowEditing = false;

                            c1UnLockGrid.AllowEditing = true;
                            c1UnLockGrid.Cols["Select"].AllowEditing = true;
                            c1UnLockGrid.Cols["nTransactionID"].AllowEditing = false;
                            c1UnLockGrid.Cols["nTransactionDate"].AllowEditing = false;
                            c1UnLockGrid.Cols["nClaimNo"].AllowEditing = false;
                            c1UnLockGrid.Cols["nPatientID"].AllowEditing = false;
                            c1UnLockGrid.Cols["PatientName"].AllowEditing = false;
                            c1UnLockGrid.Cols["nProviderID"].AllowEditing = false;
                            c1UnLockGrid.Cols["ProviderName"].AllowEditing = false;
                            c1UnLockGrid.Cols["sMachineID"].AllowEditing = false;
                            c1UnLockGrid.Cols["nUserID"].AllowEditing = false;
                            c1UnLockGrid.Cols["sUserName"].AllowEditing = false;
                            #endregion

                            #region " Set Width of Column "
                            c1UnLockGrid.Cols["Select"].Width = 50;
                            c1UnLockGrid.Cols["nTransactionID"].Width = 0;
                            c1UnLockGrid.Cols["nTransactionDate"].Width = 150;
                            c1UnLockGrid.Cols["nClaimNo"].Width = 100;
                            c1UnLockGrid.Cols["nPatientID"].Width = 0;
                            c1UnLockGrid.Cols["PatientName"].Width = 200;

                            c1UnLockGrid.Cols["PatientFName"].Width = 0;
                            c1UnLockGrid.Cols["PatientMName"].Width = 0;
                            c1UnLockGrid.Cols["PatientLName"].Width = 0;

                            c1UnLockGrid.Cols["nProviderID"].Width = 0;
                            c1UnLockGrid.Cols["ProviderName"].Width = 200;

                            c1UnLockGrid.Cols["ProviderFName"].Width = 0;
                            c1UnLockGrid.Cols["ProviderMName"].Width = 0;
                            c1UnLockGrid.Cols["ProviderLName"].Width = 0;

                            c1UnLockGrid.Cols["sMachineID"].Width = 130;
                            c1UnLockGrid.Cols["nUserID"].Width = 0;
                            c1UnLockGrid.Cols["sUserName"].Width = 200;
                            #endregion

                            #region " Set Header "
                            c1UnLockGrid.Cols["Select"].Caption = "Select";
                            c1UnLockGrid.Cols["nTransactionID"].Caption = "Transaction ID";
                            c1UnLockGrid.Cols["nTransactionDate"].Caption = "Close Date";
                            c1UnLockGrid.Cols["nClaimNo"].Caption = "Claim #";
                            c1UnLockGrid.Cols["nPatientID"].Caption = "Patient ID";
                            c1UnLockGrid.Cols["PatientName"].Caption = "Patient Name";

                            c1UnLockGrid.Cols["PatientFName"].Caption = "PatientFName";
                            c1UnLockGrid.Cols["PatientMName"].Caption = "PatientMName";
                            c1UnLockGrid.Cols["PatientLName"].Caption = "PatientLName";

                            c1UnLockGrid.Cols["nProviderID"].Caption = "Provider ID";
                            c1UnLockGrid.Cols["ProviderName"].Caption = "Provider Name";

                            c1UnLockGrid.Cols["ProviderFName"].Caption = "ProviderFName";
                            c1UnLockGrid.Cols["ProviderMName"].Caption = "ProviderMName";
                            c1UnLockGrid.Cols["ProviderLName"].Caption = "ProviderLName";

                            c1UnLockGrid.Cols["sMachineID"].Caption = "Machine ID";
                            c1UnLockGrid.Cols["nUserID"].Caption = "User ID";
                            c1UnLockGrid.Cols["sUserName"].Caption = "User Name";

                            #endregion

                            C1.Win.C1FlexGrid.CellStyle csBollean;// = c1UnLockGrid.Styles.Add("csBoolean");
                            try
                            {
                                if (c1UnLockGrid.Styles.Contains("csBoolean"))
                                {
                                    csBollean = c1UnLockGrid.Styles["csBoolean"];
                                }
                                else
                                {
                                    csBollean = c1UnLockGrid.Styles.Add("csBoolean");
            
                                }

                            }
                            catch
                            {
                                csBollean = c1UnLockGrid.Styles.Add("csBoolean");
              
                            }
              
                            csBollean.DataType = typeof(System.Boolean);
                            csBollean.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;  
                            
                            if (c1UnLockGrid.Rows.Count > 1)
                            {
                                C1.Win.C1FlexGrid.CellRange subCol;
                                subCol = c1UnLockGrid.GetCellRange(1, 0, c1UnLockGrid.Rows.Count - 1, 0);
                                subCol.Style = csBollean;
                            }
                            
                           

                            //c1UnLockGrid.Cols["Select"].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter;

                            c1UnLockGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                            c1UnLockGrid.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                            c1UnLockGrid.AutoResize = false;
                          

                        }
                    }
                    else if (sType == "ERAChecks")
                    {
                     //   c1UnLockGrid.Clear();
                        c1UnLockGrid.DataSource = null;
                        c1UnLockGrid.Visible = true;
                        oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                        oDBParameters = new gloDatabaseLayer.DBParameters();
                        oDBParameters.Clear();
                        oDBParameters.Add("@CheckStatus", 4, ParameterDirection.Input, SqlDbType.Int);
                        oDBParameters.Add("@UserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@ClinicID", _ClinicId, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Connect(false);
                        oDB.Retrive("ERA_GetChecks", oDBParameters, out dtLocked);
                        oDB.Disconnect();

                        if (dtLocked != null)
                        {
                            if (dtLocked.Rows.Count <= 0)
                            {
                                c1UnLockGrid.Rows.Count = 1;
                                c1UnLockGrid.Rows.Fixed = 1;
                            }

                            DataColumn oCol = new DataColumn("Select");
                            dtLocked.Columns.Add("Select");

                            for (int i = 0; i < dtLocked.Rows.Count; i++)
                            {
                                dtLocked.Rows[i][dtLocked.Columns.Count - 1] = "False";
                            }
                            dtLocked.AcceptChanges();
                            c1UnLockGrid.DataSource = dtLocked.DefaultView;

                            c1UnLockGrid.Cols.Move(c1UnLockGrid.Cols.Count-1,0);
                           
                            #region " Design Grid "
                            c1UnLockGrid.Cols[COL_SELECT].Caption = "Select";
                            c1UnLockGrid.Cols[COL_ImportDate].Caption = "Import Date";
                            c1UnLockGrid.Cols[COL_FileName].Caption = "File Name";
                            c1UnLockGrid.Cols[COL_PayerID].Caption = "Payer ID";
                            c1UnLockGrid.Cols[COL_PayerName].Caption = "Payer Name";
                            c1UnLockGrid.Cols[COL_CheckNo].Caption = "Check Number";
                            c1UnLockGrid.Cols[COL_CheckDate].Caption = "Check Date";
                            c1UnLockGrid.Cols[COL_CheckAmount].Caption = "Amount";

                            c1UnLockGrid.Cols[COL_ERAFileID].Visible = false;
                            c1UnLockGrid.Cols[COL_ISAID].Visible = false;
                            c1UnLockGrid.Cols[COL_BPRID].Visible = false;
                            c1UnLockGrid.Cols[COL_UpdateDate].Visible = false;
                            c1UnLockGrid.Cols[COL_OrigFileName].Visible = false;
                            c1UnLockGrid.Cols[COL_ProdDate].Visible = false;
                            c1UnLockGrid.Cols[COL_PayerContact].Visible = false;
                            c1UnLockGrid.Cols[COL_PayMethod].Visible = false;
                            c1UnLockGrid.Cols[COL_CheckDateNumeric].Visible = false;
                            c1UnLockGrid.Cols[COL_CheckAmountHidden].Visible = false;
                            c1UnLockGrid.Cols[COL_TotalClaimPaid].Visible = false;
                            c1UnLockGrid.Cols[COL_TotalPLBAmount].Visible = false;
                            c1UnLockGrid.Cols[COL_IsFullyPosted].Visible = false;
                            c1UnLockGrid.Cols[COL_CheckNotesCount].Visible = false;
                            c1UnLockGrid.Cols[COL_CloseDate].Visible = false;
                            c1UnLockGrid.Cols[COL_PaymentTray].Visible = false;
                            c1UnLockGrid.Cols[COL_UserName].Visible = false;
                            c1UnLockGrid.Cols["SearchImportDate"].Visible = false;
                            c1UnLockGrid.Cols["SearchCheckDate"].Visible = false;
                            c1UnLockGrid.Cols["DATEDIFFinDays"].Visible = false;

                            //c1UnLockGrid.Cols[COL_SearchImportDate].Visible = false;
                            //c1UnLockGrid.Cols[COL_SearchCheckDate].Visible = false;

                            Int32 _Width = c1UnLockGrid.Width;

                            c1UnLockGrid.Cols[COL_SELECT].Width = (Int32)(50);
                            c1UnLockGrid.Cols[COL_ImportDate].Width = (Int32)(_Width * 0.1);
                            c1UnLockGrid.Cols[COL_FileName].Width = (Int32)(_Width * 0.18);
                            c1UnLockGrid.Cols[COL_PayerID].Width = (Int32)(_Width * 0.18);
                            c1UnLockGrid.Cols[COL_PayerName].Width = (Int32)(_Width * 0.2);
                            c1UnLockGrid.Cols[COL_CheckNo].Width = (Int32)(_Width * 0.1);
                            c1UnLockGrid.Cols[COL_CheckDate].Width = (Int32)(_Width * 0.1);
                            c1UnLockGrid.Cols[COL_CheckAmount].Width = (Int32)(_Width * 0.07);

                            c1UnLockGrid.Cols[COL_CheckAmount].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                            c1UnLockGrid.Cols[COL_SELECT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

                            c1UnLockGrid.Cols[COL_ImportDate].DataType = typeof(System.DateTime);
                            c1UnLockGrid.Cols[COL_ImportDate].Format = "MM/dd/yyyy";

                            c1UnLockGrid.Cols[COL_CheckDate].DataType = typeof(System.DateTime);
                            c1UnLockGrid.Cols[COL_CheckDate].Format = "MM/dd/yyyy";

                            c1UnLockGrid.Cols[COL_SELECT].DataType = typeof(System.Boolean);

                            for (int i = 1; i < c1UnLockGrid.Cols.Count; i++)
                            {
                                c1UnLockGrid.Cols[i].AllowEditing = false;
                            }
                           
                            #endregion

                        }

                    }
                    else if (sType == "Workers Comp Form")
                    {
                     //   c1UnLockGrid.Clear();
                        c1UnLockGrid.DataSource = null;
                        c1UnLockGrid.Visible = true;
                        oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
                        oDBParameters = new gloDatabaseLayer.DBParameters();
                        oDBParameters.Clear();
                        oDBParameters.Add("@sMachinName", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@nTrnType", 23, ParameterDirection.Input, SqlDbType.Int);
                        oDBParameters.Add("@nMachinID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Connect(false);
                        oDB.Retrive("gsp_Select_UnLock_Record", oDBParameters, out dtLocked);
                        oDB.Disconnect();

                        if (dtLocked != null)
                        {
                            if (dtLocked.Rows.Count <= 0)
                            {
                                c1UnLockGrid.Rows.Count = 1;
                                c1UnLockGrid.Rows.Fixed = 1;
                            }

                            DataColumn oCol = new DataColumn("Select");
                            dtLocked.Columns.Add("Select");

                            for (int i = 0; i < dtLocked.Rows.Count; i++)
                            {
                                dtLocked.Rows[i][dtLocked.Columns.Count - 1] = "False";
                            }
                            dtLocked.AcceptChanges();
                            c1UnLockGrid.DataSource = dtLocked.DefaultView;

                            c1UnLockGrid.Cols.Move(c1UnLockGrid.Cols.Count - 1, 0);

                            #region " Design Grid "
                            c1UnLockGrid.Cols[COL_SELECT].Caption = "Select";
                            c1UnLockGrid.Cols[COL_WorkersCompFormID].Caption = "Form ID";
                            c1UnLockGrid.Cols[COL_WorkersCompFormPatientName].Caption = "Patient Name";
                            c1UnLockGrid.Cols[COL_WorkersCompFormDateOfInjury].Caption = "Date Of Injury";
                            c1UnLockGrid.Cols[COL_WorkersCompFormType].Caption = "Form Type";
                            c1UnLockGrid.Cols[COL_WorkersCompFormClaimNo].Caption = "Claim #";
                            c1UnLockGrid.Cols[COL_WorkersCompFormDOS].Caption = "DOS";
                            c1UnLockGrid.Cols[COL_WorkersCompFormCreatedBy].Caption = "Created By";
                            c1UnLockGrid.Cols[COL_WorkersCompFormCreatedDate].Caption = "Created Date";
                            c1UnLockGrid.Cols[COL_WorkersCompFormUserName].Caption = "User Name";
                            c1UnLockGrid.Cols[COL_WorkersCompFormMachineName].Caption = "Machine Name";

                            Int32 _Width = c1UnLockGrid.Width;

                            c1UnLockGrid.Cols[COL_SELECT].Width = (Int32)(50);
                            c1UnLockGrid.Cols[COL_WorkersCompFormID].Width = 0;
                            c1UnLockGrid.Cols[COL_WorkersCompFormPatientName].Width = (Int32)(_Width * 0.1);
                            c1UnLockGrid.Cols[COL_WorkersCompFormDateOfInjury].Width = (Int32)(_Width * 0.1);
                            c1UnLockGrid.Cols[COL_WorkersCompFormType].Width = (Int32)(_Width * 0.1);
                            c1UnLockGrid.Cols[COL_WorkersCompFormClaimNo].Width = (Int32)(_Width * 0.1);
                            c1UnLockGrid.Cols[COL_WorkersCompFormDOS].Width = (Int32)(_Width * 0.1);
                            c1UnLockGrid.Cols[COL_WorkersCompFormCreatedBy].Width = (Int32)(_Width * 0.1);
                            c1UnLockGrid.Cols[COL_WorkersCompFormCreatedDate].Width = (Int32)(_Width * 0.1);
                            c1UnLockGrid.Cols[COL_WorkersCompFormUserName].Width = (Int32)(_Width * 0.1);
                            c1UnLockGrid.Cols[COL_WorkersCompFormMachineName].Width = (Int32)(_Width * 0.1);

                            c1UnLockGrid.Cols[COL_SELECT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

                            c1UnLockGrid.Cols[COL_WorkersCompFormDateOfInjury].DataType = typeof(System.DateTime);
                            c1UnLockGrid.Cols[COL_WorkersCompFormDateOfInjury].Format = "MM/dd/yyyy";

                            c1UnLockGrid.Cols[COL_WorkersCompFormDOS].DataType = typeof(System.DateTime);
                            c1UnLockGrid.Cols[COL_WorkersCompFormDOS].Format = "MM/dd/yyyy";

                            c1UnLockGrid.Cols[COL_WorkersCompFormCreatedDate].DataType = typeof(System.DateTime);
                            c1UnLockGrid.Cols[COL_WorkersCompFormCreatedDate].Format = "MM/dd/yyyy";

                            c1UnLockGrid.Cols[COL_SELECT].DataType = typeof(System.Boolean);

                            for (int i = 1; i < c1UnLockGrid.Cols.Count; i++)
                            {
                                c1UnLockGrid.Cols[i].AllowEditing = false;
                            }

                            #endregion

                        }
                    }
                    else
                    {
                        c1UnLockGrid.Visible = false;
                    }

                    //Select button visibility
                    IsRecordExist();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; };
                if (oDB != null) { oDB.Dispose(); oDB = null; };
            }
        }

        public void UpdateRecordStatus(Int64 TransactionId, Int64 UserId,string MachineID, bool OpenTrue_CloseFalse)
        {
           
            SqlConnection con = new SqlConnection(_databaseConnectionString);
            SqlCommand command = new SqlCommand();
            SqlParameter oParam = null;
            try
            {
                con.Open(); 
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = con;
                command.CommandTimeout = 10000; // 10 Sec
                command.CommandText = "BL_UnlockClaims";
                oParam = new SqlParameter("@nTransactionMasterID",TransactionId);
                oParam.DbType = DbType.Int64;
                oParam.Direction = ParameterDirection.Input;
                command.Parameters.Add(oParam);

                oParam = new SqlParameter("@sMachineName", MachineID);
                oParam.DbType = DbType.String;
                oParam.Direction = ParameterDirection.Input;
                command.Parameters.Add(oParam);

                int iCount = command.ExecuteNonQuery();
                con.Close(); 
                

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (con.State==ConnectionState.Open ) { con.Close(); }
                con.Dispose();
                if (command != null)
                {
                    command.Parameters.Clear();
                    command.Dispose();
                    command = null;
                }
                if ((oParam != null))
                {
                    oParam = null;
                }

            }
        }

        public void UnLockWorkersCompForm(Int64 FormID)
        {

            SqlConnection con = new SqlConnection(_databaseConnectionString);
            SqlCommand command = new SqlCommand();
            SqlParameter oParam = null;
            try
            {
                con.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = con;
                command.CommandTimeout = 10000; // 10 Sec
                command.CommandText = "gsp_UnLock_Record";
               
                oParam = new SqlParameter("@nRecordID", FormID);
                oParam.DbType = DbType.Int64;
                oParam.Direction = ParameterDirection.Input;
                command.Parameters.Add(oParam);

                oParam = new SqlParameter("@nVisitID", 1);
                oParam.DbType = DbType.Int64;
                oParam.Direction = ParameterDirection.Input;
                command.Parameters.Add(oParam);

                oParam = new SqlParameter("@dtVisitDate", DateTime.Now);
                oParam.DbType = DbType.DateTime;
                oParam.Direction = ParameterDirection.Input;
                command.Parameters.Add(oParam);

                oParam = new SqlParameter("@nTrnType", 23);
                oParam.DbType = DbType.Int32;
                oParam.Direction = ParameterDirection.Input;
                command.Parameters.Add(oParam);

                int iCount = command.ExecuteNonQuery();
                con.Close();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Dispose();
                if (command != null)
                {
                    command.Parameters.Clear();
                    command.Dispose();
                    command = null;
                }
                if ((oParam != null))
                {
                    oParam = null;
                }

            }
        }
        
        #endregion

        #region "Search Events"

        private void txtBatchSearch_TextChanged(object sender, EventArgs e)
        {
            DataView _dv = null;
            DataTable dtTemp = null;
            C1.Win.C1FlexGrid.C1FlexGrid _C1 = null;
            try
            {
            if (trvClosePeriod.SelectedNode.Tag.ToString() == "ERAChecks")
            {               
                            

                    string _SearchText = ((TextBox)sender).Text.Trim();
                    string _Filter = "";

                    _SearchText = _SearchText.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]").Replace("*", "%");

                    #region " Get DV & C1 "

                    _dv = (DataView)c1UnLockGrid.DataSource;
                
                if (_dv == null) return;

                    _C1 = c1UnLockGrid;
                    #endregion

                    #region " SEARCH "
                    if (_SearchText == "")
                        _dv.RowFilter = "";
                    else
                    {
                        if (_SearchText.Contains(",") == false)
                        {
                            #region " Simple Search "
                            //
                            if (_SearchText.Length > 1)
                            {
                                string str = _SearchText.Substring(1).Replace("%", "");
                                _SearchText = _SearchText.Substring(0, 1) + str;
                            }
                            //
                            _Filter = //_C1.Cols[COL_ImportDate].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols["SearchImportDate"].Name + " LIKE '" + _SearchText + "%' OR " + 
                                _C1.Cols[COL_FileName].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_OrigFileName].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_PayerID].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_PayerName].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_CheckNo].Name + " LIKE '" + _SearchText + "%' OR " +
                                //_C1.Cols[COL_CheckDate].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols["SearchCheckDate"].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols[COL_CheckAmount].Name + " LIKE '" + _SearchText + "%' ";

                            //c1UnLockGrid.Cols["SearchImportDate"].Visible = false;
                            //c1UnLockGrid.Cols["SearchCheckDate"].Visible = false;
                            #endregion
                        }
                        else
                        {
                            #region " Comma Separated Search "

                            string[] _SplitSearch = _SearchText.Split(',');
                            string _SplitString;

                            for (int i = 0; i < _SplitSearch.Length; i++)
                            {

                                _SplitString = _SplitSearch[i].Trim();

                                if (_SplitString != "")
                                {
                                    if (_Filter != "")
                                        _Filter = _Filter + " AND ";

                                    _Filter = _Filter + " ( " +
                                        //_C1.Cols[COL_ImportDate].Name + " LIKE '" + _SplitString + "%' OR " +
                                         _C1.Cols["SearchImportDate"].Name + " LIKE '" + _SplitString + "%' OR " + 
                                        _C1.Cols[COL_FileName].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_OrigFileName].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_PayerID].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_PayerName].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_CheckNo].Name + " LIKE '" + _SplitString + "%' OR " +
                                       // _C1.Cols[COL_CheckDate].Name + " LIKE '" + _SplitString + "%' OR " +
                                       _C1.Cols["SearchCheckDate"].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols[COL_CheckAmount].Name + " LIKE '" + _SplitString + "%'" +
                                        " ) ";
                                }
                            }

                            #endregion
                        }

                        _dv.RowFilter = _Filter;
                    }
                    #endregion

                    // To Show and hide the select buttons if record exists in the dataview
                    if (_dv.Count != 0)
                    {
                        tsb_Select.Visible = true;
                    }
                    else
                    {
                        tsb_Select.Visible = false;
                    }
                    
                }
                           

            // Charges

            if (trvClosePeriod.SelectedNode.Tag.ToString() == "Charges")
            {
                            
                    string _SearchText = ((TextBox)sender).Text.Trim();
                    string _Filter = "";

                    _SearchText = _SearchText.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]").Replace("*", "%");

                    #region " Get DV & C1 "

                     dtTemp = (DataTable)c1UnLockGrid.DataSource;
                    _dv = dtTemp.DefaultView;

                    _C1 = c1UnLockGrid;

                    #endregion

                    #region " SEARCH "
                    if (_SearchText == "")
                        _dv.RowFilter = "";
                    else
                    {
                        if (_SearchText.Contains(",") == false)
                        {
                            #region " Simple Search "
                            //
                            if (_SearchText.Length > 1)
                            {
                                string str = _SearchText.Substring(1).Replace("%", "");
                                _SearchText = _SearchText.Substring(0, 1) + str;
                            }
                            //

                       _Filter = _C1.Cols["nTransactionDate"].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols["nClaimNo"].Name + " LIKE '" + _SearchText + "%' OR " +                                
                                _C1.Cols["PatientFName"].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols["PatientMName"].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols["PatientLName"].Name + " LIKE '" + _SearchText + "%' OR " +                             
                                _C1.Cols["ProviderFName"].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols["ProviderMName"].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols["ProviderLName"].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols["sMachineID"].Name + " LIKE '" + _SearchText + "%' OR " +
                                _C1.Cols["sUserName"].Name + " LIKE '" + _SearchText + "%' ";
                                

                            #endregion
                        }
                        else
                        {
                            #region " Comma Separated Search "

                            string[] _SplitSearch = _SearchText.Split(',');
                            string _SplitString;

                            for (int i = 0; i < _SplitSearch.Length; i++)
                            {

                                _SplitString = _SplitSearch[i].Trim();

                                if (_SplitString != "")
                                {
                                    if (_Filter != "")
                                        _Filter = _Filter + " AND ";

                                    _Filter = _Filter + " ( " +
                                        _C1.Cols["nTransactionDate"].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols["nClaimNo"].Name + " LIKE '" + _SplitString+ "%' OR " +
                                        _C1.Cols["PatientFName"].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols["PatientMName"].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols["PatientLName"].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols["ProviderFName"].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols["ProviderMName"].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols["ProviderLName"].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols["sMachineID"].Name + " LIKE '" + _SplitString + "%' OR " +
                                        _C1.Cols["sUserName"].Name + " LIKE '" + _SplitString + "%'"  +
                                        " ) ";
                                }
                            }

                            #endregion
                        }

                        _dv.RowFilter = _Filter;
                    }
                    
                    #endregion

                    // To Show and hide the select buttons if record exists in the dataview
                    if (_dv.Count != 0)
                    {
                        tsb_Select.Visible = true;
                    }
                    else
                    {
                        tsb_Select.Visible = false;
                    }

                }

            if (trvClosePeriod.SelectedNode.Tag.ToString() == "Workers Comp Form")
            {
                string _SearchText = ((TextBox)sender).Text.Trim();
                string _Filter = "";

                _SearchText = _SearchText.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "[%]").Replace("*", "%");

                #region " Get DV & C1 "

                _dv = (DataView)c1UnLockGrid.DataSource;

                _C1 = c1UnLockGrid;

                #endregion

                #region " SEARCH "
                if (_SearchText == "")
                    _dv.RowFilter = "";
                else
                {
                    if (_SearchText.Contains(",") == false)
                    {
                        #region " Simple Search "
                        //
                        if (_SearchText.Length > 1)
                        {
                            string str = _SearchText.Substring(1).Replace("%", "");
                            _SearchText = _SearchText.Substring(0, 1) + str;
                        }
                        //

                        _Filter = _C1.Cols[COL_WorkersCompFormPatientName].Name + " LIKE '" + _SearchText + "%' OR " +
                                 _C1.Cols[COL_WorkersCompFormDateOfInjury].Name + " LIKE '" + _SearchText + "%' OR " +
                                 _C1.Cols[COL_WorkersCompFormType].Name + " LIKE '" + _SearchText + "%' OR " +
                                 _C1.Cols[COL_WorkersCompFormClaimNo].Name + " LIKE '" + _SearchText + "%' OR " +
                                 _C1.Cols[COL_WorkersCompFormDOS].Name + " LIKE '" + _SearchText + "%' OR " +
                                 _C1.Cols[COL_WorkersCompFormCreatedBy].Name + " LIKE '" + _SearchText + "%' OR " +
                                 _C1.Cols[COL_WorkersCompFormCreatedDate].Name + " LIKE '" + _SearchText + "%' OR " +
                                 _C1.Cols[COL_WorkersCompFormUserName].Name + " LIKE '" + _SearchText + "%' OR " +
                                 _C1.Cols[COL_WorkersCompFormMachineName].Name + " LIKE '" + _SearchText + "%' ";


                        #endregion
                    }
                    else
                    {
                        #region " Comma Separated Search "

                        string[] _SplitSearch = _SearchText.Split(',');
                        string _SplitString;

                        for (int i = 0; i < _SplitSearch.Length; i++)
                        {

                            _SplitString = _SplitSearch[i].Trim();

                            if (_SplitString != "")
                            {
                                if (_Filter != "")
                                    _Filter = _Filter + " AND ";

                                _Filter = _Filter + " ( " +
                                    _C1.Cols[COL_WorkersCompFormPatientName].Name + " LIKE '" + _SplitString + "%' OR " +
                                    _C1.Cols[COL_WorkersCompFormDateOfInjury].Name + " LIKE '" + _SplitString + "%' OR " +
                                    _C1.Cols[COL_WorkersCompFormType].Name + " LIKE '" + _SplitString + "%' OR " +
                                    _C1.Cols[COL_WorkersCompFormClaimNo].Name + " LIKE '" + _SplitString + "%' OR " +
                                    _C1.Cols[COL_WorkersCompFormDOS].Name + " LIKE '" + _SplitString + "%' OR " +
                                    _C1.Cols[COL_WorkersCompFormCreatedBy].Name + " LIKE '" + _SplitString + "%' OR " +
                                    _C1.Cols[COL_WorkersCompFormCreatedDate].Name + " LIKE '" + _SplitString + "%' OR " +
                                    _C1.Cols[COL_WorkersCompFormUserName].Name + " LIKE '" + _SplitString + "%' OR " +
                                    _C1.Cols[COL_WorkersCompFormMachineName].Name + " LIKE '" + _SplitString + "%' OR " +
                                    " ) ";
                            }
                        }

                        #endregion
                    }

                    _dv.RowFilter = _Filter;
                }

                #endregion

                // To Show and hide the select buttons if record exists in the dataview
                if (_dv.Count != 0)
                {
                    tsb_Select.Visible = true;
                }
                else
                {
                    tsb_Select.Visible = false;
                }
            }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                _dv = null;
                _C1 = null;
                dtTemp = null;
            }
        }

        #endregion

        #region "Unlock Grid Events"

        private void c1UnLockGrid_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            bool _SelectAll = true;
            int _colIndex = 0;
            C1.Win.C1FlexGrid.CheckEnum _cheEnum = C1.Win.C1FlexGrid.CheckEnum.None;

            try
            {
                if (_isFormLoading == false)
                {
                    if (((C1.Win.C1FlexGrid.C1FlexGrid)sender) != null && ((C1.Win.C1FlexGrid.C1FlexGrid)sender).Rows.Count > 1)
                    {
                        //_colIndex = ((C1.Win.C1FlexGrid.C1FlexGrid)sender).Cols["Select"].Index;
                        _colIndex = ((C1.Win.C1FlexGrid.C1FlexGrid)sender).Cols[0].Index;
                        if (e.Col == _colIndex)
                        {
                            _cheEnum = ((C1.Win.C1FlexGrid.C1FlexGrid)sender).GetCellCheck(e.Row, _colIndex);

                            for (int i = 1; i <= ((C1.Win.C1FlexGrid.C1FlexGrid)sender).Rows.Count - 1; i++)
                            {
                                
                                if (((C1.Win.C1FlexGrid.C1FlexGrid)sender).GetCellCheck(i, _colIndex) == C1.Win.C1FlexGrid.CheckEnum.Unchecked)
                                {
                                    _SelectAll = false;
                                    break;
                                }
                                
                            }

                            if (_SelectAll == true)
                            {
                                tsb_Select.Text = "&DeSelect All";
                                tsb_Select.Tag = "Deselect";
                            }
                            else
                            {
                                tsb_Select.Text = "&Select All";
                                tsb_Select.Tag = "Select";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : Selection");
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false );
            }
            finally
            { }


        }

        #endregion

        #region "Is Record Exist Function"

        private void IsRecordExist()
        {
            try
            {
                if (trvClosePeriod.SelectedNode.Tag != null)
                {

                    if (c1UnLockGrid != null && c1UnLockGrid.Rows.Count > 1)
                    {
                        tsb_Select.Visible = true;
                    }
                    else
                    {
                        tsb_Select.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        #endregion

        
    }
}