using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloSecurity
{
    public partial class frmPwdSettings : Form
    {
        private string _databaseConnectionString = "";
        private ClsPwdSettings oPwdSettings;
        //private string _gstrMessageBoxCaption = " gloPMS ";
        //Added By Pramod For Message Box
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public frmPwdSettings(string databaseConnectionString)
        {

            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion

            _databaseConnectionString = databaseConnectionString;
            InitializeComponent();
        }

        private void frmPwdSettings_Load(object sender, EventArgs e)
        {
            fillSettings();         
        }

        /// <summary>
        /// This method display the previous password settings if any
        /// </summary>
        private void fillSettings()
        {
            oPwdSettings = new ClsPwdSettings(_databaseConnectionString);
            DataTable dtSetting = null;
            try
            {
                dtSetting = oPwdSettings.getSettings();

                if (dtSetting != null && dtSetting.Rows.Count > 0)
                {
                    txtCapLetters.Text = Convert.ToString(dtSetting.Rows[0]["ExpCapitalLetters"]);
                    txtLetters.Text = Convert.ToString(dtSetting.Rows[0]["ExpNoOfLetters"]);
                    txtDigits.Text = Convert.ToString(dtSetting.Rows[0]["ExpNoOfDigits"]);
                    txtSpecialChar.Text = Convert.ToString(dtSetting.Rows[0]["ExpNoOfSpecChars"]);
                    txtMinLength.Text = Convert.ToString(dtSetting.Rows[0]["ExpPwdLength"]);
                    txtdays.Text = Convert.ToString(dtSetting.Rows[0]["ExpTimeFrameinDays"]);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (dtSetting != null) { dtSetting.Dispose(); dtSetting = null; }
            }
        }

        /// <summary>
        /// perform action selected by user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {           
            switch (e.ClickedItem.Tag.ToString())
            {
                case "OK":    // Add or modify settings
                    if (ValidateData())
                    {
                        oPwdSettings = new ClsPwdSettings(_databaseConnectionString);

                        oPwdSettings.CapLetters = Convert.ToInt32(txtCapLetters.Text.Trim());
                        oPwdSettings.Letters = Convert.ToInt32(txtLetters.Text.Trim());
                        oPwdSettings.Digits = Convert.ToInt32(txtDigits.Text.Trim());
                        oPwdSettings.SpecialChar = Convert.ToInt32(txtSpecialChar.Text.Trim());
                        oPwdSettings.MinLength = Convert.ToInt32(txtMinLength.Text.Trim());
                        oPwdSettings.NoOfDays = Convert.ToInt32(txtdays.Text.Trim());

                        if (oPwdSettings.SaveSettings())
                        {
                            this.Close();
                        }

                    }
                    break;


                case "Cancel":
                    try
                    {
                        this.Close();
                    }
                    catch (Exception) // ex)
                    {
                        //ex.ToString();
                        //ex = null;
                    }//catch

                    break;
            }
        }

        /// <summary>
        /// Validate the password settings 
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            // set value to '0' if no value is entered
            if (txtCapLetters.Text.Trim() == "")         
                txtCapLetters.Text = "0";
            if (txtLetters.Text.Trim() == "")
                txtLetters.Text = "0";
            if (txtDigits.Text.Trim() == "")
                txtDigits.Text = "0";
            if (txtSpecialChar.Text.Trim() == "")
                txtSpecialChar.Text = "0";
            if (txtMinLength.Text.Trim() == "")         
                txtMinLength.Text = "0";
            if (txtdays.Text.Trim() == "")
                txtdays.Text = "0";

            // there should be atleast 1 letter password
            if (Convert.ToInt32(txtLetters.Text.Trim()) < 1)
            {
                MessageBox.Show("The Password complexity requires atleast 1 letter", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLetters.Focus();
                return false;
            }

            //no of capital letters should not be more than no of letters
            if (Convert.ToInt32(txtLetters.Text.Trim()) <= Convert.ToInt32(txtCapLetters.Text.Trim()))
            {
                MessageBox.Show("The number of capital letters should be less than or equal to the number of letters.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCapLetters.Focus();
                return false;
            }

            // total of letters + special char + digits should not be less than 8
            int totalChar = Convert.ToInt32(txtLetters.Text.Trim()) + Convert.ToInt32(txtSpecialChar.Text.Trim()) + Convert.ToInt32(txtDigits.Text.Trim()); 
            if (totalChar < 8)
            {
                MessageBox.Show("The sum of number of letters , number of digits and number of special characters should be atleast 8.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);               
                txtLetters.Focus();
                return false;
            }

            
            return true;
        }

        /// <summary>
        /// this event make sure that value enterd in text boxes is number only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCapLetters_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // If key pressed is not a number
                if (!(e.KeyChar >= 48 && e.KeyChar <= 57))
                {
                    //If key pressed is not a backspace
                    if(!(e.KeyChar == 8))       
                        e.Handled = true;
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        } 

       
      
    }
}