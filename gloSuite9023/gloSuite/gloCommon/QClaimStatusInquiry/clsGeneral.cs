using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace TriArqEDIRealTimeClaimStatus
{
    public class clsGeneral
    {
        public enum RequestType
        {
            RealTime = 1,
            Batch = 2
        }

        public static string ControlNumberGeneration()
        {
            string strNumber = String.Empty;
            strNumber = "0";
            try
            {
                //while loop added to avoid preceding zeros as X12 standards doesn't accepts leading zeros.
                strNumber = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);

                if (strNumber.StartsWith("0") == true)
                {
                    strNumber = strNumber.PadLeft(10, '1').Remove(strNumber.Length - 1, 1);
                }
            }
            catch (Exception ex)
            {
               clsQEDILogs.ExceptionLog(ActivityModule.EDIGeneral, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            return strNumber;
        }

        public static string FormattedTime(string TimeFormat)
        {
            int _length = 0;
            try
            {
                _length = TimeFormat.Length;
                if (_length == 0)
                {
                    TimeFormat = "0000";
                }
                if (_length == 1)
                {
                    TimeFormat = "000" + TimeFormat;
                }
                else if (_length == 2)
                {
                    TimeFormat = "00" + TimeFormat;
                }
                else if (_length == 3)
                {
                    TimeFormat = "0" + TimeFormat;
                }
                else if (_length == 4)
                {
                    //       TimeFormat = TimeFormat;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDIGeneral, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            return TimeFormat;
        }

        public static Int32 DateAsNumber(string datevalue)
        {
            Int32 _result = 0;
            DateTime _internaldate;
            try
            {
                _internaldate = Convert.ToDateTime(datevalue);
                datevalue = string.Format(_internaldate.ToShortDateString(), "MM/dd/yyyy");
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
               clsQEDILogs.ExceptionLog(ActivityModule.EDIGeneral, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            return _result;
        }

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
                clsQEDILogs.ExceptionLog(ActivityModule.EDIGeneral, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            return _result;
        }

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
                clsQEDILogs.ExceptionLog(ActivityModule.EDIGeneral, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
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

        public static string GetConnectionString(bool WindowsAuthentication, string SQLServerName, string Database, string LoginUser, string LoginPassword)
        {
            string _connstring = "";
            try
            {
                if (WindowsAuthentication == false)//SQL authentication
                {
                    _connstring = "Server=" + SQLServerName + ";Database=" + Database + ";Uid=" + LoginUser + ";Pwd=" + LoginPassword + ";";
                }
                else//windows authentication
                {
                    _connstring = "Server=" + SQLServerName + ";Database=" + Database + ";Trusted_Connection=yes;";
                }
            }
            catch (Exception ex)
            {
               clsQEDILogs.ExceptionLog(ActivityModule.EDIGeneral, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            return _connstring;
        }
    }

    public class clsEncryption
    {
        public static String constEncryptDecryptKeyDB = "12345678";

        #region "Destructor"

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
                    key = null;
                    IV = null;
                }
            }
            disposed = true;
        }

        ~clsEncryption()
        {
            Dispose(false);
        }

        #endregion "Destructor"

        // Use DES CryptoService with Private key pair
        private byte[] key = new byte[] { }; // we are going to pass in the key portion in our method calls
        private byte[] IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        public string DecryptFromBase64String(string stringToDecrypt, string sEncryptionKey)
        {
            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            // Note: The DES CryptoService only accepts certain key byte lengths
            // We are going to make things easy by insisting on an 8 byte legal key length
            MemoryStream ms = null;
            CryptoStream cs = null;
            DESCryptoServiceProvider des = null;
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                des = new DESCryptoServiceProvider();
                // we have a base 64 encoded string so first must decode to regular unencoded (encrypted) string
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                // now decrypt the regular string
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());

            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                inputByteArray = null;
                if (des != null)
                {
                    des.Dispose(); des = null;
                }
                if (ms != null)
                {
                    ms.Dispose(); ms = null;
                }
                if (cs != null)
                {
                    cs.Dispose(); cs = null;
                }
            }
        }

        public string EncryptToBase64String(string stringToEncrypt, string SEncryptionKey)
        {
            DESCryptoServiceProvider des = null;
            byte[] inputByteArray = null;
            MemoryStream ms = null;
            CryptoStream cs = null;
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey.Substring(0, 8));
                des = new DESCryptoServiceProvider();
                // convert our input string to a byte array
                inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                //now encrypt the bytearray
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                // now return the byte array as a "safe for XMLDOM" Base64 String
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                if (des != null)
                { des.Dispose(); des = null; }
                inputByteArray = null;
                if (ms != null)
                { ms.Dispose(); ms = null; }
                if (cs != null)
                { cs.Dispose(); cs = null; }
            }
        }
    }
}
