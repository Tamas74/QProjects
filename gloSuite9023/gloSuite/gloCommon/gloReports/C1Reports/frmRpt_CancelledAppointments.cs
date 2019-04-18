using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace gloReports.C1Reports
{
    public partial class frmRpt_CancelledAppointments : Form
    {

        #region " Declarations "

        //For Creating the object of the User Control
        gloReportViewer _ogloReportViewer;

        //For Creating the Object of the CrystalReport
        Rpt_CancelAppointments objrptCancelAppointments;

        
        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private Int32 nStartdt = 0;
        private Int32 nEnddt = 0;
        string _strUserName = "";

        private StringBuilder sbProviders = new StringBuilder();
        private StringBuilder sbPatientID = new StringBuilder();
        private bool _blnDisposed;
        private static frmRpt_CancelledAppointments frm;
        #endregion " Declarations "


        #region  " Property Procedures "
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!(this._blnDisposed))
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if ((disposing))
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    // Dispose managed resources. 
                    if ((components != null))
                    {
                        components.Dispose();
                    }
                    //frm = Nothing 
                }
                // Release unmanaged resources. If disposing is false, 
                // only the following code is executed. 

                // Note that this is not thread safe. 
                // Another thread could start disposing the object 
                // after the managed resources are disposed, 
                // but before the disposed flag is set to true. 
                // If thread safety is necessary, it must be 
                // implemented by the client. 
            }
            frm = null;
            this._blnDisposed = true;
            base.Dispose(disposing);
        }

        public void Disposer()
        {
            Dispose(true);
            // Take yourself off of the finalization queue 
            // to prevent finalization code for this object 
            // from executing a second time. 
            System.GC.SuppressFinalize(this);
        }

        ~frmRpt_CancelledAppointments()
        {
            Dispose(false);
        }

        public static frmRpt_CancelledAppointments GetInstance(string Databasestring)
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmRpt_CancelledAppointments(Databasestring);
                }
            }
            finally
            {

            }
            return frm;
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "


        #region " Constructor "

        public frmRpt_CancelledAppointments(string databaseconnectionstring)
        {
            InitializeComponent();
            _ogloReportViewer = new gloReportViewer();

            //Attaching the event Handler
            _ogloReportViewer.onReportsClose_Clicked += new gloReportViewer.onReportsCloseClicked(ogloReports_onReportsClose_Clicked);
            _ogloReportViewer.onGenerateReport_Clicked += new gloReportViewer.onGenerateReportClicked(ogloReports_onGenerateReport_Clicked);


            _databaseconnectionstring = databaseconnectionstring;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _strUserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _strUserName = "";
            }

            #endregion
        }

        #endregion


        #region "Form Events"
        private void frmRpt_CancelledAppointments_Load(object sender, EventArgs e)
        {

            try
            {
                //For Addding the ReportViewer User Control in form
                pnlContainer.Controls.Add(_ogloReportViewer);
                _ogloReportViewer.Dock = DockStyle.Fill;

                //For Hiding the controls from the Search Criteria
                _ogloReportViewer.showTransCriteria = true;
                _ogloReportViewer.showDatesCriteria = true;
                _ogloReportViewer.setdatesAsCurrentMonth();
                
                 _ogloReportViewer.showPatientCriteria = true;
                _ogloReportViewer.showPatientMultiselect = true;

                _ogloReportViewer.showProviderCriteria = true;
                _ogloReportViewer.showLocation = true;

                _ogloReportViewer.showCancelAppType = true;

                //Property to show the Export Button on Tool Bar
                _ogloReportViewer.showExport = true;
 
                    
                FillCancelledAppointments("","");
            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        #endregion



        #region "Fill Methods"

        private void FillCancelledAppointments(string ProviderID, string PatientID)
        {
            if (objrptCancelAppointments != null)
            {
                objrptCancelAppointments.Dispose();
                objrptCancelAppointments = null;
            }
            objrptCancelAppointments = new Rpt_CancelAppointments();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            nStartdt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtStartDate.ToShortDateString());
            nEnddt = gloDateMaster.gloDate.DateAsNumber(_ogloReportViewer.dtEndDate.ToShortDateString());

            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_Appointments";
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;

                _sqlcommand.Parameters.Add("@nFromDate", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters.Add("@nToDate", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters.Add("@clinicID", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters.Add("@nTrackingStatus", System.Data.SqlDbType.Int);
                


                _sqlcommand.Parameters["@nFromDate"].Value = nStartdt;
                _sqlcommand.Parameters["@nToDate"].Value = nEnddt;
                _sqlcommand.Parameters["@clinicID"].Value = _ClinicID;

                if(_ogloReportViewer.bNoShowAppointments) 
                _sqlcommand.Parameters["@nTrackingStatus"].Value = 5;
                else if (_ogloReportViewer.bDeletedAppointments)
                _sqlcommand.Parameters["@nTrackingStatus"].Value = 7;
                else
                _sqlcommand.Parameters["@nTrackingStatus"].Value = 6;

                if (ProviderID != "")
                {
                    _sqlcommand.Parameters.Add("@nProviderID", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@nProviderID"].Value = ProviderID;
                }

                if( _ogloReportViewer.sLocation!= null && _ogloReportViewer.sLocation != "")
                {
                    _sqlcommand.Parameters.Add("@LocationID", System.Data.SqlDbType.BigInt);
                    _sqlcommand.Parameters["@LocationID"].Value = _ogloReportViewer.sLocation;
                }

                if (PatientID != "")
                {
                    _sqlcommand.Parameters.Add("@nPatientID", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@nPatientID"].Value = PatientID;
                }
                               

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_Appointments");
                _sqlcommand.Parameters.Clear();
                _sqlcommand.Dispose();
                _sqlcommand = null;
                da.Dispose();
                da = null;
                if (dsReports.Tables[0].Rows.Count > 0)
                {

                    for (int iRowCount = 0; iRowCount <= dsReports.Tables[0].Rows.Count - 1; iRowCount++)
                    {
                        dsReports.Tables[0].Rows[iRowCount]["UserName"] = _strUserName;
                    }

                    if (dsReports.Tables[0].Rows.Count > 0)
                    {

                        for (int iRowCount = 0; iRowCount <= dsReports.Tables[0].Rows.Count - 1; iRowCount++)
                        {
                            dsReports.Tables[0].Rows[iRowCount]["UserName"] = _strUserName;
                            //code below added to show resources and providers together
                            DataTable dt = new DataTable();
                            _sqlcommand = null;
                            _sqlcommand = new SqlCommand();
                            _sqlcommand.CommandType = CommandType.StoredProcedure;
                            _sqlcommand.CommandText = "gsp_GetApptmntResources";
                            _sqlcommand.Connection = oConnection;
                            _sqlcommand.Parameters.Add("@nAppointmentID", System.Data.SqlDbType.BigInt);
                            _sqlcommand.Parameters["@nAppointmentID"].Value = dsReports.Tables[0].Rows[iRowCount]["nMSTAppointmentID"];
                            _sqlcommand.Parameters.Add("@nAppointmentDate", System.Data.SqlDbType.BigInt);
                            _sqlcommand.Parameters["@nAppointmentDate"].Value = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dsReports.Tables[0].Rows[iRowCount]["dtStartDate"]));
                            da = new SqlDataAdapter(_sqlcommand);
                            da.Fill(dt);
                            dsReports.Tables[0].Rows[iRowCount]["sProviderName"] = Convert.ToString(dt.Rows[0][0]);
                            da.Dispose();
                            da = null;
                            dt.Dispose();
                            dt = null;
                            _sqlcommand.Parameters.Clear();
                            _sqlcommand.Dispose();
                            _sqlcommand = null;

                        }
                    }

                }
                else
                {
                    dsReports.Tables[0].Rows.Add();

                    dsReports.Tables[0].Rows[0]["UserName"] = _strUserName;

                    if (_ogloReportViewer.bNoShowAppointments)
                        dsReports.Tables[0].Rows[0]["nAppointmentStatus"] = 5;
                    else if (_ogloReportViewer.bDeletedAppointments)
                        dsReports.Tables[0].Rows[0]["nAppointmentStatus"] = 7;
                    else
                        dsReports.Tables[0].Rows[0]["nAppointmentStatus"] = 6;
                }

                
                objrptCancelAppointments.SetDataSource(dsReports);

                //Binds the Report to the Report viewer
                _ogloReportViewer.ReportViewer = objrptCancelAppointments;


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                if (oConnection != null && oConnection.State == ConnectionState.Open)
                {
                    oConnection.Close();

                }
                if (oConnection != null)
                {
                    oConnection.Dispose();
                    oConnection = null;
                }
            }
        }

        #endregion


        #region "User Control Events"

        //Event For Closing the Form
        private void ogloReports_onReportsClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }


        //Event For Generate Report
        private void ogloReports_onGenerateReport_Clicked(object sender, EventArgs e)
        {
            try
            {

                #region "Providers"

                sbProviders.Remove(0, sbProviders.Length);

                Dictionary<Int64, String> dictProviders = new Dictionary<long, string>();
                dictProviders = _ogloReportViewer.dictProviders;

                List<Int64> values = new List<Int64>(dictProviders.Keys);
                values.Sort();

                for (int i = 0; i <= values.Count - 1; i++)
                {
                    if (i == values.Count - 1)
                    {
                        sbProviders.Append(values[i].ToString());
                    }
                    else
                    {
                        sbProviders.Append(values[i].ToString() + ",");
                    }
                }
                #endregion

                #region "Patient"

                sbPatientID.Remove(0, sbPatientID.Length);

                Dictionary<Int64, String> dictPatientID = new Dictionary<long, string>();
                dictPatientID = _ogloReportViewer.dictPatients;

                List<Int64> lstPatvalues = new List<Int64>(dictPatientID.Keys);
                lstPatvalues.Sort();

                for (int i = 0; i <= lstPatvalues.Count - 1; i++)
                {
                    if (i == lstPatvalues.Count - 1)
                    {
                        sbPatientID.Append(lstPatvalues[i].ToString());
                    }
                    else
                    {
                        sbPatientID.Append(lstPatvalues[i].ToString() + ",");
                    }
                }

                #endregion

                FillCancelledAppointments(sbProviders.ToString(),sbPatientID.ToString());

            }
            catch (SqlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }


        #endregion

   

        private void frmRpt_CancelledAppointments_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

    }
}