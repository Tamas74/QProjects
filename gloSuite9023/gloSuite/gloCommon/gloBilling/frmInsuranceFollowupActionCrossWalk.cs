using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace gloBilling
{
    public partial class frmInsuranceFollowupActionCrossWalk : Form
    {
        #region Private variable
        private Int64 _nInsCrosswalkID = 0;
        private string _sInsCrosswalkName = "";
        private string _Databaseconnectionstring = "";
        private string _Messageboxcaption = "";
        private Int64 _clinicID = 0;
        private Int64 _UserID = 0;

        System.Collections.ArrayList _DetailCPTID = new System.Collections.ArrayList();


        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public gloGridListControl ogloGridListControl = null;

        private const int COL_InsCrosswalkID = 0;
        private const int COL_InsCrosswalkDetailsID = 1;
        private const int COL_InsCrosswalkName = 2;
        private const int COL_OriginalFolloupID = 3;
        private const int COL_ORIGINALCODE = 4;
        private const int COL_Description = 5;
        private const int COL_ReplaceFollowupID = 6;
        private const int COL_REPLACECODE = 7;
        private const int COL_RDescription = 8;
        private const int COL_CreatedDate = 9;
        private const int COL_UserID = 10;
        private const int COL_ClinicID = 11;

        #endregion

        #region "Property"
        public Int64 ClinicID
        {
            get { return _clinicID; }
            set { _clinicID = value; }
        }
        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public Int64 nInsCrosswalkID
        {
            get { return _nInsCrosswalkID; }
            set { _nInsCrosswalkID = value; }
        }
        public string SInsCrosswalkName
        {
            get { return _sInsCrosswalkName; }
            set { _sInsCrosswalkName = value; }
        }
        #endregion

        public frmInsuranceFollowupActionCrossWalk(string DatabaseConnectionString)
        {
            InitializeComponent();

            _Databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicID = 0; }
            }
            else
            { _clinicID = 0; }

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
                    _Messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _Messageboxcaption = "gloPM";
                }
            }
            else
            { _Messageboxcaption = "gloPM"; }

            #endregion
        }



        public frmInsuranceFollowupActionCrossWalk(Int64 InsCrosswalkID, string DatabaseConnectionString)
        {
            InitializeComponent();
            _Databaseconnectionstring = DatabaseConnectionString;
            _nInsCrosswalkID = InsCrosswalkID;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _clinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _clinicID = 0; }
            }
            else
            { _clinicID = 0; }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _Messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _Messageboxcaption = "gloPM";
                }
            }
            else
            { _Messageboxcaption = "gloPM"; }

            #endregion
        }

        private void frmInsuranceFollowupActionCrossWalk_Load(object sender, EventArgs e)
        {
            FillFollowupcodeMapping(_nInsCrosswalkID);
            DesignGrid();
            panel5.Visible = false;
            panel7.Visible = false;

            ts_btnAddLine.Visible = false;
            ts_btnRemoveLine.Visible = false;
        }
        private void DesignGrid()
        {

            try
            {
                c1InsCrosswalk.Cols[COL_ORIGINALCODE].Caption = "Original Follow-up Code";
                c1InsCrosswalk.Cols[COL_Description].Caption = "Description";
                c1InsCrosswalk.Cols[COL_REPLACECODE].Caption = "Replacement Follow-up Code";
                c1InsCrosswalk.Cols[COL_RDescription].Caption = "Description";

                //Set visible true or false to show the columns on the form
                c1InsCrosswalk.Cols[COL_InsCrosswalkID].Visible = false;
                c1InsCrosswalk.Cols[COL_InsCrosswalkDetailsID].Visible = false;
                c1InsCrosswalk.Cols[COL_InsCrosswalkName].Visible = false;
                c1InsCrosswalk.Cols[COL_OriginalFolloupID].Visible = false;
                c1InsCrosswalk.Cols[COL_ORIGINALCODE].Visible = true;
                c1InsCrosswalk.Cols[COL_Description].Visible = true;
                c1InsCrosswalk.Cols[COL_ReplaceFollowupID].Visible = false;
                c1InsCrosswalk.Cols[COL_REPLACECODE].Visible = true;
                c1InsCrosswalk.Cols[COL_RDescription].Visible = true;
                c1InsCrosswalk.Cols[COL_CreatedDate].Visible = false;
                c1InsCrosswalk.Cols[COL_UserID].Visible = false;
                c1InsCrosswalk.Cols[COL_ClinicID].Visible = false;


                //Set width for columns of grid
                int nWidth = c1InsCrosswalk.Width - 20;
                c1InsCrosswalk.Cols[COL_InsCrosswalkID].Width = 0;
                c1InsCrosswalk.Cols[COL_InsCrosswalkDetailsID].Width = 0;
                c1InsCrosswalk.Cols[COL_InsCrosswalkName].Width = 0;
                c1InsCrosswalk.Cols[COL_OriginalFolloupID].Width = 0;
                c1InsCrosswalk.Cols[COL_ORIGINALCODE].Width = (int)(nWidth * 0.20);
                c1InsCrosswalk.Cols[COL_Description].Width = (int)(nWidth * 0.30);
                c1InsCrosswalk.Cols[COL_ReplaceFollowupID].Width = 0;
                c1InsCrosswalk.Cols[COL_REPLACECODE].Width = (int)(nWidth * 0.20);
                c1InsCrosswalk.Cols[COL_RDescription].Width = (int)(nWidth * 0.30);
                c1InsCrosswalk.Cols[COL_CreatedDate].Width = 0;
                c1InsCrosswalk.Cols[COL_UserID].Width = 0;
                c1InsCrosswalk.Cols[COL_ClinicID].Width = 0;

                c1InsCrosswalk.Cols[COL_ORIGINALCODE].AllowEditing = false;
                c1InsCrosswalk.Cols[COL_Description].AllowEditing = false;
                c1InsCrosswalk.Cols[COL_RDescription].AllowEditing = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            // c1InsCrosswalk.AllowSorting = AllowSortingEnum.None;

        }
        private void FillFollowupcodeMapping(Int64 ID)
        {
            DataTable dt = new DataTable();
            Int64 _nInsuranceID = 0;
          
            try
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_Databaseconnectionstring);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);


                oParameters.Add("nID", ID, System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_fillFollowupMapping", oParameters, out dt);



                c1InsCrosswalk.DataSource = dt.DefaultView;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtInsCrosswalk.Text = dt.Rows[0][COL_InsCrosswalkName].ToString().Trim();
                        _sInsCrosswalkName = dt.Rows[0][COL_InsCrosswalkName].ToString().Trim();
                        if (dt.Rows[0][COL_InsCrosswalkDetailsID].ToString().Trim() != "")
                        {
                            _nInsuranceID = Convert.ToInt64(dt.Rows[0][COL_InsCrosswalkID].ToString());
                        }
                        else
                        {
                            _nInsuranceID = Convert.ToInt64(dt.Rows[0][COL_InsCrosswalkID].ToString());
                            c1InsCrosswalk.Rows.Remove(c1InsCrosswalk.RowSel);

                        }
                    }
                    else
                    {
                        ts_btnRemoveLine.Enabled = false;
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex) // ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dt != null)
                {
                    dt = null;
                }
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

                int _x = c1InsCrosswalk.Cols[ColIndex].Left;
                int _y = c1InsCrosswalk.Rows[RowIndex].Bottom + 70;
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
                for (int i = pnlInternalControl.Controls.Count - 1; i >= 0; i--)
                {
                    pnlInternalControl.Controls.RemoveAt(i);
                }
                if (ogloGridListControl != null)
                {
                    try
                    {
                        //ogloGridListControl.ItemSelected -= new gloGridListControl.Item_Selected(ogloGridListControl_ItemSelected);
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
                if (c1InsCrosswalk.Parent.Bottom - c1InsCrosswalk.Rows[c1InsCrosswalk.RowSel].Bottom - 60 < pnlInternalControl.Height)
                {
                    //pnlInternalControl.Height = (c1CPTMapping.Parent.Bottom - c1CPTMapping.Rows[c1CPTMapping.RowSel].Bottom) - c1CPTMapping.ScrollPosition.Y;
                    pnlInternalControl.SetBounds((c1InsCrosswalk.Cols[c1InsCrosswalk.ColSel].Left + c1InsCrosswalk.ScrollPosition.X), (c1InsCrosswalk.Rows[c1InsCrosswalk.RowSel].Top - pnlInternalControl.Height) + c1InsCrosswalk.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);

                }
                else
                {
                    //pnlInternalControl.Height = (c1CPTMapping.Rows[c1CPTMapping.RowSel].Top - c1CPTMapping.Parent.Top) + c1CPTMapping.ScrollPosition.Y; 
                    pnlInternalControl.SetBounds(c1InsCrosswalk.Cols[c1InsCrosswalk.ColSel].Left, c1InsCrosswalk.Rows[c1InsCrosswalk.RowSel].Bottom + c1InsCrosswalk.ScrollPosition.Y, 0, 0, BoundsSpecified.Location);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

       
       
        private bool ValidateSave()
        {
            bool _retVal = true;

            try
            {
                if (txtInsCrosswalk.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Enter Insurance claim follow-up action crosswalk name. ", _Messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInsCrosswalk.Focus();
                    return false;
                }

                if (c1InsCrosswalk != null && c1InsCrosswalk.Rows.Count > 0)
                {
                    for (int rowIndex = 1; rowIndex < c1InsCrosswalk.Rows.Count; rowIndex++)
                    {
                        if ((c1InsCrosswalk.GetData(rowIndex, COL_ORIGINALCODE) == null || Convert.ToString(c1InsCrosswalk.GetData(rowIndex, COL_ORIGINALCODE)).Trim() == "") && (c1InsCrosswalk.GetData(rowIndex, COL_REPLACECODE) == null || Convert.ToString(c1InsCrosswalk.GetData(rowIndex, COL_REPLACECODE)).Trim() == "") && c1InsCrosswalk.Rows.Count - 1 == rowIndex)
                        {
                            c1InsCrosswalk.Rows.Remove(rowIndex);
                            goto final;
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

        

        void ogloGridListControl_ItemSelected(object sender, EventArgs e)
        {

            #region "Custom Event"
            TrnCtrlColValChangeEventArg e2 = new TrnCtrlColValChangeEventArg();
            #endregion

            try
            {

                int _rowIndex = 0;
                switch (c1InsCrosswalk.ColSel)
                {
                    case COL_REPLACECODE:
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
                                c1InsCrosswalk.SetData(_rowIndex, COL_ReplaceFollowupID, ogloGridListControl.SelectedItems[0].ID.ToString());
                                c1InsCrosswalk.SetData(_rowIndex, COL_REPLACECODE, ogloGridListControl.SelectedItems[0].Code.Trim());
                                c1InsCrosswalk.SetData(_rowIndex, COL_RDescription, ogloGridListControl.SelectedItems[0].Description.Trim());
                                c1InsCrosswalk.Focus();

                                if (_rowIndex == c1InsCrosswalk.Rows.Count - 1)
                                {
                                    // ts_btnAddLine_Click(null, null);
                                }
                                else
                                {
                                    c1InsCrosswalk.Select(_rowIndex + 1, COL_ORIGINALCODE);
                                }


                            }
                            else
                            {
                                MessageBox.Show("CPT code already exists.", _Messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _rowIndex = ogloGridListControl.ParentRowIndex;
                                c1InsCrosswalk.SetData(_rowIndex, COL_ReplaceFollowupID, null);
                                c1InsCrosswalk.SetData(_rowIndex, COL_REPLACECODE, null);
                                c1InsCrosswalk.SetData(_rowIndex, COL_RDescription, null);
                                c1InsCrosswalk.Select(_rowIndex, COL_REPLACECODE, true);
                            }
                        }
                        else
                        {
                            _rowIndex = ogloGridListControl.ParentRowIndex;
                            c1InsCrosswalk.SetData(_rowIndex, COL_ReplaceFollowupID, null);
                            c1InsCrosswalk.SetData(_rowIndex, COL_REPLACECODE, null);
                            c1InsCrosswalk.SetData(_rowIndex, COL_RDescription, null);
                            c1InsCrosswalk.Focus();
                            c1InsCrosswalk.Select(_rowIndex, COL_REPLACECODE, true);

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

        private void c1InsCrosswalk_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            try
            {

                if (e.Col == COL_REPLACECODE)  //Check for Replace CODE if blank then change Replace DEsc to blank
                {
                    if (c1InsCrosswalk.GetData(c1InsCrosswalk.RowSel, COL_REPLACECODE) != null)
                    {
                        if (Convert.ToString(c1InsCrosswalk.GetData(c1InsCrosswalk.RowSel, COL_REPLACECODE)) == "")
                        {
                            c1InsCrosswalk.SetData(c1InsCrosswalk.RowSel, COL_RDescription, "");
                            c1InsCrosswalk.SetData(c1InsCrosswalk.RowSel, COL_ReplaceFollowupID, 0);
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

        private void c1InsCrosswalk_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                if ((e.OldRange.c1 == COL_ORIGINALCODE) && (e.NewRange.c1 != COL_ORIGINALCODE) ||
                    (e.OldRange.c1 == COL_REPLACECODE) && (e.NewRange.c1 != COL_REPLACECODE) ||
                    (e.OldRange.c1 == COL_RDescription) && (e.NewRange.c1 != COL_RDescription))
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

        private void c1InsCrosswalk_BeforeSelChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
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

        private void c1InsCrosswalk_ChangeEdit(object sender, EventArgs e)
        {
            string _strSearchString = "";

            try
            {
                _strSearchString = c1InsCrosswalk.Editor.Text;

                if (ogloGridListControl != null)
                {

                    //if (c1InsCrosswalk.Col == COL_ORIGINALCODE || c1InsCrosswalk.Col == COL_Description)
                    //{
                    //    string _COL_CODE = "";
                    //    string _COL_DESC = "";

                    //    if (c1InsCrosswalk != null && c1InsCrosswalk.Rows.Count > 0)
                    //    {
                    //        _COL_CODE = Convert.ToString(c1InsCrosswalk.GetData(c1InsCrosswalk.Row, COL_ORIGINALCODE));
                    //        _COL_DESC = Convert.ToString(c1InsCrosswalk.GetData(c1InsCrosswalk.Row, COL_Description));
                    //        ogloGridListControl.SelectedCPTCode = _strSearchString;

                    //    }

                    //    if (c1InsCrosswalk.Col != COL_ORIGINALCODE || c1InsCrosswalk.Col != COL_Description)
                    //    {
                    //        //if (_strSearchString.Length == 4)
                    //        //{
                    //        //    if (_strSearchString.EndsWith(".") == false)
                    //        //    { _strSearchString = _strSearchString.Insert(_strSearchString.Length - 1, "."); }
                    //        //}
                    //        //else if (_strSearchString.Length > 3)
                    //        //{
                    //        //    if (_strSearchString.Substring(3, 1).ToString() != ".")
                    //        //    {
                    //        //        string _PeriodSearch = _strSearchString.Substring(0, 3) + "." + _strSearchString.Substring(3, _strSearchString.Length - 3);
                    //        //        _strSearchString = _PeriodSearch;
                    //        //    }

                    //        //}
                    //    }
                    //    ogloGridListControl.FillControl(_strSearchString);
                    //    if (_strSearchString != "" && ogloGridListControl != null)
                    //    {
                    //        ogloGridListControl.AdvanceSearch(_strSearchString);
                    //    }
                    //}



                    if (c1InsCrosswalk.Col == COL_REPLACECODE || c1InsCrosswalk.Col == COL_RDescription)
                    {
                        string _COL_CODE = "";
                        string _COL_DESC = "";

                        if (c1InsCrosswalk != null && c1InsCrosswalk.Rows.Count > 0)
                        {
                            _COL_CODE = Convert.ToString(c1InsCrosswalk.GetData(c1InsCrosswalk.Row, COL_REPLACECODE));
                            _COL_DESC = Convert.ToString(c1InsCrosswalk.GetData(c1InsCrosswalk.Row, COL_RDescription));
                            ogloGridListControl.SelectedCPTCode = _strSearchString;

                        }

                        if (c1InsCrosswalk.Col != COL_REPLACECODE || c1InsCrosswalk.Col != COL_RDescription)
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

        private void c1InsCrosswalk_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Tab)
                {
                    if (c1InsCrosswalk.Rows.Count == c1InsCrosswalk.RowSel + 1 && c1InsCrosswalk.ColSel == 5)
                    {
                        e.SuppressKeyPress = true;
                        TopToolStrip.Focus();
                        ts_btnAddLine.Select();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void c1InsCrosswalk_KeyUp(object sender, KeyEventArgs e)
        {
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

                            if (c1InsCrosswalk.RowSel > 0)
                            {

                            }
                        }
                    }
                    #endregion
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    //CellNote oCellNotes = null;

                    if (c1InsCrosswalk.GetData(c1InsCrosswalk.RowSel, COL_ORIGINALCODE) != null)
                    {
                        _code = c1InsCrosswalk.GetData(c1InsCrosswalk.RowSel, COL_ORIGINALCODE).ToString();
                    }
                    if (c1InsCrosswalk.GetData(c1InsCrosswalk.RowSel, COL_Description) != null)
                    {
                        _description = c1InsCrosswalk.GetData(c1InsCrosswalk.RowSel, COL_Description).ToString();
                    }

                    if (c1InsCrosswalk.GetData(c1InsCrosswalk.RowSel, COL_REPLACECODE) != null)
                    {
                        _code = c1InsCrosswalk.GetData(c1InsCrosswalk.RowSel, COL_REPLACECODE).ToString();
                    }
                    if (c1InsCrosswalk.GetData(c1InsCrosswalk.RowSel, COL_RDescription) != null)
                    {
                        _description = c1InsCrosswalk.GetData(c1InsCrosswalk.RowSel, COL_RDescription).ToString();
                    }

                    e2.oType = TransactionLineColumnType.None;

                    e.SuppressKeyPress = true;

                    #region "Delete Key"
                    switch (c1InsCrosswalk.ColSel)
                    {

                        //case COL_ORIGINALCODE:
                        //    {

                        //        c1InsCrosswalk.SetData(c1InsCrosswalk.RowSel, c1InsCrosswalk.ColSel, "");
                        //        c1InsCrosswalk.SetData(c1InsCrosswalk.RowSel, c1InsCrosswalk.ColSel + 1, "");

                        //        //CellRange rg = c1CPTMapping.GetCellRange(c1CPTMapping.RowSel, c1CPTMapping.ColSel);
                        //        //rg.UserData = oCellNotes;
                        //        e2.oType = TransactionLineColumnType.CPT;

                        //    }
                        //    break;
                        //case COL_Description:
                        //    {

                        //        //c1CPTMapping.SetData(c1CPTMapping.RowSel, c1CPTMapping.ColSel, "");
                        //        //c1CPTMapping.SetData(c1CPTMapping.RowSel, c1CPTMapping.ColSel + 1, "");

                        //        c1InsCrosswalk.SetData(c1InsCrosswalk.RowSel, COL_ORIGINALCODE, "");
                        //        c1InsCrosswalk.SetData(c1InsCrosswalk.RowSel, COL_Description, "");

                        //        //CellRange rg = c1CPTMapping.GetCellRange(c1CPTMapping.RowSel, c1CPTMapping.ColSel);
                        //        //rg.UserData = oCellNotes;
                        //        e2.oType = TransactionLineColumnType.CPT;

                        //    }
                        //    break;

                        case COL_REPLACECODE:
                            {
                                c1InsCrosswalk.SetData(c1InsCrosswalk.RowSel, COL_ReplaceFollowupID, 0);
                                c1InsCrosswalk.SetData(c1InsCrosswalk.RowSel, COL_REPLACECODE, "");
                                c1InsCrosswalk.SetData(c1InsCrosswalk.RowSel, COL_RDescription, "");

                                //CellRange rg = c1CPTMapping.GetCellRange(c1CPTMapping.RowSel, c1CPTMapping.ColSel);
                                //rg.UserData = oCellNotes;
                                e2.oType = TransactionLineColumnType.None;

                            }
                            break;
                        case COL_RDescription:
                            {

                                //c1CPTMapping.SetData(c1CPTMapping.RowSel, c1CPTMapping.ColSel, "");
                                //c1CPTMapping.SetData(c1CPTMapping.RowSel, c1CPTMapping.ColSel + 1, "");
                                c1InsCrosswalk.SetData(c1InsCrosswalk.RowSel, COL_ReplaceFollowupID, 0);
                                c1InsCrosswalk.SetData(c1InsCrosswalk.RowSel, COL_REPLACECODE, "");
                                c1InsCrosswalk.SetData(c1InsCrosswalk.RowSel, COL_RDescription, "");

                                //CellRange rg = c1CPTMapping.GetCellRange(c1CPTMapping.RowSel, c1CPTMapping.ColSel);
                                //rg.UserData = oCellNotes;
                                e2.oType = TransactionLineColumnType.None;

                            }
                            break;

                    }
                    _code = "";
                    e1 = new RowColEventArgs(c1InsCrosswalk.RowSel, c1InsCrosswalk.ColSel);
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

        private void c1InsCrosswalk_LeaveEdit(object sender, RowColEventArgs e)
        {
            try
            {
                ///here .... .. . .
                ///
                switch (e.Col)
                {
                    // case COL_ORIGINALCODE:
                    case COL_REPLACECODE:
                        if (c1InsCrosswalk.Editor != null)
                        {
                            c1InsCrosswalk.ChangeEdit -= new System.EventHandler(this.c1InsCrosswalk_ChangeEdit);
                            c1InsCrosswalk.Editor.Text = "";
                            c1InsCrosswalk.ChangeEdit += new System.EventHandler(this.c1InsCrosswalk_ChangeEdit);
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

        private void c1InsCrosswalk_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                switch (e.Col)
                {

                    //case COL_ORIGINALCODE:
                    //    {
                    //        OpenInternalControl(gloGridListControlType.InsFollowupMapping, "Follow-up Code", false, e.Row, e.Col, "");
                    //        string _SearchText = "";
                    //        if (c1InsCrosswalk != null && c1InsCrosswalk.Rows.Count > 0)
                    //        {

                    //            _SearchText = Convert.ToString(c1InsCrosswalk.GetData(e.Row,COL_ORIGINALCODE));

                    //            if (_SearchText != "" && ogloGridListControl != null)
                    //            {
                    //                //ogloGridListControl.AdvanceSearch(_SearchText);
                    //                ogloGridListControl.FillControl(_SearchText);
                    //            }
                    //        }
                    //    }
                    //    break;
                    //case COL_Description:
                    //    {
                    //        OpenInternalControl(gloGridListControlType.InsFollowupMapping, "Follow-up Code", false, e.Row, e.Col, "");
                    //        string _SearchText = "";
                    //        if (c1InsCrosswalk != null && c1InsCrosswalk.Rows.Count > 0)
                    //        {

                    //            _SearchText = Convert.ToString(c1InsCrosswalk.GetData(e.Row, COL_Description));

                    //            if (_SearchText != "" && ogloGridListControl != null)
                    //            {
                    //                //ogloGridListControl.AdvanceSearch(_SearchText);
                    //                ogloGridListControl.FillControl(_SearchText);
                    //            }
                    //        }
                    //    }
                    //    break;
                    case COL_REPLACECODE:
                        {
                            OpenInternalControl(gloGridListControlType.InsFollowupMapping, "Follow-up Code", false, e.Row, e.Col, "");
                            string _SearchText = "";
                            if (c1InsCrosswalk != null && c1InsCrosswalk.Rows.Count > 0)
                            {

                                _SearchText = Convert.ToString(c1InsCrosswalk.GetData(e.Row, COL_REPLACECODE));


                                if (_SearchText != "" && ogloGridListControl != null)
                                {
                                    //ogloGridListControl.AdvanceSearch(_SearchText);
                                    ogloGridListControl.FillControl(_SearchText);
                                }
                            }
                        }
                        break;
                    case COL_RDescription:
                        {
                            OpenInternalControl(gloGridListControlType.InsFollowupMapping, "Follow-up Code", false, e.Row, e.Col, "");
                            string _SearchText = "";
                            if (c1InsCrosswalk != null && c1InsCrosswalk.Rows.Count > 0)
                            {

                                _SearchText = Convert.ToString(c1InsCrosswalk.GetData(e.Row, COL_RDescription));

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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            #region " Followup Mapping Filter"
            txtSearch.Focus();
            txtSearch.Select();
            pnlInternalControl.Visible = false;
            DataView _dv = new DataView();
            string strSearch = "";
            try
            {
                _dv = new DataView();
                _dv = (DataView)c1InsCrosswalk.DataSource;


                strSearch = txtSearch.Text.ToString().Trim();
                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");

                string sFilter = "";
                if (strSearch.Trim() != "")
                {
                    // sFilter = " ( " + _dv.Table.Columns["sCPTMappingName"].ColumnName + " Like '" + strSearch + "%') or  ( " + _dv.Table.Columns["CreatedDate"].ColumnName + " Like '" + strSearch + "%')";
                    sFilter = " ( " + _dv.Table.Columns["sFollowupActionCode"].ColumnName + " Like '" + strSearch + "%') or ( " + _dv.Table.Columns["sFollowupActionDescription"].ColumnName + " Like '" + strSearch + "%') or  ( " + _dv.Table.Columns["sStdFollowupActionCode"].ColumnName + " Like '" + strSearch + "%') or ( " + _dv.Table.Columns["sStdFollowupActionDesc"].ColumnName + " Like '" + strSearch + "%')";
                }


                _dv.RowFilter = sFilter;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }
            finally
            {
                if (_dv != null)
                {
                    _dv = null;
                }
            }

            #endregion
        }

        private void mnuBilling_SaveClose_Click(object sender, EventArgs e)
        {
            ts_btnSaveClose.PerformClick();
        }

        private void ts_btnSaveClose_Click(object sender, EventArgs e)
        {
            c1InsCrosswalk.FinishEditing();
            if (ogloGridListControl != null)
            {
                CloseInternalControl();
            }


            if (ValidateSave())
            {

                if (SaveCodes())
                    this.Close();

            }
        }
        private bool SaveCodes()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_Databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            Int64 _DetailCrosswalkID = 0;
            object _CrosswalkID = 0;
            bool _retval = true;
            object _count = null;
            bool bIsModified = false;//false=Added, true= modified.
            DateTime _dtModifiedDate = DateTime.Today;
            try
            {
                
                if (_sInsCrosswalkName != txtInsCrosswalk.Text.Trim())
                {
                    _sInsCrosswalkName = txtInsCrosswalk.Text.Trim();
                    string _sqlstring = "SELECT count(sInsuranceCrosswalkName) FROM BL_InsuranceClaimCrosswalk_MST WITH (NOLOCK) WHERE sInsuranceCrosswalkName='" + _sInsCrosswalkName.Replace("'", "''").Trim() + "'";
                    oDB.Connect(false);
                    _count = oDB.ExecuteScalar_Query(_sqlstring);
                    if (Convert.ToInt64(_count.ToString()) > 0)
                    {
                        MessageBox.Show("Insurance follow-up Crosswalk Name is already in use by another entry. Enter a unique Ins. follow-up Crosswalk Name.", _Messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInsCrosswalk.Focus();
                        return false;
                    }
                }

                _dtModifiedDate = DateTime.Now;

                for (int i = 1; i < c1InsCrosswalk.Rows.Count; i++)
                {

                    if (_nInsCrosswalkID != 0)
                    {
                        if (c1InsCrosswalk.Rows[i][COL_InsCrosswalkDetailsID].ToString().Trim() != "")
                        {
                            _DetailCrosswalkID = Convert.ToInt64(c1InsCrosswalk.Rows[i][COL_InsCrosswalkDetailsID].ToString().Trim());
                        }
                        else
                        {
                            _DetailCrosswalkID = 0;
                        }
                    }
                    _sInsCrosswalkName = txtInsCrosswalk.Text.Trim();

                    Int64 _OriginalCodeID = Convert.ToInt64(c1InsCrosswalk.GetData(i, COL_OriginalFolloupID));
                    Int64 _ReplaceCodeID = Convert.ToInt64(c1InsCrosswalk.GetData(i, COL_ReplaceFollowupID));
                    oDB.Connect(false);
                    oParameters.Clear();
                    oParameters.Add("@nInsCrosswalkID", _nInsCrosswalkID, System.Data.ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oParameters.Add("@sInsuranceCrosswalkName", _sInsCrosswalkName, System.Data.ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nUserID", _UserID, System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nClinicID", _clinicID, System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nInsCrosswalkDetailID", _DetailCrosswalkID, System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nOriginalFollowupID", _OriginalCodeID, System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nReplaceFollowupID", _ReplaceCodeID, System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@dtModifiedDate", _dtModifiedDate, System.Data.ParameterDirection.Input, SqlDbType.DateTime);

                    oDB.Execute("gsp_AddFollowupCrosswalk", oParameters, out _CrosswalkID);


                    if (_nInsCrosswalkID == 0)
                    {
                        bIsModified = false;
                        //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.InsuranceClaimFollowupMapping, gloAuditTrail.ActivityType.Add, "New Insurance Claim Followup Crosswalk Action Added '" + _sInsCrosswalkName + "' Added ", 0, _nInsCrosswalkID, 0, gloAuditTrail.ActivityOutCome.Success);
                    }
                    else
                    {
                        if (_DetailCrosswalkID != 0)
                        {
                            bIsModified = true;
                            //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.InsuranceClaimFollowupMapping, gloAuditTrail.ActivityType.Add, "Insurance Claim Followup Crosswalk Action '" + _sInsCrosswalkName + "' transaction line have been Modified ", 0, _nInsCrosswalkID, 0, gloAuditTrail.ActivityOutCome.Success);
                        }
                    }
                    _nInsCrosswalkID = Convert.ToInt64(_CrosswalkID);
                    _DetailCrosswalkID = 0;

                }

                if (!bIsModified)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.InsuranceClaimFollowupMapping, gloAuditTrail.ActivityType.Add, "New Insurance Claim Followup Crosswalk Action Added '" + _sInsCrosswalkName + "' Added ", 0, _nInsCrosswalkID, 0, gloAuditTrail.ActivityOutCome.Success);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.InsuranceClaimFollowupMapping, gloAuditTrail.ActivityType.Add, "Insurance Claim Followup Crosswalk Action '" + _sInsCrosswalkName + "' transaction line have been Modified ", 0, _nInsCrosswalkID, 0, gloAuditTrail.ActivityOutCome.Success);
                }
                oDB.Disconnect();



            }
            catch (Exception ex)
            {
                _retval = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                _count = null;
                _CrosswalkID = null;

            }

            return _retval;
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.ResetText();
            txtSearch.Focus();
        }

    }
}

