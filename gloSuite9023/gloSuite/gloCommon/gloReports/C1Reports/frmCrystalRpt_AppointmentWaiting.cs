using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using gloBilling;
using System.Collections.Specialized;
using System.Collections;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace gloReports.C1Reports
{
    public partial class frmCrystalRpt_AppointmentWaiting : Form
    {
       
             
        #region " Declarations "

        Rpt_AppointmentsWaiting objRptAppointmentsWaiting = null;

        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        private Int32 nStartdt = 0;
        private Int32 nEnddt = 0;

        private StringBuilder sbProviders = new StringBuilder();
        private StringBuilder sbPatientID = new StringBuilder();

        //private Boolean _IsFromCalendar;
      //  private DataTable _dtProviders;
    //    private bool _IsPrint=false;
        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;

        
        //Array for storing the Months
        private string[] Months = new string[13];

      //  private Boolean FlagPatientMultiselect;

      //  private Boolean FlagCPTMultiselect = true;

     //   private Int64 _setPatientID;

     //   private gloGeneralItem.gloItems ogloItems = null;


        public Int64 dtAgingDate;

        //Variables for Report from Calendar
        public DataTable _dtProvider;
        public DateTime _startdate;
        public DateTime _enddate;
        //bool _IsPrint = false;
        private bool _blnDisposed;
        private static frmCrystalRpt_AppointmentWaiting frm;


        //C1.Win.C1FlexGrid.C1FlexGrid c1AppointmentWaiting = new C1.Win.C1FlexGrid.C1FlexGrid();
        #endregion " Declarations "


        #region  " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion  " Property Procedures "

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
                        if (dtpEndDate != null)
                        {
                            try
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpEndDate);
                            }
                            catch
                            {
                            }
                            dtpEndDate.Dispose();
                            dtpEndDate = null;
                        }
                    }
                    catch
                    {
                    } 

                    try
                    {
                        if (dtpStartDate != null)
                        {
                            try
                            {
                                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpStartDate);
                            }
                            catch
                            {
                            }
                            dtpStartDate.Dispose();
                            dtpStartDate = null;
                        }
                    }
                    catch
                    {
                    }

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

        ~frmCrystalRpt_AppointmentWaiting()
        {
            Dispose(false);
        }

        public static frmCrystalRpt_AppointmentWaiting GetInstance(string DatabaseString)
        {
            try
            {
                if (frm == null)
                {
                    frm = new frmCrystalRpt_AppointmentWaiting(DatabaseString);
                }
            }
            finally
            {

            }
            return frm;
        }

        #region " Constructor "

        public frmCrystalRpt_AppointmentWaiting(string databaseconnectionstring)
        {
            InitializeComponent();

      
            _databaseconnectionstring = databaseconnectionstring;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
        }

       

        #endregion


        #region "Form Events"

        private void frmRpt_AppointmentWaiting_Load(object sender, EventArgs e)
        {
            try
            {
                Fill_FilterDatesCombo();
                FillLocation();
                

                //if (_IsFromCalendar == false)
                //{
                    FillAppointments("", ""); ;
                //}
                //else
                //{
                //    string Providers="";
                //    for (int i = 0; i < _dtProviders.Rows.Count; i++)
                //    {
                //        if (i != 0)
                //        {
                //            Providers = Providers + "," + Convert.ToString(_dtProviders.Rows[i]["ID"]);
                //        }
                //        else
                //        {
                //            Providers =Convert.ToString(_dtProviders.Rows[i]["ID"]);
                //        }
                        
                //    }
                //    if (Providers.EndsWith(",") == true)
                //    {
                //      Providers=Providers.Remove(Providers.Length - 1);
                //    }
                    
                //        FillAppointments(Providers, "");
                //        if (_IsPrint == true)
                //        {
                //            this.Hide();
                //            this.Visible = false;
                //            this.Close();
                //            Application.DoEvents();
                //        }
                
                //        FillLocation();
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

         
        #endregion


        #region " Fill Methods"

        private void FillAppointments(string ProviderID, string PatientID)
        {
            if (objRptAppointmentsWaiting != null)
            {
                objRptAppointmentsWaiting.Dispose();
                objRptAppointmentsWaiting = null;
            }
            objRptAppointmentsWaiting = new Rpt_AppointmentsWaiting();
            dsC1Reports dsReports = new dsC1Reports();
            SqlCommand _sqlcommand = new SqlCommand();
            SqlConnection oConnection = new SqlConnection();

            try
            {
                oConnection.ConnectionString = _databaseconnectionstring;
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = "Rpt_AppointmentsWaiting";
                _sqlcommand.Connection = oConnection;
                _sqlcommand.CommandTimeout = 0;

                _sqlcommand.Parameters.Add("@nFromDate", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters.Add("@nToDate", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters.Add("@clinicID", System.Data.SqlDbType.Int);
                _sqlcommand.Parameters.Add("@nRefFlag", System.Data.SqlDbType.Int);


                _sqlcommand.Parameters["@nFromDate"].Value = nStartdt;
                _sqlcommand.Parameters["@nToDate"].Value = nEnddt;
                _sqlcommand.Parameters["@clinicID"].Value = _ClinicID;
                _sqlcommand.Parameters["@nRefFlag"].Value = 0;


                if (ProviderID != "")
                {

                    _sqlcommand.Parameters.Add("@nProviderID", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@nProviderID"].Value = ProviderID;
                    
                }
            
                if (PatientID != "")
                {
                    _sqlcommand.Parameters.Add("@nPatientID", System.Data.SqlDbType.NVarChar);
                    _sqlcommand.Parameters["@nPatientID"].Value = PatientID;
                    
                }
              
                if (Convert.ToString(cmbLocation.SelectedValue)!="0")
                {
                    _sqlcommand.Parameters.Add("@LocationID", System.Data.SqlDbType.BigInt);
                    _sqlcommand.Parameters["@LocationID"].Value = Convert.ToInt64(cmbLocation.SelectedValue);

                    
                }

                SqlDataAdapter da = new SqlDataAdapter(_sqlcommand);
                da.Fill(dsReports, "dt_AppointmentsWaiting");
                da.Dispose();
                objRptAppointmentsWaiting.SetDataSource(dsReports);

                crvWaitingAppointments.ReportSource = objRptAppointmentsWaiting;
                
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

        // For Filling the Dates Combo
        private void Fill_FilterDatesCombo()
        {
            try
            {
                cmb_datefilter.Items.Clear();
                cmb_datefilter.Items.Add("Custom");
                cmb_datefilter.Items.Add("Today");
                cmb_datefilter.Items.Add("Tomorrow");
                cmb_datefilter.Items.Add("Yesterday");
                cmb_datefilter.Items.Add("This Week");
                cmb_datefilter.Items.Add("Last Week");
                cmb_datefilter.Items.Add("Current Month");
                cmb_datefilter.Items.Add("Last Month");
                cmb_datefilter.Items.Add("Current Year");
                cmb_datefilter.Items.Add("Last 30 Days");
                cmb_datefilter.Items.Add("Last 60 Days");
                cmb_datefilter.Items.Add("Last 90 Days");
                cmb_datefilter.Items.Add("Last 120 Days");
                cmb_datefilter.Refresh();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }


        private void FillLocation()
        {
            gloAppointmentBook.Books.Location oLocation = new gloAppointmentBook.Books.Location();
            DataTable _dtLocations = null;
            try
            {
                _dtLocations = oLocation.GetList();

                DataTable dtLocations;
                if (_dtLocations != null && _dtLocations.Rows.Count > 0)
                {
                    dtLocations = new DataTable();
                    dtLocations.Columns.Add("nLocationID");
                    dtLocations.Columns.Add("slocation");

                    dtLocations.Clear();
                    dtLocations.Rows.Add(0, "");

                    for (int i = 0; i < _dtLocations.Rows.Count; i++)
                    {
                        dtLocations.Rows.Add(_dtLocations.Rows[i]["nLocationID"], _dtLocations.Rows[i]["slocation"]);
                    }

                    cmbLocation.DataSource = dtLocations;
                    cmbLocation.DisplayMember = "slocation";
                    cmbLocation.ValueMember = "nLocationID";
                }
                if (_dtLocations != null)
                {
                    _dtLocations.Dispose();
                    _dtLocations = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oLocation.Dispose();
                oLocation = null;
            }
        }

        public string GetPatientName(Int64 patientID)
        {

            DataTable dtPatient = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _strSQL = "";
            string _result = "";
            try
            {
                oDB.Connect(false);

                //get the provider details in the datatable -- dtProvider
                _strSQL = "select ISNULL( sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) + ISNULL(sLastName,'') AS PatientName FROM Patient WHERE nPatientID = " + patientID;
                oDB.Retrive_Query(_strSQL, out dtPatient);
                if (dtPatient != null)
                {
                    _result = Convert.ToString(dtPatient.Rows[0]["PatientName"]);
                    dtPatient.Dispose();
                    dtPatient = null;
                }


            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                DBErr = null; 

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
            finally
            {


                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return _result;

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

                //Dictionary<Int64, String> dictProviders = new Dictionary<long, string>();
                //dictProviders = _ogloReportViewer.dictProviders;

                //List<Int64> values = new List<Int64>(dictProviders.Keys);
                //values.Sort();
                

                for (int i = 0; i <= cmbProvider.Items.Count - 1; i++)
                {
                    if (i == cmbProvider.Items.Count - 1)
                    {
                        sbProviders.Append(cmbProvider.Items[i].ToString());
                    }
                    else
                    {
                        sbProviders.Append(cmbProvider.Items[i].ToString() + ",");
                    }
                }
                #endregion


                #region "Patient"

                sbPatientID.Remove(0, sbPatientID.Length);

                //Dictionary<Int64, String> dictPatientID = new Dictionary<long, string>();
                //dictPatientID = _ogloReportViewer.dictPatients;

                //List<Int64> lstPatvalues = new List<Int64>(dictPatientID.Keys);
                //lstPatvalues.Sort();

                for (int i = 0; i <= cmbPatients.Items.Count - 1; i++)
                {
                    if (i == cmbPatients.Items.Count - 1)
                    {
                        sbPatientID.Append(cmbPatients.Items[i].ToString());
                    }
                    else
                    {
                        sbPatientID.Append(cmbPatients.Items[i].ToString() + ",");
                    }
                }

                #endregion

               

                FillAppointments(sbProviders.ToString(),sbPatientID.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }


        private void removeOListControl()
        {
            if (oListControl != null)
            {
                if (PnlC1.Controls.Contains(oListControl))
                {
                    PnlC1.Controls.Remove(oListControl);
                }
                oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                oListControl.Dispose();
                oListControl = null;
            }
        }
        private void btnBrowsePatient_Click(object sender, EventArgs e)
        {
            try
            {
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                //Used Only for Patient Statement - Temp Code
                bool FlagPatientMultiselect = true;

                if (FlagPatientMultiselect)
                    oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, true, this.Width);
                else
                    oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, this.Width);


                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Patient";

                _CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                PnlC1.Controls.Add(oListControl);

                if (cmbPatients.DataSource != null)
                {
                    for (int i = 0; i < cmbPatients.Items.Count; i++)
                    {
                        cmbPatients.SelectedIndex = i;
                        cmbPatients.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbPatients.SelectedValue), cmbPatients.Text);
                    }
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnClearPatient_Click(object sender, EventArgs e)
        {
            this.cmbPatients.SelectedIndexChanged -= new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
         //   cmbPatients.Items.Clear();
            cmbPatients.DataSource = null;
            cmbPatients.Items.Clear();
            cmbPatients.Refresh();
            this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
        }

        private void btnBrowseProvider_Click(object sender, EventArgs e)
        {
            try
            {
                //if (oListControl != null)
                //{
                //    for (int i = this.Controls.Count - 1; i >= 0; i--)
                //    {
                //        if (this.Controls[i].Name == oListControl.Name)
                //        {
                //            this.Controls.Remove(this.Controls[i]);
                //            break;
                //        }
                //    }
                //}
                removeOListControl();
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Providers, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Provider";
                _CurrentControlType = gloListControl.gloListControlType.Providers;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbProvider.DataSource != null)
                {
                    for (int i = 0; i < cmbProvider.Items.Count; i++)
                    {
                        cmbProvider.SelectedIndex = i;
                        cmbProvider.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbProvider.SelectedValue), cmbProvider.Text);
                    }
                }
                oListControl.OpenControl();
                oListControl.Dock = DockStyle.Fill;
                oListControl.BringToFront();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnClearProvider_Click(object sender, EventArgs e)
        {
           
            cmbProvider.DataSource = null;
            cmbProvider.Items.Clear();
            cmbProvider.Refresh();
        }

        private void tsb_GenerateReport_Click(object sender, EventArgs e)
        {
            try
            {

                #region "Providers"

                sbProviders.Remove(0, sbProviders.Length);

                //Dictionary<Int64, String> dictProviders = new Dictionary<long, string>();
                //dictProviders = _ogloReportViewer.dictProviders;

                //List<Int64> values = new List<Int64>(dictProviders.Keys);
                //values.Sort();


                for (int i = 0; i <= cmbProvider.Items.Count - 1; i++)
                {
                    if (i == cmbProvider.Items.Count - 1)
                    {
                        sbProviders.Append(cmbProvider.SelectedValue.ToString());
                        cmbProvider.SelectedIndex = i;
                    }
                    else
                    {
                        sbProviders.Append(cmbProvider.SelectedValue.ToString() + ",");
                        cmbProvider.SelectedIndex = i;
                    }
                }
                #endregion


                #region "Patient"

                sbPatientID.Remove(0, sbPatientID.Length);

                //Dictionary<Int64, String> dictPatientID = new Dictionary<long, string>();
                //dictPatientID = _ogloReportViewer.dictPatients;

                //List<Int64> lstPatvalues = new List<Int64>(dictPatientID.Keys);
                //lstPatvalues.Sort();

                for (int i = 0; i <= cmbPatients.Items.Count - 1; i++)
                {
                    if (i == cmbPatients.Items.Count - 1)
                    {
                        sbPatientID.Append(cmbPatients.SelectedValue.ToString());
                        cmbPatients.SelectedIndex = i;
                    }
                    else
                    {
                        sbPatientID.Append(cmbPatients.SelectedValue.ToString() + ",");
                        cmbPatients.SelectedIndex = i;
                    }
                }

                #endregion

                nStartdt = gloDateMaster.gloDate.DateAsNumber(dtpStartDate.Value.ToShortDateString());
                nEnddt = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString());


                FillAppointments(sbProviders.ToString(), sbPatientID.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void tsb_btnExportReport_Click(object sender, EventArgs e)
        {
            crvWaitingAppointments.ExportReport();
        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            crvWaitingAppointments.PrintReport();
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       



        private void cmb_datefilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _filterby = 0;

            _filterby = cmb_datefilter.SelectedIndex;
            switch (_filterby)
            {
                case 0://Date Range
                    FilterBy_DateRange();
                    break;

                case 1://Today
                    FilterBy_Today();
                    break;

                case 2://Tomorrow
                    FilterBy_Tomorrow();
                    break;

                case 3://Yesterday
                    FilterBy_Yesterday();
                    break;

                case 4://This week
                    FilterBy_Thisweek();
                    break;

                case 5://Last Week
                    FilterBy_lastweek();
                    break;

                case 6://Current Month
                    FilterBy_currentmonth();
                    break;

                case 7://Last Month
                    FilterBy_lastmonth();
                    break;

                case 8://Current Year
                    FilterBy_currenYear();
                    break;

                case 9://Last 30 days
                    FilterBy_last30days();
                    break;

                case 10://Last 60 days
                    FilterBy_last60days();
                    break;

                case 11://Last 90 days
                    FilterBy_last90days();
                    break;

                case 12://Last 120 days
                    FilterBy_last120days();
                    break;
            }
        }

        void oListControl_ItemSelectedClick(object sender, EventArgs e)
        {
            int _Counter = 0;

            switch (_CurrentControlType)
            {

                case gloListControl.gloListControlType.Patient:
                    {
                        this.cmbPatients.SelectedIndexChanged -= new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
                        
                        cmbPatients.DataSource = null;
                        cmbPatients.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);

                            }


                            cmbPatients.DataSource = oBindTable;
                            cmbPatients.DisplayMember = "DispName";
                            cmbPatients.ValueMember = "ID";

                        }
                        this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
                    }
                    break;
                case gloListControl.gloListControlType.Providers:
                    {
                       
                        cmbProvider.DataSource = null;
                        cmbProvider.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");

                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                            }

                            cmbProvider.DataSource = oBindTable;
                            cmbProvider.DisplayMember = "DispName";
                            cmbProvider.ValueMember = "ID";
                        }

                    }
                    break;


                default:
                    {
                    }
                    break;
            }
        }

        void oListControl_ItemClosedClick(object sender, EventArgs e)
        {
            removeOListControl();
            //if (oListControl != null)
            //{
            //    for (int i = this.Controls.Count - 1; i >= 0; i--)
            //    {
            //        if (this.Controls[i].Name == oListControl.Name)
            //        {
            //            this.Controls.Remove(this.Controls[i]);
            //            break;
            //        }
            //    }
            //}
        }


        #endregion


        #region " Date Methods "

        private void FilterBy_Today()
        {

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_Tomorrow()
        {
            dtpStartDate.Value = DateTime.Now.AddDays(1);
            dtpEndDate.Value = DateTime.Now.AddDays(1);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_Yesterday()
        {
            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));
            dtpEndDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(24, 0, 0));

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_Thisweek()
        {

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpStartDate.Value = DateTime.Today;
                dtpEndDate.Value = DateTime.Now.Date.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(1, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(2, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(3, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(4, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(5, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(6, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_lastweek()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(7, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(8, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(9, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(10, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);

            }
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(11, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(12, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(13, 0, 0, 0));
                dtpEndDate.Value = dtpStartDate.Value.AddDays(6);
            }

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_currentmonth()
        {
            DateTime dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtTo = dtTo.AddMonths(1);
            dtTo = dtTo.AddDays(-(dtTo.Day));
            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = Convert.ToDateTime(dtTo.Date);

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_lastmonth()
        {
            DateTime firstDay = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);
            int DaysinMonth = DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);
            DateTime lastDay = firstDay.AddMonths(1).AddTicks(-1);
            dtpStartDate.Value = Convert.ToDateTime(firstDay.Date);
            dtpEndDate.Value = Convert.ToDateTime(lastDay.Date);
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_currenYear()
        {

            DateTime dtFrom = new DateTime(DateTime.Now.Year, 1, 1);
            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = DateTime.Today;
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_last30days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(30, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
        }

        private void FilterBy_last60days()
        {
            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(60, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_last90days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(90, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_last120days()
        {

            dtpStartDate.Value = DateTime.Now.Date.Subtract(new TimeSpan(120, 0, 0, 0));
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;

        }

        private void FilterBy_DateRange()
        {

            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;

            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;

        }

      


        #endregion

        private void cmbPatients_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmCrystalRpt_AppointmentWaiting_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

      


    }
}