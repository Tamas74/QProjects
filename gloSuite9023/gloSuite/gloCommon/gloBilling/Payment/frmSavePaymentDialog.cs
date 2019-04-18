using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloSettings;

namespace gloBilling
{
    public partial class frmSavePaymentDialog : Form
    {
        #region Variable Declaration

        private Int32 _strPmntType;
        public int DialogResult = 0;
        #endregion

        #region Constructor

        public frmSavePaymentDialog(Int32 pmntType)
        {
            InitializeComponent();
            _strPmntType = pmntType;
        }

        #endregion
        
        #region Form Events
      
        private void frmSavePaymentDialog_Load(object sender, EventArgs e)
        {
            if (_strPmntType == PaymentDialogType.Payment.GetHashCode())
            {
                pnlCorrection.Visible = false;
                pnlReserves.Visible = false;
            }
            else if (_strPmntType == PaymentDialogType.Correction.GetHashCode())
            {
                pnlPayment.Visible = false;
                pnlReserves.Visible = false;
            }
            else if (_strPmntType == PaymentDialogType.Reserve.GetHashCode())
            {
                pnlPayment.Visible = false;
                pnlCorrection.Visible = false;
            }
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            // This method actually sets the order all the way down the control hierarchy.
            tom.SetTabOrder(scheme);

        }

        #endregion

        #region Form Control Events
     
        private void btnOk_Click(object sender, EventArgs e)
        {
            if ( chkPendInsPayment.Checked == false && chkNewInsPayment.Checked == false)
            {
                MessageBox.Show("Choose one.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (chkPendInsPayment.Checked == true)
                    DialogResult = PaymentDialogResult.PendingInsurancePayment.GetHashCode(); 
                else if(chkNewInsPayment.Checked == true)
                    DialogResult = PaymentDialogResult.NewPayment.GetHashCode(); 
                this.Close();   
            }
        }

        private void chkPendInsPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPendInsPayment.Checked)
            {
                chkNewInsPayment.Checked = false;
            }
        }

        private void chkNewInsPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNewInsPayment.Checked)
            {
                chkPendInsPayment.Checked = false;
            }
        }

        private void chkCorrOrgPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCorrOrgPayment.Checked)
            {
                chkCorrPenInsPmnt.Checked = false;
                chkCorrNewInsPmnt.Checked = false;
            }
        }

        private void chkCorrPenInsPmnt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCorrPenInsPmnt.Checked)
            {
                chkCorrOrgPayment.Checked = false;
                chkCorrNewInsPmnt.Checked = false;
            }
        }

        private void chkCorrNewInsPmnt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCorrNewInsPmnt.Checked)
            {
                chkCorrOrgPayment.Checked = false;
                chkCorrPenInsPmnt.Checked = false;
            }
        }

        private void chkResOrgInsPmnt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkResOrgInsPmnt.Checked)
            {
                chkResPenInsPmnt.Checked = false;
                chkResNewInsPmnt.Checked = false;
            }
        }

        private void chkResPenInsPmnt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkResPenInsPmnt.Checked)
            {
                chkResOrgInsPmnt.Checked = false;
                chkResNewInsPmnt.Checked = false;
            }
        }

        private void chkResNewInsPmnt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkResNewInsPmnt.Checked)
            {
                chkResOrgInsPmnt.Checked = false;
                chkResPenInsPmnt.Checked = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnResOk_Click(object sender, EventArgs e)
        {
            if (chkResNewInsPmnt.Checked == false && chkResOrgInsPmnt.Checked == false && chkResPenInsPmnt.Checked == false )
            {
                MessageBox.Show("Choose one.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (chkResPenInsPmnt.Checked == true)
                    DialogResult = PaymentDialogResult.PendingInsurancePayment.GetHashCode();
                else if (chkResNewInsPmnt.Checked == true)
                    DialogResult = PaymentDialogResult.NewPayment.GetHashCode();
                else if (chkResOrgInsPmnt.Checked == true)
                    DialogResult = PaymentDialogResult.OrininalPayment.GetHashCode();  

                this.Close();
            }
        }

        private void btnResCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCorrOk_Click(object sender, EventArgs e)
        {
            if (chkCorrNewInsPmnt.Checked == false && chkCorrOrgPayment.Checked == false && chkCorrPenInsPmnt.Checked == false)
            {
                MessageBox.Show("Choose one.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (chkCorrPenInsPmnt.Checked == true)
                    DialogResult = PaymentDialogResult.PendingInsurancePayment.GetHashCode();
                else if (chkCorrNewInsPmnt.Checked == true)
                    DialogResult = PaymentDialogResult.NewPayment.GetHashCode();
                else if (chkCorrOrgPayment.Checked == true)
                    DialogResult = PaymentDialogResult.OrininalPayment.GetHashCode();

                this.Close();
            }
        }

        private void btnCorrCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
        #endregion Form Control Events

       
      
    }
}
