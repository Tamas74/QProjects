using System;
using System.Data;
using System.IO;

namespace gloEmdeonInterface.Classes
{


    class ClsMidmarkECGLayer
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

        public ClsMidmarkECGLayer()
        {

        }

        ~ClsMidmarkECGLayer()
        {
            Dispose(false);
        }


        #endregion "Constructor and Destructor"

        #region "Class varribales &  Properties"

        private string _sgloEMRConnectionString = string.Empty;
        private string _sDeviceConncetionstring = string.Empty;
        private long _nPatientID = 0;
        private long _nClinicID = 0;
        private long _nVisitID=0;
        private long _nloginUserID = 0;
        private string _loginUserName = string.Empty;
        private string _sInterpretation = string.Empty;  
        private long _nECGID=0;
        private DateTime _dtOrderDate = default(DateTime);
        private DateTime _dtReviewDate = default(DateTime);  
        private string _PR  = string.Empty;
        private string _QT = string.Empty;
        private string _QTC = string.Empty;
        private string _QRSDuration = string.Empty;
        private string _Paxis = string.Empty;
        private string _QRSaxis = string.Empty;
        private string _Taxis = string.Empty;
        private string _ReportFilePath = string.Empty;
        private string _docName = string.Empty; 
        private long _nDocumentID = 0;

        public string sgloEMRConnectionString
        {
            get { return _sgloEMRConnectionString; }
            set { _sgloEMRConnectionString = value; }
        }
        
        public string sDeviceConnectionAtring
        {
            get { return _sDeviceConncetionstring; }
            set { _sDeviceConncetionstring = value; }
        }

        public long PatientID { get { return _nPatientID; } set { _nPatientID = value; } }

        public long ClinicID { get { return _nClinicID; } set { _nClinicID = value; } }

        public long VisitID { get { return _nVisitID; } set { _nVisitID = value; } }

        public long LoginUserID { get { return _nloginUserID; } set { _nloginUserID = value; } }

        public string LoginUserName { get { return _loginUserName; } set { _loginUserName = value; } }

        public string Interpretation { get { return _sInterpretation; } set { _sInterpretation = value; } }

        public long ECGID { get { return _nECGID; } set { _nECGID = value; } }

        public DateTime OrderDateTime { get { return _dtOrderDate; } set { _dtOrderDate = value; } }

        public DateTime ReviewDateTime { get { return _dtReviewDate; } set { _dtReviewDate = value; } }

        public string PR { get { return _PR; } set { _PR = value; } }

        public string QT { get { return _QT; } set { _QT = value; } }

        public string QTC { get { return _QTC; } set { _QTC = value; } }

        public string QRSDuration { get { return _QRSDuration; } set { _QRSDuration = value; } }

        public string Paxis { get { return _Paxis; } set { _Paxis = value; } }

        public string QRSaxis { get { return _QRSaxis; } set { _QRSaxis = value; } }

        public string Taxis { get { return _Taxis; } set { _Taxis = value; } }

        public string ReportFilePath { get { return _ReportFilePath; } set { _ReportFilePath = value; } }

        public long DocumentID { get { return _nDocumentID; } set { _nDocumentID = value; } }

        public string DocumentName { get { return _docName; } set { _docName = value; } }

        #endregion
        
        // function to retrive patient information
        public DataTable GetPationtData(Int64 _Patient_ID)
        {
            gloDatabaseLayer.DBLayer oDbLayer = null;
            DataTable _dtGetPationtData = null;
            String _SqlQury = string.Empty;
            try
            {
                oDbLayer = new gloDatabaseLayer.DBLayer(sgloEMRConnectionString);
                _dtGetPationtData = new DataTable();

                _SqlQury = "SELECT dbo.Patient.sPatientCode,dbo.Patient.sLastName,dbo.Patient.sFirstName,dbo.Patient.sMiddleName , CONVERT( varchar(10),dbo.Patient.dtDOB,101)AS dtDOB , CASE dbo.Patient.sGender WHEN 'Male' THEN 1 WHEN 'Female' THEN 2 ELSE 0 END AS Gender From  dbo.Patient WHERE dbo.Patient.nPatientID = " + _Patient_ID + " ";
                
                oDbLayer.Connect(false);
                oDbLayer.Retrive_Query(_SqlQury, out _dtGetPationtData);
                oDbLayer.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, " Error in clsSpiroTestMst.GetPationtData() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
            }
            finally
            {
                if (oDbLayer != null)
                {
                    oDbLayer.Dispose();
                    oDbLayer = null;
                }
                _SqlQury = string.Empty;
            }

            return _dtGetPationtData;

        }

