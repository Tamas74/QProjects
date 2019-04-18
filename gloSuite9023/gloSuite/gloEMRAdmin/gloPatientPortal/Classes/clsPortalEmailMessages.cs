using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace gloPatientPortal
{
    class clsPortalEmailMessages
    {
        SqlConnection Conn = null;
        SqlCommand Cmd;

        public DataTable FillControls(string sqlconn, int MessageType)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("WS_GetEmailTemplateMessages", Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@MessageType", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = MessageType;

                adpt.Fill(ds);
                Conn.Close();
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }

        public DataTable GetEmailMessageDetails(string sqlconn, int nMessageType, Int64 nEmailMessageID)
        {
            DataSet ds = null;
            SqlDataAdapter adpt = null;
            try
            {
                adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("WS_GetEmailTemplateMessages_Details", Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@MessageType", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nMessageType;

                objParam = Cmd.Parameters.Add("@nEmailMessageID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nEmailMessageID;

                adpt.Fill(ds);
                Conn.Close();
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return null;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
        }

        public long UpdateEmailMessageDetails(string sqlconn, int nMessageType, Int64 nEmailMessageID, string sEmailSubject, string sEmailContent, Int64 nUserID)
        {
            long Id = 0;
            SqlDataAdapter adpt = null;
            try
            {
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("WS_INUPEmailTemplateMessages_Details", Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                //adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@MessageType", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nMessageType;

                objParam = Cmd.Parameters.Add("@nEmailMessageID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nEmailMessageID;

                objParam = Cmd.Parameters.Add("@sEmailSubject", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sEmailSubject;

                objParam = Cmd.Parameters.Add("@sEmailContent", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sEmailContent;

                objParam = Cmd.Parameters.Add("@nUserID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nUserID;

                Conn.Open();

                Id = Convert.ToInt64(Cmd.ExecuteScalar());
                Conn.Close();
                
            }
            catch (SqlException ex)
            {
                Conn.Close();
                return 0;
            }
            catch (Exception ex)
            {
                Conn.Close();
                return 0;
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (adpt != null)
                {
                    adpt.Dispose();
                    adpt = null;
                }
                if (Conn != null)
                {
                    Conn.Dispose();
                    Conn = null;
                }
            }
            return Id;
        }
    }
}
