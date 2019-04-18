using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using gloSettings;
using gloPMContacts;

namespace gloPMGeneral.gloPriorAuthorization
{
    public class clsgloPriorAuthorization
    {
        #region Variables

        Int64 _nPriorAuthorizationID;
        string _sPriorAuthorizationNo;
        Int64 _nPatientID; 
        Int64 _nReferralID;
        Int64 _nTOReferralID;
        bool _bIsTrackAuthLimit;
        Int64 _nStartDate;
        Int64 _nExpDate;
        Int64? _nVisitsAllowed; 
        Int64 _nInsuranceID;
        string _sInsuranceNote;
        int _nAuthorizationType;
        string _sAuthorizationNote;
        bool _bIsActive;
        string _sReferralName;
        string _sToReferralName;
        string _sPatientName;

        

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

        public string  PatientName
        {
            get { return _sPatientName; }
            set { _sPatientName = value; }
        }

        public string ReferralName
        {
            get { return _sReferralName; }
            set { _sReferralName = value; }
        }

        public Int64 ReferralID
        {
            get { return _nReferralID; }
            set { _nReferralID = value; }
        }
      
        public Int64 ToReferralID
        {
            get { return _nTOReferralID; }
            set { _nTOReferralID = value; }
        }

        public string ToReferralName
        {
            get { return _sToReferralName; }
            set { _sToReferralName = value; }
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

        public Int64? VisitsAllowed
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

        public bool IsActive
        {
            get { return _bIsActive; }
            set { _bIsActive = value; }
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
            object authno = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _Flag="Insert";
            string _productversion=System.Windows.Forms.Application.ProductVersion;
            try
            {
                oParameters.Clear();
                oDB.Connect(false);
                oParameters.Add("@Flag", _Flag, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@nPriorAuthorizationID", PriorAuthorizationID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                
                oParameters.Add("@sPriorAuthorizationNo", PriorAuthorizationNo , System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@nPatientID",PatientID , System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nReferralID", ReferralID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nToReferralID", ToReferralID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nInsuranceID", InsuranceID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                if (StartDate == 0)
                {
                    oParameters.Add("@nStartDate", DBNull.Value , System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                else
                {
                    oParameters.Add("@nStartDate", StartDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                if (ExpDate == 0)
                {
                    oParameters.Add("@nExpDate", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                else
                {
                    oParameters.Add("@nExpDate", ExpDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                oParameters.Add("@bIsTrackAuthLimit", IsTrackAuthLimit, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@bIsActive", IsActive, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                if (VisitsAllowed == null)
                {
                    oParameters.Add("@nVisitsAllowed", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                else
                {
                    oParameters.Add("@nVisitsAllowed", VisitsAllowed, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                oParameters.Add("@sInsuranceNote", InsuranceNote, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@nAuthorizationType", AuthorizationType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@sAuthorizationNote", AuthorizationNote, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sUserID", AppSettings.UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nClinicID", AppSettings.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                
                oParameters.Add("@sProductversion", _productversion , System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar );

                oDB.Execute("INUP_PriorAuthorization", oParameters,out authno);
                PriorAuthorizationID = Convert.ToInt64(authno);
                 //oParameters = _objPriorAuth.ToString();
                MakeAuditLog(PriorAuthorizationID,_Flag);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.Save, "Exception in prior auth save", PatientID, PriorAuthorizationID, 0, gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                oDB.Disconnect();
                oParameters.Dispose();
                oDB.Dispose();

            }
            return _result;
        }

        private void MakeAuditLog(Int64 nPriorAuthID, string sFlag)
        {
            if (nPriorAuthID != 0)
            {
                string sAuthDetails = string.Empty;
                string sTrackLimit = string.Empty;
                string sAuthStatus = string.Empty;
                string sAuditActivity = string.Empty;
                if (IsTrackAuthLimit)
                    sTrackLimit = "Yes";
                else
                    sTrackLimit = "No";

                if (IsActive)
                    sAuthStatus = "Active";
                else
                    sAuthStatus = "InActive";

                if (IsTrackAuthLimit)
                {
                    if (VisitsAllowed != null)
                    {
                        sAuthDetails = string.Format("Prior Auth: \"{0}\" Details Status: {1}; Track Auth Limit: {2}; valid through: {3} - {4}; Visit Allowed: {5}; added by user: {6} on {7}", PriorAuthorizationNo, sAuthStatus, sTrackLimit, Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(StartDate)), Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(ExpDate)), Convert.ToString(VisitsAllowed), Convert.ToString(AppSettings.UserName), DateTime.Now.ToShortDateString());
                        //Prior Auth: "name" Details Status: Active; Track Auth Limit: Yes; valid through: startdate - expdate; Visit Allowed: # visit; added by user: userID on datetime
                    }
                    else
                    {
                        sAuthDetails = string.Format("Prior Auth: \"{0}\" Details Status: {1}; Track Auth Limit: {2}; valid through: {3} - {4}; added by user: {5} on {6}", PriorAuthorizationNo, sAuthStatus, sTrackLimit, Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(StartDate)), Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(ExpDate)), Convert.ToString(AppSettings.UserName), DateTime.Now.ToShortDateString());
                        //Prior Auth: "name" Details Status: Active; Track Auth Limit: Yes; valid through: startdate - expdate; added by user: userID on datetime
                    }
                }
                else
                {
                    sAuthDetails = string.Format("Prior Auth: \"{0}\" Details Status: {1}; Track Auth Limit: {2}; added by user: {3} on {4}", PriorAuthorizationNo, sAuthStatus, sTrackLimit, Convert.ToString(AppSettings.UserName), DateTime.Now.ToShortDateString());
                    //Prior Auth: "name" Details Status: Active; Track Auth Limit: No; added by user: userID on datetime
                }
                if (sFlag == "Insert")
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.Save, sAuthDetails, PatientID, PriorAuthorizationID, 0, gloAuditTrail.ActivityOutCome.Success);
                else if (sFlag == "Update")
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.PriorAuthorization, gloAuditTrail.ActivityType.Modify, sAuthDetails, PatientID, PriorAuthorizationID, 0, gloAuditTrail.ActivityOutCome.Success);
            }
        }

        public Int64 Update()
        {
            Int64 _result = 0;
            object authno = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _Flag = "Update";
          
            try
            {
                oParameters.Clear();
                oDB.Connect(false);
                oParameters.Add("@Flag", _Flag, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@nPriorAuthorizationID", PriorAuthorizationID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);

                oParameters.Add("@sPriorAuthorizationNo", PriorAuthorizationNo, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@nPatientID", PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nReferralID", ReferralID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nToReferralID", ToReferralID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nInsuranceID", InsuranceID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                if (StartDate == 0)
                {
                    oParameters.Add("@nStartDate", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                else
                {
                    oParameters.Add("@nStartDate", StartDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                if (ExpDate == 0)
                {
                    oParameters.Add("@nExpDate", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                else
                {
                    oParameters.Add("@nExpDate", ExpDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                oParameters.Add("@bIsTrackAuthLimit", IsTrackAuthLimit, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@bIsActive", IsActive, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
               // oParameters.Add("@nVisitsAllowed", VisitsAllowed, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                if (VisitsAllowed == null)
                {
                    oParameters.Add("@nVisitsAllowed", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                else
                {
                    oParameters.Add("@nVisitsAllowed", VisitsAllowed, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                oParameters.Add("@sInsuranceNote", InsuranceNote, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@nAuthorizationType", AuthorizationType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@sAuthorizationNote", AuthorizationNote, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sUserID", AppSettings.UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nClinicID", AppSettings.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
              
                oDB.Execute("INUP_PriorAuthorization", oParameters, out authno);
                PriorAuthorizationID = Convert.ToInt64(authno);
                //oParameters = _objPriorAuth.ToString();
                MakeAuditLog(PriorAuthorizationID, _Flag);
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

        public static bool GetProviderSettingForPA(Int64 ProviderContactID, Int64 PatientID)
        {
        
                bool _isPARequired = false;

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
                string _sqlQuery = string.Empty;

                PriorAuthorizationRequired paRequired = PriorAuthorizationRequired.No;
                Object _retVal = null;

                try
                {
                    _sqlQuery = " SELECT ISNULL(nIsPARequired,1) AS nIsPARequired FROM Provider_MST WITH(NOLOCK) WHERE nProviderID = " + ProviderContactID;

                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) >= 0)
                    { paRequired = (PriorAuthorizationRequired)Convert.ToInt16(_retVal); }

                    if (paRequired.Equals(PriorAuthorizationRequired.No))
                    { _isPARequired = false; }
                    else if (paRequired.Equals(PriorAuthorizationRequired.Always))
                    { _isPARequired = true; }
                    else if (paRequired.Equals(PriorAuthorizationRequired.UsePlanSetting))
                    { _isPARequired = GetPlanSettingForPA(PatientID); }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                    if (_retVal != null) { _retVal = null; }
                }

            return _isPARequired;
        }

        public static bool GetPlanSettingForPA(Int64 PatientID)
        {
            bool _isPARequired = false; 

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            DataTable _dtpatientInsurances = new DataTable();
            try
            {
                //_sqlQuery = " SELECT CASE WHEN COUNT(P.nContactID)>0 THEN 1 ELSE 0 END AS bIsPARequired " +
                //            " FROM PatientInsurance_DTL AS P INNER JOIN Contacts_Insurance_DTL AS C ON P.nContactID = C.nContactID " +
                //            " WHERE P.nPatientID=" + PatientID + " and P.nInsuranceFlag = 1 AND bIsPARequired = 1 ";

                //oDB.Connect(false);
                //_retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                //oDB.Disconnect();

                //if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) >= 0)
                //{ _isPARequired = Convert.ToBoolean(_retVal); }

                string _sqlQuery =  " SELECT P.nContactID, P.nInsuranceID,P.sInsuranceName, P.nInsuranceFlag, C.bIsPARequired " +
                                    " FROM PatientInsurance_DTL AS P WITH(NOLOCK) INNER JOIN Contacts_Insurance_DTL AS C WITH(NOLOCK) ON P.nContactID = C.nContactID  " +
                                    " WHERE P.nInsuranceFlag = 1 AND P.nPatientID=" + PatientID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtpatientInsurances);
                oDB.Disconnect();

                if (_dtpatientInsurances != null && _dtpatientInsurances.Rows.Count > 0)
                {
                    foreach (DataRow dr in _dtpatientInsurances.Rows)
                    {
                        if (Convert.ToBoolean(dr["bIsPARequired"])==true)
                        { _isPARequired = true; break; }
                    }
                }
                else
                { _isPARequired = true; }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtpatientInsurances != null) { _dtpatientInsurances.Dispose(); }
            }
            return _isPARequired;
        }
        public static DataTable GetAllActiveInsurancePlan(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            DataTable _dtpatientInsurances = new DataTable();
            try
            {
                string _sqlQuery = " SELECT P.nContactID, P.nInsuranceID,P.sInsuranceName, P.nInsuranceFlag, C.bIsPARequired " +
                                    " FROM PatientInsurance_DTL AS P WITH(NOLOCK) INNER JOIN Contacts_Insurance_DTL AS C WITH(NOLOCK) ON P.nContactID = C.nContactID  " +
                                    " WHERE P.nInsuranceFlag <> 0 AND P.nPatientID=" + PatientID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtpatientInsurances);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtpatientInsurances != null) { _dtpatientInsurances.Dispose(); }
            }
            return _dtpatientInsurances;
        }
        public static bool HasPriorAuthorization(Int64 PatientID)
        {
            bool _isPAExist = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = string.Empty;

            Object _retVal = null;
            try
            {
                _sqlQuery = "SELECT count(*) FROM  PriorAuthorization_Mst WITH(NOLOCK) WHERE nPatientID='" + PatientID + "' AND bIsActive=1";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) >= 0)
                { _isPAExist = Convert.ToBoolean(_retVal); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _isPAExist;
        }

        public static int GetVisitsRemaining(Int64 AuthorizationID, Int64 nTillDate, bool IncludeCurrentVisit)
        {
            int _visitsRemaining = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = string.Empty;

            Object _retVal = null;
            try
            {
                _sqlQuery = " SELECT dbo.GET_AuthVisits_Revised('" + nTillDate + "','" + AuthorizationID + "',1) AS VisitsRemaining ";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                {
                    _visitsRemaining = Convert.ToInt32(_retVal);

                    if (IncludeCurrentVisit)
                    {  _visitsRemaining = _visitsRemaining - 1; }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _visitsRemaining;
        }

        public static int GetVisitsRemainingCutAppt(Int64 AuthorizationID, Int64 nTillDate, Int64 nMstApptID, Int64 nDtlApptID, bool IncludeCurrentVisit)
        {
            int _visitsRemaining = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = string.Empty;

            Object _retVal = null;
            try
            {
                _sqlQuery = " SELECT dbo.GET_AuthVisits_Revised_CutAppt('" + nTillDate + "','" + AuthorizationID + "','" + nMstApptID + "','" + nDtlApptID + "',1) AS VisitsRemaining ";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                {
                    _visitsRemaining = Convert.ToInt32(_retVal);

                    if (IncludeCurrentVisit)
                    { _visitsRemaining = _visitsRemaining - 1; }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _visitsRemaining;
        }

        public static int GetVisitsUsed(Int64 AuthorizationID,Int64 FromDate, Int64 ToDate)
        {
            int _visitsUsed = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = string.Empty;

            Object _retVal = null;
            try
            {
                _sqlQuery = " SELECT dbo.GET_VisitsUsed_Revised('" + FromDate + "','" + ToDate + "','" + AuthorizationID + "') AS VisitsUsed ";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) >= 0)
                {
                    _visitsUsed = Convert.ToInt32(_retVal);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _visitsUsed;
        }

        public static int GetVisitsUsed(Int64 AuthorizationID)
        {
            int _visitsUsed = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = string.Empty;

            Object _retVal = null;
            try
            {
                _sqlQuery = " SELECT dbo.GET_AuthVisits_ByID_Revised('" + AuthorizationID + "') AS VisitsUsed ";

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) >= 0)
                {
                    _visitsUsed = Convert.ToInt32(_retVal);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _visitsUsed;
        }

        public static int GetVisitsUsedByAppointment(Int64 MasterAppointmentID, Int64 DetailAppointmentID, Int64 AuthorizationID)
        {
            int _visitsUsed = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = string.Empty;

            Object _retVal = null;
            try
            {
                if (DetailAppointmentID.Equals(0))
                {
                    _sqlQuery = " SELECT COUNT(nPATransactionID) AS VisitsUsed  FROM PatientPriorAuthorization_Transaction WITH(NOLOCK) " +
                                " WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAuthorizationID =" + AuthorizationID;
                }
                else
                {
                    _sqlQuery = " SELECT COUNT(nPATransactionID) AS VisitsUsed  FROM PatientPriorAuthorization_Transaction WITH(NOLOCK) " +
                                " WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nDTLAppointmentID = " + DetailAppointmentID + " AND nAuthorizationID =" + AuthorizationID;
                }

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) >= 0)
                {
                    _visitsUsed = Convert.ToInt32(_retVal);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _visitsUsed;
        }

        public static DataTable GetUniqueDates(Int64 AuthorizationID,Int64 AsOfDate)
        {
         //   int _visitsUsed = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = string.Empty;
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtDates = new DataTable();
            Object _retVal = null;
            try
            {
                oParameters.Add("@nPriorAuthorizationID", AuthorizationID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@AsOfDate", AsOfDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nClinicID", gloSettings.AppSettings.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                
                oDB.Connect(false);
                oDB.Retrive("GET_PriorAuthorization_Dates",oParameters, out _dtDates);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }
            return _dtDates;
        }

        public static DataTable GetUniqueDatesVoidClaim(Int64 AuthorizationID, Int64 AsOfDate,Int64 nTransactionMasterID)
        {
          //  int _visitsUsed = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = string.Empty;
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtDates = new DataTable();
            Object _retVal = null;
            try
            {
                oParameters.Add("@nPriorAuthorizationID", AuthorizationID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@AsOfDate", AsOfDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nClinicID", gloSettings.AppSettings.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@nTransactionMstID", nTransactionMasterID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                oDB.Connect(false);
                oDB.Retrive("GET_PriorAuthorization_Dates_Void", oParameters, out _dtDates);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }
            return _dtDates;
        }

        public static DataRow GetPriorAuthorizationInfo(Int64 AuthorizationID)
        {
            DataRow _drPAInfo = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            DataTable _dtPA = new DataTable();

            try
            {
                string _sqlQuery = " SELECT nPriorAuthorizationID,sPriorAuthorizationNo,sReferralName,sInsuranceName,sAuthorizationNote, "
                                 + " nReferralID,bIsTrackAuthLimit,nStartDate,nExpDate,nVisitsAllowed,bIsActive,PatientName,InsuranceID,nAuthorizationType "
                                 + " FROM view_PriorAuthorizations WITH(NOLOCK) WHERE nPriorAuthorizationID = " + AuthorizationID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtPA);
                oDB.Disconnect();

                if (_dtPA != null && _dtPA.Rows.Count > 0)
                {
                    _drPAInfo = _dtPA.Rows[0];
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtPA != null) { _dtPA.Dispose(); }
            }
            return _drPAInfo;
        }

        public static DataRow GetPriorAuthorizationInfo(Int64 MasterAppointmentID, Int64 DetailAppointmentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);

            DataTable _dtPAInfo = new DataTable();
            DataRow _drPAInfo = null;

            try
            {
                string _sqlQuery = " SELECT view_PriorAuthorizations.*,nMSTAppointmentID,nDTLAppointmentID,nPATransactionID  FROM view_PriorAuthorizations " +
                                    " INNER JOIN PatientPriorAuthorization_Transaction WITH(NOLOCK) " +
                                    " ON view_PriorAuthorizations.nPriorAuthorizationID = PatientPriorAuthorization_Transaction.nAuthorizationID " +
                                    " WHERE nMSTAppointmentID = " + MasterAppointmentID + " and nDTLAppointmentID = " + DetailAppointmentID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out _dtPAInfo);
                oDB.Disconnect();

                if (_dtPAInfo != null && _dtPAInfo.Rows.Count > 0)
                {
                    _drPAInfo = _dtPAInfo.Rows[0];
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_dtPAInfo != null) { _dtPAInfo.Dispose(); }
            }
            return _drPAInfo;
        }

    }
}
