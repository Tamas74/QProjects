using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using gloTaskMail;

namespace gloTasksMails
{

  

    public class gloTaskMail
    {
        public delegate void gloTaskmailHandler();   //added delegate for calling gloCommunityViewDataform for Taskmail Download.
        public event gloTaskmailHandler EvntTaskmailHandler; //added event for calling gloCommunityViewDataform for Taskmail Download.

        #region " Declarations "

        private string _databaseconnectionstring = "";
        //private string _messageBoxCaption = "gloPMS";
        
        //Added By Pramod For Message Box
        private string _messageBoxCaption = String.Empty;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        #endregion " Declarations "

        #region " Property Procedures "

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedures "

        #region "Constructor & Distructor"


        public gloTaskMail(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
                }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~gloTaskMail()
        {
            Dispose(false);
        }

        #endregion

        #region " Category "

        //Category
        public Int64 Add(Common.Category oCategory)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Object oResult = new object();
            try
            {

                oResult = null;
                oDB.Connect(false);
                //nCategoryID,sDescription,sColorCode
                oParameters.Add("nCategoryID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("sDescription", oCategory.Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("sColorCode", oCategory.ColorCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                _result = oDB.Execute("TM_INUP_CategoryType", oParameters, out oResult);

                if (oResult != null)
                {
                    _result = Convert.ToInt64(oResult);
                    if (!(_result > 0))
                    {
                        MessageBox.Show("Error Adding Record", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;

                    }
                }
                return _result;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                oCategory.Dispose();
            }

        }

        /*
        public bool Modify(Int64 CategoryID, Common.Category oCategory)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Object oResult = new object();
            try
            {
                oResult = null;
                oDB.Connect(false);
                //nCategoryID,sDescription,sColorCode
                oParameters.Add("nCategoryID", oCategory.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("sDescription", oCategory.Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("sColorCode", oCategory.ColorCode, ParameterDirection.Input, SqlDbType.VarChar);

                _result = oDB.Execute("TM_INUP_CategoryType", oParameters, out oResult);

                if (oResult != null)
                {
                    _result = Convert.ToInt64(oResult);
                    if (!(_result > 0))
                    {
                        MessageBox.Show("Error Modifiying Record", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;

                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();

            }
        }*/

        public bool Modify(Common.Category oCategory)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Object oResult = new object();
            try
            {
                oResult = null;
                oDB.Connect(false);
                //nCategoryID,sDescription,sColorCode
                oParameters.Add("nCategoryID", oCategory.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("sDescription", oCategory.Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("sColorCode", oCategory.ColorCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                _result = oDB.Execute("TM_INUP_CategoryType", oParameters, out oResult);

                if (oResult != null)
                {
                    _result = Convert.ToInt64(oResult);
                    if (!(_result > 0))
                    {
                        MessageBox.Show("Error Modifiying Record", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;

                    }
                }
                return true;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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
                oParameters.Dispose();
                oCategory.Dispose();

            }
        }

        public bool DeleteCategory(Int64 CategoryID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from TM_Category_MST where nCategoryID =" + CategoryID;
                int result = oDB.Execute_Query(strQuery);
                if (result > 0)
                {
                    return true;
                }
                return false;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

        public bool UnblockCategory(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE TM_Category_MST SET bIsBlocked = '" + false + "' WHERE nCategoryID = " + ID + " ";
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool DeleteAllCategories()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            Int64 _categoryId = 0;
            DataTable dt = new DataTable();
            int _result = 0;
            try
            {
                oDB.Connect(false);
                strQuery = " select nCategoryID from TM_Category_MST where bIsBlocked = '" + true + "' and nClinicID = " + this.ClinicID + " ";
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _categoryId = Convert.ToInt64(dt.Rows[i][0]);

                        if (CanDeleteCategory(_categoryId))
                        {
                            strQuery = "";
                            strQuery = " DELETE FROM TM_Category_MST WHERE nCategoryID = " + _categoryId + " ";
                            _result = oDB.Execute_Query(strQuery);
                            _categoryId = 0;
                        }

                    }
                }
                return Convert.ToBoolean(_result);

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool CanDeleteCategory(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            //int _result = 0;
            DataTable dt;
            object _IsSystem = null;
            try
            {
                oDB.Connect(false);

                //Check if the Category is System Defined
                strQuery = "select bIsSystem from TM_Category_MST where nCategoryID = " + ID;
                _IsSystem = oDB.ExecuteScalar_Query(strQuery);
                if (Convert.ToInt64(_IsSystem) == 1)
                {
                    return false;
                }
                strQuery = "";

                strQuery = "Select nDraftMailID from dbo.TM_Mail_Drafts where nCategoryID = " + ID;
                oDB.Retrive_Query(strQuery, out dt);
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return false;
                }
                dt.Dispose();

                strQuery = "Select nMailID from dbo.TM_Mail_Inbox where nCategoryID = " + ID;
                dt = new DataTable();
                oDB.Retrive_Query(strQuery, out dt);
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return false;
                }
                dt.Dispose();

                strQuery = "Select nMailID from dbo.TM_Mail_SentItem where nCategoryID = " + ID;
                oDB.Retrive_Query(strQuery, out dt);
                dt = new DataTable();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return false;
                }
                dt.Dispose();

                strQuery = "Select nTaskID from dbo.TM_TaskMST where nCategoryID = " + ID;
                oDB.Retrive_Query(strQuery, out dt);
                dt = new DataTable();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return false;
                }
                dt.Dispose();

                return true;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public bool ClearCategories()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " DELETE FROM TM_Category_MST WHERE bIsSystem = '" + false + "' AND nClinicID = " + this.ClinicID + " ";
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool BlockCategory(Int64 nCategoryId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "UPDATE TM_Category_MST SET bIsBlocked = '" + true + "' WHERE nCategoryID = " + nCategoryId;
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool IsDeleteCategory(Int64 CategoryID)
        {
            try
            {
                return false;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(dbErr.ToString(), false);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            finally
            {

            }
        }

        public bool IsExistsCategory(string CategoryName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                //strQuery = "select count(nCategoryID) from TM_Category_MST where sDescription = '" + CategoryName + " ' ";
                strQuery = "select count(nCategoryID) from TM_Category_MST where sDescription = '" + CategoryName.Replace("'","''")+ " ' AND nClinicID = " + this.ClinicID + " ";
                //
                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(strQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;


            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

        public Common.Categories GetCategories()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtCategoryType = new DataTable();
            Common.Category oCategory;
            Common.Categories oCategories = new gloTasksMails.Common.Categories(); // global::gloTaskMail.Common.Categories();

            try
            {
                oDB.Connect(false);
                string strQuery = "";

                //strQuery = "select * from TM_Category_MST ";
                //strQuery = "select * from TM_Category_MST where nClinicID = "+this.ClinicID+" ";
                // strQuery = "select * from TM_Category_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + " ";  //Remove select *
                strQuery = "select nCategoryID,sDescription,sColorCode,bIsSystem from TM_Category_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + " ";

                oDB.Retrive_Query(strQuery, out dtCategoryType);
                if (dtCategoryType != null)
                {
                    //return dtCategoryType;
                    for (int i = 0; i <= dtCategoryType.Rows.Count - 1; i++)
                    {
                        //dbo.TM_Category_MST :: nCategoryID,sDescription,sColorCode,bIsSystem
                        oCategory = new gloTasksMails.Common.Category();// global::gloTaskMail.Common.Category();
                        oCategory.ID = Convert.ToInt64(dtCategoryType.Rows[i]["nCategoryID"]);
                        oCategory.Description = dtCategoryType.Rows[i]["sDescription"].ToString();
                        oCategory.ColorCode = Convert.ToInt32(dtCategoryType.Rows[i]["sColorCode"]);
                        oCategory.IsSystem = Convert.ToBoolean(dtCategoryType.Rows[i]["bIsSystem"]);
                        oCategories.Add(oCategory);
                        oCategory = null;
                    }
                    return oCategories;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public Common.Category GetCategory(Int64 CategoryID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtCategoryType = new DataTable();
            Common.Category oCategory;

            try
            {
                oDB.Connect(false);
                string strQuery = "";
                //if (CategoryID == 0)
                //{
                //    return null;
                //}
                //strQuery = "select * from TM_Category_MST where nCategoryID=" + CategoryID;  //Remove select *
                strQuery = "select nCategoryID,sDescription,sColorCode,bIsSystem  from TM_Category_MST where nCategoryID=" + CategoryID;

                oDB.Retrive_Query(strQuery, out dtCategoryType);
                if (dtCategoryType != null)
                {
                    oCategory = new gloTasksMails.Common.Category();
                    //return dtCategoryType;
                    for (int i = 0; i <= dtCategoryType.Rows.Count - 1; i++)
                    {
                        //dbo.TM_Category_MST :: nCategoryID,sDescription,sColorCode,bIsSystem
                        oCategory.ID = Convert.ToInt64(dtCategoryType.Rows[i]["nCategoryID"]);
                        oCategory.Description = dtCategoryType.Rows[i]["sDescription"].ToString();
                        oCategory.ColorCode = Convert.ToInt32(dtCategoryType.Rows[i]["sColorCode"]);
                        oCategory.IsSystem = Convert.ToBoolean(dtCategoryType.Rows[i]["bIsSystem"]);

                    }
                    return oCategory;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public DataTable GetBlockedCategories()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtCategoryType = new DataTable();

            try
            {
                oDB.Connect(false);
                string strQuery = "";

                strQuery = "select nCategoryID,sDescription,bIsSystem FROM TM_Category_MST WHERE bIsBlocked = '" + true + "' AND nClinicID = " + this.ClinicID + " ";
                oDB.Retrive_Query(strQuery, out dtCategoryType);
                return dtCategoryType;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
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

        #endregion " Category "

        #region " FollowUp "
        //Followup
        public Int64 Add(Common.Followup oFollowUp)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Object oResult = new object();
            try
            {

                oResult = null;
                oDB.Connect(false);
                //dbo.TM_FollowUp_MST - ,nFollowUpID,sDescription
                oParameters.Add("@nFollowUpID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sDescription", oFollowUp.Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                _result = oDB.Execute("TM_INUP_FollowUpType", oParameters, out oResult);

                if (oResult != null)
                {
                    _result = Convert.ToInt64(oResult);
                    if (!(_result > 0))
                    {
                        MessageBox.Show("Error Adding Record", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;

                    }
                }
                return _result;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;

            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();

            }

        }

        public bool Modify(Int64 FollowUpID, Common.Followup oFollowUp)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Object oResult = new object();
            try
            {

                oResult = null;
                oDB.Connect(false);
                //dbo.TM_FollowUp_MST - ,nFollowUpID,sDescription
                oParameters.Add("@nFollowUpID", oFollowUp.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sDescription", oFollowUp.Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                _result = oDB.Execute("TM_INUP_FollowUpType", oParameters, out oResult);

                if (oResult != null)
                {
                    _result = Convert.ToInt64(oResult);
                    if (!(_result > 0))
                    {
                        MessageBox.Show("Error Adding Record", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;

                    }
                }
                return true;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

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
                oParameters.Dispose();
                oFollowUp.Dispose();
            }

        }

        public bool Modify(Common.Followup oFollowUp)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Object oResult = new object();
            try
            {

                oResult = null;
                oDB.Connect(false);
                //dbo.TM_FollowUp_MST - ,nFollowUpID,sDescription
                oParameters.Add("@nFollowUpID", oFollowUp.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sDescription", oFollowUp.Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                _result = oDB.Execute("TM_INUP_FollowUpType", oParameters, out oResult);

                if (oResult != null)
                {
                    _result = Convert.ToInt64(oResult);
                    if (!(_result > 0))
                    {
                        MessageBox.Show("Error Adding Record", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;

                    }
                }
                return true;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

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
                oParameters.Dispose();
                oFollowUp.Dispose();

            }

        }

        public bool DeleteFollowUp(Int64 FollowUpID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from TM_FollowUp_MST where nFollowUpID =" + FollowUpID;
                int result = oDB.Execute_Query(strQuery);
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

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

        public bool CanDeleteFollowUp(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            //int _result = 0;
            DataTable dt;
            object _IsSystem = null;
            try
            {
                oDB.Connect(false);

                //Check if the FollowUp is System Defined
                strQuery = "select bIsSystem from TM_FollowUp_MST where nFollowUpID = " + ID;
                _IsSystem = oDB.ExecuteScalar_Query(strQuery);
                if (Convert.ToInt64(_IsSystem) == 1)
                {
                    return false;
                }
                strQuery = "";

                strQuery = "Select nTaskID from TM_TaskMST where nFollowupID = " + ID;
                oDB.Retrive_Query(strQuery, out dt);
                dt = new DataTable();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return false;
                }
                dt.Dispose();

                return true;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public bool IsDeleteFollowUp(Int64 FollowUpID)
        {
            return false;
        }

        public bool BlockFollowUp(Int64 FollowUpId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "UPDATE TM_FollowUp_MST SET bIsBlocked = '" + true + "' WHERE nFollowUpID = " + FollowUpId;
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public DataTable GetBlockedFollowUps()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtFollowType = new DataTable();
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "select nFollowUpID,sDescription,bIsSystem from TM_FollowUp_MST where bIsBlocked = '" + true + "' AND nClinicID = " + this.ClinicID + "";

                oDB.Retrive_Query(strQuery, out dtFollowType);
                return dtFollowType;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
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

        public bool UnblockFollowUp(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE TM_FollowUp_MST SET bIsBlocked = '" + false + "' WHERE nFollowUpID = " + ID + " ";
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool DeleteAllFollowUp()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            Int64 _followupId = 0;
            DataTable dt = new DataTable();
            int _result = 0;
            try
            {
                oDB.Connect(false);

                strQuery = " select nFollowUpID from TM_FollowUp_MST where bIsBlocked = '" + true + "' and nClinicID = " + this.ClinicID + " ";
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _followupId = Convert.ToInt64(dt.Rows[i][0]);

                        if (CanDeleteStatus(_followupId))
                        {
                            strQuery = "";
                            strQuery = " DELETE FROM TM_FollowUp_MST WHERE nFollowUpID = " + _followupId + " ";
                            _result = oDB.Execute_Query(strQuery);
                            _followupId = 0;
                        }

                    }
                }


                return Convert.ToBoolean(_result);

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool ClearFollowUps()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " DELETE FROM TM_FollowUp_MST WHERE bIsSystem = '" + false + "' AND nClinicID = " + this.ClinicID + " ";
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool IsExistsFollowUp(string FollowUpName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            object _intresult = null;
            try
            {
                oDB.Connect(false);
                //strQuery = "select count(nFollowUpID) from TM_FollowUp_MST where sDescription = '" + FollowUpName + " ' ";
                strQuery = "select count(nFollowUpID) from TM_FollowUp_MST where sDescription = '" + FollowUpName.Replace("'", "''") + " ' AND nClinicID = " + this.ClinicID + " ";
                //

                _intresult = oDB.ExecuteScalar_Query(strQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

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
                //oParameters.Dispose();

            }
        }

        public Common.Followups GetFollowUps()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtFollowType = new DataTable();
            gloTasksMails.Common.Followup oFollowUp;
            gloTasksMails.Common.Followups oFollowUps = new gloTasksMails.Common.Followups();
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //strQuery = "select * from TM_FollowUp_MST";
                //strQuery = "select * from TM_FollowUp_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + "";     //Remove select *           
                strQuery = "select nFollowUpID,sDescription,bIsSystem from TM_FollowUp_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + "";                
                //
                oDB.Retrive_Query(strQuery, out dtFollowType);
                if (dtFollowType != null)
                {
                    //dbo.TM_FollowUp_MST,nFollowUpID,sDescription,bIsSystem
                    for (int i = 0; i <= dtFollowType.Rows.Count - 1; i++)
                    {
                        oFollowUp = new gloTasksMails.Common.Followup();
                        oFollowUp.ID = Convert.ToInt64(dtFollowType.Rows[i]["nFollowUpID"]);
                        oFollowUp.Description = dtFollowType.Rows[i]["sDescription"].ToString();
                        oFollowUp.IsSystem = Convert.ToBoolean(dtFollowType.Rows[i]["bIsSystem"]);

                        oFollowUps.Add(oFollowUp);

                        oFollowUp = null;
                    }
                    return oFollowUps;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dtFollowType.Dispose();
                oFollowUps.Dispose();
                // oParameters.Dispose();

            }

        }

        public Common.Followups GetFollowUps(String sFilter)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtFollowType = new DataTable();
            gloTasksMails.Common.Followup oFollowUp;
            gloTasksMails.Common.Followups oFollowUps = new gloTasksMails.Common.Followups();
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //strQuery = "select * from TM_FollowUp_MST";
               // strQuery = "select * from TM_FollowUp_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + "";
                strQuery = "select nFollowUpID,sDescription,bIsSystem  from TM_FollowUp_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + "";
                if (sFilter.Trim() != "")
                    strQuery += " and " + sFilter;
                //
                oDB.Retrive_Query(strQuery, out dtFollowType);
                if (dtFollowType != null)
                {
                    //dbo.TM_FollowUp_MST,nFollowUpID,sDescription,bIsSystem
                    for (int i = 0; i <= dtFollowType.Rows.Count - 1; i++)
                    {
                        oFollowUp = new gloTasksMails.Common.Followup();
                        oFollowUp.ID = Convert.ToInt64(dtFollowType.Rows[i]["nFollowUpID"]);
                        oFollowUp.Description = dtFollowType.Rows[i]["sDescription"].ToString();
                        oFollowUp.IsSystem = Convert.ToBoolean(dtFollowType.Rows[i]["bIsSystem"]);

                        oFollowUps.Add(oFollowUp);

                        oFollowUp = null;
                    }
                    return oFollowUps;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dtFollowType.Dispose();
                oFollowUps.Dispose();
                // oParameters.Dispose();

            }

        }

        public Common.Followup GetFollowUp(Int64 FollowUpID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtFollowType = new DataTable();
            gloTasksMails.Common.Followup oFollowUp = new gloTasksMails.Common.Followup();

            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //strQuery = "select * from TM_FollowUp_MST where nFollowUpID=" + FollowUpID;  //Remove select *
                strQuery = "select nFollowUpID,sDescription,bIsSystem  from TM_FollowUp_MST where nFollowUpID=" + FollowUpID;

                oDB.Retrive_Query(strQuery, out dtFollowType);
                if (dtFollowType != null)
                {
                    //dbo.TM_FollowUp_MST,nFollowUpID,sDescription,bIsSystem
                    for (int i = 0; i <= dtFollowType.Rows.Count - 1; i++)
                    {

                        oFollowUp.ID = Convert.ToInt64(dtFollowType.Rows[i]["nFollowUpID"]);
                        oFollowUp.Description = dtFollowType.Rows[i]["sDescription"].ToString();
                        oFollowUp.IsSystem = Convert.ToBoolean(dtFollowType.Rows[i]["bIsSystem"]);
                    }
                    return oFollowUp;
                }
                return null;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dtFollowType.Dispose();
                oFollowUp.Dispose();
            }

        }

        #endregion " FollowUp "

        #region " Status "
        //Status
        public Int64 Add(Common.Status oStatus)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Object oResult = new object();
            try
            {

                oResult = null;

                oDB.Connect(false);
                //dbo.TM_Status_MST,nStatusID,sDescription
                oParameters.Add("@nStatusID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sDescription", oStatus.Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                _result = oDB.Execute("TM_INUP_StatusType", oParameters, out oResult);

                if (oResult != null)
                {
                    _result = Convert.ToInt64(oResult);
                    if (!(_result > 0))
                    {
                        MessageBox.Show("Error Adding Record", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;

                    }
                }
                return _result;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();

            }
        }

        public bool Modify(Int64 StatusID, Common.Status oStatus)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Object oResult = new object();
            try
            {

                oResult = null;

                oDB.Connect(false);
                //dbo.TM_Status_MST,nStatusID,sDescription
                oParameters.Add("@nStatusID", oStatus.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sDescription", oStatus.Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                _result = oDB.Execute("TM_INUP_StatusType", oParameters, out oResult);

                if (oResult != null)
                {
                    _result = Convert.ToInt64(oResult);
                    if (!(_result > 0))
                    {
                        MessageBox.Show("Error Adding Record", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;

                    }
                }
                return true;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                oStatus.Dispose();

            }
        }

        public bool Modify(Common.Status oStatus)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Object oResult = new object();
            try
            {

                oResult = null;

                oDB.Connect(false);
                //dbo.TM_Status_MST,nStatusID,sDescription
                oParameters.Add("@nStatusID", oStatus.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sDescription", oStatus.Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                _result = oDB.Execute("TM_INUP_StatusType", oParameters, out oResult);

                if (oResult != null)
                {
                    _result = Convert.ToInt64(oResult);
                    if (!(_result > 0))
                    {
                        MessageBox.Show("Error Adding Record", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;

                    }
                }
                return true;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oParameters.Dispose();
                oStatus.Dispose();

            }
        }

        public bool DeleteStatus(Int64 StatusID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //dbo.TM_Status_MST,nStatusID,sDescription
                strQuery = "delete from TM_Status_MST where nStatusID =" + StatusID;
                int result = oDB.Execute_Query(strQuery);

                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

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
                //oParameters.Dispose();

            }
        }

        public bool CanDeleteStatus(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            //int _result = 0;
            object _IsSystem = null;
            DataTable dt;
            try
            {
                oDB.Connect(false);

                //Check if the Status is System Defined
                strQuery = "select bIsSystem from TM_Status_MST where nStatusID = " + ID;
                _IsSystem = oDB.ExecuteScalar_Query(strQuery);
                if (Convert.ToInt64(_IsSystem) == 1)
                {
                    return false;
                }
                strQuery = "";


                strQuery = "";
                strQuery = "SELECT nTaskID FROM TM_Task_Progress WHERE nStatusID = " + ID;
                oDB.Retrive_Query(strQuery, out dt);
                dt = new DataTable();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return false;
                }
                dt.Dispose();

                return true;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }

        }

        public bool BlockStatus(Int64 StatusId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "UPDATE TM_Status_MST SET bIsBlocked = '" + true + "' WHERE nStatusID = " + StatusId;
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool UnblockStatus(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE TM_Status_MST SET bIsBlocked = '" + false + "' WHERE nStatusID = " + ID + " ";
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool DeleteAllStatus()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            Int64 _statusId = 0;
            DataTable dt = new DataTable();
            int _result = 0;
            try
            {
                oDB.Connect(false);

                strQuery = " select nStatusID from TM_Status_MST where bIsBlocked = '" + true + "' and nClinicID = " + this.ClinicID + " ";
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _statusId = Convert.ToInt64(dt.Rows[i][0]);

                        if (CanDeleteStatus(_statusId))
                        {
                            strQuery = "";
                            strQuery = " DELETE FROM TM_Status_MST WHERE nStatusID = " + _statusId + " ";
                            _result = oDB.Execute_Query(strQuery);
                            _statusId = 0;
                        }

                    }
                }

                return Convert.ToBoolean(_result);

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool ClearStatuses()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " DELETE FROM TM_Status_MST WHERE bIsSystem = '" + false + "' AND nClinicID = " + this.ClinicID + " ";
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public DataTable GetBlockedStatuses()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtStatusType = new DataTable();
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "select nStatusID,sDescription,bIsSystem from TM_Status_MST where bIsBlocked = '" + true + "' AND nClinicID = " + this.ClinicID + " ";
                oDB.Retrive_Query(strQuery, out dtStatusType);
                return dtStatusType;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
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
                dtStatusType.Dispose();

            }

        }

        public bool IsDeleteStatus(Int64 StatusID)
        {
            return false;
        }

        public bool IsExistsStatus(string StatusName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtStatusType = new DataTable();
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //dbo.TM_Status_MST,nStatusID,StatusName
                //strQuery = "select count(nStatusID) from TM_Status_MST where sDescription = '" + StatusName + " ' ";
                strQuery = "select count(nStatusID) from TM_Status_MST where sDescription = '" + StatusName.Replace("'", "''") + " ' AND nClinicID = " + this.ClinicID + " ";
                //
                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(strQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

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
                dtStatusType.Dispose();

                // oParameters.Dispose();

            }
        }

        public Common.Statuses GetStatuses()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtStatusType = new DataTable();
            gloTasksMails.Common.Status oStatus;
            gloTasksMails.Common.Statuses oStatues = new gloTasksMails.Common.Statuses();
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //dbo.TM_Status_MST,nStatusID,sDescription
                //strQuery = "select * from TM_Status_MST";
                // strQuery = "select * from TM_Status_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + " ";  //Remove select *              
                strQuery = "select nStatusID,sDescription,bIsSystem from TM_Status_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + " ";                
                //
                oDB.Retrive_Query(strQuery, out dtStatusType);
                if (dtStatusType != null)
                {
                    for (int i = 0; i <= dtStatusType.Rows.Count - 1; i++)
                    {
                        oStatus = new gloTasksMails.Common.Status();
                        oStatus.ID = Convert.ToInt64(dtStatusType.Rows[i]["nStatusID"]);
                        oStatus.Description = dtStatusType.Rows[i]["sDescription"].ToString();
                        oStatus.IsSystem = Convert.ToBoolean(dtStatusType.Rows[i]["bIsSystem"]);

                        oStatues.Add(oStatus);

                        oStatus = null;
                    }
                    return oStatues;


                }
                return null;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dtStatusType.Dispose();
                oStatues.Dispose();
                // oParameters.Dispose();

            }

        }

        public Common.Statuses GetStatuses(String sFilter)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtStatusType = new DataTable();
            gloTasksMails.Common.Status oStatus;
            gloTasksMails.Common.Statuses oStatues = new gloTasksMails.Common.Statuses();
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //dbo.TM_Status_MST,nStatusID,sDescription
                //strQuery = "select * from TM_Status_MST";
                // strQuery = "select * from TM_Status_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + " "; //Remove select *
                strQuery = "select nStatusID,sDescription,bIsSystem from TM_Status_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + " ";
                if (sFilter.Trim() != "")
                    strQuery += " and " + sFilter;
                //
                oDB.Retrive_Query(strQuery, out dtStatusType);
                if (dtStatusType != null)
                {
                    for (int i = 0; i <= dtStatusType.Rows.Count - 1; i++)
                    {
                        oStatus = new gloTasksMails.Common.Status();
                        oStatus.ID = Convert.ToInt64(dtStatusType.Rows[i]["nStatusID"]);
                        oStatus.Description = dtStatusType.Rows[i]["sDescription"].ToString();
                        oStatus.IsSystem = Convert.ToBoolean(dtStatusType.Rows[i]["bIsSystem"]);

                        oStatues.Add(oStatus);

                        oStatus = null;
                    }
                    return oStatues;


                }
                return null;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dtStatusType.Dispose();
                oStatues.Dispose();
                // oParameters.Dispose();

            }

        }

        public Common.Status GetStatus(Int64 StatusID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtStatusType = new DataTable();
            gloTasksMails.Common.Status oStatus;

            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //dbo.TM_Status_MST,nStatusID,sDescription
                // strQuery = "select * from TM_Status_MST where nStatusID=" + StatusID; //Remove select *
                strQuery = "select nStatusID,sDescription,bIsSystem  from TM_Status_MST where nStatusID=" + StatusID;

                oDB.Retrive_Query(strQuery, out dtStatusType);
                if (dtStatusType != null)
                {
                    oStatus = new gloTasksMails.Common.Status();
                    for (int i = 0; i <= dtStatusType.Rows.Count - 1; i++)
                    {

                        oStatus.ID = Convert.ToInt64(dtStatusType.Rows[i]["nStatusID"]);
                        oStatus.Description = dtStatusType.Rows[i]["sDescription"].ToString();
                        oStatus.IsSystem = Convert.ToBoolean(dtStatusType.Rows[i]["bIsSystem"]);

                    }
                    return oStatus;


                }
                return null;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dtStatusType.Dispose();

                // oParameters.Dispose();

            }

        }

        #endregion " Status "

        #region " Priority "
        //Priority
        public Int64 Add(Common.Priority oPriority)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Object oResult = new object();
            try
            {
                oResult = null;
                oDB.Connect(false);
                //dbo.TM_Priority_MST,nPriorityID,sDescription
                oParameters.Add("@nPriorityID", 0, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sDescription", oPriority.Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nLevel", oPriority.PriorityLevel, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                _result = oDB.Execute("TM_INUP_PriorityType", oParameters, out oResult);

                if (oResult != null)
                {
                    _result = Convert.ToInt64(oResult);
                    if (!(_result > 0))
                    {
                        MessageBox.Show("Error Adding Record", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;

                    }
                }
                return _result;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                oParameters.Dispose();
                oParameters = null;

                oResult = null;
            }
        }

        public bool Modify(Int64 PriorityID, Common.Priority oPriority)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Object oResult = new object();
            try
            {
                oResult = null;
                oDB.Connect(false);
                //dbo.TM_Priority_MST,nPriorityID,sDescription
                oParameters.Add("@nPriorityID", oPriority.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sDescription", oPriority.Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nLevel", oPriority.PriorityLevel, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                _result = oDB.Execute("TM_INUP_PriorityType", oParameters, out oResult);

                if (oResult != null)
                {
                    _result = Convert.ToInt64(oResult);
                    if (!(_result > 0))
                    {
                        MessageBox.Show("Error Adding Record", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;

                    }
                }
                return true;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                oParameters.Dispose();
                oParameters = null;

                oResult = null;
            }
        }

        public bool Modify(Common.Priority oPriority)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Int64 _result = 0;
            Object oResult = new object();
            try
            {
                oResult = null;
                oDB.Connect(false);
                //dbo.TM_Priority_MST,nPriorityID,sDescription
                oParameters.Add("@nPriorityID", oPriority.ID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sDescription", oPriority.Description, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nLevel", oPriority.PriorityLevel, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                _result = oDB.Execute("TM_INUP_PriorityType", oParameters, out oResult);

                if (oResult != null)
                {
                    _result = Convert.ToInt64(oResult);
                    if (!(_result > 0))
                    {
                        MessageBox.Show("Error Adding Record", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;

                    }
                }
                return true;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                oParameters.Dispose();
                oParameters = null;

                oResult = null;
            }
        }

        public bool DeletePriority(Int64 PriorityID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //dbo.TM_Priority_MST,nPriorityID,sDescription
                strQuery = "delete from TM_Priority_MST where nPriorityID =" + PriorityID;
                int result = oDB.Execute_Query(strQuery);
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

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
                oDB = null;

                strQuery = null;

                //oParameters.Dispose();

            }
        }

        public bool CanDeletePriority(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            //int _result = 0;
            DataTable dt;
            object _IsSystem = null;
            try
            {
                oDB.Connect(false);

                //Check for the Priority is not System defined.
                strQuery = "select bIsSystem from TM_Priority_MST where nPriorityID = " + ID;
                _IsSystem = oDB.ExecuteScalar_Query(strQuery);
                if (Convert.ToInt64(_IsSystem) == 1)
                {
                    return false;
                }
                strQuery = "";

                strQuery = "Select nDraftMailID from TM_Mail_Drafts where nPriorityID = " + ID;
                oDB.Retrive_Query(strQuery, out dt);
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return false;
                }
                dt.Dispose();

                strQuery = "Select nMailID from TM_Mail_Inbox where nPriorityID = " + ID;
                dt = new DataTable();
                oDB.Retrive_Query(strQuery, out dt);
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return false;
                }
                dt.Dispose();

                strQuery = "Select nMailID from TM_Mail_SentItem where nPriorityID = " + ID;
                oDB.Retrive_Query(strQuery, out dt);
                dt = new DataTable();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return false;
                }
                dt.Dispose();

                strQuery = "Select nTaskID from TM_TaskMST where nPriorityID = " + ID;
                oDB.Retrive_Query(strQuery, out dt);
                dt = new DataTable();
                if (dt.Rows.Count > 0 && dt != null)
                {
                    return false;
                }
                dt.Dispose();

                return true;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                strQuery = null;
            }

        }

        public bool BlockPriority(Int64 PriorityId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "UPDATE TM_Priority_MST SET bIsBlocked = '" + true + "' WHERE nPriorityID = " + PriorityId;
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                strQuery = null;
            }
        }

        public bool UnblockPriority(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE TM_Priority_MST SET bIsBlocked = '" + false + "' WHERE nPriorityID = " + ID + " ";
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                strQuery = null;
            }
        }

        public DataTable GetBlockedPriorities()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPriorityType = new DataTable();

            string strQuery = "";
            try
            {
                oDB.Connect(false);

                strQuery = "select nPriorityID,sDescription,bIsSystem from TM_Priority_MST where bIsBlocked = '" + true + "' AND nClinicID = " + this.ClinicID + " ";
                oDB.Retrive_Query(strQuery, out dtPriorityType);
                return dtPriorityType;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
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
                oDB = null;

                strQuery = null;
            }
        }

        public bool DeleteAllPriorities()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            DataTable dt = new DataTable();
            Int64 _priorityId = 0;
            int _result = 0;
            try
            {
                oDB.Connect(false);

                strQuery = " select nPriorityID from TM_Priority_MST where bIsBlocked = '" + true + "' and nClinicID = " + this.ClinicID + " ";
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _priorityId = Convert.ToInt64(dt.Rows[i][0]);

                        if (CanDeletePriority(_priorityId))
                        {
                            strQuery = "";
                            strQuery = " DELETE FROM TM_Priority_MST WHERE nPriorityID = " + _priorityId + " ";
                            _result = oDB.Execute_Query(strQuery);
                            _priorityId = 0;
                        }

                    }
                }

                return Convert.ToBoolean(_result);

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                strQuery = null;
            }
        }

        public bool ClearPriorities()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " DELETE FROM TM_Priority_MST WHERE bIsSystem = '" + false + "' AND nClinicID = " + this.ClinicID + " ";
                int _result = oDB.Execute_Query(strQuery);
                return Convert.ToBoolean(_result);

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;

                strQuery = null;
            }
        }

        public bool IsDeletePriority(Int64 PriorityID)
        {
            return false;
        }

        public bool IsExistsPriority(string PriorityName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //dbo.TM_Priority_MST,nPriorityID,sDescription
                //strQuery = "select count(nPriorityID) from TM_Priority_MST where sDescription = '" + PriorityName + " ' ";
                strQuery = "select count(nPriorityID) from TM_Priority_MST where sDescription = '" + PriorityName.Replace("'", "''") + " ' AND nClinicID = " + this.ClinicID + " ";
                //
                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(strQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

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
                oDB = null;

                strQuery = null;

                // oParameters.Dispose();

            }
        }

        public Common.Priorities GetPriorities()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPriorityType = new DataTable();
            gloTasksMails.Common.Priority oPriority;
            gloTasksMails.Common.Priorities oPriorities = new gloTasksMails.Common.Priorities();


            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //strQuery = "select * from TM_Priority_MST";
                // strQuery = "select * from TM_Priority_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + " "; //Remove select *
                strQuery = "select nPriorityID,sDescription,bIsSystem,nLevel  from TM_Priority_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + " ";


                oDB.Retrive_Query(strQuery, out dtPriorityType);
                if (dtPriorityType != null)
                {

                    for (int i = 0; i <= dtPriorityType.Rows.Count - 1; i++)
                    {
                        //dbo.TM_Priority_MST,nPriorityID,sDescription,bIsSystem
                        oPriority = new gloTasksMails.Common.Priority();
                        oPriority.ID = Convert.ToInt64(dtPriorityType.Rows[i]["nPriorityID"]);
                        oPriority.Description = dtPriorityType.Rows[i]["sDescription"].ToString();
                        oPriority.IsSystem = Convert.ToBoolean(dtPriorityType.Rows[i]["bIsSystem"]);
                        //
                        oPriority.PriorityLevel = Convert.ToInt64(dtPriorityType.Rows[i]["nLevel"]);
                        //

                        oPriorities.Add(oPriority);

                        oPriority = null;
                    }
                    return oPriorities;


                }
                return null;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                oDB = null;

                dtPriorityType.Dispose();
                dtPriorityType = null;

                oPriorities.Dispose();
                oPriorities = null;

                strQuery = null;

                // oParameters.Dispose();

            }
        }

        public Common.Priorities GetPriorities(String sFilter)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPriorityType = new DataTable();
            gloTasksMails.Common.Priority oPriority;
            gloTasksMails.Common.Priorities oPriorities = new gloTasksMails.Common.Priorities();


            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //strQuery = "select * from TM_Priority_MST";
                // strQuery = "select * from TM_Priority_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + " "; //Remove select *
                strQuery = "select  nPriorityID,sDescription,bIsSystem,nLevel  from TM_Priority_MST where bIsBlocked='" + false + "' AND nClinicID = " + this.ClinicID + " ";
                //
                if (sFilter != "")
                    strQuery += " and " + sFilter;

                oDB.Retrive_Query(strQuery, out dtPriorityType);
                if (dtPriorityType != null)
                {

                    for (int i = 0; i <= dtPriorityType.Rows.Count - 1; i++)
                    {
                        //dbo.TM_Priority_MST,nPriorityID,sDescription,bIsSystem
                        oPriority = new gloTasksMails.Common.Priority();
                        oPriority.ID = Convert.ToInt64(dtPriorityType.Rows[i]["nPriorityID"]);
                        oPriority.Description = dtPriorityType.Rows[i]["sDescription"].ToString();
                        oPriority.IsSystem = Convert.ToBoolean(dtPriorityType.Rows[i]["bIsSystem"]);
                        //
                        oPriority.PriorityLevel = Convert.ToInt64(dtPriorityType.Rows[i]["nLevel"]);
                        //

                        oPriorities.Add(oPriority);

                        oPriority = null;
                    }
                    return oPriorities;


                }
                return null;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                oDB = null;

                dtPriorityType.Dispose();
                dtPriorityType = null;

                oPriorities.Dispose();
                oPriorities = null;

                strQuery = null;

                // oParameters.Dispose();

            }
        }

