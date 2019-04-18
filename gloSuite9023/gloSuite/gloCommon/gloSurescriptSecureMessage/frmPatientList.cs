using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloSurescriptSecureMessage
{
    public partial class frmPatientList : Form
    {

        private gloPatient.PatientListControl oPatientListControl;
       
        public frmPatientList()
        {
            InitializeComponent();
        }

        private string _userName = "";
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _DatabaseConnection = "";
        public string DatabaseConnection
        {
            get { return _DatabaseConnection; }
            set { _DatabaseConnection = value; }
        }


        private long _patientID = 0;
        public long PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        } 

        private void frmPatientList_Load(object sender, EventArgs e)
        {
            oPatientListControl = new gloPatient.PatientListControl();
            //string sConnectionString = "server=glosvr02\\sql2008r2;database=glo7030_Dev;Integrated security=True";
            oPatientListControl.DatabaseConnection = DatabaseConnection;
            oPatientListControl.UserName = UserName;
            oPatientListControl.Grid_DoubleClick += new gloPatient.PatientListControl.GridDoubleClick(oPatientListControl_Grid_DoubleClick);
            oPatientListControl.GridRowSelect_Click += new gloPatient.PatientListControl.GridRowSelectHandler(oPatientListControl_GridRowSelect_Click);
            oPatientListControl.Grid_MouseDown += new gloPatient.PatientListControl.GridMouseDownHandler(oPatientListControl_Grid_MouseDown);
            oPatientListControl.ItemClosedClick += new gloPatient.PatientListControl.ItemClosed(oPatientListControl_ItemClosedClick);
           
            oPatientListControl.Dock = DockStyle.Fill;
            this.Width = 900;
            oPatientListControl.ControlHeader = "Patient";
            this.Controls.Add(oPatientListControl);
            oPatientListControl.FillPatients();
            oPatientListControl.ShowOKCancel(true);
            oPatientListControl.ShowHeader(true);
            oPatientListControl.Dock = DockStyle.Fill;
            oPatientListControl.BringToFront();
        }

        private void oPatientListControl_Grid_DoubleClick(object sender, EventArgs e)
        {
           
            try
            {
                
                long ID = oPatientListControl.SelectedPatientID;
                string namee = oPatientListControl.FirstName + " " + oPatientListControl.LastName;
                PatientID = oPatientListControl.SelectedPatientID;
                string sError = SecureMessage.ValidateZipCode(PatientID);
                if (sError != "")
                {
                    MessageBox.Show(sError, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    PatientID = 0;
                    return;
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
               
            }

        }

        private void oPatientListControl_GridRowSelect_Click(object sender, EventArgs e)
        {
        
        }

        private void oPatientListControl_Grid_MouseDown(object sender, EventArgs e)
        {
        }

        private void oPatientListControl_ItemClosedClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void oPatientListControl_ItemSavedClick(object sender, EventArgs e)
        {
        }

    }
}
