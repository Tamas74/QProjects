using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using gloClinicalQueueGeneral;
using System.Drawing.Printing;
using System.IO;

namespace gloClinicalQueueGeneral
{

    public class gloQueueMetadatawriter
    {

        StringBuilder strError = new StringBuilder();
        #region "Dispose Methods"

        public bool disposed { get; set; }

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

        ~gloQueueMetadatawriter()
        {
            Dispose(false);
        }

        #endregion
        #region "Header"

        private const int col_Header_QueNo = 0;
        private const int col_Header_QueID = 1;
        private const int col_Header_QuePatientID = 2;
        private const int col_Header_QueContactID = 3;
        private const int col_Header_QueNotes = 4;
        private const int col_Header_QueUserID = 5;
        private const int col_Header_QueUser = 6;
        private const int col_Header_QueStatus = 7;
        private const int col_Header_QueStartDate = 8;
        private const int col_Header_QueEndDate = 9;
        private const int col_Header_QueCreatedDate = 10;
        private const int col_Header_QuePCode = 11;
        private const int col_Header_QuePFirstName = 12;
        private const int col_Header_QuePMName = 13;
        private const int col_Header_QuePLastName = 14;
        private const int col_Header_QuePAddress1 = 15;
        private const int col_Header_QuePAddress2 = 16;
        private const int col_Header_QuePCity = 17;
        private const int col_Header_QuePState = 18;
        private const int col_Header_QuePZip = 19;
        private const int col_Header_QueInsuranceName = 20;
        //
        private const int col_Header_QueInsuranceContact = 21;
        private const int col_Header_QueInsuranceAddline1 = 22;

        //
        private const int col_Header_QueInsStreet = 23;
        private const int col_Header_QueInsCity = 24;
        private const int col_Header_QueInsState = 25;
        private const int col_Header_QueInsZip = 26;
        private const int col_Header_QueInsPhone = 27;
        private const int col_Header_QueInsFax = 28;
        private const int col_Header_QueInsMobile = 29;
        private const int col_Header_QueInsEmail = 30;
        private const int col_Header_QueClaimPrintType = 31;
        #endregion

        #region "Documents"

        private const int col_Doc_DetailID = 0;
        private const int col_Doc_QueID = 1;
        private const int col_Doc_Enum = 2;
        private const int col_Doc_TransID1 = 3;
        private const int col_Doc_TransID2 = 4;
        private const int col_Doc_TransID3 = 5;
        private const int col_Doc_TransID4 = 6;
        private const int col_Doc_TransID5 = 7;
        private const int col_Doc_PrintSequence = 8;
        #endregion

        #region "Queue History"

        private const int col_Q_History_NoteID = 0;
        private const int col_Q_History_QID = 1;
        private const int col_Q_History_Note = 2;
        private const int col_Q_History_ContactID = 3;
        private const int col_Q_History_User = 4;
        private const int col_Q_History_UID = 5;
        private const int col_Q_History_NoteType = 6;
        private const int col_Q_History_CreatedDate = 7;
        #endregion

        public StringBuilder strErroMsg
        {
            get { return strError; }
            set { strError = value; }
        }

        public bool bIsForPrint = false;

        public QueuePatientInfo GetPatientInfo(DataTable dtHeader)
        {
            QueuePatientInfo _patientinfo = new QueuePatientInfo();
            try
            {
                _patientinfo.FirstName = Convert.ToString(dtHeader.Rows[0][col_Header_QuePFirstName]);
                _patientinfo.LastName = Convert.ToString(dtHeader.Rows[0][col_Header_QuePLastName]);
                _patientinfo.PatientCode = Convert.ToString(dtHeader.Rows[0][col_Header_QuePCode]);
                _patientinfo.DateofBirth = Convert.ToDateTime(dtHeader.Rows[0]["PatientDateofBirth"]).ToString("dd MMMM yyyy");
                _patientinfo.MiddleName = Convert.ToString(dtHeader.Rows[0]["PatientMiddleName"]);
            }
            catch (Exception)
            {
                if (strError.Length == 0)
                {
                    strError.AppendLine("Error while writing Clinical chart queue metadata file: " + "Patient Information");
                }
                else
                {
                    strError.AppendLine("Patient Information");
                }
            }
            return _patientinfo;
        }

