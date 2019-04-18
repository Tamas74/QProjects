using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using gloCCRSchema;
using System.Xml;
using gloEMeasure;

namespace gloCCDSchema
{
    public static class gloSerialization
    {


        public static POCD_MT000040UV02ClinicalDocument GetClinicalDocument(String sFilePath)
        {

            POCD_MT000040UV02ClinicalDocument oCCD = null;           
            try
            {
                if (File.Exists(sFilePath))
                {
                    oCCD = Deserialize<POCD_MT000040UV02ClinicalDocument>(@"" + sFilePath + "");
                }                
     
            }
            catch (Exception ex)
            {
                throw new Exception("The file either invalid or not a recognized cda format. ", ex);
            }
            return oCCD;
         
        }
        public static POQM_MT000001UV03QualityMeasureDocument  GetCQMData(String sFilePath)
        {

            POQM_MT000001UV03QualityMeasureDocument oCQM = null;
            try
            {
                if (File.Exists(sFilePath))
                {
                    oCQM = Deserialize<POQM_MT000001UV03QualityMeasureDocument>(@"" + sFilePath + "");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("The file either invalid or not a recognized cda format. ", ex);
            }
            return oCQM;

        }

        public static gloCCRSchema.ContinuityOfCareRecord GetContinuityOfCareRecord(String sFilePath)
        {

            gloCCRSchema.ContinuityOfCareRecord oCCR = null;
            try
            {
                if (File.Exists(sFilePath))
                {
                    oCCR = Deserialize<ContinuityOfCareRecord>(@"" + sFilePath + "");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("The file either invalid or not a recognized ccr format. ", ex);
            }
            return oCCR;

        }

        //public static Boolean SetClinicalDocument(String sFilePath, POCD_MT000040UV02ClinicalDocument objData)
        //{
        //    Boolean _result = false;
        //    try
        //    {
        //        if (File.Exists(sFilePath) == false)
        //        {
        //            //XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        //            //namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        //            //namespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");
        //            //namespaces.Add("voc", "urn:hl7-org:v3/voc");
        //            //namespaces.Add("sdtc", "urn:hl7-org:sdtc");

        //            XmlSerializer s = new XmlSerializer(typeof(POCD_MT000040UV02ClinicalDocument));
        //            using (XmlWriter w = XmlWriter.Create(sFilePath))
        //            {
        //                w.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"http://www.glostream.com/css/XSLT/gloCCDAcss_MU2.xsl\"");
        //                s.Serialize(w, objData );
        //            }
        //            //Serialize<POCD_MT000040UV02ClinicalDocument>(objData, sFilePath);
        //            _result = true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //        throw new Exception("The file is not created. ", ex);
        //    }
        //    return _result;
        //}

        public static Boolean SetClinicalDocument(String sFilePath, POCD_MT000040UV02ClinicalDocument objData, Boolean isIntuit = false)
        {
            Boolean _result = false;
            XmlWriterSettings settings = new XmlWriterSettings();
            Encoding utf8EncodingWithOutBOM = new UTF8Encoding(false);
            try
            {
                if (File.Exists(sFilePath) == false)
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    namespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");
                    namespaces.Add("voc", "urn:hl7-org:v3/voc");
                    namespaces.Add("sdtc", "urn:hl7-org:sdtc");

                    settings.Indent = true;
                    settings.Encoding = utf8EncodingWithOutBOM;

                    XmlSerializer s = new XmlSerializer(typeof(POCD_MT000040UV02ClinicalDocument));
                    using (XmlWriter w = XmlWriter.Create(sFilePath, settings))
                    {
                        s.Serialize(w, objData, namespaces);
                    }
                    s = null;
                    _result = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("1 The file is not created. " + ex.ToString(), ex);
            }
            finally
            {
                settings = null;
                utf8EncodingWithOutBOM = null;
            }
            return _result;
        }

        public static Boolean SetClinicalDocument(String sFilePath, POCD_MT000040UV02ClinicalDocument objData, String sStyleSheet,Boolean isIntuit =false )
        {
            Boolean _result = false;
            XmlWriterSettings settings = new XmlWriterSettings();
            Encoding utf8EncodingWithOutBOM = new UTF8Encoding(false);
            try
            {
                if (File.Exists(sFilePath) == false)
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    namespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");
                    namespaces.Add("voc", "urn:hl7-org:v3/voc");
                    namespaces.Add("sdtc", "urn:hl7-org:sdtc");

                    //settings.Encoding = Encoding.ASCII;
                    // settings.OmitXmlDeclaration = true;
                    settings.Indent = true;
                    settings.Encoding = utf8EncodingWithOutBOM;
                 
                    XmlSerializer s = new XmlSerializer(typeof(POCD_MT000040UV02ClinicalDocument));
                    //if (isIntuit == true)
                    //{
                    //    using (XmlWriter w = XmlWriter.Create(sFilePath ))
                    //    {
                    //        w.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + sStyleSheet + "\"");
                    //        s.Serialize(w, objData);
                    //    }
                    //}
                    //else
                    //{
                    using (XmlWriter w = XmlWriter.Create(sFilePath, settings ))
                    {
                        w.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + sStyleSheet + "\"");
                        s.Serialize(w, objData ,namespaces );
                    }
                    //}
                    s = null;
                    //Serialize<POCD_MT000040UV02ClinicalDocument>(objData, sFilePath);
                    _result = true;
                }

            }
            catch (Exception ex)
            {
                //return false;
                throw new Exception("1 The file is not created. " + ex.ToString(), ex);
            }
            finally
            {
                settings = null;
                utf8EncodingWithOutBOM = null;
            }
            return _result;
        }
        public static Boolean SetCCDExchange(String sFilePath, gloCCDExchange.CcdExchange    objData, String sStyleSheet)
        {
            Boolean _result = false;
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            try
            {
                if (File.Exists(sFilePath) == false)
                {

                    namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    namespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");
                    // namespaces.Add("xsi:schemaLocation", "http://schema.intuit.com/health/ccd/v1");

                    namespaces.Add("eh", "http://schema.intuit.com/health/ccd/v1");
                    XmlWriterSettings settings = new XmlWriterSettings();
                    //settings.Encoding = Encoding.GetEncoding (1252);
                    settings.Encoding = Encoding.ASCII;

                    // settings.Encoding = null;
                    XmlSerializer s = new XmlSerializer(typeof(gloCCDExchange.CcdExchange));
                    using (XmlWriter w = XmlWriter.Create(sFilePath, settings))
                    {
                        //  w.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + sStyleSheet + "\"");
                        s.Serialize(w, objData, namespaces);
                    }
                    //Serialize<POCD_MT000040UV02ClinicalDocument>(objData, sFilePath);
                    s = null;
                    _result = true;
                }

            }
            catch (Exception ex)
            {
                //return false;
                throw new Exception("2 The file is not created " + ex.ToString(), ex);
            }
            finally
            {
                namespaces = null;
            }
            return _result;
        }

