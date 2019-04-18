using System;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;   
 namespace gloEmdeonInterface.Classes
{
   public class ClsWelchAllynECGLayer
    {
        #region "Constructor and Destructor"

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

        public ClsWelchAllynECGLayer()
        {

        }
        public ClsWelchAllynECGLayer(string gloConnectionString)
        {
            ConnectionString = gloConnectionString;
        }
        ~ClsWelchAllynECGLayer()
        {
            Dispose(false);
        }


        #endregion "Constructor and Destructor"

        #region "Class variables"
        private string sConnectionString = string.Empty;
        private const string ConstEncryptDecryptKey = "12345678";
        private long _nClinicID = 0;
        private long _nLoginUserID = 0;
        private long _nPatientID = 0;

        public string ConnectionString
        {
            get { return sConnectionString; }
            set { sConnectionString = value; }
        }
        

        public long ClinicID
        {
            get 
            {
                return _nClinicID;
            }
            set
            {
                _nClinicID = value;
            }
        }

        public long LoginUserID
        {
            get 
            {
                return _nLoginUserID;
            }
            set
            {
                _nLoginUserID = value;
            }
        }

        public long PatinetID
        {
            get
            {
                return _nPatientID; 
            }
            set
            {
                _nPatientID = value;
            }
        }


        public enum DeviceType { NoDeviceSelected, HeartCentrixECGDevice, WelchAllynECGDevice,  MidMarkECGDevice }

        #endregion "Class variables"
    
       
        public DataTable WA_GetPationtDemograpicData(long PatientID)
        {
            DataTable _dtGetPationtDemograpicData = null;
            gloDatabaseLayer.DBLayer OdbLayer = null;
            string sSqlQry = string.Empty;
            try
            {
                OdbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                sSqlQry = "SELECT [sPatientCode],[sFirstName],[sMiddleName],[sLastName],[dtDOB],[sGender] FROM [dbo].[Patient] WHERE  [nPatientID]= " + PatientID + "";
                OdbLayer.Connect(false);
                OdbLayer.Retrive_Query(sSqlQry, out _dtGetPationtDemograpicData);
                OdbLayer.Disconnect();
            }
            catch (Exception)
            { }
            finally
            {

                if (OdbLayer != null)
                {
                    OdbLayer.Dispose();
                    OdbLayer = null;
                }

            }
            return _dtGetPationtDemograpicData;

        }

        public long WA_SaveUPdateECGTest(long ECGID, long PatientID, long ExamID, long VisitID, long ClinicID, DateTime GivenDate, long GroupID, string CPTCode, string TestType, string ECGPerform, DateTime OrderDate, string PR, string QT, string QTC, string QRSDuration, string PAxis, string QRSAxis, string TAxis, string ECGInterpretation, string OrderInPhysician, string ReviewInPhysician, DateTime ReviewDate, bool AddDt, long MachineID, string OrderId, string TestId, string ExternalCode, string device, long DMSDocumentID = 0)
        {
            long _WA_SaveUPdateECGTest = 0;
            object _Result = null;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Clear();
                oDBParameters.Add("@nECGID", ECGID, ParameterDirection.InputOutput, SqlDbType.BigInt);
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nExamID", ExamID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nVisitID", VisitID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtGivenDate", GivenDate.ToString("yyyy-MM-dd HH:mm:ss tt"), ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@nGroupID", GroupID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sCPTCode", CPTCode, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sTestType", TestType, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sECGPerform", ECGPerform, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@dtOrderDate", OrderDate.ToString("yyyy-MM-dd HH:mm:ss tt"), ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@sPR", PR, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@sQT", QT, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@sQTc", QTC, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@sORSDuration", QRSDuration, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@sPAxis", PAxis, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@sQRSAxis", QRSAxis, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@sTAxis", TAxis, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@sECGInterpretation", ECGInterpretation, ParameterDirection.Input, SqlDbType.VarChar, 4000);
                oDBParameters.Add("@sOrderInPhysician", OrderInPhysician, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@sReviewInPhysician", ReviewInPhysician, ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@dtReviewDate", ReviewDate.ToString("yyyy-MM-dd HH:mm:ss tt"), ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@AddDt", AddDt, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@MachineID", MachineID , ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sOrderId", OrderId, ParameterDirection.Input, SqlDbType.VarChar, 500);
                oDBParameters.Add("@sTestId", TestId, ParameterDirection.Input, SqlDbType.VarChar, 500);
                oDBParameters.Add("@sExternalCode", ExternalCode, ParameterDirection.Input, SqlDbType.VarChar, 500);
                oDBParameters.Add("@sDeviceType", device, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nDMSDocumentID", DMSDocumentID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBLayer.Connect(false);
                oDBLayer.Execute("CV_InUpElectroCardioGram", oDBParameters, out _Result);
                oDBLayer.Disconnect();
                long.TryParse(Convert.ToString(_Result), out _WA_SaveUPdateECGTest);    
            }
            catch (Exception)
            {
                _WA_SaveUPdateECGTest = 0; 
            }
            finally
            {
                _Result = null;

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
                if ((oDBLayer != null))
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
            }
            return _WA_SaveUPdateECGTest;
        }

