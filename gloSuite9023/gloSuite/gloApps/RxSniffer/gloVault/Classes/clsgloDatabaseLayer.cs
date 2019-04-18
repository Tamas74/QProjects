using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;


namespace gloDatabaseLayer
{

    //************ EXCEPTION CLASS ********************
    public class DBException : System.ApplicationException
    {
        private int no;
        private string description;
        private Exception sourceException;

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

        public DBException(string sErrorMessage, String ActualError) : base(sErrorMessage)
        {
            ERROR_Log(ActualError);
        }

        public DBException(string sErrorMessage, String ActualError, System.Exception inner): base(sErrorMessage, inner)
        {
            ERROR_Log(ActualError );
        }

        public void ERROR_Log(string strLogMessage)
        {
                System.IO.StreamWriter objFile = new System.IO.StreamWriter(Application.StartupPath + "\\gloDB_ERRORLog.log", true);
                objFile.WriteLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + "   "  + strLogMessage + '\n');
                objFile.Close();
                objFile = null;

                //MessageBox.Show("Error while accessing Database. Please click on Help to view log.", "gloFaxService", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0, Application.StartupPath + "\\gloDB_ERRORLog.log");
        }
        
        
    }

    //************ CONNECTION CLASS *******************

    internal class DBConnection : IDisposable
    {

        private string _conectionstring = "";
        protected SqlConnection _connection = null;
        protected SqlTransaction _transaction = null;

        #region "Constructor & Distructor"

        public DBConnection(string ConnectionString)
        {
            _connection = new SqlConnection();
            _connection.ConnectionString = ConnectionString;
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


        #region "Constructor & Distructor"

        public DBLayer(string ConnectionString)
        {
            _conectionstring = ConnectionString;
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
                //throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
        }

        public bool CheckConnection()
        {
            bool _IsValidConnection = true;
            oConnection = new DBConnection(_conectionstring);
            SqlConnection _connection = null;
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
                Disconnect();
            }
            catch  (DBException ) //ex)
            {
                _IsValidConnection = false;
            }
            catch  (Exception)// ex)
            {
                _IsValidConnection = false;
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
            catch (DBException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
                //throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
            }
            finally
            {
                if (oConnection != null)
                {
                    oConnection.Dispose();
                }
            }
        }

        #endregion

        #region "Insert Update Delete - Stored Procedure"

        public int Execute(string StoredProcedureName, DBParameters Parameters)
        {
            int _result = 0;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            SqlParameter osqlParameter;

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();


                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    osqlParameter = new SqlParameter();

                    osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                    osqlParameter.SqlDbType = Parameters[_counter].DataType;
                    osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                    osqlParameter.Value = Parameters[_counter].Value;
                  //  if (Parameters[_counter].Size != null)
                    {
                        if (Parameters[_counter].Size != 0)
                        {
                            osqlParameter.Size = Parameters[_counter].Size;
                        }
                    }
                    _sqlcommand.Parameters.Add(osqlParameter);
                    osqlParameter = null;
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
                //throw ex;
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
                    _sqlcommand.Dispose();
                }
            }
            return _result;
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
                    _sqlcommand.Dispose();
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
            SqlParameter osqlParameter;
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();


                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    if (Parameters[_counter].ParameterDirection == ParameterDirection.Output || Parameters[_counter].ParameterDirection == ParameterDirection.InputOutput)
                    {
                        _outputCounter = _counter;
                    }
                    osqlParameter = new SqlParameter();

                    osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                    osqlParameter.SqlDbType = Parameters[_counter].DataType;
                    osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                    osqlParameter.Value = Parameters[_counter].Value;
                  //  if (Parameters[_counter].Size != null)
                    {
                        if (Parameters[_counter].Size != 0)
                        {
                            osqlParameter.Size = Parameters[_counter].Size;
                        }
                    }
                    _sqlcommand.Parameters.Add(osqlParameter);
                    osqlParameter = null;
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
                //throw ex;
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
                    _sqlcommand.Dispose();
                }
            }
            return _result;
        }

        public object ExecuteScalar(string StoredProcedureName, DBParameters Parameters)
        {
            object _result = null;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            SqlParameter osqlParameter;

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    osqlParameter = new SqlParameter();

                    osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                    osqlParameter.SqlDbType = Parameters[_counter].DataType;
                    osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                    osqlParameter.Value = Parameters[_counter].Value;
                 //   if (Parameters[_counter].Size != null)
                    {
                        if (Parameters[_counter].Size != 0)
                        {
                            osqlParameter.Size = Parameters[_counter].Size;
                        }
                    }
                    _sqlcommand.Parameters.Add(osqlParameter);
                    osqlParameter = null;
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
                //throw ex;
            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
                //throw new DBException("Error in database execution");
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
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
                //throw ex;
            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
                //throw new DBException("Error in database execution");
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
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

                _result = _sqlcommand.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
                //throw new DBException("Error in database execution");
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
                //throw ex;
            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
                //throw new DBException("Error in database execution");
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
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
                //throw new DBException("Error in database execution");
            }
            catch (DBException ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
                //throw ex;
            }
            catch (Exception ex)
            {
                if (_withtransaction == true)
                {
                    oConnection.RollbackTransaction();
                }
                throw new DBException("Error in database execution", ex.ToString());
                //throw new DBException("Error in database execution");
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }

            return _result;
        }

        #endregion

        #region "Retrive Data - Stored Procedure"

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out SqlDataReader _result)
        {
            //SqlDataReader _result;
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            SqlParameter osqlParameter;
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    osqlParameter = new SqlParameter();

                    osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                    osqlParameter.SqlDbType = Parameters[_counter].DataType;
                    osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                    osqlParameter.Value = Parameters[_counter].Value;
                  //  if (Parameters[_counter].Size != null)
                    {
                        if (Parameters[_counter].Size != 0)
                        {
                            osqlParameter.Size = Parameters[_counter].Size;
                        }
                    }
                    
                    _sqlcommand.Parameters.Add(osqlParameter);
                    osqlParameter = null;
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
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataSet _result)
        {
            //DataSet _result = new DataSet();
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            SqlParameter osqlParameter;

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();

                for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                {
                    osqlParameter = new SqlParameter();

                    osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                    osqlParameter.SqlDbType = Parameters[_counter].DataType;
                    osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                    osqlParameter.Value = Parameters[_counter].Value;
                   // if (Parameters[_counter].Size != null)
                    {
                        if (Parameters[_counter].Size != 0)
                        {
                            osqlParameter.Size = Parameters[_counter].Size;
                        }
                    }
                    _sqlcommand.Parameters.Add(osqlParameter);
                    osqlParameter = null;
                }

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);
                
                DataSet _resultinternal = new DataSet();
                
                //_dataAdapter.Fill(_result);
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
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataTable _result)
        {
            //DataTable _result = new DataTable();
            int _counter = 0;
            SqlCommand _sqlcommand = new SqlCommand();
            SqlParameter osqlParameter;
            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();

                for (_counter = 0; _counter <= Parameters.Count-1; _counter++)
                {
                    osqlParameter = new SqlParameter();

                    osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                    osqlParameter.SqlDbType  = Parameters[_counter].DataType;
                    osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                    osqlParameter.Value = Parameters[_counter].Value;
                //    if (Parameters[_counter].Size != null)
                    {
                        if (Parameters[_counter].Size != 0)
                        {
                            osqlParameter.Size = Parameters[_counter].Size;
                        }
                    }
                    _sqlcommand.Parameters.Add(osqlParameter);
                    osqlParameter = null;
                }

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
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }


        // Without Parameters //
        public void Retrive(string StoredProcedureName, out SqlDataReader _result)
        {
            //SqlDataReader _result;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();


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
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        public void Retrive(string StoredProcedureName, out DataSet _result)
        {
            //DataSet _result = new DataSet();
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();


                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                DataSet _resultinternal = new DataSet();

                //_dataAdapter.Fill(_result);
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
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        public void Retrive(string StoredProcedureName, out DataTable _result)
        {
            //DataTable _result = new DataTable();
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.StoredProcedure;
                _sqlcommand.CommandText = StoredProcedureName;
                _sqlcommand.Connection = oConnection.GetConnection();


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
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        #endregion

        #region "Retrive Data - SQL Query"

        public void Retrive_Query(string SQLQuery, out SqlDataReader _result)
        {
            //SqlDataReader _result;
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();

                _result = _sqlcommand.ExecuteReader();

            }
            catch (SqlException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
                //throw new DBException("Error in database execution");
            }
            catch (DBException ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
                //throw ex;
            }
            catch (Exception ex)
            {
                throw new DBException("Error in database execution", ex.ToString());
                //throw new DBException("Error in database execution");
            }
            finally
            {
                if (_sqlcommand != null)
                {
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        public void Retrive_Query(string SQLQuery, out DataSet _result)
        {
            //DataSet _result = new DataSet();
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();

                SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand);

                DataSet _resultinternal = new DataSet();

                //_dataAdapter.Fill(_result);
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
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        public void Retrive_Query(string SQLQuery, out DataTable _result)
        {
            //DataTable _result = new DataTable();
            SqlCommand _sqlcommand = new SqlCommand();

            try
            {
                _sqlcommand.CommandType = CommandType.Text;
                _sqlcommand.CommandText = SQLQuery;
                _sqlcommand.Connection = oConnection.GetConnection();


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
                //try
                //{
                //    MessageBox.Show("Error in DB", "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,  0,  "E:\\Developer Working Folder\\gloPMS\\gloPMS\\bin\\Debug\\gloPMS_ERRORLog.log");
                //}
                //catch (Exception ex1)
                //{
                //    MessageBox.Show(ex1.ToString());
                //}
                throw new DBException("Error in database execution", ex.ToString());
                //MessageBox.Show("Error in DB", "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, Application.StartupPath + "\\gloPMS_ERRORLog.log");
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
                    _sqlcommand.Dispose();
                }
            }
            //return _result;
        }

        #endregion

    }
    
    public class DBParameter:IDisposable
    {
        private string _parametername;
        private ParameterDirection _parameterdirection;
        private SqlDbType _datatype;
        private object _value;
        private int _size=0;


        #region "Constructor & Distructor"

                public DBParameter()
                { 
                    
                }

                public DBParameter(string parametername,object value,ParameterDirection parameterdirection,SqlDbType datatype,int fieldsize)
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
            get{return _parametername;}
            set{_parametername = value;}
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

    public class DBParameters:IDisposable 
    {
        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public DBParameters()
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


        ~DBParameters()
        {
            Dispose(false);
        }
         #endregion


        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(DBParameter item)
        {
            _innerlist.Add(item);
        }

        public int Add(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype,int fieldsize)
        {
            DBParameter item = new DBParameter(parametername,value, parameterdirection, datatype,fieldsize );
            return _innerlist.Add(item);
        }

        public int Add(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype)
        {
            DBParameter item = new DBParameter(parametername, value, parameterdirection, datatype);
            return _innerlist.Add(item);
        }

        public bool Remove(DBParameter item)
        {
            bool result = false;
            DBParameter obj; 

            for (int i = 0; i < _innerlist.Count; i++)
            {
                //store current index being checked
                obj = new DBParameter();
                obj = (DBParameter)_innerlist[i];
                if (obj.ParameterName == item.ParameterName && obj.DataType == item.DataType)
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

        public DBParameter this[int index]
        {
            get 
            {
                return (DBParameter)_innerlist[index];
            }
        }

        public bool Contains(DBParameter item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(DBParameter item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(DBParameter[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }
}