        public Common.Priority GetPriority(Int64 PriorityID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPriorityType = new DataTable();
            gloTasksMails.Common.Priority oPriority;

            string strQuery = "";
            try
            {
                oDB.Connect(false);
                //strQuery = "select * from TM_Priority_MST where nPriorityID=" + PriorityID; //Remove select *
                strQuery = "select nPriorityID,sDescription,bIsSystem,nLevel from TM_Priority_MST where nPriorityID=" + PriorityID;

                oDB.Retrive_Query(strQuery, out dtPriorityType);
                if (dtPriorityType != null)
                {
                    oPriority = new gloTasksMails.Common.Priority();
                    for (int i = 0; i <= dtPriorityType.Rows.Count - 1; i++)
                    {
                        //dbo.TM_Priority_MST,nPriorityID,sDescription,bIsSystem

                        oPriority.ID = Convert.ToInt64(dtPriorityType.Rows[i]["nPriorityID"]);
                        oPriority.Description = dtPriorityType.Rows[i]["sDescription"].ToString();
                        oPriority.IsSystem = Convert.ToBoolean(dtPriorityType.Rows[i]["bIsSystem"]);
                        oPriority.PriorityLevel = Convert.ToInt64(dtPriorityType.Rows[i]["nLevel"]);
                    }
                    return oPriority;
                }
                return null;

            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                MessageBox.Show("ERROR : " + dbErr.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                oDB = null;

                dtPriorityType.Dispose();
                dtPriorityType = null;

                strQuery = null;
            }
        }

        #endregion " Priority "

        #region " UI Method "

        public void ShowTaskMailBook(System.Windows.Forms.Form oParentWindow)
        {

            //frmViewTaskMailBook ofrmTaskMail = new frmViewTaskMailBook();
            frmViewTaskMailBook ofrmTaskMail = frmViewTaskMailBook.GetInstance();
            ofrmTaskMail.DataBaseConnectionString = _databaseconnectionstring;
            ofrmTaskMail.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ofrmTaskMail.MdiParent = oParentWindow;
            //calling gloCommunityViewDataform for Taskmail Download.
            ofrmTaskMail.EvntgloCommunityHandler += getTaskmailHandler;
            //end
            ofrmTaskMail.Show();
        }

        private void getTaskmailHandler()
        {
            if (EvntTaskmailHandler != null)
                EvntTaskmailHandler();
        }
        #endregion " UI Method "
    }