        public DataTable WA_GetECGTestInformation(long ECGID,long PatientID)
        {
            DataTable _WA_GetECGTestInformation = null;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            string SqlQury = string.Empty;
            try
            {
                SqlQury = "SELECT [nECGID] ,[nPatientID],[nExamID],[nVisitID],[nClinicID],[dtGivenDate] ,[nGroupID] ,[sCPTCode] ,[sTestType]  ,[sECGPerform] ,[dtOrderDate] ,[sPR] ,[sQT] ,[sQTc] ,[sORSDuration],[sPAxis] ,[sQRSAxis] ,[sTAxis] ,[sECGInterpretation] ,[sOrderInPhysician] ,[sReviewInPhysician] ,[dtReviewDate] ,[sOrderId] ,[sTestId] ,[sExternalCode],[sDeviceType],ISNULL([nDMSDocumentID],0) AS [nDMSDocumentID] FROM  [dbo].[CV_ElectroCardioGrams] WHERE [nECGID] =" + ECGID + " AND [nPatientID]=" + PatientID + "";
                oDBLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBLayer.Connect(false);
                oDBLayer.Retrive_Query(SqlQury, out _WA_GetECGTestInformation);    
                oDBLayer.Disconnect();
            }
            catch (Exception)
            {
                _WA_GetECGTestInformation = null;
            }
            finally
            {
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }

                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
            }
            return _WA_GetECGTestInformation;

        }

        public long WA_ECGID(string TestID)
        {

            long nECGID = 0;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            string SQLQury = string.Empty;  
            try
            {
                SQLQury = "SELECT TOP 1 [nECGID] FROM [dbo].[CV_ElectroCardioGrams] WHERE [sTestId]='" + TestID.Trim().Replace("'","''")  + "' AND [sDeviceType]='WelChAllyn'";
                oDBLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                oDBLayer.Connect(false);
                long.TryParse(Convert.ToString(oDBLayer.ExecuteScalar_Query(SQLQury)), out nECGID);
                oDBLayer.Disconnect();
            }
            catch (Exception)
            {
                nECGID = 0;
            }
            finally
            {
               if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
            }
            return nECGID;

        }
     
        public void Get_LoginDetails(long gloEMRUserID, DeviceType _deviceType, out string DeviceUserName, out string DevicePassword)
        {
          
            DeviceUserName = string.Empty;
            DevicePassword = string.Empty;
            DataTable dtGloEMRUserDetails = null;
            gloSecurity.ClsEncryption ObjClsEncryption = null;
            try
            {
                ObjClsEncryption = new gloSecurity.ClsEncryption();
                dtGloEMRUserDetails = Retrive_LoginDetails(gloEMRUserID, _deviceType);
                if (dtGloEMRUserDetails != null && dtGloEMRUserDetails.Rows.Count >= 0)
                {
                    DeviceUserName = Convert.ToString(dtGloEMRUserDetails.Rows[0]["sDeviceUserName"]);
                    DevicePassword = ObjClsEncryption.DecryptFromBase64String(Convert.ToString(dtGloEMRUserDetails.Rows[0]["sPassword"]), ConstEncryptDecryptKey);  
                }
            }
            catch (Exception)
            {
                DeviceUserName = string.Empty;
                DevicePassword = string.Empty;
            }
            finally
            {
                if (ObjClsEncryption != null)
                {
                    ObjClsEncryption.Dispose();
                    ObjClsEncryption = null;
                }
                if (dtGloEMRUserDetails != null)
                {
                    dtGloEMRUserDetails.Dispose();
                    dtGloEMRUserDetails = null; 
                }

            }

           
        }

