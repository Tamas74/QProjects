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
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;
using CrystalDecisions.CrystalReports.Engine;

namespace gloReports
{
    public partial class gloReportViewer : UserControl
    {

        #region "Variable Declarations"

        gloListControl.gloListControl oListControl = null;
        gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        private string _databaseconnectionstring = "";
        private Int64 _ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

       
        //Dictionary For storing PatientDetails,ProviderDetails...
        private Dictionary<Int64, String> _dictPatients = new Dictionary<Int64, String>();
        private Dictionary<Int64, String> _dictProviders = new Dictionary<Int64, String>();
        private Dictionary<Int64, String> _dictCPT = new Dictionary<Int64, String>();
        private Dictionary<Int64, String> _dictInsurance = new Dictionary<Int64, String>();
        private Dictionary<Int64, String> _dictDgCode = new Dictionary<Int64, String>();
        private Dictionary<Int64, String> _dictAppointmentType = new Dictionary<Int64, String>();

        private Dictionary<Int64, String> _dictlstMonths = new Dictionary<Int64, String>();
        private Dictionary<Int64, String> _dictResource = new Dictionary<Int64, String>();
        //Array for storing the Months
        private string[] Months =new string[13];

        private string[] ReportType = new string[4];

        private Boolean FlagPatientMultiselect;

        private Boolean FlagCPTMultiselect=true;
       
        private Int64 _setPatientID;

        private gloGeneralItem.gloItems ogloItems = null;


        public Int64 dtAgingDate;

        //Variables for Report from Calendar
        public DataTable _dtProvider;
        public DateTime _startdate;
        public DateTime _enddate;
        bool _IsPrint = false;

        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);
        #endregion

        
        # region "Properties "


            #region "Crystal Report Properties"
            public object ReportViewer
                {
                    //Property For Setting the Report source 
                    get { return crvReportViewer.ReportSource; }
                    set { crvReportViewer.ReportSource = value; }
                }
                       
                public string ReportViewerSelectionFormula
                {
                    //Property For Setting the Report Selection Formula 
                    get { return crvReportViewer.SelectionFormula; }
                    set { crvReportViewer.SelectionFormula = value; }
                }
            #endregion

            #region "Show Hide Properties"

            public Boolean showTransCriteria
            {
                get { return pnlTransDate.Visible; }
                set { pnlTransDate.Visible = value; }
            }

            public Boolean showReportCriteria
        {
            get { return pnelReportType.Visible; }
            set { pnelReportType.Visible = value; }
        }
        
            public Boolean showDatesCriteria
            {
                get { return pnlDates.Visible; }
                set { pnlDates.Visible = value; }
            }
        
            public Boolean showPatientCriteria
            {
                get { return pnlPatients.Visible; }
                set { pnlPatients.Visible = value; }
            }

       

            public Boolean showProviderCriteria
            {
                get { return pnlProvider.Visible; }
                set { pnlProvider.Visible = value; }
            }

            public Boolean showInsuranceCriteria
            {
                get { return pnlInsurance.Visible; }
                set { pnlInsurance.Visible = value; }
            }

            public Boolean showCPTCriteria
            {
                get { return pnlCPT.Visible; }
                set { pnlCPT.Visible = value; }
            }

            public Boolean showDiagnosisCriteria
            {
                get { return pnlDgCode.Visible; }
                set { pnlDgCode.Visible = value; }
            }

            public Boolean showFacilityCriteria
            {
                get { return pnlFacility.Visible; }
                set { pnlFacility.Visible = value; }
            }

            public Boolean showCityCriteria
            {
                get { return pnlCity.Visible; }
                set { pnlCity.Visible = value; }
            }

            public Boolean showStateCriteria
            {
                get { return pnlState.Visible; }
                set { pnlState.Visible = value; }
            }

            public Boolean showZipCriteria
            {
                get { return pnlZip.Visible; }
                set { pnlZip.Visible = value; }
            }

            public Boolean showYearCriteria
            {
                get { return pnlYear .Visible; }
                set { pnlYear.Visible = value; }
            }

            public Boolean showMonthCriteria
            {
                get { return pnlMonth.Visible; }
                set { pnlMonth.Visible = value; }
            }

            public Boolean showAgingCriteria
            {
                get { return pnlAgingCriteria.Visible; }
                set { pnlAgingCriteria.Visible = value; }
            }

            public Boolean showLocation
            {
                get { return pnlLocation.Visible; }
                set { pnlLocation.Visible = value; }
            }

            public Boolean showAppointmentFlag
            {
                get { return pnlAppFlag.Visible; }
                set { pnlAppFlag.Visible = value; }
            }

            public Boolean showFiterCritiria
        {
            get { return fpnlCriteria.Visible; }
            set { fpnlCriteria.Visible = value; }
        }

            public Boolean showAppointmentType
            {
                get { return pnlAppType.Visible; }
                set { pnlAppType.Visible = value; }
            }

            public Boolean showCancelAppType
            {
                get { return pnlCancelApp.Visible; }
                set { pnlCancelApp.Visible = value; }
            }

            public Boolean showAmountType
            {
                get { return pnlAmountType.Visible; }
                set { pnlAmountType.Visible = value; }
            } 

            public Boolean showBothChrgAllowed
            {
                get { return rbBoth.Visible; }
                set { rbBoth.Visible = value; }
            }

            public Boolean showExport
            {
                get { return tsb_btnExportReport.Visible; }
                set { tsb_btnExportReport.Visible = value; }
            }
  
            public Boolean showGenerateBatch
            {
                 get { return tsb_btnGenerateBatch.Visible; }
                 set { tsb_btnGenerateBatch.Visible = value; }
            }

            public Boolean showListMonths
            {
                get { return pnlLSTMonths.Visible; }
                set { pnlLSTMonths.Visible = value; }
            }

            public Boolean showActivePayTray
            {
                get { return pnlActivePayTray.Visible; }
                set { pnlActivePayTray.Visible = value; }
            }

            public Boolean showClosedPayTray
            {
                get { return pnlClosedPayTray.Visible; }
                set { pnlClosedPayTray.Visible = value; }
            }

            public Boolean showPayTraySelection
            {
                get { return pnlTraySelection.Visible; }
                set { pnlTraySelection.Visible = value; }
            }

            public Boolean showMultiFacility
            {
                get { return pnlMultiFacility.Visible; }
                set { pnlMultiFacility.Visible = value; }
            }

            public Boolean showMultiChargesTray
            {
                get { return pnlMultiChargesTray.Visible; }
                set { pnlMultiChargesTray.Visible = value; }
            }

            public Boolean showMultiPayTray
            {
                get { return pnlMultiPayTray.Visible; }
                set { pnlMultiPayTray.Visible = value; }
            }

            public Boolean showUser
            {
                get { return pnlUser.Visible; }
                set { pnlUser.Visible = value; }
            }
            
            public Boolean showSealCharges
            {
                get {
                    //SLR: Why 2 returns 4/24/2014?
                        return pnlSealCharges.Visible;
                      //  return pnlSummaryContainer.Visible;
                    }
                set { 
                        pnlSealCharges.Visible = value;
                        pnlSummaryContainer.Visible = value;
                    }
            }

            public Boolean showLoginUsers
            {
                get
                { return pnlLoginUsers.Visible; }
                set
                { pnlLoginUsers.Visible = value; }
            }


            public Boolean showRunReportAs
            {
                get
                {return pnlRunReportAs.Visible;}
                set
                { pnlRunReportAs.Visible = value; }
            }


            public Boolean showAppointmentSort
            {
                get
                { return pnlAppointmentSort.Visible; }
                set
                { pnlAppointmentSort.Visible = value; }
            }

            public Boolean showresourcepanel
            {
                get { return pnlResource.Visible  ; }
                set { pnlResource.Visible = value; }
            }


