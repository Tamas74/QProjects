using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloEmdeonInterface.Classes;

namespace gloEmdeonInterface.Forms
{
    public partial class frmViewOrderTests : Form
    {
        #region "C1 Constants"
    
        private const Int16  col_Select =0;
        private const Int16 col_LabTestName = 1;
        private const Int16 col_LabTestID = 2;
        #endregion
        #region "Variable Declaration"
        private Int64 _OrderID = 0;
        clsGetGloLabData objclsGetGloLabData = new clsGetGloLabData();
        #endregion
        public string _TestNames = "";  //added to show testnames on Emdeon Screen
        public frmViewOrderTests()
        {
            InitializeComponent();
        }
        #region "Property Procedures"
        public Int64 OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }
        public String sTestsID { get; set;}
        private Boolean _IsSplitOrder = false;
        public Boolean IsSplitOrder
        {
            get { return _IsSplitOrder; }
            set { _IsSplitOrder = value; }
        }
        #endregion


        #region "Private Procedures"
        
        private void frmViewOrderTests_Load(object sender, EventArgs e)
        {
            FillTestDetails();
        }
        private void FillTestDetails()
        {
               gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
            try
            {


                _IsSplitOrder = true;
                objclsGetGloLabData.IsSplitOrder = _IsSplitOrder;
            DataTable dt;
         
            dt = oLabOrderRequest.GetSplitOrderTests(_OrderID);
            C1SplitOrderTests.DataSource = dt;
            for (Int32 i = 0; i <= C1SplitOrderTests.Cols.Count - 1; i++)
            {
                C1SplitOrderTests.Cols[i].Visible = false;

            }
            C1SplitOrderTests.ExtendLastCol = true;
            C1SplitOrderTests.AllowEditing = true;
            C1SplitOrderTests.Cols[col_LabTestName].Visible = true;
            C1SplitOrderTests.Cols[col_Select].Visible = true;
            C1SplitOrderTests.Cols[col_Select].DataType = System.Type.GetType("System.Boolean");
            C1SplitOrderTests.Cols[col_LabTestName].Width = 200;
            C1SplitOrderTests.Cols[col_LabTestName].Caption = "Test Name";
            C1SplitOrderTests.Cols[col_Select].AllowEditing = true;
            C1SplitOrderTests.Cols[col_LabTestName].AllowEditing = false;
            }
               catch(Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oLabOrderRequest != null)
                {
                    oLabOrderRequest.Dispose();
                    oLabOrderRequest = null;
                }
            }
        }
        private void tlsp_OrderTests_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            
            
            try
            {
                if (e.ClickedItem.Name == this.ts_btnSave.Name)
                {
                    bool _Result=false ;
                   
                        string strTestID = "";
                        Int32 _countTestID = 0 ;
                    
                        for (Int32 i = 1; i <= C1SplitOrderTests.Rows.Count - 1; i++)
                        {
                            if (Convert.ToString ( C1SplitOrderTests.GetData (i,0))=="1")
                            {
                                if (strTestID == "")
                                {
                                    strTestID = Convert.ToString ( C1SplitOrderTests.GetData(i, col_LabTestID ));
                                    _TestNames = Convert.ToString(C1SplitOrderTests.GetData(i, col_LabTestName ));
                                }
                                else
                                {
                                    strTestID = strTestID + "," + Convert.ToString(C1SplitOrderTests.GetData(i, col_LabTestID));
                                    _TestNames = _TestNames+"~"+Convert.ToString(C1SplitOrderTests.GetData(i, col_LabTestName));
                                }
                                _countTestID = _countTestID + 1;
                          
                        
                            }
                           
                        }
                        if (_countTestID > 0)
                        {
                            if (_countTestID < C1SplitOrderTests.Rows.Count - 1)
                            {
                              //  DialogResult drMesgResult = MessageBox.Show("Do you want to create new order as an Emdeon Order?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2 );

                                //if (drMesgResult == DialogResult.Yes)
                                //{

                                //}
                                //else if (drMesgResult == DialogResult.No)
                                //{
                                if (chkEmdeon.Checked)
                                {
                                    if (strTestID != "")
                                    {
                                        sTestsID = strTestID;
                                        _Result = true;
                                    }
                                    else
                                    {
                                        _Result = false;
                                    }
                                }
                                else
                                {

                                    gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder oLabOrderRequest = new gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder();
                                    _Result = oLabOrderRequest.SplitOrder(_OrderID, strTestID);
                                    if (oLabOrderRequest != null)
                                    {
                                        oLabOrderRequest.Dispose();
                                        oLabOrderRequest = null;
                                    }
                                }
                                //}
                                
                            }
                            else
                            
                            {
                                MessageBox.Show("There should be at least one Test remaining in the existing Order. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                        }
                        else
                        {
                            MessageBox.Show("Please select one Test to create a new Order. ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                       
                    
                     if (_Result == true)
                     {
                         this.DialogResult = DialogResult.OK;
                         this.Close();
                     }


               

                }
                else if (e.ClickedItem.Name == this.ts_btnClose.Name)
                {
                    this.Close();

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