        public void SaveMidmarkECGReport()
        {

            try
            {
                //check file exist of not
                if (!File.Exists(ReportFilePath))
                {
                    return;
                }

                //check if is this new test
                if (ECGID == 0)
                {
                    OrderDateTime = DateTime.Now;
                    ReviewDateTime = DateTime.Now;
                    VisitID = RetriveVisitID(DateTime.Now);
                }
                else
                {
                    RetriveECGTestInformation(ECGID, PatientID, out _dtOrderDate, out _dtReviewDate, out _nVisitID);
                    ReviewDateTime = DateTime.Now;
                }

                //check if visit id is blank
                if (VisitID == 0)
                {
                    VisitID = RetriveVisitID(OrderDateTime);
                }

                DocumentID = SaveMidmarkECGReport_Device();

                if (DocumentID > 0)
                {
                    ECGID = SaveMidmarkECGReport_GloEMR(Convert.ToString(DocumentID), Convert.ToString(DocumentID));
                }

            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                if (DocumentID > 0 && ECGID <= 0)
                {
                    DocumentID = 0;
                    ECGID = 0;
                    Remove_MidmarkECGTest(DocumentID, ECGID);  
                }
                
            }
                                
        }
        
        public long SaveMidmarkECGReport_Device()
        {
            long ResultID = 0;
            clsSpiroReportmanager objMidmarkECGReportManger = null;
            clsSpiroReports objMidmarkECGReport = null;
            try
            {
                objMidmarkECGReport = new clsSpiroReports();
                objMidmarkECGReport.TestOrderprefix = "ECG";
                objMidmarkECGReport.TestOrderId = clsSpiroGeneralModule.GetNewTestOrderID(PatientID, sDeviceConnectionAtring);
                objMidmarkECGReport.ClinicId = ClinicID;
                objMidmarkECGReport.PatientId = PatientID;
                objMidmarkECGReport.RaceCode = 0;
                objMidmarkECGReport.VisitId = VisitID;
                objMidmarkECGReport.VisitId_Dtl =VisitID;
                objMidmarkECGReport.ReviewerId = LoginUserID;
                objMidmarkECGReport.Reviewer = LoginUserName;
                objMidmarkECGReport.OrderedById = 0;
                objMidmarkECGReport.OrderByType = string.Empty;
                objMidmarkECGReport.UserId = LoginUserID;
                objMidmarkECGReport.UserName = LoginUserName;
                objMidmarkECGReport.RefernceId = 0;
                objMidmarkECGReport.MachineName = Environment.MachineName;
                objMidmarkECGReport.DocumentId = 0;
                objMidmarkECGReport.Status = "Active";
                objMidmarkECGReport.DocumentExtension = "CAR";
                objMidmarkECGReport.Documentname = ReportFilePath;
                objMidmarkECGReport.dtDocumentCreated = DateTime.Now;
                objMidmarkECGReport.dtDocumentModified = DateTime.Now;
                objMidmarkECGReport.Interpretation = Interpretation;
                objMidmarkECGReport.TestSource = "Midmark_ECG";
                objMidmarkECGReport.TestDetails = string.Empty;
                objMidmarkECGReport.DocStream = File.ReadAllBytes(ReportFilePath);
                objMidmarkECGReport.IsSmoker = false;
                objMidmarkECGReport.NoOfCigarsperDay = 0;
                objMidmarkECGReport.Noofsmokingyears = 0;
                objMidmarkECGReport.Isquitsmoking = false;
                objMidmarkECGReport.Noofquitsmokingyears = 0;
                objMidmarkECGReport.Hieghtincms = 0;
                objMidmarkECGReport.Weightinkg = 0;
                objMidmarkECGReport.Documentname = DocumentName;
                objMidmarkECGReportManger = new clsSpiroReportmanager(sDeviceConnectionAtring);
                objMidmarkECGReportManger.saveSpiroReport(objMidmarkECGReport);
                ResultID = objMidmarkECGReport.DocumentId;
            }
            catch (Exception)
            {
                ResultID = 0;
            }
            finally
            {
                if (objMidmarkECGReport != null)
                {
                    objMidmarkECGReport.Dispose();
                    objMidmarkECGReport = null;
                }
                if (objMidmarkECGReportManger != null)
                {
                    objMidmarkECGReportManger.Dispose();
                    objMidmarkECGReportManger = null; 
                }
            }


            return ResultID;
        }

