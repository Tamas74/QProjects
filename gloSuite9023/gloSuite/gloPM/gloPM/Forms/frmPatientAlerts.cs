using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloGlobal;

namespace gloPM
{
    public partial class frmPatientAlerts : Form
    {

        #region " Constructors "

        public frmPatientAlerts(string databaseconnectionstring)
        {
            InitializeComponent();
        }

        public frmPatientAlerts(string databaseconnectionstring,Int64 PatientID)
        {
            InitializeComponent();
            _PatientID = PatientID;
        }
        public frmPatientAlerts(Int64 PatientID)
        {
            InitializeComponent();
            _PatientID = PatientID;
        }
        #endregion " Constructors "

        #region " Declarations "
        //Declarations
        //Variable added by Mayuri:20091201
        //To check whether Status is Active or Inactive
        public bool _Status = false;
        private bool bStatus=false;
        Int64 _AlertId = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        int rowSel = 0;
        private string _AlertName = "";

        private bool _IsPatAlertModifyed = false;

        private Int64 _PatientID = 0;

        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        //
        public string AlertName
        {
            get { return _AlertName; }
            set { _AlertName = value; }
        }
        //
        #endregion Declarations

        #region " Grid Constants "

        private const int COL_ALERTID = 0;
        private const int COL_ALERTNAME = 1;
        private const int COL_ALERTCOLOR = 2;
        private const int COL_ALERTSTATUS = 3;
        private const int COL_ALERTSTATUSTEXT = 4;
        private const int COL_ALERTTYPE = 5;
        private const int COL_PATIENTID = 6;
        private const int COL_IS_SYSTEMALERT = 7;
        private const int COL_COUNT = 8;

        #endregion  " Grid Constants "

        #region " Form Load "

        private void frmPatientAlerts_Load(object sender, EventArgs e)
        {


            gloC1FlexStyle.Style(c1PatientAlerts, false); 
            
            
            bStatus = true;
            DesignGrid();
            FillAlerts();

            txtAlertName.Enabled = false;
            tsb_New.Enabled = true;
            tsb_Modify.Enabled = true;
            tsb_Delete.Enabled = true;
            ts_btnSave.Enabled = false;
            ts_btnClose.Visible = true;
            ts_btnCancel.Visible = false;
            tls_btnSave.Enabled  = false;
            rdbActive.Enabled = false;
            rdbInActive.Enabled = false;
        }

        #endregion " Form Load "

        #region " Design Grid "

