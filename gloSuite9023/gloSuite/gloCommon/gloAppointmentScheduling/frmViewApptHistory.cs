using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloAppointmentScheduling
{
    public partial class frmViewApptHistory : Form
    {

        public String _databaseconnectionstring = "";
        public Int64 nAppointmentID = 0;
        DataSet dsAppointmentHistory;

        public Int64 AppointmentID
        {
            get { return nAppointmentID; }
            set { nAppointmentID = value; }
        }

        public frmViewApptHistory()
        {
            InitializeComponent();
        }

        public frmViewApptHistory(string _databaseConnString)
        {
            InitializeComponent();
            _databaseconnectionstring = _databaseConnString;
        }
        private void frmViewApptHistory_Load(object sender, EventArgs e)
        {
            gloC1FlexStyle.Style(grdApptLog, true);
            Fill_AppointmentHistory(nAppointmentID);
        }

        public void Fill_AppointmentHistory(Int64 nAppointmentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            dsAppointmentHistory = new DataSet();

            try
            {
                oDBParameters.Add("@nMSTAppointmentID", nAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetAppointmentHistory", oDBParameters, out  dsAppointmentHistory);

                dsAppointmentHistory.Tables[0].TableName = "AppointmentHistory";
                grdApptLog.SetDataBinding(dsAppointmentHistory, "AppointmentHistory", true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDBParameters.Dispose();
                oDBParameters = null;
                oDB = null;
                
            }
        }

        private void frmViewApptHistory_FormClosed(object sender, FormClosedEventArgs e)
        {
            dsAppointmentHistory.Dispose();
            dsAppointmentHistory = null;
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
