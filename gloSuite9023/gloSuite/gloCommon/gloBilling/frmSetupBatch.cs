using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using gloSettings;

namespace gloBilling
{
    public partial class frmSetupBatch : Form
    {
        #region " Variable Declarations "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private bool _DialgoResult = false;
        private Int64 _BatchId = 0;
        private string _BatchName = "";
        private string _messageBoxCaption = String.Empty;
        private string _databaseconnectionstring = "";
        private bool _SendToNew = true;
        private string _BusinessCenterCode = "";
        private string _BatchPrefix = "";
        private Int64  _BilliingType = 0;
        private Int32 _BillingMethodID = 0;
        private Int64 _DefaultBillingMethod = 0;
        private bool _UB04Setting = false;

        #endregion 

        #region " Property Procedures "

        public bool DialgoResult
        {
            get { return _DialgoResult; }
            set { _DialgoResult = value; }
        }

        public Int64 BatchId
        {
            get { return _BatchId; }
            set { _BatchId = value; }
        }

        public string BatchName
        {
            get { return _BatchName; }
            set { _BatchName = value; }
        }

        public Int64 BilliingType
        {
            get { return _BilliingType; }
            set { _BilliingType = value; }
        }

        public Int32 BillingMethodID
        {
            get { return _BillingMethodID; }
            set { _BillingMethodID = value; }
        }
        public Int64 DefaultBillingMethod
        {
            get { return _DefaultBillingMethod; }
            set { _DefaultBillingMethod = value; }
        }
        public bool UB04Setting
        {
            get { return _UB04Setting; }
            set { _UB04Setting = value; }
        }

        public StringBuilder sTransactionIDs { get; set; } 

        #endregion 

        #region " Constructor "

