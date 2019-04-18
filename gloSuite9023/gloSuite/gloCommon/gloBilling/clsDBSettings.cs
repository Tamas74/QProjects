using System;
using System.Collections.Generic;
using System.Text;

using gloSettings;

namespace gloBilling
{
    public static class BillingSettings
    {
        public static Int64 LastSelectedPaymentTrayID
        {
            get
            {
                Int64 _lastPaymentTrayID = 0;

                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
                Object _retSettingValue = null;
                oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", AppSettings.UserID, AppSettings.ClinicID, out _retSettingValue);
                oSettings.Dispose();

                if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                { _lastPaymentTrayID = Convert.ToInt64(_retSettingValue); }

                return _lastPaymentTrayID;
            }
        }

        public static string LastSelectedCloseDate
        {
            get
            {
                string _lastCloseDate = string.Empty;

                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
                Object _retValue = null;

                oSettings.GetSetting("PAYMENT_LASTCLOSEDATE", AppSettings.UserID, AppSettings.ClinicID, out _retValue);
                oSettings.Dispose();

                if (_retValue != null && Convert.ToString(_retValue).Trim() != "")
                {
                    try
                    { _lastCloseDate = Convert.ToDateTime(Convert.ToString(_retValue).Trim()).ToString("MM/dd/yyyy"); }
                    catch
                    { _lastCloseDate = DateTime.Now.Date.ToString("MM/dd/yyyy"); }
                }
                else
                { _lastCloseDate = DateTime.Now.Date.ToString("MM/dd/yyyy"); }

                return _lastCloseDate;
            }
        }

    }
}
