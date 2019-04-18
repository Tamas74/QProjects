using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloPatient;

namespace gloBilling.Collections
{
    public partial class frmSetupPaymentPlan : Form
    {

        #region "Constructor"

        public frmSetupPaymentPlan()
        {
            InitializeComponent();
            nUserID = gloGlobal.gloPMGlobal.UserID;
            sUserName = gloGlobal.gloPMGlobal.UserName;


            cmbFollowupAction.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFollowupAction.DrawItem += new DrawItemEventHandler(ShowTooltipOnComboBox);

        }


        #endregion

        #region Properties

        public Int64 nPatientID { get; set; }

        public Int64 nPatientAccountID { get; set; }

        public Int64 nPAccountID { get; set; }

        public DateTime AccountFollowUpTimeStamp { get; set; }
        private Int64 nUserID = 0;
        private String sUserName = "";
        private bool _IsValidDate = true;
        private ComboBox combo;
        private DataTable dtFollowUpActions = null;

        #endregion

        #region "Form Load "

        private void frmSetupPaymentPlan_Load(object sender, EventArgs e)
        {
            FillPatientAccountDetails();
            FillFollowUpActions();
            GetPaymentPlan();
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            tom.SetTabOrder(scheme);
            txtPlanAmount.Focus();
            txtPlanAmount.Select();
        }

        #endregion
        
        #region " Tool Strip Events "


        private void tlb_Ok_Click(object sender, EventArgs e)
        {
            SavePaymentPlan();
           
        }

        private void tlb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region " Private & Public Methods "

