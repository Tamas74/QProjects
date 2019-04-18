using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using gloGeneralItem ;
using System.Data.SqlClient;

namespace gloAddress
{
    #region " Enumerations "
   
    public enum gloGridListControlType
    {
        Providers = 1,
        Procedures = 2,
        CPT = 3,
        ICD9 = 4,
        Modifier = 5,
        PatientInsurance = 6,
        POS = 7,
        TOS = 8,
        ZIP = 9,
        WC=10, //workers comp
        AutoClaim=11

    }

    public enum SearchColumn
    {
        Code = 1,
        Description = 2,
        Name = 3
    }

    #endregion

    public partial class gloZipcontrol : UserControl
    {
        public gloZipcontrol()
        {


            InitializeComponent();
        }
        public gloZipcontrol(gloGridListControlType ControlType, bool IsMultiSelect, Int32 ControlWidth, Int32 ParentRowIndex, Int32 ParentColIndex, string sDataBaseConnectionString)
        {

            InitializeComponent();


            _ControlType = ControlType;
            _ismultiselect = IsMultiSelect;
            this.Width = ControlWidth;
            _SelectedItems = new System.Collections.Generic.List<gloItem>();
            this.ParentColIndex = ParentColIndex;
            this.ParentRowIndex = ParentRowIndex;


            _DatabaseConnectionString = sDataBaseConnectionString;
        }
        public gloZipcontrol(int  ControlType, bool IsMultiSelect, Int32 ControlWidth, Int32 ParentRowIndex, Int32 ParentColIndex, string sDataBaseConnectionString, Int64 PatienID)
        {

            InitializeComponent();

            if (ControlType == 10)
            {
                _ControlType = gloGridListControlType.WC ;
            }
            if (ControlType == 11)
            {
                _ControlType = gloGridListControlType.AutoClaim;
            }
            
            _ismultiselect = IsMultiSelect;
            this.Width = ControlWidth;
            _SelectedItems = new System.Collections.Generic.List<gloItem>();
            this.ParentColIndex = ParentColIndex;
            this.ParentRowIndex = ParentRowIndex;


            _DatabaseConnectionString = sDataBaseConnectionString;
            _PatientID = PatienID; 
        }

        const Int16 COL_ZIP = 0;
        const Int16 COL_City = 1;
        const Int16 COL_nID = 2;
        const Int16 COL_County = 3;
        const Int16 COL_ST = 4;
        const Int16 COL_AreaCode = 5;

       // private string _MessageBoxCaption = "gloEMR";
        private string _DatabaseConnectionString = "";
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private System.Collections.Generic.List<gloItem> _SelectedItems;
        private gloGridListControlType _ControlType;
        private bool _ismultiselect = false;
      //  private int _thiswidth = 0;
        private string _ControlHeader = "";
        private Int64 _PatientID;
        private Int64 _ClinicID = 0;
        //private Int64 _UserID = 0;
        private int _CurrentSelectedRow = 0;
        private int _SearchCount = 0;
        private bool _ShowHeader = true;

        private Int64 _TOSID = 0;
        private Int64 _CPTID = 0;
        private Int64 _ICD9ID = 0;

        private DataView _dv = null;

        private int _parentRowIndex = 0;
        private int _parentColIndex = 0;

       

        private DataTable _dtList = new DataTable();

        private string _SelectedCPTCode = "";
        private string _SelectedFacilityCode = "";
        private string _ControlSearchText = "";
        //bool _FireLostFocus;

        public delegate void ItemSelected(Object sender, EventArgs e);
        public event ItemSelected ItemSelectedclick;

        public delegate void InternalGridKeyDown(Object sender, EventArgs e);
        public event InternalGridKeyDown InternalGridKeyDownclick;

        public delegate void CloseBtn(Object sender, EventArgs e);
        public event CloseBtn CloseBtnClick;

        public delegate void AddBtn(Object sender, EventArgs e);
        public event AddBtn AddBtnClick;

        public delegate void ModifyBtn(Object sender, EventArgs e);
        public event ModifyBtn ModifyBtnClick;

        public delegate void LostFocus(Object sender, EventArgs e);
        public event  LostFocus LostFocusEvent;
        #region " Property Procedures "

        public string ControlSearchText
        {
            get { return _ControlSearchText; }
            set { _ControlSearchText = value; }
        }
        public int SearchCount
        {
            get { return _SearchCount; }
            set { _SearchCount = value; }
        }

        public string ControlHeader
        {
            get { return _ControlHeader; }
            set { _ControlHeader = value; }
        }

        public System.Collections.Generic.List<gloItem> SelectedItems
        {
            get { return _SelectedItems; }
            set { _SelectedItems = value; }
        }

        public Int64 TOSID
        {
            get { return _TOSID; }
            set { _TOSID = value; }
        }
        public Int64 CPTID
        {
            get { return _CPTID; }
            set { _CPTID = value; }
        }
        public Int64 ICD9ID
        {
            get { return _ICD9ID; }
            set { _ICD9ID = value; }
        }

        public gloGridListControlType ControlType
        {
            get { return _ControlType; }
            set { _ControlType = value; }
        }

        public int ParentRowIndex
        {
            get { return _parentRowIndex; }
            set { _parentRowIndex = value; }
        }
        public int ParentColIndex
        {
            get { return _parentColIndex; }
            set { _parentColIndex = value; }
        }

