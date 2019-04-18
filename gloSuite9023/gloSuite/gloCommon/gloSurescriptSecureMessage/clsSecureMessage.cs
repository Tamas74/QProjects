using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.ServiceModel;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using gloGlobal;

namespace gloSurescriptSecureMessage
{

    #region "Enum"

    public enum FileExtension
    {
        pdf = 0,
        docx = 1,
        zip = 2,
        xml = 3,
        txt=4,
        rtf=5,
        html=6,
        htm=7
    }


    public enum ModuleName
    {
        None = 0,
        Exam = 1,
        Dms= 2
    }

    
    public enum RequestFrom
    {
        Inbox = 0,
        UnReadMail = 1,
        SentItems = 2,
        DeletedItems = 3,
        OutBox = 4,
        InBoxCount = 5,
         PatientSavingsInbox = 6,
        PatientSavingsSentItems = 7,
        PatientSavingsDeletedItems = 8,
        PatientSavingsOutbox = 9
    }

    public enum MessageType
    {
        None = 0,
        Send = 1,
        OutBox = 2,
        Error = 3,
        Status = 4,
        Verify = 5
    }

    public enum MessageStatus
    { 
        None = 0,
        Send = 1,
        Receive = 2
    }

    public enum InboxSearchType
    {
        General = 0,
        From = 1,
        Subject = 2,
        Received=3
    }


    #endregion

    #region "Classes"

    public class SecureMessage:IDisposable
    {    

        #region "Constructor & Destructor"
            

        public SecureMessage()
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

        ~SecureMessage()
        {

            Dispose(false);
        }

        #endregion

        #region Properties for SecureMessage Object
               
        //nSecureMessageInboxID
        private Int64 _secureMessageInboxID = 0;

        public Int64 secureMessageInboxID
        {
            get { return _secureMessageInboxID; }
            set { _secureMessageInboxID = value; }
        }        

        //Message ID
        private string _messageID = "";
        public string  messageID
        {
            get { return _messageID; }
            set { _messageID = value; }
        }
        
        //Relates To Message ID
        private string _relateMessageID = "";
        public string relateMessageID
        {
            get { return _relateMessageID; }
            set { _relateMessageID = value; }
        }

        //Version No
        private string _version = "";
        public string version
        {
            get { return _version; }
            set { _version = value; }
        }

        //Release No
        private string _release = "";
        public string release
        {
            get { return _release; }
            set { _release = value; }
        }


        //Highest Version No
        private string _highVersion = "";
        public string highVersion
        {
            get { return _highVersion; }
            set { _highVersion = value; }
        }

        //SenderID
        private Int64 _senderId = 0;
        public Int64 senderID
        {
            get { return _senderId; }
            set { _senderId = value; }
        }

        //ReceiverID
        private Int64 _receiverId = 0;
        public Int64 receiverID
        {
            get { return _receiverId; }
            set { _receiverId = value; }
        }

        //From
        private string _from = "";
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        //FromQualifier
        private string _fromQualifier = "";
        public string FromQualifier
        {
            get { return _fromQualifier; }
            set { _fromQualifier = value; }
        }

        //To
        private string _to = "";
        public string To
        {
            get { return _to; }
            set { _to = value; }
        }

        //ToQualifier
        private string _toQualifier = "";
        public string ToQualifier
        {
            get { return _toQualifier; }
            set { _toQualifier = value; }
        }

        //Subject
        private string _subject = "";
        public string subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        //MessageBody
        private string _messageBody = "";
        public string messageBody
        {
            get { return _messageBody; }
            set { _messageBody = value; }
        }

        //DateTimeUTC
        private string _dateTimeUTC = "";
        public string dateTimeUTC
        {
            get { return _dateTimeUTC; }
            set { _dateTimeUTC = value; }
        }


        //DateTimeUTC
        private DateTime _dateUTC;
        public DateTime dateUTC
        {
            get { return _dateUTC; }
            set { _dateUTC = value; }
        }

        //DateTimeNormal
        public DateTime dateTimeNormal { get; set; }

        //IsUnRead
        private Int16 _isRead = 0;
        public Int16 isRead
        {
            get { return _isRead; }
            set { _isRead = value; }
        }

        //PatientID
        private Int64 _patientId = 0;
        public Int64 patientID
        {
            get { return _patientId; }
            set { _patientId = value; }
        }


        //PatientDemographics
        private string _firstName = "";
        public string firstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName = "";
        public string lastName 
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private string _Dob = "";
        public string Dob
        {
            get { return _Dob; }
            set { _Dob = value; }
        }

        private string _gender = "";
        public string gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        private string _clinicCode = "";
        public string clinicCode
        {
            get { return _clinicCode; }
            set { _clinicCode = value; }
        }

        private string _patientCode = "";
        public string patientCode
        {
            get { return _patientCode; }
            set { _patientCode = value; }
        }

        private string _zip = "";
        public string zip
        {
            get { return _zip; }
            set { _zip = value; }
        }

