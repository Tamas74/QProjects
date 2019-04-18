using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using gloDatabaseLayer;

namespace gloVault.Classes
{

    public class clsVaultDbLayer
    {

        public enum enmMessageTypes
        {
            email,
            data
        }

        #region "Variables"

        string _sConnectionString = string.Empty;

        #endregion

        #region "Constructor & Destructor"
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="sConnectionString"></param>
        public clsVaultDbLayer(string sConnectionString)
        {
            _sConnectionString = sConnectionString;
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

        ~clsVaultDbLayer()
        {
            Dispose(false);
        }

        #endregion

        #region "Generation of Id's"

        /// <summary>
        /// Method to get visitid.
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <returns>Long</returns>
        public Int64 GetVisitId(Int64 nPatientId)
        {
            DBLayer _objDbLayer = new DBLayer(_sConnectionString);
            string _sQuery = string.Empty;
            Int64 _nVisitId = 0;
            object _objValue = null;

            try
            {
                _sQuery = @"select isnull(nVisitId,0) from Visits where convert(datetime,convert (varchar(50),datepart(mm,dtVisitdate)) 
                            + '/'+ convert(varchar(50),datepart(dd,dtVisitdate)) + '/'+ convert(varchar(50),datepart(yy,dtVisitdate))) = '" + DateTime.Now + "' and nPatientId=" + nPatientId + "";

                _objDbLayer.Connect(false);

                _objValue = _objDbLayer.ExecuteScalar_Query(_sQuery);

                if (((_objValue != null)) & !string.IsNullOrEmpty(Convert.ToString(_objValue)))
                {
                    _nVisitId = Convert.ToInt64(_objValue);
                }

                _objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                _nVisitId = 0;
                clsGeneralInterface.UpdateLog("Error in GetVisitId: " + ex.ToString());
            }
            finally
            {
                if (_objDbLayer != null)
                {
                    _objDbLayer.Dispose();
                }
                _objValue = null;
                _sQuery = string.Empty;
            }
            return _nVisitId;
        }

        /// <summary>
        /// Method to generate transactionid.
        /// </summary>
        /// <param name="PatientID"></param>
        /// <returns>Long</returns>

        public Int64 GetPrefixTransactionID(Int64 PatientID)
        {
            Int64 _Result = 0;
            string _result = string.Empty;
            DateTime _PatientDOB = DateTime.Now;
            DateTime _CurrentDate = DateTime.Now;
            DateTime _BaseDate = Convert.ToDateTime("1/1/1900");

            string strID1 = string.Empty;
            string strID2 = string.Empty;
            string strID3 = string.Empty;

            TimeSpan oTS;

            object _internalresult = null;
            string _strSQL = string.Empty;
            DBLayer oDB = new DBLayer(_sConnectionString);

            try
            {
                if (PatientID > 0)
                {
                    oDB.Connect(false);
                    _strSQL = "SELECT dtDOB FROM Patient WHERE nPatientID = " + PatientID + "";
                    _internalresult = oDB.ExecuteScalar_Query(_strSQL);
                    if (_internalresult != null)
                    {
                        if (_internalresult.ToString() != null)
                        {
                            if (_internalresult.GetType() != typeof(System.DBNull))
                            {
                                if (_internalresult.ToString() != "")
                                {
                                    _PatientDOB = Convert.ToDateTime(_internalresult);
                                }
                            }
                        }
                    }
                    oDB.Disconnect();
                }

                _result = "";

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_BaseDate);
                strID1 = oTS.Days.ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _CurrentDate.Subtract(_CurrentDate.Date);
                strID2 = Convert.ToInt32(oTS.TotalSeconds).ToString().Replace("-", "");

                oTS = new TimeSpan();
                oTS = _PatientDOB.Subtract(_BaseDate);
                strID3 = oTS.Days.ToString().Replace("-", "");

                _result = strID1 + strID2 + strID3;

                _Result = Convert.ToInt64(_result);
            }
            catch (Exception ex)
            {
                clsGeneralInterface.UpdateLog(ex.ToString());
                return 0;
            }
            finally
            {
                _internalresult = null;

                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return _Result;
        }

        #endregion

