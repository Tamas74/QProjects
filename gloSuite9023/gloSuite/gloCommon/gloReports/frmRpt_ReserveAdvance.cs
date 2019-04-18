using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;


namespace gloReports
{
    public partial class frmRpt_ReservAvailable : Form
    {

        #region "Variable Declaration"

        private Font fBold = gloGlobal.clsgloFont.gFont_BOLD;//new Font("Tahoma", 9, FontStyle.Bold);
        private Font fRegular = gloGlobal.clsgloFont.gFont;//new Font("Tahoma", 9, FontStyle.Regular);
        private string _databaseconnectionstring = "";
        private string _UserName = "";
        private string _MessageBoxCaption = "";
     //   gloListControl.gloListControlType _CurrentControlType = gloListControl.gloListControlType.Other;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID;
      //  private string _reportDate="";
        Rpt_Available_Reserves oRpt_Available_Reserves;
        Rpt_SubReportPatReserveDetails oRpt_SubReportPatReserveDetails;
        Rpt_SubReportInsuranceReserveDetails oRpt_SubReportInsuranceReserveDetails;
        dsEOBPaymentReports _dsEOBPaymentReports;
        ReportDocument _rptName = null;

        CrystalDecisions.Shared.ParameterFields paramFields =null;
        CrystalDecisions.Shared.ParameterDiscreteValue discValue = null;
        CrystalDecisions.Shared.ParameterField paramField = null;

        #endregion

