using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloEmdeonInterface.Classes;
using gloEMRGeneralLibrary.gloEMRLab;
using gloEmdeonInterface.Classes.MergeOrderClasses;
using gloTaskMail;

namespace gloEmdeonInterface.Forms
{
    public partial class frmMergeOrder : Form
    {
       
        //internal gloUserControlLibrary.gloUC_TransactionHistory GloUC_TransactionHistory1;
        public Boolean IsLoading = false;
        private string _dataBaseConnectionString=string.Empty;
        private long _patientID=0;
        private long _orderID;
        private long _TaskId=0;
        private gloTaskMail.TaskType EnumTaskType;
        
        private const Int16 COL_ORDERID = 0;
        private const Int16 COL_ORDERPREFIX = 1;
        private const Int16 COL_ORDERNO = 2;
        private const Int16 COL_TRANSDATE = 4;
        private const Int16 COL_PROVIDERNAME = 7; 
        private const Int16 COL_CUSTOMORDERSTATUS = 5; 
        private const Int16 COL_BILLINGTYPE = 6; 
        private const Int16 COL_ORDER_ISACKNOWLEDGED = 9;
        private const Int16 COL_ORDER_HAS_RESULTS = 8;
        private const Int16 COL_ISORDERLOCKED = 10;
        private const Int16 COL_MACHINENAME = 11;
        private const Int16 COL_REFEREANCEID = 3;
        private const Int16 COL_HasResult_Value = 12;
        private const Int16 COL_HasAkw_Value = 13;
        private const Int16 COL_ORDERSTATUS = 14; 
        private const Int16 COL_COUNT = 15; // 


        private const Int16 COL_Select = 0;
        private const Int16 COL_SourceTestID = 1;
        private const Int16 COL_SourceName = 2;
        private const Int16 COL_DestinationTestID = 3;
        private const Int16 COL_DestinationName = 4;
        private const Int16 COL_MergeCOUNT = 5; 

        private String gstrMessageBoxCaption = string.Empty;

        private clsMergeOrder xMerge = null;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        [DefaultValue(0)]
        public long SourceOrder { get; set; }

        [DefaultValue(0)]
        public long DestinationOrder { get; set; }

        [DefaultValue(false)]
        public Boolean IsMerged { get; private set; }

        enum enumOrders
        {
            enumSource = 0,
            enumDestination = 1,
            enumUnsolicitedTask = 2
        };
               
           
        public frmMergeOrder()
        {
            InitializeComponent();
           
        }

        public frmMergeOrder(string dataBaseConnectionString, long patientID, long orderID )
        {
           
            this._dataBaseConnectionString = dataBaseConnectionString;
            this._patientID = patientID;
            this._orderID = orderID;

            this.xMerge = new clsMergeOrder(this._dataBaseConnectionString);            
            InitializeComponent();
          
        }
       
        public frmMergeOrder(string dataBaseConnectionString, long patientID, long orderID, gloTaskMail.TaskType oTaskType, long TaskID)
        {

            this._dataBaseConnectionString = dataBaseConnectionString;
            this._patientID = patientID;
            this._orderID = orderID;
            this.SourceOrder = orderID;
            this.EnumTaskType = oTaskType;
            this._TaskId = TaskID;

            this.xMerge = new clsMergeOrder(this._dataBaseConnectionString);
            InitializeComponent();
           
        }

        private void frmMergeOrder_Load(object sender, EventArgs e)
        {
            HideUpDownButtonAndPanels();
            RemoveHandlers();
            FillPatientStrip();
            gloEmdeonCommon.gloC1FlexStyle.Style(c1Source, true);
            gloEmdeonCommon.gloC1FlexStyle.Style(c1Destination, true);
            DesignGrid(c1Source);
            DesignGrid(c1Destination);
            IsLoading = true;
            
            if (this.EnumTaskType == gloTaskMail.TaskType.UnsolicitedTask)
            {
                ts_btnDoNotMerge.Visible = true;
            }

            FillOrderGrid();
            AddHandlers();
            gloUC_Source.Dock = DockStyle.Fill;
            gloUC_Source.Visible = true;
            gloUC_Destination.Dock = DockStyle.Fill;
            gloUC_Destination.Visible = true;
            IsLoading = false;

            if (this.xMerge != null)
            { this.xMerge.mergeFired += new clsMergeOrder.MergeFired(xMerge_mergeFired); }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            { gstrMessageBoxCaption = "gloEMR"; }

            #endregion
          
            }
        
        private void AddHandlers()
        {
            c1Source.EnterCell += new EventHandler(c1Source_EnterCell);   
            c1Destination.EnterCell += new EventHandler(c1Destination_EnterCell);   
        }

        private void RemoveHandlers()
        {
            c1Source.EnterCell -= new EventHandler(c1Source_EnterCell);           
            c1Destination.EnterCell -= new EventHandler(c1Destination_EnterCell);   
        }