        public DataTable Retrive_LoginDetails(long UserID, DeviceType _deviceType)
        {
            gloDatabaseLayer.DBLayer OdbLayer = null;
            string SqlQury = string.Empty;
            DataTable dtloginDetails = null;
            string DeviceType = string.Empty;
           try
            {
                switch (_deviceType)
                {
                    case ClsWelchAllynECGLayer.DeviceType.HeartCentrixECGDevice:
                        {
                            DeviceType = "ECG";
                            break;
                        }
                    case ClsWelchAllynECGLayer.DeviceType.WelchAllynECGDevice:
                        {
                            DeviceType = "WelchAllyn ECG";
                            break;
                        }

                }
                SqlQury = "SELECT TOP 1 [sDeviceUserName],[sPassword] FROM [dbo].[User_ExternalCodes] where [nUserId]=" + UserID + " AND [sModulename]='" + DeviceType + "'";
                OdbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                OdbLayer.Connect(false);
                OdbLayer.Retrive_Query(SqlQury, out dtloginDetails);
                OdbLayer.Disconnect();
            }
            catch (Exception)
            {
                dtloginDetails = null;
            }
            finally
            {
                SqlQury = string.Empty;
                if (OdbLayer != null)
                {
                    OdbLayer.Dispose();
                    OdbLayer = null;
                }

            }
            return dtloginDetails; 
        }

        public string Get_AusName(long ClinicID)
        {
            string AUSName=string.Empty ; 
            gloDatabaseLayer.DBLayer OdbLayer = null;
            string SqlQury = string.Empty;
            try
            {
                SqlQury = "SELECT TOP 1 [sExternalcode] FROM [dbo].[Clinic_MST] WHERE [nClinicID]=" + ClinicID + "";
                OdbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                OdbLayer.Connect(false);
                AUSName =Convert.ToString(OdbLayer.ExecuteScalar_Query(SqlQury));  
                OdbLayer.Disconnect();  

            }
            catch (Exception)
            { AUSName = string.Empty; }
            finally
            {
                SqlQury = string.Empty;
                if (OdbLayer != null)
                {
                    OdbLayer.Dispose();
                    OdbLayer = null;
                }

            }
            return AUSName;

        }

        public bool Check_DeviceActivation(DeviceType _deviceType,string AUS,long ClinicID)
        {
            bool _Check_DeviceActivation = false;
            string DeviceName = string.Empty;
            string DeviceKey = string.Empty;
            string ActivationKey = string.Empty;
            string SqlQury = string.Empty;
            gloDatabaseLayer.DBLayer OdbLayer = null;
            gloSecurity.ClsEncryption ObjClsEncryption = null;           
            try
            {
                OdbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                ObjClsEncryption = new gloSecurity.ClsEncryption();              
                switch (_deviceType)
                {
                    case DeviceType.HeartCentrixECGDevice:
                        {
                            DeviceName = "ECGDEVICEKEY";
                            ActivationKey = "gL0@PPs2k9!8605";
                            break;
                        }
                    case DeviceType.WelchAllynECGDevice:
                        {
                            DeviceName = "WELCHALLYNECGDEVICEKEY";
                            ActivationKey = "gL0@PPs2k9!8610";
                            break;
                        }
                    case DeviceType.MidMarkECGDevice:
                        {
                            DeviceName = "MIDMARKECGDEVICEKEY";
                            ActivationKey = "gL0@PPs2k9!7482";
                            break;
                        }

                }
                SqlQury = "SELECT TOP 1 [sSettingsValue] FROM [dbo].[Settings] WHERE [sSettingsName] = '" + DeviceName + "'";
                OdbLayer.Connect(false);
                DeviceKey = Convert.ToString(OdbLayer.ExecuteScalar_Query(SqlQury));   
                OdbLayer.Disconnect();
                if (DeviceKey.Trim().Length <= 0)
                {
                    _Check_DeviceActivation = false;
                    return _Check_DeviceActivation;
                }
                else
                {
                    DeviceKey = ObjClsEncryption.DecryptFromBase64String(DeviceKey, ConstEncryptDecryptKey);
                    if (ObjClsEncryption.EncryptToBase64String(String.Concat(AUS.Trim().ToLower(), ActivationKey), "87654321") == DeviceKey.Trim())
                    {
                        _Check_DeviceActivation = true;
                        return _Check_DeviceActivation;
                    }
                    else
                    {
                        _Check_DeviceActivation = false;
                        return _Check_DeviceActivation;
                    }
 
                }

            }
            catch (Exception)
            {}
            finally
            {
                if (OdbLayer != null)
                {
                    OdbLayer.Dispose();
                    OdbLayer = null;
                }
                if (ObjClsEncryption != null)
                {
                    ObjClsEncryption.Dispose();
                    ObjClsEncryption = null;
                }
            }

            return _Check_DeviceActivation;
        }

