using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloDatabaseLayer;

namespace gloReports
{
    public partial class frmDeleteOptions : Form
    {
        //Int64 _TotalLogCount=0;
        string strConnectionstring = string.Empty;
        string gstrMessageBoxCaption = string.Empty;
        public bool isDeleteRecord=false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
    
        private string sInterfaceService ;
        public string InterfaceService
        {
            get { return sInterfaceService; }
            set { sInterfaceService = value; }
        }


        #region "Constructor & Distructor"
        public frmDeleteOptions(string sConectionString)
        {               
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
            { 
                gstrMessageBoxCaption = "gloEMR"; 
            }
            InitializeComponent();
            strConnectionstring = sConectionString;
            lblLogCount.Text = GetHL7Record();
        }
            ~frmDeleteOptions()
            {
                this.Dispose();
            }
        #endregion "Constructor & Distructor"

        #region "Form Events"

        private void frmDeleteOptions_Load(object sender, EventArgs e)
        {
             rb_DeleteAll.Checked = true;
        }

        private void tlsp_DeleteLog_Click(object sender, EventArgs e)
        {
            string _strQuery = string.Empty;
            int nRecCont = 0;
            //if (lblLogCount.Text == "0")
            //{
            //    MessageBox.Show("No record found to delete.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            isDeleteRecord = true;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(strConnectionstring);
                oDBLayer.Connect(false);
                if (rb_DeleteAll.Checked)
                {
                    nRecCont= oDBLayer.Execute_Query("delete from HL7_MessageLog");
                    if (nRecCont > 0)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports,
                        gloAuditTrail.ActivityCategory.ViewInterfaceReport,
                        gloAuditTrail.ActivityType.Delete, "All Interface logs deleted successfully for " + sInterfaceService, 0, 0, 0,
                        gloAuditTrail.ActivityOutCome.Success, (gstrMessageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
                    }
                }
                else if (rb_DeleteSelected.Checked)
                {
                    if (string.IsNullOrEmpty(((Control)this.RecordCount).Text))
                    {
                        MessageBox.Show("Enter number of records to be deleted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    Int32 iRecDel = Convert.ToInt32(RecordCount.Value);
                  nRecCont= oDBLayer.Execute_Query("delete TOP ("+ iRecDel +") from HL7_MessageLog");
                  if (nRecCont > 0)
                  {
                      gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports,
                      gloAuditTrail.ActivityCategory.ViewInterfaceReport,
                      gloAuditTrail.ActivityType.Delete, nRecCont + " interface logs deleted successfully for " + sInterfaceService, 0, 0, 0,
                      gloAuditTrail.ActivityOutCome.Success, (gstrMessageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
                  }
                }
               
                MessageBox.Show("Records deleted successfully", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
              
            catch (Exception)
            {
                MessageBox.Show("Invalid selection", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (nRecCont > 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Reports,
                    gloAuditTrail.ActivityCategory.ViewInterfaceReport,
                    gloAuditTrail.ActivityType.Delete, "Failed to delete interface logs for " + sInterfaceService, 0, 0, 0,
                    gloAuditTrail.ActivityOutCome.Failure, (gstrMessageBoxCaption == "gloPM" ? gloAuditTrail.SoftwareComponent.gloPM : gloAuditTrail.SoftwareComponent.gloEMR));
                }
             }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Disconnect();
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }

            }
            lblLogCount.Text = GetHL7Record();
         }

        private void ts_btnCancel_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void RecordCount_ValueChanged(object sender, EventArgs e)
        {
            if (rb_DeleteSelected.Checked == false)
                rb_DeleteSelected.Checked = true;
        }

        private void RecordCount_MouseClick(object sender, MouseEventArgs e)
        {
            if (rb_DeleteSelected.Checked == false)
                rb_DeleteSelected.Checked = true;
        }

        private void RecordCount_KeyUp(object sender, KeyEventArgs e)
        {
            if (rb_DeleteSelected.Checked == false)
                rb_DeleteSelected.Checked = true;
        }
        #endregion "Form Events"

        #region "user Define Event"
        private string GetHL7Record()
        {
            Object _strRecCnt;
            String sRecordCount = string.Empty;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(strConnectionstring);
                oDBLayer.Connect(false);
               
                _strRecCnt =oDBLayer.ExecuteScalar_Query("SELECT COUNT(ISNULL(MessageID,0)) FROM HL7_MessageLog");
                              
                sRecordCount = Convert.ToString(_strRecCnt);
                
            }
            catch (Exception )
            {
                sRecordCount = "0";
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Disconnect();
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }

            }
            return sRecordCount;
        }
        #endregion "user Define Event"
    }
}
