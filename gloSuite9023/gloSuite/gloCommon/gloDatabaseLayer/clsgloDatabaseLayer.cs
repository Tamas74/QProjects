using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.Management;
using System.Threading;
using System.ComponentModel;

namespace gloDatabaseLayer
{

    //************ EXCEPTION CLASS ********************
    public class DBException : System.ApplicationException
    {
        private int no;
        private string description;
        private Exception sourceException;
        private static  string _messageBoxCaption = String.Empty;
       // private static RegistryKey regkey = null;

        public Exception SourceException
        {
            get
            {
                return sourceException;
            }
            set
            {
                sourceException = value;
            }
        }

        public int No
        {
            get
            {
                return no;
            }
            set
            {
                no = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }


        //public DBException(int no, string description, Exception source)
        //{
        //    this.no = no; this.description = description; this.sourceException = source;

        //}
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        public DBException(string sErrorMessage, String ActualError) : base(sErrorMessage)
        {
            ERROR_Log(ActualError);
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

        public static bool isChecked = false;
        public static bool IsServerOS = false;

        private static object GetRegistryValue(string KeyName)
        {
            //Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER
            ////Commented for - From 7020 read registrey from Current user (Windows 8) on 20121217
            //if (!isChecked)
            //{
            //    if (gloDatabaseLayer.clsCommon.GetOSInfo(out isChecked))
            //    {
            //        IsServerOS = true;
            //    }
            //}
            //if (IsServerOS)
            //{
            //    regkey = Registry.CurrentUser;
            //}
            //else
            //{
            //    regkey = Registry.LocalMachine;
            //}           

            //regkey = Registry.CurrentUser;
            //RegistryKey rkey;
            ////Following is done to Log Application exception Log which was not done at the time of Terminal Server
            //if (_messageBoxCaption.Contains("EMR"))
            //{
            //    rkey = regkey.OpenSubKey("Software\\gloEMR");
            //}
            //else
            //{
            //    rkey = regkey.OpenSubKey("Software\\gloPM");
            //}
            //if (rkey.GetValue(KeyName) == null)
            //    return null;
            //else
            //    return rkey.GetValue(KeyName);
            //SLR: Added on 3/20/2014 - to resolve registry memory
            RegistryKey regkey = Registry.CurrentUser;

            RegistryKey rkey;
            object myObject = null;
            //Following is done to Log Application exception Log which was not done at the time of Terminal Server 
            if (_messageBoxCaption.Contains("EMR"))
            {
                rkey = regkey.OpenSubKey("Software\\gloEMR");
            }
            else
            {
                rkey = regkey.OpenSubKey("Software\\gloPM");
            }
            try
            {
                if (rkey != null)
                {
                    myObject = rkey.GetValue(KeyName);
                    rkey.Close();
                    rkey.Dispose();
                }
                regkey.Close();
                regkey.Dispose();
            }
            catch
            {
            }
            return myObject;
        }

        public DBException(string sErrorMessage, String ActualError, System.Exception inner): base(sErrorMessage, inner)
        {
            ERROR_Log(ActualError );

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

        public void ERROR_Log(string strLogMessage)
        {
            Boolean flgDBErr = true;
            string strMessage = string.Empty;
            string _fileName = string.Empty;
            try
            {
                if (DBException.GetRegistryValue("EnableErrorLogs") != null)
                {
                    flgDBErr = Convert.ToBoolean(DBException.GetRegistryValue("EnableErrorLogs"));
                }

                if (flgDBErr == true)
                {
                    String logPath = Application.StartupPath + "\\Log\\DBErrorLog";
                    if (Directory.Exists(logPath) == false)
                    {
                        Directory.CreateDirectory(logPath);
                    }

                    strMessage = Environment.NewLine + "" +
                                           System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + Environment.NewLine +
                                           strLogMessage + Environment.NewLine;

                    _fileName = "DBErrorLog " + DateTime.Now.Date.ToString("MM-dd-yyyy") + ".log";
                    File.AppendAllText(logPath+"\\" + _fileName, strMessage);

                    MessageBox.Show("Error while accessing Database. Please click on Help to view log.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0, Application.StartupPath + "\\Log\\DBErrorLog\\" + _fileName);
                }
                else
                {
                    MessageBox.Show(strLogMessage, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                throw new DBException("Error in closing database connection", ex.ToString());
            }
            finally
            {
                strMessage = null;
                _fileName = null;
            }
        }

        
        
    }

    //************ CONNECTION CLASS *******************

    public class DBConnection : IDisposable
    {

        private string _conectionstring = "";
        protected SqlConnection _connection = null;
        protected SqlTransaction _transaction = null;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #region "Constructor & Destructor"

        public DBConnection(string ConnectionString)
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = ConnectionString;

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


                    if (_transaction != null)
                    {
                         CommitTransaction();
                        _transaction.Dispose();
                        _transaction = null;
                    }

                    if (_connection != null)
                    {
                        CloseConnection();
                        _connection.Dispose();
                        _connection = null;
                    }
                   
                }
            }

           
            disposed = true;            
        }

        ~DBConnection()
        {
            Dispose(false);
        }

#endregion

        public void OpenConnection()
        {
            try
            {
                if (_connection != null)
                {
                    if (_connection.State != ConnectionState.Open)
                    {
                        _connection.Open();
                    }
                }
                else
                {
                    _connection = new SqlConnection();
                    _connection.ConnectionString = _conectionstring;
                    _connection.Open();
                }
            }
            catch (SqlException ex)
            {
             
                throw new DBException("Error in opening database connection",ex.ToString());
            }
            catch (Exception ex)
            {
                throw new DBException("Error in opening database connection", ex.ToString());
            }
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }

        public bool CloseConnection()
        {
            try
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw new DBException("Error in closing database connection",ex.ToString());
            }
            catch (Exception ex)
            {
                throw new DBException("Error in closing database connection", ex.ToString());
            }
        }
        private bool _isTransactionCompleted = false;
        private bool _isTransactionBeginned = false;
        public bool BeginTransaction()
        {
            try
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        if (_isTransactionBeginned == false)
                        {
                            _transaction = _connection.BeginTransaction();
                            _isTransactionCompleted = false;
                            _isTransactionBeginned = true;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                       
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw new DBException("Error in begin transaction", ex.ToString());
            }
            catch (Exception ex)
            {
                throw new DBException("Error in begin transaction", ex.ToString());
            }
        }

        public SqlTransaction GetTransaction()
        {
            return _transaction;
        }

        public bool CommitTransaction()
        {
            try
            {
                if (_transaction != null)
                {
                    if (_isTransactionCompleted == false)
                    {
                        _transaction.Commit();
                        _isTransactionCompleted = true;
                        _isTransactionBeginned = false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw new DBException("Error in commit transaction", ex.ToString());
            }
            catch (Exception ex)
            {
                throw new DBException("Error in commit transaction", ex.ToString());
            }
        }

        public bool RollbackTransaction()
        {
            try
            {
                if (_transaction != null)
                {
                    if (_isTransactionCompleted == false)
                    {
                        _transaction.Rollback();
                        _isTransactionCompleted = true;
                        _isTransactionBeginned = false;
                    }
                   // _transaction = null; 
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw new DBException("Error in roll back transaction", ex.ToString());
            }
            catch (Exception ex)
            {
                throw new DBException("Error in roll back transaction", ex.ToString());
            }
        }

    }
  
    public class DBLayer : IDisposable
    {
        private string _conectionstring = "";
        protected bool _withtransaction = false;
        DBConnection oConnection=null;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #region "Constructor & Distructor"

        public DBLayer(string ConnectionString)
        {
            _conectionstring = ConnectionString;

            try
            {
                if (_conectionstring.Trim() == "")
                {
                    _conectionstring = Convert.ToString(appSettings["DataBaseConnectionString"]);
                }
            }

            catch (Exception)
            {

            }

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
            //Disconnect();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (oConnection != null)
                    {
                        oConnection.Dispose();
                        oConnection = null;
                    }

                }
            }
            disposed = true;
        }

        ~DBLayer()
        {
            Dispose(false);
        }

        #endregion

        #region "Connect & Disconnect"

        public bool Connect(bool WithTransaction)
        {
            if (oConnection != null)
            {
                oConnection.Dispose();
                oConnection = null;
            }
            oConnection = new DBConnection(_conectionstring);

            try
            {
                if (WithTransaction == true)
                {
                    oConnection.OpenConnection();
                    oConnection.BeginTransaction();
                    _withtransaction = WithTransaction;
                    return true;
                }
                else
                {
                    oConnection.OpenConnection();
                    return true;
                }
            }
            catch (DBException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
        }

        public bool CheckConnection()
        {
            bool _IsValidConnection = false;
            SqlConnection _connection = null;
            try
            {
                if (_connection != null)
                {
                    if (_connection.State != ConnectionState.Open)
                    {
                        _connection.Open();
                        _IsValidConnection = true;
                    }
                }
                else
                {
                    _connection = new SqlConnection();
                    _connection.ConnectionString = _conectionstring;
                    if (_connection.Database.ToString().Trim().Length <= 0)
                    {
                        _IsValidConnection = false;
                    }
                    _connection.Open();
                    _IsValidConnection = true;
                }
                // Disconnect();
            }
            catch (DBException )//ex)
            {
                _IsValidConnection = false;
                //ex.ToString();
                //ex = null;
            }
            catch (Exception)// ex)
            {
                _IsValidConnection = false;
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State != ConnectionState.Closed)
                    {
                        _connection.Close();
                    }
                    _connection.Dispose();
                    _connection = null;
                }
            }
            return _IsValidConnection;
        }

        public bool Disconnect()
        {
            try
            {
                if (_withtransaction == true)
                {
                    oConnection.CommitTransaction();
                    oConnection.CloseConnection();
                    return true;
                }
                else
                {
                    if (oConnection != null)  //check existence of object 
                    {
                        oConnection.CloseConnection();
                    }
                     return true;
                }
            }
            catch (DBException) // ex)
            {
                //ex.ToString();
                //ex = null;
                return false;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
                return false;
            }
        }

        public bool Rollback()
        {
            bool _result = false;
            try
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                    oConnection.CloseConnection();
                    return true;
                }
                _result = true;
            }
            catch (DBException)// ex)
            {
                _result = false;
                //ex.ToString();
                //ex = null;
            }
            catch (Exception) // ex)
            {
                _result = false;
                //ex.ToString();
                //ex = null;
            }
            return _result;
        }

