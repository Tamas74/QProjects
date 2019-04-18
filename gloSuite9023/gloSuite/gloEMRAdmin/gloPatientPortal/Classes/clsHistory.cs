using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace gloPatientPortal.Classes
{
    class clsHistory
    {
        SqlConnection Conn = null;
        SqlCommand Cmd;

        public DataTable FillControls(string sqlconn)
        {
            DataSet ds =null;
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("gsp_FillCategory_Mst", Conn);
                SqlParameter objParam = default(SqlParameter);

                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

                objParam = Cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = "History";

                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = 1;

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
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }
        }

        public long AddPFLibrary(long nCategoryID, string sPublishName, string sCategoryType, string sPreText, string sPostText, long nAnswerType, long nUserId, string sqlconn, long LibId, bool IsModify)
        {
            long Id=0;
            try
            {
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);
                Cmd = new System.Data.SqlClient.SqlCommand("WS_PFLibraryInsertUpdate", Conn);
                Cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter objParam = default(SqlParameter);

                objParam = Cmd.Parameters.Add("@nCategoryID", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nCategoryID;

                objParam = Cmd.Parameters.Add("@sPublishName", SqlDbType.VarChar, 500);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sPublishName;

                objParam = Cmd.Parameters.Add("@sCategoryType", SqlDbType.VarChar, 100);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sCategoryType;

                objParam = Cmd.Parameters.Add("@sPreText", SqlDbType.VarChar, 500);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sPreText;

                objParam = Cmd.Parameters.Add("@sPostText", SqlDbType.VarChar, 500);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = sPostText;

                objParam = Cmd.Parameters.Add("@nAnswerType", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nAnswerType;

                objParam = Cmd.Parameters.Add("@nUserId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = nUserId;

                objParam = Cmd.Parameters.Add("@LibId", SqlDbType.BigInt);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = LibId;

                objParam = Cmd.Parameters.Add("@IsModify", SqlDbType.Bit);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = IsModify;

                Conn.Open();


                Id = Convert.ToInt64(Cmd.ExecuteScalar());

                Conn.Close();
            }
            catch (SqlException ex)
            {
                Conn.Close();

            }
            catch (Exception ex)
            {
                Conn.Close();
            }
            finally
            {
                if (Cmd != null)
                {
                    Cmd.Dispose();
                    Cmd = null;
                }
            }
            return Id;
        }

        public DataTable GetGroups(string sqlconn)
        {
            DataSet ds = null;
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter();
                ds = new DataSet();
                Conn = new System.Data.SqlClient.SqlConnection(sqlconn);

                Cmd = new SqlCommand("WS_SelectGroups", Conn);
                
                Cmd.CommandType = CommandType.StoredProcedure;
                adpt.SelectCommand = Cmd;

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
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }
        }
    
    }

}