        public String Check_WelchAllynECGDeviceSettings(long ClinicID,string  DeviceType)
        {
            string _Massage=string.Empty;
            bool IsDeviceEnabled = false;
            string DeviceKey = string.Empty; 
            gloDatabaseLayer.DBLayer OdbLayer = null;
            gloSecurity.ClsEncryption ObjClsEncryption = null;
            string SqlQury = string.Empty;
            DataTable dtWelchyAllyn = null;
            string AUSname = string.Empty;  
            try
            {
                AUSname = Get_AusName(ClinicID);
                if (AUSname.Trim().Length <= 0)
                {
                    _Massage = "AUS Settings not found";
                    return _Massage; 
                }
                SqlQury = "SELECT [sSettingsName],ISNULL([sSettingsValue],'False')as sSettingsValue FROM [dbo].[Settings] WHERE [sSettingsName] IN ('WELCHALLYNECGDEVICEKEY','USEWELCHALLYNECGDEVICE','ECGENABLED','ECGDEVICEKEY')";
                OdbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                ObjClsEncryption = new gloSecurity.ClsEncryption();
                OdbLayer.Connect(false);
                OdbLayer.Retrive_Query(SqlQury, out dtWelchyAllyn);
                OdbLayer.Disconnect();
                if (dtWelchyAllyn != null && dtWelchyAllyn.Rows.Count > 0)
                {
                    switch (DeviceType.Trim().ToUpper())
                    {
                        case "CARDIOSCINECEECGDEVICE":
                            {
                                bool.TryParse((from s in dtWelchyAllyn.AsEnumerable() where s.Field<string>("sSettingsName") == "ECGENABLED" select s.Field<string>("sSettingsValue")).FirstOrDefault(), out IsDeviceEnabled);
                                if (!IsDeviceEnabled)
                                {
                                    _Massage = "Device Is Disabled";
                                    return _Massage;
                                }
                                DeviceKey = ObjClsEncryption.DecryptFromBase64String((from s in dtWelchyAllyn.AsEnumerable() where s.Field<string>("sSettingsName") == "ECGDEVICEKEY" select s.Field<string>("sSettingsValue")).FirstOrDefault(), ConstEncryptDecryptKey);
                                if (ObjClsEncryption.EncryptToBase64String(String.Concat(AUSname.Trim().ToLower(), "gL0@PPs2k9!8605"), "87654321") != DeviceKey.Trim())
                                {
                                    _Massage = "Invalid Activation Key";
                                    return _Massage;
                                }
                                else
                                {
                                    _Massage = string.Empty;
                                }
                                break;
                            }
                        case "WELCHALLYNECGDEVICE":
                            {
                                bool.TryParse((from s in dtWelchyAllyn.AsEnumerable() where s.Field<string>("sSettingsName") == "USEWELCHALLYNECGDEVICE" select s.Field<string>("sSettingsValue")).FirstOrDefault(), out IsDeviceEnabled);
                                if (!IsDeviceEnabled)
                                {
                                    _Massage = "Device Is Disabled";
                                    return _Massage;
                                }
                                DeviceKey = ObjClsEncryption.DecryptFromBase64String((from s in dtWelchyAllyn.AsEnumerable() where s.Field<string>("sSettingsName") == "WELCHALLYNECGDEVICEKEY" select s.Field<string>("sSettingsValue")).FirstOrDefault(), ConstEncryptDecryptKey);
                                if (ObjClsEncryption.EncryptToBase64String(String.Concat(AUSname.Trim().ToLower(), "gL0@PPs2k9!8610"), "87654321") != DeviceKey.Trim())
                                {
                                    _Massage = "Invalid Activation Key";
                                    return _Massage;
                                }
                                else
                                {
                                    _Massage = string.Empty;
                                }
                                break;
                            }
                    }

                }
                else
                {
                    _Massage = "No Device Setting Found";
                    return _Massage;
                }
              
            }
            catch (Exception)
            {
                _Massage = "No Device Setting Found";
            }
            finally
            {
                SqlQury = string.Empty;
                if (dtWelchyAllyn != null)
                {
                    dtWelchyAllyn.Dispose();
                    dtWelchyAllyn = null;
                }
                if (ObjClsEncryption != null)
                {
                    ObjClsEncryption.Dispose();
                    ObjClsEncryption = null;
                }
                if (OdbLayer != null)
                {
                    OdbLayer.Dispose();
                    OdbLayer = null;
                }

            }

            return _Massage;
        }

