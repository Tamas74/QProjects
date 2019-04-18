using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using gloMeds.Core.TenDotSix;
using System.Xml.Serialization;

using System.ServiceModel;
using System.Xml;

using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections;


namespace gloMeds.Core.MedHx
{
    public class MedHxInterface
    {
        string connectionString =String.Empty  ;//Server=dev05;Database=glo8010_demo;User Id=sa;Password=sadev05";
        //string ParticipantID  =String.Empty  ;
        //string ParticipantPwd  =String.Empty  ;
        //string gloSuiteversion  =String.Empty  ;
        string PostMedHxURL=String.Empty  ;

        bool IsONC = false;

        public MedHxInterface()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["MedHxDBConnection"] != null)
            {
                connectionString = System.Configuration.ConfigurationManager.AppSettings["MedHxDBConnection"].ToString();

            }
            if (System.Configuration.ConfigurationManager.AppSettings["IsONC"] != null)
            {
                IsONC = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["IsONC"]);
            }
        }

        private DataSet GetMedHxRequestParameters(Int64 PatientID, string PBMName, string PBMMemberID, Int64 nLoginProviderid)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataAdapter adp = null;
            DataSet dtEligibilityInfo = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("gsp_GetMedHxRequestInfo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nPatientID", PatientID));
                cmd.Parameters.Add(new SqlParameter("@strPBM", PBMName));
                cmd.Parameters.Add(new SqlParameter("@strPBMMemberID", PBMMemberID));
                cmd.Parameters.Add(new SqlParameter("@nLoginProviderid", nLoginProviderid));

                adp = new SqlDataAdapter(cmd);
                dtEligibilityInfo = new DataSet();
                adp.Fill(dtEligibilityInfo);

            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
            }

            finally
            {
                if (adp != null) { adp.Dispose(); adp = null; }
                if (cmd != null) { cmd.Parameters.Clear(); cmd.Dispose(); cmd = null; }
                if (con != null) { con.Dispose(); con = null; }
            }
            return dtEligibilityInfo;
        }
        
        

        public MedHxRequests GenerateMedHxRequest(DataSet dsMedHxRequstParams, Int64 requestID,string version,long clincID)
        {
            MedHxRequests requests = new MedHxRequests();
            MedHxRequest requestParms = null;
            try
            {

                if (dsMedHxRequstParams.Tables[0] != null)
                {
                   
                    for (int i = 0; i < dsMedHxRequstParams.Tables[0].Rows.Count; i++)
                    {
                        requestParms = new MedHxRequest();
                        requestParms.RequestID = requestID;
                        //requestParms.ParticipantID = ParticipantID; //"T00000000020315";
                        //requestParms.RxHubPassword = ParticipantPwd;
                        requestParms.gloSuiteVersion = version;
                        requestParms.ScriptVersion = "10.6";
                        requestParms.PracticeID = clincID;

                        requestParms.PBMInfo = new BenifitsCoordinations
                        {
                            PayerID = dsMedHxRequstParams.Tables[0].Rows[i]["sPBM_PayerParticipantID"].ToString(),
                            PayerName = dsMedHxRequstParams.Tables[0].Rows[i]["sPBM_PayerName"].ToString(),
                            PBMMemberID = dsMedHxRequstParams.Tables[0].Rows[i]["sPBM_PayerMemberID"].ToString(),
                            RelatesToMessageID = dsMedHxRequstParams.Tables[0].Rows[i]["RelatesToMessageID"].ToString(),
                            PlanName = dsMedHxRequstParams.Tables[0].Rows[i]["PlanName"].ToString()
                        };


                        if (dsMedHxRequstParams.Tables[1] != null)
                        {
                            if (dsMedHxRequstParams.Tables[1].Rows.Count >= 1)
                            {
                                requestParms.PatientInfo = new Patient
                                {
                                    PatientID = Convert.ToInt64(dsMedHxRequstParams.Tables[1].Rows[0]["nPatientID"]),
                                    FirstName = dsMedHxRequstParams.Tables[1].Rows[0]["sFirstName"].ToString(),
                                    LastName = dsMedHxRequstParams.Tables[1].Rows[0]["sLastName"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(dsMedHxRequstParams.Tables[1].Rows[0]["dtDOB"]),
                                    Gender = dsMedHxRequstParams.Tables[1].Rows[0]["sGender"].ToString()
                                };

                                requestParms.ProviderInfo = new Prescriber
                                {
                                    FirstName = dsMedHxRequstParams.Tables[1].Rows[0]["sPrFirstName"].ToString(),
                                    LastName = dsMedHxRequstParams.Tables[1].Rows[0]["sPrLastName"].ToString(),
                                    NPI = dsMedHxRequstParams.Tables[1].Rows[0]["sPrNPI"].ToString()
                                };
                            }
                        }

                        requests.HxRequests.Add(requestParms);
                    }


                }





            }
            catch (Exception ex)
            {
                throw ex;
            }

            return requests;
        }


        private IEnumerable<XElement> GetObjectXelement(Int64 RequestId)
        {
            IEnumerable<XElement> medHxList = null;
            string responseXML = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("gsp_MedicationHistoryMergeGet", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@sRequestID", RequestId));

                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                adp.Fill(dt);

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    //string xmlnsPattern = "\\s+xmlns\\s*(:\\w)?\\s*=\\s*\\\"(?<url>[^\\\"]*)\\\"";
                                    if (dt.Rows.Count <= 1)
                                    {
                                        responseXML = Convert.ToString(dt.Rows[0]["ResponceXMLFile"]);
                                    }
                                    else
                                    {
                                        responseXML = Convert.ToString(dt.Rows[1]["ResponceXMLFile"]);
                                    }
                                    //MatchCollection matchcol = Regex.Matches(responseXML, xmlnsPattern);
                                    //foreach (Match m in matchcol)
                                    //{
                                    //    responseXML = responseXML.Replace(m.ToString(), "");
                                    //}

                                    XDocument xdoc = XDocument.Parse(responseXML);


                                    IEnumerable<XElement> list_data = from s in xdoc.Descendants("MedicationDispensed")
                                                                      select new XElement(s);

                                    medHxList = list_data.ToList<XElement>();
                                }
                            }
                        }
                    }
                    con.Close();
                    
                }

                ///TODO ....

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return medHxList;
        }
        
        public List<MedHxItem> GetMedHxSelectedResponse(long RequestId)
        {
            List<MedHxItem> medhx_list = new List<MedHxItem>();
           
            IEnumerable<XElement> medHxList = GetObjectXelement(RequestId);
            
            if ( medHxList != null) {
            foreach (XElement item in medHxList)
            {
                MedHxItem medhx = new MedHxItem();

                if (item.Element("DrugDescription") != null)
                { medhx.DrugName = item.Element("DrugDescription").Value; }

                if (item.Element("UniqueNo") != null)
                {
                    medhx.UniqueNo = Convert.ToInt32(item.Element("UniqueNo").Value);
                }


                if (item.Element("PBM_Name") != null)
                {
                    medhx.PayerName = item.Element("PBM_Name").Value;
                }
                if (item.Element("DrugCoded") != null)
                {
                    if (item.Element("DrugCoded").Element("ProductCode") != null)
                    { medhx.NDCCode = item.Element("DrugCoded").Element("ProductCode").Value; }
                }
                if (item.Element("Quantity") != null)
                {
                    if (item.Element("Quantity").Element("Value") != null)
                    {
                        medhx.DrugQty =Convert.ToInt32(item.Element("Quantity").Element("Value").Value);
                    }
                }

                if (item.Element("DaysSupply") != null)
                { medhx.DaySupply = Convert.ToInt32(item.Element("DaysSupply").Value); }

                if (item.Element("LastFillDate") != null)
                {
                    if (item.Element("LastFillDate").Element("Date") != null)
                    {
                        try
                        {
                            medhx.StartDate = Convert.ToDateTime(item.Element("LastFillDate").Element("Date").Value);
                        }
                        catch //(Exception e)
                        {
                            medhx.StartDate = DateTime.Now.Date;
                        }
                    } 
                }

                if (item.Element("Refills") != null)
                {
                    if (item.Element("Refills").Element("Value") != null)
                    {
                        medhx.Refills = Convert.ToInt32(item.Element("Refills").Element("Value").Value);
                    }

                }

                if (item.Element("StructuredSIG") != null)
                {
                    if (item.Element("StructuredSIG").Element("FreeText") != null)
                    {
                        if (item.Element("StructuredSIG").Element("FreeText").Element("SigFreeText") != null)
                        {
                            medhx.Direction = item.Element("StructuredSIG").Element("FreeText").Element("SigFreeText").Value;
                        }
                    }
                }
                if (item.Element("Quantity") != null)
                {
                    if (item.Element("Quantity").Element("PotencyUnitCode") != null)
                    { medhx.PotencyCode = item.Element("Quantity").Element("PotencyUnitCode").Value; }
                }

                if (item.Element("Prescriber") != null)
                {
                    if (item.Element("Prescriber").Element("Identification") != null)
                    {
                        if (item.Element("Prescriber").Element("Identification").Element("NPI") != null)
                        {
                            medhx.PrescriberNPI = item.Element("Prescriber").Element("Identification").Element("NPI").Value;

                        }
                    }
                }


                if (item.Element("Pharmacy") != null)
                {
                    if (item.Element("Pharmacy").Element("StoreName") != null)
                    { medhx.Pharmacy = item.Element("Pharmacy").Element("StoreName").Value; }

                    if (item.Element("Pharmacy").Element("Identification") != null)
                    {
                        if (item.Element("Pharmacy").Element("Identification").Element("NPI") != null)
                        { medhx.NPI = item.Element("Pharmacy").Element("Identification").Element("NPI").Value; }

                        if (item.Element("Pharmacy").Element("Identification").Element("NCPDPID") != null)
                        { medhx.NCPDPId = item.Element("Pharmacy").Element("Identification").Element("NCPDPID").Value; }
                    }
                    if (item.Element("Pharmacy").Element("Address") != null)
                    {
                        if (item.Element("Pharmacy").Element("Address").Element("AddressLine1") != null)
                        { medhx.PharmacyAddress1 = item.Element("Pharmacy").Element("Address").Element("AddressLine1").Value; }

                        if (item.Element("Pharmacy").Element("Address").Element("AddressLine2") != null)
                        { medhx.PharmacyAddress2 = item.Element("Pharmacy").Element("Address").Element("AddressLine2").Value; }

                        if (item.Element("Pharmacy").Element("Address").Element("City") != null)
                        { medhx.PharmacyCity = item.Element("Pharmacy").Element("Address").Element("City").Value; }

                        if (item.Element("Pharmacy").Element("Address").Element("State") != null)
                        { medhx.PharmacyState = item.Element("Pharmacy").Element("Address").Element("State").Value; }

                        if (item.Element("Pharmacy").Element("Address").Element("ZipCode") != null)
                        { medhx.PharmacyZip = item.Element("Pharmacy").Element("Address").Element("ZipCode").Value; }
                    }

                    if (item.Element("Pharmacy").Element("CommunicationNumbers") != null)
                    {
                        foreach (XElement comm in item.Element("Pharmacy").Elements("CommunicationNumbers"))
                        {
                            if (comm != null)
                            {
                                if (comm.Element("Communication") != null)
                                {
                                    if (comm.Element("Communication").Element("Qualifier") != null)
                                    {
                                        if (comm.Element("Communication").Element("Qualifier").Value == "TE")
                                        {
                                            medhx.PharmacyPhone = comm.Element("Communication").Element("Number").Value;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                medhx.MedicationDate = DateTime.Now.Date;
                medhx_list.Add(medhx);
            }}
            
            
            return medhx_list;

        }
        
        public List<MedHxItemNew> GetMedHxSelectedResponseNew(long RequestId)
        {
            List<MedHxItemNew> medhx_list = new List<MedHxItemNew>();
           
            IEnumerable<XElement> medHxList = GetObjectXelement(RequestId);
            
            if (medHxList != null)
            {
                foreach (XElement item in medHxList)
                {
                    MedHxItemNew medhx = new MedHxItemNew();

                    if (item.Element("DrugDescription") != null)
                    { medhx.DrugName = item.Element("DrugDescription").Value; }

                    if (item.Element("UniqueNo") != null)
                    {
                        medhx.UniqueNo = Convert.ToInt32(item.Element("UniqueNo").Value);
                    }


                    if (item.Element("PBM_Name") != null)
                    {
                        medhx.PayerName = item.Element("PBM_Name").Value;
                    }
                    if (item.Element("DrugCoded") != null)
                    {
                        if (item.Element("DrugCoded").Element("ProductCode") != null)
                        { medhx.NDCCode = item.Element("DrugCoded").Element("ProductCode").Value; }
                    }
                    if (item.Element("Quantity") != null)
                    {
                        if (item.Element("Quantity").Element("Value") != null)
                        {
                            medhx.DrugQty = Convert.ToString(item.Element("Quantity").Element("Value").Value);
                        }
                    }

                    if (item.Element("DaysSupply") != null)
                    { medhx.DaySupply = Convert.ToString(item.Element("DaysSupply").Value); }

                    if (item.Element("LastFillDate") != null)
                    {
                        if (item.Element("LastFillDate").Element("Date") != null)
                        {
                            try
                            {
                                medhx.StartDate = Convert.ToDateTime(item.Element("LastFillDate").Element("Date").Value);
                            }
                            catch //(Exception e)
                            {
                                medhx.StartDate = DateTime.Now.Date;
                            }

                        }
                    }

                    if (item.Element("Refills") != null)
                    {
                        if (item.Element("Refills").Element("Value") != null)
                        {
                            medhx.Refills = Convert.ToString(item.Element("Refills").Element("Value").Value);
                        }

                    }

                    //if (item.Element("StructuredSIG") != null)
                    //{
                    //    if (item.Element("StructuredSIG").Element("FreeText") != null)
                    //    {
                    //        if (item.Element("StructuredSIG").Element("FreeText").Element("SigFreeText") != null)
                    //        {
                    //            medhx.Direction = item.Element("StructuredSIG").Element("FreeText").Element("SigFreeText").Value;
                    //        }
                    //    }
                    //}
                    if (item.Element("Directions") != null)
                    {
                        medhx.Direction = item.Element("Directions").Value;
                    }
                    if (item.Element("Quantity") != null)
                    {
                        if (item.Element("Quantity").Element("PotencyUnitCode") != null)
                        { medhx.PotencyCode = item.Element("Quantity").Element("PotencyUnitCode").Value; }
                    }

                    if (item.Element("Prescriber") != null)
                    {
                        if (item.Element("Prescriber").Element("Identification") != null)
                        {
                            if (item.Element("Prescriber").Element("Identification").Element("NPI") != null)
                            {
                                medhx.PrescriberNPI = item.Element("Prescriber").Element("Identification").Element("NPI").Value;

                            }
                        }
                    }


                    if (item.Element("Pharmacy") != null)
                    {
                        if (item.Element("Pharmacy").Element("StoreName") != null)
                        { medhx.Pharmacy = item.Element("Pharmacy").Element("StoreName").Value; }

                        if (item.Element("Pharmacy").Element("Identification") != null)
                        {
                            if (item.Element("Pharmacy").Element("Identification").Element("NPI") != null)
                            { medhx.NPI = item.Element("Pharmacy").Element("Identification").Element("NPI").Value; }

                            if (item.Element("Pharmacy").Element("Identification").Element("NCPDPID") != null)
                            { medhx.NCPDPId = item.Element("Pharmacy").Element("Identification").Element("NCPDPID").Value; }
                        }
                        if (item.Element("Pharmacy").Element("Address") != null)
                        {
                            if (item.Element("Pharmacy").Element("Address").Element("AddressLine1") != null)
                            { medhx.PharmacyAddress1 = item.Element("Pharmacy").Element("Address").Element("AddressLine1").Value; }

                            if (item.Element("Pharmacy").Element("Address").Element("AddressLine2") != null)
                            { medhx.PharmacyAddress2 = item.Element("Pharmacy").Element("Address").Element("AddressLine2").Value; }

                            if (item.Element("Pharmacy").Element("Address").Element("City") != null)
                            { medhx.PharmacyCity = item.Element("Pharmacy").Element("Address").Element("City").Value; }

                            if (item.Element("Pharmacy").Element("Address").Element("State") != null)
                            { medhx.PharmacyState = item.Element("Pharmacy").Element("Address").Element("State").Value; }

                            if (item.Element("Pharmacy").Element("Address").Element("ZipCode") != null)
                            { medhx.PharmacyZip = item.Element("Pharmacy").Element("Address").Element("ZipCode").Value; }
                        }

                        if (item.Element("Pharmacy").Element("CommunicationNumbers") != null)
                        {
                            foreach (XElement comm in item.Element("Pharmacy").Elements("CommunicationNumbers"))
                            {
                                if (comm != null)
                                {
                                    if (comm.Element("Communication") != null)
                                    {
                                        if (comm.Element("Communication").Element("Qualifier") != null)
                                        {
                                            if (comm.Element("Communication").Element("Qualifier").Value == "TE")
                                            {
                                                medhx.PharmacyPhone = comm.Element("Communication").Element("Number").Value;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    medhx.MedicationDate = DateTime.Now.Date;
                    medhx_list.Add(medhx);
                }
            }
            
            return medhx_list;

        }
        public bool SaveMergeResponceXML(Int64 RequestID, string responceFile)
        {
            int _RecordAffected = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("gsp_MedicationHistory10dot6Merge", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@RequestID", RequestID));
                        cmd.Parameters.Add(new SqlParameter("@ResponceXMLFile", responceFile));

                        _RecordAffected = cmd.ExecuteNonQuery();

                        if (_RecordAffected >= 1)
                        {
                            con.Close();
                            return true;
                        }
                        else
                        {
                            con.Close();
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }

        }
       
        public bool CreateRequest(MedHxRequest request)
        {
            int _RecordAffected = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("gsp_Medication_History10dot6Header", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@sMessageID", request.MessageID));
                        cmd.Parameters.Add(new SqlParameter("@sPatientID", request.PatientInfo.PatientID));
                        cmd.Parameters.Add(new SqlParameter("@sPatientFirstName", request.PatientInfo.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@sPatientLastName", request.PatientInfo.LastName));
                        cmd.Parameters.Add(new SqlParameter("@sPatientDOB", request.PatientInfo.DateOfBirth));
                        cmd.Parameters.Add(new SqlParameter("@sPatientGender", request.PatientInfo.Gender));
                        cmd.Parameters.Add(new SqlParameter("@sProviderID", request.ProviderInfo.ProviderID));
                        cmd.Parameters.Add(new SqlParameter("@sProviderFirstName", request.ProviderInfo.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@sProviderLastName", request.ProviderInfo.LastName));
                        cmd.Parameters.Add(new SqlParameter("@sProviderNPI", request.ProviderInfo.NPI));
                        cmd.Parameters.Add(new SqlParameter("@sPayerID", request.PBMInfo.PayerID));
                        cmd.Parameters.Add(new SqlParameter("@sPayerName", request.PBMInfo.PayerName));

                        if (request.PBMInfo.PlanName != null)
                        { cmd.Parameters.Add(new SqlParameter("@sPlanName", request.PBMInfo.PlanName)); }
                        else
                        { cmd.Parameters.Add(new SqlParameter("@sPlanName", "")); }


                        cmd.Parameters.Add(new SqlParameter("@sPayerMemberID", request.PBMInfo.PBMMemberID));
                        cmd.Parameters.Add(new SqlParameter("@sgloSuiteVersion", request.gloSuiteVersion));
                        cmd.Parameters.Add(new SqlParameter("@sPracticeID", request.PracticeID));
                        cmd.Parameters.Add(new SqlParameter("@requestXml", request.RequestXML));

                        if (request.ParentMessageID.HasValue)
                        { cmd.Parameters.Add(new SqlParameter("@ParentMessageID", request.ParentMessageID.Value)); }
                        else
                        { cmd.Parameters.Add(new SqlParameter("@ParentMessageID", DBNull.Value)); }

                        cmd.Parameters.Add(new SqlParameter("@RequestID", request.RequestID));
                        cmd.Parameters.Add(new SqlParameter("@RelatesToMessageID", request.PBMInfo.RelatesToMessageID));

                        _RecordAffected = cmd.ExecuteNonQuery();

                        if (_RecordAffected >= 1)
                        {
                            con.Close();
                            return true;
                        }
                        else
                        {
                            con.Close();
                            return false;
                        }
                    }
                                    }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        private string GetQuotedParameterThing(Guid? value)
        {
            return value == null ? "NULL" : "\"+ value + \"";
        }

        public bool UpdateRequestFile()
        {
            return false;
        }

        public bool UpdateResponseFile(MedHxResponse response)
        {
            int _RecordAffected = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("gsp_MedicationHistory10dot6ResponceUpdate", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@responceXml", response.ResponseXML));
                        cmd.Parameters.Add(new SqlParameter("@sMessageID", response.MessageID));

                        _RecordAffected = cmd.ExecuteNonQuery();

                        if (_RecordAffected >= 1)
                        {
                            con.Close();
                            return true;
                        }
                        else
                        {
                            con.Close();
                            return false;
                        }
                    }
                     
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public MedHxRequest GetRequest(Guid ParentMessageID)
        {
            MedHxRequest request = null;

            using (DataTable dt = GetRequestInfo(ParentMessageID))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    request = new MedHxRequest
                    {
                        ParentMessageID = ParentMessageID,
                        PatientInfo = new Patient
                        {
                            PatientID = Convert.ToInt64(dr["PatientID"]),
                            FirstName = Convert.ToString(dr["PatientFirstName"]),
                            LastName = Convert.ToString(dr["PatientLastName"]),
                            DateOfBirth = Convert.ToDateTime(dr["PatientDOB"]),
                            Gender = Convert.ToString(dr["PatientGender"])
                        },
                        ProviderInfo = new Prescriber
                        {
                            ProviderID = Convert.ToInt64(dr["ProviderID"]),
                            FirstName = Convert.ToString(dr["ProviderFirstName"]),
                            LastName = Convert.ToString(dr["ProviderLastName"]),
                            NPI = Convert.ToString(dr["ProviderNPI"])
                        },
                        PBMInfo = new BenifitsCoordinations
                        {
                            PayerID = Convert.ToString(dr["PayerID"]),
                            PayerName = Convert.ToString(dr["PayerName"]),
                            PBMMemberID = Convert.ToString(dr["PayerMemberID"]),
                            PlanName = Convert.ToString(dr["PlanName"]),
                            RelatesToMessageID = Convert.ToString(dr["RelatesToMessageID"])
                        },
                        gloSuiteVersion = Convert.ToString(dr["gloSuiteVersion"]),
                        PracticeID = Convert.ToInt64(dr["PracticeID"])

                    };
                }
            }

            return request;
        }

        #region "Medication History - Request & Response Methods"

        public string GenerateMedHxRequest(MedHxRequest request)
        {
            XmlDocument doc = null;
            MessageType msg = new MessageType();
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Outbox"))
            {
                //create the Outbox directory, to save the 270 generated files.
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "Outbox");
            }

            string requestFile = System.AppDomain.CurrentDomain.BaseDirectory + "Outbox\\\\" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + request.RequestID.ToString()+ ".xml";
            if (System.IO.File.Exists(requestFile))
            {
                requestFile = System.AppDomain.CurrentDomain.BaseDirectory + "Outbox\\\\" + System.DateTime.Now.ToString("yyyyMMddhhmmss") +"A"+ request.RequestID.ToString() + ".xml";
            }
            try
            {
                msg.version = "010";
                msg.release = "006";
                msg.schemaLocation = "http://www.surescripts.com/messaging SS_SCRIPT_XML_10_6.xsd";

                msg.Header = GetMedHxRequestHeader(request);
                msg.Body = GetMedHxRequestBody(request);

                if (msg != null)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(MessageType));
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(requestFile))
                    {
                        serializer.Serialize(file, msg);
                        file.Close();
                    }
                    serializer = null;
                }

                doc = new XmlDocument();
                doc.Load(requestFile);
                request.RequestXML = doc.InnerXml;
                
                bool isRequestCreated = CreateRequest(request);

                return requestFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (doc != null) { doc = null; }
                if (msg != null) { msg = null; }
            }
        }

        public string GenerateMedHxRequest(Guid ParentMessageID, DateTime? EffectiveDate, DateTime? ExpirationDate, Guid NewMessageID, Int64 RequestID, string ParticipantID, string ParticipantPwd, string TestMessage)
        {
            MedHxRequest request = GetRequest(ParentMessageID);

            request.MessageID = NewMessageID;
            request.RequestID = RequestID;

            if (EffectiveDate.HasValue)
            { request.PBMInfo.EffectiveDate = EffectiveDate.Value; }

            if (ExpirationDate.HasValue)
            { request.PBMInfo.ExpirationDate = ExpirationDate.Value; }

            request.ParticipantID = ParticipantID; //"T00000000020315";
            request.RxHubPassword = ParticipantPwd; //"FXTXGJVZ0W";
            request.gloSuiteVersion = "8.0.3.0";
            request.ScriptVersion = "10.6";
            request.Consent = "Y";
            
            if (!IsONC) {request.TestMessage = TestMessage; }

            XmlDocument doc = null;
            MessageType msg = new MessageType();
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Outbox"))
            {
                //create the Outbox directory, to save the 270 generated files.
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "Outbox");
            }

            string requestFile = System.AppDomain.CurrentDomain.BaseDirectory + "Outbox\\\\" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + RequestID.ToString() + ".xml";

            if (System.IO.File.Exists(requestFile))
            {
                requestFile = System.AppDomain.CurrentDomain.BaseDirectory + "Outbox\\\\" + System.DateTime.Now.ToString("yyyyMMddhhmmss")+"A" + RequestID.ToString() + ".xml";
            }
            try
            {
                msg.version = "010";
                msg.release = "006";
                msg.schemaLocation = "http://www.surescripts.com/messaging SS_SCRIPT_XML_10_6.xsd";

                msg.Header = GetMedHxRequestHeader(request);
                msg.Body = GetMedHxRequestBody(request);

                if (msg != null)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(MessageType));
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(requestFile))
                    {
                        serializer.Serialize(file, msg);
                        file.Close();
                    }
                    serializer = null;
                }

                doc = new XmlDocument();
                doc.Load(requestFile);
                request.RequestXML = doc.InnerXml;

                bool isRequestCreated = CreateRequest(request);

                return requestFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (doc != null) { doc = null; }
                if (msg != null) { msg = null; }
            }
        }


        public MedHxResponse PostMedHxRequest(string requestFile, Guid MessageId,string sSurescriptURL)
        {
            MedHxResponse response = null;

            string responseFile = string.Empty;

            byte[] requestBytes = null;
            byte[] responsetBytes = null;

            using (FileStream stream = new FileStream(requestFile, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    requestBytes = reader.ReadBytes(Convert.ToInt32(stream.Length));
                    reader.Close();
                }
                stream.Close();
            }

            #region "Post Request "

            if (requestBytes != null)
            {
                Uri serviceUri = null;
                WSHttpBinding binding = null;
                EndpointAddress endpointAddress = null;
                MedHxService.IeRxClient client = null;
                string sFileContent = "";
                try
                {
                    string sUriString = sSurescriptURL; //"https://ophit.net/WCFeRx/eRx.svc/secure";

                    serviceUri = new Uri(sUriString);
                    endpointAddress = new System.ServiceModel.EndpointAddress(serviceUri);

                    binding = BindingFactory.CreateInstance();
                    client = new MedHxService.IeRxClient(binding, endpointAddress);

                    responsetBytes = client.PostClient270nMedHxMessage(requestBytes, "RxHistoryRequest");

                    bool isResponseCreated = false;

                    if (responsetBytes != null)
                    {
                        if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Inbox"))
                        {
                            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "Inbox");
                        }

                        responseFile = System.AppDomain.CurrentDomain.BaseDirectory + "Inbox\\EDI271_" + (System.DateTime.Now.ToString("yyyyMMddhhmmssmmm") + MessageId.ToString() + ".XML");
                       
                        if (File.Exists(responseFile))
                        {
                            responseFile = System.AppDomain.CurrentDomain.BaseDirectory + "Inbox\\EDI271_1" + (System.DateTime.Now.ToString("yyyyMMddhhmmssmmm") + MessageId.ToString() + ".XML");
                        }
                        //using (MemoryStream mStream = new MemoryStream(responsetBytes))
                        //{
                            using (FileStream fStream = new FileStream(responseFile, FileMode.Create))
                            {
                                //mStream.WriteTo(fStream);
                                fStream.Write(responsetBytes, 0, responsetBytes.Length);
                                fStream.Close();
                            }
                            
                        //}

                        #region " Creating a Responce"


                        sFileContent = File.ReadAllText(responseFile);

                        if (sFileContent.ToLower().Contains("listener not started"))
                        {
                            response = new MedHxResponse(Guid.NewGuid());
                            response.ResponseFile = responseFile;
                            response.ResponseXML = sFileContent;
                            response.IsErrorResponse = true;
                            return response;
                        }


                        XDocument responseDoc = XDocument.Load(responseFile);


                        string responseXML = RemoveNamespace(responseDoc.ToString());

                        response = new MedHxResponse(MessageId);
                        response.ResponseFile = responseFile;
                        response.ResponseXML = responseXML;
                        
                        responseDoc = XDocument.Parse(responseXML);

                        IEnumerable<XElement> error = from s in responseDoc.Descendants("Error")
                                                      select new XElement(s);

                        IEnumerable<XElement> isAQAvailable = from s in responseDoc.Descendants("ApprovalReasonCode")
                                                              select new XElement(s);

                        IEnumerable<DateTime> LastFilldate = responseDoc.Descendants("LastFillDate").Select(p => Convert.ToDateTime(p.Element("Date").Value));

                        if (error.Count() > 0)
                        { response.IsErrorResponse = true; }
                        else
                        { response.IsErrorResponse = false; }

                        if (isAQAvailable.Count() > 0)
                        { response.IsMoreHxAvaiable = true; }
                        else
                        { response.IsMoreHxAvaiable = false; }

                        if (LastFilldate.Any())
                        {
                            response.EffectiveDate = DateTime.Now.AddYears(-2).Date;
                            response.ExpirationDate = LastFilldate.Min();
                        }

                        #endregion

                        isResponseCreated = UpdateResponseFile(response);
                    }
                    client.Close();
                    serviceUri = null;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (binding != null) { binding = null; }
                    if (endpointAddress != null) { endpointAddress = null; }
                    if (serviceUri != null) { serviceUri = null; }
                    if (client != null) { client = null; }
                    if (System.IO.File.Exists(responseFile) == true)
                    {
                        System.IO.File.Delete(responseFile);
                    }
                }
            }

            #endregion

            return response;
        }


        #endregion

        #region "Message Helpers"

        private BodyType GetMedHxRequestBody(MedHxRequest request)
        {
            BodyType body = new BodyType();
            RxHistoryRequest msg_body_request = new RxHistoryRequest();

            try
            {
                msg_body_request.Patient = GetMedHxRequestPatient(request);
                msg_body_request.Prescriber = GetMedHxRequestPrescriber(request);
                msg_body_request.BenefitsCoordination = GetMedHxRequestBenifits(request);
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
            }
            finally
            {
                body.Item = msg_body_request;
            }

            return body;
        }

        private HeaderType GetMedHxRequestHeader(MedHxRequest request)
        {
            try
            {
                HeaderType msg_header = new HeaderType();
                msg_header.RelatesToMessageID = request.PBMInfo.RelatesToMessageID;

                QualifiedMailAddressType msg_header_to = new QualifiedMailAddressType();
                msg_header_to.Qualifier = "ZZZ";
                msg_header_to.Value = request.PBMInfo.PayerID; //"T00000000001000";

                QualifiedMailAddressType msg_header_from = new QualifiedMailAddressType();
                msg_header_from.Qualifier = "ZZZ";
                msg_header_from.Value = request.ParticipantID;//"T00000000020315";

                SecurityType msg_security = new SecurityType();
               // msg_security.UsernameToken = new UsernameTokenType(); // TODO : check why username is blank;
                msg_security.Sender = new SecurityIdentificationType();
                msg_security.Receiver = new SecurityIdentificationType();
                msg_security.Sender.SecondaryIdentification = request.RxHubPassword;// "FXTXGJVZ0W";
                msg_security.Receiver.SecondaryIdentification = request.ReceiverIdentification;
                msg_header.Security = msg_security;

                msg_header.To = msg_header_to;
                msg_header.From = msg_header_from;
                msg_header.MessageID = request.MessageID.ToString("N"); //DateTime.Now.ToString("yyyyMMddhhmmss");
                msg_header.SentTime = System.DateTime.Now;
                msg_header.Security = msg_security;

                if (!IsONC)
                {
                    msg_header.SenderSoftware = new SenderSoftwareType();
                    msg_header.SenderSoftware.SenderSoftwareDeveloper = request.CompanyName;
                    msg_header.SenderSoftware.SenderSoftwareProduct = request.ProductName;
                    msg_header.SenderSoftware.SenderSoftwareVersionRelease = request.gloSuiteVersion; 
                }
                
                msg_header.TestMessage = request.TestMessage; //"1";

                return msg_header;
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex; ;
            }
        }

        private BenefitsCoordinationRequestType[] GetMedHxRequestBenifits(MedHxRequest request)
        {
            try
            {
                BenifitsCoordinations HxArgs = request.PBMInfo;

                List<BenefitsCoordinationRequestType> benifits = new List<BenefitsCoordinationRequestType>();
                BenefitsCoordinationRequestType benifit = new BenefitsCoordinationRequestType();

                PayerIDType payer_Identification = new PayerIDType();
                List<ItemsChoiceType3> payerIdentification_ch = new List<ItemsChoiceType3>();
                payerIdentification_ch.Add(ItemsChoiceType3.PayerID);
                string[] arr2 = new string[] { HxArgs.PayerID };
                payer_Identification.Items = arr2;
                payer_Identification.ItemsElementName = payerIdentification_ch.ToArray<ItemsChoiceType3>();

                benifit.PayerName = HxArgs.PayerName;
                benifit.PBMMemberID = HxArgs.PBMMemberID;
                benifit.Consent = request.Consent;
                benifit.PayerIdentification = payer_Identification;

                List<ItemsChoiceType5> benefitsItemElementname = new List<ItemsChoiceType5>();
                benefitsItemElementname.Add(ItemsChoiceType5.EffectiveDate);
                benefitsItemElementname.Add(ItemsChoiceType5.ExpirationDate);

                List<ItemChoiceType> dateElement = new List<ItemChoiceType>();
                dateElement.Add(ItemChoiceType.Date);
                dateElement.Add(ItemChoiceType.Date);
                List<DateType> ar = new List<DateType>();
                if (HxArgs.EffectiveDate.HasValue && HxArgs.ExpirationDate.HasValue)
                {
                    DateType date_type = null;
                    int cnt = 0;
                    foreach (var item in dateElement)
                    {
                        date_type = new DateType();
                        if (cnt == 0)
                        {
                            date_type.ItemElementName = item;
                            date_type.Item = HxArgs.EffectiveDate.Value;
                            ar.Add(date_type);
                            cnt++;
                        }
                        else
                        {
                            date_type.ItemElementName = item;
                            date_type.Item = HxArgs.ExpirationDate.Value;
                            ar.Add(date_type);
                        }
                    }
                    benifit.Items = ar.ToArray<DateType>();
                    benifit.ItemsElementName = benefitsItemElementname.ToArray<ItemsChoiceType5>();
                }

                benifits.Add(benifit);

                return benifits.ToArray<BenefitsCoordinationRequestType>();
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
            }

        }

        private OptionalPrescriberType GetMedHxRequestPrescriber(MedHxRequest request)
        {
            try
            {
                Prescriber ProviderArgs = request.ProviderInfo;

                OptionalPrescriberType prescriber = new OptionalPrescriberType();
                MandatoryProviderIDType prescriber_identification = new MandatoryProviderIDType();

                string[] npi_item_value = null;//= new string[] { "1705933483" };//1234567890

                if (ProviderArgs.NPI != "")
                { npi_item_value = new string[] { ProviderArgs.NPI }; }
                else
                { throw new Exception("Invalid medication history request will be posted since the provider does not have either DEA or NPI information"); }

                System.Collections.Generic.List<ItemsChoiceType> npi_item_enum = new System.Collections.Generic.List<ItemsChoiceType>();
                npi_item_enum.Add(ItemsChoiceType.NPI);

                prescriber.Name = new MandatoryNameType();


                prescriber_identification.Items = npi_item_value;
                prescriber_identification.ItemsElementName = npi_item_enum.ToArray<ItemsChoiceType>();

                if (ProviderArgs.LastName.ToString().Length > 35)
                { prescriber.Name.LastName = ProviderArgs.LastName.ToString().Substring(0, 35); }
                else
                { prescriber.Name.LastName = ProviderArgs.LastName; }

                if (ProviderArgs.FirstName.ToString().Length > 35)
                { prescriber.Name.FirstName = ProviderArgs.FirstName.ToString().Substring(0, 35); }
                else
                { prescriber.Name.FirstName = ProviderArgs.FirstName; }

                prescriber.Identification = prescriber_identification;

                return prescriber;
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
            }
        }

        private HistoryRequestPatientType GetMedHxRequestPatient(MedHxRequest request)
        {
            try
            {
                Patient PatientArgs = request.PatientInfo;

                HistoryRequestPatientType patient = new HistoryRequestPatientType();

                patient.PatientRelationship = "1";
                patient.Name = new MandatoryPatientNameType();

                if (PatientArgs.LastName.ToString().Length > 35)
                { patient.Name.LastName = PatientArgs.LastName.ToString().Substring(0, 35); }
                else
                { patient.Name.LastName = PatientArgs.LastName; }

                if (PatientArgs.FirstName.ToString().Length > 35)
                { patient.Name.FirstName = PatientArgs.FirstName.ToString().Substring(0, 35); }
                else
                { patient.Name.FirstName = PatientArgs.FirstName; }

                if (PatientArgs.Gender == "Male")
                { patient.Gender = "M"; }
                else
                {
                    if (PatientArgs.Gender == "Female")
                    { patient.Gender = "F"; }
                    else
                    { patient.Gender = "U"; }
                }

                //TODO : Check the logic
                DateType date_type = new DateType();
                List<ItemChoiceType> ss = new List<ItemChoiceType>();
                ss.Add(ItemChoiceType.Date);
                DateTime day = PatientArgs.DateOfBirth; //new DateTime(1945, 2, 1);//oPatient.DOB.ToString("yyyy-MM-dd")
                date_type.Item = day.Date;
                date_type.ItemElementName = ss.First<ItemChoiceType>();

                patient.DateOfBirth = date_type;

                return patient;
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
            }
        }

        #endregion

        #region "WCF Service Classes"

        internal sealed class BindingFactory
        {
            private BindingFactory()
            {
            }

            static internal WSHttpBinding CreateInstance()
            {
                WSHttpBinding binding = new WSHttpBinding();
                try
                {
                    binding.Security.Mode = SecurityMode.Transport;
                    binding.ReliableSession.Enabled = false;
                    binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                    binding.UseDefaultWebProxy = true;
                    binding.OpenTimeout = new TimeSpan(0, 10, 0);
                    binding.CloseTimeout = new TimeSpan(0, 10, 0);
                    binding.SendTimeout = new TimeSpan(0, 10, 10);
                    binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                    binding.MaxBufferPoolSize = 99999999999999L;
                    binding.MaxReceivedMessageSize = 2147483647;


                    binding.ReaderQuotas.MaxArrayLength = 2147483647;
                    binding.ReaderQuotas.MaxDepth = 64;
                    binding.ReaderQuotas.MaxStringContentLength = 2147483647;

                    binding.ReaderQuotas.MaxBytesPerRead = 568556652;
                    binding.ReaderQuotas.MaxNameTableCharCount = 568556652;


                    return (binding);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if ((binding != null))
                    {
                        binding = null;
                    }
                }
            }
        }

        #endregion

        //TODO : Need to impletement
        public List<MedHxItem> GetMedHxResponses(Int64 RequestId)
        {
            DataTable dt = null;
            List<MedHxItem> medhx_list = new List<MedHxItem>();
            dt = GetGUIDfromRequestID(RequestId);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    List<MedHxItem> Tmpmedhx_list = new List<MedHxItem>();

                    Tmpmedhx_list = GetMedHxResponse((Guid)(item["MessageID"]));

                    foreach (MedHxItem itemelement in Tmpmedhx_list)
                    {
                        medhx_list.Add(itemelement);
                    }
                }

            }

            return medhx_list;
        }

        public List<MedHxItem> GetMedHxResponse(Guid id)
        {
            IEnumerable<XElement> list = GetResponseXML(id);
            List<MedHxItem> medhx_list = new List<MedHxItem>();

            foreach (XElement item in list)
            {
                MedHxItem medhx = new MedHxItem();

                if (item.Element("DrugDescription") != null)
                { medhx.DrugName = item.Element("DrugDescription").Value; }



                if (item.Element("DrugCoded") != null)
                {
                    if (item.Element("DrugCoded").Element("ProductCode") != null)
                    { medhx.NDCCode = item.Element("DrugCoded").Element("ProductCode").Value; }
                }
                if (item.Element("Quantity") != null)
                { medhx.DrugQty = Convert.ToInt32(item.Element("Quantity").Element("Value").Value); }

                if (item.Element("DaysSupply") != null)
                { medhx.DaySupply = Convert.ToInt32(item.Element("DaysSupply").Value); }

                if (item.Element("LastFillDate").Element("Date") != null)
                { medhx.StartDate = Convert.ToDateTime(item.Element("LastFillDate").Element("Date").Value); }

                if (item.Element("Refills") != null)
                {
                    if (item.Element("Refills").Element("Value") != null)
                    {
                        medhx.Refills = Convert.ToInt32(item.Element("Refills").Element("Value").Value); 
                    }
                }

                if (item.Element("Quantity").Element("PotencyUnitCode") != null)
                { medhx.PotencyCode = item.Element("Quantity").Element("PotencyUnitCode").Value; }

                if (item.Element("Pharmacy") != null)
                {
                    if (item.Element("Pharmacy").Element("StoreName") != null)
                    { medhx.Pharmacy = item.Element("Pharmacy").Element("StoreName").Value; }

                    if (item.Element("Pharmacy").Element("Identification").Element("NPI") != null)
                    { medhx.NPI = item.Element("Pharmacy").Element("Identification").Element("NPI").Value; }

                    if (item.Element("Pharmacy").Element("Identification").Element("NCPDPID") != null)
                    { medhx.NCPDPId = item.Element("Pharmacy").Element("Identification").Element("NCPDPID").Value; }

                    if (item.Element("Pharmacy").Element("Address") != null)
                    {
                        if (item.Element("Pharmacy").Element("Address").Element("AddressLine1") != null)
                        { medhx.PharmacyAddress1 = item.Element("Pharmacy").Element("Address").Element("AddressLine1").Value; }

                        if (item.Element("Pharmacy").Element("Address").Element("AddressLine2") != null)
                        { medhx.PharmacyAddress2 = item.Element("Pharmacy").Element("Address").Element("AddressLine2").Value; }

                        if (item.Element("Pharmacy").Element("Address").Element("City") != null)
                        { medhx.PharmacyCity = item.Element("Pharmacy").Element("Address").Element("City").Value; }

                        if (item.Element("Pharmacy").Element("Address").Element("State") != null)
                        { medhx.PharmacyState = item.Element("Pharmacy").Element("Address").Element("State").Value; }

                        if (item.Element("Pharmacy").Element("Address").Element("ZipCode") != null)
                        { medhx.PharmacyZip = item.Element("Pharmacy").Element("Address").Element("ZipCode").Value; }
                    }

                    if (item.Element("Pharmacy").Element("CommunicationNumbers") != null)
                    {
                        foreach (XElement comm in item.Element("Pharmacy").Elements("CommunicationNumbers"))
                        {
                            if (comm.Element("Communication").Element("Qualifier").Value == "TE")
                            {
                                medhx.PharmacyPhone = comm.Element("Communication").Element("Number").Value;
                            }
                        }
                    }
                }
                medhx.MedicationDate = DateTime.Now.Date;
                medhx_list.Add(medhx);
            }
            return medhx_list;
        }

        public IEnumerable<XElement> GetResponseXML(Guid id)
        {
            IEnumerable<XElement> medHxList = null;
            string responseXML = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("gsp_Medication_History10dot6Get", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@sMessageID", id));

                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                adp.Fill(dt);

                                if (dt != null || dt.Rows.Count > 0)
                                {
                                    string xmlnsPattern = "\\s+xmlns\\s*(:\\w)?\\s*=\\s*\\\"(?<url>[^\\\"]*)\\\"";

                                    responseXML = Convert.ToString(dt.Rows[0]["ResponseFile"]);

                                    MatchCollection matchcol = Regex.Matches(responseXML, xmlnsPattern);
                                    foreach (Match m in matchcol)
                                    {
                                        responseXML = responseXML.Replace(m.ToString(), "");
                                    }

                                    XDocument xdoc = XDocument.Parse(responseXML);


                                    IEnumerable<XElement> list_data = from s in xdoc.Descendants("MedicationDispensed")
                                                                      select new XElement(s);

                                    medHxList = list_data.ToList<XElement>();
                                }
                            }
                        }
                    }
                    con.Close();
                }

                return medHxList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRequestInfo(Guid id)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("gsp_Medication_HistoryRequest10dot6Get", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@sMessageID", id));

                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            adp.Fill(dt);
                        }
                    }
                    con.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetGUIDfromRequestID(Int64 id)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("gsp_GetGUID", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@nRequestID", id));

                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            adp.Fill(dt);
                        }
                    }
                    con.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string RemoveNamespace(string xml)
        {
            string xmlnsPattern = "\\s+xmlns\\s*(:\\w)?\\s*=\\s*\\\"(?<url>[^\\\"]*)\\\"";

            MatchCollection matchCol = Regex.Matches(xml, xmlnsPattern);
            foreach (Match m in matchCol)
            {
                xml = xml.Replace(m.ToString(), "");
            }
            return xml;
        }

        public String GetFinalXml(List<TempClass> list_res)
        {

            XElement xmlTree = new XElement("Root");

            foreach (var item in list_res)
            {
                XElement TempElement = item.meds;
                TempElement.Add(new XElement("pbmName", item.pbmName));
                xmlTree.Add(TempElement);
            }
            return xmlTree.ToString();
        }


        public List<TempClass> GetCompleteResponseXML(List<Guid> Ids)
        {
            List<TempClass> medHxList = new List<TempClass>();
            string responseXML = string.Empty;
            string pbm_name = string.Empty;

            try
            {
                foreach (Guid id in Ids)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("gsp_Medication_History10dot6Get", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@sMessageID", id));

                            using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                            {
                                using (DataTable dt = new DataTable())
                                {
                                    adp.Fill(dt);

                                    if (dt != null || dt.Rows.Count > 0)
                                    {
                                        string xmlnsPattern = "\\s+xmlns\\s*(:\\w)?\\s*=\\s*\\\"(?<url>[^\\\"]*)\\\"";

                                        responseXML = Convert.ToString(dt.Rows[0]["ResponseFile"]);
                                        pbm_name = Convert.ToString(dt.Rows[0]["PayerName"]);

                                        MatchCollection matchcol = Regex.Matches(responseXML, xmlnsPattern);
                                        foreach (Match m in matchcol)
                                        {
                                            responseXML = responseXML.Replace(m.ToString(), "");
                                        }

                                        XDocument xdoc = XDocument.Parse(responseXML);


                                        IEnumerable<TempClass> list_data = from s in xdoc.Descendants("MedicationDispensed")
                                                                           select new TempClass(s, pbm_name);

                                        medHxList.AddRange(list_data.ToList<TempClass>());
                                    }
                                }
                            }
                        }
                        con.Close();
                    }
                }

                return medHxList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetMedHxResponse(Int64 RequestID)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataAdapter adp = null;
            DataTable dtResponse = null;
            try
            {
                con = new SqlConnection(connectionString);
                cmd = new SqlCommand("gsp_GetResponseFiles", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nRequestID", RequestID));

                adp = new SqlDataAdapter(cmd);
                dtResponse = new DataTable();
                adp.Fill(dtResponse);

            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.MedicationHistory, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
            }

            finally
            {
                if (adp != null) { adp.Dispose(); adp = null; }
                if (cmd != null) { cmd.Parameters.Clear(); cmd.Dispose(); cmd = null; }
                if (con != null) { con.Dispose(); con = null; }
            }
            return dtResponse;
        }
        //public void LogFile(string TextToLog)
        //{
        //    System.IO.StreamWriter objFile = null;
        //    System.Text.StringBuilder strMessage = new System.Text.StringBuilder();
        //    try
        //    {
        //        if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Outbox"))
        //        {
        //            //create the Outbox directory, to save the 270 generated files.
        //            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "Outbox");
        //        }

        //        string requestFile = System.AppDomain.CurrentDomain.BaseDirectory + "Outbox\\\\" + "LogFile" + ".txt";

        //        objFile = new System.IO.StreamWriter(requestFile, true);
        //        strMessage.Append(Environment.NewLine);
        //        strMessage.Append(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + ", ");
        //        strMessage.Append(TextToLog);
        //        objFile.WriteLine(strMessage.ToString());
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    finally
        //    {
        //        if (objFile != null)
        //        {
        //            objFile.Close();
        //            objFile.Dispose();
        //            objFile = null;
        //        }
        //        if (strMessage != null)
        //        {
        //            strMessage.Remove(0, strMessage.Length);
        //            strMessage = null;
        //        }
        //    }
        //}
        public DataTable GetCompleteResponseXML(Int64 RequestID)
        {
            return GetMedHxResponse(RequestID);
        }
    }
     
    public class TempClass
    {
        public TempClass(XElement m, string pbm)
        {
            meds = m;
            pbmName = pbm;
        }

        public XElement meds { get; set; }
        public string pbmName { get; set; }
    }

    public class MedHistory
    {
        public MedHistory()
        { }

        public string DrugName { get; set; }
        public string Dosage { get; set; }
    }

}
