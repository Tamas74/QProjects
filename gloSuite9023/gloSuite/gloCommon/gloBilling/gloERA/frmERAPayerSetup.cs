using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using gloAuditTrail;

namespace gloBilling.gloERA
{
    public partial class frmERAPayerSetup : Form
    {

        #region " Variable Declaration "
        private Font oFontRegular = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        private Font oFontBold = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
        private Font oC1Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

        private string _DataBaseConnectionString = "";
        private Int64 _ClinicID = 1;
        private Int64 _UserID = 0;
        private string _MessageBoxCaption = "";

        private gloDatabaseLayer.DBLayer oDB = null;
        private gloDatabaseLayer.DBParameters oDBPara = null;
        private gloListControl.gloListControl oListControl = null;
        string _TempString = "";
        //  Int64 _TempID = 0;

        private Int64 _SettingID = 0;
        private ERAPayer oPayer = null;
        private DataTable _dt = null;
        private bool IsPayerModified = false;
        private bool IsLoading = true;
        private bool IsSaveClicked = false;
        private bool IsLineRemoved = false;        
        int C1StandardCASRowCount = 0;
        CASLine _RemoveLine;        
        CASLine _TempCAS;        
        
        #region " C1 Constants "

        private const int COL_STD_LINEID = 0;
        private const int COL_STD_GROUP = 1;
        private const int COL_STD_REASON = 2;
        private const int COL_STD_CASTYPE = 3;
        private const int COL_STD_DEFAULT = 4;
        private const int COL_STD_ALLOWMODIFY = 5;
        private const int COL_STD_COUNT = 6;

        private const int COL_OTHER_LINEID = 0;
        private const int COL_OTHER_GROUP = 1;
        private const int COL_OTHER_REASON = 2;
        private const int COL_OTHER_PAIDACTION = 3;
        private const int COL_OTHER_ZEROPAIDACTION = 4;
        private const int COL_OTHER_ALLOWMODIFY = 5;
        private const int COL_OTHER_COUNT = 6;

        #endregion

        //private enum enum_ActivePanel
        //{
        //    None = 0,
        //    Standard_CAS = 1,
        //    Other_CAS = 2
        //}
        //enum_ActivePanel _ActivePanel;

        #endregion

        #region " Properties "
        #endregion

        #region " Constructor "
        public frmERAPayerSetup(Int64 nSettingID)
        {
            _SettingID = nSettingID;

            InitializeComponent();

            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            _DataBaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _UserID = gloGlobal.gloPMGlobal.UserID;
            _MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
        }
        #endregion

        #region " Form Events "

