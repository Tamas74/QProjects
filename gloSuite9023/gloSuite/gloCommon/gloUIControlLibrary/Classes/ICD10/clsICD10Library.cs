using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace gloUIControlLibrary.Classes.ICD10
{
    public static class gloICD10Library
    {
        public static XmlDataProvider GetICD10Notes(string dbConnectionString)
        {
            XmlDataProvider xmlDataProvider = null;
            XmlDocument xmlNotesDocument = null;

            if (gloICD10Cache.IsExists("ICD10Notes") == true)
            {
                xmlDataProvider = gloICD10Cache.Get("ICD10Notes");
            }
            else
            {
                xmlDataProvider = new XmlDataProvider();
                xmlNotesDocument = new XmlDocument();

                try
                {
                    using (SqlConnection con = new SqlConnection(dbConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("ICD10_GetNotes", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                using (DataTable dt = new DataTable())
                                {
                                    da.Fill(dt);

                                    if (dt != null && dt.Rows.Count >= 0)
                                    {
                                        string sXML = Convert.ToString(dt.Rows[0]["XMLDATA"]);
                                        xmlNotesDocument.LoadXml(sXML);
                                        xmlDataProvider.Document = xmlNotesDocument;

                                    }
                                }
                            }
                        }
                    }

                    gloICD10Cache.Add(xmlDataProvider, "ICD10Notes");
                }
                catch // (Exception Ex)
                {
                    gloICD10Cache.Add(xmlDataProvider, "ICD10Notes");
                }
            }

            return xmlDataProvider;
        }

        private static class gloICD10Cache
        {

            private static Hashtable oCatch = new Hashtable();

            public static void Add(XmlDataProvider oValue, String sSPnameAsKey)
            {
                try
                {
                    if (!oCatch.ContainsKey(sSPnameAsKey))
                    {
                        oCatch.Add(sSPnameAsKey, oValue);
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
                    return oCatch.ContainsKey(sSPnameAsKey);
                }
                catch //(Exception ex)
                {

                }
                return false;
            }

            public static XmlDataProvider Get(String sSPnameAsKey)
            {
                XmlDataProvider oResult = null;
                try
                {
                    if (oCatch.ContainsKey(sSPnameAsKey))
                    {
                        oResult = (XmlDataProvider)oCatch[sSPnameAsKey];
                    }
                }
                catch //(Exception ex)
                {

                }
                return oResult;
            }

            public static void Remove(String sSPnameAsKey)
            {
                try
                {
                    oCatch.Remove(sSPnameAsKey);
                }
                catch //(Exception ex)
                {
                }
            }

            public static void Clear()
            {
                try
                {
                    oCatch.Clear();
                }
                catch //(Exception ex)
                {

                }
            }
        }
    }
}
