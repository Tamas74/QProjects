using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using gloGlobal;


namespace gloEmdeonInterface.Classes
{
    // By Abhijeet Farkande  on date : 20100225
    // Make the class & it methods,members as non static

    //static class clsXMLLayer
    /// <summary>
    /// Class created by Roopali on 20100217
    /// Purpose := To process XML requests sent to Emdeon web services.
    /// Retrieves the response from Emdeon store results in XML files.
    /// Returns := File name for XML file response created in "\Emdeon\Inbox" location.
    /// </summary>
    public class clsXMLLayer
    {

        public string strOutboxFilePath = gloSettings.FolderSettings.AppTempFolderPath + "Emdeon\\Outbox";
        public string strInboxFilePath = gloSettings.FolderSettings.AppTempFolderPath + "Emdeon\\Inbox";

        private void SetPath()
        {
            try
            {
                if (System.IO.Directory.Exists(strOutboxFilePath) == false)
                {
                    System.IO.Directory.CreateDirectory(strOutboxFilePath);
                }

                if (System.IO.Directory.Exists(strInboxFilePath) == false)
                {
                    System.IO.Directory.CreateDirectory(strInboxFilePath);
                }
            }
            catch (IOException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 
            }
        }

        private string ProcessRequest(XmlDocument odoc)
        {

            SetPath();
            StringBuilder restURL = new StringBuilder();

            WebRequest restRequest = default(WebRequest);
            WebResponse restResponse = default(WebResponse);
            // XmlDocument xDoc = new XmlDocument(); 
            try
            {
                restURL.AppendFormat(clsEmdeonGeneral.emdeonURL + "/servlet/XMLServlet?request=");
                restURL.Append(odoc.InnerXml.ToString());
                //created Web request 
                restRequest = (HttpWebRequest)HttpWebRequest.Create(restURL.ToString());

                restRequest.Method = "POST";
                restRequest.ContentType = "application/x-www-form-urlencoded";
                // modified for timeout for early connectivity with emdeon... By madan.20100223
                //restRequest.Timeout = 6000000; // set to maximum
                restRequest.Timeout = 60000; // set to maximum
                //End of changes Madan

                restResponse = restRequest.GetResponse(); // requesting response to stream
                StreamReader sr = new StreamReader(restResponse.GetResponseStream());

                string Result = sr.ReadToEnd().Trim();
                // write to local file path after creating file dynamically
                string strfilename = gloGlobal.clsFileExtensions.NewDocumentName(strInboxFilePath, ".xml", "yyyyMMddhhmmssmmm");// strInboxFilePath + "\\" + (System.DateTime.Now.ToString("yyyyMMddhhmmssmmm").Trim() + System.Guid.NewGuid().ToString() + ".xml");
               
                // Reading and writing to filestream and to file created above
                using (FileStream fs = new FileStream(strfilename, FileMode.Create, FileAccess.ReadWrite,FileShare.ReadWrite))
                {// by Abhijeet ,made changes for read & write
                    using (StreamWriter m_streamWriter = new StreamWriter(fs))
                    {
                        m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);

                        //m_streamWriter.Write(Result);
                        m_streamWriter.WriteLine(Result);
                        m_streamWriter.Close();
                        //m_streamWriter.Dispose(); // by Abhijeet 
                    }
                    fs.Close();  // by Abhijeet
                    //fs.Dispose(); //// by Abhijeet
                    //fs = null; // by Abhijeet
                }

               // FileAttributes objfileattr= File.GetAttributes(strfilename);
               // System.Windows.Forms.MessageBox.Show(objfileattr.ToString());


                sr.Close();
                return strfilename; // return file path

            }
            // Abhijeet Farkande on Date 20100225
            // Add the System.Net.WebException to catch time out Exception 
            catch (System.Net.WebException ex)
            {
                //clsGeneral objclsGen = new clsGeneral();
                //objclsGen.UpdateLog("Time out Error in processing request : " + ex.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);  
                return "";
            }
            catch (Exception ex)
            {
                //return the output from the Surescript as response...should be XML details. 
                //If error, show in on-screen text box. this.txtReturned.Text = ex.Message; 
                //TD1.InnerHtml = " Exception: " + ex.ToString() 

                // Abhijeet Farkande on Date 20100225
                // update the exception in log file & return blank instead of null
                //return null; // Exception - logg need tobe handled
             
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);  
                return "";
            }
            finally
            {
                if (restResponse != null)
                {
                    restResponse.Close();
                }
                
            }
        }

        public string GetResponse(string requestFile)
        {
            // By Abhijeet Farkande on date 20100308
            // added try catch block
            try
            {
                string strFileName = null;

                //string _requestFile = GenerateXMLFIlE(strRequestType, xmlContent);
                //I've request file redy with me Outbox - Now sending 
                if (!string.IsNullOrEmpty(requestFile))
                {
                    if (File.Exists(requestFile))
                    {
                        XmlDocument odoc = new XmlDocument();

                        odoc.Load(requestFile);
                        //'Send Request file and recieveing response and convert to XML file to Inbox                   
                        strFileName = ProcessRequest(odoc);


                 strFileName = handleEscapeCharactersInXml(strFileName);
                        return strFileName;

                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
              
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);  
                return "";
            }
        }


        public string handleEscapeCharactersInXml(string filePath)
        {
            // Function is added by Abhijeet on Date 20100324 . 
            // function used to handle the Special characters in XMl file while reading it or making dataset of it.
            // it handle the "(double qoute) '(single quote) <(Less than symbol) >(greater than symbol) &(Ampersand sign)

            string strfilename = filePath;          
            string _finalString = string.Empty;

            StreamReader objstrmReader = new StreamReader(strfilename);


            //filePath = strInboxFilePath + "\\" + (System.DateTime.Now.ToString("yyyyMMddhhmmssmmm").Trim() + System.Guid.NewGuid().ToString() + ".xml");

            //while(File.Exists(filePath))
            //{
            //    filePath = strInboxFilePath + "\\" + (System.DateTime.Now.ToString("yyyyMMddhhmmssmmm").Trim() + System.Guid.NewGuid().ToString() + ".xml");
            //} 

            filePath = gloGlobal.clsFileExtensions.NewDocumentName(strInboxFilePath, ".xml", "yyyyMMddhhmmssmmm");
            FileStream objstrmWriter = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite,FileShare.ReadWrite);
            StreamWriter objWriter = new StreamWriter(objstrmWriter);

           // clsGeneral objclsGen1 = new clsGeneral();


            //if (strfilename != string.Empty && strfilename.ToString() != "")
            //{               
            //    objclsGen1.UpdateLog("file is Available  ");
            //}

            //string copyfilePath = strInboxFilePath + "\\" + (System.DateTime.Now.ToString("yyyyMMddhhmmssmmm").Trim() + ".xml");
            //File.Copy(strfilename, copyfilePath, true);
            // StreamReader objstrmReader = new StreamReader(copyfilePath);                                 

            try
            {

                _finalString = objstrmReader.ReadToEnd();

                _finalString = _finalString.Replace("&apos", "&apos;").Replace("&amp", "&amp;").Replace("&lt", "&lt;").Replace("&gt", "&gt;").Replace("&quot", "&quot;");

                objstrmReader.Close();
                objWriter.WriteLine(_finalString);
                objWriter.Close();
                objstrmWriter.Close();
                objstrmReader.Close();

                _finalString = string.Empty;
                if (File.Exists(strfilename))
                {
                    File.Delete(strfilename);
                }

            }
            catch (Exception ex)
            {

               
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 
                _finalString = string.Empty;

            }
            finally
            {

                if (objstrmWriter != null)
                {
                    objstrmWriter.Dispose();
                }
                if (objWriter != null)
                {
                    objWriter.Close();
                    objWriter.Dispose();
                }
                if (objstrmReader != null)
                {
                    objstrmReader.Close();
                    objstrmReader.Dispose();
                }

            }

            return filePath;
            // Code to check immediately file can read in dataset
            //DataSet ds = new DataSet();
            //ds.ReadXml("c:\\temp1.xml");


        }


        #region Not in Use

        //public string ExtractXML(object cntFromDB, string strFilePath)
        //{
        //    MemoryStream stream = default(MemoryStream);
        //    try
        //    {
        //        if ((cntFromDB != null))
        //        {
        //            string strfilename = strFilePath + "\\" + (System.DateTime.Now.ToString("yyyyMMddhhmmssmmm").Trim() + ".xml");
        //            byte[] content = (byte[])cntFromDB;
        //            stream = new MemoryStream(content);
        //            System.IO.FileStream oFile = new System.IO.FileStream(strfilename, System.IO.FileMode.Create);
        //            stream.WriteTo(oFile);
        //            oFile.Close();
        //            XmlDocument odoc = new XmlDocument();
        //            odoc.Load(strfilename);
        //            return strfilename;
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //    catch (XmlException ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 
        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false); 
        //        return "";
        //    }
        //    finally
        //    {
        //        if ((stream != null))
        //        {
        //            stream.Dispose();
        //            stream = null;
        //        }

        //    }
        //    return "";
        //}
        #endregion
    }
}
