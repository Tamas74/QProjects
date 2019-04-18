using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace gloSecurity
{
    public class gloUser : IDisposable
    {
        private string _databaseConnectionString = "";
        //private string _MessageBoxCaption = "gloPMS";

        private string _MessageBoxCaption = String.Empty;

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        //Code added on 11/04/2008 -by Sagar Ghodke for implementing ClinicID;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }
        //

        #region "Constructor & Destructor"

        public gloUser(string databaseConnectionString)
        {
            _databaseConnectionString = databaseConnectionString;

            //Code added on 11/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            }
            else
            {
                _ClinicID = 0;
            }

            //Added By Pramod Nair For Messagebox Caption 
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
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
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion

            //
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

        ~gloUser()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// method to check whether the given loginName already exists
        /// returns true if loginName exists in database
        /// returns False if loginname does not exists
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public bool CheckUserExists(string loginName)
        {
            int count;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                oDB.Connect(false);
                String strQuery = "SELECT Count(*) FROM User_MST WHERE sLoginName = '" + loginName + "'";
                count = (Int32)oDB.ExecuteScalar_Query(strQuery);
                if (count > 0)
                {
                    //Login name exists
                    return true;
                }
                else
                {
                    //Login name does not exists
                    return false;
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                DBErr = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return true;
        }

        /// <summary>
        /// This method adds new User to database
        /// returns true if user is added successfully
        /// </summary>
        /// <param name="oUser"></param>
        /// <returns></returns>
        public bool AddUser(User oUser)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                oDBParameters.Add("@UserID", oUser.UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@LoginName", oUser.UserName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Password", oUser.Password, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@NickName", oUser.NickName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@FirstName", oUser.FirstName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@MiddleName", oUser.MiddleName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@LastName", oUser.LastName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@SSNNo", oUser.SSNno, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@DOB", oUser.DateOfBirth, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                oDBParameters.Add("@Gender", oUser.Gender, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@MaritalStatus", oUser.MaritalStatus, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Address", oUser.Address1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Address2", oUser.Address2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Street", oUser.Street, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@City", oUser.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@State", oUser.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ZIP", oUser.ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PhoneNo", oUser.PhoneNo, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@MobileNo", oUser.MobileNo, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@FAX", oUser.FAX, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Email", oUser.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Administrator", oUser.IsAdministrator, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@ProviderID", oUser.ProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@IsAuditTrail", oUser.IsAuditTrail, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@nAccessDenied", oUser.IsAccessDenied, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@bCoSign", oUser.IsCoSign, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                oDBParameters.Add("@IsPasswordSettings", oUser.IsPasswordSettings, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);

                //
                oDBParameters.Add("@nClinicID", this.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                //

                //for storing image into database
                //convert image into memory stream
                //then get byte array of the image & send the array to databse
                if (oUser.Signature != null)
                {
                    MemoryStream ms = new MemoryStream();
                    oUser.Signature.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    try
                    {
                        ms.Flush();
                    }
                    catch
                    {
                    }
                    Byte[] arrImage = ms.ToArray();
                   
                    oDBParameters.Add("@Signature", arrImage, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
                    try
                    {
                        ms.Close();
                        ms.Dispose();
                        ms = null;
                    }
                    catch
                    {
                    }
                    finally
                    {
                        arrImage = null;
                    }
                }
                else
                {
                    oDBParameters.Add("@Signature", null, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Image);
                }


                oDB.Execute("gsp_InUpUser", oDBParameters);
                return true;

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                DBErr = null;
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

                return false;
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
        }

        /// <summary>
        /// this method gets list of all providers and their providerId
        /// to Fill into combo box.
        /// </summary>
        /// <returns></returns>
        internal DataTable GetAllProviders()
        {
            DataTable dtProviders = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                oDB.Connect(false);
                //
                //String strQuery = "SELECT nProviderID, (sFirstName+' '+sMiddleName+' '+sLastName) as ProviderName FROM Provider_MST ORDER BY ProviderName";
                //
                String strQuery = "";
                if (ClinicID != 0)
                {
                    strQuery = "SELECT DISTINCT nProviderID, (sFirstName+' '+sMiddleName+' '+sLastName) as ProviderName FROM Provider_MST,AB_Resource_MST WHERE AB_Resource_MST.nClinicID = " + ClinicID + " ORDER BY ProviderName";
                }
                else
                { strQuery = "SELECT DISTINCT nProviderID, (sFirstName+' '+sMiddleName+' '+sLastName) as ProviderName FROM Provider_MST,AB_Resource_MST ORDER BY ProviderName"; }
                //
                oDB.Retrive_Query(strQuery, out dtProviders);
                strQuery = null;
                return dtProviders;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                DBErr = null;
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        /// <summary>
        /// Get all details of given User from database
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns></returns>
        public DataTable GetUser(long _userId)
        {
            DataTable dtUser = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                oDB.Connect(false);
                //String strQuery = "SELECT * FROM User_MST WHERE nUserID = " + _userId + "";
                  String strQuery = "SELECT  nUserID, sLoginName, sPassword, sFirstName, sMiddleName, sLastName, sSSNNo, dtDOB, sGender, sMaritalStatus, sAddress, sStreet, sCity, sState, " +
                                    " sZIP, sPhoneNo, sMobileNo, sFAX, sEmail, nBlockStatus, nAdministrator, nProviderID, sNickName, imgSignature, IsPasswordReset, IsAuditTrail, " +
                                    " nAccessDenied, bCoSign, bIsSecurityUser, nClinicID, IsExchangeUser, sExchangeLogin, sExchangePassword, sAddress2, IsPasswordSettings, " +
                                    " sCountry, sCounty, IsPatientChartAccess, sAccessPassword, dtValidupto, sSpeciality FROM User_MST WHERE nUserID = " + _userId + "";

                oDB.Retrive_Query(strQuery, out dtUser);
                strQuery = null;
                return dtUser;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                DBErr = null;
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        /// <summary>
        /// get All types of users from databse
        /// </summary>
        /// <returns></returns>
        internal DataTable getAllUsers()
        {
            DataTable dtUsers = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                oDB.Connect(false);
                //
                //String strQuery = "SELECT nUserID,sLoginName,(sFirstName+' '+sMiddleName+' '+sLastName) as Name, sPhoneNo,sMobileNo,nAdministrator FROM User_MST";
                //
                String strQuery = "";
                if (ClinicID != 0)
                {
                    strQuery = "SELECT nUserID,sLoginName,(sFirstName+' '+sMiddleName+' '+sLastName) as Name, sPhoneNo,sMobileNo,nAdministrator FROM User_MST where nClinicID = " + ClinicID + " ";
                }
                else { strQuery = "SELECT nUserID,sLoginName,(sFirstName+' '+sMiddleName+' '+sLastName) as Name, sPhoneNo,sMobileNo,nAdministrator FROM User_MST "; }
                //
                oDB.Retrive_Query(strQuery, out dtUsers);
                strQuery = null;
                return dtUsers;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                DBErr = null;
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        /// <summary>
        /// Get All Active users from databse
        /// </summary>
        /// <returns></returns>
        internal DataTable getActiveUsers()
        {
            DataTable dtUsers = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                oDB.Connect(false);
                //
                String strQuery = "";   //
                if (ClinicID != 0)
                { strQuery = "SELECT nUserID,sLoginName,(sFirstName+' '+sMiddleName+' '+sLastName) as Name, sPhoneNo,sMobileNo,nAdministrator FROM User_MST WHERE (nBlockStatus = 0 OR nBlockStatus is null) AND nClinicID = " + ClinicID + " "; }
                else
                { strQuery = "SELECT nUserID,sLoginName,(sFirstName+' '+sMiddleName+' '+sLastName) as Name, sPhoneNo,sMobileNo,nAdministrator FROM User_MST WHERE (nBlockStatus = 0 OR nBlockStatus is null) "; }
                //
                oDB.Retrive_Query(strQuery, out dtUsers);
                strQuery = null;
                return dtUsers;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                DBErr = null;
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        /// <summary>
        /// Get All Blocked Users from databse
        /// </summary>
        /// <returns></returns>
        internal DataTable getBlockedUsers()
        {
            DataTable dtUsers = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                oDB.Connect(false);
                String strQuery = "";
                //
                //String strQuery = "SELECT nUserID,sLoginName,(sFirstName+' '+sMiddleName+' '+sLastName) as Name, sPhoneNo,sMobileNo,nAdministrator FROM User_MST WHERE nBlockStatus = 1";
                //
                if (ClinicID != 0)
                { strQuery = "SELECT nUserID,sLoginName,(sFirstName+' '+sMiddleName+' '+sLastName) as Name, sPhoneNo,sMobileNo,nAdministrator FROM User_MST WHERE nBlockStatus = 1 AND nClinicID = " + ClinicID + " "; }
                else
                { strQuery = "SELECT nUserID,sLoginName,(sFirstName+' '+sMiddleName+' '+sLastName) as Name, sPhoneNo,sMobileNo,nAdministrator FROM User_MST WHERE nBlockStatus = 1 "; }
                //
                oDB.Retrive_Query(strQuery, out dtUsers);
                strQuery = null;
                return dtUsers;
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                DBErr = null;
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        /// <summary>
        /// This method is used to Block or Unblock the user          
        /// </summary>
        /// <param name="_userId">User Id Which is to be Updated</param>
        /// <param name="block">bool value indicating whether to Block or Unblock user</param>
        public void BlockUser(Int64 userId, bool block)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                oDB.Connect(false);
                //
                //String strQuery = "UPDATE User_MST SET nBlockStatus = '" + block + "' WHERE nUserID = " + userId + "";
                //
                String strQuery = "UPDATE User_MST SET nBlockStatus = '" + block + "',nAccessDenied = '" + block + "' WHERE nUserID = " + userId + " AND nAdministrator <> 1";
                //
                oDB.Execute_Query(strQuery);
                strQuery = null;

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
                DBErr = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
        }

        public bool AddUserDiaplaySettings(String DisplaySettingFilePath)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@UserID", Convert.ToInt64(appSettings["UserID"]), ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@MachineName", System.Windows.Forms.SystemInformation.ComputerName, ParameterDirection.Input, SqlDbType.VarChar);

                //System.IO.FileInfo fInfo = new System.IO.FileInfo(DisplaySettingFilePath);
                //System.IO.FileStream stream = null;// new FileStream(DisplaySettingFilePath,  FileMode.Open,  System.Security.AccessControl.FileSystemRights.Read, FileShare.Read );
                //Stream a = null;

                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //ms.ToArray();
                //Byte[] arrImage = ms.GetBuffer();

                //stream = new System.IO.FileStream(DisplaySettingFilePath, System.IO.FileMode.Open);
                //  stream.Close();
                //stream = null;

                oDBParameters.Add("@DisplaySetting", ConvertFiletoBinary(DisplaySettingFilePath), ParameterDirection.Input, SqlDbType.Image);

                oDB.Connect(false);
                oDB.Execute("US_INUP_UserDisplaySetting", oDBParameters);
                oDBParameters = null;
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return true;
        }

        public bool GetUserDiaplaySettings(String DisplaySettingFilePath)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            try
            {
                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@UserID", Convert.ToInt64(appSettings["UserID"]), ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Connect(false);
                DataTable dt = null;
                oDB.Retrive("US_SELECT_UserDisplaySetting", oDBParameters, out dt);
                oDBParameters = null;
                oDB.Disconnect();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        GenerateFile((object)dt.Rows[0]["DisplaySetting"], DisplaySettingFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                //  MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return true;
        }


        // Convert the File into the Binary 
        public byte[] ConvertFiletoBinary(string strFileName)
        {
            //If File.Exists(strFileName) Then 
            // Dim ofile As FileStream 
            // ofile = New FileStream(strFileName, FileMode.Open, FileAccess.Read) 
            // Dim Br As BinaryReader 
            // Br = New BinaryReader(ofile) 
            // Dim bytesRead As Byte() = Br.ReadBytes(ofile.Length) 
            // Return bytesRead 
            //Else 
            // Return Nothing 
            //End If 

            if (File.Exists(strFileName))
            {
                try
                {
                    FileStream oFile;
                    BinaryReader oReader;
                    //'Please uncomment the following line of code to read the file, even the file is in use by same or another process 
                    //oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous) 

                    //'To read the file only when it is not in use by any process 
                    oFile = new FileStream(strFileName, FileMode.Open, FileAccess.Read);

                    oReader = new BinaryReader(oFile);
                    byte[] bytesRead = oReader.ReadBytes(Convert.ToInt32(oFile.Length));

                    oFile.Close();
                    oReader.Close();
                    oFile.Dispose();
                    oFile = null;
                    oReader.Dispose();
                    oReader = null;
                    return bytesRead;
                }

                catch (IOException ex)
                {
                    MessageBox.Show("Error while conversion - " + ex.ToString());
                    return null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while conversion - " + ex.ToString());
                    return null;
                }

                finally
                {
                }
            }

            else
            {
                return null;
            }
        }

        // 
        public string GenerateFile(object contentFromDB, string strFileName)
        {
            if ((contentFromDB != null))
            {
                if (File.Exists(strFileName) == true)
                {
                    File.Delete(strFileName);
                }
                byte[] content = (byte[])contentFromDB;
               // MemoryStream stream = new MemoryStream(content);
                System.IO.FileStream oFile = new System.IO.FileStream(strFileName, System.IO.FileMode.OpenOrCreate);
                oFile.Write(content, 0, content.Length);
                //stream.WriteTo(oFile);
                oFile.Close();
                oFile.Dispose();
                oFile = null;
                //stream.Close();
                //stream = null;
                content = null;
                return strFileName;
            }
            else
            {
                return null;
            }
        }
    }

    public class User : IDisposable
    {
        #region Declaration

        private Int64 _nUserID;
        private string _sUserName;
        private string _sPassword;
        private string _sNickName;
        private string _sFirstName;
        private string _sMiddleName;
        private string _sLastName;
        private string _sSSNno;
        private DateTime _dtDOB;
        private string _sGender;
        private string _sMaritalStatus;
        private string _sAddress1;
        private string _sAddress2;
        private string _sStreet;
        private string _sCity;
        private string _sState;
        private string _sZIP;
        private string _sPhoneNo;
        private string _sMobileNo;
        private string _sFAX;
        private string _sEmail;
        private byte _nBlockStatus;
        private bool _blnAdministrator;
        private Int64 _nProviderID = 0;
        private string _sProviderName;
        private bool _isPasswordReset = false;
        private bool _isAuditTrail;
        private bool _blnAccessDenied = false;
        private bool _blnCoSign;
        private bool _isPasswordSettings = false;
        private Image _signature;


        #endregion

        #region Properties

        public Int64 UserID
        {
            get { return _nUserID; }
            set { _nUserID = value; }
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

        public string NickName
        {
            get { return _sNickName; }
            set { _sNickName = value; }
        }

        public string FirstName
        {
            get { return _sFirstName; }
            set { _sFirstName = value; }
        }

        public string MiddleName
        {
            get { return _sMiddleName; }
            set { _sMiddleName = value; }
        }

        public string LastName
        {
            get { return _sLastName; }
            set { _sLastName = value; }
        }

        public string SSNno
        {
            get { return _sSSNno; }
            set { _sSSNno = value; }
        }

        public DateTime DateOfBirth
        {
            get { return _dtDOB; }
            set { _dtDOB = value; }
        }

        public string Gender
        {
            get { return _sGender; }
            set { _sGender = value; }
        }

        public string MaritalStatus
        {
            get { return _sMaritalStatus; }
            set { _sMaritalStatus = value; }
        }

        public string Address1
        {
            get { return _sAddress1; }
            set { _sAddress1 = value; }
        }

        public string Address2
        {
            get { return _sAddress2; }
            set { _sAddress2 = value; }
        }

        public string Street
        {
            get { return _sStreet; }
            set { _sStreet = value; }
        }

        public string City
        {
            get { return _sCity; }
            set { _sCity = value; }
        }

        public string State
        {
            get { return _sState; }
            set { _sState = value; }
        }

        public string ZIP
        {
            get { return _sZIP; }
            set { _sZIP = value; }
        }

        public string PhoneNo
        {
            get { return _sPhoneNo; }
            set { _sPhoneNo = value; }
        }

        public string MobileNo
        {
            get { return _sMobileNo; }
            set { _sMobileNo = value; }
        }

        public string FAX
        {
            get { return _sFAX; }
            set { _sFAX = value; }
        }

        public string Email
        {
            get { return _sEmail; }
            set { _sEmail = value; }
        }

        public byte BlockStatus
        {
            get { return _nBlockStatus; }
            set { _nBlockStatus = value; }
        }

        public bool IsAdministrator
        {
            get { return _blnAdministrator; }
            set { _blnAdministrator = value; }
        }

        public Int64 ProviderID
        {
            get { return _nProviderID; }
            set { _nProviderID = value; }
        }

        public string ProviderName
        {
            get { return _sProviderName; }
            set { _sProviderName = value; }
        }

        public bool IsPasswordReset
        {
            get { return _isPasswordReset; }
            set { _isPasswordReset = value; }
        }

        public bool IsAuditTrail
        {
            get { return _isAuditTrail; }
            set { _isAuditTrail = value; }
        }

        public bool IsAccessDenied
        {
            get { return _blnAccessDenied; }
            set { _blnAccessDenied = value; }
        }

        public bool IsCoSign
        {
            get { return _blnCoSign; }
            set { _blnCoSign = value; }
        }

        public bool IsPasswordSettings
        {
            get { return _isPasswordSettings; }
            set { _isPasswordSettings = value; }
        }

        public Image Signature
        {
            get { return _signature; }
            set { _signature = value; }
        }

        #endregion

        #region "Constructor & Destructor"


        public User()
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

        ~User()
        {
            Dispose(false);
        }

        #endregion

    }

    public class Users : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public Users()
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


        ~Users()
        {
            Dispose(false);
        }
        #endregion

        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(User item)
        {
            _innerlist.Add(item);
        }


        public bool Remove(User item)
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

        public User this[int index]
        {
            get
            { return (User)_innerlist[index]; }
        }

        public bool Contains(User item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(User item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(User[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }

    }

    public class gloValidateUser : IDisposable
    {

        #region "Constructor & Destructor"

        public gloValidateUser(string databaseConnectionString)
        {
            _databaseconnectionstring = databaseConnectionString;
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

        ~gloValidateUser()
        {
            Dispose(false);
        }

        #endregion


        #region "Declarations"

        private string _databaseconnectionstring = "";
        private bool _blnIsAcessDenied = false;
        private bool _bInIsAdministrator = false;
        private bool _blnResetPwdFlag = false;
        private Int64 _nUserID;

        public Int64 nUserID
        {
            get { return _nUserID; }
            set { _nUserID = value; }
        }
        #endregion "Declarations"


        /// <summary>
        /// Function to check whether the User trying to login is Administrator.
        /// </summary>
        /// <param name="nUserID"></param>
        /// <returns>
        /// If Administrator -> Return TRUE
        /// ELSE -> Return FALSE
        /// </returns>
        public bool chkIsAdminAccess()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable result=null;
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nUserID", _nUserID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_IsAdministrator", oParameters, out result);
                if (result != null)
                {
                    if (!(result.Rows[0]["nAdministrator"] == Convert.DBNull))
                    {
                        return Convert.ToBoolean(result.Rows[0]["nAdministrator"]);
                    }
                    else
                    { return false; }
                }
                else
                {
                    return false;
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (result != null) { result.Dispose(); result = null; }
                oParameters = null;
            }

        }

        public bool chkIsProviderAccess()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strQuery = null;
            Object ProviderID = null;
            try
            {
                oDB.Connect(false);
                if (_nUserID > 0)
                {
                    strQuery = "select nProviderID from dbo.User_MST where nUserID =" + _nUserID + " ";
                    //Int64 nProviderID = oDB.Execute_Query(strQuery);
                    ProviderID = oDB.ExecuteScalar_Query(strQuery);
                    if (Convert.ToInt64(ProviderID) > 0)
                    { return true; } //Convert.ToBoolean(nProviderID); }
                    else
                    { return false; }

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR : " + ex.Message, "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                strQuery = null;
                ProviderID = null;
            }
        }


        /// <summary>
        /// Checking user's Access if access is denied then return true
        ///  else return false.
        /// </summary>
        /// <returns>
        /// AccessDenied -> True
        /// ELSE -> False
        ///</returns>
        public bool chkIsAccessDenied(string sLoginName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt=null;

            try
            {
                //string strQuery = "select nUserID,nAccessDenied from User_MST where sLoginName =   '" + txtUserName.Text.Trim() + "' ";
                string strQuery = "select nUserID,nAccessDenied from User_MST where sLoginName='" + sLoginName.Replace("'","''") + "'";
                oDB.Retrive_Query(strQuery, out dt);

                strQuery = null;
                //Check for Data Table Null  
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!(dt.Rows[0]["nAccessDenied"] == Convert.DBNull))
                        {
                            _blnIsAcessDenied = Convert.ToBoolean(dt.Rows[0]["nAccessDenied"]);

                            //Setting the UserID.
                            _nUserID = Convert.ToInt64(dt.Rows[0]["nUserID"]);
                            return _blnIsAcessDenied;
                        }
                        else
                        {
                            return _blnIsAcessDenied = false;
                        }
                    }                   
                }
                return _blnIsAcessDenied = false;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return _blnIsAcessDenied = false;

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dt != null) { dt.Dispose(); dt = null; }
            }


        }


        /// <summary>
        /// Setting access denied to given user then return true else fales  
        /// case: 0004256 [21/04/2010]
        /// </summary>
        /// <returns>
        /// if Set Denied return->True
        /// ELSE -> False
        ///</returns>
        public bool SetAccessDenied(string sLoginName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);

            try
            {
                string strQuery = "update User_MST set nAccessDenied = 1 where sLoginName ='" + sLoginName.Replace("'","''") + "'";
                int _result = oDB.Execute_Query(strQuery);
                strQuery = null;
                return Convert.ToBoolean(_result);

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return false;

            }

            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }


        }

        /// <summary>
        /// Function to check whether the User is Administrator or not.
        /// case: 0004256 [21/04/2010]
        /// </summary>
        /// <param name="sLoginName"></param>
        /// <returns>
        /// If Administrator -> Return TRUE
        /// ELSE -> Return FALSE
        /// </returns>
        public bool IsUserAdministrator(string sLoginName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt=null;
            try
            {
                string strQuery = "select nAdministrator from User_MST where sLoginName like '" + sLoginName.Replace("'","''") + "'";

                oDB.Retrive_Query(strQuery, out dt);

                strQuery = null;
                //Check for Data Table Null  
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!(dt.Rows[0]["nAdministrator"] == Convert.DBNull))
                        {
                            _bInIsAdministrator = Convert.ToBoolean(dt.Rows[0]["nAdministrator"]);

                            return _bInIsAdministrator;
                        }
                        else
                        {
                            return _bInIsAdministrator = false;
                        }
                    }
                   
                }
                return _bInIsAdministrator = false;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return _bInIsAdministrator = false;

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dt != null) { dt.Dispose(); dt = null; }
            }

        }

        /// <summary>
        /// Function to check whether password is reseted or not.
        /// case: 0004256 [21/04/2010]
        /// </summary>
        /// <param name="sLoginName"></param>
        /// <returns>
        /// If reseted -> Return TRUE
        /// ELSE -> Return FALSE
        /// </returns>
        public bool IsPasswordResetted(string sLoginName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt=null;
            try
            {
                string strQuery = "select IsPasswordReset, nUserID from User_MST where sLoginName like '" + sLoginName.Replace("'","''") + "'";

                oDB.Retrive_Query(strQuery, out dt);

                strQuery = null;
                //Check for Data Table Null  
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!(dt.Rows[0]["IsPasswordReset"] == Convert.DBNull))
                        {
                            _blnResetPwdFlag = Convert.ToBoolean(dt.Rows[0]["IsPasswordReset"]);

                            return _blnResetPwdFlag;
                        }
                        else
                        {
                            return _blnResetPwdFlag = false;
                        }
                    }
                   
                }
                return _blnResetPwdFlag = false;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return _blnResetPwdFlag = false;

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dt != null) { dt.Dispose(); dt = null; }
            }
        }

        /// <summary>
        /// Function to check whether password complexity is validated or not.
        /// case: 0004256 [21/04/2010]
        /// </summary>
        /// <param name="sLoginName"></param>
        /// <returns>
        /// If reseted -> Return TRUE
        /// ELSE -> Return FALSE
        /// </returns>
        public bool ValidatePassword(ref string msg, string sLoginName, string psw)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt = null;
            int _ExpCapitalLetters = 0;
            int _ExpNoOfLetters = 0;
            int _ExpNoOfDigits = 0;
            int _ExpNoOfSpecChars = 0;
            int _ExpPwdLength = 0;
            int _ExpTimeFrameinDays = 0;
            int _nClinicID = 0;
            bool _validatepassword = true;


            try
            {
                string strQuery = "select isnull(ExpCapitalLetters,'0') as ExpCapitalLetters, isnull(ExpNoOfLetters,0)as ExpNoOfLetters , isnull(ExpNoOfDigits,0) as ExpNoOfDigits, isnull(ExpNoOfSpecChars,0)  as ExpNoOfSpecChars, isnull(ExpPwdLength,0) as ExpPwdLength , isnull(ExpTimeFrameinDays,0) as ExpTimeFrameinDays, isnull(nClinicID,0) as nClinicID  from PwdSettings";

                oDB.Retrive_Query(strQuery, out dt);

                strQuery = null;
                //Check for Data Table Null  
                if (dt != null)
                {
                    if (dt.Rows.Count >0)
                    {
                        if ((Convert.ToInt32(dt.Rows[0]["ExpCapitalLetters"]) != 0))
                        {
                            _ExpCapitalLetters = Convert.ToInt32(dt.Rows[0]["ExpCapitalLetters"]);
                        }
                        else
                        {
                            _ExpCapitalLetters = 0;
                        }

                        if ((Convert.ToInt32(dt.Rows[0]["ExpNoOfLetters"]) != 0))
                        {
                            _ExpNoOfLetters = Convert.ToInt32(dt.Rows[0]["ExpNoOfLetters"]);
                        }
                        else
                        {
                            _ExpNoOfLetters = 0;
                        }

                        if ((Convert.ToInt32(dt.Rows[0]["ExpNoOfDigits"]) != 0))
                        {
                            _ExpNoOfDigits = Convert.ToInt32(dt.Rows[0]["ExpNoOfDigits"]);
                        }
                        else
                        {
                            _ExpNoOfDigits = 0;
                        }

                        if ((Convert.ToInt32(dt.Rows[0]["ExpNoOfSpecChars"]) != 0))
                        {
                            _ExpNoOfSpecChars = Convert.ToInt32(dt.Rows[0]["ExpNoOfSpecChars"]);
                        }
                        else
                        {
                            _ExpNoOfSpecChars = 0;
                        }

                        if ((Convert.ToInt32(dt.Rows[0]["ExpPwdLength"]) != 0))
                        {
                            _ExpPwdLength = Convert.ToInt32(dt.Rows[0]["ExpPwdLength"]);
                        }
                        else
                        {
                            _ExpPwdLength = 0;
                        }

                        if ((Convert.ToInt32(dt.Rows[0]["ExpTimeFrameinDays"]) != 0))
                        {
                            _ExpTimeFrameinDays = Convert.ToInt32(dt.Rows[0]["ExpTimeFrameinDays"]);
                        }
                        else
                        {
                            _ExpTimeFrameinDays = 0;
                        }

                        if ((Convert.ToInt32(dt.Rows[0]["ExpCapitalLetters"]) != 0))
                        {
                            _ExpCapitalLetters = Convert.ToInt32(dt.Rows[0]["ExpCapitalLetters"]);
                        }
                        else
                        {
                            _ExpCapitalLetters = 0;
                        }

                        if ((Convert.ToInt32(dt.Rows[0]["nClinicID"]) != 0))
                        {
                            _nClinicID = Convert.ToInt32(dt.Rows[0]["nClinicID"]);
                        }
                        else
                        {
                            _nClinicID = 0;
                        }

                        // check first for password reseted or not ..if not reseted then check for pasd Complexity
                        //if (!IsPasswordResetted(sLoginName))
                        //{
                        // check for all criteria for password complexity is valid or not if valid return true...
                        if (IsPswdComplexityValid(ref msg, sLoginName, psw, _ExpPwdLength, _ExpCapitalLetters, _ExpNoOfDigits, _ExpNoOfLetters, _ExpNoOfSpecChars))
                        {
                            //return true; complexity criteria matching or do not have for matching any value
                            _validatepassword = true;
                        }
                        else
                        {
                            //return false;  criteria not match so return false
                            
                            //MessageBox.Show ("Give message here ");
                            _validatepassword = false;
                        }
                         //}
                    }                    
                   
                    return _validatepassword;  // it return true;
                }
                else
                {
                    //return true;  // do not have any criteria for matching or comparing so return true...
                    return _validatepassword;
                }

                //return _validatepassword;  // it return true;
            }

            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return _validatepassword = true;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dt != null) { dt.Dispose(); dt = null; }
            }
        }


        /// <summary>
        /// Function to check whether password complexity all criteriya is validated or not.
        /// case: 0004256 [21/04/2010]
        /// </summary>
        /// 
        /// <returns>
        /// If rcriteria validated -> Return TRUE
        /// ELSE -> Return FALSE
        /// </returns>
        public bool IsPswdComplexityValid(ref string msg,string sLoginName, string pwd, int _ExpPwdLength, int _ExpCapitalLetters, int _ExpNoOfDigits, int _ExpNoOfLetters, int _ExpNoOfSpecChars)
        {
            //int _ExpCapitalLetters = 0;
            //int _ExpNoOfLetters = 0;
            //int _ExpNoOfDigits = 0;
            //int _ExpNoOfSpecChars = 0;
            //int _ExpPwdLength = 0;
            //int _ExpTimeFrameinDays = 0;
            //int _nClinicID = 0;
            System.Text.RegularExpressions.Regex upper = new System.Text.RegularExpressions.Regex("[A-Z]");
            System.Text.RegularExpressions.Regex lower = new System.Text.RegularExpressions.Regex("[a-z]");
            System.Text.RegularExpressions.Regex letters = new System.Text.RegularExpressions.Regex("[a-zA-Z]");
            System.Text.RegularExpressions.Regex number = new System.Text.RegularExpressions.Regex("[0-9]");

            System.Text.RegularExpressions.Regex special = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");

            try
            {


                if (pwd.Trim().Length < _ExpPwdLength)
                {
                    msg = "The length of the password should be atleast " + _ExpPwdLength + " characters";
                    return false;
                }

                if (upper.Matches(pwd).Count < _ExpCapitalLetters)
                {
                    msg = "The password should contain atleast " + _ExpCapitalLetters + " upper case letter";
                    return false;
                }

                if (lower.Matches(pwd).Count < 0)
                {
                    // msg = "The password should contain atleast '" + 0 + "' lower case letter"; 
                    return false;
                }

                if (number.Matches(pwd).Count < _ExpNoOfDigits)
                {
                    msg = "The password should contain atleast " + _ExpNoOfDigits + " digits";
                    return false;
                }

                if (special.Matches(pwd).Count < _ExpNoOfSpecChars)
                {
                    msg = "The password should contain atleast " + _ExpNoOfSpecChars + " special characters";
                    return false;
                }

                if (pwd.ToUpper() == sLoginName.Trim().ToUpper())
                {
                    msg = "The password should not be same as your login name";
                    return false;
                }

                if (letters.Matches(pwd).Count < _ExpNoOfLetters)
                {
                    msg = "The password should contain atleast " + _ExpNoOfLetters + " alphabet";
                    return false;
                }

                return true;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return false;
            }
            finally
            {
                upper = null;
                lower = null;
                letters = null;
                number = null;
            }
        }


        /// <summary>
        /// Function to Update User MST with new password & set IsPassword reset to 0 
        /// Also insert  login name, password and datetime into RecentPwdMST table
        /// LoginName,DateTime of Login,Password {{{{case: 0004256 [24/04/2010]}}}}}
        /// </summary>
        /// <param name="sLoginName">Login Name of User</param>
        /// <param name="Password">psw</param>
        /// <returns>
        /// If new record inserted successfully -> Return TRUE
        /// ELSE -> Return FALSE
        /// </returns>
        public bool UpdateUserMST_RecentPwdMST(string sLoginName, string psw)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            //DataTable result;
            string strQuery = null;
            try
            {
                oDB.Connect(false);

                //---------- update User_MST set  '' 
                strQuery = "Update User_MST set sPassword = '" + psw + " ' , IsPasswordReset = 0 where sLoginName like '" + sLoginName + "'";

                int _UpdatedRowCount = oDB.Execute_Query(strQuery);

                if (_UpdatedRowCount > 0)
                {
                    //------------insert into RecentPwd_MST
                    oDB.Connect(false);
                    _UpdatedRowCount = 0;
                    strQuery = string.Empty;
                    //result = new DataTable();
                    //oParameters.Add("@LoginName", sLoginName, ParameterDirection.Input, SqlDbType.VarChar);
                    //oParameters.Add("@sPassword", psw, ParameterDirection.Input, SqlDbType.VarChar);
                    ////oParameters.Add("@PwdCreationDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    //oDB.Retrive("InsertRecentPwd_MST", oParameters, out result);


                    strQuery = "Insert into RecentPwd_MST(LoginName,sPassword,PwdCreationDate) values ('" + sLoginName + "','" + psw + " ',dbo.gloGetDate())  ";

                    _UpdatedRowCount = oDB.Execute_Query(strQuery);
                    if (_UpdatedRowCount > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                strQuery = null;
            }
        }

        /// <summary>
        /// Function to check whether password is recently used ?.
        /// case: 0004256 [23/04/2010]
        /// </summary>
        /// <param name="sLoginName">Login Name of User</param>
        /// <param name="Password">psw</param>
        /// <returns>
        /// If recently used -> Return TRUE
        /// ELSE -> Return FALSE
        /// </returns>
        public bool IsRecentUsedPSW(string sLoginName, string psw)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            ClsEncryption oEncrypt = new ClsEncryption();
            const string _encryptionKey = "12345678";
            oDB.Connect(false);
            DataTable dt=null;
           
            ArrayList PwdStr = new ArrayList();            
            bool _blnRecentUsedflag = false;
            string strQuery = string.Empty;           
            int noofdays = 0;    
           
            try
            {
                strQuery = "select sPassword, PwdCreationDate from RecentPwd_MST where LoginName like '" + sLoginName + "'";

                oDB.Retrive_Query(strQuery, out dt);

                //Check for Data Table Null  
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (!(dt.Rows[0]["PwdCreationDate"] == Convert.DBNull))
                            {
                                // DateTime  30 days logic 
                              
                                oDB.Connect(false);
                                strQuery = string.Empty;                               
                                
                                strQuery = "SELECT DATEDIFF(day,'" + dt.Rows[i]["PwdCreationDate"] + "', dbo.gloGetDate()) AS no_of_days";

                                noofdays = Convert.ToInt32(oDB.ExecuteScalar_Query(strQuery));

                                if (noofdays <= 30)
                                {                                    
                                    PwdStr.Add(oEncrypt.DecryptFromBase64String(Convert.ToString (dt.Rows[i]["sPassword"]), _encryptionKey));
                                }
                            }
                        }

                        for (int i = 0; i <= PwdStr.Count-1 ; i++)
                        {
                            if (psw == PwdStr[i].ToString())
                            {
                                _blnRecentUsedflag = true;
                            }
                        }

                    }

                }
                return _blnRecentUsedflag; //false;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (oEncrypt != null) { oEncrypt.Dispose(); oEncrypt = null; }
                if (dt != null) { dt.Dispose(); dt = null; }
                PwdStr = null;
                strQuery = null;
            }
        }


        public bool chkLogin(string sLoginName, string sPassword)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable dt=null;
            try
            {
                oDB.Connect(false);
                oParameters.Add("@LoginName", sLoginName, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@Password", sPassword, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("gsp_CheckLogin", oParameters, out dt);

                if (dt != null)
                {
                    Int64 i = Convert.ToInt64(dt.Rows[0][0]);
                    if (i != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                return false;
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return false;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dt != null) { dt.Dispose(); dt = null; }
            }


        }

        public bool chkISBlocked(string sLoginName)
        {


            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            DataTable dt=null;


            try
            {
                //string strQuery = "select nUserID,nAccessDenied from User_MST where sLoginName =   '" + txtUserName.Text.Trim() + "' ";
                string strQuery = "select nUserID,nBlockStatus from User_MST where sLoginName='" + sLoginName.Replace("'","''") + "'";
                oDB.Retrive_Query(strQuery, out dt);
                strQuery = null;

                //Check for Data Table Null  
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (!(dt.Rows[0]["nBlockStatus"] == Convert.DBNull))
                        {
                            _blnIsAcessDenied = Convert.ToBoolean(dt.Rows[0]["nBlockStatus"]);

                            //Setting the UserID.
                            _nUserID = Convert.ToInt64(dt.Rows[0]["nUserID"]);
                            return _blnIsAcessDenied;
                        }
                        else
                        {
                            return _blnIsAcessDenied = false;
                        } 
                    }                    
                }
                return _blnIsAcessDenied = false;

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                return _blnIsAcessDenied = false;

            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dt != null) { dt.Dispose(); dt = null; }
            }

        }


        /// <summary>
        /// Function to store the Login status and time of logged user
        /// LoginName,DateTime of Login,Machine Name.
        /// </summary>
        /// <param name="sLoginName">Login Name of User</param>
        /// <param name="loginStatus"></param>
        /// <param name="sMachineName"></param>
        public void updateLoginStatus(string sLoginName, bool loginStatus, string sMachineName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable result;
            try
            {
                if (loginStatus)
                {
                    oDB.Connect(false);
                    //oParameters.Add("@sLoginName", sLoginName, ParameterDirection.Input, SqlDbType.VarChar);
                    //oParameters.Add("@sMachineName", sMachineName, ParameterDirection.Input, SqlDbType.VarChar);

                    //Commented by Mukesh Patel  20091102                
                    oParameters.Add("@UserName", sLoginName, ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@MachineName", sMachineName, ParameterDirection.Input, SqlDbType.VarChar);

                    oDB.Retrive("gsp_InsertLoginUsers", oParameters, out result);

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                oParameters = null;
            }

        }



    }
}
