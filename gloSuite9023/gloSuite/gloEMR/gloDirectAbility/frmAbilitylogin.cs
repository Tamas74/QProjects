using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;




namespace gloDirectAbility
{
    public partial class frmAbilitylogin : Form
    {
        
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _databaseConnection = "";      
        private Int64 _UserID = 0;
        private string _UserName = "";
        private string _EmailAddress = "";
        private string _Password = "";
        private string _messageBoxCaption = "";       
        private string _sLoginEmail = string.Empty; 
      //  Timer timer = new Timer();

         #region "Constructor"

        public frmAbilitylogin(string sDatabaseConnectionString,string sEmail, string sPassword)
        {
            InitializeComponent();
            _EmailAddress = sEmail;
            _Password = sPassword;
            _databaseConnection = sDatabaseConnectionString;
            //_nLoginID = nLogineID;
            #region " Retrieve UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserID = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserID = 0;
            }
            #endregion
            
            #region " Retrieve UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "";
                }
            }
            else
            { _messageBoxCaption = ""; }

            #endregion
        } 

        #endregion

       
        private void frmAbilitylogin_Load(object sender, EventArgs e)
        {
            //gloSecurity.ClsEncryption oClsEncryption = new gloSecurity.ClsEncryption();
            //DataTable dt= new DataTable();
            try
            {
                #region "Commented Code"
                //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnection);
                //clsAbility oAbility = new clsAbility(_databaseConnection);

                //dt = oAbility.GetAbilityCreadential(_nLoginID.ToString());

          
                //if (dt.Rows.Count > 0)
                //{
                //    string sPassword = "";
                //    //Navigate_URL("glostream@ability.directability.com", "g1oSweet");
                    
                //    string _encryptionKey = "12345678";
                //    if (dt.Rows[0]["sAbilityPassword"] != null && Convert.ToString(dt.Rows[0]["sAbilityPassword"]).Trim() != "")
                //    { sPassword = oClsEncryption.DecryptFromBase64String(dt.Rows[0]["sAbilityPassword"].ToString(), _encryptionKey); }
                //    else
                //    { sPassword = ""; }


                //    if (Navigate_URL(dt.Rows[0]["sAbilityEmail"].ToString(), sPassword) == false)
                //    {
                //frmAbilityUserMgmt ofrmUsermgmt = new frmAbilityUserMgmt(_databaseConnection, _nLoginID);
                //ofrmUsermgmt.EmailAddress = dt.Rows[0]["sAbilityEmail"].ToString();
                //ofrmUsermgmt.Password = sPassword;
                //ofrmUsermgmt.ShowDialog(this);
                //if (ofrmUsermgmt.DialgoResult == false)
                //{
                //    ofrmUsermgmt.Dispose();
                //    this.Close();
                //}
                //else
                //{
                //    Navigate_URL(ofrmUsermgmt.EmailAddress, ofrmUsermgmt.Password);
                //}
                //ofrmUsermgmt.Dispose();
                //    }
                //}
                #endregion ""

                Navigate_URL(_EmailAddress, _Password);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
          finally
          {
              //if (dt != null) { dt.Dispose(); }
              //if (oClsEncryption != null) { oClsEncryption.Dispose(); }
          }
        }
        
        private bool Navigate_URL(string sLoginEmail, string sPassword)
        {
            try
            {
                //getCurrentBrowser().Navigate("https://mail.directability.com/webmail.php?email=glostream@ability.directability.com&password=g1oSweet");
                // getCurrentBrowser().Url = new System.Uri("https://mail.directability.com/webmail.php?email=glostream@ability.directability.com&password=g1oSweet") ;
                string URL = null;
                string TargetFrame = null;
                byte[] PostData = null;
                string Headers = null;
                string sCredantial = null;

                sCredantial = "email=" + sLoginEmail + "&" + "password=" + sPassword;

               // URL = "https://mail.directability.com/sso.php"; //Test URL for glostream
                URL = "https://mail.glostreamdirect.com/sso.php"; 

                TargetFrame = "";


                // PostData = ASCIIEncoding.ASCII.GetBytes("email=glostream@ability.directability.com&password=g1oSweet");
                PostData = ASCIIEncoding.ASCII.GetBytes(sCredantial);

                Headers = "Content-Type: application/x-www-form-urlencoded";
                if (sLoginEmail != "" || sPassword != "")
                {
                    wbLogin.Navigate(URL, TargetFrame, PostData, Headers);

                    _sLoginEmail = sLoginEmail;
                    //timer.Tick += new EventHandler(CheckPassword);
                    //timer.Interval = 7500;
                    //timer.Start();
                    return true;
                }
                else
                {
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }

        }

        //private void CheckPassword(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //timer.Stop();
        //        //timer.Dispose();
        //        if (wbLogin != null)
        //        {
        //            object objHtmlDoc = wbLogin.DocumentText;
        //            string pageSource = objHtmlDoc.ToString();
        //            if (pageSource.Contains("<title>mailABILITY</title>") == false)
        //            {
        //                frmAbilityUserMgmt ofrmUsermgmt = new frmAbilityUserMgmt(_databaseConnection, _nLoginID);
        //                ofrmUsermgmt.EmailAddress = _sLoginEmail;
        //                ofrmUsermgmt.Password = "";// sPassword;
        //                _sLoginEmail = string.Empty;
        //                ofrmUsermgmt.ShowDialog(this);
        //                if (ofrmUsermgmt.DialgoResult == false)
        //                {
        //                    ofrmUsermgmt.Dispose();
        //                    this.Close();
        //                }
        //                else
        //                {
        //                    wbLogin.Controls.Clear();
        //                    Navigate_URL(ofrmUsermgmt.EmailAddress, ofrmUsermgmt.Password);
        //                }
        //                ofrmUsermgmt.Dispose();

        //            }
        //            objHtmlDoc = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}

        private void tblbtn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAbilitylogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
               // if (timer != null) { timer.Stop(); timer.Dispose(); }
                if (wbLogin != null) { wbLogin.Dispose(); wbLogin = null; }
            }
            catch (Exception )
            {
            }
        }

       
    }

  
}
