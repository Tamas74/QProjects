using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloGlobal;

namespace gloPM.Forms
{
    public partial class frmSyncPatient : Form
    {

        Tuple<Int64, string, string> DashboardPatient = null;
        Tuple<Int64, string, string> TransactionalPatient = null;
        long PatientID_DB = 0;
        long PatientID_TS = 0;

        public long SelectedPatientId { get; set; }

        public frmSyncPatient()
        {
            InitializeComponent();
        }


        public frmSyncPatient(Tuple<Int64, string, string> Patient_DB, Tuple<Int64, string, string> Patient_TS)
        {
            InitializeComponent();
            DashboardPatient = Patient_DB;
            TransactionalPatient = Patient_TS;
        }

        private void frmSyncPatient_Load(object sender, EventArgs e)
        {
            LoadPatients();
        }

        private void LoadPatients()
        {
            PatientID_DB = DashboardPatient.Item1;
            btnDashboard.Text = btnDashboard.Text + " " + DashboardPatient.Item2 + " - " + DashboardPatient.Item3;
            btnCurrent.Text = btnCurrent.Text + " " + TransactionalPatient.Item2 + " - " + TransactionalPatient.Item3;
            PatientID_TS = TransactionalPatient.Item1;
            SelectedPatientId = PatientID_DB;
        }

        private void savePatientContext(string SettingValue)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@sSettingsName", "SyncPatient", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sSettingsValue", SettingValue, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nClinicID", gloPMGlobal.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nUserID", gloPMGlobal.UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nUserClinicFlag", 2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDB.Execute("gsp_InUpSettings", oDBParameters);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }

            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }


        }


       
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            if (chkRememberme.Checked == true)
            {
                savePatientContext("Dashboard");
            }
            SelectedPatientId = PatientID_DB;
            this.Close();


        }

        private void btnCurrent_Click(object sender, EventArgs e)
        {
            if (chkRememberme.Checked == true)
            {
                savePatientContext("Current");
            }
            SelectedPatientId = PatientID_TS;
            this.Close();
        }

        private void frmSyncPatient_FormClosing(object sender, FormClosingEventArgs e)
        {
            DashboardPatient = null;
            TransactionalPatient = null;
        }
    }
}