        /// <summary>
        /// Retrives all allergis from database for particular patient.
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <param name="nVisitId"></param>
        /// <returns>Datatabe</returns>
        public DataTable GetAllAlergies(Int64 nPatientId, Int64 nVisitId)
        {
            DBLayer oDBLayer = new DBLayer(_sConnectionString);
            DBParameters oDBParameters = new DBParameters();
            DataTable dtResult = new DataTable();
            try
            {
                oDBLayer.Connect(false);

                oDBParameters.Add("@PatientID", nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@Category", "Allergies", ParameterDirection.Input, SqlDbType.VarChar, 50);
                oDBParameters.Add("@VisitDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                oDBParameters.Add("@VisitID", nVisitId, ParameterDirection.Input, SqlDbType.BigInt);

                //oDBLayer.Retrive("sp_HV_GetLatestAllergies", oDBParameters, out dtResult);
                oDBLayer.Retrive("gsp_HV_GetLatestAllergies", oDBParameters, out dtResult);

                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                clsGeneralInterface.UpdateLog("Error in getAllergies: " + ex.ToString());
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                }
            }
            return dtResult;
        }

        /// <summary>
        /// Method to getLatest medications.
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <param name="nVisitId"></param>
        /// <returns></returns>
        public DataTable GetLatestMedications(Int64 nPatientId, Int64 nVisitId)
        {
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtResult = new DataTable();
            try
            {

                oDBLayer.Connect(false);

                oDBParameters.Add("@PatientID", nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@VisitID", nVisitId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@dtsystemdate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);

                //oDBLayer.Retrive("sp_HV_LatestMedications", oDBParameters, out dtResult);
                oDBLayer.Retrive("gsp_HV_LatestMedications", oDBParameters, out dtResult);

                oDBLayer.Disconnect();

            }
            catch (Exception ex)
            {
                clsGeneralInterface.UpdateLog("Error in getMedications: " + ex.ToString());
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                }
            }
            return dtResult;
        }

        /// <summary>
        /// Method to retrive patients test an lab results.
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <returns></returns>
        public DataTable GetLabTestResults(Int64 nPatientId, Int64 nVisitId)
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            DBParameters oDBParameters = new DBParameters();
            string sQuery = string.Empty;
            DataTable dtTests = new DataTable();
            DataTable dtResults = new DataTable();
            DataTable dtTestResults = new DataTable();

            try
            {
                dtTestResults.Columns.Add("labom_ReceivingFacilityCode");
                dtTestResults.Columns.Add("labotrd_AbnormalFlag");
                dtTestResults.Columns.Add("labom_OrderNoID");
                dtTestResults.Columns.Add("labom_ProviderID");
                dtTestResults.Columns.Add("labotrd_ResultDateTime");
                dtTestResults.Columns.Add("labotrd_ResultName");
                dtTestResults.Columns.Add("labotrd_ResultRange");
                dtTestResults.Columns.Add("labotrd_ResultUnit");
                dtTestResults.Columns.Add("labotrd_ResultValue");
                dtTestResults.Columns.Add("labotr_SpecimenReceivedDateTime");
                dtTestResults.Columns.Add("labotrd_TestName");
                dtTestResults.Columns.Add("labotrd_ResultComment");
                dtTestResults.Columns.Add("labotrd_LOINCID");
                dtTestResults.Columns.Add("labtm_Code");
                dtTestResults.Columns.Add("labotd_TestType");

                sQuery = "select lo.labom_OrderID,lt.labotd_TestID from Lab_Order_MST lo inner join Lab_Order_TestDtl lt on " + "lo.labom_OrderID=lt.labotd_OrderID where labom_PatientID=" + nPatientId;

                objDbLayer.Connect(false);

                objDbLayer.Retrive_Query(sQuery, out dtTests);

                if (dtTests != null && dtTests.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTests.Rows.Count; i++)
                    {
                        oDBParameters = new DBParameters();

                        oDBParameters.Add("@OrderID", dtTests.Rows[i]["labom_OrderID"].ToString(), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@TestID", dtTests.Rows[i]["labotd_TestID"].ToString(), ParameterDirection.Input, SqlDbType.BigInt);
                        oDBParameters.Add("@VisitID", nVisitId, ParameterDirection.Input, SqlDbType.BigInt);

                        //objDbLayer.Retrive("sp_CCDLabResults", oDBParameters, out dtResults);
                        objDbLayer.Retrive("gsp_CCDLabResults", oDBParameters, out dtResults);

                        if (dtResults != null && dtResults.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtResults.Rows.Count; j++)
                            {
                                DataRow drTempRows = dtTestResults.NewRow();

                                drTempRows["labom_ReceivingFacilityCode"] = dtResults.Rows[j]["labom_ReceivingFacilityCode"].ToString();
                                drTempRows["labotrd_AbnormalFlag"] = dtResults.Rows[j]["labotrd_AbnormalFlag"].ToString();
                                drTempRows["labom_OrderNoID"] = dtResults.Rows[j]["labom_OrderNoID"];
                                drTempRows["labom_ProviderID"] = dtResults.Rows[j]["labom_ProviderID"];
                                drTempRows["labotrd_ResultDateTime"] = dtResults.Rows[j]["labotrd_ResultDateTime"];
                                drTempRows["labotrd_ResultName"] = dtResults.Rows[j]["labotrd_ResultName"];
                                drTempRows["labotrd_ResultRange"] = dtResults.Rows[j]["labotrd_ResultRange"];
                                drTempRows["labotrd_ResultUnit"] = dtResults.Rows[j]["labotrd_ResultUnit"];
                                drTempRows["labotrd_ResultValue"] = dtResults.Rows[j]["labotrd_ResultValue"];

                                if (dtResults.Rows[j]["labotr_SpecimenReceivedDateTime"] != null && dtResults.Rows[j]["labotr_SpecimenReceivedDateTime"].ToString().Length > 0)
                                {
                                    drTempRows["labotr_SpecimenReceivedDateTime"] = dtResults.Rows[j]["labotr_SpecimenReceivedDateTime"];
                                }
                                if (dtResults.Rows[j]["labotrd_ResultDateTime"] != null && dtResults.Rows[j]["labotrd_ResultDateTime"].ToString().Length > 0)
                                {
                                    drTempRows["labotrd_ResultDateTime"] = dtResults.Rows[j]["labotrd_ResultDateTime"];
                                }
                                drTempRows["labotrd_TestName"] = dtResults.Rows[j]["labotrd_TestName"];
                                drTempRows["labotrd_ResultComment"] = dtResults.Rows[j]["labotrd_ResultComment"];
                                drTempRows["labotrd_LOINCID"] = dtResults.Rows[j]["labotrd_LOINCID"];
                                drTempRows["labtm_Code"] = dtResults.Rows[j]["labtm_Code"];
                                drTempRows["labotd_TestType"] = dtResults.Rows[j]["labotd_TestType"];

                                dtTestResults.Rows.Add(drTempRows);
                                drTempRows = null;
                            }

                        }
                        oDBParameters = null;
                    }
                }
            }
            catch (Exception ex)
            {
                dtTestResults = null;
                clsGeneralInterface.UpdateLog("Error in lab results: " + ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                }

                if (dtResults != null)
                {
                    dtResults.Dispose();
                }
                if (dtTests != null)
                {
                    dtTests.Dispose();
                }

                sQuery = string.Empty;
            }
            return dtTestResults;

        }

