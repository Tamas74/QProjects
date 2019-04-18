using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloBilling.Payment;

namespace gloBilling
{
    public partial class frmStandardReasonCodeSetup : Form
    {
        #region " Variable Declaration "
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseConnection = "";
        public gloGridListControl ogloGridListControl = null;

        private Int64 _UserID = 0;
        private string _UserName = "";
        private Int64 _ClinicID = 0;
        private string _messageboxcaption = "";


        private Int64 _ReasonID = 0;
        private gloDatabaseLayer.DBLayer oDB = null;
        private gloDatabaseLayer.DBParameters oDBPara = null;
        Tuple<long, string, string, string, string, Boolean> _SelectedReasonCode = null;
        private string _ReasonCodeType;
        //int nCASType;       

        #endregion

        #region " Column Constants "

        const int COL_ID = 0;
        const int COL_TYPE = 1;
        const int COL_CODE = 2;
        const int COL_REASONDESCRIPTION = 3;
        const int COL_SYSTEMDEFINED = 4;

        const int COL_COUNT = 5;

        #endregion " Column Constants "

        #region " Constructor "
        public frmStandardReasonCodeSetup(Tuple<long, string, string, string, string, Boolean> SelectedReasonCode)
        {
            InitializeComponent();

            #region " Retrive ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            #endregion " Retrive ClinicID from AppSettings "

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM"; ;
                }
            }
            else
            { _messageboxcaption = "gloPM"; ; }

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

