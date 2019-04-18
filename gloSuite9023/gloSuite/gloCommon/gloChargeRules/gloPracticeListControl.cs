using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

namespace ChargeRules
{
    //public enum gloGridListControlType
    //{
    //    COL_LineNo = 0,
    //    COL_Group = 1,
    //    COL_AndOr = 2,
    //    COL_Field = 3,
    //    COL_Operator = 4,
    //    COL_Value = 5


    //}

    public partial class gloPracticeListControl : UserControl
    {

        #region " Variable Declarations "
        private string _DatabaseConnectionString = "";
        private string _messageBoxCaption = "gloPM";
        private gloGridListControlType _ControlType;
        private bool _ismultiselect;
        private int _parentRowIndex = 0;
        private int _parentColIndex = 0;
        private string _ControlHeader = "";
        private int _CurrentSelectedRow = 0;
        DataTable _dtList = null;
        
       // gloDatabaseLayer.DBLayer oDB = null;
       // gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private int COL_Value = 0;
        private int COL_Description = 1;
        private int COL_ID = 2;
        private gloItems _SelectedItems = null;
        private bool bIsDescriptionColumnVisible= false;
        private string _sAUSID =string.Empty;
        private string _sServiceURL = string.Empty;

        

        #endregion

        #region propeties
        public bool IsHideControl
        {
            get;
            set;
        }
        public int ParentColIndex
        {
            get { return _parentColIndex; }
            set { _parentColIndex = value; }
        }
        public int ParentRowIndex
        {
            get { return _parentRowIndex; }
            set { _parentRowIndex = value; }
        }
        public string ControlHeader
        {
            get { return _ControlHeader; }
            set { _ControlHeader = value; }
        }
        public string DatabaseConnectionString
        {
            get { return _DatabaseConnectionString; }
            set { _DatabaseConnectionString = value; }
        }

        public gloItems SelectedItems
        {
            get { return _SelectedItems; }
            set { _SelectedItems = value; }
        }
        public string sAUSID
        {
            get { return _sAUSID; }
            set { _sAUSID = value; }
        }
        public string ServiceURL
        {
            get { return _sServiceURL; }
            set { _sServiceURL = value; }
        }
        #endregion

        #region constructors and destructor
        public gloPracticeListControl()
        {
            InitializeComponent();

            #region connstring
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
            #endregion

        }
       
        public gloPracticeListControl(gloGridListControlType ControlType, bool IsMultiSelect, int ControlWidth, int ParentRowIndex, int ParentColIndex)
        {

            #region connstring
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
            #endregion

            InitializeComponent();
         
            _ControlType = ControlType;
            _ismultiselect = IsMultiSelect;
            this.Width = ControlWidth;
            _SelectedItems = new gloItems();
            this.ParentColIndex = ParentColIndex;
            this.ParentRowIndex = ParentRowIndex;
         //   oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
         //   oDB.Connect(false);
        }
        public gloPracticeListControl(gloGridListControlType ControlType, string ControlHeader,bool IsMultiSelect, int ControlWidth, int ParentRowIndex, int ParentColIndex)
        {

            #region connstring
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
            #endregion
            InitializeComponent();
            _ControlType = ControlType;
            _ControlHeader = ControlHeader;
            _ismultiselect = IsMultiSelect;
            this.Width = ControlWidth;
            _SelectedItems = new gloItems();
            this.ParentColIndex = ParentColIndex;
            this.ParentRowIndex = ParentRowIndex;
          //  oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
          //  oDB.Connect(false);
        }
        public gloPracticeListControl(gloGridListControlType ControlType, string ControlHeader, bool IsMultiSelect, int ControlWidth, int ParentRowIndex, int ParentColIndex,string AUSID,string sServiceURL)
        {

            #region connstring
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
            #endregion
            InitializeComponent();
            _ControlType = ControlType;
            _ControlHeader = ControlHeader;
            _ismultiselect = IsMultiSelect;
            this.Width = ControlWidth;
            _SelectedItems = new gloItems();
            this.ParentColIndex = ParentColIndex;
            this.ParentRowIndex = ParentRowIndex;
            _sAUSID = AUSID;
            _sServiceURL = sServiceURL;
            //  oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            //  oDB.Connect(false);
        }
         public void Disposer()
        {
            //Disposing connection opened on Constructor
            //if (oDB != null)
            //{
            //    oDB.Disconnect();
            //    oDB.Dispose();
            //}
            //**
            if (_dtList != null)
            {
                _dtList.Dispose();
                _dtList = null;
            }
            Dispose(true);
            GC.SuppressFinalize(this);
        }

