using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloBilling.Common;
using System.Collections;
using C1.Win.C1FlexGrid;
using gloAuditTrail;
using System.Data.SqlClient;
using gloSettings;
 
namespace gloBilling
{
    public partial class frmVoidEMRTreatment : Form
    {
        #region " Variable Declarations

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _DatabaseConnectionString = "";
        private string _emrdatabaseConnectionString = "";
        private string _EMRType = "";
        private string _messageBoxCaption = "";
        private Int64 _PatientID = 0;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private string _UserName = "";
        private DataView _dvExam = new DataView();
        #endregion " Variable Declarations

        #region " Constructor "

        public frmVoidEMRTreatment(string Databaseconnectionstring, string EMRDatabaseconnectionstring)
        {
            InitializeComponent();

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

            #endregion

            _DatabaseConnectionString = Databaseconnectionstring;
            _emrdatabaseConnectionString = EMRDatabaseconnectionstring;

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

        #endregion " Constructor "

        #region " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        public Int64 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public ExternalChargesType _nEMRTreatmentType
        { 
            get; set; 
        }

        #endregion " Property Procedures "

        #region Column Contants for C1.Flex.Grid

        const int COL_EXAM_SELECT = 0;
        const int COL_EXAM_PATIENTID = 1;
        const int COL_EXAM_EXAMID = 2;
        const int COL_EXAM_VISITID = 3;
        const int COL_EXAM_DOS = 4;
        const int COL_EXAM_NAME = 5;
        const int COL_EXAM_EMRPatientCode = 6;
        const int COL_EXAM_EMRPatientFN = 7;
        const int COL_EXAM_EMRPatientMN = 8;
        const int COL_EXAM_EMRPatientLN = 9;
        //const int COL_EXAM_EMRPatientSSN = 10;
        const int COL_EXAM_EMRPatientDOB = 10;
        const int COL_EXAM_PROVIDERID = 11;
        const int COL_EXAM_PROVIDERNAME = 12;
        const int COL_EXAM_PROVIDERFNAME = 13;
        const int COL_EXAM_PROVIDERMNAME = 14;
        const int COL_EXAM_PROVIDERLNAME = 15;

        const int COL_EXAM_COUNT = 16;

        #endregion

        #region " Private Functions "

        private void BindPatientExam(Int64 PatientID)
        {

            //gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _emrdatabaseConnectionString);
            //..Code Commented on 20091102 by Mukesh
            //..Code changed to load emr treatment for EMR5.0
            gloBilling ogloBilling;

            if (_EMRType == "gloEMR50")
            {
                ogloBilling = new gloBilling(_DatabaseConnectionString, _DatabaseConnectionString);
                _emrdatabaseConnectionString = _DatabaseConnectionString;
            }
            else if (_EMRType == "gloEMR40" || _EMRType == "gloEMR40SP2")
            {
                ogloBilling = new gloBilling(_DatabaseConnectionString, _emrdatabaseConnectionString);
            }
            else
            {
                ogloBilling = new gloBilling(_DatabaseConnectionString, _emrdatabaseConnectionString);
            }

            DataTable dtEMRExam = null;
            Int64 _emrPatientId = 0;

            try
            {
                c1PatientEMRExams.AutoResize = false;
                if (c1PatientEMRExams != null)
                {
                    c1PatientEMRExams.ScrollBars = ScrollBars.None;
                  //  c1PatientEMRExams.Clear();
                    c1PatientEMRExams.DataSource = null;
                }

                if (PatientID > 0)
                {
                    //_emrPatientId = ogloBilling.GetEMRPatientID(PatientID);

                    //..Code Commented on 20091102 by Mukesh
                    //..Code changed to load emr treatment for EMR5.0
                    if (_EMRType == "gloEMR50")   // if Database is Common for both gloPM and gloEMR
                    {
                        _emrPatientId = PatientID;
                    }
                    else if (_EMRType == "gloEMR40" || _EMRType == "gloEMR40SP2") // if Database is not Common for both gloPM and gloEMR
                    {
                        _emrPatientId = ogloBilling.GetEMRPatientID(PatientID);
                    }
                    else
                    {
                        _emrPatientId = ogloBilling.GetEMRPatientID(PatientID);
                    }

                    if (_emrPatientId >= 0)
                    {
                        //gloBilling objgloBilling = new gloBilling(_DatabaseConnectionString, _emrdatabaseConnectionString);
                        //dtEMRExam = objgloBilling.GetEMRExams();

                        //dtEMRExam = gloCharges.GetEMRExams(gloSettings.ExternalChargesType.gloEMRTreatment);
                        
                        dtEMRExam = gloCharges.GetEMRExams(gloCharges.GetEMRTreatmentSourceSetting());
                        

                        if (dtEMRExam != null)
                        {
                            if (dtEMRExam.Rows.Count <= 0)
                            {
                                if (_dvExam != null && _dvExam.Table != null) { _dvExam.AddNew(); }
                                c1PatientEMRExams.Rows.Count = 1;
                                c1PatientEMRExams.Rows.Fixed = 1;
                            }

                            c1PatientEMRExams.Redraw = false;

                            _dvExam = dtEMRExam.DefaultView;
                            c1PatientEMRExams.DataSource = _dvExam;
                            //lblSearch.Text = c1PatientEMRExams.Cols["ExamName"].Name + " : ";
                            //lblSearch.Tag = c1PatientEMRExams.Cols["ExamName"].Index;

                            DesignGrid();

                            c1PatientEMRExams.Redraw = true;
                        }
                        else
                        {
                            //lblSearch.Text = "ExamName : ";
                            //lblSearch.Tag = null;
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (c1PatientEMRExams != null && c1PatientEMRExams.ScrollBars == ScrollBars.None) { c1PatientEMRExams.ScrollBars = ScrollBars.Both; }
                if (ogloBilling != null) { ogloBilling.Dispose(); }

            }

        }

        private void DesignGrid()
        {
            #region " Show Hide Columns "

            //EMRPatientId,nExamID,nVisitID,DOS,ExamName,EMRPatientCode,EMRPatientFN,EMRPatientMN,
            //EMRPatientLN,EMRPatientSSN,EMRPatientDOB,nProviderID,ProviderName,ProviderFName,ProviderMName,
            //ProviderLName
            c1PatientEMRExams.Cols["bSelect"].Visible = true;
            c1PatientEMRExams.Cols["EMRPatientId"].Visible = false;
            c1PatientEMRExams.Cols["nExamID"].Visible = false;
            c1PatientEMRExams.Cols["nVisitID"].Visible = false;
            c1PatientEMRExams.Cols["DOS"].Visible = true;
            c1PatientEMRExams.Cols["FinishDate"].Visible = true;
            c1PatientEMRExams.Cols["ExamName"].Visible = true;
            c1PatientEMRExams.Cols["TemplateName"].Visible = true;
            c1PatientEMRExams.Cols["Code"].Visible = true;
            c1PatientEMRExams.Cols["FirstName"].Visible = true;
            c1PatientEMRExams.Cols["MN"].Visible = true;
            c1PatientEMRExams.Cols["LastName"].Visible = true;
            //c1PatientEMRExams.Cols["EMRPatientSSN"].Visible = false;
            c1PatientEMRExams.Cols["DOB"].Visible = true;
            c1PatientEMRExams.Cols["nProviderID"].Visible = false;
            c1PatientEMRExams.Cols["ProviderName"].Visible = true;
            c1PatientEMRExams.Cols["ProviderFName"].Visible = false;
            c1PatientEMRExams.Cols["ProviderMName"].Visible = false;
            c1PatientEMRExams.Cols["ProviderLName"].Visible = false;
           // c1PatientEMRExams.Cols["SearchDOS"].Visible = false;
            c1PatientEMRExams.Cols["nTreatmentType"].Visible = false;
            c1PatientEMRExams.Cols["Charge Source"].Visible = false;

            if (c1PatientEMRExams.Cols["dtDOS"] != null)
            {
                c1PatientEMRExams.Cols["dtDOS"].Visible = false;
            }
            //Bug #96890: gloPM: Charges: EMR Treatment: No charge: Displaying improper header text for ndos
            if (c1PatientEMRExams.Cols["ndos"] != null)
            {
                c1PatientEMRExams.Cols["ndos"].Visible = false;
            }
            c1PatientEMRExams.Cols["AccountNote"].Visible = true;
            #endregion

            //c1PatientEMRExams.Cols[COL_EXAM_DOS].DataType = typeof(System.DateTime);
            //c1PatientEMRExams.Cols[COL_EXAM_DOS].AllowEditing = false;
            #region " Set Columns Width "

            c1PatientEMRExams.SetData(0, COL_EXAM_SELECT, "Select");

            int nWidth = 0;
            nWidth = pnlMain.Width;
            c1PatientEMRExams.Cols["bSelect"].Width = 50; ;
            c1PatientEMRExams.Cols["EMRPatientId"].Width = Convert.ToInt32(nWidth * 0.00); ;
            c1PatientEMRExams.Cols["nExamID"].Width = Convert.ToInt32(nWidth * 0.00); ;
            c1PatientEMRExams.Cols["nVisitID"].Width = Convert.ToInt32(nWidth * 0.00); ;
            c1PatientEMRExams.Cols["DOS"].Width = Convert.ToInt32(nWidth * 0.07); ;
            c1PatientEMRExams.Cols["FinishDate"].Width = Convert.ToInt32(nWidth * 0.07); ;
            c1PatientEMRExams.Cols["ExamName"].Width = Convert.ToInt32(nWidth * 0.25); ;
            c1PatientEMRExams.Cols["TemplateName"].Width = Convert.ToInt32(nWidth * 0.18);
            c1PatientEMRExams.Cols["Code"].Width = Convert.ToInt32(nWidth * 0.05);
            c1PatientEMRExams.Cols["FirstName"].Width = Convert.ToInt32(nWidth * 0.1); ;
            c1PatientEMRExams.Cols["MN"].Width = Convert.ToInt32(nWidth * 0.03); ;
            c1PatientEMRExams.Cols["LastName"].Width = Convert.ToInt32(nWidth * 0.1); ;
            //c1PatientEMRExams.Cols["EMRPatientSSN"].Width = Convert.ToInt32(nWidth * 0.00); ;
            c1PatientEMRExams.Cols["DOB"].Width = Convert.ToInt32(nWidth * 0.07); ;
            c1PatientEMRExams.Cols["nProviderID"].Width = Convert.ToInt32(nWidth * 0.00); ;
            c1PatientEMRExams.Cols["ProviderName"].Width = Convert.ToInt32(nWidth * 0.1); ;
            c1PatientEMRExams.Cols["AccountNote"].Width = Convert.ToInt32(nWidth * 0.2); ;
            c1PatientEMRExams.Cols["ProviderFName"].Width = Convert.ToInt32(nWidth * 0.00); ;
            c1PatientEMRExams.Cols["ProviderMName"].Width = Convert.ToInt32(nWidth * 0.00); ;
            c1PatientEMRExams.Cols["ProviderLName"].Width = Convert.ToInt32(nWidth * 0.00); ;

            #endregion " Set Columns Width "

            //c1PatientEMRExams.Cols[COL_EXAM_DOS].DataType = typeof(System.DateTime);
            #region " Allow Edit Columns "
            c1PatientEMRExams.AllowEditing = true;
            c1PatientEMRExams.Cols["bSelect"].AllowEditing = true;
            c1PatientEMRExams.Cols["EMRPatientId"].AllowEditing = false;
            c1PatientEMRExams.Cols["nExamID"].AllowEditing = false;
            c1PatientEMRExams.Cols["nVisitID"].AllowEditing = false;
            c1PatientEMRExams.Cols["DOS"].AllowEditing = false;
            c1PatientEMRExams.Cols["FinishDate"].AllowEditing = false;
            c1PatientEMRExams.Cols["ExamName"].AllowEditing = false;
            c1PatientEMRExams.Cols["Code"].AllowEditing = false;
            c1PatientEMRExams.Cols["FirstName"].AllowEditing = false;
            c1PatientEMRExams.Cols["MN"].AllowEditing = false;
            c1PatientEMRExams.Cols["LastName"].AllowEditing = false;
            //c1PatientEMRExams.Cols["EMRPatientSSN"].AllowEditing = false;
            c1PatientEMRExams.Cols["DOB"].AllowEditing = false;
            c1PatientEMRExams.Cols["nProviderID"].AllowEditing = false;
            c1PatientEMRExams.Cols["ProviderName"].AllowEditing = false;
            c1PatientEMRExams.Cols["ProviderFName"].AllowEditing = false;
            c1PatientEMRExams.Cols["ProviderMName"].AllowEditing = false;
            c1PatientEMRExams.Cols["ProviderLName"].AllowEditing = false;
            c1PatientEMRExams.Cols["AccountNote"].AllowEditing = false;
            #endregion

            c1PatientEMRExams.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            c1PatientEMRExams.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            c1PatientEMRExams.AutoResize = false;

            c1PatientEMRExams.Cols["DOS"].DataType = typeof(System.DateTime);
            c1PatientEMRExams.Cols["DOS"].Format = "MM/dd/yyyy";

            c1PatientEMRExams.Cols["DOB"].DataType = typeof(System.DateTime);
            c1PatientEMRExams.Cols["DOB"].Format = "MM/dd/yyyy";

            c1PatientEMRExams.Cols["FinishDate"].DataType = typeof(System.DateTime);
            c1PatientEMRExams.Cols["FinishDate"].Format = "MM/dd/yyyy";

            c1PatientEMRExams.Cols["SearchDOB"].Visible = false;
            c1PatientEMRExams.Cols["SearchFinishDate"].Visible = false;

        }

        private void c1PatientEMRExams_MouseMove(object sender, MouseEventArgs e)
        {
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1FlexGrid)sender, e.Location, false);
        }

        public DataTable GetEMRExams_Old(Int64 EMRPatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_emrdatabaseConnectionString);
            gloDatabaseLayer.DBLayer oDBPM = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            DataTable dtExams = null;
            //DataTable dtExamsIds = null;
            string _sqlQuery = "";
           // string _strEMRIDs = "";
            try
            {
                if (oDB.CheckConnection() == true)
                {
                    //Get the list of ExamID's used in gloPM to apply filter


                    String _PMDB = string.Empty;

                    if (appSettings["DatabaseName"] != null)
                    {
                        if (appSettings["DatabaseName"] != "")
                        { _PMDB = Convert.ToString(appSettings["DatabaseName"]); }
                        else { _PMDB = ""; }
                    }
                    else
                    { _PMDB = ""; }

                    if (EMRPatientId > 0)
                    {
                        _sqlQuery = " SELECT CONVERT(Bit,0) as bSelect, ISNULL(PatientExams.nPatientID, 0) AS EMRPatientId, ISNULL(PatientExams.nExamID, 0) AS nExamID,  " +
                                          "  ISNULL(PatientExams.nVisitID, 0) AS nVisitID, CONVERT(VARCHAR, PatientExams.dtDOS, 101) AS DOS,     " +
                                          "   ISNULL(PatientExams.sExamName, '') AS ExamName,    " +
                                           "  ISNULL(Patient.sPatientCode,'') AS Code, ISNULL(Patient.sFirstName,'') AS FirstName,     " +
                                           "  ISNULL(Patient.sMiddleName,'') AS MN,ISNULL(Patient.sLastName,'') AS LastName,     " +
                                           "  ISNULL(Patient.nSSN,'') AS EMRPatientSSN,CONVERT(VARCHAR,Patient.dtDOB,101) AS DOB,    " +
                                           "  ISNULL(PatientExams.nProviderID,0) AS nProviderID,     " +
                                           "  ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) +    " +
                                           "  ISNULL(Provider_MST.sLastName, '') AS ProviderName, ISNULL(Provider_MST.sFirstName, '') AS ProviderFName,     " +
                                           "  ISNULL(Provider_MST.sMiddleName, '') AS ProviderMName, ISNULL(Provider_MST.sLastName, '') AS ProviderLName    " +
                                           "  FROM  (PatientExams INNER JOIN    " +
                                           "  Patient ON PatientExams.nPatientID = Patient.nPatientID LEFT OUTER JOIN    " +
                                           "  Provider_MST ON PatientExams.nProviderID = Provider_MST.nProviderID )   " +
                                           "  Left outer JOIN [" + _PMDB + "].[dbo].[BL_Transaction_EMR_DTL] PMTable ON  " +
                                           "  PatientExams.nExamID = PMTable.nEMRExamID  " +
                                           "  WHERE  (PatientExams.bIsOpen = 'false')    AND (PatientExams.bIsFinished = 'true') AND PMTable.nEMRExamID IS NULL  " +
                                           "  ORDER BY PatientExams.dtDOS desc";
                    }
                    else
                    {
                        _sqlQuery = " SELECT CONVERT(Bit,0) as bSelect, ISNULL(PatientExams.nPatientID, 0) AS EMRPatientId, ISNULL(PatientExams.nExamID, 0) AS nExamID,  " +
                                         "  ISNULL(PatientExams.nVisitID, 0) AS nVisitID, CONVERT(VARCHAR, PatientExams.dtDOS, 101) AS DOS,     " +
                                         "   ISNULL(PatientExams.sExamName, '') AS ExamName,    " +
                                          "  ISNULL(Patient.sPatientCode,'') AS Code, ISNULL(Patient.sFirstName,'') AS FirstName,     " +
                                          "  ISNULL(Patient.sMiddleName,'') AS MN,ISNULL(Patient.sLastName,'') AS LastName,     " +
                                          "  ISNULL(Patient.nSSN,'') AS EMRPatientSSN,CONVERT(VARCHAR,Patient.dtDOB,101) AS DOB,    " +
                                          "  ISNULL(PatientExams.nProviderID,0) AS nProviderID,     " +
                                          "  ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) +    " +
                                          "  ISNULL(Provider_MST.sLastName, '') AS ProviderName, ISNULL(Provider_MST.sFirstName, '') AS ProviderFName,     " +
                                          "  ISNULL(Provider_MST.sMiddleName, '') AS ProviderMName, ISNULL(Provider_MST.sLastName, '') AS ProviderLName    " +
                                          "  FROM  (PatientExams INNER JOIN    " +
                                          "  Patient ON PatientExams.nPatientID = Patient.nPatientID LEFT OUTER JOIN    " +
                                          "  Provider_MST ON PatientExams.nProviderID = Provider_MST.nProviderID )   " +
                                          "  Left outer JOIN [" + _PMDB + "].[dbo].[BL_Transaction_EMR_DTL] PMTable ON  " +
                                          "  PatientExams.nExamID = PMTable.nEMRExamID  " +
                                          "  WHERE  (PatientExams.bIsOpen = 'false')    AND (PatientExams.bIsFinished = 'true') AND PMTable.nEMRExamID IS NULL  " +
                                          "  ORDER BY PatientExams.dtDOS desc";
                        //EMRPatientId,nExamID,nVisitID,DOS,ExamName,EMRPatientCode,EMRPatientFN,EMRPatientMN,
                        //EMRPatientLN,EMRPatientSSN,EMRPatientDOB,nProviderID,ProviderName,ProviderFName,ProviderMName,
                        //ProviderLName

                    }

                    oDB.Retrive_Query(_sqlQuery, out dtExams);
                    //dtExams.ad
                    //DataView dv = dtExams.DefaultView;
                    //dv.RowFilter = "";

                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); }
            }
            return dtExams;
        }

