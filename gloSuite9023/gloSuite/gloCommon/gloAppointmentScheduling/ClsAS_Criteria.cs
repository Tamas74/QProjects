using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace gloAppointmentScheduling
{
    namespace Criteria
    {

        public class FindRecurrences : IDisposable
        {
            #region "Constructor & Distructor"


            public FindRecurrences()
            {
                _RecurrenceDetail = new gloCriteria();
                _Dates = new ArrayList();
                _BlockedDates = new ArrayList();
                _ScheduleStatus = new ArrayList();
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
                        _RecurrenceDetail.Dispose();
                        _RecurrenceDetail = null;
                        _Dates = null;
                    }
                }
                disposed = true;
            }

            ~FindRecurrences()
            {
                Dispose(false);
            }

            #endregion

            #region "Property & Procedures"
            private gloCriteria _RecurrenceDetail;
            private Int64 _NoOfOccurencesValue = 0;
            private DateTime _EndDateValue = DateTime.Now.Date;
            private ArrayList _Dates;
            private ArrayList _BlockedDates;
            private ArrayList _ScheduleStatus;

            public gloCriteria RecurrenceDetail
            {
                get { return _RecurrenceDetail; }
                set { _RecurrenceDetail = value; }
            }

            public Int64 NoOfOccurrences
            {
                get { return _NoOfOccurencesValue; }
            }

            public DateTime EndDate
            {
                get { return _EndDateValue; }
            }

            public ArrayList Dates
            {
                get { return _Dates; }
            }

            public ArrayList BlockedDates
            {
                get { return _BlockedDates; }
            }

            public ArrayList ScheduleStatus
            {
                get { return _ScheduleStatus; }
            }

            #endregion

            /// <summary>
            /// Find recurrence according to criteria selected
            /// </summary>
            /// <returns>Date collection</returns>
            public bool FindRecurrence()
            {
                //    _FindWithDates = false;
                bool _result = false;
                try
                {
                    //Daily
                    if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Daily)
                    {
                        CreateDailyRecurrence(Convert.ToDateTime(DateTime.Today.ToShortDateString()), Convert.ToDateTime(DateTime.Today.ToShortDateString()));
                        _result = true;
                    }
                    //Weekly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Weekly)
                    {
                        CreateWeeklyRecurrence(Convert.ToDateTime(DateTime.Today.ToShortDateString()), Convert.ToDateTime(DateTime.Today.ToShortDateString()));
                        _result = true;
                    }
                    //Monthly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Monthly)
                    {
                        CreateMonthlyRecurrence(Convert.ToDateTime(DateTime.Today.ToShortDateString()), Convert.ToDateTime(DateTime.Today.ToShortDateString()));
                        _result = true;
                    }
                    //Yearly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Yearly)
                    {
                        CreateYearlyRecurrence(Convert.ToDateTime(DateTime.Today.ToShortDateString()), Convert.ToDateTime(DateTime.Today.ToShortDateString()));
                        _result = true;
                    }
                }
                catch (Exception)
                {
                    _result = false;
                }
                return _result;

            }

            /// <summary>
            /// Find recurrence in gloEMR Schedule By Pranit on 8 sep 2011 added one more parameter masterAppointmentID
            /// </summary>
            /// <param name="ProviderID"></param>
            /// <param name="StartTime"></param>
            /// <param name="EndTime"></param>
            /// <param name="MasterAppointmentID"></param>
            /// <returns></returns>
            /// 
            public bool FindRecurrence(Int64 ProviderID, DateTime StartTime, DateTime EndTime, Int64 MasterAppointmentID)
            {
                //    _FindWithDates = false;
                bool _result = false;
                try
                {
                    //Daily
                    if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Daily)
                    {
                        CreateDailyRecurrence(StartTime, EndTime);
                        _result = true;
                    }
                    //Weekly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Weekly)
                    {
                        CreateWeeklyRecurrence(StartTime, EndTime);
                        _result = true;
                    }
                    //Monthly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Monthly)
                    {
                        CreateMonthlyRecurrence(StartTime, EndTime);
                        _result = true;
                    }
                    //Yearly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Yearly)
                    {
                        CreateYearlyRecurrence(StartTime, EndTime);
                        _result = true;
                    }

                    if (_result == true)
                    {
                        if (ProviderID > 0)
                            RemoveBlockedSlots(ProviderID, StartTime, EndTime, MasterAppointmentID);
                    }
                }
                catch (Exception)
                {
                    _result = false;
                }
                return _result;

            }

            // For Resource Base 
            public bool FindRecurrence(Int64 ProviderID, DateTime StartTime, DateTime EndTime, Int64 MasterAppointmentID, Int64 ResourceID)
            {
                //    _FindWithDates = false;
                bool _result = false;
                try
                {
                    //Daily
                    if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Daily)
                    {
                        CreateDailyRecurrence(StartTime, EndTime);
                        _result = true;
                    }
                    //Weekly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Weekly)
                    {
                        CreateWeeklyRecurrence(StartTime, EndTime);
                        _result = true;
                    }
                    //Monthly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Monthly)
                    {
                        CreateMonthlyRecurrence(StartTime, EndTime);
                        _result = true;
                    }
                    //Yearly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Yearly)
                    {
                        CreateYearlyRecurrence(StartTime, EndTime);
                        _result = true;
                    }

                    if (_result == true)
                    {
                        if (ProviderID > 0)
                            RemoveBlockedSlots(ProviderID, StartTime, EndTime, MasterAppointmentID);


                    }
                }
                catch (Exception)
                {
                    _result = false;
                }
                return _result;

            }


            // For Multiple Resource Base 
            public bool FindRecurrence(Int64 ProviderID, DateTime StartTime, DateTime EndTime, Int64 MasterAppointmentID, StringBuilder ResourceID)
            {
                //    _FindWithDates = false;
                bool _result = false;
                try
                {
                    //Daily
                    if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Daily)
                    {
                        CreateDailyRecurrence(StartTime, EndTime);
                        _result = true;
                    }
                    //Weekly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Weekly)
                    {
                        CreateWeeklyRecurrence(StartTime, EndTime);
                        _result = true;
                    }
                    //Monthly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Monthly)
                    {
                        CreateMonthlyRecurrence(StartTime, EndTime);
                        _result = true;
                    }
                    //Yearly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Yearly)
                    {
                        CreateYearlyRecurrence(StartTime, EndTime);
                        _result = true;
                    }

                    if (_result == true)
                    {
                        if ((ProviderID > 0) && (string.IsNullOrEmpty(ResourceID.ToString())))
                        {
                            RemoveBlockedSlots(ProviderID, StartTime, EndTime, MasterAppointmentID);
                        }
                        if ((!string.IsNullOrEmpty(ResourceID.ToString())) && (ProviderID == 0))
                        {
                            RemoveResourceBlockedSlots(ResourceID.ToString(), StartTime, EndTime, MasterAppointmentID);
                        }
                        if ((!string.IsNullOrEmpty(ResourceID.ToString())) && (ProviderID > 0))
                        {
                            RemoveProviderResourceBlockedSlots(ProviderID, ResourceID.ToString(), StartTime, EndTime, MasterAppointmentID);
                        }


                        // By Pranit on 23 Sep 2011 to fill default values for Schedule Status array
                        if ((string.IsNullOrEmpty(ResourceID.ToString())) && (ProviderID == 0))
                        {
                            RemoveBlockedSlotsWithoutProviderAndResource(ProviderID, StartTime, EndTime, MasterAppointmentID);
                        }




                    }
                }
                catch (Exception)
                {
                    _result = false;
                }
                return _result;

            }

            /// <summary>
            /// Find recurrence in gloPM Schedule
            /// </summary>
            /// <param name="StartTime">Schedule Start Time</param>
            /// <param name="EndTime">Schedule End Time</param>
            /// <returns>Date collection</returns>
            public bool FindRecurrence(Int64 ProviderID, DateTime StartTime, DateTime EndTime)
            {
                //    _FindWithDates = false;
                bool _result = false;
                try
                {
                    //Daily
                    if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Daily)
                    {
                        CreateDailyRecurrence(StartTime, EndTime);
                        _result = true;
                    }
                    //Weekly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Weekly)
                    {
                        CreateWeeklyRecurrence(StartTime, EndTime);
                        _result = true;
                    }
                    //Monthly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Monthly)
                    {
                        CreateMonthlyRecurrence(StartTime, EndTime);
                        _result = true;
                    }
                    //Yearly
                    else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType == RecurrencePatternType.Yearly)
                    {
                        CreateYearlyRecurrence(StartTime, EndTime);
                        _result = true;
                    }

                    if (_result == true)
                    {
                        if (ProviderID > 0)
                            RemoveBlockedSlots(ProviderID, StartTime, EndTime);
                    }
                }
                catch (Exception)
                {
                    _result = false;
                }
                return _result;

            }


            #region "Search Recurrence in Various Patterns"
            private void CreateDailyRecurrence(DateTime StartTime, DateTime EndTime)
            {
                DateTime _StartDate = gloDateMaster.gloDate.DateAsDate(_RecurrenceDetail.RecurrenceCriteria.Range.StartDate);
                DateTime _EndDate = gloDateMaster.gloDate.DateAsDate(_RecurrenceDetail.RecurrenceCriteria.Range.EndDate);
                Int64 _NoOfOccurences = _RecurrenceDetail.RecurrenceCriteria.Range.EndOccurrenceNumber;
                Int64 _EveryDayValue = _RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber;

                bool _ByEndDate = false;
                DateTime _IntEndDate = _StartDate;
                int i;

                _Dates.Clear();

                if (_RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndAfterOccurence)
                {
                    _ByEndDate = false;
                }
                else if (_RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndDate)
                {
                    _ByEndDate = true;
                    _NoOfOccurences = 0;
                }
                //Every Day
                if (_RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay == RecurrencePatternFlag.EveryDay)
                {
                    if (_ByEndDate == false)
                    {
                        //_EndDate = _StartDate.AddDays(Convert.ToDouble((_NoOfOccurences - 1) * _EveryDayValue));
                        i = 0;
                        while (i < _NoOfOccurences)
                        {
                            //if (_IntEndDate.DayOfWeek.ToString().ToUpper() != "SUNDAY" && _IntEndDate.DayOfWeek.ToString().ToUpper() != "SATURDAY")
                            //{
                            i++;
                            if (i == _NoOfOccurences)
                            {
                                _EndDate = _IntEndDate;
                            }
                            //}
                            _Dates.Add(_IntEndDate);
                            if (_NoOfOccurences == 1)
                            {
                                if (Convert.ToDateTime(StartTime.ToShortTimeString()) > Convert.ToDateTime(EndTime.ToShortTimeString()))
                                {
                                    break;
                                }
                                //
                            }
                            _IntEndDate = _IntEndDate.AddDays(_EveryDayValue);
                        }
                    }
                    else
                    {
                        while (_IntEndDate <= _EndDate)
                        {
                            if (_IntEndDate <= _EndDate)
                            {
                                _NoOfOccurences = _NoOfOccurences + 1;
                                _Dates.Add(_IntEndDate);
                            }

                            if (Convert.ToDateTime(StartTime.ToShortTimeString()) > Convert.ToDateTime(EndTime.ToShortTimeString()))
                            {
                                break;
                            }

                            // _IntEndDate = _IntEndDate.AddDays(_EveryDayValue);
                            _IntEndDate = _IntEndDate.AddDays(1);
                            if (_NoOfOccurences == 999) { break; }
                        }
                    }
                }
                // Every Week Day
                else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay == RecurrencePatternFlag.EveryWeekday)
                {
                    if (_ByEndDate == false)
                    {
                        i = 0;
                        while (i < _NoOfOccurences)
                        {
                            if (_IntEndDate.DayOfWeek.ToString().ToUpper() != "SUNDAY" && _IntEndDate.DayOfWeek.ToString().ToUpper() != "SATURDAY")
                            {
                                i++;
                                if (i == _NoOfOccurences)
                                {
                                    _EndDate = _IntEndDate;
                                }
                            }
                            _Dates.Add(_IntEndDate);
                            _IntEndDate = _IntEndDate.AddDays(1);
                        }
                    }
                    else
                    {
                        while (_IntEndDate <= _EndDate)
                        {
                            if (_IntEndDate <= _EndDate)
                            {
                                if (_IntEndDate.DayOfWeek.ToString().ToUpper() != "SUNDAY" && _IntEndDate.DayOfWeek.ToString().ToUpper() != "SATURDAY")
                                {
                                    _NoOfOccurences = _NoOfOccurences + 1;
                                    _Dates.Add(_IntEndDate);

                                }
                            }
                            _IntEndDate = _IntEndDate.AddDays(1);
                            if (_NoOfOccurences == 999) { break; }
                        }
                    }
                }

                // Result
                _EndDateValue = _EndDate;
                _NoOfOccurencesValue = _NoOfOccurences;
            }

            private void CreateWeeklyRecurrence(DateTime StartTime, DateTime EndTime)
            {
                DateTime _StartDate = gloDateMaster.gloDate.DateAsDate(_RecurrenceDetail.RecurrenceCriteria.Range.StartDate);
                DateTime _EndDate = gloDateMaster.gloDate.DateAsDate(_RecurrenceDetail.RecurrenceCriteria.Range.EndDate);
                Int64 _NoOfOccurences = _RecurrenceDetail.RecurrenceCriteria.Range.EndOccurrenceNumber;
                Int64 _EveryWeekValue = _RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber;

                bool _ByEndDate = false;
                DateTime _IntEndDate = _StartDate;
                int i;

                _Dates.Clear();

                if (_RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndAfterOccurence)
                {
                    _ByEndDate = false;
                }
                else if (_RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndDate)
                {
                    _ByEndDate = true;
                    _NoOfOccurences = 0;
                }


                if (_ByEndDate == false)
                {
                    i = 0;
                    int _TotSunday = 0;
                    int _TotMonday = 0;
                    int _TotTuesday = 0;
                    int _TotWednesday = 0;
                    int _TotThursday = 0;
                    int _TotFriday = 0;
                    int _TotSaturday = 0;

                    while (i < _NoOfOccurences)
                    {
                        //SUNDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "SUNDAY")
                        {
                            _TotSunday = _TotSunday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday == true)
                            {
                                if (_TotSunday == 1)
                                {
                                    if (_TotSaturday < 1)
                                    {
                                        i++;
                                        _Dates.Add(_IntEndDate);
                                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    }
                                }
                                else
                                {
                                    if ((_TotSunday - 1) % _EveryWeekValue == 0)
                                    {
                                        i++;
                                        _Dates.Add(_IntEndDate);
                                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    }
                                }
                            }
                        }


                        //MONDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "MONDAY")
                        {
                            _TotMonday = _TotMonday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Monday == true)
                            {
                                if (_TotMonday == 1)
                                {
                                    if (_TotSaturday < 1)
                                    {
                                        i++;
                                        _Dates.Add(_IntEndDate);
                                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    }
                                }
                                else
                                {
                                    if ((_TotMonday - 1) % _EveryWeekValue == 0)
                                    {
                                        i++;
                                        _Dates.Add(_IntEndDate);
                                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    }
                                }
                            }
                        }

                        //TUESDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "TUESDAY")
                        {
                            _TotTuesday = _TotTuesday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday == true)
                            {
                                if (_TotTuesday == 1)
                                {
                                    if (_TotSaturday < 1)
                                    {
                                        i++;
                                        _Dates.Add(_IntEndDate);
                                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    }
                                }
                                else
                                {
                                    if ((_TotTuesday - 1) % _EveryWeekValue == 0)
                                    {
                                        i++;
                                        _Dates.Add(_IntEndDate);
                                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    }
                                }
                            }
                        }

                        //WEDNESDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "WEDNESDAY")
                        {
                            _TotWednesday = _TotWednesday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday == true)
                            {
                                if (_TotWednesday == 1)
                                {
                                    if (_TotSaturday < 1)
                                    {
                                        i++;
                                        _Dates.Add(_IntEndDate);
                                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    }
                                }
                                else
                                {
                                    if ((_TotWednesday - 1) % _EveryWeekValue == 0)
                                    {
                                        i++;
                                        _Dates.Add(_IntEndDate);
                                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    }
                                }
                            }
                        }

                        //THURSDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "THURSDAY")
                        {
                            _TotThursday = _TotThursday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday == true)
                            {
                                if (_TotThursday == 1)
                                {
                                    if (_TotSaturday < 1)
                                    {
                                        i++;
                                        _Dates.Add(_IntEndDate);
                                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    }
                                }
                                else
                                {
                                    if ((_TotThursday - 1) % _EveryWeekValue == 0)
                                    {
                                        i++;
                                        _Dates.Add(_IntEndDate);
                                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    }
                                }
                            }
                        }

                        //FRIDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "FRIDAY")
                        {
                            _TotFriday = _TotFriday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Friday == true)
                            {
                                if (_TotFriday == 1)
                                {
                                    if (_TotSaturday < 1)
                                    {
                                        i++;
                                        _Dates.Add(_IntEndDate);
                                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    }
                                }
                                else
                                {
                                    if ((_TotFriday - 1) % _EveryWeekValue == 0)
                                    {
                                        i++;
                                        _Dates.Add(_IntEndDate);
                                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    }
                                }
                            }
                        }

                        //SATURDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "SATURDAY")
                        {
                            _TotSaturday = _TotSaturday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday == true)
                            {
                                if (_TotSaturday == 1)
                                {
                                    i++;
                                    _Dates.Add(_IntEndDate);
                                    if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                }
                                else
                                {
                                    if ((_TotSaturday - 1) % _EveryWeekValue == 0)
                                    {
                                        i++;
                                        _Dates.Add(_IntEndDate);
                                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    }
                                }
                            }

                            //For Occurence and Week Finish Identification
                            if (_TotSunday == 0) { _TotSunday = 1; }
                            if (_TotMonday == 0) { _TotMonday = 1; }
                            if (_TotTuesday == 0) { _TotTuesday = 1; }
                            if (_TotWednesday == 0) { _TotWednesday = 1; }
                            if (_TotThursday == 0) { _TotThursday = 1; }
                            if (_TotFriday == 0) { _TotFriday = 1; }
                            if (_TotSaturday == 0) { _TotSaturday = 1; }
                        }

                        _IntEndDate = _IntEndDate.AddDays(1);

                    }
                }
                else
                {
                    i = 0;
                    int _TotSunday = 0;
                    int _TotMonday = 0;
                    int _TotTuesday = 0;
                    int _TotWednesday = 0;
                    int _TotThursday = 0;
                    int _TotFriday = 0;
                    int _TotSaturday = 0;

                    while (_IntEndDate <= _EndDate)
                    {
                        //SUNDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "SUNDAY")
                        {
                            _TotSunday = _TotSunday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday == true)
                            {
                                if (_TotSunday == 1)
                                {
                                    if (_TotSaturday < 1)
                                    {
                                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                    }
                                }
                                else
                                {
                                    if ((_TotSunday - 1) % _EveryWeekValue == 0)
                                    {
                                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                    }
                                }
                            }
                        }


                        //MONDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "MONDAY")
                        {
                            _TotMonday = _TotMonday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Monday == true)
                            {
                                if (_TotMonday == 1)
                                {
                                    if (_TotSaturday < 1)
                                    {
                                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                    }
                                }
                                else
                                {
                                    if ((_TotMonday - 1) % _EveryWeekValue == 0)
                                    {
                                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                    }
                                }
                            }
                        }

                        //TUESDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "TUESDAY")
                        {
                            _TotTuesday = _TotTuesday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday == true)
                            {
                                if (_TotTuesday == 1)
                                {
                                    if (_TotSaturday < 1)
                                    {
                                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                    }
                                }
                                else
                                {
                                    if ((_TotTuesday - 1) % _EveryWeekValue == 0)
                                    {
                                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                    }
                                }
                            }
                        }

                        //WEDNESDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "WEDNESDAY")
                        {
                            _TotWednesday = _TotWednesday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday == true)
                            {
                                if (_TotWednesday == 1)
                                {
                                    if (_TotSaturday < 1)
                                    {
                                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                    }
                                }
                                else
                                {
                                    if ((_TotWednesday - 1) % _EveryWeekValue == 0)
                                    {
                                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                    }
                                }
                            }
                        }

                        //THURSDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "THURSDAY")
                        {
                            _TotThursday = _TotThursday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday == true)
                            {
                                if (_TotThursday == 1)
                                {
                                    if (_TotSaturday < 1)
                                    {
                                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                    }
                                }
                                else
                                {
                                    if ((_TotThursday - 1) % _EveryWeekValue == 0)
                                    {
                                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                    }
                                }
                            }
                        }

                        //FRIDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "FRIDAY")
                        {
                            _TotFriday = _TotFriday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Friday == true)
                            {
                                if (_TotFriday == 1)
                                {
                                    if (_TotSaturday < 1)
                                    {
                                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                    }
                                }
                                else
                                {
                                    if ((_TotFriday - 1) % _EveryWeekValue == 0)
                                    {
                                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                    }
                                }
                            }
                        }

                        //SATURDAY
                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "SATURDAY")
                        {
                            _TotSaturday = _TotSaturday + 1;
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday == true)
                            {
                                if (_TotSaturday == 1)
                                {
                                    _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                    //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                    _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                }
                                else
                                {
                                    if ((_TotSaturday - 1) % _EveryWeekValue == 0)
                                    {
                                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
                                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
                                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
                                    }
                                }
                            }

                            //For Occurence and Week Finish Identification
                            if (_TotSunday == 0) { _TotSunday = 1; }
                            if (_TotMonday == 0) { _TotMonday = 1; }
                            if (_TotTuesday == 0) { _TotTuesday = 1; }
                            if (_TotWednesday == 0) { _TotWednesday = 1; }
                            if (_TotThursday == 0) { _TotThursday = 1; }
                            if (_TotFriday == 0) { _TotFriday = 1; }
                            if (_TotSaturday == 0) { _TotSaturday = 1; }
                        }

                        _IntEndDate = _IntEndDate.AddDays(1);
                    }
                }

                // Result
                _EndDateValue = _EndDate;
                _NoOfOccurencesValue = _NoOfOccurences;
            }

            //private void CreateWeeklyRecurrence()
            //{
            //    DateTime _StartDate = gloDateMaster.gloDate.DateAsDate(_RecurrenceDetail.RecurrenceCriteria.RecurrenceRange.StartDate);
            //    DateTime _EndDate = gloDateMaster.gloDate.DateAsDate(_RecurrenceDetail.RecurrenceCriteria.RecurrenceRange.EndDate);
            //    Int64 _NoOfOccurences = _RecurrenceDetail.RecurrenceCriteria.RecurrenceRange.EndOccurrenceNumber;
            //    Int64 _EveryWeekValue = _RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.EveryWeekNumber;

            //    bool _ByEndDate = false;
            //    DateTime _IntEndDate = _StartDate;
            //    int i;

            //    // By Mahesh 20080121 To Clear the Arraylist
            //    _Dates.Clear();
            //    //

            //    if (_RecurrenceDetail.RecurrenceCriteria.RecurrenceRange.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndAfterOccurence)
            //    {
            //        _ByEndDate = false;
            //    }
            //    else if (_RecurrenceDetail.RecurrenceCriteria.RecurrenceRange.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndDate)
            //    {
            //        _ByEndDate = true;
            //        _NoOfOccurences = 0;
            //    }


            //    if (_ByEndDate == false)
            //    {
            //        i = 0;
            //        int _TotSunday = 0;
            //        int _TotMonday = 0;
            //        int _TotTuesday = 0;
            //        int _TotWednesday = 0;
            //        int _TotThursday = 0;
            //        int _TotFriday = 0;
            //        int _TotSaturday = 0;

            //        while (i < _NoOfOccurences)
            //        {
            //            //SUNDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "SUNDAY")
            //            {
            //                _TotSunday = _TotSunday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Sunday == true)
            //                {
            //                    if (_TotSunday == 1)
            //                    {
            //                        if (_TotSaturday < 1)
            //                        {
            //                            i++;
            //                            _Dates.Add(_IntEndDate);
            //                            if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotSunday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            i++;
            //                            _Dates.Add(_IntEndDate);
            //                            if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        }
            //                    }
            //                }
            //            }


            //            //MONDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "MONDAY")
            //            {
            //                _TotMonday = _TotMonday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Monday == true)
            //                {
            //                    if (_TotMonday == 1)
            //                    {
            //                        if (_TotSaturday < 1)
            //                        {
            //                            i++;
            //                            _Dates.Add(_IntEndDate);
            //                            if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotMonday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            i++;
            //                            _Dates.Add(_IntEndDate);
            //                            if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        }
            //                    }
            //                }
            //            }

            //            //TUESDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "TUESDAY")
            //            {
            //                _TotTuesday = _TotTuesday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Tuesday == true)
            //                {
            //                    if (_TotTuesday == 1)
            //                    {
            //                        if (_TotSaturday < 1)
            //                        {
            //                            i++;
            //                            _Dates.Add(_IntEndDate);
            //                            if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotTuesday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            i++;
            //                            _Dates.Add(_IntEndDate);
            //                            if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        }
            //                    }
            //                }
            //            }

            //            //WEDNESDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "WEDNESDAY")
            //            {
            //                _TotWednesday = _TotWednesday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Wednesday == true)
            //                {
            //                    if (_TotWednesday == 1)
            //                    {
            //                        if (_TotSaturday < 1)
            //                        {
            //                            i++;
            //                            _Dates.Add(_IntEndDate);
            //                            if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotWednesday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            i++;
            //                            _Dates.Add(_IntEndDate);
            //                            if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        }
            //                    }
            //                }
            //            }

            //            //THURSDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "THURSDAY")
            //            {
            //                _TotThursday = _TotThursday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Thursday == true)
            //                {
            //                    if (_TotThursday == 1)
            //                    {
            //                        if (_TotSaturday < 1)
            //                        {
            //                            i++;
            //                            _Dates.Add(_IntEndDate);
            //                            if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotThursday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            i++;
            //                            _Dates.Add(_IntEndDate);
            //                            if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        }
            //                    }
            //                }
            //            }

            //            //FRIDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "FRIDAY")
            //            {
            //                _TotFriday = _TotFriday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Friday == true)
            //                {
            //                    if (_TotFriday == 1)
            //                    {
            //                        if (_TotSaturday < 1)
            //                        {
            //                            i++;
            //                            _Dates.Add(_IntEndDate);
            //                            if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotFriday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            i++;
            //                            _Dates.Add(_IntEndDate);
            //                            if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        }
            //                    }
            //                }
            //            }

            //            //SATURDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "SATURDAY")
            //            {
            //                _TotSaturday = _TotSaturday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Saturday == true)
            //                {
            //                    if (_TotSaturday == 1)
            //                    {
            //                        i++;
            //                        _Dates.Add(_IntEndDate);
            //                        if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotSaturday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            i++;
            //                            _Dates.Add(_IntEndDate);
            //                            if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        }
            //                    }
            //                }

            //                //For Occurence and Week Finish Identification
            //                if (_TotSunday == 0) { _TotSunday = 1; }
            //                if (_TotMonday == 0) { _TotMonday = 1; }
            //                if (_TotTuesday == 0) { _TotTuesday = 1; }
            //                if (_TotWednesday == 0) { _TotWednesday = 1; }
            //                if (_TotThursday == 0) { _TotThursday = 1; }
            //                if (_TotFriday == 0) { _TotFriday = 1; }
            //                if (_TotSaturday == 0) { _TotSaturday = 1; }
            //            }

            //            _IntEndDate = _IntEndDate.AddDays(1);

            //        }
            //    }
            //    else
            //    {
            //        i = 0;
            //        int _TotSunday = 0;
            //        int _TotMonday = 0;
            //        int _TotTuesday = 0;
            //        int _TotWednesday = 0;
            //        int _TotThursday = 0;
            //        int _TotFriday = 0;
            //        int _TotSaturday = 0;

            //        while (_IntEndDate <= _EndDate)
            //        {
            //            //SUNDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "SUNDAY")
            //            {
            //                _TotSunday = _TotSunday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Sunday == true)
            //                {
            //                    if (_TotSunday == 1)
            //                    {
            //                        if (_TotSaturday < 1)
            //                        {
            //                            _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                            //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                            _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotSunday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                            //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                            _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                        }
            //                    }
            //                }
            //            }


            //            //MONDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "MONDAY")
            //            {
            //                _TotMonday = _TotMonday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Monday == true)
            //                {
            //                    if (_TotMonday == 1)
            //                    {
            //                        if (_TotSaturday < 1)
            //                        {
            //                            _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                            //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                            _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotMonday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                            //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                            _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                        }
            //                    }
            //                }
            //            }

            //            //TUESDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "TUESDAY")
            //            {
            //                _TotTuesday = _TotTuesday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Tuesday == true)
            //                {
            //                    if (_TotTuesday == 1)
            //                    {
            //                        if (_TotSaturday < 1)
            //                        {
            //                            _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                            //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                            _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotTuesday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                            //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                            _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                        }
            //                    }
            //                }
            //            }

            //            //WEDNESDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "WEDNESDAY")
            //            {
            //                _TotWednesday = _TotWednesday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Wednesday == true)
            //                {
            //                    if (_TotWednesday == 1)
            //                    {
            //                        if (_TotSaturday < 1)
            //                        {
            //                            _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                            //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                            _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotWednesday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                            //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                            _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                        }
            //                    }
            //                }
            //            }

            //            //THURSDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "THURSDAY")
            //            {
            //                _TotThursday = _TotThursday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Thursday == true)
            //                {
            //                    if (_TotThursday == 1)
            //                    {
            //                        if (_TotSaturday < 1)
            //                        {
            //                            _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                            //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                            _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotThursday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                            //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                            _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                        }
            //                    }
            //                }
            //            }

            //            //FRIDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "FRIDAY")
            //            {
            //                _TotFriday = _TotFriday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Friday == true)
            //                {
            //                    if (_TotFriday == 1)
            //                    {
            //                        if (_TotSaturday < 1)
            //                        {
            //                            _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                            //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                            _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotFriday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                            //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                            _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                        }
            //                    }
            //                }
            //            }

            //            //SATURDAY
            //            if (_IntEndDate.DayOfWeek.ToString().ToUpper() == "SATURDAY")
            //            {
            //                _TotSaturday = _TotSaturday + 1;
            //                if (_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.WeeklyPattern.Saturday == true)
            //                {
            //                    if (_TotSaturday == 1)
            //                    {
            //                        _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                        //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                        _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                    }
            //                    else
            //                    {
            //                        if ((_TotSaturday - 1) % _EveryWeekValue == 0)
            //                        {
            //                            _NoOfOccurences = _NoOfOccurences + 1; //i++;
            //                            //if (i == _NoOfOccurences) { _EndDate = _IntEndDate; }
            //                            _Dates.Add(_IntEndDate); if (_NoOfOccurences == 999) { break; }
            //                        }
            //                    }
            //                }

            //                //For Occurence and Week Finish Identification
            //                if (_TotSunday == 0) { _TotSunday = 1; }
            //                if (_TotMonday == 0) { _TotMonday = 1; }
            //                if (_TotTuesday == 0) { _TotTuesday = 1; }
            //                if (_TotWednesday == 0) { _TotWednesday = 1; }
            //                if (_TotThursday == 0) { _TotThursday = 1; }
            //                if (_TotFriday == 0) { _TotFriday = 1; }
            //                if (_TotSaturday == 0) { _TotSaturday = 1; }
            //            }

            //            _IntEndDate = _IntEndDate.AddDays(1);
            //        }
            //    }

            //    // Result
            //    _EndDateValue = _EndDate;
            //    _NoOfOccurencesValue = _NoOfOccurences;
            //}

            //private void CreateMonthlyRecurrence()
            //{
            //    DateTime _StartDate = dtpRec_Range_StartDate.Value.Date;
            //    DateTime _EndDate = dtpRec_Range_EndBy.Value.Date;
            //    int _NoOfOccurences = Convert.ToInt16(numRec_Range_EndAfterOccurence.Value.ToString());
            //    int _EveryDayValue = 0;// Convert.ToInt16(numRec_Pattern_Daily_EveryDay.Value.ToString());
            //    int _EveryMonthValue = 0;// Convert.ToInt16(numRec_Pattern_Daily_EveryDay.Value.ToString());
            //    gloAppointment.DayWeekday _DayWeekDayValue = gloAppointmentScheduling.gloAppointment.DayWeekday.day;
            //    gloAppointment.FirstLastCriteria _FirstLastValue = gloAppointmentScheduling.gloAppointment.FirstLastCriteria.first;

            //    bool _ByEndDate = false;
            //    DateTime _IntEndDate = _StartDate;
            //    int i;


            //    if (rbRec_Range_EndAfterOccurence.Checked == true)
            //    {
            //        _ByEndDate = false;
            //    }
            //    else if (rbRec_Range_EndBy.Checked == true)
            //    {
            //        _ByEndDate = true;
            //        _NoOfOccurences = 0;
            //    }

            //    if (rbRec_Pattern_Monthly_Day.Checked == true)
            //    {
            //        _EveryDayValue = Convert.ToInt16(numRec_Pattern_Monthly_Day_Day.Value.ToString());
            //        _EveryMonthValue = Convert.ToInt16(numRec_Pattern_Monthly_Day_Month.Value.ToString());
            //        _DayWeekDayValue = gloAppointmentScheduling.gloAppointment.DayWeekday.day;
            //        _FirstLastValue = gloAppointmentScheduling.gloAppointment.FirstLastCriteria.first;
            //    }
            //    else if (rbRec_Pattern_Monthly_Criteria.Checked == true)
            //    {
            //        _EveryDayValue = 0;
            //        _EveryMonthValue = Convert.ToInt16(numRec_Pattern_Monthly_Criteria_Month.Value.ToString());
            //        switch (cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedItem.ToString())
            //        {
            //            case "day":
            //                {
            //                    _DayWeekDayValue = gloAppointmentScheduling.gloAppointment.DayWeekday.day;
            //                }
            //                break;
            //            case "weekday":
            //                {
            //                    _DayWeekDayValue = gloAppointmentScheduling.gloAppointment.DayWeekday.weekday;
            //                }
            //                break;
            //            case "weekendday":
            //                {
            //                    _DayWeekDayValue = gloAppointmentScheduling.gloAppointment.DayWeekday.weekendday;
            //                }
            //                break;
            //            case "Sunday":
            //                {
            //                    _DayWeekDayValue = gloAppointmentScheduling.gloAppointment.DayWeekday.Sunday;
            //                }
            //                break;
            //            case "Monday":
            //                {
            //                    _DayWeekDayValue = gloAppointmentScheduling.gloAppointment.DayWeekday.Monday;
            //                }
            //                break;
            //            case "Tuesday":
            //                {
            //                    _DayWeekDayValue = gloAppointmentScheduling.gloAppointment.DayWeekday.Tuesday;
            //                }
            //                break;
            //            case "Wednesday":
            //                {
            //                    _DayWeekDayValue = gloAppointmentScheduling.gloAppointment.DayWeekday.Wednesday;
            //                }
            //                break;
            //            case "Thursday":
            //                {
            //                    _DayWeekDayValue = gloAppointmentScheduling.gloAppointment.DayWeekday.Thursday;
            //                }
            //                break;
            //            case "Friday":
            //                {
            //                    _DayWeekDayValue = gloAppointmentScheduling.gloAppointment.DayWeekday.Friday;
            //                }
            //                break;
            //            case "Saturday":
            //                {
            //                    _DayWeekDayValue = gloAppointmentScheduling.gloAppointment.DayWeekday.Saturday;
            //                }
            //                break;
            //        }


            //        switch (cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedItem.ToString())
            //        {
            //            case "first":
            //                {
            //                    _FirstLastValue = gloAppointmentScheduling.gloAppointment.FirstLastCriteria.first;
            //                }
            //                break;
            //            case "second":
            //                {
            //                    _FirstLastValue = gloAppointmentScheduling.gloAppointment.FirstLastCriteria.second;
            //                }
            //                break;
            //            case "third":
            //                {
            //                    _FirstLastValue = gloAppointmentScheduling.gloAppointment.FirstLastCriteria.third;
            //                }
            //                break;
            //            case "fourth":
            //                {
            //                    _FirstLastValue = gloAppointmentScheduling.gloAppointment.FirstLastCriteria.fourth;
            //                }
            //                break;
            //            case "last":
            //                {
            //                    _FirstLastValue = gloAppointmentScheduling.gloAppointment.FirstLastCriteria.last;
            //                }
            //                break;
            //        }
            //    }

            //    // *** DAY CRITERIA ***
            //    if (rbRec_Pattern_Monthly_Day.Checked == true)
            //    {
            //        if (_ByEndDate == false)
            //        {
            //            _EndDate = _StartDate.AddDays(Convert.ToDouble((_NoOfOccurences - 1) * _EveryDayValue));
            //        }
            //        else
            //        {
            //            while (_IntEndDate <= _EndDate)
            //            {
            //                if (_IntEndDate <= _EndDate)
            //                {
            //                    _NoOfOccurences = _NoOfOccurences + 1;
            //                }
            //                _IntEndDate = _IntEndDate.AddDays(_EveryDayValue);
            //            }
            //        }
            //    }
            //    // *** THE CRITERIA ***
            //    else if (rbRec_Pattern_Monthly_Criteria.Checked == true)
            //    {
            //        //Count End Date
            //        if (_ByEndDate == false)
            //        {
            //            i = 0;
            //            while (i < _NoOfOccurences)
            //            {
            //                if (_IntEndDate.DayOfWeek.ToString().ToUpper() != "SUNDAY" && _IntEndDate.DayOfWeek.ToString().ToUpper() != "SATURDAY")
            //                {
            //                    i++;
            //                    if (i == _NoOfOccurences)
            //                    {
            //                        _EndDate = _IntEndDate;
            //                    }
            //                }
            //                _IntEndDate = _IntEndDate.AddDays(1);
            //            }
            //        }
            //        //Count Occurences
            //        else
            //        {
            //            //_EveryDayValue = 0;
            //            //_EveryMonthValue = Convert.ToInt16(numRec_Pattern_Monthly_Criteria_Month.Value.ToString());
            //            //_DayWeekDayValue = (gloAppointmentScheduling.gloAppointment.DayWeekday)cmbRec_Pattern_Monthly_Criteria_DayWeekday.SelectedItem.ToString();
            //            //_FirstLastValue = (gloAppointmentScheduling.gloAppointment.FirstLastCriteria)cmbRec_Pattern_Monthly_Criteria_FstLst.SelectedItem.ToString();

            //            int _TotalMonths = 0;

            //            //while (_IntEndDate <= _EndDate)
            //            //{
            //            //    if (_IntEndDate <= _EndDate)
            //            //    {
            //            //        if (_IntEndDate.DayOfWeek.ToString().ToUpper() != "SUNDAY" && _IntEndDate.DayOfWeek.ToString().ToUpper() != "SATURDAY")
            //            //        {
            //            //            _NoOfOccurences = _NoOfOccurences + 1;
            //            //        }
            //            //    }
            //            //    _IntEndDate = _IntEndDate.AddDays(1);
            //            //}

            //            //--------------------------
            //            switch (_FirstLastValue)
            //            {
            //                case gloAppointmentScheduling.gloAppointment.FirstLastCriteria.first:
            //                    {
            //                        switch (_DayWeekDayValue)
            //                        {
            //                            case gloAppointmentScheduling.gloAppointment.DayWeekday.day:
            //                                {
            //                                    while (_IntEndDate <= _EndDate)
            //                                    {
            //                                        if (_IntEndDate <= _EndDate)
            //                                        {
            //                                            if (_IntEndDate.Day == 1)
            //                                            {
            //                                                _TotalMonths = _TotalMonths + 1;
            //                                                if (_TotalMonths == 0)
            //                                                {
            //                                                    _NoOfOccurences = _NoOfOccurences + 1;
            //                                                }
            //                                                else
            //                                                {
            //                                                    if ((_TotalMonths - 1) % _EveryMonthValue == 0)
            //                                                    {
            //                                                        _NoOfOccurences = _NoOfOccurences + 1;
            //                                                    }
            //                                                }
            //                                            }
            //                                            else
            //                                            {
            //                                                if (_TotalMonths == 0) { _TotalMonths = 1; }
            //                                            }
            //                                        }
            //                                        _IntEndDate = _IntEndDate.AddDays(1);
            //                                    }
            //                                }
            //                                break;
            //                            case gloAppointmentScheduling.gloAppointment.DayWeekday.weekday:
            //                                {
            //                                    int _PrvMonth = 0;
            //                                    bool _WeekdayFound = false;

            //                                    while (_IntEndDate <= _EndDate)
            //                                    {
            //                                        if (_IntEndDate <= _EndDate)
            //                                        {

            //                                            if (_IntEndDate.Month != _PrvMonth || _WeekdayFound == false)
            //                                            {
            //                                                if (_IntEndDate.Month != _PrvMonth)
            //                                                {
            //                                                    _TotalMonths = _TotalMonths + 1;
            //                                                    _PrvMonth = _IntEndDate.Month;
            //                                                    _WeekdayFound = false;
            //                                                }

            //                                                if (_TotalMonths == 1)
            //                                                {
            //                                                    if (_WeekdayFound == false)
            //                                                    {
            //                                                        if (_IntEndDate.DayOfWeek.ToString().ToUpper() != "SUNDAY" && _IntEndDate.DayOfWeek.ToString().ToUpper() != "SATURDAY")
            //                                                        {
            //                                                            DateTime _TempDate = _IntEndDate;
            //                                                            for (i = _IntEndDate.Day - 1; i >= 1; i--)
            //                                                            {
            //                                                                if (_TempDate.DayOfWeek.ToString().ToUpper() != "SUNDAY" && _TempDate.DayOfWeek.ToString().ToUpper() != "SATURDAY")
            //                                                                {
            //                                                                    _WeekdayFound = true;
            //                                                                }
            //                                                                _TempDate = _TempDate.AddDays(-1);
            //                                                            }
            //                                                            //
            //                                                            if (_WeekdayFound == false)
            //                                                            {
            //                                                                _NoOfOccurences = _NoOfOccurences + 1;
            //                                                                _WeekdayFound = true;
            //                                                            }
            //                                                        }
            //                                                    }
            //                                                }
            //                                                else
            //                                                {
            //                                                    if ((_TotalMonths - 1) % _EveryMonthValue == 0)
            //                                                    {
            //                                                        if (_WeekdayFound == false)
            //                                                        {
            //                                                            if (_IntEndDate.DayOfWeek.ToString().ToUpper() != "SUNDAY" && _IntEndDate.DayOfWeek.ToString().ToUpper() != "SATURDAY")
            //                                                            {
            //                                                                _NoOfOccurences = _NoOfOccurences + 1;
            //                                                                _WeekdayFound = true;
            //                                                            }
            //                                                        }
            //                                                    }
            //                                                }
            //                                            }

            //                                        }
            //                                        _IntEndDate = _IntEndDate.AddDays(1);
            //                                    }
            //                                }
            //                                break;
            //                            case gloAppointmentScheduling.gloAppointment.DayWeekday.weekendday:
            //                                {

            //                                }
            //                                break;
            //                            case gloAppointmentScheduling.gloAppointment.DayWeekday.Sunday:
            //                                {
            //                                }
            //                                break;
            //                            case gloAppointmentScheduling.gloAppointment.DayWeekday.Monday:
            //                                {
            //                                }
            //                                break;
            //                            case gloAppointmentScheduling.gloAppointment.DayWeekday.Tuesday:
            //                                {
            //                                }
            //                                break;
            //                            case gloAppointmentScheduling.gloAppointment.DayWeekday.Wednesday:
            //                                {
            //                                }
            //                                break;
            //                            case gloAppointmentScheduling.gloAppointment.DayWeekday.Thursday:
            //                                {
            //                                }
            //                                break;
            //                            case gloAppointmentScheduling.gloAppointment.DayWeekday.Friday:
            //                                {
            //                                }
            //                                break;
            //                            case gloAppointmentScheduling.gloAppointment.DayWeekday.Saturday:
            //                                {
            //                                }
            //                                break;
            //                        }
            //                    }
            //                    break;
            //                case gloAppointmentScheduling.gloAppointment.FirstLastCriteria.second:
            //                    {
            //                    }
            //                    break;
            //                case gloAppointmentScheduling.gloAppointment.FirstLastCriteria.third:
            //                    {
            //                    }
            //                    break;
            //                case gloAppointmentScheduling.gloAppointment.FirstLastCriteria.fourth:
            //                    {
            //                    }
            //                    break;
            //                case gloAppointmentScheduling.gloAppointment.FirstLastCriteria.last:
            //                    {
            //                    }
            //                    break;
            //            }
            //        }
            //    }
            //    // Result
            //    dtpRec_Range_EndBy.Value = _EndDate;
            //    numRec_Range_EndAfterOccurence.Value = _NoOfOccurences;
            //}

            private void CreateMonthlyRecurrence(DateTime StartTime, DateTime EndTime)
            {
                DateTime _StartDate = gloDateMaster.gloDate.DateAsDate(_RecurrenceDetail.RecurrenceCriteria.Range.StartDate);//dtpRec_Range_StartDate.Value.Date;
                DateTime _EndDate = gloDateMaster.gloDate.DateAsDate(_RecurrenceDetail.RecurrenceCriteria.Range.EndDate);//dtpRec_Range_EndBy.Value.Date;
                int _NoOfOccurences = Convert.ToInt16(_RecurrenceDetail.RecurrenceCriteria.Range.EndOccurrenceNumber);//Convert.ToInt16(numRec_Range_EndAfterOccurence.Value.ToString());
                int _EveryDayValue = Convert.ToInt16(_RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber);//Convert.ToInt16(numRec_Pattern_Daily_EveryDay.Value.ToString());
                int _EveryMonthValue = Convert.ToInt16(_RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber); //Convert.ToInt16(numRec_Pattern_Daily_EveryDay.Value.ToString());

                gloAppointmentScheduling.DayWeekday _DayWeekDayValue = _RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria;
                gloAppointmentScheduling.FirstLastCriteria _FirstLastValue = _RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria;

                bool _ByEndDate = false;

                DateTime _IntEndDate = _StartDate;

                int i = 0;
                _Dates.Clear();

                if (_RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndAfterOccurence)
                {
                    _ByEndDate = false;
                }
                else if (_RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndDate)
                {
                    _ByEndDate = true;
                    _NoOfOccurences = 0;
                }

                if (_RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria == RecurrencePatternFlag.DayOfMonthCriteria)
                {
                    // _EveryDayValue = Convert.ToInt16(_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.MonthlyPattern.DayNumber); //Convert.ToInt16(numRec_Pattern_Monthly_Day_Day.Value.ToString());
                    // _EveryMonthValue = Convert.ToInt16(_RecurrenceDetail.RecurrenceCriteria.RecurrencePattern.MonthlyPattern.EveryMonthNumber); //Convert.ToInt16(numRec_Pattern_Monthly_Day_Month.Value.ToString());
                    // //_DayWeekDayValue = gloAppointmentScheduling.DayWeekday.day;
                    // _FirstLastValue = gloAppointmentScheduling.FirstLastCriteria.first;


                    Int32 TotMonth = 0;
                    Int32 _Month = 0;
                    _Month = _IntEndDate.Month;
                    //// *** DAY CRITERIA ***
                    if (_RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber > 0)
                    {
                        if (_ByEndDate == false)
                        {
                            //Condition Added in order to fix issue:#2719-Shows error " Error while finding range."
                            if (_RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber <= System.DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()))
                            {
                                _IntEndDate = Convert.ToDateTime(DateTime.Now.Month.ToString() + "/" + _RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber.ToString() + "/" + DateTime.Now.Year.ToString());
                            }
                            i = 0;
                            if (_IntEndDate < DateTime.Now.Date)
                            {
                                _Month = _Month + 1;
                                _IntEndDate = _IntEndDate.AddMonths(1);
                            }

                            while (i < _NoOfOccurences)
                            {
                                i++;
                                if (i == 1)
                                {
                                    _Dates.Add(_IntEndDate);
                                }
                                else
                                {
                                    if (_Month <= 12)
                                    {
                                        _Month = _Month + TotMonth;
                                        _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                        _Dates.Add(_IntEndDate);
                                    }
                                    else
                                    {
                                        _Month = _Month - 12;
                                        _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                        _Dates.Add(_IntEndDate);
                                    }
                                }
                                TotMonth = TotMonth + _EveryMonthValue;
                            }
                            _EndDate = _IntEndDate;
                        }
                        else
                        {
                            _IntEndDate = Convert.ToDateTime(DateTime.Now.Month.ToString() + "/" + _RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber.ToString() + "/" + DateTime.Now.Year.ToString());
                            while (_IntEndDate <= _EndDate)
                            {
                                if (_IntEndDate.Date <= _EndDate.Date)
                                {
                                    _NoOfOccurences = _NoOfOccurences + 1;
                                }
                                _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                _Dates.Add(_IntEndDate);
                            }
                        }
                        _EndDateValue = _EndDate;
                        _NoOfOccurencesValue = _NoOfOccurences;
                    }

                }
                else if (_RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria == RecurrencePatternFlag.SelectedCriteria)
                {
                    int InDayNum = 0;
                    if (_ByEndDate == false)       //End after n Occurences
                    {
                        while (i < _NoOfOccurences)
                        {
                            switch (_DayWeekDayValue)
                            {
                                case DayWeekday.Sunday:
                                case DayWeekday.Monday:
                                case DayWeekday.Tuesday:
                                case DayWeekday.Wednesday:
                                case DayWeekday.Thursday:
                                case DayWeekday.Friday:
                                case DayWeekday.Saturday:
                                    {
                                        if (_FirstLastValue.GetHashCode() <= 4) //Find Selected Weekday of Month
                                        {
                                            for (int j = 1; j <= DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month); j++)
                                            {
                                                string strTempDate = _IntEndDate.Month + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);

                                                if (InTempDate.DayOfWeek.GetHashCode() == _DayWeekDayValue.GetHashCode())
                                                {
                                                    InDayNum++;
                                                    if (InDayNum == _FirstLastValue.GetHashCode())
                                                    {
                                                        _IntEndDate = InTempDate;
                                                        if (_IntEndDate < _StartDate)
                                                        {
                                                            _IntEndDate = _IntEndDate.AddMonths(1);
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            _Dates.Add(_IntEndDate);
                                                            _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                                            i++;
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else //Last Weekday Of Month
                                        {
                                            for (int j = DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month); j > 0; j--)
                                            {
                                                string strTempDate = _IntEndDate.Month + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);

                                                if (InTempDate.DayOfWeek.GetHashCode() == _DayWeekDayValue.GetHashCode())
                                                {
                                                    _IntEndDate = InTempDate;
                                                    if (_IntEndDate < _StartDate)
                                                    {
                                                        _IntEndDate = _IntEndDate.AddMonths(1);
                                                        InDayNum = 0;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        _Dates.Add(_IntEndDate);
                                                        _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                                        i++;
                                                        InDayNum = 0;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case DayWeekday.day:
                                    {
                                        string sTempDate = "";

                                        if (_FirstLastValue.GetHashCode() <= 4)
                                        {
                                            sTempDate = _IntEndDate.Month + "/" + _FirstLastValue.GetHashCode() + "/" + _IntEndDate.Year;
                                        }
                                        else // Last Day of Month Selected
                                        {
                                            sTempDate = _IntEndDate.Month + "/" + DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month) + "/" + _IntEndDate.Year;
                                        }

                                        _IntEndDate = Convert.ToDateTime(sTempDate);
                                        if (_IntEndDate < _StartDate)
                                        {
                                            _IntEndDate = _IntEndDate.AddMonths(1);
                                        }
                                        else
                                        {
                                            _Dates.Add(_IntEndDate);
                                            _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                            i++;
                                        }
                                        if (i == _NoOfOccurences)
                                        {
                                            _EndDate = _IntEndDate;
                                        }
                                    }
                                    break;
                                case DayWeekday.weekday:
                                    {
                                        if (_FirstLastValue.GetHashCode() <= 4) //Find Selected Weekday of Month
                                        {
                                            for (int j = 1; j <= DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month); j++)
                                            {
                                                string strTempDate = _IntEndDate.Month + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);

                                                if (InTempDate.DayOfWeek != DayOfWeek.Saturday && InTempDate.DayOfWeek != DayOfWeek.Sunday)
                                                {
                                                    InDayNum++;
                                                    if (InDayNum == _FirstLastValue.GetHashCode())
                                                    {
                                                        _IntEndDate = InTempDate;
                                                        if (_IntEndDate < _StartDate)
                                                        {
                                                            _IntEndDate = _IntEndDate.AddMonths(1);
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            _Dates.Add(_IntEndDate);
                                                            _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                                            i++;
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else //Last Weekday Of Month
                                        {
                                            for (int j = DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month); j > 0; j--)
                                            {
                                                string strTempDate = _IntEndDate.Month + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);

                                                if (InTempDate.DayOfWeek != DayOfWeek.Saturday && InTempDate.DayOfWeek != DayOfWeek.Sunday)
                                                {
                                                    _IntEndDate = InTempDate;
                                                    if (_IntEndDate < _StartDate)
                                                    {
                                                        _IntEndDate = _IntEndDate.AddMonths(1);
                                                        InDayNum = 0;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        _Dates.Add(_IntEndDate);
                                                        _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                                        i++;
                                                        InDayNum = 0;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case DayWeekday.weekendday:
                                    {
                                        if (_FirstLastValue.GetHashCode() <= 4) //Find Selected Weekday of Month
                                        {
                                            for (int j = 1; j <= DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month); j++)
                                            {
                                                string strTempDate = _IntEndDate.Month + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);

                                                if (InTempDate.DayOfWeek == DayOfWeek.Friday)
                                                {
                                                    InDayNum++;
                                                    if (InDayNum == _FirstLastValue.GetHashCode())
                                                    {
                                                        _IntEndDate = InTempDate;
                                                        if (_IntEndDate < _StartDate)
                                                        {
                                                            _IntEndDate = _IntEndDate.AddMonths(1);
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            _Dates.Add(_IntEndDate);
                                                            _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                                            i++;
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else //Last Weekday Of Month
                                        {
                                            for (int j = DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month); j > 0; j--)
                                            {
                                                string strTempDate = _IntEndDate.Month + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);

                                                if (InTempDate.DayOfWeek == DayOfWeek.Friday)
                                                {
                                                    _IntEndDate = InTempDate;
                                                    if (_IntEndDate < _StartDate)
                                                    {
                                                        _IntEndDate = _IntEndDate.AddMonths(1);
                                                        InDayNum = 0;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        _Dates.Add(_IntEndDate);
                                                        _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                                        i++;
                                                        InDayNum = 0;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    i++;
                                    break;
                            }
                        }
                    }
                    else      //End by Date selected 
                    {
                        while (_IntEndDate < _EndDate)
                        {
                            switch (_DayWeekDayValue)
                            {
                                case DayWeekday.Sunday:
                                case DayWeekday.Monday:
                                case DayWeekday.Tuesday:
                                case DayWeekday.Wednesday:
                                case DayWeekday.Thursday:
                                case DayWeekday.Friday:
                                case DayWeekday.Saturday:
                                    {
                                        if (_FirstLastValue.GetHashCode() <= 4) //Find Selected Weekday of Month
                                        {
                                            for (int j = 1; j <= DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month); j++)
                                            {
                                                string strTempDate = _IntEndDate.Month + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);

                                                if (InTempDate.DayOfWeek.GetHashCode() == _DayWeekDayValue.GetHashCode())
                                                {
                                                    InDayNum++;
                                                    if (InDayNum == _FirstLastValue.GetHashCode())
                                                    {
                                                        _IntEndDate = InTempDate;
                                                        if (_IntEndDate < _StartDate)
                                                        {
                                                            _IntEndDate = _IntEndDate.AddMonths(1);
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            _Dates.Add(_IntEndDate);
                                                            _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                                            _NoOfOccurences++;
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else //Last Weekday Of Month
                                        {
                                            for (int j = DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month); j > 0; j--)
                                            {
                                                string strTempDate = _IntEndDate.Month + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);

                                                if (InTempDate.DayOfWeek.GetHashCode() == _DayWeekDayValue.GetHashCode())
                                                {
                                                    _IntEndDate = InTempDate;
                                                    if (_IntEndDate < _StartDate)
                                                    {
                                                        _IntEndDate = _IntEndDate.AddMonths(1);
                                                        InDayNum = 0;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        _Dates.Add(_IntEndDate);
                                                        _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                                        _NoOfOccurences++;
                                                        InDayNum = 0;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case DayWeekday.day:
                                    {
                                        string sTempDate = "";

                                        if (_FirstLastValue.GetHashCode() <= 4)
                                        {
                                            sTempDate = _IntEndDate.Month + "/" + _FirstLastValue.GetHashCode() + "/" + _IntEndDate.Year;
                                        }
                                        else // Last Day of Month Selected
                                        {
                                            sTempDate = _IntEndDate.Month + "/" + DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month) + "/" + _IntEndDate.Year;
                                        }

                                        _IntEndDate = Convert.ToDateTime(sTempDate);
                                        if (_IntEndDate < _StartDate)
                                        {
                                            _IntEndDate = _IntEndDate.AddMonths(1);
                                        }
                                        else
                                        {
                                            _Dates.Add(_IntEndDate);
                                            _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                            _NoOfOccurences++;
                                        }
                                        if (_NoOfOccurences > 999)
                                        {
                                            break;
                                        }
                                    }
                                    break;
                                case DayWeekday.weekday:
                                    {
                                        if (_FirstLastValue.GetHashCode() <= 4) //Find Selected Weekday of Month
                                        {
                                            for (int j = 1; j <= DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month); j++)
                                            {
                                                string strTempDate = _IntEndDate.Month + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);

                                                if (InTempDate.DayOfWeek != DayOfWeek.Saturday && InTempDate.DayOfWeek != DayOfWeek.Sunday)
                                                {
                                                    InDayNum++;
                                                    if (InDayNum == _FirstLastValue.GetHashCode())
                                                    {
                                                        _IntEndDate = InTempDate;
                                                        if (_IntEndDate < _StartDate)
                                                        {
                                                            _IntEndDate = _IntEndDate.AddMonths(1);
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            _Dates.Add(_IntEndDate);
                                                            _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                                            _NoOfOccurences++;
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else //Last Weekday Of Month
                                        {
                                            for (int j = DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month); j > 0; j--)
                                            {
                                                string strTempDate = _IntEndDate.Month + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);

                                                if (InTempDate.DayOfWeek != DayOfWeek.Saturday && InTempDate.DayOfWeek != DayOfWeek.Sunday)
                                                {
                                                    _IntEndDate = InTempDate;
                                                    if (_IntEndDate < _StartDate)
                                                    {
                                                        _IntEndDate = _IntEndDate.AddMonths(1);
                                                        InDayNum = 0;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        _Dates.Add(_IntEndDate);
                                                        _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                                        _NoOfOccurences++;
                                                        InDayNum = 0;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case DayWeekday.weekendday:
                                    {
                                        if (_FirstLastValue.GetHashCode() <= 4) //Find Selected Weekday of Month
                                        {
                                            for (int j = 1; j <= DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month); j++)
                                            {
                                                string strTempDate = _IntEndDate.Month + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);

                                                if (InTempDate.DayOfWeek == DayOfWeek.Friday)
                                                {
                                                    InDayNum++;
                                                    if (InDayNum == _FirstLastValue.GetHashCode())
                                                    {
                                                        _IntEndDate = InTempDate;
                                                        if (_IntEndDate < _StartDate)
                                                        {
                                                            _IntEndDate = _IntEndDate.AddMonths(1);
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            _Dates.Add(_IntEndDate);
                                                            _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                                            _NoOfOccurences++;
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else //Last Weekday Of Month
                                        {
                                            for (int j = DateTime.DaysInMonth(_IntEndDate.Year, _IntEndDate.Month); j > 0; j--)
                                            {
                                                string strTempDate = _IntEndDate.Month + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);

                                                if (InTempDate.DayOfWeek == DayOfWeek.Friday)
                                                {
                                                    _IntEndDate = InTempDate;
                                                    if (_IntEndDate < _StartDate)
                                                    {
                                                        _IntEndDate = _IntEndDate.AddMonths(1);
                                                        InDayNum = 0;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        _Dates.Add(_IntEndDate);
                                                        _IntEndDate = _IntEndDate.AddMonths(_EveryMonthValue);
                                                        _NoOfOccurences++;
                                                        InDayNum = 0;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }





                // }
                //}
                // Result
                _EndDateValue = _EndDate;
                _NoOfOccurencesValue = _NoOfOccurences;
            }

            private void CreateYearlyRecurrence(DateTime StartTime, DateTime EndTime)
            {
                DateTime _StartDate = gloDateMaster.gloDate.DateAsDate(_RecurrenceDetail.RecurrenceCriteria.Range.StartDate);
                DateTime _EndDate = gloDateMaster.gloDate.DateAsDate(_RecurrenceDetail.RecurrenceCriteria.Range.EndDate);
                Int64 _NoOfOccurences = _RecurrenceDetail.RecurrenceCriteria.Range.EndOccurrenceNumber;
                Int64 _DayOfMonth = _RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber;
                MonthRange _Month = _RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria;
                DayWeekday _Day = _RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria;
                FirstLastCriteria _DayNum = _RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria;

                bool _ByEndDate = false;
                DateTime _IntEndDate = _StartDate;
                int i = 0;



                _Dates.Clear();

                if (_RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndAfterOccurence)
                {
                    _ByEndDate = false;
                }
                else if (_RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag == RecurrenceRangeFlag.EndDate)
                {
                    _ByEndDate = true;
                    _NoOfOccurences = 0;
                }

                if (_RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria == RecurrencePatternFlag.DayOfMonthCriteria)
                {
                    if (_ByEndDate == false)
                    {
                        while (i < _NoOfOccurences)
                        {
                            string sTempDate = "";
                            if (_DayOfMonth <= DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()))
                            {
                                sTempDate = _Month.GetHashCode() + "/" + _DayOfMonth + "/" + _IntEndDate.Year;
                                _IntEndDate = Convert.ToDateTime(sTempDate);
                                if (_IntEndDate < _StartDate)
                                {
                                    _IntEndDate = _IntEndDate.AddYears(1);
                                }
                                else
                                {
                                    _Dates.Add(_IntEndDate);
                                    _IntEndDate = _IntEndDate.AddYears(1);
                                    i++;
                                }
                                if (i == _NoOfOccurences)
                                {
                                    _EndDate = _IntEndDate;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else //End by date  
                    {
                        while (_IntEndDate <= _EndDate)
                        {
                            if (_DayOfMonth <= DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()))
                            {
                                string sTempDate = _Month.GetHashCode() + "/" + _DayOfMonth + "/" + _IntEndDate.Year;
                                _IntEndDate = Convert.ToDateTime(sTempDate);
                                if (_IntEndDate < _StartDate)
                                {
                                    _IntEndDate = _IntEndDate.AddYears(1);
                                }
                                else
                                {
                                    _Dates.Add(_IntEndDate);
                                    _IntEndDate = _IntEndDate.AddYears(1);
                                    _NoOfOccurences++;
                                }
                                if (_NoOfOccurences > 999)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                if (_RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria == RecurrencePatternFlag.SelectedCriteria)
                {
                    int InDayNum = 0;
                    if (_ByEndDate == false)
                    {

                        while (i < _NoOfOccurences)
                        {
                            switch (_Day)
                            {
                                case DayWeekday.Monday:
                                case DayWeekday.Tuesday:
                                case DayWeekday.Wednesday:
                                case DayWeekday.Thursday:
                                case DayWeekday.Friday:
                                case DayWeekday.Saturday:
                                case DayWeekday.Sunday:
                                    {
                                        if (_DayNum.GetHashCode() <= 4) //Find Selected Weekday of Month
                                        {
                                            for (int j = 1; j <= DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()); j++)
                                            {
                                                string strTempDate = _Month.GetHashCode() + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);
                                                if (InTempDate.DayOfWeek.GetHashCode() == _Day.GetHashCode())
                                                {
                                                    InDayNum++;
                                                    if (InDayNum == _DayNum.GetHashCode())
                                                    {
                                                        _IntEndDate = InTempDate;
                                                        if (_IntEndDate < _StartDate)
                                                        {
                                                            _IntEndDate = _IntEndDate.AddYears(1);
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            _Dates.Add(_IntEndDate);
                                                            _IntEndDate = _IntEndDate.AddYears(1);
                                                            i++;
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else // Find Last Weekday of month
                                        {
                                            for (int j = DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()); j > 0; j--)
                                            {
                                                string strTempDate = _Month.GetHashCode() + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);
                                                if (InTempDate.DayOfWeek.GetHashCode() == _Day.GetHashCode())
                                                {
                                                    _IntEndDate = InTempDate;
                                                    if (_IntEndDate < _StartDate)
                                                    {
                                                        _IntEndDate = _IntEndDate.AddYears(1);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        _Dates.Add(_IntEndDate);
                                                        _IntEndDate = _IntEndDate.AddYears(1);
                                                        i++;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case DayWeekday.day:
                                    {
                                        string sTempDate = "";

                                        if (_DayNum.GetHashCode() <= 4)
                                        {
                                            sTempDate = _Month.GetHashCode() + "/" + _DayNum.GetHashCode() + "/" + _IntEndDate.Year;
                                        }
                                        else // Last Day of Month Selected
                                        {
                                            sTempDate = _Month.GetHashCode() + "/" + DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()) + "/" + _IntEndDate.Year;
                                        }

                                        _IntEndDate = Convert.ToDateTime(sTempDate);
                                        if (_IntEndDate < _StartDate)
                                        {
                                            _IntEndDate = _IntEndDate.AddYears(1);
                                        }
                                        else
                                        {
                                            _Dates.Add(_IntEndDate);
                                            _IntEndDate = _IntEndDate.AddYears(1);
                                            i++;
                                        }
                                        if (i == _NoOfOccurences)
                                        {
                                            _EndDate = _IntEndDate;
                                        }
                                    }
                                    break;

                                case DayWeekday.weekendday:
                                    {
                                        if (_DayNum.GetHashCode() <= 4) //Find selected weekend Of the Month
                                        {
                                            for (int j = 1; j <= DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()); j++)
                                            {
                                                string strTempDate = _Month.GetHashCode() + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);
                                                if (InTempDate.DayOfWeek == DayOfWeek.Friday)
                                                {
                                                    InDayNum++;
                                                    if (InDayNum == _DayNum.GetHashCode())
                                                    {
                                                        _IntEndDate = InTempDate;
                                                        if (_IntEndDate < _StartDate)
                                                        {
                                                            _IntEndDate = _IntEndDate.AddYears(1);
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            _Dates.Add(_IntEndDate);
                                                            _IntEndDate = _IntEndDate.AddYears(1);
                                                            i++;
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else      // Find last Weekend of Month
                                        {
                                            for (int j = DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()); j > 0; j--)
                                            {
                                                string strTempDate = _Month.GetHashCode() + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);
                                                if (InTempDate.DayOfWeek == DayOfWeek.Friday)
                                                {

                                                    _IntEndDate = InTempDate;
                                                    if (_IntEndDate < _StartDate)
                                                    {
                                                        _IntEndDate = _IntEndDate.AddYears(1);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        _Dates.Add(_IntEndDate);
                                                        _IntEndDate = _IntEndDate.AddYears(1);
                                                        i++;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case DayWeekday.weekday:
                                    {
                                        if (_DayNum.GetHashCode() <= 4) //Find Selected Weekday of Month
                                        {
                                            for (int j = 1; j <= DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()); j++)
                                            {
                                                string strTempDate = _Month.GetHashCode() + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);
                                                if (InTempDate.DayOfWeek != DayOfWeek.Saturday && InTempDate.DayOfWeek != DayOfWeek.Sunday)
                                                {
                                                    InDayNum++;
                                                    if (InDayNum == _DayNum.GetHashCode())
                                                    {
                                                        _IntEndDate = InTempDate;
                                                        if (_IntEndDate < _StartDate)
                                                        {
                                                            _IntEndDate = _IntEndDate.AddYears(1);
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            _Dates.Add(_IntEndDate);
                                                            _IntEndDate = _IntEndDate.AddYears(1);
                                                            i++;
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else //Find last Weekday of Month
                                        {
                                            for (int j = DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()); j > 0; j--)
                                            {
                                                string strTempDate = _Month.GetHashCode() + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);
                                                if (InTempDate.DayOfWeek != DayOfWeek.Saturday && InTempDate.DayOfWeek != DayOfWeek.Sunday)
                                                {
                                                    _IntEndDate = InTempDate;
                                                    if (_IntEndDate < _StartDate)
                                                    {
                                                        _IntEndDate = _IntEndDate.AddYears(1);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        _Dates.Add(_IntEndDate);
                                                        _IntEndDate = _IntEndDate.AddYears(1);
                                                        i++;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                    else //End by date  
                    {
                        while (_IntEndDate < _EndDate)
                        {
                            switch (_Day)
                            {
                                case DayWeekday.Monday:
                                case DayWeekday.Tuesday:
                                case DayWeekday.Wednesday:
                                case DayWeekday.Thursday:
                                case DayWeekday.Friday:
                                case DayWeekday.Saturday:
                                case DayWeekday.Sunday:
                                    {
                                        if (_DayNum.GetHashCode() <= 4) //Find Selected Weekday of Month
                                        {
                                            for (int j = 1; j <= DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()); j++)
                                            {
                                                string strTempDate = _Month.GetHashCode() + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);
                                                if (InTempDate.DayOfWeek.GetHashCode() == _Day.GetHashCode())
                                                {
                                                    InDayNum++;
                                                    if (InDayNum == _DayNum.GetHashCode())
                                                    {
                                                        _IntEndDate = InTempDate;
                                                        if (_IntEndDate < _StartDate)
                                                        {
                                                            _IntEndDate = _IntEndDate.AddYears(1);
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            _Dates.Add(_IntEndDate);
                                                            _IntEndDate = _IntEndDate.AddYears(1);
                                                            _NoOfOccurences++;
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else // Find Last Weekday of month
                                        {
                                            for (int j = DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()); j > 0; j--)
                                            {
                                                string strTempDate = _Month.GetHashCode() + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);
                                                if (InTempDate.DayOfWeek.GetHashCode() == _Day.GetHashCode())
                                                {
                                                    _IntEndDate = InTempDate;
                                                    if (_IntEndDate < _StartDate)
                                                    {
                                                        _IntEndDate = _IntEndDate.AddYears(1);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        _Dates.Add(_IntEndDate);
                                                        _IntEndDate = _IntEndDate.AddYears(1);
                                                        _NoOfOccurences++;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case DayWeekday.day:
                                    {
                                        string sTempDate = "";

                                        if (_DayNum.GetHashCode() <= 4)
                                        {
                                            sTempDate = _Month.GetHashCode() + "/" + _DayNum.GetHashCode() + "/" + _IntEndDate.Year;
                                        }
                                        else // Last Day of Month Selected
                                        {
                                            sTempDate = _Month.GetHashCode() + "/" + DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()) + "/" + _IntEndDate.Year;
                                        }

                                        _IntEndDate = Convert.ToDateTime(sTempDate);
                                        if (_IntEndDate < _StartDate)
                                        {
                                            _IntEndDate = _IntEndDate.AddYears(1);
                                        }
                                        else
                                        {
                                            _Dates.Add(_IntEndDate);
                                            _IntEndDate = _IntEndDate.AddYears(1);
                                            _NoOfOccurences++;
                                        }
                                        if (_NoOfOccurences > 999)
                                        {
                                            break;
                                        }
                                    }
                                    break;

                                case DayWeekday.weekendday:
                                    {
                                        if (_DayNum.GetHashCode() <= 4) //Find selected weekend Of the Month
                                        {
                                            for (int j = 1; j <= DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()); j++)
                                            {
                                                string strTempDate = _Month.GetHashCode() + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);
                                                if (InTempDate.DayOfWeek == DayOfWeek.Friday)
                                                {
                                                    InDayNum++;
                                                    if (InDayNum == _DayNum.GetHashCode())
                                                    {
                                                        _IntEndDate = InTempDate;
                                                        if (_IntEndDate < _StartDate)
                                                        {
                                                            _IntEndDate = _IntEndDate.AddYears(1);
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            _Dates.Add(_IntEndDate);
                                                            _IntEndDate = _IntEndDate.AddYears(1);
                                                            _NoOfOccurences++;
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else      // Find last Weekend of Month
                                        {
                                            for (int j = DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()); j > 0; j--)
                                            {
                                                string strTempDate = _Month.GetHashCode() + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);
                                                if (InTempDate.DayOfWeek == DayOfWeek.Friday)
                                                {

                                                    _IntEndDate = InTempDate;
                                                    if (_IntEndDate < _StartDate)
                                                    {
                                                        _IntEndDate = _IntEndDate.AddYears(1);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        _Dates.Add(_IntEndDate);
                                                        _IntEndDate = _IntEndDate.AddYears(1);
                                                        _NoOfOccurences++;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case DayWeekday.weekday:
                                    {
                                        if (_DayNum.GetHashCode() <= 4) //Find Selected Weekday of Month
                                        {
                                            for (int j = 1; j <= DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()); j++)
                                            {
                                                string strTempDate = _Month.GetHashCode() + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);
                                                if (InTempDate.DayOfWeek != DayOfWeek.Saturday && InTempDate.DayOfWeek != DayOfWeek.Sunday)
                                                {
                                                    InDayNum++;
                                                    if (InDayNum == _DayNum.GetHashCode())
                                                    {
                                                        _IntEndDate = InTempDate;
                                                        if (_IntEndDate < _StartDate)
                                                        {
                                                            _IntEndDate = _IntEndDate.AddYears(1);
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            _Dates.Add(_IntEndDate);
                                                            _IntEndDate = _IntEndDate.AddYears(1);
                                                            _NoOfOccurences++;
                                                            InDayNum = 0;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else //Find last Weekday of Month
                                        {
                                            for (int j = DateTime.DaysInMonth(_IntEndDate.Year, _Month.GetHashCode()); j > 0; j--)
                                            {
                                                string strTempDate = _Month.GetHashCode() + "/" + j + "/" + _IntEndDate.Year;
                                                DateTime InTempDate = Convert.ToDateTime(strTempDate);
                                                if (InTempDate.DayOfWeek != DayOfWeek.Saturday && InTempDate.DayOfWeek != DayOfWeek.Sunday)
                                                {
                                                    _IntEndDate = InTempDate;
                                                    if (_IntEndDate < _StartDate)
                                                    {
                                                        _IntEndDate = _IntEndDate.AddYears(1);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        _Dates.Add(_IntEndDate);
                                                        _IntEndDate = _IntEndDate.AddYears(1);
                                                        _NoOfOccurences++;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }



                // Result
                _EndDateValue = _EndDate;
                _NoOfOccurencesValue = _NoOfOccurences;
            }

            // Created By Pranit on 8 sep 2011, Added one more parameter MasterAppointment
            private void RemoveBlockedSlots(Int64 ProviderID, DateTime StartTime, DateTime EndTime,Int64 MasterAppointmentId)
            {
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


                _BlockedDates.Clear();
                _ScheduleStatus.Clear();
                
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    TimeSpan ts = EndTime - StartTime;
                    decimal duration = (decimal)ts.TotalMinutes;


                   // string _sqlQuery = "";
                   // for (int i = _Dates.Count - 1; i >= 0; i--)
                    for (int i = 0; i <= _Dates.Count - 1; i++)
                    {
                        //_sqlQuery = " SELECT MAX(BlockCount) FROM ( "
                        //                                            + " SELECT Count(nDTLAppointmentID) AS BlockCount"
                        //                                            + " FROM  AS_Appointment_DTL"
                        //                                            + " WHERE nASBaseID = " + ProviderID + " "
                        //                                            + " AND nASBaseFlag = 1 "
                        //                                            + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_Dates[i].ToString()) + " "
                        //                                            + " AND ((dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + ") OR (dtStartTime < " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + " AND dtEndTime >= " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ")) "
                        //                                            + " AND nClinicID = " + _clinicID + " "
                        //                                            + " UNION"
                        //                                            + " SELECT Count(nDTLScheduleID) AS BlockCount"
                        //                                            + " FROM AS_Schedule_DTL"
                        //                                            + " WHERE nASBaseID = " + ProviderID + " "
                        //                                            + " AND nASBaseFlag = 1 "
                        //                                            + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_Dates[i].ToString()) + " "
                        //                                            + " AND ((dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + ") OR (dtStartTime < " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + " AND dtEndTime >= " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ")) "
                        //                                            + " AND nClinicID = " + _clinicID + " "
                        //          + " ) AS AppointmentBlocks ";

                        //_sqlQuery = " SELECT Count(nDTLScheduleID) AS BlockCount"
                        //            + " FROM AS_Schedule_DTL"
                        //            + " WHERE nASBaseID = " + ProviderID + " "
                        //            + " AND nASBaseFlag = 1 "
                        //            + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_Dates[i].ToString()) + " "
                        //            + " AND ((dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + ") OR (dtStartTime < " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + " AND dtEndTime >= " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ")) "
                        //            + " AND nClinicID = " + _clinicID + " ";

                        //_sqlQuery = " SELECT Count(nDTLScheduleID) AS BlockCount"
                        //            + " FROM AS_Schedule_DTL"
                        //            + " WHERE nASBaseID = " + ProviderID + " "
                        //            + " AND nASBaseFlag = 1 "
                        //            + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_Dates[i].ToString()) + " "
                        //            + " AND ((dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + ") OR (dtStartTime < " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + " AND dtEndTime >= " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ") OR (dtStartTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime <" + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ")) "
                        //            + " AND nClinicID = " + _clinicID + " ";

                        //object oResult = oDB.ExecuteScalar_Query(_sqlQuery);

                        DateTime dateTime = new DateTime();
                        string[] date = _Dates[i].ToString().Split(' ');
                        string splitDate = date[0].ToString() + " " + StartTime.ToShortTimeString();

                        dateTime = Convert.ToDateTime(splitDate);                         
                       
                        DateTime newDateTime = new DateTime();
                        newDateTime = dateTime.AddMinutes((int)duration);

                        object oResult = GetScheduleConflictTime(DatabaseConnectionString, ProviderID.ToString(), "1", gloDateMaster.gloDate.DateAsNumber(dateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), gloDateMaster.gloDate.DateAsNumber(newDateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), _clinicID);

                        if (oResult != null && Convert.ToString(oResult) != "")
                        {
                            if (Convert.ToInt32(oResult) > 0)
                            {
                                _BlockedDates.Add(_Dates[i]);
                                _ScheduleStatus.Add("Blocked");
                                // _Dates.RemoveAt(i);
                            }
                            else
                            {
                               
                                //MasterAppointment oAppParameters = new MasterAppointment();
                                //SetAppointmentParameter oAppParameters1 = new SetAppointmentParameter();
                                //long mID = oAppParameters.MasterID;
                                //long mID1 = oAppParameters1.MasterAppointmentID;


                                object oResultStatus = GetAppointmentConflictTime(DatabaseConnectionString, MasterAppointmentId, ProviderID, gloDateMaster.gloDate.DateAsNumber(dateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), gloDateMaster.gloDate.DateAsNumber(newDateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), _clinicID);
                                if (oResultStatus != null)
                                {
                                    if (Convert.ToInt32(oResultStatus) > 0)
                                    {
                                        _ScheduleStatus.Add("Scheduled");
                                    }
                                    else
                                    {
                                        _ScheduleStatus.Add("Open");
                                    }
                                }
                            }


                        }

                    }
                    oDB.Disconnect();
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
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                }
            }

            // Created By Pranit on 23 sep 2011  For recurrence without provider and resource
            private void RemoveBlockedSlotsWithoutProviderAndResource(Int64 ProviderID,DateTime StartTime, DateTime EndTime, Int64 MasterAppointmentId)
            {
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


                _BlockedDates.Clear();
                _ScheduleStatus.Clear();

               


                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    TimeSpan ts = EndTime - StartTime;
                    decimal duration = (decimal)ts.TotalMinutes;

                    //string _sqlQuery = "";
                   
                    for (int i = 0; i <= _Dates.Count - 1; i++)
                    {
                   
                        //_sqlQuery = " SELECT Count(nDTLScheduleID) AS BlockCount"
                        //            + " FROM AS_Schedule_DTL"
                        //            + " WHERE nASBaseID = " + ProviderID + " "
                        //            + " AND nASBaseFlag = 1 "
                        //            + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_Dates[i].ToString()) + " "
                        //            + " AND ((dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + ") OR (dtStartTime < " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + " AND dtEndTime >= " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ") OR (dtStartTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime <" + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ")) "
                        //            + " AND nClinicID = " + _clinicID + " ";

                       // object oResult = oDB.ExecuteScalar_Query(_sqlQuery);


                        DateTime dateTime = new DateTime();
                        string[] date = _Dates[i].ToString().Split(' ');
                        string splitDate = date[0].ToString() + " " + StartTime.ToShortTimeString();

                        dateTime = Convert.ToDateTime(splitDate);

                        DateTime newDateTime = new DateTime();
                        newDateTime = dateTime.AddMinutes((int)duration);

                        object oResult = GetScheduleConflictTime(DatabaseConnectionString, ProviderID.ToString(), "1", gloDateMaster.gloDate.DateAsNumber(dateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), gloDateMaster.gloDate.DateAsNumber(newDateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), _clinicID);

                        if (oResult != null && Convert.ToString(oResult) != "")
                        {
                            if (Convert.ToInt32(oResult) > 0)
                            {
                                _BlockedDates.Add(_Dates[i]);
                                _ScheduleStatus.Add("Blocked");
                            
                            }
                            else
                            {


                               
                                object oResultStatus = GetAppointmentConflictTime(DatabaseConnectionString, MasterAppointmentId, ProviderID, gloDateMaster.gloDate.DateAsNumber(dateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), gloDateMaster.gloDate.DateAsNumber(newDateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), _clinicID);
                                if (oResultStatus != null)
                                {
                                    if (Convert.ToInt32(oResultStatus) > 0)
                                    {
                                        _ScheduleStatus.Add("Scheduled");
                                    }
                                    else
                                    {
                                        _ScheduleStatus.Add("Open");
                                    }
                                }
                            }


                        }

                    }
                    oDB.Disconnect();
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
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                }
            }
  
          

            
            /// <summary>
            ///  Created new Function By Pranit on 13 sep 2011 for multiple Recurrence
            /// </summary>
            /// <param name="ResourceID"></param>
            /// <param name="StartTime"></param>
            /// <param name="EndTime"></param>
            /// <param name="MasterAppointmentId"></param>
            public void RemoveResourceBlockedSlots(string ResourceID, DateTime StartTime, DateTime EndTime, Int64 MasterAppointmentId)
            {
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



                _BlockedDates.Clear();
                _ScheduleStatus.Clear();

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    TimeSpan ts = EndTime - StartTime;
                    decimal duration = (decimal)ts.TotalMinutes;
                    //string _sqlQuery = "";
               
                    for (int i = 0; i <= _Dates.Count - 1; i++)
                    {

                        //_sqlQuery = " SELECT Count(nDTLScheduleID) AS BlockCount"
                        //             + " FROM AS_Schedule_DTL"
                        //             + " WHERE nASBaseID in ( " + ResourceID + ") "
                        //             + " AND nASBaseFlag = 3 "
                        //             + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_Dates[i].ToString()) + " "
                        //             + " AND ((dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + ") OR (dtStartTime < " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + " AND dtEndTime >= " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ") OR (dtStartTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime <" + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ")) "
                        //             + " AND nClinicID = " + _clinicID + " ";

                        //object oResult = oDB.ExecuteScalar_Query(_sqlQuery);



                        DateTime dateTime = new DateTime();
                        string[] date = _Dates[i].ToString().Split(' ');
                        string splitDate = date[0].ToString() + " " + StartTime.ToShortTimeString();

                        dateTime = Convert.ToDateTime(splitDate);

                        DateTime newDateTime = new DateTime();
                        newDateTime = dateTime.AddMinutes((int)duration);

                        object oResult = GetScheduleConflictTime(DatabaseConnectionString, ResourceID.ToString(), "3", gloDateMaster.gloDate.DateAsNumber(dateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), gloDateMaster.gloDate.DateAsNumber(newDateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), _clinicID);


                        if (oResult != null && Convert.ToString(oResult) != "")
                        {
                            if (Convert.ToInt32(oResult) > 0)
                            {
                                _BlockedDates.Add(_Dates[i]);
                                _ScheduleStatus.Add("Blocked");

                            }
                            else
                            {



                                object oResultStatus = GetAppointmentConflictTimeMultipleRecurrence(DatabaseConnectionString, MasterAppointmentId, ResourceID, gloDateMaster.gloDate.DateAsNumber(dateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), gloDateMaster.gloDate.DateAsNumber(newDateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), _clinicID);
                                if (oResultStatus != null)
                                {
                                    if (Convert.ToInt32(oResultStatus) > 0)
                                    {
                                        _ScheduleStatus.Add("Scheduled");
                                    }
                                    else
                                    {
                                        _ScheduleStatus.Add("Open");
                                    }
                                }
                            }


                        }

                    }
                    oDB.Disconnect();
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
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                }



        
            }



            /// <summary>
            /// Created new Function By Pranit on 13 sep 2011 for multiple Recurrence for both Provider and Resource ID
            /// </summary>
            /// <param name="ProviderID"></param>
            /// <param name="ResourceID"></param>
            /// <param name="StartTime"></param>
            /// <param name="EndTime"></param>
            /// <param name="MasterAppointmentId"></param>
            public void RemoveProviderResourceBlockedSlots(Int64 ProviderID,string ResourceID, DateTime StartTime, DateTime EndTime, Int64 MasterAppointmentId)
            {
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



                _BlockedDates.Clear();
                _ScheduleStatus.Clear();

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    TimeSpan ts = EndTime - StartTime;
                    decimal duration = (decimal)ts.TotalMinutes;
                    //string _sqlQuery = "";

                    for (int i = 0; i <= _Dates.Count - 1; i++)
                    {

                        //_sqlQuery = " SELECT Count(nDTLScheduleID) AS BlockCount"
                        //             + " FROM AS_Schedule_DTL"
                        //             + " WHERE nASBaseID in ( "+ ProviderID.ToString() + "," + ResourceID + ") "
                        //             + " AND nASBaseFlag in (1,3) "
                        //             + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_Dates[i].ToString()) + " "
                        //             + " AND ((dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + ") OR (dtStartTime < " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + " AND dtEndTime >= " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ") OR (dtStartTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime <" + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ")) "
                        //             + " AND nClinicID = " + _clinicID + " ";

                        //object oResult = oDB.ExecuteScalar_Query(_sqlQuery);


                        DateTime dateTime = new DateTime();
                        string[] date = _Dates[i].ToString().Split(' ');
                        string splitDate = date[0].ToString() + " " + StartTime.ToShortTimeString();

                        dateTime = Convert.ToDateTime(splitDate);

                        DateTime newDateTime = new DateTime();
                        newDateTime = dateTime.AddMinutes((int)duration);

                        object oResult = GetScheduleConflictTime(DatabaseConnectionString, ProviderID.ToString() + "," + ResourceID.ToString(), "1,3", gloDateMaster.gloDate.DateAsNumber(dateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), gloDateMaster.gloDate.DateAsNumber(newDateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), _clinicID);


                        if (oResult != null && Convert.ToString(oResult) != "")
                        {
                            if (Convert.ToInt32(oResult) > 0)
                            {
                                _BlockedDates.Add(_Dates[i]);
                                _ScheduleStatus.Add("Blocked");

                            }
                            else
                            {



                                object oResultStatus = GetAppointmentConflictTimeMultipleRecurrence(DatabaseConnectionString, MasterAppointmentId, string.Concat(ProviderID.ToString(), ",", ResourceID), gloDateMaster.gloDate.DateAsNumber(dateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()),gloDateMaster.gloDate.DateAsNumber(newDateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), _clinicID);
                                if (oResultStatus != null)
                                {
                                    if (Convert.ToInt32(oResultStatus) > 0)
                                    {
                                        _ScheduleStatus.Add("Scheduled");
                                    }
                                    else
                                    {
                                        _ScheduleStatus.Add("Open");
                                    }
                                }
                            }


                        }

                    }
                    oDB.Disconnect();
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
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                }




            }



            private void RemoveBlockedSlots(Int64 ProviderID, DateTime StartTime, DateTime EndTime)
            {
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


                _BlockedDates.Clear();
              

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    TimeSpan ts = EndTime - StartTime;
                    decimal duration = (decimal)ts.TotalMinutes;
                   // string _sqlQuery = "";
                    // for (int i = _Dates.Count - 1; i >= 0; i--)
                    for (int i = 0; i <= _Dates.Count - 1; i++)
                    {
                        //_sqlQuery = " SELECT MAX(BlockCount) FROM ( "
                        //                                            + " SELECT Count(nDTLAppointmentID) AS BlockCount"
                        //                                            + " FROM  AS_Appointment_DTL"
                        //                                            + " WHERE nASBaseID = " + ProviderID + " "
                        //                                            + " AND nASBaseFlag = 1 "
                        //                                            + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_Dates[i].ToString()) + " "
                        //                                            + " AND ((dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + ") OR (dtStartTime < " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + " AND dtEndTime >= " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ")) "
                        //                                            + " AND nClinicID = " + _clinicID + " "
                        //                                            + " UNION"
                        //                                            + " SELECT Count(nDTLScheduleID) AS BlockCount"
                        //                                            + " FROM AS_Schedule_DTL"
                        //                                            + " WHERE nASBaseID = " + ProviderID + " "
                        //                                            + " AND nASBaseFlag = 1 "
                        //                                            + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_Dates[i].ToString()) + " "
                        //                                            + " AND ((dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + ") OR (dtStartTime < " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + " AND dtEndTime >= " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ")) "
                        //                                            + " AND nClinicID = " + _clinicID + " "
                        //          + " ) AS AppointmentBlocks ";

                        //_sqlQuery = " SELECT Count(nDTLScheduleID) AS BlockCount"
                        //            + " FROM AS_Schedule_DTL"
                        //            + " WHERE nASBaseID = " + ProviderID + " "
                        //            + " AND nASBaseFlag = 1 "
                        //            + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_Dates[i].ToString()) + " "
                        //            + " AND ((dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + ") OR (dtStartTime < " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + " AND dtEndTime >= " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ")) "
                        //            + " AND nClinicID = " + _clinicID + " ";




                        //_sqlQuery = " SELECT Count(nDTLScheduleID) AS BlockCount"
                        //            + " FROM AS_Schedule_DTL"
                        //            + " WHERE nASBaseID = " + ProviderID + " "
                        //            + " AND nASBaseFlag = 1 "
                        //            + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(_Dates[i].ToString()) + " "
                        //            + " AND ((dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + ") OR (dtStartTime < " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + " AND dtEndTime >= " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ") OR (dtStartTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime <" + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ")) "
                        //            + " AND nClinicID = " + _clinicID + " ";

                        //object oResult = oDB.ExecuteScalar_Query(_sqlQuery);


                        DateTime dateTime = new DateTime();
                        string[] date = _Dates[i].ToString().Split(' ');
                        string splitDate = date[0].ToString() + " " + StartTime.ToShortTimeString();

                        dateTime = Convert.ToDateTime(splitDate);

                        DateTime newDateTime = new DateTime();
                        newDateTime = dateTime.AddMinutes((int)duration);

                        object oResult = GetScheduleConflictTime(DatabaseConnectionString, ProviderID.ToString(), "1", gloDateMaster.gloDate.DateAsNumber(dateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), gloDateMaster.gloDate.DateAsNumber(newDateTime.ToString()), gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), _clinicID);

                        
                        
                        if (oResult != null && Convert.ToString(oResult) != "")
                        {
                            if (Convert.ToInt32(oResult) > 0)
                            {
                                _BlockedDates.Add(_Dates[i]);
                               // _Dates.RemoveAt(i);
                            }
                            else
                            {

                            }


                        }

                    }
                    oDB.Disconnect();
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
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                }
            }





            // By Pranit on 8 sep 2011 for Provider
            public object GetAppointmentConflictTime(string DatabaseConnectionString,Int64 masterAppointmentID,Int64 providerId, Int32 dtStartDate, Int32 dtStartTime,Int32 dtEndDate, Int32 dtEndTime, Int64 ClinicID)
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                object Count;
                Count = 0;

                oDB.Connect(false);

                oDBParameters.Add("@appointmentID", masterAppointmentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@providerId ", providerId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtStartDate", dtStartDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtEndDate", dtEndDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@StartTime", dtStartTime, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@EndTime", dtEndTime, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@Count", 0, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.Int);

                oDB.Execute("GetAppointmentConflictTime", oDBParameters, out Count);

                oDB.Disconnect();

                oDBParameters.Dispose();
                oDBParameters = null;

                oDB.Dispose();
                oDB = null;


                return Count;
            }

            // By Pranit on 13 sep 2011 for multiple recurrence (both Resource and Provider)
            public object GetAppointmentConflictTimeMultipleRecurrence(string DatabaseConnectionString, Int64 masterAppointmentID, string resourceId, Int32 dtStartDate, Int32 dtStartTime, Int32 dtEndDate, Int32 dtEndTime, Int64 ClinicID)
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                object Count;
                Count = 0;

                oDB.Connect(false);

                oDBParameters.Add("@appointmentID", masterAppointmentID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@providerId ", resourceId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar,100);
                oDBParameters.Add("@dtStartDate", dtStartDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@StartTime", dtStartTime, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtEndDate", dtEndDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@EndTime", dtEndTime, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@Count", 0, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.Int);

                oDB.Execute("GetAppointmentConflictTimeMultipleRecurrence", oDBParameters, out Count);

                oDB.Disconnect();

                oDBParameters.Dispose();
                oDBParameters = null;

                oDB.Dispose();
                oDB = null;


                return Count;
            }

           




            public  bool  RemoveResourceBlockedSlots(Int64 ProviderID, DateTime StartTime, DateTime EndTime,DateTime AppoinmentDate)
            {
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



                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
                try
                {
                    oDB.Connect(false);
                    string _sqlQuery = "";
                    //for (int i = _ResourceDates.Count - 1; i >= 0; i--)
                    //{
                       _sqlQuery = " SELECT Count(nDTLScheduleID) AS BlockCount"
                                    + " FROM AS_Schedule_DTL  WITH(NOLOCK) "
                                    + " WHERE nASBaseID = " + ProviderID + " "
                                    + " AND nASBaseFlag = 3 "
                                    + " AND dtStartDate = " + gloDateMaster.gloDate.DateAsNumber(AppoinmentDate.ToString()) + " "//+ gloDateMaster.gloDate.DateAsNumber(_Dates[i].ToString()) + " "
                                    + " AND ((dtStartTime <= " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + ") OR (dtStartTime < " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + " AND dtEndTime >= " + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ") OR (dtStartTime > " + gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()) + " AND dtEndTime <" + gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()) + ")) "
                                    + " AND nClinicID = " + _clinicID + " ";

                        object oResult = oDB.ExecuteScalar_Query(_sqlQuery);

                        if (oResult != null && Convert.ToString(oResult) != "")
                        {
                            if (Convert.ToInt32(oResult) > 0)
                            {
                                //_ResourceBlockedDates.Add(_Dates[i]);
                                //_ResourceDates.RemoveAt(i);
                                return true;

                            }
                        }


                    //}
                    oDB.Disconnect();
                    
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
                finally
                {
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                }
                return false;
            }

            #endregion

            /// <summary>
            /// Returns a Dates according to Criteria
            /// </summary>
            /// <param name="oCriteria"></param>
            /// <returns></returns>
            public FindRecurrences GetRecurrence(gloCriteria oCriteria, DateTime StartDate, DateTime EndDate)
            {
                FindRecurrences oFindCriteria = new FindRecurrences();

                if (oCriteria.SingleRecurrenceAppointment == SingleRecurrence.Recurrence)
                {
                    //Recurrence Range
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.StartDate = oCriteria.RecurrenceCriteria.Range.StartDate;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndDate = oCriteria.RecurrenceCriteria.Range.EndDate;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndOccurrenceNumber = oCriteria.RecurrenceCriteria.Range.EndOccurrenceNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.NoEndDateYear = oCriteria.RecurrenceCriteria.Range.NoEndDateYear;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag = oCriteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag;

                    //Pattern
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType = oCriteria.RecurrenceCriteria.Pattern.RecurrencePatternType;

                    //Daily 
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = oCriteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = oCriteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay;

                    //weekly
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Monday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Friday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday;

                    //Monthly
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria;

                    //Yearly           
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria;
                }
                else
                {
                    //Recurrence Range
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.StartDate = Convert.ToInt32(oCriteria.SingleCriteria.StartDate);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndDate = Convert.ToInt32(oCriteria.SingleCriteria.EndDate);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndOccurrenceNumber = 1;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.NoEndDateYear = oCriteria.RecurrenceCriteria.Range.NoEndDateYear;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndDate; //oCriteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag;

                    //Pattern
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Daily;

                    //Daily 
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = 1;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryDay;
                }
                oFindCriteria.FindRecurrence(0, StartDate, EndDate);
                return oFindCriteria;
            }

            public FindRecurrences GetRecurrence(gloCriteria oCriteria, Int64 ProviderID, DateTime StartTime, DateTime EndTime)
            {
                FindRecurrences oFindCriteria = new FindRecurrences();

                if (oCriteria.SingleRecurrenceAppointment == SingleRecurrence.Recurrence)
                {
                    //Recurrence Range
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.StartDate = oCriteria.RecurrenceCriteria.Range.StartDate;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndDate = oCriteria.RecurrenceCriteria.Range.EndDate;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndOccurrenceNumber = oCriteria.RecurrenceCriteria.Range.EndOccurrenceNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.NoEndDateYear = oCriteria.RecurrenceCriteria.Range.NoEndDateYear;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag = oCriteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag;

                    //Pattern
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType = oCriteria.RecurrenceCriteria.Pattern.RecurrencePatternType;

                    //Daily 
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = oCriteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = oCriteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay;

                    //weekly
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Monday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Friday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday;

                    //Monthly
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria;

                    //Yearly           
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria;
                }
                else
                {
                    //Recurrence Range
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.StartDate = Convert.ToInt32(oCriteria.SingleCriteria.StartDate);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndDate = Convert.ToInt32(oCriteria.SingleCriteria.EndDate);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndOccurrenceNumber = 1;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.NoEndDateYear = oCriteria.RecurrenceCriteria.Range.NoEndDateYear;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndDate; //oCriteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag;

                    //Pattern
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Daily;

                    //Daily 
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = 1;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryDay;
                }
                oFindCriteria.FindRecurrence(ProviderID,StartTime,EndTime);
                return oFindCriteria;
            }
           
            //added by pranit for resource based
            public FindRecurrences GetRecurrence(gloCriteria oCriteria, Int64 ProviderID, DateTime StartTime, DateTime EndTime,Int64 masterAppointmentID,Int64 nResourceID)
            {
                FindRecurrences oFindCriteria = new FindRecurrences();

                if (oCriteria.SingleRecurrenceAppointment == SingleRecurrence.Recurrence)
                {
                    //Recurrence Range
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.StartDate = oCriteria.RecurrenceCriteria.Range.StartDate;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndDate = oCriteria.RecurrenceCriteria.Range.EndDate;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndOccurrenceNumber = oCriteria.RecurrenceCriteria.Range.EndOccurrenceNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.NoEndDateYear = oCriteria.RecurrenceCriteria.Range.NoEndDateYear;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag = oCriteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag;

                    //Pattern
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType = oCriteria.RecurrenceCriteria.Pattern.RecurrencePatternType;

                    //Daily 
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = oCriteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = oCriteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay;

                    //weekly
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Monday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Friday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday;

                    //Monthly
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria;

                    //Yearly           
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria;
                }
                else
                {
                    //Recurrence Range
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.StartDate = Convert.ToInt32(oCriteria.SingleCriteria.StartDate);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndDate = Convert.ToInt32(oCriteria.SingleCriteria.EndDate);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndOccurrenceNumber = 1;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.NoEndDateYear = oCriteria.RecurrenceCriteria.Range.NoEndDateYear;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndDate; //oCriteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag;

                    //Pattern
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Daily;

                    //Daily 
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = 1;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryDay;
                }
                oFindCriteria.FindRecurrence(ProviderID, StartTime, EndTime,masterAppointmentID,nResourceID);
                return oFindCriteria;
            }

            //added by pranit on 13 sep 2011 for multiple resource based
            public FindRecurrences GetRecurrence(gloCriteria oCriteria, Int64 ProviderID, DateTime StartTime, DateTime EndTime, Int64 masterAppointmentID, StringBuilder nResourceID)
            {
                FindRecurrences oFindCriteria = new FindRecurrences();

                if (oCriteria.SingleRecurrenceAppointment == SingleRecurrence.Recurrence)
                {
                    //Recurrence Range
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.StartDate = oCriteria.RecurrenceCriteria.Range.StartDate;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndDate = oCriteria.RecurrenceCriteria.Range.EndDate;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndOccurrenceNumber = oCriteria.RecurrenceCriteria.Range.EndOccurrenceNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.NoEndDateYear = oCriteria.RecurrenceCriteria.Range.NoEndDateYear;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag = oCriteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag;

                    //Pattern
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType = oCriteria.RecurrenceCriteria.Pattern.RecurrencePatternType;

                    //Daily 
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = oCriteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = oCriteria.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay;

                    //weekly
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.EveryWeekNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Sunday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Monday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Monday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Tuesday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Wednesday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Thursday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Friday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Friday;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday = oCriteria.RecurrenceCriteria.Pattern.WeeklyPattern.Saturday;

                    //Monthly
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayOfMonthOrCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.EveryMonthNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.FirstLastCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria = oCriteria.RecurrenceCriteria.Pattern.MonthlyPattern.DayWeekdayCriteria;

                    //Yearly           
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.EveryDayOfMonthOrCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.DayNumber;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.MonthOfCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.FirstLastCriteria;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria = oCriteria.RecurrenceCriteria.Pattern.YearlyPattern.DayWeekdayCriteria;
                }
                else
                {
                    //Recurrence Range
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.StartDate = Convert.ToInt32(oCriteria.SingleCriteria.StartDate);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndDate = Convert.ToInt32(oCriteria.SingleCriteria.EndDate);
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.EndOccurrenceNumber = 1;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.NoEndDateYear = oCriteria.RecurrenceCriteria.Range.NoEndDateYear;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Range.ReccurrenceRangeFlag = RecurrenceRangeFlag.EndDate; //oCriteria.RecurrenceCriteria.Range.ReccurrenceRangeFlag;

                    //Pattern
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.RecurrencePatternType = RecurrencePatternType.Daily;

                    //Daily 
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayNumber = 1;
                    oFindCriteria.RecurrenceDetail.RecurrenceCriteria.Pattern.DailyPattern.EveryDayOrWeekDay = RecurrencePatternFlag.EveryDay;
                }
                oFindCriteria.FindRecurrence(ProviderID, StartTime, EndTime, masterAppointmentID,nResourceID);
                return oFindCriteria;
            }




            public object GetScheduleConflictTime(string DatabaseConnectionString, string providerId, string nASBaseFlag, Int32 dtStartDate, Int32 dtStartTime, Int32 dtEndDate, Int32 dtEndTime, Int64 ClinicID)
            {

                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                object Count;
                Count = 0;

                oDB.Connect(false);
                
                oDBParameters.Add("@providerId ", providerId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@flag ", nASBaseFlag, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@dtStartDate", dtStartDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtEndDate", dtEndDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@StartTime", dtStartTime, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@EndTime", dtEndTime, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@Count", 0, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.Int);

                oDB.Execute("GetScheduleConflictTime", oDBParameters, out Count);

                oDB.Disconnect();

                oDBParameters.Dispose();
                oDBParameters = null;

                oDB.Dispose();
                oDB = null;


                return Count;
            }
         
         
        }

        public class gloCriteria
        {
            #region "Constructor & Distructor"
               // private string _databaseconnectionstring = "";

                public gloCriteria()
                {
                    _SingleCriteria = new CriteriaTime();
                    _RecurrenceCriteria = new RecurrenceCriteria();
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
                            _RecurrenceCriteria.Dispose();
                            _RecurrenceCriteria = null;

                            _SingleCriteria.Dispose();
                            _SingleCriteria = null;
                        }
                    }
                    disposed = true;
                }

                ~gloCriteria()
                {
                    Dispose(false);
                }

            #endregion

            #region "private variables"
                private SingleRecurrence _SingleRecurrence = SingleRecurrence.Single;
                private CriteriaTime _SingleCriteria;
                private RecurrenceCriteria _RecurrenceCriteria;
            #endregion

            #region "properties"
                public SingleRecurrence SingleRecurrenceAppointment
                {
                    get { return _SingleRecurrence; }
                    set { _SingleRecurrence = value; }
                }

                public CriteriaTime SingleCriteria
                {
                    get { return _SingleCriteria; }
                    set { _SingleCriteria = value; }
                }

                public RecurrenceCriteria RecurrenceCriteria
                {
                    get { return _RecurrenceCriteria; }
                    set { _RecurrenceCriteria = value; }
                }
            #endregion
        }

        #region "Appointment Supporting Classes - Criteria Class"

        // Criteria Supporting(Sub) Classes
        public class RecurrenceCriteria : IDisposable
        {
            #region "Constructor & Destructor"

            public RecurrenceCriteria()
            {
                _RecurrenceAppointmentTime = new CriteriaTime();
                _RecurrencePattern = new RecurrencePattern();
                _RecurrenceRange = new RecurrenceRange();
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
                        _RecurrenceAppointmentTime.Dispose();
                        _RecurrenceAppointmentTime = null;

                        _RecurrencePattern.Dispose();
                        _RecurrencePattern = null;

                        _RecurrenceRange.Dispose();
                        _RecurrenceRange = null;
                    }
                }
                disposed = true;
            }

            ~RecurrenceCriteria()
            {
                Dispose(false);
            }

            #endregion

            #region "private variables"
            private CriteriaTime _RecurrenceAppointmentTime;
            private RecurrencePattern _RecurrencePattern;
            private RecurrenceRange _RecurrenceRange;
            #endregion

            #region "properties"
            public CriteriaTime CriteriaDateTime
            {
                get { return _RecurrenceAppointmentTime; }
                set { _RecurrenceAppointmentTime = value; }
            }

            public RecurrencePattern Pattern
            {
                get { return _RecurrencePattern; }
                set { _RecurrencePattern = value; }
            }

            public RecurrenceRange Range
            {
                get { return _RecurrenceRange; }
                set { _RecurrenceRange = value; }
            }
            #endregion
        }

        public class CriteriaTime : IDisposable
        {
            #region "Constructor & Distructor"

            public CriteriaTime()
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

            ~CriteriaTime()
            {
                Dispose(false);
            }

            #endregion

            #region "private variables"
            private Int64 _nStartTime = 0;
            private Int64 _nEndTime = 0;

            //Added for the start date and time by Sandip..01172008
            //ref .As in appointment
            private Int64 _nStartDate = 0;
            private Int64 _nEndDate = 0;
            //
            private decimal _nDuration = 0;
            #endregion

            #region "properties"
            public Int64 StartTime
            {
                get { return _nStartTime; }
                set { _nStartTime = value; }
            }

            public Int64 EndTime
            {
                get { return _nEndTime; }
                set { _nEndTime = value; }
            }
            //Added for the start date and end time by Sandip..01172008
            //ref . As in appointment

            public Int64 StartDate
            {
                get { return _nStartDate; }
                set { _nStartDate = value; }
            }

            public Int64 EndDate
            {
                get { return _nEndDate; }
                set { _nEndDate = value; }
            }
            //
            public decimal Duration
            {
                get { return _nDuration; }
                set { _nDuration = value; }
            }
            #endregion
        }

        public class RecurrencePattern : IDisposable
        {
            #region "Constructor & Distructor"

            public RecurrencePattern()
            {
                _DailyPattern = new RecurrencePatternDaily();
                _WeeklyPattern = new RecurrencePatternWeekly();
                _MonthlyPattern = new RecurrencePatternMonthly();
                _YearlyPattern = new RecurrencePatternYearly();
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
                        _DailyPattern.Dispose();
                        _DailyPattern = null;

                        _WeeklyPattern.Dispose();
                        _WeeklyPattern = null;

                        _MonthlyPattern.Dispose();
                        _MonthlyPattern = null;

                        _YearlyPattern.Dispose();
                        _YearlyPattern = null;
                    }
                }
                disposed = true;
            }

            ~RecurrencePattern()
            {
                Dispose(false);
            }

            #endregion

            #region "private variables"
            private RecurrencePatternType _RecuurencePatternType = RecurrencePatternType.Daily;
            private RecurrencePatternDaily _DailyPattern;
            private RecurrencePatternWeekly _WeeklyPattern;
            private RecurrencePatternMonthly _MonthlyPattern;
            private RecurrencePatternYearly _YearlyPattern;
            #endregion

            #region "properties"
            public RecurrencePatternType RecurrencePatternType
            {
                get { return _RecuurencePatternType; }
                set { _RecuurencePatternType = value; }
            }

            public RecurrencePatternDaily DailyPattern
            {
                get { return _DailyPattern; }
                set { _DailyPattern = value; }
            }

            public RecurrencePatternWeekly WeeklyPattern
            {
                get { return _WeeklyPattern; }
                set { _WeeklyPattern = value; }
            }

            public RecurrencePatternMonthly MonthlyPattern
            {
                get { return _MonthlyPattern; }
                set { _MonthlyPattern = value; }
            }

            public RecurrencePatternYearly YearlyPattern
            {
                get { return _YearlyPattern; }
                set { _YearlyPattern = value; }
            }

            #endregion
        }

        #region "Recurrence Pattern Supporting"
        public class RecurrencePatternDaily : IDisposable
        {
            #region "Constructor & Distructor"

            public RecurrencePatternDaily()
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

            ~RecurrencePatternDaily()
            {
                Dispose(false);
            }

            #endregion

            #region "private variables"
            private RecurrencePatternFlag _EveryDayOrWeekDay = RecurrencePatternFlag.EveryDay;
            private Int64 _EveryDayNumber = 0;
            #endregion

            #region "properties"
            public RecurrencePatternFlag EveryDayOrWeekDay
            {
                get { return _EveryDayOrWeekDay; }
                set { _EveryDayOrWeekDay = value; }
            }

            public Int64 EveryDayNumber
            {
                get { return _EveryDayNumber; }
                set { _EveryDayNumber = value; }
            }

            #endregion
        }

        public class RecurrencePatternWeekly : IDisposable
        {
            #region "Constructor & Distructor"

            public RecurrencePatternWeekly()
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

            ~RecurrencePatternWeekly()
            {
                Dispose(false);
            }

            #endregion

            #region "private variables"
            private Int64 _EveryWeekNumber = 0;
            private bool _Sunday = false;
            private bool _Monday = false;
            private bool _Tuesday = false;
            private bool _Wednesday = false;
            private bool _Thursday = false;
            private bool _Friday = false;
            private bool _Saturday = false;
            #endregion

            #region "properties"
            public Int64 EveryWeekNumber
            {
                get { return _EveryWeekNumber; }
                set { _EveryWeekNumber = value; }
            }

            public bool Sunday
            {
                get { return _Sunday; }
                set { _Sunday = value; }
            }

            public bool Monday
            {
                get { return _Monday; }
                set { _Monday = value; }
            }

            public bool Tuesday
            {
                get { return _Tuesday; }
                set { _Tuesday = value; }
            }

            public bool Wednesday
            {
                get { return _Wednesday; }
                set { _Wednesday = value; }
            }

            public bool Thursday
            {
                get { return _Thursday; }
                set { _Thursday = value; }
            }

            public bool Friday
            {
                get { return _Friday; }
                set { _Friday = value; }
            }

            public bool Saturday
            {
                get { return _Saturday; }
                set { _Saturday = value; }
            }

            #endregion
        }

        public class RecurrencePatternMonthly : IDisposable
        {
            #region "Constructor & Distructor"

            public RecurrencePatternMonthly()
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

            ~RecurrencePatternMonthly()
            {
                Dispose(false);
            }

            #endregion

            #region "private variables"
            private RecurrencePatternFlag _DayOfMonthOrCriteria = RecurrencePatternFlag.DayOfMonthCriteria;
            private Int64 _DayNumber = 0;
            private Int64 _EveryMonthNumber = 0;
            private FirstLastCriteria _FirstLastCriteria = FirstLastCriteria.first;
            private DayWeekday _DayWeekdayCriteria = DayWeekday.day;
            #endregion

            #region "properties"
            public RecurrencePatternFlag DayOfMonthOrCriteria
            {
                get { return _DayOfMonthOrCriteria; }
                set { _DayOfMonthOrCriteria = value; }
            }

            public Int64 DayNumber
            {
                get { return _DayNumber; }
                set { _DayNumber = value; }
            }

            public Int64 EveryMonthNumber
            {
                get { return _EveryMonthNumber; }
                set { _EveryMonthNumber = value; }
            }

            public FirstLastCriteria FirstLastCriteria
            {
                get { return _FirstLastCriteria; }
                set { _FirstLastCriteria = value; }
            }

            public DayWeekday DayWeekdayCriteria
            {
                get { return _DayWeekdayCriteria; }
                set { _DayWeekdayCriteria = value; }
            }
            #endregion
        }

        public class RecurrencePatternYearly : IDisposable
        {
            #region "Constructor & Distructor"

            public RecurrencePatternYearly()
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

            ~RecurrencePatternYearly()
            {
                Dispose(false);
            }

            #endregion

            #region "private variables"
            private RecurrencePatternFlag _EveryDayOfMonthOrCriteria = RecurrencePatternFlag.DayOfMonthCriteria;
            private Int64 _DayNumber = 0;
            private MonthRange _MonthOfCriteria = 0;
            private FirstLastCriteria _FirstLastCriteria = FirstLastCriteria.first;
            private DayWeekday _DayWeekdayCriteria = DayWeekday.day;
            #endregion

            #region "properties"
            public RecurrencePatternFlag EveryDayOfMonthOrCriteria
            {
                get { return _EveryDayOfMonthOrCriteria; }
                set { _EveryDayOfMonthOrCriteria = value; }
            }

            public Int64 DayNumber
            {
                get { return _DayNumber; }
                set { _DayNumber = value; }
            }

            public MonthRange MonthOfCriteria
            {
                get { return _MonthOfCriteria; }
                set { _MonthOfCriteria = value; }
            }

            public FirstLastCriteria FirstLastCriteria
            {
                get { return _FirstLastCriteria; }
                set { _FirstLastCriteria = value; }
            }

            public DayWeekday DayWeekdayCriteria
            {
                get { return _DayWeekdayCriteria; }
                set { _DayWeekdayCriteria = value; }
            }
            #endregion
        }
        #endregion

        public class RecurrenceRange : IDisposable
        {
            #region "Constructor & Distructor"

            public RecurrenceRange()
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

            ~RecurrenceRange()
            {
                Dispose(false);
            }

            #endregion

            #region "private variables"
            private Int32 _nStartDate = 0;
            private RecurrenceRangeFlag _RecurrenceRangeFlag = RecurrenceRangeFlag.EndDate;
            private Int32 _nEndDate = 0;
            private Int64 _nEndOccurrenceNumber = 0;
            private Int64 _nNoEndDateYear = 0;
            #endregion

            #region "properties"
            public Int32 StartDate
            {
                get { return _nStartDate; }
                set { _nStartDate = value; }
            }

            public RecurrenceRangeFlag ReccurrenceRangeFlag
            {
                get { return _RecurrenceRangeFlag; }
                set { _RecurrenceRangeFlag = value; }
            }

            public Int32 EndDate
            {
                get { return _nEndDate; }
                set { _nEndDate = value; }
            }

            public Int64 EndOccurrenceNumber
            {
                get { return _nEndOccurrenceNumber; }
                set { _nEndOccurrenceNumber = value; }
            }

            public Int64 NoEndDateYear
            {
                get { return _nNoEndDateYear; }
                set { _nNoEndDateYear = value; }
            }
            #endregion
        }
        #endregion

    }
}
