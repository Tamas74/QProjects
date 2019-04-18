using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloBilling;

namespace gloAccountsV2
{
    public partial class frmTransferResponsibilityStatus : Form
    {
        public DataTable dtResponsibilityTransfer { get; set; }

        public frmTransferResponsibilityStatus()
        {
            InitializeComponent();
        }

        private void frmTransferResponsibilityStatus_Load(object sender, EventArgs e)
        {
            c1InsuranceResposibility.DataSource = dtResponsibilityTransfer;
            c1InsuranceResposibility.Cols[0].Width = 250;
            c1InsuranceResposibility.Cols[1].Width = 150;
            c1InsuranceResposibility.Cols[2].Width = 80;
            c1InsuranceResposibility.Cols[3].Width = 80;
            //c1InsuranceResposibility.Cols[4].Width = 150;

            //c1InsuranceResposibility.Cols[3].Visible = false;
            c1InsuranceResposibility.Cols[4].Visible = false;
        }

        private void c1InsuranceResposibility_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTipForLineBreak(c1SuperTooltip1, (C1.Win.C1FlexGrid.C1FlexGrid)sender, e.Location);
        }
              

        private void tlsbtn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlsOK_Click(object sender, EventArgs e)
        {
            
        }
           

    }
}
