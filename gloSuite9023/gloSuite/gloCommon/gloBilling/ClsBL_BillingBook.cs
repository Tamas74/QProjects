using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections;
using gloAuditTrail;
using gloGlobal;
using System.ComponentModel;

namespace gloBilling
{
    #region "Category"
    public class Category
    {

        #region "Constructor & Distructor"

        public Category(string DatabseConnectionString)
        {
            _databaseconnectionstring = DatabseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
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
        private string _messageBoxCaption = "";

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

        #region "Private variables"
        //private string _MessageBoxCaption = "gloPM";
        private string _databaseconnectionstring = "";


        private Int64 _CategoryID = 0;

        private string _CategoryDescription = "";

        private string _CategoryType = "";

        private string _Code = "";

        private string _ParentCode = "";

        private Int64 _ParentId = 0;

        private bool _isFavorite = true;
        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;


        #endregion "Private variables"

        #region "Property Procedures"

        public bool IsFavorite
        {
            get { return _isFavorite; }
            set { _isFavorite = value; }
        }

        public Int64 ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public string ParentCode
        {
            get { return _ParentCode; }
            set { _ParentCode = value; }
        }

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        public string CategoryDescription
        {
            get { return _CategoryDescription; }
            set { _CategoryDescription = value; }
        }

        public string CategoryType
        {
            get { return _CategoryType; }
            set { _CategoryType = value; }
        }


        #endregion "Property Procedures"

        #region "Internal Functions"
        private bool IsCategoryUsedInHistory(string sCategoryName)
        {
            try
            {
                SqlConnection con = new SqlConnection(_databaseconnectionstring);
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) sHistoryCategory FROM History WITH (NOLOCK) WHERE sHistoryCategory = '" + sCategoryName.Replace("'", "''") + "'", con);
                object oResult = null;
                con.Open();
                oResult = cmd.ExecuteScalar();
                con.Close();
                con.Dispose();
                con = null;
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (Convert.ToInt16(oResult) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return true;
            }
        }

        private bool IsCategoryUsedInROS(string sCategoryName)
        {
            try
            {
                SqlConnection con = new SqlConnection(_databaseconnectionstring);
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) sROSCategory FROM ROS WITH (NOLOCK) WHERE sROSCategory = '" + sCategoryName.Replace("'", "''") + "'", con);
                object oResult = null;
                con.Open();
                oResult = cmd.ExecuteScalar();
                con.Close();
                con.Dispose();
                con = null;
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (Convert.ToInt16(oResult) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return true;
            }
        }
        #endregion "Internal Functions"

        public Int64 Add()
        {
            Int64 _result = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {
                #region " Delete DataDictionary Item "
                // SUDHIR 20090711 // TO MAINTAIN DYNAMIC DATA DICTIONARY FOR EMR //
                if (appSettings["MessageBOXCaption"] == "gloEMR" && _CategoryID > 0)
                {
                    gloOffice.gloDataDictionary oDictionary = new gloOffice.gloDataDictionary(_databaseconnectionstring);
                    DataTable dtCategory;
                    dtCategory = GetCategery(_CategoryID);
                    if (dtCategory != null && dtCategory.Rows.Count > 0)
                    {
                        string sDictionary;
                        if (dtCategory.Rows[0]["sCategoryType"].ToString() == "History")
                        {
                            if (IsCategoryUsedInHistory(dtCategory.Rows[0]["sDescription"].ToString()) == false)
                            {
                                sDictionary = "History.sHistoryItem+History.sComments|" + dtCategory.Rows[0]["sDescription"].ToString();
                                oDictionary.DeleteDataDictionary(sDictionary);
                            }
                        }
                        else if (dtCategory.Rows[0]["sCategoryType"].ToString() == "ROS")
                        {
                            if (IsCategoryUsedInROS(dtCategory.Rows[0]["sDescription"].ToString()) == false)
                            {
                                sDictionary = "ROS.sROSItem+ROS.sComments|" + dtCategory.Rows[0]["sDescription"].ToString();
                                oDictionary.DeleteDataDictionary(sDictionary);
                            }
                        }
                    }
                    if (dtCategory != null)
                    {
                        dtCategory.Dispose();
                        dtCategory = null;
                    }
                    oDictionary = null;
                }
                #endregion

                object _intresult = 0;
                oDBParameters.Add("@sCode", _Code, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@CategoryID", _CategoryID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@CategoryDescription", _CategoryDescription, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@CategoryType", _CategoryType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                if (Convert.ToString(_ParentCode) == "")
                {
                    oDBParameters.Add("@sParentCode", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                }
                else
                {
                    oDBParameters.Add("@sParentCode", _ParentCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                }
                if (_ParentId == 0)
                {
                    oDBParameters.Add("@nParentCategoryID", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                else
                {
                    oDBParameters.Add("@nParentCategoryID", _ParentId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                }
                oDBParameters.Add("@bIsFavorite", _isFavorite, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("BL_INUP_CategoryMST", oDBParameters, out _intresult);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }

                #region " Add DataDictionary Item "
                // SUDHIR 20090711 // TO MAINTAIN DYNAMIC DATA DICTIONARY FOR EMR //
                if (appSettings["MessageBOXCaption"] == "gloEMR")
                {
                    gloOffice.gloDataDictionary oDictionary = new gloOffice.gloDataDictionary(_databaseconnectionstring);
                    if (_CategoryType == "History")
                    {
                        oDictionary.AddDataDictionary("History.sHistoryItem+History.sComments|" + _CategoryDescription, "History", _CategoryDescription, "History");
                    }
                    else if (_CategoryType == "ROS")
                    {
                        oDictionary.AddDataDictionary("ROS.sROSItem+ROS.sComments|" + _CategoryDescription, "ROS", _CategoryDescription, "Review of Systems");
                    }
                    oDictionary = null;
                }
                // END SUDHIR //
                #endregion
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }

        public bool Modify()
        {
            bool _result = false;


            return _result;
        }

        public bool Block(Int64 nCategoryId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "UPDATE Category_MST WITH (READPAST) SET bIsBlocked = '" + true + "' WHERE nCategoryID = " + nCategoryId;
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public bool Unblock(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE Category_MST WITH (READPAST) SET bIsBlocked = '" + false + "' WHERE nCategoryID = " + ID + " ";
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public bool DeleteAll()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            DataTable dt = null;
            int _result = 0;
            Int64 _categoryId = 0;

            try
            {
                oDB.Connect(false);

                //get list of blocked items
                strQuery = " SELECT nCategoryID FROM Category_MST WITH (NOLOCK) WHERE bIsBlocked = 'true' AND nClinicID = " + this.ClinicID + " ";
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _categoryId = 0;
                        _categoryId = Convert.ToInt64(dt.Rows[i][0]);
                        if (CanDelete(_categoryId))
                        {
                            strQuery = " DELETE FROM Category_MST WHERE nCategoryID = " + _categoryId;
                            _result = oDB.Execute_Query(strQuery);
                        }
                        else
                        {
                            MessageBox.Show("Cannot Delete :Item in use or System defined", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Cannot Delete :Item in use or System defined", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }

            }
        }

        public bool CanDelete(Int64 ID)
        {
            //Logic need to be implemented
            return false;
        }

        public bool Delete(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from Category_MST where nCategoryID =" + ID;
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
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public System.Data.DataTable GetList()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = null;
            System.Data.DataTable _result = null;
            string _sqlQuery = string.Empty;

            try
            {
                _sqlQuery = "gsp_ViewCategory_Mst";

                oDB.Connect(false);

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sCategoryType", DBNull.Value, ParameterDirection.Input, SqlDbType.VarChar, 50);

                oDB.Retrive(_sqlQuery, oParameters, out _result);

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oParameters != null) { oParameters.Clear(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        public System.Data.DataTable GetList(String CategoryType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = null;
            System.Data.DataTable _result = null;
            string _sqlQuery = string.Empty;

            try
            {
                _sqlQuery = "gsp_ViewCategory_Mst";

                oDB.Connect(false);
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sCategoryType", CategoryType, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDB.Retrive(_sqlQuery, oParameters, out _result);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oParameters != null) { oParameters.Clear(); oParameters = null; }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }

        public System.Data.DataTable GetBlockedCategories()
        {
            System.Data.DataTable _result = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //string _sqlQuery = "SELECT nCategoryID, sDescription, sCategoryType FROM Category_MST ";
                //
                string _sqlQuery = "SELECT nCategoryID, sDescription, sCategoryType FROM Category_MST WITH (NOLOCK) WHERE bIsBlocked = '" + true + "' AND nClinicID = " + this.ClinicID + " ";
                //
                oDB.Retrive_Query(_sqlQuery, out _result);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }

        public System.Data.DataTable GetCategery(Int64 catID)
        {
            System.Data.DataTable _result = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "SELECT nCategoryID, sDescription, sCategoryType,sCode,bFavorites,nParentCategoryId  FROM Category_MST WITH (NOLOCK) where nCategoryID='" + catID + "'";
                oDB.Retrive_Query(_sqlQuery, out _result);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }

        public bool IsExists(Int64 catID, string catName, string catType)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //nPatientRelID, sRelationship, bIsBlock, nClinicID
                //PatientRelationship
                //nAppointmentTypeID, sAppointmentType, nDuration, sColorCode, bIsBlocked, nClinicID
                string _sqlQuery = "";

                if (catType.Contains("Race"))
                {
                    if (catID == 0)
                    {
                        _sqlQuery = "SELECT Count(nCategoryID) FROM Category_MST WITH (NOLOCK) WHERE (sDescription ='" + catName.Replace("'", "''") + "' AND sCategoryType IN ('Race','Race Specification')) AND nClinicID = " + this.ClinicID + " ";
                    }
                    else
                    {
                        _sqlQuery = "SELECT Count(nCategoryID) FROM Category_MST WITH (NOLOCK) WHERE (sDescription='" + catName.Replace("'", "''") + "' AND sCategoryType IN ('Race','Race Specification') AND nCategoryID<> " + catID + ") AND nClinicID = " + this.ClinicID + " ";
                    }
                }
                else if (catType.Contains("Ethnicity"))
                {
                    if (catID == 0)
                    {
                        _sqlQuery = "SELECT Count(nCategoryID) FROM Category_MST WITH (NOLOCK) WHERE (sDescription ='" + catName.Replace("'", "''") + "' AND sCategoryType IN ('Ethnicity','Ethnicity Specification')) AND nClinicID = " + this.ClinicID + " ";
                    }
                    else
                    {
                        _sqlQuery = "SELECT Count(nCategoryID) FROM Category_MST WITH (NOLOCK) WHERE (sDescription='" + catName.Replace("'", "''") + "' AND sCategoryType IN ('Ethnicity','Ethnicity Specification') AND nCategoryID<> " + catID + ") AND nClinicID = " + this.ClinicID + " ";
                    }
                }
                else
                {
                    if (catID == 0)
                    {
                        _sqlQuery = "SELECT Count(nCategoryID) FROM Category_MST WITH (NOLOCK) WHERE (sDescription ='" + catName.Replace("'", "''") + "' AND sCategoryType='" + catType.Replace("'", "''") + "') AND nClinicID = " + this.ClinicID + " ";
                    }
                    else
                    {
                        _sqlQuery = "SELECT Count(nCategoryID) FROM Category_MST WITH (NOLOCK) WHERE (sDescription='" + catName.Replace("'", "''") + "' AND sCategoryType='" + catType.Replace("'", "''") + "' AND nCategoryID<> " + catID + ") AND nClinicID = " + this.ClinicID + " ";
                    }
                }

                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }

        public Boolean IsCPTCategoryInUse(long CategoryId, string CategoryDescription)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dt = null;
            Boolean _isCPTCategoryInUse = false;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nCategoryID", CategoryId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sCategoryDesc", CategoryDescription, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("Check_For_IsCPTCategoryInUse", oParameters, out _dt);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    if (Convert.ToInt64(_dt.Rows[0]["CPTCategoryCount"]) > 0)
                    {
                        _isCPTCategoryInUse = true;
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
            }

            return _isCPTCategoryInUse;
        }

        public Int64 UpdateCPTCategoryInUse(long CategoryId)//, string CategoryDescription
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            object _intresult = 0;
            Int64 _result = 0;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nCategoryID", CategoryId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nRowupdated", _result, ParameterDirection.Output, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Execute("UpdtInUseCPTMstCatWithCatMst", oParameters, out _intresult);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }
    }
    #endregion

    #region  " Credit Card Types "

    public class CreditCards
    {

        #region "Private variables"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        //private string _MessageBoxCaption = "gloPM";
        private string _databaseconnectionstring = "";
        private Int64 _CreditCardId = 0;
        private string _CreditCardDesc = "";
        private bool _isblocked = false;
        private Int64 _ClinicID = 0;

        #endregion "Private variables"

        #region " Property Procedures "

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }
        public Int64 CreditCardID
        {
            get { return _CreditCardId; }
            set { _CreditCardId = value; }
        }
        public string CreditCardDesc
        {
            get { return _CreditCardDesc; }
            set { _CreditCardDesc = value; }
        }
        public bool IsBlocked
        {
            get { return _isblocked; }
            set { _isblocked = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion " Property Procedures "

        #region "Constructor & Distructor"

        public CreditCards(string DatabseConnectionString)
        {
            _databaseconnectionstring = DatabseConnectionString;

            #region " Retrieve ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        private bool disposed = false;
        private string _messageBoxCaption = "";

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

        ~CreditCards()
        {
            Dispose(false);
        }

        #endregion

        #region " Public & Private Methods "

        public Int64 Add()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object _intresult = 0;
            Int64 _result = 0;

            try
            {
                oDB.Connect(false);

                oDBParameters.Add("@nCreditCardID", _CreditCardId, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sCreditCardDesc", _CreditCardDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@bIsBlocked", false, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("BL_INUP_CreditCardMST", oDBParameters, out _intresult);
                oDB.Disconnect();

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            { ex.ERROR_Log(ex.ToString()); }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        public bool Block(Int64 creditcardid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " Delete From BL_CreditCard_MST where nCreditCardID = " + creditcardid + " AND nClinicID = " + ClinicID + "";
                int _result = oDB.Execute_Query(strQuery);
                oDB.Disconnect();
                return Convert.ToBoolean(_result);
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

        public bool checkExistsCCType(string creditcardDesc)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            Int64 _itemCount = 0;
            Boolean _result = false;
            try
            {
                oDB.Connect(false);
                strQuery = " SELECT COUNT(nCreditID) FROM Credits_Ext WITH (NOLOCK) WHERE sCreditCardType = '" + creditcardDesc.Replace("'", "''").Trim() + "' AND nClinicID = " + ClinicID;
                _itemCount = Convert.ToInt64(oDB.ExecuteScalar_Query(strQuery));
                if (_itemCount > 0)
                {
                    _result = true;
                }
                oDB.Disconnect();
                return _result;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

        public bool Unblock(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE BL_CreditCard_MST WITH (READPAST) SET  bIsBlocked = '" + false + "' where nCreditCardID = " + ID + " AND nClinicID = " + ClinicID + "";
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public bool IsExists(Int64 creditcardid, string carddesc)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            bool _result = false;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                if (creditcardid == 0)
                {
                    _sqlQuery = "SELECT Count(nCreditCardID) FROM BL_CreditCard_MST WITH (NOLOCK)  " +
                        " WHERE UPPER(sCreditCardDesc) ='" + carddesc.Replace("'", "''").ToUpper().Trim() + "' AND nClinicID = " + this.ClinicID + " ";
                }
                else
                {
                    _sqlQuery = " SELECT COUNT(nCreditCardID) FROM BL_CreditCard_MST WITH (NOLOCK) " +
                    " WHERE UPPER(sCreditCardDesc) ='" + carddesc.Replace("'", "''").ToUpper().Trim() + "' " +
                    " AND nCreditCardID <> " + creditcardid + " " +
                    " AND nClinicID = " + this.ClinicID + " ";
                }

                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }

        public System.Data.DataTable GetList()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            System.Data.DataTable _result = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);

                //_sqlQuery = " SELECT nCreditCardID,ISNULL(sCreditCardDesc,'') AS sCreditCardDesc,ISNULL(nClinicID,0) AS nClinicID, " +
                //" ISNULL(bIsBlocked,0) AS bIsBlocked " +
                //" FROM BL_CreditCard_MST  " +
                //" WHERE bIsBlocked = '"+false+"' AND nClinicID = "+this.ClinicID+" ";


                ////*** modified by vishal for replace (' ') with (')
                //_sqlQuery = " SELECT nCreditCardID, ISNULL(Replace(sCreditCardDesc,'''''',''''),'') AS sCreditCardDesc,ISNULL(nClinicID,0) AS nClinicID, "+
                //    " ISNULL(bIsBlocked,0) AS bIsBlock "+
                //    " FROM BL_CreditCard_MST  " +
                //    " WHERE bIsBlocked = '"+false+"' AND nClinicID = "+this.ClinicID+" ";

                //*** modified by MaheshB for replace (' ') with (')
                _sqlQuery = " SELECT nCreditCardID, sCreditCardDesc,ISNULL(nClinicID,0) AS nClinicID, " +
                    " ISNULL(bIsBlocked,0) AS bIsBlock " +
                    " FROM BL_CreditCard_MST WITH (NOLOCK)  " +
                    " WHERE bIsBlocked = '" + false + "' AND nClinicID = " + this.ClinicID + " ";


                oDB.Retrive_Query(_sqlQuery, out _result);
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }

        public System.Data.DataTable GetCreditCard(Int64 cardId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            System.Data.DataTable _result = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                //*** modified for replace (' ') with (') while retieving from db..
                _sqlQuery = " SELECT nCreditCardID,ISNULL(REPLACE(sCreditCardDesc,'''''',''''),'') AS sCreditCardDesc,ISNULL(nClinicID,0) AS nClinicID, " +
                    " ISNULL(bIsBlocked,0) AS bIsBlocked " +
                    " FROM BL_CreditCard_MST WITH (NOLOCK)  " +
                    " WHERE nCreditCardID = " + cardId + " AND ISNULL(bIsBlocked,0) = 'false' AND nClinicID = " + ClinicID + "";
                oDB.Retrive_Query(_sqlQuery, out _result);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }

        #endregion " Public & Private Methods "

    }

    #endregion  " Credit Card Types "

    #region "CPT"
    public enum nCostPer
    {
        Patient = 0,
        Visit = 1,
        Unit = 2
    }

    public class CPT
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";

        public CPT(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~CPT()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        //Public Property DataBaseConnectionString
        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = "";

        //_CPTiD is set to 0 for new Insertion
        //sCPTCode,sDescription,sSpecialityCode,sCategoryType,sCategoryDesc,sCodeTypeCode,sCodeTypeDesc,sDefaultMoodifier1Code,
        //sDefaultMoodifier1Type,sDefaultMoodifier2Code,sDefaultMoodifier2Type,sDefaultMoodifier3Code,
        //sDefaultMoodifier3Type,sDefaultMoodifier4Code,sDefaultMoodifier4Type,nDefaultUnits,bIsCPTDrug,
        //sNDCCode,bIsTaxable,nRate,nCharges,nAllowed,nClinicFee,nClinicFee,bInactive,nClinicID,bIsBlocked

        private Int64 _CPTiD = 0;
        private string _CPTCode = "";
        private string _Description = "";
        private Int64 _nSpecialtyID;
        private Int64 _nCategoryID = 0;
        private DateTime _CPTActivationDate = DateTime.MinValue;
        private DateTime _CPTInactivationDate = DateTime.MinValue;

        #region Variables

        private string _SpecialityCode = "";
        private string _CategoryType = "";
        private string _Categorydesc = "";
        ////sCodeTypeCodes CodeTypeDesc
        //private string _CodeTypeCode = "";
        //private string _CodeTypeDesc = "";
        private string _Modifier1Code = "";
        private string _Modifier1Desc = "";
        private string _Modifier2Code = "";
        private string _Modifier2Desc = "";
        private string _Modifier3Code = "";
        private string _Modifier3Desc = "";
        private string _Modifier4Code = "";
        private string _Modifier4Desc = "";
        private Decimal _Units = 0;
        private Decimal _AnesthesiaUnits = 0;
        private bool _IsCPTDrug = false;
        private string _NDCCode = "";
        private bool _IsTaxable = false;
        private decimal _Rate = 0;
        private decimal _Charges = 0;
        //    private decimal _Allowed = 0;
        //private bool _IsUseFromFeeSchedule= false;
        private decimal _ClinicFee = 0;
        private bool _Inactive = false;
        private Int64 _ClinicID = 0;
        private string _sStatementDesc = String.Empty;
        private string _sRevenueCode = String.Empty;
        private string _CptCLIANumber = String.Empty;   //  added on 15Apr2014 for CLIANumber on CPT Master - Sameer
        private bool _bDefaultSelf = false; //  added on 03-09-2014  for Default to Self on CPT Master
        private bool _bNonServiceCode = false; //  added on 03-09-2014  for Non-service communication to Insurance on CPT Master
        private bool _bIsMammogram = false;
        private decimal _ProductCost = 0;
        private Int16 _CostPer = 2;
        #endregion

        public DateTime CPTActivationDate
        {
            get { return _CPTActivationDate; }
            set { _CPTActivationDate = value; }
        }

        public DateTime CPTInactivationDate
        {
            get { return _CPTInactivationDate; }
            set { _CPTInactivationDate = value; }
        }

        public Int64 CPTID
        {
            get { return _CPTiD; }
            set { _CPTiD = value; }
        }
        public string CPTCode
        {
            get { return _CPTCode; }
            set { _CPTCode = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public Int64 nSpecialtyID
        {
            get { return _nSpecialtyID; }
            set { _nSpecialtyID = value; }
        }
        public Int64 nCategoryID
        {
            get { return _nCategoryID; }
            set { _nCategoryID = value; }
        }

        #region Properties
        public string SpecialityCode
        {
            get { return _SpecialityCode; }
            set { _SpecialityCode = value; }
        }
        public string CategoryType
        {
            get { return _CategoryType; }
            set { _CategoryType = value; }
        }
        public string Categorydesc
        {
            get { return _Categorydesc; }
            set { _Categorydesc = value; }
        }
        //public string CodeTypeCode
        //{
        //    get { return _CodeTypeCode; }
        //    set { _CodeTypeCode = value; }
        //}
        //public string CodeTypeDesc
        //{
        //    get { return _CodeTypeDesc; }
        //    set { _CodeTypeDesc = value; }
        //}
        public string Modifier1Code
        {
            get { return _Modifier1Code; }
            set { _Modifier1Code = value; }
        }
        public string Modifier1Desc
        {
            get { return _Modifier1Desc; }
            set { _Modifier1Desc = value; }
        }
        public string Modifier2Code
        {
            get { return _Modifier2Code; }
            set { _Modifier2Code = value; }
        }
        public string Modifier2Desc
        {
            get { return _Modifier2Desc; }
            set { _Modifier2Desc = value; }
        }
        public string Modifier3Code
        {
            get { return _Modifier3Code; }
            set { _Modifier3Code = value; }
        }
        public string Modifier3Desc
        {
            get { return _Modifier3Desc; }
            set { _Modifier3Desc = value; }
        }
        public string Modifier4Code
        {
            get { return _Modifier4Code; }
            set { _Modifier4Code = value; }
        }
        public string Modifier4Desc
        {
            get { return _Modifier4Desc; }
            set { _Modifier4Desc = value; }
        }
        public Decimal Units
        {
            get { return _Units; }
            set { _Units = value; }
        }
        public bool IsCPTDrug
        {
            get { return _IsCPTDrug; }
            set { _IsCPTDrug = value; }
        }
        public string NDCCode
        {
            get { return _NDCCode; }
            set { _NDCCode = value; }
        }
        public bool IsTaxable
        {
            get { return _IsTaxable; }
            set { _IsTaxable = value; }
        }
        public decimal Rate
        {
            get { return _Rate; }
            set { _Rate = (decimal)value; }
        }
        public decimal Charges
        {
            get { return _Charges; }
            set { _Charges = value; }
        }
        //public decimal Allowed 
        //     {
        //         get { return _Allowed; }
        //         set { _Allowed = value; }
        //}
        //public bool IsUseFromFeeSchedule 
        //     {
        //         get { return _IsUseFromFeeSchedule; }
        //         set { _IsUseFromFeeSchedule = value; }
        //}
        public decimal ClinicFee
        {
            get { return _ClinicFee; }
            set { _ClinicFee = value; }
        }
        public bool Inactive
        {
            get { return _Inactive; }
            set { _Inactive = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public string StatementDesc
        {
            get { return _sStatementDesc; }
            set { _sStatementDesc = value; }
        }

        public string RevenueCode
        {
            //Added for Bug Id 5386.
            get
            {
                return _sRevenueCode;
            }
            set
            {
                _sRevenueCode = value;
            }
            //End
        }
        public bool CPTTriggrs { get; set; }
        public Int32 PeriodDays { get; set; }
        public string BillingReminder { get; set; }
        public bool bIsPromptforEpsdtFamPlan { get; set; }
        public bool bIsAnesthesia { get; set; }
        public Decimal AnesthesiaUnits
        {
            get { return _AnesthesiaUnits; }
            set { _AnesthesiaUnits = value; }
        }
        // Property added on 15Apr2014 for CLIANumber on CPT Master- Sameer
        public string CptCLIANumber
        {
            get { return _CptCLIANumber; }
            set { _CptCLIANumber = value; }
        }

        public bool bDefaultSelf
        {
            get { return _bDefaultSelf; }
            set { _bDefaultSelf = value; }
        }
        public bool bNonServiceCode
        {
            get { return _bNonServiceCode; }
            set { _bNonServiceCode = value; }
        }

        public bool bIsMammogram
        {
            get { return _bIsMammogram; }
            set { _bIsMammogram = value; }
        }
        public decimal ProductCost
        {
            get { return _ProductCost; }
            set { _ProductCost = value; }
        }
        public Int16 CostPer
        {
            get { return _CostPer; }
            set { _CostPer = value; }
        }
        #endregion

        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        //private Int64 _ClinicID = 0;
        //public Int64 ClinicID
        //{
        //    get { return _ClinicID; }
        //    set { _ClinicID = value; }
        //}
        ////


        #endregion 'Public and Private Properties'

        //<summary>
        // This method Adds new CPT to CPT_MST table
        //returns a true value on insertion
        //</summary>
        public Int64 Add()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            //nCPTID, sCPTCode, sDescription, sSpecialityCode, sCategoryType, sCategoryDesc, sCodeTypeCode, sCodeTypeDesc, sDefaultMoodifier1Code,
            //sDefaultMoodifier1Desc, sDefaultMoodifier2Code, sDefaultMoodifier2Desc, sDefaultMoodifier3Code, sDefaultMoodifier3Desc,
            //sDefaultMoodifier4Code, sDefaultMoodifier4Desc, nDefaultUnits, bIsCPTDrug, sNDCCode, bIsTaxable, nRate, nCharges, nAllowed, 
            //nClinicFee, bInactive, nClinicID

            try
            {
                oDB.Connect(false);
                object _intresult = 0;
                oParameters.Add("@CPTID", CPTID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oParameters.Add("@sCPTCode", CPTCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sDescription", Description, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sSpecialityCode", SpecialityCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sCategoryType", CategoryType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sCategoryDesc", Categorydesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sCodeTypeCode", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sCodeTypeDesc", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier1Code", Modifier1Code, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier1Desc", Modifier1Desc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier2Code", Modifier2Code, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier2Desc", Modifier2Desc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier3Code", Modifier3Code, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier3Desc", Modifier3Desc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier4Code", Modifier4Code, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@sModifier4Desc", Modifier4Desc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@nUnits", Units, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oParameters.Add("@bIsCPTDrug", IsCPTDrug, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@sNDCCode", NDCCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oParameters.Add("@bIsTaxable", IsTaxable, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@nRate", Rate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oParameters.Add("@nCharges", Charges, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oParameters.Add("@nAllowed", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oParameters.Add("@bIsUseFromFeeSchedule", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@nClinicFee", ClinicFee, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oParameters.Add("@bInactive", Inactive, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sStatementDesc", StatementDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sRevenueCode", RevenueCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@bCPTTriggrs", CPTTriggrs, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@bIsPromptforEpsdtFamPlan", bIsPromptforEpsdtFamPlan, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@bIsAnesthesia", bIsAnesthesia, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@nSpecialityID", _nSpecialtyID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCategoryID", nCategoryID, ParameterDirection.Input, SqlDbType.BigInt);
                if (AnesthesiaUnits != 0)
                {
                    oParameters.Add("@nAnesthesiaBaseUnits", AnesthesiaUnits, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                }
                else
                {
                    oParameters.Add("@nAnesthesiaBaseUnits", DBNull.Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                }
                if (PeriodDays == -1)
                {
                    oParameters.Add("@nPeriodDays", null, ParameterDirection.Input, SqlDbType.SmallInt);
                }
                else
                {
                    oParameters.Add("@nPeriodDays", PeriodDays, ParameterDirection.Input, SqlDbType.SmallInt);
                }
                oParameters.Add("@sBillingReminder", BillingReminder, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sCLIANo", CptCLIANumber, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);  //added on 15Apr2014 for CLIANumber on CPT Master- Sameer
                oParameters.Add("@bDefaultSelf", bDefaultSelf, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@bNonServiceCode", bNonServiceCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oParameters.Add("@bIsMammogram", bIsMammogram, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                if (this.CPTActivationDate != DateTime.MinValue)
                { oParameters.Add("@dtActivationDate", this.CPTActivationDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Date); }

                if (this.CPTInactivationDate != DateTime.MinValue)
                { oParameters.Add("@dtInactivationDate", this.CPTInactivationDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Date); }

                oParameters.Add("@dProductCost", ProductCost, ParameterDirection.Input, SqlDbType.Decimal);
                oParameters.Add("@nCostPer", CostPer, ParameterDirection.Input, SqlDbType.SmallInt);

                _result = oDB.Execute("gsp_InUpCPT_MST", oParameters, out _intresult);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

            }
            return _result;
        }

        public bool Delete(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from CPT_MST where nCPTID =" + ID;
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        //<summary>
        //This method is used to retrive the Specialities
        //from the Specialty_MST table
        //</summary>
        internal DataTable GetSpecialitys()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            try
            {
                //String strQuery = "SELECT nSpecialtyID,isnull(sDescription,'') as sDescription FROM Specialty_MST";
                String strQuery = "SELECT nSpecialtyID,isnull(sDescription,'') as sDescription FROM Specialty_MST WITH (NOLOCK) where nClinicID = " + ClinicID + " ";

                //connect to the database
                oDB.Connect(false);
                oDB.Retrive_Query(strQuery, out dt);
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }


        }//end getALLSpecialty

        //<summary>
        //This method is used to retrive the Categories
        //from the Category_MST table
        //</summary>
        internal DataTable GetCategorys()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            try
            {
                // String strQuery = "SELECT nCategoryID,isnull(sCategoryType,'')as sCategoryType FROM Category_MST";

                // String strQuery = "SELECT nCategoryID,isnull(sDescription,'') as sDescription FROM Category_MST where sCategoryType='CPT'and nClinicID = "+ClinicID+" ";

                String strQuery = "SELECT nCategoryID,isnull(sDescription,'') as sDescription FROM Category_MST WITH (NOLOCK) where ( sCategoryType='CPT' AND bIsBlocked = '" + false + "') AND nClinicID = " + ClinicID + " ";

                // Connecting to the database
                oDB.Connect(false);

                oDB.Retrive_Query(strQuery, out dt);

                return dt;
                //Disconnect the Database Connection
                // oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

            }

        }//end getALLCategory





        //<summary>
        //Special function to fill the CPT form for modify
        //This function used to retrive data from CPT_MST table
        //</summary>

        public DataTable getCPT()
        {

            //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //Connect to database
                oDB.Connect(false);

                string strQuery = "Select nCPTID, ISNULL(sCPTCode,'') AS sCPTCode,ISNULL(CPT_MST.sDescription,'') AS sDescription,ISNULL(sSpecialityCode,'')AS sSpecialityCode," +
                "ISNULL(sCategoryType,'')   AS sCategory, ISNULL(sCategoryDesc,'' ) AS sCategory," +
                "ISNULL(sModifier1Code,'') AS Modifier1Code,ISNULL(sModifier1Desc,'') AS Modifier1Desc," +
                "ISNULL(sModifier2Code,'') AS Modifier2Code, ISNULL(sModifier2Desc,'') AS Modifier2Desc,ISNULL(sModifier3Code,'')  AS Modifier3Code," +
                "ISNULL(sModifier3Desc,'') AS Modifier3Desc,ISNULL(sModifier4Code,'')  AS Modifier4Code, ISNULL(sModifier4Desc,'') AS Modifier4Desc," +
                "ISNULL(nUnits,0) AS nUnits ,ISNULL(bIsCPTDrug,'false') AS bIsCPTDrug, ISNULL(sNDCCode,'') AS sNDCCode, " +
                "ISNULL(bIsTaxable,'false') AS bIsTaxable, ISNULL(nRate,0) AS nRate,ISNULL(nCharges,0) AS nCharges," +
                "ISNULL(nClinicFee,0) AS nClinicFee,ISNULL(bInactive, 'false') AS bInactive," +
                "ISNULL(sStatementDesc,'')as sStatementDesc," +
                "ISNULL(BL_UB_RevenueCode_MST.sRevenueCode,'')as sRevenueCode,ISNULL(BL_UB_RevenueCode_MST.nRevenueID,0) as RevenueID,ISNULL(bCPTTriggrs,'False') AS bCPTTriggrs,nPeriodDays,ISNULL(sBillingReminder,'') AS sBillingReminder,ISNULL(bIsPromptforEpsdtFamPlan,0) AS bIsPromptforEpsdtFamPlan,ISNULL(bIsAnesthesia,0) AS bIsAnesthesia, nAnesthesiaBaseUnits, ISNULL(sCLIANo,'') AS sCLIANo, " +
                "ISNULL(bDefaultSelf,0) AS bDefaultSelf,  ISNULL(bNonServiceCode,0) AS bNonServiceCode,  CONVERT(DATE, dtCPTActivateDate, 101) AS dtCPTActivateDate, dtCPTInActivateDate,ISNULL(dProductCost,0) AS dProductCost , ISNULL(nCostPer,2) AS nCostPer,ISNULL(bIsMammogram,0) AS bIsMammogram FROM CPT_MST WITH (NOLOCK) left outer join BL_UB_RevenueCode_MST WITH (NOLOCK) on CPT_MST.sRevenueCode=BL_UB_RevenueCode_MST.sRevenueCode " +
                "WHERE  nCPTID = " + this.CPTID + " AND  nClinicID = " + _ClinicID + "";

                //Get data into datatable
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }//end getCPT

        public DataTable getCPT(string CPTCode)
        {

            //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //Connect to database
                oDB.Connect(false);

                string strQuery = "Select nCPTID, ISNULL(sCPTCode,'') AS sCPTCode,ISNULL(CPT_MST.sDescription,'') AS sDescription,ISNULL(sSpecialityCode,'')AS sSpecialityCode," +
                "ISNULL(sCategoryType,'')   AS sCategory, ISNULL(sCategoryDesc,'' ) AS sCategory,ISNULL(sCodeTypeCode,'')AS sCodeTypeCode," +
                "ISNULL(sCodeTypeDesc,'') AS sCodeTypeDesc,ISNULL(sModifier1Code,'') AS Modifier1Code,ISNULL(sModifier1Desc,'') AS Modifier1Desc," +
                "ISNULL(sModifier2Code,'') AS Modifier2Code, ISNULL(sModifier2Desc,'') AS Modifier2Desc,ISNULL(sModifier3Code,'')  AS Modifier3Code," +
                "ISNULL(sModifier3Desc,'') AS Modifier3Desc,ISNULL(sModifier4Code,'')  AS Modifier4Code, ISNULL(sModifier4Desc,'') AS Modifier4Desc," +
                "ISNULL(nUnits,0) AS nUnits ,ISNULL(bIsCPTDrug,'false') AS bIsCPTDrug, ISNULL(sNDCCode,'') AS sNDCCode, " +
                "ISNULL(bIsTaxable,'false') AS bIsTaxable, ISNULL(nRate,0) AS nRate,ISNULL(nCharges,0) AS nCharges," +
                "ISNULL(nAllowed,0) AS nAllowed, ISNULL(nClinicFee,0) AS nClinicFee,ISNULL(bInactive, 'false') AS bInactive, " +
                " ISNULL(bIsUseFromFeeSchedule, 'false') AS bIsUseFromFeeSchedule,ISNULL(sStatementDesc,'')as sStatementDesc," +
                " ISNULL(BL_UB_RevenueCode_MST.sRevenueCode,'')as sRevenueCode,ISNULL(BL_UB_RevenueCode_MST.nRevenueID,0) as RevenueID FROM CPT_MST WITH (NOLOCK) " +
                " left outer join BL_UB_RevenueCode_MST WITH (NOLOCK) on CPT_MST.sRevenueCode=BL_UB_RevenueCode_MST.sRevenueCode WHERE sCPTCode= '" + CPTCode.Replace("'", "''") + "' AND  nClinicID = " + _ClinicID + "";

                //Get data into datatable
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }//end getCPT

        public DataTable GetCPTs()
        {
            //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //Connect to database
                oDB.Connect(false);
                string strQuery = "Select nCPTID, ISNULL(sCPTCode,'') AS sCPTCode,ISNULL(sDescription,'') AS sDescription,ISNULL(sStatementDesc,'') AS sStatementDesc, ISNULL(sSpecialityCode,'')AS sSpecialityCode," +
                    //"ISNULL(sCategoryType,'')  + case when ISNULL(sCategoryDesc,'') <>'' then '-' + ISNULL(sCategoryDesc,'')  else '' end AS sCategory," +
                "CASE WHEN ISNULL(sCategoryType,'') <>'' THEN " +
                            "CASE WHEN ISNULL(sCategoryDesc,'') <>'' THEN sCategoryType + '-' + sCategoryDesc " +
                                                                    " ELSE sCategoryType END " +
                " ELSE " +
                            " CASE WHEN ISNULL(sCategoryDesc,'') <>'' " +
                " THEN CASE WHEN ISNULL(sCategoryDesc, '') = 'All' " +
                " THEN '' " +
                " ELSE sCategoryDesc END " +
                " ELSE '' END " +
                " END AS sCategory, " +
                "ISNULL(sModifier1Code,'') + case when ISNULL(sModifier1Desc,'')<>'' then '-' + ISNULL(sModifier1Desc,'') else '' end AS Modifier1," +
                "ISNULL(sModifier2Code,'') + case when ISNULL(sModifier2Desc,'')<>'' then '-' + ISNULL(sModifier2Desc,'') else '' end AS Modifier2," +
                "ISNULL(sModifier3Code,'') + case when ISNULL(sModifier3Desc,'')<>'' then '-' + ISNULL(sModifier3Desc,'') else '' end AS Modifier3," +
                "ISNULL(sModifier4Code,'') + case when ISNULL(sModifier4Desc,'')<>'' then '-' + ISNULL(sModifier4Desc,'') else '' end AS Modifier4," +
                "ISNULL(nUnits,0) AS nUnits , CASE  ISNULL(bIsCPTDrug,'false') WHEN 'FALSE' THEN 'NO' WHEN 'TRUE' THEN 'YES' END  AS bIsCPTDrug, ISNULL(sNDCCode,'') AS sNDCCode, " +
                "ISNULL(bIsTaxable,'false') AS bIsTaxable, ISNULL(nRate,0) AS nRate, ISNULL(nCharges,0) AS nCharges," +
                "ISNULL(nClinicFee,0) AS nClinicFee, ISNULL(bInactive, 'false') AS bInactive,ISNULL(sRevenueCode,'')as sRevenueCode, ISNULL(sCLIANo,'') AS sCLIANo, dtCPTActivateDate, dtCPTInActivateDate FROM CPT_MST WITH (NOLOCK) WHERE nClinicID = " + _ClinicID + "";

                //Get data into datatable
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }


        }

        // 20100107 Mahesh Nawal  Code for geting CPT code In range
        public DataTable GetCPTsInRange(string startvalue, string endvalue)
        {

            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable dt = null;
            try
            {
                //Connect to database
                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);

                dt = null;
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@FromCpt", startvalue, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ToCpt", endvalue, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetCPTsInRange", oParameters, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {

                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }
        //

        public DataTable GetCodeTypes()
        {
            //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //Connect to database
                oDB.Connect(false);

                //String strQuery = "SELECT nCPTID, sCPTCode,sDescription,nSpecialtyID,nCategoryID FROM CPT_MST";
                String strQuery = "SELECT  sCodeTypeCode,sCodeTypeDesc FROM  CPT_MST WITH (NOLOCK) WHERE  nClinicID = " + ClinicID + "";

                //Get data into datatable
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }

        }

        public bool IsExistsCPT(Int64 CPTId, string CPTCode, string CPTName)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                //strQuery = "select count(nCPTID) from CPT_MST where sCPTCode = '" + CPTCode.Replace("'", "''") + "' AND sDescription = '" + CPTName.Replace("'", "''") + "' ";
                if (CPTId == 0)
                {

                    strQuery = "select count(nCPTID) from CPT_MST WITH (NOLOCK) where (sCPTCode = '" + CPTCode.Replace("'", "''") + "' OR sDescription = '" + CPTName.Replace("'", "''") + "') AND nClinicID = " + this.ClinicID + " ";
                }
                else
                {
                    //strQuery = "select count(nCPTID) from CPT_MST where (sCPTCode = '" + CPTCode + "' OR sDescription = '" + CPTName + "') AND nCPTID <> "+CPTId+" ";
                    //
                    strQuery = "select count(nCPTID) from CPT_MST WITH (NOLOCK) where ((sCPTCode = '" + CPTCode.Replace("'", "''") + "' OR sDescription = '" + CPTName.Replace("'", "''") + "') AND nCPTID <> " + CPTId + ") AND nClinicID = " + this.ClinicID + " ";

                }

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {

                if (oDB != null) { oDB.Dispose(); }
            }

            return _result;
        }
        public bool IsDuplicateCPT(Int64 CPTId, string CPTCode, string CPTName)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                strQuery = "select count(nCPTID) from CPT_MST WITH (NOLOCK) where sCPTCode = '" + CPTCode.Replace("'", "''") + "' AND sDescription = '" + CPTName.Replace("'", "''") + "' ";
                //if (CPTId == 0)
                //{

                //    strQuery = "select count(nCPTID) from CPT_MST where (sCPTCode = '" + CPTCode.Replace("'", "''") + "' OR sDescription = '" + CPTName.Replace("'", "''") + "') AND nClinicID = " + this.ClinicID + " ";
                //}
                //else
                //{
                //    //strQuery = "select count(nCPTID) from CPT_MST where (sCPTCode = '" + CPTCode + "' OR sDescription = '" + CPTName + "') AND nCPTID <> "+CPTId+" ";
                //    //
                //    strQuery = "select count(nCPTID) from CPT_MST where ((sCPTCode = '" + CPTCode.Replace("'", "''") + "' OR sDescription = '" + CPTName.Replace("'", "''") + "') AND nCPTID <> " + CPTId + ") AND nClinicID = " + this.ClinicID + " ";

                //}

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {

                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }
        public DataTable GetCPT(string CPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtCPT = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT nCPTID,ISNULL(sDescription,'') AS sDescription " +
                " FROM CPT_MST WITH (NOLOCK) WHERE UPPER(sCPTCode) = '" + CPTCode.Trim().ToUpper() + "' AND nClinicID = " + this.ClinicID + "";
                oDB.Retrive_Query(_sqlQuery, out _dtCPT);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true); }
            finally
            { if (oDB != null) { oDB.Dispose(); oDB = null; } }

            return _dtCPT;
        }

        internal DataTable GetReferralCPTs()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            try
            {
                //Connect to database
                oDB.Connect(false);

                //Query to retrieve data from ICD9 master table
                //String strQuery = "SELECT nICD9ID, sICD9Code, sDescription,nSpecialtyID FROM ICD9";
                String strQuery = " SELECT ISNULL(nCPTID,0) AS nCPTID, ISNULL(sCPTCode,'') AS sCPTCode, ISNULL(sDescription,'') AS sDescription, ISNULL(bIsReferral,0) AS bIsReferral " +
                                  " FROM   BL_ReferralCPT_MST WITH (NOLOCK) " +
                                  " WHERE ISNULL(BL_ReferralCPT_MST.nClinicID,1) = " + _ClinicID;

                //Get data into datatable

                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return dt;
        }

        public Boolean IsCPTCodeInUse(string sCPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dt = null;
            Boolean _isCPTInUse = false;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@sCPTCode", sCPTCode, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("Check_For_IsCPTCodeInUse", oParameters, out _dt);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    if (Convert.ToInt64(_dt.Rows[0]["CPTCount"]) > 0)
                    {
                        _isCPTInUse = true;
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
            }

            return _isCPTInUse;
        }



        //code added for to check facility whether it is used or not.If used then dont delete that faciltiy.
        public bool IsFacilityInUse(string sFacilitycode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _strQuery = "";
            bool _isFacilityInUse = false;

            try
            {
                oDB.Connect(false);

                _strQuery = "SELECT COUNT(sFacilitycode) FROM BL_Transaction_MST WHERE sFacilityCode = '" + sFacilitycode + "' ";

                object _intResult = null;

                _intResult = oDB.ExecuteScalar_Query(_strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _isFacilityInUse = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {

                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _isFacilityInUse;

        }
        // end code

        public Decimal getCPTDefaultBaseUnit(string sCPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            Decimal _cptDefaultBaseUnit = 0;
            try
            {
                oDB.Connect(false);
                _cptDefaultBaseUnit = Convert.ToDecimal(oDB.ExecuteScalar_Query("Select ISNULL(nAnesthesiaBaseUnits,0) from Cpt_mst where sCPTCode='" + sCPTCode.Trim().Replace("'", "''") + "'"));
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }

            return _cptDefaultBaseUnit;
        }

        public Decimal getCPTDefaultMinPerUnit(Int64 nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            Decimal _planDefaultMinPerUnit = 0;
            try
            {
                oDB.Connect(false);
                _planDefaultMinPerUnit = Convert.ToDecimal(oDB.ExecuteScalar_Query("SELECT ISNULL(nMinutesPerUnits,0)  FROM dbo.Contacts_Insurance_DTL WHERE nContactID=" + nContactID));
                oDB.Disconnect();
            }

            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            catch
            {
                // Blank catch for collection agency as anesthesia information not present in contacts_Insurance_DTL 
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _planDefaultMinPerUnit;
        }

    } // end class CPT
    #endregion

    #region "Drugs"
    public class Drugs
    {
        #region "Global Variables"
        Object drugID;
        public Int64 Id = 0;
        private string _messageBoxCaption = "";

        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //

        #endregion "Global Variables"

        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";

        public Drugs(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        public Drugs(Int64 DrugID, string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            _DrugsID = DrugID;

            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~Drugs()
        {
            Dispose(false);
        }

        #endregion

        #region  "Private Property Variable "
        //nDrugsID, sDrugName, sGenericName, sDosage, sRoute,
        private Int64 _DrugsID = 0;
        private string _DrugsName = "";
        private string _DrugsGenericName = "";
        private string _Dosage = "";
        private string _Route = "";

        //sFrequency, sDuration, bIsClinicalDrug, sAmount, 
        private string _Frequency = "";
        private string _Duration = "";
        private bool _IsClinicalDrug = false;
        private string _Amount = "";

        //nIsNarcotics, nddid, bIsAllergicDrug
        private Int64 _IsNarcotics = 0;
        private Int64 _ddid = 0;
        private bool _IsAllergicDrug = false;


        #endregion  "Private Property Variable "

        #region "Property Procedures"
        //nDrugsID, sDrugName, sGenericName, sDosage, sRoute,
        public Int64 DrugsID
        {
            get { return _DrugsID; }
            set { _DrugsID = value; }
        }
        public string DrugsName
        {
            get { return _DrugsName; }
            set { _DrugsName = value; }
        }
        public string DrugsGenericName
        {
            get { return _DrugsGenericName; }
            set { _DrugsGenericName = value; }
        }
        public string Dosage
        {
            get { return _Dosage; }
            set { _Dosage = value; }
        }
        public string Route
        {
            get { return _Route; }
            set { _Route = value; }
        }

        //sFrequency, sDuration, bIsClinicalDrug, sAmount, 
        public string Frequency
        {
            get { return _Frequency; }
            set { _Frequency = value; }
        }
        public string Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }

        public bool IsClinicalDrug
        {
            get { return _IsClinicalDrug; }
            set { _IsClinicalDrug = value; }
        }
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //nIsNarcotics, nddid, bIsAllergicDrug
        public Int64 IsNarcotics
        {
            get { return _IsNarcotics; }
            set { _IsNarcotics = value; }
        }
        public Int64 ddid
        {
            get { return _ddid; }
            set { _ddid = value; }
        }
        public bool IsAllergicDrug
        {
            get { return _IsAllergicDrug; }
            set { _IsAllergicDrug = value; }
        }


        #endregion "Property Procedures"

        #region "Functions"

        public Int64 Add(Int64 drugid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            // gloDatabaseLayer.DBParameter oDBParameter;

            try
            {
                oDB.Connect(false);

                // @nDrugsID, @sDrugName, @sGenericName, @sDosage, @sRoute, @sFrequency, @sDuration, @bIsClinicalDrug,

                oDBParameters.Add("@DrugsID", drugid, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@DrugName", DrugsName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@GenericName", DrugsGenericName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Dosage", Dosage, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Route", Route, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Frequency", Frequency, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Duration", Duration, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@IsClinicalDrug", IsClinicalDrug, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);

                //@sAmount, @nIsNarcotics,	@nddid, @bIsAllergicDrug                  
                oDBParameters.Add("@IsAllergicDrug", IsAllergicDrug, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@Amount", Amount, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nNarcotics", IsNarcotics, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                //
                oDBParameters.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                //

                oDB.Execute("gsp_InUpDrugs_MST", oDBParameters, out drugID);

                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }

            return Convert.ToInt64(drugID);
        }

        public bool Block(Int64 drugid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "UPDATE Drugs_MST WITH (READPAST) SET bIsBlocked = '" + true + "' WHERE nDrugsID = " + drugid;
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public bool Unblock(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE Drugs_MST WITH (READPAST) SET bIsBlocked = '" + false + "' WHERE nDrugsID = " + ID + " ";
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public bool DeleteAll()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            DataTable dt = null;
            int _result = 0;
            Int64 _drugId = 0;

            try
            {
                oDB.Connect(false);

                //Get list of Blocked Drugs
                strQuery = " SELECT nDrugsID FROM Drugs_MST WITH (NOLOCK) WHERE bIsBlocked = 'true' AND nClinicID = " + this.ClinicID + " ";
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _drugId = 0;
                        _drugId = Convert.ToInt64(dt.Rows[i][0]);
                        if (CanDelete(_drugId))
                        {
                            strQuery = " DELETE FROM Drugs_MST WHERE nDrugsID = " + _drugId;
                            _result = oDB.Execute_Query(strQuery);
                            strQuery = "";
                        }
                        else
                        {
                            MessageBox.Show("Cannot Delete :Item in use or System defined", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cannot Delete :Item in use or System defined", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
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
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }

        public bool CanDelete(Int64 ID)
        {
            //Logic need to be implemented
            return false;
        }

        public bool Delete(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from Drugs_MST where nDrugsID =" + ID;
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
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public System.Data.DataTable GetList()
        {
            //Function to Get the Drug information from Database

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            //Drugs oDrugs = new Drugs(_databaseconnectionstring);
            DataTable dt = null;
            string strquery = "";

            try
            {
                oDB.Connect(false);


                {
                    //oDB.Retrive_Query("Select * from  drugs_MST ", out  dt);
                    //
                    //oDB.Retrive_Query("Select * from  drugs_MST where nClinicID ="+this.ClinicID+" ", out  dt);
                    strquery = "Select nDrugsID, sDrugName, sGenericName, sDosage, sRoute, sFrequency, sDuration, sAmount, " +
                         " CASE ISNULL(nIsNarcotics,0) WHEN 0 THEN 'C1'WHEN 1 THEN 'C2' END AS bISNarcotics ," +
                    " bIsClinicalDrug, nddid, bIsAllergicDrug from  Drugs_MST WITH (NOLOCK) where (bIsBlocked='" + false + "' OR bIsBlocked IS NULL) AND (nClinicID =" + this.ClinicID + " OR nClinicID IS NULL)";
                    oDB.Retrive_Query(strquery, out  dt);
                    //
                }
                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {

                //        //nDrugsID, sDrugName, sGenericName, sDosage, sRoute, sFrequency, 
                //        oDrugs.DrugsID = (Int64)(dt.Rows[0]["nDrugsID"]); // Resourse Description
                //        oDrugs.DrugsName = dt.Rows[0]["sDrugName"].ToString();
                //        oDrugs.DrugsGenericName = dt.Rows[0]["sGenericName"].ToString();
                //        oDrugs.Dosage = dt.Rows[0]["sDosage"].ToString();
                //        oDrugs.Route = dt.Rows[0]["sRoute"].ToString();
                //        oDrugs.Frequency = dt.Rows[0]["sFrequency"].ToString();

                //        //sDuration, bIsClinicalDrug, sAmount, nIsNarcotics, nddid, bIsAllergicDrug                        
                //        oDrugs.Duration = dt.Rows[0]["sDuration"].ToString();
                //        oDrugs.IsClinicalDrug = (Boolean)dt.Rows[0]["bIsClinicalDrug"];
                //        oDrugs.Amount = dt.Rows[0]["sAmount"].ToString();
                //        oDrugs.IsNarcotics = (Int64)dt.Rows[0]["nIsNarcotics"];
                //        oDrugs.ddid = (Int64)dt.Rows[0]["nddid"];
                //        oDrugs.IsAllergicDrug = (Boolean)dt.Rows[0]["bIsAllergicDrug"];
                //    }
                //}
                //dt.Dispose();

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return dt;
        }

        public System.Data.DataTable GetBlockedList()
        {
            //Function to Get the Drug information from Database

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            //Drugs oDrugs = new Drugs(_databaseconnectionstring);
            DataTable dt = null;

            try
            {
                oDB.Connect(false);
                oDB.Retrive_Query("Select nDrugsID, sDrugName, sGenericName, sDosage, sRoute, sFrequency, sDuration, sAmount, nIsNarcotics, bIsClinicalDrug, nddid, bIsAllergicDrug from  Drugs_MST WITH (NOLOCK) where bIsBlocked='" + true + "' AND nClinicID =" + this.ClinicID + " ", out  dt);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return dt;
        }

        public System.Data.DataTable GetDrug(Int64 drugId)
        {
            System.Data.DataTable _result = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "SELECT nDrugsID, sDrugName, sGenericName, sDosage, sRoute, sFrequency, sDuration, bIsClinicalDrug, sAmount,CASE ISNULL(nIsNarcotics,0) WHEN 0 THEN 'C1'WHEN 1 THEN 'C2' END AS nIsNarcotics , nddid, bIsAllergicDrug FROM Drugs_MST WITH (NOLOCK) where nDrugsID='" + drugId + "'";
                oDB.Retrive_Query(_sqlQuery, out _result);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }

        public bool IsExists(Int64 drugId, string drugName)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                //nPatientRelID, sRelationship, bIsBlock, nClinicID
                //PatientRelationship
                //nAppointmentTypeID, sAppointmentType, nDuration, sColorCode, bIsBlocked, nClinicID
                string _sqlQuery = "";
                if (drugId == 0)
                {
                    // _sqlQuery = "SELECT Count(nDrugsId) FROM Drugs_MST WHERE sDrugName ='" + drugName + "'";
                    //
                    _sqlQuery = "SELECT Count(nDrugsId) FROM Drugs_MST WITH (NOLOCK) WHERE sDrugName ='" + drugName + "' AND nClinicID = " + this.ClinicID + "";
                    //
                }
                else
                {
                    //_sqlQuery = "SELECT Count(nDrugsId) FROM Drugs_MST WHERE sDrugName='" + drugName + "' AND nDrugsID <> " + drugId;
                    //
                    _sqlQuery = "SELECT Count(nDrugsId) FROM Drugs_MST WITH (NOLOCK) WHERE (sDrugName='" + drugName + "' AND nDrugsID <> " + drugId + ")AND nClinicID=" + this.ClinicID + "";
                    //
                }

                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }


        #endregion "Functions"
    }
    #endregion

    #region "Facility"
    public class Facility : IDisposable
    {

        #region "Constructor & Destructor"

        public Facility()
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

        ~Facility()
        {
            Dispose(false);
        }

        #endregion

        #region " Declarations "

        private Int64 _FacilityID = 0;
        private string _FacilityCode = "";
        private string _FacilityName = "";
        private string _NPI = "";
        private string _MedicadID = "";
        private string _BlueShieldID = "";
        private string _MedicareID = "";
        private string _TaxID = "";//Added 20090123
        private Int64 _POSID = 0;
        private string _POS_CODE = "";
        private string _POS = "";
        private string _Address1 = "";
        private string _Address2 = "";
        private string _Zip = "";
        private string _City = "";
        private string _State = "";
        private string _AreaCode = "";
        private string _Country = "";
        private string _County = "";
        private string _Phone = "";
        private string _Fax = "";
        private FacilityType _FacilityType = FacilityType.None;
        private Int64 _ClinicID = 0;
        private string _FaclityCLIANumber = "";
        private string _CarrierNumber = "";
        private string _Locality = "";

        //Facility Physical Address

        private string _sPhysicalAddContactName = "";
        private string _sPhysicalAddressline1 = "";
        private string _sPhysicalAddressline2 = "";
        private string _sPhysicalCity = "";
        private string _sPhysicalState = "";
        private string _sPhysicalZIP = "";
        private string _sPhysicalAreaCode = "";
        private string _sPhysicalCountry = "";
        private string _sPhysicalCounty = "";
        private string _sPhysicalPhoneNo = "";
        private string _sPhysicalFAX = "";
        private string _sPhysicalPagerNo = "";
        private string _sPhysicalEmail = "";
        private string _sPhysicalURL = "";

        private string _sMammogramCertNumber = "";
        #endregion " Declarations "

        #region " Property Procedures "

        public Int64 FacilityID
        {
            get { return _FacilityID; }
            set { _FacilityID = value; }
        }
        public string FacilityCode
        {
            get { return _FacilityCode; }
            set { _FacilityCode = value; }
        }
        public string FacilityName
        {
            get { return _FacilityName; }
            set { _FacilityName = value; }
        }
        public string NPI
        {
            get { return _NPI; }
            set { _NPI = value; }
        }
        public string MedicadID
        {
            get { return _MedicadID; }
            set { _MedicadID = value; }
        }
        public string BlueShieldID
        {
            get { return _BlueShieldID; }
            set { _BlueShieldID = value; }
        }
        public string MedicareID
        {
            get { return _MedicareID; }
            set { _MedicareID = value; }
        }
        public string TaxID
        {
            get { return _TaxID; }
            set { _TaxID = value; }
        }
        public Int64 POSID
        {
            get { return _POSID; }
            set { _POSID = value; }
        }
        public string POS_CODE
        {
            get { return _POS_CODE; }
            set { _POS_CODE = value; }
        }
        public string POS
        {
            get { return _POS; }
            set { _POS = value; }
        }
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public string County
        {
            get { return _County; }
            set { _County = value; }
        }
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }


        public FacilityType FacilityType
        {
            get { return _FacilityType; }
            set { _FacilityType = value; }
        }


        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public string FaclityCLIANumber
        {
            get { return _FaclityCLIANumber; }
            set { _FaclityCLIANumber = value; }
        }

        public string CarrierNumber
        {
            get { return _CarrierNumber; }
            set { _CarrierNumber = value; }
        }

        public string Locality
        {
            get { return _Locality; }
            set { _Locality = value; }
        }

        public string TaxonomyCode
        { get; set; }

        //7022Items: Home Billing
        public bool ReportPatientAddress
        { get; set; }
        #endregion

        #region "Facility Physical Address "

        public string PhysicalAddContactName
        {
            get { return _sPhysicalAddContactName; }
            set { _sPhysicalAddContactName = value; }
        }

        public string PhysicalAddressline1
        {
            get { return _sPhysicalAddressline1; }
            set { _sPhysicalAddressline1 = value; }
        }
        public string PhysicalAddressline2
        {
            get { return _sPhysicalAddressline2; }
            set { _sPhysicalAddressline2 = value; }
        }
        public string PhysicalCity
        {
            get { return _sPhysicalCity; }
            set { _sPhysicalCity = value; }
        }
        public string PhysicalState
        {
            get { return _sPhysicalState; }
            set { _sPhysicalState = value; }
        }
        public string PhysicalZIP
        {
            get { return _sPhysicalZIP; }
            set { _sPhysicalZIP = value; }
        }
        public string PhysicalAreaCode
        {
            get { return _sPhysicalAreaCode; }
            set { _sPhysicalAreaCode = value; }
        }
        public string PhysicalCountry
        {
            get { return _sPhysicalCountry; }
            set { _sPhysicalCountry = value; }
        }
        public string PhysicalCounty
        {
            get { return _sPhysicalCounty; }
            set { _sPhysicalCounty = value; }
        }
        public string PhysicalPhoneNo
        {
            get { return _sPhysicalPhoneNo; }
            set { _sPhysicalPhoneNo = value; }
        }
        public string PhysicalFAX
        {
            get { return _sPhysicalFAX; }
            set { _sPhysicalFAX = value; }
        }
        public string PhysicalPagerNo
        {
            get { return _sPhysicalPagerNo; }
            set { _sPhysicalPagerNo = value; }
        }
        public string PhysicalEmail
        {
            get { return _sPhysicalEmail; }
            set { _sPhysicalEmail = value; }
        }
        public string PhysicalURL
        {
            get { return _sPhysicalURL; }
            set { _sPhysicalURL = value; }
        }

        #endregion





        public string sMammogramCertNumber
        {
            get { return _sMammogramCertNumber; }
            set { _sMammogramCertNumber = value; }
        }
    }

    public class Facilities : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"

        public Facilities()
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
                    if (_innerlist != null)
                    {
                        _innerlist.Clear();
                    }
                }
            }
            disposed = true;
        }
        ~Facilities()
        {
            Dispose(false);
        }

        #endregion


        // Methods Add, Remove, Count , Item of TransactionLineModifier
        public int Count
        {
            get { return _innerlist.Count; }
        }
        public void Add(Facility item)
        {
            _innerlist.Add(item);
        }
        public bool Remove(Facility item)
        {
            bool result = false;


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
        public Facility this[int index]
        {
            get
            { return (Facility)_innerlist[index]; }
        }
        public bool Contains(Facility item)
        {
            return _innerlist.Contains(item);
        }
        public int IndexOf(Facility item)
        {
            return _innerlist.IndexOf(item);
        }
        public void CopyTo(Facility[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    public class gloFacility : IDisposable
    {
        #region "Constructor & Destructor"

        public gloFacility(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~gloFacility()
        {
            Dispose(false);
        }

        #endregion

        #region " Declarations "

        private string _messageBoxCaption = "";
        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;


        #endregion " Declarations "

        #region " Property Procedures "

        public string DatabaseConnectionstring
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }


        #endregion

        #region " Private & Public Methods "

        public Facility GetFacility(Int64 FacilityId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Facility oFacility = null;
            DataTable dtFacility = null;
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                //Select the fields for given FaciltyID
                strQuery = " SELECT FMST.nFacilityID,ISNULL(FMST.sFacilityCode,'') AS sFacilityCode, "
                          + " ISNULL(FMST.sFacilityName,'') AS sFacilityName,ISNULL(FMST.sNPI,'') AS sNPI, "
                          + " ISNULL(FMST.sMedicadID,'') AS sMedicadID,ISNULL(FMST.sBlueShieldID,'') AS sBlueShieldID, "
                          + " ISNULL(FMST.sMedicareID,0) AS sMedicareID,ISNULL(FMST.sTaxID,'') AS sTaxID,ISNULL(FMST.nPOSID,0) AS nPOSID, "
                          + " ISNULL(FMST.sAddress1,'') AS sAddress1,ISNULL(FMST.sAddress2,'') AS sAddress2, "
                          + " ISNULL(FMST.sZip,'') AS sZip,ISNULL(FMST.sCity,'') AS sCity,ISNULL(FMST.sState,'') AS sState, "
                          + " ISNULL(FMST.sPhone,'') AS sPhone,ISNULL(FMST.sFax,'') AS sFax,ISNULL(FMST.nFacilityType,0) AS nFacilityType,FMST.nClinicID, "
                          + " ISNULL(FMST.sCLIANo,'') AS sCLIANo, "
                          + " ISNULL(FMST.sCarrierNumber,'') AS sCarrierNumber, "
                          + " ISNULL(FMST.sLocality,'') AS sLocality, ISNULL(FMST.sAreaCode,'') AS sAreaCode, ISNULL(FMST.sCounty,'') AS sCounty,ISNULL(FMST.sCountry,'') AS sCountry, "

                          + " ISNULL(FPA.sContactName,'') as sPhyContactName,ISNULL(FPA.sAddressline1,'') as sPhyAddressline1, "
                          + " ISNULL(FPA.sAddressline2,'') as sPhyAddressline2,ISNULL(FPA.sCity,'') as sPhyCity, "
                          + " ISNULL(FPA.sState,'') as sPhyState,ISNULL(FPA.sZIP,'') as sPhyZIP,ISNULL(FPA.sAreaCode,'') as sPhyAreaCode, "
                          + " ISNULL(FPA.sCountry,'') as sPhyCountry,ISNULL(FPA.sCounty,'') as sPhyCounty,ISNULL(FPA.sPhoneNo,'') as sPhyPhoneNo, "
                          + " ISNULL(FPA.sFAX,'') as sPhyFAX,ISNULL(FPA.sPagerNo,'') as sPhyPagerNo,ISNULL(FPA.sEmail,'') as sPhyEmail, "
                          + " ISNULL(FPA.sURL,'') as sPhyURL,  Case isnull(FMST.sTaxonomyCode,'')  WHEN '' THEN '' ELSE (Ltrim(RTrim(Specialty_MST.sTaxonomyCode))  +'-'+  Specialty_MST.sTaxonomyDesc) END as sTaxonomyCode, "
                          + " ISNULL(FMST.bReportPatientAddress, 0) AS bReportPatientAddress,ISNULL(FMST.sMammogramCertNumber ,'') as sMammogramCertNumber "//7022Items: Home Billing
                          + " FROM   BL_Facility_MST FMST WITH (NOLOCK) LEFT OUTER JOIN BL_Facility_PhysicalAddress FPA WITH (NOLOCK) ON "
                          + " FMST.nFacilityID = FPA.nFacilityID LEFT OUTER JOIN Specialty_MST   WITH (NOLOCK) ON  FMST.sTaxonomyCode = Specialty_MST.sTaxonomyCode "
                          + " WHERE FMST.nFacilityID = " + FacilityId + " ";

                oDB.Retrive_Query(strQuery, out dtFacility);

                if (dtFacility != null && dtFacility.Rows.Count > 0)
                {
                    oFacility = new Facility();
                    oFacility.FacilityID = Convert.ToInt64(dtFacility.Rows[0]["nFacilityID"]);
                    oFacility.FacilityCode = dtFacility.Rows[0]["sFacilityCode"].ToString();
                    oFacility.FacilityName = dtFacility.Rows[0]["sFacilityName"].ToString();
                    oFacility.NPI = dtFacility.Rows[0]["sNPI"].ToString();
                    oFacility.MedicadID = dtFacility.Rows[0]["sMedicadID"].ToString();
                    oFacility.BlueShieldID = dtFacility.Rows[0]["sBlueShieldID"].ToString();
                    oFacility.MedicareID = dtFacility.Rows[0]["sMedicareID"].ToString();
                    oFacility.TaxID = dtFacility.Rows[0]["sTaxID"].ToString();
                    oFacility.POSID = Convert.ToInt64(dtFacility.Rows[0]["nPOSID"]);
                    oFacility.TaxonomyCode = Convert.ToString(dtFacility.Rows[0]["sTaxonomyCode"]);
                    //7022Items: Home Billing
                    oFacility.ReportPatientAddress = Convert.ToBoolean(dtFacility.Rows[0]["bReportPatientAddress"]);

                    CLsBL_TOSPOS oPOS = new CLsBL_TOSPOS(_databaseconnectionstring);
                    DataTable dtPOS = oPOS.GetPOS(oFacility.POSID);
                    if (dtPOS != null && dtPOS.Rows.Count > 0)
                    {
                        oFacility.POS_CODE = dtPOS.Rows[0]["sPOSCode"].ToString();
                        oFacility.POS = dtPOS.Rows[0]["sPOSName"].ToString();
                    }
                    if (dtPOS != null)
                    {
                        dtPOS.Dispose();
                        dtPOS = null;
                    }
                    oPOS.Dispose();
                    oPOS = null;

                    oFacility.Address1 = dtFacility.Rows[0]["sAddress1"].ToString();
                    oFacility.Address2 = Convert.ToString(dtFacility.Rows[0]["sAddress2"]);
                    oFacility.Zip = dtFacility.Rows[0]["sZip"].ToString();
                    oFacility.City = dtFacility.Rows[0]["sCity"].ToString();
                    oFacility.State = dtFacility.Rows[0]["sState"].ToString();
                    oFacility.Phone = dtFacility.Rows[0]["sPhone"].ToString();
                    oFacility.Fax = dtFacility.Rows[0]["sFax"].ToString();
                    oFacility.FacilityType = (FacilityType)Convert.ToInt32(dtFacility.Rows[0]["nFacilityType"]);
                    oFacility.ClinicID = Convert.ToInt64(dtFacility.Rows[0]["nClinicID"]);
                    oFacility.FaclityCLIANumber = Convert.ToString(dtFacility.Rows[0]["sCLIANo"]);
                    oFacility.CarrierNumber = Convert.ToString(dtFacility.Rows[0]["sCarrierNumber"]);
                    oFacility.Locality = Convert.ToString(dtFacility.Rows[0]["sLocality"]);
                    oFacility.AreaCode = Convert.ToString(dtFacility.Rows[0]["sAreaCode"]);
                    oFacility.County = Convert.ToString(dtFacility.Rows[0]["sCounty"]);
                    oFacility.Country = Convert.ToString(dtFacility.Rows[0]["sCountry"]);
                    oFacility.sMammogramCertNumber = Convert.ToString(dtFacility.Rows[0]["sMammogramCertNumber"]);

                    oFacility.PhysicalAddContactName = Convert.ToString(dtFacility.Rows[0]["sPhyContactName"]);
                    oFacility.PhysicalAddressline1 = Convert.ToString(dtFacility.Rows[0]["sPhyAddressline1"]);
                    oFacility.PhysicalAddressline2 = Convert.ToString(dtFacility.Rows[0]["sPhyAddressline2"]);
                    oFacility.PhysicalCity = Convert.ToString(dtFacility.Rows[0]["sPhyCity"]);
                    oFacility.PhysicalState = Convert.ToString(dtFacility.Rows[0]["sPhyState"]);
                    oFacility.PhysicalZIP = Convert.ToString(dtFacility.Rows[0]["sPhyZIP"]);
                    oFacility.PhysicalAreaCode = Convert.ToString(dtFacility.Rows[0]["sPhyAreaCode"]);
                    oFacility.PhysicalCountry = Convert.ToString(dtFacility.Rows[0]["sPhyCountry"]);
                    oFacility.PhysicalCounty = Convert.ToString(dtFacility.Rows[0]["sPhyCounty"]);
                    oFacility.PhysicalPhoneNo = Convert.ToString(dtFacility.Rows[0]["sPhyPhoneNo"]);
                    oFacility.PhysicalPagerNo = Convert.ToString(dtFacility.Rows[0]["sPhyPagerNo"]);
                    oFacility.PhysicalFAX = Convert.ToString(dtFacility.Rows[0]["sPhyFAX"]);
                    oFacility.PhysicalEmail = Convert.ToString(dtFacility.Rows[0]["sPhyEmail"]);
                    oFacility.PhysicalURL = Convert.ToString(dtFacility.Rows[0]["sPhyURL"]);

                    oDB.Disconnect();
                }

                return oFacility;
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {
                MessageBox.Show("ERROR : " + dbEX.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (dtFacility != null)
                {
                    dtFacility.Dispose();
                    dtFacility = null;
                }
            }
        }

        public Facilities GetFacilities(Int64 ClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Facility oFacility;
            Facilities oFacilities;
            DataTable dtFacility = null;
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                //Select the fields for given FaciltyID
                strQuery = " SELECT nFacilityID,ISNULL(sFacilityCode,'') AS sFacilityCode, "
                          + " ISNULL(sFacilityName,'') AS sFacilityName,ISNULL(sNPI,'') AS sNPI, "
                          + " ISNULL(sMedicadID,'') AS sMedicadID,ISNULL(sBlueShieldID,'') AS sBlueShieldID, "
                          + " ISNULL(sMedicareID,0) AS sMedicareID,ISNULL(nPOSID,0) AS nPOSID, "
                          + " ISNULL(sAddress1,'') AS sAddress1,ISNULL(sAddress2,'') AS sAddress2, "
                          + " ISNULL(sZip,'') AS sZip,ISNULL(sCity,'') AS sCity,ISNULL(sState,'') AS sState, "
                          + " ISNULL(sPhone,'') AS sPhone,ISNULL(sFax,'') AS sFax,ISNULL(nFacilityType,0) AS nFacilityType,nClinicID, "
                          + " ISNULL(sCLIANo,'') AS sCLIANo "
                          + " FROM   BL_Facility_MST WITH (NOLOCK) "
                          + "WHERE bIsBlocked = '" + false + "' AND nClinicID = " + ClinicId + " ";

                oDB.Retrive_Query(strQuery, out dtFacility);

                if (dtFacility != null && dtFacility.Rows.Count > 0)
                {
                    oFacilities = new Facilities();

                    for (int i = 0; i < dtFacility.Rows.Count; i++)
                    {
                        oFacility = new Facility();

                        oFacility.FacilityID = Convert.ToInt64(dtFacility.Rows[i]["nFacilityID"]);
                        oFacility.FacilityCode = dtFacility.Rows[i]["sFacilityCode"].ToString();
                        oFacility.FacilityName = dtFacility.Rows[i]["sFacilityName"].ToString();
                        oFacility.NPI = dtFacility.Rows[i]["sNPI"].ToString();
                        oFacility.MedicadID = dtFacility.Rows[i]["sMedicadID"].ToString();
                        oFacility.BlueShieldID = dtFacility.Rows[i]["sBlueShieldID"].ToString();
                        oFacility.MedicareID = dtFacility.Rows[i]["sMedicareID"].ToString();

                        oFacility.POSID = Convert.ToInt64(dtFacility.Rows[i]["nPOSID"]);

                        CLsBL_TOSPOS oPOS = new CLsBL_TOSPOS(_databaseconnectionstring);
                        DataTable dtPOS = oPOS.GetPOS(oFacility.POSID);
                        if (dtPOS != null && dtPOS.Rows.Count > 0)
                        {
                            oFacility.POS_CODE = dtPOS.Rows[0]["sPOSCode"].ToString();
                            oFacility.POS = dtPOS.Rows[0]["sPOSName"].ToString();
                        }
                        if (dtPOS != null)
                        {
                            dtPOS.Dispose();
                            dtPOS = null;
                        }
                        oPOS.Dispose();
                        oPOS = null;

                        oFacility.Address1 = dtFacility.Rows[i]["sAddress1"].ToString();
                        oFacility.Address2 = dtFacility.Rows[i]["sAddress2"].ToString();
                        oFacility.Zip = dtFacility.Rows[i]["sZip"].ToString();
                        oFacility.City = dtFacility.Rows[i]["sCity"].ToString();
                        oFacility.State = dtFacility.Rows[i]["sState"].ToString();
                        oFacility.Phone = dtFacility.Rows[i]["sPhone"].ToString();
                        oFacility.FacilityType = (FacilityType)Convert.ToInt32(dtFacility.Rows[i]["nFacilityType"]);
                        oFacility.ClinicID = Convert.ToInt64(dtFacility.Rows[i]["nClinicID"]);
                        oFacility.FaclityCLIANumber = Convert.ToString(dtFacility.Rows[i]["sCLIANo"]);
                        oFacility.sMammogramCertNumber = Convert.ToString(dtFacility.Rows[i]["sMammogramCertNumber "]);
                        oFacilities.Add(oFacility);
                        oFacility = null;

                    }//for (int i = 0; i < dtFacility.Rows.Count;i++)

                    return oFacilities;

                }//if (dtFacility != null && dtFacility.Rows.Count > 0)
                return null;
            }
            catch (gloDatabaseLayer.DBException dbEX)
            {
                MessageBox.Show("ERROR : " + dbEX.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                if (dtFacility != null)
                {
                    dtFacility.Dispose();
                    dtFacility = null;
                }
            }

        }

        public string GetFacilityCLIA(string FacilityCode, Int64 ClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Object retVal = null;
            string _FacilityCLIANo = "";
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "select ISNULL(sCLIANo,'') AS sCLIANo from  BL_Facility_MST WITH (NOLOCK) where UPPER(sFacilityCode) = '" + FacilityCode.Trim().ToUpper() + "' AND nClinicID = " + ClinicId + "";
                retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                if (retVal != null && retVal != DBNull.Value && Convert.ToString(retVal).Trim() != "")
                {
                    _FacilityCLIANo = Convert.ToString(retVal).Trim();
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (retVal != null) { retVal = null; }
            }

            return _FacilityCLIANo;
        }

        public Int64 GetFacilityPOS(string FacilityCode, Int64 ClinicId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Object retVal = null;
            Int64 _FacilityPOS = 0;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "select ISNULL(nPosID,0) AS nPosID from  BL_Facility_MST WITH (NOLOCK) where UPPER(sFacilityCode) = '" + FacilityCode.Trim().ToUpper() + "' AND nClinicID = " + ClinicId + "";
                retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                if (retVal != null && retVal != DBNull.Value && Convert.ToString(retVal).Trim() != "")
                {
                    _FacilityPOS = Convert.ToInt64(retVal);
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (retVal != null) { retVal = null; }
            }

            return _FacilityPOS;
        }

        public Int64 AddModify(Facility oFacility)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;

            try
            {
                oDB.Connect(false);

                oParameters.Add("@nFacilityID", oFacility.FacilityID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sFacilityCode", oFacility.FacilityCode, ParameterDirection.Input, SqlDbType.VarChar, 100);
                oParameters.Add("@sFacilityName", oFacility.FacilityName, ParameterDirection.Input, SqlDbType.VarChar, 100);
                oParameters.Add("@sNPI", oFacility.NPI, ParameterDirection.Input, SqlDbType.VarChar, 100);
                oParameters.Add("@sMedicadID", oFacility.MedicadID, ParameterDirection.Input, SqlDbType.VarChar, 100);
                oParameters.Add("@sBlueShieldID", oFacility.BlueShieldID, ParameterDirection.Input, SqlDbType.VarChar, 100);
                oParameters.Add("@sMedicareID", oFacility.MedicareID, ParameterDirection.Input, SqlDbType.VarChar, 100);
                oParameters.Add("@sTaxID", oFacility.TaxID, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oParameters.Add("@nPOSID", oFacility.POSID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sAddress1", oFacility.Address1, ParameterDirection.Input, SqlDbType.VarChar, 200);
                oParameters.Add("@sAddress2", oFacility.Address2, ParameterDirection.Input, SqlDbType.VarChar, 200);
                oParameters.Add("@sZip", oFacility.Zip, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oParameters.Add("@sCity", oFacility.City, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oParameters.Add("@sState", oFacility.State, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oParameters.Add("@sPhone", oFacility.Phone, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oParameters.Add("@sFax", oFacility.Fax, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oParameters.Add("@nFacilityType", oFacility.FacilityType.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@nClinicID", oFacility.ClinicID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sCLIANo", oFacility.FaclityCLIANumber, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sMammogramCertNumber ", oFacility.sMammogramCertNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sCarrierNumber", oFacility.CarrierNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sLocality", oFacility.Locality, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sAreaCode", oFacility.AreaCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sCounty", oFacility.County, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sCountry", oFacility.Country, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sTaxonomyCode", oFacility.TaxonomyCode, ParameterDirection.Input, SqlDbType.VarChar);
                //7022Items: Home Billing
                oParameters.Add("@bReportPatientAddress", oFacility.ReportPatientAddress, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Execute("BL_INUP_Facility", oParameters, out _oResult);

                SaveFacilityPhysicalAddress(oFacility, Convert.ToInt64(_oResult));

                return Convert.ToInt64(_oResult);

            }
            catch (gloDatabaseLayer.DBException dbEX)
            {
                MessageBox.Show("ERROR : " + dbEX.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                _oResult = null;
            }
        }

        private int SaveFacilityPhysicalAddress(Facility oFacility, Int64 FacilityID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            int iResult = 0;

            try
            {
                oDBParameters.Add("@FacilityID", FacilityID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sContactName", oFacility.PhysicalAddContactName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sAddressline1", oFacility.PhysicalAddressline1, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sAddressline2", oFacility.PhysicalAddressline2, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sCity", oFacility.PhysicalCity, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sState", oFacility.PhysicalState, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sZIP", oFacility.PhysicalZIP, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sAreaCode", oFacility.PhysicalAreaCode, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sCountry", oFacility.PhysicalCountry, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sCounty", oFacility.PhysicalCounty, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPhoneNo", oFacility.PhysicalPhoneNo, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sFAX", oFacility.PhysicalFAX, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sPagerNo", oFacility.PhysicalPagerNo, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sEmail", oFacility.PhysicalEmail, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sURL", oFacility.PhysicalURL, ParameterDirection.Input, SqlDbType.VarChar);

                oDB.Connect(false);
                //execute the Insert/Update Facility Physical Address stored procedure
                iResult = oDB.Execute("BL_InUpFacility_PhysicalAddress", oDBParameters);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return iResult;
        }

        public bool IsExistsFacility(Int64 FacilityId, string Code, string FacilityName)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                if (FacilityId == 0)
                {
                    //strQuery = "select count(nFacilityID) from BL_Facility_MST where sFacilityCode = "+Code+" OR sFacilityName = '"+FacilityName+"'";
                    //
                    strQuery = "select count(nFacilityID) from BL_Facility_MST WITH (NOLOCK) where (sFacilityCode = '" + Code + "' OR sFacilityName = '" + FacilityName + "') AND nClinicID=" + this.ClinicID + " ";
                    //
                }
                else
                {
                    //strQuery = "select count(nFacilityID) from BL_Facility_MST where (sFacilityCode = " + Code + " OR sFacilityName = '" + FacilityName  + "') AND nFacilityID <> "+ FacilityId +"";
                    //
                    strQuery = "select count(nFacilityID) from BL_Facility_MST WITH (NOLOCK) where ((sFacilityCode = '" + Code + "' OR sFacilityName = '" + FacilityName + "') AND nFacilityID <> " + FacilityId + ") AND nClinicID=" + this.ClinicID + " ";
                    //

                }

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {

                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }

        public DataTable GetFacilities()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtFacility = null;
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                //Select the fields for given FaciltyID

                if (this.ClinicID == 0)
                {
                    strQuery = "SELECT nFacilityID,sFacilityCode,sFacilityName,sNPI,sMedicadID,sBlueShieldID, "
                              + "sMedicareID,sTaxID,sCity,sPhone "
                              + "FROM   BL_Facility_MST WITH (NOLOCK) WHERE bIsBlocked = '" + false + "'";
                }
                else
                {

                    strQuery = "SELECT nFacilityID,sFacilityCode,sFacilityName,sNPI,sMedicadID,sBlueShieldID, "
                              + "sMedicareID,sTaxID,sCity,sPhone "
                              + "FROM   BL_Facility_MST WITH (NOLOCK) WHERE bIsBlocked = '" + false + "' AND nClinicID = " + this.ClinicID + " ";
                }

                oDB.Retrive_Query(strQuery, out dtFacility);

                if (dtFacility != null && dtFacility.Rows.Count > 0)
                {
                    return dtFacility;
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

                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        public DataTable GetBlockedFacilities()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtFacility = null;
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                //Select the fields for given FaciltyID
                strQuery = "SELECT nFacilityID,sFacilityCode,sFacilityName,sNPI,sMedicadID,sBlueShieldID, "
                          + "sMedicareID,sCity,sPhone "
                          + "FROM   BL_Facility_MST WITH (NOLOCK) WHERE bIsBlocked = '" + true + "' AND nClinicID = " + this.ClinicID + " ";

                oDB.Retrive_Query(strQuery, out dtFacility);
                return dtFacility;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        public bool BlockFacility(Int64 nFacilityId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE BL_Facility_MST WITH (READPAST) SET bIsBlocked = '" + true + "' WHERE nFacilityID = " + nFacilityId;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool Unblock(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE BL_Facility_MST WITH (READPAST) SET bIsBlocked = '" + false + "' WHERE nFacilityID = " + ID + " ";
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool CanDelete(Int64 ID)
        {
            //Logic need to be implemented
            return false;
        }

        public bool Delete(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_Facility_MST where nFacilityID =" + ID;
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
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool CleanUp()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " DELETE FROM BL_Facility_MST WHERE bIsBlocked = '" + true + "' AND nClinicID =" + this.ClinicID + " ";
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public void AddFacility_Location(Int64 FacID, string Facode, string FacDesc, Int64 LocID, string LocCode, string LocDesc)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            // Object _oResult = new object();

            try
            {
                oDB.Connect(false);
                //@FacilityID,@FacilityCode,@FacilityDesc,@LocationID,@LocationCode,@LocationDesc,@ProviderID, @ClinicID 
                oParameters.Add("@FacilityID", FacID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@FacilityCode", Facode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@FacilityDesc", FacDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@LocationID", LocID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@LocationCode", LocCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@LocationDesc", LocDesc, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ProviderID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("BL_INSERT_Facility_Location", oParameters);



            }
            catch (gloDatabaseLayer.DBException dbEX)
            {
                MessageBox.Show("ERROR : " + dbEX.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                //   _oResult = null;
            }
        }

        public bool DeleteFacility_Location(Int64 FacilityID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_Facility_Location where nFacilityID = " + FacilityID + " AND nClinicID = " + _ClinicID;
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
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
        }

        public bool DeleteFacility_QualifierIDs(Int64 FacilityID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_Facility_ID_Qualifiers where nFacilityID = " + FacilityID + " AND nClinicID = " + _ClinicID;
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
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public DataTable GetFacility_Location(Int64 FacilityID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtFacility = null;
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                //Select the fields for given FaciltyID

                strQuery = "select  nLocationID ,sLocationDesc from BL_Facility_Location WITH (NOLOCK) where nFacilityID =" + FacilityID + " AND nClinicID = " + ClinicID + "";


                oDB.Retrive_Query(strQuery, out dtFacility);

                if (dtFacility != null && dtFacility.Rows.Count > 0)
                {
                    return dtFacility;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        public FacilityType GetFacilityType(string sFacilityCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            Object retVal = null;
            FacilityType oFacilityType = FacilityType.None;

            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT ISNULL(nFacilityType,0) AS nFacilityType " +
                 " FROM BL_Facility_MST WITH (NOLOCK) WHERE UPPER(sFacilityCode) = '" + sFacilityCode.Trim().ToUpper() + "' AND nClinicID = " + this.ClinicID + "";
                retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                if (retVal != null && Convert.ToString(retVal).Trim() != "")
                { oFacilityType = ((FacilityType)Convert.ToInt32(retVal)); }
                oDB.Disconnect();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (retVal != null) { retVal = null; }
            }

            return oFacilityType;
        }



        #endregion " Private & Public Methods "

    }
    #endregion

    #region "ICD9"
    public class ICD9 : IDisposable
    {
        private Int64 _nICD9ID = 0;
        private string _sICD9Code = "";
        private string _description = "";
        private Int64 _nSpecialtyID = 0;
        private bool _Inactive = false;
        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "";
        private string _Speciality = "";


        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private Int64 _nImmediacy = 0;

        public Int64 Immediacy
        {
            get { return _nImmediacy; }
            set { _nImmediacy = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //

        #region Properties

        public Int64 ICD9ID
        {
            get { return _nICD9ID; }
            set { _nICD9ID = value; }
        }

        public gloICD.CodeRevision ICDRevision { get; set; }


        public Int64 SpecialtyID
        {
            get { return _nSpecialtyID; }
            set { _nSpecialtyID = value; }
        }

        public string ICD9Code
        {
            get { return _sICD9Code; }
            set { _sICD9Code = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public bool Inactive
        {
            get { return _Inactive; }
            set { _Inactive = value; }
        }
        //Property Procedure Added by Mayuri:20091229-To fix issue-5303-Edit>Billing configuration>ICD9>'Specialty' column is NOT displayed.
        public string Speciality
        {
            get { return _Speciality; }
            set { _Speciality = value; }
        }

        #endregion

        #region "Constructor & Destructor"


        public ICD9(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~ICD9()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// this method gets All types of specialties available
        /// from the database
        /// </summary>
        /// <returns>data table containing All specialties</returns>
        internal DataTable GetAllSpecialty()
        {

            DataTable dtSpecialty = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT nSpecialtyID, sDescription FROM Specialty_MST WITH (NOLOCK) where nClinicID = " + ClinicID + " ORDER BY sDescription ";
                oDB.Retrive_Query(strQuery, out dtSpecialty);
                return dtSpecialty;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        /// <summary>
        /// this method insert new ICD9 to database   
        /// and also Update old ICD9 
        /// </summary>
        /// <returns>bool --> true for successful insertion</returns>
        internal Int64 Save()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameter oDBParameter;
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                object _intresult = 0;
                oDB.Connect(false);

                oDBParameters.Add("@ICD9ID", this.ICD9ID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ICD9Code", this.ICD9Code, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Description", this.Description, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@SpecialtyID", this.SpecialtyID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@Inactive", this.Inactive, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@Immediacy", this.Immediacy, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nICDRevision", this.ICDRevision.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.SmallInt);

                _result = oDB.Execute("gsp_InUpICD9", oDBParameters, out _intresult);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        //internal DataTable GetICD(long _nICDID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //    gloDatabaseLayer.DBParameters oParameters = null;
        //    DataTable _dt = null;
        //    try
        //    {

        //        oParameters = new gloDatabaseLayer.DBParameters();
        //        oParameters.Add("@nICDID", _nICDID, ParameterDirection.Input, SqlDbType.BigInt);               
        //        oDB.Connect(false);
        //        oDB.Retrive("gsp_GetSelectedICD", oParameters, out _dt);
        //        return _dt;

        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //        throw dbEx;
        //    }
        //    finally
        //    {
        //        if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); }
        //        if (oDB != null) { oDB.Dispose(); }
        //    }

        //}
        //To be Checked

        internal DataTable GetICD9(long _nICD9ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dt = null;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nICDID", _nICD9ID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetSelectedICD", oParameters, out _dt);
                return _dt;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

        public DataTable GetICD9(string icd9code)
        {
            DataTable _result = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT  ISNULL(nICD9ID,0) AS nICD9ID,ISNULL(sDescription,'') AS sDescription FROM ICD9 WITH (NOLOCK) WHERE UPPER(sICD9Code) = '" + icd9code.Trim().ToUpper() + "' ";
                oDB.Retrive_Query(strQuery, out _result);
                return _result;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        internal DataTable GetICDByRevision(gloICD.CodeRevision ICDRevision)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dt = null;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nICDRevision", ICDRevision.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@ClinicId", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetICD9Or10", oParameters, out _dt);
                return _dt;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

        public bool IsExistsICD(Int64 ICDId, string ICDCode, string ICDName, Int16 _nICDRevision)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dt = null;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nICDID", ICDId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sICDCode", ICDCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sICDName", ICDName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nICDRevision", _nICDRevision, ParameterDirection.Input, SqlDbType.Int);
                oParameters.Add("@ClinicId", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_IsExistsICD", oParameters, out _dt);

                if (_dt != null && _dt.Rows.Count > 0)
                {
                    if (Convert.ToInt64(_dt.Rows[0]["ICDCount"]) > 0)
                    {
                        _result = true;
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
            }
            return _result;
        }

        public bool IsUsedICDCode(string ICDCode)
        {
            bool _result = false;
            object _error = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            int Cnt = 0;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@sICDCode", ICDCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@ICDCount", Cnt, ParameterDirection.Output, SqlDbType.Int);
                oDB.Connect(false);
                oDB.Execute("gsp_IsUsedICDCode", oParameters, out _error);

                if (Cnt > 0)
                {
                    _result = true;
                }


            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        internal DataTable GetICD9s()
        {
            //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //Connect to database
                oDB.Connect(false);

                //Query to retrieve data from ICD9 master table
                //String strQuery = "SELECT nICD9ID, sICD9Code, sDescription,nSpecialtyID FROM ICD9";
                //String strQuery = "SELECT ISNULL(nICD9ID,0) AS nICD9ID, ISNULL(sICD9Code,'') AS sICD9Code,ISNULL(sDescription,'') AS sDescription,ISNULL(nSpecialtyID,0) AS nSpecialtyID,bInActive,"
                //+ " CASE ISNULL(bInActive,0) WHEN 0 THEN 'Active'WHEN 1 THEN 'Inactive' END AS sStatus "
                //+ " FROM ICD9 where (bIsBlocked = 'false' OR bIsBlocked IS NULL)  AND ISNULL(nClinicID,1) = " + _ClinicID;

                //Query modified by Mayuri:20991229-To fix issue-5303-Edit>Billing configuration>ICD9>'Specialty' column is NOT displayed.
                string strQuery = "SELECT ISNULL(ICD9.nICD9ID,0) AS nICD9ID, ISNULL(ICD9.sICD9Code,'') AS sICD9Code," +
                                  "ISNULL(ICD9.sDescription,'') AS sDescription,ISNULL(ICD9.nSpecialtyID,0) AS nSpecialtyID," +
                                  "ICD9.bInActive,CASE ISNULL(bInActive,0) WHEN 0 THEN 'Active'WHEN 1 THEN 'Inactive' END AS sStatus," +
                                  " ISNULL(Specialty_MST.sDescription,'') as Speciality FROM ICD9 WITH (NOLOCK) " +
                                  " LEFT OUTER JOIN Specialty_MST WITH (NOLOCK) ON ICD9.nSpecialtyID = Specialty_MST.nSpecialtyID where (ICD9.bIsBlocked = 'false' OR ICD9.bIsBlocked IS NULL)  AND ISNULL(ICD9.nClinicID,1) = " + _ClinicID;
                //End code added by Mayuri:20091229

                //Get data into datatable
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

        //public string GetICD9Description(string Code)
        //{
        //    string _result = "";
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        oDB.Connect(false);

        //        //string sqlQuery = "SELECT sDescription FROM ICD9 WHERE sICD9Code = '" + Code + "'";
        //        //
        //        string sqlQuery = "";
        //        if (this.ClinicID == 0)
        //            sqlQuery = "SELECT ISNULL(sDescription,'') AS sDescription FROM ICD9 WITH (NOLOCK) WHERE sICD9Code = '" + Code + "'";
        //        else
        //            sqlQuery = "SELECT ISNULL(sDescription,'') AS sDescription FROM ICD9 WITH (NOLOCK) WHERE sICD9Code = '" + Code + "' AND nClinicID=" + this.ClinicID + " ";
        //        //
        //        oDB.Retrive_Query(sqlQuery, out dt);
        //        if (dt.Rows.Count > 0)
        //        {
        //            _result = Convert.ToString(dt.Rows[0]["sDescription"]);
        //        }

        //    }
        //    catch (gloDatabaseLayer.DBException ex)
        //    {
        //        ex.ERROR_Log(ex.ToString());
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //        return null;
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oParameters.Dispose();
        //        oDB.Dispose();
        //    }
        //    return _result;
        //}

        //public DataTable GetBlockedICD9s()
        //{
        //    //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    try
        //    {
        //        //Connect to database
        //        oDB.Connect(false);

        //        //Query to retrieve data from ICD9 master table
        //        //String strQuery = "SELECT nICD9ID, sICD9Code, sDescription,nSpecialtyID FROM ICD9";
        //        String strQuery = "SELECT ISNULL(nICD9ID,0) AS nICD9ID,ISNULL(sICD9Code,'') AS sICD9Code,ISNULL(sDescription,'') AS sDescription,ISNULL(nSpecialtyID,'') AS nSpecialtyID FROM ICD9 WITH (NOLOCK) where bIsBlocked = '" + true + "' AND nClinicID = " + ClinicID + "";

        //        //Get data into datatable
        //        DataTable dt = new DataTable();
        //        oDB.Retrive_Query(strQuery, out dt);

        //        //Disconnect from database
        //        oDB.Disconnect();
        //        return dt;
        //    }
        //    catch (gloDatabaseLayer.DBException ex)
        //    {
        //        ex.ERROR_Log(ex.ToString());
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //        return null;
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //    }
        //}

        //public bool Unblock(Int64 ID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string strQuery = "";
        //    try
        //    {
        //        oDB.Connect(false);
        //        strQuery = " UPDATE ICD9 WITH (READPAST) SET bIsBlocked = '" + false + "' WHERE nICD9ID = " + ID + " ";
        //        int _result = oDB.Execute_Query(strQuery);
        //        return Convert.ToBoolean(_result);
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //        return false;
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //    }
        //}

        //public bool IsExistsICD()
        //{
        //    bool _result = false;
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
        //    gloDatabaseLayer.DBParameters oParameters = null;
        //    DataTable _dt = null;
        //    try
        //    {
        //        oParameters = new gloDatabaseLayer.DBParameters();
        //        oParameters.Add("@nICDID", this.ICD9ID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oParameters.Add("@sICDCode", this.ICD9Code, ParameterDirection.Input, SqlDbType.VarChar);
        //        oParameters.Add("@nICDRevision", this.ICDRevision, ParameterDirection.Input, SqlDbType.Int);
        //        oParameters.Add("@ClinicId", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDB.Connect(false);
        //        oDB.Retrive("gsp_IsExistsICD", oParameters, out _dt);

        //        if (_dt != null && _dt.Rows.Count > 0)
        //        {
        //            if (Convert.ToInt64(_dt.Rows[0]["ICDCount"]) > 0)
        //            {
        //                _result = true;
        //            }
        //        }

        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //        throw dbEx;
        //    }
        //    finally
        //    {
        //        if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); }
        //        if (oDB != null) { oDB.Dispose(); }
        //        if (_dt != null) { _dt.Dispose(); _dt = null; }
        //    }
        //    return _result;
        //}



        //public bool BlockICD9(Int64 ICD9Id)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string strQuery = "";
        //    try
        //    {
        //        oDB.Connect(false);
        //        strQuery = " UPDATE ICD9 WITH (READPAST) SET bIsBlocked = '" + true + "' WHERE nICD9ID = " + ICD9Id;
        //        int _result = oDB.Execute_Query(strQuery);
        //        return Convert.ToBoolean(_result);


        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //        return false;
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //    }
        //}

        public bool IsCodeInUse(string ICDCode, gloICD.CodeRevision ICDRevision)
        {
            bool _result = false;
            // object _error = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;

            try
            {
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@sICDCode", ICDCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nICDRevision", ICDRevision.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                oDB.Connect(false);

                object cnt = oDB.ExecuteScalar("gsp_IsUsedICDCode", oParameters);

                if (cnt != null && Convert.ToInt32(cnt) > 0)
                {
                    _result = true;
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        public bool Delete(Int64 ID)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nICDID", ID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                int cnt = oDB.Execute("gsp_DeleteSelectedICD", oParameters);

                if (cnt > 0)
                {
                    _result = true;
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        //public bool DeleteAll()
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string strQuery = "";
        //    DataTable dt = new DataTable();
        //    Int64 _ICD9Id = 0;
        //    int _result = 0;
        //    try
        //    {
        //        oDB.Connect(false);

        //        //Get all blocked ICD9's in data table
        //        strQuery = "SELECT nICD9ID FROM ICD9 WITH (NOLOCK) WHERE bIsBlocked = 'true' AND nClinicID = " + this.ClinicID + " ";
        //        oDB.Retrive_Query(strQuery, out dt);

        //        // Check for blocked records exists or not
        //        if (dt != null && dt.Rows.Count > 0)
        //        {

        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                _ICD9Id = 0;
        //                _ICD9Id = Convert.ToInt64(dt.Rows[i][0]);

        //                //Check if the item is not in use or is not System defined
        //                if (CanDelete(_ICD9Id))
        //                {
        //                    strQuery = " DELETE FROM ICD9 WHERE nICD9ID = " + _ICD9Id;
        //                    _result = oDB.Execute_Query(strQuery);
        //                    strQuery = "";
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Cannot Delete :Item in use or System defined", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    return true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("No Blocked Records found", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return true;
        //        }

        //        return Convert.ToBoolean(_result);

        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //        return false;
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //        dt.Dispose();
        //    }
        //}

        //public bool CanDelete(Int64 ID)
        //{
        //    //Logic need to be implemented
        //    return false;
        //}


        //TODO : Tobe completed

        public bool MoveICD(Int64 ID, string action)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nICDID", ID, ParameterDirection.Input, SqlDbType.BigInt);
                if (action == "9to10")
                {
                    oParameters.Add("@nICDRevision", gloGlobal.gloICD.CodeRevision.ICD10.GetHashCode(), ParameterDirection.Input, SqlDbType.SmallInt);
                    oParameters.Add("@nCodeType", 1, ParameterDirection.Input, SqlDbType.SmallInt);
                }
                else if (action == "10to9")
                {
                    oParameters.Add("@nICDRevision", gloGlobal.gloICD.CodeRevision.ICD9.GetHashCode(), ParameterDirection.Input, SqlDbType.SmallInt);
                    oParameters.Add("@nCodeType", 0, ParameterDirection.Input, SqlDbType.SmallInt);
                }
                oDB.Connect(false);
                int cnt = oDB.Execute("gsp_MoveICD", oParameters);

                if (cnt > 0)
                {
                    _result = true;
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        //TODO : Tobe completed
        public bool MarkAsInvalidForEDI(string ICDCode, string ICDDescription, gloICD.CodeRevision ICDRevision)
        {

            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@sICDCode", ICDCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sICDDescription", ICDDescription, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nCodeRevision", ICDRevision.GetHashCode(), ParameterDirection.Input, SqlDbType.SmallInt);

                oDB.Connect(false);
                int cnt = oDB.Execute("gsp_MarkAsInvalidForEDI", oParameters);

                if (cnt > 0)
                {
                    _result = true;
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return _result;

        }

        //TODO : Tobe completed
        //public void MarkAsValidForEDI(long ID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string strQuery = "";
        //    DataTable dt = new DataTable();
        //    int _result = 0;
        //    try
        //    {
        //        oDB.Connect(false);

        //        dt = GetICD9(ID);

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            string _Code = Convert.ToString(dt.Rows[0]["sICD9Code"]).Replace("'", "''");
        //            string _Description = Convert.ToString(dt.Rows[0]["sDescription"]).Replace("'", "''");

        //            strQuery = " DELETE FROM ICD9_InvalidEDI WHERE sICD9Code = '" + _Code + "'";
        //            oDB.Execute_Query(strQuery);


        //            strQuery = " INSERT INTO ICD9_InvalidEDI (sICD9Code, sDescription) " +
        //                       " VALUES ('" + _Code + "','" + _Description + "')";
        //            oDB.Execute_Query(strQuery);
        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //        dt.Dispose();
        //    }

        //}

        //public void InvalidateICD9ForEDI(long _ICD9ID)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string strQuery = "";
        //    DataTable dt = new DataTable();
        //    int _result = 0;
        //    try
        //    {
        //        oDB.Connect(false);

        //        dt = GetICD9(_ICD9ID);

        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            string _Code = Convert.ToString(dt.Rows[0]["sICD9Code"]).Replace("'", "''");
        //            string _Description = Convert.ToString(dt.Rows[0]["sDescription"]).Replace("'","''");

        //            strQuery = " DELETE FROM ICD9_InvalidEDI WHERE sICD9Code = '" + _Code + "'";
        //            oDB.Execute_Query(strQuery);


        //            strQuery = " INSERT INTO ICD9_InvalidEDI (sICD9Code, sDescription) "+
        //                       " VALUES ('" + _Code + "','" + _Description + "')";
        //            oDB.Execute_Query(strQuery);
        //        }
        //    }
        //    catch (gloDatabaseLayer.DBException dbEx)
        //    {
        //        dbEx.ERROR_Log(dbEx.ToString());
        //    }
        //    finally
        //    {
        //        oDB.Disconnect();
        //        oDB.Dispose();
        //        dt.Dispose();
        //    }
        //}

        //public bool IsInValidICD(string ICD9Code)
        //{
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    string _sqlQuery = "";
        //    Object _retVal = null;
        //    bool _IsInValidICD = false;

        //    try
        //    {
        //        oDB.Connect(false);
        //        _sqlQuery = "SELECT COUNT(sICD9Code) FROM ICD9_InvalidEDI WITH (NOLOCK) where UPPER(sICD9Code) = '" + ICD9Code.ToUpper() + "'";
        //        _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
        //        if (_retVal != null)
        //        {
        //            _IsInValidICD = Convert.ToBoolean(_retVal);
        //        }
        //        oDB.Disconnect();
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
        //    }
        //    finally
        //    {
        //        if (oDB != null) { oDB.Dispose(); }
        //    }
        //    return _IsInValidICD;
        //}

        internal DataTable GetInvalidICD9(gloICD.CodeRevision ICDRevision)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dt = null;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@ClinicId", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nCodeRevision", ICDRevision.GetHashCode(), ParameterDirection.Input, SqlDbType.SmallInt);
                oDB.Connect(false);
                oDB.Retrive("gsp_GetInvalidICD9", oParameters, out _dt);
                return _dt;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
        }

        public bool MarkAsValidForEDI(List<string> Icd9Code)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            try
            {
                string sICDCodes = string.Empty;
                for (int _Index = 0; _Index < Icd9Code.Count; _Index++)
                {
                    if (sICDCodes.Trim().Length > 0)
                        sICDCodes = sICDCodes + "," + "'" + Icd9Code[_Index].ToString().Trim() + "'";
                    else
                        sICDCodes = "('" + Icd9Code[_Index].ToString().Trim() + "'";

                }
                sICDCodes = sICDCodes + ")";
                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@sICDCode", sICDCodes, ParameterDirection.Input, SqlDbType.VarChar);

                oDB.Connect(false);
                int cnt = oDB.Execute("gsp_ValidICD9", oParameters);

                if (cnt > 0)
                {
                    _result = true;
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return _result;
        }



    }
    #endregion

    #region "TOSPOS"
    public class CLsBL_TOSPOS
    {
        #region " Declarations "

        Int64 _ClinicID = 0;
        string _messageBoxCaption = "";
        string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


        #endregion " Declarations "

        #region "Constructor & Destructor"

        //public CLsBL_TOSPOS()
        //    {
        //        //Code added on 8/04/2008 -by Sagar Ghodke for implementing ClinicID;
        //        _nClinicID = Convert.ToInt64(appSettings["ClinicID"]);

        //    }

        public CLsBL_TOSPOS(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~CLsBL_TOSPOS()
        {
            Dispose(false);
        }

        #endregion

        #region " TOS & POS Methods "

        public DataTable GetTOS(Int64 TOSId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTOS = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                if (TOSId > 0)
                {
                    // strQuery = "select nTOSID,sDescription from BL_TOS_MST where nTOSID= " + TOSId;
                    strQuery = "select ISNULL(nTOSID,0) AS nTOSID,ISNULL(sTOSCode,'') AS sTOSCode,ISNULL(sDescription,'') AS sDescription from BL_TOS_MST WITH (NOLOCK) where nTOSID= " + TOSId;
                }
                else
                {
                    //strQuery = "select nTOSID,sDescription from BL_TOS_MST where nClinicID = "+_nClinicID+" ";
                    strQuery = "select ISNULL(nTOSID,0) AS nTOSID,ISNULL(sTOSCode,'') AS sTOSCode,ISNULL(sDescription,'') AS sDescription from BL_TOS_MST WITH (NOLOCK) where bIsBlocked = '" + false + "' AND nClinicID = " + _ClinicID + " ";
                }
                oDB.Retrive_Query(strQuery, out dtTOS);
                return dtTOS;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }

            }
        }

        public DataTable GetPOS(Int64 POSId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPOS = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                if (POSId > 0)
                {
                    strQuery = "select ISNULL(nPOSID,0) AS nPOSID,ISNULL(sPOSCode,'') AS sPOSCode,ISNULL(sPOSName,'') AS sPOSName,ISNULL(sPOSDescription,'') AS sPOSDescription,ISNULL(nFacilityType,0) AS nFacilityType from BL_POS_MST WITH (NOLOCK) where nPOSID= " + POSId;
                }
                else
                {
                    //strQuery = "select nPOSID,sPOSCode,sPOSName,sPOSDescription from BL_POS_MST where nClinicID="+_nClinicID+" ";
                    strQuery = " select ISNULL(nPOSID,0) AS nPOSID,ISNULL(sPOSCode,'') AS sPOSCode,ISNULL(sPOSName,'') AS sPOSName,ISNULL(sPOSDescription,'') AS sPOSDescription,ISNULL(nFacilityType,0) AS nFacilityType, " +
                                " CASE ISNULL(nFacilityType,0)  WHEN 1 THEN 'Facility' WHEN 3 THEN 'NonFacility' ELSE '' END AS facilityType " +
                                " from BL_POS_MST WITH (NOLOCK) where bIsBlocked='" + false + "' AND nClinicID=" + _ClinicID + " ";
                }
                oDB.Retrive_Query(strQuery, out dtPOS);
                return dtPOS;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }


            }
        }

        public DataTable GetPOS(string POSCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPOS = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                if (POSCode != "")
                {
                    strQuery = " select ISNULL(nPOSID,0) AS nPOSID,ISNULL(sPOSCode,'') AS sPOSCode, " +
                               " ISNULL(sPOSName,'') AS sPOSName,ISNULL(sPOSDescription,'') AS sPOSDescription,ISNULL(nFacilityType,0) AS nFacilityType " +
                               " from BL_POS_MST WITH (NOLOCK) where UPPER(sPOSName)= '" + POSCode.ToUpper() + "' ";
                }
                oDB.Retrive_Query(strQuery, out dtPOS);
                return dtPOS;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }


            }
        }

        public Int64 AddModifyPOS(Int64 POSId, string sCode, string sName, string sDesc, int nFacilityType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);

                oParameters.Add("@nPOSID", POSId, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sPOSCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sPOSName", sName, ParameterDirection.Input, SqlDbType.VarChar, 100);
                oParameters.Add("@sPOSDescription", sDesc, ParameterDirection.Input, SqlDbType.VarChar, 1000);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nFacilityType", nFacilityType, ParameterDirection.Input, SqlDbType.Int);
                oDB.Execute("AB_INUP_POS", oParameters, out _oResult);

                gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.PlaceOfServices);

                return Convert.ToInt64(_oResult);


            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }

        public Int64 AddModifyTOS(Int64 TOSId, string sDescription, string sCode)
        {

            //Pass 0 to Add
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;

            try
            {
                oDB.Connect(false);

                oParameters.Add("@nTOSID", TOSId, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sDescription", sDescription, ParameterDirection.Input, SqlDbType.VarChar, 100);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sTOSCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("AB_INUP_TOS", oParameters, out _oResult);

                gloGlobal.gloPMMasters.ClearCache(gloGlobal.gloPMMasters.MasterType.TypeOfServices);

                return Convert.ToInt64(_oResult);
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }

        }

        public bool IsExistsPOS(Int64 POSId, string Code, string Name)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                if (POSId == 0)
                {
                    //strQuery = " select count(nPOSID) from BL_POS_MST where sPOSCode='" + Code + "' OR sPOSName='" + Name + "' ";
                    //
                    //strQuery = " select count(nPOSID) from BL_POS_MST where (sPOSCode='" + Code + "' OR sPOSName='" + Name + "') AND nClinicID="+_nClinicID+" ";
                    strQuery = " select count(nPOSID) from BL_POS_MST WITH (NOLOCK) where sPOSCode='" + Code.Replace("'", "''") + "'AND nClinicID=" + _ClinicID + " ";
                    //
                }
                else
                {
                    //strQuery = " select count(nPOSID) from BL_POS_MST where (sPOSCode='" + Code + "' OR sPOSName='" + Name + "') and nPOSID <> " + POSId + " ";
                    //
                    //strQuery = " select count(nPOSID) from BL_POS_MST where ((sPOSCode='" + Code + "' OR sPOSName='" + Name + "') and nPOSID <> " + POSId + ") AND nClinicID="+_nClinicID+" ";
                    strQuery = " select count(nPOSID) from BL_POS_MST WITH (NOLOCK) where ((sPOSCode='" + Code.Replace("'", "''") + "') and nPOSID <> " + POSId + ") AND nClinicID=" + _ClinicID + " ";
                    //
                }

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }

        public Boolean IsPOSCodeInUse(string sPOSCode, Int64 nPOSId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dt = null;
            Boolean _isCPTInUse = false;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@sPOSCode", sPOSCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nPOSID", nPOSId, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Connect(false);
                oDB.Retrive("Check_For_IsPOSCodeInUse", oParameters, out _dt);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    if (Convert.ToInt64(_dt.Rows[0]["POSCount"]) > 0)
                    {
                        _isCPTInUse = true;
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
            }

            return _isCPTInUse;
        }

        public bool IsExistsTOS(Int64 TOSId, string Description, string Code)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                if (TOSId == 0)
                {
                    //strQuery = " select count(nTOSID) from BL_TOS_MST where sDescription = '" + Description + "'AND nClinicID = " + _nClinicID + " ";
                    strQuery = " select count(nTOSID) from BL_TOS_MST WITH (NOLOCK) where sTOSCode = '" + Code.Replace("'", "''") + "'  AND nClinicID = " + _ClinicID + " ";
                    //
                }
                else
                {
                    //strQuery = " select count(nTOSID) from BL_TOS_MST where (sDescription='" + Description + "' and nTOSID <> " + TOSId + ") AND nClinicID=" + _nClinicID + " ";
                    strQuery = " select count(nTOSID) from BL_TOS_MST WITH (NOLOCK) where (sTOSCode='" + Code.Replace("'", "''") + "' and nTOSID <> " + TOSId + ") AND nClinicID=" + _ClinicID + " ";
                    //
                }

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }

                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }

        public bool BlockTOS(Int64 nTOSId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE BL_TOS_MST WITH (READPAST) SET bIsBlocked = '" + true + "' WHERE nTOSID = " + nTOSId;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool UnblockTOS(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE BL_TOS_MST WITH (READPAST) SET bIsBlocked = '" + false + "' WHERE nTOSID = " + ID + " ";
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool CanDeleteTOS(Int64 ID)
        {
            //Logic need to be implemented
            return false;
        }

        public bool DeleteTOS(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_TOS_MST where nTOSID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool DeleteAllTOS()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            // DataTable dt = new DataTable();
            //int _result = 0;
            //Int64 _TOSId = 0;

            try
            {
                oDB.Connect(false);
                //strQuery = " SELECT nTOSID FROM BL_TOS_MST WHERE bIsBlocked = 'true' AND nClinicID = " + _nClinicID + " ";
                //oDB.Retrive_Query(strQuery, out dt);
                //if (dt != null && dt.Rows.Count > 0)
                //{ 

                //}
                strQuery = " DELETE FROM BL_TOS_MST WHERE bIsBlocked = '" + true + "' WHERE nClinicID = " + _ClinicID + " ";
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool BlockPOS(Int64 nPOSId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE BL_POS_MST WITH (READPAST) SET bIsBlocked = '" + true + "' WHERE nPOSID = " + nPOSId;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool UnblockPOS(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE BL_POS_MST WITH (READPAST) SET bIsBlocked = '" + false + "' WHERE nPOSID = " + ID + " ";
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool CanDeletePOS(Int64 ID)
        {
            //Logic need to be implemented
            return false;
        }

        public bool DeletePOS(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_POS_MST where nPOSID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool CleanUpPOS()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " DELETE FROM BL_POS_MST WHERE bIsBlocked = '" + true + "' AND nClinicID = " + _ClinicID + " ";
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public DataTable GetBlockedTOS()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtTOS = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                //strQuery = "select nTOSID,sDescription from BL_TOS_MST where nClinicID = "+_nClinicID+" ";
                strQuery = "select nTOSID,sDescription,sTOSCode from BL_TOS_MST WITH (NOLOCK) where bIsBlocked = '" + true + "' AND nClinicID = " + _ClinicID + " ";
                oDB.Retrive_Query(strQuery, out dtTOS);
                return dtTOS;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }

            }
        }

        public DataTable GetBlockedPOS()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtPOS = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);

                //strQuery = "select nPOSID,sPOSCode,sPOSName,sPOSDescription from BL_POS_MST where nClinicID="+_nClinicID+" ";
                strQuery = "select nPOSID,sPOSCode,sPOSName,sPOSDescription from BL_POS_MST WITH (NOLOCK) where bIsBlocked='" + true + "' AND nClinicID=" + _ClinicID + " ";

                oDB.Retrive_Query(strQuery, out dtPOS);
                return dtPOS;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }


            }
        }

        #endregion " TOS & POS Methods "

    }
    #endregion

    #region "Modifer"
    public class Modifier : IDisposable
    {
        private Int64 _nModifierID = 0;
        private string _sModifierCode = "";
        private string _sDecription = "";
        private string _messageBoxCaption = "";


        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //

        #region Properties

        public Int64 ModifierID
        {
            get { return _nModifierID; }
            set { _nModifierID = value; }
        }

        public string ModifierCode
        {
            get { return _sModifierCode; }
            set { _sModifierCode = value; }
        }

        public string Decription
        {
            get { return _sDecription; }
            set { _sDecription = value; }
        }

        #endregion

        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";

        public Modifier(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
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

        ~Modifier()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// this method insert new Modifier to database   
        /// and also Update old ICD9 
        /// </summary>
        /// <returns></returns>
        public Int64 Add()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                object _intresult = 0;
                // ModifierID = 0 for inserting new ICD9
                oDBParameters.Add("@ModifierID", this.ModifierID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@ModifierCode", this.ModifierCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Description", this.Decription, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                _result = oDB.Execute("gsp_InUpModifier_MST", oDBParameters, out _intresult);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }

            }
            return _result;
        }

        public bool Block(Int64 nModifierId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE Modifier_MST WITH (READPAST) SET bIsBlocked = '" + true + "' WHERE nModifierID = " + nModifierId;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool CanDelete(Int64 ID)
        {
            //Logic need to be implemented
            return false;
        }

        public bool Delete(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from Modifier_MST where nModifierID =" + ID;
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
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool Unblock(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE Modifier_MST WITH (READPAST) SET bIsBlocked = '" + false + "' WHERE nModifierID = " + ID + " ";
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool DeleteAll()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            DataTable dt = null;
            int _result = 0;
            Int64 _modifierId = 0;

            try
            {
                oDB.Connect(false);

                //Get list of Blocked items
                strQuery = "SELECT nModifierID FROM Modifier_MST WITH (NOLOCK) WHERE bIsBlocked = 'true' AND nClinicID = " + this.ClinicID + " ";
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _modifierId = 0;
                        _modifierId = Convert.ToInt64(dt.Rows[i][0]);

                        if (CanDelete(_modifierId))
                        {
                            strQuery = " DELETE FROM Modifier_MST WHERE nModifierID = " + _modifierId;
                            _result = oDB.Execute_Query(strQuery);
                            strQuery = "";
                        }
                        else
                        {
                            MessageBox.Show("Cannot Delete :Item in use or System defined", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cannot Delete :Item in use or System defined", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }

        /// <summary>
        /// this method checks whether given modifier code is alredy present in database
        /// returns true if alredy exists
        /// </summary>
        /// <param name="ModifierId"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        internal bool CheckDuplicate(Int64 _modifierId, string _modifierCode, string _sDescription)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            string strQuery = "";
            bool _result = false;
            _modifierCode = _modifierCode.Replace("'", "''");
            _sDescription = _sDescription.Replace("'", "''");
            try
            {

                oDB.Connect(false);
                if (_modifierId == 0)
                {
                    strQuery = " select count(nModifierID) FROM Modifier_MST WITH (NOLOCK) where (sModifierCode = '" + _modifierCode + "' OR sDescription='" + _sDescription + "' ) AND nClinicID = " + this.ClinicID + " ";
                }
                else
                {
                    strQuery = " select count(nModifierID) FROM Modifier_MST WITH (NOLOCK) where ((sModifierCode = '" + _modifierCode + "' OR sDescription='" + _sDescription + "' ) AND nModifierID <> " + _modifierId + " ) AND nClinicID = " + this.ClinicID + " ";
                }

                object _intResult = null;

                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                //oDBParameters.Dispose();
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        internal DataTable GetModifier(long _modifierID)
        {
            DataTable _result = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT ISNULL(sModifierCode,'') AS sModifierCode,ISNULL(sDescription,'') AS sDescription FROM Modifier_MST WITH (NOLOCK) WHERE nModifierID = " + _modifierID;
                oDB.Retrive_Query(strQuery, out _result);
                return _result;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        internal DataTable GetModifiers()
        {
            DataTable localTable = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                //String strQuery = "SELECT nModifierID,sModifierCode,sDescription FROM Modifier_MST";
                //String strQuery = "SELECT nModifierID,sModifierCode,sDescription FROM Modifier_MST where nClinicID = " + ClinicID + "";
                String strQuery = "SELECT ISNULL(nModifierID,0) AS nModifierID,ISNULL(sModifierCode,'') AS sModifierCode,ISNULL(sDescription,'') AS sDescription FROM Modifier_MST WITH (NOLOCK) where bIsBlocked = '" + false + "' AND nClinicID = " + ClinicID + "";
                oDB.Retrive_Query(strQuery, out localTable);
                return localTable;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }//end GetModifiers

        public DataTable GetBlockedModifiers()
        {
            DataTable localTable = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                //String strQuery = "SELECT nModifierID,sModifierCode,sDescription FROM Modifier_MST";
                //String strQuery = "SELECT nModifierID,sModifierCode,sDescription FROM Modifier_MST where nClinicID = " + ClinicID + "";
                String strQuery = "SELECT ISNULL(nModifierID,0) AS nModifierID,ISNULL(sModifierCode,'') AS sModifierCode,ISNULL(sDescription,'') AS sDescription FROM Modifier_MST WITH (NOLOCK) where bIsBlocked = '" + true + "' AND nClinicID = " + ClinicID + "";
                oDB.Retrive_Query(strQuery, out localTable);
                return localTable;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }//end GetModifiers

        public string GetModifierDescription(string Code)
        {
            string _result = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt = null;
            try
            {
                oDB.Connect(false);

                //string sqlQuery = "SELECT sDescription FROM Modifier_MST WHERE sModifierCode = '" + Code + "'";
                //
                string sqlQuery = "";
                if (this._ClinicID == 0)
                    sqlQuery = "SELECT ISNULL(sDescription,'') AS sDescription FROM Modifier_MST WITH (NOLOCK) WHERE sModifierCode = '" + Code + "'";
                else
                    sqlQuery = "SELECT ISNULL(sDescription,'') AS sDescription FROM Modifier_MST WITH (NOLOCK) WHERE sModifierCode = '" + Code + "' AND nClinicID=" + this.ClinicID + " ";

                oDB.Retrive_Query(sqlQuery, out dt);
                if (dt.Rows.Count > 0)
                {
                    _result = Convert.ToString(dt.Rows[0]["sDescription"]);
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            return _result;
        }
    }
    #endregion

    #region "TOSCPT"
    public class TOSCPT
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";

        public TOSCPT(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
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

        ~TOSCPT()
        {
            Dispose(false);
        }

        #endregion

        public void Add(gloGeneralItem.gloItem oItem)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                ////First delete all existing association for the given TOS from TOS_CPT & TOS_ICD9 Tables
                //strQuery = " delete from TOS_CPT where nTOSID=" + oItem.ID;
                //oDB.Execute_Query(strQuery);
                //strQuery = " delete from TOS_ICD9 where nTOSID =" + oItem.ID;
                //oDB.Execute_Query(strQuery);
                ////


                for (int i = 0; i < oItem.SubItems.Count; i++)
                {
                    strQuery = "";
                    //Insert the new Association
                    switch (oItem.SubItems[i].ID)
                    {
                        case 1:
                            {
                                //strQuery = "INSERT INTO TOS_CPT(nTOSID,sCPTCode,sCPTDesc) VALUES(" + oItem.ID + " , '" + oItem.SubItems[i].Code.Replace("'", "''") + "', '" + oItem.SubItems[i].Description.Replace("'", "''") + "')";
                                strQuery = "INSERT INTO BL_TOS_CPT(nTOSID,sCPTCode,sCPTDesc) VALUES(" + oItem.ID + " , '" + oItem.SubItems[i].Code.Replace("'", "''") + "', '" + oItem.SubItems[i].Description.Replace("'", "''") + "')";
                                //
                                oDB.Execute_Query(strQuery);

                            }
                            break;

                        case 2:
                            {
                                //strQuery = "INSERT INTO TOS_ICD9(nTOSID,sICD9Code,sICD9Desc) VALUES(" + oItem.ID + ",'" + oItem.SubItems[i].Code.Replace("'", "''") + "','" + oItem.SubItems[i].Description.Replace("'", "''") + "')";
                                strQuery = "INSERT INTO BL_TOS_ICD9(nTOSID,sICD9Code,sICD9Desc) VALUES(" + oItem.ID + ",'" + oItem.SubItems[i].Code.Replace("'", "''") + "','" + oItem.SubItems[i].Description.Replace("'", "''") + "')";
                                //
                                oDB.Execute_Query(strQuery);
                            }
                            break;
                    }

                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool RemoveAssociation(Int64 TOSId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                //First delete all existing association for the given TOS from TOS_CPT & TOS_ICD9 Tables
                //strQuery = " delete from TOS_CPT where nTOSID=" + TOSId;
                strQuery = " delete from BL_TOS_CPT where nTOSID=" + TOSId;
                //
                oDB.Execute_Query(strQuery);
                //
                //strQuery = " delete from TOS_ICD9 where nTOSID =" + TOSId;
                strQuery = " delete from BL_TOS_ICD9 where nTOSID =" + TOSId;
                //
                oDB.Execute_Query(strQuery);
                //
                return true;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public DataTable GetAssociatedCPT(Int64 TOSid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            DataTable dt = null;
            try
            {
                oDB.Connect(false);
                //strQuery = "select nTOSID,sCPTCode,sCPTDesc from TOS_CPT where nTOSID = " + TOSid;
                strQuery = "select nTOSID,sCPTCode,sCPTDesc from BL_TOS_CPT WITH (NOLOCK) where nTOSID = " + TOSid;
                //
                oDB.Retrive_Query(strQuery, out dt);
                return dt;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }

            }
        }

        public DataTable GetAssciatedICD9(Int64 TOSid)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            DataTable dt = null;
            try
            {
                oDB.Connect(false);
                //
                //strQuery = "select nTOSID,sICD9Code,sICD9Desc from TOS_ICD9 where nTOSID = " + TOSid;
                strQuery = "select nTOSID,sICD9Code,sICD9Desc from BL_TOS_ICD9 WITH (NOLOCK) where nTOSID = " + TOSid;
                //
                oDB.Retrive_Query(strQuery, out dt);
                return dt;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }

            }
        }

        public Int64 GetCPTID(string cptCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            Object _result = null;
            try
            {
                oDB.Connect(false);
                //strQuery = "select nCPTID from CPT_MST where sCPTCode = '" + cptCode + "'";
                strQuery = "SELECT ISNULL(nCPTID,0) AS nCPTID  FROM CPT_MST WITH (NOLOCK) WHERE sCPTCode = '" + cptCode + "'";
                _result = oDB.ExecuteScalar_Query(strQuery);

                if (_result != null && Convert.ToString(_result) != "")
                    return Convert.ToInt64(_result);
                else
                    return 0;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _result = null;

            }
        }

        public Int64 GetICD9ID(string icd9Code)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            Object _result = null;
            try
            {
                oDB.Connect(false);
                strQuery = "select nICD9ID from ICD9 WITH (NOLOCK) where sICD9Code = '" + icd9Code + "' ";
                _result = oDB.ExecuteScalar_Query(strQuery);

                if (_result != null)
                    return Convert.ToInt64(_result);
                else
                    return 0;

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _result = null;

            }
        }
    }
    #endregion

    #region "CPTCharges"
    public class CPTCharges
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public CPTCharges(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~CPTCharges()
        {
            Dispose(false);
        }

        #endregion


        #region 'Public and Private Properties'

        //Public Property DataBaseConnectionString
        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = String.Empty;
        #endregion
        #region" Methods"

        public Int64 AddCPTCharges(Int64 _CPTChargesID, string _code, string _Desc, decimal _CPTCharges, decimal _Al_CPTCharges, decimal _ClinicFee, string _SPCode, string _SPDesc, string _ModCode, string _Moddesc, bool _Facility)
        {
            //@nCPTChargesID,@sCPTCode,@sCPTDesc,@nCPTCharges,@nCPTAllowedCharges,@nClinicFee,@sSpecialityCode,
            //                @sSpecialityDesc,@sModifierCode,@sModifierDesc,@bFacility
            //int _CPTChargesID = 0;
            Int64 _result = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {
                object _intresult = 0;

                oDBParameters.Add("@nCPTChargesID", _CPTChargesID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sCPTCode", _code, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sCPTDesc", _Desc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nCPTCharges", _CPTCharges, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oDBParameters.Add("@nCPTAllowedCharges", _Al_CPTCharges, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oDBParameters.Add("@nClinicFee", _ClinicFee, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Decimal);
                oDBParameters.Add("@sSpecialityCode", _SPCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sSpecialityDesc", _SPDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sModifierCode", _ModCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sModifierDesc", _Moddesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@bFacility", _Facility, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                _intresult = oDB.Execute("BL_INUP_CPTCharges", oDBParameters, out _intresult);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;

        }

        internal DataTable GetCPTCharges()
        {
            //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //Connect to database
                oDB.Connect(false);

                String strQuery = "SELECT nCPTChargesID, sCPTCode,sCPTDesc,nCPTCharges,nCPTAllowedCharges,nClinicFee,sSpecialityCode,sSpecialityDesc,sModifierCode,sModifierDesc,bFacility FROM BL_CPTCharges WITH (NOLOCK) ";

                //Get data into datatable
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

        }

        internal DataTable Getchargestoedit(Int64 _id)
        {
            //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //Connect to database
                oDB.Connect(false);

                String strQuery = "SELECT nCPTChargesID, sCPTCode,sCPTDesc,nCPTCharges,nCPTAllowedCharges,nClinicFee,sSpecialityCode,sSpecialityDesc,sModifierCode,sModifierDesc,bFacility FROM BL_CPTCharges WITH (NOLOCK) where nCPTChargesID =" + _id + " ";
                //Get data into datatable
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

        }

        public bool DeleteCPTCharges(Int64 _id)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_CPTCharges where nCPTChargesID =" + _id;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        #endregion
    }
    #endregion

    #region "Code Type"
    public class CodeType
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _CodetypeID = 0;
        private Int64 _ClinicID = 0;
        private string _CodeTypeCode = "";
        private string _CodeTypeDesc = "";


        public CodeType(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
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

        ~CodeType()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        public Int64 CodetypeID
        {
            get { return _CodetypeID; }
            set { _CodetypeID = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public string CodeTypeCode
        {
            get { return _CodeTypeCode; }
            set { _CodeTypeCode = value; }
        }

        public string CodeTypeDesc
        {
            get { return _CodeTypeDesc; }
            set { _CodeTypeDesc = value; }
        }

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = String.Empty;

        public CodeType()
        {
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
        }
        #endregion

        #region" Methods"

        public Int64 Add()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameter oDBParameter;
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                object _intresult = 0;
                oDB.Connect(false);

                // ICD9ID = 0 for inserting new ICD9
                oDBParameters.Add("@nCodeTypeID", this.CodetypeID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sCodeTypeCode", this.CodeTypeCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sCodeTypeDesc", this.CodeTypeDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nClinicID", this.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                //@nCodeTypeID, @sCodeTypeCode, @sCodeTypeDesc

                _result = oDB.Execute("gsp_BL_InUpCodeType_MST", oDBParameters, out _intresult);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

            return _result;
        }

        //Retrieving all Code Types to display
        public DataTable GetCodetypes()
        {
            //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //Connect to database
                oDB.Connect(false);


                String strQuery = "SELECT ISNULL(nCodeTypeID,0)AS CodetypeID,ISNULL(sCodeTypeCode,'')AS Code ,ISNULL(sCodeTypeDesc,'') AS CodeTypedesc FROM CodeType_MST WITH (NOLOCK) WHERE  nClinicID = " + ClinicID + "";
                //@nCodeTypeID, @sCodeTypeCode, @sCodeTypeDesc
                //Get data into datatable
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

        }

        //Retrieving Code Type to edit
        public DataTable GetcodeType(Int64 ID)
        {
            //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //Connect to database
                oDB.Connect(false);


                String strQuery = "SELECT ISNULL(sCodeTypeCode,'')AS Code ,ISNULL(sCodeTypeDesc,'') AS CodeTypedesc FROM CodeType_MST WITH (NOLOCK) WHERE  nClinicID = " + ClinicID + " AND nCodeTypeID =" + ID + " ";
                //@nCodeTypeID, @sCodeTypeCode, @sCodeTypeDesc
                //Get data into datatable
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

        }

        public bool DeleteCodetype(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from CodeType_MST where nCodeTypeID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        public bool IsExistsCodeType(Int64 CodetypeID, string code, string desc)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                if (CodetypeID == 0)
                {
                    //@nCodeTypeID, @sCodeTypeCode, @sCodeTypeDesc
                    //strQuery = "select count(nCodeTypeID) from CodeType_MST where (sCodeTypeCode = '" + code.Replace("'", "''") + "' OR sCodeTypeDesc = '" + desc.Replace("'", "''") + "') AND nClinicID=" + this.ClinicID + " ";
                    strQuery = "select count(nCodeTypeID) from CodeType_MST WITH (NOLOCK) where (sCodeTypeCode = '" + code.Replace("'", "''") + "') AND nClinicID=" + this.ClinicID + " ";
                    //
                }
                else
                {

                    strQuery = "select count(nCodeTypeID) from CodeType_MST WITH (NOLOCK) where ((sCodeTypeCode = '" + code.Replace("'", "''") + "') AND nCodeTypeID <> " + CodetypeID + ") AND nClinicID=" + this.ClinicID + " ";
                    //strQuery = "select count(nCodeTypeID) from CodeType_MST where ((sCodeTypeCode = '" + code.Replace("'", "''") + "' OR sCodeTypeDesc = '" + desc.Replace("'", "''") + "') AND nCodeTypeID <> " + CodetypeID + ") AND nClinicID=" + this.ClinicID + " ";

                }

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;

        }

        #endregion
    }
    #endregion

    #region "Flag Type"

    public class FlagType
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _flagtypeID = 0;
        private Int64 _ClinicID = 0;
        private string _flagtypeCode = "";
        private string _flagtypeDesc = "";
        private Int64 _ColorCode = 0;


        public FlagType(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
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

        ~FlagType()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        public Int64 flagtypeID
        {
            get { return _flagtypeID; }
            set { _flagtypeID = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public string flagtypeCode
        {
            get { return _flagtypeCode; }
            set { _flagtypeCode = value; }
        }

        public string flagtypeDesc
        {
            get { return _flagtypeDesc; }
            set { _flagtypeDesc = value; }
        }
        public Int64 ColorCode
        {
            get { return _ColorCode; }
            set { _ColorCode = value; }
        }

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = String.Empty;

        public FlagType()
        {
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


        }
        #endregion

        #region" Methods"

        public Int64 Add()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameter oDBParameter;
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                object _intResult = 0;
                // nflagtypeID, sflagtypeCode, sflagtypeDesc, nColorCode, nClinicID
                oDBParameters.Add("@nflagtypeID", flagtypeID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sflagtypeCode", flagtypeCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sflagtypeDesc", flagtypeDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nColorCode", ColorCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                oDB.Execute("BL_InUp_FlagType_MST", oDBParameters, out _intResult);
                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = Convert.ToInt64(_intResult);
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;

        }

        //Retrieving all Code Types to display
        public DataTable GetFlagtypes()
        {
            //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //Connect to database
                oDB.Connect(false);

                String strQuery = "SELECT ISNULL(nflagtypeID,0)AS FlagtypeID,ISNULL(sflagtypeCode,'')AS FlagtypeCode ,ISNULL(sflagtypeDesc,'') AS FlagtypeDesc,ISNULL(nColorCode,0)AS ColorCode FROM BL_FlagType_MST WITH (NOLOCK) WHERE  nClinicID = " + ClinicID + "";
                //String strQuery = "SELECT ISNULL(nflagtypeID,0)AS FlagtypeID,ISNULL(sflagtypeCode,'')AS FlagtypeCode ,ISNULL(sflagtypeDesc,'') AS FlagtypeDesc FROM BL_FlagType_MST WHERE  nClinicID = " + ClinicID + "";
                // nflagtypeID, sflagtypeCode, sflagtypeDesc, nColorCode, nClinicID
                //Get data into datatable
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

        }

        //Retrieving Code Type to edit
        public DataTable GetFlagtype(Int64 ID)
        {
            //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //Connect to database
                oDB.Connect(false);


                String strQuery = "SELECT ISNULL(sflagtypeCode,'')AS FlagtypeCode ,ISNULL(sflagtypeDesc,'') AS FlagtypeDesc,ISNULL(nColorCode,0)AS ColorCode FROM BL_FlagType_MST WITH (NOLOCK) WHERE  nClinicID = " + ClinicID + " AND nflagtypeID =" + ID + " ";
                // nflagtypeID, sflagtypeCode, sflagtypeDesc, nColorCode, nClinicID
                //Get data into datatable
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

        }

        public bool DeleteFlagtype(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_FlagType_MST where nflagtypeID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        public bool IsExistsFlagType(Int64 flagtypeID, string code, string desc)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                if (flagtypeID == 0)
                {
                    // nflagtypeID, sflagtypeCode, sflagtypeDesc, nColorCode, nClinicID

                    strQuery = "select count(nflagtypeID) from BL_FlagType_MST WITH (NOLOCK) where (sflagtypeCode = '" + code.Replace("'", "''") + "') AND nClinicID=" + ClinicID + " ";
                    //
                }
                else
                {

                    strQuery = "select count(nflagtypeID) from BL_FlagType_MST WITH (NOLOCK) where ((sflagtypeCode = '" + code.Replace("'", "''") + "') AND nflagtypeID <> " + flagtypeID + ") AND nClinicID=" + ClinicID + " ";


                }

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }

                oDB.Disconnect();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;

        }

        #endregion

    }

    #endregion

    #region "Specialty"
    public class Specialty : IDisposable
    {
        private Int64 _nSpecialtyID = 0;
        private string _sCode = "";
        private string _sDecription = "";
        private string _sTaxonomyCode = "";
        private string _sTaxonomyDesc = "";
        private string _sClassification = "";
        private Int64 _ClinicID = 0;
        private string _messageBoxCaption = String.Empty;


        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //

        #region Properties

        public Int64 SpecialtyID
        {
            get { return _nSpecialtyID; }
            set { _nSpecialtyID = value; }
        }

        public string SpecialtyCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        public string Decription
        {
            get { return _sDecription; }
            set { _sDecription = value; }
        }

        public string TaxonomyCode
        {
            get { return _sTaxonomyCode; }
            set { _sTaxonomyCode = value; }
        }

        public string TaxonomyDesc
        {
            get { return _sTaxonomyDesc; }
            set { _sTaxonomyDesc = value; }
        }

        public string Classification
        {
            get { return _sClassification; }
            set { _sClassification = value; }
        }
        #endregion

        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";

        public Specialty(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~Specialty()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// To Modify Specialty to database   
        /// </summary>
        /// <returns></returns>
        public Int64 Modify(Int64 SpecialtyID)
        {
            Int64 _result = 0;
            String strSQL = "";
            _nSpecialtyID = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                strSQL = "UPDATE Specialty_MST WITH (READPAST) SET "
                + " sCode = '" + _sCode.Replace("'", "''") + "', sDescription = '" + _sDecription.Replace("'", "''") + "', sTaxonomyCode =  '" + _sTaxonomyCode.Replace("'", "''") + "' , sTaxonomyDesc =  '" + _sTaxonomyDesc.Replace("'", "''") + "' , "
                + " sTaxonomyClassification =  '" + _sClassification.Replace("'", "''") + "' , nClinicID = " + _ClinicID + " , bIsBlocked = 0"
                + " WHERE nSpecialtyID = " + SpecialtyID;

                _result = oDB.Execute_Query(strSQL);

                if (_result > 0)
                    return SpecialtyID;
                else
                    return 0;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        /// <summary>
        /// To Insert new Specialty to database   
        /// </summary>
        /// <returns> New SpecialtyID </returns>
        public Int64 Add()
        {
            Int64 _result = 0;
            //String strSQL = "";
            String strSql = "";
            _nSpecialtyID = 0;
            Int64 _replicationId = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                //**..Code chages made on 20090520 By - Sagar Ghodke
                //**..Changes made to implement Replication ID

                //strSql = "Select ISNULL(MAX(nSpecialtyID),0) +1 FROM Specialty_MST ";
                //_nSpecialtyID = Convert.ToInt64(oDB.ExecuteScalar_Query(strSql));

                _replicationId = oDB.GetPrefixTransactionID(0);

                strSql = " IF EXISTS(SELECT nSpecialtyID FROM Specialty_MST WITH (NOLOCK) WHERE convert(varchar(18),nSpecialtyID) Like convert(varchar(18)," + _replicationId + ")+ '%') " +
                " SELECT  isnull(max(nSpecialtyID),0)+1 FROM Specialty_MST WITH (NOLOCK) where convert(varchar(18),nSpecialtyID) Like convert(varchar(18)," + _replicationId + ")+ '%' " +
                " ELSE " +
                " SELECT convert(numeric(18,0), convert(varchar(18)," + _replicationId + ") + '001')  ";

                _nSpecialtyID = Convert.ToInt64(oDB.ExecuteScalar_Query(strSql));

                //**..End Code changes 20090520

                strSql = "INSERT INTO Specialty_MST (nSpecialtyID, sCode, sDescription, sTaxonomyCode, sTaxonomyDesc, sTaxonomyClassification, nClinicID, bIsBlocked) " +
                       " VALUES (" + _nSpecialtyID + ",'" + _sCode.Replace("'", "''") + "','" + _sDecription.Replace("'", "''") + "','" + _sTaxonomyCode.Replace("'", "''") + "','" + _sTaxonomyDesc.Replace("'", "''") + "','" + _sClassification.Replace("'", "''") + "'," + _ClinicID + ",0)";

                //_result = oDB.Execute_Query(strSQL);
                _result = oDB.Execute_Query(strSql);

                if (_result > 0)
                    return _nSpecialtyID;
                else
                    return 0;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            //return _result;
            //return _result;
        }

        public bool Block(Int64 nSpecialtyId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE Specialty_MST WITH (READPAST) SET bIsBlocked = '" + true + "' WHERE nSpecialtyID = " + nSpecialtyId;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool CanDelete(Int64 ID)
        {
            //Logic need to be implemented
            return false;
        }

        public bool Delete(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from Specialty_MST where nSpecialtyID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool Unblock(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE Specialty_MST WITH (READPAST) SET bIsBlocked = '" + false + "' WHERE nSpecialtyID = " + ID + " ";
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        /// <summary>
        /// Delete All Blocked Specialties
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            DataTable dt = null;
            int _result = 0;
            Int64 _SpecialtyId = 0;

            try
            {
                oDB.Connect(false);

                //Get list of Blocked items
                strQuery = "SELECT nSpecialtyID FROM Specialty_MST WITH (NOLOCK) WHERE bIsBlocked = 'true' AND nClinicID = " + this.ClinicID + " ";
                oDB.Retrive_Query(strQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _SpecialtyId = 0;
                        _SpecialtyId = Convert.ToInt64(dt.Rows[i][0]);

                        if (CanDelete(_SpecialtyId))
                        {
                            strQuery = " DELETE FROM Specialty_MST WHERE nSpecialtyID = " + _SpecialtyId;
                            _result = oDB.Execute_Query(strQuery);
                            strQuery = "";
                        }
                        else
                        {
                            MessageBox.Show("Cannot Delete :Item in use or System defined", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cannot Delete :Item in use or System defined", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }

        /// <summary>
        /// this method checks whether given Specialty code is alredy present in database
        /// returns true if alredy exists
        /// </summary>
        /// <param name="SpecialtyId"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        internal bool CheckDuplicate(Int64 _SpecialtyId, string _SpecialtyCode, string _sDescription)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            string strQuery = "";
            bool _result = false;
            try
            {

                oDB.Connect(false);
                if (_SpecialtyId == 0)
                {
                    // strQuery = " select count(nSpecialtyID) FROM Specialty_MST where (sSpecialtyCode = '" + _SpecialtyCode + "' OR sDescription='" + _sDescription + "' ) AND nClinicID = " + this.ClinicID + " ";
                    strQuery = " select count(nSpecialtyID) FROM Specialty_MST WITH (NOLOCK) where sCode = '" + _SpecialtyCode.Replace("'", "''") + "' AND nClinicID = " + this.ClinicID + " ";
                }
                else
                {
                    // strQuery = " select count(nSpecialtyID) FROM Specialty_MST where ((sSpecialtyCode = '" + _SpecialtyCode + "' OR sDescription='" + _sDescription + "' ) AND nSpecialtyID <> " + _SpecialtyId + " ) AND nClinicID = " + this.ClinicID + " ";
                    strQuery = " select count(nSpecialtyID) FROM Specialty_MST WITH (NOLOCK) where sCode = '" + _SpecialtyCode.Replace("'", "''") + "' AND nSpecialtyID <> " + _SpecialtyId + "  AND nClinicID = " + this.ClinicID + " ";
                }

                object _intResult = null;

                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        /// <summary>
        /// Get All the Details of Selected Specialty
        /// </summary>
        /// <param name="_SpecialtyID"></param>
        /// <returns></returns>
        internal DataTable GetSpecialty(long _SpecialtyID)
        {
            DataTable _result = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT ISNULL(sCode,'') AS sCode, ISNULL(sDescription,'') AS sDescription, ISNULL(sTaxonomyCode,'') AS sTaxonomyCode, ISNULL(sTaxonomyDesc,'') AS sTaxonomyDesc, ISNULL(sTaxonomyClassification,'') AS sTaxonomyClassification, ISNULL(nClinicID,0) AS nClinicID, ISNULL(bIsBlocked,'false') AS bIsBlocked "
                                + " FROM Specialty_MST WITH (NOLOCK) WHERE nSpecialtyID = " + _SpecialtyID + "  AND nClinicID = " + ClinicID;
                oDB.Retrive_Query(strQuery, out _result);
                return _result;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        /// <summary>
        ///  Get All unblocked the Specialties of the Clinic
        /// </summary>
        /// <returns></returns>
        internal DataTable GetSpecialties()
        {
            DataTable localTable = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                //String strQuery = "SELECT nSpecialtyID,sSpecialtyCode,sDescription FROM Specialty_MST";
                //String strQuery = "SELECT nSpecialtyID,sSpecialtyCode,sDescription FROM Specialty_MST where nClinicID = " + ClinicID + "";
                String strQuery = "SELECT nSpecialtyID,ISNULL(sCode,'') AS sCode, ISNULL(sDescription,'') AS sDescription, ISNULL(sTaxonomyCode,'') AS sTaxonomyCode, ISNULL(sTaxonomyDesc,'') AS sTaxonomyDesc, ISNULL(sTaxonomyClassification,'') AS sTaxonomyClassification, ISNULL(nClinicID,0) AS nClinicID, ISNULL(bIsBlocked,'false') AS bIsBlocked FROM Specialty_MST WITH (NOLOCK) where nClinicID = " + ClinicID + "";
                oDB.Retrive_Query(strQuery, out localTable);
                return localTable;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }//end GetSpecialties

        /// <summary>
        /// To Get All the Blocked Specialties of the Selected Clinic
        /// </summary>
        /// <returns></returns>
        public DataTable GetBlockedSpecialties()
        {
            DataTable localTable = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                //String strQuery = "SELECT nSpecialtyID,sSpecialtyCode,sDescription FROM Specialty_MST";
                //String strQuery = "SELECT nSpecialtyID,sSpecialtyCode,sDescription FROM Specialty_MST where nClinicID = " + ClinicID + "";
                String strQuery = "SELECT nSpecialtyID,ISNULL(sCode,'') AS sCode, ISNULL(sDescription,'') AS sDescription, ISNULL(sTaxonomy,'') AS sTaxonomy  FROM Specialty_MST WITH (NOLOCK) where bIsBlocked = '" + true + "' AND nClinicID = " + ClinicID + "";
                oDB.Retrive_Query(strQuery, out localTable);
                return localTable;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }//end GetSpecialties

    }
    #endregion

    #region "Insurance Service Type "

    public class ServiceType
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ServiceTypeID = 0;
        private Int64 _ClinicID = 0;
        private string _ServiceTypeCode = "";
        private string _ServiceTypeDesc = "";
        private string _InsuranceType = "";
        //nServiceTypeID, sServiceTypeCode, sServiceTypeDesc, sInsuranceType, nClinicID

        public ServiceType(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~ServiceType()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        public Int64 ServiceTypeID
        {
            get { return _ServiceTypeID; }
            set { _ServiceTypeID = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public string ServiceTypeCode
        {
            get { return _ServiceTypeCode; }
            set { _ServiceTypeCode = value; }
        }

        public string ServiceTypeDesc
        {
            get { return _ServiceTypeDesc; }
            set { _ServiceTypeDesc = value; }
        }
        public string InsuranceType
        {
            get { return _InsuranceType; }
            set { _InsuranceType = value; }
        }

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = String.Empty;

        #endregion

        #region" Methods"

        public Int64 Add()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameter oDBParameter;
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                object _intresult = 0;
                // nServiceTypeID, sServiceTypeCode, sServiceTypeDesc, sInsuranceType, nClinicID  --BL_InsuranceServiceType
                oDBParameters.Add("@nServiceTypeID", ServiceTypeID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sServiceTypeCode", ServiceTypeCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sServiceTypeDesc", ServiceTypeDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sInsuranceType", InsuranceType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);


                oDB.Execute("BL_InUp_InsuranceServiceType", oDBParameters, out _intresult);
                if (_intresult != null)
                {
                    if (Convert.ToInt64(_intresult) > 0)
                    {
                        _result = Convert.ToInt64(_intresult);
                    }
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

            return _result;
        }

        //Retrieving all Code Types to display
        public DataTable GetInsuranceServiceTypes()
        {
            //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //Connect to database
                oDB.Connect(false);
                //nServiceTypeID, sServiceTypeCode, sServiceTypeDesc, sInsuranceType, nClinicID  --BL_InsuranceServiceType
                String strQuery = "SELECT ISNULL(nServiceTypeID,0)AS ServiceTypeID,ISNULL(sServiceTypeCode,'')AS ServiceTypeCode ,ISNULL(sServiceTypeDesc,'') AS ServiceTypeDesc,ISNULL(sInsuranceType,'')AS InsuranceType FROM BL_InsuranceServiceType WITH (NOLOCK) WHERE  nClinicID = " + ClinicID + "";

                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

        }

        //Retrieving Code Type to edit
        public DataTable GetInsuranceServiceType(Int64 ID)
        {
            //Declare the class(gloDatabaseLayer.DBLayer) object to retrieve data from database
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //Connect to database
                oDB.Connect(false);


                String strQuery = "SELECT ISNULL(sServiceTypeCode,'')AS ServiceTypeCode ,ISNULL(sServiceTypeDesc,'') AS ServiceTypeDesc,ISNULL(sInsuranceType,'')AS InsuranceType FROM BL_InsuranceServiceType WITH (NOLOCK) WHERE  nClinicID = " + ClinicID + " AND nServiceTypeID =" + ID + " ";

                //Get data into datatable
                DataTable dt = null;
                oDB.Retrive_Query(strQuery, out dt);

                //Disconnect from database
                oDB.Disconnect();
                return dt;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

        }

        public bool DeleteInsuranceServiceType(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_InsuranceServiceType where nServiceTypeID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        public bool IsExistsServicetype(Int64 ServicetypeID, string Servicetypecode, string Servicetypedesc)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                if (ServicetypeID == 0)
                {

                    strQuery = "select count(nServiceTypeID) from BL_InsuranceServiceType WITH (NOLOCK) where (sServiceTypeCode = '" + Servicetypecode.Replace("'", "''") + "') AND nClinicID=" + ClinicID + " ";
                    //
                }
                else
                {

                    strQuery = "select count(nServiceTypeID) from BL_InsuranceServiceType WITH (NOLOCK) where ((sServiceTypeCode = '" + Servicetypecode.Replace("'", "''") + "') AND nServiceTypeID <> " + ServicetypeID + ") AND nClinicID=" + ClinicID + " ";


                }
                //nServiceTypeID, sServiceTypeCode, sServiceTypeDesc, sInsuranceType, nClinicID  --BL_InsuranceServiceType
                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;

        }

        #endregion

    }

    #endregion

    #region "Insurance Plan"

    public class InsurancePlan : IDisposable
    {
        #region " Private Variables "

        private Int64 _nInsurancePlanID = 0;
        private string _sPlanCode = "";
        private string _sPlanDecription = "";
        private Int64 _ClinicID = 0;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion " Private Variables "
        //

        #region Properties

        public Int64 InsurancePlanID
        {
            get { return _nInsurancePlanID; }
            set { _nInsurancePlanID = value; }
        }

        public string InsurancePlanCode
        {
            get { return _sPlanCode; }
            set { _sPlanCode = value; }
        }

        public string Decription
        {
            get { return _sPlanDecription; }
            set { _sPlanDecription = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion

        #region "Constructor & Destructor"

        private string _databaseconnectionstring = "";

        public InsurancePlan(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~InsurancePlan()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// To Modify InsurancePlan to database   
        /// </summary>
        /// <returns></returns>
        public Int64 Modify(Int64 InsurancePlanID)
        {
            Int64 _result = 0;
            String strSQL = "";
            _nInsurancePlanID = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                strSQL = "UPDATE BL_InsurancePlanCodes_MST  WITH (READPAST) SET "
                + " sPlanCode = '" + _sPlanCode.Replace("'", "''").Trim() + "', sPlanDescription = '" + _sPlanDecription.Replace("'", "''").Trim() + "', "
                + " nClinicID = " + _ClinicID + " , bIsblock = 0"
                + " WHERE nPlanID = " + InsurancePlanID;

                _result = oDB.Execute_Query(strSQL);

                if (_result > 0)
                    return InsurancePlanID;
                else
                    return 0;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        /// <summary>
        /// To Insert new InsurancePlan to database   
        /// </summary>
        /// <returns> New InsurancePlanID </returns>
        public Int64 Add()
        {
            Int64 _result = 0;
            String strSql = "";
            Int64 _replicationId = 0;
            _nInsurancePlanID = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                //..**Code changes made on 20090520 By - Sagar Ghodke
                //..**Code changes made to implement replication id
                //..Below commented code is previous code

                //strSql = "Select ISNULL(MAX(nPlanID),0) +1 FROM BL_InsurancePlanCodes_MST ";
                //_nInsurancePlanID = Convert.ToInt64(oDB.ExecuteScalar_Query(strSql));

                _replicationId = oDB.GetPrefixTransactionID(0);

                strSql = " IF EXISTS(SELECT nPlanID FROM BL_InsurancePlanCodes_MST WITH (NOLOCK) WHERE convert(varchar(18),nPlanID) Like convert(varchar(18)," + _replicationId + ")+ '%') " +
              " SELECT  isnull(max(nPlanID),0)+1 FROM BL_InsurancePlanCodes_MST WITH (NOLOCK) where convert(varchar(18),nPlanID) Like convert(varchar(18)," + _replicationId + ")+ '%' " +
              " ELSE " +
              " SELECT convert(numeric(18,0), convert(varchar(18)," + _replicationId + ") + '001')  ";

                _nInsurancePlanID = Convert.ToInt64(oDB.ExecuteScalar_Query(strSql));


                //..**End code changes 20090520,Sagar Ghodke


                strSql = "INSERT INTO BL_InsurancePlanCodes_MST (nPlanID, sPlanCode, sPlanDescription, nClinicID, bIsblock) " +
                       " VALUES (" + _nInsurancePlanID + ",'" + _sPlanCode.Replace("'", "''") + "','" + _sPlanDecription.Replace("'", "''") + "'," + _ClinicID + ",0)";

                _result = oDB.Execute_Query(strSql);

                if (_result > 0)
                    return _nInsurancePlanID;
                else
                    return 0;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            //return _result;
        }

        public bool Block(Int64 nInsurancePlanId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE BL_InsurancePlanCodes_MST WITH (READPAST) SET bIsBlocked = '" + true + "' WHERE nPlanID = " + nInsurancePlanId;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool Delete(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "Delete from BL_InsurancePlanCodes_MST where nPlanID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool Unblock(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE BL_InsurancePlanCodes_MST WITH (READPAST) SET bIsBlocked = '" + false + "' WHERE nPlanID = " + ID + " ";
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }


        /// <summary>
        /// this method checks whether given InsurancePlan code is already present in database
        /// returns true if alredy exists
        /// </summary>
        /// <param name="InsurancePlanId"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        internal bool CheckDuplicate(Int64 _InsurancePlanId, string _InsurancePlanCode, string _InsuranceDescription)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            string strQuery = "";
            bool _result = false;
            try
            {

                oDB.Connect(false);
                if (_InsurancePlanId == 0)
                {
                    // strQuery = " select count(nInsurancePlanID) FROM InsurancePlan_MST where (sInsurancePlanCode = '" + _InsurancePlanCode + "' OR sDescription='" + _sDescription + "' ) AND nClinicID = " + this.ClinicID + " ";
                    strQuery = " select count(nPlanID) FROM BL_InsurancePlanCodes_MST WITH (NOLOCK) where sPlanCode = '" + _InsurancePlanCode.Replace("'", "''") + "' AND nClinicID = " + this.ClinicID + " ";
                }
                else
                {
                    // strQuery = " select count(nInsurancePlanID) FROM InsurancePlan_MST where ((sInsurancePlanCode = '" + _InsurancePlanCode + "' OR sDescription='" + _sDescription + "' ) AND nInsurancePlanID <> " + _InsurancePlanId + " ) AND nClinicID = " + this.ClinicID + " ";
                    strQuery = " select count(nPlanID) FROM BL_InsurancePlanCodes_MST WITH (NOLOCK) where sPlanCode = '" + _InsurancePlanCode.Replace("'", "''") + "' AND nPlanID <> " + _InsurancePlanId + "  AND nClinicID = " + this.ClinicID + " ";
                }

                object _intResult = null;

                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        /// <summary>
        /// Get All the Details of Selected InsurancePlan
        /// </summary>
        /// <param name="_InsurancePlanID"></param>
        /// <returns></returns>
        internal DataTable GetInsurancePlan(long _InsurancePlanID)
        {
            DataTable _result = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT ISNULL(sPlanCode,'') AS sPlanCode, ISNULL(sPlanDescription,'') AS sPlanDescription, ISNULL(nClinicID,0) AS nClinicID, ISNULL(bIsBlock,'false') AS bIsBlock "
                                + " FROM BL_InsurancePlanCodes_MST WITH (NOLOCK) WHERE nPlanID = " + _InsurancePlanID + "  AND nClinicID = " + ClinicID;
                oDB.Retrive_Query(strQuery, out _result);
                return _result;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        /// <summary>
        ///  Get All unblocked the Insurance Plans of the Clinic
        /// </summary>
        /// <returns></returns>
        internal DataTable GetInsurancePlans()
        {
            DataTable localTable = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT nPlanID,ISNULL(sPlanCode,'') AS sPlanCode, ISNULL(sPlanDescription,'') AS sPlanDescription FROM BL_InsurancePlanCodes_MST WITH (NOLOCK) where nClinicID = " + ClinicID + "";
                oDB.Retrive_Query(strQuery, out localTable);
                return localTable;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }


    }

    #endregion

    #region "Insurance Type"

    public class InsuranceType : IDisposable
    {
        #region " Private Variables "

        private Int64 _nInsuranceTypeID = 0;
        private string _sTypeCode = "";
        private string _sTypeDecription = "";
        private Int64 _ClinicID = 0;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion " Private Variables "
        //

        #region Properties

        public Int64 InsuranceTypeID
        {
            get { return _nInsuranceTypeID; }
            set { _nInsuranceTypeID = value; }
        }

        public string InsuranceTypeCode
        {
            get { return _sTypeCode; }
            set { _sTypeCode = value; }
        }

        public string InduranceTypeDesc
        {
            get { return _sTypeDecription; }
            set { _sTypeDecription = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion

        #region "Constructor & Destructor"

        private string _databaseconnectionstring = "";

        public InsuranceType(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~InsuranceType()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// To Modify InsurancePlan to database   
        /// </summary>
        /// <returns></returns>
        public Int64 Modify(Int64 InsuranceTypeID)
        {
            Int64 _result = 0;
            String strSQL = "";
            _nInsuranceTypeID = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                //nInsuranceTypeID, sInsuranceTypeDesc, sInsuranceTypeCode, nClinicID, bIsSystem
                strSQL = "UPDATE InsuranceType_MST  WITH (READPAST) SET "
                + " sInsuranceTypeCode = '" + _sTypeCode.Replace("'", "''").Trim() + "', sInsuranceTypeDesc = '" + _sTypeDecription.Replace("'", "''").Trim() + "', "
                + " nClinicID = " + _ClinicID + " , bIsSystem = 0"
                + " WHERE nInsuranceTypeID = " + InsuranceTypeID;

                _result = oDB.Execute_Query(strSQL);

                if (_result > 0)
                    return InsuranceTypeID;
                else
                    return 0;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        /// <summary>
        /// To Insert new InsurancePlan to database   
        /// </summary>
        /// <returns> New InsurancePlanID </returns>
        public Int64 Add()
        {
            Int64 _result = 0;
            String strSql = "";
            _nInsuranceTypeID = 0;
            Int64 _replicationId = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                //nInsuranceTypeID, sInsuranceTypeDesc, sInsuranceTypeCode, nClinicID, bIsSystem

                //..**Code changes made on 20090520 By - Sagar Ghodke
                //..**Code changes made to implement replication id
                //..Below commented code is previous code

                //strSql = "Select ISNULL(MAX(nInsuranceTypeID),0) +1 FROM InsuranceType_MST ";
                //_nInsuranceTypeID = Convert.ToInt64(oDB.ExecuteScalar_Query(strSql));

                _replicationId = oDB.GetPrefixTransactionID(0);

                strSql = " IF EXISTS(SELECT nInsuranceTypeID FROM InsuranceType_MST WITH (NOLOCK) WHERE convert(varchar(18),nInsuranceTypeID) Like convert(varchar(18)," + _replicationId + ")+ '%') " +
               " SELECT  isnull(max(nInsuranceTypeID),0)+1 FROM InsuranceType_MST WITH (NOLOCK) where convert(varchar(18),nInsuranceTypeID) Like convert(varchar(18)," + _replicationId + ")+ '%' " +
               " ELSE " +
               " SELECT convert(numeric(18,0), convert(varchar(18)," + _replicationId + ") + '001')  ";

                _nInsuranceTypeID = Convert.ToInt64(oDB.ExecuteScalar_Query(strSql));

                //..**End code changes made on 20090520,Sagar Ghodke

                strSql = "INSERT INTO InsuranceType_MST (nInsuranceTypeID, sInsuranceTypeDesc, sInsuranceTypeCode, nClinicID, bIsSystem) " +
                       " VALUES (" + _nInsuranceTypeID + ",'" + _sTypeDecription.Replace("'", "''") + "','" + _sTypeCode.Replace("'", "''") + "'," + _ClinicID + ",0)";

                _result = oDB.Execute_Query(strSql);

                if (_result > 0)
                    return _nInsuranceTypeID;
                else
                    return 0;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            //return _result;
        }


        public bool Delete(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "Delete from InsuranceType_MST where nInsuranceTypeID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }



        /// <summary>
        /// this method checks whether given InsurancePlan code is already present in database
        /// returns true if alredy exists
        /// </summary>
        /// <param name="InsurancePlanId"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        internal bool CheckDuplicate(Int64 _InsuranceTypeId, string _InsuranceTypeCode, string _InsuranceTypeDescription)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            string strQuery = "";
            bool _result = false;
            try
            {

                oDB.Connect(false);
                if (_InsuranceTypeId == 0)
                {//nInsuranceTypeID, sInsuranceTypeDesc, sInsuranceTypeCode, nClinicID, bIsSystem

                    strQuery = " select count(nInsuranceTypeID) FROM InsuranceType_MST WITH (NOLOCK) where sInsuranceTypeCode = '" + _InsuranceTypeCode.Replace("'", "''") + "' AND nClinicID = " + this.ClinicID + " ";
                }
                else
                {
                    strQuery = " select count(nInsuranceTypeID) FROM InsuranceType_MST WITH (NOLOCK) where sInsuranceTypeCode = '" + _InsuranceTypeCode.Replace("'", "''") + "' AND nInsuranceTypeID <> " + _InsuranceTypeId + "  AND nClinicID = " + this.ClinicID + " ";
                }

                object _intResult = null;

                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        /// <summary>
        /// Get All the Details of Selected InsurancePlan
        /// </summary>
        /// <param name="_InsurancePlanID"></param>
        /// <returns></returns>
        internal DataTable GetInsuranceType(long _InsuranceTypeID)
        {
            DataTable _result = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                //nInsuranceTypeID, sInsuranceTypeDesc, sInsuranceTypeCode, nClinicID, bIsSystem
                oDB.Connect(false);
                String strQuery = "SELECT ISNULL(sInsuranceTypeCode,'') AS sTypeCode, ISNULL(sInsuranceTypeDesc,'') AS sTypeDesc, ISNULL(nClinicID,0) AS nClinicID, ISNULL(bIsSystem,'false') AS bIsSystem "
                                + " FROM InsuranceType_MST WITH (NOLOCK) WHERE nInsuranceTypeID = " + _InsuranceTypeID + "  AND nClinicID = " + ClinicID;
                oDB.Retrive_Query(strQuery, out _result);
                return _result;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        /// <summary>
        ///  Get All unblocked the Insurance Plans of the Clinic
        /// </summary>
        /// <returns></returns>
        internal DataTable GetInsuranceTypes()
        {
            DataTable localTable = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT nInsuranceTypeID,ISNULL(sInsuranceTypeCode,'') AS sTypeCode, ISNULL(sInsuranceTypeDesc,'') AS sTypeDesc FROM InsuranceType_MST WITH (NOLOCK) where nClinicID = " + ClinicID + "";
                oDB.Retrive_Query(strQuery, out localTable);
                return localTable;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }


    }

    #endregion

    #region "Clearing House"

    public enum TypeOfData
    {
        None = 0,
        TestData = 1,
        ProductionData = 2,
        Blank = 3
    }

    public class ClearingHouse : IDisposable
    {
        #region " Private Variables "
        private string _messageBoxCaption = String.Empty;
        private Int64 _nClearingHouseID = 0;
        private string _sClearingHouseName = "";
        private string _sRecieverName = "";
        private string _sRecieverID = "";
        private string _sSubmitterID = "";
        private bool _bIs1JQualifier = false;
        private string _s1JQualifier = "";
        private bool _bIsSenderCode = false;
        private string _sSenderCode = "";
        private bool _bIsVenderID = false;
        private string _sVenderID = "";
        private bool _bIsLoop100B = false;
        private string _sLoop100B = "";
        private TypeOfData _TypeOfData = TypeOfData.None;
        private bool _bIsISA = false;
        private Int64 _ClinicID = 0;

        //Fields to be added to detail table
        #region "Variables for detail table fields
        private string _sClearingHouseCode = "";
        private string _sURL = "";
        private string _sUserName = "";
        private string _sPassword = "";
        private string _sIn_271_ElgibilityResponse = "";
        private string _sIn_277_ClaimStatus = "";
        private string _sIn_835_Remitance = "";
        private string _sIn_997_Acknowledge = "";
        private string _sOut_276_ElgibilityEnquiry = "";
        private string _sOut_837P_ClaimSubmition = "";
        private string _sOut_997_Acknowledge = "";
        private string _sGen_CSRReports = "";
        private string _sGen_Letters = "";
        private string _sGen_Reports = "";
        private string _sGen_Statements = "";
        private string _sGen_WorkedTrans = "";
        private Int64 _nFolderCategory = 0;
        #endregion

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion " Private Variables "

        #region Properties

        public Int64 ClearingHouseID
        {
            get { return _nClearingHouseID; }
            set { _nClearingHouseID = value; }
        }

        public string ClearingHouseName
        {
            get { return _sClearingHouseName; }
            set { _sClearingHouseName = value; }
        }

        public string RecieverName
        {
            get { return _sRecieverName; }
            set { _sRecieverName = value; }
        }

        public string RecieverID
        {
            get { return _sRecieverID; }
            set { _sRecieverID = value; }
        }

        public string SubmitterID
        {
            get { return _sSubmitterID; }
            set { _sSubmitterID = value; }
        }

        public bool IsOneJQualifier
        {
            get { return _bIs1JQualifier; }
            set { _bIs1JQualifier = value; }
        }

        public string OneJQualifier
        {
            get { return _s1JQualifier; }
            set { _s1JQualifier = value; }
        }

        public bool IsSenderCode
        {
            get { return _bIsSenderCode; }
            set { _bIsSenderCode = value; }
        }

        public string SenderCode
        {
            get { return _sSenderCode; }
            set { _sSenderCode = value; }
        }

        public bool IsVenderID
        {
            get { return _bIsVenderID; }
            set { _bIsVenderID = value; }
        }

        public string VenderID
        {
            get { return _sVenderID; }
            set { _sVenderID = value; }
        }

        public bool IsLoop1000B
        {
            get { return _bIsLoop100B; }
            set { _bIsLoop100B = value; }
        }

        public string Loop1000B
        {
            get { return _sLoop100B; }
            set { _sLoop100B = value; }
        }

        public TypeOfData TypeOfData
        {
            get { return _TypeOfData; }
            set { _TypeOfData = value; }
        }

        public bool IsISA
        {
            get { return _bIsISA; }
            set { _bIsISA = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion


        #region Properties for deteil table fields
        public string ClearingHouseCode
        {
            get { return _sClearingHouseCode; }
            set { _sClearingHouseCode = value; }
        }
        public string URL
        {

            get { return _sURL; }
            set { _sURL = value; }

        }
        public string UserName
        {
            get { return _sUserName; }
            set { _sUserName = value; }
        }

        public string Password
        {
            get { return _sPassword; }
            set { _sPassword = value; }
        }
        public string In_271_ElgibilityResponse
        {
            get { return _sIn_271_ElgibilityResponse; }
            set { _sIn_271_ElgibilityResponse = value; }
        }
        public string In_277_ClaimStatus
        {
            get { return _sIn_277_ClaimStatus; }
            set { _sIn_277_ClaimStatus = value; }
        }
        public string In_835_Remitance
        {
            get { return _sIn_835_Remitance; }
            set { _sIn_835_Remitance = value; }
        }
        public string In_997_Acknowledge
        {
            get { return _sIn_997_Acknowledge; }
            set { _sIn_997_Acknowledge = value; }
        }
        public string Out_276_ElgibilityEnquiry
        {
            get { return _sOut_276_ElgibilityEnquiry; }
            set { _sOut_276_ElgibilityEnquiry = value; }
        }
        public string Out_837P_ClaimSubmition
        {
            get { return _sOut_837P_ClaimSubmition; }
            set { _sOut_837P_ClaimSubmition = value; }
        }
        public string Out_997_Acknowledge
        {
            get { return _sOut_997_Acknowledge; }
            set { _sOut_997_Acknowledge = value; }
        }
        public string Gen_CSRReports
        {
            get { return _sGen_CSRReports; }
            set { _sGen_CSRReports = value; }
        }
        public string Gen_Letters
        {
            get { return _sGen_Letters; }
            set { _sGen_Letters = value; }
        }
        public string Gen_Reports
        {
            get { return _sGen_Reports; }
            set { _sGen_Reports = value; }
        }
        public string Gen_Statements
        {
            get { return _sGen_Statements; }
            set { _sGen_Statements = value; }
        }
        public string Gen_WorkedTrans
        {
            get { return _sGen_WorkedTrans; }
            set { _sGen_WorkedTrans = value; }
        }
        public Int64 FolderCategory
        {
            get { return _nFolderCategory; }
            set { _nFolderCategory = value; }
        }

        #endregion

        #region "Constructor & Destructor"

        private string _databaseconnectionstring = "";

        public ClearingHouse(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~ClearingHouse()
        {
            Dispose(false);
        }

        #endregion

        public long Add(ClearingHouse oClearingHouse)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object Value = null;
            Int64 _ReturnID = 0;
            try
            {

                oDB.Connect(false);

                oDBParameters.Add("@nClearingHouseID", oClearingHouse.ClearingHouseID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sClearingHouseCode", oClearingHouse.ClearingHouseName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sReceiverID", oClearingHouse.RecieverID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sReceiverName", oClearingHouse.RecieverName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sSubmitterID", oClearingHouse.SubmitterID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@bIsOneJQulifier", oClearingHouse.IsOneJQualifier, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@sOneJQulifier", oClearingHouse.OneJQualifier, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@bIsSenderCode", oClearingHouse.IsSenderCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@sSenderCode", oClearingHouse.SenderCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@bIsVenderIDCode", oClearingHouse.IsVenderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@sVenderIDCode", oClearingHouse.VenderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@bIsLoop1000BNM109", oClearingHouse.IsLoop1000B, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@sLoop1000BNM109", oClearingHouse.Loop1000B, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nTypeOfData", oClearingHouse.TypeOfData.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@bIsISA", oClearingHouse.IsISA, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@nClinicID", oClearingHouse.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("BL_INUP_ClearingHouseMST", oDBParameters, out Value);
                if (Value != null && Convert.ToString(Value) != "")
                {
                    _ReturnID = Convert.ToInt64(Value);
                }

                #region Adding To Detail Table
                //Delete entries from detail table
                oDBParameters.Clear();
                string _sqlQuery = "";
                _sqlQuery = "DELETE FROM BL_ClearingHouse_DTL "
                         + " WHERE nClearingHouseID = " + _ReturnID + " AND nClinicID = " + ClinicID + " ";
                oDB.Execute_Query(_sqlQuery);

                //Insert entries into detail table

                oDBParameters.Add("@nClearingHouseID", _ReturnID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sClearingHouseCode", oClearingHouse.ClearingHouseCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sURL", oClearingHouse.URL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sUserName", oClearingHouse.UserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sPassword", oClearingHouse.Password, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sIn_271_ElgibilityResponse", oClearingHouse.In_271_ElgibilityResponse, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sIn_277_ClaimStatus", oClearingHouse.In_277_ClaimStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sIn_835_Remitance", oClearingHouse.In_835_Remitance, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sIn_997_Acknowledge", oClearingHouse.In_997_Acknowledge, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sOut_276_ElgibilityEnquiry", oClearingHouse.Out_276_ElgibilityEnquiry, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sOut_837P_ClaimSubmition", oClearingHouse.Out_837P_ClaimSubmition, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sOut_997_Acknowledge", oClearingHouse.Out_997_Acknowledge, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sGen_CSRReports", oClearingHouse.Gen_CSRReports, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sGen_Letters", oClearingHouse.Gen_Letters, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sGen_Reports", oClearingHouse.Gen_Reports, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sGen_Statements", oClearingHouse.Gen_Statements, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sGen_WorkedTrans", oClearingHouse.Gen_WorkedTrans, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nFolderCategory", ClearinghouseFolderTypes.None.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", oClearingHouse.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                oDB.Execute("BL_INSERT_ClearingHouseDTL", oDBParameters, out Value);
                if (Value != null && Convert.ToString(Value) != "")
                {
                    _ReturnID = Convert.ToInt64(Value);
                }
                #endregion

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _ReturnID;
        }

        public long Add_Detail(ClearingHouse oClearingHouse)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object Value = null;
            Int64 _ReturnID = 0;
            try
            {

                oDB.Connect(false);
                //@nClearingHouseID, @sClearingHouseCode, @sURL, @sUserName, @sPassword, @sIn_271_ElgibilityResponse,
                // @sIn_277_ClaimStatus, @sIn_835_Remitance, @sIn_997_Acknowledge, @sOut_276_ElgibilityEnquiry,
                // @sOut_837P_ClaimSubmition, @sOut_997_Acknowledge, @sGen_CSRReports, @sGen_Letters, @sGen_Reports,
                //@sGen_Statements, @sGen_WorkedTrans, @nFolderCategory, @nClinicID

                oDBParameters.Add("@nClearingHouseID", oClearingHouse.ClearingHouseID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sClearingHouseCode", oClearingHouse.ClearingHouseCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sURL", oClearingHouse.URL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sUserName", oClearingHouse.UserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sPassword", oClearingHouse.Password, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sIn_271_ElgibilityResponse", oClearingHouse.In_271_ElgibilityResponse, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sIn_277_ClaimStatus", oClearingHouse.In_277_ClaimStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sIn_835_Remitance", oClearingHouse.In_835_Remitance, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sIn_997_Acknowledge", oClearingHouse.In_997_Acknowledge, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sOut_276_ElgibilityEnquiry", oClearingHouse.Out_276_ElgibilityEnquiry, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sOut_837P_ClaimSubmition", oClearingHouse.Out_837P_ClaimSubmition, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sOut_997_Acknowledge", oClearingHouse.Out_997_Acknowledge, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sGen_CSRReports", oClearingHouse.Gen_CSRReports, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sGen_Letters", oClearingHouse.Gen_Letters, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sGen_Reports", oClearingHouse.Gen_Reports, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sGen_Statements", oClearingHouse.Gen_Statements, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sGen_WorkedTrans", oClearingHouse.Gen_WorkedTrans, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nFolderCategory", oClearingHouse.FolderCategory, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", oClearingHouse.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                oDB.Execute("BL_INUP_ClearingHouseDTL", oDBParameters, out Value);
                if (Value != null && Convert.ToString(Value) != "")
                {
                    _ReturnID = Convert.ToInt64(Value);
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _ReturnID;
        }

        public ClearingHouse GetClearingHouse(Int64 ClearingHouseID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ClearingHouse oClearingHouse = null;
            try
            {
                DataTable dtClearing = null;
                string _sqlQuery = "";

                oDB.Connect(false);

                _sqlQuery = "SELECT ISNULL(nClearingHouseID,0) AS nClearingHouseID,ISNULL(sClearingHouseCode,'') AS  sClearingHouseName,"
                + " ISNULL(sReceiverID,'') AS  sReceiverID,ISNULL(sReceiverName,'') AS  sReceiverName,ISNULL(sSubmitterID,'') AS  sSubmitterID,"
                + " ISNULL(bIsOneJQulifier,'false') AS  bIsOneJQulifier,ISNULL(sOneJQulifier,'') AS  sOneJQulifier,ISNULL(bIsSenderCode,'false') AS  bIsSenderCode,"
                + " ISNULL(sSenderCode,'') AS  sSenderCode,ISNULL(bIsVenderIDCode,'false') AS  bIsVenderIDCode,ISNULL(sVenderIDCode,'') AS  sVenderIDCode,"
                + " ISNULL(bIsLoop1000BNM109,'false') AS  bIsLoop1000BNM109,ISNULL(sLoop1000BNM109,'') AS  sLoop1000BNM109,ISNULL(nTypeOfData,0) AS  nTypeOfData,"
                + " ISNULL(bIsISA,'') AS bIsISA"
                + " FROM BL_ClearingHouse_MST WITH (NOLOCK) "
                + " WHERE nClearingHouseID = " + ClearingHouseID + " AND nClinicID = " + ClinicID + " ";

                oDB.Retrive_Query(_sqlQuery, out dtClearing);

                if (dtClearing != null && dtClearing.Rows.Count > 0)
                {
                    oClearingHouse = new ClearingHouse(_databaseconnectionstring);

                    oClearingHouse.ClearingHouseID = ClearingHouseID;
                    oClearingHouse.ClearingHouseName = Convert.ToString(dtClearing.Rows[0]["sClearingHouseName"]);
                    oClearingHouse.RecieverName = Convert.ToString(dtClearing.Rows[0]["sReceiverName"]);
                    oClearingHouse.RecieverID = Convert.ToString(dtClearing.Rows[0]["sReceiverID"]);
                    oClearingHouse.SubmitterID = Convert.ToString(dtClearing.Rows[0]["sSubmitterID"]);
                    oClearingHouse.IsOneJQualifier = Convert.ToBoolean(dtClearing.Rows[0]["bIsOneJQulifier"]);
                    oClearingHouse.OneJQualifier = Convert.ToString(dtClearing.Rows[0]["sOneJQulifier"]);
                    oClearingHouse.IsSenderCode = Convert.ToBoolean(dtClearing.Rows[0]["bIsSenderCode"]);
                    oClearingHouse.SenderCode = Convert.ToString(dtClearing.Rows[0]["sSenderCode"]);
                    oClearingHouse.IsVenderID = Convert.ToBoolean(dtClearing.Rows[0]["bIsVenderIDCode"]);
                    oClearingHouse.VenderID = Convert.ToString(dtClearing.Rows[0]["sVenderIDCode"]);
                    oClearingHouse.IsLoop1000B = Convert.ToBoolean(dtClearing.Rows[0]["bIsLoop1000BNM109"]);
                    oClearingHouse.Loop1000B = Convert.ToString(dtClearing.Rows[0]["sLoop1000BNM109"]);
                    oClearingHouse.TypeOfData = (TypeOfData)Convert.ToInt32(dtClearing.Rows[0]["nTypeOfData"]);
                    oClearingHouse.IsISA = Convert.ToBoolean(dtClearing.Rows[0]["bIsISA"]);
                    oClearingHouse.ClinicID = ClinicID;
                }

                #region Get Clearinghouse details

                DataTable dtClearingDetail = null;

                oDB.Connect(false);

                _sqlQuery = "SELECT ISNULL(sClearingHouseCode,'') AS sClearingHouseCode,ISNULL(sURL,'') AS sURL,ISNULL(sUserName,'') AS sUserName, "
                        + " ISNULL(sPassword,'') AS sPassword,ISNULL(sIn_271_ElgibilityResponse,'') AS sIn_271_ElgibilityResponse, "
                        + " ISNULL(sIn_277_ClaimStatus,'') AS sIn_277_ClaimStatus,ISNULL(sIn_835_Remitance,'') AS sIn_835_Remitance, "
                        + " ISNULL(sIn_997_Acknowledge,'') AS sIn_997_Acknowledge,ISNULL(sOut_276_ElgibilityEnquiry,'') AS sOut_276_ElgibilityEnquiry, "
                        + " ISNULL(sOut_837P_ClaimSubmition,'') AS sOut_837P_ClaimSubmition,ISNULL(sOut_997_Acknowledge,'') AS  sOut_997_Acknowledge, "
                        + " ISNULL(sGen_CSRReports,'') AS sGen_CSRReports,ISNULL(sGen_Letters,'') AS sGen_Letters,ISNULL(sGen_Reports,'') AS sGen_Reports,"
                        + " ISNULL(sGen_Statements,'') AS sGen_Statements,ISNULL(sGen_WorkedTrans,'') AS sGen_WorkedTrans,ISNULL(nFolderCategory,0) AS nFolderCategory "
                        + " FROM BL_ClearingHouse_DTL WITH (NOLOCK)  WHERE nClearingHouseID = " + ClearingHouseID + " AND nClinicID = " + ClinicID + " ";

                oDB.Retrive_Query(_sqlQuery, out dtClearingDetail);

                if (dtClearingDetail != null && dtClearingDetail.Rows.Count > 0)
                {

                    oClearingHouse.ClearingHouseID = ClearingHouseID;
                    oClearingHouse.ClearingHouseCode = Convert.ToString(dtClearingDetail.Rows[0]["sClearingHouseCode"]);
                    oClearingHouse.URL = Convert.ToString(dtClearingDetail.Rows[0]["sURL"]);
                    oClearingHouse.UserName = Convert.ToString(dtClearingDetail.Rows[0]["sUserName"]);
                    oClearingHouse.Password = Convert.ToString(dtClearingDetail.Rows[0]["sPassword"]);
                    oClearingHouse.In_271_ElgibilityResponse = Convert.ToString(dtClearingDetail.Rows[0]["sIn_271_ElgibilityResponse"]);
                    oClearingHouse.In_277_ClaimStatus = Convert.ToString(dtClearingDetail.Rows[0]["sIn_277_ClaimStatus"]);
                    oClearingHouse.In_835_Remitance = Convert.ToString(dtClearingDetail.Rows[0]["sIn_835_Remitance"]);
                    oClearingHouse.In_997_Acknowledge = Convert.ToString(dtClearingDetail.Rows[0]["sIn_997_Acknowledge"]);
                    oClearingHouse.Out_276_ElgibilityEnquiry = Convert.ToString(dtClearingDetail.Rows[0]["sOut_276_ElgibilityEnquiry"]);
                    oClearingHouse.Out_837P_ClaimSubmition = Convert.ToString(dtClearingDetail.Rows[0]["sOut_837P_ClaimSubmition"]);
                    oClearingHouse.Out_997_Acknowledge = Convert.ToString(dtClearingDetail.Rows[0]["sOut_997_Acknowledge"]);
                    oClearingHouse.In_997_Acknowledge = Convert.ToString(dtClearingDetail.Rows[0]["sIn_997_Acknowledge"]);
                    oClearingHouse.Gen_CSRReports = Convert.ToString(dtClearingDetail.Rows[0]["sGen_CSRReports"]);
                    oClearingHouse.Gen_Letters = Convert.ToString(dtClearingDetail.Rows[0]["sGen_Letters"]);
                    oClearingHouse.Gen_Reports = Convert.ToString(dtClearingDetail.Rows[0]["sGen_Reports"]);
                    oClearingHouse.Gen_Statements = Convert.ToString(dtClearingDetail.Rows[0]["sGen_Statements"]);
                    oClearingHouse.Gen_WorkedTrans = Convert.ToString(dtClearingDetail.Rows[0]["sGen_WorkedTrans"]);
                    oClearingHouse.FolderCategory = Convert.ToInt64(dtClearingDetail.Rows[0]["nFolderCategory"]);

                    oClearingHouse.ClinicID = ClinicID;
                }

                if (dtClearing != null)
                {
                    dtClearing.Dispose();
                    dtClearing = null;
                }
                if (dtClearingDetail != null)
                {
                    dtClearingDetail.Dispose();
                    dtClearingDetail = null;
                }
                #endregion

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return oClearingHouse;
        }


        public DataTable GetClearingHouse()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtClearing = null;
            try
            {

                string _sqlQuery = "";

                oDB.Connect(false);

                _sqlQuery = "SELECT ISNULL(nClearingHouseID,0) AS nClearingHouseID,ISNULL(sClearingHouseCode,'') AS  sClearingHouseName,"
                + " ISNULL(sReceiverID,'') AS  sReceiverID,ISNULL(sReceiverName,'') AS  sReceiverName,ISNULL(sSubmitterID,'') AS  sSubmitterID,"
                + " ISNULL(bIsOneJQulifier,'false') AS  bIsOneJQulifier,ISNULL(sOneJQulifier,'') AS  sOneJQulifier,ISNULL(bIsSenderCode,'false') AS  bIsSenderCode,"
                + " ISNULL(sSenderCode,'') AS  sSenderCode,ISNULL(bIsVenderIDCode,'false') AS  bIsVenderIDCode,ISNULL(sVenderIDCode,'') AS  sVenderIDCode,"
                + " ISNULL(bIsLoop1000BNM109,'false') AS  bIsLoop1000BNM109,ISNULL(sLoop1000BNM109,'') AS  sLoop1000BNM109,ISNULL(nTypeOfData,0) AS  nTypeOfData,"
                + " CASE ISNULL(bIsISA,'FALSE') WHEN 'TRUE' THEN 'YES'  WHEN 'FALSE' THEN 'NO' END AS bIsISA "
                + " FROM BL_ClearingHouse_MST WITH (NOLOCK) "
                + " WHERE nClinicID = " + ClinicID + " ";

                oDB.Retrive_Query(_sqlQuery, out dtClearing);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return dtClearing;
        }

        public void Delete(long ClearingHouseID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {

                string _sqlQuery = "";

                oDB.Connect(false);

                _sqlQuery = "DELETE FROM BL_ClearingHouse_MST "
                         + " WHERE nClearingHouseID = " + ClearingHouseID + " AND nClinicID = " + ClinicID + " ";

                oDB.Execute_Query(_sqlQuery);

                _sqlQuery = "DELETE FROM BL_ClearingHouse_DTL "
                       + " WHERE nClearingHouseID = " + ClearingHouseID + " AND nClinicID = " + ClinicID + " ";

                oDB.Execute_Query(_sqlQuery);


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        internal bool CheckDuplicate(Int64 ClearingHouseID, string _ClearingHousecode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            string strQuery = "";
            bool _result = false;
            try
            {

                oDB.Connect(false);
                if (ClearingHouseID == 0)
                {
                    strQuery = " select count(nClearingHouseID) FROM BL_ClearingHouse_MST WITH (NOLOCK) where sClearingHouseCode = '" + _ClearingHousecode.Replace("'", "''") + "' AND nClinicID = " + this.ClinicID + " ";
                }
                else
                {
                    strQuery = " select count(nClearingHouseID) FROM BL_ClearingHouse_MST WITH (NOLOCK) where sClearingHouseCode = '" + _ClearingHousecode.Replace("'", "''") + "' AND nClearingHouseID <> " + ClearingHouseID + "  AND nClinicID = " + this.ClinicID + " ";
                }

                object _intResult = null;

                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

    }

    #endregion

    #region " AdjustmentType "

    public class AdjustmentType : IDisposable
    {
        #region " Variable Declarations "
        private Int64 _AdjusmentTypeId = 0;
        private string _AdjusmentTypeCode = "";
        private string _AdjustmentTypeDesc = "";
        private bool _Inactive = false;
        private string _messageBoxCaption = String.Empty;
        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        #endregion

        #region " Property Procedures "

        public Int64 AdjusmentTypeID
        {
            get { return _AdjusmentTypeId; }
            set { _AdjusmentTypeId = value; }
        }
        public string AdjusmentTypeCode
        {
            get { return _AdjusmentTypeCode; }
            set { _AdjusmentTypeCode = value; }
        }
        public string AdjustmentTypeDesc
        {
            get { return _AdjustmentTypeDesc; }
            set { _AdjustmentTypeDesc = value; }
        }
        public bool Inactive
        {
            get { return _Inactive; }
            set { _Inactive = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion

        #region "Constructor & Destructor"


        public AdjustmentType(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~AdjustmentType()
        {
            Dispose(false);
        }

        #endregion

        #region " Private & Public Methods "

        public Int64 Add()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameter oDBParameter;
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                object _intresult = 0;
                oDB.Connect(false);

                oDBParameters.Add("@nAdjustmentTypeId", this.AdjusmentTypeID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sAdjustmentTypeCode", this.AdjusmentTypeCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sAdjustmentTypeDesc", this.AdjustmentTypeDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@bIsBlocked", this.Inactive, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                _result = oDB.Execute("BL_INUP_AdjustmentType", oDBParameters, out _intresult);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }

            }
            return _result;
        }

        public DataTable GetAdjustmentTypes()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtAdjustmentTypes = null;
            string _sqlQuery = "";
            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT nAdjustmentTypeId,ISNULL(sAdjustmentTypeCode,'') AS sAdjustmentTypeCode, " +
                            " ISNULL(sAdjustmentTypeDesc,'') AS sAdjustmentTypeDesc, " +
                            " CASE ISNULL(bIsBlocked,'false') WHEN 'false' THEN 'Active' WHEN 'true' THEN 'Inactive' END AS Status, " +
                            " ISNULL(nClinicID,0) AS nClinicID " +
                            " FROM BL_AdjustmentType_MST WITH (NOLOCK) " +
                            " WHERE  nClinicID = " + _ClinicID + " ";

                oDB.Retrive_Query(_sqlQuery, out dtAdjustmentTypes);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return dtAdjustmentTypes;
        }

        public DataTable GetAdjustmentType(Int64 AdjustmentTypeId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtAdjustmentTypes = null;
            string _sqlQuery = "";
            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT nAdjustmentTypeId,ISNULL(sAdjustmentTypeCode,'') AS sAdjustmentTypeCode, " +
                            " ISNULL(sAdjustmentTypeDesc,'') AS sAdjustmentTypeDesc,ISNULL(bIsBlocked,'false') AS Inactive, ISNULL(nClinicID,0) AS nClinicID " +
                            " FROM BL_AdjustmentType_MST WITH (NOLOCK) " +
                            " WHERE nAdjustmentTypeId = " + AdjustmentTypeId + " AND nClinicID = " + _ClinicID + " ";

                oDB.Retrive_Query(_sqlQuery, out dtAdjustmentTypes);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return dtAdjustmentTypes;
        }

        public string GetAdjustmentDescription(string AdjustmentCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Object ReturnValue = null;
            string _sqlQuery = "";
            string _result = "";
            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT ISNULL(sAdjustmentTypeDesc,'') AS sDescription FROM BL_AdjustmentType_MST WITH (NOLOCK) WHERE " +
                           " UPPER(sAdjustmentTypeCode) = '" + AdjusmentTypeCode.ToUpper() + "' AND nClinicID=" + this.ClinicID + " ";
                ReturnValue = oDB.ExecuteScalar_Query(_sqlQuery);
                if (ReturnValue != null && ReturnValue != DBNull.Value)
                {
                    _result = Convert.ToString(ReturnValue);
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (ReturnValue != null) { ReturnValue = null; }
            }
            return _result;
        }

        public bool IsExistsAdjustmentType(Int64 AdjustmentTypeId, string AdjustmentTypeCode, string AdjustmentTypeName)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                if (AdjusmentTypeID == 0)
                {
                    strQuery = "select count(nAdjustmentTypeId) from BL_AdjustmentType_MST WITH (NOLOCK) where (sAdjustmentTypeCode = '" + AdjustmentTypeCode.Replace("'", "''") + "' OR sAdjustmentTypeDesc = '" + AdjustmentTypeName.Replace("'", "''") + "') AND nClinicID=" + this.ClinicID + " ";
                }
                else
                {
                    strQuery = "select count(nAdjustmentTypeId) from BL_AdjustmentType_MST WITH (NOLOCK) where ((sAdjustmentTypeCode = '" + AdjustmentTypeCode.Replace("'", "''") + "' OR sAdjustmentTypeDesc = '" + AdjustmentTypeName.Replace("'", "''") + "') AND nAdjustmentTypeId <> " + AdjustmentTypeId + ") AND nClinicID=" + this.ClinicID + " ";

                }

                object _intResult = null;
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        public bool BlockAdjustmentType(Int64 AdjustmentTypeId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "UPDATE BL_AdjustmentType_MST WITH (READPAST) SET bIsBlocked = '" + true + "' WHERE nAdjustmentTypeId = " + AdjustmentTypeId;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }
        public bool DeleteAdjustmentType(Int64 AdjustmentTypeId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "DELETE FROM  BL_AdjustmentType_MST  WHERE nAdjustmentTypeId = " + AdjustmentTypeId;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool IsBlocked(string AdjustmentTypeCode, string AdjustmentTypeDesc)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            Object _returnValue = null;
            bool _IsBlocked = false;

            try
            {
                oDB.Connect(false);
                _sqlQuery = "select ISNULL(count(nAdjustmentTypeId),0) AS nAdjustmentTypeId " +
                " FROM BL_AdjustmentType_MST WITH (NOLOCK) " +
                " WHERE UPPER(sAdjustmentTypeCode) = '" + AdjustmentTypeCode + "' " +
                " AND UPPER(sAdjustmentTypeDesc) = '" + AdjustmentTypeDesc + "' " +
                " AND bIsBlocked = '" + true + "' AND nClinicID = " + this.ClinicID + " ";
                _returnValue = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_returnValue != null && _returnValue != DBNull.Value)
                {
                    _IsBlocked = Convert.ToBoolean(_returnValue);
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return _IsBlocked;
        }

        public bool IsInUse(string AdjustmentTypeCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            Object _returnValue = null;
            bool _IsInUse = false;

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT COUNT(RC.sReasonCode) UsedCount " +
                " FROM BL_EOB_ReasonCodes RC WITH(NOLOCK) INNER JOIN Debits D WITH(NOLOCK)  " +
                " ON	RC.nEOBPaymentID = D.nCreditID  " +
                " AND	RC.nEOBID = D.nEOBID  " +
                " WHERE D.nEntryType IN (6,9) AND RC.sReasonCode = '" + AdjustmentTypeCode + "'  ";

                _returnValue = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_returnValue != null && _returnValue != DBNull.Value)
                {
                    _IsInUse = Convert.ToBoolean(_returnValue);
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (_returnValue != null) { _returnValue = null; }
            }
            return _IsInUse;
        }

        #endregion
    }

    #endregion " AdjustmentType "

    #region " Refund Type "

    public class RefundType : IDisposable
    {
        #region " Variable Declarations "
        private Int64 _RefundTypeId = 0;
        private string _RefundTypeCode = "";
        private string _RefundTypeDesc = "";
        private bool _IsSystem = false;
        private string _messageBoxCaption = String.Empty;
        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        #endregion

        #region " Property Procedures "

        public Int64 RefundTypeID
        {
            get { return _RefundTypeId; }
            set { _RefundTypeId = value; }
        }
        public string RefundTypeCode
        {
            get { return _RefundTypeCode; }
            set { _RefundTypeCode = value; }
        }
        public string RefundTypeDesc
        {
            get { return _RefundTypeDesc; }
            set { _RefundTypeDesc = value; }
        }
        public bool IsSystem
        {
            get { return _IsSystem; }
            set { _IsSystem = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        #endregion

        #region "Constructor & Destructor"


        public RefundType(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~RefundType()
        {
            Dispose(false);
        }

        #endregion

        #region " Private & Public Methods "

        public Int64 Add()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameter oDBParameter;
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                object _intresult = 0;
                oDB.Connect(false);

                oDBParameters.Add("@nRefundTypeId", this.RefundTypeID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sRefundTypeCode", this.RefundTypeCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sRefundTypeDesc", this.RefundTypeDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@bIsSystem", this.IsSystem, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                _result = oDB.Execute("BL_INUP_RefundType", oDBParameters, out _intresult);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }

                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }

            }
            return _result;
        }

        public DataTable GetRefundTypes()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtRefundTypes = null;
            string _sqlQuery = "";
            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT nRefundTypeId,ISNULL(sRefundTypeCode,'') AS sRefundTypeCode, " +
                            " ISNULL(sRefundTypeDesc,'') AS sRefundTypeDesc,case ISNULL(bIsSystem,0)  when 0 then 'User' when 1 then 'System' end AS bIsSystem ,ISNULL(nClinicID,0) AS nClinicID " +
                            " FROM BL_RefundType_MST WITH (NOLOCK) " +
                            " WHERE nClinicID = " + _ClinicID + " ";

                oDB.Retrive_Query(_sqlQuery, out dtRefundTypes);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return dtRefundTypes;
        }

        public DataTable GetRefundType(Int64 RefundTypeId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtRefundTypes = null;
            string _sqlQuery = "";
            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT nRefundTypeId,ISNULL(sRefundTypeCode,'') AS sRefundTypeCode, " +
                            " ISNULL(sRefundTypeDesc,'') AS sRefundTypeDesc,case ISNULL(bIsSystem,0)  when 0 then 'User' when 1 then 'System' end AS bIsSystem,ISNULL(nClinicID,0) AS nClinicID " +
                            " FROM BL_RefundType_MST WITH (NOLOCK) " +
                            " WHERE nRefundTypeId = " + RefundTypeId + " and nClinicID = " + _ClinicID + " ";

                oDB.Retrive_Query(_sqlQuery, out dtRefundTypes);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return dtRefundTypes;
        }

        public string GetRefundDescription(string RefundCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Object ReturnValue = null;
            string _sqlQuery = "";
            string _result = "";
            try
            {
                oDB.Connect(false);
                _sqlQuery = " SELECT ISNULL(sRefundTypeDesc,'') AS sDescription FROM BL_RefundType_MST WITH (NOLOCK) WHERE " +
                           " UPPER(sRefundTypeCode) = '" + RefundTypeCode.ToUpper() + "' AND nClinicID=" + this.ClinicID + " ";
                ReturnValue = oDB.ExecuteScalar_Query(_sqlQuery);
                if (ReturnValue != null && ReturnValue != DBNull.Value)
                {
                    _result = Convert.ToString(ReturnValue);
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (ReturnValue != null) { ReturnValue = null; }
            }
            return _result;
        }

        public bool IsExistsRefundType(Int64 RefundTypeId, string RefundTypeCode, string RefundTypeName, Int64 identifyRefundType)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                // Added by Rahul Patel on 16-09-2010
                // For handling validation for both Refund type code and Refund Type description
                // Here 'identifyRefundType' = 1 is for Refund type Code and  'identifyRefundType' = 2 is for Refund type Description
                switch (identifyRefundType)
                {
                    case 1:// Refund Type Code
                        if (RefundTypeID == 0)
                        {
                            strQuery = "select count(nRefundTypeId) from BL_RefundType_MST WITH (NOLOCK) where (sRefundTypeCode = '" + RefundTypeCode.Replace("'", "''") + "' ) AND nClinicID=" + this.ClinicID + " ";
                        }
                        else
                        {
                            strQuery = "select count(nRefundTypeId) from BL_RefundType_MST WITH (NOLOCK) where ((sRefundTypeCode = '" + RefundTypeCode.Replace("'", "''") + "') AND nRefundTypeId <> " + RefundTypeId + ") AND nClinicID=" + this.ClinicID + " ";
                        }
                        break;
                    case 2:// Refund Type Description
                        if (RefundTypeID == 0)
                        {
                            strQuery = "select count(nRefundTypeId) from BL_RefundType_MST WITH (NOLOCK) where (sRefundTypeDesc = '" + RefundTypeName.Replace("'", "''") + "') AND nClinicID=" + this.ClinicID + " ";
                        }
                        else
                        {
                            strQuery = "select count(nRefundTypeId) from BL_RefundType_MST WITH (NOLOCK) where ((sRefundTypeDesc = '" + RefundTypeName.Replace("'", "''") + "') AND nRefundTypeId <> " + RefundTypeId + ") AND nClinicID=" + this.ClinicID + " ";
                        }
                        break;
                    //default: //For Both 
                    //    if (RefundTypeID == 0)
                    //    {
                    //        strQuery = "select count(nRefundTypeId) from BL_RefundType_MST where (sRefundTypeCode = '" + RefundTypeCode.Replace("'", "''") + "') OR (sRefundTypeDesc = '" + RefundTypeName.Replace("'", "''") + "') AND nClinicID=" + this.ClinicID + " ";
                    //    }
                    //    else
                    //    {
                    //        strQuery = "select count(nRefundTypeId) from BL_RefundType_MST where ((sRefundTypeCode = '" + RefundTypeCode.Replace("'", "''") + "') OR (sRefundTypeDesc = '" + RefundTypeName.Replace("'", "''") + "') AND nRefundTypeId <> " + RefundTypeId + ") AND nClinicID=" + this.ClinicID + " ";
                    //    }
                    //    break;
                }


                object _intResult = null;
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }

        public bool DeleteRefundType(Int64 RefundTypeId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "DELETE FROM  BL_RefundType_MST  WHERE nRefundTypeId = " + RefundTypeId;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool CanDelete(Int64 RefundTypeId)
        {
            bool Result = true;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //   DataTable dt = null;
            object value = null;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "";
                SqlQuery = "SELECT ISNULL(bIsSystem,'false') AS bIsSystem FROM BL_RefundType_MST WITH (NOLOCK)  WHERE nRefundTypeId = " + RefundTypeId;
                value = oDB.ExecuteScalar_Query(SqlQuery);
                if (value != null && Convert.ToString(value) != "")
                {
                    if (Convert.ToBoolean(value) == true)
                    {
                        Result = false;
                    }
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return Result;
        }

        #endregion
    }

    #endregion " Refund Type "


    #region "Scrubber"

    public class gloScrubber : IDisposable
    {
        enum CodeTypeFlag
        {
            None = 0,
            TOS = 1,
            POS = 2,
            Diagnosis = 3,
            Modifier = 4
        }

        private Int64 _ClinicID = 0;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";

        public gloScrubber(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~gloScrubber()
        {
            Dispose(false);
        }

        #endregion

        public Int64 Add(Scrubber oScrubber)
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                string _sqlQuery = "DELETE FROM CPT_Scrubber WHERE sCPTCode = '" + oScrubber.CPTCode.Replace("'", "''").Trim() + "'";
                oDB.Execute_Query(_sqlQuery);

                //Add POS
                if (oScrubber.POSCode.Trim() != "")
                {
                    oDBParameters.Clear();
                    oDBParameters.Add("@nCPTScrubberID", oScrubber.ScrubberID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                    oDBParameters.Add("@sCPTCode", oScrubber.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                    oDBParameters.Add("@sCPTDesc", oScrubber.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                    oDBParameters.Add("@sCode", oScrubber.POSCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                    oDBParameters.Add("@sDescription", oScrubber.POSDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                    oDBParameters.Add("@nCodeTypeFlag", CodeTypeFlag.POS.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                    oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Execute("gsp_INSERT_CPT_Scrubber", oDBParameters);
                }

                //Add TOS
                if (oScrubber.TOSCode.Trim() != "")
                {
                    oDBParameters.Clear();
                    oDBParameters.Add("@nCPTScrubberID", oScrubber.ScrubberID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                    oDBParameters.Add("@sCPTCode", oScrubber.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                    oDBParameters.Add("@sCPTDesc", oScrubber.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                    oDBParameters.Add("@sCode", oScrubber.TOSCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                    oDBParameters.Add("@sDescription", oScrubber.TOSDesc, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                    oDBParameters.Add("@nCodeTypeFlag", CodeTypeFlag.TOS.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                    oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Execute("gsp_INSERT_CPT_Scrubber", oDBParameters);
                }

                //Add Diagnosis
                for (int i = 0; i < oScrubber.Diagnosis.Count; i++)
                {
                    oDBParameters.Clear();
                    oDBParameters.Add("@nCPTScrubberID", oScrubber.ScrubberID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                    oDBParameters.Add("@sCPTCode", oScrubber.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                    oDBParameters.Add("@sCPTDesc", oScrubber.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                    oDBParameters.Add("@sCode", oScrubber.Diagnosis[i].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                    oDBParameters.Add("@sDescription", oScrubber.Diagnosis[i].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                    oDBParameters.Add("@nCodeTypeFlag", CodeTypeFlag.Diagnosis.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                    oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Execute("gsp_INSERT_CPT_Scrubber", oDBParameters);
                }

                //Add Diagnosis
                for (int i = 0; i < oScrubber.Modifiers.Count; i++)
                {
                    oDBParameters.Clear();
                    oDBParameters.Add("@nCPTScrubberID", oScrubber.ScrubberID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                    oDBParameters.Add("@sCPTCode", oScrubber.CPTCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                    oDBParameters.Add("@sCPTDesc", oScrubber.CPTDescription, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                    oDBParameters.Add("@sCode", oScrubber.Modifiers[i].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                    oDBParameters.Add("@sDescription", oScrubber.Modifiers[i].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                    oDBParameters.Add("@nCodeTypeFlag", CodeTypeFlag.Modifier.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                    oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Execute("gsp_INSERT_CPT_Scrubber", oDBParameters);
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            //return _result;
            return _result;
        }


        public DataTable GetScrubbers()
        {
            DataTable dtResult = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtCPT = null;
            DataTable dtCodes = null;
            DataRow[] _drCodes = null;

            try
            {
                oDB.Connect(false);

                dtResult.Columns.Add("CPTCode");
                dtResult.Columns.Add("CPTDesc");
                dtResult.Columns.Add("TOS");
                dtResult.Columns.Add("POS");
                dtResult.Columns.Add("Diagnosis");
                dtResult.Columns.Add("Modifiers");


                string _sqlQuery = "SELECT DISTINCT ISNULL(sCPTCode,'') AS sCPTCode FROM CPT_Scrubber WITH (NOLOCK) ORDER BY sCPTCode ";
                oDB.Retrive_Query(_sqlQuery, out dtCPT);

                _sqlQuery = "SELECT ISNULL(sCPTCode,'') AS sCPTCode,ISNULL(sCode,'') AS sCode,ISNULL(nCodeTypeFlag,0) AS nCodeTypeFlag  FROM CPT_Scrubber WITH (NOLOCK)";
                oDB.Retrive_Query(_sqlQuery, out dtCodes);

                oDB.Disconnect();


                if (dtCPT != null)
                {
                    for (int i = 0; i < dtCPT.Rows.Count; i++)
                    {

                        //_sqlQuery = "SELECT ISNULL(sCode,'') AS sCode,nCodeTypeFlag FROM CPT_Scrubber WITH (NOLOCK) WHERE sCPTCode = '" + Convert.ToString(dt.Rows[i]["sCPTCode"]).Trim() + "'";
                        //oDB.Retrive_Query(_sqlQuery, out dtCodes);

                        if (dtCodes != null && dtCodes.Rows.Count > 0)
                        {

                            _drCodes = dtCodes.Select("sCPTCode = '" + Convert.ToString(dtCPT.Rows[i]["sCPTCode"]).Trim() + "'");

                            if (_drCodes.Length > 0)
                            {
                                DataRow oRow = dtResult.NewRow();
                                oRow["CPTCode"] = Convert.ToString(dtCPT.Rows[i]["sCPTCode"]).Trim();
                                //for (int k = 0; k < dtCodes.Rows.Count; k++)
                                //{ 

                                for (int k = 0; k < _drCodes.Length; k++)
                                {
                                    switch ((CodeTypeFlag)Convert.ToInt32(_drCodes[k]["nCodeTypeFlag"]))
                                    {
                                        case CodeTypeFlag.TOS:
                                            {
                                                oRow["TOS"] = Convert.ToString(_drCodes[k]["sCode"]);
                                            }
                                            break;
                                        case CodeTypeFlag.POS:
                                            {
                                                oRow["POS"] = Convert.ToString(_drCodes[k]["sCode"]);
                                            }
                                            break;
                                        case CodeTypeFlag.Diagnosis:
                                            {
                                                oRow["Diagnosis"] = oRow["Diagnosis"].ToString().Trim() + ", " + Convert.ToString(_drCodes[k]["sCode"]).Trim();
                                            }
                                            break;
                                        case CodeTypeFlag.Modifier:
                                            {
                                                oRow["Modifiers"] = oRow["Modifiers"].ToString().Trim() + ", " + Convert.ToString(_drCodes[k]["sCode"]).Trim();
                                            }
                                            break;
                                    }

                                }


                                if (Convert.ToString(oRow["Diagnosis"]) != "")
                                {
                                    oRow["Diagnosis"] = Convert.ToString(oRow["Diagnosis"]).Trim().Substring(1, Convert.ToString(oRow["Diagnosis"]).Trim().Length - 1);
                                }

                                if (Convert.ToString(oRow["Modifiers"]) != "")
                                {
                                    oRow["Modifiers"] = Convert.ToString(oRow["Modifiers"]).Trim().Substring(1, Convert.ToString(oRow["Modifiers"]).Trim().Length - 1);
                                }

                                dtResult.Rows.Add(oRow);
                            }
                        }


                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                //oDB.Disconnect();
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                if (dtCodes != null) { dtCodes.Dispose(); dtCodes = null; }
                if (dtCPT != null) { dtCPT.Dispose(); dtCPT = null; }
            }

            return dtResult;
        }

        internal bool DeleteScruber(string sCPTCode)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);

                string _sqlQuery = "DELETE FROM CPT_Scrubber WHERE sCPTCode = '" + sCPTCode + "'";
                oDB.Execute_Query(_sqlQuery);
                _result = true;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            //return _result;
            return _result;
        }

        internal Scrubber GetScrubber(string sCPTCode)
        {
            Scrubber oScrubber = new Scrubber();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);

                DataTable dtCodes = null;

                string _sqlQuery = "SELECT ISNULL(sCPTCode,'') AS  sCPTCode,ISNULL(sCPTDesc,'') AS  sCPTDesc,ISNULL(sCode,'') AS sCode,ISNULL(sDescription,'') AS sDescription,nCodeTypeFlag FROM CPT_Scrubber WITH (NOLOCK) WHERE sCPTCode = '" + sCPTCode.Trim().Replace("'", "''") + "'";
                oDB.Retrive_Query(_sqlQuery, out dtCodes);

                if (dtCodes != null && dtCodes.Rows.Count > 0)
                {
                    oScrubber.ClinicID = _ClinicID;
                    oScrubber.CPTCode = Convert.ToString(dtCodes.Rows[0]["sCPTCode"]).Trim();
                    oScrubber.CPTDescription = Convert.ToString(dtCodes.Rows[0]["sCPTDesc"]).Trim();

                    for (int k = 0; k < dtCodes.Rows.Count; k++)
                    {

                        switch ((CodeTypeFlag)Convert.ToInt32(dtCodes.Rows[k]["nCodeTypeFlag"]))
                        {
                            case CodeTypeFlag.TOS:
                                {
                                    oScrubber.TOSCode = Convert.ToString(dtCodes.Rows[k]["sCode"]);
                                    oScrubber.TOSDesc = Convert.ToString(dtCodes.Rows[k]["sDescription"]);
                                }
                                break;
                            case CodeTypeFlag.POS:
                                {
                                    oScrubber.POSCode = Convert.ToString(dtCodes.Rows[k]["sCode"]);
                                    oScrubber.POSDesc = Convert.ToString(dtCodes.Rows[k]["sDescription"]);
                                }
                                break;
                            case CodeTypeFlag.Diagnosis:
                                {
                                    oScrubber.Diagnosis.Add(0, Convert.ToString(dtCodes.Rows[k]["sCode"]), Convert.ToString(dtCodes.Rows[k]["sDescription"]));
                                }
                                break;
                            case CodeTypeFlag.Modifier:
                                {
                                    oScrubber.Modifiers.Add(0, Convert.ToString(dtCodes.Rows[k]["sCode"]), Convert.ToString(dtCodes.Rows[k]["sDescription"]));
                                }
                                break;
                        }

                    }

                }
                if (dtCodes != null)
                {
                    dtCodes.Dispose();
                    dtCodes = null;
                }



            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

            return oScrubber;
        }
    }

    public class Scrubber : IDisposable
    {

        #region "Constructor & Destructor"

        public Scrubber()
        {
            _Diagnosis = new gloGeneralItem.gloItems();
            _Modifiers = new gloGeneralItem.gloItems();
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
                    if (diagAssigned)
                    {
                        if (_Diagnosis != null)
                        {
                            _Diagnosis.Dispose();
                            _Diagnosis = null;
                        }
                    }
                    if (modAssigned)
                    {
                        if (_Modifiers != null)
                        {
                            _Modifiers.Dispose();
                            _Modifiers = null;
                        }
                    }
                }
            }
            disposed = true;
        }

        ~Scrubber()
        {
            Dispose(false);
        }

        #endregion

        private Int64 _nScrubberID = 0;
        private string _sCPTCode = "";
        private string _sCPTDescription = "";
        private string _sTOSCode = "";
        private string _sTOSDesc = "";
        private string _sPOSCode = "";
        private string _sPOSDesc = "";
        private gloGeneralItem.gloItems _Diagnosis = null;
        private gloGeneralItem.gloItems _Modifiers = null;
        private Int64 _ClinicID = 0;
        private bool diagAssigned = true;
        private bool modAssigned = false;

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public Int64 ScrubberID
        {
            get { return _nScrubberID; }
            set { _nScrubberID = value; }
        }

        public string CPTCode
        {
            get { return _sCPTCode; }
            set { _sCPTCode = value; }
        }

        public string CPTDescription
        {
            get { return _sCPTDescription; }
            set { _sCPTDescription = value; }
        }

        public string TOSCode
        {
            get { return _sTOSCode; }
            set { _sTOSCode = value; }
        }

        public string TOSDesc
        {
            get { return _sTOSDesc; }
            set { _sTOSDesc = value; }
        }

        public string POSCode
        {
            get { return _sPOSCode; }
            set { _sPOSCode = value; }
        }

        public string POSDesc
        {
            get { return _sPOSDesc; }
            set { _sPOSDesc = value; }
        }

        public gloGeneralItem.gloItems Diagnosis
        {
            get { return _Diagnosis; }
            set
            {

                if (diagAssigned)
                {
                    if (_Diagnosis != null)
                    {
                        _Diagnosis.Dispose();
                        _Diagnosis = null;
                    }
                }
                _Diagnosis = value;
                diagAssigned = false;
            }
        }

        public gloGeneralItem.gloItems Modifiers
        {
            get { return _Modifiers; }
            set
            {
                if (modAssigned)
                {
                    if (_Modifiers != null)
                    {
                        _Modifiers.Dispose();
                        _Modifiers = null;
                    }
                }
                _Modifiers = value;
                modAssigned = false;
            }
        }

    }

    #endregion

    #region " Referral CPT "

    public class gloReferralCPT : IDisposable
    {

        private Int64 _ClinicID = 0;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";

        public gloReferralCPT(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~gloReferralCPT()
        {
            Dispose(false);
        }

        #endregion

        public Int64 Add(ReferralCPT oReferralCPT)
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Int64 _RefCPTId = 0;
            Int64 _replicationId = 0;
            string _sqlQuery = "";
            object RefID = null;
            try
            {
                oDB.Connect(false);

                if (oReferralCPT.ReferralCPTID == 0)
                {
                    //_sqlQuery = "SELECT ISNULL(MAX(nRefCPTID),0) + 1   FROM BL_ReferralCPT_MST ";
                    //RefID = oDB.ExecuteScalar_Query(_sqlQuery);

                    _replicationId = oDB.GetPrefixTransactionID(0);

                    _sqlQuery = " IF EXISTS(SELECT nRefCPTID FROM BL_ReferralCPT_MST WITH (NOLOCK) WHERE convert(varchar(18),nRefCPTID) Like convert(varchar(18)," + _replicationId + ")+ '%') " +
                  " SELECT isnull(max(nRefCPTID),0)+1 FROM BL_ReferralCPT_MST WITH (NOLOCK) where convert(varchar(18),nRefCPTID) Like convert(varchar(18)," + _replicationId + ")+ '%' " +
                  " ELSE " +
                  " SELECT convert(numeric(18,0), convert(varchar(18)," + _replicationId + ") + '001')  ";

                    RefID = oDB.ExecuteScalar_Query(_sqlQuery);

                    if (RefID != null && Convert.ToInt64(RefID) > 0)
                    {
                        _RefCPTId = Convert.ToInt64(RefID);

                        //Add CPTs
                        for (int i = 0; i < oReferralCPT.CPTs.Count; i++)
                        {
                            _sqlQuery = "";
                            _sqlQuery = " DELETE  " +
                                " FROM  BL_ReferralCPT_MST  " +
                                " WHERE (sCPTCode = '" + oReferralCPT.CPTs[i].Code.Replace("'", "''").Trim() + "')";

                            oDB.Execute_Query(_sqlQuery);

                            oDBParameters.Clear();

                            oDBParameters.Add("@nRefCPTID", _RefCPTId, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            //oDBParameters.Add("@nCPTID", oReferralCPT.CPTs[i].ID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                            oDBParameters.Add("@sCPTCode", oReferralCPT.CPTs[i].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                            oDBParameters.Add("@sDescription", oReferralCPT.CPTs[i].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                            oDBParameters.Add("@bIsReferral", oReferralCPT.IsReferralRequired, ParameterDirection.Input, SqlDbType.Bit);//	varchar(50),
                            oDBParameters.Add("@nClinicID", oReferralCPT.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                            oDB.Execute("gsp_INSERT_BL_ReferralCPT_MST", oDBParameters);
                        }
                    }

                }
                else
                {
                    _sqlQuery = "";
                    _sqlQuery = " DELETE  " +
                        " FROM  BL_ReferralCPT_MST  " +
                        " WHERE (nRefCPTID = " + oReferralCPT.ReferralCPTID + ")";

                    oDB.Execute_Query(_sqlQuery);

                    for (int i = 0; i < oReferralCPT.CPTs.Count; i++)
                    {

                        oDBParameters.Clear();

                        oDBParameters.Add("@nRefCPTID", oReferralCPT.ReferralCPTID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        //oDBParameters.Add("@nCPTID", oReferralCPT.CPTs[i].ID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oDBParameters.Add("@sCPTCode", oReferralCPT.CPTs[i].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                        oDBParameters.Add("@sDescription", oReferralCPT.CPTs[i].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                        oDBParameters.Add("@bIsReferral", oReferralCPT.IsReferralRequired, ParameterDirection.Input, SqlDbType.Bit);//	varchar(50),
                        oDBParameters.Add("@nClinicID", oReferralCPT.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                        oDB.Execute("gsp_INSERT_BL_ReferralCPT_MST", oDBParameters);
                    }
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            //return _result;
            return _result;
        }

        public DataTable GetReferralCPTs()
        {
            DataTable dtResult = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);

                //DataTable dt = new DataTable();

                string _sqlQuery =
                _sqlQuery = " SELECT nRefCPTID, sCPTCode, sDescription, " +
                     " CASE ISNULL(bIsReferral,0) WHEN 0 THEN 'No'WHEN 1 THEN 'Yes' END AS bIsReferral, " +
                     " nClinicID  " +
                     " FROM  BL_ReferralCPT_MST WITH (NOLOCK) " +
                     " ORDER BY sCPTCode ";
                oDB.Retrive_Query(_sqlQuery, out dtResult);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

            return dtResult;
        }

        public Int64 DeleteReferralCPT(string sCPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Int64 _result = 0;
            string strSQL = "";
            try
            {
                oDB.Connect(false);

                //strSQL = " DELETE  " +
                //         " FROM  BL_ReferralCPT_MST  " +
                //         " WHERE (sCPTCode = " + sCPTCode + ")";

                strSQL = " DELETE  FROM  BL_ReferralCPT_MST WHERE (sCPTCode = '" + sCPTCode + "')";

                _result = oDB.Execute_Query(strSQL);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        internal ReferralCPT GetReferralCPT(Int64 RefCPTID)
        {
            ReferralCPT oReferralCPT = new ReferralCPT();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);

                DataTable dtCodes = null;

                string _sqlQuery = "";
                _sqlQuery = " SELECT nRefCPTID, sCPTCode, sDescription, bIsReferral, nClinicID  " +
                      " FROM  BL_ReferralCPT_MST WITH (NOLOCK)  " +
                      " WHERE (nRefCPTID = " + RefCPTID + ")";
                oDB.Retrive_Query(_sqlQuery, out dtCodes);
                if (dtCodes != null && dtCodes.Rows.Count > 0)
                {
                    for (int _index = 0; _index < dtCodes.Rows.Count; _index++)
                    {
                        oReferralCPT.ReferralCPTID = Convert.ToInt64(dtCodes.Rows[_index]["nRefCPTID"]);
                        oReferralCPT.ClinicID = Convert.ToInt64(dtCodes.Rows[_index]["nClinicID"]);
                        oReferralCPT.IsReferralRequired = Convert.ToBoolean(dtCodes.Rows[_index]["bIsReferral"]);
                        oReferralCPT.CPTs.Add(0, Convert.ToString(dtCodes.Rows[_index]["sCPTCode"]), Convert.ToString(dtCodes.Rows[_index]["sDescription"]));
                    }
                }
                if (dtCodes != null)
                {
                    dtCodes.Dispose();
                    dtCodes = null;
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

            return oReferralCPT;
        }
    }

    public class ReferralCPT : IDisposable
    {

        #region "Constructor & Destructor"

        public ReferralCPT()
        {
            _CPTs = new gloGeneralItem.gloItems();
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
                    if (cptAssigned)
                    {
                        if (_CPTs != null)
                        {
                            _CPTs.Dispose();
                            _CPTs = null;
                        }
                    }
                }
            }
            disposed = true;
        }

        ~ReferralCPT()
        {
            Dispose(false);
        }

        #endregion

        private Int64 _nCPTID = 0;
        private Int64 _nRefCPTID = 0;
        private string _sCPTCode = "";
        private string _sCPTDescription = "";
        private gloGeneralItem.gloItems _CPTs = null;
        private Int64 _ClinicID = 0;
        private bool _IsReferralRequired = false;
        private bool cptAssigned = true;

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public Int64 CPTID
        {
            get { return _nCPTID; }
            set { _nCPTID = value; }
        }

        public Int64 ReferralCPTID
        {
            get { return _nRefCPTID; }
            set { _nRefCPTID = value; }
        }

        public string CPTCode
        {
            get { return _sCPTCode; }
            set { _sCPTCode = value; }
        }

        public string CPTDescription
        {
            get { return _sCPTDescription; }
            set { _sCPTDescription = value; }
        }

        public bool IsReferralRequired
        {
            get { return _IsReferralRequired; }
            set { _IsReferralRequired = value; }
        }

        public gloGeneralItem.gloItems CPTs
        {
            get { return _CPTs; }
            set
            {
                if (cptAssigned)
                {
                    if (_CPTs != null)
                    {
                        _CPTs.Dispose();
                        _CPTs = null;
                    }
                }
                _CPTs = value;
                cptAssigned = false;
            }
        }

    }

    #endregion " Referral CPT "

    #region " Anti Scrubber "

    public class gloAntiScrubber : IDisposable
    {
        enum CodeTypeFlag
        {
            None = 0,
            TOS = 1,
            POS = 2,
            Diagnosis = 3,
            Modifier = 4,
            CPT = 5
        }

        private Int64 _ClinicID = 0;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #region "Constructor & Destructor"

        private string _databaseconnectionstring = "";

        public gloAntiScrubber(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
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

        ~gloAntiScrubber()
        {
            Dispose(false);
        }

        #endregion

        public Int64 Add(AntiScrubber oAntiScrubber)
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            string _sqlQuery = "";
            object _nResult = null;
            Int64 _ScrubberID = 0;
            Int64 _replicationId = 0;

            try
            {

                oDB.Connect(false);
                if (oAntiScrubber.ScrubberID == 0)
                {
                    //_sqlQuery = "SELECT ISNULL(MAX(nScrubberID),0) + 1  FROM BL_ClaimScrubber"; ;
                    //_nResult = oDB.ExecuteScalar_Query(_sqlQuery);

                    _replicationId = oDB.GetPrefixTransactionID(0);

                    _sqlQuery = " IF EXISTS(SELECT nScrubberID FROM BL_ClaimScrubber WITH (NOLOCK) WHERE convert(varchar(18),nScrubberID) Like convert(varchar(18)," + _replicationId + ")+ '%') " +
                      " SELECT  isnull(max(nScrubberID),0)+1 FROM BL_ClaimScrubber WITH (NOLOCK) where convert(varchar(18),nScrubberID) Like convert(varchar(18)," + _replicationId + ")+ '%' " +
                      " ELSE " +
                      " SELECT convert(numeric(18,0), convert(varchar(18)," + _replicationId + ") + '001')  ";

                    _nResult = oDB.ExecuteScalar_Query(_sqlQuery);

                    if (_nResult != null && Convert.ToInt64(_nResult) > 0)
                    {
                        _ScrubberID = Convert.ToInt64(_nResult);
                    }
                    _sqlQuery = "";
                    //nScrubberID, sPOSCode, sTOSCode, sCode, sDescription, nCodeTypeFlag, nClinicID

                    //_sqlQuery = "";
                    //_sqlQuery = "DELETE FROM BL_ClaimScrubber WHERE sTOSCode = '" + oAntiScrubber.TOSCode.Replace("'", "''").Trim() + "'";
                    //oDB.Execute_Query(_sqlQuery);

                    //Add Scrubber Data
                    for (int i = 0; i < oAntiScrubber.CPTs.Count; i++)
                    {
                        _sqlQuery = "DELETE FROM BL_ClaimScrubber WHERE sPOSCode = '" + oAntiScrubber.POSCode.Replace("'", "''").Trim() + "' AND sTOSCode = '" + oAntiScrubber.TOSCode.Replace("'", "''").Trim() + "' AND sCode = '" + oAntiScrubber.CPTs[i].Code.Replace("'", "''").Trim() + "'";
                        oDB.Execute_Query(_sqlQuery);

                        oDBParameters.Clear();
                        oDBParameters.Add("@nScrubberID", _ScrubberID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oDBParameters.Add("@sPOSCode", oAntiScrubber.POSCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                        oDBParameters.Add("@sTOSCode", oAntiScrubber.TOSCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                        oDBParameters.Add("@sCode", oAntiScrubber.CPTs[i].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                        oDBParameters.Add("@sDescription", oAntiScrubber.CPTs[i].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                        oDBParameters.Add("@nCodeTypeFlag", CodeTypeFlag.CPT.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                        oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                        oDB.Execute("gsp_INSERT_BL_ClaimScrubber", oDBParameters);
                    }
                }
                else
                {
                    _sqlQuery = "";
                    _sqlQuery = "DELETE FROM BL_ClaimScrubber WHERE nScrubberID=" + oAntiScrubber.ScrubberID + "";
                    oDB.Execute_Query(_sqlQuery);


                    //Add Scrubber Data
                    for (int i = 0; i < oAntiScrubber.CPTs.Count; i++)
                    {
                        oDBParameters.Clear();
                        oDBParameters.Add("@nScrubberID", oAntiScrubber.ScrubberID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0),
                        oDBParameters.Add("@sPOSCode", oAntiScrubber.POSCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                        oDBParameters.Add("@sTOSCode", oAntiScrubber.TOSCode, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                        oDBParameters.Add("@sCode", oAntiScrubber.CPTs[i].Code, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(50),
                        oDBParameters.Add("@sDescription", oAntiScrubber.CPTs[i].Description, ParameterDirection.Input, SqlDbType.VarChar);//	varchar(255),
                        oDBParameters.Add("@nCodeTypeFlag", CodeTypeFlag.CPT.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);//	int,
                        oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);//	numeric(18, 0)

                        oDB.Execute("gsp_INSERT_BL_ClaimScrubber", oDBParameters);
                    }
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

            return _result;
        }

        public DataTable GetAntiScrubberData()
        {
            DataTable dtResult = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);

                string _sqlQuery =
                _sqlQuery = "SELECT nScrubberID, sTOSCode, sPOSCode, sCode, sDescription, nCodeTypeFlag, nClinicID FROM BL_ClaimScrubber WITH (NOLOCK) ";
                oDB.Retrive_Query(_sqlQuery, out dtResult);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

            return dtResult;
        }

        public Int64 DeleteScrubberData(string POS, string TOS, string CPTCode)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Int64 _result = 0;
            string strSQL = "";
            try
            {
                oDB.Connect(false);
                //nScrubberID, sPOSCode, sTOSCode, sCode, sDescription, nCodeTypeFlag, nClinicID
                if (CPTCode.Trim() != "")
                {
                    strSQL = "DELETE FROM BL_ClaimScrubber WHERE sPOSCode = '" + POS.Replace("'", "''").Trim() + "' AND sTOSCode = '" + TOS.Replace("'", "''").Trim() + "' AND sCode = '" + CPTCode.Replace("'", "''").Trim() + "'";
                    _result = oDB.Execute_Query(strSQL);
                }

                //if (TOSCode.Trim() != "")
                //{
                //    strSQL = "DELETE FROM BL_ClaimScrubber WHERE sTOSCode = " + TOSCode + "";
                //    _result = oDB.Execute_Query(strSQL);
                //}

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        internal AntiScrubber GetAntiScrubber(Int64 ScrubberID)
        {
            AntiScrubber oAntiScrubber = new AntiScrubber();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //      DataTable dtResult = null;
            try
            {

                oDB.Connect(false);

                DataTable dtCodes = null;
                //nScrubberID, sPOSCode, sTOSCode, sCode, sDescription, nCodeTypeFlag, nClinicID
                string _sqlQuery = "";
                _sqlQuery = " SELECT nScrubberID, sTOSCode, sPOSCode, sCode, sDescription, nCodeTypeFlag, nClinicID FROM BL_ClaimScrubber WITH (NOLOCK) WHERE nScrubberID=" + ScrubberID + " ";
                oDB.Retrive_Query(_sqlQuery, out dtCodes);
                if (dtCodes != null && dtCodes.Rows.Count > 0)
                {
                    for (int _index = 0; _index < dtCodes.Rows.Count; _index++)
                    {
                        oAntiScrubber.ScrubberID = Convert.ToInt64(dtCodes.Rows[_index]["nScrubberID"]);
                        oAntiScrubber.POSCode = Convert.ToString(dtCodes.Rows[_index]["sPOSCode"]);
                        oAntiScrubber.TOSCode = Convert.ToString(dtCodes.Rows[_index]["sTOSCode"]);
                        oAntiScrubber.ClinicID = Convert.ToInt64(dtCodes.Rows[_index]["nClinicID"]);
                        oAntiScrubber.CPTs.Add(0, Convert.ToString(dtCodes.Rows[_index]["sCode"]), Convert.ToString(dtCodes.Rows[_index]["sDescription"]));
                    }
                }
                if (dtCodes != null)
                {
                    dtCodes.Dispose();
                    dtCodes = null;
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

            return oAntiScrubber;
        }

    }

    public class AntiScrubber : IDisposable
    {

        #region "Constructor & Destructor"

        public AntiScrubber()
        {
            _CPTs = new gloGeneralItem.gloItems();
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
                    if (cptAssigned)
                    {
                        if (_CPTs != null)
                        {
                            _CPTs.Dispose();
                            _CPTs = null;
                        }
                    }
                }
            }
            disposed = true;
        }

        ~AntiScrubber()
        {
            Dispose(false);
        }

        #endregion

        private Int64 _nScrubberID = 0;
        private string _sTOSCode = "";
        private string _sTOSDesc = "";
        private string _sPOSCode = "";
        private string _sPOSDesc = "";
        private gloGeneralItem.gloItems _CPTs = null;
        private gloGeneralItem.gloItems _Modifiers = null;
        private gloGeneralItem.gloItems _Diagnosis = null;
        private Int64 _ClinicID = 0;
        private bool cptAssigned = true;

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public Int64 ScrubberID
        {
            get { return _nScrubberID; }
            set { _nScrubberID = value; }
        }

        public string TOSCode
        {
            get { return _sTOSCode; }
            set { _sTOSCode = value; }
        }

        public string TOSDesc
        {
            get { return _sTOSDesc; }
            set { _sTOSDesc = value; }
        }

        public string POSCode
        {
            get { return _sPOSCode; }
            set { _sPOSCode = value; }
        }

        public string POSDesc
        {
            get { return _sPOSDesc; }
            set { _sPOSDesc = value; }
        }

        public gloGeneralItem.gloItems CPTs
        {
            get { return _CPTs; }
            set
            {
                if (cptAssigned)
                {
                    if (_CPTs != null)
                    {
                        _CPTs.Dispose();
                        _CPTs = null;
                    }
                }
                _CPTs = value;
                cptAssigned = false;
            }
        }

        public gloGeneralItem.gloItems Modifiers
        {
            get { return _Modifiers; }
            set { _Modifiers = value; }
        }

        public gloGeneralItem.gloItems Diagnosis
        {
            get { return _Diagnosis; }
            set { _Diagnosis = value; }
        }

    }

    #endregion " Anti Scrubber "
    #region " Qualifier "
    public class Qualifier
    {
        private Int64 _QualifierTypeId;
        private String _QualifierCode;
        private String _QualifierDescription;
        private string _databaseconnectionstring = "";
        private Boolean _IsActive = true;
        private Int64 _ClinicID = 0;
        enum RecordType
        {
            System = 1,
            Default = 2,
            User = 3

        }

        public Qualifier(string _databaseconnectionstring)
        {
            this._databaseconnectionstring = _databaseconnectionstring;
        }
        public Int64 QualifierTypeID
        {
            get { return _QualifierTypeId; }
            set { _QualifierTypeId = value; }
        }
        public String QualifierCode
        {
            get { return _QualifierCode; }
            set { _QualifierCode = value; }
        }
        public string QualifierDescription
        {
            get { return _QualifierDescription; }
            set { _QualifierDescription = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 Add()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameter oDBParameter;
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object _intresult = 0;
            oDB.Connect(false);

            oDBParameters.Add("@nQualifierMstID", this._QualifierTypeId, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
            oDBParameters.Add("@sCode", this._QualifierCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
            oDBParameters.Add("@sDescription", this._QualifierDescription, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
            oDBParameters.Add("@nRecordType", RecordType.User.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
            oDBParameters.Add("@bIsActive", _IsActive, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
            oDBParameters.Add("@nClinicID", this._ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
            _result = oDB.Execute("BL_INUP_IDQualifier", oDBParameters, out _intresult);

            if (_intresult != null)
            {
                if (_intresult.ToString().Trim() != "")
                {
                    if (Convert.ToInt64(_intresult) > 0)
                    {
                        _result = Convert.ToInt64(_intresult.ToString());
                    }
                }
            }

            if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            return _result;

        }

        public Boolean CanDelete(Int64 Id)
        {
            bool Result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //  DataTable dt = null;
            // object value = null;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "";
                SqlQuery = "Select count(*) from BL_IDQualifier_Association WITH (NOLOCK) where nQualifierMstID= " + Id;
                oDB.Connect(false);
                object _intResult = null;
                _intResult = oDB.ExecuteScalar_Query(SqlQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            Result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return Result;
        }
        public DataTable GetQualifierCode(Int64 Id)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtRevenueCode = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "select ISNULL(sCode,'') AS sCode,ISNULL(sDescription,'') AS sDescription from BL_IDQualifier WITH (NOLOCK) where nQualifierMstID= " + Id.ToString();
                oDB.Retrive_Query(strQuery, out dtRevenueCode);
                return dtRevenueCode;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                //if(dtRevenueCode != null)
                //{
                //dtRevenueCode.Dispose();
                //}

            }
        }
        public void DeleteQualifier(Int64 _QualifierID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "Delete from BL_IDQualifier where nQualifierMstID= " + _QualifierID.ToString();
                oDB.Execute_Query(strQuery);

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }

            }
        }

    }
    #endregion

    #region " Qualifier Association "
    public class QualifierAssociation
    {
        private Int64 _QualifiermstId;
        private Int64 _QualifierAssociationId;
        //   private Int64 _QualifierId;
        private String _QualifierAssociationCode;
        private String _QualifierDescription;
        private string _databaseconnectionstring = "";
        private Boolean _IsActive = true;
        private Int64 _ClinicID = 0;
        enum RecordType
        {
            System = 1,
            Default = 2,
            User = 3

        }

        public QualifierAssociation(string _databaseconnectionstring)
        {
            this._databaseconnectionstring = _databaseconnectionstring;
        }
        public Int64 QualifierAssociationID
        {
            get { return _QualifierAssociationId; }
            set { _QualifierAssociationId = value; }
        }
        public Int64 QualifierMasterID
        {
            get { return _QualifiermstId; }
            set { _QualifiermstId = value; }
        }
        public String QualifierCode
        {
            get { return _QualifierAssociationCode; }
            set { _QualifierAssociationCode = value; }
        }
        public string QualifierDescription
        {
            get { return _QualifierDescription; }
            set { _QualifierDescription = value; }
        }
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 Add()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameter oDBParameter;
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            object _intresult = 0;
            oDB.Connect(false);

            oDBParameters.Add("@nQualifierID", this._QualifierAssociationId, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
            oDBParameters.Add("@nQualifierMstID", this._QualifiermstId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
            oDBParameters.Add("@sCode", this._QualifierAssociationCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
            oDBParameters.Add("@sAdditionalDescription", this._QualifierDescription, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
            oDBParameters.Add("@bIsSystem", false, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
            oDBParameters.Add("@bIsActive", _IsActive, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
            oDBParameters.Add("@nClinicID", this._ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
            _result = oDB.Execute("BL_INUP_IDQualifier_Association", oDBParameters, out _intresult);

            if (_intresult != null)
            {
                if (_intresult.ToString().Trim() != "")
                {
                    if (Convert.ToInt64(_intresult) > 0)
                    {
                        _result = Convert.ToInt64(_intresult.ToString());
                    }
                }
            }

            if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            return _result;
        }
        public Boolean CanDelete(Int64 Id)
        {
            bool Result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //DataTable dt = null;
            //object value = null;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "";
                SqlQuery = "Select count(id) as id from  (Select count(*) as id from Provider_ID_Qualifiers WITH (NOLOCK) where nQualifierID=" + Id + " Union  Select count(*) as id from  Clinic_ID_Qualifiers WITH (NOLOCK) where nQualifierID=" + Id + " Union  Select count(*) as id  from BL_Facility_ID_Qualifiers WITH (NOLOCK) where nQualifierID=" + Id + " Union  Select count(*) as id  from dbo.Provider_ID_CompanyQualifiers WITH (NOLOCK) where nQualifierID= " + Id + "  Union Select count(*) as id  from BL_AlternateID_Settings WITH (NOLOCK) where nQualifierID=" + Id + ") ss where id>0 ";
                oDB.Connect(false);
                object _intResult = null;
                _intResult = oDB.ExecuteScalar_Query(SqlQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            Result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return Result;
        }
        public DataTable GetQualifierCode(Int64 Id)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtQualifierCode = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "select ISNULL(sCode,'') AS sCode,ISNULL(sAdditionalDescription,'') AS sDescription , ISNULL(nQualifierID,0) AS nQualifierID from BL_IDQualifier_Association WITH (NOLOCK) where nQualifierID= " + Id.ToString();
                oDB.Retrive_Query(strQuery, out dtQualifierCode);
                return dtQualifierCode;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
                //   dtQualifierCode.Dispose();

            }
        }
        public void DeleteQualifier(Int64 _QualifierID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "Delete from BL_IDQualifier_Association where nQualifierID= " + _QualifierID.ToString();
                oDB.Execute_Query(strQuery);

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

    }
    #endregion

    #region "Condition Codes"

    public class ConditionCodes
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ConditionCodeID = 0;

        private string _sCode = "";
        private string _sDescription = "";

        private bool _bIsSystem = false;




        public ConditionCodes(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

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

        ~ConditionCodes()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        public Int64 ConditionCodeID
        {
            get { return _ConditionCodeID; }
            set { _ConditionCodeID = value; }
        }

        public string sCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        public bool IsSystem
        {
            get { return _bIsSystem; }
            set { _bIsSystem = value; }
        }

        public string sDescription
        {
            get { return _sDescription; }
            set { _sDescription = value; }
        }

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = String.Empty;

        public ConditionCodes()
        {
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
        }
        #endregion

        #region" Methods"

        public Int64 AddConditionCode(Int64 nID, string sCode, string sDesc, bool IsBlocked)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);
                oParameters.Add("@nID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDescription", sDesc, ParameterDirection.Input, SqlDbType.VarChar, 1000);
                oParameters.Add("@IsActive", IsBlocked, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@nUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.Bit, 100);


                oDB.Execute("UB04_INUP_ConditionCode", oParameters, out _oResult);


                if (oParameters["@nID"].Value != null)
                { return (Int64)oParameters["@nID"].Value; }


                return 0;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }



        public bool DeleteConditionCode(Int64 ID, string DatabaseConnectionString)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from UB_ConditionCodes where nConditionID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }


        public bool IsSystemRecord(Int64 Id, string databaseconnectionstring)
        {
            bool Result = true;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(databaseconnectionstring);
            //      DataTable dt = new DataTable();
            object value = null;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "";
                SqlQuery = "SELECT ISNULL(IsSystem,'false') AS IsSystem FROM UB_ConditionCodes WITH (NOLOCK)  WHERE [nConditionID] = " + Id;
                value = oDB.ExecuteScalar_Query(SqlQuery);
                if (value != null && Convert.ToString(value) != "")
                {
                    if (Convert.ToBoolean(value) == true)
                    {
                        Result = false;
                    }
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return Result;
        }



        public bool IsExistsConditionCode(Int64 nID, string Code)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                strQuery = " select count(nConditionID) from UB_ConditionCodes WITH (NOLOCK) where sConditionCode='" + Code.Replace("'", "''") + "'  and nConditionID <> '" + nID + "' ";
                //

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }




        #endregion
    }
    #endregion

    #region "Value Codes"

    public class ValueCodes
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ValueCodeID = 0;

        private string _sCode = "";
        private string _sDescription = "";

        private bool _bIsSystem = false;




        public ValueCodes(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

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

        ~ValueCodes()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        public Int64 ValueCodeID
        {
            get { return _ValueCodeID; }
            set { _ValueCodeID = value; }
        }

        public string sCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        public bool IsSystem
        {
            get { return _bIsSystem; }
            set { _bIsSystem = value; }
        }

        public string sDescription
        {
            get { return _sDescription; }
            set { _sDescription = value; }
        }

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = String.Empty;

        public ValueCodes()
        {
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
        }
        #endregion

        #region" Methods"

        public Int64 AddValueCode(Int64 nID, string sCode, string sDesc, bool IsBlocked)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);
                oParameters.Add("@nID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDescription", sDesc, ParameterDirection.Input, SqlDbType.VarChar, 1000);
                oParameters.Add("@IsActive", IsBlocked, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@nUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("UB04_INUP_ValueCode", oParameters, out _oResult);


                if (oParameters["@nID"].Value != null)
                { return (Int64)oParameters["@nID"].Value; }


                return 0;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }



        public bool DeleteValueCode(Int64 ID, string DatabaseConnectionString)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from UB_ValueCodes where nValueID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }


        public bool IsSystemRecord(Int64 Id, string databaseconnectionstring)
        {
            bool Result = true;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(databaseconnectionstring);
            //  DataTable dt = new DataTable();
            object value = null;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "";
                SqlQuery = "SELECT ISNULL(IsSystem,'false') AS IsSystem FROM UB_ValueCodes WITH (NOLOCK)  WHERE [nValueID] = " + Id;
                value = oDB.ExecuteScalar_Query(SqlQuery);
                if (value != null && Convert.ToString(value) != "")
                {
                    if (Convert.ToBoolean(value) == true)
                    {
                        Result = false;
                    }
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return Result;
        }



        public bool IsExistsValueCode(Int64 nID, string Code)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                strQuery = " select count(nValueID) from UB_ValueCodes WITH (NOLOCK) where sValueCode='" + Code.Replace("'", "''") + "'  and nValueID!='" + nID + "' ";
                //

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }




        #endregion
    }
    #endregion

    #region "Occurrence Codes"

    public class OccurrenceCodes
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _OccurrenceCodeID = 0;

        private string _sCode = "";
        private string _sDescription = "";

        private bool _bIsSystem = false;




        public OccurrenceCodes(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

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

        ~OccurrenceCodes()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        public Int64 OccurrenceCodeID
        {
            get { return _OccurrenceCodeID; }
            set { _OccurrenceCodeID = value; }
        }

        public string sCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        public bool IsSystem
        {
            get { return _bIsSystem; }
            set { _bIsSystem = value; }
        }

        public string sDescription
        {
            get { return _sDescription; }
            set { _sDescription = value; }
        }

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = String.Empty;

        public OccurrenceCodes()
        {
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
        }
        #endregion

        #region" Methods"

        public Int64 AddOccurrenceCode(Int64 nID, string sCode, string sDesc, bool IsBlocked)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);
                oParameters.Add("@nID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDescription", sDesc, ParameterDirection.Input, SqlDbType.VarChar, 1000);
                oParameters.Add("@IsActive", IsBlocked, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@nUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);


                oDB.Execute("UB04_INUP_OccurrenceCode", oParameters, out _oResult);
                if (oParameters["@nID"].Value != null)
                { return (Int64)oParameters["@nID"].Value; }
                return 0;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }



        public bool DeleteOccurrenceCode(Int64 ID, string DatabaseConnectionString)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from UB_OccurrenceCodes where nOccurrenceID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }


        public bool IsSystemRecord(Int64 Id, string databaseconnectionstring)
        {
            bool Result = true;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(databaseconnectionstring);
            //   DataTable dt = new DataTable();
            object value = null;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "";
                SqlQuery = "SELECT ISNULL(IsSystem,'false') AS IsSystem FROM UB_OccurrenceCodes WITH (NOLOCK)  WHERE [nOccurrenceID] = " + Id;
                value = oDB.ExecuteScalar_Query(SqlQuery);
                if (value != null && Convert.ToString(value) != "")
                {
                    if (Convert.ToBoolean(value) == true)
                    {
                        Result = false;
                    }
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return Result;
        }



        public bool IsExistsOccurrenceCode(Int64 nID, string Code)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                strQuery = " select count(nOccurrenceID) from UB_OccurrenceCodes WITH (NOLOCK) where sOccurrenceCode='" + Code.Replace("'", "''") + "'  and nOccurrenceID <> '" + nID + "' ";
                //

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }




        #endregion
    }
    #endregion

    #region "OccurrenceSpan Codes"

    public class OccurrenceSpanCodes
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _OccurrenceSpanCodeID = 0;

        private string _sCode = "";
        private string _sDescription = "";

        private bool _bIsSystem = false;




        public OccurrenceSpanCodes(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

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

        ~OccurrenceSpanCodes()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        public Int64 OccurrenceSpanCodeID
        {
            get { return _OccurrenceSpanCodeID; }
            set { _OccurrenceSpanCodeID = value; }
        }

        public string sCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        public bool IsSystem
        {
            get { return _bIsSystem; }
            set { _bIsSystem = value; }
        }

        public string sDescription
        {
            get { return _sDescription; }
            set { _sDescription = value; }
        }

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = String.Empty;

        public OccurrenceSpanCodes()
        {
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
        }
        #endregion

        #region" Methods"

        public Int64 AddOccurrenceSpanCode(Int64 nID, string sCode, string sDesc, bool IsBlocked)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);
                oParameters.Add("@nID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDescription", sDesc, ParameterDirection.Input, SqlDbType.VarChar, 1000);
                oParameters.Add("@IsActive", IsBlocked, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@nUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("UB04_INUP_OccurrenceSpanCode", oParameters, out _oResult);
                if (oParameters["@nID"].Value != null)
                { return (Int64)oParameters["@nID"].Value; }
                return 0;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }



        public bool DeleteOccurrenceSpanCode(Int64 ID, string DatabaseConnectionString)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from UB_OccurrenceSpanCodes where nOccurrenceSpanID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }


        public bool IsSystemRecord(Int64 Id, string databaseconnectionstring)
        {
            bool Result = true;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(databaseconnectionstring);
            //    DataTable dt = new DataTable();
            object value = null;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "";
                SqlQuery = "SELECT ISNULL(IsSystem,'false') AS IsSystem FROM UB_OccurrenceSpanCodes WITH (NOLOCK)  WHERE [nOccurrenceSpanID] = " + Id;
                value = oDB.ExecuteScalar_Query(SqlQuery);
                if (value != null && Convert.ToString(value) != "")
                {
                    if (Convert.ToBoolean(value) == true)
                    {
                        Result = false;
                    }
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return Result;
        }



        public bool IsExistsOccurrenceSpanCode(Int64 nID, string Code)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                strQuery = " select count(nOccurrenceSpanID) from UB_OccurrenceSpanCodes WITH (NOLOCK) where sOccurrenceSpanCode='" + Code.Replace("'", "''") + "'  and nOccurrenceSpanID <> '" + nID + "' ";
                //

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }




        #endregion
    }
    #endregion

    #region "Reporting Category"

    public class ReportingCategory
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ReportingCategoryID = 0;

        private string _sCode = "";
        private string _sDescription = "";

        private bool _bIsSystem = false;




        public ReportingCategory(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

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

        ~ReportingCategory()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        public Int64 ReportingCategoryID
        {
            get { return _ReportingCategoryID; }
            set { _ReportingCategoryID = value; }
        }

        public string sCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        public bool IsSystem
        {
            get { return _bIsSystem; }
            set { _bIsSystem = value; }
        }

        public string sDescription
        {
            get { return _sDescription; }
            set { _sDescription = value; }
        }

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = String.Empty;

        public ReportingCategory()
        {
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
        }
        #endregion

        #region" Methods"

        public Int64 AddReportingCategory()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);
                oParameters.Add("@nID", ReportingCategoryID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDescription", sDescription, ParameterDirection.Input, SqlDbType.VarChar, 1000);
                oParameters.Add("@bIsblocked", 0, ParameterDirection.Input, SqlDbType.Bit, 100);


                oDB.Execute("Case_INUP_ReportingCategory", oParameters, out _oResult);


                if (oParameters["@nID"].Value != null)
                { return (Int64)oParameters["@nID"].Value; }


                return 0;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }

        public DataTable GetReportingCategoryCode(Int64 Id)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtRevenueCode = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "select ISNULL(sCode,'') AS sCode,ISNULL(sDescription,'') AS sDescription from Cases_ReportingCategory WITH (NOLOCK) where nID= " + Id.ToString();
                oDB.Retrive_Query(strQuery, out dtRevenueCode);
                return dtRevenueCode;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }


            }
        }

        public bool DeleteReportingCategory(Int64 ID, string DatabaseConnectionString)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "Update Cases_ReportingCategory  set  bIsblocked=1 where nID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }





        public bool IsExistsReportingCategory(Int64 nID, string Code)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                strQuery = " select count(nID) from Cases_ReportingCategory WITH (NOLOCK) where sCode='" + Code.Replace("'", "''") + "'  and nID!='" + nID + "' and bIsblocked=1 ";
                //

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }




        #endregion
    }
    #endregion

    #region "Reason Codes"

    public class ReasonCodes
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ReasonCodeID = 0;
        private Int64 _ClinicID = 0;
        private string _sCode = "";
        private string _CodeTypeDesc = "";
        private bool _bIsSystem = false;
        private bool _bIsBlock = false;
        private Int64 _nActionID = 0;


        public ReasonCodes(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
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

        ~ReasonCodes()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        public Int64 ReasonCodeID
        {
            get { return _ReasonCodeID; }
            set { _ReasonCodeID = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public string sCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        public bool IsSystem
        {
            get { return _bIsSystem; }
            set { _bIsSystem = value; }
        }

        public bool IsBlock
        {
            get { return _bIsBlock; }
            set { _bIsBlock = value; }
        }

        public Int64 nActionID
        {
            get { return _nActionID; }
            set { _nActionID = value; }
        }

        public string sDescription
        {
            get { return _CodeTypeDesc; }
            set { _CodeTypeDesc = value; }
        }

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = String.Empty;

        public ReasonCodes()
        {
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
        }
        #endregion

        #region" Methods"

        public Int64 AddModifyGroupCodes(Int64 nID, string sCode, string sDesc, bool IsBlocked)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);
                oParameters.Add("@nID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDescription", sDesc, ParameterDirection.Input, SqlDbType.VarChar, 1000);
                oParameters.Add("@isBlocked", IsBlocked, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@ClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("BL_INUP_GroupCode", oParameters, out _oResult);

                return 0;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }
        public Int64 AddModify(Int64 nID, string sCode, string sDesc, string sGroupCode, string sGroupCodeDesc, bool IsBlocked, bool IsSystem)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);

                oParameters.Add("@nReasonID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sCode", sCode, ParameterDirection.Input, SqlDbType.VarChar, 10);
                oParameters.Add("@sDescription", sDesc, ParameterDirection.Input, SqlDbType.VarChar, 1000);
                //lines added by dipak 20091209 as new parameters to store procedure
                oParameters.Add("@sGroupCode", sGroupCode, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@sGroupCodeDescription", sGroupCodeDesc, ParameterDirection.Input, SqlDbType.VarChar, 1000);
                //end add by dipak 
                oParameters.Add("@bIsSystem", IsSystem, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@bIsBlock", IsBlocked, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@nActionID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("BL_INUP_ReasonCodesMST", oParameters, out _oResult);

                return Convert.ToInt64(_oResult);
                //return 0;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }
        //Code Added by Mayuri:20091106
        //For Action Codes
        public Int64 AddModifyActionCodes(Int64 nID, string sCode, string sDesc, bool IsBlocked, bool IsSystem)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);

                oParameters.Add("@nActionCodeID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDescription", sDesc, ParameterDirection.Input, SqlDbType.VarChar, 100);
                oParameters.Add("@bIsSystem", IsSystem, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@bIsBlock", IsBlocked, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@nActionID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("BL_INUP_ActionCodesMST", oParameters, out _oResult);

                //return Convert.ToInt64(_oResult);
                return 0;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }
        //End Code Added by Mayuri:20091106
        //
        public bool DeleteActionCode(Int64 ID, string DatabaseConnectionString)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_ActionCodes_MST where nActionCodeID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }
        //

        public bool DeleteReasonCode(Int64 ID, string DatabaseConnectionString)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_ReasonCodes_MST where nReasonID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }
        //GroupCode parameter to function added by dipak 20091210
        public bool IsExists(Int64 nID, string Code, string Name, string GroupCode)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                //strQuery = " select count(nPOSID) from BL_POS_MST where sPOSCode='" + Code + "' OR sPOSName='" + Name + "' ";
                //
                //strQuery = " select count(nPOSID) from BL_POS_MST where (sPOSCode='" + Code + "' OR sPOSName='" + Name + "') AND nClinicID="+_nClinicID+" ";

                //query string commented and modified by dipak 20091210 as table BL_ReasonCodes_MST 's fields altered 
                //strQuery = " select count(nReasonID) from BL_ReasonCodes_MST where sCode='" + Code.Replace("'", "''") + "' AND nClinicID=" + _ClinicID + " and nReasonID!='" + nID + "' ";

                strQuery = " select count(nReasonID) from BL_ReasonCodes_MST WITH (NOLOCK) where sCode='" + Code.Replace("'", "''") + "' AND sGroupCode='" + GroupCode.Replace("'", "''") + "' AND nClinicID=" + _ClinicID + " and nReasonID!='" + nID + "' ";
                //

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }
        //Code Added by Mayuri:20091106 for Action Codes duplication check
        public bool IsExistsActionCode(Int64 nID, string Code, string Name)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                //strQuery = " select count(nPOSID) from BL_POS_MST where sPOSCode='" + Code + "' OR sPOSName='" + Name + "' ";
                //
                //strQuery = " select count(nPOSID) from BL_POS_MST where (sPOSCode='" + Code + "' OR sPOSName='" + Name + "') AND nClinicID="+_nClinicID+" ";
                strQuery = " select count(nActionCodeID) from BL_ActionCodes_MST WITH (NOLOCK) where sCode='" + Code.Replace("'", "''") + "' AND nClinicID=" + _ClinicID + " and nActionCodeID!='" + nID + "' ";
                //

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }

        //End Code Added by Mayuri:20091106
        //
        public bool CanDeleteCode(Int64 ActionCodeId, string databaseconnectionstring)
        {
            bool Result = true;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(databaseconnectionstring);
            //   DataTable dt = new DataTable();
            object value = null;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "";
                SqlQuery = "SELECT ISNULL(bIsSystem,'false') AS bIsSystem FROM BL_ActionCodes_MST WITH (NOLOCK)  WHERE [nActionCodeID] = " + ActionCodeId;
                value = oDB.ExecuteScalar_Query(SqlQuery);
                if (value != null && Convert.ToString(value) != "")
                {
                    if (Convert.ToBoolean(value) == true)
                    {
                        Result = false;
                    }
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return Result;
        }


        public bool CanDelete(Int64 ReasonId, string databaseconnectionstring)
        {
            bool Result = true;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(databaseconnectionstring);
            //    DataTable dt = new DataTable();
            object value = null;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "";
                SqlQuery = "SELECT ISNULL(bIsSystem,'false') AS bIsSystem FROM BL_ReasonCodes_MST WITH (NOLOCK)  WHERE [nReasonID] = " + ReasonId;
                value = oDB.ExecuteScalar_Query(SqlQuery);
                if (value != null && Convert.ToString(value) != "")
                {
                    if (Convert.ToBoolean(value) == true)
                    {
                        Result = false;
                    }
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return Result;
        }
        public bool IsExistsGroupCode(Int64 nID, string Code, string sDescription)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                //strQuery = " select count(nPOSID) from BL_POS_MST where sPOSCode='" + Code + "' OR sPOSName='" + Name + "' ";
                //
                //strQuery = " select count(nPOSID) from BL_POS_MST where (sPOSCode='" + Code + "' OR sPOSName='" + Name + "') AND nClinicID="+_nClinicID+" ";

                //query string commented and modified by dipak 20091210 as table BL_ReasonCodes_MST 's fields altered 
                //strQuery = " select count(nReasonID) from BL_ReasonCodes_MST where sCode='" + Code.Replace("'", "''") + "' AND nClinicID=" + _ClinicID + " and nReasonID!='" + nID + "' ";

                strQuery = " select count(nID) from BL_Reason_GroupCode_MST WITH (NOLOCK) where sCode='" + Code.Replace("'", "''") + "' AND sDescription='" + sDescription.Replace("'", "''") + "' AND nClinicID=" + _ClinicID + " and nID!='" + nID + "' ";
                //

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }


        //20091231  Mahesh Nawal   For checking the group code is System code or not. 
        public bool CanDeleteGroupCode(Int64 nID, string databaseconnectionstring)
        {
            bool Result = true;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(databaseconnectionstring);
            //     DataTable dt = new DataTable();
            object value = null;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "";
                SqlQuery = "SELECT ISNULL(bIsSystem,'false') AS bIsSystem FROM BL_Reason_GroupCode_MST WITH (NOLOCK)  WHERE [nID] = " + nID;
                value = oDB.ExecuteScalar_Query(SqlQuery);
                if (value != null && Convert.ToString(value) != "")
                {
                    if (Convert.ToBoolean(value) == true)
                    {
                        Result = false;
                    }
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return Result;
        }

        public DataTable GetDelayReasonCodes()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtDelayReasonCodes = null;
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                strQuery = "SELECT nDelayReasonID,sDelayReasonCode,sDelayReasonCode + ' - ' + sDelayCodeDesc as sDelayCodeDesc ,bIsActive "
                           + " FROM BL_DelayReasonCode_MST WITH (NOLOCK) WHERE bIsActive = 1 AND nClinicID = " + this.ClinicID + " order by convert(numeric, sDelayReasonCode) asc";

                oDB.Retrive_Query(strQuery, out _dtDelayReasonCodes);

                if (_dtDelayReasonCodes != null && _dtDelayReasonCodes.Rows.Count > 0)
                {
                    return _dtDelayReasonCodes;
                }
                oDB.Disconnect();
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

        }

        #endregion
    }
    #endregion


    #region "Remark Codes"

    public class RemarkCodes
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _RemarkCodeID = 0;
        private Int64 _ClinicID = 0;
        private string _sCode = "";
        private string _CodeTypeDesc = "";
        private bool _bIsSystem = false;
        private bool _bIsBlock = false;
        //   private Int64 _nActionID = 0;


        public RemarkCodes(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
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

        ~RemarkCodes()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        public Int64 RemarkCodeID
        {
            get { return _RemarkCodeID; }
            set { _RemarkCodeID = value; }
        }

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public string sCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        public bool IsSystem
        {
            get { return _bIsSystem; }
            set { _bIsSystem = value; }
        }

        public bool IsBlock
        {
            get { return _bIsBlock; }
            set { _bIsBlock = value; }
        }


        public string sDescription
        {
            get { return _CodeTypeDesc; }
            set { _CodeTypeDesc = value; }
        }

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = String.Empty;

        public RemarkCodes()
        {
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
        }
        #endregion

        #region" Methods"


        public Int64 AddModify(Int64 nID, string sCode, string sDesc, bool IsBlocked, bool IsSystem)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {

                oDB.Connect(false);
                oParameters.Add("@nRemarkID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDescription", sDesc, ParameterDirection.Input, SqlDbType.VarChar, 1000);
                oParameters.Add("@bIsSystem", IsSystem, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@bIsBlock", IsBlocked, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("BL_INUP_RemarkCodesMST", oParameters, out _oResult);
                return 0;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }

        public bool DeleteRemarkCode(Int64 ID, string DatabaseConnectionString)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_RemarkCodes_MST where nRemarkID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        public bool IsExists(Int64 nID, string Code, string Name)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                strQuery = " select count(nRemarkID) from BL_RemarkCodes_MST WITH (NOLOCK) where sRemarkCode='" + Code.Replace("'", "''") + "'  AND nClinicID=" + _ClinicID + " and nRemarkID!='" + nID + "' ";
                object _intResult = null;
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }

        public bool CanDelete(Int64 nRemarkId, string databaseconnectionstring)
        {
            bool Result = true;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(databaseconnectionstring);

            object value = null;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "";
                SqlQuery = "SELECT ISNULL(bIsSystem,'false') AS bIsSystem FROM BL_RemarkCodes_MST WITH (NOLOCK)  WHERE [nRemarkID] = " + nRemarkId;
                value = oDB.ExecuteScalar_Query(SqlQuery);
                if (value != null && Convert.ToString(value) != "")
                {
                    if (Convert.ToBoolean(value) == true)
                    {
                        Result = false;
                    }
                }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return Result;
        }


        #endregion
    }
    #endregion


    #region "Revenue Code"

    public class CLsBL_RevenueCode
    {
        #region " Declarations "

        Int64 _ClinicID = 0;
        string _messageBoxCaption = "";
        string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


        #endregion " Declarations "

        #region "Constructor & Destructor"

        public CLsBL_RevenueCode(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }

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

        ~CLsBL_RevenueCode()
        {
            Dispose(false);
        }

        #endregion

        #region " Public & Private Methods "

        public DataTable GetRevenueCode(Int64 Id, Int64 CptId, bool InactiveStatus)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtRevenueCode = null;
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                if (Id > 0)
                {
                    strQuery = "select ISNULL(nRevenueID,0) AS nID,ISNULL(sRevenueCode,'') AS nCode,ISNULL(sDescription,'') AS sDescription,CASE ISNULL(bIsActive,0) WHEN 0 THEN 'Inactive' ELSE 'Active' END AS bIsActive,ISNULL(sRevenueCode,'')+' - '+ISNULL(sDescription,'') as RevcodeDescription from BL_UB_RevenueCode_MST WITH (NOLOCK) where nRevenueID= " + Id.ToString();

                }
                else
                {
                    if (InactiveStatus == true)
                    {
                        strQuery = "SELECT nID,nCode,sDescription,bIsActive,RevcodeDescription from " +
                                   "(select ISNULL(nRevenueID,0) AS nID,ISNULL(sRevenueCode,'') AS nCode,ISNULL(sDescription,'') AS sDescription,CASE ISNULL(bIsActive,0) WHEN 0 THEN 'Inactive' ELSE 'Active' END AS bIsActive,ISNULL(BL_UB_RevenueCode_MST.sRevenueCode,'')+' - '+ISNULL(BL_UB_RevenueCode_MST.sDescription,'') as RevcodeDescription from BL_UB_RevenueCode_MST WITH (NOLOCK) " +
                                   " union select ISNULL(nRevenueID,0) AS nID,ISNULL(BL_UB_RevenueCode_MST.sRevenueCode,'') AS nCode,ISNULL(BL_UB_RevenueCode_MST.sDescription,'') AS sDescription,CASE ISNULL(bIsActive,0) " +
                                   " WHEN 0 THEN 'Inactive' ELSE 'Active' END AS bIsActive,ISNULL(BL_UB_RevenueCode_MST.sRevenueCode,'')+' - '+ISNULL(BL_UB_RevenueCode_MST.sDescription,'') as RevcodeDescription from cpt_mst WITH (NOLOCK) inner join BL_UB_RevenueCode_MST WITH (NOLOCK) on BL_UB_RevenueCode_MST.sRevenueCode=cpt_mst.sRevenueCode " +
                                   " where nCPTID=" + CptId + ") temp order by nCode";
                    }
                    else
                    {
                        strQuery = "SELECT nID,nCode,sDescription,bIsActive,RevcodeDescription from " +
                                   "(select ISNULL(nRevenueID,0) AS nID,ISNULL(sRevenueCode,'') AS nCode,ISNULL(sDescription,'') AS sDescription,CASE ISNULL(bIsActive,0) WHEN 0 THEN 'Inactive' ELSE 'Active' END AS bIsActive,ISNULL(BL_UB_RevenueCode_MST.sRevenueCode,'')+' - '+ISNULL(BL_UB_RevenueCode_MST.sDescription,'') as RevcodeDescription from BL_UB_RevenueCode_MST WITH (NOLOCK) " +
                                   " where ISNULL(bIsActive,0) =1 " +
                                   " union select ISNULL(nRevenueID,0) AS nID,ISNULL(BL_UB_RevenueCode_MST.sRevenueCode,'') AS nCode,ISNULL(BL_UB_RevenueCode_MST.sDescription,'') AS sDescription,CASE ISNULL(bIsActive,0) " +
                                   " WHEN 0 THEN 'Inactive' ELSE 'Active' END AS bIsActive,ISNULL(BL_UB_RevenueCode_MST.sRevenueCode,'')+' - '+ISNULL(BL_UB_RevenueCode_MST.sDescription,'') as RevcodeDescription from cpt_mst WITH (NOLOCK) inner join BL_UB_RevenueCode_MST WITH (NOLOCK) on BL_UB_RevenueCode_MST.sRevenueCode=cpt_mst.sRevenueCode " +
                                   " where nCPTID=" + CptId + ") temp order by nCode";
                    }
                }
                oDB.Retrive_Query(strQuery, out dtRevenueCode);
                return dtRevenueCode;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }

            }
        }

        public Int64 AddModifyRevenueCode(Int64 Id, string sCode, string sDesc, bool bIsActive)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);

                oParameters.Add("@nRevenueID", Id, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sRevenueCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDescription", sDesc, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@bIsActive", bIsActive, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("BL_INUP_UB_RevenueCode", oParameters, out _oResult);

                return Convert.ToInt64(_oResult);

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }

        public bool IsExistsRevenueCode(Int64 Id, string Code, string Name)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                strQuery = " select count(nRevenueID) from BL_UB_RevenueCode_MST WITH (NOLOCK) where sRevenueCode='" + Code.Replace("'", "''") + "' and nRevenueID <> " + Id.ToString();


                object _intResult = null;
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }


        public bool BlockUnblockRevenueCode(Int64 nId, bool bIsActive)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " UPDATE BL_UB_RevenueCode_MST WITH (READPAST) SET bIsActive = '" + bIsActive + "' WHERE nID = " + nId;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool DeleteRevenueCode(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_UB_RevenueCode_MST where nRevenueID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool DeleteAllRevenueCode()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            //   DataTable dt = new DataTable();

            try
            {
                oDB.Connect(false);

                strQuery = " DELETE FROM BL_UB_RevenueCode_MST WHERE bIsActive = '" + true;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }


        #endregion " Public & Private Methods "

    }

    #endregion

    #region "RVU Schedule"

    public class CLsBL_RVUSchedule
    {
        #region " Declarations "

        Int64 _ClinicID = 0;
        string _messageBoxCaption = "";
        string _databaseconnectionstring = "";

        #endregion " Declarations "

        #region "Constructor & Destructor"

        public CLsBL_RVUSchedule()
        {
            _databaseconnectionstring = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
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

        ~CLsBL_RVUSchedule()
        {
            Dispose(false);
        }

        #endregion

        #region " Public & Private Methods "

        public bool IsValidCPT(string CPTCode)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            bool _returnvalue = false;
            string sqlgetQuery = " SELECT sCPTCode " +
                                 " FROM CPT_MST WHERE sCPTCode in ('" + (CPTCode).Replace("'", "''") + "')";
            try
            {
                ODB.Connect(false);
                ODB.Retrive_Query(sqlgetQuery, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    _returnvalue = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (ODB != null) { ODB.Dispose(); ODB = null; }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            return _returnvalue;

        }

        public Boolean SaveRVUScheduleTVP(DataTable dtMst, DataTable dtDetail, string strOperation)
        {
            object _error = null;
            Boolean bResult = true;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string strErrorMessage = string.Empty;
            string sTransResult = "";
            try
            {
                if (dtMst != null && dtMst.Rows.Count > 0)
                {
                    oParameters.Clear();
                    oDB.Connect(false);
                    oParameters.Add("@tvpRVUScheduleMST", dtMst, ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@tvpRVUScheduleDTL", dtDetail, ParameterDirection.Input, SqlDbType.Structured);
                    oParameters.Add("@sOperation", strOperation, ParameterDirection.Input, SqlDbType.VarChar, 1);
                    oParameters.Add("@sTranResult", sTransResult, ParameterDirection.Output, SqlDbType.VarChar, 1000);
                    oDB.Execute("BL_Save_RVUSchedule_TVP", oParameters, out _error);

                    oDB.Disconnect();

                    if (_error != null)
                    {
                        sTransResult = Convert.ToString(_error);
                        if (sTransResult.Trim() == "")
                        {
                            bResult = true;
                        }
                        else
                        {
                            bResult = false;
                        }
                    }
                    else
                    {
                        sTransResult = "";
                        bResult = false;
                    }

                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
            }

            return bResult;
        }

        public DataTable fillRVUScheduleMst(long _nRVUId)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = null;
            try
            {
                ODB.Connect(false);
                string sqlgetQuery = "Select nRVUID,BL_RVU_Schedule_MST.dtEffectiveDate, " +
                                        " BL_RVU_Schedule_MST.nScheduleType, BL_RVU_Schedule_MST.sStatementNote, BL_RVU_Schedule_MST.bIsActive from " +
                                        " BL_RVU_Schedule_MST  WHERE BL_RVU_Schedule_MST.nRVUID=" + _nRVUId;
                ODB.Retrive_Query(sqlgetQuery, out dt);
                ODB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Dispose(); ODB = null; }

            }
            return dt;
        }

        public DataTable fillRVUScheduleDtl(long _nRVUId)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtDetail = null;
            try
            {

                ODB.Connect(false);
                string sqlgetQuery = " SELECT BL_RVU_Schedule_DTL.nRVUID, BL_RVU_Schedule_DTL.nRVUDtlID, BL_RVU_Schedule_DTL.sCPTCode, BL_RVU_Schedule_DTL.sModifier,BL_RVU_Schedule_DTL.sCPTDescription,  " +
                                        " BL_RVU_Schedule_DTL.dWorkUnits, BL_RVU_Schedule_DTL.dMPUnits, BL_RVU_Schedule_DTL.dPEUnits, BL_RVU_Schedule_DTL.dTotalRVU from " +
                                        " BL_RVU_Schedule_DTL WHERE BL_RVU_Schedule_DTL.nRVUID=" + _nRVUId + " ORDER BY BL_RVU_Schedule_DTL.sCPTCode";


                ODB.Retrive_Query(sqlgetQuery, out dtDetail);
                ODB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Dispose(); ODB = null; }
                // dtDetail.Dispose();
            }
            return dtDetail;
        }

        public object getEffectiveDate(string effDate)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object _Name = 0;
            try
            {
                ODB.Connect(false);
                string _sqlQuery = " SELECT Count(*) FROM   BL_RVU_Schedule_MST WHERE  dtEffectiveDate = '" + effDate + "'";
                _Name = ODB.ExecuteScalar_Query(_sqlQuery);
                ODB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ODB != null) { ODB.Dispose(); ODB = null; }
            }
            return _Name;
        }

        #endregion " Public & Private Methods "

    }

    #endregion

    #region "Fee Schedule"

    public static class CLsBL_FeeSchedule
    {


        #region " Public & Private Methods "

        public static void DeleteStdFeeSchedule(Int64 _StdFeeScheeduleID)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlDeleteQuery = "";
            try
            {
                oDB.Connect(false);

                _sqlDeleteQuery = "DELETE FROM BL_FeeSchedule_MST WHERE nFeeScheduleID = " + _StdFeeScheeduleID + " AND nClinicID =" + gloGlobal.gloPMGlobal.ClinicID + " ";

                int retVal = oDB.Execute_Query(_sqlDeleteQuery);
                string _sqlQuery = "DELETE FROM BL_FeeSchedule_DTL WHERE nFeeScheduleID = " + _StdFeeScheeduleID + " AND nClinicID= " + gloGlobal.gloPMGlobal.ClinicID + " ";

                int retVal1 = oDB.Execute_Query(_sqlQuery);

                if (retVal > 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupStandardFeeSchedule, ActivityType.Delete, "Delete fee schedule", 0, _StdFeeScheeduleID, 0, ActivityOutCome.Success);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupStandardFeeSchedule, ActivityType.Delete, "Delete fee schedule", 0, _StdFeeScheeduleID, 0, ActivityOutCome.Failure);
                }
                if (retVal1 > 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupStandardFeeSchedule, ActivityType.Delete, "Delete fee schedule details", 0, _StdFeeScheeduleID, 0, ActivityOutCome.Success);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupStandardFeeSchedule, ActivityType.Delete, "Delete fee schedule details", 0, _StdFeeScheeduleID, 0, ActivityOutCome.Failure);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }

            }
        }

        public static void DeleteStdFeeSchedule(Int64 _StdFeeScheeduleID, string HCPCSCode, string SpecialtyID, string Year, string CarrierNumber, string Locality, string Modifier)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            //string _sqlDeleteQuery = "";
            try
            {
                oDB.Connect(false);

                string _sqlQuery = "DELETE FROM BL_FeeSchedule_DTL WHERE nFeeScheduleID = " + _StdFeeScheeduleID + " AND nClinicID= " + gloGlobal.gloPMGlobal.ClinicID + " AND  sHCPCS ='" + HCPCSCode.Replace("'", "''") + "' ";
                if (SpecialtyID != "")
                {
                    _sqlQuery += " AND sSpecialtyID = '" + SpecialtyID + "'";
                }
                if (Year != "")
                {
                    _sqlQuery += " AND sYear = '" + Year + "'";
                }
                if (CarrierNumber != "")
                {
                    _sqlQuery += " AND sCarrierNumber = '" + CarrierNumber + "'";
                }
                if (Modifier != "")
                {
                    _sqlQuery += " AND sModifier = '" + Modifier + "'";
                }
                if (Locality != "")
                {
                    _sqlQuery += " AND sLocality = '" + Locality + "'";
                }
                int retVal1 = oDB.Execute_Query(_sqlQuery);

                if (retVal1 > 0)
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupStandardFeeSchedule, ActivityType.Delete, "Delete fee schedule Line", 0, _StdFeeScheeduleID, 0, ActivityOutCome.Success);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.Billing, ActivityCategory.SetupStandardFeeSchedule, ActivityType.Delete, "Delete fee schedule Line", 0, _StdFeeScheeduleID, 0, ActivityOutCome.Failure);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public static Boolean IsFeeScheduleInUse(Int64 nFeescheduleID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable _dt = null;
            Boolean _isFeeScheduleInUse = false;
            try
            {

                oParameters = new gloDatabaseLayer.DBParameters();
                oParameters.Add("@nFeeScheduleID", nFeescheduleID, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Retrive("Check_For_FeeScheduleInUse", oParameters, out _dt);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    if (Convert.ToInt64(_dt.Rows[0]["FeeSheduleIDCount"]) > 0)
                    {
                        _isFeeScheduleInUse = true;
                    }
                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                throw dbEx;
            }
            finally
            {
                if (oParameters != null) { oDB.Disconnect(); oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
            }

            return _isFeeScheduleInUse;
        }

        #endregion " Public & Private Methods "

    }

    #endregion

    #region "MedicaidResubmissionCodes"
    public class MedicaidResubmissionCode
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ValueCodeID = 0;

        private string _sCode = "";
        private string _sDescription = "";

        private bool _bIsSystem = false;




        public MedicaidResubmissionCode(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

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

        ~MedicaidResubmissionCode()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        public Int64 ValueCodeID
        {
            get { return _ValueCodeID; }
            set { _ValueCodeID = value; }
        }

        public string sCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        public bool IsSystem
        {
            get { return _bIsSystem; }
            set { _bIsSystem = value; }
        }

        public string sDescription
        {
            get { return _sDescription; }
            set { _sDescription = value; }
        }

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = String.Empty;

        public MedicaidResubmissionCode()
        {
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
        }
        #endregion

        #region" Methods"

        public Int64 AddMedicaidResubmissionCode(Int64 nID, string sCode, string sDesc, bool IsBlocked)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);
                oParameters.Add("@nResubmissionCodeID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sResubmissionCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDescription", sDesc, ParameterDirection.Input, SqlDbType.VarChar, 1000);
                oParameters.Add("@bIsActive", IsBlocked, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@nUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("BL_INUP_MedicaidResubmissionCodes", oParameters, out _oResult);


                if (oParameters["@nResubmissionCodeID"].Value != null)
                { return (Int64)oParameters["@nResubmissionCodeID"].Value; }


                return 0;

            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }

        public bool DeleteMedicaidResubmissionCode(Int64 ID, string DatabaseConnectionString)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_MedicaidResubmissionCodes where nResubmissionCodeID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        public bool IsExistsMedicaidResubmissionCode(Int64 nID, string Code)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                strQuery = " select count(nResubmissionCodeID) from BL_MedicaidResubmissionCodes WITH (NOLOCK) where sResubmissionCode='" + Code.Replace("'", "''") + "'  and nResubmissionCodeID!='" + nID + "' ";
                //

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }
        public DataTable GetMedicaidReasonCodes()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtMedicaidResubmissionCodes = null;
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                strQuery = "SELECT nResubmissionCodeID,sResubmissionCode,sResubmissionCode + ' - ' + sDescription as sMedicaidResubmissionCodeDesc ,bIsActive "
                           + " FROM BL_MedicaidResubmissionCodes WITH (NOLOCK) WHERE bIsActive = 1  order by  sResubmissionCode asc";
                oDB.Retrive_Query(strQuery, out _dtMedicaidResubmissionCodes);

                if (_dtMedicaidResubmissionCodes != null && _dtMedicaidResubmissionCodes.Rows.Count > 0)
                {
                    return _dtMedicaidResubmissionCodes;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }



        #endregion
    }
    #endregion

    #region "BusinessCenter"
    public class BusinessCenter
    {
        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ValueCodeID = 0;

        private string _sCode = "";
        private string _sDescription = "";

        private bool _bIsSystem = false;




        public BusinessCenter(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

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

        ~BusinessCenter()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        public Int64 ValueCodeID
        {
            get { return _ValueCodeID; }
            set { _ValueCodeID = value; }
        }

        public string sCode
        {
            get { return _sCode; }
            set { _sCode = value; }
        }

        public bool IsSystem
        {
            get { return _bIsSystem; }
            set { _bIsSystem = value; }
        }

        public string sDescription
        {
            get { return _sDescription; }
            set { _sDescription = value; }
        }

        public string DataBaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }

        }

        private string _messageBoxCaption = String.Empty;

        public BusinessCenter()
        {
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
        }
        #endregion

        #region" Methods"

        public Int64 AddBusinessCenterCode(Int64 nID, string sCode, string sDesc, bool IsBlocked, Int64 nStatementDisplaySettingsID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);
                oParameters.Add("@nBusinessCenterID", nID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sBusinessCenterCode", sCode, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDescription", sDesc, ParameterDirection.Input, SqlDbType.VarChar, 1000);
                oParameters.Add("@bIsActive", IsBlocked, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@nStatementDisplaySettingsID", nStatementDisplaySettingsID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Execute("BL_INUP_BusinessCenterCode", oParameters, out _oResult);

                //if (_oResult != null)
                //{
                //    oDB.Execute_Query("DELETE FROM BL_BusinessCenter_UsersAssociation WHERE nBusinessCenterID =" + (Int64)oParameters["@nBusinessCenterID"].Value);

                //    if (dtUsers != null && dtUsers.Rows.Count > 0)
                //    {
                //        for (int i = 0; i < dtUsers.Rows.Count; i++)
                //        {
                //            DataRow dr = dtUsers.Rows[i];
                //            gloDatabaseLayer.DBParameters oDBParameter = new gloDatabaseLayer.DBParameters();
                //            oDBParameter.Add("@nID", 0, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                //            oDBParameter.Add("@nBusinessCenterID", (Int64)oParameters["@nBusinessCenterID"].Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                //            oDBParameter.Add("@nUserID", Convert.ToString(dr[0]), System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                //            oDB.Execute("BL_INUP_BusinessCenter_UsersAssociation", oDBParameter);
                //            oDBParameter.Dispose();
                //        }
                //    }
                //}

                if (oParameters["@nBusinessCenterID"].Value != null)
                { return (Int64)oParameters["@nBusinessCenterID"].Value; }
                return 0;
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }

        public bool DeleteBusinessCenterCode(Int64 ID, string DatabaseConnectionString)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_BusinessCenterCodes where nBusinessCenterID =" + ID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        public bool IsExistsBusinessCenterCode(Int64 nID, string Code)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                strQuery = " select count(nBusinessCenterID) from BL_BusinessCenterCodes WITH (NOLOCK) where sBusinessCenterCode='" + Code.Replace("'", "''") + "'  and nBusinessCenterID!='" + nID + "' ";
                //

                object _intResult = null;
                //_intResult = oDB.Execute_Query(strQuery);
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }

        public DataTable GetMedicaidReasonCodes()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _dtBusinessCenterCodes = null;
            string strQuery = "";

            try
            {
                oDB.Connect(false);
                strQuery = "SELECT nBusinessCenterID,sBusinessCenterCode,sBusinessCenterCode + ' - ' + sDescription as sBusinessCenterCodeDesc ,bIsActive "
                           + " FROM BL_BusinessCenterCodes WITH (NOLOCK) WHERE bIsActive = 1  order by  sBusinessCenterCode asc";
                oDB.Retrive_Query(strQuery, out _dtBusinessCenterCodes);

                if (_dtBusinessCenterCodes != null && _dtBusinessCenterCodes.Rows.Count > 0)
                {
                    return _dtBusinessCenterCodes;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        public bool VerifyBforDeleteBusinessCenterCode(Int64 BusinessCenterID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " SELECT 1 FROM Pa_accounts WHERE nBusinessCenterID = " + BusinessCenterID + ""
                            + " UNION "
                            + "SELECT  1 FROM BL_BusinessCenter_UsersAssociation WHERE nBusinessCenterID = " + BusinessCenterID + "";

                string result = Convert.ToString(oDB.ExecuteScalar_Query(strQuery));

                if (result != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }

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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        #endregion
    }
    #endregion "BusinessCenter"


    #region "ClaimCategoryType"

    public class ClaimReportingCategory
    {
        #region "Constructor & Destructor"

        private string _databaseconnectionstring = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        //  private Int64 _ValueCodeID = 0;


        public ClaimReportingCategory(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;

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

        ~ClaimReportingCategory()
        {
            Dispose(false);
        }

        #endregion

        #region 'Public and Private Properties'

        public Int64 ClaimReportingCategoryID
        {
            get;
            set;
        }

        public string ClaimReportingCategoryCode
        {
            get;
            set;
        }

        public string ClaimReportingCategoryDescription
        {
            get;
            set;
        }
        public bool bIsActive
        {
            get;
            set;
        }

        public string DataBaseConnectionString
        {
            get;
            set;

        }

        private string _messageBoxCaption = String.Empty;

        public ClaimReportingCategory()
        {
            _messageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption;
        }

        #endregion

        #region" Methods"

        public Int64 AddClaimReportingCategory()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object _oResult = null;
            try
            {
                //Pass 0 to Add
                oDB.Connect(false);
                oParameters.Add("@nClaimReportingCategoryID", ClaimReportingCategoryID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sClaimReportingCategoryCode", ClaimReportingCategoryCode, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oParameters.Add("@sClaimReportingCategoryDesc", ClaimReportingCategoryDescription, ParameterDirection.Input, SqlDbType.VarChar, 255);
                oParameters.Add("@bIsActive", bIsActive, ParameterDirection.Input, SqlDbType.Bit, 100);
                oParameters.Add("@nUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nClinicID", gloGlobal.gloPMGlobal.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Execute("BL_INUP_ClaimCategoryType", oParameters, out _oResult);


                if (oParameters["@nClaimReportingCategoryID"].Value != null)
                { return (Int64)oParameters["@nClaimReportingCategoryID"].Value; }
                return 0;
            }
            catch (gloDatabaseLayer.DBException dbex)
            {
                dbex.ERROR_Log(dbex.ToString());
                return 0;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return 0;
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
                _oResult = null;
            }
        }

        public bool DeleteClaimReportingCategory()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "DELETE FROM BL_ClaimReportingCategory_MST where nClaimReportingCategoryID =" + ClaimReportingCategoryID;
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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }

        }

        public bool IsClaimReportingCategoryExists()
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";

            try
            {
                oDB.Connect(false);

                if (ClaimReportingCategoryID == 0)
                {
                    strQuery = "SELECT COUNT(nClaimReportingCategoryID) As nCount FROM BL_ClaimReportingCategory_MST WITH (NOLOCK) WHERE  " +
                                " (sCode = '" + ClaimReportingCategoryCode.Replace("'", "''") + "') AND nClinicID=" + gloGlobal.gloPMGlobal.ClinicID + " ";
                }
                else
                {
                    strQuery = "SELECT COUNT(nClaimReportingCategoryID) As nCount FROM BL_ClaimReportingCategory_MST WITH (NOLOCK) WHERE  " +
                                " ((sCode = '" + ClaimReportingCategoryCode.Replace("'", "''") + "') AND nClaimReportingCategoryID <> " + ClaimReportingCategoryID + ") AND nClinicID=" + gloGlobal.gloPMGlobal.ClinicID + " ";

                }


                object _intResult = null;
                _intResult = oDB.ExecuteScalar_Query(strQuery);

                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }

            return _result;
        }

        public bool IsClaimReportingCategoryUsedinTransactions()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = " SELECT count(nClaimReportingCategoryID)   FROM BL_Transaction_MST WHERE nClaimReportingCategoryID = " + ClaimReportingCategoryID + " AND isnull(nClaimReportingCategoryID,0) <> 0 ";

                string result = Convert.ToString(oDB.ExecuteScalar_Query(strQuery));

                if (result != "")
                {
                    if (Convert.ToInt16(result) > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }

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
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public DataTable GetClaimReportingCategory(Int64 nClaimCategoryTypeID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dtClaimCategoryType = null;
            string _sqlQuery = "";
            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT  nClaimReportingCategoryID , " +
                            " ISNULL(sCode, '') AS sClaimReportingCategoryCode ," +
                            " ISNULL(sDescription, '') AS sClaimReportingCategoryDescription ," +
                            " ISNULL(bIsactive,1) as sStatus " +
                            " FROM    BL_ClaimReportingCategory_MST WITH (NOLOCK) " +
                            " WHERE nClaimReportingCategoryID = " + nClaimCategoryTypeID + " AND nClinicID = " + gloGlobal.gloPMGlobal.ClinicID + " ";

                oDB.Retrive_Query(_sqlQuery, out dtClaimCategoryType);
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return dtClaimCategoryType;
        }

        #endregion
    }

    #endregion "ClaimCategoryType"

    #region "QuickNotes"
    public class QuickNotes
    {

        #region "Constructor & Distructor"

        public QuickNotes(string DatabseConnectionString)
        {
            _databaseconnectionstring = DatabseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
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
        private string _messageBoxCaption = "";

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

        ~QuickNotes()
        {
            Dispose(false);
        }

        #endregion

        #region "Private variables"
        //private string _MessageBoxCaption = "gloPM";
        private string _databaseconnectionstring = "";

        private Int64 _nID = 0;

        private string _Notes = "";

        private QuickNoteType _NoteType = QuickNoteType.None;

        private bool _bIsActive = true;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        #endregion "Private variables"

        #region "Property Procedures"

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        public string DatabaseConnectionString
        {
            get { return _databaseconnectionstring; }
            set { _databaseconnectionstring = value; }
        }

        public Int64 NoteID
        {
            get { return _nID; }
            set { _nID = value; }
        }

        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }

        public QuickNoteType NoteType
        {
            get { return _NoteType; }
            set { _NoteType = value; }
        }

        public bool Status
        {
            get { return _bIsActive; }
            set { _bIsActive = value; }
        }
        #endregion "Property Procedures"

        public Int64 Add()
        {
            Int64 _result = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {
                object _intresult = 0;

                oDBParameters.Add("@nID", _nID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sNoteDescription", _Notes, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nNoteType", _NoteType.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@bIsActive", _bIsActive, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);

                oDB.Execute("BL_INUP_NotesMaster", oDBParameters, out _intresult);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult.ToString());
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Clear();
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }


        public bool Delete(Int64 ID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "DELETE from BL_NotesMaster where nID =" + ID;
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
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        public System.Data.DataTable GetList()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            System.Data.DataTable _result = null;
            string _sqlQuery = "";

            try
            {
                oDB.Connect(false);

                _sqlQuery = " SELECT nID,ISNULL(bIsActive,'TRUE') AS bIsActive," +
                "(CASE nNoteType when " + QuickNoteType.ClaimInternal.GetHashCode() + " then '" + Enumerations.GetEnumDescription(QuickNoteType.ClaimInternal) + "'"
                + " when " + QuickNoteType.AccountInternal.GetHashCode() + " then '" + Enumerations.GetEnumDescription(QuickNoteType.AccountInternal) + "'"
                + " when " + QuickNoteType.StatementPatient.GetHashCode() + " then '" + Enumerations.GetEnumDescription(QuickNoteType.StatementPatient) + "'"
                + " when " + QuickNoteType.StatementCharge.GetHashCode() + " then '" + Enumerations.GetEnumDescription(QuickNoteType.StatementCharge) + "'"
                + " else '" + QuickNoteType.None.ToString() + "' end) AS nNoteType,"
                + " ISNULL(sNoteDescription,'') AS sNoteDescription from dbo.BL_NotesMaster order by bIsActive desc,nNoteType";

                oDB.Retrive_Query(_sqlQuery, out _result);
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

        public System.Data.DataTable GetNote(Int64 _ID)
        {
            System.Data.DataTable _result = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "SELECT	nID,ISNULL(sNoteDescription,'') AS sNoteDescription,ISNULL(nNoteType,0) AS nNoteType,"
                                + " ISNULL(bIsActive,'TRUE') AS bIsActive from dbo.BL_NotesMaster where nID =" + _ID;
                oDB.Retrive_Query(_sqlQuery, out _result);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }


    }
    #endregion "QuickNotes"
    #region "Hold Billing"

    public class HoldBilling
    {
        private Int64 _nHoldBillingID = 0;
        private string _sHoldBillingReason = "";
        private string _sHoldBillingDesription = "";
        private string _messageBoxCaption = "";

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;


        #region Properties
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 HoldBillingID
        {
            get { return _nHoldBillingID; }
            set { _nHoldBillingID = value; }
        }
        public string HoldBillingReason
        {
            get { return _sHoldBillingReason; }
            set { _sHoldBillingReason = value; }
        }
        public string HoldBillingDescription
        {
            get { return _sHoldBillingDesription; }
            set { _sHoldBillingDesription = value; }
        }
        #endregion

        #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
        public HoldBilling(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _ClinicID = Convert.ToInt64(appSettings["clinicId"]);
                }
                else { _ClinicID = 0; }
            }

            else
            {
                _ClinicID = 0;
            }
            #region "Retrieve MessageBoxCaption from AppSettings "
            if (appSettings["MessageBoxCaption"] != null)
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
            #endregion
        }
        #endregion
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
        ~HoldBilling()
        {
            Dispose(false);
        }

   


        public Int64 Add()
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                object _intresult = 0;

                oDBParameters.Add("@nHoldBillingID", this.HoldBillingID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sHoldBillingReason", this.HoldBillingReason, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sHoldBillingDesc", this.HoldBillingDescription, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                _result = oDB.Execute("gsp_InUpHoldBilling_MST", oDBParameters, out _intresult);
                if (_intresult != null)
                {
                    if (Convert.ToInt64(_intresult) > 0)
                    {
                        _result = Convert.ToInt64(_intresult.ToString());
                    }
                }
            }

            catch (gloDatabaseLayer.DBException ex)
            {

                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                finally
            {
                 if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }

            }
            return _result;
        }

        public bool Delete(Int64 nHoldBillingID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from HoldBilling_MST where nHoldBillingID =" + nHoldBillingID;
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
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }

        public bool IsHoldBillingReasonCodeUsed(Int64 nHoldBillingID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            object _Result = null;
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT COUNT(nHoldBillingID) FROM  dbo.BL_Transaction_Claim_MST WHERE ISNULL(bIsHold,0)=1 AND nHoldBillingID=" + nHoldBillingID;
                _Result = oDB.ExecuteScalar_Query(strQuery);
                if (_Result != null)
                {
                    if (Convert.ToInt16(_Result) > 0)
                    { return true; }
                    else
                        return false;
  
                }
                else
                {
                    return false;
                }
                
            }
            catch (gloDatabaseLayer.DBException dbErr)
            {
                dbErr.ERROR_Log(dbErr.ToString());
                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }
      
       
        #region "Get Hold Billing"
        internal DataTable GetHoldBilling(long _nHoldBillingID)
        {
            DataTable _result = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                if (_nHoldBillingID != 0)
                {
                    String strQuery = "SELECT ISNULL(nHoldBillingID,'') AS nHoldBillingID,ISNULL(sHoldBillingReason,'') AS sHoldBillingReason,ISNULL(sHoldBillingDescription,'') AS sHoldBillingDescription,ISNULL(sHoldBillingReason,'') +' - '+ISNULL(sHoldBillingDescription,'')  AS sDescription FROM HoldBilling_MST WITH (NOLOCK) WHERE nHoldBillingID = " + _nHoldBillingID;
                    oDB.Retrive_Query(strQuery, out _result);
                    return _result;
                }
                else
                {
                    String strQuery = "SELECT ISNULL(nHoldBillingID,'') AS nHoldBillingID,ISNULL(sHoldBillingReason,'') AS sHoldBillingReason,ISNULL(sHoldBillingDescription,'') AS sHoldBillingDescription,ISNULL(sHoldBillingReason,'') +' - '+ISNULL(sHoldBillingDescription,'')  AS sDescription FROM HoldBilling_mst ";
                    oDB.Retrive_Query(strQuery, out _result);
                }
      
                return _result;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }
        #endregion
        internal bool CheckDuplicate(Int64 _nHoldBillingID, string _sHoldBillingReason, string _sHoldBillingDescription)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            bool _result = false;
            _sHoldBillingReason = _sHoldBillingReason.Replace("'", "''");
            _sHoldBillingDescription = _sHoldBillingDescription.Replace("'", "''");
            try
            {
                oDB.Connect(false);
                if (_nHoldBillingID == 0)
                {
                    strQuery = "select count(nHoldBillingID) from HoldBilling_mst where (sHoldBillingReason='" + _sHoldBillingReason + "'OR sHoldBillingDescription='" + _sHoldBillingDesription.Replace("'", "''") + "')";
                }
                else
                {
                    strQuery = " select count(nHoldBillingID) FROM HoldBilling_MST WITH (NOLOCK) where ((sHoldBillingReason = '" + _sHoldBillingReason + "' OR sHoldBillingDescription='" + _sHoldBillingDesription.Replace("'", "''") + "' ) AND nHoldBillingID <> " + _nHoldBillingID + ")";
                }
                object _intResult = null;
                _intResult = oDB.ExecuteScalar_Query(strQuery);
                if (_intResult != null)
                {
                    if (_intResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                //oDBParameters.Dispose();
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
            return _result;
        }

    }
     #endregion
    #region "Standard Claim Follow-up Action"
    public class StandardFollowupAction
    {
        private Int64 _nStdFollowupID = 0;
        private string _sStdFollowupCode = "";
        private string _sStdFollowupDesc = "";
        private string _MessageBoxCaption = "";

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;

        //properties
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        public Int64 nStdFollowupID
        {
            get { return _nStdFollowupID; }
            set { _nStdFollowupID = value; }
        }
        public string sStdFollowupCode
        {
            get { return _sStdFollowupCode; }
            set { _sStdFollowupCode = value; }
        }
        public string sStdFollowupDesc
        {
            get { return _sStdFollowupDesc; }
            set { _sStdFollowupDesc = value; }
        }


        private string _databaseconnectionstring = "";
        public StandardFollowupAction(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                {
                    _ClinicID = Convert.ToInt64(appSettings["clinicId"]);
                }
                else { _ClinicID = 0; }
            }

            else
            {
                _ClinicID = 0;
            }
            #region "Retrieve MessageBoxCaption from AppSettings "
            if (appSettings["MessageBoxCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
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
        ~StandardFollowupAction()
        {
            Dispose(false);
        }

        #region "Get Standard Followup Action"
        internal DataTable GetStdFollowupAction(long _nStdActionID)
        {
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                oDB.Connect(false);
                string str = string.Empty;
                if (_nStdActionID == 0)
                {
                    str = "select ISNULL(nID,0) as nID ,ISNULL(sStdFollowupActionCode,'') as sStdFollowupActionCode,ISNULL(sStdFollowupActionDesc,0) as sStdFollowupActionDesc from BL_StandardFollowupAction WITH (NOLOCK) order by sStdFollowupActionCode ";
                    
                }
                else
                {
                    str = "select ISNULL(nID,0) as nID,ISNULL(sStdFollowupActionCode,'') as sStdFollowupActionCode,ISNULL(sStdFollowupActionDesc,0) as sStdFollowupActionDesc from BL_StandardFollowupAction WITH (NOLOCK) where nID=" + _nStdActionID;
                   
                }
                oDB.Retrive_Query(str, out dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return dt;
        }

        #endregion
        #region "Add"
        public Int64 Add()
        {
            object StdFollowupID = 0;
            Int64 nStdFollowupActionID = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oParameters.Add("@nID", this.nStdFollowupID, System.Data.ParameterDirection.InputOutput, SqlDbType.BigInt);
                oParameters.Add("@sCode", this.sStdFollowupCode, System.Data.ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@sDescription", this.sStdFollowupDesc, System.Data.ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Execute("gsp_StandardFollowupAction", oParameters, out StdFollowupID);

                if (StdFollowupID != null)
                {
                    if (Convert.ToInt64(StdFollowupID) > 0)
                    {
                        nStdFollowupActionID = Convert.ToInt64(StdFollowupID);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oParameters != null) { oParameters.Dispose(); oParameters = null; }
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }

            }

            return nStdFollowupActionID;
        }
        #endregion

        public bool Delete(Int64 nStdfollowupActionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = "";
            try
            {
                oDB.Connect(false);
                strQuery = "delete from BL_StandardFollowupAction where nID =" + nStdfollowupActionID;
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
                MessageBox.Show("ERROR : " + ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); oDB = null; }
            }
        }
        public bool CheckDublicate(Int64 _nStdFollowupID, string _sStdFollowupCode, string _sStdFollowupDesc)
        {
            string sqlQuery = "";
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                if (_nStdFollowupID == 0)
                {
                    sqlQuery = "select count(nID) from BL_StandardFollowupAction WITH (NOLOCK)  where (sStdFollowupActionCode='" + _sStdFollowupCode.Replace("'", "''") + "'or sStdFollowupActionDesc='" + _sStdFollowupDesc.Replace("'", "''") + "')";

                }
                else
                {
                    sqlQuery = "select count(nID) from BL_StandardFollowupAction WITH (NOLOCK)  where ((sStdFollowupActionCode='" + _sStdFollowupCode.Replace("'", "''") + "'or sStdFollowupActionDesc='" + _sStdFollowupDesc.Replace("'", "''") + "' ) and nID <> " + _nStdFollowupID + ")";
                }
                object _objResult = null;
                _objResult = oDB.ExecuteScalar_Query(sqlQuery);

                if (_objResult != null)
                {
                    if (_objResult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_objResult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
               
            }
            return _result;

    
        }
        public bool IsEnableAddEditDeleteSetting()
        {
            string strQuery = null;
            bool _result = false;
            object _objresult = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            try
            {
                oDB.Connect(false);
                strQuery = "select sSettingsValue from settings where sSettingsName='EnableAddEditDelete_StandardScheduleAction'";
                _objresult = oDB.ExecuteScalar_Query(strQuery);
                if (_objresult != null)
                {
                    _result = Convert.ToBoolean(_objresult);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _result;
        }
        public bool VerifyBeforeDeleteStdCode(Int64 nID)
        {
            object _Result=null;
            bool IsUsed = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nStdFollowupID", nID, System.Data.ParameterDirection.Input, SqlDbType.BigInt);
                _Result=oDB.ExecuteScalar("gsp_IsUsedStdFollowupCode", oParameters);

                if (_Result != null)
                {
                    if (Convert.ToInt16(_Result) > 0)
                    {
                        IsUsed = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }
                _Result = null;
            }
            return IsUsed;
        }

    }
    #endregion
}