        public void Device_Available(out bool IsCardioScinceECGDeviceEanbled, out bool ISWelchAllynECGDeviceEnabled, out bool ISMidmarkECGDeviceEnabled)
        {
            IsCardioScinceECGDeviceEanbled = false;
            ISWelchAllynECGDeviceEnabled = false;
            ISMidmarkECGDeviceEnabled = false;
            gloDatabaseLayer.DBLayer OdbLayer = null;
            string SqlQury = string.Empty;
            DataTable dtResult = null;
            try
            {
                SqlQury = "SELECT sSettingsName, ISNULL(sSettingsValue,'False') as sSettingsValue FROM dbo.Settings WHERE sSettingsName IN ('ECGENABLED','USEWELCHALLYNECGDEVICE','USEMIDMARKECGDEVICE')";
                OdbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                OdbLayer.Connect(false);
                OdbLayer.Retrive_Query(SqlQury, out dtResult);
                OdbLayer.Disconnect();
                if (dtResult != null || dtResult.Rows.Count > 0)
                {
                    bool.TryParse(Convert.ToString((from s in dtResult.AsEnumerable() where s.Field<string>("sSettingsName") == "ECGENABLED" select s.Field<string>("sSettingsValue")).FirstOrDefault()), out IsCardioScinceECGDeviceEanbled);
                    bool.TryParse(Convert.ToString((from s in dtResult.AsEnumerable() where s.Field<string>("sSettingsName") == "USEWELCHALLYNECGDEVICE" select s.Field<string>("sSettingsValue")).FirstOrDefault()), out ISWelchAllynECGDeviceEnabled);
                    bool.TryParse(Convert.ToString((from s in dtResult.AsEnumerable() where s.Field<string>("sSettingsName") == "USEMIDMARKECGDEVICE" select s.Field<string>("sSettingsValue")).FirstOrDefault()), out ISMidmarkECGDeviceEnabled);
                }
                else
                {
                    IsCardioScinceECGDeviceEanbled = false;
                    ISWelchAllynECGDeviceEnabled = false;
                    ISMidmarkECGDeviceEnabled = false;
                }

            }
            catch (Exception)
            {
                IsCardioScinceECGDeviceEanbled = false;
                ISWelchAllynECGDeviceEnabled = false;
                ISMidmarkECGDeviceEnabled = false;
            }
            finally
            {
                SqlQury = string.Empty;
                if (dtResult != null)
                {
                    dtResult.Dispose();
                    dtResult = null;
                }
                if (OdbLayer != null)
                {
                    OdbLayer.Dispose();
                    OdbLayer = null;
                }

            }


        }

        public DataTable Retrive_ECGID()
        {
            DataTable _Retrive_ECGID = null;
            gloDatabaseLayer.DBLayer OdbLayer = null;
            string SqlQury = string.Empty;
            try
            {
                SqlQury = "SELECT [nECGID],[sTestId] FROM [dbo].[CV_ElectroCardioGrams] WHERE [nPatientID]=" + PatinetID + " AND [nClinicID]=" + ClinicID  + " AND [sDeviceType]='WelChAllyn'";
                OdbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                OdbLayer.Connect(false);
                OdbLayer.Retrive_Query(SqlQury, out _Retrive_ECGID);   
                OdbLayer.Disconnect(); 

            }
            catch (Exception)
            {
            }
            finally
            {
                if (OdbLayer != null)
                {
                    OdbLayer.Dispose();
                    OdbLayer = null;
                }
            }

            return _Retrive_ECGID;
        }