        public string SelectedCPTCode
        {
            get { return _SelectedCPTCode; }
            set { _SelectedCPTCode = value; }
        }
        public string SelectedFacilityCode
        {
            get { return _SelectedFacilityCode; }
            set { _SelectedFacilityCode = value; }
        }

        public string DatabaseConnectionString
        {
            get { return _DatabaseConnectionString; }
            set { _DatabaseConnectionString = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public bool ShowHeader
        {
            get { return _ShowHeader; }
            set
            { 
                _ShowHeader = value;
                Panel4.Visible = value;
            }
        }
        #endregion

        public static void Style(C1.Win.C1FlexGrid.C1FlexGrid FlexGrid, bool blnShowCellLabels)
        {
            FlexGrid.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            FlexGrid.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

            // Normal Style
            FlexGrid.Styles.Normal.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.Styles.Normal.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Normal.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
            FlexGrid.Styles.Normal.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            // Alternet Style
            FlexGrid.Styles.Alternate.BackColor = System.Drawing.Color.FromArgb(222, 231, 250);
            FlexGrid.Styles.Alternate.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Alternate.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Alternate.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            // Fixed Style
            FlexGrid.Styles.Fixed.BackColor = System.Drawing.Color.FromArgb(86, 126, 211);
            FlexGrid.Styles.Fixed.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Fixed.Font = gloGlobal.clsgloFont.gFont_BOLD;// new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Fixed.ForeColor = System.Drawing.Color.White;

            // Heighlight Style
            FlexGrid.Styles.Highlight.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGrid.Styles.Highlight.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Highlight.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Highlight.ForeColor = System.Drawing.Color.Black;

            // Focus Style
            FlexGrid.Styles.Focus.BackColor = System.Drawing.Color.FromArgb(255, 224, 160);
            FlexGrid.Styles.Focus.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Focus.Font = gloGlobal.clsgloFont.gFont_BOLD; new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Focus.ForeColor = System.Drawing.Color.Black;

            // EDITOR Style
            FlexGrid.Styles.Editor.BackColor = System.Drawing.Color.Beige;
            FlexGrid.Styles.Editor.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            FlexGrid.Styles.Editor.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Editor.ForeColor = System.Drawing.Color.Black;

            // Search Style
            FlexGrid.Styles.Search.BackColor = System.Drawing.Color.FromArgb(255, 197, 108);
            FlexGrid.Styles.Search.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Search.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Search.ForeColor = System.Drawing.Color.White;

            // Frozen Style
            FlexGrid.Styles.Frozen.BackColor = System.Drawing.Color.FromArgb(255, 224, 160);
            FlexGrid.Styles.Frozen.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.Frozen.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FlexGrid.Styles.Frozen.ForeColor = System.Drawing.Color.Black;

            // new Row Style
            FlexGrid.Styles.NewRow.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            FlexGrid.Styles.NewRow.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.NewRow.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);
            FlexGrid.Styles.NewRow.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);


            // Empty Area Style
            FlexGrid.Styles.EmptyArea.BackColor = System.Drawing.Color.White;
            FlexGrid.Styles.EmptyArea.Border.Color = System.Drawing.Color.FromArgb(159, 181, 221);
            FlexGrid.Styles.EmptyArea.ForeColor = System.Drawing.Color.FromArgb(31, 73, 125);

            FlexGrid.ShowCellLabels = blnShowCellLabels;
        }

         private void uctl_Treatment_Load(object sender, System.EventArgs e)
        {
        //Style(C1GridList, False)

        }
        #region " Fill Control "

