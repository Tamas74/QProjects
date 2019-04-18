using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloUserControlLibrary;

namespace gloEmdeonInterface.Forms
{
    public partial class frmLabDemonstration : Form
    {
        #region Variables & Declrations

        public Int64 nTaskId = 0;

        Int64 _PatientID = 0;
        Int64 _LoginUserId = 0;
        string _ConnectionString = string.Empty;
        private String gstrMessageBoxCaption = string.Empty;
        gloUserControlLibrary.gloUC_PatientStrip oPatientStrip = null;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private string _LabDemoFileName = string.Empty;
        #endregion

        #region Constructor

        public frmLabDemonstration(Int64 PatientID)
        {
            _PatientID = PatientID;

            _LabDemoFileName = Application.StartupPath + "\\LabDemo\\demo1.htm";
            if (appSettings != null)
            {
                _ConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                _LoginUserId = Convert.ToInt64(appSettings["UserID"]);
            }

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            { gstrMessageBoxCaption = "gloEMR"; }

            #endregion

            InitializeComponent();
            LoadPatientStrip();
           
        }

        #endregion

        #region Events & Methods

        private void tlbbtn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tlbbtn_save_Click(object sender, EventArgs e)
        {
            Classes.clsLabDemonstration objClsLabDemonstration = new gloEmdeonInterface.Classes.clsLabDemonstration();
            try
            {
                if (_PatientID > 0)
                {
                    objClsLabDemonstration.DemoOrdersFilePath = System.Windows.Forms.Application.StartupPath + @"\LabDemo\";
                    objClsLabDemonstration.DemoOrderMstFilePath = objClsLabDemonstration.DemoOrdersFilePath + "Order_Mst.xml";
                    objClsLabDemonstration.DemoTestMstFilePath = objClsLabDemonstration.DemoOrdersFilePath + "Test_Mst.xml";
                    if (objClsLabDemonstration.CheckDemoOrderFileStatus())
                    {

                        objClsLabDemonstration.GetAllLatestOrder(_PatientID, _ConnectionString, 0, "");

                        this.Close();
                    }                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        private void LoadPatientStrip()
        {
            try
            {
                oPatientStrip = new gloUserControlLibrary.gloUC_PatientStrip();
                oPatientStrip.Dock = DockStyle.Top;
                oPatientStrip.Padding = new Padding(3, 0, 3, 0);
                this.Controls.Add(oPatientStrip);
                pnlToolStrip.SendToBack();
                oPatientStrip.DTPEnabled = false;
                oPatientStrip.ShowDetail(_PatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.LabOrder, 0, 0, 0, false, false, false, "", false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }
        #endregion

        private void frmLabDemonstration_Load(object sender, EventArgs e)
        {
            try
            {
                gloPatient.gloPatient.GetWindowTitle(this, _PatientID, _ConnectionString, gstrMessageBoxCaption);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            if (System.IO.File.Exists(_LabDemoFileName))
            {
                webBrowserEmdeon.Navigate(_LabDemoFileName);
            }
            ShowTaskInfo();
        }

        //Developer: Sanjog Dhamke
        //Date:10 Dec 2011
        //Bug ID/PRD Name/Salesforce Case: Lab Usability PRD Show Task Information on Emdeon Lab 
        //Reason: To show task info
        private void ShowTaskInfo()
        {
            if (nTaskId > 0)
            {
                pnlTaskControl.Visible = true;
                gloUC_TaskInfo oUC = new gloUC_TaskInfo(nTaskId);
                oUC.Padding = new Padding(3, 0, 3, 0);
                oUC.Dock = DockStyle.Fill;
                oPatientStrip.DTPEnabled = false;
                pnlTaskControl.Controls.Add(oUC);

            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(_LabDemoFileName))
            {
                webBrowserEmdeon.Navigate(_LabDemoFileName);
            }
        }


    }
}
