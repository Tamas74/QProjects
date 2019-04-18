using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace gloOffice
{
    public class gloDataDictionary
    {
        #region " Private Variables "
        private string _DataBaseConnectionString;
        #endregion 

        #region " Constructor "
        public gloDataDictionary(string sDataBaseConnectionString)
        {
            _DataBaseConnectionString = sDataBaseConnectionString;
        }
        #endregion

        public void AddDataDictionary(string sFieldName, string sTableName, string sFieldCaption, string sTableCaption)
        {
            SqlCommand cmd = null;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(_DataBaseConnectionString);
                cmd = new SqlCommand("gsp_InUpDataDictionary", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlParam = default(SqlParameter);

                sqlParam = cmd.Parameters.AddWithValue("@DictionaryID", 0);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.SqlDbType = SqlDbType.BigInt;

                sqlParam = cmd.Parameters.AddWithValue("@FieldName", sFieldName);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.SqlDbType = SqlDbType.VarChar;

                sqlParam = cmd.Parameters.AddWithValue("@TableName", sTableName);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.SqlDbType = SqlDbType.VarChar;

                sqlParam = cmd.Parameters.AddWithValue("@Caption", sFieldCaption.Replace("'", "''"));
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.SqlDbType = SqlDbType.VarChar;

                sqlParam = cmd.Parameters.AddWithValue("@sTableCaption", sTableCaption);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.SqlDbType = SqlDbType.VarChar;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                if (sqlParam != null)
                {
                    sqlParam = null;
                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {                
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        public void UpdateDataDictionary(Int64 nDictionaryID, string sFieldName, string sTableName, string sFieldCaption, string sTableCaption)
        {
            SqlConnection con = new SqlConnection(_DataBaseConnectionString);
            SqlCommand cmd = null;
            try
            {
                string sQuery = "UPDATE DataDictionary_MST SET sFieldName = '" + sFieldName + "', sTableName = '" + sTableName + "', sCaption = '" + sFieldCaption + "', sTableCaption = '" + sTableCaption + "' WHERE nDictionaryID = " + nDictionaryID + "";
                cmd = new SqlCommand(sQuery, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null; 
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        public void DeleteDataDictionary(string sFieldName)
        {
            SqlConnection con = new SqlConnection(_DataBaseConnectionString);
            SqlCommand cmd = null;
            try
            {
                string sQuery = "DELETE FROM DataDictionary_MST WHERE sFieldName = '" + sFieldName + "'";
                cmd = new SqlCommand(sQuery, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        


    }
}