        private void frmERAPayerSetup_Load(object sender, EventArgs e)
        {
            try
            {
                #region " C1 Design "
                //gloC1FlexStyle.Style(C1StandardCAS, false);
                //gloC1FlexStyle.Style(C1OtherCAS, false);

                C1.Win.C1FlexGrid.CellStyle csStdGroupCodes;// = C1StandardCAS.Styles.Add("csGroupCodes");
                try
                {
                    if (C1StandardCAS.Styles.Contains("csGroupCodes"))
                    {
                        csStdGroupCodes = C1StandardCAS.Styles["csGroupCodes"];
                    }
                    else
                    {
                        csStdGroupCodes = C1StandardCAS.Styles.Add("csGroupCodes");
                        csStdGroupCodes.DataType = typeof(System.String);
                        csStdGroupCodes.Font = oC1Font;
                        csStdGroupCodes.BackColor = Color.White;
                    }

                }
                catch
                {
                    csStdGroupCodes = C1StandardCAS.Styles.Add("csGroupCodes");
                    csStdGroupCodes.DataType = typeof(System.String);
                    csStdGroupCodes.Font = oC1Font;
                    csStdGroupCodes.BackColor = Color.White;

                }

                csStdGroupCodes.ComboList = GetGroupCodeString();

                C1.Win.C1FlexGrid.CellStyle csOtherGroupCodes;// = C1OtherCAS.Styles.Add("csGroupCodes");
                try
                {
                    if (C1OtherCAS.Styles.Contains("csGroupCodes"))
                    {
                        csOtherGroupCodes = C1OtherCAS.Styles["csGroupCodes"];
                    }
                    else
                    {
                        csOtherGroupCodes = C1OtherCAS.Styles.Add("csGroupCodes");
                        csOtherGroupCodes.DataType = typeof(System.String);
                        csOtherGroupCodes.Font = oC1Font;
                        csOtherGroupCodes.BackColor = Color.White;
                    }

                }
                catch
                {
                    csOtherGroupCodes = C1OtherCAS.Styles.Add("csGroupCodes");
                    csOtherGroupCodes.DataType = typeof(System.String);
                    csOtherGroupCodes.Font = oC1Font;
                    csOtherGroupCodes.BackColor = Color.White;

                }

                csOtherGroupCodes.ComboList = GetGroupCodeString();

                C1.Win.C1FlexGrid.CellStyle csCASType;// = C1StandardCAS.Styles.Add("csCASType");
                try
                {
                    if (C1StandardCAS.Styles.Contains("csCASType"))
                    {
                        csCASType = C1StandardCAS.Styles["csCASType"];
                    }
                    else
                    {
                        csCASType = C1StandardCAS.Styles.Add("csCASType");
                        csCASType.DataType = typeof(System.String);
                        csCASType.Font = oC1Font;
                        csCASType.BackColor = Color.White;
                    }

                }
                catch
                {
                    csCASType = C1StandardCAS.Styles.Add("csCASType");
                    csCASType.DataType = typeof(System.String);
                    csCASType.Font = oC1Font;
                    csCASType.BackColor = Color.White;

                }

                csCASType.ComboList = "|Coins|Copay|Deduct|Prev Paid|WH|W/O";

                C1.Win.C1FlexGrid.CellStyle csPaidAction;// = C1OtherCAS.Styles.Add("csPaidAction");
                try
                {
                    if (C1OtherCAS.Styles.Contains("csPaidAction"))
                    {
                        csPaidAction = C1OtherCAS.Styles["csPaidAction"];
                    }
                    else
                    {
                        csPaidAction = C1OtherCAS.Styles.Add("csPaidAction");
                        csPaidAction.DataType = typeof(System.String);
                        csPaidAction.Font = oC1Font;
                        csPaidAction.BackColor = Color.White;
                    }

                }
                catch
                {
                    csPaidAction = C1OtherCAS.Styles.Add("csPaidAction");
                    csPaidAction.DataType = typeof(System.String);
                    csPaidAction.Font = oC1Font;
                    csPaidAction.BackColor = Color.White;

                }

                csPaidAction.ComboList = "|Post & Transfer|Post & No Transfer|Stop Post";

                // Code Added for adding denial action to ZeroPaidAction
                C1.Win.C1FlexGrid.CellStyle csZeroPaidAction;// = C1OtherCAS.Styles.Add("csPaidAction");
                try
                {
                    if (C1OtherCAS.Styles.Contains("csZeroPaidAction"))
                    {
                        csZeroPaidAction = C1OtherCAS.Styles["csZeroPaidAction"];
                    }
                    else
                    {
                        csZeroPaidAction = C1OtherCAS.Styles.Add("csZeroPaidAction");
                        csZeroPaidAction.DataType = typeof(System.String);
                        csZeroPaidAction.Font = oC1Font;
                        csZeroPaidAction.BackColor = Color.White;
                    }

                }
                catch
                {
                    csZeroPaidAction = C1OtherCAS.Styles.Add("csZeroPaidAction");
                    csZeroPaidAction.DataType = typeof(System.String);
                    csZeroPaidAction.Font = oC1Font;
                    csZeroPaidAction.BackColor = Color.White;

                }

                csZeroPaidAction.ComboList = "|Post & Transfer|Post & No Transfer|Stop Post|Denial";





                DesignStandardCAS();
                DesignOtherCAS();
                #endregion

                //SelectStandardCASPanel();
                FillOtherReasonDefaultAction();
                chkUseClaimStatus.Checked = true;
                chkSecondaryAdjst.Checked = true;

                if (_SettingID == 0)
                {
                    FillDefaultCAS();
                }
                else
                {
                    FillPayerCAS(_SettingID, false);
                }

                IsLoading = false;
                C1StandardCAS.SetupEditor += new C1.Win.C1FlexGrid.RowColEventHandler(C1StandardCAS_SetupEditor);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        void C1StandardCAS_SetupEditor(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (e.Col == COL_STD_REASON)
            {
                var tb = C1StandardCAS.Editor as TextBoxBase;
                if (tb != null)
                {
                    tb.MaxLength = 10;
                }
            }
        }

        private void frmERAPayerSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!IsSaveClicked && IsPayerModified)
                {
                    DialogResult _Result = MessageBox.Show("Do you want to save changes?", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (_Result == DialogResult.Cancel)
                        e.Cancel = true;
                    else if (_Result == DialogResult.Yes)
                    {
                        if (SavePayer())
                            this.DialogResult = DialogResult.Yes;
                        else
                            e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void frmERAPayerSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //if (oFontRegular != null) { oFontRegular.Dispose(); oFontRegular = null; }
                //if (oFontBold != null) { oFontBold.Dispose(); oFontBold = null; }
                //if (oC1Font != null) { oC1Font.Dispose(); oC1Font = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oDBPara != null) { oDBPara.Dispose(); oDBPara = null; }
                if (oListControl != null) { oListControl.Dispose(); oListControl = null; }
                if (oPayer != null) { oPayer.Dispose(); oPayer = null; }
                if (_dt != null) { _dt.Dispose(); _dt = null; }
                C1StandardCAS.SetupEditor -= new C1.Win.C1FlexGrid.RowColEventHandler(C1StandardCAS_SetupEditor);
            }
            catch { }
        }

        #endregion

        #region " Public Methods "
        #endregion

        #region " Private Methods "

        #region " Open/Close Database Connection "

        private bool OpenConnection(bool withParameters)
        {
            bool _Result = false;
            try
            {
                if (_DataBaseConnectionString != "")
                {
                    oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                    oDB.Connect(false);
                    if (withParameters)
                        oDBPara = new gloDatabaseLayer.DBParameters();
                    _Result = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _Result;
        }

        private void CloseConnection()
        {
            if (oDB != null)
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
            if (oDBPara != null)
            {
                oDBPara.Dispose();
                oDBPara = null;
            }
        }

        #endregion

        private void DesignStandardCAS()
        {
            C1StandardCAS.Rows.Count = 1;
            C1StandardCAS.Rows.Fixed = 1;
            C1StandardCAS.Cols.Count = COL_STD_COUNT;

            C1StandardCAS.SetData(0, COL_STD_GROUP, "Group");
            C1StandardCAS.SetData(0, COL_STD_REASON, "Reason");
            C1StandardCAS.SetData(0, COL_STD_CASTYPE, "Type");

            C1StandardCAS.Cols[COL_STD_LINEID].Visible = false;
            C1StandardCAS.Cols[COL_STD_DEFAULT].Visible = false;
            C1StandardCAS.Cols[COL_STD_ALLOWMODIFY].Visible = false;

            int _Width = C1StandardCAS.Width;
            C1StandardCAS.Cols[COL_STD_GROUP].Width = (int)(_Width * 0.3);
            C1StandardCAS.Cols[COL_STD_REASON].Width = (int)(_Width * 0.3);
            C1StandardCAS.Cols[COL_STD_CASTYPE].Width = (int)(_Width * 0.3) + 60;

            C1StandardCAS.Cols[COL_STD_GROUP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1StandardCAS.Cols[COL_STD_REASON].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1StandardCAS.Cols[COL_STD_CASTYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        }

        private void DesignOtherCAS()
        {
            C1OtherCAS.Rows.Count = 1;
            C1OtherCAS.Rows.Fixed = 1;
            C1OtherCAS.Cols.Count = COL_OTHER_COUNT;

            C1OtherCAS.SetData(0, COL_OTHER_GROUP, "Group");
            C1OtherCAS.SetData(0, COL_OTHER_REASON, "Reason");
            C1OtherCAS.SetData(0, COL_OTHER_PAIDACTION, "Paid Action");
            C1OtherCAS.SetData(0, COL_OTHER_ZEROPAIDACTION, "Zero Paid Action");

            C1OtherCAS.Cols[COL_OTHER_LINEID].Visible = false;
            C1OtherCAS.Cols[COL_OTHER_ALLOWMODIFY].Visible = false;

            int _Width = C1StandardCAS.Width;
            C1OtherCAS.Cols[COL_OTHER_GROUP].Width = (int)(_Width * 0.2);
            C1OtherCAS.Cols[COL_OTHER_REASON].Width = (int)(_Width * 0.2);
            C1OtherCAS.Cols[COL_OTHER_PAIDACTION].Width = (int)(_Width * 0.3);
            C1OtherCAS.Cols[COL_OTHER_ZEROPAIDACTION].Width = (int)(_Width * 0.3);

            C1OtherCAS.Cols[COL_OTHER_GROUP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1OtherCAS.Cols[COL_OTHER_REASON].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1OtherCAS.Cols[COL_OTHER_PAIDACTION].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            C1OtherCAS.Cols[COL_OTHER_ZEROPAIDACTION].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        }

        private void FillPayerCAS(Int64 nSettingID, bool FillCASOnly)
        {
            using (oPayer = new ERAPayer())
            {
                oPayer.GetPayer(nSettingID);

                if (!FillCASOnly) // IF NOT IMPORTING PAYER //
                {
                    txtPayerID.Text = oPayer.PayerID;
                    chkActivate.Checked = oPayer.IsActive;
                    chkUseClaimStatus.Checked = oPayer.UseClaimStatus;
                    chkSecondaryAdjst.Checked = oPayer.PostSecondaryAdjust;
                    cmbZBilled.SelectedValue = oPayer.ZeroPaidBilled.GetHashCode();
                    cmbZNotBilled.SelectedValue = oPayer.ZeroPaidNotBilled.GetHashCode();
                    cmbPaid.SelectedValue = oPayer.PaidNotZero.GetHashCode();
                }

                C1StandardCAS.Rows.Count = 1;
                C1OtherCAS.Rows.Count = 1;

                int _RowIndex = -1;
                for (int i = 0; i < oPayer.CASLines.Count; i++)
                {
                    if (oPayer.CASLines[i].CASReasonType != enum_CASReasonType.Other)
                    {
                        // FILL STANDARD REASON CODES //
                        C1StandardCAS.Rows.Add();
                        _RowIndex = C1StandardCAS.Rows.Count - 1;

                        // Don't allow user to modify GroupCode for already saved CAS or Default CAS // 
                        // C1StandardCAS.SetCellStyle(_RowIndex, COL_STD_GROUP, C1StandardCAS.Styles["csGroupCodes"]);
                        C1StandardCAS.SetCellStyle(_RowIndex, COL_STD_CASTYPE, C1StandardCAS.Styles["csCASType"]);

                        C1StandardCAS.SetData(_RowIndex, COL_STD_LINEID, oPayer.CASLines[i].CASID);
                        C1StandardCAS.SetData(_RowIndex, COL_STD_GROUP, oPayer.CASLines[i].GroupCode);
                        C1StandardCAS.SetData(_RowIndex, COL_STD_REASON, oPayer.CASLines[i].ReasonCode);
                        if (oPayer.CASLines[i].CASReasonType == enum_CASReasonType.WO)
                            C1StandardCAS.SetData(_RowIndex, COL_STD_CASTYPE, "W/O");
                        else if (oPayer.CASLines[i].CASReasonType == enum_CASReasonType.PrevPaid)
                            C1StandardCAS.SetData(_RowIndex, COL_STD_CASTYPE, "Prev Paid");
                        else
                            C1StandardCAS.SetData(_RowIndex, COL_STD_CASTYPE, oPayer.CASLines[i].CASReasonType);
                        C1StandardCAS.SetData(_RowIndex, COL_STD_DEFAULT, oPayer.CASLines[i].IsDefault);
                        C1StandardCAS.SetData(_RowIndex, COL_STD_ALLOWMODIFY, false);
                    }
                    else
                    {
                        // FILL OTHER REASON CODES //
                        C1OtherCAS.Rows.Add();
                        _RowIndex = C1OtherCAS.Rows.Count - 1;


                        // Don't allow user to modify GroupCode for already saved CAS or Default CAS // 
                        // C1OtherCAS.SetCellStyle(_RowIndex, COL_OTHER_GROUP, C1OtherCAS.Styles["csGroupCodes"]);
                        C1OtherCAS.SetCellStyle(_RowIndex, COL_OTHER_PAIDACTION, C1OtherCAS.Styles["csPaidAction"]);
                        C1OtherCAS.SetCellStyle(_RowIndex, COL_OTHER_ZEROPAIDACTION, C1OtherCAS.Styles["csZeroPaidAction"]);

                        C1OtherCAS.SetData(_RowIndex, COL_OTHER_LINEID, oPayer.CASLines[i].CASID);
                        C1OtherCAS.SetData(_RowIndex, COL_OTHER_GROUP, oPayer.CASLines[i].GroupCode);
                        C1OtherCAS.SetData(_RowIndex, COL_OTHER_REASON, oPayer.CASLines[i].ReasonCode);
                        if (oPayer.CASLines[i].PaidAction == enum_PayAction.PostTransfer)
                            C1OtherCAS.SetData(_RowIndex, COL_OTHER_PAIDACTION, "Post & Transfer");
                        else if (oPayer.CASLines[i].PaidAction == enum_PayAction.PostNoTransfer)
                            C1OtherCAS.SetData(_RowIndex, COL_OTHER_PAIDACTION, "Post & No Transfer");
                        else if (oPayer.CASLines[i].PaidAction == enum_PayAction.StopPost)
                            C1OtherCAS.SetData(_RowIndex, COL_OTHER_PAIDACTION, "Stop Post");

                        if (oPayer.CASLines[i].ZeroPaidAction == enum_PayAction.PostTransfer)
                            C1OtherCAS.SetData(_RowIndex, COL_OTHER_ZEROPAIDACTION, "Post & Transfer");
                        else if (oPayer.CASLines[i].ZeroPaidAction == enum_PayAction.PostNoTransfer)
                            C1OtherCAS.SetData(_RowIndex, COL_OTHER_ZEROPAIDACTION, "Post & No Transfer");
                        else if (oPayer.CASLines[i].ZeroPaidAction == enum_PayAction.StopPost)
                            C1OtherCAS.SetData(_RowIndex, COL_OTHER_ZEROPAIDACTION, "Stop Post");
                        else if (oPayer.CASLines[i].ZeroPaidAction == enum_PayAction.Denial)
                            C1OtherCAS.SetData(_RowIndex, COL_OTHER_ZEROPAIDACTION, "Denial");

                        C1OtherCAS.SetData(_RowIndex, COL_OTHER_ALLOWMODIFY, false);
                    }

                }
            }
        }

        private void FillDefaultCAS()
        {
            try
            {
                using (oPayer = new ERAPayer())
                {
                    _dt = oPayer.GetDefaultCASCodes();
                    if (_dt != null)
                    {
                        int _RowIndex = -1;
                        for (int iRow = 0; iRow < _dt.Rows.Count; iRow++)
                        {
                            C1StandardCAS.Rows.Add();
                            _RowIndex = C1StandardCAS.Rows.Count - 1;

                            // Don't allow user to modify GroupCode for already saved CAS or Default CAS // 
                            // C1StandardCAS.SetCellStyle(_RowIndex, COL_STD_GROUP, C1StandardCAS.Styles["csGroupCodes"]);
                            C1StandardCAS.SetCellStyle(_RowIndex, COL_STD_CASTYPE, C1StandardCAS.Styles["csCASType"]);

                            C1StandardCAS.SetData(_RowIndex, COL_STD_LINEID, 0);
                            C1StandardCAS.SetData(_RowIndex, COL_STD_GROUP, _dt.Rows[iRow]["GroupCode"].ToString());
                            C1StandardCAS.SetData(_RowIndex, COL_STD_REASON, _dt.Rows[iRow]["ReasonCode"].ToString());
                            C1StandardCAS.SetData(_RowIndex, COL_STD_CASTYPE, _dt.Rows[iRow]["CASTypeDesc"]);
                            C1StandardCAS.SetData(_RowIndex, COL_STD_DEFAULT, true);
                            C1StandardCAS.SetData(_RowIndex, COL_STD_ALLOWMODIFY, false);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private string GetGroupCodeString()
        {
            object oResult = null;
            string _Result = "";
            if (OpenConnection(true))
            {
                oDBPara.Clear();
                oDBPara.Add("@GroupString", "", ParameterDirection.InputOutput, SqlDbType.VarChar);
                oResult = oDB.ExecuteScalar("ERA_GetGroupCodeString", oDBPara);
                if (oResult != null && oResult.ToString() != "")
                    _Result = oResult.ToString();
                CloseConnection();
            }
            return _Result;
        }

        private void FillOtherReasonDefaultAction()
        {
            try
            {
                DataTable _Action = new DataTable();
                DataRow _Row;
                _Action.Columns.Add("Key");
                _Action.Columns.Add("Action");
                _Row = _Action.NewRow(); _Row["Key"] = 1; _Row["Action"] = "Post & Transfer"; _Action.Rows.Add(_Row);
                _Row = _Action.NewRow(); _Row["Key"] = 2; _Row["Action"] = "Post & No Transfer"; _Action.Rows.Add(_Row);
                _Row = _Action.NewRow(); _Row["Key"] = 3; _Row["Action"] = "Stop Post"; _Action.Rows.Add(_Row);

                _dt = _Action.Clone();
                _dt.Merge(_Action);

                _Row = _dt.NewRow(); _Row["Key"] = 4; _Row["Action"] = "Denial"; _dt.Rows.Add(_Row);
                cmbZBilled.DataSource = _dt;
                cmbZBilled.DisplayMember = "Action";
                cmbZBilled.ValueMember = "Key";
                cmbZBilled.SelectedValue = 3;

                _dt = _Action.Clone();
                _dt.Merge(_Action);

                _Row = _dt.NewRow(); _Row["Key"] = 4; _Row["Action"] = "Denial"; _dt.Rows.Add(_Row);
                cmbZNotBilled.DataSource = _dt;
                cmbZNotBilled.DisplayMember = "Action";
                cmbZNotBilled.ValueMember = "Key";
                cmbZNotBilled.SelectedValue = 1;

                _dt = _Action.Clone();
                _dt.Merge(_Action);
                cmbPaid.DataSource = _dt;
                cmbPaid.DisplayMember = "Action";
                cmbPaid.ValueMember = "Key";
                cmbPaid.SelectedValue = 1;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private bool SavePayer()
        {
            bool _Result = false;
            CASLine _CASLine = null;
            try
            {
                if (!ValidatePayer())
                    return false;

                #region " Set Payer Object "
                using (oPayer = new ERAPayer())
                {
                    #region " Set Payer Defaults "
                    oPayer.PayerID = txtPayerID.Text.Trim();
                    oPayer.IsActive = chkActivate.Checked;
                    oPayer.UseClaimStatus = chkUseClaimStatus.Checked;
                    oPayer.PostSecondaryAdjust = chkSecondaryAdjst.Checked;
                    #endregion

                    #region " Set Other CAS Defaults "
                    oPayer.ZeroPaidBilled = (enum_PayAction)Convert.ToInt16(cmbZBilled.SelectedValue);
                    oPayer.ZeroPaidNotBilled = (enum_PayAction)Convert.ToInt16(cmbZNotBilled.SelectedValue);
                    oPayer.PaidNotZero = (enum_PayAction)Convert.ToInt16(cmbPaid.SelectedValue);
                    #endregion

                    #region " Set Standard CAS "
                    for (int iRow = 1; iRow < C1StandardCAS.Rows.Count; iRow++)
                    {
                        _CASLine = new CASLine();                        
                        if (C1StandardCAS.GetData(iRow, COL_STD_GROUP) != null)
                            _CASLine.GroupCode = C1StandardCAS.GetData(iRow, COL_STD_GROUP).ToString();

                        if (C1StandardCAS.GetData(iRow, COL_STD_REASON) != null)
                            _CASLine.ReasonCode = C1StandardCAS.GetData(iRow, COL_STD_REASON).ToString();

                        if (C1StandardCAS.GetData(iRow, COL_STD_CASTYPE) != null)
                            switch (C1StandardCAS.GetData(iRow, COL_STD_CASTYPE).ToString())
                            {
                                case "W/O": { _CASLine.CASReasonType = enum_CASReasonType.WO; break; }
                                case "WH": { _CASLine.CASReasonType = enum_CASReasonType.WH; break; }
                                case "Deduct": { _CASLine.CASReasonType = enum_CASReasonType.Deduct; break; }
                                case "Coins": { _CASLine.CASReasonType = enum_CASReasonType.Coins; break; }
                                case "Copay": { _CASLine.CASReasonType = enum_CASReasonType.Copay; break; }
                                case "Prev Paid": { _CASLine.CASReasonType = enum_CASReasonType.PrevPaid; break; }
                            }

                        _CASLine.PaidAction = enum_PayAction.None;
                        _CASLine.ZeroPaidAction = enum_PayAction.None;

                        if (C1StandardCAS.GetData(iRow, COL_STD_DEFAULT) != null)
                            _CASLine.IsDefault = Convert.ToBoolean(C1StandardCAS.GetData(iRow, COL_STD_DEFAULT).ToString());

                        oPayer.CASLines.Add(_CASLine);
                        
                    }
                    if (C1StandardCASRowCount != 0 && C1StandardCASRowCount != C1StandardCAS.Rows.Count) { _TempCAS = new CASLine(); _TempCAS = _CASLine; }
                    #endregion

                    #region " Set Other CAS "
                    for (int iRow = 1; iRow < C1OtherCAS.Rows.Count; iRow++)
                    {
                        _CASLine = new CASLine();

                        if (C1OtherCAS.GetData(iRow, COL_OTHER_GROUP) != null)
                            _CASLine.GroupCode = C1OtherCAS.GetData(iRow, COL_OTHER_GROUP).ToString();

                        if (C1OtherCAS.GetData(iRow, COL_OTHER_REASON) != null)
                            _CASLine.ReasonCode = C1OtherCAS.GetData(iRow, COL_OTHER_REASON).ToString();

                        _CASLine.CASReasonType = enum_CASReasonType.Other;

                        if (C1OtherCAS.GetData(iRow, COL_OTHER_PAIDACTION) != null)
                            switch (C1OtherCAS.GetData(iRow, COL_OTHER_PAIDACTION).ToString())
                            {
                                case "Post & Transfer": { _CASLine.PaidAction = enum_PayAction.PostTransfer; break; }
                                case "Post & No Transfer": { _CASLine.PaidAction = enum_PayAction.PostNoTransfer; break; }
                                case "Stop Post": { _CASLine.PaidAction = enum_PayAction.StopPost; break; }
                            }

                        if (C1OtherCAS.GetData(iRow, COL_OTHER_ZEROPAIDACTION) != null)
                            switch (C1OtherCAS.GetData(iRow, COL_OTHER_ZEROPAIDACTION).ToString())
                            {
                                case "Post & Transfer": { _CASLine.ZeroPaidAction = enum_PayAction.PostTransfer; break; }
                                case "Post & No Transfer": { _CASLine.ZeroPaidAction = enum_PayAction.PostNoTransfer; break; }
                                case "Stop Post": { _CASLine.ZeroPaidAction = enum_PayAction.StopPost; break; }
                                case "Denial": { _CASLine.ZeroPaidAction = enum_PayAction.Denial; break; }
                            }

                        _CASLine.IsDefault = false;

                        oPayer.CASLines.Add(_CASLine);
                    }
                    #endregion

                    oPayer.SettingID = _SettingID;
                    if (oPayer.SavePayer() > 0)
                        _Result = true;

                }
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            return _Result;
        }

        private bool ValidatePayer()
        {
            if (txtPayerID.Text.Trim() == "")
            {
                MessageBox.Show("Enter Payer ID.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPayerID.Focus();
                return false;
            }

            using (oPayer = new ERAPayer())
            {
                if (oPayer.IsPayerPresent(txtPayerID.Text.Trim(), _SettingID))
                {
                    MessageBox.Show("ERA Payer ID already exist.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            if (C1StandardCAS.Rows.Count <= 1 && C1OtherCAS.Rows.Count <= 1)
            {
                MessageBox.Show("No reason codes entered for payer.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!ValidateCASLines(""))
                return false;

            return true;
        }

        private bool ValidateCASLines(string _SelectedTab)
        {
            #region " Standard CAS "
            if (_SelectedTab != "Other")
            {
                _TempString = "Enter complete standard reason code parameters.";
                for (int iRow = 1; iRow < C1StandardCAS.Rows.Count; iRow++)
                {
                    if (C1StandardCAS.GetData(iRow, COL_STD_GROUP) == null || C1StandardCAS.GetData(iRow, COL_STD_GROUP).ToString() == "")
                    {
                        tbCAS.SelectedIndex = 0;
                        C1StandardCAS.Select(iRow, COL_STD_GROUP);
                        C1StandardCAS.Select();
                        MessageBox.Show(_TempString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (C1StandardCAS.GetData(iRow, COL_STD_REASON) == null || C1StandardCAS.GetData(iRow, COL_STD_REASON).ToString() == "")
                    {
                        tbCAS.SelectedIndex = 0;
                        C1StandardCAS.Select(iRow, COL_STD_REASON);
                        C1StandardCAS.Select();
                        MessageBox.Show(_TempString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (C1StandardCAS.GetData(iRow, COL_STD_CASTYPE) == null || C1StandardCAS.GetData(iRow, COL_STD_CASTYPE).ToString() == "")
                    {
                        tbCAS.SelectedIndex = 0;
                        C1StandardCAS.Select(iRow, COL_STD_CASTYPE);
                        C1StandardCAS.Select();
                        MessageBox.Show(_TempString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                }
            }
            #endregion

            #region " Other CAS "
            if (_SelectedTab != "Standard")
            {
                _TempString = "Enter complete other reason code parameters.";
                for (int iRow = 1; iRow < C1OtherCAS.Rows.Count; iRow++)
                {
                    if (C1OtherCAS.GetData(iRow, COL_OTHER_GROUP) == null || C1OtherCAS.GetData(iRow, COL_OTHER_GROUP).ToString() == "")
                    {
                        tbCAS.SelectedIndex = 1;
                        C1OtherCAS.Select(iRow, COL_OTHER_GROUP);
                        C1OtherCAS.Select();
                        MessageBox.Show(_TempString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (C1OtherCAS.GetData(iRow, COL_OTHER_REASON) == null || C1OtherCAS.GetData(iRow, COL_OTHER_REASON).ToString() == "")
                    {
                        tbCAS.SelectedIndex = 1;
                        C1OtherCAS.Select(iRow, COL_OTHER_REASON);
                        C1OtherCAS.Select();
                        MessageBox.Show(_TempString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (C1OtherCAS.GetData(iRow, COL_OTHER_PAIDACTION) == null || C1OtherCAS.GetData(iRow, COL_OTHER_PAIDACTION).ToString() == "")
                    {
                        tbCAS.SelectedIndex = 1;
                        C1OtherCAS.Select(iRow, COL_OTHER_PAIDACTION);
                        C1OtherCAS.Select();
                        MessageBox.Show(_TempString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (C1OtherCAS.GetData(iRow, COL_OTHER_ZEROPAIDACTION) == null || C1OtherCAS.GetData(iRow, COL_OTHER_ZEROPAIDACTION).ToString() == "")
                    {
                        tbCAS.SelectedIndex = 1;
                        C1OtherCAS.Select(iRow, COL_OTHER_ZEROPAIDACTION);
                        C1OtherCAS.Select();
                        MessageBox.Show(_TempString, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                }
            }
            #endregion

            return true;
        }

        private void oListControl_PayerSelectedClick(object sender, EventArgs e)
        {
            try
            {
                if (oListControl.SelectedItems.Count > 0)
                {
                    for (Int16 i = 0; i <= oListControl.SelectedItems.Count - 1; i++)
                    {
                        FillPayerCAS(oListControl.SelectedItems[i].ID, true);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                pnlTab.Visible = true;
                pnlMST.Visible = true;
                pnlToolStrip.Visible = true;
            }
        }

        private void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            pnlTab.Visible = true;
            pnlMST.Visible = true;
            pnlToolStrip.Visible = true;
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
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_PayerSelectedClick);

                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                }
                catch { }

            }
        }
        #endregion

        #region " C1 Events "

        private void C1StandardCAS_MouseDown(object sender, MouseEventArgs e)
        {
            C1.Win.C1FlexGrid.HitTestInfo oHit = C1StandardCAS.HitTest(e.X, e.Y);
            if (oHit.Row <= 0)
                C1StandardCAS.Row = -1;
            else
                C1StandardCAS.Row = oHit.Row;

            //SelectStandardCASPanel();
        }

        private void C1OtherCAS_MouseDown(object sender, MouseEventArgs e)
        {
            C1.Win.C1FlexGrid.HitTestInfo oHit = C1OtherCAS.HitTest(e.X, e.Y);
            if (oHit.Row <= 0)
                C1OtherCAS.Row = -1;
            else
                C1OtherCAS.Row = oHit.Row;

            //SelectOtherCASPanel();
        }

        private void C1OtherCAS_BeforeRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                #region " Data Validation "
                if (e.OldRange.c1 == COL_OTHER_GROUP)
                {
                    if (!(e.OldRange.Data.ToString() == "CO" ||
                          e.OldRange.Data.ToString() == "CR" ||
                          e.OldRange.Data.ToString() == "PR" ||
                          e.OldRange.Data.ToString() == "PI" ||
                          e.OldRange.Data.ToString() == "OA" ||
                          e.OldRange.Data.ToString() == ""))
                    {
                        MessageBox.Show("Enter valid Group Code.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        e.Cancel = true;
                        C1OtherCAS.StartEditing(e.OldRange.r1, e.OldRange.c1);
                        return;
                    }
                }
                if (e.OldRange.c1 == COL_OTHER_PAIDACTION)
                {
                    if (!(e.OldRange.Data.ToString() == "Post & Transfer" ||
                          e.OldRange.Data.ToString() == "Post & No Transfer" ||
                          e.OldRange.Data.ToString() == "Stop Post" ||
                          e.OldRange.Data.ToString() == ""))
                    {
                        MessageBox.Show("Enter valid action.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        e.Cancel = true;
                        C1OtherCAS.StartEditing(e.OldRange.r1, e.OldRange.c1);
                        return;
                    }
                }

                if (e.OldRange.c1 == COL_OTHER_ZEROPAIDACTION)
                {
                    if (!(e.OldRange.Data.ToString() == "Post & Transfer" ||
                          e.OldRange.Data.ToString() == "Post & No Transfer" ||
                          e.OldRange.Data.ToString() == "Stop Post" ||
                           e.OldRange.Data.ToString() == "Denial" ||
                          e.OldRange.Data.ToString() == ""))
                    {
                        MessageBox.Show("Enter valid action.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        e.Cancel = true;
                        C1OtherCAS.StartEditing(e.OldRange.r1, e.OldRange.c1);
                        return;
                    }
                }
                #endregion

                #region " Duplicate Validation "
                if (e.OldRange.c1 == COL_OTHER_GROUP || e.OldRange.c1 == COL_OTHER_REASON)
                {
                    string _OtherString = "";
                    string _StandardString = "";

                    if (C1OtherCAS.GetData(e.OldRange.r1, COL_OTHER_GROUP) == null || C1OtherCAS.GetData(e.OldRange.r1, COL_OTHER_REASON) == null)
                        return;

                    _OtherString = C1OtherCAS.GetData(e.OldRange.r1, COL_OTHER_GROUP).ToString() + C1OtherCAS.GetData(e.OldRange.r1, COL_OTHER_REASON).ToString();

                    for (int iRow = 1; iRow < C1StandardCAS.Rows.Count; iRow++)
                    {
                        _StandardString = "";
                        if (C1StandardCAS.GetData(iRow, COL_STD_GROUP) == null || C1StandardCAS.GetData(iRow, COL_STD_REASON) == null)
                            continue;

                        _StandardString = C1StandardCAS.GetData(iRow, COL_STD_GROUP).ToString() + C1StandardCAS.GetData(iRow, COL_STD_REASON).ToString();

                        if (_OtherString.ToUpper().Trim() == _StandardString.ToUpper().Trim())
                        {
                            MessageBox.Show(_StandardString + " is a Standard Reason Code." + Environment.NewLine + "Standard Reason Codes are not permitted Action Overrides.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                            C1OtherCAS.StartEditing(e.OldRange.r1, e.OldRange.c1);
                            return;
                        }
                    }
                }
                #endregion
            }
            catch { }
        }

        private void C1StandardCAS_BeforeRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                #region " Data Validation "
                if (e.OldRange.c1 == COL_STD_GROUP)
                {
                    if (!(e.OldRange.Data.ToString() == "CO" ||
                          e.OldRange.Data.ToString() == "CR" ||
                          e.OldRange.Data.ToString() == "PR" ||
                          e.OldRange.Data.ToString() == "PI" ||
                          e.OldRange.Data.ToString() == "OA" ||
                          e.OldRange.Data.ToString() == ""))
                    {
                        MessageBox.Show("Enter valid Group Code.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        e.Cancel = true;
                        C1StandardCAS.StartEditing(e.OldRange.r1, e.OldRange.c1);
                        return;
                    }
                }
                if (e.OldRange.c1 == COL_STD_CASTYPE)
                {
                    if (!(e.OldRange.Data.ToString() == "Coins" ||
                          e.OldRange.Data.ToString() == "Copay" ||
                          e.OldRange.Data.ToString() == "Deduct" ||
                          e.OldRange.Data.ToString() == "Prev Paid" ||
                          e.OldRange.Data.ToString() == "WH" ||
                          e.OldRange.Data.ToString() == "W/O" ||
                          e.OldRange.Data.ToString() == ""))
                    {
                        MessageBox.Show("Enter valid Type.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        e.Cancel = true;
                        C1StandardCAS.StartEditing(e.OldRange.r1, e.OldRange.c1);
                        return;
                    }
                }
                #endregion

                #region " Duplicate Validation "
                if (e.OldRange.c1 == COL_STD_GROUP || e.OldRange.c1 == COL_STD_REASON)
                {
                    string _StandardString = "";
                    string _OtherString = "";

                    if (C1StandardCAS.GetData(e.OldRange.r1, COL_STD_GROUP) == null || C1StandardCAS.GetData(e.OldRange.r1, COL_STD_REASON) == null)
                        return;

                    _StandardString = C1StandardCAS.GetData(e.OldRange.r1, COL_STD_GROUP).ToString() + C1StandardCAS.GetData(e.OldRange.r1, COL_STD_REASON).ToString();

                    for (int iRow = 1; iRow < C1OtherCAS.Rows.Count; iRow++)
                    {
                        _OtherString = "";
                        if (C1OtherCAS.GetData(iRow, COL_OTHER_GROUP) == null || C1OtherCAS.GetData(iRow, COL_OTHER_REASON) == null)
                            continue;

                        _OtherString = C1OtherCAS.GetData(iRow, COL_OTHER_GROUP).ToString() + C1OtherCAS.GetData(iRow, COL_OTHER_REASON).ToString();

                        if (_OtherString.ToUpper().Trim() == _StandardString.ToUpper().Trim())
                        {
                            MessageBox.Show(_OtherString + " is an Other Reason Code." + Environment.NewLine + "Other Reason Codes cannot also be Standard Reason Codes.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                            C1StandardCAS.StartEditing(e.OldRange.r1, e.OldRange.c1);
                            return;
                        }
                    }
                }
                #endregion

            }
            catch { }
        }

        private void C1OtherCAS_Leave(object sender, EventArgs e)
        {
            C1OtherCAS.Select(0, 0);
        }

        private void C1StandardCAS_Leave(object sender, EventArgs e)
        {
            C1StandardCAS.Select(0, 0);
        }

        private void C1StandardCAS_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            try
            {
                if (e.Col == COL_STD_GROUP || e.Col == COL_STD_CASTYPE)
                    e.Handled = true;
                else if (e.Col == COL_STD_REASON)
                {
                    if (C1StandardCAS.Editor.Text.Length >= 10 && e.KeyChar != 8) // KeyChar 8 = BACKSPACE // ALLOW TO BACKSPACE BUT STOP OTHERS //
                        e.Handled = true;
                }
            }
            catch { }
        }

        private void C1OtherCAS_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            try
            {
                if (e.Col == COL_OTHER_GROUP || e.Col == COL_OTHER_PAIDACTION || e.Col == COL_OTHER_ZEROPAIDACTION)
                    e.Handled = true;
                else if (e.Col == COL_OTHER_REASON)
                {
                    if (C1OtherCAS.Editor.Text.Length >= 10 && e.KeyChar != 8) // KeyChar 8 = BACKSPACE // ALLOW TO BACKSPACE BUT STOP OTHERS //
                        e.Handled = true;
                }
            }
            catch { }
        }

        private void C1StandardCAS_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (e.Col == COL_STD_GROUP || e.Col == COL_STD_REASON)
                {
                    if (!Convert.ToBoolean(C1StandardCAS.GetData(e.Row, COL_STD_ALLOWMODIFY)))
                        e.Cancel = true;
                }
            }
            catch { }
        }

        private void C1OtherCAS_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                if (e.Col == COL_OTHER_GROUP || e.Col == COL_OTHER_REASON)
                {
                    if (!Convert.ToBoolean(C1OtherCAS.GetData(e.Row, COL_OTHER_ALLOWMODIFY)))
                        e.Cancel = true;
                }
            }
            catch { }
        }

        private void C1OtherCAS_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (!IsLoading) IsPayerModified = true;
        }
        #endregion

        #region " Control Design "
        //private void SelectStandardCASPanel()
        //{
        //    pnlStandardCASLabel.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
        //    pnlOtherCASLabel.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
        //    _ActivePanel = enum_ActivePanel.Standard_CAS;
        //    C1OtherCAS.Select(0, 0);
        //}
        //private void SelectOtherCASPanel()
        //{
        //    pnlOtherCASLabel.BackgroundImage = global::gloBilling.Properties.Resources.Img_LongOrange;
        //    pnlStandardCASLabel.BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
        //    _ActivePanel = enum_ActivePanel.Other_CAS;
        //    C1StandardCAS.Select(0, 0);
        //}
        private void lblStandardCAS_Click(object sender, EventArgs e)
        {
            //SelectStandardCASPanel();
        }
        private void lblOtherCAS_Click(object sender, EventArgs e)
        {
            //SelectOtherCASPanel();
        }
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsLoading) IsPayerModified = true;

                CheckBox _Chk = (CheckBox)sender;
                if (_Chk.Checked)
                    _Chk.Font = oFontBold;
                else
                    _Chk.Font = oFontRegular;
            }
            catch { }
        }
        private void txtPayerID_TextChanged(object sender, EventArgs e)
        {
            if (!IsLoading) IsPayerModified = true;
        }

        private void cmbAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsLoading) IsPayerModified = true;
        }

        private void C1StandardCAS_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (!IsLoading)  IsPayerModified = true;  
        }
        #endregion

        #region " ToolStrip Buttons Events "

        private void tsb_Add_Click(object sender, EventArgs e)
         {
            try
            {
                if (tbCAS.SelectedTab.Tag.ToString() == "Standard")  //if (_ActivePanel == enum_ActivePanel.Standard_CAS)
                {
                    if (ValidateCASLines("Standard"))
                    {
                        C1StandardCASRowCount = C1StandardCAS.Rows.Count;
                        C1StandardCAS.Rows.Add();
                        C1StandardCAS.SetCellStyle(C1StandardCAS.Rows.Count - 1, COL_STD_GROUP, C1StandardCAS.Styles["csGroupCodes"]);
                        C1StandardCAS.SetCellStyle(C1StandardCAS.Rows.Count - 1, COL_STD_CASTYPE, C1StandardCAS.Styles["csCASType"]);
                        C1StandardCAS.SetData(C1StandardCAS.Rows.Count - 1, COL_STD_DEFAULT, false);
                        C1StandardCAS.SetData(C1StandardCAS.Rows.Count - 1, COL_STD_ALLOWMODIFY, true);
                        C1StandardCAS.Select(C1StandardCAS.Rows.Count - 1, COL_STD_GROUP);
                        C1StandardCAS.Select();
                    }
                }
                else if (tbCAS.SelectedTab.Tag.ToString() == "Other")  //if (_ActivePanel == enum_ActivePanel.Other_CAS)
                {
                    if (ValidateCASLines("Other"))
                    {
                        C1OtherCAS.Rows.Add();
                        C1OtherCAS.SetData(C1OtherCAS.Rows.Count - 1, COL_OTHER_ALLOWMODIFY, true);
                        C1OtherCAS.SetCellStyle(C1OtherCAS.Rows.Count - 1, COL_OTHER_GROUP, C1OtherCAS.Styles["csGroupCodes"]);
                        C1OtherCAS.SetCellStyle(C1OtherCAS.Rows.Count - 1, COL_OTHER_PAIDACTION, C1OtherCAS.Styles["csPaidAction"]);
                        C1OtherCAS.SetCellStyle(C1OtherCAS.Rows.Count - 1, COL_OTHER_ZEROPAIDACTION, C1OtherCAS.Styles["csZeroPaidAction"]);
                        C1OtherCAS.Select(C1OtherCAS.Rows.Count - 1, COL_OTHER_GROUP);
                        C1OtherCAS.Select();
                    }
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_Remove_Click(object sender, EventArgs e)
        {
            try
            {                
                Int32 _RowSel = -1;
                if (tbCAS.SelectedTab.Tag.ToString() == "Standard")  //if (_ActivePanel == enum_ActivePanel.Standard_CAS)
                {
                    _RowSel = C1StandardCAS.RowSel;
                    if (_RowSel >= 0)
                    {
                        // IF NO DATA PRESENT IN CAS LINE, THEN DON'T PROMT FOR CONFIRAMTION MESSAGE //
                        if ((C1StandardCAS.GetData(_RowSel, COL_STD_GROUP) == null || C1StandardCAS.GetData(_RowSel, COL_STD_GROUP).ToString() == "") &&
                            (C1StandardCAS.GetData(_RowSel, COL_STD_REASON) == null || C1StandardCAS.GetData(_RowSel, COL_STD_REASON).ToString() == "") &&
                            (C1StandardCAS.GetData(_RowSel, COL_STD_CASTYPE) == null || C1StandardCAS.GetData(_RowSel, COL_STD_CASTYPE).ToString() == ""))
                            C1StandardCAS.Rows.Remove(_RowSel);
                        else
                            if (MessageBox.Show("Are you sure you want to remove selected reason code?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {                                
                                _RemoveLine = new CASLine();
                                _RemoveLine.GroupCode = C1StandardCAS.GetData(_RowSel, COL_STD_GROUP).ToString();
                                _RemoveLine.ReasonCode = C1StandardCAS.GetData(_RowSel, COL_STD_REASON).ToString();
                                switch (C1StandardCAS.GetData(_RowSel, COL_STD_CASTYPE).ToString())
                                {
                                    case "W/O": { _RemoveLine.CASReasonType = enum_CASReasonType.WO; break; }
                                    case "WH": { _RemoveLine.CASReasonType = enum_CASReasonType.WH; break; }
                                    case "Deduct": { _RemoveLine.CASReasonType = enum_CASReasonType.Deduct; break; }
                                    case "Coins": { _RemoveLine.CASReasonType = enum_CASReasonType.Coins; break; }
                                    case "Copay": { _RemoveLine.CASReasonType = enum_CASReasonType.Copay; break; }
                                    case "Prev Paid": { _RemoveLine.CASReasonType = enum_CASReasonType.PrevPaid; break; }
                                }

                                C1StandardCAS.Rows.Remove(_RowSel);
                                IsPayerModified = true;
                                IsLineRemoved = true;                                                          
                            }
                    }
                }
                else if (tbCAS.SelectedTab.Tag.ToString() == "Other")  //if (_ActivePanel == enum_ActivePanel.Other_CAS)
                {
                    _RowSel = C1OtherCAS.RowSel;
                    if (_RowSel >= 0)
                    {
                        // IF NO DATA PRESENT IN CAS LINE, THEN DON'T PROMT FOR CONFIRAMTION MESSAGE //
                        if ((C1OtherCAS.GetData(_RowSel, COL_OTHER_GROUP) == null || C1OtherCAS.GetData(_RowSel, COL_OTHER_GROUP).ToString() == "") &&
                            (C1OtherCAS.GetData(_RowSel, COL_OTHER_REASON) == null || C1OtherCAS.GetData(_RowSel, COL_OTHER_REASON).ToString() == "") &&
                            (C1OtherCAS.GetData(_RowSel, COL_OTHER_PAIDACTION) == null || C1OtherCAS.GetData(_RowSel, COL_OTHER_PAIDACTION).ToString() == "") &&
                            (C1OtherCAS.GetData(_RowSel, COL_OTHER_ZEROPAIDACTION) == null || C1OtherCAS.GetData(_RowSel, COL_OTHER_ZEROPAIDACTION).ToString() == ""))
                            C1OtherCAS.Rows.Remove(_RowSel);
                        else
                            if (MessageBox.Show("Are you sure you want to remove selected reason code?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                C1OtherCAS.Rows.Remove(_RowSel);
                                IsPayerModified = true;                                 
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_Import_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(
                    "Copying another ERA Payer’s Reason Code Setup will replace this ERA Payer’s Reason Code Setup. " +
                    Environment.NewLine + "Continue Copying?", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

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
                        oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_PayerSelectedClick);
                        oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                    }
                    catch { }
                    oListControl.Dispose();
                    oListControl = null;
                }

                oListControl = new gloListControl.gloListControl(_DataBaseConnectionString, gloListControl.gloListControlType.ERAPayer, false, this.Width);
                oListControl.ControlHeader = "ERA Payers";

                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_PayerSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.Dock = DockStyle.Fill;
                this.Controls.Add(oListControl);
                pnlMST.Visible = false;
                pnlToolStrip.Visible = false;
                pnlTab.Visible = false;
                oListControl.OpenControl();
                
                //oListControl is disposed in OpenControl() Method if Zero records found
                if (oListControl.IsDisposed == false)
                {
                    oListControl.Dock = DockStyle.Fill;
                    oListControl.BringToFront();
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_Reset_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(
                    "The Reason Code Setup for this ERA Payer will be Reset back to the shipped default values. " + //Environment.NewLine +
                    "Any user entered values will be deleted. " + Environment.NewLine + "Reset the Reason Code Setup for this ERA Payer?",
                    _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    C1StandardCAS.Rows.Count = 1;
                    C1OtherCAS.Rows.Count = 1;
                    FillDefaultCAS();
                    FillOtherReasonDefaultAction();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_SaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsPayerModified && txtPayerID.Text.Trim() != "")
                {
                    this.Close();
                    return;
                }


                if (SavePayer())
                {
                    this.DialogResult = DialogResult.Yes;
                    IsSaveClicked = true;                    
                    this.Close();
                    if (_SettingID == 0)
                    { 
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.ERAPayer, ActivityCategory.Setup, ActivityType.Add, "ERAPayer Added", 0, _SettingID, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                        if (IsLineRemoved == false && _TempCAS == null)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.ERAPayer, ActivityCategory.Setup, ActivityType.Modify, "ERAPayer Modified", 0, _SettingID, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                        if (IsLineRemoved == true)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.ERAPayer, ActivityCategory.Setup, ActivityType.Modify, "ERAPayer : Line Removed For GroupCode : " + _RemoveLine.GroupCode + " ,ReasonCode : " + _RemoveLine.ReasonCode + " ,ReasonType : " + _RemoveLine.CASReasonType, 0, _SettingID, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                        if (_TempCAS != null)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.ERAPayer, ActivityCategory.Setup, ActivityType.Modify, "ERAPayer : Line Added For GroupCode : " + _TempCAS.GroupCode + " ,ReasonCode : " + _TempCAS.ReasonCode + " ,ReasonType : " + _TempCAS.CASReasonType, 0, _SettingID, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);                        
                    }
                    else 
                    {
                        if (IsLineRemoved == false && _TempCAS == null) 
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.ERAPayer, ActivityCategory.Setup, ActivityType.Modify, "ERAPayer Modified", 0, _SettingID, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                        if (IsLineRemoved == true) 
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.ERAPayer, ActivityCategory.Setup, ActivityType.Modify, "ERAPayer : Line Removed For GroupCode : " + _RemoveLine.GroupCode + " ,ReasonCode : " + _RemoveLine.ReasonCode + " ,ReasonType : " + _RemoveLine.CASReasonType, 0, _SettingID, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                        if (_TempCAS != null) 
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.ERAPayer, ActivityCategory.Setup, ActivityType.Modify, "ERAPayer : Line Added For GroupCode : " + _TempCAS.GroupCode + " ,ReasonCode : " + _TempCAS.ReasonCode + " ,ReasonType : " + _TempCAS.CASReasonType, 0, _SettingID, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void mnuItemAddLine_Click(object sender, EventArgs e)
        {
            tsb_Add_Click(sender, e);
        }

        private void mnuItemRemoveLine_Click(object sender, EventArgs e)
        {
            tsb_Remove_Click(sender, e);
        }

        private void mnuItemSaveClose_Click(object sender, EventArgs e)
        {
            tsb_SaveClose_Click(sender, e);
        }

        #endregion

        private void C1StandardCAS_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            String _sGroupCode = "";
            String _sReasonCode = "";
            if (C1StandardCAS.GetData(e.Row, COL_STD_REASON) != null)
                _sReasonCode = C1StandardCAS.GetData(e.Row, COL_STD_REASON).ToString().ToLower();
            if (C1StandardCAS.GetData(e.Row, COL_STD_GROUP) != null)
                _sGroupCode = C1StandardCAS.GetData(e.Row, COL_STD_GROUP).ToString().ToLower();

            for (int i = 1; i < C1StandardCAS.Rows.Count; i++)
            {
                if (i != e.Row)
                {
                    if (C1StandardCAS.GetData(i, COL_STD_REASON).ToString().ToLower() == _sReasonCode && C1StandardCAS.GetData(i, COL_STD_GROUP).ToString().ToLower() == _sGroupCode)
                    {
                        MessageBox.Show(_sGroupCode + _sReasonCode + " Already Exists. Enter unique Reason Code and Group Code.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        C1StandardCAS.SetData(e.Row, COL_STD_REASON, "");
                    }
                }
            }

            if (_sReasonCode.Trim() != "" && _sGroupCode.Trim() != "")
            {
                if (this.Visible)
                {
                    using (oPayer = new ERAPayer())
                    {
                        if (oPayer.IsReasonCodePresent(_sReasonCode, _sGroupCode) == false)
                        {
                            string sMsg = string.Format("Selected Group & Reason code \"{0} {1}\" is not present in Reason Code Master. Do you want to add?", _sGroupCode.ToUpper(), _sReasonCode.ToUpper());
                            if (MessageBox.Show(sMsg, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                C1StandardCAS.SetData(e.Row, COL_STD_REASON, "");
                                return;
                            }

                            frmSetupReasonCodes frmReasonCode = new frmSetupReasonCodes(_DataBaseConnectionString, _sReasonCode, _sGroupCode);
                            frmReasonCode.ShowDialog(this);

                            if (frmReasonCode.IsReasonCodeSave == false)
                            {
                                C1StandardCAS.SetData(e.Row, COL_STD_REASON, "");
                            }
                        }
                    }
                }

            }
        }


        private void C1OtherCAS_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            String _sGroupCode = "";
            String _sReasonCode = "";
            if (C1OtherCAS.GetData(e.Row, COL_OTHER_REASON) != null)
                _sReasonCode = C1OtherCAS.GetData(e.Row, COL_OTHER_REASON).ToString().ToLower();
            if (C1OtherCAS.GetData(e.Row, COL_OTHER_GROUP) != null)
                _sGroupCode = C1OtherCAS.GetData(e.Row, COL_OTHER_GROUP).ToString().ToLower();

            for (int i = 1; i < C1OtherCAS.Rows.Count; i++)
            {
                if (i != e.Row)
                {
                    if (C1OtherCAS.GetData(i, COL_OTHER_REASON).ToString().ToLower() == _sReasonCode && C1OtherCAS.GetData(i, COL_OTHER_GROUP).ToString().ToLower() == _sGroupCode)
                    {
                        MessageBox.Show(_sGroupCode + _sReasonCode + " Already Exists. Enter unique Reason Code and Group Code.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        C1OtherCAS.SetData(e.Row, COL_OTHER_REASON, "");
                    }
                }
            }
        }

    }
}
