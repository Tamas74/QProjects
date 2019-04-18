using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Xml;
using System.IO;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using gloGlobal;

namespace gloEmdeonInterface.Classes
{
    public static class clsEmdeonGeneral
    {
        #region Global_Variables_EmdeonInterface

        public static string emdeonUserName = string.Empty;
        public static string emdeonUserPassword = string.Empty;
        public static string emdeonURL = string.Empty;
        public static string emdeonFacilityCode = string.Empty;
        //Removed... on 22/02/2010--Madan
        //public static string emdeonDescription = string.Empty;
        public static string gloLab_hsilabel = string.Empty;
        public static Boolean blnEmdeonPreselectDiagnosis = false;
        public static Int16 iEmdeonGetLabOrdersFromDaysOnReload = 90;
        
        //Removed... on 20/02/2010--Madan
        //public static bool gloLab_pat = false;
        public static string gloLab_patcode = string.Empty;
        public static Int64 gloLab_patID = 0;
        public static string sgloLabtype = string.Empty;
        public static string sgloLab_Value = string.Empty;
        //Connection string
        public static string sConnectionString = string.Empty;
        //Payment type..checking the type is clinet(C),Patient(P),Third Party(T)
        public static string gloLab_BillingStatus = string.Empty;
        public static string gloLab_BillingType = string.Empty;
        public static Int64 gloLab_BillGuarantorAcntID = 0;
        //Reference value from DB for Particular Patinet..
        public static string gloLab_personRefval = string.Empty;
        public static string gloLab_providerRefval = string.Empty;
        public static string gloLab_guarantorRefval = string.Empty;
        public static string gloLab_insuranceRefval = string.Empty;
        public static string gloLab_ispRefval = string.Empty;
        public static string gloLab_personhsiRefval = string.Empty;
        //Patient Identifier ...
        public static string gloLab_Identifier = string.Empty;
        // variable added by Abhijeet on 20100411 for saving staus of URl validation
        public static Boolean gloLab_boolValidateURL = false;
        // End of changes by Abhijeet for variable to save staus of URl validation

        public static Boolean gblnICD9Driven = true;
        //Added this for maintaining session.
        public static string gloLab_SessionId = "";
        public static Int64 gloLab_insuranceFlag = 0;
        public static int gloLab_insuranceIndex = 0;

        public static int gloLab_SecondaryinsuranceIndex = 0;
        public static int gloLab_TertiaryinsuranceIndex = 0;

        public static bool IsLockScreenActivated = false; //added by madan on 20100513 for identifying lock screen.
        //Added by madan on 20100614
        public static string gloLab_Provider_Usage = string.Empty;
        public static bool gloLab_IsOrderLocked = false;
        //End 20100614

        //added by madan on 20100701
        public static bool IsDemoLab = false;
        
        #endregion


        #region global_Methods_EmdeonInterface

