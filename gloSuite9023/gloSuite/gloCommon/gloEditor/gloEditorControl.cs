using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;





namespace gloEditor
{
    public partial class gloEditorControl : UserControl
    {
        #region "Declarations"
        private const int _indent = 20;
        private Byte[] arrImage;
        private string _databaseconnnectionstring = "";
        #endregion "Declarations"

        #region "Constructor"
        public gloEditorControl()
        {
            InitializeComponent();
        }

        public gloEditorControl(string databaseconnectionstring)
        {
            InitializeComponent();
            _databaseconnnectionstring = databaseconnectionstring;
        }
        #endregion "Constructor"

        #region  "Delegates"


        #endregion  "Delegates"

        private void RTE_Load(object sender, EventArgs e)
        {
            fillFontCombo();
            ts_cmbFontSize.SelectedIndex = 0;
            ts_cmbFonts.SelectedIndex = 0;

            rtxtBox.SelectionFont = rtxtBox.Font;
            rtxtBox.Focus();

        }

        #region "Editor Methods "

        public void InsertImage()
        {
            try
            {
                try
                {
                    gloWord.gloWord.GetClipboardData();
                    //Clipboard.Clear();
                }
                catch //(Exception Ex2)
                {
                }
                string lstrFile = fileDialog.FileName;
                Bitmap myBitmap = new Bitmap(lstrFile);
                // Copy the bitmap to the clipboard.
                try
                {
                    Clipboard.SetDataObject(myBitmap);
                }
                catch //(Exception ex)
                {

                }
                // Get the format for the object type.
                DataFormats.Format myFormat = DataFormats.GetFormat(DataFormats.Bitmap);
                // After verifying that the data can be pasted, paste
                if (rtxtBox.CanPaste(myFormat))
                {
                    rtxtBox.Paste(myFormat);
                }
                else
                {
                    MessageBox.Show("The data format that you attempted site" +
                      " is not supportedby this control.");
                }
                rtxtBox.SelectionFont = fontDialog1.Font;
                try
                {
                    gloWord.gloWord.SetClipboardData();
                }
                catch //(Exception Ex1)
                {
                }
                myBitmap.Dispose();
                myFormat = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                fileDialog.FileName = "";
            }

        }