            #endregion

            #region "Dictonaries Property"

            public Dictionary<Int64,String>  dictPatients
            {
                //Property for storing the PatientID and Name
                get 
                {
                    if (FlagPatientMultiselect)
                    {
                        this.cmbPatients.SelectedIndexChanged -= new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
                        _dictPatients.Clear();
                        for (int i = 0; i < cmbPatients.Items.Count; i++)
                        {
                           
                            cmbPatients.SelectedIndex = i;
                            cmbPatients.Refresh();
                            //_dictPatients.Add(Convert.ToInt64(cmbPatients.SelectedValue), cmbPatients.Text);
                            if(cmbPatients.SelectedValue != null)
                            _dictPatients.Add(Convert.ToInt64(cmbPatients.SelectedValue), cmbPatients.Text);
                        
                        }
                        this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
                        
                    }
                    return _dictPatients;
                }
               
            }
           
            public Dictionary<Int64, String> dictProviders
            {
                //Property for storing the ProviderID and Name
                get
                {
                    _dictProviders.Clear();
                    for (int i = 0; i < cmbProvider.Items.Count; i++)
                    {
                        cmbProvider.SelectedIndex = i;
                        cmbProvider.Refresh();
                        _dictProviders.Add(Convert.ToInt64(cmbProvider.SelectedValue), cmbProvider.Text);
                    }
                    return _dictProviders;
                }

            }
           
            public Dictionary<Int64, String> dictCPT
            {
                //Property for storing the CPTCode and Name
                get
                {
                    _dictCPT.Clear();
                    for (int i = 0; i < cmbCPT.Items.Count; i++)
                    {
                        cmbCPT.SelectedIndex = i;
                        cmbCPT.Refresh();
                        _dictCPT.Add(Convert.ToInt64(cmbCPT.SelectedValue), cmbCPT.Text);
                    }
                    return _dictCPT;
                }

            }

            public Dictionary<Int64, String> dictInsurance
            {
                //Property for storing the InsuranceID and Name
                get
                {
                    _dictInsurance.Clear();
                    //for (int i = 0; i < cmbInsurance.Items.Count; i++)
                    //{
                    //    cmbInsurance.SelectedIndex = i;
                    //    cmbInsurance.Refresh();
                    //    _dictInsurance.Add(Convert.ToInt64(cmbInsurance.SelectedValue), cmbInsurance.Text);
                    //}
                    if (ogloItems != null && ogloItems.Count > 0)
                    {
                        for (int i = 0; i < ogloItems.Count; i++)
                        {
                            _dictInsurance.Add(ogloItems[i].ID, ogloItems[i].Description);
                        }
                    }
                    return _dictInsurance;
                }

            } 
                   
            public Dictionary<Int64, String> dictDgCode
            {
                //Property for storing the InsuranceID and Name
                get
                {
                    _dictDgCode.Clear();
                    for (int i = 0; i < cmbDiagnosisCode.Items.Count; i++)
                    {
                        cmbDiagnosisCode.SelectedIndex = i;
                        cmbDiagnosisCode.Refresh();
                        _dictDgCode.Add(Convert.ToInt64(cmbDiagnosisCode.SelectedValue), cmbDiagnosisCode.Text);
                    }
                    return _dictDgCode;
                }

            }


            public Dictionary<Int64, String> dictappointmentType
            {
                //Property for storing the ProviderID and Name
                get
                {
                    _dictAppointmentType.Clear();
                    for (int i = 0; i < cmbApp_AppointmentType.Items.Count; i++)
                    {
                        cmbApp_AppointmentType.SelectedIndex = i;
                        cmbApp_AppointmentType.Refresh();
                        _dictAppointmentType.Add(Convert.ToInt64(cmbApp_AppointmentType.SelectedValue), cmbApp_AppointmentType.Text);
                    }
                    return _dictAppointmentType;
                }

            }


            public Dictionary<Int64, String> dictMonthsLst
            {
                //Property for storing the ProviderID and Name
                get
                {
                    _dictlstMonths.Clear();
                    for (int i = 0; i < trvMonths.Nodes.Count; i++)
                    {
                        if (trvMonths.Nodes[i].Checked)
                        {
                            _dictlstMonths.Add(Convert.ToInt64(trvMonths.Nodes[i].Tag), trvMonths.Nodes[i].Text);
                        }
                    }
                    return _dictlstMonths;
                }

            }

            public Dictionary<Int64, String> dictResource
            {
                get
                {
                    _dictResource.Clear();
                    for (int i = 0; i < cmbResouce.Items.Count; i++)
                    {
                        cmbResouce.SelectedIndex = i;
                        cmbResouce.Refresh();
                        _dictResource.Add(Convert.ToInt64(cmbResouce.SelectedValue), cmbResouce.Text);
                    }
                    return _dictResource;
                }


            }
            #endregion

            #region "Other Properties"

            public Int64 ClinicID
            {
                get { return _ClinicID; }
                set { _ClinicID = value; }
            }

            public Int64  nPatientID
            {
                
                get 
                {
                    if (!FlagPatientMultiselect)
                    {
                        if (cmbPatients.SelectedValue != null)
                            return Convert.ToInt64(cmbPatients.SelectedValue);
                        else
                            return 0;
                    }
                    else
                    {
                        //return 0;
                        if (cmbPatients.SelectedValue != null)
                            return Convert.ToInt64(cmbPatients.SelectedValue);
                        else
                            return 0;
                    }
                }
                set 
                {
                    cmbPatients.SelectedValue = value;
                }
            }

            public String sFacilityCode
            {
                //Property For storing the FacilityCode
                get {

                    if (cmbFacility.SelectedValue != null)
                        if (cmbFacility.SelectedValue.ToString() != "0")
                            return cmbFacility.SelectedValue.ToString();
                        else
                            return "";
                    else
                        return "";
                        
                    }

                set { cmbFacility.SelectedValue = value; }
            }

            public String sCity
            {
                //Property For storing the City
                get { return txtCity.Text.ToString().Replace("'", "''"); }
                set { txtCity.Text = value; }
            }

            public String sState
            {
                //Property For storing the State
                get { return txtState.Text.ToString().Replace("'", "''"); }
                set { txtState.Text = value; }
            }
                  
            public String sZip
            {
                //Property For storing the Zip
                get { return txtZipCode.Text.ToString().Replace("'", "''"); }
                set { txtZipCode.Text = value; }
            }

            public DateTime dtStartDate
            {
                //Property For storing the StartDate
                get { return dtpStartDate.Value; }
                set { dtpStartDate.Value = value; }
            }

            public DateTime dtEndDate
            {
                //Property For storing the EndDate
                get { return dtpEndDate.Value; }
                set { dtpEndDate.Value = value; }
            }

            public Decimal  sYear
            {
                get { return ndrpYear.Value; }
                set { ndrpYear.Value = value; }
            }

            public Int32 nMonth
            {
                get { return drpMonth.SelectedIndex; }
                set { drpMonth.SelectedIndex = value; }
            }

        public Int32 nReportType
        {
            get { return cmbReportType.SelectedIndex; }
            set { cmbReportType.SelectedIndex = value; }
        }
                  
            public Boolean showPatientMultiselect
            {
                //Flag For Showing/Hiding the Multi Select in a PatientList Control
                get {return FlagPatientMultiselect;}
                set { FlagPatientMultiselect = value; }
            }


            public Boolean showCPTMultiselect
            {
                //Flag For Showing/Hiding the Multi Select in a PatientList Control
                get { return FlagCPTMultiselect; }
                set { FlagCPTMultiselect = value; }
            }

            public Int64 setPatientID
            {
                get { return _setPatientID; }
                set { _setPatientID=value;}
            }