        public static Boolean SetQRDADocument(String sFilePath, POCD_MT000040UV02ClinicalDocument objData)
        {
            Boolean _result = false;
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            XmlWriterSettings settings = new XmlWriterSettings();
            try
            {
                //08-Jan-13 Aniket: Resolve issue where file was not getting overwritten
                if (File.Exists(sFilePath) == true)
                {
                    File.Delete(sFilePath);
                }
                settings.Indent = true;
                namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                namespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");
                namespaces.Add("voc", "urn:hl7-org:v3/voc");
                namespaces.Add("sdtc", "urn:hl7-org:sdtc");

                XmlSerializer s = new XmlSerializer(typeof(POCD_MT000040UV02ClinicalDocument));
                using (XmlWriter w = XmlWriter.Create(sFilePath,settings ))
                {
                    //  w.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"http://www.glostream.com/css/XSLT/gloccdCss.xsl\"");
                    s.Serialize(w, objData, namespaces);
                }
                //Serialize<POCD_MT000040UV02ClinicalDocument>(objData, sFilePath);
                s = null;
                _result = true;


            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("3 The file is not created " + ex.ToString(), ex);
            }
            finally
            {
                settings = null;
                namespaces = null;
            }
            return _result;
        }
        private static void Serialize<T>(T value, string pathName)
        {
            XmlSerializer serializer = null;

            try
            {
              
                using (TextWriter writer = new StreamWriter(pathName))
                {
                    serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, value );
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

        private static T Deserialize<T>(string pathName)
        {
            XmlSerializer serializer = null;

            try
            {
                using (TextReader reader = new StreamReader(pathName))
                {
                    serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(reader);
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

    }

}
