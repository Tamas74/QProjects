using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using System.Data.OleDb;
using C1.Win.C1FlexGrid;

namespace gloBilling
{
    public partial class frmCPTMapping : Form
    {

        #region "Private Variables"

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "";

        //   private gloListControl.gloListControl oListControl;
        //  private gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

        private Int64 _CPTMappingId = 0;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private Int64 ID = 0;
        private string _CPTMappingName = "";
        System.Collections.ArrayList _DetailCPTID = new System.Collections.ArrayList();


        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public gloGridListControl ogloGridListControl = null;

        #endregion "Private Variables"

        #region "Properties"


        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        #endregion

        #region " Contructors "

        public frmCPTMapping(string DatabaseConnectionString)
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

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }

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



        public frmCPTMapping(Int64 CPTID, string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            ID = CPTID;

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


        #endregion

        #region  "Form Event"

        private void frmCPTMapping_Load(object sender, EventArgs e)
        {

            FillCPTMapping(ID);
            DesignGrid();
            panel5.Visible = false;
            panel7.Visible = false;
            if (ID != 0 )
            {
                panel5.Visible = true;
                panel7.Visible = true;
                FillInsurance(ID);
                DesignGridInsurance();

            }

        }

        #endregion  "Form Event"

        #region " Private Functions "

        private void DesignGridInsurance()
        {
            c1Insurance.Cols["sInsurancePlan"].Caption = "Insurance Plan";
            c1Insurance.Cols["Company"].Caption = "Insurance Company";
            c1Insurance.Cols["ReportingCategory"].Caption = "Reporting Category";
            c1Insurance.Cols["InsuranceTypeDesc"].Caption = "Insurance Type";



            //Set width for columns of grid
            int nWidth = c1Insurance.Width - 20;
            c1Insurance.Cols["sInsurancePlan"].Width = (int)(nWidth * 0.25);
            c1Insurance.Cols["Company"].Width = (int)(nWidth * 0.25);
            c1Insurance.Cols["ReportingCategory"].Width = (int)(nWidth * 0.25);
            c1Insurance.Cols["InsuranceTypeDesc"].Width = (int)(nWidth * 0.25);



        }

        private void FillInsurance(Int64 CPTMappingID)
        {
            DataTable dt = new DataTable();

            try
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _sqlRetrieveQuery = String.Empty;
                //if (IsAdmin() != true)
                //{
                _sqlRetrieveQuery = "select  ISNULL(Contacts_MST.sName, '')   " +
                      "AS sInsurancePlan,ISNULL(Contacts_InsuranceCompany_MST.sDescription, '') AS Company," +
                      "  ISNULL(Contacts_InsuranceReportingCategory_MST.sDescription, '') AS ReportingCategory, " +
                    " ISNULL(Contacts_Insurance_DTL.sInsuranceTypeDesc, '') AS InsuranceTypeDesc " +
                    " FROM         Contact_InsurancePlanReportingCat_Association INNER JOIN  " +
                     "  Contacts_InsuranceReportingCategory_MST ON   " +
                     " Contact_InsurancePlanReportingCat_Association.nReportingCategoryId = Contacts_InsuranceReportingCategory_MST.nID RIGHT OUTER JOIN  " +
                     " Contacts_MST ON Contact_InsurancePlanReportingCat_Association.nContactId = Contacts_MST.nContactID LEFT OUTER JOIN  " +
                     " Contacts_InsuranceCompany_MST INNER JOIN  " +
                     " Contact_InsurancePlan_Association ON Contacts_InsuranceCompany_MST.nID = Contact_InsurancePlan_Association.nCompanyId ON   " +
                     " Contacts_MST.nContactID = Contact_InsurancePlan_Association.nContactId LEFT OUTER JOIN  " +
                     " Contacts_Insurance_DTL ON Contacts_MST.nContactID = Contacts_Insurance_DTL.nContactID " +
                     " WHERE     (ISNULL(Contacts_MST.bIsBlocked, 0) = 0) AND (Contacts_MST.sContactType = 'Insurance') AND (ISNULL(Contacts_MST.nClinicID, 1) =" + _ClinicID + " ) " +
                     "AND ISNULL(Contacts_Insurance_DTL.nCPTMappingID,0) !=0  AND Contacts_Insurance_DTL.nCPTMappingID= " + ID;

                if (_sqlRetrieveQuery != "")
                {
                    oDB.Retrive_Query(_sqlRetrieveQuery, out dt);

                }
                c1Insurance.DataSource = dt.DefaultView;
                oDB.Disconnect();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void DesignGrid()
        {
            c1CPTMapping.Cols[2].Caption = "Original CPT";
            c1CPTMapping.Cols[3].Caption = "Description";
            c1CPTMapping.Cols[4].Caption = "Replacement CPT";
            c1CPTMapping.Cols[5].Caption = "Description";

            //Set visible true or false to show the columns on the form
            c1CPTMapping.Cols[0].Visible = false;
            c1CPTMapping.Cols[1].Visible = false;
            c1CPTMapping.Cols[2].Visible = true;
            c1CPTMapping.Cols[3].Visible = true;
            c1CPTMapping.Cols[4].Visible = true;
            c1CPTMapping.Cols[5].Visible = true;
            c1CPTMapping.Cols[6].Visible = false;
            c1CPTMapping.Cols[7].Visible = false;
            c1CPTMapping.Cols[8].Visible = false;
            c1CPTMapping.Cols[9].Visible = false;


            //Set width for columns of grid
            int nWidth = c1CPTMapping.Width - 20;
            c1CPTMapping.Cols[0].Width = 0;
            c1CPTMapping.Cols[1].Width = 0;
            c1CPTMapping.Cols[2].Width = (int)(nWidth * 0.15);
            c1CPTMapping.Cols[3].Width = (int)(nWidth * 0.35);
            c1CPTMapping.Cols[4].Width = (int)(nWidth * 0.15);
            c1CPTMapping.Cols[5].Width = (int)(nWidth * 0.35);
            c1CPTMapping.Cols[6].Width = 0;
            c1CPTMapping.Cols[7].Width = 0;
            c1CPTMapping.Cols[8].Width = 0;
            c1CPTMapping.Cols[9].Width = 0;

            c1CPTMapping.Cols[3].AllowEditing = false;
            c1CPTMapping.Cols[5].AllowEditing = false;
            c1CPTMapping.AllowSorting = AllowSortingEnum.None;

        }

        private void FillCPTMapping(Int64 ID)
        {
            DataTable dt = new DataTable();
            _CPTMappingId = 0;
            try
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _sqlRetrieveQuery = String.Empty;
                //if (IsAdmin() != true)
                //{
                _sqlRetrieveQuery = "SELECT distinct CPT_Mapping_DTL.nCPTMappingDetailsID, CPT_Mapping_MST.nCPTMappingID, CPT_Mapping_DTL.sCPTCode, CPT_MST.sDescription, " +
                     " CPT_Mapping_DTL.sMappingCPT, CPT_MST_1.sDescription AS sMappingDescription, CPT_Mapping_DTL.dtCreatedDate, CPT_Mapping_DTL.nUserID, " +
                     " CPT_Mapping_DTL.nClinicID ,CPT_Mapping_MST.sCPTMappingName" +
                     " FROM  CPT_Mapping_DTL LEFT OUTER JOIN " +
                     " CPT_MST AS CPT_MST_1 ON CPT_Mapping_DTL.sMappingCPT = CPT_MST_1.sCPTCode LEFT OUTER JOIN " +
                     " CPT_MST ON CPT_Mapping_DTL.sCPTCode = CPT_MST.sCPTCode right outer JOIN CPT_Mapping_MST ON  " +
                     " CPT_Mapping_MST.nCPTMappingID=CPT_Mapping_DTL.nCPTMappingID WHERE CPT_Mapping_MST.nCPTMappingID=" + ID.ToString() + " order by CPT_Mapping_DTL.sCPTCode";

                if (_sqlRetrieveQuery != "")
                {
                    oDB.Retrive_Query(_sqlRetrieveQuery, out dt);

                }
                c1CPTMapping.DataSource = dt.DefaultView;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtcptmapping.Text = dt.Rows[0]["sCPTMappingName"].ToString().Trim();
                        ts_btnRemoveLine.Enabled = true;
                        _CPTMappingName = dt.Rows[0]["sCPTMappingName"].ToString().Trim();
                        if (dt.Rows[0]["nCPTMappingDetailsID"].ToString().Trim() != "")
                        {
                            _CPTMappingId = Convert.ToInt64(dt.Rows[0]["nCPTMappingID"].ToString());
                        }
                        else
                        {
                            _CPTMappingId = Convert.ToInt64(dt.Rows[0]["nCPTMappingID"].ToString());
                            c1CPTMapping.Rows.Remove(c1CPTMapping.RowSel);
                            ts_btnRemoveLine.Enabled = false;
                        }
                    }
                    else
                    {
                        ts_btnRemoveLine.Enabled = false;
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private bool SaveCodes()
        {
            bool _retVal = true;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            string _Flag = "";
            //  string _CPTMappingName="";
            string _CPTCode = "";
            string _MappingCPTCode = "";
            Int64 _DetailCPTID = 0;
            string _sqlstring = "";
            object _objCPTMappingId = null;
            object _count = null;
            oDB.Connect(false);
            try
            {
                if (_CPTMappingId != 0)
                {
                    _Flag = "UpdateCPTCrosswalk";

                    if (_CPTMappingName != txtcptmapping.Text.Trim())
                    {
                        //_CPTMappingName = txtcptmapping.Text.Replace("'","''").Trim();
                        _sqlstring = "SELECT count(sCPTMappingName) FROM CPT_Mapping_MST WHERE sCPTMappingName='" + txtcptmapping.Text.Replace("'", "''").Trim() + "'";
                        _count = oDB.ExecuteScalar_Query(_sqlstring);
                        if (Convert.ToInt64(_count.ToString()) > 0)
                        {
                            MessageBox.Show("CPT Billing Crosswalk Name is already in use by another entry. Enter a unique CPT Billing Crosswalk Name.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtcptmapping.Focus();
                            return false;
                        }
                    }

                    oDBParameters.Add("@Flag", _Flag, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@CPTMappingName", txtcptmapping.Text.Trim(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@CPTMappingID", _CPTMappingId, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@UserId", _UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@ClinicId", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDB.Execute("BL_CPTBillingCrosswalk", oDBParameters);
                    // _CPTMappingId = Convert.ToInt64(_objCPTMappingId.ToString());

                }
                else
                {
                    oDBParameters.Clear();

                    _Flag = "NewCPTCrosswalk";
                    _CPTMappingName = txtcptmapping.Text.Trim();
                    _sqlstring = "SELECT count(sCPTMappingName) FROM CPT_Mapping_MST WHERE sCPTMappingName='" + _CPTMappingName.Replace("'", "''").Trim() + "'";
                    _count = oDB.ExecuteScalar_Query(_sqlstring);
                    if (Convert.ToInt64(_count.ToString()) > 0)
                    {
                        MessageBox.Show("CPT Billing Crosswalk Name is already in use by another entry. Enter a unique CPT Billing Crosswalk Name.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtcptmapping.Focus();
                        return false;
                    }
                    else
                    {
                        oDBParameters.Add("@Flag", _Flag, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDBParameters.Add("@CPTMappingName", _CPTMappingName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDBParameters.Add("@CPTMappingID", _CPTMappingId, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@UserId", _UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@ClinicId", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDB.Execute("BL_CPTBillingCrosswalk", oDBParameters, out _objCPTMappingId);
                        _CPTMappingId = Convert.ToInt64(_objCPTMappingId.ToString());
                    }

                }
                for (int i = 1; c1CPTMapping.Rows.Count > i; i++)
                {
                    _Flag = "CPTDetails";
                    _CPTCode = c1CPTMapping.Rows[i]["sCPTCode"].ToString().Trim();
                    _MappingCPTCode = c1CPTMapping.Rows[i]["sMappingCPT"].ToString().Trim();
                    if (c1CPTMapping.Rows[i]["nCPTMappingDetailsID"].ToString().Trim().Length > 0)
                    {
                        _DetailCPTID = Convert.ToInt64(c1CPTMapping.Rows[i]["nCPTMappingDetailsID"].ToString().Trim());
                    }
                    oDBParameters.Clear();
                    oDBParameters.Add("@Flag", _Flag, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@CPTMappingID", _CPTMappingId, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@CPTCode", _CPTCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@MappingCPTCode", _MappingCPTCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@UserId", _UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@ClinicId", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@DetailsCPTMappingID", _DetailCPTID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                    oDB.Execute("BL_CPTBillingCrosswalk", oDBParameters);
                    _DetailCPTID = 0;
                    //_CPTMappingId = Convert.ToInt64(_objCPTMappingId.ToString());
                }

                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                _retVal = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
            }

            return _retVal;
        }

        private bool ValidateSave()
        {
            bool _retVal = true;

            try
            {
                if (txtcptmapping.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Enter CPT billing crosswalk name. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtcptmapping.Focus();
                    return false;
                }

                if (c1CPTMapping != null && c1CPTMapping.Rows.Count > 0)
                {
                    for (int rowIndex = 1; rowIndex < c1CPTMapping.Rows.Count; rowIndex++)
                    {
                        if ((c1CPTMapping.GetData(rowIndex, 2) == null || Convert.ToString(c1CPTMapping.GetData(rowIndex, 2)).Trim() == "") && (c1CPTMapping.GetData(rowIndex, 4) == null || Convert.ToString(c1CPTMapping.GetData(rowIndex, 4)).Trim() == "") && c1CPTMapping.Rows.Count - 1 == rowIndex)
                        {
                            c1CPTMapping.Rows.Remove(rowIndex);
                            goto final;
                        }
                        if (c1CPTMapping.GetData(rowIndex, 2) == null || Convert.ToString(c1CPTMapping.GetData(rowIndex, 2)).Trim() == "")
                        {
                            MessageBox.Show("Enter original CPT for line number " + rowIndex.ToString() + ". ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _retVal = false;
                            c1CPTMapping.Focus();
                            c1CPTMapping.Select(rowIndex, 2, true);
                            break;
                        }
                        if (c1CPTMapping.GetData(rowIndex, 4) == null || Convert.ToString(c1CPTMapping.GetData(rowIndex, 4)).Trim() == "")
                        {
                            MessageBox.Show("Enter replacement CPT for line number " + rowIndex.ToString() + ". ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _retVal = false;
                            c1CPTMapping.Focus();
                            c1CPTMapping.Select(rowIndex, 4, true);
                            break;
                        }


                    }


                }

            }
            catch (Exception ex)
            {
                _retVal = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex.ToString();
            }
        final:
            return _retVal;
        }

        #endregion " Private Functions "

        #region "Grid List Control"

        void ogloGridListControl_InternalGridKeyDown(object sender, EventArgs e)
        {
            try
            {
                CloseInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            { }
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
                pnlInternalControl.Controls.Add(ogloGridListControl);
                ogloGridListControl.Dock = DockStyle.Fill;
                if (SearchText != "")
                {
                    ogloGridListControl.Search(SearchText, SearchColumn.Code);
                }
                ogloGridListControl.Show();

                int _x = c1CPTMapping.Cols[ColIndex].Left;
                int _y = c1CPTMapping.Rows[RowIndex].Bottom + 70;
                int _width = pnlInternalControl.Width;
                int _height = pnlInternalControl.Height;



                int _parentleft = pnlInternalControl.Parent.Bounds.Left;
                int _parentwidth = pnlInternalControl.Parent.Bounds.Width;
                int _diffFactor = _parentwidth - _x;

                if (_diffFactor < _width)
                {
                    _x = pnlInternalControl.Parent.Bounds.Width + (_diffFactor);
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }
                else
                {
                    pnlInternalControl.SetBounds(_x, _y, 0, 0, BoundsSpecified.Location);
                }

                //pnlInternalControl.SetBounds(c1CPTMapping.Cols[ColIndex].Left, _y + c1CPTMapping.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
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
                RePositionInternalControl();
            }
            return _result;
        }

        private bool CloseInternalControl()
        {
            bool _result = false;
            try
            {
                //SLR: Changed on 2/4/2014
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0;  i--)
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

        private void RePositionInternalControl()
        {
            try
            {
                //int i = c1CPTMapping.Rows[c1CPTMapping.RowSel].Bottom;
                //if (pnlInternalControl.Visible == true && ogloGridListControl != null)
                //{
                //    pnlInternalControl.SetBounds((c1CPTMapping.Cols[c1CPTMapping.ColSel].Left + c1CPTMapping.ScrollPosition.X), c1CPTMapping.Rows[c1CPTMapping.RowSel].Bottom, 0, 0, BoundsSpecified.Location);
                //}
                if (c1CPTMapping.Parent.Bottom - c1CPTMapping.Rows[c1CPTMapping.RowSel].Bottom-60 < pnlInternalControl.Height)
                {
                    //pnlInternalControl.Height = (c1CPTMapping.Parent.Bottom - c1CPTMapping.Rows[c1CPTMapping.RowSel].Bottom) - c1CPTMapping.ScrollPosition.Y;
                    pnlInternalControl.SetBounds((c1CPTMapping.Cols[c1CPTMapping.ColSel].Left + c1CPTMapping.ScrollPosition.X), (c1CPTMapping.Rows[c1CPTMapping.RowSel].Top - pnlInternalControl.Height) + c1CPTMapping.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);

                }
                else
                {
                    //pnlInternalControl.Height = (c1CPTMapping.Rows[c1CPTMapping.RowSel].Top - c1CPTMapping.Parent.Top) + c1CPTMapping.ScrollPosition.Y; 
                    pnlInternalControl.SetBounds(c1CPTMapping.Cols[c1CPTMapping.ColSel].Left, c1CPTMapping.Rows[c1CPTMapping.RowSel].Bottom + c1CPTMapping.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {

            #region "Custom Event"
            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            #endregion
            int COL_CPTCODE = 2;
            int COL_CPTDescription = 3;
            int COL_MAPCPTCODE = 4;
            int COL_MAPCPTDescription = 5;
            try
            {

                int _rowIndex = 0;
                switch (c1CPTMapping.ColSel)
                {
                    case 2:
                        if (ogloGridListControl.SelectedItems != null && ogloGridListControl.SelectedItems.Count > 0)
                        {
                            //...Check if code exists
                            bool _isExistsCode = false;
                            if (c1CPTMapping != null && c1CPTMapping.Rows.Count > 1)
                            {
                                for (int rIndex = 1; rIndex < c1CPTMapping.Rows.Count; rIndex++)
                                {
                                    if (rIndex != ogloGridListControl.ParentRowIndex)
                                    {
                                        if (c1CPTMapping.GetData(rIndex, COL_CPTCODE) != null && Convert.ToString(c1CPTMapping.GetData(rIndex, COL_CPTCODE)).Trim() != ""
                                            && Convert.ToString(c1CPTMapping.GetData(rIndex, COL_CPTCODE)).Trim().ToUpper() == ogloGridListControl.SelectedItems[0].Code.Trim().ToUpper())
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
                                c1CPTMapping.SetData(_rowIndex, COL_CPTCODE, ogloGridListControl.SelectedItems[0].Code.Trim());
                                c1CPTMapping.SetData(_rowIndex, COL_CPTDescription, ogloGridListControl.SelectedItems[0].Description.Trim());
                                c1CPTMapping.Focus();
                                c1CPTMapping.Select(_rowIndex, COL_MAPCPTCODE, true);

                            }
                            else
                            {
                                MessageBox.Show("CPT code already exists.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _rowIndex = ogloGridListControl.ParentRowIndex;
                                c1CPTMapping.SetData(_rowIndex, COL_CPTCODE, null);
                                c1CPTMapping.SetData(_rowIndex, COL_CPTDescription, null);
                                c1CPTMapping.Select(_rowIndex, COL_CPTCODE, true);
                            }
                        }
                        else
                        {
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            c1CPTMapping.SetData(_rowIndex, COL_CPTCODE, null);
                            c1CPTMapping.SetData(_rowIndex, COL_CPTDescription, null);
                            c1CPTMapping.Focus();
                            c1CPTMapping.Select(_rowIndex, COL_CPTCODE, true);
                            //c1CPTMapping.Select(_rowIndex, COL_MAPCPTCODE, true);

                        }
                        break;
                    case 4:
                        if (ogloGridListControl.SelectedItems != null && ogloGridListControl.SelectedItems.Count > 0)
                        {
                            //...Check if code exists
                            bool _isExistsCode = false;
                            //if (c1CPTMapping != null && c1CPTMapping.Rows.Count > 1)
                            //{
                            //    for (int rIndex = 1; rIndex < c1CPTMapping.Rows.Count; rIndex++)
                            //    {
                            //        if (rIndex != ogloGridListControl.ParentRowIndex)
                            //        {
                            //            if (c1CPTMapping.GetData(rIndex, COL_MAPCPTCODE) != null && Convert.ToString(c1CPTMapping.GetData(rIndex, COL_CPTCODE)).Trim() != ""
                            //                && Convert.ToString(c1CPTMapping.GetData(rIndex, COL_MAPCPTCODE)).Trim().ToUpper() == ogloGridListControl.SelectedItems[0].Code.Trim().ToUpper())
                            //            {
                            //                _isExistsCode = true;
                            //                break;
                            //            }
                            //        }
                            //    }
                            //}

                            if (_isExistsCode == false)
                            {
                                _rowIndex = ogloGridListControl.ParentRowIndex;
                                c1CPTMapping.SetData(_rowIndex, COL_MAPCPTCODE, ogloGridListControl.SelectedItems[0].Code.Trim());
                                c1CPTMapping.SetData(_rowIndex, COL_MAPCPTDescription, ogloGridListControl.SelectedItems[0].Description.Trim());
                                c1CPTMapping.Focus();

                                if (_rowIndex == c1CPTMapping.Rows.Count - 1)
                                {
                                    ts_btnAddLine_Click(null, null);
                                }
                                else
                                {
                                    c1CPTMapping.Select(_rowIndex + 1, COL_CPTCODE);
                                }


                            }
                            else
                            {
                                MessageBox.Show("CPT code already exists.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _rowIndex = ogloGridListControl.ParentRowIndex;
                                c1CPTMapping.SetData(_rowIndex, COL_MAPCPTCODE, null);
                                c1CPTMapping.SetData(_rowIndex, COL_MAPCPTDescription, null);
                                c1CPTMapping.Select(_rowIndex, COL_MAPCPTCODE, true);
                            }
                        }
                        else
                        {
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            c1CPTMapping.SetData(_rowIndex, COL_MAPCPTCODE, null);
                            c1CPTMapping.SetData(_rowIndex, COL_MAPCPTDescription, null);
                            c1CPTMapping.Focus();
                            c1CPTMapping.Select(_rowIndex, COL_MAPCPTCODE, true);

                            //if (_rowIndex == c1CPTMapping.Rows.Count)
                            //{
                            //    ts_btnAddLine_Click(null, null);
                            //}
                            //else
                            //{
                            //    c1CPTMapping.Select(_rowIndex + 1, COL_CPTCODE);
                            //}
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                CloseInternalControl();
            }
        }
        #endregion

        #region " C1 Grid Events "

        private void c1CPTMapping_KeyUp(object sender, KeyEventArgs e)
        {
            //int _id = 0;
            string _code = "";
            string _description = "";
            bool _isdeleted = true;
            int COL_CPTCODE = 2;
            int COL_CPTDescription = 3;
            int COL_MAPCPTCODE = 4;
            int COL_MAPCPTDescription = 5;

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

                            if (c1CPTMapping.RowSel > 0)
                            {

                            }
                        }
                    }
                    #endregion
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    //CellNote oCellNotes = null;

                    if (c1CPTMapping.GetData(c1CPTMapping.RowSel, COL_CPTCODE) != null)
                    {
                        _code = c1CPTMapping.GetData(c1CPTMapping.RowSel, COL_CPTCODE).ToString();
                    }
                    if (c1CPTMapping.GetData(c1CPTMapping.RowSel, COL_CPTDescription) != null)
                    {
                        _description = c1CPTMapping.GetData(c1CPTMapping.RowSel, COL_CPTDescription).ToString();
                    }

                    if (c1CPTMapping.GetData(c1CPTMapping.RowSel, COL_MAPCPTCODE) != null)
                    {
                        _code = c1CPTMapping.GetData(c1CPTMapping.RowSel, COL_MAPCPTCODE).ToString();
                    }
                    if (c1CPTMapping.GetData(c1CPTMapping.RowSel, COL_MAPCPTDescription) != null)
                    {
                        _description = c1CPTMapping.GetData(c1CPTMapping.RowSel, COL_MAPCPTDescription).ToString();
                    }

                    e2.oType = TransactionLineColumnType.None;

                    e.SuppressKeyPress = true;

                    #region "Delete Key"
                    switch (c1CPTMapping.ColSel)
                    {

                        case 2:
                            {

                                c1CPTMapping.SetData(c1CPTMapping.RowSel, c1CPTMapping.ColSel, "");
                                c1CPTMapping.SetData(c1CPTMapping.RowSel, c1CPTMapping.ColSel + 1, "");

                                //CellRange rg = c1CPTMapping.GetCellRange(c1CPTMapping.RowSel, c1CPTMapping.ColSel);
                                //rg.UserData = oCellNotes;
                                e2.oType = TransactionLineColumnType.CPT;

                            }
                            break;
                        case 3:
                            {

                                //c1CPTMapping.SetData(c1CPTMapping.RowSel, c1CPTMapping.ColSel, "");
                                //c1CPTMapping.SetData(c1CPTMapping.RowSel, c1CPTMapping.ColSel + 1, "");

                                c1CPTMapping.SetData(c1CPTMapping.RowSel, COL_CPTCODE, "");
                                c1CPTMapping.SetData(c1CPTMapping.RowSel, COL_CPTDescription, "");

                                //CellRange rg = c1CPTMapping.GetCellRange(c1CPTMapping.RowSel, c1CPTMapping.ColSel);
                                //rg.UserData = oCellNotes;
                                e2.oType = TransactionLineColumnType.CPT;

                            }
                            break;

                        case 4:
                            {

                                c1CPTMapping.SetData(c1CPTMapping.RowSel, c1CPTMapping.ColSel, "");
                                c1CPTMapping.SetData(c1CPTMapping.RowSel, c1CPTMapping.ColSel + 1, "");

                                //CellRange rg = c1CPTMapping.GetCellRange(c1CPTMapping.RowSel, c1CPTMapping.ColSel);
                                //rg.UserData = oCellNotes;
                                e2.oType = TransactionLineColumnType.CPT;

                            }
                            break;
                        case 5:
                            {

                                //c1CPTMapping.SetData(c1CPTMapping.RowSel, c1CPTMapping.ColSel, "");
                                //c1CPTMapping.SetData(c1CPTMapping.RowSel, c1CPTMapping.ColSel + 1, "");

                                c1CPTMapping.SetData(c1CPTMapping.RowSel, COL_MAPCPTCODE, "");
                                c1CPTMapping.SetData(c1CPTMapping.RowSel, COL_MAPCPTDescription, "");

                                //CellRange rg = c1CPTMapping.GetCellRange(c1CPTMapping.RowSel, c1CPTMapping.ColSel);
                                //rg.UserData = oCellNotes;
                                e2.oType = TransactionLineColumnType.CPT;

                            }
                            break;

                    }
                    _code = "";
                    e1 = new RowColEventArgs(c1CPTMapping.RowSel, c1CPTMapping.ColSel);
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex.ToString();
            }
            finally
            {

            }
        }

        private void c1CPTMapping_BeforeSelChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (ogloGridListControl != null)
                {
                    if (e.OldRange.r1 != e.NewRange.r1)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1CPTMapping_StartEdit(object sender, RowColEventArgs e)
        {
            //if (e.Row > 0)
            //{
            //    CellNote _cellNote = null;
            //    CellRange _cellRange = c1CPTMapping.GetCellRange(e.Row, e.Col);
            //    _cellRange.UserData = _cellNote;
            //}

            switch (e.Col)
            {

                case 2:
                    {
                        OpenInternalControl(gloGridListControlType.CPT, "CPT Code", false, e.Row, e.Col, "");
                        string _SearchText = "";
                        if (c1CPTMapping != null && c1CPTMapping.Rows.Count > 0)
                        {

                            _SearchText = Convert.ToString(c1CPTMapping.GetData(e.Row, 2));

                            if (_SearchText != "" && ogloGridListControl != null)
                            {
                                //ogloGridListControl.AdvanceSearch(_SearchText);
                                ogloGridListControl.FillControl(_SearchText);
                            }
                        }
                    }
                    break;
                case 3:
                    {
                        OpenInternalControl(gloGridListControlType.CPT, "CPT Code", false, e.Row, e.Col, "");
                        string _SearchText = "";
                        if (c1CPTMapping != null && c1CPTMapping.Rows.Count > 0)
                        {

                            _SearchText = Convert.ToString(c1CPTMapping.GetData(e.Row, 3));

                            if (_SearchText != "" && ogloGridListControl != null)
                            {
                                //ogloGridListControl.AdvanceSearch(_SearchText);
                                ogloGridListControl.FillControl(_SearchText);
                            }
                        }
                    }
                    break;
                case 4:
                    {
                        OpenInternalControl(gloGridListControlType.CPT, "CPT Code", false, e.Row, e.Col, "");
                        string _SearchText = "";
                        if (c1CPTMapping != null && c1CPTMapping.Rows.Count > 0)
                        {

                            _SearchText = Convert.ToString(c1CPTMapping.GetData(e.Row, 4));


                            if (_SearchText != "" && ogloGridListControl != null)
                            {
                                //ogloGridListControl.AdvanceSearch(_SearchText);
                                ogloGridListControl.FillControl(_SearchText);
                            }
                        }
                    }
                    break;
                case 5:
                    {
                        OpenInternalControl(gloGridListControlType.CPT, "CPT Code", false, e.Row, e.Col, "");
                        string _SearchText = "";
                        if (c1CPTMapping != null && c1CPTMapping.Rows.Count > 0)
                        {

                            _SearchText = Convert.ToString(c1CPTMapping.GetData(e.Row, 5));

                            if (_SearchText != "" && ogloGridListControl != null)
                            {
                                //ogloGridListControl.AdvanceSearch(_SearchText);
                                ogloGridListControl.FillControl(_SearchText);
                            }
                        }
                    }
                    break;
            }
        }

        private void c1CPTMapping_ChangeEdit(object sender, EventArgs e)
        {
            string _strSearchString = "";
            int COL_CPTCODE = 2;
            int COL_CPTDescription = 3;
            int COL_MAPCPTCODE = 4;
            int COL_MAPCPTDescription = 5;
            try
            {
                _strSearchString = c1CPTMapping.Editor.Text;

                if (ogloGridListControl != null)
                {

                    if (c1CPTMapping.Col == COL_CPTCODE || c1CPTMapping.Col == COL_CPTDescription)
                    {
                        string _COL_CODE = "";
                        string _COL_DESC = "";

                        if (c1CPTMapping != null && c1CPTMapping.Rows.Count > 0)
                        {
                            _COL_CODE = Convert.ToString(c1CPTMapping.GetData(c1CPTMapping.Row, COL_CPTCODE));
                            _COL_DESC = Convert.ToString(c1CPTMapping.GetData(c1CPTMapping.Row, COL_CPTDescription));
                            ogloGridListControl.SelectedCPTCode = _strSearchString;

                        }

                        if (c1CPTMapping.Col != COL_CPTCODE || c1CPTMapping.Col != COL_CPTDescription)
                        {
                            //if (_strSearchString.Length == 4)
                            //{
                            //    if (_strSearchString.EndsWith(".") == false)
                            //    { _strSearchString = _strSearchString.Insert(_strSearchString.Length - 1, "."); }
                            //}
                            //else if (_strSearchString.Length > 3)
                            //{
                            //    if (_strSearchString.Substring(3, 1).ToString() != ".")
                            //    {
                            //        string _PeriodSearch = _strSearchString.Substring(0, 3) + "." + _strSearchString.Substring(3, _strSearchString.Length - 3);
                            //        _strSearchString = _PeriodSearch;
                            //    }

                            //}
                        }
                        ogloGridListControl.FillControl(_strSearchString);
                        if (_strSearchString != "" && ogloGridListControl != null)
                        {
                            ogloGridListControl.AdvanceSearch(_strSearchString);
                        }
                    }



                    if (c1CPTMapping.Col == COL_MAPCPTCODE || c1CPTMapping.Col == COL_MAPCPTDescription)
                    {
                        string _COL_CODE = "";
                        string _COL_DESC = "";

                        if (c1CPTMapping != null && c1CPTMapping.Rows.Count > 0)
                        {
                            _COL_CODE = Convert.ToString(c1CPTMapping.GetData(c1CPTMapping.Row, COL_MAPCPTCODE));
                            _COL_DESC = Convert.ToString(c1CPTMapping.GetData(c1CPTMapping.Row, COL_MAPCPTDescription));
                            ogloGridListControl.SelectedCPTCode = _strSearchString;

                        }

                        if (c1CPTMapping.Col != COL_MAPCPTCODE || c1CPTMapping.Col != COL_MAPCPTDescription)
                        {
                            //if (_strSearchString.Length == 4)
                            //{
                            //    if (_strSearchString.EndsWith(".") == false)
                            //    { _strSearchString = _strSearchString.Insert(_strSearchString.Length - 1, "."); }
                            //}
                            //else if (_strSearchString.Length > 3)
                            //{
                            //    if (_strSearchString.Substring(3, 1).ToString() != ".")
                            //    {
                            //        string _PeriodSearch = _strSearchString.Substring(0, 3) + "." + _strSearchString.Substring(3, _strSearchString.Length - 3);
                            //        _strSearchString = _PeriodSearch;
                            //    }

                            //}
                        }
                        ogloGridListControl.FillControl(_strSearchString);
                        if (_strSearchString != "" && ogloGridListControl != null)
                        {
                            ogloGridListControl.AdvanceSearch(_strSearchString);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex.ToString();
            }
            finally
            {
            }

        }

        private void c1CPTMapping_Enter(object sender, EventArgs e)
        {
            //if (c1CPTMapping.Col == 2)
            //{
            //    c1CPTMapping.Select(c1CPTMapping.RowSel , 3, true);
            //}
            //if (c1CPTMapping.Col == 3)
            //{
            //    c1CPTMapping.Select(c1CPTMapping.RowSel, 4, true);
            //}
            //if (c1CPTMapping.Col == 4)
            //{
            //    c1CPTMapping.Select(c1CPTMapping.RowSel, 5, true);
            //}
            //if (c1CPTMapping.Col == 5)
            //{
            //    ts_btnAddLine_Click(null,null );
            //}
        }

        private void c1CPTMapping_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                if ((e.OldRange.c1 == 2) && (e.NewRange.c1 != 2) ||
                    (e.OldRange.c1 == 4) && (e.NewRange.c1 != 4) ||
                    (e.OldRange.c1 == 5) && (e.NewRange.c1 != 5))
                { CloseInternalControl(); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {

            }
        }

        private void c1CPTMapping_LeaveEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {
                ///here .... .. . .
                ///
                switch (e.Col)
                {
                    case 2:
                    case 4:
                        if (c1CPTMapping.Editor != null)
                        {
                            c1CPTMapping.ChangeEdit -= new System.EventHandler(this.c1CPTMapping_ChangeEdit);
                            c1CPTMapping.Editor.Text = "";
                            c1CPTMapping.ChangeEdit += new System.EventHandler(this.c1CPTMapping_ChangeEdit);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1CPTMapping_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (e.Col == 2)  //Check for CPT CODE if blank then change CPT DEsc to blank
                {
                    if (c1CPTMapping.GetData(c1CPTMapping.RowSel, 2) != null)
                    {
                        if (Convert.ToString(c1CPTMapping.GetData(c1CPTMapping.RowSel, 2)) == "")
                        {
                            c1CPTMapping.SetData(c1CPTMapping.RowSel, 3, "");
                        }
                    }
                }
                else if (e.Col == 4)  //Check for CPT CODE if blank then change CPT DEsc to blank
                {
                    if (c1CPTMapping.GetData(c1CPTMapping.RowSel, 4) != null)
                    {
                        if (Convert.ToString(c1CPTMapping.GetData(c1CPTMapping.RowSel, 4)) == "")
                        {
                            c1CPTMapping.SetData(c1CPTMapping.RowSel, 5, "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        #endregion " C1 Grid Events "

        #region " Tool Strip Control Events "

        private void ts_btnAddLine_Click(object sender, EventArgs e)
        {
            if (c1CPTMapping != null)
            {
                if (ogloGridListControl != null)
                {
                    CloseInternalControl();
                }
                c1CPTMapping.Rows.Add();
                ts_btnRemoveLine.Enabled = true;
                c1CPTMapping.Focus();
                c1CPTMapping.Select(c1CPTMapping.Rows.Count - 1, 2, true);


            }
            CloseInternalControl();

        }

        private void ts_btnRemoveLine_Click(object sender, EventArgs e)
        {
            // gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //  gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                if (c1CPTMapping != null)
                {
                    if (c1CPTMapping.RowSel > 0)
                    {

                        if (MessageBox.Show("Are you sure you want to remove selected line?", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //   oDB.Connect(false);
                            //     oDBParameters.Clear();
                            //    string _Flag = "RemoveLine";

                            if (c1CPTMapping.RowSel > 0)
                            {
                                if (c1CPTMapping.Rows[c1CPTMapping.RowSel]["nCPTMappingDetailsID"].ToString().Trim().Length > 0)
                                {
                                    _DetailCPTID.Add(Convert.ToInt64(c1CPTMapping.Rows[c1CPTMapping.RowSel]["nCPTMappingDetailsID"].ToString().Trim()));
                                }
                            }
                            //  oDBParameters.Add("@Flag", _Flag, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                            //  oDBParameters.Add("@CPTMappingID", _CPTMappingId, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                            //    oDBParameters.Add("@DetailsCPTMappingID", _DetailCPTID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            //     oDB.Execute("BL_CPTBillingCrosswalk", oDBParameters);
                            //    oDB.Disconnect();
                            c1CPTMapping.Rows.Remove(c1CPTMapping.RowSel);
                            c1CPTMapping.Update();
                            c1CPTMapping.Refresh();
                            if (c1CPTMapping.Rows.Count == 1)
                            {
                                ts_btnRemoveLine.Enabled = false;
                            }
                        }
                    }
                }
                CloseInternalControl();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
            }
        }

        private bool DeleteRow()
        {
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                string _Flag = "RemoveLine";
                oDB.Connect(false);
                foreach (Int64 i in _DetailCPTID)
                {
                    oDBParameters.Clear();
                    oDBParameters.Add("@Flag", _Flag, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    //  oDBParameters.Add("@CPTMappingID", _CPTMappingId, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@DetailsCPTMappingID", i, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDB.Execute("BL_CPTBillingCrosswalk", oDBParameters);
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return false;
            }
            finally
            {
            }
            return true;
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            this.Close();
        }

        private void ts_btnSaveClose_Click(object sender, EventArgs e)
        {
            c1CPTMapping.FinishEditing();
            if (ogloGridListControl != null)
            {
                CloseInternalControl();
            }


            if (ValidateSave())
            {
                if (DeleteRow())
                {
                    if (SaveCodes())
                        this.Close();
                }
            }

        }

        private void mnuBilling_AddLine_Click(object sender, EventArgs e)
        {
            ts_btnAddLine_Click(null, null);
        }

        private void mnuBilling_RemoveLine_Click(object sender, EventArgs e)
        {
            ts_btnRemoveLine_Click(null, null);
        }

        #endregion " Tool Strip Control Events "

        private void c1CPTMapping_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Tab)
                {
                    if (c1CPTMapping.Rows.Count == c1CPTMapping.RowSel + 1 && c1CPTMapping.ColSel == 5)
                    {
                        e.SuppressKeyPress = true;
                        TopToolStrip.Focus();
                        ts_btnAddLine.Select();
                    }
                }
            }
            catch (Exception)
            { }
        }

      
    }
}
