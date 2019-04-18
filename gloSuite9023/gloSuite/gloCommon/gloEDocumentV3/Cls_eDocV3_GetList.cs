using System;
using gloEDocumentV3.Enumeration;
using gloEDocumentV3.DocumentContextMenu;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlTypes;

namespace gloEDocumentV3
{
    namespace eDocManager
    {
        public partial class eDocGetList : IDisposable
        {
            static string _ErrorMessage = "";
            static bool _HasError = false;

            #region "Constructor & Distructor"

            public eDocGetList()
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

            ~eDocGetList()
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

            private void ErrorMessagees(string _ErrorMessage)
            {
                #region " Make Log Entry "
                try
                {
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorHere = ex.ToString();
                    MessageBox.Show("Unable to update Log with " + _ErrorMessage, _ErrorHere);
                }

                //End Code add
                #endregion " Make Log Entry "

            }

            #region "SubCategory"
            //SubCategories
            public gloEDocumentV3.Common.SubCategories GetSubCategories(long ClinicID, Int64 CategoryId, string Year, bool IsAcknowledged)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Common.SubCategories oSubCategories = new gloEDocumentV3.Common.SubCategories();
                DataTable oDataTable = null;
                string _strSQL = "";

                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {

                                //_strSQL = " SELECT DISTINCT SubCategory FROM eDocument_Details_V3_RCM WHERE CategoryID = " + CategoryId + " AND ClinicID=" + ClinicID + " AND Year='" + Year + "'";
                                if (!IsAcknowledged)
                                {
                                    _strSQL = " SELECT DISTINCT SubCategory FROM eDocument_Details_V3_RCM WHERE CategoryID = " + CategoryId + " AND ClinicID=" + ClinicID + " AND Year='" + Year + "' AND ISNULL(eDocument_Details_V3_RCM.IsAcknowledge,0) = " + IsAcknowledged.GetHashCode();
                                }
                                else 
                                {
                                    _strSQL = " SELECT DISTINCT SubCategory FROM eDocument_Details_V3_RCM WHERE CategoryID = " + CategoryId + " AND ClinicID=" + ClinicID + " AND Year='" + Year + "'";
                                }
                                

                                oDB.Retrive_Query(_strSQL, out oDataTable);
                                if (oDataTable != null)
                                {
                                    if (oDataTable.Rows.Count > 0)
                                    {
                                        if (oSubCategories != null)
                                        {
                                            for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                            {
                                                oSubCategories.Add(oDataTable.Rows[i]["SubCategory"].ToString());
                                            }
                                        }
                                    }
                                }
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }
                finally
                {
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                }
                return oSubCategories;

            }

            #endregion

            #region "Category"

            #region "Dhruv 2010 -> UpdateContainer"
            public gloEDocumentV3.Common.Categories GetCategories(long ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Common.Categories oCategories = new gloEDocumentV3.Common.Categories();
                DataTable oDataTable = null;
                string _strSQL = "";

                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    _strSQL = "SELECT CategoryId,CategoryName FROM eDocument_Category_V3_RCM WITH(NOLOCK) WHERE ClinicID = " + ClinicID + " AND CategoryId IS NOT NULL AND CategoryName IS NOT NULL ORDER BY CategoryName ";
                                }
                                else
                                {
                                    _strSQL = "SELECT CategoryId,CategoryName FROM eDocument_Category_V3 WITH(NOLOCK) WHERE ClinicID = " + ClinicID + " AND CategoryId IS NOT NULL AND CategoryName IS NOT NULL ORDER BY CategoryName ";
                                }

