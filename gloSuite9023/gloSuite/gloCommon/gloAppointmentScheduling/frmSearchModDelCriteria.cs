using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloAppointmentScheduling
{
    public partial class frmSearchModDelCriteria : Form
    {
        public string SelectedForModDel = "Modify";
        public string SelectedCriteria = "Simple"; //"Recurrence";
        public bool SelectedResult = false;
        
        private string _MessageBoxCaption = string.Empty;
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public frmSearchModDelCriteria()
        {

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
         
            InitializeComponent();
        }

        private void frmSearchModDelCriteria_Load(object sender, EventArgs e)
        {
            if (SelectedForModDel == "Modify")
            {
                tsb_Modify.Visible = true;
                tsb_Delete.Visible = false;
                this.Text = "Modify";
                rbThisOccurence.Text = "Open this occurrence";
                rbSeries.Text = "Open the series";
            }
            else
            {
                tsb_Modify.Visible = false;
                tsb_Delete.Visible = true;
                this.Text = "Delete";
                rbThisOccurence.Text = "Delete this occurrence";
                rbSeries.Text = "Delete the series";
            }

            if (SelectedCriteria == "Simple")
            {
                rbThisOccurence.Checked = true;
            }
            else
            {
                rbSeries.Checked = true;
            }
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            if (rbThisOccurence.Checked == true)
                            {
                                SelectedCriteria = "Simple";
                            }
                            else
                            {
                                SelectedCriteria = "Recurrence";
                            }

                            SelectedResult = true;
                            this.Close();
                        }
                        break;
                    case "Cancel":
                        {
                            this.Close();
                        }
                        break;
                    case "Help":
                        {
                            this.Close();
                        }
                        break;
                    default:
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);              
            }
        }

        private void rbThisOccurence_CheckedChanged(object sender, EventArgs e)
        {
            if (rbThisOccurence.Checked == true)
            {
                rbThisOccurence.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbThisOccurence.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }

        private void rbSeries_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSeries.Checked == true)
            {
                rbSeries.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            }
            else
            {

                rbSeries.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
            }
        }
    }
}