        private void FillOrderGrid()
        {

            if (this.EnumTaskType == gloTaskMail.TaskType.UnsolicitedTask)
            {
                FillGrid(Convert.ToInt16(enumOrders.enumUnsolicitedTask), c1Source);
                c1Source.EnterCell += new EventHandler(c1Source_EnterCell);
                c1Source.Rows[0].Selected = true;
                c1Source.EnterCell -= new EventHandler(c1Source_EnterCell);
            }

            else
            {

              FillGrid(Convert.ToInt16(enumOrders.enumSource), c1Source);
            }


           FillGrid(Convert.ToInt16(enumOrders.enumDestination),c1Destination);
            

            //if (this.EnumTaskType == gloTaskMail.TaskType.UnsolicitedTask)
            //{
            //    rbSource.Checked = true;
            //    FillGrid(Convert.ToInt16(enumOrders.enumUnsolicitedTask));
            //    c1Source.EnterCell += new EventHandler(c1TestLibrary_EnterCell);
            //    c1Source.Rows[0].Selected = true;
            //    c1Source.EnterCell -= new EventHandler(c1TestLibrary_EnterCell);
            //}
          
        }
        
        private void FillPatientStrip()
        {
            gloUC_PatientStrip1.PatID = _patientID;//140264479223540903; //_patientID
            AddPatientStrip();
        }

        private void HideUpDownButtonAndPanels()
        {
            gloUC_Source.HidePanel(null, null);
            gloUC_Destination.HidePanel(null, null);
        }

        private void gloUC_PatientStrip1_Load(object sender, EventArgs e)
        {

        }

        private void AddPatientStrip()
        {
            gloUC_PatientStrip1.ShowDetail(_patientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.LabOrder);
            gloUC_PatientStrip1.DTPValue = System.DateTime.Now;
            gloUC_PatientStrip1.DTPFormat = DateTimePickerFormat.Short;
            gloUC_PatientStrip1.DTPEnabled = false;       

        }

        private void Fill_Labs(long orderID,string OrderType)
        {
            try
            {
                //if (OrderType == "Source")
                //{
                //    gloUC_Source.Visible = true;
                //    gloUC_Source.Dock = DockStyle.Fill;
                //    gloUC_Source.MergeOrderID = orderID;
                //    gloUC_Source.CurOrderID = 0;
                //    gloUC_Source.SetDataByOrderIDSource(orderID,0);                   

                //    if (DestinationOrder > 0)
                //    {
                //        gloUC_Destination.Visible = true;
                //        gloUC_Destination.Dock = DockStyle.Fill;
                //        gloUC_Destination.MergeOrderID = DestinationOrder;
                //        gloUC_Source.CurOrderID = SourceOrder;
                //        gloUC_Destination.SetDataByOrderIDSource(DestinationOrder, SourceOrder);
                //    }

                //}
                //else if (OrderType == "Destination")
                //    {
                //        gloUC_Destination.Visible = true;
                //        gloUC_Destination.Dock = DockStyle.Fill;
                //        gloUC_Destination.MergeOrderID = orderID;
                //        gloUC_Source.CurOrderID = SourceOrder;
                //        gloUC_Destination.SetDataByOrderIDSource(orderID, SourceOrder);
                //    }   


                if (OrderType == "Destination")
                {
                    gloUC_Destination.Visible = true;
                    gloUC_Destination.Dock = DockStyle.Fill;
                    gloUC_Destination.MergeOrderID = orderID;
                    gloUC_Destination.CurOrderID = 0;
                    gloUC_Destination.ForMerging = true;
                    gloUC_Destination.SetDataByOrderIDSource(orderID, 0);


                    if (DestinationOrder > 0)
                    {
                        gloUC_Source.Visible = true;
                        gloUC_Source.Dock = DockStyle.Fill;
                        gloUC_Source.MergeOrderID = SourceOrder;
                        gloUC_Source.CurOrderID = DestinationOrder;
                        gloUC_Source.ForMerging = true;
                        gloUC_Source.SetDataByOrderIDSource(SourceOrder, DestinationOrder);
                    }

                }

                else if (OrderType == "Source")
                {

                    gloUC_Source.Visible = true;
                    gloUC_Source.Dock = DockStyle.Fill;
                    gloUC_Source.MergeOrderID = orderID;
                    gloUC_Source.CurOrderID = DestinationOrder;
                    gloUC_Source.ForMerging = true;
                    gloUC_Source.SetDataByOrderIDSource(orderID, DestinationOrder);

                } 


            }

            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }

        }
       
        private string GetOrderType(long _OrderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_dataBaseConnectionString);
            string _result = string.Empty;
            string strQuery = string.Empty;
            DataTable _dtResult = null;
            string _labom_dgloLabOrderID = string.Empty;
            string _labom_ExternalCode = string.Empty;

