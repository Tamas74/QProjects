using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSetupReferralCPT : Form
    {
        #region " Contructors "

        public frmSetupReferralCPT(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "";
                }
            }
            else
            { _MessageBoxCaption = ""; }

            #endregion
        }

        public frmSetupReferralCPT(string DatabaseConnectionString,Int64 CPTID)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _CPTID = CPTID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "";
                }
            }
            else
            { _MessageBoxCaption = ""; }

            #endregion
        }

        #endregion " Contructors "

        #region " Private And Public Variables "

        private string _databaseconnectionstring = "";
        public string _MessageBoxCaption = "";
        private Int64 _CPTID = 0;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private string _Code = "";
        private string _Description = "";

        gloGridListControl ogloGridListControl = null;

        const int COL_ID = 0;
        const int COL_CODE = 1;
        const int COL_DESC = 2;
        const int COL_COUNT = 3;

        #endregion " Private And Public Variables "

        #region " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 CPTID
        {
            get { return _CPTID; }
            set { _CPTID = value; }
        }
        public string CPTCode
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public string CPTDescription
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        #endregion " Property Procedures "

        #region " Form Load Event "

        private void frmSetupReferralCPT_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(c1CPT, false);
            c1CPTSearchGrid.Select();
            
            try
            {
                DesignGrid();
                Fill_ReferralCPTs();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #endregion " Form Load Event "

        #region " Private Methods Not Using Anymore"

        private string GetCPTDescription(string strCPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _Description = "";
            string _sqlQuery = "";
            object _result = null;
            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT ISNULL(sDescription,'') AS sDescription " +
                    " FROM CPT_MST  " +
                " WHERE UPPER(sCPTCode) = '" + txtCode.Text.Trim().ToUpper() + "' AND nClinicID = " + this.ClinicID + "";
                _result = oDB.ExecuteScalar_Query(_sqlQuery);

                if (_result != null && Convert.ToString(_result) != "")
                {
                    _Description = Convert.ToString(_result);
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            { if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); } }

            return _Description;
        }

        private bool ValidateData()
        {
            // Validations for Add CPT form
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Enter a CPT code.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Focus();
                return false;
            }
            if (txtDescription.Text.Trim() == "")
            {
                MessageBox.Show("Enter a description.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescription.Focus();
                return false;
            }

            return true;
        }

        private void SaveCPT()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            Int64 _result = 0;
            object _Id = null;
            try
            {
                oDB.Connect(false);
                if (IsExistsCPT(_CPTID, txtCode.Text.Trim(), txtDescription.Text.Trim()) == false)
                {
                    if (_CPTID <= 0)
                    {

                        strSQL = " SELECT ISNULL(Max(nCPTID),0)+1 " +
                                 " FROM  BL_ReferralCPT_MST  ";

                                 _Id = oDB.ExecuteScalar_Query(strSQL);

                        if (_Id != null && Convert.ToInt64(_Id) > 0)
                        {
                            _CPTID = Convert.ToInt64(_Id);
                        }

                        strSQL = "";

                        strSQL = " INSERT INTO BL_ReferralCPT_MST " +
                                 " (nCPTID, sCPTCode, sDescription, bIsReferral, nClinicID)  " +
                                 " VALUES     (" + _CPTID + ", '" + txtCode.Text.Trim() + "', '" + txtDescription.Text.Trim() + "', " + chkIsReferralCPT.Checked.GetHashCode() + ", " + this.ClinicID + ")";
                        _result = oDB.Execute_Query(strSQL);
                    }
                    else
                    {
                        strSQL = " INSERT INTO BL_ReferralCPT_MST " +
                                 " (nCPTID, sCPTCode, sDescription, bIsReferral, nClinicID)  " +
                                 " VALUES     (" + _CPTID + ", '" + txtCode.Text.Trim() + "', '" + txtDescription.Text.Trim() + "', " + chkIsReferralCPT.Checked.GetHashCode() + ", " + this.ClinicID + ")";
                        _result = oDB.Execute_Query(strSQL);
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private bool IsExistsCPT(Int64 CPTId, string CPTCode, string CPTName)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                if (CPTId == 0)
                {

                    strQuery = "select count(nCPTID) from BL_ReferralCPT_MST where (sCPTCode = '" + CPTCode.Replace("'", "''") + "' OR sDescription = '" + CPTName.Replace("'", "''") + "') AND nClinicID = " + this.ClinicID + " ";
                }
                else
                {
                    //strQuery = "select count(nCPTID) from CPT_MST where (sCPTCode = '" + CPTCode + "' OR sDescription = '" + CPTName + "') AND nCPTID <> "+CPTId+" ";
                    //
                    strQuery = "select count(nCPTID) from BL_ReferralCPT_MST where ((sCPTCode = '" + CPTCode.Replace("'", "''") + "' OR sDescription = '" + CPTName.Replace("'", "''") + "') AND nCPTID <> " + CPTId + ") AND nClinicID = " + this.ClinicID + " ";

                }

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if(oDB!=null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return _result;
        }

        private void FillControls()
        {
            DataTable dt = new DataTable();
            
            try
            {
                dt = GetReferralCPT(_CPTID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtCode.Tag = _CPTID;
                    txtCode.Text=Convert.ToString(dt.Rows[0]["sCPTCode"]);
                    txtDescription.Text=Convert.ToString(dt.Rows[0]["sDescription"]);
                    chkIsReferralCPT.Checked = Convert.ToBoolean(dt.Rows[0]["bIsReferral"]);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private DataTable GetReferralCPT(Int64 nCPTID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = new DataTable();
            string strSQL = "";
            try
            {
                oDB.Connect(false);

                strSQL = " SELECT nCPTID, sCPTCode, sDescription, bIsReferral, nClinicID  "+
                         " FROM  BL_ReferralCPT_MST  "+
                         " WHERE (nCPTID = " + nCPTID + ")";

                oDB.Retrive_Query(strSQL, out dt);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            return dt;
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (txtCode.Text.Trim() != "")
                //{
                //    txtDescription.Text = GetCPTDescription(txtCode.Text.Trim());
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #endregion " Private Methods"
    
        #region " New Methods "

        private void DesignGrid()
        {
            try
            {
                int _width = 0;
                #region " CPT Grid "

                c1CPT.Rows.Count = 1;
                c1CPT.Rows.Fixed = 1;
                c1CPT.Cols.Count = COL_COUNT;
                c1CPT.Cols.Fixed = 0;

                c1CPT.SetData(0, COL_ID, "ID");
                c1CPT.SetData(0, COL_CODE, "Code");
                c1CPT.SetData(0, COL_DESC, "Description");

                c1CPT.Cols[COL_ID].Visible = false;
                c1CPT.Cols[COL_CODE].Visible = true;
                c1CPT.Cols[COL_DESC].Visible = true;

                c1CPT.Cols[COL_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1CPT.Cols[COL_DESC].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                _width = pnlCPTGrid.Width - 2;

                c1CPT.Cols[COL_ID].Width = 0;
                c1CPT.Cols[COL_CODE].Width = Convert.ToInt32(_width * 0.4);
                c1CPT.Cols[COL_DESC].Width = Convert.ToInt32(_width * 0.60);


              
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
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
                ogloGridListControl.ItemSelected +=new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                ogloGridListControl.InternalGridKeyDown += new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);
                ogloGridListControl.ControlHeader = ControlHeader;
                pnlInternalControl.Controls.Add(ogloGridListControl);
                ogloGridListControl.Dock = DockStyle.Fill;
                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, SearchColumn.Code);
                }
                ogloGridListControl.Show();
             

                if (ogloGridListControl.ControlType == gloGridListControlType.CPT)
                    pnlInternalControl.SetBounds(pnlCPTGrid.Location.X, pnlCPTGrid.Location.Y, 0, 0, BoundsSpecified.Location);
               

                pnlInternalControl.Visible = true;
                pnlInternalControl.BringToFront();
                ogloGridListControl.Focus();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            {
                //RePositionInternalControl();
            }
            return _result;
        }

        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changed on 4/2/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }
                if (ogloGridListControl != null)
                {
                    try
                    {
                        ogloGridListControl.ItemSelected -= new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
                        ogloGridListControl.InternalGridKeyDown -= new gloGridListControl.Key_Down(ogloGridListControl_InternalGridKeyDown);

                    }
                    catch { }
                    ogloGridListControl.Dispose();
                    ogloGridListControl = null;
                }
                pnlInternalControl.Visible = false;
                pnlInternalControl.SendToBack();
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
            }
            finally
            { }
            return _result;
        }

        void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
        }

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {
            try
            {
                if (ogloGridListControl.SelectedItems != null)
                {
                    if (ogloGridListControl.SelectedItems.Count > 0)
                    {

                        switch (ogloGridListControl.ControlType)
                        {

                            case gloGridListControlType.CPT:
                                {

                                    //Added By Pramod Nair For Avoiding Duplicate Insertion Of CPT
                                    Boolean isExists = false;
                                    for (int j = 0; j <= c1CPT.Rows.Count - 1; j++)
                                    {
                                        if (c1CPT.GetData(j, 1).ToString() == ogloGridListControl.SelectedItems[0].Code.ToString())
                                        {
                                            isExists = true;
                                            break;
                                        }

                                    }
                                    //End 

                                    if (!isExists)
                                    {
                                        C1.Win.C1FlexGrid.Row oNewRow = c1CPT.Rows.Add();
                                        c1CPT.SetData(oNewRow.Index, COL_ID, ogloGridListControl.SelectedItems[0].ID);
                                        c1CPT.SetData(oNewRow.Index, COL_CODE, ogloGridListControl.SelectedItems[0].Code);
                                        c1CPT.SetData(oNewRow.Index, COL_DESC, ogloGridListControl.SelectedItems[0].Description);
                                    }
                                }
                                break;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                c1CPTSearchGrid.Clear(C1.Win.C1FlexGrid.ClearFlags.Content, 0, 0);
                CloseInternalControl();
            }
        }

        private void Fill_ReferralCPTs()
        {
            gloReferralCPT ogloReferralCPT = new gloReferralCPT(_databaseconnectionstring);
            try
            {
                ReferralCPT oReferralCPT = new ReferralCPT();
                oReferralCPT = ogloReferralCPT.GetReferralCPT(_CPTID);

                if (oReferralCPT != null)
                {
                    if (oReferralCPT.CPTs != null)
                    {
                        for (int i = 0; i < oReferralCPT.CPTs.Count; i++)
                        {
                            C1.Win.C1FlexGrid.Row oNewRow = c1CPT.Rows.Add();
                            c1CPT.SetData(oNewRow.Index, COL_ID, oReferralCPT.CPTs[i].ID);
                            c1CPT.SetData(oNewRow.Index, COL_CODE, oReferralCPT.CPTs[i].Code);
                            c1CPT.SetData(oNewRow.Index, COL_DESC, oReferralCPT.CPTs[i].Description);
                        }
                        chkIsReferralCPT.Checked = oReferralCPT.IsReferralRequired;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloReferralCPT != null)
                {
                    ogloReferralCPT.Dispose();
                }
            }
        }

        private bool ValidationForReferralRequired()
        {
            bool _result = true;
            
            try
            {
                if (chkIsReferralCPT.Checked == false)
                {
                    if (MessageBox.Show("Referral Physician checkbox is not checked. Do you want to continue?  ", _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        _result = true;
                    }
                    else
                    {
                        _result = false;
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
            return _result;
        }

        #endregion " New Methods "

        #region " C1 and Other Events "

        private void c1CPTSearchGrid_ChangeEdit(object sender, EventArgs e)
        {
            try
            {
                if (ogloGridListControl != null)
                {
                    string _strSearchString = c1CPTSearchGrid.Editor.Text;

                    if (ogloGridListControl.ControlHeader == "ICD9")
                    {
                        if (_strSearchString.Length == 3)
                        {
                            _strSearchString = _strSearchString + ".";
                        }
                        else if (_strSearchString.Length > 3)
                        {
                            if (_strSearchString.Substring(3, 1).ToString() != ".")
                            {
                                string _PeriodSearch = _strSearchString.Substring(0, 3) + "." + _strSearchString.Substring(3, _strSearchString.Length - 3);
                                _strSearchString = _PeriodSearch;
                            }
                        }
                    }
                    ogloGridListControl.FillControl(_strSearchString);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void c1CPTSearchGrid_StartEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                OpenInternalControl(gloGridListControlType.CPT, "CPT", false, 0, 0, "");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void c1CPTSearchGrid_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            e.SuppressKeyPress = true;
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
                        }
                        break;
                    case Keys.Delete:
                        {

                        }
                        break;
                    case Keys.Down:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {
                                    ogloGridListControl.Focus();
                                }
                            }
                        }
                        break;
                    case Keys.Escape:
                        {
                            e.SuppressKeyPress = true;
                            if (pnlInternalControl.Visible)
                            {
                                if (ogloGridListControl != null)
                                {
                                    CloseInternalControl();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void btnSelectCPT_Click(object sender, EventArgs e)
        {
            try
            {
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
                else
                {
                    OpenInternalControl(gloGridListControlType.CPT, "CPT", false, 0, 0, "");
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void btnDeleteCPT_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1CPT.Rows.Count > 1)
                {
                    c1CPT.Rows.Remove(c1CPT.Row);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_Yellow;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_Button;
            ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void tsb_OK_Click(object sender, EventArgs e)
        {
            gloReferralCPT ogloReferralCPT = new gloReferralCPT(_databaseconnectionstring);
            try
            {
                if (c1CPT.Rows.Count > 1)
                {
                    if (ValidationForReferralRequired())
                    {
                        ReferralCPT oReferralCPT = new ReferralCPT();
                        oReferralCPT.ReferralCPTID = _CPTID;
                        oReferralCPT.ClinicID = this.ClinicID;
                        oReferralCPT.IsReferralRequired = chkIsReferralCPT.Checked;

                        for (int k = 1; k < c1CPT.Rows.Count; k++)
                        {
                            oReferralCPT.CPTs.Add(Convert.ToInt64(c1CPT.GetData(k, COL_ID)), Convert.ToString(c1CPT.GetData(k, COL_CODE)), Convert.ToString(c1CPT.GetData(k, COL_DESC)));
                        }

                        ogloReferralCPT.Add(oReferralCPT);
                        this.Close();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Add atleast one CPT. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloReferralCPT.Dispose();
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion " C1 and Other Events "

        private void c1CPT_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            gloReferralCPT ogloReferralCPT = new gloReferralCPT(_databaseconnectionstring);
            try
            {
                if (c1CPT.Rows.Count > 1)
                {
                    if (ValidationForReferralRequired())
                    {
                        ReferralCPT oReferralCPT = new ReferralCPT();
                        oReferralCPT.ReferralCPTID = _CPTID;
                        oReferralCPT.ClinicID = this.ClinicID;
                        oReferralCPT.IsReferralRequired = chkIsReferralCPT.Checked;

                        for (int k = 1; k < c1CPT.Rows.Count; k++)
                        {
                            oReferralCPT.CPTs.Add(Convert.ToInt64(c1CPT.GetData(k, COL_ID)), Convert.ToString(c1CPT.GetData(k, COL_CODE)), Convert.ToString(c1CPT.GetData(k, COL_DESC)));
                        }

                        ogloReferralCPT.Add(oReferralCPT);
                        _CPTID = 0;
                        chkIsReferralCPT.Checked = false;
                        int i = c1CPT.Rows.Count;
                        if (i > 1)
                        {
                            c1CPT.Rows.RemoveRange(1, i-1);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Add atleast one CPT. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloReferralCPT.Dispose();
            }
        }
    }
}