        #endregion

        #region "Insert Update Delete - Stored Procedure"

        public int Execute(string StoredProcedureName, DBParameters Parameters)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;



                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }

                _result = _sqlcommand.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                   
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
               
            }
            return _result;
        }

        //Added by Mahesh Satlapalli (Apollo) on 2011-06-27(yyyy-mm-dd) - To Support Transaction
        public int ExecuteWithTransaction(string StoredProcedureName, DBParameters Parameters)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            if (_withtransaction)
            {
                _sqlcommand.Transaction = oConnection.GetTransaction();
            }

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }

                _result = _sqlcommand.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (Exception ex)
            {

                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }
            return _result;
        }

        public Hashtable GetOutParamResults(SqlCommand _sqlcommand)
        {
            Hashtable oOutParam = new Hashtable();
            try
            {
                for (int _ParamCount = 0; _ParamCount <= _sqlcommand.Parameters.Count - 1; _ParamCount++)
                {
                    if (_sqlcommand.Parameters[_ParamCount].Direction == ParameterDirection.Output)
                    {
                        oOutParam.Add(_sqlcommand.Parameters[_ParamCount].ParameterName, _sqlcommand.Parameters[_ParamCount].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oOutParam;
        }

        public Hashtable Execute(string StoredProcedureName, DBParameters Parameters, Boolean bIsOutParamExist)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            Hashtable oOutParam = new Hashtable();
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                if (bIsOutParamExist)
                {
                    for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                    {
                        _sqlcommand.Parameters.Add(Parameters[_counter]);
                    }

                    _result = _sqlcommand.ExecuteNonQuery();

                    for (int _ParamCount = 0; _ParamCount <= Parameters.Count - 1; _ParamCount++)
                    {
                        if (Parameters[_ParamCount].Direction == ParameterDirection.Output)
                        {
                            oOutParam.Add(Parameters[_ParamCount].ParameterName, _sqlcommand.Parameters[_ParamCount].Value);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }
            return oOutParam;
        }

        public int Execute(string StoredProcedureName)
        {
            int _result = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                _result = _sqlcommand.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                
            }
            return _result;
        }

        public int Execute(string StoredProcedureName, DBParameters Parameters, out object ParameterValue)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            int _outputCounter = 0;
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;


                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    if (Parameters[_counter].Direction == ParameterDirection.Output || Parameters[_counter].Direction == ParameterDirection.InputOutput)
                    {
                        _outputCounter = _counter;
                    }

                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }


                _result = _sqlcommand.ExecuteNonQuery();

                if (_sqlcommand.Parameters[_outputCounter].Value != null)
                {
                    ParameterValue = _sqlcommand.Parameters[_outputCounter].Value;
                }
                else
                {
                    ParameterValue = 0;
                }

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (Exception ex)
            {

                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }
            return _result;
        }

        public int Execute(string StoredProcedureName, DBParameters Parameters, out object ParameterValue1, out object ParameterValue2)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            String _outputParamName1 = "";
            String _outputParamName2 = "";

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;


                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    if (Parameters[_counter].Direction == ParameterDirection.Output || Parameters[_counter].Direction == ParameterDirection.InputOutput)
                    {
                        if (_outputParamName1 == "")
                        { _outputParamName1 = Parameters[_counter].ParameterName; }
                        else if (_outputParamName2 == "")
                        { _outputParamName2 = Parameters[_counter].ParameterName; }
                    }

                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }


                _result = _sqlcommand.ExecuteNonQuery();

                if (_sqlcommand.Parameters[_outputParamName1].Value != null)
                { ParameterValue1 = _sqlcommand.Parameters[_outputParamName1].Value; }
                else { ParameterValue1 = 0; }

                if (_sqlcommand.Parameters[_outputParamName2].Value != null)
                { ParameterValue2 = _sqlcommand.Parameters[_outputParamName2].Value; }
                else
                { ParameterValue2 = 0; }

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (Exception ex)
            {

                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }
            return _result;
        }

        public int Execute(string StoredProcedureName, DBParameters Parameters, out object ParameterValue1, out object ParameterValue2, out object ParameterValue3)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            String _outputParamName1 = "";
            String _outputParamName2 = "";
            String _outputParamName3 = "";

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;


                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    if (Parameters[_counter].Direction == ParameterDirection.Output || Parameters[_counter].Direction == ParameterDirection.InputOutput)
                    {
                        if (_outputParamName1 == "")
                        { _outputParamName1 = Parameters[_counter].ParameterName; }
                        else if (_outputParamName2 == "")
                        { _outputParamName2 = Parameters[_counter].ParameterName; }
                        else if (_outputParamName3 == "")
                        { _outputParamName3 = Parameters[_counter].ParameterName; }
                    }

                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }


                _result = _sqlcommand.ExecuteNonQuery();

                if (_sqlcommand.Parameters[_outputParamName1].Value != null)
                { ParameterValue1 = _sqlcommand.Parameters[_outputParamName1].Value; }
                else { ParameterValue1 = 0; }

                if (_sqlcommand.Parameters[_outputParamName2].Value != null)
                { ParameterValue2 = _sqlcommand.Parameters[_outputParamName2].Value; }
                else
                { ParameterValue2 = 0; }

                if (_sqlcommand.Parameters[_outputParamName3].Value != null)
                { ParameterValue3 = _sqlcommand.Parameters[_outputParamName3].Value; }
                else
                { ParameterValue3 = 0; }

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (Exception ex)
            {

                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }
            return _result;
        }

        public object ExecuteScalar(string StoredProcedureName, DBParameters Parameters)
        {
            object _result = null;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {


                    _sqlcommand.Parameters.Add(Parameters[_counter]);

                }

                _result = _sqlcommand.ExecuteScalar();

                if (_result == null)
                {
                    _result = "";
                }

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }

            return _result;
        }

        public object ExecuteScalar(string StoredProcedureName)
        {
            object _result = null;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                _result = _sqlcommand.ExecuteScalar();

                if (_result == null)
                {
                    _result = "";
                }

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
            }

            return _result;
        }

        #endregion

        #region "Insert Update Delete - SQL Query"

        public int Execute_Query(string SQLQuery)
        {
            int _result = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                _result = _sqlcommand.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
            }
            return _result;
        }

        public object ExecuteScalar_Query(string SQLQuery)
        {
            object _result = null;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                _result = _sqlcommand.ExecuteScalar();

                if (_result == null)
                {
                    _result = "";
                }

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
            }

            return _result;
        }

        public object ExecuteScalar_Query(string SQLQuery, out string ErrorMessage)
        {
            object _result = null;
            SqlCommand _sqlcommand = new SqlCommand();
            ErrorMessage = "";
            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                _result = _sqlcommand.ExecuteScalar();

                if (_result == null)
                {
                    _result = "";
                }

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                ErrorMessage = ex.ToString();

            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                ErrorMessage = ex.ToString();

            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                ErrorMessage = ex.ToString();

            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
            }

            return _result;
        }

        #endregion

        #region "Retrive Data - Stored Procedure"

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out SqlDataReader _result)
        {

            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }

                _result = _sqlcommand.ExecuteReader();

            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }

        }

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataSet _result)
        {
            _result = null;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {

                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }


                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                _result = new DataSet();


                _dataAdapter.Fill(_result);
                _dataAdapter.Dispose();

//                _result = _resultinternal;
            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }

        }

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataTable _result)
        {
            _result = null;
            int _counter = 0;
            
            SqlCommand _sqlcommand = new SqlCommand();
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                DataSet _dataset = new DataSet();
          //      DataTable _resultinternal = null; // new DataTable();
               
                _dataAdapter.Fill(_dataset);
                if (_dataset.Tables != null)
                {
                    if (_dataset.Tables.Count > 0)
                    {
                        if (_dataset.Tables[0] != null)
                        {
                            //                        _resultinternal = _dataset.Tables[0];
                            _result = _dataset.Tables[0].Copy();
                        }

                    }
                }
//                _result = _resultinternal;   
                //word crash and memory leak changes reverted
//                _resultinternal.Dispose();                
                _dataset.Dispose();                
                _dataAdapter.Dispose();                               
            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }

        }

        public void RetriveUsingDataReader(string StoredProcedureName, DBParameters Parameters, out DataTable _result)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlDataReader _dataAdapter = null;
            _result = null;
            int _counter = 0;

            try
            {
            
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }

                _dataAdapter = _sqlcommand.ExecuteReader(CommandBehavior.SingleResult);
                
                _result = new DataTable();
                _result.Load(_dataAdapter);
                _dataAdapter.Close();
            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    if (_sqlcommand.Parameters != null) { _sqlcommand.Parameters.Clear(); } _sqlcommand.Dispose(); _sqlcommand = null;
                }
                if (_dataAdapter != null) { _dataAdapter.Dispose(); _dataAdapter = null; }
                
            }

        }

        //Added in 6000 .This method will return two out parameters,datatable and error message.
        public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataTable _result, out string _ErrMsg)
        {
            _result = null;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                _ErrMsg = "";

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {

                    _sqlcommand.Parameters.Add(Parameters[_counter]);

                }

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                DataSet _dataset = new DataSet();

                _dataAdapter.Fill(_dataset);
                if (_dataset.Tables != null)
                {
                    if (_dataset.Tables.Count > 0)
                    {
                        if (_dataset.Tables[0] != null)
                        {
                            //                        _resultinternal = _dataset.Tables[0];
                            _result = _dataset.Tables[0].Copy();
                        }

                    }
                }

                if (_sqlcommand.Parameters["@sErrMessage"].Value != null)
                { _ErrMsg = (_sqlcommand.Parameters["@sErrMessage"].Value).ToString(); }

                _dataset.Dispose();
                _dataAdapter.Dispose();


            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }

        }

        // Without Parameters //
        public void Retrive(string StoredProcedureName, out SqlDataReader _result)
        {
            _result = null;
            SqlCommand _sqlcommand = new SqlCommand();
            
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                _result = _sqlcommand.ExecuteReader();

            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
            }

        }

        public void Retrive(string StoredProcedureName, out DataSet _result)
        {
            _result = null;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                _result = new DataSet();


                _dataAdapter.Fill(_result);
                _dataAdapter.Dispose();

                
            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
            }

        }

        public void Retrive(string StoredProcedureName, out DataTable _result)
        {

            _result = null;
            SqlCommand _sqlcommand = null;

            try
            {
                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                DataSet _dataset = new DataSet();

                _dataAdapter.Fill(_dataset);
                if (_dataset.Tables != null)
                {
                    if (_dataset.Tables.Count > 0)
                    {
                        if (_dataset.Tables[0] != null)
                        {
                            //                        _resultinternal = _dataset.Tables[0];
                            _result = _dataset.Tables[0].Copy();
                        }

                    }
                }
                _dataset.Dispose();
                _dataAdapter.Dispose();


            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand=null;
                }
            }

        }

        #endregion

        #region "Retrive Data - SQL Query"

        public void Retrive_Query(string SQLQuery, out SqlDataReader _result)
        {
            _result = null;
            SqlCommand _sqlcommand = null;

            try
            {
                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                _result = _sqlcommand.ExecuteReader();

            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand=null;
                }
            }
        }

        public void Retrive_Query(string SQLQuery, out DataSet _result)
        {
            _result = null;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                _result = new DataSet();

                _dataAdapter.Fill(_result);
                _dataAdapter.Dispose();

                
            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
            }
        }

        public void Retrive_Query(string SQLQuery, out DataTable _result)
        {
            _result = null;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                DataSet _dataset = new DataSet();
                
                _dataAdapter.Fill(_dataset);
                if (_dataset.Tables != null)
                {
                    if (_dataset.Tables.Count > 0)
                    {
                        if (_dataset.Tables[0] != null)
                        {
                            //                        _resultinternal = _dataset.Tables[0];
                            _result = _dataset.Tables[0].Copy();
                        }

                    }
                }
                _dataset.Dispose();
                _dataAdapter.Dispose();


            }
            catch (SqlException ex)
            {

                throw new DBException("Error in database execution. ", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution.  ", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
            }
        }

        #endregion

        public SqlCommand GetCmdParameters(gloDatabaseLayer.DBParameters Parameters)
        {
            SqlCommand _sqlCommand = new SqlCommand();

            try
            {
                if (Parameters != null && Parameters.Count > 0)
                {
                    for (int _counter = 0; _counter <= Parameters.Count - 1; _counter++)
                    {
                        _sqlCommand.Parameters.Add(Parameters[_counter]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _sqlCommand;
        }

        public Int64 GetPrefixTransactionID(Int64 PatientID)
        {
            Int64 _Result = 0;
            string _result = "";
            DateTime _PatientDOB = DateTime.Now;
            DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

            string strID1 = "";
            string strID2 = "";
            string strID3 = "";

            TimeSpan oTS;

            object _internalresult = null;
            string _strSQL = "";
            try
            {

                if (PatientID > 0)
                {
                    _strSQL = "SELECT dtDOB FROM Patient WITH (NOLOCK) WHERE nPatientID = " + PatientID + "";
                    _internalresult = ExecuteScalar_Query(_strSQL);
                    if (_internalresult != null)
                    {
                        if (_internalresult.ToString() != null)
                        {
                            if (_internalresult.GetType() != typeof(System.DBNull))
                            {
                                if (_internalresult.ToString() != "")
                                {
                                    _PatientDOB = Convert.ToDateTime(_internalresult);
                                }
                            }
                        }
                    }
                }


                _result = "";

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTS.Days.ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTS.Days.ToString().Replace("-", "");

                _result = strID1 + strID2 + strID3;

                _Result = Convert.ToInt64(_result);

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            finally
            {
                _internalresult = null;
            }
            return _Result;
        }

        public void UpdatePILog(string strLogMessage)
        {

            try
            {
                System.IO.StreamWriter objFile = new System.IO.StreamWriter("C:\\Performance.log", true);
                objFile.WriteLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + "   " + strLogMessage);
                objFile.Close();
                objFile.Dispose();
                objFile = null;

            }
            catch 
            {

            }
        }

        public Int64 GetPrefixTransactionID(SqlConnection _sqlConnection, SqlTransaction _sqlTransaction, bool _useExtConn, Int64 PatientID)
        {
            Int64 _Result = 0;
            string _result = "";
            DateTime _PatientDOB = DateTime.Now;
            DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");
            SqlCommand _sqlCommand = new SqlCommand();
            string strID1 = "";
            string strID2 = "";
            string strID3 = "";

            TimeSpan oTS;

            object _internalresult = null;
            string _strSQL = "";
            try
            {

                if (PatientID > 0)
                {
                    _strSQL = "SELECT dtDOB FROM Patient WITH (NOLOCK) WHERE nPatientID = " + PatientID + "";
                    if (_useExtConn == false)
                    {
                        _internalresult = ExecuteScalar_Query(_strSQL);
                    }
                    else
                    {
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = _strSQL;
                        _sqlCommand.CommandTimeout = 0;
                        _internalresult = _sqlCommand.ExecuteScalar();
                    }

                    if (_internalresult != null)
                    {
                        if (_internalresult.ToString() != null)
                        {
                            if (_internalresult.GetType() != typeof(System.DBNull))
                            {
                                if (_internalresult.ToString() != "")
                                {
                                    _PatientDOB = Convert.ToDateTime(_internalresult);
                                }
                            }
                        }
                    }
                }


                _result = "";

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTS.Days.ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTS.Days.ToString().Replace("-", "");

                _result = strID1 + strID2 + strID3;

                _Result = Convert.ToInt64(_result);

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                //returns random number if exception occures
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            finally
            {
                if (_sqlCommand != null)
                {
                    _sqlCommand.Parameters.Clear(); _sqlCommand.Dispose(); _sqlCommand = null;
                }
                _internalresult = null;
                strID1 = null;
                strID2 = null;
                strID3 = null;
            }
            return _Result;
        }

    }

    public class DBParameters : IDisposable
    {


        private System.Collections.Specialized.OrderedDictionary _innerList = null;

        #region "Constructor & Destructor"

        public DBParameters()
        {
            _innerList = new OrderedDictionary();
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
                    disposeThis();
                }
            }
           
            disposed = true;
        }
        private void disposeThis()
        {
            if (_innerList != null)
            {
                for (int i = _innerList.Count - 1; i >= 0; i--)
                {
                    ((SqlParameter)_innerList[i]).Value = null;
                }
                _innerList.Clear();
                _innerList = null;
            }
        }

        ~DBParameters()
        {
            Dispose(false);
        }
        #endregion

        public int Count
        {
            get 
            {
                if (_innerList != null)
                {

                    return _innerList.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void Add(SqlParameter oParam)
        {
            if (_innerList == null)
            {
                _innerList = new OrderedDictionary();
            }
            _innerList.Add(oParam.ParameterName, oParam);
        }

        public void Add(DBParameter oParam)
        {
            SqlParameter osqlParam = new SqlParameter();

            osqlParam.ParameterName = oParam.ParameterName;
            osqlParam.Direction = oParam.ParameterDirection;
            osqlParam.SqlDbType = oParam.DataType;
            osqlParam.Value = oParam.Value;
            osqlParam.Size = oParam.Size;
            if (_innerList == null)
            {
                _innerList = new OrderedDictionary();
            }
            _innerList.Add(osqlParam.ParameterName, osqlParam);

            osqlParam = null;
        }

        public void Add(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype, int fieldsize)
        {
            SqlParameter oParam = new SqlParameter(parametername, value);
            oParam.SqlDbType = datatype;
            oParam.Direction = parameterdirection;
            oParam.Size = fieldsize;
            if (_innerList == null)
            {
                _innerList = new OrderedDictionary();
            }
            _innerList.Add(oParam.ParameterName, oParam);
            
        }

        public void Add(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype)
        {
            SqlParameter oParam = new SqlParameter(parametername, value);
            oParam.SqlDbType = datatype;
            oParam.Direction = parameterdirection;
            if (_innerList == null)
            {
                _innerList = new OrderedDictionary();
            }
            _innerList.Add(oParam.ParameterName, oParam);
        }

        public void Remove(SqlParameter oParam)
        {
            if (_innerList != null)
            {
                _innerList.Remove(oParam.ParameterName);
            }

        }

        public void Clear()
        {
            if (_innerList != null)
            {
                _innerList.Clear();
            }

        }

        public void ClearValues()
        {
            if (_innerList != null)
            {

                Object[] oArry = new Object[_innerList.Count];
                _innerList.Keys.CopyTo(oArry, 0);

                for (int i = 0; i < oArry.Length; i++)
                {
                    ((SqlParameter)_innerList[oArry[i]]).Value = null;
                }
             
                oArry = null;
            }

        }

        public SqlParameter this[Int32 Count]
        {
            get
            {
                ////---------For Hashtable Collection--------------
                ////SqlParameter[] oArry = new SqlParameter[_innerList.Count];
                //Object[] oArry = new Object[_innerList.Count];
                //_innerList.Keys.CopyTo(oArry, 0);

                //if (oArry.Length >= Count)
                //{
                //    return ((SqlParameter)_innerList[Convert.ToString(oArry[Count])]);
                //}
                ////---------For Hashtable Collection--------------
                if (_innerList != null)
                {

                    if (_innerList.Count >= Count)
                    {
                        return ((SqlParameter)_innerList[Count]);
                    }
                }
                return null;
            }
        }

        public SqlParameter this[String sName]
        {
            get
            {
                if (_innerList != null)
                {

                    return (SqlParameter)_innerList[sName];
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public class DBParameter : IDisposable
    {
        private string _parametername;
        private ParameterDirection _parameterdirection;
        private SqlDbType _datatype;
        private object _value;
        private int _size = 0;


        #region "Constructor & Distructor"

        public DBParameter()
        {

        }

        public DBParameter(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype, int fieldsize)
        {
            _parametername = parametername;
            _parameterdirection = parameterdirection;
            _datatype = datatype;
            _value = value;
            _size = fieldsize;
        }

        public DBParameter(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype)
        {
            _parametername = parametername;
            _parameterdirection = parameterdirection;
            _datatype = datatype;
            _value = value;
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

        ~DBParameter()
        {
            Dispose(false);
        }

        #endregion

        public string ParameterName
        {
            get { return _parametername; }
            set { _parametername = value; }
        }

        public ParameterDirection ParameterDirection
        {
            get { return _parameterdirection; }
            set { _parameterdirection = value; }
        }

        public SqlDbType DataType
        {
            get { return _datatype; }
            set { _datatype = value; }
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }
    }


    public class DBInfoMessageLayer : IDisposable
    {
        private string _conectionstring = "";
        protected bool _withtransaction = false;
        DBConnection oConnection = null;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        BackgroundWorker SpCaller = new BackgroundWorker();
        private enum executerobj
        {
            EXECUTE = 1, EXECUTESCALAR = 2, ExecuteReader = 3, EXECUTENONQUERY = 4,FillAdapter=5
        };
        private executerobj SpCallerObj = executerobj.EXECUTENONQUERY;
        private volatile bool myprocessed = false;
        private int int_result = 0;
        private object obj_result = null;
        private DataTable _dt = new DataTable();
        private DataSet _Ds = null;
        private SqlDataReader SqlDataReader = null;

        #region "Constructor & Distructor"

        public DBInfoMessageLayer(string ConnectionString)
        {
            _conectionstring = ConnectionString;
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

            SpCaller.WorkerReportsProgress = true;
            SpCaller.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SpCaller_DoWork);
            SpCaller.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.SpCaller_ProgressChanged);
            SpCaller.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SpCaller_RunWorkerCompleted);

            #endregion
        }


        private bool disposed = false;

        public void Dispose()
        {
            //Disconnect();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (oConnection != null)
                    {
                        oConnection.Dispose();
                        oConnection = null;
                    }

                }
            }
            disposed = true;
        }

        ~DBInfoMessageLayer()
        {
            Dispose(false);
        }

        #endregion

        #region "Connect & Disconnect"

        public bool Connect(bool WithTransaction)
        {
            if (oConnection != null)
            {
                oConnection.Dispose();
                oConnection = null;
            }
            oConnection = new DBConnection(_conectionstring);

            try
            {
                if (WithTransaction == true)
                {
                    oConnection.OpenConnection();
                    oConnection.BeginTransaction();
                    _withtransaction = WithTransaction;
                    return true;
                }
                else
                {
                    oConnection.OpenConnection();
                    return true;
                }
            }
            catch (DBException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
        }

        public bool CheckConnection()
        {
            bool _IsValidConnection = false;
            SqlConnection _connection = null;
            try
            {
                if (_connection != null)
                {
                    if (_connection.State != ConnectionState.Open)
                    {
                        _connection.Open();
                        _IsValidConnection = true;
                    }
                }
                else
                {
                    _connection = new SqlConnection();
                    _connection.ConnectionString = _conectionstring;
                    if (_connection.Database.ToString().Trim().Length <= 0)
                    {
                        _IsValidConnection = false;
                    }
                    _connection.Open();
                    _IsValidConnection = true;
                }
                // Disconnect();
            }
            catch (DBException)//ex)
            {
                _IsValidConnection = false;
                //ex.ToString();
                //ex = null;
            }
            catch (Exception)// ex)
            {
                _IsValidConnection = false;
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State != ConnectionState.Closed)
                    {
                        _connection.Close();
                    }
                    _connection.Dispose();
                    _connection = null;
                }
            }
            return _IsValidConnection;
        }

        public bool Disconnect()
        {
            try
            {
                if (_withtransaction == true)
                {
                    oConnection.CommitTransaction();
                    oConnection.CloseConnection();
                    return true;
                }
                else
                {
                    if (oConnection != null)  //check existence of object 
                    {
                        oConnection.CloseConnection();
                    }
                    return true;
                }
            }
            catch (DBException) // ex)
            {
                //ex.ToString();
                //ex = null;
                return false;
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
                return false;
            }
        }

        public bool Rollback()
        {
            bool _result = false;
            try
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                    oConnection.CloseConnection();
                    return true;
                }
                _result = true;
            }
            catch (DBException)// ex)
            {
                _result = false;
                //ex.ToString();
                //ex = null;
            }
            catch (Exception) // ex)
            {
                _result = false;
                //ex.ToString();
                //ex = null;
            }
            return _result;
        }

        #endregion

        #region "delegateToimplement for progressbar Update"
        
        public delegate void UpdateProgressBar(Object Sender, ProgressChangedEventArgs e);
        public event UpdateProgressBar Update_ProgressBar;
        
        #endregion

        #region BackGroundWorker
        
        private void SpCaller_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                BackgroundWorker Self = sender as BackgroundWorker;
                switch (SpCallerObj)
                {
                    case executerobj.EXECUTENONQUERY:
                        Tuple<SqlCommand> paramss_EXECUTENONQUERY = e.Argument as Tuple<SqlCommand>;
                        paramss_EXECUTENONQUERY.Item1.Connection.FireInfoMessageEventOnUserErrors = true;
                        paramss_EXECUTENONQUERY.Item1.Connection.InfoMessage += (o, args) => Self.ReportProgress(0, args.Message);
                        int_result = paramss_EXECUTENONQUERY.Item1.ExecuteNonQuery();
                        break;
                    case executerobj.ExecuteReader:
                        Tuple<SqlCommand> paramss_EXECUTEQUERY = e.Argument as Tuple<SqlCommand>;
                        paramss_EXECUTEQUERY.Item1.Connection.FireInfoMessageEventOnUserErrors = true;
                        paramss_EXECUTEQUERY.Item1.Connection.InfoMessage += (o, args) => Self.ReportProgress(0, args.Message);
                        SqlDataReader = paramss_EXECUTEQUERY.Item1.ExecuteReader();
                        break;
                    case executerobj.EXECUTESCALAR:
                        Tuple<SqlCommand> paramss_EXECUTESCALAR = e.Argument as Tuple<SqlCommand>;
                        paramss_EXECUTESCALAR.Item1.Connection.FireInfoMessageEventOnUserErrors = true;
                        paramss_EXECUTESCALAR.Item1.Connection.InfoMessage += (o, args) => Self.ReportProgress(0, args.Message);
                        obj_result = paramss_EXECUTESCALAR.Item1.ExecuteScalar();
                        break;
                    case executerobj.FillAdapter:
                        Tuple<SqlDataAdapter> paramss_FillAdapter = e.Argument as Tuple<SqlDataAdapter>;
                        paramss_FillAdapter.Item1.SelectCommand.Connection.FireInfoMessageEventOnUserErrors = true;
                        paramss_FillAdapter.Item1.SelectCommand.Connection.InfoMessage += (o, args) => Self.ReportProgress(0, args.Message);
                        _Ds = new DataSet();
                        int_result = paramss_FillAdapter.Item1.Fill(_Ds);
                        break;
                    //default:
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error in Background DoWork", ex);
            }
            finally
            {

            }

            //try
            //{
            //    paramss.Item1.Connection.InfoMessage += (o, args) => Self.ReportProgress(0, args.Message);
            //    int Result = paramss.Item1.ExecuteNonQuery();
            //    for (int _ParamCount = 0; _ParamCount <= paramss.Item1.Parameters.Count - 1; _ParamCount++)
            //    {
            //        if (paramss.Item1.Parameters[_ParamCount].Direction == ParameterDirection.Output)
            //        {
            //            paramss.Item2.Add(paramss.Item1.Parameters[_ParamCount].ParameterName, paramss.Item1.Parameters[_ParamCount].Value);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Error in Background DoWork", ex);  // ("Error in opening database connection", ex.ToString());
            //}
            //finally
            //{
            //}

        }
        
        private void SpCaller_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (Update_ProgressBar != null)
            {
                Update_ProgressBar(sender, e);
            }
        }

        private void SpCaller_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            myprocessed = true;
        }
        #endregion

        #region "Insert Update Delete - Stored Procedure"

        public int Execute(string StoredProcedureName, DBParameters Parameters)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;



                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }
               
                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.EXECUTENONQUERY;
             //   SpCaller.WorkerReportsProgress = true;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }

                _result = int_result;

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {

                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}

            }
            return _result;
        }

        //Added by Mahesh Satlapalli (Apollo) on 2011-06-27(yyyy-mm-dd) - To Support Transaction
        public int ExecuteWithTransaction(string StoredProcedureName, DBParameters Parameters)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            if (_withtransaction)
            {
                _sqlcommand.Transaction = oConnection.GetTransaction();
            }

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }
                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.EXECUTENONQUERY;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = int_result;

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (Exception ex)
            {

                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }
            return _result;
        }

        public Hashtable GetOutParamResults(SqlCommand _sqlcommand)
        {
            Hashtable oOutParam = new Hashtable();
            try
            {
                for (int _ParamCount = 0; _ParamCount <= _sqlcommand.Parameters.Count - 1; _ParamCount++)
                {
                    if (_sqlcommand.Parameters[_ParamCount].Direction == ParameterDirection.Output)
                    {
                        oOutParam.Add(_sqlcommand.Parameters[_ParamCount].ParameterName, _sqlcommand.Parameters[_ParamCount].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oOutParam;
        }

        public Hashtable Execute(string StoredProcedureName, DBParameters Parameters, Boolean bIsOutParamExist)
        {
            int _counter = 0;
           // int result = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            Hashtable oOutParam = new Hashtable();
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                if (bIsOutParamExist)
                {
                    for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                    {
                        _sqlcommand.Parameters.Add(Parameters[_counter]);
                    }
                
                    myprocessed = false;
                    var paramss = Tuple.Create<SqlCommand>(_sqlcommand);

                     SpCallerObj = executerobj.EXECUTENONQUERY;
                     BackgroundWorker SpCaller2 = new BackgroundWorker();
                     SpCaller2.WorkerReportsProgress = true;
                     SpCaller2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SpCaller_DoWork);

                     SpCaller2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.SpCaller_ProgressChanged);
                     SpCaller2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SpCaller_RunWorkerCompleted);

                     SpCaller2.RunWorkerAsync(paramss);

                    while (myprocessed == false)
                    {
                        Application.DoEvents();
                       // Thread.Sleep(10);
                    }

                    for (int _ParamCount = 0; _ParamCount <= _sqlcommand.Parameters.Count - 1; _ParamCount++)
                    {
                        if (_sqlcommand.Parameters[_ParamCount].Direction == ParameterDirection.Output)
                        {
                            oOutParam.Add(_sqlcommand.Parameters[_ParamCount].ParameterName, _sqlcommand.Parameters[_ParamCount].Value);
                        }
                    }
                   
                }
            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }
            return oOutParam;
        }

        public int Execute(string StoredProcedureName)
        {
            int _result = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;
             
                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.EXECUTENONQUERY;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = int_result;
               // _result = _sqlcommand.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }

            }
            return _result;
        }

        public int Execute(string StoredProcedureName, DBParameters Parameters, out object ParameterValue)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            int _outputCounter = 0;
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;


                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    if (Parameters[_counter].Direction == ParameterDirection.Output || Parameters[_counter].Direction == ParameterDirection.InputOutput)
                    {
                        _outputCounter = _counter;
                    }

                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }

                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.EXECUTENONQUERY;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = int_result;
                //_result = _sqlcommand.ExecuteNonQuery();

                if (_sqlcommand.Parameters[_outputCounter].Value != null)
                {
                    ParameterValue = _sqlcommand.Parameters[_outputCounter].Value;
                }
                else
                {
                    ParameterValue = 0;
                }

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (Exception ex)
            {

                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }
            return _result;
        }

        public int Execute(string StoredProcedureName, DBParameters Parameters, out object ParameterValue1, out object ParameterValue2)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            String _outputParamName1 = "";
            String _outputParamName2 = "";

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;


                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    if (Parameters[_counter].Direction == ParameterDirection.Output || Parameters[_counter].Direction == ParameterDirection.InputOutput)
                    {
                        if (_outputParamName1 == "")
                        { _outputParamName1 = Parameters[_counter].ParameterName; }
                        else if (_outputParamName2 == "")
                        { _outputParamName2 = Parameters[_counter].ParameterName; }
                    }

                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }


               // _result = _sqlcommand.ExecuteNonQuery();
                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.EXECUTENONQUERY;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = int_result;

                if (_sqlcommand.Parameters[_outputParamName1].Value != null)
                { ParameterValue1 = _sqlcommand.Parameters[_outputParamName1].Value; }
                else { ParameterValue1 = 0; }

                if (_sqlcommand.Parameters[_outputParamName2].Value != null)
                { ParameterValue2 = _sqlcommand.Parameters[_outputParamName2].Value; }
                else
                { ParameterValue2 = 0; }

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (Exception ex)
            {

                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }
            return _result;
        }

        public object ExecuteScalar(string StoredProcedureName, DBParameters Parameters)
        {
            object _result = null;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {


                    _sqlcommand.Parameters.Add(Parameters[_counter]);

                }

              //  _result = _sqlcommand.ExecuteScalar();
                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.EXECUTESCALAR;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = obj_result;

                if (_result == null)
                {
                    _result = "";
                }

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }

            return _result;
        }

        public object ExecuteScalar(string StoredProcedureName)
        {
            object _result = null;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.EXECUTESCALAR;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = obj_result;

                //_result = _sqlcommand.ExecuteScalar();

                if (_result == null)
                {
                    _result = "";
                }

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
            }

            return _result;
        }

        #endregion

        #region "Insert Update Delete - SQL Query"

        public int Execute_Query(string SQLQuery)
        {
            int _result = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

               // _result = _sqlcommand.ExecuteNonQuery();
                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.EXECUTENONQUERY;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = int_result;

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
            }
            return _result;
        }

        public object ExecuteScalar_Query(string SQLQuery)
        {
            object _result = null;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                //_result = _sqlcommand.ExecuteScalar();
                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.EXECUTESCALAR;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = obj_result;

                if (_result == null)
                {
                    _result = "";
                }

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());

            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
            }

            return _result;
        }

        public object ExecuteScalar_Query(string SQLQuery, out string ErrorMessage)
        {
            object _result = null;
            SqlCommand _sqlcommand = new SqlCommand();
            ErrorMessage = "";
            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                //_result = _sqlcommand.ExecuteScalar();
                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.EXECUTESCALAR;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = obj_result;

                if (_result == null)
                {
                    _result = "";
                }

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                ErrorMessage = ex.ToString();

            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                ErrorMessage = ex.ToString();

            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                ErrorMessage = ex.ToString();

            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear();
                    _sqlcommand.Dispose();
                    _sqlcommand = null;
                }
            }

            return _result;
        }

        #endregion

        #region "Retrive Data - Stored Procedure"

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out SqlDataReader _result)
        {

            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }

               // _result = _sqlcommand.ExecuteReader();
                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.ExecuteReader;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = SqlDataReader;

            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }

        }

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataSet _result)
        {
            _result = null;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {

                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }


                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                _result = new DataSet();

                myprocessed = false;
                var paramss = Tuple.Create<SqlDataAdapter>(_dataAdapter);
                SpCallerObj = executerobj.FillAdapter;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = _Ds.Copy();

                //_dataAdapter.Fill(_result);
                _dataAdapter.Dispose();

                //                _result = _resultinternal;
            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }

        }

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataTable _result)
        {
            _result = null;
            int _counter = 0;

            SqlCommand _sqlcommand = new SqlCommand();
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                DataSet _dataset = new DataSet();
                //      DataTable _resultinternal = null; // new DataTable();

               // _dataAdapter.Fill(_dataset);
                myprocessed = false;
                var paramss = Tuple.Create<SqlDataAdapter>(_dataAdapter);
                SpCallerObj = executerobj.FillAdapter;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                //_result = _Ds.Tables[0];
                _dataset = _Ds.Copy();
                if (_dataset.Tables != null)
                {
                    if (_dataset.Tables.Count > 0)
                    {
                        if (_dataset.Tables[0] != null)
                        {
                            //                        _resultinternal = _dataset.Tables[0];
                            _result = _dataset.Tables[0].Copy();
                        }

                    }
                }
                //                _result = _resultinternal;   
                //word crash and memory leak changes reverted
                //                _resultinternal.Dispose();                
                _dataset.Dispose();
                _dataAdapter.Dispose();
            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }

        }

        public void RetriveUsingDataReader(string StoredProcedureName, DBParameters Parameters, out DataTable _result)
        {
            SqlCommand _sqlcommand = new SqlCommand();
            SqlDataReader _dataAdapter = null;
            _result = null;
            int _counter = 0;

            try
            {

                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    _sqlcommand.Parameters.Add(Parameters[_counter]);
                }

                _dataAdapter = _sqlcommand.ExecuteReader(CommandBehavior.SingleResult);

                _result = new DataTable();
                _result.Load(_dataAdapter);
                _dataAdapter.Close();
            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    if (_sqlcommand.Parameters != null) { _sqlcommand.Parameters.Clear(); } _sqlcommand.Dispose(); _sqlcommand = null;
                }
                if (_dataAdapter != null) { _dataAdapter.Dispose(); _dataAdapter = null; }

            }

        }

        //Added in 6000 .This method will return two out parameters,datatable and error message.
        public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataTable _result, out string _ErrMsg)
        {
            _result = null;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                _ErrMsg = "";

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {

                    _sqlcommand.Parameters.Add(Parameters[_counter]);

                }

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                DataSet _dataset = new DataSet();

               // _dataAdapter.Fill(_dataset);
                
                myprocessed = false;
                var paramss = Tuple.Create<SqlDataAdapter>(_dataAdapter);
                SpCallerObj = executerobj.FillAdapter;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _dataset = _Ds.Copy();

                if (_dataset.Tables != null)
                {
                    if (_dataset.Tables.Count > 0)
                    {
                        if (_dataset.Tables[0] != null)
                        {
                            //                        _resultinternal = _dataset.Tables[0];
                            _result = _dataset.Tables[0].Copy();
                        }

                    }
                }

                if (_sqlcommand.Parameters["@sErrMessage"].Value != null)
                { _ErrMsg = (_sqlcommand.Parameters["@sErrMessage"].Value).ToString(); }

                _dataset.Dispose();
                _dataAdapter.Dispose();


            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
                //if (Parameters != null)
                //{
                //    Parameters.Dispose();
                //    Parameters = null;
                //}
            }

        }

        // Without Parameters //
        public void Retrive(string StoredProcedureName, out SqlDataReader _result)
        {
            _result = null;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                //_result = _sqlcommand.ExecuteReader();
                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.ExecuteReader;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = SqlDataReader;

            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
            }

        }

        public void Retrive(string StoredProcedureName, out DataSet _result)
        {
            _result = null;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                _result = new DataSet();
               
                myprocessed = false;
                var paramss = Tuple.Create<SqlDataAdapter>(_dataAdapter);
                SpCallerObj = executerobj.FillAdapter;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = _Ds.Copy();

               // _dataAdapter.Fill(_result);

                _dataAdapter.Dispose();


            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
            }

        }

        public void Retrive(string StoredProcedureName, out DataTable _result)
        {

            _result = null;
            SqlCommand _sqlcommand = null;

            try
            {
                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                DataSet _dataset = new DataSet();

              //  _dataAdapter.Fill(_dataset);
                myprocessed = false;
                var paramss = Tuple.Create<SqlDataAdapter>(_dataAdapter);
                SpCallerObj = executerobj.FillAdapter;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _dataset = _Ds.Copy();
                if (_dataset.Tables != null)
                {
                    if (_dataset.Tables.Count > 0)
                    {
                        if (_dataset.Tables[0] != null)
                        {
                            //                        _resultinternal = _dataset.Tables[0];
                            _result = _dataset.Tables[0].Copy();
                        }

                    }
                }
                _dataset.Dispose();
                _dataAdapter.Dispose();


            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
            }

        }

        #endregion

        #region "Retrive Data - SQL Query"

        public void Retrive_Query(string SQLQuery, out SqlDataReader _result)
        {
            _result = null;
            SqlCommand _sqlcommand = null;

            try
            {
                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

               // _result = _sqlcommand.ExecuteReader();
                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.ExecuteReader;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = SqlDataReader;

            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
            }
        }

        public void Retrive_Query(string SQLQuery, out DataSet _result)
        {
            _result = null;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                _result = new DataSet();

                //_dataAdapter.Fill(_result);

                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.FillAdapter;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _result = _Ds.Copy();

                _dataAdapter.Dispose();


            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
            }
        }

        public void Retrive_Query(string SQLQuery, out DataTable _result)
        {
            _result = null;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                DataSet _dataset = new DataSet();

                //_dataAdapter.Fill(_dataset);
                myprocessed = false;
                var paramss = Tuple.Create<SqlCommand>(_sqlcommand);
                SpCallerObj = executerobj.FillAdapter;
                SpCaller.RunWorkerAsync(paramss);

                while (myprocessed == false)
                {
                    Application.DoEvents();
                }
                _dataset = _Ds.Copy();
                if (_dataset.Tables != null)
                {
                    if (_dataset.Tables.Count > 0)
                    {
                        if (_dataset.Tables[0] != null)
                        {
                            //                        _resultinternal = _dataset.Tables[0];
                            _result = _dataset.Tables[0].Copy();
                        }

                    }
                }
                _dataset.Dispose();
                _dataAdapter.Dispose();


            }
            catch (SqlException ex)
            {

                throw new DBException("Error in database execution. ", ex.ToString());
            }
            catch (DBException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution.  ", ex.ToString());
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand = null;
                }
            }
        }

        #endregion

        public SqlCommand GetCmdParameters(gloDatabaseLayer.DBParameters Parameters)
        {
            SqlCommand _sqlCommand = new SqlCommand();

            try
            {
                if (Parameters != null && Parameters.Count > 0)
                {
                    for (int _counter = 0; _counter <= Parameters.Count - 1; _counter++)
                    {
                        _sqlCommand.Parameters.Add(Parameters[_counter]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _sqlCommand;
        }

        public Int64 GetPrefixTransactionID(Int64 PatientID)
        {
            Int64 _Result = 0;
            string _result = "";
            DateTime _PatientDOB = DateTime.Now;
            DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

            string strID1 = "";
            string strID2 = "";
            string strID3 = "";

            TimeSpan oTS;

            object _internalresult = null;
            string _strSQL = "";
            try
            {

                if (PatientID > 0)
                {
                    _strSQL = "SELECT dtDOB FROM Patient WITH (NOLOCK) WHERE nPatientID = " + PatientID + "";
                    _internalresult = ExecuteScalar_Query(_strSQL);
                    if (_internalresult != null)
                    {
                        if (_internalresult.ToString() != null)
                        {
                            if (_internalresult.GetType() != typeof(System.DBNull))
                            {
                                if (_internalresult.ToString() != "")
                                {
                                    _PatientDOB = Convert.ToDateTime(_internalresult);
                                }
                            }
                        }
                    }
                }


                _result = "";

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTS.Days.ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTS.Days.ToString().Replace("-", "");

                _result = strID1 + strID2 + strID3;

                _Result = Convert.ToInt64(_result);

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            finally
            {
                _internalresult = null;
                strID1 = null;
                strID2 = null;
                strID3 = null;
            }
            return _Result;
        }

        public void UpdatePILog(string strLogMessage)
        {

            try
            {
                System.IO.StreamWriter objFile = new System.IO.StreamWriter("C:\\Performance.log", true);
                objFile.WriteLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + "   " + strLogMessage);
                objFile.Close();
                objFile.Dispose();
                objFile = null;

            }
            catch
            {

            }
        }

        public Int64 GetPrefixTransactionID(SqlConnection _sqlConnection, SqlTransaction _sqlTransaction, bool _useExtConn, Int64 PatientID)
        {
            Int64 _Result = 0;
            string _result = "";
            DateTime _PatientDOB = DateTime.Now;
            DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");
            SqlCommand _sqlCommand = new SqlCommand();
            string strID1 = "";
            string strID2 = "";
            string strID3 = "";

            TimeSpan oTS;

            object _internalresult = null;
            string _strSQL = "";
            try
            {

                if (PatientID > 0)
                {
                    _strSQL = "SELECT dtDOB FROM Patient WITH (NOLOCK) WHERE nPatientID = " + PatientID + "";
                    if (_useExtConn == false)
                    {
                        _internalresult = ExecuteScalar_Query(_strSQL);
                    }
                    else
                    {
                        _sqlCommand.Connection = _sqlConnection;
                        _sqlCommand.Transaction = _sqlTransaction;
                        _sqlCommand.CommandType = CommandType.Text;
                        _sqlCommand.CommandText = _strSQL;
                        _sqlCommand.CommandTimeout = 0;
                        _internalresult = _sqlCommand.ExecuteScalar();
                    }

                    if (_internalresult != null)
                    {
                        if (_internalresult.ToString() != null)
                        {
                            if (_internalresult.GetType() != typeof(System.DBNull))
                            {
                                if (_internalresult.ToString() != "")
                                {
                                    _PatientDOB = Convert.ToDateTime(_internalresult);
                                }
                            }
                        }
                    }
                }


                _result = "";

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTS.Days.ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTS.Days.ToString().Replace("-", "");

                _result = strID1 + strID2 + strID3;

                _Result = Convert.ToInt64(_result);

            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
                //returns random number if exception occures
                Random oRan = new Random();
                return oRan.Next(1, Int32.MaxValue);
            }
            finally
            {
                if (_sqlCommand != null)
                {
                    _sqlCommand.Parameters.Clear(); _sqlCommand.Dispose(); _sqlCommand = null;
                }
                _internalresult = null;
                strID1 = null;
                strID2 = null;
                strID3 = null;
                _strSQL = null;
            }
            return _Result;
        }

    }
    #region " Class CUtility "
    /// <summary>
    /// ************************************************
    /// Class Name: CUtility.
    /// Created By: Debasish Das.
    /// Date of Creation: 17th May 2010.
    /// Modified By:
    /// Date of Modification:
    /// ************************************************
    /// </summary>
    public class CUtility : IDisposable
    {

        #region "Public and Private Variables "

        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion

        #region "Constructor & Distructor"

        public CUtility()
        {
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

        ~CUtility()
        {
            Dispose(false);
        }

        #endregion

        #region " Generate Procedure Executation Query "

        public static string getProcedureExeCode(string StoredProcedureName, DBParameters Parameters)
        {
            string sDeclareStatements = "";
            string sSelectStatements = "";
            string sExecuationStatement = "";
            string sSelectOutPutVariables = "";
            try
            {
                if (Parameters != null)
                {
                    sDeclareStatements = getDeclareStatements(Parameters);
                    sSelectStatements = getSelectStatements(Parameters);
                    sExecuationStatement = getExecuationStatement(StoredProcedureName, Parameters);
                    sSelectOutPutVariables = getSelectOutPutVariables(Parameters);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error While Creating Procedure Call " + Environment.NewLine + ex.ToString(), "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

            }
            return Environment.NewLine + "Execuating Procedure ' " + StoredProcedureName + " '"
                + Environment.NewLine + "Date: " + DateTime.Now + Environment.NewLine + Environment.NewLine + "BEGIN TRANSACTION"
                + Environment.NewLine + sDeclareStatements + Environment.NewLine + sSelectStatements
                + Environment.NewLine + sExecuationStatement + sSelectOutPutVariables + "ROLLBACK";
        }

        public static string getDeclareStatements(DBParameters Parameters)
        {
            string sReturnString = "";
            int _counter = 0;
            for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
            {
                sReturnString += "DECLARE " + Parameters[_counter].ParameterName;


                if (Parameters[_counter].SqlDbType == SqlDbType.BigInt)
                {
                    sReturnString += " BigInt";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Binary)
                {
                    sReturnString += " Binary(" + Parameters[_counter].Size + ")";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Bit)
                {
                    sReturnString += " Bit";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Char)
                {
                    if (Parameters[_counter].Size == 0)
                    {
                        sReturnString += " Char";
                    }
                    else
                    {
                        sReturnString += " Char(" + Parameters[_counter].Size + ")";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Date)
                {
                    sReturnString += " Date";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.DateTime)
                {
                    sReturnString += " DateTime";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.DateTime2)
                {
                    sReturnString += " DateTime2";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.DateTimeOffset)
                {
                    sReturnString += " DateTimeOffset";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Decimal)
                {
                    if (Parameters[_counter].Size == 0)
                    {
                        sReturnString += " Decimal(18,18)";
                    }
                    else
                    {
                        sReturnString += " Decimal(" + Parameters[_counter].Size + ",5)";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Float)
                {
                    sReturnString += " Float";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Image)
                {
                    sReturnString += " Image";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Int)
                {
                    sReturnString += " Int";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Money)
                {
                    sReturnString += " Money";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.NChar)
                {
                    if (Parameters[_counter].Size == 0)
                    {
                        sReturnString += " NChar";
                    }
                    else
                    {
                        sReturnString += " NChar(" + Parameters[_counter].Size + ")";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.NText)
                {
                    sReturnString += " NText";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.NVarChar)
                {
                    if (Parameters[_counter].Size == 0)
                    {
                        sReturnString += " NVarChar";
                    }
                    else
                    {
                        sReturnString += " NVarChar(" + Parameters[_counter].Size + ")";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Real)
                {
                    sReturnString += " Real";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.SmallDateTime)
                {
                    sReturnString += " SmallDateTime";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.SmallInt)
                {
                    sReturnString += " SmallInt";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.SmallMoney)
                {
                    sReturnString += " SmallMoney";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Structured)
                {
                    sReturnString += " Structured";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Text)
                {
                    sReturnString += " Text";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Time)
                {
                    sReturnString += " Time";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Timestamp)
                {
                    sReturnString += " Timestamp";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.TinyInt)
                {
                    sReturnString += " TinyInt";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Udt)
                {
                    sReturnString += " Udt";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.UniqueIdentifier)
                {
                    sReturnString += " UniqueIdentifier";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.VarBinary)
                {
                    if (Parameters[_counter].Size == 0)
                    {
                        sReturnString += " VarBinary";
                    }
                    else
                    {
                        sReturnString += " VarBinary(" + Parameters[_counter].Size + ")";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.VarChar)
                {
                    if (Parameters[_counter].Size == 0)
                    {
                        sReturnString += " VarChar";
                    }
                    else
                    {
                        sReturnString += " VarChar(" + Parameters[_counter].Size + ")";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Variant)
                {
                    sReturnString += " Variant";
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Xml)
                {
                    sReturnString += " Xml";
                    sReturnString += Environment.NewLine;
                }

            }
            return sReturnString;
        }

        public static string getSelectStatements(DBParameters Parameters)
        {
            string sReturnString = "";
            int _counter;
            for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
            {
                sReturnString += "SELECT " + Parameters[_counter].ParameterName + " = ";

                if (Parameters[_counter].SqlDbType == SqlDbType.BigInt)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Binary)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Bit)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Char)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Date)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.DateTime)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.DateTime2)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.DateTimeOffset)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" ||Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Decimal)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Float)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Image)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Int)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Money)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.NChar)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.NText)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null ||Convert.ToString(Parameters[_counter].Value) == "" ||Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.NVarChar)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Real)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.SmallDateTime)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.SmallInt)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.SmallMoney)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Structured)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Text)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Time)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Timestamp)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.TinyInt)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Udt)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.UniqueIdentifier)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += Parameters[_counter].Value;
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.VarBinary)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.VarChar)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Variant)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null)
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
                else if (Parameters[_counter].SqlDbType == SqlDbType.Xml)
                {
                    if (Parameters[_counter].Value == DBNull.Value || Parameters[_counter].Value == null || Convert.ToString(Parameters[_counter].Value) == "" || Convert.ToString(Parameters[_counter].Value) == " ")
                    {
                        sReturnString += " NULL";
                    }
                    else
                    {
                        sReturnString += "'" + Parameters[_counter].Value + "'";
                    }
                    sReturnString += Environment.NewLine;
                }
            }
            return sReturnString;
        }

        public static string getExecuationStatement(string sProcName, DBParameters Parameters)
        {
            string sReturnString = "";
            int _counter;
            sReturnString += "EXEC  " + sProcName + " ";
            for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
            {
                sReturnString += Parameters[_counter].ParameterName;
                if (Parameters[_counter].Direction == ParameterDirection.Output || Parameters[_counter].Direction == ParameterDirection.InputOutput)
                {
                    sReturnString += " OUTPUT";
                }
                sReturnString += ",";
            }
            sReturnString = sReturnString.Remove(sReturnString.Length - 1);
            sReturnString += Environment.NewLine;

            return sReturnString;
        }
        public static string getSelectOutPutVariables(DBParameters Parameters)
        {
            string sReturnString = "";
            int _counter;

            sReturnString += "SELECT ";
            for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
            {

                if (Parameters[_counter].Direction == ParameterDirection.Output || Parameters[_counter].Direction == ParameterDirection.InputOutput)
                {
                    sReturnString += Parameters[_counter].ParameterName + ",";
                }
            }
            if (sReturnString.Length > 7)
            {
                sReturnString = sReturnString.Remove(sReturnString.Length - 1);
                sReturnString += Environment.NewLine;
            }
            else
            {
                sReturnString = "";
            }
            return sReturnString;
        }
        #endregion
    }
    #endregion

    #region " Class clsCommon "
    /// <summary>
    /// ************************************************
    /// Class Name: clsCommon.
    /// Created By: Yogesh.
    /// Date of Creation: 05th Nov 2012.
    /// Modified By:
    /// Date of Modification:
    /// ************************************************
    /// </summary>
    public class clsCommon : IDisposable
    {

        #region "Public and Private Variables "

        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #endregion

        #region "Constructor & Distructor"

        public clsCommon()
        {
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

        ~clsCommon()
        {
            Dispose(false);
        }

        #endregion

        #region " GetOSInfo "
         
        //Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER
        //Following Function added to check OS.
        public static bool GetOSInfo(out bool IsExecuted)
        {            
            bool  bReturn  = false;
            try
            {                
                using (ManagementObjectSearcher objMOS = new ManagementObjectSearcher("SELECT * FROM  Win32_OperatingSystem"))
                {
                    foreach (ManagementObject objMgmt in objMOS.Get())
                    {
                        char[] sep = { '|' };
                        string[] strOsName = objMgmt["Name"].ToString().Split(sep);
                        if (strOsName.Length > 0)
                        {
                            ///'Change for 64 - bit server
                            //If strOsName(0).Contains("Microsoft Windows Server") Then
                            if (strOsName[0].Contains("Microsoft") & strOsName[0].Contains("Windows") & strOsName[0].Contains("Server"))
                            {
                                bReturn = true;
                                break; //Added by SLR: on 5/8/2014
                            }
                            else
                            {
                                bReturn = false;
                            }
                        }
                        else
                        {
                            bReturn = false;
                        }
                        strOsName = null;
                    }
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            IsExecuted = true;
            return bReturn; 
        }
        
        #endregion
    }
    #endregion
    #region "Old Classes"


    #endregion
}