        public bool InsertExternalID(long PatientID,string PatientCode)
        {
            bool _InsertExternalID = false;
            gloDatabaseLayer.DBLayer OdbLayer = null;
            gloDatabaseLayer.DBParameters OdbParametors=null;
            string SqlQury = string.Empty;
            int Result = 0;
            try
            {
                OdbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                OdbParametors = new gloDatabaseLayer.DBParameters();
                OdbParametors.Add("@nPatientId", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                OdbParametors.Add("@sExternalType", "WelchAllyn ECG", ParameterDirection.Input, SqlDbType.VarChar);
                OdbParametors.Add("@sExternalSubType", "Patient Code", ParameterDirection.Input, SqlDbType.VarChar);
                OdbParametors.Add("@sExternalValue", PatientCode, ParameterDirection.Input, SqlDbType.VarChar);
                OdbParametors.Add("@sModuleName", "ECG", ParameterDirection.Input, SqlDbType.VarChar);
                OdbLayer.Connect(false);
                Result=OdbLayer.Execute("gsp_INUP_PatientExternalCodes", OdbParametors);
                OdbLayer.Disconnect();  
                if (Result == 1)
                {
                    _InsertExternalID = true;
                }
              
            }
            catch (Exception)
            {

            }
            finally
            {
                if (OdbLayer != null)
                {
                    OdbLayer.Dispose();
                    OdbLayer = null;
                }
                if (OdbParametors != null)
                {
                    OdbParametors.Dispose();
                    OdbParametors = null;
                }
            }

            return _InsertExternalID;


        }

        public String Retrive_ExternalPatientCode(long PatientID)
        {
            string ExternalPatientCode = string.Empty;
            gloDatabaseLayer.DBLayer OdbLayer = null;
            string SqlQury = string.Empty;
            try
            {
                OdbLayer = new gloDatabaseLayer.DBLayer(ConnectionString);
                SqlQury = "SELECT TOP 1 [sExternalValue]FROM [dbo].[PatientExternalCodes] where [nPatientId]= " + PatientID + " AND [sExternalType]='WelchAllyn ECG' AND [sModuleName]='ECG' AND sExternalSubType='Patient Code'";
                OdbLayer.Connect(false);
                ExternalPatientCode =Convert.ToString(OdbLayer.ExecuteScalar_Query(SqlQury));   
                OdbLayer.Disconnect();  
            }
            catch (Exception)
            {
                ExternalPatientCode = string.Empty;
            }
            finally
            {
                if (OdbLayer != null)
                {
                    OdbLayer.Dispose();
                    OdbLayer = null;
                }
            }
            return ExternalPatientCode; 
        }

        public long getVisitID(DateTime VisitDate, Int64 PatientID, string strConnectionstring)
        {
            object objResult = null;
            long VisitID = 0;
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(strConnectionstring);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Clear();
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtVisitdate", VisitDate, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@AppointmentID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                long Machinid = GetPrefixTransactionId(PatientID, strConnectionstring);
                oDBParameters.Add("@MachineID", Machinid, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@flag", 0, ParameterDirection.Output, SqlDbType.Int);
                oDBParameters.Add("@VisitID", 0, ParameterDirection.Output, SqlDbType.BigInt);
                oDBLayer.Connect(false);
                oDBLayer.Execute("gsp_INSERTVISITS", oDBParameters, out objResult);
                oDBLayer.Disconnect();
                long.TryParse(Convert.ToString(objResult), out VisitID);
            }
            catch (Exception ex)
            {
                VisitID = 0;
                MessageBox.Show(ex.Message.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroGeneralModule.getVisitID() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
                if (oDBParameters != null)
                { oDBParameters.Clear(); oDBParameters.Dispose(); oDBParameters = null; }
            }


            return VisitID;
        }

        public long GetPrefixTransactionId(long PatientId, String strConnectionString)
        {
            gloDatabaseLayer.DBLayer oDBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            System.Int64 _Result = 0;

            System.DateTime _PatientDOB = default(System.DateTime);
            System.DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");
            string strID1 = "";
            string strID2 = "";
            string strID3 = "";
            TimeSpan oTs = default(TimeSpan);
            object _internalresult = null;
            string _strSql = "";

            try
            {
                oDBLayer = new gloDatabaseLayer.DBLayer(strConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Clear();
                _strSql = "SELECT dtDOB FROM Patient WHERE nPatientID = " + PatientId;
                oDBLayer.Connect(false);
                _internalresult = oDBLayer.ExecuteScalar_Query(_strSql);
                if (_internalresult != null)
                {
                    if (string.IsNullOrEmpty(_internalresult.ToString()))
                    {
                        _PatientDOB = Convert.ToDateTime(_internalresult);
                    }
                }

                oTs = new TimeSpan();
                oTs = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTs.Days.ToString().Replace("-", "");

                oTs = new TimeSpan();
                oTs = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTs.TotalSeconds).ToString().Replace("-", "");

                oTs = new TimeSpan();
                oTs = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTs.Days.ToString().Replace("-", "");
                _Result = Convert.ToInt64(strID1) + Convert.ToInt64(strID2) + Convert.ToInt64(strID3);
                _Result = Convert.ToInt64(_Result);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Spirometry, gloAuditTrail.ActivityType.Add, " Error in clsSpiroGeneralModule.GetPrefixTransactionId() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }
                if (oDBParameters != null)
                { oDBParameters.Clear(); oDBParameters.Dispose(); oDBParameters = null; }
            }
            return _Result;
        }

    }

   public static class ClsWelchAllynECGLayerGenral
   {

       public static DataTable Retrive_ECGID(long ClinicID, long PatientID, string gloConnectionString)
       {
           DataTable _Retrive_ECGID = null;
           Classes.ClsWelchAllynECGLayer ObjWelchallynlayer = null;
           try
           {
               ObjWelchallynlayer = new Classes.ClsWelchAllynECGLayer(gloConnectionString);
               ObjWelchallynlayer.ClinicID = ClinicID;
               ObjWelchallynlayer.PatinetID = PatientID;
               _Retrive_ECGID = ObjWelchallynlayer.Retrive_ECGID();
           }
           catch (Exception)
           {
           }
           finally
           {
               if (ObjWelchallynlayer != null)
               {
                   ObjWelchallynlayer.Dispose();
                   ObjWelchallynlayer = null;
               }

           }

           return _Retrive_ECGID;
       }

       public static long Get_ECGID(DataTable dtECGID, string sTestID)
       {
           long _Get_ECGID = 0;
           string Result = string.Empty;
           try
           {
               if (dtECGID != null && dtECGID.Rows.Count >= 0)
               {
                   Result = (from s in dtECGID.AsEnumerable() where s.Field<string>("sTestId") == sTestID select s.Field<Decimal>("nECGID")).FirstOrDefault().ToString();
                   long.TryParse(Result, out _Get_ECGID);
               }
           }
           catch (Exception)
           {
               _Get_ECGID = 0;
           }
           finally
           {
               Result = string.Empty;
           }
           return _Get_ECGID;
       }

       public static CcBase.ITest Retrive_Test(string TestID, CcBase.ITests ALLTest)
       {
           CcBase.ITest _Retrive_Test = null;
           CcBase.IPersistent2 IPt = null;
           foreach (CcBase.ITest Test in ALLTest)
           {
               IPt = (CcBase.IPersistent2)Test;
               if (string.Compare(TestID, IPt.GetIdAsString(), true) == 0)
               {
                   _Retrive_Test = Test;
               }

           }


           return _Retrive_Test;
       }

       public static bool Validate_Device_Settings(ClsWelchAllynECGLayer.DeviceType _DeviceType, long nClinicID, long nLoginID, string gloEMRConnectionString, string GloEMRMessgaeboxCaption,  bool bIsCardioScinceDeviceEnabled , bool bIsWelchAllynEcgDeviceEnabled, bool bIsMidmarkECGDeviceEnabled)
       {
           bool IsValid = false;
           ClsWelchAllynECGLayer ObjClsWelchAllynECGLayer = null;
           //bool bIsCardioScinceDeviceEnabled = false;
           //bool bIsWelchAllynEcgDeviceEnabled = false;
           //bool bIsMidmarkECGDeviceEnabled = false;
           string AusName = string.Empty;
           string DeviceUserName = string.Empty;
           string DevicePassword = string.Empty;
          try
           {
               ObjClsWelchAllynECGLayer = new ClsWelchAllynECGLayer(gloEMRConnectionString);
               //ObjClsWelchAllynECGLayer.Device_Available(out bIsCardioScinceDeviceEnabled, out bIsWelchAllynEcgDeviceEnabled, out bIsMidmarkECGDeviceEnabled);
               switch (_DeviceType)
               {

                   case ClsWelchAllynECGLayer.DeviceType.HeartCentrixECGDevice:
                       {

                           if (!bIsCardioScinceDeviceEnabled)
                           {
                               MessageBox.Show("HeartCentrix ECG Device Interface is not activated", GloEMRMessgaeboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                               IsValid = false;
                               return IsValid;
                           }
                           AusName = ObjClsWelchAllynECGLayer.Get_AusName(nClinicID);
                           if (AusName.Length <= 0)
                           {
                               MessageBox.Show("AUS Settings is not present, Configure AUS settings at “gloEMR Admin >> Clinic>> License>> AUS User Name” to perform ECG test.", GloEMRMessgaeboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                               IsValid = false;
                               return IsValid;
                           }
                           if (!ObjClsWelchAllynECGLayer.Check_DeviceActivation(_DeviceType, AusName, nClinicID))
                           {
                               MessageBox.Show("Invalid HeartCentrix ECG Device interface activation key. Please check the activation key in gloEMR Admin>> Settings>> Interface Settings>> Device Interface Settings", GloEMRMessgaeboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                               IsValid = false;
                               return IsValid;
                           }

                           ObjClsWelchAllynECGLayer.Get_LoginDetails(nLoginID, _DeviceType, out DeviceUserName, out DevicePassword);
                         
                           if (DeviceUserName.Trim().Length <= 0)
                           {
                               MessageBox.Show("Your login account is not configured to access HeartCentrix ECG device interface.Configure user account in gloEMR Admin >> Device User Management", GloEMRMessgaeboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                               IsValid = false;
                               return IsValid;
                           }

                           IsValid = true;

                           break;
                       }
                   case ClsWelchAllynECGLayer.DeviceType.WelchAllynECGDevice:
                       {

                           if (!bIsWelchAllynEcgDeviceEnabled)
                           {
                               MessageBox.Show("WelchAllyn ECG Device Interface is not activated.", GloEMRMessgaeboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                               IsValid = false;
                               return IsValid;
                           }

                           AusName = ObjClsWelchAllynECGLayer.Get_AusName(nClinicID);
                           if (AusName.Length <= 0)
                           {
                               MessageBox.Show("AUS Settings is not present, Configure AUS settings at “gloEMR Admin >> Clinic>> License>> AUS User Name” to perform ECG test.", GloEMRMessgaeboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                               IsValid = false;
                               return IsValid;
                           }

                           if (!ObjClsWelchAllynECGLayer.Check_DeviceActivation(_DeviceType, AusName, nClinicID))
                           {
                               MessageBox.Show("Invalid WelchAllyn ECG Device interface activation key. Please check the activation key in gloEMR Admin>> Settings>> Interface Settings>> Device Interface Settings", GloEMRMessgaeboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                               IsValid = false;
                               return IsValid;
                           }

                           ObjClsWelchAllynECGLayer.Get_LoginDetails(nLoginID, _DeviceType, out DeviceUserName, out DevicePassword);

                           if (DeviceUserName.Trim().Length <= 0)
                           {
                               MessageBox.Show("Your login account is not configured to access WelchAllyn ECG device interface.Configure user account in gloEMR Admin >> Device User Management", GloEMRMessgaeboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                               IsValid = false;
                               return IsValid;
                           }

                           IsValid = true;
                         

                           break;
                       }
                   case ClsWelchAllynECGLayer.DeviceType.MidMarkECGDevice:
                       {

                           if (!bIsMidmarkECGDeviceEnabled)
                           {
                               MessageBox.Show("Midmark ECG Device Interface is not activated.", GloEMRMessgaeboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                               IsValid = false;
                               return IsValid;
                           }

                           AusName = ObjClsWelchAllynECGLayer.Get_AusName(nClinicID);

                           if (AusName.Length <= 0)
                           {
                               MessageBox.Show("AUS Settings is not present, Configure AUS settings at “gloEMR Admin >> Clinic>> License>> AUS User Name” to perform ECG test.", GloEMRMessgaeboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                               IsValid = false;
                               return IsValid;
                           }

                           if (!ObjClsWelchAllynECGLayer.Check_DeviceActivation(_DeviceType, AusName, nClinicID))
                           {
                               MessageBox.Show("Invalid Midmark ECG Device interface activation key. Please check the activation key in gloEMR Admin>> Settings>> Interface Settings>> Device Interface Settings", GloEMRMessgaeboxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                               IsValid = false;
                               return IsValid;
                           }
                           else
                           {
                               IsValid = true;
                           }

                           break;
                       }
               }

           }
           catch (Exception)
           {
               IsValid = false;
               return IsValid;
           }
           finally
           {
               if (ObjClsWelchAllynECGLayer != null)
               {
                   ObjClsWelchAllynECGLayer.Dispose();
                   ObjClsWelchAllynECGLayer = null;
               }
           }
        
           return IsValid;           
       }
       
     

   }

   #region Remote WelchAllyn ECG

   public class WelchAllynECGRequest
   {
       public Int64 PatientID { get; set; }
       public string PatientCode { get; set; }
       public string LastName { get; set; }
       public string MiddleName { get; set; }
       public string FirstName { get; set; }
       public DateTime BirthDate { get; set; }
       public string Gender { get; set; }
       public string DeviceUser { get; set; }
       public string DevicePassword { get; set; }
       public gloEmdeonInterface.Forms.FrmWelChallynECG.TestType TestType { get; set; }
       public List<PatientECGDetails> PatientECGCollection { get; set; }

       public WelchAllynECGRequest()
       {
           PatientECGCollection = new List<PatientECGDetails>();
       }
   }

   public class PatientECGDetails
   {
       public Int64 ECGID { get; set; }
       public string TestId { get; set; }
   }

   public class WelchAllynECGResponse
   {
       public Int64 PatientID { get; set; }
       public gloEmdeonInterface.Forms.FrmWelChallynECG.TestType _TestType { get; set; }
       public List<WelchAllynECGDetails> WelchAllynECGCollection { get; set; }

       public WelchAllynECGResponse()
       {
           WelchAllynECGCollection = new List<WelchAllynECGDetails>();
       }
   }

   public class WelchAllynECGDetails
   {
       public string TestId { get; set; }
       public string OrderDate { get; set; }
       public string PR { get; set; }
       public string QT { get; set; }
       public string QTC { get; set; }
       public string QRSDuration { get; set; }
       public string PAxis { get; set; }
       public string QRSAxis { get; set; }
       public string TAxis { get; set; }
       public string ECGInterpretation { get; set; }
       public string ECGDocumentName { get; set; }
   }

   #endregion
}
