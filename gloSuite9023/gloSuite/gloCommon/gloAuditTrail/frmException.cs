using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloAuditTrail
{
    public partial class frmException : Form
    {
      //  string _exp = "";
      //  string _expdetail = "";
        public frmException(string Exp,string ExpDetail)
        {
            InitializeComponent();
            this.Height = 170;
          
            txterror.Text   = Exp;
           txterrdetail.Text   = ExpDetail;  
        }
      

        private void btnok_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btndetail_Click(object sender, EventArgs e)
        {
            if (this.Height > 400)
            {
                this.Height = 170;
            }
            else
            {

                this.Height = 480;
            }
        }
    }
}
