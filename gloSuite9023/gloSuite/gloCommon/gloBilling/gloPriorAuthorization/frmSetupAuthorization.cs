using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloAuditTrail;
using gloBilling.gloPriorAuthorization;
using gloSettings;
namespace gloBilling
{
    public partial class frmSetupAuthorization : Form
    {

        #region " Declarations "

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = String.Empty;      
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private Int64 _PatientID = 0;

        #endregion " Declarations "
        
        #region " Property Procedures "

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }
     
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedures "

        #region " Constructor "

        public frmSetupAuthorization(Int64 PatientID)
        {
            InitializeComponent();

            _UserID = AppSettings.UserID;
            _ClinicID = AppSettings.ClinicID;
            _PatientID = PatientID;
            _databaseconnectionstring = AppSettings.ConnectionStringPM;

         }

        #endregion " Constructor "

    
      
        #region " Private Methods "

   

   

        private bool Validate()
        {
            try
            {
                if(txtauth.Text.Trim() =="")
                {
                    
                }

                return true;
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;                
            }
            finally
            { 
                
            }
        }

        #endregion " Private Methods "

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                if (SavePriorAuthorization())
                {
                    this.Close();
                }
            }
        }

        private bool SavePriorAuthorization()
        {

            try
            {

                clsgloPriorAuthorization objgloPriorAuth = new clsgloPriorAuthorization();

                objgloPriorAuth.PriorAuthorizationNo = txtauth.Text.Replace("'", "''");
                objgloPriorAuth.PatientID = _PatientID;
                objgloPriorAuth.ReferralID = Convert.ToInt64(cmbReferralProvider.SelectedValue);
                objgloPriorAuth.InsuranceID = Convert.ToInt64(cmbGenInfoInsurance.SelectedValue);
                objgloPriorAuth.StartDate = gloDateMaster.gloDate.DateAsNumber(dtAuthorizationstart.Text);
                objgloPriorAuth.ExpDate = gloDateMaster.gloDate.DateAsNumber(dtauthexp.Text);
                if (rdblimityes.Checked == true)
                {
                    objgloPriorAuth.IsTrackAuthLimit = true;
                }
                else
                {
                    objgloPriorAuth.IsTrackAuthLimit = false;
                }
                objgloPriorAuth.IsInActive = true;
                objgloPriorAuth.VisitsAllowed = Convert.ToInt16(txtvisitsallow.Text.ToString().Trim());
                objgloPriorAuth.InsuranceNote = txtInsnote.Text.Replace("'", "''");
                if (radreferralin.Checked == true)
                {
                    objgloPriorAuth.AuthorizationType = clsgloPriorAuthorization.AuthorizationTypeEnum.ReferralIn.GetHashCode();
                }
                else if (radreferralout.Checked == true)
                {
                    objgloPriorAuth.AuthorizationType = clsgloPriorAuthorization.AuthorizationTypeEnum.ReferralOut.GetHashCode();
                }
                else if (radboth.Checked == true)
                {
                    objgloPriorAuth.AuthorizationType = clsgloPriorAuthorization.AuthorizationTypeEnum.Both.GetHashCode();
                }

                objgloPriorAuth.AuthorizationNote = txtAuthorizationNote.Text.ToString().Trim().Replace("'", "''"); ;

                objgloPriorAuth.Add();
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            { 
                
            }

        }

        
        private void FillReferralProviders(Int64 PatientId)
        {
            try
            {
                DataTable dt;
                dt = GetPatientReferral(PatientId);

                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["nReferralID"] = 0;
                    dr["sReferralName"] = "";
                    dt.Rows.InsertAt(dr, 0);

                    if (dt.Rows.Count > 0)
                    {
                        cmbReferralProvider.DataSource = dt.Copy();
                        cmbReferralProvider.ValueMember = dt.Columns["nReferralID"].ColumnName;
                        cmbReferralProvider.DisplayMember = dt.Columns["sReferralName"].ColumnName;
                        cmbReferralProvider.Refresh();

                        if (cmbReferralProvider.Items.Count > 1)
                        { cmbReferralProvider.SelectedIndex = 1; }
                        else
                        { cmbReferralProvider.SelectedIndex = 0; }
                    }


                }
                dt = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }

        }


        private DataTable GetPatientReferral(Int64 PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtReferrals = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT ISNULL(nPatientDetailID,0) AS nReferralID, " +
                  " ISNULL(sFirstName,'') +SPACE(1)+ISNULL(sMiddleName,'')+SPACE(1) + ISNULL(sLastName,'') AS sReferralName " +
                  " FROM Patient_DTL WHERE nPatientID = " + PatientId + " AND nClinicID = " + _ClinicID + " " +
                  " AND nContactFlag  = " + gloPatient.PatientContactType.Referral.GetHashCode() + " ";
                oDB.Retrive_Query(_sqlQuery, out dtReferrals);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtReferrals != null) { dtReferrals.Dispose(); }
            }

            return dtReferrals;
        }


        private void FillInsurance(Int64 PatientId)
        {
            try
            {
                DataTable dt;
                dt = GetPatientInsurance(PatientId);

                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["nInsuranceID"] = 0;
                    dr["sInsuranceName"] = "";
                    dt.Rows.InsertAt(dr, 0);

                    if (dt.Rows.Count > 0)
                    {
                        cmbGenInfoInsurance.DataSource = dt.Copy();
                        cmbGenInfoInsurance.ValueMember = dt.Columns["nInsuranceID"].ColumnName;
                        cmbGenInfoInsurance.DisplayMember = dt.Columns["sInsuranceName"].ColumnName;
                        cmbGenInfoInsurance.Refresh();

                        if (cmbGenInfoInsurance.Items.Count > 1)
                        { cmbGenInfoInsurance.SelectedIndex = 1; }
                        else
                        { cmbGenInfoInsurance.SelectedIndex = 0; }
                    }


                }
                dt = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }

        }

        private DataTable GetPatientInsurance(Int64 PatientId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtInsurances = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT nInsuranceID,sInsuranceName FROM PatientInsurance_DTL WHERE nPatientID=" + PatientId;
                oDB.Retrive_Query(_sqlQuery, out dtInsurances);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtInsurances != null) { dtInsurances.Dispose(); }
            }

            return dtInsurances;
        }


        private void btnAdd_Referral_Click(object sender, EventArgs e)
        {
            Int64 _currentPatientId = 0;
            gloUserRights.ClsgloUserRights ObjUserRights = new gloUserRights.ClsgloUserRights(_databaseconnectionstring);

            try
            {

                   ObjUserRights.CheckForUserRights(_UserID);
                if (ObjUserRights.ModifyPatient == true)
                {
                    if (this._PatientID > 0)
                    {
                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                        ogloPatient.ShowPatientRegistration(this._PatientID, gloPatient.ModifyPatientDetailType.Referral, out _currentPatientId,this);
                        FillReferralProviders(_currentPatientId);
                        ogloPatient.Dispose();
                        ogloPatient = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                try
                {
                    if (ObjUserRights != null)
                    {
                        ObjUserRights.Dispose();
                        ObjUserRights = null;
                    }
                }
                catch
                {
                }
            }
        }

        

        private void frmSetupAuthorization_Load_1(object sender, EventArgs e)
        {
            FillPatient(_PatientID);
            FillInsurance(_PatientID);
            FillReferralProviders(_PatientID);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillPatient(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPatient = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT dbo.GET_NAME(sFirstName, sMiddleName, sLastName) AS Patientname FROM Patient WHERE nPatientID=" + PatientID;
                oDB.Retrive_Query(_sqlQuery, out dtPatient);
                lblPatientName1.Text = dtPatient.Rows[0]["Patientname"].ToString().Trim();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (dtPatient != null) { dtPatient.Dispose(); }
            }
        }

        private void rdblimityes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdblimityes.Checked == true)
            {
                dtauthexp.Enabled = true;
                dtAuthorizationstart.Enabled = true;
                txtvisitsallow.Enabled = true;
            }
            else
            {
                dtauthexp.Enabled = false;
                dtAuthorizationstart.Enabled = false;
                txtvisitsallow.Enabled = false;
            }
        }

        private void txtvisitsallow_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != Convert.ToChar(8) && e.KeyChar != Convert.ToChar(46))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9]*$") == false)
                    {
                        e.Handled = true;
                    }
                }
                else if (e.KeyChar == Convert.ToChar(46))
                {
                    e.Handled = true;
                }

               
            }
            catch (System.OverflowException ex)
            {
                MessageBox.Show("Amount is invalid, Please enter a valid amount", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                return;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }
    

    }
}