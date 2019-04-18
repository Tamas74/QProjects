using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;

namespace ClearGage
{
    public class clsCleargage
    {
        public enum OnlineActivity
        {
            PaymentPlan = 0,
            OneTimePayment = 1,
            Subscriptions = 2,
            Transaction = 3
        }

        private ClearGage.SSO.SsoHelper ssoHelper;
        private ResourceManager configResource;
        private const string _encryptionKey = "12345678";
        public static bool IsFromPortal= false;

        public SSO.SsoHelper InitiateSOSHelper()
        {
        
            
#if (DEV)
			this.configResource = new ResourceManager("SSODemo.ClientConfigDev", typeof(PMSForm).Assembly);
#else
            configResource = new ResourceManager("ClearGageSSO.ClientConfig", typeof(frmCGWebBrowser).Assembly);
#endif

            // Initialize the SsoHelper component
            ssoHelper = new ClearGage.SSO.SsoHelper(
                clientId: configResource.GetString("clientId"),
                username: configResource.GetString("username"),
                password: configResource.GetString("password"),
                key: configResource.GetString("key"),
                name: configResource.GetString("name")
            );

            // Set DEMO mode to avoid making requests to the production server in this demo application 
            ssoHelper.SetMode(ClearGage.SSO.SsoHelper.Mode.MODE_DEMO);

#if (DEV)
			ssoHelper.SetHost(this.configResource.GetString("host"));
#endif
            return ssoHelper;
        }
        public SSO.SsoHelper InitiateSOSHelper(string DBConnectionString,bool bIsFromPortal=false)
        {
            object objResult = null;
            string sMode = "";
            string nClientID="";
            string sUserName = "";
            string sPassword = "";
            string sKey = "";
            string sName = "";
            IsFromPortal = bIsFromPortal;

            gloSecurity.ClsEncryption oEncryption =new gloSecurity.ClsEncryption();
            gloSettings.GeneralSettings ogloSetting = new gloSettings.GeneralSettings(DBConnectionString);
            
            ogloSetting.GetSetting("Cleargage_Mode", out objResult);
            if (Convert.ToString(objResult) != "")
            {
                sMode = Convert.ToString(objResult);
                if (sMode=="")
                {
                    sMode = "Demo";
                }
                objResult = null;
            }

            if (sMode.ToLower() == "demo")
            {
                ogloSetting.GetSetting("Cleargage_ClientID", out objResult);
                if (Convert.ToString(objResult) != "")
                {
                    nClientID = Convert.ToString(objResult);
                    objResult = null;
                }
                ogloSetting.GetSetting("Cleargage_UserName", out objResult);
                if (Convert.ToString(objResult) != "")
                {
                    sUserName = Convert.ToString(objResult);
                    objResult = null;
                }
                ogloSetting.GetSetting("Cleargage_Password", out objResult);
                if (Convert.ToString(objResult) != "")
                {
                    sPassword = oEncryption.DecryptFromBase64String(Convert.ToString(objResult), _encryptionKey).Trim();
                    //sPassword = Convert.ToString(objResult);
                    objResult = null;
                }
                ogloSetting.GetSetting("Cleargage_Key", out objResult);
                if (Convert.ToString(objResult) != "")
                {
                    sKey = Convert.ToString(objResult);
                    objResult = null;
                }
                ogloSetting.GetSetting("Cleargage_Name", out objResult);
                if (Convert.ToString(objResult) != "")
                {
                    sName = Convert.ToString(objResult);
                    objResult = null;
                }
            }
            else if (sMode.ToLower()=="live")
            {
                ogloSetting.GetSetting("CleargageLive_ClientID", out objResult);
                if (Convert.ToString(objResult) != "")
                {
                    nClientID = Convert.ToString(objResult);
                    objResult = null;
                }
                ogloSetting.GetSetting("CleargageLive_UserName", out objResult);
                if (Convert.ToString(objResult) != "")
                {
                    sUserName = Convert.ToString(objResult);
                    objResult = null;
                }
                ogloSetting.GetSetting("CleargageLive_Password", out objResult);
                if (Convert.ToString(objResult) != "")
                {
                    sPassword = oEncryption.DecryptFromBase64String(Convert.ToString(objResult), _encryptionKey).Trim();
                    //sPassword = Convert.ToString(objResult);
                    objResult = null;
                }
                ogloSetting.GetSetting("CleargageLive_Key", out objResult);
                if (Convert.ToString(objResult) != "")
                {
                    sKey = Convert.ToString(objResult);
                    objResult = null;
                }
                ogloSetting.GetSetting("CleargageLive_Name", out objResult);
                if (Convert.ToString(objResult) != "")
                {
                    sName = Convert.ToString(objResult);
                    objResult = null;
                }
            }
            
//#if (DEV)
//            this.configResource = new ResourceManager("SSODemo.ClientConfigDev", typeof(PMSForm).Assembly);
//#else
//            configResource = new ResourceManager("ClearGageSSO.ClientConfig", typeof(frmCGWebBrowser).Assembly);
//#endif

            // Initialize the SsoHelper component
            ssoHelper = new ClearGage.SSO.SsoHelper(
                clientId: nClientID,
                username: sUserName,
                password: sPassword,
                key:  sKey,
                name: sName
            );

            // Set DEMO mode to avoid making requests to the production server in this demo application 
            
            if (sMode.ToLower()=="demo")
            {
                ssoHelper.SetMode(ClearGage.SSO.SsoHelper.Mode.MODE_DEMO2);
            }
            else if (sMode.ToLower()=="live")
            {
                ssoHelper.SetMode(ClearGage.SSO.SsoHelper.Mode.MODE_LIVE);
            }
            
//#if (DEV)
//            ssoHelper.SetHost(this.configResource.GetString("host"));
//#endif
            return ssoHelper;
        }

    }
}
