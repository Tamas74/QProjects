using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text; 
using System.Windows.Forms;

namespace gloEmdeonInterface.Forms
{
    public partial class frmGrid : Form
    {
        private string  _strFileName;

        public string  strFileName
        {
            get { return _strFileName; }
            set { _strFileName = value; }
        }
	

        public frmGrid(string strInFileName)
        {
            strFileName = strInFileName; 
            InitializeComponent();
        }

        private void frmGrid_Load(object sender, EventArgs e)
        {
            DataSet dsResponce = new DataSet();
            dsResponce.ReadXml(strFileName);
            dataGridResponce.DataSource = dsResponce.Tables[1];
            //dataGridResponce.DataBind();
        }


    }
}