        public long SaveMidmarkECGReport_GloEMR(string OrderId, string TestId)
        {
            long ResultID = 0;
            long nGroupID = 0;
            long nExamID=0;
            string CPTCode = string.Empty;
            string TestType = "ECG";
            string ECGPerform = string.Empty;
            string OrderInPhysician = string.Empty;
            string ReviewInPhysician = string.Empty;
            bool AddDt = true;
            long MachineID = 0;
            string ExternalCode = "Test Completed";
            string device = "Midmark IQ ECG";
            Classes.ClsWelchAllynECGLayer ObjClsWelchAllynECGLayer = null;
            try
            {
                ObjClsWelchAllynECGLayer = new Classes.ClsWelchAllynECGLayer(sgloEMRConnectionString);
                ResultID= ObjClsWelchAllynECGLayer.WA_SaveUPdateECGTest(ECGID, PatientID, nExamID, VisitID, 1, OrderDateTime, nGroupID, CPTCode, TestType, ECGPerform, ReviewDateTime, PR, QT, QTC, QRSDuration, Paxis, QRSaxis, Taxis, Interpretation , OrderInPhysician, ReviewInPhysician, ReviewDateTime, AddDt, MachineID, OrderId, TestId, ExternalCode, device);
                
            }
            catch (Exception)
            {
                ResultID = 0;
            }


            return ResultID;
        }

        public long RetriveVisitID(DateTime VisitDate)
        {
            long _nResultID = 0;
            Classes.ClsWelchAllynECGLayer ObjClsWelchAllynECGLayer = null;
            try
            {
                ObjClsWelchAllynECGLayer = new Classes.ClsWelchAllynECGLayer(sgloEMRConnectionString);

                _nResultID = ObjClsWelchAllynECGLayer.getVisitID(VisitDate, PatientID, sgloEMRConnectionString);
            }
            catch (Exception)
            {
                _nResultID = 0;
            }
            finally
            {
                if (ObjClsWelchAllynECGLayer != null)
                {
                    ObjClsWelchAllynECGLayer.Dispose();
                    ObjClsWelchAllynECGLayer = null;
                }
            }
            return _nResultID;
        }

        private long nDocumentID(string sOrderID)
        {
            long Result = 0;
            gloDatabaseLayer.DBLayer objDBLayer = null;
            String SqlQury = String.Empty;
            try
            {
                long.TryParse(sOrderID, out Result);
                if (Result > 0)
                {
                    SqlQury = "SELECT dbo.e_DocumentContainer.eDocumentId FROM dbo.e_DocumentContainer INNER JOIN dbo.e_Document ON dbo.e_DocumentContainer.eDocumentId = dbo.e_Document.eDocumentId INNER JOIN dbo.e_DocumentDetails ON dbo.e_DocumentContainer.eDocumentId = dbo.e_DocumentDetails.eDocumentId WHERE dbo.e_DocumentContainer.sTestType = 'Midmark ECG' AND dbo.e_DocumentContainer.eDocumentId = " + Result + "";

                }

            }
            catch (Exception)
            {
                Result = 0;
            }
            finally
            {
                if (objDBLayer != null)
                {
                    objDBLayer.Dispose();
                    objDBLayer = null;
                }
            }
            return Result = 0;
        }
        
