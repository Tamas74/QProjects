using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.IO;

namespace gloCommunity.Classes
{
    public class clsBizTalkDBLayer
    {
        //Code Start-Added by kanchan on 20110725 for send CCD via biztalk
        public bool SaveDataInBiztalkQueue(Int64 _PatientID, string _code, string _FirstName, string _MiddleName, string _LastName, string _Gender, DateTime _DateofBirth, long nID, string _FileType, long _clientID,string _clientName, string _sFileCreatedFrom = null)
        {
            SqlCommand cmd = null;
            SqlParameter sqlParam = null;
            try
            {
                //Dim arrByte As Byte() = ConvertFiletoBinary(sFilePath)
                //Dim XMLarrByte As Byte() = ConvertFiletoBinary(sFilePath)
                SqlConnection conn = null;
                string _query = null;

                if (_FileType == "CCD")
                {
                    _query = "select iData from CCD_Exported_Files where nCCDID=" + nID + "";
                }
                else if (_FileType == "DOC")
                {
                    _query = "select sPatientNotes from PatientExams where nExamID=" + nID + "";
                }
                else if (_FileType == "PDF")
                {
                    _query = "select CONVERT(image,iDocumentStream) from eDocument_Container_V3 where eDocumentID=" + nID + "";
                }
                if (_FileType == "PDF")
                {
                    conn = new SqlConnection(clsGeneral.DMSConnectionString);
                }
                else
                {
                    conn = new SqlConnection(clsGeneral.EMRConnectionString);
                }

                cmd = new SqlCommand(_query, conn);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                byte[] arrByte = (byte[])cmd.ExecuteScalar();



                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

               
                conn = new SqlConnection(clsGeneral.EMRConnectionString);

                cmd = new SqlCommand("InsertDatainBiztalkQueue", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = _PatientID;

                sqlParam = cmd.Parameters.Add("@sPatientCode", SqlDbType.VarChar, 50);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = _code;

                sqlParam = cmd.Parameters.Add("@sFirstName", SqlDbType.VarChar, 50);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = _FirstName;

                sqlParam = cmd.Parameters.Add("@sMiddleName", SqlDbType.VarChar, 50);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = _MiddleName;

                sqlParam = cmd.Parameters.Add("@sLastName", SqlDbType.VarChar, 50);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = _LastName;

                sqlParam = cmd.Parameters.Add("@dtDOB", SqlDbType.DateTime);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = _DateofBirth;

                sqlParam = cmd.Parameters.Add("@sGender", SqlDbType.VarChar, 10);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = _Gender;

                sqlParam = cmd.Parameters.Add("@nSSN", SqlDbType.VarChar, 10);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = "";

                sqlParam = cmd.Parameters.Add("@dtDocTimeStamp", SqlDbType.DateTime);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = DateTime.Now;

                sqlParam = cmd.Parameters.Add("@iData", SqlDbType.Image);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = arrByte;

                sqlParam = cmd.Parameters.Add("@sFileName", SqlDbType.VarChar, 50);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = "";

                sqlParam = cmd.Parameters.Add("@sFileType", SqlDbType.VarChar, 10);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = _FileType;

                sqlParam = cmd.Parameters.Add("@sNotes", SqlDbType.VarChar, 100);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = "";

                //Dim _fileHashValue As String = ""
                //Dim _fileHashAlgorithmType As String = ""

                //_fileHashValue = gloSecurity.gloDataHashing.GetSHA1Hash(sFilePath, _fileHashAlgorithmType)

                sqlParam = cmd.Parameters.Add("@sHashValue", SqlDbType.VarChar);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = "";

                sqlParam = cmd.Parameters.Add("@sHashAlgoType", SqlDbType.VarChar);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = "";

                sqlParam = cmd.Parameters.Add("@sFileCreatedFrom", SqlDbType.VarChar);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = _sFileCreatedFrom;

                sqlParam = cmd.Parameters.Add("@nSendTOClientRefId", SqlDbType.BigInt);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = _clientID;

                sqlParam = cmd.Parameters.Add("@sSendTOClientIdentifier", SqlDbType.VarChar);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = _clientName;

                sqlParam = cmd.Parameters.Add("@nFileStatus", SqlDbType.BigInt);
                sqlParam.Direction = ParameterDirection.Input;
                sqlParam.Value = 1;


                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                if (sqlParam != null)
                {
                    sqlParam = null;
                }
            }
        }
        //Code Start-Added by kanchan on 20110725 for send CCD via biztalk
        private byte[] ConvertFiletoBinary(string strFileName)
        {
            if (File.Exists(strFileName))
            {
                FileStream oFile = null;
                BinaryReader oReader = null;
                try
                {
                    oFile = new FileStream(strFileName, FileMode.Open, FileAccess.Read);
                    oReader = new BinaryReader(oFile);
                    byte[] bytesRead = oReader.ReadBytes((int)oFile.Length);
                    return bytesRead;

                }
                catch (IOException ex)
                {
                    throw new Exception(ex.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally
                {
                    if ((oFile != null))
                    {
                        oFile.Close();
                        oFile.Dispose();
                        oFile = null;
                    }
                    if ((oReader != null))
                    {
                        oReader.Close();
                        oReader.Dispose();
                        oReader = null;
                    }

                }
            }
            else
            {
                return null;
            }
        }
        //Code Start-Added by kanchan on 20110726 for send DOC via biztalk
        public DataTable getPatientInfo(Int64 npatientid)
        {
            SqlDataAdapter sqladp = null;
          //  SqlCommand cmd = null;
            SqlConnection cnn = new SqlConnection();
            DataTable dt = new DataTable();
            string str = null;
            try
            {
                cnn.ConnectionString = clsGeneral.EMRConnectionString;
                str = "SELECT isnull(sPatientCode,'') as PatientCode,isnull(sFirstName,'') as FirstName,isnull(sMiddleName,'') as MiddleName,isnull(sLastName,'') as LastName,dtDOB as DOB,ISNULL(sGender,'') as Gender from Patient where npatientid = " + npatientid;
                sqladp = new SqlDataAdapter(str, cnn);
                sqladp.Fill(dt);
                return dt;
            }
            catch //(Exception ex)
            {
                return null;
            }
            //Code End-Added by kanchan on 20110726 for send DOC via biztalk
        }
    }
}




