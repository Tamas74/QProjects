using System;
using System.Collections.Generic;
using System.Linq;
using PMP = gloGlobal.PDMP.XSD;
using PDMPMeds = gloGlobal.PDMP.Meds;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace gloRxHub.PDMP
{
    public class PDMP
    {
        public string ConnectionString { get; set; }
        public string WebURL { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public Dictionary<string, string> CustomHeaders { get; set; }

        public PDMP(string UserName, string Password)
        {
            try
            {
                this.CustomHeaders = new Dictionary<string, string>();
                
                this.CustomHeaders.Add("X-Auth", PDMP.EncryptionHelper.Encrypt(UserName) + ":" + PDMP.EncryptionHelper.Encrypt(Password));
            }
            catch (Exception ex)
            {                
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            
        }
        
        public string GetUniqueID()
        {
            try
            {
                DateTime dtdate = System.DateTime.UtcNow;
                string UniqueID = dtdate.Month.ToString() + "" + dtdate.Day.ToString() + "" + dtdate.Year.ToString() + dtdate.Hour.ToString() + dtdate.Minute.ToString() + dtdate.Second.ToString() + dtdate.Millisecond.ToString();
                return UniqueID;
            }
            catch (Exception ex) {
                throw ex;
            }
          
        }

        #region Patient Response

        public void InsertPatientResponse(Int64 PatientID, Int64 ProviderID, string xElement, string sLicenseeRequestId, string sReportURL, string sReportID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand("PDMP_InsertPatientResponse", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        comm.Parameters.AddWithValue("@nPatientID", PatientID);
                        comm.Parameters.AddWithValue("@nProviderID", ProviderID);
                        comm.Parameters.AddWithValue("@sContent", xElement.ToString());
                        comm.Parameters.AddWithValue("@sLicenseeRequestId", sLicenseeRequestId);
                        comm.Parameters.AddWithValue("@sReportLink", sReportURL);
                        comm.Parameters.AddWithValue("@sReportID", sReportID);
                        comm.Connection.Open();
                        comm.ExecuteNonQuery();
                        comm.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PatientRequest(Int64 PatientID, Int64 ProviderID)
        {
            XElement xElement;
            XElement xExpirationDate;
            XElement LicenseeRequestId;
            XElement xReportURL;
            XElement ViewableReportLink;
            XElement xReportResponse;

            try
            {
                string sResponse = this.GetPatientResponseString(PatientID, ProviderID);

                if (sResponse != null && sResponse.Any())
                {
                    xElement = XElement.Parse(sResponse);

                    xReportResponse = xElement.Elements().FirstOrDefault(p => p.Name.LocalName == "Report");

                    if (xReportResponse != null)
                    {
                        DateTime dtExpirationDate = DateTime.Now.AddDays(2);
                        string sReportURL = string.Empty;
                        string sReportID = string.Empty;
                        string sLicenseeRequestId = string.Empty;
                        xExpirationDate = xReportResponse.Elements().FirstOrDefault(p => p.Name.LocalName == "ReportExpiration");

                        LicenseeRequestId = xElement.Elements().FirstOrDefault(p => p.Name.LocalName == "LicenseeRequestId");

                        if (LicenseeRequestId != null)
                        {
                            sLicenseeRequestId = LicenseeRequestId.Value;
                        }

                        xReportURL = xReportResponse.Elements()
                            .FirstOrDefault(p => p.Name.LocalName == "ReportRequestURLs");


                        if (xReportURL != null)
                        {
                            ViewableReportLink = xReportURL.Elements().FirstOrDefault(p => p.Name.LocalName == "ViewableReport");

                            if (ViewableReportLink != null)
                            {
                                sReportURL = ViewableReportLink.Value;

                                string[] values = sReportURL.Split('/');
                                if (values != null)
                                {
                                    string sValue = values.LastOrDefault();

                                    if (sValue != null)
                                    {
                                        sReportID = sValue;
                                    }
                                }
                                Array.Clear(values, 0, values.Length);
                                values = null;
                            }
                        }

                        if (xExpirationDate != null)
                        {
                            string sExpirationDate = xExpirationDate.Value;
                            DateTime.TryParse(sExpirationDate, out dtExpirationDate);
                        }

                        InsertPatientResponse(PatientID, ProviderID, xElement.ToString(), sLicenseeRequestId, sReportURL, sReportID);
                    }
                    else
                    {
                        XElement xErrorResponse = xElement.Elements().FirstOrDefault(p => p.Name.LocalName == "Error");

                        if (xErrorResponse != null)
                        {
                            InsertPatientResponse(PatientID, ProviderID, xElement.ToString(), "", "", null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                xElement = null;
                xExpirationDate = null;
                LicenseeRequestId = null;
                xReportURL = null;
                ViewableReportLink = null;
                ViewableReportLink = null;
                xReportResponse = null;
            }
        }

        private PMP.PatientRequestType GetPatientRequestType(Int64 PatientID, Int64 ProviderID)
        {
            PMP.PatientRequestType p = null;
            DataSet ds = new DataSet();

            try
            {
                ds = this.FillPatientRequest(PatientID, ProviderID);

                p = new PMP.PatientRequestType();
                p.Requester = new PMP.RequesterType()
                {
                    LicenseeRequestId = this.GetUniqueID(),
                    Provider = this.GetProvider(ds.Tables["Provider"]),
                    Location = this.GetLocation(ds.Tables["Location"]),
                };

                p.PrescriptionRequest = this.GetPrescriptionRequest(ds.Tables["Patient"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return p;
        }

        private string GetPatientResponseString(Int64 PatientID, Int64 ProviderID)
        {
            string sFormData = "";
            string responseString = "";
                        
            gloGlobal.Common.BaseServiceHelper<string> service = new gloGlobal.Common.BaseServiceHelper<string>(WebURL + "ReportRequest/RequestView") { JsonType = true, ContentType = true };

            try
            {
                PMP.PatientRequestType patientRequest = null;
                patientRequest = this.GeneratePatientRequest(PatientID, ProviderID);
                sFormData = JsonConvert.SerializeObject(patientRequest);
                
                responseString = service.GetResponse(sFormData, CustomHeaders);               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseString;
        }

        #endregion

        #region Get Data

        private PMP.PatientRequestTypePrescriptionRequest GetPrescriptionRequest(DataTable dt)
        {
            PMP.PatientRequestTypePrescriptionRequest returned = new PMP.PatientRequestTypePrescriptionRequest();

            try
            {
                returned.ContinuityOfCare = null;
                returned.Coverage = null;
                returned.DiagnosisCodes = null;
                returned.DateRange = null;
                returned.RxCodes = null;
                returned.Patient = this.GetPatient(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returned;
        }

        private PMP.PatientType GetPatient(DataTable dt)
        {
            PMP.PatientType returnedPatient = new PMP.PatientType();

            try
            {                
                DateTime dtDOB = DateTime.MinValue;
                string sDOB = Convert.ToString(dt.Rows[0]["dtDOB"]);

                PMP.SexCodeType sexCode = PMP.SexCodeType.U;
                string sSexCode = Convert.ToString(dt.Rows[0]["sGender"]);

                if (Enum.TryParse(sSexCode.Substring(0, 1), out sexCode))
                {
                    returnedPatient.SexCode = sexCode;
                    returnedPatient.SexCodeSpecified = true;
                }
                else
                {
                    returnedPatient.SexCodeSpecified = false;
                }

                if (DateTime.TryParse(sDOB, out dtDOB))
                {
                    returnedPatient.Birthdate = dtDOB;
                }
                returnedPatient.Name = new PMP.PatientTypeName()
                {
                    First = Convert.ToString(dt.Rows[0]["sFirstName"]),
                    Middle = Convert.ToString(dt.Rows[0]["sMiddleName"]),
                    Last = Convert.ToString(dt.Rows[0]["sLastName"])
                };

                if (Convert.ToString(dt.Rows[0]["sAddressLine1"]) != null)
                {
                    string sAddressLine1 = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                    string sAddressLine2 = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                    string sCity = Convert.ToString(dt.Rows[0]["sCity"]);
                    string sState = Convert.ToString(dt.Rows[0]["sState"]);
                    string sZIP = Convert.ToString(dt.Rows[0]["sZIP"]);


                    if (sAddressLine1 != null && sAddressLine1.Any())
                    {
                        PMP.AddressRequiredZipType address = new PMP.AddressRequiredZipType();

                        List<string> lstAddressValues = new List<string>();

                        lstAddressValues.Add(sAddressLine1);

                        if (sAddressLine2 != null && sAddressLine2.Any()) { lstAddressValues.Add(sAddressLine2); }
                        if (sCity != null && sCity.Any()) { address.City = sCity; }

                        if (sState != null && sState.Any())
                        {
                            PMP.USStateCodeType stateCode = PMP.USStateCodeType.AA;
                            if (Enum.TryParse(sState, out stateCode)) { address.StateCode = stateCode; }
                       }

                        if (sZIP != null && sZIP.Any()) { address.ZipCode = sZIP; }

                        address.Street = lstAddressValues.ToArray();                       
                        returnedPatient.addressRequiredZipType = address;
                    }                    
                    sAddressLine1 = null;
                }


                if (Convert.ToString(dt.Rows[0]["sPhone"]) != null)
                {
                    string sPhone = Convert.ToString(dt.Rows[0]["sPhone"]);

                    if (sPhone.Any())
                    {
                        returnedPatient.phone = sPhone;
                    }                    
                }

                returnedPatient.MedicalRecordID = Convert.ToString(dt.Rows[0]["nPatientID"]);
                returnedPatient.DriversLicenseIdentifier = null;
                returnedPatient.MedicalBenefitsMemberID = null;                
                returnedPatient.PharmacyBenefitsMemberID = null;
                returnedPatient.SSN = null;
                returnedPatient.VeterinaryPrescription = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnedPatient;
        }

        private PMP.USStateCodeType GetStateCode(string LocationCode)
        {
            PMP.USStateCodeType returned = PMP.USStateCodeType.AA;

            try
            {
                Enum.TryParse(LocationCode, out returned);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returned;
        }

        private PMP.ProviderType GetProvider(DataTable dt)
        {
            PMP.ProviderType returned = new PMP.ProviderType();

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    string sLocationState = Convert.ToString(dt.Rows[0]["sState"]);
                    PMP.USStateCodeType stateCode = PMP.USStateCodeType.AA;

                    if (Enum.TryParse(sLocationState, out stateCode))
                    {
                        returned.LocationStateCode = stateCode;
                        returned.LocationStateCodeSpecified = true;
                    }

                    returned.FirstName = Convert.ToString(dt.Rows[0]["sFirstName"]);
                    returned.LastName = Convert.ToString(dt.Rows[0]["sLastName"]);

                    returned.NPINumber = Convert.ToString(dt.Rows[0]["sNPI"]);
                    returned.DEANumber = Convert.ToString(dt.Rows[0]["sDEA"]);

                    returned.ProfessionalLicenseNumber = null;
                    returned.Role = PMP.RoleType.Physician;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returned;
        }

        public PMP.PatientRequestType GeneratePatientRequest(Int64 PatientID, Int64 ProviderID)
        {
            PMP.PatientRequestType p = null;
            DataSet ds = new DataSet();

            try
            {
                ds = this.FillPatientRequest(PatientID, ProviderID);
                p = new PMP.PatientRequestType();
                p.Requester = new PMP.RequesterType()
                {
                    LicenseeRequestId = this.GetUniqueID(),
                    Provider = this.GetProvider(ds.Tables["Provider"]),
                    Location = this.GetLocation(ds.Tables["Location"]),
                };

                p.PrescriptionRequest = this.GetPrescriptionRequest(ds.Tables["Patient"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return p;
        }

        private PMP.LocationType GetLocation(DataTable dt)
        {
            PMP.LocationType locationType = new PMP.LocationType();

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<string> streetList = new List<string>();

                    if (Convert.ToString(dt.Rows[0]["sStreet"]) != "")
                    {
                        streetList.Add(Convert.ToString(dt.Rows[0]["sStreet"]));
                    }

                    locationType.Address = new PMP.LocationTypeAddress();
                    locationType.Address.City = Convert.ToString(dt.Rows[0]["sCity"]);
                    locationType.Address.StateCode = this.GetStateCode(Convert.ToString(dt.Rows[0]["sState"]));

                    if (streetList.Any())
                    { locationType.Address.Street = streetList.ToArray(); }

                    locationType.Address.ZipCode = Convert.ToString(dt.Rows[0]["sZIP"]);

                    locationType.DEANumber = null;
                    locationType.Name = Convert.ToString(dt.Rows[0]["sClinicName"]);
                    locationType.NCPDPNumber = null;

                    if (Convert.ToString(dt.Rows[0]["sClinicNPI"]) != "")
                    { locationType.NPINumber = Convert.ToString(dt.Rows[0]["sClinicNPI"]); }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return locationType;
        }

        #endregion
        
        #region Report Response

        public void ReportRequest(Int64 PatientID, Int64 ProviderID, Int64 ResponseID, string ReportID)
        {
            XElement xElement;
            XElement reportrequestid;
            XElement reportrequesturl;

            string reportrequestURL = "";
            string sFormData = "";
            string responseString = "";            

            try
            {                                
                gloGlobal.Common.BaseServiceHelper<string> service = new gloGlobal.Common.BaseServiceHelper<string>(WebURL + "ReportRequest/ReportView") { JsonType = true, ContentType = true };

                sFormData = this.GetReportRequestString(ProviderID, ReportID, WebURL + "ReportRequest/ReportView");

                responseString = service.GetResponse(sFormData, CustomHeaders);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPPost, gloAuditTrail.ActivityType.Send, "PDMP Report Request sent", PatientID, Convert.ToInt64(ReportID), ProviderID, gloAuditTrail.ActivityOutCome.Success);

                xElement = XElement.Parse(responseString);

                if (xElement != null)
                {
                    reportrequestid = xElement.Elements().FirstOrDefault(p => p.Name.LocalName == "ReportRequestId");
                    reportrequesturl = xElement.Elements().FirstOrDefault(p => p.Name.LocalName == "ReportLink");
                    if (reportrequesturl != null)
                    {
                        reportrequestURL = reportrequesturl.Value;
                        this.InsertReportResponse(PatientID, ProviderID, ResponseID, sFormData, xElement, reportrequestURL);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xElement = null;
                reportrequestid = null;
                reportrequesturl = null;
            }
        }

        private PMP.ReportRequestType GetReportRequest(Int64 ProviderID, string URL)
        {
            PMP.ReportRequestType reportRequest = null;

            try
            {
                reportRequest = new PMP.ReportRequestType() { Requester = new PMP.ReportRequestTypeRequester() };

                using (DataSet ds = this.FillPatientRequest(0, ProviderID))
                {
                    reportRequest.Requester.Provider = JsonConvert.DeserializeObject<PMP.ProviderTypeForReports>(JsonConvert.SerializeObject(this.GetProvider(ds.Tables["Provider"])));
                    reportRequest.Requester.Location = this.GetLocation(ds.Tables["Location"]);
                    reportRequest.Requester.ReportLink = URL;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reportRequest;
        }

        private string GetReportRequestString(Int64 ProviderID, string ReportID, string ReportURL)
        {
            string sFormData = "";

            PMP.ReportRequestType reportrequestype = null;
            PDMPMeds.PDMPReportRequest reportreq = null;

            try
            {
                reportrequestype = new PMP.ReportRequestType();
                reportrequestype = this.GetReportRequest(ProviderID, ReportURL);
                reportreq = new PDMPMeds.PDMPReportRequest();
                reportreq.ReportID = ReportID;
                reportreq.ReportRequest = reportrequestype;
                sFormData = JsonConvert.SerializeObject(reportreq);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reportreq != null)
                {
                    reportreq.Dispose();
                }

                reportrequestype = null;
                reportreq = null;
            }
            return sFormData;
        }
      
        #endregion

        #region Database functions

        private void InsertReportResponse(Int64 PatientID, Int64 ProviderID, Int64 ResponseID, string sFormData, XElement xElement, string reportrequestURL)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand("PDMP_InsertReportResponse", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        comm.Parameters.AddWithValue("@nPatientID", PatientID);
                        comm.Parameters.AddWithValue("@nProviderID", ProviderID);
                        comm.Parameters.AddWithValue("@nResponseID", ResponseID);
                        comm.Parameters.AddWithValue("@sRequestContent", sFormData);
                        comm.Parameters.AddWithValue("@sResponseContent", xElement.ToString());
                        comm.Parameters.AddWithValue("@sViewableURL", reportrequestURL);
                        comm.Connection.Open();
                        comm.ExecuteNonQuery();
                        comm.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable FillReportResponse(Int64 PatientID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand("PDMP_GetReportResponse", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        comm.Parameters.AddWithValue("@nPatientID", PatientID);

                        using (SqlDataAdapter da = new SqlDataAdapter(comm))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        private DataSet FillPatientRequest(Int64 PatientID, Int64 ProviderID)
        {

            DataSet ds = new DataSet();

            try
            {
                string sQuery = "PDMP_GetRequest";

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand(sQuery, conn) { CommandType = CommandType.StoredProcedure })
                    {
                        comm.Parameters.AddWithValue("@nPatientID", PatientID);
                        comm.Parameters.AddWithValue("@nProviderID", ProviderID);
                        using (SqlDataAdapter da = new SqlDataAdapter(comm))
                        {
                            da.Fill(ds);
                        }
                    }
                }
                ds.Tables[0].TableName = "Provider";
                ds.Tables[1].TableName = "Location";
                ds.Tables[2].TableName = "Patient";

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;


        }
        
        public DataTable GetViewableURL(Int64 PatientID)
        {
            DataTable dtReturned = new DataTable();
           
            try
            {
                string sQuery = "PDMP_GetViewableURL";

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand(sQuery, conn) { CommandType = CommandType.StoredProcedure })
                    {
                        comm.Parameters.AddWithValue("@PatientID", PatientID);
                        using (SqlDataAdapter da = new SqlDataAdapter(comm))
                        {
                            da.Fill(dtReturned);
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtReturned;
        }

        public DataTable GetReportFromDB(Int64 PatientID, Int64 ProviderID)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = this.FillReportResponse(PatientID);
                if (dt.Rows.Count == 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PDMP, gloAuditTrail.ActivityCategory.PDMPPost, gloAuditTrail.ActivityType.Send, "Sending PDMP Patient Request", PatientID, 0, ProviderID, gloAuditTrail.ActivityOutCome.Success);
                    this.PatientRequest(PatientID, ProviderID);                    
                    dt = this.FillReportResponse(PatientID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public void UpdateViewedURL(Int64 ReportID)
        {
            try
            {
                string sQuery = "PDMP_UpdateViewedURL";

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand(sQuery, conn) { CommandType = CommandType.StoredProcedure })
                    {
                        comm.Parameters.AddWithValue("@nReportID", ReportID);
                        comm.Connection.Open();
                        comm.ExecuteNonQuery();
                        comm.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }       
        }

        public DataTable GetPdmpPrograms(Int64 PatientID, Int64 ProviderID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand("PDMP_GetPdmprograms", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        comm.Parameters.AddWithValue("@nPatientID", PatientID);
                        comm.Parameters.AddWithValue("@nProviderID", ProviderID);
                        comm.Parameters.AddWithValue("@nflag", 1);
                        using (SqlDataAdapter da = new SqlDataAdapter(comm))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public DataTable GetNarcoticsHTML(Int64 ReportID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand("PDMP_GetPdmprograms", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        comm.Parameters.AddWithValue("@nReportID", ReportID);
                        comm.Parameters.AddWithValue("@nflag", 2);
                        using (SqlDataAdapter da = new SqlDataAdapter(comm))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public int UpdateReportHTML(Int64 nReportID, string html)
        {
            int icnt = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand comm = new SqlCommand("PDMP_InsertReport", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        comm.Parameters.AddWithValue("@imgContent", html);
                        comm.Parameters.AddWithValue("@nReportID", nReportID);
                        comm.Connection.Open();
                        icnt = comm.ExecuteNonQuery();
                        comm.Connection.Close();
                      
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return icnt;
        }
        #endregion

        public static class EncryptionHelper
        {
            public static string Encrypt(string clearText)
            {
                string EncryptionKey = "abc123";
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return clearText;
            }

            //public static string Decrypt(string cipherText)
            //{
            //    string EncryptionKey = "abc123";
            //    cipherText = cipherText.Replace(" ", "+");
            //    byte[] cipherBytes = Convert.FromBase64String(cipherText);
            //    using (Aes encryptor = Aes.Create())
            //    {
            //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            //        encryptor.Key = pdb.GetBytes(32);
            //        encryptor.IV = pdb.GetBytes(16);
            //        using (MemoryStream ms = new MemoryStream())
            //        {
            //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            //            {
            //                cs.Write(cipherBytes, 0, cipherBytes.Length);
            //                cs.Close();
            //            }
            //            cipherText = Encoding.Unicode.GetString(ms.ToArray());
            //        }
            //    }
            //    return cipherText;
            //}
        }

    }

  
}

