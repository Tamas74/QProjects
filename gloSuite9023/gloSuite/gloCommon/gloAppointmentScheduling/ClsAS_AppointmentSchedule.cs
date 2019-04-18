using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using gloSettings;

namespace gloAppointmentScheduling
{
    #region "Enum Declaration"

        //public enum AppointmentBookFlag
        //{
        //    Provider = 1, Resource = 2, Coverage = 3, Procedure = 4, ReferralDoctor = 5, None = 6
        //}

        public enum AppointmentScheduleFlag
        {
            None = 0,
            ProviderSchedule = 1,
            ResourceSchedule = 2,
            BlockedSchedule = 3,
            Appointment = 4,
            TemplateAppointment = 5,
            TemplateBlock = 6
        }

        public enum ASBaseType
        {
            None = 0,
            Provider = 1,
            ProblemType = 2,
            Resource = 3,
            User = 4,
            Block = 5,
            ReferralDoctor = 6
        }

        public enum ASUpdateCriteria
        {
            None = 0,
            CancelSave = 1,
            DeleteOccurenceAndCreateNewRecurrence = 2,
            DontDeleteOccurenceAndCreateNewRecurrence = 3,
        }
        public enum ASUsedStatus
        {
            //Currently we are using only used or not used, we have to define as per project move
            NotUsed = 0, Registred = 1, Waiting = 2, CheckIn = 3, CheckOut = 4, NoShow = 5,
            Cancel = 6, Delete = 7, Unknown3 = 8, Unknown4 = 9, Unknown5 = 10, Block = 11
        }

        public enum SingleRecurrence
        {
            Single = 1, Recurrence = 2, SingleInRecurrence = 3
        }

        public enum RecurrencePatternType
        {
            Daily = 1, Weekly = 2, Monthly = 3, Yearly = 4
        }

        public enum RecurrencePatternFlag
        {
            EveryDay = 1, EveryWeekday = 2, DayOfMonthCriteria = 3, SelectedCriteria = 4
        }

        public enum RecurrenceRangeFlag
        {
            EndByYear = 1, EndAfterOccurence = 2, EndDate = 3
        }

        public enum PatternDailyFlag
        {
            EveryDay = 1, EveryWeekday = 2
        }

        public enum FirstLastCriteria
        {
            first = 1, second = 2, third = 3, fourth = 4, last = 5
        }


        public enum DayWeekday
        {
            Sunday = 0, Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6, day = 7, weekday = 8, weekendday = 9
        }

        public enum MonthRange
        {
            January = 1, February = 2, March = 3, April = 4, May = 5, June = 6, July = 7, August = 8, September = 9, October = 10, November = 11, December = 12
        }

    #endregion

    public class AppointmentSchedule : IDisposable
    {
        #region "Constructor & Distructor"
            public AppointmentSchedule()
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

            ~AppointmentSchedule()
            {
                Dispose(false);
            }

        #endregion

        #region "Private Variables"
            private Int64 _nMasterID = 0;
            private Int64 _nDetailID = 0;
            private SingleRecurrence _nIsRecurrence = SingleRecurrence.Single;
            private Int64 _nLineNo = 0;
            private AppointmentScheduleFlag _ASFlag = AppointmentScheduleFlag.None;
            private Int64 _nPatientID = 0; // only for appointment purpose to retrive in calendar
            private string _sPatientName = "";
            private Int64 _nASBaseID = 0; //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
            private string _sASBaseCode = "";
            private string _sASBaseDesc = "";
            private ASBaseType _ASBaseFlag;
            private Int64 _nASRefID = 0; //Appointment - Ref Appointment Detials ID, Schedule - Base Provider ID,Base Resource ID, etc
            private ASBaseType _ASRefFlag;
            private DateTime _dtStartDate;    
            private DateTime _dtStartTime;
            private DateTime _dtEndDate;    
            private DateTime _dtEndTime;
            private Int32 _nColorCode = 0;
            private Int64 _nLocationID = 0;
            private string _sLocationName = "";
            private Int64 _nDepartmentID = 0;
            private string _sDepartmentName = "";
            private string _sNotes = "";
            private Int64 _nClinicID = 0;
            private string _sExternalCode = "";
            private Int64 _nExternalID = 0;
            private ASUsedStatus _nUsedStatus = ASUsedStatus.NotUsed;
        #endregion