        //Audit trail exception log.. added by Madan-20100402
        public static bool ConfirmNull(string strValue)
        {
           // clsGeneral objclsGeneral = new clsGeneral();
            bool blnCheck = false;
            try
            {
                if (strValue != null && strValue.ToString().Trim().Length != 0 && strValue.ToString() != "")
                {
                    blnCheck = true;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            return blnCheck;
        }

        public static bool ValidateGloLabUrl()
        {   // Code By Abhijeet on 20100411 
            // written new function for validating emdeon url ans set global variable which keep 
            // track of url validation
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(sConnectionString);
            bool _retResult = false;

            DataTable dt = null;
            try
            {
                oDB.Connect(false);
                string strQry = "select sSettingsName, sSettingsValue from settings where sSettingsName in ('GLOLAB USER NAME','GLOLAB PASSWORD','GLOLAB URL','GLOLAB FACILITY CODE','GLOLAB HSI LABEL','PRESELECT DIAGNOSIS WHILE PLACING EMDEON ORDERS','EmdeonGetLabOrdersFromDaysOnReload') ";
                oDB.Retrive_Query(strQry, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int rowCnt = 0; rowCnt < dt.Rows.Count; rowCnt++)
                    {
                        switch (Convert.ToString(dt.Rows[rowCnt]["sSettingsName"]).Trim())
                        {
                            case "GLOLAB USER NAME":
                                emdeonUserName = Convert.ToString(dt.Rows[rowCnt]["sSettingsValue"]);
                                break;
                            case "GLOLAB PASSWORD":
                                gloSecurity.ClsEncryption oEncryption = new gloSecurity.ClsEncryption();
                                emdeonUserPassword = oEncryption.DecryptFromBase64String(Convert.ToString(dt.Rows[rowCnt]["sSettingsValue"]), "20gloStreamInc08");
                                oEncryption.Dispose();
                                //emdeonUserPassword = Convert.ToString(dt.Rows[rowCnt]["sSettingsValue"]);
                                break;
                            case "GLOLAB URL":
                                emdeonURL = Convert.ToString(dt.Rows[rowCnt]["sSettingsValue"]);
                                break;
                            case "GLOLAB FACILITY CODE":
                                emdeonFacilityCode = Convert.ToString(dt.Rows[rowCnt]["sSettingsValue"]);
                                break;
                            case "GLOLAB HSI LABEL":
                                gloLab_hsilabel = Convert.ToString(dt.Rows[rowCnt]["sSettingsValue"]);
                                break;
                            case "PRESELECT DIAGNOSIS WHILE PLACING EMDEON ORDERS":
                                blnEmdeonPreselectDiagnosis = Convert.ToBoolean(dt.Rows[rowCnt]["sSettingsValue"]);
                                break;
                            case "EMDEONGETLABORDERSFROMDAYSONRELOAD":
                                iEmdeonGetLabOrdersFromDaysOnReload = Convert.ToInt16(dt.Rows[rowCnt]["sSettingsValue"]);
                                break;
                        }
                    }
                }

                if (emdeonUserName.ToString().Trim() != "" && emdeonUserPassword.ToString().Trim() != "" && emdeonURL.ToString().Trim() != "" && emdeonFacilityCode.ToString().Trim() != "" && gloLab_hsilabel.ToString().Trim() != "")
                {
                    gloLab_boolValidateURL = true;
                    _retResult = true;
                }
                else
                {
                    gloLab_boolValidateURL = false;
                    _retResult = false;
                }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                //clsGeneral objclsGen = new clsGeneral();           
                _retResult = false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

                if (dt != null)
                {
                    dt.Dispose();
                }

            }
            return _retResult;

        }

        public static bool CheckConnectionParameters(string DBConnectionString)
        {
            bool _retResult = false;
            try
            {
                if (DBConnectionString.Length > 0)
                {
                    sConnectionString = DBConnectionString;

                    _retResult = ValidateGloLabUrl();
                }
                else
                {
                    // TO DO : Event track instead exceptions.
            //        clsGeneral objclsGen = new clsGeneral();

                    _retResult = false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

                _retResult = false;
            }

            return _retResult;
        }

        #endregion

    }
    public class clsEmdeonLabLayer
    {
        //// #region "Constructor & Distructor"
                 
        ////private bool disposed = false;
        ////public void Dispose()
        ////{
        ////    Dispose(true);
        ////    GC.SuppressFinalize(this);
        ////}

        ////protected virtual void Dispose(bool disposing)
        ////{
        ////    if (!this.disposed)
        ////    {
        ////        if (disposing)
        ////        {

        ////        }
        ////    }
        ////    disposed = true;
        ////}

        ////~clsEmdeonLabLayer()
        ////{
        ////    Dispose(false);

        ////    if (this.objclsGeneral != null)
        ////    {
        ////        this.objclsGeneral.Dispose();
        ////    }

        ////}
        ////#endregion "Constructor & Distructor"
       
        private string _strInboxFilePath;

        public string strInboxFilePath
        {
            get { return _strInboxFilePath; }
            set { _strInboxFilePath = value; }
        }


        private String _strOutboxFilePath;

        public String strOutboxFilePath
        {
            get { return _strOutboxFilePath; }
            set { _strOutboxFilePath = value; }
        }

        private long _nPatientID;

        public long PatientId
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }

        //Added by madan on 20100612
        private string _orderRefreanceID = string.Empty;

        public string OrderRefreanceID
        {
            get { return _orderRefreanceID; }
            set { _orderRefreanceID = value; }
        }
        
        // By Abhijeet on Date 20100318
        // variables for  Person & PersonHsi for patient.
        private string _strPesron = "";
        private string _strPersonhsi = "";
        
        //By madan on date:20100331
        //Variables are added for upateding orderstatus in emdeon.
        private string _strOrderRef="";
        private string _strOrderStatus="";
     
     
        public clsEmdeonLabLayer()
        {

            strOutboxFilePath = gloSettings.FolderSettings.AppTempFolderPath + "Emdeon\\Outbox";
            strInboxFilePath = gloSettings.FolderSettings.AppTempFolderPath + "Emdeon\\Inbox";
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
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);    
 
            }
        }
        public clsEmdeonLabLayer(long nPatientId)
        {
            strOutboxFilePath = gloSettings.FolderSettings.AppTempFolderPath + "Emdeon\\Outbox";
            strInboxFilePath = gloSettings.FolderSettings.AppTempFolderPath + "Emdeon\\Inbox";
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
                PatientId = nPatientId;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);     
    
            }
        }


        ////getting an memory exception if large files recieved - implemented new function at clsXML layer
        //public byte[] PostDirServiceXML(XmlDocument odoc)
        //{
        //    StringBuilder restURL = new StringBuilder();

        //    WebRequest restRequest = default(WebRequest);
        //    WebResponse restResponse = default(WebResponse);
        //    // XmlDocument xDoc = new XmlDocument(); 
        //    try
        //    {

        //        restURL.AppendFormat(clsEmdeonGeneral.emdeonURL + "/servlet/XMLServlet?request=");
        //        restURL.Append(odoc.InnerXml.ToString());
        //        restRequest = (HttpWebRequest)HttpWebRequest.Create(restURL.ToString());

        //        // the key line. This adds the base64-encoded authentication information to the request header 
        //        //restRequest.Headers.Add("Authorization: Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("glo$treamU$er:m@3r4501g"))) 
        //        //restRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic" & Convert.ToBase64String(Encoding.UTF8.GetBytes("glo$treamU$er:m@3r4501g"))) 

        //        restRequest.Method = "POST";
        //        restRequest.ContentType = "text/xml";
        //        restRequest.Timeout = 6000000;

        //        restResponse = restRequest.GetResponse();



        //        //byte[] someBytes = System.Text.Encoding.UTF8.GetBytes(odoc.InnerXml);
        //        //restRequest.ContentLength = someBytes.Length;
        //        //System.IO.Stream newStream = restResponse.GetRequestStream();
        //        // newStream.Write(someBytes, 0, someBytes.Length);
        //        //newStream.Close();


        //        System.IO.BinaryReader oReader = new System.IO.BinaryReader(restResponse.GetResponseStream());
        //        byte[] bytesRead = oReader.ReadBytes(Convert.ToInt32(restResponse.ContentLength));
        //        return bytesRead;
        //    }
        //    catch (Exception ex)
        //    {
        //        objclsGeneral.UpdateLog(ex.ToString());
        //        //return the output from the Surescript as response...should be XML details. 

        //        //If error, show in on-screen text box. this.txtReturned.Text = ex.Message; 
        //        //TD1.InnerHtml = " Exception: " + ex.ToString() 
        //        return null;
        //    }
        //    finally
        //    {
        //        if (restResponse != null)
        //        {
        //            restResponse.Close();
        //        }
        //    }
        //}



        public string ExtractXML(object cntFromDB, string strFilePath)
        {
         //   MemoryStream stream = default(MemoryStream);
            try
            {
                if ((cntFromDB != null))
                {
                    string strfilename = strFilePath + "\\" + (gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") + ".xml");
                    byte[] content = (byte[])cntFromDB;
                //    stream = new MemoryStream(content);
                    System.IO.FileStream oFile = new System.IO.FileStream(strfilename, System.IO.FileMode.Create);
                    oFile.Write(content, 0, content.Length);
                  //  stream.WriteTo(oFile);
                    oFile.Close();
                    oFile.Dispose();
                    oFile = null;
                    XmlDocument odoc = new XmlDocument();
                    odoc.Load(strfilename);
                    odoc = null;
                    return strfilename;
                }
                else
                {
                    return "";
                }
            }
            catch (XmlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);    

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);    


            }
            finally
            {
                //if ((stream != null))
                //{
                //    stream.Dispose();
                //    stream = null;
                //}

            }
            return "";
        }

        // By Abhijeet on Date 20100318
        // written function which set the  Person & PersonHsi for patient.

       
        private void SetPersonPersonhsi()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsEmdeonGeneral.sConnectionString);
            try
            {                
                DataTable dt = null;                
                //string strQry = "select a.npatientid,b.sgloLabType,b.sgloLabValue from Patient a inner join  patient_gloLab b on a.nPatientId=b.nPatientId where a.nPatientId=" + PatientId.ToString();
                string strQry = "select a.nPatientId,b.sExternalSubType,b.sExternalValue from Patient a inner join PatientExternalCodes b on a.nPatientId=b.nPatientId  where b.sExternalType = 'EMDEON' AND   a.nPatientId=" + PatientId.ToString().Trim();
                oDB.Connect(false);
                oDB.Retrive_Query(strQry,out dt);
                oDB.Disconnect();

                if ((dt !=null) && (dt.Rows.Count > 0))
                {
                    for (int rowCnt = 0; rowCnt < dt.Rows.Count; rowCnt++)
                    {
                        switch (Convert.ToString(dt.Rows[rowCnt]["sExternalSubType"]).ToUpper())
                        {
                            case "PERSON":
                                _strPesron = Convert.ToString(dt.Rows[rowCnt]["sExternalValue"]);
                                break;
                            case "PERSONHSI":
                                _strPersonhsi = Convert.ToString(dt.Rows[rowCnt]["sExternalValue"]);
                                break;

                                // commented by Abhijeet
                            //default :      
                            //    _strPesron = "";
                            //    _strPersonhsi = "";
                            //    break;
                        }
                    }                   
                }
                else
                {
                  _strPesron = "";
                  _strPersonhsi = "";
                }
            }
            catch (Exception ex)
            {
                _strPesron = "";
                _strPersonhsi = "";
           
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);    
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }                      
        }
        // End of changes by Abhijeet for set the  Person & PersonHsi for patient.


        /// <summary>
        /// Generate xml file that will be store in Outbox folder to pass to emdeon API's as request.
        /// </summary>
        /// <returns></returns>
        public string GenerateXMLFIlE(string strRequestType, object xmlContent)
        {
            string _outboxFIlename = strOutboxFilePath + "\\" + (gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") + ".xml");
            try
            {


                // Dim writerString As New StringWriter() 
                XmlTextWriter writer = new XmlTextWriter(_outboxFIlename, null);
               // string validate=string.Empty;

                //Madan--2010414---- added for generating XML file according to session Id
                writer.WriteStartDocument();
                if (ConfirmNull(clsEmdeonGeneral.gloLab_SessionId))
                {
                    writer.WriteStartElement("REQUEST");
                    writer.WriteAttributeString("sessionid", clsEmdeonGeneral.gloLab_SessionId);
                    writer.WriteAttributeString("facility", clsEmdeonGeneral.emdeonFacilityCode);           
                }
                else
                {
                    writer.WriteStartElement("REQUEST");
                    writer.WriteAttributeString("userid", clsEmdeonGeneral.emdeonUserName);
                    writer.WriteAttributeString("password", clsEmdeonGeneral.emdeonUserPassword);                     
                }                   
                       
                
                //end Madan...
                clsgloLabPatientLayer objgloPatientDB = new clsgloLabPatientLayer();
                string _gender = string.Empty;
                string _relationtype = string.Empty;
                string _empStatus = string.Empty;
                string _Grelationtype = string.Empty;
                string _phAreaCode = string.Empty;//Phone area code.
                string _phoneNo = string.Empty;//Phone No;
                // Boolean _validateNul = false;
                switch (strRequestType.ToLower())
                {                        
                    case "order_search_by_order_info":
                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "order");
                        writer.WriteAttributeString("op", "search_by_order_info");
                        // increase date duration 
                        TimeSpan ts = new TimeSpan(0, 30, 0, 0, 0);
                        //writer.WriteElementString("collection_datetime_from", "01/28/2010");
                        //writer.WriteElementString("collection_datetime_to", "01/29/2010");
                        
                        //writer.WriteElementString("collection_datetime_from", Convert.ToString(System.DateTime.Now.Subtract(ts)));
                        //writer.WriteElementString("collection_datetime_to", System.DateTime.Now.AddHours(10).ToString());

                        //12-Sep-14 Aniket: Fixing Incident #00036876: Provider ID getting 0 saved while reloading labs
                        writer.WriteElementString("request_date_from", Convert.ToString(System.DateTime.Now.Subtract(ts)));
                        writer.WriteElementString("request_date_to", System.DateTime.Now.AddHours(10).ToString());

                        //writer.WriteElementString("person", PatientId.ToString());
                        writer.WriteElementString("orderingorganization", clsEmdeonGeneral.emdeonFacilityCode);
                        writer.WriteEndElement();
                        break;
                    // By Abhijeet On Date 20100318
                    // written code for XML file format for retrieving order with option "search_by_Patient_info"
                    case "order_search_by_patient_info_saveclose":  // On Save & close button it is used
                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "order");
                        writer.WriteAttributeString("op", "search_by_order_info");

                        // by Abhijeet on 20100429
                        // obtain person ,personhsi with another new method

                        //_strPesron = ""; 
                        //_strPersonhsi = "";
                        //SetPersonPersonhsi(); // Setting Patient's Person & pesrhsi
                        //writer.WriteElementString("person", _strPesron.ToString());
                        //writer.WriteElementString("personhsi", _strPersonhsi.ToString());

                        _strPesron = "";
                        _strPersonhsi = "";
                        Classes.clsGeneral objclsGeneral = new clsGeneral();
                        objclsGeneral.IsgloLabRegisteredPatient(PatientId, out _strPesron, out _strPersonhsi);
                        objclsGeneral.Dispose();
                        writer.WriteElementString("person", _strPesron.ToString());
                        //    START : Removed by Sandip Deshmukh, V:8070.
                        //    Purpose : PersonHSI is non mandatory field and when their are more than one personhsi is associated at Emdeon. 
                        //    The orders were not coming into glo
                        //writer.WriteElementString("personhsi", _strPersonhsi.ToString());
                        //     END : Removed by Sandip Deshmukh, V:8070.                      
                        // End of changes by Abhijeet on 20100429 for getting person ,personhsi

                        // End of changes by Abhijeet on 20100429 for getting person ,personhsi

                        // increase date duration 
                        TimeSpan tsDays = new TimeSpan(7, 0, 0, 0, 0);
                        //writer.WriteElementString("collection_datetime_from", Convert.ToString(System.DateTime.Now.Subtract(tsDays)));
                        //writer.WriteElementString("collection_datetime_to", System.DateTime.Now.AddHours(10).ToString());                        

                        //12-Sep-14 Aniket: Fixing Incident #00036876: Provider ID getting 0 saved while reloading labs
                        writer.WriteElementString("request_date_from", Convert.ToString(System.DateTime.Now.Subtract(tsDays)));
                        writer.WriteElementString("request_date_to", System.DateTime.Now.AddHours(10).ToString());                        

                       //writer.WriteElementString("request_date_from", Convert.ToString(System.DateTime.Now.Subtract(tsDays)));
                       //writer.WriteElementString("request_date_to", System.DateTime.Now.AddHours(10).ToString());                        
                      
                        writer.WriteElementString("orderingorganization", clsEmdeonGeneral.emdeonFacilityCode);
                        writer.WriteEndElement();
                        break;
                    case "order_search_by_patient_info_refresh": // On Refresh button it is used
                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "order");                    
                        writer.WriteAttributeString("op", "search_by_patient_info");
                        // Required  Parameters for this operation are added from below on xml request
                        //****
                        // by Abhijeet on 20100429
                        // obtain person ,personhsi with another new method
                        //_strPesron="";
                        //_strPersonhsi = "";
                        //SetPersonPersonhsi();  // Setting Patient's Person & pesrhsi
                        //writer.WriteElementString("person", _strPesron.ToString());
                        //writer.WriteElementString("personhsi", _strPersonhsi.ToString());
                        TimeSpan ts_refresh = new TimeSpan(clsEmdeonGeneral.iEmdeonGetLabOrdersFromDaysOnReload, 0, 0, 0);
                         _strPesron = "";
                         _strPersonhsi = "";
                         Classes.clsGeneral oclsGeneral = new clsGeneral();
                         oclsGeneral.IsgloLabRegisteredPatient(PatientId, out _strPesron, out _strPersonhsi);
                         oclsGeneral.Dispose();
                         writer.WriteElementString("person", _strPesron.ToString());
                         //    START : Removed by Sandip Deshmukh, V:8070.
                         //    Purpose : PersonHSI is non mandatory field and when their are more than one personhsi is associated at Emdeon. 
                         //    The orders were not coming into glo
                         //writer.WriteElementString("personhsi", _strPersonhsi.ToString());
                         //     END : Removed by Sandip Deshmukh, V:8070.                      
                         // End of changes by Abhijeet on 20100429 for getting person ,personhsi

                                               
                        // End of changes by Abhijeet on 20100429 for getting person ,personhsi

                        writer.WriteElementString("orderingorganization", clsEmdeonGeneral.emdeonFacilityCode);
                        //**** END of Required Parameters.

                        // Optional Parameters for this operation are added from below on xml request
                        //****                        
                        writer.WriteElementString("request_date_from", Convert.ToString(System.DateTime.Now.Subtract(ts_refresh)));
                        writer.WriteElementString("request_date_to", System.DateTime.Now.AddHours(10).ToString());
                        //**** END of Optional Parameters.
                        writer.WriteEndElement();
                        break;
                    // End of changes by Abhijeet 
                    case "search_diag_code":
                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "orderdiagnosis");
                        writer.WriteAttributeString("op", "search_diag_code");
                        writer.WriteElementString("order", xmlContent.ToString());
                        writer.WriteEndElement();
                        break;
                    case "order_update":
                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "order");
                        writer.WriteAttributeString("op", "update");
                        writer.WriteElementString("order", _strOrderRef.ToString());
                        writer.WriteElementString("order_status", _strOrderStatus.ToString());
                        writer.WriteEndElement();
                        break;
                    //Added my madan on 20100612
                    case "retrieve_selected_order":
                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "order");
                        writer.WriteAttributeString("op", "get");
                        writer.WriteElementString("order", _orderRefreanceID.ToString());
                        writer.WriteEndElement();
                        break;

                    // Changed Three API call structure to two API call to get "Lab test name" Durga Prasad - 02/20/20100 
                    //case "ordertest_search":
                    //    writer.WriteStartElement("OBJECT");
                    //    writer.WriteAttributeString("name", "ordertest");
                    //    writer.WriteAttributeString("op", "search");
                    //    writer.WriteElementString("order", xmlContent.ToString());
                    //    writer.WriteEndElement();
                    //    break;
                    //case "orderable_search":
                    //    writer.WriteStartElement("OBJECT");
                    //    writer.WriteAttributeString("name", "orderable");
                    //    writer.WriteAttributeString("op", "search");
                    //    writer.WriteElementString("orderable", xmlContent.ToString());
                    //    writer.WriteEndElement();
                    //    break;
                    //MADAN

                    //case "hsilabel_search":
                    //    validate = "hsilabel_search";
                    //    Boolean checkv = gloPatientDB.ValidatePatient(validate);
                    //    if (checkv = true)
                    //    {


                    //        // oPatient = (gloPatient.Patient)xmlContent;
                    //        writer.WriteStartElement("OBJECT");
                    //        writer.WriteAttributeString("name", "hsilabel");
                    //        writer.WriteAttributeString("op", "search");
                    //        writer.WriteElementString("organization", clsEmdeonGeneral.emdeonFacilityCode);
                    //        writer.WriteElementString("is_hsi_for", "Patient");
                    //        writer.WriteElementString("registration", "Y");
                    //        writer.WriteEndElement();
                    //    }
                    //    break;

                    //MADAN Registering Patient in gloLab(Emdeon)
                    case "payment_put":
                        gloPatient.Patient oPatient;
                        oPatient = (gloPatient.Patient)xmlContent;
                        //assigning value to check the referance value in the patient_gloLab(database) 
                        string IpatID = oPatient.DemographicsDetail.PatientCode.ToString();
                        //fetching the refernce value from the patient_gloLab(database) 

                        if (ConfirmNull(clsEmdeonGeneral.gloLab_personRefval.ToString()) == true)
                        {
                            writer.WriteStartElement("OBJECT");
                            //Registering payment mode using "personprovinfo".put method.
                            writer.WriteAttributeString("name", "personprovinfo");
                            writer.WriteAttributeString("op", "put");

                            writer.WriteElementString("person", clsEmdeonGeneral.gloLab_personRefval.ToString());
                            //assigning bill_type... Payment mode.
                            writer.WriteElementString("bill_type", clsEmdeonGeneral.gloLab_BillingType.ToString());
                            writer.WriteElementString("organization", clsEmdeonGeneral.emdeonFacilityCode.ToString());
                            writer.WriteEndElement();
                            clsEmdeonGeneral.sgloLabtype = "ProviderInfo";
                            //clsEmdeonGeneral.gloLab_patID = Convert.ToInt64(oPatient.DemographicsDetail.PatientID.ToString());
                            clsEmdeonGeneral.gloLab_patcode = oPatient.DemographicsDetail.PatientCode.ToString();
                        }

                        break;
                    case "person_put":

                       // validate = "person_put";

                        oPatient = (gloPatient.Patient)xmlContent;
                        //gloPatient.Patient oPatient = new gloPatient.Patient();
                        //_validateNul = objgloPatientDB.ValidatePatient(validate, oPatient);

                        if (ConfirmNull(oPatient.DemographicsDetail.PatientGender.ToString()))
                        {
                            if (oPatient.DemographicsDetail.PatientGender != "Other")
                            {
                                _gender = oPatient.DemographicsDetail.PatientGender.ToString();
                            }
                            else
                                _gender = "Unknown";

                        }
                        if (ConfirmNull(oPatient.DemographicsDetail.PatientPhone.ToString()))
                        {
                            string _phNo = oPatient.DemographicsDetail.PatientPhone.ToString();
                            _phAreaCode = _phNo.Substring(0, 3);
                            _phoneNo = _phNo.Substring(3);

                        }
                        //if (_validateNul == true)
                        //{

                        oPatient = (gloPatient.Patient)xmlContent;
                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "person");
                        writer.WriteAttributeString("op", "put");

                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientAddress1.ToString())) == true)
                        {
                            writer.WriteElementString("address_1", oPatient.DemographicsDetail.PatientAddress1.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientAddress2.ToString())) == true)
                        {
                            writer.WriteElementString("address_2", oPatient.DemographicsDetail.PatientAddress2.ToString());
                        }

                        writer.WriteElementString("birth_date", oPatient.DemographicsDetail.PatientDOB.ToString());

                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientCity.ToString())) == true)
                        {
                            writer.WriteElementString("city", oPatient.DemographicsDetail.PatientCity.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientCountry.ToString())) == true)
                        {
                            writer.WriteElementString("country", oPatient.DemographicsDetail.PatientCountry.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientCounty.ToString())) == true)
                        {
                            writer.WriteElementString("county", oPatient.DemographicsDetail.PatientCounty.ToString());
                        }
                        writer.WriteElementString("first_name", oPatient.DemographicsDetail.PatientFirstName.ToString());

                        //writer.WriteElementString("height", oPatient);
                        // writer.WriteElementString("home_phone_area_code", xmlContent.ToString());
                        //writer.WriteElementString("home_phone_number", xmlContent.ToString());

                        writer.WriteElementString("last_name", oPatient.DemographicsDetail.PatientLastName.ToString());
                        if ((ConfirmNull(oPatient.DemographicsDetail.PharmacyName.ToString())) == true)
                        {
                            writer.WriteElementString("favorite_pharmacy", oPatient.DemographicsDetail.PharmacyName.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientMiddleName.ToString())) == true)
                        {
                            writer.WriteElementString("middle_name", oPatient.DemographicsDetail.PatientMiddleName.ToString());
                        }
                        if ((ConfirmNull(oPatient.GuardianDetail.PatientMotherFirstName.ToString())) == true)
                        {
                            writer.WriteElementString("mother_name", oPatient.GuardianDetail.PatientMotherFirstName.ToString());
                        }
                        //writer.WriteElementString("name_prefix", oPatient.DemographicsDetail .ToString());

                        writer.WriteElementString("sex", _gender.ToString());
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientSSN.ToString())) == true)
                        {
                            writer.WriteElementString("ssn", oPatient.DemographicsDetail.PatientSSN.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientState.ToString())) == true)
                        {
                            writer.WriteElementString("state", oPatient.DemographicsDetail.PatientState.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientPhone.ToString())) == true)
                        {
                            writer.WriteElementString("home_phone_number", _phoneNo.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientPhone.ToString())) == true)
                        {
                            writer.WriteElementString("home_phone_area_code", _phAreaCode.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientZip.ToString())) == true)
                        {
                            if (oPatient.DemographicsDetail.PatientZip.Length > 4)
                            {
                                writer.WriteElementString("zip", oPatient.DemographicsDetail.PatientZip.ToString());
                            }
                            else
                            {
                                string _Pzip = oPatient.DemographicsDetail.PatientZip.ToString();
                                _Pzip = string.Concat("0", _Pzip);
                                writer.WriteElementString("zip", _Pzip.ToString());
                            }
                        }
                        if ((ConfirmNull(oPatient.OccupationDetail.PatientWorkPhone.ToString())) == true)
                        {
                            writer.WriteElementString("work_phone_number", oPatient.OccupationDetail.PatientWorkPhone.ToString());
                        }
                        //writer.WriteElementString("person_hsi", oPatient.DemographicsDetail.PatientCode.ToString());
                        writer.WriteElementString("hsilabel", clsEmdeonGeneral.gloLab_hsilabel.ToString());
                        writer.WriteElementString("hsi_value", oPatient.DemographicsDetail.PatientCode.ToString());
                        writer.WriteEndElement();
                        clsEmdeonGeneral.sgloLabtype = "person";
                        // clsEmdeonGeneral.gloLab_patID = Convert.ToInt64(oPatient.DemographicsDetail.PatientID.ToString());
                        clsEmdeonGeneral.gloLab_patcode = oPatient.DemographicsDetail.PatientCode.ToString();

                        //}

                        break;

                    // Registerin ISP information
                    case "isp_put":

                        oPatient = (gloPatient.Patient)xmlContent;
                       // validate = "isp_put";
                        //_validateNul = objgloPatientDB.ValidatePatient(validate, oPatient);
                        //if (_validateNul == true)
                        //{
                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "isp");
                        writer.WriteAttributeString("op", "put");

                        writer.WriteElementString("address_1", oPatient.InsuranceDetails.InsurancesDetails[0].AddrressLine1.ToString());
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].AddrressLine2.ToString()))
                        {
                            writer.WriteElementString("address_2", oPatient.InsuranceDetails.InsurancesDetails[0].AddrressLine2.ToString());
                        }
                        writer.WriteElementString("city", oPatient.InsuranceDetails.InsurancesDetails[0].City.ToString());
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Country.ToString()))
                        {
                            writer.WriteElementString("country", oPatient.InsuranceDetails.InsurancesDetails[0].Country.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Fax.ToString()))
                        {
                            writer.WriteElementString("fax", oPatient.InsuranceDetails.InsurancesDetails[0].Fax.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].InsuranceName.ToString()))
                        {
                            writer.WriteElementString("name", oPatient.InsuranceDetails.InsurancesDetails[0].InsuranceName.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Phone.ToString()))
                        {
                            writer.WriteElementString("phone", oPatient.InsuranceDetails.InsurancesDetails[0].Phone.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].State.ToString()))
                        {
                            writer.WriteElementString("state", oPatient.InsuranceDetails.InsurancesDetails[0].State.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].ZIP.ToString()))
                        {
                            if (oPatient.InsuranceDetails.InsurancesDetails[0].ZIP.Length > 4)
                            {
                                writer.WriteElementString("zip", oPatient.InsuranceDetails.InsurancesDetails[0].ZIP.ToString());
                            }
                            else
                            {
                                string _Pzip = oPatient.InsuranceDetails.InsurancesDetails[0].ZIP.ToString();
                                _Pzip = string.Concat("0", _Pzip);
                                writer.WriteElementString("zip", _Pzip.ToString());
                            }

                        }
                        writer.WriteEndElement();
                        clsEmdeonGeneral.sgloLabtype = "isp";
                        clsEmdeonGeneral.gloLab_patcode = oPatient.DemographicsDetail.PatientCode.ToString();
                        //}
                        break;
                    //For Register guarantor information in gloLab

                    case "guarantor_put":
                        //validate = "guarantor_put";
                        oPatient = (gloPatient.Patient)xmlContent;

                        //guarantor gender check
                        string _gGender = string.Empty;
                        if (ConfirmNull(oPatient.PatientGuarantors[0].Gender.ToString()))
                        {
                            if (oPatient.PatientGuarantors[0].Gender != "Other")
                            {
                                _gGender = oPatient.PatientGuarantors[0].Gender.ToString();
                            }
                            else
                                _gGender = "Unknown";

                        }
                        //_validateNul = objgloPatientDB.ValidatePatient(validate, oPatient);
                        //if (_validateNul == true)
                        //{
                        if (ConfirmNull(oPatient.PatientGuarantors[0].Relation.ToString()))
                        {
                            _Grelationtype = objgloPatientDB.GetInsuranceRelationshipType(oPatient.PatientGuarantors[0].Relation.ToString());
                        }

                        // string LpatID = oPatient.DemographicsDetail.PatientCode.ToString();
                        //string reference_val = objgloPatientDB.ReferenceValue(LpatID, "person");



                        //gloLab_emp.
                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "guarantor");
                        writer.WriteAttributeString("op", "put");

                        //writer.WriteElementString("cob_priority",)
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].AddressLine1.ToString())) == true)
                        {
                            writer.WriteElementString("address_1", oPatient.PatientGuarantors[0].AddressLine1.ToString());
                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].AddressLine2.ToString())) == true)
                        {
                            writer.WriteElementString("address_2", oPatient.PatientGuarantors[0].AddressLine2.ToString());
                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].Phone.ToString())) == true)
                        {
                            writer.WriteElementString("home_phone", oPatient.PatientGuarantors[0].Phone.ToString());
                        }

                        //    writer.WriteElementString("birth_date", oPatient.PatientGuarantors[0].DOB.ToString());
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].City.ToString())) == true)
                        {
                            writer.WriteElementString("city", oPatient.PatientGuarantors[0].City.ToString());
                        }

                        writer.WriteElementString("guarantor_sex", _gGender.ToString());
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].Phone.ToString())) == true)
                        {
                            writer.WriteElementString("work_phone", oPatient.PatientGuarantors[0].Phone.ToString());
                        }

                        if ((ConfirmNull(oPatient.PatientGuarantors[0].LastName.ToString())) == true)
                        {
                            writer.WriteElementString("last_name", oPatient.PatientGuarantors[0].LastName.ToString());
                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].MiddleName.ToString())) == true)
                        {
                            writer.WriteElementString("middle_name", oPatient.PatientGuarantors[0].MiddleName.ToString());
                        }
                        writer.WriteElementString("relationship", _Grelationtype.ToString());
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].SSN.ToString())) == true)
                        {
                            writer.WriteElementString("ssn", oPatient.PatientGuarantors[0].SSN.ToString());
                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].State.ToString())) == true)
                        {
                            writer.WriteElementString("state", oPatient.PatientGuarantors[0].State.ToString());
                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].Zip.ToString())) == true)
                        {
                            if (oPatient.PatientGuarantors[0].Zip.Length > 4)
                            {
                                writer.WriteElementString("zip", oPatient.PatientGuarantors[0].Zip.ToString());
                            }
                            else
                            {
                                string _Pzip = oPatient.PatientGuarantors[0].Zip.ToString();
                                _Pzip = string.Concat("0", _Pzip);
                                writer.WriteElementString("zip", _Pzip.ToString());
                            }

                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].FirstName.ToString())) == true)
                        {
                            writer.WriteElementString("first_name", oPatient.PatientGuarantors[0].FirstName.ToString());
                        }
                        if ((ConfirmNull(oPatient.PatientGuarantors[0].DOB.ToString())) == true)
                        {
                            writer.WriteElementString("birth_date", oPatient.PatientGuarantors[0].DOB.ToString());
                        }
                        writer.WriteElementString("person", clsEmdeonGeneral.gloLab_personRefval.ToString());
                        writer.WriteEndElement();
                        clsEmdeonGeneral.sgloLabtype = "Guarantor";
                        //clsEmdeonGeneral.gloLab_patID = Convert.ToInt64(oPatient.DemographicsDetail.PatientID.ToString());
                        clsEmdeonGeneral.gloLab_patcode = oPatient.DemographicsDetail.PatientCode.ToString();


                        //}

                        break;
                    case "insurance_add":

                        //validate = "Insurance_add";
                        oPatient = (gloPatient.Patient)xmlContent;
                        //_validateNul = objgloPatientDB.ValidatePatient(validate, oPatient);

                        long _cobtype = 0;
                        //if (_validateNul == true)
                        //{


                        _cobtype = oPatient.InsuranceDetails.InsurancesDetails[0].InsuranceFlag;
                        //cobType--Eg:primary,scondary,teritary

                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].RelationshipName.ToString()))
                        {
                            _relationtype = objgloPatientDB.GetInsuranceRelationshipType(oPatient.InsuranceDetails.InsurancesDetails[0].RelationshipName.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.PatientEmploymentStatus.ToString()))
                        {
                            _empStatus = objgloPatientDB.GetEmployeeStatus(oPatient.OccupationDetail.PatientEmploymentStatus.ToString());
                        }

                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "insurance");
                        writer.WriteAttributeString("op", "put");

                        writer.WriteElementString("cob_priority", _cobtype.ToString());

                        writer.WriteElementString("expiration_date", oPatient.InsuranceDetails.InsurancesDetails[0].EndDate.ToString());

                        writer.WriteElementString("effective_date", oPatient.InsuranceDetails.InsurancesDetails[0].StartDate.ToString());


                        //writer.WriteElementString("expiration_date", oPatient.InsuranceDetails.InsurancesDetails[0].EndDate.ToString());
                        //writer.WriteElementString("group_name", oPatient.InsuranceDetails.InsurancesDetails[oPatient].SubscriberPolicy);
                        //if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Group.ToString()))
                        //{
                        //writer.WriteElementString("plan_identifier", oPatient.InsuranceDetails.InsurancesDetails[0].InsuranceID.ToString());
                        //}
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Group.ToString()))
                        {
                            writer.WriteElementString("group_number", oPatient.InsuranceDetails.InsurancesDetails[0].Group.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberAddr1.ToString()))
                        {
                            writer.WriteElementString("insured_address_1", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberAddr1.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberAddr2.ToString()))
                        {
                            writer.WriteElementString("insured_address_2", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberAddr2.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].DOB.ToString()))
                        {
                            writer.WriteElementString("insured_birth_date", oPatient.InsuranceDetails.InsurancesDetails[0].DOB.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberCity.ToString()))
                        {
                            writer.WriteElementString("insured_city", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberCity.ToString());
                        }
                        //if (ConfirmNull(oPatient)))
                        //{
                        //    writer.WriteElementString("insured_employee_id", oPatient.OccupationDetail.PatientWorkAddress1.ToString());
                        //}
                        if (ConfirmNull(oPatient.OccupationDetail.PatientWorkAddress1.ToString()))
                        {
                            writer.WriteElementString("insured_empl_address_1", oPatient.OccupationDetail.PatientWorkAddress1.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.PatientWorkAddress2.ToString()))
                        {
                            writer.WriteElementString("insured_empl_address_2", oPatient.OccupationDetail.PatientWorkAddress2.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.PatientWorkCity.ToString()))
                        {
                            writer.WriteElementString("insured_empl_city", oPatient.OccupationDetail.PatientWorkCity.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.EmployerName.ToString()))
                        {
                            writer.WriteElementString("insured_empl_name", oPatient.OccupationDetail.EmployerName.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.PatientWorkState.ToString()))
                        {
                            writer.WriteElementString("insured_empl_state", oPatient.OccupationDetail.PatientWorkState.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.PatientWorkZip.ToString()))
                        {
                            writer.WriteElementString("insured_empl_zip", oPatient.OccupationDetail.PatientWorkZip.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberFName.ToString()))
                        {
                            writer.WriteElementString("insured_first_name", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberFName.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberLName.ToString()))
                        {
                            writer.WriteElementString("insured_last_name", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberLName.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberMName.ToString()))
                        {
                            writer.WriteElementString("insured_middle_name", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberMName.ToString());
                        }
                        //writer.WriteElementString("insured_name_suffix", oPatient.Insurances[0].SubscriberMName.ToString());
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberGender.ToString()))
                        {
                            writer.WriteElementString("insured_sex", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberGender.ToString());
                        }
                        //writer.WriteElementString("insured_ssn", oPatient.Insurances[0].ToString());
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberState.ToString()))
                        {
                            writer.WriteElementString("insured_state", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberState.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Phone.ToString()))
                        {
                            //Splitting... phoneNo and area code..
                            string _phNo = oPatient.InsuranceDetails.InsurancesDetails[0].Phone.ToString();
                            _phAreaCode = _phNo.Substring(0, 3);
                            _phoneNo = _phNo.Substring(3);
                            writer.WriteElementString("insured_home_phone_number", _phoneNo.ToString());
                            writer.WriteElementString("insured_home_phone_area_code", _phAreaCode.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.PatientWorkPhone.ToString()))
                        {
                            _phAreaCode = string.Empty;
                            _phoneNo = string.Empty;
                            string _phNo = oPatient.OccupationDetail.PatientWorkPhone.ToString();
                            _phAreaCode = _phNo.Substring(0, 3);
                            _phoneNo = _phNo.Substring(3);
                            writer.WriteElementString("insured_work_phone_area_code", _phAreaCode.ToString());
                            writer.WriteElementString("insured_work_phone_number", _phoneNo.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberZip.ToString()))
                        {
                            if (oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberZip.Length > 4)
                            {
                                writer.WriteElementString("insured_zip", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberZip.ToString());
                            }
                            else
                            {
                                string _Pzip = oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberZip.ToString();
                                _Pzip = string.Concat("0", _Pzip);
                                writer.WriteElementString("insured_zip", _Pzip.ToString());
                            }

                        }
                        //if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].i.ToString()))
                        //{
                        writer.WriteElementString("policy_number", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberID.ToString());

                        if (ConfirmNull(_empStatus.ToString()))
                        {
                            writer.WriteElementString("insured_employment_status", _empStatus.ToString());
                        }
                        if (ConfirmNull(_relationtype.ToString()))
                        {
                            writer.WriteElementString("patient_rel_to_insured", _relationtype.ToString());
                        }

                        writer.WriteElementString("isp", clsEmdeonGeneral.gloLab_ispRefval.ToString());
                        writer.WriteElementString("organization", clsEmdeonGeneral.emdeonFacilityCode.ToString());
                        writer.WriteElementString("person", clsEmdeonGeneral.gloLab_personRefval.ToString());

                        //
                        writer.WriteEndElement();
                        clsEmdeonGeneral.sgloLabtype = "Insurance";
                        clsEmdeonGeneral.gloLab_patcode = oPatient.DemographicsDetail.PatientCode.ToString();
                       

                        //}
                        break;
                    case "person_update":

                        oPatient = (gloPatient.Patient)xmlContent;
                        //gloPatient.Patient oPatient = new gloPatient.Patient();
                        //_validateNul = objgloPatientDB.ValidatePatient(validate, oPatient);


                        if (ConfirmNull(oPatient.DemographicsDetail.PatientGender.ToString()))
                        {
                            if (oPatient.DemographicsDetail.PatientGender != "Other")
                            {
                                _gender = oPatient.DemographicsDetail.PatientGender.ToString();
                            }
                            else
                                _gender = "Unknown";

                        }
                        if (ConfirmNull(oPatient.DemographicsDetail.PatientPhone.ToString()))
                        {
                            string _phNo = oPatient.DemographicsDetail.PatientPhone.ToString();
                            _phAreaCode = _phNo.Substring(0, 3);
                            _phoneNo = _phNo.Substring(3);

                        }

                        oPatient = (gloPatient.Patient)xmlContent;
                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "person");
                        writer.WriteAttributeString("op", "update");

                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientAddress1.ToString())) == true)
                        {
                            writer.WriteElementString("address_1", oPatient.DemographicsDetail.PatientAddress1.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientAddress2.ToString())) == true)
                        {
                            writer.WriteElementString("address_2", oPatient.DemographicsDetail.PatientAddress2.ToString());
                        }

                        writer.WriteElementString("birth_date", oPatient.DemographicsDetail.PatientDOB.ToString());

                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientCity.ToString())) == true)
                        {
                            writer.WriteElementString("city", oPatient.DemographicsDetail.PatientCity.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientCountry.ToString())) == true)
                        {
                            writer.WriteElementString("country", oPatient.DemographicsDetail.PatientCountry.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientCounty.ToString())) == true)
                        {
                            writer.WriteElementString("county", oPatient.DemographicsDetail.PatientCounty.ToString());
                        }
                        writer.WriteElementString("first_name", oPatient.DemographicsDetail.PatientFirstName.ToString());

                        //writer.WriteElementString("height", oPatient);
                        // writer.WriteElementString("home_phone_area_code", xmlContent.ToString());
                        //writer.WriteElementString("home_phone_number", xmlContent.ToString());

                        writer.WriteElementString("last_name", oPatient.DemographicsDetail.PatientLastName.ToString());
                        if ((ConfirmNull(oPatient.DemographicsDetail.PharmacyName.ToString())) == true)
                        {
                            writer.WriteElementString("favorite_pharmacy", oPatient.DemographicsDetail.PharmacyName.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientMiddleName.ToString())) == true)
                        {
                            writer.WriteElementString("middle_name", oPatient.DemographicsDetail.PatientMiddleName.ToString());
                        }
                        if ((ConfirmNull(oPatient.GuardianDetail.PatientMotherFirstName.ToString())) == true)
                        {
                            writer.WriteElementString("mother_name", oPatient.GuardianDetail.PatientMotherFirstName.ToString());
                        }
                        //writer.WriteElementString("name_prefix", oPatient.DemographicsDetail .ToString());

                        writer.WriteElementString("sex", _gender.ToString());
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientSSN.ToString())) == true)
                        {
                            writer.WriteElementString("ssn", oPatient.DemographicsDetail.PatientSSN.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientState.ToString())) == true)
                        {
                            writer.WriteElementString("state", oPatient.DemographicsDetail.PatientState.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientPhone.ToString())) == true)
                        {
                            writer.WriteElementString("home_phone_number", _phoneNo.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientPhone.ToString())) == true)
                        {
                            writer.WriteElementString("home_phone_area_code", _phAreaCode.ToString());
                        }
                        if ((ConfirmNull(oPatient.DemographicsDetail.PatientZip.ToString())) == true)
                        {
                            if (oPatient.DemographicsDetail.PatientZip.Length > 4)
                            {
                                writer.WriteElementString("zip", oPatient.DemographicsDetail.PatientZip.ToString());
                            }
                            else
                            {
                                string _Pzip = oPatient.DemographicsDetail.PatientZip.ToString();
                                _Pzip = string.Concat("0", _Pzip);
                                writer.WriteElementString("zip", _Pzip.ToString());
                            }
                        }
                        if ((ConfirmNull(oPatient.OccupationDetail.PatientWorkPhone.ToString())) == true)
                        {
                            writer.WriteElementString("work_phone_number", oPatient.OccupationDetail.PatientWorkPhone.ToString());
                        }
                        writer.WriteElementString("person", clsEmdeonGeneral.gloLab_personRefval.ToString());
                        writer.WriteEndElement();
                        clsEmdeonGeneral.sgloLabtype = "person";
                        clsEmdeonGeneral.sgloLab_Value = clsEmdeonGeneral.gloLab_personRefval.ToString();
                        clsEmdeonGeneral.gloLab_patcode = oPatient.DemographicsDetail.PatientCode.ToString();
                        break;
                    case "payment_update":
                        oPatient = (gloPatient.Patient)xmlContent;

                        //fetching the refernce value from the patient_gloLab(database) 

                        if (ConfirmNull(clsEmdeonGeneral.gloLab_personRefval.ToString()) == true)
                        {
                            writer.WriteStartElement("OBJECT");
                            //Registering payment mode using "personprovinfo".put method.
                            writer.WriteAttributeString("name", "personprovinfo");
                            writer.WriteAttributeString("op", "update");

                            writer.WriteElementString("person", clsEmdeonGeneral.gloLab_personRefval.ToString());
                            //assigning bill_type... Payment mode.
                            writer.WriteElementString("bill_type", clsEmdeonGeneral.gloLab_BillingType.ToString());
                            writer.WriteElementString("personprovinfo", clsEmdeonGeneral.gloLab_providerRefval.ToString());
                            writer.WriteEndElement();
                            clsEmdeonGeneral.sgloLabtype = "ProviderInfo";
                            //clsEmdeonGeneral.gloLab_patID = Convert.ToInt64(oPatient.DemographicsDetail.PatientID.ToString());
                            clsEmdeonGeneral.gloLab_patcode = oPatient.DemographicsDetail.PatientCode.ToString();
                            clsEmdeonGeneral.sgloLab_Value = clsEmdeonGeneral.gloLab_providerRefval.ToString();
                        }

                        break;
                    // Registerin ISP information
                    case "isp_update":

                        oPatient = (gloPatient.Patient)xmlContent;
                       // validate = "isp_put";
                        //_validateNul = objgloPatientDB.ValidatePatient(validate, oPatient);
                        //if (_validateNul == true)
                        //{
                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "isp");
                        writer.WriteAttributeString("op", "update");

                        writer.WriteElementString("address_1", oPatient.InsuranceDetails.InsurancesDetails[0].AddrressLine1.ToString());
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].AddrressLine2.ToString()))
                        {
                            writer.WriteElementString("address_2", oPatient.InsuranceDetails.InsurancesDetails[0].AddrressLine2.ToString());
                        }

                        writer.WriteElementString("city", oPatient.InsuranceDetails.InsurancesDetails[0].City.ToString());
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Country.ToString()))
                        {
                            writer.WriteElementString("country", oPatient.InsuranceDetails.InsurancesDetails[0].Country.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Fax.ToString()))
                        {
                            writer.WriteElementString("fax", oPatient.InsuranceDetails.InsurancesDetails[0].Fax.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].InsuranceName.ToString()))
                        {
                            writer.WriteElementString("name", oPatient.InsuranceDetails.InsurancesDetails[0].InsuranceName.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Phone.ToString()))
                        {
                            writer.WriteElementString("phone", oPatient.InsuranceDetails.InsurancesDetails[0].Phone.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].State.ToString()))
                        {
                            writer.WriteElementString("state", oPatient.InsuranceDetails.InsurancesDetails[0].State.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].ZIP.ToString()))
                        {
                            if (oPatient.InsuranceDetails.InsurancesDetails[0].ZIP.Length > 4)
                            {
                                writer.WriteElementString("zip", oPatient.InsuranceDetails.InsurancesDetails[0].ZIP.ToString());
                            }
                            else
                            {
                                string _Pzip = oPatient.InsuranceDetails.InsurancesDetails[0].ZIP.ToString();
                                _Pzip = string.Concat("0", _Pzip);
                                writer.WriteElementString("zip", _Pzip.ToString());
                            }

                        }

                        writer.WriteElementString("isp", clsEmdeonGeneral.gloLab_ispRefval.ToString());

                        writer.WriteEndElement();
                        clsEmdeonGeneral.sgloLabtype = "isp";
                        clsEmdeonGeneral.sgloLab_Value = clsEmdeonGeneral.gloLab_ispRefval.ToString();
                        clsEmdeonGeneral.gloLab_patcode = oPatient.DemographicsDetail.PatientCode.ToString();

                        //}
                        break;
                    case "guarantor_update":
                       // validate = "guarantor_put";
                        oPatient = (gloPatient.Patient)xmlContent;
                        // 

                        if (ConfirmNull(oPatient.PatientGuarantors[0].Gender.ToString()))
                        {
                            if (oPatient.PatientGuarantors[0].Gender != "Other")
                            {
                                _gender = oPatient.PatientGuarantors[0].Gender.ToString();
                            }
                            else
                                _gender = "Unknown";

                        }
                        //_validateNul = objgloPatientDB.ValidatePatient(validate, oPatient);
                        //if (_validateNul == true)
                        //{
                        if (ConfirmNull(oPatient.PatientGuarantors[0].Relation.ToString()))
                        {
                            _Grelationtype = objgloPatientDB.GetInsuranceRelationshipType(oPatient.PatientGuarantors[0].Relation.ToString());
                        }

                        // string LpatID = oPatient.DemographicsDetail.PatientCode.ToString();
                        //string reference_val = objgloPatientDB.ReferenceValue(LpatID, "person");

                        if ((ConfirmNull(clsEmdeonGeneral.gloLab_personRefval.ToString()) == true) && (ConfirmNull(_Grelationtype.ToString())))
                        {

                            //gloLab_emp.
                            writer.WriteStartElement("OBJECT");
                            writer.WriteAttributeString("name", "guarantor");
                            writer.WriteAttributeString("op", "update");

                            //writer.WriteElementString("cob_priority",)
                            if ((ConfirmNull(oPatient.PatientGuarantors[0].AddressLine1.ToString())) == true)
                            {
                                writer.WriteElementString("address_1", oPatient.PatientGuarantors[0].AddressLine1.ToString());
                            }
                            if ((ConfirmNull(oPatient.PatientGuarantors[0].AddressLine2.ToString())) == true)
                            {
                                writer.WriteElementString("address_2", oPatient.PatientGuarantors[0].AddressLine2.ToString());
                            }
                            if ((ConfirmNull(oPatient.PatientGuarantors[0].Phone.ToString())) == true)
                            {
                                writer.WriteElementString("home_phone", oPatient.PatientGuarantors[0].Phone.ToString());
                            }

                            //    writer.WriteElementString("birth_date", oPatient.PatientGuarantors[0].DOB.ToString());
                            if ((ConfirmNull(oPatient.PatientGuarantors[0].City.ToString())) == true)
                            {
                                writer.WriteElementString("city", oPatient.PatientGuarantors[0].City.ToString());
                            }

                            writer.WriteElementString("guarantor_sex", _gender.ToString());
                            if ((ConfirmNull(oPatient.PatientGuarantors[0].Phone.ToString())) == true)
                            {
                                writer.WriteElementString("work_phone", oPatient.PatientGuarantors[0].Phone.ToString());
                            }

                            if ((ConfirmNull(oPatient.PatientGuarantors[0].LastName.ToString())) == true)
                            {
                                writer.WriteElementString("last_name", oPatient.PatientGuarantors[0].LastName.ToString());
                            }
                            if ((ConfirmNull(oPatient.PatientGuarantors[0].MiddleName.ToString())) == true)
                            {
                                writer.WriteElementString("middle_name", oPatient.PatientGuarantors[0].MiddleName.ToString());
                            }
                            writer.WriteElementString("relationship", _Grelationtype.ToString());
                            if ((ConfirmNull(oPatient.PatientGuarantors[0].SSN.ToString())) == true)
                            {
                                writer.WriteElementString("ssn", oPatient.PatientGuarantors[0].SSN.ToString());
                            }
                            if ((ConfirmNull(oPatient.PatientGuarantors[0].State.ToString())) == true)
                            {
                                writer.WriteElementString("state", oPatient.PatientGuarantors[0].State.ToString());
                            }
                            if ((ConfirmNull(oPatient.PatientGuarantors[0].Zip.ToString())) == true)
                            {
                                if (oPatient.PatientGuarantors[0].Zip.Length > 4)
                                {
                                    writer.WriteElementString("zip", oPatient.PatientGuarantors[0].Zip.ToString());
                                }
                                else
                                {
                                    string _Pzip = oPatient.PatientGuarantors[0].Zip.ToString();
                                    _Pzip = string.Concat("0", _Pzip);
                                    writer.WriteElementString("zip", _Pzip.ToString());
                                }
                            }
                            if ((ConfirmNull(oPatient.PatientGuarantors[0].FirstName.ToString())) == true)
                            {
                                writer.WriteElementString("first_name", oPatient.PatientGuarantors[0].FirstName.ToString());
                            }
                            if ((ConfirmNull(oPatient.PatientGuarantors[0].DOB.ToString())) == true)
                            {
                                writer.WriteElementString("birth_date", oPatient.PatientGuarantors[0].DOB.ToString());
                            }
                            writer.WriteElementString("person", clsEmdeonGeneral.gloLab_personRefval.ToString());
                            writer.WriteElementString("guarantor", clsEmdeonGeneral.gloLab_guarantorRefval.ToString());
                            writer.WriteEndElement();
                            clsEmdeonGeneral.sgloLabtype = "Guarantor";
                            //clsEmdeonGeneral.gloLab_patID = Convert.ToInt64(oPatient.DemographicsDetail.PatientID.ToString());
                            clsEmdeonGeneral.gloLab_patcode = oPatient.DemographicsDetail.PatientCode.ToString();
                            clsEmdeonGeneral.sgloLab_Value = clsEmdeonGeneral.gloLab_guarantorRefval.ToString();
                        }
                        break;
                    case "insurance_update":

                       // validate = "Insurance_add";
                        oPatient = (gloPatient.Patient)xmlContent;
                        //_validateNul = objgloPatientDB.ValidatePatient(validate, oPatient);
                        //if (_validateNul == true)
                        //{

                        _cobtype = oPatient.InsuranceDetails.InsurancesDetails[0].InsuranceFlag;
                        //cobType--Eg:primary,scondary,teritary

                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].RelationshipName.ToString()))
                        {

                            _relationtype = objgloPatientDB.GetInsuranceRelationshipType(oPatient.InsuranceDetails.InsurancesDetails[0].RelationshipName.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.PatientEmploymentStatus.ToString()))
                        {
                            _empStatus = objgloPatientDB.GetEmployeeStatus(oPatient.OccupationDetail.PatientEmploymentStatus.ToString());
                        }

                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "insurance");
                        writer.WriteAttributeString("op", "update");

                        writer.WriteElementString("cob_priority", _cobtype.ToString());

                        writer.WriteElementString("expiration_date", oPatient.InsuranceDetails.InsurancesDetails[0].EndDate.ToString());

                        writer.WriteElementString("effective_date", oPatient.InsuranceDetails.InsurancesDetails[0].StartDate.ToString());


                        //writer.WriteElementString("expiration_date", oPatient.InsuranceDetails.InsurancesDetails[0].EndDate.ToString());
                        //writer.WriteElementString("group_name", oPatient.InsuranceDetails.InsurancesDetails[oPatient].SubscriberPolicy);
                        //if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Group.ToString()))
                        //{
                        //writer.WriteElementString("plan_identifier", oPatient.InsuranceDetails.InsurancesDetails[0].InsuranceID.ToString());
                        //}
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Group.ToString()))
                        {
                            writer.WriteElementString("group_number", oPatient.InsuranceDetails.InsurancesDetails[0].Group.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberAddr1.ToString()))
                        {
                            writer.WriteElementString("insured_address_1", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberAddr1.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberAddr2.ToString()))
                        {
                            writer.WriteElementString("insured_address_2", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberAddr2.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].DOB.ToString()))
                        {
                            writer.WriteElementString("insured_birth_date", oPatient.InsuranceDetails.InsurancesDetails[0].DOB.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberCity.ToString()))
                        {
                            writer.WriteElementString("insured_city", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberCity.ToString());
                        }
                        //if (ConfirmNull(oPatient)))
                        //{
                        //    writer.WriteElementString("insured_employee_id", oPatient.OccupationDetail.PatientWorkAddress1.ToString());
                        //}
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Phone.ToString()))
                        {
                            //Splitting... phoneNo and area code..
                            string _phNo = oPatient.InsuranceDetails.InsurancesDetails[0].Phone.ToString();
                            _phAreaCode = _phNo.Substring(0, 3);
                            _phoneNo = _phNo.Substring(3);
                            writer.WriteElementString("insured_home_phone_number", _phoneNo.ToString());
                            writer.WriteElementString("insured_home_phone_area_code", _phAreaCode.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.PatientWorkPhone.ToString()))
                        {
                            _phAreaCode = string.Empty;
                            _phoneNo = string.Empty;
                            string _phNo = oPatient.OccupationDetail.PatientWorkPhone.ToString();
                            _phAreaCode = _phNo.Substring(0, 3);
                            _phoneNo = _phNo.Substring(3);
                            writer.WriteElementString("insured_work_phone_area_code", _phAreaCode.ToString());
                            writer.WriteElementString("insured_work_phone_number", _phoneNo.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.PatientWorkAddress1.ToString()))
                        {
                            writer.WriteElementString("insured_empl_address_1", oPatient.OccupationDetail.PatientWorkAddress1.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.PatientWorkAddress2.ToString()))
                        {
                            writer.WriteElementString("insured_empl_address_2", oPatient.OccupationDetail.PatientWorkAddress2.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.PatientWorkCity.ToString()))
                        {
                            writer.WriteElementString("insured_empl_city", oPatient.OccupationDetail.PatientWorkCity.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.EmployerName.ToString()))
                        {
                            writer.WriteElementString("insured_empl_name", oPatient.OccupationDetail.EmployerName.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.PatientWorkState.ToString()))
                        {
                            writer.WriteElementString("insured_empl_state", oPatient.OccupationDetail.PatientWorkState.ToString());
                        }
                        if (ConfirmNull(oPatient.OccupationDetail.PatientWorkZip.ToString()))
                        {
                            writer.WriteElementString("insured_empl_zip", oPatient.OccupationDetail.PatientWorkZip.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberFName.ToString()))
                        {
                            writer.WriteElementString("insured_first_name", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberFName.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberLName.ToString()))
                        {
                            writer.WriteElementString("insured_last_name", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberLName.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberMName.ToString()))
                        {
                            writer.WriteElementString("insured_middle_name", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberMName.ToString());
                        }
                        //writer.WriteElementString("insured_name_suffix", oPatient.Insurances[0].SubscriberMName.ToString());
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberGender.ToString()))
                        {
                            writer.WriteElementString("insured_sex", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberGender.ToString());
                        }
                        //writer.WriteElementString("insured_ssn", oPatient.Insurances[0].ToString());
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberState.ToString()))
                        {
                            writer.WriteElementString("insured_state", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberState.ToString());
                        }
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberZip.ToString()))
                        {
                            if (oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberZip.Length > 4)
                            {
                                writer.WriteElementString("insured_zip", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberZip.ToString());
                            }
                            else
                            {
                                string _Pzip = oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberZip.ToString();
                                _Pzip = string.Concat("0", _Pzip);
                                writer.WriteElementString("insured_zip", _Pzip.ToString());
                            }
                            //writer.WriteElementString("insured_zip", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberZip.ToString());
                        }
                        //if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberPolicy.ToString()))
                        //{
                        //writer.WriteElementString("policy_number", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberPolicy.ToString());
                        writer.WriteElementString("policy_number", oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberID.ToString());
                        //}
                        if (ConfirmNull(_empStatus.ToString()))
                        {
                            writer.WriteElementString("insured_employment_status", _empStatus.ToString());
                        }
                        if (ConfirmNull(_relationtype.ToString()))
                        {
                            writer.WriteElementString("patient_rel_to_insured", _relationtype.ToString());
                        }

                        writer.WriteElementString("isp", clsEmdeonGeneral.gloLab_ispRefval.ToString());
                        writer.WriteElementString("organization", clsEmdeonGeneral.emdeonFacilityCode.ToString());
                        writer.WriteElementString("person", clsEmdeonGeneral.gloLab_personRefval.ToString());
                        writer.WriteElementString("insurance", clsEmdeonGeneral.gloLab_insuranceRefval.ToString());
                        writer.WriteEndElement();
                        clsEmdeonGeneral.sgloLabtype = "Insurance";
                        clsEmdeonGeneral.gloLab_patcode = oPatient.DemographicsDetail.PatientCode.ToString();
                        clsEmdeonGeneral.sgloLab_Value = clsEmdeonGeneral.gloLab_insuranceRefval.ToString();
                        break;
                    case "personhsi_add":
                        oPatient = (gloPatient.Patient)xmlContent;
                        if (ConfirmNull(clsEmdeonGeneral.gloLab_personRefval.ToString()) == true)
                        {
                            writer.WriteStartElement("OBJECT");
                            writer.WriteAttributeString("name", "personhsi");
                            writer.WriteAttributeString("op", "put");
                            writer.WriteElementString("person", clsEmdeonGeneral.gloLab_personRefval.ToString());
                            writer.WriteElementString("hsi_value", clsEmdeonGeneral.gloLab_patcode.ToString());
                            writer.WriteElementString("hsilabel", clsEmdeonGeneral.gloLab_hsilabel.ToString());
                            //writer.WriteElementString("hsilabel", clsEmdeonGeneral.gloLab_hsilabel.ToString());
                            writer.WriteEndElement();
                            clsEmdeonGeneral.sgloLabtype = "personhsi";
                            //clsEmdeonGeneral.gloLab_patID = Convert.ToInt64(oPatient.DemographicsDetail.PatientID.ToString());
                            clsEmdeonGeneral.gloLab_patcode = oPatient.DemographicsDetail.PatientCode.ToString();
                            //clsEmdeonGeneral.sgloLab_Value = clsEmdeonGeneral.gloLab_providerRefval.ToString();
                        }
                        break;
                    case "personhsi_update":
                        oPatient = (gloPatient.Patient)xmlContent;
                        if (ConfirmNull(clsEmdeonGeneral.gloLab_personRefval.ToString()) == true)
                        {
                            writer.WriteStartElement("OBJECT");
                            writer.WriteAttributeString("name", "personhsi");
                            writer.WriteAttributeString("op", "update");
                            writer.WriteElementString("person", clsEmdeonGeneral.gloLab_personRefval.ToString());
                            writer.WriteElementString("hsi_value", clsEmdeonGeneral.gloLab_patcode.ToString());
                            writer.WriteElementString("hsilabel", clsEmdeonGeneral.gloLab_hsilabel.ToString());
                            writer.WriteElementString("personhsi", clsEmdeonGeneral.gloLab_personhsiRefval.ToString());
                            writer.WriteEndElement();
                            clsEmdeonGeneral.sgloLabtype = "personhsi";
                            //clsEmdeonGeneral.gloLab_patID = Convert.ToInt64(oPatient.DemographicsDetail.PatientID.ToString());
                            clsEmdeonGeneral.gloLab_patcode = oPatient.DemographicsDetail.PatientCode.ToString();
                            clsEmdeonGeneral.sgloLab_Value = clsEmdeonGeneral.gloLab_personhsiRefval.ToString();
                            //clsEmdeonGeneral.sgloLab_Value = clsEmdeonGeneral.gloLab_providerRefval.ToString();
                        }
                        break;
                    case "personhsi_search":

                        writer.WriteStartElement("OBJECT");
                        writer.WriteAttributeString("name", "personhsi");
                        writer.WriteAttributeString("op", "search_hsi");
                        writer.WriteElementString("hsi_value", clsEmdeonGeneral.gloLab_patcode.ToString());
                        writer.WriteElementString("hsilabel", clsEmdeonGeneral.gloLab_hsilabel.ToString());
                        writer.WriteElementString("organization", clsEmdeonGeneral.emdeonFacilityCode.ToString());
                        writer.WriteEndElement();
                        break;

                    default:
                        break;
                    //clsEmdeonGeneral.gloLab_patcode = Convert.ToInt64(oPatient.DemographicsDetail.PatientCode.ToString());
                }
                writer.WriteEndElement();
                //Request End 

                writer.WriteEndDocument();
                writer.Close();
                
                objgloPatientDB.Dispose();
                objgloPatientDB = null;
            }
            catch (XmlException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);     
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);     
            }

            return _outboxFIlename;
        }


        public string ProcessRequest(string strRequestType, object xmlContent)
        {
            int _reqCount = 0;
        Start:
            //Added by madan on date--20100225
            // declaring object of clsxmllayer class 
            clsXMLLayer objclsXmlLayer = new clsXMLLayer();
            clsgloLabPatientLayer clsPatientLayer = new clsgloLabPatientLayer();
            string strFileName = ""; ;
            string _requestFile = "";


            _requestFile = GenerateXMLFIlE(strRequestType, xmlContent);

            //Addded by madan on 20100522
            _requestFile = ReplaceSpecialCharater(_requestFile);

            string _strErrorTyp = "";
            //I've request file redy with me Outbox - Now sending 
            try
            {

                if (!string.IsNullOrEmpty(_requestFile))
                {
                    if (File.Exists(_requestFile))
                    {
                        try
                        {
                            //Added by madan on date--20100225
                            // accessing get response method of clsxmllayer class with object
                            //strFileName =clsXMLLayer.GetResponse(_requestFile);
                            strFileName = objclsXmlLayer.GetResponse(_requestFile);
                            _strErrorTyp = CheckXML(strFileName);
                            switch (_strErrorTyp.ToLower())
                            {
                                case "session":
                                    clsEmdeonGeneral.gloLab_SessionId = clsPatientLayer.GetSessionId(strFileName);
                                    return strFileName;
                                case "login_error":
                                    if (_reqCount == 0)
                                    {
                                        clsEmdeonGeneral.gloLab_SessionId = "";
                                        _reqCount += 1;
                                        goto Start;
                                    }
                                    break;
                                default:
                                    clsEmdeonGeneral.gloLab_SessionId = "";
                                    return "";
                            }
                            return "";
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            return "";
                        }
                        finally
                        {
                            if (File.Exists(_requestFile))
                            {
                                File.Delete(_requestFile);
                            }
                        }

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
            catch
            {
                return "";
            }
            finally
            {
                objclsXmlLayer = null;
                
                clsPatientLayer.Dispose();
                clsPatientLayer=null;
            }

        }
        protected bool ConfirmNull(string strValue)
        {
            bool blnCheck = false;
            try
            {
                if (strValue != null && strValue.ToString().Trim().Length != 0 && strValue.ToString() != "")
                {
                    blnCheck = true;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);    
            }
            return blnCheck;
        }
        // Updateing orderstatus.
        public bool OrderStatusUpdate(string strOrder,clsGeneral.OrderStatus eOrderStatus)
        {
            bool _blnOrderStatus = false;
            string strOrderStatus = "";
            string strFileName = "";
            try
            {
                //strOrderStatus = GetOrderSatusInfo(_enmOrderStatus);
                clsGeneral objclsGeneral = new clsGeneral(); //Update Log...
                strOrderStatus = objclsGeneral.GetOrderStatus(eOrderStatus.GetHashCode(), 1);
                objclsGeneral.Dispose();
                objclsGeneral = null;

                if (ConfirmNull(strOrder.ToString()) && ConfirmNull(strOrderStatus.ToString()))
                {
                    _strOrderRef = strOrder;
                    _strOrderStatus = strOrderStatus;
                    gloPatient.Patient ObjPatient = new gloPatient.Patient();
                    strFileName = ProcessRequest("order_update", ObjPatient);
                    ObjPatient.Dispose();
                    ObjPatient = null;
                    if (ConfirmNull(strFileName.ToString()))
                    {
                        StreamReader testTxt = new StreamReader(strFileName);
                        string allRead = testTxt.ReadToEnd();
                        testTxt.Close();
                        testTxt.Dispose();

                        string regMatch = "sessionid";
                        if (Regex.IsMatch(allRead, regMatch))
                        {
                            _blnOrderStatus = true;
                        }
                        else
                        {
                            _blnOrderStatus = false;
                        }
                    }
                    else
                    {
                        _blnOrderStatus = false;
                    }
                }
                else
                {
                    _blnOrderStatus = false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);  
            }
            return _blnOrderStatus;
        }
        //added by madan... on 20100414..
        public string CheckXML(string strFileName)
        {
            string _retErrorType = "";
           // string _strErrorType=""; -- commented to solve the warnings.. by madan on 20100520
            try
            {
                if (ConfirmNull(strFileName.ToString()))
                {
                    StreamReader testTxt = new StreamReader(strFileName);
                    string allRead = testTxt.ReadToEnd();
                    testTxt.Close();
                    testTxt.Dispose();
                    string regMatch = "ERROR";//string to search in XML file. It is case sensitive.
                    if (Regex.IsMatch(allRead, "sessionid"))//If the match is found in allRead
                    {
                        return "session";
                    }
                    else if (Regex.IsMatch(allRead, regMatch))
                    {

                        _retErrorType = "login_error";
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
            return _retErrorType;
        }
        #region GetStatus
        //protected string GetOrderSatusInfo(clsGeneral.OrderStatus _orderStatusinfo)
        //{
        //    string _rtnOrderStatus = "";
        //    try
        //    {
        //        switch (_orderStatusinfo)
        //        {
        //            case clsGeneral.OrderStatus.CancelledbyClient:
        //                _rtnOrderStatus = "XC";
        //                break;
        //            case clsGeneral.OrderStatus.CancelledbyLab:
        //                _rtnOrderStatus = "XL";
        //                break;
        //            case clsGeneral.OrderStatus.Corrected:
        //                _rtnOrderStatus = "C";
        //                break;
        //            case clsGeneral.OrderStatus.Draft:
        //                _rtnOrderStatus = "D";
        //                break;
        //            case clsGeneral.OrderStatus.Entered:
        //                _rtnOrderStatus = "E";
        //                break;
        //            case clsGeneral.OrderStatus.Error:
        //                _rtnOrderStatus = "X";
        //                break;
        //            case clsGeneral.OrderStatus.FinalReported:
        //                _rtnOrderStatus = "F";
        //                break;
        //            case clsGeneral.OrderStatus.Inactive:
        //                _rtnOrderStatus = "I";
        //                break;
        //            case clsGeneral.OrderStatus.PartialReported:
        //                _rtnOrderStatus = "P";
        //                break;
        //            case clsGeneral.OrderStatus.ReadyToTransmit:
        //                _rtnOrderStatus = "R";
        //                break;
        //            case clsGeneral.OrderStatus.ResultsReceived:
        //                _rtnOrderStatus = "NA";
        //                break;
        //            case clsGeneral.OrderStatus.TransmissionError:
        //                _rtnOrderStatus = "TX";
        //                break;
        //            case clsGeneral.OrderStatus.Transmitted:
        //                _rtnOrderStatus = "T";
        //                break;
        //            default:
        //                _rtnOrderStatus = "";
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        objclsGeneral.UpdateLog("Order status:" + ex.ToString());
        //    }
        //    return _rtnOrderStatus;
        //}
        #endregion GetStatus

        public string ReplaceSpecialCharater(string filePath)
        {
            // Function is added by madan on Date 20100522 . 
            // function used to handle the Special characters in XMl file while sending request to EMDEON
            // it handle the "# "
            string strfilename = filePath;
            string _finalString = string.Empty;
            StreamReader objstrmReader = new StreamReader(strfilename);
            //filePath = strOutboxFilePath + "\\" + (System.DateTime.Now.ToString("yyyyMMddhhmmssmmm").Trim() + System.Guid.NewGuid().ToString() + ".xml");
            //while (File.Exists(filePath))
            //{
            //    filePath = strOutboxFilePath + "\\" + (System.DateTime.Now.ToString("yyyyMMddhhmmssmmm").Trim() + System.Guid.NewGuid().ToString() + ".xml");
            //}
            filePath = gloGlobal.clsFileExtensions.NewDocumentName(strOutboxFilePath, ".xml", "yyyyMMddhhmmssmmm");
            FileStream objstrmWriter = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            StreamWriter objWriter = new StreamWriter(objstrmWriter);
            try
            {
                _finalString = objstrmReader.ReadToEnd();
                _finalString = _finalString.Replace("#", "%23");
                _finalString = _finalString.Replace("&amp;", "%26amp;").Replace("&apos;", "%27").Replace("'", "%27");
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
        }


        //Added by madan on 20100612
        /// <summary>
        /// Retrieve the status of orders from emdeon
        /// </summary>
        /// <param name="OrderReferanceId"></param>
        /// <returns></returns>
        public string GetEmdeonOrderStatus(string OrderReferanceId)
        {
            string _strOrderStatus = string.Empty;
            string _strFileName = string.Empty;
            string _strTempOrderReferance = string.Empty;
            DataSet dsReference = new DataSet();
            try
            {
                if (ConfirmNull(OrderReferanceId.ToString()))
                {
                    _orderRefreanceID = OrderReferanceId;
                    _strFileName = ProcessRequest("retrieve_selected_order", null);
                    if (ConfirmNull(_strFileName.ToString()))
                    {
                        dsReference.ReadXml(_strFileName);
                        if (dsReference.Tables["OBJECT"] != null)
                        {
                            if (dsReference.Tables[1].Rows.Count > 0)
                            {
                                //_strTempOrderReferance = dsReference.Tables[1].Rows[0]["order"].ToString();

                                _strOrderStatus = Convert.ToString(dsReference.Tables[1].Rows[0]["order_status"]);

                                //if ((string.Compare(_strTempOrderReferance, Convert.ToDouble(OrderReferanceId).ToString())) == 0)
                                //{
                                //    _strOrderStatus = dsReference.Tables[1].Rows[0]["order_status"].ToString();
                                //}
                                //else
                                //{
                                //    _strOrderStatus = string.Empty;
                                //}
                            }
                            else
                            {
                                _strOrderStatus = string.Empty;
                            }
                        }
                        else
                        {
                            if (dsReference.Tables["ERROR"] != null)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog("Received XML response file contains Error.", false);
                            }
                            _strOrderStatus = string.Empty;
                        }

                    }
                    else
                        return "";
                }
            }
            catch (Exception ex)
            {
                _strOrderStatus = string.Empty;       
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                
            }
            finally
            {
                _strFileName = string.Empty;
                _strTempOrderReferance = string.Empty;

                if (dsReference != null)
                {
                    dsReference.Dispose();
                }
            }
            return _strOrderStatus;
        }


    }

}
