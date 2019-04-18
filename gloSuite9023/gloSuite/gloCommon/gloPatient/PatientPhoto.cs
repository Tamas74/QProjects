using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;

namespace gloPatient
{
    public class PatientPhoto : IDisposable
    {

        private string databaseconnectionstring = string.Empty;

        public string DatabaseConnectionString
        { get { return databaseconnectionstring; } }


        public PatientPhoto()
        {
            this.databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            
            if (this.databaseconnectionstring == null || this.databaseconnectionstring.Trim().Length <= 0)
            {
                Exception ex = new Exception("Invalid or empty database connection string.");
                ex.Source = "Namespace: gloPatient, Class: PatientPhoto, Method: public PatientPhoto()";
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }

        public PatientPhoto(string databaseconnection)
        {
            this.databaseconnectionstring = databaseconnection;
            if (this.databaseconnectionstring == null || this.databaseconnectionstring.Trim().Length <= 0)
            {
                Exception ex= new Exception("Invalid or empty database connection string.");
                ex.Source = "Namespace: gloPatient, Class: PatientPhoto, Method: public PatientPhoto(string databaseconnection)";
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
        }


        public void Dispose()
        {
            
        }


        public void InsertPhoto(Int64 patientid,byte[] iphoto)
        {
            try
            {
                InsertUpdate(patientid, iphoto);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            { }
        }

        public void UpdatePhoto(Int64 patientid, byte[] iphoto)
        {
            try
            {
                InsertUpdate(patientid, iphoto);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            { }
        }

        public void GetPhoto(Int64 patientid)
        {
            try
            {
                Retrive(patientid);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            { }
        }
        
        private bool InsertUpdate(Int64 patientid,byte[] iphoto,string fileextension="", string mimetype="",int filesize=0,int width=0,int height=0,byte[] thumbnail=null)
        {
            bool bInsertResult = false;
            string sqlQueryName = string.Empty;
            string sqlQueryResult = "";
            gloDatabaseLayer.DBLayer dbLayer = null;
            gloDatabaseLayer.DBParameters dbParams = null;
            Exception innerEx = null;

            try
            {

                if (patientid > 0)
                {
                    //TableName: [dbo].[Patient_Photo]
                    sqlQueryName = "gsp_InUpPatientPhoto";
                    //sqlQueryResult = new object();

                    dbLayer = new gloDatabaseLayer.DBLayer(this.DatabaseConnectionString);
                    dbLayer.Connect(false);

                    //Define parameters 
                    dbParams = new gloDatabaseLayer.DBParameters();
                    dbParams.Add("@nPatientID", patientid, ParameterDirection.Input, SqlDbType.BigInt);
                    dbParams.Add("@iPhoto", (iphoto == null) ? (object)DBNull.Value : iphoto, ParameterDirection.Input, SqlDbType.VarBinary); 
                    dbParams.Add("@FileExtension", fileextension, ParameterDirection.Input, SqlDbType.VarChar);
                    dbParams.Add("@MIMEType", mimetype, ParameterDirection.Input, SqlDbType.VarChar);
                    dbParams.Add("@FileSize", filesize, ParameterDirection.Input, SqlDbType.Int);
                    dbParams.Add("@Width", width, ParameterDirection.Input, SqlDbType.Int);
                    dbParams.Add("@Height", height, ParameterDirection.Input, SqlDbType.Int);
                    dbParams.Add("@Thumbnail", thumbnail, ParameterDirection.Input, SqlDbType.VarBinary);
                    dbParams.Add("@ErrorMessage", sqlQueryResult, ParameterDirection.Output, SqlDbType.VarChar,255);
                   
                    dbLayer.Execute(sqlQueryName, dbParams);

                    dbLayer.Disconnect();

                    if (Convert.ToString(sqlQueryResult).Trim().Length == 0)
                    {
                        bInsertResult = true;
                    }
                    else
                    {
                        bInsertResult = false;
                        innerEx = new Exception("Error while inserting patient photo.::" + Convert.ToString(sqlQueryResult));
                        innerEx.Source = String.Format("Class: {0}, Method: {1}, Stored Procedure: {2}.", "PatientPhoto", "Insert", sqlQueryName);
                        gloAuditTrail.gloAuditTrail.ExceptionLog(innerEx, false);
                    }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                bInsertResult = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, false);
            }
            catch (Exception ex)
            {
                bInsertResult = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (dbLayer != null) { dbLayer.Disconnect(); dbLayer.Dispose(); dbLayer = null; }
                if (dbParams != null) { dbParams.Dispose(); dbParams = null; }
                if (innerEx != null) { innerEx = null; }
                sqlQueryResult = null;
                sqlQueryName = string.Empty;
                sqlQueryName = null;
            }

            return bInsertResult;
        }

        private DataTable Retrive(Int64 patientid)
        {
            DataTable dtPhoto = null;
            string sqlQueryName = string.Empty;
            gloDatabaseLayer.DBLayer dbLayer = null;
            gloDatabaseLayer.DBParameters dbParams = null;
            try
            {
                sqlQueryName = "gsp_GetPatientPhoto";
                dtPhoto = new DataTable();
                dbLayer = new gloDatabaseLayer.DBLayer(this.DatabaseConnectionString);
                dbLayer.Connect(false);
                dbParams = new gloDatabaseLayer.DBParameters();
                dbParams.Add("@nPatientID", patientid, ParameterDirection.Input, SqlDbType.BigInt);
                dbLayer.Retrive(sqlQueryName, dbParams, out dtPhoto);
                dbLayer.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbEx, false);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                if (dbLayer != null) { dbLayer.Disconnect(); dbLayer.Dispose(); dbLayer = null; }
                if (dbParams != null) { dbParams.Dispose(); dbParams = null; }
                sqlQueryName = string.Empty;
                sqlQueryName = null;
            }
            return dtPhoto;
        }
    }
}
