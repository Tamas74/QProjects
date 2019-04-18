using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace gloBilling
{
    public class FTPParamete : IDisposable
    {

        #region "Constructor & Distructor"


        public FTPParamete()
        {

        }

        private bool disposed = false;

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

        ~FTPParamete()
        {
            Dispose(false);
        }

        #endregion

        public string Host = "";
        public int Port = 0;
        public string SecureHost = "";
        public int SecurePort = 0;

        public string Login = "";
        public string Password = "";
        public bool Passive = false;
        public int Timeout = 30000;

        public string Proxy = "";
        public string ProxyLogin = "";
        public string ProxyPassword = "";
        public bool ProxyEnabled = false;
        public int ProxyPort = 0;
        public Rebex.Net.FtpProxyType ProxyType = Rebex.Net.FtpProxyType.None;

        public Rebex.Net.FtpSecurity SecurityType = Rebex.Net.FtpSecurity.Unsecure;
        public string CertificatePath = "";
        public Rebex.Net.TlsCipherSuite AllowedSuite = Rebex.Net.TlsCipherSuite.All;
        public Rebex.Net.TlsVersion Protocol = Rebex.Net.TlsVersion.None;
        public bool ClearCommandChannel = false;

        public Rebex.LogLevel LogLevel = Rebex.LogLevel.Error;

        public string ServerPath = "";
        public string ClaimManagement_Path = "";
        public string ClaimManagement_InBox = "";
        public string ClaimManagement_OutBox = "";
        public string ClaimManagement_General = "";

        public string ClaimManagement_InBox_271EligibilityResponse = "";
        public string ClaimManagement_InBox_277ClaimStatusResponse = "";
        public string ClaimManagement_InBox_835RemittanceAdvice = "";
        public string ClaimManagement_InBox_997Acknowledgement = "";

        public string ClaimManagement_OutBox_276EligibilityEnquiry = "";
        public string ClaimManagement_OutBox_837PClaimSubmission = "";
        public string ClaimManagement_OutBox_997Acknowledgement = "";

        public string ClaimManagement_General_CSRReports = "";
        public string ClaimManagement_General_Letters = "";
        public string ClaimManagement_General_Reports = "";
        public string ClaimManagement_General_Statements = "";
        public string ClaimManagement_General_WorkedTransaction = "";
        public string ClaimManagement_OutBox_StatementsSubmission = "";

        public void ReadFolderStructure(Int64 clearinghouseid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsGeneralClaimManager.DatabaseConnectionString);
            DataTable oDataTable = new DataTable();
            string _strSQL = "";

            oDB.Connect(false);
            _strSQL = " SELECT nClearingHouseID, ISNULL(sClearingHouseCode,'') AS sClearingHouseCode, " +
            " ISNULL(sURL,'') AS sURL,  " +
            " ISNULL(sUserName,'') AS sUserName ,ISNULL(sPassword,'') AS sPassword,  " +
            " ISNULL(sIn_271_ElgibilityResponse,'') AS sIn_271_ElgibilityResponse,  " +
            " ISNULL(sIn_277_ClaimStatus,'') AS sIn_277_ClaimStatus,  " +
            " ISNULL(sIn_835_Remitance,'') AS sIn_835_Remitance, " +
            " ISNULL(sIn_997_Acknowledge,'') AS sIn_997_Acknowledge,  " +
            " ISNULL(sOut_276_ElgibilityEnquiry,'') AS sOut_276_ElgibilityEnquiry, " +
            " ISNULL(sOut_837P_ClaimSubmition,'')AS sOut_837P_ClaimSubmition,  " +
            " ISNULL(sOut_997_Acknowledge,'') AS sOut_997_Acknowledge,  " +
            " ISNULL(sGen_CSRReports,'') AS sGen_CSRReports,  " +
            " ISNULL(sGen_Letters,'') AS sGen_Letters,  " +
            " ISNULL(sGen_Reports,'') AS sGen_Reports,  " +
            " ISNULL(sGen_Statements,'') AS sGen_Statements,  " +
            " ISNULL(sGen_WorkedTrans,'') AS sGen_WorkedTrans, " +
            " ISNULL(nFolderCategory,0) AS nFolderCategory,  " +
            " ISNULL(nClinicID,0) AS nClinicID,  " +
            " ISNULL(sOut_Statements_Submition,'') As sOut_Statements_Submition" +
            " FROM BL_ClearingHouse_DTL  " +
            " WHERE nClearingHouseID = " + clearinghouseid + " AND nClinicID = " + ClsGeneralClaimManager.ClinicID + "";

            oDB.Retrive_Query(_strSQL, out oDataTable);

            if (oDataTable != null && oDataTable.Rows.Count > 0)
            {
                Host = oDataTable.Rows[0]["sURL"].ToString();
                Port = 21;
                if (Host.StartsWith("s") == false && Host.Trim().ToUpper() == "FTP.GATEWAYEDI.COM")
                {
                    SecureHost = Host.PadLeft(Host.Length + 1, 's');
                }
                else
                {
                    SecureHost = Host;
                }
                SecurePort = 22;

                Login = oDataTable.Rows[0]["sUserName"].ToString();

                //Password = oDataTable.Rows[0]["sPassword"].ToString();
                try
                {

                    gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
                    string _encryptionKey = "12345678";
                    if (oDataTable.Rows[0]["sPassword"] != null && Convert.ToString(oDataTable.Rows[0]["sPassword"]).Trim() != "")
                    { Password = oClsEncryption.DecryptFromBase64String(oDataTable.Rows[0]["sPassword"].ToString(), _encryptionKey); }
                    else
                    { Password = ""; }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Invalid password", true);
                }
                //

                Passive = false;
                Timeout = 30000;

                Proxy = "";
                ProxyLogin = "";
                ProxyPassword = "";
                ProxyEnabled = false;
                ProxyPort = 0;
                ProxyType = Rebex.Net.FtpProxyType.None;

                SecurityType = Rebex.Net.FtpSecurity.Unsecure;
                CertificatePath = "";
                AllowedSuite = Rebex.Net.TlsCipherSuite.All;
                Protocol = Rebex.Net.TlsVersion.None;
                ClearCommandChannel = false;

                LogLevel = Rebex.LogLevel.Error;

                ClaimManagement_InBox_271EligibilityResponse = oDataTable.Rows[0]["sIn_271_ElgibilityResponse"].ToString();
                ClaimManagement_InBox_277ClaimStatusResponse = oDataTable.Rows[0]["sIn_277_ClaimStatus"].ToString();
                ClaimManagement_InBox_835RemittanceAdvice = oDataTable.Rows[0]["sIn_835_Remitance"].ToString();
                ClaimManagement_InBox_997Acknowledgement = oDataTable.Rows[0]["sIn_997_Acknowledge"].ToString();

                ClaimManagement_OutBox_276EligibilityEnquiry = oDataTable.Rows[0]["sOut_276_ElgibilityEnquiry"].ToString();
                ClaimManagement_OutBox_837PClaimSubmission = oDataTable.Rows[0]["sOut_837P_ClaimSubmition"].ToString();
                ClaimManagement_OutBox_997Acknowledgement = oDataTable.Rows[0]["sOut_997_Acknowledge"].ToString();

                ClaimManagement_General_CSRReports = oDataTable.Rows[0]["sGen_CSRReports"].ToString();
                ClaimManagement_General_Letters = oDataTable.Rows[0]["sGen_Letters"].ToString();
                ClaimManagement_General_Reports = oDataTable.Rows[0]["sGen_Reports"].ToString();
                ClaimManagement_General_Statements = oDataTable.Rows[0]["sGen_Statements"].ToString();
                ClaimManagement_General_WorkedTransaction = oDataTable.Rows[0]["sGen_WorkedTrans"].ToString();

                ClaimManagement_OutBox_StatementsSubmission = oDataTable.Rows[0]["sGen_Statements"].ToString();
            }

            oDB.Disconnect();
            oDB.Dispose();
        }
    }
}
