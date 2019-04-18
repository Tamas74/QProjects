using System;
using System.Collections.Generic;
using System.Text;
using gloEDocumentV3.DocumentContextMenu;
using gloEDocumentV3.Enumeration;
using System.IO;

namespace gloEDocumentV3
{
    namespace eDocManager
    {
        public static class eDocValidator
        {

            #region "Dhruv 2010 -> IsAcknowledged"
            
            public static bool IsAcknowledged(Int64 ContainerID, Int64 DocumentID, Int64 ClinicID)
            {
                bool _result = false;
                object _internalresult = null;
                
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString); 
               
                string _strSQL = "";
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            int _AckCount = 0;
                            _strSQL = "SELECT COUNT(eDocumentID) FROM eDocument_NTAO_V3 WITH(NOLOCK) WHERE eContainerID = " + ContainerID + " AND eDocumentID = " + DocumentID + " AND NTAOType = " + enum_NTAOType.Acknowledge.GetHashCode() + " AND ClinicID = " + ClinicID + " AND IsPage = 0 AND DocumentPageNumber = 0";
                            _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                            if (_internalresult != null)
                            {

                                if (_internalresult.GetType() != typeof(System.DBNull))
                                {
                                    if (_internalresult.ToString() != "")
                                    {
                                        _AckCount = Convert.ToInt32(_internalresult.ToString());
                                    }
                                }

                            }

                            if (_AckCount > 0)
                            {
                                _result = true;
                            }


                        }
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    _result = false;
                }
                finally
                {
                    if (_internalresult != null)
                    {
                        _internalresult = null;

                    }
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
                return _result;
            }
            #endregion "Dhruv 2010 -> IsAcknowledged"



            //sudhir 20081220
             #region "Dhruv 2010 -> IsAcknowledged"

            public static bool IsAcknowledged(Int64 DocumentID, Int64 ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                bool _result = false;
                object _internalresult = null;
               
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString); 
                string _strSQL = "";
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            int _AckCount = 0;

                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                            {
                                _strSQL = "SELECT COUNT(eDocumentID) FROM eDocument_NTAO_V3_RCM WITH(NOLOCK) WHERE eDocumentID = " + DocumentID + "  AND  NTAOType = " + enum_NTAOType.Acknowledge.GetHashCode() + " AND  ClinicID = " + ClinicID + " AND IsPage = 0 AND DocumentPageNumber = 0";
                            }
                            else
                            {
                                _strSQL = "SELECT COUNT(eDocumentID) FROM eDocument_NTAO_V3 WITH(NOLOCK) WHERE eDocumentID = " + DocumentID + "  AND  NTAOType = " + enum_NTAOType.Acknowledge.GetHashCode() + " AND  ClinicID = " + ClinicID + " AND IsPage = 0 AND DocumentPageNumber = 0";
                            }
                            
                            _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                            if (_internalresult != null)
                            {

                                if (_internalresult.GetType() != typeof(System.DBNull))
                                {
                                    if (_internalresult.ToString() != "")
                                    {
                                        _AckCount = Convert.ToInt32(_internalresult.ToString());
                                    }
                                }

                            }

