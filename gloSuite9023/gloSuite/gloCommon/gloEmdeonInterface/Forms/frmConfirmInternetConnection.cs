using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloEmdeonInterface.Forms
{
    public partial class frmConfirmInternetConnection : Form
    {
        private bool _IsInternetConnectionFailed = false;

        public frmConfirmInternetConnection(bool IsInternetConnectionFailed)
        {
            _IsInternetConnectionFailed = IsInternetConnectionFailed;

            InitializeComponent();
        }

        private void btn_ConnectionFailed_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmConfirmInternetConnection_Load(object sender, EventArgs e)
        {
            if (_IsInternetConnectionFailed)
            {
                pnlInternetFailed.BringToFront();
                pnlInternetFailed.Visible = true;
            }
            else
            {
                pnlConnectionFailed.BringToFront();
                pnlConnectionFailed.Visible = true;
            }
            

        }
        private void btn_InternetFailed_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
