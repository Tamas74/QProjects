using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using gloTransparentScreen;

namespace gloTransparentScreen
{
    public partial class gloCustomMessageBoxForm : Form
    {
         


            #region Private constructor

            /// <summary>
            /// Initializes a new instance of the <see cref="gloCustomMessageBoxForm"/> class.
            /// </summary>
            private gloCustomMessageBoxForm()
            {
                InitializeComponent();

                //Try to evaluate the language. If this fails, the fallback language English will be used
                Enum.TryParse<TwoLetterISOLanguageID>(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, out this.languageID);

                this.KeyPreview = true;
                this.KeyUp += gloCustomMessageBoxForm_KeyUp;
            }

            #endregion
            #region Private constants

            //These separators are used for the "copy to clipboard" standard operation, triggered by Ctrl + C (behavior and clipboard format is like in a standard MessageBox)
            private static readonly String STANDARD_MESSAGEBOX_SEPARATOR_LINES = "---------------------------\n";
            private static readonly String STANDARD_MESSAGEBOX_SEPARATOR_SPACES = "   ";

            //These are the possible buttons (in a standard MessageBox)
            private enum ButtonID { OK = 0, CANCEL, YES, NO, ABORT, RETRY, IGNORE };

            //These are the buttons texts for different languages. 
            //If you want to add a new language, add it here and in the GetButtonText-Function
            private enum TwoLetterISOLanguageID { en, de, es, it };
            private static readonly String[] BUTTON_TEXTS_ENGLISH_EN = { "OK", "Cancel", "&Yes", "&No", "&Abort", "&Retry", "&Ignore" }; //Note: This is also the fallback language
            private static readonly String[] BUTTON_TEXTS_GERMAN_DE = { "OK", "Abbrechen", "&Ja", "&Nein", "&Abbrechen", "&Wiederholen", "&Ignorieren" };
            private static readonly String[] BUTTON_TEXTS_SPANISH_ES = { "Aceptar", "Cancelar", "&Sí", "&No", "&Abortar", "&Reintentar", "&Ignorar" };
            private static readonly String[] BUTTON_TEXTS_ITALIAN_IT = { "OK", "Annulla", "&Sì", "&No", "&Interrompi", "&Riprova", "&Ignora" };

            #endregion
            #region Private statics

            /// <summary>
            /// Defines the maximum width for all gloCustomMessageBox instances in percent of the working area.
            /// 
            /// Allowed values are 0.2 - 1.0 where: 
            /// 0.2 means:  The gloCustomMessageBox can be at most half as wide as the working area.
            /// 1.0 means:  The gloCustomMessageBox can be as wide as the working area.
            /// 
            /// Default is: 70% of the working area width.
            /// </summary>
            private static double MAX_WIDTH_FACTOR = gloCustomMessageBox.MAX_WIDTH_FACTOR;

            /// <summary>
            /// Defines the maximum height for all gloCustomMessageBox instances in percent of the working area.
            /// 
            /// Allowed values are 0.2 - 1.0 where: 
            /// 0.2 means:  The gloCustomMessageBox can be at most half as high as the working area.
            /// 1.0 means:  The gloCustomMessageBox can be as high as the working area.
            /// 
            /// Default is: 90% of the working area height.
            /// </summary>
            private static double MAX_HEIGHT_FACTOR = gloCustomMessageBox.MAX_HEIGHT_FACTOR;

            /// <summary>
            /// Defines the font for all gloCustomMessageBox instances.
            /// 
            /// Default is: SystemFonts.MessageBoxFont
            /// </summary>
            private static Font FONT = gloCustomMessageBox.FONT;

            #endregion
            #region Private members

            private MessageBoxDefaultButton defaultButton;
            private int visibleButtonsCount;
            private TwoLetterISOLanguageID languageID = TwoLetterISOLanguageID.en;

            #endregion

            #region Private helper functions

