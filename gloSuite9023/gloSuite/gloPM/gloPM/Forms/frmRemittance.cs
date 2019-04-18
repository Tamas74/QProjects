using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloPM
{
    public partial class frmRemittance : Form
    {

     
        #region "Constructor"

            public frmRemittance()
            {
                InitializeComponent();

            }

        #endregion "Constructor"


        private void frmRemittance_Load(object sender, EventArgs e)
        {


            gloC1FlexStyle.Style(c1Transaction, false);

            c1Transaction.Rows.Count = 0;
            c1Transaction.Rows.Fixed = 0;
            c1Transaction.Cols.Count = 0;
            c1Transaction.Cols.Fixed = 0;
            c1Transaction.Clear();
        }

        private void tls_btnOK_Click(object sender, EventArgs e)
        {
            OpenFileDialog oDialog = new OpenFileDialog();
            if (oDialog.ShowDialog(this) == DialogResult.OK)
            {
                string _FileName = "";
                _FileName = oDialog.FileName;
                if (System.IO.File.Exists(_FileName) == true)
                {
                    c1Transaction.LoadGrid(_FileName, C1.Win.C1FlexGrid.FileFormatEnum.TextCustom, C1.Win.C1FlexGrid.FileFlags.AsDisplayed);
                }
            }
            oDialog.Dispose();
            oDialog = null;
        }

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void c1Transaction_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

        }
    }
}