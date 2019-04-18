using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using gloPatientSaving;

namespace gloRxPatientSaving
{
    public static class gloSerialization
    {
        public static PatientSavingsNotificationType GetClinicalDocument(MemoryStream oFileStream)
        {

            PatientSavingsNotificationType oCCD = null;           
            try
            {
                if(oFileStream !=null)
                    oCCD = Deserialize<PatientSavingsNotificationType>(oFileStream);
               
     
            }
            catch //(Exception ex)
            {
                return null;
            }
            return oCCD;
         
        }

        public static Boolean SetClinicalDocument(String sFilePath, OpportunityResponseType objData)
        {
            Boolean _result = false;
            try
            {
                if (File.Exists(sFilePath) == false)
                {
                    XmlSerializer s = new XmlSerializer(typeof(OpportunityResponseType));
                    Serialize<OpportunityResponseType>(objData, sFilePath);
                    _result = true;
                }

            }
            catch //(Exception ex)
            {
                return false;
                
            }
            return _result;
        }

       private static void Serialize<T>(T value, string pathName)
        {
            XmlSerializer serializer = null;

            try
            {
              XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
              ns.Add("structures", "http://www.ncpdp.org/schema/structures");
              ns.Add("datatypes", "http://www.ncpdp.org/schema/datatypes");
              ns.Add("ecl", "http://www.ncpdp.org/schema/ecl");
                using (TextWriter writer = new StreamWriter(pathName))
                {
                    serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, value, ns);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                serializer = null;
            }
        }

       private static T Deserialize<T>(MemoryStream oFileStream)
        {
            XmlSerializer serializer = null;

            try
            {
                serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(oFileStream);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                serializer = null;
            }
        }

    }

}