        private void makeStyled(FontStyle fs)
        {
            try
            {

                //get the current font of textbox
                Font fntCurrentTxtBoxFont = rtxtBox.SelectionFont;
                Font fntNewFont;
                //Check if font supports the given style
                if (fntCurrentTxtBoxFont.FontFamily.IsStyleAvailable(fs))
                {
                    //set the current fontstyle to bold.
                    fntNewFont = new Font(fntCurrentTxtBoxFont, fs);

                    //set selected text to bold
                    rtxtBox.SelectionFont = fntNewFont;
                    try
                    {
                        fntCurrentTxtBoxFont.Dispose();
                        fntCurrentTxtBoxFont = null;
                    }
                    catch
                    {
                    }
                }

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void makeStyled()
        {
            Font fntCurrentTxtBoxFont = rtxtBox.SelectionFont;
            bool fntChanged = false;
            try
            {

                //get the current font of textbox
              
                Font fntNewFont = null;
                //Font f;

                //--------------------------------------
                // Case 4 : Bold Italic Underlined
                if (((fntCurrentTxtBoxFont.FontFamily.IsStyleAvailable(FontStyle.Bold)) && (fntCurrentTxtBoxFont.FontFamily.IsStyleAvailable(FontStyle.Italic)) && (fntCurrentTxtBoxFont.FontFamily.IsStyleAvailable(FontStyle.Underline)))
                    && ((ts_btnBold.CheckState == CheckState.Checked) && (ts_btnUnderline.CheckState == CheckState.Checked) && (ts_btnItalic.CheckState == CheckState.Checked)))
                {
                    //font = new System.Drawing.Font(fnt.Name, Convert.ToInt64(ts_cmbFontSize.SelectedItem), FontStyle.Italic);
                    //rtxtBox.SelectionFont = font;
                    fntNewFont = new System.Drawing.Font(rtxtBox.SelectionFont.Name, rtxtBox.SelectionFont.Size, (FontStyle.Underline | FontStyle.Italic | FontStyle.Bold));
                    rtxtBox.SelectionFont = fntNewFont;
                    fntChanged = true;
                    return;
                }
                //---------------------------------------------


                //Check if the Bold style for text is true (i.e if Bold button is checked) then check if the 
                //selected font supports bold style if yes then set the style to bold.
                if ((fntCurrentTxtBoxFont.FontFamily.IsStyleAvailable(FontStyle.Bold)) && (ts_btnBold.CheckState == CheckState.Checked))
                {
                    fntNewFont = new System.Drawing.Font(fntCurrentTxtBoxFont.Name, Convert.ToInt64(ts_cmbFontSize.SelectedItem), FontStyle.Bold);
                    rtxtBox.SelectionFont = fntNewFont;
                    fntChanged = true;
                }
                //Check if the Italic style for text is true (i.e if Italic button is checked) then check if the 
                //selected font supports Italic style if yes then set the style to Italic.
                else if ((fntCurrentTxtBoxFont.FontFamily.IsStyleAvailable(FontStyle.Italic)) && (ts_btnItalic.CheckState == CheckState.Checked))
                {
                    fntNewFont = new System.Drawing.Font(fntCurrentTxtBoxFont.Name, Convert.ToInt64(ts_cmbFontSize.SelectedItem), FontStyle.Italic);
                    rtxtBox.SelectionFont = fntNewFont;
                    fntChanged = true;
                }
                //UnderLined
                else if ((fntCurrentTxtBoxFont.FontFamily.IsStyleAvailable(FontStyle.Underline)) && (ts_btnUnderline.CheckState == CheckState.Checked))
                {
                    fntNewFont = new System.Drawing.Font(fntCurrentTxtBoxFont.Name, Convert.ToInt64(ts_cmbFontSize.SelectedItem), FontStyle.Underline);
                    rtxtBox.SelectionFont = fntNewFont;
                    fntChanged = true;
                }
                else
                {
                    //set the current fontstyle to bold.
                    fntNewFont = new Font(fntCurrentTxtBoxFont, FontStyle.Regular);

                    //set selected text to bold
                    rtxtBox.SelectionFont = fntNewFont;
                    fntChanged = true;
                }




                //Check if the BoldItalic style for text is true (i.e if Bold & Italic  button is checked) then check if the 
                //selected font supports BoldItalic  style if yes then set the style to BoldItalic .
                //Special Case.

                //Case 1 : Bold Italic
                if (((fntCurrentTxtBoxFont.FontFamily.IsStyleAvailable(FontStyle.Bold)) && (fntCurrentTxtBoxFont.FontFamily.IsStyleAvailable(FontStyle.Italic)))
                    && ((ts_btnItalic.CheckState == CheckState.Checked) && (ts_btnBold.CheckState == CheckState.Checked)))
                {
                    try
                    {
                        if (fntNewFont != null)
                        {
                            fntNewFont.Dispose();
                            fntNewFont = null;
                        }
                    }
                    catch
                    {
                    }
                    fntNewFont = new System.Drawing.Font(fntCurrentTxtBoxFont.Name, Convert.ToInt64(ts_cmbFontSize.SelectedItem), FontStyle.Bold);
                    rtxtBox.SelectionFont = fntNewFont;
                    try
                    {
                        if (fntNewFont != null)
                        {
                            fntNewFont.Dispose();
                            fntNewFont = null;
                        }
                    }
                    catch
                    {
                    }
                    fntNewFont = new System.Drawing.Font(rtxtBox.SelectionFont.Name, rtxtBox.SelectionFont.Size, (FontStyle.Italic | FontStyle.Bold));
                    rtxtBox.SelectionFont = fntNewFont;
                    fntChanged = true;

                } //Case 1 : Bold Italic


                // Case 2 : Bold UnderLined
                else if (((fntCurrentTxtBoxFont.FontFamily.IsStyleAvailable(FontStyle.Bold)) && (fntCurrentTxtBoxFont.FontFamily.IsStyleAvailable(FontStyle.Underline)))
                    && ((ts_btnUnderline.CheckState == CheckState.Checked) && (ts_btnBold.CheckState == CheckState.Checked)))
                {
                    try
                    {
                        if (fntNewFont != null)
                        {
                            fntNewFont.Dispose();
                            fntNewFont = null;
                        }
                    }
                    catch
                    {
                    }
                    fntNewFont = new System.Drawing.Font(fntCurrentTxtBoxFont.Name, Convert.ToInt64(ts_cmbFontSize.SelectedItem), FontStyle.Bold);
                    rtxtBox.SelectionFont = fntNewFont;
                    try
                    {
                        if (fntNewFont != null)
                        {
                            fntNewFont.Dispose();
                            fntNewFont = null;
                        }
                    }
                    catch
                    {
                    }
                    fntNewFont = new System.Drawing.Font(rtxtBox.SelectionFont.Name, rtxtBox.SelectionFont.Size, (FontStyle.Underline | FontStyle.Bold));
                    rtxtBox.SelectionFont = fntNewFont;
                    fntChanged = true;
                } // Case 2 : Bold UnderLined


                //Case 3 : Italic Underlined
                else if (((fntCurrentTxtBoxFont.FontFamily.IsStyleAvailable(FontStyle.Italic)) && (fntCurrentTxtBoxFont.FontFamily.IsStyleAvailable(FontStyle.Underline)))
               && ((ts_btnUnderline.CheckState == CheckState.Checked) && (ts_btnItalic.CheckState == CheckState.Checked)))
                {
                    //font = new System.Drawing.Font(fnt.Name, Convert.ToInt64(ts_cmbFontSize.SelectedItem), FontStyle.Italic);
                    //rtxtBox.SelectionFont = font;
                    try
                    {
                        if (fntNewFont != null)
                        {
                            fntNewFont.Dispose();
                            fntNewFont = null;
                        }
                    }
                    catch
                    {
                    }
                    fntNewFont = new System.Drawing.Font(rtxtBox.SelectionFont.Name, rtxtBox.SelectionFont.Size, (FontStyle.Underline | FontStyle.Italic));
                    rtxtBox.SelectionFont = fntNewFont;
                    fntChanged = true;
                }//Case 3 : Italic Underlined

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (fntChanged)
                {
                    try
                    {
                        if (fntCurrentTxtBoxFont != null)
                        {
                            fntCurrentTxtBoxFont.Dispose();
                            fntCurrentTxtBoxFont = null;
                        }
                    }
                    catch
                    {
                    }
                }
            }

        }

        private void setAlignment(HorizontalAlignment ha)
        {

            rtxtBox.SelectionAlignment = ha;

        }

        public void clearText()
        {
            rtxtBox.Clear();
        }

        #endregion "Editor Methods "

        #region " Tool Strip Events "

        private void tsb_Editor_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem.Tag == null)
                {
                    return;
                }

                switch (e.ClickedItem.Tag.ToString())
                {
                    case "BOLD":

                        if (rtxtBox.SelectionFont.Bold || ts_btnBold.Checked == true)
                        {

                            //makeRegular();
                            ts_btnBold.CheckState = CheckState.Unchecked;
                            makeStyled();
                            ts_btnBold.CheckState = CheckState.Checked;

                        }
                        else
                        {
                            //makeStyled(FontStyle.Bold);


                            //Make the check state to check explictly since it occours after this event.
                            ts_btnBold.CheckState = CheckState.Checked;
                            makeStyled();
                            //again reset the state back to its state
                            ts_btnBold.CheckState = CheckState.Unchecked;
                        }
                        break;

                    case "ITALIC":

                        if (rtxtBox.SelectionFont.Italic || ts_btnItalic.Checked == true)
                        {

                            // makeRegular();
                            ts_btnItalic.CheckState = CheckState.Unchecked;
                            makeStyled();
                            ts_btnItalic.CheckState = CheckState.Checked;

                        }
                        else
                        {
                            //makeStyled(FontStyle.Italic);

                            ts_btnItalic.CheckState = CheckState.Checked;
                            makeStyled();
                            ts_btnItalic.CheckState = CheckState.Unchecked;
                        }
                        break;

                    case "UNDERLINED":

                        if (rtxtBox.SelectionFont.Underline || ts_btnUnderline.Checked == true)
                        {
                            ts_btnUnderline.CheckState = CheckState.Unchecked;
                            makeStyled();
                            ts_btnUnderline.CheckState = CheckState.Checked;
                        }
                        else
                        {
                            ts_btnUnderline.CheckState = CheckState.Checked;
                            makeStyled();
                            ts_btnUnderline.CheckState = CheckState.Unchecked;
                        }

                        //if (rtxtBox.SelectionFont.Underline)
                        //{
                        //    makeRegular();
                        //}
                        //else
                        //{
                        //    makeStyled(FontStyle.Underline);
                        //}
                        break;


                    case "STRIKEOUT":
                        if (rtxtBox.SelectionFont.Strikeout)
                        {

                        }
                        else
                        {
                            makeStyled(FontStyle.Strikeout);
                        }
                        break;

                    case "L_ALIGN":

                        setAlignment(HorizontalAlignment.Left);
                        break;

                    case "C_ALING":

                        setAlignment(HorizontalAlignment.Center);
                        break;

                    case "R_ALING":

                        setAlignment(HorizontalAlignment.Right);
                        break;



                    case "IMAGE":
                        fileDialog.Filter = "(*.bmp)|*.bmp|(*.jpeg)|*.jpeg|(*.jpg)|*.jpg|(*.gif)|*.gif|(*.png)|*.png";
                        fileDialog.ShowDialog(this);
                        if (fileDialog.FileName != "")
                        {
                            InsertImage();
                        }

                        break;
                    case "COLOR":
                        try
                        {
                            colorDialog1.CustomColors = gloGlobal.gloCustomColor.customColor;
                        }
                        catch
                        {
                        }
                        DialogResult ok = colorDialog1.ShowDialog(this);
                        if ( (ok == DialogResult.OK) || (ok == DialogResult.Yes ) )
                        {
                            rtxtBox.SelectionColor = colorDialog1.Color;
                            ts_btnColor.BackColor = colorDialog1.Color;
                            try
                            {
                                gloGlobal.gloCustomColor.customColor = colorDialog1.CustomColors;
                            }
                            catch
                            {
                            }
                        }
                        //tsb_btnFont.ForeColor = colorDialog1.Color;
                        break;

                    case "INDENT_IN":

                        rtxtBox.SelectionIndent = rtxtBox.SelectionIndent + _indent;
                        break;

                    case "INDENT_OUT":

                        if (rtxtBox.SelectionIndent >= _indent)
                        {
                            rtxtBox.SelectionIndent = rtxtBox.SelectionIndent - _indent;
                        }
                        break;
                    case "SAVE":

                        SaveSignature();
                        break;

                    case "OPEN":
                        LoadSignature(arrImage);
                        break;

                    case "getdata":
                        getData();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        #endregion " Tool Strip Events "

        #region " Save & Load Signature Methods "

        private void SaveSignature()
        {
            try
            {
                saveFileDialog1.Filter = "(*.sig)|*.sig";
                saveFileDialog1.ShowDialog(this);
                if (saveFileDialog1.FileName != "")
                {
                    ///System.IO.Stream stream;
                   // System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    //SLR: Removed clearance of arrimage; on 12/22: why assigned?
                    rtxtBox.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                  //  arrImage = ms.ToArray();
                  //  ms.Close();
                  //  ms.Dispose();
                    // rtxtBox.Clear();

                    // object obj;
                    // obj = (Object)arrImage;
                    // arrImage = (Byte[])obj;
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void LoadSignature(Byte[] arrImage)
        {
            try
            {
                fileDialog.Filter = "(*.sig)|*.sig";
                fileDialog.ShowDialog(this);
                if (fileDialog.FileName == "")
                { return; }
                rtxtBox.LoadFile(fileDialog.FileName, RichTextBoxStreamType.RichText);
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                fileDialog.FileName = "";
            }
        }

        #endregion " Save & Load Signature Methods "

        #region " GET & SET Data Methods "

        public Byte[] getData()
        {
            try
            {
                if (rtxtBox.Text != "")
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    rtxtBox.SaveFile(ms, RichTextBoxStreamType.RichText);
                    try
                    {
                        ms.Flush();
                    }
                    catch
                    {
                    }
                    arrImage = ms.ToArray();
                    try
                    {
                        ms.Close();
                        ms.Dispose();
                    }
                    catch
                    {
                    }
                    return arrImage;
                    //rtxtBox.Clear();
                    //setData(arrImage);
                }
                return null;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); 
                return null;
            }

        }//getData

        public bool setData(Byte[] arImage)
        {
            try
            {
                if (arImage.Length > 0)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(arImage);
                    rtxtBox.Clear();
                  rtxtBox.LoadFile(ms, RichTextBoxStreamType.RichText);
                   // rtxtBox.LoadFile(Application.StartupPath.ToString() + "\\Temp\\body.Rtf") ; //,RichTextBoxStreamType.RichText );
                  try
                  {
                      ms.Close();
                      ms.Dispose();
                  }
                  catch
                  {
                  }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); 
                return false;
            }
        }//setData

        #endregion " GET & SET Data Methods "

        #region "Font Combo & Font Methods"

        private void fillFontCombo()
        {

            //Get the font Installed on System.
            InstalledFontCollection instFonts = new InstalledFontCollection();
            if (instFonts != null)
            {
                // Gets the list of installed fonts.
                FontFamily[] ff = instFonts.Families;  //FontFamily.Families;


                // Loop and create a sample of each font.
                for (int x = 0; x < ff.Length; x++)
                {

                    System.Drawing.Font font = null;


                    // Create the font - based on the styles available.
                    if (ff[x].IsStyleAvailable(FontStyle.Regular))
                        font = new System.Drawing.Font(
                            ff[x].Name,
                            ts_cmbFonts.Font.Size
                            );
                    else if (ff[x].IsStyleAvailable(FontStyle.Bold))
                        font = new System.Drawing.Font(
                            ff[x].Name,
                            ts_cmbFonts.Font.Size,
                            FontStyle.Bold
                            );
                    else if (ff[x].IsStyleAvailable(FontStyle.Italic))
                        font = new System.Drawing.Font(
                            ff[x].Name,
                            ts_cmbFonts.Font.Size,
                            FontStyle.Italic
                            );
                    else if (ff[x].IsStyleAvailable(FontStyle.Strikeout))
                        font = new System.Drawing.Font(
                            ff[x].Name,
                            ts_cmbFonts.Font.Size,
                            FontStyle.Strikeout
                            );
                    else if (ff[x].IsStyleAvailable(FontStyle.Underline))
                        font = new System.Drawing.Font(
                            ff[x].Name,
                            ts_cmbFonts.Font.Size,
                            FontStyle.Underline
                            );

                    // Should we add the item?
                    if (font != null)
                    {

                        ts_cmbFonts.Items.Add(font);
                    }
                  
                    //ts_cmbFonts.DisplayMember= font.Name;

                }
                if (instFonts != null) //added for bugid 83926
                {
                    instFonts.Dispose();
                }
               instFonts = null;
            } // End for all the fonts.


        }

        private void ts_cmbFonts_DrawItem(object sender, DrawItemEventArgs e)
        {
            // If the index is invalid then simply exit.
            if (e.Index == -1 || e.Index >= ts_cmbFonts.Items.Count)
                return;

            // Draw the background of the item.
            e.DrawBackground();

            // draw the focus rectangle
            if ((e.State & DrawItemState.Focus) != 0)
                e.DrawFocusRectangle();

            Brush b = null;

            try
            {

                // Create a new background brush.
                b = new SolidBrush(e.ForeColor);

                // Draw the item.
                e.Graphics.DrawString(
                    ((Font)ts_cmbFonts.Items[e.Index]).Name,
                    ((Font)ts_cmbFonts.Items[e.Index]),
                    b,
                    e.Bounds
                    );

            } // End try

            finally
            {

                // Should we cleanup the brush?
                if (b != null)
                    b.Dispose();

                b = null;

            } // End finally

        }

        private void ts_cmbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Font fntCurrentTxtBoxFont = rtxtBox.SelectionFont;
            Font fntNewFont = chkFontStyle(fntCurrentTxtBoxFont, Convert.ToInt64(ts_cmbFontSize.SelectedItem));
            if (fntNewFont != null)
            {
                rtxtBox.SelectionFont = fntNewFont;
                try
                {
                    fntCurrentTxtBoxFont.Dispose();
                    fntCurrentTxtBoxFont = null;
                }
                catch
                {
                }
            }
            ////////Font tempf = new Font(rtxtBox.SelectionFont.Name , Convert.ToInt64(ts_cmbFontSize.SelectedItem));
            //////if((tempf.FontFamily.IsStyleAvailable(FontStyle.Bold) && (ts_btnBold.CheckState == CheckState.Checked)))
            //////{
            //////    font = new System.Drawing.Font(tempf.Name,tempf.Size,FontStyle.Bold);
            //////    rtxtBox.SelectionFont = font ;
            //////    rtxtBox.Focus();
            //////}
            ////////Check if the Italic style for text is true (i.e if Italic button is checked) then check if the 
            ////////selected font supports Italic style if yes then set the style to Italic.
            //////else if ((tempf.FontFamily.IsStyleAvailable(FontStyle.Italic)) && (ts_btnItalic.CheckState == CheckState.Checked))
            //////{
            //////    font = new System.Drawing.Font(tempf.Name, tempf.Size, FontStyle.Italic);
            //////    rtxtBox.SelectionFont = font;
            //////}
            //////else
            //////{
            //////    rtxtBox.SelectionFont = tempf;
            //////    rtxtBox.Focus();
            //////}


            ////////Check if the BoldItalic style for text is true (i.e if Bold & Italic  button is checked) then check if the 
            ////////selected font supports BoldItalic  style if yes then set the style to BoldItalic .
            ////////Special Case.
            //////if (((tempf.FontFamily.IsStyleAvailable(FontStyle.Bold)) && (tempf.FontFamily.IsStyleAvailable(FontStyle.Italic)))
            //////    && ((ts_btnItalic.CheckState == CheckState.Checked) && (ts_btnBold.CheckState == CheckState.Checked)))
            //////{
            //////    font = new System.Drawing.Font(tempf.Name, tempf.Size, FontStyle.Bold);
            //////    rtxtBox.SelectionFont = font;
            //////    font = new System.Drawing.Font(rtxtBox.SelectionFont.Name, rtxtBox.SelectionFont.Size,(FontStyle.Bold|FontStyle.Italic));
            //////    rtxtBox.SelectionFont = font;

            //////}


            //rtxtBox.SelectionFont = tempf;

        }

        private void ts_cmbFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Font fntCurrentTxtBoxFont = rtxtBox.SelectionFont;
                Font fntNewFont = chkFontStyle(((Font)ts_cmbFonts.SelectedItem), Convert.ToInt64(ts_cmbFontSize.SelectedItem));
                if (fntNewFont != null)
                {
                 //   Font font;
                    //TODO : check for font is system supported font n then add.
                    rtxtBox.SelectionFont = fntNewFont;
                    ////////Check if the Bold style for text is true (i.e if Bold button is checked) then check if the 
                    ////////selected font supports bold style if yes then set the style to bold.
                    //////if ((fnt.FontFamily.IsStyleAvailable(FontStyle.Bold)) && (ts_btnBold.CheckState == CheckState.Checked))
                    //////{
                    //////    font = new System.Drawing.Font(fnt.Name, fnt.Size, FontStyle.Bold);
                    //////    rtxtBox.SelectionFont = font;
                    //////}
                    ////////Check if the Italic style for text is true (i.e if Italic button is checked) then check if the 
                    ////////selected font supports Italic style if yes then set the style to Italic.
                    //////else if ((fnt.FontFamily.IsStyleAvailable(FontStyle.Italic)) && (ts_btnItalic.CheckState == CheckState.Checked))
                    //////{
                    //////    font = new System.Drawing.Font(fnt.Name, fnt.Size, FontStyle.Italic);
                    //////    rtxtBox.SelectionFont = font;
                    //////}
                    //////else
                    //////{
                    //////    rtxtBox.SelectionFont = fnt;

                    //////}

                    ////////Check if the BoldItalic style for text is true (i.e if Bold & Italic  button is checked) then check if the 
                    ////////selected font supports BoldItalic  style if yes then set the style to BoldItalic .
                    ////////Special Case.
                    //////if (( (fnt.FontFamily.IsStyleAvailable(FontStyle.Bold)) && (fnt.FontFamily.IsStyleAvailable(FontStyle.Italic)))
                    //////    && ((ts_btnItalic.CheckState == CheckState.Checked) && (ts_btnBold.CheckState == CheckState.Checked)))
                    //////{
                    //////   font = new System.Drawing.Font(fnt.Name, fnt.Size, FontStyle.Bold);
                    //////   rtxtBox.SelectionFont = font;
                    //////   font = new System.Drawing.Font(rtxtBox.SelectionFont.Name, rtxtBox.SelectionFont.Size,(FontStyle.Italic|FontStyle.Bold ));
                    //////   rtxtBox.SelectionFont = font;

                    //////}

                    try
                    {
                        fntCurrentTxtBoxFont.Dispose();
                        fntCurrentTxtBoxFont = null;
                    }
                    catch
                    {
                    }


                }
                rtxtBox.Focus();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private Font chkFontStyle(Font fnt, Int64 fntSize)
        {
            Font fntNewFont = null;
            try
            {
                // Create the font - based on the styles available.
                if (fnt.FontFamily.IsStyleAvailable(FontStyle.Regular))
                {
                    fntNewFont = new System.Drawing.Font(fnt.Name, fntSize);
                }
                else if (fnt.FontFamily.IsStyleAvailable(FontStyle.Italic))
                {
                    fntNewFont = new System.Drawing.Font(fnt.Name, fntSize, FontStyle.Italic);
                }

                //--------------------------------------

                //Check if the Bold style for text is true (i.e if Bold button is checked) then check if the 
                //selected font supports bold style if yes then set the style to bold.
                if ((fnt.FontFamily.IsStyleAvailable(FontStyle.Bold)) && (ts_btnBold.CheckState == CheckState.Checked))
                {
                    try
                    {
                        if (fntNewFont != null)
                        {
                            fntNewFont.Dispose();
                            fntNewFont = null;
                        }
                    }
                    catch
                    {
                    }
                    fntNewFont = new System.Drawing.Font(fnt.Name, fntSize, FontStyle.Bold);
                    rtxtBox.SelectionFont = fntNewFont;
                }
                //Check if the Italic style for text is true (i.e if Italic button is checked) then check if the 
                //selected font supports Italic style if yes then set the style to Italic.
                else if ((fnt.FontFamily.IsStyleAvailable(FontStyle.Italic)) && (ts_btnItalic.CheckState == CheckState.Checked))
                {
                    try
                    {
                        if (fntNewFont != null)
                        {
                            fntNewFont.Dispose();
                            fntNewFont = null;
                        }
                    }
                    catch
                    {
                    }
                    fntNewFont = new System.Drawing.Font(fnt.Name, fntSize, FontStyle.Italic);
                    rtxtBox.SelectionFont = fntNewFont;
                }

                //Check if the BoldItalic style for text is true (i.e if Bold & Italic  button is checked) then check if the 
                //selected font supports BoldItalic  style if yes then set the style to BoldItalic .
                //Special Case.
                if (((fnt.FontFamily.IsStyleAvailable(FontStyle.Bold)) && (fnt.FontFamily.IsStyleAvailable(FontStyle.Italic)))
                    && ((ts_btnItalic.CheckState == CheckState.Checked) && (ts_btnBold.CheckState == CheckState.Checked)))
                {
                    try
                    {
                        if (fntNewFont != null)
                        {
                            fntNewFont.Dispose();
                            fntNewFont = null;
                        }
                    }
                    catch
                    {
                    }
                    fntNewFont = new System.Drawing.Font(fnt.Name, fntSize, FontStyle.Bold);
                    rtxtBox.SelectionFont = fntNewFont;
                    try
                    {
                        if (fntNewFont != null)
                        {
                            fntNewFont.Dispose();
                            fntNewFont = null;
                        }
                    }
                    catch
                    {
                    }
                    fntNewFont = new System.Drawing.Font(rtxtBox.SelectionFont.Name, rtxtBox.SelectionFont.Size, (FontStyle.Italic | FontStyle.Bold));
                    rtxtBox.SelectionFont = fntNewFont;

                }
                //--------------------------------------


                return fntNewFont;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;

            }
        }

        #endregion "Font Combo & Font Methods"


    }
}
