using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace gloBilling
{
    #region " Enumerations "

    public enum gloGridListControlType
    {
        Providers = 1, Procedures = 2, CPT = 3, ICD9 = 4, Modifier = 5, PatientInsurance = 6, POS = 7, TOS = 8, ReasonCodes = 9,CondtionCodes=10,
        OccurrenceCode = 11, OccurrenceSpanCode = 12, ValueCodes = 13, RemarkCodes = 14, InsFollowupMapping=15
    }

    public enum SearchColumn
    {
        Code = 1, Description = 2, Name = 3
    }

    #endregion " Enumerations "

    public partial class gloGridListControl : UserControl, IDisposable
    {

        #region " Variable Declarations "

     //   private string _MessageBoxCaption = "gloPMS";
        private string _DatabaseConnectionString = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private gloGeneralItem.gloItems _SelectedItems = null;
        public gloGridListControlType _ControlType;
        private bool _ismultiselect = false;
      //  private int _thiswidth = 0;
        private string _ControlHeader = "";
        private Int64 _PatientID = 0;
        private Int64 _ClinicID = 0;
    //    private Int64 _UserID = 0;
        private int _CurrentSelectedRow = 0;
        private int _SearchCount = 0;


        private Int64 _TOSID = 0;
        private Int64 _CPTID = 0;
        private Int64 _ICD9ID = 0;

        private DataView _dv = null;

        private int _parentRowIndex = 0;
        private int _parentColIndex = 0;

        private DataTable dtTemp = null;
        private DataView dvNext = null;

        DataTable _dtList = new DataTable();

        private string _SelectedCPTCode = "";
        private string _SelectedFacilityCode = "";
        private string _ControlSearchText = "";
        private string _SelectedReasonCode = "";

        gloDatabaseLayer.DBLayer oDB = null;
        gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

        public  int IcdCodeType = gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode();
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
            //Connection Disposed on Dispose Method and Control Remove event (i.e gloGridListControl_ControlRemoved)
            oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            oDB.Connect(false);
            //**
        }

        public gloGridListControl(gloGridListControlType ControlType, bool IsMultiSelect, int ControlWidth, int ParentRowIndex, int ParentColIndex)
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

            //Connection Disposed on Dispose Method and Control Remove event (i.e gloGridListControl_ControlRemoved)
            oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            oDB.Connect(false);
            //**
        }


     //   private bool disposed = false;

        public void Disposer()
        {
            //Disposing connection opened on Constructor
            if (oDB != null)
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            //**
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~gloGridListControl()
        {
            //Disposing connection opened on Constructor
            if (oDB != null)
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            //**
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

        public void FillControl_Old(string SearchText)
        {
            //this code was commented 
            gloDatabaseLayer.DBLayer oDB=null;
            //this code was commented 
            try
            {
                SearchText = SearchText.Trim().Replace("'", "''").Replace("[", "").Replace("]", "");
                _ControlSearchText = SearchText;

                //if (_ControlType != null)
                {
                    if (_DatabaseConnectionString != "")
                    {
                        string _sqlQuery = "";
                        string _sqlAlternetQuery = "";
                        //this code was commented 
                        oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                        gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                        oDB.Connect(false);
                        //this code was commented 
                        switch (_ControlType)
                        {
                            case gloGridListControlType.Providers:
                                {
                                    //*********************************************************************************
                                    //Code Chages made on 20081014 By - Sagar Ghodke
                                    //Below commented code is previous code

                                    // _sqlQuery = "SELECT AB_Resource_MST.nResourceID, " +
                                    //" ISNULL(Provider_MST.sFirstName,'')+ SPACE(1) + ISNULL(Provider_MST.sMiddleName,'')+SPACE(1)+ISNULL(Provider_MST.sLastName,'') AS ProviderName " +
                                    //" FROM AB_Resource_MST INNER JOIN Provider_MST ON AB_Resource_MST.nResourceID = Provider_MST.nProviderID INNER JOIN AB_ResourceType_MST ON AB_Resource_MST.nResourceTypeID = AB_ResourceType_MST.nResourceTypeID " +
                                    //" WHERE (AB_ResourceType_MST.nResourceType = 1 AND AB_Resource_MST.bitIsBlocked = 0)";
                                    //if (_ClinicID > 0)
                                    //{
                                    //    _sqlQuery = _sqlQuery + " AND AB_Resource_MST.nClinicID = " + _ClinicID + " ";
                                    //}

                                    //***Notes : nProviderID is taken as nResourceID to avoid name conflicts 
                                    //as the Provider_MST is changed

                                    _sqlQuery = " SELECT ISNULL(nProviderID,0) AS nResourceID, " +
                                                " (ISNULL(sFirstName,'')+SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1)+ISNULL(sLastName,'')) AS ProviderName " +
                                                " FROM Provider_MST WITH (NOLOCK) ";
                                    if (_ClinicID > 0)
                                    {
                                        _sqlQuery = _sqlQuery + " WHERE nClinicID = " + _ClinicID + " ";
                                    }

                                    _sqlQuery = _sqlQuery + " ORDER BY sFirstName";

                                    //END - Code Chages made on 20081014 By - Sagar Ghodke
                                    //*********************************************************************************
                                }
                                break;
                            case gloGridListControlType.CPT:
                                {

                                    //*********************************************
                                    //Commented By Debasish Das. on 9th Aug 2010
                                    //*********************************************
                                    //////if (SearchText.Trim() != "")
                                    //////{
                                    //////    if (TOSID == 0)
                                    //////    {
                                    //////        // _sqlQuery = "SELECT DISTINCT CPT_MST.nCPTID, BL_TOS_CPT.sCPTCode, BL_TOS_CPT.sCPTDesc FROM BL_TOS_CPT INNER JOIN CPT_MST ON BL_TOS_CPT.sCPTCode = CPT_MST.sCPTCode ";

                                    //////        _sqlQuery = "SELECT DISTINCT TOP 100 case  when ISNULL(nCPTID,0) >= 0 then 0 end nCPTID,ISNULL(sCPTCode,'') AS sCPTCode,ISNULL(sDescription,'') AS sDescription FROM CPT_MST WHERE nClinicID = " + _ClinicID + " " +
                                    //////        " AND (sCPTCode like '%" + SearchText + "%' OR sDescription like '%" + SearchText + "%')";
                                    //////    }
                                    //////    else
                                    //////    {
                                    //////        _sqlQuery = "SELECT DISTINCT TOP 100 case  when ISNULL(nCPTID,0) >= 0 then 0 end nCPTID,ISNULL(sCPTCode,'') AS sCPTCode,ISNULL(sDescription,'') AS sDescription FROM CPT_MST WHERE nClinicID = " + _ClinicID + " " +
                                    //////        " AND (sCPTCode like '%" + SearchText + "%' OR sDescription like '%" + SearchText + "%')";

                                    //////        //_sqlQuery = "SELECT DISTINCT CPT_MST.nCPTID, BL_TOS_CPT.sCPTCode, BL_TOS_CPT.sCPTDesc FROM BL_TOS_CPT INNER JOIN CPT_MST ON BL_TOS_CPT.sCPTCode = CPT_MST.sCPTCode " +
                                    //////        //            " WHERE BL_TOS_CPT.nTOSID = " + _TOSID;
                                    //////    }
                                    //////}
                                    //////else
                                    //////{
                                    //////    if (TOSID == 0)
                                    //////    {
                                    //////        // _sqlQuery = "SELECT DISTINCT CPT_MST.nCPTID, BL_TOS_CPT.sCPTCode, BL_TOS_CPT.sCPTDesc FROM BL_TOS_CPT INNER JOIN CPT_MST ON BL_TOS_CPT.sCPTCode = CPT_MST.sCPTCode ";
                                    //////        _sqlQuery = "SELECT DISTINCT TOP 100 case  when ISNULL(nCPTID,0) >= 0 then 0 end nCPTID,ISNULL(sCPTCode,'') AS sCPTCode,ISNULL(sDescription,'') AS sDescription FROM CPT_MST WHERE nClinicID = " + _ClinicID + "";

                                    //////    }
                                    //////    else
                                    //////    {
                                    //////        _sqlQuery = "SELECT DISTINCT TOP 100 case  when ISNULL(nCPTID,0) >= 0 then 0 end nCPTID,ISNULL(sCPTCode,'') AS sCPTCode,ISNULL(sDescription,'') AS sDescription FROM CPT_MST WHERE nClinicID = " + _ClinicID + "";

                                    //////        //_sqlQuery = "SELECT DISTINCT CPT_MST.nCPTID, BL_TOS_CPT.sCPTCode, BL_TOS_CPT.sCPTDesc FROM BL_TOS_CPT INNER JOIN CPT_MST ON BL_TOS_CPT.sCPTCode = CPT_MST.sCPTCode " +
                                    //////        //            " WHERE BL_TOS_CPT.nTOSID = " + _TOSID;
                                    //////    }
                                    //////}
                                    //*******************************************************
                                    oParameters.Clear();
                                    oParameters.Add("@SearchString", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDB.Retrive("BL_GET_CPT_SEARCH", oParameters, out _dtList);
                                }
                                break;
                            case gloGridListControlType.ICD9:
                                {

                                    //*********************************************
                                    //Commented By Debasish Das. on 9th Aug 2010
                                    //*********************************************
                                    //_sqlQuery = "SELECT DISTINCT TOP 100 case  when ISNULL(nICD9ID,0) >= 0 then 0 end nICD9ID,ISNULL(sICD9Code,'') AS sICD9Code,ISNULL(sDescription,'') AS sDescription FROM ICD9 " +
                                    //" where (bIsBlocked IS NULL OR bIsBlocked = '" + false + "') AND (sICD9Code like '%" + SearchText + "%' OR sDescription like '%" + SearchText.Replace(".","") + "')";
                                    //if (_ClinicID > 0)
                                    //{
                                    //    _sqlQuery = _sqlQuery + " AND nClinicID = " + _ClinicID + "";
                                    //}
                                    //***********************************************

                                    oParameters.Clear();
                                    oParameters.Add("@SearchString", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDB.Retrive("BL_GET_DIAGONOSIS_SEARCH", oParameters, out _dtList);
                                }


                                break;
                            case gloGridListControlType.Modifier:
                                {
                                    //*********************************************
                                    //Commented By Debasish Das. on 9th Aug 2010
                                    //*********************************************
                                    //////////_sqlAlternetQuery = "SELECT ISNULL(nModifierID,0) AS nModifierID,ISNULL(sModifierCode,'') AS sModifierCode,ISNULL(sDescription,'') AS sDescription  FROM Modifier_MST where (bIsBlocked IS NULL OR bIsBlocked = '" + false + "') ";
                                    //////////if (_ClinicID > 0)
                                    //////////{
                                    //////////    _sqlAlternetQuery = _sqlAlternetQuery + " AND nClinicID = " + _ClinicID + "";
                                    //////////}

                                    //////////if (SelectedCPTCode.Trim() != "" && SelectedFacilityCode.Trim() != "")
                                    //////////{
                                    //////////    // *** Code changes made on 20090309 By - Sagar Ghodke
                                    //////////    // *** Code changes for Fee Schedule Table change
                                    //////////    // *** Below commented code is previous code 

                                    //////////    //_sqlQuery = " SELECT   DISTINCT ISNULL(Modifier_MST.nModifierID, 0) AS nModifierID, ISNULL(Modifier_MST.sModifierCode, '') AS sModifierCode, ISNULL(Modifier_MST.sDescription,'') AS sDescription " +
                                    //////////    //" FROM Modifier_MST INNER JOIN BL_ClinicFeeSchedule ON Modifier_MST.sModifierCode = BL_ClinicFeeSchedule.MOD " +
                                    //////////    //" WHERE (Modifier_MST.bIsBlocked IS NULL OR Modifier_MST.bIsBlocked = 'False') AND " +
                                    //////////    //" (BL_ClinicFeeSchedule.HCPCS = '" + SelectedCPTCode + "') AND (BL_ClinicFeeSchedule.SPEC = '" + SelectedFacilityCode + "') ";
                                    //////////    //if (_ClinicID > 0)
                                    //////////    //{
                                    //////////    //    _sqlQuery = _sqlQuery + " AND Modifier_MST.nClinicID = " + _ClinicID + "";
                                    //////////    //}

                                    //////////    _sqlQuery = " SELECT DISTINCT ISNULL(Modifier_MST.nModifierID, 0) AS nModifierID, " +
                                    //////////    " ISNULL(Modifier_MST.sModifierCode, '') AS sModifierCode,  " +
                                    //////////    " ISNULL(Modifier_MST.sDescription,'') AS sDescription " +
                                    //////////    " FROM Modifier_MST INNER JOIN BL_FeeSchedule_DTL ON Modifier_MST.sModifierCode = BL_FeeSchedule_DTL.sModifier " +
                                    //////////    " WHERE (Modifier_MST.bIsBlocked IS NULL OR Modifier_MST.bIsBlocked = 'False') AND " +
                                    //////////    " (BL_FeeSchedule_DTL.sHCPCS = '" + SelectedCPTCode + "') AND (BL_FeeSchedule_DTL.sSpecialtyID = '" + SelectedFacilityCode + "') ";
                                    //////////    if (_ClinicID > 0)
                                    //////////    {
                                    //////////        _sqlQuery = _sqlQuery + " AND Modifier_MST.nClinicID = " + _ClinicID + "";
                                    //////////    }

                                    //////////    // *** End Code Changes 20090309

                                    //////////    // *** Code changes made on 20090309 By - Sagar Ghodke
                                    //////////}
                                    //**************************************************************************

                                    oParameters.Clear();
                                    oParameters.Add("@SearchString", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDB.Retrive("BL_GET_MODIFIER_SEARCH", oParameters, out _dtList);
                                }
                                break;
                            case gloGridListControlType.PatientInsurance:
                                {
                                    _sqlQuery = "SELECT Distinct  PatientInsurance_DTL.nInsuranceID, Contacts_MST.sName AS Insurence" +
                                                " FROM  PatientInsurance_DTL WITH (NOLOCK) INNER JOIN Contacts_MST WITH (NOLOCK) ON PatientInsurance_DTL.nInsuranceID = Contacts_MST.nContactID " +
                                                " WHERE  PatientInsurance_DTL.nPatientID = " + _PatientID;
                                }
                                break;
                            case gloGridListControlType.POS:
                                {
                                    if (_ClinicID == 0)
                                        _sqlQuery = "select ISNULL(nPOSID,0) AS nPOSID,ISNULL(sPOSCode,'') AS sPOSCode,ISNULL(sPOSName,'') AS sDescription from BL_POS_MST WITH (NOLOCK) where (bIsBlocked IS NULL OR bIsBlocked = '" + false + "') ORDER BY sPOSName";
                                    else
                                        _sqlQuery = "select ISNULL(nPOSID,0) AS nPOSID,ISNULL(sPOSCode,'') AS sPOSCode,ISNULL(sPOSName,'') AS sDescription from BL_POS_MST WITH (NOLOCK) where (bIsBlocked IS NULL OR bIsBlocked = '" + false + "') AND nClinicID = " + _ClinicID + " ORDER BY sPOSName";

                                }
                                break;
                            case gloGridListControlType.TOS:
                                {
                                    if (_ClinicID == 0)
                                        _sqlQuery = "select ISNULL(nTOSID,0) AS nTOSID,ISNULL(sTOSCode,'') AS sTOSCode ,ISNULL(sDescription,'') AS sDescription from BL_TOS_MST WITH (NOLOCK) where (bIsBlocked IS NULL OR bIsBlocked = '" + false + "') ORDER BY sDescription";
                                    else
                                        _sqlQuery = "select ISNULL(nTOSID,0) AS nTOSID,ISNULL(sTOSCode,'') AS sTOSCode,ISNULL(sDescription,'') AS sDescription from BL_TOS_MST WITH (NOLOCK) where (bIsBlocked IS NULL OR bIsBlocked = '" + false + "') AND nClinicID = " + _ClinicID + " ORDER BY sDescription";
                                }
                                break;
                            //Added By Pramod Nair For Reason Codes
                            case gloGridListControlType.ReasonCodes:
                                {
                                    if (_ClinicID == 0)
                                        _sqlQuery = "select nReasonID,isnull(sGroupCode,'') + isnull(sCode,'') as ReasonCode ,sDescription from BL_ReasonCodes_MST WITH (NOLOCK) where (bIsBlock IS NULL OR bIsBlock = '" + false + "') ORDER BY ReasonCode,nReasonID";
                                    else
                                        _sqlQuery = "select nReasonID,isnull(sGroupCode,'') + isnull(sCode,'') as ReasonCode ,sDescription from BL_ReasonCodes_MST WITH (NOLOCK) where (bIsBlock IS NULL OR bIsBlock = '" + false + "') AND nClinicID = " + _ClinicID + " ORDER BY ReasonCode,nReasonID";
                                }
                                break;
                           
                            default:
                                break;
                        }

                        if (_sqlQuery.Trim() != "")
                        {
                            oDB.Retrive_Query(_sqlQuery, out _dtList);

                            //Special Case
                            if (_ControlType == gloGridListControlType.Modifier)
                            {
                                if (_dtList != null)
                                {
                                    //Code commented on 20090129 by Sagar to empty inner list
                                    //if (_dtList.Rows.Count <= 0)
                                    //{
                                    _dtList = new DataTable();
                                    oDB.Retrive_Query(_sqlAlternetQuery, out _dtList);
                                    //}
                                }
                            }


                            if (_dtList != null)
                            {
                                //Code commented on 20090129 by Sagar to empty inner list
                                //if (_dtList.Rows.Count > 0)
                                //{
                                _dv = _dtList.DefaultView;
                                c1GridList.DataSource = _dv;
                                DesignGridList_V2();
                                //}
                                //else
                                //{ _dv = null; }
                            }
                            else
                            { _dv = null; }
                        }
                        //*********************************************************************
                        //COMMENTED BY DEBASISH DAS ON 09TH AUG
                        //*********************************************************************
                        //else if (_ControlType == gloGridListControlType.Modifier)
                        //{
                        //    if (_sqlAlternetQuery.Trim() != "")
                        //    {
                        //        _dtList = new DataTable();
                        //        oDB.Retrive_Query(_sqlAlternetQuery, out _dtList);
                        //        if (_dtList != null)
                        //        {
                        //            //Code commented on 20090129 by Sagar to empty inner list
                        //            //if (_dtList.Rows.Count > 0)
                        //            //{
                        //            _dv = _dtList.DefaultView;
                        //            c1GridList.DataSource = _dv;
                        //            DesignGridList();
                        //            //}
                        //            //else
                        //            //{ _dv = null; }
                        //        }
                        //        else
                        //        { _dv = null; }
                        //    }
                        //}
                        //********************************************************************
                        else
                        {
                            if (_dtList != null)
                            {
                                //Code commented on 20090129 by Sagar to empty inner list
                                //if (_dtList.Rows.Count > 0)
                                //{
                                _dv = _dtList.DefaultView;
                                c1GridList.DataSource = _dv;
                                DesignGridList_V2();
                                //}
                                //else
                                //{ _dv = null; }
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
            }
        }

        public void FillControl(string SearchText)
        {
            //this code was commented 
            gloDatabaseLayer.DBLayer oDB = null;
            //this code was commented 
            DataTable _dtTempTable = null;

            try
            {
                SearchText = SearchText.Trim().Replace("'", "''").Replace("[", "").Replace("]", "");
                _ControlSearchText = SearchText;

               // if (_ControlType != null)
                {
                    if (_DatabaseConnectionString != "")
                    {
                        string _sqlQuery = "";
                        string _sqlAlternetQuery = "";
                        //this code was commented 
                        oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                        gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                        oDB.Connect(false);
                        //this code was commented 
                        switch (_ControlType)
                        {
                            case gloGridListControlType.Providers:
                                {
                                    _dtTempTable = gloGlobal.gloPMMasters.GetProviders();

                                    //..Remove the unwanted columns from the data table here
                                    //..this is done here because if the common get provider method changes 
                                    //..(by adding new columns) then on this control we need to change the design grid each time
                                    //..(To minimize the impact)
                                    //..This control only needs nProviderID & sProvider Name

                                    _dtList = _dtTempTable.Copy();

                                    if (_dtTempTable != null && _dtTempTable.Columns.Count > 0)
                                    {
                                        foreach (DataColumn _dc in _dtTempTable.Columns)
                                        {
                                            if (_dc.ColumnName != "nProviderID" &&  _dc.ColumnName != "sProviderName")
                                            {
                                                _dtList.Columns.Remove(_dc.ColumnName);
                                                _dtList.AcceptChanges();
                                            }
                                        }

                                        if (_dtTempTable != null) { _dtTempTable.Dispose(); }
                                    }
                                    

                                }
                                break;
                            case gloGridListControlType.CPT:
                                {
                                    oParameters.Clear();
                                    oParameters.Add("@SearchString", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDB.Retrive("BL_GET_CPT_SEARCH", oParameters, out _dtList);
                                }
                                break;
                            case gloGridListControlType.ICD9:
                                {
                                    
                                    oParameters.Clear();
                                    oParameters.Add("@SearchString", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                                    oParameters.Add("@nICDRevision", IcdCodeType.GetHashCode(), ParameterDirection.Input, SqlDbType.SmallInt);
                                    //oDB.Retrive("gsp_Diagnosis_Search", oParameters, out _dtList);
                                    oDB.Retrive("gsp_Diagnosis_Search_ChargeEntry", oParameters, out _dtList);
                                }


                                break;
                            case gloGridListControlType.Modifier:
                                {
                                    oParameters.Clear();
                                    oParameters.Add("@SearchString", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDB.Retrive("BL_GET_MODIFIER_SEARCH", oParameters, out _dtList);
                                }
                                break;
                            case gloGridListControlType.PatientInsurance:
                                {
                                    _sqlQuery = "SELECT Distinct  PatientInsurance_DTL.nInsuranceID, Contacts_MST.sName AS Insurence" +
                                                " FROM  PatientInsurance_DTL WITH (NOLOCK) INNER JOIN Contacts_MST WITH (NOLOCK) ON PatientInsurance_DTL.nInsuranceID = Contacts_MST.nContactID " +
                                                " WHERE  PatientInsurance_DTL.nPatientID = " + _PatientID;
                                }
                                break;
                            case gloGridListControlType.POS:
                                {
                                    //_dtList = gloGlobal.gloPMMasters.GetPlaceOfServices();
                                    _dtTempTable = gloGlobal.gloPMMasters.GetPlaceOfServices();

                                    //..Remove the unwanted columns from the data table here
                                    //..this is done here because if the common get POS method changes 
                                    //..(by adding new columns) then on this control we need to change the design grid each time
                                    //..(To minimize the impact)
                                    //..This control only needs nPOSID,sPOSCode & sPOSName

                                    _dtList = _dtTempTable.Copy();

                                    if (_dtTempTable != null && _dtTempTable.Columns.Count > 0)
                                    {
                                        foreach (DataColumn _dc in _dtTempTable.Columns)
                                        {
                                            if (_dc.ColumnName != "nPOSID" && _dc.ColumnName != "sPOSCode" && _dc.ColumnName != "sPOSName")
                                            {
                                                _dtList.Columns.Remove(_dc.ColumnName);
                                                _dtList.AcceptChanges();
                                            }
                                        }

                                        if (_dtTempTable != null) { _dtTempTable.Dispose(); }
                                    }
                                }
                                break;
                            case gloGridListControlType.TOS:
                                {
                                    _dtList = gloGlobal.gloPMMasters.GetTypeOfServices();

                                    //..Remove the unwanted columns from the data table here
                                    //..this is done here because if the common get POS method changes 
                                    //..(by adding new columns) then on this control we need to change the design grid each time
                                    //..(To minimize the impact)
                                    //..This control only needs nPOSID,sPOSCode & sPOSName

                                    _dtList = _dtTempTable.Copy();

                                    if (_dtTempTable != null && _dtTempTable.Columns.Count > 0)
                                    {
                                        foreach (DataColumn _dc in _dtTempTable.Columns)
                                        {
                                            if (_dc.ColumnName != "nTOSID" && _dc.ColumnName != "sTOSCode" && _dc.ColumnName != "sDescription")
                                            {
                                                _dtList.Columns.Remove(_dc.ColumnName);
                                                _dtList.AcceptChanges();
                                            }
                                        }

                                        if (_dtTempTable != null) { _dtTempTable.Dispose(); }
                                    }
                                }
                                break;
                            
                            case gloGridListControlType.ReasonCodes:
                                {
                                    if (_ClinicID == 0)
                                        _sqlQuery = "select nReasonID,isnull(sGroupCode,'') + isnull(sCode,'') as ReasonCode ,sDescription from BL_ReasonCodes_MST WITH (NOLOCK) where (bIsBlock IS NULL OR bIsBlock = '" + false + "') ORDER BY ReasonCode,nReasonID";
                                    else
                                        _sqlQuery = "select nReasonID,isnull(sGroupCode,'') + isnull(sCode,'') as ReasonCode ,sDescription from BL_ReasonCodes_MST WITH (NOLOCK) where (bIsBlock IS NULL OR bIsBlock = '" + false + "') AND nClinicID = " + _ClinicID + " ORDER BY ReasonCode,nReasonID";
                                }
                                break;
                            case gloGridListControlType.RemarkCodes:
                                {
                                    if (_ClinicID == 0)
                                        _sqlQuery = "select nRemarkID,sRemarkCode ,sRemarkDescription from BL_RemarkCodes_MST WITH (NOLOCK) where (bIsBlock IS NULL OR bIsBlock = '" + false + "') ORDER BY sRemarkCode,nRemarkID";
                                    else
                                        _sqlQuery = "select nRemarkID,sRemarkCode ,sRemarkDescription from BL_RemarkCodes_MST WITH (NOLOCK) where (bIsBlock IS NULL OR bIsBlock = '" + false + "')  AND nClinicID = " + _ClinicID + " ORDER BY sRemarkCode,nRemarkID";

                                }
                                break;
                            case gloGridListControlType.CondtionCodes:
                                {
                                    _sqlQuery = "select sConditionCode ,sDescription  from UB_ConditionCodes where IsActive != 0";
                                }
                                break;
                            case gloGridListControlType.OccurrenceCode: 
                                {
                                    _sqlQuery = "select sOccurrenceCode  ,sDescription  from UB_OccurrenceCodes where IsActive != 0";
                                }
                                break;
                            case gloGridListControlType.OccurrenceSpanCode :
                                {
                                    _sqlQuery = "select sOccurrenceSpanCode,sDescription  from UB_OccurrenceSpanCodes where IsActive != 0";
                                }
                                break;
                            case gloGridListControlType.ValueCodes :
                                {
                                    _sqlQuery = "select sValueCode  ,sDescription  from UB_ValueCodes where IsActive != 0 ";
                                }
                                break;
                            case gloGridListControlType.InsFollowupMapping:
                                {
                                    oParameters.Clear();
                                   // oParameters.Add("@FollowupCodeType", ParentColIndex, ParameterDirection.Input, SqlDbType.Int);
                                    oParameters.Add("@SearchString", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDB.Retrive("gsp_InsFollowupSearchCode", oParameters, out _dtList);
                                }
                                break;
                            default:
                                break;
                        }

                        if (_sqlQuery.Trim() != "")
                        {
                            oDB.Retrive_Query(_sqlQuery, out _dtList);

                            //Special Case
                            if (_ControlType == gloGridListControlType.Modifier)
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
                                DesignGridList_V2();
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
                                DesignGridList_V2();
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
            }
        }

        #endregion " Fill Control "

        #region " C1 Grid Design "

        //private void DesignGridList()
        //{
        //    int _Width = this.Width - 3;
        //    try
        //    {
        //        c1GridList.AllowEditing = false;

        //        switch (_ControlType)
        //        {
        //            case gloGridListControlType.Providers:
        //                {
        //                    //nResourceID,sFirstName,sMiddleName,sLastName
        //                    c1GridList.Cols["nResourceID"].Visible = false;
        //                    c1GridList.Cols["ProviderName"].Visible = true;


        //                    c1GridList.Cols["nResourceID"].Width = 0;
        //                    c1GridList.Cols["ProviderName"].Width = Convert.ToInt32(_Width * 1);

        //                }
        //                break;
        //            case gloGridListControlType.CPT:
        //                {
        //                    //nCPTID,sCPTCode,sCPTDesc 
        //                    c1GridList.Cols["nCPTID"].Visible = false;
        //                    c1GridList.Cols["sCPTCode"].Visible = true;
        //                    c1GridList.Cols["sDescription"].Visible = true;
        //                    c1GridList.Cols["bIsReferralRequired"].Visible = false;

        //                    c1GridList.Cols["nCPTID"].Width = 0;
        //                    c1GridList.Cols["sCPTCode"].Width = Convert.ToInt32(_Width * 0.20);
        //                    c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
        //                    c1GridList.Cols["bIsReferralRequired"].Width = 0;
        //                }
        //                break;
        //            case gloGridListControlType.ICD9:
        //                {
        //                    //nICD9ID, sICD9Code, sDescription    
        //                    c1GridList.Cols["nICD9ID"].Visible = false;
        //                    c1GridList.Cols["sICD9Code"].Visible = true;
        //                    c1GridList.Cols["sDescription"].Visible = true;

        //                    c1GridList.Cols["nICD9ID"].Width = 0;
        //                    c1GridList.Cols["sICD9Code"].Width = Convert.ToInt32(_Width * 0.20);
        //                    c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
        //                }
        //                break;
        //            case gloGridListControlType.Modifier:
        //                {
        //                    //nModifierID,sModifierCode,sDescription
        //                    c1GridList.Cols["nModifierID"].Visible = false;
        //                    c1GridList.Cols["sModifierCode"].Visible = true;
        //                    c1GridList.Cols["sDescription"].Visible = true;

        //                    c1GridList.Cols["nModifierID"].Width = 0;
        //                    c1GridList.Cols["sModifierCode"].Width = Convert.ToInt32(_Width * 0.20);
        //                    c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
        //                }
        //                break;
        //            case gloGridListControlType.PatientInsurance:
        //                {

        //                }
        //                break;
        //            case gloGridListControlType.POS:
        //                {
        //                    //nPOSID,sPOSCode,sDescription
        //                    c1GridList.Cols["nPOSID"].Visible = false;
        //                    c1GridList.Cols["sPOSCode"].Visible = true;
        //                    c1GridList.Cols["sDescription"].Visible = true;

        //                    c1GridList.Cols["nPOSID"].Width = 0;
        //                    c1GridList.Cols["sPOSCode"].Width = Convert.ToInt32(_Width * 0.20);
        //                    c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
        //                }
        //                break;
        //            case gloGridListControlType.TOS:
        //                {
        //                    //nTOSID,sTOSCode ,sDescription
        //                    c1GridList.Cols["nTOSID"].Visible = false;
        //                    c1GridList.Cols["sTOSCode"].Visible = true;
        //                    c1GridList.Cols["sDescription"].Visible = true;

        //                    c1GridList.Cols["nTOSID"].Width = 0;
        //                    c1GridList.Cols["sTOSCode"].Width = Convert.ToInt32(_Width * 0.20);
        //                    c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
        //                }
        //                break;

        //            //Added By Pramod Nair For Reason Codes
        //            case gloGridListControlType.ReasonCodes:
        //                {
        //                    //nTOSID,sTOSCode ,sDescription
        //                    c1GridList.Cols["nReasonID"].Visible = false;
        //                    c1GridList.Cols["ReasonCode"].Visible = true;
        //                    c1GridList.Cols["sDescription"].Visible = true;

        //                    c1GridList.Cols["nReasonID"].Width = 0;
        //                    c1GridList.Cols["ReasonCode"].Width = Convert.ToInt32(_Width * 0.20);
        //                    c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
        //                }
        //                break;

        //            default:
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {

        //    }
        //}

        private void DesignGridList_V2()
        {
            int _Width = this.Width - 3;
            try
            {
                c1GridList.AllowEditing = false;

                switch (_ControlType)
                {
                    case gloGridListControlType.Providers:
                        {
                            //nResourceID,sFirstName,sMiddleName,sLastName
                            c1GridList.Cols["nProviderID"].Visible = false;
                            c1GridList.Cols["sProviderName"].Visible = true;


                            c1GridList.Cols["nProviderID"].Width = 0;
                            c1GridList.Cols["sProviderName"].Width = Convert.ToInt32(_Width * 1);

                        }
                        break;
                    case gloGridListControlType.CPT:
                        {
                            //nCPTID,sCPTCode,sCPTDesc 
                            c1GridList.Cols["nCPTID"].Visible = false;
                            c1GridList.Cols["sCPTCode"].Visible = true;
                            c1GridList.Cols["sDescription"].Visible = true;
                            c1GridList.Cols["bIsReferralRequired"].Visible = false;

                            c1GridList.Cols["nCPTID"].Width = 0;
                            c1GridList.Cols["sCPTCode"].Width = Convert.ToInt32(_Width * 0.20);
                            c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
                            c1GridList.Cols["bIsReferralRequired"].Width = 0;

                            c1GridList.Cols["bIsReferralRequired"].DataType = typeof(System.Boolean);
                        }
                        break;
                    case gloGridListControlType.ICD9:
                        {
                            //nICD9ID, sICD9Code, sDescription    
                            c1GridList.Cols["nICD9ID"].Visible = false;
                            c1GridList.Cols["sICD9Code"].Visible = true;
                            c1GridList.Cols["sDescription"].Visible = true;
                            c1GridList.Cols["InvalidICD9"].Visible = false;
                            c1GridList.Cols["nICDRevision"].Visible = false;

                            c1GridList.Cols["nICD9ID"].Width = 0;
                            c1GridList.Cols["sICD9Code"].Width = Convert.ToInt32(_Width * 0.20);
                            c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
                            c1GridList.Cols["InvalidICD9"].Width = 0;
                            c1GridList.Cols["nICDRevision"].Width = 0;

                        }
                        break;
                    case gloGridListControlType.Modifier:
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
                    case gloGridListControlType.PatientInsurance:
                        {

                        }
                        break;
                    case gloGridListControlType.POS:
                        {
                            //nPOSID,sPOSCode,sDescription
                            c1GridList.Cols["nPOSID"].Visible = false;
                            c1GridList.Cols["sPOSCode"].Visible = true;
                            c1GridList.Cols["sPOSName"].Visible = true;
                            
                            c1GridList.Cols["nPOSID"].Width = 0;
                            c1GridList.Cols["sPOSCode"].Width = Convert.ToInt32(_Width * 0.20);
                            c1GridList.Cols["sPOSName"].Width = Convert.ToInt32(_Width * 0.80);
                        }
                        break;
                    case gloGridListControlType.TOS:
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
                    case gloGridListControlType.ReasonCodes:
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
                    case gloGridListControlType.RemarkCodes:
                        {
                            //nTOSID,sTOSCode ,sDescription
                            c1GridList.Cols["nRemarkID"].Visible = false;
                            c1GridList.Cols["sRemarkCode"].Visible = true;
                            c1GridList.Cols["sRemarkDescription"].Visible = true;

                           // c1GridList.Cols["nRemarkID"].Width = 0;
                            c1GridList.Cols["sRemarkCode"].Width = Convert.ToInt32(_Width * 0.20);
                            c1GridList.Cols["sRemarkDescription"].Width = Convert.ToInt32(_Width * 0.80);
                        }
                        break;
                    case gloGridListControlType.CondtionCodes:
                        {
                            //nTOSID,sTOSCode ,sDescription
                           
                                c1GridList.Cols["sConditionCode"].Visible = true;
                                c1GridList.Cols["sDescription"].Visible = true;
                                c1GridList.Cols["sConditionCode"].Width = Convert.ToInt32(_Width * 0.20);
                                c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
                                if (c1GridList.DataSource == null)
                                {
                                    c1GridList.Rows.Count = 0;
                                }
                        }
                        break;
                    case gloGridListControlType.OccurrenceCode:
                        {
                            //nTOSID,sTOSCode ,sDescription
                            c1GridList.Cols["sOccurrenceCode"].Visible = true;
                            c1GridList.Cols["sDescription"].Visible = true;
                            c1GridList.Cols["sOccurrenceCode"].Width = Convert.ToInt32(_Width * 0.20);
                            c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
                            if (c1GridList.DataSource == null)
                            {
                                c1GridList.Rows.Count = 0;
                            }
                        }
                        break;
                    case gloGridListControlType.OccurrenceSpanCode :
                        {
                            //nTOSID,sTOSCode ,sDescription
                            c1GridList.Cols["sOccurrenceSpanCode"].Visible = true;
                            c1GridList.Cols["sDescription"].Visible = true;
                            c1GridList.Cols["sOccurrenceSpanCode"].Width = Convert.ToInt32(_Width * 0.20);
                            c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
                            if (c1GridList.DataSource == null)
                            {
                                c1GridList.Rows.Count = 0;
                            }
                        }
                        break;
                    case gloGridListControlType.ValueCodes:
                        {
                            //nTOSID,sTOSCode ,sDescription
                            c1GridList.Cols["sValueCode"].Visible = true;
                            c1GridList.Cols["sDescription"].Visible = true;
                            c1GridList.Cols["sValueCode"].Width = Convert.ToInt32(_Width * 0.20);
                            c1GridList.Cols["sDescription"].Width = Convert.ToInt32(_Width * 0.80);
                            if (c1GridList.DataSource == null)
                            {
                                c1GridList.Rows.Count = 0;
                            }
                        }
                        break;
                    case gloGridListControlType.InsFollowupMapping:
                        {
                            c1GridList.Cols["ID"].Visible = false;
                            c1GridList.Cols["StdFollowupActionCode"].Visible = true;
                            c1GridList.Cols["StdFollowupActionDesc"].Visible = true;


                            c1GridList.Cols["ID"].Width = 0;
                            c1GridList.Cols["StdFollowupActionCode"].Width = Convert.ToInt32(_Width * 0.30);
                            c1GridList.Cols["StdFollowupActionDesc"].Width = Convert.ToInt32(_Width * 0.80);
                            if (c1GridList.DataSource == null)
                            {
                                c1GridList.Rows.Count = 0;
                            }

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

        public void Search(string SearchText, SearchColumn SearchCol)
        {
            int _colIndex = 0;
            try
            {
                SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "");
                DataView _dvTemp = (DataView)c1GridList.DataSource;

                switch (_ControlType)
                {
                    case gloGridListControlType.Providers:
                        break;
                    case gloGridListControlType.PatientInsurance:
                        break;
                    case gloGridListControlType.CPT:
                    case gloGridListControlType.ICD9:
                    case gloGridListControlType.Modifier:
                    case gloGridListControlType.POS:
                    case gloGridListControlType.TOS:
                    case gloGridListControlType.ReasonCodes:
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
                            DesignGridList_V2();
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
         //   int _colCodeIndex = 0;
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
                    case gloGridListControlType.PatientInsurance:
                        break;
                    case gloGridListControlType.Providers:
                        {
                            //_colCodeIndex = 0;
                            _colDescriptionIndex = 1;
                            _searchStringArray = SearchText.Split(',');
                        }
                        break;
                    case gloGridListControlType.CPT:
                    case gloGridListControlType.ICD9:
                    case gloGridListControlType.Modifier:
                    case gloGridListControlType.POS:
                    case gloGridListControlType.TOS:
                    case gloGridListControlType.ReasonCodes:
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
                DesignGridList_V2();


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
                    case gloGridListControlType.PatientInsurance:
                        break;
                    case gloGridListControlType.Providers:
                        {
                            _colCodeIndex = 0;
                            _colDescriptionIndex = 1;
                            _searchStringArray = SearchText.Split(',');
                        }
                        break;
                    case gloGridListControlType.CPT:
                    case gloGridListControlType.ICD9:
                    case gloGridListControlType.Modifier: 
                    case gloGridListControlType.POS:
                    case gloGridListControlType.TOS:
                    case gloGridListControlType.ReasonCodes:
                        {
                            _colCodeIndex = 1;
                            _colDescriptionIndex = 2;
                            _searchStringArray = SearchText.Split(',');
                        }
                        break;
                    case gloGridListControlType.RemarkCodes:
                        {
                            _colCodeIndex = 1;
                            _colDescriptionIndex = 2;
                            _searchStringArray = SearchText.Split(',');
                        }
                        break;
                    case gloGridListControlType.CondtionCodes :
                        {
                            _colCodeIndex = 0;
                            _colDescriptionIndex = 1;
                            _searchStringArray = SearchText.Split(',');
                        }
                        break;
                    case gloGridListControlType.OccurrenceCode:
                        {
                            _colCodeIndex = 0;
                            _colDescriptionIndex = 1;
                            _searchStringArray = SearchText.Split(',');
                        }
                        break;
                    case gloGridListControlType.OccurrenceSpanCode :
                        {
                            _colCodeIndex = 0;
                            _colDescriptionIndex = 1;
                            _searchStringArray = SearchText.Split(',');
                        }
                        break;
                    case gloGridListControlType.ValueCodes:
                        {
                            _colCodeIndex = 0;
                            _colDescriptionIndex = 1;
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
                        if (_ControlType == gloGridListControlType.Providers)
                        {
                            _dvTemp.RowFilter = _dvTemp.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%" + _searchString + "%' ";
                        }
                        //*********************************************
                        //Commented By Debasish Das. on 9th Aug 2010
                        //*********************************************

                        //else if (_ControlType == gloGridListControlType.Modifier) //MaheshB
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
                            if (_searchString.Trim() != "")
                            {
                                if (i == 0)
                                {
                                    dtTemp = _dvTemp.ToTable();
                                    dvNext = dtTemp.DefaultView;
                                }
                                else
                                {
                                    if (dvNext != null)
                                    {
                                        dtTemp = dvNext.ToTable();
                                    }
                                    if (dtTemp != null)
                                    {
                                        dvNext = dtTemp.DefaultView;
                                    }
                                }

                                if (_ControlType == gloGridListControlType.Providers)
                                {
                                    dvNext.RowFilter = dvNext.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%" + _searchString + "%' ";
                                }
                                //*********************************************
                                //Commented By Debasish Das. on 9th Aug 2010
                                //*********************************************

                                //else if (_ControlType == gloGridListControlType.Modifier) //MaheshB 
                                //{
                                //    dvNext.RowFilter = dvNext.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%" + _searchString + "%' OR " +
                                //                       dvNext.Table.Columns[_colCodeIndex].ColumnName + " Like '%" + _searchString + "%' ";

                                //}
                                //*********************************************
                                else
                                {
                                    if (dvNext != null)
                                    {
                                        dvNext.RowFilter = dvNext.Table.Columns[_colDescriptionIndex].ColumnName + " Like '%" + _searchString + "%' OR " +
                                                           dvNext.Table.Columns[_colCodeIndex].ColumnName + " Like '%" + _searchString + "%' ";
                                    }
                                }
                            }
                        }
                        //if (_searchString != "")
                        //{
                            if (_searchStringArray.Length > 1)
                            {
                                //if (dvNext != null)
                                //{
                                    _dvTemp = dvNext;
                                //}
                            }
                        //}
                    }

                }
                else
                {
                    _dvTemp.RowFilter = "";
                    _dvTemp.RowStateFilter = DataViewRowState.OriginalRows;
                }

                c1GridList.DataSource = _dvTemp;
                DesignGridList_V2();


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

        #endregion " Search Functionality "

        #region " Grid Events "

        private void c1GridList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int rowIndex = 0;
            Int64 _id = 0;
            string _code = "";
            string _desc = "";
            string _isReferralCPT = "False";
            bool _isInvalidICDForEDI = false;

            try
            {
                if (c1GridList != null)
                {
                    if (c1GridList.Rows.Count > 0)
                    {
                        rowIndex = c1GridList.RowSel;
                        switch (_ControlType)
                        {
                            case gloGridListControlType.Providers:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0)); //Provider ID 
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                            case gloGridListControlType.PatientInsurance:
                                break;
                            case gloGridListControlType.CPT:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 2));
                                    _isReferralCPT = Convert.ToString(c1GridList.GetData(rowIndex, 3));
                                }
                                break;
                            case gloGridListControlType.ICD9:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 2));
                                    
                                    if (c1GridList.GetData(rowIndex, 3) != null && Convert.ToString(c1GridList.GetData(rowIndex,3)).Trim() != "")
                                    {
                                        _isInvalidICDForEDI = true;
                                    }
                                }
                                break;
                            case gloGridListControlType.Modifier:
                            case gloGridListControlType.POS:
                            case gloGridListControlType.TOS:
                            case gloGridListControlType.ReasonCodes:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 2));
                                }
                                break;
                            case gloGridListControlType.RemarkCodes:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 2));
                                }
                                break;
                            case gloGridListControlType.CondtionCodes: 
                                {
                                    
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                            case gloGridListControlType.OccurrenceCode:
                                {

                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                            case gloGridListControlType.OccurrenceSpanCode :
                                {

                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                            case gloGridListControlType.ValueCodes:
                                {

                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                            case gloGridListControlType.InsFollowupMapping:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 2));
                                }
                                break;
                        }
                    }
                }
                gloGeneralItem.gloItem oListItem = new gloGeneralItem.gloItem();
                oListItem.ID = _id;
                oListItem.Code = _code;
                oListItem.Description = _desc;
                if (_ControlType == gloGridListControlType.CPT)
                {
                    //The status field of gloItem will hold the IsReferralCPT for CPT 
                    if (_isReferralCPT.Trim() != "") { oListItem.Status = _isReferralCPT; }
                }
                else if (_ControlType == gloGridListControlType.ICD9)
                {
                    oListItem.Status = _isInvalidICDForEDI.ToString();
                }

                _SelectedItems.Clear();
                _SelectedItems.Add(oListItem);

            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ItemSelected(null, null);
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
        //                        case gloGridListControlType.Providers:
        //                            {
        //                                _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
        //                                _code = Convert.ToString(c1GridList.GetData(rowIndex, 0)); //Provider ID 
        //                                _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
        //                            }
        //                            break;
        //                        case gloGridListControlType.PatientInsurance:
        //                            break;
        //                        case gloGridListControlType.CPT:
        //                        case gloGridListControlType.ICD9:
        //                        case gloGridListControlType.Modifier:
        //                        case gloGridListControlType.POS:
        //                        case gloGridListControlType.TOS:
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
            string _isReferralCPT = "False";
            bool _isInvalidICDForEDI = false;
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
                            case gloGridListControlType.Providers:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0)); //Provider ID 
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                            case gloGridListControlType.PatientInsurance:
                                break;
                            case gloGridListControlType.CPT:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 2));
                                    _isReferralCPT = Convert.ToString(c1GridList.GetData(rowIndex, 3));
                                }
                                break;
                            case gloGridListControlType.ICD9:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 2));
                                    if (c1GridList.GetData(rowIndex, 3) != null && Convert.ToString(c1GridList.GetData(rowIndex, 3)).Trim() != "")
                                    {
                                        _isInvalidICDForEDI = true;
                                    }
                                }
                                break;
                            case gloGridListControlType.Modifier:
                            case gloGridListControlType.POS:
                            case gloGridListControlType.TOS:
                            case gloGridListControlType.ReasonCodes:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 2));
                                }
                                break;
                            case gloGridListControlType.RemarkCodes:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 2));
                                }
                                break;
                            case gloGridListControlType.CondtionCodes:
                                {

                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                            case gloGridListControlType.OccurrenceCode:
                                {

                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                            case gloGridListControlType.OccurrenceSpanCode:
                                {

                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                            case gloGridListControlType.ValueCodes:
                                {

                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 0));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                }
                                break;
                            case gloGridListControlType.InsFollowupMapping:
                                {
                                    _id = Convert.ToInt64(c1GridList.GetData(rowIndex, 0));
                                    _code = Convert.ToString(c1GridList.GetData(rowIndex, 1));
                                    _desc = Convert.ToString(c1GridList.GetData(rowIndex, 2));
                                }
                                break;
                        }

                        gloGeneralItem.gloItem oListItem = new gloGeneralItem.gloItem();
                        oListItem.ID = _id;
                        oListItem.Code = _code;
                        oListItem.Description = _desc;


                        if (_ControlType == gloGridListControlType.CPT)
                        { 
                            if (_isReferralCPT.Trim() != "") { oListItem.Status = _isReferralCPT; }
                        }
                        else if (_ControlType == gloGridListControlType.ICD9)
                        {
                            oListItem.Status = _isInvalidICDForEDI.ToString();
                        }


                        _SelectedItems.Add(oListItem);
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
            }

            return _retValue;
        }

        private bool IsTableExists(string strTableName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@sTableName", strTableName, ParameterDirection.InputOutput, SqlDbType.VarChar);
                object _oresult = new object();
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

            }

        }

        private void c1GridList_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

        }

        private void gloGridListControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (oDB != null)
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }


        //End  - Code Changes 2000801013,Sagar Ghodke

        #endregion " Public & Private Methods "

    }
}