            public void setdatesAsCurrentMonth()
            {
                 FilterBy_currentmonth();
            }

            public void setCrystalReportViewerProperties()
            {
                crvReportViewer.ShowExportButton = false;
                crvReportViewer.ShowParameterPanelButton = false;
                crvReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            }


            public String sLocation
            {
                //Property For storing the FacilityCode
                get
                {

                    if (cmbLocation.SelectedValue != null)
                        if (cmbLocation.SelectedValue.ToString() != "0")
                            return cmbLocation.SelectedValue.ToString();
                        else
                            return "";
                    else
                        return "";

                }

                set { cmbLocation.SelectedValue = value; }
            }

            public Boolean bAppointmentFlag
            {
                //Flag For Showing/Hiding the Multi Select in a PatientList Control
                get { return rbSortEstablishedPatient.Checked; }
                set { rbSortEstablishedPatient.Checked = value; }
            }

            public Boolean bCharges
            {
                //Flag For Showing/Hiding the Multi Select in a PatientList Control
                get { return rbCharges.Checked; }
                set { rbCharges.Checked = value; }
            }
        //abhisekh 10/02/2010 for insurance plan report----------------start----------------
        public Boolean bReportWithCompany
            {
                //Flag For Showing/Hiding the Multi Select in a PatientList Control
                get { return rdoWithName.Checked; }
                set { rdoWithName.Checked = value; }
            }
        public Boolean bReportWithOutCompany
        {
            //Flag For Showing/Hiding the Multi Select in a PatientList Control
            get { return rdoWithoutName.Checked; }
            set { rdoWithoutName.Checked = value; }
        }

        //abhisekh 10/02/2010 for insurance plan report----------------End----------------


        //abhisekh 10/02/2010 for insurance plan report----------------start----------------
        public Boolean bReportWithInsuranceRptCtgry
        {
            //Flag For Showing/Hiding the Multi Select in a PatientList Control
            get { return rdoWithInsRepCat.Checked; }
            set { rdoWithInsRepCat.Checked = value; }
        }
        public Boolean bReportWithOutInsuranceRptCtgry
        {
            //Flag For Showing/Hiding the Multi Select in a PatientList Control
            get { return rdoWithoutInsRepCat .Checked; }
            set { rdoWithoutInsRepCat.Checked = value; }
        }

        //abhisekh 10/02/2010 for insurance plan report----------------End----------------

        public Boolean bAllowed
        {
            //Flag For Showing/Hiding the Multi Select in a PatientList Control
            get { return rbAllowed.Checked; }
            set { rbAllowed.Checked = value; }
        }
      
            public Boolean bBoth
            {
                //Flag For Showing/Hiding the Multi Select in a PatientList Control
                get { return rbBoth.Checked; }
                set { rbBoth.Checked = value; }
            }


            public Boolean bCancelAppointments
            {
                //Flag For Showing/Hiding the Multi Select in a PatientList Control
                get { return rbCancelAppointments.Checked; }
                set { rbCancelAppointments.Checked = value; }
            }

            public Boolean bNoShowAppointments
            {
                //Flag For Showing/Hiding the Multi Select in a PatientList Control
                get { return rbNoShowAppointments.Checked; }
                set { rbNoShowAppointments.Checked = value; }
            }

            public Boolean bDeletedAppointments
            {
                //Flag For Showing/Hiding the Multi Select in a PatientList Control
                get { return rbDeletedAppointments.Checked; }
                set { rbDeletedAppointments.Checked = value; }
            }

            public Int64 nAvtivePayTray
            {
              
                get
                {
                    if (rbActivePayTray.Checked)
                    {
                        if (cmbActivePayTray.SelectedValue != null)
                        if (cmbActivePayTray.SelectedValue.ToString() != "0")
                            return Convert.ToInt64(cmbActivePayTray.SelectedValue);
                        else
                            return 0;
                      else
                        return 0;
                    }
                    else
                    {
                        if (cmbClosedPayTray.SelectedValue != null)
                            if (cmbClosedPayTray.SelectedValue.ToString() != "0")
                                return Convert.ToInt64(cmbClosedPayTray.SelectedValue);
                            else
                                return 0;
                        else
                            return 0;
                    }
                }

                set {
                    if (rbActivePayTray.Checked)
                    {
                        cmbActivePayTray.SelectedValue = value;
                    }
                    else
                    {
                        cmbClosedPayTray.SelectedValue = value;
                    }
                }
            }

            public Boolean bActivePaymentTray
            {
                get { return rbActivePayTray.Checked; }
                set { rbActivePayTray.Checked = value; }
            }

            public Boolean bClosedPaymentTray
            {
                get { return rbClosedPayTray.Checked; }
                set { rbClosedPayTray.Checked = value; }
            }

            public Boolean bsortAppointmentDates
            {
                //Flag For Showing/Hiding the Multi Select in a PatientList Control
                get { return rdoFstLst.Checked; }
                set { rdoFstLst.Checked = value; }
            }


            #endregion


        #endregion


        #region "Delegates"

                //Delegates For Close Event
                public delegate void onReportsCloseClicked(object sender, EventArgs e);
                public event onReportsCloseClicked onReportsClose_Clicked;
                
                //Delegates For Generate Report Event
                public delegate void onGenerateReportClicked(object sender, EventArgs e);
                public event onGenerateReportClicked onGenerateReport_Clicked;


                //Delegates For Patient Selection Index Event
                public delegate void onPatientsSelectedIndexChanged(object sender, EventArgs e);
                public event onPatientsSelectedIndexChanged onPatients_SelectedIndexChanged;


                //Delegates For Patient Selection Index Event
                public delegate void onGenerateBatchClicked(object sender, EventArgs e);
                public event onGenerateBatchClicked onGenerateBatch_Clicked;

                // GLO2010-0006121 : Printing schedule/calendar defaults to manual feed
                // Delegate & event added for print report event
                public delegate void onPrintReportClicked(object sender, EventArgs e);
                public event onPrintReportClicked onPrintReport_Clicked; 

                
        #endregion


        #region "Constructors"

            public gloReportViewer()
            {
                InitializeComponent();
                if (appSettings["ClinicID"] != null)
                {
                    if (appSettings["ClinicID"] != "")
                    { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                    else { _ClinicID = 0; }
                }
                else
                { _ClinicID = 0; }

                if (appSettings["DataBaseConnectionString"] != null)
                    _databaseconnectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]);


                //To Assign the Months to the Array
                Months[0] = "ALL";
                Months[1] = "January";
                Months[2] = "Feburary";
                Months[3] = "March";
                Months[4] = "April";
                Months[5] = "May";
                Months[6] = "June";
                Months[7] = "July";
                Months[8] = "August";
                Months[9] = "September";
                Months[10] = "October";
                Months[11] = "November";
                Months[12] = "December";

