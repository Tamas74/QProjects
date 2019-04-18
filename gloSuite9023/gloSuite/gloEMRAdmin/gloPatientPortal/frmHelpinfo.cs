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
    public partial class frmHelpinfo : Form
    {
        int intImgtype = 0;

        /// <summary>
        /// Set Help Image based on type specified.
        /// </summary>
        /// <param name="GrporQueImg">1 = Group , 2 = Question</param>
         public frmHelpinfo(int GrporQueImg)
        {
            InitializeComponent();
            intImgtype = GrporQueImg;
        }

        private void frmHelpinfo_Load(object sender, EventArgs e)
        {
            try
            {
                //lblDatatable.Visible = false;
                //lblQuestion.Visible = false;
                if (intImgtype == 1)
                {
                    // imghelp.Image = Properties.Resources.datatable;
                    //lblQuestion.Visible = false;
                    //lblDatatable.Visible = true;
                }
                else if (intImgtype == 2)
                {
                    //lblDatatable.Visible = false;
                    //lblQuestion.Visible = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmHelpinfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