            _SelectedReasonCode = SelectedReasonCode;
            if (_SelectedReasonCode != null)
            {
                _ReasonID = _SelectedReasonCode.Item1;
            }
            _databaseConnection = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            DesignGrid();
        }
        #endregion

        #region "Property Procedure"

        public string ReasonCodeType
        { get { return _ReasonCodeType; } set { _ReasonCodeType = value; } }

        #endregion


        private void frmStandardReasonCodeSetup_Load(object sender, EventArgs e)
        {
            if (_SelectedReasonCode != null)
            {
                LoadReasonCode();
            }
        }

        private void tls_btnAddLine_Click(object sender, EventArgs e)
        {
            AddLine();
        }

        private void tls_btnRemoveLine_Click(object sender, EventArgs e)
        {
            RemoveLine();
        }

        private void tlb_SavenClose_Click(object sender, EventArgs e)
        {
            if (ValidateSave())
            {
                SaveReasonCodes();                
                this.Close();                
            }
        }

        private void tlb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void ReasonGrid_StartEdit(object sender, RowColEventArgs e)
        {
            switch (e.Col)
            {
                case COL_CODE:
                    {
                        OpenInternalControl(gloGridListControlType.ReasonCodes, "Reason Code", false, e.Row, e.Col, "");
                        string _SearchText = "";
                        if (ReasonGrid != null && ReasonGrid.Rows.Count > 0)
                        {

                            //_SearchText = Convert.ToString(ReasonGrid.GetData(COL_REASONCODE, e.Col));
                            _SearchText = Convert.ToString(ReasonGrid.GetData(e.Row, COL_CODE));

                            //_SearchText = Convert.ToString("CR  8");
                            if (_SearchText != "" && ogloGridListControl != null)
                            {
                                ogloGridListControl.AdvanceSearch(_SearchText);
                            }
                        }
                    }
                    break;
                case COL_REASONDESCRIPTION:
                    {
                        OpenInternalControl(gloGridListControlType.ReasonCodes, "Reason Code", false, e.Row, e.Col, "");
                        string _SearchText = "";
                        if (ReasonGrid != null && ReasonGrid.Rows.Count > 0)
                        {

                            //_SearchText = Convert.ToString(ReasonGrid.GetData(COL_REASONCODE, e.Col));
                            _SearchText = Convert.ToString(ReasonGrid.GetData(e.Row, COL_REASONDESCRIPTION));

                            //_SearchText = Convert.ToString("CR  8");
                            if (_SearchText != "" && ogloGridListControl != null)
                            {
                                ogloGridListControl.AdvanceSearch(_SearchText);
                            }
                        }
                    }
                    break;
            }

        }

        private void ReasonGrid_ChangeEdit(object sender, EventArgs e)
        {
            string _strSearchString = "";
            try
            {
                _strSearchString = ReasonGrid.Editor.Text;

                if (ogloGridListControl != null)
                {

                    if (ReasonGrid.Col == COL_CODE || ReasonGrid.Col == COL_REASONDESCRIPTION)
                    {
                        string _COL_REASONCODE = "";
                        string _COL_REASONDESC = "";

                        if (ReasonGrid != null && ReasonGrid.Rows.Count > 0)
                        {
                            _COL_REASONCODE = Convert.ToString(ReasonGrid.GetData(ReasonGrid.Row, COL_CODE));
                            _COL_REASONDESC = Convert.ToString(ReasonGrid.GetData(ReasonGrid.Row, COL_REASONDESCRIPTION));
                            ogloGridListControl.SelectedReasonCode = _strSearchString;
                        }

                        if (_strSearchString != "" && ogloGridListControl != null)
                        {
                            ogloGridListControl.AdvanceSearch(_strSearchString);                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex.ToString();
            }
            finally
            {
            }
        }

        private void ReasonGrid_Enter(object sender, EventArgs e)
        {
            pnlReasonCodemain.BackgroundImage = global::gloBilling.Properties.Resources.Img_Rx_MxGreen;
            pnlReasonCodemain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            lblReasonCodemain.ForeColor = System.Drawing.Color.White;
        }

        private void ReasonGrid_KeyUp(object sender, KeyEventArgs e)
        {

            //int _id = 0;
            string _code = "";
            string _description = "";
            bool _isdeleted = true;

            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            RowColEventArgs e1 = null;

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    #region "Enter Key"

                    if (pnlInternalControl.Visible)
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
                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            ogloGridListControl.Focus();
                        }
                    }
                    #endregion
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    #region "Escape Key"
                    if (pnlInternalControl.Visible)
                    {
                        if (ogloGridListControl != null)
                        {
                            CloseInternalControl();

                            if (ReasonGrid.RowSel > 0)
                            {

                            }
                        }
                    }
                    #endregion
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    if (ReasonGrid.GetData(ReasonGrid.RowSel, COL_CODE) != null)
                    {
                        _code = ReasonGrid.GetData(ReasonGrid.RowSel, COL_CODE).ToString();
                    }
                    if (ReasonGrid.GetData(ReasonGrid.RowSel, COL_REASONDESCRIPTION) != null)
                    {
                        _description = ReasonGrid.GetData(ReasonGrid.RowSel, COL_REASONDESCRIPTION).ToString();
                    }

                    e2.oType = TransactionLineColumnType.None;

                    e.SuppressKeyPress = true;

                    #region "Delete Key"
                    switch (ReasonGrid.ColSel)
                    {
                        case COL_CODE:
                            {
                                ReasonGrid.SetData(ReasonGrid.RowSel, ReasonGrid.ColSel, null);
                                ReasonGrid.SetData(ReasonGrid.RowSel, ReasonGrid.ColSel + 1, null);
                                ReasonGrid.SetData(ReasonGrid.RowSel, ReasonGrid.ColSel, null);
                                e2.oType = TransactionLineColumnType.ReasonCode;
                            }
                            break;
                        case COL_REASONDESCRIPTION:
                            {
                                ReasonGrid.SetData(ReasonGrid.RowSel, COL_CODE, null);
                                ReasonGrid.SetData(ReasonGrid.RowSel, COL_REASONDESCRIPTION, null);
                                ReasonGrid.SetData(ReasonGrid.RowSel, ReasonGrid.ColSel, null);
                                e2.oType = TransactionLineColumnType.ReasonCode;
                            }
                            break;

                    }
                    _code = "";
                    e1 = new RowColEventArgs(ReasonGrid.RowSel, ReasonGrid.ColSel);
                    e2.code = _code;
                    e2.description = _description;
                    e2.isdeleted = true;


                    e2.code = _code;
                    e2.description = _description;
                    e2.isdeleted = _isdeleted;


                    #endregion
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex.ToString();
            }
            finally
            {

            }

        }

        private void ReasonGrid_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }


        private void LoadReasonCode()
        {
            try
            {
                ReasonGrid.Rows.Add();
                ReasonGrid.SetCellStyle(ReasonGrid.Rows.Count - 1, COL_TYPE, ReasonGrid.Styles["csGroupCodes"]);
                ReasonGrid.SetData(1, COL_ID, _ReasonID);
                ReasonGrid.SetData(1, COL_CODE, _SelectedReasonCode.Item2 + _SelectedReasonCode.Item3);
                ReasonGrid.SetData(1, COL_TYPE, _SelectedReasonCode.Item4);
                ReasonGrid.SetData(1, COL_REASONDESCRIPTION, _SelectedReasonCode.Item5);
                if (_SelectedReasonCode.Item6 == true)
                    ReasonGrid.SetCellCheck(1, COL_SYSTEMDEFINED, CheckEnum.Checked);
                else
                    ReasonGrid.SetCellCheck(1, COL_SYSTEMDEFINED, CheckEnum.Unchecked);

                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.InsurancePayment, gloAuditTrail.ActivityCategory.ReasonCodeSetup, gloAuditTrail.ActivityType.Add, "Reason code  '" + _SelectedReasonCode.Item3 + "' loaded successfully.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.InsurancePayment, gloAuditTrail.ActivityCategory.ReasonCodeSetup, gloAuditTrail.ActivityType.Add, "Error while loading Reason code  '" + _SelectedReasonCode.Item3 + "'.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
        }

        private void SaveReasonCodes()
        {
            DataTable _dt = new DataTable();
            object _result;
            string Code = string.Empty;
            string GroupCode = string.Empty;
            string ReasonCode = string.Empty;
            string Type = string.Empty;
            string Description = string.Empty;
            bool Systemdef;
            bool isSuccess = true;
            clsStandardReasonCode oStandardReasonCode = null;
            int CastType;            
            try
            {
                if (OpenConnection(true))
                {
                    for (int i = 1; i < ReasonGrid.Rows.Count; i++)
                    {
                        Code = ReasonGrid[i, COL_CODE].ToString();
                        GroupCode = Code.Substring(0, 2);
                        ReasonCode = Code.Substring(2);
                        Type = ReasonGrid[i, COL_TYPE].ToString();

                        if (Type == "Co-ins")
                            CastType = 1;
                        else if (Type == "Copay")
                            CastType = 2;
                        else if (Type == "Deduct")
                            CastType = 3;
                        else if (Type == "Withhold")
                            CastType = 5;
                        else
                            CastType = 6;

                        Description = ReasonGrid[i, COL_REASONDESCRIPTION].ToString();

                        if (ReasonGrid.GetCellCheck(i, COL_SYSTEMDEFINED) == CheckEnum.Checked)
                            Systemdef = true;
                        else
                            Systemdef = false;

                        bool allowSave = true;
                        if (_ReasonID == 0)
                        {
                            oStandardReasonCode = new global::gloBilling.clsStandardReasonCode();
                            if (!oStandardReasonCode.CheckDuplicate(GroupCode, ReasonCode) == false)
                            {
                                MessageBox.Show("Reason code already present.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);                                
                                allowSave = false;
                                isSuccess = false;                                
                            }
                        }

                        if (allowSave)
                        {
                            oDBPara.Add("@ReasonCodeID", _ReasonID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            oDBPara.Add("@GroupCode", GroupCode, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBPara.Add("@ReasonCode", ReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBPara.Add("@CASType", CastType, ParameterDirection.Input, SqlDbType.Int);
                            oDBPara.Add("@CASTypeDesc", Description, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBPara.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBPara.Add("@UserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBPara.Add("@CreatedDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                            oDBPara.Add("@ModifiedDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                            oDBPara.Add("@SystemDefined", Systemdef, ParameterDirection.Input, SqlDbType.Bit);
                            oDB.Execute("BL_INUP_Standard_ReasonCode", oDBPara, out _result);
                        
                            if (Convert.ToInt64(_result) == 0)
                            {
                                isSuccess = false;                                                             
                            }
                        } 
                        oDBPara.Clear();
                    }

                    if (isSuccess)
                    {
                        if (_ReasonID == 0)
                        {
                            MessageBox.Show("Reason code added successfully.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.InsurancePayment, gloAuditTrail.ActivityCategory.ReasonCodeSetup, gloAuditTrail.ActivityType.Add, "Reason code '" + ReasonCode + "' added successfully.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                        }
                        else
                        {
                            MessageBox.Show("Reason code updated successfully.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.InsurancePayment, gloAuditTrail.ActivityCategory.ReasonCodeSetup, gloAuditTrail.ActivityType.Add, "Reason code '" + ReasonCode + "' updated successfully.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

                if (_ReasonID == 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.InsurancePayment, gloAuditTrail.ActivityCategory.ReasonCodeSetup, gloAuditTrail.ActivityType.Add, "Error While Adding Reason code '" + ReasonCode + "'.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure);
                }
                else
                {

                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.InsurancePayment, gloAuditTrail.ActivityCategory.ReasonCodeSetup, gloAuditTrail.ActivityType.Add, "Error While updating Reason code '" + ReasonCode + "'.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure);
                }

            }
            finally
            {                
                if (oDBPara != null) 
                { 
                    oDBPara.Dispose();                    
                }
                if (oDB != null) 
                { 
                    oDB.Disconnect(); 
                    oDB.Dispose();                     
                }
                CloseConnection();
            }
        }

        public bool DeleteReasonCodes(Int64 ReasonID)
        {
            bool _Result = false;
            try
            {
                if (OpenConnection(true))
                {
                    string strQuery = "DELETE FROM Insurance_DefaultReasonCodes WHERE nReasonCodeID=" + ReasonID;
                    int result = oDB.Execute_Query(strQuery);
                    if (result > 0)
                    {
                        _Result = true;
                        MessageBox.Show("Record Deleted Successsfully.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.InsurancePayment, gloAuditTrail.ActivityCategory.ReasonCodeSetup, gloAuditTrail.ActivityType.Add, "Reason code Id '" + ReasonID + "' deleted successfully.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                    }
                    else
                        _Result = false;
                }
                CloseConnection();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.InsurancePayment, gloAuditTrail.ActivityCategory.ReasonCodeSetup, gloAuditTrail.ActivityType.Add, "Error While deleting Reason code Id '" + ReasonID + "'.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
            return _Result;
        }

        private bool ValidateSave()
        {
            bool _retVal = true;
            try
            {
                if (ReasonGrid != null && ReasonGrid.Rows.Count > 1)
                {
                    for (int rowIndex = 1; rowIndex < ReasonGrid.Rows.Count; rowIndex++)
                    {   
                        if (!Validation(rowIndex))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select atleast one reason code.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                ex.ToString();
                return false;
            }
            return _retVal;
        }

        private bool Validation(int rowindex)
        {
            bool Flag = true;

            if ((ReasonGrid.GetData(rowindex, COL_TYPE)) == null || Convert.ToString(ReasonGrid.GetData(rowindex, COL_TYPE)) == "") 
            {
                MessageBox.Show("Please enter type for line number " + rowindex.ToString() + ".", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReasonGrid.Select(rowindex, COL_TYPE, true);
                return false;
            }

            if ((ReasonGrid.GetData(rowindex, COL_CODE)) == null || Convert.ToString(ReasonGrid.GetData(rowindex, COL_CODE)) == "")
            {
                MessageBox.Show("Please enter reason code for line number " + rowindex.ToString() + ".", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReasonGrid.Select(rowindex, COL_CODE, true);
                return false;
            }

            if ((ReasonGrid.GetData(rowindex, COL_REASONDESCRIPTION)) == null || Convert.ToString(ReasonGrid.GetData(rowindex, COL_REASONDESCRIPTION)) == "")
            {
                MessageBox.Show("Please enter description for line number " + rowindex.ToString() + ".", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReasonGrid.Select(rowindex, COL_REASONDESCRIPTION, true);
                return false;
            }


            if ((ReasonGrid.GetData(rowindex, COL_CODE) != null) || (Convert.ToString(ReasonGrid.GetData(rowindex, COL_CODE)).Trim() == ""))
            {
                try
                {
                    string _str = Convert.ToString(ReasonGrid.GetData(rowindex, COL_CODE)).Trim().Replace('-', ' ').Replace(',', ' ').Replace('(', ' ').Replace(')', ' ').Trim();
                    string _strType = Convert.ToString(ReasonGrid.GetData(rowindex, COL_TYPE));
                    bool _isValidCode = false;
                    _str = Convert.ToString(ReasonGrid.GetData(rowindex, COL_CODE)).Trim();
                    if (ReasonGrid.Editor != null) 
                    _str = ReasonGrid.Editor.Text;
                    _isValidCode = InsurancePayment.IsValidReasonCode(_str);
                    if (_isValidCode == false)
                    {
                        MessageBox.Show("Please select a valid code.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (ReasonGrid.Editor != null)
                            ReasonGrid.Editor.Text = "";
                        ReasonGrid.SetData(rowindex, COL_CODE, null);
                        ReasonGrid.SetData(rowindex, COL_REASONDESCRIPTION, null);
                        ReasonGrid.Select(rowindex, COL_CODE, true);
                        CloseInternalControl();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                }
            }
                        
            try
            {
                clsStandardReasonCode oStandardReasonCode = new global::gloBilling.clsStandardReasonCode();
                string GroupCode = string.Empty;
                string ReasonCode = string.Empty;
                string Code = Convert.ToString(ReasonGrid.GetData(rowindex, COL_CODE));
                GroupCode = Code.Substring(0, 2);
                ReasonCode = Code.Substring(2);

                if (ReasonGrid.Editor != null) 
                {
                    Code = ReasonGrid.Editor.Text;
                    GroupCode = Code.Substring(0, 2);
                    ReasonCode = Code.Substring(2);
                }

                if (_SelectedReasonCode != null)
                {
                    if (String.Concat(GroupCode, ReasonCode) != String.Concat(_SelectedReasonCode.Item2, _SelectedReasonCode.Item3))
                    {
                        if (!oStandardReasonCode.CheckDuplicate(GroupCode, ReasonCode) == false)
                        {
                            MessageBox.Show("Reason code already present.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            GroupCode = string.Empty;
                            ReasonCode = string.Empty;
                            Code = string.Empty;
                            ReasonGrid.SetData(rowindex, COL_CODE, null);
                            ReasonGrid.SetData(rowindex, COL_REASONDESCRIPTION, null);
                            ReasonGrid.Select(rowindex, COL_CODE, true);
                            return false;
                        }
                    }
                }
                else
                {
                    if (!oStandardReasonCode.CheckDuplicate(GroupCode, ReasonCode) == false)
                    {
                        MessageBox.Show("Reason code already present.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        GroupCode = string.Empty;
                        ReasonCode = string.Empty;
                        Code = string.Empty;
                        ReasonGrid.SetData(rowindex, COL_CODE, null);
                        ReasonGrid.SetData(rowindex, COL_REASONDESCRIPTION, null);
                        ReasonGrid.Select(rowindex, COL_CODE, true);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }           

            return Flag;
        }    


        private void AddLine()
        {
            try
            {
                if (ReasonGrid != null)
                {
                    ReasonGrid.Rows.Add();
                    ReasonGrid.SetCellStyle(ReasonGrid.Rows.Count - 1, COL_TYPE, ReasonGrid.Styles["csGroupCodes"]);
                    ReasonGrid.SetCellCheck(ReasonGrid.Rows.Count - 1, 4, CheckEnum.Unchecked);
                    //ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_CODE, ClaimNo);
                    //ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_REASONDESCRIPTION, BillingTransactionID);
                    //ReasonGrid.SetData(ReasonGrid.Rows.Count - 1, COL_SYSTEMDEFINED, BillingTransactionDetailID);                
                    ReasonGrid.Focus();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void RemoveLine()
        {
            if (ReasonGrid != null)
            {
                if (ReasonGrid.RowSel > 0)
                {
                    if (ReasonGrid.Rows.Count > 1) 
                        ReasonGrid.Rows.Remove(ReasonGrid.RowSel);                     
                    SetReasonCodes();                 
                }
                else
                {
                    if (ReasonGrid.Rows.Count > 1) 
                        ReasonGrid.Rows.Remove(ReasonGrid.Rows.Count - 1);
                    SetReasonCodes();
                }
                ReasonGrid.Focus();
            }
            CloseInternalControl();
        }


        private void SetComboBoxCellStyle()
        {
            CellStyle csStdGroupCode;
            if (!ReasonGrid.Styles.Contains("csGroupCodes"))
            {
                csStdGroupCode = ReasonGrid.Styles.Add("csGroupCodes");
                csStdGroupCode.ComboList = "W/O|Copay|Deduct|Co-ins|Withhold";
            }
        }

        private void DesignGrid()
        {
            try
            {
                ReasonGrid.Cols.Count = COL_COUNT;
                ReasonGrid.Rows.Count = 1;
                ReasonGrid.Cols.Fixed = 0;

                ReasonGrid.Rows.Fixed = 0;
                ReasonGrid.ScrollBars = ScrollBars.Both;

                ReasonGrid.SetData(0, COL_ID, "Id");
                ReasonGrid.SetData(0, COL_TYPE, "Type");
                ReasonGrid.SetData(0, COL_CODE, "Code");
                ReasonGrid.SetData(0, COL_REASONDESCRIPTION, "Description");
                ReasonGrid.SetData(0, COL_SYSTEMDEFINED, "System Defined");

                ReasonGrid.Cols[COL_ID].Name = "Id";
                ReasonGrid.Cols[COL_TYPE].Name = "Type";
                ReasonGrid.Cols[COL_CODE].Name = "Code";
                ReasonGrid.Cols[COL_REASONDESCRIPTION].Name = "Description";
                ReasonGrid.Cols[COL_SYSTEMDEFINED].Name = "System Defined";

                ReasonGrid.Cols[COL_ID].Visible = false;
                ReasonGrid.Cols[COL_TYPE].Visible = true;
                ReasonGrid.Cols[COL_CODE].Visible = true;
                ReasonGrid.Cols[COL_REASONDESCRIPTION].Visible = true;
                ReasonGrid.Cols[COL_SYSTEMDEFINED].Visible = false;

                ReasonGrid.Cols[COL_ID].Width = 0;
                ReasonGrid.Cols[COL_TYPE].Width = 95;
                ReasonGrid.Cols[COL_CODE].Width = 90;
                ReasonGrid.Cols[COL_REASONDESCRIPTION].Width = 350;
                ReasonGrid.Cols[COL_SYSTEMDEFINED].Width = 0;

                ReasonGrid.Cols[COL_ID].AllowEditing = false;
                ReasonGrid.Cols[COL_TYPE].AllowEditing = true;
                ReasonGrid.Cols[COL_CODE].AllowEditing = true;
                ReasonGrid.Cols[COL_REASONDESCRIPTION].AllowEditing = false;
                ReasonGrid.Cols[COL_SYSTEMDEFINED].AllowEditing = true;

                ReasonGrid.Cols[COL_TYPE].TextAlign = TextAlignEnum.LeftCenter;
                ReasonGrid.Cols[COL_CODE].TextAlign = TextAlignEnum.LeftCenter;
                ReasonGrid.Cols[COL_REASONDESCRIPTION].TextAlign = TextAlignEnum.LeftCenter;
                ReasonGrid.Cols[COL_SYSTEMDEFINED].TextAlign = TextAlignEnum.LeftCenter;

                ReasonGrid.Cols[COL_SYSTEMDEFINED].ImageAlign = ImageAlignEnum.CenterCenter;
                ReasonGrid.Rows[0].AllowEditing = false;

                //ReasonGrid.Rows[0].StyleNew.[C1.Win.C1FlexGrid.CellStyleEnum.Fixed].Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                CellStyle cs = ReasonGrid.Rows[0].StyleNew;
                cs.Font = new Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);

                SetComboBoxCellStyle();


                C1.Win.C1FlexGrid.CellStyle csEditableActionStatus;// = FlexGrid.Styles.Add("cs_ReasonCodes");
                try
                {
                    if (ReasonGrid.Styles.Contains("cs_ReasonCodes"))
                    {
                        csEditableActionStatus = ReasonGrid.Styles["cs_ReasonCodes"];
                    }
                    else
                    {
                        csEditableActionStatus = ReasonGrid.Styles.Add("cs_ReasonCodes");
                        csEditableActionStatus.DataType = typeof(System.String);
                        csEditableActionStatus.Font = gloGlobal.clsgloFont.gFont_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableActionStatus.BackColor = Color.White;
                    }
                }
                catch
                {
                    csEditableActionStatus = ReasonGrid.Styles.Add("cs_ReasonCodes");
                    csEditableActionStatus.DataType = typeof(System.String);
                    csEditableActionStatus.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableActionStatus.BackColor = Color.White;
                }

                string _comboList = "";
                EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(_databaseConnection);
                _comboList = ogloEOBPaymentInsurance.GetReasonCodes();
                ogloEOBPaymentInsurance.Dispose();
                csEditableActionStatus.ComboList = _comboList;

                ReasonGrid.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross;
                ReasonGrid.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2007Blue;
                ReasonGrid.Styles[C1.Win.C1FlexGrid.CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                ReasonGrid.Styles[C1.Win.C1FlexGrid.CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                ReasonGrid.Styles[C1.Win.C1FlexGrid.CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }


        private bool OpenInternalControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int RowIndex, int ColIndex, string SearchText)
        {
            bool _result = false;
            try
            {

                if (ogloGridListControl != null)
                {
                    CloseInternalControl();
                }

                ogloGridListControl = new gloGridListControl(ControlType, false, pnlInternalControl.Width, RowIndex, ColIndex);

                ogloGridListControl.ItemSelected += new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                ogloGridListControl.ControlHeader = ControlHeader;

                ogloGridListControl.Dock = DockStyle.Fill;
                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, SearchColumn.Code);
                }
                ogloGridListControl.Show();


                int _x = 0;
                int _y = 0;
                int _width = 0;
                int _height = 0;
                int _parentleft = 0;
                int _parentwidth = 0;
                int _diffFactor = 0;



                pnlInternalControl.Controls.Add(ogloGridListControl);
                _x = ReasonGrid.Cols[ColIndex].Left;
                _y = ReasonGrid.Rows[RowIndex].Bottom;
                _width = pnlInternalControl.Width;
                _height = pnlInternalControl.Height;



                _parentleft = pnlInternalControl.Parent.Bounds.Left;
                _parentwidth = pnlInternalControl.Parent.Bounds.Width;
                _diffFactor = _parentwidth - _x;

                if (_diffFactor < _width)
                {
                    _x = pnlInternalControl.Parent.Bounds.Width + (_diffFactor);
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                else
                {
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                pnlInternalControl.Visible = true;
                pnlInternalControl.BringToFront();



                ogloGridListControl.Focus();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                _result = false;
            }
            finally
            {
                RePositionInternalControl(ControlType);
            }
            return _result;
        }

        private void RePositionInternalControl(gloGridListControlType ControlType)
        {
            try
            {
                if (ControlType == gloGridListControlType.ReasonCodes)
                {
                    if (ReasonGrid.Parent.Bottom - ReasonGrid.Rows[ReasonGrid.RowSel].Bottom - pnlInternalControl.Height + 160 < pnlReason.Height)
                    {
                        pnlInternalControl.SetBounds((ReasonGrid.Cols[ReasonGrid.ColSel].Left + ReasonGrid.ScrollPosition.X), (ReasonGrid.Rows[ReasonGrid.RowSel].Top - pnlInternalControl.Height) + ReasonGrid.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                        pnlInternalControl.Visible = true;
                        pnlInternalControl.BringToFront();
                    }
                    else
                    {
                        pnlInternalControl.SetBounds(ReasonGrid.Cols[ReasonGrid.ColSel].Left, ReasonGrid.Rows[ReasonGrid.RowSel].Bottom + ReasonGrid.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {

                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }
                pnlInternalControl.Visible = false;
                pnlInternalControl.SendToBack();

                if (ogloGridListControl != null)
                {
                    try
                    {
                        ogloGridListControl.ItemSelected -= new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                        ogloGridListControl.InternalGridKeyDown -= new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);

                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    }
                    ogloGridListControl.Dispose();
                    ogloGridListControl = null;
                }

                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                _result = false;
            }
            finally
            { }
            return _result;
        }

        private void SetReasonCodes()
        {
            String sReasonCodes = "";
            string[] ReasonCodes;
            try
            {

                if (ReasonGrid != null && ReasonGrid.Rows.Count > 1)
                {
                    for (int rIndex = 1; rIndex < ReasonGrid.Rows.Count; rIndex++)
                    {
                        if (ReasonGrid.GetData(rIndex, COL_CODE) != null && Convert.ToString(ReasonGrid.GetData(rIndex, COL_CODE)).Trim() != "" && Convert.ToString(ReasonGrid.GetData(rIndex, COL_CODE)).Trim() != "")
                        {
                            sReasonCodes = sReasonCodes + ReasonGrid.GetData(rIndex, COL_CODE) + "|";
                        }

                    }
                }
                //C1.Win.C1FlexGrid.CellStyle csReasonCodes;

                ReasonCodes = sReasonCodes.Split('|');


            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);

            }
            finally
            {
                ReasonCodes = null;
            }
        }


        #region " Delegates "

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {
            #region "Custom Event"
            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            #endregion

            try
            {
                if (ogloGridListControl._ControlType == gloGridListControlType.ReasonCodes)
                {
                    int _rowIndex = 0;
                    if (ogloGridListControl.SelectedItems != null && ogloGridListControl.SelectedItems.Count > 0)
                    {
                        //...Check if code exists
                        bool _isExistsCode = false;

                        if (ReasonGrid != null && ReasonGrid.Rows.Count > 1)
                        {
                            for (int rIndex = 1; rIndex < ReasonGrid.Rows.Count; rIndex++)
                            {
                                if (rIndex != ogloGridListControl.ParentRowIndex)
                                {
                                    if (ReasonGrid.GetData(rIndex, COL_CODE) != null && Convert.ToString(ReasonGrid.GetData(rIndex, COL_CODE)).Trim() != ""
                                        && Convert.ToString(ReasonGrid.GetData(rIndex, COL_CODE)).Trim().ToUpper() == ogloGridListControl.SelectedItems[0].Code.Trim().ToUpper())
                                    {
                                        _isExistsCode = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (_isExistsCode == false)
                        {
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            ReasonGrid.SetData(_rowIndex, COL_CODE, ogloGridListControl.SelectedItems[0].Code.Trim());
                            ReasonGrid.SetData(_rowIndex, COL_REASONDESCRIPTION, ogloGridListControl.SelectedItems[0].Description.Trim());
                            ReasonGrid.Focus();
                            ReasonGrid.Select(_rowIndex, COL_CODE, true);
                        }
                        else
                        {
                            MessageBox.Show("Adjustment code already exists.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            ReasonGrid.SetData(_rowIndex, COL_CODE, null);
                            ReasonGrid.SetData(_rowIndex, COL_REASONDESCRIPTION, null);
                            ReasonGrid.Select(_rowIndex, COL_CODE, true);
                        }
                    }
                    else
                    {                       
                        _rowIndex = ogloGridListControl.ParentRowIndex;
                        if (ogloGridListControl.SelectedItems.Count == 0)
                        {
                            ReasonGrid.SetData(_rowIndex, COL_CODE, null);
                            ReasonGrid.SetData(_rowIndex, COL_REASONDESCRIPTION, null);
                            ReasonGrid.Focus();
                            ReasonGrid.Select(_rowIndex, COL_CODE, true);
                        }
                    }

                }

                SetReasonCodes();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);

            }
            finally
            {
                CloseInternalControl();
            }
        }

        void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                CloseInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            { }
        }

        #endregion

        #region " Open/Close Database Connection "
        private bool OpenConnection(bool withParameters)
        {
            bool _Result = false;
            try
            {
                if (_databaseConnection != "")
                {
                    oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
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
            try
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        #endregion              
    }
}
