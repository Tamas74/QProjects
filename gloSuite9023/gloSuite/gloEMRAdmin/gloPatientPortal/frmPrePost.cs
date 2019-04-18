using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloPatientPortal
{
    public partial class frmPrePost : Form
    {

        public string textItems { get; set; }
        public string strLabel { get; set; }

        //public int IsPrePost { get; set; }
        
        public frmPrePost()
        {
            InitializeComponent();
           
        }

        private void ts_Save_Click(object sender, EventArgs e)
        {
            textItems=txtPrePost.Text.ToString();           
            this.Close();
        }

        private void ts_ShowHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrePost_Load(object sender, EventArgs e)
        {
            lblPrePost.Text=strLabel;
            txtPrePost.Text = textItems;
            txtPrePost.Select(0,0);
            txtPrePost.Focus();

        }
    }
}
