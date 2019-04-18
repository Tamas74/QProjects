using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using gloSettings;

namespace gloBilling.gloPriorAuthorization
{
    public class clsgloPriorAuthorization
    {
        #region Variables

        Int64 _nPriorAuthorizationID;
        string _sPriorAuthorizationNo;
        Int64 _nPatientID; 
        Int64 _nReferralID; 
        bool _bIsTrackAuthLimit;
        Int64 _nStartDate;
        Int64 _nExpDate;
        int _nVisitsAllowed; 
        Int64 _nInsuranceID;
        string _sInsuranceNote;
        int _nAuthorizationType;
        string _sAuthorizationNote;
        bool _bIsInActive;



        //Int64 _ClinicID;  // Use AppSettings.ClinicID 
        //string _messageBoxCaption  // Use AppSettings.MessageBoxCaption;
        //string DatabaseConnectionString; // Use AppSettings.ConnectionStringPM;
        //Int64 _nUserID; // Use AppSettings.UserID;
        
        #endregion

        #region Property and Procedures

        public Int64 PriorAuthorizationID
        {
            get { return _nPriorAuthorizationID; }
            set { _nPriorAuthorizationID = value; }
        }

        public string PriorAuthorizationNo
        {
            get { return _sPriorAuthorizationNo; }
            set { _sPriorAuthorizationNo = value; }
        }

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public Int64 ReferralID
        {
            get { return _nReferralID; }
            set { _nReferralID = value; }
        }

 

        public bool IsTrackAuthLimit
        {
            get { return _bIsTrackAuthLimit; }
            set { _bIsTrackAuthLimit = value; }
        }

        public Int64 StartDate
        {
            get { return _nStartDate; }
            set { _nStartDate = value; }
        }

        public Int64 ExpDate
        {
            get { return _nExpDate; }
            set { _nExpDate = value; }
        }

        public int VisitsAllowed
        {
            get { return _nVisitsAllowed; }
            set { _nVisitsAllowed = value; }
        }

        public Int64 InsuranceID
        {
            get { return _nInsuranceID; }
            set { _nInsuranceID = value; }
        }

    

        public string InsuranceNote
        {
            get { return _sInsuranceNote; }
            set { _sInsuranceNote = value; }
        }

        public int AuthorizationType
        {
            get { return _nAuthorizationType; }
            set { _nAuthorizationType = value; }
        }

        public string AuthorizationNote
        {
            get { return _sAuthorizationNote; }
            set { _sAuthorizationNote = value; }
        }

        public bool IsInActive
        {
            get { return _bIsInActive; }
            set { _bIsInActive = value; }
        }

        #endregion

        #region "Constructor & Distructor"

        public clsgloPriorAuthorization()
        {

        }

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

        ~clsgloPriorAuthorization()
        {
            Dispose(false);
        }

        #endregion


        public enum AuthorizationTypeEnum
        {           
            ReferralIn = 1,
            ReferralOut = 2,
            Both=3
            
        }

        public Int64 Add()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _Flag="Insert";
   
            try
            {
                oParameters.Clear();
                oDB.Connect(false);
                oParameters.Add("@Flag", _Flag, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sPriorAuthorizationNo", PriorAuthorizationNo , System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@nPatientID",PatientID , System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nReferralID", ReferralID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nInsuranceID", InsuranceID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nStartDate", StartDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nExpDate", ExpDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@bIsTrackAuthLimit", IsTrackAuthLimit, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@bIsInActive", IsInActive, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@nVisitsAllowed", VisitsAllowed, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@sInsuranceNote", InsuranceNote, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@nAuthorizationType", AuthorizationType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@sAuthorizationNote", AuthorizationNote, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sUserID", AppSettings.UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nClinicID", AppSettings.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                oDB.Execute("INUP_PriorAuthorization", oParameters);
                 //oParameters = _objPriorAuth.ToString();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Disconnect();
                oParameters.Dispose();
                oDB.Dispose();

            }
            return _result;
        }

        public bool Delete(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "";
                int result = oDB.Execute_Query(strQuery);
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return false;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }
    }
}
