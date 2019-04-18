using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace gloContacts
{
    #region " Enumerations "

    public enum gloContactsGridListControlType
    {
        Providers = 1, Procedures = 2, CPT = 3, ICD9 = 4, Modifier = 5, PatientInsurance = 6, POS = 7, TOS = 8, ReasonCodes = 9, Taxonomy = 10
    }

    public enum gloContactsSearchColumn
    {
        Code = 1, Description = 2, Name = 3
    }

    #endregion " Enumerations "

    public partial class gloGridListControl : UserControl, IDisposable
    {

        #region " Variable Declarations "

        //private string _MessageBoxCaption = "gloPMS";
        private string _DatabaseConnectionString = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private gloGeneralItem.gloItems _SelectedItems = null;
        private gloContactsGridListControlType _ControlType;
        private bool _ismultiselect = false;
        //private int _thiswidth = 0;
        private string _ControlHeader = "";
        private Int64 _PatientID = 0;
        private Int64 _ClinicID = 0;
        //private Int64 _UserID = 0;
        private int _CurrentSelectedRow = 0;
        private int _SearchCount = 0;


        private Int64 _TOSID = 0;
        private Int64 _CPTID = 0;
        private Int64 _ICD9ID = 0;

        private DataView _dv = null;

        private int _parentRowIndex = 0;
        private int _parentColIndex = 0;

       
        private DataView dvNext = null;

        DataTable _dtList = new DataTable();

        private string _SelectedCPTCode = "";
        private string _SelectedFacilityCode = "";
        private string _ControlSearchText = "";
        private string _SelectedReasonCode = "";

        #endregion " Variable Declarations "

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

        public gloGeneralItem.gloItems SelectedItems
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

        public gloContactsGridListControlType ControlType
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

        public string SelectedReasonCode
        {
            get { return _SelectedReasonCode; }
            set { _SelectedReasonCode = value; }
        }
        #endregion " Property Procedures "

        #region " Delegates "

        public delegate void Item_Selected(object sender, EventArgs e);
        public event Item_Selected ItemSelected;

        public delegate void Key_Down(object sender, EventArgs e);
        public event Key_Down InternalGridKeyDown;

        #endregion " Delegates "

        #region " Constructor &  Destructor  "

        public gloGridListControl()
        {
            InitializeComponent();



            _SelectedItems = new gloGeneralItem.gloItems();

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                {
                    _DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                else
                {
                    _DatabaseConnectionString = "";
                }
            }
            else
            {
                _DatabaseConnectionString = "";
            }
        }

        public gloGridListControl(gloContactsGridListControlType ControlType, bool IsMultiSelect, int ControlWidth, int ParentRowIndex, int ParentColIndex)
        {
            InitializeComponent();

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                {
                    _DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
                else
                {
                    _DatabaseConnectionString = "";
                }
            }
            else
            {
                _DatabaseConnectionString = "";
            }


            _ControlType = ControlType;
            _ismultiselect = IsMultiSelect;
            this.Width = ControlWidth;
            _SelectedItems = new gloGeneralItem.gloItems();
            this.ParentColIndex = ParentColIndex;
            this.ParentRowIndex = ParentRowIndex;
          
        }


       // private bool disposed = false;

        public void Disposer()
        {           
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~gloGridListControl()
        {           
            Dispose(false);
        }

        #endregion " Constructor &  Destructor "

        #region " Control Load "

        private void gloGridListControl_Load(object sender, EventArgs e)
        {

            gloC1FlexStyle.Style(c1GridList, false);

            try
            {
                lblHeader.Text = ControlHeader;
                FillControl("");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        #endregion " Control Load "

        #region " Fill Control "

        public void FillControl(string SearchText)
        {
            gloDatabaseLayer.DBLayer oDB=null;
            gloDatabaseLayer.DBParameters oParameters = null;
            string _sqlQuery = "";
            string _sqlAlternetQuery = "";
            try
            {
                SearchText = SearchText.Trim().Replace("'", "''").Replace("[", "").Replace("]", "");
                _ControlSearchText = SearchText;

                //if (_ControlType != null)
                {
                    if (_DatabaseConnectionString != "")
                    {
                        oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                        oParameters = new gloDatabaseLayer.DBParameters();
                        oDB.Connect(false);
                      

                        switch (_ControlType)
                        {
                            case gloContactsGridListControlType.Providers:
                                {
                                    _sqlQuery = " SELECT ISNULL(nProviderID,0) AS nResourceID, " +
                                                " (ISNULL(sFirstName,'')+SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1)+ISNULL(sLastName,'')) AS ProviderName " +
                                                " FROM Provider_MST ";
                                    if (_ClinicID > 0)
                                    {
                                        _sqlQuery = _sqlQuery + " WHERE nClinicID = " + _ClinicID + " ";
                                    }

                                    _sqlQuery = _sqlQuery + " ORDER BY sFirstName";

                                }
                                break;
                            case gloContactsGridListControlType.CPT:
                                {                                   
                                    oParameters.Clear();
                                    oParameters.Add("@SearchString", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDB.Retrive("BL_GET_CPT_SEARCH", oParameters, out _dtList);
                                }
                                break;
                            case gloContactsGridListControlType.ICD9:
                                {
                                    oParameters.Clear();
                                    oParameters.Add("@SearchString", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDB.Retrive("BL_GET_DIAGONOSIS_SEARCH", oParameters, out _dtList);
                                }


                                break;
                            case gloContactsGridListControlType.Modifier:
                                {   
                                    oParameters.Clear();
                                    oParameters.Add("@SearchString", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDB.Retrive("BL_GET_MODIFIER_SEARCH", oParameters, out _dtList);
                                }
                                break;
                            case gloContactsGridListControlType.PatientInsurance:
                                {
                                    _sqlQuery = "SELECT Distinct  PatientInsurance_DTL.nInsuranceID, Contacts_MST.sName AS Insurence" +
                                                " FROM  PatientInsurance_DTL INNER JOIN Contacts_MST ON PatientInsurance_DTL.nInsuranceID = Contacts_MST.nContactID " +
                                                " WHERE  PatientInsurance_DTL.nPatientID = " + _PatientID;
                                }
                                break;
                            case gloContactsGridListControlType.POS:
                                {
                                    if (_ClinicID == 0)
                                        _sqlQuery = "select ISNULL(nPOSID,0) AS nPOSID,ISNULL(sPOSCode,'') AS sPOSCode,ISNULL(sPOSName,'') AS sDescription from BL_POS_MST where (bIsBlocked IS NULL OR bIsBlocked = '" + false + "') ORDER BY sPOSName";
                                    else
                                        _sqlQuery = "select ISNULL(nPOSID,0) AS nPOSID,ISNULL(sPOSCode,'') AS sPOSCode,ISNULL(sPOSName,'') AS sDescription from BL_POS_MST where (bIsBlocked IS NULL OR bIsBlocked = '" + false + "') AND nClinicID = " + _ClinicID + " ORDER BY sPOSName";

                                }
                                break;
                            case gloContactsGridListControlType.TOS:
                                {
                                    if (_ClinicID == 0)
                                        _sqlQuery = "select ISNULL(nTOSID,0) AS nTOSID,ISNULL(sTOSCode,'') AS sTOSCode ,ISNULL(sDescription,'') AS sDescription from BL_TOS_MST where (bIsBlocked IS NULL OR bIsBlocked = '" + false + "') ORDER BY sDescription";
                                    else
                                        _sqlQuery = "select ISNULL(nTOSID,0) AS nTOSID,ISNULL(sTOSCode,'') AS sTOSCode,ISNULL(sDescription,'') AS sDescription from BL_TOS_MST where (bIsBlocked IS NULL OR bIsBlocked = '" + false + "') AND nClinicID = " + _ClinicID + " ORDER BY sDescription";
                                }
                                break;                      
                            case gloContactsGridListControlType.ReasonCodes:
                                {
                                    if (_ClinicID == 0)
                                        _sqlQuery = "select nReasonID,isnull(sGroupCode,'') + isnull(sCode,'') as ReasonCode ,sDescription from BL_ReasonCodes_MST where (bIsBlock IS NULL OR bIsBlock = '" + false + "') ORDER BY ReasonCode,nReasonID";
                                    else
                                        _sqlQuery = "select nReasonID,isnull(sGroupCode,'') + isnull(sCode,'') as ReasonCode ,sDescription from BL_ReasonCodes_MST where (bIsBlock IS NULL OR bIsBlock = '" + false + "') AND nClinicID = " + _ClinicID + " ORDER BY ReasonCode,nReasonID";
                                }
                                break;
                            case gloContactsGridListControlType.Taxonomy:
                                {                                  
                                    if (_ClinicID == 0)
                                        _sqlQuery = "SELECT DISTINCT isnull(sTaxonomyCode,'') as sTaxonomyCode,isnull(sTaxonomyDesc,'') as sTaxonomyDesc, isnull(sTaxonomyClassification,'') as sTaxonomyClassification FROM Specialty_MST where (bIsBlocked IS NULL OR bIsBlocked = '" + false + "') AND (sTaxonomyCode like '" + SearchText + "%' OR sTaxonomyClassification like '%" + SearchText + "%' OR sTaxonomyDesc like '%" + SearchText + "%') ORDER BY sTaxonomyCode,sTaxonomyClassification,sTaxonomyDesc";
                                    else
                                        _sqlQuery = "SELECT DISTINCT isnull(sTaxonomyCode,'') as sTaxonomyCode,isnull(sTaxonomyDesc,'') as sTaxonomyDesc, isnull(sTaxonomyClassification,'') as sTaxonomyClassification  FROM Specialty_MST where (bIsBlocked IS NULL OR bIsBlocked = '" + false + "') AND nClinicID = " + _ClinicID + " AND (sTaxonomyCode like '" + SearchText + "%' OR sTaxonomyClassification like '%" + SearchText + "%' OR sTaxonomyDesc like '%" + SearchText + "%') ORDER BY sTaxonomyCode,sTaxonomyClassification,sTaxonomyDesc";
                                }
                                break;
                            default:
                                break;
                        }

                        if (_sqlQuery.Trim() != "")
                        {
                            oDB.Retrive_Query(_sqlQuery, out _dtList);

                            //Special Case
                            if (_ControlType == gloContactsGridListControlType.Modifier)
                            {
                                if (_dtList != null)
                                {
                                    _dtList = new DataTable();
                                    oDB.Retrive_Query(_sqlAlternetQuery, out _dtList);

                                }
                            }


                            if (_dtList != null)
                            {                               
                                _dv = _dtList.DefaultView;
                                c1GridList.DataSource = _dv;
                                DesignGridList();                              
                            }
                            else
                            { _dv = null; }
                        }                       
                        else
                        {
                            if (_dtList != null)
                            {                               
                                _dv = _dtList.DefaultView;
                                c1GridList.DataSource = _dv;
                                DesignGridList();                              
                            }
                            else
                            { _dv = null; }
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                _sqlQuery = null;
                _sqlAlternetQuery = null;
            }
        }

        #endregion " Fill Control "

        #region " C1 Grid Design "

        private void DesignGridList()
        {
            int _Width = this.Width - 3;
            try
            {
                c1GridList.AllowEditing = false;

                switch (_ControlType)
                {
                    case gloContactsGridListControlType.Providers:
                        {
                            //nResourceID,sFirstName,sMiddleName,sLastName
                            c1GridList.Cols["nResourceID"].Visible = false;
                            c1GridList.Cols["ProviderName"].Visible = true;


                            c1GridList.Cols["nResourceID"].Width = 0;
                            c1GridList.Cols["ProviderName"].Width = Convert.ToInt32(_Width * 1);

                        }
                        break;
                    case gloContactsGridListControlType.CPT:
                        {
                            //nCPTID,sCPTCode,sCPTDesc 
                            c1GridList.Cols["nCPTID"].Visible = false;
                            c1GridList.Cols["sCPTCode"].Visible = true;
                            c1GridList.Cols["sDescription"].Visible = true;

                            c1GridList.Cols["nCPTID"].Width = 0;
                            c1GridList.Cols["sCPTCode"].Width = Convert.ToInt32(_Width * 0.20);
                            c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
                        }
                        break;
                    case gloContactsGridListControlType.ICD9:
                        {
                            //nICD9ID, sICD9Code, sDescription    
                            c1GridList.Cols["nICD9ID"].Visible = false;
                            c1GridList.Cols["sICD9Code"].Visible = true;
                            c1GridList.Cols["sDescription"].Visible = true;

                            c1GridList.Cols["nICD9ID"].Width = 0;
                            c1GridList.Cols["sICD9Code"].Width = Convert.ToInt32(_Width * 0.20);
                            c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
                        }
                        break;
                    case gloContactsGridListControlType.Modifier:
                        {
                            //nModifierID,sModifierCode,sDescription
                            c1GridList.Cols["nModifierID"].Visible = false;
                            c1GridList.Cols["sModifierCode"].Visible = true;
                            c1GridList.Cols["sDescription"].Visible = true;

                            c1GridList.Cols["nModifierID"].Width = 0;
                            c1GridList.Cols["sModifierCode"].Width = Convert.ToInt32(_Width * 0.20);
                            c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
                        }
                        break;
                    case gloContactsGridListControlType.PatientInsurance:
                        {

                        }
                        break;
                    case gloContactsGridListControlType.POS:
                        {
                            //nPOSID,sPOSCode,sDescription
                            c1GridList.Cols["nPOSID"].Visible = false;
                            c1GridList.Cols["sPOSCode"].Visible = true;
                            c1GridList.Cols["sDescription"].Visible = true;

                            c1GridList.Cols["nPOSID"].Width = 0;
                            c1GridList.Cols["sPOSCode"].Width = Convert.ToInt32(_Width * 0.20);
                            c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
                        }
                        break;
                    case gloContactsGridListControlType.TOS:
                        {
                            //nTOSID,sTOSCode ,sDescription
                            c1GridList.Cols["nTOSID"].Visible = false;
                            c1GridList.Cols["sTOSCode"].Visible = true;
                            c1GridList.Cols["sDescription"].Visible = true;

                            c1GridList.Cols["nTOSID"].Width = 0;
                            c1GridList.Cols["sTOSCode"].Width = Convert.ToInt32(_Width * 0.20);
                            c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
                        }
                        break;

                    //Added By Pramod Nair For Reason Codes
                    case gloContactsGridListControlType.ReasonCodes:
                        {
                            //nTOSID,sTOSCode ,sDescription
                            c1GridList.Cols["nReasonID"].Visible = false;
                            c1GridList.Cols["ReasonCode"].Visible = true;
                            c1GridList.Cols["sDescription"].Visible = true;

                            c1GridList.Cols["nReasonID"].Width = 0;
                            c1GridList.Cols["ReasonCode"].Width = Convert.ToInt32(_Width * 0.20);
                            c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
                        }
                        break;
                    case gloContactsGridListControlType.Taxonomy:
                        {
                            //nTOSID,sTOSCode ,sDescription
                            c1GridList.Cols["sTaxonomyCode"].Visible = true;
                            c1GridList.Cols["sTaxonomyDesc"].Visible = true;
                            c1GridList.Cols["sTaxonomyClassification"].Visible = true;
                                                      
                            //c1GridList.Cols["sTaxonomyCode"].Width = Convert.ToInt32(100);
                            c1GridList.Cols["sTaxonomyDesc"].Width = Convert.ToInt32(150);
                            //c1GridList.Cols["sTaxonomyClassification"].Width = Convert.ToInt32(300);
                            c1GridList.ExtendLastCol = true;
                                                     
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        #endregion " C1 Grid Design "

        #region " Search Functionality "

        private void txtListSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        public void Search(string SearchText, gloContactsSearchColumn SearchCol)
        {
            int _colIndex = 0;
            try
            {
                SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "");
                DataView _dvTemp = (DataView)c1GridList.DataSource;

                switch (_ControlType)
                {
                    case gloContactsGridListControlType.Providers:
                        break;
                    case gloContactsGridListControlType.PatientInsurance:
                        break;
                    case gloContactsGridListControlType.CPT:
                    case gloContactsGridListControlType.ICD9:
                    case gloContactsGridListControlType.Modifier:
                    case gloContactsGridListControlType.POS:
                    case gloContactsGridListControlType.TOS:
                    case gloContactsGridListControlType.ReasonCodes:
                        {
                            if (SearchCol == gloContactsSearchColumn.Code)
                            {
                                _colIndex = 1;
                            }
                            else if (SearchCol == gloContactsSearchColumn.Description)
                            {
                                _colIndex = 2;
                            }
                            else
                            {
                                _colIndex = 1;
                            }

                            SearchText.Replace("'", "");
                            if (_dvTemp == null)
                            {
                                return;
                            }
                            if (SearchText.StartsWith("%") == true | SearchText.StartsWith("*") == true)
                            {
                                _dvTemp.RowFilter = _dvTemp.Table.Columns[_colIndex].ColumnName + " Like '%" + SearchText + "%'";

                            }
                            else
                            {
                                _dvTemp.RowFilter = _dvTemp.Table.Columns[_colIndex].ColumnName + " Like '" + SearchText + "%'";
                            }
                            c1GridList.DataSource = _dvTemp;
                            DesignGridList();
                        }
                        break;
                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (c1GridList.DataSource != null)
                {
                    this.SearchCount = ((DataView)c1GridList.DataSource).Count;
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
                if (_dtList == null)
                {
                    return;
                }
                DataView _dvTemp = _dtList.DefaultView; //(DataView)c1GridList.DataSource;
                SearchText = SearchText.Replace("'", "''").Replace("%", "").Replace("*", "").Replace("[", "").Replace("(", "").Replace("]", "").Replace(")", "");

                switch (_ControlType)
                {
                    case gloContactsGridListControlType.PatientInsurance:
                        break;
                    case gloContactsGridListControlType.Providers:
                        {
                            //_colCodeIndex = 0;
                            _colDescriptionIndex = 1;
                            _searchStringArray = SearchText.Split(',');
                        }
                        break;
                    case gloContactsGridListControlType.CPT:
                    case gloContactsGridListControlType.ICD9:
                    case gloContactsGridListControlType.Modifier:
                    case gloContactsGridListControlType.POS:
                    case gloContactsGridListControlType.TOS:
                    case gloContactsGridListControlType.ReasonCodes:
                        {
                            //_colCodeIndex = 1;
                            _colDescriptionIndex = 2;
                            _searchStringArray = SearchText.Split(',');


                        }
                        break;
                    default:
                        break;
                }

                if (SearchText != "")
                {
                    if (_searchStringArray.Length == 1)
                    {
                        _searchString = _searchStringArray[0];
                        _dvTemp.RowFilter = _dvTemp.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%" + _searchString + "%' ";
                    }
                    else
                    {
                        for (int i = 0; i < _searchStringArray.Length; i++)
                        {
                            _searchString = _searchStringArray[i];
                            DataTable dtTemp = null;
                            if (_searchString.Trim() != "")
                            {
                                if (i == 0)
                                {
                                    dtTemp = _dvTemp.ToTable();
                                    dvNext = dtTemp.DefaultView;
                                }
                                else
                                {
                                    dtTemp = dvNext.ToTable();
                                    dvNext = dtTemp.DefaultView;
                                }

                                dvNext.RowFilter = dvNext.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%" + _searchString + "%' ";
                            }
                            if (dtTemp != null)
                            {
                                dtTemp.Dispose();
                                dtTemp = null;
                            }
                        }
                        if (_searchString != "")
                        {
                            if (_searchStringArray.Length > 1)
                            {
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

                c1GridList.DataSource = _dvTemp;
                DesignGridList();


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (c1GridList.DataSource != null)
                {
                    this.SearchCount = ((DataView)c1GridList.DataSource).Count;
                }
                _searchStringArray = null;
                _searchString = "";
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
                if (_dtList == null)
                {
                    return;
                }
                DataView _dvTemp = _dtList.DefaultView; //(DataView)c1GridList.DataSource;
                SearchText = SearchText.Trim().Replace("'", "''").Replace("%", "").Replace("*", "").Replace("[", "").Replace("(", "").Replace("]", "").Replace(")", "");
                _ControlSearchText = SearchText;

                switch (_ControlType)
                {
                    case gloContactsGridListControlType.PatientInsurance:
                        break;
                    case gloContactsGridListControlType.Providers:
                        {
                            _colCodeIndex = 0;
                            _colDescriptionIndex = 1;
                            _searchStringArray = SearchText.Split(',');
                        }
                        break;
                    case gloContactsGridListControlType.CPT:
                    case gloContactsGridListControlType.ICD9:
                    case gloContactsGridListControlType.Modifier: 
                    case gloContactsGridListControlType.POS:
                    case gloContactsGridListControlType.TOS:
                    case gloContactsGridListControlType.ReasonCodes:
                        {
                            _colCodeIndex = 1;
                            _colDescriptionIndex = 2;
                            _searchStringArray = SearchText.Split(',');
                        }
                        break;
                    default:
                        break;
                }

                if (SearchText != "")
                {
                    if (_searchStringArray.Length == 1)
                    {
                        _searchString = _searchStringArray[0];
                        if (_ControlType == gloContactsGridListControlType.Providers)
                        {
                            _dvTemp.RowFilter = _dvTemp.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%" + _searchString + "%' ";
                        }
                        //*********************************************
                        //Commented By Debasish Das. on 9th Aug 2010
                        //*********************************************

                        //else if (_ControlType == gloContactsGridListControlType.Modifier) //MaheshB
                        //{
                        //    _dvTemp.RowFilter = _dvTemp.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%" + _searchString + "%' OR " +
                        //                        _dvTemp.Table.Columns[_colCodeIndex].ColumnName + " Like '%" + _searchString + "%' ";

                        //}
                        //*********************************************
                        else
                        {
                            _dvTemp.RowFilter = _dvTemp.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%" + _searchString + "%' OR " +
                                                _dvTemp.Table.Columns[_colCodeIndex].ColumnName + " Like '%" + _searchString + "%' ";
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _searchStringArray.Length; i++)
                        {
                            _searchString = _searchStringArray[i];
                             DataTable dtTemp = null;
                            if (_searchString.Trim() != "")
                            {
                                if (i == 0)
                                {
                                    dtTemp = _dvTemp.ToTable();
                                    dvNext = dtTemp.DefaultView;
                                }
                                else
                                {
                                    dtTemp = dvNext.ToTable();
                                    dvNext = dtTemp.DefaultView;
                                }

                                if (_ControlType == gloContactsGridListControlType.Providers)
                                {
                                    dvNext.RowFilter = dvNext.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%" + _searchString + "%' ";
                                }
                                //*********************************************
                                //Commented By Debasish Das. on 9th Aug 2010
                                //*********************************************

                                //else if (_ControlType == gloContactsGridListControlType.Modifier) //MaheshB 
                                //{
                                //    dvNext.RowFilter = dvNext.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%" + _searchString + "%' OR " +
                                //                       dvNext.Table.Columns[_colCodeIndex].ColumnName + " Like '%" + _searchString + "%' ";

                                //}
                                //*********************************************
                                else
                                {
                                    dvNext.RowFilter = dvNext.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%" + _searchString + "%' OR " +
                                                       dvNext.Table.Columns[_colCodeIndex].ColumnName + " Like '%" + _searchString + "%' ";
                                }
                            }
                            if (dtTemp != null)
                            {
                                dtTemp.Dispose();
                                dtTemp = null;
                            }
                        }
                        if (_searchString != "")
                        {
                            if (_searchStringArray.Length > 1)
                            {
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

                c1GridList.DataSource = _dvTemp;
                DesignGridList();


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (c1GridList.DataSource != null)
                {
                    this.SearchCount = ((DataView)c1GridList.DataSource).Count;
                }
                _searchStringArray = null;
                _searchString = null;
            }

        }

        #endregion " Search Functionality "

        #region " Grid Events "

        private void c1GridList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int rowIndex = 0;
            Int64 _id = 0;
            string _code = "";
            string _desc = "";

            try
            {
                if (c1GridList != null)
                {
                    if (c1GridList.Rows.Count > 0)
                    {
                        rowIndex = c1GridList.RowSel;
                        switch (_ControlType)
                        {
                            case gloContactsGridListControlType.Providers:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0)); //Provider ID 
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                            case gloContactsGridListControlType.PatientInsurance:
                                break;
                            case gloContactsGridListControlType.CPT:
                            case gloContactsGridListControlType.ICD9:
                            case gloContactsGridListControlType.Modifier:
                            case gloContactsGridListControlType.POS:
                            case gloContactsGridListControlType.TOS:
                            case gloContactsGridListControlType.ReasonCodes:
                            case gloContactsGridListControlType.Taxonomy:
                                {                                  
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                        }
                    }
                }
                gloGeneralItem.gloItem oListItem = new gloGeneralItem.gloItem();
                oListItem.ID = _id;
                oListItem.Code = _code;
                oListItem.Description = _desc;

                _SelectedItems.Clear();
                _SelectedItems.Add(oListItem);
                oListItem.Dispose();
                oListItem = null;

            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ItemSelected(null, null);
                _code = null;
                _desc = null;
            }
        }

        private void c1GridList_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    c1GridList_MouseDoubleClick(null, null);
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    InternalGridKeyDown(sender, e);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }

        private void c1GridList_AfterSelChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                if (c1GridList != null && c1GridList.Rows.Count > 0)
                {
                    _CurrentSelectedRow = c1GridList.RowSel;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }
        }

        #endregion " Grid Events "

        #region " Public & Private Methods "

        //Code Changes on 20081013 By  - Sagar Ghodke
        //Below commented code is previous code

        #region " Commented Code "
        //public void GetCurrentSelectedItem()
        //{
        //    Int64 _id = 0;
        //    string _code = "";
        //    string _desc = "";
        //    int rowIndex = 0;

        //    try
        //    {
        //        _SelectedItems.Clear();
        //        if (c1GridList != null)
        //        {
        //            if (c1GridList.Rows.Count > 0)
        //            {

        //                    rowIndex = _CurrentSelectedRow;
        //                    switch (_ControlType)
        //                    {
        //                        case gloContactsGridListControlType.Providers:
        //                            {
        //                                _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
        //                                _code = Convert.ToString(c1GridList.GetData(rowIndex, 0)); //Provider ID 
        //                                _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
        //                            }
        //                            break;
        //                        case gloContactsGridListControlType.PatientInsurance:
        //                            break;
        //                        case gloContactsGridListControlType.CPT:
        //                        case gloContactsGridListControlType.ICD9:
        //                        case gloContactsGridListControlType.Modifier:
        //                        case gloContactsGridListControlType.POS:
        //                        case gloContactsGridListControlType.TOS:
        //                            {
        //                                _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
        //                                _code = Convert.ToString(c1GridList.GetData(rowIndex, 1));
        //                                _desc = Convert.ToString(c1GridList.GetData(rowIndex, 2));
        //                            }
        //                            break;
        //                    }

        //                    gloGeneralItem.gloItem oListItem = new gloGeneralItem.gloItem();
        //                    oListItem.ID = _id;
        //                    oListItem.Code = _code;
        //                    oListItem.Description = _desc;


        //                    _SelectedItems.Add(oListItem);

        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("ERROR :" + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    finally
        //    {

        //        ItemSelected(null, null);
        //    }
        //}
        #endregion " Commented Code "

        public bool GetCurrentSelectedItem()
        {
            Int64 _id = 0;
            string _code = "";
            string _desc = "";
            int rowIndex = 0;
            bool _retValue = false;

            try
            {
                _SelectedItems.Clear();
                if (c1GridList != null)
                {
                    if (c1GridList.Rows.Count > 0)
                    {

                        rowIndex = _CurrentSelectedRow;
                        switch (_ControlType)
                        {
                            case gloContactsGridListControlType.Providers:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0)); //Provider ID 
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                            case gloContactsGridListControlType.PatientInsurance:
                                break;
                            case gloContactsGridListControlType.CPT:
                            case gloContactsGridListControlType.ICD9:
                            case gloContactsGridListControlType.Modifier:
                            case gloContactsGridListControlType.POS:
                            case gloContactsGridListControlType.TOS:
                            case gloContactsGridListControlType.ReasonCodes:
                            case gloContactsGridListControlType.Taxonomy:
                                {                                  
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                        }

                        gloGeneralItem.gloItem oListItem = new gloGeneralItem.gloItem();
                        oListItem.ID = _id;
                        oListItem.Code = _code;
                        oListItem.Description = _desc;


                        _SelectedItems.Add(oListItem);
                        oListItem.Dispose();
                        oListItem = null;
                        _retValue = true;
                    }
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ItemSelected(null, null);
                _code = null;
                _desc = null;
            }

            return _retValue;
        }

        private bool IsTableExists(string strTableName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            object _oresult = new object();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@sTableName", strTableName, ParameterDirection.InputOutput, SqlDbType.VarChar);
                oDB.Execute("TABLEEXISTS", oParameters, out _oresult);
                if (Convert.ToInt64(_oresult) > 0 && _oresult != null)
                {
                    return true;
                }
                else
                {
                    //MessageBox.Show("Table Not Exists", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                //MessageBox.Show("ERROR : " + ex.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                _oresult = null;
            }

        }

        private void c1GridList_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

        }

        private void gloGridListControl_ControlRemoved(object sender, ControlEventArgs e)
        {
           
        }


        //End  - Code Changes 2000801013,Sagar Ghodke

        #endregion " Public & Private Methods "

    }
}
