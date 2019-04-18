using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloSettings;

namespace gloBilling
{
    public partial class frmPromptforGlobalPeriod : Form
    {
        #region "Variable declaration"
        
        DataTable _dtGPInfo = null;

        #endregion

        #region "Properties"

        public GPPromptOutput PromptOutput { get; set; } 

        #endregion

        #region " Constructor "

        public frmPromptforGlobalPeriod()
        {
            InitializeComponent();
        }

        public frmPromptforGlobalPeriod(DataTable _dt)
        {
            InitializeComponent();
            _dtGPInfo = _dt;
        } 

        #endregion

        #region " Form Events "

        private void frmPromptforGlobalPeriod_Load(object sender, EventArgs e)
        {          

            if (_dtGPInfo.Rows.Count > 1)
            {
                pnlMultipleGlobalPeriodPrompt.Visible = true;
                pnlSingleGlobalPeriodPrompt.Visible = false;
                this.Height = 204;

                chkMultiStoptoReviewCharges.Checked = true;
                PromptOutput = GPPromptOutput.Multiple_StoptoReviewCharges;
              
            }
            else
            {
                pnlMultipleGlobalPeriodPrompt.Visible = false;
                pnlSingleGlobalPeriodPrompt.Visible = true;

                lblCPT.Text = Convert.ToString(_dtGPInfo.Rows[0]["sCPT"]);
                lblPeriodDateRange.Text = Convert.ToString(_dtGPInfo.Rows[0]["sDate"]);
                lblProvider.Text = Convert.ToString(_dtGPInfo.Rows[0]["sProviderName"]);
                lblInsurance.Text = Convert.ToString(_dtGPInfo.Rows[0]["sInsuranceName"]);
                lblReminder.Text = Convert.ToString(_dtGPInfo.Rows[0]["sReminder"]);
                lblNotes.Text = Convert.ToString(_dtGPInfo.Rows[0]["sNotes"]);

                chkStoptoReviewCharges.Checked = true;
                PromptOutput = GPPromptOutput.Single_StoptoReviewCharges;
            }

            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            // This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme);
        }

        private void btnSingleGPAction_Click(object sender, EventArgs e)
        {
            if (chkStoptoReviewCharges.Checked)
            {
                PromptOutput = GPPromptOutput.Single_StoptoReviewCharges;
                this.Close();
            }
            else if (chkStoptoReviewGlobalPeriodDetails.Checked)
            {
                PromptOutput = GPPromptOutput.Single_StoptoReviewGlobalPeriodDetails;
                this.Close();
            }
            else if (chkContinueSaveofNewCharges.Checked)
            {
                PromptOutput = GPPromptOutput.Single_ContinueSaveofNewCharges;
                this.Close();
            }
            else
            {
                MessageBox.Show("Choose one option.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void btnMultipleGPAction_Click(object sender, EventArgs e)
        {
            if (chkMultiStoptoReviewCharges.Checked)
            {
                PromptOutput = GPPromptOutput.Multiple_StoptoReviewCharges;
                this.Close();
            }
            else if (chkMultiStoptoReviewGlobalPeriodDetails.Checked)
            {
                PromptOutput = GPPromptOutput.Multiple_StoptoReviewGlobalPeriodDetails;
                this.Close();
            }
            else if (chkMultiContinueSaveofNewCharges.Checked)
            {
                PromptOutput = GPPromptOutput.Multiple_ContinueSaveofNewCharges;
                this.Close();
            }
            else
            {
                MessageBox.Show("Choose one option.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        } 

        #endregion

        private void lblReminder_MouseHover(object sender, EventArgs e)
        {
            if (Convert.ToString(lblReminder.Text).Trim() != "") 
            {
                tooltip_Billing.SetToolTip(this.lblReminder, Convert.ToString(lblReminder.Text));
            }
        }

        private void lblNotes_MouseHover(object sender, EventArgs e)
        {
            if (Convert.ToString(lblNotes.Text).Trim() != "")
            {
                tooltip_Billing.SetToolTip(this.lblNotes, Convert.ToString(lblNotes.Text));
            }
        }

    
    }
}
