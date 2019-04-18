using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace gloPM
{
    public partial class frmLoadDashboard : Form
    {
        private string _ProductVersion = "";

        public frmLoadDashboard()
        {
            InitializeComponent();
        }

        //Bug #55199: 00000517 : PM dashboard performance issue
        //Added changes to resolve the performance issue.
        protected override CreateParams CreateParams
        {
            get
            {
                // Activate double buffering at the form level.  All child controls will be double buffered as well.
                CreateParams cp = base.CreateParams;
                if (gloSettings.gloRegistrySetting.IsServerOS)
                {
                    cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                }
                return cp;
            }
        } 

        private void frmLoadDashboard_Load(object sender, EventArgs e)
        {

            ShowSoftwareDateTime();
        }

        private void ShowSoftwareDateTime()
        {
            try
            {
                //20100630 Version Set
                lblCopyrghTag.Text = gloTransparentScreen.clsgloCopyRightText.gloCopyRightMain;
                label1.Text = gloTransparentScreen.clsgloCopyRightText.gloCopyRightSub;
                _ProductVersion = Application.ProductVersion;
                //lblVersion.Text = _ProductVersion; //Application.ProductVersion;
                lblVersion.Text = gloGlobal.clsMISC.GetMajorVersion(_ProductVersion);

                //lblLastModifiedDate.Text = "Last Modified  " + File.GetLastWriteTime(Application.StartupPath + "\\" + System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName).ToString("dd MMM, yyyy");
                lblLastModifiedDate.Text = File.GetLastWriteTime(Application.StartupPath + "\\" + System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName).ToString("MMM dd, yyyy");
                lblLastModifiedTime.Text = File.GetLastWriteTime(Application.StartupPath + "\\" + System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName).ToString("hh:mm:ss tt");
                Application.DoEvents();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
            }
        }
       
    }
}