                                oDB.Retrive_Query(_strSQL, out oDataTable);
                                if (oDataTable != null)
                                {
                                    if (oDataTable.Rows.Count > 0)
                                    {
                                        if (oCategories != null)
                                        {
                                            for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                            {
                                                oCategories.Add(Convert.ToInt32(oDataTable.Rows[i]["CategoryId"].ToString()), oDataTable.Rows[i]["CategoryName"].ToString());
                                            }
                                        }
                                    }
                                }
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }
                finally
                {
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                }
                return oCategories;
            }
            #endregion "Dhruv 2010 -> UpdateContainer"

            #region "Dhruv 2010 -> GetCategories"
            public gloEDocumentV3.Common.Categories GetCategories(long ClinicID, out DataTable oDataTable, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Common.Categories oCategories = new gloEDocumentV3.Common.Categories();
                oDataTable = null;
                string _strSQL = "";

                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    _strSQL = "SELECT CategoryId,CategoryName FROM eDocument_Category_V3_RCM WITH(NOLOCK) WHERE ClinicID = " + ClinicID + " AND CategoryId IS NOT NULL AND CategoryName IS NOT NULL ORDER BY CategoryName ";
                                }
                                else
                                {
                                    _strSQL = "SELECT CategoryId,CategoryName FROM eDocument_Category_V3 WITH(NOLOCK) WHERE ClinicID = " + ClinicID + " AND CategoryId IS NOT NULL AND CategoryName IS NOT NULL ORDER BY CategoryName ";
                                }

                                oDB.Retrive_Query(_strSQL, out oDataTable);
                                if (oDataTable != null)
                                {
                                    if (oDataTable.Rows.Count > 0)
                                    {
                                        for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                        {
                                            oCategories.Add(Convert.ToInt32(oDataTable.Rows[i]["CategoryId"].ToString()), oDataTable.Rows[i]["CategoryName"].ToString());
                                        }
                                    }
                                }
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }
                finally
                {

                }
                return oCategories;
            }
            #endregion "Dhruv 2010 -> GetCategories"

            #region "Dhruv 2010 -> GetAdvanceDirectiveCateory"
            public string GetAdvanceDirectiveCateory()
            {
                _ErrorMessage = "";
                _HasError = false;
                string _result = "";
                // gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString);
                string _strSQL = "";
                object _intresult = null;
                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString))
                    {

                        if (oDB.Connect(false))
                        {
                            _strSQL = "SELECT ISNULL(sSettingsValue,'') FROM Settings WITH(NOLOCK) WHERE UPPER(sSettingsName) = '" + Convert.ToString("OMR Category - Directive").ToUpper() + "' AND sSettingsValue IS NOT NULL";
                            _intresult = oDB.ExecuteScalar_Query(_strSQL);
                            if (_intresult != null && _intresult.ToString() != "")
                            {
                                _result = _intresult.ToString();
                            }
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }

                return _result;
            }
            #endregion "Dhruv 2010 -> GetAdvanceDirectiveCateory"

            public string GetRCMtoDMSCategory()
            {
                _ErrorMessage = "";
                _HasError = false;
                string _result = "";
                string _strSQL = "";
                object _intresult = null;
                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                _strSQL = "SELECT ISNULL(sSettingsValue,'') FROM Settings WITH(NOLOCK) WHERE UPPER(sSettingsName) = '" + Convert.ToString("RCM To DMS Category").ToUpper() + "' AND sSettingsValue IS NOT NULL";
                                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                                if (_intresult != null && _intresult.ToString() != "")
                                {
                                    _result = _intresult.ToString();
                                }
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                  
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    
                    #endregion " Make Log Entry "
                }

                return _result;
            }

            #region "Dhruv 2010 -> GetReceivedFaxCategory"
            public string GetReceivedFaxCategory()
            {
                _ErrorMessage = "";
                _HasError = false;
                string _result = "";
                //gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString);
                string _strSQL = "";
                object _intresult = null;
                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                _strSQL = "SELECT ISNULL(sSettingsValue,'') FROM Settings WITH(NOLOCK) WHERE UPPER(sSettingsName) = '" + Convert.ToString("OMR Category - Fax").ToUpper() + "' AND sSettingsValue IS NOT NULL";
                                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                                if (_intresult != null && _intresult.ToString() != "")
                                {
                                    _result = _intresult.ToString();
                                }
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }

                return _result;
            }
            #endregion "Dhruv 2010 -> GetReceivedFaxCategory"
            #endregion
            #region "Dhruv 2010 -> GetReceivedFaxCategory"
            public string GetDirectMsgAttachmentCategory()
            {
                _ErrorMessage = "";
                _HasError = false;
                string _result = "";
                //gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString);
                string _strSQL = "";
                object _intresult = null;
                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                _strSQL = "SELECT ISNULL(sSettingsValue,'') FROM Settings WITH(NOLOCK) WHERE UPPER(sSettingsName) = '" + Convert.ToString("DIRECT MESSAGE ATTACHMENT CATEGORY").ToUpper() + "' AND sSettingsValue IS NOT NULL";
                                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                                if (_intresult != null && _intresult.ToString() != "")
                                {
                                    _result = _intresult.ToString();
                                }
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }

                return _result;
            }
            #endregion "Dhruv 2010 -> GetReceivedFaxCategory"
            public string InUpImmunizationSettings()
            {
                _ErrorMessage = "";
                _HasError = false;
                string _result = "";
                string _strSQL = "";
                object _intresult = null;
                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString))
                    {
                        if (oDB.Connect(false))
                        {
                            #region InupSetting

                            _strSQL = "select nSettingsID from Settings where sSettingsName='VISCATEGORY'";
                            _intresult = oDB.ExecuteScalar_Query(_strSQL);
                            if (_intresult != null && _intresult.ToString() != "")
                            {
                                _strSQL = "update Settings set sSettingsValue='Vaccine Information Statements' where sSettingsName='VISCATEGORY' ";
                                _intresult = oDB.Execute_Query(_strSQL);
                                if (Convert.ToInt16(_intresult) > 0)
                                    _result = "Vaccine Information Statements";
                                else
                                    _result = "";
                            }
                            else
                            {
                                if (oDB != null)
                                {
                                    using (gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters())
                                    {
                                        if (oDBParams != null)
                                        {
                                            oDBParams.Clear();
                                            oDBParams.Add("@sSettingsName", "VISCATEGORY", ParameterDirection.Input, SqlDbType.VarChar);
                                            oDBParams.Add("@sSettingsValue", "", ParameterDirection.Input, SqlDbType.VarChar);
                                            oDBParams.Add("@nClinicID", 1, ParameterDirection.Input, SqlDbType.VarChar);
                                            oDBParams.Add("@nUserID", 0, ParameterDirection.Input, SqlDbType.VarChar);
                                            oDBParams.Add("@nUserClinicFlag", 2, ParameterDirection.Input, SqlDbType.BigInt);
                                            int _ret = oDB.Execute("gsp_InUpSettings", oDBParams, 0);

                                            if (_ret <= 0)
                                                _intresult = "";
                                            else
                                                _intresult = "Vaccine Information Statements";
                                        }
                                    }
                                }
                            }
                            #endregion

                            //}
                            if (oDB != null)
                                oDB.Disconnect();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    #region " Make Log Entry "
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                    #endregion " Make Log Entry "
                }
                return _result;
            }
            public string GetImmunizationCateory()
            {
                _ErrorMessage = "";
                _HasError = false;
                string _result = "";
                // gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString);
                string _strSQL = "";
                object _intresult = null;
                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString))
                    {
                        if (oDB.Connect(false))
                        {
                            _strSQL = "SELECT ISNULL(sSettingsValue,'') FROM Settings WITH(NOLOCK) WHERE UPPER(sSettingsName) = '" + Convert.ToString("VISCATEGORY").ToUpper() + "' AND sSettingsValue IS NOT NULL";
                            _intresult = oDB.ExecuteScalar_Query(_strSQL);

                            if (_intresult != null && _intresult.ToString() != "")
                                _result = _intresult.ToString();
                            else
                                _result = InUpImmunizationSettings();

                            if (oDB != null)
                                oDB.Disconnect();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }

                return _result;
            }
            #region "Document Information"
            #region "Dhruv 2010 -> GetYearOfDocuments"
            public int GetYearOfDocuments(Int64 PatientID, Int64 ClinicID, Int64 DocumentID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                string _strSQL = "";
                int _result = DateTime.Now.Year;
                object _intresult = null;
                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    _strSQL = "SELECT Year FROM eDocument_Details_V3_RCM WITH(NOLOCK) WHERE PatientID = " + PatientID + " AND ClinicID = " + ClinicID + "AND eDocumentID = " + DocumentID + " ";
                                }
                                else
                                {
                                    _strSQL = "SELECT Year FROM eDocument_Details_V3 WITH(NOLOCK) WHERE PatientID = " + PatientID + " AND ClinicID = " + ClinicID + "AND eDocumentID = " + DocumentID + " ";
                                }


                                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                                if (_intresult != null && _intresult.ToString() != "")
                                {
                                    _result = Convert.ToInt32(_intresult.ToString());
                                }
                                _intresult = null;
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }

                return _result;
            }
            #endregion "Dhruv 2010 -> GetYearOfDocuments"

            #region "Dhruv 2010 -> GetLastYearOfDocuments"
            public int GetLastYearOfDocuments(Int64 PatientID, Int64 ClinicID, bool GetMinimum, bool GetDefaultMin, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                string _strSQL = "";
                int _result = DateTime.Now.Year;
                object _intresult = null;
                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                if (GetMinimum == true)
                                {
                                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                    {
                                        _strSQL = "SELECT MIN(Convert(int,Year)) FROM eDocument_Details_V3_RCM WITH(NOLOCK) WHERE ClinicID = " + ClinicID + "";
                                    }
                                    else
                                    {
                                        _strSQL = "SELECT MIN(Convert(int,Year)) FROM eDocument_Details_V3 WITH(NOLOCK) WHERE PatientID = " + PatientID + " AND ClinicID = " + ClinicID + "";
                                    }
                                }
                                else
                                {
                                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                    {
                                        _strSQL = "SELECT MAX(Convert(int,Year)) FROM eDocument_Details_V3_RCM WITH(NOLOCK) WHERE ClinicID = " + ClinicID + "";
                                    }
                                    else
                                    {
                                        _strSQL = "SELECT MAX(Convert(int,Year)) FROM eDocument_Details_V3 WITH(NOLOCK) WHERE PatientID = " + PatientID + " AND ClinicID = " + ClinicID + "";
                                    }
                                }
                                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                                if (_intresult != null && _intresult.ToString() != "")
                                {
                                    _result = Convert.ToInt32(_intresult.ToString());
                                }
                                _intresult = null;
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                            if (GetMinimum == true && GetDefaultMin == true)
                            {
                                _result = 2006;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }

                return _result;
            }
            #endregion "Dhruv 2010 -> GetLastYearOfDocuments"


            #region " Get Last Year Of PatientDocuments from Current Year "
            public String GetLastPreviousYearDocuments(Int64 PatientID, Int64 ClinicID, Int32 CurrYear, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                string _strSQL = "";
                String _result = string.Empty;
                DataTable _intresult;
                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {

                                // _strSQL = "SELECT Top 1 Convert(int,Year) as LastYear FROM eDocument_Details_V3 WITH(NOLOCK) WHERE PatientID = " + PatientID + " AND ClinicID = " + ClinicID + " And Convert(int,Year) <> '" + CurrYear + "' order by [Year] desc ";

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    _strSQL = "   SELECT Tar.PatientID, STUFF((SELECT  ',' + Srs.[Year] FROM eDocument_Details_V3_RCM Srs WHERE Srs.PatientID = Tar.PatientID  GROUP BY Srs.[Year] ORDER BY Srs.[Year] DESC FOR XML PATH('')),1,1,'') AS LastYear FROM eDocument_Details_V3_RCM AS Tar " +
                                          "   WHERE Tar.PatientID = " + PatientID + " GROUP BY Tar.PatientID ";
                                }
                                else
                                {
                                    _strSQL = "   SELECT Tar.PatientID, STUFF((SELECT  ',' + Srs.[Year] FROM eDocument_Details_V3 Srs WHERE Srs.PatientID = Tar.PatientID  GROUP BY Srs.[Year] ORDER BY Srs.[Year] DESC FOR XML PATH('')),1,1,'') AS LastYear FROM eDocument_Details_V3 AS Tar " +
                                          "   WHERE Tar.PatientID = " + PatientID + " GROUP BY Tar.PatientID ";
                                }

                                // _intresult = oDB.ExecuteScalar_Query(_strSQL);                               
                                oDB.Retrive_Query(_strSQL, out _intresult);
                                if (_intresult != null)
                                {
                                    _result = Convert.ToString(_intresult.Rows[0]["LastYear"]);
                                }
                                if (_intresult != null)
                                {
                                    _intresult.Dispose();
                                    _intresult = null;
                                }

                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    #region " Make Log Entry "
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                    #endregion " Make Log Entry "
                }

                return _result;
            }
            #endregion "Dhruv 2010 -> GetLastYearOfDocuments"



            #region "Dhruv 2010 -> GetArchiveInformation"
            public bool GetArchiveInformation(Int64 PatientID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {

                _ErrorMessage = "";
                _HasError = false;
                string _strSQL = "";
                bool _result = false;
                object _intresult = null;
                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    _strSQL = "SELECT count(*) FROM eDocument_Details_V3_RCM WITH(NOLOCK) WHERE PatientID = " + PatientID + " AND ClinicID = " + gloEDocumentV3.gloEDocV3Admin.gClinicID + " AND ArchiveStatus = 'true'";
                                }
                                else
                                {
                                    _strSQL = "SELECT count(*) FROM eDocument_Details_V3 WITH(NOLOCK) WHERE PatientID = " + PatientID + " AND ClinicID = " + gloEDocumentV3.gloEDocV3Admin.gClinicID + " AND ArchiveStatus = 'true'";
                                }

                                _intresult = oDB.ExecuteScalar_Query(_strSQL);

                                if (_intresult != null && _intresult.ToString() != "")
                                {
                                    if (_intresult.ToString() == "0")
                                    {
                                        _result = false;
                                    }
                                    else
                                    {
                                        _result = true;
                                    }
                                }
                                _intresult = null;
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }

                return _result;
            }

            public bool GetFileArchiveInformation(Int64 PatientID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {

                _ErrorMessage = "";
                _HasError = false;
                string _strSQL = "";
                bool _result = false;
                object _intresult = null;

                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    _strSQL = "IF EXISTS(SELECT * FROM sys.tables t WHERE t.name = 'eDocument_Archive_RCM') " +
                                              "BEGIN " +
                                              "SELECT count(*) FROM eDocument_Archive_RCM WITH(NOLOCK) WHERE ISNULL(StatusCode,0) = 2; " +
                                              "END ELSE BEGIN SELECT 0; END ";

                                }
                                else
                                {
                                    _strSQL =
                                        "IF EXISTS(SELECT * FROM sys.tables t WHERE t.name = 'eDocument_Archive') " +
                                        "BEGIN " +
                                        "SELECT count(*) FROM eDocument_Archive WITH(NOLOCK) WHERE PatientID = " + PatientID + " AND ISNULL(StatusCode,0) = 2 " +
                                        "END ELSE BEGIN SELECT 0; END ";
                                }

                                _intresult = oDB.ExecuteScalar_Query(_strSQL);

                                if (_intresult != null && _intresult.ToString() != "")
                                {
                                    if (_intresult.ToString() == "0")
                                    {
                                        _result = false;
                                    }
                                    else
                                    {
                                        _result = true;
                                    }
                                }
                                _intresult = null;
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "

                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }


                    #endregion " Make Log Entry "
                }

                return _result;
            }

            #endregion "Dhruv 2010 -> GetArchiveInformation"

            #region "Dhruv 2010 -> GetNonArchiveInformation"
            public bool GetNonArchiveInformation(Int64 PatientID)
            {

                _ErrorMessage = "";
                _HasError = false;
                //gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString);
                string _strSQL = "";
                bool _result = false;
                object _intresult = null;
                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {

                                _strSQL = "SELECT count(*) FROM eDocument_Details_V3 WITH(NOLOCK) WHERE PatientID = " + PatientID + " AND ClinicID = " + gloEDocumentV3.gloEDocV3Admin.gClinicID + " AND ArchiveStatus = 'false'";

                                _intresult = oDB.ExecuteScalar_Query(_strSQL);
                                if (_intresult != null && _intresult.ToString() != "")
                                {
                                    if (_intresult.ToString() == "0")
                                    {
                                        _result = false;
                                    }
                                    else
                                    {
                                        _result = true;
                                    }
                                }
                                _intresult = null;
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }

                return _result;
            }
            #endregion "Dhruv 2010 -> GetNonArchiveInformation"

            #region "Dhruv 2010 -> GetArchiveDocument"
            public DataTable GetArchiveDocument(Int64 PatientID, enum_OpenExternalSource OpenExternalSource)
            {
                _ErrorMessage = "";
                _HasError = false;
                //string _strSQL = "";
                DataTable _result = null;
                Database.DBParameters dbParameters = null;

                uint DocumentType = 0;

                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                if (OpenExternalSource == enum_OpenExternalSource.RCM) { DocumentType = 0; }
                                else { DocumentType = 1; }

                                dbParameters = new Database.DBParameters();
                                dbParameters.Add(new Database.DBParameter("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt));
                                dbParameters.Add(new Database.DBParameter("@ClinicID", gloEDocumentV3.gloEDocV3Admin.gClinicID, ParameterDirection.Input, SqlDbType.BigInt));
                                dbParameters.Add(new Database.DBParameter("@DocumentType", DocumentType, ParameterDirection.Input, SqlDbType.BigInt));
                                oDB.Retrive("gsp_GetArchivedDocuments", dbParameters, out _result);
                                                                
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }
                finally
                {
                    if (dbParameters != null)
                    {
                        dbParameters.Clear();
                        dbParameters.Dispose();
                        dbParameters = null;
                    }
                }
                return _result;
            }
            #endregion "Dhruv 2010 -> GetArchiveDocument"

            #region "Dhruv 2010 -> RetrieveDocumentOfPatient"
            public bool RetrieveDocumentOfPatient(Int64 PatientID, Int64 DocumentID)
            {
                _ErrorMessage = "";
                _HasError = false;
                bool _result = false;

                try
                {
                    string _strgloDMSDatabaseName = gloEDocumentV3.gloEDocV3Admin.gstrDMSDatabaseName;
                    string _strgloDMSSQLServerName = gloEDocumentV3.gloEDocV3Admin.gstrDMSSqlServerName;
                    string _strgloArchiveDatabaseName = gloEDocumentV3.gloEDocV3Admin.gloArchiveDatabaseName;
                    string _strgloArchiveSQLServerName = gloEDocumentV3.gloEDocV3Admin.gloArchiveSQLServerName;
                    Int64 _documentID = DocumentID;

                    using (gloEDocumentV3.Database.DBLayer oDBLayer = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.getArchiveDatabaseConnectionString))
                    {

                        if (oDBLayer.Connect(false))
                        {
                            if (oDBLayer != null)
                            {
                                using (gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters())
                                {
                                    if (oDBParams != null)
                                    {
                                        //oDBParams = new gloEDocumentV3.Database.DBParameters();
                                        oDBParams.Clear();

                                        // oDBParams.Add("@gloEMRServerName", _strgloDMSDatabaseName, ParameterDirection.Input, SqlDbType.VarChar);
                                        oDBParams.Add("@gloEMRServerName", _strgloDMSSQLServerName, ParameterDirection.Input, SqlDbType.VarChar);
                                        //oDBParams.Add("@gloEMRDatabaseName", _strgloDMSSQLServerName, ParameterDirection.Input, SqlDbType.VarChar);
                                        oDBParams.Add("@gloEMRDatabaseName", _strgloDMSDatabaseName, ParameterDirection.Input, SqlDbType.VarChar);
                                        oDBParams.Add("@ArchiveServerName", _strgloArchiveSQLServerName, ParameterDirection.Input, SqlDbType.VarChar);
                                        oDBParams.Add("@ArchiveDatabaseName", _strgloArchiveDatabaseName, ParameterDirection.Input, SqlDbType.VarChar);

                                        oDBParams.Add("@DocumentID", _documentID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oDBParams.Add("@IsDeleteDocumentsAfterRetrieve", true, ParameterDirection.Input, SqlDbType.Bit);

                                        //GLO2011-0013160 : Misspelling in a stored procedure
                                        // Changed sp name from gsp_RetrieveDMSDocumnets to gsp_RetrieveDMSDocuments
                                        int _ret = oDBLayer.Execute("gsp_RetrieveDMSDocuments", oDBParams, 0);

                                        if (_ret <= 0)
                                        {
                                            _result = false;

                                        }
                                        else
                                        {
                                            _result = true;
                                        }

                                    }
                                }
                            }
                        }

                    }
                    return _result;

                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                    _result = false;
                    return _result;
                }

            }

            public bool RetrieveRCMDocuments(Int64 DocumentID)
            {
                _ErrorMessage = "";
                _HasError = false;
                bool _result = false;

                try
                {
                    string _strgloDMSDatabaseName = gloEDocumentV3.gloEDocV3Admin.gstrDMSDatabaseName;
                    string _strgloDMSSQLServerName = gloEDocumentV3.gloEDocV3Admin.gstrDMSSqlServerName;
                    string _strgloArchiveDatabaseName = gloEDocumentV3.gloEDocV3Admin.gloArchiveDatabaseName;
                    string _strgloArchiveSQLServerName = gloEDocumentV3.gloEDocV3Admin.gloArchiveSQLServerName;
                    Int64 _documentID = DocumentID;

                    using (gloEDocumentV3.Database.DBLayer oDBLayer = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.getArchiveDatabaseConnectionString))
                    {

                        if (oDBLayer.Connect(false))
                        {
                            if (oDBLayer != null)
                            {
                                using (gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters())
                                {
                                    if (oDBParams != null)
                                    {
                                        
                                        oDBParams.Clear();                                        
                                        oDBParams.Add("@gloEMRServerName", _strgloDMSSQLServerName, ParameterDirection.Input, SqlDbType.VarChar);                                        
                                        oDBParams.Add("@gloEMRDatabaseName", _strgloDMSDatabaseName, ParameterDirection.Input, SqlDbType.VarChar);

                                        oDBParams.Add("@ArchiveServerName", _strgloArchiveSQLServerName, ParameterDirection.Input, SqlDbType.VarChar);
                                        oDBParams.Add("@ArchiveDatabaseName", _strgloArchiveDatabaseName, ParameterDirection.Input, SqlDbType.VarChar);

                                        oDBParams.Add("@DocumentID", _documentID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oDBParams.Add("@IsDeleteDocumentsAfterRetrieve", true, ParameterDirection.Input, SqlDbType.Bit);

                                        int _ret = oDBLayer.Execute("gsp_RetrieveRCMDocuments", oDBParams, 0);

                                        if (_ret <= 0)
                                        {
                                            _result = false;

                                        }
                                        else
                                        {
                                            _result = true;
                                        }

                                    }
                                }
                            }
                        }

                    }
                    return _result;

                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                    _result = false;
                    return _result;
                }

            }
            #endregion "Dhruv 2010 -> RetrieveDocumentOfPatient"

            #region "PatientID, Category, Year, Month, CliniID"
          
            #endregion

            #region "PatientID, Category, Year, CliniID"

            #region "Dhruv 2010 -> GetBaseDocumentsExcepts"
            public gloEDocumentV3.Document.BaseDocuments GetBaseDocumentsExcepts(long PatientID, string Category, string Year, string ExceptDocumentIDs, long ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Document.BaseDocuments oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
                gloEDocumentV3.Document.BaseDocument oBaseDocument = null;
                gloEDocumentV3.Document.eBaseContainers oBaseContainers = null;
                gloEDocumentV3.Document.eBaseContainer oBaseContainer = null;
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);

                DataTable oDataTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            #region "1.Get Document Details "

                            gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();

                            if (oDBParams != null)
                            {
                                oDBParams.Clear();

                                oDBParams.Add("@Year", Year.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParams.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParams.Add("@Category", Category.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParams.Add("@ExceptDocumentIDs", ExceptDocumentIDs.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar);

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    oDB.Retrive("gsp_GetBaseDocuments_RCM", oDBParams, out oDataTable);
                                }
                                else 
                                {
                                    oDBParams.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                                    oDB.Retrive("gsp_GetBaseDocuments", oDBParams, out oDataTable);
                                }
                                oDBParams.Dispose();
                                oDBParams = null;
                            }

                            //oDB.Retrive_Query(_strSQL, out oDataTable);

                            #endregion "1.Get Document Details "

                            if (oDataTable != null)
                            {
                                if (oDataTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                    {

                                        oBaseDocument = new gloEDocumentV3.Document.BaseDocument();
                                        {
                                            if (oBaseDocument != null)
                                            {
                                                oBaseDocument.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                oBaseDocument.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                                oBaseDocument.CategoryID = Convert.ToInt32(oDataTable.Rows[i]["CategoryID"]);
                                                oBaseDocument.Category = Convert.ToString(oDataTable.Rows[i]["Category"]);
                                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                                {
                                                    oBaseDocument.SubCategory = Convert.ToString(oDataTable.Rows[i]["SubCategory"]);
                                                }
                                                oBaseDocument.PatientID = Convert.ToInt64(oDataTable.Rows[i]["PatientID"]);
                                                oBaseDocument.Year = Convert.ToString(oDataTable.Rows[i]["Year"]);
                                                oBaseDocument.Month = Convert.ToString(oDataTable.Rows[i]["Month"]);
                                                oBaseDocument.PageCounts = Convert.ToInt32(oDataTable.Rows[i]["PageCounts"]);
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.CreatedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["CreatedDateTime"]); }
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.ModifiedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["ModifiedDateTime"]); }
                                                oBaseDocument.IsAcknowledge = Convert.ToBoolean(oDataTable.Rows[i]["IsAcknowledge"]);
                                                oBaseDocument.HasNote = Convert.ToBoolean(oDataTable.Rows[i]["HasNote"]);
                                                oBaseDocument.IsCompressed = Convert.ToBoolean(oDataTable.Rows[i]["IsCompressed"]);
                                                oBaseDocument.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                                oBaseContainers = new gloEDocumentV3.Document.eBaseContainers();
                                                {
                                                    if (oBaseDocument != null)
                                                    {

                                                        oBaseContainer = new gloEDocumentV3.Document.eBaseContainer();
                                                        {
                                                            if (oBaseContainer != null)
                                                            {
                                                                oBaseContainer.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                                oBaseContainer.EContainerID = Convert.ToInt64(oDataTable.Rows[i]["eContainerID"]);
                                                                oBaseContainer.PageFrom = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageFrom"]);
                                                                oBaseContainer.PageTo = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageTo"]);
                                                                oBaseContainer.DocumentExtension = Convert.ToString(oDataTable.Rows[i]["DocumentExtension"]);
                                                                oBaseContainer.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                                                oBaseContainers.Add(oBaseContainer);
                                                            }

                                                        }
                                                        oBaseDocument.EContainers = oBaseContainers;
                                                        oBaseDocuments.Add(oBaseDocument);
                                                    }
                                                }

                                            }
                                        }

                                    }
                                }
                                oDataTable.Dispose();
                                oDataTable = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

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
                return oBaseDocuments;
            }

            #endregion "Dhruv 2010 -> GetBaseDocumentsExcepts"

            #region "GetSubCategoryBaseDocumentsExcepts"

            public gloEDocumentV3.Document.BaseDocuments GetSubCategoryBaseDocumentsExcepts(long PatientID, string Category, string SubCategory, string Year, string ExceptDocumentIDs, long ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None, bool SkipAcknowledged = false)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Document.BaseDocuments oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
                gloEDocumentV3.Document.BaseDocument oBaseDocument = null;
                gloEDocumentV3.Document.eBaseContainers oBaseContainers = null;
                gloEDocumentV3.Document.eBaseContainer oBaseContainer = null;
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);

                DataTable oDataTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            #region "1.Get Document Details "

                            gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();

                            if (oDBParams != null)
                            {
                                oDBParams.Clear();

                                oDBParams.Add("@Year", Year.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParams.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParams.Add("@Category", Category.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParams.Add("@ExceptDocumentIDs", ExceptDocumentIDs.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar);

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    oDBParams.Add("@SubCategory", SubCategory.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParams.Add("@SkipAcknowledged", SkipAcknowledged, ParameterDirection.Input, SqlDbType.Bit);
                                    oDB.Retrive("gsp_GetBaseDocuments_RCM", oDBParams, out oDataTable);
                                }
                                else 
                                {
                                    oDBParams.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                                    oDB.Retrive("gsp_GetBaseDocuments", oDBParams, out oDataTable);
                                }
                                oDBParams.Dispose();
                                oDBParams = null;
                            }

                            #endregion "1.Get Document Details "

                            if (oDataTable != null)
                            {
                                if (oDataTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                    {

                                        oBaseDocument = new gloEDocumentV3.Document.BaseDocument();
                                        {
                                            if (oBaseDocument != null)
                                            { 
                                                oBaseDocument.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                oBaseDocument.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                                oBaseDocument.CategoryID = Convert.ToInt32(oDataTable.Rows[i]["CategoryID"]);
                                                oBaseDocument.Category = Convert.ToString(oDataTable.Rows[i]["Category"]);
                                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                                {
                                                    oBaseDocument.SubCategory = Convert.ToString(oDataTable.Rows[i]["SubCategory"]);
                                                }
                                                oBaseDocument.PatientID = Convert.ToInt64(oDataTable.Rows[i]["PatientID"]);
                                                oBaseDocument.Year = Convert.ToString(oDataTable.Rows[i]["Year"]);
                                                oBaseDocument.Month = Convert.ToString(oDataTable.Rows[i]["Month"]);
                                                oBaseDocument.PageCounts = Convert.ToInt32(oDataTable.Rows[i]["PageCounts"]);
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.CreatedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["CreatedDateTime"]); }
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.ModifiedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["ModifiedDateTime"]); }
                                                oBaseDocument.IsAcknowledge = Convert.ToBoolean(oDataTable.Rows[i]["IsAcknowledge"]);
                                                oBaseDocument.HasNote = Convert.ToBoolean(oDataTable.Rows[i]["HasNote"]);
                                                oBaseDocument.IsCompressed = Convert.ToBoolean(oDataTable.Rows[i]["IsCompressed"]);
                                                oBaseDocument.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                                oBaseContainers = new gloEDocumentV3.Document.eBaseContainers();
                                                {
                                                    if (oBaseDocument != null)
                                                    {

                                                        oBaseContainer = new gloEDocumentV3.Document.eBaseContainer();
                                                        {
                                                            if (oBaseContainer != null)
                                                            {
                                                                oBaseContainer.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                                oBaseContainer.EContainerID = Convert.ToInt64(oDataTable.Rows[i]["eContainerID"]);
                                                                oBaseContainer.PageFrom = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageFrom"]);
                                                                oBaseContainer.PageTo = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageTo"]);
                                                                oBaseContainer.DocumentExtension = Convert.ToString(oDataTable.Rows[i]["DocumentExtension"]);
                                                                oBaseContainer.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                                                oBaseContainers.Add(oBaseContainer);
                                                            }

                                                        }
                                                        oBaseDocument.EContainers = oBaseContainers;
                                                        oBaseDocuments.Add(oBaseDocument);
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                                oDataTable.Dispose();
                                oDataTable = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

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
                return oBaseDocuments;
            }
            #endregion

            #endregion


            #region "PatientID, Year, CliniID"

            #region "Dhruv 2010 -> GetBaseDocuments"

            public gloEDocumentV3.Document.BaseDocuments GetBaseDocuments_Optimized(long PatientID, int Year, long ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Document.BaseDocuments oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
                gloEDocumentV3.Document.BaseDocument oBaseDocument = null;
                gloEDocumentV3.Document.eBaseContainers oBaseContainers = null;
                gloEDocumentV3.Document.eBaseContainer oBaseContainer = null;

                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);

                DataTable oDataTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            #region "1.Get Document Details "

                            gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();

                            if (oDBParams != null)
                            {
                                oDBParams.Clear();

                                oDBParams.Add("@Year", Year.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParams.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParams.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                                oDB.Retrive("gsp_GetBaseDocuments", oDBParams, out oDataTable);
                               
                                oDBParams.Dispose();
                                oDBParams = null;
                            }

                            #endregion "1.Get Document Details "

                            if (oDataTable != null)
                            {
                                if (oDataTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                    {

                                        oBaseDocument = new gloEDocumentV3.Document.BaseDocument();
                                        {
                                            if (oBaseDocument != null)
                                            {
                                                oBaseDocument.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                oBaseDocument.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                                oBaseDocument.CategoryID = Convert.ToInt32(oDataTable.Rows[i]["CategoryID"]);
                                                oBaseDocument.Category = Convert.ToString(oDataTable.Rows[i]["Category"]);
                                                oBaseDocument.PatientID = Convert.ToInt64(oDataTable.Rows[i]["PatientID"]);
                                                oBaseDocument.Year = Convert.ToString(oDataTable.Rows[i]["Year"]);
                                                oBaseDocument.Month = Convert.ToString(oDataTable.Rows[i]["Month"]);
                                                oBaseDocument.PageCounts = Convert.ToInt32(oDataTable.Rows[i]["PageCounts"]);
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.CreatedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["CreatedDateTime"]); }
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.ModifiedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["ModifiedDateTime"]); }
                                                oBaseDocument.IsAcknowledge = Convert.ToBoolean(oDataTable.Rows[i]["IsAcknowledge"]);
                                                oBaseDocument.HasNote = Convert.ToBoolean(oDataTable.Rows[i]["HasNote"]);
                                                oBaseDocument.IsCompressed = Convert.ToBoolean(oDataTable.Rows[i]["IsCompressed"]);
                                                oBaseDocument.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);


                                                oBaseContainers = new gloEDocumentV3.Document.eBaseContainers();
                                                {
                                                    if (oBaseDocument != null)
                                                    {

                                                        oBaseContainer = new gloEDocumentV3.Document.eBaseContainer();
                                                        {
                                                            if (oBaseContainer != null)
                                                            {
                                                                oBaseContainer.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                                oBaseContainer.EContainerID = Convert.ToInt64(oDataTable.Rows[i]["eContainerID"]);
                                                                oBaseContainer.PageFrom = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageFrom"]);
                                                                oBaseContainer.PageTo = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageTo"]);
                                                                oBaseContainer.DocumentExtension = Convert.ToString(oDataTable.Rows[i]["DocumentExtension"]);
                                                                oBaseContainer.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                                                oBaseContainers.Add(oBaseContainer);
                                                            }

                                                        }
                                                        oBaseDocument.EContainers = oBaseContainers;
                                                        oBaseDocuments.Add(oBaseDocument);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                }
                return oBaseDocuments;
            }

            #endregion "Dhruv 2010 -> GetBaseDocuments"


            public gloEDocumentV3.Document.BaseDocuments GetBaseDocuments_RCM_Optimized(int Year, long ClinicID, bool SkipAcknowledged)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Document.BaseDocuments oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
                gloEDocumentV3.Document.BaseDocument oBaseDocument = null;
                gloEDocumentV3.Document.eBaseContainers oBaseContainers = null;
                gloEDocumentV3.Document.eBaseContainer oBaseContainer = null;

                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);

                DataTable oDataTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            #region "1.Get Document Details "

                            gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();

                            if (oDBParams != null)
                            {
                                oDBParams.Clear();

                                oDBParams.Add("@Year", Year.ToString().Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParams.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParams.Add("@SkipAcknowledged", SkipAcknowledged, ParameterDirection.Input, SqlDbType.Bit);

                                oDB.Retrive("gsp_GetBaseDocuments_RCM", oDBParams, out oDataTable);

                                oDBParams.Dispose();
                                oDBParams = null;
                            }

                            #endregion "1.Get Document Details "

                            if (oDataTable != null)
                            {
                                if (oDataTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                    {

                                        oBaseDocument = new gloEDocumentV3.Document.BaseDocument();
                                        {
                                            if (oBaseDocument != null)
                                            {
                                                oBaseDocument.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                oBaseDocument.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                                oBaseDocument.CategoryID = Convert.ToInt32(oDataTable.Rows[i]["CategoryID"]);
                                                oBaseDocument.Category = Convert.ToString(oDataTable.Rows[i]["Category"]);
                                                oBaseDocument.SubCategory = Convert.ToString(oDataTable.Rows[i]["SubCategory"]);
                                                oBaseDocument.PatientID = Convert.ToInt64(oDataTable.Rows[i]["PatientID"]);
                                                oBaseDocument.Year = Convert.ToString(oDataTable.Rows[i]["Year"]);
                                                oBaseDocument.Month = Convert.ToString(oDataTable.Rows[i]["Month"]);
                                                oBaseDocument.PageCounts = Convert.ToInt32(oDataTable.Rows[i]["PageCounts"]);
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.CreatedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["CreatedDateTime"]); }
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.ModifiedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["ModifiedDateTime"]); }
                                                oBaseDocument.IsAcknowledge = Convert.ToBoolean(oDataTable.Rows[i]["IsAcknowledge"]);
                                                oBaseDocument.HasNote = Convert.ToBoolean(oDataTable.Rows[i]["HasNote"]);
                                                oBaseDocument.IsCompressed = Convert.ToBoolean(oDataTable.Rows[i]["IsCompressed"]);
                                                oBaseDocument.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);


                                                oBaseContainers = new gloEDocumentV3.Document.eBaseContainers();
                                                {
                                                    if (oBaseDocument != null)
                                                    {

                                                        oBaseContainer = new gloEDocumentV3.Document.eBaseContainer();
                                                        {
                                                            if (oBaseContainer != null)
                                                            {
                                                                oBaseContainer.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                                oBaseContainer.EContainerID = Convert.ToInt64(oDataTable.Rows[i]["eContainerID"]);
                                                                oBaseContainer.PageFrom = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageFrom"]);
                                                                oBaseContainer.PageTo = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageTo"]);
                                                                oBaseContainer.DocumentExtension = Convert.ToString(oDataTable.Rows[i]["DocumentExtension"]);
                                                                oBaseContainer.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                                                oBaseContainers.Add(oBaseContainer);
                                                            }

                                                        }
                                                        oBaseDocument.EContainers = oBaseContainers;
                                                        oBaseDocuments.Add(oBaseDocument);
                                                    }
                                                }


                                            }
                                        }


                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                }
                return oBaseDocuments;
            }

            #region "Dhruv 2010 -> GetFilteredBaseDocuments"
            public gloEDocumentV3.Document.BaseDocuments GetFilteredBaseDocuments(long patientid, int year, long clinicid, string whereusertagis, string wherenotesis, string whereacknowledgeis, string wheredocumentnameis, string whichyearis, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Document.BaseDocuments oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
                gloEDocumentV3.Document.BaseDocument oBaseDocument = null;
                gloEDocumentV3.Document.eBaseContainers oBaseContainers = null;
                gloEDocumentV3.Document.eBaseContainer oBaseContainer = null;

                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);

                //string _strSQL = "";
                DataTable oDataTable = null;
                DataTable oContainerDetailTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            #region "1.Get Document Details "

                            gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();

                            if (oDBParams != null)
                            {
                                oDBParams.Clear();

                                oDBParams.Add("@Ack", whereacknowledgeis.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParams.Add("@Notes", wherenotesis.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParams.Add("@Tags", whereusertagis.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParams.Add("@DocuName", wheredocumentnameis.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParams.Add("@Year", whichyearis.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                                oDBParams.Add("@PatientID", patientid, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParams.Add("@ClinicID", clinicid, ParameterDirection.Input, SqlDbType.BigInt);

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    oDB.Retrive("gsp_GetFilteredBaseDocuments_RCM", oDBParams, out oDataTable);
                                }
                                else
                                {
                                    oDB.Retrive("gsp_GetFilteredBaseDocuments", oDBParams, out oDataTable);
                                }

                                oDBParams.Dispose();
                                oDBParams = null;
                            }

                            #endregion "1.Get Document Details "

                            if (oDataTable != null)
                            {
                                if (oDataTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                    {

                                        oBaseDocument = new gloEDocumentV3.Document.BaseDocument();
                                        {
                                            if (oBaseDocument != null)
                                            {
                                                oBaseDocument.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                oBaseDocument.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                                oBaseDocument.CategoryID = Convert.ToInt32(oDataTable.Rows[i]["CategoryID"]);
                                                oBaseDocument.Category = Convert.ToString(oDataTable.Rows[i]["Category"]);
                                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                                {
                                                    oBaseDocument.SubCategory = Convert.ToString(oDataTable.Rows[i]["SubCategory"]);
                                                }
                                                oBaseDocument.PatientID = Convert.ToInt64(oDataTable.Rows[i]["PatientID"]);
                                                oBaseDocument.Year = Convert.ToString(oDataTable.Rows[i]["Year"]);
                                                oBaseDocument.Month = Convert.ToString(oDataTable.Rows[i]["Month"]);
                                                oBaseDocument.PageCounts = Convert.ToInt32(oDataTable.Rows[i]["PageCounts"]);
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.CreatedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["CreatedDateTime"]); }
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.ModifiedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["ModifiedDateTime"]); }
                                                oBaseDocument.IsAcknowledge = Convert.ToBoolean(oDataTable.Rows[i]["IsAcknowledge"]);
                                                oBaseDocument.HasNote = Convert.ToBoolean(oDataTable.Rows[i]["HasNote"]);
                                                oBaseDocument.IsCompressed = Convert.ToBoolean(oDataTable.Rows[i]["IsCompressed"]);
                                                oBaseDocument.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                                //_strSQL = "";

                                                #region "2.Get Container Details for Document "

                                                 oDBParams = new gloEDocumentV3.Database.DBParameters();

                                                if (oDBParams != null)
                                                {
                                                    oDBParams.Clear();

                                                    oDBParams.Add("@DocumentID", Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]), ParameterDirection.Input, SqlDbType.BigInt);
                                                    oDBParams.Add("@ClinicID", clinicid, ParameterDirection.Input, SqlDbType.BigInt);

                                                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                                    {
                                                        oDB.Retrive("gsp_GetContainerDetails_RCM", oDBParams, out oContainerDetailTable);
                                                    }
                                                    else
                                                    {
                                                        oDB.Retrive("gsp_GetContainerDetails", oDBParams, out oContainerDetailTable);
                                                    }

                                                    oDBParams.Dispose();
                                                    oDBParams = null;
                                                }

                                                #endregion "2.Get Container Details for Document "

                                                oBaseContainers = new gloEDocumentV3.Document.eBaseContainers();
                                                {
                                                    if (oBaseContainers != null)
                                                    {
                                                        if (oContainerDetailTable != null && oContainerDetailTable.Rows.Count > 0)
                                                        {
                                                            for (int j = 0; j < oContainerDetailTable.Rows.Count; j++)
                                                            {
                                                                oBaseContainer = new gloEDocumentV3.Document.eBaseContainer();
                                                                if (oBaseContainer != null)
                                                                {
                                                                    oBaseContainer.EDocumentID = Convert.ToInt64(oContainerDetailTable.Rows[j]["eDocumentID"]);
                                                                    oBaseContainer.EContainerID = Convert.ToInt64(oContainerDetailTable.Rows[j]["eContainerID"]);
                                                                    oBaseContainer.PageFrom = Convert.ToInt32(oContainerDetailTable.Rows[j]["DocumentPageFrom"]);
                                                                    oBaseContainer.PageTo = Convert.ToInt32(oContainerDetailTable.Rows[j]["DocumentPageTo"]);
                                                                    oBaseContainer.DocumentExtension = Convert.ToString(oContainerDetailTable.Rows[j]["DocumentExtension"]);
                                                                    oBaseContainer.ClinicID = Convert.ToInt64(oContainerDetailTable.Rows[j]["ClinicID"]);

                                                                    oBaseContainers.Add(oBaseContainer);

                                                                }
                                                            }

                                                        }
                                                        if (oContainerDetailTable != null)
                                                        {
                                                            oContainerDetailTable.Dispose();
                                                            oContainerDetailTable = null;
                                                        }

                                                        oBaseDocument.EContainers = oBaseContainers;
                                                        oBaseDocuments.Add(oBaseDocument);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                oDataTable.Dispose();
                                oDataTable = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    ErrorMessagees(_ErrorMessage);
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                    if (oContainerDetailTable != null)
                    {
                        oContainerDetailTable.Dispose();
                        oContainerDetailTable = null;
                    }
                    
                }
                return oBaseDocuments;
            }

            #endregion "Dhruv 2010 -> GetFilteredBaseDocuments"
            #endregion

            #region "PatientID, Category, CliniID"
            #region "Dhruv 2010 -> GetBaseDocuments"
            public gloEDocumentV3.Document.BaseDocuments GetBaseDocuments(long PatientID, string Category, long ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Document.BaseDocuments oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
                gloEDocumentV3.Document.BaseDocument oBaseDocument = null;
                gloEDocumentV3.Document.eBaseContainers oBaseContainers = null;
                gloEDocumentV3.Document.eBaseContainer oBaseContainer = null;

                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);

                string _strSQL = "";
                DataTable oDataTable = null;
                DataTable oContainerDetailTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            #region "1.Get Document Details "

                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                            {
                                _strSQL = " SELECT " +
                           " eDocument_Details_V3_RCM.eDocumentID,ISNULL(eDocument_Details_V3_RCM.DocumentName,'') AS DocumentName, " +
                           " ISNULL(eDocument_Details_V3_RCM.CategoryID,0) AS CategoryID,ISNULL(eDocument_Details_V3_RCM.Category,'') AS Category,  " +
                           " ISNULL(eDocument_Details_V3_RCM.SubCategory,'') AS SubCategory,  " +
                           " eDocument_Details_V3_RCM.PatientID, ISNULL(eDocument_Details_V3_RCM.Year,'') AS Year,ISNULL(eDocument_Details_V3_RCM.Month,'') AS Month,  " +
                           " eDocument_Details_V3_RCM.PageCounts, eDocument_Details_V3_RCM.CreatedDateTime,  " +
                           " eDocument_Details_V3_RCM.ModifiedDateTime,ISNULL(eDocument_Details_V3_RCM.IsAcknowledge,'false') AS IsAcknowledge, " +
                           " ISNULL(eDocument_Details_V3_RCM.HasNote,'false') AS HasNote,ISNULL(eDocument_Details_V3_RCM.IsCompressed,'false') AS IsCompressed, " +
                           " eDocument_Details_V3_RCM.ClinicID " +
                           " FROM  " +
                           " eDocument_Details_V3_RCM WITH(NOLOCK) " +
                           " WHERE  " +
                           " eDocument_Details_V3_RCM.PatientID = " + PatientID + " " +
                           " AND eDocument_Details_V3_RCM.Category = '" + Category + "' " +
                           " AND eDocument_Details_V3_RCM.CategoryID <> 0  " +
                           " AND eDocument_Details_V3_RCM.ClinicID =  " + ClinicID + " " +
                           " AND eDocument_Details_V3_RCM.eDocumentID <> 0  " +
                           " AND eDocument_Details_V3_RCM.DocumentName IS NOT NULL  " +
                           " AND eDocument_Details_V3_RCM.Category IS NOT NULL  " +
                           " AND eDocument_Details_V3_RCM.PatientID IS NOT NULL  " +
                           " AND eDocument_Details_V3_RCM.[Year] IS NOT NULL  " +
                           " AND eDocument_Details_V3_RCM.[Month] IS NOT NULL  " +
                           " AND eDocument_Details_V3_RCM.PageCounts IS NOT NULL  " +
                           " ORDER BY  " +
                           " eDocument_Details_V3_RCM.[Year],eDocument_Details_V3_RCM.[Month], " +
                           " eDocument_Details_V3_RCM.DocumentName, eDocument_Details_V3_RCM.CreatedDateTime, " +
                           " eDocument_Details_V3_RCM.ModifiedDateTime desc  ";
                            }
                            else
                            {
                                _strSQL = " SELECT " +
                           " eDocument_Details_V3.eDocumentID,ISNULL(eDocument_Details_V3.DocumentName,'') AS DocumentName, " +
                           " ISNULL(eDocument_Details_V3.CategoryID,0) AS CategoryID,ISNULL(eDocument_Details_V3.Category,'') AS Category,  " +
                           " eDocument_Details_V3.PatientID, ISNULL(eDocument_Details_V3.Year,'') AS Year,ISNULL(eDocument_Details_V3.Month,'') AS Month,  " +
                           " eDocument_Details_V3.PageCounts, eDocument_Details_V3.CreatedDateTime,  " +
                           " eDocument_Details_V3.ModifiedDateTime,ISNULL(eDocument_Details_V3.IsAcknowledge,'false') AS IsAcknowledge, " +
                           " ISNULL(eDocument_Details_V3.HasNote,'false') AS HasNote,ISNULL(eDocument_Details_V3.IsCompressed,'false') AS IsCompressed, " +
                           " eDocument_Details_V3.ClinicID " +
                           " FROM  " +
                           " eDocument_Details_V3 WITH(NOLOCK) " +
                           " WHERE  " +
                           " eDocument_Details_V3.PatientID = " + PatientID + " " +
                           " AND eDocument_Details_V3.Category = '" + Category + "' " +
                           " AND eDocument_Details_V3.CategoryID <> 0  " +
                           " AND eDocument_Details_V3.ClinicID =  " + ClinicID + " " +
                           " AND eDocument_Details_V3.eDocumentID <> 0  " +
                           " AND eDocument_Details_V3.DocumentName IS NOT NULL  " +
                           " AND eDocument_Details_V3.Category IS NOT NULL  " +
                           " AND eDocument_Details_V3.PatientID IS NOT NULL  " +
                           " AND eDocument_Details_V3.[Year] IS NOT NULL  " +
                           " AND eDocument_Details_V3.[Month] IS NOT NULL  " +
                           " AND eDocument_Details_V3.PageCounts IS NOT NULL  " +
                           " ORDER BY  " +
                           " eDocument_Details_V3.[Year],eDocument_Details_V3.[Month], " +
                           " eDocument_Details_V3.DocumentName, eDocument_Details_V3.CreatedDateTime, " +
                           " eDocument_Details_V3.ModifiedDateTime desc  ";
                            }

                            oDB.Retrive_Query(_strSQL, out oDataTable);

                            #endregion "1.Get Document Details "

                            if (oDataTable != null)
                            {
                                if (oDataTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                    {

                                        oBaseDocument = new gloEDocumentV3.Document.BaseDocument();
                                        {
                                            if (oBaseDocument != null)
                                            {
                                                oBaseDocument.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                oBaseDocument.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                                oBaseDocument.CategoryID = Convert.ToInt32(oDataTable.Rows[i]["CategoryID"]);
                                                oBaseDocument.Category = Convert.ToString(oDataTable.Rows[i]["Category"]);
                                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                                {
                                                    oBaseDocument.SubCategory = Convert.ToString(oDataTable.Rows[i]["SubCategory"]);
                                                }
                                                oBaseDocument.PatientID = Convert.ToInt64(oDataTable.Rows[i]["PatientID"]);
                                                oBaseDocument.Year = Convert.ToString(oDataTable.Rows[i]["Year"]);
                                                oBaseDocument.Month = Convert.ToString(oDataTable.Rows[i]["Month"]);
                                                oBaseDocument.PageCounts = Convert.ToInt32(oDataTable.Rows[i]["PageCounts"]);
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.CreatedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["CreatedDateTime"]); }
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.ModifiedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["ModifiedDateTime"]); }
                                                oBaseDocument.IsAcknowledge = Convert.ToBoolean(oDataTable.Rows[i]["IsAcknowledge"]);
                                                oBaseDocument.HasNote = Convert.ToBoolean(oDataTable.Rows[i]["HasNote"]);
                                                oBaseDocument.IsCompressed = Convert.ToBoolean(oDataTable.Rows[i]["IsCompressed"]);
                                                oBaseDocument.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                                _strSQL = "";

                                                #region "2.Get Container Details for Document "

                                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                                {
                                                    _strSQL = " SELECT " +
                                                " eDocumentID, eContainerID, DocumentPageFrom, DocumentPageTo,  " +
                                                " ISNULL(DocumentExtension,'') AS DocumentExtension, " +
                                                " ISNULL(IsModified, 'false') AS IsModified, " +
                                                " ISNULL(MachineID, '') AS MachineID, " +
                                                " ISNULL(SourceMachine, '') AS SourceMachine, " +
                                                " ISNULL(SourceBin, '') AS SourceBin, " +
                                                " ClinicID " +
                                                " FROM   " +
                                                " eDocument_Container_V3_RCM WITH(NOLOCK) " +
                                                " WHERE " +
                                                " eDocumentID = " + Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]) + " " +
                                                " AND ClinicID = " + ClinicID + " ";
                                                }
                                                else
                                                {
                                                    _strSQL = " SELECT " +
                                                " eDocumentID, eContainerID, DocumentPageFrom, DocumentPageTo,  " +
                                                " ISNULL(DocumentExtension,'') AS DocumentExtension, " +
                                                " ISNULL(IsModified, 'false') AS IsModified, " +
                                                " ISNULL(MachineID, '') AS MachineID, " +
                                                " ISNULL(SourceMachine, '') AS SourceMachine, " +
                                                " ISNULL(SourceBin, '') AS SourceBin, " +
                                                " ClinicID " +
                                                " FROM   " +
                                                " eDocument_Container_V3 WITH(NOLOCK) " +
                                                " WHERE " +
                                                " eDocumentID = " + Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]) + " " +
                                                " AND ClinicID = " + ClinicID + " ";
                                                }

                                                oDB.Retrive_Query(_strSQL, out oContainerDetailTable);

                                                #endregion "2.Get Container Details for Document "

                                                oBaseContainers = new gloEDocumentV3.Document.eBaseContainers();
                                                {
                                                    if (oBaseContainers != null)
                                                    {
                                                        if (oContainerDetailTable != null && oContainerDetailTable.Rows.Count > 0)
                                                        {
                                                            for (int j = 0; j < oContainerDetailTable.Rows.Count; j++)
                                                            {
                                                                oBaseContainer = new gloEDocumentV3.Document.eBaseContainer();
                                                                if (oBaseContainers != null)
                                                                {
                                                                    oBaseContainer.EDocumentID = Convert.ToInt64(oContainerDetailTable.Rows[j]["eDocumentID"]);
                                                                    oBaseContainer.EContainerID = Convert.ToInt64(oContainerDetailTable.Rows[j]["eContainerID"]);
                                                                    oBaseContainer.PageFrom = Convert.ToInt32(oContainerDetailTable.Rows[j]["DocumentPageFrom"]);
                                                                    oBaseContainer.PageTo = Convert.ToInt32(oContainerDetailTable.Rows[j]["DocumentPageTo"]);
                                                                    oBaseContainer.DocumentExtension = Convert.ToString(oContainerDetailTable.Rows[j]["DocumentExtension"]);
                                                                    oBaseContainer.ClinicID = Convert.ToInt64(oContainerDetailTable.Rows[j]["ClinicID"]);

                                                                    oBaseContainers.Add(oBaseContainer);

                                                                    if (oBaseContainer != null)
                                                                    {
                                                                        oBaseContainer.Dispose();
                                                                        oBaseContainer = null;
                                                                    }
                                                                }
                                                            }

                                                        }
                                                        if (oContainerDetailTable != null)
                                                        {
                                                            oContainerDetailTable.Dispose();
                                                            oContainerDetailTable = null;
                                                        }
                                                    }
                                                    oBaseDocument.EContainers = oBaseContainers;
                                                    oBaseDocuments.Add(oBaseDocument);
                                                }
                                            }
                                        }
                                       
                                    }
                                }
                                oDataTable.Dispose();
                                oDataTable = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    ErrorMessagees(_ErrorMessage);
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                    if (oContainerDetailTable != null)
                    {
                        oContainerDetailTable.Dispose();
                        oContainerDetailTable = null;
                    }
                   
                }
                return oBaseDocuments;
            }

            #endregion "Dhruv 2010 -> GetBaseDocuments"
            #endregion
            #region "Get Documents for VIS-Immunization"
            public gloEDocumentV3.Document.BaseDocuments GetBaseDocuments_Immunization(long PatientID, string Category, long ClinicID, long DocumentID)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Document.BaseDocuments oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
                gloEDocumentV3.Document.BaseDocument oBaseDocument = null;
                gloEDocumentV3.Document.eBaseContainers oBaseContainers = null;
                gloEDocumentV3.Document.eBaseContainer oBaseContainer = null;

                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);

                string _strSQL = "";
                DataTable oDataTable = null;
                DataTable oContainerDetailTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            #region "1.Get Document Details "

                            _strSQL = " SELECT " +
                            " eDocument_Details_V3.eDocumentID,ISNULL(eDocument_Details_V3.DocumentName,'') AS DocumentName, " +
                            " ISNULL(eDocument_Details_V3.CategoryID,0) AS CategoryID,ISNULL(eDocument_Details_V3.Category,'') AS Category,  " +
                            " eDocument_Details_V3.PatientID, ISNULL(eDocument_Details_V3.Year,'') AS Year,ISNULL(eDocument_Details_V3.Month,'') AS Month,  " +
                            " eDocument_Details_V3.PageCounts, eDocument_Details_V3.CreatedDateTime,  " +
                            " eDocument_Details_V3.ModifiedDateTime,ISNULL(eDocument_Details_V3.IsAcknowledge,'false') AS IsAcknowledge, " +
                            " ISNULL(eDocument_Details_V3.HasNote,'false') AS HasNote,ISNULL(eDocument_Details_V3.IsCompressed,'false') AS IsCompressed, " +
                            " eDocument_Details_V3.ClinicID " +
                            " FROM  " +
                            " eDocument_Details_V3 WITH(NOLOCK) " +
                            " WHERE  " +
                            " eDocument_Details_V3.PatientID = " + PatientID + " " +
                            " AND eDocument_Details_V3.Category = '" + Category + "' " +
                            " AND eDocument_Details_V3.CategoryID <> 0  " +
                            " AND eDocument_Details_V3.ClinicID =  " + ClinicID + " " +
                            " AND eDocument_Details_V3.eDocumentID = " + DocumentID + " " +
                            " AND eDocument_Details_V3.eDocumentID <> 0  " +
                            " AND eDocument_Details_V3.DocumentName IS NOT NULL  " +
                            " AND eDocument_Details_V3.Category IS NOT NULL  " +
                            " AND eDocument_Details_V3.PatientID IS NOT NULL  " +
                            " AND eDocument_Details_V3.[Year] IS NOT NULL  " +
                            " AND eDocument_Details_V3.[Month] IS NOT NULL  " +
                            " AND eDocument_Details_V3.PageCounts IS NOT NULL  " +
                            " ORDER BY  " +
                            " eDocument_Details_V3.[Year],eDocument_Details_V3.[Month], " +
                            " eDocument_Details_V3.DocumentName, eDocument_Details_V3.CreatedDateTime, " +
                            " eDocument_Details_V3.ModifiedDateTime desc  ";

                            oDB.Retrive_Query(_strSQL, out oDataTable);

                            #endregion "1.Get Document Details "

                            if (oDataTable != null)
                            {
                                if (oDataTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                    {

                                        oBaseDocument = new gloEDocumentV3.Document.BaseDocument();
                                        {
                                            if (oBaseDocument != null)
                                            {
                                                oBaseDocument.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                oBaseDocument.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                                oBaseDocument.CategoryID = Convert.ToInt32(oDataTable.Rows[i]["CategoryID"]);
                                                oBaseDocument.Category = Convert.ToString(oDataTable.Rows[i]["Category"]);
                                                oBaseDocument.PatientID = Convert.ToInt64(oDataTable.Rows[i]["PatientID"]);
                                                oBaseDocument.Year = Convert.ToString(oDataTable.Rows[i]["Year"]);
                                                oBaseDocument.Month = Convert.ToString(oDataTable.Rows[i]["Month"]);
                                                oBaseDocument.PageCounts = Convert.ToInt32(oDataTable.Rows[i]["PageCounts"]);
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.CreatedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["CreatedDateTime"]); }
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.ModifiedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["ModifiedDateTime"]); }
                                                oBaseDocument.IsAcknowledge = Convert.ToBoolean(oDataTable.Rows[i]["IsAcknowledge"]);
                                                oBaseDocument.HasNote = Convert.ToBoolean(oDataTable.Rows[i]["HasNote"]);
                                                oBaseDocument.IsCompressed = Convert.ToBoolean(oDataTable.Rows[i]["IsCompressed"]);
                                                oBaseDocument.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                                _strSQL = "";

                                                #region "2.Get Container Details for Document "

                                                _strSQL = " SELECT " +
                                                " eDocumentID, eContainerID, DocumentPageFrom, DocumentPageTo,  " +
                                                " ISNULL(DocumentExtension,'') AS DocumentExtension, " +
                                                " ISNULL(IsModified, 'false') AS IsModified, " +
                                                " ISNULL(MachineID, '') AS MachineID, " +
                                                " ISNULL(SourceMachine, '') AS SourceMachine, " +
                                                " ISNULL(SourceBin, '') AS SourceBin, " +
                                                " ClinicID " +
                                                " FROM   " +
                                                " eDocument_Container_V3 WITH(NOLOCK) " +
                                                " WHERE " +
                                                " eDocumentID = " + Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]) + " " +
                                                " AND ClinicID = " + ClinicID + " ";

                                                oDB.Retrive_Query(_strSQL, out oContainerDetailTable);

                                                #endregion "2.Get Container Details for Document "

                                                oBaseContainers = new gloEDocumentV3.Document.eBaseContainers();
                                                {
                                                    if (oBaseContainers != null)
                                                    {
                                                        if (oContainerDetailTable != null && oContainerDetailTable.Rows.Count > 0)
                                                        {
                                                            for (int j = 0; j < oContainerDetailTable.Rows.Count; j++)
                                                            {
                                                                oBaseContainer = new gloEDocumentV3.Document.eBaseContainer();
                                                                if (oBaseContainers != null)
                                                                {
                                                                    oBaseContainer.EDocumentID = Convert.ToInt64(oContainerDetailTable.Rows[j]["eDocumentID"]);
                                                                    oBaseContainer.EContainerID = Convert.ToInt64(oContainerDetailTable.Rows[j]["eContainerID"]);
                                                                    oBaseContainer.PageFrom = Convert.ToInt32(oContainerDetailTable.Rows[j]["DocumentPageFrom"]);
                                                                    oBaseContainer.PageTo = Convert.ToInt32(oContainerDetailTable.Rows[j]["DocumentPageTo"]);
                                                                    oBaseContainer.DocumentExtension = Convert.ToString(oContainerDetailTable.Rows[j]["DocumentExtension"]);
                                                                    oBaseContainer.ClinicID = Convert.ToInt64(oContainerDetailTable.Rows[j]["ClinicID"]);

                                                                    oBaseContainers.Add(oBaseContainer);

                                                                }
                                                            }

                                                        }
                                                    }
                                                    oBaseDocument.EContainers = oBaseContainers;
                                                    oBaseDocuments.Add(oBaseDocument);
                                                }
                                            }
                                        }

                                    }
                                }
                                oDataTable.Dispose();
                                oDataTable = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    ErrorMessagees(_ErrorMessage);
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                    if (oContainerDetailTable != null)
                    {
                        oContainerDetailTable.Dispose();
                        oContainerDetailTable = null;
                    }

                }
                return oBaseDocuments;
            }
            #endregion
            #region "PateintID,ClinicID"
            #region "Dhruv 2010 -> GetPatientBaseDocuments"
            public gloEDocumentV3.Document.BaseDocuments GetPatientBaseDocuments(long PatientID, long ClinicID)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Document.BaseDocuments oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
                gloEDocumentV3.Document.BaseDocument oBaseDocument = null;
                gloEDocumentV3.Document.eBaseContainers oBaseContainers = null;
                gloEDocumentV3.Document.eBaseContainer oBaseContainer = null;

                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);

                string _strSQL = "";
                DataTable oDataTable = null;
                DataTable oContainerDetailTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            //CreatedDateTime
                            #region "1.Get Document Details "
                            _strSQL = " SELECT  eDocument_Details_V3.eDocumentID,ISNULL(eDocument_Details_V3.DocumentName,'') AS DocumentName, " +
                                      " ISNULL(eDocument_Details_V3.Category,'') AS Category, " +
                                      " ISNULL(eDocument_Details_V3.Year,'') AS Year,ISNULL(eDocument_Details_V3.Month,'') AS Month, " +
                                      " ISNULL(eDocument_Details_V3.IsAcknowledge,'false') AS IsAcknowledge, " +
                                      " ISNULL(eDocument_Details_V3.HasNote,'false') AS HasNote, " +
                                      " eDocument_Details_V3.ClinicID, CreatedDateTime, ModifiedDateTime " +
                                      " FROM   eDocument_Details_V3 WITH(NOLOCK)  WHERE   eDocument_Details_V3.PatientID = " + PatientID + "  " +
                                      " AND eDocument_Details_V3.ClinicID = " + ClinicID + " AND eDocument_Details_V3.DocumentName IS NOT NULL " +
                                      " ORDER BY eDocument_Details_V3.Category, CreatedDateTime desc";
                            //" ORDER BY   eDocument_Details_V3.[Year],ISNULL(eDocument_Details_V3.[Month],''), " +
                            //" eDocument_Details_V3.DocumentName,eDocument_Details_V3.Category desc";

                            oDB.Retrive_Query(_strSQL, out oDataTable);

                            #endregion "1.Get Document Details "

                            if (oDataTable != null)
                            {
                                if (oDataTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                    {

                                        oBaseDocument = new gloEDocumentV3.Document.BaseDocument();
                                        if (oBaseDocument != null)
                                        {
                                            oBaseDocument.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                            oBaseDocument.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                            //oBaseDocument.CategoryID = Convert.ToInt32(oDataTable.Rows[i]["CategoryID"]);
                                            oBaseDocument.Category = Convert.ToString(oDataTable.Rows[i]["Category"]);
                                            //oBaseDocument.PatientID = Convert.ToInt64(oDataTable.Rows[i]["PatientID"]);
                                            oBaseDocument.Year = Convert.ToString(oDataTable.Rows[i]["Year"]);
                                            oBaseDocument.Month = Convert.ToString(oDataTable.Rows[i]["Month"]);
                                            oBaseDocument.IsAcknowledge = Convert.ToBoolean(oDataTable.Rows[i]["IsAcknowledge"]);
                                            oBaseDocument.HasNote = Convert.ToBoolean(oDataTable.Rows[i]["HasNote"]);
                                            oBaseDocument.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);
                                            oBaseDocument.CreatedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["CreatedDateTime"]);
                                            oBaseDocument.ModifiedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["ModifiedDateTime"]);
                                            _strSQL = "";

                                            #region "2.Get Container Details for Document "

                                            _strSQL = " SELECT eContainerID,eDocumentID,ClinicID FROM eDocument_Container_V3 WITH(NOLOCK) WHERE eDocumentID = " + Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]) + " AND ClinicID = " + ClinicID + " ";

                                            oDB.Retrive_Query(_strSQL, out oContainerDetailTable);

                                            #endregion "2.Get Container Details for Document "

                                            oBaseContainers = new gloEDocumentV3.Document.eBaseContainers();
                                            if (oBaseContainers != null)
                                            {
                                                if (oContainerDetailTable != null && oContainerDetailTable.Rows.Count > 0)
                                                {
                                                    for (int j = 0; j < oContainerDetailTable.Rows.Count; j++)
                                                    {
                                                        oBaseContainer = new gloEDocumentV3.Document.eBaseContainer();

                                                        oBaseContainer.EDocumentID = Convert.ToInt64(oContainerDetailTable.Rows[j]["eDocumentID"]);
                                                        oBaseContainer.EContainerID = Convert.ToInt64(oContainerDetailTable.Rows[j]["eContainerID"]);
                                                        oBaseContainer.ClinicID = Convert.ToInt64(oContainerDetailTable.Rows[j]["ClinicID"]);

                                                        oBaseContainers.Add(oBaseContainer);

                                                    }

                                                }
                                                if (oContainerDetailTable != null)
                                                {
                                                    oContainerDetailTable.Dispose();
                                                    oContainerDetailTable = null;
                                                }

                                                oBaseDocument.EContainers = oBaseContainers;
                                                oBaseDocuments.Add(oBaseDocument);
                                            }
                                        }
                                    }
                                }
                                oDataTable.Dispose();
                                oDataTable = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }
                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                   
                }
                return oBaseDocuments;
            }


            #endregion "Dhruv 2010 -> GetPatientBaseDocuments"
            #endregion

            #region "PatientID,DocumentID,ClinicID"
            #region "Dhruv 2010 -> GetBaseDocuments"
            public gloEDocumentV3.Document.BaseDocuments GetBaseDocuments(long PatientID, long DocumentID, long ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Document.BaseDocuments oBaseDocuments = new gloEDocumentV3.Document.BaseDocuments();
                gloEDocumentV3.Document.BaseDocument oBaseDocument = null;
                gloEDocumentV3.Document.eBaseContainers oBaseContainers = null;
                gloEDocumentV3.Document.eBaseContainer oBaseContainer = null;

                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);

                DataTable oDataTable = null;
                
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            #region "1.Get Document Details "

                            gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();

                            if (oDBParams != null)
                            {
                                oDBParams.Clear();

                                oDBParams.Add("@nDocumentID", DocumentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParams.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    oDB.Retrive("gsp_GetBaseDocuments_RCM", oDBParams, out oDataTable);
                                }
                                else
                                {
                                    oDBParams.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                                    oDB.Retrive("gsp_GetBaseDocuments", oDBParams, out oDataTable);    
                                }

                                oDBParams.Dispose();
                                oDBParams = null;
                            }

                          

                            #endregion "1.Get Document Details "

                            if (oDataTable != null)
                            {
                                if (oDataTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                    {

                                        oBaseDocument = new gloEDocumentV3.Document.BaseDocument();
                                        {
                                            if (oBaseDocument != null)
                                            {
                                                oBaseDocument.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                oBaseDocument.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                                oBaseDocument.CategoryID = Convert.ToInt32(oDataTable.Rows[i]["CategoryID"]);
                                                oBaseDocument.Category = Convert.ToString(oDataTable.Rows[i]["Category"]);
                                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                                {
                                                    oBaseDocument.SubCategory = Convert.ToString(oDataTable.Rows[i]["SubCategory"]);
                                                }
                                                oBaseDocument.PatientID = Convert.ToInt64(oDataTable.Rows[i]["PatientID"]);
                                                oBaseDocument.Year = Convert.ToString(oDataTable.Rows[i]["Year"]);
                                                oBaseDocument.Month = Convert.ToString(oDataTable.Rows[i]["Month"]);
                                                oBaseDocument.PageCounts = Convert.ToInt32(oDataTable.Rows[i]["PageCounts"]);
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.CreatedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["CreatedDateTime"]); }
                                                if (oDataTable.Rows[i]["CreatedDateTime"] != System.DBNull.Value)
                                                { oBaseDocument.ModifiedDateTime = Convert.ToDateTime(oDataTable.Rows[i]["ModifiedDateTime"]); }
                                                oBaseDocument.IsAcknowledge = Convert.ToBoolean(oDataTable.Rows[i]["IsAcknowledge"]);
                                                oBaseDocument.HasNote = Convert.ToBoolean(oDataTable.Rows[i]["HasNote"]);
                                                oBaseDocument.IsCompressed = Convert.ToBoolean(oDataTable.Rows[i]["IsCompressed"]);
                                                oBaseDocument.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);


                                                oBaseContainers = new gloEDocumentV3.Document.eBaseContainers();
                                                {
                                                    if (oBaseContainers != null)
                                                    {
                                                       
                                                                oBaseContainer = new gloEDocumentV3.Document.eBaseContainer();
                                                                if (oBaseContainers != null)
                                                                {
                                                                    oBaseContainer.EDocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                                    oBaseContainer.EContainerID = Convert.ToInt64(oDataTable.Rows[i]["eContainerID"]);
                                                                    oBaseContainer.PageFrom = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageFrom"]);
                                                                    oBaseContainer.PageTo = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageTo"]);
                                                                    oBaseContainer.DocumentExtension = Convert.ToString(oDataTable.Rows[i]["DocumentExtension"]);
                                                                    oBaseContainer.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                                                    oBaseContainers.Add(oBaseContainer);
                                                                   
                                                                }
                                                            
                                                        oBaseDocument.EContainers = oBaseContainers;
                                                        oBaseDocuments.Add(oBaseDocument);
                                                    }
                                                }
                                            }
                                        }

                                    }

                                }
                                oDataTable.Dispose();
                                oDataTable = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    ErrorMessagees(_ErrorMessage);
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }

                }
                return oBaseDocuments;
            }

            #endregion "Dhruv 2010 -> GetBaseDocuments"
            #endregion

            #region "Dhruv 2010 -> GetContainerStream"
            public object GetContainerStream(Int64 DocumentID, Int64 ContainerID, Int64 ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                object _result = null;
                Database.DBParameters oDBParameters = null;//new gloEDocumentV3.Database.DBParameters();
                using (Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString))
                {
                    try
                    {

                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                using (oDBParameters = new gloEDocumentV3.Database.DBParameters())
                                {
                                    if (oDBParameters != null)
                                    {
                                        oDBParameters.Add("@ContainerID", ContainerID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oDBParameters.Add("@DocumentID", DocumentID, ParameterDirection.Input, SqlDbType.BigInt);
                                        oDBParameters.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                        {
                                            _result = oDB.ExecuteScalar("gsp_eDocV3_GetContainerStream_RCM", oDBParameters);
                                        }
                                        else
                                        {
                                            _result = oDB.ExecuteScalar("gsp_eDocV3_GetContainerStream", oDBParameters);
                                        }

                                    }
                                }
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        _HasError = true;
                        _ErrorMessage = ex.Message;
                        _result = false;
                        ErrorMessagees(_ErrorMessage);
                    }
                }

                return _result;
            }
            #endregion "Dhruv 2010 -> GetContainerStream"

            #region "Dhruv 2010 -> GetContainerStream"
            public string GetContainerStream(Int64 DocumentID, Int64 ContainerID, Int64 ClinicID, bool IsByte, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                //_ErrorMessage = "";
                //_HasError = false;
                //object _result = null;
                string _Path = null;
                string _FilePath = gloEDocV3Admin.gTemporaryProcessPath + "\\" + ContainerID.ToString() + DocumentID.ToString();
                
                _Path = GetContainerByteStream(DocumentID, ContainerID, ClinicID, _FilePath, _OpenExternalSource);
                return _Path;
            }

            #endregion "Dhruv 2010 -> GetContainerStream"

            #region "Dhruv 2010 -> GetFaxConvertPages"
            public DataTable GetFaxConvertPages()
            {
                _ErrorMessage = "";
                _HasError = false;
                DataTable dt = null;
                // Database.DBParameters oDBParameters = null;//new gloEDocumentV3.Database.DBParameters();
                using (Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString))
                {


                    try
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {

                                string strQRY = "select nTemplateID,sTemplateName from TemplateGallery_MST tm inner join category_mst cm on tm.ncategoryid=cm.ncategoryid  "
                                              + "where cm.sDescription= 'Fax Cover Page' Order By sTemplateName";
                                oDB.Retrive_Query(strQRY, out dt);
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _HasError = true;
                        _ErrorMessage = ex.Message;

                        ErrorMessagees(_ErrorMessage);
                    }

                }
                return dt;
            }
            #endregion "Dhruv 2010 -> GetFaxConvertPages"

            #region "Dhruv 2010 -> GetFaxConverpageContent"
            public DataTable GetFaxConverpageContent(Int64 TemplateID)
            {
                DataTable _dt = null;//new DataTable();
                object _result = null;
                Database.DBParameters oDBParameters = null;//new gloEDocumentV3.Database.DBParameters();
                using (Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString))
                {
                    if (TemplateID > 0)
                    {
                        _ErrorMessage = "";
                        _HasError = false;

                        try
                        {
                            if (oDB != null)
                            {
                                if (oDB.Connect(false))
                                {

                                    using (oDBParameters = new gloEDocumentV3.Database.DBParameters())
                                    {
                                        if (oDBParameters != null)
                                        {
                                            oDBParameters.Add("@nTemplateId", TemplateID, ParameterDirection.Input, SqlDbType.BigInt);
                                            //using (_dt = new DataTable())
                                            {
                                                oDB.Retrive("gsp_GetExamContents", oDBParameters, out _dt);
                                            }
                                        }
                                    }
                                    if (oDB != null)
                                    {
                                        oDB.Disconnect();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _HasError = true;
                            _ErrorMessage = ex.Message;
                            _result = false;
                            if (_ErrorMessage.Trim() != "")
                            {
                                string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                                _MessageString = "";
                            }
                        }

                    }
                }
                return _dt;
            }
            #endregion "Dhruv 2010 -> GetFaxConverpageContent"

            #region "Get File From DB and convert into Byte for the memory handling"
            //Developer: Mitesh Patel
            //Date:03-Feb-2012'
            //Bug ID: 15824
            public string GetContainerByteStream(Int64 DocumentID, Int64 ContainerID, Int64 ClinicID, string FilePath, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                bool _result = true;
                bool _isFirstTime = true;

                String filePath = "";
                // DataTable oDataTable = new DataTable();
                SqlConnection sqlConnection = null;
                SqlCommand osqlCommand = null;
                SqlTransaction transaction = null;
                SqlFileStream serverStream = null;
                byte[] buffer = null;
                try
                {


                    #region "Stored Procedure"
                    gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);
                    Object pathObj = null;


                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();

                            if (oDBParams != null)
                            {
                                oDBParams.Clear();
                                oDBParams.Add("@eContainerID", ContainerID, ParameterDirection.Input, SqlDbType.BigInt);

                                oDBParams.Add("@eDocumentID", DocumentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParams.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    pathObj = oDB.ExecuteScalar("gsp_GetiDocumentStreamPathName_RCM", oDBParams);
                                }
                                else
                                {
                                    pathObj = oDB.ExecuteScalar("gsp_GetiDocumentStreamPathName", oDBParams);
                                }
                                oDBParams.Clear();
                                oDBParams.Dispose();
                                oDBParams = null;
                            }

                        }
                        oDB.Dispose();
                        oDB = null;
                    }
                    #endregion
                    if (DBNull.Value != pathObj)
                        filePath = Convert.ToString(pathObj);
                    sqlConnection = new SqlConnection(gloEDocV3Admin.gDMSDatabaseConnectionString);
                    sqlConnection.Open();
                    transaction = sqlConnection.BeginTransaction("mainTranaction");
                    osqlCommand = new SqlCommand("gsp_GETFILESTREAM", sqlConnection);
                    osqlCommand.CommandType = CommandType.StoredProcedure;
                    osqlCommand.Transaction = transaction;

                    Object obj = osqlCommand.ExecuteScalar();

                    byte[] txContext = (byte[])obj;

                    try
                    {
                        serverStream = new SqlFileStream(filePath, txContext, FileAccess.Read);
                    }
                    catch (Exception ex)
                    {
                        _HasError = true;
                        _ErrorMessage = ex.Message;
                        _result = false;
                        if (_ErrorMessage.Trim() != "")
                        {
                            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                            _MessageString = "";
                        }
                    }
                    if (serverStream != null)
                    {
                        //serverStream.Seek(0L, SeekOrigin.Begin);
                        int currentIndex = 0;
                        buffer = new byte[gloEDocV3Admin.gBufferSize];
                        int bytesRead;
                        int block = 0;
                        long maxBytesToRead = serverStream.Length;
                        long currentBytesToRead = gloEDocV3Admin.gBufferSize;
                        if (serverStream.Length > 0)
                        {
                            currentIndex = 0;
                            do
                            {
                                //serverStream.Seek(currentIndex, SeekOrigin.Begin);
                                block = block + 1;
                                if (currentBytesToRead > maxBytesToRead)
                                {
                                    currentBytesToRead = maxBytesToRead;
                                }
                                bytesRead = serverStream.Read(buffer, 0, (int)currentBytesToRead);

                                if (bytesRead > 0)
                                {
                                    _result = ConvertBinaryToFileNew(FilePath, ref buffer, bytesRead, ref _isFirstTime);

                                    if (_result == false)
                                    {
                                        _ErrorMessage = "Error in the converting Binary to New file";
                                        break;
                                    }
                                    currentIndex += bytesRead;
                                    maxBytesToRead -= bytesRead;
                                }
                            } while (bytesRead == gloEDocV3Admin.gBufferSize);
                        }
                        serverStream.Close();
                        serverStream.Dispose();
                    }
                    transaction.Commit();
                }

                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    transaction.Rollback();
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    buffer = null;
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                    return "";
                }
                finally
                {

                    if (osqlCommand != null)
                    {
                        osqlCommand.Parameters.Clear();
                        osqlCommand.Dispose();
                        osqlCommand = null;
                    }
                    if (sqlConnection.State == ConnectionState.Open)
                    { sqlConnection.Close(); }
                    if (sqlConnection != null)
                    { sqlConnection.Dispose(); }
                    if (transaction != null)
                    {
                        transaction.Dispose();
                        transaction = null;
                    }
                }
                return FilePath;
            }

            #endregion  "Dhruv 20100621 - Code from the 273 to 5050 for the memory handling"


            #region "Dhruv 20100621 - Code from the 273 to 5050 for the memory handling"

            public bool GetContainerStream(Int64 DocumentID, Int64 ContainerID, Int64 ClinicID, ref string FilePath, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "Cls_eDocV3_GetList.cs GetContainerStream() function start");
                _ErrorMessage = "";
                _HasError = false;
                bool _result = true;
                bool _isFirstTime = true;
                bool bIsDocProcessed = false;
                bool bIsNotSupported = false;

                String filePath = "";
                DataTable oDataTable = null;
                SqlConnection osqlConnection = null;
                SqlCommand osqlCommand = null;
                SqlTransaction transaction = null;
                SqlFileStream serverStream = null;
                byte[] buffer = null;
                Object obj = null;
                try
                {
                    #region "Stored Procedure"
                    gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);
                    Object pathObj = null;


                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();

                            if (oDBParams != null)
                            {
                                oDBParams.Clear();
                                oDBParams.Add("@eContainerID", ContainerID, ParameterDirection.Input, SqlDbType.BigInt);

                                oDBParams.Add("@eDocumentID", DocumentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParams.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    pathObj = oDB.ExecuteScalar("gsp_GetiDocumentStreamPathName_RCM", oDBParams);
                                    //Changes done by Nilesh to update the flag to true as it is RCM Docs
                                    bIsDocProcessed = true;
                                }
                                else
                                {
                                    oDBParams.Add("@NeedCheck", true, ParameterDirection.Input, SqlDbType.Bit);

                                    oDB.Retrive("gsp_GetiDocumentStreamPathName", oDBParams, out oDataTable);

                                    if (oDataTable != null)
                                    {
                                        if (oDataTable.Rows.Count > 0)
                                        {
                                            pathObj = oDataTable.Rows[0]["filePath"];
                                            bIsDocProcessed = (Boolean)oDataTable.Rows[0]["bIsDocProcessed"];
                                        }
                                        oDataTable.Dispose();
                                        oDataTable = null;
                                    }
                                }
                                oDBParams.Clear();
                                oDBParams.Dispose();
                                oDBParams = null;
                            }

                        }
                        oDB.Dispose();
                        oDB = null;
                    }
                    #endregion
                    if (DBNull.Value != pathObj)
                        filePath = Convert.ToString(pathObj);
                    osqlConnection = new SqlConnection(gloEDocV3Admin.gDMSDatabaseConnectionString);
                    osqlConnection.Open();
                    transaction = osqlConnection.BeginTransaction("mainTranaction");
                    osqlCommand = new SqlCommand("gsp_GETFILESTREAM", osqlConnection);
                    osqlCommand.CommandType = CommandType.StoredProcedure;
                    osqlCommand.Transaction = transaction;

                    obj = osqlCommand.ExecuteScalar();
                    byte[] txContext = (byte[])obj;


                    try
                    {
                        serverStream = new SqlFileStream(filePath, txContext, FileAccess.Read);

                    }
                    catch (Exception ex)
                    {
                        _HasError = true;
                        _ErrorMessage = ex.Message;
                        _result = false;
                        if (_ErrorMessage.Trim() != "")
                        {
                            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                            _MessageString = "";
                        }
                    }
                    if (serverStream != null)
                    {
                        //gloEDocV3Admin.LoadDocumentsWithCompatibleMode = false;
                        if (gloEDocV3Admin.IsRead_LoadDocumentsWithCompatibleMode == false)
                        {
                            gloSettings.GeneralSettings osettings = null;
                            string _ErrorMessageReadSetting = "";
                            try
                            {
                                //Read settings from Database for 'LoadDocumentsWithCompatibleMode'
                                osettings = new gloSettings.GeneralSettings(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString);

                                object _obj = null;
                                osettings.GetSetting("LoadDocumentsWithCompatibleMode", out _obj);

                                if (!string.IsNullOrEmpty(Convert.ToString(_obj)))
                                {
                                    gloEDocV3Admin.LoadDocumentsWithCompatibleMode = Convert.ToBoolean(_obj);
                                    gloEDocV3Admin.IsRead_LoadDocumentsWithCompatibleMode = true;
                                }
                                else
                                { gloEDocV3Admin.IsRead_LoadDocumentsWithCompatibleMode = false; }
                            }
                            catch (Exception ex)
                            {
                                _ErrorMessageReadSetting = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + ex.Message;
                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessageReadSetting);

                                ex = null;
                                _ErrorMessageReadSetting = "";
                            }
                            finally
                            {
                                if (osettings != null)
                                {
                                    osettings.Dispose();
                                    osettings = null;
                                }

                                _ErrorMessageReadSetting = "";

                            }
                        }

                        //serverStream.Seek(0L, SeekOrigin.Begin);
                        int currentIndex = 0;
                        buffer = new byte[gloEDocV3Admin.gBufferSize];
                        int bytesRead;
                        int block = 0;
                        long maxBytesToRead = serverStream.Length;
                        long currentBytesToRead = gloEDocV3Admin.gBufferSize;
                        if (serverStream.Length > 0)
                        {
                            currentIndex = 0;
                            do
                            {
                                //serverStream.Seek(currentIndex, SeekOrigin.Begin);
                                block = block + 1;
                                if (currentBytesToRead > maxBytesToRead)
                                {
                                    currentBytesToRead = maxBytesToRead;
                                }
                                bytesRead = serverStream.Read(buffer, 0, (int)currentBytesToRead);
                                if (block == 1)
                                {
                                    if (gloEDocV3Admin.LoadDocumentsWithCompatibleMode == false)
                                    {
                                        if (bytesRead > 8)
                                        {
                                            string strdata = System.Text.Encoding.UTF8.GetString(buffer, 0, 8);
                                            if (strdata.ToUpper().Contains("%PDF") == false)
                                            {
                                                _ErrorMessage = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : ";
                                                _ErrorMessage += "Error in the converting Binary to New file";
                                                _result = false;
                                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_ErrorMessage);
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (bytesRead > 0)
                                {
                                    _result = ConvertBinaryToFileNew(FilePath, ref buffer, bytesRead, ref _isFirstTime);

                                    if (_result == false)
                                    {
                                        _ErrorMessage = "Error in the converting Binary to New file";
                                        break;
                                    }
                                    currentIndex += bytesRead;
                                    maxBytesToRead -= bytesRead;
                                }
                            } while (bytesRead == gloEDocV3Admin.gBufferSize);
                            //Bug #86274: 00000916: Application freeze after importing PDF
                            //here to close serverstream and dispose it
                            serverStream.Flush();
                            serverStream.Close();
                            serverStream.Dispose();
                            transaction.Commit();
                            //Changes done by Nilesh to recover only non RCM Docs
                            if (_OpenExternalSource != enum_OpenExternalSource.RCM)
                            {
                                if (_result && (bIsDocProcessed == false))
                                {
                                    int iPageCount = 0;
                                    bool bOkToProceed = false;
                                    FilePath = gloPrintDialog.gloRecoverPDF.RecoverIfNotSupported(FilePath, gloSettings.FolderSettings.AppTempFolderPath, out bIsNotSupported, out bOkToProceed, out iPageCount);
                                    if ((bOkToProceed == false) || (iPageCount <= 0))
                                    {
                                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "\t" + FilePath + " Is Not Supported By Pdftron");
                                    }
                                    else
                                    {
                                        if (bIsNotSupported)
                                        {
                                            using (gloEDocumentV3.eDocManager.eDocManager DocManager = new gloEDocumentV3.eDocManager.eDocManager())
                                            {
                                                bool _retVal = DocManager.UpdateContainer(DocumentID, ContainerID, FilePath, true, _OpenExternalSource);

                                            }
                                        }
                                    }
                                    UpdateFileProcessed(DocumentID, ContainerID, ClinicID);
                                }
                            }

                        }
                        else
                        {
                            serverStream.Close();
                            serverStream.Dispose();
                            transaction.Commit();
                            _result = false;
                        }
                    }
                    else
                    {
                        _result = false;
                    }
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "Cls_eDocV3_GetList.cs GetContainerStream() function End");
                }

                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    transaction.Rollback();
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    _result = false;
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                }
                finally
                {
                    obj = null;

                    if (osqlCommand != null)
                    {
                        osqlCommand.Parameters.Clear();
                        osqlCommand.Dispose();
                        osqlCommand = null;
                    }
                    if (osqlConnection.State == ConnectionState.Open)
                    { osqlConnection.Close(); }
                    if (osqlConnection != null)
                    { osqlConnection.Dispose(); }
                    if (transaction != null)
                    {
                        transaction.Dispose();
                        transaction = null;
                    }
                }
                return _result;


            }

            public static bool UpdateFileProcessed(Int64 DocumentID, Int64 ContainerID, Int64 ClinicID)
            {
                SqlCommand _sqlcommand = null;
                SqlConnection _connection = null;

                try
                {
                    _connection = new SqlConnection(gloEDocV3Admin.gDMSDatabaseConnectionString);
                    if (_connection != null)
                    {
                        _connection.Open();

                        // _transaction = _connection.BeginTransaction();

                        _sqlcommand = new SqlCommand();
                        if (_sqlcommand != null)
                        {
                            _sqlcommand.Connection = _connection;
                            _sqlcommand.CommandType = CommandType.StoredProcedure;
                            //_sqlcommand.Transaction = _transaction;
                            _sqlcommand.CommandText = "gsp_eDocV3_UpdateProcessed";
                            _sqlcommand.Parameters.Clear();


                            _sqlcommand.Parameters.Add("@eDocumentID", SqlDbType.BigInt).Value = DocumentID;
                            _sqlcommand.Parameters.Add("@eContainerID", SqlDbType.BigInt).Value = ContainerID;
                            _sqlcommand.Parameters.Add("@eClinicID", SqlDbType.BigInt).Value = ClinicID;

                            if (_sqlcommand.ExecuteNonQuery() <= 0)
                            {
                                // _transaction.Rollback();
                                if (_sqlcommand != null)
                                {
                                    _sqlcommand.Parameters.Clear();
                                    _sqlcommand.Dispose();
                                    _sqlcommand = null;
                                }
                                return false;
                            }
                            if (_sqlcommand != null)
                            {
                                _sqlcommand.Parameters.Clear();
                                _sqlcommand.Dispose();
                                _sqlcommand = null;
                            }

                        }
                        _connection.Close();
                        _connection.Dispose();
                        _connection = null;
                    }
                    //_transaction.Commit();
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    return false;
                }
                return true;
            }

            #endregion  "Dhruv 20100621 - Code from the 273 to 5050 for the memory handling"
            public bool GetContainerStream(Int64 TemplateID, string FilePath)
            {
                _ErrorMessage = "";
                _HasError = false;
                bool _result = false;

                byte[] content = null;
                //MemoryStream imageStream = null;
                DataTable _dt = null;
                object cntFromDB = null;

                try
                {
                    _dt = GetFaxConverpageContent(TemplateID);

                    if (_dt != null)
                    {
                        if (_dt.Rows.Count > 0)
                        {
                            if (_dt.Rows[0][0] != null)
                            {
                                cntFromDB = (object)_dt.Rows[0][0];
                                if (cntFromDB != null)
                                {
                                    content = (byte[])cntFromDB;

                                    try
                                    {

                                        if (content != null)
                                        {
                                            //SLR: Stream is not needed 12/22
                                            System.IO.FileStream oFile = new System.IO.FileStream(FilePath, System.IO.FileMode.Create);
                                            oFile.Write(content, 0, content.Length);
                                            oFile.Close();
                                            oFile.Dispose();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        _HasError = true;
                                        _ErrorMessage = ex.Message;
                                        _result = false;
                                        if (_ErrorMessage.Trim() != "")
                                        {
                                            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                                            _MessageString = "";
                                        }
                                    }

                                }
                                else
                                {

                                    return false;
                                }

                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    _result = false;
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                }
                finally
                {
                    if (content != null)
                    {
                        // Array.Clear(content, 0, content.Length);
                        content = null;

                    }
                    if (_dt != null)
                    {
                        _dt.Dispose();
                        _dt = null;
                    }
                    
                }

                return _result;
            }

            #region "Dhruv 20100621 ->Code merge from 273 to 5050"
            public bool ConvertBinaryToFileNew(string FilePath, ref byte[] buffer, int bytesRead, ref bool _isFirstTimeOpen)
            {
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "Cls_eDocV3_GetList.cs ConvertBinaryTofileNew() function start");
                bool _result = true;
                // Byte[] byteRead = null;
                FileStream oFile = null;
                try
                {
                    if (buffer == null)
                    {
                        _result = false;
                        _ErrorMessage = "Error due to buffer is empty";
                    }
                    else
                    {
                        if (_isFirstTimeOpen == true)
                        {
                            oFile = new FileStream(FilePath, FileMode.Create);
                            _isFirstTimeOpen = false;
                        }
                        else
                        {
                            if (System.IO.File.Exists(FilePath))
                            {
                                oFile = new FileStream(FilePath, FileMode.Append);
                            }

                        }
                    }
                    if (oFile == null)
                    {
                        _result = false;
                        _ErrorMessage = "Error file object is null";

                    }
                    if (_result == true)
                    {
                        using (BinaryWriter bw = new BinaryWriter(oFile))
                        {
                            bw.Write(buffer, 0, bytesRead);
                            bw.Flush();
                            bw.Close();
                        }
                    }
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "Cls_eDocV3_GetList.cs ConvertBinaryTofileNew() function End");

                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    _result = false;
                }
                finally
                {
                    if (oFile != null) { oFile.Close(); oFile.Dispose(); }
                    //Code added on 4rd Octomber 2008 By - Sagar Ghodke
                    //               //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }
                    if (_result == false)
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage.ToString();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";

                    }
                }
                return _result;
            }
            #endregion "Dhruv 20100621 ->Code merge from 273 to 5050"

            #region "Dhruv 2010 -> GetDocumentCounts"
            public int GetDocumentCounts(long PatientID, long ClinicID)
            {
                _ErrorMessage = "";
                _HasError = false;
                Int32 _Result = 0;
                object _internalresult = null;
                //Database.DBLayer oDB = new Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString);
                string _strSQL = "";
                try
                {
                    using (Database.DBLayer oDB = new Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                _strSQL = "SELECT COUNT(eDocumentID) FROM eDocument_Details_V3 WITH(NOLOCK) WHERE PatientID = " + PatientID + " AND ClinicID = " + ClinicID + "";
                                _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                                if (_internalresult != null)
                                {
                                    if (_internalresult.ToString() != null)
                                    {
                                        if (_internalresult.GetType() != typeof(System.DBNull))
                                        {
                                            if (_internalresult.ToString() != "")
                                            {
                                                _Result = Convert.ToInt32(_internalresult.ToString());
                                            }
                                        }
                                    }
                                }
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;

                    #region " Make Log Entry "
                    //Code added on 7rd October 2008 By - Sagar Ghodke
                    //Make Log entry in DMSExceptionLog file for any exceptions found
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    //End Code add
                    #endregion " Make Log Entry "
                }
                finally
                {
                    if (_internalresult != null)
                    {
                        _internalresult = null;
                    }
                }
                return _Result;
            }
            #endregion "Dhruv 2010 -> GetDocumentCounts"

            #endregion

            #region "GetContainerID"
            public Int64 GetContainerID(Int64 nDocumentID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                Int64 _result = 0;

                using (Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString))
                {
                    string strQRY = null;
                    try
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    strQRY = "SELECT eContainerID FROM  eDocument_Container_V3_RCM WHERE eDocumentID =" + nDocumentID;
                                }
                                else
                                {
                                    strQRY = "SELECT eContainerID FROM  eDocument_Container_V3 WHERE eDocumentID =" + nDocumentID;
                                }

                                _result = Convert.ToInt64(oDB.ExecuteScalar_Query(strQRY));

                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _HasError = true;
                        _ErrorMessage = ex.Message;

                        ErrorMessagees(_ErrorMessage);
                    }

                }
                return _result;
            }
            #endregion "GetContainerID"

            #region "Page Information"

            #region "Dhruv 2010 -> GetBasePages"
            public gloEDocumentV3.Document.BasePages GetBasePages(Int64 DocumentID, Int64 ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Document.BasePages oBasePages = new gloEDocumentV3.Document.BasePages();
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);

                DataTable oDataTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            #region "Added StoredProcdure"


                            gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();

                            if (oDBParams != null)
                            {
                                oDBParams.Clear();
                                oDBParams.Add("@eDocumentID", DocumentID, ParameterDirection.Input, SqlDbType.BigInt);

                                oDBParams.Add("@eContainerID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParams.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    oDB.Retrive("gsp_GetEDocumentDetails_RCM", oDBParams, out oDataTable);
                                }
                                else
                                {
                                    oDB.Retrive("gsp_GetEDocumentDetails", oDBParams, out oDataTable);
                                }

                                oDBParams.Dispose();
                                oDBParams = null;
                            }



                            #endregion
                            if (oDataTable != null)
                            {
                                if (oDataTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                    {
                                        oBasePages.Add(Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"].ToString()), Convert.ToInt64(oDataTable.Rows[i]["eContainerID"].ToString()), Convert.ToInt32(oDataTable.Rows[i]["ContainerPageNumber"].ToString()), Convert.ToInt32(oDataTable.Rows[i]["DocumentPageNumber"].ToString()), oDataTable.Rows[i]["PageName"].ToString(), oDataTable.Rows[i]["BookMarkTag"].ToString(), false, Convert.ToInt64(oDataTable.Rows[i]["ClinicID"].ToString()));
                                        try
                                        {
                                            if (oDataTable.Rows[i]["HasNote"].GetType() != typeof(System.DBNull))
                                            {
                                                oBasePages[oBasePages.Count - 1].HasNotes = (System.Boolean)oDataTable.Rows[i]["HasNote"];
                                            }
                                        }
                                        catch { }
                                    }
                                }
                                oDataTable.Dispose();
                                oDataTable = null;
                            }
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

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
                return oBasePages;
            }
            #endregion "Dhruv 2010 -> GetBasePages"

            #region "Dhruv 2010 -> GetFilteredBasePages"
            public gloEDocumentV3.Document.BasePages GetFilteredBasePages(Int64 DocumentID, Int64 ClinicID, string WhereUserTagIs, string WhereNotesIs, string WhereAcknowledgeIs, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Document.BasePages oBasePages = new gloEDocumentV3.Document.BasePages();
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);

                string _strSQL = "";
                string _strAllPagesSQL = "";

                DataTable oDataTable = null;
                DataTable oDataTableAllPages = null;

                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            #region "Generate Query"

                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                            {
                                _strAllPagesSQL = "SELECT eDocumentID,eContainerID,ContainerPageNumber,DocumentPageNumber,PageName," +
                            " BookMarkTag,HasNote,ClinicID FROM eDocument_Pages_V3_RCM WITH(NOLOCK) " +
                            " WHERE eDocumentID = " + DocumentID + " AND ClinicID = " + ClinicID + " AND " +
                            " ContainerPageNumber IS NOT NULL AND DocumentPageNumber IS NOT NULL AND PageName IS NOT NULL ORDER BY DocumentPageNumber";
                            }
                            else
                            {
                                _strAllPagesSQL = "SELECT eDocumentID,eContainerID,ContainerPageNumber,DocumentPageNumber,PageName," +
                            " BookMarkTag,HasNote,ClinicID FROM eDocument_Pages_V3 WITH(NOLOCK) " +
                            " WHERE eDocumentID = " + DocumentID + " AND ClinicID = " + ClinicID + " AND " +
                            " ContainerPageNumber IS NOT NULL AND DocumentPageNumber IS NOT NULL AND PageName IS NOT NULL ORDER BY DocumentPageNumber";
                            }

                            if (WhereUserTagIs.Trim() == "" && WhereNotesIs.Trim() == "" && WhereAcknowledgeIs.Trim() == "")
                            {
                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    _strSQL = "SELECT eDocumentID,eContainerID,ContainerPageNumber,DocumentPageNumber,PageName," +
                                " BookMarkTag,HasNote,ClinicID FROM eDocument_Pages_V3_RCM WITH(NOLOCK) " +
                                " WHERE eDocumentID = " + DocumentID + " AND ClinicID = " + ClinicID + " AND " +
                                " ContainerPageNumber IS NOT NULL AND DocumentPageNumber IS NOT NULL AND PageName IS NOT NULL ORDER BY DocumentPageNumber";
                                }
                                else
                                {
                                    _strSQL = "SELECT eDocumentID,eContainerID,ContainerPageNumber,DocumentPageNumber,PageName," +
                                " BookMarkTag,HasNote,ClinicID FROM eDocument_Pages_V3 WITH(NOLOCK) " +
                                " WHERE eDocumentID = " + DocumentID + " AND ClinicID = " + ClinicID + " AND " +
                                " ContainerPageNumber IS NOT NULL AND DocumentPageNumber IS NOT NULL AND PageName IS NOT NULL ORDER BY DocumentPageNumber";
                                }

                            }
                            else
                            {
                                bool _AcknowledgeAttched = false;
                                bool _NoteAttched = false;
                                //  bool _UserTagAttched = false;

                                _strSQL = "SELECT DISTINCT F.eDocumentID,F.eContainerID,F.ContainerPageNumber,F.DocumentPageNumber,F.PageName," +
                                " F.BookMarkTag,F.HasNote,F.ClinicID FROM ( ";

                                #region "Acknowledge"
                                if (WhereAcknowledgeIs.Trim() != "")
                                {
                                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                    {
                                        _strSQL = _strSQL + "SELECT eDocumentID,eContainerID,ContainerPageNumber,DocumentPageNumber,PageName," +
                                    " BookMarkTag,HasNote,ClinicID FROM eDocument_Pages_V3_RCM WITH(NOLOCK) " +
                                    " WHERE eDocumentID = " + DocumentID + " AND ClinicID = " + ClinicID + " AND " +
                                    " ContainerPageNumber IS NOT NULL AND DocumentPageNumber IS NOT NULL AND PageName IS NOT NULL" +
                                    " AND DocumentPageNumber IN " +
                                    "(SELECT  DocumentPageNumber FROM eDocument_NTAO_V3_RCM WHERE upper(NTAODescription) like '%" + WhereAcknowledgeIs.ToUpper() + "%' AND ClinicID = " + ClinicID + " AND eDocumentID = " + DocumentID + "  AND NTAOType = " + enum_NTAOType.Acknowledge.GetHashCode() + " ) ";
                                    }
                                    else
                                    {
                                        _strSQL = _strSQL + "SELECT eDocumentID,eContainerID,ContainerPageNumber,DocumentPageNumber,PageName," +
                                    " BookMarkTag,HasNote,ClinicID FROM eDocument_Pages_V3 WITH(NOLOCK) " +
                                    " WHERE eDocumentID = " + DocumentID + " AND ClinicID = " + ClinicID + " AND " +
                                    " ContainerPageNumber IS NOT NULL AND DocumentPageNumber IS NOT NULL AND PageName IS NOT NULL" +
                                    " AND DocumentPageNumber IN " +
                                    "(SELECT  DocumentPageNumber FROM eDocument_NTAO_V3 WHERE upper(NTAODescription) like '%" + WhereAcknowledgeIs.ToUpper() + "%' AND ClinicID = " + ClinicID + " AND eDocumentID = " + DocumentID + "  AND NTAOType = " + enum_NTAOType.Acknowledge.GetHashCode() + " ) ";
                                    }

                                    _AcknowledgeAttched = true;
                                }
                                #endregion

                                #region "Notes"
                                if (WhereNotesIs.Trim() != "")
                                {
                                    if (_AcknowledgeAttched == true)
                                    {
                                        _strSQL = _strSQL + " UNION ";
                                    }

                                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                    {
                                        _strSQL = _strSQL + "SELECT eDocumentID,eContainerID,ContainerPageNumber,DocumentPageNumber,PageName," +
                                    " BookMarkTag,HasNote,ClinicID FROM eDocument_Pages_V3_RCM WITH(NOLOCK) " +
                                    " WHERE eDocumentID = " + DocumentID + " AND ClinicID = " + ClinicID + " AND " +
                                    " ContainerPageNumber IS NOT NULL AND DocumentPageNumber IS NOT NULL AND PageName IS NOT NULL" +
                                    " AND DocumentPageNumber IN " +
                                    "(SELECT  DocumentPageNumber FROM eDocument_NTAO_V3_RCM WHERE upper(NTAODescription) like '%" + WhereNotesIs.ToUpper() + "%' AND ClinicID = " + ClinicID + " AND eDocumentID = " + DocumentID + "  AND NTAOType = " + enum_NTAOType.Notes.GetHashCode() + " ) ";
                                    }
                                    else
                                    {
                                        _strSQL = _strSQL + "SELECT eDocumentID,eContainerID,ContainerPageNumber,DocumentPageNumber,PageName," +
                                    " BookMarkTag,HasNote,ClinicID FROM eDocument_Pages_V3 WITH(NOLOCK) " +
                                    " WHERE eDocumentID = " + DocumentID + " AND ClinicID = " + ClinicID + " AND " +
                                    " ContainerPageNumber IS NOT NULL AND DocumentPageNumber IS NOT NULL AND PageName IS NOT NULL" +
                                    " AND DocumentPageNumber IN " +
                                    "(SELECT  DocumentPageNumber FROM eDocument_NTAO_V3 WHERE upper(NTAODescription) like '%" + WhereNotesIs.ToUpper() + "%' AND ClinicID = " + ClinicID + " AND eDocumentID = " + DocumentID + "  AND NTAOType = " + enum_NTAOType.Notes.GetHashCode() + " ) ";
                                    }

                                    _NoteAttched = true;
                                }
                                #endregion

                                #region "User Tags"
                                if (WhereUserTagIs.Trim() != "")
                                {
                                    if (_AcknowledgeAttched == true || _NoteAttched == true)
                                    {
                                        _strSQL = _strSQL + " UNION ";
                                    }

                                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                    {
                                        _strSQL = _strSQL + "SELECT eDocumentID,eContainerID,ContainerPageNumber,DocumentPageNumber,PageName," +
                                    " BookMarkTag,HasNote,ClinicID FROM eDocument_Pages_V3_RCM WITH(NOLOCK) " +
                                    " WHERE eDocumentID = " + DocumentID + " AND ClinicID = " + ClinicID + " AND " +
                                    " ContainerPageNumber IS NOT NULL AND DocumentPageNumber IS NOT NULL AND PageName IS NOT NULL" +
                                    " AND DocumentPageNumber IN " +
                                    "(SELECT  DocumentPageNumber FROM eDocument_NTAO_V3_RCM WHERE upper(NTAODescription) like '%" + WhereUserTagIs.ToUpper() + "%' AND ClinicID = " + ClinicID + " AND eDocumentID = " + DocumentID + "  AND NTAOType = " + enum_NTAOType.Tag.GetHashCode() + " ) ";
                                    }
                                    else
                                    {
                                        _strSQL = _strSQL + "SELECT eDocumentID,eContainerID,ContainerPageNumber,DocumentPageNumber,PageName," +
                                    " BookMarkTag,HasNote,ClinicID FROM eDocument_Pages_V3 WITH(NOLOCK) " +
                                    " WHERE eDocumentID = " + DocumentID + " AND ClinicID = " + ClinicID + " AND " +
                                    " ContainerPageNumber IS NOT NULL AND DocumentPageNumber IS NOT NULL AND PageName IS NOT NULL" +
                                    " AND DocumentPageNumber IN " +
                                    "(SELECT  DocumentPageNumber FROM eDocument_NTAO_V3 WHERE upper(NTAODescription) like '%" + WhereUserTagIs.ToUpper() + "%' AND ClinicID = " + ClinicID + " AND eDocumentID = " + DocumentID + "  AND NTAOType = " + enum_NTAOType.Tag.GetHashCode() + " ) ";
                                    }


                                    // _UserTagAttched = true;
                                }
                                #endregion

                                _strSQL = _strSQL + ") F ORDER BY DocumentPageNumber";
                            }
                            #endregion

                            oDB.Retrive_Query(_strSQL, out oDataTable);
                            oDB.Retrive_Query(_strAllPagesSQL, out oDataTableAllPages);
                        }
                        if (oDataTable != null)
                        {
                            if (oDataTable.Rows.Count <= 0)
                            {
                                if (oDataTableAllPages != null)
                                {
                                    if (oDataTable != null)
                                    {
                                        oDataTable.Dispose();
                                        oDataTable = null;
                                    }
                                    //oDataTable = new DataTable();


                                    oDataTable = oDataTableAllPages;

                                }
                            }
                        }
                        else //SLR: Changed the logic for if == null also.
                        {
                            oDataTable = oDataTableAllPages;
                        }
                        if (oDataTable != null)
                        {
                            if (oDataTable.Rows.Count > 0)
                            {
                                for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                {
                                    oBasePages.Add(Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"].ToString()), Convert.ToInt64(oDataTable.Rows[i]["eContainerID"].ToString()), Convert.ToInt32(oDataTable.Rows[i]["ContainerPageNumber"].ToString()), Convert.ToInt32(oDataTable.Rows[i]["DocumentPageNumber"].ToString()), oDataTable.Rows[i]["PageName"].ToString(), oDataTable.Rows[i]["BookMarkTag"].ToString(), false, Convert.ToInt64(oDataTable.Rows[i]["ClinicID"].ToString()));
                                    try
                                    {
                                        if (oDataTable.Rows[i]["HasNote"].GetType() != typeof(System.DBNull))
                                        {
                                            oBasePages[oBasePages.Count - 1].HasNotes = (System.Boolean)oDataTable.Rows[i]["HasNote"];
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        _ErrorMessage = ex.ToString();
                                        ErrorMessagees(_ErrorMessage);
                                    }
                                }
                            }
                            oDataTable.Dispose();
                            oDataTable = null;
                        }
                        if (oDB != null)
                        {
                            oDB.Disconnect();
                        }


                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

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
                return oBasePages;
            }
            #endregion "Dhruv 2010 -> GetFilteredBasePages"

            #region "Dhruv 2010 -> GetBasePages"
            public gloEDocumentV3.Document.BasePages GetBasePages(Int64 DocumentID, Int64 ContainerID, Int64 ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Document.BasePages oBasePages = new gloEDocumentV3.Document.BasePages();
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);

                DataTable oDataTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            #region "Added StoredProcdure"


                            gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();

                            if (oDBParams != null)
                            {
                                oDBParams.Clear();
                                oDBParams.Add("@eDocumentID", DocumentID, ParameterDirection.Input, SqlDbType.BigInt);

                                oDBParams.Add("@eContainerID", ContainerID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParams.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    oDB.Retrive("gsp_GetEDocumentDetails_RCM", oDBParams, out oDataTable);
                                }
                                else
                                {
                                    oDB.Retrive("gsp_GetEDocumentDetails", oDBParams, out oDataTable);
                                }

                                oDBParams.Dispose();
                                oDBParams = null;
                            }

                            #endregion
                            
                        }
                        if (oDataTable != null)
                        {
                            if (oDataTable.Rows.Count > 0)
                            {
                                for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                {
                                    oBasePages.Add(Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"].ToString()), Convert.ToInt64(oDataTable.Rows[i]["eContainerID"].ToString()), Convert.ToInt32(oDataTable.Rows[i]["ContainerPageNumber"].ToString()), Convert.ToInt32(oDataTable.Rows[i]["DocumentPageNumber"].ToString()), oDataTable.Rows[i]["PageName"].ToString(), oDataTable.Rows[i]["BookMarkTag"].ToString(), false, Convert.ToInt64(oDataTable.Rows[i]["ClinicID"].ToString()));
                                    try
                                    {
                                        if (oDataTable.Rows[i]["HasNote"].GetType() != typeof(System.DBNull))
                                        {
                                            oBasePages[oBasePages.Count - 1].HasNotes = (System.Boolean)oDataTable.Rows[i]["HasNote"];
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        _ErrorMessage = ex.ToString();
                                        ErrorMessagees(_ErrorMessage);
                                    }
                                }
                            }
                        }
                        if (oDB != null)
                        {
                            oDB.Disconnect();
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

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
                return oBasePages;
            }
            #endregion "Dhruv 2010 -> GetBasePages"

            #region "Dhruv 2010 -> GetDocumentPageCount"
            public Int32 GetDocumentPageCount(Int64 DocumentID, Int64 PatientID, Int64 ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);
                string _strSQL = "";
                Int32 PageCount = 0;
                //    DataTable oDataTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                            {
                                _strSQL = "SELECT PageCounts FROM eDocument_Details_V3_RCM WITH(NOLOCK) WHERE eDocumentID = " + DocumentID + " AND PatientID = " + PatientID + " AND ClinicID = " + ClinicID + " ";
                            }
                            else
                            {
                                _strSQL = "SELECT PageCounts FROM eDocument_Details_V3 WITH(NOLOCK) WHERE eDocumentID = " + DocumentID + " AND PatientID = " + PatientID + " AND ClinicID = " + ClinicID + " ";
                            }

                            PageCount = Convert.ToInt32(oDB.ExecuteScalar_Query(_strSQL));
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

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
                return PageCount;
            }

            #endregion "Dhruv 2010 -> GetDocumentPageCount"

            #endregion

            #region "Acknowledge"
            #region "Dhruv 2010 -> GetAcknowledges"
            public gloEDocumentV3.Common.NTAOs GetAcknowledges(long ContainerID, long DocumentID, long ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Common.NTAOs oAcknowledges = new gloEDocumentV3.Common.NTAOs();
                gloEDocumentV3.Common.NTAO oAcknowledge = null;
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);

                DataTable oDataTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            #region " Retrive Acknowledges "

                            gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();

                            if (oDBParams != null)
                            {
                                oDBParams.Clear();

                                oDBParams.Add("@DocumentID", DocumentID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParams.Add("@ContainerID", ContainerID, ParameterDirection.Input, SqlDbType.BigInt);
                                oDBParams.Add("@NTAOType", enum_NTAOType.Acknowledge.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParams.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    oDB.Retrive("gsp_GetAcknowledges_RCM", oDBParams, out oDataTable);
                                }
                                else
                                {
                                    oDB.Retrive("gsp_GetAcknowledges", oDBParams, out oDataTable);
                                }

                                oDBParams.Dispose();
                                oDBParams = null;
                            }
                        }
                            #endregion

                        if (oDataTable != null && oDataTable.Rows.Count > 0)
                        {
                            for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                            {

                                using (oAcknowledge = new gloEDocumentV3.Common.NTAO())
                                {
                                    if (oAcknowledge != null)
                                    {
                                        oAcknowledge.DocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                        oAcknowledge.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                        oAcknowledge.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                        oAcknowledge.ContainerID = Convert.ToInt64(oDataTable.Rows[i]["eContainerID"]);
                                        oAcknowledge.ContainerPageNumber = Convert.ToInt32(oDataTable.Rows[i]["ContainerPageNumber"]);
                                        oAcknowledge.DocumentPageNumber = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageNumber"]);
                                        oAcknowledge.NTAOID = Convert.ToInt64(oDataTable.Rows[i]["NTAOID"]);
                                        oAcknowledge.UserID = Convert.ToInt64(oDataTable.Rows[i]["UserID"]);
                                        oAcknowledge.UserName = Convert.ToString(oDataTable.Rows[i]["UserName"]);
                                        if (oDataTable.Rows[i]["NTAODateTime"] != DBNull.Value)
                                        { oAcknowledge.NTAODateTime = Convert.ToDateTime(oDataTable.Rows[i]["NTAODateTime"]); }
                                        oAcknowledge.NTAODescription = Convert.ToString(oDataTable.Rows[i]["NTAODescription"]);
                                        oAcknowledge.IsPage = Convert.ToBoolean(oDataTable.Rows[i]["IsPage"]);

                                        oAcknowledge.NTAOType = (enum_NTAOType)Convert.ToInt32(oDataTable.Rows[i]["NTAOType"]);


                                        oAcknowledge.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                        oAcknowledges.Add(oAcknowledge);
                                        //if (oAcknowledge != null)
                                        //{
                                        //    oAcknowledge.Dispose();
                                        //    oAcknowledge = null;
                                        //}
                                    }
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

                }
                finally
                {


                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                    //if (oAcknowledge != null)
                    //{
                    //    oAcknowledge.Dispose();
                    //    oAcknowledge = null;
                    //}
                }
                return oAcknowledges;
            }


            #endregion "Dhruv 2010 -> GetAcknowledges"

            #region "Dhruv 2010 -> GetAcknowledges"
            public gloEDocumentV3.Common.NTAOs GetAcknowledges(DocumentContextMenu.eContextDocuments SelectedDocuments, long ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {

                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                gloEDocumentV3.Common.NTAOs oAcknowledges = new gloEDocumentV3.Common.NTAOs();
                gloEDocumentV3.Common.NTAO oAcknowledge = null;

                string _IncludeDocumentIDs = "";
                DataTable oDataTable = null;

                try
                {
                    _ErrorMessage = "";
                    _HasError = false;
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            if (SelectedDocuments != null && SelectedDocuments.Count > 0)
                            {
                                _IncludeDocumentIDs = "(";
                                for (int i = 0; i <= SelectedDocuments.Count - 1; i++)
                                {
                                    if (i == 0)
                                    {
                                        _IncludeDocumentIDs = _IncludeDocumentIDs + SelectedDocuments[i].DocumentID.ToString();
                                    }
                                    else
                                    {
                                        _IncludeDocumentIDs = _IncludeDocumentIDs + "," + SelectedDocuments[i].DocumentID.ToString();
                                    }
                                }
                                if (_IncludeDocumentIDs == "(")
                                {
                                    _IncludeDocumentIDs = _IncludeDocumentIDs + "0)";
                                }
                                else
                                {
                                    _IncludeDocumentIDs = _IncludeDocumentIDs + ")";
                                }

                                gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();

                                if (oDBParams != null)
                                {
                                    oDBParams.Clear();

                                    oDBParams.Add("@IncludeDocumentIDs", _IncludeDocumentIDs, ParameterDirection.Input, SqlDbType.VarChar);
                                    oDBParams.Add("@NTAOType", enum_NTAOType.Acknowledge.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                    oDBParams.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                    {
                                        oDB.Retrive("gsp_GetAcknowledges_RCM", oDBParams, out oDataTable);
                                    }
                                    else
                                    {
                                        oDB.Retrive("gsp_GetAcknowledges", oDBParams, out oDataTable);
                                    }

                                    oDBParams.Dispose();
                                    oDBParams = null;
                                }

                                if (oDataTable != null && oDataTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                    {

                                        oAcknowledge = new gloEDocumentV3.Common.NTAO();
                                        {
                                            if (oAcknowledge != null)
                                            {
                                                oAcknowledge.DocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                oAcknowledge.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                                oAcknowledge.ContainerID = Convert.ToInt64(oDataTable.Rows[i]["eContainerID"]);
                                                oAcknowledge.ContainerPageNumber = Convert.ToInt32(oDataTable.Rows[i]["ContainerPageNumber"]);
                                                oAcknowledge.DocumentPageNumber = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageNumber"]);
                                                oAcknowledge.NTAOID = Convert.ToInt64(oDataTable.Rows[i]["NTAOID"]);
                                                oAcknowledge.UserID = Convert.ToInt64(oDataTable.Rows[i]["UserID"]);
                                                oAcknowledge.UserName = Convert.ToString(oDataTable.Rows[i]["UserName"]);
                                                if (oDataTable.Rows[i]["NTAODateTime"] != DBNull.Value)
                                                { oAcknowledge.NTAODateTime = Convert.ToDateTime(oDataTable.Rows[i]["NTAODateTime"]); }
                                                oAcknowledge.NTAODescription = Convert.ToString(oDataTable.Rows[i]["NTAODescription"]);
                                                oAcknowledge.IsPage = Convert.ToBoolean(oDataTable.Rows[i]["IsPage"]);
                                                oAcknowledge.NTAOType = (enum_NTAOType)Convert.ToInt32(oDataTable.Rows[i]["NTAOType"]);
                                                oAcknowledge.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                                oAcknowledges.Add(oAcknowledge);
                                                
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    oAcknowledges = null;
                    ErrorMessagees(_ErrorMessage);

                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                }
                return oAcknowledges;
            }

            #endregion "Dhruv 2010 -> GetAcknowledges"

            #endregion

            #region "Notes"

            #region "Dhruv 2010 -> GetNotes"
            public gloEDocumentV3.Common.NTAOs GetNotes(long DocumentID, int DocumentPageNumber, int ContainerPageNumber, long ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Common.NTAOs oNotes = new gloEDocumentV3.Common.NTAOs();
                gloEDocumentV3.Common.NTAO oNote = null;
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);

                DataTable oDataTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            #region " Retrive Notes "

                            #region "Added StoredProcdure"

                            gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();

                            if (oDBParams != null)
                            {
                                oDBParams.Clear();
                                oDBParams.Add("@eDocumentID", DocumentID, ParameterDirection.Input, SqlDbType.BigInt);

                                oDBParams.Add("@DocumentPageNumber", DocumentPageNumber, ParameterDirection.Input, SqlDbType.Int);
                                oDBParams.Add("@ContainerPageNumber", ContainerPageNumber, ParameterDirection.Input, SqlDbType.Int);
                                oDBParams.Add("@NTAOType", enum_NTAOType.Notes.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                                oDBParams.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    oDB.Retrive("gsp_GetEDocumentNotes_RCM", oDBParams, out oDataTable);
                                }
                                else
                                {
                                    oDB.Retrive("gsp_GetEDocumentNotes", oDBParams, out oDataTable);
                                }

                                oDBParams.Dispose();
                                oDBParams = null;

                            }

                            #endregion                           
                        }
                            #endregion

                        if (oDataTable != null && oDataTable.Rows.Count > 0)
                        {
                            for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                            {

                                oNote = new gloEDocumentV3.Common.NTAO();
                                {
                                    if (oNote != null)
                                    {
                                        oNote.DocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                        oNote.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                        oNote.ContainerID = Convert.ToInt64(oDataTable.Rows[i]["eContainerID"]);
                                        oNote.ContainerPageNumber = Convert.ToInt32(oDataTable.Rows[i]["ContainerPageNumber"]);
                                        oNote.DocumentPageNumber = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageNumber"]);
                                        oNote.PageName = Convert.ToString(oDataTable.Rows[i]["PageName"]);
                                        oNote.NTAOID = Convert.ToInt64(oDataTable.Rows[i]["NTAOID"]);
                                        oNote.UserID = Convert.ToInt64(oDataTable.Rows[i]["UserID"]);
                                        oNote.UserName = Convert.ToString(oDataTable.Rows[i]["UserName"]);
                                        if (oDataTable.Rows[i]["NTAODateTime"] != DBNull.Value)
                                        { oNote.NTAODateTime = Convert.ToDateTime(oDataTable.Rows[i]["NTAODateTime"]); }
                                        oNote.NTAODescription = Convert.ToString(oDataTable.Rows[i]["NTAODescription"]);
                                        oNote.IsPage = Convert.ToBoolean(oDataTable.Rows[i]["IsPage"]);
                                        oNote.NTAOType = (enum_NTAOType)Convert.ToInt32(oDataTable.Rows[i]["NTAOType"]);
                                        oNote.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                        oNotes.Add(oNote);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

                }
                finally
                {

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }

                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                }
                return oNotes;
            }
            #endregion "Dhruv 2010 -> GetNotes"

            #region "Dhruv 2010 -> GetNotes"
            public gloEDocumentV3.Common.NTAOs GetNotes(long DocumentID, long ContainerID, int DocumentPageNumber, int ContainerPageNumber, long ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Common.NTAOs oNotes = new gloEDocumentV3.Common.NTAOs();
                gloEDocumentV3.Common.NTAO oNote = null;
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                string _strSQL = "";
                DataTable oDataTable = null;
                try
                {

                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            #region " Retrive Notes "

                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                            {
                                _strSQL = " SELECT eDocument_NTAO_V3_RCM.eDocumentID,ISNULL(eDocument_Details_V3_RCM.DocumentName,'') AS DocumentName, " +
                               " eDocument_NTAO_V3_RCM.eContainerID, eDocument_NTAO_V3_RCM.ContainerPageNumber,  " +
                               " eDocument_NTAO_V3_RCM.DocumentPageNumber, eDocument_NTAO_V3_RCM.NTAOID, ISNULL(eDocument_NTAO_V3_RCM.UserID, 0) AS UserID, " +
                               " ISNULL(eDocument_NTAO_V3_RCM.UserName, '') AS UserName, eDocument_NTAO_V3_RCM.NTAODateTime,  " +
                               " ISNULL(eDocument_NTAO_V3_RCM.NTAODescription, '') AS NTAODescription, ISNULL(eDocument_NTAO_V3_RCM.IsPage, 'false') AS IsPage,  " +
                               " ISNULL(eDocument_NTAO_V3_RCM.NTAOType, 0) AS NTAOType,  " +
                               " ISNULL(eDocument_Pages_V3_RCM.PageName,'') AS PageName, " +
                               " eDocument_NTAO_V3_RCM.ClinicID  " +
                               " FROM         eDocument_NTAO_V3_RCM LEFT OUTER JOIN " +
                               " eDocument_Details_V3_RCM ON eDocument_NTAO_V3_RCM.eDocumentID = eDocument_Details_V3_RCM.eDocumentID " +
                               " LEFT OUTER JOIN eDocument_Pages_V3_RCM ON eDocument_NTAO_V3_RCM.eDocumentID = eDocument_Pages_V3_RCM.eDocumentID " +
                               " AND eDocument_NTAO_V3_RCM.eContainerID = eDocument_Pages_V3_RCM.eContainerID AND " +
                               " eDocument_NTAO_V3_RCM.ContainerPageNumber = eDocument_Pages_V3_RCM.ContainerPageNumber AND " +
                               " eDocument_NTAO_V3_RCM.DocumentPageNumber = eDocument_Pages_V3_RCM.DocumentPageNumber " +
                               " WHERE " +
                               " eDocument_NTAO_V3_RCM.eDocumentID  = " + DocumentID + " " +
                               " AND eDocument_NTAO_V3_RCM.eContainerID = " + ContainerID + " " +
                               " AND eDocument_NTAO_V3_RCM.DocumentPageNumber = " + DocumentPageNumber + " " +
                               " AND eDocument_NTAO_V3_RCM.ContainerPageNumber = " + ContainerPageNumber + " " +
                               " AND eDocument_NTAO_V3_RCM.NTAOType = " + enum_NTAOType.Notes.GetHashCode() + " " +
                               " AND eDocument_NTAO_V3_RCM.ClinicID = " + ClinicID + "";
                            }
                            else
                            {
                                _strSQL = " SELECT eDocument_NTAO_V3.eDocumentID,ISNULL(eDocument_Details_V3.DocumentName,'') AS DocumentName, " +
                               " eDocument_NTAO_V3.eContainerID, eDocument_NTAO_V3.ContainerPageNumber,  " +
                               " eDocument_NTAO_V3.DocumentPageNumber, eDocument_NTAO_V3.NTAOID, ISNULL(eDocument_NTAO_V3.UserID, 0) AS UserID, " +
                               " ISNULL(eDocument_NTAO_V3.UserName, '') AS UserName, eDocument_NTAO_V3.NTAODateTime,  " +
                               " ISNULL(eDocument_NTAO_V3.NTAODescription, '') AS NTAODescription, ISNULL(eDocument_NTAO_V3.IsPage, 'false') AS IsPage,  " +
                               " ISNULL(eDocument_NTAO_V3.NTAOType, 0) AS NTAOType,  " +
                               " ISNULL(eDocument_Pages_V3.PageName,'') AS PageName, " +
                               " eDocument_NTAO_V3.ClinicID  " +
                               " FROM         eDocument_NTAO_V3 LEFT OUTER JOIN " +
                               " eDocument_Details_V3 ON eDocument_NTAO_V3.eDocumentID = eDocument_Details_V3.eDocumentID " +
                               " LEFT OUTER JOIN eDocument_Pages_V3 ON eDocument_NTAO_V3.eDocumentID = eDocument_Pages_V3.eDocumentID " +
                               " AND eDocument_NTAO_V3.eContainerID = eDocument_Pages_V3.eContainerID AND " +
                               " eDocument_NTAO_V3.ContainerPageNumber = eDocument_Pages_V3.ContainerPageNumber AND " +
                               " eDocument_NTAO_V3.DocumentPageNumber = eDocument_Pages_V3.DocumentPageNumber " +
                               " WHERE " +
                               " eDocument_NTAO_V3.eDocumentID  = " + DocumentID + " " +
                               " AND eDocument_NTAO_V3.eContainerID = " + ContainerID + " " +
                               " AND eDocument_NTAO_V3.DocumentPageNumber = " + DocumentPageNumber + " " +
                               " AND eDocument_NTAO_V3.ContainerPageNumber = " + ContainerPageNumber + " " +
                               " AND eDocument_NTAO_V3.NTAOType = " + enum_NTAOType.Notes.GetHashCode() + " " +
                               " AND eDocument_NTAO_V3.ClinicID = " + ClinicID + "";
                            }

                            oDB.Retrive_Query(_strSQL, out oDataTable);

                            #endregion

                            if (oDataTable != null && oDataTable.Rows.Count > 0)
                            {
                                for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                {

                                    oNote = new gloEDocumentV3.Common.NTAO();
                                    {


                                        if (oNote != null)
                                        {
                                            oNote.DocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                            oNote.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                            oNote.ContainerID = Convert.ToInt64(oDataTable.Rows[i]["eContainerID"]);
                                            oNote.ContainerPageNumber = Convert.ToInt32(oDataTable.Rows[i]["ContainerPageNumber"]);
                                            oNote.DocumentPageNumber = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageNumber"]);
                                            oNote.PageName = Convert.ToString(oDataTable.Rows[i]["PageName"]);
                                            oNote.NTAOID = Convert.ToInt64(oDataTable.Rows[i]["NTAOID"]);
                                            oNote.UserID = Convert.ToInt64(oDataTable.Rows[i]["UserID"]);
                                            oNote.UserName = Convert.ToString(oDataTable.Rows[i]["UserName"]);
                                            if (oDataTable.Rows[i]["NTAODateTime"] != DBNull.Value)
                                            { oNote.NTAODateTime = Convert.ToDateTime(oDataTable.Rows[i]["NTAODateTime"]); }
                                            oNote.NTAODescription = Convert.ToString(oDataTable.Rows[i]["NTAODescription"]);
                                            oNote.IsPage = Convert.ToBoolean(oDataTable.Rows[i]["IsPage"]);
                                            oNote.NTAOType = (enum_NTAOType)Convert.ToInt32(oDataTable.Rows[i]["NTAOType"]);
                                            oNote.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                            oNotes.Add(oNote);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);


                }
                finally
                {

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                }
                return oNotes;
            }

            #endregion "Dhruv 2010 -> GetNotes"

            #region "Dhruv 2010 -> GetNotes"
            public gloEDocumentV3.Common.NTAOs GetNotes(DocumentContextMenu.eContextDocuments SelectedDocuments, long ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {

                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                gloEDocumentV3.Common.NTAOs oNotes = new gloEDocumentV3.Common.NTAOs();
                gloEDocumentV3.Common.NTAO oNote = null;
                string _sqlQuery = "";
                string _IncludeDocumentIDs = "";
                DataTable oDataTable = null;

                try
                {
                    _ErrorMessage = "";
                    _HasError = false;


                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {
                            if (SelectedDocuments != null && SelectedDocuments.Count > 0)
                            {
                                _IncludeDocumentIDs = "(";
                                for (int i = 0; i <= SelectedDocuments.Count - 1; i++)
                                {
                                    if (i == 0)
                                    {
                                        _IncludeDocumentIDs = _IncludeDocumentIDs + SelectedDocuments[i].DocumentID.ToString();
                                    }
                                    else
                                    {
                                        _IncludeDocumentIDs = _IncludeDocumentIDs + "," + SelectedDocuments[i].DocumentID.ToString();
                                    }
                                }
                                if (_IncludeDocumentIDs == "(")
                                { _IncludeDocumentIDs = _IncludeDocumentIDs + "0)"; }
                                else
                                {
                                    _IncludeDocumentIDs = _IncludeDocumentIDs + ")";
                                }

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    _sqlQuery = " SELECT eDocument_NTAO_V3_RCM.eDocumentID,ISNULL(eDocument_Details_V3_RCM.DocumentName,'') AS DocumentName, " +
                                " eDocument_NTAO_V3_RCM.eContainerID, eDocument_NTAO_V3_RCM.ContainerPageNumber,  " +
                                " eDocument_NTAO_V3_RCM.DocumentPageNumber, eDocument_NTAO_V3_RCM.NTAOID, ISNULL(eDocument_NTAO_V3_RCM.UserID, 0) AS UserID, " +
                                " ISNULL(eDocument_NTAO_V3_RCM.UserName, '') AS UserName, eDocument_NTAO_V3_RCM.NTAODateTime,  " +
                                " CONVERT(VARCHAR,eDocument_NTAO_V3_RCM.NTAODateTime,100) AS NTADate, " +
                                " ISNULL(eDocument_NTAO_V3_RCM.NTAODescription, '') AS NTAODescription, ISNULL(eDocument_NTAO_V3_RCM.IsPage, 'false') AS IsPage,  " +
                                " ISNULL(eDocument_NTAO_V3_RCM.NTAOType, 0) AS NTAOType,  " +
                                " ISNULL(eDocument_Pages_V3_RCM.PageName,'') AS PageName, " +
                                " eDocument_NTAO_V3_RCM.ClinicID  " +
                                " FROM         eDocument_NTAO_V3_RCM LEFT OUTER JOIN " +
                                " eDocument_Details_V3_RCM ON eDocument_NTAO_V3_RCM.eDocumentID = eDocument_Details_V3_RCM.eDocumentID " +
                                " LEFT OUTER JOIN eDocument_Pages_V3_RCM ON eDocument_NTAO_V3_RCM.eDocumentID = eDocument_Pages_V3_RCM.eDocumentID " +
                                " AND eDocument_NTAO_V3_RCM.eContainerID = eDocument_Pages_V3_RCM.eContainerID AND " +
                                " eDocument_NTAO_V3_RCM.ContainerPageNumber = eDocument_Pages_V3_RCM.ContainerPageNumber AND " +
                                " eDocument_NTAO_V3_RCM.DocumentPageNumber = eDocument_Pages_V3_RCM.DocumentPageNumber " +
                                " WHERE eDocument_NTAO_V3_RCM.eDocumentID IN " + _IncludeDocumentIDs + " AND eDocument_NTAO_V3_RCM.NTAOType = " + enum_NTAOType.Notes.GetHashCode() + " " +
                                " AND eDocument_NTAO_V3_RCM.ClinicID = " + ClinicID + " ORDER BY NTADate , eDocument_NTAO_V3_RCM.DocumentPageNumber";
                                }
                                else
                                {
                                    _sqlQuery = " SELECT eDocument_NTAO_V3.eDocumentID,ISNULL(eDocument_Details_V3.DocumentName,'') AS DocumentName, " +
                                " eDocument_NTAO_V3.eContainerID, eDocument_NTAO_V3.ContainerPageNumber,  " +
                                " eDocument_NTAO_V3.DocumentPageNumber, eDocument_NTAO_V3.NTAOID, ISNULL(eDocument_NTAO_V3.UserID, 0) AS UserID, " +
                                " ISNULL(eDocument_NTAO_V3.UserName, '') AS UserName, eDocument_NTAO_V3.NTAODateTime,  " +
                                " CONVERT(VARCHAR,eDocument_NTAO_V3.NTAODateTime,100) AS NTADate, " +
                                " ISNULL(eDocument_NTAO_V3.NTAODescription, '') AS NTAODescription, ISNULL(eDocument_NTAO_V3.IsPage, 'false') AS IsPage,  " +
                                " ISNULL(eDocument_NTAO_V3.NTAOType, 0) AS NTAOType,  " +
                                " ISNULL(eDocument_Pages_V3.PageName,'') AS PageName, " +
                                " eDocument_NTAO_V3.ClinicID  " +
                                " FROM         eDocument_NTAO_V3 LEFT OUTER JOIN " +
                                " eDocument_Details_V3 ON eDocument_NTAO_V3.eDocumentID = eDocument_Details_V3.eDocumentID " +
                                " LEFT OUTER JOIN eDocument_Pages_V3 ON eDocument_NTAO_V3.eDocumentID = eDocument_Pages_V3.eDocumentID " +
                                " AND eDocument_NTAO_V3.eContainerID = eDocument_Pages_V3.eContainerID AND " +
                                " eDocument_NTAO_V3.ContainerPageNumber = eDocument_Pages_V3.ContainerPageNumber AND " +
                                " eDocument_NTAO_V3.DocumentPageNumber = eDocument_Pages_V3.DocumentPageNumber " +
                                " WHERE eDocument_NTAO_V3.eDocumentID IN " + _IncludeDocumentIDs + " AND eDocument_NTAO_V3.NTAOType = " + enum_NTAOType.Notes.GetHashCode() + " " +
                                " AND eDocument_NTAO_V3.ClinicID = " + ClinicID + " ORDER BY NTADate , eDocument_NTAO_V3.DocumentPageNumber";
                                }

                                oDB.Retrive_Query(_sqlQuery, out oDataTable);

                                if (oDataTable != null && oDataTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                    {

                                        oNote = new gloEDocumentV3.Common.NTAO();
                                        {
                                            if (oNote != null)
                                            {
                                                oNote.DocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                oNote.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                                oNote.ContainerID = Convert.ToInt64(oDataTable.Rows[i]["eContainerID"]);
                                                oNote.ContainerPageNumber = Convert.ToInt32(oDataTable.Rows[i]["ContainerPageNumber"]);
                                                oNote.DocumentPageNumber = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageNumber"]);
                                                oNote.PageName = Convert.ToString(oDataTable.Rows[i]["PageName"]);
                                                oNote.NTAOID = Convert.ToInt64(oDataTable.Rows[i]["NTAOID"]);
                                                oNote.UserID = Convert.ToInt64(oDataTable.Rows[i]["UserID"]);
                                                oNote.UserName = Convert.ToString(oDataTable.Rows[i]["UserName"]);
                                                if (oDataTable.Rows[i]["NTAODateTime"] != DBNull.Value)
                                                { oNote.NTAODateTime = Convert.ToDateTime(oDataTable.Rows[i]["NTAODateTime"]); }
                                                oNote.NTAODescription = Convert.ToString(oDataTable.Rows[i]["NTAODescription"]);
                                                oNote.IsPage = Convert.ToBoolean(oDataTable.Rows[i]["IsPage"]);
                                                oNote.NTAOType = (enum_NTAOType)Convert.ToInt32(oDataTable.Rows[i]["NTAOType"]);
                                                oNote.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                                oNotes.Add(oNote);

                                                //if (oNote != null)
                                                //{
                                                //    oNote.Dispose();
                                                //    oNote = null;
                                                //}
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    oNotes = null;
                    ErrorMessagees(_ErrorMessage);
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                }
                return oNotes;
            }

            #endregion "Dhruv 2010 -> GetNotes"


            #endregion

            #region "Dhruv 2010 -> UserTags"

            #region "User Tags"

            public gloEDocumentV3.Common.NTAOs GetUserTags(long DocumentID, long ContainerID, int DocumentPageNumber, int ContainerPageNumber, long ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                _ErrorMessage = "";
                _HasError = false;
                gloEDocumentV3.Common.NTAOs oUserTags = new gloEDocumentV3.Common.NTAOs();
                gloEDocumentV3.Common.NTAO oUserTag = null;
                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                string _strSQL = "";
                DataTable oDataTable = null;
                try
                {
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            #region " Retrive Notes "

                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                            {
                                _strSQL = " SELECT eDocument_NTAO_V3_RCM.eDocumentID,ISNULL(eDocument_Details_V3_RCM.DocumentName,'') AS DocumentName, " +
                           " eDocument_NTAO_V3_RCM.eContainerID, eDocument_NTAO_V3_RCM.ContainerPageNumber,  " +
                           " eDocument_NTAO_V3_RCM.DocumentPageNumber, eDocument_NTAO_V3_RCM.NTAOID, ISNULL(eDocument_NTAO_V3_RCM.UserID, 0) AS UserID, " +
                           " ISNULL(eDocument_NTAO_V3_RCM.UserName, '') AS UserName, eDocument_NTAO_V3_RCM.NTAODateTime,  " +
                           " ISNULL(eDocument_NTAO_V3_RCM.NTAODescription, '') AS NTAODescription, ISNULL(eDocument_NTAO_V3_RCM.IsPage, 'false') AS IsPage,  " +
                           " ISNULL(eDocument_NTAO_V3_RCM.NTAOType, 0) AS NTAOType,  " +
                           " ISNULL(eDocument_Pages_V3_RCM.PageName,'') AS PageName, " +
                           " eDocument_NTAO_V3_RCM.ClinicID  " +
                           " FROM         eDocument_NTAO_V3_RCM LEFT OUTER JOIN " +
                           " eDocument_Details_V3_RCM ON eDocument_NTAO_V3_RCM.eDocumentID = eDocument_Details_V3_RCM.eDocumentID " +
                           " LEFT OUTER JOIN eDocument_Pages_V3_RCM ON eDocument_NTAO_V3_RCM.eDocumentID = eDocument_Pages_V3_RCM.eDocumentID " +
                           " AND eDocument_NTAO_V3_RCM.eContainerID = eDocument_Pages_V3_RCM.eContainerID AND " +
                           " eDocument_NTAO_V3_RCM.ContainerPageNumber = eDocument_Pages_V3_RCM.ContainerPageNumber AND " +
                           " eDocument_NTAO_V3_RCM.DocumentPageNumber = eDocument_Pages_V3_RCM.DocumentPageNumber " +
                           " WHERE " +
                           " eDocument_NTAO_V3_RCM.eDocumentID  = " + DocumentID + " " +
                           " AND eDocument_NTAO_V3_RCM.eContainerID = " + ContainerID + " " +
                           " AND eDocument_NTAO_V3_RCM.DocumentPageNumber = " + DocumentPageNumber + " " +
                           " AND eDocument_NTAO_V3_RCM.ContainerPageNumber = " + ContainerPageNumber + " " +
                           " AND eDocument_NTAO_V3_RCM.NTAOType = " + enum_NTAOType.Tag.GetHashCode() + " " +
                           " AND eDocument_NTAO_V3_RCM.ClinicID = " + ClinicID + "";
                            }
                            else
                            {
                                _strSQL = " SELECT eDocument_NTAO_V3.eDocumentID,ISNULL(eDocument_Details_V3.DocumentName,'') AS DocumentName, " +
                           " eDocument_NTAO_V3.eContainerID, eDocument_NTAO_V3.ContainerPageNumber,  " +
                           " eDocument_NTAO_V3.DocumentPageNumber, eDocument_NTAO_V3.NTAOID, ISNULL(eDocument_NTAO_V3.UserID, 0) AS UserID, " +
                           " ISNULL(eDocument_NTAO_V3.UserName, '') AS UserName, eDocument_NTAO_V3.NTAODateTime,  " +
                           " ISNULL(eDocument_NTAO_V3.NTAODescription, '') AS NTAODescription, ISNULL(eDocument_NTAO_V3.IsPage, 'false') AS IsPage,  " +
                           " ISNULL(eDocument_NTAO_V3.NTAOType, 0) AS NTAOType,  " +
                           " ISNULL(eDocument_Pages_V3.PageName,'') AS PageName, " +
                           " eDocument_NTAO_V3.ClinicID  " +
                           " FROM         eDocument_NTAO_V3 LEFT OUTER JOIN " +
                           " eDocument_Details_V3 ON eDocument_NTAO_V3.eDocumentID = eDocument_Details_V3.eDocumentID " +
                           " LEFT OUTER JOIN eDocument_Pages_V3 ON eDocument_NTAO_V3.eDocumentID = eDocument_Pages_V3.eDocumentID " +
                           " AND eDocument_NTAO_V3.eContainerID = eDocument_Pages_V3.eContainerID AND " +
                           " eDocument_NTAO_V3.ContainerPageNumber = eDocument_Pages_V3.ContainerPageNumber AND " +
                           " eDocument_NTAO_V3.DocumentPageNumber = eDocument_Pages_V3.DocumentPageNumber " +
                           " WHERE " +
                           " eDocument_NTAO_V3.eDocumentID  = " + DocumentID + " " +
                           " AND eDocument_NTAO_V3.eContainerID = " + ContainerID + " " +
                           " AND eDocument_NTAO_V3.DocumentPageNumber = " + DocumentPageNumber + " " +
                           " AND eDocument_NTAO_V3.ContainerPageNumber = " + ContainerPageNumber + " " +
                           " AND eDocument_NTAO_V3.NTAOType = " + enum_NTAOType.Tag.GetHashCode() + " " +
                           " AND eDocument_NTAO_V3.ClinicID = " + ClinicID + "";
                            }


                            oDB.Retrive_Query(_strSQL, out oDataTable);

                            #endregion

                            if (oDataTable != null && oDataTable.Rows.Count > 0)
                            {
                                for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                {

                                    oUserTag = new gloEDocumentV3.Common.NTAO();
                                    {
                                        if (oUserTag != null)
                                        {
                                            oUserTag.DocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                            oUserTag.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                            oUserTag.ContainerID = Convert.ToInt64(oDataTable.Rows[i]["eContainerID"]);
                                            oUserTag.ContainerPageNumber = Convert.ToInt32(oDataTable.Rows[i]["ContainerPageNumber"]);
                                            oUserTag.DocumentPageNumber = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageNumber"]);
                                            oUserTag.PageName = Convert.ToString(oDataTable.Rows[i]["PageName"]);
                                            oUserTag.NTAOID = Convert.ToInt64(oDataTable.Rows[i]["NTAOID"]);
                                            oUserTag.UserID = Convert.ToInt64(oDataTable.Rows[i]["UserID"]);
                                            oUserTag.UserName = Convert.ToString(oDataTable.Rows[i]["UserName"]);
                                            if (oDataTable.Rows[i]["NTAODateTime"] != DBNull.Value)
                                            { oUserTag.NTAODateTime = Convert.ToDateTime(oDataTable.Rows[i]["NTAODateTime"]); }
                                            oUserTag.NTAODescription = Convert.ToString(oDataTable.Rows[i]["NTAODescription"]);
                                            oUserTag.IsPage = Convert.ToBoolean(oDataTable.Rows[i]["IsPage"]);
                                            oUserTag.NTAOType = (enum_NTAOType)Convert.ToInt32(oDataTable.Rows[i]["NTAOType"]);
                                            oUserTag.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                            oUserTags.Add(oUserTag);
                                            //if (oUserTag != null)
                                            //{
                                            //    oUserTag.Dispose();
                                            //    oUserTag = null;
                                            //}
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    ErrorMessagees(_ErrorMessage);

                }
                finally
                {

                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }

                }
                return oUserTags;
            }
            
            public gloEDocumentV3.Common.NTAOs GetUserTags(DocumentContextMenu.eContextDocuments SelectedDocuments, long ClinicID, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {

                gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString);
                gloEDocumentV3.Common.NTAOs oUserTags = new gloEDocumentV3.Common.NTAOs();
                gloEDocumentV3.Common.NTAO oUserTag = null;
                string _sqlQuery = "";
                string _IncludeDocumentIDs = "";
                DataTable oDataTable = null;

                try
                {
                    _ErrorMessage = "";
                    _HasError = false;
                    if (oDB != null)
                    {
                        if (oDB.Connect(false))
                        {

                            if (SelectedDocuments != null && SelectedDocuments.Count > 0)
                            {
                                _IncludeDocumentIDs = "(";
                                for (int i = 0; i <= SelectedDocuments.Count - 1; i++)
                                {
                                    if (i == 0)
                                    {
                                        _IncludeDocumentIDs = _IncludeDocumentIDs + SelectedDocuments[i].DocumentID.ToString();
                                    }
                                    else
                                    {
                                        _IncludeDocumentIDs = _IncludeDocumentIDs + "," + SelectedDocuments[i].DocumentID.ToString();
                                    }
                                }
                                if (_IncludeDocumentIDs == "(")
                                {
                                    _IncludeDocumentIDs = _IncludeDocumentIDs + "0)";
                                }
                                else
                                {
                                    _IncludeDocumentIDs = _IncludeDocumentIDs + ")";
                                }

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    _sqlQuery = " SELECT eDocument_NTAO_V3_RCM.eDocumentID,ISNULL(eDocument_Details_V3_RCM.DocumentName,'') AS DocumentName, " +
                               " eDocument_NTAO_V3_RCM.eContainerID, eDocument_NTAO_V3_RCM.ContainerPageNumber,  " +
                               " eDocument_NTAO_V3_RCM.DocumentPageNumber, eDocument_NTAO_V3_RCM.NTAOID, ISNULL(eDocument_NTAO_V3_RCM.UserID, 0) AS UserID, " +
                               " ISNULL(eDocument_NTAO_V3_RCM.UserName, '') AS UserName, eDocument_NTAO_V3_RCM.NTAODateTime,  " +
                               " ISNULL(eDocument_NTAO_V3_RCM.NTAODescription, '') AS NTAODescription, ISNULL(eDocument_NTAO_V3_RCM.IsPage, 'false') AS IsPage,  " +
                               " ISNULL(eDocument_NTAO_V3_RCM.NTAOType, 0) AS NTAOType,  " +
                               " ISNULL(eDocument_Pages_V3_RCM.PageName,'') AS PageName, " +
                               " eDocument_NTAO_V3_RCM.ClinicID  " +
                               " FROM   eDocument_NTAO_V3_RCM LEFT OUTER JOIN " +
                               " eDocument_Details_V3_RCM ON eDocument_NTAO_V3_RCM.eDocumentID = eDocument_Details_V3_RCM.eDocumentID " +
                               " LEFT OUTER JOIN eDocument_Pages_V3_RCM ON eDocument_NTAO_V3_RCM.eDocumentID = eDocument_Pages_V3_RCM.eDocumentID " +
                               " AND eDocument_NTAO_V3_RCM.eContainerID = eDocument_Pages_V3_RCM.eContainerID AND " +
                               " eDocument_NTAO_V3_RCM.ContainerPageNumber = eDocument_Pages_V3_RCM.ContainerPageNumber AND " +
                               " eDocument_NTAO_V3_RCM.DocumentPageNumber = eDocument_Pages_V3_RCM.DocumentPageNumber " +
                               " WHERE eDocument_NTAO_V3_RCM.eDocumentID IN " + _IncludeDocumentIDs + " AND " +
                               " eDocument_NTAO_V3_RCM.NTAOType = " + enum_NTAOType.Tag.GetHashCode() + " " +
                               " AND eDocument_NTAO_V3_RCM.ClinicID = " + ClinicID + " ORDER BY eDocument_NTAO_V3_RCM.NTAODateTime ";
                                }
                                else
                                {
                                    _sqlQuery = " SELECT eDocument_NTAO_V3.eDocumentID,ISNULL(eDocument_Details_V3.DocumentName,'') AS DocumentName, " +
                               " eDocument_NTAO_V3.eContainerID, eDocument_NTAO_V3.ContainerPageNumber,  " +
                               " eDocument_NTAO_V3.DocumentPageNumber, eDocument_NTAO_V3.NTAOID, ISNULL(eDocument_NTAO_V3.UserID, 0) AS UserID, " +
                               " ISNULL(eDocument_NTAO_V3.UserName, '') AS UserName, eDocument_NTAO_V3.NTAODateTime,  " +
                               " ISNULL(eDocument_NTAO_V3.NTAODescription, '') AS NTAODescription, ISNULL(eDocument_NTAO_V3.IsPage, 'false') AS IsPage,  " +
                               " ISNULL(eDocument_NTAO_V3.NTAOType, 0) AS NTAOType,  " +
                               " ISNULL(eDocument_Pages_V3.PageName,'') AS PageName, " +
                               " eDocument_NTAO_V3.ClinicID  " +
                               " FROM   eDocument_NTAO_V3 LEFT OUTER JOIN " +
                               " eDocument_Details_V3 ON eDocument_NTAO_V3.eDocumentID = eDocument_Details_V3.eDocumentID " +
                               " LEFT OUTER JOIN eDocument_Pages_V3 ON eDocument_NTAO_V3.eDocumentID = eDocument_Pages_V3.eDocumentID " +
                               " AND eDocument_NTAO_V3.eContainerID = eDocument_Pages_V3.eContainerID AND " +
                               " eDocument_NTAO_V3.ContainerPageNumber = eDocument_Pages_V3.ContainerPageNumber AND " +
                               " eDocument_NTAO_V3.DocumentPageNumber = eDocument_Pages_V3.DocumentPageNumber " +
                               " WHERE eDocument_NTAO_V3.eDocumentID IN " + _IncludeDocumentIDs + " AND " +
                               " eDocument_NTAO_V3.NTAOType = " + enum_NTAOType.Tag.GetHashCode() + " " +
                               " AND eDocument_NTAO_V3.ClinicID = " + ClinicID + " ORDER BY eDocument_NTAO_V3.NTAODateTime ";
                                }

                                oDB.Retrive_Query(_sqlQuery, out oDataTable);

                                if (oDataTable != null && oDataTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                    {

                                        oUserTag = new gloEDocumentV3.Common.NTAO();
                                        {
                                            if (oUserTag != null)
                                            {

                                                oUserTag.DocumentID = Convert.ToInt64(oDataTable.Rows[i]["eDocumentID"]);
                                                oUserTag.DocumentName = Convert.ToString(oDataTable.Rows[i]["DocumentName"]);
                                                oUserTag.ContainerID = Convert.ToInt64(oDataTable.Rows[i]["eContainerID"]);
                                                oUserTag.ContainerPageNumber = Convert.ToInt32(oDataTable.Rows[i]["ContainerPageNumber"]);
                                                oUserTag.DocumentPageNumber = Convert.ToInt32(oDataTable.Rows[i]["DocumentPageNumber"]);
                                                oUserTag.PageName = Convert.ToString(oDataTable.Rows[i]["PageName"]);
                                                oUserTag.NTAOID = Convert.ToInt64(oDataTable.Rows[i]["NTAOID"]);
                                                oUserTag.UserID = Convert.ToInt64(oDataTable.Rows[i]["UserID"]);
                                                oUserTag.UserName = Convert.ToString(oDataTable.Rows[i]["UserName"]);
                                                if (oDataTable.Rows[i]["NTAODateTime"] != DBNull.Value)
                                                { oUserTag.NTAODateTime = Convert.ToDateTime(oDataTable.Rows[i]["NTAODateTime"]); }
                                                oUserTag.NTAODescription = Convert.ToString(oDataTable.Rows[i]["NTAODescription"]);
                                                oUserTag.IsPage = Convert.ToBoolean(oDataTable.Rows[i]["IsPage"]);
                                                oUserTag.NTAOType = (enum_NTAOType)Convert.ToInt32(oDataTable.Rows[i]["NTAOType"]);
                                                oUserTag.ClinicID = Convert.ToInt64(oDataTable.Rows[i]["ClinicID"]);

                                                oUserTags.Add(oUserTag);
                                                //if (oUserTag != null)
                                                //{
                                                //    oUserTag.Dispose();
                                                //    oUserTag = null;
                                                //}
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _HasError = true;
                    _ErrorMessage = ex.Message;
                    oUserTags = null;

                    ErrorMessagees(_ErrorMessage);
                }
                finally
                {
                    if (oDB != null)
                    {
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDataTable != null)
                    {
                        oDataTable.Dispose();
                        oDataTable = null;
                    }
                }
                return oUserTags;
            }

            #endregion

            #endregion "Dhruv 2010 -> UserTags"

            #region "Dhruv 2010 -> GetUsers"
            public ArrayList GetUsers(long ClinicID)
            {
                _ErrorMessage = "";
                _HasError = false;
                System.Collections.ArrayList _Results = new ArrayList();
                using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString))
                {
                    string _strSQL = "";
                    try
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {

                                _strSQL = "SELECT nUserID,sLoginName ,ISNULL(sFirstName,'')+Space(1)+ ISNULL(sMiddleName,'') +Space(1) + ISNULL(sLastName,'') AS UserName FROM User_MST";
                                DataTable oDataTable = null;

                                oDB.Retrive_Query(_strSQL, out oDataTable);
                                if (oDataTable != null)
                                {
                                    if (oDataTable.Rows.Count > 0)
                                    {
                                        for (int i = 0; i <= oDataTable.Rows.Count - 1; i++)
                                        {
                                            _Results.Add(oDataTable.Rows[i]["sLoginName"].ToString());
                                        }
                                    }
                                    oDataTable.Dispose();
                                    oDataTable = null;
                                }
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                        }


                    }

                    catch (Exception ex)
                    {
                        _HasError = true;
                        _ErrorMessage = ex.Message;
                        ErrorMessagees(_ErrorMessage);

                    }
                }

                return _Results;
            }

            #endregion "Dhruv 2010 -> GetUsers"

            internal bool ValidateArchiveDBExists()
            {
                if (checkArchiveDBConnection())
                    if (checkArchiveSchemaExists())
                        return true;
                    else
                    {
                        return false;
                    }
                else
                {
                    return false;
                }
            }

            string _gloArchiveConnString = String.Empty;

            private Boolean checkArchiveSchemaExists()
            {
                //gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(_gloArchiveConnString);
                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(_gloArchiveConnString))
                    {

                        //gloEDocumentV3.gloEDocV3Admin.gloArchiveDatabaseName;
                        if (oDB.Connect(false))
                        {
                            object obj = oDB.ExecuteScalar_Query("SELECT Count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[eDocument_Details_V3]') AND type in (N'U')");
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                            }
                            if (obj != null && Convert.ToInt64(obj) > 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Missing DMS tables in archive database ", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return false;
                    //throw;
                }
                return true;
            }

            private Boolean checkArchiveDBConnection()
            {
                _gloArchiveConnString = gloEDocumentV3.gloEDocV3Admin.getArchiveDatabaseConnectionString;
                if (_gloArchiveConnString != "")
                {
                    SqlConnection strTestConnection = new SqlConnection(_gloArchiveConnString);
                    try
                    {
                        if (strTestConnection.State == ConnectionState.Closed)
                            strTestConnection.Open();
                        return true;
                        //gstrArchiveDatabaseConnectionString 

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error in connecting archive database ", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                        //throw;
                    }
                    finally
                    {
                        if (strTestConnection.State == ConnectionState.Open)
                        {
                            strTestConnection.Close();
                        }
                        if (strTestConnection != null)
                        {
                            strTestConnection.Dispose();
                            strTestConnection = null;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }


        }
    }
}
