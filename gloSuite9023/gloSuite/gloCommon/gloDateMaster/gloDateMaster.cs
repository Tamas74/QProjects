using System;
using System.Collections.Generic;
using System.Text;

namespace gloDateMaster
{
    public static class gloDate
    {
        //This class is based on MM/dd/yyyy

        public static Int32 DateAsNumber(string datevalue)
        {
            Int32 _result = 0;
            DateTime _internaldate = Convert.ToDateTime(datevalue);
            datevalue = string.Format(_internaldate.ToShortDateString(), "MM/dd/yyyy");
            try
            {
                if (datevalue.Length == 10)
                {
                    string _internalresult = "";
                    _internalresult = datevalue.Substring(6, 4) + datevalue.Substring(0, 2) + datevalue.Substring(3, 2);
                    _result = Convert.ToInt32(_internalresult);
                    _internalresult = null; 
                }
                else if (datevalue.Length == 9)
                {
                    string _internalresult = "";
                    if (_internaldate.Month <= 9) // 1/11/2007
                    {
                        _internalresult = datevalue.Substring(5, 4) + "0" + datevalue.Substring(0, 1) + datevalue.Substring(2, 2);
                    }
                    else if (_internaldate.Day <= 9) // 11/2/2007
                    {
                        _internalresult = datevalue.Substring(5, 4) + datevalue.Substring(0, 2) + "0" + datevalue.Substring(3, 1);
                    }


                    _result = Convert.ToInt32(_internalresult);
                    _internalresult = null; 
                }
                else if (datevalue.Length == 8)
                {
                    string _internalresult = "";
                    _internalresult = datevalue.Substring(4, 4) + "0" + datevalue.Substring(0, 1) + "0" + datevalue.Substring(2, 1);
                    _result = Convert.ToInt32(_internalresult);
                    _internalresult = null; 
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result;
        }

        //public static Int32 DateAsNumber(DateTime datevalue)
        //{
        //    Int32 _result = 0;
        //    try
        //    {
        //        if (datevalue.ToString().Length == 10)
        //        {
        //            string _internalresult = "";
        //            _internalresult = datevalue.ToString().Substring(6, 4) + datevalue.ToString().Substring(0, 2) + datevalue.ToString().Substring(3, 2);
        //            _result = Convert.ToInt32(_internalresult);
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    return _result;
        //}

        //public static string DateAsString(Int32 datevalue)
        //{
        //    string _result = "";
        //    try
        //    {
        //        if (datevalue.ToString().Length == 8)
        //        {
        //            string _internalresult = datevalue.ToString();
        //            _result = _internalresult.Substring(4, 2) + "/" + _internalresult.Substring(6, 2) + "/" + _internalresult.Substring(0, 4);
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    return _result;
        //}

        public static DateTime DateAsDate(Int64 datevalue)
        {
            DateTime _result = DateTime.Now;
            try
            {
                if (datevalue.ToString().Length == 8)
                {
                    string _internalresult = datevalue.ToString();
                    string _internaldate = "";
                    _internaldate = _internalresult.Substring(4, 2) + "/" + _internalresult.Substring(6, 2) + "/" + _internalresult.Substring(0, 4);
                    _result = Convert.ToDateTime(_internaldate);
                    _internalresult = null; 
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result;
        }

        public static string DateAsStringDate(string datevalue)
        {
            string _internaldate = "";
            try
            {
                if (datevalue.ToString().Length >= 8)
                {
                    string _internalresult = datevalue.ToString();
                    string sValidation = datevalue.Substring(0, 8);

                    Int64 validation = 0;

                    if (Int64.TryParse(sValidation, out validation))
                    {
                        _internaldate = _internalresult.Substring(4, 2) + "/" + _internalresult.Substring(6, 2) + "/" + _internalresult.Substring(0, 4);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _internaldate;
        }


        public static string DateAsDateString(Int64 datevalue)
        {
            string _internaldate = "";
            try
            {
                if (datevalue.ToString().Length == 8)
                {
                    string _internalresult = datevalue.ToString();
                   
                    _internaldate = _internalresult.Substring(4, 2) + "/" + _internalresult.Substring(6, 2) + "/" + _internalresult.Substring(0, 4);
                   
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _internaldate;
        }
        public static string QRDADateAsDateString(string datevalue1)
        {
            Int64 datevalue;
            string _internaldate = "";
            try
            {
                if (datevalue1.IndexOf("+") > 0)
                {
                    datevalue1 = datevalue1.Substring(0, datevalue1.IndexOf("+"));
                }
                else if (datevalue1.IndexOf("-") > 0)
                {
                    datevalue1 = datevalue1.Substring(0, datevalue1.IndexOf("-"));
                }
                datevalue = Convert.ToInt64(datevalue1);

                if (datevalue.ToString().Length == 12 || datevalue.ToString().Length == 14)
                {
                    string _internalresult = datevalue.ToString();

                    _internaldate = _internalresult.Substring(4, 2) + "/" + _internalresult.Substring(6, 2) + "/" + _internalresult.Substring(0, 4);

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _internaldate;
        }
       
        public static bool IsValidDate(string datevalue)
        {
            bool _result = false;
            try
            {
                #region " COMMENTED BY SUDHIR 20100722 - WRONG LOGIC "
                //if (datevalue.Length == 10)
                //{
                //    Int32 _Day = Convert.ToInt32(datevalue.Substring(3, 2));
                //    Int32 _Month = Convert.ToInt32(datevalue.Substring(0, 2));
                //    Int32 _Year = Convert.ToInt32(datevalue.Substring(6, 4));

                //    if (_Month <= 0 && _Month > 12)
                //    {
                //        return false;
                //    }

                //    if (_Month == 4 || _Month == 6 || _Month == 9 || _Month == 11)
                //    {
                //        if (_Day > 30)
                //        {
                //            return false;
                //        }
                //    }
                //    if (_Month == 2)
                //    {
                //        if (DateTime.IsLeapYear(_Year) == true)
                //        {
                //            if (_Day > 29)
                //            {
                //                return false;
                //            }
                //        }
                //        else
                //        {
                //            if (_Day > 28)
                //            {
                //                return false;
                //            }
                //        }
                //    }

                //    if (_Year < 1900) { return false; }
                //    if (_Year < 3000) { return false; }

                //    _result = true;
                //}
                #endregion
                
                DateTime _dt = Convert.ToDateTime(datevalue);
                _result = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            return _result;
        }

        public static bool IsValidDateV2(object strDate)
        {
            bool Success;
            try
            {
                DateTime validatedDate;
                Success = DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);
                if (validatedDate != null && Success == true)
                {
                    if (validatedDate < DateTime.MaxValue && validatedDate >= Convert.ToDateTime("01/01/1900"))
                    {
                        Success = true;
                    }
                    else
                    {
                        Success = false;
                    }

                }
            }
            catch (FormatException)
            {
                Success = false; // If this line is reached, an exception was thrown

            }
            return Success;
        }

        /// <summary>
        /// This function can be used to validate the date in the format MM/dd/yyyy.
        /// <example>
        /// bool _isValid = gloDate.IsValid(mskDate.Text);
        /// </example>
        /// </summary>
        /// <param name="DateToValidate"></param>
        /// <returns></returns>
        public static bool IsValid(string DateToValidate)
        {
            bool Success;
            try
            {
                DateTime validatedDate;
                Success = DateTime.TryParseExact(DateToValidate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);
                if (validatedDate != null && Success == true)
                {
                    if (validatedDate < DateTime.MaxValue && validatedDate >= Convert.ToDateTime("01/01/1900"))
                    {
                        Success = true;
                    }
                    else
                    {
                        Success = false;
                    }

                }
            }
            catch
            {
                Success = false; // If this line is reached, an exception was thrown
            }
            return Success;
        }


        public static Int64 DateTimeAsNumber(string DateTimeValue)
        {
            Int64 _result = 0;
            string datevalue;
            string timevalue;
            DateTime _internaldate = Convert.ToDateTime(DateTimeValue);
            datevalue = string.Format(_internaldate.ToShortDateString(), "MM/dd/yyyy");

            string _AmPm = null;
            string _Hour = null;
            string _Minutes = null;
            string _HourNo = null;
            string _MinuteNo = null;
            try
            {
                if (datevalue.Length == 10)
                {
                    string _internalresult = "";
                    _internalresult = datevalue.Substring(6, 4) + datevalue.Substring(0, 2) + datevalue.Substring(3, 2);
                    _result = Convert.ToInt64(_internalresult);
                    _internalresult = null;
                }
                else if (datevalue.Length == 9)
                {
                    string _internalresult = "";
                    if (_internaldate.Month <= 9) // 1/11/2007
                    {
                        _internalresult = datevalue.Substring(5, 4) + "0" + datevalue.Substring(0, 1) + datevalue.Substring(2, 2);
                    }
                    else if (_internaldate.Day <= 9) // 11/2/2007
                    {
                        _internalresult = datevalue.Substring(5, 4) + datevalue.Substring(0, 2) + "0" + datevalue.Substring(3, 1);
                    }
                    _result = Convert.ToInt64(_internalresult);
                    _internalresult = null;
                }
                else if (datevalue.Length == 8)
                {
                    string _internalresult = "";
                    _internalresult = datevalue.Substring(4, 4) + "0" + datevalue.Substring(0, 1) + "0" + datevalue.Substring(2, 1);
                    _result = Convert.ToInt64(_internalresult);
                    _internalresult = null;
                }


                // Time 
                timevalue = string.Format(Convert.ToDateTime(DateTimeValue).ToShortTimeString(), "hh:mm tt");

                if (timevalue.Length <= 8)
                {
                    if (timevalue.Substring(timevalue.Length - 2, 2).ToUpper().Trim() == "AM" || timevalue.Substring(timevalue.Length - 2, 2).ToUpper().Trim() == "PM")
                    {
                        string _internalresult = "";
                        _AmPm = timevalue.Substring(timevalue.Length - 2, 2).ToUpper().Trim();
                        _Hour = timevalue.Substring(0, timevalue.IndexOf(":", 0)).ToUpper().Trim();
                        _Minutes = timevalue.Substring(timevalue.IndexOf(":", 0) + 1, 2).ToUpper().Trim();
                        _HourNo = "";
                        _MinuteNo = "";

                        if (_AmPm == "PM")
                        {
                            if (_Hour != "12")
                            {
                                _HourNo = Convert.ToString(Convert.ToInt16(_Hour) + 12);
                            }
                            else
                            {
                                _HourNo = _Hour;
                            }
                        }
                        else
                        {
                            if (_Hour == "12")
                            {
                                _HourNo = "";
                            }
                            else
                            {
                                _HourNo = _Hour;
                            }
                        }
                        _MinuteNo = _Minutes;

                        _internalresult = _HourNo + _MinuteNo;
                        _result = Convert.ToInt64(_result + "" + _internalresult);
                        _internalresult = null;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _AmPm = null;
                _Hour = null;
                _Minutes = null;
                _HourNo = null;
                _MinuteNo = null;
            }
            return _result;
        }

        public static DateTime DateAsDateTime(Int64 datetimevalue)
        {
            DateTime _result = DateTime.Now;
            string _internalresult = null;
            string _internaldate = null;
            string _AmPm = "";
            string _internaltime = "";
            string _timeValue = null;

            try
            {
                if (datetimevalue.ToString().Length == 12)
                {
                    _internalresult = datetimevalue.ToString();
                    _internaldate = "";
                    _internaldate = _internalresult.Substring(4, 2) + "/" + _internalresult.Substring(6, 2) + "/" + _internalresult.Substring(0, 4);
                    _result = Convert.ToDateTime(_internaldate);

                    // Time 
                    _timeValue = datetimevalue.ToString();
                    _timeValue = _timeValue.Substring(8, 4);
                    int _Hour = 0;// Convert.ToInt16(_timeValue.Substring(0, _timeValue.Length - 2).ToUpper().Trim());
                    int _Minutes = 0;// Convert.ToInt16(_timeValue.Substring(_timeValue.Length - 2).ToUpper().Trim());

                    if (Convert.ToInt64(_timeValue) > 0)
                    { _Hour = Convert.ToInt16(_timeValue.Substring(0, _timeValue.Length - 2).ToUpper().Trim()); }

                    if (Convert.ToInt64(_timeValue) > 0)
                    { _Minutes = Convert.ToInt16(_timeValue.Substring(_timeValue.Length - 2).ToUpper().Trim()); }

                    // string _internalresult;

                    if (_Hour < 12)
                    {
                        _AmPm = "AM";
                    }
                    else if (_Hour >= 12)
                    {
                        _AmPm = "PM";
                    }

                    _internaltime = _Hour.ToString() + ":" + _Minutes.ToString() + " " + _AmPm;
                    _result = Convert.ToDateTime(string.Format(Convert.ToDateTime(_internaldate).ToShortDateString(), "MM/dd/yyyy") + " " + string.Format(Convert.ToDateTime(_internaltime).ToShortTimeString(), "hh:mm tt"));
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _internalresult = null;
                _internaldate = null;
                _AmPm = null;
                _internaltime = null;
                _timeValue = null;
            }
            return _result;
        }
        public static DateTime QRDADateAsDateTime(string datetimevalue1)
        {
            Int64 datetimevalue;
            
            DateTime _result = DateTime.Now;
            string _internalresult = null;
            string _internaldate = null;
            string _AmPm = "";
            string _internaltime = "";
            string _timeValue = null;

            try
            {
                if (datetimevalue1.IndexOf ("+")>0)
                {
                     datetimevalue1 = datetimevalue1.Substring(0, datetimevalue1.IndexOf("+"));
                }
                else if (datetimevalue1.IndexOf ("-")>0)
                {
                    datetimevalue1 = datetimevalue1.Substring(0, datetimevalue1.IndexOf("-"));
                }
                datetimevalue = Convert.ToInt64 ( datetimevalue1);

                if (datetimevalue.ToString().Length == 12 || datetimevalue.ToString().Length == 14)
                {
                    _internalresult = datetimevalue.ToString();
                    _internaldate = "";
                    _internaldate = _internalresult.Substring(4, 2) + "/" + _internalresult.Substring(6, 2) + "/" + _internalresult.Substring(0, 4);
                    _result = Convert.ToDateTime(_internaldate);

                    // Time 
                    _timeValue = datetimevalue.ToString();
                    _timeValue = _timeValue.Substring(8, 4);
                    int _Hour = 0;// Convert.ToInt16(_timeValue.Substring(0, _timeValue.Length - 2).ToUpper().Trim());
                    int _Minutes = 0;// Convert.ToInt16(_timeValue.Substring(_timeValue.Length - 2).ToUpper().Trim());

                    if (Convert.ToInt64(_timeValue) > 0)
                    { _Hour = Convert.ToInt16(_timeValue.Substring(0, _timeValue.Length - 2).ToUpper().Trim()); }

                    if (Convert.ToInt64(_timeValue) > 0)
                    { _Minutes = Convert.ToInt16(_timeValue.Substring(_timeValue.Length - 2).ToUpper().Trim()); }

                    // string _internalresult;

                    if (_Hour < 12)
                    {
                        _AmPm = "AM";
                    }
                    else if (_Hour >= 12)
                    {
                        _AmPm = "PM";
                    }

                    _internaltime = _Hour.ToString() + ":" + _Minutes.ToString() + " " + _AmPm;
                    _result = Convert.ToDateTime(string.Format(Convert.ToDateTime(_internaldate).ToShortDateString(), "MM/dd/yyyy") + " " + string.Format(Convert.ToDateTime(_internaltime).ToShortTimeString(), "hh:mm tt"));
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _internalresult = null;
                _internaldate = null;
                _AmPm = null;
                _internaltime = null;
                _timeValue = null;
            }
            return _result;
        }
    }


    public static class gloTime
    {
        public static Int32 TimeAsNumber(string timevalue)
        {
            Int32 _result = 0;
            timevalue = string.Format(Convert.ToDateTime(timevalue).ToShortTimeString(), "hh:mm tt");
            string _internalresult = "";
            string _AmPm = null;
            string _Hour = null;
            string _Minutes = null;
            string _HourNo = "";
            string _MinuteNo = "";
            try
            {
                if (timevalue.Length <= 8)
                {
                    if (timevalue.Substring(timevalue.Length - 2, 2).ToUpper().Trim() == "AM" || timevalue.Substring(timevalue.Length - 2, 2).ToUpper().Trim() == "PM")
                    {
                        _AmPm = timevalue.Substring(timevalue.Length - 2, 2).ToUpper().Trim();
                        _Hour = timevalue.Substring(0, timevalue.IndexOf(":", 0)).ToUpper().Trim();
                        _Minutes = timevalue.Substring(timevalue.IndexOf(":", 0) + 1, 2).ToUpper().Trim();

                        if (_AmPm == "PM")
                        {
                            if (_Hour != "12")
                            {
                                _HourNo = Convert.ToString(Convert.ToInt16(_Hour) + 12);
                            }
                            else
                            {
                                _HourNo = _Hour;
                            }
                        }
                        else
                        {
                            if (_Hour == "12")
                            {
                                _HourNo = "";
                            }
                            else
                            {
                                _HourNo = _Hour;
                            }

                        }
                        _MinuteNo = _Minutes;

                        _internalresult = _HourNo + _MinuteNo;
                        _result = Convert.ToInt32(_internalresult);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _internalresult = null;
                _AmPm = null;
                _Hour = null;
                _Minutes = null;
                _HourNo = null;
                _MinuteNo = null;
            }
            return _result;
        }

        //public static Int32 TimeAsNumber(DateTime timevalue)
        //{
        //    Int32 _result = 0;
        //    try
        //    {

        //    }
        //    catch
        //    {
        //    }
        //    return _result;
        //}

        //public static string TimeAsString(Int32 timevalue)
        //{
        //    string _result = "";
        //    try
        //    {

        //    }
        //    catch
        //    {
        //    }
        //    return _result;
        //}

        public static DateTime TimeAsDateTime(DateTime SupportingDate, Int32 timevalue)
        {
            DateTime _result = DateTime.Now;
            try
            {
                string _timeValue = timevalue.ToString();

                if (_timeValue.Length > 2 && _timeValue.Length <= 4)
                {
                    string _AmPm = "";
                    int _Hour = 0;// Convert.ToInt16(_timeValue.Substring(0, _timeValue.Length - 2).ToUpper().Trim());
                    int _Minutes = 0;// Convert.ToInt16(_timeValue.Substring(_timeValue.Length - 2).ToUpper().Trim());

                    if (timevalue > 0)
                    { _Hour = Convert.ToInt16(_timeValue.Substring(0, _timeValue.Length - 2).ToUpper().Trim()); }

                    if (timevalue > 0)
                    { _Minutes = Convert.ToInt16(_timeValue.Substring(_timeValue.Length - 2).ToUpper().Trim()); }

                    string _internalresult;

                    if (_Hour < 12)
                    {
                        _AmPm = "AM";
                    }
                    else if (_Hour >= 12)
                    {
                        _AmPm = "PM";
                    }

                    _internalresult = _Hour.ToString() + ":" + _Minutes.ToString() + " " + _AmPm;

                    _result = Convert.ToDateTime(SupportingDate.Date.ToString("MM/dd/yyyy") + " " + _internalresult);
                    _internalresult = null;
                    _AmPm = null;
                }
                else if (_timeValue.Length <= 2)
                {
                    string _AmPm = "";
                    int _Hour = 0;// Convert.ToInt16(_timeValue.Substring(0, _timeValue.Length - 2).ToUpper().Trim());
                    int _Minutes = 0;// Convert.ToInt16(_timeValue.Substring(_timeValue.Length - 2).ToUpper().Trim());

                    _Hour = 0;

                    _Minutes = Convert.ToInt16(_timeValue);

                    string _internalresult;

                    if (_Hour < 12)
                    {
                        _AmPm = "AM";
                    }
                    else if (_Hour >= 12)
                    {
                        _AmPm = "PM";
                    }

                    _internalresult = _Hour.ToString() + ":" + _Minutes.ToString() + " " + _AmPm;

                    _result = Convert.ToDateTime(SupportingDate.Date.ToString("MM/dd/yyyy") + " " + _internalresult);
                    _internalresult = null;
                    _AmPm = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result;
        }

        public static bool IsValidTime(string timevalue)
        {
            bool _result = false;
            try
            {

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result;
        }

        public static Int64 GetMinutes(Int64 nMinutes)
        {
            nMinutes = System.Math.Abs(nMinutes);
            Int64 nHrs = 0;
            Int64 nMins = 0;

            nHrs = (nMinutes / 100);
            nMins = (nMinutes % 100);
            return (nHrs * 60) + nMins;
        }

        public static Int64 GetMinutes(Int64 nMinute1, Int64 nMinute2)
        {
            Int64 nMinutes;
            Int64 nHrs = 0;
            Int64 nMins = 0;

            nMinutes = System.Math.Abs(nMinute1 - nMinute2);

            nHrs = (nMinutes / 100);
            nMins = (nMinutes % 100);
            return (nHrs * 60) + nMins;
        }


    }
}
