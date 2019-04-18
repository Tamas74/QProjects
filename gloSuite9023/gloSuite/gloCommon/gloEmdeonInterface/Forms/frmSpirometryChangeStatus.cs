using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloEmdeonInterface.Forms
{
    public partial class frmSpirometryChangeStatus : Form
    {
        string sPreStatus = string.Empty;//define variable to assign previous status by RK on 20110602
        string _gstrMessageBoxCaption = string.Empty;
       
        public string Status 
        {
            get
            {
                return Convert.ToString(cmbStatus.Text);  
               
            }
            set
            {
                cmbStatus.Text =value;
            }
        }

        public frmSpirometryChangeStatus(String StatusMessage)
        {
          InitializeComponent();
          Status = StatusMessage;
          sPreStatus = StatusMessage;//assign previous status by RK on 20110602
          System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
          if (appSettings["MessageBOXCaption"] != null)
          {
              if (appSettings["MessageBOXCaption"].Length > 0)
              {
                  _gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
              }
              else
              {
                  _gstrMessageBoxCaption = "gloEMR";
              }
          }
          else
          { _gstrMessageBoxCaption = "gloEMR"; }
        }

        private void ts_LabMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "Save&Close":
                    {
                        this.Close(); 
                        break;
                    }
                case "Close":
                    {
                        if (sPreStatus != Status )
                        {
                            if ((MessageBox.Show("Do you want to save the status?.", _gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.No)
                          {
                            Status = sPreStatus;
                          }
                       }
                       this.Close(); 
                       break;
                    }
            }
        }

      

        
    }
}
