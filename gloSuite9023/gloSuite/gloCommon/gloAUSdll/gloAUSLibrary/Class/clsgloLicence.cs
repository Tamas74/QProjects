using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace gloAUSLibrary.Class
{
    public static class clsgloLicenseParameters
    {
        public static Boolean IsLicenseValid = false;
        public static string sProviderLicenseIDs = "";
    }
    public class clsgloLicence : IDisposable
    {
        private string MasterSecrateKey = "triarq@1981!";
        private string ClientSecrateKey = "thealth$2014@";
        public string AUSServicePortalURL{ get; set; } 
        public string ConnectionString{ get; set; }
     
        //public enum enmAUSStatus
        //{
        //    Active = 0,
        //    PendingForLicense = 1,
        //    PendingForReview = 2,
        //    ReviewForServices = 3
        //}
        #region Constructor and Destructor
        public clsgloLicence(string AUSPortalURL, string _sConnectionString)
        {
            AUSServicePortalURL = AUSPortalURL;
            ConnectionString = _sConnectionString;
        }

        private bool disposed = false;
        private const string gstrMessageBoxCaption = "AUS License";
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~clsgloLicence()
        {
            Dispose(false);
        }
        #endregion

        public string GetLicensedServiceLevel(string sLicenceKey)
        {
            string sServiceLevel = string.Empty;
            string AusId = string.Empty;
            string nID = string.Empty;
            if (sLicenceKey != "")
            {
                try
                {
                    using (clsEncryption oDecrypt = new clsEncryption())
                    {
                        // oDecrypt.DecryptFromBase64String(sLicenceKey, "");
                        string[] stemp = null;
                        stemp = oDecrypt.DecryptFromBase64String(sLicenceKey, MasterSecrateKey).Split('|');
                        if (stemp.Length >= 3)
                        {
                            AusId = oDecrypt.DecryptFromBase64String(Convert.ToString(stemp[0]), MasterSecrateKey);
                            nID = oDecrypt.DecryptFromBase64String(Convert.ToString(stemp[1]), MasterSecrateKey);
                            sServiceLevel = oDecrypt.DecryptFromBase64String(Convert.ToString(stemp[2]), ClientSecrateKey);
                        }
                        if (sServiceLevel.Length != 16) { sServiceLevel = ""; }
                    }
                }
                catch (Exception)
                {
                   // gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return string.Empty;
                }
            }
            return sServiceLevel;
        }
        public bool getLicensedPDRFlag(string sLicenceKey)
        {            
            bool bPDR = false;
            if (sLicenceKey != "")
            {
                try
                {
                    using (clsEncryption oDecrypt = new clsEncryption())
                    {
                        // oDecrypt.DecryptFromBase64String(sLicenceKey, "");
                        string[] stemp = null;
                        stemp = oDecrypt.DecryptFromBase64String(sLicenceKey, MasterSecrateKey).Split('|');
                        if (stemp.Length > 3)
                        {                           
                           string str  = oDecrypt.DecryptFromBase64String(Convert.ToString(stemp[3]), ClientSecrateKey).Trim();
                           if (str == "1") { bPDR = true; } else { bPDR = false; }
                        }
                        
                    }
                }
                catch (Exception)
                {
                    // gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return false;
                }
            }
            return bPDR;
        }

        public bool getLicensedPDMPFlag(string sLicenceKey)
        {
            bool bPDMP = false;
            if (sLicenceKey != "")
            {
                try
                {
                    using (clsEncryption oDecrypt = new clsEncryption())
                    {
                        // oDecrypt.DecryptFromBase64String(sLicenceKey, "");
                        string[] stemp = null;
                        stemp = oDecrypt.DecryptFromBase64String(sLicenceKey, MasterSecrateKey).Split('|');
                        if (stemp.Length > 3)
                        {
                            string str = oDecrypt.DecryptFromBase64String(Convert.ToString(stemp[4]), ClientSecrateKey).Trim();
                            if (str == "1") { bPDMP = true; } else { bPDMP = false; }
                        }

                    }
                }
                catch (Exception)
                {
                    // gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    return false;
                }
            }
            return bPDMP;
        }

        public Int64 GetAUSPortalID(string sLicenceKey)
        {
            Int64 nAUSPortalID = 0;
          
            if (sLicenceKey != "")
            {
                try
                {
                    using (clsEncryption oDecrypt = new clsEncryption())
                    {
                        // oDecrypt.DecryptFromBase64String(sLicenceKey, "");
                        string[] stemp = null;
                        stemp = oDecrypt.DecryptFromBase64String(sLicenceKey, MasterSecrateKey).Split('|');
                        if (stemp.Length >= 3)
                        {
                            nAUSPortalID = Convert.ToInt64(oDecrypt.DecryptFromBase64String(Convert.ToString(stemp[1]), MasterSecrateKey));                           
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    return 0;
                    throw ex;                   
                }
            }
            return nAUSPortalID;
        }
        public bool IsDemoNPI(string sNPI, string gsDemoNPIs)
        {
            bool isValid = false;
            try
            {
                using (clsEncryption objEncryption = new clsEncryption())
                {
                    string sTempNpis = objEncryption.DecryptFromBase64String(gsDemoNPIs, "triarq@1981!");
                    string[] NPIs = sTempNpis.Split(',');
                    for (int i = 0; i < NPIs.Length; i++)
                    {
                        if (NPIs[i].Trim() == sNPI.Trim())
                        {
                            isValid = true;
                            break;
                        }

                    }
                }
            }
            catch (Exception)
            {
                isValid = false;
                // throw;
            }
            return isValid;
        }

        public string ValidateLicenseKey(string strLicensekey, string strFirstName, string strMiddleName, string strLastName, string ClinicExternalCode, Int64 ProviderID, Int64 AUSPortalID, DataTable dtLicenceData=null)
        {
            string message = string.Empty;
            
            try
            {
                if (AUSServicePortalURL != "")
                {
                    if (dtLicenceData != null)
                    {
                        if (dtLicenceData.Rows.Count > 0)
                        {
                            strLicensekey = Convert.ToString(dtLicenceData.Rows[0]["LicenseCode"]).Trim();
                            strFirstName = Convert.ToString(dtLicenceData.Rows[0]["sFirstName"]).Trim();
                            strMiddleName = Convert.ToString(dtLicenceData.Rows[0]["sMiddleName"]).Trim();
                            strLastName = Convert.ToString(dtLicenceData.Rows[0]["sLastName"]).Trim();
                            ProviderID = Convert.ToInt64(dtLicenceData.Rows[0]["nProviderID"]);
                            if (strLicensekey.Trim() != "")
                            {
                                if (CheckLicenseIsInUse(strLicensekey, ProviderID) == true)
                                {
                                    message = "License Key already in use.";
                                    return message;
                                } 
                            }
                        }
                    }
                    if (strLicensekey.ToUpper().Trim() != "DEMO LICENSE")
                    {
                        using (AUSLicenseService.gloAUSService ausService = new AUSLicenseService.gloAUSService())
                        {
                            ausService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                            ausService.Url = AUSServicePortalURL;
                            string _Key = ausService.Login("sarika@ophit.net", "spX12ss@!!21nasik");
                            message = ausService.ValidateLicenseKey(strLicensekey, strFirstName, strMiddleName, strLastName, ClinicExternalCode, ProviderID, AUSPortalID, _Key);
                        }
                    }
                    else
                    {
                        clsgloLicenseParameters.IsLicenseValid = true;
                        message = "ok";
                    }
                }
                else
                {
                    MessageBox.Show("AUS Portal URL Not set in gloEMRAdmin settings.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ////gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                message = "";
                // throw;
            }
            return message;
        }

        public void CheckifChanged(Control ctrl)
        {

            if (((TextBox)ctrl).Modified)
            {
                string strOut = null;

                strOut = string.Format("{0} is changed", ((TextBox)ctrl).Name);

                MessageBox.Show(strOut);

            }
        }

        public Boolean CheckLicenseIsInUse(string strLicenseKey, Int64 nProviderID = 0)
        {
            bool bResult = true;

            try
            {
                SqlConnection objCon = new SqlConnection(ConnectionString);
                SqlCommand objCmd = new SqlCommand();
                SqlParameter objParam = new SqlParameter();
                Int32 nCount;               

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "gsp_CheckLicenseIsInUse";

                objParam.ParameterName = "@LicenseKey";
                objParam.Value = strLicenseKey;
                objParam.Direction = ParameterDirection.Input;
                objParam.SqlDbType = SqlDbType.VarChar;
                objCmd.Parameters.Add(objParam);
                

                if (nProviderID != 0)
                {
                    objParam = new SqlParameter();
                    objParam.ParameterName = "@ProviderID";
                    objParam.Value = nProviderID;
                    objParam.Direction = ParameterDirection.Input;
                    objParam.SqlDbType = SqlDbType.BigInt;
                    objCmd.Parameters.Add(objParam);
                }

                objCmd.Connection = objCon;
                objCon.Open();
                nCount = Convert.ToInt32(objCmd.ExecuteScalar());
                
                
                objCon.Close();
                objCon.Dispose();
                objCon = null;
                objCmd.Parameters.Clear();
                objCmd.Dispose();
                objCmd = null;
                objParam = null;

                if (nCount == 0)
                {
                    bResult = false;
                }
                else
                {
                    bResult = true;
                }              
            }
            catch (Exception)
            {
                //  throw;
                return bResult;
            }
            finally
            {
              
            }

            return bResult;

        }
        public Boolean UpdateProviderLicenseStatus(Int64 nProviderID, int nAusstatus,string slicensekey)
        {
            bool bResult = false;

            try
            {
                SqlConnection objCon = new SqlConnection(ConnectionString);
                SqlCommand objCmd = new SqlCommand();
                SqlParameter objParam = new SqlParameter();
               
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "gsp_UpdateLicenseStatus";

                    objParam = new SqlParameter();
                    objParam.ParameterName = "@ProviderID";
                    objParam.Value = nProviderID;
                    objParam.Direction = ParameterDirection.Input;
                    objParam.SqlDbType = SqlDbType.BigInt;
                    objCmd.Parameters.Add(objParam);

                    objParam = new SqlParameter();
                    objParam.ParameterName = "@AUSStatus";
                    objParam.Value = nAusstatus;
                    objParam.Direction = ParameterDirection.Input;
                    objParam.SqlDbType = SqlDbType.Int;
                    objCmd.Parameters.Add(objParam);

                    objParam = new SqlParameter();
                    objParam.ParameterName = "@LicenceKey";
                    objParam.Value = slicensekey;
                    objParam.Direction = ParameterDirection.Input;
                    objParam.SqlDbType = SqlDbType.VarChar;
                    objCmd.Parameters.Add(objParam);
                
                objCmd.Connection = objCon;
                objCon.Open();
                objCmd.ExecuteNonQuery();
                bResult = true;

                objCon.Close();
                objCon.Dispose();
                objCon = null;
                objCmd.Parameters.Clear();
                objCmd.Dispose();
                objCmd = null;
                objParam = null;

            }
            catch (Exception ex)
            {
                bResult = false;
                clsGeneral.UpdateLog("Error in UpdateProviderLicenseStatus :" + ex.Message.ToString());
            }
           
            return bResult;

        }
        private DataTable getProviderLicenseInfo(Int64 nProviderID)
        {
            DataTable dtResult = null;

            try
            {
                SqlConnection objCon = new SqlConnection(ConnectionString);
                SqlCommand objCmd = new SqlCommand();
                SqlParameter objParam = new SqlParameter();

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "CheckProviderLicenseDetails";

                objParam = new SqlParameter();
                objParam.ParameterName = "@nProviderID";
                objParam.Value = nProviderID;
                objParam.Direction = ParameterDirection.Input;
                objParam.SqlDbType = SqlDbType.BigInt;
                objCmd.Parameters.Add(objParam);


                objCmd.Connection = objCon;
                objCon.Open();
                SqlDataAdapter objDA = new SqlDataAdapter(objCmd);
                dtResult = new DataTable();
                objDA.Fill(dtResult);
                objDA.Dispose();
                objDA = null;
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count <= 0)
                    {
                        dtResult.Dispose();
                        dtResult = null;
                    }
                }
                objCon.Close();
                objCon.Dispose();
                objCon = null;
                objCmd.Parameters.Clear();
                objCmd.Dispose();
                objCmd = null;
                objParam = null;


            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error in GetProviderLicenseDetail :" + ex.Message.ToString());
            }
            finally
            {

            }

            return dtResult;
        }
        private DataTable GetProviderLicenseDetail(Int64 nProviderID)
        {
          DataTable dtResult = null;

            try
            {
                SqlConnection objCon = new SqlConnection(ConnectionString);
                SqlCommand objCmd = new SqlCommand();
                SqlParameter objParam = new SqlParameter();
               
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "GetProviderLicenseDetails";

                    objParam = new SqlParameter();
                    objParam.ParameterName = "@nProviderID";
                    objParam.Value = nProviderID;
                    objParam.Direction = ParameterDirection.Input;
                    objParam.SqlDbType = SqlDbType.BigInt;
                    objCmd.Parameters.Add(objParam);
               

                objCmd.Connection = objCon;
                objCon.Open();
                SqlDataAdapter objDA = new SqlDataAdapter(objCmd);               
                dtResult = new DataTable();
                objDA.Fill(dtResult);
                objDA.Dispose();
                objDA = null;
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count <= 0)
                    {
                        dtResult.Dispose();
                        dtResult = null;
                    }
                }
                objCon.Close();
                objCon.Dispose();
                objCon = null;
                objCmd.Parameters.Clear();
                objCmd.Dispose();
                objCmd = null;
                objParam = null;

                        
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error in GetProviderLicenseDetail :" + ex.Message.ToString());
            }
            finally
            {
              
            }

            return dtResult;
        }
        private string GetInvalidLicenseProviderlist()
        {
            string sResult = "";
            try
            {
                SqlConnection objCon = new SqlConnection(ConnectionString);
                SqlCommand objCmd = new SqlCommand();            

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "GetInvalidLicenseProviders";
                
                objCmd.Connection = objCon;
                objCon.Open();
                SqlDataAdapter objDA = new SqlDataAdapter(objCmd);
                using (DataTable dtResult = new DataTable())
                {
                    objDA.Fill(dtResult);
                    objDA.Dispose();
                    objDA = null;
                    if (dtResult != null)
                    {
                        if (dtResult.Rows.Count > 0)
                        {
                            sResult = string.Join(",", dtResult.Rows.OfType<DataRow>().Select(r => r[0].ToString()));
                        }
                    }
                }
                objCon.Close();
                objCon.Dispose();
                objCon = null;
                objCmd.Parameters.Clear();
                objCmd.Dispose();
                objCmd = null;
            
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error in GetInvalidLicenseProviderlist :" + ex.Message.ToString());
            }         

            return sResult;
        }
        private bool ToSkipLicense()
        {
            bool result = false;
            try
            {
                SqlConnection objCon = new SqlConnection(ConnectionString);
                SqlCommand objCmd = new SqlCommand();
                SqlParameter objParam = new SqlParameter();

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "gsp_GetSettings_License";


                objCmd.Connection = objCon;
                objCon.Open();
                SqlDataAdapter objDA = new SqlDataAdapter(objCmd);
                using (DataTable dtResult = new DataTable())
                {
                    objDA.Fill(dtResult);
                    objDA.Dispose();
                    objDA = null;
                    if (dtResult != null)
                    {
                        if (dtResult.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtResult.Rows[0][0]) == "1")
                            {
                                result = true;
                            }
                        }
                    }
                }
                objCon.Close();
                objCon.Dispose();
                objCon = null;
                objCmd.Parameters.Clear();
                objCmd.Dispose();
                objCmd = null;
                objParam = null;


            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error in ToSkipLicense :" + ex.Message.ToString());
            }           

            return result;
        }
        public bool SendProviderForApproval(Int64 providerid)
        {
            bool bResult = false;
            try
            {
                if (providerid > 0)
                {  
                    DataTable dtprovider = GetProviderLicenseDetail(providerid);
                    if (dtprovider != null)
                    {
                        bResult = SendProviderOnAUS(dtprovider);
                        dtprovider.Dispose();
                        dtprovider = null;
                    }
                }
                if (bResult == false)
                {
                    MessageBox.Show("Problem while sending provider data for approval", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
            }
            catch (Exception ex)
            {
                bResult = false;
                clsGeneral.UpdateLog("Error while sending provider data for approval :" + ex.Message.ToString());
            }
            return bResult;
        }
        public Byte[] ConvertFiletoBinary(string strFileName)
        {
            FileStream oFile = null;
            BinaryReader oReader = null;
            Byte[] bytesRead = null;
            try
            {
                //const int CHUNK_SIZE = 1024 * 8; //8K write buffer.

                if (File.Exists(strFileName))
                {
                    //'To read the file only when it is not in use by any process 
                    oFile = new FileStream(strFileName, FileMode.Open, FileAccess.Read);
                    using (oReader = new BinaryReader(oFile))
                    {
                        bytesRead = new Byte[oReader.BaseStream.Length];
                        oReader.Read(bytesRead, 0, bytesRead.Length);
                        oFile.Close();
                        oReader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error While reading file :" + ex.Message.ToString());
            }
            finally
            {
                if (oFile != null)
                {
                    oFile.Close();
                    oFile.Dispose();
                    oFile = null;
                }
                if (oReader != null)
                {
                    oReader.Close();
                    oReader.Dispose();
                    oReader = null;
                }
            }
            return bytesRead;

        }
        public string GetFileName(enumFileType eFType)
        {
            string strAppPath = Application.StartupPath + "\\Temp\\";
            string _NewDocumentName = null;
            string _Extension = null;
            string retFileNameValue = null;
            try
            {

                CreateDirectory(Application.StartupPath + "\\Temp\\", false);

                if (eFType == enumFileType.XMLFile)
                {
                    _Extension = ".xml";
                }
                else
                {
                    _Extension = ".zip";
                }

                DateTime _dtCurrentDateTime = System.DateTime.Now;

                int i = 0;
                _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") + _Extension;
                while (File.Exists(strAppPath + "\\" + _NewDocumentName) == true)
                {
                    i = i + 1;
                    _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") + "-" + i + _Extension;
                }
                if (!string.IsNullOrEmpty(_NewDocumentName))
                {
                    retFileNameValue = strAppPath + _NewDocumentName;
                }

            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error Creating a File Name. " + ex.Message.ToString() + "");
            }
            return retFileNameValue;
        } //GetFileName
        private static string CreateDirectory(string path, bool IsDeleteRepace)
        {
            try
            {
                if (IsDeleteRepace)
                {
                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                    }
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Unable to Delete and create Directory" + ex.ToString());
                path = "";
            }
            return path;
        }

        public bool SendProviderOnAUS(DataTable dtLicenceData)
        {
            bool message = false;
            byte[] _ResponseData = null;
            try
            {
                if (AUSServicePortalURL != "")
                {
                    using (AUSLicenseService.gloAUSService ausService = new AUSLicenseService.gloAUSService())
                    {
                        ausService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        ausService.Url = AUSServicePortalURL;
                        string _Key = ausService.Login("sarika@ophit.net", "spX12ss@!!21nasik");
                        _ResponseData = ReadDatatableToxmlFileBytes(dtLicenceData);
                        if (_ResponseData != null)
                        {
                            message = ausService.SendProviderLicenseData(_ResponseData, _Key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                message = false;
            }
            return message;
        }
        private Byte[] ReadDatatableToxmlFileBytes(DataTable dtData)
        {
            string strResponseXml = "";
            byte[] bResponseData = null;
            try
            {
                if (dtData != null)
                {
                    using (DataSet dsProvider = new DataSet())
                    {
                        if (dtData.Rows.Count > 0)
                        {
                            dsProvider.Tables.Add(dtData.Copy());
                        }
                        if (dsProvider != null)
                        {
                            if (dsProvider.Tables.Count > 0)
                            {
                                strResponseXml = clsGeneral.GetFileName(enumFileType.XMLFile);
                                dsProvider.WriteXml(strResponseXml);

                                if (strResponseXml != null)
                                    bResponseData = clsGeneral.ConvertFiletoBinary(strResponseXml);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);              
                bResponseData = null;
                
            }
            return bResponseData;
        }
        public string IsValidLicensedprovider(Int64 providerID, string sAction = "")
        {
            string result = string.Empty;
            DataTable dt = null;
            try
            {
                if (ToSkipLicense() == false)
                {
                    dt = GetProviderLicenseDetail(providerID);
                    if (dt != null)
                    {
                        if (Convert.ToString(dt.Rows[0]["LicenseCode"]).Trim() != "")
                        {
                            result = "";
                        }
                        else
                        {
                            if (sAction != "")
                            {
                                result = "The selected provider has not yet obtained a valid license from TRIARQ Health." + Environment.NewLine + "Are you sure you want to continue with restricted accesses?" + Environment.NewLine + "Please contact TRIARQ Health support to obtain a valid license.";
                            }
                            else
                            {
                                result = "The selected provider has not yet obtained a valid license from TRIARQ Health." + Environment.NewLine + " " + sAction + " is restricted.";
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = "";
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            return result;
        }
        public string getInValidLicensedproviders(out string sProviders,string sAction ="")
        {            
            string message = "";
            sProviders = "";
            try
            {
                if (ToSkipLicense() == false)
                {
                    sProviders = GetInvalidLicenseProviderlist();
                    if (sProviders != "")
                    {
                        if (sAction != "")
                        {
                            message = "Few providers have not yet obtained a valid license from TRIARQ Health." + Environment.NewLine + " Are you sure you want to continue with restricted accesses for those providers?" + Environment.NewLine + " Please contact TRIARQ Health support to obtain a valid license for them.";
                        }
                        else
                        {
                            message = "The selected provider has not yet obtained a valid license from TRIARQ Health." + Environment.NewLine + " " + sAction + " is restricted.";
                        }
                    }
                    else
                    {
                        clsgloLicenseParameters.IsLicenseValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                sProviders = "";
            }

            return message;
        }

        public string IsValidProviderLicense(Int64 providerID,out string sProviders)
        {
            string result = string.Empty;
            DataTable dt = null;
            sProviders = "";

            try
            {
                if (ToSkipLicense() == false)
                {
                    if (providerID != 0)
                    {
                        //for provider login
                        dt = getProviderLicenseInfo(providerID);
                        if (dt != null)
                        {
                            if (Convert.ToString(dt.Rows[0]["LicenseCode"]).Trim() == "")
                            {
                                result = "The selected provider has not yet obtained a valid license from TRIARQ Health." + Environment.NewLine + "Are you sure you want to continue with restricted accesses?" + Environment.NewLine + "Please contact TRIARQ Health support to obtain a valid license.";
                                clsgloLicenseParameters.IsLicenseValid = false;
                            }
                            else if (Convert.ToString(dt.Rows[0]["AUSStatus"]).Trim() == "3")
                            {
                                result = "The selected provider is disabled from TRIARQ Health." + Environment.NewLine + "Are you sure you want to continue with restricted accesses?" + Environment.NewLine + "Please contact TRIARQ Health support to enable the provider.";
                                clsgloLicenseParameters.IsLicenseValid = false;
                            }
                            else
                            {
                                clsgloLicenseParameters.IsLicenseValid = true;
                            }

                        }
                    }
                    else
                    {
                        //for non - provider login
                        clsgloLicenseParameters.IsLicenseValid = true;
                        sProviders = GetInvalidLicenseProviderlist();                        
                        if (sProviders != "")
                        {
                            result = "Few providers have not yet obtained a valid license or disabled from TRIARQ Health." + Environment.NewLine + " Are you sure you want to continue with restricted accesses for those providers?" + Environment.NewLine + "Please contact TRIARQ Health support to obtain a valid license for them or enable those providers.";
                            
                        }
                        //else
                        //{
                        //    clsgloLicenseParameters.IsLicenseValid = true;                          
                        //}
                    }

                }
                else
                {
                    // to skip licenses
                    clsgloLicenseParameters.IsLicenseValid = true;
                    sProviders = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = "";
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }           
            return result;
        }
    
    }
    public class TempProviderdata
    {
        public TempProviderdata()
        {
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NPI { get; set; }
        public string BMAddress1 { get; set; }
        public string BMAddress2 { get; set; }
        public string BMCity { get; set; }
        public string BMState { get; set; }
        public string BMZIP { get; set; }
        public string ServiceLevel { get; set; }
        public string licenseKey { get; set; }
        public bool ISPDR { get; set; }

    }
}