        public Queue GenerateQueueMetaDataFile(DataSet ds,QueueDocumentDocumentDetails popUpDetails = null)
        {
            Queue QueueDoc = new Queue();
            if (ds != null)
            {
                DataTable dtHeader = ds.Tables["ClinicalChartQueueMst"];
                DataTable dtdocumentDetails = ds.Tables["ClinicalChartQueueDetails"];
                DataTable dtQueue = ds.Tables["ClinicalChartQueueNotes"];
                DataTable dtClinic = ds.Tables["ClinicDetails"];
                int _claimPrintType = 0;
                if (dtHeader.Rows.Count > 0)
                {
                    QueueDoc.QueueNo = Convert.ToInt64(dtHeader.Rows[0][col_Header_QueNo]);
                    QueueDoc.QueueUser = Convert.ToString(dtHeader.Rows[0][col_Header_QueUser]);
                    QueueDoc.CreatedDateTime = Convert.ToDateTime(dtHeader.Rows[0][col_Header_QueCreatedDate]).ToString("yyyyMMddhhmmss");
                    QueueDoc.Priority = 0;
                    QueueDoc.QueueName = Convert.ToString(dtHeader.Rows[0]["QueueName"]);
                    QueueDoc.Notes = Convert.ToString(dtHeader.Rows[0][col_Header_QueNotes]);
                    _claimPrintType = Convert.ToInt16(dtHeader.Rows[0][col_Header_QueClaimPrintType]);
                    QueueDoc.ClaimPrintType = _claimPrintType;

                    QueueDoc.PatientInfo = new QueuePatientInfo[1];
                    QueueDoc.PatientInfo[0] = new QueuePatientInfo();
                    QueueDoc.PatientInfo[0] = GetPatientInfo(dtHeader);


                    QueueDoc.InsuranceInfo = new QueueInsuranceInfo[1];
                    QueueDoc.InsuranceInfo[0] = new QueueInsuranceInfo();
                    QueueDoc.InsuranceInfo[0] = GetInsuranceInfo(dtHeader);

                }
                if (dtClinic.Rows.Count > 0)
                {
                    QueueDoc.PracticeAUSid = Convert.ToString(dtClinic.Rows[0]["SiteID"]);
                    QueueDoc.PracticeName = Convert.ToString(dtClinic.Rows[0]["sClinicName"]);
                }
                QueueDoc = GetDocumentDetails(dtdocumentDetails, QueueDoc, _claimPrintType,popUpDetails);

                if (dtQueue.Rows.Count > 0)
                {
                    //QueueDoc.QueueHistory = new QueueQueueHistoryQueueHistoryDetails[dtQueue.Rows .Count];
                    QueueDoc.QueueHistory = new QueueQueueHistory[1];
                    QueueDoc.QueueHistory[0] = new QueueQueueHistory();
                    QueueDoc.QueueHistory[0].QueueHistoryDetails = new QueueQueueHistoryQueueHistoryDetails[dtQueue.Rows.Count];
                    for (Int16 j = 0; j <= dtQueue.Rows.Count - 1; j++)
                    {
                        QueueDoc.QueueHistory[0].QueueHistoryDetails[j] = new QueueQueueHistoryQueueHistoryDetails();
                        QueueDoc.QueueHistory[0].QueueHistoryDetails[j].CreatedDateTime = Convert.ToDateTime(dtQueue.Rows[j][col_Q_History_CreatedDate]).ToString("yyyyMMddhhmmss");
                        QueueDoc.QueueHistory[0].QueueHistoryDetails[j].Note = Convert.ToString(dtQueue.Rows[j][col_Q_History_Note]);
                        QueueDoc.QueueHistory[0].QueueHistoryDetails[j].UserName = Convert.ToString(dtQueue.Rows[j][col_Q_History_User]);
                        QueueDoc.QueueHistory[0].QueueHistoryDetails[j].NoteType = Convert.ToString(dtQueue.Rows[j][col_Q_History_NoteType]);
                    }

                }
                if (dtHeader != null)
                {
                    dtHeader.Dispose();
                    dtHeader = null;
                }
                if (dtdocumentDetails != null)
                {
                    dtdocumentDetails.Dispose();
                    dtdocumentDetails = null;
                }
                if (dtQueue != null)
                {
                    dtQueue.Dispose();
                    dtQueue = null;
                }
                if (dtClinic != null)
                {
                    dtClinic.Dispose();
                    dtClinic = null;
                }
            }
            return QueueDoc;
        }
        private Queue GetDocumentDetails(DataTable dtdocumentDetails, Queue QueueDoc, int _claimPrintType, QueueDocumentDocumentDetails popUpDetails = null)
        {
            try
            {
                if (dtdocumentDetails.Rows.Count > 0)
                {
                    DataView dvDocument = null;

                    dvDocument = dtdocumentDetails.DefaultView;

                    dvDocument.RowFilter = "PhysicalFileName <>''";
                    //QueueDoc.Document = new QueueDocumentDocumentDetails[dvDocument.ToTable().Rows.Count];
                    DataTable dvDocumentTable = dvDocument.ToTable();
                    QueueDoc.FileCount = dvDocumentTable.Rows.Count;
                    QueueDoc.Document = new QueueDocument[1];
                    QueueDoc.Document[0] = new QueueDocument();
                    QueueDoc.Document[0].DocumentDetails = new QueueDocumentDocumentDetails[QueueDoc.FileCount];
                    for (Int16 i = 0; i <= QueueDoc.FileCount - 1; i++)
                    {


                        QueueDoc.Document[0].DocumentDetails[i] = new QueueDocumentDocumentDetails();
                        QueueDoc.Document[0].DocumentDetails[i].SystemName = Convert.ToString(dvDocumentTable.Rows[i]["DocumentName"]);
                        QueueDoc.Document[0].DocumentDetails[i].Name = Convert.ToString(dvDocumentTable.Rows[i]["PhysicalFileName"]);
                        QueueDoc.Document[0].DocumentDetails[i].Sequence = Convert.ToInt64(dvDocumentTable.Rows[i][col_Doc_PrintSequence]);

                        if ((Convert.ToString(dvDocumentTable.Rows[i][col_Doc_Enum]) == "ClaimDetails") && _claimPrintType == 1)
                        {
                            QueueDoc.Document[0].DocumentDetails[i].PrintType = "PrintData";
                        }
                        else
                        {
                            QueueDoc.Document[0].DocumentDetails[i].PrintType = "PrintFile";
                        }
                        String sTranID_III = Convert.ToString(dvDocumentTable.Rows[i]["sTranID_III"]);
                        if (sTranID_III == "False")
                        {
                            QueueDoc.Document[0].DocumentDetails[i].ClaimType = "CMS1500";
                        }
                        else if (sTranID_III == "True")
                        {
                            QueueDoc.Document[0].DocumentDetails[i].ClaimType = "UB04";
                        }
                        if (sTranID_III == "")
                        {
                            QueueDoc.Document[0].DocumentDetails[i].ClaimType = "";
                        }
                        QueueDoc.Document[0].DocumentDetails[i].Type = Convert.ToString(dvDocumentTable.Rows[i][col_Doc_Enum]);

                        QueueDoc.Document[0].DocumentDetails[i].DocumentNo = i + 1;
                        QueueDoc.Document[0].DocumentDetails[i].IsPrint = 1;
                        QueueDoc.Document[0].DocumentDetails[i].VersionNumber = 1;

                        if (bIsForPrint)
                        {
                            Int32 footr_StartingPage, footr_TotalPage;
                            Int32.TryParse(dvDocumentTable.Rows[i]["footr_StartingPage"].ToString(), out footr_StartingPage);
                            Int32.TryParse(dvDocumentTable.Rows[i]["footr_TotalPage"].ToString(), out footr_TotalPage);
                            if ((footr_StartingPage > 0) && (footr_TotalPage > 0))
                            {
                                //QueueDoc.Document[0].DocumentDetails[i].FooterFrom = PhysicalFileNames[i].footerInfo[0].FromPage;
                                //QueueDoc.Document[0].DocumentDetails[i].FooterTo = PhysicalFileNames[i].footerInfo[0].ToPage;
                                QueueDoc.Document[0].DocumentDetails[i].FooterStart = footr_StartingPage;
                                QueueDoc.Document[0].DocumentDetails[i].FooterTotal = footr_TotalPage;
                            }
                        }
                       

                        if (popUpDetails != null)
                        {
                            QueueDoc.Document[0].DocumentDetails[i].PrintFrom = popUpDetails.PrintFrom;
                            QueueDoc.Document[0].DocumentDetails[i].PrintTo = popUpDetails.PrintTo;
                            QueueDoc.Document[0].DocumentDetails[i].Printer = popUpDetails.Printer;
                            QueueDoc.Document[0].DocumentDetails[i].Copies = popUpDetails.Copies;
                            QueueDoc.Document[0].DocumentDetails[i].Landscape = popUpDetails.Landscape;
                            QueueDoc.Document[0].DocumentDetails[i].Duplex = popUpDetails.Duplex;
                            QueueDoc.Document[0].DocumentDetails[i].Size = popUpDetails.Size;
                            QueueDoc.Document[0].DocumentDetails[i].Tray = popUpDetails.Tray;
                            QueueDoc.Document[0].DocumentDetails[i].isCollete = popUpDetails.isCollete;
                        }
                    }
                    if (dvDocumentTable != null)
                    {
                        dvDocumentTable.Dispose();
                        dvDocumentTable = null;
                    }
                    if (dvDocument != null)
                    {
                        dvDocument.Dispose();
                        dvDocument = null;
                    }
                }
            }
            catch (Exception)
            {
                if (strError.Length == 0)
                {
                    strError.AppendLine("Error while writing Clinical chart queue metadata file: " + "Document Information");
                }
                else
                {
                    strError.AppendLine("Document Information");
                }
            }
            return QueueDoc;
        }
        private Queue GetQueueHistory(DataTable dtQueue, Queue QueueDoc)
        {
            try
            {
                if (dtQueue.Rows.Count > 0)
                {
                    QueueDoc.QueueHistory = new QueueQueueHistory[1];
                    QueueDoc.QueueHistory[0] = new QueueQueueHistory();
                    QueueDoc.QueueHistory[0].QueueHistoryDetails = new QueueQueueHistoryQueueHistoryDetails[dtQueue.Rows.Count];
                    for (Int16 j = 0; j <= dtQueue.Rows.Count - 1; j++)
                    {
                        QueueDoc.QueueHistory[0].QueueHistoryDetails[j] = new QueueQueueHistoryQueueHistoryDetails();
                        QueueDoc.QueueHistory[0].QueueHistoryDetails[j].CreatedDateTime = Convert.ToDateTime(dtQueue.Rows[j][col_Q_History_CreatedDate]).ToString("yyyyMMddhhmmss");
                        QueueDoc.QueueHistory[0].QueueHistoryDetails[j].Note = Convert.ToString(dtQueue.Rows[j][col_Q_History_Note]);
                        QueueDoc.QueueHistory[0].QueueHistoryDetails[j].UserName = Convert.ToString(dtQueue.Rows[j][col_Q_History_User]);
                        QueueDoc.QueueHistory[0].QueueHistoryDetails[j].NoteType = Convert.ToString(dtQueue.Rows[j][col_Q_History_NoteType]);
                    }

                }
            }
            catch (Exception)
            {
                if (strError.Length == 0)
                {
                    strError.AppendLine("Error while writing Clinical chart queue metadata file: " + "QueueHistory Information");
                }
                else
                {
                    strError.AppendLine("QueueHistory Information");
                }
            }
            return QueueDoc;
        }

