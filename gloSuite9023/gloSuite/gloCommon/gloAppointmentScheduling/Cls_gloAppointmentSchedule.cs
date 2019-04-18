using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using gloPMGeneral.gloPriorAuthorization;
using gloSettings;


namespace gloAppointmentScheduling
{
    public class gloAppointment
    {
        #region "Constructor & Distructor"

        public string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        private string _MessageBoxCaption = String.Empty;



        //Added By Mukesh Patel For Override Provider Block Schedule Rights 20090808
        private Int64 _UserID = 0;
        private string _UserName = "";

        public  string gstrSQLServerName="";
        public string gstrDatabaseName="";
        public bool gblnSQLAuthentication=false;
        public string gstrSQLUser="";
        public string gstrSQLPassword="";
        public string gstrCaption = "";
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        frmViewAppointment oViewAppointment;

        public gloAppointment(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 1; }


            //Added By Mukesh Patel For Override Provider Block Schedule Rights 20090808
            #region "UserId"
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else
                { _UserID = 1; }
            }
            else
            { _UserID = 1; }


            //Get User Name
            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                { _UserName = Convert.ToString(appSettings["UserName"]).Trim(); }
                else
                { _UserName = ""; }
            }
            else
            { _UserName = ""; }
            //
            #endregion


            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion

            #region "HL7 Message Queue"
            //Added by Abhijeet on 20110923
            gloHL7.HL7OutboundSettings(_databaseconnectionstring);
            //End of code Added by Abhijeet on 20110923
            #endregion "HL7 Message Queue"

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

        ~gloAppointment()
        {
            Dispose(false);
        }

        #endregion

        public delegate void CalendarClosed(object sender, EventArgs e);
        public event CalendarClosed Calendar_Closed;

       
        private Int64 _MasterAppointmentIDForAuthorization = 0;
        private Int64 _DetailAppointmentIDForAuthorization = 0;
        private bool _ISPatientSavingEnable = false;

        public bool ISPatientSavingEnable
        {
            get { return _ISPatientSavingEnable; }
            set { _ISPatientSavingEnable = value; }
        }

        public Int64 MasterAppointmentIDForAuthorization
        {
            get { return _MasterAppointmentIDForAuthorization; }
        }

        public Int64 DetailAppointmentIDForAuthorization
        {
            get { return _DetailAppointmentIDForAuthorization; }
        }

        public Int64 Add(MasterAppointment oMasterAppointment)
        {
            Int64 _nMasterAppointmentID = 0;
            Int64 _nDetailAppointmentID = 0;
            Int64 _nPrevDetailAppointmentID = 0;
            Int64 _nPrevMstAppointmentID = 0;
            string _AppTypeCode = "0";
            string _ProviderCode = "0";
            string _ReferralProviderCode = "0";
            DataTable dt = new DataTable();

            int nAppCntr = 0;
            int nPTCntr = 0;
            int nResCntr = 0;
            Int64 _AppTempLineNumber = 0;
            Object objectID;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            gloAppointmentScheduling.Criteria.FindRecurrences _AppointmentDates = new gloAppointmentScheduling.Criteria.FindRecurrences();
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);
                _AppointmentDates = _AppointmentDates.GetRecurrence(oMasterAppointment.Criteria, oMasterAppointment.ASBaseID, oMasterAppointment.StartTime, oMasterAppointment.EndTime, oMasterAppointment.MasterID, oMasterAppointment.ResourceIDS);



                // by Pranit on 19 sep to check provider is blocked or not and below "isProviderBlocked==false"

                bool isResourcesBlocked = false;
                bool isProviderBlocked = false;

                if ((_AppointmentDates.Dates.Count > 0) && isResourcesBlocked == false && isProviderBlocked == false)
                {

                    #region "Master Appointment"
                    oDBParameters.Add("@MSTAppointmentID", oMasterAppointment.MasterID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@IsSingleRecurrence", oMasterAppointment.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@AppointmentFlag", oMasterAppointment.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@AppointmentTypeID", oMasterAppointment.AppointmentTypeID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@AppointmentTypeCode", _AppTypeCode, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@AppointmentTypeDesc", oMasterAppointment.AppointmentTypeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@ASBaseID", oMasterAppointment.ASBaseID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@ASBaseCode", _ProviderCode, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@ASBaseDesc", oMasterAppointment.ASBaseDescription, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@ASBaseFlag", oMasterAppointment.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@ReferralProviderID", oMasterAppointment.ReferralProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@ReferralProviderCode", _ReferralProviderCode, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@ReferralProviderName", oMasterAppointment.ReferralProviderName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@PatientID", oMasterAppointment.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@StartDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.StartDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@Duration", oMasterAppointment.Duration, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@ColorCode", oMasterAppointment.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@LocationID", oMasterAppointment.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@LocationName", oMasterAppointment.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@DepartmentID", oMasterAppointment.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@DepartmentName", oMasterAppointment.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@ClinicID", oMasterAppointment.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Execute("AS_INSERT_Appointment_MST", oDBParameters, out  objectID);

                    if (objectID == null)
                    { return 0; }

                    _nMasterAppointmentID = (Int64)objectID;

                    _nPrevMstAppointmentID = oMasterAppointment.PATransaction.MasterAppointmentID;
                    _nPrevDetailAppointmentID = oMasterAppointment.PATransaction.DetailAppointmentID;
                    oMasterAppointment.PATransaction.MasterAppointmentID = _nMasterAppointmentID;

                    #endregion

                    #region "Criteria "

                    //Delete All old Detail Records
                    oDB.Execute_Query("DELETE FROM AS_Appointment_MST_Criteria WHERE nMasterAppointmentID = " + _nMasterAppointmentID);

                    //Insert Into Criteria Table.   dbo.AS_Schedule_MST_Criteria
                    //nMasterScheduleID, bIsSingleRecurrence, nRecurrence_PatternType, nRecurrence_Pattern_Daily_EveryDayNo, 
                    if (oMasterAppointment.Criteria.SingleRecurrenceAppointment == SingleRecurrence.Recurrence)
                    {
                        oDBParameters.Clear();
                        oDBParameters = new gloDatabaseLayer.DBParameters();
                        oDBParameters.Add("@nMasterAppointmentID", _nMasterAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@bIsSingleRecurrence", oMasterAppointment.Criteria.SingleRecurrenceAppointment.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_PatternType", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType.GetHashCode(), System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Daily_EveryDayNo", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber, ParameterDirection.Input, SqlDbType.BigInt);

                        //nRecurrence_Pattern_Daily_EveryDayOrWeekDay, nRecurrence_Pattern_Weekly_EveryWeekNo, bRecurrence_Pattern_Weekly_Sunday, 
                        oDBParameters.Add("@nRecurrence_Pattern_Daily_EveryDayOrWeekDay", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Weekly_EveryWeekNo", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Sunday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday, ParameterDirection.Input, SqlDbType.Bit);

                        //bRecurrence_Pattern_Weekly_Monday, bRecurrence_Pattern_Weekly_Tuesday, bRecurrence_Pattern_Weekly_Wednesday, 
                        //bRecurrence_Pattern_Weekly_Thursday, bRecurrence_Pattern_Weekly_Friday, bRecurrence_Pattern_Weekly_Saturday, 
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Monday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Tuesday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Wednesday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Thursday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Friday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Saturday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday, ParameterDirection.Input, SqlDbType.Bit);

                        //nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria, nRecurrence_Pattern_Monthly_DayNumber, nRecurrence_Pattern_Monthly_EveryMonthNumber, 
                        //nRecurrence_Pattern_Monthly_FstLstCriteriaID, nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID, nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria,
                        oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayNumber", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Monthly_EveryMonthNumber", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                        oDBParameters.Add("@nRecurrence_Pattern_Monthly_FstLstCriteriaID", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                        //nRecurrence_Pattern_Yearly_DayNumber, nRecurrence_Pattern_Yearly_MonthOfCriteriaID, nRecurrence_Pattern_Yearly_FstLstCriteriaID,
                        //nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID, nClinicID
                        oDBParameters.Add("@nRecurrence_Pattern_Yearly_DayNumber", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Yearly_MonthOfCriteriaID", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Yearly_FstLstCriteriaID", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                        oDBParameters.Add("@bRange_Flag", oMasterAppointment.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_StartDate", oMasterAppointment.Criteria.RecurrenceCriteria.Range.StartDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.StartTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_EndDate", oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_Duration", Convert.ToInt32(((TimeSpan)oMasterAppointment.EndTime.Subtract(oMasterAppointment.StartTime)).TotalMinutes), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_NoOfOccurence", oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_NoEndDateYear", oMasterAppointment.Criteria.RecurrenceCriteria.Range.NoEndDateYear, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                        oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                        oDB.Execute("AS_INSERT_Appointment_MST_Criteria", oDBParameters);
                    }
                    #endregion "Criteria "

                    //Delete All old Detail Records
                    oDB.Execute_Query("DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + _nMasterAppointmentID);

                    //Delete all PA Transaction for this master entry
                    PriorAuthorizationTransaction.Delete(_nMasterAppointmentID, 0);

                    int Flag = 0;
                    bool isContain = false;

                    //Find Dates for multiple appointment
                    #region "Provider Appointments"

                    for (nAppCntr = 0; nAppCntr <= _AppointmentDates.Dates.Count - 1; nAppCntr++)
                    {
                        oDBParameters.Clear();

                        // By Pranit on 14 sep to not allow entry in Database whose status is Blocked By Pranit on 15 sep Added or condition to check is it single recurrence.

                        if ((_AppointmentDates.ScheduleStatus[nAppCntr].ToString() != "Blocked") || (oMasterAppointment.IsRecurrence == SingleRecurrence.Single) || (oMasterAppointment.AllowRecurrenceToOverRideBlockeAppointment == true))
                        {
                            object _lineresult = new object();
                            _lineresult = oDB.ExecuteScalar_Query("SELECT ISNULL(MAX(nLineNumber),0) + 1 FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE nASBaseID = " + oMasterAppointment.ASBaseID + " AND nASBaseFlag  = " + oMasterAppointment.ASBaseFlag.GetHashCode() + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()) + " AND nClinicID = " + oMasterAppointment.ClinicID + "");
                            if (_lineresult != null)
                            {
                                if (_lineresult.ToString() != "")
                                {
                                    _AppTempLineNumber = Convert.ToInt64(_lineresult.ToString());
                                }
                            }
                            _lineresult = null;

                            oDBParameters.Add("@MSTAppointmentID", _nMasterAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@DTLAppointmentID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            oDBParameters.Add("@IsSingleRecurrence", oMasterAppointment.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@LineNumber", _AppTempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@AppointmentFlag", oMasterAppointment.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@ASBaseID", oMasterAppointment.ASBaseID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@ASBaseCode", oMasterAppointment.ASBaseCode, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@ASBaseDesc", oMasterAppointment.ASBaseDescription, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@ASBaseFlag", oMasterAppointment.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@RefID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@RefFlag", 0, ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@StartDate", gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                            if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                            {
                                oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            }
                            else
                            {
                                System.DateTime dtendDate;
                                decimal dDuration;
                                dDuration = oMasterAppointment.Duration;
                                dtendDate = (DateTime)_AppointmentDates.Dates[nAppCntr];
                                dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterAppointment.StartTime.ToShortTimeString());
                                dtendDate = dtendDate.AddMinutes((double)dDuration);

                                oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            }

                            oDBParameters.Add("@LocationID", oMasterAppointment.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@LocationName", oMasterAppointment.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@DepartmentID", oMasterAppointment.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@DepartmentName", oMasterAppointment.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@ColorCode", oMasterAppointment.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@Notes", oMasterAppointment.Notes, ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                            oDBParameters.Add("@ClinicID", oMasterAppointment.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@ExternalCode", "0", ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@ExternalID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@PARequired", oMasterAppointment.PARequired, ParameterDirection.Input, SqlDbType.Bit);

                            isContain = false;
                            Flag = oMasterAppointment.UsedStatus.GetHashCode();
                            if (oMasterAppointment.DatesWithCommaSeparator != String.Empty)
                            {
                                string[] strArr = oMasterAppointment.DatesWithCommaSeparator.Split(',');
                                if (strArr.Length > 0)
                                {
                                    for (int i = 0; i <= strArr.Length - 1; i++)
                                    {
                                        if ((strArr[i]).Contains(Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()))))
                                        {
                                            string[] splitString = strArr[i].Split('_');
                                            if (splitString.Length > 0)
                                            {
                                                int usedFlag = 1;
                                                usedFlag = Convert.ToInt16(splitString[1]);
                                                Flag = usedFlag;
                                                isContain = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (isContain == false)
                                    {
                                        Flag = 1;
                                    }
                                }
                            }

                            oDBParameters.Add("@nUsedStatus", Flag, ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);

                            objectID = new object();
                            oDB.Execute("AS_INSERT_Appointment_DTL", oDBParameters, out  objectID);
                            if (objectID == null)
                            { return 0; }

                            _nDetailAppointmentID = (Int64)objectID;

                            oMasterAppointment.PATransaction.DetailAppointmentID = _nDetailAppointmentID;
                            oMasterAppointment.PATransaction.AppointmentDate = gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString());

                            objectID = null;


                            #region " PA Transaction "

                            if (oMasterAppointment.PATransaction.PriorAuthorizationID != 0)
                            {
                                PriorAuthorizationTransaction.Insert(oMasterAppointment.PATransaction);
                            }

                            #endregion

                            #region "Provider Appointments - Problem Types "
                            for (nPTCntr = 0; nPTCntr <= oMasterAppointment.ProblemTypes.Count - 1; nPTCntr++)
                            {
                                oDBParameters.Clear();
                                oDBParameters.Add("@MSTAppointmentID", _nMasterAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@DTLAppointmentID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@IsSingleRecurrence", oMasterAppointment.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@LineNumber", _AppTempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@AppointmentFlag", oMasterAppointment.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@ASBaseID", oMasterAppointment.ProblemTypes[nPTCntr].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@ASBaseCode", oMasterAppointment.ProblemTypes[nPTCntr].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ASBaseDesc", oMasterAppointment.ProblemTypes[nPTCntr].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ASBaseFlag", oMasterAppointment.ProblemTypes[nPTCntr].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@RefID", _nDetailAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@RefFlag", oMasterAppointment.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@StartDate", gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.ProblemTypes[nPTCntr].StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                                if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                                {
                                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }
                                else
                                {
                                    System.DateTime dtendDate;
                                    decimal dDuration;
                                    dDuration = oMasterAppointment.Duration;
                                    dtendDate = (DateTime)_AppointmentDates.Dates[nAppCntr];
                                    dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterAppointment.StartTime.ToShortTimeString());
                                    dtendDate = dtendDate.AddMinutes((double)dDuration);

                                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.ProblemTypes[nPTCntr].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }

                                oDBParameters.Add("@LocationID", oMasterAppointment.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@LocationName", oMasterAppointment.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@DepartmentID", oMasterAppointment.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@DepartmentName", oMasterAppointment.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ColorCode", oMasterAppointment.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@Notes", "", ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                oDBParameters.Add("@ClinicID", oMasterAppointment.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@ExternalCode", "0", ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ExternalID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@PARequired", oMasterAppointment.PARequired, ParameterDirection.Input, SqlDbType.Bit);

                                isContain = false;
                                Flag = oMasterAppointment.UsedStatus.GetHashCode();
                                if (oMasterAppointment.DatesWithCommaSeparator != String.Empty)
                                {
                                    string[] strArr = oMasterAppointment.DatesWithCommaSeparator.Split(',');
                                    if (strArr.Length > 0)
                                    {
                                        for (int i = 0; i <= strArr.Length - 1; i++)
                                        {
                                            if ((strArr[i]).Contains(Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()))))
                                            {
                                                string[] splitString = strArr[i].Split('_');

                                                if (splitString.Length > 0)
                                                {
                                                    int usedFlag = 1;
                                                    usedFlag = Convert.ToInt16(splitString[1]);
                                                    Flag = usedFlag;
                                                    isContain = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (isContain == false)
                                        {
                                            Flag = 1;
                                        }
                                    }
                                }
                                oDBParameters.Add("@nUsedStatus", Flag, ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDB.Execute("AS_INSERT_Appointment_DTL", oDBParameters, out  objectID);
                            }
                            #endregion

                            #region "Provider Appointments - Resources"

                            for (nResCntr = 0; nResCntr <= oMasterAppointment.Resources.Count - 1; nResCntr++)
                            {
                                oDBParameters.Clear();
                                oDBParameters.Add("@MSTAppointmentID", _nMasterAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@DTLAppointmentID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@IsSingleRecurrence", oMasterAppointment.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@LineNumber", _AppTempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@AppointmentFlag", oMasterAppointment.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@ASBaseID", oMasterAppointment.Resources[nResCntr].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@ASBaseCode", oMasterAppointment.Resources[nResCntr].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ASBaseDesc", oMasterAppointment.Resources[nResCntr].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ASBaseFlag", oMasterAppointment.Resources[nResCntr].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@RefID", _nDetailAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@RefFlag", oMasterAppointment.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@StartDate", gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.Resources[nResCntr].StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                                if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                                {
                                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }
                                else
                                {
                                    System.DateTime dtendDate;
                                    decimal dDuration;
                                    dDuration = oMasterAppointment.Duration;
                                    dtendDate = (DateTime)_AppointmentDates.Dates[nAppCntr];
                                    dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterAppointment.StartTime.ToShortTimeString());
                                    dtendDate = dtendDate.AddMinutes((double)dDuration);

                                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.Resources[nResCntr].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }

                                oDBParameters.Add("@LocationID", oMasterAppointment.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@LocationName", oMasterAppointment.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@DepartmentID", oMasterAppointment.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@DepartmentName", oMasterAppointment.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ColorCode", oMasterAppointment.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@Notes", "", ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                oDBParameters.Add("@ClinicID", oMasterAppointment.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@ExternalCode", "0", ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ExternalID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@PARequired", oMasterAppointment.PARequired, ParameterDirection.Input, SqlDbType.Bit);

                                isContain = false;
                                Flag = oMasterAppointment.UsedStatus.GetHashCode();
                                if (oMasterAppointment.DatesWithCommaSeparator != String.Empty)
                                {
                                    string[] strArr = oMasterAppointment.DatesWithCommaSeparator.Split(',');
                                    if (strArr.Length > 0)
                                    {
                                        for (int i = 0; i <= strArr.Length - 1; i++)
                                        {

                                            if ((strArr[i]).Contains(Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()))))
                                            {
                                                string[] splitString = strArr[i].Split('_');

                                                if (splitString.Length > 0)
                                                {
                                                    int usedFlag = 1;
                                                    usedFlag = Convert.ToInt16(splitString[1]);
                                                    Flag = usedFlag;
                                                    isContain = true;
                                                    break;
                                                }
                                            }

                                        }
                                        if (isContain == false)
                                        {
                                            Flag = 1;
                                        }
                                    }
                                }

                                oDBParameters.Add("@nUsedStatus", Flag, ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDB.Execute("AS_INSERT_Appointment_DTL", oDBParameters, out  objectID);

                            }
                            #endregion

                            #region "Appointment Status"

                            string _sqlQuery = "";
                            DataTable dtAppointment = new DataTable();
                            string sTimeOut = "";
                            if (Flag == ASUsedStatus.CheckOut.GetHashCode())
                            {
                                gloDatabaseLayer.DBLayer oDBAppintment = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                oDBAppintment.Connect(false);
                                _sqlQuery = "SELECT ISNULL(sTimeOut,'') AS sTimeOut FROM PatientTracking  WITH(NOLOCK) WHERE nMstAppointmentid =" + _nPrevMstAppointmentID + " AND nDtlAppointmentid = " + _nPrevDetailAppointmentID + " AND nTrackingStatus=4";
                                oDBAppintment.Retrive_Query(_sqlQuery, out dtAppointment);
                                oDBAppintment.Disconnect();
                                if (dtAppointment != null)
                                {
                                    if (dtAppointment.Rows.Count > 0)
                                    {
                                        sTimeOut = Convert.ToString(dtAppointment.Rows[0]["sTimeOut"]);
                                    }
                                }
                            }

                            if (oMasterAppointment.AppointmentStatusID == 0)
                            {
                                oMasterAppointment.AppointmentStatusID = 1;
                            }

                            oDBParameters.Clear();
                            oDBParameters.Add("@nID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@dtDate", DateTime.Now, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                            oDBParameters.Add("@nPatientID", oMasterAppointment.PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@sTimeIn", DateTime.Now.ToShortTimeString(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 50);
                            oDBParameters.Add("@sLocation", oMasterAppointment.LocationName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 255);
                            oDBParameters.Add("@sStatus", "", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 255);
                            oDBParameters.Add("@sTimeOut", sTimeOut, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 50);
                            oDBParameters.Add("@nTrackingStatus", oMasterAppointment.AppointmentStatusID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                            oDBParameters.Add("@nMSTAppointmentID", _nMasterAppointmentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@nDTLAppointmentID", _nDetailAppointmentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                            oDB.Execute("gsp_INUP_PatientTracking", oDBParameters);
                            if (Flag.GetHashCode() == 4)
                            {
                                _sqlQuery = " UPDATE PatientTracking SET bIsCheckOut = 1 WHERE nDTLAppointmentID = " + _nDetailAppointmentID + " AND nClinicID = " + _ClinicID + " AND nTrackingStatus=4";
                                oDB.Execute_Query(_sqlQuery);
                            }
                            if (Flag.GetHashCode() == 4)
                            {
                                _sqlQuery = " UPDATE PatientTracking SET nMSTAppointmentID = " + _nMasterAppointmentID + " , nDTLAppointmentID = " + _nDetailAppointmentID + " WHERE nDTLAppointmentID = " + _nPrevDetailAppointmentID + " AND nClinicID = " + _ClinicID + " AND nTrackingStatus <> 4 ";
                                oDB.Execute_Query(_sqlQuery);
                            }
                            else if (Flag.GetHashCode() == 3)
                            {
                                _sqlQuery = " UPDATE PatientTracking SET nMSTAppointmentID = " + _nMasterAppointmentID + " , nDTLAppointmentID = " + _nDetailAppointmentID + " WHERE nDTLAppointmentID = " + _nPrevDetailAppointmentID + " AND nClinicID = " + _ClinicID + " AND nTrackingStatus <> 3 ";
                                oDB.Execute_Query(_sqlQuery);
                            }

                            #endregion
                        }
                    }


                    // By Pranit on 15 sep 2011 to check if any entry in AS_Appointment_DTL w.r.t MasterAppointmentID if no record then delete entries from AS_Appointment_MST_Criteria and AS_Appointment_MST Added if else condition if (cntAppID == 0)  Then delete records ELSE Generate HL7 Message Queue 

                    int cntAppID = 0;
                    cntAppID = Convert.ToInt16(oDB.ExecuteScalar_Query("SELECT Count(nMSTAppointmentID) FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE nMSTAppointmentID = " + _nMasterAppointmentID));

                    if (cntAppID == 0) 
                    {
                        oDB.Execute_Query("DELETE FROM dbo.AS_Appointment_MST_Criteria where nMasterAppointmentID = " + _nMasterAppointmentID);
                        oDB.Execute_Query("DELETE FROM dbo.AS_Appointment_MST where nMSTAppointmentID = " + _nMasterAppointmentID);
                    }
                    else
                    {
                        #region "Generate HL7 Message Queue for New Appointment"
                        // Code Start - Added by kanchan on 20091205 for HL7 appointment outbound
                        if (gloHL7.boolSendAppointmentDetails) // (appSettings["GenerateHL7Message"] != null)
                        {
                            if (_nDetailAppointmentID > 0)
                            {
                                String _HL7MessageName = "";
                                if (gloHL7._AppointmentHL7Flag == HL7AppointmentFlag.Add)
                                {
                                    _HL7MessageName = "S12";
                                    gloHL7.InsertInMessageQueue(_HL7MessageName, oMasterAppointment.PatientID, _nMasterAppointmentID, "", _databaseconnectionstring);
                                }

                                if (gloHL7._AppointmentHL7Flag == HL7AppointmentFlag.Update)
                                {

                                    if (oMasterAppointment.IsRecurrence == SingleRecurrence.SingleInRecurrence)
                                    {
                                        gloHL7.InsertInMessageQueue("S12", oMasterAppointment.PatientID, _nMasterAppointmentID, _nDetailAppointmentID.ToString(), _databaseconnectionstring);
                                    }
                                    else
                                    {
                                        if (gloHL7.nOldBaseId != 0 && oMasterAppointment.ASBaseID == 0)
                                        { //it means provider removed from appointment so make delete entry
                                            _HL7MessageName = "S15";
                                            gloHL7.InsertInMessageQueue(_HL7MessageName, oMasterAppointment.PatientID, _nMasterAppointmentID, "", _databaseconnectionstring, true);
                                        }
                                        else
                                        {
                                            _HL7MessageName = "S13";
                                            gloHL7.InsertInMessageQueue(_HL7MessageName, oMasterAppointment.PatientID, _nMasterAppointmentID, "", _databaseconnectionstring);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion
                }

                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                _nMasterAppointmentID = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _nMasterAppointmentID = 0;
            }
            finally
            {
                oDB.Dispose();
                oDBParameters.Dispose();
                objectID = null;
                _AppointmentDates.Dispose();
            }
            _MasterAppointmentIDForAuthorization = _nMasterAppointmentID;
            _DetailAppointmentIDForAuthorization = _nDetailAppointmentID;
            return _nMasterAppointmentID;
        }

        public DataTable GetAlert(string PatientCode, Int16 AlertType, Int16 AlertStatus)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = new DataTable();
            try
            {
                oDBParameters.Add("@PatientCode", PatientCode, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@AlertType", AlertType, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@Status", AlertStatus, ParameterDirection.Input, SqlDbType.Int);

                oDB.Connect(false);

                oDB.Retrive("gsp_ScanPatientAlert", oDBParameters, out dt);

                oDB.Disconnect();

                oDBParameters.Dispose();
                oDBParameters = null;

                oDB.Dispose();
                oDB = null;
                return dt;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                dt.Dispose();
                dt = null;
            }
        }

        public bool ResourceBlockedSlots(Int64 ProviderID, DateTime StartTime, DateTime EndTime, DateTime AppoinmentDate, String Location = "")
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            TimeSpan ts = EndTime - StartTime;
            decimal duration = (decimal)ts.TotalMinutes;

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            string DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            Int64 _clinicID = 1;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _clinicID = Convert.ToInt64(appSettings["ClinicID"]);
                }
                else
                {
                    _clinicID = 1;
                }
            }
            else
            { _clinicID = 1; }

            try
            {
                oDB.Connect(false);


                DateTime dateTime = new DateTime();
                string splitDate = AppoinmentDate.ToShortDateString() + " " + StartTime.ToShortTimeString();
                dateTime = Convert.ToDateTime(splitDate);



                DateTime newDateTime = new DateTime();
                newDateTime = dateTime.AddMinutes((int)duration);

                oDBParameters.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(AppoinmentDate.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(newDateTime.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@ClinicId", _clinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@Flag", 2, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@LocationName", Location, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                object _result = 0;
                _result = oDB.ExecuteScalar("AS_AppointmentBlockedSlots", oDBParameters);

                if (_result != null && Convert.ToString(_result) != "")
                {
                    if (Convert.ToInt32(_result) > 0)
                    {
                        return true;
                    }
                }
                oDB.Disconnect();

                oDBParameters.Dispose();
                oDBParameters = null;

                oDB.Dispose();
                oDB = null;

            }
            catch (gloDatabaseLayer.DBException odbEx)
            {
                odbEx.ERROR_Log(odbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            return false;
        }

        public DataTable Appointmentdatetime(Int64 nmstAppointmentid)
        {
            DataTable dt = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            string DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            Int64 _clinicID = 1;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _clinicID = Convert.ToInt64(appSettings["ClinicID"]);
                }
                else
                {
                    _clinicID = 1;
                }
            }
            else
            { _clinicID = 1; }

            String _sqlQuery = "";
            try
            {
                oDB.Connect(false);

                _sqlQuery = " SELECT dtStartDate,dbo.Convert_To_Time(dtStartTime) As dtStartTime,dbo.Convert_To_Time(dtEndTime) As dtEndTime"
                                 + " FROM AS_Appointment_MST  WITH(NOLOCK) "
                                 + " WHERE nMSTAppointmentID = " + nmstAppointmentid + " AND nClinicId = " + _clinicID;

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();

                oDBParameters.Dispose();
                oDBParameters = null;

                oDB.Dispose();
                oDB = null;

                return dt;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                dt.Dispose();
                dt = null;
            }

        }

        public DataTable ResourseName(Int64 ProviderID, DateTime StartTime, DateTime EndTime, DateTime AppoinmentDate, String Location = "")
        {
            DataTable dt = new DataTable();

            TimeSpan ts = EndTime - StartTime;
            decimal duration = (decimal)ts.TotalMinutes;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            string DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            Int64 _clinicID = 1;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _clinicID = Convert.ToInt64(appSettings["ClinicID"]);
                }
                else
                {
                    _clinicID = 1;
                }
            }
            else
            { _clinicID = 1; }

            try
            {
                oDB.Connect(false);


                DateTime dateTime = new DateTime();
                string splitDate = AppoinmentDate.ToShortDateString() + " " + StartTime.ToShortTimeString();
                dateTime = Convert.ToDateTime(splitDate);


                DateTime newDateTime = new DateTime();
                newDateTime = dateTime.AddMinutes((int)duration);

                oDBParameters.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(AppoinmentDate.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(newDateTime.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@ClinicId", _clinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@Flag", 2, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@LocationName", Location, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("AS_BlockedSlots", oDBParameters, out  dt);
                oDB.Disconnect();

                oDBParameters.Dispose();
                oDBParameters = null;

                oDB.Dispose();
                oDB = null;

                return dt;

            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                dt.Dispose();
                dt = null;
            }
        }

        public bool BlockedSlots(Int64 ProviderID, DateTime StartTime, DateTime EndTime, DateTime Startdate, String Location="")
        {
            TimeSpan ts = EndTime - StartTime;
            decimal duration = (decimal)ts.TotalMinutes;


            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            bool _isblocked = false;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            string DatabaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            Int64 _clinicID = 1;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _clinicID = Convert.ToInt64(appSettings["ClinicID"]);
                }
                else
                {
                    _clinicID = 1;
                }
            }
            else
            { _clinicID = 1; }

            try
            {
                oDB.Connect(false);


                DateTime dateTime = new DateTime();
                string splitDate = Startdate.ToShortDateString() + " " + StartTime.ToShortTimeString();
                dateTime = Convert.ToDateTime(splitDate);




                DateTime newDateTime = new DateTime();
                newDateTime = dateTime.AddMinutes((int)duration);



                oDBParameters.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(Startdate.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(newDateTime.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@ClinicId", _clinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@Flag", 1, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@LocationName", Location, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                object _result = 0;
                _result = oDB.ExecuteScalar("AS_AppointmentBlockedSlots", oDBParameters);
                oDB.Disconnect();

                oDBParameters.Dispose();
                oDBParameters = null;

                oDB.Dispose();
                oDB = null;

                if (_result != null && Convert.ToString(_result) != "")
                {
                    if (Convert.ToInt32(_result) > 0)
                    {
                        _isblocked = true;
                    }
                }
            }
            catch (gloDatabaseLayer.DBException odbEx)
            {
                odbEx.ERROR_Log(odbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            return _isblocked;
        }
        public string GetAppointmentPatientName(Int64 mstAppointmentId)
        {
            DataTable dtPatient = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _strSQL = "";
            string _result = "";
            string firstName = "", midName = "", lastName = "";
            try
            {
                oDB.Connect(false);

                //get the provider details in the datatable -- dtProvider
                _strSQL = "SELECT Patient.sFirstName, Patient.sMiddleName, Patient.sLastName from dbo.AS_Appointment_MST  WITH(NOLOCK) "
                           + "inner join patient  WITH(NOLOCK) on AS_Appointment_MST.nPatientId = patient.nPatientId "
                            + "where AS_Appointment_MST.nMSTAppointmentId = " + mstAppointmentId;
                oDB.Retrive_Query(_strSQL, out dtPatient);

                if (dtPatient.Rows.Count > 0 && dtPatient != null)
                {
                    if (dtPatient.Rows[0]["sFirstName"] != null)
                        firstName = dtPatient.Rows[0]["sFirstName"].ToString();

                    if (dtPatient.Rows[0]["sMiddleName"] != null)
                        midName = dtPatient.Rows[0]["sMiddleName"].ToString();

                    if (dtPatient.Rows[0]["sLastName"] != null)
                        lastName = dtPatient.Rows[0]["sLastName"].ToString();
                }
                _result = firstName + " " + midName + " " + lastName;

                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                dtPatient.Dispose();
                dtPatient = null;

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ToString();
                DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }

            return _result;
        }


        public DataTable PatientIMAlerts(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = new DataTable();

            try
            {
                oDB.Connect(false);
                Collection col = new Collection();

                oDBParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gIM_PatientIMAlerts", oDBParameters, out dt);


                oDB.Disconnect();

                oDBParameters.Dispose();
                oDBParameters = null;

                oDB.Dispose();
                oDB = null;

                return dt;

            }

            catch (gloDatabaseLayer.DBException odbEx)
            {
                odbEx.ERROR_Log(odbEx.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return null;
            }
            finally
            {
                dt.Dispose();
                dt = null;
            }

        }

        public DataTable PatientAlerts(Int64 nPatientID, String PatientCode)
        {
            DataTable dt = new DataTable();
            try
            {
                String strAlert = "";
                string strIMAlert = "";
                DataTable dtIM;
                DataTable dtAlert = new DataTable();
                Int32 _AlertCounter;
                string strPatientDirectivesAlert = "";

                _AlertCounter = 0;
                if (string.IsNullOrEmpty(strAlert))
                {
                    dtAlert = GetAlert(PatientCode, 1, 1);
                }

                if (dtAlert.Rows.Count > 0)
                {
                    if (dtAlert.Rows[0][1].ToString().Trim() == "1")
                    {
                        strAlert = "" + dtAlert.Rows[0][0].ToString().Trim();
                        _AlertCounter = _AlertCounter + 1;
                    }
                }

                dtIM = PatientIMAlerts(nPatientID);

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

              //  Int32 _HasDirective;
                string _strSQL;
                DataTable dtPatientDirective;
                oDB.Connect(false);
                _strSQL = "SELECT nPatientDirective FROM patient  WITH(NOLOCK) WHERE npatientid = " + nPatientID + " ";

                oDB.Retrive_Query(_strSQL, out dtPatientDirective);
                if (dtPatientDirective.Rows[0][0].ToString() != "0")
                {
                    strPatientDirectivesAlert = "This Patient has Advance directive.";
                }

                dt.Columns.Add("Alerts");
                dt.Columns.Add("Value");
                if (strAlert != "")
                {
                    dt.Rows.Add();
                    dt.Rows[0][0] = "Patient Alert";
                    dt.Rows[0][1] = strAlert;
                }

                if (dtIM.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtIM.Rows.Count - 1; i++)
                    {
                        //strIMAlert = "" + dtIM.Rows[i][0].ToString().Trim() + " - " + dtIM.Rows[i][1].ToString().Trim() + " due on " + Convert.ToDateTime(dtIM.Rows[i][2]).ToShortDateString();
                        strIMAlert = "" + dtIM.Rows[i]["im_item_name"].ToString().Trim() + " due on " + Convert.ToDateTime(dtIM.Rows[i]["im_trn_duedate"]).ToShortDateString();
                        _AlertCounter = _AlertCounter + 1;
                        dt.Rows.Add();
                        dt.Rows[dt.Rows.Count - 1][0] = "IM Alert(s)";
                        dt.Rows[dt.Rows.Count - 1][1] = strIMAlert;
                    }
                }
                if (strPatientDirectivesAlert != "")
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1][0] = "Advance Directive";
                    dt.Rows[dt.Rows.Count - 1][1] = strPatientDirectivesAlert.Trim();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        public Int64 Add(MasterAppointment oMasterAppointment, AppointmentScheduleFlag IsTemplateAppointment, Int64 TemplateAllocationMasterID, Int64 TemplateAllocationID, Int64 AppointmentLineNo)
        {
            Int64 _nMasterAppointmentID = 0;
            Int64 _nDetailAppointmentID = 0;
            Int64 _nPrevDetailAppointmentID = 0;

            Int64 _nPrevMstAppointmentID = 0;
            string _AppTypeCode = "0";
            string _ProviderCode = "0";
            string _ReferralProviderCode = "0";
            DataTable dt = new DataTable();


            int nAppCntr = 0;
            int nPTCntr = 0;
            int nResCntr = 0;
            Int64 _AppTempLineNumber = 0;
            Object objectID;


            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            gloAppointmentScheduling.Criteria.FindRecurrences _AppointmentDates = new gloAppointmentScheduling.Criteria.FindRecurrences();
            try
            {
                oDB.Connect(false);
                _AppointmentDates = _AppointmentDates.GetRecurrence(oMasterAppointment.Criteria, oMasterAppointment.ASBaseID, oMasterAppointment.StartTime, oMasterAppointment.EndTime, oMasterAppointment.MasterID, oMasterAppointment.ResourceIDS);

                bool isResourcesBlocked = false;
                bool isProviderBlocked = false;


                if ((_AppointmentDates.Dates.Count > 0) && isResourcesBlocked == false && isProviderBlocked == false)
                {
                    #region "Master Appointment"
                    oDBParameters.Add("@MSTAppointmentID", oMasterAppointment.MasterID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                    oDBParameters.Add("@IsSingleRecurrence", oMasterAppointment.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@AppointmentFlag", oMasterAppointment.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@AppointmentTypeID", oMasterAppointment.AppointmentTypeID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@AppointmentTypeCode", _AppTypeCode, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@AppointmentTypeDesc", oMasterAppointment.AppointmentTypeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@ASBaseID", oMasterAppointment.ASBaseID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@ASBaseCode", _ProviderCode, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@ASBaseDesc", oMasterAppointment.ASBaseDescription, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@ASBaseFlag", oMasterAppointment.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@ReferralProviderID", oMasterAppointment.ReferralProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@ReferralProviderCode", _ReferralProviderCode, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@ReferralProviderName", oMasterAppointment.ReferralProviderName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@PatientID", oMasterAppointment.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@StartDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.StartDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@Duration", oMasterAppointment.Duration, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@ColorCode", oMasterAppointment.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@LocationID", oMasterAppointment.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@LocationName", oMasterAppointment.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@DepartmentID", oMasterAppointment.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@DepartmentName", oMasterAppointment.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Execute("AS_INSERT_Appointment_MST", oDBParameters, out  objectID);
                    _nPrevMstAppointmentID = oMasterAppointment.PATransaction.MasterAppointmentID;
                    _nPrevDetailAppointmentID = oMasterAppointment.PATransaction.DetailAppointmentID;

                    if (objectID == null)
                    { return 0; }

                    _nMasterAppointmentID = (Int64)objectID;

                    oMasterAppointment.PATransaction.MasterAppointmentID = _nMasterAppointmentID;

                    #endregion

                    #region "Criteria "

                    //Delete All old Detail Records
                    oDB.Execute_Query("DELETE FROM AS_Appointment_MST_Criteria WHERE nMasterAppointmentID = " + _nMasterAppointmentID);

                    //Insert Into Criteria Table.   dbo.AS_Schedule_MST_Criteria
                    //nMasterScheduleID, bIsSingleRecurrence, nRecurrence_PatternType, nRecurrence_Pattern_Daily_EveryDayNo, 
                    if (oMasterAppointment.Criteria.SingleRecurrenceAppointment == SingleRecurrence.Recurrence)
                    {
                        oDBParameters.Clear();
                        oDBParameters = new gloDatabaseLayer.DBParameters();
                        oDBParameters.Add("@nMasterAppointmentID", _nMasterAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@bIsSingleRecurrence", oMasterAppointment.Criteria.SingleRecurrenceAppointment.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_PatternType", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType.GetHashCode(), System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Daily_EveryDayNo", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber, ParameterDirection.Input, SqlDbType.BigInt);

                        //nRecurrence_Pattern_Daily_EveryDayOrWeekDay, nRecurrence_Pattern_Weekly_EveryWeekNo, bRecurrence_Pattern_Weekly_Sunday, 
                        oDBParameters.Add("@nRecurrence_Pattern_Daily_EveryDayOrWeekDay", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Weekly_EveryWeekNo", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Sunday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday, ParameterDirection.Input, SqlDbType.Bit);

                        //bRecurrence_Pattern_Weekly_Monday, bRecurrence_Pattern_Weekly_Tuesday, bRecurrence_Pattern_Weekly_Wednesday, 
                        //bRecurrence_Pattern_Weekly_Thursday, bRecurrence_Pattern_Weekly_Friday, bRecurrence_Pattern_Weekly_Saturday, 
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Monday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Tuesday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Wednesday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Thursday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Friday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday, ParameterDirection.Input, SqlDbType.Bit);
                        oDBParameters.Add("@bRecurrence_Pattern_Weekly_Saturday", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday, ParameterDirection.Input, SqlDbType.Bit);

                        //nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria, nRecurrence_Pattern_Monthly_DayNumber, nRecurrence_Pattern_Monthly_EveryMonthNumber, 
                        //nRecurrence_Pattern_Monthly_FstLstCriteriaID, nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID, nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria,
                        oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayNumber", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Monthly_EveryMonthNumber", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                        oDBParameters.Add("@nRecurrence_Pattern_Monthly_FstLstCriteriaID", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                        //nRecurrence_Pattern_Yearly_DayNumber, nRecurrence_Pattern_Yearly_MonthOfCriteriaID, nRecurrence_Pattern_Yearly_FstLstCriteriaID,
                        //nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID, nClinicID
                        oDBParameters.Add("@nRecurrence_Pattern_Yearly_DayNumber", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Yearly_MonthOfCriteriaID", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Yearly_FstLstCriteriaID", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID", oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                        oDBParameters.Add("@bRange_Flag", oMasterAppointment.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_StartDate", oMasterAppointment.Criteria.RecurrenceCriteria.Range.StartDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.StartTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_EndDate", oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_Duration", Convert.ToInt32(((TimeSpan)oMasterAppointment.EndTime.Subtract(oMasterAppointment.StartTime)).TotalMinutes), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_NoOfOccurence", oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nRange_NoEndDateYear", oMasterAppointment.Criteria.RecurrenceCriteria.Range.NoEndDateYear, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                        oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                        oDB.Execute("AS_INSERT_Appointment_MST_Criteria", oDBParameters);
                    }
                    #endregion "Criteria "

                    //Delete All old Detail Records
                    oDB.Execute_Query("DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + _nMasterAppointmentID);


                    int Flag = 0;
                    bool isContain = false;
   
                    //Delete all PA Transaction for this master entry
                    PriorAuthorizationTransaction.Delete(_nMasterAppointmentID, 0);

                    //Find Dates for multiple appointment
                    #region "Provider Appointments"

                    for (nAppCntr = 0; nAppCntr <= _AppointmentDates.Dates.Count - 1; nAppCntr++)
                    {

                        // By Pranit on 14 sep to not allow entry in Database whose status is Blocked By Pranit on 15 sep Added or condition to check is it single recurrence.

                        if ((_AppointmentDates.ScheduleStatus[nAppCntr].ToString() != "Blocked") || (oMasterAppointment.IsRecurrence == SingleRecurrence.Single) || (oMasterAppointment.AllowRecurrenceToOverRideBlockeAppointment == true))
                        {
                            oDBParameters.Clear();
                            oDBParameters.Add("@MSTAppointmentID", _nMasterAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@DTLAppointmentID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            oDBParameters.Add("@IsSingleRecurrence", oMasterAppointment.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@LineNumber", AppointmentLineNo, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@AppointmentFlag", oMasterAppointment.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@ASBaseID", oMasterAppointment.ASBaseID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@ASBaseCode", oMasterAppointment.ASBaseCode, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@ASBaseDesc", oMasterAppointment.ASBaseDescription, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@ASBaseFlag", oMasterAppointment.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@RefID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@RefFlag", 0, ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@StartDate", gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                            if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                            {
                                oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            }
                            else
                            {
                                System.DateTime dtendDate;
                                decimal dDuration;
                                dDuration = oMasterAppointment.Duration;
                                dtendDate = (DateTime)_AppointmentDates.Dates[nAppCntr];
                                dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterAppointment.StartTime.ToShortTimeString());
                                dtendDate = dtendDate.AddMinutes((double)dDuration);

                                oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            }
                            oDBParameters.Add("@LocationID", oMasterAppointment.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@LocationName", oMasterAppointment.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@DepartmentID", oMasterAppointment.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@DepartmentName", oMasterAppointment.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@ColorCode", oMasterAppointment.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@Notes", oMasterAppointment.Notes, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@ClinicID", oMasterAppointment.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@ExternalCode", "0", ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@ExternalID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@PARequired", oMasterAppointment.PARequired, ParameterDirection.Input, SqlDbType.Bit);

                            objectID = new object();

                            isContain = false;
                            Flag = oMasterAppointment.UsedStatus.GetHashCode();
                            if (oMasterAppointment.DatesWithCommaSeparator != String.Empty)
                            {
                                string[] strArr = oMasterAppointment.DatesWithCommaSeparator.Split(',');
                                if (strArr.Length > 0)
                                {
                                    for (int i = 0; i <= strArr.Length - 1; i++)
                                    {
                                        if ((strArr[i]).Contains(Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()))))
                                        {
                                            string[] splitString = strArr[i].Split('_');

                                            if (splitString.Length > 0)
                                            {
                                                int usedFlag = 1;
                                                usedFlag = Convert.ToInt16(splitString[1]);
                                                Flag = usedFlag;
                                                isContain = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (isContain == false)
                                    {
                                        Flag = 1;
                                    }
                                }
                            }
                            oDBParameters.Add("@nUsedStatus", Flag, ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nTemplateAllocationID", TemplateAllocationID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nTemplateAllocationMasterID", TemplateAllocationMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDB.Execute("AS_INSERT_Appointment_DTL", oDBParameters, out  objectID);


                            if (objectID == null)
                            { return 0; }
                            _nDetailAppointmentID = (Int64)objectID;

                            oMasterAppointment.PATransaction.DetailAppointmentID = _nDetailAppointmentID;
                            oMasterAppointment.PATransaction.AppointmentDate = gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString());

                            objectID = null;


                            #region " PA Transaction "

                            if (oMasterAppointment.PATransaction.PriorAuthorizationID != 0)
                            {
                                PriorAuthorizationTransaction.Insert(oMasterAppointment.PATransaction);
                            }

                            #endregion

                            #region "Provider Appointments - Problem Types "
                            for (nPTCntr = 0; nPTCntr <= oMasterAppointment.ProblemTypes.Count - 1; nPTCntr++)
                            {
                                oDBParameters.Clear();
                                oDBParameters.Add("@MSTAppointmentID", _nMasterAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@DTLAppointmentID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@IsSingleRecurrence", oMasterAppointment.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@LineNumber", _AppTempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@AppointmentFlag", oMasterAppointment.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@ASBaseID", oMasterAppointment.ProblemTypes[nPTCntr].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@ASBaseCode", oMasterAppointment.ProblemTypes[nPTCntr].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ASBaseDesc", oMasterAppointment.ProblemTypes[nPTCntr].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ASBaseFlag", oMasterAppointment.ProblemTypes[nPTCntr].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@RefID", _nDetailAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@RefFlag", oMasterAppointment.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@StartDate", gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.ProblemTypes[nPTCntr].StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                                if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                                {
                                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }
                                else
                                {
                                    System.DateTime dtendDate;
                                    decimal dDuration;
                                    dDuration = oMasterAppointment.Duration;
                                    dtendDate = (DateTime)_AppointmentDates.Dates[nAppCntr];
                                    dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterAppointment.StartTime.ToShortTimeString());
                                    dtendDate = dtendDate.AddMinutes((double)dDuration);

                                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.ProblemTypes[nPTCntr].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }

                                oDBParameters.Add("@LocationID", oMasterAppointment.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@LocationName", oMasterAppointment.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@DepartmentID", oMasterAppointment.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@DepartmentName", oMasterAppointment.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ColorCode", oMasterAppointment.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@Notes", "", ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                oDBParameters.Add("@ClinicID", oMasterAppointment.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@ExternalCode", "0", ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ExternalID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@PARequired", oMasterAppointment.PARequired, ParameterDirection.Input, SqlDbType.Bit);

                                isContain = false;
                                Flag = oMasterAppointment.UsedStatus.GetHashCode();
                                if (oMasterAppointment.DatesWithCommaSeparator != String.Empty)
                                {
                                    string[] strArr = oMasterAppointment.DatesWithCommaSeparator.Split(',');
                                    if (strArr.Length > 0)
                                    {
                                        for (int i = 0; i <= strArr.Length - 1; i++)
                                        {
                                            if ((strArr[i]).Contains(Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()))))
                                            {
                                                string[] splitString = strArr[i].Split('_');

                                                if (splitString.Length > 0)
                                                {
                                                    int usedFlag = 1;
                                                    usedFlag = Convert.ToInt16(splitString[1]);
                                                    Flag = usedFlag;
                                                    isContain = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (isContain == false)
                                        {
                                            Flag = 1;
                                        }
                                    }
                                }
                                oDBParameters.Add("@nUsedStatus", Flag, ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nTemplateAllocationID", TemplateAllocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nTemplateAllocationMasterID", TemplateAllocationMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDB.Execute("AS_INSERT_Appointment_DTL", oDBParameters, out  objectID);
                            }
                            #endregion

                            #region "Provider Appointments - Resources"

                            for (nResCntr = 0; nResCntr <= oMasterAppointment.Resources.Count - 1; nResCntr++)
                            {
                                oDBParameters.Clear();
                                oDBParameters.Add("@MSTAppointmentID", _nMasterAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@DTLAppointmentID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@IsSingleRecurrence", oMasterAppointment.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@LineNumber", _AppTempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@AppointmentFlag", oMasterAppointment.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@ASBaseID", oMasterAppointment.Resources[nResCntr].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@ASBaseCode", oMasterAppointment.Resources[nResCntr].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ASBaseDesc", oMasterAppointment.Resources[nResCntr].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ASBaseFlag", oMasterAppointment.Resources[nResCntr].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@RefID", _nDetailAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@RefFlag", oMasterAppointment.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@StartDate", gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.Resources[nResCntr].StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                                if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                                {
                                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }
                                else
                                {
                                    System.DateTime dtendDate;
                                    decimal dDuration;
                                    dDuration = oMasterAppointment.Duration;
                                    dtendDate = (DateTime)_AppointmentDates.Dates[nAppCntr];
                                    dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterAppointment.StartTime.ToShortTimeString());
                                    dtendDate = dtendDate.AddMinutes((double)dDuration);

                                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.Resources[nResCntr].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }

                                oDBParameters.Add("@LocationID", oMasterAppointment.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@LocationName", oMasterAppointment.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@DepartmentID", oMasterAppointment.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@DepartmentName", oMasterAppointment.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ColorCode", oMasterAppointment.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@Notes", "", ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                oDBParameters.Add("@ClinicID", oMasterAppointment.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@ExternalCode", "0", ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ExternalID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@PARequired", oMasterAppointment.PARequired, ParameterDirection.Input, SqlDbType.Bit);

                                isContain = false;
                                Flag = oMasterAppointment.UsedStatus.GetHashCode();
                                if (oMasterAppointment.DatesWithCommaSeparator != String.Empty)
                                {
                                    string[] strArr = oMasterAppointment.DatesWithCommaSeparator.Split(',');
                                    if (strArr.Length > 0)
                                    {
                                        for (int i = 0; i <= strArr.Length - 1; i++)
                                        {
                                            if ((strArr[i]).Contains(Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()))))
                                            {
                                                string[] splitString = strArr[i].Split('_');

                                                if (splitString.Length > 0)
                                                {
                                                    int usedFlag = 1;
                                                    usedFlag = Convert.ToInt16(splitString[1]);
                                                    Flag = usedFlag;
                                                    isContain = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (isContain == false)
                                        {
                                            Flag = 1;
                                        }
                                    }
                                }

                                oDBParameters.Add("@nUsedStatus", Flag, ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nTemplateAllocationID", TemplateAllocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nTemplateAllocationMasterID", TemplateAllocationMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDB.Execute("AS_INSERT_Appointment_DTL", oDBParameters, out  objectID);
                            }
                            #endregion

                            #region "Appointment Status"

                            string _sqlQuery = "";
                            DataTable dtAppointment = new DataTable();
                            string sTimeOut = "";
                            if (Flag == ASUsedStatus.CheckOut.GetHashCode())
                            {
                                gloDatabaseLayer.DBLayer oDBAppintment = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                oDBAppintment.Connect(false);
                                _sqlQuery = "SELECT ISNULL(sTimeOut,'') AS sTimeOut FROM PatientTracking  WITH(NOLOCK) WHERE nMstAppointmentid =" + _nPrevMstAppointmentID + " AND nDtlAppointmentid = " + _nPrevDetailAppointmentID + " AND nTrackingStatus=4";
                                oDBAppintment.Retrive_Query(_sqlQuery, out dtAppointment);
                                oDBAppintment.Disconnect();
                                if (dtAppointment != null)
                                {
                                    if (dtAppointment.Rows.Count > 0)
                                    {
                                        sTimeOut = Convert.ToString(dtAppointment.Rows[0]["sTimeOut"]);
                                    }
                                }
                            }

                            if (oMasterAppointment.AppointmentStatusID == 0)
                            {
                                oMasterAppointment.AppointmentStatusID = 1;
                            }

                            oDBParameters.Clear();
                            oDBParameters.Add("@nID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@dtDate", DateTime.Now, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                            oDBParameters.Add("@nPatientID", oMasterAppointment.PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@sTimeIn", DateTime.Now.ToShortTimeString(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 50);
                            oDBParameters.Add("@sLocation", oMasterAppointment.LocationName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 255);
                            oDBParameters.Add("@sStatus", "", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 255);
                            oDBParameters.Add("@sTimeOut", sTimeOut, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 50);
                            oDBParameters.Add("@nTrackingStatus", oMasterAppointment.AppointmentStatusID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                            oDBParameters.Add("@nMSTAppointmentID", _nMasterAppointmentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@nDTLAppointmentID", _nDetailAppointmentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                            oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                            oDB.Execute("gsp_INUP_PatientTracking", oDBParameters);
                            if (Flag.GetHashCode() == 4)
                            {
                                _sqlQuery = " UPDATE PatientTracking SET bIsCheckOut = 1 WHERE nDTLAppointmentID = " + _nDetailAppointmentID + " AND nClinicID = " + _ClinicID + " AND nTrackingStatus=4";
                                oDB.Execute_Query(_sqlQuery);
                            }
                            if (Flag.GetHashCode() == 4)
                            {
                                _sqlQuery = " UPDATE PatientTracking SET nMSTAppointmentID = " + _nMasterAppointmentID + " , nDTLAppointmentID = " + _nDetailAppointmentID + " WHERE nDTLAppointmentID = " + _nPrevDetailAppointmentID + " AND nClinicID = " + _ClinicID + " AND nTrackingStatus <> 4 ";
                                oDB.Execute_Query(_sqlQuery);
                            }
                            else if (Flag.GetHashCode() == 3)
                            {
                                _sqlQuery = " UPDATE PatientTracking SET nMSTAppointmentID = " + _nMasterAppointmentID + " , nDTLAppointmentID = " + _nDetailAppointmentID + " WHERE nDTLAppointmentID = " + _nPrevDetailAppointmentID + " AND nClinicID = " + _ClinicID + " AND nTrackingStatus <> 3 ";
                                oDB.Execute_Query(_sqlQuery);
                            }
                            #endregion
                        }
                    }


                    //By Pranit on 15 sep 2011 to check if any entry in AS_Appointment_DTL w.r.t MasterAppointmentID if no record then delete entries from AS_Appointment_MST_Criteria and AS_Appointment_MST Added if else condition if (cntAppID == 0)  Then delete records ELSE Generate HL7 Message Queue

                    int cntAppID = 0;
                    cntAppID = Convert.ToInt16(oDB.ExecuteScalar_Query("SELECT Count(nMSTAppointmentID) FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE nMSTAppointmentID = " + _nMasterAppointmentID));

                    if (cntAppID == 0)
                    {
                        oDB.Execute_Query("DELETE FROM dbo.AS_Appointment_MST_Criteria where nMasterAppointmentID = " + _nMasterAppointmentID);
                        oDB.Execute_Query("DELETE FROM dbo.AS_Appointment_MST where nMSTAppointmentID = " + _nMasterAppointmentID);
                    }
                    else 
                    {
                        #region "Generate HL7 Message Queue for New Appointment"
                        // Code Start - Added by kanchan on 20091205 for HL7 appointment outbound
                        if (gloHL7.boolSendAppointmentDetails) //(appSettings["GenerateHL7Message"] != null)
                        {
                            if (_nDetailAppointmentID > 0)
                            {
                                String _HL7MessageName = "";
                                if (gloHL7._AppointmentHL7Flag == HL7AppointmentFlag.Add)
                                {
                                    _HL7MessageName = "S12";
                                    gloHL7.InsertInMessageQueue(_HL7MessageName, oMasterAppointment.PatientID, _nMasterAppointmentID, "", _databaseconnectionstring);
                                }
                                if (gloHL7._AppointmentHL7Flag == HL7AppointmentFlag.Update)
                                {
                                    if (gloHL7.nOldBaseId != 0 && oMasterAppointment.ASBaseID == 0)
                                    { //it means provider removed from appointment so make delete entry
                                        _HL7MessageName = "S15";
                                        gloHL7.InsertInMessageQueue(_HL7MessageName, oMasterAppointment.PatientID, _nMasterAppointmentID, "", _databaseconnectionstring, true);
                                    }
                                    else
                                    {
                                        _HL7MessageName = "S13";
                                        gloHL7.InsertInMessageQueue(_HL7MessageName, oMasterAppointment.PatientID, _nMasterAppointmentID, "", _databaseconnectionstring);
                                    }
                                }
                            }
                        }
                        // Code End - Added by kanchan on 20091205 for HL7 appointment outbound
                        #endregion

                    }
                    #endregion

                    //Register Template Appointment
                    oDB.Execute_Query("UPDATE AB_AppointmentTemplate_Allocation SET nIsRegistered = 1 WHERE nTemplateAllocationMasterID = " + TemplateAllocationMasterID + " AND nTemplateAllocationID = " + TemplateAllocationID + " and (nIsRegistered = 0 or nIsRegistered is null) ");
                    //oDB.Execute_Query("UPDATE AS_Appointment_DTL SET  AS_Appointment_DTL.nTemplateAllocationID=" + TemplateAllocationID + ",AS_Appointment_DTL.nTemplateAllocationMasterID=" + TemplateAllocationMasterID + " WHERE nDTLAppointmentID=" + _nDetailAppointmentID);
                }

                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                _nMasterAppointmentID = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _nMasterAppointmentID = 0;
            }
            finally
            {
                oDB.Dispose();
                oDBParameters.Dispose();
                objectID = null;
                _AppointmentDates.Dispose();
            }
            _MasterAppointmentIDForAuthorization = _nMasterAppointmentID;
            _DetailAppointmentIDForAuthorization = _nDetailAppointmentID;
            return _nMasterAppointmentID;
        }

        //Added by Amit to solve recurrence Appointment save multiple times when click on Print button

        public Int64 Modify(MasterAppointment oMasterAppointment, Int64 MasterAppointmentID, Int64 AppointmentID, Int64 ClinicID, SingleRecurrence ModifyMethod, SingleRecurrence ModifyMasterMethod, bool ModifySingleInRecurrence, ASUpdateCriteria UpdateCriteria, ArrayList DontDeleteAppointments)
        {
            bool _result = false;
            Int64 _intResult = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            int _AppCntr = 0;
            string _strSQL = "";
            object _sqlresult = null;

            try
            {
                oDB.Connect(false);
                if (ModifyMethod == SingleRecurrence.Single || ModifyMethod == SingleRecurrence.SingleInRecurrence)
                {
                    Int64 _OldProviderID = 0;
                    Int64 _OldPAID = 0;

                    #region "Read Old Provider ID"

                    _sqlresult = new object();
                    string str = "SELECT nASBaseID FROM AS_Appointment_MST  WITH(NOLOCK) WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " ";
                    _sqlresult = oDB.ExecuteScalar_Query(str);

                    if (_sqlresult != null)
                    {
                        if (_sqlresult.ToString() != null)
                        {
                            if (_sqlresult.ToString() != "")
                            {
                                _OldProviderID = Convert.ToInt64(_sqlresult.ToString());
                            }
                        }
                    }
                    _sqlresult = null;

                    #endregion

                    #region "Read Old PA ID"

                    DataRow drPA = clsgloPriorAuthorization.GetPriorAuthorizationInfo(MasterAppointmentID, AppointmentID);
                    if (drPA != null)
                    {
                        _OldPAID = Convert.ToInt64(drPA["nPriorAuthorizationID"]);
                    }

                    #endregion

                    if (ModifyMasterMethod == SingleRecurrence.Recurrence)
                    {
                        oMasterAppointment.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;

                        bool _isProviderChanged = false;

                        if (_OldProviderID != oMasterAppointment.ASBaseID)
                        { _isProviderChanged = true; }
                        else
                        { _isProviderChanged = false; }

                        bool _isPAChanged = false;
                        if (oMasterAppointment.PATransaction != null)
                        {
                            if (_OldPAID != oMasterAppointment.PATransaction.PriorAuthorizationID)
                            { _isPAChanged = true; }
                            else
                            { _isPAChanged = false; }
                        }

                        if (_isProviderChanged || _isPAChanged)
                        {
                            //***Delete and create new simple appointment instance***
                            #region "Delete Appointment from recurrence"
                            bool _NoAppointment = true;
                            DataTable _DeleteAppList = new DataTable();

                            #region "Delete Provider Appointment then Problem Type and Resources Appointment base on Provider Appointment"

                            Int64 _DelAppID = 0;
                            _DelAppID = AppointmentID;

                            //AS_Appointment_DTL - Provider
                            _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                            " AND nDTLAppointmentID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " ";
                            oDB.Execute_Query(_strSQL);

                            //AS_Appointment_DTL - Problem Types
                            _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                            " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " ";
                            oDB.Execute_Query(_strSQL);

                            //AS_Appointment_DTL - Resource Types
                            _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                            " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " ";
                            oDB.Execute_Query(_strSQL);

                            ////AS_Appointment_DTL_Insurances
                            //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + " AND nClinicID = " + ClinicID + "";
                            //oDB.Execute_Query(_strSQL);
                            ////AS_Appointment_DTL_Referrals
                            //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + "  AND nClinicID = " + ClinicID + "";
                            //oDB.Execute_Query(_strSQL);
                            ////AS_Appointment_DTL_Status
                            //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + "  AND nClinicID = " + ClinicID + "";
                            //oDB.Execute_Query(_strSQL);

                            // Delete respective PA Transaction for this appointment
                            PriorAuthorizationTransaction.Delete(MasterAppointmentID, _DelAppID);

                            #endregion


                            #region "Check Is there any appointment remaing - Start"
                            if (_DeleteAppList != null) { _DeleteAppList.Dispose(); }
                            _DeleteAppList = new DataTable();

                            _strSQL = "SELECT COUNT(nDTLAppointmentID) FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE " +
                                " nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";

                            oDB.Retrive_Query(_strSQL, out _DeleteAppList);
                            if (_DeleteAppList != null)
                            {
                                if (_DeleteAppList.Rows.Count > 0)
                                {
                                    _NoAppointment = false;
                                }
                            }

                            if (_DeleteAppList != null) { _DeleteAppList.Dispose(); }
                            #endregion //Check Is there any appointment remaing - Finish

                            if (_NoAppointment == true)
                            {
                                #region "If no appointments then delete master records also"
                                //AS_Appointment_MST
                                _strSQL = "DELETE FROM AS_Appointment_MST WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                                oDB.Execute_Query(_strSQL);
                                //AS_Appointment_MST_Criteria
                                _strSQL = "DELETE FROM AS_Appointment_MST_Criteria WHERE nMasterAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                                oDB.Execute_Query(_strSQL);
                                //AS_Appointment_DTL
                                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";

                                //oDB.Execute_Query(_strSQL);
                                ////AS_Appointment_DTL_Insurances
                                //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                                //oDB.Execute_Query(_strSQL);
                                ////AS_Appointment_DTL_Referrals
                                //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                                //oDB.Execute_Query(_strSQL);
                                ////AS_Appointment_DTL_Status
                                //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";

                                // Delete all PA Transactions for this master entry
                                PriorAuthorizationTransaction.Delete(MasterAppointmentID, 0);

                                oDB.Execute_Query(_strSQL);
                                #endregion
                            }
                            #endregion

                            #region "Create New Simple Appointment"
                            oMasterAppointment.IsRecurrence = SingleRecurrence.Single;

                            #region "Generate HL7 Message Queue for Modify Appointment"
                            // Code Start - Added by kanchan on 20091231 for HL7 appointment outbound
                            if (gloHL7.boolSendAppointmentDetails) //(appSettings["GenerateHL7Message"] != null)
                            {
                                //if (appSettings["GenerateHL7Message"] != "")
                                //{
                                //    if ((Convert.ToBoolean(appSettings["GenerateHL7Message"])) == true)
                                //    {
                                if (MasterAppointmentID > 0)
                                {
                                    //Added by Abhijeet on 20100909
                                    //Changes: If provider details are removed in modified appointment then generating S15 file for that provider
                                    //gloHL7.InsertInMessageQueue("S13", oMasterAppointment.PatientID, MasterAppointmentID, "", _databaseconnectionstring);
                                    if (_OldProviderID != 0 && oMasterAppointment.ASBaseID == 0)
                                    { //it means provider removed from appointment so make delete entry
                                        gloHL7.InsertInMessageQueue("S15", oMasterAppointment.PatientID, MasterAppointmentID, "", _databaseconnectionstring, true);
                                    }
                                    else
                                    {
                                        gloHL7.InsertInMessageQueue("S13", oMasterAppointment.PatientID, MasterAppointmentID, "", _databaseconnectionstring);
                                    }
                                    //End of changes By Abhijeet on 20100909

                                }
                                //    }
                                //}
                            }
                            // Code End - Added by kanchan on 20091231 for HL7 appointment outbound
                            #endregion

                            // Add(oMasterAppointment);
                            //ADDED BY SHUBHANGI TO RESOLVE 2487 ON 20100709



                            _intResult = Add(oMasterAppointment);

                            if (_intResult > 0) { _result = true; }

                            #endregion
                        }
                        else
                        {
                            //***Update Appointment***
                            #region "Delete Problem Types and Resources with respect to Provider Appoitnment"
                            Int64 _DelAppID = 0;
                            _DelAppID = AppointmentID;

                            //AS_Appointment_DTL - Problem Types
                            _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                            " AND  nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
                            oDB.Execute_Query(_strSQL);

                            //AS_Appointment_DTL - Problem Types
                            _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                            " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
                            oDB.Execute_Query(_strSQL);


                            //AS_Appointment_DTL - Resorces
                            _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                            " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " ";
                            oDB.Execute_Query(_strSQL);


                            // Delete respective PA Transaction for this appointment
                            PriorAuthorizationTransaction.Delete(MasterAppointmentID, _DelAppID);

                            #endregion

                            #region "Update Appointment"

                            _result = Update(oMasterAppointment, SingleRecurrence.SingleInRecurrence, MasterAppointmentID, AppointmentID, ClinicID);

                            if (_result)
                            {
                                _intResult = MasterAppointmentID;
                            }

                            #endregion
                        }
                    }
                    else if (ModifyMasterMethod == SingleRecurrence.Single)
                    {
                        //***Update Record even if provider change or not***
                        #region "Delete Problem Types and Resources with respect to Provider Appoitnment"

                        Int64 _DelAppID = 0;
                        _DelAppID = AppointmentID;

                        ////AS_Appointment_DTL - Problem Types
                        //_strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                        //" AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " ";
                        //oDB.Execute_Query(_strSQL);

                        ////AS_Appointment_DTL - Resorces
                        //_strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                        //" AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " ";
                        //oDB.Execute_Query(_strSQL);


                        // Delete respective PA Transaction for this appointment
                        PriorAuthorizationTransaction.Delete(MasterAppointmentID, _DelAppID);

                        #endregion

                        #region "Update Appointment"

                        _result = Update_TVP(oMasterAppointment, SingleRecurrence.Single, MasterAppointmentID, AppointmentID, ClinicID);
                        //_result = Update(oMasterAppointment, SingleRecurrence.Single, MasterAppointmentID, AppointmentID, ClinicID);

                        if (_result)
                        {
                            _intResult = MasterAppointmentID;
                        }

                        #endregion
                    }
                }
                else if (ModifyMethod == SingleRecurrence.Recurrence)
                {
                    #region "Delete Appointments"
                    if (UpdateCriteria == ASUpdateCriteria.DontDeleteOccurenceAndCreateNewRecurrence)
                    {
                        bool _NoAppointment = true;
                        DataTable _DeleteAppList = new DataTable();

                        #region "Retirve Delete Appointment List"

                        string _InAppIDs = "";
                        for (int i = 0; i < DontDeleteAppointments.Count; i++)
                        {
                            if (i == 0)
                            {
                                _InAppIDs = "(" + DontDeleteAppointments[i].ToString();
                            }
                            else
                            {
                                _InAppIDs = _InAppIDs + "," + DontDeleteAppointments[i].ToString();
                            }

                            if (i == DontDeleteAppointments.Count - 1)
                            {
                                _InAppIDs = _InAppIDs + ")";
                            }
                        }
                        if (_InAppIDs.Length <= 0)
                            _InAppIDs = "(0)";

                        _strSQL = "SELECT nDTLAppointmentID FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE " +
                        " nMSTAppointmentID = " + MasterAppointmentID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " " +
                        " AND nDTLAppointmentID NOT IN " + _InAppIDs + " AND nClinicID = " + ClinicID + "";

                        oDB.Retrive_Query(_strSQL, out _DeleteAppList);

                        #endregion

                        if (_DeleteAppList != null)
                        {
                            #region "Delete Provider Appointment then Problem Type and Resources Appointment base on Provider Appointment"
                            for (_AppCntr = 0; _AppCntr <= _DeleteAppList.Rows.Count - 1; _AppCntr++)
                            {
                                Int64 _DelAppID = 0;
                                _DelAppID = Convert.ToInt64(_DeleteAppList.Rows[_AppCntr][0].ToString());

                                //AS_Appointment_DTL - Provider
                                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                                " AND nDTLAppointmentID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " ";
                                oDB.Execute_Query(_strSQL);

                                //AS_Appointment_DTL - Problem Types
                                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                                " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
                                oDB.Execute_Query(_strSQL);

                                //AS_Appointment_DTL - Problem Types
                                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                                " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
                                oDB.Execute_Query(_strSQL);

                                ////AS_Appointment_DTL_Insurances
                                //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + " AND nClinicID = " + ClinicID + "";
                                //oDB.Execute_Query(_strSQL);
                                ////AS_Appointment_DTL_Referrals
                                //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + "  AND nClinicID = " + ClinicID + "";
                                //oDB.Execute_Query(_strSQL);
                                ////AS_Appointment_DTL_Status
                                //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + "  AND nClinicID = " + ClinicID + "";
                                //oDB.Execute_Query(_strSQL);

                                // Delete respective PA Transaction for this appointment
                                PriorAuthorizationTransaction.Delete(MasterAppointmentID, _DelAppID);
                            }
                            #endregion
                        }

                        //Check Is there any appointment remaing - Start
                        if (_DeleteAppList != null) { _DeleteAppList.Dispose(); }
                        _DeleteAppList = new DataTable();

                        _strSQL = "SELECT COUNT(nDTLAppointmentID) FROM AS_Appointment_DTL WITH(NOLOCK)  WHERE " +
                            " nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";

                        oDB.Retrive_Query(_strSQL, out _DeleteAppList);
                        if (_DeleteAppList != null)
                        {
                            if (_DeleteAppList.Rows.Count > 0)
                            {
                                _NoAppointment = false;
                            }
                        }

                        if (_DeleteAppList != null) { _DeleteAppList.Dispose(); }
                        //Check Is there any appointment remaing - Finish

                        if (_NoAppointment == true)
                        {
                            #region "If no appointments then delete master records also"
                            //AS_Appointment_MST
                            _strSQL = "DELETE FROM AS_Appointment_MST WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                            oDB.Execute_Query(_strSQL);
                            //AS_Appointment_MST_Criteria
                            _strSQL = "DELETE FROM AS_Appointment_MST_Criteria WHERE nMasterAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                            oDB.Execute_Query(_strSQL);
                            //AS_Appointment_DTL
                            _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                            oDB.Execute_Query(_strSQL);

                            ////AS_Appointment_DTL_Insurances
                            //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                            //oDB.Execute_Query(_strSQL);
                            ////AS_Appointment_DTL_Referrals
                            //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                            //oDB.Execute_Query(_strSQL);
                            ////AS_Appointment_DTL_Status
                            //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                            //oDB.Execute_Query(_strSQL);

                            // Delete all PA Transactions for this master entry
                            PriorAuthorizationTransaction.Delete(MasterAppointmentID, 0);

                            #endregion
                        }
                    }
                    else
                    {
                        //AS_Appointment_MST
                        _strSQL = "DELETE FROM AS_Appointment_MST WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                        oDB.Execute_Query(_strSQL);
                        //AS_Appointment_MST_Criteria
                        _strSQL = "DELETE FROM AS_Appointment_MST_Criteria WHERE nMasterAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                        oDB.Execute_Query(_strSQL);
                        //AS_Appointment_DTL
                        _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                        oDB.Execute_Query(_strSQL);

                        //removed below tables from database in 6040

                        //AS_Appointment_DTL_Insurances
                        //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                        //oDB.Execute_Query(_strSQL);
                        //AS_Appointment_DTL_Referrals
                        //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                        //oDB.Execute_Query(_strSQL);
                        //AS_Appointment_DTL_Status
                        //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                        //oDB.Execute_Query(_strSQL);


                        // Delete all PA Transactions for this master entry
                        PriorAuthorizationTransaction.Delete(MasterAppointmentID, 0);
                    }

                    #endregion

                    _intResult = Add(oMasterAppointment);

                    if (_intResult > 0) { _result = true; }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                _result = false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _intResult;
        }


        //commented by Amit to solve recurrence Appointment save multiple times when click on Print button

        //public bool Modify(MasterAppointment oMasterAppointment, Int64 MasterAppointmentID, Int64 AppointmentID, Int64 ClinicID, SingleRecurrence ModifyMethod, SingleRecurrence ModifyMasterMethod, bool ModifySingleInRecurrence, ASUpdateCriteria UpdateCriteria, ArrayList DontDeleteAppointments)
        //{
        //    bool _result = false;
        //    Int64 _intResult = 0;
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    int _AppCntr = 0;
        //    string _strSQL = "";
        //    object _sqlresult = null;

        //    try
        //    {
        //        oDB.Connect(false);
        //        if (ModifyMethod == SingleRecurrence.Single || ModifyMethod == SingleRecurrence.SingleInRecurrence)
        //        {
        //            Int64 _OldProviderID = 0;
        //            Int64 _OldPAID = 0;

        //            #region "Read Old Provider ID"

        //            _sqlresult = new object();
        //            string str = "SELECT nASBaseID FROM AS_Appointment_MST WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " ";
        //            _sqlresult = oDB.ExecuteScalar_Query(str);

        //            if (_sqlresult != null)
        //            {
        //                if (_sqlresult.ToString() != null)
        //                {
        //                    if (_sqlresult.ToString() != "")
        //                    {
        //                        _OldProviderID = Convert.ToInt64(_sqlresult.ToString());
        //                    }
        //                }
        //            }
        //            _sqlresult = null;

        //            #endregion

        //            #region "Read Old PA ID"

        //            DataRow drPA = clsgloPriorAuthorization.GetPriorAuthorizationInfo(MasterAppointmentID, AppointmentID);
        //            if (drPA != null)
        //            {
        //                _OldPAID = Convert.ToInt64(drPA["nPriorAuthorizationID"]);
        //            }

        //            #endregion

        //            if (ModifyMasterMethod == SingleRecurrence.Recurrence)
        //            {
        //                oMasterAppointment.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;

        //                bool _isProviderChanged = false;

        //                if (_OldProviderID != oMasterAppointment.ASBaseID)
        //                { _isProviderChanged = true; }
        //                else
        //                { _isProviderChanged = false; }

        //                bool _isPAChanged = false;
        //                if (oMasterAppointment.PATransaction != null)
        //                {
        //                    if (_OldPAID != oMasterAppointment.PATransaction.PriorAuthorizationID)
        //                    { _isPAChanged = true; }
        //                    else
        //                    { _isPAChanged = false; }
        //                }

        //                if (_isProviderChanged || _isPAChanged)
        //                {
        //                    //***Delete and create new simple appointment instance***
        //                    #region "Delete Appointment from recurrence"
        //                    bool _NoAppointment = true;
        //                    DataTable _DeleteAppList = new DataTable();

        //                    #region "Delete Provider Appointment then Problem Type and Resources Appointment base on Provider Appointment"

        //                    Int64 _DelAppID = 0;
        //                    _DelAppID = AppointmentID;

        //                    //AS_Appointment_DTL - Provider
        //                    _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
        //                    " AND nDTLAppointmentID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " ";
        //                    oDB.Execute_Query(_strSQL);

        //                    //AS_Appointment_DTL - Problem Types
        //                    _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
        //                    " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " ";
        //                    oDB.Execute_Query(_strSQL);

        //                    //AS_Appointment_DTL - Resource Types
        //                    _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
        //                    " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " ";
        //                    oDB.Execute_Query(_strSQL);

        //                    ////AS_Appointment_DTL_Insurances
        //                    //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + " AND nClinicID = " + ClinicID + "";
        //                    //oDB.Execute_Query(_strSQL);
        //                    ////AS_Appointment_DTL_Referrals
        //                    //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + "  AND nClinicID = " + ClinicID + "";
        //                    //oDB.Execute_Query(_strSQL);
        //                    ////AS_Appointment_DTL_Status
        //                    //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + "  AND nClinicID = " + ClinicID + "";
        //                    //oDB.Execute_Query(_strSQL);

        //                    // Delete respective PA Transaction for this appointment
        //                    PriorAuthorizationTransaction.Delete(MasterAppointmentID, _DelAppID);

        //                    #endregion


        //                    #region "Check Is there any appointment remaing - Start"
        //                    if (_DeleteAppList != null) { _DeleteAppList.Dispose(); }
        //                    _DeleteAppList = new DataTable();

        //                    _strSQL = "SELECT COUNT(nDTLAppointmentID) FROM AS_Appointment_DTL WHERE " +
        //                        " nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";

        //                    oDB.Retrive_Query(_strSQL, out _DeleteAppList);
        //                    if (_DeleteAppList != null)
        //                    {
        //                        if (_DeleteAppList.Rows.Count > 0)
        //                        {
        //                            _NoAppointment = false;
        //                        }
        //                    }

        //                    if (_DeleteAppList != null) { _DeleteAppList.Dispose(); }
        //                    #endregion //Check Is there any appointment remaing - Finish

        //                    if (_NoAppointment == true)
        //                    {
        //                        #region "If no appointments then delete master records also"
        //                        //AS_Appointment_MST
        //                        _strSQL = "DELETE FROM AS_Appointment_MST WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                        oDB.Execute_Query(_strSQL);
        //                        //AS_Appointment_MST_Criteria
        //                        _strSQL = "DELETE FROM AS_Appointment_MST_Criteria WHERE nMasterAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                        oDB.Execute_Query(_strSQL);
        //                        //AS_Appointment_DTL
        //                        _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";

        //                        //oDB.Execute_Query(_strSQL);
        //                        ////AS_Appointment_DTL_Insurances
        //                        //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                        //oDB.Execute_Query(_strSQL);
        //                        ////AS_Appointment_DTL_Referrals
        //                        //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                        //oDB.Execute_Query(_strSQL);
        //                        ////AS_Appointment_DTL_Status
        //                        //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";

        //                        // Delete all PA Transactions for this master entry
        //                        PriorAuthorizationTransaction.Delete(MasterAppointmentID, 0);

        //                        oDB.Execute_Query(_strSQL);
        //                        #endregion
        //                    }
        //                    #endregion

        //                    #region "Create New Simple Appointment"
        //                    oMasterAppointment.IsRecurrence = SingleRecurrence.Single;

        //                    #region "Generate HL7 Message Queue for Modify Appointment"
        //                    // Code Start - Added by kanchan on 20091231 for HL7 appointment outbound
        //                    if (appSettings["GenerateHL7Message"] != null)
        //                    {
        //                        if (appSettings["GenerateHL7Message"] != "")
        //                        {
        //                            if ((Convert.ToBoolean(appSettings["GenerateHL7Message"])) == true)
        //                            {
        //                                if (MasterAppointmentID > 0)
        //                                {
        //                                    //Added by Abhijeet on 20100909
        //                                    //Changes: If provider details are removed in modified appointment then generating S15 file for that provider
        //                                    //gloHL7.InsertInMessageQueue("S13", oMasterAppointment.PatientID, MasterAppointmentID, "", _databaseconnectionstring);
        //                                    if (_OldProviderID != 0 && oMasterAppointment.ASBaseID == 0)
        //                                    { //it means provider removed from appointment so make delete entry
        //                                        gloHL7.InsertInMessageQueue("S15", oMasterAppointment.PatientID, MasterAppointmentID, "", _databaseconnectionstring, true);
        //                                    }
        //                                    else
        //                                    {
        //                                        gloHL7.InsertInMessageQueue("S13", oMasterAppointment.PatientID, MasterAppointmentID, "", _databaseconnectionstring);
        //                                    }
        //                                    //End of changes By Abhijeet on 20100909

        //                                }
        //                            }
        //                        }
        //                    }
        //                    // Code End - Added by kanchan on 20091231 for HL7 appointment outbound
        //                    #endregion

        //                    // Add(oMasterAppointment);
        //                    //ADDED BY SHUBHANGI TO RESOLVE 2487 ON 20100709
        //                    _intResult = Add(oMasterAppointment);

        //                    if (_intResult > 0) { _result = true; }

        //                    #endregion
        //                }
        //                else
        //                {
        //                    //***Update Appointment***
        //                    #region "Delete Problem Types and Resources with respect to Provider Appoitnment"
        //                    Int64 _DelAppID = 0;
        //                    _DelAppID = AppointmentID;

        //                    //AS_Appointment_DTL - Problem Types
        //                    _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
        //                    " AND  nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
        //                    oDB.Execute_Query(_strSQL);

        //                    //AS_Appointment_DTL - Problem Types
        //                    _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
        //                    " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
        //                    oDB.Execute_Query(_strSQL);

        //                    // Delete respective PA Transaction for this appointment
        //                    PriorAuthorizationTransaction.Delete(MasterAppointmentID, _DelAppID);

        //                    #endregion

        //                    #region "Update Appointment"
        //                    _result = Update(oMasterAppointment, SingleRecurrence.SingleInRecurrence, MasterAppointmentID, AppointmentID, ClinicID);
        //                    #endregion
        //                }
        //            }
        //            else if (ModifyMasterMethod == SingleRecurrence.Single)
        //            {
        //                //***Update Record even if provider change or not***
        //                #region "Delete Problem Types and Resources with respect to Provider Appoitnment"

        //                Int64 _DelAppID = 0;
        //                _DelAppID = AppointmentID;

        //                //AS_Appointment_DTL - Problem Types
        //                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
        //                " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " ";
        //                oDB.Execute_Query(_strSQL);

        //                //AS_Appointment_DTL - Resorces
        //                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
        //                " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " ";
        //                oDB.Execute_Query(_strSQL);


        //                // Delete respective PA Transaction for this appointment
        //                PriorAuthorizationTransaction.Delete(MasterAppointmentID, _DelAppID);

        //                #endregion

        //                #region "Update Appointment"

        //                _result = Update(oMasterAppointment, SingleRecurrence.Single, MasterAppointmentID, AppointmentID, ClinicID);

        //                #endregion
        //            }
        //        }
        //        else if (ModifyMethod == SingleRecurrence.Recurrence)
        //        {
        //            #region "Delete Appointments"
        //            if (UpdateCriteria == ASUpdateCriteria.DontDeleteOccurenceAndCreateNewRecurrence)
        //            {
        //                bool _NoAppointment = true;
        //                DataTable _DeleteAppList = new DataTable();

        //                #region "Retirve Delete Appointment List"

        //                string _InAppIDs = "";
        //                for (int i = 0; i < DontDeleteAppointments.Count; i++)
        //                {
        //                    if (i == 0)
        //                    {
        //                        _InAppIDs = "(" + DontDeleteAppointments[i].ToString();
        //                    }
        //                    else
        //                    {
        //                        _InAppIDs = _InAppIDs + "," + DontDeleteAppointments[i].ToString();
        //                    }

        //                    if (i == DontDeleteAppointments.Count - 1)
        //                    {
        //                        _InAppIDs = _InAppIDs + ")";
        //                    }
        //                }
        //                if (_InAppIDs.Length <= 0)
        //                    _InAppIDs = "(0)";

        //                _strSQL = "SELECT nDTLAppointmentID FROM AS_Appointment_DTL WHERE " +
        //                " nMSTAppointmentID = " + MasterAppointmentID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " " +
        //                " AND nDTLAppointmentID NOT IN " + _InAppIDs + " AND nClinicID = " + ClinicID + "";

        //                oDB.Retrive_Query(_strSQL, out _DeleteAppList);

        //                #endregion

        //                if (_DeleteAppList != null)
        //                {
        //                    #region "Delete Provider Appointment then Problem Type and Resources Appointment base on Provider Appointment"
        //                    for (_AppCntr = 0; _AppCntr <= _DeleteAppList.Rows.Count - 1; _AppCntr++)
        //                    {
        //                        Int64 _DelAppID = 0;
        //                        _DelAppID = Convert.ToInt64(_DeleteAppList.Rows[_AppCntr][0].ToString());

        //                        //AS_Appointment_DTL - Provider
        //                        _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
        //                        " AND nDTLAppointmentID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " ";
        //                        oDB.Execute_Query(_strSQL);

        //                        //AS_Appointment_DTL - Problem Types
        //                        _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
        //                        " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
        //                        oDB.Execute_Query(_strSQL);

        //                        //AS_Appointment_DTL - Problem Types
        //                        _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
        //                        " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
        //                        oDB.Execute_Query(_strSQL);

        //                        ////AS_Appointment_DTL_Insurances
        //                        //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + " AND nClinicID = " + ClinicID + "";
        //                        //oDB.Execute_Query(_strSQL);
        //                        ////AS_Appointment_DTL_Referrals
        //                        //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + "  AND nClinicID = " + ClinicID + "";
        //                        //oDB.Execute_Query(_strSQL);
        //                        ////AS_Appointment_DTL_Status
        //                        //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + "  AND nClinicID = " + ClinicID + "";
        //                        //oDB.Execute_Query(_strSQL);

        //                        // Delete respective PA Transaction for this appointment
        //                        PriorAuthorizationTransaction.Delete(MasterAppointmentID, _DelAppID);
        //                    }
        //                    #endregion
        //                }

        //                //Check Is there any appointment remaing - Start
        //                if (_DeleteAppList != null) { _DeleteAppList.Dispose(); }
        //                _DeleteAppList = new DataTable();

        //                _strSQL = "SELECT COUNT(nDTLAppointmentID) FROM AS_Appointment_DTL WHERE " +
        //                    " nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";

        //                oDB.Retrive_Query(_strSQL, out _DeleteAppList);
        //                if (_DeleteAppList != null)
        //                {
        //                    if (_DeleteAppList.Rows.Count > 0)
        //                    {
        //                        _NoAppointment = false;
        //                    }
        //                }

        //                if (_DeleteAppList != null) { _DeleteAppList.Dispose(); }
        //                //Check Is there any appointment remaing - Finish

        //                if (_NoAppointment == true)
        //                {
        //                    #region "If no appointments then delete master records also"
        //                    //AS_Appointment_MST
        //                    _strSQL = "DELETE FROM AS_Appointment_MST WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                    oDB.Execute_Query(_strSQL);
        //                    //AS_Appointment_MST_Criteria
        //                    _strSQL = "DELETE FROM AS_Appointment_MST_Criteria WHERE nMasterAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                    oDB.Execute_Query(_strSQL);
        //                    //AS_Appointment_DTL
        //                    _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                    oDB.Execute_Query(_strSQL);

        //                    ////AS_Appointment_DTL_Insurances
        //                    //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                    //oDB.Execute_Query(_strSQL);
        //                    ////AS_Appointment_DTL_Referrals
        //                    //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                    //oDB.Execute_Query(_strSQL);
        //                    ////AS_Appointment_DTL_Status
        //                    //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                    //oDB.Execute_Query(_strSQL);

        //                    // Delete all PA Transactions for this master entry
        //                    PriorAuthorizationTransaction.Delete(MasterAppointmentID, 0);

        //                    #endregion
        //                }
        //            }
        //            else
        //            {
        //                //AS_Appointment_MST
        //                _strSQL = "DELETE FROM AS_Appointment_MST WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                oDB.Execute_Query(_strSQL);
        //                //AS_Appointment_MST_Criteria
        //                _strSQL = "DELETE FROM AS_Appointment_MST_Criteria WHERE nMasterAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                oDB.Execute_Query(_strSQL);
        //                //AS_Appointment_DTL
        //                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                oDB.Execute_Query(_strSQL);

        //                //removed below tables from database in 6040

        //                //AS_Appointment_DTL_Insurances
        //                //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                //oDB.Execute_Query(_strSQL);
        //                //AS_Appointment_DTL_Referrals
        //                //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                //oDB.Execute_Query(_strSQL);
        //                //AS_Appointment_DTL_Status
        //                //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
        //                //oDB.Execute_Query(_strSQL);


        //                // Delete all PA Transactions for this master entry
        //                PriorAuthorizationTransaction.Delete(MasterAppointmentID, 0);
        //            }

        //            #endregion

        //            _intResult = Add(oMasterAppointment);

        //            if (_intResult > 0) { _result = true; }
        //        }
        //        oDB.Disconnect();
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

        //        _result = false;
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //    }
        //    return _result;
        //}
        //added for resource appointment


        public bool ModifyResourceappt(MasterAppointment oMasterAppointment, Int64 MasterAppointmentID, Int64 AppointmentID, Int64 ClinicID, SingleRecurrence ModifyMethod, SingleRecurrence ModifyMasterMethod, bool ModifySingleInRecurrence, ASUpdateCriteria UpdateCriteria, ArrayList DontDeleteAppointments)
        {
            bool _result = false;
            Int64 _intResult = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            int _AppCntr = 0;
            //int _PrbTypCntr = 0;
            //int _ResCntr = 0;
            string _strSQL = "";
            object _sqlresult = null;

            try
            {
                oDB.Connect(false);
                if (ModifyMethod == SingleRecurrence.Single || ModifyMethod == SingleRecurrence.SingleInRecurrence)
                {
                    Int64 _OldProviderID = 0;
                    Int64 _OldPAID = 0;

                    #region "Read Old Provider ID"

                    _sqlresult = new object();
                    string str = "SELECT nASBaseID FROM AS_Appointment_MST  WITH(NOLOCK) WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " ";
                    _sqlresult = oDB.ExecuteScalar_Query(str);
                    if (_sqlresult != null)
                    {
                        if (_sqlresult.ToString() != null)
                        {
                            if (_sqlresult.ToString() != "")
                            {
                                _OldProviderID = Convert.ToInt64(_sqlresult.ToString());
                            }
                        }
                    }
                    _sqlresult = null;

                    #endregion

                    #region "Read Old PA ID"

                    DataRow drPA = clsgloPriorAuthorization.GetPriorAuthorizationInfo(MasterAppointmentID, AppointmentID);
                    if (drPA != null)
                    {
                        _OldPAID = Convert.ToInt64(drPA["nPriorAuthorizationID"]);
                    }

                    #endregion

                    //if (_OldProviderID > 0)
                    {
                        //That means provider change, so for simple appointment modify and for single in recurrence delete and create new entry
                        if (ModifyMasterMethod == SingleRecurrence.Recurrence)
                        {
                            //Chnage Criteria Flag so it will register appointment on single date
                            oMasterAppointment.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;

                            bool _isProviderChanged = false;

                            if (_OldProviderID != oMasterAppointment.ASBaseID)
                            { _isProviderChanged = true; }
                            else
                            { _isProviderChanged = false; }

                            bool _isPAChanged = false;
                            if (oMasterAppointment.PATransaction != null)
                            {
                                if (_OldPAID != oMasterAppointment.PATransaction.PriorAuthorizationID)
                                { _isPAChanged = true; }
                                else
                                { _isPAChanged = false; }
                            }

                            //if (_OldProviderID != oMasterAppointment.ASBaseID)
                            if (_isProviderChanged || _isPAChanged)
                            {
                                //***Delete and create new simple appointment instance***
                                #region "Delete Appointment from recurrence"
                                bool _NoAppointment = true;
                                DataTable _DeleteAppList = new DataTable();

                                #region "Delete Provider Appointment then Problem Type and Resources Appointment base on Provider Appointment"

                                Int64 _DelAppID = 0;
                                _DelAppID = AppointmentID;

                                //AS_Appointment_DTL - Provider
                                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                                " AND nDTLAppointmentID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " ";
                                oDB.Execute_Query(_strSQL);

                                //AS_Appointment_DTL - Problem Types
                                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                                " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
                                oDB.Execute_Query(_strSQL);

                                //AS_Appointment_DTL - Problem Types
                                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                                " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
                                oDB.Execute_Query(_strSQL);

                                ////AS_Appointment_DTL_Insurances
                                //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + " AND nClinicID = " + ClinicID + "";
                                //oDB.Execute_Query(_strSQL);
                                ////AS_Appointment_DTL_Referrals
                                //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + "  AND nClinicID = " + ClinicID + "";
                                //oDB.Execute_Query(_strSQL);
                                ////AS_Appointment_DTL_Status
                                //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + "  AND nClinicID = " + ClinicID + "";
                                //oDB.Execute_Query(_strSQL);

                                // Delete respective PA Transaction for this appointment
                                PriorAuthorizationTransaction.Delete(MasterAppointmentID, _DelAppID);

                                #endregion


                                #region "Check Is there any appointment remaing - Start"
                                if (_DeleteAppList != null) { _DeleteAppList.Dispose(); }
                                _DeleteAppList = new DataTable();

                                _strSQL = "SELECT COUNT(nDTLAppointmentID) FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE " +
                                    " nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";

                                oDB.Retrive_Query(_strSQL, out _DeleteAppList);
                                if (_DeleteAppList != null)
                                {
                                    if (_DeleteAppList.Rows.Count > 0)
                                    {
                                        _NoAppointment = false;
                                    }
                                }

                                if (_DeleteAppList != null) { _DeleteAppList.Dispose(); }
                                #endregion //Check Is there any appointment remaing - Finish

                                if (_NoAppointment == true)
                                {
                                    #region "If no appointments then delete master records also"
                                    //AS_Appointment_MST
                                    _strSQL = "DELETE FROM AS_Appointment_MST WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                                    oDB.Execute_Query(_strSQL);
                                    //AS_Appointment_MST_Criteria
                                    _strSQL = "DELETE FROM AS_Appointment_MST_Criteria WHERE nMasterAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                                    oDB.Execute_Query(_strSQL);
                                    //AS_Appointment_DTL
                                    _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";

                                    //oDB.Execute_Query(_strSQL);
                                    ////AS_Appointment_DTL_Insurances
                                    //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                                    //oDB.Execute_Query(_strSQL);
                                    ////AS_Appointment_DTL_Referrals
                                    //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                                    //oDB.Execute_Query(_strSQL);
                                    ////AS_Appointment_DTL_Status
                                    //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";

                                    // Delete all PA Transactions for this master entry
                                    PriorAuthorizationTransaction.Delete(MasterAppointmentID, 0);

                                    oDB.Execute_Query(_strSQL);
                                    #endregion
                                }
                                #endregion

                                #region "Create New Simple Appointment"
                                oMasterAppointment.IsRecurrence = SingleRecurrence.Single;

                                #region "Generate HL7 Message Queue for Modify Appointment"
                                // Code Start - Added by kanchan on 20091231 for HL7 appointment outbound
                                if (gloHL7.boolSendAppointmentDetails) //(appSettings["GenerateHL7Message"] != null)
                                {
                                    //if (appSettings["GenerateHL7Message"] != "")
                                    //{
                                    //    if ((Convert.ToBoolean(appSettings["GenerateHL7Message"])) == true)
                                    //    {
                                    if (MasterAppointmentID > 0)
                                    {
                                        //Added by Abhijeet on 20100909    
                                        //Changes: If provider details are removed in modified appointment then generating S15 file for that provider                                    
                                        //gloHL7.InsertInMessageQueue("S13", oMasterAppointment.PatientID, MasterAppointmentID, "", _databaseconnectionstring);
                                        if (gloHL7.nOldBaseId != 0 && oMasterAppointment.ASBaseID == 0)
                                        { //it means provider removed from appointment so make delete entry                                                   
                                            gloHL7.InsertInMessageQueue("S15", oMasterAppointment.PatientID, MasterAppointmentID, "", _databaseconnectionstring, true);
                                        }
                                        else
                                        {
                                            gloHL7.InsertInMessageQueue("S13", oMasterAppointment.PatientID, MasterAppointmentID, "", _databaseconnectionstring);
                                        }
                                        //End of changes By Abhijeet on 20100909


                                    }
                                    //    }
                                    //}
                                }
                                // Code End - Added by kanchan on 20091231 for HL7 appointment outbound
                                #endregion

                                // Add(oMasterAppointment);
                                //ADDED BY SHUBHANGI TO RESOLVE 2487 ON 20100709
                                _intResult = Add(oMasterAppointment);

                                if (_intResult > 0) { _result = true; }

                                #endregion
                            }
                            else
                            {
                                //***Update Appointment***
                                #region "Delete Problem Types and Resources with respect to Provider Appoitnment"
                                Int64 _DelAppID = 0;
                                _DelAppID = AppointmentID;

                                //AS_Appointment_DTL - Problem Types
                                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                                " AND  nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
                                oDB.Execute_Query(_strSQL);

                                //AS_Appointment_DTL - Problem Types
                                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                                " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
                                oDB.Execute_Query(_strSQL);

                                // Delete respective PA Transaction for this appointment
                                PriorAuthorizationTransaction.Delete(MasterAppointmentID, _DelAppID);

                                #endregion

                                #region "Update Appointment"
                                _result = Update(oMasterAppointment, SingleRecurrence.SingleInRecurrence, MasterAppointmentID, AppointmentID, ClinicID);
                                #endregion
                            }
                        }
                        else if (ModifyMasterMethod == SingleRecurrence.Single)
                        {
                            //***Update Record even if provider change or not***
                            #region "Delete Problem Types and Resources with respect to Provider Appoitnment"

                            Int64 _DelAppID = 0;
                            _DelAppID = AppointmentID;

                            //AS_Appointment_DTL - Problem Types
                            _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                            " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
                            oDB.Execute_Query(_strSQL);

                            //AS_Appointment_DTL - Problem Types
                            _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                            " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
                            oDB.Execute_Query(_strSQL);

                            // Delete respective PA Transaction for this appointment
                            PriorAuthorizationTransaction.Delete(MasterAppointmentID, _DelAppID);

                            #endregion

                            #region "Update Appointment"

                            _result = Update(oMasterAppointment, SingleRecurrence.Single, MasterAppointmentID, AppointmentID, ClinicID);

                            #endregion
                        }


                    }
                }
                else if (ModifyMethod == SingleRecurrence.Recurrence)
                {
                    #region "Delete Appointments"
                    if (UpdateCriteria == ASUpdateCriteria.DontDeleteOccurenceAndCreateNewRecurrence)
                    {
                        bool _NoAppointment = true;
                        DataTable _DeleteAppList = new DataTable();

                        #region "Retrieve Delete Appointment List"

                        string _InAppIDs = "";
                        for (int i = 0; i < DontDeleteAppointments.Count; i++)
                        {
                            if (i == 0)
                            {
                                _InAppIDs = "(" + DontDeleteAppointments[i].ToString();
                            }
                            else
                            {
                                _InAppIDs = _InAppIDs + "," + DontDeleteAppointments[i].ToString();
                            }

                            if (i == DontDeleteAppointments.Count - 1)
                            {
                                _InAppIDs = _InAppIDs + ")";
                            }
                        }
                        if (_InAppIDs.Length <= 0)
                            _InAppIDs = "(0)";

                        _strSQL = "SELECT nDTLAppointmentID FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE " +
                        " nMSTAppointmentID = " + MasterAppointmentID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " " +
                        " AND nDTLAppointmentID NOT IN " + _InAppIDs + " AND nClinicID = " + ClinicID + "";

                        oDB.Retrive_Query(_strSQL, out _DeleteAppList);

                        #endregion

                        if (_DeleteAppList != null)
                        {
                            #region "Delete Provider Appointment then Problem Type and Resources Appointment base on Provider Appointment"
                            for (_AppCntr = 0; _AppCntr <= _DeleteAppList.Rows.Count - 1; _AppCntr++)
                            {
                                Int64 _DelAppID = 0;
                                _DelAppID = Convert.ToInt64(_DeleteAppList.Rows[_AppCntr][0].ToString());

                                //AS_Appointment_DTL - Provider
                                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                                " AND nDTLAppointmentID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " ";
                                oDB.Execute_Query(_strSQL);

                                //AS_Appointment_DTL - Problem Types
                                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                                " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
                                oDB.Execute_Query(_strSQL);

                                //AS_Appointment_DTL - Problem Types
                                _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + " " +
                                " AND nRefID = " + _DelAppID + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + "";
                                oDB.Execute_Query(_strSQL);

                                ////AS_Appointment_DTL_Insurances
                                //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + " AND nClinicID = " + ClinicID + "";
                                //oDB.Execute_Query(_strSQL);
                                ////AS_Appointment_DTL_Referrals
                                //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + "  AND nClinicID = " + ClinicID + "";
                                //oDB.Execute_Query(_strSQL);
                                ////AS_Appointment_DTL_Status
                                //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nAppointmentID = " + _DelAppID + "  AND nClinicID = " + ClinicID + "";
                                //oDB.Execute_Query(_strSQL);

                                // Delete respective PA Transaction for this appointment
                                PriorAuthorizationTransaction.Delete(MasterAppointmentID, _DelAppID);
                            }
                            #endregion
                        }

                        //Check Is there any appointment remaing - Start
                        if (_DeleteAppList != null) { _DeleteAppList.Dispose(); }
                        _DeleteAppList = new DataTable();

                        _strSQL = "SELECT COUNT(nDTLAppointmentID) FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE " +
                            " nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";

                        oDB.Retrive_Query(_strSQL, out _DeleteAppList);
                        if (_DeleteAppList != null)
                        {
                            if (_DeleteAppList.Rows.Count > 0)
                            {
                                _NoAppointment = false;
                            }
                        }

                        if (_DeleteAppList != null) { _DeleteAppList.Dispose(); }
                        //Check Is there any appointment remaing - Finish

                        if (_NoAppointment == true)
                        {
                            #region "If no appointments then delete master records also"
                            //AS_Appointment_MST
                            _strSQL = "DELETE FROM AS_Appointment_MST WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                            oDB.Execute_Query(_strSQL);
                            //AS_Appointment_MST_Criteria
                            _strSQL = "DELETE FROM AS_Appointment_MST_Criteria WHERE nMasterAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                            oDB.Execute_Query(_strSQL);
                            //AS_Appointment_DTL
                            _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                            oDB.Execute_Query(_strSQL);

                            ////AS_Appointment_DTL_Insurances
                            //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                            //oDB.Execute_Query(_strSQL);
                            ////AS_Appointment_DTL_Referrals
                            //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                            //oDB.Execute_Query(_strSQL);
                            ////AS_Appointment_DTL_Status
                            //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                            //oDB.Execute_Query(_strSQL);

                            // Delete all PA Transactions for this master entry
                            PriorAuthorizationTransaction.Delete(MasterAppointmentID, 0);

                            #endregion
                        }
                    }
                    else
                    {
                        //AS_Appointment_MST
                        _strSQL = "DELETE FROM AS_Appointment_MST WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                        oDB.Execute_Query(_strSQL);
                        //AS_Appointment_MST_Criteria
                        _strSQL = "DELETE FROM AS_Appointment_MST_Criteria WHERE nMasterAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                        oDB.Execute_Query(_strSQL);
                        //AS_Appointment_DTL
                        _strSQL = "DELETE FROM AS_Appointment_DTL WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                        oDB.Execute_Query(_strSQL);

                        ////AS_Appointment_DTL_Insurances
                        //_strSQL = "DELETE FROM AS_Appointment_DTL_Insurances WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                        //oDB.Execute_Query(_strSQL);
                        ////AS_Appointment_DTL_Referrals
                        //_strSQL = "DELETE FROM AS_Appointment_DTL_Referrals WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                        //oDB.Execute_Query(_strSQL);
                        ////AS_Appointment_DTL_Status
                        //_strSQL = "DELETE FROM AS_Appointment_DTL_Status WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + ClinicID + "";
                        //oDB.Execute_Query(_strSQL);

                        // Delete all PA Transactions for this master entry
                        PriorAuthorizationTransaction.Delete(MasterAppointmentID, 0);
                    }

                    #endregion

                    _intResult = Add(oMasterAppointment);

                    if (_intResult > 0) { _result = true; }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                _result = false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        public bool Update_TVP(MasterAppointment oMasterAppointment, SingleRecurrence IsRecurrenceType, Int64 MasterAppointmentID, Int64 AppointmentID, Int64 ClinicID, Int64 TemplateAllocationMasterID=0, Int64 TemplateAllocationID=0, Int64 AppointmentLineNo =0)
        {
            int Flag = 0;
            bool isContain = false;
            int nAppCntr = 0;

            #region "Create dt_AS_Appointment_MST"
            DataTable dt_AS_Appointment_MST = new DataTable();
            dt_AS_Appointment_MST.Columns.Add("nMSTAppointmentID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("bIsSingleRecurrence", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_MST.Columns.Add("nAppointmentFlag", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_MST.Columns.Add("nAppointmentTypeID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("sAppointmentTypeCode", System.Type.GetType("System.String"));
            dt_AS_Appointment_MST.Columns.Add("sAppointmentTypeDesc", System.Type.GetType("System.String"));
            dt_AS_Appointment_MST.Columns.Add("nASBaseID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("sASBaseCode", System.Type.GetType("System.String"));
            dt_AS_Appointment_MST.Columns.Add("sASBaseDesc", System.Type.GetType("System.String"));
            dt_AS_Appointment_MST.Columns.Add("nASBaseFlag", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_MST.Columns.Add("nReferralProviderID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("nReferralProviderCode", System.Type.GetType("System.String"));
            dt_AS_Appointment_MST.Columns.Add("nReferralProviderName", System.Type.GetType("System.String"));
            dt_AS_Appointment_MST.Columns.Add("nPatientID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("dtStartDate", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("dtStartTime", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("dtEndDate", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("dtEndTime", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("nDuration", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("nColorCode", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("nLocationID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("sLocationName", System.Type.GetType("System.String"));
            dt_AS_Appointment_MST.Columns.Add("nDepartmentID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("sDepartmentName", System.Type.GetType("System.String"));
            dt_AS_Appointment_MST.Columns.Add("nClinicID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_MST.Columns.Add("bIsPARequired", System.Type.GetType("System.Boolean"));
            #endregion

            #region "Create dt_AS_Appointment_DTL"
            DataTable dt_AS_Appointment_DTL = new DataTable();
            dt_AS_Appointment_DTL.Columns.Add("nMSTAppointmentID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("nDTLAppointmentID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("bIsSingleRecurrence", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_DTL.Columns.Add("nLineNumber", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("nAppointmentFlag", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_DTL.Columns.Add("nASBaseID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("sASBaseCode", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL.Columns.Add("sASBaseDesc", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL.Columns.Add("nASBaseFlag", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_DTL.Columns.Add("nRefID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("nRefFlag", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_DTL.Columns.Add("dtStartDate", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("dtStartTime", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("dtEndDate", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("dtEndTime", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("nLocationID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("sLocationName", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL.Columns.Add("nDepartmentID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("sDepartmentName", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL.Columns.Add("nColorCode", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("sNotes", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL.Columns.Add("nClinicID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("sExternalCode", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL.Columns.Add("nExternalID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("nUsedStatus", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("nTemplateAllocationMasterID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("nTemplateAllocationID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL.Columns.Add("bIsPARequired", System.Type.GetType("System.Boolean"));
            #endregion

            #region "Create dt_AS_Appointment_DTL Problem Types"
            DataTable dt_AS_Appointment_DTL_Problem_Types = new DataTable();
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nMSTAppointmentID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nDTLAppointmentID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("bIsSingleRecurrence", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nLineNumber", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nAppointmentFlag", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nASBaseID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("sASBaseCode", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("sASBaseDesc", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nASBaseFlag", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nRefID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nRefFlag", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("dtStartDate", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("dtStartTime", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("dtEndDate", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("dtEndTime", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nLocationID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("sLocationName", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nDepartmentID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("sDepartmentName", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nColorCode", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("sNotes", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nClinicID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("sExternalCode", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nExternalID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nUsedStatus", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nTemplateAllocationMasterID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("nTemplateAllocationID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Problem_Types.Columns.Add("bIsPARequired", System.Type.GetType("System.Boolean"));
            #endregion

            #region "Create dt_AS_Appointment_DTL Resources"
            DataTable dt_AS_Appointment_DTL_Resources = new DataTable();
            dt_AS_Appointment_DTL_Resources.Columns.Add("nMSTAppointmentID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nDTLAppointmentID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("bIsSingleRecurrence", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nLineNumber", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nAppointmentFlag", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nASBaseID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("sASBaseCode", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("sASBaseDesc", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nASBaseFlag", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nRefID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nRefFlag", System.Type.GetType("System.Int16"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("dtStartDate", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("dtStartTime", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("dtEndDate", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("dtEndTime", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nLocationID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("sLocationName", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nDepartmentID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("sDepartmentName", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nColorCode", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("sNotes", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nClinicID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("sExternalCode", System.Type.GetType("System.String"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nExternalID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nUsedStatus", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nTemplateAllocationMasterID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("nTemplateAllocationID", System.Type.GetType("System.Int64"));
            dt_AS_Appointment_DTL_Resources.Columns.Add("bIsPARequired", System.Type.GetType("System.Boolean"));
            #endregion

            #region "Finding Appointment Dates"
            gloAppointmentScheduling.Criteria.FindRecurrences _AppointmentDates = new gloAppointmentScheduling.Criteria.FindRecurrences();
            _AppointmentDates = _AppointmentDates.GetRecurrence(oMasterAppointment.Criteria, oMasterAppointment.ASBaseID, oMasterAppointment.StartTime, oMasterAppointment.EndTime, MasterAppointmentID, oMasterAppointment.ResourceIDS);
            #endregion


            if (_AppointmentDates.Dates.Count > 0)
            {

                #region "Inserting Appointment master record in dt_AS_Appointment_MST table"
                if (IsRecurrenceType == SingleRecurrence.Single)
                {
                    dt_AS_Appointment_MST.Rows.Add();
                    dt_AS_Appointment_MST.Rows[0]["nMSTAppointmentID"] = MasterAppointmentID;
                    dt_AS_Appointment_MST.Rows[0]["bIsSingleRecurrence"] = oMasterAppointment.IsRecurrence.GetHashCode();
                    dt_AS_Appointment_MST.Rows[0]["nAppointmentFlag"] = oMasterAppointment.ASFlag.GetHashCode();
                    dt_AS_Appointment_MST.Rows[0]["nAppointmentTypeID"] = oMasterAppointment.AppointmentTypeID;
                    dt_AS_Appointment_MST.Rows[0]["sAppointmentTypeCode"] = "0";
                    dt_AS_Appointment_MST.Rows[0]["sAppointmentTypeDesc"] = oMasterAppointment.AppointmentTypeDesc;
                    dt_AS_Appointment_MST.Rows[0]["nASBaseID"] = oMasterAppointment.ASBaseID;
                    dt_AS_Appointment_MST.Rows[0]["sASBaseCode"] = "0";
                    if (oMasterAppointment.ASBaseDescription.Length > 255)
                    dt_AS_Appointment_MST.Rows[0]["sASBaseDesc"] = oMasterAppointment.ASBaseDescription.Substring(0,255);
                    else
                    dt_AS_Appointment_MST.Rows[0]["sASBaseDesc"] = oMasterAppointment.ASBaseDescription;

                    dt_AS_Appointment_MST.Rows[0]["nASBaseFlag"] = oMasterAppointment.ASBaseFlag.GetHashCode();
                    dt_AS_Appointment_MST.Rows[0]["nReferralProviderID"] = oMasterAppointment.ReferralProviderID;
                    dt_AS_Appointment_MST.Rows[0]["nReferralProviderCode"] = "0";
                    dt_AS_Appointment_MST.Rows[0]["nReferralProviderName"] = oMasterAppointment.ReferralProviderName;
                    dt_AS_Appointment_MST.Rows[0]["nPatientID"] = oMasterAppointment.PatientID;
                    dt_AS_Appointment_MST.Rows[0]["dtStartDate"] = gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.StartDate.ToString());
                    dt_AS_Appointment_MST.Rows[0]["dtStartTime"] = gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.StartTime.ToString());
                    dt_AS_Appointment_MST.Rows[0]["dtEndDate"] = gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString());
                    dt_AS_Appointment_MST.Rows[0]["dtEndTime"] = gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString());
                    dt_AS_Appointment_MST.Rows[0]["nDuration"] = oMasterAppointment.Duration;
                    dt_AS_Appointment_MST.Rows[0]["nColorCode"] = oMasterAppointment.ColorCode;
                    dt_AS_Appointment_MST.Rows[0]["nLocationID"] = oMasterAppointment.LocationID;
                    dt_AS_Appointment_MST.Rows[0]["sLocationName"] = oMasterAppointment.LocationName;
                    dt_AS_Appointment_MST.Rows[0]["nDepartmentID"] = oMasterAppointment.DepartmentID;
                    dt_AS_Appointment_MST.Rows[0]["sDepartmentName"] = oMasterAppointment.DepartmentName;
                    dt_AS_Appointment_MST.Rows[0]["nClinicID"] = _ClinicID;
                    dt_AS_Appointment_MST.Rows[0]["bIsPaRequired"] = 0;
                }
                #endregion

                nAppCntr = 0;
                for (nAppCntr = 0; nAppCntr <= _AppointmentDates.Dates.Count - 1; nAppCntr++)
                {

                    #region "Inserting Appointment Detail record in dt_AS_Appointment_DTL table"

                    if ((_AppointmentDates.ScheduleStatus[nAppCntr].ToString() != "Blocked") || (oMasterAppointment.IsRecurrence == SingleRecurrence.Single) || (oMasterAppointment.AllowRecurrenceToOverRideBlockeAppointment == true))
                    {

                        dt_AS_Appointment_DTL.Rows.Add();
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nMSTAppointmentID"] = MasterAppointmentID;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nDTLAppointmentID"] = AppointmentID;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["bIsSingleRecurrence"] = IsRecurrenceType.GetHashCode();

                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nLineNumber"] = AppointmentLineNo;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nAppointmentFlag"] = oMasterAppointment.ASFlag.GetHashCode();
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nASBaseID"] = oMasterAppointment.ASBaseID;

                        if (oMasterAppointment.ASBaseCode.Length > 255)
                            dt_AS_Appointment_DTL.Rows[nAppCntr]["sASBaseCode"] = oMasterAppointment.ASBaseCode.Substring(0, 255);
                        else
                            dt_AS_Appointment_DTL.Rows[nAppCntr]["sASBaseCode"] = oMasterAppointment.ASBaseCode;

                        //dt_AS_Appointment_DTL.Rows[nAppCntr]["sASBaseCode"] = oMasterAppointment.ASBaseCode;

                        if (oMasterAppointment.ASBaseDescription.Length > 255)
                            dt_AS_Appointment_DTL.Rows[nAppCntr]["sASBaseDesc"] = oMasterAppointment.ASBaseDescription.Substring(0, 255);
                        else
                            dt_AS_Appointment_DTL.Rows[nAppCntr]["sASBaseDesc"] = oMasterAppointment.ASBaseDescription;

                        //dt_AS_Appointment_DTL.Rows[nAppCntr]["sASBaseDesc"] = oMasterAppointment.ASBaseDescription;

                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nASBaseFlag"] = oMasterAppointment.ASBaseFlag.GetHashCode();
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nRefID"] = 0;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nRefFlag"] = 0;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["dtStartDate"] = gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString());
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["dtStartTime"] = gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.StartTime.ToString());

                        if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                        {
                            dt_AS_Appointment_DTL.Rows[nAppCntr]["dtEndDate"] = gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString());
                            dt_AS_Appointment_DTL.Rows[nAppCntr]["dtEndTime"] = gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString());
                        }
                        else
                        {
                            System.DateTime dtendDate;
                            decimal dDuration;
                            dDuration = oMasterAppointment.Duration;
                            dtendDate = (DateTime)_AppointmentDates.Dates[nAppCntr];
                            dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterAppointment.StartTime.ToShortTimeString());
                            dtendDate = dtendDate.AddMinutes((double)dDuration);
                            dt_AS_Appointment_DTL.Rows[nAppCntr]["dtEndDate"] = gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString());
                            dt_AS_Appointment_DTL.Rows[nAppCntr]["dtEndTime"] = gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString());
                        }

                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nLocationID"] = oMasterAppointment.LocationID;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["sLocationName"] = oMasterAppointment.LocationName;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nDepartmentID"] = oMasterAppointment.DepartmentID;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["sDepartmentName"] = oMasterAppointment.DepartmentName;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nColorCode"] = oMasterAppointment.ColorCode;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["sNotes"] = oMasterAppointment.Notes;

                        Flag = 0;
                        isContain = false;

                        Flag = oMasterAppointment.UsedStatus.GetHashCode();
                        if (oMasterAppointment.DatesWithCommaSeparator != String.Empty)
                        {
                            string[] strArr = oMasterAppointment.DatesWithCommaSeparator.Split(',');
                            if (strArr.Length > 0)
                            {
                                for (int i = 0; i <= strArr.Length - 1; i++)
                                {

                                    if ((strArr[i]).Contains(Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()))))
                                    {
                                        string[] splitString = strArr[i].Split('_');

                                        if (splitString.Length > 0)
                                        {
                                            int usedFlag = 1;
                                            usedFlag = Convert.ToInt16(splitString[1]);
                                            Flag = usedFlag;
                                            isContain = true;
                                            break;
                                        }
                                    }

                                }
                                if (isContain == false)
                                {
                                    Flag = 1;
                                }
                            }
                        }

                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nClinicID"] = oMasterAppointment.ClinicID;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["sExternalCode"] = "0";
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nExternalID"] = 0;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nUsedStatus"] = Flag;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nTemplateAllocationMasterID"] = TemplateAllocationMasterID;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["nTemplateAllocationID"] = TemplateAllocationID;
                        dt_AS_Appointment_DTL.Rows[nAppCntr]["bIsPARequired"] = oMasterAppointment.PARequired;
                    }

                    #endregion

                    #region "Inserting Appointment Detail records (Problem type) in dt_AS_Appointment_DTL table"

                    for (int nPTCntr = 0; nPTCntr <= oMasterAppointment.ProblemTypes.Count - 1; nPTCntr++)
                    {
                        dt_AS_Appointment_DTL_Problem_Types.Rows.Add();
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nMSTAppointmentID"] = MasterAppointmentID;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nDTLAppointmentID"] = 0;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["bIsSingleRecurrence"] = IsRecurrenceType.GetHashCode();

                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nLineNumber"] = AppointmentLineNo;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nAppointmentFlag"] = oMasterAppointment.ASFlag.GetHashCode();
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nASBaseID"] = oMasterAppointment.ProblemTypes[nPTCntr].ASCommonID;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["sASBaseCode"] = oMasterAppointment.ProblemTypes[nPTCntr].ASCommonCode;

                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["sASBaseDesc"] = oMasterAppointment.ProblemTypes[nPTCntr].ASCommonDescription;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nASBaseFlag"] = oMasterAppointment.ProblemTypes[nPTCntr].ASCommonFlag.GetHashCode();

                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nRefID"] = AppointmentID;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nRefFlag"] = oMasterAppointment.ASBaseFlag.GetHashCode();
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["dtStartDate"] = gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString());
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["dtStartTime"] = gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.ProblemTypes[nPTCntr].StartTime.ToString());

                        if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                        {
                            dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["dtEndDate"] = gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString());
                            dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["dtEndTime"] = gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString());
                        }
                        else
                        {
                            System.DateTime dtendDate;
                            decimal dDuration;
                            dDuration = oMasterAppointment.Duration;
                            dtendDate = (DateTime)_AppointmentDates.Dates[nAppCntr];
                            dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterAppointment.StartTime.ToShortTimeString());
                            dtendDate = dtendDate.AddMinutes((double)dDuration);

                            dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["dtEndDate"] = gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString());
                            dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["dtEndTime"] = gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.ProblemTypes[nPTCntr].EndTime.ToString());

                        }

                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nLocationID"] = oMasterAppointment.LocationID;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["sLocationName"] = oMasterAppointment.LocationName;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nDepartmentID"] = oMasterAppointment.DepartmentID;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["sDepartmentName"] = oMasterAppointment.DepartmentName;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nColorCode"] = oMasterAppointment.ColorCode;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["sNotes"] = "";

                        isContain = false;
                        Flag = oMasterAppointment.UsedStatus.GetHashCode();
                        if (oMasterAppointment.DatesWithCommaSeparator != String.Empty)
                        {
                            string[] strArr = oMasterAppointment.DatesWithCommaSeparator.Split(',');
                            if (strArr.Length > 0)
                            {
                                for (int i = 0; i <= strArr.Length - 1; i++)
                                {

                                    if ((strArr[i]).Contains(Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()))))
                                    {
                                        string[] splitString = strArr[i].Split('_');

                                        if (splitString.Length > 0)
                                        {
                                            int usedFlag = 1;
                                            usedFlag = Convert.ToInt16(splitString[1]);
                                            Flag = usedFlag;
                                            isContain = true;
                                            break;
                                        }
                                    }

                                }
                                if (isContain == false)
                                {
                                    Flag = 1;
                                }
                            }
                        }

                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nClinicID"] = oMasterAppointment.ClinicID;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["sExternalCode"] = "0";
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nExternalID"] = 0;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nUsedStatus"] = Flag;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nTemplateAllocationMasterID"] = TemplateAllocationMasterID;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["nTemplateAllocationID"] = TemplateAllocationID;
                        dt_AS_Appointment_DTL_Problem_Types.Rows[nPTCntr]["bIsPARequired"] = oMasterAppointment.PARequired;

                    }

                    #endregion

                    #region "Inserting Appointment Detail records (Resources type) in dt_AS_Appointment_DTL table"

                    for (int nResCntr = 0; nResCntr <= oMasterAppointment.Resources.Count - 1; nResCntr++)
                    {
                        dt_AS_Appointment_DTL_Resources.Rows.Add();
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nMSTAppointmentID"] = MasterAppointmentID;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nDTLAppointmentID"] = 0;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["bIsSingleRecurrence"] = IsRecurrenceType.GetHashCode();

                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nLineNumber"] = AppointmentLineNo;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nAppointmentFlag"] = oMasterAppointment.ASFlag.GetHashCode();
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nASBaseID"] = oMasterAppointment.Resources[nResCntr].ASCommonID;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["sASBaseCode"] = oMasterAppointment.Resources[nResCntr].ASCommonCode;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["sASBaseDesc"] = oMasterAppointment.Resources[nResCntr].ASCommonDescription;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nASBaseFlag"] = oMasterAppointment.Resources[nResCntr].ASCommonFlag.GetHashCode();

                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nRefID"] = AppointmentID;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nRefFlag"] = oMasterAppointment.ASBaseFlag.GetHashCode();
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["dtStartDate"] = gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString());
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["dtStartTime"] = gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.Resources[nResCntr].StartTime.ToString());


                        if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                        {
                            dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["dtEndDate"] = gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString());
                            dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["dtEndTime"] = gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString());
                        }
                        else
                        {
                            System.DateTime dtendDate;
                            decimal dDuration;
                            dDuration = oMasterAppointment.Duration;
                            dtendDate = (DateTime)_AppointmentDates.Dates[nAppCntr];
                            dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterAppointment.StartTime.ToShortTimeString());
                            dtendDate = dtendDate.AddMinutes((double)dDuration);

                            dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["dtEndDate"] = gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString());
                            dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["dtEndTime"] = gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.Resources[nResCntr].EndTime.ToString());
                        }


                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nLocationID"] = oMasterAppointment.LocationID;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["sLocationName"] = oMasterAppointment.LocationName;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nDepartmentID"] = oMasterAppointment.DepartmentID;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["sDepartmentName"] = oMasterAppointment.DepartmentName;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nColorCode"] = oMasterAppointment.ColorCode;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["sNotes"] = "";

                        isContain = false;
                        Flag = oMasterAppointment.UsedStatus.GetHashCode();
                        if (oMasterAppointment.DatesWithCommaSeparator != String.Empty)
                        {
                            string[] strArr = oMasterAppointment.DatesWithCommaSeparator.Split(',');
                            if (strArr.Length > 0)
                            {
                                for (int i = 0; i <= strArr.Length - 1; i++)
                                {

                                    if ((strArr[i]).Contains(Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()))))
                                    {
                                        string[] splitString = strArr[i].Split('_');

                                        if (splitString.Length > 0)
                                        {
                                            int usedFlag = 1;
                                            usedFlag = Convert.ToInt16(splitString[1]);
                                            Flag = usedFlag;
                                            isContain = true;
                                            break;
                                        }
                                    }

                                }
                                if (isContain == false)
                                {
                                    Flag = 1;
                                }
                            }
                        }

                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nClinicID"] = oMasterAppointment.ClinicID;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["sExternalCode"] = "0";
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nExternalID"] = 0;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nUsedStatus"] = Flag;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nTemplateAllocationMasterID"] = TemplateAllocationMasterID;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["nTemplateAllocationID"] = TemplateAllocationID;
                        dt_AS_Appointment_DTL_Resources.Rows[nResCntr]["bIsPARequired"] = oMasterAppointment.PARequired;
                    }

                    #endregion

                }

            }

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            
            oDB.Connect(false);
            oDBParameters.Add("@TVP_AS_Appointment_MST", dt_AS_Appointment_MST, ParameterDirection.Input, SqlDbType.Structured);
            oDBParameters.Add("@TVP_AS_Appointment_DTL", dt_AS_Appointment_DTL, ParameterDirection.Input, SqlDbType.Structured);
            oDBParameters.Add("@TVP_AS_Appointment_DTL_Problem_Types", dt_AS_Appointment_DTL_Problem_Types, ParameterDirection.Input, SqlDbType.Structured);
            oDBParameters.Add("@TVP_AS_Appointment_DTL_Resources", dt_AS_Appointment_DTL_Resources , ParameterDirection.Input, SqlDbType.Structured);
            oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
            oDB.Execute("Appt_Update_AppointmentRecord", oDBParameters);
            oDB.Disconnect();

            oDBParameters.Clear();
            oDBParameters.Dispose();
            oDBParameters = null;

            oDB.Dispose();
            oDB = null;


            #region " PA Transaction Update / Delete "

            if (oMasterAppointment.PATransaction.IsDeleted)
            { PriorAuthorizationTransaction.Delete(MasterAppointmentID, AppointmentID); }

            if (PriorAuthorizationTransaction.IsExist(oMasterAppointment.PATransaction.MasterAppointmentID, oMasterAppointment.PATransaction.DetailAppointmentID))
            {
                PriorAuthorizationTransaction.Update(oMasterAppointment.PATransaction);
            }
            else
            {
                if (oMasterAppointment.PATransaction.PriorAuthorizationID != 0)
                {
                    PriorAuthorizationTransaction.Insert(oMasterAppointment.PATransaction);
                }
            }

            #endregion


            #region "Generate HL7 Message Queue for Modify Appointment"
            // Code Start - Added by kanchan on 20091205 for HL7 appointment outbound
            if (gloHL7.boolSendAppointmentDetails) //(appSettings["GenerateHL7Message"] != null)
            {
                if (AppointmentID > 0)
                {
                    //Added by Abhijeet on 20100909 
                    //Changes: If provider details are removed in modified appointment then generating S15 file for that provider                                       
                    //gloHL7.InsertInMessageQueue("S13", oMasterAppointment.PatientID, _nMasterAppointmentID, "", _databaseconnectionstring);
                    if (gloHL7.nOldBaseId != 0 && oMasterAppointment.ASBaseID == 0)
                    { //it means provider removed from appointment so make delete entry                                        
                        gloHL7.InsertInMessageQueue("S15", oMasterAppointment.PatientID, MasterAppointmentID, "", _databaseconnectionstring, true);
                    }
                    else
                    {
                        gloHL7.InsertInMessageQueue("S13", oMasterAppointment.PatientID, MasterAppointmentID, "", _databaseconnectionstring);
                    }
                }
            }
            #endregion

            //#region "Appointment Status"

            //if (gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()))
            //{
            //    gloDatabaseLayer.DBLayer oDBUpdateTrackingStatus = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //    try
            //    {
            //        oDBUpdateTrackingStatus.Connect(false);
            //        String _strSQL = "UPDATE PatientTracking SET nTrackingStatus= " + oMasterAppointment.AppointmentStatusID + " WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nDTLAppointmentID = " + AppointmentID + " AND  nClinicID = " + this._ClinicID;
            //        oDBUpdateTrackingStatus.Execute_Query(_strSQL);
            //        oDBUpdateTrackingStatus.Disconnect();
            //    }
            //    catch (Exception ex)
            //    {
            //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //        ex = null;
            //    }
            //    finally
            //    {
            //        if (oDBUpdateTrackingStatus != null) 
            //        { 
            //            oDBUpdateTrackingStatus.Dispose();
            //            oDBUpdateTrackingStatus = null;
            //        }
            //    }
            //}

            //gloDatabaseLayer.DBLayer oDBTrackingStatus = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //try
            //{
            //    oDBTrackingStatus.Connect(false);
            //    String _strSQL = "UPDATE PatientTracking SET npatientid= " + oMasterAppointment.PatientID + " WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nDTLAppointmentID = " + AppointmentID + " AND  nClinicID = " + this._ClinicID;
            //    oDBTrackingStatus.Execute_Query(_strSQL);
            //    oDBTrackingStatus.Disconnect();
            //}
            //catch (Exception ex)
            //{
            //    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            //    ex = null;
            //}
            //finally
            //{
            //    if (oDBTrackingStatus != null) 
            //    { 
            //        oDBTrackingStatus.Dispose();
            //        oDBTrackingStatus = null; 
            //    }
            //}

            //#endregion


            return true;
        }

        private bool Update(MasterAppointment oMasterAppointment, SingleRecurrence IsRecurrenceType, Int64 MasterAppointmentID, Int64 AppointmentID, Int64 ClinicID)
        {

            bool _result = false;
            Int64 _nMasterAppointmentID = MasterAppointmentID;
            Int64 _nDetailAppointmentID = AppointmentID;
            string _AppTypeCode = "0";
            string _ProviderCode = "0";
            string _ReferralProviderCode = "0";
            DataTable dt = new DataTable();


            int nAppCntr = 0;
            int nPTCntr = 0;
            int nResCntr = 0;
            Int64 _AppTempLineNumber = 0;
            Object objectID;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            gloAppointmentScheduling.Criteria.FindRecurrences _AppointmentDates = new gloAppointmentScheduling.Criteria.FindRecurrences();
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);
            frmSetupAppointment oSetupAppointment = new frmSetupAppointment(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);

                _AppointmentDates = _AppointmentDates.GetRecurrence(oMasterAppointment.Criteria, oMasterAppointment.ASBaseID, oMasterAppointment.StartTime, oMasterAppointment.EndTime, _nMasterAppointmentID, oMasterAppointment.ResourceIDS);


                // by Pranit on 19 sep to check provider is blocked or not and to check whether Provider is blocked.
                bool isResourcesBlocked = false;
                bool isProviderBlocked = false;

                if ((_AppointmentDates.Dates.Count > 0) && isResourcesBlocked == false && isProviderBlocked == false)
                {

                    #region "Update Master Appointment"
                    oDBParameters.Clear();
                    if (IsRecurrenceType == SingleRecurrence.Single)
                    {
                        oDBParameters.Add("@MSTAppointmentID", MasterAppointmentID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                        oDBParameters.Add("@IsSingleRecurrence", oMasterAppointment.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                        oDBParameters.Add("@AppointmentFlag", oMasterAppointment.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                        oDBParameters.Add("@AppointmentTypeID", oMasterAppointment.AppointmentTypeID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@AppointmentTypeCode", _AppTypeCode, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@AppointmentTypeDesc", oMasterAppointment.AppointmentTypeDesc, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@ASBaseID", oMasterAppointment.ASBaseID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@ASBaseCode", _ProviderCode, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@ASBaseDesc", oMasterAppointment.ASBaseDescription, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@ASBaseFlag", oMasterAppointment.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                        oDBParameters.Add("@ReferralProviderID", oMasterAppointment.ReferralProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@ReferralProviderCode", _ReferralProviderCode, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@ReferralProviderName", oMasterAppointment.ReferralProviderName, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@PatientID", oMasterAppointment.PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@StartDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.StartDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@Duration", oMasterAppointment.Duration, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@ColorCode", oMasterAppointment.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@LocationID", oMasterAppointment.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@LocationName", oMasterAppointment.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@DepartmentID", oMasterAppointment.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@DepartmentName", oMasterAppointment.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                        oDBParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                        oDB.Execute("AS_UPDATE_Appointment_MST", oDBParameters, out  objectID);
                        if (objectID == null) { return false; }
                        _nMasterAppointmentID = (Int64)objectID;
                    }
                    #endregion

                    int Flag = 0;
                    bool isContain = false;

                    #region "Provider Appointments"
                    for (nAppCntr = 0; nAppCntr <= _AppointmentDates.Dates.Count - 1; nAppCntr++)
                    {
                        oDBParameters.Clear();

                        // By Pranit on 15 sep to not allow entry in Database whose status is Blocked By Pranit on 15 sep Added or condition to check is it single recurrence.

                        if ((_AppointmentDates.ScheduleStatus[nAppCntr].ToString() != "Blocked") || (oMasterAppointment.IsRecurrence == SingleRecurrence.Single) || (oMasterAppointment.AllowRecurrenceToOverRideBlockeAppointment == true))
                        {
                            object _lineresult = new object();
                            _lineresult = oDB.ExecuteScalar_Query("SELECT ISNULL(MAX(nLineNumber),0) + 1 FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE nASBaseID = " + oMasterAppointment.ASBaseID + " AND nASBaseFlag  = " + oMasterAppointment.ASBaseFlag.GetHashCode() + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()) + " AND nClinicID = " + oMasterAppointment.ClinicID + "");
                            if (_lineresult != null)
                            {
                                if (_lineresult.ToString() != "")
                                {
                                    _AppTempLineNumber = Convert.ToInt64(_lineresult.ToString());
                                }
                            }
                            _lineresult = null;

                            #region "Provider Appointments"
                            oDBParameters.Add("@MSTAppointmentID", _nMasterAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@DTLAppointmentID", _nDetailAppointmentID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            oDBParameters.Add("@IsSingleRecurrence", IsRecurrenceType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@LineNumber", _AppTempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@AppointmentFlag", oMasterAppointment.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@ASBaseID", oMasterAppointment.ASBaseID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@ASBaseCode", oMasterAppointment.ASBaseCode, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@ASBaseDesc", oMasterAppointment.ASBaseDescription, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@ASBaseFlag", oMasterAppointment.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@RefID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@RefFlag", 0, ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@StartDate", gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                            if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                            {
                                oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            }
                            else
                            {
                                System.DateTime dtendDate;
                                decimal dDuration;
                                dDuration = oMasterAppointment.Duration;
                                dtendDate = (DateTime)_AppointmentDates.Dates[nAppCntr];
                                dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterAppointment.StartTime.ToShortTimeString());
                                dtendDate = dtendDate.AddMinutes((double)dDuration);

                                oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            }

                            oDBParameters.Add("@LocationID", oMasterAppointment.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@LocationName", oMasterAppointment.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@DepartmentID", oMasterAppointment.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@DepartmentName", oMasterAppointment.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@ColorCode", oMasterAppointment.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@Notes", oMasterAppointment.Notes, ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                            oDBParameters.Add("@ClinicID", oMasterAppointment.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@ExternalCode", "0", ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@ExternalID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                            isContain = false;
                            Flag = oMasterAppointment.UsedStatus.GetHashCode();
                            if (oMasterAppointment.DatesWithCommaSeparator != String.Empty)
                            {
                                string[] strArr = oMasterAppointment.DatesWithCommaSeparator.Split(',');
                                if (strArr.Length > 0)
                                {
                                    for (int i = 0; i <= strArr.Length - 1; i++)
                                    {
                                        if ((strArr[i]).Contains(Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()))))
                                        {
                                            string[] splitString = strArr[i].Split('_');

                                            if (splitString.Length > 0)
                                            {
                                                int usedFlag = 1;
                                                usedFlag = Convert.ToInt16(splitString[1]);
                                                Flag = usedFlag;
                                                isContain = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (isContain == false)
                                    {
                                        Flag = 1;
                                    }
                                }
                            }

                            oDBParameters.Add("@nUsedStatus", Flag, ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@PARequired", oMasterAppointment.PARequired, ParameterDirection.Input, SqlDbType.Bit);
                            oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);

                            objectID = new object();
                            oDB.Execute("AS_UPDATE_Appointment_DTL", oDBParameters, out  objectID);
                            #endregion


                            if (objectID == null)
                            { return false; }

                            _nDetailAppointmentID = (Int64)objectID;

                            oMasterAppointment.PATransaction.AppointmentDate = gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString());

                            #region " PA Transaction Update / Delete "

                            if (oMasterAppointment.PATransaction.IsDeleted)
                            { PriorAuthorizationTransaction.Delete(_nMasterAppointmentID, _nDetailAppointmentID); }

                            if (PriorAuthorizationTransaction.IsExist(oMasterAppointment.PATransaction.MasterAppointmentID, oMasterAppointment.PATransaction.DetailAppointmentID))
                            {
                                PriorAuthorizationTransaction.Update(oMasterAppointment.PATransaction);
                            }
                            else
                            {
                                if (oMasterAppointment.PATransaction.PriorAuthorizationID != 0)
                                {
                                    PriorAuthorizationTransaction.Insert(oMasterAppointment.PATransaction);
                                }
                            }

                            #endregion

                            objectID = null;

                            #region "Provider Appointments - Problem Types "
                            for (nPTCntr = 0; nPTCntr <= oMasterAppointment.ProblemTypes.Count - 1; nPTCntr++)
                            {
                                oDBParameters.Clear();
                                oDBParameters.Add("@MSTAppointmentID", _nMasterAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@DTLAppointmentID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@IsSingleRecurrence", IsRecurrenceType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@LineNumber", _AppTempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@AppointmentFlag", oMasterAppointment.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@ASBaseID", oMasterAppointment.ProblemTypes[nPTCntr].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@ASBaseCode", oMasterAppointment.ProblemTypes[nPTCntr].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ASBaseDesc", oMasterAppointment.ProblemTypes[nPTCntr].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ASBaseFlag", oMasterAppointment.ProblemTypes[nPTCntr].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@RefID", _nDetailAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@RefFlag", oMasterAppointment.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@StartDate", gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.ProblemTypes[nPTCntr].StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                                if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                                {
                                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }
                                else
                                {
                                    System.DateTime dtendDate;
                                    decimal dDuration;
                                    dDuration = oMasterAppointment.Duration;
                                    dtendDate = (DateTime)_AppointmentDates.Dates[nAppCntr];
                                    dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterAppointment.StartTime.ToShortTimeString());
                                    dtendDate = dtendDate.AddMinutes((double)dDuration);
                                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.ProblemTypes[nPTCntr].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }

                                oDBParameters.Add("@LocationID", oMasterAppointment.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@LocationName", oMasterAppointment.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@DepartmentID", oMasterAppointment.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@DepartmentName", oMasterAppointment.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ColorCode", oMasterAppointment.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@Notes", "", ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                oDBParameters.Add("@ClinicID", oMasterAppointment.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@ExternalCode", "0", ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ExternalID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                                isContain = false;
                                Flag = oMasterAppointment.UsedStatus.GetHashCode();
                                if (oMasterAppointment.DatesWithCommaSeparator != String.Empty)
                                {
                                    string[] strArr = oMasterAppointment.DatesWithCommaSeparator.Split(',');
                                    if (strArr.Length > 0)
                                    {
                                        for (int i = 0; i <= strArr.Length - 1; i++)
                                        {
                                            if ((strArr[i]).Contains(Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()))))
                                            {
                                                string[] splitString = strArr[i].Split('_');

                                                if (splitString.Length > 0)
                                                {
                                                    int usedFlag = 1;
                                                    usedFlag = Convert.ToInt16(splitString[1]);
                                                    Flag = usedFlag;
                                                    isContain = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (isContain == false)
                                        {
                                            Flag = 1;
                                        }
                                    }
                                }
                                oDBParameters.Add("@nUsedStatus", Flag, ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@PARequired", oMasterAppointment.PARequired, ParameterDirection.Input, SqlDbType.Bit);
                                oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDB.Execute("AS_INSERT_Appointment_DTL", oDBParameters, out  objectID);
                            }
                            #endregion


                            #region "Provider Appointments - Resources"
                            for (nResCntr = 0; nResCntr <= oMasterAppointment.Resources.Count - 1; nResCntr++)
                            {
                                oDBParameters.Clear();
                                oDBParameters.Add("@MSTAppointmentID", _nMasterAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@DTLAppointmentID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@IsSingleRecurrence", IsRecurrenceType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@LineNumber", _AppTempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@AppointmentFlag", oMasterAppointment.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@ASBaseID", oMasterAppointment.Resources[nResCntr].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@ASBaseCode", oMasterAppointment.Resources[nResCntr].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ASBaseDesc", oMasterAppointment.Resources[nResCntr].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ASBaseFlag", oMasterAppointment.Resources[nResCntr].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@RefID", _nDetailAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@RefFlag", oMasterAppointment.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@StartDate", gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.Resources[nResCntr].StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                                if (oMasterAppointment.IsRecurrence == SingleRecurrence.Single)
                                {
                                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }
                                else
                                {
                                    System.DateTime dtendDate;
                                    decimal dDuration;
                                    dDuration = oMasterAppointment.Duration;
                                    dtendDate = (DateTime)_AppointmentDates.Dates[nAppCntr];
                                    dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterAppointment.StartTime.ToShortTimeString());
                                    dtendDate = dtendDate.AddMinutes((double)dDuration);

                                    oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.Resources[nResCntr].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }

                                oDBParameters.Add("@LocationID", oMasterAppointment.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@LocationName", oMasterAppointment.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@DepartmentID", oMasterAppointment.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@DepartmentName", oMasterAppointment.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ColorCode", oMasterAppointment.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@Notes", oMasterAppointment.Notes, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ClinicID", oMasterAppointment.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@ExternalCode", "0", ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@ExternalID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@PARequired", oMasterAppointment.PARequired, ParameterDirection.Input, SqlDbType.Bit);

                                isContain = false;
                                Flag = oMasterAppointment.UsedStatus.GetHashCode();
                                if (oMasterAppointment.DatesWithCommaSeparator != String.Empty)
                                {
                                    string[] strArr = oMasterAppointment.DatesWithCommaSeparator.Split(',');
                                    if (strArr.Length > 0)
                                    {
                                        for (int i = 0; i <= strArr.Length - 1; i++)
                                        {
                                            if ((strArr[i]).Contains(Convert.ToString(gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()))))
                                            {
                                                string[] splitString = strArr[i].Split('_');

                                                if (splitString.Length > 0)
                                                {
                                                    int usedFlag = 1;
                                                    usedFlag = Convert.ToInt16(splitString[1]);
                                                    Flag = usedFlag;
                                                    isContain = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (isContain == false)
                                        {
                                            Flag = 1;
                                        }
                                    }
                                }

                                oDBParameters.Add("@nUsedStatus", Flag, ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nUserID", _UserID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDB.Execute("AS_INSERT_Appointment_DTL", oDBParameters, out  objectID);
                            }
                            #endregion

                            #region "Appointment Status"

                            if (gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.StartDate.ToShortDateString()) > gloDateMaster.gloDate.DateAsNumber(System.DateTime.Now.ToShortDateString()))
                            {

                                gloDatabaseLayer.DBLayer oDBUpdateTrackingStatus = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                try
                                {
                                    oDBUpdateTrackingStatus.Connect(false);
                                    String _strSQL = "UPDATE PatientTracking SET nTrackingStatus= " + oMasterAppointment.AppointmentStatusID + " "
                                        + " WHERE nMSTAppointmentID = " + _nMasterAppointmentID + " AND nDTLAppointmentID = " + _nDetailAppointmentID + " "
                                        + " AND  nClinicID = " + this._ClinicID + " ";
                                    oDBUpdateTrackingStatus.Execute_Query(_strSQL);
                                    oDBUpdateTrackingStatus.Disconnect();
                                }
                                catch (Exception ex)
                                {
                                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                    ex = null;
                                }
                                finally
                                {
                                    if (oDBUpdateTrackingStatus != null) { oDBUpdateTrackingStatus.Dispose(); }
                                }
                            }

                            //to update status if user changes patient of appointment
                            gloDatabaseLayer.DBLayer oDBTrackingStatus = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                            try
                            {
                                oDBTrackingStatus.Connect(false);
                                String _strSQL = "UPDATE PatientTracking SET npatientid= " + oMasterAppointment.PatientID + " "
                                    + " WHERE nMSTAppointmentID = " + _nMasterAppointmentID + " AND nDTLAppointmentID = " + _nDetailAppointmentID + " "
                                    + " AND  nClinicID = " + this._ClinicID + " ";
                                oDBTrackingStatus.Execute_Query(_strSQL);
                                oDBTrackingStatus.Disconnect();
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                ex = null;
                            }
                            finally
                            {
                                if (oDBTrackingStatus != null) { oDBTrackingStatus.Dispose(); }
                            }
                            #endregion
                        }
                    }

                    #region "Generate HL7 Message Queue for Modify Appointment"
                    // Code Start - Added by kanchan on 20091205 for HL7 appointment outbound
                    if (gloHL7.boolSendAppointmentDetails) //(appSettings["GenerateHL7Message"] != null)
                    {
                        if (_nDetailAppointmentID > 0)
                        {
                            //Added by Abhijeet on 20100909 
                            //Changes: If provider details are removed in modified appointment then generating S15 file for that provider                                       
                            //gloHL7.InsertInMessageQueue("S13", oMasterAppointment.PatientID, _nMasterAppointmentID, "", _databaseconnectionstring);
                            if (gloHL7.nOldBaseId != 0 && oMasterAppointment.ASBaseID == 0)
                            { //it means provider removed from appointment so make delete entry                                        
                                gloHL7.InsertInMessageQueue("S15", oMasterAppointment.PatientID, _nMasterAppointmentID, "", _databaseconnectionstring, true);
                            }
                            else
                            {
                                gloHL7.InsertInMessageQueue("S13", oMasterAppointment.PatientID, _nMasterAppointmentID, "", _databaseconnectionstring);
                            }
                        }
                    }
                    #endregion

                    #endregion

                    _result = true;
                }

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                _nMasterAppointmentID = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _nMasterAppointmentID = 0;
            }
            finally
            {
                oDB.Dispose();
                oDBParameters.Dispose();
                objectID = null;
                _AppointmentDates.Dispose();
            }
            return _result;

        }

        public MasterAppointment GetMasterAppointment(Int64 MasterAppointmentID, Int64 AppointmentID, SingleRecurrence RetriveMethod, SingleRecurrence RetriveMasterMethod, bool RetriveSingleInRecurrence, Int64 ClinicID)
        {
            MasterAppointment oMasterAppointment = new MasterAppointment();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            object _intresult = new object();
            DataTable oData = new DataTable();
            bool _FoundMasterRecord = false;

            Int64 _ProviderIDforProbTypeNRes = 0;
            Int64 _StartDateforProbTypeNRes = 0;
            Int64 _EndDateforProbTypeNRes = 0;

            ShortApointmentSchedule oProblemType = new ShortApointmentSchedule();
            ShortApointmentSchedule oResource = new ShortApointmentSchedule();

            try
            {
                oDB.Connect(false);

                oMasterAppointment.MasterID = 0;

                #region "Retrive Master Information"
                _strSQL = " SELECT appt_mst.nMSTAppointmentID, appt_mst.bIsSingleRecurrence, appt_mst.nAppointmentFlag, " +
                            " ISNULL(appt_mst.nAppointmentTypeID,0) AS nAppointmentTypeID,  ISNULL(appt_mst.sAppointmentTypeCode,'') AS sAppointmentTypeCode, " +
                            " ISNULL(appt_mst.sAppointmentTypeDesc,'') AS sAppointmentTypeDesc,  ISNULL(appt_mst.nASBaseID,0) AS nASBaseID, " +
                            " ISNULL(appt_mst.sASBaseCode,'') AS sASBaseCode, ISNULL(appt_mst.sASBaseDesc,'') AS sASBaseDesc, appt_mst.nASBaseFlag,  " +
                            " ISNULL(appt_mst.nReferralProviderID,0) AS nReferralProviderID,  ISNULL(appt_mst.nReferralProviderCode,'') AS nReferralProviderCode, " +
                            " LTRIM(ISNULL(appt_mst.nReferralProviderName,'')) AS nReferralProviderName,  appt_mst.nPatientID, appt_mst.dtStartDate, " +
                            " appt_mst.dtStartTime, appt_mst.dtEndDate, appt_mst.dtEndTime, appt_mst.nDuration, appt_mst.nColorCode,  " +
                            " ISNULL(appt_mst.nLocationID,0) AS nLocationID, ISNULL(appt_mst.sLocationName,'') AS sLocationName,  " +
                            " ISNULL(appt_mst.nDepartmentID,0) AS nDepartmentID, ISNULL(appt_mst.sDepartmentName,'') AS sDepartmentName, " +
                            " appt_mst.nClinicID, appt_dtl.bIsPARequired,ISNULL(appt_dtl.sNotes,'') AS sNotes  " +
                            " FROM AS_Appointment_MST as appt_mst  WITH(NOLOCK) " +
                            " INNER JOIN AS_Appointment_DTL as appt_dtl  WITH(NOLOCK) ON appt_mst.nMSTAppointmentID = appt_dtl.nMSTAppointmentID " +
                            " WHERE appt_mst.nClinicID = 1 AND  appt_mst.bIsSingleRecurrence IS NOT NULL " +
                            " AND appt_mst.nAppointmentFlag IS NOT NULL AND appt_mst.dtStartDate IS NOT NULL " +
                            " AND appt_mst.dtStartTime IS NOT NULL AND appt_mst.dtEndDate IS NOT NULL " +
                            " AND appt_mst.dtEndTime IS NOT NULL " +
                            " AND appt_mst.nMSTAppointmentID = " + MasterAppointmentID +
                            " AND appt_dtl.nDTLAppointmentID = " + AppointmentID;

                oDB.Retrive_Query(_strSQL, out oData);

                if (oData != null)
                {
                    if (oData.Rows.Count > 0)
                    {
                        for (int i = 0; i <= oData.Rows.Count - 1; i++)
                        {
                            _FoundMasterRecord = true;
                            oMasterAppointment.IsRecurrence = (SingleRecurrence)Convert.ToInt32(oData.Rows[i]["bIsSingleRecurrence"]);
                            oMasterAppointment.ASFlag = (AppointmentScheduleFlag)Convert.ToInt32(oData.Rows[i]["nAppointmentFlag"]);
                            oMasterAppointment.AppointmentTypeID = GetAppointmentTypeID(Convert.ToInt64(oData.Rows[i]["nAppointmentTypeID"].ToString()), oData.Rows[i]["sAppointmentTypeDesc"].ToString(), ClinicID);
                            oMasterAppointment.AppointmentTypeCode = oData.Rows[i]["sAppointmentTypeCode"].ToString();
                            oMasterAppointment.AppointmentTypeDesc = oData.Rows[i]["sAppointmentTypeDesc"].ToString();
                            _ProviderIDforProbTypeNRes = Convert.ToInt64(oData.Rows[i]["nASBaseID"].ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            oMasterAppointment.ASBaseID = _ProviderIDforProbTypeNRes;
                            oMasterAppointment.ASBaseCode = oData.Rows[i]["sASBaseCode"].ToString();
                            oMasterAppointment.ASBaseDescription = GetProviderName(_ProviderIDforProbTypeNRes, ClinicID);
                            oMasterAppointment.ASBaseFlag = (ASBaseType)Convert.ToInt32(oData.Rows[i]["nAppointmentFlag"]);
                            oMasterAppointment.ReferralProviderID = Convert.ToInt64(oData.Rows[i]["nReferralProviderID"].ToString()); //Remark
                            oMasterAppointment.ReferralProviderCode = oData.Rows[i]["nReferralProviderCode"].ToString(); //Remark
                            oMasterAppointment.ReferralProviderName = oData.Rows[i]["nReferralProviderName"].ToString(); //Remark
                            oMasterAppointment.PatientID = Convert.ToInt64(oData.Rows[i]["nPatientID"].ToString());
                            oMasterAppointment.PatientName = GetPatientName(oMasterAppointment.PatientID, ClinicID);
                            _StartDateforProbTypeNRes = Convert.ToInt64(oData.Rows[i]["dtStartDate"].ToString());
                            oMasterAppointment.StartDate = gloDateMaster.gloDate.DateAsDate(_StartDateforProbTypeNRes);
                            oMasterAppointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(oMasterAppointment.StartDate, Convert.ToInt32(oData.Rows[i]["dtStartTime"].ToString()));
                            _EndDateforProbTypeNRes = Convert.ToInt64(oData.Rows[i]["dtEndDate"].ToString());
                            oMasterAppointment.EndDate = gloDateMaster.gloDate.DateAsDate(_EndDateforProbTypeNRes);
                            oMasterAppointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(oMasterAppointment.EndDate, Convert.ToInt32(oData.Rows[i]["dtEndTime"].ToString()));
                            oMasterAppointment.Duration = Convert.ToDecimal(oData.Rows[i]["nDuration"].ToString());
                            oMasterAppointment.ColorCode = Convert.ToInt32(oData.Rows[i]["nColorCode"].ToString());
                            oMasterAppointment.LocationID = GetLocationID(oData.Rows[i]["sLocationName"].ToString(), ClinicID);
                            oMasterAppointment.LocationName = oData.Rows[i]["sLocationName"].ToString();
                            oMasterAppointment.DepartmentID = GetDepartmentID(oData.Rows[i]["sDepartmentName"].ToString(), oMasterAppointment.LocationID, ClinicID);
                            oMasterAppointment.DepartmentName = oData.Rows[i]["sDepartmentName"].ToString();
                            oMasterAppointment.Notes = oData.Rows[i]["sNotes"].ToString();
                            oMasterAppointment.ClinicID = ClinicID;
                            oMasterAppointment.UsedStatus = ASUsedStatus.Unknown5;
                            oMasterAppointment.PARequired = Convert.ToBoolean(oData.Rows[i]["bIsPARequired"]);
                        }
                    }
                }
                oData.Dispose();
                #endregion

                if (_FoundMasterRecord == true)
                {
                    #region "Retrive Criteria"
                    oData = new DataTable();
                    _strSQL = "SELECT ISNULL(bIsSingleRecurrence,0) AS bIsSingleRecurrence, " +
                    " ISNULL(nRecurrence_PatternType,0) AS nRecurrence_PatternType,  " +
                    " ISNULL(nRecurrence_Pattern_Daily_EveryDayNo,0) AS nRecurrence_Pattern_Daily_EveryDayNo,  " +
                    " ISNULL(nRecurrence_Pattern_Daily_EveryDayOrWeekDay,0) AS nRecurrence_Pattern_Daily_EveryDayOrWeekDay,  " +
                    " ISNULL(nRecurrence_Pattern_Weekly_EveryWeekNo,0) AS  nRecurrence_Pattern_Weekly_EveryWeekNo,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Sunday,0) AS bRecurrence_Pattern_Weekly_Sunday,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Monday,0) AS bRecurrence_Pattern_Weekly_Monday,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Tuesday,0) AS bRecurrence_Pattern_Weekly_Tuesday,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Wednesday,0) AS bRecurrence_Pattern_Weekly_Wednesday,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Thursday,0) AS bRecurrence_Pattern_Weekly_Thursday,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Friday,0) AS bRecurrence_Pattern_Weekly_Friday,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Saturday,0) AS bRecurrence_Pattern_Weekly_Saturday,  " +
                    " ISNULL(nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria,0) AS nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria,  " +
                    " ISNULL(nRecurrence_Pattern_Monthly_DayNumber,0) AS nRecurrence_Pattern_Monthly_DayNumber,  " +
                    " ISNULL(nRecurrence_Pattern_Monthly_EveryMonthNumber,0) AS nRecurrence_Pattern_Monthly_EveryMonthNumber,  " +
                    " ISNULL(nRecurrence_Pattern_Monthly_FstLstCriteriaID,0) AS nRecurrence_Pattern_Monthly_FstLstCriteriaID,  " +
                    " ISNULL(nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID,0) AS nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID,  " +
                    " ISNULL(nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria,0) AS nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria,  " +
                    " ISNULL(nRecurrence_Pattern_Yearly_DayNumber,0) AS nRecurrence_Pattern_Yearly_DayNumber,  " +
                    " ISNULL(nRecurrence_Pattern_Yearly_MonthOfCriteriaID,0) AS nRecurrence_Pattern_Yearly_MonthOfCriteriaID,  " +
                    " ISNULL(nRecurrence_Pattern_Yearly_FstLstCriteriaID,0) AS nRecurrence_Pattern_Yearly_FstLstCriteriaID,  " +
                    " ISNULL(nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID,0) AS nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID,  " +
                    " ISNULL(bRange_Flag,0) AS bRange_Flag,  " +
                    " ISNULL(nRange_StartDate,0) AS nRange_StartDate,  " +
                    " ISNULL(nRange_StartTime,0) AS nRange_StartTime,  " +
                    " ISNULL(nRange_EndDate,0) AS nRange_EndDate,  " +
                    " ISNULL(nRange_EndTime,0) AS nRange_EndTime,  " +
                    " ISNULL(nRange_Duration,0) AS nRange_Duration,  " +
                    " ISNULL(nRange_NoOfOccurence,0) AS nRange_NoOfOccurence,  " +
                    " ISNULL(nRange_NoEndDateYear,0) AS nRange_NoEndDateYear " +
                    " FROM AS_Appointment_MST_Criteria WHERE nMasterAppointmentID = " + MasterAppointmentID + " and nClinicID = " + ClinicID + "";

                    oDB.Retrive_Query(_strSQL, out oData);

                    if (oData != null)
                    {
                        if (oData.Rows.Count > 0)
                        {
                            oMasterAppointment.Criteria.SingleRecurrenceAppointment = (SingleRecurrence)Convert.ToInt32(oData.Rows[0]["bIsSingleRecurrence"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag = (RecurrenceRangeFlag)Convert.ToInt32(oData.Rows[0]["bRange_Flag"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Range.StartDate = Convert.ToInt32(oData.Rows[0]["nRange_StartDate"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.CriteriaDateTime.StartTime = Convert.ToInt64(oData.Rows[0]["nRange_StartTime"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndDate = Convert.ToInt32(oData.Rows[0]["nRange_EndDate"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.CriteriaDateTime.EndTime = Convert.ToInt64(oData.Rows[0]["nRange_EndTime"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.CriteriaDateTime.Duration = Convert.ToDecimal(oData.Rows[0]["nRange_Duration"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber = Convert.ToInt64(oData.Rows[0]["nRange_NoOfOccurence"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Range.NoEndDateYear = Convert.ToInt64(oData.Rows[0]["nRange_NoEndDateYear"]);

                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = (RecurrencePatternType)Convert.ToInt32(oData.Rows[0]["nRecurrence_PatternType"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = Convert.ToInt64(oData.Rows[0]["nRecurrence_Pattern_Daily_EveryDayNo"]);

                            //nRecurrence_Pattern_Daily_EveryDayOrWeekDay, nRecurrence_Pattern_Weekly_EveryWeekNo, bRecurrence_Pattern_Weekly_Sunday, 
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = (RecurrencePatternFlag)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Daily_EveryDayOrWeekDay"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber = Convert.ToInt64(oData.Rows[0]["nRecurrence_Pattern_Weekly_EveryWeekNo"]);

                            //bRecurrence_Pattern_Weekly_Monday, bRecurrence_Pattern_Weekly_Tuesday, bRecurrence_Pattern_Weekly_Wednesday, 
                            //bRecurrence_Pattern_Weekly_Thursday, bRecurrence_Pattern_Weekly_Friday, bRecurrence_Pattern_Weekly_Saturday, 
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Sunday"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Monday"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Tuesday"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Wednesday"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Thursday"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Friday"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Saturday"]);


                            //nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria, nRecurrence_Pattern_Monthly_DayNumber, nRecurrence_Pattern_Monthly_EveryMonthNumber, 
                            //nRecurrence_Pattern_Monthly_FstLstCriteriaID, nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID, nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria,

                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = (RecurrencePatternFlag)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = Convert.ToInt64(oData.Rows[0]["nRecurrence_Pattern_Monthly_DayNumber"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(oData.Rows[0]["nRecurrence_Pattern_Monthly_EveryMonthNumber"]);

                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = (FirstLastCriteria)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Monthly_FstLstCriteriaID"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = (gloAppointmentScheduling.DayWeekday)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = (RecurrencePatternFlag)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria"]);

                            //nRecurrence_Pattern_Yearly_DayNumber, nRecurrence_Pattern_Yearly_MonthOfCriteriaID, nRecurrence_Pattern_Yearly_FstLstCriteriaID,
                            //nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID, nClinicID              

                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = Convert.ToInt64(oData.Rows[0]["nRecurrence_Pattern_Yearly_DayNumber"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (gloAppointmentScheduling.MonthRange)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Yearly_MonthOfCriteriaID"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = (gloAppointmentScheduling.FirstLastCriteria)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Yearly_FstLstCriteriaID"]);
                            oMasterAppointment.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = (gloAppointmentScheduling.DayWeekday)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID"]);
                        }
                    }
                    oData.Dispose();
                    #endregion

                    #region "Notes AND Date/Time as per Single/Recurrence/Single inRecurrence"
                    oData = new DataTable();
                    if (RetriveMethod != SingleRecurrence.Recurrence)
                    {
                        if (RetriveSingleInRecurrence == true)
                        {
                            _strSQL = "SELECT dtStartDate, dtStartTime, dtEndDate, dtEndTime, " +
                            " ISNULL(nColorCode,0) AS nColorCode, ISNULL(sNotes,'') AS sNotes, ISNULL(nUsedStatus,5) AS nUsedStatus " +
                            " FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nRefID = 0  and nDTLAppointmentID = " + AppointmentID + " ";

                        }
                        else
                        {
                            _strSQL = "SELECT TOP 1 dtStartDate, dtStartTime, dtEndDate, dtEndTime, " +
                            " ISNULL(nColorCode,0) AS nColorCode, ISNULL(sNotes,'') AS sNotes, ISNULL(nUsedStatus,5) AS nUsedStatus " +
                            " FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nRefID = 0 ";
                        }

                        if (_strSQL.Trim() != "")
                        {
                            oDB.Retrive_Query(_strSQL, out oData);
                            if (oData != null)
                            {
                                if (oData.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oData.Rows.Count - 1; i++)
                                    {
                                        oMasterAppointment.StartDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oData.Rows[0]["dtStartDate"].ToString()));
                                        oMasterAppointment.StartTime = gloDateMaster.gloTime.TimeAsDateTime(oMasterAppointment.StartDate, Convert.ToInt32(oData.Rows[0]["dtStartTime"].ToString()));
                                        oMasterAppointment.EndDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oData.Rows[0]["dtEndDate"].ToString()));
                                        oMasterAppointment.EndTime = gloDateMaster.gloTime.TimeAsDateTime(oMasterAppointment.EndDate, Convert.ToInt32(oData.Rows[0]["dtEndTime"].ToString()));
                                        oMasterAppointment.ColorCode = Convert.ToInt32(oData.Rows[0]["nColorCode"].ToString());
                                        oMasterAppointment.Notes = oData.Rows[0]["sNotes"].ToString();
                                        oMasterAppointment.UsedStatus = (ASUsedStatus)Convert.ToInt32(oData.Rows[0]["nUsedStatus"]);

                                        //Update Duration
                                        //TimeSpan _dur = oMasterAppointment.EndTime.Subtract(oMasterAppointment.StartTime);
                                        //Code Added to resolve bug if appointment end time falls into next day Abhisekh
                                        TimeSpan _dur = new TimeSpan();
                                        if (oMasterAppointment.EndTime <= oMasterAppointment.StartTime)
                                        {
                                            _dur = (oMasterAppointment.EndTime.AddDays(1)).Subtract(oMasterAppointment.StartTime);
                                        }
                                        else
                                        {
                                            _dur = (oMasterAppointment.EndTime).Subtract(oMasterAppointment.StartTime);
                                        }

                                        oMasterAppointment.Duration = Convert.ToDecimal(_dur.TotalMinutes);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        oData = new DataTable();
                        _strSQL = "SELECT TOP 1 dtStartDate, dtStartTime, dtEndDate, dtEndTime, " +
                        " ISNULL(nColorCode,0) AS nColorCode, ISNULL(sNotes,'') AS sNotes, ISNULL(nUsedStatus,5) AS nUsedStatus " +
                        " FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nRefID = 0 ";

                        if (_strSQL.Trim() != "")
                        {
                            oDB.Retrive_Query(_strSQL, out oData);
                            if (oData != null)
                            {
                                if (oData.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oData.Rows.Count - 1; i++)
                                    {
                                        oMasterAppointment.Notes = oData.Rows[0]["sNotes"].ToString();
                                    }
                                }
                            }
                        }
                    }
                    oData.Dispose();
                    #endregion

                    #region "Retrive Provider Appointments"
                    oData = new DataTable();
                    if (RetriveMethod == SingleRecurrence.Single)
                    {
                        _strSQL = "";
                    }
                    else if (RetriveMethod == SingleRecurrence.Recurrence)
                    {
                        _strSQL = "";
                    }
                    else if (RetriveMethod == SingleRecurrence.SingleInRecurrence)
                    {
                        _strSQL = "";
                    }

                    if (_strSQL.Trim() != "")
                    {
                        oDB.Retrive_Query(_strSQL, out oData);
                        if (oData != null)
                        {
                            if (oData.Rows.Count > 0)
                            {
                                for (int i = 0; i <= oData.Rows.Count - 1; i++)
                                {
                                    oMasterAppointment.AppointmentDetails = null;
                                }
                            }
                        }
                    }
                    oData.Dispose();
                    #endregion

                    #region "Retrive Problem Type Appointments"
                    oData = new DataTable();
                    if (RetriveMasterMethod == SingleRecurrence.Single)
                    {
                        _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc, nASBaseFlag, " +
                        " " + _StartDateforProbTypeNRes + " As dtStartDate , " + _EndDateforProbTypeNRes + " AS dtEndDate, nDTLAppointmentID  " +
                        " FROM AS_Appointment_DTL  WITH(NOLOCK) where nMSTAppointmentID = " + MasterAppointmentID + " AND nDTLAppointmentID <> 0 " +
                        " AND (nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + ") AND bIsSingleRecurrence <> " + SingleRecurrence.SingleInRecurrence.GetHashCode() + "";
                    }
                    else
                    {
                        if (RetriveSingleInRecurrence == false)
                        {
                            _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc, nASBaseFlag, " +
                            " " + _StartDateforProbTypeNRes + " As dtStartDate , " + _EndDateforProbTypeNRes + " AS dtEndDate, 0 as nDTLAppointmentID  " +
                            " FROM AS_Appointment_DTL  WITH(NOLOCK) where nMSTAppointmentID = " + MasterAppointmentID + " AND nDTLAppointmentID <> 0 " +
                            " AND (nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + ") AND bIsSingleRecurrence <> " + SingleRecurrence.SingleInRecurrence.GetHashCode() + "";
                        }
                        else
                        {
                            _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc, nASBaseFlag,dtStartDate,dtEndDate, 0 as nDTLAppointmentID " +
                            " FROM AS_Appointment_DTL  WITH(NOLOCK) where nMSTAppointmentID = " + MasterAppointmentID + " AND nRefID = " + AppointmentID + " " +
                            " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " ";
                        }
                    }

                    oDB.Retrive_Query(_strSQL, out oData);
                    if (oData != null)
                    {
                        if (oData.Rows.Count > 0)
                        {
                            for (int i = 0; i <= oData.Rows.Count - 1; i++)
                            {
                                oProblemType = new ShortApointmentSchedule();

                                oProblemType.MasterID = MasterAppointmentID;
                                oProblemType.DetailID = Convert.ToInt64 (oData.Rows[i]["nDTLAppointmentID"]); 
                                oProblemType.IsRecurrence = RetriveMasterMethod;
                                oProblemType.PatientID = 0; // only for appointment purpose to retrive in calendar
                                oProblemType.LineNo = 0;
                                oProblemType.ASFlag = oMasterAppointment.ASFlag;
                                oProblemType.ASCommonID = GetProblemTypeID(oData.Rows[i]["sASBaseCode"].ToString(), oData.Rows[i]["sASBaseDesc"].ToString(), ClinicID);// Convert.ToInt64(oData.Rows[i]["nASBaseID"].ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                                oProblemType.ASCommonCode = oData.Rows[i]["sASBaseCode"].ToString();
                                oProblemType.ASCommonDescription = oData.Rows[i]["sASBaseDesc"].ToString();
                                oProblemType.ASCommonFlag = (ASBaseType)Convert.ToInt32(oData.Rows[i]["nASBaseFlag"]);

                                Int64 _StDt = Convert.ToInt64(oData.Rows[i]["dtStartDate"].ToString());
                                Int64 _EdDt = Convert.ToInt64(oData.Rows[i]["dtEndDate"].ToString());
                                oProblemType.StartDate = gloDateMaster.gloDate.DateAsDate(_StDt);
                                oProblemType.EndDate = gloDateMaster.gloDate.DateAsDate(_EdDt);

                                oProblemType.StartTime = gloDateMaster.gloTime.TimeAsDateTime(oProblemType.StartDate, Convert.ToInt32(oData.Rows[i]["dtStartTime"].ToString()));
                                oProblemType.EndTime = gloDateMaster.gloTime.TimeAsDateTime(oProblemType.EndDate, Convert.ToInt32(oData.Rows[i]["dtEndTime"].ToString()));

                                oProblemType.ColorCode = oMasterAppointment.ColorCode;
                                oProblemType.ClinicID = ClinicID;
                                oProblemType.ViewOtherDetails = ""; // only for appointment purpose to retrive in calendar
                                oProblemType.UsedStatus = oMasterAppointment.UsedStatus;

                                oMasterAppointment.ProblemTypes.Add(oProblemType);
                                oProblemType.Dispose();
                            }
                        }
                    }
                    oData.Dispose();
                    #endregion

                    #region "Retrive Resources Appointments"
                    oData = new DataTable();
                    if (RetriveMasterMethod == SingleRecurrence.Single)
                    {
                        _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc, nASBaseFlag, " +
                                " " + _StartDateforProbTypeNRes + " As dtStartDate , " + _EndDateforProbTypeNRes + " AS dtEndDate  " +
                                " FROM AS_Appointment_DTL  WITH(NOLOCK) where nMSTAppointmentID = " + MasterAppointmentID + " AND ISNULL(nASBaseID,0) <> 0 " +
                                " AND (nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + ") AND bIsSingleRecurrence <> " + SingleRecurrence.SingleInRecurrence.GetHashCode() + "";
                    }
                    else
                    {
                        if (RetriveSingleInRecurrence == false)
                        {
                            _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc, nASBaseFlag, " +
                            " " + _StartDateforProbTypeNRes + " As dtStartDate , " + _EndDateforProbTypeNRes + " AS dtEndDate  " +
                            " FROM AS_Appointment_DTL  WITH(NOLOCK) where nMSTAppointmentID = " + MasterAppointmentID + " AND ISNULL(nASBaseID,0) <> 0 " +
                            " AND (nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + ") AND bIsSingleRecurrence <> " + SingleRecurrence.SingleInRecurrence.GetHashCode() + "";
                        }
                        else
                        {
                            //_strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc, nASBaseFlag,dtStartDate,dtEndDate " +
                            //" FROM AS_Appointment_DTL where nMSTAppointmentID = " + MasterAppointmentID + " AND nRefID = " + AppointmentID + " " +
                            //" AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " ";

                            _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc, nASBaseFlag,dtStartDate,dtEndDate " +
                            " FROM AS_Appointment_DTL  WITH(NOLOCK) where nMSTAppointmentID = " + MasterAppointmentID + " AND nRefID = " + AppointmentID + " " +
                            " AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND ISNULL(nASBaseID,0) <> 0 ";
                        }
                    }

                    oDB.Retrive_Query(_strSQL, out oData);

                    StringBuilder resourceIDS = new StringBuilder("");
                    if (oData != null)
                    {
                        if (oData.Rows.Count > 0)
                        {

                            for (int i = 0; i <= oData.Rows.Count - 1; i++)
                            {
                                oResource = new ShortApointmentSchedule();

                                oResource.MasterID = MasterAppointmentID;
                                oResource.DetailID = 0;
                                oResource.IsRecurrence = RetriveMasterMethod;
                                oResource.PatientID = 0; // only for appointment purpose to retrive in calendar
                                oResource.LineNo = 0;
                                oResource.ASFlag = oMasterAppointment.ASFlag;
                                //Bug #67759: 00000687: adding notes the appointments are no longer on the calendar
                                //oResource.ASCommonID = GetResourceID(oData.Rows[i]["sASBaseCode"].ToString(), oData.Rows[i]["sASBaseDesc"].ToString(), ClinicID);// Convert.ToInt64(oData.Rows[i]["nASBaseID"].ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                                oResource.ASCommonID = Convert.ToInt64(oData.Rows[i]["nASBaseID"]);

                                oResource.ASCommonCode = oData.Rows[i]["sASBaseCode"].ToString();
                                oResource.ASCommonDescription = oData.Rows[i]["sASBaseDesc"].ToString();
                                oResource.ASCommonFlag = (ASBaseType)Convert.ToInt32(oData.Rows[i]["nASBaseFlag"]);

                                Int64 _StDt = Convert.ToInt64(oData.Rows[i]["dtStartDate"].ToString());
                                Int64 _EdDt = Convert.ToInt64(oData.Rows[i]["dtEndDate"].ToString());
                                oResource.StartDate = gloDateMaster.gloDate.DateAsDate(_StDt);
                                oResource.EndDate = gloDateMaster.gloDate.DateAsDate(_EdDt);

                                oResource.StartTime = gloDateMaster.gloTime.TimeAsDateTime(oResource.StartDate, Convert.ToInt32(oData.Rows[i]["dtStartTime"].ToString()));
                                oResource.EndTime = gloDateMaster.gloTime.TimeAsDateTime(oResource.EndDate, Convert.ToInt32(oData.Rows[i]["dtEndTime"].ToString()));

                                oResource.ColorCode = oMasterAppointment.ColorCode;
                                oResource.ClinicID = ClinicID;
                                oResource.ViewOtherDetails = ""; // only for appointment purpose to retrive in calendar
                                oResource.UsedStatus = oMasterAppointment.UsedStatus;


                                oMasterAppointment.Resources.Add(oResource);

                                resourceIDS = resourceIDS.Append(oData.Rows[i]["nASBaseID"].ToString());
                                resourceIDS = resourceIDS.Append(",");


                                oResource.Dispose();
                            }
                        }

                        if (oData.Rows.Count > 0)
                        {
                            resourceIDS = resourceIDS.Remove(resourceIDS.Length - 1, 1);
                            oMasterAppointment.ResourceIDS = resourceIDS;
                        }
                        else
                        {
                            oMasterAppointment.ResourceIDS = new StringBuilder("");
                        }

                    }
                    else
                    {
                        oMasterAppointment.ResourceIDS = new StringBuilder("");
                    }
                    oData.Dispose();
                    #endregion

                    #region "Retrive Insurances"
                    oMasterAppointment.Insurances = new object();
                    #endregion

                    #region "Retrive Appointment Status"

                    DataTable dtStatus = new DataTable();
                    _strSQL = " SELECT ISNULL(nTrackingStatus,0) AS nAppointmentStatus FROM PatientTracking  WITH(NOLOCK) "
                             + " WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND  nDTLAppointmentID = " + AppointmentID + " AND nClinicID = " + _ClinicID + " "
                             + " ORDER BY dtDate DESC";

                    oDB.Retrive_Query(_strSQL, out dtStatus);
                    if (dtStatus != null && dtStatus.Rows.Count > 0)
                    {
                        oMasterAppointment.AppointmentStatusID = Convert.ToInt64(dtStatus.Rows[0]["nAppointmentStatus"]);
                    }

                    #endregion
                }
                oDB.Disconnect();
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
            //it will return master data as well as single date & time selected appointment and its associated problem types, resources, etc
            return oMasterAppointment;
        }

        public AppointmentSchedules GetUsedAppointment(Int64 MasterAppointmentID, Int64 AppointmentID, Int64 ClinicID, ASUsedStatus UsedStatusFlag)
        {
            AppointmentSchedules oAppointmentSchedules = new AppointmentSchedules();
            AppointmentSchedule oAppointmentSchedule = new AppointmentSchedule();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            object _intresult = new object();
            DataTable oData = new DataTable();

            try
            {
                oDB.Connect(false);

                #region "Retrive Provider Appointments"
                oData = new DataTable();
                if (AppointmentID <= 0)
                {
                    _strSQL = "SELECT nDTLAppointmentID, nASBaseID, sASBaseCode, sASBaseDesc, nASBaseFlag, " +
                    " dtStartDate, dtStartTime, dtEndDate, dtEndTime, " +
                    " nLocationID, sLocationName, nDepartmentID, sDepartmentName, " +
                    " nColorCode, sNotes " +
                    " FROM AS_Appointment_DTL  WITH(NOLOCK)  WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " " +
                    " AND nDTLAppointmentID <> 0 AND nClinicID = " + ClinicID + " AND nUsedStatus =  " + UsedStatusFlag.GetHashCode() + " ORDER BY nLineNumber";
                }
                else
                {
                    _strSQL = "SELECT nDTLAppointmentID, nASBaseID, sASBaseCode, sASBaseDesc, nASBaseFlag, " +
                    " dtStartDate, dtStartTime, dtEndDate, dtEndTime, " +
                    " nLocationID, sLocationName, nDepartmentID, sDepartmentName, " +
                    " nColorCode, sNotes " +
                    " FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " " +
                    " AND nDTLAppointmentID <> " + AppointmentID + " AND nClinicID = " + ClinicID + " AND nUsedStatus =  " + UsedStatusFlag.GetHashCode() + " ORDER BY nLineNumber";
                }

                if (_strSQL.Trim() != "")
                {
                    oDB.Retrive_Query(_strSQL, out oData);
                    if (oData != null)
                    {
                        if (oData.Rows.Count > 0)
                        {
                            for (int i = 0; i <= oData.Rows.Count - 1; i++)
                            {
                                oAppointmentSchedule = new AppointmentSchedule();
                                Int64 _ProviderIDforProbTypeNRes = 0;
                                Int64 _TempDate = 0;
                                oAppointmentSchedule.DetailID = Convert.ToInt64(oData.Rows[i]["nDTLAppointmentID"].ToString());
                                _ProviderIDforProbTypeNRes = Convert.ToInt64(oData.Rows[i]["nASBaseID"].ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                                oAppointmentSchedule.ASBaseID = _ProviderIDforProbTypeNRes;
                                oAppointmentSchedule.ASBaseCode = oData.Rows[i]["sASBaseCode"].ToString();
                                oAppointmentSchedule.ASBaseDescription = GetProviderName(_ProviderIDforProbTypeNRes, ClinicID);
                                oAppointmentSchedule.ASBaseFlag = (ASBaseType)Convert.ToInt32(oData.Rows[i]["nASBaseFlag"]);
                                _TempDate = Convert.ToInt64(oData.Rows[i]["dtStartDate"].ToString());
                                oAppointmentSchedule.StartDate = gloDateMaster.gloDate.DateAsDate(_TempDate);
                                oAppointmentSchedule.StartTime = gloDateMaster.gloTime.TimeAsDateTime(oAppointmentSchedule.StartDate, Convert.ToInt32(oData.Rows[i]["dtStartTime"].ToString()));
                                _TempDate = Convert.ToInt64(oData.Rows[i]["dtEndDate"].ToString());
                                oAppointmentSchedule.EndDate = gloDateMaster.gloDate.DateAsDate(_TempDate);
                                oAppointmentSchedule.EndTime = gloDateMaster.gloTime.TimeAsDateTime(oAppointmentSchedule.EndDate, Convert.ToInt32(oData.Rows[i]["dtEndTime"].ToString()));
                                oAppointmentSchedule.ColorCode = Convert.ToInt32(oData.Rows[i]["nColorCode"].ToString());
                                oAppointmentSchedule.LocationID = GetLocationID(oData.Rows[i]["sLocationName"].ToString(), ClinicID);
                                oAppointmentSchedule.LocationName = oData.Rows[i]["sLocationName"].ToString();
                                oAppointmentSchedule.DepartmentID = GetDepartmentID(oData.Rows[i]["sDepartmentName"].ToString(), oAppointmentSchedule.LocationID, ClinicID);
                                oAppointmentSchedule.DepartmentName = oData.Rows[i]["sDepartmentName"].ToString();
                                oAppointmentSchedule.Notes = oData.Rows[i]["sNotes"].ToString();
                                oAppointmentSchedules.Add(oAppointmentSchedule);
                                oAppointmentSchedule.Dispose();
                            }
                        }
                    }
                }
                oData.Dispose();
                #endregion

                oDB.Disconnect();
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
            return oAppointmentSchedules;
        }

        #region "Old Get CalanderAppointmentMethods"

        public CalendarApointmentSchedules GetCalendarAppointments_Old(Int32 FromDate, Int32 ToDate, ArrayList PrvResIDs, gloAppointmentScheduling.ASBaseType PrvResType, Int64 ClinicID, Boolean IncludeTemplateBlocks)
        {
            CalendarApointmentSchedules oAppointments = new CalendarApointmentSchedules();
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _sqlQuery = "";

                string _InPrvResIDs = "";
                for (int i = 0; i < PrvResIDs.Count; i++)
                {
                    if (i == 0)
                    {
                        _InPrvResIDs = "(" + PrvResIDs[i].ToString();
                    }
                    else
                    {
                        _InPrvResIDs = _InPrvResIDs + "," + PrvResIDs[i].ToString();
                    }

                    if (i == PrvResIDs.Count - 1)
                    {
                        _InPrvResIDs = _InPrvResIDs + ")";
                    }
                }
                if (_InPrvResIDs.Length <= 0)
                    _InPrvResIDs = "(0)";

                if (PrvResType == ASBaseType.Provider)
                {
                    //nothing    
                }
                else if (PrvResType == ASBaseType.Resource)
                {
                    IncludeTemplateBlocks = false;
                }

                if (IncludeTemplateBlocks == true)
                {
                    _sqlQuery = "SELECT * FROM (" +
                    " SELECT nLineNumber AS nLineNumber,(ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sLocationName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sDepartmentName, ''))  " +
                    " AS ASText,  " +
                    " (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nDTLAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT( VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT( VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0)))  " +
                    " AS ASTag, " +
                    " AS_Appointment_DTL.sNotes AS ASDescription,  " +
                    " AS_Appointment_DTL.dtStartDate AS dtStartDate, AS_Appointment_DTL.dtStartTime AS dtStartTime,  " +
                    " AS_Appointment_DTL.dtEndDate AS dtEndDate, AS_Appointment_DTL.dtEndTime AS dtEndTime,  " +
                    " AS_Appointment_DTL.nASBaseID AS PrvResUsrID, AS_Appointment_DTL.nASBaseFlag As PrvResUsrFlag,  " +
                    " AS_Appointment_DTL.nColorCode AS ColorCode,1 as isApp" +
                    " FROM Patient   WITH(NOLOCK) RIGHT OUTER JOIN AS_Appointment_MST  WITH(NOLOCK) ON Patient.nPatientID = AS_Appointment_MST.nPatientID RIGHT OUTER JOIN AS_Appointment_DTL  WITH(NOLOCK) ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID  " +
                    " WHERE (AS_Appointment_DTL.nASBaseFlag = " + PrvResType.GetHashCode() + ") AND (AS_Appointment_DTL.nASBaseID IN " + _InPrvResIDs + ") " +
                    " AND (AS_Appointment_DTL.nClinicID = " + ClinicID + ") AND  " +
                    " (AS_Appointment_DTL.dtStartDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtStartDate <= " + ToDate + ") " +
                    " AND  (AS_Appointment_DTL.dtEndDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtEndDate <= " + ToDate + ") " +
                    " UNION " +
                    " SELECT nLineNumber AS nLineNumber,(ISNULL(sAppointmentTypeDesc, '') + ',' + SPACE(1) + ISNULL(sLocationName, '') + ',' + SPACE(1) + ISNULL(sDepartmentName, '')) " +
                    " AS ASText, " +
                    " (Convert(varchar,nTemplateAllocationMasterID) + '~' + Convert(varchar,nTemplateAllocationID) + '~' + '1' + '~' + '6' + '~' + Convert(varchar,ISNULL(nLocationID,0)) + '~' + ISNULL(sLocationName, '') + '~' + Convert(varchar,ISNULL(nDepartmentID,0)) + '~' + ISNULL(sDepartmentName, '') + '~' + CONVERT( VARCHAR,ISNULL(nLineNumber, 0)) + '~0')  " +
                    " AS ASTag, " +
                    " sTemplateName AS ASDescription,  " +
                    " dtStartDate AS dtStartDate,dtStartTime AS dtStartTime, " +
                    " dtEndDate AS dtEndDate, dtEndTime AS dtEndTime," +
                    " nASBaseID AS PrvResUsrID, nASBaseFlag As PrvResUsrFlag, " +
                    " nColorCode AS ColorCode,2 as isApp " +
                    " FROM AB_AppointmentTemplate_Allocation  WITH(NOLOCK) WHERE " +
                    " nASBaseFlag = " + PrvResType.GetHashCode() + " and nASBaseID IN " + _InPrvResIDs + " and nClinicID = " + ClinicID + " and " +
                    " (dtStartDate >= " + FromDate + " and dtStartDate <= " + ToDate + ") and " +
                    " (dtEndDate >= " + FromDate + " and dtEndDate <= " + ToDate + ") and " +
                    " (nIsRegistered = 0 or nIsRegistered is null) " +
                    "  UNION " +
                    " SELECT nLineNumber AS nLineNumber, ISNULL(sLocationName,'') + ',' + SPACE(1) + ISNULL(sDepartmentName,'') " +
                    " AS ASText,  " +
                    " (Convert(varchar,nMSTScheduleID) + '~' + Convert(varchar,nDTLScheduleID) + '~' + Convert(varchar,bIsSingleRecurrence) + '~3~' + Convert(varchar,ISNULL(nLocationID,0)) + '~' + ISNULL(sLocationName, '') + '~' + Convert(varchar,ISNULL(nDepartmentID,0)) + '~' + ISNULL(sDepartmentName, '') + '~' + + CONVERT(VARCHAR,ISNULL(nLineNumber, 0)) + '~' + '1') " +
                    " AS ASTag, " +
                    " sNotes AS ASDescription, " +
                    " dtStartDate AS dtStartDate,dtStartTime AS dtStartTime, " +
                    " dtEndDate AS dtEndDate, dtEndTime AS dtEndTime, " +
                    " nASBaseID AS PrvResUsrID, " +
                    " nASBaseFlag AS PrvResUsrFlag, " +
                    " nColorCode AS ColorCode, " +
                    " 2 as isApp " +
                    " FROM  AS_Schedule_DTL  WITH(NOLOCK)  " +
                    " WHERE (nASBaseFlag = " + PrvResType.GetHashCode() + ") AND (nASBaseID IN " + _InPrvResIDs + ") AND (nClinicID =  " + ClinicID + ") " +
                    " AND (dtStartDate >=  " + FromDate + " ) AND (dtStartDate <= " + ToDate + ") AND   (dtEndDate >= " + FromDate + ") AND (dtEndDate <= " + ToDate + ") AND (nScheduleFlag = " + AppointmentScheduleFlag.BlockedSchedule.GetHashCode() + " ) " +
                    "  ) a " +
                    " ORDER BY a.dtStartDate, a.nLineNumber, a.isApp";
                }
                else
                {
                    _sqlQuery = "SELECT * FROM ( "
                                + " SELECT nLineNumber AS nLineNumber,(ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sLocationName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sDepartmentName, ''))  "
                                + " AS ASText,  "
                                + " (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nDTLAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0))) "
                                + " AS ASTag,  "
                                + " AS_Appointment_DTL.sNotes AS ASDescription,  "
                                + " AS_Appointment_DTL.dtStartDate AS dtStartDate, AS_Appointment_DTL.dtStartTime AS dtStartTime,  "
                                + " AS_Appointment_DTL.dtEndDate AS dtEndDate, AS_Appointment_DTL.dtEndTime AS dtEndTime,   "
                                + " AS_Appointment_DTL.nASBaseID AS PrvResUsrID, "
                                + " AS_Appointment_DTL.nASBaseFlag As PrvResUsrFlag, "
                                + " AS_Appointment_DTL.nColorCode AS ColorCode, "
                                + " 1 as isApp  "
                                + " FROM Patient  WITH(NOLOCK) RIGHT OUTER JOIN AS_Appointment_MST  WITH(NOLOCK) ON Patient.nPatientID = AS_Appointment_MST.nPatientID RIGHT OUTER JOIN AS_Appointment_DTL  WITH(NOLOCK) ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID  "
                                + " WHERE (AS_Appointment_DTL.nASBaseFlag =  " + PrvResType.GetHashCode() + ") AND (AS_Appointment_DTL.nASBaseID IN " + _InPrvResIDs + ") AND "
                                + " (AS_Appointment_DTL.nClinicID = " + ClinicID + ") AND  "
                                + " (AS_Appointment_DTL.dtStartDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtStartDate <= " + ToDate + ") "
                                + " AND (AS_Appointment_DTL.dtEndDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtEndDate <= " + ToDate + ") "
                                + " UNION "
                                + " SELECT nLineNumber AS nLineNumber, ISNULL(sLocationName,'') + ',' + SPACE(1) + ISNULL(sDepartmentName,'') "
                                + " AS ASText,  "
                                + " (Convert(varchar,nMSTScheduleID) + '~' + Convert(varchar,nDTLScheduleID) + '~' + Convert(varchar,bIsSingleRecurrence) + '~3~' + Convert(varchar,ISNULL(nLocationID,0)) + '~' + ISNULL(sLocationName, '') + '~' + Convert(varchar,ISNULL(nDepartmentID,0)) + '~' + ISNULL(sDepartmentName, '') + '~' + + CONVERT(VARCHAR,ISNULL(nLineNumber, 0)) + '~' + '1') "
                                + " AS ASTag, "
                                + " sNotes AS ASDescription, "
                                + " dtStartDate AS dtStartDate,dtStartTime AS dtStartTime, "
                                + " dtEndDate AS dtEndDate, dtEndTime AS dtEndTime, "
                                + " nASBaseID AS PrvResUsrID, "
                                + " nASBaseFlag AS PrvResUsrFlag, "
                                + " nColorCode AS ColorCode, "
                                + " 2 as isApp "
                                + " FROM  AS_Schedule_DTL   WITH(NOLOCK) "
                                + " WHERE (nASBaseFlag = " + PrvResType.GetHashCode() + ") AND (nASBaseID IN  " + _InPrvResIDs + ") AND (nClinicID =  " + ClinicID + ") "
                                + " AND (dtStartDate >=  " + FromDate + " ) AND (dtStartDate <= " + ToDate + ") AND   (dtEndDate >= " + FromDate + ") AND (dtEndDate <= " + ToDate + ") AND (nScheduleFlag = " + AppointmentScheduleFlag.BlockedSchedule.GetHashCode() + " ) ) a "
                                + " ORDER BY a.dtStartDate, a.nLineNumber, a.isApp";
                }

                DataTable dt = new DataTable();
                oDB.Retrive_Query(_sqlQuery, out dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CalendarApointmentSchedule oShortAppointment = new CalendarApointmentSchedule();

                        //Appointment  ASText = Patient Name + Location + Department
                        oShortAppointment.ASText = dt.Rows[i]["ASText"].ToString();
                        //Appointment  ASTag = 
                        //Master ID + Detail ID + Single/Recurrence + Is Appointmnet or Template Block Hash Code Value (AppointmentScheduleFlag) + Location ID + Location Name + Department ID + Department Name (Location & Department for send to app form while register from template block) + Line Number
                        oShortAppointment.ASTag = dt.Rows[i]["ASTag"].ToString();
                        //Appointment Description = Notes
                        oShortAppointment.ASDescription = dt.Rows[i]["ASDescription"].ToString();
                        oShortAppointment.StartDateTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dt.Rows[i]["dtStartDate"])), Convert.ToInt32(dt.Rows[i]["dtStartTime"]));
                        oShortAppointment.EndDateTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dt.Rows[i]["dtEndDate"])), Convert.ToInt32(dt.Rows[i]["dtEndTime"]));
                        oShortAppointment.PrvResUsrID = Convert.ToInt64(dt.Rows[i]["PrvResUsrID"]);
                        oShortAppointment.PrvResUsrFlag = (ASBaseType)Convert.ToInt32(dt.Rows[i]["PrvResUsrFlag"]);
                        oShortAppointment.ColorCode = Convert.ToInt32(dt.Rows[i]["ColorCode"].ToString());
                        oAppointments.Add(oShortAppointment);
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return oAppointments;
        }

        public CalendarApointmentSchedules GetCalendarAppointments_Old(DateTime FromDate, DateTime ToDate, ArrayList PrvResIDs, gloAppointmentScheduling.ASBaseType PrvResType, Int64 ClinicID, Boolean IncludeTemplateBlocks)
        {
            return GetCalendarAppointments_Old(gloDateMaster.gloDate.DateAsNumber(FromDate.ToString()), gloDateMaster.gloDate.DateAsNumber(ToDate.ToString()), PrvResIDs, PrvResType, ClinicID, IncludeTemplateBlocks);
        }

        #endregion

        //For All locations 
        public CalendarApointmentSchedules GetCalendarAppointments(Int32 FromDate, Int32 ToDate, ArrayList ProviderIDs, ArrayList ResourceIDs, Int64 ClinicID, Boolean IncludeTemplateBlocks)
        {
            CalendarApointmentSchedules oAppointments = new CalendarApointmentSchedules();
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _sqlQuery = "";
                //string _InPrvResIDs = "";

                //--
                string _InProviderIDs = "";
                string _InResourceIDs = "";

                //Provider IDs
                for (int i = 0; i < ProviderIDs.Count; i++)
                {
                    if (i == 0)
                    {
                        _InProviderIDs = "(" + ProviderIDs[i].ToString();
                    }
                    else
                    {
                        _InProviderIDs = _InProviderIDs + "," + ProviderIDs[i].ToString();
                    }

                    if (i == ProviderIDs.Count - 1)
                    {
                        _InProviderIDs = _InProviderIDs + ")";
                    }
                }
                if (_InProviderIDs.Length <= 0)
                    _InProviderIDs = "(null)";


                //Resource IDs
                for (int i = 0; i < ResourceIDs.Count; i++)
                {
                    if (i == 0)
                    {
                        _InResourceIDs = "(" + ResourceIDs[i].ToString();
                    }
                    else
                    {
                        _InResourceIDs = _InResourceIDs + "," + ResourceIDs[i].ToString();
                    }

                    if (i == ResourceIDs.Count - 1)
                    {
                        _InResourceIDs = _InResourceIDs + ")";
                    }
                }
                if (_InResourceIDs.Length <= 0)
                    _InResourceIDs = "(null)";

                if (IncludeTemplateBlocks == true)
                {


                   _sqlQuery = " DECLARE @ApptStatus as Table (nUsedStatus int null)														             "+
                    " 																																	 "+
                    " DELETE FROM @ApptStatus;																											 "+
                    "        With A as (																												 "+
                    "        select   5  as nUsedStatus   from settings where sSettingsName = 'ShowNoShowAppointmentOnCalender' and sSettingsValue = '1' "+
                    "        union																														 "+
                    "         select  6  as nUsedStatus from settings where sSettingsName = 'ShowCancelAppointmentOnCalender' and sSettingsValue = '1'	 "+
                    "        Union																														 "+
                    "        select 7 as nUsedStatus                                                                                                     "+
                    "        )																															 "+
                    "        Insert Into @ApptStatus    SELECT nUsedStatus from A;																		 "+
                    " 																																	 "+																																			
                    " 																																	 "+
                    "        IF (NOT EXISTS       (SELECT nSettingsID FROM settings WHERE ssettingsname ='ShowNoShowAppointmentOnCalender'))			 "+	
                    "        BEGIN																														 "+
                    "         Insert Into @ApptStatus    SELECT  5 as nUsedStatus																		 "+
                    "        END																														 "+	
                    " 																																	 "+	
                    "        IF (NOT EXISTS       (SELECT nSettingsID FROM settings WHERE ssettingsname ='ShowCancelAppointmentOnCalender'))			 "+	
                    "        BEGIN																														 "+	
                    "         Insert Into @ApptStatus    SELECT  6 as nUsedStatus																		 "+		   
                    "        END																														 "+
    
                

                   " SELECT nLineNumber,	ASText,	ASTag,	ASDescription,	dtStartDate,	dtStartTime,	dtEndDate,	dtEndTime,	PrvResUsrID,	PrvResUsrFlag,	ColorCode,	isApp FROM (" +
                    " SELECT nLineNumber AS nLineNumber,(ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sLocationName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sDepartmentName, ''))  " +
                    " AS ASText,  " +
                    " CASE ISNULL(nRefID,0) WHEN 0 " +
                    " THEN  (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nDTLAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0))) " +
                    " ELSE (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nRefID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0))) " +
                    " END AS ASTag,  " +
                        //" AS_Appointment_DTL.sNotes AS ASDescription,  " +        // solving salesforce case - GLO2010-0007243    
                    " CASE ISNULL(nRefID,0) WHEN 0  THEN AS_Appointment_DTL.sNotes " +
                    " ELSE " +
                    " 	( SELECT APP_DTL.sNotes FROM  AS_Appointment_DTL  AS APP_DTL  WITH(NOLOCK) " +
                    " 	  WHERE APP_DTL.nDTLAppointmentID =	AS_Appointment_DTL.nRefID" +
                    " 	)" +
                    " END  AS ASDescription, " +                                // End
                    " AS_Appointment_DTL.dtStartDate AS dtStartDate, AS_Appointment_DTL.dtStartTime AS dtStartTime,  " +
                    " AS_Appointment_DTL.dtEndDate AS dtEndDate, AS_Appointment_DTL.dtEndTime AS dtEndTime,  " +
                    " AS_Appointment_DTL.nASBaseID AS PrvResUsrID, AS_Appointment_DTL.nASBaseFlag As PrvResUsrFlag,  " +
                    " AS_Appointment_DTL.nColorCode AS ColorCode,1 as isApp" +
                    " FROM Patient  WITH(NOLOCK) RIGHT OUTER JOIN AS_Appointment_MST  WITH(NOLOCK) ON Patient.nPatientID = AS_Appointment_MST.nPatientID RIGHT OUTER JOIN AS_Appointment_DTL  WITH(NOLOCK) ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID  " +
                    " WHERE ((AS_Appointment_DTL.nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " AND AS_Appointment_DTL.nASBaseID IN " + _InProviderIDs + ") OR (AS_Appointment_DTL.nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND AS_Appointment_DTL.nASBaseID IN " + _InResourceIDs + "))" +
                    " AND (AS_Appointment_DTL.nClinicID = " + ClinicID + ") AND  " +
                    " (AS_Appointment_DTL.dtStartDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtStartDate <= " + ToDate + ") " +
                    " AND  (AS_Appointment_DTL.dtEndDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtEndDate <= " + ToDate + ") " +
                    " AND AS_Appointment_DTL.nASBaseID <> 0 and (AS_Appointment_DTL.nUsedStatus not in (select nUsedStatus from @ApptStatus)) " + //line addded to restrict entries with 0 as nASBaseID 20100907

                    " UNION " +
                    " SELECT nLineNumber AS nLineNumber,(ISNULL(sAppointmentTypeDesc, '') + ',' + SPACE(1) + ISNULL(sLocationName, '') + ',' + SPACE(1) + ISNULL(sDepartmentName, '')) " +
                    " AS ASText, " +
                    " (Convert(varchar,nTemplateAllocationMasterID) + '~' + Convert(varchar,nTemplateAllocationID) + '~' + '1' + '~' + '6' + '~' + Convert(varchar,ISNULL(nLocationID,0)) + '~' + ISNULL(sLocationName, '') + '~' + Convert(varchar,ISNULL(nDepartmentID,0)) + '~' + ISNULL(sDepartmentName, '') + '~' + CONVERT( VARCHAR,ISNULL(nLineNumber, 0)) + '~0')  " +
                    " AS ASTag, " +
                    " sTemplateName AS ASDescription,  " +
                    " dtStartDate AS dtStartDate,dtStartTime AS dtStartTime, " +
                    " dtEndDate AS dtEndDate, dtEndTime AS dtEndTime," +
                    " nASBaseID AS PrvResUsrID, nASBaseFlag As PrvResUsrFlag, " +
                    " nColorCode AS ColorCode,2 as isApp " +
                    " FROM AB_AppointmentTemplate_Allocation  WITH(NOLOCK) WHERE " +
                    " nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " and nASBaseID IN " + _InProviderIDs + " and nClinicID = " + ClinicID + " and " +
                    " (dtStartDate >= " + FromDate + " and dtStartDate <= " + ToDate + ") and " +
                    " (dtEndDate >= " + FromDate + " and dtEndDate <= " + ToDate + ") and " +
                    " (nIsRegistered = 0 or nIsRegistered is null) " +

                    "  UNION " +
                    " SELECT nLineNumber AS nLineNumber, ISNULL(sLocationName,'') + ',' + SPACE(1) + ISNULL(sDepartmentName,'') " +
                    " AS ASText,  " +
                    " (Convert(varchar,nMSTScheduleID) + '~' + Convert(varchar,nDTLScheduleID) + '~' + Convert(varchar,bIsSingleRecurrence) + '~3~' + Convert(varchar,ISNULL(nLocationID,0)) + '~' + ISNULL(sLocationName, '') + '~' + Convert(varchar,ISNULL(nDepartmentID,0)) + '~' + ISNULL(sDepartmentName, '') + '~' + + CONVERT(VARCHAR,ISNULL(nLineNumber, 0)) + '~' + '11') " +
                    " AS ASTag, " +
                    " sNotes AS ASDescription, " +
                    " dtStartDate AS dtStartDate,dtStartTime AS dtStartTime, " +
                    " dtEndDate AS dtEndDate, dtEndTime AS dtEndTime, " +
                    " nASBaseID AS PrvResUsrID, " +
                    " nASBaseFlag AS PrvResUsrFlag, " +
                    " nColorCode AS ColorCode, " +
                    " 2 as isApp " +
                    " FROM  AS_Schedule_DTL  WITH(NOLOCK)  " +
                    " WHERE ((nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " AND nASBaseID IN " + _InProviderIDs + " ) OR (nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND nASBaseID IN " + _InResourceIDs + " )) AND (nClinicID =  " + ClinicID + ") " +
                    " AND (dtStartDate >=  " + FromDate + " ) AND (dtStartDate <= " + ToDate + ") AND   (dtEndDate >= " + FromDate + ") AND (dtEndDate <= " + ToDate + ") " +
                    "  ) a " +
                    " ORDER BY a.dtStartDate, a.nLineNumber, a.isApp";
                }
                else
                {

                    //  _sqlQuery = "SELECT * FROM ( "  //Removed Select *
                    _sqlQuery = " DECLARE @ApptStatus as Table (nUsedStatus int null)														             " +
                    " 																																	 " +
                    " DELETE FROM @ApptStatus;																											 " +
                    "        With A as (																												 " +
                    "        select   5  as nUsedStatus   from settings where sSettingsName = 'ShowNoShowAppointmentOnCalender' and sSettingsValue = '1' " +
                    "        union																														 " +
                    "         select  6  as nUsedStatus from settings where sSettingsName = 'ShowCancelAppointmentOnCalender' and sSettingsValue = '1'	 " +
                    "        Union																														 " +
                    "        select 7 as nUsedStatus                                                                                                     " +
                    "        )																															 " +
                    "        Insert Into @ApptStatus    SELECT nUsedStatus from A;																		 " +
                    " 																																	 " +
                    " 																																	 " +
                    "        IF (NOT EXISTS       (SELECT nSettingsID FROM settings WHERE ssettingsname ='ShowNoShowAppointmentOnCalender'))			 " +
                    "        BEGIN																														 " +
                    "         Insert Into @ApptStatus    SELECT  5 as nUsedStatus																		 " +
                    "        END																														 " +
                    " 																																	 " +
                    "        IF (NOT EXISTS       (SELECT nSettingsID FROM settings WHERE ssettingsname ='ShowCancelAppointmentOnCalender'))			 " +
                    "        BEGIN																														 " +
                    "         Insert Into @ApptStatus    SELECT  6 as nUsedStatus																		 " +
                    "        END																														 " +


                    "SELECT nLineNumber,	ASText,	ASTag,	ASDescription,	dtStartDate,	dtStartTime,	dtEndDate,	dtEndTime,	PrvResUsrID,	PrvResUsrFlag,	ColorCode,	isApp FROM ( " 
                                + " SELECT nLineNumber AS nLineNumber,(ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sLocationName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sDepartmentName, ''))  "
                                + " AS ASText,  "
                                + " CASE ISNULL(nRefID,0) WHEN 0 "
                                + " THEN  (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nDTLAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0))) "
                                + " ELSE (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nRefID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0))) "
                                + " END AS ASTag,  "
                        // + " AS_Appointment_DTL.sNotes AS ASDescription,  "               // solving salesforce case - GLO2010-0007243   
                                + " CASE ISNULL(nRefID,0) WHEN 0  THEN AS_Appointment_DTL.sNotes "
                                + " ELSE "
                                + " 	( SELECT APP_DTL.sNotes FROM  AS_Appointment_DTL  AS APP_DTL  WITH(NOLOCK) "
                                + " 	  WHERE APP_DTL.nDTLAppointmentID =	AS_Appointment_DTL.nRefID"
                                + " 	)"
                                + " END  AS ASDescription, "                                    //End
                                + " AS_Appointment_DTL.dtStartDate AS dtStartDate, AS_Appointment_DTL.dtStartTime AS dtStartTime,  "
                                + " AS_Appointment_DTL.dtEndDate AS dtEndDate, AS_Appointment_DTL.dtEndTime AS dtEndTime,   "
                                + " AS_Appointment_DTL.nASBaseID AS PrvResUsrID, "
                                + " AS_Appointment_DTL.nASBaseFlag As PrvResUsrFlag, "
                                + " AS_Appointment_DTL.nColorCode AS ColorCode, "
                                + " 1 as isApp  "
                                + " FROM Patient  WITH(NOLOCK) RIGHT OUTER JOIN AS_Appointment_MST  WITH(NOLOCK) ON Patient.nPatientID = AS_Appointment_MST.nPatientID RIGHT OUTER JOIN AS_Appointment_DTL  WITH(NOLOCK)  ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID  "
                                + " WHERE ((AS_Appointment_DTL.nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " AND AS_Appointment_DTL.nASBaseID IN " + _InProviderIDs + ") OR (AS_Appointment_DTL.nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND AS_Appointment_DTL.nASBaseID IN " + _InResourceIDs + ")) "
                                + " AND (AS_Appointment_DTL.nClinicID = " + ClinicID + ") AND  "
                                + " (AS_Appointment_DTL.dtStartDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtStartDate <= " + ToDate + ") "
                                + " AND (AS_Appointment_DTL.dtEndDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtEndDate <= " + ToDate + ") and (AS_Appointment_DTL.nUsedStatus not in (select nUsedStatus from @ApptStatus)) "
                                + " UNION "
                                + " SELECT nLineNumber AS nLineNumber, ISNULL(sLocationName,'') + ',' + SPACE(1) + ISNULL(sDepartmentName,'') "
                                + " AS ASText,  "
                                + " (Convert(varchar,nMSTScheduleID) + '~' + Convert(varchar,nDTLScheduleID) + '~' + Convert(varchar,bIsSingleRecurrence) + '~3~' + Convert(varchar,ISNULL(nLocationID,0)) + '~' + ISNULL(sLocationName, '') + '~' + Convert(varchar,ISNULL(nDepartmentID,0)) + '~' + ISNULL(sDepartmentName, '') + '~' + + CONVERT(VARCHAR,ISNULL(nLineNumber, 0)) + '~' + '11') "
                                + " AS ASTag, "
                                + " sNotes AS ASDescription, "
                                + " dtStartDate AS dtStartDate,dtStartTime AS dtStartTime, "
                                + " dtEndDate AS dtEndDate, dtEndTime AS dtEndTime, "
                                + " nASBaseID AS PrvResUsrID, "
                                + " nASBaseFlag AS PrvResUsrFlag, "
                                + " nColorCode AS ColorCode, "
                                + " 2 as isApp "
                                + " FROM  AS_Schedule_DTL  WITH(NOLOCK)  "
                                + " WHERE ((nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " AND nASBaseID IN " + _InProviderIDs + " ) OR (nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND nASBaseID IN " + _InResourceIDs + " )) AND (nClinicID =  " + ClinicID + ") "
                                + " AND (dtStartDate >=  " + FromDate + " ) AND (dtStartDate <= " + ToDate + ") AND   (dtEndDate >= " + FromDate + ") AND (dtEndDate <= " + ToDate + ")  ) a "
                                + " ORDER BY a.dtStartDate, a.nLineNumber, a.isApp";
                }

                DataTable dt = new DataTable();
                oDB.Retrive_Query(_sqlQuery, out dt);

                //  GetCalendarAppointmentList(FromDate, ToDate, ProviderIDs, ResourceIDs, ClinicID, IncludeTemplateBlocks);


                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CalendarApointmentSchedule oShortAppointment = new CalendarApointmentSchedule();

                        //Appointment  ASText = Patient Name + Location + Department
                        oShortAppointment.ASText = dt.Rows[i]["ASText"].ToString();
                        //Appointment  ASTag = 
                        //Master ID + Detail ID + Single/Recurrence + Is Appointmnet or Template Block Hash Code Value (AppointmentScheduleFlag) + Location ID + Location Name + Department ID + Department Name (Location & Department for send to app form while register from template block) + Line Number
                        oShortAppointment.ASTag = dt.Rows[i]["ASTag"].ToString();
                        //Appointment Description = Notes
                        oShortAppointment.ASDescription = dt.Rows[i]["ASDescription"].ToString();
                        oShortAppointment.StartDateTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dt.Rows[i]["dtStartDate"])), Convert.ToInt32(dt.Rows[i]["dtStartTime"]));
                        oShortAppointment.EndDateTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dt.Rows[i]["dtEndDate"])), Convert.ToInt32(dt.Rows[i]["dtEndTime"]));
                        oShortAppointment.PrvResUsrID = Convert.ToInt64(dt.Rows[i]["PrvResUsrID"]);
                        oShortAppointment.PrvResUsrFlag = (ASBaseType)Convert.ToInt32(dt.Rows[i]["PrvResUsrFlag"]);
                        oShortAppointment.ColorCode = Convert.ToInt32(dt.Rows[i]["ColorCode"].ToString());
                        oShortAppointment.IsApp = Convert.ToInt32(dt.Rows[i]["isApp"].ToString());
                        oAppointments.Add(oShortAppointment);
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            return oAppointments;
        }


        public CalendarApointmentSchedules GetCalendarAppointmentList(Int32 FromDate, Int32 ToDate, ArrayList ProviderIDs, ArrayList ResourceIDs, Int64 ClinicID, Boolean IncludeTemplateBlocks)
        {
            CalendarApointmentSchedules oAppointments = new CalendarApointmentSchedules();
            try
            {

               // string _sqlQuery = "";

                string _InProviderIDs = "";
                string _InResourceIDs = "";

                //Provider IDs
                for (int i = 0; i < ProviderIDs.Count; i++)
                {
                    if (i == 0)
                    {
                        _InProviderIDs = ProviderIDs[i].ToString();
                    }
                    else
                    {
                        _InProviderIDs = _InProviderIDs + "," + ProviderIDs[i].ToString();
                    }

                    //if (i == ProviderIDs.Count - 1)
                    //{
                    //    _InProviderIDs = _InProviderIDs ;
                    //}
                }
                if (_InProviderIDs.Length <= 0)
                    _InProviderIDs = null;


                //Resource IDs
                for (int i = 0; i < ResourceIDs.Count; i++)
                {
                    if (i == 0)
                    {
                        _InResourceIDs = ResourceIDs[i].ToString();
                    }
                    else
                    {
                        _InResourceIDs = _InResourceIDs + "," + ResourceIDs[i].ToString();
                    }

                    //if (i == ResourceIDs.Count - 1)
                    //{
                    //    _InResourceIDs = _InResourceIDs + ")";
                    //}
                }
                if (_InResourceIDs.Length <= 0)
                    _InResourceIDs = null;

                //if (IncludeTemplateBlocks == true)
                //{

                //    _sqlQuery = "SELECT nLineNumber,	ASText,	ASTag,	ASDescription,	dtStartDate,	dtStartTime,	dtEndDate,	dtEndTime,	PrvResUsrID,	PrvResUsrFlag,	ColorCode,	isApp FROM (" +
                //    " SELECT nLineNumber AS nLineNumber,(ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sLocationName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sDepartmentName, ''))  " +
                //    " AS ASText,  " +
                //    " CASE ISNULL(nRefID,0) WHEN 0 " +
                //    " THEN  (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nDTLAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0))) " +
                //    " ELSE (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nRefID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0))) " +
                //    " END AS ASTag,  " +
                //        //" AS_Appointment_DTL.sNotes AS ASDescription,  " +        // solving salesforce case - GLO2010-0007243    
                //    " CASE ISNULL(nRefID,0) WHEN 0  THEN AS_Appointment_DTL.sNotes " +
                //    " ELSE " +
                //    " 	( SELECT APP_DTL.sNotes FROM  AS_Appointment_DTL  AS APP_DTL " +
                //    " 	  WHERE APP_DTL.nDTLAppointmentID =	AS_Appointment_DTL.nRefID" +
                //    " 	)" +
                //    " END  AS ASDescription, " +                                // End
                //    " AS_Appointment_DTL.dtStartDate AS dtStartDate, AS_Appointment_DTL.dtStartTime AS dtStartTime,  " +
                //    " AS_Appointment_DTL.dtEndDate AS dtEndDate, AS_Appointment_DTL.dtEndTime AS dtEndTime,  " +
                //    " AS_Appointment_DTL.nASBaseID AS PrvResUsrID, AS_Appointment_DTL.nASBaseFlag As PrvResUsrFlag,  " +
                //    " AS_Appointment_DTL.nColorCode AS ColorCode,1 as isApp" +
                //    " FROM Patient RIGHT OUTER JOIN AS_Appointment_MST ON Patient.nPatientID = AS_Appointment_MST.nPatientID RIGHT OUTER JOIN AS_Appointment_DTL ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID  " +
                //    " WHERE ((AS_Appointment_DTL.nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " AND AS_Appointment_DTL.nASBaseID IN " + _InProviderIDs + ") OR (AS_Appointment_DTL.nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND AS_Appointment_DTL.nASBaseID IN " + _InResourceIDs + "))" +
                //    " AND (AS_Appointment_DTL.nClinicID = " + ClinicID + ") AND  " +
                //    " (AS_Appointment_DTL.dtStartDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtStartDate <= " + ToDate + ") " +
                //    " AND  (AS_Appointment_DTL.dtEndDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtEndDate <= " + ToDate + ") " +
                //    " AND AS_Appointment_DTL.nASBaseID <> 0 and (AS_Appointment_DTL.nUsedStatus not in (5,6,7)) " + //line addded to restrict entries with 0 as nASBaseID 20100907

                //    " UNION " +
                //    " SELECT nLineNumber AS nLineNumber,(ISNULL(sAppointmentTypeDesc, '') + ',' + SPACE(1) + ISNULL(sLocationName, '') + ',' + SPACE(1) + ISNULL(sDepartmentName, '')) " +
                //    " AS ASText, " +
                //    " (Convert(varchar,nTemplateAllocationMasterID) + '~' + Convert(varchar,nTemplateAllocationID) + '~' + '1' + '~' + '6' + '~' + Convert(varchar,ISNULL(nLocationID,0)) + '~' + ISNULL(sLocationName, '') + '~' + Convert(varchar,ISNULL(nDepartmentID,0)) + '~' + ISNULL(sDepartmentName, '') + '~' + CONVERT( VARCHAR,ISNULL(nLineNumber, 0)) + '~0')  " +
                //    " AS ASTag, " +
                //    " sTemplateName AS ASDescription,  " +
                //    " dtStartDate AS dtStartDate,dtStartTime AS dtStartTime, " +
                //    " dtEndDate AS dtEndDate, dtEndTime AS dtEndTime," +
                //    " nASBaseID AS PrvResUsrID, nASBaseFlag As PrvResUsrFlag, " +
                //    " nColorCode AS ColorCode,2 as isApp " +
                //    " FROM AB_AppointmentTemplate_Allocation WHERE " +
                //    " nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " and nASBaseID IN " + _InProviderIDs + " and nClinicID = " + ClinicID + " and " +
                //    " (dtStartDate >= " + FromDate + " and dtStartDate <= " + ToDate + ") and " +
                //    " (dtEndDate >= " + FromDate + " and dtEndDate <= " + ToDate + ") and " +
                //    " (nIsRegistered = 0 or nIsRegistered is null) " +

                //    "  UNION " +
                //    " SELECT nLineNumber AS nLineNumber, ISNULL(sLocationName,'') + ',' + SPACE(1) + ISNULL(sDepartmentName,'') " +
                //    " AS ASText,  " +
                //    " (Convert(varchar,nMSTScheduleID) + '~' + Convert(varchar,nDTLScheduleID) + '~' + Convert(varchar,bIsSingleRecurrence) + '~3~' + Convert(varchar,ISNULL(nLocationID,0)) + '~' + ISNULL(sLocationName, '') + '~' + Convert(varchar,ISNULL(nDepartmentID,0)) + '~' + ISNULL(sDepartmentName, '') + '~' + + CONVERT(VARCHAR,ISNULL(nLineNumber, 0)) + '~' + '11') " +
                //    " AS ASTag, " +
                //    " sNotes AS ASDescription, " +
                //    " dtStartDate AS dtStartDate,dtStartTime AS dtStartTime, " +
                //    " dtEndDate AS dtEndDate, dtEndTime AS dtEndTime, " +
                //    " nASBaseID AS PrvResUsrID, " +
                //    " nASBaseFlag AS PrvResUsrFlag, " +
                //    " nColorCode AS ColorCode, " +
                //    " 2 as isApp " +
                //    " FROM  AS_Schedule_DTL  " +
                //    " WHERE ((nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " AND nASBaseID IN " + _InProviderIDs + " ) OR (nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND nASBaseID IN " + _InResourceIDs + " )) AND (nClinicID =  " + ClinicID + ") " +
                //    " AND (dtStartDate >=  " + FromDate + " ) AND (dtStartDate <= " + ToDate + ") AND   (dtEndDate >= " + FromDate + ") AND (dtEndDate <= " + ToDate + ") " +
                //    "  ) a " +
                //    " ORDER BY a.dtStartDate, a.nLineNumber, a.isApp";
                //}
                //else
                //{

                //    //  _sqlQuery = "SELECT * FROM ( "  //Removed Select *
                //    _sqlQuery = "SELECT nLineNumber,	ASText,	ASTag,	ASDescription,	dtStartDate,	dtStartTime,	dtEndDate,	dtEndTime,	PrvResUsrID,	PrvResUsrFlag,	ColorCode,	isApp FROM ( "
                //                + " SELECT nLineNumber AS nLineNumber,(ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sLocationName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sDepartmentName, ''))  "
                //                + " AS ASText,  "
                //                + " CASE ISNULL(nRefID,0) WHEN 0 "
                //                + " THEN  (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nDTLAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0))) "
                //                + " ELSE (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nRefID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0))) "
                //                + " END AS ASTag,  "
                //        // + " AS_Appointment_DTL.sNotes AS ASDescription,  "               // solving salesforce case - GLO2010-0007243   
                //                + " CASE ISNULL(nRefID,0) WHEN 0  THEN AS_Appointment_DTL.sNotes "
                //                + " ELSE "
                //                + " 	( SELECT APP_DTL.sNotes FROM  AS_Appointment_DTL  AS APP_DTL "
                //                + " 	  WHERE APP_DTL.nDTLAppointmentID =	AS_Appointment_DTL.nRefID"
                //                + " 	)"
                //                + " END  AS ASDescription, "                                    //End
                //                + " AS_Appointment_DTL.dtStartDate AS dtStartDate, AS_Appointment_DTL.dtStartTime AS dtStartTime,  "
                //                + " AS_Appointment_DTL.dtEndDate AS dtEndDate, AS_Appointment_DTL.dtEndTime AS dtEndTime,   "
                //                + " AS_Appointment_DTL.nASBaseID AS PrvResUsrID, "
                //                + " AS_Appointment_DTL.nASBaseFlag As PrvResUsrFlag, "
                //                + " AS_Appointment_DTL.nColorCode AS ColorCode, "
                //                + " 1 as isApp  "
                //                + " FROM Patient RIGHT OUTER JOIN AS_Appointment_MST ON Patient.nPatientID = AS_Appointment_MST.nPatientID RIGHT OUTER JOIN AS_Appointment_DTL ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID  "
                //                + " WHERE ((AS_Appointment_DTL.nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " AND AS_Appointment_DTL.nASBaseID IN " + _InProviderIDs + ") OR (AS_Appointment_DTL.nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND AS_Appointment_DTL.nASBaseID IN " + _InResourceIDs + ")) "
                //                + " AND (AS_Appointment_DTL.nClinicID = " + ClinicID + ") AND  "
                //                + " (AS_Appointment_DTL.dtStartDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtStartDate <= " + ToDate + ") "
                //                + " AND (AS_Appointment_DTL.dtEndDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtEndDate <= " + ToDate + ") and (AS_Appointment_DTL.nUsedStatus not in (5,6,7)) "
                //                + " UNION "
                //                + " SELECT nLineNumber AS nLineNumber, ISNULL(sLocationName,'') + ',' + SPACE(1) + ISNULL(sDepartmentName,'') "
                //                + " AS ASText,  "
                //                + " (Convert(varchar,nMSTScheduleID) + '~' + Convert(varchar,nDTLScheduleID) + '~' + Convert(varchar,bIsSingleRecurrence) + '~3~' + Convert(varchar,ISNULL(nLocationID,0)) + '~' + ISNULL(sLocationName, '') + '~' + Convert(varchar,ISNULL(nDepartmentID,0)) + '~' + ISNULL(sDepartmentName, '') + '~' + + CONVERT(VARCHAR,ISNULL(nLineNumber, 0)) + '~' + '11') "
                //                + " AS ASTag, "
                //                + " sNotes AS ASDescription, "
                //                + " dtStartDate AS dtStartDate,dtStartTime AS dtStartTime, "
                //                + " dtEndDate AS dtEndDate, dtEndTime AS dtEndTime, "
                //                + " nASBaseID AS PrvResUsrID, "
                //                + " nASBaseFlag AS PrvResUsrFlag, "
                //                + " nColorCode AS ColorCode, "
                //                + " 2 as isApp "
                //                + " FROM  AS_Schedule_DTL  "
                //                + " WHERE ((nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " AND nASBaseID IN " + _InProviderIDs + " ) OR (nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND nASBaseID IN " + _InResourceIDs + " )) AND (nClinicID =  " + ClinicID + ") "
                //                + " AND (dtStartDate >=  " + FromDate + " ) AND (dtStartDate <= " + ToDate + ") AND   (dtEndDate >= " + FromDate + ") AND (dtEndDate <= " + ToDate + ")  ) a "
                //                + " ORDER BY a.dtStartDate, a.nLineNumber, a.isApp";
                //}


                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                DataTable dt = new DataTable();

                oDBParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@fromDate", FromDate, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@ToDate", ToDate, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@ProviderFlag", ASBaseType.Provider.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@ProviderIDs", _InProviderIDs, ParameterDirection.Input, SqlDbType.Text);
                oDBParameters.Add("@ResourceFlag", ASBaseType.Resource.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@ResourceIDs", _InResourceIDs, ParameterDirection.Input, SqlDbType.Text);
                oDBParameters.Add("@includeTemplateBlocks", IncludeTemplateBlocks, ParameterDirection.Input, SqlDbType.Bit);


                oDB.Connect(false);

                oDB.Retrive("GetCalendarAppointments", oDBParameters, out dt);

                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;


                //oDB.Retrive_Query(_sqlQuery, out dt);


                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CalendarApointmentSchedule oShortAppointment = new CalendarApointmentSchedule();

                        //Appointment  ASText = Patient Name + Location + Department
                        oShortAppointment.ASText = dt.Rows[i]["ASText"].ToString();
                        //Appointment  ASTag = 
                        //Master ID + Detail ID + Single/Recurrence + Is Appointmnet or Template Block Hash Code Value (AppointmentScheduleFlag) + Location ID + Location Name + Department ID + Department Name (Location & Department for send to app form while register from template block) + Line Number
                        oShortAppointment.ASTag = dt.Rows[i]["ASTag"].ToString();
                        //Appointment Description = Notes
                        oShortAppointment.ASDescription = dt.Rows[i]["ASDescription"].ToString();
                        oShortAppointment.StartDateTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dt.Rows[i]["dtStartDate"])), Convert.ToInt32(dt.Rows[i]["dtStartTime"]));
                        oShortAppointment.EndDateTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dt.Rows[i]["dtEndDate"])), Convert.ToInt32(dt.Rows[i]["dtEndTime"]));
                        oShortAppointment.PrvResUsrID = Convert.ToInt64(dt.Rows[i]["PrvResUsrID"]);
                        oShortAppointment.PrvResUsrFlag = (ASBaseType)Convert.ToInt32(dt.Rows[i]["PrvResUsrFlag"]);
                        oShortAppointment.ColorCode = Convert.ToInt32(dt.Rows[i]["ColorCode"].ToString());
                        oShortAppointment.IsApp = Convert.ToInt32(dt.Rows[i]["isApp"].ToString());
                        oAppointments.Add(oShortAppointment);
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            return oAppointments;
        }


        public CalendarApointmentSchedules GetCalendarAppointments(DateTime FromDate, DateTime ToDate, ArrayList ProviderIDs, ArrayList ResourceIDs, Int64 ClinicID, Boolean IncludeTemplateBlocks)
        {
            //  return GetCalendarAppointments(gloDateMaster.gloDate.DateAsNumber(FromDate.ToString()), gloDateMaster.gloDate.DateAsNumber(ToDate.ToString()), ProviderIDs, ResourceIDs, ClinicID, IncludeTemplateBlocks);
            return GetCalendarAppointmentList(gloDateMaster.gloDate.DateAsNumber(FromDate.ToString()), gloDateMaster.gloDate.DateAsNumber(ToDate.ToString()), ProviderIDs, ResourceIDs, ClinicID, IncludeTemplateBlocks);
        }

        //With location filter
        public CalendarApointmentSchedules GetCalendarAppointments(Int32 FromDate, Int32 ToDate, ArrayList ProviderIDs, ArrayList ResourceIDs, ArrayList Locations, Int64 ClinicID, Boolean IncludeTemplateBlocks)
        {
            CalendarApointmentSchedules oAppointments = new CalendarApointmentSchedules();
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _sqlQuery = "";
                // string _InPrvResIDs = "";

                //--
                string _InProviderIDs = "";
                string _InResourceIDs = "";
                string _InLocations = "";

                //Provider IDs
                for (int i = 0; i < ProviderIDs.Count; i++)
                {
                    if (i == 0)
                    {
                        _InProviderIDs = "(" + ProviderIDs[i].ToString();
                    }
                    else
                    {
                        _InProviderIDs = _InProviderIDs + "," + ProviderIDs[i].ToString();
                    }

                    if (i == ProviderIDs.Count - 1)
                    {
                        _InProviderIDs = _InProviderIDs + ")";
                    }
                }
                if (_InProviderIDs.Length <= 0)
                    _InProviderIDs = "(null)";


                //Resource IDs
                for (int i = 0; i < ResourceIDs.Count; i++)
                {
                    if (i == 0)
                    {
                        _InResourceIDs = "(" + ResourceIDs[i].ToString();
                    }
                    else
                    {
                        _InResourceIDs = _InResourceIDs + "," + ResourceIDs[i].ToString();
                    }

                    if (i == ResourceIDs.Count - 1)
                    {
                        _InResourceIDs = _InResourceIDs + ")";
                    }
                }
                if (_InResourceIDs.Length <= 0)
                    _InResourceIDs = "(null)";

                for (int i = 0; i < Locations.Count; i++)
                {
                    if (i == 0)
                    {
                        _InLocations = "('" + Locations[i].ToString().Replace("'", "''") + "'";
                    }
                    else
                    {
                        _InLocations = _InLocations + ",'" + Locations[i].ToString().Replace("'", "''") + "'";
                    }

                    if (i == Locations.Count - 1)
                    {
                        _InLocations = _InLocations + "," + "'<All Locations>'" + "," + "' '" + ")";
                    }
                }


                if (IncludeTemplateBlocks == true)
                {

                    _sqlQuery =
                        " DECLARE @ApptStatus as Table (nUsedStatus int null)														             " +
                    " 																																	 " +
                    " DELETE FROM @ApptStatus;																											 " +
                    "        With A as (																												 " +
                    "        select   5  as nUsedStatus   from settings where sSettingsName = 'ShowNoShowAppointmentOnCalender' and sSettingsValue = '1' " +
                    "        union																														 " +
                    "         select  6  as nUsedStatus from settings where sSettingsName = 'ShowCancelAppointmentOnCalender' and sSettingsValue = '1'	 " +
                    "        Union																														 " +
                    "        select 7 as nUsedStatus                                                                                                     " +
                    "        )																															 " +
                    "        Insert Into @ApptStatus    SELECT nUsedStatus from A;																		 " +
                    " 																																	 " +
                    " 																																	 " +
                    "        IF (NOT EXISTS       (SELECT nSettingsID FROM settings WHERE ssettingsname ='ShowNoShowAppointmentOnCalender'))			 " +
                    "        BEGIN																														 " +
                    "         Insert Into @ApptStatus    SELECT  5 as nUsedStatus																		 " +
                    "        END																														 " +
                    " 																																	 " +
                    "        IF (NOT EXISTS       (SELECT nSettingsID FROM settings WHERE ssettingsname ='ShowCancelAppointmentOnCalender'))			 " +
                    "        BEGIN																														 " +
                    "         Insert Into @ApptStatus    SELECT  6 as nUsedStatus																		 " +
                    "        END																														 " +
    
                    "SELECT nLineNumber, ASText, ASTag,	ASDescription,	dtStartDate, dtStartTime, dtEndDate,	dtEndTime, PrvResUsrID,PrvResUsrFlag, ColorCode, isApp FROM (" +
                     " SELECT nLineNumber AS nLineNumber,(ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sLocationName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sDepartmentName, ''))  " +
                     " AS ASText,  " +
                     " CASE ISNULL(nRefID,0) WHEN 0 " +
                     " THEN  (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nDTLAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0))) " +
                     " ELSE (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nRefID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0))) " +
                     " END AS ASTag,  " +
                        //" AS_Appointment_DTL.sNotes AS ASDescription,  " +    // solving salesforce case - GLO2010-0007243   
                     " CASE ISNULL(nRefID,0) WHEN 0  THEN AS_Appointment_DTL.sNotes " +
                     " ELSE " +
                     " 	( SELECT APP_DTL.sNotes FROM  AS_Appointment_DTL  AS APP_DTL  WITH(NOLOCK) " +
                     " 	  WHERE APP_DTL.nDTLAppointmentID =	AS_Appointment_DTL.nRefID" +
                     " 	)" +
                     " END  AS ASDescription, " +                            //End
                     " AS_Appointment_DTL.dtStartDate AS dtStartDate, AS_Appointment_DTL.dtStartTime AS dtStartTime,  " +
                     " AS_Appointment_DTL.dtEndDate AS dtEndDate, AS_Appointment_DTL.dtEndTime AS dtEndTime,  " +
                     " AS_Appointment_DTL.nASBaseID AS PrvResUsrID, AS_Appointment_DTL.nASBaseFlag As PrvResUsrFlag,  " +
                     " AS_Appointment_DTL.nColorCode AS ColorCode,1 as isApp" +
                     " FROM Patient  WITH(NOLOCK) RIGHT OUTER JOIN AS_Appointment_MST  WITH(NOLOCK) ON Patient.nPatientID = AS_Appointment_MST.nPatientID RIGHT OUTER JOIN AS_Appointment_DTL  WITH(NOLOCK) ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID  " +
                     " WHERE ((AS_Appointment_DTL.nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " AND AS_Appointment_DTL.nASBaseID IN " + _InProviderIDs + ") OR (AS_Appointment_DTL.nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND AS_Appointment_DTL.nASBaseID IN " + _InResourceIDs + "))" +
                     " AND (AS_Appointment_DTL.sLocationName IN " + _InLocations + ")" +
                     " AND (AS_Appointment_DTL.nClinicID = " + ClinicID + ") AND  " +
                     " (AS_Appointment_DTL.dtStartDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtStartDate <= " + ToDate + ") " +
                     " AND  (AS_Appointment_DTL.dtEndDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtEndDate <= " + ToDate + ") and (AS_Appointment_DTL.nUsedStatus not in (select nUsedStatus from @ApptStatus)) " +

                     " UNION " +
                     " SELECT nLineNumber AS nLineNumber,(ISNULL(sAppointmentTypeDesc, '') + ',' + SPACE(1) + ISNULL(sLocationName, '') + ',' + SPACE(1) + ISNULL(sDepartmentName, '')) " +
                     " AS ASText, " +
                     " (Convert(varchar,nTemplateAllocationMasterID) + '~' + Convert(varchar,nTemplateAllocationID) + '~' + '1' + '~' + '6' + '~' + Convert(varchar,ISNULL(nLocationID,0)) + '~' + ISNULL(sLocationName, '') + '~' + Convert(varchar,ISNULL(nDepartmentID,0)) + '~' + ISNULL(sDepartmentName, '') + '~' + CONVERT( VARCHAR,ISNULL(nLineNumber, 0)) + '~0')  " +
                     " AS ASTag, " +
                     " sTemplateName AS ASDescription,  " +
                     " dtStartDate AS dtStartDate,dtStartTime AS dtStartTime, " +
                     " dtEndDate AS dtEndDate, dtEndTime AS dtEndTime," +
                     " nASBaseID AS PrvResUsrID, nASBaseFlag As PrvResUsrFlag, " +
                     " nColorCode AS ColorCode,2 as isApp " +
                     " FROM AB_AppointmentTemplate_Allocation  WITH(NOLOCK) WHERE " +
                     " nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " and nASBaseID IN " + _InProviderIDs + " and nClinicID = " + ClinicID + "" +
                     " AND (sLocationName IN " + _InLocations + ") and " +
                     " (dtStartDate >= " + FromDate + " and dtStartDate <= " + ToDate + ") and " +
                     " (dtEndDate >= " + FromDate + " and dtEndDate <= " + ToDate + ") and " +
                     " (nIsRegistered = 0 or nIsRegistered is null) " +

                     "  UNION " +
                     " SELECT AS_Schedule_DTL.nLineNumber AS nLineNumber, ISNULL(AS_Schedule_DTL.sLocationName,'') + ',' + SPACE(1) + ISNULL(AS_Schedule_DTL.sDepartmentName,'') " +
                     " AS ASText,  " +
                     " (Convert(varchar,AS_Schedule_DTL.nMSTScheduleID) + '~' + Convert(varchar,AS_Schedule_DTL.nDTLScheduleID) + '~' + Convert(varchar,AS_Schedule_DTL.bIsSingleRecurrence) + '~3~' + Convert(varchar,ISNULL(AS_Schedule_DTL.nLocationID,0)) + '~' + ISNULL(AS_Schedule_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Schedule_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Schedule_DTL.sDepartmentName, '') + '~' + + CONVERT(VARCHAR,ISNULL(AS_Schedule_DTL.nLineNumber, 0)) + '~' + '11') " +
                     " AS ASTag, " +
                     " case when AS_Schedule_MST.sASBaseDesc = '' then AS_Schedule_DTL.sNotes else case when AS_Schedule_Mst.nasBaseFlag = 5 then case when ltrim(AS_Schedule_DTL.sNotes) = '' then AS_Schedule_MST.sASBaseDesc else AS_Schedule_MST.sASBaseDesc + ' : ' + AS_Schedule_DTL.sNotes end else  AS_Schedule_DTL.sNotes end end AS ASDescription, " +
                        //" sNotes AS ASDescription, " + // for GLO2012-0016240 Appointment block type by time - Changes the query insert inner join with AS_Schedule_MST
                     " AS_Schedule_DTL.dtStartDate AS dtStartDate,AS_Schedule_DTL.dtStartTime AS dtStartTime, " +
                     " AS_Schedule_DTL.dtEndDate AS dtEndDate, AS_Schedule_DTL.dtEndTime AS dtEndTime, " +
                     " AS_Schedule_DTL.nASBaseID AS PrvResUsrID, " +
                     " AS_Schedule_DTL.nASBaseFlag AS PrvResUsrFlag, " +
                     " AS_Schedule_DTL.nColorCode AS ColorCode, " +
                     " 2 as isApp " +
                     " FROM  AS_Schedule_DTL   WITH(NOLOCK) " +
                     " INNER JOIN dbo.AS_Schedule_MST ON dbo.AS_Schedule_DTL.nMSTScheduleID = dbo.AS_Schedule_MST.nMSTScheduleID " +
                     " WHERE ((AS_Schedule_DTL.nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " AND AS_Schedule_DTL.nASBaseID IN " + _InProviderIDs + " ) OR (AS_Schedule_DTL.nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND AS_Schedule_DTL.nASBaseID IN " + _InResourceIDs + " )) AND (AS_Schedule_DTL.nClinicID =  " + ClinicID + ") " +
                     " AND (AS_Schedule_DTL.sLocationName IN " + _InLocations + ")" +
                     " AND (AS_Schedule_DTL.dtStartDate >=  " + FromDate + " ) AND (AS_Schedule_DTL.dtStartDate <= " + ToDate + ") AND   (AS_Schedule_DTL.dtEndDate >= " + FromDate + ") AND (AS_Schedule_DTL.dtEndDate <= " + ToDate + ") " +
                     "  ) a " +
                     " ORDER BY a.dtStartDate, a.nLineNumber, a.isApp";
                }
                else
                {

                    _sqlQuery =

                        " DECLARE @ApptStatus as Table (nUsedStatus int null)														             " +
                    " 																																	 " +
                    " DELETE FROM @ApptStatus;																											 " +
                    "        With A as (																												 " +
                    "        select   5  as nUsedStatus   from settings where sSettingsName = 'ShowNoShowAppointmentOnCalender' and sSettingsValue = '1' " +
                    "        union																														 " +
                    "         select  6  as nUsedStatus from settings where sSettingsName = 'ShowCancelAppointmentOnCalender' and sSettingsValue = '1'	 " +
                    "        Union																														 " +
                    "        select 7 as nUsedStatus                                                                                                     " +
                    "        )																															 " +
                    "        Insert Into @ApptStatus    SELECT nUsedStatus from A;																		 " +
                    " 																																	 " +
                    " 																																	 " +
                    "        IF (NOT EXISTS       (SELECT nSettingsID FROM settings WHERE ssettingsname ='ShowNoShowAppointmentOnCalender'))			 " +
                    "        BEGIN																														 " +
                    "         Insert Into @ApptStatus    SELECT  5 as nUsedStatus																		 " +
                    "        END																														 " +
                    " 																																	 " +
                    "        IF (NOT EXISTS       (SELECT nSettingsID FROM settings WHERE ssettingsname ='ShowCancelAppointmentOnCalender'))			 " +
                    "        BEGIN																														 " +
                    "         Insert Into @ApptStatus    SELECT  6 as nUsedStatus																		 " +
                    "        END																														 " +
    
                    "SELECT nLineNumber, ASText, ASTag,	ASDescription,	dtStartDate, dtStartTime, dtEndDate,	dtEndTime, PrvResUsrID,PrvResUsrFlag, ColorCode, isApp  FROM ( "
                                + " SELECT nLineNumber AS nLineNumber,(ISNULL(Patient.sFirstName, '') + SPACE(1) + ISNULL(Patient.sMiddleName, '') + SPACE(1) + ISNULL(Patient.sLastName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sLocationName, '') + ',' + SPACE(1) + ISNULL(AS_Appointment_DTL.sDepartmentName, ''))  "
                                + " AS ASText,  "
                                + " CASE ISNULL(nRefID,0) WHEN 0 "
                                + " THEN  (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nDTLAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0))) "
                                + " ELSE (Convert(varchar,AS_Appointment_DTL.nMSTAppointmentID) + '~' + Convert(varchar,AS_Appointment_DTL.nRefID) + '~' + Convert(varchar,AS_Appointment_DTL.bIsSingleRecurrence) + '~' + '4' + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nLocationID,0)) + '~' + ISNULL(AS_Appointment_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Appointment_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Appointment_DTL.sDepartmentName, '') + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nLineNumber, 0)) + '~' + CONVERT(VARCHAR,ISNULL(AS_Appointment_DTL.nUsedStatus, 0))) "
                                + " END AS ASTag,  "
                        // + " AS_Appointment_DTL.sNotes AS ASDescription,  "       // solving salesforce case - GLO2010-0007243   
                                + " CASE ISNULL(nRefID,0) WHEN 0  THEN AS_Appointment_DTL.sNotes "
                                + " ELSE "
                                + " 	( SELECT APP_DTL.sNotes FROM  AS_Appointment_DTL  AS APP_DTL  WITH(NOLOCK) "
                                + " 	  WHERE APP_DTL.nDTLAppointmentID =	AS_Appointment_DTL.nRefID"
                                + " 	)"
                                + " END  AS ASDescription, "                              // End
                                + " AS_Appointment_DTL.dtStartDate AS dtStartDate, AS_Appointment_DTL.dtStartTime AS dtStartTime,  "
                                + " AS_Appointment_DTL.dtEndDate AS dtEndDate, AS_Appointment_DTL.dtEndTime AS dtEndTime,   "
                                + " AS_Appointment_DTL.nASBaseID AS PrvResUsrID, "
                                + " AS_Appointment_DTL.nASBaseFlag As PrvResUsrFlag, "
                                + " AS_Appointment_DTL.nColorCode AS ColorCode, "
                                + " 1 as isApp  "
                                + " FROM Patient  WITH(NOLOCK) RIGHT OUTER JOIN AS_Appointment_MST  WITH(NOLOCK) ON Patient.nPatientID = AS_Appointment_MST.nPatientID RIGHT OUTER JOIN AS_Appointment_DTL  WITH(NOLOCK) ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID  "
                                + " WHERE ((AS_Appointment_DTL.nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " AND AS_Appointment_DTL.nASBaseID IN " + _InProviderIDs + ") OR (AS_Appointment_DTL.nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND AS_Appointment_DTL.nASBaseID IN " + _InResourceIDs + ")) "
                                + " AND (AS_Appointment_DTL.sLocationName IN " + _InLocations + ")"
                                + " AND (AS_Appointment_DTL.nClinicID = " + ClinicID + ") AND  "
                                + " (AS_Appointment_DTL.dtStartDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtStartDate <= " + ToDate + ") "
                                + " AND (AS_Appointment_DTL.dtEndDate >= " + FromDate + ") AND (AS_Appointment_DTL.dtEndDate <= " + ToDate + ") and (AS_Appointment_DTL.nUsedStatus not in (select nUsedStatus from @ApptStatus)) "

                                + " UNION "
                                + " SELECT AS_Schedule_DTL.nLineNumber AS nLineNumber, ISNULL(AS_Schedule_DTL.sLocationName,'') + ',' + SPACE(1) + ISNULL(AS_Schedule_DTL.sDepartmentName,'') "
                                + " AS ASText,  "
                                + " (Convert(varchar,AS_Schedule_DTL.nMSTScheduleID) + '~' + Convert(varchar,AS_Schedule_DTL.nDTLScheduleID) + '~' + Convert(varchar,AS_Schedule_DTL.bIsSingleRecurrence) + '~3~' + Convert(varchar,ISNULL(AS_Schedule_DTL.nLocationID,0)) + '~' + ISNULL(AS_Schedule_DTL.sLocationName, '') + '~' + Convert(varchar,ISNULL(AS_Schedule_DTL.nDepartmentID,0)) + '~' + ISNULL(AS_Schedule_DTL.sDepartmentName, '') + '~' + + CONVERT(VARCHAR,ISNULL(AS_Schedule_DTL.nLineNumber, 0)) + '~' + '11') "
                                + " AS ASTag, "
                                + " case when AS_Schedule_MST.sASBaseDesc = '' then AS_Schedule_DTL.sNotes else case when AS_Schedule_Mst.nasBaseFlag = 5 then case when ltrim(AS_Schedule_DTL.sNotes) = '' then AS_Schedule_MST.sASBaseDesc else AS_Schedule_MST.sASBaseDesc + ' : ' + AS_Schedule_DTL.sNotes end else  AS_Schedule_DTL.sNotes end end AS ASDescription, "
                        //+ " sNotes AS ASDescription, " //for GLO2012-0016240 Appointment block type by time - Changes the query insert inner join with AS_Schedule_MST
                                + " AS_Schedule_DTL.dtStartDate AS dtStartDate,AS_Schedule_DTL.dtStartTime AS dtStartTime, "
                                + " AS_Schedule_DTL.dtEndDate AS dtEndDate, AS_Schedule_DTL.dtEndTime AS dtEndTime, "
                                + " AS_Schedule_DTL.nASBaseID AS PrvResUsrID, "
                                + " AS_Schedule_DTL.nASBaseFlag AS PrvResUsrFlag, "
                                + " AS_Schedule_DTL.nColorCode AS ColorCode, "
                                + " 2 as isApp "
                                + " FROM  AS_Schedule_DTL  WITH(NOLOCK)  "
                                + " INNER JOIN dbo.AS_Schedule_MST  WITH(NOLOCK) ON dbo.AS_Schedule_DTL.nMSTScheduleID = dbo.AS_Schedule_MST.nMSTScheduleID "
                                + " WHERE ((AS_Schedule_DTL.nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " AND AS_Schedule_DTL.nASBaseID IN " + _InProviderIDs + " ) OR (AS_Schedule_DTL.nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND AS_Schedule_DTL.nASBaseID IN " + _InResourceIDs + " )) AND (AS_Schedule_DTL.nClinicID =  " + ClinicID + ") "
                                + " AND (AS_Schedule_DTL.sLocationName IN " + _InLocations + ")"
                                + " AND (AS_Schedule_DTL.dtStartDate >=  " + FromDate + " ) AND (AS_Schedule_DTL.dtStartDate <= " + ToDate + ") AND   (AS_Schedule_DTL.dtEndDate >= " + FromDate + ") AND (AS_Schedule_DTL.dtEndDate <= " + ToDate + ") ) a "
                                + " ORDER BY a.dtStartDate, a.nLineNumber, a.isApp";
                }

                DataTable dt = new DataTable();
                oDB.Retrive_Query(_sqlQuery, out dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CalendarApointmentSchedule oShortAppointment = new CalendarApointmentSchedule();

                        //Appointment  ASText = Patient Name + Location + Department
                        oShortAppointment.ASText = dt.Rows[i]["ASText"].ToString();
                        //Appointment  ASTag = 
                        //Master ID + Detail ID + Single/Recurrence + Is Appointment or Template Block Hash Code Value (AppointmentScheduleFlag) + Location ID + Location Name + Department ID + Department Name (Location & Department for send to app form while register from template block) + Line Number
                        oShortAppointment.ASTag = dt.Rows[i]["ASTag"].ToString();
                        //Appointment Description = Notes
                        oShortAppointment.ASDescription = dt.Rows[i]["ASDescription"].ToString();
                        oShortAppointment.StartDateTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dt.Rows[i]["dtStartDate"])), Convert.ToInt32(dt.Rows[i]["dtStartTime"]));
                        oShortAppointment.EndDateTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dt.Rows[i]["dtEndDate"])), Convert.ToInt32(dt.Rows[i]["dtEndTime"]));
                        oShortAppointment.PrvResUsrID = Convert.ToInt64(dt.Rows[i]["PrvResUsrID"]);
                        oShortAppointment.PrvResUsrFlag = (ASBaseType)Convert.ToInt32(dt.Rows[i]["PrvResUsrFlag"]);
                        oShortAppointment.ColorCode = Convert.ToInt32(dt.Rows[i]["ColorCode"].ToString());
                        oAppointments.Add(oShortAppointment);
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            return oAppointments;
        }

        public CalendarApointmentSchedules GetCalendarAppointments(DateTime FromDate, DateTime ToDate, ArrayList ProviderIDs, ArrayList ResourceIDs, ArrayList Locations, Int64 ClinicID, Boolean IncludeTemplateBlocks)
        {
            return GetCalendarAppointments(gloDateMaster.gloDate.DateAsNumber(FromDate.ToString()), gloDateMaster.gloDate.DateAsNumber(ToDate.ToString()), ProviderIDs, ResourceIDs, Locations, ClinicID, IncludeTemplateBlocks);
        }


        public DataTable GetPatientAppointments(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            object _intresult = new object();
            DataTable oData = new DataTable();

            try
            {
                oDB.Connect(false);
                _strSQL = " SELECT AS_Appointment_DTL.nMSTAppointmentID,AS_Appointment_DTL.nDTLAppointmentID, " +
                         " AS_Appointment_DTL.dtStartDate AS Date, " +
                         " AS_Appointment_DTL.dtStartTime AS StartTime, " +
                         " AS_Appointment_DTL.dtEndTime AS EndTime, " +
                         " ISNULL(AS_Appointment_DTL.sLocationName,'') AS Location , " +
                         " ISNULL(AS_Appointment_DTL.sDepartmentName,'') AS Department ," +
                         " ISNULL(AS_Appointment_MST.sAppointmentTypeDesc,'') AS AppointmentType, " +
                         " ISNULL(AS_Appointment_DTL.nASBaseID,0) AS nASBaseID , " +
                         " ISNULL(AS_Appointment_DTL.sASBaseDesc,'') AS Provider, " +
                         " ISNULL(AS_Appointment_DTL.sNotes,'') AS Notes , " +
                         " ISNULL(AS_Appointment_DTL.nUsedStatus,0) AS nUsedStatus, " +
                         " ISNULL(AS_Appointment_DTL.bIsSingleRecurrence,0) AS DTLAppMethod, " +
                         " ISNULL(AS_Appointment_MST.bIsSingleRecurrence,0) AS MSTAppMethod, " +
                         " ISNULL(AS_Appointment_DTL.nLineNumber,0) AS nLineNumber " +
                         " FROM AS_Appointment_MST  WITH(NOLOCK) INNER JOIN AS_Appointment_DTL  WITH(NOLOCK) ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID " +
                         " WHERE (AS_Appointment_MST.nPatientID = " + PatientID + ") " +
                         " AND AS_Appointment_DTL.nRefID = 0 AND (AS_Appointment_DTL.nClinicID = " + this._ClinicID + ") ORDER BY Date Desc,StartTime ";
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

        public string GetAppointmentReferral(long MasterAppointmentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable oData = new DataTable();
            string _result = "";
            try
            {
                oDB.Connect(false);
                string _strSQL = "";

                _strSQL = "SELECT ISNULL(nReferralProviderName,'') AS sReferralProviderName FROM AS_Appointment_MST  WITH(NOLOCK) WHERE  nMSTAppointmentID = " + MasterAppointmentID + " AND nClinicID = " + _ClinicID + "";

                oDB.Retrive_Query(_strSQL, out oData);
                if (oData != null && oData.Rows.Count > 0)
                {
                    _result = Convert.ToString(oData.Rows[0]["sReferralProviderName"]);
                }

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
                oDB.Dispose();
            }

            return _result;
        }

        public bool IsPatientCheckIn(long MasterAppointmentID, long DetailAppointmentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable oData = new DataTable();
            bool _result = false;
            try
            {
                oDB.Connect(false);
                string _strSQL = "";

                if (IsPatientCheckOut(MasterAppointmentID, DetailAppointmentID) == false)
                {
                    _strSQL = "SELECT COUNT(nID) FROM PatientTracking   WITH(NOLOCK) " +
                    " WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nDTLAppointmentID = " + DetailAppointmentID + " AND nTrackingStatus = " + ASUsedStatus.CheckIn.GetHashCode() + " AND nClinicID = " + _ClinicID + " ";

                    object ResultCount = oDB.ExecuteScalar_Query(_strSQL);

                    if (ResultCount != null && Convert.ToString(ResultCount) != "")
                    {
                        if (Convert.ToInt32(ResultCount) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                else
                {
                    _result = false;
                }

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
                oDB.Dispose();
            }

            return _result;
        }

        public bool IsPatientCheckOut(long MasterAppointmentID, long DetailAppointmentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable oData = new DataTable();
            bool _result = false;
            try
            {
                oDB.Connect(false);
                string _strSQL = "";

                _strSQL = "SELECT COUNT(nID) FROM PatientTracking   WITH(NOLOCK) " +
                " WHERE nMSTAppointmentID = " + MasterAppointmentID + " AND nDTLAppointmentID = " + DetailAppointmentID + " AND nTrackingStatus = " + ASUsedStatus.CheckOut.GetHashCode() + " AND nClinicID = " + _ClinicID + "  AND bIsCheckOut=1";

                object ResultCount = oDB.ExecuteScalar_Query(_strSQL);

                if (ResultCount != null && Convert.ToString(ResultCount) != "")
                {
                    if (Convert.ToInt32(ResultCount) > 0)
                    {
                        _result = true;
                    }
                }

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
                oDB.Dispose();
            }

            return _result;
        }

        //CHANGE ITS RETURN TYPE COZ FOR MODIFY & NEW WE NEED TO CHECK DIFF. COUNT 20110326 RESOLVED 10290
        //public bool IsAppointmentOnToday(Int64 _PatientID, Int64 _ClinicID, DateTime AppointmentDate, Int64 _TempAppointmentID)
        public Int64 IsAppointmentOnToday(Int64 _PatientID, Int64 _ClinicID, DateTime AppointmentDate, Int64 _TempAppointmentID)
        {
            //bool _result = false;
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                Int64 appdate = gloDateMaster.gloDate.DateAsNumber(AppointmentDate.ToShortDateString());
                string strquery = "SELECT  COUNT(AS_Appointment_DTL.nDTLAppointmentID)" +
                            " FROM AS_Appointment_MST  WITH(NOLOCK) INNER JOIN" +
                            " AS_Appointment_DTL  WITH(NOLOCK) ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID" +
                            " WHERE     (AS_Appointment_MST.nPatientID = '" + _PatientID + "') AND (AS_Appointment_MST.nClinicID = '" + _ClinicID + "') AND (AS_Appointment_DTL.dtStartDate = '" + appdate + "') AND " +
                            " (AS_Appointment_DTL.nRefFlag = 0) and AS_Appointment_DTL.nUsedStatus!='" + ASUsedStatus.Delete.GetHashCode() + "' and AS_Appointment_DTL.nUsedStatus!='" + ASUsedStatus.NoShow.GetHashCode() + "'  and AS_Appointment_DTL.nUsedStatus!='" + ASUsedStatus.Cancel.GetHashCode() + "' and AS_Appointment_DTL.nMSTAppointmentID!='" + _TempAppointmentID + "'";


                oDB.Connect(false);
                object value = oDB.ExecuteScalar_Query(strquery);

                if (value != null && Convert.ToString(value) != "")
                {
                    _result = Convert.ToInt64(value);
                }

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
                oDB.Dispose();
            }

            return _result;
        }

        // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
        // Function added to get the visitID
        public long GetCurrentVisitID(long nPatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            long oData = 0;
            if (_MessageBoxCaption != "gloPM")
            {
                try
                {
                    oDB.Connect(false);

                    _strSQL = "select nVisitID as VisitID from Visits  WITH(NOLOCK) where nPatientID = " + nPatientID + "and Convert(Varchar,dtVisitDate,101) = Convert(Varchar,dbo.gloGetDate() ,101)";

                    object value = oDB.ExecuteScalar_Query(_strSQL);

                    if (value != null && Convert.ToString(value) != "")
                    { oData = Convert.ToInt64(value); }
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
            }
            return oData;
        }

        public DataTable GetPatient(long MasterAppointmentID, long DetailAppointmentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            DataTable oData = new DataTable();

            try
            {
                oDB.Connect(false);

                _strSQL = "SELECT AS_Appointment_MST.nPatientID, ISNULL(Patient.sPatientCode,'') AS  sPatientCode,"
                + " ISNULL(Patient.sFirstName,'') + SPACE(1) + ISNULL(Patient.sMiddleName,'') + SPACE(1) + ISNULL(Patient.sLastName,'') AS sPatientName, "
                + " dbo.FormatPhone(ISNULL(Patient.sPhone,''),0) AS PatientPhone, dbo.FormatPhone(ISNULL(Patient.sMobile,''),0) AS PatientMobile, AS_Appointment_MST.nASBaseID as nASBaseID  "
                + " FROM  AS_Appointment_DTL  WITH(NOLOCK) INNER JOIN AS_Appointment_MST  WITH(NOLOCK) ON AS_Appointment_DTL.nMSTAppointmentID = AS_Appointment_MST.nMSTAppointmentID "
                + " INNER JOIN Patient  WITH(NOLOCK) ON AS_Appointment_MST.nPatientID = Patient.nPatientID"
                + " WHERE AS_Appointment_DTL.nMSTAppointmentID = " + MasterAppointmentID + " "
                + " AND AS_Appointment_DTL.nDTLAppointmentID = " + DetailAppointmentID + " "
                + " AND AS_Appointment_DTL.nClinicID = " + _ClinicID + "";

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

        //Bug #65081: 00000640 : Appointment cancellation entries not showining entry in Audit Trail
        //Added new function to get Patient Id for current appointment
        public object GetPatientId(long MasterAppointmentID)
        {
            object result=null;
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                oDBParameters.Add("@nMSTAppointmentID", MasterAppointmentID, ParameterDirection.Input, SqlDbType.Decimal);

                oDB.Connect(false);

                DataTable dt = new DataTable();

                oDB.Retrive("get_PatientId_From_Appointment", oDBParameters, out dt);

                if (dt.Rows.Count>0)
                {
                    result = dt.Rows[0][0];
                }

                oDB.Disconnect();
                oDB = null;
                return result;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }

        }

        public DataTable GetUpdatedDateTime(long MasterAppointmentID, long DetailAppointmentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            DataTable oData = new DataTable();

            try
            {
                oDB.Connect(false);

                _strSQL = "SELECT User_MST.sLoginName AS [User], AS_Appointment_DTL.dtUpdatedDateTime AS UpdatedDateTime FROM AS_Appointment_DTL  WITH(NOLOCK) INNER JOIN User_MST  WITH(NOLOCK) ON AS_Appointment_DTL.nUserID = User_MST.nUserID WHERE AS_Appointment_DTL.nDTLAppointmentID = " + DetailAppointmentID;
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
                if (oDB != null)
                {oDB.Disconnect(); oDB.Dispose();}
                if (oData != null) { oData.Dispose();}
            }

            return oData;
        }

        public DataTable GetAppointmentProviderID(long MasterAppointmentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            DataTable oData = new DataTable();

            try
            {
                oDB.Connect(false);

                _strSQL = "select nASBaseID from AS_Appointment_MST  WITH(NOLOCK) WHERE nMSTAppointmentID = " + MasterAppointmentID;

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


        // GLO2011-0011970 
        // Created a function to check the patient status is Legal Pending or not
        public bool IsLegalPending(long MasterAppointmentID, long DetailAppointmentID)
        {
            gloAppointment ogloAppointment = new gloAppointment(_databaseconnectionstring);

            Int64 PatientID = 0;
            try
            {
                using (gloPatient.gloPatient objPatient = new gloPatient.gloPatient(_databaseconnectionstring))
                {
                    using (DataTable dtPatient = ogloAppointment.GetPatient(MasterAppointmentID, DetailAppointmentID))
                    {
                        if (dtPatient != null && dtPatient.Rows.Count > 0)
                        {
                            PatientID = Convert.ToInt64(dtPatient.Rows[0]["nPatientID"]);

                            if (objPatient.IsLegalPending(PatientID))
                            {
                                return true;
                            }
                        }
                    }
                }
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
                ogloAppointment.Dispose();
            }

            return false;
        }

        // Instead of this function please use following function
        // PriorAuthorizationTransaction.GetByAppointment()
        public DataTable GetAppointmentPriorAuthorization(long AppointmentMasterID, long AppointmentDeatailID, SingleRecurrence oSingleRecurrence)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable oData = new DataTable();
            try
            {
                oDB.Connect(false);
                string _sqlQuery = "";

                _sqlQuery = " SELECT nMSTAppointmentID,nDTLAppointmentID,bIsSingleRecurrence, nPatientID ,nAuthorizationID,	nAuthorizationNo,	nInsuranceID,	nBillingTransactionID,	nAppointmentDate,	nIsCheckedIn,	nPATransactionID,	nVersion,	dtPATransactionDate " +
                            " FROM PatientPriorAuthorization_Transaction   WITH(NOLOCK) WHERE nMSTAppointmentID = " + AppointmentMasterID + " AND  nDTLAppointmentID = " + AppointmentDeatailID + " ";

                oDB.Retrive_Query(_sqlQuery, out oData);

                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException oDbex)
            {
                oDbex.ERROR_Log(oDbex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return oData;
        }

        public void AddNotes(Int64 nMSTAppointmentID, Int64 nDTLAppointmentID, string sNotes)
        {
            try
            {
                // if (sNotes.Trim() != "")  // commenting for solving sales force case - GLO2011-0013479 
                // {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string _strSQL = "";
                object Value = new object();
                DataTable oData = new DataTable();
                try
                {
                    oDB.Connect(false);
                    sNotes = " " + sNotes.Trim();
                    _strSQL = "UPDATE  AS_Appointment_DTL SET sNotes = '" + sNotes.Replace("'", "''").Trim() + "', nUserID = " + _UserID + ", dtUpdatedDateTime = '" + DateTime.Now + "' WHERE nMSTAppointmentID = " + nMSTAppointmentID + " AND nDTLAppointmentID = " + nDTLAppointmentID + " AND nClinicID = " + _ClinicID + "";
                    oDB.Execute_Query(_strSQL);

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
                //}

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public void AddNotes(Int64 nMSTAppointmentID, Int64 nDTLAppointmentID, string sNotes, Int32 nAppointmentStatusID)
        {
            try
            {
                // if (sNotes.Trim() != "")  // commenting for solving sales force case - GLO2011-0013479 
                // {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string _strSQL = "";
                object Value = new object();
                DataTable oData = new DataTable();
                try
                {
                    oDB.Connect(false);
                    sNotes = " " + sNotes.Trim();
                    // Added Code against Problem no: #648 & Incident Number: 00026223

                    if (nAppointmentStatusID == 0)
                    {
                        _strSQL = "UPDATE AS_Appointment_DTL SET sNotes = '" + sNotes.Replace("'", "''").Trim() + "', nUserID = " + _UserID + ", dtUpdatedDateTime = '" + DateTime.Now + "' WHERE nMSTAppointmentID = " + nMSTAppointmentID + " AND nDTLAppointmentID = " + nDTLAppointmentID + " AND nClinicID = " + _ClinicID + "";
                    }
                    else
                    {
                        _strSQL = "UPDATE AS_Appointment_DTL SET nUsedStatus = " + nAppointmentStatusID + " , sNotes = '" + sNotes.Replace("'", "''").Trim() + "', nUserID = " + _UserID + ", dtUpdatedDateTime = '" + DateTime.Now + "' WHERE nMSTAppointmentID = " + nMSTAppointmentID + " AND (nDTLAppointmentID = " + nDTLAppointmentID + " OR nRefID = " + nDTLAppointmentID + ") AND nClinicID = " + _ClinicID + "";
                    }

                    oDB.Execute_Query(_strSQL);

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
                //}

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        internal void RescheduleAppointment(long _nAppointmentMasterID, long _nAppointmentDetailID, DateTime _RescheduleDate, SingleRecurrence _IsSingleRecurrence)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);

                string _strSQL = "";

                if (_IsSingleRecurrence == SingleRecurrence.Single)
                {

                    _strSQL = "UPDATE AS_Appointment_MST "
                    + " SET dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_RescheduleDate.ToShortDateString()) + " ,dtEndDate = " + gloDateMaster.gloDate.DateAsNumber(_RescheduleDate.ToShortDateString()) + ""
                    + " WHERE nMSTAppointmentID = " + _nAppointmentMasterID + " "
                     + " AND nClinicID = " + _ClinicID + "";

                    oDB.Execute_Query(_strSQL);


                    _strSQL = " UPDATE AS_Appointment_DTL"
                   + " SET dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_RescheduleDate.ToShortDateString()) + " ,dtEndDate = " + gloDateMaster.gloDate.DateAsNumber(_RescheduleDate.ToShortDateString()) + ","
                   + " bIsSingleRecurrence = " + SingleRecurrence.Single.GetHashCode() + ""
                   + " WHERE "
                   + " nMSTAppointmentID = " + _nAppointmentMasterID + ""
                   + " AND (nDTLAppointmentID = " + _nAppointmentDetailID + " OR nRefID = " + _nAppointmentDetailID + ")"
                   + " AND nClinicID = " + _ClinicID + "";
                    oDB.Execute_Query(_strSQL);

                }
                else
                {
                    _strSQL = " UPDATE AS_Appointment_DTL"
                    + " SET dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_RescheduleDate.ToShortDateString()) + " ,dtEndDate = " + gloDateMaster.gloDate.DateAsNumber(_RescheduleDate.ToShortDateString()) + ","
                    + " bIsSingleRecurrence = " + SingleRecurrence.SingleInRecurrence.GetHashCode() + ""
                    + " WHERE "
                    + " nMSTAppointmentID = " + _nAppointmentMasterID + ""
                    + " AND (nDTLAppointmentID = " + _nAppointmentDetailID + " OR nRefID = " + _nAppointmentDetailID + ")"
                    + " AND nClinicID = " + _ClinicID + "";
                    oDB.Execute_Query(_strSQL);
                }

                #region "Generate HL7 Message Queue for Modify Appointment"
                // Code Start - Added by kanchan on 20091205 for HL7 appointment outbound
                if (gloHL7.boolSendAppointmentDetails) //(appSettings["GenerateHL7Message"] != null)
                {
                    //if (appSettings["GenerateHL7Message"] != "")
                    //{
                    //    if ((Convert.ToBoolean(appSettings["GenerateHL7Message"])) == true)
                    //    {
                    if (_nAppointmentDetailID > 0)
                    {
                        long _nPatientID = 0;
                        Object result = null;
                        _strSQL = "SELECT nPatientID from AS_Appointment_MST  WITH(NOLOCK) where nMSTAppointmentID = " + _nAppointmentMasterID;
                        result = oDB.ExecuteScalar_Query(_strSQL);
                        if (result != null)
                        {
                            _nPatientID = Convert.ToInt64(result);
                        }

                        gloHL7.InsertInMessageQueue("S13", _nPatientID, _nAppointmentMasterID, "", _databaseconnectionstring);
                    }
                    //    }
                    //}
                }
                // Code End - Added by kanchan on 20091205 for HL7 appointment outbound
                #endregion

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            //Code Start - Added by kanchan on 20091205 for HL7 appointment outbound
            // Dispose gloDatabaseLayer object
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            //Code End - Added by kanchan on 20091205 for HL7 appointment outbound
        }

        internal void UpdateAppointmentStatus(long nMasterAppointmentID, long nDetailAppointmentID, Int32 _nAppointmentStatusID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            MasterAppointment oAppointment = GetMasterAppointment(nMasterAppointmentID, nDetailAppointmentID, SingleRecurrence.Single, SingleRecurrence.Single, true, _ClinicID);
            try
            {
                oDB.Connect(false);

                oDBParameters.Clear();
                oDBParameters.Add("@nID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtDate", DateTime.Now, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                oDBParameters.Add("@nPatientID", oAppointment.PatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sTimeIn", DateTime.Now.ToShortTimeString(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 50);
                oDBParameters.Add("@sLocation", oAppointment.LocationName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 255);
                oDBParameters.Add("@sStatus", "", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 255);
                oDBParameters.Add("@sTimeOut", "", System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar, 50);
                oDBParameters.Add("@nTrackingStatus", _nAppointmentStatusID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nMSTAppointmentID", nMasterAppointmentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nDTLAppointmentID", nDetailAppointmentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                //oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(oAppointment.PatientID), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                oDB.Execute("gsp_INUP_PatientTracking", oDBParameters);

                oDB.Disconnect();
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
                oDB.Dispose();
                oDBParameters.Dispose();
            }

        }

        internal void UpdateAppointmentUsedStatus(long nMasterAppointmentID, long nDetailAppointmentID, int _nAppointmentUsedStatusID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                string _sqlQuery = "";

                //--------Update Appointment Table
                if (_nAppointmentUsedStatusID == 7)
                {
                    _sqlQuery = "UPDATE AS_Appointment_DTL SET nUsedStatus = " + _nAppointmentUsedStatusID + " , nUserID = " + _UserID + ", dtUpdatedDateTime = '" + DateTime.Now + "' WHERE nMSTAppointmentID = " + nMasterAppointmentID + " AND (nDTLAppointmentID = " + nDetailAppointmentID + " OR nRefID = " + nDetailAppointmentID + " ) AND  nClinicID = " + _ClinicID + " ";
                    oDB.Execute_Query(_sqlQuery);
                }

                //--------Update PAtient Tracking Status
                UpdateAppointmentStatus(nMasterAppointmentID, nDetailAppointmentID, _nAppointmentUsedStatusID);

                //--------Free Template slot if appointment Deleted or Canceled
                if (ASUsedStatus.Delete.GetHashCode() == _nAppointmentUsedStatusID
                    || ASUsedStatus.Cancel.GetHashCode() == _nAppointmentUsedStatusID
                    || ASUsedStatus.NoShow.GetHashCode() == _nAppointmentUsedStatusID)
                {
                    _sqlQuery = " UPDATE AB_AppointmentTemplate_Allocation SET  nIsRegistered = 0 "
                              + " FROM AS_Appointment_DTL INNER JOIN AB_AppointmentTemplate_Allocation ON AS_Appointment_DTL.nTemplateAllocationID = AB_AppointmentTemplate_Allocation.nTemplateAllocationID "
                              + " WHERE AB_AppointmentTemplate_Allocation.nTemplateAllocationID = AS_Appointment_DTL.nTemplateAllocationID "
                              + " AND AS_Appointment_DTL.nDTLAppointmentID = " + nDetailAppointmentID + " AND AS_Appointment_DTL.nMSTAppointmentID = " + nMasterAppointmentID + " AND  AS_Appointment_DTL.nClinicID = " + _ClinicID + "  ";
                    oDB.ExecuteScalar_Query(_sqlQuery);
                }
                oDB.Disconnect();

                //Template Appointment updating nIsRegisted = 0 if nIsRegisted = 2 
                ApptDeleteOverlapTemplate(nMasterAppointmentID, nDetailAppointmentID);
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
                oDB.Dispose();
            }
        }


        private void ApptDeleteOverlapTemplate(long nMSTAppointmentID, long nDTLAppointmentID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            oDB.Connect(false);

            oDBParameters.Add("@nMSTAppointmentID", nMSTAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@nDTLAppointmentID", nDTLAppointmentID, ParameterDirection.Input, SqlDbType.BigInt);

            oDB.Execute("ApptDeleteOverlapTemplate", oDBParameters);

            oDB.Disconnect();

            oDBParameters.Dispose();
            oDBParameters = null;

            oDB.Dispose();
            oDB = null;

        }

        internal void UpdateRecurrenceAppointmentUsedStatus(long nMasterAppointmentID, int _nAppointmentUsedStatusID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);

                string _sqlQuery = "UPDATE AS_Appointment_DTL SET nUsedStatus = " + _nAppointmentUsedStatusID + " " +
                " WHERE nMSTAppointmentID = " + nMasterAppointmentID + " AND  nClinicID = " + _ClinicID + " ";

                oDB.Execute_Query(_sqlQuery);

                DataTable dtDetailAppointments = new DataTable();
                _sqlQuery = " SELECT nDTLAppointmentID from AS_Appointment_DTL  WITH(NOLOCK) "
                          + " where nMSTAppointmentID = 1 AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + "";
                oDB.Retrive_Query(_sqlQuery, out dtDetailAppointments);

                if (dtDetailAppointments != null)
                {
                    for (int i = 0; i < dtDetailAppointments.Rows.Count; i++)
                    {
                        UpdateAppointmentStatus(nMasterAppointmentID, Convert.ToInt64(dtDetailAppointments.Rows[i]["nDTLAppointmentID"]), _nAppointmentUsedStatusID);
                    }
                }

                //UpdateAppointmentStatus(nMasterAppointmentID, nDetailAppointmentID, _nAppointmentUsedStatusID);

                oDB.Disconnect();
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
                oDB.Dispose();
            }
        }

        //function's access specifire changed by dipak 20100102
        //internal bool IsMaximumAppointmentRegisterd(DateTime dtAppDate, DateTime dtAppTime)
        //make IsMaximumAppointmentRegisterd public 
        public bool IsMaximumAppointmentRegisterd(DateTime dtAppDate, DateTime dtAppTime, Int64 ASBaseId)
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
            Int32 _MaxAppointmentsInSlot = 0;
            object value = new object();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable oData = new DataTable();
            bool _result = false;
            try
            {
                ogloSettings.GetSetting("MaxAppointmentsInSlot", out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    _MaxAppointmentsInSlot = Convert.ToInt32(value);
                    value = null;
                }
                else
                {
                    return false;
                }

                oDB.Connect(false);
                string _strSQL = "";

                _strSQL = "SELECT COUNT(nDTLAppointmentID)  FROM AS_Appointment_DTL  WITH(NOLOCK)  "
                + " WHERE nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + "  AND nASBaseID= " + ASBaseId + "AND AS_Appointment_DTL.nUsedStatus <> 6 AND AS_Appointment_DTL.nUsedStatus <> 7 AND AS_Appointment_DTL.nUsedStatus <> 5 AND nClinicID = " + _ClinicID + " "
                + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(dtAppDate.ToShortDateString()) + " "
                + " AND (dtStartTime >= " + gloDateMaster.gloTime.TimeAsNumber(dtAppTime.Hour + ":00") + " AND dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(dtAppTime.Hour + ":59") + ")";


                object ResultCount = oDB.ExecuteScalar_Query(_strSQL);

                if (ResultCount != null && Convert.ToString(ResultCount) != "")
                {
                    if (_MaxAppointmentsInSlot != 0 && Convert.ToInt32(ResultCount) >= _MaxAppointmentsInSlot)
                    {
                        _result = true;
                    }
                }
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
                oDB.Dispose();
                if (ogloSettings != null)
                {
                    ogloSettings.Dispose();
                    ogloSettings = null;
                }
            }

            return _result;
        }
        //SHUBHANGI 20100715 RESTRICT TO ADD NEW APPOINTMENT IN THE ALREADY ALLOCATED TIME SPAN
        public bool IsAppointmentRegisterd(DateTime dtAppDate, DateTime dtAppTime, Int64 providerId, Int64 appointmentID)
        {
           // gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
           // object value = new object();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    DataTable oData = new DataTable();
            bool _result = false;
            try
            {
                oDB.Connect(false);
               // string _strSQL = "";



                //_strSQL = "SELECT COUNT(nDTLAppointmentID)  FROM AS_Appointment_DTL  WHERE  AS_Appointment_DTL.nUsedStatus <> 6 AND AS_Appointment_DTL.nUsedStatus <> 7 "
                //       + "AND AS_Appointment_DTL.nUsedStatus <> 5 AND nClinicID = " + _ClinicID + "  AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(dtAppDate.ToShortDateString()) + "  "
                //       + "AND dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(dtAppDate.TimeOfDay.ToString()) + " AND dtEndTime >=" + gloDateMaster.gloTime.TimeAsNumber(dtAppTime.TimeOfDay.ToString()) + " AND nAsBaseID ='" + providerId + "' AND nMSTAppointmentID != " + appointmentID + "";

                //object ResultCount = oDB.ExecuteScalar_Query(_strSQL);

                //if (Convert.ToInt64(ResultCount) == 0)
                //{

                //_strSQL = "SELECT COUNT(nDTLAppointmentID)  FROM AS_Appointment_DTL  WHERE  AS_Appointment_DTL.nUsedStatus <> 6 AND AS_Appointment_DTL.nUsedStatus <> 7 "
                //                      + "AND AS_Appointment_DTL.nUsedStatus <> 5 AND nClinicID = " + _ClinicID + "  AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(dtAppDate.ToShortDateString()) + "  "
                //                      + "AND dtEndTime between " + gloDateMaster.gloTime.TimeAsNumber(dtAppDate.TimeOfDay.ToString()) + " AND " + gloDateMaster.gloTime.TimeAsNumber(dtAppTime.TimeOfDay.ToString()) + " AND dtEndTime != " + gloDateMaster.gloTime.TimeAsNumber(dtAppDate.TimeOfDay.ToString()) + "  AND nAsBaseID ='" + providerId + "' AND nMSTAppointmentID != " + appointmentID + "";

                //object ResultCount = oDB.ExecuteScalar_Query(_strSQL);
                //}

                //if (Convert.ToInt64(ResultCount) == 0)
                //{

                //    _strSQL = "SELECT COUNT(nDTLAppointmentID)  FROM AS_Appointment_DTL  WHERE  AS_Appointment_DTL.nUsedStatus <> 6 AND AS_Appointment_DTL.nUsedStatus <> 7 "
                //                          + "AND AS_Appointment_DTL.nUsedStatus <> 5 AND nClinicID = " + _ClinicID + "  AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(dtAppDate.ToShortDateString()) + "  "
                //                          + "AND dtStartTime between " + gloDateMaster.gloTime.TimeAsNumber(dtAppDate.TimeOfDay.ToString()) + " AND " + gloDateMaster.gloTime.TimeAsNumber(dtAppTime.TimeOfDay.ToString()) + " AND dtStartTime != " + gloDateMaster.gloTime.TimeAsNumber(dtAppTime.TimeOfDay.ToString()) + "  AND nAsBaseID ='" + providerId + "' AND nMSTAppointmentID != " + appointmentID + "";

                //    ResultCount = oDB.ExecuteScalar_Query(_strSQL);
                //}
                //if (Convert.ToInt64(ResultCount) == 0)
                //{

                object ResultCount = GetAppointmentConflictTime(appointmentID, providerId, gloDateMaster.gloDate.DateAsNumber(dtAppDate.ToShortDateString()), gloDateMaster.gloDate.DateAsNumber(dtAppTime.ToShortDateString()), gloDateMaster.gloTime.TimeAsNumber(dtAppDate.TimeOfDay.ToString()), gloDateMaster.gloTime.TimeAsNumber(dtAppTime.TimeOfDay.ToString()), _ClinicID);

                //}

                if (ResultCount != null && Convert.ToString(ResultCount) != "")
                {
                    if (Convert.ToInt64(ResultCount) > 0)
                    {
                        _result = true;
                    }
                }
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
                oDB.Dispose();
            }

            return _result;
        }


        public object GetAppointmentConflictTime(Int64 appointmentID, Int64 providerId, Int32 dtStartDate, Int32 dtEndDate, Int32 dtStartTime, Int32 dtEndTime, Int64 ClinicID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object Count;
            Count = 0;

            oDB.Connect(false);

            oDBParameters.Add("@appointmentID", appointmentID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@providerId ", providerId, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@dtEndDate", dtEndDate, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@StartTime", dtStartTime, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@EndTime", dtEndTime, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@Count", 0, ParameterDirection.InputOutput, SqlDbType.Int);

            oDB.Execute("GetAppointmentConflictTime", oDBParameters, out Count);

            oDB.Disconnect();

            oDBParameters.Dispose();
            oDBParameters = null;

            oDB.Dispose();
            oDB = null;


            return Count;
        }

        //END
        #region "Appointmnet GUI Methods"
        public void ShowAppointmentView(bool FixedDialog, System.Windows.Forms.Form oParentWindow)
        {
            oViewAppointment = frmViewAppointment.GetInstance(_databaseconnectionstring, _ClinicID);
             oViewAppointment.gstrSQLServerName=gstrSQLServerName;
            oViewAppointment.gstrDatabaseName=gstrDatabaseName;
            oViewAppointment.gblnSQLAuthentication=gblnSQLAuthentication;
            oViewAppointment.gstrSQLUser=gstrSQLUser;
            oViewAppointment.gstrSQLPassword=gstrSQLPassword;
            oViewAppointment.gstrCaption = gstrCaption;
            oViewAppointment.CalendarView_Closed += new frmViewAppointment.CalendarViewClosed(oViewAppointment_CalendarView_Closed);
            
            oViewAppointment.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            if (FixedDialog == false)
            {
                oViewAppointment.MdiParent = oParentWindow;
                oViewAppointment.Show();
            }
            else
            {
                oViewAppointment.ShowDialog(oViewAppointment.Parent);
                oViewAppointment.CalendarView_Closed -= new frmViewAppointment.CalendarViewClosed(oViewAppointment_CalendarView_Closed);
 
                oViewAppointment.Dispose();
                oViewAppointment = null;
            }
        }

       
        void oViewAppointment_CalendarView_Closed(object sender, EventArgs e)
        {
            try
            {
                Calendar_Closed(sender, e);
                oViewAppointment.CalendarView_Closed -= new frmViewAppointment.CalendarViewClosed(oViewAppointment_CalendarView_Closed);
                oViewAppointment.Dispose();
                oViewAppointment = null;

            }
            catch (Exception)
            {
            }
        }

        public void ShowAppointment()
        {
            frmSetupAppointment oSetupAppointment = new frmSetupAppointment(_databaseconnectionstring);
            oSetupAppointment.ShowDialog(oSetupAppointment.Parent);
            oSetupAppointment.Dispose();
            oSetupAppointment = null;
        }

        public void ShowAppointment(Int64 PatientId, Form objParent)
        {
            frmSetupAppointment oSetupAppointment = new frmSetupAppointment(_databaseconnectionstring);
            if (PatientId > 0)
            {
                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                DataTable dtProvider =  ogloPatient.GetPatientProvider(PatientId, _ClinicID);
                ogloPatient.Dispose();
                ogloPatient = null;
                oSetupAppointment.SetAppointmentParameters.MasterAppointmentID = 0;
                oSetupAppointment.SetAppointmentParameters.AppointmentID = 0;
                oSetupAppointment.SetAppointmentParameters.ClinicID = this._ClinicID;
                oSetupAppointment.SetAppointmentParameters.PatientID = PatientId;



                if (dtProvider != null && dtProvider.Rows.Count > 0)
                {
                    if (dtProvider.Rows[0]["nProviderID"] != DBNull.Value)
                    {
                        oSetupAppointment.SetAppointmentParameters.ProviderID = Convert.ToInt64(dtProvider.Rows[0]["nProviderID"]);
                    }
                    else
                    {
                        oSetupAppointment.SetAppointmentParameters.ProviderID = 0;
                    }
                    if (dtProvider.Rows[0]["ProviderName"] != DBNull.Value)
                    {
                        oSetupAppointment.SetAppointmentParameters.ProviderName = Convert.ToString(dtProvider.Rows[0]["ProviderName"]);
                    }
                    else
                    {
                        oSetupAppointment.SetAppointmentParameters.ProviderName = "";
                    }
                }

                oSetupAppointment.SetAppointmentParameters.AddTrue_ModifyFalse_Flag = true; // Add - true, Modify - false
                oSetupAppointment.SetAppointmentParameters.ModifyAppointmentMethod = SingleRecurrence.Single;
                oSetupAppointment.SetAppointmentParameters.ModifyMasterAppointmentMethod = SingleRecurrence.Single;
                oSetupAppointment.SetAppointmentParameters.ModifySingleAppointmentFromReccurence = false;
                oSetupAppointment.SetAppointmentParameters.LoadParameters = true;

            }
            oSetupAppointment.ShowDialog(objParent);
            oSetupAppointment.Dispose();
            oSetupAppointment = null;
        }
        #endregion

        #region "Private Methods"
        private string GetProviderName(Int64 ProviderID, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _intresult = new object();
            string _result = "";
            string _strSQL = "";
            try
            {
                _result = "";
                oDB.Connect(false);
                //Modified by Mayuri:20100519-Case No:#0004942
                _strSQL = "SELECT (ISNULL(sFirstName,'') + space(1) + CASE sMiddleName   WHEN  '' THEN '' When sMiddleName then   sMiddleName + SPACE(1) END  + ISNULL(sLastName,'')) AS ProviderName " +
                " FROM Provider_MST  WITH(NOLOCK) WHERE nProviderID  = " + ProviderID + " AND nClinicID = " + ClinicID + "";

                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = _intresult.ToString();
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        private string GetPatientName(Int64 PatientID, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _intresult = new object();
            string _result = "";
            string _strSQL = "";
            try
            {
                _result = "";
                oDB.Connect(false);

                _strSQL = "SELECT (ISNULL(sFirstName,'') + space(1) + ISNULL(sMiddleName,'') +  space(1) + ISNULL(sLastName,'')) AS PatientName " +
                " FROM Patient  WITH(NOLOCK) WHERE nPatientID  = " + PatientID + " AND nClinicID = " + ClinicID + "";

                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = _intresult.ToString();
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        private Int64 GetAppointmentTypeID(Int64 AppointmentTypeID, string AppointmentTypeDescription, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _intresult = new object();
            Int64 _result = 0;
            string _strSQL = "";
            try
            {
                _result = 0;
                oDB.Connect(false);
                _strSQL = "SELECT nAppointmentTypeID FROM AB_AppointmentType  WITH(NOLOCK) WHERE sAppointmentType = '" + AppointmentTypeDescription.Trim().Replace("'", "''") + "' AND nAppointmentTypeID = " + AppointmentTypeID + " AND nAppProcType = 1 AND nClinicID = " + ClinicID + "";
                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = Convert.ToInt64(_intresult.ToString());
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        private Int64 GetLocationID(string LocationName, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _intresult = new object();
            Int64 _result = 0;
            string _strSQL = "";
            try
            {
                _result = 0;
                oDB.Connect(false);
                _strSQL = "SELECT nLocationID  FROM AB_Location  WITH(NOLOCK) WHERE sLocation = '" + LocationName.Trim().Replace("'", "''") + "' AND bIsBlocked = 0 AND nClinicID = " + ClinicID + "";
                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = Convert.ToInt64(_intresult.ToString());
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        private Int64 GetDepartmentID(string DepartmentName, Int64 LocationID, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _intresult = new object();
            Int64 _result = 0;
            string _strSQL = "";
            try
            {
                _result = 0;
                oDB.Connect(false);
                _strSQL = "SELECT nDepartmentID  FROM AB_Department  WITH(NOLOCK) WHERE sDepartment = '" + DepartmentName.Trim().Replace("'", "''") + "' AND bIsBlocked = 0 AND nLocationID = " + LocationID + " AND nClinicID = " + ClinicID + "";
                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = Convert.ToInt64(_intresult.ToString());
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        private Int64 GetProblemTypeID(string ProblemTypeCode, string ProblemTypeName, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _intresult = new object();
            Int64 _result = 0;
            string _strSQL = "";
            try
            {
                _result = 0;
                oDB.Connect(false);
                _strSQL = "SELECT nAppointmentTypeID  FROM AB_AppointmentType  WITH(NOLOCK) WHERE sAppointmentType = '" + ProblemTypeName.Trim().Replace("'", "''") + "' AND bIsBlocked = 0 AND nAppProcType = 2 AND nClinicID = " + ClinicID + "";
                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = Convert.ToInt64(_intresult.ToString());
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        private Int64 GetResourceID(string ResourceCode, string ResourceName, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _intresult = new object();
            Int64 _result = 0;
            string _strSQL = "";
            try
            {
                _result = 0;
                oDB.Connect(false);
                _strSQL = "SELECT nResourceID  FROM AB_Resource_MST  WITH(NOLOCK) WHERE sCode = '" + ResourceCode.Trim().Replace("'", "''") + "' AND sDescription = '" + ResourceName.Trim().Replace("'", "''") + "' AND bitIsBlocked = 0 AND nClinicID = " + ClinicID + "";
                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = Convert.ToInt64(_intresult.ToString());
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }
        #endregion


        public bool IsResourceOnlyAppointment(Int64 MSTAppointmentID)
        {

            string _sqlstr = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _result = false;

            try
            {

                _sqlstr = "SELECT  nASBaseFlag FROM AS_Appointment_MST  WITH(NOLOCK) WHERE  nMSTAppointmentID ='" + MSTAppointmentID + "' ";


                oDB.Connect(false);
                object ResultCount = oDB.ExecuteScalar_Query(_sqlstr);

                if (ResultCount != null && Convert.ToString(ResultCount) != "")
                {
                    if (ASBaseType.Resource == (ASBaseType)Convert.ToInt32(ResultCount))
                    {
                        _result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null && Convert.ToString(oDB) != "")
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }
            return _result;
        }




        // By Pranit on 16 feb 2011
        public bool IsProviderResourceAppointment(Int64 MSTAppointmentID)
        {

            string _sqlstr = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _result = false;

            try
            {

                //_sqlstr = "SELECT  nASBaseFlag FROM AS_Appointment_MST WHERE  nMSTAppointmentID ='" + MSTAppointmentID + "' ";

                _sqlstr = "SELECT convert(varchar,nASBaseFlag)+','  FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE  nMSTAppointmentID='" + MSTAppointmentID + "' for XML PATH('')";


                oDB.Connect(false);
                object ResultCount = oDB.ExecuteScalar_Query(_sqlstr);

                if (ResultCount != null && Convert.ToString(ResultCount) != "")
                {
                    if (ResultCount.ToString().Contains("1") && ResultCount.ToString().Contains("3"))
                    {
                        _result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null && Convert.ToString(oDB) != "")
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }
            return _result;
        }






        public bool IsMultiResourceAppointment(Int64 MSTAppointmentID)
        {

            string _sqlstr = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _result = false;

            try
            {
                _sqlstr = " SELECT  COUNT(DISTINCT AS_Appointment_DTL.nASBaseID) "
                + " FROM AS_Appointment_MST  WITH(NOLOCK) INNER JOIN AS_Appointment_DTL  WITH(NOLOCK) "
                + " ON AS_Appointment_MST.nMSTAppointmentID = AS_Appointment_DTL.nMSTAppointmentID "
                + " WHERE AS_Appointment_MST.nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND AS_Appointment_DTL.nASBaseID <> 0 AND AS_Appointment_DTL.nMSTAppointmentID = " + MSTAppointmentID + " ";

                oDB.Connect(false);
                object ResultCount = oDB.ExecuteScalar_Query(_sqlstr);

                if (ResultCount != null && Convert.ToString(ResultCount) != "")
                {
                    if (Convert.ToInt32(ResultCount) > 1)
                    {
                        _result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null && Convert.ToString(oDB) != "")
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }
            return _result;
        }

        public DataTable GetAppointmentTypeDetails(Int64 mstAppID, Int64 dtlAppID)
        {

            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                oDBParameters.Add("@MstAppID", mstAppID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@DtlAppID", dtlAppID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);

                DataTable dt = new DataTable();

                oDB.Retrive("GetAppointmentTypeDetailsUsingMasAppIDAndDetAppID", oDBParameters, out dt);

                oDB.Disconnect();
                oDB = null;
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }

        }

        public DataTable GetAppointmentTypeDetailsUsingAllocationID(Int64 mstAllID, Int64 allID)
        {

            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                oDBParameters.Add("@MstAllID", mstAllID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@DtlAllID", allID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);

                DataTable dt = new DataTable();

                oDB.Retrive("GetAppointmentTypeDetailsUsingTempAllMasIDAndAllID", oDBParameters, out dt);

                oDB.Disconnect();
                oDB = null;
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }

        }


        // Added By Pranit on 18 jan 2012
        public object GetAppointmentConflictTimeOverlapSet(Int64 nTemplateAllocationID, Int64 providerId, Int32 dtStartDate, Int32 dtStartTime, Int32 dtEndTime, Int64 ClinicID, DateTime Startdate, DateTime StartTime, DateTime EndTime, String LocationIDs = null)
        {
            if (EndTime < StartTime)
            {
                EndTime = EndTime.AddDays(1);
            }

            TimeSpan ts = EndTime - StartTime;
            double duration = ts.TotalMinutes;

            DateTime dateTime = new DateTime();
            string splitDate = Startdate.ToShortDateString() + " " + StartTime.ToShortTimeString();
            dateTime = Convert.ToDateTime(splitDate);

            DateTime newDateTime = new DateTime();
            newDateTime = dateTime.AddMinutes((int)duration);


            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object Count;
            Count = 0;

            oDB.Connect(false);

            oDBParameters.Add("@nTemplateAllocationID", nTemplateAllocationID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@providerId", providerId, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@dtEndDate", Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(newDateTime.ToString())), ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@StartTime", dtStartTime, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@EndTime", dtEndTime, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@LocationIDs", LocationIDs, ParameterDirection.Input, SqlDbType.Text);
            oDBParameters.Add("@Count", 0, ParameterDirection.InputOutput, SqlDbType.Int);

            oDB.Execute("GetAppointmentConflictTimeOverlapSet", oDBParameters, out Count);

            oDB.Disconnect();

            oDBParameters.Dispose();
            oDBParameters = null;

            oDB.Dispose();
            oDB = null;


            return Count;
        }

        public void ApptUpdateOverlapTemplate(Int64 nTemplateAllocationID, Int64 providerId, Int32 dtStartDate, Int32 dtStartTime, Int32 dtEndTime, DateTime Startdate, DateTime StartTime, DateTime EndTime, Int64 ClinicID)
        {

            if (EndTime < StartTime)
            {
                EndTime = EndTime.AddDays(1);
            }

            TimeSpan ts = EndTime - StartTime;
            double duration = ts.TotalMinutes;

            DateTime dateTime = new DateTime();
            string splitDate = Startdate.ToShortDateString() + " " + StartTime.ToShortTimeString();
            dateTime = Convert.ToDateTime(splitDate);

            DateTime newDateTime = new DateTime();
            newDateTime = dateTime.AddMinutes((int)duration);



            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            oDB.Connect(false);

            oDBParameters.Add("@nTemplateAllocationID", nTemplateAllocationID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@providerId", providerId, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@StartTime", dtStartTime, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@EndTime", dtEndTime, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@dtStartDate", dtStartDate, ParameterDirection.Input, SqlDbType.BigInt);
            oDBParameters.Add("@dtEndDate", Convert.ToInt32(gloDateMaster.gloDate.DateAsNumber(newDateTime.ToString())), ParameterDirection.Input, SqlDbType.BigInt);

            oDB.Execute("ApptUpdateOverlapTemplate", oDBParameters);

            oDB.Disconnect();

            oDBParameters.Dispose();
            oDBParameters = null;

            oDB.Dispose();
            oDB = null;

        }
        // End By Pranit on 18 jan 2012


    }


    public class gloSchedule
    {

        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        private Int64 _ClinicID = 0;
        private string _MessageBoxCaption = String.Empty;
        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


        public gloSchedule(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }


            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

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

        ~gloSchedule()
        {
            Dispose(false);
        }

        #endregion

        public Int64 Add(MasterSchedule oMasterSchedule, Int64 DetailScheduleID)
        {
            Int64 _nMasterScheduleID = 0;
            Int64 _nDetailScheduleID = 0;
            Int64 _TempLineNumber = 0;
            Object objectID;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            gloAppointmentScheduling.Criteria.FindRecurrences _ScheduleDates = new gloAppointmentScheduling.Criteria.FindRecurrences();
            try
            {
                oDB.Connect(false);

                _ScheduleDates = _ScheduleDates.GetRecurrence(oMasterSchedule.Criteria, oMasterSchedule.StartDate, oMasterSchedule.EndDate);
                if (_ScheduleDates.Dates.Count > 0)
                {
                    if (oMasterSchedule.ASFlag != AppointmentScheduleFlag.BlockedSchedule)
                    {
                        if (oMasterSchedule.Criteria.SingleRecurrenceAppointment != SingleRecurrence.SingleInRecurrence)
                        {
                            #region "Master Schedule"

                            oDBParameters.Add("@nMSTScheduleID", oMasterSchedule.MasterID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@nASBaseID", oMasterSchedule.ASBaseID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@sASBaseCode", oMasterSchedule.ASBaseCode, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@sASBaseDesc", oMasterSchedule.ASBaseDescription, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@nASBaseFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.StartDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nDuration", oMasterSchedule.Duration, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                            oDB.Execute("AS_INSERT_Schedule_MST", oDBParameters, out  objectID);

                            if (objectID == null)
                            { return 0; }

                            _nMasterScheduleID = (Int64)objectID;
                            #endregion

                            #region "Criteria "

                            //Delete All old Detail Records
                            oDB.Execute_Query("DELETE FROM AS_Schedule_MST_Criteria WHERE nMasterScheduleID = " + _nMasterScheduleID);

                            //Insert Into Criteria Table.   dbo.AS_Schedule_MST_Criteria
                            //nMasterScheduleID, bIsSingleRecurrence, nRecurrence_PatternType, nRecurrence_Pattern_Daily_EveryDayNo, 
                            if (oMasterSchedule.Criteria.SingleRecurrenceAppointment == SingleRecurrence.Recurrence)
                            {
                                oDBParameters.Clear();
                                oDBParameters = new gloDatabaseLayer.DBParameters();
                                oDBParameters.Add("@nMasterScheduleID", _nMasterScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.Criteria.SingleRecurrenceAppointment.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nRecurrence_PatternType", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType.GetHashCode(), System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nRecurrence_Pattern_Daily_EveryDayNo", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber, ParameterDirection.Input, SqlDbType.BigInt);

                                //nRecurrence_Pattern_Daily_EveryDayOrWeekDay, nRecurrence_Pattern_Weekly_EveryWeekNo, bRecurrence_Pattern_Weekly_Sunday, 
                                oDBParameters.Add("@nRecurrence_Pattern_Daily_EveryDayOrWeekDay", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nRecurrence_Pattern_Weekly_EveryWeekNo", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@bRecurrence_Pattern_Weekly_Sunday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday, ParameterDirection.Input, SqlDbType.Bit);

                                //bRecurrence_Pattern_Weekly_Monday, bRecurrence_Pattern_Weekly_Tuesday, bRecurrence_Pattern_Weekly_Wednesday, 
                                //bRecurrence_Pattern_Weekly_Thursday, bRecurrence_Pattern_Weekly_Friday, bRecurrence_Pattern_Weekly_Saturday, 
                                oDBParameters.Add("@bRecurrence_Pattern_Weekly_Monday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday, ParameterDirection.Input, SqlDbType.Bit);
                                oDBParameters.Add("@bRecurrence_Pattern_Weekly_Tuesday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday, ParameterDirection.Input, SqlDbType.Bit);
                                oDBParameters.Add("@bRecurrence_Pattern_Weekly_Wednesday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday, ParameterDirection.Input, SqlDbType.Bit);
                                oDBParameters.Add("@bRecurrence_Pattern_Weekly_Thursday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday, ParameterDirection.Input, SqlDbType.Bit);
                                oDBParameters.Add("@bRecurrence_Pattern_Weekly_Friday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday, ParameterDirection.Input, SqlDbType.Bit);
                                oDBParameters.Add("@bRecurrence_Pattern_Weekly_Saturday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday, ParameterDirection.Input, SqlDbType.Bit);

                                //nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria, nRecurrence_Pattern_Monthly_DayNumber, nRecurrence_Pattern_Monthly_EveryMonthNumber, 
                                //nRecurrence_Pattern_Monthly_FstLstCriteriaID, nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID, nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria,
                                oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayNumber", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRecurrence_Pattern_Monthly_EveryMonthNumber", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                                oDBParameters.Add("@nRecurrence_Pattern_Monthly_FstLstCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                                //nRecurrence_Pattern_Yearly_DayNumber, nRecurrence_Pattern_Yearly_MonthOfCriteriaID, nRecurrence_Pattern_Yearly_FstLstCriteriaID,
                                //nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID, nClinicID
                                oDBParameters.Add("@nRecurrence_Pattern_Yearly_DayNumber", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRecurrence_Pattern_Yearly_MonthOfCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRecurrence_Pattern_Yearly_FstLstCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                                oDBParameters.Add("@bRange_Flag", oMasterSchedule.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRange_StartDate", oMasterSchedule.Criteria.RecurrenceCriteria.Range.StartDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRange_StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.StartTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRange_EndDate", oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRange_EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRange_Duration", Convert.ToInt32(((TimeSpan)oMasterSchedule.EndTime.Subtract(oMasterSchedule.StartTime)).TotalMinutes), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRange_NoOfOccurence", oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                oDBParameters.Add("@nRange_NoEndDateYear", oMasterSchedule.Criteria.RecurrenceCriteria.Range.NoEndDateYear, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                                oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                                oDB.Execute("AS_INSERT_Schedule_MST_Criteria", oDBParameters);
                            }
                            #endregion "Criteria "

                            //Delete All old Detail Records
                            oDB.Execute_Query("DELETE FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + _nMasterScheduleID);
                        }
                        else
                        {
                            // oMasterSchedule.IsRecurrence = SingleRecurrence.SingleInRecurrence;
                            _nMasterScheduleID = oMasterSchedule.MasterID;
                            _nDetailScheduleID = DetailScheduleID;
                            // oDB.Execute_Query("DELETE FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + _nMasterScheduleID + " and ndtlscheduleid=" + _nDetailScheduleID);

                            //===============By Pranit=========================================

                            Int64 _OldProviderID = 0;
                            object _sqlresult;

                            #region "Read Old Provider ID"

                            _sqlresult = new object();
                            string str = "SELECT nASBaseID FROM AS_Schedule_MST  WITH(NOLOCK) WHERE nMSTScheduleID=" + _nMasterScheduleID;
                            _sqlresult = oDB.ExecuteScalar_Query(str);

                            if (_sqlresult != null)
                            {
                                if (_sqlresult.ToString() != null)
                                {
                                    if (_sqlresult.ToString() != "")
                                    {
                                        _OldProviderID = Convert.ToInt64(_sqlresult.ToString());
                                    }
                                }
                            }
                            _sqlresult = null;

                            #endregion


                            //if (ModifyMasterMethod == SingleRecurrence.Recurrence)
                            //{
                            // oMasterSchedule.Criteria.SingleRecurrenceAppointment = SingleRecurrence.Single;

                            bool _isProviderChanged = false;

                            if (_OldProviderID != oMasterSchedule.ASBaseID)
                            {
                                _isProviderChanged = true;
                            }
                            else
                            {
                                _isProviderChanged = false;
                            }


                            if (_isProviderChanged)
                            {
                                //***Delete and create new simple schedule instance***
                                #region "Delete Schedule from recurrence"
                                bool _NoSchedule = true;
                                DataTable _DeleteScheduleList = new DataTable();

                                #region "Delete Provider Schedule then Problem Type and Resources Schedule base on Provider Schedule"

                                Int64 _DelSchID = 0;
                                _DelSchID = DetailScheduleID;
                                string _strSQL;

                                //AS_Schedule_DTL - Provider
                                _strSQL = "DELETE FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + _nMasterScheduleID + " AND nDTLScheduleID = " + _DelSchID + " ";
                                oDB.Execute_Query(_strSQL);


                                #endregion


                                #region "Check Is there any schedule remaing - Start"
                                if (_DeleteScheduleList != null) { _DeleteScheduleList.Dispose(); }
                                _DeleteScheduleList = new DataTable();

                                _strSQL = "SELECT COUNT(nDTLScheduleID) FROM AS_Schedule_DTL  WITH(NOLOCK) WHERE " +
                                    " nMSTScheduleID = " + _nMasterScheduleID + " ";

                                oDB.Retrive_Query(_strSQL, out _DeleteScheduleList);

                                if (_DeleteScheduleList != null)
                                {
                                    if (_DeleteScheduleList.Rows.Count > 0)
                                    {
                                        _NoSchedule = false;
                                    }
                                }

                                if (_DeleteScheduleList != null) { _DeleteScheduleList.Dispose(); }
                                #endregion //Check Is there any schedule remaing - Finish

                                if (_NoSchedule == true)
                                {
                                    #region "If no schedule then delete master records also"

                                    //select *,nMasterScheduleID  from  AS_Schedule_MST_Criteria

                                    //AS_Schedule_MST
                                    _strSQL = "DELETE FROM AS_Schedule_MST WHERE nMSTScheduleID = " + _nMasterScheduleID + " ";
                                    oDB.Execute_Query(_strSQL);
                                    // AS_Schedule_MST_Criteria
                                    _strSQL = "DELETE FROM AS_Schedule_MST_Criteria WHERE nMasterScheduleID = " + _nMasterScheduleID + " ";
                                    oDB.Execute_Query(_strSQL);

                                    #endregion
                                }
                                #endregion

                                #region "Create New Simple Appointment"
                                oMasterSchedule.IsRecurrence = SingleRecurrence.Single;

                                // Add Master Entry

                                #region "Master Schedule"

                                oDBParameters.Add("@nMSTScheduleID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nASBaseID", oMasterSchedule.ASBaseID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sASBaseCode", oMasterSchedule.ASBaseCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@sASBaseDesc", oMasterSchedule.ASBaseDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nASBaseFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.StartDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nDuration", oMasterSchedule.Duration, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                                oDB.Execute("AS_INSERT_Schedule_MST", oDBParameters, out  objectID);

                                if (objectID == null)
                                { return 0; }

                                _nMasterScheduleID = (Int64)objectID;
                                #endregion



                                #endregion

                            }

                            else
                            {
                                oMasterSchedule.IsRecurrence = SingleRecurrence.SingleInRecurrence;
                                //_nMasterScheduleID = oMasterSchedule.MasterID;
                                //_nDetailScheduleID = DetailScheduleID;
                                object _referenceID = new object();
                                _referenceID = oDB.ExecuteScalar_Query("SELECT ISNULL(nrefID,0) FROM AS_Schedule_DTL  WITH(NOLOCK) WHERE nMSTScheduleID = " + _nMasterScheduleID + " and ndtlscheduleid=" + _nDetailScheduleID);
                                if (_referenceID != null)
                                {
                                    if (_referenceID.ToString() != "")
                                    {
                                        oDB.Execute_Query("DELETE FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + _nMasterScheduleID + " and ndtlscheduleid=" + Convert.ToInt64(_referenceID.ToString()));
                                    }
                                }
                                _referenceID = null;

                                oDB.Execute_Query("DELETE FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + _nMasterScheduleID + " and (ndtlscheduleid=" + _nDetailScheduleID + " or nrefID=" + _nDetailScheduleID + ")");
                            }



                            //=========End Pranit================================================

                        }



                        #region Schedule Details

                        for (int i = 0; i < _ScheduleDates.Dates.Count; i++)
                        {
                            //nMSTScheduleID, nDTLScheduleID, bIsSingleRecurrence, nLineNumber, nScheduleFlag, nASBaseID, sASBaseCode, sASBaseDesc, nASBaseFlag, 
                            //nRefID, nRefFlag, dtStartDate, dtStartTime, dtEndDate, dtEndTime, nLocationID, sLocationName, nDepartmentID, sDepartmentName,
                            //nColorCode, sNotes, nClinicID

                            object _lineresult = new object();
                            _lineresult = oDB.ExecuteScalar_Query("SELECT ISNULL(MAX(nLineNumber),0) + 1 FROM AS_Schedule_DTL  WITH(NOLOCK)  WHERE nASBaseID = " + oMasterSchedule.ASBaseID + " AND nASBaseFlag  = " + oMasterSchedule.ASBaseFlag.GetHashCode() + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()) + " AND nClinicID = " + oMasterSchedule.ClinicID + "");
                            if (_lineresult != null)
                            {
                                if (_lineresult.ToString() != "")
                                {
                                    _TempLineNumber = Convert.ToInt64(_lineresult.ToString());
                                }
                            }
                            _lineresult = null;

                            if (oMasterSchedule.ASFlag == AppointmentScheduleFlag.ProviderSchedule)
                            {
                                oDBParameters.Clear();

                                oDBParameters.Add("@nMSTScheduleID", _nMasterScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nDTLScheduleID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nLineNumber", _TempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nASBaseID", oMasterSchedule.ASBaseID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sASBaseCode", oMasterSchedule.ASBaseCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@sASBaseDesc", oMasterSchedule.ASBaseDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nASBaseFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nRefID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nRefFlag", 0, ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                                if (oMasterSchedule.IsRecurrence == SingleRecurrence.Single)
                                {
                                    oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }
                                else
                                {

                                    System.DateTime dtendDate;
                                    decimal dDuration;
                                    dDuration = oMasterSchedule.Duration;
                                    dtendDate = (DateTime)_ScheduleDates.Dates[i];
                                    dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterSchedule.StartTime.ToShortTimeString());
                                    dtendDate = dtendDate.AddMinutes((double)dDuration);

                                    oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }

                                //oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                //oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nColorCode", oMasterSchedule.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sNotes", oMasterSchedule.Notes, ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                oDBParameters.Add("@nClinicID", oMasterSchedule.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                                objectID = new object();
                                oDB.Execute("AS_INSERT_Schedule_DTL", oDBParameters, out  objectID);

                                if (objectID == null)
                                { return 0; }

                                _nDetailScheduleID = (Int64)objectID;
                                objectID = null;
                            }

                            #region Problem Type

                            for (int k = 0; k < oMasterSchedule.ProblemTypes.Count; k++)
                            {
                                oDBParameters.Clear();

                                oDBParameters.Add("@nMSTScheduleID", _nMasterScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nDTLScheduleID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nLineNumber", _TempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nASBaseID", oMasterSchedule.ProblemTypes[k].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sASBaseCode", oMasterSchedule.ProblemTypes[k].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@sASBaseDesc", oMasterSchedule.ProblemTypes[k].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nASBaseFlag", oMasterSchedule.ProblemTypes[k].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nRefID", _nDetailScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nRefFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.ProblemTypes[k].StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                                if (oMasterSchedule.IsRecurrence == SingleRecurrence.Single)
                                {
                                    oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.ProblemTypes[k].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }
                                else
                                {

                                    System.DateTime dtendDate;
                                    decimal dDuration;
                                    dDuration = oMasterSchedule.Duration;
                                    dtendDate = (DateTime)_ScheduleDates.Dates[i];
                                    dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterSchedule.ProblemTypes[k].StartTime.ToShortTimeString());
                                    dtendDate = dtendDate.AddMinutes((double)dDuration);

                                    oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.ProblemTypes[k].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }


                                //oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                //oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.ProblemTypes[k].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nColorCode", oMasterSchedule.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sNotes", oMasterSchedule.Notes, ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                oDBParameters.Add("@nClinicID", oMasterSchedule.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                                oDB.Execute("AS_INSERT_Schedule_DTL", oDBParameters, out  objectID);

                            }

                            #endregion

                            #region Resources

                            for (int k = 0; k < oMasterSchedule.Resources.Count; k++)
                            {
                                oDBParameters.Clear();

                                oDBParameters.Add("@nMSTScheduleID", _nMasterScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nDTLScheduleID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nLineNumber", _TempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nASBaseID", oMasterSchedule.Resources[k].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sASBaseCode", oMasterSchedule.Resources[k].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@sASBaseDesc", oMasterSchedule.Resources[k].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nASBaseFlag", oMasterSchedule.Resources[k].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nRefID", _nDetailScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nRefFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.Resources[k].StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                                if (oMasterSchedule.IsRecurrence == SingleRecurrence.Single)
                                {
                                    oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.Resources[k].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }
                                else
                                {

                                    System.DateTime dtendDate;
                                    decimal dDuration;
                                    dDuration = oMasterSchedule.Duration;
                                    dtendDate = (DateTime)_ScheduleDates.Dates[i];
                                    dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterSchedule.Resources[k].StartTime.ToShortTimeString());
                                    dtendDate = dtendDate.AddMinutes((double)dDuration);

                                    oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.Resources[k].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                }


                                //oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                //oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.Resources[k].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nColorCode", oMasterSchedule.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sNotes", oMasterSchedule.Notes, ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                oDBParameters.Add("@nClinicID", oMasterSchedule.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                                oDB.Execute("AS_INSERT_Schedule_DTL", oDBParameters, out  objectID);

                            }

                            #endregion

                            #region Users

                            for (int k = 0; k < oMasterSchedule.Users.Count; k++)
                            {
                                oDBParameters.Clear();

                                oDBParameters.Add("@nMSTScheduleID", _nMasterScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nDTLScheduleID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nLineNumber", _TempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nASBaseID", oMasterSchedule.Users[k].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sASBaseCode", oMasterSchedule.Users[k].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@sASBaseDesc", oMasterSchedule.Users[k].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nASBaseFlag", oMasterSchedule.Users[k].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nRefID", _nDetailScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nRefFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.Users[k].StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.Users[k].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nColorCode", oMasterSchedule.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sNotes", oMasterSchedule.Notes, ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                oDBParameters.Add("@nClinicID", oMasterSchedule.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                                oDB.Execute("AS_INSERT_Schedule_DTL", oDBParameters, out  objectID);

                            }

                            #endregion
                        }

                        #endregion
                    }
                    else
                    {

                        if (oMasterSchedule.Criteria.SingleRecurrenceAppointment != SingleRecurrence.SingleInRecurrence)
                        {

                            #region Resources
                            //GLO2012-0016240 Appointment block type by time
                            //and Issue reported as BUG ID 21216: Schedule >> Application delete block for multiple provider when we modify block.
                            //START Adding
                            if (oMasterSchedule.Resources.Count > 0 && oMasterSchedule.MasterID > 0)
                            {
                                oDB.Execute_Query("DELETE FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + oMasterSchedule.MasterID);
                                oDB.Execute_Query("DELETE FROM AS_Schedule_Mst WHERE nMSTScheduleID = " + oMasterSchedule.MasterID);
                                oMasterSchedule.MasterID = 0;
                            }
                            //END

                            for (int k = 0; k < oMasterSchedule.Resources.Count; k++)
                            {
                                #region "Master Schedule"
                                oDBParameters.Clear();
                                oDBParameters.Add("@nMSTScheduleID", oMasterSchedule.MasterID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nASBaseID", oMasterSchedule.ASBaseID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sASBaseCode", oMasterSchedule.ASBaseCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@sASBaseDesc", oMasterSchedule.ASBaseDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nASBaseFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.StartDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nDuration", oMasterSchedule.Duration, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                                oDB.Execute("AS_INSERT_Schedule_MST", oDBParameters, out  objectID);

                                if (objectID == null)
                                { return 0; }

                                _nMasterScheduleID = (Int64)objectID;
                                #endregion

                                #region "Criteria "

                                //Delete All old Detail Records
                                oDB.Execute_Query("DELETE FROM AS_Schedule_MST_Criteria WHERE nMasterScheduleID = " + _nMasterScheduleID);

                                //Insert Into Criteria Table.   dbo.AS_Schedule_MST_Criteria
                                //nMasterScheduleID, bIsSingleRecurrence, nRecurrence_PatternType, nRecurrence_Pattern_Daily_EveryDayNo, 
                                if (oMasterSchedule.Criteria.SingleRecurrenceAppointment == SingleRecurrence.Recurrence)
                                {
                                    oDBParameters.Clear();
                                    oDBParameters = new gloDatabaseLayer.DBParameters();
                                    oDBParameters.Add("@nMasterScheduleID", _nMasterScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.Criteria.SingleRecurrenceAppointment.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_PatternType", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType.GetHashCode(), System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Daily_EveryDayNo", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber, ParameterDirection.Input, SqlDbType.BigInt);

                                    //nRecurrence_Pattern_Daily_EveryDayOrWeekDay, nRecurrence_Pattern_Weekly_EveryWeekNo, bRecurrence_Pattern_Weekly_Sunday, 
                                    oDBParameters.Add("@nRecurrence_Pattern_Daily_EveryDayOrWeekDay", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Weekly_EveryWeekNo", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Sunday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday, ParameterDirection.Input, SqlDbType.Bit);

                                    //bRecurrence_Pattern_Weekly_Monday, bRecurrence_Pattern_Weekly_Tuesday, bRecurrence_Pattern_Weekly_Wednesday, 
                                    //bRecurrence_Pattern_Weekly_Thursday, bRecurrence_Pattern_Weekly_Friday, bRecurrence_Pattern_Weekly_Saturday, 
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Monday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday, ParameterDirection.Input, SqlDbType.Bit);
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Tuesday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday, ParameterDirection.Input, SqlDbType.Bit);
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Wednesday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday, ParameterDirection.Input, SqlDbType.Bit);
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Thursday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday, ParameterDirection.Input, SqlDbType.Bit);
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Friday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday, ParameterDirection.Input, SqlDbType.Bit);
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Saturday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday, ParameterDirection.Input, SqlDbType.Bit);

                                    //nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria, nRecurrence_Pattern_Monthly_DayNumber, nRecurrence_Pattern_Monthly_EveryMonthNumber, 
                                    //nRecurrence_Pattern_Monthly_FstLstCriteriaID, nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID, nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria,
                                    oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayNumber", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Monthly_EveryMonthNumber", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                                    oDBParameters.Add("@nRecurrence_Pattern_Monthly_FstLstCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                                    //nRecurrence_Pattern_Yearly_DayNumber, nRecurrence_Pattern_Yearly_MonthOfCriteriaID, nRecurrence_Pattern_Yearly_FstLstCriteriaID,
                                    //nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID, nClinicID
                                    oDBParameters.Add("@nRecurrence_Pattern_Yearly_DayNumber", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Yearly_MonthOfCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Yearly_FstLstCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                                    oDBParameters.Add("@bRange_Flag", oMasterSchedule.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_StartDate", oMasterSchedule.Criteria.RecurrenceCriteria.Range.StartDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.StartTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_EndDate", oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_Duration", Convert.ToInt32(((TimeSpan)oMasterSchedule.EndTime.Subtract(oMasterSchedule.StartTime)).TotalMinutes), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_NoOfOccurence", oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_NoEndDateYear", oMasterSchedule.Criteria.RecurrenceCriteria.Range.NoEndDateYear, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                                    oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                                    oDB.Execute("AS_INSERT_Schedule_MST_Criteria", oDBParameters);
                                }
                                #endregion "Criteria "

                                #region " Block Type Resource details "

                                // oDB.Execute_Query("DELETE FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + _nMasterScheduleID);

                                for (int i = 0; i < _ScheduleDates.Dates.Count; i++)
                                {
                                    object _lineresult = new object();
                                    _lineresult = oDB.ExecuteScalar_Query("SELECT ISNULL(MAX(nLineNumber),0) + 1 FROM AS_Schedule_DTL  WITH(NOLOCK) WHERE nASBaseID = " + oMasterSchedule.ASBaseID + " AND nASBaseFlag  = " + oMasterSchedule.ASBaseFlag.GetHashCode() + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()) + " AND nClinicID = " + oMasterSchedule.ClinicID + "");
                                    if (_lineresult != null)
                                    {
                                        if (_lineresult.ToString() != "")
                                        {
                                            _TempLineNumber = Convert.ToInt64(_lineresult.ToString());
                                        }
                                    }
                                    _lineresult = null;

                                    oDBParameters.Clear();

                                    oDBParameters.Add("@nMSTScheduleID", _nMasterScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nDTLScheduleID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                    oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    oDBParameters.Add("@nLineNumber", _TempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    oDBParameters.Add("@nASBaseID", oMasterSchedule.Resources[k].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@sASBaseCode", oMasterSchedule.Resources[k].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParameters.Add("@sASBaseDesc", oMasterSchedule.Resources[k].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParameters.Add("@nASBaseFlag", oMasterSchedule.Resources[k].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    oDBParameters.Add("@nRefID", _nDetailScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nRefFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.Resources[k].StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                                    if (oMasterSchedule.IsRecurrence == SingleRecurrence.Single)
                                    {
                                        oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                        oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    }
                                    else
                                    {

                                        System.DateTime dtendDate;
                                        decimal dDuration;
                                        dDuration = oMasterSchedule.Duration;
                                        dtendDate = (DateTime)_ScheduleDates.Dates[i];
                                        dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterSchedule.StartTime.ToShortTimeString());
                                        dtendDate = dtendDate.AddMinutes((double)dDuration);

                                        oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                                        //oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                        oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    }


                                    //  oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    //  oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.Resources[k].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParameters.Add("@nColorCode", oMasterSchedule.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@sNotes", oMasterSchedule.Notes, ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                    oDBParameters.Add("@nClinicID", oMasterSchedule.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                                    oDB.Execute("AS_INSERT_Schedule_DTL", oDBParameters, out  objectID);
                                }
                                #endregion
                            }

                            #endregion

                            #region Providers
                            //GLO2012-0016240 Appointment block type by time
                            //and Issue reported as BUG ID 21216: Schedule >> Application delete block for multiple provider when we modify block.
                            //START Adding
                            if (oMasterSchedule.Users.Count > 0 && oMasterSchedule.MasterID > 0)
                            {
                                oDB.Execute_Query("DELETE FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + oMasterSchedule.MasterID);
                                oDB.Execute_Query("DELETE FROM AS_Schedule_Mst WHERE nMSTScheduleID = " + oMasterSchedule.MasterID);
                                oMasterSchedule.MasterID = 0;
                            }
                            //END
                            for (int k = 0; k < oMasterSchedule.Users.Count; k++)
                            {
                                #region "Master Schedule"
                                oDBParameters.Clear();
                                oDBParameters.Add("@nMSTScheduleID", oMasterSchedule.MasterID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nASBaseID", oMasterSchedule.ASBaseID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sASBaseCode", oMasterSchedule.ASBaseCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@sASBaseDesc", oMasterSchedule.ASBaseDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nASBaseFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.StartDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nDuration", oMasterSchedule.Duration, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                                oDB.Execute("AS_INSERT_Schedule_MST", oDBParameters, out  objectID);

                                if (objectID == null)
                                { return 0; }

                                _nMasterScheduleID = (Int64)objectID;
                                #endregion

                                #region "Criteria "

                                //Delete All old Detail Records
                                oDB.Execute_Query("DELETE FROM AS_Schedule_MST_Criteria WHERE nMasterScheduleID = " + _nMasterScheduleID);

                                //Insert Into Criteria Table.   dbo.AS_Schedule_MST_Criteria
                                //nMasterScheduleID, bIsSingleRecurrence, nRecurrence_PatternType, nRecurrence_Pattern_Daily_EveryDayNo, 
                                if (oMasterSchedule.Criteria.SingleRecurrenceAppointment == SingleRecurrence.Recurrence)
                                {
                                    oDBParameters.Clear();
                                    oDBParameters = new gloDatabaseLayer.DBParameters();
                                    oDBParameters.Add("@nMasterScheduleID", _nMasterScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.Criteria.SingleRecurrenceAppointment.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_PatternType", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType.GetHashCode(), System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Daily_EveryDayNo", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber, ParameterDirection.Input, SqlDbType.BigInt);

                                    //nRecurrence_Pattern_Daily_EveryDayOrWeekDay, nRecurrence_Pattern_Weekly_EveryWeekNo, bRecurrence_Pattern_Weekly_Sunday, 
                                    oDBParameters.Add("@nRecurrence_Pattern_Daily_EveryDayOrWeekDay", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay.GetHashCode(), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Weekly_EveryWeekNo", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Sunday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday, ParameterDirection.Input, SqlDbType.Bit);

                                    //bRecurrence_Pattern_Weekly_Monday, bRecurrence_Pattern_Weekly_Tuesday, bRecurrence_Pattern_Weekly_Wednesday, 
                                    //bRecurrence_Pattern_Weekly_Thursday, bRecurrence_Pattern_Weekly_Friday, bRecurrence_Pattern_Weekly_Saturday, 
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Monday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday, ParameterDirection.Input, SqlDbType.Bit);
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Tuesday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday, ParameterDirection.Input, SqlDbType.Bit);
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Wednesday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday, ParameterDirection.Input, SqlDbType.Bit);
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Thursday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday, ParameterDirection.Input, SqlDbType.Bit);
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Friday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday, ParameterDirection.Input, SqlDbType.Bit);
                                    oDBParameters.Add("@bRecurrence_Pattern_Weekly_Saturday", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday, ParameterDirection.Input, SqlDbType.Bit);

                                    //nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria, nRecurrence_Pattern_Monthly_DayNumber, nRecurrence_Pattern_Monthly_EveryMonthNumber, 
                                    //nRecurrence_Pattern_Monthly_FstLstCriteriaID, nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID, nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria,
                                    oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayNumber", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Monthly_EveryMonthNumber", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                                    oDBParameters.Add("@nRecurrence_Pattern_Monthly_FstLstCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                                    //nRecurrence_Pattern_Yearly_DayNumber, nRecurrence_Pattern_Yearly_MonthOfCriteriaID, nRecurrence_Pattern_Yearly_FstLstCriteriaID,
                                    //nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID, nClinicID
                                    oDBParameters.Add("@nRecurrence_Pattern_Yearly_DayNumber", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Yearly_MonthOfCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Yearly_FstLstCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID", oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);


                                    oDBParameters.Add("@bRange_Flag", oMasterSchedule.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_StartDate", oMasterSchedule.Criteria.RecurrenceCriteria.Range.StartDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_StartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.StartTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_EndDate", oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_EndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToShortTimeString()), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_Duration", Convert.ToInt32(((TimeSpan)oMasterSchedule.EndTime.Subtract(oMasterSchedule.StartTime)).TotalMinutes), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_NoOfOccurence", oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@nRange_NoEndDateYear", oMasterSchedule.Criteria.RecurrenceCriteria.Range.NoEndDateYear, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                                    oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                                    oDB.Execute("AS_INSERT_Schedule_MST_Criteria", oDBParameters);
                                }
                                #endregion "Criteria "

                                #region " Block Type Provider details "


                                //oDB.Execute_Query("DELETE FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + _nMasterScheduleID);

                                for (int i = 0; i < _ScheduleDates.Dates.Count; i++)
                                {
                                    object _lineresult = new object();
                                    _lineresult = oDB.ExecuteScalar_Query("SELECT ISNULL(MAX(nLineNumber),0) + 1 FROM AS_Schedule_DTL  WITH(NOLOCK) WHERE nASBaseID = " + oMasterSchedule.ASBaseID + " AND nASBaseFlag  = " + oMasterSchedule.ASBaseFlag.GetHashCode() + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()) + " AND nClinicID = " + oMasterSchedule.ClinicID + "");
                                    if (_lineresult != null)
                                    {
                                        if (_lineresult.ToString() != "")
                                        {
                                            _TempLineNumber = Convert.ToInt64(_lineresult.ToString());
                                        }
                                    }
                                    _lineresult = null;
                                    oDBParameters.Clear();

                                    oDBParameters.Add("@nMSTScheduleID", _nMasterScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nDTLScheduleID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                    oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    oDBParameters.Add("@nLineNumber", _TempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    oDBParameters.Add("@nASBaseID", oMasterSchedule.Users[k].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@sASBaseCode", oMasterSchedule.Users[k].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParameters.Add("@sASBaseDesc", oMasterSchedule.Users[k].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParameters.Add("@nASBaseFlag", oMasterSchedule.Users[k].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    oDBParameters.Add("@nRefID", _nDetailScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nRefFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.Users[k].StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                                    if (oMasterSchedule.IsRecurrence == SingleRecurrence.Single)
                                    {
                                        oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                        oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    }
                                    else
                                    {

                                        System.DateTime dtendDate;
                                        decimal dDuration;
                                        dDuration = oMasterSchedule.Duration;
                                        dtendDate = (DateTime)_ScheduleDates.Dates[i];
                                        dtendDate = Convert.ToDateTime(dtendDate.ToShortDateString() + " " + oMasterSchedule.StartTime.ToShortTimeString());
                                        dtendDate = dtendDate.AddMinutes((double)dDuration);

                                        oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(dtendDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);

                                        //oDBParameters.Add("@EndDate", gloDateMaster.gloDate.DateAsNumber(_AppointmentDates.Dates[nAppCntr].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                        oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    }

                                    //oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(_ScheduleDates.Dates[i].ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    //oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.Users[k].EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParameters.Add("@nColorCode", oMasterSchedule.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@sNotes", oMasterSchedule.Notes, ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                    oDBParameters.Add("@nClinicID", oMasterSchedule.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                                    oDB.Execute("AS_INSERT_Schedule_DTL", oDBParameters, out  objectID);
                                }
                                #endregion
                            }

                            #endregion
                        }
                        else
                        {

                            _nMasterScheduleID = oMasterSchedule.MasterID;
                            _nDetailScheduleID = DetailScheduleID;


                            //***Delete and create new simple schedule instance***
                            #region "Delete Schedule from recurrence"
                            bool _NoSchedule = true;
                            DataTable _DeleteScheduleList = new DataTable();

                            #region "Delete Provider Schedule then Problem Type and Resources Schedule base on Provider Schedule"

                            Int64 _DelSchID = 0;
                            _DelSchID = DetailScheduleID;
                            string _strSQL;

                            //AS_Schedule_DTL - Provider
                            _strSQL = "DELETE FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + _nMasterScheduleID + " AND nDTLScheduleID = " + _DelSchID + " ";
                            oDB.Execute_Query(_strSQL);


                            #endregion


                            #region "Check Is there any schedule remaing - Start"
                            if (_DeleteScheduleList != null) { _DeleteScheduleList.Dispose(); }
                            _DeleteScheduleList = new DataTable();

                            _strSQL = "SELECT COUNT(nDTLScheduleID) FROM AS_Schedule_DTL  WITH(NOLOCK) WHERE " +
                                " nMSTScheduleID = " + _nMasterScheduleID + " ";

                            oDB.Retrive_Query(_strSQL, out _DeleteScheduleList);

                            if (_DeleteScheduleList != null)
                            {
                                if (_DeleteScheduleList.Rows.Count > 0)
                                {
                                    _NoSchedule = false;
                                }
                            }

                            if (_DeleteScheduleList != null) { _DeleteScheduleList.Dispose(); }
                            #endregion //Check Is there any schedule remaing - Finish

                            if (_NoSchedule == true)
                            {
                                #region "If no schedule then delete master records also"

                                //select *,nMasterScheduleID  from  AS_Schedule_MST_Criteria

                                //AS_Schedule_MST
                                _strSQL = "DELETE FROM AS_Schedule_MST WHERE nMSTScheduleID = " + _nMasterScheduleID + " ";
                                oDB.Execute_Query(_strSQL);
                                // AS_Schedule_MST_Criteria
                                _strSQL = "DELETE FROM AS_Schedule_MST_Criteria WHERE nMasterScheduleID = " + _nMasterScheduleID + " ";
                                oDB.Execute_Query(_strSQL);

                                #endregion
                            }
                            #endregion

                            #region "Create New Simple Appointment"
                            oMasterSchedule.IsRecurrence = SingleRecurrence.Single;

                            // Add Master Entry

                            #region "Master Schedule"

                            oDBParameters.Add("@nMSTScheduleID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                            oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@nASBaseID", oMasterSchedule.ASBaseID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@sASBaseCode", oMasterSchedule.ASBaseCode, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@sASBaseDesc", oMasterSchedule.ASBaseDescription, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@nASBaseFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                            oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.StartDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nDuration", oMasterSchedule.Duration, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                            oDB.Execute("AS_INSERT_Schedule_MST", oDBParameters, out  objectID);

                            if (objectID == null)
                            { return 0; }

                            _nMasterScheduleID = (Int64)objectID;
                            #endregion



                            #endregion



                            #region Problem Type

                            for (int k = 0; k < oMasterSchedule.ProblemTypes.Count; k++)
                            {
                                oDBParameters.Clear();

                                oDBParameters.Add("@nMSTScheduleID", _nMasterScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nDTLScheduleID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nLineNumber", _TempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nASBaseID", oMasterSchedule.ProblemTypes[k].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sASBaseCode", oMasterSchedule.ProblemTypes[k].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@sASBaseDesc", oMasterSchedule.ProblemTypes[k].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nASBaseFlag", oMasterSchedule.ProblemTypes[k].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nRefID", _nDetailScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nRefFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.StartDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nColorCode", oMasterSchedule.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sNotes", oMasterSchedule.Notes, ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                oDBParameters.Add("@nClinicID", oMasterSchedule.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                                oDB.Execute("AS_INSERT_Schedule_DTL", oDBParameters, out  objectID);

                            }

                            #endregion

                            #region Resources

                            for (int k = 0; k < oMasterSchedule.Resources.Count; k++)
                            {
                                oDBParameters.Clear();

                                oDBParameters.Add("@nMSTScheduleID", _nMasterScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nDTLScheduleID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nLineNumber", _TempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nASBaseID", oMasterSchedule.Resources[k].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sASBaseCode", oMasterSchedule.Resources[k].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@sASBaseDesc", oMasterSchedule.Resources[k].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nASBaseFlag", oMasterSchedule.Resources[k].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nRefID", _nDetailScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nRefFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.StartDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nColorCode", oMasterSchedule.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sNotes", oMasterSchedule.Notes, ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                oDBParameters.Add("@nClinicID", oMasterSchedule.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                                oDB.Execute("AS_INSERT_Schedule_DTL", oDBParameters, out  objectID);

                            }

                            #endregion

                            #region Users

                            for (int k = 0; k < oMasterSchedule.Users.Count; k++)
                            {
                                oDBParameters.Clear();

                                oDBParameters.Add("@nMSTScheduleID", _nMasterScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nDTLScheduleID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                                oDBParameters.Add("@bIsSingleRecurrence", oMasterSchedule.IsRecurrence.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nLineNumber", _TempLineNumber, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nScheduleFlag", oMasterSchedule.ASFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nASBaseID", oMasterSchedule.Users[k].ASCommonID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sASBaseCode", oMasterSchedule.Users[k].ASCommonCode, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@sASBaseDesc", oMasterSchedule.Users[k].ASCommonDescription, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nASBaseFlag", oMasterSchedule.Users[k].ASCommonFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@nRefID", _nDetailScheduleID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nRefFlag", oMasterSchedule.ASBaseFlag.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.StartDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.StartTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(oMasterSchedule.EndDate.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(oMasterSchedule.EndTime.ToString()), ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@nLocationID", oMasterSchedule.LocationID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sLocationName", oMasterSchedule.LocationName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nDepartmentID", oMasterSchedule.DepartmentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sDepartmentName", oMasterSchedule.DepartmentName, ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParameters.Add("@nColorCode", oMasterSchedule.ColorCode, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@sNotes", oMasterSchedule.Notes, ParameterDirection.Input, SqlDbType.VarChar);//oMasterAppointment.Notes
                                oDBParameters.Add("@nClinicID", oMasterSchedule.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                                oDB.Execute("AS_INSERT_Schedule_DTL", oDBParameters, out  objectID);

                            }

                            #endregion










                            //=========End Pranit================================================





                        }
                    }

                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                _nMasterScheduleID = 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _nMasterScheduleID = 0;
            }
            finally
            {
                oDB.Dispose();
                oDBParameters.Dispose();
                objectID = null;
                _ScheduleDates.Dispose();
            }
            return _nMasterScheduleID;

        }


        #region "Schedule GUI Methods"
        public void ShowScheduleView(bool FixedDialog)
        {
            //frmViewSchedule oViewSchedule = new frmViewSchedule(_databaseconnectionstring);
            frmViewSchedule oViewSchedule = frmViewSchedule.GetInstance(_databaseconnectionstring);
            oViewSchedule.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            if (FixedDialog == false)
            {
                oViewSchedule.Show();
            }
            else
            {
                oViewSchedule.ShowDialog(oViewSchedule.Parent);
                oViewSchedule.Dispose();
                oViewSchedule = null;
            }
        }

        public void ShowScheduleView(System.Windows.Forms.Form oParentWindow)
        {
            //frmViewSchedule oViewSchedule = new frmViewSchedule(_databaseconnectionstring);
            frmViewSchedule oViewSchedule = frmViewSchedule.GetInstance(_databaseconnectionstring);
            oViewSchedule.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            oViewSchedule.MdiParent = oParentWindow;
            oViewSchedule.Show();
        }

        public void ShowSchedule()
        {
            frmSetupSchedule ofrmSchedule = new frmSetupSchedule(_databaseconnectionstring);

            #region " Set initial parameters"
            ofrmSchedule.StartTime = DateTime.Now;
            ofrmSchedule.ScheduleType = AppointmentScheduleFlag.ProviderSchedule;
            #endregion

            ofrmSchedule.ShowDialog(ofrmSchedule.Parent);
            ofrmSchedule.Dispose();
        }
        //MasterScheduleID, ScheduleID, ModifyMethod, DatabaseConnectionString
        public void ShowSchedule(Int64 MasterScheduleID, Int64 ScheduleID, SingleRecurrence ModifyMethod, string DatabaseConnectionString)
        {
        }

        //Starttime ,Startdate, owner, OwnerName, connstring
        public void ShowSchedule(DateTime StartDate, TimeSpan StartTime, Int64 Owner, string OwnerName, string DatabaseConnectionString)
        {
        }

        //Starttime , Startdate, Enddate, Endtime, owner, OwnerName,  connstring 
        public void ShowSchedule(DateTime StartDate, TimeSpan StartTime, DateTime Enddate, TimeSpan Endtime, Int64 Owner, string OwnerName, string DatabaseConnectionString)
        {
        }

        //Starttime , Startdate, Enddate, Endtime, owner, ownerType, OwnerName, connstring 
        public void ShowSchedule(DateTime StartDate, TimeSpan StartTime, DateTime Enddate, TimeSpan Endtime, Int64 Owner, Int64 OwnerType, string OwnerName, string DatabaseConnectionString)
        {
        }
        #endregion

        public ShortApointmentSchedules GetScheduleDetails(DateTime _FillStartDateTime, DateTime _FillEndDateTime, ArrayList _FillProviders, ArrayList _FillResources)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public CalendarApointmentSchedules GetScheduleDetails_Old(DateTime FromDate, DateTime ToDate, ArrayList PrvResIDs, ASBaseType PrvResType, long ClinicID)
        {
            CalendarApointmentSchedules oSchedules = new CalendarApointmentSchedules();
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _sqlQuery = "";

                string _InPrvResIDs = "";
                for (int i = 0; i < PrvResIDs.Count; i++)
                {
                    if (i == 0)
                    {
                        _InPrvResIDs = "(" + PrvResIDs[i].ToString();
                    }
                    else
                    {
                        _InPrvResIDs = _InPrvResIDs + "," + PrvResIDs[i].ToString();
                    }

                    if (i == PrvResIDs.Count - 1)
                    {
                        _InPrvResIDs = _InPrvResIDs + ")";
                    }
                }
                if (_InPrvResIDs.Length <= 0)
                    _InPrvResIDs = "(0)";



                _sqlQuery = "SELECT  nLineNumber, ISNULL(sLocationName,'') + SPACE(1) + ISNULL(sDepartmentName,'') AS ASText, " +
                " (Convert(varchar,nMSTScheduleID) + '~' + Convert(varchar,nDTLScheduleID) + '~' + Convert(varchar,bIsSingleRecurrence) + '~' + Convert(varchar,nScheduleFlag)) AS ASTag, " +
                " sNotes AS ASDescription,dtStartDate,dtStartTime, dtEndDate, dtEndTime,nASBaseID AS PrvResUsrID, nASBaseFlag AS PrvResUsrFlag, nColorCode AS ColorCode " +
                " FROM  AS_Schedule_DTL  WITH(NOLOCK) " +
                " WHERE (nASBaseFlag = " + PrvResType.GetHashCode() + ") AND (nASBaseID IN " + _InPrvResIDs + ") AND (nClinicID =  " + ClinicID + ") AND  " +
                " (dtStartDate >=  " + gloDateMaster.gloDate.DateAsNumber(FromDate.ToString()) + " ) AND (dtStartDate <= " + gloDateMaster.gloDate.DateAsNumber(ToDate.ToString()) + ") AND  " +
                " (dtEndDate >= " + gloDateMaster.gloDate.DateAsNumber(FromDate.ToString()) + ") AND (dtEndDate <= " + gloDateMaster.gloDate.DateAsNumber(ToDate.ToString()) + ")  " +
                " ORDER BY dtStartDate, nLineNumber ";

                DataTable dt = new DataTable();
                oDB.Retrive_Query(_sqlQuery, out dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CalendarApointmentSchedule oSchedule = new CalendarApointmentSchedule();

                        //Schedule  ASText =  Location + Department
                        oSchedule.ASText = dt.Rows[i]["ASText"].ToString();
                        //Schedule  ASTag = 
                        //Master ID + Detail ID + Single/Recurrence + Schedule Type (ScheduleFlag) 
                        oSchedule.ASTag = dt.Rows[i]["ASTag"].ToString();
                        //Schedule Description = Notes
                        oSchedule.ASDescription = dt.Rows[i]["ASDescription"].ToString();
                        oSchedule.StartDateTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dt.Rows[i]["dtStartDate"])), Convert.ToInt32(dt.Rows[i]["dtStartTime"]));
                        oSchedule.EndDateTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dt.Rows[i]["dtEndDate"])), Convert.ToInt32(dt.Rows[i]["dtEndTime"]));
                        oSchedule.PrvResUsrID = Convert.ToInt64(dt.Rows[i]["PrvResUsrID"]);
                        oSchedule.PrvResUsrFlag = (ASBaseType)Convert.ToInt32(dt.Rows[i]["PrvResUsrFlag"]);
                        oSchedule.ColorCode = Convert.ToInt32(dt.Rows[i]["ColorCode"].ToString());
                        oSchedules.Add(oSchedule);
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            return oSchedules;
        }

        public CalendarApointmentSchedules GetScheduleDetails(DateTime FromDate, DateTime ToDate, ArrayList ProviderIDs, ArrayList ResourceIDs, long ClinicID)
        {
            CalendarApointmentSchedules oSchedules = new CalendarApointmentSchedules();
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                string _sqlQuery = "";
                // string _InPrvResIDs = "";
                string _InProviderIDs = "";
                string _InResourceIDs = "";


                //Provider IDs
                for (int i = 0; i < ProviderIDs.Count; i++)
                {
                    if (i == 0)
                    {
                        _InProviderIDs = "(" + ProviderIDs[i].ToString();
                    }
                    else
                    {
                        _InProviderIDs = _InProviderIDs + "," + ProviderIDs[i].ToString();
                    }

                    if (i == ProviderIDs.Count - 1)
                    {
                        _InProviderIDs = _InProviderIDs + ")";
                    }
                }
                if (_InProviderIDs.Length <= 0)
                    _InProviderIDs = "(0)";


                //Resource IDs
                for (int i = 0; i < ResourceIDs.Count; i++)
                {
                    if (i == 0)
                    {
                        _InResourceIDs = "(" + ResourceIDs[i].ToString();
                    }
                    else
                    {
                        _InResourceIDs = _InResourceIDs + "," + ResourceIDs[i].ToString();
                    }

                    if (i == ResourceIDs.Count - 1)
                    {
                        _InResourceIDs = _InResourceIDs + ")";
                    }
                }
                if (_InResourceIDs.Length <= 0)
                    _InResourceIDs = "(0)";

                _sqlQuery = "SELECT  AS_Schedule_DTL.nLineNumber, ISNULL(AS_Schedule_DTL.sLocationName,'') + ' , ' + ISNULL(AS_Schedule_DTL.sDepartmentName,'') AS ASText, "
                + " (Convert(varchar,AS_Schedule_DTL.nMSTScheduleID) + '~' + Convert(varchar,AS_Schedule_DTL.nDTLScheduleID) + '~' + Convert(varchar,AS_Schedule_DTL.bIsSingleRecurrence) + '~' + Convert(varchar,AS_Schedule_DTL.nScheduleFlag)) AS ASTag, "
                + " AS_Schedule_DTL.sNotes AS ASDescription,AS_Schedule_DTL.dtStartDate,AS_Schedule_DTL.dtStartTime, AS_Schedule_DTL.dtEndDate, AS_Schedule_DTL.dtEndTime,AS_Schedule_DTL.nASBaseID AS PrvResUsrID, AS_Schedule_DTL.nASBaseFlag AS PrvResUsrFlag, AS_Schedule_DTL.nColorCode AS ColorCode,ISNULL(AS_Schedule_MST.sASBaseDesc,'') AS sASBaseDesc, AS_Schedule_DTL.nScheduleFlag,ISNULL(AS_Schedule_MST.nASBaseFlag,'') AS nASBaseFlag "
                + " FROM  AS_Schedule_DTL  WITH(NOLOCK) inner join AS_Schedule_MST  WITH(NOLOCK) on AS_Schedule_DTL.nMSTScheduleID=AS_Schedule_MST.nMSTScheduleID "
                + " WHERE ((AS_Schedule_DTL.nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " AND AS_Schedule_DTL.nASBaseID IN " + _InProviderIDs + ") OR (AS_Schedule_DTL.nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND AS_Schedule_DTL.nASBaseID IN " + _InResourceIDs + ")) "
                + " AND (AS_Schedule_DTL.nClinicID =  " + ClinicID + ") "
                + " AND (AS_Schedule_DTL.dtStartDate between " + gloDateMaster.gloDate.DateAsNumber(FromDate.ToString()) + " AND " + gloDateMaster.gloDate.DateAsNumber(ToDate.ToString()) + " "
                + " or AS_Schedule_DTL.dtEndDate between " + gloDateMaster.gloDate.DateAsNumber(FromDate.ToString()) + " AND " + gloDateMaster.gloDate.DateAsNumber(ToDate.ToString()) + " ) "
                + " ORDER BY AS_Schedule_DTL.dtStartDate, AS_Schedule_DTL.nLineNumber ";

                //commented below query part because there is not need to show 'only Recurrence' appointment on schedule screen

                //+ " UNION "
                //+ " SELECT nLineNumber, "
                //+ " ISNULL(sLocationName,'') + SPACE(1) + ISNULL(sDepartmentName,'') AS ASText,  "
                //+ " (Convert(varchar,nMSTAppointmentID) + '~' + Convert(varchar,nDTLAppointmentID) + '~' + Convert(varchar,bIsSingleRecurrence) + '~' + Convert(varchar,nAppointmentFlag)) AS ASTag,  "
                //+ " sNotes AS ASDescription,dtStartDate,dtStartTime,dtEndDate, dtEndTime,nASBaseID AS PrvResUsrID,nASBaseFlag AS PrvResUsrFlag,nColorCode AS ColorCode "
                //+ " FROM AS_Appointment_DTL "
                //+ " WHERE (nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " AND nASBaseID IN " + _InResourceIDs + ") AND (nClinicID =  " + ClinicID + ") AND  "
                //+ " (dtStartDate >=  " + gloDateMaster.gloDate.DateAsNumber(FromDate.ToString()) + " ) AND (dtStartDate <= " + gloDateMaster.gloDate.DateAsNumber(ToDate.ToString()) + ") AND  "
                //+ " (dtEndDate >= " + gloDateMaster.gloDate.DateAsNumber(FromDate.ToString()) + ") AND (dtEndDate <= " + gloDateMaster.gloDate.DateAsNumber(ToDate.ToString()) + ")  "




                DataTable dt = new DataTable();
                oDB.Retrive_Query(_sqlQuery, out dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CalendarApointmentSchedule oSchedule = new CalendarApointmentSchedule();

                        //Schedule  ASText =  Location + Department

                        // By Pranit on 16 Feb 2011 added "," in query
                        string asText = string.Empty;
                        if (dt.Rows[i]["ASText"].ToString().Trim() != "")
                        {
                            asText = dt.Rows[i]["ASText"].ToString().Trim();
                            if (asText != string.Empty)
                            {
                                string source;
                                if (asText.Length > 0)
                                {
                                    source = asText.Substring(asText.Length - 1, 1);
                                    if (source == ",")
                                    {
                                        asText = asText.Substring(0, asText.Length - 1);
                                    }
                                }
                            }
                        }

                        oSchedule.ASText = asText;
                        //Schedule  ASTag = 
                        //Master ID + Detail ID + Single/Recurrence + Schedule Type (ScheduleFlag) 
                        oSchedule.ASTag = dt.Rows[i]["ASTag"].ToString();
                        //Schedule Description = Notes

                        // GLO2012-0016240 : Appointment block type by time
                        // To check if BaseFlag is BlockType
                        if (dt.Rows[i]["nASBaseFlag"].ToString() == "5")
                        {
                            //Check for notes If available, show block type & notes together.
                            if (dt.Rows[i]["ASDescription"].ToString().Trim() != "")
                            {
                                // Check for block type If available, show block type & notes together.
                                if (dt.Rows[i]["sASBaseDesc"].ToString().Trim() != "")
                                {
                                    oSchedule.ASDescription = dt.Rows[i]["sASBaseDesc"].ToString() + " : " + dt.Rows[i]["ASDescription"].ToString();
                                }
                                else
                                {
                                    oSchedule.ASDescription = dt.Rows[i]["ASDescription"].ToString();
                                }
                            }
                            else
                            {
                                oSchedule.ASDescription = dt.Rows[i]["sASBaseDesc"].ToString();//+ " : " + dt.Rows[i]["ASDescription"].ToString();
                            }

                        }
                        else
                        {
                            oSchedule.ASDescription = dt.Rows[i]["ASDescription"].ToString();
                        }
                        //oSchedule.ASDescription = dt.Rows[i]["ASDescription"].ToString();
                        oSchedule.StartDateTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dt.Rows[i]["dtStartDate"])), Convert.ToInt32(dt.Rows[i]["dtStartTime"]));
                        oSchedule.EndDateTime = gloDateMaster.gloTime.TimeAsDateTime(gloDateMaster.gloDate.DateAsDate(Convert.ToInt32(dt.Rows[i]["dtEndDate"])), Convert.ToInt32(dt.Rows[i]["dtEndTime"]));
                        oSchedule.PrvResUsrID = Convert.ToInt64(dt.Rows[i]["PrvResUsrID"]);
                        oSchedule.PrvResUsrFlag = (ASBaseType)Convert.ToInt32(dt.Rows[i]["PrvResUsrFlag"]);
                        oSchedule.ColorCode = Convert.ToInt32(dt.Rows[i]["ColorCode"].ToString());
                        oSchedules.Add(oSchedule);
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            return oSchedules;
        }

        public MasterSchedule GetMasterSchedule(Int64 MasterScheduleID, Int64 ScheduleDetailID, SingleRecurrence RetriveMethod, SingleRecurrence RetriveMasterMethod, bool RetriveSingleInRecurrence, Int64 ClinicID)
        {
            MasterSchedule oMasterSchedule = new MasterSchedule();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            object _intresult = new object();
            DataTable oData = new DataTable();
            bool _FoundMasterRecord = false;

            Int64 _ProviderIDforProbTypeNRes = 0;
            Int64 _StartDateforProbTypeNRes = 0;
            Int64 _EndDateforProbTypeNRes = 0;

            ShortApointmentSchedule oProblemType = new ShortApointmentSchedule();
            ShortApointmentSchedule oResource = new ShortApointmentSchedule();
            ShortApointmentSchedule oProviders = new ShortApointmentSchedule();

            try
            {
                oDB.Connect(false);

                oMasterSchedule.MasterID = 0;

                #region "Retrive Master Information"
                _strSQL = "SELECT nMSTScheduleID, bIsSingleRecurrence, nScheduleFlag, "
                        + " ISNULl(nASBaseID,0) AS nASBaseID , ISNULL(sASBaseCode,'') AS sASBaseCode, ISNULL(sASBaseDesc,'') AS sASBaseDesc, nASBaseFlag, "
                        + " dtStartDate, dtStartTime, dtEndDate, dtEndTime, nDuration, "
                        + " ISNULL(nLocationID,0) AS nLocationID ,ISNULL(sLocationName,'') AS sLocationName, "
                        + " ISNULL(nDepartmentID,0) AS nDepartmentID , ISNULL(sDepartmentName,'') AS sDepartmentName ,ISNULL(nClinicID,0) AS nClinicID "
                        + " FROM   AS_Schedule_MST  WITH(NOLOCK) "
                        + " WHERE nMSTScheduleID = " + MasterScheduleID + " AND nClinicID = " + ClinicID + " ";

                oDB.Retrive_Query(_strSQL, out oData);

                if (oData != null)
                {
                    if (oData.Rows.Count > 0)
                    {
                        for (int i = 0; i <= oData.Rows.Count - 1; i++)
                        {
                            _FoundMasterRecord = true;
                            oMasterSchedule.IsRecurrence = (SingleRecurrence)Convert.ToInt32(oData.Rows[i]["bIsSingleRecurrence"]);
                            oMasterSchedule.ASFlag = (AppointmentScheduleFlag)Convert.ToInt32(oData.Rows[i]["nScheduleFlag"]);
                            _ProviderIDforProbTypeNRes = Convert.ToInt64(oData.Rows[i]["nASBaseID"].ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                            oMasterSchedule.ASBaseID = Convert.ToInt64(oData.Rows[i]["nASBaseID"].ToString());
                            oMasterSchedule.ASBaseCode = oData.Rows[i]["sASBaseCode"].ToString();
                            oMasterSchedule.ASBaseDescription = oData.Rows[i]["sASBaseDesc"].ToString();
                            oMasterSchedule.ASBaseFlag = (ASBaseType)Convert.ToInt32(oData.Rows[i]["nASBaseFlag"]);

                            _StartDateforProbTypeNRes = Convert.ToInt64(oData.Rows[i]["dtStartDate"].ToString());
                            oMasterSchedule.StartDate = gloDateMaster.gloDate.DateAsDate(_StartDateforProbTypeNRes);
                            oMasterSchedule.StartTime = gloDateMaster.gloTime.TimeAsDateTime(oMasterSchedule.StartDate, Convert.ToInt32(oData.Rows[i]["dtStartTime"].ToString()));
                            _EndDateforProbTypeNRes = Convert.ToInt64(oData.Rows[i]["dtEndDate"].ToString());
                            oMasterSchedule.EndDate = gloDateMaster.gloDate.DateAsDate(_EndDateforProbTypeNRes);
                            oMasterSchedule.EndTime = gloDateMaster.gloTime.TimeAsDateTime(oMasterSchedule.EndDate, Convert.ToInt32(oData.Rows[i]["dtEndTime"].ToString()));
                            oMasterSchedule.Duration = Convert.ToDecimal(oData.Rows[i]["nDuration"].ToString());

                            //oMasterSchedule.ColorCode = Convert.ToInt32(oData.Rows[i]["nColorCode"].ToString());
                            //Dhruv 20100104 Here we are storing the Id but providing the name to it which was wrong
                            //oMasterSchedule.LocationID = GetLocationID(oData.Rows[i]["sLocationName"].ToString(), ClinicID);
                            //Accepting the actual value id..
                            oMasterSchedule.LocationID = GetLocationID(oData.Rows[i]["nLocationID"].ToString(), ClinicID);
                            //End by dhruv
                            oMasterSchedule.LocationName = oData.Rows[i]["sLocationName"].ToString();
                            oMasterSchedule.DepartmentID = GetDepartmentID(oData.Rows[i]["sDepartmentName"].ToString(), oMasterSchedule.LocationID, ClinicID);
                            oMasterSchedule.DepartmentName = oData.Rows[i]["sDepartmentName"].ToString();
                            oMasterSchedule.Notes = "";
                            oMasterSchedule.ClinicID = ClinicID;
                            oMasterSchedule.UsedStatus = ASUsedStatus.Unknown5;

                        }
                    }
                }
                oData.Dispose();
                #endregion

                if (_FoundMasterRecord == true)
                {
                    #region "Retrive Criteria"
                    oData = new DataTable();
                    _strSQL = "SELECT ISNULL(bIsSingleRecurrence,0) AS bIsSingleRecurrence, " +
                    " ISNULL(nRecurrence_PatternType,0) AS nRecurrence_PatternType,  " +
                    " ISNULL(nRecurrence_Pattern_Daily_EveryDayNo,0) AS nRecurrence_Pattern_Daily_EveryDayNo,  " +
                    " ISNULL(nRecurrence_Pattern_Daily_EveryDayOrWeekDay,0) AS nRecurrence_Pattern_Daily_EveryDayOrWeekDay,  " +
                    " ISNULL(nRecurrence_Pattern_Weekly_EveryWeekNo,0) AS  nRecurrence_Pattern_Weekly_EveryWeekNo,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Sunday,0) AS bRecurrence_Pattern_Weekly_Sunday,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Monday,0) AS bRecurrence_Pattern_Weekly_Monday,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Tuesday,0) AS bRecurrence_Pattern_Weekly_Tuesday,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Wednesday,0) AS bRecurrence_Pattern_Weekly_Wednesday,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Thursday,0) AS bRecurrence_Pattern_Weekly_Thursday,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Friday,0) AS bRecurrence_Pattern_Weekly_Friday,  " +
                    " ISNULL(bRecurrence_Pattern_Weekly_Saturday,0) AS bRecurrence_Pattern_Weekly_Saturday,  " +
                    " ISNULL(nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria,0) AS nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria,  " +
                    " ISNULL(nRecurrence_Pattern_Monthly_DayNumber,0) AS nRecurrence_Pattern_Monthly_DayNumber,  " +
                    " ISNULL(nRecurrence_Pattern_Monthly_EveryMonthNumber,0) AS nRecurrence_Pattern_Monthly_EveryMonthNumber,  " +
                    " ISNULL(nRecurrence_Pattern_Monthly_FstLstCriteriaID,0) AS nRecurrence_Pattern_Monthly_FstLstCriteriaID,  " +
                    " ISNULL(nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID,0) AS nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID,  " +
                    " ISNULL(nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria,0) AS nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria,  " +
                    " ISNULL(nRecurrence_Pattern_Yearly_DayNumber,0) AS nRecurrence_Pattern_Yearly_DayNumber,  " +
                    " ISNULL(nRecurrence_Pattern_Yearly_MonthOfCriteriaID,0) AS nRecurrence_Pattern_Yearly_MonthOfCriteriaID,  " +
                    " ISNULL(nRecurrence_Pattern_Yearly_FstLstCriteriaID,0) AS nRecurrence_Pattern_Yearly_FstLstCriteriaID,  " +
                    " ISNULL(nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID,0) AS nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID,  " +
                    " ISNULL(bRange_Flag,0) AS bRange_Flag,  " +
                    " ISNULL(nRange_StartDate,0) AS nRange_StartDate,  " +
                    " ISNULL(nRange_StartTime,0) AS nRange_StartTime,  " +
                    " ISNULL(nRange_EndDate,0) AS nRange_EndDate,  " +
                    " ISNULL(nRange_EndTime,0) AS nRange_EndTime,  " +
                    " ISNULL(nRange_Duration,0) AS nRange_Duration,  " +
                    " ISNULL(nRange_NoOfOccurence,0) AS nRange_NoOfOccurence,  " +
                    " ISNULL(nRange_NoEndDateYear,0) AS nRange_NoEndDateYear " +
                    " FROM AS_Schedule_MST_Criteria  WITH(NOLOCK) WHERE nMasterScheduleID = " + MasterScheduleID + " and nClinicID = " + ClinicID + "";

                    oDB.Retrive_Query(_strSQL, out oData);

                    if (oData != null)
                    {
                        if (oData.Rows.Count > 0)
                        {
                            oMasterSchedule.Criteria.SingleRecurrenceAppointment = (SingleRecurrence)Convert.ToInt32(oData.Rows[0]["bIsSingleRecurrence"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag = (RecurrenceRangeFlag)Convert.ToInt32(oData.Rows[0]["bRange_Flag"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Range.StartDate = Convert.ToInt32(oData.Rows[0]["nRange_StartDate"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.CriteriaDateTime.StartTime = Convert.ToInt64(oData.Rows[0]["nRange_StartTime"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndDate = Convert.ToInt32(oData.Rows[0]["nRange_EndDate"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.CriteriaDateTime.EndTime = Convert.ToInt64(oData.Rows[0]["nRange_EndTime"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.CriteriaDateTime.Duration = Convert.ToDecimal(oData.Rows[0]["nRange_Duration"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Range.EndOccurrenceNumber = Convert.ToInt64(oData.Rows[0]["nRange_NoOfOccurence"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Range.NoEndDateYear = Convert.ToInt64(oData.Rows[0]["nRange_NoEndDateYear"]);

                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.RecurrencePatternType = (RecurrencePatternType)Convert.ToInt32(oData.Rows[0]["nRecurrence_PatternType"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = Convert.ToInt64(oData.Rows[0]["nRecurrence_Pattern_Daily_EveryDayNo"]);

                            //nRecurrence_Pattern_Daily_EveryDayOrWeekDay, nRecurrence_Pattern_Weekly_EveryWeekNo, bRecurrence_Pattern_Weekly_Sunday, 
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = (RecurrencePatternFlag)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Daily_EveryDayOrWeekDay"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber = Convert.ToInt64(oData.Rows[0]["nRecurrence_Pattern_Weekly_EveryWeekNo"]);

                            //bRecurrence_Pattern_Weekly_Monday, bRecurrence_Pattern_Weekly_Tuesday, bRecurrence_Pattern_Weekly_Wednesday, 
                            //bRecurrence_Pattern_Weekly_Thursday, bRecurrence_Pattern_Weekly_Friday, bRecurrence_Pattern_Weekly_Saturday, 
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Sunday"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Monday"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Tuesday"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Wednesday"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Thursday"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Friday"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday = Convert.ToBoolean(oData.Rows[0]["bRecurrence_Pattern_Weekly_Saturday"]);


                            //nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria, nRecurrence_Pattern_Monthly_DayNumber, nRecurrence_Pattern_Monthly_EveryMonthNumber, 
                            //nRecurrence_Pattern_Monthly_FstLstCriteriaID, nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID, nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria,

                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = (RecurrencePatternFlag)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Monthly_DayOfMonthOrCriteria"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = Convert.ToInt64(oData.Rows[0]["nRecurrence_Pattern_Monthly_DayNumber"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = Convert.ToInt64(oData.Rows[0]["nRecurrence_Pattern_Monthly_EveryMonthNumber"]);

                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = (FirstLastCriteria)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Monthly_FstLstCriteriaID"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = (gloAppointmentScheduling.DayWeekday)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Monthly_DayWeekdayCriteriaID"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = (RecurrencePatternFlag)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Yearly_EveryDayMonthOrCriteria"]);

                            //nRecurrence_Pattern_Yearly_DayNumber, nRecurrence_Pattern_Yearly_MonthOfCriteriaID, nRecurrence_Pattern_Yearly_FstLstCriteriaID,
                            //nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID, nClinicID              

                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = Convert.ToInt64(oData.Rows[0]["nRecurrence_Pattern_Yearly_DayNumber"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = (gloAppointmentScheduling.MonthRange)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Yearly_MonthOfCriteriaID"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = (gloAppointmentScheduling.FirstLastCriteria)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Yearly_FstLstCriteriaID"]);
                            oMasterSchedule.Criteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = (gloAppointmentScheduling.DayWeekday)Convert.ToInt32(oData.Rows[0]["nRecurrence_Pattern_Yearly_DayWeekDayCriteriaID"]);
                        }
                    }
                    oData.Dispose();
                    #endregion

                    #region "Notes AND Date/Time as per Single/Recurrence/Single inRecurrence"
                    //_ProviderIDforProbTypeNRes
                    //_StartDateforProbTypeNRes
                    //_EndDateforProbTypeNRes
                    oData = new DataTable();
                    if (RetriveMethod != SingleRecurrence.Recurrence)
                    {
                        if (RetriveSingleInRecurrence == true)
                        {
                            _strSQL = "SELECT dtStartDate, dtStartTime, dtEndDate, dtEndTime, " +
                            " ISNULL(nColorCode,0) AS nColorCode, ISNULL(sNotes,'') AS sNotes " +
                            " FROM AS_Schedule_DTL  WITH(NOLOCK) WHERE nMSTScheduleID = " + MasterScheduleID + " and (nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " OR nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + ") and nDTLScheduleID = " + ScheduleDetailID + " " +
                            " ORDER BY nDTLScheduleID";
                        }
                        else
                        {
                            //RESOLVED 10011
                            if (oMasterSchedule.ASBaseFlag == ASBaseType.Provider)
                            {
                                // Commented by Pranit on 16 feb 2011

                                //_strSQL = "SELECT TOP 1 dtStartDate, dtStartTime, dtEndDate, dtEndTime, " +
                                //" ISNULL(nColorCode,0) AS nColorCode, ISNULL(sNotes,'') AS sNotes " +
                                //" FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + MasterScheduleID + " and nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " and nDTLScheduleID  <> 0 " +
                                //" ORDER BY nDTLScheduleID";

                                // By Pranit on 16 feb 2011
                                _strSQL = "SELECT dtStartDate, dtStartTime, dtEndDate, dtEndTime, " +
                                " ISNULL(nColorCode,0) AS nColorCode, ISNULL(sNotes,'') AS sNotes " +
                                " FROM AS_Schedule_DTL  WITH(NOLOCK) WHERE nMSTScheduleID = " + MasterScheduleID + " and nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " and nDTLScheduleID  = " + ScheduleDetailID + " " +
                                " ORDER BY nDTLScheduleID";
                            }
                            else if (oMasterSchedule.ASBaseFlag == ASBaseType.Block || oMasterSchedule.ASBaseFlag == ASBaseType.Resource)
                            {
                                // Commented by Pranit on 16 feb 2011

                                // _strSQL = "SELECT TOP 1 dtStartDate, dtStartTime, dtEndDate, dtEndTime, " +
                                //" ISNULL(nColorCode,0) AS nColorCode, ISNULL(sNotes,'') AS sNotes " +
                                //" FROM AS_Schedule_DTL WHERE nMSTScheduleID = " + MasterScheduleID + " and (nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " OR nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + ") and nDTLScheduleID  <> 0 " +
                                //" ORDER BY nDTLScheduleID";

                                // By Pranit on 16 feb 2011
                                _strSQL = "SELECT dtStartDate, dtStartTime, dtEndDate, dtEndTime, " +
                               " ISNULL(nColorCode,0) AS nColorCode, ISNULL(sNotes,'') AS sNotes " +
                               " FROM AS_Schedule_DTL  WITH(NOLOCK) WHERE nMSTScheduleID = " + MasterScheduleID + " and (nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " OR nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + ") and nDTLScheduleID  = " + ScheduleDetailID + " " +
                               " ORDER BY nDTLScheduleID";
                            }
                        }

                        if (_strSQL.Trim() != "")
                        {
                            oDB.Retrive_Query(_strSQL, out oData);
                            if (oData != null)
                            {
                                if (oData.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oData.Rows.Count - 1; i++)
                                    {
                                        oMasterSchedule.StartDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oData.Rows[0]["dtStartDate"].ToString()));
                                        oMasterSchedule.StartTime = gloDateMaster.gloTime.TimeAsDateTime(oMasterSchedule.StartDate, Convert.ToInt32(oData.Rows[0]["dtStartTime"].ToString()));
                                        oMasterSchedule.EndDate = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oData.Rows[0]["dtEndDate"].ToString()));
                                        oMasterSchedule.EndTime = gloDateMaster.gloTime.TimeAsDateTime(oMasterSchedule.EndDate, Convert.ToInt32(oData.Rows[0]["dtEndTime"].ToString()));
                                        oMasterSchedule.ColorCode = Convert.ToInt32(oData.Rows[0]["nColorCode"].ToString());
                                        oMasterSchedule.Notes = oData.Rows[0]["sNotes"].ToString();

                                        //Update Duration
                                        TimeSpan _dur = oMasterSchedule.EndTime.Subtract(oMasterSchedule.StartTime);
                                        oMasterSchedule.Duration = Convert.ToDecimal(_dur.TotalMinutes);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        oData = new DataTable();
                        // Made Changes in query added "OR" case "nASBaseFlag = " + ASBaseType.Resource.GetHashCode()" on 2 march 2012
                        _strSQL = "SELECT TOP 1 dtStartDate, dtStartTime, dtEndDate, dtEndTime, " +
                        " ISNULL(nColorCode,0) AS nColorCode, ISNULL(sNotes,'') AS sNotes " +
                        " FROM AS_Schedule_DTL  WITH(NOLOCK) WHERE nMSTScheduleID = " + MasterScheduleID + " and (nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " OR nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + ") and nDTLScheduleID <> 0 " +
                        " ORDER BY nDTLScheduleID";

                        if (_strSQL.Trim() != "")
                        {
                            oDB.Retrive_Query(_strSQL, out oData);
                            if (oData != null)
                            {
                                if (oData.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oData.Rows.Count - 1; i++)
                                    {
                                        oMasterSchedule.Notes = oData.Rows[0]["sNotes"].ToString();
                                        oMasterSchedule.ColorCode = Convert.ToInt32(oData.Rows[0]["nColorCode"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    oData.Dispose();
                    #endregion

                    #region "Retrive Provider Schedule Details"
                    oData = new DataTable();
                    if (RetriveMethod == SingleRecurrence.Single)
                    {
                        _strSQL = "";
                    }
                    else if (RetriveMethod == SingleRecurrence.Recurrence)
                    {
                        _strSQL = "";
                    }
                    else if (RetriveMethod == SingleRecurrence.SingleInRecurrence)
                    {
                        _strSQL = "";
                    }

                    if (_strSQL.Trim() != "")
                    {
                        oDB.Retrive_Query(_strSQL, out oData);
                        if (oData != null)
                        {
                            if (oData.Rows.Count > 0)
                            {
                                for (int i = 0; i <= oData.Rows.Count - 1; i++)
                                {
                                    oMasterSchedule.ScheduleDetails = null;
                                }
                            }
                        }
                    }
                    oData.Dispose();
                    #endregion

                    #region "Retrive Problem Type Schedules"

                    oData = new DataTable();
                    if (RetriveMasterMethod == SingleRecurrence.Single)
                    {
                        _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc,ISNULL(nColorCode,0) AS nColorCode, nASBaseFlag, " +
                        " " + _StartDateforProbTypeNRes + " As dtStartDate , " + _EndDateforProbTypeNRes + " AS dtEndDate  " +
                        " FROM AS_Schedule_DTL  WITH(NOLOCK) where nMSTScheduleID = " + MasterScheduleID + " AND nDTLScheduleID <> 0 " +
                        " AND (nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + ") AND bIsSingleRecurrence <> " + SingleRecurrence.SingleInRecurrence.GetHashCode() + "";
                    }
                    else
                    {
                        if (RetriveSingleInRecurrence == false)
                        {
                            _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc,ISNULL(nColorCode,0) AS nColorCode, nASBaseFlag, " +
                            " " + _StartDateforProbTypeNRes + " As dtStartDate , " + _EndDateforProbTypeNRes + " AS dtEndDate  " +
                            " FROM AS_Schedule_DTL  WITH(NOLOCK) where nMSTScheduleID = " + MasterScheduleID + " AND nDTLScheduleID <> 0 " +
                            " AND (nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + ") AND bIsSingleRecurrence <> " + SingleRecurrence.SingleInRecurrence.GetHashCode() + "";
                        }
                        else
                        {


                            _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc,ISNULL(nColorCode,0) AS nColorCode, nASBaseFlag,dtStartDate,dtEndDate " +
                            " FROM AS_Schedule_DTL WITH(NOLOCK)  where nMSTScheduleID = " + MasterScheduleID + " AND nRefID = " + ScheduleDetailID + " " +
                            " AND nRefFlag = " + ASBaseType.Provider.GetHashCode() + " AND nASBaseFlag = " + ASBaseType.ProblemType.GetHashCode() + " ";
                        }
                    }

                    oDB.Retrive_Query(_strSQL, out oData);
                    if (oData != null)
                    {
                        if (oData.Rows.Count > 0)
                        {
                            for (int i = 0; i <= oData.Rows.Count - 1; i++)
                            {
                                oProblemType = new ShortApointmentSchedule();

                                oProblemType.MasterID = MasterScheduleID;
                                oProblemType.DetailID = 0;
                                oProblemType.IsRecurrence = RetriveMasterMethod;
                                oProblemType.PatientID = 0; // only for appointment purpose to retrive in calendar
                                oProblemType.LineNo = 0;
                                oProblemType.ASFlag = oMasterSchedule.ASFlag;
                                oProblemType.ASCommonID = GetProblemTypeID(oData.Rows[i]["sASBaseCode"].ToString(), oData.Rows[i]["sASBaseDesc"].ToString(), ClinicID);// Convert.ToInt64(oData.Rows[i]["nASBaseID"].ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                                oProblemType.ASCommonCode = oData.Rows[i]["sASBaseCode"].ToString();
                                oProblemType.ASCommonDescription = oData.Rows[i]["sASBaseDesc"].ToString();
                                oProblemType.ASCommonFlag = (ASBaseType)Convert.ToInt32(oData.Rows[i]["nASBaseFlag"]);

                                Int64 _StDt = Convert.ToInt64(oData.Rows[i]["dtStartDate"].ToString());
                                Int64 _EdDt = Convert.ToInt64(oData.Rows[i]["dtEndDate"].ToString());
                                oProblemType.StartDate = gloDateMaster.gloDate.DateAsDate(_StDt);
                                oProblemType.EndDate = gloDateMaster.gloDate.DateAsDate(_EdDt);

                                oProblemType.StartTime = gloDateMaster.gloTime.TimeAsDateTime(oProblemType.StartDate, Convert.ToInt32(oData.Rows[i]["dtStartTime"].ToString()));
                                oProblemType.EndTime = gloDateMaster.gloTime.TimeAsDateTime(oProblemType.EndDate, Convert.ToInt32(oData.Rows[i]["dtEndTime"].ToString()));

                                oProblemType.ColorCode = Convert.ToInt32(oData.Rows[0]["nColorCode"].ToString());
                                oMasterSchedule.ColorCode = Convert.ToInt32(oData.Rows[0]["nColorCode"].ToString());
                                oProblemType.ClinicID = ClinicID;
                                oProblemType.ViewOtherDetails = ""; // only for appointment purpose to retrive in calendar

                                oMasterSchedule.ProblemTypes.Add(oProblemType);
                                oProblemType.Dispose();
                            }
                        }
                    }
                    oData.Dispose();
                    #endregion

                    #region "Retrive Resources Schedules"
                    oData = new DataTable();
                    if (RetriveMasterMethod == SingleRecurrence.Single)
                    {
                        _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc,ISNULL(nColorCode,0) AS nColorCode, nASBaseFlag, " +
                        " " + _StartDateforProbTypeNRes + " As dtStartDate , " + _EndDateforProbTypeNRes + " AS dtEndDate  " +
                        " FROM AS_Schedule_DTL  WITH(NOLOCK) where nMSTScheduleID = " + MasterScheduleID + " AND nDTLScheduleID <> 0 " +
                        " AND (nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + ") AND bIsSingleRecurrence <> " + SingleRecurrence.SingleInRecurrence.GetHashCode() + "";
                    }
                    else
                    {
                        if (RetriveSingleInRecurrence == false)
                        {
                            _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode,ISNULL(nColorCode,0) AS nColorCode, sASBaseDesc, nASBaseFlag, " +
                            " " + _StartDateforProbTypeNRes + " As dtStartDate , " + _EndDateforProbTypeNRes + " AS dtEndDate  " +
                            " FROM AS_Schedule_DTL  WITH(NOLOCK) where nMSTScheduleID = " + MasterScheduleID + " AND nDTLScheduleID <> 0 " +
                            " AND (nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + ") AND bIsSingleRecurrence <> " + SingleRecurrence.SingleInRecurrence.GetHashCode() + "";
                        }
                        else
                        {
                            _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc,ISNULL(nColorCode,0) AS nColorCode, nASBaseFlag,dtStartDate,dtEndDate " +
                            " FROM AS_Schedule_DTL  WITH(NOLOCK) where nMSTScheduleID = " + MasterScheduleID + " AND (nDTLScheduleID = " + ScheduleDetailID + " OR  nRefID = " + ScheduleDetailID + ") " +
                            " AND (nRefFlag = " + ASBaseType.Block.GetHashCode() + " OR  nRefFlag = " + ASBaseType.Provider.GetHashCode() + "  ) AND nASBaseFlag = " + ASBaseType.Resource.GetHashCode() + " ";
                        }
                    }

                    oDB.Retrive_Query(_strSQL, out oData);
                    if (oData != null)
                    {
                        if (oData.Rows.Count > 0)
                        {
                            for (int i = 0; i <= oData.Rows.Count - 1; i++)
                            {
                                oResource = new ShortApointmentSchedule();

                                oResource.MasterID = MasterScheduleID;
                                oResource.DetailID = 0;
                                oResource.IsRecurrence = RetriveMasterMethod;
                                oResource.PatientID = 0; // only for appointment purpose to retrive in calendar
                                oResource.LineNo = 0;
                                oResource.ASFlag = oMasterSchedule.ASFlag;
                                //Bug #67759: 00000687: adding notes the appointments are no longer on the calendar
                                //oResource.ASCommonID = GetResourceID(oData.Rows[i]["sASBaseCode"].ToString(), oData.Rows[i]["sASBaseDesc"].ToString(), ClinicID);// Convert.ToInt64(oData.Rows[i]["nASBaseID"].ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                                oResource.ASCommonID = Convert.ToInt64(oData.Rows[i]["nASBaseID"]);
                                oResource.ASCommonCode = oData.Rows[i]["sASBaseCode"].ToString();
                                oResource.ASCommonDescription = oData.Rows[i]["sASBaseDesc"].ToString();
                                oResource.ASCommonFlag = (ASBaseType)Convert.ToInt32(oData.Rows[i]["nASBaseFlag"]);

                                Int64 _StDt = Convert.ToInt64(oData.Rows[i]["dtStartDate"].ToString());
                                Int64 _EdDt = Convert.ToInt64(oData.Rows[i]["dtEndDate"].ToString());
                                oResource.StartDate = gloDateMaster.gloDate.DateAsDate(_StDt);
                                oResource.EndDate = gloDateMaster.gloDate.DateAsDate(_EdDt);

                                oResource.StartTime = gloDateMaster.gloTime.TimeAsDateTime(oResource.StartDate, Convert.ToInt32(oData.Rows[i]["dtStartTime"].ToString()));
                                oResource.EndTime = gloDateMaster.gloTime.TimeAsDateTime(oResource.EndDate, Convert.ToInt32(oData.Rows[i]["dtEndTime"].ToString()));

                                oResource.ColorCode = Convert.ToInt32(oData.Rows[0]["nColorCode"].ToString());
                                oMasterSchedule.ColorCode = Convert.ToInt32(oData.Rows[0]["nColorCode"].ToString());
                                oResource.ClinicID = ClinicID;
                                oResource.ViewOtherDetails = ""; // only for appointment purpose to retrive in calendar

                                oMasterSchedule.Resources.Add(oResource);
                                oResource.Dispose();
                            }
                        }
                    }
                    oData.Dispose();
                    #endregion

                    #region "Retrive Provider Schedules For Block Type"
                    oData = new DataTable();
                    if (RetriveMasterMethod == SingleRecurrence.Single)
                    {
                        _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc,ISNULL(nColorCode,0) AS nColorCode, nASBaseFlag, " +
                        " " + _StartDateforProbTypeNRes + " As dtStartDate , " + _EndDateforProbTypeNRes + " AS dtEndDate  " +
                        " FROM AS_Schedule_DTL  WITH(NOLOCK) where nMSTScheduleID = " + MasterScheduleID + " AND nDTLScheduleID <> 0 " +
                        " AND (nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + ") AND bIsSingleRecurrence <> " + SingleRecurrence.SingleInRecurrence.GetHashCode() + "";
                    }
                    else
                    {
                        if (RetriveSingleInRecurrence == false)
                        {
                            _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc,ISNULL(nColorCode,0) AS nColorCode, nASBaseFlag, " +
                            " " + _StartDateforProbTypeNRes + " As dtStartDate , " + _EndDateforProbTypeNRes + " AS dtEndDate  " +
                            " FROM AS_Schedule_DTL  WITH(NOLOCK) where nMSTScheduleID = " + MasterScheduleID + " AND nDTLScheduleID <> 0 " +
                            " AND (nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + ") AND bIsSingleRecurrence <> " + SingleRecurrence.SingleInRecurrence.GetHashCode() + "";
                        }
                        else
                        {
                            _strSQL = "SELECT DISTINCT dtStartTime, dtEndTime, nASBaseID, sASBaseCode, sASBaseDesc,ISNULL(nColorCode,0) AS nColorCode, nASBaseFlag,dtStartDate,dtEndDate " +
                            " FROM AS_Schedule_DTL  WITH(NOLOCK) where nMSTScheduleID = " + MasterScheduleID + " AND nDTLScheduleID = " + ScheduleDetailID + " " +
                            " AND nRefFlag = " + ASBaseType.Block.GetHashCode() + " AND nASBaseFlag = " + ASBaseType.Provider.GetHashCode() + " ";
                        }
                    }

                    oDB.Retrive_Query(_strSQL, out oData);
                    if (oData != null)
                    {
                        if (oData.Rows.Count > 0)
                        {
                            for (int i = 0; i <= oData.Rows.Count - 1; i++)
                            {
                                oProviders = new ShortApointmentSchedule();

                                oProviders.MasterID = MasterScheduleID;
                                oProviders.DetailID = 0;
                                oProviders.IsRecurrence = RetriveMasterMethod;
                                oProviders.PatientID = 0; // only for appointment purpose to retrive in calendar
                                oProviders.LineNo = 0;
                                oProviders.ASFlag = oMasterSchedule.ASFlag;
                                oProviders.ASCommonID = Convert.ToInt64(oData.Rows[i]["nASBaseID"]); // Convert.ToInt64(oData.Rows[i]["nASBaseID"].ToString()); //Appointment - Provider ID, Schedule - Provider Base Schedule, Resource Base Schedule, Block Schedule
                                oProviders.ASCommonCode = oData.Rows[i]["sASBaseCode"].ToString();
                                oProviders.ASCommonDescription = oData.Rows[i]["sASBaseDesc"].ToString();
                                oProviders.ASCommonFlag = (ASBaseType)Convert.ToInt32(oData.Rows[i]["nASBaseFlag"]);

                                Int64 _StDt = Convert.ToInt64(oData.Rows[i]["dtStartDate"].ToString());
                                Int64 _EdDt = Convert.ToInt64(oData.Rows[i]["dtEndDate"].ToString());
                                oProviders.StartDate = gloDateMaster.gloDate.DateAsDate(_StDt);
                                oProviders.EndDate = gloDateMaster.gloDate.DateAsDate(_EdDt);

                                oProviders.StartTime = gloDateMaster.gloTime.TimeAsDateTime(oProviders.StartDate, Convert.ToInt32(oData.Rows[i]["dtStartTime"].ToString()));
                                oProviders.EndTime = gloDateMaster.gloTime.TimeAsDateTime(oProviders.EndDate, Convert.ToInt32(oData.Rows[i]["dtEndTime"].ToString()));



                                oProviders.ColorCode = Convert.ToInt32(oData.Rows[0]["nColorCode"].ToString());
                                oMasterSchedule.ColorCode = Convert.ToInt32(oData.Rows[0]["nColorCode"].ToString());
                                oProviders.ClinicID = ClinicID;
                                oProviders.ViewOtherDetails = ""; // only for appointment purpose to retrive in calendar

                                oMasterSchedule.Users.Add(oProviders);
                                oProviders.Dispose();
                            }
                        }
                    }
                    oData.Dispose();
                    #endregion

                }
                oDB.Disconnect();
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
            //it will return master data as well as single date & time selected appointment and its associated problem types, resources, etc
            return oMasterSchedule;

        }

        public void AddNotes(Int64 MasterScheduleID, Int64 ScheduleDetailID, string sNotes)
        {
            try
            {
                // By Pranit on 14 feb 2012
                // if (sNotes.Trim() != "") commenting for solving issue #20992 
                //  {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                string _strSQL = "";
                object Value = new object();
                DataTable oData = new DataTable();
                try
                {
                    oDB.Connect(false);
                    sNotes = " " + sNotes.Trim();
                    sNotes = sNotes.Replace("'", "''");
                    if (sNotes.Length >= 1000)
                    {
                        sNotes = sNotes.Substring(0, 999);
                    }
                    _strSQL = "UPDATE  AS_Schedule_DTL SET sNotes = '" + sNotes + "' WHERE nMSTScheduleID = " + MasterScheduleID + " AND nDTLScheduleID = " + ScheduleDetailID + " AND nClinicID = " + _ClinicID + "";
                    oDB.Execute_Query(_strSQL);





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
                // }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }


        #region "Private Methods"

        private string GetProviderName(Int64 ProviderID, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _intresult = new object();
            string _result = "";
            string _strSQL = "";
            try
            {
                _result = "";
                oDB.Connect(false);

                _strSQL = "SELECT (ISNULL(sFirstName,'') + space(1) + ISNULL(sMiddleName,'') +  space(1) + ISNULL(sLastName,'')) AS ProviderName " +
                " FROM Provider_MST  WITH(NOLOCK) WHERE nProviderID  = " + ProviderID + " AND nClinicID = " + ClinicID + "";

                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = _intresult.ToString();
                    }
                }
                oDB.Disconnect();
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
            return _result;
        }

        private Int64 GetLocationID(string LocationName, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _intresult = new object();
            Int64 _result = 0;
            string _strSQL = "";
            try
            {
                _result = 0;
                oDB.Connect(false);
                _strSQL = "SELECT nLocationID  FROM AB_Location  WITH(NOLOCK) WHERE sLocation = '" + LocationName + "' AND bIsBlocked = 0 AND nClinicID = " + ClinicID + "";
                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = Convert.ToInt64(_intresult.ToString());
                    }
                }
                oDB.Disconnect();
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
            return _result;
        }

        private Int64 GetDepartmentID(string DepartmentName, Int64 LocationID, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _intresult = new object();
            Int64 _result = 0;
            string _strSQL = "";
            try
            {
                _result = 0;
                oDB.Connect(false);
                _strSQL = "SELECT nDepartmentID  FROM AB_Department  WITH(NOLOCK) WHERE sDepartment = '" + (DepartmentName).Replace("'", "''") + "' AND bIsBlocked = 0 AND nLocationID = " + LocationID + " AND nClinicID = " + ClinicID + "";
                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = Convert.ToInt64(_intresult.ToString());
                    }
                }
                oDB.Disconnect();
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
            return _result;
        }

        private Int64 GetProblemTypeID(string ProblemTypeCode, string ProblemTypeName, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _intresult = new object();
            Int64 _result = 0;
            string _strSQL = "";
            try
            {
                _result = 0;
                oDB.Connect(false);
                //20100106 Mahesh Nawal Solve the Bug No 453

                _strSQL = "SELECT nAppointmentTypeID  FROM AB_AppointmentType  WITH(NOLOCK) WHERE sAppointmentType = '" + ProblemTypeName.Trim().Replace("'", "''") + "' AND bIsBlocked = 0 AND nAppProcType = 2 AND nClinicID = " + ClinicID + "";
                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = Convert.ToInt64(_intresult.ToString());
                    }
                }
                oDB.Disconnect();
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
            return _result;
        }

        private Int64 GetResourceID(string ResourceCode, string ResourceName, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _intresult = new object();
            Int64 _result = 0;
            string _strSQL = "";
            try
            {
                _result = 0;
                oDB.Connect(false);
                //20100106 Mahesh Nawal
                _strSQL = "SELECT nResourceID  FROM AB_Resource_MST  WITH(NOLOCK) WHERE sCode = '" + ResourceCode.Trim().Replace("'", "''") + "' AND sDescription = '" + ResourceName.Trim().Replace("'", "''") + "' AND bitIsBlocked = 0 AND nClinicID = " + ClinicID + "";
                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                if (_intresult != null)
                {
                    if (_intresult.ToString() != "")
                    {
                        _result = Convert.ToInt64(_intresult.ToString());
                    }
                }
                oDB.Disconnect();
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
            return _result;
        }
        #endregion


    }

    public class gloAppointmnetScheduleCommon : IDisposable
    {
        #region "Constructor & Distructor"
        private string _databaseconnectionstring = "";
        private string _MessageBoxCaption = String.Empty;
        private Int64 _ClinicID = 0;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public gloAppointmnetScheduleCommon(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            //Code added on 9/01/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            //



            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

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

        ~gloAppointmnetScheduleCommon()
        {
            Dispose(false);
        }

        #endregion

        #region "Appointment Supporting"

        public gloGeneralItem.gloItems GetLocations()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtResult = new DataTable();
            gloGeneralItem.gloItems _result = new gloGeneralItem.gloItems();

            string _strSQL = "";
            oDB.Connect(false);

            try
            {
                _strSQL = "select nLocationID, sLocation from AB_Location  WITH(NOLOCK) where bIsBlocked = 0";
                oDB.Retrive_Query(_strSQL, out dtResult);
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtResult.Rows.Count - 1; i++)
                        {
                            _result.Add(Convert.ToInt64(dtResult.Rows[i]["nLocationID"].ToString()), dtResult.Rows[i]["sLocation"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtResult.Dispose();
            }
            return _result;
        }

        public DataTable GetLocationsList()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtLocation = new DataTable();
            //gloGeneralItem.gloItems _result = new gloGeneralItem.gloItems();

            string _strSQL = "";

            try
            {
                oDB.Connect(false);
                _strSQL = "select nLocationID, sLocation from AB_Location  WITH(NOLOCK) where bIsBlocked = 0";
                oDB.Retrive_Query(_strSQL, out dtLocation);

                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                return dtLocation;
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                dtLocation.Dispose();
                dtLocation = null;
            }
        }

        public gloGeneralItem.gloItems GetDepartments(Int64 LocationID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtResult = new DataTable();
            gloGeneralItem.gloItems _result = new gloGeneralItem.gloItems();

            string _strSQL = "";
            oDB.Connect(false);

            try
            {
                _strSQL = "select nDepartmentID, sDepartment from AB_Department  WITH(NOLOCK) where bIsBlocked=0 and nLocationID = " + LocationID + "";
                oDB.Retrive_Query(_strSQL, out dtResult);
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtResult.Rows.Count - 1; i++)
                        {
                            _result.Add(Convert.ToInt64(dtResult.Rows[i]["nDepartmentID"].ToString()), dtResult.Rows[i]["sDepartment"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtResult.Dispose();
            }
            return _result;
        }

        public gloGeneralItem.gloItems GetAppointmentTypes()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtResult = new DataTable();
            gloGeneralItem.gloItems _result = new gloGeneralItem.gloItems();

            string _strSQL = "";
            oDB.Connect(false);

            try
            {
                _strSQL = "select nAppointmentTypeID, sAppointmentType from AB_AppointmentType  WITH(NOLOCK) where bIsBlocked = 0 AND nAppProcType = 1 ORDER BY sAppointmentType ";
                oDB.Retrive_Query(_strSQL, out dtResult);
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtResult.Rows.Count - 1; i++)
                        {
                            _result.Add(Convert.ToInt64(dtResult.Rows[i]["nAppointmentTypeID"].ToString()), dtResult.Rows[i]["sAppointmentType"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                dtResult.Dispose();
            }
            return _result;
        }

        public DataTable GetAppointmentTypesList()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtAppointmentType = new DataTable();
            string _strSQL = "";

            try
            {
                oDB.Connect(false);
                //_strSQL = "select nAppointmentTypeID, sAppointmentType from AB_AppointmentType where bIsBlocked = 0 AND nAppProcType = 1 ORDER BY sAppointmentType ";
                _strSQL = "select 0 as nAppointmentTypeID, '' as sAppointmentType union select nAppointmentTypeID, sAppointmentType from AB_AppointmentType  WITH(NOLOCK) where bIsBlocked = 0 AND nAppProcType = 1 ORDER BY sAppointmentType ";
                oDB.Retrive_Query(_strSQL, out dtAppointmentType);
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                return dtAppointmentType;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                dtAppointmentType.Dispose();
                dtAppointmentType = null;
            }
        }

        public gloGeneralItem.gloItems GetAppointmentTypeDurationColor(Int64 AppointmetTypeID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtResult = new DataTable();
            gloGeneralItem.gloItems _result = new gloGeneralItem.gloItems();

            string _strSQL = "";
            oDB.Connect(false);

            try
            {
                _strSQL = "Select nAppointmentTypeID,nDuration,sColorCode from AB_AppointmentType  WITH(NOLOCK) where nAppointmentTypeID = " + AppointmetTypeID + " AND bIsBlocked = 0 AND nAppProcType = 1";
                oDB.Retrive_Query(_strSQL, out dtResult);
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtResult.Rows.Count - 1; i++)
                        {
                            _result.Add(Convert.ToInt64(dtResult.Rows[i]["nAppointmentTypeID"].ToString()), dtResult.Rows[i]["sColorCode"].ToString(), dtResult.Rows[i]["nDuration"].ToString());
                        }
                    }
                }
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
                oDB.Dispose();
                dtResult.Dispose();
            }
            return _result;
        }

        public gloGeneralItem.gloItems GetStatus()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            DataTable dtResult = new DataTable();
            gloGeneralItem.gloItems _result = new gloGeneralItem.gloItems();

            string _strSQL = "";

            oDB.Connect(false);

            try
            {

                _strSQL = "select nAppointmentStatusID, sAppointmentStatus from AB_AppointmentStatus  WITH(NOLOCK)  WHERE (bIsBlocked <> 'True' OR bIsBlocked IS NULL) ORDER BY sAppointmentStatus";


                oDB.Retrive_Query(_strSQL, out dtResult);


                if (dtResult != null)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtResult.Rows.Count - 1; i++)
                        {
                            _result.Add(Convert.ToInt64(dtResult.Rows[i]["nAppointmentStatusID"].ToString()), dtResult.Rows[i]["sAppointmentStatus"].ToString());
                        }
                    }
                }
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
                oDB.Dispose();
                dtResult.Dispose();
            }
            return _result;
        }


        public DataTable GetStatusList()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtStatusList = new DataTable();

            string _strSQL = "";
            oDB.Connect(false);

            try
            {
                //_strSQL = "select nAppointmentStatusID, sAppointmentStatus from AB_AppointmentStatus  WHERE (bIsBlocked <> 'True' OR bIsBlocked IS NULL) ORDER BY sAppointmentStatus";
                _strSQL = "select 0 as nAppointmentStatusID, '' as sAppointmentStatus union select nAppointmentStatusID, sAppointmentStatus from AB_AppointmentStatus   WITH(NOLOCK) WHERE (bIsBlocked <> 'True' OR bIsBlocked IS NULL) ORDER BY sAppointmentStatus";
                oDB.Retrive_Query(_strSQL, out dtStatusList);
                oDB.Disconnect();
                oDB.Dispose();
                return dtStatusList;
            }

            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                dtStatusList.Dispose();
                dtStatusList = null;
            }
        }


        public Int64 GetActiveStatus(Int64 MasterAppointmentID, Int64 AppointmentID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Int64 _result = 0;
            // Int32 _Status = 0;
            string _strSQL = "";
            oDB.Connect(false);

            try
            {
                _strSQL = "SELECT nStatusID As StatusID FROM AS_Appointment_DTL  WITH(NOLOCK) WHERE nMasterAppointmentID= " + MasterAppointmentID + " AND nAppointmentID= " + AppointmentID + " AND bIsLocked = 0";
                _result = Convert.ToInt64(oDB.ExecuteScalar_Query(_strSQL));
                //_result = Convert.ToInt64(_Status);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

                return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();

            }
            return _result;
        }

        public void UpdateStatus(Int64 MasterAppointmentID, Int64 AppointmentID, Int64 StatusID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            oDB.Connect(false);

            try
            {
                _strSQL = "UPDATE AS_Appointment_DTL SET nStatusID =" + StatusID + "  WHERE (nMasterAppointmentID = " + MasterAppointmentID + ") AND (nAppointmentID = " + AppointmentID + ") AND (bIsLocked = 0)";
                oDB.Execute_Query(_strSQL);
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
                oDB.Dispose();

            }
            // return _result;
        }

        public Int64 GetAppointmentsCount(Int64 StartTime, Int64 EndTime, Int64 StartDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            oDB.Connect(false);
            Int64 _result = 0;
            try
            {
                _strSQL = "Select Count(*) from AS_Appointment_DTL  WITH(NOLOCK) where nStartDate=" + StartDate + " And nStartTime=" + StartTime + " And nEndTime=" + EndTime + "";
                _result = Convert.ToInt64(oDB.ExecuteScalar_Query(_strSQL));
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
                oDB.Dispose();

            }
            return _result;
        }

        public Int64 GetAppointmentCountInA_Slot()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Int64 Result = 0;
            try
            {
                oDB.Connect(false);
                string strQuery = "SELECT sSettingsValue FROM Settings WHERE  sSettingsName = 'MaxAppointmentsInSlot'";
                Result = Convert.ToInt64(oDB.ExecuteScalar_Query(strQuery));
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
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
            return Result;
        }

        public gloGeneralItem.gloItems GetProviders()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtResult = new DataTable();
            gloGeneralItem.gloItems _result = new gloGeneralItem.gloItems();

            string _strSQL = "";
            oDB.Connect(false);

            try
            {
                _strSQL = " SELECT nProviderID AS ProviderID, " +
                    " (ISNULL(sFirstName,'') + space(1)  + CASE sMiddleName   WHEN  '' THEN '' When sMiddleName then   sMiddleName + SPACE(1) END +ISNULL(sLastName,'')) AS ProviderName " +
                    " From Provider_MST  WITH(NOLOCK) WHERE  ISNULL(bIsblocked,'false')='FALSE' ";

                if (_ClinicID > 0)
                {
                    _strSQL = _strSQL + " AND Provider_MST.nClinicID = " + _ClinicID + " ";
                }

                _strSQL = _strSQL + " ORDER BY ProviderName ";

                oDB.Retrive_Query(_strSQL, out dtResult);
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtResult.Rows.Count - 1; i++)
                        {
                            _result.Add(Convert.ToInt64(dtResult.Rows[i]["ProviderID"].ToString()), dtResult.Rows[i]["ProviderName"].ToString());
                        }
                    }
                }
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
                oDB.Dispose();
                dtResult.Dispose();
            }
            return _result;
        }

        public DataTable GetProvidersList()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtProvider = new DataTable();
            string _strSQL = "";
            try
            {
                oDB.Connect(false);

                //  query modified by Amit Bhavsar added SELECT 0 AS ProviderID, '' as ProviderName union
                _strSQL = "  SELECT 0 AS ProviderID, '' as ProviderName union SELECT nProviderID AS ProviderID, "
                        + " (ISNULL(sFirstName,'') + space(1)  + CASE sMiddleName   WHEN  '' THEN '' When sMiddleName then   sMiddleName + SPACE(1) END +ISNULL(sLastName,'')) AS ProviderName "
                        + " From Provider_MST  WITH(NOLOCK) WHERE  ISNULL(bIsblocked,'false')='FALSE' ";

                if (_ClinicID > 0)
                {
                    _strSQL = _strSQL + " AND Provider_MST.nClinicID = " + _ClinicID + " ";
                }

                _strSQL = _strSQL + " ORDER BY ProviderName ";

                oDB.Retrive_Query(_strSQL, out dtProvider);

                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                _strSQL = null;

                return dtProvider;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                dtProvider.Dispose();
                dtProvider = null;
            }
        }

        public DataTable GetProviderListView(int StartDate)
        {
            try
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

                oDBParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@StartDate", StartDate, ParameterDirection.Input, SqlDbType.Int);

                oDB.Connect(false);

                DataTable dt = new DataTable();

                oDB.Retrive("GetAppointmentProvider", oDBParameters, out dt);

                oDB.Disconnect();
                oDB = null;
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
        }



        public DataTable GetTemplateAppointmentType(Int64 TemplateID, Int32 SearchTime, Int32 SearchDate, Int64 ProviderID)
        {
            string _strSQL = "";
            // string _result = "";
            DataTable dt = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);
                _strSQL = "SELECT  AB_AppointmentType.sAppointmentType as DispName, AB_AppointmentType.nAppointmentTypeID as ID" +
                " FROM AB_AppointmentTemplate_DTL  WITH(NOLOCK) INNER JOIN AB_AppointmentType  WITH(NOLOCK) ON AB_AppointmentTemplate_DTL.nAppointmentTypeID = AB_AppointmentType.nAppointmentTypeID INNER JOIN AB_AppointmentTemplate_Allocation ON AB_AppointmentTemplate_DTL.nAppointmentTemplateID = AB_AppointmentTemplate_Allocation.nTemplateID " +
                " WHERE (AB_AppointmentTemplate_DTL.nAppointmentTemplateID = " + TemplateID + ") AND (" + SearchTime + " >= AB_AppointmentTemplate_DTL.nStartTime AND " + SearchTime + " <= AB_AppointmentTemplate_DTL.nEndTime) " +
                " AND (AB_AppointmentTemplate_Allocation.nProviderID = " + ProviderID + ") " +
                " AND (" + SearchDate + " >= AB_AppointmentTemplate_Allocation.nStartDate AND " + SearchDate + " <= AB_AppointmentTemplate_Allocation.nEndDate)";

                // object _internalresult = null;
                oDB.Retrive_Query(_strSQL, out dt);

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
                oDB.Dispose();
            }
            return dt;
        }

        public DataTable GetTemplateAppointments(Int64 StartDate, Int64 ProviderID)
        {
            string _strSQL = "";
            DataTable dt = new DataTable();
            DataTable dtInternal = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);

                _strSQL = " SELECT AB_AppointmentTemplate_Allocation.nProviderID, AB_AppointmentTemplate_Allocation.nTemplateID, AB_AppointmentTemplate_Allocation.nStartDate,  " +
                           " AB_AppointmentTemplate_Allocation.nEndDate, AB_AppointmentTemplate_DTL.nAppointmentTypeID, AB_AppointmentTemplate_DTL.nStartTime,  " +
                           " AB_AppointmentTemplate_DTL.nEndTime, AB_AppointmentTemplate_DTL.sColorCode, AB_AppointmentTemplate_DTL.nTemplateLineNo, AB_AppointmentType.sAppointmentType " +
                           " FROM   AB_AppointmentTemplate_DTL  WITH(NOLOCK) INNER JOIN " +
                           " AB_AppointmentTemplate_Allocation  WITH(NOLOCK) ON " +
                           " AB_AppointmentTemplate_DTL.nAppointmentTemplateID = AB_AppointmentTemplate_Allocation.nTemplateID INNER JOIN " +
                           " AB_AppointmentType ON AB_AppointmentTemplate_DTL.nAppointmentTypeID = AB_AppointmentType.nAppointmentTypeID " +
                           " WHERE (" + StartDate + " >= AB_AppointmentTemplate_Allocation.nStartDate  AND " + StartDate + " <= AB_AppointmentTemplate_Allocation.nEndDate ) AND (AB_AppointmentTemplate_Allocation.nProviderID =" + ProviderID + ") " +
                           " AND AB_AppointmentTemplate_Allocation.nTemplateID IS NOT NULL AND AB_AppointmentTemplate_DTL.nStartTime IS NOT NULL AND AB_AppointmentTemplate_DTL.nEndTime IS NOT NULL AND AB_AppointmentTemplate_DTL.bIsBlocked=0 " +
                           " AND  AB_AppointmentTemplate_DTL.nTemplateLineNo NOT IN (SELECT nTemplateLineNo " +
                           " FROM  AS_Appointment_DTL_Template  WITH(NOLOCK) " +
                           " WHERE (nProviderID = " + ProviderID + ") AND " +
                           " (nAppointmentTemplateID = (SELECT distinct AS_Appointment_DTL_Template.nAppointmentTemplateID " +
                           " FROM AS_Appointment_DTL_Template  WITH(NOLOCK) INNER JOIN " +
                           " AS_Appointment_DTL  WITH(NOLOCK) ON AS_Appointment_DTL_Template.nMasterAppointmentID = AS_Appointment_DTL.nMasterAppointmentID AND " +
                           " AS_Appointment_DTL_Template.nAppointmentID = AS_Appointment_DTL.nAppointmentID " +
                           " WHERE (AS_Appointment_DTL.nStartDate = " + StartDate + ")" +
                           " )))";
                oDB.Retrive_Query(_strSQL, out dt);
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
                oDB.Dispose();
            }

            return dt;
        }

        public DataTable GetTemplateAppointmentDetails(Int64 TemplateID, Int64 ProviderID, Int64 AppointmentTypeID)
        {
            string _strSQL = "";
            DataTable dt = new DataTable();

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);

                _strSQL = "SELECT AB_AppointmentTemplate_DTL.nStartTime, AB_AppointmentTemplate_DTL.nEndTime, AB_AppointmentTemplate_DTL.sColorCode, AB_AppointmentTemplate_Allocation.nStartDate " +
                            "FROM AB_AppointmentTemplate_DTL  WITH(NOLOCK) INNER JOIN " +
                            "AB_AppointmentTemplate_Allocation  WITH(NOLOCK) ON AB_AppointmentTemplate_DTL.nAppointmentTemplateID = AB_AppointmentTemplate_Allocation.nTemplateID " +
                            "WHERE nAppointmentTemplateID=" + TemplateID + " AND nAppointmentTypeID=" + AppointmentTypeID + " AND nProviderID=" + ProviderID + "";

                oDB.Retrive_Query(_strSQL, out dt);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

            return dt;
        }

        #endregion

    }

    // Code Start - Added by kanchan on 20091205 for case 3593
    // Flag for HL7 Queue for Outbound HL7 file

    public enum HL7AppointmentFlag
    {
        None = 0,
        Add = 1,
        Update = 2,
        Delete = 3,
        Print = 4
    }

    public static class gloHL7
    {
        public static HL7AppointmentFlag _AppointmentHL7Flag = HL7AppointmentFlag.None;

        //Changes :Variables added for saving provider details when appointment opend for modify
        //& wriiten new function InsertInMessageQueue() with additional boolean parameter 
        public static long nOldBaseId = 0;
        //public static string nOldBaseCode = "";
        //public static string nOldBaseDesc = "";


        // public static Boolean boolSendChargesToHL7 = false; // HL7 //on Setting screen variable for EMR
        public static Boolean boolSendPatientDetails = false; // Send Patient Details // //on Setting screen variable for EMR
        public static Boolean boolSendAppointmentDetails = false;// Send Appointment Details // //on Setting screen variable for EMR
        public static Boolean boolHL7SENDOUTBOUNDGLOPM = false;
        public static Boolean boolHL7SENDOUTBOUNDGLOEMR = false;
        public static string sGLOLABHSILABEL = string.Empty;


        //6031
        static System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public static void InsertInMessageQueue(string strMessageName, Int64 PatientID, Int64 OtherID, string Field1, string _Connectionstring, bool IsSendProviderInfo)
        {
            gloDatabaseLayer.DBLayer oDBLayer;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(_Connectionstring);
                gloDatabaseLayer.DBParameters oDBParamters = new gloDatabaseLayer.DBParameters();
                oDBLayer.Connect(false);

                oDBParamters.Add("@dtDatetimeStamp", DateAndTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParamters.Add("@MessageName", strMessageName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@sMachineID", GetMachineID(System.Environment.MachineName, _Connectionstring), ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@sMachinename", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParamters.Add("@nID", OtherID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParamters.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int, 1);
                oDBParamters.Add("@sField1", Field1, ParameterDirection.Input, SqlDbType.VarChar, 5000);
                oDBParamters.Add("@MachineID", oDBLayer.GetPrefixTransactionID(PatientID), ParameterDirection.Input, SqlDbType.BigInt);

                if (IsSendProviderInfo)
                {
                    oDBParamters.Add("@nASBaseID", nOldBaseId, ParameterDirection.Input, SqlDbType.BigInt, 1);
                }
                oDBLayer.Execute("HL7_InsertMessageQueue", oDBParamters);
                oDBLayer.Disconnect();
                oDBLayer.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            finally
            {

            }
        }
        //End of changes by Abhijeet on 20100909 for declaring variables to storing provider details

        //public static bool _IsModify = false;
        // This Insert Data into HL7 Message Queue
        public static void InsertInMessageQueue(string strMessageName, Int64 PatientID, Int64 OtherID, string Field1, string _Connectionstring)
        {
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_Connectionstring);
            try
            {
                gloDatabaseLayer.DBParameters oDBParamters = new gloDatabaseLayer.DBParameters();
                oDBLayer.Connect(false);

                oDBParamters.Add("@dtDatetimeStamp", DateAndTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParamters.Add("@MessageName", strMessageName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@sMachineID", GetMachineID(System.Environment.MachineName, _Connectionstring), ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@sMachinename", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParamters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParamters.Add("@nID", OtherID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParamters.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int, 1);
                oDBParamters.Add("@sField1", Field1, ParameterDirection.Input, SqlDbType.VarChar, 5000);
                oDBParamters.Add("@MachineID", oDBLayer.GetPrefixTransactionID(PatientID), ParameterDirection.Input, SqlDbType.BigInt);

                oDBLayer.Execute("HL7_InsertMessageQueue", oDBParamters);
                oDBLayer.Disconnect();
                oDBLayer.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Disconnect();
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
            }
        }

        //Added by Abhijeet on 20110607
        public static void InsertInMessageQueueforgloLab(string strMessageName, Int64 PatientID, Int64 OtherID, ref Int32 intTotalRecords, ArrayList arrTestName, string _Connectionstring)
        {
            gloDatabaseLayer.DBLayer odb = new gloDatabaseLayer.DBLayer(_Connectionstring);
            gloDatabaseLayer.DBParameters odbParas = new gloDatabaseLayer.DBParameters();
            try
            {
                odb.Connect(false);
                odbParas.Add("@dtDatetimeStamp", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                odbParas.Add("@MessageName", strMessageName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                odbParas.Add("@sMachineID", GetMachineID(System.Environment.MachineName, _Connectionstring), ParameterDirection.Input, SqlDbType.VarChar, 50);
                odbParas.Add("@sMachinename", System.Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar, 50);
                odbParas.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParas.Add("@nID", OtherID, ParameterDirection.Input, SqlDbType.BigInt);
                odbParas.Add("@Status", 1, ParameterDirection.Input, SqlDbType.Int);

                string sFieldVal = "";
                if (arrTestName != null)
                {
                    if (arrTestName.Count > 0)
                    {
                        foreach (object sTestName in arrTestName)
                        {
                            sFieldVal = sFieldVal + sTestName.ToString();
                        }
                    }
                }
                odbParas.Add("@sField1", sFieldVal, ParameterDirection.Input, SqlDbType.VarChar, 5000);
                odbParas.Add("@MachineID", odb.GetPrefixTransactionID(PatientID), ParameterDirection.Input, SqlDbType.BigInt);
                sFieldVal = "";
                if (string.Compare(strMessageName, "A04", true) == 0 || string.Compare(strMessageName, "A08", true) == 0)
                {
                    if (sGLOLABHSILABEL.Length > 0)
                    {
                        sFieldVal = "Queue";
                    }
                    else
                    {
                        sFieldVal = "LabInActive";
                    }
                }
                else
                {
                    sFieldVal = "";
                }
                odbParas.Add("@sField2", sFieldVal, ParameterDirection.Input, SqlDbType.VarChar, 50);

                odb.ExecuteScalar("HL7_InsertMessageQueueGloLab", odbParas);
                intTotalRecords = intTotalRecords + 1;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (odbParas != null)
                {
                    odbParas.Dispose();
                    odbParas = null;
                }
                if (odb != null)

                    odb.Disconnect();
                odb.Dispose();
                odb = null;
            }
        }




        public static void ScanClientInterface(string strProductName, string strMachineName, string _Connectionstring)
        {

            boolSendPatientDetails = false;
            boolSendAppointmentDetails = false;
            gloDatabaseLayer.DBLayer odb = new gloDatabaseLayer.DBLayer(_Connectionstring);
            gloDatabaseLayer.DBParameters odbParas = new gloDatabaseLayer.DBParameters();
            DataSet dsData = new DataSet();
            try
            {
                odb.Connect(false);

                odbParas.Add("@sMachineName", strMachineName, ParameterDirection.Input, SqlDbType.NVarChar);
                odbParas.Add("@sProductName", strProductName, ParameterDirection.Input, SqlDbType.NVarChar);

                odb.Retrive("gsp_ViewClientInterface", odbParas, out dsData);
                int nCount = 0;
                for (nCount = 0; nCount <= dsData.Tables[0].Rows.Count - 1; nCount++)
                {
                    boolSendPatientDetails = Convert.ToBoolean(dsData.Tables[0].Rows[nCount]["bHl7_SendPatientDetails"]);
                    boolSendAppointmentDetails = Convert.ToBoolean(dsData.Tables[0].Rows[nCount]["bHL7_SendAppointmentDetails"]);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (odbParas != null)
                {
                    odbParas.Dispose();
                    odbParas = null;
                }
                if (odb != null)
                {
                    odb.Disconnect();
                    odb.Dispose();
                    odb = null;
                }
            }
        }


        //End of changes by Abhijeet on 20110607

        //Added by Abhijeet on 20110607 for reading registry values for HL7 outbound setting
        public static void HL7OutboundSettings(string _connectionstring)
        {
            // Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\gloEMR");
            try
            {

                //Read individual value for EMR as well as PM
                string _MessageBoxCaption = "";
                if (appSettings["MessageBOXCaption"] != null)
                {
                    if (appSettings["MessageBOXCaption"] != "")
                    {
                        _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                    }
                    else
                    {
                        _MessageBoxCaption = "";
                    }
                }
                else
                { _MessageBoxCaption = ""; }

                if (_MessageBoxCaption == "gloPM")
                {
                    #region "PM"

                    // ScanClientInterface("gloPM", System.Environment.MachineName, _connectionstring);

                    if (appSettings["HL7SENDOUTBOUNDGLOPM"] != null)
                    {
                        if (appSettings["HL7SENDOUTBOUNDGLOPM"] != "")
                        {
                            if ((Convert.ToBoolean(Convert.ToInt16(appSettings["HL7SENDOUTBOUNDGLOPM"])) == true))
                            {
                                boolHL7SENDOUTBOUNDGLOPM = true;
                            }
                            else { boolHL7SENDOUTBOUNDGLOPM = false; }
                        }
                        else
                        { boolHL7SENDOUTBOUNDGLOPM = false; }
                    }
                    else { boolHL7SENDOUTBOUNDGLOPM = false; }


                    if (boolHL7SENDOUTBOUNDGLOPM == true)
                    {
                        if (appSettings["SendPatientDetails"] != null)
                        {
                            if (appSettings["SendPatientDetails"] != "")
                            {
                                if ((Convert.ToBoolean(Convert.ToInt16(appSettings["SendPatientDetails"])) == true))
                                {
                                    boolSendPatientDetails = true;
                                }
                                else { boolSendPatientDetails = false; }
                            }
                            else { boolSendPatientDetails = false; }
                        }
                        else { boolSendPatientDetails = false; }

                        if (appSettings["SendAppointmentDetails"] != null)
                        {
                            if (appSettings["SendAppointmentDetails"] != "")
                            {
                                if ((Convert.ToBoolean(Convert.ToInt16(appSettings["SendAppointmentDetails"])) == true))
                                {
                                    boolSendAppointmentDetails = true;
                                }
                                else { boolSendAppointmentDetails = false; }
                            }
                            else { boolSendAppointmentDetails = false; }
                        }
                        else { boolSendAppointmentDetails = false; }
                    }
                    else
                    {
                        boolSendAppointmentDetails = false;
                        boolSendPatientDetails = false;
                    }

                    //Modified code to set outbound message variables value with new separate setting by Abhijeet on 20110927




                    //if (appSettings["GenerateHL7Message"] != null)
                    //{
                    //    if (appSettings["GenerateHL7Message"] != "")
                    //    {
                    //        boolSendChargesToHL7 = true;
                    //        boolSendPatientDetails = true;
                    //        boolSendAppointmentDetails = true;
                    //    }
                    //}

                    //Commented on 11/29/2012 For HL7 outbound setting from Database
                    //Setting 'HL7' variables
                    //if (appSettings["GenerateHL7Message"] != null)
                    //{
                    //    if (appSettings["GenerateHL7Message"] != "" )
                    //    {
                    //        if ((Convert.ToBoolean(appSettings["GenerateHL7Message"]) == true))
                    //        {
                    //             boolSendChargesToHL7 = true;                               
                    //        }
                    //        else
                    //        {
                    //            boolSendChargesToHL7 = false;                                
                    //        }
                    //    }
                    //    else
                    //    {
                    //        boolSendChargesToHL7 = false;                            
                    //    }
                    //}
                    //else
                    //{
                    //    boolSendChargesToHL7 = false;                        
                    //}


                    //Setting 'Send patient Details' variables
                    //Commented on 11-29-2012 for HL 7 outbound setting came from Database.
                    //if (appSettings["SendPatientDetails"] != null)
                    //{
                    //    if ( appSettings["SendPatientDetails"] != "")
                    //    {
                    //        if ((Convert.ToBoolean(appSettings["SendPatientDetails"]) == true))
                    //        {                               
                    //            boolSendPatientDetails = true;
                    //        }
                    //        else
                    //        {                               
                    //            boolSendPatientDetails = false;
                    //        }
                    //    }
                    //    else
                    //    {                           
                    //        boolSendPatientDetails = false;
                    //    }
                    //}
                    //else
                    //{                       
                    //    boolSendPatientDetails = false;
                    //}

                    ////Setting 'Send Appointment Details' variables
                    //if (appSettings["SendAppointmentDetails"] != null)
                    //{
                    //    if (appSettings["SendAppointmentDetails"] != "")
                    //    {
                    //        if ((Convert.ToBoolean(appSettings["SendAppointmentDetails"]) == true))
                    //        {                               
                    //            boolSendAppointmentDetails = true;
                    //        }
                    //        else
                    //        {                               
                    //            boolSendAppointmentDetails = false;
                    //        }
                    //    }
                    //    else
                    //    {                           
                    //        boolSendAppointmentDetails = false;
                    //    }
                    //}
                    //else
                    //{                       
                    //    boolSendAppointmentDetails = false;
                    //}
                    #endregion "PM"
                }
                else if (_MessageBoxCaption == "gloEMR")
                {
                    #region "EMR"


                    if (appSettings["HL7SENDOUTBOUNDGLOEMR"] != null)
                    {
                        if (appSettings["HL7SENDOUTBOUNDGLOEMR"] != "")
                        {
                            if ((Convert.ToBoolean(Convert.ToInt16(appSettings["HL7SENDOUTBOUNDGLOEMR"])) == true))
                            {
                                boolHL7SENDOUTBOUNDGLOEMR = true;
                            }
                            else
                            { boolHL7SENDOUTBOUNDGLOEMR = false; }
                        }
                        else { boolHL7SENDOUTBOUNDGLOEMR = false; }
                    }
                    else { boolHL7SENDOUTBOUNDGLOEMR = false; }


                    if (boolHL7SENDOUTBOUNDGLOEMR == true)
                    {
                        if (appSettings["SendPatientDetails"] != null)
                        {
                            if (appSettings["SendPatientDetails"] != "")
                            {
                                if ((Convert.ToBoolean(Convert.ToInt16(appSettings["SendPatientDetails"])) == true))
                                {
                                    boolSendPatientDetails = true;
                                }
                                else { boolSendPatientDetails = false; }
                            }
                            else { boolSendPatientDetails = false; }
                        }
                        else { boolSendPatientDetails = false; }

                        if (appSettings["SendAppointmentDetails"] != null)
                        {
                            if (appSettings["SendAppointmentDetails"] != "")
                            {
                                if ((Convert.ToBoolean(Convert.ToInt16(appSettings["SendAppointmentDetails"])) == true))
                                {
                                    boolSendAppointmentDetails = true;
                                }
                                else { boolSendAppointmentDetails = false; }
                            }
                            else { boolSendAppointmentDetails = false; }
                        }
                        else { boolSendAppointmentDetails = false; }
                    }
                    else
                    {
                        boolSendAppointmentDetails = false;
                        boolSendPatientDetails = false;
                    }

                    //if (gloSettings.gloRegistrySetting.OpenSubKey(gloSettings.gloRegistrySetting.gstrSoftEMR) == false)
                    //{
                    //    return;
                    //}

                    //gloSettings.gloRegistrySetting.OpenSubKey(gloSettings.gloRegistrySetting.gstrSoftEMR, true);

                    //if (gloSettings.gloRegistrySetting.GetRegistryValue("SendChargesToHL7") == null)
                    //{
                    //    boolSendChargesToHL7 = false;
                    //}
                    //else
                    //{
                    //    boolSendChargesToHL7 = Convert.ToBoolean(gloSettings.gloRegistrySetting.GetRegistryValue("SendChargesToHL7"));
                    //}
                    //if (gloSettings.gloRegistrySetting.GetRegistryValue("SendPatientDetails") == null)
                    //{
                    //    boolSendPatientDetails = false;
                    //}
                    //else
                    //{
                    //    boolSendPatientDetails = Convert.ToBoolean(gloSettings.gloRegistrySetting.GetRegistryValue("SendPatientDetails"));
                    //}
                    //if (gloSettings.gloRegistrySetting.GetRegistryValue("SendAppointmentDetails") == null)
                    //{
                    //    boolSendAppointmentDetails = false;
                    //}
                    //else
                    //{                        

                    //    boolSendAppointmentDetails = Convert.ToBoolean(gloSettings.gloRegistrySetting.GetRegistryValue("SendAppointmentDetails"));
                    //}
                    //gloSettings.gloRegistrySetting.CloseRegistryKey();
                    #endregion "EMR"
                }
                else
                {
                    boolHL7SENDOUTBOUNDGLOEMR = false;
                    boolHL7SENDOUTBOUNDGLOPM = false;
                    boolSendPatientDetails = false;
                    boolSendAppointmentDetails = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {



            }
        }
        //End of changes by Abhijeet on 20110607 for reading registry values for HL7 outbound setting 

        public static void GetgloEMRSettings(string sConnectionstring)
        {
            gloDatabaseLayer.DBLayer odb = new gloDatabaseLayer.DBLayer(sConnectionstring);
            try
            {
                string sQuery = "select isnull(sSettingsValue,'') from settings where sSettingsName='GLOLAB HSI LABEL'";
                object obj;
                odb.Connect(false);
                obj = odb.ExecuteScalar_Query(sQuery);
                if (obj != null)
                {
                    sGLOLABHSILABEL = Convert.ToString(obj).Trim();
                }
                else
                {
                    sGLOLABHSILABEL = string.Empty;
                }
                odb.Disconnect();
                sQuery = null;
                obj = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (odb != null)
                {
                    odb.Dispose();
                    odb = null;
                }
            }
        }

        //Function Added by Abhijeet on 20110607 for getting MachineID 
        private static Int64 GetMachineID(string sMachineName, string _sConnString)
        {
            Int64 nMachineID = 1;
            gloDatabaseLayer.DBLayer odb = new gloDatabaseLayer.DBLayer(_sConnString);
            string sQuery = string.Empty;

            object _objVal = null;
            try
            {
                sQuery = " Select nClientID from ClientSettings_MST where sMachineName='" + sMachineName.Trim().Replace("'", "''") + "' ";

                odb.Connect(false);

                // SP checking machine rights 
                // Select nClientID from ClientSettings_MST where sMachineName=@MachineName   
                // AND ISNULL(sProductCode,'1')IN('1','9','10','12','15','11')  
                _objVal = odb.ExecuteScalar_Query(sQuery);
                if (_objVal != null && Convert.ToString(_objVal).Length > 0)
                {
                    nMachineID = Convert.ToInt64(_objVal);
                }

                odb.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (odb != null)
                {
                    odb.Disconnect();
                    odb.Dispose();
                    odb = null;
                }

                _objVal = null;

            }
            return nMachineID;
        }

        //End of changes by Abhijeet on 20110607 for getting MachineID

    }
    // Code End - Added by kanchan on 20091205 for HL7 appointment outbound

}
