using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
//using BitmapRegionTest;

//

namespace gloPM
{
    public partial class frmgloAboutUs : System.Windows.Forms.Form
    {
        //================= Load our bitmaps
        private Bitmap bmpFrmBack = new Bitmap(gloGlobal.Properties.Resources.Aboutus);
        //================================================ 
      
        
        private string _version = "";

        public frmgloAboutUs()
        {
            InitializeComponent();
        }

        private void frmgloAboutUs_Load(object sender, EventArgs e)
        {
            // --------------Make our bitmap region for the form
            gloGlobal.BitmapRegion.CreateControlRegion(this, bmpFrmBack);
            // --------------

         //  lblGlostreamLink.Links.Add(0,lblGlostreamLink.Text.Length,"www.glostream.com");    

            //FileInfo oFile;/
            label3.Focus();
            //Roopali Change for Terminal Server.

            //oFile = new FileInfo(Application.StartupPath + "\\gloPMS.EXE");
            //oFile = new FileInfo(appSettings["StartupPath"].ToString()  + "\\gloPMS.EXE");



            //  lblApplicationVersion.BackColor = Color.White;
            //  lblDatabaseVersion.BackColor = Color.White;
            //  lblBuildDate.BackColor = Color.White;
            //
            
            //lblAppVersion.Text = "Version : " + Application.ProductVersion;

            // Get the version no from the database.
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtversion = new DataTable();
            dtversion = oSetting.GetSetting("gloPMApplicationVersion", 0);
            if (dtversion != null && dtversion.Rows.Count > 0)
            {
                label9.Text = dtversion.Rows[0]["sSettingsValue"].ToString(); 
                _version = dtversion.Rows[0]["sSettingsValue"].ToString();
            }
            FileInfo  oFile = new FileInfo(Application.StartupPath + "\\gloPM.EXE");
            if (oFile.Exists == true)
            {
               
                FileVersionInfo oFileVersionInfo = FileVersionInfo.GetVersionInfo(oFile.FullName);
                label8.Text = oFileVersionInfo.FileVersion;
            }
            //20100630 Version set
            ////label9.Text =gloGlobal.clsMISC.GetMajorVersion(_version);            
            _version = Application.ProductVersion;
            label3.Focus();
       //     lblAppVersion.Text = gloGlobal.clsMISC.GetMajorVersion(_version); 
            //lblAppVersion.Text = Application.ProductVersion; 

            //if (oFile.Exists == true)
            //{
                //lblBuildDate.Text = oFile.LastWriteTime.ToString();
                //FileVersionInfo oFileVersionInfo = FileVersionInfo.GetVersionInfo(oFile.FullName);
                //lblApplicationVersion.Text = oFileVersionInfo.FileVersion;
            //}

            

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = global::gloPM.Properties.Resources.Img_ButtonHover;
            btnClose.BackgroundImageLayout = ImageLayout.Tile;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = null;
        }

      

       

        

    }
}