        private void DesignGrid()
        {
            try
            {
                c1PatientAlerts.Cols.Count = COL_COUNT;
                c1PatientAlerts.Rows.Count = 1;
               
                c1PatientAlerts.SetData(0, COL_ALERTID, "nAlertID ");
                c1PatientAlerts.SetData(0, COL_ALERTNAME, "Alerts");
                c1PatientAlerts.SetData(0, COL_ALERTTYPE, "Alert Type");
                c1PatientAlerts.SetData(0, COL_ALERTSTATUS, "AlertStatus");
                c1PatientAlerts.SetData(0, COL_ALERTSTATUSTEXT, "Status");
                c1PatientAlerts.SetData(0, COL_PATIENTID, "PatientID");
                c1PatientAlerts.SetData(0, COL_ALERTCOLOR, "Alert Color");
                c1PatientAlerts.SetData(0, COL_IS_SYSTEMALERT, "IsSystem");
               

                c1PatientAlerts.Cols[COL_ALERTID].Visible = false;
                c1PatientAlerts.Cols[COL_ALERTNAME].Visible = true;
                c1PatientAlerts.Cols[COL_ALERTTYPE].Visible = false;
                c1PatientAlerts.Cols[COL_ALERTSTATUS].Visible = false;
                c1PatientAlerts.Cols[COL_ALERTSTATUSTEXT].Visible = true;
                c1PatientAlerts.Cols[COL_ALERTCOLOR].Visible = false;
                c1PatientAlerts.Cols[COL_PATIENTID].Visible = false;
                c1PatientAlerts.Cols[COL_IS_SYSTEMALERT].Visible = false;

                int nWidth = pnl_PatientAlertGrid.Width;
                c1PatientAlerts.Cols[COL_ALERTID].Width = 0;
                c1PatientAlerts.Cols[COL_ALERTNAME].Width = (int)(0.85 * (nWidth));
                //c1PatientAlerts.Cols[COL_ALERTSTATUS].Width = (int)(0.08 * (nWidth));
                c1PatientAlerts.Cols[COL_ALERTSTATUS].Width = 0; 
                c1PatientAlerts.Cols[COL_ALERTSTATUSTEXT].Width = (int)(0.10 * (nWidth));
                c1PatientAlerts.Cols[COL_ALERTTYPE].Width = 0;
                c1PatientAlerts.Cols[COL_ALERTCOLOR].Width = 0;
                c1PatientAlerts.Cols[COL_PATIENTID].Width = 0;
                c1PatientAlerts.Cols[COL_IS_SYSTEMALERT].Width= 0;

                c1PatientAlerts.Cols[COL_ALERTID].DataType = typeof(System.Int64);
                c1PatientAlerts.Cols[COL_ALERTNAME].DataType = typeof(System.String);
                c1PatientAlerts.Cols[COL_ALERTSTATUS].DataType = typeof(System.Boolean);
                c1PatientAlerts.Cols[COL_ALERTSTATUSTEXT].DataType = typeof(System.String); 
                c1PatientAlerts.Cols[COL_ALERTTYPE].DataType = typeof(System.Int64);
                c1PatientAlerts.Cols[COL_ALERTCOLOR].DataType = typeof(System.Int64);
                c1PatientAlerts.Cols[COL_PATIENTID].DataType = typeof(System.Int64);
                c1PatientAlerts.Cols[COL_IS_SYSTEMALERT].DataType = typeof(System.Boolean);

                //c1PatientAlerts.Cols[COL_ALERTID].AllowEditing = true; 
                //c1PatientAlerts.Cols[COL_ALERTNAME].AllowEditing = true;
                //c1PatientAlerts.Cols[COL_ALERTTYPE].AllowEditing = true;
                //c1PatientAlerts.Cols[COL_ALERTCOLOR].AllowEditing = true;
                //c1PatientAlerts.Cols[COL_ALERTSTATUS].AllowEditing=true;
                //c1PatientAlerts.Cols[COL_PATIENTID].AllowEditing = true;
                
                c1PatientAlerts.AllowEditing = false;
                c1PatientAlerts.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
                c1PatientAlerts.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion " Design Grid "

        #region " Button Click Events "

        private void ts_btnSave_Click(object sender, EventArgs e)
        {

            Int64 _Intresult = 0;
            try
            {
                
                if (txtAlertName.Text.Trim() != "")
                {
                    if (_AlertId == 0)
                    {

                        _Intresult = SaveAlerts(_AlertId, txtAlertName.Text.Trim(), bStatus);
                        DesignGrid();
                        FillAlerts();
                        txtAlertName.Text = "";
                        txtAlertName.Tag = null;
                        rdbActive.Checked = true;
                        _AlertId = 0;
                    }
                    else
                    {
                        _Intresult = SaveAlerts(Convert.ToInt64(txtAlertName.Tag), txtAlertName.Text.Trim(), rdbActive.Checked);
                        DesignGrid();
                        FillAlerts();
                        c1PatientAlerts.RowSel = rowSel;
                        txtAlertName.Text = "";
                        txtAlertName.Tag = null;
                        rdbActive.Checked = true;
                        _AlertId = 0;
                    }

                    tsb_New.Enabled = true;
                    tsb_Modify.Enabled = true;
                    tsb_Delete.Enabled = true;
                    ts_btnSave.Enabled = false;
                    tls_btnSave.Enabled = false;
                    ts_btnClose.Visible = true;
                    ts_btnCancel.Visible = false;

                    rdbActive.Enabled = false;
                    rdbInActive.Enabled = false;

                    txtAlertName.Enabled = false;
                    _IsPatAlertModifyed = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Enter an alert description.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ts_btnSave.Visible = true;
                    tls_btnSave.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
               
            }
        }
        //Code Added by Mayuri:20091201
        //To check whether any changes done by user or not
        public bool IsModified()
        {
            if (_AlertId == 0)
            {
                //if (txtAlertName.Text.Trim() == AlertName && rdbInActive.Checked != true)
                    if(txtAlertName.Text.Trim() == AlertName)
                {
                    return false;
                }
            }
            else if (_AlertId > 0)
            {

                if (txtAlertName.Text.Trim() == AlertName && (_Status == true && rdbInActive.Checked == false || _Status == false && rdbActive.Checked == false))
                {
                    return false;
                }

            }
            else
            {
                return true;
            }
            return true;
        }
        //End code added by Mayuri:20091201
        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Color_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ColorDialog oColorDialog = new ColorDialog();
            try
            {
                oColorDialog.CustomColors = gloGlobal.gloCustomColor.customColor;
            }
            catch
            {
            }
            if (oColorDialog.ShowDialog(this) == DialogResult.OK)
            {
                lbl_ColorContainer.BackColor = oColorDialog.Color;
                lbl_ColorContainer.BackColor = oColorDialog.Color;
                try
                {
                    gloGlobal.gloCustomColor.customColor = oColorDialog.CustomColors;
                }
                catch
                {
                }
            }
            oColorDialog.Dispose();
            oColorDialog = null;
        }

        private void btnApp_ClearColor_Click(object sender, EventArgs e)
        {
            lbl_ColorContainer.BackColor = Color.White;
            lbl_ColorContainer.Refresh();
        }

        private void tsb_Modify_Click(object sender, EventArgs e)
        {
            
            if (c1PatientAlerts.Rows.Count > 1)
            {
                if (c1PatientAlerts.RowSel > 0)
                {
                    if (Convert.ToBoolean(c1PatientAlerts.GetData(c1PatientAlerts.RowSel, COL_IS_SYSTEMALERT)) == false)
                    {
                        rowSel = c1PatientAlerts.RowSel;
                        _AlertId = Convert.ToInt64(c1PatientAlerts.GetData(c1PatientAlerts.RowSel, COL_ALERTID));
                        txtAlertName.Tag = _AlertId;
                        txtAlertName.Text = Convert.ToString(c1PatientAlerts.GetData(c1PatientAlerts.RowSel, COL_ALERTNAME)).Trim();
                        //Code Added by Mayuri:20091201
                        //To hold Description:
                        AlertName = txtAlertName.Text;
                        //End code Added by Mayuri:20091201
                        if (Convert.ToBoolean(c1PatientAlerts.GetData(c1PatientAlerts.RowSel, COL_ALERTSTATUS)) == true)
                        {
                            rdbActive.Checked = true;
                            rdbInActive.Checked = false;
                            _Status = true;
                        }
                        else
                        {
                            rdbInActive.Checked = true;
                            rdbActive.Checked = false;
                            _Status = false;
                        }

                        tsb_New.Enabled = false;
                        tsb_Modify.Enabled = false;
                        tsb_Delete.Enabled = false;
                        ts_btnSave.Enabled = true;
                        tls_btnSave.Enabled = true;
                        ts_btnClose.Visible = false;
                        ts_btnCancel.Visible = true;

                        txtAlertName.Enabled = true;

                        rdbActive.Enabled = true;
                        rdbInActive.Enabled = true;

                        txtAlertName.Focus();
                        txtAlertName.SelectAll();

                        
                    }
                    else if (Convert.ToBoolean(c1PatientAlerts.GetData(c1PatientAlerts.RowSel, COL_IS_SYSTEMALERT)) == true)
                    {
                        MessageBox.Show("Cannot modify system defined alerts.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        }

        private void tsb_Delete_Click(object sender, EventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            string strSQL = "";
            object _result = null;
            //Int64 _Intresult = 0;
            Int64 _ID = 0;

            try
            {
                oDB.Connect(false);
                if (c1PatientAlerts != null && c1PatientAlerts.Rows.Count > 0)
                {
                    if (c1PatientAlerts.RowSel > 0)
                    {
                        if (Convert.ToBoolean(c1PatientAlerts.GetData(c1PatientAlerts.RowSel, COL_IS_SYSTEMALERT)) == false)
                        {
                            if (MessageBox.Show("Are you sure you want to delete this alert?", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _ID = Convert.ToInt64(c1PatientAlerts.GetData(c1PatientAlerts.Row, COL_ALERTID));
                                strSQL = "DELETE FROM PatientAlerts WHERE (nPatientID = " + _PatientID + ") AND (nClinicID = " + gloPMGlobal.ClinicID + ") AND nAlertID=" + _ID + "";
                                _result = oDB.ExecuteScalar_Query(strSQL);
                            }

                        }
                        else if (Convert.ToBoolean(c1PatientAlerts.GetData(c1PatientAlerts.RowSel, COL_IS_SYSTEMALERT)) == true)
                        {
                            MessageBox.Show("Cannot delete system defined alerts.  ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                DesignGrid();
                FillAlerts();
                txtAlertName.Text = "";
                txtAlertName.Tag = 0;
                if (oDB != null) { oDB.Dispose(); }
            }
            
        }

        private void tsb_New_Click(object sender, EventArgs e)
        {
            txtAlertName.Enabled = true;
           
            tsb_New.Enabled = false;
            tsb_Modify.Enabled = false;
            tsb_Delete.Enabled = false;
            ts_btnSave.Enabled = true;
            tls_btnSave.Enabled = true;
            ts_btnClose.Visible = false;
            ts_btnCancel.Visible = true;

            rdbActive.Enabled = true;
            rdbInActive.Enabled = true;

            txtAlertName.Text = "";
            txtAlertName.Tag = 0;
            _AlertId = 0;
            rdbActive.Checked = true;
            txtAlertName.Focus();
            
        }

        #endregion " Button Click Events "

        #region " Private Methods "

        private void FillAlerts()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable dt = new DataTable();
            string _strSQL = "";
            int _rowIndex=0;
            try
            {
                string LastSeendate = GetLastSeenDate(_PatientID);
                if (LastSeendate.Length > 0)
                {
                    c1PatientAlerts.Rows.Add();
                    _rowIndex = c1PatientAlerts.Rows.Count - 1;
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "Last Seen On " + LastSeendate);
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                    c1PatientAlerts.SetCellCheck(_rowIndex, COL_ALERTSTATUS, C1.Win.C1FlexGrid.CheckEnum.Checked );
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUSTEXT, "");
                    c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");
                    c1PatientAlerts.SetData(_rowIndex, COL_IS_SYSTEMALERT, true);
                }

                gloBilling.Common.PatientBalances oPatientBalances = new gloBilling.Common.PatientBalances();
                gloBilling.gloPayment oPayment = new gloBilling.gloPayment(gloPMGlobal.DatabaseConnectionString);
                oPatientBalances = oPayment.GetPatientBalaces(_PatientID, gloPMGlobal.ClinicID);

                if (oPatientBalances != null)
                {
                    if (oPatientBalances.Count > 0)
                    {
                        c1PatientAlerts.Rows.Add();
                        _rowIndex = c1PatientAlerts.Rows.Count - 1;
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "Balances");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                        c1PatientAlerts.SetCellCheck(_rowIndex, COL_ALERTSTATUS, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUSTEXT, "");
                        c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");
                        c1PatientAlerts.SetData(_rowIndex, COL_IS_SYSTEMALERT, true);
                    }

                    for (int i = 0; i <= oPatientBalances.Count - 1; i++)
                    {
                        c1PatientAlerts.Rows.Add();
                        _rowIndex = c1PatientAlerts.Rows.Count - 1;
                        string Balance = "    " + oPatientBalances[i].SelfInsuranceName + " : " + oPatientBalances[i].SelfInsuranceBalance.ToString();
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, Balance);
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                        c1PatientAlerts.SetCellCheck(_rowIndex, COL_ALERTSTATUS, C1.Win.C1FlexGrid.CheckEnum.Checked);
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUSTEXT, "Active");
                        c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");
                        c1PatientAlerts.SetData(_rowIndex, COL_IS_SYSTEMALERT, true);
                    }
                }

                if (oPatientBalances != null) { oPatientBalances.Dispose(); }
                if (oPayment != null) { oPayment.Dispose(); }

                oDB.Connect(false);
                _strSQL = " SELECT ISNULL(nAlertID,0) AS nAlertID,ISNULL(sAlertName,'') AS sAlertName, "+
                          " ISNULL(nAlertType,0) AS nAlertType,ISNULL(bAlertStatus,0) AS  bAlertStatus, "+
                          " ISNULL(sAlertColor,'') AS sAlertColor,ISNULL(nPatientID,0) AS nPatientID,ISNULL(nClinicID,0) AS nClinicID " +
                          " FROM PatientAlerts "+
                          " WHERE (nPatientID = " + _PatientID + ") AND (nClinicID = " + gloPMGlobal.ClinicID + ")";
                oDB.Retrive_Query(_strSQL, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    c1PatientAlerts.Rows.Add();
                    _rowIndex = c1PatientAlerts.Rows.Count - 1;
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "Alerts ");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                    c1PatientAlerts.SetCellCheck(_rowIndex, COL_ALERTSTATUS, C1.Win.C1FlexGrid.CheckEnum.Unchecked);
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUSTEXT, "");
                    c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");
                    c1PatientAlerts.SetData(_rowIndex, COL_IS_SYSTEMALERT, true);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        c1PatientAlerts.Rows.Add();
                        _rowIndex = c1PatientAlerts.Rows.Count - 1;

                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, Convert.ToInt64(dt.Rows[i]["nAlertID"]));
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "    " + Convert.ToString(dt.Rows[i]["sAlertName"]));
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, Convert.ToString(dt.Rows[i]["nAlertType"]));
                        if (Convert.ToBoolean(dt.Rows[i]["bAlertStatus"]) == true)
                        {
                            c1PatientAlerts.SetCellCheck(_rowIndex, COL_ALERTSTATUS, C1.Win.C1FlexGrid.CheckEnum.Checked);
                            c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUSTEXT, "Active");
                        }
                        else
                        {
                            c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUSTEXT, "Inactive");
                        }
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, Convert.ToString(dt.Rows[i]["sAlertColor"]));
                        c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, Convert.ToInt64(dt.Rows[i]["nPatientID"]));
                        c1PatientAlerts.SetData(_rowIndex, COL_IS_SYSTEMALERT, false);
                    }
                }

                DataTable dtNotes = new DataTable();
                dtNotes = GetPatientNotes(_PatientID);
               // string Notes = "";
                if (dtNotes != null && dtNotes.Rows.Count > 0)
                {
                    c1PatientAlerts.Rows.Add();
                    _rowIndex = c1PatientAlerts.Rows.Count - 1;
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "Calendar Notes ");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUSTEXT, "");
                    c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                    c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");
                    c1PatientAlerts.SetData(_rowIndex, COL_IS_SYSTEMALERT, true);

                    for (int i = 0; i < dtNotes.Rows.Count; i++)
                    {
                        c1PatientAlerts.Rows.Add();
                        _rowIndex = c1PatientAlerts.Rows.Count - 1;
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTID, "0 ");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTNAME, "    " + Convert.ToString(dtNotes.Rows[i]["sNotes"]));
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTTYPE, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUS, "True");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTSTATUSTEXT, "Active");
                        c1PatientAlerts.SetData(_rowIndex, COL_PATIENTID, "0");
                        c1PatientAlerts.SetData(_rowIndex, COL_ALERTCOLOR, "");
                        c1PatientAlerts.SetData(_rowIndex, COL_IS_SYSTEMALERT, true);
                    }
                }
                oDB.Disconnect();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private Int64 SaveAlerts(Int64 AlertId,string AlertName,bool bStatus)
        {
            //*** Note 
            //Method also implemented on frmSetupQuickPatient 
            //Any modifications to this method please also check for the same
            //on frmSetupQuickPatient in gloPatient project

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = new object();
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);
                oParameters.Add("@nAlertID", AlertId, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sAlertName", AlertName, ParameterDirection.Input, SqlDbType.VarChar, 250);
                oParameters.Add("@nAlertType", 0, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@bAlertStatus", bStatus, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@sAlertColor", "", ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("AB_INUP_PatientAlerts", oParameters, out _oResult);

                oDB.Disconnect();

                return Convert.ToInt64(_oResult);

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                
                oDB.Dispose();
                oParameters.Dispose();
                _oResult = null;
            }
        }

        private string GetPatientBalance(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
           // string strSQL = "";
            object _result = null;

            try
            {
                //oDB.Connect(false);
               // strSQL = "";
               // _result = oDB.ExecuteScalar_Query(strSQL);

                if (_result != null)
                {
                    return Convert.ToString(_result);
                }
                return "";
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private string GetLastSeenDate(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            string strSQL = "";
            object _result = null;

            try
            {
                oDB.Connect(false);
                strSQL = "SELECT MAX(dtDate) FROM PatientTracking WHERE (nPatientID = " + PatientID + ") AND (nClinicID = " + gloPMGlobal.ClinicID + ") AND (nTrackingStatus = 3)";
                _result = oDB.ExecuteScalar_Query(strSQL);

                if (_result != null)
                {
                    return Convert.ToDateTime(_result).ToShortDateString();
                }
                oDB.Disconnect(); 
                return "";
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private DataTable GetPatientNotes(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            string strSQL = "";
            DataTable dtNotes = new DataTable();

            try
            {
                oDB.Connect(false);
                strSQL = "SELECT sNotes FROM Patient_Notes WHERE (nPatientID = " + PatientID + ") AND (nClinicID = " + gloPMGlobal.ClinicID + ")";
                oDB.Retrive_Query(strSQL,out dtNotes);

                if (dtNotes != null)
                {
                    return dtNotes;
                }
                
                oDB.Disconnect();

                return null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                return null;
            }
            finally
            {
                if (oDB != null) {  oDB.Dispose(); }
            }
        }

        #endregion " Private Methods "

        #region " Radio Button Events "

        private void rdbActive_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbActive.Checked == true)
            {
                rdbActive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma",9,FontStyle.Bold);
                rdbInActive.Checked = false;
                bStatus = true;
            }
            else
            {
                rdbActive.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma",9,FontStyle.Regular);
            }

            if (rdbInActive.Checked == true)
            {
                rdbActive.Checked = false;
                bStatus = false;
            }
        }

        private void rdbInActive_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbInActive.Checked == true)
            {
                rdbInActive.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rdbInActive.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
            }
           
        }

        #endregion " Radio Button Events "

        private void c1PatientAlerts_SelChange(object sender, EventArgs e)
        {
            //if (ts_btnSave.Visible == true) 
            //{
            //    if (DialogResult.Yes == MessageBox.Show("Do you want to save the changes ?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            //    {
            //        ts_btnSave_Click(null, null);
            //    }
            //    else
            //    {
            //        txtAlertName.Text = "";
            //        txtAlertName.Tag = 0;
            //        ts_btnSave.Visible = false;
            //    }

            //}


            if (c1PatientAlerts != null && c1PatientAlerts.Rows.Count > 0)
            {
                if (c1PatientAlerts.RowSel > 0)
                {
                    int rowIndex = 0;
                    rowIndex = c1PatientAlerts.RowSel;

                    if (Convert.ToBoolean(c1PatientAlerts.GetData(rowIndex, COL_IS_SYSTEMALERT)) == false)
                    {
                        _AlertId = Convert.ToInt64(c1PatientAlerts.GetData(c1PatientAlerts.Row, COL_ALERTID));
                        txtAlertName.Tag = _AlertId;
                        txtAlertName.Text = Convert.ToString(c1PatientAlerts.GetData(c1PatientAlerts.Row, COL_ALERTNAME)).Trim();
                        if (Convert.ToBoolean(c1PatientAlerts.GetData(c1PatientAlerts.Row, COL_ALERTSTATUS)) == true)
                        { rdbActive.Checked = true; rdbInActive.Checked = false; }
                        else
                        { rdbInActive.Checked = true; rdbActive.Checked = false; }

                    }
                    else
                    {
                        //If is System defined
                        txtAlertName.Text = "";
                        txtAlertName.Tag = 0;

                    }
                }
            }
        }
        //Code Added by Mayuri:20091201
        //To display message while closing form whether to to save changes or not
        private bool CloseForm()
        {
            if (IsModified() == true)
            {
                DialogResult res = MessageBox.Show("Do you want to save changes to this record? ", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    Int64 _Intresult = 0;
                    if (txtAlertName.Text.Trim() != "")
                    {
                        if (_AlertId == 0)
                        {

                            _Intresult = SaveAlerts(_AlertId, txtAlertName.Text.Trim(), bStatus);
                            DesignGrid();
                            FillAlerts();
                            txtAlertName.Text = "";
                            txtAlertName.Tag = null;
                            rdbActive.Checked = true;
                            _AlertId = 0;
                        }
                        else
                        {
                            _Intresult = SaveAlerts(Convert.ToInt64(txtAlertName.Tag), txtAlertName.Text.Trim(), rdbActive.Checked);
                            DesignGrid();
                            FillAlerts();
                            c1PatientAlerts.RowSel = rowSel;
                            txtAlertName.Text = "";
                            txtAlertName.Tag = null;
                            rdbActive.Checked = true;
                            _AlertId = 0;
                        }

                        tsb_New.Enabled = true;
                        tsb_Modify.Enabled = true;
                        tsb_Delete.Enabled = true;
                        ts_btnSave.Enabled = false;
                        tls_btnSave.Enabled = false;
                        ts_btnClose.Visible = true;
                        ts_btnCancel.Visible = false;

                        rdbActive.Enabled = false;
                        rdbInActive.Enabled = false;

                        txtAlertName.Enabled = false;
                        this.Close();
                        return true;
                    }
                  
                                      
                }
                if (res == DialogResult.No)
                {
                    this.Close();
                    return true;
                }
                if (res == DialogResult.Cancel)
                {
                    return false;
                }
                return true;
            }
            else
            {
                this.Close();
            }
            return true;
        }
        //End code Added by Mayuri:20091201

        private void ts_btnCancel_Click(object sender, EventArgs e)
        {
           //Code Added by Mayuri:200911201
            //While closing form display message whether to save changes or not
            CloseForm();
            //Code Added by Mayuri:20091201
            
           
            //End code Added by Mayuri:20091201
            tsb_New.Enabled = true;
            tsb_Modify.Enabled = true;
            tsb_Delete.Enabled = true;
            ts_btnSave.Enabled = false;
            tls_btnSave.Enabled = false;
            ts_btnClose.Visible = true;
            ts_btnCancel.Visible = false;

            rdbActive.Enabled = false;
            rdbInActive.Enabled = false;

            txtAlertName.Enabled = false;
            
        }

        private void c1PatientAlerts_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

        }

        private void tls_btnSave_Click(object sender, EventArgs e)
        {
            Int64 _Intresult = 0;
            try
            {

                if (txtAlertName.Text.Trim() != "")
                {
                    if (_AlertId == 0)
                    {

                        _Intresult = SaveAlerts(_AlertId, txtAlertName.Text.Trim(), bStatus);
                        DesignGrid();
                        FillAlerts();
                        txtAlertName.Text = "";
                        txtAlertName.Tag = null;
                        rdbActive.Checked = true;
                        _AlertId = 0;
                    }
                    else
                    {
                        _Intresult = SaveAlerts(Convert.ToInt64(txtAlertName.Tag), txtAlertName.Text.Trim(), rdbActive.Checked);
                        DesignGrid();
                        FillAlerts();
                        c1PatientAlerts.RowSel = rowSel;
                        txtAlertName.Text = "";
                        txtAlertName.Tag = null;
                        rdbActive.Checked = true;
                        _AlertId = 0;
                    }

                    tsb_New.Enabled = true;
                    tsb_Modify.Enabled = true;
                    tsb_Delete.Enabled = true;
                    ts_btnSave.Enabled = false;
                    tls_btnSave.Enabled = false;
                    ts_btnClose.Visible = true;
                    ts_btnCancel.Visible = false;

                    rdbActive.Enabled = false;
                    rdbInActive.Enabled = false;

                    txtAlertName.Enabled = false;
                    _IsPatAlertModifyed = true;
                    
                }
                else
                {
                    MessageBox.Show("Enter an alert description.", gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ts_btnSave.Visible = true;
                    tls_btnSave.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void frmPatientAlerts_FormClosed(object sender, FormClosedEventArgs e)
        {
            gloSettings.DatabaseSetting.DataBaseSetting oSettings = new gloSettings.DatabaseSetting.DataBaseSetting();
            try
            {
                if (_IsPatAlertModifyed)
                {
                    //oSettings.WriteSettings_XML("PatientAlert", "IsPatientAlertModified", Boolean.TrueString);
                    gloGlobal.gloPMGlobal.PatientAlertIsPatientAlertModified = true;
                }
                else
                {
                    //oSettings.WriteSettings_XML("PatientAlert", "IsPatientAlertModified", Boolean.FalseString);
                    gloGlobal.gloPMGlobal.PatientAlertIsPatientAlertModified = false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

      
    }
}