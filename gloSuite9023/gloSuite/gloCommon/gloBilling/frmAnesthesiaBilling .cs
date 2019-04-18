using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;

using gloBilling.Common;

namespace gloBilling
{
    public partial class frmAnesthesiaBilling : Form
    {


        #region "Variable declaration"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public TransactionLine oTransLine = new TransactionLine();
        public Boolean oDialogResult = false;
        private string _messageBoxCaption = "";
        string strBaseUnit = "";
        string strTimeUnit = "";
        string strOtherUnit = "";

        #endregion

        #region "Properties""

        public Boolean bIsVoid
        {
            get;
            set;
        }
        public Int64 nContactID
        {
            get;
            set;
        }

        public enum DateValidationType
        {
            StartDate = 0,
            EndDate = 1,
        }

        #endregion

        #region " Constructor "

        public frmAnesthesiaBilling()
        {
            InitializeComponent();
        }

        public frmAnesthesiaBilling(TransactionLine _oTransLine)
        {
            InitializeComponent();
            oTransLine = _oTransLine;

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

        #region "Form Load "

        private void frmAnesthesiaBilling_Load(object sender, EventArgs e)
        {
            try
            {
                DetachMaskBoxEvents();
                this.mskStartTime.TextChanged -= new System.EventHandler(this.mskStartTime_TextChanged);
                this.mskEndTime.TextChanged -= new System.EventHandler(this.mskEndTime_TextChanged);
                DateTime _dtDos= Convert.ToDateTime(oTransLine.DateServiceFrom);
                if (bIsVoid == true)
                    tlb_SaveCls.Enabled = false;

                if (oTransLine != null)
                {
                    lblCPTCodeText.Text = oTransLine.CPTCode;
                    lblCPTDescText.Text = oTransLine.CPTDescription;
                    lblDOSText.Text = Convert.ToDateTime(oTransLine.DateServiceFrom).ToString("MM/dd/yyyy");
                    lblMod1Text.Text = oTransLine.Mod1Code;
                    lblMod2Text.Text = oTransLine.Mod2Code;

                    if (oTransLine.bIsAneshtesia)
                    {
                      
                        if (oTransLine.AnesthesiaStartTime.Value.ToShortDateString() == "1/1/0001") // To Skip the Default Value after save and close
                        {
                            mskStartDate.Text = "";
                            mskEndDate.Text = "";
                            mskStartTime.Text = "";
                            mskEndTime.Text = "";
                            
                        }
                        else
                        {
                            mskStartDate.Text = oTransLine.AnesthesiaStartTime.Value.ToString("MM/dd/yyyy");
                            mskEndDate.Text = oTransLine.AnesthesiaEndTime.Value.ToString("MM/dd/yyyy");
                            mskStartTime.Text = oTransLine.AnesthesiaStartTime.Value.ToString("HHmm");
                            mskEndTime.Text = oTransLine.AnesthesiaEndTime.Value.ToString("HHmm");
                            txtStartTime.Text = FormatTime(mskStartTime.Text);
                            txtEndTime.Text = FormatTime(mskEndTime.Text);
                        }
                        chkAutoCalculate.Checked = Convert.ToBoolean(oTransLine.bIsAutoCalculateAnesthesia);
                        txtTotalMinutes.Text = Convert.ToString(oTransLine.AnesthesiaTotalMinutes);
                        txtMinPerUnit.Text = Convert.ToString(oTransLine.AnesthesiaMinPerUnit);
                        txtTimeUnits.Text = Convert.ToString(oTransLine.AnesthesiaTimeUnits);
                        txtBaseUnits.Text = Convert.ToString(oTransLine.AnesthesiaBaseUnits);
                        txtOtherUnits.Text = Convert.ToString(oTransLine.AnesthesiaOtherUnits);
                        txtTotalUnits.Text = Convert.ToString(oTransLine.AnesthesiaTotalUnits);

                        //mskStartTime.Text = oTransLine.AnesthesiaStartTime.Value.ToString("HHmm");
                        //mskEndTime.Text = oTransLine.AnesthesiaEndTime.Value.ToString("HHmm");

                    }
                    else
                    {
                     
                        mskStartDate.Text = _dtDos.ToString("MM/dd/yyyy");
                        mskEndDate.Text = _dtDos.ToString("MM/dd/yyyy");
                        mskStartTime.Text = DateTime.Now.ToString("HHmm");
                        mskEndTime.Text = DateTime.Now.ToString("HHmm");
                        txtStartTime.Text = FormatTime(mskStartTime.Text);
                        txtEndTime.Text = FormatTime(mskEndTime.Text);
                        txtTotalMinutes.Text = "0";
                        txtTimeUnits.Text  = "0";
                        txtTotalUnits.Text = "0";
                        
                        CPT oCPT = new CPT(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                        Decimal DefaultBaseUnit = oCPT.getCPTDefaultBaseUnit(oTransLine.CPTCode);
                        txtBaseUnits.Text = ( DefaultBaseUnit.ToString("#############0.##"));
                        if (nContactID != 0)  // Removed commented if and else block because it is throwing exception for self charge
                        {
                            Decimal DefaultMinPerUnit = oCPT.getCPTDefaultMinPerUnit(nContactID);
                            txtMinPerUnit.Text = ( DefaultMinPerUnit.ToString("#############0.##"));
                        }
                        else
                        {
                            txtMinPerUnit.Text = "0";
                        }
                        oCPT.Dispose();

                        //txtMinPerUnit.Text = Convert.ToString(Convert.ToInt64(0)));
                        
                    }
                }

                Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
                Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
                tom.SetTabOrder(scheme);
                mskStartTime.Focus();
                mskStartTime.Select();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                
                AttachMaskBoxEvents();
                this.mskStartTime.TextChanged += new System.EventHandler(this.mskStartTime_TextChanged);
                this.mskEndTime.TextChanged += new System.EventHandler(this.mskEndTime_TextChanged);
            }
        }

        #endregion

        #region "Form Events"

        private void tlb_SaveCls_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime? dtstartDate;
                dtstartDate = null;

                DateTime? dtEndDate;
                dtEndDate = null;
                DetachMaskBoxEvents();
                if (ValidateDataBeforeSave())
                {
                    if (chkAutoCalculate.Checked)
                    {
                        CalculateTotalMinutes();
                        oTransLine.bIsAutoCalculateAnesthesia = true;
                    }
                    else
                    {
                        oTransLine.bIsAutoCalculateAnesthesia = false;
                    }
                    if (txtTotalMinutes.Text.Trim() != "" && Convert.ToInt64(txtTotalMinutes.Text.Trim())>0)
                    {
         
                        bool bHasdate = false;
                        mskStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        if (mskStartDate.Text.Trim() != "")
                        {
                            bHasdate = true;
                        }
                        mskStartDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

                        if (bHasdate)
                        {
                            dtstartDate = ConcatDateAndTime(mskStartDate.Text, mskStartTime.Text);
                            dtEndDate = ConcatDateAndTime(mskEndDate.Text, mskEndTime.Text);
                        }
                        

                        oTransLine.bIsAneshtesia = true;
                        oTransLine.AnesthesiaID = oTransLine.AnesthesiaID;
                        oTransLine.AnesthesiaStartTime = dtstartDate;
                        oTransLine.AnesthesiaEndTime = dtEndDate;
                        oTransLine.AnesthesiaTotalMinutes = Convert.ToInt64(txtTotalMinutes.Text);

                        if (txtMinPerUnit.Text.Trim().Length > 0)
                            oTransLine.AnesthesiaMinPerUnit = Convert.ToInt64(txtMinPerUnit.Text);
                        else
                            oTransLine.AnesthesiaMinPerUnit = 0;


                        if (txtTimeUnits.Text.Trim().Length > 0)
                            oTransLine.AnesthesiaTimeUnits = Convert.ToDecimal(txtTimeUnits.Text);
                        else
                            oTransLine.AnesthesiaTimeUnits = 0;


                        if (txtBaseUnits.Text.Trim().Length > 0)
                            oTransLine.AnesthesiaBaseUnits = Convert.ToDecimal(txtBaseUnits.Text);
                        else
                            oTransLine.AnesthesiaBaseUnits = 0;


                        if (txtOtherUnits.Text.Trim().Length > 0)
                            oTransLine.AnesthesiaOtherUnits = Convert.ToDecimal(txtOtherUnits.Text);
                        else
                            oTransLine.AnesthesiaOtherUnits = 0;


                        if (txtTotalUnits.Text.Trim().Length > 0)
                            oTransLine.AnesthesiaTotalUnits = Convert.ToDecimal(txtTotalUnits.Text);
                        else
                            oTransLine.AnesthesiaTotalUnits = 0;

                    }
                    else
                    {
                        oTransLine.bIsAneshtesia = false;
                        oTransLine.AnesthesiaID = oTransLine.AnesthesiaID;
                        if (mskStartDate.Text.Trim() == "")
                        {
                            oTransLine.AnesthesiaStartTime = null;
                            oTransLine.AnesthesiaEndTime = null;
                        }
                        else
                        {
                            oTransLine.AnesthesiaStartTime = oTransLine.DateServiceFrom;
                            oTransLine.AnesthesiaEndTime = oTransLine.DateServiceFrom;
                        }
                        oTransLine.bIsAutoCalculateAnesthesia = false;
                        oTransLine.AnesthesiaTotalMinutes = 0;
                        oTransLine.AnesthesiaMinPerUnit = 0;
                        oTransLine.AnesthesiaTimeUnits = 0;
                        oTransLine.AnesthesiaBaseUnits = 0;
                        oTransLine.AnesthesiaOtherUnits = 0;
                        oTransLine.AnesthesiaTotalUnits = 0;

                    }
                    oDialogResult = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                AttachMaskBoxEvents();
            }
        }

        private void tlb_Close_Click(object sender, EventArgs e)
        {
            try
            {
                oDialogResult = false;
                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        #endregion

        #region "Form Control Events"


        private void mskStartDate_Validating(object sender, CancelEventArgs e)
        {
            mskStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (mskStartDate.Text.Trim() != string.Empty)
             {
                 if (IsValidDate(DateValidationType.StartDate))
                 {
                     if (chkAutoCalculate.Checked)
                     {
                         CalculateTotalMinutes();
                     }
                 }
                 else
                 {
                     if (chkAutoCalculate.Checked)
                     {
                         txtTotalMinutes.Text = "";
                         txtTimeUnits.Text = "";
                     }
                 }
             }
             else
             {
                 if (chkAutoCalculate.Checked)
                 {
                     txtTotalMinutes.Text = "";
                     txtTimeUnits.Text = "";
                 }
             }
            mskStartDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void mskStartTime_Validating(object sender, CancelEventArgs e)
        {
            if (ValidateTime(mskStartTime, e))
            {
                if (IsValidDate(DateValidationType.StartDate))
                {
                    if (chkAutoCalculate.Checked)
                    {
                        CalculateTotalMinutes();
                    }
                }
                else
                {
                    if (chkAutoCalculate.Checked)
                    {
                        txtTotalMinutes.Text = "";
                        txtTimeUnits.Text = "";
                    }
                    e.Cancel = true;
                }
            }
        }

        private void mskEndDate_Validating(object sender, CancelEventArgs e)
        {
            mskEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (mskEndDate.Text.Trim() != string.Empty)
            {
                if (IsValidDate(DateValidationType.EndDate))
                {
                    if (chkAutoCalculate.Checked)
                    CalculateTotalMinutes();
                }
                else
                {
                    if (chkAutoCalculate.Checked)
                    {
                        txtTotalMinutes.Text = "";
                        txtTimeUnits.Text = "";
                    }
                }
            }
            else
            {
                if (chkAutoCalculate.Checked)
                {
                    txtTotalMinutes.Text = "";
                    txtTimeUnits.Text = "";
                }
            }
            mskEndDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }


        private void mskEndTime_Validating(object sender, CancelEventArgs e)
        {
            if (ValidateTime(mskEndTime, e))
            {
                if (IsValidDate(DateValidationType.EndDate))
                {
                    if(chkAutoCalculate.Checked)
                    CalculateTotalMinutes();
                }
                else
                {
                    if (chkAutoCalculate.Checked)
                    {
                        txtTotalMinutes.Text = "";
                        txtTimeUnits.Text = "";
                    }
                    e.Cancel = true;
                }
            }

        }

        private void txtMinPerUnit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutoCalculate.Checked)
                {
                    if (txtTotalMinutes.Text != "" && txtMinPerUnit.Text != "")
                    {
                        if (ValidateInteger(txtMinPerUnit))
                        {
                            Int64 _totalMin = Convert.ToInt64(txtTotalMinutes.Text);
                            Int64 _minpermin = Convert.ToInt64(txtMinPerUnit.Text);
                            if (_minpermin != 0)
                            {
                                Decimal TimeUnits = Convert.ToDecimal(Convert.ToDecimal(_totalMin) / Convert.ToDecimal(_minpermin));
                                txtTimeUnits.Text = TimeUnits.ToString("#############0.##");
                            }
                            else
                            {
                                txtTimeUnits.Text = 0.ToString();
                            }

                            CalculateTotalUnits();
                        }
                        else
                        {
                            txtMinPerUnit.Text = "";
                            txtMinPerUnit.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
              gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        private void txtBaseUnits_TextChanged(object sender, EventArgs e)
        {

            CalculateTotalUnits();
        }

        private void txtOtherUnits_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalUnits();
        }
        private void txtTimeUnits_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalUnits();
        }

        private void txtMinPerUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        private void txtTimeUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                strTimeUnit = txtTimeUnits.Text.Trim();
                //To Validate the Decimal Places
                if (((txtTimeUnits.Text.Contains(".")) && (e.KeyChar == 46)))
                {
                    e.Handled = true;
                }
                else
                {
                    if (txtTimeUnits.Text.Trim().Length >= 3 && !txtTimeUnits.Text.Trim().Contains(".") && e.KeyChar != '\b' && (e.KeyChar != 46))
                    {
                        e.Handled = true;
                    }
                    //else if (txtTimeUnits.Text.Trim().Contains(".") && txtTimeUnits.Text.Trim().Substring(txtTimeUnits.Text.IndexOf(".") + 1).Length >= 2 && e.KeyChar != '\b')
                    //{
                    //    e.Handled = true;
                    //}
                    else
                    {
                        e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 46;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
        private void txtBaseUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                strBaseUnit=txtBaseUnits.Text.Trim();
                //To Validate the Decimal Places
                if (((txtBaseUnits.Text.Contains(".")) && (e.KeyChar == 46)))
                {
                    e.Handled = true;
                }
                else
                {
                    if (txtBaseUnits.Text.Trim().Length >= 3 && !txtBaseUnits.Text.Trim().Contains(".") && e.KeyChar != '\b' && (e.KeyChar != 46))
                    {
                        e.Handled = true;
                    }
                    //else if (txtBaseUnits.Text.Trim().Contains(".") && txtBaseUnits.Text.Trim().Substring(txtBaseUnits.Text.IndexOf(".") + 1).Length >= 2 && e.KeyChar != '\b')
                    //{
                    //    e.Handled = true;
                    //}
                    else
                    {
                        e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 46;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }

        }

        private void txtBaseUnits_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtBaseUnits.Text.Trim().Contains("."))
            {
                string[] arr = txtBaseUnits.Text.Trim().Split('.');
                if (arr[0].Length > 3 || arr[1].Length > 2)
                {
                    txtBaseUnits.Text = strBaseUnit;
                }
            }
        }

        private void txtTimeUnits_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTimeUnits.Text.Trim().Contains("."))
            {
                string[] arr = txtTimeUnits.Text.Trim().Split('.');
                if (arr[0].Length > 3 || arr[1].Length > 2)
                {
                    txtTimeUnits.Text = strTimeUnit;
                }
            }
        }

        private void txtOtherUnits_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtOtherUnits.Text.Trim().Contains("."))
            {
                string[] arr = txtOtherUnits.Text.Trim().Split('.');
                if (arr[0].Length > 3 || arr[1].Length > 2)
                {
                    txtOtherUnits.Text = strOtherUnit;
                }
            }
        }


