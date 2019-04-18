using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace gloEmdeonInterface.Forms
{
    public partial class frmViewLabs : Form
    {
        private const Int16 COL_ORDERID = 0;
        private const Int16 COL_ORDERPREFIX = 1;
        private const Int16 COL_ORDERNO = 2;
        private const Int16 COL_TRANSDATE = 3;
        private const Int16 COL_COUNT = 4;


        private string _dataBaseConnectionString = "";
        private long _patientID = 0;
        private long _LabProviderID = 0;
        private LabRequestOrderParameter _OrderParamter = new LabRequestOrderParameter();

        public LabRequestOrderParameter LabOrderParameter
        {
            get
            {
                return _OrderParamter;
            }
            set
            {
                _OrderParamter = value;
            }
        } 
       //commented by madan on 20100520 
       //System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        //public frmViewLabs(long patientID)
        //{         
        //    InitializeComponent();
        //}

        //public frmViewLabs(long patientID )
        //{
        //    _patientID = patientID;

        //     if (appSettings != null)
        //     {
        //        _dataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
        //     }
        //    InitializeComponent();            
        //}


        public frmViewLabs(long patientID)
        {
            _patientID = patientID;
            if (appSettings != null)
            {
                _dataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            }
            InitializeComponent();       

        }
           private void fillPreviousOrdsGrid()
           {

               DesignGrid();
               
               DataTable dtOrders = new DataTable();
               
               // Getting Orders for Patient
               dtOrders = GetOrders(_patientID);


               for (int iRow = 0; iRow <= dtOrders.Rows.Count-1; iRow++)
               {
                   c1TestLibrary.Rows.Add();
                   Int32 _Row = c1TestLibrary.Rows.Count - 1;

                   c1TestLibrary.SetData(_Row, COL_ORDERID, dtOrders.Rows[iRow]["labom_OrderID"].ToString());  
                   c1TestLibrary.SetData(_Row, COL_ORDERPREFIX, dtOrders.Rows[iRow]["labom_OrderNoPrefix"].ToString());
                   c1TestLibrary.SetData(_Row, COL_ORDERNO, dtOrders.Rows[iRow]["labom_OrderNoID"].ToString());
                   c1TestLibrary.SetData(_Row, COL_TRANSDATE, dtOrders.Rows[iRow]["labom_TransactionDate"].ToString());
               }              

           }

           private DataTable GetOrders(long PatientId) 
            {             
                //DataTable dt ;
                SqlConnection con = new SqlConnection(_dataBaseConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter Da = new SqlDataAdapter();
                DataSet Ds = new DataSet();
                
                try
                {
                    //first get all the orders aginst that PatientID                  
                   // dt = new DataTable();

                    string strSql = "SELECT DISTINCT labom_OrderID, Lab_Order_MST.labom_OrderNoPrefix," +
                                     "Lab_Order_MST.labom_OrderNoID, Lab_Order_MST.labom_TransactionDate ," +
                                     "ISNULL(Lab_Order_MST.labom_VisitID,0) AS labom_VisitID FROM Lab_Order_MST " +
                                     " LEFT OUTER JOIN Lab_Order_Test_Result ON Lab_Order_MST.labom_OrderID = " +
                                     "Lab_Order_Test_Result.labotr_OrderID LEFT OUTER JOIN " +
                                     "Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_OrderID =" +
                                     "Lab_Order_Test_ResultDtl.labotrd_OrderID AND " +
                                     "Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID " +
                                     " WHERE(labom_PatientID = " + PatientId + ")ORDER By labom_TransactionDate desc, labom_OrderNoID";
                    cmd.Connection = con;
                    cmd.CommandText = strSql;
                    Da.SelectCommand = cmd;
                    Da.Fill(Ds);
                    return Ds.Tables[0];                    

                    //return dt;
                }
                catch (Exception )
                {
                    return null;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        cmd = null;
                    }
                    Ds.Dispose();
                    Da.Dispose();
                }                             
           }

        private void DesignGrid()
        {

            try
            {
                //c1TestLibrary.Clear();
                c1TestLibrary.DataSource = null;
                c1TestLibrary.Clear();

                // setfont
                c1TestLibrary.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                c1TestLibrary.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                c1TestLibrary.BackColor= Color.White;
                c1TestLibrary.AllowSorting= C1.Win.C1FlexGrid.AllowSortingEnum.None;

                
                c1TestLibrary.Cols.Count= COL_COUNT;
                c1TestLibrary.Cols.Fixed=1;
                c1TestLibrary.Rows.Count=1;
                c1TestLibrary.Rows.Fixed=1;

                // set visibility of column
                c1TestLibrary.Cols[COL_ORDERID].Visible= false;
                c1TestLibrary.Cols[COL_ORDERPREFIX].Visible = true;
                c1TestLibrary.Cols[COL_ORDERNO].Visible = true;
                c1TestLibrary.Cols[COL_TRANSDATE].Visible = true;
           
                // set column editing
                c1TestLibrary.Cols[COL_ORDERPREFIX].AllowEditing = false;
                c1TestLibrary.Cols[COL_ORDERNO].AllowEditing = false;
                c1TestLibrary.Cols[COL_TRANSDATE].AllowEditing = false;
           

                 //set Heading
                c1TestLibrary.SetData(0,COL_ORDERNO,"Order ID");
                c1TestLibrary.SetData(0, COL_ORDERPREFIX, "Order Prefix No");
                c1TestLibrary.SetData(0, COL_ORDERNO, "Order NO");
                c1TestLibrary.SetData(0, COL_TRANSDATE, "Transaction Date");

                c1TestLibrary.ExtendLastCol = true;
                
            }
            catch (Exception e)
            {
                gloEmdeonInterface.Classes.clsGeneral obj = new gloEmdeonInterface.Classes.clsGeneral();
                obj.UpdateLog("Error In Designing Grid " +e.ToString());                
            }

        }

        private void frmViewLabs_Load(object sender, EventArgs e)
        {
            fillPreviousOrdsGrid();
            Set_PatientDetailStrip();
        }

        private void ts_LabMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {            
            if(e.ClickedItem.Name==tlbbtn_Close.Name) // Closing the form
            {
                       this.Close();
            }
            else if (e.ClickedItem.Name == tlbbtn_New.Name) // calling form for new Lab order
            {                    
                frmEmdeonInterface objfrmEmdonInterface = new frmEmdeonInterface(_patientID);
                objfrmEmdonInterface.WindowState = FormWindowState.Maximized;
                objfrmEmdonInterface.ShowDialog(this);
                objfrmEmdonInterface.Dispose();
                objfrmEmdonInterface = null;
            }
        }
        gloUserControlLibrary.gloUC_PatientStrip gloUC_PatientStrip1 = null;
        private void Set_PatientDetailStrip()
        {
            // '' Add Patient Details Control 
            if (gloUC_PatientStrip1 != null)
            {
                if (this.Controls.Contains(gloUC_PatientStrip1))
                {
                    this.Controls.Remove(gloUC_PatientStrip1);
                }
                gloUC_PatientStrip1.Dispose();
                gloUC_PatientStrip1 = null;
            }
            gloUC_PatientStrip1 = new gloUserControlLibrary.gloUC_PatientStrip();
            this.Controls.Add(gloUC_PatientStrip1);
            gloUC_PatientStrip1.Padding = new Padding(3, 0, 3, 0);
            pnlToolStrip.SendToBack();
            {
                gloUC_PatientStrip1.Dock = DockStyle.Top;
                //' Pass Paarameters Type of Form   LabOrderParameter
                                             
               // gloUC_PatientStrip1.DTPValue =  string.Format( _OrderParamter.TransactionDate,"MM/dd/yyyy hh:mm tt"); // Strings.Format(, );
                gloUC_PatientStrip1.DTPValue = LabOrderParameter.TransactionDate;
                gloUC_PatientStrip1.ShowDetail(LabOrderParameter.PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.LabOrder, 0, LabOrderParameter.VisitID, LabOrderParameter.ProviderID, true, true, true, " ", true);                           
            }
        }

        private void c1TestLibrary_DoubleClick(object sender, EventArgs e)
        {

             if (c1TestLibrary.RowSel > 0)
             {
                long curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));              
                ShowOrders(curOrderID);
             }
            
            //try
            //{

            //    _OrderParamter.IsEditMode = true;                
            //    _OrderParamter.OrderNumberID = 0;
            //    _OrderParamter.OrderNumberPrefix = "ORD";
            //    _OrderParamter.PatientID = _OrderParamter.PatientID;
            //    _OrderParamter.VisitID = _OrderParamter.VisitID;
            //    _OrderParamter.TransactionDate = _OrderParamter.TransactionDate;
            //    _OrderParamter.CloseAfterSave = true;

            //    if (c1TestLibrary.RowSel > 0)
            //    {
            //        long curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
            //        _OrderParamter.OrderID = curOrderID;

            //         LoadOrder();
            //    }

            //}
            //catch (Exception e1)
            //{
            //    MessageBox.Show(e1.Message, "gloLabs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        public void ShowOrders(long lngOrderID)
        {
                _OrderParamter.OrderID = lngOrderID;     

                _OrderParamter.IsEditMode = true;
                _OrderParamter.OrderNumberID = 0;
                _OrderParamter.OrderNumberPrefix = "ORD";
                _OrderParamter.PatientID = _OrderParamter.PatientID;
                _OrderParamter.VisitID = _OrderParamter.VisitID;
                _OrderParamter.TransactionDate = _OrderParamter.TransactionDate;
                _OrderParamter.CloseAfterSave = true;                

                    //long curOrderID = Convert.ToInt64(c1TestLibrary.GetData(c1TestLibrary.RowSel, COL_ORDERID));
                    //_OrderParamter.OrderID = curOrderID;

                    LoadOrder();                           
         }

        public Boolean LoadOrder()
        {

            gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
            gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrder oLabActor_Order = null;
            gloEMRGeneralLibrary.gloEMRLab.gloEMRLabContactInfo oLabActorContact = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabContactInfo();

            //Assign Actor to Object 
            oLabActor_Order =  oLabOrderRequest.GetOrder(_OrderParamter.OrderID);
           

            if ((oLabActor_Order != null))
            {
                //Clear All Tests 
                gloUCLab_Transaction.ClearTest();
                             

                //Show Order Detail 
            //    gloUCLab_OrderDetail.SetData(oLabActor_Order.OrderNoPrefix, (Int16) oLabActor_Order.OrderNoID, oLabActor_Order.PreferredLab, oLabActor_Order.ReferredBy, oLabActor_Order.SampledBy, oLabActor_Order.Users, oLabActor_Order.PreferredLabID, oLabActor_Order.ReferredByID, oLabActor_Order.SampledByID, oLabActor_Order.TaskDescription,
             //   oLabActor_Order.TaskDueDate);

                //Show First Test Detail 
                ////--Remark--// 
                //----------------------------------------------------------- 
                //Assign Values to Order Object 

                {
                    
                 //   this.gloUC_PatientStrip1.TransactionDate = oLabActor_Order.TransactionDate;
                //    this.gloUC_PatientStrip1.Provider = oLabActor_Order.Provider;
                  
                    _OrderParamter.ProviderID = oLabActor_Order.ProviderID;
                    _LabProviderID = oLabActor_Order.ProviderID;

                    DataTable oDataTable = new DataTable();
                    string _Provider = null;
                    oDataTable = oLabActorContact.GetProviderName(_OrderParamter.ProviderID);
                    _Provider = "";
                    if ((oDataTable != null))
                    {
                        if (oDataTable.Rows.Count > 0)
                        {
                                                        
                            if (!(oDataTable.Rows[0]["sFirstName"] == System.DBNull.Value))
                            {
                                _Provider = oDataTable.Rows[0]["sFirstName"] + "";
                            }
                            if (!(oDataTable.Rows[0]["sMiddleName"] == System.DBNull.Value))
                            {
                                _Provider = _Provider + " " + oDataTable.Rows[0]["sMiddleName"] + "";
                            }
                            if (!(oDataTable.Rows[0]["sLastName"] == System.DBNull.Value))
                            {
                                _Provider = _Provider + " " + oDataTable.Rows[0]["sLastName"] + "";
                            }
                        }
                    }

                  //  gloUC_PatientStrip1.SetProviderName(_Provider, _OrderParamter.ProviderID);
                    gloUCLab_Transaction.SetData(oLabActor_Order);
                }
            }
            if (oLabActor_Order != null)
            {
                oLabActor_Order.Dispose();
                oLabActor_Order = null;
            }
            if (oLabOrderRequest != null)
            {
                oLabOrderRequest.Dispose();
                oLabOrderRequest = null;
            }
            if (oLabActorContact != null)
            {
                oLabActorContact.Dispose();
                oLabActorContact = null;
            }
            return true;
        }

        private void gloUCLab_History_gUC_FillOrder(short CriteriaNumber)
        {

            try
            {
                gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrders oLabOrders = null;// new gloEMRGeneralLibrary.gloEMRActors.LabActor.LabOrders();
                gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrder = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();

                oLabOrders = oLabOrder.GetOrders(_OrderParamter.PatientID, (gloEMRGeneralLibrary.gloEMRActors.enmHistoryCriteria)CriteriaNumber, false);
                if ((oLabOrders != null))
                {
                    gloUCLab_History.FillOrder(CriteriaNumber, oLabOrders);
                }
                if (oLabOrders != null)
                {
                    oLabOrders.Dispose();
                    oLabOrders = null;
                }
                if (oLabOrder != null)
                {
                    oLabOrder.Dispose();
                    oLabOrder = null;
                }
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "gloLabs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

        }

        private void gloUCLab_History_gUC_OpenLabForModify(long OrderID)
        {
            if (OrderID != 0)
            {
                ShowOrders(OrderID);
            }
        }


    }
        
      //public class LabRequestOrderParameter 
      //{ 
      //   private string _OrderNumberPrefix = ""; 
      //   private Int16 _OrderNumberID = 0; 
      //   private Int64 _OrderID = 0; 
             
      //   //Order Transaction Information 
      //   private bool _IsEditMode = false; 
      //   private Int64 _PatientID = 0; 
      //   private Int64 _VisitID = 0; 
      //   private bool _CloseAfterSave = true;
      //    private System.DateTime _TransactionDate = DateTime.Now; 
  
      //   private enumTransactionType _TransactionType; 
      //   private long _ProviderID; 
                                
      //          public enum enumTransactionType 
      //          { 
      //              None = 0, 
      //              LabOrder = 1, 
      //              LabResult = 2, 
      //              LabExternalResult = 3 
      //          } 
                
      //          public enumTransactionType TransactionType { 
      //              get { return _TransactionType; } 
      //              set { _TransactionType = value; } 
      //          } 
                
      //          public string OrderNumberPrefix { 
      //              get { return _OrderNumberPrefix; } 
      //              set { _OrderNumberPrefix = value; } 
      //          } 
                
      //          public Int16 OrderNumberID { 
      //              get { return _OrderNumberID; } 
      //              set { _OrderNumberID = value; } 
      //          } 
                
      //          public Int64 OrderID { 
      //              get { return _OrderID; } 
      //              set { _OrderID = value; } 
      //          } 
                
      //          public Int64 PatientID { 
      //              get { return _PatientID; } 
      //              set { _PatientID = value; } 
      //          } 
                
      //          public Int64 VisitID { 
      //              get { return _VisitID; } 
      //              set { _VisitID = value; } 
      //          } 
                
      //          public bool IsEditMode { 
      //              get { return _IsEditMode; } 
      //              set { _IsEditMode = value; } 
      //          } 
                
      //          public bool CloseAfterSave { 
      //              get { return _CloseAfterSave; } 
      //              set { _CloseAfterSave = value; } 
      //          } 
                
      //          public System.DateTime TransactionDate { 
      //              get { return _TransactionDate; } 
      //              set { _TransactionDate = value; } 
      //          } 
      //          public long ProviderID { 
      //              get { return _ProviderID; } 
      //              set { _ProviderID = value; } 
      //          } 
      //          public LabRequestOrderParameter() : base() 
      //          { 
      //          } 
                                                            
      //      } 
   
}