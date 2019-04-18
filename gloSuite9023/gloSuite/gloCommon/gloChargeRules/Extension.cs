using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ChargeRules
{
    public static class MyExtension
    {
        public static bool ParseExpression(this string expressionString)
        {
            bool expResult = false;
            DataTable dtTemp = new DataTable();

            try
            {
                expResult = Convert.ToBoolean(dtTemp.Compute(expressionString, ""));
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                if (dtTemp != null) { dtTemp.Dispose(); dtTemp = null; }
                throw ex;
            }
            
            return expResult;
        }

        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
        {

            try
            {
                var array = new T[listToClone.Count];
                listToClone.CopyTo(array, 0);
                return array.ToList();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                throw ex;
                
            }
        }
    }
}