        public class QueueDocumentInfo
        {
            public String PdfFileName;
            public String SrcFileName;
            public List<QueueFooterInfo> footerInfo;
        }
        public class QueueFooterInfo
        {
            public int FromPage = 0;
            public int ToPage = 0;
            public int StartingPage = 0;
            public int TotalPages = 0;

            public void CopyExceptText(QueueFooterInfo thisfooter)
            {
                FromPage = thisfooter.FromPage;
                ToPage = thisfooter.ToPage;
                StartingPage = thisfooter.StartingPage;
                TotalPages = thisfooter.TotalPages;
            }
        }

        public Queue GenerateWordMetaDataFile(String sPatient, DateTime sPatientDOB, Boolean AddFooterInService, List<QueueDocumentInfo> PhysicalFileNames, String QueueName, Boolean autoLandScape = true, Boolean isClaim = false, String claimType = "", String PrintType = "",QueueDocumentDocumentDetails popUpDetails = null)
        {
            Queue QueueDoc = new Queue();
            QueueDoc.QueueNo = 01;
            QueueDoc.QueueUser = "admin";
            QueueDoc.CreatedDateTime = Convert.ToDateTime(DateTime.Now).ToString("yyyyMMddhhmmss");
            QueueDoc.Priority = 0;
            QueueDoc.QueueName = QueueName;
            QueueDoc.Notes = "";
            QueueDoc.ClaimPrintType = 0;

            //Patient Info
            QueueDoc.PatientInfo = new QueuePatientInfo[1];
            QueueDoc.PatientInfo[0] = new QueuePatientInfo();
            QueueDoc.PatientInfo[0].FirstName = sPatient;
            QueueDoc.PatientInfo[0].LastName = "";
            QueueDoc.PatientInfo[0].PatientCode = "";
            QueueDoc.PatientInfo[0].DateofBirth = Convert.ToDateTime(sPatientDOB).ToString("dd MMMM yyyy"); ;
            QueueDoc.PatientInfo[0].MiddleName = "";

            //Insurance Info
            QueueDoc.InsuranceInfo = new QueueInsuranceInfo[1];
            QueueDoc.InsuranceInfo[0] = new QueueInsuranceInfo();
            QueueDoc.InsuranceInfo[0].contact = "";
            QueueDoc.InsuranceInfo[0].ContactID = 0;
            QueueDoc.InsuranceInfo[0].InsuranceName = "";
            QueueDoc.InsuranceInfo[0].addr = new QueueInsuranceInfoAddr[1];
            QueueDoc.InsuranceInfo[0].addr[0] = new QueueInsuranceInfoAddr();
            QueueDoc.InsuranceInfo[0].addr[0].streetAddressLine1 = "";
            QueueDoc.InsuranceInfo[0].addr[0].city = "";
            QueueDoc.InsuranceInfo[0].addr[0].state = "";
            QueueDoc.InsuranceInfo[0].addr[0].zip = "";
            QueueDoc.InsuranceInfo[0].addr[0].phone = "";
            QueueDoc.InsuranceInfo[0].addr[0].mobile = "";
            QueueDoc.InsuranceInfo[0].addr[0].fax = "";
            QueueDoc.InsuranceInfo[0].addr[0].email = "";

            //Practice Info
            QueueDoc.PracticeAUSid = "";
            QueueDoc.PracticeName = "";

            //Document Info
            int fileCount = PhysicalFileNames.Count;
            QueueDoc.FileCount = fileCount;
            QueueDoc.Document = new QueueDocument[1];
            QueueDoc.Document[0] = new QueueDocument();
            QueueDoc.Document[0].DocumentDetails = new QueueDocumentDocumentDetails[QueueDoc.FileCount];

            String PDFWithoutPath = "";
            int currentPageFrom = 1;
            int currentPageTo = gloGlobal.gloTSPrint.NoOfPages;
            for (int i = 0; i < QueueDoc.FileCount; i++)
            {
                PDFWithoutPath = PhysicalFileNames[i].PdfFileName.Substring(PhysicalFileNames[i].PdfFileName.LastIndexOf("\\") + 1);
                QueueDoc.Document[0].DocumentDetails[i] = new QueueDocumentDocumentDetails();
                QueueDoc.Document[0].DocumentDetails[i].SystemName = "";
                QueueDoc.Document[0].DocumentDetails[i].Name = PDFWithoutPath;
                QueueDoc.Document[0].DocumentDetails[i].Sequence = i;
                if (!isClaim)
                {
                    if (autoLandScape)
                    {
                        QueueDoc.Document[0].DocumentDetails[i].PrintType = "DefaultPrinter";
                        QueueDoc.Document[0].DocumentDetails[i].ClaimType = "ActualSize";
                    }
                    else
                    {
                        QueueDoc.Document[0].DocumentDetails[i].PrintType = "PrintFile";
                        QueueDoc.Document[0].DocumentDetails[i].ClaimType = "";
                    }
                    
                    QueueDoc.Document[0].DocumentDetails[i].VersionNumber = 1;
                    if ((PhysicalFileNames[i].footerInfo != null) && (PhysicalFileNames[i].footerInfo.Count >= 1))
                    {
                        QueueDoc.Document[0].DocumentDetails[i].FooterFrom = PhysicalFileNames[i].footerInfo[0].FromPage;
                        QueueDoc.Document[0].DocumentDetails[i].FooterStart = PhysicalFileNames[i].footerInfo[0].StartingPage;
                        QueueDoc.Document[0].DocumentDetails[i].FooterTo = PhysicalFileNames[i].footerInfo[0].ToPage;
                        QueueDoc.Document[0].DocumentDetails[i].FooterTotal = PhysicalFileNames[i].footerInfo[0].TotalPages;
                    }
                    if (AddFooterInService == true)
                        QueueDoc.Document[0].DocumentDetails[i].Type = "ScannedDocument";
                    else
                        QueueDoc.Document[0].DocumentDetails[i].Type = "PatientExam";
                }
                else
                {
                    QueueDoc.Document[0].DocumentDetails[i].PrintType = PrintType;
                    QueueDoc.Document[0].DocumentDetails[i].ClaimType = claimType;
                    QueueDoc.Document[0].DocumentDetails[i].Type = "ClaimDetails";
                }

                QueueDoc.Document[0].DocumentDetails[i].DocumentNo = (i + 1);
                QueueDoc.Document[0].DocumentDetails[i].IsPrint = 1;

                if (popUpDetails != null)
                {
                    if (gloGlobal.gloTSPrint.NoOfPages == 0 || (popUpDetails.PrintFrom == 0 && popUpDetails.PrintTo==0))
                    {
                        QueueDoc.Document[0].DocumentDetails[i].PrintFrom = popUpDetails.PrintFrom;
                        QueueDoc.Document[0].DocumentDetails[i].PrintTo = popUpDetails.PrintTo;
                    }
                    else
                    {
                        //if ((currentPageFrom <= popUpDetails.PrintTo) && (currentPageTo >= popUpDetails.PrintFrom))
                        //{
                        //    QueueDoc.Document[0].DocumentDetails[i].PrintFrom = 1;
                        //    QueueDoc.Document[0].DocumentDetails[i].PrintTo = gloGlobal.gloTSPrint.NoOfPages;
                        //}
                        //else
                        {
                            if ((currentPageFrom >= popUpDetails.PrintFrom) && (currentPageTo <= popUpDetails.PrintTo))
                            {

                                QueueDoc.Document[0].DocumentDetails[i].PrintFrom = 1;
                                QueueDoc.Document[0].DocumentDetails[i].PrintTo = gloGlobal.gloTSPrint.NoOfPages ;
                                
                            }
                            else 
                            {
                                if ((currentPageFrom < popUpDetails.PrintFrom) && (currentPageTo <= popUpDetails.PrintTo))                            
                                {

                                    QueueDoc.Document[0].DocumentDetails[i].PrintFrom = popUpDetails.PrintFrom - currentPageFrom + 1;
                                    QueueDoc.Document[0].DocumentDetails[i].PrintTo = gloGlobal.gloTSPrint.NoOfPages;
                                    if (QueueDoc.Document[0].DocumentDetails[i].PrintFrom > gloGlobal.gloTSPrint.NoOfPages)
                                    {
                                        QueueDoc.Document[0].DocumentDetails[i].PrintFrom = gloGlobal.gloTSPrint.NoOfPages + 1;
                                        QueueDoc.Document[0].DocumentDetails[i].PrintTo = gloGlobal.gloTSPrint.NoOfPages + 1;
                                    }
                                    
                                }
                                else
                                {

                                    if ((currentPageFrom >= popUpDetails.PrintFrom) && (currentPageTo > popUpDetails.PrintTo))
                                    {

                                        QueueDoc.Document[0].DocumentDetails[i].PrintFrom = 1;
                                        QueueDoc.Document[0].DocumentDetails[i].PrintTo = gloGlobal.gloTSPrint.NoOfPages + popUpDetails.PrintTo - currentPageTo ;
                                        if (QueueDoc.Document[0].DocumentDetails[i].PrintTo < 1)
                                        {
                                            QueueDoc.Document[0].DocumentDetails[i].PrintFrom = gloGlobal.gloTSPrint.NoOfPages + 1;
                                            QueueDoc.Document[0].DocumentDetails[i].PrintTo = gloGlobal.gloTSPrint.NoOfPages + 1;
                                        }

                                    }
                                    else
                                    {



                                        if ((currentPageFrom < popUpDetails.PrintFrom) && (currentPageTo > popUpDetails.PrintTo))
                                        {

                                            QueueDoc.Document[0].DocumentDetails[i].PrintFrom = popUpDetails.PrintFrom - currentPageFrom + 1;
                                            QueueDoc.Document[0].DocumentDetails[i].PrintTo = gloGlobal.gloTSPrint.NoOfPages + popUpDetails.PrintTo - currentPageTo ;
                                            if (QueueDoc.Document[0].DocumentDetails[i].PrintTo < 1)
                                            {
                                                QueueDoc.Document[0].DocumentDetails[i].PrintFrom = gloGlobal.gloTSPrint.NoOfPages + 1;
                                                QueueDoc.Document[0].DocumentDetails[i].PrintTo = gloGlobal.gloTSPrint.NoOfPages + 1;
                                            }
                                            else
                                            {
                                                if (QueueDoc.Document[0].DocumentDetails[i].PrintFrom > gloGlobal.gloTSPrint.NoOfPages)
                                                {
                                                    QueueDoc.Document[0].DocumentDetails[i].PrintFrom = gloGlobal.gloTSPrint.NoOfPages + 1;
                                                    QueueDoc.Document[0].DocumentDetails[i].PrintTo = gloGlobal.gloTSPrint.NoOfPages + 1;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (popUpDetails.PrintFrom > currentPageFrom)
                                            {
                                                QueueDoc.Document[0].DocumentDetails[i].PrintFrom = popUpDetails.PrintFrom - currentPageFrom + 1;
                                            }
                                            else
                                            {
                                                QueueDoc.Document[0].DocumentDetails[i].PrintFrom = gloGlobal.gloTSPrint.NoOfPages + 1;
                                            }
                                            if (popUpDetails.PrintTo >= currentPageTo)
                                            {
                                                QueueDoc.Document[0].DocumentDetails[i].PrintTo = gloGlobal.gloTSPrint.NoOfPages + popUpDetails.PrintTo - currentPageTo;
                                            }
                                            else
                                            {
                                                QueueDoc.Document[0].DocumentDetails[i].PrintTo = gloGlobal.gloTSPrint.NoOfPages + 1;
                                            }
                                            if ((QueueDoc.Document[0].DocumentDetails[i].PrintTo < 1) || (QueueDoc.Document[0].DocumentDetails[i].PrintFrom > gloGlobal.gloTSPrint.NoOfPages))
                                            {
                                                QueueDoc.Document[0].DocumentDetails[i].PrintFrom = gloGlobal.gloTSPrint.NoOfPages + 1;
                                                QueueDoc.Document[0].DocumentDetails[i].PrintTo = gloGlobal.gloTSPrint.NoOfPages + 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        currentPageFrom += gloGlobal.gloTSPrint.NoOfPages;
                        currentPageTo += gloGlobal.gloTSPrint.NoOfPages;
                    }
                    QueueDoc.Document[0].DocumentDetails[i].Printer = popUpDetails.Printer;
                    QueueDoc.Document[0].DocumentDetails[i].Copies = popUpDetails.Copies;
                    QueueDoc.Document[0].DocumentDetails[i].Landscape = popUpDetails.Landscape;
                    QueueDoc.Document[0].DocumentDetails[i].Duplex = popUpDetails.Duplex;
                    QueueDoc.Document[0].DocumentDetails[i].Size = popUpDetails.Size;
                    QueueDoc.Document[0].DocumentDetails[i].Tray = popUpDetails.Tray;
                    QueueDoc.Document[0].DocumentDetails[i].isCollete = popUpDetails.isCollete;
                }
                if (claimType == "emdeon")
                {
                    QueueDoc.Document[0].DocumentDetails[i].Tray = ".mht";
                }
            }

            //Queue History
            QueueDoc.QueueHistory = new QueueQueueHistory[1];
            QueueDoc.QueueHistory[0] = new QueueQueueHistory();
            QueueDoc.QueueHistory[0].QueueHistoryDetails = new QueueQueueHistoryQueueHistoryDetails[1];
            QueueDoc.QueueHistory[0].QueueHistoryDetails[0] = new QueueQueueHistoryQueueHistoryDetails();
            QueueDoc.QueueHistory[0].QueueHistoryDetails[0].CreatedDateTime = Convert.ToDateTime(DateTime.Now).ToString("yyyyMMddhhmmss"); ;
            QueueDoc.QueueHistory[0].QueueHistoryDetails[0].Note = "";
            QueueDoc.QueueHistory[0].QueueHistoryDetails[0].UserName = "admin";
            QueueDoc.QueueHistory[0].QueueHistoryDetails[0].NoteType = "";


            return QueueDoc;
        }


        private string getFooterDetail(List<QueueFooterInfo> list)
        {
            if (list == null)
            {
                return "";
            }
            else
            {
                if (list.Count > 0)
                {
                    return list[0].FromPage.ToString() + "." +
                            list[0].ToPage.ToString() + "." +
                            list[0].StartingPage.ToString() + "." +
                            list[0].TotalPages.ToString();

                }
                else
                    return "";
            }

        }
        public static double InchesHundreds = 100;
        private QueueInsuranceInfo GetInsuranceInfo(DataTable dtHeader)
        {
            QueueInsuranceInfo _insuranceinfo = new QueueInsuranceInfo();
            try
            {

                _insuranceinfo.contact = Convert.ToString(dtHeader.Rows[0][col_Header_QueInsuranceContact]);
                _insuranceinfo.ContactID = Convert.ToInt64(dtHeader.Rows[0][col_Header_QueContactID]);
                _insuranceinfo.InsuranceName = Convert.ToString(dtHeader.Rows[0][col_Header_QueInsuranceName]);
                _insuranceinfo.addr = new QueueInsuranceInfoAddr[1];
                _insuranceinfo.addr[0] = new QueueInsuranceInfoAddr();
                _insuranceinfo.addr[0].streetAddressLine1 = Convert.ToString(dtHeader.Rows[0][col_Header_QueInsuranceAddline1]);
                _insuranceinfo.addr[0].city = Convert.ToString(dtHeader.Rows[0][col_Header_QueInsCity]);
                _insuranceinfo.addr[0].state = Convert.ToString(dtHeader.Rows[0][col_Header_QueInsState]);
                _insuranceinfo.addr[0].zip = Convert.ToString(dtHeader.Rows[0][col_Header_QueInsZip]);
                _insuranceinfo.addr[0].phone = Convert.ToString(dtHeader.Rows[0][col_Header_QueInsPhone]);
                _insuranceinfo.addr[0].mobile = Convert.ToString(dtHeader.Rows[0][col_Header_QueInsMobile]);
                _insuranceinfo.addr[0].fax = Convert.ToString(dtHeader.Rows[0][col_Header_QueInsFax]);
                _insuranceinfo.addr[0].email = Convert.ToString(dtHeader.Rows[0][col_Header_QueInsEmail]);
            }
            catch (Exception)
            {
                if (strError.Length == 0)
                {
                    strError.AppendLine("Error while writing Clinical chart queue metadata file: " + "Insurance Information");
                }
                else
                {
                    strError.AppendLine("Insurance Information");
                }
            }
            return _insuranceinfo;
        }
        public static InstalledPrinters GenerateInstalledPrintersDataFile()
        {
            InstalledPrinters aPrinterList = new InstalledPrinters();
            int printerTotalCount = 0;
            try
            {
                printerTotalCount = PrinterSettings.InstalledPrinters.Count;
            }
            catch
            {
                printerTotalCount = 0;
            }
            if (printerTotalCount > 0)
            {
                aPrinterList.Printer = new InstalledPrintersPrinter[printerTotalCount];
                int printercount = 0;
                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    PrinterSettings settings = new PrinterSettings();
                    try
                    {
                        settings.PrinterName = printer;
                    }
                    catch
                    {
                    }
                    InstalledPrintersPrinter aPrinter = new InstalledPrintersPrinter();
                    aPrinter.Name = printer;
                    try
                    {
                        aPrinter.CanDuplex = settings.CanDuplex;
                    }
                    catch
                    {
                    }
                    try
                    {
                        aPrinter.IsDefault = settings.IsDefaultPrinter;
                        if (aPrinter.IsDefault)
                        {
                            aPrinterList.Default = printer;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        aPrinter.Size = settings.DefaultPageSettings.PaperSize.PaperName;
                    }
                    catch
                    {
                    }
                    try
                    {
                        aPrinter.Tray = settings.DefaultPageSettings.PaperSource.SourceName;
                    }
                    catch
                    {
                    }
                    aPrinter.SizeTray = new InstalledPrintersPrinterSizeTray[1];
                    InstalledPrintersPrinterSizeTray aSizeTray = new InstalledPrintersPrinterSizeTray();
                    int aPaperSizeCount = 0;
                    try
                    {
                        aPaperSizeCount = settings.PaperSizes.Count;
                    }
                    catch
                    {
                    }
                    if (aPaperSizeCount > 0)
                    {
                        aSizeTray.Size = new InstalledPrintersPrinterSizeTraySize[aPaperSizeCount];
                        int sizeCount = 0;
                        foreach( PaperSize aPaperSize in settings.PaperSizes )
                        {
                            InstalledPrintersPrinterSizeTraySize aSizeTraySize = new InstalledPrintersPrinterSizeTraySize();
                            aSizeTraySize.Name = aPaperSize.PaperName;
                            
                            aSizeTraySize.Width = Convert.ToDouble((double)aPaperSize.Width / InchesHundreds);
                            aSizeTraySize.Height = Convert.ToDouble((double)aPaperSize.Height / InchesHundreds);
                            aSizeTraySize.SizeID = sizeCount;
                            aSizeTray.Size[sizeCount] = aSizeTraySize;
                            sizeCount++;
                        }
                    }
                    int aPaperTrayCount = 0;
                    try
                    {
                        aPaperTrayCount = settings.PaperSources.Count;
                    }
                    catch
                    {
                    }
                    if (aPaperTrayCount > 0)
                    {
                        aSizeTray.Tray = new InstalledPrintersPrinterSizeTrayTray[aPaperTrayCount];
                        int trayCount = 0;
                        foreach (PaperSource aPaperSource in settings.PaperSources)
                        {
                            InstalledPrintersPrinterSizeTrayTray aSizeTrayTray = new InstalledPrintersPrinterSizeTrayTray();
                            aSizeTrayTray.Name = aPaperSource.SourceName;
                            aSizeTrayTray.TrayID = trayCount;
                            aSizeTray.Tray[trayCount] = aSizeTrayTray;
                            trayCount++;
                        }
                    }
                    aPrinter.SizeTray[0] = aSizeTray;
                    aPrinter.PrinterID = printercount;
                    aPrinter.PrinterSettings = gloGlobal.gloTSPrint.PrinterSettingsToString(settings);
                    aPrinterList.Printer[printercount] = aPrinter;
                    printercount++;
                    settings = null;
                }
            }
            return aPrinterList;
        }
        public static InstalledPrintersPrinter GetPrinterProperties(InstalledPrinters aPrinterList, string Name)
        {
           for( int printerCount=0; printerCount < aPrinterList.Printer.Length; printerCount++)
            {
                InstalledPrintersPrinter aPrinter = aPrinterList.Printer[printerCount];
                if (aPrinter.Name == Name)
                {
                    return aPrinter;
                }
            }
            return null;
        }

        public static MasterConfigFileMasterConfig GenerateDefaultMasterConfigFile()
        {
            MasterConfigFileMasterConfig aMainConfig = new MasterConfigFileMasterConfig();
            try
            {
                aMainConfig.InstalledPrintersFile = "";
                aMainConfig.CurrentDefaultPrinter = "";
                String[] modules = Enum.GetNames(typeof(Classes.clsSettings.ModulePrintType));
                aMainConfig.ModulePrinters = new MasterConfigFileMasterConfigModulePrinters[modules.Length];
                for (int i = 0; i < modules.Length; i++)
                {
                    aMainConfig.ModulePrinters[i] = new MasterConfigFileMasterConfigModulePrinters();
                    aMainConfig.ModulePrinters[i].ModuleName = modules[i];
                    aMainConfig.ModulePrinters[i].PrinterName = "default";
                    aMainConfig.ModulePrinters[i].PrinterType = "default";
                    aMainConfig.ModulePrinters[i].SettingFile = "";
                }
            }
            catch
            {
            }
            return aMainConfig;
        }

        public static MasterConfigFileMasterConfig GetMasterConfigFileData(String MasterConfigFilePath)
        {
            MasterConfigFileMasterConfig mainConfig = null;
            try
            {
                if (File.Exists(MasterConfigFilePath))
                {
                    try
                    {
                        mainConfig = gloQueueSchema.gloSerialization.GetMasterConfigDocument(MasterConfigFilePath);
                    }
                    catch
                    {
                    }
                }
                if (mainConfig == null)
                {
                    mainConfig = gloQueueMetadatawriter.GenerateDefaultMasterConfigFile();
                }
            }
            catch
            {
            }
            return mainConfig;
        }

        public static void UpdateMasterConfigFileData(MasterConfigFileMasterConfig mainConfig,  String MasterConfigFilePath)
        {
            try
            {
                String tempFile = Path.Combine(Path.GetDirectoryName(MasterConfigFilePath), gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + DateTime.Now.Millisecond + ".xml");
                gloQueueSchema.gloSerialization.SetMasterConfigDocument(tempFile, mainConfig);
                if (File.Exists(tempFile))
                {
                    File.Copy(tempFile, MasterConfigFilePath, true);
                    File.Delete(tempFile);
                }
            }
            catch
            {
                
            }
        }

        public static Dictionary<String, String> GenerateDictionaryForModuleConfig(MasterConfigFileMasterConfig mainConfig)
        {
            Dictionary<String,String> dict = new Dictionary<String,String>();
            try
            {
                for (int i = 0; i < mainConfig.ModulePrinters.Length; i++)
                {
                    dict.Add(mainConfig.ModulePrinters[i].ModuleName + "_PrinterName", mainConfig.ModulePrinters[i].PrinterName);
                    dict.Add(mainConfig.ModulePrinters[i].ModuleName + "_PrinterType", mainConfig.ModulePrinters[i].PrinterType);
                    dict.Add(mainConfig.ModulePrinters[i].ModuleName + "_SettingsFile", mainConfig.ModulePrinters[i].SettingFile);
                }
            }
            catch
            {
            }
            return dict;
        }

        public static String getPrinterSettingsFileName(String mappedPath, String RegistryModuleName, Boolean isModulePrinter = true)
        {
            String PrinterSettingFile = "";
            String configFile = Path.Combine(mappedPath, gloGlobal.gloTSPrint.PrinterConfigDirectory, gloGlobal.gloTSPrint.MasterConfig);
            try
            {
                MasterConfigFileMasterConfig mainConfig = GetMasterConfigFileData(configFile);
                if (mainConfig != null)
                {
                    if (isModulePrinter)
                    {
                        for (int i = 0; i < mainConfig.ModulePrinters.Length; i++)
                        {
                            if (mainConfig.ModulePrinters[i].ModuleName == RegistryModuleName)
                            {
                                PrinterSettingFile = mainConfig.ModulePrinters[i].SettingFile;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (RegistryModuleName == "CurrentDefaultPrinter")
                        {
                            PrinterSettingFile = mainConfig.CurrentDefaultPrinter;
                        }
                        else if (RegistryModuleName == "InstalledPrintersFile")
                        {
                            PrinterSettingFile = mainConfig.InstalledPrintersFile;
                        }
                    }
                    
                    mainConfig = null;
                }
            }
            catch
            {
            }
            return PrinterSettingFile;
        }
    }
}
