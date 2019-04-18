using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace gloBilling
{
   public class clsGlobalPeriods
    {

           private Int64 _nID;
           private Int64 _nPatientID;
           private Int64 _nInsuranceID;
           private string _sCPT;
           private string _sCPTDescription;
           private DateTime _dtStartDate;
           private DateTime _dtEndDate;
           private Int32 _nDays;
           private Int64 _nProviderID;
           private string _sReminder;
           private string _sNotes;
           private DateTime _dtCreated;
           private Int64 _nUserID;
           private string _sUserName;
           private bool _bSaveForcefully;



           public Int64 ID
           { get { return _nID; } set { _nID = value; } }
           public Int64 nPatientID
           { get { return _nPatientID; } set { _nPatientID = value; } }
           public  Int64 nInsuranceID
           { get { return _nInsuranceID; } set { _nInsuranceID = value; } }  
           public  string sCPT
           { get { return _sCPT; } set { _sCPT = value; } }
           public string sCPTDescription
           { get { return _sCPTDescription; } set { _sCPTDescription = value; } }
           public  DateTime dtStartDate
           { get { return _dtStartDate; } set { _dtStartDate = value; } }
           public  DateTime dtEndDate
           { get { return _dtEndDate; } set { _dtEndDate = value; } }
           public  Int32 nDays
           { get { return _nDays; } set { _nDays = value; } }
           public  Int64 nProviderID
           { get { return _nProviderID; } set { _nProviderID = value; } }
           public  string sReminder
           { get { return _sReminder; } set { _sReminder = value; } }
           public  string sNotes
           { get { return _sNotes; } set { _sNotes = value; } }
           public  DateTime dtCreated
           { get { return _dtCreated; } set { _dtCreated = value; } }
           public  Int64 nUserID
           { get { return _nUserID; } set { _nUserID = value; } }
           public  string sUserName
           { get { return _sUserName; } set { _sUserName = value; } }
           public  bool bSaveForcefully
           { get { return _bSaveForcefully; } set { _bSaveForcefully = value; } }
     

        private bool disposed = false;

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

        ~clsGlobalPeriods()
        {
            Dispose(false);
        }

        public DataSet GetPatientDetails(Int64 nPatientID, string sCPT, Int64 nContactId)
        {
            DataSet _ds = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;

            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sCPT", sCPT, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nContactId", nContactId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("GET_Patient_GlobalPeriod_Details", oParameters, out _ds);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return _ds;
        }




        public bool SaveGlobalPeriod(Int64 nID, Int64 nPatientID, Int64 nInsuranceID, string sCPT, DateTime dtStartDate, DateTime dtEndDate,
            Int32 nDays, Int64 nProviderID, string sReminder, string sNotes, DateTime dtCreated, Int64 nUserID, string sUserName,ref string sMessage,bool bSaveForcefully)
        {
            sMessage = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            bool _Return = false;
            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nID", nID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", nPatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nInsuranceID", nInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sCPT", sCPT, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@dtEndDate", dtEndDate, ParameterDirection.Input, SqlDbType.Date);
                oParameters.Add("@nDays", nDays, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nProviderID", nProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sReminder", sReminder, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sNotes", sNotes , ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@dtCreatedDateTime", dtCreated, ParameterDirection.Input, SqlDbType.DateTime);              
                oParameters.Add("@nUserID", nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@bSaveForcefully", bSaveForcefully, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@Message", sMessage, ParameterDirection.Output, SqlDbType.VarChar,255);
                oDB.Connect(false);
                Hashtable oResult = oDB.Execute("BL_INUP_Patient_Global_Periods", oParameters,true);
                sMessage = Convert.ToString(oResult["@Message"]);

                if (sMessage == string.Empty)
                {
                    _Return = true;
                }
                else
                {
                    _Return = false;
                }

            }
            catch (Exception ex)
            {
                _Return = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return _Return;
        }

        public DataSet GetGlobalperiodDetails(Int64 nGlobalperiodID)
        {
            DataSet _ds = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;

            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nGlobalPeriodID", nGlobalperiodID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("GET_GlobalPeriod_Details", oParameters, out _ds);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return _ds;
        }

        public DataTable GetGlobalperiodChargeDetails(Int64 nGlobalperiodID)
        {
            DataTable _dt = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;

            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nGlobalPeriodID", nGlobalperiodID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("Patient_Global_Periods_Claims_Charges", oParameters, out _dt);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return _dt;
        }
        public DataTable GetClaimsOnHold(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtClaimOnHold = new DataTable();
            try
            {              
                oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);  
                oDB.Connect(false);
                oDB.Retrive("GlobalPeriod_ClaimOnHold_V2", oParameters, out dtClaimOnHold);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Dispose();
                oParameters.Dispose();
            }
            return dtClaimOnHold;
        }
        public DataTable GetGlobalPeriodsList(Int64 nPAccountId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtGlobalPeriodsList = null;
            try
            {
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nPAccountId", nPAccountId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("GET_Patient_GlobalPeriod_Lists", oParameters, out _dtGlobalPeriodsList);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _dtGlobalPeriodsList;
        }
        
        public bool DeleteGlobalPeriod(Int64 nGlobalperiodID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            bool _return = false;
            try
            {                
                oDB.Connect(false);
                int _result = oDB.Execute_Query("DELETE FROM Patient_Global_Periods WHERE nID = " + nGlobalperiodID);
                if (_result == 1)
                {
                    _return = true;
                }
                else
                {
                    _return = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _return;
        }


        public bool ValidateCPT(string CPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            bool _return = false;
            try
            {
                oDB.Connect(false);
                object _result = oDB.ExecuteScalar_Query("select count(sCPTCode) from CPT_MST where sCPTCode = '"+ CPTCode.Trim().Replace("'","''")  +"'") ;
                if (_result != null)
                {

                    if (Convert.ToInt32(_result) == 0)
                    {
                        _return = false;
                    }
                    else
                    {
                        _return = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

            return _return;
        }

        public void SaveGlobalPeriod(DataTable dtCPTDays, string InsCompanyIds, Int64 nuserid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;

            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@dtCPTDays", dtCPTDays, ParameterDirection.Input, SqlDbType.Structured);
                oParameters.Add("@InsCompanyIds", InsCompanyIds, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", nuserid, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Execute("BL_IN_CPT_Global_Periods", oParameters);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

        public void SaveGlobalPeriod_CPTLevel(DataTable dtCPTDays)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;

            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@dtCPTDays", dtCPTDays, ParameterDirection.Input, SqlDbType.Structured);              
                oDB.Connect(false);
                oDB.Execute("BL_IN_CPT_Global_Periods_CPTLevel", oParameters);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }
        public void SaveGlobalPeriod(string sCPTCode, Int32 nDays ,string InsCompanyIds, Int64 nuserid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;

            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@sCPTCode", sCPTCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nDays", nDays, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@InsCompanyIds", InsCompanyIds, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", nuserid, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Execute("BL_INUP_CPT_Global_Periods", oParameters);

            }
            catch //(Exception ex)
            {
                throw;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }

        public void SaveGlobalPeriod_Inslevel(string sCPTCode, Int32 nDays, string InsCompanyIds, Int64 nuserid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;

            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@sCPTCode", sCPTCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nDays", nDays, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@InsCompanyIds", InsCompanyIds, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nUserID", nuserid, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Execute("BL_INUP_CPT_Global_Periods_InsLevel", oParameters);

            }
            catch //(Exception ex)
            {
                throw;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }


        public void SaveGlobalPeriod_Inslevel(DataTable dtCPTDays, Int64 InsCompanyIds, Int64 nuserid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;

            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@dtCPTDays", dtCPTDays, ParameterDirection.Input, SqlDbType.Structured);
                oParameters.Add("@InsCompanyId", InsCompanyIds, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nUserID", nuserid, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Execute("BL_INUP_Global_Periods_InsLevel", oParameters);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
        }


        public void update_CPT_Description()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
           
            try
            {
                oDB.Connect(false);
                int _result = oDB.Execute_Query("UPDATE CPT_Ins_Global_Periods SET  sCPTDescription  =   sDescription From CPT_Ins_Global_Periods R1, (select  sCPTCode,sDescription from CPT_MST ) MyTable Where  R1.sCPT  = MyTable.sCPTCode AND R1.sCPTDescription IS NULL ");
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }

           
        }



    }
}