        /// <summary>
        /// Method to retrive patient information.
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <returns></returns>
        public DataTable GetPatientInfo(Int64 nPatientId)
        {
            DBLayer objDblayer = new DBLayer(_sConnectionString);
            DataTable dtResult = new DataTable();
            string sQuery = string.Empty;
            try
            {
                sQuery = @"SELECT     Patient.sPatientCode AS spatientcode, ISNULL(Patient.sFirstName, '') AS 'sFirstName', ISNULL(Patient.sMiddleName, '') AS 'sMiddleName', 
                      ISNULL(Patient.sLastName, '') AS 'sLastName', ISNULL(Patient.sGender, '') AS 'sGender', Patient.dtDOB, ISNULL(Patient.sAddressLine1, '') 
                      AS 'sAddressLine1', ISNULL(Patient.sAddressLine2, '') AS 'sAddressLine2', ISNULL(Patient.sCity, '') AS 'sCity', ISNULL(Patient.sState, '') AS 'sState', 
                      ISNULL(Patient.sZIP, '') AS 'sZip', ISNULL(Patient.sCounty, '') AS 'sCounty', ISNULL(Patient.nSSN, '') AS nSSN, ISNULL(Patient.sMaritalStatus, '') 
                      AS sMaritalStatus, ISNULL(Patient.sPhone, '') AS sPhone, ISNULL(Patient.sMobile, '') AS sMobile, ISNULL(Patient.sEmail, '') AS sEmail, 
                      ISNULL(Patient.sRace, '') AS sRace, COALESCE (Provider_MST.sFirstName, '') + ' ' + COALESCE (Provider_MST.sLastName, '') AS Provider, 
                      PatientExternalCodes.sExternalValue as sPrId, PatientExternalCodes.sExternalSystemCode as sRcId                        FROM         Patient INNER JOIN
                      Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID INNER JOIN
                      PatientExternalCodes ON Patient.nPatientID = PatientExternalCodes.nPatientId
                      where Patient.nPatientId =" + nPatientId + "  and sModuleName='HEALTHVAULT'and sExternalType ='HVSEND'AND nExternalStatus='1'  ";


                objDblayer.Connect(false);

                objDblayer.Retrive_Query(sQuery, out dtResult);

                objDblayer.Disconnect();

            }
            catch (Exception ex)
            {
                dtResult = null;
                clsGeneralInterface.UpdateLog("Error in Retriveing Patientinfo: " + ex.ToString());
            }
            finally
            {
                if (objDblayer != null)
                {
                    objDblayer.Dispose();
                }
                sQuery = string.Empty;
            }
            return dtResult;

        }