                //To Assign the Months to the Array
                ReportType[0] = "Insurance Plan by Company";
                ReportType[1] = "Insurance Plans without Company";
                ReportType[2] = "Insurance Plans with Insurance Reporting Category";
                ReportType[3] = "Insurance Plans without Insurance Reporting Category";
              
            }

        public gloReportViewer(DataTable dt, DateTime startdate, DateTime enddate, bool IsPrint)//Added By MaheshB
        { 
            InitializeComponent();

            _dtProvider = dt.Copy();
            _startdate=startdate;
            _enddate=enddate;
            _IsPrint = IsPrint;
           
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            if (appSettings["DataBaseConnectionString"] != null)
                _databaseconnectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]);


            //To Assign the Months to the Array
            Months[0] = "ALL";
            Months[1] = "January";
            Months[2] = "Feburary";
            Months[3] = "March";
            Months[4] = "April";
            Months[5] = "May";
            Months[6] = "June";
            Months[7] = "July";
            Months[8] = "August";
            Months[9] = "September";
            Months[10] = "October";
            Months[11] = "November";
            Months[12] = "December";
        }
        #endregion

        
        #region "Form Events "


        private void gloReportViewer_Load(object sender, EventArgs e)
        {
            if (_setPatientID != 0)
            {
                string sPatientName=GetPatientName(_setPatientID);
                DataTable dtPat = new DataTable();
                dtPat.Columns.Add("ID");
                dtPat.Columns.Add("Name");

                DataRow dr = dtPat.NewRow();
                dr["ID"] = _setPatientID;
                dr["Name"] = sPatientName;
                dtPat.Rows.Add(dr);
                dtPat.AcceptChanges();

                this.cmbPatients.SelectedIndexChanged -= new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
                cmbPatients.DataSource = dtPat;
                cmbPatients.DisplayMember = "Name";
                cmbPatients.ValueMember = "ID";
                cmbPatients.SelectedIndex = 0; 
                this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
                
                //ListItem liPatients = new ListItem(sPatientName.ToString(), _setPatientID.ToString());
                //cmbPatients.Items.Add(liPatients);
                //cmbPatients.SelectedItem = liPatients;
              
            }
                if (_dtProvider != null)
                {
                  
                    cmbProvider.DataSource = null;
                    cmbProvider.Items.Clear();
                    cmbProvider.DataSource = _dtProvider;
                    cmbProvider.DisplayMember = "DispName";
                    cmbProvider.ValueMember = "ID";
                }
                
            Fill_FilterDatesCombo();
            FillFacilities();
            FillMonths();
            FillReportType();

            FillAgingCriteria();

            FillLocation();

            FillActivePaymentTray();
            FillClosedPaymentTray();

            AdjustCriteriaPanelHight();
            cmb_datefilter.SelectedIndex = 0;
          
            if (_dtProvider != null)
            { 
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
                dtpStartDate.Checked = true;
                dtpEndDate.Checked = true;
                dtpStartDate.Value = _startdate;
                dtpEndDate.Value = _enddate;
                
            }

            lblbtnDown.Visible = false;
            btnUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnUP.BackgroundImageLayout = ImageLayout.Center;
            btnDown.Visible = false;

            //btnTrayUp.BackgroundImage = global::gloReports.Properties.Resources.UP;
            //btnTrayUp.BackgroundImageLayout = ImageLayout.Center;
            //btnTrayDown.Visible = false;

            btnTrayDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnTrayDown.BackgroundImageLayout = ImageLayout.Center;
            btnTrayUp.Visible = false;
           
            timer1.Start();
            if (cmb_datefilter.SelectedIndex == 0)
            {
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
            }
            else
            {
                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;
            }
        }
             
        //For Form close
        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            onReportsClose_Clicked(sender, e);
        }
                   
        //For Printing the Report
        public void tsb_Print_Click(object sender, EventArgs e)
        {
            try
            {
                // GLO2010-0006121 : Even if user selected the default printer, print dialogue was coming
                // Check if comes from EMR
                if (Convert.ToString(appSettings["MessageBOXCaption"]).Equals("gloEMR"))
                {
                    // Check if comes from Schedule/Appointments
                    if (this.ParentForm.Name == "frmRpt_Appointments")
                    {
                        // Raise the Print report click event
                        // This event will check for the default printer settings & will send the print accordingly.
                        onPrintReport_Clicked(sender, e);
                        return;
                    }
                }
                // If not comming EMR & from Appointment, print the report
                //crvReportViewer.PrintReport();
                ConverttoPDF();
           }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ConverttoPDF()
        {

            ReportDocument crReportDocument = (ReportDocument)crvReportViewer.ReportSource;

            DiskFileDestinationOptions rptFileDestOption = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions rptFormatOption = new PdfRtfWordFormatOptions();
            string[] Nm = crvReportViewer.ReportSource.ToString().ToLower().Split('.');
            string reportFileName = "";

            if (Nm[1].ToString().Contains("rpt"))
            {
                reportFileName = gloSettings.FolderSettings.AppTempFolderPath + Nm[1].ToString() + ".PDF";
            }
            else
            {
                reportFileName = gloSettings.FolderSettings.AppTempFolderPath + Nm[2].ToString() + ".PDF";
            }
            
            rptFileDestOption.DiskFileName = reportFileName;
            crReportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, reportFileName);
            try
            {
                Print(reportFileName, Nm[2].ToString(), "Success", true);
            }
            catch
            {
                Print(reportFileName, Nm[1].ToString(), "Success", true);
            }
        }

        private void Print(string _PDFFileName, string rptName, string msgboxcaption, Boolean blnPrintDialog)
        {



            gloPrintDialog.gloPrintProgressController ogloPrintProgressController = null;

            try
            {
                using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog())
                {
                    oDialog.ConnectionString = _databaseconnectionstring;
                    oDialog.TopMost = true;
                    oDialog.ShowPrinterProfileDialog = true;


                    oDialog.ModuleName = "CrystalReports";
                    oDialog.RegistryModuleName = "CrystalReports";
                    if (!gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        System.Drawing.Printing.PrinterSettings _printsettings = new PrinterSettings();
                        oDialog.PrinterSettings = _printsettings;
                    }
                    if (blnPrintDialog == false)
                    {
                        oDialog.bUseDefaultPrinter = true;
                    }
                    if (oDialog != null)
                    {

                        IntPtr handle = GetActiveWindow();
                        Control NetControl = ControlFromHandle(handle);
                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                        {

                            try
                            {
                                //Bug #96771 added for selecting specific pages for printing 
                                FileStream fs = new FileStream(_PDFFileName, FileMode.Open, FileAccess.Read);
                                StreamReader r = new StreamReader(fs);
                                string pdfText = r.ReadToEnd();
                                Regex rx1 = new Regex(@"/Type\s*/Page[^s]");
                                MatchCollection matches = rx1.Matches(pdfText);
                                fs.Close();
                                fs.Dispose();
                                r.Close();
                                r.Dispose();
                                fs = null;
                                r = null;
                                pdfText = null;
                                if (matches.Count > 0)
                                {
                                    oDialog.AllowSomePages = true;
                                    oDialog.PrinterSettings.ToPage = matches.Count;
                                    oDialog.PrinterSettings.FromPage = 1;
                                }
                            }
                            catch
                            {
                            }
                        }
                        if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            if (Convert.ToBoolean(appSettings["DefaultPrinter"]))
                            {
                                oDialog.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                oDialog.CustomPrinterExtendedSettings.IsShowProgress = true;
                                blnPrintDialog = false;
                            }
                            if (!gloGlobal.gloTSPrint.isCopyPrint)
                            {
                                if (rptName.Contains("rpt_patientbydob") || rptName.Contains("rpt_insuranceplanwithcompanyname") || rptName.Contains("rpt_insuranceplanwithcompanyname") || rptName.Contains("rpt_newpatvsestpat") || rptName.Contains("rpt_patientreport"))
                                {
                                    oDialog.PrinterSettings.DefaultPageSettings.Landscape = true;
                                }
                            }


                            ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController(_PDFFileName, oDialog.PrinterSettings, oDialog.CustomPrinterExtendedSettings);
                            ogloPrintProgressController.showTSPrinterSelection = blnPrintDialog;
                            ogloPrintProgressController.ShowProgress(NetControl);



                        }//if

                    }
                    else
                    {
                        string _ErrorMessage = "Error in Showing Print Dialog";

                        if (_ErrorMessage.Trim() != "")
                        {
                            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;

                            _MessageString = "";
                        }


                        MessageBox.Show(_ErrorMessage, msgboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }


            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                string _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    //  gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

                MessageBox.Show(ex.Message, msgboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ex = null;
            }
            finally
            {

            }

        }
        private static Control ControlFromHandle(IntPtr hWND)
        {
            while (hWND != IntPtr.Zero)
            {
                Control control = Control.FromChildHandle(hWND);
                if (control != null)
                {
                    if (control is Form)
                    {
                        Control childControl = ((Form)control).ActiveControl;
                        if (childControl != null)
                        {
                            control = childControl;
                        }
                    }
                }
                if (control != null)
                    return control;

                hWND = GetParent(hWND);

                //  Control control2 = System.Windows.Forms.Control.FromChildHandle(hWND);
                // IntPtr hwnd = (IntPtr)this.Handle.ToPointer();
            }

            return null;
        }
        //For Generate Report 
        private void tsb_GenerateReport_Click(object sender, EventArgs e)
        {
            onGenerateReport_Clicked(sender, e);

        }

        //For Export Functionality
        private void tsb_btnExport(object sender, EventArgs e)
        {
            crvReportViewer.ExportReport();
        }

        //For Patient Combo Selection Changed
        private void cmbPatients_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (onPatients_SelectedIndexChanged != null)
            {
                onPatients_SelectedIndexChanged(sender, e);
            }
        }


        private void tsb_btnGenerateBatch_Click(object sender, EventArgs e)
        {
            if (onGenerateBatch_Clicked != null)
            {
                onGenerateBatch_Clicked(sender, e);
            }
        }


        //For Date Range

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

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (_dtProvider != null)
                {
                    dtpEndDate.Value = dtpStartDate.Value;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        
        private void gloReportViewer_Paint(object sender, PaintEventArgs e)
        {
            AdjustCriteriaPanelHight();
        }

        private void cmbAgingCritera_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _Agingby = 0;
            dtAgingDate = 0;
            _Agingby = cmbAgingCritera.SelectedIndex;
            switch (_Agingby)
            {
                //For Empty Selection
                case 0:
                 //   cmbPatients.Items.Clear();
                    cmbPatients.DataSource = null;
                    cmbPatients.Items.Clear();
                    break;

                //To Search For Balance > 0
                case 1:
                    dtAgingDate = 1;
                    break;

                //For Overdue > 30 Days
                case 2:
                    dtAgingDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(DateTime.Now.Date.Subtract(new TimeSpan(30, 0, 0, 0))));
                    break;

                //For Overdue > 60 Days
                case 3:
                    dtAgingDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(DateTime.Now.Date.Subtract(new TimeSpan(60, 0, 0, 0))));
                    break;

                //For Overdue > 90 Days
                case 4:
                    dtAgingDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(DateTime.Now.Date.Subtract(new TimeSpan(90, 0, 0, 0))));
                    break;

                //For Overdue > 120 Days
                case 5:
                    dtAgingDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(DateTime.Now.Date.Subtract(new TimeSpan(120, 0, 0, 0))));
                    break;

                //For Overdue > 180 Days
                case 6:
                    dtAgingDate = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(DateTime.Now.Date.Subtract(new TimeSpan(180, 0, 0, 0))));
                    break;

            }

            //Fill the Patients Only when its not case '0'
            if (dtAgingDate != 0)
            {
                FillPatientsWithOverdueBalance(dtAgingDate);
            }

        }

        #endregion

                                     
        #region " Methods "

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
            //DateTime dtFrom = new DateTime(dtpStartDate.Value.Year, dtpStartDate.Value.Month, 1);
            //DateTime dtTo = new DateTime(DateTime.Now.Year, dtpStartDate.Value.Month, 1);
            DateTime dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtTo = dtTo.AddMonths(1);
            dtTo = dtTo.AddDays(-(dtTo.Day));
            dtpStartDate.Value = Convert.ToDateTime(dtFrom.Date);
            dtpEndDate.Value = Convert.ToDateTime(dtTo.Date);

            if (cmb_datefilter.SelectedIndex == 0)
            {
                dtpStartDate.Enabled = true;
                dtpEndDate.Enabled = true;
            }
            else
            {
                dtpStartDate.Enabled = false;
                dtpEndDate.Enabled = false;
            }

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

        private void AdjustCriteriaPanelHight()
        {
            try
            {
                Int32 FlowPanelHight = 0;
                String sControlName = "";
                Int32 iCounter = 0;
                for (Int32 i = 0; i < fpnlCriteria.Controls.Count; i++)
                {
                    if (fpnlCriteria.Controls[i].Visible == true)
                    {
                        if (FlowPanelHight < (fpnlCriteria.Controls[i].Location.Y + fpnlCriteria.Controls[i].Height))
                        {
                            FlowPanelHight = fpnlCriteria.Controls[i].Location.Y + fpnlCriteria.Controls[i].Height + 22;
                            sControlName=fpnlCriteria.Controls[i].Name;
                            iCounter++;
                        }
                    }
                }
                
                if (FlowPanelHight == 80)
                {
                    if ((iCounter == 1) && (sControlName == "pnlDates"))
                        fpnlCriteria.Height = FlowPanelHight-10;
                    else
                        fpnlCriteria.Height = FlowPanelHight + 30;
                }
                else
                {
                    fpnlCriteria.Height = FlowPanelHight + 5;
                }
                fpnlCriteria.Refresh();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }


        #endregion
                
        
        #region "Fill Methods"

        // For Filling The Facilities Combo
        private void FillFacilities()
        {
            try
            {
                DataTable _dtLocations = new DataTable();
                gloFacility ogloFacilities = new gloFacility(_databaseconnectionstring);
                _dtLocations = ogloFacilities.GetFacilities();

                DataTable dtLocations;
                if (_dtLocations != null && _dtLocations.Rows.Count > 0)
                {
                    dtLocations = new DataTable();
                    dtLocations.Columns.Add("sFacilityCode");
                    dtLocations.Columns.Add("sFacilityName");

                    dtLocations.Clear();
                    dtLocations.Rows.Add(0, "");

                    for (int i = 0; i < _dtLocations.Rows.Count; i++)
                    {
                        dtLocations.Rows.Add(_dtLocations.Rows[i]["sFacilityCode"], _dtLocations.Rows[i]["sFacilityName"]);
                    }

                    cmbFacility.DataSource = dtLocations;
                    cmbFacility.DisplayMember = "sFacilityName";
                    cmbFacility.ValueMember = "sFacilityCode";
                }

                _dtLocations = null;
                ogloFacilities.Dispose();

               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
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


        private void FillMonths()
        {
            try
            {
                //this.drpMonth.SelectedIndexChanged -= new System.EventHandler(this.drpMonth_SelectedIndexChanged);
                for (int i = 0; i <= 12; i++)
                {
                    ListItem liMonths = new ListItem(Months[i], i.ToString());
                    drpMonth.Items.Add(liMonths);

                }
                drpMonth.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                //this.drpMonth.SelectedIndexChanged += new System.EventHandler(this.drpMonth_SelectedIndexChanged);
            }
           
            
        }
        private void FillReportType()
        {
            try
            {
                //this.drpMonth.SelectedIndexChanged -= new System.EventHandler(this.drpMonth_SelectedIndexChanged);
                for (int i = 0; i <= 3; i++)
                {
                    ListItem liReportType = new ListItem(ReportType[i], i.ToString());
                    cmbReportType.Items.Add(liReportType);

                }
                cmbReportType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
              
                //this.drpMonth.SelectedIndexChanged += new System.EventHandler(this.drpMonth_SelectedIndexChanged);
            }


        }

        public string GetPatientName(Int64 patientID)
        {

            DataTable dtPatient = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _strSQL = "";
            string _result = "";
            try
            {
                oDB.Connect(false);

                //get the provider details in the datatable -- dtProvider
                _strSQL = "select ISNULL( sFirstName,'') + SPACE(1) + ISNULL(sMiddleName,'') + SPACE(1) + ISNULL(sLastName,'') AS PatientName FROM Patient WHERE nPatientID = " + patientID;
                oDB.Retrive_Query(_strSQL, out dtPatient);
                _result =Convert.ToString(dtPatient.Rows[0]["PatientName"]);


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

                oDB.Disconnect();
                if (oDB != null)
                    oDB.Dispose();
            }

            return _result;

        }

        //For Filling the aging Criteria Combo Box
        private void FillAgingCriteria()
        {
            cmbAgingCritera.Items.Clear();
            cmbAgingCritera.Items.Add("");
            cmbAgingCritera.Items.Add("Patients with outstanding balance");
            cmbAgingCritera.Items.Add("Patients with balance > 30 days outstanding");
            cmbAgingCritera.Items.Add("Patients with balance > 60 days outstanding");
            cmbAgingCritera.Items.Add("Patients with balance > 90 days outstanding");
            cmbAgingCritera.Items.Add("Patients with balance > 120 days outstanding");
            cmbAgingCritera.Items.Add("Patients with balance > 180 days outstanding");

        }

        //To Fill the Patients Combo who is having "Over Paid Balance" 
        private void FillPatientsWithOverdueBalance(Int64 _dtAgingDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbparam = new gloDatabaseLayer.DBParameters();
            DataTable dt;
            try
            {
               // cmbPatients.Items.Clear();
                cmbPatients.DataSource = null;
                cmbPatients.Items.Clear();
                oDB.Connect(false);
                odbparam.Add("@nTransactionDate", _dtAgingDate, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("Rpt_Calculate_PatientsWithOverDueBal", odbparam, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        cmbPatients.DataSource = dt;
                        cmbPatients.ValueMember = dt.Columns["nPatientID"].ColumnName;
                        cmbPatients.DisplayMember = dt.Columns["PatientName"].ColumnName;

                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
        }


        private void FillLocation()
        {
            gloAppointmentBook.Books.Location oLocation = new gloAppointmentBook.Books.Location();
            DataTable _dtLocations = new DataTable();
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

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {

            }
        }


        private void FillActivePaymentTray()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Object _retVal = null;
         
            try
            {
                
                _sqlQuery = "SELECT nCloseDayTrayID,sCode, " +
                        " sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                        " FROM BL_CloseDayTray WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                        "AND sDescription <> '' AND ISNULL(bIsClosed,0) = 0 AND nClinicID = " + _ClinicID + "";
                
                DataTable dtCloseDayTray = new DataTable();
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtCloseDayTray);
                oDB.Disconnect();

              
                cmbActivePayTray.DataSource = dtCloseDayTray;
                cmbActivePayTray.ValueMember = "nCloseDayTrayID";
                cmbActivePayTray.DisplayMember = "sDescription";

                //To Select the Default tray
                if (dtCloseDayTray != null && dtCloseDayTray.Rows.Count > 0)
                {
                   _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray " +
                   " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                   " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true'  AND nClinicID = " + _ClinicID + "";
                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                    {
                        _defaultTrayId = Convert.ToInt64(_retVal);
                        cmbActivePayTray.SelectedValue = _defaultTrayId;
                    }
                    else
                    {
                        cmbActivePayTray.SelectedIndex = 0;
                    }
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }

            }
        }

        private void FillClosedPaymentTray()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Object _retVal = null;
        
            try
            {

                _sqlQuery = "SELECT nCloseDayTrayID,sCode, " +
                        " sDescription,ISNULL(bIsDefault,0) AS bIsDefault" +
                        " FROM BL_CloseDayTray WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                        "AND sDescription <> '' AND ISNULL(bIsClosed,0) = 1 AND nClinicID = " + _ClinicID + "";

                DataTable dtCloseDayTray = new DataTable();
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtCloseDayTray);
                oDB.Disconnect();


                cmbClosedPayTray.DataSource = dtCloseDayTray;
                cmbClosedPayTray.ValueMember = "nCloseDayTrayID";
                cmbClosedPayTray.DisplayMember = "sDescription";

                //To Select the Default tray
                if (dtCloseDayTray != null && dtCloseDayTray.Rows.Count > 0)
                {
                    _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray " +
                    " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                    " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 1 AND bIsDefault = 'true'  AND nClinicID = " + _ClinicID + "";
                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                    {
                        _defaultTrayId = Convert.ToInt64(_retVal);
                        cmbClosedPayTray.SelectedValue = _defaultTrayId;
                    }
                    else
                    {
                        cmbClosedPayTray.SelectedIndex = 0;
                    }
                }
              
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }

            }
        }



        #endregion


        #region "List Control Eventes"
        private void removeOListControl(bool bDispose = true)
        {
            if (oListControl != null)
            {
                if (this.Controls.Contains(oListControl))
                {
                    this.Controls.Remove(oListControl);
                }
                try
                {
                    oListControl.ItemSelectedClick -= new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                    oListControl.ItemClosedClick -= new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);
                }
                catch
                {
                }
                if (bDispose)
                {
                    oListControl.Dispose();
                    oListControl = null;
                }
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
            
  
                if(FlagPatientMultiselect)
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, true, this.Width);
                else
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Patient, false, this.Width);

               
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Patient";
                 
                _CurrentControlType = gloListControl.gloListControlType.Patient;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

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
           // cmbPatients.Items.Clear();
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

        private void btnBrowseDiagnosisCode_Click(object sender, EventArgs e)
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
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Diagnosis, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Diagnosis";

                _CurrentControlType = gloListControl.gloListControlType.Diagnosis;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbDiagnosisCode.DataSource != null)
                {
                    for (int i = 0; i < cmbDiagnosisCode.Items.Count; i++)
                    {
                        cmbDiagnosisCode.SelectedIndex = i;
                        cmbDiagnosisCode.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbDiagnosisCode.SelectedValue), cmbDiagnosisCode.Text, "");

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

        private void btnClearDiagnosisCode_Click(object sender, EventArgs e)
        {
           
            cmbDiagnosisCode.DataSource = null;
            cmbDiagnosisCode.Items.Clear();
            cmbDiagnosisCode.Refresh();
        }

        private void btnBrowseInsurance_Click(object sender, EventArgs e)
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
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Insurance, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Insurances";
                _CurrentControlType = gloListControl.gloListControlType.AllPatientInsurances;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);
                if (cmbInsurance.DataSource != null)
                {
                    for (int i = 0; i < cmbInsurance.Items.Count; i++)
                    {
                        cmbInsurance.SelectedIndex = i;
                        cmbInsurance.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbInsurance.SelectedValue), cmbInsurance.Text);
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

        private void btnClearInsurance_Click(object sender, EventArgs e)
        {
            if (cmbInsurance.Items.Count > 0)
            {
                ogloItems.Clear();
            }
           
            cmbInsurance.DataSource = null;
            cmbInsurance.Items.Clear();
            cmbInsurance.Refresh();
        }

        public void btnBrowseCPT_Click(object sender, EventArgs e)
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
                if (FlagCPTMultiselect)
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.CPT, true, this.Width);
                else
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.CPT, false, this.Width);

                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " CPT";
                _CurrentControlType = gloListControl.gloListControlType.CPT;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbCPT.DataSource != null)
                {
                    for (int i = 0; i < cmbCPT.Items.Count; i++)
                    {
                        cmbCPT.SelectedIndex = i;
                        cmbCPT.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbCPT.SelectedValue), cmbCPT.Text, "");
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

        private void btnClearCPT_Click(object sender, EventArgs e)
        {
           
            cmbCPT.DataSource = null;
            cmbCPT.Items.Clear();
            cmbCPT.Refresh();
        }

        private void btnBrowseAppointmentType_Click(object sender, EventArgs e)
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
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.AppointmentType, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Appointment Type";
                _CurrentControlType = gloListControl.gloListControlType.AppointmentType;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);


                if (cmbApp_AppointmentType.DataSource != null)
                {

                    for (int i = 0; i < cmbApp_AppointmentType.Items.Count; i++)
                    {
                        cmbApp_AppointmentType.SelectedIndex = i;
                        cmbApp_AppointmentType.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbApp_AppointmentType.SelectedValue), cmbApp_AppointmentType.Text);
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

        private void btnClearAppointmentType_Click(object sender, EventArgs e)
        {
           
            cmbApp_AppointmentType.DataSource = null;
            cmbApp_AppointmentType.Items.Clear();
            cmbApp_AppointmentType.Refresh();
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
                case gloListControl.gloListControlType.AllPatientInsurances:
                    {
                       
                        cmbInsurance.DataSource = null;
                        cmbInsurance.Items.Clear();
                        if (oListControl.SelectedItems.Count > 0)
                        {
                            DataTable oBindTable = new DataTable();

                            oBindTable.Columns.Add("ID");
                            oBindTable.Columns.Add("DispName");
                            ogloItems = new gloGeneralItem.gloItems();
                            gloGeneralItem.gloItem ogloItem = null;
                            for (_Counter = 0; _Counter <= oListControl.SelectedItems.Count - 1; _Counter++)
                            {
                                DataRow oRow;
                                oRow = oBindTable.NewRow();
                                oRow[0] = oListControl.SelectedItems[_Counter].ID;
                                oRow[1] = oListControl.SelectedItems[_Counter].Description;
                                oBindTable.Rows.Add(oRow);
                                ogloItem = new gloGeneralItem.gloItem();
                                ogloItem.ID = oListControl.SelectedItems[_Counter].ID;
                                ogloItem.Description = oListControl.SelectedItems[_Counter].Description;
                                ogloItems.Add(ogloItem);
                                ogloItem.Dispose();
                                ogloItem = null;
                            }

                            cmbInsurance.DataSource = oBindTable;
                            cmbInsurance.DisplayMember = "DispName";
                            cmbInsurance.ValueMember = "ID";
                            ogloItems.Clear();
                            ogloItems.Dispose();
                            ogloItems = null;
                        }


                    }
                    break;
                case gloListControl.gloListControlType.Diagnosis:
                    {
                        
                        cmbDiagnosisCode.DataSource = null;
                        cmbDiagnosisCode.Items.Clear();
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
                                oRow[1] = oListControl.SelectedItems[_Counter].Code;
                                oBindTable.Rows.Add(oRow);
                            }

                            cmbDiagnosisCode.DataSource = oBindTable;
                            cmbDiagnosisCode.DisplayMember = "DispName";
                            cmbDiagnosisCode.ValueMember = "ID";
                        }


                    }
                    break;
                case gloListControl.gloListControlType.CPT:
                    {
                       
                        cmbCPT.DataSource = null;
                        cmbCPT.Items.Clear();
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
                                oRow[1] = oListControl.SelectedItems[_Counter].Code;
                                oBindTable.Rows.Add(oRow);
                            }

                            cmbCPT.DataSource = oBindTable;
                            cmbCPT.DisplayMember = "DispName";
                            cmbCPT.ValueMember = "ID";
                        }


                    }
                    break;

                case gloListControl.gloListControlType.AppointmentType:
                    {
                       
                        cmbApp_AppointmentType.DataSource = null;
                        cmbApp_AppointmentType.Items.Clear();
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

                            cmbApp_AppointmentType.DataSource = oBindTable;
                            cmbApp_AppointmentType.DisplayMember = "DispName";
                            cmbApp_AppointmentType.ValueMember = "ID";
                        }

                    }
                    break;

                case gloListControl.gloListControlType.Facility:
                    {
                        
                        cmbMultiFacility.DataSource = null;
                        cmbMultiFacility.Items.Clear();
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

                            cmbMultiFacility.DataSource = oBindTable;
                            cmbMultiFacility.DisplayMember = "DispName";
                            cmbMultiFacility.ValueMember = "ID";
                        }

                    }
                    break;

                case gloListControl.gloListControlType.ChargeTray:
                    {
                       
                        cmbMultiChargesTray.DataSource = null;
                        cmbMultiChargesTray.Items.Clear();
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

                            cmbMultiChargesTray.DataSource = oBindTable;
                            cmbMultiChargesTray.DisplayMember = "DispName";
                            cmbMultiChargesTray.ValueMember = "ID";
                        }

                    }
                    break;



                case gloListControl.gloListControlType.PaymentTray:
                    {
                        
                        cmblMultiPayTray.DataSource = null;
                        cmblMultiPayTray.Items.Clear();
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

                            cmblMultiPayTray.DataSource = oBindTable;
                            cmblMultiPayTray.DisplayMember = "DispName";
                            cmblMultiPayTray.ValueMember = "ID";
                        }

                    }
                    break;

                case gloListControl.gloListControlType.Users:
                    {
                       
                        cmbUser.DataSource = null;
                        cmbUser.Items.Clear();
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

                            cmbUser.DataSource = oBindTable;
                            cmbUser.DisplayMember = "DispName";
                            cmbUser.ValueMember = "ID";
                        }

                    }
                    break;
                case gloListControl.gloListControlType.Resources:
                    {
                        cmbResouce.DataSource= null;
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

                            cmbResouce.DataSource = oBindTable;
                            cmbResouce.DisplayMember = "DispName";
                            cmbResouce.ValueMember = "ID";
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
            removeOListControl(false);
        }


        #endregion


        #region "Criteria Controls Events"

        private void rbSortEstablishedPatient_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSortEstablishedPatient.Checked == true)
                rbSortEstablishedPatient.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbSortEstablishedPatient.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void rbSortNewPatient_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSortNewPatient.Checked == true)
                rbSortNewPatient.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbSortNewPatient.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void rbCancelAppointments_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCancelAppointments.Checked == true)
                rbCancelAppointments.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbCancelAppointments.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void rbNoShowAppointments_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNoShowAppointments.Checked == true)
                rbNoShowAppointments.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbNoShowAppointments.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void rbDeletedAppointments_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDeletedAppointments.Checked == true)
                rbDeletedAppointments.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbDeletedAppointments.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void rbCharges_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCharges.Checked == true)
                rbCharges.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbCharges.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void rbAllowed_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAllowed.Checked == true)
                rbAllowed.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbAllowed.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void rbBoth_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBoth.Checked == true)
                rbBoth.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rbBoth.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)sender).BackgroundImage = global::gloReports.Properties.Resources.Img_LongYellow;
            ((System.Windows.Forms.Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)sender).BackgroundImage = global::gloReports.Properties.Resources.Img_LongButton;
            ((System.Windows.Forms.Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void rbActivePayTray_CheckedChanged(object sender, EventArgs e)
        {
            pnlActivePayTray.Visible = true;
            pnlClosedPayTray.Visible = false;

        }

        private void rbClosedPayTray_CheckedChanged(object sender, EventArgs e)
        {
            pnlActivePayTray.Visible = false;
            pnlClosedPayTray.Visible = true;

        }

        #endregion

        private void btnBrowseMultiFacility_Click(object sender, EventArgs e)
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
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Facility, true, this.Width);

                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Facility ";

                _CurrentControlType = gloListControl.gloListControlType.Facility;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbMultiFacility.DataSource != null)
                {
                    for (int i = 0; i < cmbPatients.Items.Count; i++)
                    {
                        cmbMultiFacility.SelectedIndex = i;
                        cmbMultiFacility.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbMultiFacility.SelectedValue), cmbMultiFacility.Text);
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

        private void btnClearMultiFacility_Click(object sender, EventArgs e)
        {
          //  cmbMultiFacility.Items.Clear();
            cmbMultiFacility.DataSource = null;
            cmbMultiFacility.Items.Clear();
            cmbMultiFacility.Refresh();
        }

        private void btnBrowseMultiChargesTray_Click(object sender, EventArgs e)
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
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.ChargeTray, true, this.Width);

                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Charges Tray ";

                _CurrentControlType = gloListControl.gloListControlType.ChargeTray;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbMultiChargesTray.DataSource != null)
                {
                    for (int i = 0; i < cmbPatients.Items.Count; i++)
                    {
                        cmbMultiChargesTray.SelectedIndex = i;
                        cmbMultiChargesTray.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbMultiChargesTray.SelectedValue), cmbMultiChargesTray.Text);
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

        private void btnClearMultiChargesTray_Click(object sender, EventArgs e)
        {
         //   cmbMultiChargesTray.Items.Clear();
            cmbMultiChargesTray.DataSource = null;
            cmbMultiChargesTray.Items.Clear();
            cmbMultiChargesTray.Refresh();
        }

        private void btnBrowseMultiPayTray_Click(object sender, EventArgs e)
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
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.PaymentTray, true, this.Width);

                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " Payment Tray ";

                _CurrentControlType = gloListControl.gloListControlType.PaymentTray;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmblMultiPayTray.DataSource != null)
                {
                    for (int i = 0; i < cmbPatients.Items.Count; i++)
                    {
                        cmblMultiPayTray.SelectedIndex = i;
                        cmblMultiPayTray.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmblMultiPayTray.SelectedValue), cmblMultiPayTray.Text);
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

        private void btnClearMultiPayTray_Click(object sender, EventArgs e)
        {
          //  cmblMultiPayTray.Items.Clear();
            cmblMultiPayTray.DataSource = null;
            cmblMultiPayTray.Items.Clear();
            cmblMultiPayTray.Refresh();
        }

        private void btnBrowseUser_Click(object sender, EventArgs e)
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
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Users, true, this.Width);


                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = " User ";

                _CurrentControlType = gloListControl.gloListControlType.Users;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbUser.DataSource != null)
                {
                    for (int i = 0; i < cmbPatients.Items.Count; i++)
                    {
                        cmbUser.SelectedIndex = i;
                        cmbUser.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbUser.SelectedValue), cmbUser.Text);
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

        private void btnClearUser_Click(object sender, EventArgs e)
        {
        //    cmbUser.Items.Clear();
            cmbUser.DataSource = null;
            cmbUser.Items.Clear();
            cmbUser.Refresh();
        }

        private void btnUP_Click(object sender, EventArgs e)
        {

            try
            {
                btnUP.Visible = false;
                btnDown.Visible = true;
                //fpnlCriteria.Visible = true;
                fpnlCriteria.Visible = false;
                fpnlCriteria.Refresh();
                btnDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
                btnDown.BackgroundImageLayout = ImageLayout.Center;
                lblbtnDown.Visible = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
            }

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {

                btnDown.Visible = false; 
                btnUP.Visible = true;
                //fpnlCriteria.Visible = false;
                fpnlCriteria.Visible = true;
                btnUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
                btnUP.BackgroundImageLayout = ImageLayout.Center;
                lblbtnDown.Visible = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
            }
            
        }

        private void btnTrayDown_Click(object sender, EventArgs e)
        {
            pnlSummaryContainer.Visible = false;
            btnTrayUp.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnTrayUp.BackgroundImageLayout = ImageLayout.Center;
            btnTrayDown.Visible = false;
            btnTrayUp.Visible = true;
        }

        private void btnTrayUp_Click(object sender, EventArgs e)
        {
            pnlSummaryContainer.Visible = true;
            btnTrayDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnTrayDown.BackgroundImageLayout = ImageLayout.Center;
            btnTrayUp.Visible = false;
            btnTrayDown.Visible = true;
        }

        private void btnUP_MouseHover(object sender, EventArgs e)
        {
            btnUP.BackgroundImage = global::gloReports.Properties.Resources.UPHover;
            btnUP.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown_MouseHover(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloReports.Properties.Resources.DownHover;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnTrayDown_MouseHover(object sender, EventArgs e)
        {
            btnTrayDown.BackgroundImage = global::gloReports.Properties.Resources.DownHover;
            btnTrayDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnTrayUp_MouseHover(object sender, EventArgs e)
        {
            btnTrayUp.BackgroundImage = global::gloReports.Properties.Resources.UPHover;
            btnTrayUp.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnTrayDown_MouseLeave(object sender, EventArgs e)
        {
            btnTrayDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnTrayDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown_MouseLeave(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnUP_MouseLeave(object sender, EventArgs e)
        {
            btnUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnUP.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnTrayUp_MouseLeave(object sender, EventArgs e)
        {
            btnTrayUp.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnTrayUp.BackgroundImageLayout = ImageLayout.Center;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false; 
            if (pnlSealCharges.Tag.ToString() == "Orange")
            {
                pnlSealCharges.BackgroundImage = global::gloReports.Properties.Resources.Img_LongButton;
                pnlSealCharges.Tag = "Yellow";
            }
            else
            {
                pnlSealCharges.BackgroundImage = global::gloReports.Properties.Resources.Img_LongOrange;
                pnlSealCharges.Tag = "Orange";
            }
            timer1.Enabled = true;
        }

        private void rdoLstFst_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoLstFst.Checked == true)
                rdoLstFst.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            else
                rdoLstFst.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void rdoFstLst_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoFstLst.Checked == true)
                rdoFstLst.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rdoFstLst.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void crvReportViewer_Load(object sender, EventArgs e)
        {

        }

        private void rdoWithName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoWithName.Checked == true)
                rdoWithName.Font = gloGlobal.clsgloFont.gFont_BOLD;// new Font("Tahoma", 9, FontStyle.Bold);
            else
                rdoWithName.Font = gloGlobal.clsgloFont.gFont;// new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void rdoWithoutName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoWithoutName.Checked == true)
                rdoWithoutName.Font = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
            else
                rdoWithoutName.Font = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        }

        private void Panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtpStartDate_ValueChanged_1(object sender, EventArgs e)
        {
            if (dtpEndDate.Value <= dtpStartDate.Value)
            {
                dtpEndDate.Value = dtpStartDate.Value;
            }

        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStartDate.Value >= dtpEndDate.Value)
            {
               dtpStartDate.Value =  dtpEndDate.Value;
            }
        }

        private void btnBrwsMultiResource_Click(object sender, EventArgs e)
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
                oListControl = new gloListControl.gloListControl(_databaseconnectionstring, gloListControl.gloListControlType.Resources, true, this.Width);
                oListControl.ClinicID = _ClinicID;
                oListControl.ControlHeader = "Resource";
                _CurrentControlType = gloListControl.gloListControlType.Resources;
                oListControl.ItemSelectedClick += new gloListControl.gloListControl.ItemSelected(oListControl_ItemSelectedClick);
                oListControl.ItemClosedClick += new gloListControl.gloListControl.ItemClosed(oListControl_ItemClosedClick);

                this.Controls.Add(oListControl);

                if (cmbResouce.DataSource!= null)
                {
                    for (int i = 0; i < cmbResouce.Items.Count; i++)
                    {
                        cmbResouce.SelectedIndex = i;
                        cmbResouce.Refresh();
                        oListControl.SelectedItems.Add(Convert.ToInt64(cmbResouce.SelectedValue), cmbResouce.Text);
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

        private void btnClearResource_Click(object sender, EventArgs e)
        {
           
            cmbResouce.DataSource= null;
            cmbResouce.Refresh();
        }

        private void pnlResource_Paint(object sender, PaintEventArgs e)
        {

        }
       
    }


}