        private void RetriveECGTestInformation(long nECGID,long nPatientID, out DateTime dtOrderDate,out DateTime dtReviewDate,out long nVisitID)
        {
            gloDatabaseLayer.DBLayer oDBLayer = null;
            string sSqlQury = string.Empty;
            DataTable dtResult = null;
            dtOrderDate = DateTime.Now;
            dtReviewDate = DateTime.Now;
            nVisitID = 0;
            try
            {
               sSqlQury = "SELECT nVisitID,dtOrderDate,dtReviewDate  FROM  CV_ElectroCardioGrams where sDeviceType='Midmark IQ ECG' AND nPatientID= " + nPatientID + " AND [nECGID] = " + nECGID + "";
               oDBLayer = new gloDatabaseLayer.DBLayer(sgloEMRConnectionString);
               oDBLayer.Connect(false);
               oDBLayer.Retrive_Query(sSqlQury,out dtResult);   
               oDBLayer.Disconnect();
               if (dtResult != null && dtResult.Rows.Count > 0)
               {
                  DateTime.TryParse(Convert.ToString(dtResult.Rows[0]["dtOrderDate"]), out dtOrderDate);
                  DateTime.TryParse(Convert.ToString(dtResult.Rows[0]["dtReviewDate"]), out dtReviewDate);
                  long.TryParse(Convert.ToString(dtResult.Rows[0]["nVisitID"]), out nVisitID);  
               }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, " Error in clsSpiroTestMst.GetPationtData() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                ex = null;
                dtOrderDate = DateTime.Now;
                dtReviewDate = DateTime.Now;
                nVisitID = 0;
            }
            finally
            {
                if (oDBLayer != null)
                {
                    oDBLayer.Dispose();
                    oDBLayer = null;
                }


            }



        }

        private bool Remove_MidmarkECGTest(long DeviceDocumentID, long gloEMRECGID)
        {
            bool _bResult = false;
            string SqlQury=string.Empty;
            gloDatabaseLayer.DBLayer objDBLayer = null;
            try
            {
                 
                if (DeviceDocumentID > 0)
                {
                    SqlQury = "delete from dbo.e_DocumentDetails where dbo.e_DocumentDetails.eDocumentId = " + DeviceDocumentID + " ";
                    SqlQury = SqlQury + "delete from dbo.e_DocumentContainer where dbo.e_DocumentContainer.eDocumentId =" + DeviceDocumentID + " ";
                    SqlQury = SqlQury + "delete from dbo.e_Document where dbo.e_Document.eDocumentId =" + DeviceDocumentID + "";
                    objDBLayer = new gloDatabaseLayer.DBLayer(sDeviceConnectionAtring);
                    objDBLayer.Connect(false);
                    objDBLayer.Execute_Query(SqlQury);  
                    objDBLayer.Disconnect(); 
                            
                }
                if (gloEMRECGID > 0)
                {
                    SqlQury = "delete from CV_ElectroCardioGrams where nECGID=" + gloEMRECGID + "";
                    objDBLayer = new gloDatabaseLayer.DBLayer(sgloEMRConnectionString);
                    objDBLayer.Connect(false);
                    objDBLayer.Execute_Query(SqlQury);
                    objDBLayer.Disconnect(); 
                }
                _bResult = true;
            }
            catch (Exception)
            { _bResult = false; }
            return _bResult;
        }

        public clsSpiroReports RetriveMidmarkECGTest(long nDcoumentID)
        {
            clsSpiroReports objclsSpiroReports = null;
            clsSpiroReportmanager objSpiroManager = null;
            try
            {
                objclsSpiroReports = new clsSpiroReports(); 

                if (nDcoumentID > 0)
                {
                  objSpiroManager = new clsSpiroReportmanager(sDeviceConnectionAtring);

                  objclsSpiroReports = objSpiroManager.getSpiroObject(nDcoumentID);

                  if (objclsSpiroReports != null && objclsSpiroReports.DocumentId > 0)
                  {
                      objclsSpiroReports.DocStream = objSpiroManager.GetContainerStream(objclsSpiroReports.DocumentId); 

                  }

                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return objclsSpiroReports;
          

        }

    } 
  }
