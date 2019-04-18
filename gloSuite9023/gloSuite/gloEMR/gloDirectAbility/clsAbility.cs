using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Net;
using System.IO;

namespace gloDirectAbility
{
  public class clsAbility
    {
         #region "Constructor & Destructor"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
     

        private string _MessageBoxCaption = String.Empty;

        private string _databaseconnectionstring = "";
       // private Int64 _nLoginId = 0;

        public clsAbility(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
           // _nLoginId = nLoginID;
            
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
        
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloEMR";
                }
            }
            else
            { _MessageBoxCaption = "gloEMR"; }

            #endregion
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

        ~clsAbility()
        {
            Dispose(false);
        }

        #endregion

        public DataTable GetAbilityCreadential(string _sLoginID)
      {
          gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
          oDB.Connect(false);
          DataTable dt = new DataTable();
          string _strSQL = string.Empty ;

          try
          {

              _strSQL = "SELECT ISNULL(sAbilityEmail,'') AS sAbilityEmail,ISNULL(sAbilityPassword,'') AS sAbilityPassword FROM User_MST WHERE nUserID='" + _sLoginID + "'";
            
              oDB.Retrive_Query(_strSQL, out dt);
              return dt;
          }
          catch (gloDatabaseLayer.DBException DBErr)
          {
              DBErr.ERROR_Log(DBErr.ToString());
              System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
              return null;
          }
          catch (Exception ex)
          {
              gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
              return null;
          }
          finally
          {
              dt.Dispose();
              oDB.Disconnect();
              oDB.Dispose();
          }
         
      }

        public bool UpdateAbilityEmail(Int64 nUserID, string sEmail,string sPassword)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "Update User_MST  set  sAbilityEmail = '" + sEmail + "', sAbilityPassword = '" + sPassword + "'  where nUserID =" + nUserID;
                int result = oDB.Execute_Query(strQuery);
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public bool getAuthentication(string sLoginEmail, string sPassword)
        {
            StreamReader reader=null;
            WebResponse response=null;
            Stream dataStream=null;
            try
            {
                string sCredantial = null;

                sCredantial = "email=" + sLoginEmail + "&" + "password=" + sPassword;

                // WebRequest request = WebRequest.Create("https://mail.directability.com/sso.php"); //Test URL for glostream
                  WebRequest request = WebRequest.Create("https://mail.glostreamdirect.com/sso.php");
                request.Method = "POST";

                string postData = sCredantial; //"email=glostream@ability.directability.com&password=g1oSweet";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);               
                response = request.GetResponse();
                     
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                if (Convert.ToString(reader.ReadToEnd()) != "")
                {
                    byteArray = null;
                    return true;
                }
                else
                {
                    byteArray = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (response != null) { response.Close(); }
                if (reader != null) { reader.Close(); reader.Dispose(); reader = null; }
                if (dataStream != null) { dataStream.Close(); dataStream.Dispose(); dataStream = null; }
            }
        }


    }
}
