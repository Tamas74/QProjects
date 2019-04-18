using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;
using gloVault.gloVaultSniffer;

namespace gloVault.Classes
{
    public class clsBussinessLayer
    {
        #region Variables

        public static bool IsTroubleShoot = false;

        string _sConnectionString = string.Empty;

        string _strInboxFilePath;

        string _strOutboxFilePath;


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="sConnectionString"></param>
        public clsBussinessLayer(string sConnectionString)
        {
            _sConnectionString = sConnectionString;

            _strOutboxFilePath = System.Windows.Forms.Application.StartupPath + "\\gloVault\\Outbox";
            _strInboxFilePath = System.Windows.Forms.Application.StartupPath + "\\gloVault\\Inbox";
            try
            {
                if (System.IO.Directory.Exists(_strOutboxFilePath) == false)
                {
                    System.IO.Directory.CreateDirectory(_strOutboxFilePath);
                }

                if (System.IO.Directory.Exists(_strInboxFilePath) == false)
                {
                    System.IO.Directory.CreateDirectory(_strInboxFilePath);
                }
            }
            catch (Exception ex)
            {
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
        }
        #endregion

        #region UserDefined methods

        /// <summary>
        /// Method to add and test webservice connectivity.
        /// </summary>
        /// <param name="sWebServiceUrl"></param>
        /// <returns></returns>
        public bool TestConnection(string sWebServiceUrl)
        {
            bool _blnResult = false;

            try
            {
                Properties.Settings.Default.gloVault_gloVaultSniffer_gloVaultSniffer = sWebServiceUrl;

                gloVaultSniffer.gloVaultSniffer objSniffer = new gloVault.gloVaultSniffer.gloVaultSniffer();

                string sValue = string.Empty;

                sValue = objSniffer.HelloWorld();

                if (sValue == "gloStream Service")
                {
                    _blnResult = true;
                }
                else
                    _blnResult = false;

                objSniffer = null;
            }
            catch (Exception ex)
            {
                _blnResult = false;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            return _blnResult;
        }

        /// <summary>
        /// Method to retive patients details and genrate XMl file
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <returns></returns>
        public bool ProcessPatientsInfo(Int64 nPatientId, List<Int64> lsConfigIdS, string sKey, Int64 nUserId, string sMachineName)
        {
            clsVaultDbLayer objVaultDblayer = new clsVaultDbLayer(_sConnectionString);
            ClsPatientInfoCollection objPatientCollection = new ClsPatientInfoCollection();
            gloVault.gloVaultSniffer.gloVaultSniffer objVaultSniffer = new gloVault.gloVaultSniffer.gloVaultSniffer();

            string sPatientInfoXmlPath = string.Empty;
            bool blnResult = false;
            List<ClsgloVaultConfigFields> lsConfigurations = new List<ClsgloVaultConfigFields>();

            try
            {
                sPatientInfoXmlPath = clsGeneralInterface.GetFileName(_strOutboxFilePath + "\\");

                ///Get gloVaultConfigurations.
                lsConfigurations = objVaultDblayer.getGloVaultConfigurations();

                if (lsConfigurations.Count <= 0)
                {
                    clsGeneralInterface.UpdateLog("gloVault configurations not found in the sysetm.");
                    return false;
                }

                ///Process as per configurations
                for (int index = 0; index < lsConfigIdS.Count; index++)
                {
                    string sConfigName = string.Empty;

                    sConfigName = GetConfigNameForConfigId(lsConfigurations, lsConfigIdS[index]);

                    if (sConfigName.Length <= 0)
                    {
                        clsGeneralInterface.UpdateLog("Specified gloVault configuration not found in the sysetm.");
                        continue;
                    }

                    switch (sConfigName.ToLower())
                    {
                        //DEMOGRAPHICS
                        case "demographics":
                            objPatientCollection.DtPatientInfo = objVaultDblayer.GetPatientInfo(nPatientId);
                            break;
                        //ALLERGIES
                        case "allergies":
                            objPatientCollection.DtAllergies = GetLatestInformation(nPatientId, objVaultDblayer.GetAllAlergies(nPatientId, 0), "allergies", lsConfigIdS[index].ToString());
                            break;

                        //MEDICATIONS
                        case "medications":
                            objPatientCollection.DtMedications = GetLatestInformation(nPatientId, objVaultDblayer.GetLatestMedications(nPatientId, 0), "medications", lsConfigIdS[index].ToString());
                            break;

                        //LABRESULTS
                        case "labresults":
                            objPatientCollection.DtTestResults = GetLatestInformation(nPatientId, objVaultDblayer.GetLatestLabResults(nPatientId), "labresults", lsConfigIdS[index].ToString());
                            break;

                        //Problemlist
                        case "problems":
                            objPatientCollection.DtProblemList = GetLatestInformation(nPatientId, objVaultDblayer.GetProblemList(nPatientId, 0), "problems", lsConfigIdS[index].ToString());
                            break;
                        default:
                            break;
                    }
                    sConfigName = string.Empty;
                }

                //Generate XMl file
                if (GenerateCompleteXMl(objPatientCollection, sPatientInfoXmlPath))
                {
                    //Save last access time.. if time exceeds 1/2 hour then create new key. and save the last time.
                    byte[] myBytes = clsGeneralInterface.ConvertFiletoBinary(sPatientInfoXmlPath);

                    string sValue = string.Empty;

                    if (objVaultSniffer.ProcessElementRequest(myBytes, sKey, out sValue))
                    {
                        ///Save the Last Accesstime for further retrivel.
                        //objVaultDblayer.SaveLastAccessTime(nPatientId, DateTime.Now);
                        objVaultDblayer.SaveLastAccessedModules(nPatientId, lsConfigIdS);

                        if (sValue.Length > 0)
                        {
                            objVaultDblayer.InsertAuditLog(nPatientId, sValue, true, nUserId, sMachineName, "information");

                            if (IsTroubleShoot)
                            {
                                clsGeneralInterface.UpdateLog("Process Results:" + sValue);
                            }
                        }
                        blnResult = true;
                    }
                    else
                    {
                        objVaultDblayer.InsertAuditLog(nPatientId, "Data Exchange failed:" + sValue, false, nUserId, sMachineName, "information");
                        blnResult = false;
                    }

                }
                else
                    blnResult = false;
            }
            catch (Exception ex)
            {
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (IsTroubleShoot == false)
                {
                    if (sPatientInfoXmlPath.Length > 0)
                    {
                        if (File.Exists(sPatientInfoXmlPath))
                        {
                            File.Delete(sPatientInfoXmlPath);
                        }
                    }
                }
                if (objVaultDblayer != null)
                {
                    objVaultDblayer.Dispose();
                }
                objPatientCollection = null;
            }

            return blnResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lsFields"></param>
        /// <param name="nConfigId"></param>
        /// <returns></returns>
        private string GetConfigNameForConfigId(List<ClsgloVaultConfigFields> lsFields, Int64 nConfigId)
        {
            ClsgloVaultConfigFields objVaultFields = null;
            string sResult = string.Empty;

            try
            {
                for (int i = 0; i < lsFields.Count; i++)
                {
                    //objVaultFields = new ClsgloVaultConfigFields();

                    objVaultFields = lsFields[i];

                    if (objVaultFields.nConfigId == nConfigId)
                    {
                        sResult = objVaultFields.sConfigName;
                        return sResult;
                    }
                    else
                        continue;

                  //  objVaultFields = null;
                }

            }
            catch (Exception ex)
            {
                sResult = string.Empty;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            return sResult;
        }

        /// <summary>
        /// Method to generate xml file.
        /// </summary>
        /// <param name="objPatientCollection"></param>
        /// <param name="sFilePath"></param>
        /// <returns></returns>
        private bool GenerateCompleteXMl(ClsPatientInfoCollection objPatientCollection, string sFilePath)
        {
            bool _blnResult = false;
            XmlTextWriter xmlwriter = null;


            try
            {
                if (System.IO.File.Exists(sFilePath))
                {
                    System.IO.File.Delete(sFilePath);
                }

                xmlwriter = new XmlTextWriter(sFilePath, null);
                xmlwriter.Formatting = Formatting.Indented;
                xmlwriter.WriteStartDocument();

                xmlwriter.WriteStartElement("Request");

                ///InsertPatient information to XML
                if (objPatientCollection.DtPatientInfo != null && objPatientCollection.DtPatientInfo.Rows.Count > 0)
                {
                    xmlwriter.WriteStartElement("Patient");
                    for (Int32 i = 0; i <= objPatientCollection.DtPatientInfo.Rows.Count - 1; i++)
                    {
                        for (Int32 j = 0; j <= objPatientCollection.DtPatientInfo.Columns.Count - 1; j++)
                        {
                            xmlwriter.WriteElementString(objPatientCollection.DtPatientInfo.Columns[j].ColumnName.ToString(), objPatientCollection.DtPatientInfo.Rows[i][j].ToString());
                        }
                    }
                    xmlwriter.WriteEndElement();
                    _blnResult = true;
                    //End Patient
                }
                else
                {
                    return _blnResult;
                }

                //Allergies
                if (objPatientCollection.DtAllergies != null && objPatientCollection.DtAllergies.Rows.Count > 0)
                {
                    xmlwriter.WriteStartElement("Allergies");
                    for (Int32 i = 0; i <= objPatientCollection.DtAllergies.Rows.Count - 1; i++)
                    {
                        xmlwriter.WriteStartElement("AllergiesItem");
                        for (Int32 j = 0; j <= objPatientCollection.DtAllergies.Columns.Count - 1; j++)
                        {
                            xmlwriter.WriteElementString(objPatientCollection.DtAllergies.Columns[j].ColumnName.ToString(), objPatientCollection.DtAllergies.Rows[i][j].ToString());
                        }
                        xmlwriter.WriteEndElement();
                        //End Allergies
                    }
                    xmlwriter.WriteEndElement();
                    //End Allergies
                    _blnResult = true;
                }

                // DataRow[] dr = objPatientCollection.DtMedications.Select("MedicationDate <='" + DateTime.Now + "'");

                //Medications
                if (objPatientCollection.DtMedications != null && objPatientCollection.DtMedications.Rows.Count > 0)
                {
                    xmlwriter.WriteStartElement("Medication");
                    for (Int32 i = 0; i <= objPatientCollection.DtMedications.Rows.Count - 1; i++)
                    {
                        xmlwriter.WriteStartElement("MedicationItem");

                        for (Int32 j = 0; j <= objPatientCollection.DtMedications.Columns.Count - 1; j++)
                        {
                            xmlwriter.WriteElementString(objPatientCollection.DtMedications.Columns[j].ColumnName.ToString(), objPatientCollection.DtMedications.Rows[i][j].ToString());
                        }
                        xmlwriter.WriteEndElement();
                        //End Medication
                    }
                    xmlwriter.WriteEndElement();
                    _blnResult = true;
                }

                //ProblemList
                if (objPatientCollection.DtProblemList != null && objPatientCollection.DtProblemList.Rows.Count > 0)
                {
                    xmlwriter.WriteStartElement("ProblemList");
                    for (Int32 i = 0; i <= objPatientCollection.DtProblemList.Rows.Count - 1; i++)
                    {
                        xmlwriter.WriteStartElement("ProblemListItem");

                        for (Int32 j = 0; j <= objPatientCollection.DtProblemList.Columns.Count - 1; j++)
                        {
                            xmlwriter.WriteElementString(objPatientCollection.DtProblemList.Columns[j].ColumnName.ToString(), objPatientCollection.DtProblemList.Rows[i][j].ToString());
                        }
                        xmlwriter.WriteEndElement();
                        //End Medication
                    }
                    xmlwriter.WriteEndElement();
                    _blnResult = true;
                }

                //Medications
                if (objPatientCollection.DtTestResults != null && objPatientCollection.DtTestResults.Rows.Count > 0)
                {
                    xmlwriter.WriteStartElement("LabResults");
                    for (Int32 i = 0; i <= objPatientCollection.DtTestResults.Rows.Count - 1; i++)
                    {
                        xmlwriter.WriteStartElement("ResultItem");
                        xmlwriter.WriteElementString("LabTestName", objPatientCollection.DtTestResults.Rows[i]["LabTestName"].ToString());
                        xmlwriter.WriteElementString("LaboratryName", objPatientCollection.DtTestResults.Rows[i]["LabName"].ToString());
                        xmlwriter.WriteElementString("ProviderName", objPatientCollection.DtTestResults.Rows[i]["ProviderName"].ToString());
                        xmlwriter.WriteElementString("LabTestDate", objPatientCollection.DtTestResults.Rows[i]["LabTestDate"].ToString());
                        xmlwriter.WriteElementString("ResultName", objPatientCollection.DtTestResults.Rows[i]["LabTestResultName"].ToString());
                        xmlwriter.WriteElementString("ResultValue", objPatientCollection.DtTestResults.Rows[i]["LabTestResultValue"].ToString());
                        xmlwriter.WriteElementString("ResultUnit", objPatientCollection.DtTestResults.Rows[i]["LabTestResultUnit"].ToString());
                        xmlwriter.WriteElementString("ResultRange", objPatientCollection.DtTestResults.Rows[i]["LabTestResultRange"].ToString());
                        xmlwriter.WriteElementString("ResultType", objPatientCollection.DtTestResults.Rows[i]["LabTestResultType"].ToString());
                        xmlwriter.WriteElementString("ResultAbnormalFlag", objPatientCollection.DtTestResults.Rows[i]["LabTestResultAbnormalFlag"].ToString());
                        xmlwriter.WriteElementString("ResultComment", objPatientCollection.DtTestResults.Rows[i]["LabTestResultComment"].ToString());
                        xmlwriter.WriteElementString("ResultDateTime", objPatientCollection.DtTestResults.Rows[i]["LabTestResultDateTime"].ToString());
                        xmlwriter.WriteEndElement();
                        //End TestResults
                    }
                    xmlwriter.WriteEndElement();
                    _blnResult = true;
                }

                xmlwriter.WriteEndElement();
                //End Request

                xmlwriter.WriteEndDocument();
                xmlwriter.Close();

            }
            catch (Exception ex)
            {
                _blnResult = false;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                xmlwriter = null;
            }

            return _blnResult;

        }

        private bool GenerateEmailXML(Int64 nPatientId, string sEmailxmlPath)
        {
            clsVaultDbLayer objDbvaultLayer = new clsVaultDbLayer(_sConnectionString);
            XmlTextWriter xmlwriter = null;

            DataTable dtPatientInfo = new DataTable();
            bool blnResult = false;

            try
            {
                //retrive patient information with clinic name and ausid.
                dtPatientInfo = objDbvaultLayer.RetrivePatientInfo(nPatientId);

                if (dtPatientInfo == null || dtPatientInfo.Rows.Count <= 0)
                {
                    clsGeneralInterface.UpdateLog("Retriveing patient from database failed");
                    return false;
                }

                if (System.IO.File.Exists(sEmailxmlPath))
                {
                    System.IO.File.Delete(sEmailxmlPath);
                }

                xmlwriter = new XmlTextWriter(sEmailxmlPath, null);
                xmlwriter.Formatting = Formatting.Indented;
                xmlwriter.WriteStartDocument();

                xmlwriter.WriteStartElement("Request");
                for (Int32 i = 0; i <= dtPatientInfo.Rows.Count - 1; i++)
                {
                    xmlwriter.WriteStartElement("Patient");
                    for (Int32 j = 0; j <= dtPatientInfo.Columns.Count - 1; j++)
                    {
                        if (dtPatientInfo.Columns[j].ColumnName.ToString() == "logoimage")
                        {
                            if (dtPatientInfo.Rows[i][j] == null || dtPatientInfo.Rows[i][j].ToString() == "")
                            {
                                continue;
                            }
                            //DataRow dr = dtPatientInfo.Rows[i];
                            byte[] arr = (byte[])(dtPatientInfo.Rows[i][j]);
                            //string test = System.Text.Encoding.Default.GetString(arr);
                            string test = Convert.ToBase64String(arr);

                            if (test.Length > 0)
                            {
                                xmlwriter.WriteStartElement("LogoImage");
                                xmlwriter.WriteCData(test);
                                xmlwriter.WriteEndElement();
                            }
                            else
                            {
                                xmlwriter.WriteElementString("LogoImage", "");
                            }

                            //byte[] ass = Convert.FromBase64String(test);
                            //Image img = byteArrayToImage(ass);
                            //img.Save("c:\\Maa.JPG");

                        }
                        else
                        {
                            xmlwriter.WriteElementString(dtPatientInfo.Columns[j].ColumnName.ToString(), dtPatientInfo.Rows[i][j].ToString());
                        }

                    }
                    xmlwriter.WriteEndElement();
                    //End TestResults
                }
                xmlwriter.WriteEndElement();
                //End Request

                xmlwriter.WriteEndDocument();
                xmlwriter.Close();

                blnResult = true;

            }
            catch (Exception ex)
            {
                blnResult = false;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (dtPatientInfo != null)
                {
                    dtPatientInfo.Dispose();
                }
            }
            return blnResult;
        }

        /// <summary>
        /// Method to generate EmailRequest.
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <returns></returns>
        public bool GenerateEmailRequest(Int64 nPatientId, string sKey)
        {
            gloVaultSniffer.gloVaultSniffer objSniffer = new gloVault.gloVaultSniffer.gloVaultSniffer();
            clsVaultDbLayer objVaultDbLayer = new clsVaultDbLayer(_sConnectionString);

            bool blnResult = false;
            string sEmailXmlPath = string.Empty;
            string sOutPutResponse = string.Empty;
            try
            {
                sEmailXmlPath = clsGeneralInterface.GetFileName(_strOutboxFilePath + "\\");

                if (GenerateEmailXML(nPatientId, sEmailXmlPath))
                {

                    byte[] myBytes = clsGeneralInterface.ConvertFiletoBinary(sEmailXmlPath);

                    if (myBytes != null)
                    {
                        if (objSniffer.SendRequestAccess(myBytes, sKey, out sOutPutResponse))
                        {
                            string sEmail = string.Empty;

                            sEmail = objVaultDbLayer.GetPatientEmailId(nPatientId);

                            if (sEmail.Length > 0)
                            {
                                objVaultDbLayer.InsertEmailResponseInExternalTb(nPatientId, sEmail, sOutPutResponse);
                            }
                            sEmail = string.Empty;
                            blnResult = true;
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                blnResult = false;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (IsTroubleShoot == false)
                {
                    if (sEmailXmlPath.Length > 0)
                    {
                        if (File.Exists(sEmailXmlPath))
                        {
                            File.Delete(sEmailXmlPath);
                        }
                    }
                }

                if (objVaultDbLayer != null)
                {
                    objVaultDbLayer.Dispose();
                }
            }
            return blnResult;
        }

        /// <summary>
        /// Method to retive approved patients from gloVault webservice
        /// </summary>
        /// <returns></returns>
        public bool GetPatientApprovals(string sKey, string sAUSId)
        {
            gloVaultSniffer.gloVaultSniffer objSniffer = new gloVaultSniffer.gloVaultSniffer();
            string sXmlPath = string.Empty;
            DataSet dsResult = new DataSet();

            try
            {
                //Retrive pending approvals from webservice.
                byte[] myBytes = objSniffer.GetMyApprovals(sKey, sAUSId);

                if (myBytes != null)
                {
                    sXmlPath = clsGeneralInterface.GetFileName(_strInboxFilePath + "\\");

                    clsGeneralInterface.ConvertBinarytoFile(myBytes, sXmlPath);

                    clsGeneralInterface.UpdateLog("Retrived patient approvals");
                    dsResult.ReadXml(sXmlPath);
                }

                //save the apprival status and value in patient externalcodes table
                if (dsResult != null && dsResult.Tables.Count > 0)
                {
                    ProcessPatientIds(dsResult);
                }

            }
            catch (Exception ex)
            {
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (IsTroubleShoot == false)
                {
                    if (sXmlPath.Length > 0)
                    {
                        if (File.Exists(sXmlPath))
                        {
                            File.Delete(sXmlPath);
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Method to process and save all the responses.
        /// </summary>
        /// <param name="dsPatientResp"></param>
        private void ProcessPatientIds(DataSet dsPatientResp)
        {
            clsencryption objEncyption = new clsencryption();
            clsVaultDbLayer objVaultDblayer = new clsVaultDbLayer(_sConnectionString);
            try
            {
                foreach (DataTable dtPatientResp in dsPatientResp.Tables)
                {
                    if (dtPatientResp.TableName == "Member")
                    {
                        for (int i = 0; i < dtPatientResp.Rows.Count; i++)
                        {
                            Int64 nPatientId = 0;
                            Int64 nStatus = 0;
                            string sPrId = string.Empty;
                            string sRcId = string.Empty;
                            string sEmail = string.Empty;

                            ///GetPatientID... If patient id not found.. then response should be discarded.
                            if (dtPatientResp.Rows[i]["MemeberId"] != null && dtPatientResp.Rows[i]["MemeberId"].ToString().Length > 0)
                            {
                                nPatientId = Convert.ToInt64(dtPatientResp.Rows[i]["MemeberId"]);
                            }
                            else
                                continue;

                            if (dtPatientResp.Rows[i]["Status"] != null && dtPatientResp.Rows[i]["Status"].ToString().Length > 0)
                            {
                                nStatus = Convert.ToInt64(dtPatientResp.Rows[i]["Status"]);
                            }

                            if (dtPatientResp.Rows[i] != null && dtPatientResp.Rows[i]["PRID"].ToString().Length > 0)
                            {
                                //decrypt.
                                sPrId = Convert.ToString(dtPatientResp.Rows[i]["PRID"]);
                            }

                            if (dtPatientResp.Rows[i]["RCID"] != null && dtPatientResp.Rows[i]["RCID"].ToString().Length > 0)
                            {
                                sRcId = Convert.ToString(dtPatientResp.Rows[i]["RCID"]);
                            }

                            if (dtPatientResp.Rows[i]["Email"] != null && dtPatientResp.Rows[i]["Email"].ToString().Length > 0)
                            {
                                sEmail = Convert.ToString(dtPatientResp.Rows[i]["Email"]);
                            }

                            ///Status types 1=Approved,2=Denied,3=Expiried

                            switch (nStatus)
                            {
                                case 1:
                                    if (objVaultDblayer.SavePatientHealthVaultId(nPatientId, sPrId, sRcId, nStatus, sEmail) == false)
                                    {
                                        clsGeneralInterface.UpdateLog("Failed to save the patient healthvault information in Db");
                                    }
                                    break;
                                case 2:
                                    sEmail = objVaultDblayer.GetPatientEmailId(nPatientId);
                                    if (objVaultDblayer.SavePatientHealthVaultId(nPatientId, "", "", nStatus, sEmail) == false)
                                    {
                                        clsGeneralInterface.UpdateLog("Failed to save the patient healthvault information in Db");
                                    }
                                    break;
                                case 3:
                                    sEmail = objVaultDblayer.GetPatientEmailId(nPatientId);
                                    if (objVaultDblayer.SavePatientHealthVaultId(nPatientId, "", "", nStatus, sEmail) == false)
                                    {
                                        clsGeneralInterface.UpdateLog("Failed to save the patient healthvault information in Db");
                                    }
                                    break;
                                default:
                                    break;
                            }

                            nPatientId = 0;
                            nStatus = 0;
                            sPrId = string.Empty;
                            sRcId = string.Empty;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (objVaultDblayer != null)
                {
                    objVaultDblayer.Dispose();
                }
            }
        }

        public bool SendCCDFile(Int64 nPatientId, string sFilePath, string sAccessKey, Int64 nUserId, string sMachineName)
        {
            clsVaultDbLayer objVaultDbLayer = new clsVaultDbLayer(_sConnectionString);
            gloVaultSniffer.gloVaultSniffer objSniffer = new gloVaultSniffer.gloVaultSniffer();
            string _sValue = string.Empty;
            bool _blnResult = false;
            string sNewFilePath = string.Empty;
            try
            {

                if (File.Exists(sFilePath) == false)
                {
                    return false;
                }

                _sValue = objVaultDbLayer.GetAccessId(nPatientId);

                if (_sValue.Length > 0)
                {
                    sNewFilePath = RemoveStyleSheetFromCCD(sFilePath);

                    if (sNewFilePath.Length <= 0)
                    {
                        return false;
                    }

                    //Retrive pending approvals from webservice.
                    byte[] myBytes = clsGeneralInterface.ConvertFiletoBinary(sNewFilePath);

                    if (myBytes != null)
                    {
                        string sErrormsg = string.Empty;
                        if (objSniffer.CProcessCD(myBytes, _sValue, sAccessKey, out sErrormsg))
                        {
                            //if (IsTroubleShoot)
                            //{
                            //    clsGeneralInterface.UpdateLog("Invalid CCD File : " + sErrormsg);
                            //}
                            _blnResult = true;
                        }
                        else
                        {
                            objVaultDbLayer.InsertAuditLog(nPatientId, sErrormsg.ToString(), true, nUserId, sMachineName, "CCD");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _blnResult = false;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (IsTroubleShoot == false)
                {
                    if (sNewFilePath.Length > 0)
                    {
                        File.Delete(sNewFilePath);
                    }
                }
            }
            return _blnResult;
        }

        /// <summary>
        /// Method to remove styleSheet
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        private string RemoveStyleSheetFromCCD(string strFilePath)
        {
            string sLine = null;
            string sMyValue = string.Empty;
            string myFile = string.Empty;

            try
            {
                myFile = clsGeneralInterface.GetFileName(_strOutboxFilePath + "\\");
                sMyValue = "<?xml-stylesheet type='text/xsl' href='http://www.glostream.com/css/XSLT/gloccdCss.xsl'?>";
                using (StreamReader strReader = new StreamReader(strFilePath))
                {
                    using (StreamWriter strWriter = new StreamWriter(myFile))
                    {
                        while ((sLine = strReader.ReadLine()) != null)
                        {
                            if (string.Compare(sMyValue, sLine) == 0)
                            {
                                continue;
                            }
                            strWriter.WriteLine(sLine);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                myFile = string.Empty;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                sLine = string.Empty;
                sMyValue = string.Empty;
            }
            return myFile;
        }

        /// <summary>
        /// Method to retrive access key.
        /// </summary>
        /// <param name="sAusId"></param>
        /// <returns></returns>
        public string GetAccessKey(string sAusId)
        {
            gloVaultSniffer.gloVaultSniffer objSniffer = new gloVault.gloVaultSniffer.gloVaultSniffer();
            string sKey = string.Empty;
            int nCount = 0;

            try
            {

            Label1:

                //Get Access key.--- Try for three times if fails send empty;
                if (objSniffer.GetAccessKey("43512D-PrGLO12112", sAusId, out sKey))
                {
                    if (isNumeric(sKey, System.Globalization.NumberStyles.Integer))
                    {
                        return sKey;
                    }
                    else
                    {
                        nCount += 1;

                        if (nCount <= 3)
                        {
                            goto Label1;
                        }
                        else
                        {
                            clsGeneralInterface.UpdateLog("Key generation Failed due to:" + sKey);
                            return "";
                        }
                    }
                }
                else
                {
                    nCount += 1;

                    if (nCount <= 3)
                    {
                        goto Label1;
                    }
                    else
                    {
                        clsGeneralInterface.UpdateLog("Key generation Failed due to:" + sKey);
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                sKey = string.Empty;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (objSniffer != null)
                {
                    objSniffer.Dispose();
                }
                nCount = 0;
            }
            return sKey;
        }

        /// <summary>
        /// Method to retrive latest results.
        /// </summary>
        /// <param name="nSelectedPatientId"></param>
        /// <param name="dtResults"></param>
        /// <param name="sType"></param>
        /// <param name="sModuleId"></param>
        /// <returns></returns>
        private DataTable GetLatestInformation(Int64 nSelectedPatientId, DataTable dtResults, string sType, string sModuleId)
        {
            clsVaultDbLayer objDBVaultLayer = new clsVaultDbLayer(_sConnectionString);
            DataTable dtResulted = new DataTable();
            DateTime dtLastAccessTime;
            string sFilterExprName = string.Empty;
            DataView dvFilter = null;
            try
            {
                if (dtResults == null && dtResults.Rows.Count <= 0)
                {
                    return null;
                }

                dtLastAccessTime = objDBVaultLayer.GetLastAccessTime(nSelectedPatientId, sModuleId);

                ///If last accesstime not found then retrun the same result.
                if (dtLastAccessTime == null || dtLastAccessTime.Year <= 1900)
                {
                    return dtResults;
                }

                //Removed medication module for latest comparison.
                if (sType == "medications")
                {
                    return dtResults;
                }

                if (sType == "allergies")
                {
                    return dtResults;
                }

                //Select the field value.
                switch (sType.ToLower())
                {
                    case "labresults":
                        sFilterExprName = "LabTestResultDateTime";
                        break;
                    case "allergies":
                        sFilterExprName = "HistoryDate";
                        break;
                    case "medications":
                        sFilterExprName = "MedicationDate";
                        break;
                    case "problems":
                        sFilterExprName = "ModifiedDate";
                        break;
                    default:
                        break;
                }

                if (sType.ToLower() == "problems")
                {
                    sFilterExprName = sFilterExprName + ">= '" + dtLastAccessTime.ToString("MM/dd/yyyy") + "'";
                }
                else
                {
                    //Set Filter criteria.
                    sFilterExprName = sFilterExprName + "> '" + dtLastAccessTime + "'";
                }

                //Apply filter.
                dvFilter = new DataView();
                dvFilter = dtResults.DefaultView;
                dvFilter.RowFilter = sFilterExprName;
                dtResulted = dvFilter.ToTable();

                #region Removed
                ////Set column name.
                //for (int i = 0; i < dtResults.Columns.Count; i++)
                //{
                //    dtResulted.Columns.Add(dtResults.Columns[i].ColumnName);
                //}

                ////Filter and add the row to datatable.
                //dRows = dtResults.Select(sFilterExprName);

                //foreach (DataRow dtRow in dRows)
                //{
                //    dtResulted.ImportRow(dtRow);
                //} 
                #endregion


            }
            catch (Exception ex)
            {
                dtResulted = null;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                sFilterExprName = string.Empty;

                if (objDBVaultLayer != null)
                {
                    objDBVaultLayer.Dispose();
                }
            }

            return dtResulted;
        }

        /// <summary>
        /// Method  to identify numeric value.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="NumberStyle"></param>
        /// <returns></returns>
        private bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }
        #endregion
    }

    /// <summary>
    /// Datatable collection
    /// </summary>
    class ClsPatientInfoCollection
    {
        #region Variables

        DataTable _dtTestResults = new DataTable();
        DataTable _dtAllergies = new DataTable();
        DataTable _dtPatientInfo = new DataTable();
        DataTable _dtMedications = new DataTable();
        DataTable _dtProblemList = new DataTable();

        #endregion

        #region Properties

        public DataTable DtMedications
        {
            get { return _dtMedications; }
            set { _dtMedications = value; }
        }

        public DataTable DtPatientInfo
        {
            get { return _dtPatientInfo; }
            set { _dtPatientInfo = value; }
        }

        public DataTable DtAllergies
        {
            get { return _dtAllergies; }
            set { _dtAllergies = value; }
        }

        public DataTable DtTestResults
        {
            get { return _dtTestResults; }
            set { _dtTestResults = value; }
        }

        public DataTable DtProblemList
        {
            get { return _dtProblemList; }
            set { _dtProblemList = value; }
        }


        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class ClsgloVaultConfigFields
    {
        #region Variables

        private Int64 _nConfigId = 0;

        private string _sConfigName = string.Empty;

        #endregion

        #region Properties

        public string sConfigName
        {
            get { return _sConfigName; }
            set { _sConfigName = value; }
        }

        public Int64 nConfigId
        {
            get { return _nConfigId; }
            set { _nConfigId = value; }
        }

        #endregion
    }
}
