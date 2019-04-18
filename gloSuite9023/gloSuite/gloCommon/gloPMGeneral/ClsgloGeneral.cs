using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace gloPMGeneral
{

    public class PriorAutorization : IDisposable
    {
        #region "Constructor & Distructor"
        private string _databaseconnectionstring = "";

        public PriorAutorization(String DatabaseConnectionstring)
        {
            _databaseconnectionstring = DatabaseConnectionstring;
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

        ~PriorAutorization()
        {
            Dispose(false);
        }

        #endregion

        #region "Private Variables"
        //Patient First Name, 
        private string _PatientFirstName = "";
        //Patient Middle Name, 
        private string _PatientMiddleName = "";
        //Patient Last Name, 
        private string _PatientLastName = "";
        //nPatientID, 
        private Int64 _PatientID = 0;
        //nInsuranceID,
        private Int64 _InsuranceID = 0;
        //Insurance Name,
        private string _InsuranceName = "";
        //
        private Boolean _IsNotAuthorizationDate = false;
        private DateTime _AuthorizationDate;
        //
        private Boolean _IsNotAuthorizationThroughDate = false;
        private DateTime _AuthorizationThroughDate;
        //
        private String _AuthorizationStatus;
        //
        private Boolean _IsNotAuthorizationStatusDate = false;
        private DateTime _AuthorizationStatusDate;
        // 
        private String _AuthorizationNumber;
        // 
        private Int64 _TotalVisits;
        // 
        private Int64 _VisitsMade;
        //
        private Int64 _AppointMentType;

        private Int64 _AuthorizationID = 0;
        
        #endregion

        #region "Property Procedures"
        //nInsuranceID,
        public Int64 InsuranceID
        { get { return _InsuranceID; } set { _InsuranceID = value; } }
        //
        public string InsuranceName
        { get { return _InsuranceName; } set { _InsuranceName = value; } }
        // 
        //nPatientID, 
        public Int64 PatientID
        { get { return _PatientID; } set { _PatientID = value; } }

        public String PatientFirstName
        { get { return _PatientFirstName; } set { _PatientFirstName = value; } }

        public String PatientMiddleName
        { get { return _PatientMiddleName; } set { _PatientMiddleName = value; } }

        public String PatientLastName
        { get { return _PatientLastName; } set { _PatientLastName = value; } }
        //, 
        public Boolean IsNotAuthorizationDate
        { get { return _IsNotAuthorizationDate; } set { _IsNotAuthorizationDate = value; } }
        //, 
        public DateTime AuthorizationDate
        { get { return _AuthorizationDate; } set { _AuthorizationDate = value; } }
        //
        public DateTime AuthorizationThroughDate
        { get { return _AuthorizationThroughDate; } set { _AuthorizationThroughDate = value; } }
        //, 
        public Boolean IsNotAuthorizationThroughDate
        { get { return _IsNotAuthorizationThroughDate; } set { _IsNotAuthorizationThroughDate = value; } }

        public String AuthorizationStatus
        { get { return _AuthorizationStatus; } set { _AuthorizationStatus = value; } }
        //
        public Boolean IsNotAuthorizationStatusDate
        { get { return _IsNotAuthorizationStatusDate; } set { _IsNotAuthorizationStatusDate = value; } }

        public DateTime AuthorizationStatusDate
        { get { return _AuthorizationStatusDate; } set { _AuthorizationStatusDate = value; } }

        public String AuthorizationNumber
        { get { return _AuthorizationNumber; } set { _AuthorizationNumber = value; } }

        public Int64 VisitsMade
        { get { return _VisitsMade; } set { _VisitsMade = value; } }

        public Int64 TotalVisits
        { get { return _TotalVisits; } set { _TotalVisits = value; } }
        public Int64 AppointMentType
        { get { return _AppointMentType; } set { _AppointMentType = value; } }

        public Int64 AuthorizationID
        { get { return _AuthorizationID; } set { _AuthorizationID = value; } }
       
        #endregion
        


        # region " Public Methods "

        /// <summary>
        /// To get the Authorization Information for Patient's Insurance
        /// </summary>
        /// <param name="PatientID"></param>
        /// <param name="InsuranceID"></param>
        /// <param name="InsuranceName"></param>
        /// <returns></returns>
        public DataTable GetPriorAuthorization(Int64 PatientID, Int64 InsuranceID, String InsuranceName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            DataTable dt_PriorAuthorization = null;
            try
            {

                oParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                // oParameters.Add("@PatientID", InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sInsuranceName", InsuranceName, ParameterDirection.Input, SqlDbType.VarChar);

                oDB.Retrive("PA_SELECT_PatientPriorAuthorization", oParameters, out dt_PriorAuthorization);

                if (dt_PriorAuthorization != null)
                {
                    return dt_PriorAuthorization;

                }
                return null;


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                
                dbEx.ERROR_Log(dbEx.ToString());      
                return null;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null; 
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }

        }

        /// <summary>
        /// To Add/ Update the Prior Authorization for the Patient Insurance
        /// </summary>
        /// <param name="oPriorAutorization"> Object of PriorAutorization </param>
        public Boolean AddModifyPriorAuthorization(PriorAutorization oPriorAutorization)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            object _result = null;
            try
            {
                oDB.Connect(false);

                oParameters.Add("@nAuthorizationID", oPriorAutorization.AuthorizationID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@nPatientID", oPriorAutorization.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nInsuranceID", oPriorAutorization.InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sInsuranceName", oPriorAutorization.InsuranceName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@dtAuthorization", oPriorAutorization.AuthorizationDate, ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@IsNOTAuthorizationDate", 0, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@dtAuthorizationThrough", oPriorAutorization.AuthorizationThroughDate, ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@IsNOTAuthorizationThroughDate", 0, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@dtAuthorizationStatus", oPriorAutorization.AuthorizationStatusDate, ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@IsNOTAuthorizationStatusDate", 0, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@sAuthorizationStatus", oPriorAutorization.AuthorizationStatus, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sAuthorizationNo", oPriorAutorization.AuthorizationNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nNumberOfVisits", oPriorAutorization.TotalVisits, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nVisitsMade", oPriorAutorization.VisitsMade, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nAppointmentType", oPriorAutorization.AppointMentType, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("PA_INUP_PatientPriorAuthorization", oParameters, out _result);

                if (_result != null)
                {
                    return true;
                }

                oDB.Disconnect();
                return false;
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally 
            {
                if(oDB!=null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }

        }

        public DataTable ViewPriorAuthorization(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt_PriorAuthorization = null;
            try
            {
                string Sqlquery = "SELECT   ISNULL(nAuthorizationID,0)AS nAuthorizationID, ISNULL(nPatientID,0) AS  nPatientID, " +
                "ISNULL(nInsuranceID,0) AS nInsuranceID,ISNULL(sInsuranceName,'') AS sInsuranceName, " +
                "dtAuthorization, dtAuthorizationThrough, ISNULL(sAuthorizationNumber,'') AS sAuthorizationNumber,  " +
                "ISNULL(nTotalVisits,0) AS nTotalVisits,  ISNULL(nVisitsMade,0) AS nVisitsMade, ISNULL(nAppointmentType,0) AS nAppointmentType,  " +
                "ISNULL(sAuthorizationStatus,'') AS sAuthorizationStatus,  dtAuthorizationStatus " +
                "FROM PatientPriorAuthorization WHERE nPatientID= " + PatientID + "  ORDER BY nAuthorizationID ";

                oDB.Retrive_Query(Sqlquery, out dt_PriorAuthorization);
                if (dt_PriorAuthorization != null)
                {
                    return dt_PriorAuthorization;

                }
                return null;


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());  
                return null; 

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null; 

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

        }

        public bool Delete_PriorAuthorization(Int64 AuthorizationID)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ODB.Connect(false);
            string sqlquery = "DELETE  FROM PatientPriorAuthorization WHERE  nAuthorizationID= " + AuthorizationID + "  ";
            int _result = 0;
            try
            {
                _result = ODB.Execute_Query(sqlquery);
                if (_result > 0)
                    return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally 
            {
                ODB.Disconnect();
                ODB.Dispose();
            }
            return false;

        }


        #endregion
    }

    public class PatientStatus : IDisposable
    {

        #region "Constructor & Distructor"
        private string _databaseconnectionstring = "";
        //private string _messageBoxCaption = "gloPMS";
        private string _messageBoxCaption = String.Empty;

        private Int64 _ClinicID = 0;
        //public bool _IsChekinappointment = false;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public PatientStatus(String DatabaseConnectionstring)
        {
            _databaseconnectionstring = DatabaseConnectionstring;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }


            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }
                        
            //Added for getting User id for Bug #74828: 00000783: Appointment History
            
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }
            #endregion
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

        ~PatientStatus()
        {
            Dispose(false);
        }
        #endregion "Constructor & Distructor"

        #region "Private variables"
        Int64 _patientStatusID;
        DateTime _patientStatusDate;
        Int64 _patientID;
        string _timeIn = "";
        string _location = "";
        string _status = "";
        string _timeOut = "";
        Int32 _trackingStatus;
        Int64 _masterAppointmentID = 0;
        Int64 _detailAppointmentID = 0;
        Int64 _UserID; //Added for getting User id for Bug #74828: 00000783: Appointment History



        #endregion "Private variables"

        #region "Property Procedures"

        public Int64 PatientStatusID
        { get { return _patientStatusID; } set { _patientStatusID = value; } }

        public DateTime patientStatusDate
        { get { return _patientStatusDate; } set { _patientStatusDate = value; } }

        public Int64 PatientID
        { get { return _patientID; } set { _patientID = value; } }


        public string TimeIn
        { get { return _timeIn; } set { _timeIn = value; } }


        public string Location
        { get { return _location; } set { _location = value; } }

        public string Status

        { get { return _status; } set { _status = value; } }

        public string TimeOut
        { get { return _timeOut; } set { _timeOut = value; } }

        public Int32 TrackingStatus
        { get { return _trackingStatus; } set { _trackingStatus = value; } }


        public Int64 MasterAppointmentID
        { get { return _masterAppointmentID; } set { _masterAppointmentID = value; } }

        public Int64 DetailAppointmentID
        { get { return _detailAppointmentID; } set { _detailAppointmentID = value; } }
        
        //Added for getting User id for Bug #74828: 00000783: Appointment History
        public Int64 UserID
        { get { return _UserID; } set { _UserID = value; } }


        // Added By Pranit on 6 feb 2012
        public DateTime StartAppointmentDate { get; set; }
       
        #endregion "Property Procedures"

        #region "Procedures"
        /// <summary>
        /// To Check Out the Patient Fro the Day (given Date i.e ToDays Date) 
        /// and Update the Appointment Status
        /// </summary>
        public void PatientCheckOut()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {
                //string strSQL = "";
                //strSQL  = "UPDATE PatientTracking  SET nTrackingStatus = 4 WHERE nPatientID= " + _patientID  +" AND CONVERT(VARCHAR, dtDate,101) = '" + DateTime.Now.Date.ToString("MM/dd/yyyy") +"'"; 
                //oDB.Execute_Query (strSQL  );

                object _intresult = 0;
                //@nID ,@dtDate ,@nPatientID,@sTimeIn ,@sLocation ,@sStatus ,@sTimeOut ,@nTrackingStatus  
                oDBParameters.Add("@nID", _patientStatusID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtDate", _patientStatusDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                oDBParameters.Add("@nPatientID", _patientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sTimeIn", _timeIn, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 50);
                oDBParameters.Add("@sLocation", _location, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 255);
                oDBParameters.Add("@sStatus", _status, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 255);
                oDBParameters.Add("@sTimeOut", _timeOut, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 50);
                oDBParameters.Add("@nTrackingStatus", _trackingStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nMSTAppointmentID", _masterAppointmentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nDTLAppointmentID", _detailAppointmentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                //oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(_patientID), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                _intresult = oDB.Execute("gsp_INUP_PatientTracking", oDBParameters, out _intresult);

                //Update Appointment Status
                //NotUsed = 0, Registred = 1, Waiting = 2, CheckIn = 3, CheckOut = 4, NoShow = 5

                // GLO2011-0014315 : Apointment Status
                // Removed dtstartdate condition coz when user checkout the appointment after that day check-in icon not changing to check-out icon.
                // string _sqlQuery = " UPDATE AS_Appointment_DTL WITH (READPAST) SET nUsedStatus = 4 WHERE nMSTAppointmentID = " + _masterAppointmentID + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_patientStatusDate.ToString()) + " AND nClinicID = " + _ClinicID + " ";

                //  BY Pranit on 6 Feb 2012 , changed Update query removed nDTLAppointmentID and Taken StartAppointmentDate GLO2012-0016382
                //string _sqlQuery = " UPDATE AS_Appointment_DTL SET nUsedStatus = 4 WHERE nMSTAppointmentID = " + _masterAppointmentID + " AND nDTLAppointmentID = " + _detailAppointmentID + " AND nClinicID = " + _ClinicID + " ";
                
                //Added for getting User id for Bug #74828: 00000783: Appointment History
                string _sqlQuery = " UPDATE AS_Appointment_DTL WITH (READPAST) SET nUsedStatus = 4, nUserID = " +_UserID  + "  WHERE nMSTAppointmentID = " + _masterAppointmentID + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(StartAppointmentDate.ToShortDateString()) + " AND nClinicID = " + _ClinicID + " ";
                 // End by Pranit on 6 Feb 2012 GLO2012-0016382


                oDB.Execute_Query(_sqlQuery);
                ////Sandip Darade 20100312
                // _sqlQuery = " UPDATE PatientTracking SET bIsCheckOut = 1 WHERE nMSTAppointmentID = " + _masterAppointmentID + " AND nClinicID = " + _ClinicID + " ";
                //oDB.Execute_Query(_sqlQuery);   
                //Sandip Darade 
                _sqlQuery = " UPDATE PatientTracking WITH (READPAST) SET bIsCheckOut = 1 WHERE nDTLAppointmentID = " + _detailAppointmentID + " AND nClinicID = " + _ClinicID + " ";
                oDB.Execute_Query(_sqlQuery);
                //Added by Mayuri:20100422-To fix case No:#0003868
                Int64 nProviderID = 0;
                using (DataTable dtProviderID = GetAppointmentProviderID(_masterAppointmentID))
                {
                    if (dtProviderID != null && dtProviderID.Rows.Count > 0)
                    {
                        nProviderID = Convert.ToInt64(dtProviderID.Rows[0]["nASBaseID"]);
                    }
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.CheckOut, gloAuditTrail.ActivityType.None, "Appointment checked out", _patientID, _masterAppointmentID, nProviderID, gloAuditTrail.ActivityOutCome.Success);


            }
            catch (gloDatabaseLayer.DBException) // DBErr)
            {
                //DBErr.ToString();
                //DBErr = null;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }

        }
        
        /// <summary>
        /// Add Patient Check in details for keeping Patient Status Track.
        /// </summary>
        public void PatientCheckIn()
        {
            
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            Int64 _result = 0;
            try
            {
                object _intresult = 0;
                //@nID ,@dtDate ,@nPatientID,@sTimeIn ,@sLocation ,@sStatus ,@sTimeOut ,@nTrackingStatus  
                oDBParameters.Add("@nID", _patientStatusID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtDate", _patientStatusDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                oDBParameters.Add("@nPatientID", _patientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sTimeIn", _timeIn, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 50);
                oDBParameters.Add("@sLocation", _location, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 255);
                oDBParameters.Add("@sStatus", _status, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 255);
                oDBParameters.Add("@sTimeOut", _timeOut, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 50);
                oDBParameters.Add("@nTrackingStatus", _trackingStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nMSTAppointmentID", _masterAppointmentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nDTLAppointmentID", _detailAppointmentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                //oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(_patientID), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                _intresult = oDB.Execute("gsp_INUP_PatientTracking", oDBParameters, out _intresult);
                //Added by Mayuri:20100422-To fix case No:#0003868
                Int64 nProviderID = 0;
                using (DataTable dtProviderID = GetAppointmentProviderID(_masterAppointmentID))
                {
                    if (dtProviderID != null && dtProviderID.Rows.Count > 0)
                    {
                        nProviderID = Convert.ToInt64(dtProviderID.Rows[0]["nASBaseID"]);
                    }
                }
                //Int64 nProviderID = 
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.CheckIn, gloAuditTrail.ActivityType.None, "Appointment checked in", _patientID, _masterAppointmentID, nProviderID, gloAuditTrail.ActivityOutCome.Success);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        // TO DO
                        MakePriorAuthorizationVisit();

                        //Update Appointment Status
                        //NotUsed = 0, Registred = 1, Waiting = 2, CheckIn = 3, CheckOut = 4, NoShow = 5
                      //  string _sqlQuery = " UPDATE AS_Appointment_DTL SET nUsedStatus = 3 WHERE nMSTAppointmentID = " + _masterAppointmentID + " AND nDTLAppointmentID = " + _detailAppointmentID + " AND nClinicID = " + _ClinicID + " ";
                        
                        //Added for getting User id for Bug #74828: 00000783: Appointment History
                        string _sqlQuery = " UPDATE AS_Appointment_DTL WITH (READPAST) SET nUsedStatus = 3, nUserID = " + _UserID + " WHERE nMSTAppointmentID = " + _masterAppointmentID + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_patientStatusDate.ToString()) + " AND nClinicID = " + _ClinicID + " ";
                        oDB.Execute_Query(_sqlQuery);   

                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }
                
               
                
            }
            catch (gloDatabaseLayer.DBException) // DBErr)
            {
                //DBErr.ToString();
                //DBErr = null;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }

        }

        public DataTable GetAppointmentProviderID(long MasterAppointmentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            DataTable oData = new DataTable();

            try
            {
                oDB.Connect(false);

                _strSQL = "select nASBaseID from AS_Appointment_MST WHERE nMSTAppointmentID = " + MasterAppointmentID;

                oDB.Retrive_Query(_strSQL, out oData);

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

            return oData;
        }

        /// <summary>
        /// Get list of global period to show alter while check in and check out.
        /// </summary>
        public DataTable GetGlobalPeriods_ForAlter(Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtGlobalPeriodsList = null;
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
               

                oDB.Connect(false);
                    string strQuery = " select PD.sInsuranceName AS Insurance, " +
                    " CASE ISNULL(PM.sMiddleName,'') WHEN '' THEN PM.sFirstName + ' ' +  PM.sLastName " +
                    " ELSE PM.sFirstName + ' ' + PM.sMiddleName + ' ' + PM.sLastName END AS Provider, " +
                    " GP.sCPT AS CPT,  CONVERT(VARCHAR(20),GP.dtStartDate,101) + ' - ' + CONVERT(VARCHAR(20),GP.dtEndDate,101) AS Dates, " +
                    " GP.sNotes AS Notes  ,  GlobalDays.sReminder as Reminder from Patient_Global_Periods GP LEFT OUTER JOIN Provider_MST PM " +
                    " ON GP.nProviderID =  PM.nProviderID  LEFT OUTER JOIN PatientInsurance_DTL PD " +
                    " ON GP.nInsuranceID = PD.nInsuranceID CROSS APPLY " +
                    " dbo.Get_Default_GlobalDays_CPT(GP.sCPT,GP.nInsuranceID,GP.nPatientID) AS GlobalDays  " +
                    " where GP.nPatientID =" + nPatientID +
                    " and CONVERT(varchar(10),dbo.gloGetDate(),101) between GP.dtStartDate AND GP.dtEndDate ";
                oDB.Retrive_Query(strQuery, out _dtGlobalPeriodsList);
                 

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }

            return _dtGlobalPeriodsList;
        }
        /// <summary>
        /// Get list of global period to show alter.
        /// </summary>
        public DataTable GetLastGlobalPeriods_ForAlter(Int64 nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtGlobalPeriodsList = null;
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                

                oDB.Connect(false);
                string strQuery = " select top(1) nid As nId,CONVERT(VARCHAR(20),dtStartDate,101) + ' - ' + CONVERT(VARCHAR(20),dtEndDate,101) + ' - ' + sCPT AS Dates " +
                                  " from Patient_Global_Periods  where nPatientID =" + nPatientID + " and CONVERT(varchar(10),dbo.gloGetDate(),101) between dtStartDate AND dtEndDate " +
                                  " order by dtEndDate desc ,dtCreatedDatetime DESC  ";
                oDB.Retrive_Query(strQuery, out _dtGlobalPeriodsList);


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
            }

            return _dtGlobalPeriodsList;
        }

        /// <summary>
        /// Increment visits made by Patient across autorized Insurances.
        /// </summary>
        public void MakePriorAuthorizationVisit()
        {
            //variable declarations.

            String _sql = String.Empty;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            DataTable dttempPatientTracking = new DataTable();
            DataTable dttempPriorInsurances = new DataTable();

            // Check Checkin Flag for Patient.
            try
            {
                _sql = " Select nID from PatientTracking WITH (NOLOCK) where nPatientID = '" + _patientID + "' and dtDate ='" + DateTime.Now.Date + "'";

                oDB.Connect(false);
                oDB.Retrive_Query(_sql, out dttempPatientTracking);

                if (dttempPatientTracking != null && dttempPatientTracking.Rows.Count > 0)
                {
                    // Read Insurances if any from Prior Authorization.
                    //_sql = "SELECT ISNULL(nAuthorizationID, 0) AS nAuthorizationID, ISNULL(nPatientID, 0) AS nPatientID, ISNULL(nInsuranceID, 0) AS nInsuranceID, ISNULL(sInsuranceName,'') AS sInsuranceName, ";
                    //_sql += "dtAuthorization, dtAuthorizationThrough, ISNULL(sAuthorizationNumber,'') AS sAuthorizationNumber, ISNULL(nTotalVisits, 0) AS nTotalVisits, ISNULL(nVisitsMade, 0) AS nVisitsMade, ";
                    //_sql += "ISNULL(nAppointmentType, 0) AS nAppointmentType FROM PatientPriorAuthorization WHERE nPatientID = '" + _patientID + "'";


                    _sql = "";
                    _sql = " SELECT ISNULL(PatientPriorAuthorization.nAuthorizationID, 0) AS nAuthorizationID, ISNULL(PatientPriorAuthorization.nPatientID, 0) AS nPatientID, ";
                    _sql += " ISNULL(PatientPriorAuthorization.nInsuranceID, 0) AS nInsuranceID, ISNULL(PatientPriorAuthorization.sInsuranceName, '') AS sInsuranceName, ";
                    _sql += " PatientPriorAuthorization.dtAuthorization, PatientPriorAuthorization.dtAuthorizationThrough, ISNULL(PatientPriorAuthorization.sAuthorizationNumber, '') ";
                    _sql += " AS sAuthorizationNumber, ISNULL(PatientPriorAuthorization.nTotalVisits, 0) AS nTotalVisits, ISNULL(PatientPriorAuthorization.nVisitsMade, 0) ";
                    _sql += " AS nVisitsMade, ISNULL(PatientPriorAuthorization.nAppointmentType, 0) AS nAppointmentType, AS_Appointment_MST.sAppointmentTypeDesc, ";
                    _sql += " AS_Appointment_DTL.dtStartDate , AS_Appointment_DTL.nDTLAppointmentID as nDTLAppointmentID, AS_Appointment_DTL.nMSTAppointmentID AS nMSTAppointmentID, AS_Appointment_DTL.nUsedStatus as  nUsedStatus ";
                    _sql += " FROM AS_Appointment_DTL WITH (NOLOCK) INNER JOIN ";
                    _sql += " AS_Appointment_MST WITH (NOLOCK) ON AS_Appointment_DTL.nMSTAppointmentID = AS_Appointment_MST.nMSTAppointmentID INNER JOIN ";
                    _sql += " PatientPriorAuthorization WITH (NOLOCK) ON AS_Appointment_MST.nPatientID = PatientPriorAuthorization.nPatientID AND ";
                    _sql += " AS_Appointment_MST.nAppointmentTypeID = PatientPriorAuthorization.nAppointmentType ";
                    _sql += " where PatientPriorAuthorization.npatientid = '" + _patientID + "' AND AS_Appointment_DTL.dtStartDate = '" + gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString()) + "' AND AS_Appointment_DTL.dtEndDate = '" + gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToString()) + "' ";

                    oDB.Retrive_Query(_sql, out dttempPriorInsurances);

                    if (dttempPriorInsurances != null && dttempPriorInsurances.Rows.Count > 0)
                    {
                        for (int i = 0; i < dttempPriorInsurances.Rows.Count; i++)
                        {
                            // Validate against Date.    
                            if (Convert.ToDateTime(dttempPriorInsurances.Rows[i]["dtAuthorization"]) < DateTime.Now && Convert.ToDateTime(dttempPriorInsurances.Rows[i]["dtAuthorizationThrough"]) > DateTime.Now)
                            {
                                //Increment Visits made for a PATIENT against all insurances.
                                _sql = " UPDATE PatientPriorAuthorization WITH (READPAST) SET nVisitsMade =nVisitsMade + 1 where nPatientId = '" + _patientID + "' and nInsuranceID = '" + dttempPriorInsurances.Rows[i]["nInsuranceID"] + "'and dtAuthorization <= '" + DateTime.Now.Date + "' and  dtAuthorizationThrough >= '" + DateTime.Now.Date + "'";
                                oDB.Execute_Query(_sql);

                            }

                            //Change the AppointmentDTL Status.
                            if (dttempPriorInsurances.Rows[i]["nDTLAppointmentID"].ToString() != "" && dttempPriorInsurances.Rows[i]["nMSTAppointmentID"].ToString() != "")
                            {
                                //Increment Visits made for a PATIENT against all insurances.
                                _sql = " UPDATE AS_Appointment_DTL WITH (READPAST) SET nUsedStatus = 2 where nDTLAppointmentID = '" + dttempPriorInsurances.Rows[i]["nDTLAppointmentID"].ToString() + "' and nMSTAppointmentID = '" + dttempPriorInsurances.Rows[i]["nMSTAppointmentID"].ToString() + "'";
                                oDB.Execute_Query(_sql);
                            }
                            //**Change the AppointmentDTL Status.
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                if (dttempPatientTracking != null)
                {
                    dttempPatientTracking.Dispose();
                    dttempPatientTracking = null;
                }

                if (dttempPriorInsurances != null)
                {
                    dttempPriorInsurances.Dispose();
                    dttempPriorInsurances = null;
                }

                //Show error     
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);     
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dttempPatientTracking != null)
                {
                    dttempPatientTracking.Dispose();
                    dttempPatientTracking = null;
                }

                if (dttempPriorInsurances != null)
                {
                    dttempPriorInsurances.Dispose();
                    dttempPriorInsurances = null;
                }
            }
        }

        #endregion "Procedures"
    }
}