        #region "Constructor"
        public frmRpt_ReservAvailable(string databaseconnectionstring)
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

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM"; ;
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; ; }
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            #endregion
           
        } 
        #endregion 

        #region "Function"

           
        private string getCloseDate()
        {
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                object _Result = oDB.ExecuteScalar_Query("select dbo.Convert_to_date(max(nCloseDayDate)) As CloseDate from BL_CloseDays");
                if (_Result.ToString() != "")
                {
                    return _Result.ToString();
                }
                else
                {

                    return "";

                }
            }
            catch (Exception e)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(e.ToString(), false);
                e = null; 
               return  "Error in Returning Date.";
            }
        }

        private string getClinicName()
        {
            try
            {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            object _Result = oDB.ExecuteScalar_Query("select coalesce(sClinicName,'') as  sClinicName  from Clinic_MST");
            if (_Result.ToString() != "")
            {
                return _Result.ToString();
            }
            else
            {

                return "";

            }

        }
            catch (Exception e)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(e.ToString(), false);
                e = null; 
                return "Error in Returning Clinic Name.";
            }
        }

        private void closed()
        {

            //if (!(object.ReferenceEquals(fBold, null)))
            //    fBold.Dispose();
            //if (!(object.ReferenceEquals(fRegular, null)))
            //    fRegular.Dispose();
            //fBold = null;
            //fRegular = null;
        }

        private void reportClosed()
        {
            if (!(object.ReferenceEquals(oRpt_Available_Reserves, null)))
            {
                oRpt_Available_Reserves.Close();
                oRpt_Available_Reserves.Dispose();
                oRpt_Available_Reserves = null;
            }
            if (!(object.ReferenceEquals(oRpt_SubReportPatReserveDetails, null)))
            {
                oRpt_SubReportPatReserveDetails.Close();
                oRpt_SubReportPatReserveDetails.Dispose();
                oRpt_SubReportPatReserveDetails = null;
            }
            if (!(object.ReferenceEquals(oRpt_SubReportInsuranceReserveDetails, null)))
            {
                oRpt_SubReportInsuranceReserveDetails.Close();
                oRpt_SubReportInsuranceReserveDetails.Dispose();
                oRpt_SubReportInsuranceReserveDetails = null;
            }
           
        
        }

        #endregion

        #region " Tool Strip Events "
        
        private void tsb_GenerateReport_Click(object sender, EventArgs e)
        {
            _rptName = null;
           
            Cursor.Current = Cursors.WaitCursor;
            GenerateReserveReport();
            Cursor.Current = Cursors.Default;

           
        }

        private void tsb_btnExport_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            crvReportViewer.ExportReport();
            Cursor.Current = Cursors.Default;
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            closed();
            if (!(object.ReferenceEquals(_rptName, null)))
            {
                _rptName.Close();
                _rptName.Dispose();
                _rptName = null;
            }
            this.Close();
        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {
                crvReportViewer.PrintReport();
        } 

        #endregion

        #region "Form Events "

        private void frmRpt_ReservAvailable_Load(object sender, EventArgs e)
        {
            btnUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnUP.BackgroundImageLayout = ImageLayout.Center;
            btnDown.Visible = false;
            rbtn_Both.Font = fBold;
            FillLastcloseDate();
            Cursor.Current = Cursors.WaitCursor;
            GenerateReserveReport();
            Cursor.Current = Cursors.Default;
            
        }

        private void frmRpt_FinancialSummary_FormClosed(object sender, FormClosedEventArgs e)
        {
            closed();
            if (!(object.ReferenceEquals(_rptName, null)))
            {
                _rptName.Close();
                _rptName.Dispose();
                _rptName = null;
            }
        } 


        #endregion

        #region " User Control Events "
        
        private void btnUP_Click(object sender, EventArgs e)
        {
            try
            {
                btnUP.Visible = false;
                btnDown.Visible = true;
                //fpnlCriteria.Visible = true;
                pnlCriteria.Visible = false;
                pnlCriteria.Refresh();
                btnDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
                btnDown.BackgroundImageLayout = ImageLayout.Center;
                lblbtnDown.Visible = true;
            }
            catch //(Exception ex)
            {

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
                pnlCriteria.Visible = true;
                btnUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
                btnUP.BackgroundImageLayout = ImageLayout.Center;
                lblbtnDown.Visible = true;
            }
            catch //(Exception ex)
            {
            }
            finally
            {
            }
        }

        private void btnDown_MouseHover(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloReports.Properties.Resources.DownHover;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDown_MouseLeave(object sender, EventArgs e)
        {
            btnDown.BackgroundImage = global::gloReports.Properties.Resources.Down;
            btnDown.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnUP_MouseHover(object sender, EventArgs e)
        {
            btnUP.BackgroundImage = global::gloReports.Properties.Resources.UPHover;
            btnUP.BackgroundImageLayout = ImageLayout.Center;

        }

        private void btnUP_MouseLeave(object sender, EventArgs e)
        {
            btnUP.BackgroundImage = global::gloReports.Properties.Resources.UP;
            btnUP.BackgroundImageLayout = ImageLayout.Center;
        }

        private void rbtn_Patient_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Patient.Checked)
            { rbtn_Patient.Font = fBold; }
            else
            { rbtn_Patient.Font = fRegular; }
        }

        private void rbtn_InsCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_InsCompany.Checked)
            { rbtn_InsCompany.Font = fBold; }
            else
            { rbtn_InsCompany.Font = fRegular; }
        }

        private void rbtn_Both_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Both.Checked)
            { rbtn_Both.Font = fBold; }
            else
            { rbtn_Both.Font = fRegular; }
        } 

        #endregion
        
        #region "Fill Methods"

        private void FillLastcloseDate()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            System.Data.DataTable _dtclosedate = new System.Data.DataTable();
            string _sqlQuery = "";
            try
            {
                _sqlQuery = "select dbo.CONVERT_TO_DATE (max(nCloseDayDate)) as nCloseDayDate from dbo.BL_CloseDays";
                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtclosedate);

                if (_dtclosedate.Rows.Count > 0)
                {
                    dtpEndDate.Value = Convert.ToDateTime(_dtclosedate.Rows[0][0]);
                }
                else
                {
                    dtpEndDate.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }

                oDB.Disconnect();

            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            { if (oDB != null) { oDB.Dispose(); } }

            //   return  _dtclosedate;

        }

        private void GenerateReserveReport()
        {
            SqlCommand _sqlCommand = new SqlCommand();
            SqlConnection _sqlConnection = new SqlConnection(_databaseconnectionstring);
            _dsEOBPaymentReports = new dsEOBPaymentReports();
           
            SqlDataAdapter da = null;
            try
            {

                #region "Patient Reserve"
                
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.CommandText = "Rpt_SELECT_PaymentTransaction_UseReserve_Patient";
                _sqlCommand.Connection = _sqlConnection;
                _sqlCommand.CommandTimeout = 0;
                _sqlCommand.Parameters.Add("@nClinicID", SqlDbType.Int).Value = _ClinicID;
                _sqlCommand.Parameters.Add("@nCloseDate", SqlDbType.Int).Value = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.Date.ToShortDateString());

                da = new SqlDataAdapter(_sqlCommand);
                da.Fill(_dsEOBPaymentReports, "dt_PatientReserve");
                _sqlCommand.Parameters.Clear();

                #endregion

                #region "Insurance Reserve"

                _sqlCommand.CommandText = "Rpt_SELECT_PaymentTransaction_UseReserve_Insurance";
                _sqlCommand.Connection = _sqlConnection;
                _sqlCommand.CommandTimeout = 0;
                _sqlCommand.Parameters.Add("@nClinicID", SqlDbType.Int).Value = _ClinicID;
                _sqlCommand.Parameters.Add("@nCloseDate", SqlDbType.Int).Value = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.Date.ToShortDateString());

                da = new SqlDataAdapter(_sqlCommand);
                da.Fill(_dsEOBPaymentReports, "dt_InsuranceReserve");
               
                _sqlCommand.Parameters.Clear();

                #endregion

                #region " Summary "

                _sqlCommand.CommandText = "Rpt_ReserveSummary";
                _sqlCommand.Connection = _sqlConnection;
                _sqlCommand.CommandTimeout = 0;
                _sqlCommand.Parameters.Add("@nClinicID", SqlDbType.Int).Value = _ClinicID;
                _sqlCommand.Parameters.Add("@nCloseDate", SqlDbType.Int).Value = gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.Date.ToShortDateString());

                da = new SqlDataAdapter(_sqlCommand);
                da.Fill(_dsEOBPaymentReports, "dtReserveSummary");
                _sqlCommand.Parameters.Clear();

                #endregion

                DataTable dtparam = new DataTable();
                dtparam.Columns.Add("User");
                dtparam.Columns.Add("EndDate");
                dtparam.Columns.Add("ReportType");
                dtparam.Columns.Add("Practice");

                
                dtparam.Rows.Add();

                dtparam.Rows[0]["User"] = _UserName;
                dtparam.Rows[0]["Practice"] = getClinicName();
                dtparam.Rows[0]["EndDate"] = dtpEndDate.Value.Date.ToShortDateString();

                string sReportType="";

                if(rbtn_Both.Checked)
                {
                    sReportType="Both";
                }
                else if (rbtn_InsCompany.Checked)
                {
                     sReportType="Insurance Company";
                }
                else if (rbtn_Patient.Checked)
                {
                     sReportType="Patient";
                }
                dtparam.Rows[0]["ReportType"] = sReportType;

                foreach (DataRow orow in dtparam.Rows)
                    _dsEOBPaymentReports.Tables["dtReserveReportParam"].ImportRow(orow);


                ShowHideRegions();

            }
            catch //(Exception ex)
            {
                da.Dispose();
            }
            finally
            {
                if (_sqlCommand  != null)
                {
                    _sqlCommand .Parameters.Clear();
                    _sqlCommand.Dispose();
                    _sqlCommand  = null;
                }
                if (_sqlConnection   != null && _sqlConnection .State == ConnectionState.Open)
                {
                    _sqlConnection.Close();
                    _sqlConnection .Dispose();

                }
                da.Dispose();
                
            }
        }

        private void ShowHideRegions()
        {

            oRpt_Available_Reserves = new Rpt_Available_Reserves();

            #region "Include Details "

            paramFields = new CrystalDecisions.Shared.ParameterFields();
            discValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
            paramField = new CrystalDecisions.Shared.ParameterField();

            paramField.Name = "IncludeDetails";
            paramField.HasCurrentValue = true;
            int Value = 0;
            if (chkIncludeDetails.Checked)
                Value = 1;
            else
                Value = 0;

            discValue.Value = Value;
            paramField.CurrentValues.Add(discValue);
            paramFields.Add(paramField);

            #endregion

            #region "Patient"

            CrystalDecisions.Shared.ParameterDiscreteValue PatdiscValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
            CrystalDecisions.Shared.ParameterField PatparamField = new CrystalDecisions.Shared.ParameterField();

            PatparamField.Name = "ShowPatientDetails";
            PatparamField.HasCurrentValue = true;
            int PatValue = 0;
            if (!chkIncludeDetails.Checked)
                PatValue = 0;
            else if (rbtn_Both.Checked)
                PatValue = 1;
            else if (rbtn_Patient.Checked)
                PatValue = 1;
            else
                PatValue = 0;

            PatdiscValue.Value = PatValue;
            PatparamField.CurrentValues.Add(PatdiscValue);
            paramFields.Add(PatparamField);


            CrystalDecisions.Shared.ParameterDiscreteValue PatSumdiscValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
            CrystalDecisions.Shared.ParameterField PatSumparamField = new CrystalDecisions.Shared.ParameterField();

            PatSumparamField.Name = "ShowPatientSummary";
            PatSumparamField.HasCurrentValue = true;
            int PatSummValue = 0;
            
            if (rbtn_Both.Checked)
                PatSummValue = 1;
            else if (rbtn_Patient.Checked)
                PatSummValue = 1;
            else
                PatSummValue = 0;

            PatSumdiscValue.Value = PatSummValue;
            PatSumparamField.CurrentValues.Add(PatSumdiscValue);
            paramFields.Add(PatSumparamField);

            

            CrystalDecisions.Shared.ParameterDiscreteValue InsSumdiscValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
            CrystalDecisions.Shared.ParameterField InsSumparamField = new CrystalDecisions.Shared.ParameterField();

            InsSumparamField.Name = "ShowInsuranceSummary";
            InsSumparamField.HasCurrentValue = true;
            int InsSummValue = 0;
            
            if (rbtn_Both.Checked)
                InsSummValue = 1;
            else if (rbtn_InsCompany.Checked)
                InsSummValue = 1;
            else
                InsSummValue = 0;

            InsSumdiscValue.Value = InsSummValue;
            InsSumparamField.CurrentValues.Add(InsSumdiscValue);
            paramFields.Add(InsSumparamField);




            #endregion

            #region "Insurance"

            CrystalDecisions.Shared.ParameterDiscreteValue InsdiscValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
            CrystalDecisions.Shared.ParameterField InsparamField = new CrystalDecisions.Shared.ParameterField();

            InsparamField.Name = "ShowInsuranceDetails";
            InsparamField.HasCurrentValue = true;
            int InsValue = 0;
            if (!chkIncludeDetails.Checked)
                InsValue = 0;
            else if (rbtn_Both.Checked)
                InsValue = 1;
            else if (rbtn_InsCompany.Checked)
                InsValue = 1;
            else
                InsValue = 0;

            InsdiscValue.Value = InsValue;
            InsparamField.CurrentValues.Add(InsdiscValue);

            #endregion

            paramFields.Add(InsparamField);
            this.crvReportViewer.ParameterFieldInfo = paramFields;
            oRpt_Available_Reserves.SetDataSource(_dsEOBPaymentReports);
            crvReportViewer.ReportSource = oRpt_Available_Reserves;

        }

      
        #endregion

        private void chkIncludeDetails_CheckedChanged(object sender, EventArgs e)
        {
            ShowHideRegions();
           
        }

    }
}