        #region "Properties"
        public Int64 MasterID
        {
            get { return _nMasterID; }
            set { _nMasterID = value; }
        }

        public Int64 DetailID
        {
            get { return _nDetailID; }
            set { _nDetailID = value; }
        }

        public SingleRecurrence IsRecurrence
        {
            get { return _nIsRecurrence; }
            set { _nIsRecurrence = value; }
        }

        public Int64 LineNo
        {
            get { return _nLineNo; }
            set { _nLineNo = value; }
        }

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public string PatientName
        {
            get { return _sPatientName; }
            set { _sPatientName = value; }
        }

        public AppointmentScheduleFlag ASFlag
        {
            get { return _ASFlag; }
            set { _ASFlag = value; }
        }

        public Int64 ASBaseID
        {
            get { return _nASBaseID; }
            set { _nASBaseID = value; }
        }
        //---
        public string ASBaseCode
        {
            get { return _sASBaseCode; }
            set { _sASBaseCode = value; }
        }

        public string ASBaseDescription
        {
            get { return _sASBaseDesc; }
            set { _sASBaseDesc = value; }
        }

        public ASBaseType ASBaseFlag
        {
            get { return _ASBaseFlag; }
            set { _ASBaseFlag = value; }
        }

        public Int64 ASReferranceID
        {
            get { return _nASRefID; }
            set { _nASRefID = value; }
        }

        public ASBaseType ASReferranceFlag
        {
            get { return _ASRefFlag; }
            set { _ASRefFlag = value; }
        }

        public DateTime StartDate
        {
            get { return _dtStartDate; }
            set { _dtStartDate = value; }
        }

        public DateTime StartTime
        {
            get { return _dtStartTime; }
            set { _dtStartTime = value; }
        }

        public DateTime EndDate
        {
            get { return _dtEndDate; }
            set { _dtEndDate = value; }
        }

        public DateTime EndTime
        {
            get { return _dtEndTime; }
            set { _dtEndTime = value; }
        }

        public Int32 ColorCode
        {
            get { return _nColorCode; }
            set { _nColorCode = value; }
        }

        public Int64 LocationID
        {
            get { return _nLocationID; }
            set { _nLocationID = value; }
        }

        public string LocationName
        {
            get { return _sLocationName; }
            set { _sLocationName = value; }
        }

        public Int64 DepartmentID
        {
            get { return _nDepartmentID; }
            set { _nDepartmentID = value; }
        }

        public string DepartmentName
        {
            get { return _sDepartmentName; }
            set { _sDepartmentName = value; }
        }

        public string Notes
        {
            get { return _sNotes; }
            set { _sNotes = value; }
        }

        public Int64 ClinicID
        {
            get { return _nClinicID; }
            set { _nClinicID = value; }
        }

        public string ExternalCode
        {
            get { return _sExternalCode; }
            set { _sExternalCode = value; }
        }

        public Int64 ExternalID
        {
            get { return _nExternalID; }
            set { _nExternalID = value; }
        }

        public ASUsedStatus UsedStatus
        {
            get { return _nUsedStatus; }
            set { _nUsedStatus = value; }
        }
        #endregion

    }