         ~gloPracticeListControl()
        {
            //Disposing connection opened on Constructor
            //if (oDB != null)
            //{
            //    oDB.Disconnect();
            //    oDB.Dispose();
            //}
            //**
            if (_dtList != null)
            {
                _dtList.Dispose();
                _dtList = null;
            }
            Dispose(false);
        }
        #endregion

        #region Delegates
      

        public delegate void Key_Down(object sender, EventArgs e);
        public event Key_Down InternalGridKeyDown;

        public delegate void Item_Selected(object sender, EventArgs e, int RowI);
        public event Item_Selected ItemSelected;

        #endregion

        #region " Control Load "
        private void gloPracticeListControl_Load(object sender, EventArgs e)
        {

           
            try
            {
                lblHeader.Text = ControlHeader;
                FillControl(ControlHeader,"");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        #endregion " Control Load "

        #region " Fill Control "
    

        public void FillControl(string sFieldName ,string SearchText)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            QCommService.QCommunicatorServiceClient client = null;
            try
            {
                if (sAUSID != "")
                {
                    WSHttpBinding httpsws = new WSHttpBinding("WSHttpBinding_IQCommunicatorService");
                    httpsws.Security.Mode = SecurityMode.Transport;
                    EndpointAddress ep = new EndpointAddress(ServiceURL);
                    client = new QCommService.QCommunicatorServiceClient(httpsws, ep);
                    string sPracticeData = client.PopulatePracticeData("claimrule", sAUSID, sFieldName, SearchText);


                    SearchText = SearchText.Trim().Replace("'", "''").Replace("[", "").Replace("]", "");

                    DataTable _dtPracticeData = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(sPracticeData);
                    _dtList = _dtPracticeData;
                    if (_dtList != null )
                    {
                        DataView _dv = null;
                        _dv = _dtList.DefaultView;
                        c1GridList.DataSource = _dv;
                        DataRow[] _dr=null;
                        if (_dtList.Rows.Count>0)
                        {
                            _dr = _dtList.Select("Description<>'" + string.Empty + "'"); 
                        }
                       
                        if (_dr!=null&&_dr.Length > 0)
                        {
                            DesignGridListWithDescription();
                            bIsDescriptionColumnVisible = true;
                        }
                        else
                        {
                            DesignGridListWithoutDescription();
                            bIsDescriptionColumnVisible = false;
                        }

                    }
                    else
                    {
                        IsHideControl = true;
                    }

                    try
                    {
                        if (_dtList != null)
                        {
                            IsHideControl = _dtList.Rows.Count <= 0;
                        }

                        else
                        {
                            IsHideControl = false;
                        }

                    }
                    catch (Exception)
                    {
                        IsHideControl = true;
                    }
                }
                else
                {
                    if (_DatabaseConnectionString != "")
                    {
                        // string _sqlQuery = "";                   

                        switch (_ControlType)
                        {

                            case gloGridListControlType.COL_Value:
                                oDB = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                                oDB.Connect(false);

                                oParameters.Clear();
                                oParameters.Add("@sFieldName", sFieldName, ParameterDirection.Input, SqlDbType.NVarChar, 50);
                                oParameters.Add("@SearchString", SearchText, ParameterDirection.Input, SqlDbType.VarChar);
                                if (_dtList != null)
                                {
                                    _dtList.Dispose();
                                    _dtList = null;
                                }
                                oDB.Retrive("ClaimRule_MasterValues", oParameters, out _dtList);
                                oParameters.Dispose();
                                oParameters = null;
                                break;
                        }

                        if (_dtList != null)
                        {
                            DataView _dv = null;
                            _dv = _dtList.DefaultView;
                            c1GridList.DataSource = _dv;

                            DataRow[] _dr = _dtList.Select("Description<>'" + string.Empty + "'");
                            if (_dr.Length > 0)
                            {
                                DesignGridListWithDescription();
                                bIsDescriptionColumnVisible = true;
                            }
                            else
                            {
                                DesignGridListWithoutDescription();
                                bIsDescriptionColumnVisible = false;
                            }

                        }
                        else
                        {
                            IsHideControl = true;
                        }

                        try
                        {
                            if (_dtList != null)
                            {
                                IsHideControl = _dtList.Rows.Count <= 0;
                            }

                            else
                            {
                                IsHideControl = false;
                            }

                        }
                        catch (Exception)
                        {
                            IsHideControl = true;
                        }

                    }
                }
                   

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        #region " Search Functionality "

     

        public void Search(string SearchText)
        {
            int _colIndex = 0;
            try
            {
                SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "");
                DataView _dvTemp = (DataView)c1GridList.DataSource;

                switch (_ControlType)
                {
                   
                    case gloGridListControlType.COL_Value:
                        {
                           // if (SearchCol == SearchColumn.Code)
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
                            if (bIsDescriptionColumnVisible)
                            {
                                DesignGridListWithDescription();
                            }
                            else
                            {
                                DesignGridListWithoutDescription();
                            }
                           
                        }
                        break;
                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (c1GridList.DataSource != null)
                {
                    //this.SearchCount = ((DataView)c1GridList.DataSource).Count;
                }
            }

        }
        public void Search2(string sFieldName,string SearchText)
        {
            int _colIndex = 0;
            try
            {
                SearchText = SearchText.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "");
                DataView _dvTemp = (DataView)c1GridList.DataSource;

                switch (_ControlType)
                {
                   
                    case gloGridListControlType.COL_Value:
                        {
                          
                                _colIndex = 1;
                           

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
                            if (bIsDescriptionColumnVisible)
                            {
                                DesignGridListWithDescription();
                            }
                            else
                            {
                                DesignGridListWithoutDescription();
                            }
                        }
                        break;
                    default:
                        break;
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (c1GridList.DataSource != null)
                {
                    //this.SearchCount = ((DataView)c1GridList.DataSource).Count;
                }
            }

        }

    

        #endregion " Search Functionality "

        #region " Public & Private Methods "
        private void DesignGridListWithoutDescription()
        {
            int _Width = this.Width - 3;
            try
            {
                c1GridList.AllowEditing = false;

                switch (_ControlType)
                {
                    case gloGridListControlType.COL_Value:
                        {
                            if (c1GridList.Cols.Count>0)
                            {
                                c1GridList.Cols[COL_Value].Width = Convert.ToInt32(_Width * 1);
                            c1GridList.Cols[COL_Description].Width = 0;
                            c1GridList.Cols[COL_ID].Width = 0;
                            c1GridList.Cols[COL_Value].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                            }
                            

                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void DesignGridListWithDescription()
        {
            int _Width = this.Width - 3;
            try
            {
                c1GridList.AllowEditing = false;

                switch (_ControlType)
                {
                    case gloGridListControlType.COL_Value:
                        {

                            c1GridList.Cols[COL_Value].Width = Convert.ToInt32(_Width * 0.3);
                            c1GridList.Cols[COL_Description].Width = Convert.ToInt32(_Width * 0.7);                            
                            c1GridList.Cols[COL_ID].Width = 0;
                            c1GridList.Cols[COL_Value].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion " Public & Private Methods "

        #region grid events
        private void c1GridList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
            string _code ="";
            Int64 _nID = 0;


            if (ItemSelected != null && c1GridList != null && c1GridList.Rows.Count > 0)
            {
                _code = Convert.ToString(c1GridList.GetData(c1GridList.RowSel, COL_Value));
                _nID = Convert.ToInt64(c1GridList.GetData(c1GridList.RowSel, COL_ID));
            }
                gloItem oListItem = new gloItem(_nID,_code);
                oListItem.ID = _nID;
                oListItem.Code = _code;
                _SelectedItems.Clear();
                _SelectedItems.Add(oListItem);

                ItemSelected(_code, e, this.ParentRowIndex);
           
            this.Hide();
            this.Parent.Focus();
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }
        public bool GetCurrentSelectedItem()
        {
            string _code = "";
            Int64 _nID = 0;
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
                            case gloGridListControlType.COL_Value:
                                {
                                     _code = Convert.ToString(c1GridList.GetData(rowIndex, 0)); //Provider ID  
                                     _nID = Convert.ToInt64(c1GridList.GetData(c1GridList.RowSel, COL_ID));
                                }
                                break;
                          
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
                EventArgs e = new EventArgs();
                if (ItemSelected != null)
                {
                    gloItem oListItem = new gloItem(_nID, _code);
                    oListItem.ID = _nID;
                    oListItem.Code = _code;
                    _SelectedItems.Add(oListItem);
                    ItemSelected(_code, e, this.ParentRowIndex);
                }
            }

            return _retValue;
        }
      
        #endregion

    }
}
