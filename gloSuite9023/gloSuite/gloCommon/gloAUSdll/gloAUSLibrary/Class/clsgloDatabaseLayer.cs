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

namespace gloAUSLibrary
{

    //************ EXCEPTION CLASS ********************
    public class DBException : System.ApplicationException
    {
        private int no;
        private string description;
        private Exception sourceException;
        private static  string _messageBoxCaption = String.Empty;
        private static RegistryKey regkey = null;

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
                    _messageBoxCaption = "gloClientUpdates Manager";
                }
            }
            else
            { _messageBoxCaption = "gloClientUpdates Manager"; }

            #endregion
        }

        private static object GetRegistryValue(string KeyName)
        {
            regkey = Registry.LocalMachine;
            RegistryKey rkey = regkey.OpenSubKey("Software\\" + _messageBoxCaption);
            object myobject = null;
            if (rkey != null)
            {
                myobject = rkey.GetValue(KeyName);
                try
                {

                    rkey.Close();
                    rkey.Dispose();
                }
                catch
                {
                }
            }
            return myobject;

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
                    _messageBoxCaption = "gloClientUpdates Manager";
                }
            }
            else
            { _messageBoxCaption = "gloClientUpdates Manager"; }

            #endregion
        }

        public void ERROR_Log(string strLogMessage)
        {
            Boolean flgDBErr = true;
            
            if (DBException.GetRegistryValue("EnableErrorLogs") != null)
            {
                flgDBErr = Convert.ToBoolean(DBException.GetRegistryValue("EnableErrorLogs"));
            }

            if (flgDBErr == true)
            {
                try
                {
                    if (Directory.Exists(Application.StartupPath + "\\Log\\DBErrorLog") == false)
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\Log\\DBErrorLog");
                    }
                }
                catch (Exception)
                {
                    if (Directory.Exists(Path.GetTempPath() + "InstallationLogs\\gloClientAUSUpdatesLog\\DBErrorLog") == false)
                    {
                        Directory.CreateDirectory(Path.GetTempPath() + "InstallationLogs\\gloClientAUSUpdatesLog\\DBErrorLog");
                    }
                }
                string strMessage = Environment.NewLine + "" +
                                       System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + Environment.NewLine +
                                       strLogMessage + Environment.NewLine;

                string _fileName = "DBErrorLog " + DateTime.Now.Date.ToString("MM-dd-yyyy") + ".log";

                try
                {
                    File.AppendAllText(Application.StartupPath + "\\Log\\DBErrorLog\\" + _fileName, strMessage);
                }
                catch (Exception)
                {
                    File.AppendAllText(Path.GetTempPath() + "InstallationLogs\\gloClientAUSUpdatesLog\\DBErrorLog\\" + _fileName, strMessage);
                }

                //MessageBox.Show("Error while accessing Database. Please click on Help to view log.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0, Application.StartupPath + "\\Log\\DBErrorLog\\" + _fileName);
                clsGeneral.UpdateLog("Error while accessing Database. Please click on Help to view log.");
            }
            else
            {
                clsGeneral.UpdateLog(strLogMessage);
                //MessageBox.Show(strLogMessage, _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
                    _messageBoxCaption = "gloClientUpdates Manager";
                }
            }
            else
            { _messageBoxCaption = "gloClientUpdates Manager"; }

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

        public bool BeginTransaction()
        {
            try
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _transaction = _connection.BeginTransaction();
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
                    _transaction.Commit();
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
                    _transaction.Rollback();
                    _transaction = null; 
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
        DBConnection oConnection;
        private string _messageBoxCaption = String.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        #region "Constructor & Distructor"

        public DBLayer(string ConnectionString)
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
                    _messageBoxCaption = "gloClientUpdates Manager";
                }
            }
            else
            { _messageBoxCaption = "gloClientUpdates Manager"; }

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
            catch (DBException ex)
            {
                _IsValidConnection = false;
                clsGeneral.UpdateLog("Error while checking database connection :"+ex.Message.ToString());
                ex = null;
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("Error while checking database connection :"+ex.Message.ToString());
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
                    oConnection.CloseConnection();
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
            catch (DBException) // ex)
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
            }

        }

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataSet _result)
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


                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                DataSet _resultinternal = new DataSet();


                _dataAdapter.Fill(_resultinternal);
                _dataAdapter.Dispose();

                _result = _resultinternal;
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

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataTable _result)
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

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                DataSet _dataset = new DataSet();
                DataTable _resultinternal = new DataTable();

                _dataAdapter.Fill(_dataset);
                if (_dataset.Tables.Count > 0)
                {
                    if (_dataset.Tables[0] != null)
                    {
                        _resultinternal = _dataset.Tables[0];
                    }
                }
                _result = _resultinternal;

                _resultinternal.Dispose();
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

        //Added in 6000 .This method will return two out parameters,datatable and error message.
        public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataTable _result, out string _ErrMsg)
        {

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
                DataTable _resultinternal = new DataTable();

                _dataAdapter.Fill(_dataset);
                if (_dataset.Tables != null && _dataset.Tables.Count > 0 && _dataset.Tables[0] != null)
                {
                    _resultinternal = _dataset.Tables[0];
                }
                _result = _resultinternal;

                if (_sqlcommand.Parameters["@sErrMessage"].Value != null)
                { _ErrMsg = (_sqlcommand.Parameters["@sErrMessage"].Value).ToString(); }

                _resultinternal.Dispose();
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

        // Without Parameters //
        public void Retrive(string StoredProcedureName, out SqlDataReader _result)
        {

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

            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                DataSet _resultinternal = new DataSet();


                _dataAdapter.Fill(_resultinternal);
                _dataAdapter.Dispose();

                _result = _resultinternal;
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
                DataTable _resultinternal = new DataTable();

                _dataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null)
                {
                    _resultinternal = _dataset.Tables[0];
                }
                _result = _resultinternal;

                _resultinternal.Dispose();
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
            SqlCommand _sqlcommand = null;

            try
            {
                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                DataSet _resultinternal = new DataSet();

                _dataAdapter.Fill(_resultinternal);
                _dataAdapter.Dispose();

                _result = _resultinternal;
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
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose();_sqlcommand=null  ;
                }
            }
        }

        public void Retrive_Query(string SQLQuery, out DataTable _result)
        {
            SqlCommand _sqlcommand = null;

            try
            {
                _sqlcommand = new SqlCommand();
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();
                _sqlcommand.CommandTimeout = 0;

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                DataSet _dataset = new DataSet();
                DataTable _resultinternal = new DataTable();

                _dataAdapter.Fill(_dataset);
                if (_dataset.Tables[0] != null)
                {
                    _resultinternal = _dataset.Tables[0];
                }
                _result = _resultinternal;

                _resultinternal.Dispose();
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
                    _sqlcommand.Parameters.Clear(); _sqlcommand.Dispose(); _sqlcommand=null;
                }
            }
        }

        #endregion

        public SqlCommand GetCmdParameters(DBParameters Parameters)
        {
            SqlCommand _sqlCommand = null;

            try
            {
                _sqlCommand = new SqlCommand();
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
                objFile = null;

            }
            catch (Exception)
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
            catch (Exception) // ex)
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
                    _sqlCommand.Parameters.Clear();
                    _sqlCommand.Dispose();
                    _sqlCommand = null;
                }

                _internalresult = null;
            }
            return _Result;
        }

    }

    public class DBParameters : IDisposable
    {


        private System.Collections.Specialized.OrderedDictionary _innerList;

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

                }
            }
            disposed = true;
        }


        ~DBParameters()
        {
            Dispose(false);
        }
        #endregion

        public int Count
        {
            get { return _innerList.Count; }
        }

        public void Add(SqlParameter oParam)
        {
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
            _innerList.Add(osqlParam.ParameterName, osqlParam);

            osqlParam = null;
        }

        public void Add(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype, int fieldsize)
        {
            SqlParameter oParam = new SqlParameter(parametername, value);
            oParam.SqlDbType = datatype;
            oParam.Direction = parameterdirection;
            oParam.Size = fieldsize;

            _innerList.Add(oParam.ParameterName, oParam);
        }

        public void Add(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype)
        {
            SqlParameter oParam = new SqlParameter(parametername, value);
            oParam.SqlDbType = datatype;
            oParam.Direction = parameterdirection;

            _innerList.Add(oParam.ParameterName, oParam);
        }

        public void Remove(SqlParameter oParam)
        {

            _innerList.Remove(oParam.ParameterName);

        }

        public void Clear()
        {
            _innerList.Clear();

        }

        public void ClearValues()
        {
            Object[] oArry = new Object[_innerList.Count];
            _innerList.Keys.CopyTo(oArry, 0);

            for (int i = 0; i < oArry.Length; i++)
            {
                ((SqlParameter)_innerList[oArry[i]]).Value = null;
            }

        }

        public SqlParameter this[Int32 Count]
        {
            get
            {
                if (_innerList.Count >= Count)
                {
                    return ((SqlParameter)_innerList[Count]);
                }

                return null;
            }
        }

        public SqlParameter this[String sName]
        {
            get
            {

                return (SqlParameter)_innerList[sName];
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
                    _messageBoxCaption = "gloClientUpdates Manager";
                }
            }
            else
            { _messageBoxCaption = "gloClientUpdates Manager"; }

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
                MessageBox.Show("Error While Creating Procedure Call " + Environment.NewLine + ex.ToString(), "gloClientUpdates Manager", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

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
       
}
