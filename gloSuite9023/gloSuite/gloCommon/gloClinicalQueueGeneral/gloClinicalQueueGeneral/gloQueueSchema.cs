using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using gloClinicalQueueGeneral;
using System.Xml;

namespace gloQueueSchema
{
    public static class gloSerialization
    {


        public static Queue GetClinicalDocument(String sFilePath)
        {

            Queue oQueueDoc = null;
            try
            {
                if (File.Exists(sFilePath))
                {
                    oQueueDoc = Deserialize<Queue>(@"" + sFilePath + "");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("The file either invalid or not a recognized  format. ", ex);
            }
            return oQueueDoc;

        }






        public static Boolean SetClinicalDocument(String sFilePath, gloClinicalQueueGeneral.Queue objData,Boolean bUseFileZip = false)
        {
            Boolean _result = false;
            try
            {
                if (File.Exists(sFilePath) == false)
                {
                   
                    XmlWriterSettings settings = new XmlWriterSettings();
                   
                    Encoding utf8EncodingWithOutBOM = new UTF8Encoding(false);
                    settings.Encoding = utf8EncodingWithOutBOM;
                    settings.Indent = true;
                    XmlSerializer s = new XmlSerializer(typeof(gloClinicalQueueGeneral.Queue));

                        //using (XmlWriter w = XmlWriter.Create(sFilePath,settings))
                        //{
                            
                        //   // w.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + sStyleSheet + "\"");
                        //    s.Serialize(w, objData);
                        //}

                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(ms, settings))//sFilePath
                            {
                                s.Serialize(w, objData);
                            }
                            Byte[] barrMS = ms.ToArray();
                            if (bUseFileZip)
                            {
                                gloGlobal.gloTSPrint.ZipBytes(barrMS, sFilePath);
                            }
                            else
                            {
                                File.WriteAllBytes(sFilePath, barrMS);
                            }
                            barrMS = null;
                        }
                    _result = true;
                }

            }
            catch (Exception ex)
            {
               // return false;
                throw new Exception("1 The file is not created. " + ex.ToString(), ex);
            }
            return _result;
        }

        public static InstalledPrinters GetInstalledPrintersDocument(String sFilePath)
        {

            InstalledPrinters oInstalledPrinters = null;
            try
            {
                if (File.Exists(sFilePath))
                {
                    oInstalledPrinters = Deserialize<InstalledPrinters>(@"" + sFilePath + "");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("The file either invalid or not a recognized  format. ", ex);
            }
            return oInstalledPrinters;

        }






        public static Boolean SetInstalledPrintersDocument(String sFilePath, InstalledPrinters objData)
        {
            Boolean _result = false;
            try
            {
                if (File.Exists(sFilePath) == false)
                {

                    XmlWriterSettings settings = new XmlWriterSettings();

                    Encoding utf8EncodingWithOutBOM = new UTF8Encoding(false);
                    settings.Encoding = utf8EncodingWithOutBOM;
                    settings.Indent = true;
                    XmlSerializer s = new XmlSerializer(typeof(InstalledPrinters));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(ms, settings))//sFilePath
                        {
                            s.Serialize(w, objData);
                        }
                        Byte[] barrMS = ms.ToArray();
                        File.WriteAllBytes(sFilePath, barrMS);
                        barrMS = null;
                    }
                    //using (XmlWriter w = XmlWriter.Create(sFilePath, settings))
                    //{

                    //    // w.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + sStyleSheet + "\"");
                    //    s.Serialize(w, objData);
                    //}

                    _result = true;
                }

            }
            catch (Exception ex)
            {
                // return false;
                throw new Exception("1 The file is not created. " + ex.ToString(), ex);
            }
            return _result;
        }

        public static MasterConfigFileMasterConfig GetMasterConfigDocument(String sFilePath)
        {

            MasterConfigFileMasterConfig oMasterConfig = null;
            try
            {
                if (File.Exists(sFilePath))
                {
                    oMasterConfig = Deserialize<MasterConfigFileMasterConfig>(@"" + sFilePath + "");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("The Master file either invalid or not a recognized  format. ", ex);
            }
            return oMasterConfig;

        }






        public static Boolean SetMasterConfigDocument(String sFilePath, MasterConfigFileMasterConfig objData)
        {
            Boolean _result = false;
            try
            {
                if (File.Exists(sFilePath) == false)
                {

                    XmlWriterSettings settings = new XmlWriterSettings();

                    Encoding utf8EncodingWithOutBOM = new UTF8Encoding(false);
                    settings.Encoding = utf8EncodingWithOutBOM;
                    settings.Indent = true;
                    XmlSerializer s = new XmlSerializer(typeof(MasterConfigFileMasterConfig));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(ms, settings))//sFilePath
                        {
                            s.Serialize(w, objData);
                        }
                        Byte[] barrMS = ms.ToArray();
                        File.WriteAllBytes(sFilePath, barrMS);
                        barrMS = null;
                    }
                    //using (XmlWriter w = XmlWriter.Create(sFilePath, settings))
                    //{

                    //    // w.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + sStyleSheet + "\"");
                    //    s.Serialize(w, objData);
                    //}

                    _result = true;
                }

            }
            catch (Exception ex)
            {
                // return false;
                throw new Exception("1 The file is not created. " + ex.ToString(), ex);
            }
            return _result;
        }

        private static void Serialize<T>(T value, string pathName)
        {
            XmlSerializer serializer = null;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (TextWriter writer = new StreamWriter(ms))//sFilePath
                    {
                        serializer = new XmlSerializer(typeof(T));
                        {
                            serializer.Serialize(writer, value);
                        }
                    }
                    Byte[] barrMS = ms.ToArray();
                    File.WriteAllBytes(pathName, barrMS);
                    barrMS = null;
                }

                //using (TextWriter writer = new StreamWriter(pathName))
                //{
                //    serializer = new XmlSerializer(typeof(T));
                //    serializer.Serialize(writer, value );
                //}

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
            byte[] fileArray = null;
            try
            {
                String sFileExtension = Path.GetExtension(pathName);
                if (sFileExtension == ".xmz")
                {
                    fileArray = gloGlobal.gloTSPrint.UnZipBytes(pathName);
                }
                else
                { 
                    fileArray = File.ReadAllBytes(pathName);
                }
                
                using (MemoryStream ms = new MemoryStream(fileArray))
                {
                    using (TextReader reader = new StreamReader(ms))
                    {
                        serializer = new XmlSerializer(typeof(T));
                        return (T)serializer.Deserialize(reader);
                    }
                }
                //using (TextReader reader = new StreamReader(pathName))
                //{
                //    serializer = new XmlSerializer(typeof(T));
                //    return (T)serializer.Deserialize(reader);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                serializer = null;
                fileArray = null;
            }
        }

    }

}
