using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace gloTaskMail
{
    public class clsSignature
    {

        #region "Declarations"
        
        //Added By Pramod For Message Box
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private string _databaseconnectionstring = "";

        #endregion "Declarations"

        #region "Property Procedures"

        private string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        #endregion "Property Procedures"

        #region "Constructor & Destructor"


        public clsSignature(string dataBaseConnectionString)
        {
            _databaseconnectionstring = dataBaseConnectionString;
            
            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
        }



        //public clsSignature(Int64 SignatureID, Int64 UserID, Byte[] sign, Int64 Category, bool IsDefault, String SignatureName)
        //{

        //}

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

        ~clsSignature()
        {
            Dispose(false);
        }


        #endregion "Constructor & Destructor"

        #region " Methods"

        /// <summary>
        /// Method to get User Signature by UserID or by SignatureID
        /// Pass UserID=0 for getting Signature by SignatureID
        /// Pass SignatureID =0 for getting Signature by UserID.
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="SignatureID"></param>
        /// <returns></returns>
        public DataTable getAllSignatures(Int64 UserID, Int64 SignatureID)
        {
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer("Server=sakarserver;Database=gloPMSData_20080130;Uid=sa;Pwd=sasakar;");
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DataBaseConnectionString);
            try
            {

                oDB.Connect(false);
                DataTable dtSignature = null;

                if (UserID > 0 && SignatureID == 0)
                {
                    // oDB.Retrive_Query("select * from User_Signature where nUserID=" + UserID + " ", out dtSignature); //Remove select *
                      oDB.Retrive_Query("select  nSignatureID, nUserID, iSignature, nCategory, bIsDefault, sSignatureName  from User_Signature where nUserID=" + UserID + " ", out dtSignature);
                }
                else if (SignatureID > 0 && UserID == 0)
                {
                    //  oDB.Retrive_Query("select * from User_Signature where nSignatureID=" + SignatureID + " ", out dtSignature); //Remove select *
                      oDB.Retrive_Query("select  nSignatureID, nUserID, iSignature, nCategory, bIsDefault, sSignatureName   from User_Signature where nSignatureID=" + SignatureID + " ", out dtSignature);
                }
                if (dtSignature != null)
                {
                    return dtSignature;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public DataTable getAllSignatures(Int64 UserID, Int64 SignatureID, String sFilter)
        {
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer("Server=sakarserver;Database=gloPMSData_20080130;Uid=sa;Pwd=sasakar;");
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DataBaseConnectionString);
            try
            {
                string strQuery = "";
                oDB.Connect(false);
                DataTable dtSignature = null;

                if (UserID > 0 && SignatureID == 0)
                {
                    //strQuery="select * from User_Signature where nUserID=" + UserID + " ";
                    strQuery = "select  nSignatureID, nUserID, iSignature, nCategory, bIsDefault, sSignatureName   from User_Signature where nUserID=" + UserID + " ";
                }
                else if (SignatureID > 0 && UserID == 0)
                {
                    //strQuery="select * from User_Signature where nSignatureID=" + SignatureID + " ";
                    strQuery = "select  nSignatureID, nUserID, iSignature, nCategory, bIsDefault, sSignatureName   from User_Signature where nSignatureID=" + SignatureID + " ";
                }

                if (sFilter != "")
                    strQuery += " and " + sFilter;

                oDB.Retrive_Query(strQuery, out dtSignature);

                if (dtSignature != null)
                {
                    return dtSignature;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public Object saveSignature(Int64 SignatureID, Int64 UserID, Byte[] sign, Int64 Category, bool IsDefault, String SignatureName)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DataBaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            if (sign == null)
            {
               // string str = "false";
                sign = new Byte[0];

            }

            Int64 ret_nSignatureID = 0;
            try
            {
                oDB.Connect(false);
                //nSignatureID,nUserID,iSignature,nCategory,bIsDefault
                oParameters.Add("@nSignatureID", SignatureID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@iSignature", sign, ParameterDirection.Input, SqlDbType.Image);
                oParameters.Add("@nCategory", Category, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@bIsDefault", IsDefault, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@sSignatureName", SignatureName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nReturnSignID", ret_nSignatureID, ParameterDirection.Output, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                Object retObj = new object();
                if (Convert.ToBoolean(oDB.Execute("gsp_INUP_Signature", oParameters, out retObj)))
                {
                    return retObj;
                }
                return null;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
            }

        }

        public bool deleteSignature(Int64 SignatureID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DataBaseConnectionString);
            try
            {
                oDB.Connect(false);

                string strQuery = "delete  from User_Signature where nSignatureID = " + SignatureID;
                oDB.Execute_Query(strQuery);
                return true;
                //fill_lstSignature();

            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public DataTable getDefaultSignature(Int64 UserID)
        {
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer("Server=sakarserver;Database=gloPMSData_20080130;Uid=sa;Pwd=sasakar;");
            return null; 

        }

        public bool IsSignatureNameExists(long _nUserId, long _nSignatureID, string sSignatureName)
        {
            bool _result = false; 
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DataBaseConnectionString);
            try
            {

                oDB.Connect(false);
                string _sqlQuery = "select Count(*) from User_Signature where nUserID = " + _nUserId + " AND  nSignatureID <> " + _nSignatureID + " AND sSignatureName = '" + sSignatureName.Trim().Replace("'", "''")+ "'";
                Object oCount = null;
                oCount = oDB.ExecuteScalar_Query(_sqlQuery);

                if (oCount != null && Convert.ToString(oCount) != "")
                {
                    if (Convert.ToInt32(oCount) > 0)
                        _result = true; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;  
        }

        #endregion "Methods"

        #region " UI Method "

        public void ShowSignature()
        {
            frmSignature ofrmSignature = new frmSignature(_databaseconnectionstring);
            ofrmSignature.ShowDialog(ofrmSignature.Parent);
            ofrmSignature.Dispose();
            ofrmSignature = null;
        }

        #endregion " UI Method " 


    
        
    }
}