            /// <summary>
            /// Gets the string rows.
            /// </summary>
            /// <param name="message">The message.</param>
            /// <returns>The string rows as 1-dimensional array</returns>
            private static string[] GetStringRows(string message)
            {
                if (string.IsNullOrEmpty(message)) return null;

                var messageRows = message.Split(new char[] { '\n' }, StringSplitOptions.None);
                return messageRows;
            }

            /// <summary>
            /// Gets the button text for the CurrentUICulture language.
            /// Note: The fallback language is English
            /// </summary>
            /// <param name="buttonID">The ID of the button.</param>
            /// <returns>The button text</returns>
            private string GetButtonText(ButtonID buttonID)
            {
                var buttonTextArrayIndex = Convert.ToInt32(buttonID);

                switch (this.languageID)
                {
                    case TwoLetterISOLanguageID.de: return BUTTON_TEXTS_GERMAN_DE[buttonTextArrayIndex];
                    case TwoLetterISOLanguageID.es: return BUTTON_TEXTS_SPANISH_ES[buttonTextArrayIndex];
                    case TwoLetterISOLanguageID.it: return BUTTON_TEXTS_ITALIAN_IT[buttonTextArrayIndex];

                    default: return BUTTON_TEXTS_ENGLISH_EN[buttonTextArrayIndex];
                }
            }

            /// <summary>
            /// Ensure the given working area factor in the range of  0.2 - 1.0 where: 
            /// 
            /// 0.2 means:  20 percent of the working area height or width.
            /// 1.0 means:  100 percent of the working area height or width.
            /// </summary>
            /// <param name="workingAreaFactor">The given working area factor.</param>
            /// <returns>The corrected given working area factor.</returns>
            private static double GetCorrectedWorkingAreaFactor(double workingAreaFactor)
            {
                const double MIN_FACTOR = 0.2;
                const double MAX_FACTOR = 1.0;

                if (workingAreaFactor < MIN_FACTOR) return MIN_FACTOR;
                if (workingAreaFactor > MAX_FACTOR) return MAX_FACTOR;

                return workingAreaFactor;
            }

            /// <summary>
            /// Set the dialogs start position when given. 
            /// Otherwise center the dialog on the current screen.
            /// </summary>
            /// <param name="gloCustomMessageBoxForm">The gloCustomMessageBox dialog.</param>
            /// <param name="owner">The owner.</param>
            private static void SetDialogStartPosition(gloCustomMessageBoxForm gloCustomMessageBoxForm, IWin32Window owner)
            {
                //If no owner given: Center on current screen
                if (owner == null)
                {
                    var screen = Screen.FromPoint(Cursor.Position);
                    gloCustomMessageBoxForm.StartPosition = FormStartPosition.Manual;
                    gloCustomMessageBoxForm.Left = screen.Bounds.Left + screen.Bounds.Width / 2 - gloCustomMessageBoxForm.Width / 2;
                    gloCustomMessageBoxForm.Top = screen.Bounds.Top + screen.Bounds.Height / 2 - gloCustomMessageBoxForm.Height / 2;
                }
            }