        public DataTable GetEMRExams(Int64 EMRPatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_emrdatabaseConnectionString);
            gloDatabaseLayer.DBLayer oDBPM = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
            DataTable dtExams = null;
            DataTable dtPMExamsIds = null;
            //DataView _dvEmrExam = null; 
            string _sqlQuery = "";
            //string _strEMRIDs = "";
            try
            {
                if (oDB.CheckConnection() == true)
                {
                    //Get the list of ExamID's used in gloPM to apply filter


                    String _PMDB = string.Empty;

                    if (appSettings["DatabaseName"] != null)
                    {
                        if (appSettings["DatabaseName"] != "")
                        { _PMDB = Convert.ToString(appSettings["DatabaseName"]); }
                        else { _PMDB = ""; }
                    }
                    else
                    { _PMDB = ""; }


                    _sqlQuery = " SELECT  CONVERT(Bit,0) as bSelect,ISNULL(PatientExams.nPatientID, 0) AS EMRPatientId, ISNULL(PatientExams.nExamID, 0) AS nExamID, " +
                    " ISNULL(PatientExams.nVisitID, 0) AS nVisitID, CONVERT(VARCHAR, PatientExams.dtDOS, 101) AS DOS,      " +
                    " ISNULL(PatientExams.sExamName, '') AS ExamName, " +
                    " ISNULL(Patient.sPatientCode,'') AS Code, ISNULL(Patient.sFirstName,'') AS FirstName,  " +
                    " ISNULL(Patient.sMiddleName,'') AS MN,ISNULL(Patient.sLastName,'') AS LastName, " +
                    " ISNULL(Patient.nSSN,'') AS EMRPatientSSN,CONVERT(VARCHAR,Patient.dtDOB,101) AS DOB, " +
                    " ISNULL(PatientExams.nProviderID,0) AS nProviderID, " +
                    " ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) + " +
                    " ISNULL(Provider_MST.sLastName, '') AS ProviderName, ISNULL(Provider_MST.sFirstName, '') AS ProviderFName,      " +
                    " ISNULL(Provider_MST.sMiddleName, '') AS ProviderMName, ISNULL(Provider_MST.sLastName, '') AS ProviderLName    " +
                    " FROM  (PatientExams INNER JOIN     " +
                    " Patient ON PatientExams.nPatientID = Patient.nPatientID LEFT OUTER JOIN     " +
                    " Provider_MST ON PatientExams.nProviderID = Provider_MST.nProviderID )  " +
                    " WHERE   " +
                    " (PatientExams.bIsOpen = 'false')     " +
                    " AND (PatientExams.bIsFinished = 'true') " +
                    " ORDER BY PatientExams.dtDOS desc ";

                    oDB.Retrive_Query(_sqlQuery, out dtExams);

                    _sqlQuery = " SELECT DISTINCT nEMRExamID FROM BL_Transaction_EMR_DTL ";
                    oDBPM.Connect(false);
                    oDBPM.Retrive_Query(_sqlQuery, out dtPMExamsIds);
                    oDBPM.Disconnect();

                    if (dtPMExamsIds != null && dtPMExamsIds.Rows.Count > 0)
                    {
                        DataRow[] dr = null;
                        for (int i = 0; i < dtPMExamsIds.Rows.Count; i++)
                        {
                            dr = dtExams.Select(" nExamID = " + dtPMExamsIds.Rows[i]["nEMRExamID"] + "");
                            if (dr != null && dr.Length > 0)
                            {
                                dtExams.Rows.Remove(dr[0]);
                                dtExams.AcceptChanges();
                            }
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
            return dtExams;
        }

        private String GetSetting(String SettingName)
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_DatabaseConnectionString);
            object value = new object();
            string _Result = "";
            try
            {

                ogloSettings.GetSetting(SettingName, out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    _Result = Convert.ToString(value);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _Result = "";
            }
            finally
            {
                ogloSettings.Dispose();
                ogloSettings = null;
                value = null;
            }
            return _Result;
        }

        private bool VoidEMRExams()
        {
            gloBilling ogloBilling = new gloBilling(_DatabaseConnectionString, _emrdatabaseConnectionString);
            object value = new object();
            bool _Result = false;
            ExternalChargesType _TreatmentType = 0;
            string _auditNoChargeMessage = "";
            try
            {
                for (int i = 1; i < c1PatientEMRExams.Rows.Count; i++)
                {
                    if (c1PatientEMRExams.GetCellCheck(i, COL_EXAM_SELECT) == CheckEnum.Checked)
                    {
                        if (c1PatientEMRExams.GetData(i, "nTreatmentType") != null)
                        {
                            _TreatmentType = (ExternalChargesType)c1PatientEMRExams.GetData(i, "nTreatmentType");
                        }

                        if (_TreatmentType == ExternalChargesType.gloEMRTreatment)
                        {
                            ogloBilling.AddEMRTransaction(0, Convert.ToInt64(c1PatientEMRExams.GetData(i, COL_EXAM_EXAMID)), Convert.ToInt64(Convert.ToInt64(c1PatientEMRExams.GetData(i, COL_EXAM_VISITID))), _ClinicID);

                            _auditNoChargeMessage = "Exam: " + Convert.ToString(c1PatientEMRExams.GetData(i, "ExamName")) + "[ExamId: " + Convert.ToString(c1PatientEMRExams.GetData(i, COL_EXAM_EXAMID)) + "] for Patient Code: " + Convert.ToString(c1PatientEMRExams.GetData(i, "Code")) + " with VisitId: " + Convert.ToString(c1PatientEMRExams.GetData(i, COL_EXAM_VISITID)) + " is marked NoCharge by User: " + gloGlobal.gloPMGlobal.UserName + " [UserId: " + gloGlobal.gloPMGlobal.UserID + "].";
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.NoCharge,_auditNoChargeMessage, 0,0,0,ActivityOutCome.Success,SoftwareComponent.gloPM,true);
                        }
                        else if (_TreatmentType == ExternalChargesType.HL7InboundCharges)
                        {
                            gloCharges.VoidExternalCharges(0, Convert.ToInt64(c1PatientEMRExams.GetData(i, COL_EXAM_EXAMID)), Convert.ToInt64(Convert.ToInt64(c1PatientEMRExams.GetData(i, COL_EXAM_VISITID))), _ClinicID);
                            _auditNoChargeMessage = "External Charges: " + Convert.ToString(c1PatientEMRExams.GetData(i, COL_EXAM_EXAMID)) + " with Visit: " + Convert.ToString(c1PatientEMRExams.GetData(i, COL_EXAM_VISITID)) + " is marked NoCharge by User: " + gloGlobal.gloPMGlobal.UserName + "-" + gloGlobal.gloPMGlobal.UserID + ".";
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupCharges, ActivityType.NoCharge, _auditNoChargeMessage, 0, 0, 0, ActivityOutCome.Success, SoftwareComponent.gloPM, true);
                        }
                        
                    }
                }
                _Result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
            return _Result;
        }

        private bool ValidateEMR()
        {
            bool _Result = false;
            try
            {
                for (int i = 1; i < c1PatientEMRExams.Rows.Count; i++)
                {
                    if (c1PatientEMRExams.GetCellCheck(i, COL_EXAM_SELECT) == CheckEnum.Checked)
                    {
                        _Result = true;
                        break;
                    }
                }

                if (_Result == false && c1PatientEMRExams.Rows.Count > 1)
                {
                    MessageBox.Show("Please select EMR Treatment. ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Result = false;
                }
                else if (_Result == true && c1PatientEMRExams.Rows.Count > 1)
                {
                    if (MessageBox.Show("Do you want to continue?  ", _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    { _Result = true; }
                    else
                    { _Result = false; }
                }
            }
            catch (Exception )
            {
                _Result = false;   
            }            
            return _Result;
        }

        #endregion " Private Functions "

        private void frmVoidEMRTreatment_Load(object sender, EventArgs e)
        {
            _EMRType = GetSetting("MigrateToEMRType");
            this.Cursor = Cursors.WaitCursor;
            BindPatientExam(_PatientID);
            this.Cursor = Cursors.Default;

            lblSearch.Text = "Exam Name : ";
            lblSearch.Tag = "Exam";
            chkGeneralSearch.Visible = true;
        }

        #region " Tool Strip Item Click Event "

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            if (ValidateEMR())
            {
                if (VoidEMRExams() == true)
                {
                    BindPatientExam(_PatientID);
                }
            }
        }

        private void tsb_SaveClose_Click(object sender, EventArgs e)
        {
            if (ValidateEMR())
            {
                VoidEMRExams();
                this.Close();
            }
        }

        private void tsb_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion " Tool Strip Item Click Event "

        #region " Search "
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ////detach the event
            //cbSelectAll.CheckedChanged -= new EventHandler(cbSelectAll_CheckedChanged);

            //string strSearch = "";
            //try
            //{
            //    if (c1PatientEMRExams.DataSource != null)
            //    {
            //        DataView dv = (DataView)c1PatientEMRExams.DataSource;
                    
            //        // clear all the previous selection
            //        foreach (DataRow row in dv.Table.Rows) { row[COL_EXAM_SELECT] = 0; }

            //        strSearch = txtSearch.Text.Trim();
            //        strSearch = strSearch.Replace("'", "").Replace(",", "").Replace("%", "").Replace("*", "");

            //        if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
            //        {
            //            dv.RowFilter = dv.Table.Columns["ExamName"].ColumnName + " Like '%" + strSearch + "%' OR " +
            //            dv.Table.Columns["DOS"].ColumnName + " Like '%" + strSearch + "%' OR " +
            //            dv.Table.Columns["Code"].ColumnName + " Like '%" + strSearch + "%' OR " +
            //            dv.Table.Columns["FirstName"].ColumnName + " Like '%" + strSearch + "%' OR " +
            //            dv.Table.Columns["MN"].ColumnName + " Like '%" + strSearch + "%' OR " +
            //            dv.Table.Columns["LastName"].ColumnName + " Like '%" + strSearch + "%' OR " +
            //            dv.Table.Columns["ProviderName"].ColumnName + " Like '%" + strSearch + "%'";
            //        }
            //        else
            //        {
            //            strSearch = strSearch.Replace("*", "");
            //            dv.RowFilter = dv.Table.Columns["ExamName"].ColumnName + " Like '" + strSearch + "%' OR " +
            //            dv.Table.Columns["DOS"].ColumnName + " Like '" + strSearch + "%' OR " +
            //            dv.Table.Columns["Code"].ColumnName + " Like '" + strSearch + "%' OR " +
            //            dv.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
            //            dv.Table.Columns["MN"].ColumnName + " Like '" + strSearch + "%' OR " +
            //            dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
            //            dv.Table.Columns["ProviderName"].ColumnName + " Like '" + strSearch + "%'";
            //        }
            //        c1PatientEMRExams.DataSource = dv;

            //        #region " After applying filter, as per cbSelectAll selection set the value "

            //        dv = (DataView)c1PatientEMRExams.DataSource;
            //        foreach (DataRow row in dv.Table.Rows)
            //        {
            //            if (cbSelectAll.Checked)
            //            { row[COL_EXAM_SELECT] = 1; }
            //            else
            //            { row[COL_EXAM_SELECT] = 0; }
            //        }

            //        #endregion " After applying filter, as per cbSelectAll selection set the value "
            //    }
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            //}

            ////attach the event again
            //cbSelectAll.CheckedChanged += new EventHandler(cbSelectAll_CheckedChanged);


            if (cbSelectAll.Checked)
            {
                if (c1PatientEMRExams.DataSource != null)
                {
                    foreach (DataRow row in ((DataView)c1PatientEMRExams.DataSource).Table.Rows)
                    {
                        row[COL_EXAM_SELECT] = 0;
                    }
                    cbSelectAll.Checked = false;
                }
            }

            c1PatientEMRExams.Redraw = false;
            c1PatientEMRExams.FinishEditing();

            string strSearch = "";
            string sFilter = "";
            try
            {
                #region " Search Old "

                if (chkGeneralSearch.Checked == false)
                {
                    if (lblSearch.Tag != null)
                    {
                        DataView dv = (DataView)c1PatientEMRExams.DataSource;
                        if (dv != null)
                        {
                            strSearch = txtSearch.Text.Trim();
                            int colIndex = c1PatientEMRExams.Cols["ExamName"].Index; //COL_EXAM_NAME; //Convert.ToInt32(lblSearch.Tag);
                            strSearch = strSearch.Replace("'", "");

                            if (strSearch.StartsWith("%") == true | strSearch.StartsWith("*") == true)
                            {
                                dv.RowFilter = dv.Table.Columns[colIndex].ColumnName + " Like '%" + strSearch.Replace("%", "").Replace("*", "") + "%'";
                            }
                            else
                            {
                                dv.RowFilter = dv.Table.Columns[colIndex].ColumnName + " Like '" + strSearch.Replace("%", "").Replace("*", "") + "%'";
                            }
                            c1PatientEMRExams.DataSource = dv;
                        }
                    }
                }
                else
                {
                    DataView dv = (DataView)c1PatientEMRExams.DataSource;
                    if (dv != null)
                    {
                        strSearch = txtSearch.Text.Trim();
                        string[] strSearchArray = null;
                        //  strSearch = strSearch.Replace("'", "").Replace(",", "").Replace("%", "").Replace("*", "");
                        strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("*", "%");
                        if (strSearch.Length > 1)
                        {
                            string str = strSearch.Substring(1).Replace("%", "");
                            strSearch = strSearch.Substring(0, 1) + str;
                        }
                        if (strSearch.Trim() != "")
                        {
                            strSearchArray = strSearch.Split(',');
                        }

                        if (lblSearch.Tag != null)
                        {
                            // strSearch = strSearch.Replace("%", "").Replace("*", "");

                            if (strSearch.Trim() != "")
                            {
                                if (strSearchArray.Length == 1)
                                {
                                    dv.RowFilter = dv.Table.Columns["ExamName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                        dv.Table.Columns["TemplateName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                        // Bugid- 49716 DOS column  giving error for datetime filtering so it was converted to string 
                                    " CONVERT(" + dv.Table.Columns["DOS"].ColumnName + ",System.String)  Like '" + strSearch + "%' OR " +
                                     "CONVERT(" + dv.Table.Columns["SearchFinishDate"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                     "CONVERT(" + dv.Table.Columns["SearchDOB"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["Code"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["MN"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                    dv.Table.Columns["ProviderName"].ColumnName + " Like '" + strSearch + "%' OR "+
                                    dv.Table.Columns["AccountNote"].ColumnName + " Like '" + strSearch + "%' ";
                                }
                                else
                                {
                                    for (int j = 0; j < strSearchArray.Length; j++)
                                    {
                                        strSearch = strSearchArray[j];
                                        if (strSearch.Trim() != "")
                                        {
                                            if (sFilter == "")//if (j == 0)
                                            {
                                                sFilter = " ( " + dv.Table.Columns["ExamName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    dv.Table.Columns["TemplateName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    // Bugid- 49716 DOS column  giving error for datetime filtering so it was converted to string 
                                                " CONVERT(" + dv.Table.Columns["DOS"].ColumnName + ",System.String)  Like '" + strSearch + "%' OR " +
                                                 "CONVERT(" + dv.Table.Columns["SearchFinishDate"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                                 "CONVERT(" + dv.Table.Columns["SearchDOB"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["Code"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["MN"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["AccountNote"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["ProviderName"].ColumnName + " Like '" + strSearch + "%' )";
                                            }
                                            else
                                            {
                                                sFilter = sFilter + " AND (" + dv.Table.Columns["ExamName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    dv.Table.Columns["TemplateName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                    // Bugid- 49716 DOS column  giving error for datetime filtering so it was converted to string 
                                                " CONVERT(" + dv.Table.Columns["DOS"].ColumnName + ", System.String)  Like '" + strSearch + "%' OR " +
                                                 "CONVERT(" + dv.Table.Columns["SearchFinishDate"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                                 "CONVERT(" + dv.Table.Columns["SearchDOB"].ColumnName.ToString() + ",System.String) Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["Code"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["FirstName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["MN"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["LastName"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["AccountNote"].ColumnName + " Like '" + strSearch + "%' OR " +
                                                dv.Table.Columns["ProviderName"].ColumnName + " Like '" + strSearch + "%' )";
                                            }
                                        }

                                    }
                                    dv.RowFilter = sFilter;
                                }
                            }
                            else
                            {
                                dv.RowFilter = "";
                            }
                            c1PatientEMRExams.DataSource = dv;
                        }
                    }
                }
                #endregion " Search Old"
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            c1PatientEMRExams.Redraw = true;
        }
        #endregion " Search "

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            ToggleSelection(cbSelectAll.Checked);
        }

        private void ToggleSelection(bool isChecked)
        {
            this.Cursor = Cursors.WaitCursor;
            c1PatientEMRExams.FinishEditing();
            c1PatientEMRExams.Redraw = false;

            if ((c1PatientEMRExams != null) && (c1PatientEMRExams.DataSource != null))
            {
                foreach (DataRow dr in ((DataView)c1PatientEMRExams.DataSource).Table.Rows)
                {
                    if (cbSelectAll.Checked)
                    { dr["bSelect"] = 1; }
                    else
                    { dr["bSelect"] = 0; }
                }
            }

            c1PatientEMRExams.Redraw = true;
            this.Cursor = Cursors.Default;
        }

        private void chkGeneralSearch_CheckedChanged(object sender, EventArgs e)
        {
            lblSearch.Text = "Exam Name : ";
            lblSearch.Tag = "";
        }
    }
}
