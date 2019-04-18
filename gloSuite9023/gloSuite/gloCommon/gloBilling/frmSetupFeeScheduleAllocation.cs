using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gloBilling
{
    public partial class frmSetupFeeScheduleAllocation : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;
        private Int64 _nFeeScheduleID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;


        #endregion " Declarations "

        public frmSetupFeeScheduleAllocation(String DatabaseConnectionString,Int64 FeeScheduleID)
        {
              InitializeComponent();

            _databaseconnectionstring = DatabaseConnectionString;
            _nFeeScheduleID = FeeScheduleID ;
          
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

        private void frmSetupFeeScheduleAllocation_Load(object sender, EventArgs e)
        {
            FillFeeSchedules();
        }

        private void FillFeeSchedules()
        {
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);

                string _sqlQuery = " SELECT ISNULL(nFeeScheduleID,0) AS nFeeScheduleID,ISNULL(sFeeScheduleName,'') AS sFeeScheduleName "
                + " FROM BL_FeeSchedule_MST WHERE nClinicID = " + _ClinicID ;

                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();

                if (dt != null)
                {
                    cmbFeeSchedule.DataSource = dt.Copy();
                    cmbFeeSchedule.ValueMember = "nFeeScheduleID";
                    cmbFeeSchedule.DisplayMember = "sFeeScheduleName";

                    if (_nFeeScheduleID != 0)
                        cmbFeeSchedule.SelectedValue = _nFeeScheduleID;
                    else
                        cmbFeeSchedule.SelectedIndex = -1; 
                    cmbFeeSchedule.Refresh();  
                }

               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        private void FillFeeScheduleAllocation(long FeeScheduleID)
        {
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);

                string _sqlQuery = "SELECT ISNULL(BL_FeeSchedule_MST.nFeeScheduleID,0) AS nFeeScheduleID, ISNULL(BL_FeeSchedule_MST.sFeeScheduleName,'') AS sFeeScheduleName, "
                + " ISNULL(BL_FeeSchedule_Allocation.nFromDate,0) AS  nFromDate,ISNULL(BL_FeeSchedule_Allocation.nToDate,0) AS nToDate"
                + " FROM  BL_FeeSchedule_Allocation INNER JOIN BL_FeeSchedule_MST ON BL_FeeSchedule_Allocation.nFeeScheduleID = BL_FeeSchedule_MST.nFeeScheduleID"
                + " WHERE BL_FeeSchedule_MST.nFeeScheduleID = " + FeeScheduleID + " AND  BL_FeeSchedule_Allocation.nClinicID = " + _ClinicID;

                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();

                if (dt != null && dt.Rows.Count > 0)
                {
                    cmbFeeSchedule.Text = Convert.ToString(dt.Rows[0]["sFeeScheduleName"]);
                    dtpStartdate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[0]["nFromDate"]));
                    dtpEndDate.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dt.Rows[0]["nToDate"]));
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }

        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            //frmImportFeeSchedule ofrm = new frmImportFeeSchedule(_databaseconnectionstring);
            //ofrm.ShowDialog();
            //ofrm.Dispose();  
            this.Close(); 
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveData() == true)
                {
                    this.Close();  
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private bool SaveData()
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                if (ValidateData() == false)
                    return false;

                oDB.Connect(false);

                string _sqlQuery = " DELETE FROM BL_FeeSchedule_Allocation WHERE nFeeScheduleID = " + Convert.ToInt64(cmbFeeSchedule.SelectedValue) + " AND nClinicID = " + _ClinicID;
                oDB.Execute_Query(_sqlQuery);

                _sqlQuery = "INSERT INTO BL_FeeSchedule_Allocation (nFeeScheduleID, nFromDate, nToDate, nClinicID) "
                + " VALUES (" + Convert.ToInt64(cmbFeeSchedule.SelectedValue) + ", " + gloDateMaster.gloDate.DateAsNumber(dtpStartdate.Value.ToShortDateString()) + ", " + gloDateMaster.gloDate.DateAsNumber(dtpEndDate.Value.ToShortDateString()) + ", " + _ClinicID + ")";
                oDB.Execute_Query(_sqlQuery);

                oDB.Disconnect();

                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = false;
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

        private bool ValidateData()
        {
            if (cmbFeeSchedule.SelectedIndex == -1)
            {
                MessageBox.Show("Select Fee Schedule.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbFeeSchedule.Focus();
                return false; 
            }

            if (dtpStartdate.Value.Date > dtpEndDate.Value.Date)
            {
                MessageBox.Show("Start date cannot be greater than end date.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpStartdate.Focus();
                return false;
            }
            return true; 
        }

        private void cmbFeeSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(cmbFeeSchedule.SelectedIndex != -1)
                    FillFeeScheduleAllocation(Convert.ToInt64(cmbFeeSchedule.SelectedValue));
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
        }

        private void tsbOnlySave_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveData() == true)
                {
                    cmbFeeSchedule.SelectedIndex = -1;
                    _nFeeScheduleID = 0;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

       


    }
}