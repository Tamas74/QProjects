using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

using gloSettings;
using gloPatientStripControl;
using gloBilling.EOBPayment.Common;

namespace gloBilling.gloERA
{

    public class ClsERALogs
    {

          #region "Variable Declaration"
           private gloDatabaseLayer.DBLayer oDB;
           private gloDatabaseLayer.DBParameters oDBPara;
           string _DataBaseConnectionString = "";
           Int64 _ClinicID = 0;
           Int64 _UserID = 0;
          #endregion

          #region "Constructor & Distructor"


           private bool disposed = false;

            public ClsERALogs()
           {
               #region " Clinic ID ,UserID,Databaseconnection Stringfrom gloGlobal "
               _DataBaseConnectionString = Convert.ToString(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                  _ClinicID = Convert.ToInt64(gloGlobal.gloPMGlobal.ClinicID);
                  _UserID = Convert.ToInt64(gloGlobal.gloPMGlobal.UserID);
                #endregion

            }
           
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
          
            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {

                    }
                }
                disposed = true;
            }
            ~ClsERALogs()
            {
                Dispose(false);
            }

            #endregion

          #region "Open And Close Connection"
          
            private bool OpenConnection(bool withParameters)
            {
                bool _Result = false;
                try
                {
                    if (_DataBaseConnectionString != "")
                    {
                        oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                        oDB.Connect(false);
                        if (withParameters)
                            oDBPara = new gloDatabaseLayer.DBParameters();
                        _Result = true;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                return _Result;
            }

            private void CloseConnection()
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (oDBPara != null)
                {
                    oDBPara.Dispose();
                    oDBPara = null;
                }
            }

            
            #endregion
        
          #region " Private & Public Methods "

            public static bool ClaimStatusLog(Int64 nBPRID, Int64 nCLPID, string ERAClaimNo, Int64 nSVCId, String sFlagLegend, String sComments, LogStage oLogStage)
            {
                //SP: ERA_IN_PostingLogs; 
                //@nBPRID			numeric(18, 0),
                //@nCLPID			numeric(18, 0),
                //@sERAClaimNo	varchar(50),
                //@nSVCId			numeric(18, 0),
                //@sFlagLegend	varchar(5),
                //@sComments		varchar(1000),
                //@LogStage		int

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                string _sqlQuery = "";

                try
                {
                    _sqlQuery = "ERA_IN_PostingLogs";

                    oParameters.Clear();
                    oParameters.Add("@nBPRId", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nCLPID", nCLPID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sERAClaimNo", ERAClaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nSVCId", nSVCId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sFlagLegend", sFlagLegend, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sComments", sComments, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@LogStage", oLogStage.GetHashCode() , ParameterDirection.Input, SqlDbType.Int);

                    oDB.Connect(false);
                
                    oDB.Execute(_sqlQuery, oParameters);
                    return true;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    if (oDB != null) { oDB.Dispose(); }
                    if (oParameters != null) { oParameters.Dispose(); }
                }
                

            }

            public static bool SVCExceptionLog(Int64 nBPRID, Int64 nCLPID, string ERAClaimNo, Int64 nSVCId, String sFlagLegend, String sComments, LogStage oLogStage)
            {
                //SP: ERA_SVCPostingLogs 
                //@nBPRID			numeric(18, 0),
                //@nCLPID			numeric(18, 0),
                //@sERAClaimNo	varchar(50),
                //@nSVCId			numeric(18, 0),
                //@sFlagLegend	varchar(5),
                //@sComments		varchar(1000),
                //@LogStage		int

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                string _sqlQuery = "";

                try
                {
                    _sqlQuery = "ERA_SVCPostingLogs";

                    oParameters.Clear();
                    oParameters.Add("@nBPRId", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nCLPID", nCLPID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sERAClaimNo", ERAClaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@nSVCId", nSVCId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sFlagLegend", sFlagLegend, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@sComments", sComments, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@LogStage", oLogStage.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);

                    oDB.Connect(false);

                    oDB.Execute(_sqlQuery, oParameters);
                    return true;
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                    return false;
                }
                finally
                {
                    oDB.Disconnect();
                    if (oDB != null) { oDB.Dispose(); }
                    if (oParameters != null) { oParameters.Dispose(); }
                }
                
            }

            public DataTable GetERAFiles()
            {
                DataTable dtFiles = null;
                try
                {
                    if (OpenConnection(true))
                    {
                        oDBPara.Clear();
                        oDBPara.Add("@UserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBPara.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Retrive("ERA_GetERAFiles", oDBPara, out dtFiles);
                        CloseConnection();
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                return dtFiles;
            }

            public void DeleteERAFile(Int64 nFileID)
            {
                try
                {
                     gloERA oERA;

                    if (OpenConnection(false))
                    {
                       String  _TempStr = "DELETE FROM ERA_FILES WHERE nERAFileID = " + nFileID;
                        oDB.Execute_Query(_TempStr);
                        CloseConnection();

                        oERA = new gloERA();
                        oERA.DeleteParsedData(nFileID);

                        if (oERA != null)
                        {
                            oERA.Dispose();
                            oERA = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
            }

            public bool IsPostedCheckPresent(Int64 nFileID)
            {
                bool _Result = false;
                Object oResult = null;
                try
                {
                    if (OpenConnection(false))
                    {
                        String _TempStr = "SELECT COUNT(nBPRID) FROM ERA_BPR WHERE nCheckStatus = 2 AND nERAFileID = " + nFileID;
                        oResult = oDB.ExecuteScalar_Query(_TempStr);
                        if (oResult != null && oResult.ToString() != "")
                            if (Convert.ToInt32(oResult) > 0)
                                _Result = true;
                        CloseConnection();
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                return _Result;
            }
                        
            public string GetServerPath()
            {

                Object retVal = null;
                string _isValidPath = "";

                try
                {
                    if (OpenConnection(false))
                    {
                       string  _TempStr = "SELECT sSettingsValue FROM Settings WHERE UPPER(sSettingsName) = 'SERVERPATH'";
                        retVal = oDB.ExecuteScalar_Query(_TempStr);
                        CloseConnection();

                        if (retVal != null && retVal != DBNull.Value)
                        {
                            _isValidPath = Convert.ToString(retVal);
                            if (System.IO.Directory.Exists(_isValidPath) == false)
                            { _isValidPath = ""; }
                        }
                        else
                        { _isValidPath = ""; }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                }
                finally
                {
                    retVal = null;
                }
                return _isValidPath;
            }


   
            #endregion " Private & Public Methods "



    }
}