            try
            {
                _dtResult = new DataTable();
                strQuery = "select isnull(labom_ExternalCode,'') as labom_ExternalCode,labom_dgloLabOrderID from lab_order_Mst where labom_OrderID=" + _OrderID;

                oDB.Connect(false);

                oDB.Retrive_Query(strQuery, out _dtResult);

                if (_dtResult != null && _dtResult.Rows.Count > 0)
                {

                    string _LabOrdExternalCode = string.Empty;

                    if (Convert.ToString(_dtResult.Rows[0]["labom_ExternalCode"]).Trim() != ""
                       && _dtResult.Rows[0]["labom_dgloLabOrderID"] != DBNull.Value)
                    {
                        _result = "Emdeon";
                    }
                    else
                    {
                        _result = "Emr";
                    }
                }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _result = string.Empty;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (_dtResult != null)
                {
                    _dtResult.Dispose();
                    _dtResult = null;
                }
                _labom_dgloLabOrderID = string.Empty;
                _labom_ExternalCode = string.Empty;
            }
            return _result;
        }

        private void DesignGrid(C1.Win.C1FlexGrid.C1FlexGrid c1Test)
        {
            try
            {
                //c1Test.Clear();
                c1Test.DataSource = null;
                c1Test.Clear();

                // setfont
                c1Test.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                c1Test.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                c1Test.BackColor = Color.White;
                c1Test.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;

                c1Test.Cols.Count = COL_COUNT;
                c1Test.Cols.Fixed = 1;
                c1Test.Rows.Count = 1;
                c1Test.Rows.Fixed = 1;

                // set visibility of column
                c1Test.Cols[COL_ORDERID].Visible = false;
                c1Test.Cols[COL_ORDERPREFIX].Visible = true;
                c1Test.Cols[COL_ORDERNO].Visible = false;
                c1Test.Cols[COL_TRANSDATE].Visible = true;
                c1Test.Cols[COL_PROVIDERNAME].Visible = true;

                c1Test.Cols[COL_CUSTOMORDERSTATUS].Visible = true; 
                c1Test.Cols[COL_ORDERSTATUS].Visible = true; 
                c1Test.Cols[COL_BILLINGTYPE].Visible = true; 
                c1Test.Cols[COL_ISORDERLOCKED].Visible = false; 
                c1Test.Cols[COL_MACHINENAME].Visible = false;
                c1Test.Cols[COL_REFEREANCEID].Visible = true; 
                c1Test.Cols[COL_HasAkw_Value].Visible = false;
                c1Test.Cols[COL_HasResult_Value].Visible = false;

                c1Test.Cols[COL_ORDERNO].Width = 0;
                c1Test.Cols[COL_TRANSDATE].Width = 200;
                c1Test.Cols[COL_PROVIDERNAME].Width = 200;

                c1Test.Cols[COL_CUSTOMORDERSTATUS].Width = 150;
                c1Test.Cols[COL_ORDERSTATUS].Width = 0;
                c1Test.Cols[COL_ORDER_HAS_RESULTS].Width = 0;
                c1Test.Cols[COL_ORDER_ISACKNOWLEDGED].Width = 0;
                c1Test.Cols[COL_ISORDERLOCKED].Width = 0; 
                c1Test.Cols[COL_REFEREANCEID].Width = 0;
                c1Test.Cols[COL_BILLINGTYPE].Width = 0;


              
                
              

              
               
              




                // set column editing
                c1Test.Cols[COL_ORDERPREFIX].AllowEditing = false;
                c1Test.Cols[COL_ORDERNO].AllowEditing = false;
                c1Test.Cols[COL_TRANSDATE].AllowEditing = false;
                c1Test.Cols[COL_PROVIDERNAME].AllowEditing = false;

                c1Test.Cols[COL_CUSTOMORDERSTATUS].AllowEditing = false; 
                c1Test.Cols[COL_ORDERSTATUS].AllowEditing = false; 
                c1Test.Cols[COL_BILLINGTYPE].AllowEditing = false; 
                c1Test.Cols[COL_ORDER_ISACKNOWLEDGED].AllowEditing = false; 
                c1Test.Cols[COL_ORDER_HAS_RESULTS].AllowEditing = false;
                c1Test.Cols[COL_ISORDERLOCKED].AllowEditing = false;
                c1Test.Cols[COL_REFEREANCEID].AllowEditing = false;  

                //set Heading
                c1Test.SetData(0, COL_ORDERNO, "Order ID");
                c1Test.SetData(0, COL_ORDERPREFIX, "Order #");
                //c1Test.SetData(0, COL_REFEREANCEID, "Reference #"); 
                //c1TestLibrary.SetData(0, COL_ORDERNO, "Order NO");
                c1Test.SetData(0, COL_TRANSDATE, "Order Date");
                c1Test.SetData(0, COL_PROVIDERNAME, "Provider Name");

                c1Test.SetData(0, COL_CUSTOMORDERSTATUS, "Order Status"); 
               // c1Test.SetData(0, COL_ORDERSTATUS, "Electronic Order Status");
              //  c1Test.SetData(0, COL_BILLINGTYPE, "Billing Type");
              //  c1Test.SetData(0, COL_ORDER_HAS_RESULTS, "Results");
              //  c1Test.SetData(0, COL_ORDER_ISACKNOWLEDGED, "Acknowledged");
                //added by madan on 20100619

                //c1Test.Cols[COL_ISORDERLOCKED].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.RightCenter;
                //c1Test.Cols[COL_ISORDERLOCKED].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                //c1Test.SetData(0, COL_ISORDERLOCKED, "Locked");

                ////Added by madan on 20100831
                //c1Test.Cols[COL_REFEREANCEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //End Changes

                c1Test.ExtendLastCol = false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void DesignGrid( )
        {

            try
            {
                //c1Source.Clear();
                c1Source.DataSource = null;
                c1Source.Clear();

                // setfont
                c1Source.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                c1Source.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                c1Source.BackColor = Color.White;
                c1Source.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;

                c1Source.Cols.Count = COL_COUNT;
                c1Source.Cols.Fixed = 1;
                c1Source.Rows.Count = 1;
                c1Source.Rows.Fixed = 1;

                // set visibility of column
                c1Source.Cols[COL_ORDERID].Visible = false;
                c1Source.Cols[COL_ORDERPREFIX].Visible = true;
                c1Source.Cols[COL_ORDERNO].Visible = false;
                c1Source.Cols[COL_TRANSDATE].Visible = true;
                c1Source.Cols[COL_PROVIDERNAME].Visible = true;

                c1Source.Cols[COL_CUSTOMORDERSTATUS].Visible = true; //29-May-13 Aniket: Orders PRD: 
                c1Source.Cols[COL_ORDERSTATUS].Visible = true; // by Abhijeet on date 20100330
                c1Source.Cols[COL_BILLINGTYPE].Visible = true; // by Abhijeet on date 20100330
                c1Source.Cols[COL_ISORDERLOCKED].Visible = false; //Added by madan on 20100619
                c1Source.Cols[COL_MACHINENAME].Visible = false;//Added by madan on 20100619
                c1Source.Cols[COL_REFEREANCEID].Visible = true; //Added by madan on 20100831
                c1Source.Cols[COL_HasAkw_Value].Visible = false;
                c1Source.Cols[COL_HasResult_Value].Visible = false;

                c1Source.Cols[COL_TRANSDATE].Width = 200;
                c1Source.Cols[COL_PROVIDERNAME].Width = 200;// by Abhijeet on date 20100330

                c1Source.Cols[COL_CUSTOMORDERSTATUS].Width = 0; //29-May-13 Aniket: Orders PRD: 
                c1Source.Cols[COL_ORDERSTATUS].Width = 150; // by Abhijeet on date 20100330
                c1Source.Cols[COL_ORDER_HAS_RESULTS].Width =0;//Added by madan on 20100512
                c1Source.Cols[COL_ORDER_ISACKNOWLEDGED].Width = 0;//Added by madan on 20100512
                c1Source.Cols[COL_ISORDERLOCKED].Width = 0; //added by madan on 20100619
                c1Source.Cols[COL_REFEREANCEID].Width = 100; //Added by madan on 20100831

                // set column editing
                c1Source.Cols[COL_ORDERPREFIX].AllowEditing = false;
                c1Source.Cols[COL_ORDERNO].AllowEditing = false;
                c1Source.Cols[COL_TRANSDATE].AllowEditing = false;
                c1Source.Cols[COL_PROVIDERNAME].AllowEditing = false;

                c1Source.Cols[COL_CUSTOMORDERSTATUS].AllowEditing = false; //29-May-13 Aniket: Orders PRD: 
                c1Source.Cols[COL_ORDERSTATUS].AllowEditing = false; // by Abhijeet on date 20100330
                c1Source.Cols[COL_BILLINGTYPE].AllowEditing = false; // by Abhijeet on date 20100330
                c1Source.Cols[COL_ORDER_ISACKNOWLEDGED].AllowEditing = false;//Added by madan on 20100512
                c1Source.Cols[COL_ORDER_HAS_RESULTS].AllowEditing = false;//Added by madan on 20100512
                c1Source.Cols[COL_ISORDERLOCKED].AllowEditing = false; //added by madan on 2010618
                c1Source.Cols[COL_REFEREANCEID].AllowEditing = false;  //Added by madan on 20100831

                //set Heading
                c1Source.SetData(0, COL_ORDERNO, "Order ID");
                c1Source.SetData(0, COL_ORDERPREFIX, "Order #");
                c1Source.SetData(0, COL_REFEREANCEID, "Reference #");//Added by madan on 20100831
                //c1TestLibrary.SetData(0, COL_ORDERNO, "Order NO");
                c1Source.SetData(0, COL_TRANSDATE, "Order Date");
                c1Source.SetData(0, COL_PROVIDERNAME, "Provider Name");

                c1Source.SetData(0, COL_CUSTOMORDERSTATUS, "Order Status"); //29-May-13 Aniket: Orders PRD: 
                //c1Source.SetData(0, COL_ORDERSTATUS, "Electronic Order Status");// by Abhijeet on date 20100330
                //c1Source.SetData(0, COL_BILLINGTYPE, "Billing Type");// by Abhijeet on date 20100330
                //c1Source.SetData(0, COL_ORDER_HAS_RESULTS, "Results");
                //c1Source.SetData(0, COL_ORDER_ISACKNOWLEDGED, "Acknowledged");
                ////added by madan on 20100619

                //c1Source.Cols[COL_ISORDERLOCKED].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.RightCenter;
                //c1Source.Cols[COL_ISORDERLOCKED].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                //c1Source.SetData(0, COL_ISORDERLOCKED, "Locked");

                ////Added by madan on 20100831
                //c1Source.Cols[COL_REFEREANCEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //End Changes

                c1Source.ExtendLastCol = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }              

        private void FillGrid(int OrderType,C1.Win.C1FlexGrid.C1FlexGrid c1test)
        {
            DataTable dt = null;
            Int32 rowsCountToDisplay = 0;
            Int32 iRow1 = 0;
          //  Int32 ordRowNo = 0;
           

            if (IsLoading == false)
            {
               if (OrderType==Convert.ToInt16(enumOrders.enumSource))
               {
                  c1Source.EnterCell -= new EventHandler(c1Source_EnterCell);
               }

               if (OrderType==Convert.ToInt16(enumOrders.enumDestination))
               {
                  c1Destination.EnterCell -= new EventHandler(c1Destination_EnterCell);
               }               
            }

            c1test.BeginUpdate();

            try
            {
                dt = new DataTable();
               
                if (OrderType == Convert.ToInt16(enumOrders.enumSource))
                {
                    DesignGrid(c1Source);
                }

                if (OrderType == Convert.ToInt16(enumOrders.enumUnsolicitedTask))
                {
                    DesignGrid(c1Source);
                }


                if (OrderType == Convert.ToInt16(enumOrders.enumDestination))
                {
                    DesignGrid(c1Destination);
                }     

                dt = GetOrder_New(_patientID, OrderType, _orderID);

                iRow1 = 0;
                if (dt.Rows.Count < 100)
                    rowsCountToDisplay = dt.Rows.Count;
                else
                    rowsCountToDisplay = 100;

                if (dt.Rows.Count > 0)
                {



                    for (int iRow = iRow1; iRow <= dt.Rows.Count - 1; iRow++)
                    {

                        c1test.Rows.Add();
                        Int32 _Row = c1test.Rows.Count - 1;

                        c1test.SetData(_Row, COL_ORDERID, dt.Rows[iRow]["labom_OrderID"].ToString());
                        c1test.SetData(_Row, COL_ORDERPREFIX, dt.Rows[iRow]["PrfixID"].ToString());
                        c1test.SetData(_Row, COL_ORDERNO, dt.Rows[iRow]["labom_OrderNoID"].ToString());
                        c1test.SetData(_Row, COL_TRANSDATE, dt.Rows[iRow]["labom_OrderDate"].ToString());


                        c1test.SetData(_Row, COL_PROVIDERNAME, dt.Rows[iRow]["ProviderName"].ToString());


                        string _strOrderStatus = Convert.ToString(dt.Rows[iRow]["labom_gloLabOrderStatus1"]);
                        string _strBillingType = Convert.ToString(dt.Rows[iRow]["labom_gloLabOrderBillingType1"]);

                        c1test.SetData(_Row, COL_CUSTOMORDERSTATUS, dt.Rows[iRow]["OrderStatus"]); //29-May-13 Aniket: Orders PRD: Order Status Column added

                        // c1test.SetData(_Row, COL_ORDERSTATUS, _strOrderStatus);
                        // c1test.SetData(_Row, COL_BILLINGTYPE, _strBillingType);

                        //Madan added on 20100512
                        #region ContainsAcknowledgement

                        Int64 _nOrderID = 0;
                        string _OrderNumberPreFix = string.Empty;

                        //added by madan on 20100619
                        string _MachineName = string.Empty;
                        //end madan

                        _nOrderID = Convert.ToInt64(dt.Rows[iRow]["labom_OrderID"].ToString());
                        _MachineName = Convert.ToString(dt.Rows[iRow]["labom_MachineName"].ToString());

                        if (Convert.ToInt64(dt.Rows[iRow]["IsAkw"]) > 0 && Convert.ToInt64(dt.Rows[iRow]["bIsClosed"]) > 0)
                        {
                            c1test.SetData(_Row, COL_HasAkw_Value, Convert.ToInt64(dt.Rows[iRow]["IsAkw"]));
                        }
                        else
                        {
                            c1test.SetData(_Row, COL_HasAkw_Value, 0);
                        }

                        if (Convert.ToInt64(dt.Rows[iRow]["IsAkw"]) > 0 && Convert.ToInt64(dt.Rows[iRow]["bIsClosed"]) > 0)//CheckAcknoledgement(_nOrderID, _OrderNumberPreFix, _nOrderNumberID))
                        {
                            c1test.SetCellImage(_Row, COL_ORDER_ISACKNOWLEDGED, gloEmdeonInterface.Properties.Resources.Yes);
                        }
                        else
                        {
                            c1test.SetCellImage(_Row, COL_ORDER_ISACKNOWLEDGED, gloEmdeonInterface.Properties.Resources.FlagNone);
                        }

                        if (_nOrderID != 0)
                        {
                            c1test.SetData(_Row, COL_HasResult_Value, Convert.ToInt64(dt.Rows[iRow]["IsResult"]));
                            if (Convert.ToInt64(dt.Rows[iRow]["IsResult"]) > 0)
                            {
                                c1test.SetCellImage(_Row, COL_ORDER_HAS_RESULTS, gloEmdeonInterface.Properties.Resources.FlagAcknowledge1);
                            }
                            else
                            {
                                c1test.SetCellImage(_Row, COL_ORDER_HAS_RESULTS, gloEmdeonInterface.Properties.Resources.FlagNone);
                            }
                        }
                        if (ConfirmNull(_MachineName.ToString()))
                        {
                            c1test.SetData(_Row, COL_MACHINENAME, _MachineName.ToString());
                            c1test.SetCellImage(_Row, COL_ISORDERLOCKED, gloEmdeonInterface.Properties.Resources.Lock);
                        }

                        _nOrderID = 0;
                        _MachineName = string.Empty;

                        #endregion

                        if (ConfirmNull(_strOrderStatus) && ConfirmNull(_strBillingType))
                        {

                            string _LabExternalCode = string.Empty;

                            _LabExternalCode = Convert.ToString(dt.Rows[iRow]["labom_ExternalCode"].ToString());

                            if (ConfirmNull(_LabExternalCode))
                            {
                                // c1test.SetData(_Row, COL_REFEREANCEID, _LabExternalCode.ToString());
                            }

                            _LabExternalCode = string.Empty;

                        }

                        _strOrderStatus = string.Empty;
                        _strBillingType = string.Empty;

                    }

                    string _OrderSelectID = string.Empty;



                    if (OrderType == Convert.ToInt16(enumOrders.enumSource))
                    {
                        if (SourceOrder > 0)
                        {
                            _OrderSelectID = SourceOrder.ToString();
                            int _TestID = c1Source.FindRow(_OrderSelectID, 1, COL_ORDERID, false, true, true);
                            c1Source.Select(_TestID, 0, true);
                        }

                        if (SourceOrder == 0)
                        {
                            _OrderSelectID = "1";
                            int _TestID = c1Source.FindRow(_OrderSelectID, 1, COL_ORDERID, false, true, true);
                            c1Source.Select(_TestID, 0, false);
                        }

                    }

                    if (OrderType == Convert.ToInt16(enumOrders.enumDestination))
                    {
                        if (DestinationOrder > 0)
                        {
                            _OrderSelectID = DestinationOrder.ToString();
                            int _TestID = c1Destination.FindRow(_OrderSelectID, 1, COL_ORDERID, false, true, true);
                            c1Destination.Select(_TestID, 0, true);
                        }

                        if (DestinationOrder == 0)
                        {
                            _OrderSelectID = "1";
                            int _TestID = c1Destination.FindRow(_OrderSelectID, 1, COL_ORDERID, false, true, true);
                            c1Destination.Select(_TestID, 0, false);
                        }

                    }



                    c1test.EndUpdate();

                    if (IsLoading == false)
                    {
                        if (OrderType == Convert.ToInt16(enumOrders.enumSource))
                        {
                            c1Source.EnterCell += new EventHandler(c1Source_EnterCell);
                        }

                        if (OrderType == Convert.ToInt16(enumOrders.enumDestination))
                        {
                            c1Destination.EnterCell += new EventHandler(c1Destination_EnterCell);
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }         

            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }

            }
            
        }

        protected bool ConfirmNull(string strValue)
        {
            bool blnCheck = false;
            try
            {
                if (strValue != null && strValue.ToString().Trim().Length != 0 && strValue.ToString() != "")
                {
                    blnCheck = true;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return blnCheck;
        }

        private DataTable GetOrder_New(long PatientID,int orderType,long orderID)
        {
            DataTable dt = new DataTable();
            gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder obj = new gloEMRLabOrder();
            try
            {
                if (orderType == enumOrders.enumUnsolicitedTask.GetHashCode())
                {
                    dt = obj.GetSelectedOrder(orderID);
                }
                else
                {
                    dt = obj.GetOrder_Merge(PatientID, orderType);
                }
                return dt;
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return null;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (obj != null)
                {
                    obj.Dispose();
                    obj = null;
                }
            }
        }
        
        private DataTable GetSelectedOrderDetails(Int64 orderID)
        {
            DataTable dt = new DataTable();
            gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder obj = new gloEMRLabOrder();
            try
            {
                dt = obj.GetSelectedOrder(orderID);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (obj != null)
                {
                    obj.Dispose();
                    obj = null;
                }
            }
        }

        //private void LoadOrders()
        //{
        //    // Added by Ashish for Integration	

        //    if (xMerge != null)
        //    {
        //        xMerge.FillOrdersFromDB(Convert.ToUInt64(SourceOrder), Convert.ToUInt64(DestinationOrder));

        //        //xMerge.LoadSourceOrder(Convert.ToUInt64(SourceOrder));
        //        //xMerge.LoadTargetOrder(Convert.ToUInt64(DestinationOrder));
        //    }		                        
        //}

        private void ts_btnSave_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (xMerge != null)
            //    {                    
            //        if (CheckReplacingConstraints())
            //        {
            //            xMerge.FillOrdersFromDB(Convert.ToUInt64(SourceOrder), Convert.ToUInt64(DestinationOrder));
            //            //LoadOrders();

            //            //if (!this.xMerge.TargetOrder.IsMerged)
            //            //{
            //                DataTable dtt = gloUC_Destination.GetDataTableFromControl();
            //                this.BuildReplacingTests(dtt);

            //                dtt.Clear();
            //                dtt.Dispose();
            //                dtt = null;
            //            //}                        
            //            xMerge.ExecuteMerge();
            //            this.Close();

            //            //if (MessageBox.Show("Do you want to merge any more Orders?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            //            //{ this.Close(); }         
            //        }
            //    }
                       
               
            //}
            //catch (gloEmdeonInterface.Classes.MergeOrderClasses.TemplateException templateException)
            //{ MessageBox.Show(Convert.ToString(templateException.Message), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            //catch (Exception ex) { MessageBox.Show(Convert.ToString(ex.Message), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
       
        private void c1Source_EnterCell(object sender, EventArgs e)
        {
            long curOrderID = 0;
            if (c1Source.RowSel > 0)
            {
                curOrderID = Convert.ToInt64(c1Source.GetData(c1Source.RowSel, COL_ORDERID));
                if (curOrderID > 0 )
                {
                    lblSourceOrderNo.Text = "ORD " + Convert.ToString(c1Source.GetData(c1Source.RowSel, COL_ORDERNO));
                    SourceOrder = curOrderID;
                    Fill_Labs(curOrderID, "Source");
                    
					// Added by Ashish for Integration
                    //if (xMerge != null)
                    //{ xMerge.LoadSourceOrder(Convert.ToUInt64(SourceOrder)); }
                }                
            }

        }

        private void c1Destination_EnterCell(object sender, EventArgs e)
        {
            long curOrderID = 0;
            if (c1Destination.RowSel > 0)
            {
                curOrderID = Convert.ToInt64(c1Destination.GetData(c1Destination.RowSel, COL_ORDERID));    
                if (curOrderID > 0)
                    {
                        lblDestinationOrderNo.Text = "ORD " + Convert.ToString(c1Destination.GetData(c1Destination.RowSel, COL_ORDERNO));
                        DestinationOrder = curOrderID;
                        Fill_Labs(curOrderID, "Destination");
					    
                        //// Added by Ashish for Integration						
                        //if (xMerge != null)
                        //{ xMerge.LoadTargetOrder(Convert.ToUInt64(DestinationOrder)); }
                    }
           } 
        }
        
        //private void FillComboBox(string order)
        //{
            

        //    if (order == "Source")
        //    {               
        //        if (SourceOrder > 0)
        //        {
        //            cmbSource.DataSource = null;
        //            cmbSource.Items.Clear();
        //            List<gloEmdeonInterface.Classes.MergeOrderClasses.clsGloTest> test = new List<gloEmdeonInterface.Classes.MergeOrderClasses.clsGloTest>();
        //            test.Add(new gloEmdeonInterface.Classes.MergeOrderClasses.clsGloTest("--Select--", 0, 0, 0, string.Empty));
        //            test.AddRange(xMerge.GetTestsByOrderID(SourceOrder).Values.ToList());


        //            cmbSource.DataSource = test;
        //            cmbSource.DisplayMember = "TestName";
        //            cmbSource.ValueMember = "TestID"; 
        //        }                
        //    }

        //    if (order == "Destination")
        //    {
        //        if (DestinationOrder > 0)
        //        {
        //            cmbDestination.DataSource = null;
        //            cmbDestination.Items.Clear();
        //            List<gloEmdeonInterface.Classes.MergeOrderClasses.clsGloTest> test = new List<gloEmdeonInterface.Classes.MergeOrderClasses.clsGloTest>();
        //            test.Add(new gloEmdeonInterface.Classes.MergeOrderClasses.clsGloTest("--Select--", 0, 0, 0, string.Empty));
        //            test.Add(new gloEmdeonInterface.Classes.MergeOrderClasses.clsGloTest("New", 1, 1, 1, string.Empty));

        //            test.AddRange(xMerge.GetTestsByOrderID(DestinationOrder).Values.ToList());
                    

        //            cmbDestination.DataSource = test;
        //            cmbDestination.DisplayMember = "TestName";
        //            cmbDestination.ValueMember = "TestID";                   
        //        }
              
        //    }
        //}

        

        //private void btnReplace_Click(object sender, EventArgs e)
        //{            
        //    if (xMerge != null)
        //    {
        //        int SourceTestHashCode = 0;// Convert.ToInt32(lstSource.SelectedValue);
        //        int TargetTestHashCode = 0;// Convert.ToInt32(lstTarget.SelectedValue);
                
        //        try { xMerge.ReplaceTargetTest(SourceTestHashCode, TargetTestHashCode); }
        //        catch (ReplacementTestException ex) { MessageBox.Show(Convert.ToString(ex.Message)); }
        //        catch (Exception ex) { MessageBox.Show(Convert.ToString(ex.Message)); }
        //    }
        //}               
              
        private void frmMergeOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.xMerge != null)
                {
                    this.xMerge.mergeFired -= xMerge_mergeFired;
                    this.xMerge.Dispose();
                    this.xMerge = null;
                }
            }
            catch (Exception ex) { MessageBox.Show(Convert.ToString(ex.Message), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }

          
        }

        #region "Complete Unsolicited Task"

        void xMerge_mergeFired(object sender)
        {
            try
            {
                if (this._TaskId != 0)
                {
                    using (gloTask completeTask = new gloTask(this._dataBaseConnectionString))
                    { completeTask.ModifyTaskComplete(this._TaskId, 100); }
                }
                this.IsMerged = true;
                MergeOrderFired(sender);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Convert.ToString(ex), false);
                MessageBox.Show(Convert.ToString(ex.Message), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #endregion
        
        #region "Replace Tests"

        private void BuildReplacingTests(DataTable DataTable)
        {


            IEnumerable<clsReplacingObject> enumAllReplacingTests = from DataRow dRow in DataTable.AsEnumerable()
                                                                    where Convert.ToInt64(dRow["Manual"]) != 1 && Convert.ToInt64(dRow["Manual"]) != 0
                                                                    select new clsReplacingObject(dRow);

            IEnumerable<clsReplacingObject> enumReplacingTests = enumAllReplacingTests.Where(p => p.SourceTestID != p.TargetTestID);
            
            foreach (clsReplacingObject Replacement in enumReplacingTests)
            {
                try { xMerge.ReplaceTargetTest(Replacement.SourceTestID, Replacement.TargetTestID); }
                catch (ReplacementTestException ex) { MessageBox.Show(Convert.ToString(ex.Message), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                catch (Exception ex) { MessageBox.Show(Convert.ToString(ex.Message), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }

            enumReplacingTests = null;
            enumAllReplacingTests = null;

           
        }

        private Boolean CheckReplacingConstraints()
        {
          

            DataTable dtt = gloUC_Source.GetDataTableFromControl();
            List<UInt64> lstReplacingObjectHash = new List<UInt64>();

            if (dtt.AsEnumerable().Any(p => Convert.ToInt64(p["Manual"]) == 0))
            {
                StringBuilder message = new StringBuilder();
                message.AppendLine("Please complete Step 3.");
                message.AppendLine();
                message.AppendLine("Each Test from the ‘Order to Merge’ must have a selection from the ‘Merge Into’ list.");


                string sMessage = message.ToString();
                message.Clear();
                message = null;

                MessageBox.Show(sMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            IEnumerable<clsReplacingObject> enumAllReplacingTests = dtt.AsEnumerable().Where(dRow => Convert.ToInt64(dRow["Manual"]) != 1 && Convert.ToInt64(dRow["Manual"]) != 0).Select(p => new clsReplacingObject(p));

            foreach (clsReplacingObject ReplacingObject in enumAllReplacingTests)
            {
                if (lstReplacingObjectHash.Any(p => p == ReplacingObject.TargetTestID))
                {
                    StringBuilder message = new StringBuilder();
                    message.AppendLine("Merge Problem:");
                    message.AppendLine();
                    message.AppendLine("Test cannot be selected multiple times.");
                    message.AppendLine("Review the selections for Step 3.");


                    string sMessage = message.ToString();
                    message.Clear();
                    message = null;

                    MessageBox.Show(sMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else { lstReplacingObjectHash.Add(ReplacingObject.TargetTestID); }
            }

            lstReplacingObjectHash.Clear();
            lstReplacingObjectHash = null;

            if (dtt != null)
            {
                dtt.Dispose();
                dtt = null;
            }

            return true;
          
        }

        #endregion

        #region "Button Click Events"

        private void ts_btnPreviewMerge_Click(object sender, EventArgs e)
        {
            try
            {

               if (DestinationOrder == 0)
                {
                    StringBuilder message = new StringBuilder();
                    message.AppendLine("Please complete Step 1: ");
                    message.AppendLine();
                    message.AppendLine("Select an 'Order to Keep'");

                    MessageBox.Show(message.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    message.Clear();
                    message = null;

                    return;
                }
               else if (SourceOrder == 0)
               {
                   StringBuilder message = new StringBuilder();
                   message.AppendLine("Please complete Step 2: ");
                   message.AppendLine();
                   message.AppendLine("Select an 'Order to Merge'");

                   MessageBox.Show(message.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                   message.Clear();
                   message = null;

                   return;
               }
                else if (SourceOrder == DestinationOrder)
                {
                    MessageBox.Show("'Order to Keep' and 'Order to Merge' cannot be same.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (CheckReplacingConstraints())
                {

                    if (this.xMerge != null)
                    {
                        xMerge.FillOrdersFromDB(Convert.ToUInt64(SourceOrder), Convert.ToUInt64(DestinationOrder));
                        if (xMerge.SourceOrder != null && xMerge.TargetOrder != null)
                        {

                            DataTable dtt = gloUC_Source.GetDataTableFromControl();
                            this.xMerge.ResetReplacingTestObject();

                            this.BuildReplacingTests(dtt);

                            dtt.Clear();
                            dtt.Dispose();
                            dtt = null;

                            DataSet BindingDataSet = xMerge.BuildPreviewDataSet();

                            Boolean IsMerged = false;
                            frmMergeOrderPreview frmPreview = new frmMergeOrderPreview();

                            frmPreview.MergeOrder = this.xMerge;
                            frmPreview.BindingDataSet = BindingDataSet;
                            frmPreview.ShowDialog(this);

                            IsMerged = frmPreview.IsMerging;

                            frmPreview.Close();
                            frmPreview.Dispose();
                            frmPreview = null;

                            if (IsMerged) { gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.Reconciliation, gloAuditTrail.ActivityType.Reconcile, "ORD " + xMerge.SourceOrder.OrderNoID + " Merge into ORD " + xMerge.TargetOrder.OrderNoID, _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success); this.Close(); }
                        }
                    }
                }
            }
            catch (TemplateException ex) { MessageBox.Show(Convert.ToString(ex.Message), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
            catch (Exception ex) { MessageBox.Show(Convert.ToString(ex.Message), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void ts_btnDoNotMerge_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to complete the Task without Merging the Order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                xMerge_mergeFired(sender);
                this.Close();
            }

            
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }       

        #endregion

        #region "Events"
        public delegate void MergeOrderOperationFired(object sender);
        public event MergeOrderOperationFired mergeOperationFired;

        protected virtual void MergeOrderFired(object sender)
        { if (mergeOperationFired != null) { mergeOperationFired(sender); } }
        #endregion
    }
}