    public class AppointmentSchedules : IDisposable
    {

        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public AppointmentSchedules()
        {
            _innerlist = new ArrayList();
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


        ~AppointmentSchedules()
        {
            Dispose(false);
        }
        #endregion

        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(AppointmentSchedule item)
        {
            _innerlist.Add(item);
        }

        //Remark - Work Remining for comparision
        public bool Remove(AppointmentSchedule item)        
        {
            bool result = false;


            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public AppointmentSchedule this[int index]
        {
            get
            { return (AppointmentSchedule)_innerlist[index]; }
        }

        public bool Contains(AppointmentSchedule item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(AppointmentSchedule item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(AppointmentSchedule[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    public class MasterAppointment : IDisposable
    {
        #region "Constructor & Distructor"

        public MasterAppointment()
        {
            _Criteria = new gloAppointmentScheduling.Criteria.gloCriteria();
            _ProblemTypes = new ShortApointmentSchedules();
            _Resources = new ShortApointmentSchedules();
            _AppointmentDetails = new AppointmentSchedules();
            this.PATransaction = new PriorAuthorizationTransaction();
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
                    _Criteria.Dispose();
                    _ProblemTypes.Dispose();
                    _Resources.Dispose();
                }
            }
            disposed = true;
        }

        ~MasterAppointment()
        {
            Dispose(false);
        }

        #endregion

        #region "Private Variables"
        private Int64 _nMasterID = 0;
        private SingleRecurrence _nIsRecurrence = SingleRecurrence.Single;
        private AppointmentScheduleFlag _ASFlag = AppointmentScheduleFlag.None;
        private Int64 _nAppointmentTypeID = 0; //Appointment Predefined Type
        private string _sAppointmentTypeCode = "";
        private string _sAppointmentTypeDesc = "";
        private Int64 _nASBaseID = 0; //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
        private string _sASBaseCode = "";
        private string _sASBaseDesc = "";
        private ASBaseType _ASBaseFlag;
        private Int64 _nReferralProviderID = 0; 
        private string _sReferralProviderCode = "";
        private string _sReferralProviderName = "";
        private Int64 _nPatientID = 0;
        private string _sPatientName = "";
        private DateTime _dtStartDate;
        private DateTime _dtStartTime;
        private DateTime _dtEndDate;
        private DateTime _dtEndTime;
        private decimal _dDuration = 0;
        private Int32 _nColorCode = 0;
        private Int64 _nLocationID = 0;
        private string _sLocationName = "";
        private Int64 _nDepartmentID = 0;
        private string _sDepartmentName = "";
        private string _sNotes = "";
        private Int64 _nClinicID = 0;
        private AppointmentSchedules _AppointmentDetails = null;
        private Criteria.gloCriteria _Criteria = null;
        private ShortApointmentSchedules _ProblemTypes = null;
        private ShortApointmentSchedules _Resources = null;
        private object _Insurances = new object();
        private ASUsedStatus _nUsedStatus = ASUsedStatus.NotUsed; //we used in master only for provider appointment other will cover in short appointment
        private Int64 _nAppointmentStatusID = 0;
        #endregion




        #region "Properties"

        //By Pranit on 13 sep to store comma seperated Resource IDs in this (nResourceIDS) Field
        public StringBuilder ResourceIDS { get; set; }
        
        
        public Int64 MasterID
        {
            get { return _nMasterID; }
            set { _nMasterID = value; }
        }

        public SingleRecurrence IsRecurrence
        {
            get { return _nIsRecurrence; }
            set { _nIsRecurrence = value; }
        }

        public AppointmentScheduleFlag ASFlag
        {
            get { return _ASFlag; }
            set { _ASFlag = value; }
        }

        public Int64 AppointmentTypeID
        {
            get { return _nAppointmentTypeID; }
            set { _nAppointmentTypeID = value; }
        }

        public string AppointmentTypeCode
        {
            get { return _sAppointmentTypeCode; }
            set { _sAppointmentTypeCode = value; }
        }

        public string AppointmentTypeDesc
        {
            get { return _sAppointmentTypeDesc; }
            set { _sAppointmentTypeDesc = value; }
        }

        public Int64 ASBaseID
        {
            get { return _nASBaseID; }
            set { _nASBaseID = value; }
        }
      
        public string ASBaseCode
        {
            get { return _sASBaseCode; }
            set { _sASBaseCode = value; }
        }

        public string ASBaseDescription
        {
            get { return _sASBaseDesc; }
            set { _sASBaseDesc = value; }
        }

        public ASBaseType ASBaseFlag
        {
            get { return _ASBaseFlag; }
            set { _ASBaseFlag = value; }
        }

        public Int64 ReferralProviderID
        {
            get { return _nReferralProviderID; }
            set { _nReferralProviderID = value; }
        }

        public string ReferralProviderCode
        {
            get { return _sReferralProviderCode; }
            set { _sReferralProviderCode = value; }
        }

        public string ReferralProviderName
        {
            get { return _sReferralProviderName; }
            set { _sReferralProviderName = value; }
        }

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public string PatientName
        {
            get { return _sPatientName; }
            set { _sPatientName = value; }
        }

        public DateTime StartDate
        {
            get { return _dtStartDate; }
            set { _dtStartDate = value; }
        }

        public DateTime StartTime
        {
            get { return _dtStartTime; }
            set { _dtStartTime = value; }
        }

        public DateTime EndDate
        {
            get { return _dtEndDate; }
            set { _dtEndDate = value; }
        }

        public DateTime EndTime
        {
            get { return _dtEndTime; }
            set { _dtEndTime = value; }
        }

        public decimal Duration
        {
            get { return _dDuration; }
            set { _dDuration = value; }
        }

        public Int32 ColorCode
        {
            get { return _nColorCode; }
            set { _nColorCode = value; }
        }

        public Int64 LocationID
        {
            get { return _nLocationID; }
            set { _nLocationID = value; }
        }

        public string LocationName
        {
            get { return _sLocationName; }
            set { _sLocationName = value; }
        }

        public Int64 DepartmentID
        {
            get { return _nDepartmentID; }
            set { _nDepartmentID = value; }
        }

        public string DepartmentName
        {
            get { return _sDepartmentName; }
            set { _sDepartmentName = value; }
        }

        public string Notes
        {
            get { return _sNotes; }
            set { _sNotes = value; }
        }

        public Int64 ClinicID
        {
            get { return _nClinicID; }
            set { _nClinicID = value; }
        }

        public  AppointmentSchedules AppointmentDetails
        {
            get { return _AppointmentDetails; }
            set { _AppointmentDetails = value; }
        }

        public Criteria.gloCriteria Criteria
        {
            get { return _Criteria; }
            set { _Criteria = value; }
        }

        public ShortApointmentSchedules ProblemTypes
        {
            get { return _ProblemTypes; }
            set { _ProblemTypes = value; }
        }

        public ShortApointmentSchedules Resources
        {
            get { return _Resources; }
            set { _Resources = value; }
        }

        public object Insurances
        {
             get { return _Insurances; }
             set { _Insurances = value; }
        }

        public ASUsedStatus UsedStatus
        {
            get { return _nUsedStatus; }
            set { _nUsedStatus = value; }
        }

        public Int64 AppointmentStatusID
        {
            get { return _nAppointmentStatusID; }
            set { _nAppointmentStatusID  = value; }
        }

        public bool PARequired { get; set; }

        public PriorAuthorizationTransaction PATransaction { get; set; }

        public bool ShowTemplateAppt_Flag { get; set; }

        public long TempAllocationID { get; set; }

        public string LocationIDs { get; set; }

        // Added By Pranit on 2 feb 2012
        private bool _allowRecurrenceToOverRideBlockeAppointment = false;
        public bool AllowRecurrenceToOverRideBlockeAppointment
        {
            get { return _allowRecurrenceToOverRideBlockeAppointment; }
            set { _allowRecurrenceToOverRideBlockeAppointment = value; }
        }
        // End By Pranit on 2 feb 2012


        // Added By Pranit on 16 feb 2012
        private string _datesWithCommaSeparator = string.Empty;
        public string DatesWithCommaSeparator
        {
            get { return _datesWithCommaSeparator; }
            set { _datesWithCommaSeparator = value; }
        }

        private long _oldMasterID = 0;
        public long OldMasterID
        {
            get { return _oldMasterID; }
            set { _oldMasterID = value; }
        }

        // End By Pranit on 16 feb 2012



        #endregion
    }

    public class PriorAuthorizationTransaction : IDisposable
    {
        #region "Constructor & Distructor"

        public PriorAuthorizationTransaction()
        { }

    //    private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        //    disposed = true;
        }

        ~PriorAuthorizationTransaction()
        {
            Dispose(false);
        }

        #endregion

        #region "Properties"

        public Int64 MasterAppointmentID { get; set; }
        public Int64 DetailAppointmentID { get; set; }

        public Int64 AppointmentDate { get; set; }

        public Int64 PriorAuthorizationID { get; set; }
        public string PriorAuthorizationNo { get; set; }

        public Int64 PatientID { get; set; }
        
        public Int64 BillingTranactionID { get; set; }
        public Int64 InsuranceID { get; set; }
        public int IsCheckIn { get; set; }
        public int IsSingleOccurance { get; set; }
        public string Version { get; set; }

        public Int64 PATransactionID { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsUpdated { get; set; }

        #endregion

        public static bool Insert(PriorAuthorizationTransaction PATransaction)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            bool _isSaved = false;

            try
            {
                oParameters.Clear();

                oParameters.Add("@nMSTAppointmentID", PATransaction.MasterAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nDTLAppointmentID", PATransaction.DetailAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bIsSingleRecurrence", PATransaction.IsSingleOccurance, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nPatientID", PATransaction.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nAuthorizationID", PATransaction.PriorAuthorizationID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nAuthorizationNo", PATransaction.PriorAuthorizationNo, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nInsuranceID", PATransaction.InsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nBillingTransactionID", PATransaction.BillingTranactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nAppointmentDate", PATransaction.AppointmentDate, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nIsCheckedIn", PATransaction.IsCheckIn, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nVersion", PATransaction.Version, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nPATransactionID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);

                int _retVal = 0;
                oDB.Connect(false);
                _retVal = oDB.Execute("AS_INSERT_Appointment_PATransaction", oParameters);

                if (_retVal == 0)
                { _isSaved = false; }
                else
                { _isSaved = true; }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
            }
            return _isSaved;
        }

        public static bool Update(PriorAuthorizationTransaction PATransaction)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            object _retVal = null;

            bool _isUpdated = false;

            try
            {
                _sqlQuery = " UPDATE [PatientPriorAuthorization_Transaction] SET " +
                                        " [nAuthorizationID] = " + PATransaction.PriorAuthorizationID + ", " +  
                                        " [nAuthorizationNo] = '" + PATransaction.PriorAuthorizationNo.Replace("'","''") + "'" + ", " +
                                        " [nAppointmentDate] = '" + PATransaction.AppointmentDate + "'" +
                                        " WHERE nMSTAppointmentID = " + PATransaction.MasterAppointmentID +
                                        " AND  nDTLAppointmentID = " + PATransaction.DetailAppointmentID + ";";

                oDB.Connect(false);
                _retVal = oDB.Execute_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                { _isUpdated = true; }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
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
            return _isUpdated;
        }

        public static bool Delete(Int64 MasterAppointmentID, Int64 DetailAppointmentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            object _retVal = null;

            bool _isDeleted = false;

            try
            {
                if (!DetailAppointmentID.Equals(0))
                {
                    _sqlQuery = "DELETE FROM PatientPriorAuthorization_Transaction WHERE nMSTAppointmentID = "
                        + MasterAppointmentID + " AND nDTLAppointmentID = " + DetailAppointmentID;
                }
                else
                {
                    _sqlQuery = "DELETE FROM PatientPriorAuthorization_Transaction WHERE nMSTAppointmentID = "
                        + MasterAppointmentID;
                }

                oDB.Connect(false);
                _retVal = oDB.Execute_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                { _isDeleted = true; }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
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
            return _isDeleted;
        }

        public static bool IsExist(Int64 MasterAppointmentID, Int64 DetailAppointmentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string _sqlQuery = "";
            object _retVal = null;

            bool _isDeleted = false;

            try
            {
                _sqlQuery = " SELECT count(*) IsExist FROM PatientPriorAuthorization_Transaction WITH(NOLOCK) " +
                                    " WHERE nMSTAppointmentID = " + MasterAppointmentID + " and nDTLAppointmentID = " + DetailAppointmentID;

                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToBoolean(_retVal) == true)
                { _isDeleted = true; }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
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
            return _isDeleted;
        }

    }

    public class MasterSchedule : IDisposable
    {
        #region "Constructor & Distructor"

        public MasterSchedule()
        {
            _Criteria = new gloAppointmentScheduling.Criteria.gloCriteria();
            _ProblemTypes = new ShortApointmentSchedules();
            _Resources = new ShortApointmentSchedules();
            _Users = new ShortApointmentSchedules();
            _ScheduleDetails = new AppointmentSchedules(); 
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
                    _Criteria.Dispose();
                    _ProblemTypes.Dispose();
                    _Resources.Dispose();
                    _Users.Dispose();
                }
            }
            disposed = true;
        }

        ~MasterSchedule()
        {
            Dispose(false);
        }

        #endregion

        #region "Private Variables"
        private Int64 _nMasterID = 0;
        private SingleRecurrence _nIsRecurrence = SingleRecurrence.Single;
        private AppointmentScheduleFlag _ASFlag = AppointmentScheduleFlag.None;
        private Int64 _nScheduleTypeID = 0; //***PROVISIONAL*** Schedule Predefined Type
        private string _sScheduleTypeCode = "";
        private string _sScheduleTypeDesc = "";
        private Int64 _nASBaseID = 0; //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
        private string _sASBaseCode = "";
        private string _sASBaseDesc = "";
        private ASBaseType _ASBaseFlag;
        private DateTime _dtStartDate;
        private DateTime _dtStartTime;
        private DateTime _dtEndDate;
        private DateTime _dtEndTime;
        private decimal _dDuration = 0;
        private Int32 _nColorCode = 0;
        private Int64 _nLocationID = 0;
        private string _sLocationName = "";
        private Int64 _nDepartmentID = 0;
        private string _sDepartmentName = "";
        private string _sNotes = "";
        private Int64 _nClinicID = 0;
        private Criteria.gloCriteria _Criteria = null;
        private AppointmentSchedules _ScheduleDetails = null;
        private ShortApointmentSchedules _ProblemTypes = null;
        private ShortApointmentSchedules _Resources = null;
        private ShortApointmentSchedules _Users = null;
        private ASUsedStatus _nUsedStatus = ASUsedStatus.NotUsed;

        #endregion

        #region "Properties"

        public Int64 MasterID
        {
            get { return _nMasterID; }
            set { _nMasterID = value; }
        }

        public SingleRecurrence IsRecurrence
        {
            get { return _nIsRecurrence; }
            set { _nIsRecurrence = value; }
        }

        public AppointmentScheduleFlag ASFlag
        {
            get { return _ASFlag; }
            set { _ASFlag = value; }
        }

        public Int64 ScheduleTypeID
        {
            get { return _nScheduleTypeID; }
            set { _nScheduleTypeID = value; }
        }

        public string ScheduleTypeCode
        {
            get { return _sScheduleTypeCode; }
            set { _sScheduleTypeCode = value; }
        }

        public string ScheduleTypeDesc
        {
            get { return _sScheduleTypeDesc; }
            set { _sScheduleTypeDesc = value; }
        }

        public Int64 ASBaseID
        {
            get { return _nASBaseID; }
            set { _nASBaseID = value; }
        }

        public string ASBaseCode
        {
            get { return _sASBaseCode; }
            set { _sASBaseCode = value; }
        }

        public string ASBaseDescription
        {
            get { return _sASBaseDesc; }
            set { _sASBaseDesc = value; }
        }

        public ASBaseType ASBaseFlag
        {
            get { return _ASBaseFlag; }
            set { _ASBaseFlag = value; }
        }
        
        public DateTime StartDate
        {
            get { return _dtStartDate; }
            set { _dtStartDate = value; }
        }

        public DateTime StartTime
        {
            get { return _dtStartTime; }
            set { _dtStartTime = value; }
        }

        public DateTime EndDate
        {
            get { return _dtEndDate; }
            set { _dtEndDate = value; }
        }

        public DateTime EndTime
        {
            get { return _dtEndTime; }
            set { _dtEndTime = value; }
        }

        public decimal Duration
        {
            get { return _dDuration; }
            set { _dDuration = value; }
        }

        public Int32 ColorCode
        {
            get { return _nColorCode; }
            set { _nColorCode = value; }
        }

        public Int64 LocationID
        {
            get { return _nLocationID; }
            set { _nLocationID = value; }
        }

        public string LocationName
        {
            get { return _sLocationName; }
            set { _sLocationName = value; }
        }

        public Int64 DepartmentID
        {
            get { return _nDepartmentID; }
            set { _nDepartmentID = value; }
        }

        public string DepartmentName
        {
            get { return _sDepartmentName; }
            set { _sDepartmentName = value; }
        }

        public string Notes
        {
            get { return _sNotes; }
            set { _sNotes = value; }
        }

        public Int64 ClinicID
        {
            get { return _nClinicID; }
            set { _nClinicID = value; }
        }

        public Criteria.gloCriteria Criteria
        {
            get { return _Criteria; }
            set { _Criteria = value; }
        }

        public AppointmentSchedules ScheduleDetails
         {
            get { return _ScheduleDetails; }
             set { _ScheduleDetails = value; }
        }

        public ShortApointmentSchedules ProblemTypes
        {
            get { return _ProblemTypes; }
            set { _ProblemTypes = value; }
        }

        public ShortApointmentSchedules Resources
        {
            get { return _Resources; }
            set { _Resources = value; }
        }

        public ShortApointmentSchedules Users
        {
            get { return _Users; }
            set { _Users = value; }
        }

        public ASUsedStatus UsedStatus
        {
            get { return _nUsedStatus; }
            set { _nUsedStatus = value; }
        }
        #endregion
    }

    public class ShortApointmentSchedule : IDisposable
    {
        #region "Constructor & Distructor"
        public ShortApointmentSchedule()
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

        ~ShortApointmentSchedule()
        {
            Dispose(false);
        }

        #endregion

        #region "Private Variables"
        private Int64 _nMasterID = 0;
        private Int64 _nDetailID = 0;
        private SingleRecurrence _nIsRecurrence = SingleRecurrence.Single;
        private Int64 _nPatientID = 0; // only for appointment purpose to retrive in calendar
        private Int64 _nLineNo = 0;
        private AppointmentScheduleFlag _ASFlag = AppointmentScheduleFlag.None;
        private Int64 _nASCommonID = 0; //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
        private string _sASCommonCode = "";
        private string _sASCommonDesc = "";
        private ASBaseType _ASCommonFlag;
        private DateTime _dtStartDate;
        private DateTime _dtStartTime;
        private DateTime _dtEndDate;
        private DateTime _dtEndTime;
        private Int32 _nColorCode = 0;
        private Int64 _nClinicID = 0;
        private string _ViewOtherDetails = ""; // only for appointment purpose to retrive in calendar
        private ASUsedStatus _nUsedStatus = ASUsedStatus.NotUsed;
        private string _Location = "";

    

        #endregion

        #region "Properties"
        public Int64 MasterID
        {
            get { return _nMasterID; }
            set { _nMasterID = value; }
        }

        public Int64 DetailID
        {
            get { return _nDetailID; }
            set { _nDetailID = value; }
        }

        public SingleRecurrence IsRecurrence
        {
            get { return _nIsRecurrence; }
            set { _nIsRecurrence = value; }
        }

        public Int64 PatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        public Int64 LineNo
        {
            get { return _nLineNo; }
            set { _nLineNo = value; }
        }

        public AppointmentScheduleFlag ASFlag
        {
            get { return _ASFlag; }
            set { _ASFlag = value; }
        }

        public Int64 ASCommonID
        {
            get { return _nASCommonID; }
            set { _nASCommonID = value; }
        }
        //---
        public string ASCommonCode
        {
            get { return _sASCommonCode; }
            set { _sASCommonCode = value; }
        }

        public string ASCommonDescription
        {
            get { return _sASCommonDesc; }
            set { _sASCommonDesc = value; }
        }

        public ASBaseType ASCommonFlag
        {
            get { return _ASCommonFlag; }
            set { _ASCommonFlag = value; }
        }

        public DateTime StartDate
        {
            get { return _dtStartDate; }
            set { _dtStartDate = value; }
        }

        public DateTime StartTime
        {
            get { return _dtStartTime; }
            set { _dtStartTime = value; }
        }

        public DateTime EndDate
        {
            get { return _dtEndDate; }
            set { _dtEndDate = value; }
        }

        public DateTime EndTime
        {
            get { return _dtEndTime; }
            set { _dtEndTime = value; }
        }

        public Int32 ColorCode
        {
            get { return _nColorCode; }
            set { _nColorCode = value; }
        }

        public string ViewOtherDetails
        {
            get { return _ViewOtherDetails; }
            set { _ViewOtherDetails = value; }
        }

        public Int64 ClinicID
        {
            get { return _nClinicID; }
            set { _nClinicID = value; }
        }

        public ASUsedStatus UsedStatus
        {
            get { return _nUsedStatus; }
            set { _nUsedStatus = value; }
        }
        public string Location { get { return _Location; } set { _Location =value;} }
        #endregion

    }

    public class ShortApointmentSchedules : IDisposable
    {

        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public ShortApointmentSchedules()
        {
            _innerlist = new ArrayList();
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


        ~ShortApointmentSchedules()
        {
            Dispose(false);
        }
        #endregion

        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(ShortApointmentSchedule item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(ShortApointmentSchedule item)
        //Remark - Work Remining for comparision
        {
            bool result = false;


            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public ShortApointmentSchedule this[int index]
        {
            get
            { return (ShortApointmentSchedule)_innerlist[index]; }
        }

        public bool Contains(ShortApointmentSchedule item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(ShortApointmentSchedule item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(ShortApointmentSchedule[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    public class CalendarApointmentSchedule:IDisposable
    {
        #region "Constructor & Distructor"
        public CalendarApointmentSchedule()
        {

        }

        public CalendarApointmentSchedule(string ASText, object ASTag, string ASDescription, DateTime StartDateTime, DateTime EndDateTime, Int64 PrvResUsrID, ASBaseType PrvResUsrFlag, Int32 ColorCode)
        {
            _sASText = ASText;
            _oASTag = ASTag;
            _sASDescription = ASDescription;
            _dtStartDateTime =  StartDateTime;
            _dtEndDateTime = EndDateTime;
            _PrvResUsrID = PrvResUsrID;
            _PrvResUsrFlag = PrvResUsrFlag;
            _ColorCode = ColorCode;
            _IsApp = IsApp;
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

        ~CalendarApointmentSchedule()
        {
            Dispose(false);
        }

        #endregion

        #region "Private Variables"
            private string _sASText = ""; // For Appointment - Patient Name + Location + Department
                                          // For Schedule - Location + Department   
            private object _oASTag = null; //For Appointment - Master ID + Detail ID + Single/Recurrence + Is Appointmnet or Template Block Hash Code Value (AppointmentScheduleFlag) + Location ID + Location Name + Department ID + Department Name (Location & Department for send to app form while register from template block) + Line Number + Used Status
                                           //For Schedule - Master ID + Detail ID + Single/Recurrence + Schedule Type Flag + Line Number 
                                                
            private string _sASDescription = ""; // For Appointment - Notes
                                                 // For Schedule - Notes           
            private DateTime _dtStartDateTime;
            private DateTime _dtEndDateTime;
            private Int64 _PrvResUsrID = 0;
            private ASBaseType _PrvResUsrFlag = ASBaseType.None;
            private Int32 _ColorCode = 0;
            private Int32 _IsApp = 0;
        #endregion

        #region "Properties"
        
            public string ASText
            {
                get { return _sASText; }
                set { _sASText = value; }
            }

            public object ASTag
            {
                get { return _oASTag; }
                set { _oASTag = value; }
            }

            public string ASDescription
            {
                get { return _sASDescription; }
                set { _sASDescription = value; }
            }

            public DateTime StartDateTime
            {
                get { return _dtStartDateTime; }
                set { _dtStartDateTime = value; }
            }

            public DateTime EndDateTime
            {
                get { return _dtEndDateTime; }
                set { _dtEndDateTime = value; }
            }

            public Int64 PrvResUsrID
            {
                get { return _PrvResUsrID; }
                set { _PrvResUsrID = value; }
            }

            public ASBaseType PrvResUsrFlag
            {
                get { return _PrvResUsrFlag; }
                set { _PrvResUsrFlag = value; }
            }

            public Int32 ColorCode
            {
                get { return _ColorCode; }
                set { _ColorCode = value; }
            }
            public Int32 IsApp
            {
                get { return _IsApp ; }
                set { _IsApp = value; }
            }
        
        #endregion
    }

    public class CalendarApointmentSchedules : IDisposable
    {

        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public CalendarApointmentSchedules()
        {
            _innerlist = new ArrayList();
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


        ~CalendarApointmentSchedules()
        {
            Dispose(false);
        }
        #endregion

        // Methods Add, Remove, Count , Item of TransactionLine
        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(CalendarApointmentSchedule item)
        {
            _innerlist.Add(item);
        }

        public void Add(string ASText, object ASTag, string ASDescription, DateTime StartDateTime, DateTime EndDateTime, Int64 PrvResUsrID, ASBaseType PrvResUsrFlag, Int32 ColorCode)
        {
            CalendarApointmentSchedule _item = new CalendarApointmentSchedule(ASText, ASTag, ASDescription, StartDateTime, EndDateTime, PrvResUsrID, PrvResUsrFlag,ColorCode);
            _innerlist.Add(_item);
            _item.Dispose();
        }

        public bool Remove(CalendarApointmentSchedule item)
        //Remark - Work Remining for comparision
        {
            bool result = false;


            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public CalendarApointmentSchedule this[int index]
        {
            get
            { return (CalendarApointmentSchedule)_innerlist[index]; }
        }

        public bool Contains(CalendarApointmentSchedule item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(CalendarApointmentSchedule item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(CalendarApointmentSchedule[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }
}