        public void FillControl(string SearchText)
        {
            C1GridList.ScrollBars = ScrollBars.None;
            string _sqlQuery = "";
            try
            {
                SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("]", "");
                _ControlSearchText = SearchText;

                //if ((_ControlType != null))
                {
                    if (!string.IsNullOrEmpty(_DatabaseConnectionString))
                    {
                        
                      //  string _sqlAlternetQuery = "";

                        switch (_ControlType)
                        {

                            case gloGridListControlType.CPT:
                                if (true)
                                {
                                    if (!string.IsNullOrEmpty(SearchText.Trim()))
                                    {
                                        _sqlQuery = "SELECT DISTINCT TOP 100 case when ISNULL(nCPTID,0) >= 0 then 0 end nCPTID,ISNULL(sCPTCode,'') AS Code,ISNULL(sDescription,'') AS Description FROM CPT_MST WHERE sCPTCode like '%" + SearchText + "%' OR sDescription like '%" + SearchText + "%'";
                                    }
                                    else
                                    {
                                        _sqlQuery = "SELECT DISTINCT TOP 100 case when ISNULL(nCPTID,0) >= 0 then 0 end nCPTID,ISNULL(sCPTCode,'') AS Code,ISNULL(sDescription,'') AS Description FROM CPT_MST ";
                                    }
                                }

                                break; 

                                //break;
                            case gloGridListControlType.ICD9:
                                if (true)
                                {
                                    if (!string.IsNullOrEmpty(SearchText.Trim()))
                                    {
                                        _sqlQuery = "SELECT DISTINCT TOP 100 case when ISNULL(nICD9ID,0) >= 0 then 0 end nICD9ID,ISNULL(sICD9Code,'') AS Code,ISNULL(sDescription,'') AS Description FROM ICD9 " + " where sICD9Code like '%" + SearchText + "%' OR sDescription like '%" + SearchText.Replace(".", "") + "%'";
                                    }
                                    else
                                    {
                                        _sqlQuery = "SELECT DISTINCT TOP 100 case when ISNULL(nICD9ID,0) >= 0 then 0 end nICD9ID,ISNULL(sICD9Code,'') AS Code,ISNULL(sDescription,'') AS Description FROM ICD9 ";

                                    }
                                }

                                break; 

                               // break;
                            case gloGridListControlType.Modifier:
                                if (!string.IsNullOrEmpty(SearchText.Trim()))
                                {
                                    _sqlQuery = " SELECT DISTINCT ISNULL(Modifier_MST.nModifierID, 0) AS nModifierID, " + " ISNULL(Modifier_MST.sModifierCode, '') AS Code, " + " ISNULL(Modifier_MST.sDescription,'') AS Description " + " FROM Modifier_MST ";
                                }
                                else
                                {

                                    _sqlQuery = " SELECT DISTINCT ISNULL(Modifier_MST.nModifierID, 0) AS nModifierID, " + " ISNULL(Modifier_MST.sModifierCode, '') AS Code, " + " ISNULL(Modifier_MST.sDescription,'') AS Description " + " FROM Modifier_MST ";
                                }


                                break; 

                              //break;
                            
                            case gloGridListControlType.ZIP:
                                if (string.IsNullOrEmpty(SearchText.Trim()))
                                {
                                    //_sqlQuery = "Select distinct TOP 100 isnull(Zip,0) As Zip, City,nID,county,ST from CSZ_MST where zip like '%" & SearchText.Trim & "%'"
                                    _sqlQuery = "SELECT DISTINCT TOP 100 ISNULL(ZIP,0) AS Zip, ISNULL(City,'') as City, nID, ISNULL(County,'') as County, ISNULL(ST,'') AS ST, ISNULL(AreaCode,0) AS AreaCode FROM CSZ_MST WHERE Zip =  '" + SearchText + "'";//LIKE '" + SearchText + "%'";
                                }
                                else
                                //if (string.IsNullOrEmpty(SearchText.Trim()))
                                    //if (SearchText.Trim() != "")
                                {
                                    //_sqlQuery = "Select distinct TOP 100 isnull(Zip,0) As Zip, City,nID,county,ST from CSZ_MST where zip like '%" & SearchText.Trim & "%'"
                                    _sqlQuery = "SELECT DISTINCT TOP 100 ISNULL(ZIP,0) AS Zip, ISNULL(City,'') as City, nID, ISNULL(County,'') as County, ISNULL(ST,'') AS ST, ISNULL(AreaCode,0) AS AreaCode FROM CSZ_MST WHERE Zip LIKE '" + SearchText + "%'";//like '%" + SearchText + "%' ";
                                }


                                break;
                               //Sandip Darade 20091120
                            case gloGridListControlType.WC:
                                if (!string.IsNullOrEmpty(SearchText.Trim()))
                                {

                                  //_sqlQuery = "SELECT   ISNULL(sClaimno,'') as ClaimNo,ISNULL(sOtherinfo,'') as sNote, ISNULL(dtValidFrom,0)  as dtValidFrom FROM Patient_WorkersComp WHERE  nPatientId = " + _PatientID + " AND nType = 1AND  sClaimno like ( '%" + SearchText + "%') ";
                                    _sqlQuery = "SELECT   dbo.CONVERT_TO_DATE(dtValidFrom) as Date, ISNULL(sClaimno,'') as ClaimNo,ISNULL(sOtherinfo,'') as sNote FROM Patient_WorkersComp WHERE  nPatientId = " + _PatientID + " AND nType = 1AND  sClaimno like ( '%" + SearchText + "%') ";
                                }
                                else
                                {

                                    //   _sqlQuery = "SELECT   ISNULL(sClaimno,'') as ClaimNo,ISNULL(sOtherinfo,'') as sNote, ISNULL(dtValidFrom,0)  as dtValidFrom FROM Patient_WorkersComp WHERE  nPatientId = " + _PatientID + " AND nType = 1";
                                 // _sqlQuery = "SELECT   ISNULL(sClaimno,'') as ClaimNo,ISNULL(sOtherinfo,'') as sNote, dbo.CONVERT_TO_DATE(dtValidFrom) as Date FROM Patient_WorkersComp WHERE  nPatientId = " + _PatientID + " AND nType = 1";
                                    _sqlQuery = "SELECT   dbo.CONVERT_TO_DATE(dtValidFrom) as Date,ISNULL(sClaimno,'') as ClaimNo,ISNULL(sOtherinfo,'') as sNote  FROM Patient_WorkersComp WHERE  nPatientId = " + _PatientID + " AND nType = 1";

                                }


                               //convert(varchar,dtValidFrom,101) as Date 

                                break;

                            case gloGridListControlType.AutoClaim:
                                //if (!string.IsNullOrEmpty(SearchText.Trim()))
                                //{

                                //    _sqlQuery = "SELECT  ISNULL(sClaimno,'') as ClaimNo ,ISNULL(sOtherinfo,'') as Note, ISNULL(dtValidFrom,0)  as dtValidFrom FROM Patient_WorkersComp WHERE  nPatientId = " + _PatientID + " AND nType = 2  AND  sClaimno like ( '%" + SearchText + "%') ";
                                //}
                                //else
                                //{

                                //    _sqlQuery = "SELECT   ISNULL(sClaimno,'') as ClaimNo,ISNULL(sOtherinfo,'')   as Note, ISNULL(dtValidFrom,0)  as dtValidFrom FROM Patient_WorkersComp WHERE  nPatientId = " + _PatientID + " AND nType = 2";
                                //}
                                if (!string.IsNullOrEmpty(SearchText.Trim()))
                                {

                                    //_sqlQuery = "SELECT   ISNULL(sClaimno,'') as ClaimNo,ISNULL(sOtherinfo,'') as sNote, ISNULL(dtValidFrom,0)  as dtValidFrom FROM Patient_WorkersComp WHERE  nPatientId = " + _PatientID + " AND nType = 1AND  sClaimno like ( '%" + SearchText + "%') ";
                                    _sqlQuery = "SELECT   dbo.CONVERT_TO_DATE(dtValidFrom) as Date, ISNULL(sClaimno,'') as ClaimNo,ISNULL(sOtherinfo,'') as sNote FROM Patient_WorkersComp WHERE  nPatientId = " + _PatientID + " AND nType = 2 AND  sClaimno like ( '%" + SearchText + "%') ";
                                }
                                else
                                {

                                    //   _sqlQuery = "SELECT   ISNULL(sClaimno,'') as ClaimNo,ISNULL(sOtherinfo,'') as sNote, ISNULL(dtValidFrom,0)  as dtValidFrom FROM Patient_WorkersComp WHERE  nPatientId = " + _PatientID + " AND nType = 1";
                                    _sqlQuery = "SELECT   dbo.CONVERT_TO_DATE(dtValidFrom) as Date, ISNULL(sClaimno,'') as ClaimNo,ISNULL(sOtherinfo,'') as sNote FROM Patient_WorkersComp WHERE  nPatientId = " + _PatientID + " AND nType = 2 ";
                                }

                                break;
                            default:
                                break; 

                               // break;
                        }

                        if (!string.IsNullOrEmpty(_sqlQuery.Trim()))
                        {
                            if (_dtList != null)
                            {
                                _dtList.Dispose();
                                _dtList = null;
                            }
                            if (_dv != null)
                            {
                                _dv.Dispose();
                                _dv = null;
                            }
                            _dtList = new DataTable();
                            Retrive_Query(_sqlQuery, ref _dtList);


                            if (_dtList != null)
                            {
                                //Code commented on 20090129 by Sagar to empty inner list
                                //if (_dtList.Rows.Count > 0)
                                //{
                              
                                _dv = _dtList.DefaultView;
                             //   C1GridList.Clear();
                               C1GridList.DataSource = null;
                                C1GridList.DataSource = _dv;
                                //}
                                //else
                                //{ _dv = null; }
                               Application.DoEvents();
                                DesignGridList();
                            }
                            else
                            {
                                _dv = null;

                            }
                        }


                    }
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                _sqlQuery = null;
                C1GridList.ScrollBars = ScrollBars.Both;
            }
        }

        #endregion

        #region "Retrieve Result"
        private object Retrive_Query(string strQuery, ref DataTable dt)
        {
            System.Data.SqlClient.SqlConnection conn = default(System.Data.SqlClient.SqlConnection);
            System.Data.SqlClient.SqlCommand cmd = default(System.Data.SqlClient.SqlCommand);
            SqlDataAdapter sqladpt = new SqlDataAdapter();
            try
            {
                conn = new SqlConnection(_DatabaseConnectionString);
                cmd = new SqlCommand(strQuery, conn);
             //   sqladpt = new SqlDataAdapter();

                sqladpt.SelectCommand = cmd;

                sqladpt.Fill(dt);
                sqladpt.Dispose();
                sqladpt = null;
                return dt;
            }
            catch(Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                return null;
            }
            finally
            {
                if ((cmd != null))
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if ((conn != null))
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Dispose();
                }
            }
        }
        #endregion

