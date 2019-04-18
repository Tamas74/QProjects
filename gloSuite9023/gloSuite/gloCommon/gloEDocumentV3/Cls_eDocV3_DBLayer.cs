using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Collections;

namespace gloEDocumentV3
{
    namespace Database
    {
        internal class DBConnection : IDisposable
        {

            private string _conectionstring = "";
            protected SqlConnection _connection = null;
            protected SqlTransaction _transaction = null;
            private bool isTransactionOn = false;
            public string ErrorMessage
            {
                get { return _ErrorMessage; }
                set { _ErrorMessage = value; }
            }

            public bool HasError
            {
                get { return _HasError; }
                set { _HasError = value; }
            }

            string _ErrorMessage = "";
            bool _HasError = false;

            #region "Constructor & Distructor"

            public DBConnection(string ConnectionString)
            {
                if (_connection != null)
                {
                    CloseConnection();
                    _connection.Dispose();
                    _connection = null;
                }
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
                        if (_connection != null)
                        {
                            CloseConnection();
                            _connection.Dispose();
                            _connection = null;
                        }
                        if (_transaction != null)
                        {
                            CommitTransaction();
                            _transaction.Dispose();
                            _transaction = null;
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
                string _ErrorMessage = "";
              //  bool _HasError = false;
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
                //    _HasError = true;
                    _ErrorMessage = "Error in opening database connection" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBConnection:OpenConnection()" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add

                }
                catch (Exception ex)
                {
                  //  _HasError = true;
                    _ErrorMessage = "Error in opening database connection" + Environment.NewLine + ex.Message;


                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBConnection:OpenConnection()" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
            }

            public SqlConnection GetConnection()
            {
                return _connection;
            }

            public bool CloseConnection()
            {
                string _ErrorMessage = "";
                //bool _HasError = false;
                bool _result = false;
                try
                {
                    if (_transaction != null)
                    {
                        CommitTransaction();
                    }
                    if (_connection != null)
                    {
                        if (_connection.State == ConnectionState.Open)
                        {
                            _connection.Close();
                            _result = true;
                        }
                        else
                        {
                            _result = false;
                        }
                    }
                    else
                    {
                        _result = false;
                    }
                }
                catch (SqlException ex)
                {
                  //  _HasError = true;
                    _ErrorMessage = "Error in closing database connection" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBConnection:CloseConnection()" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    //_HasError = true;
                    _ErrorMessage = "Error in closing database connection" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBConnection:CloseConnection()" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                return _result;
            }

            public bool BeginTransaction()
            {
                string _ErrorMessage = "";
                //bool _HasError = false;
                bool _result = false;
                try
                {
                    if (_connection != null)
                    {
                        if (_connection.State == ConnectionState.Open)
                        {
                            
                            if (_transaction != null)
                            {
                                 CommitTransaction();
                                _transaction.Dispose();
                                _transaction = null;
                            }
                            _transaction = _connection.BeginTransaction();
                            isTransactionOn = true;
                            _result = true;
                        }
                        else
                        {
                            _result = false;
                        }
                    }
                    else
                    {
                        _result = false;
                    }
                }
                catch (SqlException ex)
                {
                  //  _HasError = true;
                    _ErrorMessage = "Error in begin transaction" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBConnection:BeginTransaction()" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    //_HasError = true;
                    _ErrorMessage = "Error in begin transaction" + Environment.NewLine + ex.Message;


                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBConnection:BeginTransaction()" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                return _result;
            }

            public SqlTransaction GetTransaction()
            {
                return _transaction;
            }

            public bool CommitTransaction()
            {
                string _ErrorMessage = "";
                //bool _HasError = false;
                bool _result = false;
                try
                {
                    if (_transaction != null)
                    {
                        if (isTransactionOn)
                        {
                            _transaction.Commit();
                        }
                        isTransactionOn = false;
                        _result = true;
                    }
                    else
                    {
                        _result = false;
                    }
                }
                catch (SqlException ex)
                {
                  //  _HasError = true;
                    _ErrorMessage = "Error in commit transaction" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBConnection:CommitTransaction()" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    // _HasError = true;
                    _ErrorMessage = "Error in commit transaction" + Environment.NewLine + ex.Message;
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBConnection:CommitTransaction()" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                return _result;
            }

            public bool RollbackTransaction()
            {
                string _ErrorMessage = "";
                //bool _HasError = false;
                bool _result = false;

                try
                {
                    if (_transaction != null)
                    {
                        if (isTransactionOn)
                        {
                            _transaction.Rollback();
                        }
                        isTransactionOn = false;
                        _result = true;
                    }
                    else
                    {
                        _result = false;
                    }
                }
                catch (SqlException ex)
                {
                  //  _HasError = true;
                    _ErrorMessage = "Error in roll back transaction" + Environment.NewLine + ex.Message;
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBConnection:RollbackTransaction()" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    //_HasError = true;
                    _ErrorMessage = "Error in roll back transaction" + Environment.NewLine + ex.Message;
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBConnection:RollbackTransaction()" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                return _result;
            }

        }

        internal class DBLayer : IDisposable
        {
            private string _conectionstring = "";
            protected bool _withtransaction = false;
            DBConnection oConnection=null;

            public string ErrorMessage
            {
                get { return _ErrorMessage; }
                set { _ErrorMessage = value; }
            }

            public bool HasError
            {
                get { return _HasError; }
                set { _HasError = value; }
            }

            string _ErrorMessage = "";
            bool _HasError = false;

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

            

            #region "Dhruv 2010 -> Disconnect,Connect"
            public bool Connect(bool WithTransaction)
            {
                if (oConnection != null)
                {
                    oConnection.Dispose();
                    oConnection = null;
                }

                oConnection = new DBConnection(_conectionstring);

                _HasError = false;
                _ErrorMessage = "";
                bool _result = false;
                try
                {
                    if (oConnection != null)
                    {
                        if (WithTransaction == true)
                        {
                            oConnection.OpenConnection();
                            oConnection.BeginTransaction();
                            _withtransaction = WithTransaction;
                            _result = true;
                        }
                        else
                        {
                            oConnection.OpenConnection();
                            _result = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Connect(bool WithTransaction)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add

                }
                return _result;
            }
            public bool Disconnect()
            {
                _HasError = false;
                _ErrorMessage = "";
                bool _result = false;
                try
                {
                    if (oConnection != null)
                    {
                        if (_withtransaction == true)
                        {
                            if (oConnection.GetConnection().State == ConnectionState.Open)
                            {
                                oConnection.CommitTransaction();
                            }
                            oConnection.CloseConnection();
                            _result = true;
                        }
                        else
                        {
                            oConnection.CloseConnection();
                            _result = true;
                        }
                        if (oConnection != null)
                        {
                            oConnection.Dispose();
                            oConnection = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Disconnect()" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                return _result;
            }
            #endregion "Dhruv 2010 -> Disconnect,Connect"


            #endregion


            #region "Insert Update Delete - Stored Procedure"


            public void Retrive(string StoredProcedureName, DBParameters Parameters, out SqlDataReader result)
            {
                SqlDataReader _result = null;
                int _counter = 0;
                SqlCommand _sqlcommand = new SqlCommand();
                SqlParameter osqlParameter;
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    _sqlcommand.CommandType = CommandType.StoredProcedure;
                    _sqlcommand.CommandText = StoredProcedureName;
                    _sqlcommand.Connection = oConnection.GetConnection();
                    _sqlcommand.CommandTimeout = 0;

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
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, DBParameters Parameters, out SqlDataReader result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, DBParameters Parameters, out SqlDataReader result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
                result = _result;
            }


            #region "Dhruv 2010 -> Execute"
           
            public int Execute(string StoredProcedureName, DBParameters Parameters)
            {
                int _result = 0;
                int _counter = 0;
                SqlCommand _sqlcommand = new SqlCommand();
                SqlParameter osqlParameter;
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.StoredProcedure;
                        _sqlcommand.CommandText = StoredProcedureName;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;

                        if (_withtransaction == true)
                        {
                            _sqlcommand.Transaction = oConnection.GetTransaction();
                        }

                        for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                        {
                            osqlParameter = new SqlParameter();
                            if (osqlParameter != null)
                            {
                                osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                                osqlParameter.SqlDbType = Parameters[_counter].DataType;
                                osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                                osqlParameter.Value = Parameters[_counter].Value;
                             //   if (Parameters[_counter].Size != null)
                                {
                                    if (Parameters[_counter].Size > 0)
                                    {
                                        osqlParameter.Size = Parameters[_counter].Size;
                                    }
                                    else
                                    {
                                        osqlParameter.Size = 0;
                                    }
                                }
                                //else
                                //{
                                //    osqlParameter.Size = 0;
                                //}
                                _sqlcommand.Parameters.Add(osqlParameter);
                                osqlParameter = null;
                            }
                        }

                        _result = _sqlcommand.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Execute(string StoredProcedureName, DBParameters Parameters)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Execute(string StoredProcedureName, DBParameters Parameters)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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

            #endregion "Dhruv 2010 -> Execute"


            #region "Dhruv 2010 -> Execute"
            
            public int Execute(string StoredProcedureName, DBParameters Parameters, int ConnectionTimeOut)
            {
                int _result = 0;
                int _counter = 0;
                SqlCommand _sqlcommand = new SqlCommand();
                SqlParameter osqlParameter;
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.StoredProcedure;
                        _sqlcommand.CommandTimeout = ConnectionTimeOut;
                        _sqlcommand.CommandText = StoredProcedureName;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        if (_withtransaction == true)
                        {
                            _sqlcommand.Transaction = oConnection.GetTransaction();
                        }

                        for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                        {
                            osqlParameter = new SqlParameter();
                            if (osqlParameter != null)
                            {
                                osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                                osqlParameter.SqlDbType = Parameters[_counter].DataType;
                                osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                                osqlParameter.Value = Parameters[_counter].Value;
                              //  if (Parameters[_counter].Size != null)
                                {
                                    if (Parameters[_counter].Size > 0)
                                    {
                                        osqlParameter.Size = Parameters[_counter].Size;
                                    }
                                    else
                                    {
                                        osqlParameter.Size = 0;
                                    }
                                }
                                //else
                                //{
                                //    osqlParameter.Size = 0;
                                //}
                                _sqlcommand.Parameters.Add(osqlParameter);
                                if (osqlParameter != null)
                                {
                                    osqlParameter = null;
                                }
                            }
                        }

                        _result = _sqlcommand.ExecuteNonQuery();
                    }

                }
                catch (SqlException ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Execute(string StoredProcedureName, DBParameters Parameters)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Execute(string StoredProcedureName, DBParameters Parameters)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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

           #endregion "Dhruv 2010 -> Execute"


           #region "Dhruv 2010 -> Execute"
          
            public int Execute(string StoredProcedureName)
            {
                int _result = 0;
                SqlCommand _sqlcommand = new SqlCommand();
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.StoredProcedure;
                        _sqlcommand.CommandText = StoredProcedureName;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;

                        if (_withtransaction == true)
                        {
                            _sqlcommand.Transaction = oConnection.GetTransaction();
                        }
                        _result = _sqlcommand.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Execute(string StoredProcedureName)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Execute(string StoredProcedureName)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
           #endregion "Dhruv 2010 -> Execute"


           #region "Dhruv 2010 -> Execute"

            public int Execute(string StoredProcedureName, DBParameters Parameters, out object ParameterValue)
            {
                int _result = 0;
                int _counter = 0;
                SqlCommand _sqlcommand = new SqlCommand();
                int _outputCounter = 0;
                SqlParameter osqlParameter;
                _HasError = false;
                _ErrorMessage = "";
                object ParVal = null;

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.StoredProcedure;
                        _sqlcommand.CommandText = StoredProcedureName;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;
                        if (_withtransaction == true)
                        {
                            _sqlcommand.Transaction = oConnection.GetTransaction();
                        }

                        for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                        {
                            if (Parameters[_counter].ParameterDirection == ParameterDirection.Output || Parameters[_counter].ParameterDirection == ParameterDirection.InputOutput)
                            {
                                _outputCounter = _counter;
                            }
                            osqlParameter = new SqlParameter();
                            if (osqlParameter != null)
                            {
                                osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                                osqlParameter.SqlDbType = Parameters[_counter].DataType;
                                osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                                osqlParameter.Value = Parameters[_counter].Value;
                               // if (Parameters[_counter].Size != null)
                                {
                                    if (Parameters[_counter].Size > 0)
                                    {
                                        osqlParameter.Size = Parameters[_counter].Size;
                                    }
                                    else
                                    {
                                        osqlParameter.Size = 0;
                                    }
                                }
                                //else
                                //{
                                //    osqlParameter.Size = 0;
                                //}
                                _sqlcommand.Parameters.Add(osqlParameter);
                                if (osqlParameter != null)
                                {
                                    osqlParameter = null;
                                }
                            }
                        }


                        _result = _sqlcommand.ExecuteNonQuery();

                        if (_sqlcommand.Parameters[_outputCounter].Value != null)
                        {
                            ParVal = _sqlcommand.Parameters[_outputCounter].Value;
                        }
                        else
                        {
                            ParVal = 0;
                        }
                    }

                }
                catch (SqlException ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Execute(string StoredProcedureName)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {

                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Execute(string StoredProcedureName)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add

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
                ParameterValue = ParVal;
                return _result;
            }

           #endregion "Dhruv 2010 -> Execute"


           #region "Dhruv 2010 -> ExecuteScalar"
           
            public object ExecuteScalar(string StoredProcedureName, DBParameters Parameters)
            {
                object _result = null;
                int _counter = 0;
                SqlCommand _sqlcommand = new SqlCommand();
                SqlParameter osqlParameter;
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.StoredProcedure;
                        _sqlcommand.CommandText = StoredProcedureName;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;

                        if (_withtransaction == true)
                        {
                            _sqlcommand.Transaction = oConnection.GetTransaction();
                        }
                        for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                        {
                            osqlParameter = new SqlParameter();
                            if (osqlParameter != null)
                            {
                                osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                                osqlParameter.SqlDbType = Parameters[_counter].DataType;
                                osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                                osqlParameter.Value = Parameters[_counter].Value;
                               // if (Parameters[_counter].Size != null)
                                {
                                    if (Parameters[_counter].Size > 0)
                                    {
                                        osqlParameter.Size = Parameters[_counter].Size;
                                    }
                                    else
                                    {
                                        osqlParameter.Size = 0;
                                    }

                                }
                                //else
                                //{
                                //    osqlParameter.Size = 0;
                                //}
                                _sqlcommand.Parameters.Add(osqlParameter);
                                if (osqlParameter != null)
                                {
                                    osqlParameter = null;
                                }
                            }
                        }

                        _result = _sqlcommand.ExecuteScalar();

                        if (_result == null)
                        {
                            _result = "";
                        }
                    }
                }
                catch (SqlException ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:ExecuteScalar(string StoredProcedureName, DBParameters Parameters)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:ExecuteScalar(string StoredProcedureName, DBParameters Parameters)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add

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

           #endregion "Dhruv 2010 -> ExecuteScalar"


           #region "Dhruv 2010 -> ExecuteScalar"
          
            public object ExecuteScalar(string StoredProcedureName)
            {
                object _result = null;
                SqlCommand _sqlcommand = new SqlCommand();
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.StoredProcedure;
                        _sqlcommand.CommandText = StoredProcedureName;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;

                        if (_withtransaction == true)
                        {
                            _sqlcommand.Transaction = oConnection.GetTransaction();
                        }
                        _result = _sqlcommand.ExecuteScalar();

                        if (_result == null)
                        {
                            _result = "";
                        }
                    }
                }
                catch (SqlException ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:ExecuteScalar(string StoredProcedureName)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:ExecuteScalar(string StoredProcedureName)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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

           #endregion "Dhruv 2010 -> ExecuteScalar"


            #endregion

           #region "Insert Update Delete - SQL Query"

           #region "Dhruv 2010 -> Execute_Query"
          
            public int Execute_Query(string SQLQuery)
            {
                int _result = 0;
                SqlCommand _sqlcommand = new SqlCommand();
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        //_sqlcommand = new SqlCommand();
                        _sqlcommand.CommandType = CommandType.Text;
                        _sqlcommand.CommandText = SQLQuery;
                        _sqlcommand.CommandTimeout = 0;

                        _sqlcommand.Connection = oConnection.GetConnection();

                        if (_withtransaction == true)
                        {
                            _sqlcommand.Transaction = oConnection.GetTransaction();
                        }
                        _result = _sqlcommand.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Execute_Query(string SQLQuery)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Execute_Query(string SQLQuery)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
            #endregion "Dhruv 2010 -> Execute_Query"

            #region "Dhruv 2010 -> ExecuteScalar_Query"
            
            public object ExecuteScalar_Query(string SQLQuery)
            {
                object _result = null;
                SqlCommand _sqlcommand = new SqlCommand();
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.Text;
                        _sqlcommand.CommandText = SQLQuery;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;

                        if (_withtransaction == true)
                        {
                            _sqlcommand.Transaction = oConnection.GetTransaction();
                        }
                        _result = _sqlcommand.ExecuteScalar();

                        if (_result == null)
                        {
                            _result = "";
                        }
                    }
                }
                catch (SqlException ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:ExecuteScalar_Query(string SQLQuery)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:ExecuteScalar_Query(string SQLQuery)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
            #endregion "Dhruv 2010 -> ExecuteScalar_Query"



            #endregion "Insert Update Delete - SQL Query"


            #region "Insert File Chunk by chunk"

            public bool ConvertnInsertFile(Int64 DocumentID, Int64 ContainerID, string FilePath)
            {
                //SqlCommand _sqlcommand = null;
                _HasError = false;
                _ErrorMessage = "";
                bool _ReturnValue = false;
                System.IO.FileStream oFileStream = null;
               // System.IO.FileInfo oFile = default(System.IO.FileInfo);

                try
                {
                    if (System.IO.File.Exists(FilePath))
                    {

                        Cls_SQLFileStream.SaveFile(ContainerID, DocumentID, FilePath, oConnection.GetTransaction(), gloEDocV3Admin.gClinicID, Enumeration.enum_OpenExternalSource.None);
                    }
                }
                catch (Exception ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;
                    _ReturnValue = false;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:ExecuteScalar_Query(string SQLQuery)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                finally
                {
                  //  if (_sqlcommand != null) { _sqlcommand.Dispose(); }
                    if (oFileStream != null) { oFileStream.Dispose(); }

                }

                return _ReturnValue;
            }
            


            #endregion "Insert File Chunk by chunk"


            #region "Retrive Data - Stored Procedure"
            #region "Dhruv 2010 -> SpiltDocumentsOnSize"
            public void Retrive_Old(string StoredProcedureName, DBParameters Parameters, out SqlDataReader result)
            {
                SqlDataReader _result = null;
                int _counter = 0;
                SqlCommand _sqlcommand = new SqlCommand();
                SqlParameter osqlParameter;
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    _sqlcommand.CommandType = CommandType.StoredProcedure;
                    _sqlcommand.CommandText = StoredProcedureName;
                    _sqlcommand.Connection = oConnection.GetConnection();
                    _sqlcommand.CommandTimeout = 0;

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

                    _result = _sqlcommand.ExecuteReader();

                }
                catch (SqlException ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, DBParameters Parameters, out SqlDataReader result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, DBParameters Parameters, out SqlDataReader result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
                result = _result;
            }
            #endregion "Dhruv 2010 -> SpiltDocumentsOnSize"

            #region "Dhruv 2010 -> SpiltDocumentsOnSize"
           
            public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataSet result)
            {
                DataSet _result = new DataSet();
                int _counter = 0;
                SqlCommand _sqlcommand = new SqlCommand();
                SqlParameter osqlParameter;
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.StoredProcedure;
                        _sqlcommand.CommandText = StoredProcedureName;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;

                        for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                        {
                            osqlParameter = new SqlParameter();
                            if (osqlParameter != null)
                            {
                                osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                                osqlParameter.SqlDbType = Parameters[_counter].DataType;
                                osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                                osqlParameter.Value = Parameters[_counter].Value;
                             //   if (Parameters[_counter].Size != null)
                                {
                                    if (Parameters[_counter].Size > 0)
                                    {
                                        osqlParameter.Size = Parameters[_counter].Size;
                                    }
                                    else
                                    {
                                        osqlParameter.Size = 0;
                                    }
                                }
                                //else
                                //{
                                //    osqlParameter.Size = 0;
                                //}

                                _sqlcommand.Parameters.Add(osqlParameter);
                                if (osqlParameter != null)
                                {
                                    osqlParameter = null;
                                }
                            }
                        }

                        using (SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand))
                        {
                     //       using (DataSet _resultinternal = new DataSet())
                            {

                                _dataAdapter.Fill(_result);
                                //_result = _resultinternal;
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, DBParameters Parameters, out DataSet result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, DBParameters Parameters, out DataSet result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
                result = _result;
            }

            #endregion "Dhruv 2010 -> SpiltDocumentsOnSize"

            #region "Dhruv 2010 -> SpiltDocumentsOnSize"
           
            public void Retrive(string StoredProcedureName, DBParameters Parameters, out DataTable result)
            {
                int _counter = 0;
                SqlCommand _sqlcommand = new SqlCommand();
                SqlParameter osqlParameter;
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    result = null;

                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.StoredProcedure;
                        _sqlcommand.CommandText = StoredProcedureName;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;

                        for (_counter = 0; _counter <= Parameters.Count - 1; _counter++)
                        {
                            osqlParameter = new SqlParameter();
                            if (osqlParameter != null)
                            {
                                osqlParameter.ParameterName = Parameters[_counter].ParameterName;
                                osqlParameter.SqlDbType = Parameters[_counter].DataType;
                                osqlParameter.Direction = Parameters[_counter].ParameterDirection;
                                osqlParameter.Value = Parameters[_counter].Value;
                             //   if (Parameters[_counter].Size != null)
                                {
                                    if (Parameters[_counter].Size > 0)
                                    {
                                        osqlParameter.Size = Parameters[_counter].Size;
                                    }
                                    else
                                    {
                                        osqlParameter.Size = 0;
                                    }
                                }
                                //else
                                //{
                                //    osqlParameter.Size = 0;
                                //}
                                _sqlcommand.Parameters.Add(osqlParameter);
                                if (osqlParameter != null)
                                {
                                    osqlParameter = null;
                                }
                            }
                        }

                        using (SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand))
                        {
                            result = new DataTable();
                            _dataAdapter.Fill(result);
                        }
                    }

                }
                catch (SqlException ex)
                {
                    result = null;
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, DBParameters Parameters, out DataTable result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    result = null;
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, DBParameters Parameters, out DataTable result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
            }

            #endregion "Dhruv 2010 -> SpiltDocumentsOnSize"



            #region "Dhruv 2010 -> SpiltDocumentsOnSize"
            
            public void Retrive(string StoredProcedureName, out SqlDataReader result)
            {
                SqlDataReader _result = null;
                SqlCommand _sqlcommand = new SqlCommand();
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.StoredProcedure;
                        _sqlcommand.CommandText = StoredProcedureName;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;

                        _result = _sqlcommand.ExecuteReader();
                    }
                }
                catch (SqlException ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, out SqlDataReader result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, out SqlDataReader result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
                result = _result;
            }

            #endregion "Dhruv 2010 -> SpiltDocumentsOnSize"


            #region "Dhruv 2010 -> SpiltDocumentsOnSize"
            
            public void Retrive(string StoredProcedureName, out SqlDataReader result, int ConnectionTimeOut, DBParameters Parameters)
            {
                SqlDataReader _result = null;
                SqlCommand _sqlcommand = new SqlCommand();
                SqlParameter osqlParameter;
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.StoredProcedure;
                        _sqlcommand.CommandTimeout = ConnectionTimeOut;
                        _sqlcommand.CommandText = StoredProcedureName;
                        _sqlcommand.Connection = oConnection.GetConnection();


                        if (_withtransaction == true)
                        {
                            _sqlcommand.Transaction = oConnection.GetTransaction();
                        }
                        for (int _counter = 0; _counter <= Parameters.Count - 1; _counter++)
                        {
                            osqlParameter = new SqlParameter();
                            if (osqlParameter != null)
                            {
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
                                if (osqlParameter != null)
                                {
                                    osqlParameter = null;
                                }
                            }
                        }

                        _result = _sqlcommand.ExecuteReader(CommandBehavior.SequentialAccess);
                    }
                }
                catch (SqlException ex)
                {

                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, out SqlDataReader result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, out SqlDataReader result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
                result = _result;
            }

            #endregion "Dhruv 2010 -> SpiltDocumentsOnSize"

            #region "Dhruv 2010 -> SpiltDocumentsOnSize"
            
            public void Retrive(string StoredProcedureName, out DataSet result)
            {
                DataSet _result = new DataSet();
                SqlCommand _sqlcommand = new SqlCommand();
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.StoredProcedure;
                        _sqlcommand.CommandText = StoredProcedureName;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;

                        using (SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand))
                        {
                            //using (DataSet _resultinternal = new DataSet())
                            {

                                _dataAdapter.Fill(_result);
                                //_result = _resultinternal;
                            }
                        }

                    }
                }
                catch (SqlException ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, out DataSet result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;


                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, out DataSet result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
                result = _result;
            }

            #endregion "Dhruv 2010 -> SpiltDocumentsOnSize"

            #region "Dhruv 2010 -> SpiltDocumentsOnSize"
           
            public void Retrive(string StoredProcedureName, out DataTable result)
            {
                DataTable _result = null;
                SqlCommand _sqlcommand = new SqlCommand();
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.StoredProcedure;
                        _sqlcommand.CommandText = StoredProcedureName;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;

                        using (SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand))
                        {
                            using (DataSet _dataset = new DataSet())
                            {
                                DataTable _resultinternal = null;
                               // if (_resultinternal != null)
                                {
                                    _dataAdapter.Fill(_dataset);
                                    if (_dataset.Tables[0] != null)
                                    {
                                        _resultinternal = _dataset.Tables[0].Copy();
                                    }
                                    _result = _resultinternal;
                                    //if (_resultinternal != null)
                                    //{
                                    //    _resultinternal.Dispose();
                                    //    _resultinternal = null;
                                    //}
                                }

                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, out DataSet result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive(string StoredProcedureName, out DataSet result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
                result = _result;
            }
            #endregion "Dhruv 2010 -> SpiltDocumentsOnSize"

            #endregion



            #region "Retrive Data - SQL Query"

            #region "Dhruv 2010 -> Retrive_Query"
          
            public void Retrive_Query(string SQLQuery, out SqlDataReader result)
            {
                SqlDataReader _result = null;
                SqlCommand _sqlcommand = new SqlCommand();
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.Text;
                        _sqlcommand.CommandText = SQLQuery;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;
                        if (_withtransaction == true)
                        {
                            _sqlcommand.Transaction = oConnection.GetTransaction();
                        }
                        _result = _sqlcommand.ExecuteReader();

                    }
                }
                catch (SqlException ex)
                {
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive_Query(string SQLQuery, out SqlDataReader result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive_Query(string SQLQuery, out SqlDataReader result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
                result = _result;
            }


            #endregion "Dhruv 2010 -> Retrive_Query"

            #region "Dhruv 2010 -> Retrive_Query"
           
            public void Retrive_Query(string SQLQuery, out DataSet result)
            {
                DataSet _result = new DataSet();
                SqlCommand _sqlcommand = new SqlCommand();
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {

                        _sqlcommand.CommandType = CommandType.Text;
                        _sqlcommand.CommandText = SQLQuery;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;

                        using (SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand))
                        {
                            //DataSet _resultinternal = new DataSet();
                            //if (_resultinternal != null)
                            {

                                _dataAdapter.Fill(_result);

                                //_result = _resultinternal;
                                //if (_resultinternal != null)
                                //{
                                //    _resultinternal = null;
                                //}
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive_Query(string SQLQuery, out DataSet result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive_Query(string SQLQuery, out DataSet result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
                result = _result;
            }

            #endregion "Dhruv 2010 -> Retrive_Query"

            #region "Dhruv 2010 -> Retrive_Query"
           
            public void Retrive_Query(string SQLQuery, out DataTable result)
            {
                result = null;
                SqlCommand _sqlcommand = new SqlCommand();
                _HasError = false;
                _ErrorMessage = "";

                try
                {
                    if (_sqlcommand != null)
                    {
                        _sqlcommand.CommandType = CommandType.Text;
                        _sqlcommand.CommandText = SQLQuery;
                        _sqlcommand.Connection = oConnection.GetConnection();
                        _sqlcommand.CommandTimeout = 0;

                        if (_withtransaction == true)
                        {
                            _sqlcommand.Transaction = oConnection.GetTransaction();
                        }

                        using (SqlDataAdapter _dataAdapter = new SqlDataAdapter(_sqlcommand))
                        {
                            result = new DataTable();
                            _dataAdapter.Fill(result);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    result = null;
                    if (_withtransaction == true)
                    {
                        oConnection.RollbackTransaction();
                    }
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;

                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive_Query(string SQLQuery, out DataTable result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                }
                catch (Exception ex)
                {
                    result = null;
                    _HasError = false;
                    _ErrorMessage = "Error in database execution" + Environment.NewLine + ex.Message;


                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : DBLayer:Retrive_Query(string SQLQuery, out DataTable result)" + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
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
            }

            #endregion "Dhruv 2010 -> Retrive_Query"

            #endregion

        }

        internal class DBParameter : IDisposable
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
        #region "Dhruv 2010 -> DBParameters"
       
        internal class DBParameters : IDisposable
        {
            protected ArrayList _innerlist;

            #region "Constructor & Destructor"

            public DBParameters()
            {
                _innerlist = new ArrayList();
                if (_innerlist == null)
                {
                    string _ErrorMessage = "Insuffucient Memory . " + "Pages";
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);

                }
            }

            private bool disposed = false;

            public void Dispose()
            {
                if (_innerlist != null)
                {
                    _innerlist = null;
                }
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
                if (_innerlist != null)
                {
                    _innerlist = null;
                }
                Dispose(false);
            }
            #endregion


            public int Count
            {
                get
                {
                    if (_innerlist != null)
                    {
                        return _innerlist.Count;
                    }
                    else
                    {
                        return -1;
                    }

                }
            }

            public void Add(DBParameter item)
            {
                if (_innerlist != null)
                {
                    _innerlist.Add(item);
                }
            }

            public int Add(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype, int fieldsize)
            {
                if (_innerlist != null)
                {
                    using (DBParameter item = new DBParameter(parametername, value, parameterdirection, datatype, fieldsize))
                    {
                        if (item != null)
                        {
                            return _innerlist.Add(item);
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    return -1;
                }
            }

            public int Add(string parametername, object value, ParameterDirection parameterdirection, SqlDbType datatype)
            {
                if (_innerlist != null)
                {
                    using (DBParameter item = new DBParameter(parametername, value, parameterdirection, datatype))
                    {
                        if (item != null)
                        {
                            return _innerlist.Add(item);
                        }
                        else
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    return -1;
                }
            }

            public bool Remove(DBParameter item)
            {
                bool result = false;
                DBParameter obj;
                if (_innerlist != null)
                {
                    for (int i = _innerlist.Count - 1; i >= 0; i--)
                    {
                        //store current index being checked
                        using (obj = new DBParameter())
                        {
                            obj = (DBParameter)_innerlist[i];
                            if (obj.ParameterName == item.ParameterName && obj.DataType == item.DataType)
                            {
                                _innerlist.RemoveAt(i);
                                result = true;
                                break;
                            }

                        }

                    }
                }

                return result;
            }

            public bool RemoveAt(int index)
            {
                bool result = false;
                if (_innerlist != null)
                {
                    if (index < 0 || index >= _innerlist.Count)
                    {
                        return false;
                    }
                    else
                    {
                        _innerlist.RemoveAt(index);
                        result = true;
                    }
                }
                return result;
            }

            public void Clear()
            {
                if (_innerlist != null)
                {
                    _innerlist.Clear();
                }
            }

            public DBParameter this[int index]
            {
                get
                {
                    if (_innerlist != null)
                    {
                        if (index < 0 || index >= _innerlist.Count)
                        {
                            return null;
                        }
                        else
                        {
                            return (DBParameter)_innerlist[index];
                        }

                    }
                    else
                    {
                        return null;
                    }

                }
            }

            public bool Contains(DBParameter item)
            {
                if (_innerlist != null)
                {
                    return _innerlist.Contains(item);
                }
                else
                {
                    return false;
                }
            }

            public int IndexOf(DBParameter item)
            {
                if (_innerlist != null)
                {
                    return _innerlist.IndexOf(item);
                }
                else
                {
                    return -1;
                }
            }

            public void CopyTo(DBParameter[] array, int index)
            {
                if (_innerlist != null)
                {
                    _innerlist.CopyTo(array, index);
                }
            }

        }

        #endregion "Dhruv 2010 -> DBParameters"

        internal class Supporting
        {
            string _ErrorMessage = "";
            bool _HasError = false;

            #region "Constructor & Distructor"

            public Supporting()
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

            ~Supporting()
            {
                Dispose(false);
            }

            #endregion

            public string ErrorMessage
            {
                get { return _ErrorMessage; }
                set { _ErrorMessage = value; }
            }

            public bool HasError
            {
                get { return _HasError; }
                set { _HasError = value; }
            }

        }
    }
}