        private void txtOtherUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                strOtherUnit = txtOtherUnits.Text.Trim();
                //To Validate the Decimal Places
                if (((txtOtherUnits.Text.Contains(".")) && (e.KeyChar == 46)))
                {
                    e.Handled = true;
                }
                else
                {
                    if (txtOtherUnits.Text.Trim().Length >= 3 && !txtOtherUnits.Text.Trim().Contains(".") &&  e.KeyChar != '\b' && (e.KeyChar != 46))
                    {
                        e.Handled = true;
                    }
                    //else if (txtOtherUnits.Text.Trim().Contains(".") && txtOtherUnits.Text.Trim().Substring(txtOtherUnits.Text.IndexOf(".") + 1).Length >= 2 && e.KeyChar != '\b')
                    //{
                    //    e.Handled = true;
                    //}
                    else
                    {
                        e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 46;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);

            }
        }
        
        private void mskStartTime_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private void mskEndTime_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

        }

        private void mskStartDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

        }

        private void mskEndDate_MouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

        }

        private void txtTotalMinutes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

      


        private void chkAutoCalculate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoCalculate.Checked)
            {
                CalculateTotalMinutes();
                CalculateTotalUnits();
                txtTotalMinutes.ReadOnly = true;
                txtTimeUnits.ReadOnly = true;
            }
            else
            {
                txtTotalMinutes.ReadOnly = false;
                txtTimeUnits.ReadOnly = false;
            }
        }

        private void txtTotalMinutes_MouseUp(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTotalMinutes.Text))
            {
                if (!ValidateInteger(txtTotalMinutes))
                {
                    txtTotalMinutes.Text = "";
                    txtTotalMinutes.Focus();
                    return;
                }
            }
        }

        private void txtMinPerUnit_MouseUp(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMinPerUnit.Text))
            {
                if (!ValidateInteger(txtMinPerUnit))
                {
                    txtTotalMinutes.Text = "";
                    txtTotalMinutes.Focus();
                    return;
                }
            }
        }

        private void txtTotalMinutes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                    case Keys.P:
                    case Keys.X:
                        e.Handled = true;
                        txtTotalMinutes.SelectionLength = 0;
                        break;
                }
            }

        }

        private void txtMinPerUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                    case Keys.P:
                    case Keys.X:
                        e.Handled = true;
                        txtMinPerUnit.SelectionLength = 0;
                        break;
                }
            }

        }

        private void txtTimeUnits_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                    case Keys.P:
                    case Keys.X:
                        e.Handled = true;
                        txtTimeUnits.SelectionLength = 0;
                        break;
                }
            }

        }

        private void txtBaseUnits_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                    case Keys.P:
                    case Keys.X:
                        e.Handled = true;
                        txtBaseUnits.SelectionLength = 0;
                        break;
                }
            }

        }

        private void txtOtherUnits_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                    case Keys.P:
                    case Keys.X:
                        e.Handled = true;
                        txtOtherUnits.SelectionLength = 0;
                        break;
                }
            }

        }

        private void mskStartTime_TextChanged(object sender, EventArgs e)
        {
            if (mskStartTime.MaskCompleted)
            {
                if (mskStartTime.Text.Trim() != "" && mskStartTime.Text.Trim().Length == 4)
                {
                    if (Convert.ToInt64(mskStartTime.Text.Trim()) <= 2359)
                    {
                        if (Convert.ToInt32(mskStartTime.Text.Trim().Substring(2)) <= 59)
                        {
                            txtStartTime.Text = FormatTime(mskStartTime.Text);
                        }
                        else
                        {
                            txtStartTime.Text = "";
                        }

                    }
                    else
                    {
                        txtStartTime.Text = "";
                    }

                }
                else
                {
                    txtStartTime.Text = "";
                }
            }
            else
            {
                txtStartTime.Text = "";
            }
        }

        private void mskEndTime_TextChanged(object sender, EventArgs e)
        {
            if (mskEndTime.MaskCompleted)
            {
                if (mskEndTime.Text.Trim() != "" && mskEndTime.Text.Trim().Length == 4)
                {
                    if (Convert.ToInt64(mskEndTime.Text.Trim()) <= 2359)
                    {
                        if (Convert.ToInt32(mskEndTime.Text.Trim().Substring(2)) <= 59)
                        {
                            txtEndTime.Text = FormatTime(mskEndTime.Text);
                        }
                        else
                        {
                            txtEndTime.Text = "";
                        }

                    }
                    else
                    {
                        txtEndTime.Text = "";
                    }

                }
                else
                {
                    txtEndTime.Text = "";
                }
            }
            else
            {
                txtEndTime.Text = "";
            }
        }

        #endregion

        #region " Public & Private Methods "

        private string FormatTime(string strMilitarytime)
        {
            DateTime dt;
            dt = DateTime.ParseExact(strMilitarytime, "HHmm", null);
            String sTheTimeFormatYouWant = String.Empty;
            sTheTimeFormatYouWant = dt.ToString("hh:mm tt");
            return sTheTimeFormatYouWant;
        }

        private void CalculateTotalUnits()
        {
            try
            {
                Decimal _TimeUnits = 0;
                Decimal _BaseUnits = 0;
                Decimal _otherUnits = 0;

                if (txtTimeUnits.Text.Trim() != "" && txtTimeUnits.Text.Trim() != ".")
                    _TimeUnits = Convert.ToDecimal(txtTimeUnits.Text);

                if (txtBaseUnits.Text.Trim() != "" && txtBaseUnits.Text.Trim() != ".")
                _BaseUnits = Convert.ToDecimal(txtBaseUnits.Text);

                if (txtOtherUnits.Text.Trim() != "" && txtOtherUnits.Text.Trim() != ".")
                    _otherUnits = Convert.ToDecimal(txtOtherUnits.Text);


                Decimal _TotalUnits = _TimeUnits + _BaseUnits + _otherUnits;

                txtTotalUnits.Text = _TotalUnits.ToString("#############0.##");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        private void CalculateTotalMinutes()
        {
            try
            {
                if (chkAutoCalculate.Checked == true)
                {
                    //TimeSpan TStartspan = ParseTime(dtpStartTime.Text);
                    //string sStarttime = TStartspan.ToString();
                    //string sStartdate = mskStartDate.Text + " " + sStarttime;
                    //DateTime dtstartDate = DateTime.Parse(sStartdate);


                    //TimeSpan TEndspan = ParseTime(dtp_EndTime.Text);
                    //string sEndtime = TEndspan.ToString();
                    //string sEnddate = mskEndDate.Text + " " + sEndtime;
                    //DateTime dtEndDate = DateTime.Parse(sEnddate);
                    mskStartTime.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    mskEndTime.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    mskStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    mskEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    if (mskStartDate.Text != "" && mskEndDate.Text != "" && mskStartTime.Text != "" && mskEndTime.Text != "")
                    {
                        mskStartTime.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        mskEndTime.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        mskStartDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        mskEndDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        DateTime dtstartDate = ConcatDateAndTime(mskStartDate.Text, mskStartTime.Text);
                        DateTime dtEndDate = ConcatDateAndTime(mskEndDate.Text, mskEndTime.Text);

                        TimeSpan ts = new TimeSpan();
                        ts = dtEndDate.Subtract(dtstartDate);
                        if (ts.TotalMinutes >= 0)
                        {
                            txtTotalMinutes.Text = Convert.ToInt64(ts.TotalMinutes).ToString();
                        }
                        else
                        {
                            txtTotalMinutes.Text = "";
                        }
                    }
                    else
                    {
                        mskStartTime.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        mskEndTime.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        mskStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        mskEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        txtTotalMinutes.Text = "";
                        //txtMinPerUnit.Text = "";
                    }
                    

                    if (txtTotalMinutes.Text != "" && txtMinPerUnit.Text != "")
                    {
                        Int64 _totalMin = Convert.ToInt64(txtTotalMinutes.Text);
                        Int64 _minpermin = Convert.ToInt64(txtMinPerUnit.Text);
                        if (_minpermin != 0)
                        {
                            Decimal TimeUnits = Convert.ToDecimal(_totalMin) / Convert.ToDecimal(_minpermin);
                            txtTimeUnits.Text = TimeUnits.ToString("#############0.##");
                        }
                        else
                        {
                            txtTimeUnits.Text = 0.ToString();
                        }

                        CalculateTotalUnits();
                    }
                    else
                    {
                        txtTimeUnits.Text = "0";
                        //txtTimeUnits.Text = "";
                        //txtBaseUnits.Text = "";
                        //txtOtherUnits.Text = "";
                        //txtTotalUnits.Text = "";
                    }
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

        }

        public static TimeSpan ParseTime(string input)
        {

            CultureInfo cultureInfo = new CultureInfo("en-US");
            DateTime parsedDateTime=DateTime.Now;

            try
            {
                if (!DateTime.TryParseExact(input, "HH:mm", cultureInfo, DateTimeStyles.None, out parsedDateTime))
                {
                    int parsedInt32;
                    if (!int.TryParse(input, NumberStyles.None, cultureInfo, out parsedInt32))
                    {
                        throw new ArgumentException("Unable to parse input value as time in any of the accepted formats.", "input");
                    }

                    int remainder;
                    int quotient = Math.DivRem(parsedInt32, 100, out remainder);
                    return new TimeSpan(quotient, remainder, 0);
                }
            }
            catch (Exception ex)
            {
              gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
             
            }
            return parsedDateTime.TimeOfDay;
        }

        private Boolean ValidateTime(MaskedTextBox mskTime, CancelEventArgs e)
        {
           
            bool _isValidTime = true;
            try
            {

                //DetachMaskBoxEvents();

                mskTime.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string value = mskTime.Text.Replace(":", "").Trim();
                mskTime.TextMaskFormat = MaskFormat.IncludeLiterals;

                //Allow for an empty value (your choice)
                if (value.Length == 0)
                {
                    if(chkAutoCalculate.Checked)
                    txtTotalMinutes.Text = "";
                    return _isValidTime = true;
                }
                if (value.Length == 4)
                {
                    TimeSpan TEndspanTemp = ParseTime(mskTime.Text);
                    string[] parts = TEndspanTemp.ToString().Split(':');

                    if (parts.Length != 3)
                    {
                        MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isValidTime = false;
                        mskTime.Focus();
                        return false;
                    }
                    else
                    {
                        for (Int32 i = 0; i <= parts.Length - 1; i++)
                        {
                            Int32 intValue = default(Int32);
                            if (Int32.TryParse(parts[i], out intValue))
                            {
                                switch (i)
                                {
                                    case 0:
                                        if(e!=null)
                                        e.Cancel = !(intValue <= 23 && intValue >= 0);
                                        break;
                                    case 1:
                                    case 2:
                                        if (e != null)
                                        e.Cancel = !(intValue <= 59 && intValue >= 0);
                                        break;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _isValidTime = false;
                                mskTime.Focus();
                                return false;
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isValidTime = false;
                    mskTime.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                //AttachMaskBoxEvents();
            }
            return _isValidTime;

        }
        private bool ValidateDate(object strDate)
        {
            //DetachMaskBoxEvents();
            bool Success;
            try
            {
                DateTime validatedDate;
                Success = DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);
                if (validatedDate != null && Success == true)
                {
                    if (validatedDate < DateTime.MaxValue && validatedDate >= Convert.ToDateTime("01/01/1900"))
                    {
                        Success = true;
                    }
                    else
                    {
                        Success = false;
                    }

                }
            }
            catch (FormatException)
            {
                Success = false; // If this line is reached, an exception was thrown
                //AttachMaskBoxEvents();
            }
            return Success;
        }
        private Boolean IsValidDate(DateValidationType sType)
        {
           
            bool Success;
            try
            {
                //DetachMaskBoxEvents();
                mskStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskStartTime.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskEndTime.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                if (mskStartDate.Text.Trim() != "" && mskEndDate.Text.Trim() != "" && mskStartTime.Text.Trim() != "" && mskEndTime.Text.Trim() != "")
                {
                    mskStartDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    mskEndDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    mskStartTime.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    mskEndTime.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;

                    if (!mskStartTime.MaskCompleted)
                    {
                        MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskStartTime.Focus();
                        mskStartTime.Select();
                        return false;
                    }


                    if (Convert.ToInt64(mskStartTime.Text.Trim()) > 2359)
                    {
                        MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskStartTime.Focus();
                        mskStartTime.Select();
                        return false;
                    }
                    if (mskStartTime.MaskCompleted)
                    {
                        if (Convert.ToInt32(mskStartTime.Text.Trim().Substring(2)) > 59)
                        {
                            MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskStartTime.Focus();
                            mskStartTime.Select();
                            return false;
                        }
                    }
                    if (!mskStartDate.MaskCompleted)
                    {
                        MessageBox.Show("Enter valid start Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskStartDate.Focus();
                        mskStartDate.Select();
                        return false;
                    }
                    if (!ValidateDate(mskStartDate.Text.Trim()))
                    {
                        MessageBox.Show("Enter valid start Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskStartDate.Focus();
                        mskStartDate.Select();
                        return false;
                    }
                    if (!mskEndTime.MaskCompleted)
                    {
                        MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskEndTime.Focus();
                        mskEndTime.Select();
                        return false;
                    }
                    if (mskEndTime.MaskCompleted)
                    {
                        if (Convert.ToInt32(mskEndTime.Text.Trim().Substring(2))> 59)
                        {
                            MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskEndTime.Focus();
                            mskEndTime.Select();
                            return false;
                        }
                    }
                    if (Convert.ToInt64(mskEndTime.Text.Trim()) > 2359)
                    {
                        MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskEndTime.Focus();
                        mskEndTime.Select();
                        return false;
                    }
                    if (!mskEndDate.MaskCompleted)
                    {
                        MessageBox.Show("Enter valid end Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskEndDate.Focus();
                        mskEndDate.Select();
                        return false;
                    }
                    if (!ValidateDate(mskEndDate.Text.Trim()))
                    {
                        MessageBox.Show("Enter valid end Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskEndDate.Focus();
                        mskEndDate.Select();
                        return false;
                    }
                    Success = true;
                   
                }
                else
                {
                    mskStartTime.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    if (mskStartTime.Text.Trim() != "")
                    {
                        mskStartTime.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        if (!mskStartTime.MaskCompleted)
                        {
                            MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskStartTime.Focus();
                            mskStartTime.Select();
                            return false;
                        }


                        if (Convert.ToInt64(mskStartTime.Text.Trim()) > 2359)
                        {
                            MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskStartTime.Focus();
                            mskStartTime.Select();
                            return false;
                        }

                        if (mskStartTime.MaskCompleted)
                        {
                            if (Convert.ToInt32(mskStartTime.Text.Trim().Substring(2)) > 59)
                            {
                                MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskStartTime.Focus();
                                mskStartTime.Select();
                                return false;
                            }
                        }
                    }
                    mskStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    if (mskStartDate.Text.Trim() != "")
                    {
                        mskStartDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                        if (!mskStartDate.MaskCompleted)
                        {
                            MessageBox.Show("Enter valid start Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskStartDate.Focus();
                            mskStartDate.Select();
                            return false;
                        }
                        if (!ValidateDate(mskStartDate.Text.Trim()))
                        {
                            MessageBox.Show("Enter valid start Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskStartDate.Focus();
                            mskStartDate.Select();
                            return false;
                        }
                    }
                    mskEndTime.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                     if (mskEndTime.Text.Trim() != "")
                     {
                         mskEndTime.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                         if (!mskEndTime.MaskCompleted)
                         {
                             MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                             mskEndTime.Focus();
                             mskEndTime.Select();
                             return false;
                         }
                         if (Convert.ToInt64(mskEndTime.Text.Trim()) > 2359)
                         {
                             MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                             mskEndTime.Focus();
                             mskEndTime.Select();
                             return false;
                         }

                         if (mskEndTime.MaskCompleted)
                         {
                             if (Convert.ToInt32(mskEndTime.Text.Trim().Substring(2)) > 59)
                             {
                                 MessageBox.Show("Please provide the time in military format.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                 mskEndTime.Focus();
                                 mskEndTime.Select();
                                 return false;
                             }
                         }
                     }
                     mskEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                     if (mskEndDate.Text.Trim() != "")
                     {
                         mskEndDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                         if (!mskEndDate.MaskCompleted)
                         {
                             MessageBox.Show("Enter valid end Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                             mskEndDate.Focus();
                             mskEndDate.Select();
                             return false;
                         }
                         if (!ValidateDate(mskEndDate.Text.Trim()))
                         {
                             MessageBox.Show("Enter valid end Date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                             mskEndDate.Focus();
                             mskEndDate.Select();
                             return false;
                         }
                     }
                     Success = true;
                }
            }
            catch (FormatException)
            {
                Success = false;

            }
            finally
            {
                //AttachMaskBoxEvents();

               
            }
            return Success;
        }

        private bool ValidateDataBeforeSave()
        {
            bool _isValid = true;
            try
            {
                mskStartDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskEndDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskStartTime.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mskEndTime.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;


                if ((mskStartDate.Text != "" || mskEndTime.Text != "") && mskStartTime.Text == "")
                {
                    MessageBox.Show("Enter Start Time. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskStartTime.Focus();
                    mskStartTime.Select();
                    _isValid = false;
                    return _isValid;
                }
                else if (mskStartDate.Text == "" && (mskEndDate.Text != "" || mskStartTime.Text != ""))
                {
                    MessageBox.Show("Enter Start Date. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskStartDate.Focus();
                    mskStartDate.Select();
                    _isValid = false;
                    return _isValid;
                }

                else if ((mskEndDate.Text != "" || mskStartTime.Text != "") && mskEndTime.Text == "")
                {
                    MessageBox.Show("Enter End Time. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskEndTime.Focus();
                    mskEndTime.Select();
                    _isValid = false;
                    return _isValid;
                }
                else if ((mskStartDate.Text != "" || mskEndTime.Text != "") && mskEndDate.Text == "")
                {
                    MessageBox.Show("Enter End Date. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskEndDate.Focus();
                    mskEndDate.Select();
                    _isValid = false;
                    return _isValid;
                }
                else if (!IsValidDate(DateValidationType.StartDate))
                {
                    _isValid = false;
                    return _isValid;
                }
                else if (!IsValidDate(DateValidationType.EndDate))
                {
                    _isValid = false;
                    return _isValid;
                }
                if (txtBaseUnits.Text.Trim() == ".")
                {
                    MessageBox.Show("Enter valid Base Units. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBaseUnits.Focus();
                    _isValid = false;
                    return _isValid;
                }
                if (txtOtherUnits.Text.Trim() == ".")
                {
                    MessageBox.Show("Enter valid Other Units. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOtherUnits.Focus();
                    _isValid = false;
                    return _isValid;
                }
                if (mskStartDate.Text != "")
                {
                    mskStartDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    mskEndDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    DateTime dtstartDate = ConcatDateAndTime(mskStartDate.Text, mskStartTime.Text);
                    DateTime dtEndDate = ConcatDateAndTime(mskEndDate.Text, mskEndTime.Text);

                    if (dtEndDate < dtstartDate)
                    {
                        if (dtEndDate.Date >= dtstartDate.Date)
                        {
                            MessageBox.Show("Start time should be less than end time.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskStartTime.Focus();
                            mskStartTime.Select();
                            _isValid = false;
                            return _isValid;
                        }
                        else
                        {
                            MessageBox.Show("Start date should be less than end date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskStartDate.Focus();
                            mskStartDate.Select();
                            _isValid = false;
                            return _isValid;
                        }
                    }
                }
                
                //else if (!ValidateTime(mskStartTime, null))
                //{
                //    _isValid = false;
                //    return _isValid;
                //}
                //else if (!ValidateTime(mskEndTime, null))
                //{
                //    _isValid = false;
                //    return _isValid;
                //}

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                _isValid = false;
                return _isValid;
            }
            finally
            {
                mskStartDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                mskEndDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                mskStartTime.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                mskEndTime.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }
            return _isValid;
        }

        private DateTime ConcatDateAndTime(string sDate,string sTime)
        {
            DateTime dtAnesthesiaDate = DateTime.Now;
            if (sTime != "")
            {
                TimeSpan TStartspan = ParseTime(sTime);
                string sStarttime = TStartspan.ToString();
                string sStartdate = sDate + " " + sStarttime;
                dtAnesthesiaDate = DateTime.Parse(sStartdate);
            }
           
            return dtAnesthesiaDate;
        }

        private void DetachMaskBoxEvents()
        {
            this.mskStartDate.Validating -= new System.ComponentModel.CancelEventHandler(this.mskStartDate_Validating);
            this.mskStartTime.Validating -= new System.ComponentModel.CancelEventHandler(this.mskStartTime_Validating);
            this.mskEndDate.Validating -= new System.ComponentModel.CancelEventHandler(this.mskEndDate_Validating);
            this.mskEndTime.Validating -= new System.ComponentModel.CancelEventHandler(this.mskEndTime_Validating);
        }

        private void AttachMaskBoxEvents()
        {
            this.mskStartDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskStartDate_Validating);
            this.mskStartTime.Validating += new System.ComponentModel.CancelEventHandler(this.mskStartTime_Validating);
            this.mskEndDate.Validating += new System.ComponentModel.CancelEventHandler(this.mskEndDate_Validating);
            this.mskEndTime.Validating += new System.ComponentModel.CancelEventHandler(this.mskEndTime_Validating);
        }

        public bool ValidateDecimal(TextBox txt)
        {
            decimal dec;
            if (!decimal.TryParse(txt.Text, out dec))
            {
                return false;
            }
            else
                return true;
        }

        public bool ValidateInteger(TextBox txt)
        {
            Int64 dec;
            if (!Int64.TryParse(txt.Text, out dec))
            {
                return false;
            }
            else
                return true;
        }

        #endregion

   




    }
}