        private void FillPatientAccountDetails()
        {
            try
            {
                string _sAccountBannerName = "";

                DataTable dtAccDetails = gloAccount.GetAccountDetails(nPAccountID);
                if (dtAccDetails.Rows[0]["sAccountNo"] != DBNull.Value)
                {
                    _sAccountBannerName = Convert.ToString(dtAccDetails.Rows[0]["sAccountNo"]);
                }
                if (dtAccDetails.Rows[0]["sGuarantorName"] != DBNull.Value)
                {
                    _sAccountBannerName = _sAccountBannerName + "  -  " + Convert.ToString(dtAccDetails.Rows[0]["sGuarantorName"]);
                }
                if (dtAccDetails.Rows[0]["sAccountDesc"] != DBNull.Value)
                {
                    if (Convert.ToString(dtAccDetails.Rows[0]["sAccountDesc"]) != string.Empty)
                    {
                        _sAccountBannerName = _sAccountBannerName + "  -  " + Convert.ToString(dtAccDetails.Rows[0]["sAccountDesc"]);
                    }
                }
                lblAccountNo.Text = _sAccountBannerName;
                this.toolTip1.SetToolTip(lblAccountNo, _sAccountBannerName);
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void FillFollowUpActions()
        {
            try
            {
               CL_FollowUpCode clFollow= new CL_FollowUpCode();
               dtFollowUpActions = clFollow.getFollowUpAction(CollectionEnums.FollowUpType.PatientAccount);  
                if (dtFollowUpActions != null && dtFollowUpActions.Rows.Count > 0)
                {
                    DataRow dr = dtFollowUpActions.NewRow();
                    dr["sFollowUpActionCode"] = "";
                    dr["sFollowUpAction"] = "";
                    dtFollowUpActions.Rows.InsertAt(dr, 0);

                    cmbFollowupAction.BeginUpdate();
                    cmbFollowupAction.DataSource = dtFollowUpActions;
                    cmbFollowupAction.DisplayMember = dtFollowUpActions.Columns["sFollowUpAction"].ColumnName;
                    cmbFollowupAction.ValueMember = dtFollowUpActions.Columns["sFollowUpActionCode"].ColumnName;
                    cmbFollowupAction.EndUpdate();
                    cmbFollowupAction.SelectedIndex = -1;
                }

                DataTable dtAccountFollowUpTimeStamp = null;
                dtAccountFollowUpTimeStamp = CL_FollowUpCode.GetAccountFollowUpTimeStamp(nPAccountID);
                if (dtAccountFollowUpTimeStamp != null && dtAccountFollowUpTimeStamp.Rows.Count > 0)
                {
                    if (Convert.ToString(dtAccountFollowUpTimeStamp.Rows[0]["dtCreatedDateTime"]).Trim() != "")
                    {
                        AccountFollowUpTimeStamp = Convert.ToDateTime(dtAccountFollowUpTimeStamp.Rows[0]["dtCreatedDateTime"]);
                    }
                    else
                    {
                        AccountFollowUpTimeStamp = DateTime.MinValue;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void SavePaymentPlan()
        {
            bool bHasWorked = false;
            if (ValidatePaymentPlan())
            {
                if (cmbFollowupAction.SelectedIndex >= 1)
                {
                    CL_FollowUpCode oFollowUpCode = new CL_FollowUpCode();

                    var sActionLogDesc = (from dt in dtFollowUpActions.AsEnumerable()
                                          where dt.Field<String>("sFollowUpActionCode") == Convert.ToString(cmbFollowupAction.SelectedValue)
                                          select dt.Field<String>("sFollowUpActionDescription")).ToArray().Single();

                    oFollowUpCode.SaveFollowUpScedule(CollectionEnums.FollowUpType.PatientAccount, nPAccountID, nPatientID, Convert.ToDateTime(mskFollowupDate.Text), Convert.ToString(cmbFollowupAction.SelectedValue), sActionLogDesc, nUserID, sUserName, CollectionEnums.ScheduleType.Manual,AccountFollowUpTimeStamp, ref bHasWorked);
                   
                }

                if (bHasWorked)
                {
                    MessageBox.Show("Someone else has just worked this Account.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Plan Amount
                    CL_PaymentPlan.AddModifyPaymentPlan(0, nPatientID, nPAccountID, nPatientAccountID, txtPlanAmount.Text.Trim());

                    //Account Notes
                    if (txtNotes.Text != null && Convert.ToString(txtNotes.Text).Trim() != "")
                    {
                        gloAccount.SavePatAccNotes(0, gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()), txtNotes.Text.Trim(), nPatientID, nPatientAccountID, nPAccountID);

                    }
                    this.Close();
                }
               
            }
            
        }

        private void GetPaymentPlan()
        {
             DataSet dsPaymentPlan=CL_PaymentPlan.GetPaymentPlan(nPAccountID);
             Int64 _NoofDays = 0;
             string _ActionCode = "";
             if (dsPaymentPlan != null)
             {
                 if (dsPaymentPlan.Tables[0] != null)
                 {
                     if (dsPaymentPlan.Tables[0].Rows.Count > 0)
                     {
                         if (dsPaymentPlan.Tables[0].Rows[0]["dPlanAmount"] != DBNull.Value)
                         {

                             txtPlanAmount.Text = dsPaymentPlan.Tables[0].Rows[0]["dPlanAmount"].ToString();
                         }
                     }
                 }

                 if (dsPaymentPlan.Tables[1] != null)
                 {
                     if (dsPaymentPlan.Tables[1].Rows.Count > 0)
                     {
                         if (dsPaymentPlan.Tables[1].Rows[0]["sPaymentPlanDays"] != DBNull.Value)
                         {

                             _NoofDays = Convert.ToInt64(dsPaymentPlan.Tables[1].Rows[0]["sPaymentPlanDays"]);

                             DateTime dt_Follow = new DateTime();
                             dt_Follow = DateTime.Now;
                             dt_Follow = dt_Follow.AddDays(_NoofDays);
                             mskFollowupDate.Text = dt_Follow.ToString("MM/dd/yyyy");

                         }
                     }
                 }

                 if (dsPaymentPlan.Tables[2] != null)
                 {
                     if (dsPaymentPlan.Tables[2].Rows.Count > 0)
                     {
                         if (dsPaymentPlan.Tables[2].Rows[0]["sPaymentPlanAction"] != DBNull.Value)
                         {

                             _ActionCode = Convert.ToString(dsPaymentPlan.Tables[2].Rows[0]["sPaymentPlanAction"]);
                             cmbFollowupAction.SelectedValue = _ActionCode;
                         }
                     }
                 }
             }
        }

        private Boolean ValidatePaymentPlan()
        {
            bool _inValid = true;
            decimal CheckAmount = 0;
            mskFollowupDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string strDate = mskFollowupDate.Text;
            mskFollowupDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            DialogResult _dlgInsurance = DialogResult.None;

             if (txtPlanAmount.Text != "")
            {
                try
                { 
                    CheckAmount = Convert.ToDecimal(txtPlanAmount.Text);
                }
                catch
                { CheckAmount = 0; }

            }
            else
             { 
                 CheckAmount = Convert.ToDecimal(0.1); 
             }

            //if (CheckAmount <= 0)
            //{
            //    MessageBox.Show("Payment plan amount should be greater than zero.  ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtPlanAmount.Focus();
            //    _inValid = false;
            //}
            //else 
            if (cmbFollowupAction.SelectedIndex > 0 && strDate.Length == 0)
            {
                MessageBox.Show("Please enter follow-up date.  ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskFollowupDate.Focus();
                mskFollowupDate.Select();
                _inValid = false;
            }
            else if (strDate.Length > 0 && cmbFollowupAction.SelectedIndex <= 0)
            {
                if (isValidDate())
                {
                        MessageBox.Show("Please select follow-up action.  ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbFollowupAction.Focus();
                        _inValid = false;
                }
                else
                {
                    _inValid = false;
                }
            }
            else if (strDate.Length > 0)
            {
                if (isValidDate())
                {
                    if (CL_FollowUpCode.IsScheduleDateToFarinFuture(mskFollowupDate.Text))
                    {
                        _dlgInsurance = MessageBox.Show("Are you sure you want to schedule this action that far in the future: " + mskFollowupDate.Text + " ?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (_dlgInsurance == DialogResult.Cancel)
                        {
                            _inValid = false;
                            mskFollowupDate.Focus();
                            mskFollowupDate.Select();
                        }
                        else
                        {
                            _inValid = true;
                        }
                    }
                }
                else
                {
                    _inValid = false;
                    mskFollowupDate.Focus();
                    mskFollowupDate.Select();
                }
            }



            return _inValid;
        }

        private Boolean isValidDate()
        {

            mskFollowupDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string strDate = mskFollowupDate.Text;
            mskFollowupDate.TextMaskFormat = MaskFormat.IncludeLiterals;
            _IsValidDate = true;
            
            if (mskFollowupDate != null)
            {
                if (strDate.Length > 0)
                {
                    if (gloDateMaster.gloDate.IsValidDateV2(mskFollowupDate.Text.Trim()) == false)
                    {
                        MessageBox.Show("Please enter a valid date.  ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _IsValidDate = false;

                    }

                }
            }
            return _IsValidDate;
        }

        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g!=null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            
            return width;
        }

        void ShowTooltipOnComboBox(object sender, DrawItemEventArgs e)
        {

            combo = (ComboBox)sender;
            if (combo.Items.Count > 0 && e.Index >= 0)
            {

                e.DrawBackground();
                using (SolidBrush br = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(combo.GetItemText(combo.Items[e.Index]).ToString(), e.Font, br, e.Bounds);
                }

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    if (combo.DroppedDown)
                    {
                        string txt = combo.GetItemText(combo.Items[e.Index]).ToString();


                        if (getWidthofListItems(combo.GetItemText(combo.Items[e.Index]).ToString(), combo) >= combo.DropDownWidth - 20)
                        {
                            if (toolTip1.GetToolTip(combo) != txt)
                            {
                                this.toolTip1.Show(combo.GetItemText(combo.Items[e.Index]), combo, e.Bounds.Right - 180, e.Bounds.Bottom);
                            }
                        }
                        else
                        {
                            this.toolTip1.SetToolTip(combo, "");
                        }
                    }
                    else
                    {
                        this.toolTip1.Hide(combo);
                    }
                }
                else
                {
                    //this.tooltip_Billing.SetToolTip(combo,"");
                }
                e.DrawFocusRectangle();
            }
        }
        #endregion

        #region "Form Controls Events"

        private void txtPlanAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                #region " Allow decimal amount only "

                bool hasDecimal = false;
                decimal CheckAmount = 0;
                if (e.KeyChar == (char)46)
                {
                    if (txtPlanAmount.Text.Contains(Convert.ToString(e.KeyChar)))
                    { hasDecimal = true; }
                    else
                    { hasDecimal = false; }
                }

                if (hasDecimal == true)
                {
                    if (!Char.IsDigit(e.KeyChar) & e.KeyChar != (char)8)
                    { e.Handled = true; }
                }
                else
                {
                    if (!Char.IsDigit(e.KeyChar) & e.KeyChar != (char)46 & e.KeyChar != (char)8)
                    { e.Handled = true; }
                }

              

                #endregion

                if (e.KeyChar == Convert.ToChar(13))
                {
                    if (txtPlanAmount.Text != "")
                    {
                        try
                        { CheckAmount = Convert.ToDecimal(txtPlanAmount.Text); }
                        catch
                        { CheckAmount = 0; }

                    }
                    else
                    { CheckAmount = 0; }

                    if (txtPlanAmount.Text != "")
                    {
                        txtPlanAmount.Text = CheckAmount.ToString("#0.00");
                    }

                }

                if (Char.IsControl(e.KeyChar))
                {
                //
                }
                else if (Char.IsNumber(e.KeyChar) || e.KeyChar == '.')
                {
                    TextBox tb = sender as TextBox;
                    int cursorPosLeft = tb.SelectionStart;
                    int cursorPosRight = tb.SelectionStart + tb.SelectionLength;
                    string result = tb.Text.Substring(0, cursorPosLeft) + e.KeyChar + tb.Text.Substring(cursorPosRight);
                    string[] parts = result.Split('.');
                    if (parts.Length > 1)
                    {
                        if (parts[1].Length > 2 || parts.Length > 2)
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            catch (System.OverflowException ex)
            {
                MessageBox.Show("Amount is invalid, Please enter a valid amount", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                return;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void txtPlanAmount_Leave(object sender, EventArgs e)
        {
            decimal CheckAmount = 0;
            if (txtPlanAmount.Text != "")
            {
                try
                { CheckAmount = Convert.ToDecimal(txtPlanAmount.Text); }
                catch
                { CheckAmount = 0; }

            }
            else
            { CheckAmount = 0; }

            if (txtPlanAmount.Text != "")
            {
                txtPlanAmount.Text = CheckAmount.ToString("#0.00");
            }


        }

        private void mskFollowupDate_Validating(object sender, CancelEventArgs e)
        {
            isValidDate();
        }

        private void mskFollowupDate_MouseClick(object sender, MouseEventArgs e)
        {

            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

        }

        private void cmbFollowupAction_MouseEnter(object sender, EventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbFollowupAction.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbFollowupAction.Items[cmbFollowupAction.SelectedIndex])["sFollowUpAction"]), cmbFollowupAction) >= cmbFollowupAction.DropDownWidth - 20)
                {
                    string txt = Convert.ToString(((DataRowView)cmbFollowupAction.Items[cmbFollowupAction.SelectedIndex])["sFollowUpAction"]);
                    if (toolTip1.GetToolTip(cmbFollowupAction) != txt)
                    {
                        toolTip1.SetToolTip(cmbFollowupAction, txt);
                    }
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbFollowupAction, "");
                }

            }
        }

        private void cmbFollowupAction_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void cmbFollowupAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo = (ComboBox)sender;
            if (cmbFollowupAction.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbFollowupAction.Items[cmbFollowupAction.SelectedIndex])["sFollowUpAction"]), cmbFollowupAction) >= cmbFollowupAction.DropDownWidth - 20)
                {
                    string txt = Convert.ToString(((DataRowView)cmbFollowupAction.Items[cmbFollowupAction.SelectedIndex])["sFollowUpAction"]);
                    if (toolTip1.GetToolTip(cmbFollowupAction) != txt)
                    {
                        toolTip1.SetToolTip(cmbFollowupAction, txt);
                    }
                }
                else
                {
                    this.toolTip1.SetToolTip(cmbFollowupAction, "");
                }

            }

        }

        #endregion

        #region "Browse Quick Notes"
        private void btnBrowseNotes_Click(object sender, EventArgs e)
        {
            frmQuickNotes ofrmQuickNotes = null;
            try
            {

                ofrmQuickNotes = new frmQuickNotes(QuickNoteType.AccountInternal.GetHashCode());
                ofrmQuickNotes.ShowDialog(this);
                if (txtNotes.Text != "")
                    txtNotes.Text = txtNotes.Text + " " + ofrmQuickNotes.Note;
                else
                    txtNotes.Text = ofrmQuickNotes.Note;

                const int MaxChars = 255;
                if (txtNotes.Text.Length > MaxChars)
                    txtNotes.Text = txtNotes.Text.Substring(0, MaxChars);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (ofrmQuickNotes != null)
                {
                    ofrmQuickNotes.Dispose(); ofrmQuickNotes = null;
                }
            }
        }

        private void btnMouseHover(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongYellow;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = global::gloBilling.Properties.Resources.Img_LongButton;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

       
    }
}
