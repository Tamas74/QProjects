using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
//using System.Text;
using System.IO;
using System.Xml;

namespace gloEDI
{
    public class ClsClaimSubMittal
    {

        const string URL = "http://glostream.yostengineering.com/submit.py";
        const string UNAME = "glostream_app";
        const string PWORD = "YlcU5Cap";

        private ClsTransaction oTransaction;
        private string sResponse = "";
        public string ApplicationPath = "";
        public List<ClsValidationResponse> oValidationResponseList;
        public List<ClsSearchResponse> oSearchResponseList;

        public string Response
        {
            get
            {
                return sResponse;
            }
            set
            {
                sResponse = value;
            }
        }
        public ClsTransaction ChargesTransaction
        {
            get
            {
                if (oTransaction == null)
                {
                    oTransaction = new ClsTransaction();
                }
                return oTransaction;
            }
            set
            {
                oTransaction = value;
            }
        }
        public void PostXML()
        {
            string SAMPLECLAIM_IN = "";
            SAMPLECLAIM_IN = GenerateXML();

            //SAMPLECLAIM_IN="E:\\Working folder 2008\\Projects\\gloPM\\SampleClaimSubmittal\\SampleClaimSubmittal\\Sample_Validation_Request.xml";
            //Console.WriteLine("building transaction markup from file {0}...", SAMPLECLAIM_IN);
            StringBuilder postBuilder = new StringBuilder();
            postBuilder.Append("Transaction=");
            StreamReader inputReader = new StreamReader(SAMPLECLAIM_IN);
            postBuilder.Append(inputReader.ReadToEnd());
            string postString = postBuilder.ToString();
            inputReader.Close();
            WebClient wcObj = new WebClient();

            wcObj.Credentials = new NetworkCredential(UNAME, PWORD);
            try
            {
                // Console.WriteLine("contacting server...\n");
                wcObj.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                //Console.WriteLine("posting claim data: {0}\n", postString);
                string serverResp = Encoding.ASCII.GetString(wcObj.UploadData(URL, Encoding.ASCII.GetBytes(postString)));
                //Console.WriteLine("server response: {0}", serverResp);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(serverResp);

                XmlNodeList results = xmlDoc.GetElementsByTagName("SearchResult");

                oValidationResponseList = new List<ClsValidationResponse>();
                oSearchResponseList = new List<ClsSearchResponse>();
                ClsValidationResponse oValidationResponse;
                ClsSearchResponse oSearchResponse;

                foreach (XmlNode node in results)
                {
                    //Console.WriteLine("-------------------------------------------------");
                    //Console.WriteLine("Code -> {0}\n", node.SelectSingleNode("Code").InnerText);
                    oSearchResponse = new ClsSearchResponse();
                    oSearchResponse.Code = node.SelectSingleNode("Code").InnerText;
                    oSearchResponse.Description = node.SelectSingleNode("Description").InnerText;
                    oSearchResponseList.Add(oSearchResponse);
                    //Console.WriteLine("Description -> {0}\n", node.SelectSingleNode("Description").InnerText);
                }

                results = xmlDoc.GetElementsByTagName("Service");

                foreach (XmlNode node in results)
                {
                    //System.Xml.XPath.XPathNavigator oxPath = node.CreateNavigator();
                    oValidationResponse = new ClsValidationResponse();

                    //XmlNode oNode = node.SelectSingleNode("Service");
                    //oValidationResponse.Code = oNode.InnerText;

                    XmlNode oNode = node.SelectSingleNode("MedicalNecessity");
                    oValidationResponse.MedicalNecessity = oNode.InnerText;

                    oNode = node.SelectSingleNode("CCI");
                    oValidationResponse.CCI = oNode.InnerText;

                    oNode = node.SelectSingleNode("Usage");
                    oValidationResponse.Usage = oNode.InnerText;

                    //oNode = node.SelectSingleNode("Modifiers");
                    //oValidationResponse.Modifiers = oNode.InnerText;

                    //oValidationResponse.Code = node.SelectSingleNode("Code").InnerText;
                    //oValidationResponse.MedicalNecessity = node.SelectSingleNode("MedicalNecessity").InnerText;
                    //oValidationResponse.CCI = node.SelectSingleNode("CCI").InnerText;
                    //oValidationResponse.Usage = node.SelectSingleNode("Usage").InnerText;
                    //oValidationResponse.Modifiers = node.SelectSingleNode("Modifiers").InnerText;

                    oValidationResponseList.Add(oValidationResponse);
                    //Console.WriteLine("Description -> {0}\n", node.SelectSingleNode("Description").InnerText);
                }
                //Console.WriteLine("done!");
            }
            catch (ClsClaimSubmittalException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            catch (Exception ex)
            {
                throw new ClsClaimSubmittalException(ex.ToString());
            }
            //System.Threading.Thread.Sleep(50000);
        }
        private string GenerateXML()
        {
            string XMLFilePath = "";
            XmlTextWriter oxmlWriter;
            try
            {
                XMLFilePath = GenerateFileName();
                if (File.Exists(XMLFilePath) == true)
                {
                    File.Delete(XMLFilePath);
                }
                oxmlWriter = new XmlTextWriter(XMLFilePath, null);
                oxmlWriter.WriteStartDocument();
                oxmlWriter.WriteStartElement("Transaction");
                oxmlWriter.WriteElementString("TransactionID", oTransaction.TransactionID);
                oxmlWriter.WriteElementString("ProviderNPI", oTransaction.TransactionID);
                oxmlWriter.WriteElementString("ActionCode", oTransaction.ActionCode);

                oxmlWriter.WriteStartElement("SearchData");
                oxmlWriter.WriteElementString("CodeSet", oTransaction.CodeSet);
                oxmlWriter.WriteElementString("SearchTerms", oTransaction.SearchTerms);
                oxmlWriter.WriteEndElement(); // End SearchData

                //PatientInfo
                oxmlWriter.WriteStartElement("PatientInfo");

                oxmlWriter.WriteStartElement("DOB");
                oxmlWriter.WriteElementString("Month", oTransaction.Patient.Month);
                oxmlWriter.WriteElementString("Day", oTransaction.Patient.Day);
                oxmlWriter.WriteElementString("Year", oTransaction.Patient.Year);
                oxmlWriter.WriteEndElement(); //End DOB
                oxmlWriter.WriteElementString("Sex", oTransaction.Patient.Gender);
                oxmlWriter.WriteEndElement(); //End PatientInfo

                oxmlWriter.WriteStartElement("ServiceInfo");
                oxmlWriter.WriteElementString("ZIP", oTransaction.TransactionID);
                oxmlWriter.WriteStartElement("Services");
                foreach (ClsServiceInfo oServiceInfo in oTransaction.Services)
                {
                    oxmlWriter.WriteStartElement("Service");
                    //POS
                    oxmlWriter.WriteElementString("POS", oServiceInfo.POS);
                    oxmlWriter.WriteStartElement("DOS");

                    oxmlWriter.WriteStartElement("From");
                    oxmlWriter.WriteElementString("Month", oServiceInfo.FromMonth);
                    oxmlWriter.WriteElementString("Day", oServiceInfo.FromDay);
                    oxmlWriter.WriteElementString("Year", oServiceInfo.FromYear);
                    oxmlWriter.WriteEndElement(); //End From

                    oxmlWriter.WriteStartElement("To");
                    oxmlWriter.WriteElementString("Month", oServiceInfo.ToMonth);
                    oxmlWriter.WriteElementString("Day", oServiceInfo.ToDay);
                    oxmlWriter.WriteElementString("Year", oServiceInfo.ToYear);
                    oxmlWriter.WriteEndElement(); //End To

                    oxmlWriter.WriteEndElement(); //End DOS

                    oxmlWriter.WriteElementString("ProcCode", oServiceInfo.ProcCode);


                    if (oServiceInfo.ModifierList != null)
                    {
                        oxmlWriter.WriteStartElement("Modifiers");
                        foreach (string oModifier in oServiceInfo.ModifierList)
                        {
                            oxmlWriter.WriteElementString("Modifier", oModifier);
                        }
                        oxmlWriter.WriteEndElement(); // End Modifiers
                    }

                    //DiagList

                    if (oServiceInfo.DiagnosisList != null)
                    {
                        oxmlWriter.WriteStartElement("DiagList");
                        foreach (string oDiagnosis in oServiceInfo.DiagnosisList)
                        {
                            oxmlWriter.WriteElementString("Diag", oDiagnosis);
                        }
                        oxmlWriter.WriteEndElement(); // End DiagList
                    }

                    oxmlWriter.WriteEndElement(); // End Service
                }


                oxmlWriter.WriteEndElement(); // End Services
                oxmlWriter.WriteEndElement(); // End ServiceInfo

                oxmlWriter.WriteEndElement(); //End Transaction
                oxmlWriter.WriteEndDocument();
                oxmlWriter.Close();
                return XMLFilePath;
            }

            catch (ClsClaimSubmittalException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ClsClaimSubmittalException(ex.ToString());
            }
            finally
            {

            }
        }
        private string GenerateFileName()
        {
            string strfilename = "";
            try
            {
                //switch (eMessageType)
                //{

                //    case MessageType.eError:
                //        strfilename = "Error";
                //        break;
                //    case MessageType.eNewRx:
                //        strfilename = "NewRx";
                //        break;
                //    case MessageType.eRefillRequest:
                //        strfilename = "RefillRequest";
                //        break;
                //    case MessageType.eRefillResponse:
                //        strfilename = "RefillResponse";
                //        break;
                //    case MessageType.eStatus:
                //        strfilename = "Status";
                //        break;
                //    case MessageType.eVerify:
                //        strfilename = "Verify";
                //        break;
                //    case MessageType.eCancelRx:
                //        strfilename = "CancelRx";
                //        break;
                //}
                DateTime dtdate = System.DateTime.UtcNow;
                string strtemp = strfilename + dtdate.Month.ToString() + dtdate.Day.ToString() + dtdate.Year.ToString() + dtdate.Hour.ToString() + dtdate.Minute.ToString() + dtdate.Second.ToString() + dtdate.Millisecond.ToString();

                string strrootname = ApplicationPath + "\\ClaimValidations";
                if (Directory.Exists(strrootname) == false) { Directory.CreateDirectory(strrootname); }
                strfilename = strrootname + "\\" + strtemp + ".xml";

                return strfilename;
            }

            catch (ClsClaimSubmittalException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ClsClaimSubmittalException(ex.ToString());
            }
            finally
            {

            }
        }

    }

}