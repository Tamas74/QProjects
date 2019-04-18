using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace gloPrintDialog
{
    public partial class gloExtendedPropertiesControl : UserControl
    {
        //OpenForBatchPrint parameter added for checking whether it was open for batchprinting 
        public gloExtendedPropertiesControl(Boolean OpenfromBatchPrint = false)
        {
            //if (!gloGlobal.gloTSPrint.isCopyPrint)
            {
                InitializeComponent();

                //if open from batch printing then to disable unnecessary controls
                if (OpenfromBatchPrint == true)
                {
                    gbResolution.Enabled = false;
                    gbPrinterMargins.Enabled = false;
                    gbFooterFont.Enabled = false;
                    gbFooterMargins.Enabled = false;
                    gbPageMargin.Enabled = false;
                    gbOverlapActualSize.Enabled = false;
                    gbGutterActualSize.Enabled = false;
                    gbFlowDirection.Enabled = false;
                    gbPageSetting.Enabled = false;
                }
            }
        }
        private Font thisFont = null;
        private Color thisColor = System.Drawing.Color.Black;

        //private void textNumBox_KeyDown1(object sender, KeyEventArgs e)
        //{
        //    //Allow navigation keyboard arrows
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Up:
        //        case Keys.Down:
        //        case Keys.Left:
        //        case Keys.Right:
        //        case Keys.PageUp:
        //        case Keys.PageDown:
        //        case Keys.Delete:
        //            e.SuppressKeyPress = false;
        //            return;
        //        default:
        //            break;
        //    }

        //    //Block non-number characters
        //    char currentKey = (char)e.KeyCode;
        //    bool modifier = e.Control || e.Alt || e.Shift;
        //    bool nonNumber = char.IsLetter(currentKey) ||
        //                     char.IsSymbol(currentKey) ||
        //                     char.IsWhiteSpace(currentKey) ||
        //                     char.IsPunctuation(currentKey);

        //    if (!modifier && nonNumber)
        //        e.SuppressKeyPress = true;

        //    //Handle pasted Text
        //    if (e.Control && e.KeyCode == Keys.V)
        //    {
        //        //Preview paste data (removing non-number characters)
        //        string pasteText = Clipboard.GetText();
        //        string strippedText = "";
        //        for (int i = 0; i < pasteText.Length; i++)
        //        {
        //            if (char.IsDigit(pasteText[i]))
        //                strippedText += pasteText[i].ToString();
        //        }

        //        if (strippedText != pasteText)
        //        {
        //            //There were non-numbers in the pasted text
        //            e.SuppressKeyPress = true;

        //            //OPTIONAL: Manually insert text stripped of non-numbers
        //            TextBox me = (TextBox)sender;
        //            int start = me.SelectionStart;
        //            string newTxt = me.Text;
        //            newTxt = newTxt.Remove(me.SelectionStart, me.SelectionLength); //remove highlighted text
        //            newTxt = newTxt.Insert(me.SelectionStart, strippedText); //paste
        //            me.Text = newTxt;
        //            me.SelectionStart = start + strippedText.Length;
        //        }
        //        else
        //            e.SuppressKeyPress = false;
        //    }
        //}

        public void DisableActualSizeControlsOnDlg()
        {
            this.gbOverlapActualSize.Enabled = false;
            this.rbActualPageSize.Enabled = false;
            this.rbFitToPage.Checked = true;
            this.gbPageSetting.Enabled = false;
            this.gbFlowDirection.Enabled = false;
            this.rbTopToBottom.Checked = false;
            this.rbLeftToRight.Checked = true;
            this.chkPrintOnePage.Checked = true;
            this.chkPrinterLandscape.Checked = true;

        }
        public void DisablelandscapeControlsOnDlg()
        {
            this.gbOverlapActualSize.Enabled = false;
            this.rbActualPageSize.Enabled = false;
            this.rbFitToPage.Checked = true;
            this.gbPageSetting.Enabled = false;
            this.gbFlowDirection.Enabled = false;
            this.rbTopToBottom.Checked = false;
            this.rbLeftToRight.Checked = true;
            this.chkPrintOnePage.Checked = true;
            this.chkPrinterLandscape.Checked = false;

        }
        public void DisablePrintMethod(bool Enabled = false)
        {
            gbPrint.Enabled = Enabled;
        }
        public void StartTimer()
        {
            if (timerToRefresh != null)
            {
                timerToRefresh.Start();
            }
        }
        public void StopTimer()
        {
            if (timerToRefresh != null)
            {
                timerToRefresh.Stop();
            }
        }
        public void DisposeTimer()
        {
            if (timerToRefresh != null)
            {
                timerToRefresh.Dispose();
                timerToRefresh = null;
            }
        }
        public void EnableEitherOverlapOrGutter()
        {
            if (rbActualPageSize.Checked)
            {
                gbOverlapActualSize.Enabled = true;
                chkPrintOnePage.Enabled = true;
                gbGutterActualSize.Enabled = false;
            }
            else
            {
                gbOverlapActualSize.Enabled = false;
                chkPrintOnePage.Enabled = false;
                gbGutterActualSize.Enabled = true;
            }
        }
        public bool IsValidPrinterExtendedParameters(out String ValidationMessage)
        {
            float fltCheck = 0;
            try
            {
                if (gbFooterMargins.Enabled)
                {
                    if (this.txtFooterMarginsTop.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Top Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtFooterMarginsTop.Focus();
                        return false;
                    }

                    if (!float.TryParse(this.txtFooterMarginsTop.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Top Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtFooterMarginsTop.Focus();
                        return false;
                    }

                    if (this.txtFooterMarginsLeft.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Left Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtFooterMarginsLeft.Focus();
                        return false;
                    }
                    if (!float.TryParse(this.txtFooterMarginsLeft.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Left Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtFooterMarginsLeft.Focus();
                        return false;
                    }

                    if (this.txtFooterMarginsRight.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Right Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtFooterMarginsRight.Focus();
                        return false;
                    }
                    if (!float.TryParse(this.txtFooterMarginsRight.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Right Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtFooterMarginsRight.Focus();
                        return false;
                    }

                    if (this.txtFooterMarginsBottom.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Bottom Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtFooterMarginsBottom.Focus();
                        return false;
                    }
                    if (!float.TryParse(txtFooterMarginsBottom.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Bottom Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtFooterMarginsBottom.Focus();
                        return false;
                    }
                }

                // if (!this._isrbFitToPageChecked)
                {
                    if (this.txtPrinterMarginsTop.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Top Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtPrinterMarginsTop.Focus();
                        return false;
                    }

                    if (!float.TryParse(this.txtPrinterMarginsTop.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Top Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtPrinterMarginsTop.Focus();
                        return false;
                    }

                    if (this.txtPrinterMarginsLeft.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Left Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtPrinterMarginsLeft.Focus();
                        return false;
                    }
                    if (!float.TryParse(this.txtPrinterMarginsLeft.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Left Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtPrinterMarginsLeft.Focus();
                        return false;
                    }

                    if (this.txtPrinterMarginsRight.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Right Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtPrinterMarginsRight.Focus();
                        return false;
                    }
                    if (!float.TryParse(this.txtPrinterMarginsRight.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Right Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtPrinterMarginsRight.Focus();
                        return false;
                    }

                    if (this.txtPrinterMarginsBottom.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Bottom Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtPrinterMarginsBottom.Focus();
                        return false;
                    }
                    if (!float.TryParse(txtPrinterMarginsBottom.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Bottom Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtPrinterMarginsBottom.Focus();
                        return false;
                    }

                }
                {
                    if (this.txtGutterFlat.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Horizontal Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtGutterFlat.Focus();
                        return false;
                    }

                    if (!float.TryParse(this.txtGutterFlat.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Horizontal Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtGutterFlat.Focus();
                        return false;
                    }

                    if (this.txtOverlapFlat.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Horizontal Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtOverlapFlat.Focus();
                        return false;
                    }

                    if (!float.TryParse(this.txtOverlapFlat.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Horizontal Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtOverlapFlat.Focus();
                        return false;
                    }

                    if (this.txtOverlapErect.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Vertical Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtOverlapErect.Focus();
                        return false;
                    }

                    if (!float.TryParse(this.txtOverlapErect.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Vertical Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtOverlapErect.Focus();
                        return false;
                    }

                    if (this.txtGutterErect.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Vertical Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtPrinterMarginsTop.Focus();
                        return false;
                    }

                    if (!float.TryParse(this.txtGutterErect.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Vertical Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtPrinterMarginsTop.Focus();
                        return false;
                    }
                }
                {
                    if (this.txtGutterActualSizeWidth.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Horizontal Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtGutterActualSizeWidth.Focus();
                        return false;
                    }

                    if (!float.TryParse(this.txtGutterActualSizeWidth.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Horizontal Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtGutterActualSizeWidth.Focus();
                        return false;
                    }

                    if (this.txtOverlapActualSizeWidth.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Horizontal Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtOverlapActualSizeWidth.Focus();
                        return false;
                    }

                    if (!float.TryParse(this.txtOverlapActualSizeWidth.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Horizontal Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtOverlapActualSizeWidth.Focus();
                        return false;
                    }

                    if (this.txtOverlapActualSizeHeight.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Vertical Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtOverlapActualSizeHeight.Focus();
                        return false;
                    }

                    if (!float.TryParse(this.txtOverlapActualSizeHeight.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Vertical Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtOverlapActualSizeHeight.Focus();
                        return false;
                    }

                    if (this.txtGutterActualSizeHeight.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "Vertical Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtGutterActualSizeHeight.Focus();
                        return false;
                    }

                    if (!float.TryParse(this.txtGutterActualSizeHeight.Text.Trim(), out fltCheck))
                    {
                        ValidationMessage = "Vertical Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtGutterActualSizeHeight.Focus();
                        return false;
                    }
                }
                if (!this._isrbDefaultDPIChecked)
                {

                    if (this.txtDPIPrintResolutionValue.Text.Trim().Length <= 0)
                    {
                        ValidationMessage = "DPI Value should be entered.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtDPIPrintResolutionValue.Focus();
                        return false;
                    }
                    Int32 intCheck = 0;
                    if (!Int32.TryParse(this.txtDPIPrintResolutionValue.Text.Trim(), out intCheck))
                    {
                        ValidationMessage = "DPI Value should be numeric.";
                        MessageBox.Show(ValidationMessage, "Printer Parameters Insufficient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtDPIPrintResolutionValue.Focus();
                        return false;
                    }
                }


                ValidationMessage = "Success";
                return true;
            }
            catch { ValidationMessage = "Error occurred while validating Printer settings."; return false; }

        }

        gloExtendedPrinterSettings.PageSize curPageSizeRadio = gloExtendedPrinterSettings.PageSize.None;
        // Wire all events into this.
        //private void AllRadioButton_CheckedChanged(Object sender, EventArgs e)
        //{
        //    // Check of the raiser of the event is a checked Checkbox.
        //    // Of course we also need to to cast it first.
        //    if (((RadioButton)sender).Checked)
        //    {
        //        // This is the correct control.
        //        RadioButton rb = (RadioButton)sender;

        //        if (rb == rbActualPageSize)
        //        {
        //            curPageSizeRadio = gloExtendedPrinterSettings.PageSize.ActualPageSize;

        //        }
        //        if (rb == rbFitToPage)
        //        {
        //            curPageSizeRadio = gloExtendedPrinterSettings.PageSize.FitToPage;

        //        }
        //        if (rb == rbThreeByThree)
        //        {
        //            curPageSizeRadio = gloExtendedPrinterSettings.PageSize.ThreeByThree;

        //        }
        //        if (rb == rbThreeByTwo)
        //        {
        //            curPageSizeRadio = gloExtendedPrinterSettings.PageSize.ThreeByTwo;

        //        }
        //        if (rb == rbTwoByTwo)
        //        {
        //            curPageSizeRadio = gloExtendedPrinterSettings.PageSize.TwoByTwo;

        //        }
        //        if (rb == rbTwoByOne)
        //        {
        //            curPageSizeRadio = gloExtendedPrinterSettings.PageSize.TwoByOne;

        //        }

        //    }
        //}
        Boolean _isrbDefaultDPIChecked = true;
        private void rbDefaultDPIPrintResolution_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                _isrbDefaultDPIChecked = ((RadioButton)(sender)).Checked;
                this.txtDPIPrintResolutionValue.Enabled = !_isrbDefaultDPIChecked;
            }
            catch { }
        }

        //Boolean _isrbFitToPageChecked = true;
        //private void rbFitToPage_CheckedChanged(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        this._isrbFitToPageChecked = ((RadioButton)(sender)).Checked;
        //        //this.txtPrinterMarginsBottom.Enabled = !_isrbFitToPageChecked;
        //        //this.txtPrinterMarginsTop.Enabled = !_isrbFitToPageChecked;
        //        //this.txtPrinterMarginsRight.Enabled = !_isrbFitToPageChecked;
        //        //this.txtPrinterMarginsLeft.Enabled = !_isrbFitToPageChecked;
        //    }
        //    catch { }
        //}



        public gloExtendedPrinterSettings GetPrinterParametersExtended()
        {
            gloExtendedPrinterSettings objExtendedPrinterSettings = new gloExtendedPrinterSettings();
            if (gbFooterMargins.Enabled)
            {
                try
                {
                    objExtendedPrinterSettings.FooterTop = (float)Convert.ToDouble(txtFooterMarginsTop.Text);
                    objExtendedPrinterSettings.FooterBottom = (float)Convert.ToDouble(txtFooterMarginsBottom.Text);
                    objExtendedPrinterSettings.FooterRight = (float)Convert.ToDouble(txtFooterMarginsRight.Text);
                    objExtendedPrinterSettings.FooterLeft = (float)Convert.ToDouble(txtFooterMarginsLeft.Text);
                    objExtendedPrinterSettings.FooterFont = thisFont;
                    objExtendedPrinterSettings.FooterColor = thisColor;
                }
                catch
                {
                }
            }

            ////Custom Size with PrinterMargins defining rectangle dimensions viz., Top, Right, Bottom, Left
            //if (!this._isrbFitToPageChecked)
            //{
            //    objExtendedPrinterSettings.IsActualPageSize = !this._isrbFitToPageChecked;

            //}
            //else
            //{ objExtendedPrinterSettings.IsActualPageSize = !this._isrbFitToPageChecked; }

            if (rbActualPageSize.Checked)
            {
                curPageSizeRadio = gloExtendedPrinterSettings.PageSize.ActualPageSize;

            }
            if (rbFitToPage.Checked)
            {
                curPageSizeRadio = gloExtendedPrinterSettings.PageSize.FitToPage;

            }
            if (rbThreeByThree.Checked)
            {
                curPageSizeRadio = gloExtendedPrinterSettings.PageSize.ThreeByThree;

            }
            if (rbThreeByTwo.Checked)
            {
                curPageSizeRadio = gloExtendedPrinterSettings.PageSize.ThreeByTwo;

            }
            if (rbTwoByTwo.Checked)
            {
                curPageSizeRadio = gloExtendedPrinterSettings.PageSize.TwoByTwo;

            }
            if (rbTwoByOne.Checked)
            {
                curPageSizeRadio = gloExtendedPrinterSettings.PageSize.TwoByOne;

            }
            objExtendedPrinterSettings.CurrentPageSize = curPageSizeRadio;


            try
            {
                objExtendedPrinterSettings.PrinterMarginsTop = (float)Convert.ToDouble(txtPrinterMarginsTop.Text);
                objExtendedPrinterSettings.PrinterMarginsBottom = (float)Convert.ToDouble(txtPrinterMarginsBottom.Text);
                objExtendedPrinterSettings.PrinterMarginsRight = (float)Convert.ToDouble(txtPrinterMarginsRight.Text);
                objExtendedPrinterSettings.PrinterMarginsLeft = (float)Convert.ToDouble(txtPrinterMarginsLeft.Text);
            }
            catch
            {
            }
            try
            {
                objExtendedPrinterSettings.VerticalGutter = (float)Convert.ToDouble(txtGutterErect.Text);
                objExtendedPrinterSettings.VerticalOverlap = (float)Convert.ToDouble(txtOverlapErect.Text);
                objExtendedPrinterSettings.HorizontalGutter = (float)Convert.ToDouble(txtGutterFlat.Text);
                objExtendedPrinterSettings.HorizontalOverlap = (float)Convert.ToDouble(txtOverlapFlat.Text);

                objExtendedPrinterSettings.AdjustActualPageHorizontalPageWidthMargin = (float)Convert.ToDouble(txtOverlapActualSizeWidth.Text);
                objExtendedPrinterSettings.AdjustActualPageVerticalPageHeightMargin = (float)Convert.ToDouble(txtOverlapActualSizeHeight.Text);
                objExtendedPrinterSettings.AdjustFitToPageHorizontalPageWidthMargin = (float)Convert.ToDouble(txtGutterActualSizeWidth.Text);
                objExtendedPrinterSettings.AdjustFitToPageVerticalPageHeightMargin = (float)Convert.ToDouble(txtGutterActualSizeHeight.Text);

            }
            catch
            {
            }
            //Custom DPI
            if (!this._isrbDefaultDPIChecked)
            {
                objExtendedPrinterSettings.IsCustomDPI = !this._isrbDefaultDPIChecked;
                objExtendedPrinterSettings.CustomDPI = Convert.ToInt32(txtDPIPrintResolutionValue.Text);
            }
            else { objExtendedPrinterSettings.IsCustomDPI = !this._isrbDefaultDPIChecked; }


            objExtendedPrinterSettings.IsShowProgress = this._ischkShowProgress;
            if (!this._ischkShowProgress)
            {
             //   chkBackground.Enabled = false;
             //   chkBackground.Checked = true;
                this._ischkPrintBackground = true;
            }
            else
            {
               // chkBackground.Enabled = true;
            }
            objExtendedPrinterSettings.IsBackGroundPrint = this._ischkPrintBackground;

            objExtendedPrinterSettings.IsHorizontalFlow = this.rbLeftToRight.Checked;
            objExtendedPrinterSettings.IsActualLandscape = !this.chkPrinterLandscape.Checked;
            objExtendedPrinterSettings.IsActualMultiPage = !this.chkPrintOnePage.Checked;


            // return new ExtendedPrinterSettings();
            return objExtendedPrinterSettings;
        }

        internal void SetPrinterParametersExtended(gloExtendedPrinterSettings CustomPrinterExtendedSettings)
        {
            if (gbFooterMargins.Enabled)
            {
                try
                {
                    txtFooterMarginsTop.Text = Convert.ToString(CustomPrinterExtendedSettings.FooterTop);
                    txtFooterMarginsLeft.Text = Convert.ToString(CustomPrinterExtendedSettings.FooterLeft);
                    txtFooterMarginsRight.Text = Convert.ToString(CustomPrinterExtendedSettings.FooterRight);
                    txtFooterMarginsBottom.Text = Convert.ToString(CustomPrinterExtendedSettings.FooterBottom);
                    thisFont = CustomPrinterExtendedSettings.FooterFont;
                    thisColor = CustomPrinterExtendedSettings.FooterColor;
                }
                catch
                {
                }
                SetFontLabelString();

            }

            try
            {
                txtOverlapFlat.Text = Convert.ToString(CustomPrinterExtendedSettings.HorizontalOverlap);
                txtOverlapErect.Text = Convert.ToString(CustomPrinterExtendedSettings.VerticalOverlap);
                txtGutterFlat.Text = Convert.ToString(CustomPrinterExtendedSettings.HorizontalGutter);
                txtGutterErect.Text = Convert.ToString(CustomPrinterExtendedSettings.VerticalGutter);

                txtOverlapActualSizeWidth.Text = Convert.ToString(CustomPrinterExtendedSettings.AdjustActualPageHorizontalPageWidthMargin);
                txtOverlapActualSizeHeight.Text = Convert.ToString(CustomPrinterExtendedSettings.AdjustActualPageVerticalPageHeightMargin);
                txtGutterActualSizeWidth.Text = Convert.ToString(CustomPrinterExtendedSettings.AdjustFitToPageHorizontalPageWidthMargin);
                txtGutterActualSizeHeight.Text = Convert.ToString(CustomPrinterExtendedSettings.AdjustFitToPageVerticalPageHeightMargin);
            }
            catch
            {
            }

            //if (CustomPrinterExtendedSettings.IsActualPageSize)
            {
                //rbFitToPage.Checked = !CustomPrinterExtendedSettings.IsActualPageSize;
                //rbActualPageSize.Checked = CustomPrinterExtendedSettings.IsActualPageSize;
                txtPrinterMarginsTop.Text = Convert.ToString(CustomPrinterExtendedSettings.PrinterMarginsTop);
                txtPrinterMarginsLeft.Text = Convert.ToString(CustomPrinterExtendedSettings.PrinterMarginsLeft);
                txtPrinterMarginsRight.Text = Convert.ToString(CustomPrinterExtendedSettings.PrinterMarginsRight);
                txtPrinterMarginsBottom.Text = Convert.ToString(CustomPrinterExtendedSettings.PrinterMarginsBottom);
            }
            curPageSizeRadio = CustomPrinterExtendedSettings.CurrentPageSize;
            switch (CustomPrinterExtendedSettings.CurrentPageSize)
            {
                case gloExtendedPrinterSettings.PageSize.ActualPageSize:
                    {
                        rbActualPageSize.Checked = true;
                        break;
                    }
                case gloExtendedPrinterSettings.PageSize.FitToPage:
                    {
                        rbFitToPage.Checked = true;
                        break;
                    }
                case gloExtendedPrinterSettings.PageSize.ThreeByThree:
                    {
                        rbThreeByThree.Checked = true;
                        break;
                    }
                case gloExtendedPrinterSettings.PageSize.ThreeByTwo:
                    {
                        rbThreeByTwo.Checked = true;
                        break;
                    }
                case gloExtendedPrinterSettings.PageSize.TwoByTwo:
                    {
                        rbTwoByTwo.Checked = true;
                        break;
                    }
                case gloExtendedPrinterSettings.PageSize.TwoByOne:
                    {
                        rbTwoByOne.Checked = true;
                        break;
                    }
                case gloExtendedPrinterSettings.PageSize.None:
                    {

                        break;
                    }
            }
            //else
            //{
            //    rbFitToPage.Checked = !CustomPrinterExtendedSettings.IsActualPageSize;
            //    rbActualPageSize.Checked = CustomPrinterExtendedSettings.IsActualPageSize;
            //}

            //Custom DPI
            _isrbDefaultDPIChecked = !CustomPrinterExtendedSettings.IsCustomDPI;
            if (CustomPrinterExtendedSettings.IsCustomDPI)
            {
                rbDefaultDPIPrintResolution.Checked = !CustomPrinterExtendedSettings.IsCustomDPI;
                rbCustomDPIPrintResolution.Checked = CustomPrinterExtendedSettings.IsCustomDPI;
                txtDPIPrintResolutionValue.Text = Convert.ToString(CustomPrinterExtendedSettings.CustomDPI);
            }
            else
            {
                rbDefaultDPIPrintResolution.Checked = !CustomPrinterExtendedSettings.IsCustomDPI;
                rbCustomDPIPrintResolution.Checked = CustomPrinterExtendedSettings.IsCustomDPI;
            }
            _ischkShowProgress = CustomPrinterExtendedSettings.IsShowProgress;
            chkShowProgress.Checked = CustomPrinterExtendedSettings.IsShowProgress;
            if (!CustomPrinterExtendedSettings.IsShowProgress)
            {
                CustomPrinterExtendedSettings.IsBackGroundPrint = true;
            }
            _ischkPrintBackground = CustomPrinterExtendedSettings.IsBackGroundPrint;
            chkBackground.Checked = CustomPrinterExtendedSettings.IsBackGroundPrint;
            if (!CustomPrinterExtendedSettings.IsShowProgress)
            {
                chkBackground.Enabled = false;
                chkBackground.Checked = true;
                CustomPrinterExtendedSettings.IsBackGroundPrint = true;
            }
            else
            {
                chkBackground.Enabled = true;
            }
            
            rbTopToBottom.Checked = !CustomPrinterExtendedSettings.IsHorizontalFlow;
            rbLeftToRight.Checked = CustomPrinterExtendedSettings.IsHorizontalFlow;
            chkPrinterLandscape.Checked = !CustomPrinterExtendedSettings.IsActualLandscape;
            chkPrintOnePage.Checked = !CustomPrinterExtendedSettings.IsActualMultiPage;

        }

        private void SetFontLabelString()
        {
            Font CurFont = thisFont == null ? System.Drawing.SystemFonts.CaptionFont : thisFont;
            lblFooterFont.Text = CurFont.FontFamily.Name + "(" + CurFont.Size.ToString() + ")[" + CurFont.Style.ToString() + "]";
            lblFooterFont.ForeColor = thisColor;
            lblFooterFont.BackColor = gloGlobal.clsgloFont.BestForegroundColorForBackground(thisColor);
        }


        internal void SetTabOrder(IntPtr hCollateCheck, IntPtr hOKBtn, IntPtr hCancelBtn)
        {
            SetWindowPos(this.Handle, hCollateCheck, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));

            // SetWindowPos(pnlExtPanelContainer.Handle, hCollateCheck, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));



            // // SetWindowPos(gbPrinterMargins.Handle, this.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            //  SetWindowPos(rbFitToPage.Handle, this.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            //  SetWindowPos(rbActualPageSize.Handle, rbFitToPage.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            //  SetWindowPos(lblPrinterMarginsTop.Handle, rbActualPageSize.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            //  SetWindowPos(txtPrinterMarginsTop.Handle, lblPrinterMarginsTop.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            //  SetWindowPos(lblPrinterMarginsLeft.Handle, txtPrinterMarginsTop.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            //  SetWindowPos(txtPrinterMarginsLeft.Handle, lblPrinterMarginsLeft.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            //  SetWindowPos(lblPrinterMarginsRight.Handle, txtPrinterMarginsLeft.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            //  SetWindowPos(txtPrinterMarginsRight.Handle, lblPrinterMarginsRight.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            //  SetWindowPos(lblPrinterMarginsBottom.Handle, txtPrinterMarginsRight.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            //  SetWindowPos(txtPrinterMarginsBottom.Handle, lblPrinterMarginsBottom.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            ////  SetWindowPos(gbResolution.Handle, txtPrinterMarginsBottom.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));

            //  SetWindowPos(rbDefaultDPIPrintResolution.Handle, lblPrinterMarginsBottom.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            //  SetWindowPos(rbCustomDPIPrintResolution.Handle, rbDefaultDPIPrintResolution.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            //  SetWindowPos(txtDPIPrintResolutionValue.Handle, rbCustomDPIPrintResolution.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            //  SetWindowPos(lblDPIPrintResolution.Handle, txtDPIPrintResolutionValue.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));


            SetWindowPos(hOKBtn, chkBackground.Handle, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));
            SetWindowPos(hCancelBtn, hOKBtn, 0, 0, 0, 0, (UInt16)(WndPos.SWP_NOMOVE | WndPos.SWP_NOSIZE));


        }
        /*
    * SetWindowPos Flags
    */
        //internal class WndPos
        //{
        //    static public UInt16 SWP_NOSIZE = 0x0001;
        //    static public UInt16 SWP_NOMOVE = 0x0002;
        //    static public UInt16 SWP_NOZORDER = 0x0004;
        //    static public UInt16 SWP_NOREDRAW = 0x0008;
        //    static public UInt16 SWP_NOACTIVATE = 0x0010;
        //    static public UInt16 SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
        //    static public UInt16 SWP_SHOWWINDOW = 0x0040;
        //    static public UInt16 SWP_HIDEWINDOW = 0x0080;
        //    static public UInt16 SWP_NOCOPYBITS = 0x0100;
        //    static public UInt16 SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
        //    static public UInt16 SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */
        //};

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int Width, int Height, UInt16 uFlags);
        Boolean _ischkPrintBackground = true;
        Boolean _ischkShowProgress = false;
        private void chkBackground_CheckedChanged(object sender, EventArgs e)
        {
            this._ischkPrintBackground = ((CheckBox)(sender)).Checked;
        }

        private void chkShowProgress_CheckedChanged(object sender, EventArgs e)
        {
            this._ischkShowProgress = ((CheckBox)(sender)).Checked;


            if (!this._ischkShowProgress)
            {
                chkBackground.Enabled = false;
                chkBackground.Checked = true;
                this._ischkPrintBackground = true;
            }
            else
            {
                chkBackground.Enabled = true;
            }
        }
        void rbThreeByThree_CheckedChanged(object sender, System.EventArgs e)
        {
            EnableEitherOverlapOrGutter();
        }

        void rbThreeByTwo_CheckedChanged(object sender, System.EventArgs e)
        {
            EnableEitherOverlapOrGutter();
        }

        void rbTwoByTwo_CheckedChanged(object sender, System.EventArgs e)
        {
            EnableEitherOverlapOrGutter();
        }

        void rbTwoByOne_CheckedChanged(object sender, System.EventArgs e)
        {
            EnableEitherOverlapOrGutter();
        }

        void rbActualPageSize_CheckedChanged(object sender, System.EventArgs e)
        {
            EnableEitherOverlapOrGutter();
        }

        void rbFitToPage_CheckedChanged(object sender, System.EventArgs e)
        {
            EnableEitherOverlapOrGutter();
        }

        private void lblFooterFont_Click(object sender, EventArgs e)
        {
            GetFontDialog();
        }

        private void btnFooterFont_Click(object sender, EventArgs e)
        {
            GetFontDialog();
        }

        private void GetFontDialog()
        {
            fontDialogFooter.Font = thisFont == null ? System.Drawing.SystemFonts.CaptionFont : thisFont;
            fontDialogFooter.Color = thisColor;

            if (fontDialogFooter.ShowDialog(this) == DialogResult.OK)
            {
                thisFont = fontDialogFooter.Font;
                thisColor = fontDialogFooter.Color;
                SetFontLabelString();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
            if (this.Parent != null) { this.Parent.Refresh(); }
        }


    }
}