        /// <summary>
        /// Method to retive patietninfo
        /// </summary>
        /// <param name="nPatientid"></param>
        /// <returns></returns>
        public DataTable RetrivePatientInfo(Int64 nPatientid)
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;
            DataTable dtPatientList = new DataTable();

            try
            {
                sQuery = @"SELECT     dbo.Clinic_MST.sSiteID AS AUSID, dbo.Clinic_MST.sClinicName AS PracticeName,imgcliniclogo as logoimage,dbo.Patient.nPatientID AS PatientId, 
                      dbo.Patient.sPatientCode AS PatientCode, dbo.Patient.sFirstName AS FirstName, dbo.Patient.sMiddleName AS MiddleName, 
                      dbo.Patient.sLastName AS LastName, dbo.Patient.nSSN AS SSN, convert(varchar,dbo.Patient.dtDOB,103) AS DOB, dbo.Patient.sGender AS Gender, 
                      dbo.Patient.sAddressLine1 AS AddressLine1, dbo.Patient.sAddressLine2 AS AddressLine2, dbo.Patient.sEmail AS Email, SPACE(10) as Description
                      FROM         dbo.Patient INNER JOIN  dbo.Clinic_MST ON dbo.Patient.nClinicID = dbo.Clinic_MST.nClinicID where nPatientID=" + nPatientid;

                objDbLayer.Connect(false);

                objDbLayer.Retrive_Query(sQuery, out dtPatientList);

                objDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                dtPatientList = null;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                sQuery = string.Empty;
            }
            return dtPatientList;
        }

        /// <summary>
        /// Method to retrive problem list.
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <returns></returns>
        public DataTable GetProblemList(Int64 nPatientId, Int64 nVisitId)
        {
            DBLayer oDBLayer = new DBLayer(_sConnectionString);
            DBParameters oDBParameters = new DBParameters();
            DataTable dtResult = new DataTable();
            try
            {
                oDBLayer.Connect(false);

                oDBParameters.Add("@PatientID", nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@VisitId", nVisitId, ParameterDirection.Input, SqlDbType.BigInt);

                //oDBLayer.Retrive("sp_HV_PatientProblems", oDBParameters, out dtResult);
                oDBLayer.Retrive("gsp_HV_PatientProblems", oDBParameters, out dtResult);

                oDBLayer.Disconnect();

            }
            catch (Exception ex)
            {
                dtResult = null;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                }

                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                }
            }
            return dtResult;
        }

        //<summary>
        //Method to retrive messagequeue
        //</summary>
        //<returns></returns>
        public DataTable GetglMessageQueue(enmMessageTypes eMessTypes)
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;
            DataTable dtResult = new DataTable();

            try
            {
                if (eMessTypes == enmMessageTypes.email)
                {
                    sQuery = "SELECT nMessageId,nPatientId,nOtherID,sField1,ISNULL(sMachineName,'') as sMachineName  from Gl_Messagequeue Where sMessageName='HEALTHVAULT-EMAIL' AND sServiceName='RXSNIFFER' AND nStatus=1";
                }
                else if (eMessTypes == enmMessageTypes.data)
                {
                    sQuery = "SELECT nMessageId,nPatientId,nOtherID,sField1,sField2,ISNULL(sMachineName,'') as sMachineName from Gl_Messagequeue Where sMessageName='HEALTHVAULT-DATA' AND sServiceName='RXSNIFFER' AND nStatus=1";
                }
                objDbLayer.Connect(false);

                objDbLayer.Retrive_Query(sQuery, out dtResult);

                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                dtResult = null;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
            }
            return dtResult;

        }

        /// <summary>
        /// Method to update messagequeue.
        /// </summary>
        /// <param name="nMessageId"></param>
        /// <param name="nStatusType"></param>
        public void UpdateMessageQueue(Int64 nMessageId, int nStatusType)
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;

            try
            {
                sQuery = "update Gl_Messagequeue SET nStatus='" + nStatusType + "' where sServiceName='RXSNIFFER' AND nMessageID='" + nMessageId + "'";

                objDbLayer.Connect(false);

                objDbLayer.Execute_Query(sQuery);

                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
            }
        }

        /// <summary>
        /// GetLatestLabResults
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <returns></returns>
        public DataTable GetLatestLabResults(Int64 nPatientId)
        {
            DataTable dtResults = new DataTable();
            DataTable dtTestResults = new DataTable();
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;

            try
            {
                sQuery = @"SELECT     dbo.Lab_Order_Test_ResultDtl.labotrd_TestResultNumber AS LabTestResultNo, 
                dbo.Lab_Order_Test_ResultDtl.labotrd_ResultLineNo AS LabTestResultLineNo, 
                dbo.Lab_Order_Test_ResultDtl.labotrd_ResultName AS LabTestResultName, 
                dbo.Lab_Order_Test_ResultDtl.labotrd_ResultValue AS LabTestResultValue, 
                dbo.Lab_Order_Test_ResultDtl.labotrd_ResultUnit AS LabTestResultUnit, 
                dbo.Lab_Order_Test_ResultDtl.labotrd_ResultRange AS LabTestResultRange, 
                dbo.Lab_Order_Test_ResultDtl.labotrd_ResultType AS LabTestResultType, 
                dbo.Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag AS LabTestResultAbnormalFlag, 
                dbo.Lab_Order_Test_ResultDtl.labotrd_ResultComment AS LabTestResultComment, 
                dbo.Lab_Order_Test_ResultDtl.labotrd_ResultDateTime AS LabTestResultDateTime, 
                dbo.Lab_Order_Test_ResultDtl.labotrd_TestName AS LabTestName,
                dbo.Lab_Order_TestDtl.labotd_DateTime AS LabTestDate, 
                dbo.Lab_Order_Test_ResultDtl.labotrd_ProducerIdentifier AS LabName,
                COALESCE(dbo.Provider_MST.sFirstName,'') +' '+ COALESCE(dbo.Provider_MST.sLastName,'') as ProviderName
                FROM         Lab_Order_MST INNER JOIN
                Provider_MST ON Lab_Order_MST.labom_ProviderID = Provider_MST.nProviderID INNER JOIN
                Lab_Order_TestDtl ON Lab_Order_MST.labom_OrderID = Lab_Order_TestDtl.labotd_OrderID INNER JOIN
                Lab_Order_Test_Result ON Lab_Order_TestDtl.labotd_OrderID = Lab_Order_Test_Result.labotr_OrderID AND 
                Lab_Order_TestDtl.labotd_TestID = Lab_Order_Test_Result.labotr_TestID INNER JOIN
                Lab_Order_Test_ResultDtl ON Lab_Order_Test_Result.labotr_TestID = Lab_Order_Test_ResultDtl.labotrd_TestID AND 
                Lab_Order_Test_Result.labotr_TestResultNumber = Lab_Order_Test_ResultDtl.labotrd_TestResultNumber AND 
                Lab_Order_Test_Result.labotr_OrderID = Lab_Order_Test_ResultDtl.labotrd_OrderID
                Where dbo.Lab_Order_MST.labom_PatientID=  'nPPatientID'";

                sQuery = sQuery.Replace("nPPatientID", nPatientId.ToString());

                objDbLayer.Connect(false);

                objDbLayer.Retrive_Query(sQuery, out dtResults);

                objDbLayer.Disconnect();

                if (dtResults == null && dtResults.Rows.Count <= 0)
                {
                    return null;
                }

                dtTestResults.Columns.Add("LabTestName");
                dtTestResults.Columns.Add("LabTestDate");
                dtTestResults.Columns.Add("LabName");
                dtTestResults.Columns.Add("ProviderName");
                dtTestResults.Columns.Add("LabTestResultName");
                dtTestResults.Columns.Add("LabTestResultValue");
                dtTestResults.Columns.Add("LabTestResultUnit");
                dtTestResults.Columns.Add("LabTestResultRange");
                dtTestResults.Columns.Add("LabTestResultType");
                dtTestResults.Columns.Add("LabTestResultAbnormalFlag");
                dtTestResults.Columns.Add("LabTestResultDateTime", System.Type.GetType("System.DateTime"));
                dtTestResults.Columns.Add("LabTestResultComment");

                for (int j = 0; j < dtResults.Rows.Count; j++)
                {
                    DataRow drTempRows = dtTestResults.NewRow();

                    drTempRows["LabTestName"] = dtResults.Rows[j]["LabTestName"].ToString();
                    drTempRows["LabTestDate"] = dtResults.Rows[j]["LabTestDate"].ToString();
                    drTempRows["LabName"] = dtResults.Rows[j]["LabName"];
                    drTempRows["ProviderName"] = dtResults.Rows[j]["ProviderName"];
                    drTempRows["LabTestResultName"] = dtResults.Rows[j]["LabTestResultName"];
                    drTempRows["LabTestResultValue"] = dtResults.Rows[j]["LabTestResultValue"];
                    drTempRows["LabTestResultUnit"] = dtResults.Rows[j]["LabTestResultUnit"];
                    drTempRows["LabTestResultRange"] = dtResults.Rows[j]["LabTestResultRange"];
                    drTempRows["LabTestResultType"] = dtResults.Rows[j]["LabTestResultType"];
                    drTempRows["LabTestResultAbnormalFlag"] = dtResults.Rows[j]["LabTestResultAbnormalFlag"];
                    drTempRows["LabTestResultDateTime"] = dtResults.Rows[j]["LabTestResultDateTime"];
                    drTempRows["LabTestResultComment"] = dtResults.Rows[j]["LabTestResultComment"];

                    dtTestResults.Rows.Add(drTempRows);
                    drTempRows = null;
                }
            }
            catch (Exception ex)
            {
                dtTestResults = null;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }

                if (dtResults != null)
                {
                    dtResults.Dispose();
                }
                sQuery = string.Empty;
            }

            return dtTestResults;
        }

        /// <summary>
        /// Method to insert email response to patient external ccodes table
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <param name="sEmail"></param>
        /// <param name="sResponseDesc"></param>
        public void InsertEmailResponseInExternalTb(Int64 nPatientId, string sEmail, string sResponseDesc)
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;

            try
            {
                sQuery = "DELETE PatientExternalCodes WhERE nPatientId='" + nPatientId + "' AND (sExternalType='EMAIL' OR sExternalType='HVSEND') AND sModuleName='HEALTHVAULT'";

                objDbLayer.Connect(false);

                objDbLayer.Execute_Query(sQuery);

                sQuery = string.Empty;

                sQuery = @"DECLARE @ID numeric(18,0) EXEC SP_GetUniqueID @ID OUTPUT INSERT INTO [PatientExternalCodes] ([nPatientExternalID],[nPatientId],[sExternalType],[sExternalSubType],[sExternalValue],[sExternalDescription],[sModuleName],[sExternalSystemName],[sExternalSystemCode],[dtAccessDate],[nExternalStatus])
                           VALUES( @ID, '" + nPatientId + "'  ,'EMAIL', '" + sEmail + "',0,'" + sResponseDesc + "','HEALTHVAULT',NULL,NULL,NULL,11)";


                objDbLayer.Execute_Query(sQuery);

                objDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                clsGeneralInterface.UpdateLog("Error in Saving EmailResponse :" + ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                sQuery = string.Empty;
            }
        }

        /// <summary>
        /// Method to save patient externalcodes.
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <param name="sPrId"></param>
        /// <param name="sRcId"></param>
        public bool SavePatientHealthVaultId(Int64 nPatientId, string sPrId, string sRcId, Int64 nStatus, string sEmail)
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;
            bool blnResult = false;

            try
            {
                sQuery = "Update PatientExternalCodes SET sExternalType='HVSEND',sExternalValue='" + sPrId + "',sExternalSystemCode='" + sRcId + "',nExternalStatus='" + nStatus + "',sExternalSubType='" + sEmail + "' Where nPatientId='" + nPatientId + "' AND sExternalType='EMAIL' AND nExternalStatus=11 AND sModuleName='HEALTHVAULT'";

                objDbLayer.Connect(false);

                int i = objDbLayer.Execute_Query(sQuery);

                if (i > 0)
                {
                    blnResult = true;
                }

                ///Save Registerd datetime.
                SaveLastAccessTime(nPatientId, DateTime.Now);

                objDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                blnResult = false;
                clsGeneralInterface.UpdateLog("Error in Saving patient healthVaultId :" + ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                sQuery = string.Empty;
            }
            return blnResult;
        }

        /// <summary>
        /// Method to save last accesstime for the particular patient in patient externalcodes.
        ///  </summary>
        /// <param name="nPatientId"></param>
        /// <param name="dtAccessTime"></param>
        public void SaveLastAccessTime(Int64 nPatientId, DateTime dtAccessTime)
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;

            try
            {
                sQuery = "Update PatientExternalCodes SET dtAccessDate='" + dtAccessTime + "' WHERE sExternalType='HVSEND'AND nPatientId='" + nPatientId + "' AND sModuleName='HEALTHVAULT'";

                objDbLayer.Connect(false);

                objDbLayer.Execute_Query(sQuery);

                objDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                clsGeneralInterface.UpdateLog("Error in Saving patient healthVaultId :" + ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                sQuery = string.Empty;
            }
        }

        /// <summary>
        /// Method to retrive patient PatientEmail.
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <returns></returns>
        public string GetPatientEmailId(Int64 nPatientId)
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;
            string sEmail = string.Empty;

            try
            {
                sQuery = "SELECT ISNULL(sEmail,'')as Email FROM Patient WHERE nPatientID=" + nPatientId;

                objDbLayer.Connect(false);

                sEmail = (string)objDbLayer.ExecuteScalar_Query(sQuery);

                objDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                sEmail = string.Empty;
                clsGeneralInterface.UpdateLog("Error while retriving patient email :" + ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                sQuery = string.Empty;
            }
            return sEmail;
        }

        /// <summary>
        ///method to retrive modules last access time.
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <returns></returns>
        public DateTime GetLastAccessTime(Int64 nPatientId, string sModuleType)
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;
            DateTime dtTime = new DateTime();
            object objResult = null;

            try
            {
                sQuery = "SELECT dtAccessDate FROM PatientExternalCodes WHERE nPatientId=" + nPatientId + " AND sExternalSubType='" + sModuleType + "' AND sMODULENAME='HEALTHVAULT' AND sExternalType='HV-LASTSENT'";

                objDbLayer.Connect(false);

                objResult = objDbLayer.ExecuteScalar_Query(sQuery);

                if (((objResult != null)) & !string.IsNullOrEmpty(Convert.ToString(objResult)))
                {
                    dtTime = Convert.ToDateTime(objResult);
                }

                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                clsGeneralInterface.UpdateLog("Error while retriving patient email :" + ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                sQuery = string.Empty;
                objResult = null;
            }
            return dtTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ClsgloVaultConfigFields> getGloVaultConfigurations()
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;
            DataTable dtResult = new DataTable();
            ClsgloVaultConfigFields objFields = null;
            List<ClsgloVaultConfigFields> lsConfigFields = new List<ClsgloVaultConfigFields>();
            try
            {
                sQuery = "SELECT ISNULL(nModuleId,0) as ModuleId, ISNULL(sModName,'') as ModuleName from gl_modulesMst where sType='gloVault'";

                objDbLayer.Connect(false);

                objDbLayer.Retrive_Query(sQuery, out dtResult);

                objDbLayer.Disconnect();

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        objFields = new ClsgloVaultConfigFields();
                        objFields.nConfigId = Convert.ToInt64(dtResult.Rows[i]["ModuleId"]);
                        objFields.sConfigName = Convert.ToString(dtResult.Rows[i]["ModuleName"]);

                        lsConfigFields.Add(objFields);
                        objFields = null;
                    }
                }

            }
            catch (Exception ex)
            {
                lsConfigFields = null;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
            }
            return lsConfigFields;
        }

        /// <summary>
        /// Method to retrive AUSId
        /// </summary>
        /// <returns></returns>
        public string RetriveAusId()
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;
            object objResult = null;
            string sValue = string.Empty;

            try
            {
                sQuery = "select ISNULL(sSiteID,'') as AUSID from Clinic_MST";

                objDbLayer.Connect(false);
                objResult = objDbLayer.ExecuteScalar_Query(sQuery);

                if (((objResult != null)) & !string.IsNullOrEmpty(Convert.ToString(objResult)))
                {
                    sValue = Convert.ToString(objResult);
                }

                objDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                sValue = string.Empty;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                objResult = null;
                sQuery = string.Empty;
            }
            return sValue;
        }

        /// <summary>
        /// Method to retrive processids
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <returns></returns>
        public string GetAccessId(Int64 nPatientId)
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;
            string sCode = string.Empty;
            DataTable dtResult = new DataTable();

            try
            {
                sQuery = "Select sExternalValue,sExternalSystemCode from PatientExternalCodes where sExternalType='HVSEND' AND sModuleName='HEALTHVAULT' AND nPatientId='" + nPatientId + "'";

                objDbLayer.Connect(false);

                objDbLayer.Retrive_Query(sQuery, out dtResult);

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    if (dtResult.Rows[0]["sExternalValue"].ToString().Length > 0 && dtResult.Rows[0]["sExternalSystemCode"].ToString().Length > 0)
                    {
                        sCode = dtResult.Rows[0]["sExternalValue"].ToString() + "," + dtResult.Rows[0]["sExternalSystemCode"].ToString();
                    }
                }

                objDbLayer.Disconnect();
            }
            catch (Exception ex)
            {
                sCode = string.Empty;
                clsGeneralInterface.UpdateLog("Error while retriving patient email :" + ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                sQuery = string.Empty;

            }
            return sCode;
        }

        /// <summary>
        /// Method to insert audittrail.
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <param name="sModuleName"></param>
        /// <param name="sDescription"></param>
        /// <param name="blnResultType"></param>
        /// <param name="sUserName"></param>
        /// <param name="nUserId"></param>
        /// <param name="sMachineName"></param>
        /// <returns></returns>
        public bool InsertAuditLog(Int64 nPatientId, string sDescription, bool blnResultType, Int64 nUserId, string sMachineName, string sProcessType)
        {
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            bool blnResult = false;

            string sUserName = string.Empty;

            try
            {
                sUserName = RetriveUserName(nUserId);

                if (sUserName.Length <= 0)
                {
                    clsGeneralInterface.UpdateLog("Inserting in audittrail failed.");
                    return false;
                }

                oDBLayer.Connect(false);

                oDBParameters.Add("@nAuditTrailID", 0, ParameterDirection.Output, SqlDbType.BigInt);
                oDBParameters.Add("@dtActivityDateTime", DateTime.Now.ToString(), ParameterDirection.Input, SqlDbType.DateTime);

                if (sProcessType == "email")
                {
                    oDBParameters.Add("@sActivityModule", "Email", ParameterDirection.Input, SqlDbType.VarChar);
                }
                else if (sProcessType == "information")
                {
                    oDBParameters.Add("@sActivityModule", "Send Information", ParameterDirection.Input, SqlDbType.VarChar);
                }
                else if (sProcessType == "CCD")
                {
                    oDBParameters.Add("@sActivityModule", "CCD Information", ParameterDirection.Input, SqlDbType.VarChar);
                }

                oDBParameters.Add("@sActivityCategory", "HealthVault", ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sActivityType", "ClinicalExchange", ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sDescription", sDescription, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nPatientID", nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nProviderID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nUserID", nUserId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sUserName", sUserName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sMachineName", sMachineName, ParameterDirection.Input, SqlDbType.VarChar);

                if (blnResultType)
                {
                    oDBParameters.Add("@sOutcome", "Success", ParameterDirection.Input, SqlDbType.VarChar);
                }
                else
                {
                    oDBParameters.Add("@sOutcome", "Failure", ParameterDirection.Input, SqlDbType.VarChar);
                }

                oDBParameters.Add("@sSoftwareComponent", "gloEMR", ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@nClinicID", 0, ParameterDirection.Input, SqlDbType.BigInt);

                //oDBLayer.Execute("sp_INSERT_AuditTrail", oDBParameters);
                oDBLayer.Execute("gsp_INSERT_AuditTrail", oDBParameters);
                blnResult = true;

                oDBLayer.Disconnect();

            }
            catch (Exception ex)
            {
                blnResult = false;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                }

            }
            return blnResult;
        }

        /// <summary>
        /// Method to retrive Username;
        /// </summary>
        /// <param name="nUserId"></param>
        /// <returns></returns>
        private string RetriveUserName(Int64 nUserId)
        {
            DBLayer objDbLayer = new DBLayer(_sConnectionString);
            string sQuery = string.Empty;
            object objResult = null;
            string sUserName = string.Empty;
            try
            {
                sQuery = "Select ISNULL(sLoginName,'') as UserName from user_mst where nuserid=" + nUserId;

                objDbLayer.Connect(false);

                objResult = objDbLayer.ExecuteScalar_Query(sQuery);

                if (((objResult != null)) & !string.IsNullOrEmpty(Convert.ToString(objResult)))
                {
                    sUserName = Convert.ToString(objResult);
                }

                objDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                sUserName = string.Empty;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
                objDbLayer = null;
                sQuery = string.Empty;
            }
            return sUserName;
        }

        /// <summary>
        /// Method to save last accessed modules.
        /// </summary>
        /// <param name="nPatientId"></param>
        /// <param name="lsConfigIds"></param>
        /// <returns></returns>
        public bool SaveLastAccessedModules(Int64 nPatientId, List<Int64> lsConfigIds)
        {
            bool blnResult = false;

            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(_sConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters;
            try
            {
                oDBLayer.Connect(false);
                foreach (Int64 nValue in lsConfigIds)
                {
                    oDBParameters = new gloDatabaseLayer.DBParameters();

                    oDBParameters.Add("@nPatientId", nPatientId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBParameters.Add("@sExternalSubType", nValue.ToString(), ParameterDirection.Input, SqlDbType.VarChar);
                    oDBParameters.Add("@dtLastAccess", DateTime.Now.ToString(), ParameterDirection.Input, SqlDbType.DateTime);

                    //oDBLayer.Execute("sp_INUP_HV_PatientExternalCodes", oDBParameters);
                    oDBLayer.Execute("gsp_INUP_HV_PatientExternalCodes", oDBParameters);

                    oDBParameters = null;
                }
                blnResult = true;
                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                blnResult = false;
                clsGeneralInterface.UpdateLog(ex.ToString());
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                }
            }
            return blnResult;
        }



    }



}