        #region " Design Grid List "


        private void DesignGridList_old()
        {
            int _Width = this.Width - 4;
            try
            {
                C1GridList.AllowEditing = false;
               // C1GridList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

            //  Application.DoEvents();
                //switch (_ControlType)
                //{

                    //case gloGridListControlType.CPT:
                    //    if (true)
                    //    {
                    //        //nCPTID,sCPTCode,sCPTDesc 
                    //        C1GridList.Cols["nCPTID"].Visible = false;
                    //        C1GridList.Cols["sCPTCode"].Visible = true;
                    //        C1GridList.Cols["sDescription"].Visible = true;

                    //        C1GridList.Cols["nCPTID"].Width = 0;
                    //        C1GridList.Cols["sCPTCode"].Width = Convert.ToInt32(_Width * 0.2);
                    //        C1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.8);
                    //    }

                    //    break; // TODO: might not be correct. Was : Exit Select

                    //    break;
                    //case gloGridListControlType.ICD9:
                    //    if (true)
                    //    {
                    //        //nICD9ID, sICD9Code, sDescription 
                    //        C1GridList.Cols("nICD9ID").Visible = false;
                    //        C1GridList.Cols("sICD9Code").Visible = true;
                    //        C1GridList.Cols("sDescription").Visible = true;

                    //        C1GridList.Cols("nICD9ID").Width = 0;
                    //        C1GridList.Cols("sICD9Code").Width = Convert.ToInt32(_Width * 0.2);
                    //        C1GridList.Cols("sDescription").Width = Convert.ToInt32(_Width * 0.8);
                    //    }

                    //    break; // TODO: might not be correct. Was : Exit Select

                    //    break;
                    //case gloGridListControlType.Modifier:
                    //    if (true)
                    //    {
                    //        //nModifierID,sModifierCode,sDescription
                    //        C1GridList.Cols("nModifierID").Visible = false;
                    //        C1GridList.Cols("sModifierCode").Visible = true;
                    //        C1GridList.Cols("sDescription").Visible = true;

                    //        C1GridList.Cols("nModifierID").Width = 0;
                    //        C1GridList.Cols("sModifierCode").Width = Convert.ToInt32(_Width * 0.2);
                    //        C1GridList.Cols("sDescription").Width = Convert.ToInt32(_Width * 0.8);
                    //    }
                   
                    //    break; // TODO: might not be correct. Was : Exit Select

                    ////    break;
                  //case gloGridListControlType.ZIP:
               // if (true)
                    //{

                    C1GridList.Cols[COL_ZIP ].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter ;

                    C1GridList.Cols[COL_ZIP].Visible = true;
                    C1GridList.Cols[COL_City].Visible = true;
                    C1GridList.Cols[COL_nID].Visible = false;
                    C1GridList.Cols[COL_County].Visible = false;
                    C1GridList.Cols[COL_ST].Visible = false;
                    C1GridList.Cols[COL_AreaCode].Visible = false;

                    C1GridList.Cols[COL_ZIP].Width = Convert.ToInt32(_Width * 0.4);
                            C1GridList.Cols[COL_City].Width = Convert.ToInt32(_Width * 0.6);
                            C1GridList.Cols[COL_nID].Width = 0;
                            C1GridList.Cols[COL_County].Width = 0;
                            C1GridList.Cols[COL_ST].Width = 0;
                            C1GridList.Cols[COL_AreaCode].Width = 0;
                            //C1GridList.Cols("sDescription").Width = Convert.ToInt32(_Width * 0.8R)
                            C1GridList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
                    //    }

                    //    break; // TODO: might not be correct. Was : Exit Select


                    //    break;
                    //default:
                    //    break; // TODO: might not be correct. Was : Exit Select

                //    //    break;
                //}
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                ////gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

            }
        }
        //Sandip Darade 20091120
        private void DesignGridList()
      {
            int _Width = this.Width - 4;
            try
            {
                C1GridList.AllowEditing = false;
                // C1GridList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;

                Application.DoEvents();
                switch (_ControlType)
                {

                    case gloGridListControlType.WC:
                        if (true)
                        {
                            //C1GridList.DataSource = null;
                            //C1GridList.Rows.Count = 1;
                            C1GridList.Cols[0].Visible = true;
                            C1GridList.Cols[1].Visible = true;
                            C1GridList.Cols[2].Visible = true;

                            C1GridList.Cols[1].Caption = " Claim #";
                            C1GridList.Cols[2].Caption = " Notes";
                            C1GridList.Cols[0].Caption = "Injury Date";
                            
                            C1GridList.Cols[0].Width = Convert.ToInt32(_Width * 0.20);
                            C1GridList.Cols[1].Width = Convert.ToInt32(_Width * 0.20);
                            C1GridList.Cols[2].Width = Convert.ToInt32(_Width * 0.595);

                          //  DataTable dt = new DataTable();
                          ////  dt = (DataTable)C1GridList.DataSource;
                          //  dt = _dtList;
                           
                          //  if (dt != null && dt.Rows.Count > 0)
                          //  {
                               
                          //      for (int i = 0; i < dt.Rows.Count; i++)
                          //      {
                          //         C1GridList.Rows.Add();
                          //          int rowIndex = C1GridList.Rows.Count - 1;
                          //          C1GridList.SetData(rowIndex, 0, Convert.ToString(dt.Rows[i][0]));
                          //          C1GridList.SetData(rowIndex, 1, Convert.ToString(dt.Rows[i][1]));
                          //          Int64 _date =Convert.ToInt64(dt.Rows[i][2]);
                          //          C1GridList.SetData(rowIndex, 2, Convert.ToString(gloDateMaster.gloDate.DateAsDate(_date).ToShortDateString()));
                                  
                          //      }
                          //  }
                        }

                        break; // TODO: might not be correct. Was : Exit Select

                    case gloGridListControlType.AutoClaim:
                        if (true)
                        {
                            C1GridList.Cols[0].Visible = true;
                            C1GridList.Cols[1].Visible = true;
                            C1GridList.Cols[2].Visible = true;

                            C1GridList.Cols[1].Caption = " Claim #";
                            C1GridList.Cols[2].Caption = " Notes";
                            C1GridList.Cols[0].Caption = "Injury Date";

                            C1GridList.Cols[1].Width = Convert.ToInt32(_Width * 0.3);
                            C1GridList.Cols[2].Width = Convert.ToInt32(_Width * 0.4);
                            C1GridList.Cols[0].Width = Convert.ToInt32(_Width * 0.3);

                        }

                        break; // TODO: might not be correct. Was : Exit Select

                      //  break;
                   
                    default:
                        C1GridList.Cols[COL_ZIP].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                        C1GridList.Cols[COL_ZIP].Visible = true;
                        C1GridList.Cols[COL_City].Visible = true;
                        C1GridList.Cols[COL_nID].Visible = false;
                        C1GridList.Cols[COL_County].Visible = false;
                        C1GridList.Cols[COL_ST].Visible = false;
                        C1GridList.Cols[COL_AreaCode].Visible = false;

                        C1GridList.Cols[COL_ZIP].Width = Convert.ToInt32(_Width * 0.4);
                        C1GridList.Cols[COL_City].Width = Convert.ToInt32(_Width * 0.6);
                        C1GridList.Cols[COL_nID].Width = 0;
                        C1GridList.Cols[COL_County].Width = 0;
                        C1GridList.Cols[COL_ST].Width = 0;
                        C1GridList.Cols[COL_AreaCode].Width = 0;
                        //C1GridList.Cols("sDescription").Width = Convert.ToInt32(_Width * 0.8R)
                        C1GridList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None;
                        break; // TODO: might not be correct. Was : Exit Select

                    //    break;
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                ////gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

            }
        }

        #endregion

        #region " Search Functionality "

        private void txtListSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

            }
        }