            /// <summary>
            /// Calculate the dialogs start size (Try to auto-size width to show longest text row).
            /// Also set the maximum dialog size. 
            /// </summary>
            /// <param name="gloCustomMessageBoxForm">The gloCustomMessageBox dialog.</param>
            /// <param name="text">The text (the longest text row is used to calculate the dialog width).</param>
            /// <param name="text">The caption (this can also affect the dialog width).</param>
            private static void SetDialogSizes(gloCustomMessageBoxForm gloCustomMessageBoxForm, string text, string caption)
            {
                //First set the bounds for the maximum dialog size
                gloCustomMessageBoxForm.MaximumSize = new Size(Convert.ToInt32(SystemInformation.WorkingArea.Width * gloCustomMessageBoxForm.GetCorrectedWorkingAreaFactor(MAX_WIDTH_FACTOR)),
                                                              Convert.ToInt32(SystemInformation.WorkingArea.Height * gloCustomMessageBoxForm.GetCorrectedWorkingAreaFactor(MAX_HEIGHT_FACTOR)));

                //Get rows. Exit if there are no rows to render...
                var stringRows = GetStringRows(text);
                if (stringRows == null) return;

                //Calculate whole text height
                var textHeight = TextRenderer.MeasureText(text, FONT).Height;

                //Calculate width for longest text line
                const int SCROLLBAR_WIDTH_OFFSET = 15;
                var longestTextRowWidth = stringRows.Max(textForRow => TextRenderer.MeasureText(textForRow, FONT).Width);
                var captionWidth = TextRenderer.MeasureText(caption, SystemFonts.CaptionFont).Width;
                var textWidth = Math.Max(longestTextRowWidth + SCROLLBAR_WIDTH_OFFSET, captionWidth);

                //Calculate margins
                var marginWidth = gloCustomMessageBoxForm.Width - gloCustomMessageBoxForm.richTextBoxMessage.Width;
                var marginHeight = gloCustomMessageBoxForm.Height - gloCustomMessageBoxForm.richTextBoxMessage.Height;

                //Set calculated dialog size (if the calculated values exceed the maximums, they were cut by windows forms automatically)
                gloCustomMessageBoxForm.Size = new Size(textWidth + marginWidth,
                                                       textHeight + marginHeight);
            }

            /// <summary>
            /// Set the dialogs icon. 
            /// When no icon is used: Correct placement and width of rich text box.
            /// </summary>
            /// <param name="gloCustomMessageBoxForm">The gloCustomMessageBox dialog.</param>
            /// <param name="icon">The MessageBoxIcon.</param>
            private static void SetDialogIcon(gloCustomMessageBoxForm gloCustomMessageBoxForm, MessageBoxIcon icon)
            {
                switch (icon)
                {
                    case MessageBoxIcon.Information:
                        gloCustomMessageBoxForm.pictureBoxForIcon.Image = SystemIcons.Information.ToBitmap();
                        break;
                    case MessageBoxIcon.Warning:
                        gloCustomMessageBoxForm.pictureBoxForIcon.Image = SystemIcons.Warning.ToBitmap();
                        break;
                    case MessageBoxIcon.Error:
                        gloCustomMessageBoxForm.pictureBoxForIcon.Image = SystemIcons.Error.ToBitmap();
                        break;
                    case MessageBoxIcon.Question:
                        gloCustomMessageBoxForm.pictureBoxForIcon.Image = SystemIcons.Question.ToBitmap();
                        break;
                    default:
                        //When no icon is used: Correct placement and width of rich text box.
                        gloCustomMessageBoxForm.pictureBoxForIcon.Visible = false;
                        gloCustomMessageBoxForm.richTextBoxMessage.Left -= gloCustomMessageBoxForm.pictureBoxForIcon.Width;
                        gloCustomMessageBoxForm.richTextBoxMessage.Width += gloCustomMessageBoxForm.pictureBoxForIcon.Width;
                        break;
                }
            }

            /// <summary>
            /// Set dialog custom label texts for buttons. 
            /// </summary>
            /// <param name="gloCustomMessageBoxForm">The gloCustomMessageBox dialog.</param>
            /// <param name="buttonTexts">The button Label Texts.</param>
            
            private static void SetDialogButtonTexts(gloCustomMessageBoxForm gloCustomMessageBoxForm, string[] buttonTexts)
            {
                //Set the buttons custom label texts
                if (buttonTexts != null)
                {
                    if (buttonTexts.Length >= 1)
                    {
                        gloCustomMessageBoxForm.button1.Text = buttonTexts[0];
                        if (buttonTexts.Length >= 2)
                        {
                            gloCustomMessageBoxForm.button2.Text = buttonTexts[1];
                            if (buttonTexts.Length >= 3)
                            {
                                gloCustomMessageBoxForm.button3.Text = buttonTexts[2];
                            }
                        }
                        
                    }
                }
            }

