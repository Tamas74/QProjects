using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using gloGlobal;

namespace gloGallery
{
    public static class clsGallery //: IDisposable
    {
        public enum GalleryType
        { 
            ICD10=10,
            ICD9=9,
            CPT=0
        }

        public static DataTable GetGallery(string connectionString, GalleryType galleryType, bool IsUnusedFilter = false, bool mappingFilter = false, string indicator = "")
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(connectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtGallery = new DataTable();

            try
            {               
                oParameters.Add("@ICDRevision", galleryType.GetHashCode(), ParameterDirection.Input, SqlDbType.SmallInt);
                oParameters.Add("@ShowMappedOnly", mappingFilter, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@ShowUnusedOnly", IsUnusedFilter, ParameterDirection.Input, SqlDbType.Bit);
                if (indicator == "NC")
                {
                    oParameters.Add("@indicatorFilter", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar);
                }
                else
                {
                    oParameters.Add("@indicatorFilter", indicator, ParameterDirection.Input, SqlDbType.VarChar);
                }

                oDB.Connect(false);
                oDB.Retrive("gsp_GetGalleryCodes", oParameters, out dtGallery);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return dtGallery;
        }

        public static DataTable GetDistinctGalleryDates(string _databaseConnectionString)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = new DataTable();
            try
            {
                oDB.Connect(false);
                oDB.Retrive("gsp_GetDistinctGalleryActivationDates", out dt);
                oDB.Disconnect();

                DataRow drAll = null;
                drAll = dt.NewRow();
                drAll["dtActivationDate"] = "All";
                dt.Rows.Add(drAll);

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return dt;
        }

        public static DataTable GetCPTGallery(string connectionString, string ActivationDateTime)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(connectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtGallery = new DataTable();

            try
            {
                oParameters.Add("@sActivationDateTime", ActivationDateTime, ParameterDirection.Input, SqlDbType.VarChar);
                
                oDB.Connect(false);
                oDB.Retrive("gsp_GetGalleryCodesCPT", oParameters, out dtGallery);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return dtGallery;
        }

        public static DataTable GetMaster(string connectionString, GalleryType galleryType, Int64 SpecialityId, Int64 CategoryId=0)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(connectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtMaster = new DataTable();

            try
            {
                oParameters.Add("@nGalleryType", galleryType.GetHashCode(), ParameterDirection.Input, SqlDbType.SmallInt);
                oParameters.Add("@nSpecialtyID", SpecialityId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCategoryID", CategoryId, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                oDB.Retrive("gsp_GetCurrentCodes", oParameters, out dtMaster);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return dtMaster;
        }

        //TODO : Use Scaler Value 
        //public static bool IsExistICD(string connectionString, GalleryType galleryType, Int64 ID, string ICDCode, long ClinicID)
        //{
        //    bool _result = false;
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //    gloDatabaseLayer.DBParameters oParameters = null;
        //    Int32 output;
        //    try
        //    {
        //        oParameters = new gloDatabaseLayer.DBParameters();
        //        oParameters.Add("@nICDID", ID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@sICDCode", ICDCode, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@nICDRevision", galleryType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //        oParameters.Add("@ClinicId", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDB.Connect(false);
        //        output = (Int32)oDB.ExecuteScalar("gsp_IsExistsICD", oParameters);
        //        if (output>0)
        //        {
        //            _result = true;
        //        }
          

        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //        throw dbEx;
        //    }
        //    finally
        //    {
        //        if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); }
        //        if (oDB != null) { oDB.Dispose(); }
        //    }
        //    return _result;
        //}

        //public static void SaveICDCode(string connectionString, GalleryType galleryType, long ID, string ICD9Code, string Description, long SpecialtyID, long ClinicID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(connectionString);
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //    try
        //    {
        //        oDB.Connect(false);
        //        oParameters.Add("@ICD9ID", ID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@ICD9Code", ICD9Code, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@Description", Description, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@SpecialtyID", SpecialtyID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@Inactive", false, ParameterDirection.Input, SqlDbType.Bit);
        //        oParameters.Add("@nICDRevision", galleryType.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.SmallInt);
        //        oDB.Execute("gsp_InUpICD9", oParameters);
        //        oDB.Disconnect();
        //        if (ID != 0)
        //        {
        //            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Modify, "ICD9 Modified", gloAuditTrail.ActivityOutCome.Success);
        //        }
        //        else
        //        {
        //            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Added", gloAuditTrail.ActivityOutCome.Success);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //        if (oParameters != null) { oParameters.Dispose(); }
        //    }
        //}

        //public static Boolean DeleteCode(Int64 ID,GalleryType galllerytype)
        //{
        //    bool _result = false;
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //    gloDatabaseLayer.DBParameters oParameters = null;
        //    try
        //    {

        //        oParameters = new gloDatabaseLayer.DBParameters();
        //        oParameters.Add("@nCodeID", ID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@nGalleryType", galllerytype.GetHashCode(), ParameterDirection.Input, SqlDbType.SmallInt);
        //        oDB.Connect(false);
        //        oDB.Execute("gsp_DeleteCodes", oParameters);
        //        _result = true;
               
           

        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //        throw dbEx;
        //    }
        //    finally
        //    {
        //        if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); }
        //        if (oDB != null) { oDB.Dispose(); }
        //    }
        //    return _result;
        //}

        //public static bool IsCodeInUse(string ICDCode, GalleryType galleryType)
        //{
        //    bool _result = false;
        //    object _error = null;
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //    gloDatabaseLayer.DBParameters oParameters = null;
        //    int Cnt = 0;
        //    try
        //    {
        //        oParameters = new gloDatabaseLayer.DBParameters();
        //        oParameters.Add("@sCode", ICDCode, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@nGalleryType", galleryType.GetHashCode(), ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@ICDCount", Cnt, ParameterDirection.Output, SqlDbType.Int);
        //        oDB.Connect(false);
        //        oDB.Execute("gsp_IsCodeUsed", oParameters, out _error);

        //        if (_error!=null)
        //        {
        //            if (_error.ToString() != "")
        //            {
        //                _result = true;
        //            }
        //        }


        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //        throw dbEx;
        //    }
        //    finally
        //    {
        //        if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); }
        //        if (oDB != null) { oDB.Dispose(); }
        //    }
        //    return _result;
        //}

        public static DataTable GetAllCategory(string _databaseConnectionString)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oParameters.Add("@CategoryType", "CPT", ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("gsp_FillCategory_MST", oParameters, out dt);

                DataRow drAll = null;
                drAll = dt.NewRow();
                drAll["nCategoryID"] = 0;
                drAll["sDescription"] = "All";
                dt.Rows.Add(drAll);

                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return dt;
        }

        //public static DataTable GetAllSpeciality(string _databaseConnectionString)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        oDB.Connect(false);
        //        oDB.Retrive("gsp_FillSpecialty_MST", out dt);

        //        DataRow drAll = null;
        //        drAll = dt.NewRow();
        //        drAll["nSpecialtyId"] = 0;
        //        drAll["sDescription"] = "All";
        //        dt.Rows.Add(drAll);

        //        oDB.Disconnect();
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //        throw dbEx;
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //    }
        //    return dt;
        //}

              
    }
}