        public frmSetupBatch(string Databaseconnectionstring, bool SendToNew, string BusinessCenterCode)
        {
            InitializeComponent();

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

            _databaseconnectionstring = Databaseconnectionstring;
            _SendToNew = SendToNew;
            _BusinessCenterCode = BusinessCenterCode;
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

        #endregion " Constructor "

        private void frmSetupBatch_Load(object sender, EventArgs e)
        {
            cmbBatch.Visible = false;
            txtBatchName.Visible = false;

            AddBatchBillingMethod();

            if (_SendToNew == true)
            {
                tsb_OK.Visible = false;
                tsb_Save.Visible = true; 
                txtBatchName.Visible = true;

                if (_BusinessCenterCode != "")
                    _BatchPrefix = gloGlobal.gloPMGlobal.IsUseBatchClaimPrefix == true && gloGlobal.gloPMGlobal.sClaimPrefix != "" ? gloGlobal.gloPMGlobal.sClaimPrefix + "Batch-" + _BusinessCenterCode + "-" + DateTime.Now.ToString("yyyyMMMdd") + "_"
                                                      : "Batch-" + _BusinessCenterCode + "-" + DateTime.Now.ToString("yyyyMMMdd") + "_";
                else
                    _BatchPrefix = gloGlobal.gloPMGlobal.IsUseBatchClaimPrefix == true && gloGlobal.gloPMGlobal.sClaimPrefix != "" ? gloGlobal.gloPMGlobal.sClaimPrefix + "Batch-" + DateTime.Now.ToString("yyyyMMMdd") + "_"
                                                      : "Batch-" + DateTime.Now.ToString("yyyyMMMdd") + "_";

                txtBatchName.Text = GetBatchName(_BatchPrefix);
                //txtBatchName.Text = "Batch-" + DateTime.Now.ToString("yyyyMMMdd");
            }
            else
            {
                FillBatch();
                tsb_OK.Visible = true;
                tsb_Save.Visible = false; 
                cmbBatch.Visible = true;
            }
            cmbBatchBillingMethod.SelectedValue = DefaultBillingMethod; 
            //Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            //Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
           
            //tom.SetTabOrder(scheme);
        }
        
        private void tsb_Save_Click(object sender, EventArgs e)
        {
            if (txtBatchName.Text.Trim() != "")
            {
                if (IsExistBatch(txtBatchName.Text.Trim()) == false)
                {
                    if (txtBatchName.Text.ToUpper().Trim() != "SELF")
                    {
                        if (cmbBatchBillingMethod.Text == "")
                        {
                            MessageBox.Show("Please select a Billing Method. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return; 
                        }

                        this.BatchId = 0;
                        this.BatchName = txtBatchName.Text.Trim();
                        //For batch billing method 
                        this.BillingMethodID = Convert.ToInt32(cmbBatchBillingMethod.SelectedValue);
                        this.DialgoResult = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please enter another batch name. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBatchName.Select(0, txtBatchName.Text.Length);
                    }
                }
                else
                {
                    MessageBox.Show("Batch Name already exists. Please enter another batch name. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBatchName.Select(0, txtBatchName.Text.Length);
                }

                #region"Check claim Remittance"
                if (sTransactionIDs != null)
                {
                    if (Convert.ToInt32(cmbBatchBillingMethod.SelectedValue) == Convert.ToInt32(BatchBillingMethod.ElectronicInstitutionalANSI4010) || Convert.ToInt32(cmbBatchBillingMethod.SelectedValue) == Convert.ToInt32(BatchBillingMethod.ElectronicProfessionalANSI4010) || Convert.ToInt32(cmbBatchBillingMethod.SelectedValue) == Convert.ToInt32(BatchBillingMethod.ElectronicProfessionalANSI5010) || Convert.ToInt32(cmbBatchBillingMethod.SelectedValue) == Convert.ToInt32(BatchBillingMethod.ElectronicInstitutionalANSI5010))
                    {
                        if (Check_RemitanceAmount(sTransactionIDs) == true)
                        {
                            sTransactionIDs = null;
                            this.DialgoResult = false;
                            return;
                        }
                        else
                        {
                            sTransactionIDs = null;
                        }
                    }
                }
                #endregion""
            }
            else
            {
                MessageBox.Show("Please enter the Batch Name", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBatch.SelectedIndex != -1)
                {
                    this.BatchId = Convert.ToInt64(cmbBatch.SelectedValue);
                    this.BatchName = Convert.ToString(cmbBatch.Text);
                    //For batch billing method 
                    this.BillingMethodID = Convert.ToInt32(cmbBatchBillingMethod.SelectedValue);  
                    this.DialgoResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select the Batch", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.DialgoResult = false;
            this.Close();
        }

        private void FillBatch()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtBatch = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "select distinct nBatchID,ISNULL(sBatchName,'') AS sBatchName " +
                " from BL_Transaction_Batch WITH(NOLOCK) where nBatchType = " + BatchType.Batch.GetHashCode() + " AND nClinicID = " + this._ClinicID + " ";
                oDB.Retrive_Query(_sqlQuery, out dtBatch);
                if (dtBatch != null && dtBatch.Rows.Count > 0)
                {
                    cmbBatch.DataSource = dtBatch;
                    cmbBatch.DisplayMember = dtBatch.Columns[1].ColumnName;
                    cmbBatch.ValueMember = dtBatch.Columns[0].ColumnName;
                    cmbBatch.SelectedIndex = -1;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }

        private bool IsExistBatch(string BatchName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _sqlQuery = "";
            bool _isexists = false;
            //DataTable dtBatch = null;
           BatchName=BatchName.Replace("'","''");
            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT ISNULL(COUNT(nBatchID),0) AS Batchid from BL_Transaction_Batch WITH(NOLOCK) " +
                " where UPPER(sBatchName) = '" + BatchName.ToUpper() + "' and nBatchType = " + BatchType.Batch.GetHashCode() + "";
                //oParameters.Add("@BatchName", BatchName, ParameterDirection.Input, SqlDbType.VarChar);
                //oParameters.Add("@BatchType", BatchType.Batch.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                //oDB.Retrive("BL_ISEXISTING_BATCH", oParameters, out dtBatch);
                _isexists = Convert.ToBoolean(oDB.ExecuteScalar_Query(_sqlQuery));
                //if (dtBatch != null && dtBatch.Rows.Count > 0)
                //{
                //    if (Convert.ToInt32(dtBatch.Rows[0]["Batchid"]) > 0)
                //    {
                //        _isexists = true;
                //    }
                //}
                
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
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
             return _isexists;
        }

        private string GetBatchName(string BatchPrefix)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
           
            string _batchName = "";
          //  int _batchCount = 0;
            DataTable _dtBatchCounter = null;
            try
            {
                oDB.Connect(false);

                oDBParameters.Clear();
                oDBParameters.Add("@sBatchPrefix", BatchPrefix, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("BL_GetBatchNameCounter", oDBParameters, out _dtBatchCounter);

                if (_dtBatchCounter != null && _dtBatchCounter.Rows.Count > 0)
                {
                    _batchName = BatchPrefix + Convert.ToString(_dtBatchCounter.Rows[0][0]);
                }
                //_sqlQuery = " SELECT ISNULL(MAX(convert(numeric,substring(sBatchName," + Convert.ToInt32(BatchPrefix.Length + 1) + ",len(sBatchName)- " + BatchPrefix.Length + "))),0) + 1   AS BatchName " +
                //" FROM BL_Transaction_Batch WITH(NOLOCK) " +
                //" WHERE  " +
                //" substring(sBatchName,1," + BatchPrefix.Length + ") = '" + BatchPrefix + "'  " +
                //" AND  isnumeric(substring(sBatchName, " + Convert.ToInt64(BatchPrefix.Length + 1) + ",len(sBatchName)- " + BatchPrefix.Length + ")) > 0 ";

                //_retVal = oDB.ExecuteScalar_Query(_sqlQuery);

                //if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                //{
                //    _batchName = BatchPrefix + Convert.ToInt64(_retVal);
                //}

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally 
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (_dtBatchCounter!=null)
                {
                    _dtBatchCounter.Dispose();
                    _dtBatchCounter = null;
                }
            }
            return _batchName;
        }
        //Sandip Darade 20091208

        private void txtBatchName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Regex.IsMatch(e.KeyChar.ToString(), @"^([\\\/\?\*\|\:\<\>\""]*)$") == true)
            {
                e.Handled = true;
            }
        }

        private void AddBatchBillingMethod()
        {
            DataColumn dcBillingMethod;
            DataRow drBillingMethod;
            DataTable dtBillingMethod = new DataTable();
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");

            try
            {
                
                cmbBatchBillingMethod.DataSource = null;
                cmbBatchBillingMethod.Items.Clear();
                dcBillingMethod = new DataColumn();
                dcBillingMethod.DataType = System.Type.GetType("System.Int32");
                dcBillingMethod.ColumnName = "ID";

                dtBillingMethod.Columns.Add(dcBillingMethod);

                dcBillingMethod = new DataColumn();
                dcBillingMethod.DataType = System.Type.GetType(" System.String");
                dcBillingMethod.ColumnName = "Desc";

                dtBillingMethod.Columns.Add(dcBillingMethod);  


                if (Convert.ToInt16(BillingType.Institutional) == BilliingType)
                {
                    drBillingMethod = dtBillingMethod.NewRow();
                    drBillingMethod["ID"] = BatchBillingMethod.None;
                    drBillingMethod["Desc"] = "";
                    dtBillingMethod.Rows.Add(drBillingMethod);

                    drBillingMethod = dtBillingMethod.NewRow();
                    drBillingMethod["ID"] = BatchBillingMethod.UB04 ;
                    drBillingMethod["Desc"] = "UB04";// "UB04-Paper";
                    dtBillingMethod.Rows.Add(drBillingMethod);

                    drBillingMethod = dtBillingMethod.NewRow();
                    drBillingMethod["ID"] = BatchBillingMethod.ElectronicInstitutionalANSI4010;
                    drBillingMethod["Desc"] = "Electronic Institutional [ANSI 4010]";//"UB04-Electronic";
                    dtBillingMethod.Rows.Add(drBillingMethod);

                    drBillingMethod = dtBillingMethod.NewRow();
                    drBillingMethod["ID"] = BatchBillingMethod.ElectronicInstitutionalANSI5010;
                    drBillingMethod["Desc"] = "Electronic Institutional [ANSI 5010]";//"UB04-Electronic";
                    dtBillingMethod.Rows.Add(drBillingMethod);

                }
                else
                {
                    drBillingMethod = dtBillingMethod.NewRow();
                    drBillingMethod["ID"] = BatchBillingMethod.None;
                    drBillingMethod["Desc"] = "";
                    dtBillingMethod.Rows.Add(drBillingMethod);

                    drBillingMethod = dtBillingMethod.NewRow();
                    drBillingMethod["ID"] = BatchBillingMethod.CMS1500;
                    drBillingMethod["Desc"] = "CMS1500 08/05"; //"Paper";
                    dtBillingMethod.Rows.Add(drBillingMethod);

                    drBillingMethod = dtBillingMethod.NewRow();
                    drBillingMethod["ID"] = BatchBillingMethod.CMS1500New;
                    drBillingMethod["Desc"] = "CMS1500 02/12"; //"Paper";
                    dtBillingMethod.Rows.Add(drBillingMethod);

                    if (UB04Setting)
                    {
                        drBillingMethod = dtBillingMethod.NewRow();
                        drBillingMethod["ID"] = BatchBillingMethod.ElectronicProfessionalANSI4010;
                        drBillingMethod["Desc"] = "Electronic Professional [ANSI 4010]";
                        dtBillingMethod.Rows.Add(drBillingMethod);

                        drBillingMethod = dtBillingMethod.NewRow();
                        drBillingMethod["ID"] = BatchBillingMethod.ElectronicProfessionalANSI5010;
                        drBillingMethod["Desc"] = "Electronic Professional [ANSI 5010]";
                        dtBillingMethod.Rows.Add(drBillingMethod);
                    }
                    else
                    {
                        drBillingMethod = dtBillingMethod.NewRow();
                        drBillingMethod["ID"] = BatchBillingMethod.ElectronicProfessionalANSI4010;
                        drBillingMethod["Desc"] = "Electronic [ANSI 4010]";
                        dtBillingMethod.Rows.Add(drBillingMethod);

                        drBillingMethod = dtBillingMethod.NewRow();
                        drBillingMethod["ID"] = BatchBillingMethod.ElectronicProfessionalANSI5010;
                        drBillingMethod["Desc"] = "Electronic [ANSI 5010]";
                        dtBillingMethod.Rows.Add(drBillingMethod);
                    }
                }
                ogloBilling.Dispose();
               // this.cmbBatchBillingMethod.SelectedIndexChanged -= new System.EventHandler(this.cmbBillingMethod_SelectedIndexChanged);
                cmbBatchBillingMethod.DataSource = dtBillingMethod;
                cmbBatchBillingMethod.DisplayMember = "Desc";
                cmbBatchBillingMethod.ValueMember = "ID";
                cmbBatchBillingMethod.Refresh();


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }

               // this.cmbBatchBillingMethod.SelectedIndexChanged += new System.EventHandler(this.cmbBillingMethod_SelectedIndexChanged);
            }
        }

        private bool Check_RemitanceAmount(StringBuilder  _sTransactionID)
        {

            DataTable _dtRemitance = new DataTable();
            string _sClaims = string.Empty;
            string _FilePath = AppDomain.CurrentDomain.BaseDirectory;
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            try
            {
                string _sMessage = string.Empty;
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor; 
                _dtRemitance = ogloBilling.Get_RemitanceBalance(_sTransactionID);
                this.Cursor = System.Windows.Forms.Cursors.Default;

                if (_dtRemitance != null && _dtRemitance.Rows.Count > 0)
                {
                    _sMessage = "Warning: There are claims with unbalanced remittances and "+ Environment.NewLine +"may be rejected when billing to secondary payers. " + Environment.NewLine;
                    _sMessage = _sMessage + Environment.NewLine + "Remittances must add up according to this formula: " + Environment.NewLine;
                    _sMessage = _sMessage + "Billed Amount – Insurance Payment must equal the " + Environment.NewLine + "sum of Write-off + Deduct + Co-ins + Withhold + Other Reasons. " + Environment.NewLine;
                    _sMessage = _sMessage + Environment.NewLine + "Cancel this New Batch? ";

                    DialogResult _result = MessageBox.Show(_sMessage, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (_result == DialogResult.Yes)
                    {

                        for (int i = 0; i <= _dtRemitance.Rows.Count - 1; i++)
                        {
                            _sClaims += Convert.ToString(_dtRemitance.Rows[i]["nClaimNo"]) + Environment.NewLine;
                        }

                        string _Header = Environment.NewLine + "Review Claims: " + Environment.NewLine + Environment.NewLine + "";
                        _Header += _sClaims;
                        _FilePath = _FilePath + "Remittances Balance.txt";
                        System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                        oStreamWriter.WriteLine(_Header);
                        oStreamWriter.Flush();
                        oStreamWriter.Close();
                        if (oStreamWriter != null)
                        {
                            oStreamWriter.Dispose();
                            oStreamWriter = null;
                        }
                        System.Diagnostics.Process.Start(_FilePath);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

               // return false;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
                dbEx.ERROR_Log(dbEx.ToString()); 
                return false; 
            }
            catch (Exception ex)
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 
                return false;
            }
            finally
            {
                if (_dtRemitance != null) { _dtRemitance.Dispose(); _dtRemitance = null; }
                if (ogloBilling != null) { ogloBilling.Dispose(); ogloBilling = null; }
            }
        }

    }
}