            /// <summary>
            /// Set dialog buttons visibilities and texts. 
            /// Also set a default button.
            /// </summary>
            /// <param name="gloCustomMessageBoxForm">The gloCustomMessageBox dialog.</param>
            /// <param name="buttons">The buttons.</param>
            /// <param name="defaultButton">The default button.</param>
            private static void SetDialogButtons(gloCustomMessageBoxForm gloCustomMessageBoxForm, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
            {
                //Set the buttons visibilities and texts
                switch (buttons)
                {
                    case MessageBoxButtons.AbortRetryIgnore:
                        gloCustomMessageBoxForm.visibleButtonsCount = 3;

                        gloCustomMessageBoxForm.button1.Visible = true;
                        gloCustomMessageBoxForm.button1.Text = gloCustomMessageBoxForm.GetButtonText(ButtonID.ABORT);
                        gloCustomMessageBoxForm.button1.DialogResult = DialogResult.Abort;

                        gloCustomMessageBoxForm.button2.Visible = true;
                        gloCustomMessageBoxForm.button2.Text = gloCustomMessageBoxForm.GetButtonText(ButtonID.RETRY);
                        gloCustomMessageBoxForm.button2.DialogResult = DialogResult.Retry;

                        gloCustomMessageBoxForm.button3.Visible = true;
                        gloCustomMessageBoxForm.button3.Text = gloCustomMessageBoxForm.GetButtonText(ButtonID.IGNORE);
                        gloCustomMessageBoxForm.button3.DialogResult = DialogResult.Ignore;

                        gloCustomMessageBoxForm.ControlBox = false;
                        break;

                    case MessageBoxButtons.OKCancel:
                        gloCustomMessageBoxForm.visibleButtonsCount = 2;

                        gloCustomMessageBoxForm.button2.Visible = true;
                        gloCustomMessageBoxForm.button2.Text = gloCustomMessageBoxForm.GetButtonText(ButtonID.OK);
                        gloCustomMessageBoxForm.button2.DialogResult = DialogResult.OK;

                        gloCustomMessageBoxForm.button3.Visible = true;
                        gloCustomMessageBoxForm.button3.Text = gloCustomMessageBoxForm.GetButtonText(ButtonID.CANCEL);
                        gloCustomMessageBoxForm.button3.DialogResult = DialogResult.Cancel;

                        gloCustomMessageBoxForm.CancelButton = gloCustomMessageBoxForm.button3;
                        break;

                    case MessageBoxButtons.RetryCancel:
                        gloCustomMessageBoxForm.visibleButtonsCount = 2;

                        gloCustomMessageBoxForm.button2.Visible = true;
                        gloCustomMessageBoxForm.button2.Text = gloCustomMessageBoxForm.GetButtonText(ButtonID.RETRY);
                        gloCustomMessageBoxForm.button2.DialogResult = DialogResult.Retry;

                        gloCustomMessageBoxForm.button3.Visible = true;
                        gloCustomMessageBoxForm.button3.Text = gloCustomMessageBoxForm.GetButtonText(ButtonID.CANCEL);
                        gloCustomMessageBoxForm.button3.DialogResult = DialogResult.Cancel;

                        gloCustomMessageBoxForm.CancelButton = gloCustomMessageBoxForm.button3;
                        break;

                    case MessageBoxButtons.YesNo:
                        gloCustomMessageBoxForm.visibleButtonsCount = 2;

                        gloCustomMessageBoxForm.button2.Visible = true;
                        gloCustomMessageBoxForm.button2.Text = gloCustomMessageBoxForm.GetButtonText(ButtonID.YES);
                        gloCustomMessageBoxForm.button2.DialogResult = DialogResult.Yes;

                        gloCustomMessageBoxForm.button3.Visible = true;
                        gloCustomMessageBoxForm.button3.Text = gloCustomMessageBoxForm.GetButtonText(ButtonID.NO);
                        gloCustomMessageBoxForm.button3.DialogResult = DialogResult.No;

                        gloCustomMessageBoxForm.ControlBox = false;
                        break;

                    case MessageBoxButtons.YesNoCancel:
                        gloCustomMessageBoxForm.visibleButtonsCount = 3;

                        gloCustomMessageBoxForm.button1.Visible = true;
                        gloCustomMessageBoxForm.button1.Text = gloCustomMessageBoxForm.GetButtonText(ButtonID.YES);
                        gloCustomMessageBoxForm.button1.DialogResult = DialogResult.Yes;

                        gloCustomMessageBoxForm.button2.Visible = true;
                        gloCustomMessageBoxForm.button2.Text = gloCustomMessageBoxForm.GetButtonText(ButtonID.NO);
                        gloCustomMessageBoxForm.button2.DialogResult = DialogResult.No;

                        gloCustomMessageBoxForm.button3.Visible = true;
                        gloCustomMessageBoxForm.button3.Text = gloCustomMessageBoxForm.GetButtonText(ButtonID.CANCEL);
                        gloCustomMessageBoxForm.button3.DialogResult = DialogResult.Cancel;

                        gloCustomMessageBoxForm.CancelButton = gloCustomMessageBoxForm.button3;
                        break;

                    case MessageBoxButtons.OK:
                    default:
                        gloCustomMessageBoxForm.visibleButtonsCount = 1;
                        gloCustomMessageBoxForm.button3.Visible = true;
                        gloCustomMessageBoxForm.button3.Text = gloCustomMessageBoxForm.GetButtonText(ButtonID.OK);
                        gloCustomMessageBoxForm.button3.DialogResult = DialogResult.OK;

                        gloCustomMessageBoxForm.CancelButton = gloCustomMessageBoxForm.button3;
                        break;
                }

                //Set default button (used in gloCustomMessageBoxForm_Shown)
                gloCustomMessageBoxForm.defaultButton = defaultButton;
            }

            #endregion

            #region Private event handlers

            /// <summary>
            /// Handles the Shown event of the gloCustomMessageBoxForm control.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
            private void gloCustomMessageBoxForm_Shown(object sender, EventArgs e)
            {
                int buttonIndexToFocus = 1;
                Button buttonToFocus;

                //Set the default button...
                switch (this.defaultButton)
                {
                    case MessageBoxDefaultButton.Button1:
                    default:
                        buttonIndexToFocus = 1;
                        break;
                    case MessageBoxDefaultButton.Button2:
                        buttonIndexToFocus = 2;
                        break;
                    case MessageBoxDefaultButton.Button3:
                        buttonIndexToFocus = 3;
                        break;
                }

                if (buttonIndexToFocus > this.visibleButtonsCount) buttonIndexToFocus = this.visibleButtonsCount;

                if (buttonIndexToFocus == 3)
                {
                    buttonToFocus = this.button3;
                }
                else if (buttonIndexToFocus == 2)
                {
                    buttonToFocus = this.button2;
                }
                else
                {
                    buttonToFocus = this.button1;
                }

                buttonToFocus.Focus();
            }

            /// <summary>
            /// Handles the LinkClicked event of the richTextBoxMessage control.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">The <see cref="System.Windows.Forms.LinkClickedEventArgs"/> instance containing the event data.</param>
            private void richTextBoxMessage_LinkClicked(object sender, LinkClickedEventArgs e)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Process.Start(e.LinkText);
                }
                catch (Exception)
                {
                    //Let the caller of gloCustomMessageBoxForm decide what to do with this exception...
                    throw;
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }

            }

