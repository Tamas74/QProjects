using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloReminder
{
    public partial class frmSetupReminder : Form
    {
        private string _databaseconnectionstring = "";
        //private string _messageBoxCaption = "gloPM";
        private string _messageBoxCaption = String.Empty;
        private Int64 _userID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


        public frmSetupReminder()
        {
            InitializeComponent();
            _databaseconnectionstring = appSettings["DataBaseConnectionString"].ToString();
            _userID = Convert.ToInt64(appSettings["UserID"]);
            //Sandip Darade  20090428
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
        }

        public frmSetupReminder(string DatabaseConnectionString)
        {
            InitializeComponent();
            _databaseconnectionstring = DatabaseConnectionString;
            _userID = Convert.ToInt64(appSettings["UserID"]);
            //Sandip Darade  20090428
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
        }

        private void tls_Top_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Tag.ToString())
                {
                    case "OK":
                        {
                            SaveReminder();
                            this.Close();
                        }
                        break;
                    case "Cancel":
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveReminder()
        {
            try
            {
                Reminder oReminder = new Reminder();

                oReminder.Description = txtDescription.Text;
                oReminder.IsDismissed = false;
                oReminder.OwnerID = _userID;
                oReminder.Place = txtLocation.Text;
                oReminder.ReferanceID = 0;
                oReminder.ReferenceType = ReferenceType.Appointment;
                oReminder.ReminderDate = dtpStartDate.Value;                
                oReminder.ReminderStartDate = dtpStartDate.Value;
                oReminder.ReminderStartTime = dtpStartTime.Value;
                oReminder.ReminderEndDate = dtpEndDate.Value;
                oReminder.ReminderEndTime = dtpEndTime.Value;

                switch (cmbReminderIntervel.SelectedItem.ToString())
                {
                    case "0 Minutes":
                        oReminder.ReminderIntervalMinutes = 0;
                        break;
                    case "5 Minutes":
                        oReminder.ReminderIntervalMinutes = 5;
                        break;
                    case "10 Minutes":
                        oReminder.ReminderIntervalMinutes = 10;
                        break;
                    case "15 Minutes":
                        oReminder.ReminderIntervalMinutes = 15;
                        break;
                    case "30 Minutes":
                        oReminder.ReminderIntervalMinutes = 30;
                        break;                    
                    default:
                        oReminder.ReminderIntervalMinutes = 0;
                        break;
                }

                oReminder.ReminderTime = oReminder.ReminderStartTime.AddMinutes(-oReminder.ReminderIntervalMinutes);

                oReminder.Add(oReminder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}