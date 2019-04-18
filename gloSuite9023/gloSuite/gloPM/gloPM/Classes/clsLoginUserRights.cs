using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using gloGlobal;

namespace gloPM
{

    public class clsLoginUserRights
    {
        public static ArrayList GetUserRightsArrayForPatientStrip()
        {

            SqlConnection objCon = new SqlConnection();
            objCon.ConnectionString = gloPMGlobal.DatabaseConnectionString;
            SqlCommand objCmd = new SqlCommand();
            SqlDataReader objSQLDataReader = default(SqlDataReader);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "gsp_RetrieveUserRights";

            SqlParameter objParaUserName = new SqlParameter();
            //Sql parameter added by dipak 20091029 for indicate store procedure executed from EMR or PM
            SqlParameter objParaApplicationType = new SqlParameter();
            var _with1 = objParaUserName;
            _with1.ParameterName = "@UserName";
            _with1.Value = gloPMGlobal.UserName;
            _with1.Direction = ParameterDirection.Input;
            _with1.SqlDbType = SqlDbType.VarChar;
            objCmd.Parameters.Add(objParaUserName);
            //code added by dipak 20091029 to pass Sql parameter attributes value
            var _with2 = objParaApplicationType;
            _with2.ParameterName = "@ApplicationType";
            _with2.Value = 0;
            _with2.Direction = ParameterDirection.Input;
            _with2.SqlDbType = SqlDbType.Int;
            objCmd.Parameters.Add(objParaApplicationType);


            objCmd.Connection = objCon;
            objCon.Open();
            objSQLDataReader = objCmd.ExecuteReader();

            ArrayList arrLst = new ArrayList();
            while (objSQLDataReader.Read())
            {
                if (objSQLDataReader[0] != null)
                {
                    arrLst.Add(Convert.ToString(objSQLDataReader[0]).Trim());
                }
            }
            objSQLDataReader.Close();
            objCon.Close();
            objSQLDataReader = null;
            objCon.Dispose();
            objCon = null;

            if (objCmd != null)
            {
                objCmd.Parameters.Clear();
                objCmd.Dispose();
                objCmd = null;
            }
            objParaApplicationType = null;
            objParaUserName = null;
            return arrLst;
        }
    }


}
