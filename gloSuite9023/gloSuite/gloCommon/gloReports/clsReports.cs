using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gloGlobal;
using System.Data;
using System.Data.SqlClient;

namespace gloReports
{
    public static class gloReports
    {
        
        #region " Daily Close Report Methods"

        public static string getClinicName()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            string _result = string.Empty;
            try
            {
                oDB.Connect(false);
                object _Result = oDB.ExecuteScalar_Query("SELECT COALESCE(sClinicName,'') AS sClinicName FROM Clinic_MST");
                oDB.Disconnect();
                if (Convert.ToString(_Result) != "")
                { _result = _Result.ToString(); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _result;
        }

        public static Boolean SaveDailyCloseDates(string _strDayClose)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Boolean _bResult = false;
            try
            {
                object _intresult = null;
                oParameters.Add("@nCloseDate", _strDayClose, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@nUserID", gloPMGlobal.UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@sUserName", gloPMGlobal.UserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@nClinicID", gloPMGlobal.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Execute("BL_DayClose", oParameters, out _intresult);
                oDB.Disconnect();
                if (_intresult != null)
                {
                    _bResult = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return _bResult;
        }

        public static DataTable FillDayClose()
        {

            DataTable dtLastCloseDt = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            try
            {
                oDB.Connect(false);
                oDB.Retrive_Query("select dbo.CONVERT_TO_DATE (max(nCloseDayDate)) as nCloseDayDate from dbo.BL_CloseDays", out dtLastCloseDt);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtLastCloseDt;
        }

        public static DataTable FillDailyCloseDates()
        {
            DataTable dtCloseDates = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            try
            {
                //String strQuery = "";

                //strQuery = "Select  dbo.CONVERT_TO_DATE(nTransactionDate) as CloseDates from " +
                //                " ( select distinct nTransactionDate from dbo.BL_Transaction_MST " +
                //                        "Union " +
                //                    " select distinct CASE WHEN nVoidType=4 THEN dbo.CONVERT_DateAsNumber(convert(varchar,dtPaymentVoidCloseDate,101)) " +
                //                                            "ELSE  dbo.CONVERT_DateAsNumber(convert(varchar,dtCloseDate,101)) END AS nclosedate from Credits " +
                //                         "Union " +
                //                        " select distinct nVoidCloseDate from dbo.BL_EOBPaymentVoid_Notes " +
                //                         ") as Final Where nTransactionDate " +
                //                "Not In (select distinct ncloseDayDate from BL_CloseDays) and nTransactionDate <> 0 order by nTransactionDate asc";

                oDB.Connect(false);
                oDB.Retrive("BL_FILL_DailyCloseDays", out dtCloseDates);
                oDB.Disconnect();
              

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return dtCloseDates;
        }

        public static Boolean CheckForCloseDatesInBetween(DateTime dtFromDate, DateTime dtToDate, string _strDayClose)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oParameters =null;
            DataTable dtCloseDates = null;
            Boolean _bResult = false;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nFromDate", dtFromDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@nToDate", dtToDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@nCloseDate", _strDayClose, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("BL_FILL_DailyCloseDays", oParameters, out dtCloseDates);
                oDB.Disconnect();
                if (dtCloseDates != null && dtCloseDates.Rows.Count > 0)
                {
                    _bResult = true;
                }

            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
            return _bResult;
        }

        public static bool get_DailyCloseSetting()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            try
            {
                oDB.Connect(false);

                object _Result = oDB.ExecuteScalar_Query("SELECT sSettingsValue FROM Settings WHERE sSettingsName = 'Complete Payments before Daily Close'");

                oDB.Disconnect();
                if (_Result.ToString() != "")
                {
                    return Convert.ToBoolean(_Result);
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }


        public static DataTable Get_RemainingAmount_Payments(string _sCloseDate)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oParameters = null;
           
           
            DataTable _dtRemaining = null;
            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@sCloseDate", _sCloseDate, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("gsp_Get_RemainingPaymentByCloseDate_V2", oParameters, out _dtRemaining);
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
            }
            return _dtRemaining;
        }

        #endregion
    }
}
