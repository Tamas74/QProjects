using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloReports
{
    public partial class frmHL7MessageDetails : Form
    {
        string gstrMessageBoxCaption = string.Empty;

        public frmHL7MessageDetails(String sMessageId,String sMessageDetails)
        {
            try
            {
                System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

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

                appSettings = null;

                InitializeComponent();

                rtbHL7Message.ReadOnly = true;
                rtbHL7Message.WordWrap = true;

                rtbHL7Message.Text = sMessageDetails;
                lblMessageID.Text = "Message ID : " + sMessageId;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region "   events  "

        private void ts_btnWrap_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtbHL7Message.WordWrap == true)
                {
                    rtbHL7Message.WordWrap = false;
                }
                else
                {
                    rtbHL7Message.WordWrap = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ts_btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion




    }
}