        //No ofAttachments
        private Int16 _noOfAttachements = 0;
        public Int16 noofAttachements
        {
            get { return _noOfAttachements; }
            set { _noOfAttachements = value; }
        }

        //Message Status
        private Int16 _messageStatus = 0;
        public Int16 MessageStatus
        {
            get { return _messageStatus; }
            set { _messageStatus = value; }
        }

        //Message Type
        private Int16 _messageType = 0;
        public Int16 messageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }

        //Associated
        private Int16 _associated = 0;
        public Int16 associated
        {
            get { return _associated; }
            set { _associated = value; }
        }

        //Delivery status code
        private string _deliveryStatusCode = "";
        public string deliveryStatusCode
        {
            get { return _deliveryStatusCode; }
            set { _deliveryStatusCode = value; }
        }

        //Delivery status code
        private string _deliveryStatusDescription = "";
        public string deliveryStatusDescription
        {
            get { return _deliveryStatusDescription; }
            set { _deliveryStatusDescription = value; }
        }

        //SoftWare Version
        private string _softwareVersion = "";
        public string softwareVersion
        {
            get { return _softwareVersion; }
            set { _softwareVersion = value; }
        }

        //SoftWare Product
        private string _softwareProduct = "";
        public string softwareProduct
        {
            get { return _softwareProduct; }
            set { _softwareProduct = value; }
        }

        //Company Name
        private string _companyName = "";
        public string companyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        //User Name
        private string _userName = "";
        public string userName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        //Machine Name
        private string _machineName = "";
        public string machineName
        {
            get { return _machineName; }
            set { _machineName = value; }
        }

        //Deleted
        private Int16 _deleted = 0;
        public Int16 deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

        //No ofRAttachments
        private Int16 _noOfRAttachements = 0;
        public Int16 noOfRAttachements
        {
            get { return _noOfRAttachements; }
            set { _noOfRAttachements = value; }
        }

        public Int64 DocumentReferenceID
        {
            get;
            set;
        }

       //Module Name
        public string ModuleName 
        { get; 
          set; 
        }    


        //Use Case
        private Int16 _useCase = 0;
        public Int16 UseCase
        {
            get { return _useCase; }
            set { _useCase = value; }
        }

        private string sDelegatedUser = "";
        public string DelegatedUser
        {
            get { return sDelegatedUser; }
            set { sDelegatedUser = value; }
        }

        #endregion

        #region "Public and Private Methods"

        public static  gloSurescriptSecureMessage.gloDirectservice.IgloDirectClient GetSecureMsgFSvc(string siteUrl)
        {
            gloSurescriptSecureMessage.gloDirectservice.IgloDirectClient client = null;
            try
            {
                Uri serviceUri = new Uri(siteUrl);
                System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(serviceUri);
                //Create the binding here
                WSHttpBinding binding = CreateInstance();
                client = new gloSurescriptSecureMessage.gloDirectservice.IgloDirectClient(binding, endpointAddress);
                return client;
            }
            catch //(Exception ex)
            {
                
                return client;
            }
        }

        public static  WSHttpBinding CreateInstance()
        {
            WSHttpBinding binding = new WSHttpBinding();
            try
            {
                binding.Security.Mode = SecurityMode.Transport;
                binding.ReliableSession.Enabled = false;
                binding.ReliableSession.InactivityTimeout = new TimeSpan(4, 20, 0);
               
               
                binding.OpenTimeout = new TimeSpan(4, 20, 0);
                binding.CloseTimeout = new TimeSpan(4, 20, 0);
                binding.SendTimeout = new TimeSpan(4, 20, 10);
                binding.ReceiveTimeout = new TimeSpan(4, 20, 0);
                binding.MaxBufferPoolSize = 99999999999999;
                binding.MaxReceivedMessageSize = 2147483647;
               

                binding.ReaderQuotas.MaxArrayLength = 2147483647;
                binding.ReaderQuotas.MaxDepth = 64;
                binding.ReaderQuotas.MaxStringContentLength = 2147483647;
                binding.ReaderQuotas.MaxBytesPerRead = 2147483647;
                binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
               
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                binding.UseDefaultWebProxy = true;
                return binding;
            }
            catch //(Exception ex)
            {
               
                return binding;
            }
            finally
            {
                //if ((binding != null))
                //{
                //    binding = null;
                //}
            }
        }

        public static String GetFileName(String strAppPath)
        {
            try
            {
                //string _NewDocumentName = "";
                //string _Extension = ".xml";
                //DateTime _dtCurrentDateTime = System.DateTime.Now;
                //int i = 0;
                //_NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") + _Extension;
                //while (File.Exists(Convert.ToString(strAppPath) + "\\" + _NewDocumentName) == true)
                //{
                //    i = i + 1;
                //    _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") + "-" + i + _Extension;

                //}
                
                //return Convert.ToString(strAppPath) + _NewDocumentName;
                return gloGlobal.clsFileExtensions.NewDocumentName(strAppPath, ".xml", "MMddyyyyHHmmssffff");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return "";
            }
            finally
            {

            }
        }
        