        public void Search(string SearchText, SearchColumn SearchCol)
        {
            int _colIndex = 0;
            try
            {
                SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("]", "");
                DataView _dvTemp = (DataView)C1GridList.DataSource;
                if (_dvTemp != null)
                {
                    switch (_ControlType)
                    {

                        case gloGridListControlType.CPT:
                        case gloGridListControlType.ICD9:
                        case gloGridListControlType.Modifier:
                        case gloGridListControlType.POS:
                        case gloGridListControlType.TOS:
                            if (true)
                            {
                                if (SearchCol == SearchColumn.Code)
                                {
                                    _colIndex = 1;
                                }
                                else if (SearchCol == SearchColumn.Description)
                                {
                                    _colIndex = 2;
                                }
                                else
                                {
                                    _colIndex = 1;
                                }

                                SearchText.Replace("'", "");

                                if (SearchText.StartsWith("%") == true | SearchText.StartsWith("*") == true)
                                {

                                    _dvTemp.RowFilter = (_dvTemp.Table.Columns[_colIndex].ColumnName + " Like '%") + SearchText + "%'";
                                }
                                else
                                {
                                    _dvTemp.RowFilter = (_dvTemp.Table.Columns[_colIndex].ColumnName + " Like '") + SearchText + "%'";
                                }
                                C1GridList.DataSource = _dvTemp;
                                DesignGridList();
                            }

                            break; // TODO: might not be correct. Was : Exit Select

                        //break;
                        default:
                            break; // TODO: might not be correct. Was : Exit Select


                        //  break;

                    }
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                if (C1GridList.DataSource != null)
                {
                    this.SearchCount = ((DataView)C1GridList.DataSource).Count;

                }
            }
        }

        public void SearchWC(string SearchText)
        {
            int _colIndex = 0;
            try
            {
                SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("]", "");
                DataView _dvTemp = (DataView)C1GridList.DataSource;
                if (_dvTemp != null)
                {

                    //if (SearchCol == SearchColumn.Code)
                    //{
                    //    _colIndex = 1;
                    //}
                    //else if (SearchCol == SearchColumn.Description)
                    //{
                    //    _colIndex = 2;
                    //}
                    //else
                    //{
                    //    _colIndex = 1;
                    //}
                    _colIndex = 1;
                    SearchText.Replace("'", "");

                    if (SearchText.StartsWith("%") == true | SearchText.StartsWith("*") == true)
                    {

                        _dvTemp.RowFilter = (_dvTemp.Table.Columns[_colIndex].ColumnName + " Like '%") + SearchText + "%'";
                    }
                    else
                    {
                        _dvTemp.RowFilter = (_dvTemp.Table.Columns[_colIndex].ColumnName + " Like '") + SearchText + "%'";
                    }
                    C1GridList.DataSource = _dvTemp;
                    DesignGridList();
                }
            }
            //    break; // TODO: might not be correct. Was : Exit Select

                    //    break;
            //default:
            //    break; // TODO: might not be correct. Was : Exit Select


                    //    break;

                //}
            //}
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                if (C1GridList.DataSource != null)
                {
                    this.SearchCount = ((DataView)C1GridList.DataSource).Count;

                }
            }
        }

