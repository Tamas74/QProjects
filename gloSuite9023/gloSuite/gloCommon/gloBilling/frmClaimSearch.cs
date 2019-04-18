using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmClaimSearch : Form
    {
        #region " Variable Declarations"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public bool oDialogResult = false;
     //   private string _databaseConnection = "";
        private string _messageBoxCaption = "";

        #endregion

        #region " Public Enums "

        public enum SelectedOptions
        {
            HighlightedClaim,
            ClaimByPosition,
            ClaimByClaimNo
        } 

        #endregion

        #region "Public Properties "

        public SelectedOptions SelectedOption { get; set; }
        public string SelectedValue { get; set; }
        public ArrayList SelectedClaimList { get; set; } 

        #endregion

        #region " Constructor "

        public frmClaimSearch()
        {
            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion

        }

        public frmClaimSearch(ArrayList arSelectedClaimNo)
        {
            SelectedClaimList = arSelectedClaimNo;
            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion

        } 

        #endregion

        #region " Form Load "
        
        private void frmClaimSearch_Load(object sender, EventArgs e)
        {
            try
            {
                rbHighlightedClaim.Checked = false;
                numQueueClaimCount.Enabled = false;
                cmbSelectedClaim.Enabled = true;
                rbClaimByClaimNo.Checked = true;
                
                if (SelectedClaimList != null)
                {
                    numQueueClaimCount.Maximum = SelectedClaimList.Count;
                    numQueueClaimCount.Increment = 1;
                    numQueueClaimCount.Minimum = 1;

                    for (int iCount = 0; iCount <= SelectedClaimList.Count - 1; iCount++)
                    {
                        cmbSelectedClaim.Items.Add(SelectedClaimList[iCount]);
                    }
                }
                cmbSelectedClaim.SelectedIndex = -1; 
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        } 

        #endregion

        #region " Public Methods "

        public bool ISValidClaim()
        {
            try
            {
                if (cmbSelectedClaim.Text != "")
                {
                    if (!SelectedClaimList.Contains(cmbSelectedClaim.Text.ToString()))
                    {
                        MessageBox.Show("Not a valid claim as per selection.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbSelectedClaim.Text = "";
                        cmbSelectedClaim.Focus();
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
        } 

        #endregion

        #region " Form Control Events "

        private void tls_SaveAndCloseMod_Click(object sender, EventArgs e)
        {
            try
            {
                if (ISValidClaim())
                {
                
                    oDialogResult = true;

                    if (rbHighlightedClaim.Checked)
                    {
                        SelectedOption = SelectedOptions.HighlightedClaim;
                        SelectedValue = "";
                    }
                    else if (rbClaimByPosition.Checked)
                    {
                        SelectedOption = SelectedOptions.ClaimByPosition;
                        SelectedValue = Convert.ToString(numQueueClaimCount.Value);
                    }
                    else if (rbClaimByClaimNo.Checked)
                    {   

                        if (cmbSelectedClaim.Text != "")
                        {
                            SelectedOption = SelectedOptions.ClaimByClaimNo;
                            if(cmbSelectedClaim.SelectedText!="")
                                SelectedValue = Convert.ToString(cmbSelectedClaim.SelectedText);
                            else
                                SelectedValue = Convert.ToString(cmbSelectedClaim.Text);
                        }
                        else
                        {
                            MessageBox.Show("Select a claim #.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tls_CloseMod_Click(object sender, EventArgs e)
        {
            try
            {
                oDialogResult = false;
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void rbClaimByPosition_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbClaimByPosition.Checked)
                {
                    numQueueClaimCount.Enabled = true;
                }
                else
                {
                    numQueueClaimCount.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void rbClaimByClaimNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbClaimByClaimNo.Checked)
                {
                    cmbSelectedClaim.Enabled = true;
                }
                else
                {
                    cmbSelectedClaim.Enabled = false;
                    cmbSelectedClaim.SelectedIndex = -1;
                    cmbSelectedClaim.Text = "";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void cmbSelectedClaim_Leave(object sender, EventArgs e)
        {
           
        }
        private void numQueueClaimCount_ValueChanged(object sender, EventArgs e)
        {
            if (numQueueClaimCount.Enabled == true)
            {
                if (numQueueClaimCount.Value > SelectedClaimList.Count)
                    numQueueClaimCount.Value = SelectedClaimList.Count;
            }
        }

        #endregion
    }
}
