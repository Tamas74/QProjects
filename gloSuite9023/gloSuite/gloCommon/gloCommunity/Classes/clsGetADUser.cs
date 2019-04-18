using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Net;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace gloCommunity.Classes
{
    public class clsGetADUser
    {
        public static string GetDomainName()
        {
            string domainName = null;
            try
            {
                domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            }
            catch
            {
            }

            if (domainName == null)
            {
                domainName = Dns.GetHostEntry(Dns.GetHostName()).HostName;
                if (domainName == null)
                {
                    domainName = Dns.GetHostByName(Dns.GetHostName()).HostName;
                }
            }
            return domainName;
        }
        public static Int32 CheckADuser()
        {
            //Get the username and domain information
            string userName = Environment.UserName;

            string domainName = GetDomainName();
            SearchResult result = null;
            //bool _IsEmailconfig = false;
            Int32 RetVal = 0;
            try
            {
                int cnt = Environment.MachineName.Length + 1;
                string actdomainname = domainName.Substring(cnt);
                domainName = actdomainname;

                int _intcnt = domainName.LastIndexOf(".") + 1;
                string _org = domainName.Substring(_intcnt, domainName.Length - _intcnt);

                string ldapQueryFormat = @"LDAP://" + actdomainname + "/DC=" + Environment.UserDomainName + ",DC=" + _org;
                string queryFilterFormat = @"(&(samAccountName=" + userName + ")(objectCategory=person)(objectClass=user))";

                using (DirectoryEntry root = new DirectoryEntry(ldapQueryFormat))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(root))
                    {
                        searcher.Filter = queryFilterFormat;
                        SearchResultCollection results = searcher.FindAll();
                        result = (results.Count != 0) ? results[0] : null;
                    }
                }
                if (result != null && result.Properties.Count > 0)
                {
                    if (((System.Collections.ReadOnlyCollectionBase)(result.Properties["mail"])).Count > 0)
                    {
                        string _UserEmail = result.Properties["mail"][0].ToString();
                        //_IsEmailconfig = true;
                        RetVal = 1;
                    }
                }
            }
            catch (Exception)
            {
                RetVal = 2;
            }
            return RetVal;
        }

        public static SearchResult GetADuser()
        {
            //Get the username and domain information
            string userName = Environment.UserName;
            string domainName = GetDomainName();
            SearchResult result = null;
       //     bool _IsEmailconfig = false;
            try
            {
                int cnt = Environment.MachineName.Length + 1;
                string actdomainname = domainName.Substring(cnt);
                domainName = actdomainname;

                int _intcnt = domainName.LastIndexOf(".") + 1;
                string _org = domainName.Substring(_intcnt, domainName.Length - _intcnt);

                string ldapQueryFormat = @"LDAP://" + actdomainname + "/DC=" + Environment.UserDomainName + ",DC=" + _org;
                string queryFilterFormat = @"(&(samAccountName=" + userName + ")(objectCategory=person)(objectClass=user))";

                using (DirectoryEntry root = new DirectoryEntry(ldapQueryFormat))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(root))
                    {
                        searcher.Filter = queryFilterFormat;
                        SearchResultCollection results = searcher.FindAll();
                        result = (results.Count != 0) ? results[0] : null;
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public bool CheckADUserEmail()
        {
            bool _blnConfigured = true;
            int CHKADResult = clsGetADUser.CheckADuser();
            //'        If clsGetADUser.CheckADuser() = False Then
            if (CHKADResult == 0)
            {
                int _Result = Convert.ToInt32(MessageBox.Show("User E-mail Id is not configured to Active Directory. Do you want to configure?",clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3));
                if (_Result == Convert.ToInt32(DialogResult.Yes))
                {
                    gloCommunity.frmEmailConfig ofrmEmail = new gloCommunity.frmEmailConfig();
                    int _frmResult = Convert.ToInt32(ofrmEmail.ShowDialog(ofrmEmail.Parent));
                    if (_frmResult == Convert.ToInt32(DialogResult.Cancel))
                    {
                        _blnConfigured = false;
                    }
                    ofrmEmail.Dispose();
                    ofrmEmail = null;
                }
                else
                {
                    _blnConfigured = false;
                }
            }
            else if (CHKADResult == 2)
            {
                MessageBox.Show("Windows Login User Does Not have Rights to Add E-mail Address in Active Directory." + Constants.vbCrLf + "Please Contact System Administrator.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _blnConfigured = false;
            }
            return _blnConfigured;
        }
    }
}