            /// <summary>
            /// Handles the KeyUp event of the richTextBoxMessage control.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
            void gloCustomMessageBoxForm_KeyUp(object sender, KeyEventArgs e)
            {
                //Handle standard key strikes for clipboard copy: "Ctrl + C" and "Ctrl + Insert"
                if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.Insert))
                {
                    var buttonsTextLine = (this.button1.Visible ? this.button1.Text + STANDARD_MESSAGEBOX_SEPARATOR_SPACES : string.Empty)
                                        + (this.button2.Visible ? this.button2.Text + STANDARD_MESSAGEBOX_SEPARATOR_SPACES : string.Empty)
                                        + (this.button3.Visible ? this.button3.Text + STANDARD_MESSAGEBOX_SEPARATOR_SPACES : string.Empty);

                    //Build same clipboard text like the standard .Net MessageBox
                    var textForClipboard = STANDARD_MESSAGEBOX_SEPARATOR_LINES
                                         + this.Text + Environment.NewLine
                                         + STANDARD_MESSAGEBOX_SEPARATOR_LINES
                                         + this.richTextBoxMessage.Text + Environment.NewLine
                                         + STANDARD_MESSAGEBOX_SEPARATOR_LINES
                                         + buttonsTextLine.Replace("&", string.Empty) + Environment.NewLine
                                         + STANDARD_MESSAGEBOX_SEPARATOR_LINES;

                    //Set text in clipboard
                    Clipboard.SetText(textForClipboard);
                }
            }

            #endregion

            #region Properties (only used for binding)

            /// <summary>
            /// The text that is been used for the heading.
            /// </summary>
            public string CaptionText { get; set; }

            /// <summary>
            /// The text that is been used in the gloCustomMessageBoxForm.
            /// </summary>
            public string MessageText { get; set; }

            #endregion

            #region Public show function

            /// <summary>
            /// Shows the specified message box.
            /// </summary>
            /// <param name="owner">The owner.</param>
            /// <param name="text">The text.</param>
            /// <param name="caption">The caption.</param>
            /// <param name="buttons">The buttons.</param>
            /// <param name="icon">The icon.</param>
            /// <param name="defaultButton">The default button.</param>
            /// <param name="buttomTexts">The custom label texts.</param>
            /// <returns>The dialog result.</returns>
            public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, string[] buttonTexts=null)
            {
                //Create a new instance of the gloCustomMessageBox form
                var gloCustomMessageBoxForm = new gloCustomMessageBoxForm();
                gloCustomMessageBoxForm.ShowInTaskbar = false;

                //Bind the caption and the message text
                gloCustomMessageBoxForm.CaptionText = caption;
                gloCustomMessageBoxForm.MessageText = text;
                gloCustomMessageBoxForm.gloCustomMessageBoxFormBindingSource.DataSource = gloCustomMessageBoxForm;

                //Set the buttons visibilities and texts. Also set a default button.
                SetDialogButtons(gloCustomMessageBoxForm, buttons, defaultButton);
                
                //Set the buttons custom texts for the labels.
                SetDialogButtonTexts(gloCustomMessageBoxForm, buttonTexts);

                //Set the dialogs icon. When no icon is used: Correct placement and width of rich text box.
                SetDialogIcon(gloCustomMessageBoxForm, icon);

                //Set the font for all controls
                gloCustomMessageBoxForm.Font = FONT;
                gloCustomMessageBoxForm.richTextBoxMessage.Font = FONT;

                //Calculate the dialogs start size (Try to auto-size width to show longest text row). Also set the maximum dialog size. 
                SetDialogSizes(gloCustomMessageBoxForm, text, caption);

                //Set the dialogs start position when given. Otherwise center the dialog on the current screen.
                SetDialogStartPosition(gloCustomMessageBoxForm, owner);

                //Show the dialog
                return gloCustomMessageBoxForm.ShowDialog(owner);
            }

            #endregion
        } //class gloCustomMessageBoxForm
    
}
