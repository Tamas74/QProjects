using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace gloEmdeonInterface.Forms
{
    public partial class frmIntuitPatient_View : Form
    {
        UserControls.UC_IntuitPatient_New objUC_IntuitPatient = null;
        ElementHost objElementHost = null;

        private string gloSuiteConString { get; set; }

        public frmIntuitPatient_View(string sgloSuiteConString)
        {
            InitializeComponent();
            gloSuiteConString = sgloSuiteConString;                 
        }

        private void frmIntuitPatient_View_Load(object sender, EventArgs e)
        {
            try
            {              
                objUC_IntuitPatient = new UserControls.UC_IntuitPatient_New(gloSuiteConString);
                objElementHost = new ElementHost();
                objUC_IntuitPatient.FormsWindow = this;
                this.Controls.Add(objElementHost);
                objElementHost.Child = objUC_IntuitPatient;
                objElementHost.Dock = DockStyle.Fill;              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null; 
            }

        }

        private void frmIntuitPatient_View_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (objElementHost != null)
                {
                    objElementHost.Dispose();
                    objElementHost = null;
                }
                if (objUC_IntuitPatient != null)
                {
                    objUC_IntuitPatient.UserControl_Closing();
                    objUC_IntuitPatient = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }          

        }    
     

    }
}
