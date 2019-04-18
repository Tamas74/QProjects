using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloAppointmentScheduling
{
    public partial class frmSearchDate : Form
    {
        public DateTime SelectedDate = DateTime.Now;
        public string SelectedView = "Day View";
        public bool SelectedResult = false;
        
        private string _MessageBoxCaption = string.Empty;

        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


        public frmSearchDate()
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

        private void frmSearchDate_Load(object sender, EventArgs e)
        {
            cmbView.Items.Clear();
            cmbView.Items.Add("Day View");
            cmbView.Items.Add("Week View");
            cmbView.Items.Add("Monthly View");

            if (SelectedView.Trim() != null)
            {
                for (int i = 0; i <= cmbView.Items.Count - 1; i++)
                {
                    if (cmbView.Items[i].ToString() == SelectedView.Trim())
                    {
                        cmbView.SelectedIndex = i;
                        break;
                    }
                }
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
                            if (cmbView.SelectedItem == null)
                            {
                                MessageBox.Show("Please select a view type.  ", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            SelectedDate = dtpDate.Value;
                            SelectedView = cmbView.SelectedItem.ToString();
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
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

       
    }
}