                            if (_AckCount > 0)
                            {
                                _result = true;
                            }


                        }
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();


                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                    _result = false;
                }
                finally
                {
                    if (_internalresult != null)
                    {
                        _internalresult = null;

                    }

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
                return _result;
            }
             #endregion "Dhruv 2010 -> IsAcknowledged"


            #region "Dhruv 2010 -> IsDocumentCompressed"
            
            public static bool IsDocumentCompressed(Int64 PatientID, Int64 DocumentID, Int64 ClinicID)
            {
                
               
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString); 
                object _internalresult = null;
                bool _result = false;
                string _strSQL = "";

                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            _strSQL = " SELECT ISNULL(IsCompressed,'false') AS IsCompressed " +
                                      " FROM eDocument_Details_V3 WITH(NOLOCK)" +
                                      " WHERE PatientID = " + PatientID + " AND eDocumentID = " + DocumentID + " AND ClinicID = " + ClinicID + "";

                            _internalresult = oDB.ExecuteScalar_Query(_strSQL);

                            if (_internalresult != null)
                            {
                                _result = Convert.ToBoolean(_internalresult);
                            }


                        }
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();


                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                    _result = false;
                }
                finally
                {
                    if (_internalresult != null)
                    {
                        _internalresult = null;

                    }

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
                return _result;
            }
            #endregion "Dhruv 2010 -> IsDocumentCompressed"



            #region "Dhruv 2010 -> IsDocumentHasNote"

            
            public static bool IsDocumentHasNote(Int64 PatientID, Int64 DocumentID, Int64 ClinicID)
            {
                
                
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString); 
 
                object _internalresult = null;
                bool _result = false;
                string _strSQL = "";

                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            _strSQL = " SELECT ISNULL(HasNote,'false') AS HasNote " +
                                      " FROM eDocument_Details_V3 WITH(NOLOCK) " +
                                      " WHERE PatientID = " + PatientID + " AND eDocumentID = " + DocumentID + " AND ClinicID = " + ClinicID + "";

                            _internalresult = oDB.ExecuteScalar_Query(_strSQL);

                            if (_internalresult != null)
                            {
                                _result = Convert.ToBoolean(_internalresult);
                            }


                        }
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();


                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                    _result = false;
                }
                finally
                {
                    if (_internalresult != null)
                    {
                        _internalresult = null;

                    }

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
                return _result;
            }

            #endregion "Dhruv 2010 -> IsDocumentHasNote"

            #region "Dhruv 2010 -> GetNATOID"
            
            public static Int64 GetNATOID(Int64 DocumentID, Int64 ContainerID, enum_NTAOType Type, Int64 ClinicID)
            {
                object _internalresult = null;
                Int64 _NATOID = 0;
                
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString); 
                
                string _strSQL = "";
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            _strSQL = "SELECT NTAOID FROM eDocument_NTAO_V3 WITH(NOLOCK) WHERE eDocumentID = " + DocumentID + " AND eContainerID = " + ContainerID + " AND NTAOType = " + enum_NTAOType.Acknowledge.GetHashCode() + " AND ClinicID = " + ClinicID + " ";
                            _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                            if (_internalresult != null)
                            {
                                if (_internalresult.GetType() != typeof(System.DBNull))
                                {

                                    _NATOID = Convert.ToInt64(_internalresult);

                                }

                            }

                            return _NATOID;
                        }
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();


                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                }
                finally
                {
                    if (_internalresult != null)
                    {
                        _internalresult = null;

                    }

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
                return _NATOID;
            }
            #endregion "Dhruv 2010 -> GetNATOID"



            #region "Dhruv 2010 -> GetNATOID_Parameters"
            
            public static Int64 GetNATOID(Int64 DocumentID, Int64 ContainerID, Int32 ContainerPageNo, Int32 DocumentPageNo, enum_NTAOType Type, Int64 ClinicID)
            {
                object _internalresult = null;
                Int64 _NATOID = 0;
                
                Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString); 
                string _strSQL = "";
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            _strSQL = "SELECT NTAOID FROM eDocument_NTAO_V3 WITH(NOLOCK) WHERE eDocumentID = " + DocumentID + " AND eContainerID = " + ContainerID + " AND ContainerPageNumber = " + ContainerPageNo + " AND DocumentPageNumber = " + DocumentPageNo + " AND NTAOType = " + Type.GetHashCode() + " AND ClinicID = " + ClinicID + " ";
                            _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                            if (_internalresult != null)
                            {
                                if (_internalresult.GetType() != typeof(System.DBNull))
                                {

                                    _NATOID = Convert.ToInt64(_internalresult);

                                }

                            }


                            return _NATOID;
                        }
                    }

                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();


                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                }
                finally
                {
                    if (_internalresult != null)
                    {
                        _internalresult = null;

                    }

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
                return _NATOID;
            }
            #endregion "Dhruv 2010 -> GetNATOID_Parameters"


            #region "Dhruv 2010 -> GetNewDocumentName"


            public static string GetNewDocumentName(Int64 PatientID, string Category, Int64 ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                string _result = "";
                object _internalresult = null;
                
                Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString); 
                string _strSQL = "";
                try
                {
                    if (oDB != null)
                    {
                        oDB.Connect(false);
                        {

                            string _DocumentStartName = DateTime.Now.ToString("MM dd yyyy hh mm ss tt");
                            string _DocumentName = _DocumentStartName;
                            int i = 0;
                            Int64 _DocID = 0;
                            bool _DocNameFound = true;

                            while (_DocNameFound == true)
                            {
                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    _strSQL = "SELECT eDocumentID FROM eDocument_Details_V3_RCM WITH(NOLOCK) WHERE DocumentName = '" + _DocumentName + "' AND PatientID = " + PatientID + " and Category = '" + Category + "' and clinicid = " + ClinicID + "";
                                }
                                else
                                {
                                    _strSQL = "SELECT eDocumentID FROM eDocument_Details_V3 WITH(NOLOCK) WHERE DocumentName = '" + _DocumentName + "' AND PatientID = " + PatientID + " and Category = '" + Category + "' and clinicid = " + ClinicID + "";
                                }
                                
                                _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                                if (_internalresult != null)
                                {
                                    if (_internalresult.ToString() != null)
                                    {
                                        if (_internalresult.GetType() != typeof(System.DBNull))
                                        {
                                            if (_internalresult.ToString() != "")
                                            {
                                                _DocID = Convert.ToInt64(_internalresult.ToString());
                                                if (_DocID > 0)
                                                {
                                                    _DocNameFound = true;
                                                }
                                            }
                                            else
                                            {
                                                _DocNameFound = false;
                                                string _ErrorMessage = "Document not found. " + _DocumentName;
                                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                                                
                                            }
                                        }
                                        else
                                        {
                                            _DocNameFound = false;
                                            string _ErrorMessage = "Document not found. " + _DocumentName;
                                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                                            
                                        }
                                    }
                                    else
                                    {
                                        _DocNameFound = false;
                                        string _ErrorMessage = "Document not found. " + _DocumentName;
                                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                                        
                                    }
                                }
                                else
                                {
                                    _DocNameFound = false;
                                    string _ErrorMessage = "Document not found. " + _DocumentName;
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                                    
                                }
                                if (_DocNameFound == true)
                                {
                                    i++;

                                }

                                _DocID = 0;
                                if (i > 0)
                                {
                                    _DocumentName = _DocumentStartName + "-" + i.ToString();
                                }
                                else
                                {
                                    _DocumentName = _DocumentStartName;
                                    string _ErrorMessage = "Document name us not present. " + _DocumentName;
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                                    //return string.Empty;
                                }

                            }

                            _result = _DocumentName;
                        }
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();


                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }


                    _result = "";
                }
                finally
                {
                    if (_internalresult != null)
                    {
                        _internalresult = null;
                    }

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
                return _result;
            }


            #endregion "Dhruv 2010 -> GetNewDocumentName"



            #region "Dhruv 2010 -> GenerateDocumentName"
            public static string GenerateDocumentName(Int64 PatientID, string Category, Int64 ClinicID, string DocumentBaseName, int DocumentCount, int CurrentDocumentCounter, out int ReturnDocumentCounter)
            {
                string SubCategory = string.Empty;
                enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None;

                return GenerateDocumentName(PatientID, Category, SubCategory, ClinicID, DocumentBaseName, DocumentCount, CurrentDocumentCounter, out ReturnDocumentCounter, _OpenExternalSource);
            }

            public static string GenerateDocumentName(Int64 PatientID, string Category,string SubCategory, Int64 ClinicID, string DocumentBaseName, int DocumentCount, int CurrentDocumentCounter, out int ReturnDocumentCounter, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {

                string _result = "";
                object _internalresult = null;
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                string _strSQL = "";
                ReturnDocumentCounter = 0;
                string _DocumentStartName = DocumentBaseName;
                string _DocumentName = _DocumentStartName;
                int _DocumentCount = DocumentCount;
                int _CurrentDocumentCounter = CurrentDocumentCounter;


                try
                {
                    oDB.Connect(false);
                    {
                        #region "Generate Base Document Name"
                        if (_DocumentCount > 1)
                        {
                            if (_CurrentDocumentCounter == 0)
                            {
                                _CurrentDocumentCounter = _CurrentDocumentCounter + 1;
                            }
                            string _NextCntrName = "";
                            if (_DocumentCount > 99)
                            {
                                _NextCntrName = _CurrentDocumentCounter.ToString("#000");
                            }
                            else
                            {
                                _NextCntrName = _CurrentDocumentCounter.ToString("#00");
                            }
                            _DocumentName = _DocumentStartName + " - " + _NextCntrName;
                        }
                        else
                        {
                            _DocumentName = _DocumentStartName;
                            string _ErrorMessage = "Document name is not present." + _DocumentName;
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                            //return string.Empty;
                        }
                        #endregion

                        Int64 _DocID = 0;
                        bool _DocNameFound = true;

                        while (_DocNameFound == true)
                        {
                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                            {
                                _strSQL = "SELECT eDocumentID FROM eDocument_Details_V3_RCM WITH(NOLOCK) WHERE DocumentName = '" + _DocumentName.Replace("'", "''") + "' AND PatientID = " + PatientID + " and Category = '" + Category.Replace("'", "''") + "' AND SubCategory = '" + SubCategory.Replace("'", "''") + "' and clinicid = " + ClinicID + "";
                            }
                            else
                            {
                                _strSQL = "SELECT eDocumentID FROM eDocument_Details_V3 WITH(NOLOCK) WHERE DocumentName = '" + _DocumentName.Replace("'", "''") + "' AND PatientID = " + PatientID + " and Category = '" + Category.Replace("'", "''") + "' and clinicid = " + ClinicID + "";
                            }
                            
                            _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                            if (_internalresult != null)
                            {
                                if (_internalresult.ToString() != null)
                                {
                                    if (_internalresult.GetType() != typeof(System.DBNull))
                                    {
                                        if (_internalresult.ToString() != "")
                                        {
                                            _DocID = Convert.ToInt64(_internalresult.ToString());
                                            if (_DocID > 0)
                                            {
                                                _DocNameFound = true;
                                            }
                                        }
                                        else
                                        {
                                            _DocNameFound = false;
                                            string _ErrorMessage = "Document not Found." + _DocumentName;
                                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                                            
                                        }
                                    }
                                    else
                                    {
                                        _DocNameFound = false;
                                        string _ErrorMessage = "Document not Found." + _DocumentName;
                                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                                        
                                    }
                                }
                                else
                                {
                                    _DocNameFound = false;
                                    string _ErrorMessage = "Document not Found." + _DocumentName;
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                                    
                                }
                            }
                            else
                            {
                                _DocNameFound = false;
                                string _ErrorMessage = "Document not Found." + _DocumentName;
                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                                
                            }
                            if (_DocNameFound == true)
                            {


                                _DocID = 0;

                                #region "Generate Base Document Name"
                                _CurrentDocumentCounter = _CurrentDocumentCounter + 1;
                                string _NextCntrName = "";
                                if (_CurrentDocumentCounter <= 0)
                                {
                                    string _ErrorMessage = "Current document not present" + _DocumentName;
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                                    
                                }
                                if (_DocumentCount > 99)
                                {
                                    _NextCntrName = _CurrentDocumentCounter.ToString("#000");
                                }
                                else
                                {
                                    _NextCntrName = _CurrentDocumentCounter.ToString("#00");
                                }
                                _DocumentName = _DocumentStartName + " - " + _NextCntrName;
                                #endregion
                            }
                            if (_internalresult != null)
                            {
                                _internalresult = null;
                            }
                        }

                        _result = _DocumentName;
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();


                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }


                    _result = "";
                }
                finally
                {
                    if (_internalresult != null)
                    {
                        _internalresult = null;
                    }


                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }

                }
                if (_CurrentDocumentCounter > 0)
                {
                    ReturnDocumentCounter = _CurrentDocumentCounter + 1;
                }
                else
                {
                    ReturnDocumentCounter = _CurrentDocumentCounter;
                    string _ErrorMessage = "Current document not present." + _DocumentName;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                    
                }
                return _result;
            }
            #endregion "Dhruv 2010 -> GenerateDocumentName"



            #region "Dhruv 2010 -> GetNewDocumentName"

            
            public static string GetNewDocumentName(string DirectoryFullPath, string DocumentExtension)
            {
                string _result = "";
                try
                {
                    string _DocumentStartName = DateTime.Now.ToString("MM dd yyyy hh mm ss tt");
                    string _DocumentName = DirectoryFullPath + "\\" + _DocumentStartName + "." + DocumentExtension;
                    int i = 0;
                    bool _DocNameFound = true;

                    while (_DocNameFound == true)
                    {

                        _DocNameFound = File.Exists(_DocumentName);
                        i++;
                        _DocumentName = DirectoryFullPath + "\\" + _DocumentStartName + "-" + i.ToString() + "." + DocumentExtension;
                        if (i == 0)
                        {
                            string _ErrorMessage = "Document Found is wrong. " + _DocumentName;


                            //Make Log entry in DMSExceptionLog file for any exceptions found
                            if (_ErrorMessage.Trim() != "")
                            {
                                string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                                _MessageString = "";
                                return _result;
                            }
                           
                        }
                    }
                    _result = _DocumentName;
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();


                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }



                    _result = "";
                }

                return _result;
            }


            #endregion "Dhruv 2010 -> GetNewDocumentName"


            #region "Dhruv 2010 -> GetNewDocumentName"

            
            public static string GetNewDocumentName(string DirectoryFullPath, string PostFixName, string DocumentExtension)
            {
                string _result = "";
                try
                {
                    string _DocumentStartName = DateTime.Now.ToString("MM dd yyyy hh mm ss tt");
                    string _DocumentName = DirectoryFullPath + "\\" + _DocumentStartName + "_" + PostFixName + "." + DocumentExtension;
                    int i = 0;
                    bool _DocNameFound = true;

                    while (_DocNameFound == true)
                    {
                        _DocNameFound = File.Exists(_DocumentName);
                        i++;
                        _DocumentName = DirectoryFullPath + "\\" + _DocumentStartName + "-" + i.ToString() + "_" + PostFixName + "." + DocumentExtension;
                        if (i == 0)
                        {
                            string _ErrorMessage = "Document not Found." + _DocumentName;
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                            return _result;
                        }
                        
                    }
                    _result = _DocumentName;
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();


                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    _result = "";
                }

                return _result;
            }

            #endregion "Dhruv 2010 -> GetNewDocumentName"


            #region "Dhruv 2010 -> GetMonthName"

            
            public static string GetMonthName(int monthno)
            {
                string[] _result = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

                if ((monthno > 0) && (monthno <= 12))
                {
                    return _result[monthno - 1];
                }
                else
                {
                    return string.Empty;
                }
            }

            #endregion "Dhruv 2010 -> GetMonthName"


            #region "Dhruv 2010 -> GetPrefixTransactionID"

            
            public static Int64 GetPrefixTransactionID(Int64 PatientID)
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
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
                string _strSQL = "";
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            _strSQL = "SELECT dtDOB FROM Patient WITH(NOLOCK) WHERE nPatientID = " + PatientID + "";
                            _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                            if (!string.IsNullOrEmpty(Convert.ToString(_internalresult)))
                            {

                                if (_internalresult.GetType() != typeof(System.DBNull))
                                {
                                    
                                        _PatientDOB = Convert.ToDateTime(_internalresult);
                                    
                                }

                            }
                        }

                        _result = "";

                        {
                            oTS = _CurrentDate.Subtract(_BaseDate);
                            //oTS = new TimeSpan(_CurrentDate.Ticks);
                            strID1 = oTS.Days.ToString().Replace("-", "");
                        }


                        oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                        strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                        oTS = _PatientDOB.Subtract(_BaseDate);
                        strID3 = oTS.Days.ToString().Replace("-", "");


                        _result = strID1 + strID2 + strID3;

                        _Result = Convert.ToInt64(_result);
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();

                    //Code added on 4rd Octomber 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add

                    return 0;
                }
                finally
                {
                    if (_internalresult != null)
                    {
                        _internalresult = null;
                    }
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }

                }
                return _Result;
            }

            #endregion "Dhruv 2010 -> GetPrefixTransactionID"


            //Pramod
            #region "Dhruv 2010 -> GetUserID"
            
            public static long GetUserID(string UserName, long ClinicID)
            {
                Int64 _Result = 0;
                object _internalresult = null;
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
                string _strSQL = "";
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            _strSQL = "SELECT nUserID FROM User_MST WITH(NOLOCK) where sLoginName = '" + UserName.Replace("'", "''") + "'";
                            _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                            if (_internalresult != null)
                            {

                                if (_internalresult.GetType() != typeof(System.DBNull))
                                {
                                    
                                        _Result = Convert.ToInt64(_internalresult.ToString());
                                    
                                }

                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                    _Result = 0;
                }
                finally
                {
                    if (_internalresult != null)
                    {
                        _internalresult = null;
                    }
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
                return _Result;
            }

            #endregion "Dhruv 2010 -> GetUserID"

            #region "Dhruv 2010 -> GetFilePageCount"
            
            public static int GetFilePageCount(string FilePath)
            {
                pdftron.PDF.PDFDoc oPDFDoc = null;
                int _DocumentPageCount = 0;

                try
                {
                    if (File.Exists(FilePath) == true)
                    {
                        using (oPDFDoc = new pdftron.PDF.PDFDoc(FilePath))
                        {

                            if (oPDFDoc != null)
                            {
                                _DocumentPageCount = oPDFDoc.GetPageCount();
                                oPDFDoc.Close();

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                }
                finally
                {
                    if (oPDFDoc != null)
                    {
                        oPDFDoc.Dispose();
                        oPDFDoc = null;
                    }
                }
                return _DocumentPageCount;
            }

            #endregion "Dhruv 2010 -> GetFilePageCount"


            #region "Dhruv 2010 -> GetUserName"
            
            public static string GetUserName(long UserID, long ClinicID)
            {
                string _Result = "";
                object _internalresult = null;
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
                string _strSQL = "";
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            _strSQL = "SELECT sLoginName,ISNULL(sFirstName,'')+ISNULL(sMiddleName,'')+ISNULL(sLastName,'') AS UserName "
                                     + "FROM User_MST WITH(NOLOCK) WHERE nUserID  = " + UserID + "";

                            _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                            if (_internalresult != null)
                            {

                                if (_internalresult.GetType() != typeof(System.DBNull))
                                {
                                    
                                        _Result = _internalresult.ToString();
                                    
                                }

                            }


                        }
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();

                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                    _Result = "";
                }
                finally
                {
                    if (_internalresult != null)
                    {
                        _internalresult = null;
                    }
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
                return _Result;
            }
            #endregion "Dhruv 2010 -> GetUserName"


            #region "Dhruv 2010 -> IsAcknowledged"
            public static void UpdateExceptionLog_Old(string LogString)
            {
                string _logFilePath = "";
                try
                {
                    _logFilePath = gloEDocV3Admin.gErrorLogFilePath;
                    StreamWriter oStreamWriter = new StreamWriter(_logFilePath, true);
                    oStreamWriter.WriteLine(LogString);
                    oStreamWriter.Close();
                    oStreamWriter.Dispose();
                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();

                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                }
            }
            public static void UpdateExceptionLog(string LogString)
            {
                string _logFilePath = "";
                try
                {
                    _logFilePath = gloEDocV3Admin.gErrorLogFilePath;
                    using (StreamWriter oStreamWriter = new StreamWriter(_logFilePath, true))
                    {
                        if (oStreamWriter != null)
                        {
                            oStreamWriter.WriteLine(LogString);
                            oStreamWriter.Close();
                        }

                    }
                }
                catch (Exception ex)
                {
                    string _ErrorMessage = "Unable to write in Logfile" + ex.ToString();
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                }
            }
            #endregion "Dhruv 2010 -> IsAcknowledged"


            //sudhir 20081517
            #region "Dhruv 2010 -> IsCategoryExists"

            public static bool IsCategoryExists_old(int CategoryId, string CategoryName, Int64 ClinicId)
            {
               
                Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString); 
                bool _result = false;
                string _strQuery = "";

                try
                {
                    if (oDB.Connect(false))
                    {
                        if (CategoryId == 0)
                        {
                            _strQuery = "SELECT COUNT(*) FROM eDocument_Category_V3 WITH(NOLOCK) WHERE UPPER(CategoryName) ='" + CategoryName.ToUpper() + "' AND ClinicID=" + ClinicId + "";
                        }
                        else if (CategoryId > 0)
                        {
                            _strQuery = "SELECT COUNT(*) FROM eDocument_Category_V3 WITH(NOLOCK) WHERE UPPER(CategoryName) ='" + CategoryName.ToUpper() + "' AND ClinicID=" + ClinicId + " AND CategoryId <> " + CategoryId + "";
                        }
                        Object _value = oDB.ExecuteScalar_Query(_strQuery);
                        if (Convert.ToInt64(_value) > 0)
                        {
                            _result = true;
                        }
                        else
                        {
                            _result = false;
                        }

                        _value = null;
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();

                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add

                    _result = false;
                }
                finally
                {



                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }

                    if (_strQuery != "")
                    {
                        _strQuery = null;
                    }

                }

                return _result;
            }
            public static bool IsCategoryExists(int CategoryId, string CategoryName, Int64 ClinicId, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
               
                Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString); 
                bool _result = false;
                string _strQuery = "";

                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            if (CategoryId == 0)
                            {
                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    _strQuery = "SELECT COUNT(*) FROM eDocument_Category_V3_RCM WITH(NOLOCK) WHERE UPPER(CategoryName) ='" + CategoryName.ToUpper() + "' AND ClinicID=" + ClinicId + "";
                                }
                                else
                                {
                                    _strQuery = "SELECT COUNT(*) FROM eDocument_Category_V3 WITH(NOLOCK) WHERE UPPER(CategoryName) ='" + CategoryName.ToUpper() + "' AND ClinicID=" + ClinicId + "";
                                }
                            }
                            else if (CategoryId > 0)
                            {
                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    _strQuery = "SELECT COUNT(*) FROM eDocument_Category_V3_RCM WITH(NOLOCK) WHERE UPPER(CategoryName) ='" + CategoryName.ToUpper() + "' AND ClinicID=" + ClinicId + " AND CategoryId <> " + CategoryId + "";
                                }
                                else
                                {
                                    _strQuery = "SELECT COUNT(*) FROM eDocument_Category_V3 WITH(NOLOCK) WHERE UPPER(CategoryName) ='" + CategoryName.ToUpper() + "' AND ClinicID=" + ClinicId + " AND CategoryId <> " + CategoryId + "";
                                }
                            }
                            Object _value = oDB.ExecuteScalar_Query(_strQuery);
                            if (Convert.ToInt64(_value) > 0)
                            {
                                _result = true;
                            }
                            else
                            {
                                _result = false;
                            }
                            _value = null;
                        }
                    }

                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();

                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add

                    _result = false;
                }
                finally
                {


                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }


                }

                return _result;
            }

            #endregion "Dhruv 2010 -> IsCategoryExists"

            #region "Dhruv -> Is Categroy present or not in the Category table"
            public static bool IsCategoryPresent(Int64 ClinicId, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                
                Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                bool _result = false;
                string _strQuery = "";

                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                            {
                                _strQuery = "SELECT COUNT(*) FROM eDocument_Category_V3_RCM WITH(NOLOCK) WHERE  ClinicID=" + ClinicId + "";
                            }
                            else
                            {
                                _strQuery = "SELECT COUNT(*) FROM eDocument_Category_V3 WITH(NOLOCK) WHERE  ClinicID=" + ClinicId + "";
                            }

                            Object _value = oDB.ExecuteScalar_Query(_strQuery);
                            if (Convert.ToInt64(_value) > 0)
                            {
                                _result = true;
                            }
                            else
                            {
                                _result = false;
                            }
                            _value = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();

                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add

                    _result = false;
                }
                finally
                {

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }

                    if (_strQuery != "")
                        _strQuery = null;

                }

                return _result;
            }
            #endregion


            #region "Dhruv 2010 -> IsUserAdministrator"

            
            public static bool IsUserAdministrator(long UserID, long ClinicID)
            {
                bool _Result = false;
                object _internalresult = null;
                Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
                string _strSQL = "";
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            _strSQL = "SELECT nUserID FROM User_MST WITH(NOLOCK) where nUserID = " + UserID + " AND nAdministrator = 1";
                            _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                            if (_internalresult != null)
                            {

                                if (_internalresult.GetType() != typeof(System.DBNull))
                                {
                                    
                                        _Result = true;
                                    
                                }

                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();

                    //Code added on 4rd Octomber 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add

                    _Result = false;
                }
                finally
                {
                    if (_internalresult != null)
                    {
                        _internalresult = null;
                    }

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
                return _Result;
            }

            #endregion "Dhruv 2010 -> IsUserAdministrator"


            #region "Dhruv 2010 -> IsDocumentNameExists"

            public static bool IsDocumentNameExists_Old(string DocumentName, Int64 PatientID, string Category, Int64 ClinicID)
            {
                
                Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                bool _result = false;
                string _strQuery = "";

                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            _strQuery = "SELECT COUNT(*) FROM eDocument_Details_V3 WITH(NOLOCK) WHERE UPPER(DocumentName) ='" + DocumentName.ToUpper() + "' AND ClinicID=" + ClinicID + " AND UPPER(Category) = '" + Category.ToUpper() + "' AND PatientID=" + PatientID + "";
                            Object _value = oDB.ExecuteScalar_Query(_strQuery);
                            if (Convert.ToInt64(_value) > 0)
                            {
                                _result = true;
                            }
                            else
                            {
                                _result = false;
                            }
                            _value = null;
                        }
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();

                    //Code added on 4rd Octomber 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add

                    _result = false;
                }
                finally
                {
                    
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }

                    if (_strQuery != "")
                        _strQuery = null;

                }

                return _result;
            }
            public static bool IsDocumentNameExists(string DocumentName, Int64 PatientID, string Category,string SubCategory, Int64 ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                
                Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                bool _result = false;
                string _strQuery = "";

                try
                {
                    if (oDB != null)
                    {

                        if (oDB.Connect(false))
                        {
                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                            {
                                _strQuery = "SELECT COUNT(*) FROM eDocument_Details_V3_RCM WITH(NOLOCK) WHERE UPPER(DocumentName) ='" + DocumentName.ToUpper() + "' AND ClinicID=" + ClinicID + " AND UPPER(Category) = '" + Category.ToUpper() + "' AND UPPER(SubCategory) = '" + SubCategory.ToUpper() + "' AND PatientID=" + PatientID + "";
                            }
                            else
                            {
                                _strQuery = "SELECT COUNT(*) FROM eDocument_Details_V3 WITH(NOLOCK) WHERE UPPER(DocumentName) ='" + DocumentName.ToUpper() + "' AND ClinicID=" + ClinicID + " AND UPPER(Category) = '" + Category.ToUpper() + "' AND PatientID=" + PatientID + "";
                            }
                            
                            Object _value = oDB.ExecuteScalar_Query(_strQuery);
                            if (Convert.ToInt64(_value) > 0)
                            {
                                _result = true;
                            }
                            else
                            {
                                _result = false;
                            }
                            _value = null;

                        }
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();


                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }



                    _result = false;
                }
                finally
                {

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }

                }

                return _result;
            }
            public static bool IsDocumentNameExists(string DocumentName, Int64 PatientID, string Category, Int64 ClinicID)
            {

                Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                bool _result = false;
                string _strQuery = "";

                try
                {
                    if (oDB != null)
                    {

                        if (oDB.Connect(false))
                        {

                            _strQuery = "SELECT COUNT(*) FROM eDocument_Details_V3 WITH(NOLOCK) WHERE UPPER(DocumentName) ='" + DocumentName.ToUpper() + "' AND ClinicID=" + ClinicID + " AND UPPER(Category) = '" + Category.ToUpper() + "' AND PatientID=" + PatientID + "";







                            Object _value = oDB.ExecuteScalar_Query(_strQuery);
                            if (Convert.ToInt64(_value) > 0)
                            {
                                _result = true;
                            }
                            else
                            {
                                _result = false;
                            }
                            _value = null;

                        }
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();


                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }



                    _result = false;
                }
                finally
                {

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }

                }

                return _result;
            }


            #endregion "Dhruv 2010 -> IsDocumentNameExists"


            #region "Dhruv 2010 -> IsPageNameExists"


            public static bool IsPageNameExists(string PageName, Int64 DocumentID, Int64 ClinicID, Int32 ExceptPageID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                
                Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                
                bool _result = false;
                string _strQuery = "";

                try
                {
                    if (oDB != null)
                    {

                        if (oDB.Connect(false))
                        {
                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                            {
                                _strQuery = "SELECT COUNT(*) FROM eDocument_Pages_V3_RCM WITH(NOLOCK) WHERE UPPER(PageName) ='" + PageName.ToUpper() + "' AND ClinicID=" + ClinicID + " AND eDocumentID =" + DocumentID + " AND DocumentPageNumber <> " + ExceptPageID + "";
                            }
                            else
                            {
                                _strQuery = "SELECT COUNT(*) FROM eDocument_Pages_V3 WITH(NOLOCK) WHERE UPPER(PageName) ='" + PageName.ToUpper() + "' AND ClinicID=" + ClinicID + " AND eDocumentID =" + DocumentID + " AND DocumentPageNumber <> " + ExceptPageID + "";
                            }
                            
                            Object _value = oDB.ExecuteScalar_Query(_strQuery);
                            if (Convert.ToInt64(_value) > 0)
                            {
                                _result = true;
                            }
                            else
                            {
                                _result = false;
                            }
                            _value = null;

                        }
                    }
                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();


                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    _result = false;
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }

                }

                return _result;
            }

            #endregion "Dhruv 2010 -> IsPageNameExists"


            #region "Dhruv 2010 -> GetCategoryId"

            
            public static Int64 GetCategoryId(string CategoryName, Int64 ClinicId)
            {
                
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                string _sqlQuery = "";
                Int64 _CategoryId = 0;

                try
                {
                    if (oDB != null)
                    {

                        if (oDB.Connect(false))
                        {

                            _sqlQuery = " SELECT ISNULL(CategoryId,0) AS CategoryId FROM eDocument_Category_V3 WITH(NOLOCK) " +
                                " WHERE  UPPER(CategoryName) = '" + CategoryName.Trim().ToUpper() + "' AND ClinicID = " + ClinicId + "";

                            Object RetVal = oDB.ExecuteScalar_Query(_sqlQuery);
                            if (RetVal != null && RetVal != DBNull.Value && Convert.ToString(RetVal) != "")
                            {
                                _CategoryId = Convert.ToInt64(RetVal);
                            }
                            if (RetVal != null)
                            {
                                RetVal = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();

                    #region " Make Exception log entry "

                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }


                    #endregion                    _result = false;

                }
                finally
                {

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
                return _CategoryId;
            }

            #endregion "Dhruv 2010 -> GetCategoryId"

            public static string GetDocumentName_Immunization(Int64 DocumentID)
            {
                string DocumentName = "";
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                string _sqlQuery = "";
                try
                {
                    if (oDB != null)
                    {

                        if (oDB.Connect(false))
                        {

                            _sqlQuery = " SELECT ISNULL(DocumentName,'') AS DocumentName FROM eDocument_Details_V3 " +
                                " WHERE eDocumentID = " + DocumentID + "";

                            Object RetVal = oDB.ExecuteScalar_Query(_sqlQuery);
                            if (RetVal != null && RetVal != DBNull.Value && Convert.ToString(RetVal) != "")
                            {
                                DocumentName = Convert.ToString(RetVal);
                            }
                            if (RetVal != null)
                            {
                                RetVal = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();

                    #region " Make Exception log entry "

                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }


                    #endregion                    _result = false;

                }
                finally
                {

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                }
                return DocumentName;
            }
          
            public static bool IsDocumentExist_Immunization(Int64 DocumentID, Int64 PatientID, Int64 ClinicID)
            {

                Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                bool _result = false;
                string _strQuery = "";

                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            _strQuery = "SELECT COUNT(*) FROM eDocument_Details_V3 WITH(NOLOCK) WHERE eDocumentID =" + DocumentID + " AND CategoryID=176 AND ClinicID=" + ClinicID + " AND PatientID=" + PatientID + "";
                            Object _value = oDB.ExecuteScalar_Query(_strQuery);
                            if (Convert.ToInt64(_value) > 0)
                            {
                                _result = true;
                            }
                            else
                            {
                                _result = false;
                            }
                            _value = null;

                        }
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();


                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }



                    _result = false;
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }

                }

                return _result;
            }
            #region "Dhruv 2010 -> IsAcknowledged"


            public static bool IsDocumentExist(Int64 DocumentID, Int64 PatientID, Int64 ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                
                Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                bool _result = false;
                string _strQuery = "";

                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                            {
                                _strQuery = "SELECT COUNT(*) FROM eDocument_Details_V3_RCM WITH(NOLOCK) WHERE eDocumentID =" + DocumentID + " AND ClinicID=" + ClinicID + " AND PatientID=" + PatientID + "";
                            }
                            else
                            {
                                _strQuery = "SELECT COUNT(*) FROM eDocument_Details_V3 WITH(NOLOCK) WHERE eDocumentID =" + DocumentID + " AND ClinicID=" + ClinicID + " AND PatientID=" + PatientID + "";
                            }
                            
                            Object _value = oDB.ExecuteScalar_Query(_strQuery);
                            if (Convert.ToInt64(_value) > 0)
                            {
                                _result = true;
                            }
                            else
                            {
                                _result = false;
                            }
                            _value = null;

                        }
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorMessage = ex.ToString();


                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }



                    _result = false;
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }

                }

                return _result;
            }

            #endregion "Dhruv 2010 -> IsAcknowledged"

        }
    }
}
