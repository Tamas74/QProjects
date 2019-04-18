using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloAppointmentScheduling
{
    public partial class frmPrint : Form
    {

        private string _MessageBoxCaption = string.Empty;
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public frmPrint(string AppDate, string AppTime, string AppProvider, string AppLocation, string AppDepartment, string Signprovider, string PatientName)
        {

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
         
            InitializeComponent();
            txtTodaysDate.Text = DateTime.Now.ToString();
            txtDate.Text = AppDate;
            txtTime.Text = AppTime;
            txtAppointmentProvider.Text = AppProvider;
            txtLocation.Text = AppLocation;
            txtDepartment.Text = AppDepartment;
            txtProviderName.Text = Signprovider;
            txtPatientName.Text = PatientName;  
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //printForm1.Print(this, Microsoft.VisualBasic.PowerPacks.Printing.PrintForm.PrintOption.ClientAreaOnly);
            timer1.Stop();
            this.Close();
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }

    }
}