using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using gloPatient;
using gloSettings;
using gloBilling.Common;

using System.Data.SqlClient;
using gloBilling.EOBPayment.Common;

namespace gloBilling.Payment
{
    partial class PaymentBase
    {
        public static bool IsExistCheck(string CheckNumber, Int64 CheckDate, decimal CheckAmt, EOBPaymentAccountType _EOBPaymentAccountType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            Object _retVal = false;
            string _sqlQuery = "";
            bool _IsExistCheck = false;

            try
            {
                if (CheckNumber.ToString().Trim() != "")
                {
                    oDB.Connect(false);
                    _sqlQuery = "SELECT COUNT(*) FROM BL_EOBPayment_MST WITH (NOLOCK)" +
                    " WHERE UPPER(sCheckNumber) = '" + CheckNumber.Trim().ToUpper().Replace("'", "''") + "' " +
                    " AND nCheckDate = " + CheckDate + " AND nCheckAmount = " + CheckAmt + " " +
                    " AND sCheckNumber IS NOT NULL AND nCheckDate IS NOT NULL AND nCheckAmount IS NOT NULL AND ISNULL(bIsVoid,0) = 0 " +
                    " AND ISNULL(nVoidType,0) = 0 AND nPayerType = " + _EOBPaymentAccountType.GetHashCode() + " ";
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    { _IsExistCheck = Convert.ToBoolean(_retVal); }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }

            return _IsExistCheck;
        }
    }
}
