using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloEmdeonInterface.Classes;
using gloEmdeonInterface.Classes.MergeOrderClasses;

namespace gloEmdeonInterface.Forms
{


    public partial class frmMergeOrderPreview : Form
    {
        #region "C1 Constants"
        private const Int16 COL_ORDERID = 0;
        private const Int16 COL_ORDERPREFIX = 1;
        private const Int16 COL_ORDERNO = 2;
        //private const Int16 COL_REFEREANCEID = 3;
        private const Int16 COL_TRANSDATE = 3; //4
        private const Int16 COL_CUSTOMORDERSTATUS = 4; //5
        //private const Int16 COL_BILLINGTYPE = 6;
        private const Int16 COL_PROVIDERNAME = 5; //7
        private const Int16 COL_ORDER_HAS_RESULTS = 6; //8
        private const Int16 COL_ORDER_ISACKNOWLEDGED = 7; //9        
        //private const Int16 COL_ISORDERLOCKED = 10;
        //private const Int16 COL_MACHINENAME = 11;        
        private const Int16 COL_HasResult_Value = 8; //12
        private const Int16 COL_HasAkw_Value = 9; //13
        private const Int16 COL_ORDERSTATUS = 10; //14
        private const Int16 COL_COUNT = 11; //15
        #endregion

        #region "Properties"

        public clsMergeOrder MergeOrder { get; set; }

        public DataSet BindingDataSet { get; set; }

        [DefaultValue(false)]
        public Boolean IsMerging { get; private set; }
        
        #endregion

        public frmMergeOrderPreview()
        {
            InitializeComponent();
        }

        private void frmMergeOrderPreview_Load(object sender, EventArgs e)
        {
            gloUC_Preview.Visible = true;
            gloUC_Preview.Dock = DockStyle.Fill;

            try
            {
                gloUC_Preview.Bind(this.BindingDataSet);
                DisplayOrderDetails(MergeOrder.TargetOrder);
            }
            catch (Exception ex) 
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Convert.ToString(ex), false);
                MessageBox.Show(Convert.ToString(ex), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }

            
        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        { this.Close(); }

        private void AcceptMerge(object sender, EventArgs e)
        {
            try
            {
                if (MergeOrder.TargetOrder.IsMerged)
                {
                    MergeOrder.ExecuteMerge();
                    this.IsMerging = true;

                    this.Close();
                }
            }
            catch (gloEmdeonInterface.Classes.MergeOrderClasses.TemplateException templateException)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(Convert.ToString(templateException), false);
                MessageBox.Show(Convert.ToString(templateException.Message), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Convert.ToString(ex), false);
                MessageBox.Show(Convert.ToString(ex.Message), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        #region "Order Details filling"
       
        private void DisplayOrderDetails(clsGloOrder BindingOrder)
        {

            try
            {
                lblOrderNo.Text = "ORD " + Convert.ToString(BindingOrder.OrderNoID);

                lblOrdered.Text = BindingOrder.OrderDate.ToString();

                lblProvider.Text = BindingOrder.ProviderName;
                lblStatus.Text = BindingOrder.OrderStatus;

                lblAcknowledged.Text = "";

                if (BindingOrder.IsAcknowledged)
                { lblAcknowledged.Text = "Yes"; }
                else
                { lblAcknowledged.Text = "No"; }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Convert.ToString(ex), false);
                MessageBox.Show(Convert.ToString(ex.Message), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }                     
        }

        #endregion
    }
}
