using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using gloDatabaseLayer;
using System.Windows.Data;

using System.IO;
using System.Xml.Serialization;
using gloGlobal;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Runtime.Serialization;

namespace gloSurescriptSecureMessage
{
  
   public class clsSecureMessageDB : IDisposable
    {

        public static Object BinaryDeSerialize(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.AssemblyFormat

                = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
            formatter.Binder

            = new VersionConfigToNamespaceAssemblyObjectBinder();
            Object obj = (Object)formatter.Deserialize(stream);
            return obj;
        }

        internal sealed class VersionConfigToNamespaceAssemblyObjectBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                Type typeToDeserialize = null;
                try
                {
                    string ToAssemblyName = assemblyName.Split(',')[0];
                    Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (Assembly ass in Assemblies)
                    {
                        if (ass.FullName.Split(',')[0] == ToAssemblyName)
                        {
                            typeToDeserialize = ass.GetType(typeName);
                            break;
                        }
                    }
                }
                catch (System.Exception exception)
                {
                    throw exception;
                }
                return typeToDeserialize;
            }
        }

        #region "Object Attributes"        
        private bool disposed = false;
        #endregion

        #region "Constructor and Destructor"

        public clsSecureMessageDB()
        {
        }

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

        #endregion

        #region "Public Functions that should be used to interact with this Object"

        public string InsertSureScriptMessageInDB(SecureMessage SecureMessage)
        {
            string returnedString = string.Empty;

            if (SecureMessage == null)
            {
                throw new Exception("Message not supplied");
            }
            else 
            {
                returnedString = InsertSureScriptMessageInDB(SecureMessage, null);
            }
            
            return returnedString;
        }

        public string InsertSureScriptMessageInDB(SecureMessage SecureMessage, List<Attachment> AttachmentList)
        {

            string sMessageID = string.Empty;
            if (SecureMessage == null) // Checking if argument SecureMessage is not Null
            {
                throw new Exception("Message not supplied");
            }
            else 
            {
                // Get a DBParameters object that contains multiple 
                // DBParameter objects
                DBParameters localDBParameters = GetMessageParameters(SecureMessage);

                if (AttachmentList != null)
                {

                    // If there are any attachments then we will get to unpack each
                    // Attachment object into a Row in the DataTable.

                    // The Stored Procedure in SQL Server has an Attachment form
                    // of Table Valued Parameter for the SP.
                    DataTable TVPAttachmentDataTable = GetAttachmentDataTable(AttachmentList);

                    // Calling the main function that actually interacts with the database
                    sMessageID = SerializeMessage(localDBParameters, TVPAttachmentDataTable);
                    

                    TVPAttachmentDataTable.Dispose();
                    TVPAttachmentDataTable = null;
                }
                else
                {
                    sMessageID = SerializeMessage(localDBParameters);
                }

                localDBParameters.Dispose();
                localDBParameters = null;
            }
            

            return sMessageID;
        }

        #endregion

        #region "Private Functions"

        #region "Main functions that actually interact with the Database. Private scope only."

        private string SerializeMessage(DBParameters DBParameters)
        {
            return SerializeMessage(DBParameters, null);
        }
        private string SerializeMessage(DBParameters DBParameters, DataTable TVPDataTable)
        {
            string sMessageID = string.Empty;

            using (DBParameters localParameterList = DBParameters)
            {
                if (TVPDataTable != null)
                {
                    using (DBParameter TVPParameter = new DBParameter("@TVP_Attachment", TVPDataTable, ParameterDirection.Input, SqlDbType.Structured))
                    { localParameterList.Add(TVPParameter); }
                }

                gloDatabaseLayer.DBLayer gloDatabaseLayer = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);

                try
                {
                    gloDatabaseLayer.Connect(false);
                    sMessageID = Convert.ToString( gloDatabaseLayer.ExecuteScalar("SureScriptsInsertMessageAttachment", localParameterList));
                }                                
                catch (Exception Error)
                {
                    if (sMessageID == string.Empty)
                    {
                        System.Windows.MessageBox.Show("Message was not inserted in database.", "Error in insertion", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    }
                    gloAuditTrail.gloAuditTrail.ExceptionLog(Error.ToString(), false);
                    Error = null;
                }
                finally
                {
                    gloDatabaseLayer.Disconnect();                    
                    if (gloDatabaseLayer != null) { gloDatabaseLayer.Dispose(); }
                }
            }// DBParameters disposed here.

            return sMessageID;
        }
        #endregion

        #region "Convert the Message and Attachment objects into DBParameters"
        
        private DBParameters GetMessageParameters(SecureMessage secureMessage)
        {
            DBParameters localDBParameters = new DBParameters();

            try
            {
                localDBParameters.Add(new DBParameter("@sSecureMessageID", secureMessage.messageID, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@sRelatesToMessageID", secureMessage.relateMessageID, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@sMessageVersionNo", secureMessage.version, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@sMessageReleaseNo", secureMessage.release, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@sMessageHighestVersion", secureMessage.highVersion, ParameterDirection.Input, SqlDbType.VarChar));

                localDBParameters.Add(new DBParameter("@nSenderID", secureMessage.senderID, ParameterDirection.Input, SqlDbType.BigInt));
                localDBParameters.Add(new DBParameter("@nReceiverID", secureMessage.receiverID, ParameterDirection.Input, SqlDbType.BigInt));
                localDBParameters.Add(new DBParameter("@sFrom", secureMessage.From, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@sFromQualifier", secureMessage.FromQualifier, ParameterDirection.Input, SqlDbType.VarChar));

                localDBParameters.Add(new DBParameter("@sTo", secureMessage.To, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@sToQualifier", secureMessage.ToQualifier, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@sSubject", secureMessage.subject, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@sMessageBody", secureMessage.messageBody, ParameterDirection.Input, SqlDbType.VarChar));

                localDBParameters.Add(new DBParameter("@dtSendReceiveDateTime_UTC", secureMessage.dateTimeUTC, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@dtSendReceiveDateTime", secureMessage.dateTimeNormal, ParameterDirection.Input, SqlDbType.DateTime));
                localDBParameters.Add(new DBParameter("@bIsRead", secureMessage.isRead, ParameterDirection.Input, SqlDbType.Bit));
                localDBParameters.Add(new DBParameter("@nPatientID", secureMessage.patientID, ParameterDirection.Input, SqlDbType.BigInt));

                localDBParameters.Add(new DBParameter("@nNoOfAttachments", secureMessage.noofAttachements, ParameterDirection.Input, SqlDbType.Int));
                localDBParameters.Add(new DBParameter("@bMessageStatus", secureMessage.MessageStatus, ParameterDirection.Input, SqlDbType.Int));
                localDBParameters.Add(new DBParameter("@bMessageType", secureMessage.messageType, ParameterDirection.Input, SqlDbType.Int));
                localDBParameters.Add(new DBParameter("@bIsAssociated", secureMessage.associated, ParameterDirection.Input, SqlDbType.Bit));

                localDBParameters.Add(new DBParameter("@sDeliveryStatusCode", secureMessage.deliveryStatusCode, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@sDeliveryStatusDescription", secureMessage.deliveryStatusDescription, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@sSoftwareVersion", secureMessage.softwareVersion, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@sSoftwareProduct", secureMessage.softwareProduct, ParameterDirection.Input, SqlDbType.VarChar));

                localDBParameters.Add(new DBParameter("@sCompanyName", secureMessage.companyName, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@sUserName", secureMessage.userName, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@sMachineName", secureMessage.machineName, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@bIsDeleted", secureMessage.deleted, ParameterDirection.Input, SqlDbType.Bit));
                localDBParameters.Add(new DBParameter("@nDocumentReferenceID", secureMessage.DocumentReferenceID, ParameterDirection.Input, SqlDbType.BigInt));
                localDBParameters.Add(new DBParameter("@sModuleName", secureMessage.ModuleName, ParameterDirection.Input, SqlDbType.VarChar));
                localDBParameters.Add(new DBParameter("@nUseCase", secureMessage.UseCase, ParameterDirection.Input, SqlDbType.Int));
                localDBParameters.Add(new DBParameter("@sDelegatedUser", secureMessage.DelegatedUser, ParameterDirection.Input, SqlDbType.VarChar));
            }

            catch (Exception Error)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Error.ToString(), false);
                Error = null;
            }
                                    
            return localDBParameters;
        }
        
        private DataTable GetAttachmentDataTable(List<Attachment> AttachmentList)
        {            
            DataTable TVPAttachmentDataTable = new DataTable("TVPAttachmentDataTable");

            try
            {
                TVPAttachmentDataTable.Columns.Add(new DataColumn("nSecureMessageInboxID", System.Type.GetType("System.Int64")));
                TVPAttachmentDataTable.Columns.Add(new DataColumn("nAttachmentID", System.Type.GetType("System.Int64")));
                TVPAttachmentDataTable.Columns.Add(new DataColumn("nModuleName", System.Type.GetType("System.Int64")));

                TVPAttachmentDataTable.Columns.Add(new DataColumn("nFileExtension", System.Type.GetType("System.Int64")));
                TVPAttachmentDataTable.Columns.Add(new DataColumn("sDocumentName", System.Type.GetType("System.String")));
                TVPAttachmentDataTable.Columns.Add(new DataColumn("iContent", System.Type.GetType("System.Byte[]")));
                TVPAttachmentDataTable.Columns.Add(new DataColumn("sCDAConfidentiality", System.Type.GetType("System.String")));

                foreach (Attachment attachmentInList in AttachmentList)
                {

                    DataRow TVPAttachmentDataRow = TVPAttachmentDataTable.NewRow();

                    TVPAttachmentDataRow["nSecureMessageInboxID"] = attachmentInList.nSecureMessageInboxID;
                    TVPAttachmentDataRow["nAttachmentID"] = attachmentInList.attachmentID;
                    TVPAttachmentDataRow["nModuleName"] = attachmentInList.moduleName;

                    TVPAttachmentDataRow["nFileExtension"] = attachmentInList.fileExtension;
                    TVPAttachmentDataRow["sDocumentName"] = attachmentInList.documentName;
                    TVPAttachmentDataRow["iContent"] = attachmentInList.iContent;

                    if (attachmentInList.iContent != null & attachmentInList.fileExtension ==3)
                    {

                        XmlSerializer xs = null;
                        FileStream fs = null;
                        string strFileName = GetFileName(gloSettings.FolderSettings.AppTempFolderPath);
                        SecureMessage.ConvertBinarytoFile(attachmentInList.iContent, strFileName);
                        if (strFileName.Trim() != "")
                        {
                            xs = new XmlSerializer(typeof(POCD_MT000040UV02ClinicalDocument));
                            fs = new FileStream(strFileName, FileMode.Open);
                            try
                            {
                                POCD_MT000040UV02ClinicalDocument obj11 = (POCD_MT000040UV02ClinicalDocument)xs.Deserialize(fs);
                                if (obj11 is POCD_MT000040UV02ClinicalDocument)
                                {
                                    POCD_MT000040UV02ClinicalDocument CCDAFile = (POCD_MT000040UV02ClinicalDocument)obj11;

                                    if (CCDAFile != null)
                                    {
                                        TVPAttachmentDataRow["sCDAConfidentiality"] = CCDAFile.confidentialityCode.code;
                                    }
                                    CCDAFile = null;
                                }
                                obj11 = null;
                            }
                            catch 
                            {
                                //Exception ex
                                //  System.Windows.Forms.MessageBox.Show(ex.GetBaseException().ToString());
                            }
                            finally
                            {
                                xs = null;
                                fs = null;                                
                            }                            
                        }
                      
                    }
                    else
                    {
                        TVPAttachmentDataRow["sCDAConfidentiality"] = DBNull.Value;
                    }
                               


                    TVPAttachmentDataTable.Rows.Add(TVPAttachmentDataRow);
                    TVPAttachmentDataRow = null;
                }
            }
            
            catch (Exception Error)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Error.ToString(), false);
                Error = null;
            }
            
            return TVPAttachmentDataTable;
        }
        
        #endregion
        
        #endregion

        public Int32 GetNewMailCount(Int64 nUserID, RequestFrom sRequestFrom)
        {
            Int32 nNewMailCount = 0;
            gloDatabaseLayer.DBLayer ogloDatabaseLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {

                if (gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress != null)
                {
                    ogloDatabaseLayer = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                    oDBParameters = new DBParameters();
                    oDBParameters.Add("@LoginUserEmailID", gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@TypeOfMail", sRequestFrom.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    
                    ogloDatabaseLayer.Connect(false);
                    nNewMailCount = Convert.ToInt32(ogloDatabaseLayer.ExecuteScalar("SureScripts_GetMails", oDBParameters));  
                    ogloDatabaseLayer.Disconnect();
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
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                if (ogloDatabaseLayer != null) { ogloDatabaseLayer.Dispose(); }
            }

            return nNewMailCount;
        }

        public DataSet GetNewMail(Int64 UserID, Decimal DifferenceInMinutes)
        {
            DataSet returned = new DataSet();

            gloDatabaseLayer.DBLayer ogloDatabaseLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
                ogloDatabaseLayer = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                oDBParameters = new DBParameters();
                oDBParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@nDifferenceInMinutes", DifferenceInMinutes, ParameterDirection.Input, SqlDbType.Decimal);

                ogloDatabaseLayer.Connect(false);
                ogloDatabaseLayer.Retrive("Direct_GetAllUnreadMails", oDBParameters, out returned);
                ogloDatabaseLayer.Disconnect();
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
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                if (ogloDatabaseLayer != null) { ogloDatabaseLayer.Dispose(); }
            }

            return returned;
        }

        public Int32[] GetUnReadMailCount(string ProviderDirectAddress)
        {
            Int32[] nNewMailCount = new int[2];
            gloDatabaseLayer.DBLayer ogloDatabaseLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            DataSet dsMailCount = null;
            try
            {

                if (gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress != null)
                {
                    ogloDatabaseLayer = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                    oDBParameters = new DBParameters();
                    oDBParameters.Add("@LoginUserEmailID", ProviderDirectAddress, ParameterDirection.Input, SqlDbType.VarChar);
                    dsMailCount = new DataSet();
                    ogloDatabaseLayer.Connect(false);
                    ogloDatabaseLayer.Retrive("SureScripts_GetUnReadMailCount",oDBParameters, out dsMailCount);
                    ogloDatabaseLayer.Disconnect();

                    
                    if (dsMailCount.Tables[0].Rows.Count > 0)
                    { nNewMailCount[0] = Convert.ToInt32(dsMailCount.Tables[0].Rows[0][0]); }
                    else
                    { nNewMailCount[0] = 0; }

                    if (dsMailCount.Tables[1].Rows.Count > 0)
                    { nNewMailCount[1] = Convert.ToInt32(dsMailCount.Tables[1].Rows[0][0]); }
                    else
                    { nNewMailCount[1] = 0; }
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
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                if (ogloDatabaseLayer != null) { ogloDatabaseLayer.Dispose(); }
            }

            return nNewMailCount;
        }

        public DataSet RetrieveMails(string DirectAddress, RequestFrom requestFrom,Int32 nStart,Int32 nEnd)
        {
            DataSet dsInbox = null;
            gloDatabaseLayer.DBLayer ogloDatabaseLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {

                if (!string.IsNullOrWhiteSpace(DirectAddress))
                {
                    ogloDatabaseLayer = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                    oDBParameters = new DBParameters();
                    oDBParameters.Add("@LoginUserEmailID", DirectAddress, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@TypeOfMail", requestFrom.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@StartIndex", nStart, ParameterDirection.Input, SqlDbType.Int);
                    oDBParameters.Add("@EndIndex", nEnd, ParameterDirection.Input, SqlDbType.Int);
                    dsInbox = new DataSet();
                    ogloDatabaseLayer.Connect(false);
                    ogloDatabaseLayer.Retrive("SureScripts_GetMails", oDBParameters, out dsInbox);
                    ogloDatabaseLayer.Disconnect();
                }

                return dsInbox;
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
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                if (ogloDatabaseLayer != null) { ogloDatabaseLayer.Dispose(); }
            }
        }

        //public DataSet RetrieveMails(string DirectAddress, RequestFrom requestFrom)
        //{
        //    DataSet dsInbox = null;
        //    gloDatabaseLayer.DBLayer ogloDatabaseLayer = null;
        //    gloDatabaseLayer.DBParameters oDBParameters = null;
        //    try
        //    {

        //        if (!string.IsNullOrWhiteSpace(DirectAddress))
        //        {
        //            ogloDatabaseLayer = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
        //            oDBParameters = new DBParameters();
        //            oDBParameters.Add("@LoginUserEmailID", DirectAddress, ParameterDirection.Input, SqlDbType.VarChar);
        //            oDBParameters.Add("@TypeOfMail", requestFrom.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //            dsInbox = new DataSet();
        //            ogloDatabaseLayer.Connect(false);
        //            ogloDatabaseLayer.Retrive("SureScripts_GetMails", oDBParameters, out dsInbox);
        //            ogloDatabaseLayer.Disconnect();
        //        }

        //        return dsInbox;
        //    }
        //    catch (gloDatabaseLayer.DBException DBErr)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
        //        DBErr = null;
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        ex = null;
        //        return null;
        //    }
        //    finally
        //    {
        //        if (oDBParameters != null) { oDBParameters.Dispose(); }
        //        if (ogloDatabaseLayer != null) { ogloDatabaseLayer.Dispose(); }
        //    }
        //}
        public DataTable RetrieveMailPageCount(string DirectAddress, RequestFrom requestFrom)
        {
            DataTable dtResult = null;
            
            gloDatabaseLayer.DBLayer ogloDatabaseLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            
            try
            {

                if (!string.IsNullOrWhiteSpace(DirectAddress))
                {
                    ogloDatabaseLayer = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                    oDBParameters = new DBParameters();
                    oDBParameters.Add("@LoginUserEmailID", DirectAddress, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@TypeOfMail", requestFrom.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                    ogloDatabaseLayer.Connect(false);
                    ogloDatabaseLayer.Retrive("SureScripts_GetMailPageCount", oDBParameters, out dtResult);
                    ogloDatabaseLayer.Disconnect();
                }

                return dtResult;
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
                if (oDBParameters != null) { oDBParameters.Dispose(); }
                if (ogloDatabaseLayer != null) { ogloDatabaseLayer.Dispose(); }
            }
        }

        //public DataSet RetrieveMails(Int64 nUserID, RequestFrom sRequestFrom)
        //{
        //    DataSet dsInbox = null;
        //    gloDatabaseLayer.DBLayer ogloDatabaseLayer = null;
        //    gloDatabaseLayer.DBParameters oDBParameters = null ; 
        //    try
        //    {

        //        if (gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress != null)
        //        {
        //            ogloDatabaseLayer = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
        //            oDBParameters = new DBParameters(); 
        //            oDBParameters.Add("@LoginUserEmailID", gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, ParameterDirection.Input, SqlDbType.VarChar);
        //            oDBParameters.Add("@TypeOfMail", sRequestFrom.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
        //            dsInbox = new DataSet();
        //            ogloDatabaseLayer.Connect(false);
        //            ogloDatabaseLayer.Retrive("SureScripts_GetMails", oDBParameters, out dsInbox);
        //            ogloDatabaseLayer.Disconnect();
        //        }

        //        return dsInbox;
        //    }
        //    catch (gloDatabaseLayer.DBException DBErr)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), false);
        //        DBErr = null;
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        ex = null;
        //        return null;
        //    }
        //    finally
        //    {
        //        if (oDBParameters != null) { oDBParameters.Dispose(); }
        //        if (ogloDatabaseLayer != null) { ogloDatabaseLayer.Dispose(); }
        //    }
        //}

        public DataTable GetReferalList(long PatientID)
        {
           DataTable dtreferal = null;
           gloDatabaseLayer.DBLayer oDB = null;
           gloDatabaseLayer.DBParameters oDBParameters = null;

           try
           {
               if (PatientID != 0)
               {
                   oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                   oDBParameters = new gloDatabaseLayer.DBParameters();
                   oDB.Connect(false);
                   oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                   oDB.Retrive("SurescriptGetReferalNameAndID", oDBParameters, out dtreferal);
                   oDB.Disconnect();
               }
               return dtreferal;

           }
           catch (Exception ex)
           {
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
               dtreferal = null;
               return null;
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

                if (dtreferal != null)
                {
                    dtreferal.Dispose();
                    dtreferal = null;
                }               
            }          
           
        }

        public DataTable GetReferalListByContactID(long ContactID)
        {
            DataTable dtreferal = null;
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;

            try
            {
                if (ContactID != 0)
                {
                    oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                    oDBParameters = new gloDatabaseLayer.DBParameters();
                    oDB.Connect(false);
                    oDBParameters.Add("@nContactID", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("SurescriptGetReferalByID", oDBParameters, out dtreferal);
                    oDB.Disconnect();
                }
                return dtreferal;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                dtreferal = null;
                return null;
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

                if (dtreferal != null)
                {
                    dtreferal.Dispose();
                    dtreferal = null;
                }
            }

        }
        
        public DataTable GetReferalListByDataTable(DataTable dtTVP)
        {
            DataTable dtreferal = null;
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters =null;

            try 
            {
                if (dtTVP != null)
                {
                    oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                    oDBParameters = new gloDatabaseLayer.DBParameters();
                    oDB.Connect(false);
                    oDBParameters.Add("@refIDS", dtTVP, ParameterDirection.Input, SqlDbType.Structured);
                    oDB.Retrive("SurescriptGetReferalInformation", oDBParameters, out dtreferal);
                    oDB.Disconnect();
                }
                return dtreferal;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                dtreferal = null;
                return null;
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

                if (dtreferal != null)
                {
                    dtreferal.Dispose();
                    dtreferal = null;
                }
            }

        }

        public string GetProviderEmailByID(long providerID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            string emailID = string.Empty;

            try
            {
                if (providerID != 0)
                {
                    oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                    oDBParameters = new gloDatabaseLayer.DBParameters();
                    oDB.Connect(false);
                    oDBParameters.Add("@nProviderID", providerID, ParameterDirection.Input, SqlDbType.BigInt);
                    emailID = Convert.ToString(oDB.ExecuteScalar("SurescriptGetProviderEmailByID", oDBParameters));
                    oDB.Disconnect();
                }

                return emailID;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return emailID;
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
                }
            }

        }

        public DataSet GetPatientDetails(long PatientID)
        {
            DataSet ds = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("GetPatientStripDetails", oDBParameters, out ds);
                oDB.Disconnect();



                ds.Tables[0].TableName = "Module";
                ds.Tables[1].TableName = "PCP";
                ds.Tables[2].TableName = "PharmaName";
                ds.Tables[3].TableName = "PharmaPhone";
                ds.Tables[4].TableName = "PharmaFax";
                ds.Tables[5].TableName = "ReffName";
                ds.Tables[6].TableName = "Provider";
                ds.Tables[7].TableName = "PatientInfo";
                ds.Tables[8].TableName = "AgeSettings";
                ds.Tables[9].TableName = "PediatricSetting";
                ds.Tables[10].TableName = "Insurance"; ;
                ds.Tables[11].TableName = "PharmacyAddress1";
                ds.Tables[12].TableName = "PharmacyAddress2";
                ds.Tables[13].TableName = "PharmaState";
                ds.Tables[14].TableName = "PharmaCity";
                ds.Tables[15].TableName = "PharmaZip";
                ds.Tables[16].TableName = "IntuitSettings";



                return ds;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ds = null;
                return null;
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

                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }

        }

        public DataTable GetPatientDetailsforSecureMessage(long PatientID)
        {
            DataTable  dt = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("GetPatientDetailsforSecureMessage", oDBParameters, out dt);
                oDB.Disconnect();
                return dt;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                dt = null;
                return null;
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

                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }

        }

        public DataTable GetProviderDetails(long providerID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string _strQuery = "SELECT dbo.GET_NAME(sFirstName,sMiddleName,sLastName) AS sProviderName,ISNULL(sDirectAddress,'') as sDirectAddress, sSPIID as sSPID  FROM Provider_MST WHERE nProviderID = " + gloSurescriptSecureMessage.SecureMessageProperties.ProviderID + "  ";
            DataTable dtProviderDetails = null;
            try
            {
                if (providerID > 0)
                {
                    oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                    oDB.Connect(false);
                    oDB.Retrive_Query(_strQuery, out dtProviderDetails);
                    oDB.Disconnect();
                }
                return dtProviderDetails;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return dtProviderDetails;
            }
            finally
            {

                if (oDB != null)
                {
                    oDB.Dispose();
                }
                if (dtProviderDetails != null)
                {
                    dtProviderDetails.Dispose();
                    dtProviderDetails = null;
                }
            }

        }

        public DataSet RetrieveSecureMessageStagging()
        {
             DataSet ds = null;
             gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
            
            try
            {
                oDB.Connect(false);
              
                oDB.Retrive("SureScriptsGetFromDBtoXML", out ds);
                oDB.Disconnect();
                ds.Tables[0].TableName = "MessagesDataTable";
                ds.Tables[1].TableName = "AttachmentsDataTable";
                return ds;
            }

            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return ds;
            }
            finally
            {

                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
              
            }
        }

        public Boolean RevertDownloadStatus(Int64 nSecureMessageInboxID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
            Object _result = null;
            Boolean _isUpdated=false;
            try
            {
                oDB.Connect(false);
                string _strQuery = "UPDATE SecureMessage_Inbox SET sDeliveryStatusCode='999',sDeliveryStatusDescription='Processing' WHERE nSecureMessageInboxID= " + nSecureMessageInboxID + "";
                _result = oDB.Execute_Query(_strQuery);
                oDB.Disconnect();
                if (_result != null)
                {
                    if (Convert.ToInt16(_result) == 1)
                    {
                        _isUpdated = true;
                    }
                }
                return _isUpdated;
            }
            catch (Exception Error)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Error.ToString(), false);
                return _isUpdated;
            }
            finally
            {

                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (_result != null) { _result=null ; }
            }
        }

        public Boolean DeleteMail(Int64 nSecureMessageInboxID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            Boolean _bResult = false;
            try
            {
                int _result = 0;
                string _strQuery = "UPDATE SecureMessage_Inbox SET bIsDeleted=1 WHERE nSecureMessageInboxID= " + nSecureMessageInboxID + "";
                oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                oDB.Connect(false);
                _result= oDB.Execute_Query(_strQuery);
                oDB.Disconnect();
                if (_result > 0)
                {
                    _bResult = true;
 
                }
                return _bResult;
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return _bResult;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }

            }

        }

        public DataTable GetSecureMessageDatailsUsingMessageID(string MessageID)
        {
            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@MessageID", MessageID, ParameterDirection.Input, SqlDbType.VarChar, 40);
                oDB.Retrive("GetSecureMessageDetailsUsingMessageID", oDBParameters, out dt);
                oDB.Disconnect();
                return dt;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                dt = null;
                return null;
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

                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }

        }

        public List<Attachment> GetFileInformationForAttachment(string FileName)
        {
            List<Attachment> oLsAttach = null;
            Attachment objAttach = null;

            gloCCDLibrary.gloCCDInterface ogloInterface = null;
            ogloInterface = new gloCCDLibrary.gloCCDInterface();
            string fileName = string.Empty;
            string DocumentName = string.Empty;
            string fileExt = string.Empty;
            string Base64String = string.Empty;
            string mimeType = string.Empty;

            //'string mimeType = GetMimeType(fileExt.ToUpper());

            double fileLength = 0.0;

            if (!string.IsNullOrEmpty(FileName))
            {

                objAttach = new Attachment();
                oLsAttach = new List<Attachment>();
                fileName = FileName;

                DocumentName = Path.GetFileName(FileName);
                objAttach.documentName = DocumentName;

                fileExt = Path.GetExtension(FileName);

                mimeType = GetMimeType(fileExt.ToUpper());

                foreach (FileExtension fx in Enum.GetValues(typeof(FileExtension)))
                {
                    if (fileExt.Replace(".", "").ToLower() == Convert.ToString(fx))
                    {
                        objAttach.fileExtension = Convert.ToInt16(fx.GetHashCode());
                        break;
                    }

                }



                Base64String = Convert.ToBase64String(File.ReadAllBytes(fileName), Base64FormattingOptions.InsertLineBreaks);
                objAttach.base64 = Base64String;


                fileLength = Base64String.Length;
                mimeType = GetMimeType(fileExt.ToUpper());

                objAttach.mimeType = mimeType;



                if (CalculateFileSize(fileLength))
                {
                    Byte[] arrByte = ogloInterface.ConvertFiletoBinary(FileName);
                    objAttach.iContent = arrByte;
                    arrByte = null;
                }
                else
                {

                    //System.Windows.MessageBox.Show("File size of '" & strFileName & "' is " & SetBytes(Bytes) & ". File size should not exceed 2MB.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information)  "File size Exceed";
                    //System.Windows.MessageBox.Show("File '" + DocumentName + "'  size exceeded", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                }

                oLsAttach.Add(objAttach);

            }
            return oLsAttach;
        }

        public void InsertOppSecureMessage(DataTable dtMessage, List<Attachment> oLsAttach, Int64 nPatientID)
        {
            SecureMessage SecureMessage = null;
            string Messagedescription = "";
            clsSecureMessageDB objclsSecureDB = new clsSecureMessageDB();
            try
            {
                SecureMessage = new SecureMessage();
                DateTime dtdate;
                var _with1 = SecureMessage;
                //_with1.secureMessageInboxID = Convert.ToInt64(dtMessage.Rows[0]["nSecureMessageInboxID"].ToString());
                //_with1.messageID = 
                _with1.relateMessageID = Convert.ToString(dtMessage.Rows[0]["sMessageID"].ToString());
                _with1.version = Convert.ToString(dtMessage.Rows[0]["sMessageVersionNo"].ToString());
                _with1.release = Convert.ToString(dtMessage.Rows[0]["sMessageReleaseNo"].ToString());
                _with1.highVersion = Convert.ToString(dtMessage.Rows[0]["sMessageHighestVersion"].ToString());

                _with1.senderID = Convert.ToInt64(dtMessage.Rows[0]["nReceiverID"].ToString());
                _with1.receiverID = Convert.ToInt64(dtMessage.Rows[0]["nSenderID"].ToString());
                _with1.To = Convert.ToString(dtMessage.Rows[0]["sFrom"].ToString());
                _with1.ToQualifier = Convert.ToString(dtMessage.Rows[0]["sFromQualifier"].ToString());

                _with1.From = Convert.ToString(dtMessage.Rows[0]["sTo"].ToString());
                _with1.FromQualifier = Convert.ToString(dtMessage.Rows[0]["sToQualifier"].ToString());
                _with1.subject = "Opportunity Disposition";
                //Convert.ToString(dtMessage.Rows[0]["sSubject"].ToString());
                _with1.messageBody = "Please find attachment for Opportunity Disposition";
                //Convert.ToString(dtMessage.Rows[0]["sMessageBody"].ToString());

                dtdate = System.DateTime.UtcNow;
                string strdate = dtdate.ToString("yyyy-MM-dd");
                string strtime = dtdate.ToString("hh:mm:ss");
                string strUTCFormat = strdate + "T" + strtime + ".0Z";
                _with1.dateTimeUTC = strUTCFormat;
                _with1.dateTimeNormal = System.DateTime.Now;
                _with1.isRead = 0;
                _with1.patientID = nPatientID;
                //Convert.ToInt64(dtMessage.Rows[0]["nPatientID"].ToString());


                if (_with1.patientID > 0)
                {
                    clsSecureMessageDB clsDb = new clsSecureMessageDB();
                    DataTable dtPatient = new DataTable();
                    dtPatient = clsDb.GetPatientDetailsforSecureMessage(_with1.patientID);
                    if (dtPatient != null)
                    {
                        if (dtPatient.Rows.Count > 0)
                        {
                            _with1.firstName = Convert.ToString(dtPatient.Rows[0]["sfirstname"]);
                            _with1.lastName = Convert.ToString(dtPatient.Rows[0]["sLastName"]);
                            _with1.Dob = Convert.ToString(dtPatient.Rows[0]["dob"]).Trim();
                            _with1.gender = Convert.ToString(dtPatient.Rows[0]["Gender"]);
                            _with1.zip = Convert.ToString(dtPatient.Rows[0]["sZIP"]);
                            _with1.clinicCode = Convert.ToString(dtPatient.Rows[0]["sExternalCode"]);
                            _with1.patientCode = Convert.ToString(dtPatient.Rows[0]["sPatientCode"]);
                        }
                    }
                    if (clsDb != null)
                    {
                        clsDb.Dispose();
                        clsDb = null;
                    }

                    if (dtPatient != null)
                    {
                        dtPatient.Dispose();
                        dtPatient = null;
                    }
                }

                if (oLsAttach != null)
                {
                    _with1.noofAttachements = (short)oLsAttach.Count;
                }
                else
                {
                    _with1.noofAttachements = 0;
                }
                _with1.MessageStatus = Convert.ToInt16(MessageStatus.Send.GetHashCode());
                _with1.messageType = Convert.ToInt16(gloSurescriptSecureMessage.MessageType.Send.GetHashCode());
                _with1.associated = 0;

                _with1.deliveryStatusCode = "";
                _with1.deliveryStatusDescription = "";
                _with1.softwareVersion = System.Windows.Forms.Application.ProductVersion;
                _with1.softwareProduct = System.Windows.Forms.Application.ProductName;
                _with1.companyName = System.Windows.Forms.Application.CompanyName;
                _with1.userName = gloSurescriptSecureMessage.SecureMessageProperties.UserName;
                _with1.machineName = System.Environment.MachineName;
                _with1.deleted = 0;
                _with1.DocumentReferenceID = 0;
                _with1.ModuleName = "Rx Savings";
                _with1.messageID = "aaaaabbbbbcccccdddddeeeeefffffggggghhhhh";
                _with1.UseCase = 1;
                _with1.messageID = null;
                _with1.DelegatedUser = "";


                if (_with1.noofAttachements > 0)
                {
                    _with1.messageID = objclsSecureDB.InsertSureScriptMessageInDB(_with1, oLsAttach);
                }
                else
                {
                    _with1.messageID = objclsSecureDB.InsertSureScriptMessageInDB(_with1);
                }

                if (!string.IsNullOrEmpty(_with1.messageID))
                {
                    XmlSerializer xs = null;
                    FileStream fs = null;
                    N2NMessageType objN2N = null;

                    try
                    {
                        byte[] byteArray = SecureMessage.GenerateXML(_with1, oLsAttach);
                        byte[] Response = null;
                        string key = string.Empty;

                        gloSurescriptSecureMessage.gloDirectservice.IgloDirectClient oDirect;
                        //oDirect = gloSurescriptSecureMessage.SecureMessage.GetSecureMsgFSvc("http://localhost:1454/gloDirect.svc/Secure");

                        if (gloSurescriptSecureMessage.SecureMessageProperties.IsStagingServerEnable)
                            oDirect = gloSurescriptSecureMessage.SecureMessage.GetSecureMsgFSvc(gloSurescriptSecureMessage.SecureMessageProperties.StagingServerUrl);
                        else
                            oDirect = gloSurescriptSecureMessage.SecureMessage.GetSecureMsgFSvc(gloSurescriptSecureMessage.SecureMessageProperties.ProductionServerUrl); 
                        //oDirect.
                        key = oDirect.Login("gloSecureMsg@ophit.net", "spX12ss@!!21nasik");
                        //Response = oDirect.PostSecureMessage(byteArray);

                        Response = oDirect.PostSecureMessage(_with1.messageID, _with1.From, _with1.To, SecureMessageProperties.SPID, SecureMessageProperties.ClinicName, SecureMessageProperties.AUSID, SecureMessageProperties.SiteID, SecureMessageProperties.Location, byteArray, (gloSurescriptSecureMessage.gloDirectservice.ClsglobalMessageType)gloSurescriptSecureMessage.MessageType.Send);

                        if (Response != null)
                        {
                            string strFileName = GetFileName(gloSettings.FolderSettings.AppTempFolderPath);
                            SecureMessage.ConvertBinarytoFile(Response, strFileName);

                            if (strFileName.Trim() != "")
                            {
                                xs = new XmlSerializer(typeof(N2NMessageType));
                                fs = new FileStream(strFileName, FileMode.Open);
                                try
                                {
                                    objN2N = (N2NMessageType)xs.Deserialize(fs);
                                }
                                catch //(Exception ex)
                                {

                                    System.Windows.Forms.MessageBox.Show("Sure Script Unable to process Message", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                }


                                fs.Close();
                                if (objN2N != null)
                                {
                                    _with1 = SecureMessage.ExtractXML(objN2N, _with1);

                                    if (_with1.deliveryStatusDescription == "" && _with1.deliveryStatusCode == "010")
                                    {
                                        _with1.deliveryStatusDescription = "Clinical message delivered";
                                    }
                                    else if (_with1.deliveryStatusDescription == "" && _with1.deliveryStatusCode == "000")
                                    {
                                        _with1.deliveryStatusDescription = "Clinical message sent to partner network";
                                    }

                                    _with1.messageID = objclsSecureDB.InsertSureScriptMessageInDB(_with1);

                                    if (_with1.deliveryStatusDescription != "")
                                    {
                                        //System.Windows.Forms.MessageBox.Show(objSecureMessage.deliveryStatusDescription, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                        if (Messagedescription != "")
                                        {
                                            if (_with1.deliveryStatusDescription.Contains("NetworkAddress has an invalid format") || _with1.deliveryStatusDescription.Contains("@"))
                                            {
                                                Messagedescription = Messagedescription + _with1.deliveryStatusDescription + "\n\n";
                                            }
                                            else
                                            {
                                                //Messagedescription = Messagedescription + objSecureMessage.deliveryStatusDescription + " for " + objSecureMessage.To + "\n";
                                                Messagedescription = Messagedescription + _with1.From + "\n     " + _with1.deliveryStatusDescription + "\n\n";
                                            }
                                        }
                                        else
                                        {
                                            //Messagedescription = objSecureMessage.deliveryStatusDescription + " for " + objSecureMessage.To + "\n";
                                            if (_with1.deliveryStatusDescription.Contains("NetworkAddress has an invalid format") || _with1.deliveryStatusDescription.Contains("@"))
                                            {
                                                Messagedescription = _with1.deliveryStatusDescription + "\n\n";
                                            }
                                            else
                                            {
                                                Messagedescription = _with1.From + "\n    " + _with1.deliveryStatusDescription + "\n\n";
                                            }
                                        }

                                    }

                                }
                                else
                                {
                                    _with1.deliveryStatusCode = "999";
                                    _with1.deliveryStatusDescription = "Processing";
                                    if (Messagedescription != "")
                                    {
                                        Messagedescription = Messagedescription + _with1.To + "\n     " + "Message sending is in queue" + "\n\n";
                                    }
                                    else
                                    {
                                        Messagedescription = _with1.To + "\n    " + "Message sending is in queue" + "\n\n";
                                    }
                                    _with1.messageID = objclsSecureDB.InsertSureScriptMessageInDB(_with1);
                                }



                            }
                        }
                        else
                        {
                            _with1.deliveryStatusCode = "999";
                            _with1.deliveryStatusDescription = "Processing";
                            if (Messagedescription != "")
                            {
                                Messagedescription = Messagedescription + _with1.To + "\n     " + "Message sending is in queue" + "\n\n";
                            }
                            else
                            {
                                Messagedescription = _with1.To + "\n    " + "Message sending is in queue" + "\n\n";
                            }
                            _with1.messageID = objclsSecureDB.InsertSureScriptMessageInDB(_with1);

                        }

                        oDirect.Close();
                        oDirect = null;

                    }
                    catch //(Exception ex)
                    {
                        _with1.deliveryStatusCode = "999";
                        _with1.deliveryStatusDescription = "Processing";
                        if (Messagedescription != "")
                        {
                            Messagedescription = Messagedescription + _with1.To + "\n     " + "Message sending is in queue" + "\n\n";
                        }
                        else
                        {
                            Messagedescription = _with1.To + "\n    " + "Message sending is in queue" + "\n\n";
                        }
                        _with1.messageID = objclsSecureDB.InsertSureScriptMessageInDB(_with1);

                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    }
                    finally
                    {
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                        if (objN2N != null)
                        {
                            objN2N = null;
                        }
                        if (fs != null)
                        {
                            fs = null;
                        }
                        if (xs != null)
                        {
                            xs = null;
                        }
                    }

                }
                //return SecureMessage;

            }
            catch //(Exception ex)
            {
                //mdlGeneral.UpdateLog("Error :" + ex.ToString());
                //return null;

            }
            finally
            {
            }

        }


        public static String GetFileName(String strAppPath)
        {
            try
            {

                ////clsException.UpdateLog("start GetFileName", LogFilePath, EnableLog);
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
                ////clsException.UpdateLog("End GetFileName", LogFilePath, EnableLog);
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
        private bool CalculateFileSize(double Length)
        {
            double fileLength = 0.0;

            if (Length > 1024)
            {
                fileLength = Length / 1024;
            }
            else
            {
                fileLength = (Length / 1024f) / 1024f;
                fileLength = Math.Round(fileLength, 4);
            }

            if (fileLength >= 20480) // 20.0 mb
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private string GetMimeType(string extension)
        {
            string documentType = string.Empty;
            switch (extension.ToUpper())
            {
                case ".XML":
                    {
                        documentType = "application/xml";
                        break;
                    }
                case ".PDF":
                    {
                        documentType = "application/pdf";
                        break;
                    }

                case ".DOC":
                    {
                        documentType = "application/msword";
                        break;
                    }
                case ".DOCX":
                    {
                        documentType = "application/msword";
                        break;
                    }
                case ".ZIP":
                    {
                        documentType = "application/zip";
                        break;
                    }
                case ".HTML":
                    {
                        documentType = "text/html";
                        break;
                    }
                case ".HTM":
                    {
                        documentType = "text/html";
                        break;
                    }
                case ".TXT":
                    {
                        documentType = "text/plain";
                        break;
                    }
                case ".RTF":
                    {
                        documentType = "text/RTF";
                        break;
                    }

                default:
                    break;

            }

            return documentType;
        }



        //#region "Functions"


        //public void GetPatientSavingDetailsUsingMessageID(string sMessageID)
        //{
        //    DataSet dsPatientQueueMessages = null;
        //    dsPatientQueueMessages = RetrieveMessages(nMessageID);
        //    dsPatientQueueMessages.Tables[0].TableName = "MessagesDataTable";
        //    dsPatientQueueMessages.Tables[1].TableName = "AttachmentsDataTable";

        //    //dsPatientQueueMessages=RetrivePatientSavingDetailsUsingMessageID()

        //}

        //private DataSet RetrieveQueuedMessages(long nMessageID)
        //{
        //    DataSet ds = null;
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

        //    try
        //    {
        //        oDB.Connect(false);
        //        oDBParameters.Add("@MessageID", nMessageID, ParameterDirection.Input, SqlDbType.BigInt);
        //        oDB.Retrive("GetPatientSavingDetailsUsingMessageID", oDBParameters, out ds);
        //        oDB.Disconnect();
        //        return ds;

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        ds = null;
        //        return null;
        //    }
        //    finally
        //    {

        //        if (oDBParameters != null)
        //        {
        //            oDBParameters.Dispose();
        //            oDBParameters = null;
        //        }

        //        if (oDB != null)
        //        {
        //            oDB.Dispose();
        //            oDB = null;
        //        }

        //        if (ds != null)
        //        {
        //            ds.Dispose();
        //            ds = null;
        //        }
        //    }
        //}



        //#endregion

        public DataTable GetAllPatientReferalsByPatientID(long PatientID)
        {
            DataTable dtreferal = null;
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;

            try
            {
                if (PatientID != 0)
                {
                    oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                    oDBParameters = new gloDatabaseLayer.DBParameters();
                    oDB.Connect(false);
                    oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("SurescriptGetAllReferalByPatientID", oDBParameters, out dtreferal);
                    oDB.Disconnect();
                }
                return dtreferal;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                dtreferal = null;
                return null;
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

                if (dtreferal != null)
                {
                    dtreferal.Dispose();
                    dtreferal = null;
                }
            }

        }

        public DataTable GetAllOrderResultByPatientID(long PatientID)
        {
            DataTable dtreferal = null;
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;

            try
            {
                if (PatientID != 0)
                {
                    oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                    oDBParameters = new gloDatabaseLayer.DBParameters();
                    oDB.Connect(false);
                    oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("SurescriptGetOrderResultByPatientID", oDBParameters, out dtreferal);
                    oDB.Disconnect();
                }
                return dtreferal;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                dtreferal = null;
                return null;
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

                if (dtreferal != null)
                {
                    dtreferal.Dispose();
                    dtreferal = null;
                }
            }

        }

        public DataTable GetOwnAddress(string sSearchText)
        {
            //Added paramter sSearchText parameter to resolve Bug #88949: surescript Catalog search

            DataTable dtreferal = null;
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;

            try
            {

                oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oDBParameters.Add("@nClinicID", gloSurescriptSecureMessage.SecureMessageProperties.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sSearchText", sSearchText, ParameterDirection.Input, SqlDbType.Text);
                oDB.Retrive("SurescriptGetOwnAddress", oDBParameters, out dtreferal);
                oDB.Disconnect();

                return dtreferal;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                dtreferal = null;
                return null;
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (dtreferal != null) { dtreferal.Dispose(); dtreferal = null; }
            }
        }

        public DataTable GetOtherAddressList(string sSearchText)
        {
            //Added paramter sSearchText parameter to resolve Bug #88949: surescript Catalog search
            DataTable dtreferal = null;
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;

            try
            {
                oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDB.Connect(false);
                oDBParameters.Add("@nClinicID", gloSurescriptSecureMessage.SecureMessageProperties.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sSearchText", sSearchText, ParameterDirection.Input, SqlDbType.Text);
                oDB.Retrive("SurescriptGetOtherAddress", oDBParameters, out dtreferal);
                oDB.Disconnect();

                return dtreferal;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                dtreferal = null;
                return null;
            }
            finally
            {
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (dtreferal != null) { dtreferal.Dispose(); dtreferal = null; }
            }
        }

        public void SetPreferredProvider(long ProviderID, long UserID)
        {
            gloDatabaseLayer.DBLayer databaseLayer = null;
            DBParameters dbParameters = null;
            try
            {
                databaseLayer = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                dbParameters = new DBParameters();

                dbParameters.Add(new DBParameter("@UserID", UserID, ParameterDirection.Input, SqlDbType.BigInt));
                dbParameters.Add(new DBParameter("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt));

                databaseLayer.Connect(false);
                databaseLayer.Execute("Direct_SetPreferredProvider", dbParameters);
                databaseLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally 
            {
                if (databaseLayer != null)
                {
                    databaseLayer.Dispose();
                    databaseLayer = null;
                }
                if (dbParameters != null)
                {
                    dbParameters.Clear();
                    dbParameters.Dispose();
                    dbParameters = null;
                }

            }
        }

    }
}

