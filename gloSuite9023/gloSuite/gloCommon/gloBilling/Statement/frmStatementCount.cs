using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloBilling.Statement
{
    public partial class frmStatementCount : Form
    {
        public frmStatementCount()
        {
            InitializeComponent();
        }

        #region "Variable Declaration"
        
        Int64 _nPAccountID = 0;
        Int64 _nAccountPatientID = 0;
        Int64 _nPatientID = 0;       
        Int64 LastStatementCount = 0;

        #endregion

        #region "Property Procedure"

        public Int64 PAccountID
        {
            get { return _nPAccountID; }
            set { _nPAccountID = value; }
        }

        public Int64 AccountPatientID
        {
            get { return _nAccountPatientID; }
            set { _nAccountPatientID = value; }
        }

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        #endregion

        #region "Private Methods"

        private void GetStatementCount(Int64 PAccountID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            oDB.Connect(false);
            try
            {
                String _SQLString = " select ISNULL(nStatementCount,-1) AS nStatementCount,dtCountDate From cl_statementcount"
                                  + " where nPAccountID=" + PAccountID;
                DataTable dtresult;
                oDB.Retrive_Query(_SQLString, out dtresult);
                //dgAllNotes.DataSource = _sresult;

                if (dtresult != null && dtresult.Rows.Count > 0)
                {
                    
                  if( Convert.ToString(dtresult.Rows[0]["nStatementCount"]) != "-1")
                  {
                    cmbStatementCount.Text = Convert.ToString(dtresult.Rows[0]["nStatementCount"]);
                    LastStatementCount = Convert.ToInt64(dtresult.Rows[0]["nStatementCount"]);                 
                  }
                  else 
                  {
                    cmbStatementCount.Text ="";
                      
                  }
                 
                }
                else
                {
                    cmbStatementCount.Text = "0";                  
                    //txtStatementNote.Text = GetStatementNotes(PatientID, dtFromDate.Value, dtToDate.Value);
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        private Int64 SetStatementCount()
        {


            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            Int64 _result = 0;
            try
            {
                object _intresult = 0;               
                oDBParameters.Add("@nPatientID", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nPAccountID", PAccountID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nAccountPatientID", AccountPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtCountDate", gloDateMaster.gloDate.DateAsDate(gloDateMaster.gloDate.DateAsNumber(getCloseDate())), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Date);

                if (cmbStatementCount.Text == "")
                {
                    oDBParameters.Add("@nStatementCount",DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                }
                else
                {
                    oDBParameters.Add("@nStatementCount", Convert.ToInt16(cmbStatementCount.Text), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                }
                oDBParameters.Add("@nUserID", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sUserName", gloGlobal.gloPMGlobal.UserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@IsManual", true, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDB.Execute("gsp_INUP_Patient_Statement_Count", oDBParameters, out _intresult);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Modify, "Statement Count Modified From " + LastStatementCount + " to " + cmbStatementCount.Text, PatientID, PAccountID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                           
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;

        }

        public string getCloseDate()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                oDB.Connect(false);
                object _Result = oDB.ExecuteScalar_Query("SELECT ISNULL(dbo.Convert_to_date(max(nCloseDayDate)),CONVERT(VARCHAR(10),dbo.gloGetDate(),101)) As CloseDate from BL_CloseDays WITH (NOLOCK)");
                if (_Result.ToString() != "")
                {
                    return _Result.ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }

        #endregion

        #region "Tool Strip Events"

        private void tls_btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {

                if ((cmbStatementCount.Text == "") || LastStatementCount != Convert.ToInt64(cmbStatementCount.Text))
                {
                    Int64 _result = SetStatementCount();
                    //Bug #68473: CR00000352 : RCM Queue issue
                    gloStatment oStatement = new gloStatment();
                    if (Convert.ToInt64(cmbStatementCount.Text) == 0 && !(oStatement.GetIsPaymentPlan(PAccountID)))
                    {
                        Collections.CL_FollowUpCode.DeleteAccountFollowUp(PAccountID);
                    }

                }
                //else
                //{ 
                
                //    }

                this.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        private void tls_btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region "Form Load"

        private void frmStatementCount_Load(object sender, EventArgs e)
        {
            try
            {
                GetStatementCount(PAccountID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }
        
        #endregion

        private void cmbStatementCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) || e.KeyChar == '')
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void cmbStatementCount_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenu cntMenu = null;// new System.Windows.Forms.ContextMenu();
                cmbStatementCount.ContextMenu = cntMenu;
            }
        }
    }
}
