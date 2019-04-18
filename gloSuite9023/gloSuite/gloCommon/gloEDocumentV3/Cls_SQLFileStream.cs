using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Windows.Forms;

namespace gloEDocumentV3
{
    class Cls_SQLFileStream
    {

        public static Boolean ShowMessageBox = true;



        public static void SaveFile(Int64 ContainerID, Int64 DocumentId, string filename, SqlTransaction txn, long ClinicID, Enumeration.enum_OpenExternalSource _OpenExternalSource)
        {
            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "Save File start in CLs_SQLFilestream.cs at Line 21");
            const int BlockSize = 1024 * 512;
            FileStream source = null;
            SafeFileHandle handle = null;
            FileStream dest = null;
            string _ErrorMessage = "";
      //      bool _HasError = false;
      //      bool _result = true;
            try
            {


                handle = GetOutputFileHandle(ContainerID, DocumentId, txn, ClinicID, _OpenExternalSource);
                dest = new FileStream(handle, FileAccess.Write);

                source = new FileStream(filename, FileMode.Open, FileAccess.Read);
                long sourceLength = source.Length;
                byte[] buffer = new byte[BlockSize];
                long destWritten = 0;
                int bytesRead;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    dest.Write(buffer, 0, bytesRead);
                    destWritten += ((long)bytesRead);
                }
                dest.Flush(); //We cannot able to write 3K file problem of the app's teams
                if (destWritten < sourceLength)
                {
                    _ErrorMessage = "Only written " + destWritten.ToString() + " Unable to write " + sourceLength.ToString();
                    if (ShowMessageBox == true)
                    {
                        System.Windows.Forms.MessageBox.Show("Only written " + destWritten.ToString() + " Unable to write " + sourceLength.ToString(), gloEDocV3Admin.gMessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                   
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                    throw new Exception(_ErrorMessage);

                }
            }
            catch (Exception ex)
            {
                if (ShowMessageBox == true)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
           //     _HasError = true;
                _ErrorMessage = ex.Message;
              //  _result = false;
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }
                throw ex;
            }
            finally
            {
             
                if (source != null)
                {
                    source.Close();
                    source.Dispose();
                }
                //We cannot able to write 3K file problem of the app's teams
                if (dest != null)
                {
                    dest.Close();
                    dest.Dispose();
                }
                if (handle != null)
                {
                    handle.Close();
                    handle.Dispose();
                }
            }
           
        }

        public static SafeFileHandle GetOutputFileHandle(Int64 ContainerID, Int64 DocumentId, SqlTransaction txn, long ClinicID, Enumeration.enum_OpenExternalSource _OpenExternalSource)
        {
            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "GetOutputFileHandle start in CLs_SQLFilestream.cs at Line 87");
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            SafeFileHandle handle = null;
            string _ErrorMessage = "";
            string GetOutputFileInfoCmd = null;
            string queryString = string.Empty;
            string sqlVersion = string.Empty;

            try
            {
                queryString = "SELECT LEFT(CAST(SERVERPROPERTY('productversion') AS VARCHAR),2) SQLServerVersion";
                cmd = new SqlCommand(queryString, txn.Connection, txn);
                using (rdr = cmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    rdr.Read();
                    sqlVersion = Convert.ToString(rdr.GetSqlString(0).Value);
                    rdr.Close();
                }

                if (sqlVersion == "10")
                {
                    if (_OpenExternalSource == Enumeration.enum_OpenExternalSource.RCM)
                    {
                        GetOutputFileInfoCmd = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT(), iDocumentStream.PathName()"
                                       + " FROM dbo.eDocument_Container_V3_RCM WITH(NOLOCK) "
                                       + " WHERE eContainerID = " + ContainerID + " AND eDocumentID = " + DocumentId + " AND ClinicID = " + ClinicID;
                    }
                    else
                    {
                        GetOutputFileInfoCmd = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT(), iDocumentStream.PathName()"
                    + " FROM dbo.eDocument_Container_V3 WITH(NOLOCK) "
                    + " WHERE eContainerID = " + ContainerID + " AND eDocumentID = " + DocumentId + " AND ClinicID = " + ClinicID;
                    }
                }
                else
                {
                    if (_OpenExternalSource == Enumeration.enum_OpenExternalSource.RCM)
                    {
                        GetOutputFileInfoCmd = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT(), iDocumentStream.PathName(0,1)"
                                       + " FROM dbo.eDocument_Container_V3_RCM WITH(NOLOCK) "
                                       + " WHERE eContainerID = " + ContainerID + " AND eDocumentID = " + DocumentId + " AND ClinicID = " + ClinicID;
                    }
                    else
                    {
                        GetOutputFileInfoCmd = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT(), iDocumentStream.PathName(0,1)"
                    + " FROM dbo.eDocument_Container_V3 WITH(NOLOCK) "
                    + " WHERE eContainerID = " + ContainerID + " AND eDocumentID = " + DocumentId + " AND ClinicID = " + ClinicID;
                    }
                }


                cmd = new SqlCommand(GetOutputFileInfoCmd, txn.Connection, txn);

                string filePath;
                byte[] txnToken;

                using (rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    rdr.Read();
                    txnToken = rdr.GetSqlBinary(0).Value;
                    filePath = rdr.GetSqlString(1).Value;
                    rdr.Close();
                }

                handle = Cls_NativeSqlClient.GetSqlFilestreamHandle(filePath, Cls_NativeSqlClient.DesiredAccess.Write, txnToken);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                _ErrorMessage = ex.Message;

                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();   
                    cmd.Dispose();
                    cmd.Dispose();  
                }
                if (rdr != null)
                {
                    rdr.Dispose();
                }
            }
            
            return handle;
        }

    }
}
