using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloSecurity
{
   public class clsgloLicence : IDisposable
    {
        private string MasterSecrateKey = "triarq@1981!";
        private string ClientSecrateKey = "thealth$2014@";
        //public enum enmAUSStatus
        //{
        //    Active = 0,
        //    PendingForLicense = 1,
        //    PendingForReview = 2,
        //    ReviewForServices = 3
        //}
      #region Constructor and Destructor
       public clsgloLicence()
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

            ~clsgloLicence()
            {
                Dispose(false);
            }
        #endregion

            public string GetLicensedServiceLevel(string sLicenceKey)
            {
                string sServiceLevel = string.Empty;    
                string AusId= string.Empty; 
                string nID= string.Empty;
                if (sLicenceKey != "")
                {
                    try
                    {
                        using (ClsEncryption oDecrypt = new ClsEncryption())
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
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        return string.Empty;
                    }
                }
                return sServiceLevel;
            }
    }

}