        public static byte[] GenerateXML(SecureMessage objSecureMessage, List<Attachment> oLsAttach)
        {
            N2NMessageType NewN2NMessage = null;
            byte[] byteArray = null;

            try
            {
                NewN2NMessage = CreateN2NMessage(objSecureMessage, oLsAttach);
                byteArray = GenerateXML(NewN2NMessage);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (NewN2NMessage != null)
                {
                    NewN2NMessage = null;
                }
            }

            return byteArray;

        }

        public static N2NMessageType CreateN2NMessage(SecureMessage objSecureMessage, List<Attachment> oLsAttach)
        {

            N2NMessageType _messageContent = null;
            N2NHeaderType _messageHeader = null;
            N2NBodyType _messageBody = null;
            N2NAddressType _messageTo = null;
            N2NAddressType _messageFrom = null;
            SenderSoftwareType _senderDetails = null;
            ClinicalMessageType _messageClinical = null;
            DocumentType _documentType = null;
            AttachmentType[] _attachmentType = null;
            AttachmentType _attachment = null;
            FileType _file = null;
            DocumentDataItemType[] _DocumentData = null;
            DocumentDataItemType _document = null;
            DocumentDataItemType _docPatientContextVersion = null;
            try
            {
                _messageContent = new N2NMessageType();
                _messageHeader = new N2NHeaderType();
                _messageBody = new N2NBodyType();
                _messageTo = new N2NAddressType();
                _messageFrom = new N2NAddressType();
                _senderDetails = new SenderSoftwareType();
                _messageClinical = new ClinicalMessageType();
                _documentType = new DocumentType();
                _attachmentType = new AttachmentType[1];
                _attachment = new AttachmentType();
                _file = new FileType();
                _DocumentData = new DocumentDataItemType[3];

               
                DateTime dtdate = System.DateTime.UtcNow;
                string strdate = dtdate.ToString("yyyy-MM-dd");
                string strtime = dtdate.ToString("hh:mm:ss");
                DateTime utc = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, DateTimeKind.Utc);
                string strUTCFormat = dtdate.Year.ToString() + "-" + "0" + dtdate.Month.ToString() + "-" + dtdate.Day.ToString() + "T" + dtdate.Hour.ToString() + ":" + dtdate.Minute.ToString() + ":" + dtdate.Second.ToString() + ".0Z";
                strUTCFormat = strdate + "T" + strtime + ":0Z";

                _messageContent.release = "006";
                _messageContent.version = "010";

                _messageTo.Qualifier = "DIRECT";
                _messageTo.Value = objSecureMessage.To;

                _messageFrom.Qualifier = "DIRECT";
                _messageFrom.Value = objSecureMessage.From;

                //Header
                _messageHeader.To = _messageTo;
                _messageHeader.From = _messageFrom;
                _messageHeader.MessageID = objSecureMessage.messageID;
                _messageHeader.RelatesToMessageID = "";
                _messageHeader.SentTime = utc;
                _messageHeader.TestMessage = "1";

                    // Added Use Case in Header
                if (objSecureMessage.UseCase == 1)
                {
                    _messageHeader.UseCase = "ADVSAVE";
                    _messageHeader.RelatesToMessageID = objSecureMessage.relateMessageID;
                }

                //Sender Software Developer
                _senderDetails.SenderSoftwareDeveloper = objSecureMessage.companyName;
                _senderDetails.SenderSoftwareProduct = objSecureMessage.softwareProduct;
                _senderDetails.SenderSoftwareVersionRelease = objSecureMessage.softwareVersion;
                _messageHeader.SenderSoftware = _senderDetails;
                _messageContent.Header = _messageHeader;

                //Document Type
                _documentType.PlainText = objSecureMessage.messageBody;

                

                


                // Attachments
                if (oLsAttach != null)
                {
                    if (oLsAttach.Count > 0)
                    {

                        _attachmentType = new AttachmentType[oLsAttach.Count];
                        for (int i = 0; i <= oLsAttach.Count - 1; i++)
                        {
                            _attachment = new AttachmentType();
                            _file = new FileType();

                            //File Type
                            _file.DocumentData = oLsAttach[i].base64;
                            _file.DocumentType = oLsAttach[i].mimeType;

                            //Attachment
                            _attachment.DocumentName = oLsAttach[i].documentName;
                            _attachment.Item = _file;
                            _attachmentType[i] = _attachment;
                        }

                        _messageClinical.Attachment = _attachmentType;
                    }
                }

                //clinical message
                _messageClinical.Document = _documentType;
                _messageClinical.Subject = objSecureMessage.subject;

                //Document Data
                if (objSecureMessage.patientID > 0)
                {                    
                    string sAUSGUID = null;
                    if (objSecureMessage.clinicCode != null)
                    {
                        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                        {
                            sAUSGUID = new Guid(md5.ComputeHash(Encoding.Default.GetBytes(objSecureMessage.clinicCode))).ToString();
                        }

                        _docPatientContextVersion = new DocumentDataItemType();
                        _docPatientContextVersion.key = "X-PatientContextVersion";
                        _docPatientContextVersion.Value = "1.0";
                        _DocumentData[0] = _docPatientContextVersion;

                        _document = new DocumentDataItemType();
                        _document.key = "X-PatientDemographics";
                        _document.Value = "given=" + objSecureMessage.firstName + "; family=" + objSecureMessage.lastName + "; dob=" + objSecureMessage.Dob + "; gender=" + objSecureMessage.gender + "; zip=" + objSecureMessage.zip + "";
                        _DocumentData[1] = _document;
                        _document = null;

                        _document = new DocumentDataItemType();
                        _document.key = "X-PatientId";
                        _document.Value = objSecureMessage.patientCode + "; issuer=" + sAUSGUID;
                        _DocumentData[2] = _document;
                        _document = null;

                        _messageClinical.DocumentData = _DocumentData;
                    }
                }
                
                //Body
                _messageBody.Item = _messageClinical;
                _messageContent.Body = _messageBody;

                return _messageContent;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return _messageContent;
            }
            finally
            {

                if (_file != null)
                {
                    _file = null;
                }

                if (_attachment != null)
                {
                    _attachment = null;
                }

                if (_attachmentType != null)
                {
                    _attachmentType = null;
                }

                if (_documentType != null)
                {
                    _documentType = null;
                }

                if (_messageClinical != null)
                {
                    _messageClinical = null;
                }

                if (_senderDetails != null)
                {
                    _senderDetails = null;
                }

                if (_messageFrom != null)
                {
                    _messageFrom = null;
                }

                if (_messageTo != null)
                {
                    _messageTo = null;
                }

                if (_messageBody != null)
                {
                    _messageBody = null;
                }

                if (_messageHeader != null)
                {
                    _messageHeader = null;
                }

                if (_messageContent != null)
                {
                    _messageContent = null;
                }

                if (_document != null)
                {
                    _document = null;
                }

                if (_DocumentData != null)
                {
                    _DocumentData = null;
                }
                if (_docPatientContextVersion != null)
                {
                    _docPatientContextVersion = null;
                }

            }


        }

        private static byte[] GenerateXML(N2NMessageType message)
        {
            byte[] byteArray = null;
            XmlSerializer xs = null;
            FileStream fs = null;

            try
            {
                string strFileName = GetFileName(gloSettings.FolderSettings.AppTempFolderPath);
                xs = new XmlSerializer(typeof(N2NMessageType));
                fs = new FileStream(strFileName, FileMode.Create);
                xs.Serialize(fs, message);
                fs.Close();
                byteArray = System.IO.File.ReadAllBytes(strFileName);

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (fs != null)
                {
                    fs = null;
                }
                if (xs != null)
                {
                    xs = null;
                }
            }

            return byteArray;



        }
        
        public static String ConvertBinarytoFile(byte[] cntFromDB, String strFileName)
        {
            try
            {

                if (cntFromDB != null)
                {
                   // MemoryStream stream = new MemoryStream(cntFromDB);
                    FileStream oFile = new FileStream(strFileName, System.IO.FileMode.Create);
                    oFile.Write(cntFromDB, 0, cntFromDB.Length);
                    //stream.WriteTo(oFile);
                    oFile.Close();
                    oFile.Dispose();
                    oFile = null;
                }
                else
                {
                    return "";
                }
                //clsException.UpdateLog("End  ConvertBinarytoFile", LogFilePath, EnableLog);
            }
            catch //(Exception ex)
            {

            }
            return strFileName;
        }

        public static SecureMessage ExtractXML(N2NMessageType objN2N, SecureMessage objMessage)
        {
            objMessage.release = objN2N.release;
            objMessage.version = objN2N.version;

            objMessage.ToQualifier = objN2N.Header.To.Qualifier;
            objMessage.To = objN2N.Header.To.Value;

            objMessage.FromQualifier = objN2N.Header.From.Qualifier;
            objMessage.From = objN2N.Header.From.Value;

            // objMessage.messageID = objN2N.Header.MessageID;
            objMessage.relateMessageID = objN2N.Header.MessageID;
            objMessage.dateUTC = objN2N.Header.SentTime;

            objMessage.companyName = objN2N.Header.SenderSoftware.SenderSoftwareDeveloper;
            objMessage.softwareProduct = objN2N.Header.SenderSoftware.SenderSoftwareProduct;
            objMessage.softwareVersion = objN2N.Header.SenderSoftware.SenderSoftwareVersionRelease;

            if (Convert.ToString(objN2N.Body.Item) == "Error")
            {
                Error _messagestatus = null;
                _messagestatus = (Error)(objN2N.Body.Item);

                if (_messagestatus.Code != null)
                {
                    objMessage.deliveryStatusCode = _messagestatus.Code;
                }
                else
                {
                    objMessage.deliveryStatusCode = "";
                }


                if (_messagestatus.Description != null)
                {
                    objMessage.deliveryStatusDescription = _messagestatus.Description;
                }
                else
                {
                    objMessage.deliveryStatusDescription = "";
                }

            }
            else
            {
                Status _messagestatus = null;
                _messagestatus = (Status)(objN2N.Body.Item);


                if (_messagestatus.Code != null)
                {
                    objMessage.deliveryStatusCode = _messagestatus.Code;
                }
                else
                {
                    objMessage.deliveryStatusCode = "";
                }


                if (_messagestatus.Description != null)
                {
                    objMessage.deliveryStatusDescription = _messagestatus.Description;
                }
                else
                {
                    objMessage.deliveryStatusDescription = "";
                }


            }
            return objMessage;
        }

        public void PrintReport(string sSecureMessageInboxID, bool _isShowPrintDialog=true)
        {
            gloSSRSApplication.clsPrintReport clsPrntRpt = null;
            string _MessageBoxCaption = string.Empty;
            string _databaseConnectionString = string.Empty;
            string _LoginName = string.Empty;
            string gstrSQLServerName = string.Empty;
            string gstrDatabaseName = string.Empty;
            bool gblnSQLAuthentication = false;
            string gstrSQLUserEMR = string.Empty;
            string gstrSQLPasswordEMR = string.Empty;
            bool gblnDefaultPrinter = false;
            SqlConnection Con = null;
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            //bool _showprintdialog = false;

            try
            {
                if (appSettings["DataBaseConnectionString"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["DataBaseConnectionString"]))
                    {
                        _databaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
                        Con = new SqlConnection(_databaseConnectionString);
                    }
                }

                if (appSettings["UserName"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["UserName"]))
                    {
                        _LoginName = Convert.ToString(appSettings["UserName"]);
                    }
                }

                if (appSettings["SQLServerName"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["SQLServerName"]))
                    {
                        gstrSQLServerName = Convert.ToString(appSettings["SQLServerName"]);
                    }
                }

                if (appSettings["DatabaseName"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["DatabaseName"]))
                    {
                        gstrDatabaseName = Convert.ToString(appSettings["DatabaseName"]);
                    }
                }

                if (appSettings["SQLLoginName"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["SQLLoginName"]))
                    {
                        gstrSQLUserEMR = Convert.ToString(appSettings["SQLLoginName"]);
                    }
                }

                if (appSettings["SQLPassword"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["SQLPassword"]))
                    {
                        gstrSQLPasswordEMR = Convert.ToString(appSettings["SQLPassword"]);
                    }
                }
                
                    if (appSettings["DefaultPrinter"] != null)
                    {
                        if (!string.IsNullOrEmpty(appSettings["DefaultPrinter"]))
                        {
                            gblnDefaultPrinter = !Convert.ToBoolean(appSettings["DefaultPrinter"]);
                        }
                    }
               
                if (appSettings["WindowAuthentication"] != null)
                {
                    if (!string.IsNullOrEmpty(appSettings["WindowAuthentication"]))
                    {
                        gblnSQLAuthentication = !Convert.ToBoolean(appSettings["WindowAuthentication"]);
                    }
                }


                string ParameterName = null;
                ParameterName = "nSecureMessageID";

                string ParameterValue = null;
                ParameterValue = sSecureMessageInboxID;

                clsPrntRpt = new gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR);
                if (_isShowPrintDialog == false)
                {
                    clsPrntRpt.PrintReport("rptSureScriptsSelectedMessage", ParameterName, ParameterValue, _isShowPrintDialog , "");
                }
                else
                {
                    clsPrntRpt.PrintReport("rptSureScriptsSelectedMessage", ParameterName, ParameterValue, gblnDefaultPrinter, "");
                }
                

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (Con != null)
                {
                    Con.Dispose();

                }
                if (clsPrntRpt != null)
                {
                    clsPrntRpt.Dispose();
                    clsPrntRpt = null;
                }
            }
        }

        public static String ValidateZipCode(Int64 nPatientID)
        {
            clsSecureMessageDB clsDb = null;
            DataTable dtPatientDtls = null;
            string _ErrorMessage = "";
            try
            {
                clsDb = new clsSecureMessageDB();
                dtPatientDtls = clsDb.GetPatientDetailsforSecureMessage(nPatientID);
                if (dtPatientDtls != null)
                {
                    if (dtPatientDtls.Rows.Count > 0)
                    {
                        string sZipCode = Convert.ToString(dtPatientDtls.Rows[0]["sZIP"]).Trim();
                        if (sZipCode.Length == 0)
                        {
                            _ErrorMessage = "Patient is missing zip code."+System.Environment.NewLine+"Provider DIRECT Messages require patient to have a Zip Code.";
                        }
                        else if (sZipCode.Length < 5)
                        {
                            _ErrorMessage = "Make sure the zip code is at least 5 digits.";
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (clsDb != null)
                {
                    clsDb.Dispose();
                }
                if (dtPatientDtls != null)
                {
                    dtPatientDtls.Dispose();
                }
            }
            return _ErrorMessage;
        }

        public static Boolean bIsAccess(string sCDAConfidentiality)
        {
            Boolean bIsAccess = false;
            int nuserid = 0;
            string strQuery = "";
            gloDatabaseLayer.DBLayer oDB = null;

            try
            {

                if (sCDAConfidentiality.Trim().ToUpper() == "R")
                {

                    strQuery = "select isnull(count(nUserID),0) from UserRights_DTL  where nUserID =" + SecureMessageProperties.UserID + " and nrightsid=(select nrightsid from Rights_MST where sRightsName ='Import Restricted CCDA')";
                    oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                    oDB.Connect(false);
                    nuserid = (int)oDB.ExecuteScalar_Query(strQuery);

                    if (nuserid > 0)
                    {
                        bIsAccess = true;
                    }
                    
                }
                else
                {
                    bIsAccess= true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog("In function - bIsAccess: "+ex.ToString(), true);
            }
            finally
            {
                oDB = null;
                nuserid = 0;
            }
            
            return bIsAccess;

            //
        }
        #region "Set Preferred Provider from outside of Secure Messaging module"
        public static void SetPreferredProvider(long ProviderID)
        {

            if (SecureMessageProperties.ListUserProviderAssociation != null)
            {
                if (SecureMessageProperties.ListUserProviderAssociation.Any(p => p.ProviderID == ProviderID && !p.IsProviderInbox))
                {                   
                    if (SecureMessageProperties.ListUserProviderAssociation.Any(p => p.ProviderID == ProviderID && !p.IsPreferred))
                    {
                        if (SecureMessageProperties.ListUserProviderAssociation.Any(p => p.IsPreferred))
                        { SecureMessageProperties.ListUserProviderAssociation.First(p => p.IsPreferred).IsPreferred = false; }

                        SecureMessageProperties.ListUserProviderAssociation.First(p => p.ProviderID == ProviderID).IsPreferred = true;
                    }
                                        
                }
                else
                {
                    if (SecureMessageProperties.ListUserProviderAssociation.Any(p => p.IsProviderInbox))
                    {
                        if (SecureMessageProperties.ListUserProviderAssociation.Any(p => p.IsPreferred && !p.IsProviderInbox))
                        { SecureMessageProperties.ListUserProviderAssociation.First(p => p.IsPreferred).IsPreferred = false; }
                    }
                }
            }


            
            
        }
		#endregion

        #endregion
    }

    public class Attachment : IDisposable
    {
        #region "Constructor & Destructor"

        public Attachment()
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

        ~Attachment()
        {

            Dispose(false);
        }

        #endregion

        #region Properties for Attachment Object

        //nID
        private Int64 _nSecureMessageInboxID = 0;
        public Int64 nSecureMessageInboxID
        {
            get { return _nSecureMessageInboxID; }
            set { _nSecureMessageInboxID = value; }
        }

        //Message ID
        private Int64 _attachmentID = 0;
        public Int64 attachmentID
        {
            get { return _attachmentID; }
            set { _attachmentID = value; }
        }

        //ModuleName
        private Int16 _moduleName = 0;
        public Int16 moduleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }

        //FileExtension
        private Int16 _fileExtension = 0;
        public Int16 fileExtension
        {
            get { return _fileExtension; }
            set { _fileExtension = value; }
        }

        //DocumentName
        private string _documentName = "";
        public string documentName
        {
            get { return _documentName; }
            set { _documentName = value; }
        }

        //Content
        private byte[] _iContent = null;
        public byte[] iContent
        {
            get { return _iContent; }
            set { _iContent = value; }
        }

        //Base64String
        private string _base64 = "";
        public string base64
        {
            get { return _base64; }
            set { _base64 = value; }
        }

        //MineType
        private string _mimeType = "";
        public string mimeType
        {
            get { return _mimeType; }
            set { _mimeType = value; }
        }


        #endregion

        public string GenerateFile(byte[] oResult, string strFileName)
        {
           // MemoryStream stream = null;
            System.IO.FileStream oFile = null;

            try
            {
                if (oResult != null)
                {
                    byte[] content = (byte[])(oResult);
                   // stream = new MemoryStream(content);
                    strFileName = gloSettings.FolderSettings.AppTempFolderPath + strFileName;
                    oFile = new System.IO.FileStream(strFileName, System.IO.FileMode.Create);
                    oFile.Write(content, 0, content.Length);
                    //stream.WriteTo(oFile);
                    oFile.Close();
                    content = null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exGenerateFile)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exGenerateFile.ToString(), true);
            }
            finally
            {
                if (oFile != null)
                {
                    oFile.Dispose();
                    oFile = null;
                }

                //if (stream != null)
                //{
                //    stream.Dispose();
                //    stream = null;
                //}
            }
            return strFileName;
        }

    }

    public static class SecureMessageProperties
    {
        #region " Property Procedures "

        //Added by Ashish for User Provider Direct Association
        #region "Properties for User Provider Direct Association"
        
        public static List<DirectUserProviderAssociation> ListUserProviderAssociation
        { get; set; }

        //public static Boolean IsProviderDelegated
        //{ get; set; }

        public static DirectUserProviderAssociation DelegatedProvider
        { get; set; }

        #endregion

        public static string CCDAFilePath { get; set; }

        public static string DatabaseConnectionString
        {
            get;
            set;
        }

        public static Boolean bEnablePatientSavingsInbox
        {
            get;
            set;
        }
        
        public static Boolean bIsPatientSavingEnabled
        {
            get;
            set;
        }

        public static Boolean HasPatientSavingRights
        {
            get;
            set;
        }

        public static Boolean DisplayPatientSavingsInbox
        { get; set; }
                
        public static string ApplicationVersion
        {
            get;
            set;
        }

        public static Int64 ClinicID
        {
            get;
            set;
        }

        public static Int64 UserID
        {
            get;
            set;
        }

        public static string UserName
        {
            get;
            set;
        }

        public static string MessageBoxCaption
        {
            get;
            set;
        }

        public static Int64 LoginProviderID
        {
            get;
            set;
        }

        public static Boolean IsAccountsOn
        {
            get;
            set;
        }
        //Added for Auto Deleting CCDA files
        public static Boolean IsAutoDeleteCCDA
        {
            get;
            set;
        }
        public static long PatientID
        {
            get;
            set;

        }

        public static long ProviderID
        {
            get;
            set;
        }

        public static string ProviderDirectAddress
        {
            get;
            set;

        }

        public static string MachineName
        {
            get;
            set;

        }

        public static bool IsStagingServerEnable
        {
            get;
            set;
        }

        public static string StagingServerUrl
        {
            get;
            set;
        }

        public static string ProductionServerUrl
        {
            get;
            set;
        }

        public static string ProviderName
        {
            get;
            set;

        }

        public static object objPatientExam
        {
            get;
            set;

        }

        public static object objPatientLetters
        {
            get;
            set;

        }

        public static object objPatientMessages
        {
            get;
            set;

        }

        public static object objNurseNotes
        {
            get;
            set;

        }

        public static object objHistory
        {
            get;
            set;

        }

        public static object objLabs
        {
            get;
            set;

        }

        public static object objDMS
        {
            get;
            set;

        }

        public static object objRxmed
        {
            get;
            set;

        }
        
        public static object objOrders
        {
            get;
            set;

        }

        public static object objProblemList
        {
            get;
            set;

        }

        public static object objCriteria
        {
            get;
            set;

        }

        public static object objWord
        {
            get;
            set;

        }

        public static string SPID
        {
            get;
            set;
        }

        public static string SiteID
        {
            get;
            set;
        }

        public static string AUSID
        {
            get;
            set;
        }

        public static string Location
        {
            get;
            set;
        }

        public static string ClinicName
        {
            get;
            set;
        }

        public static Hashtable OpenedFormsCollection
        {
            get;
            set;
        }
        #endregion " Property Procedures "
    }

    public class DirectUserProviderAssociation:IDisposable

    {
        #region "Properties"        
        public Int64 AssociationID
        { get; set; }

        public Int64 UserID
        { get; set; }

        public Int64 ProviderID
        { get; set; }

        public string Name
        { get; set; }

        public string FirstName
        { get; set; }
   
        public string LastName
        { get; set; }

        public string DirectAddress
        { get; set; }

        public Boolean IsProviderInbox
        { get; set; }

        public Boolean IsPreferred
        { get; set; }
        public string SSPID
        { get; set; }

        public string UserLoginName { get; set; }

        public string ProviderNameAndAddress { get { return this.Name + " - " + this.DirectAddress; } }

        #endregion

        #region "Text Representation"
        public string FirstNameAndDirectAddress
        {
            get
            { return this.FirstName + " - " + this.DirectAddress; }
        }

        public string FirstAndLastName
        { get {return this.Name;} }

        public string NameAndDirectAddress
        { get { return this.FirstAndLastName + " - " + this.DirectAddress; } }


        #endregion

        #region "Constructors"
        
        public DirectUserProviderAssociation(DirectUserProviderAssociation DirectUserProviderAssociation)            
        {
            this.AssociationID = DirectUserProviderAssociation.AssociationID;
            this.ProviderID = DirectUserProviderAssociation.ProviderID;

            this.UserID = DirectUserProviderAssociation.UserID;

            this.FirstName = DirectUserProviderAssociation.FirstName;
            this.LastName = DirectUserProviderAssociation.LastName;

            this.Name = DirectUserProviderAssociation.Name;

            this.DirectAddress = DirectUserProviderAssociation.DirectAddress;
            this.IsPreferred = DirectUserProviderAssociation.IsPreferred;

            this.SSPID = DirectUserProviderAssociation.SSPID;
        }

        #region "Obsolete Constructors"
        //public DirectUserProviderAssociation(DataRow DataRow)             
        //    {
        //    this.AssociationID = Convert.ToInt64(DataRow["nAssociationID"]);
        //    this.ProviderID = Convert.ToInt64(DataRow["nProviderID"]);

        //    this.UserID = Convert.ToInt64(DataRow["nUserID"]);

        //    this.FirstName = Convert.ToString(DataRow["sFirstName"]);
        //    this.LastName = Convert.ToString(DataRow["sLastName"]);

        //    this.Name = FirstName + " " + LastName;

        //    this.DirectAddress = Convert.ToString(DataRow["sDirectAddress"]);
        //    this.IsPreferred = Convert.ToBoolean(DataRow["bIsPreferred"]);
        //}






        //public DirectUserProviderAssociation(Int64 AssociationID, Int64 UserID, string FirstName, string LastName, string DirectAddress)
        //    : this(AssociationID, UserID, FirstName, LastName, DirectAddress, false){}

        //public DirectUserProviderAssociation(Int64 AssociationID, Int64 UserID, string FirstName, string LastName, string DirectAddress, Boolean IsProviderInbox)
        //    : this(AssociationID, UserID, FirstName + " " + LastName, DirectAddress, false)
        //{      
        //    this.FirstName = FirstName;
        //    this.LastName = LastName;         
        //}


        //public DirectUserProviderAssociation(Int64 AssociationID, Int64 UserID, Int64 ProviderID, string Name, string DirectAddress, Boolean IsProviderInbox):this(AssociationID,UserID,ProviderID,string.Empty,string.Empty,DirectAddress,false,IsProviderInbox)
        //{
        //    this.AssociationID = AssociationID;
        //    this.UserID = UserID;
        //    this.ProviderID = ProviderID;
        //    this.Name = Name;
        //    this.DirectAddress = DirectAddress;
        //    this.IsProviderInbox = IsProviderInbox;

        //}
        #endregion

       #region "New Constructors"

        public DirectUserProviderAssociation
            (
            Int64 AssociationID,
            Int64 UserID,
            Int64 ProviderID,
            string Name,
            string DirectAddress,
            string SSPID,
            Boolean IsProviderInbox
            )
            :
            this
            (
                AssociationID,
                UserID,
                ProviderID,
                Name,
                DirectAddress,
                SSPID,
                false,
                IsProviderInbox
            ) { }

        public DirectUserProviderAssociation
          (
          Int64 AssociationID,
          Int64 UserID,
          Int64 ProviderID,
          string Name,
          string DirectAddress,
          string SSPID,
          Boolean IsPreferred,
          Boolean IsProviderInbox
          )
            : this(AssociationID, UserID, ProviderID, Name, DirectAddress, SSPID, string.Empty, IsPreferred, IsProviderInbox)
        { }

        public DirectUserProviderAssociation
           (
           Int64 AssociationID,
           Int64 UserID,
           Int64 ProviderID,
           string Name,
           string DirectAddress,
           string SSPID,
           string UserLoginName,
           Boolean IsPreferred,
           Boolean IsProviderInbox
           )
        {
            this.AssociationID = AssociationID;
            this.UserID = UserID;
            this.ProviderID = ProviderID;

            this.DirectAddress = DirectAddress;
            this.IsPreferred = IsPreferred;
            this.IsProviderInbox = IsProviderInbox;

            this.Name = Name;
            this.UserLoginName = UserLoginName;
            this.SSPID = SSPID;

        }
        public DirectUserProviderAssociation
            (
            DataRow DataRow
            )
            :
            this
            (
            Convert.ToInt64(DataRow["nAssociationID"]),
            Convert.ToInt64(DataRow["nUserID"]),
            Convert.ToInt64(DataRow["nProviderID"]),
            Convert.ToString(DataRow["sFirstName"]),
            Convert.ToString(DataRow["sLastName"]),
            Convert.ToString(DataRow["sDirectAddress"]),
            Convert.ToString(DataRow["sSPIID"]),
            Convert.ToString(DataRow["sLoginName"]),
            Convert.ToBoolean(DataRow["bIsPreferred"]),
            false
            ) { }

        public DirectUserProviderAssociation
           (
           Int64 AssociationID,
           Int64 UserID,
           Int64 ProviderID,
           string FirstName,
           string LastName,
           string DirectAddress,
           string SSPID,
           string UserLoginName,
           Boolean IsPreferred,
           Boolean IsProviderInbox
           )
            :
            this
            (
            AssociationID,
            UserID,
            ProviderID,
            FirstName + " " + LastName,
            DirectAddress,
            SSPID,
            UserLoginName,
            IsPreferred,
            IsProviderInbox
            )
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
        }


        #endregion
                
        #endregion

        #region "Static Method"
        public static DirectUserProviderAssociation CloneObject(DirectUserProviderAssociation DirectUserProviderAssociation)
        { return new DirectUserProviderAssociation(DirectUserProviderAssociation); }
        #endregion

        #region "Dispose"
        public void Dispose()
        {
            this.AssociationID = 0;
            this.UserID = 0;
            this.ProviderID = 0;
            this.Name = null;
            this.FirstName = null;
            this.LastName = null;
            this.DirectAddress = null;
            this.IsProviderInbox = false;
            this.IsPreferred = false;
            this.UserLoginName = string.Empty;
        }
        #endregion
        
    }

    #endregion

}