        public void InStringSearch(string SearchText)
        {
            //int _colCodeIndex = 0;
            int _colDescriptionIndex = 0;
            string[] _searchStringArray = null;
            string _searchString = "";

            try
            {
                if (_dtList != null)
                {
                    DataView _dvTemp = _dtList.DefaultView;
                    //(DataView)c1GridList.DataSource;
                    SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("(", "").Replace("]", "").Replace(")", "");

                    switch (_ControlType)
                    {

                        case gloGridListControlType.CPT:
                        case gloGridListControlType.ICD9:
                        case gloGridListControlType.Modifier:
                            if (true)
                            {
                                //_colCodeIndex = 1;
                                _colDescriptionIndex = 2;

                                _searchStringArray = SearchText.Split(',');
                            }


                            break; // TODO: might not be correct. Was : Exit Select

                        //break;
                        default:
                            break; // TODO: might not be correct. Was : Exit Select

                        //  break;
                    }

                    if (!string.IsNullOrEmpty(SearchText))
                    {
                        if (_searchStringArray.Length == 1)
                        {
                            _searchString = _searchStringArray[0];
                            _dvTemp.RowFilter = (_dvTemp.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%") + _searchString + "%' ";
                        }
                        else
                        {
                            DataView dvNext = null;
                            for (int i = 0; i <= _searchStringArray.Length - 1; i++)
                            {
                                _searchString = _searchStringArray[i];
                                if (!string.IsNullOrEmpty(_searchString.Trim()))
                                {
                                    if (i == 0)
                                    { 
                                        DataTable dtTemp = null;
                                        dtTemp = _dvTemp.ToTable();
                                        if (dvNext != null)
                                        {
                                            dvNext.Dispose();
                                            dvNext = null;
                                        }
                                        dvNext = dtTemp.Copy().DefaultView;
                                        dtTemp.Dispose();
                                        dtTemp = null;
                                    }
                                    else
                                    {
                                        DataTable dtTemp = null;
                                        if (dvNext != null)
                                        {
                                            dtTemp = dvNext.ToTable();
                                            dvNext.Dispose();
                                            dvNext = null;
                                        }
                                        else
                                        {
                                            dtTemp = new DataTable();
                                        }
                                        dvNext = dtTemp.Copy().DefaultView;
                                        dtTemp.Dispose();
                                        dtTemp = null;
                                    }

                                    dvNext.RowFilter = dvNext.Table.Columns[_colDescriptionIndex].ColumnName + "' Like '%" + _searchString + "%' ";
                                }
                            }
                            if (!string.IsNullOrEmpty(_searchString))
                            {
                                if (_searchStringArray.Length > 1)
                                {
                                    if (_dvTemp != null)
                                    {
                                        _dvTemp.Dispose();
                                        _dvTemp = null;
                                    }
                                    _dvTemp = dvNext;
                                }

                            }
                        }
                    }
                    else
                    {
                        _dvTemp.RowFilter = "";
                        _dvTemp.RowStateFilter = DataViewRowState.OriginalRows;
                    }

                    C1GridList.DataSource = _dvTemp;


                    DesignGridList();
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                if (C1GridList.DataSource != null)
                {
                    this.SearchCount = ((DataView)C1GridList.DataSource).Count;

                }
                _searchStringArray = null;
                _searchString = null;
            }
        }

        public void AdvanceSearch(string SearchText)
        {
            int _colCodeIndex = 0;
            int _colDescriptionIndex = 0;
            string[] _searchStringArray = null;
            string _searchString = "";

            try
            {
                if (_dtList != null)
                {
                    DataView _dvTemp = _dtList.DefaultView;
                    //(DataView)c1GridList.DataSource;
                    SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("(", "").Replace("]", "").Replace(")", "");
                    _ControlSearchText = SearchText;

                    switch (_ControlType)
                    {

                        case gloGridListControlType.CPT:
                        case gloGridListControlType.ICD9:
                        case gloGridListControlType.Modifier:
                            if (true)
                            {
                                _colCodeIndex = 1;
                                _colDescriptionIndex = 2;

                                _searchStringArray = SearchText.Split(',');
                            }


                            break; // TODO: might not be correct. Was : Exit Select

                        // break;
                        default:
                            break; // TODO: might not be correct. Was : Exit Select

                        //  break;
                    }

                    if (!string.IsNullOrEmpty(SearchText))
                    {
                        if (_searchStringArray.Length == 1)
                        {
                            _searchString = _searchStringArray[0];
                            if (_ControlType == gloGridListControlType.Providers)
                            {
                                _dvTemp.RowFilter = (_dvTemp.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%") + _searchString + "%' ";
                            }
                            else
                            {
                                _dvTemp.RowFilter = (((_dvTemp.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%") + _searchString + "%' OR ") + _dvTemp.Table.Columns[_colCodeIndex].ColumnName + " Like '%") + _searchString + "%' ";
                            }
                        }
                        else
                        {
                            DataView dvNext = null;
                            for (int i = 0; i <= _searchStringArray.Length - 1; i++)
                            {
                                _searchString = _searchStringArray[i];
                                if (!string.IsNullOrEmpty(_searchString.Trim()))
                                {
                                    if (i == 0)
                                    {
                                        DataTable dtTemp = null;
                                        dtTemp = _dvTemp.ToTable();
                                        if (dvNext != null)
                                        {
                                            dvNext.Dispose();
                                            dvNext = null;
                                        }
                                        dvNext = dtTemp.Copy().DefaultView;
                                        dtTemp.Dispose();
                                        dtTemp = null;
                                    }
                                    else
                                    {
                                        DataTable dtTemp = null;
                                        if (dvNext != null)
                                        {
                                            dtTemp = dvNext.ToTable();
                                            dvNext.Dispose();
                                            dvNext = null;
                                        }
                                        else
                                        {
                                            dtTemp = new DataTable();
                                        }
                                        dvNext = dtTemp.DefaultView;
                                        dtTemp.Dispose();
                                        dtTemp = null;
                                    }

                                    if (_ControlType == gloGridListControlType.Providers)
                                    {
                                        dvNext.RowFilter = (dvNext.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%") + _searchString + "%' ";
                                    }
                                    else
                                    {
                                        dvNext.RowFilter = (((dvNext.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%") + _searchString + "%' OR ") + dvNext.Table.Columns[_colCodeIndex].ColumnName + " Like '%") + _searchString + "%' ";
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(_searchString))
                            {
                                if (_searchStringArray.Length > 1)
                                {
                                    if (_dvTemp != null)
                                    {
                                        _dvTemp.Dispose();
                                        _dvTemp = null;
                                    }
                                    _dvTemp = dvNext;
                                }

                            }
                        }
                    }
                    else
                    {
                        _dvTemp.RowFilter = "";
                        _dvTemp.RowStateFilter = DataViewRowState.OriginalRows;
                    }

                    C1GridList.DataSource = _dvTemp;


                    DesignGridList();
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                if (C1GridList.DataSource != null)
                {
                    this.SearchCount = ((DataView)C1GridList.DataSource).Count;

                }
                _searchStringArray = null;
                _searchString = null;
            }
        }

        #endregion
        private void gloZipcontrol_Load(object sender, EventArgs e)
        {


            gloC1FlexStyle.Style(C1GridList, false);
            if ((ToolTip1 != null))
            {
                ToolTip1.RemoveAll();
                ToolTip1.Dispose();
            }
            ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnCloseRefill, " Close ");
            ToolTip1.SetToolTip(this.btnModify, " Modify ");
            ToolTip1.SetToolTip(this.btnAdd, " Add ");
            ToolTip1.SetToolTip(this.btnSelect, " Select ");


            try
            {
                //btnCloseRefill.Visible = True
                btnCloseRefill.BringToFront();
                lblHeader.Text = ControlHeader;
                lblHeader.SendToBack();

                Application.DoEvents();
                FillControl("");
               if (_ShowHeader == true)
               {
                   Label1.Visible = false;
                   Label2.Visible = false;
                   Label3.Visible = false;
                   Label4.Visible = false;
                    lblHeader.Visible = true;
                    lblHeader.Text = _ControlHeader;  
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

            }
        }

        private void C1GridList_KeyDown(object sender, KeyEventArgs e)
        {
            {
                try
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        C1GridList_MouseDoubleClick(null, null);
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        if (InternalGridKeyDownclick  != null)
                        {
                            InternalGridKeyDownclick(sender, e);
                        }
                    }
                }
                catch (Exception)// ex)
                {
                    //ex.ToString();
                    //ex = null;
                }
                finally
                {
                    //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)

                }
            }
        }

        private void C1GridList_AfterSelChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            {
                try
                {
                    if (C1GridList != null && C1GridList.Rows.Count > 0)
                    {
                        _CurrentSelectedRow = C1GridList.RowSel;
                    }
                }
                catch (Exception)// ex)
                {
                    //ex.ToString();
                    //ex = null;
                }
                finally
                {
                    //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                }
            }
        }

        private void C1GridList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
                int rowIndex = 0;
                Int64 _id = 0;
                string _code = "";
                string _desc = "";

                try
                {
                    if (C1GridList != null)
                    {
                        if (C1GridList.Rows.Count > 0)
                        {
                            rowIndex = C1GridList.RowSel;
                            switch (_ControlType)
                            {

                                case gloGridListControlType.CPT:
                                case gloGridListControlType.ICD9:
                                case gloGridListControlType.Modifier:
                                case gloGridListControlType.ZIP:
                                    if (true)
                                    {
                                        _id = Convert.ToInt64(C1GridList.GetData(rowIndex, 0));
                                        _code = Convert.ToString(C1GridList.GetData(rowIndex, 1));
                                        _desc = Convert.ToString(C1GridList.GetData(rowIndex, 2));
                                    }

                                    break; // TODO: might not be correct. Was : Exit Select


                                  //  break;
                            }
                        }
                    }
                    gloItem oListItem = new gloItem();
                    oListItem.ID = _id;
                    oListItem.Code = _code;
                    oListItem.Description = _desc;

                    _SelectedItems.Clear();

                    _SelectedItems.Add(oListItem);
                    oListItem.Dispose();
                }
                catch (Exception)// ex)
                {
                    //ex.ToString();
                    //ex = null;
                }
                finally
                {
                    //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
                    //_FireLostFocus = false;
                    if (ItemSelectedclick   != null)
                    {
                        ItemSelectedclick(null, null);
                    }
                    _code = null;
                    _desc = null;
                }
        }

        private void  btnCloseRefill_Click(System.Object sender, System.EventArgs e)
        {
        if ((ToolTip1 != null)) {
            ToolTip1.RemoveAll();
            ToolTip1.Dispose();
        }
        if (CloseBtnClick != null)
        {
            CloseBtnClick(sender, e);
        }
        }

        private void  btnSelect_Click(System.Object sender, System.EventArgs e)
        {
        //_FireLostFocus = false;
        if (ItemSelectedclick != null) {
            ItemSelectedclick(sender, e);
        }
        }

        private void  btnAdd_Click(System.Object sender, System.EventArgs e)    
        {
            if (AddBtnClick != null)
            {
            AddBtnClick(sender, e);
        }
        }

        private void  btnModify_Click(System.Object sender, System.EventArgs e)
        {
            if (ModifyBtnClick != null)
            {
            ModifyBtnClick(sender, e);
        }
        }

        private void C1GridList_Leave(object sender, EventArgs e)
        {
            try
            {
                LostFocusEvent(null, null);
            }
            catch (Exception)
            {

            }   
             
        }

        private void C1GridList_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1,C1GridList, e.Location);
        }

    }
}
 