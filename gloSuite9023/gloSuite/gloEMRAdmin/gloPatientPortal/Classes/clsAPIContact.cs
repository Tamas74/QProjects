using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace gloPatientPortal
{
   public class clsAPIContact
    {
        private string _MessageBoxCaption = String.Empty;
        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


        #region Contructor
        public clsAPIContact(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
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
        ~clsAPIContact()
        {
            Dispose(false);
        } 
        #endregion

        #region Varible Properties
        public Int64 APIAccessUserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Int64 RoleID { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long UserID { get; set; }
        public string IsActive { get; set; }
        
        
        #endregion
        public bool Add(clsAPIContact oContact)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            object _intresult = 0;
            try
            {


                //   oDBParameters.Add("@ContactID", oContact.ContactID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sFirstName", oContact.FirstName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sLastName", oContact.LastName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nRoleId", oContact.RoleID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtDOB", oContact.DOB, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                oDBParameters.Add("@sGender", oContact.Gender, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sEmail", oContact.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sMiddleName", oContact.MiddleName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDBParameters.Add("@sAddressLine1", oContact.AddressLine1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sAddressLine2", oContact.AddressLine2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sCity", oContact.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sState", oContact.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sZIP", oContact.Zip, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sCountry", oContact.Country, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sCounty", oContact.County, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sPhone", oContact.Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sMobile", oContact.Mobile, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDBParameters.Add("@UserID", oContact.UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
              
                int result = oDB.Execute("gsp_APIContactInsert", oDBParameters);
                if (result > 0)
                {
                    _result = true;
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
               // System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                _intresult = null;
            }
            return _result;
        }
        public bool Modify(clsAPIContact oContact)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            object _intresult = 0;
            try
            {


                oDBParameters.Add("@nAPIAccessUserId", oContact.APIAccessUserId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sFirstName", oContact.FirstName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sLastName", oContact.LastName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nRoleId", oContact.RoleID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtDOB", oContact.DOB, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                oDBParameters.Add("@sGender", oContact.Gender, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sEmail", oContact.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sMiddleName", oContact.MiddleName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                
                oDBParameters.Add("@sAddressLine1", oContact.AddressLine1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sAddressLine2", oContact.AddressLine2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sCity", oContact.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sState", oContact.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sZIP", oContact.Zip, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sCountry", oContact.Country, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sCounty", oContact.County , System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sPhone", oContact.Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sMobile", oContact.Mobile, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDBParameters.Add("@UserID", oContact.UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
              
                int result = oDB.Execute("gsp_APIContactUpdate", oDBParameters);
                if (result > 0)
                {
                    _result = true;
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
               // System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
                _intresult = null;
            }
            return _result;
        }
        public DataTable GetAPIContacts(Int64 nAPIAccessUserId = 0)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = null;
            oDB.Connect(false);
            DataTable dt = null;
            //string _strSQL = "";

            try
            {

                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nAPIAccessUserId", nAPIAccessUserId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_APIContactSelect", oDBParameters, out dt);
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }
            return dt;
        }

    }
}