    namespace Common
    {
        public class Category : IDisposable
        {
            #region "Constructor & Distructor"


            public Category()
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

            ~Category()
            {
                Dispose(false);
            }

            #endregion

            #region "Declarations"

            private Int64 _id = 0;
            private string _description = "";
            private Int32 _colorcode = 0;
            private bool _issystem = false;

            #endregion "Declarations"

            #region "Property Procedures"

            public Int64 ID
            {
                get { return _id; }
                set { _id = value; }
            }
            public string Description
            {
                get { return _description; }
                set { _description = value; }
            }
            public Int32 ColorCode
            {
                get { return _colorcode; }
                set { _colorcode = value; }
            }
            public bool IsSystem
            {
                get { return _issystem; }
                set { _issystem = value; }
            }

            #endregion "Property Procedures"
        }

        public class Categories : IDisposable
        {

            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public Categories()
            {
                _innerlist = new ArrayList();
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


            ~Categories()
            {
                Dispose(false);
            }
            #endregion

            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(Category item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(Category item)
            {
                bool result = false;
                Category obj;

                for (int i = 0; i < _innerlist.Count; i++)
                {
                    //store current index being checked
                    obj = new Category();
                    obj = (Category)_innerlist[i];
                    if (obj.ID == item.ID)
                    {
                        _innerlist.RemoveAt(i);
                        result = true;
                        break;
                    }
                    obj = null;
                }

                return result;
            }

            public bool RemoveAt(int index)
            {
                bool result = false;
                _innerlist.RemoveAt(index);
                result = true;
                return result;
            }

            public void Clear()
            {
                _innerlist.Clear();
            }

            public Category this[int index]
            {
                get
                {
                    return (Category)_innerlist[index];
                }
            }

            public bool Contains(Category item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(Category item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(Category[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }
        }

        public class Followup : IDisposable
        {
            #region "Constructor & Distructor"


            public Followup()
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

            ~Followup()
            {
                Dispose(false);
            }

            #endregion

            #region "Declarations"

            private Int64 _id = 0;
            private string _description = "";
            private bool _issystem = false;

            #endregion "Declarations"


            #region "Property Procedures"

            public Int64 ID
            {
                get { return _id; }
                set { _id = value; }
            }
            public string Description
            {
                get { return _description; }
                set { _description = value; }
            }
            public bool IsSystem
            {
                get { return _issystem; }
                set { _issystem = value; }
            }
            #endregion "Property Procedures"
        }

        public class Followups : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public Followups()
            {
                _innerlist = new ArrayList();
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


            ~Followups()
            {
                Dispose(false);
            }
            #endregion

            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(Followup item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(Followup item)
            {
                bool result = false;
                Followup obj;

                for (int i = 0; i < _innerlist.Count; i++)
                {
                    //store current index being checked
                    obj = new Followup();
                    obj = (Followup)_innerlist[i];
                    if (obj.ID == item.ID)
                    {
                        _innerlist.RemoveAt(i);
                        result = true;
                        break;
                    }
                    obj = null;
                }

                return result;
            }

            public bool RemoveAt(int index)
            {
                bool result = false;
                _innerlist.RemoveAt(index);
                result = true;
                return result;
            }

            public void Clear()
            {
                _innerlist.Clear();
            }

            public Followup this[int index]
            {
                get
                {
                    return (Followup)_innerlist[index];
                }
            }

            public bool Contains(Followup item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(Followup item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(Followup[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }

        }

        public class Status : IDisposable
        {
            #region "Constructor & Distructor"


            public Status()
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

            ~Status()
            {
                Dispose(false);
            }

            #endregion

            #region "Declarations"
            private Int64 _id = 0;
            private string _description = "";
            private bool _issystem = false;
            #endregion "Declarations"

            #region "Property Procedures"

            public Int64 ID
            {
                get { return _id; }
                set { _id = value; }
            }
            public string Description
            {
                get { return _description; }
                set { _description = value; }
            }
            public bool IsSystem
            {
                get { return _issystem; }
                set { _issystem = value; }
            }
            #endregion "Property Procedures"
        }

        public class Statuses : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public Statuses()
            {
                _innerlist = new ArrayList();
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


            ~Statuses()
            {
                Dispose(false);
            }
            #endregion

            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(Status item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(Status item)
            {
                bool result = false;
                Status obj;

                for (int i = 0; i < _innerlist.Count; i++)
                {
                    //store current index being checked
                    obj = new Status();
                    obj = (Status)_innerlist[i];
                    if (obj.ID == item.ID)
                    {
                        _innerlist.RemoveAt(i);
                        result = true;
                        break;
                    }
                    obj = null;
                }

                return result;
            }

            public bool RemoveAt(int index)
            {
                bool result = false;
                _innerlist.RemoveAt(index);
                result = true;
                return result;
            }

            public void Clear()
            {
                _innerlist.Clear();
            }

            public Status this[int index]
            {
                get
                {
                    return (Status)_innerlist[index];
                }
            }

            public bool Contains(Status item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(Status item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(Status[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }
        }

        public class Priority : IDisposable
        {
            #region "Constructor & Distructor"


            public Priority()
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

            ~Priority()
            {
                Dispose(false);
            }

            #endregion

            #region "Declarations"

            private Int64 _id = 0;
            private string _description = "";
            private bool _issystem = false;
            private Int64 _prioritylevel = 0;

            #endregion "Declarations"

            #region "Property Procedures"

            public Int64 ID
            {
                get { return _id; }
                set { _id = value; }
            }
            public string Description
            {
                get { return _description; }
                set { _description = value; }
            }
            public bool IsSystem
            {
                get { return _issystem; }
                set { _issystem = value; }
            }
            public Int64 PriorityLevel
            {
                get { return _prioritylevel; }
                set { _prioritylevel = value; }
            }

            #endregion "Property Procedures"
        }

        public class Priorities : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public Priorities()
            {
                _innerlist = new ArrayList();
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


            ~Priorities()
            {
                Dispose(false);
            }
            #endregion

            public int Count
            {
                get { return _innerlist.Count; }
            }

            public void Add(Priority item)
            {
                _innerlist.Add(item);
            }

            public bool Remove(Priority item)
            {
                bool result = false;
                Priority obj;

                for (int i = 0; i < _innerlist.Count; i++)
                {
                    //store current index being checked
                    obj = new Priority();
                    obj = (Priority)_innerlist[i];
                    if (obj.ID == item.ID)
                    {
                        _innerlist.RemoveAt(i);
                        result = true;
                        break;
                    }
                    obj = null;
                }

                return result;
            }

            public bool RemoveAt(int index)
            {
                bool result = false;
                _innerlist.RemoveAt(index);
                result = true;
                return result;
            }

            public void Clear()
            {
                _innerlist.Clear();
            }

            public Priority this[int index]
            {
                get
                {
                    return (Priority)_innerlist[index];
                }
            }

            public bool Contains(Priority item)
            {
                return _innerlist.Contains(item);
            }

            public int IndexOf(Priority item)
            {
                return _innerlist.IndexOf(item);
            }

            public void CopyTo(Priority[] array, int index)
            {
                _innerlist.CopyTo(array, index);
            }
        }

        #region " Enumerated DataType "

        public enum SelfAssigned
        {
            Self = 1,
            Assigned = 2
        }

        public enum AcceptRejectHold
        {
            Accept = 1,
            Reject = 2,
            Hold = 3,
            Deleted = 4  //added by pradeep 20101006 -to show status of self task deleted
        }

        public enum MailFlag
        {
            From = 1,
            To = 2,
            Cc = 3,
            BCc = 4

        }

        public enum SendingType
        {
            Reply = 1,
            ReplyAll = 2,
            Forward = 3
        }

        public enum ItemType
        {
            Inbox = 1,
            Outbox = 2,
            Draft = 3,
            Deleted = 4
        }

        #endregion " Enumerated DataType "
    }
}
