using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using gloEMRGeneralLibrary.gloEMRDatabase;
namespace gloCommunity.Classes
{
    class clsSmartCPT
    {
       

        public DataTable FetchassociatedCPT()
        {
            SqlConnection Conn;
            SqlCommand Cmd = null;
            DataTable dt = new DataTable();
            SqlDataAdapter ad = default(SqlDataAdapter);

            try
            {
                Conn = new SqlConnection(clsGeneral.EMRConnectionString);
                //Check connection states
                if (Conn.State == ConnectionState.Closed)
                {
                    Conn.Open();
                }

                Cmd = new System.Data.SqlClient.SqlCommand("gsp_AssociatedCPT", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                ad = new SqlDataAdapter(Cmd);
                ad.Fill(dt);
                ad.Dispose();
                ad = null;
                Conn.Close();
                Conn.Dispose();
                Conn = null;
                return dt;
            }
            catch //(Exception ex)
            {
                // MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }
            }
        }

        public DataTable FetchICD9forUpdate(long CPTID)
        {
            SqlConnection Conn;
            SqlCommand Cmd = null;
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter sqladpt = new SqlDataAdapter();
                Conn = new SqlConnection(clsGeneral.EMRConnectionString);

                Cmd = new System.Data.SqlClient.SqlCommand("gsp_ScanCPTICD9Association", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                sqladpt.SelectCommand = Cmd;

                SqlParameter objParam = default(SqlParameter);

                objParam = Cmd.Parameters.Add("@CPTID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = CPTID;

                sqladpt.Fill(dt);
                sqladpt.Dispose();
                sqladpt = null;
                Conn.Close();
                Conn.Dispose();
                Conn = null;
                if (objParam != null)
                    objParam = null;
                return dt;
            }
            catch //(SqlException ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.Message,clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            //catch (Exception ex)
            //{
            //    //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.CPT, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            //    //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return null;
            //}
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Parameters.Clear();
                    Cmd.Dispose();
                    Cmd = null;
                }
            }

        }
    }
}
