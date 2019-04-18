using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ChargeRules
{
    public static class RulesRepository
    {
        public enum CacheListType
        {
            All = 0,
            RulesObject = 1,
            RulesDataSet = 2
        }

        public static List<ChargeRules.Rule> GetRules(bool bIsServiceCall=false,string sAUSID="")
        {
            System.Data.DataSet cachedRuleDataSet = null;
            List<ChargeRules.Rule> chargeRules = null;
            ClsRuleEngine objClsRuleEngine = null;

            if (RulesCache.IsExists(CacheListType.RulesDataSet.ToString()) == true)
            {
                cachedRuleDataSet = RulesCache.GetDataSet(CacheListType.RulesDataSet.ToString());
            }
            else
            {
                objClsRuleEngine = new ClsRuleEngine();
                cachedRuleDataSet = objClsRuleEngine.GetRules(0,false,bIsServiceCall,sAUSID);
                RulesCache.Add(cachedRuleDataSet.Copy(), CacheListType.RulesDataSet.ToString());
            }

            BusinessObjects businessObjects = new BusinessObjects();
            chargeRules = businessObjects.GetRules(cachedRuleDataSet);
            if (businessObjects != null) { businessObjects.Dispose(); businessObjects = null; }
            
            return chargeRules;
        }

        public static void ClearRulesCache()
        { 
            RulesCache.Clear();
        }

        private static class RulesCache
        {
            private static Hashtable oRulesCache = new Hashtable();

            public static void Add(List<ChargeRules.Rule> oValue, String sSPnameAsKey)
            {
                try
                {
                    if (!oRulesCache.ContainsKey(sSPnameAsKey))
                    {
                        oRulesCache.Add(sSPnameAsKey, oValue);
                    }
                }
                catch
                {


                }
            }

            public static void Add(System.Data.DataSet oValue, String sSPnameAsKey)
            {
                try
                {
                    if (!oRulesCache.ContainsKey(sSPnameAsKey))
                    {
                        oRulesCache.Add(sSPnameAsKey, oValue);
                    }
                }
                catch
                {


                }
            }

            public static Boolean IsExists(String sSPnameAsKey)
            {
                try
                {
                    return oRulesCache.ContainsKey(sSPnameAsKey);
                }
                catch //(Exception ex)
                {

                }
                return false;
            }

            public static List<ChargeRules.Rule> Get(String sSPnameAsKey)
            {
                List<ChargeRules.Rule> chargeRules = null;

                try
                {
                    if (oRulesCache.ContainsKey(sSPnameAsKey))
                    {
                        chargeRules = (List<ChargeRules.Rule>)oRulesCache[sSPnameAsKey];
                    }
                    
                }
                catch //(Exception ex)
                {

                }
                finally 
                { 
                    
                }

                return chargeRules;
            }

            public static System.Data.DataSet GetDataSet(String sSPnameAsKey)
            {
                System.Data.DataSet oResult = null;
                try
                {
                    if (oRulesCache.ContainsKey(sSPnameAsKey))
                    {
                        oResult = (System.Data.DataSet)oRulesCache[sSPnameAsKey];
                    }
                }
                catch //(Exception ex)
                {
                    oResult = null;
                }
                return oResult;
            }

            public static void Remove(String sSPnameAsKey)
            {
                try
                {
                    oRulesCache.Remove(sSPnameAsKey);
                }
                catch //(Exception ex)
                {
                }
            }

            public static void Clear()
            {
                try
                {
                    oRulesCache.Clear();
                }
                catch //(Exception ex)
                {

                }
            }
        }
    }
}
