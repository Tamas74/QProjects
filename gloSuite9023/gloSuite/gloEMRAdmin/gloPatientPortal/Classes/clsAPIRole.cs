using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace gloPatientPortal
{
    public  class clsAPIRole
    {

        private string _MessageBoxCaption = String.Empty;
        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #region Contructor
        public clsAPIRole(string DatabaseConnectionString)
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
        ~clsAPIRole()
        {
            Dispose(false);
        }
        #endregion
        #region Properties
        public Int64 RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsSystemDefined { get; set; }
        public Int64 UserID { get; set; }

        public string CDASections { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        #endregion


        public Int64 Add(clsAPIRole oAPIRole)
        {
            Int64 _result = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            object _intresult = 0;
            try
            {


                //   oDBParameters.Add("@ContactID", oContact.ContactID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
             //   oDBParameters.Add("@nRoleId", oAPIRole.RoleID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sRoleName", oAPIRole.RoleName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@bIsSystemDefined", oAPIRole.IsSystemDefined, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@UserID", oAPIRole.UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@IsActive", oAPIRole.IsActive, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@CDASections", oAPIRole.CDASections, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                object result = oDB.ExecuteScalar("gsp_APIRoleInsert", oDBParameters);
              
                    _result = Convert.ToInt64(result);
                
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
                oDBParameters.Dispose();
                oDB.Dispose();
                _intresult = null;
            }
            return _result;
        }
        public Int64 Modify(clsAPIRole oAPIRole)
        {
            Int64 _result = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            object _intresult = 0;
            try
            {

                oDBParameters.Add("@nRoleId", oAPIRole.RoleID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sRoleName", oAPIRole.RoleName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@bIsSystemDefined", oAPIRole.IsSystemDefined, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@UserID", oAPIRole.UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@IsActive", oAPIRole.IsActive, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@CDASections", oAPIRole.CDASections, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                object result = oDB.ExecuteScalar("gsp_APIRoleUpdate", oDBParameters);
                _result = Convert.ToInt64(result);
               
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                //System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        public DataTable GetAPIRoles(Int64 RoleID=0,bool mode=false)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = null;
            oDB.Connect(false);
            DataTable dt = null;
            //string _strSQL = "";

            try
            {
               
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nRoleId", RoleID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@mode", mode, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("gsp_APIRoleSelect", oDBParameters, out dt);
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
              //  System.Windows.Forms.MessageBox.Show(DBErr.ToString(), _MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
