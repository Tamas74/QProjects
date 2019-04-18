using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Rebex.Legacy;

namespace gloPMClaimService
{
    public class FTPParamete:IDisposable
    {
        private string _DataBaseConnectionString = "";
        private string _MessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #region Constructor & Destructor"


        public FTPParamete()
        {
            _DataBaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
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
       
        public bool ProxyEnabled = false;
        public int ProxyPort = 0;
        public Rebex.Net.FtpProxyType ProxyType = Rebex.Net.FtpProxyType.None;

		public Rebex.Net.FtpSecurity SecurityType = Rebex.Net.FtpSecurity.Unsecure;
		
		public Rebex.Net.TlsCipherSuite AllowedSuite = Rebex.Net.TlsCipherSuite.All;
		public Rebex.Net.TlsVersion Protocol = Rebex.Net.TlsVersion.None;
		public bool ClearCommandChannel = false;
	
		public Rebex.LogLevel LogLevel = Rebex.LogLevel.Error;
        
   
   //     public string ClaimManagement_InBox = "";
           public string ClaimManagement_InBox_835RemittanceAdvice = "";
        public void ReadFolderStructure(Int64 clearinghouseid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            DataTable oDataTable = new DataTable();
            string _strSQL = "";
            
            oDB.Connect(false);
            _strSQL = " SELECT ISNULL(sURL,'') AS sURL,  " +
            " ISNULL(sUserName,'') AS sUserName ,ISNULL(sPassword,'') AS sPassword,  " +
            " ISNULL(sIn_835_Remitance,'') AS sIn_835_Remitance " +
            " FROM BL_ClearingHouse_DTL  " +
            " WHERE nClearingHouseID = " + clearinghouseid + " AND nClinicID = " +1+ "";

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
                catch //(Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Invalid password", true);
                }
                //

		        Passive = false;
                Timeout = 30000;

                Proxy = "";
             
                
                ProxyEnabled = false;
                ProxyPort = 0;
                ProxyType = Rebex.Net.FtpProxyType.None;

		        SecurityType = Rebex.Net.FtpSecurity.Unsecure;
		        AllowedSuite = Rebex.Net.TlsCipherSuite.All;
		        Protocol = Rebex.Net.TlsVersion.None;
		        ClearCommandChannel = false;
        	
		        LogLevel = Rebex.LogLevel.Error;
              
                ClaimManagement_InBox_835RemittanceAdvice = oDataTable.Rows[0]["sIn_835_Remitance"].ToString();
               
            }

            oDB.Disconnect();
            oDB.Dispose();
        }
    }
}
